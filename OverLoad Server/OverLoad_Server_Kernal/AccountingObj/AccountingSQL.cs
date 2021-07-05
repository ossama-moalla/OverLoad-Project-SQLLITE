

using OverLoad_Server_Kernal.Objects;
using OverLoad_Server_Kernal.TradeSQL;
using System;
using System.Collections.Generic;
using System.Data;

namespace OverLoad_Server_Kernal
{
    namespace AccountingSQL
    {
        internal class MoneyAccountSQL
        {
            DatabaseInterface DB;

          
            internal MoneyAccountSQL(DatabaseInterface db)
            {
                DB = db;

            }

           

        }

   
        internal class CurrencySQL
        {
            DatabaseInterface DB;
            public  static class CurrencyTable
            {
                internal const string TableName = "Account_Currency";
                internal const string CurrencyID = "CurrencyID";
                internal const string CurrencyName = "CurrencyName";
                internal const string CurrencySymbol = "CurrencySymbol";
                internal const string ReferenceFactor = "ReferenceFactor";
                internal const string ReferenceCurrencyID = "ReferenceCurrencyID";
                internal const string Disable = "Disable";

            }
            internal CurrencySQL(DatabaseInterface db)
            {
                DB = db;
                
            }
            internal Currency GetCurrencyINFO_ByID(uint currencyid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + CurrencyTable.CurrencyName + ","
                     + CurrencyTable.CurrencySymbol + ","
                     + CurrencyTable.ReferenceFactor + ","
                     + CurrencyTable.ReferenceCurrencyID + ","
                     + CurrencyTable.Disable 
                    + " from   "
                    + CurrencyTable.TableName
                    + " where "
                    + CurrencyTable.CurrencyID + "=" + currencyid
                      );
                    if (t.Rows.Count == 1)
                    {
                        string name = t.Rows[0][0].ToString();
                        string symbol = t.Rows[0][1].ToString();
                        double referncefactor = Convert.ToDouble(t.Rows[0][2].ToString());
                        uint? RefCurrencyid;
                        try
                        {
                            RefCurrencyid = Convert.ToUInt32(t.Rows[0][3].ToString());
                        }
                        catch
                        {
                            RefCurrencyid = null;
                        }
                        bool disable;
                        if (Convert.ToInt32(t.Rows[0][4].ToString()) == 0) disable = false;
                        else disable = true;
                        return new Currency(currencyid, name, symbol, referncefactor, RefCurrencyid,disable );

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("فشل جلب بيانات العملة"+ee.Message );

                }
                
            }
            internal Currency GetReferenceCurrency()
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + CurrencyTable.CurrencyID  + ","
                     + CurrencyTable.CurrencyName + ","
                     + CurrencyTable.CurrencySymbol + ","
                      + CurrencyTable.Disable 
                    + " from   "
                    + CurrencyTable.TableName
                    + " where "
                     + CurrencyTable.ReferenceCurrencyID+" is null"
                     +" and "
                     + CurrencyTable.ReferenceFactor  + "=1 "
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint currencyid = Convert.ToUInt32(t.Rows[0][0].ToString());
                        string name = t.Rows[0][1].ToString();
                        string symbol = t.Rows[0][2].ToString();

