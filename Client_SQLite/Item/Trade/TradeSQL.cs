using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Company.Objects;
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Trade.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Trade
{
    namespace TradeSQL
    {
        public class OperationSQL
        {
            public static class OperationFunctionsSQL
            {
                public const string OperationValue_Function = "dbo.Operation_GetOperation_Value";
                public const string OperationPaysValue_UPON_OperationCurrency_Function = "dbo.Operation_GetPays_Value_UPON_OperationCurrency";
            }
            DatabaseInterface DB;
            public OperationSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public Currency GetOperationItemINCurrency(Operation operation)
            {
                try
                {

                    switch (operation .OperationType )
                    {
                        
                        case Operation.BILL_BUY:
                            BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(operation.OperationID);
                            return new Currency(billbuy._Currency.CurrencyID, billbuy._Currency.CurrencyName
                                , billbuy._Currency.CurrencySymbol, billbuy.ExchangeRate, billbuy._Currency.ReferenceCurrencyID, billbuy ._Currency.Disable );
                        case Operation.ASSEMBLAGE:
                            return new CurrencySQL(DB).GetReferenceCurrency();

                        case Operation.DISASSEMBLAGE:
                            List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(operation);
                            if(itemoutlist.Count >0)
                            return itemoutlist[0]._ItemIN ._INCost._Currency ;
                            else 
                                return new CurrencySQL(DB).GetReferenceCurrency();

                        default:
                            throw new Exception("جلب عملة تكلفة ادخال العنصر: العملية غير صحيحة");
                           

                    }


                }
                catch(Exception ee)
                {
                    throw new Exception("فشل جلب عملة العملية المصدر" +ee.Message );
                }

            }
            public Currency GetOperation_BillAdditionalClause_Currency(Operation operation)
            {
                try
                {

                    switch (operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(operation.OperationID);
                            return billbuy._Currency ;
                        case Operation.BILL_SELL:
                            BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(operation.OperationID);
                            return BillSell_._Currency;

                        case Operation.BILL_MAINTENANCE:
                            BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(operation.OperationID);
                            return BillMaintenance_._Currency;
                        case Operation.RavageOPR:
                            return ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);

                        default:
                            throw new Exception("فشل جلب عملة الفاتورة : عملية غير صحيحة");

                    }


                }
                catch
                {
                    throw new Exception("فشل جلب عملة العملية المصدر");
                }

            }
            public Currency GetOperationItemOUTCurrency(Operation operation)
            {
                try
                {

                    switch (operation.OperationType)
                    {
                        case Operation.BILL_SELL:
                            BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(operation.OperationID);
                            return BillSell_._Currency;

                        case Operation.BILL_MAINTENANCE:
                            BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(operation.OperationID);
                            return BillMaintenance_._Currency;

                        case Operation.REPAIROPR:
                            RepairOPR repairopr= new Maintenance.MaintenanceSQL.RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(operation.OperationID);
                            BillMaintenance BillMaintenance2_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(repairopr._MaintenanceOPR);
                            return BillMaintenance2_._Currency;
                        case Operation.ASSEMBLAGE:
                            return ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);

                        case Operation.DISASSEMBLAGE:
                            return ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);

                        case Operation.RavageOPR:
                            return ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
                        default:
                            throw new Exception("جلب عملة تكلفة اخراج العنصر: العملية غير صحيحة");

                    }


                }
                catch
                {
                    throw new Exception("فشل جلب عملة العملية المصدر");
                }

            }
            public Bill GetOperationBill(Operation operation)
            {
                try
                {
                    switch (operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(operation.OperationID);
                            return billbuy;
                       case Operation.BILL_SELL:
                             BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(operation.OperationID);
                            return BillSell_;
                        case Operation.BILL_MAINTENANCE:
                             BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(operation.OperationID);
                            return BillMaintenance_;

                        default:
                            return null;


                    }
                }
                catch
                {
                    return null;
                }
            }
            public double  Get_OperationValue(uint OperationType,uint OperationID)
            {
                try
                {

                    Operation Operation_ = new Operation(OperationType, OperationID);
                    if (OperationType == Operation.BILL_BUY)
                    {
                        BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(OperationID);
                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(Operation_);
                        List<BillAdditionalClause> BillAdditionalClauselist = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(Operation_);
                        return (iteminlist.Sum(x => x._INCost.Value * x.Amount) + BillAdditionalClauselist.Sum(x => x.Value)-billbuy.Discount);
                    }
                    else if (OperationType == Operation.BILL_SELL)
                    {
                        BillSell billsell = new BillSellSQL(DB).GetBillSell_INFO_BYID(OperationID);

                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(Operation_);
                        List<BillAdditionalClause> BillAdditionalClauselist = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(Operation_);
                        return (itemoutlist.Sum(x => x._OUTValue.Value * x.Amount) + BillAdditionalClauselist.Sum(x => x.Value)-billsell.Discount);
                    }
                    else if (OperationType == Operation.BILL_MAINTENANCE)
                    {
                        BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(Operation_.OperationID);

                        List<BillMaintenance_Clause> BillMaintenance_ClauseList
                            = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_).Where(x => x.Value != null).ToList();


                        return BillMaintenance_ClauseList.Sum(x => Convert.ToDouble(x.Value))-BillMaintenance_.Discount;
                    }
                    else if (OperationType == Operation.Employee_PayOrder )
                    {
                        EmployeePayOrder EmployeePayOrder_ = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(Operation_.OperationID);



                        return EmployeePayOrder_.Value;
                    }
                    else if (OperationType == Operation.RavageOPR)
                        return 0;
                    else throw new Exception("operation type not found");

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_OperationValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }

            }
            public double Get_OperationPaysValue_UPON_OperationCurrency(uint OperationType, uint OperationID)
            {
                try
                {
                    Operation Operation_ = new Operation(OperationType, OperationID);
                    
                    if (OperationType == Operation.BILL_BUY )
                    {
                        List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(Operation_);
                        BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(OperationID);
                        double paysvalue = 0;
                        for(int i=0;i< payoutlist.Count;i++)
                        {
                            if (payoutlist[i]._Currency.CurrencyID == billbuy._Currency.CurrencyID)
                                paysvalue += payoutlist[i].Value;
                            else
                                paysvalue += payoutlist[i].Value*(billbuy .ExchangeRate  /payoutlist [i].ExchangeRate );
                        }
                        return paysvalue;
                    }
                    else if (OperationType == Operation.Employee_PayOrder )
                    {
                        List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(Operation_);
                        Company.Objects.EmployeePayOrder EmployeePayOrder
                            = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(OperationID);
                        double paysvalue = 0;
                        for (int i = 0; i < payoutlist.Count; i++)
                        {
                            if (payoutlist[i]._Currency.CurrencyID == EmployeePayOrder._Currency.CurrencyID)
                                paysvalue += payoutlist[i].Value;
                            else
                                paysvalue += payoutlist[i].Value * (EmployeePayOrder.ExchangeRate / payoutlist[i].ExchangeRate);
                        }
                        return paysvalue;
                    }
                    else if (OperationType == Operation.BILL_SELL)
                    {
                        List<PayIN> payinlist = new PayINSQL(DB).GetPayINList(Operation_);
                        BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(OperationID);
                        double paysvalue = 0;
                        for (int i = 0; i < payinlist.Count; i++)
                        {
                            if (payinlist[i]._Currency.CurrencyID == BillSell_._Currency.CurrencyID)
                                paysvalue += payinlist[i].Value;
                            else
                                paysvalue += payinlist[i].Value * (BillSell_.ExchangeRate / payinlist[i].ExchangeRate);
                        }
                        return paysvalue;
                    }
                    else if (OperationType == Operation.BILL_MAINTENANCE)
                    {
                        List<PayIN> payinlist = new PayINSQL(DB).GetPayINList(Operation_);
                        BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(OperationID);
                        double paysvalue = 0;
                        for (int i = 0; i < payinlist.Count; i++)
                        {
                            if (payinlist[i]._Currency.CurrencyID == BillMaintenance_._Currency.CurrencyID)
                                paysvalue += payinlist[i].Value;
                            else
                                paysvalue += payinlist[i].Value * (BillMaintenance_.ExchangeRate / payinlist[i].ExchangeRate);
                        }
                        return paysvalue;
                    }
                    else if (OperationType == Operation.RavageOPR)
                        return 0;
                    else throw new Exception("operation type not found");

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_OperationPaysValue_UPON_OperationCurrency:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }

            }
            public double Get_OperationValue(Operation Operation_)
            {
                try
                {
                   
                    if (Operation_.OperationType == Operation.BILL_BUY)
                    {
                        BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(Operation_.OperationID);
   
                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(Operation_);
                        List<BillAdditionalClause> BillAdditionalClauselist = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(Operation_);
                        return (iteminlist.Sum(x => x._INCost.Value * x.Amount) + BillAdditionalClauselist.Sum(x => x.Value))-billbuy.Discount ;
                    }
                    else if (Operation_.OperationType == Operation.BILL_SELL)
                    {
                        BillSell billsell = new BillSellSQL(DB).GetBillSell_INFO_BYID(Operation_.OperationID );

                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(Operation_);
                        List<BillAdditionalClause> BillAdditionalClauselist = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(Operation_);
                        return (itemoutlist.Sum(x => x._OUTValue.Value * x.Amount) + BillAdditionalClauselist.Sum(x => x.Value))-billsell.Discount;
                    }
                    else if (Operation_.OperationType == Operation.BILL_MAINTENANCE)
                    {
                        BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(Operation_.OperationID);

                        List<BillMaintenance_Clause> BillMaintenance_ClauseList
                            = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_).Where(x => x.Value != null).ToList();


                        return BillMaintenance_ClauseList.Sum(x => Convert.ToDouble(x.Value))-BillMaintenance_.Discount ;
                    }
                    else if (Operation_. OperationType == Operation.RavageOPR)
                        return 0;
                    else throw new Exception("operation type not found");
                   
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_OperationValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }

            }
            public double Get_OperationPaysValue_UPON_OperationCurrency(Operation Operation_)
            {
                try
                {

                    if (Operation_.OperationType == Operation.BILL_BUY)
                    {
                        List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(Operation_);
                        BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(Operation_.OperationID);
                        double paysvalue = 0;
                        for (int i = 0; i < payoutlist.Count; i++)
                        {
                            if (payoutlist[i]._Currency.CurrencyID == billbuy._Currency.CurrencyID)
                                paysvalue += payoutlist[i].Value;
                            else
                                paysvalue += payoutlist[i].Value * (billbuy.ExchangeRate / payoutlist[i].ExchangeRate);
                        }
                        return paysvalue;
                    }
                    else if (Operation_.OperationType == Operation.Employee_PayOrder)
                    {
                        List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(Operation_);
                        Company.Objects.EmployeePayOrder EmployeePayOrder
                            = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(Operation_.OperationID);
                        double paysvalue = 0;
                        for (int i = 0; i < payoutlist.Count; i++)
                        {
                            if (payoutlist[i]._Currency.CurrencyID == EmployeePayOrder._Currency.CurrencyID)
                                paysvalue += payoutlist[i].Value;
                            else
                                paysvalue += payoutlist[i].Value * (EmployeePayOrder.ExchangeRate / payoutlist[i].ExchangeRate);
                        }
                        return paysvalue;
                    }
                    else if (Operation_.OperationType == Operation.BILL_SELL)
                    {
                        List<PayIN> payinlist = new PayINSQL(DB).GetPayINList(Operation_);
                        BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(Operation_.OperationID);
                        double paysvalue = 0;
                        for (int i = 0; i < payinlist.Count; i++)
                        {
                            if (payinlist[i]._Currency.CurrencyID == BillSell_._Currency.CurrencyID)
                                paysvalue += payinlist[i].Value;
                            else
                                paysvalue += payinlist[i].Value * (BillSell_.ExchangeRate / payinlist[i].ExchangeRate);
                        }
                        return paysvalue;
                    }
                    else if (Operation_.OperationType == Operation.BILL_MAINTENANCE)
                    {
                        List<PayIN> payinlist = new PayINSQL(DB).GetPayINList(Operation_);
                        BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(Operation_.OperationID);
                        double paysvalue = 0;
                        for (int i = 0; i < payinlist.Count; i++)
                        {
                            if (payinlist[i]._Currency.CurrencyID == BillMaintenance_._Currency.CurrencyID)
                                paysvalue += payinlist[i].Value;
                            else
                                paysvalue += payinlist[i].Value * (BillMaintenance_.ExchangeRate / payinlist[i].ExchangeRate);
                        }
                        return paysvalue;
                    }
                    else throw new Exception("operation type not found");

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_OperationPaysValue_UPON_OperationCurrency:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }


            }
            internal List <Operation > GetItemOUT_Real_OUTOperations( ItemOUT ItemOUT_)
            {
                List<Operation> operationlist = new List<Operation>();
                try
                {
                    if (ItemOUT_._Operation.OperationType == Operation.BILL_SELL
                        || ItemOUT_._Operation.OperationType == Operation.BILL_MAINTENANCE
                         || ItemOUT_._Operation.OperationType == Operation.RavageOPR)
                    {
                        operationlist.Add (ItemOUT_._Operation);
                    }
                    else if (ItemOUT_._Operation.OperationType == Operation.REPAIROPR)
                    {
                        RepairOPR repairopr = new Maintenance.MaintenanceSQL.RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(ItemOUT_._Operation.OperationID);
                        BillMaintenance BillMaintenance_
                            = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(repairopr._MaintenanceOPR);
                        operationlist.Add (BillMaintenance_._Operation);
                    }
                    else if(ItemOUT_._Operation.OperationType == Operation.ASSEMBLAGE || ItemOUT_._Operation.OperationType == Operation.DISASSEMBLAGE )
                    {
                        List<ItemIN> ItemINList = new ItemINSQL(DB).GetItemINList(ItemOUT_._Operation);
                        for(int i=0;i<ItemINList .Count;i++)
                        {
                            List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("GetItemOUT_Real_OUTOperations",ItemINList[i]);
                            for (int j=0;j<ItemOUTList.Count;j++)
                            {
                                operationlist.AddRange(GetItemOUT_Real_OUTOperations(ItemOUTList[j]));
                            }
                        }

                    }
 

                }
                catch (Exception ee)
                {

                    System.Windows.Forms.MessageBox.Show("GetItemIN_Real_OUTOperations" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return operationlist;
            }
        }
        public class BillAdditionalClauseSQL
        {
            DatabaseInterface DB;
            private static class BillAdditionalClauseTable
            {
                public const string TableName = "Trade_Bill_AdditionalClause";
                public const string ClauseID = "ClauseID";
                public const string OperationType = "OperationType";
                public const string OperationID = "OperationID";
                public const string Description = "Description";
                public const string Value_ = "Value_";


            }
            public BillAdditionalClauseSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public BillAdditionalClause Get_BillAdditionalClause_INFO_BYID(uint ClauseID)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + BillAdditionalClauseTable.OperationType + ","
                    + BillAdditionalClauseTable.OperationID + ","
                    + BillAdditionalClauseTable.Description + ","
                    + BillAdditionalClauseTable.Value_
                    + " from   "
                    + BillAdditionalClauseTable.TableName
                    + " where "
                    + BillAdditionalClauseTable.ClauseID + "=" + ClauseID
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint operation_type = Convert.ToUInt32(t.Rows[0][0]);
                        uint operation_id = Convert.ToUInt32(t.Rows[0][1]);
                        string desc = t.Rows[0][2].ToString();
                        double value = Convert.ToDouble(t.Rows[0][3].ToString());

                        return new BillAdditionalClause(new Operation(operation_type, operation_id), ClauseID,
                            desc, value);

                    }
                    else
                        return null;
                }
                catch(Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_BillAdditionalClause_INFO_BYID:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
               
            }
            public bool  AddBillAdditionalClause(Operation Operation_,string description, double Value)
            {
                try
                {
                    switch (Operation_.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_SELL:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_MAINTENANCE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        default:
                            break;

                    }

                    DataTable t = DB.GetData(" insert into "
                    + BillAdditionalClauseTable.TableName
                    + "("
                    + BillAdditionalClauseTable.OperationType
                    + ","
                    + BillAdditionalClauseTable.OperationID
                    + ","
                    + BillAdditionalClauseTable.Description 
                    + ","
                    + BillAdditionalClauseTable.Value_ 
                    
                    + ")"
                    + "values"
                    + "("
                   + Operation_.OperationType
                    + ","
                    + Operation_.OperationID
                    + ","
                    +"'"+description +"'"
                    + ","
                    + Value         
                    + ")"
                    );
  


                    DB.AddLog(
                          DatabaseInterface.Log.LogType.INSERT
                          , DatabaseInterface.Log.Log_Target.Trade_BillAdditionalClause
                          , ""
                          , true, "");

                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_BillAdditionalClause
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("AddBillAdditionalClause:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool UpdateBillAdditionalClause(uint ClauseID, string description, double Value)
            {
                try
                {
                    BillAdditionalClause billadditionalclause = new BillAdditionalClauseSQL(DB).Get_BillAdditionalClause_INFO_BYID(ClauseID);
                    switch (billadditionalclause._Operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_SELL:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_MAINTENANCE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        default:
                            break;

                    }
                    DB.ExecuteSQLCommand("update  "
                    + BillAdditionalClauseTable.TableName
                    + " set "
   
                    + BillAdditionalClauseTable.Description  + "='" + description + "'"
                    + ","
                    + BillAdditionalClauseTable.Value_  + "=" + Value 
                   
                    + " where "
                    + BillAdditionalClauseTable.ClauseID  + "=" + ClauseID
                    );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.UPDATE
                        , DatabaseInterface.Log.Log_Target.Trade_BillAdditionalClause 
                         , ""
                         , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE
                            , DatabaseInterface.Log.Log_Target.Trade_BillAdditionalClause 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("UpdateBillAdditionalClause:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteBillAdditionalClause(uint clauseID)
            {
                try
                {
                    BillAdditionalClause billadditionalclause = new BillAdditionalClauseSQL(DB).Get_BillAdditionalClause_INFO_BYID(clauseID);
                    switch (billadditionalclause._Operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_SELL:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_MAINTENANCE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        default:
                            break;

                    }
                    DB.ExecuteSQLCommand("delete from   "
                    + BillAdditionalClauseTable.TableName
                    + " where "
                    + BillAdditionalClauseTable.ClauseID  + "=" + clauseID
                    );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE
                       , DatabaseInterface.Log.Log_Target.Trade_BillAdditionalClause 
                        , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE
                            , DatabaseInterface.Log.Log_Target.Trade_BillAdditionalClause 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("DeleteBillAdditionalClause:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List <BillAdditionalClause > GetBill_AdditionalClauses(Operation Operation_)
            {
                List<BillAdditionalClause> List = new List<BillAdditionalClause>();
                try
                {
                    DataTable  t = DB.GetData("select "
                    + BillAdditionalClauseTable.ClauseID  + ","
                    + BillAdditionalClauseTable.Description + ","
                    + BillAdditionalClauseTable.Value_
                    + " from   "
                    + BillAdditionalClauseTable.TableName
                    + " where "
                    + BillAdditionalClauseTable.OperationType + "=" + Operation_.OperationType
                     + " and "
                    + BillAdditionalClauseTable.OperationID + "=" + Operation_.OperationID 
                      );
                    for(int i=0;i< t.Rows.Count;i++)
                    {
                        uint ClauseID = Convert.ToUInt32(t.Rows[i][0]);
                        string desc = t.Rows[i][1].ToString();
                        double value = Convert.ToDouble(t.Rows[i][2].ToString());

                        List .Add ( new BillAdditionalClause(Operation_, ClauseID,
                            desc, value));

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBill_AdditionalClauses:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
                return List;
            }

            internal IEnumerable<Money_Currency> GetBill_AdditionalClauses_AS_Money_Currency(Currency _Currency, double exchangeRate, Operation _Operation)
            {
                List<Money_Currency> List = new List<Money_Currency>();
                try
                {
                    DataTable t = DB.GetData("select "
                    + BillAdditionalClauseTable.Value_
                    + " from   "
                    + BillAdditionalClauseTable.TableName
                    + " where "
                    + BillAdditionalClauseTable.OperationType + "=" + _Operation.OperationType
                     + " and "
                    + BillAdditionalClauseTable.OperationID + "=" + _Operation.OperationID
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        double value = Convert.ToDouble(t.Rows[i][0].ToString());

                        List.Add(new Money_Currency (_Currency , value, exchangeRate));

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBill_AdditionalClauses:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
                return List;
            }
        }
        public class BillBuySQL
        {
            DatabaseInterface DB;
            private static class BillBuyTable
            {
                public const string TableName = "Trade_BillBuy";
                public const string BillBuyID = "BillBuyID";
                public const string BillDate = "BillDate";
                public const string BillDescription = "BillDescription";
                public const string ContactID = "ContactID";
                public const string CurrencyID = "CurrencyID";
                public const string ExchangeRate = "ExchangeRate";
                public const string Discount = "Discount";
                public const string Notes = "Notes";

            }
            public BillBuySQL(DatabaseInterface db)
            {
                DB = db;

            }
            public BillBuy GetBillBuy_INFO_BYID(uint billid)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                + BillBuyTable.BillDate+","
                + BillBuyTable.BillDescription + ","
                + BillBuyTable.ContactID + ","
                + BillBuyTable.CurrencyID + ","
                + BillBuyTable.ExchangeRate  + ","
                + BillBuyTable.Discount + ","
                + BillBuyTable.Notes 
                + " from   "
                + BillBuyTable.TableName
                + " where "
                + BillBuyTable.BillBuyID + "=" + billid 
                  );
                if (t.Rows.Count == 1)
                {
                    DateTime billdate = Convert.ToDateTime(t.Rows [0][0]);
                    string desc = t.Rows[0][1].ToString ();
                    Contact  Contact_ = new ContactSQL(DB).GetContactInforBYID (Convert.ToUInt32(t.Rows[0][2].ToString()));
                    Currency Currency_ = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][3].ToString()));
                    double exchangerate = Convert.ToDouble(t.Rows[0][4].ToString());
                    double discount =Convert .ToDouble ( t.Rows[0][5].ToString());
                    string notes = t.Rows[0][6].ToString();
                    if (Currency_.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                    return new BillBuy  (billid  ,billdate  ,desc ,Contact_,Currency_,exchangerate ,discount ,notes );

                }
                else
                    return null;
            }
            public BillBuy GetBillBuy_INFO_BY_RowID(uint rowid)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                      + BillBuyTable.BillBuyID + ","
                + BillBuyTable.BillDate + ","
                + BillBuyTable.BillDescription + ","
                + BillBuyTable.ContactID + ","
                + BillBuyTable.CurrencyID + ","
                + BillBuyTable.ExchangeRate + ","
                + BillBuyTable.Discount + ","
                + BillBuyTable.Notes
                + " from   "
                + BillBuyTable.TableName
                + " where "
                + DatabaseInterface .ROWID_COLUMN  + "=" + rowid 
                  );
                if (t.Rows.Count == 1)
                {
                    uint billid= Convert.ToUInt32 (t.Rows[0][0]);
                    DateTime billdate = Convert.ToDateTime(t.Rows[0][1]);
                    string desc = t.Rows[0][2].ToString();
                    Contact Contact_ = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[0][3].ToString()));
                    Currency Currency_ = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                    double exchangerate = Convert.ToDouble(t.Rows[0][5].ToString());
                    double discount = Convert.ToDouble(t.Rows[0][6].ToString());
                    string notes = t.Rows[0][7].ToString();
                    if (Currency_.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                    return new BillBuy(billid, billdate, desc, Contact_, Currency_, exchangerate, discount, notes);

                }
                else
                    return null;
            }
            public BillBuy   AddBillBuy( DateTime billdate,string description,Contact contact,Currency currency,double ExchangeRate,double discount,string notes)
            {
                try
                {
           
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (currency.ReferenceCurrencyID == null && ExchangeRate != 1) throw new Exception("معامل صرف العملة الرجعية يجب أن يكون 1");
                    DataTable t = DB.GetData (" insert into "
                    + BillBuyTable.TableName
                    + "("
                    + BillBuyTable.BillDate  
                    + ","
                    + BillBuyTable.BillDescription 
                    + ","
                    + BillBuyTable.ContactID 
                    + ","
                    + BillBuyTable.CurrencyID 
                    + ","
                    + BillBuyTable.ExchangeRate 
                    + ","
                    + BillBuyTable.Discount 
                    + ","
                    + BillBuyTable.Notes 
                    + ")"
                    + "values"
                    + "("
                    + "'" + billdate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + "'"+description +"'" 
                    + ","
                    +contact .ContactID 
                    + ","
                    + currency .CurrencyID 
                    + ","
                     +ExchangeRate 
                    + ","
                    + discount 
                    + ","
                    + "'"+notes +"'"
                    + ")"
                    + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows [0][0].ToString ());

                    
                    DB.AddLog(
                                                    DatabaseInterface.Log.LogType.INSERT
                                                    , DatabaseInterface.Log.Log_Target.Trade_BillBuy 
                                                    , ""
                                                                              , true, "");
                    return GetBillBuy_INFO_BY_RowID(rowid);
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_BillBuy 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
            }
            public bool UpdateBillBuy(uint billid,DateTime billoutdate,string description, Contact contact,Currency currency,double ExchangeRate,double discount,string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (currency.ReferenceCurrencyID == null && ExchangeRate != 1) throw new Exception("معامل صرف العملة الرجعية يجب أن يكون 1");

                    DB.ExecuteSQLCommand("update  "
                    + BillBuyTable.TableName
                    + " set "
                    + BillBuyTable.BillDate  + "='" + billoutdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + BillBuyTable.BillDescription + "='" + description   + "'"
                    + ","
                    + BillBuyTable.ContactID + "=" + contact .ContactID 
                    + ","
                    + BillBuyTable.CurrencyID  + "=" + currency .CurrencyID
                    + ","
                     + BillBuyTable.ExchangeRate  + "=" + ExchangeRate 
                    + ","
                    + BillBuyTable.Discount  + "=" + discount  
                    + ","
                    + BillBuyTable.Notes  + "='" + notes  + "'"

                    + " where "
                    + BillBuyTable.BillBuyID  + "=" + billid  
                    );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.UPDATE 
                        , DatabaseInterface.Log.Log_Target.Trade_BillBuy
                         , ""
                         , true, "");
                    return true ;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_BillBuy
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool DeleteBillBuy(uint billbuyid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    BillBuy billbuy = GetBillBuy_INFO_BYID(billbuyid);
                    if (billbuy == null) throw new Exception("BillNuy NULL");
                    if (new ItemINSQL(DB).GetItemINList(billbuy._Operation).Count > 0 || new BillAdditionalClauseSQL (DB).GetBill_AdditionalClauses (billbuy ._Operation ).Count >0) 
                    {
                        throw new Exception("يجب حذف بنود الفاتورة اولا");
                    }
                    if (new PayOUTSQL(DB).GetPaysOUT_List(billbuy._Operation).Count > 0) throw new Exception("يجب حذف كل الدفعات التابعة لهذه الفاتورة أولا");
                   
                    DB.ExecuteSQLCommand("delete from   "
                    + BillBuyTable.TableName
                    + " where "
                    + BillBuyTable.BillBuyID  + "=" + billbuy ._Operation.OperationID   
                    );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE 
                       , DatabaseInterface.Log.Log_Target.Trade_BillBuy
                        , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_BillBuy
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<BillBuy> Get_All_BillBuy_List()
            {
                List<BillBuy> list = new List<BillBuy>();
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + BillBuyTable.BillBuyID  + ","
                    + BillBuyTable.BillDate + ","
                    + BillBuyTable.BillDescription + ","
                    + BillBuyTable.ContactID + ","
                    + BillBuyTable.CurrencyID + ","
                    + BillBuyTable.ExchangeRate + ","
                    + BillBuyTable.Discount + ","
                    + BillBuyTable.Notes
                    + " from   "
                    + BillBuyTable.TableName

                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint BillID = Convert.ToUInt32(t.Rows [i][0].ToString ());
                        DateTime BillDate_ = Convert .ToDateTime (t.Rows[i][1].ToString());
                        string BillDescription_= t.Rows[i][2].ToString();
                        Contact Contact_ = new ContactSQL(DB)
                            .GetContactInforBYID(Convert.ToUInt32(t.Rows[i][3].ToString()));
                        Currency Currency_=new CurrencySQL (DB)
                            .GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][4].ToString()));
                        double ExchangeRate_ = Convert.ToDouble(t.Rows[i][5].ToString());
                        double discount = Convert.ToDouble(t.Rows[i][6].ToString());
                        string Notes = t.Rows[i][7].ToString();
                        if (Currency_.ReferenceCurrencyID == null && ExchangeRate_ != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                        BillBuy billbuy = new BillBuy(BillID, BillDate_, BillDescription_
                        , Contact_, Currency_,
                            ExchangeRate_, discount, Notes);
                        list.Add(billbuy);
                    }
                    return list ;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_All_BillBuy_List:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list ;
                }
            }
            internal double GetBillBuyValue(uint billid)
            {
                try
                {

                    return new OperationSQL(DB).Get_OperationValue(Operation.BILL_BUY,billid);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillBuyValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }
            }
            internal double GetBillBuy_PaysValue(uint billid)
            {
                try
                {

                    return new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Operation.BILL_BUY, billid);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillBuyValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }
            }
            internal List <PayIN > Get_Billbuy__Returns_Pays(uint billbuyid)
            {
                List<PayIN> PayINList = new List<PayIN>();
                try
                {
                    PayINSQL PayINSQL_= new PayINSQL(DB);
                    List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(new Operation(Operation.BILL_BUY, billbuyid));

                    for (int i=0;i<iteminlist .Count;i++)
                    {
                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("Get_Billbuy__Returns_Pays",iteminlist[i]);

                        for (int j=0;j< itemoutlist.Count;j++)
                        {
                            List<Operation> operationlist = new OperationSQL(DB).GetItemOUT_Real_OUTOperations(itemoutlist[j]);
                            for(int k=0;k<operationlist .Count;k++)
                            {
                                PayINList.AddRange(PayINSQL_ .GetPayINList(operationlist[k]));
                            }
                        } 
                    }
                }
                catch (Exception ee)
                {

                    System.Windows.Forms.MessageBox.Show("Get_Billbuy__Returns_Pays" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return PayINList;
            }

       
        }
        public class ContactSQL
        {

            DatabaseInterface DB;
            
            private static class ContactBillsTable
            {
                public const string TableName = " [dbo].[Bills_Get_Contact_BillsReport_Details]";
                public const string Bill_Date = "Bill_Date";
                public const string Bill_ID = "Bill_ID";
                public const string BillType = "BillType";
                public const string Bill_Description = "Bill_Description";
                public const string Bill_Operations = "Bill_Operations";
                public const string Currency_Name = "Currency_Name";
                public const string Bill_Value = "Bill_Value";
                public const string Pays_Value = "Pays_Value";
                public const string Remain = "Remain";

            }

            private static class ContactBillsReportTable
            {
                public const string TableName = "[dbo].[Bills_Get_Contact_BillsReport]";
                public const string CurrencyID = "CurrencyID";
                public const string Currency = "Currency";
                public const string BillsIN_Count = "BillsIN_Count";
                public const string BillsIN_Value = "BillsIN_Value";
                public const string BillsIN_Pays_Value = "BillsIN_Pays_Value";
                public const string BillsM_Count = "BillsM_Count";
                public const string BillsM_Value = "BillsM_Value";
                public const string BillsM_Pays_Value = "BillsM_Pays_Value";
                public const string BillsOUT_Count = "BillsOUT_Count";
                public const string BillsOUT_Value = "BillsOUT_Value";
                public const string BillsOUT_Pays_Value = "BillsOUT_Pays_Value";


            }
            private static class ContactTable
            {
                public const string TableName = "Trade_Contact";
                public const string ContactID = "ContactID";
                public const string ContactType = "ContactType";
                public const string Name = "Name";
                public const string Phone = "Phone";
                public const string Mobile = "Mobile";
                public const string Address = "Address";

            }
            public ContactSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public Contact GetContactInforBYID(uint contactid)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                    +ContactTable.ContactType + ","
                    + ContactTable.Name +","
                    + ContactTable.Phone + ","
                    + ContactTable.Mobile + ","
                    + ContactTable.Address 
                    + " from   "
                    + ContactTable.TableName
                    + " where "
                    + ContactTable.ContactID  + "=" + contactid
                  );
                if (t.Rows.Count == 1)
                {
                    bool contacttype = Convert.ToInt32(t.Rows[0][0].ToString())==1?true :false ;
                    string name = t.Rows[0][1].ToString();
                    string phone = t.Rows[0][2].ToString();
                    string mobile = t.Rows[0][3].ToString();
                    string address = t.Rows[0][4].ToString();

                    return new Contact(contactid,contacttype  ,name , phone, mobile, address);

                }
                else
                    return null;
            }
            public bool AddContact(bool type, string name,string phone,string mobile,string address)
            {
                try
                {

                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Contact_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" insert into "
                    + ContactTable.TableName
                    + "("
                    + ContactTable.ContactType + ","
                    + ContactTable.Name+","
                    + ContactTable.Phone  + ","
                    + ContactTable.Mobile  + ","
                    + ContactTable.Address  
                    + ")"
                    + "values"
                    + "("
                    +(type?1:0).ToString ()
                    +","
                    + "'" + name   + "'"
                    +","
                    + "'" + phone  + "'"
                    + ","
                    + "'" + mobile  + "'"
                    + ","
                    + "'" + address  + "'"
                    + ")"
                    );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.INSERT 
                       , DatabaseInterface.Log.Log_Target.Trade_Contact
                        , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT 
                            , DatabaseInterface.Log.Log_Target.Trade_Contact
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdateContact(uint contactid,bool type, string name, string phone, string mobile, string address)
            {
                try
                {

                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Contact_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                       + ContactTable.TableName
                       + " set "
                       + ContactTable.ContactType + "=" + (type ? "1" : "0")
                       +","
                       + ContactTable.Name   + "='" + name   + "'"
                       +","
                       + ContactTable.Phone + "='" + phone  + "'"
                       + ","
                       + ContactTable.Mobile  + "='" + mobile  + "'"
                       + ","
                       + ContactTable.Address + "='" + address  + "'"
                    + " where "
                    + ContactTable.ContactID + "=" + contactid
                    );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                      , DatabaseInterface.Log.Log_Target.Trade_Contact
                       , ""
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_Contact
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteContact(uint contactid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Contact_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (new ContactSQL(DB).Get_Contact_Buys_ReportDetail(contactid).Count > 0) throw new Exception("يجب حذف كل فواتير الشراء التابعة للهذه الجهة أولا");
                    if (new ContactSQL(DB).Get_Contact_Sells_ReportDetail (contactid).Count > 0) throw new Exception("يجب حذف كل فواتير المبيعات التابعة للهذه الجهة أولا");
                    if (new ContactSQL(DB).Get_Contact_MaintenanceOPRs_ReportDetail (contactid).Count > 0) throw new Exception("يجب حذف كل عمليات الصيانة التابعة للهذه الجهة أولا");

                    DB.ExecuteSQLCommand("delete from   "
                    + ContactTable.TableName
                    + " where "
                    + ContactTable.ContactID   + "=" + contactid
                    );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE 
                       , DatabaseInterface.Log.Log_Target.Trade_Contact
                        , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_Contact
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<Contact > GetContactList()
            {
                List<Contact> contactlist = new List<Contact>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ContactTable.ContactID + ","
                    + ContactTable.ContactType + ","
                    + ContactTable.Name + ","
                    + ContactTable.Phone + ","
                    + ContactTable.Mobile + ","
                    + ContactTable.Address
                    + " from   "
                    + ContactTable.TableName
                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint contactid =  Convert.ToUInt32(t.Rows[i][0].ToString());

                        bool contacttype = Convert.ToInt32(t.Rows[i][1].ToString()) == 1 ? true : false; ;
                        string name = t.Rows[i][2].ToString();
                        string phone = t.Rows[i][3].ToString();
                        string mobile = t.Rows[i][4].ToString();
                        string address = t.Rows[i][5].ToString();

                        contactlist.Add ( new Contact(contactid, contacttype, name, phone, mobile, address));
                    }
                    return contactlist;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetContactList:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return contactlist;
                }
            }
            internal List<Contact> SearchContact(string text)
            {
                List<Contact> list = new List<Contact>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ContactTable.ContactID + ","
                    + ContactTable.ContactType + ","
                    + ContactTable.Name + ","
                    + ContactTable.Phone + ","
                    + ContactTable.Mobile + ","
                    + ContactTable.Address
                    + " from   "
                    + ContactTable.TableName
                       + " where " + ContactTable.Name  + " like  '%" +text  + "%'");
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint contactid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        bool contacttype = Convert.ToBoolean(t.Rows[i][1].ToString());
                        string name = t.Rows[i][2].ToString();
                        string phone = t.Rows[i][3].ToString();
                        string mobile = t.Rows[i][4].ToString();
                        string address = t.Rows[i][5].ToString();

                        list.Add(new Contact(contactid, contacttype, name, phone, mobile, address));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("SearchContact:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list;
                }
            }
            //public List<BillReportDetail> GetContactBillsList(Contact Contact_)
            //{
            //    List<BillReportDetail> BillReportDetailList = new List<BillReportDetail>();
            //    try
            //    {

            //        DataTable table = new DataTable();
            //        table = DB.GetData("select "
            //            + ContactBillsTable.Bill_Date +","
            //            + ContactBillsTable.Bill_ID + ","
            //            + ContactBillsTable.BillType + ","
            //            + ContactBillsTable.Bill_Description + ","
            //            + ContactBillsTable.Bill_Operations + ","
            //            + ContactBillsTable.Currency_Name + ","
            //            + ContactBillsTable.Bill_Value + ","
            //            + ContactBillsTable.Pays_Value  + ","
            //            + ContactBillsTable.Remain  
            //            + " from  "
            //            +ContactBillsTable .TableName 
            //            +"("
            //            + Contact_.ContactID 
            //            +")"
            //            + " order by Bill_Date"

            //      );

            //        for (int i = 0; i <table .Rows.Count; i++)
            //        {
            //            DateTime billdate = Convert.ToDateTime(table.Rows[i][0].ToString());
            //            int billid = Convert.ToInt32(table.Rows[i][1].ToString());
            //            string billtype = table.Rows[i][2].ToString();
            //            string desc = table.Rows[i][3].ToString();
            //            string operations = table.Rows[i][4].ToString();
            //            string currency = table.Rows[i][5].ToString();
            //            double value = Convert.ToDouble(table.Rows[i][6].ToString());
            //            double paid = Convert.ToDouble(table.Rows[i][7].ToString());
            //            double remain = Convert.ToDouble(table.Rows[i][8].ToString());
            //            BillReportDetail billreportDetail = new BillReportDetail(billdate, billid, billtype, desc, Contact_.ContactName, operations, currency, value, paid, remain);
            //            BillReportDetailList.Add(billreportDetail);
            //        }
            //        return BillReportDetailList;

            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return BillReportDetailList;
            //    }
            //}
         

            #region ReportDetails

            public List<Contact_Pays_ReportDetail> Get_Contact_Pays_ReportDetail(uint ContactID)
            {
                //return new DataBaseFunctions(DB).Contact_Get_Pays_ReportDetail(ContactID);
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Pays_ReportDetail,
                       para);

                    return Contact_Pays_ReportDetail.Get_Contact_Contact_Pays_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Pays_ReportDetail:" + ee.Message);

                }
            }
            public List<Contact_MaintenanceOPRs_ReportDetail> Get_Contact_MaintenanceOPRs_ReportDetail(uint ContactID)
            {

                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Maintenance_ReportDetail,
                       para);

                    return Contact_MaintenanceOPRs_ReportDetail.Get_Contact_MaintenanceOPRs_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_MaintenanceOPRs_ReportDetail:" + ee.Message);

                }
            }
            public List<Contact_Sells_ReportDetail> Get_Contact_Sells_ReportDetail(uint ContactID)
            {
                //return new DataBaseFunctions(DB).Contact_Get_Sells_ReportDetail(ContactID);
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Sells_ReportDetail,
                       para);

                    return Contact_Sells_ReportDetail.Get_Contact_Sells_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Sells_ReportDetail:" + ee.Message);

                }
            }
            public List<Contact_Buys_ReportDetail> Get_Contact_Buys_ReportDetail(uint ContactID)
            {
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID",typeof (uint ));
                    DataRow row= para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Buys_ReportDetail,
                       para);

                    return Contact_Buys_ReportDetail.Get_Contact_Buys_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Buys_ReportDetail:" + ee.Message);

                }
            }
            #endregion
            #region Report
            public Contact_MaintenanceOPRs_Report Get_Contact_MaintenanceOPRs_Report(uint ContactID)
            {
                //return new DataBaseFunctions(DB).Contact_Get_Report_Sells(ContactID);
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Maintenance_Report,
                       para);

                    return Contact_MaintenanceOPRs_Report.Get_Contact_MaintenanceOPRs_Report_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Sells_Report:" + ee.Message);

                }
            }
            public Contact_Sells_Report Get_Contact_Sells_Report(uint ContactID)
            {
                //return new DataBaseFunctions(DB).Contact_Get_Report_Sells(ContactID);
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Sells_Report,
                       para);

                    return Contact_Sells_Report.Get_Contact_Sells_Report_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Sells_Report:" + ee.Message);

                }
            }
            public Contact_Buys_Report Get_Contact_Buys_Report(uint ContactID)
            {
                //return new DataBaseFunctions(DB).Contact_Get_Report_Buys(ContactID);
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Buys_Report,
                       para);

                    return Contact_Buys_Report.Get_Contact_Buys_Report_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Buys_Report:" + ee.Message);

                }
            }
            
            public List<Contact_PayCurrencyReport> Get_Contact_PayCurrencyReport(uint ContactID)
            {
                //return new DataBaseFunctions(DB).Contact_Get_Report_Pays(ContactID);
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_PayCurrencyReport,
                       para);

                    return Contact_PayCurrencyReport.Get_Contact_PayCurrencyReport_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_PayCurrencyReport:" + ee.Message);

                }
            }
            public List<Contact_BillCurrencyReport> Contact_GetBillsReportList(uint ContactID)
            {
                try
                {
                    //return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
                    DataTable para = new DataTable();
                    para.Columns.Add("ContactID", typeof(uint));
                    DataRow row = para.NewRow();
                    row["ContactID"] = ContactID;
                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.ContactSQL_Contact_GetBillsReportList,
                       para);

                    return Contact_BillCurrencyReport.Get_Contact_BillCurrencyReport_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_PayCurrencyReport:" + ee.Message);

                }
            }
            #endregion

        }
        public class SellTypeSql
        {
            public static class SellTypeTable
            {
                public const string TableName = "Trade_SellTypes";
                public const string SellTypeID = "SellTypeID";
                public const string SellTypeName = "SellTypeName";
                public const string Disable = "Disable";
            }
            DatabaseInterface DB;
            public SellTypeSql(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public SellType GetSellTypeinfo(uint selltypeid)
            {

                DataTable t = DB.GetData("select "
                + SellTypeTable.SellTypeID + ","
                + SellTypeTable.SellTypeName
                + " from  "
                + SellTypeTable.TableName
                + " where "
                + SellTypeTable.SellTypeID + "=" + selltypeid);
               
                if (t.Rows.Count == 0) return null;
                else return new SellType(Convert.ToUInt32(t.Rows[0][0].ToString()), t.Rows[0][1].ToString());
            }
            public SellType GetSellTypeinfo(string SellTypeName_)
            {
                DataTable t = DB.GetData("select "
                 + SellTypeTable.SellTypeID + ","
                + SellTypeTable.SellTypeName
                + " from  "
                + SellTypeTable.TableName
                + " where "
            + SellTypeTable.SellTypeName + "='" + SellTypeName_ + "'");
                if (t.Rows.Count == 0) return null;
                else return new SellType(Convert.ToUInt32(t.Rows[0][0].ToString()), t.Rows[0][1].ToString());
            }
            public bool IsSellTypeExists(string SellType_name)
            {
                try
                {
                    DataTable t = DB.GetData("select * from  "
                  + SellTypeTable.TableName
                  + " where "
                  + SellTypeTable.SellTypeName + "='" + SellType_name + "'");
                    if (t.Rows.Count > 0) return true;
                    else return false;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("حدث خطأ اثناء الاتصال بقاعدة البيانات", sqlEx);
                }
            }

            public bool AddSellType(string SellTypename)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID)))
                        throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (IsSellTypeExists(SellTypename))
                    {
                        MessageBox.Show("البيانات موجودة مسبقا", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    DB.ExecuteSQLCommand("insert into  "
                        + SellTypeTable.TableName
                        + " ( "
                        + SellTypeTable.SellTypeName
                        + ")values( "
                        + "'" + SellTypename + "'"
                        + ")"
                        );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.INSERT
                       , DatabaseInterface.Log.Log_Target.Trade_SellTypes
                        , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_SellTypes 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdateSellType(uint SellTypeid, string newSellTypename)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID)))
                        throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DB.ExecuteSQLCommand("update   "
                        + SellTypeTable.TableName
                        + " set "
                        + SellTypeTable.SellTypeName + "='" + newSellTypename + "'"
                        + " where "
                        + SellTypeTable.SellTypeID + "=" + SellTypeid
                        );
                    DB.AddLog(
             DatabaseInterface.Log.LogType.UPDATE 
             , DatabaseInterface.Log.Log_Target.Trade_SellTypes
              , ""
              , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_SellTypes
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteSellType(uint SellTypeid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID)))
                        throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DB.ExecuteSQLCommand("delete from    "
                        + SellTypeTable.TableName
                        + " where "
                        + SellTypeTable.SellTypeID + "=" + SellTypeid
                        );
                    DB.AddLog(
             DatabaseInterface.Log.LogType.DELETE 
             , DatabaseInterface.Log.Log_Target.Trade_SellTypes
              , ""
              , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_SellTypes
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<SellType> GetSellTypeList()
            {
                try
                {
                    List<SellType> list = new List<SellType>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select * from "
                        + SellTypeTable.TableName
                       );


                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        SellType m = new SellType(Convert.ToUInt32(t.Rows[i][0]), t.Rows[i][1].ToString());
                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب قائمة علاقات العناصر:", ee.Message);
                    return null;
                }

            }
        }
        public class TradeStateSQL
        {
            DatabaseInterface DB;
            private static class TradeStateTable
            {
                public const string TableName = "Trade_TradeState";
                public const string TradeStateID = "TradeStateID";
                public const string TradeStateName = "TradeStateName";


            }
            public TradeStateSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public TradeState GetTradeStateBYID(uint tradestateid)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select * from   "
                + TradeStateTable.TableName
                + " where "
                + TradeStateTable.TradeStateID  + "=" + tradestateid
                  );
                if (t.Rows.Count == 1)
                {
                    string tradestatename = t.Rows[0][1].ToString();


                    return new TradeState (tradestateid , tradestatename);

                }
                else
                    return null;
            }
            public bool AddTradeState(string tradestatename)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) 
                        || DB.IS_Belong_To_Buy_Group(DB.__User.UserID)
                        ))
                        throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DB.ExecuteSQLCommand(" insert into "
                    + TradeStateTable .TableName
                    + "("
                    + TradeStateTable.TradeStateName
                    + ")"
                    + "values"
                    + "("
                    + "'" + tradestatename   + "'"

                    + ")"
                    );
                    DB.AddLog(
              DatabaseInterface.Log.LogType.INSERT
              , DatabaseInterface.Log.Log_Target.Item_TradeState
               , ""
               , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Item_TradeState
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdateTradeState(uint tradestateidid, string newname)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID)
                         || DB.IS_Belong_To_Buy_Group(DB.__User.UserID)
                         ))
                        throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DB.ExecuteSQLCommand("update  "
                    + TradeStateTable .TableName
                    + " set "
                       + TradeStateTable.TradeStateName  + "='" + newname  + "'"
                    + " where "
                    + TradeStateTable.TradeStateID + "=" + tradestateidid
                    );
                    
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.UPDATE 
                          , DatabaseInterface.Log.Log_Target.Item_TradeState
                           , ""
                           , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Item_TradeState
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteTradestate(uint tradestateidid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID)
                         || DB.IS_Belong_To_Buy_Group(DB.__User.UserID)
                         ))
                        throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DB.ExecuteSQLCommand("delete from   "
                    + TradeStateTable.TableName
                    + " where "
                    + TradeStateTable.TradeStateID + "=" + tradestateidid
                     );
                    DB.AddLog(
              DatabaseInterface.Log.LogType.DELETE 
              , DatabaseInterface.Log.Log_Target.Item_TradeState
               , ""
               , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Item_TradeState
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<TradeState > GetTradeStateList()
            {
                try
                {
                    List<TradeState> tradestatelist = new List<TradeState>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select * from   "
                    + TradeStateTable.TableName
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint ownerrid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string ownername = t.Rows[i][1].ToString();


                        tradestatelist.Add(new TradeState(ownerrid, ownername));
                    }
                    return tradestatelist;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetTradeStateList:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
        }
        public class ItemINSQL
        {
            DatabaseInterface DB;
            internal  static class ItemINTable
            {
                public const string TableName = "Trade_ItemIN";
                public const string ItemINID = "ItemINID";
                public const string IN_Date = "IN_Date";
                public const string OperationType = "OperationType";
                public const string OperationID = "OperationID";
                public const string ItemID = "ItemID";
                public const string TradeStateID = "TradeStateID";
                public const string Amount = "Amount";
                public const string ConsumeUnitID = "ConsumeUnitID";
                public const string Cost = "Cost";
                public const string Notes = "Notes";

            }
            //private static class INCost_Table
            //{
            //    public const string TableName = "[dbo].[Trade_ItemIN_GetCost]";
            //    public const string Value = "Value_";
            //    public const string CurrencyID = "CurrencyID";
            //    public const string ExchangeRate = "ExchangeRate";
            //}
            public ItemINSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public ItemIN GetItemININFO_BYID(uint iteminid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + ItemINTable.OperationType + ","
                     + ItemINTable.OperationID + ","
                     + ItemINTable.ItemID + ","
                     + ItemINTable.TradeStateID + ","
                     + ItemINTable.Amount + ","
                     + ItemINTable.ConsumeUnitID + ","
                     //+ ItemINTable.Cost  + ","
                     + ItemINTable.Notes + ","
                      + ItemINTable.IN_Date 

                     + " from   "
                    + ItemINTable.TableName
                    + " where "
                    + ItemINTable.ItemINID + "=" + iteminid
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint operationtype = Convert.ToUInt32(t.Rows[0][0].ToString());
                        uint operationid = Convert.ToUInt32(t.Rows[0][1].ToString());
                        Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][2].ToString()));
                        TradeState tradestate = new TradeStateSQL(DB).GetTradeStateBYID(Convert.ToUInt32(t.Rows[0][3].ToString()));
                        double amount = Convert.ToDouble(t.Rows[0][4].ToString());
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[0][5].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, item.DefaultConsumeUnit, item, 1);
                        }
                        //double buyprice = Convert.ToDouble(t.Rows[0][6].ToString());
                        string notes = t.Rows[0][6].ToString();

                        INCost INCost_ = GetItemINCost(iteminid);
                        DateTime in_date = Convert.ToDateTime(t.Rows[0][7].ToString());
                        return new ItemIN(iteminid,in_date , new Operation(operationtype, operationid), item, tradestate, amount, consumeunit, INCost_, notes);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetItemININFO_BYID" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            public ItemIN  GetItemININFO_BY_RowID(uint rowid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + ItemINTable.OperationType + ","
                     + ItemINTable.OperationID  + ","
                     + ItemINTable.ItemID + ","
                     + ItemINTable.TradeStateID + ","
                     + ItemINTable.Amount + ","
                     + ItemINTable.ConsumeUnitID+","
                     + ItemINTable.Notes+","
                      + ItemINTable.ItemINID + ","
                        + ItemINTable.IN_Date  
                     + " from   "
                    + ItemINTable.TableName
                    + " where "
                    + DatabaseInterface.ROWID_COLUMN + "=" + rowid 
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint operationtype = Convert.ToUInt32(t.Rows [0][0].ToString ());
                        uint operationid = Convert.ToUInt32(t.Rows[0][1].ToString());
                        Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][2].ToString()));
                        TradeState tradestate = new TradeStateSQL(DB).GetTradeStateBYID(Convert.ToUInt32(t.Rows[0][3].ToString()));
                        double amount = Convert.ToDouble(t.Rows[0][4].ToString());
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[0][5].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, item.DefaultConsumeUnit, item, 1);
                        }
                        //double buyprice = Convert.ToDouble(t.Rows[0][6].ToString());
                        string notes = t.Rows[0][6].ToString();
                        uint iteminid = Convert.ToUInt32(t.Rows[0][7].ToString());
                        DateTime in_date= Convert.ToDateTime (t.Rows[0][8].ToString());
                        INCost INCost_ = GetItemINCost(iteminid);
                        return new ItemIN (iteminid, in_date, new Operation ( operationtype ,operationid ), item, tradestate, amount, consumeunit, INCost_, notes);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetItemININFO_BYID" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return null;
                }
                
            }
            public ItemIN AddItemIN(Operation Operation_, Item item,TradeState tradestate,double  amount,ConsumeUnit consumeunit,double?  cost,string notes)
            {
                try
                {
                    switch (Operation_.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE :
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                           
                            break;
                        case Operation.DISASSEMBLAGE :
                            DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(Operation_.OperationID);
                            List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(DisAssemblabgeOPR_._Operation);
                            if (itemoutlist.Count ==0) throw new Exception("يجب أولا ضبط العنصر المفكك");
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group (DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                            break;
                    
                        default:
                            throw new Exception("عملية غير صحيحة");
                           

                    }
                    List<ItemIN> list = new ItemINSQL(DB).GetItemINList(Operation_).Where(x => x._Item.ItemID == item.ItemID&& x._TradeState .TradeStateID==tradestate.TradeStateID ).ToList();
                    if (list.Count > 0) throw new Exception ("العنصر ذو الرقم  :"+item.ItemID+ ",حالته:" + tradestate.TradeStateName+"  موجود مسبقا.. ");
                    DataTable t= DB.GetData (" insert into "
                    + ItemINTable.TableName
                    + "("
                    + ItemINTable.OperationType   +","
                     + ItemINTable.OperationID  + ","
                    + ItemINTable.ItemID + ","
                    + ItemINTable.TradeStateID + ","
                    + ItemINTable.Amount  + ","
                    + ItemINTable.ConsumeUnitID +","
                    + ItemINTable.Cost  + ","
                    + ItemINTable.Notes + ","
                     + ItemINTable.IN_Date
                    + ")"
                    + "values"
                    + "("
                    + Operation_.OperationType 
                    + ","
                     + Operation_.OperationID
                    + ","
                    + item.ItemID 
                     + ","
                    + tradestate .TradeStateID
                     + ","
                      + amount 
                     + ","
                     +(consumeunit == null ? "null" : consumeunit.ConsumeUnitID.ToString())
                     + ","
                     + (cost  == null ? "null" : cost .ToString())
                     + ","
                     + "'"+notes +"'"
                     + ","
                     + "'" + DateTime .Now .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ")"
                     + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());



                    DB.AddLog(
              DatabaseInterface.Log.LogType.INSERT
              , DatabaseInterface.Log.Log_Target.Trade_ItemIN 
               , ""
               , true, "");
                    return GetItemININFO_BY_RowID(rowid );
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_ItemIN 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("AddItemIN:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
            }
            public bool UpdateItemIN(uint iteminid,Item item,TradeState tradestate, double  amount,ConsumeUnit consumeunit, double? cost,  string notes)
            {
                try
                {
                    ItemIN itemin =GetItemININFO_BYID(iteminid);
                    double new_factor= consumeunit == null ? 1 : consumeunit.Factor;
                    double newamount = amount * itemin._ConsumeUnit.Factor / new_factor;
                    if (itemin == null) throw new Exception(" ItemIN NULL");
                    double availableamount = new AvailableItemSQL(DB).Get_AvailabeAmount_by_ItemIN(itemin);
                    if (newamount < availableamount) throw new Exception("الكمية الجديدة اقل من الكمية المتوفرة(الغير مستخدمة في التخزين او تم اخراجها), يرجى التصحيح");
                        //    if (new ItemOUTSQL(DB).GetItemIN_ItemOUTList("UpdateItemIN",itemin).Count > 0 || new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(itemin).Count > 0)
                        //        throw new Exception("يجب الغاء العناصر المخرجة و إلغاء التخزين قبل تعديل العنصر");
                        switch (itemin._Operation.OperationType)
                        {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;

                        default:
                            throw new Exception("عملية غير صحيحة");


                    }
                    List<ItemIN> list = new ItemINSQL(DB).GetItemINList(itemin._Operation ).Where(x =>x.ItemINID!=iteminid&& x._Item.ItemID == item.ItemID && x._TradeState.TradeStateID == tradestate.TradeStateID).ToList();
                    if (list.Count > 0) throw new Exception("العنصر ذو الرقم  :" + item.ItemID + ",حالته:" + tradestate.TradeStateName + "  موجود مسبقا.. ");
                    if(item .ItemID !=itemin ._Item.ItemID )
                    {
                        List<ItemINSellPrice> ItemINSellPriceList = new ItemINSellPriceSql(DB).GetItemINPrices(itemin);
                        if (ItemINSellPriceList.Count > 0)
                        {
                            DialogResult ff = MessageBox.Show("لاكمال عملية التعديل يجب اعادة تصفير جميع الاسعار المضبوطة هل تريد المتابعة؟","",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning );
                            if (ff == DialogResult.OK) new ItemINSellPriceSql(DB).ClearINSellPrices(itemin);
                            else return false;
                        }

                    }

                    DB.ExecuteSQLCommand("update  "
                    + ItemINTable.TableName
                    + " set "
                     + ItemINTable.ItemID + "=" + item.ItemID 
                    + ","
                     + ItemINTable.TradeStateID  + "=" + tradestate .TradeStateID 
                    + ","
                    + ItemINTable.Amount  + "=" +amount 
                    + ","
                    + ItemINTable.ConsumeUnitID  + "=" + (consumeunit == null ? "null" : consumeunit.ConsumeUnitID.ToString())
                    + ","
                    + ItemINTable.Cost   + "="  +(cost == null ? "null" : cost.ToString())
                    + ","
                    + ItemINTable.Notes   + "='" + notes  +"'"
                    + " where "
                    + ItemINTable.ItemINID +"="+ iteminid 
                    );
                    DB.AddLog(
            DatabaseInterface.Log.LogType.UPDATE 
            , DatabaseInterface.Log.Log_Target.Trade_ItemIN
             , ""
             , true, "");
                    return true ;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemIN
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("UpdateItemIN:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool DeleteItemIN(uint iteminid)
            {
                try
                {
                    ItemIN itemin = GetItemININFO_BYID(iteminid);
                    switch (itemin._Operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;

                        default:
                            throw new Exception("عملية غير صحيحة");


                    }
                    List<ItemOUT> itemoutlist =new ItemOUTSQL (DB). GetItemIN_ItemOUTList("DeleteItemIN",itemin);
                    if (itemoutlist .Count >0)
                    {
                        throw new Exception("لا يمكن حذف عملية الادخال , يجب اولا الغاء عمليات الاخراج اللتي مصدرها عملية الادخال"+iteminid );
                    }
                    List <TradeItemStore > placeslist= new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(itemin);
                    if (placeslist.Count > 0)
                    {
                        throw new Exception("يجب اولا الغاء  عمليات التخزين التابعة لعملية الادخال" + iteminid);
                    }
                    DB.ExecuteSQLCommand("delete from   "
                    + ItemINTable.TableName
                    + " where "
                    + ItemINTable.ItemINID + "=" + iteminid
                    );
                    DB.AddLog(
           DatabaseInterface.Log.LogType.DELETE 
           , DatabaseInterface.Log.Log_Target.Trade_ItemIN
            , ""
            , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemIN
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("DeleteItemIN:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Does_Operation_Has_ItemsIN(uint oprtype,uint oprid)
            {
                try
                {
                    DataTable t= DB.GetData ("select * from   "
                    + ItemINTable.TableName
                    + " where "
                    + ItemINTable.OperationType + "=" + oprtype
                    + " and "
                    + ItemINTable.OperationID + "=" + oprid 
                    );
                    if (t.Rows.Count > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("DeleteItemINListForOperation" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<ItemIN_ItemOUTReport> GetItemIN_ItemOUTReport_List(Operation operation)
            {
                List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportList = new List<ItemIN_ItemOUTReport>();
                try
                {
                    List<ItemIN> ItemINList = GetItemINList(operation);
                    ItemOUTSQL ItemOUTSQL_ = new ItemOUTSQL(DB);
                    for (int i = 0; i < ItemINList.Count; i++)
                    {
                        List<ItemOUT> ItemOUTList = ItemOUTSQL_.GetItemIN_ItemOUTList("GetItemIN_ItemOUTReport_List",ItemINList[i]);
                        ItemIN_ItemOUTReportList.Add(new ItemIN_ItemOUTReport(ItemINList[i], ItemOUTList));
                    }
                }
                catch(Exception ee)
                {
                    MessageBox.Show("ItemIN_ItemOUTReportList:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return ItemIN_ItemOUTReportList;
            }
            private INCost GetItemINCost(uint iteminid)
            {
                try
                {         DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + ItemINTable.OperationType + ","
                     + ItemINTable.OperationID + ","
                     + ItemINTable.Cost + ","
                     + ItemINTable.Amount  
                     + " from   "
                    + ItemINTable.TableName
                    + " where "
                    + ItemINTable.ItemINID + "=" + iteminid
                      );
                    if (t.Rows.Count != 1) throw new Exception("العنصر المدخل غير موجود");
                    Operation _Operation = new Operation(Convert.ToUInt32(t.Rows[0][ItemINTable.OperationType]), Convert.ToUInt32(t.Rows[0][ItemINTable.OperationID]));
                    Currency referense_curruncy = new CurrencySQL(DB).GetReferenceCurrency();

                    switch (_Operation.OperationType)
                    {
                        case Operation.BILL_BUY :
                          
                                double value = Convert.ToDouble(t.Rows[0][ItemINTable.Cost].ToString());
                                BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(_Operation.OperationID);
                                return new INCost(value, billbuy._Currency , billbuy.ExchangeRate);

                        case Operation.ASSEMBLAGE :
                            //AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_Operation.OperationID);
                            List<ItemOUT> assemblage_itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(_Operation);
                            double assemblage_itemincost = 0;
                            for (int i=0;i<assemblage_itemoutlist.Count;i++)
                            {

                                INCost incost = GetItemINCost(assemblage_itemoutlist[i]._ItemIN.ItemINID);

                                assemblage_itemincost += (incost.Value / incost.ExchangeRate)*assemblage_itemoutlist[i].Amount;
                            }
                            double amount = Convert.ToDouble(t.Rows[0][ItemINTable.Amount].ToString());
                            
                            return new INCost(assemblage_itemincost/amount , referense_curruncy, 1);

                        case Operation.DISASSEMBLAGE :
                            DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_Operation.OperationID);
                            return new INCost(Convert.ToDouble(t.Rows[0][2].ToString()), DisAssemblabgeOPR_._ItemOUT._OUTValue._Currency, DisAssemblabgeOPR_._ItemOUT._OUTValue.ExchangeRate );

                        default:
                            throw new Exception("عملية ادخال عنصر خاطئة");
                    }
                   
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemINCost" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                     return new INCost(-1, new CurrencySQL (DB).GetReferenceCurrency (), 1); ;
                }
            }
            public List<ItemIN  > GetItemINList(Operation operation)
            {
                try
                {
                    List<ItemIN> ItemINlist = new List<ItemIN>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + ItemINTable.ItemINID   + ","
                    + ItemINTable.ItemID + ","
                    + ItemINTable.TradeStateID + ","
                    + ItemINTable.Amount + ","
                    + ItemINTable.ConsumeUnitID + ","
                    //+ ItemINTable.Cost  + ","
                    + ItemINTable.Notes + ","
                    + ItemINTable.IN_Date 
                    + " from   "
                    + ItemINTable.TableName
                    +" where "
                    + ItemINTable.OperationType + "=" + operation.OperationType 
                    + " and "
                    + ItemINTable.OperationID  + "=" + operation.OperationID 

                );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint iteminid  = Convert.ToUInt32(t.Rows[i][0].ToString());
                        Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][1].ToString()));
                        TradeState tradestate = new TradeStateSQL(DB).GetTradeStateBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        double  amount = Convert.ToDouble(t.Rows[i][3].ToString());
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[i][4].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, item.DefaultConsumeUnit, item, 1);
                        }
                        //double cost = Convert.ToDouble(t.Rows[i][5].ToString());
                         string notes = t.Rows[i][5].ToString();
                        DateTime in_date = Convert.ToDateTime(t.Rows[i][6].ToString());
                        INCost INCost_ = GetItemINCost(iteminid);
                        ItemINlist.Add(new ItemIN(iteminid, in_date, operation , item, tradestate, amount, consumeunit, INCost_, notes));

                    }
                    return ItemINlist;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemINList" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public List<ItemIN> Get_ALL_ItemIN_List()
            {
                try
                {
                    List<ItemIN> ItemINlist = new List<ItemIN>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + ItemINTable.ItemINID + ","
                    + ItemINTable.ItemID + ","
                    + ItemINTable.TradeStateID + ","
                    + ItemINTable.Amount + ","
                    + ItemINTable.ConsumeUnitID + ","
                    + ItemINTable.Notes + ","
                    + ItemINTable.OperationType + ","
                    + ItemINTable.OperationID + ","
                     + ItemINTable.IN_Date 
                    + " from   "
                    + ItemINTable.TableName


                );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint iteminid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][1].ToString()));
                        TradeState tradestate = new TradeStateSQL(DB).GetTradeStateBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        double amount = Convert.ToDouble(t.Rows[i][3].ToString());
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[i][4].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, item.DefaultConsumeUnit, item, 1);
                        }
                        //double cost = Convert.ToDouble(t.Rows[i][5].ToString());
                        string notes = t.Rows[i][5].ToString();
                        uint operationtype = Convert.ToUInt32(t.Rows[i][6].ToString());
                        uint operationid= Convert.ToUInt32(t.Rows[i][7].ToString());
                        Operation operation = new Operation(operationtype, operationid);
                        DateTime in_date = Convert.ToDateTime(t.Rows[i][8].ToString());

                        INCost INCost_ = GetItemINCost(iteminid);
                        ItemINlist.Add(new ItemIN(iteminid, in_date, operation, item, tradestate, amount, consumeunit, INCost_, notes));

                    }
                    return ItemINlist;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemINList" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public List<ItemIN_ItemOUTReport> GetItemINList_ForItem(Item  item)
            {
                try
                {
                    List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportlist = new List<ItemIN_ItemOUTReport>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + ItemINTable.ItemINID + ","
                     + ItemINTable.OperationType + ","
                     + ItemINTable.OperationID + ","
                    + ItemINTable.TradeStateID + ","
                    + ItemINTable.Amount + ","
                    + ItemINTable.ConsumeUnitID  + ","
                    + ItemINTable.Notes + ","
                    + ItemINTable.IN_Date 
                    + " from   "
                    + ItemINTable.TableName
                    + " where "
                    + ItemINTable.ItemID  + "=" + item.ItemID 


                );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint iteminid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        uint operationtype = Convert.ToUInt32(t.Rows[i][1].ToString());
                        uint operationid = Convert.ToUInt32(t.Rows[i][2].ToString());
                        Operation Operation_ = new Operation(operationtype, operationid);
                        TradeState tradestate = new TradeStateSQL(DB).GetTradeStateBYID(Convert.ToUInt32(t.Rows[i][3].ToString()));
                        double amount = Convert.ToDouble(t.Rows[i][4].ToString());
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[i][5].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, item.DefaultConsumeUnit, item, 1);
                        }
                        //double cost = Convert.ToDouble(t.Rows[i][5].ToString());
                        string notes = t.Rows[i][6].ToString();
                        DateTime in_date = Convert.ToDateTime(t.Rows[i][7].ToString());
                        INCost INCost_ = GetItemINCost(iteminid);

                        ItemIN itemin = new ItemIN(iteminid, in_date, Operation_, item, tradestate, amount, consumeunit, INCost_, notes);
                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("GetItemINList_ForItem:",itemin);
                        ItemIN_ItemOUTReportlist.Add(new ItemIN_ItemOUTReport(itemin ,itemoutlist ));

                    }
                    return ItemIN_ItemOUTReportlist;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemINList_ForItem" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            internal List<ItemIN_StoreReport> GetItemIN_StoreReportList(ItemIN itemIN_)
            {
                List<ItemIN_StoreReport> List = new List<ItemIN_StoreReport>();
                try
                {
                    AvailableItemSQL AvailableItemSQL_ = new AvailableItemSQL(DB);
                    List<TradeItemStore> TradeItemStoreList = new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(itemIN_);

                    double nonsotred = itemIN_.Amount  - TradeItemStoreList.Sum (x=>x.Amount *(itemIN_._ConsumeUnit.Factor /x._ConsumeUnit .Factor ));

                    if (nonsotred > 0)
                    {
                        List.Add(new ItemIN_StoreReport(itemIN_, null, itemIN_._ConsumeUnit, nonsotred, AvailableItemSQL_.Get_SpentAmount_by_Place(itemIN_, null)));
                    }
            
                
                    for (int i = 0; i < TradeItemStoreList.Count; i++)
                    {
                        List.Add(new ItemIN_StoreReport(itemIN_, TradeItemStoreList[i]._TradeStorePlace, TradeItemStoreList[i]._ConsumeUnit, TradeItemStoreList[i].Amount, AvailableItemSQL_.Get_SpentAmount_by_Place (itemIN_, TradeItemStoreList[i]._TradeStorePlace)));
                    }
                }
                catch(Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("ITEMINSQL-GetItemIN_StoreReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return List;
            }
        }
        public class ItemOUTSQL
        {
            DatabaseInterface DB;
            internal static class ItemOUTTable
            {
                public const string TableName = "Trade_ItemOUT";
                public const string ItemOUTID = "ItemOUTID";
                public const string OUT_Date = "OUT_Date";

                public const string OperationType = "OperationType";
                public const string OperationID = "OperationID";
                public const string ItemINID = "ItemINID";
                public const string PlaceID = "PlaceID";
                public const string Amount = "Amount";
                public const string ConsumeUnitID = "ConsumeUnitID";
                public const string Cost = "Cost";
                public const string Notes = "Notes";

            }
            //private static class OUTValue_Table
            //{
            //    public const string TableName = "[dbo].[Trade_ItemOUT_GetOutValue]";
            //    public const string Value = "Value_";
            //    public const string CurrencyID = "CurrencyID";
            //    public const string ExchangeRate = "ExchangeRate";
            //}
            public ItemOUTSQL(DatabaseInterface db)
            {
                DB = db;

            }
      
            private OUTValue  GetOUTValue(uint itemoutid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + ItemOUTTable.OperationType + ","
                     + ItemOUTTable.OperationID + ","
                     + ItemOUTTable.Cost + ","
                     + ItemOUTTable.ItemINID
                     + " from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.ItemOUTID + "=" + itemoutid 
                      );
                    if (t.Rows.Count == 0) throw new Exception("العنصر المخرج غير موجود");
                    Operation _Operation = new Operation(Convert.ToUInt32(t.Rows[0][0]), Convert.ToUInt32(t.Rows[0][1]));
                    Currency referense_curruncy = new CurrencySQL(DB).GetReferenceCurrency();
                    //double value =;

                    ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(Convert.ToUInt32(t.Rows[0][3]));
                    switch (_Operation.OperationType)
                    {
                        case Operation.BILL_SELL:

                            BillSell  billsell = new BillSellSQL(DB).GetBillSell_INFO_BYID(_Operation.OperationID);
                            return new OUTValue(Convert.ToDouble(t.Rows[0][2].ToString()), billsell._Currency, billsell.ExchangeRate);
                        case Operation.BILL_MAINTENANCE:

                            BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(_Operation.OperationID);
                            return new OUTValue(Convert.ToDouble(t.Rows[0][2].ToString()), BillMaintenance_._Currency, BillMaintenance_.ExchangeRate);
                        case Operation.REPAIROPR:
                            RepairOPR RepairOPR_ = new Maintenance.MaintenanceSQL.RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(_Operation.OperationID);
                            BillMaintenance BillMaintenance2_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(RepairOPR_._MaintenanceOPR);
                            return new OUTValue(Convert.ToDouble(t.Rows[0][2].ToString()), BillMaintenance2_._Currency, BillMaintenance2_.ExchangeRate);

                        case Operation.ASSEMBLAGE:
                            //AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_Operation.OperationID);
                            //List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(_Operation);
                            //double itemin_cost = 0;
                            //for (int i = 0; i < iteminlist.Count; i++)
                            //    itemin_cost += iteminlist[i]._INCost.Value / iteminlist[i]._INCost.ExchangeRate;
                            return new OUTValue (itemin._INCost.Value, itemin._INCost._Currency , itemin._INCost.ExchangeRate);

     
                        case Operation.DISASSEMBLAGE:
                            return new OUTValue(itemin._INCost.Value, itemin._INCost._Currency, itemin._INCost.ExchangeRate);
                        case Operation.RavageOPR:
                            return new OUTValue(0, itemin._INCost._Currency, itemin._INCost.ExchangeRate);

                        default:
                            throw new Exception("عملية اخراج عنصر خاطئة");
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetOUTValue" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return new OUTValue(-1, new CurrencySQL(DB).GetReferenceCurrency(), 1); ;
                }
                
            }
            public ItemOUT GetItemOUTINFO_BYID(uint itemoutid)
            {
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + ItemOUTTable.OperationType + ","
                     + ItemOUTTable.OperationID + ","
                     + ItemOUTTable.ItemINID + ","
                     + ItemOUTTable.PlaceID + ","
                     + ItemOUTTable.Amount + ","
                     + ItemOUTTable.ConsumeUnitID + ","
                     //+ ItemOUTTable.Cost + ","
                     + ItemOUTTable.Notes + ","
                      + ItemOUTTable.OUT_Date 
                     + " from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.ItemOUTID + "=" + itemoutid
                      );
                    if (t.Rows.Count == 1)
                    {

                        uint operationtype = Convert.ToUInt32(t.Rows[0][0].ToString());
                        uint operationid = Convert.ToUInt32(t.Rows[0][1].ToString());
                        ItemIN ItemIN_ = new ItemINSQL(DB).GetItemININFO_BYID(Convert.ToUInt32(t.Rows[0][2].ToString()));
                        TradeStorePlace Place;
                        try
                        {
                            Place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[0][3].ToString()));
                        }
                        catch
                        {
                            Place = null;
                        }
                        double amount = Convert.ToInt32(t.Rows[0][4].ToString());
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[0][5].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, ItemIN_._Item.DefaultConsumeUnit, ItemIN_._Item, 1);

                        }

                        //double Cost = Convert.ToDouble(t.Rows[0][6].ToString());
                        string notes = t.Rows[0][6].ToString();
                        DateTime out_date = Convert.ToDateTime(t.Rows[0][7].ToString());
                        OUTValue OUTValue_ = GetOUTValue(itemoutid);
                        return new ItemOUT(itemoutid, out_date, new Operation ( operationtype, operationid), ItemIN_, Place, amount, consumeunit, OUTValue_, notes);


                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemOUTINFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public ItemOUT GetItemOUTINFO_BY_RowID(uint rowid)
            {
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + ItemOUTTable.OperationType + ","
                     + ItemOUTTable.OperationID + ","
                     + ItemOUTTable.ItemINID + ","
                     + ItemOUTTable.PlaceID + ","
                     + ItemOUTTable.Amount + ","
                     + ItemOUTTable.ConsumeUnitID + ","
                     + ItemOUTTable.Notes + ","
                     + ItemOUTTable.ItemOUTID + ","
                     + ItemOUTTable.OUT_Date  
                     + " from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + DatabaseInterface .ROWID_COLUMN + "=" + rowid 
                      );
                    if (t.Rows.Count == 1)
                    {

                        uint operationtype = Convert.ToUInt32(t.Rows[0][0].ToString());
                        uint operationid = Convert.ToUInt32(t.Rows[0][1].ToString());
                        ItemIN ItemIN_ = new ItemINSQL(DB).GetItemININFO_BYID(Convert.ToUInt32(t.Rows[0][2].ToString()));
                        TradeStorePlace Place;
                        try
                        {
                            Place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[0][3].ToString()));
                        }
                        catch
                        {
                            Place = null;
                        }
                        double amount = Convert.ToInt32(t.Rows[0][4].ToString());
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[0][5].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, ItemIN_._Item.DefaultConsumeUnit, ItemIN_._Item, 1);

                        }

                        //double Cost = Convert.ToDouble(t.Rows[0][6].ToString());
                        string notes = t.Rows[0][6].ToString();
                        uint itemoutid = Convert.ToUInt32(t.Rows[0][7].ToString());
                        DateTime out_date= Convert.ToDateTime (t.Rows[0][8].ToString());
                        OUTValue OUTValue_ = GetOUTValue(itemoutid);
                        return new ItemOUT(itemoutid, out_date, new Operation(operationtype, operationid), ItemIN_, Place, amount, consumeunit, OUTValue_, notes);


                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemOUTINFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public ItemOUT AddItemOUT(Operation Operation_, ItemIN  itemin, TradeStorePlace Place, double amount, ConsumeUnit consumeunit, double? cost, string notes)
            {
                try
                {
                    switch (Operation_.OperationType)
                    {
                        case Operation.BILL_SELL:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_MAINTENANCE :
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.RavageOPR:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) )) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.REPAIROPR:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        default:
                            throw new Exception("عملية غير صحيحة");


                    }
                    double orginalfactor = (itemin._ConsumeUnit == null ? 1 : itemin._ConsumeUnit.Factor);
                    double consumefactor = (consumeunit == null ? 1 : consumeunit.Factor);
                    double available_amount=new AvailableItemSQL(DB).Get_AvailabeAmount_by_Place(itemin, Place);
                    if (available_amount < (amount * consumefactor / orginalfactor))
                        throw new Exception("الكمية المدخلة اقل من الكمية المتوفرة");

                    string ConsumeID_Str = "";
                    if (consumeunit == null)
                        ConsumeID_Str = "null";
                    else if (consumeunit.ConsumeUnitID == 0)
                        ConsumeID_Str = "null";
                    else ConsumeID_Str = consumeunit.ConsumeUnitID.ToString();
                    DataTable t = DB.GetData(" insert into "
                    + ItemOUTTable.TableName
                    + "("
                    + ItemOUTTable.OperationType + ","
                    + ItemOUTTable.OperationID + ","
                    + ItemOUTTable.ItemINID + ","
                     + ItemOUTTable.PlaceID + ","
                    + ItemOUTTable.Amount + ","
                    + ItemOUTTable.ConsumeUnitID + ","
                    + ItemOUTTable.Cost + ","
                    + ItemOUTTable.Notes + ","
                    + ItemOUTTable.OUT_Date 
                    + ")"
                    + "values"
                    + "("
                    + Operation_.OperationType 
                    + ","
                    + Operation_.OperationID 
                    + ","
                    + itemin.ItemINID
                    + ","
                    + (Place == null ? "null" : Place.PlaceID.ToString())
                    + ","
                    + amount
                    + ","
                    + ConsumeID_Str
                    + ","
                    + (cost  == null ? "null" : cost.ToString())
                    + ","
                    + "'" + notes + "'"
                     + ","
                     + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'"

                    + ")"
                    + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());


                    DB.AddLog(
                           DatabaseInterface.Log.LogType.INSERT 
                           , DatabaseInterface.Log.Log_Target.Trade_ItemOut 
                            , ""
                            , true, "");
                    return GetItemOUTINFO_BY_RowID(rowid );
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemOut 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("AddItemOUT:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
            }
            public bool UpdateItemOUT(uint itemoutid, ItemIN  itemin, TradeStorePlace Place, double amount, ConsumeUnit ConsumeUnit_, double? cost, string notes)
            {
                try
                {
                   
                    ItemOUT itemout = GetItemOUTINFO_BYID(itemoutid);
                    switch (itemout._Operation .OperationType)
                    {
                        case Operation.BILL_SELL:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_MAINTENANCE :
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.RavageOPR:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.REPAIROPR:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        default:
                            throw new Exception("عملية غير صحيحة");


                    }

                    double itemin_consumeunit_factor = 1, itemout_consumeunit_factor = 1;
                    if (itemin._ConsumeUnit != null)
                        itemin_consumeunit_factor = itemin._ConsumeUnit.Factor;
                    if (ConsumeUnit_ != null)
                        itemout_consumeunit_factor = ConsumeUnit_.Factor;
                    double available_amount = new AvailableItemSQL(DB).Get_AvailabeAmount_by_Place(itemin, Place);
                    if (available_amount < (amount * itemin_consumeunit_factor / itemout_consumeunit_factor))
                        throw new Exception("الكمية المدخلة اقل من الكمية المتوفرة");

                    string ConsumeID_Str = "";
                    if (ConsumeUnit_ == null)
                        ConsumeID_Str = "null";
                    else if (ConsumeUnit_.ConsumeUnitID == 0)
                        ConsumeID_Str = "null";
                    else ConsumeID_Str = ConsumeUnit_.ConsumeUnitID.ToString();

                    DB.ExecuteSQLCommand("update  "
                    + ItemOUTTable.TableName
                    + " set "
                    + ItemOUTTable.ItemINID + "=" + itemin.ItemINID
                    + ","
                    + ItemOUTTable.PlaceID + "=" + (Place == null ? "null" : Place.PlaceID.ToString())
                    + ","
                    + ItemOUTTable.Amount + "=" + amount
                    + ","
                    + ItemOUTTable.ConsumeUnitID + "=" + ConsumeID_Str
                    + ","
                    + ItemOUTTable.Cost + "=" + (cost == null ? "null" : cost.ToString())
                    + ","
                    + ItemOUTTable.Notes + "='" + notes + "'"
                    + " where "
                    + ItemOUTTable.ItemOUTID + "=" + itemoutid
                    );
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.UPDATE 
                          , DatabaseInterface.Log.Log_Target.Trade_ItemOut
                           , ""
                           , true, "");
                    return true ;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemOut
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("UpdateItemOUT:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool DeleteItemOUT(uint itemoutid)
            {
                try
                {
                    ItemOUT itemout = GetItemOUTINFO_BYID(itemoutid);
                    switch (itemout._Operation.OperationType)
                    {
                        case Operation.BILL_SELL:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.BILL_MAINTENANCE :
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.RavageOPR:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.REPAIROPR:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        default:
                            throw new Exception("عملية غير صحيحة");


                    }
                    DB.ExecuteSQLCommand("delete from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.ItemOUTID + "=" + itemoutid
                    );
                    DB.AddLog(
      DatabaseInterface.Log.LogType.DELETE 
      , DatabaseInterface.Log.Log_Target.Trade_ItemOut
       , ""
       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemOut
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("DeleteItemOUT:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Does_Operation_Has_ItemsOUT(uint oprtype, uint oprid)
            {
                try
                {
                    DataTable t = DB.GetData("select * from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.OperationType + "=" + oprtype
                    + " and "
                    + ItemOUTTable.OperationID + "=" + oprid
                    );
                    if (t.Rows.Count > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("DeleteItemINListForOperation" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<ItemOUT> GetItemOUTList(Operation operation)
            {
                List<ItemOUT> ItemOUTList = new List<ItemOUT>();
                try
                {
                

                    DataTable t = new DataTable();
             
                    t = DB.GetData("select "
                     + ItemOUTTable.ItemOUTID + ","
                     + ItemOUTTable.ItemINID + ","
                     + ItemOUTTable.PlaceID + ","
                     + ItemOUTTable.Amount + ","
                     + ItemOUTTable.ConsumeUnitID + ","
                     //+ ItemOUTTable.Cost + ","
                     + ItemOUTTable.Notes + ","
                       + ItemOUTTable.OUT_Date 
                     + " from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.OperationType + "=" + operation.OperationType 
                    + " and "
                    + ItemOUTTable.OperationID + "=" + operation.OperationID 
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint itemoutid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(Convert.ToUInt32(t.Rows[i][1].ToString()));
                        TradeStorePlace Place;
                        try
                        {
                            Place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        }
                        catch
                        {
                            Place = null;
                        }

                        double amount = Convert.ToDouble(t.Rows[i][3].ToString());

                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[i][4].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, itemin._Item.DefaultConsumeUnit, itemin._Item, 1);

                        }

                        //double cost = Convert.ToDouble(t.Rows[i][5].ToString());
                        string notes = t.Rows[i][5].ToString();
                        DateTime out_date = Convert.ToDateTime(t.Rows[i][6].ToString());
                        OUTValue OUTValue_ = GetOUTValue(itemoutid);
                        ItemOUTList.Add(new ItemOUT(itemoutid, out_date, operation, itemin, Place, amount, consumeunit, OUTValue_ , notes));

                    }
                 
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemOUTList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                  
                }
                return ItemOUTList;
            }

            public List<Money_Currency> GetItemOUTList_AS_Money_Currency(Operation operation)
            {
                try
                {
                    List<Money_Currency> ItemOUTList = new List<Money_Currency>();

                    DataTable t = new DataTable();

                    t = DB.GetData("select "
                     + ItemOUTTable.ItemOUTID
                     + " from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.OperationType + "=" + operation.OperationType
                    + " and "
                    + ItemOUTTable.OperationID + "=" + operation.OperationID
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint itemoutid = Convert.ToUInt32(t.Rows [i][0]);
                        OUTValue OUTValue_ = GetOUTValue(itemoutid);
                        ItemOUTList.Add(new Money_Currency (OUTValue_._Currency ,OUTValue_.Value,OUTValue_.ExchangeRate) );

                    }
                    return ItemOUTList;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemOUTList" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }

            public int GetItemsOUT_Count(Operation operation)
            {
                try
                {
                    DataTable t = new DataTable();

                    t = DB.GetData("select  count(*) from "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.OperationType + "=" + operation.OperationType
                    + " and "
                    + ItemOUTTable.OperationID + "=" + operation.OperationID
                      );
                    return Convert.ToInt32(t.Rows[0][0].ToString());
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemsOUT_Count" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }

            }
            public List<ItemOUT> GetItemIN_ItemOUTList(string Source, ItemIN itemin)
            {
                List<ItemOUT> ItemOUTList = new List<ItemOUT>();
                try
                {

                    DataTable t  = DB.GetData("select "
                     + ItemOUTTable.ItemOUTID + ","
                     + ItemOUTTable.OperationType + ","
                      + ItemOUTTable.OperationID + ","
                     + ItemOUTTable.PlaceID + ","
                     + ItemOUTTable.Amount + ","
                     + ItemOUTTable.ConsumeUnitID + ","
                     //+ ItemOUTTable.Cost + ","
                     + ItemOUTTable.Notes + ","
                     + ItemOUTTable.OUT_Date 
                     + " from   "
                    + ItemOUTTable.TableName
                    + " where "
                    + ItemOUTTable.ItemINID + "=" + itemin.ItemINID
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint itemoutid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        uint operationtype = Convert.ToUInt32(t.Rows[i][1].ToString());
                        uint operationid = Convert.ToUInt32(t.Rows[i][2].ToString());

                        TradeStorePlace Place;
                        try
                        {
                            Place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[i][3].ToString()));
                        }
                        catch
                        {
                            Place = null;
                        }

                        double amount = Convert.ToDouble(t.Rows[i][4].ToString());

                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[i][5].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, itemin._Item.DefaultConsumeUnit, itemin._Item, 1);

                        }

                        //double cost = Convert.ToDouble(t.Rows[i][6].ToString());
                        string notes = t.Rows[i][6].ToString();
                        DateTime out_date = Convert.ToDateTime(t.Rows[i][7].ToString());
                        OUTValue OUTValue_ = GetOUTValue(itemoutid);
                        ItemOUTList.Add(new ItemOUT(itemoutid, out_date, new Operation ( operationtype, operationid), itemin, Place, amount, consumeunit, OUTValue_ , notes));

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemIN_ItemOUTList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return ItemOUTList;

            }

            internal List <ItemOUT> GetItemOUTList_ForItem(Item Item_)
            {
                List<ItemOUT> ItemOUTList = new List<ItemOUT>();
                try
                {
                    DataTable t = DB.GetData("select "
                    + ItemOUTTable.ItemOUTID + ","
                    + ItemOUTTable.OperationType + ","
                     + ItemOUTTable.OperationID + ","
                    + ItemOUTTable.PlaceID + ","
                    + ItemOUTTable.Amount + ","
                    + ItemOUTTable.ConsumeUnitID + ","
                    //+ ItemOUTTable.Cost + ","
                    + ItemOUTTable.Notes + ","
                    + ItemOUTTable.OUT_Date + ","
                     + ItemOUTTable.ItemINID

                    + " from   "
                   + ItemOUTTable.TableName
                   + " where "
                   + ItemOUTTable.ItemINID + " in"
                   + "(SELECT "
                   + ItemINSQL.ItemINTable.ItemINID
                   + " from "
                  + ItemINSQL.ItemINTable.TableName
                    + " where "
                   + ItemINSQL.ItemINTable.ItemID + " = " + Item_.ItemID + ")"
                     );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint itemoutid = Convert.ToUInt32(t.Rows[i][ItemOUTTable.ItemOUTID].ToString());
                        uint operationtype = Convert.ToUInt32(t.Rows[i][ItemOUTTable.OperationType].ToString());
                        uint operationid = Convert.ToUInt32(t.Rows[i][ItemOUTTable.OperationID].ToString());

                        TradeStorePlace Place;
                        try
                        {
                            Place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[i][ItemOUTTable.PlaceID].ToString()));
                        }
                        catch
                        {
                            Place = null;
                        }

                        double amount = Convert.ToDouble(t.Rows[i][ItemOUTTable.Amount].ToString());

                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunitid = Convert.ToUInt32(t.Rows[i][ItemOUTTable.ConsumeUnitID].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunitid);

                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit(0, Item_.DefaultConsumeUnit, Item_, 1);

                        }

                        //double cost = Convert.ToDouble(t.Rows[i][6].ToString());
                        string notes = t.Rows[i][ItemOUTTable.Notes].ToString();
                        DateTime out_date = Convert.ToDateTime(t.Rows[i][ItemOUTTable.OUT_Date].ToString());
                        OUTValue OUTValue_ = GetOUTValue(itemoutid);
                        ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(Convert.ToUInt32(t.Rows[i][ItemOUTTable.ItemINID].ToString()));
                        ItemOUTList.Add(new ItemOUT(itemoutid, out_date, new Operation(operationtype, operationid), itemin, Place, amount, consumeunit, OUTValue_, notes));
                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemOUTList_ForItem:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return ItemOUTList;
            }
        }
        public class ItemINSellPriceSql
        {
            public static class BuyOPRSellPriceTable
            {
                public const string TableName = "Trade_ItemIN_SellPrices";
                public const string SellPriceID = "SellPriceID";
                public const string ItemINID = "ItemINID";
                public const string ConsumeUnitID = "ConsumeUnitID";
                public const string SellTypeID = "SellTypeID";
                public const string Price = "Price";
            }
            DatabaseInterface DB;
            public ItemINSellPriceSql(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public bool IsPriceSet(ItemIN ItemIN_,  ConsumeUnit ConsumeUnit_, SellType SellType_)
            {
                try
                {
                    string cid_string = " is null";
                    if (ConsumeUnit_ != null)
                        if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = "=" + ConsumeUnit_.ConsumeUnitID.ToString();

                    DataTable t = DB.GetData("select * from   "
                        + BuyOPRSellPriceTable.TableName
                        + " where "
                       + BuyOPRSellPriceTable.ItemINID + "=" + ItemIN_.ItemINID   + " and "
                        + BuyOPRSellPriceTable.ConsumeUnitID + cid_string + " and "
                        + BuyOPRSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID

                        );
                    if (t.Rows.Count > 0)
                        return true;
                    else return false;

                }
                catch (Exception ee)
                {
                    throw new Exception("IsPriceSet :" + ee.Message);

                }

            }
            public bool SetItemINPrice(ItemIN ItemIN_,  ConsumeUnit ConsumeUnit_, SellType SellType_, double price)
            {
                try
                {
                    switch (ItemIN_._Operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;

                        default:
                            throw new Exception("عملية غير صحيحة");


                    }
                  bool Belong=  new ConsumeUnitSql(DB).IsConsumeUnitBelongToItem(ItemIN_._Item, ConsumeUnit_);
                    if (!Belong) throw new Exception("وحدة التوزيع لا تتبع للعنصر");
                    bool is_price_set = IsPriceSet(ItemIN_, ConsumeUnit_, SellType_);

                    if (!is_price_set)
                    {
                        string cid_string = "  null";
                        if (ConsumeUnit_ != null)
                            if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = ConsumeUnit_.ConsumeUnitID.ToString();
                        try
                        {


                            DB.ExecuteSQLCommand("insert into  "
                                + BuyOPRSellPriceTable.TableName
                                + " ( "
                                + BuyOPRSellPriceTable.ItemINID + ","
                                 + BuyOPRSellPriceTable.ConsumeUnitID + ","
                                  + BuyOPRSellPriceTable.SellTypeID + ","
                                + BuyOPRSellPriceTable.Price
                                + ")values( "
                                + ItemIN_.ItemINID + ","
                                + cid_string + ","
                                + SellType_.SellTypeID + ","
                                + price
                                + ")"
                                );
                            DB.AddLog(
                          DatabaseInterface.Log.LogType.INSERT
                          , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                           , ""
                           , true, "");
                            return true;
                        }
                        catch (Exception ee)
                        {
                            DB.AddLog(
                                    DatabaseInterface.Log.LogType.INSERT
                                    , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                                    , ""
                                  , false, ee.Message);
                            throw new Exception(ee.Message);
                            
                        }
                    }
                    else
                    {

                        string cid_string = " is null";
                        if (ConsumeUnit_ != null)
                            if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = "=" + ConsumeUnit_.ConsumeUnitID.ToString();
                        try
                        {
                            DB.ExecuteSQLCommand("update  "
                                + BuyOPRSellPriceTable.TableName
                                + " set "
                                + BuyOPRSellPriceTable.Price + "=" + price
                                + " where "
                                + BuyOPRSellPriceTable.ItemINID + "=" + ItemIN_.ItemINID
                                + " and "
                                 + BuyOPRSellPriceTable.ConsumeUnitID + cid_string
                                 + " and "
                                  + BuyOPRSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID
                                );
                            DB.AddLog(
                        DatabaseInterface.Log.LogType.UPDATE
                        , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                         , ""
                         , true, "");
                            return true;
                        }
                        catch (Exception ee)
                        {
                            DB.AddLog(
                                    DatabaseInterface.Log.LogType.UPDATE
                                    , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                                    , ""
                                  , false, ee.Message);
                            throw new Exception(ee.Message);
                           
                        }
                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show("SetItemINPrice:"+ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
              

            }

            public bool UNSetBuyOPRPrice(ItemIN ItemIN_, ConsumeUnit ConsumeUnit_, SellType SellType_)
            {
                string cid_string = " is null";
                if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = "=" + ConsumeUnit_.ConsumeUnitID.ToString();
                try
                {
                    switch (ItemIN_._Operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;

                        default:
                            throw new Exception("عملية غير صحيحة");


                    }

                    DB.ExecuteSQLCommand("delete from   "
                        + BuyOPRSellPriceTable.TableName
                        + " where "
                       + BuyOPRSellPriceTable.ItemINID + "=" + ItemIN_.ItemINID + " and "
                        + BuyOPRSellPriceTable.ConsumeUnitID + cid_string + " and "
                        + BuyOPRSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID

                        );
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                    , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public double? GetPrice(ItemIN ItemIN_, SellType SellType_, ConsumeUnit ConsumeUnit_)
            {
                try
                {

                    string cid_string = " is null";
                    if (ConsumeUnit_ != null)
                        if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = "=" + ConsumeUnit_.ConsumeUnitID.ToString();
                    double? price;

                    DataTable t = DB.GetData("select "
                          + BuyOPRSellPriceTable.Price
                        + " from "
                        + BuyOPRSellPriceTable.TableName
                         + " where "
                        + BuyOPRSellPriceTable.ItemINID + "=" + ItemIN_.ItemINID
                        + " and "
                         + BuyOPRSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID
                        + " and "
                         + BuyOPRSellPriceTable.ConsumeUnitID + cid_string);

                    if (t.Rows.Count == 1)
                    {
                        price = Convert.ToDouble(t.Rows[0][0]);
                    }
                    else price = null;
                    return price;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message); return null;
                }
            }
            public List<ItemINSellPrice> GetItemINPrices(ItemIN ItemIN_)
            {
                try
                {
                    List<ItemINSellPrice> list = new List<ItemINSellPrice>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + BuyOPRSellPriceTable.ConsumeUnitID + ","
                           + BuyOPRSellPriceTable.SellTypeID + ","
                          + BuyOPRSellPriceTable.Price
                        + " from "
                        + BuyOPRSellPriceTable.TableName
                         + " where "
                        + BuyOPRSellPriceTable.ItemINID + "=" + ItemIN_.ItemINID

                       );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        ConsumeUnit CU;
                        try
                        {
                            CU = new ConsumeUnitSql(DB).GetConsumeAmountinfo(Convert.ToUInt32(t.Rows[i][0].ToString()));
                        }
                        catch
                        {
                            CU = new ConsumeUnit(0, ItemIN_._Item.DefaultConsumeUnit, ItemIN_._Item, 1);
                        }
                        ItemINSellPrice m = new ItemINSellPrice(ItemIN_, CU, new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(t.Rows[i][1].ToString())), Convert.ToDouble(t.Rows[i][2].ToString()));
                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب اسعار العنصر:", ee.Message);
                    return null;
                }

            }

            internal bool  ClearINSellPrices(ItemIN ItemIN_)
            {
                try
                {
                    switch (ItemIN_._Operation.OperationType)
                    {
                        case Operation.BILL_BUY:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.ASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;
                        case Operation.DISASSEMBLAGE:
                            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                            break;

                        default:
                            throw new Exception("عملية غير صحيحة");


                    }

                    DB.ExecuteSQLCommand("delete from   "
                        + BuyOPRSellPriceTable.TableName
                        + " where "
                       + BuyOPRSellPriceTable.ItemINID + "=" + ItemIN_.ItemINID 

                        );
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.DELETE
                   , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                    , "تصفير الاسعار للعنصر المدخل بلعملية رقم:"+ItemIN_.ItemINID 
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE
                            , DatabaseInterface.Log.Log_Target.Trade_ItemIN_SellPrice
                            , "تصفير الاسعار للعنصر المدخل بلعملية رقم:" + ItemIN_.ItemINID
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("ClearINSellPrices:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public class AvailableItemSQL
        {



    
    
            DatabaseInterface DB;
            public AvailableItemSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public double Get_AvailabeAmount_by_Place(ItemIN ItemIN_, TradeStorePlace place)
            {

                try
                {
                    if (place != null)
                    {
                        TradeItemStore TradeItemStore_= new TradeItemStoreSQL(DB).GetTradeItemStoreINFO(place,ItemIN_.ItemINID ,TradeItemStore .ITEMIN_STORE_TYPE);
                        if (TradeItemStore_ == null) return 0;
                        double orginalfactor = (ItemIN_._ConsumeUnit == null ? 1 : ItemIN_._ConsumeUnit.Factor);
                        double consumefactor = (TradeItemStore_._ConsumeUnit == null ? 1 : TradeItemStore_._ConsumeUnit.Factor);
                        return (TradeItemStore_.Amount *(orginalfactor  / consumefactor )) - Get_SpentAmount_by_Place(ItemIN_, place);
                    }
                    else
                    {
                        List<TradeItemStore> list = new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(ItemIN_);
                        double amount_store = 0;
                        for (int i=0;i<list .Count;i++)
                        {
                            amount_store += list[i].Amount * ( list[i]._ConsumeUnit.Factor/ ItemIN_._ConsumeUnit.Factor);
                        }
                        return ItemIN_.Amount - amount_store - Get_SpentAmount_by_Place(ItemIN_, null);
                    }
                   
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetAvailabeAmount_by_Place:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;

                }



            }
            public double Get_SpentAmount_by_Place(ItemIN ItemIN_, TradeStorePlace place)
            {
                try
                {
            

                    List<ItemOUT> itemsoutlist;
                    if(place!=null )
                        itemsoutlist = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("Get_SpentAmount_by_Place-NotNull",ItemIN_).Where(x =>x.Place !=null && x.Place.PlaceID == place.PlaceID).ToList();
                    else
                        itemsoutlist = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("Get_SpentAmount_by_Place-Null", ItemIN_).Where(x => x.Place == null ).ToList();

                    double spentamount = 0;
                    double orginalfactor = (ItemIN_._ConsumeUnit == null ? 1 : ItemIN_._ConsumeUnit.Factor);

                    for (int i = 0; i < itemsoutlist.Count; i++)
                    {
                        double consumefactor = (itemsoutlist[i]._ConsumeUnit == null ? 1 : itemsoutlist[i]._ConsumeUnit.Factor);

                        spentamount += itemsoutlist[i].Amount * (consumefactor / orginalfactor);
                    }
                    return  spentamount; ;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetSpentAmount_by_Place:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;

                }

            }
            public double Get_AvailabeAmount_by_ItemIN(ItemIN ItemIN_)
            {
                try
                {
                    double orginalfactor = (ItemIN_._ConsumeUnit == null ? 1 : ItemIN_._ConsumeUnit.Factor);

                    List<ItemOUT> itemsoutlist = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("Get_AvailabeAmount_by_ItemIN",ItemIN_);
                    double spentamount = 0;
                    for (int i=0;i<itemsoutlist .Count;i++)
                    {
                        double consumefactor = (itemsoutlist[i]._ConsumeUnit == null ? 1 : itemsoutlist[i]._ConsumeUnit.Factor);

                        spentamount += itemsoutlist[i].Amount * (consumefactor / orginalfactor);
                    }
                    return ItemIN_.Amount - spentamount; ;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetAvailabeAmount_by_ItemIN:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;

                }


            }

            public string Get_AvailabeAmount_For_Item(uint itemid)
            {
                string amount = "";
                try
                {
                    List<ItemIN_AvailableAmount_Report> ItemIN_AvailableAmount_Reportlist = Get_ItemIN_AvailableAmount_Report_List ()
                        .Where (x=>x.ItemID ==itemid ).ToList ();
                    List<uint> tradestateID_List = ItemIN_AvailableAmount_Reportlist.Select(x => x.TradeStateID).ToList();
                    for(int i=0;i<tradestateID_List .Count;i++)
                    {
                        List<ItemIN_AvailableAmount_Report> ItemIN_Tradestate_List = ItemIN_AvailableAmount_Reportlist.Where(x => x.TradeStateID == tradestateID_List[i]).ToList();
                        string tradestate_name = ItemIN_Tradestate_List.Select(x => x.TradeStateName).ToList()[0];
                        double available_Amount = ItemIN_Tradestate_List.Sum (x=>x.AvailableAmount );
                        amount += available_Amount + " " + tradestate_name;
                        if (i != tradestateID_List.Count - 1) amount += ",";
                    }


                    return amount;

                }
                catch (Exception ee)
                {
                    MessageBox.Show("Get_AvailabeAmount_For_Item:" + ee.Message);
                    return "";
                }
            }
            public string Get_AvailabeAmount_For_Item_IN_Place(uint itemid,uint placeid)
            {
                string amount = "";
                try
                {
                    List<ItemIN_AvailableAmount_Report_PlaceDetail> ItemIN_Place_AvailableAmount_Report_list = Get_ItemIN_AvailableAmount_Report_PlaceDetail_List().Where (x=>x._ItemIN._Item.ItemID ==itemid
                    && x.Place !=null &&x.Place.PlaceID ==placeid ).ToList ();

                    List<uint> tradestateID_List = ItemIN_Place_AvailableAmount_Report_list.Select(x => x._ItemIN._TradeState.TradeStateID).ToList();
                    for (int i = 0; i < tradestateID_List.Count; i++)
                    {
                        List<ItemIN_AvailableAmount_Report_PlaceDetail> ItemIN_Tradestate_List = ItemIN_Place_AvailableAmount_Report_list.Where(x => x._ItemIN._TradeState.TradeStateID == tradestateID_List[i]).ToList();
                        string tradestate_name = ItemIN_Tradestate_List.Select(x => x._ItemIN._TradeState.TradeStateName).ToList()[0];
                        double available_Amount = ItemIN_Tradestate_List.Sum(x => x.AvailableAmount);
                        amount += available_Amount + " " + tradestate_name;
                        if (i != tradestateID_List.Count - 1) amount += ",";
                    }


                    return amount;

                }
                catch (Exception ee)
                {
                    MessageBox.Show("Get_AvailabeAmount_For_Item:" + ee.Message);
                    return "";
                }
            }

            
          internal List <ItemIN_AvailableAmount_Report> Get_ItemIN_AvailableAmount_Report_List()
            {
                List<ItemIN_AvailableAmount_Report> ItemIN_AvailableAmount_Report_List = new List<ItemIN_AvailableAmount_Report>();
                try
                {

                    DataTable para = new DataTable();
                    para.Columns.Add("UserID", typeof(uint));

                    DataRow row = para.NewRow();
                    row["UserID"] = DB.__User.UserID;

                    para.Rows.Add(row);

                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.TradeSQL_Get_ItemIN_AvailableAmount_Report_List ,
                       para);

                    return ItemIN_AvailableAmount_Report.Get_ItemIN_AvailableAmount_Report_List_From_DataTable(t);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Get_ItemIN_AvailableAmount_Report_List:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ItemIN_AvailableAmount_Report_List;
                }
            }
            internal List<ItemIN_AvailableAmount_Report_PlaceDetail> Get_ItemIN_AvailableAmount_Report_PlaceDetail_List()
            {
                List<ItemIN_AvailableAmount_Report_PlaceDetail> ItemIN_AvailableAmount_Report_PlaceDetail_List = new List<ItemIN_AvailableAmount_Report_PlaceDetail>();
                try
                {

                    List<ItemIN> iteminlist = new ItemINSQL(DB).Get_ALL_ItemIN_List();

                    for (int i = 0; i < iteminlist.Count; i++)
                    {
                        double store_amount = 0;
                        List<TradeItemStore> TradeItemStoreList = new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(iteminlist[i]);

                        for (int j = 0; j < TradeItemStoreList.Count; j++)
                        {
                            double ItemIN_Store_Available_amount = Get_AvailabeAmount_by_Place(iteminlist[i], TradeItemStoreList[j]._TradeStorePlace);
                            double ItemIN_Store_Spent_amount = Get_SpentAmount_by_Place(iteminlist[i], TradeItemStoreList[j]._TradeStorePlace);
                            store_amount += TradeItemStoreList[j].Amount *   TradeItemStoreList[j]._ConsumeUnit.Factor/ TradeItemStoreList[j]._ItemIN._ConsumeUnit.Factor;
                            if (ItemIN_Store_Available_amount > 0)
                                ItemIN_AvailableAmount_Report_PlaceDetail_List.Add(
                                    new ItemIN_AvailableAmount_Report_PlaceDetail(iteminlist[i], TradeItemStoreList[j]._TradeStorePlace, TradeItemStoreList[j].Amount, ItemIN_Store_Available_amount, ItemIN_Store_Spent_amount));

                        }

                        double ItemIN_None_Store_Available_amount = Get_AvailabeAmount_by_Place(iteminlist[i], null);
                        double ItemIN_None_Spent_amount = Get_SpentAmount_by_Place(iteminlist[i], null);
                        if (ItemIN_None_Store_Available_amount > 0)
                            ItemIN_AvailableAmount_Report_PlaceDetail_List.Add(
                                new ItemIN_AvailableAmount_Report_PlaceDetail(iteminlist[i], null,iteminlist[i].Amount -store_amount , ItemIN_None_Store_Available_amount, ItemIN_None_Spent_amount));

                    }
                    return ItemIN_AvailableAmount_Report_PlaceDetail_List;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Get_ItemIN_AvailableAmount_Report_PlaceDetail_List:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ItemIN_AvailableAmount_Report_PlaceDetail_List;
                }
            }
            internal List<Item_AvailableAmount_Report> Get_Item_AvailableAmount_Report_List()
            {
                try
                {
                    DataTable para = new DataTable();
                    para.Columns.Add("UserID", typeof(uint));

                    DataRow row = para.NewRow();
                    row["UserID"] = DB.__User .UserID ;

                    para.Rows.Add(row);

                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.TradeSQL_Get_Item_AvailableAmount_Report_List,
                       para);

                    return Item_AvailableAmount_Report.Get_Item_AvailableAmount_Report_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    MessageBox .Show ("Get_Item_AvailableAmount_Report_List:" + ee.Message,"",MessageBoxButtons.OK ,MessageBoxIcon.Error );
                    return new List<Item_AvailableAmount_Report>();
                }

            }
            internal List<Item_AvailableAmount_Report> Get_Item_AvailableAmount_Report_List_CUSTOM(List<Item> ItemList)
            {
                List<Item_AvailableAmount_Report> Item_AvailableAmount_Report_List = new List<Item_AvailableAmount_Report>();
                try
                {
                    List<Item_AvailableAmount_Report> TempList = Get_Item_AvailableAmount_Report_List();

                    for (int i=0;i<ItemList .Count;i++)
                    {
                        Item_AvailableAmount_Report_List.AddRange(TempList.Where(x => x.ItemID == TempList[i].ItemID));
                    }
                    return Item_AvailableAmount_Report_List;
                }
                catch (Exception ee)
                {
                    MessageBox .Show  ("Get_Item_AvailableAmount_Report_List_CUSTOM:" + ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
                    return new List<Item_AvailableAmount_Report>();
                }
            }

        }
        public class TradeStoreContainerSQL
        {
            DatabaseInterface DB;
            private static class TradeStoreContainerTable
            {
                public const string TableName = "Trade_Store_Container";
                public const string ContainerID = "ContainerID";
                public const string ContainerName = "ContainerName";
                public const string ParentContainerID = "ParentContainerID";
                public const string Desc = "Description";
            }
            public TradeStoreContainerSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public string GetContainerPath(TradeStoreContainer  container)
            {
                List<string> container_path = new List<string>();
                TradeStoreContainer f = container ;
                string s = "ROOT / ";
                while (f.ParentContainerID != null)
                {
                    f = GetContainerBYID(Convert.ToUInt32(f.ParentContainerID  ));
                    container_path.Add(f.ContainerName );
                }
                for (int i = container_path.Count - 1; i >= 0; i--)
                    s += container_path[i] + " /";
                return s;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="container"></param>
            /// <returns></returns>
            public TradeStoreContainer  GetParentContainer(TradeStoreContainer container)
            {
                try
                {
                    if (container.ParentContainerID == null) return null;
                    DataTable t = new DataTable();
                    try
                    {
                        t = DB.GetData("select  "
                                    + TradeStoreContainerTable.ContainerName + ","
                                    + TradeStoreContainerTable.ParentContainerID + ","
                                    + TradeStoreContainerTable.Desc
                                    + " from "
                                    + TradeStoreContainerTable.TableName
                                    + " where "
                                    + TradeStoreContainerTable.ContainerID + "=" + container.ParentContainerID
                                    );
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }


                    uint fid = Convert.ToUInt32(container.ParentContainerID);
                    string fname = t.Rows[0][0].ToString();
                    uint? p;
                    try
                    {
                        p = Convert.ToUInt32(t.Rows[0][1]);
                    }
                    catch
                    {
                        p = null;
                    }
                    string desc = t.Rows[0][2].ToString();
                    return new TradeStoreContainer(fid, fname, p, desc);

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("AddContainer" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }


               
            }
            public TradeStoreContainer GetContainerBYID(uint containerid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + TradeStoreContainerTable.ContainerName + ","
                    + TradeStoreContainerTable.ParentContainerID + ","
                    + TradeStoreContainerTable.Desc
                    + " from   "
                    + TradeStoreContainerTable.TableName
                    + " where "
                    + TradeStoreContainerTable.ContainerID + "=" + containerid
                      );
                    if (t.Rows.Count == 1)
                    {
                        string containername = t.Rows[0][0].ToString();

                        string desc = t.Rows[0][2].ToString();
                        uint? parentcontainerID;
                        try
                        {
                            parentcontainerID = Convert.ToUInt32(t.Rows[0][1].ToString());
                        }
                        catch
                        {
                            parentcontainerID = null;
                        }


                        return new TradeStoreContainer(containerid, containername, parentcontainerID, desc);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("AddContainer" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
               
            }
            public bool AddContainer(uint? parentcontainerID,string Containername, string desc)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    string parentid_string = "";
                    if (parentcontainerID == null)
                        parentid_string = "null";
                    else
                        parentid_string = parentcontainerID.ToString();
                    DB.ExecuteSQLCommand(" insert into "
                    + TradeStoreContainerTable.TableName
                    + "("
                    + TradeStoreContainerTable.ContainerName+","
                    +TradeStoreContainerTable.ParentContainerID + ","
                    + TradeStoreContainerTable.Desc 
                    + ")"
                    + "values"
                    + "("
                    + "'" + Containername  + "'"
                    +","
                    + parentid_string
                    + ","
                    +"'"+desc+"'" 
                    + ")"
                    );

                    DB.AddLog(
                                       DatabaseInterface.Log.LogType.INSERT
                                       , DatabaseInterface.Log.Log_Target.Trade_Store_Container
                                        , ""
                                        , true, "");
                    return true ;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_Store_Container
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false ;
                }
      
            }
            public bool UpdateContainer(uint containerid, string containername,string desc)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                    + TradeStoreContainerTable.TableName
                    + " set "
                    + TradeStoreContainerTable.ContainerName  + "='" + containername + "'"
                    + ","
                    + TradeStoreContainerTable.Desc + "='" + desc +"'"
                    + " where "
                    + TradeStoreContainerTable.ContainerID + "=" + containerid
                    );
                    DB.AddLog(
                                        DatabaseInterface.Log.LogType.UPDATE 
                                        , DatabaseInterface.Log.Log_Target.Trade_Store_Container
                                         , ""
                                         , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_Store_Container
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteContainer(TradeStoreContainer container)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (new TradeStorePlaceSQL(DB).GetPlacesINContainer(container).Count > 0
                        ||new TradeStoreContainerSQL(DB).GetContainerChildsList(container).Count >0)
                        throw new Exception("الحاوية غير فارغة لا يمكن الحذف");
                    DB.ExecuteSQLCommand("delete from   "
                    + TradeStoreContainerTable.TableName
                    + " where "
                    + TradeStoreContainerTable.ContainerID + "=" + container.ContainerID
                    );
                    DB.AddLog(
                                      DatabaseInterface.Log.LogType.DELETE 
                                      , DatabaseInterface.Log.Log_Target.Trade_Store_Container
                                       , ""
                                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_Store_Container
                            , ""
                          , false, ee.Message);
                    throw new Exception ("DeleteContainer:" + ee.Message);

                }
            }
            public List<TradeStoreContainer> GetContainerChildsList(TradeStoreContainer container)
            {
                try
                {
                    string parentid_string = "";
                    if (container == null)
                        parentid_string = " is null ";
                    else
                        parentid_string =" = "+ container.ContainerID  .ToString();
                    List<TradeStoreContainer> conainerchilds_list = new List<TradeStoreContainer>();
                    DataTable t = new DataTable();
                    //Forms.Form1 d = new Forms.Form1("select    "
                    //+ TradeStoreContainerTable.ContainerID + ","
                    //+ TradeStoreContainerTable.ContainerName + ","
                    //+ TradeStoreContainerTable.Desc
                    //+ " from   "
                    //+ TradeStoreContainerTable.TableName
                    //   + " where "
                    //+ TradeStoreContainerTable.ParentContainerID + parentid_string
                    // );
                    //d.ShowDialog();
                    t = DB.GetData("select    "
                    +TradeStoreContainerTable.ContainerID  + ","
                    + TradeStoreContainerTable.ContainerName + ","
                    + TradeStoreContainerTable.Desc
                    + " from   "
                    + TradeStoreContainerTable.TableName
                       + " where "
                    + TradeStoreContainerTable.ParentContainerID  + parentid_string
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint containerid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string containername = t.Rows[i][1].ToString();
                        string desc = t.Rows[i][2].ToString();
                        uint? p;
                        if (container  == null) p = null;
                        else p = container.ContainerID ;


                        conainerchilds_list.Add(new TradeStoreContainer(containerid, containername, p , desc));
                    }
                    return conainerchilds_list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("فشل جلب الحاويات الابناء" + ee.Message, "خطأ" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }

            internal List<TradeStoreContainer> SearchContainer(string text)
            {
                List<TradeStoreContainer> list = new List<TradeStoreContainer>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeStoreContainerTable.ContainerID + ","
                        + TradeStoreContainerTable.ContainerName + ","
                        + TradeStoreContainerTable.ParentContainerID + ","
                        + TradeStoreContainerTable.Desc
                        + " from " + TradeStoreContainerTable.TableName
                       + " where " + TradeStoreContainerTable.ContainerName + " like  '%" + text + "%'");
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint conainerid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string conainername = t.Rows[i][1].ToString();
                        uint? p;
                        try
                        {
                            p = Convert.ToUInt32(t.Rows[i][2].ToString());
                        }
                        catch
                        {
                            p = null;
                        }

                        string d = t.Rows[i][3].ToString();
                        list.Add(new TradeStoreContainer(conainerid, conainername, p, d));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show(ee.Message + ":فشل جلب بيانات مكان التخزين", "خطأ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

                
            }
        }
        public class TradeStorePlaceSQL
        {

            DatabaseInterface DB;
            private static class TradeStorePlaceTable
            {
                public const string TableName = "Trade_Store_Place";
                public const string PlaceID = "PlaceID";
                public const string PlaceName = "PlaceName";
                public const string ContainerID = "ContainerID";
                public const string Desc = "Description";

            }
           
            public TradeStorePlaceSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public string GetPlacePath(TradeStorePlace  place)
            {
                if (place == null) return "";
                TradeStoreContainerSQL  containerSQL_ = new TradeStoreContainerSQL(DB);
                List<string> container_path = new List<string>();
                TradeStoreContainer f = place._TradeStoreContainer;
                string s = "ROOT /";

                while (f.ParentContainerID  != null)
                {
                    container_path.Add(f.ContainerName );
                    f = containerSQL_ .GetContainerBYID (Convert.ToUInt32(f.ParentContainerID ));

                }
                container_path.Add(f.ContainerName );
                for (int i = container_path.Count - 1; i >= 0; i--)
                    s += container_path[i] + "/";
                return s;
            }
            public TradeStorePlace GetTradeStorePlaceBYID(uint placeid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + TradeStorePlaceTable.PlaceName + ","
                    + TradeStorePlaceTable.ContainerID + ","
                    + TradeStorePlaceTable.Desc
                    + " from   "
                    + TradeStorePlaceTable.TableName
                    + " where "
                    + TradeStorePlaceTable.PlaceID  + "=" + placeid
                      );
                    if (t.Rows.Count == 1)
                    {
                        string placename = t.Rows[0][0].ToString();
                        string desc = t.Rows[0][2].ToString();
                        TradeStoreContainer container = new TradeStoreContainerSQL(DB).GetContainerBYID(Convert.ToUInt32(t.Rows[0][1].ToString()));
                        return new TradeStorePlace(placeid, placename, container, desc);
                    }
                    else
                        return null;
                }
                catch(Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show(ee.Message + ":فشل جلب بيانات مكان التخزين", "خطأ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public bool AddPlace(TradeStoreContainer container, string placename, string desc)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" insert into "
                    + TradeStorePlaceTable.TableName
                    + "("
                    + TradeStorePlaceTable.PlaceName  + ","
                    + TradeStorePlaceTable.ContainerID  + ","
                    + TradeStorePlaceTable.Desc
                    + ")"
                    + "values"
                    + "("
                    + "'" + placename  + "'"
                    + ","
                    + container .ContainerID 
                    + ","
                    + "'" + desc + "'" 
                    + ")"
                    );
                    DB.AddLog(
                                      DatabaseInterface.Log.LogType.INSERT
                                      , DatabaseInterface.Log.Log_Target.Trade_Store_Place 
                                       , ""
                                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_Store_Place 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdatePlace( uint placeid, string placename,  string desc)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                    + TradeStorePlaceTable.TableName
                    + " set "
                    + TradeStorePlaceTable.PlaceName  + "='" + placename  + "'"
                    + ","
                    + TradeStorePlaceTable.Desc + "='" + desc + "'"
                    + " where "
                    + TradeStorePlaceTable.PlaceID  + "=" + placeid
                    );
                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.UPDATE 
                                     , DatabaseInterface.Log.Log_Target.Trade_Store_Place
                                      , ""
                                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_Store_Place
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeletePlace(TradeStorePlace  place)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (new TradeItemStoreSQL(DB).GetItemsStoredINPlace(place).Count > 0)
                        throw new Exception("لا يمكن الحزف لانه توجد مواد مخزنة ضمن هذا المكان, يجب  افراغ هذه المكان أولا");
                    DB.ExecuteSQLCommand("delete from   "
                    + TradeStorePlaceTable.TableName
                    + " where "
                    + TradeStorePlaceTable.PlaceID  + "=" + place.PlaceID 
                    );
                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.DELETE 
                                     , DatabaseInterface.Log.Log_Target.Trade_Store_Place
                                      , ""
                                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_Store_Place
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<TradeStorePlace > GetPlacesINContainer(TradeStoreContainer container)
            {
                List<TradeStorePlace> conainerPlaces_list = new List<TradeStorePlace>();

                try
                {
                 
                    if (container == null) return conainerPlaces_list;
                    DataTable t = new DataTable();
                    t = DB.GetData("select    "
                    + TradeStorePlaceTable.PlaceID + ","
                    + TradeStorePlaceTable.PlaceName + ","
                    + TradeStorePlaceTable.Desc
                    + " from   "
                    + TradeStorePlaceTable.TableName
                       + " where "
                    + TradeStorePlaceTable.ContainerID + "=" + container .ContainerID 
                      );
                  
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint placeid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string placename = t.Rows[i][1].ToString();
                        string desc = t.Rows[i][2].ToString();


                        conainerPlaces_list.Add(new TradeStorePlace (placeid , placename , container, desc));
                    }
                   
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show(ee.Message+ ":فشل جلب اماكن التخزين في الحاوية", "خطأ" + ee.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return conainerPlaces_list;
            }

            internal List<TradeStorePlace> SearchPlace(string text)
            {

                List<TradeStorePlace> list = new List<TradeStorePlace>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeStorePlaceTable.PlaceID + ","
                        + TradeStorePlaceTable.PlaceName + ","
                        + TradeStorePlaceTable.ContainerID + ","
                        + TradeStorePlaceTable.Desc
                        + " from " + TradeStorePlaceTable.TableName
                       + " where " + TradeStorePlaceTable.PlaceName + " like  '%" + text + "%'");
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint placeid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string placename = t.Rows[i][1].ToString();
                        TradeStoreContainer container = new TradeStoreContainerSQL(DB).GetContainerBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        string d = t.Rows[i][3].ToString();
                        list.Add(new TradeStorePlace(placeid, placename, container, d));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("SearchPlace" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list;
                }
               
            }

            internal List<TradeStorePlace> SearchPlacesInContainer(TradeStoreContainer _Container, string text)
            {
                List<TradeStorePlace> list = new List<TradeStorePlace>();
                if (_Container == null) return list;


                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeStorePlaceTable.PlaceID + ","
                        + TradeStorePlaceTable.PlaceName + ","
                        + TradeStorePlaceTable.ContainerID + ","
                        + TradeStorePlaceTable.Desc
                        + " from " + TradeStorePlaceTable.TableName
                       + " where " + TradeStorePlaceTable.PlaceName  + " like '%" + text  + "%'"
                       + " and " + TradeStorePlaceTable.ContainerID  + "=" + _Container.ContainerID 

                       + " order by " + TradeStorePlaceTable.PlaceName 
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint placeid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string placename = t.Rows[i][1].ToString();
                        TradeStoreContainer container = new TradeStoreContainerSQL(DB).GetContainerBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        string d = t.Rows[i][3].ToString();
                        list.Add(new TradeStorePlace(placeid, placename, container, d));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("SearchItemINFolder:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list;
                }

            }

        }
        public class BillSellSQL
        {
            DatabaseInterface DB;
            private static class BillSELLTable
            {
                public const string TableName = "Trade_BillSell";
                public const string BillSellID = "BillSellID";
                public const string BillDate = "BillDate";
                public const string BillDescription = "BillDescription";
                public const string SellTypeID = "SellTypeID";
                public const string ContactID = "ContactID";
                public const string CurrencyID = "CurrencyID";
                public const string ExchangeRate = "ExchangeRate";
                public const string Discount = "Discount";
                public const string Notes = "Notes";

            }
            public BillSellSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public BillSell  GetBillSell_INFO_BYID(uint billid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + BillSELLTable.BillDate  + ","
                    + BillSELLTable.BillDescription + ","
                    + BillSELLTable.SellTypeID  + ","
                    + BillSELLTable.ContactID + ","
                    + BillSELLTable.CurrencyID + ","
                     + BillSELLTable.ExchangeRate + ","
                    + BillSELLTable.Discount + ","
                    + BillSELLTable.Notes
                    + " from   "
                    + BillSELLTable.TableName
                    + " where "
                    + BillSELLTable.BillSellID + "=" + billid 
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime billindate = Convert.ToDateTime(t.Rows[0][0]);
                        string desc = t.Rows[0][1].ToString();
                        SellType  SellType_ =new SellTypeSql (DB).GetSellTypeinfo (Convert .ToUInt32 ( t.Rows[0][2].ToString()));
                        Contact Contact_ = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[0][3].ToString()));
                        Currency Currency_ = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                        double exchangerate = Convert.ToDouble(t.Rows[0][5].ToString());
                        double discount = Convert.ToDouble(t.Rows[0][6].ToString());
                        string notes = t.Rows[0][7].ToString();
                        if (Currency_.ReferenceCurrencyID == null && exchangerate  != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                        return new BillSell (billid , billindate, desc, SellType_, Contact_, Currency_, exchangerate, discount, notes);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillIN_INFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public BillSell GetBillSell_INFO_BY_RowID(uint rowid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + BillSELLTable.BillSellID + ","
                    + BillSELLTable.BillDate + ","
                    + BillSELLTable.BillDescription + ","
                    + BillSELLTable.SellTypeID + ","
                    + BillSELLTable.ContactID + ","
                    + BillSELLTable.CurrencyID + ","
                     + BillSELLTable.ExchangeRate + ","
                    + BillSELLTable.Discount + ","
                    + BillSELLTable.Notes
                    + " from   "
                    + BillSELLTable.TableName
                    + " where "
                    + DatabaseInterface.ROWID_COLUMN  + "=" + rowid 
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint billid = Convert.ToUInt32(t.Rows[0][0]);
                        DateTime billindate = Convert.ToDateTime(t.Rows[0][1]);
                        string desc = t.Rows[0][2].ToString();
                        SellType SellType_ = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(t.Rows[0][3].ToString()));
                        Contact Contact_ = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                        Currency Currency_ = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][5].ToString()));
                        double exchangerate = Convert.ToDouble(t.Rows[0][6].ToString());
                        double discount = Convert.ToDouble(t.Rows[0][7].ToString());
                        string notes = t.Rows[0][8].ToString();
                        if (Currency_.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                        return new BillSell(billid, billindate, desc, SellType_, Contact_, Currency_, exchangerate, discount, notes);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillIN_INFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }

            public BillSell  AddBillSell(DateTime billindate, string description,SellType  SellType_, Contact contact, Currency currency, double exchangerate, double discount, string notes)
            {
                try
                {
        
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (currency.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception("معامل صرف العملة الرجعية يجب أن يكون 1");

                    DataTable t = DB.GetData(" insert into "
                    + BillSELLTable.TableName
                    + "("
                    + BillSELLTable.BillDate 
                    + ","
                    + BillSELLTable.BillDescription
                    + ","
                    + BillSELLTable.SellTypeID
                    + ","
                    + BillSELLTable.ContactID
                    + ","
                    + BillSELLTable.CurrencyID
                    + ","
                     + BillSELLTable.ExchangeRate
                    + ","
                    + BillSELLTable.Discount
                    + ","
                    + BillSELLTable.Notes
                    + ")"
                    + "values"
                    + "("
                    + "'" + billindate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + "'" + description + "'"
                     + ","
                    +  SellType_ .SellTypeID
                    + ","
                    + contact.ContactID
                    + ","
                    + currency.CurrencyID
                    + ","
                    + exchangerate
                    + ","
                    + discount
                    + ","
                    + "'" + notes + "'"
                    + ")"
                    + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());

                    
                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.INSERT
                                     , DatabaseInterface.Log.Log_Target.Trade_BillSell
                                      , ""
                                      , true, "");
                    return GetBillSell_INFO_BY_RowID(rowid);
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_BillSell 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
            }
            public bool UpdateBillIN(uint billid, DateTime billindate, string description,SellType   SellType_, Contact contact, Currency currency, double exchangerate, double discount, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (currency.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception("معامل صرف العملة الرجعية يجب أن يكون 1");

                    DB.ExecuteSQLCommand("update  "
                    + BillSELLTable.TableName
                    + " set "
                    + BillSELLTable.BillDate  + "='" + billindate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + BillSELLTable.BillDescription + "='" + description + "'"
                     + ","
                    + BillSELLTable.SellTypeID + "=" + SellType_.SellTypeID
                    + ","
                    + BillSELLTable.ContactID + "=" + contact.ContactID
                    + ","
                    + BillSELLTable.CurrencyID + "=" + currency.CurrencyID
                    + ","
                   + BillSELLTable.ExchangeRate + "=" + exchangerate
                    + ","
                    + BillSELLTable.Discount + "=" + discount
                    + ","
                    + BillSELLTable.Notes + "='" + notes + "'"
                    + " where "
                    + BillSELLTable.BillSellID  + "=" + billid 
                    );

                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.UPDATE 
                                     , DatabaseInterface.Log.Log_Target.Trade_BillSell
                                      , ""
                                      , true, "");
                    return true ;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_BillSell
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool DeleteBillSell(uint  billsellid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    BillSell  billsell = GetBillSell_INFO_BYID(billsellid);
                    if (new ItemOUTSQL (DB).GetItemOUTList(billsell._Operation).Count > 0 || new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(billsell._Operation).Count > 0)
                    {
                        throw new Exception("يجب حذف بنود الفاتورة اولا");
                    }
                    if (new PayINSQL(DB).GetPayINList (billsell._Operation).Count > 0) throw new Exception("يجب حذف كل الدفعات التابعة لهذه الفاتورة أولا");

                    DB.ExecuteSQLCommand("delete from   "
                    + BillSELLTable.TableName
                    + " where "
                    + BillSELLTable.BillSellID  + "=" + billsell._Operation .OperationID 
                    );
                    DB.AddLog(
                                      DatabaseInterface.Log.LogType.DELETE 
                                      , DatabaseInterface.Log.Log_Target.Trade_BillSell
                                       , ""
                                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_BillSell
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            internal double GetBillSellValue(uint billsellid)
            {
                try
                {

                    return new OperationSQL(DB).Get_OperationValue(Operation.BILL_SELL, billsellid );
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillSellValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }
            }
            internal double GetBillSell_PaysValue(uint billsellid)
            {
                try
                {
                    return new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency (Operation.BILL_SELL , billsellid );
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillSellValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }
            }
            internal List<BillSell > Get_All_BillSell_List()
            {
                List<BillSell> list = new List<BillSell>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + BillSELLTable.BillSellID + ","
                    + BillSELLTable.BillDate + ","
                    + BillSELLTable.BillDescription + ","
                    + BillSELLTable.SellTypeID + ","
                    + BillSELLTable.ContactID + ","
                    + BillSELLTable.CurrencyID + ","
                     + BillSELLTable.ExchangeRate + ","
                    + BillSELLTable.Discount + ","
                    + BillSELLTable.Notes
                    + " from   "
                    + BillSELLTable.TableName
                      );
                   for(int i=0;i<t.Rows.Count;i++)
                    {
                        uint billid = Convert.ToUInt32(t.Rows[i][0]);
                        DateTime billindate = Convert.ToDateTime(t.Rows[i][1]);
                        string desc = t.Rows[i][2].ToString();
                        SellType SellType_ = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(t.Rows[i][3].ToString()));
                        Contact Contact_ = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[i][4].ToString()));
                        Currency Currency_ = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        double exchangerate = Convert.ToDouble(t.Rows[i][6].ToString());
                        double discount = Convert.ToDouble(t.Rows[i][7].ToString());
                        string notes = t.Rows[i][8].ToString();
                        if (Currency_.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                        list.Add ( new BillSell(billid, billindate, desc, SellType_, Contact_, Currency_, exchangerate, discount, notes));

                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetAllBillSell:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return list;
            }
        }
        public class RavageOPRSQL
        {
            DatabaseInterface DB;
            private static class RavageOPR_Table
            {
                public const string TableName = "Trade_RavageOPR";
                public const string RavageOPRID = "RavageOPRID";
                public const string RavageOPRDate = "RavageOPRDate";
                public const string PartID = "PartID";

                public const string Notes = "Notes";

            }
            public RavageOPRSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public RavageOPR GetRavageOPR_INFO_BYID(uint RavageOPRid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + RavageOPR_Table.RavageOPRDate + ","
                    + RavageOPR_Table.PartID    + ","
                    + RavageOPR_Table.Notes
                    + " from   "
                    + RavageOPR_Table.TableName
                    + " where "
                    + RavageOPR_Table.RavageOPRID + "=" + RavageOPRid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime RavageOPRindate = Convert.ToDateTime(t.Rows[0][0]);
                        Part Part_;
                        try
                        {
                            Part_ = new Company.CompanySQL.PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[0][1].ToString()));

                        }
                        catch
                        {
                            Part_ = null;
                        }
                        string notes = t.Rows[0][2].ToString();
                        return new RavageOPR(RavageOPRid, RavageOPRindate, Part_,0, notes);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetRavageOPR_INFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public RavageOPR GetRavageOPR_INFO_BY_RowID(uint rowid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + RavageOPR_Table.RavageOPRID + ","
                    + RavageOPR_Table.RavageOPRDate + ","
                    + RavageOPR_Table.PartID  + ","
                    + RavageOPR_Table.Notes
                    + " from   "
                    + RavageOPR_Table.TableName
                    + " where "
                    + DatabaseInterface.ROWID_COLUMN + "=" + rowid
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint RavageOPRid = Convert.ToUInt32(t.Rows[0][0]);
                        DateTime RavageOPRindate = Convert.ToDateTime(t.Rows[0][1]);
                        Part Part_;
                        try
                        {
                            Part_ = new Company.CompanySQL.PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[0][2].ToString()));

                        }
                        catch
                        {
                            Part_ = null;
                        }
                        string notes = t.Rows[0][3].ToString();
                        return new RavageOPR(RavageOPRid, RavageOPRindate, Part_, 0, notes);
                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetRavageOPR_INFO_BY_RowID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }

            public RavageOPR AddRavageOPR(DateTime RavageOPRindate, Part Part_, string notes)
            {
                try
                {

                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) )) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = DB.GetData(" insert into "
                    + RavageOPR_Table.TableName
                    + "("
                    + RavageOPR_Table.RavageOPRDate
                    + ","
                    + RavageOPR_Table.PartID 
                    + ","
                    + RavageOPR_Table.Notes
                    + ")"
                    + "values"
                    + "("
                    + "'" + RavageOPRindate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    
                    + (Part_ ==null ?"null":Part_.PartID .ToString ())
                    + ","
                    + "'" + notes + "'"
                    + ")"
                    + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());


                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.INSERT
                                     , DatabaseInterface.Log.Log_Target.Trade_RavageOPR
                                      , ""
                                      , true, "");
                    return GetRavageOPR_INFO_BY_RowID(rowid);
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_RavageOPR
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("AddRavageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public bool UpdateRavageOPR(uint RavageOPRid, DateTime RavageOPRdate, Part Part_, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                    + RavageOPR_Table.TableName
                    + " set "
                    + RavageOPR_Table.RavageOPRDate + "='" + RavageOPRdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                     + RavageOPR_Table.PartID  + "=" + (Part_ ==null ?"null":Part_ .PartID .ToString ())
                    + ","
                    + RavageOPR_Table.Notes + "='" + notes + "'"
                    + " where "
                    + RavageOPR_Table.RavageOPRID + "=" + RavageOPRid
                    );

                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.UPDATE
                                     , DatabaseInterface.Log.Log_Target.Trade_RavageOPR
                                      , ""
                                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE
                            , DatabaseInterface.Log.Log_Target.Trade_RavageOPR
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("UpdateRavageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteRavageOPR(uint RavageOPRid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) )) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from   "
                   + ItemOUTSQL.ItemOUTTable.TableName
                   + " where "
                   + ItemOUTSQL.ItemOUTTable.OperationType + "=" + Operation.RavageOPR
                   + " and "
                   + ItemOUTSQL.ItemOUTTable.OperationID + "=" + RavageOPRid
                   );
                    DB.ExecuteSQLCommand("delete from   "
                    + RavageOPR_Table.TableName
                    + " where "
                    + RavageOPR_Table.RavageOPRID + "=" + RavageOPRid
                    );
                    DB.AddLog(
                                      DatabaseInterface.Log.LogType.DELETE
                                      , DatabaseInterface.Log.Log_Target.Trade_RavageOPR
                                       , ""
                                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE
                            , DatabaseInterface.Log.Log_Target.Trade_RavageOPR
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("DeleteRavageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }


            internal List<RavageOPR> Get_All_RavageOPR_List()
            {
                List<RavageOPR> list = new List<RavageOPR>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + RavageOPR_Table.RavageOPRID + ","
                    + RavageOPR_Table.RavageOPRDate + ","
                    + RavageOPR_Table.PartID  + ","
                    + RavageOPR_Table.Notes
                    + " from   "
                    + RavageOPR_Table.TableName
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint RavageOPRid = Convert.ToUInt32(t.Rows[i][0]);
                        DateTime RavageOPRindate = Convert.ToDateTime(t.Rows[i][1]);
                        Part Part_;
                        try
                        {
                            Part_ = new Company.CompanySQL.PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[i][2].ToString()));

                        }
                        catch
                        {
                            Part_ = null;
                        }
                        string notes = t.Rows[i][3].ToString();
                        list .Add ( new RavageOPR(RavageOPRid, RavageOPRindate, Part_,new ItemOUTSQL (DB).GetItemOUTList(
                            new Operation(Operation.RavageOPR ,RavageOPRid)).Count , notes));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetAllRavageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return list;
            }
        }
        public class TradeItemStoreSQL
        {
            DatabaseInterface DB;
            private static class TradeItemStoreTable
            {
                public const string TableName = "Trade_Items_Store";
                public const string PlaceID = "PlaceID";
                public const string ItemSourceOPRID = "ItemSourceOPRID";
                public const string StoreType = "StoreType";

                public const string Amount = "Amount";
                public const string ConsumeUnitID = "ConsumeUnitID";


            }
            public TradeItemStoreSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public TradeItemStore GetTradeItemStoreINFO(TradeStorePlace place, uint ItemSourceOPRID_,uint StoreType_)
            {
                try
                {
                    if (place == null) throw new Exception("مكان التخزين يجب ان يكون معرف");
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeItemStoreTable.Amount+","
                        +TradeItemStoreTable .ConsumeUnitID 
                        + " from   "
                        + TradeItemStoreTable.TableName
                        + " where "
                        + TradeItemStoreTable.PlaceID + "=" + place.PlaceID
                        +" and "
                        + TradeItemStoreTable.ItemSourceOPRID  + "=" + ItemSourceOPRID_
                        + " and "
                        + TradeItemStoreTable.StoreType  + "=" + StoreType_ 
                      );

                    if (t.Rows.Count == 1)
                    {
                        double  amount = Convert.ToDouble (t.Rows[0][0].ToString());
                 
                        switch (StoreType_)
                        {
                            case TradeItemStore.ITEMIN_STORE_TYPE:
                                ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(ItemSourceOPRID_);
                                ConsumeUnit consumeunit;
                                try
                                {
                                    uint consumeunit_id = Convert.ToUInt32(t.Rows[0][1].ToString());
                                    consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                                }
                                catch
                                {
                                    consumeunit = new ConsumeUnit(0, itemin._Item.DefaultConsumeUnit,itemin._Item,1);
                                }
                                return new TradeItemStore(place, itemin, amount, consumeunit);
                            case TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE:
                                MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(ItemSourceOPRID_);
                                try
                                {
                                    uint consumeunit_id = Convert.ToUInt32(t.Rows[0][1].ToString());
                                    consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                                }
                                catch
                                {
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_._Item.DefaultConsumeUnit, MaintenanceOPR_._Item, 1);
                                }
                                return new TradeItemStore(place, MaintenanceOPR_, amount, consumeunit);

                            case TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE:
                                MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ = new Maintenance.MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(ItemSourceOPRID_);
                                try
                                {
                                    uint consumeunit_id = Convert.ToUInt32(t.Rows[0][1].ToString());
                                    consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                                }
                                catch
                                {
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_Accessory_._Item.DefaultConsumeUnit, MaintenanceOPR_Accessory_._Item, 1);
                                }
                                return new TradeItemStore(place, MaintenanceOPR_Accessory_, amount, consumeunit);
                            default: throw new Exception("نمط تخزين خاطىء");
                        }
                    }
                    else return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("TradeItemStoreSQL-GetTradeItemStoreINFO:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public bool IS_ItemStoredInPlace( uint PlaceID, uint ItemSourceOPRID_, uint StoreType_)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select  * from   "
                        + TradeItemStoreTable.TableName
                        +" where "
                        + TradeItemStoreTable.PlaceID +"="+PlaceID
                       + " and "
                        + TradeItemStoreTable.ItemSourceOPRID + "=" + ItemSourceOPRID_
                        + " and "
                        + TradeItemStoreTable.StoreType + "=" + StoreType_
                      );
                    if (t.Rows.Count > 0) return true;
                    else return false;
                }
                catch(Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("TradeItemStoreSQL-IS_ItemStoredInPlace" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    return false; 
                }
            }
            public bool UpdateItemAmountStored( uint PlaceID, uint ItemSourceOPRID_, uint StoreType_, double  amount, ConsumeUnit ConsumeUnit_)
            {
                try
                {

                    DB.ExecuteSQLCommand("update  "
                        + TradeItemStoreTable.TableName
                        + " set "
                        + TradeItemStoreTable.Amount + "=" + amount
                        +","
                         + TradeItemStoreTable.ConsumeUnitID + "=" + (ConsumeUnit_ == null ? "null" : ConsumeUnit_.ConsumeUnitID.ToString ())
                        + " where "
                        + TradeItemStoreTable.PlaceID + "=" + PlaceID
                         + " and "
                        + TradeItemStoreTable.ItemSourceOPRID + "=" + ItemSourceOPRID_
                        + " and "
                        + TradeItemStoreTable.StoreType + "=" + StoreType_
                      );
                    DB.AddLog(
                                    DatabaseInterface.Log.LogType.UPDATE
                                    , DatabaseInterface.Log.Log_Target.Trade_ItemsStore 
                                     , ""
                                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE
                            , DatabaseInterface.Log.Log_Target.Trade_ItemsStore
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("TradeItemStoreSQL-UpdateItemAmountStored:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Store_Item_INPlace( uint PlaceID, uint ItemSourceOPRID_, uint StoreType_, double  amount,ConsumeUnit ConsumeUnit_)
            {
                try
                {
                    //if (IS_ItemStoredInPlace(PlaceID, ItemSourceOPRID_ ,StoreType_ ))
                    //    UpdateItemAmountStored(PlaceID, ItemSourceOPRID_, StoreType_, amount);
                    //else 
                    DB.ExecuteSQLCommand(" insert into "
                    + TradeItemStoreTable.TableName
                    + "("

                    + TradeItemStoreTable.PlaceID
                    + ","
                     + TradeItemStoreTable.ItemSourceOPRID
                    + ","
                     + TradeItemStoreTable.StoreType 
                    + ","
                    + TradeItemStoreTable.Amount
                    + ","
                    + TradeItemStoreTable.ConsumeUnitID 
                    + ")"
                    + "values"
                    + "("
 
                    + PlaceID
                    + ","
                    + ItemSourceOPRID_ 
                    + ","
                    + StoreType_ 
                    + ","
                    + amount
                    + ","
                    + (ConsumeUnit_ ==null ||ConsumeUnit_ .ConsumeUnitID ==0 ?"null": ConsumeUnit_.ConsumeUnitID .ToString ())
                    + ")"
                    );
                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.INSERT 
                                     , DatabaseInterface.Log.Log_Target.Trade_ItemsStore
                                      , ""
                                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemsStore
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("TradeItemStoreSQL-Store_Item_INPlace" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UNStore_Item_INPlace( uint PlaceID, uint ItemSourceOPRID_, uint StoreType_)
            {
                try
                {
                    DB.ExecuteSQLCommand("delete from   "
                        + TradeItemStoreTable.TableName
                        + " where "
                        + TradeItemStoreTable.PlaceID + "=" + PlaceID
                         + " and "
                        + TradeItemStoreTable.ItemSourceOPRID + "=" + ItemSourceOPRID_
                        + " and "
                        + TradeItemStoreTable.StoreType + "=" + StoreType_
                      );
                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.DELETE 
                                     , DatabaseInterface.Log.Log_Target.Trade_ItemsStore
                                      , ""
                                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_ItemsStore
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("TradeItemStoreSQL-UNStore_Item_INPlace" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<TradeItemStore_Report > Get_TradeItemStore_Report_List()
            {

                try
                {

                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.TradeItemStoreSQL_Get_TradeItemStore_Report_List,
                       new DataTable());

                    return TradeItemStore_Report.Get_TradeItemStore_Report_List_From_DataTable(t);

                }
                catch (Exception ee)
                {
                    throw new Exception ("TradeItemStoreSQL-GetItemsStoredINPlace:" + ee.Message);
                    
                }
            }

            public List<TradeStoreItems_AvailableAmount_Report> GetItemsStoredINPlace(TradeStorePlace place)
            {
                List<TradeStoreItems_AvailableAmount_Report> storeditems = new List<TradeStoreItems_AvailableAmount_Report>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeItemStoreTable.ItemSourceOPRID   + ","
                          + TradeItemStoreTable.StoreType  + ","
                         + TradeItemStoreTable.Amount + ","
                        + TradeItemStoreTable.ConsumeUnitID
                        + " from   "
                        + TradeItemStoreTable.TableName
                        +" where "
                         + TradeItemStoreTable.PlaceID+"=" +place .PlaceID 
                      );
                    
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint itemsourceopr_id = Convert.ToUInt32(t.Rows[i][0].ToString());
                        uint storetype = Convert.ToUInt32(t.Rows[i][1].ToString());
                        double  amount = Convert.ToDouble(t.Rows[i][2].ToString());

                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunit_id = Convert.ToUInt32(t.Rows[i][3].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                        }
                        catch
                        {
                            consumeunit = null;
                        }

                        switch (storetype)
                        {
                            case TradeItemStore.ITEMIN_STORE_TYPE:
   
                                ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(itemsourceopr_id);
                                double availabe_amount = new AvailableItemSQL(DB).Get_AvailabeAmount_by_ItemIN(itemin);
                                if (consumeunit == null)
                                    consumeunit = new ConsumeUnit(0, itemin._Item.DefaultConsumeUnit, itemin._Item, 1);
                                storeditems.Add(new TradeStoreItems_AvailableAmount_Report( new TradeItemStore(place, itemin, amount, consumeunit),availabe_amount ));
                                break;
                            case TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE:

                                MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(itemsourceopr_id);
                                double maintenaceopr_stillin_store = 0;
                                if (MaintenanceOPR_._MaintenanceOPR_EndWork == null)
                                    maintenaceopr_stillin_store = 1;
                                else if(MaintenanceOPR_._MaintenanceOPR_EndWork.DeliveredDate ==null )
                                    maintenaceopr_stillin_store = 1;
                                else 
                                    maintenaceopr_stillin_store =0;
                                if (consumeunit == null)
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_._Item.DefaultConsumeUnit, MaintenanceOPR_._Item, 1);
                                storeditems.Add(new TradeStoreItems_AvailableAmount_Report ( new TradeItemStore(place, MaintenanceOPR_, amount, consumeunit), maintenaceopr_stillin_store));
                                break;
                            case TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE:
                                MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ = new Maintenance.MaintenanceSQL.MaintenanceAccessorySQL (DB).Get_Accessory_INFO_BYID(itemsourceopr_id);
                                double accessory_stillin_store = 0;
                                if (MaintenanceOPR_Accessory_._MaintenanceOPR._MaintenanceOPR_EndWork == null)
                                    accessory_stillin_store = 1;
                                else if (MaintenanceOPR_Accessory_._MaintenanceOPR._MaintenanceOPR_EndWork.DeliveredDate == null)
                                    accessory_stillin_store = 1;
                                else
                                    accessory_stillin_store = 0;
                                if (consumeunit == null)
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_Accessory_._Item.DefaultConsumeUnit, MaintenanceOPR_Accessory_._Item, 1);
                                storeditems.Add(new TradeStoreItems_AvailableAmount_Report ( new TradeItemStore(place, MaintenanceOPR_Accessory_, amount, consumeunit), accessory_stillin_store));
                                break;
                        }
                    }
                    return storeditems;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("TradeItemStoreSQL-GetItemsStoredINPlace" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public List<TradeStoreItems_AvailableAmount_Report> GetItemsStoredINPlace_BY_Item(TradeStorePlace place,Item item)
            {
                List<TradeStoreItems_AvailableAmount_Report> storeditems = new List<TradeStoreItems_AvailableAmount_Report>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeItemStoreTable.ItemSourceOPRID + ","
                          + TradeItemStoreTable.StoreType + ","
                         + TradeItemStoreTable.Amount + ","
                        + TradeItemStoreTable.ConsumeUnitID
                        + " from   "
                        + TradeItemStoreTable.TableName
                        + " where "
                         + TradeItemStoreTable.PlaceID + "=" + place.PlaceID
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint itemsourceopr_id = Convert.ToUInt32(t.Rows[i][0].ToString());
                        uint storetype = Convert.ToUInt32(t.Rows[i][1].ToString());

                        double amount = Convert.ToInt32(t.Rows[i][2].ToString());
                        ConsumeUnit consumeunit;
                        
                        switch (storetype)
                        {
                            case TradeItemStore.ITEMIN_STORE_TYPE:
                                ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(itemsourceopr_id);
                                if (itemin._Item.ItemID != item.ItemID) continue;
                                double availabe_amount = new AvailableItemSQL(DB).Get_AvailabeAmount_by_ItemIN(itemin);
                                try
                                {
                                    uint consumeunit_id = Convert.ToUInt32(t.Rows[i][3].ToString());
                                    consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                                }
                                catch
                                {
                                    consumeunit = new ConsumeUnit (0,itemin._Item.DefaultConsumeUnit,itemin._Item,1);
                                }
                                storeditems.Add(new TradeStoreItems_AvailableAmount_Report(new TradeItemStore(place, itemin, amount, consumeunit), availabe_amount));
                                break;
                            case TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE:
                                MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(itemsourceopr_id);
                                if (MaintenanceOPR_._Item.ItemID != item.ItemID) continue;
                                double maintenaceopr_stillin_store = 0;
                                if (MaintenanceOPR_._MaintenanceOPR_EndWork == null)
                                    maintenaceopr_stillin_store = 1;
                                else if (MaintenanceOPR_._MaintenanceOPR_EndWork.DeliveredDate == null)
                                    maintenaceopr_stillin_store = 1;
                                else
                                    maintenaceopr_stillin_store = 0;
                                try
                                {
                                    uint consumeunit_id = Convert.ToUInt32(t.Rows[i][3].ToString());
                                    consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                                }
                                catch
                                {
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_._Item.DefaultConsumeUnit, MaintenanceOPR_._Item, 1);
                                }
                                storeditems.Add(new TradeStoreItems_AvailableAmount_Report(new TradeItemStore(place, MaintenanceOPR_, amount, consumeunit), maintenaceopr_stillin_store));
                                break;
                            case TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE:
                                MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ = new Maintenance.MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(itemsourceopr_id);
                                if (MaintenanceOPR_Accessory_._Item.ItemID != item.ItemID) continue;

                                double accessory_stillin_store = 0;
                                if (MaintenanceOPR_Accessory_._MaintenanceOPR._MaintenanceOPR_EndWork == null)
                                    accessory_stillin_store = 1;
                                else if (MaintenanceOPR_Accessory_._MaintenanceOPR._MaintenanceOPR_EndWork.DeliveredDate == null)
                                    accessory_stillin_store = 1;
                                else
                                    accessory_stillin_store = 0;
                                try
                                {
                                    uint consumeunit_id = Convert.ToUInt32(t.Rows[i][3].ToString());
                                    consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                                }
                                catch
                                {
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_Accessory_._Item.DefaultConsumeUnit, MaintenanceOPR_Accessory_._Item, 1);
                                }
                                storeditems.Add(new TradeStoreItems_AvailableAmount_Report(new TradeItemStore(place, MaintenanceOPR_Accessory_, amount, consumeunit), accessory_stillin_store));
                                break;
                        }
                    }
                    return storeditems;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("TradeItemStoreSQL-GetItemsStoredINPlace" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }

            public List<TradeItemStore> Get_ItemIN_StoredPlaces(ItemIN ItemIN_)
            {
                
                List<TradeItemStore> storeditems = new List<TradeItemStore>();
                try
                {
    
                    DataTable t= DB.GetData("select "
                        + TradeItemStoreTable.PlaceID  + ","
                        + TradeItemStoreTable.Amount + ","
                        + TradeItemStoreTable.ConsumeUnitID
                        + " from   "
                        + TradeItemStoreTable.TableName
                        + " where "
                        + TradeItemStoreTable.ItemSourceOPRID + "=" + ItemIN_.ItemINID
                        + " and "
                        + TradeItemStoreTable.StoreType + "=" + TradeItemStore.ITEMIN_STORE_TYPE
                      );
                  
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                       
                        TradeStorePlace place
                            = new TradeStorePlaceSQL (DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[i][0].ToString()));
                        double  amount = Convert.ToDouble (t.Rows[i][1].ToString());
                        
                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunit_id = Convert.ToUInt32(t.Rows[i][2].ToString());
                            consumeunit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunit_id);
                        }
                        catch
                        {
                            consumeunit = new ConsumeUnit (0, ItemIN_._Item.DefaultConsumeUnit, ItemIN_._Item,1);
                        }
                     

                        storeditems.Add(new TradeItemStore(place, ItemIN_, amount, consumeunit));

                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_ItemIN_StoredPlaces:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                
                return storeditems;
            }
            //internal double getNON_StoredAmount(ItemIN ItemIN_)
            //{
          
            //    double storedamount = 0;
            //    try
            //    {
                    
            //       DataTable t=  DB.GetData ("Select "
            //            + TradeItemStoreTable .Amount 
            //            +","
            //            +TradeItemStoreTable.ConsumeUnitID
            //            + " from   "
            //        + TradeItemStoreTable.TableName
            //        + " where "
            //       + TradeItemStoreTable.ItemSourceOPRID + "=" + ItemIN_.ItemINID 
            //       + " and "
            //       + TradeItemStoreTable.StoreType + "=" + TradeItemStore.ITEMIN_STORE_TYPE
            //        );
            //        for(int i=0;i<t.Rows .Count;i++)
            //        {
            //            double amount=Convert.ToDouble(t.Rows[i][0].ToString());
            //            double factor ;
            //            try
            //            {
            //                ConsumeUnit consumuint = new ConsumeUnitSql(DB).GetConsumeAmountinfo(Convert.ToUInt32(t.Rows[i][1].ToString()));
            //                factor = consumuint.Factor;
            //            }catch 
            //            {
            //                factor = 1;
            //            }
            //            storedamount += amount * ItemIN_._ConsumeUnit.Factor / factor;

            //        }
            //        return (ItemIN_.Amount - storedamount);           
            //    }
            //    catch 
            //    {
            //        return ItemIN_.Amount;
            //    }
            //}
            public TradeStorePlace  GetMaintenanceStorePlace(UInt32 maintenenaceoprid)
            {
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeItemStoreTable.PlaceID 
                        + " from   "
                        + TradeItemStoreTable.TableName
                        + " where "
                        + TradeItemStoreTable.ItemSourceOPRID + "=" + maintenenaceoprid
                        + " and "
                        + TradeItemStoreTable.StoreType + "=" + TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE
                      );

                   if(t.Rows .Count ==1)
                    {
                        TradeStorePlace place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        return place;
                    }
                   else  return null ;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetMaintenanceStorePlace" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public TradeStorePlace GetAccessoryStorePlace(UInt32 AccessoryID)
            {
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeItemStoreTable.PlaceID
                        + " from   "
                        + TradeItemStoreTable.TableName
                        + " where "
                        + TradeItemStoreTable.ItemSourceOPRID + "=" + AccessoryID
                        + " and "
                        + TradeItemStoreTable.StoreType + "=" + TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE
                      );

                    if (t.Rows.Count == 1)
                    {
                        TradeStorePlace place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        return place;
                    }
                    return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetAccessoryStorePlace" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }

            internal void Clear_Places()
            {
                try
                {
                    DB.ExecuteSQLCommand("delete from   "
                        + TradeItemStoreTable.TableName

                      );
                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.DELETE
                                     , DatabaseInterface.Log.Log_Target.Trade_ItemsStore
                                      , ""
                                      , true, "");
                    MessageBox.Show("h");
                }
                catch (Exception ee)
                {
                    MessageBox.Show("n");
                }
            }
        }
 
        
    }
}
