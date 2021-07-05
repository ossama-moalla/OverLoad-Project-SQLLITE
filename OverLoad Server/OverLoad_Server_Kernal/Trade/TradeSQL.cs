

using OverLoad_Server_Kernal.AccountingSQL;
using OverLoad_Server_Kernal.CompanySQL;
using OverLoad_Server_Kernal.ItemObjSQL;
using OverLoad_Server_Kernal.MaintenanceSQL;
using OverLoad_Server_Kernal.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OverLoad_Server_Kernal
{
    namespace TradeSQL
    {
        public class OperationSQL
        {

            DatabaseInterface DB;
            public OperationSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public Currency GetOperationItemINCurrency(Operation operation)
            {
                try
                {

                    switch (operation.OperationType)
                    {

                        case Operation.BILL_BUY:
                            BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(operation.OperationID);
                            return new Currency(billbuy._Currency.CurrencyID, billbuy._Currency.CurrencyName
                                , billbuy._Currency.CurrencySymbol, billbuy.ExchangeRate, billbuy._Currency.ReferenceCurrencyID, billbuy._Currency.Disable);
                        case Operation.ASSEMBLAGE:
                            return new CurrencySQL(DB).GetReferenceCurrency();

                        case Operation.DISASSEMBLAGE:
                            List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(operation);
                            return itemoutlist[0]._ItemIN._INCost._Currency;

                        default:
                            throw new Exception("جلب عملة تكلفة ادخال العنصر: العملية غير صحيحة");


                    }


                }
                catch (Exception ee)
                {
                    throw new Exception("فشل جلب عملة العملية المصدر" + ee.Message);
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
                            BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(operation.OperationID);
                            return BillMaintenance_._Currency;

                       
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
                            BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(operation.OperationID);
                            return BillMaintenance_._Currency;

                        case Operation.REPAIROPR:
                            RepairOPR repairopr= new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(operation.OperationID);
                            BillMaintenance BillMaintenance2_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(repairopr._MaintenanceOPR);
                            return BillMaintenance2_._Currency;
                        case Operation.ASSEMBLAGE:
                            return new CurrencySQL(DB).GetReferenceCurrency();

                        case Operation.DISASSEMBLAGE:
                            return new CurrencySQL(DB).GetReferenceCurrency();

                        case Operation.RavageOPR:
                            return new CurrencySQL(DB).GetReferenceCurrency();
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
                             BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(operation.OperationID);
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
                        BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(Operation_.OperationID);

                        List<BillMaintenance_Clause> BillMaintenance_ClauseList
                            = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_).Where(x => x.Value != null).ToList();


                        return BillMaintenance_ClauseList.Sum(x => Convert.ToDouble(x.Value))-BillMaintenance_.Discount;
                    }
                    else if (OperationType == Operation.RavageOPR)
                        return 0;
                    else throw new Exception("operation type not found");

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_OperationValue:" + ee.Message);
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
                        EmployeePayOrder EmployeePayOrder
                            = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(OperationID);
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
                        BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(OperationID);
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
                    throw new Exception("Get_OperationPaysValue_UPON_OperationCurrency:" + ee.Message);
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
                        return (iteminlist.Sum(x => x._INCost.Value * x.Amount) + BillAdditionalClauselist.Sum(x => x.Value)) - billbuy.Discount;
                    }
                    else if (Operation_.OperationType == Operation.BILL_SELL)
                    {
                        BillSell billsell = new BillSellSQL(DB).GetBillSell_INFO_BYID(Operation_.OperationID);

                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(Operation_);
                        List<BillAdditionalClause> BillAdditionalClauselist = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(Operation_);
                        return (itemoutlist.Sum(x => x._OUTValue.Value * x.Amount) + BillAdditionalClauselist.Sum(x => x.Value)) - billsell.Discount;
                    }
                    else if (Operation_.OperationType == Operation.BILL_MAINTENANCE)
                    {
                        BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(Operation_.OperationID);

                        List<BillMaintenance_Clause> BillMaintenance_ClauseList
                            = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_).Where(x => x.Value != null).ToList();


                        return BillMaintenance_ClauseList.Sum(x => Convert.ToDouble(x.Value)) - BillMaintenance_.Discount;
                    }
                    else if (Operation_.OperationType == Operation.RavageOPR)
                        return 0;
                    else throw new Exception("operation type not found");
                   
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_OperationValue:" + ee.Message);
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
                        EmployeePayOrder EmployeePayOrder
                            = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(Operation_.OperationID);
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
                        BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(Operation_.OperationID);
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
                    else if (Operation_.OperationType == Operation.RavageOPR)
                        return 0;
                    else throw new Exception("operation type not found");

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_OperationPaysValue_UPON_OperationCurrency:" + ee.Message);
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
                        operationlist.Add(ItemOUT_._Operation);
                    }
                    else if (ItemOUT_._Operation.OperationType == Operation.REPAIROPR)
                    {
                        RepairOPR repairopr = new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(ItemOUT_._Operation.OperationID);
                        BillMaintenance BillMaintenance_
                            = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(repairopr._MaintenanceOPR);
                        operationlist.Add(BillMaintenance_._Operation);
                    }
                    else if (ItemOUT_._Operation.OperationType == Operation.ASSEMBLAGE || ItemOUT_._Operation.OperationType == Operation.DISASSEMBLAGE)
                    {
                        List<ItemIN> ItemINList = new ItemINSQL(DB).GetItemINList(ItemOUT_._Operation);
                        for (int i = 0; i < ItemINList.Count; i++)
                        {
                            List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("GetItemOUT_Real_OUTOperations", ItemINList[i]);
                            for (int j = 0; j < ItemOUTList.Count; j++)
                            {
                                operationlist.AddRange(GetItemOUT_Real_OUTOperations(ItemOUTList[j]));
                            }
                        }

                    }
                    else throw new Exception("Operation Not Found");

                }
                catch (Exception ee)
                {

                    throw new Exception("GetItemOUT_Real_OUTOperations" + ee.Message);
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
                    throw new Exception("Get_BillAdditionalClause_INFO_BYID:" + ee.Message);
                    return null;
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
                    throw new Exception("GetBill_AdditionalClauses:" + ee.Message);
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
                    throw new Exception("GetBill_AdditionalClauses:" + ee.Message);
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
            public List<BillBuy> Get_All_BillBuy_List()
            {
                List<BillBuy> list = new List<BillBuy>();
                try
                {

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
                    throw new Exception("Get_All_BillBuy_List:" + ee.Message);
                    return list ;
                }
            }
            public List<BillBuy> Get_Contact_BillBuy_List(Contact  Contact_)
            {
                List<BillBuy> list = new List<BillBuy>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + BillBuyTable.BillBuyID + ","
                    + BillBuyTable.BillDate + ","
                    + BillBuyTable.BillDescription + ","
                    + BillBuyTable.CurrencyID + ","
                    + BillBuyTable.ExchangeRate + ","
                    + BillBuyTable.Discount + ","
                    + BillBuyTable.Notes
                    + " from   "
                    + BillBuyTable.TableName
                     + " where   "
                    + BillBuyTable.ContactID + "=" + Contact_.ContactID

                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                     
                        uint BillID = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime BillDate_ = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string BillDescription_ = t.Rows[i][2].ToString();
                        Currency Currency_ = new CurrencySQL(DB)
                            .GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][3].ToString()));
                        double ExchangeRate_ = Convert.ToDouble(t.Rows[i][4].ToString());
                        double discount = Convert.ToDouble(t.Rows[i][5].ToString());
                        string Notes = t.Rows[i][6].ToString();
                        if (Currency_.ReferenceCurrencyID == null && ExchangeRate_ != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");
                        BillBuy billbuy = new BillBuy(BillID, BillDate_, BillDescription_
                        , Contact_, Currency_,
                            ExchangeRate_, discount, Notes);
                        list.Add(billbuy);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_BillBuy_List:"+ ee.Message);

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

                            double Itemout_value = itemoutlist[j].Amount * itemoutlist[j]._OUTValue.Value;
                            for (int k=0;k<operationlist .Count;k++)
                            {
                                double operation_value = new OperationSQL(DB).Get_OperationValue(operationlist[k]);

                                double factor =  Itemout_value /operation_value ;
                                List<PayIN> temp_payin_list = PayINSQL_.GetPayINList(operationlist[k]);
                                temp_payin_list.ForEach(x => x.Value = x.Value * factor);
                                PayINList.AddRange(temp_payin_list );
                            }
                        } 
                    }
                }
                catch (Exception ee)
                {

                    throw new Exception("Get_Billbuy__Returns_Pays" + ee.Message);
                }
                return PayINList;
            }
        }
        public class ContactSQL
        {

            DatabaseInterface DB;
            
    
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
                    throw new Exception("GetContactList:" + ee.Message);
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
                    throw new Exception("SearchContact:" + ee.Message);
                    return list;
                }
            }
         

            #region ReportDetails
       
            public List<Contact_Pays_ReportDetail> Get_Contact_Pays_ReportDetail(uint ContactID)
            {
                return new DataBaseFunctions(DB).Contact_Get_Pays_ReportDetail(ContactID);
            }
            public List<Contact_MaintenanceOPRs_ReportDetail> Get_Contact_MaintenanceOPRs_ReportDetail(uint ContactID)
            {

                return new DataBaseFunctions(DB).Contact_Get_MaintenanceOPRs_ReportDetail(ContactID);
            }
            public List<Contact_Sells_ReportDetail> Get_Contact_Sells_ReportDetail(uint ContactID)
            {
                return new DataBaseFunctions(DB).Contact_Get_Sells_ReportDetail(ContactID);
            }
            public List<Contact_Buys_ReportDetail> Get_Contact_Buys_ReportDetail(uint ContactID)
            {
                return new DataBaseFunctions(DB).Contact_Get_Buys_ReportDetail(ContactID);
            }
            #endregion
            #region Report
            //public static class Contact_Pays_Report_Table
            //{

            //    public const string TableName = "[dbo].[Contact_Pays_GetReport]";
            //    public const string CurrencyID = "CurrencyID";
            //    public const string Currency = "Currency";
            //    public const string PayIN_Sell = "PayIN_Sell";
            //    public const string PayIN_Maintenance = "PayIN_Maintenance";
            //    public const string PayOUT_Buy = "PayOUT_Buy";
            //}
            //public static class Contact_Sells_Report_Table
            //{
                
            //    public const string TableName = "[dbo].[Contact_Sell_GetReport]";
            //    public const string Bills_Count = "Bills_Count";
            //    public const string Bills_Value = "Bills_Value";
            //    public const string Bills_Pays_Value = "Bills_Pays_Value";
            //    public const string Bills_Pays_Remain = "Bills_Pays_Remain";
            //    public const string Bills_Pays_Remain_UPON_BillsCurrency = "Bills_Pays_Remain_UPON_BillsCurrency";
            //    public const string Bills_ItemsIN_Value = "Bills_ItemsIN_Value";
            //    public const string Bills_ItemsIN_RealValue = "Bills_ItemsIN_RealValue";
            //    public const string Bills_RealValue = "Bills_RealValue";
            //    public const string Bills_Pays_RealValue = "Bills_Pays_RealValue";

            //}
            //public static class Contact_Buys_Report_Table
            //{

            //    public const string TableName = "[dbo].[Contact_Buy_GetReport]";
            //    public const string Bills_Count = "Bills_Count";
            //    public const string Bills_Amounts_IN = "Bills_Amounts_IN";
            //    public const string Bills_Amounts_Remain = "Bills_Amounts_Remain";
            //    public const string Bills_Value = "Bills_Value";
            //    public const string Bills_Pays_Value = "Bills_Pays_Value";
            //    public const string Bills_Pays_Remain = "Bills_Pays_Remain";
            //    public const string Bills_Pays_Remain_UPON_BillsCurrency = "Bills_Pays_Remain_UPON_BillsCurrency";

            //    public const string Bills_RealValue = "Bills_RealValue";
            //    public const string Bills_Pays_RealValue = "Bills_Pays_RealValue";
            //    public const string Bills_ItemsOut_Value = "Bills_ItemsOut_Value";
            //    public const string Bills_ItemsOut_RealValue = "Bills_ItemsOut_RealValue";
            //    public const string Bills_Pays_Return_Value = "Bills_Pays_Return_Value";
            //    public const string Bills_Pays_Return_RealValue = "Bills_Pays_Return_RealValue";

            //}
            //public static class Contact_MaintenanceOPRs_Report_Table
            //{

            //    public const string TableName = "[dbo].[Contact_MaintenanceOPR_GetReport]";

            //    public const string MaintenanceOPRs_Count = "MaintenanceOPRs_Count";
            //    public const string MaintenanceOPRs_EndWork_Count = "MaintenanceOPRs_EndWork_Count";
            //    public const string MaintenanceOPRs_Repaired_Count = "MaintenanceOPRs_Repaired_Count";
            //    public const string MaintenanceOPRs_Warranty_Count = "MaintenanceOPRs_Warranty_Count";
            //    public const string MaintenanceOPRs_Endarranty_Count = "MaintenanceOPRs_Endarranty_Count";
            //    public const string BillMaintenances_Count = "BillMaintenances_Count";
            //    public const string BillMaintenances_Value = "BillMaintenances_Value";
            //    public const string BillMaintenances_Pays_Value = "BillMaintenances_Pays_Value";
            //    public const string BillMaintenances_Pays_Remain = "BillMaintenances_Pays_Remain";

            //    public const string BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = "BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency";

            //    public const string BillMaintenances_ItemsOut_Value = "BillMaintenances_ItemsOut_Value";
            //    public const string BillMaintenances_ItemsOut_RealValue = "BillMaintenances_ItemsOut_RealValue";
            //    public const string BillMaintenances_RealValue = "BillMaintenances_RealValue";
            //    public const string BillMaintenances_Pays_RealValue = "BillMaintenances_Pays_RealValue";


            //}
            public Contact_MaintenanceOPRs_Report Get_Contact_MaintenanceOPRs_Report(uint ContactID)
            {
                return new DataBaseFunctions(DB).Contact_Get_Report_Maintenance(ContactID);
            }
            public Contact_Sells_Report Get_Contact_Sells_Report(uint ContactID)
            {
                return new DataBaseFunctions(DB).Contact_Get_Report_Sells(ContactID);
            }
            public Contact_Buys_Report Get_Contact_Buys_Report(uint ContactID)
            {
                return new DataBaseFunctions(DB).Contact_Get_Report_Buys(ContactID);
            }
            
            public List<Contact_PayCurrencyReport> Get_Contact_PayCurrencyReport(uint ContactID)
            {
                return new DataBaseFunctions(DB).Contact_Get_Report_Pays(ContactID);
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
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw new Exception("حدث خطأ اثناء الاتصال بقاعدة البيانات", sqlEx);
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
                    throw new Exception("فشل جلب قائمة علاقات العناصر:"+ee.Message);
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
                    throw new Exception("GetTradeStateList:" + ee.Message);
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
                    throw new Exception("GetItemININFO_BYID" + ee.Message);
                    return null;
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
                    throw new Exception("DeleteItemINListForOperation" + ee.Message);
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
                    throw new Exception("ItemIN_ItemOUTReportList:"+ee.Message );
                }
                return ItemIN_ItemOUTReportList;
            }
            private INCost GetItemINCost(uint iteminid)
            {
                try
                {
                    DataTable t = new DataTable();
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
                        case Operation.BILL_BUY:

                            double value = Convert.ToDouble(t.Rows[0][ItemINTable.Cost].ToString());
                            BillBuy billbuy = new BillBuySQL(DB).GetBillBuy_INFO_BYID(_Operation.OperationID);
                            return new INCost(value, billbuy._Currency, billbuy.ExchangeRate);

                        case Operation.ASSEMBLAGE:
                            //AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_Operation.OperationID);
                            List<ItemOUT> assemblage_itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(_Operation);
                            double assemblage_itemincost = 0;
                            for (int i = 0; i < assemblage_itemoutlist.Count; i++)
                            {

                                INCost incost = GetItemINCost(assemblage_itemoutlist[i]._ItemIN.ItemINID);

                                assemblage_itemincost += (incost.Value / incost.ExchangeRate) * assemblage_itemoutlist[i].Amount;
                            }
                            double amount = Convert.ToDouble(t.Rows[0][ItemINTable.Amount].ToString());

                            return new INCost(assemblage_itemincost / amount, referense_curruncy, 1);

                        case Operation.DISASSEMBLAGE:
                            DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_Operation.OperationID);
                            return new INCost(Convert.ToDouble(t.Rows[0][2].ToString()), DisAssemblabgeOPR_._ItemOUT._OUTValue._Currency, DisAssemblabgeOPR_._ItemOUT._OUTValue.ExchangeRate);

                        default:
                            throw new Exception("عملية ادخال عنصر خاطئة");
                    }

                }
                catch (Exception ee)
                {
                    throw new Exception("GetItemINCost" + ee.Message);
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
                    throw new Exception("GetItemINList" + ee.Message);
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
                    throw new Exception("GetItemINList" + ee.Message);
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
                    throw new Exception("GetItemINList_ForItem" + ee.Message);
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
                    throw new Exception("ITEMINSQL-GetItemIN_StoreReportList:" + ee.Message);

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

                            BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(_Operation.OperationID);
                            return new OUTValue(Convert.ToDouble(t.Rows[0][2].ToString()), BillMaintenance_._Currency, BillMaintenance_.ExchangeRate);
                        case Operation.REPAIROPR:
                            RepairOPR RepairOPR_ = new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(_Operation.OperationID);
                            BillMaintenance BillMaintenance2_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(RepairOPR_._MaintenanceOPR);
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
                    throw new Exception("GetOUTValue" + ee.Message);
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
                    throw new Exception("GetItemOUTINFO_BYID" + ee.Message);
                    return null;
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
                    throw new Exception("GetItemOUTList:" + ee.Message);
                  
                }
                return ItemOUTList;
            }

            //public List<Money_Currency> GetItemOUTList_AS_Money_Currency(Operation operation)
            //{
            //    try
            //    {
            //        List<Money_Currency> ItemOUTList = new List<Money_Currency>();

            //        DataTable t = new DataTable();

            //        t = DB.GetData("select "
            //         + ItemOUTTable.ItemOUTID
            //         + " from   "
            //        + ItemOUTTable.TableName
            //        + " where "
            //        + ItemOUTTable.OperationType + "=" + operation.OperationType
            //        + " and "
            //        + ItemOUTTable.OperationID + "=" + operation.OperationID
            //          );

            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {
            //            uint itemoutid = Convert.ToUInt32(t.Rows [i][0]);
            //            OUTValue OUTValue_ = GetOUTValue(itemoutid);
            //            ItemOUTList.Add(new Money_Currency (OUTValue_._Currency ,OUTValue_.Value,OUTValue_.ExchangeRate) );

            //        }
            //        return ItemOUTList;
            //    }
            //    catch (Exception ee)
            //    {
            //        throw new Exception("GetItemOUTList" + ee.Message);
            //        return null;
            //    }
            //}

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
                    throw new Exception("GetItemsOUT_Count" + ee.Message);
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
                    throw new Exception("GetItemIN_ItemOUTList:" + ee.Message);
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
                    throw new Exception(ee.Message); return null;
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
                    throw new Exception("فشل جلب اسعار العنصر:"+ ee.Message);
                    return null;
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
                    throw new Exception("GetAvailabeAmount_by_Place:" + ee.Message);
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
                    throw new Exception("GetSpentAmount_by_Place:" + ee.Message);

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
                    throw new Exception("GetAvailabeAmount_by_ItemIN:" + ee.Message);

                }


            }

            public string Get_AvailabeAmount_For_Item(uint userid, uint itemid)
            {
                string amount = "";
                try
                {
                    List<ItemIN_AvailableAmount_Report> ItemIN_AvailableAmount_Reportlist = Get_ItemIN_AvailableAmount_Report_List_By_Item(userid,itemid);
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
                    throw new Exception("Get_AvailabeAmount_For_Item:" + ee.Message);
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
                    throw new Exception("Get_AvailabeAmount_For_Item:" + ee.Message);
                    return "";
                }
            }
            public List<PlaceAvailbeItems_ItemDetails> GetStored_TradeItems(TradeStorePlace place)
            {
  

                List<PlaceAvailbeItems_ItemDetails> PlaceAvailbeItems_ItemDetails_List = new List<PlaceAvailbeItems_ItemDetails>();
                try
                {

                    List<TradeStoreItems_AvailableAmount_Report> TradeItemStorelist = new TradeItemStoreSQL(DB).GetItemsStoredINPlace(place).Where (x=>x._TradeItemStore .StoreType==TradeItemStore.ITEMIN_STORE_TYPE).ToList ();
                    List<uint> itemid_list = TradeItemStorelist.Select(x => x._TradeItemStore ._ItemIN._Item.ItemID).ToList();

                    for (int i = 0; i < itemid_list.Count; i++)
                    {
                        Item item = TradeItemStorelist.Where(x => x._TradeItemStore._ItemIN._Item.ItemID == itemid_list[i]).ToList()[0]._TradeItemStore ._ItemIN._Item;

                        PlaceAvailbeItems_ItemDetails_List.Add(new PlaceAvailbeItems_ItemDetails(place, item, Get_AvailabeAmount_For_Item_IN_Place(item.ItemID,place.PlaceID )));
                    }

                    return PlaceAvailbeItems_ItemDetails_List;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetStored_TradeItems:" + ee.Message);
                    return PlaceAvailbeItems_ItemDetails_List;
                }
            }

            internal List<ItemIN_AvailableAmount_Report> Get_ItemIN_AvailableAmount_Report_List_By_Item(uint userid,uint itemid)
            {
                try
                {
                    return Get_ItemIN_AvailableAmount_Report_List(userid).Where(x => x.ItemID == itemid).ToList();
        
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_ItemIN_AvailableAmount_Report_List_By_Item:" + ee.Message);
                }
            }
          internal List <ItemIN_AvailableAmount_Report> Get_ItemIN_AvailableAmount_Report_List(uint userid)
            {
                List<ItemIN_AvailableAmount_Report> ItemIN_AvailableAmount_Report_List = new List<ItemIN_AvailableAmount_Report>();
                try
                {
                    List<Folder> foldersList = new FolderSQL(DB).Get_User_Allowed_Folders(userid);
                    bool Is_Belong_To_Admin_Group = DB.IS_Belong_To_Admin_Group(userid);
                    List<ItemIN> iteminlist = new ItemINSQL(DB).Get_ALL_ItemIN_List() ;

                    for (int i = 0; i < iteminlist.Count; i++)
                    {
                        if (!Is_Belong_To_Admin_Group) if (foldersList.Where(x => x.FolderID == iteminlist[i]._Item .folder .FolderID ).ToList().Count == 0) continue;

                        double Available_amount = Get_AvailabeAmount_by_ItemIN(iteminlist[i]);
                        if (Available_amount > 0)
                            ItemIN_AvailableAmount_Report_List.Add(new ItemIN_AvailableAmount_Report(iteminlist[i]._Operation .OperationType, iteminlist[i]._Operation .OperationID
                                , iteminlist[i].ItemINID, (iteminlist[i]._ConsumeUnit ==null ? iteminlist[i]._Item .DefaultConsumeUnit : iteminlist[i]._ConsumeUnit .ConsumeUnitName )
                                , iteminlist[i].Amount , iteminlist[i]._Item.ItemID, iteminlist[i]._Item.ItemName, iteminlist[i]._Item.ItemCompany
                                , iteminlist[i]._Item.folder .FolderID , iteminlist[i]._Item.folder .FolderName,new FolderSQL (DB).GetFolderPath (iteminlist[i]._Item.folder )
                                , iteminlist[i]._TradeState .TradeStateID , iteminlist[i]._TradeState .TradeStateName , Available_amount));
                    }
                    return ItemIN_AvailableAmount_Report_List;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_ItemIN_AvailableAmount_Report_List:" + ee.Message);
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
                    throw new Exception("Get_ItemIN_AvailableAmount_Report_PlaceDetail_List:" + ee.Message);
                }
            }
            internal List<Item_AvailableAmount_Report> Get_Item_AvailableAmount_Report_List(uint UserID)
            {
                try
                {
                    List<Item_AvailableAmount_Report> Item_AvailableAmount_Report_List = new List<Item_AvailableAmount_Report>();


                    List<Item> ItemList = new ItemSQL(DB).Get_ALL_Item_List(UserID);
                    List<ItemIN_AvailableAmount_Report> ItemIN_AvailableAmount_Reportlist = Get_ItemIN_AvailableAmount_Report_List(UserID);
                    List<TradeState> TradeStateList = new TradeStateSQL(DB).GetTradeStateList();
                    for (int i = 0; i < ItemList.Count; i++)
                    {
                        List<ItemIN_AvailableAmount_Report> Temp_List = ItemIN_AvailableAmount_Reportlist.Where(x => x.ItemID == ItemList[i].ItemID).ToList();
                        string FolderPath = new FolderSQL(DB).GetFolderPath(ItemList[i].folder);
                        for (int j = 0; j < TradeStateList.Count; j++)
                        {
                            double Available_amount = Temp_List.Where(x => x.TradeStateID == TradeStateList[j].TradeStateID).Sum(y => y.AvailableAmount);
                            Item_AvailableAmount_Report_List.Add(new Item_AvailableAmount_Report(ItemList[i].ItemID, ItemList[i].ItemName 
                                , ItemList[i].ItemCompany , ItemList[i].folder .FolderID , ItemList[i].folder .FolderName 
                                , FolderPath ,TradeStateList [j].TradeStateID ,TradeStateList [j].TradeStateName , Available_amount));
                        }
                    }
                    return Item_AvailableAmount_Report_List;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Item_AvailableAmount_Report_List:" + ee.Message);
                }
            }
            internal List<Item_AvailableAmount_Report> Get_Item_AvailableAmount_Report_IN_Place_List(TradeStorePlace place)
            {
                List<Item_AvailableAmount_Report> Item_AvailableAmount_Report_List = new List<Item_AvailableAmount_Report>();
                try
                {
   
                    List<ItemIN_AvailableAmount_Report_PlaceDetail> ItemIN_AvailableAmount_Reportlist = Get_ItemIN_AvailableAmount_Report_PlaceDetail_List().Where (x=>x.Place !=null &&x.Place .PlaceID ==place.PlaceID).ToList ();
                    List<TradeState> TradeStateList = new TradeStateSQL(DB).GetTradeStateList();
                    List<uint> itemidlist = ItemIN_AvailableAmount_Reportlist.Select(x => x._ItemIN._Item.ItemID).ToList();
                    for (int i = 0; i < itemidlist.Count; i++)
                    {
                        List<ItemIN_AvailableAmount_Report_PlaceDetail> Temp_List = ItemIN_AvailableAmount_Reportlist.Where(x => x._ItemIN._Item.ItemID == itemidlist[i]).ToList();
                        Item item = Temp_List[0]._ItemIN._Item;
                        string FolderPath = new FolderSQL(DB).GetFolderPath(item.folder);
                        for (int j = 0; j < TradeStateList.Count; j++)
                        {
                            double Available_amount = Temp_List.Where(x => x._ItemIN._TradeState.TradeStateID == TradeStateList[j].TradeStateID).Sum(y => y.AvailableAmount);

                            Item_AvailableAmount_Report_List.Add(new Item_AvailableAmount_Report(item.ItemID, item.ItemName, item.ItemCompany
                                , item.folder.FolderID, item.folder.FolderName, FolderPath, TradeStateList[j].TradeStateID
                                , TradeStateList[j].TradeStateName, Available_amount));
                        }
                    }
                    return Item_AvailableAmount_Report_List;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Item_AvailableAmount_Report_IN_Place_List:" + ee.Message);
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
                        throw new Exception("GetParentContainer:"+ee.Message );
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
                    throw new Exception("AddContainer" + ee.Message);
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
                    throw new Exception("AddContainer" + ee.Message);
                    return null ;
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
                    throw new Exception("فشل جلب الحاويات الابناء" + ee.Message);
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
                    throw new Exception(ee.Message + ":فشل جلب بيانات مكان التخزين");
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
                    throw new Exception(ee.Message + ":فشل جلب بيانات مكان التخزين");
                    return null;
                }

            }

            public List<TradeStorePlace > GetPlacesINContainer(TradeStoreContainer container)
            {
                try
                {
                 
                    List<TradeStorePlace> conainerPlaces_list = new List<TradeStorePlace>();
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
                        string desc = t.Rows[0][2].ToString();



                        conainerPlaces_list.Add(new TradeStorePlace (placeid , placename , container, desc));
                    }
                    return conainerPlaces_list;
                }
                catch (Exception ee)
                {
                    throw new Exception(ee.Message+ ":فشل جلب اماكن التخزين في الحاوية");
                    return null;
                }
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
                    throw new Exception("SearchPlace" + ee.Message);
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
                    throw new Exception("SearchItemINFolder:" + ee.Message);
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
                    throw new Exception("GetBillIN_INFO_BYID" + ee.Message);
                    return null;
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
                    throw new Exception("GetBillSellValue:" + ee.Message);
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
                    throw new Exception("GetBillSellValue:" + ee.Message);
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
                    throw new Exception("GetAllBillSell:" + ee.Message);

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
                            Part_ = new PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[0][1].ToString()));

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
                    throw new Exception("GetRavageOPR_INFO_BYID" + ee.Message);
                    return null;
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
                            Part_ = new PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[i][2].ToString()));

                        }
                        catch
                        {
                            Part_ = null;
                        }
                        string notes = t.Rows[i][3].ToString();
                        list .Add ( new RavageOPR(RavageOPRid, RavageOPRindate, Part_, 0, notes));
                    }

                }
                catch (Exception ee)
                {
                    throw new Exception("GetAllRavageOPR:" + ee.Message);

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
                                MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(ItemSourceOPRID_);
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
                                MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ = new MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(ItemSourceOPRID_);
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
                    throw new Exception("TradeItemStoreSQL-GetTradeItemStoreINFO:" + ee.Message);
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
                    throw new Exception("TradeItemStoreSQL-IS_ItemStoredInPlace" + ee.Message);

                    return false; 
                }
            }  
            public List<ItemIN > Get_Stored_ItemIN_INPlace(TradeStorePlace   Place)
            {
                List<ItemIN> ItemINList = new List<ItemIN>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + TradeItemStoreTable.ItemSourceOPRID + ","
                         + TradeItemStoreTable.Amount + ","
                        + TradeItemStoreTable.ConsumeUnitID
                        + " from   "
                        + TradeItemStoreTable.TableName
                        + " where "
                         + TradeItemStoreTable.PlaceID + "=" + Place.PlaceID
                          + " and "
                         + TradeItemStoreTable.StoreType + "=" + TradeItemStore.ITEMIN_STORE_TYPE
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint itemsourceopr_id = Convert.ToUInt32(t.Rows[i][0].ToString());
                        ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(itemsourceopr_id);
                        if(new AvailableItemSQL (DB).Get_AvailabeAmount_by_Place(itemin, Place) >0)
                            ItemINList.Add (itemin);
                       

                    }

                }
                catch (Exception ee)
                {
                    throw new Exception("TradeItemStoreSQL-GetItemsStoredINPlace" + ee.Message);
                    
                }
                return ItemINList;
            }
            internal List <TradeItemStore_Report> Get_TradeItemStore_Report_List()
            {
                try
                {

                    List<TradeItemStore_Report> list = new List<TradeItemStore_Report>();

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                          + TradeItemStoreTable.PlaceID + ","
                             + TradeItemStoreTable.ItemSourceOPRID + ","
                               + TradeItemStoreTable.StoreType + ","
                              + TradeItemStoreTable.Amount + ","
                             + TradeItemStoreTable.ConsumeUnitID
                             + " from   "
                             + TradeItemStoreTable.TableName

                              );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint PlaceID = Convert.ToUInt32(t.Rows[i][0].ToString());
                        TradeStorePlace Place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(PlaceID);
                        uint itemsourceopr_id = Convert.ToUInt32(t.Rows[i][1].ToString());
                        uint storetype = Convert.ToUInt32(t.Rows[i][2].ToString());
                        double amount = Convert.ToDouble(t.Rows[i][3].ToString());

                        ConsumeUnit consumeunit;
                        try
                        {
                            uint consumeunit_id = Convert.ToUInt32(t.Rows[i][4].ToString());
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
                                double availabe_amount = new AvailableItemSQL(DB).Get_AvailabeAmount_by_Place(itemin, Place);
                                double spent_amount = new AvailableItemSQL(DB).Get_SpentAmount_by_Place(itemin, Place);

                                if (consumeunit == null)
                                    consumeunit = new ConsumeUnit(0, itemin._Item.DefaultConsumeUnit, itemin._Item, 1);
                                if (availabe_amount > 0)
                                {
                                    list.Add(
                                    new TradeItemStore_Report(PlaceID, itemin.ItemINID, storetype
                                     , itemin._Operation.OperationType, itemin._Operation.OperationID
                                     , consumeunit.ConsumeUnitName
                                    , itemin._Item.ItemID, itemin._Item.folder.FolderName, itemin._Item.ItemName,
                                    itemin._Item.ItemCompany, itemin._TradeState.TradeStateID, itemin._TradeState.TradeStateName, availabe_amount, spent_amount));
                                }
                                    
                                break;
                            case TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE:

                                MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(itemsourceopr_id);
                                double maintenaceopr_stillin_store = 0;
                                if (MaintenanceOPR_._MaintenanceOPR_EndWork == null)
                                    maintenaceopr_stillin_store = 1;
                                else if (MaintenanceOPR_._MaintenanceOPR_EndWork.DeliveredDate == null)
                                    maintenaceopr_stillin_store = 1;
                                else
                                    maintenaceopr_stillin_store = 0;
                                if (consumeunit == null)
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_._Item.DefaultConsumeUnit, MaintenanceOPR_._Item, 1);
                                if (maintenaceopr_stillin_store > 0)
                                {
                                    list.Add(   new TradeItemStore_Report
                                        (PlaceID, MaintenanceOPR_._Operation.OperationID, storetype
                                        , MaintenanceOPR_._Operation.OperationType , MaintenanceOPR_._Operation.OperationID
                                        , consumeunit.ConsumeUnitName
                                        , MaintenanceOPR_._Item.ItemID, MaintenanceOPR_._Item.folder.FolderName, MaintenanceOPR_._Item.ItemName,
                                         MaintenanceOPR_._Item.ItemCompany, 0, "", maintenaceopr_stillin_store, 0));

                                }
                                break;
                            case TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE:
                                MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ = new MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(itemsourceopr_id);
                                double accessory_stillin_store = 0;
                                if (MaintenanceOPR_Accessory_._MaintenanceOPR._MaintenanceOPR_EndWork == null)
                                    accessory_stillin_store = 1;
                                else if (MaintenanceOPR_Accessory_._MaintenanceOPR._MaintenanceOPR_EndWork.DeliveredDate == null)
                                    accessory_stillin_store = 1;
                                else
                                    accessory_stillin_store = 0;
                                if (consumeunit == null)
                                    consumeunit = new ConsumeUnit(0, MaintenanceOPR_Accessory_._Item.DefaultConsumeUnit, MaintenanceOPR_Accessory_._Item, 1);
                                if (accessory_stillin_store > 0)
                                {
                                    list.Add(
                                    new TradeItemStore_Report(PlaceID, MaintenanceOPR_Accessory_.AccessoryID, storetype
                                    , MaintenanceOPR_Accessory_. _MaintenanceOPR._Operation.OperationType

                                    , MaintenanceOPR_Accessory_._MaintenanceOPR._Operation.OperationID
                                    , consumeunit.ConsumeUnitName
                                    , MaintenanceOPR_Accessory_._Item.ItemID, MaintenanceOPR_Accessory_._Item.folder.FolderName, MaintenanceOPR_Accessory_._Item.ItemName,
                                    MaintenanceOPR_Accessory_._Item.ItemCompany, 0, "", accessory_stillin_store, 0));

                                }
                                break;
                        }
                    }


                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_TradeItemStore_Report_List:" + ee.Message);
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

                                MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(itemsourceopr_id);
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
                                MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ = new MaintenanceAccessorySQL (DB).Get_Accessory_INFO_BYID(itemsourceopr_id);
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
                    throw new Exception("TradeItemStoreSQL-GetItemsStoredINPlace" + ee.Message);
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
                                MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(itemsourceopr_id);
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
                                MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ = new MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(itemsourceopr_id);
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
                    throw new Exception("TradeItemStoreSQL-GetItemsStoredINPlace" + ee.Message);
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
                    throw new Exception("Get_ItemIN_StoredPlaces:" + ee.Message);

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
                    return null ;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMaintenanceStorePlace" + ee.Message);
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
                    throw new Exception("GetAccessoryStorePlace" + ee.Message);
                    return null;
                }
            }

        }
        public class IndustrySQL
        {
            DatabaseInterface DB;

            public IndustrySQL(DatabaseInterface db)
            {
                DB = db;

            }
            public List<Industrial_OPR> GetIndustrial_Operations()
            {
                List<Industrial_OPR> Industrial_OPRList = new List<Industrial_OPR>();
                try
                {
                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    List<DisAssemblabgeOPR> DisAssemblabgeOPR_List = new DisAssemblageSQL(DB).Get_All_DisAssemblageOPR();
                    for (int i = 0; i < DisAssemblabgeOPR_List.Count; i++)
                    {
                        Money_Currency Money_Currency_ = null;
                        Item Item_ = null;
                        double? Amount = null;
                        ConsumeUnit consumeunit = null;
                        string TradeStateName = "-";
                        if (DisAssemblabgeOPR_List[i]._ItemOUT != null)
                        {
                            Money_Currency_ = new Money_Currency(DisAssemblabgeOPR_List[i]._ItemOUT._OUTValue._Currency,
                                DisAssemblabgeOPR_List[i]._ItemOUT._OUTValue.Value, DisAssemblabgeOPR_List[i]._ItemOUT._OUTValue.ExchangeRate);
                            Item_ = DisAssemblabgeOPR_List[i]._ItemOUT._ItemIN._Item;
                            Amount = DisAssemblabgeOPR_List[i]._ItemOUT.Amount;
                            consumeunit = DisAssemblabgeOPR_List[i]._ItemOUT._ConsumeUnit;
                            TradeStateName = DisAssemblabgeOPR_List[i]._ItemOUT._ItemIN._TradeState.TradeStateName;
                        }
                        Industrial_OPRList.Add(new Industrial_OPR(DisAssemblabgeOPR_List[i]._Operation, DisAssemblabgeOPR_List[i].OprDate
                            , DisAssemblabgeOPR_List[i].OprDesc, Amount, consumeunit,
                          Item_, TradeStateName, Money_Currency_, ""));

                    }
                    List<AssemblabgeOPR> AssemblabgeOPR_List = new AssemblageSQL(DB).Get_All_AssemblageOPR();
                    for (int i = 0; i < AssemblabgeOPR_List.Count; i++)
                    {
                        Money_Currency Money_Currency_ = null;
                        Item Item_ = null;
                        double? Amount = null;
                        ConsumeUnit consumeunit = null;
                        string TradeStateName = "-";
                        if (AssemblabgeOPR_List[i]._ItemIN != null)
                        {
                            Money_Currency_ = new Money_Currency(AssemblabgeOPR_List[i]._ItemIN._INCost._Currency,
                                AssemblabgeOPR_List[i]._ItemIN._INCost.Value, AssemblabgeOPR_List[i]._ItemIN._INCost.ExchangeRate);
                            Item_ = AssemblabgeOPR_List[i]._ItemIN._Item;
                            Amount = AssemblabgeOPR_List[i]._ItemIN.Amount;
                            consumeunit = AssemblabgeOPR_List[i]._ItemIN._ConsumeUnit;
                            TradeStateName = AssemblabgeOPR_List[i]._ItemIN._TradeState.TradeStateName;
                        }
                        Industrial_OPRList.Add(new Industrial_OPR(AssemblabgeOPR_List[i]._Operation, AssemblabgeOPR_List[i].OprDate
                            , AssemblabgeOPR_List[i].OprDesc, Amount, consumeunit,
                          Item_, TradeStateName, Money_Currency_, ""));

                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("GetIndustrial_Operations:" + ee.Message);
                    return null;
                }
                return Industrial_OPRList;
            }
        }
        public class AssemblageSQL
        {
            DatabaseInterface DB;
            private static class AssemblageTable
            {
                public const string TableName = "Trade_Assemblage";
                public const string AssemblageID = "AssemblageID";
                public const string OprDate = "OprDate";
                public const string OprDesc = "OprDesc";
                public const string Notes = "Notes";
                //public const string CurrencyID = "CurrencyID";
                //public const string ExchangeRate = "ExchangeRate";

            }
            public AssemblageSQL(DatabaseInterface db)
            {
                DB = db;

            }

            public AssemblabgeOPR GetAssemblageOPR_INFO_BYID(uint oprid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + AssemblageTable.OprDate + ","
                    + AssemblageTable.OprDesc + ","
                    + AssemblageTable.Notes
                    + " from   "
                    + AssemblageTable.TableName
                    + " where "
                    + AssemblageTable.AssemblageID + "=" + oprid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[0][0]);
                        string oprdesc = t.Rows[0][1].ToString();
                        string notes = t.Rows[0][2].ToString();

                        Operation operation = new Operation(Operation.ASSEMBLAGE, oprid);
                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(operation);
                        ItemIN ItemIN_ = null;
                        if (iteminlist.Count > 0)
                            ItemIN_ = iteminlist[0];
                        return new AssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemIN_);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetAssemblageOPR_INFO_BYID" + ee.Message);
                    return null;
                }

            }
            public List<AssemblabgeOPR> Get_All_AssemblageOPR()
            {
                List<AssemblabgeOPR> list = new List<AssemblabgeOPR>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + AssemblageTable.OprDate + ","
                    + AssemblageTable.OprDesc + ","
                    + AssemblageTable.Notes + ","
                    + AssemblageTable.AssemblageID
                    + " from   "
                    + AssemblageTable.TableName
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[i][0]);
                        string oprdesc = t.Rows[i][1].ToString();
                        string notes = t.Rows[i][2].ToString();
                        uint oprid = Convert.ToUInt32(t.Rows[i][3].ToString());
                        Operation operation = new Operation(Operation.ASSEMBLAGE, oprid);

                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(operation);
                        ItemIN ItemIN_ = null;
                        if (iteminlist.Count > 0)
                            ItemIN_ = iteminlist[0];
                        list.Add(new AssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemIN_));

                    }

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_All_AssemblageOPR:" + ee.Message);

                }
                return list;
            }
        }
        public class DisAssemblageSQL
        {
            DatabaseInterface DB;
            private static class DisAssemblageTable
            {
                public const string TableName = "Trade_DisAssemblage";
                public const string DisAssemblageID = "DisAssemblageID";
                public const string OprDate = "OprDate";
                public const string OprDesc = "OprDesc";

                public const string Notes = "Notes";


            }
            public DisAssemblageSQL(DatabaseInterface db)
            {
                DB = db;

            }

            public DisAssemblabgeOPR GetDisAssemblageOPR_INFO_BYID(uint oprid)
            {
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + DisAssemblageTable.OprDate + ","
                     + DisAssemblageTable.OprDesc + ","
                    + DisAssemblageTable.Notes
                    + " from   "
                    + DisAssemblageTable.TableName
                    + " where "
                    + DisAssemblageTable.DisAssemblageID + "=" + oprid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[0][0]);
                        string oprdesc = t.Rows[0][1].ToString();
                        string notes = t.Rows[0][2].ToString();
                        Operation operation = new Operation(Operation.DISASSEMBLAGE, oprid);
                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(operation);
                        ItemOUT ItemOUT_ = null;
                        if (itemoutlist.Count > 0)
                            ItemOUT_ = itemoutlist[0];
                        return new DisAssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemOUT_);



                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetAssemblageOPR_INFO_BYID" + ee.Message);
                    return null;
                }

            }
            public List<DisAssemblabgeOPR> Get_All_DisAssemblageOPR()
            {
                List<DisAssemblabgeOPR> list = new List<DisAssemblabgeOPR>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + DisAssemblageTable.OprDate + ","
                    + DisAssemblageTable.OprDesc + ","
                    + DisAssemblageTable.Notes + ","
                      + DisAssemblageTable.DisAssemblageID
                    + " from   "
                    + DisAssemblageTable.TableName

                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[i][0]);
                        string oprdesc = t.Rows[i][1].ToString();
                        string notes = t.Rows[i][2].ToString();
                        uint oprid = Convert.ToUInt32(t.Rows[i][3].ToString());
                        Operation operation = new Operation(Operation.DISASSEMBLAGE, oprid);

                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(operation);
                        ItemOUT ItemOUT_ = null;
                        if (itemoutlist.Count > 0)
                            ItemOUT_ = itemoutlist[0];
                        list.Add(new DisAssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemOUT_));

                    }

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_All_DisAssemblageOPR:" + ee.Message);

                }
                return list;
            }
        }

    }
}