                        bool disable;
                        if (Convert.ToInt32(t.Rows[0][3].ToString()) == 0) disable = false;
                        else disable = true;
                        return new Currency(currencyid, name, symbol, 1, null,disable  );

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception ("فشل جلبالعملة المرجعية:"+ee.Message );

                }

            }
       
            internal List<Currency> GetCurrencyList()
            {
                List<Currency> currencyList = new List<Currency>();
                try
                {
                   
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + CurrencyTable.CurrencyID  + ","
                     + CurrencyTable.CurrencyName + ","
                     + CurrencyTable.CurrencySymbol + ","
                     + CurrencyTable.ReferenceFactor + ","
                     + CurrencyTable.ReferenceCurrencyID +","
                     + CurrencyTable.Disable 
                    + " from   "
                    + CurrencyTable.TableName
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint currencyid = Convert.ToUInt32(t.Rows[i][0].ToString()); 
                        string name = t.Rows[i][1].ToString();
                        string symbol = t.Rows[i][2].ToString().Replace (" ",string .Empty );
                        double referncefactor = Convert.ToDouble(t.Rows[i][3].ToString());
                        uint? RefCurrencyid ;
                        try
                        {
                            RefCurrencyid = Convert.ToUInt32(t.Rows[i][4].ToString());
                        }
                        catch
                        {
                            RefCurrencyid = null;
                        }
                        bool disable;
                        if (Convert.ToInt32(t.Rows[i][5].ToString()) == 0) disable = false;
                        else disable = true;
                        currencyList .Add ( new Currency(currencyid, name, symbol, referncefactor, RefCurrencyid,disable ));

                    }
                    return currencyList;
                }
                catch (Exception ee)
                {
                   throw new Exception ("فشل جلب قائمة العملات"+ee.Message );
                    return currencyList;
                }

            }
         
         }
        internal class ExchangeOPRSQL
        {

            DatabaseInterface DB;
            public  static class ExchangeOPRTable
            {
                internal const string TableName = "Account_ExchangeOpr";
                internal const string ExchangeOprID = "ExchangeOprID";
                internal const string ExchangeOprDate = "ExchangeOprDate";
                internal const string SourceCurrencyID = "SourceCurrencyID";
                internal const string SourceExchangeRate = "SourceExchangeRate";
                internal const string OutMoneyValue = "OutMoneyValue";
                internal const string TargetCurrencyID = "TargetCurrencyID";
                internal const string TargetExchangeRate = "TargetExchangeRate";
                internal const string Notes = "Notes";
                internal const string MoneyBoxID = "MoneyBoxID";


            }
            internal ExchangeOPRSQL(DatabaseInterface db)
            {
                DB = db;

            }
            internal ExchangeOPR GetExchangeOPR_INFO_BYID(uint exchangeoprid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ExchangeOPRTable.ExchangeOprDate
                        + ","
                        + ExchangeOPRTable.SourceCurrencyID
                        + ","
                        + ExchangeOPRTable.SourceExchangeRate
                        + ","
                        + ExchangeOPRTable.OutMoneyValue
                        + ","
                        + ExchangeOPRTable.TargetCurrencyID
                        + ","
                        + ExchangeOPRTable.TargetExchangeRate
                        + ","
                        + ExchangeOPRTable.Notes
                         + ","
                        + ExchangeOPRTable.MoneyBoxID 
                        + " from   "
                        + ExchangeOPRTable.TableName
                        + " where "
                        + ExchangeOPRTable.ExchangeOprID + "=" + exchangeoprid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime exchangeoprdate = Convert.ToDateTime(t.Rows[0][0].ToString());
                        Currency sourcecurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][1].ToString()));
                        double source_exchangerate = Convert.ToDouble(t.Rows[0][2].ToString());
                        double outmenyvalue = Convert.ToDouble(t.Rows[0][3].ToString());
                        Currency targetcurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                        double target_exchangerate = Convert.ToDouble(t.Rows[0][5].ToString());
                        string notes = t.Rows[0][6].ToString();
                        MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[0][7]));

                        return new ExchangeOPR(moneybox , exchangeoprid, exchangeoprdate, sourcecurrency, source_exchangerate, outmenyvalue, targetcurrency, target_exchangerate, notes);

                    }
                    else
                        return null;
                }
                catch(Exception ee)
                {
                    throw new Exception ("GetExchangeOPR_INFO_BYID:" + ee.Message);

                }
               
            }
      
            internal List<ExchangeOPR> Get_All_ExchangeOPRList(MoneyBox moneybox)
            {
                try
                {
                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID,moneybox ))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    List<ExchangeOPR> ExchangeOPRList = new List<ExchangeOPR >();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + ExchangeOPRTable.ExchangeOprID
                    + ","
                    + ExchangeOPRTable.ExchangeOprDate
                    + ","
                    + ExchangeOPRTable.SourceCurrencyID
                    + ","
                    + ExchangeOPRTable.SourceExchangeRate
                    + ","
                    + ExchangeOPRTable.OutMoneyValue
                    + ","
                    + ExchangeOPRTable.TargetCurrencyID
                    + ","
                    + ExchangeOPRTable.TargetExchangeRate
                    + ","
                    + ExchangeOPRTable.Notes
                    + " from   "
                    + ExchangeOPRTable.TableName
                     + " where   "
                    + ExchangeOPRTable.MoneyBoxID +"="+moneybox.BoxID
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint exchangeoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime exchangeoprdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        Currency sourcecurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        double source_exchangerate = Convert.ToDouble(t.Rows[i][3].ToString());
                        double outmenyvalue = Convert.ToDouble(t.Rows[i][4].ToString());
                        Currency targetcurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        double target_exchangerate = Convert.ToDouble(t.Rows[i][6].ToString());
                        string notes = t.Rows[i][7].ToString();

                        ExchangeOPRList.Add (new ExchangeOPR(moneybox , exchangeoprid, exchangeoprdate, sourcecurrency, source_exchangerate, outmenyvalue, targetcurrency, target_exchangerate, notes));

                    }
                    return ExchangeOPRList;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_All_ExchangeOPRList:" + ee.Message);

                }
            }
        }
        internal class PayINSQL
        {

            DatabaseInterface DB;
            public   static class PayINTable
            {
                internal const string TableName = "Account_PayIN";
                internal const string PayOprID = "PayOprID";
                internal const string PayOprDate = "PayOprDate";
                internal const string OperationType = "OperationType";
                internal const string OperationID = "OperationID";
                internal const string PayDescription = "PayDescription";
                internal const string Value = "Value";
                internal const string ExchangeRate = "ExchangeRate";
                internal const string CurrencyID = "CurrencyID";
                internal const string Notes = "Notes"; 
                internal const string MoneyBoxID = "MoneyBoxID";
            }
            internal PayINSQL(DatabaseInterface db)
            {
                DB = db;

            }
            internal PayIN GetPayIN_INFO_BYID(uint payinid)
            {
                DataTable t = new DataTable();
                
                t = DB.GetData("select "
                    + PayINTable.PayOprDate
                    + ","
                    + PayINTable.OperationType 
                    + ","
                    + PayINTable.OperationID  
                    + ","
                    + PayINTable.PayDescription 
                    + ","
                    + PayINTable.Value
                    + ","
                    + PayINTable.ExchangeRate
                    + ","
                    + PayINTable.CurrencyID 
                    + ","
                    + PayINTable.Notes
                    + ","
                    + PayINTable.MoneyBoxID 
                    + " from   "
                    + PayINTable.TableName
                    + " where "
                    + PayINTable.PayOprID + "=" + payinid 
                  );
                if (t.Rows.Count == 1)
                {
                    DateTime payindate = Convert.ToDateTime(t.Rows[0][0].ToString());
                    Bill Bill_ = null;
                    try
                    {
                        uint operationtype = Convert.ToUInt32(t.Rows[0][1].ToString());
                        uint operationID = Convert.ToUInt32(t.Rows[0][2].ToString());
                        Operation  Operation_ = new Operation(operationtype, operationID);
                        Bill_ = new OperationSQL(DB).GetOperationBill(Operation_);
                    }
                    catch 
                    {
                        Bill_ = null;

                    }
                   
                    string description = t.Rows[0][3].ToString();
                    double value = Convert.ToDouble(t.Rows[0][4].ToString());
                    double exchangerate = Convert.ToDouble(t.Rows[0][5].ToString());
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][6].ToString()));
                    string notes = t.Rows[0][7].ToString();
                    MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows [0][8]));
                    return new PayIN (moneybox,payinid, payindate, Bill_, description , value, exchangerate, currency , notes);

                }
                else
                    return null;
            }
      
            internal List<PayIN > GetPayINList(Operation  opeartion)
            {
                try
                {
                    List<PayIN > payinList = new List<PayIN >();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + PayINTable.PayOprID 
                    + ","
                    + PayINTable.PayOprDate
                    + ","
                    + PayINTable.PayDescription
                    + ","
                    + PayINTable.Value
                    + ","
                    + PayINTable.ExchangeRate
                    + ","
                    + PayINTable.CurrencyID
                    + ","
                    + PayINTable.Notes
                         + ","
                    + PayINTable.MoneyBoxID 
                    + " from   "
                    + PayINTable.TableName
                    + " where "
                    + PayINTable.OperationType   + "=" +opeartion.OperationType
                     + " and "
                    + PayINTable.OperationID + "=" + opeartion.OperationID 
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint payinid =Convert .ToUInt32 ( t.Rows[i][0].ToString());
                        DateTime payindate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string description = t.Rows[i][2].ToString();
                        double value = Convert.ToDouble(t.Rows[i][3].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][4].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        string notes = t.Rows[i][6].ToString();
                        MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][7]));

                        payinList.Add ( new PayIN(moneybox , payinid, payindate, new OperationSQL(DB).GetOperationBill(opeartion ), description, value, exchangerate, currency, notes));
                    }
                    return payinList;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayINList:" + ee.Message);
                    return null;
                }
            }
            internal List<Money_Currency> GetPayINList_As_Money_Currency(Operation opeartion)
            {
                List<Money_Currency> payinList = new List<Money_Currency>();
                try
                {
                   
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + PayINTable.Value
                    + ","
                    + PayINTable.ExchangeRate
                    + ","
                    + PayINTable.CurrencyID
                    + " from   "
                    + PayINTable.TableName
                    + " where "
                    + PayINTable.OperationType + "=" + opeartion.OperationType
                     + " and "
                    + PayINTable.OperationID + "=" + opeartion.OperationID
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        double value = Convert.ToDouble(t.Rows[i][0].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        payinList.Add(new Money_Currency(currency, value, exchangerate));
                    }
                    
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayINList_As_Money_Currenncy:" + ee.Message);
                }
                return payinList;
            }

            internal List<PayIN> Get_All_PayINList(MoneyBox moneybox)
            {
                try
                {
  
                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    List<PayIN> payinList = new List<PayIN>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + PayINTable.PayOprID
                    + ","
                    + PayINTable.PayOprDate
                    + ","
                    + PayINTable.PayDescription
                    + ","
                    + PayINTable.Value
                    + ","
                    + PayINTable.ExchangeRate
                    + ","
                    + PayINTable.CurrencyID
                    + ","
                    + PayINTable.Notes
                    + ","
                    + PayINTable.OperationID 
                    + ","
                    + PayINTable.OperationType

                    + " from   "
                    + PayINTable.TableName
                    +" where "
                    + PayINTable.MoneyBoxID+"="+moneybox.BoxID
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint payinid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        
                        DateTime payindate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string description = t.Rows[i][2].ToString(); 
                        double value = Convert.ToDouble(t.Rows[i][3].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][4].ToString()); 
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        string notes = t.Rows[i][6].ToString();
                        Bill  bill;
                        //uint operationid;
                        //if(uint.TryParse (t.Rows[i][7].ToString(),out operationid))
                        //{

                        //}
                        //else
                        //{

                        //}
                        uint operationid;
                        if(uint .TryParse (t.Rows[i][7].ToString(),out operationid ))
                        {
                             operationid = Convert.ToUInt32(t.Rows[i][7].ToString());
                            uint operationtype = Convert.ToUInt32(t.Rows[i][8].ToString());
                            bill = new OperationSQL(DB).GetOperationBill(new Operation(operationtype, operationid));
                        }
                        else 
                        {
                            bill  = null;
                        }

                        payinList.Add(new PayIN(moneybox , payinid, payindate,bill , description, value, exchangerate, currency, notes));
                    }
                    
                    return payinList;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_All_PayINList:" + ee.Message);
                    return null;
                }
            }

        }
        internal class PayOUTSQL
        {

            DatabaseInterface DB;
            public    static class PayOUTTable
            {
                internal const string TableName = "Account_PayOUT";
                internal const string PayOprID = "PayOprID";
                internal const string PayOprDate = "PayOprDate";
                internal const string OperationType = "OperationType";
                internal const string OperationID = "OperationID";
                internal const string PayDescription = "PayDescription";
                internal const string Value = "Value";
                internal const string ExchangeRate = "ExchangeRate";
                internal const string CurrencyID = "CurrencyID";
                internal const string Notes = "Notes";
                internal const string MoneyBoxID = "MoneyBoxID";
            }
            internal PayOUTSQL(DatabaseInterface db)
            {
                DB = db;

            }
            internal PayOUT  GetPayOUT_INFO_BYID(uint payoutid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + PayOUTTable.PayOprDate
                        + ","
                        + PayOUTTable.OperationType
                        + ","
                         + PayOUTTable.OperationID
                        + ","
                        + PayOUTTable.PayDescription
                        + ","
                        + PayOUTTable.Value
                        + ","
                        + PayOUTTable.ExchangeRate
                        + ","
                        + PayOUTTable.CurrencyID
                        + ","
                        + PayOUTTable.Notes
                        + ","
                        + PayOUTTable.MoneyBoxID
                        + " from   "
                        + PayOUTTable.TableName
                        + " where "
                        + PayOUTTable.PayOprID + "=" + payoutid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime payindate = Convert.ToDateTime(t.Rows[0][0].ToString());
                        Operation operation;
                        try
                        {

                            uint operationtype = Convert.ToUInt32(t.Rows[0][1].ToString());
                            uint operationid = Convert.ToUInt32(t.Rows[0][2].ToString());
                            operation = new Operation(operationtype, operationid);

                        }
                        catch
                        {
                            operation = null;
                        }
                        string description = t.Rows[0][3].ToString();
                        double value = Convert.ToDouble(t.Rows[0][4].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[0][5].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][6].ToString()));
                        string notes = t.Rows[0][7].ToString();
                        MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[0][8]));

                        return new PayOUT(moneybox, payoutid, payindate, new OperationSQL(DB).GetOperationBill(operation), description, value, exchangerate, currency, notes);
                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayOUT_INFO_BYID:" + ee.Message);
                }
        
            }

            internal List<PayOUT> GetPaysOUT_List(Operation Operation_)
            {
                try
                {
                    List<PayOUT> PayOUTList = new List<PayOUT>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + PayOUTTable.PayOprID
                    + ","
                    + PayOUTTable.PayOprDate
                    + ","
                    + PayOUTTable.PayDescription
                    + ","
                    + PayOUTTable.Value
                    + ","
                    + PayOUTTable.ExchangeRate
                    + ","
                    + PayOUTTable.CurrencyID
                    + ","
                    + PayOUTTable.Notes
                     + ","
                    + PayOUTTable.MoneyBoxID
                    + " from   "
                    + PayOUTTable.TableName
                    + " where "
                    + PayOUTTable.OperationType + "=" + Operation_.OperationType
                    + " and "
                    + PayOUTTable.OperationID + "=" + Operation_.OperationID
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint payinid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime payindate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string description = t.Rows[i][2].ToString();
                        double value = Convert.ToDouble(t.Rows[i][3].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][4].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        string notes = t.Rows[i][6].ToString();
                        MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][7]));

                        if (Operation_ != null && Operation_.OperationType == Operation.Employee_PayOrder)
                        {
                            EmployeePayOrder EmployeePayOrder_ = new CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(Operation_.OperationID);
                            PayOUTList.Add(new PayOUT(moneybox, payinid, payindate, EmployeePayOrder_, description, value, exchangerate, currency, notes));

                        }
                        else
                            PayOUTList.Add(new PayOUT(moneybox, payinid, payindate, new OperationSQL(DB).GetOperationBill(Operation_), description, value, exchangerate, currency, notes));
                    }
                    return PayOUTList;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPaysOUT_List:" + ee.Message);
                }
            }
            internal List<PayOUT> Get_All_PaysOUT_List(MoneyBox moneybox)
            {
                try
                {

                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    List<PayOUT> PayOUTList = new List<PayOUT>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + PayOUTTable.PayOprID
                    + ","
                    + PayOUTTable.PayOprDate
                    + ","
                    + PayOUTTable.PayDescription
                    + ","
                    + PayOUTTable.Value
                    + ","
                    + PayOUTTable.ExchangeRate
                    + ","
                    + PayOUTTable.CurrencyID
                    + ","
                    + PayOUTTable.Notes
                    + " , "
                    + PayOUTTable.OperationID
                    + " , "
                    + PayOUTTable.OperationType

                    + " from   "
                    + PayOUTTable.TableName
                    + " where "
                    + PayOUTTable.MoneyBoxID + "=" + moneybox.BoxID
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint payinid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime payindate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string description = t.Rows[i][2].ToString();
                        double value = Convert.ToDouble(t.Rows[i][3].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][4].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        string notes = t.Rows[i][6].ToString();
                        Bill bill;
                        uint operationid;
                        if (uint.TryParse(t.Rows[i][7].ToString(), out operationid))
                        {
                            operationid = Convert.ToUInt32(t.Rows[i][7].ToString());
                            uint operationtype = Convert.ToUInt32(t.Rows[i][8].ToString());
                            if (operationtype == Operation.Employee_PayOrder)
                            {
                                EmployeePayOrder EmployeePayOrder_ = new CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(operationid);
                                PayOUTList.Add(new PayOUT(moneybox, payinid, payindate, EmployeePayOrder_, description, value, exchangerate, currency, notes));

                            }
                            else
                            {
                                bill = new OperationSQL(DB).GetOperationBill(new Operation(operationtype, operationid));
                                PayOUTList.Add(new PayOUT(moneybox, payinid, payindate, bill, description, value, exchangerate, currency, notes));

                            }

                        }
                        else
                        {
                            bill = null;
                            PayOUTList.Add(new PayOUT(moneybox, payinid, payindate, bill, description, value, exchangerate, currency, notes));

                        }

                    }
                    return PayOUTList;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_All_PaysOUT_List:" + ee.Message);
                }
            }


        }
        public class MoneyBoxSQL
        {
            DatabaseInterface DB;
            private static class MoneyBoxTable
            {
                public const string TableName = "Account_MoneyBox";
                public const string MoneyBoxID = "MoneyBoxID";
                public const string MoneyBoxName = "MoneyBoxName";


            }
            public MoneyBoxSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public MoneyBox GetMoneyBoxBYID(uint MoneyBoxid)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                    +MoneyBoxTable.MoneyBoxName
                    +" from   "
                + MoneyBoxTable.TableName
                + " where "
                + MoneyBoxTable.MoneyBoxID + "=" + MoneyBoxid
                  );
                if (t.Rows.Count == 1)
                {
                    string MoneyBoxname = t.Rows[0][0].ToString();


                    return new MoneyBox(MoneyBoxid, MoneyBoxname);

                }
                else
                    return null;
            }
     
            public List< MoneyBox> GetMoneyBox_List()
            {
                List<MoneyBox> list = new List<MoneyBox>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + MoneyBoxTable.MoneyBoxID
                        + ","
                        + MoneyBoxTable.MoneyBoxName
                        + " from   "
                    + MoneyBoxTable.TableName

                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint MoneyBoxid = Convert.ToUInt32(t.Rows[i][0]);
                        string MoneyBoxname = t.Rows[i][1].ToString();


                        list .Add ( new MoneyBox(MoneyBoxid, MoneyBoxname));

                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMoneyBox_List:" + ee.Message);

                }
               
                
                    return list ;
            }

        }
        internal class MoneyTransFormOPRSQL
        {

            DatabaseInterface DB;
            public static class MoneyTransFormOPRTable
            {
                internal const string TableName = "Account_MoneyTransFormOPR";
                internal const string Creator_UserID = "Creator_UserID";
                internal const string MoneyTransFormOPRID = "MoneyTransFormOPRID";
                internal const string MoneyTransFormOPRDate = "MoneyTransFormOPRDate";
                internal const string SourceMoneyBoxID = "SourceMoneyBoxID";
                internal const string TargetMoneyBoxID = "TargetMoneyBoxID";
                internal const string Value = "Value";
                internal const string ExchangeRate = "ExchangeRate";
                internal const string CurrencyID = "CurrencyID";
                internal const string Notes = "Notes";
                internal const string Confirm_UserID = "Confirm_UserID";
            }
            internal MoneyTransFormOPRSQL(DatabaseInterface db)
            {
                DB = db;

            }
            internal MoneyTransFormOPR GetMoneyTransFormOPR_INFO_BYID(uint MoneyTransFormOPRid)
            {
                DataTable t = new DataTable();

                t = DB.GetData("select "
                    
                    + MoneyTransFormOPRTable.MoneyTransFormOPRDate
                    + ","
                    + MoneyTransFormOPRTable.SourceMoneyBoxID 
                    + ","
                    + MoneyTransFormOPRTable.TargetMoneyBoxID
                    + ","
                    + MoneyTransFormOPRTable.Value
                    + ","
                    + MoneyTransFormOPRTable.ExchangeRate
                    + ","
                    + MoneyTransFormOPRTable.CurrencyID
                    + ","
                    + MoneyTransFormOPRTable.Notes
                    + ","
                     + MoneyTransFormOPRTable.Creator_UserID
                    + ","
                    + MoneyTransFormOPRTable.Confirm_UserID  
                    + " from   "
                    + MoneyTransFormOPRTable.TableName
                    + " where "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRID  + "=" + MoneyTransFormOPRid
                  );
                if (t.Rows.Count == 1)
                {
                    DateTime MoneyTransFormOPRdate = Convert.ToDateTime(t.Rows[0][0].ToString());
                    MoneyBox sourcemoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[0][1]));
                    MoneyBox targetmoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[0][2]));

                    double value = Convert.ToDouble(t.Rows[0][3].ToString());
                    double exchangerate = Convert.ToDouble(t.Rows[0][4].ToString());
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][5].ToString()));
                    string notes = t.Rows[0][6].ToString();
                    DatabaseInterface.User Creator_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[0][7].ToString()));
                    DatabaseInterface.User Confirm_User ;
                    try
                    {
                        Confirm_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[0][8].ToString()));
                    }
                    catch
                    {
                        Confirm_User = null;
                    }
                    return new MoneyTransFormOPR(Creator_User, MoneyTransFormOPRid, MoneyTransFormOPRdate, sourcemoneybox 
                        , targetmoneybox , value, exchangerate, currency, notes,Confirm_User);

                }
                else
                    return null;
            }
         
            internal List<MoneyTransFormOPR> Get_All_MoneyTransFormOPRList()
            {
                try
                {
                    List<MoneyTransFormOPR> MoneyTransFormOPRList = new List<MoneyTransFormOPR>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRID
                    + ","
                   + MoneyTransFormOPRTable.MoneyTransFormOPRDate
                    + ","
                    + MoneyTransFormOPRTable.SourceMoneyBoxID
                    + ","
                    + MoneyTransFormOPRTable.TargetMoneyBoxID
                    + ","
                    + MoneyTransFormOPRTable.Value
                    + ","
                    + MoneyTransFormOPRTable.ExchangeRate
                    + ","
                    + MoneyTransFormOPRTable.CurrencyID
                    + ","
                    + MoneyTransFormOPRTable.Notes
                    + ","
                    + MoneyTransFormOPRTable.Creator_UserID
                    + ","
                    + MoneyTransFormOPRTable.Confirm_UserID
                    + " from   "
                    + MoneyTransFormOPRTable.TableName
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint MoneyTransFormOPRid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime MoneyTransFormOPRdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        MoneyBox sourcemoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][2]));
                        MoneyBox targetmoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][3]));

                        double value = Convert.ToDouble(t.Rows[i][4].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][5].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][6].ToString()));
                        string notes = t.Rows[i][7].ToString();
                        DatabaseInterface.User Creator_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[i][8].ToString()));
                        DatabaseInterface.User Confirm_User;
                        try
                        {
                            Confirm_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[i][9].ToString()));
                        }
                        catch
                        {
                            Confirm_User = null;
                        }
                        MoneyTransFormOPRList.Add ( new MoneyTransFormOPR(Creator_User, MoneyTransFormOPRid, MoneyTransFormOPRdate, sourcemoneybox
                            , targetmoneybox, value, exchangerate, currency, notes, Confirm_User ));

                    }
                    return MoneyTransFormOPRList;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMoneyTransFormOPRList:" + ee.Message);
                    return null;
                }
            }
            internal List<Money_Currency> GetMoneyTransFormOPRList_As_Money_Currency()
            {
                List<Money_Currency> MoneyTransFormOPRList = new List<Money_Currency>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MoneyTransFormOPRTable.Value
                    + ","
                    + MoneyTransFormOPRTable.ExchangeRate
                    + ","
                    + MoneyTransFormOPRTable.CurrencyID
                    + " from   "
                    + MoneyTransFormOPRTable.TableName
            
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        double value = Convert.ToDouble(t.Rows[i][0].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        MoneyTransFormOPRList.Add(new Money_Currency(currency, value, exchangerate));
                    }

                }
                catch (Exception ee)
                {
                    throw new Exception("GetMoneyTransFormOPRList_As_Money_Currenncy:" + ee.Message);
                }
                return MoneyTransFormOPRList;
            }

            internal List <MoneyTransFormOPR > Get_Stuck_MoneyTransformOPR_IN_List(MoneyBox moneybox)
            {
                try
                {
 
                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    List<MoneyTransFormOPR> MoneyTransFormOPRList = new List<MoneyTransFormOPR>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRID
                    + ","
                   + MoneyTransFormOPRTable.MoneyTransFormOPRDate
                    + ","
                    + MoneyTransFormOPRTable.SourceMoneyBoxID
                    + ","
                    + MoneyTransFormOPRTable.TargetMoneyBoxID
                    + ","
                    + MoneyTransFormOPRTable.Value
                    + ","
                    + MoneyTransFormOPRTable.ExchangeRate
                    + ","
                    + MoneyTransFormOPRTable.CurrencyID
                    + ","
                    + MoneyTransFormOPRTable.Notes
                    + ","
                    + MoneyTransFormOPRTable.Creator_UserID
                    + ","
                    + MoneyTransFormOPRTable.Confirm_UserID
                    + " from   "
                    + MoneyTransFormOPRTable.TableName
                     + " where   "
                    + MoneyTransFormOPRTable.TargetMoneyBoxID + "=" + moneybox.BoxID
                    + " and   "
                    + MoneyTransFormOPRTable.Confirm_UserID + " is null"
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint MoneyTransFormOPRid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime MoneyTransFormOPRdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        MoneyBox sourcemoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][2]));
                        MoneyBox targetmoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][3]));

                        double value = Convert.ToDouble(t.Rows[i][4].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][5].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][6].ToString()));
                        string notes = t.Rows[i][7].ToString();
                        DatabaseInterface.User Creator_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[i][8].ToString()));
                        DatabaseInterface.User Confirm_User;
                        try
                        {
                            Confirm_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[i][9].ToString()));
                        }
                        catch
                        {
                            Confirm_User = null;
                        }
                        MoneyTransFormOPRList.Add(new MoneyTransFormOPR(Creator_User, MoneyTransFormOPRid, MoneyTransFormOPRdate, sourcemoneybox
                            , targetmoneybox, value, exchangerate, currency, notes, Confirm_User));

                    }
                    return MoneyTransFormOPRList;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Stuck_MoneyTransformOPR_IN_List:" + ee.Message);
                    return null;
                }
               
         
            }
            internal List<MoneyTransFormOPR> Get_Stuck_MoneyTransformOPR_OUT_List(MoneyBox moneybox)
            {
                try
                {

                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    List<MoneyTransFormOPR> MoneyTransFormOPRList = new List<MoneyTransFormOPR>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRID
                    + ","
                   + MoneyTransFormOPRTable.MoneyTransFormOPRDate
                    + ","
                    + MoneyTransFormOPRTable.SourceMoneyBoxID
                    + ","
                    + MoneyTransFormOPRTable.TargetMoneyBoxID
                    + ","
                    + MoneyTransFormOPRTable.Value
                    + ","
                    + MoneyTransFormOPRTable.ExchangeRate
                    + ","
                    + MoneyTransFormOPRTable.CurrencyID
                    + ","
                    + MoneyTransFormOPRTable.Notes
                    + ","
                    + MoneyTransFormOPRTable.Creator_UserID
                    + ","
                    + MoneyTransFormOPRTable.Confirm_UserID
                    + " from   "
                    + MoneyTransFormOPRTable.TableName
                      + " where   "
                    + MoneyTransFormOPRTable.SourceMoneyBoxID + "=" + moneybox.BoxID
                    + " and   "
                    + MoneyTransFormOPRTable.Confirm_UserID + " is null"
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint MoneyTransFormOPRid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime MoneyTransFormOPRdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        MoneyBox sourcemoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][2]));
                        MoneyBox targetmoneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][3]));

                        double value = Convert.ToDouble(t.Rows[i][4].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][5].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][6].ToString()));
                        string notes = t.Rows[i][7].ToString();
                        DatabaseInterface.User Creator_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[i][8].ToString()));
                        DatabaseInterface.User Confirm_User;
                        try
                        {
                            Confirm_User = DB.GetUser_BY_ID(Convert.ToUInt32(t.Rows[i][9].ToString()));
                        }
                        catch
                        {
                            Confirm_User = null;
                        }
                        MoneyTransFormOPRList.Add(new MoneyTransFormOPR(Creator_User, MoneyTransFormOPRid, MoneyTransFormOPRdate, sourcemoneybox
                            , targetmoneybox, value, exchangerate, currency, notes, Confirm_User));

                    }
                    return MoneyTransFormOPRList;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Stuck_MoneyTransformOPR_IN_List:" + ee.Message);
                    return null;
                }
               
            }
        }
    }
}
