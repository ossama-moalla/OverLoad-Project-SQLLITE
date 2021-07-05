

using OverLoad_Server_Kernal.AccountingSQL;
using OverLoad_Server_Kernal.CompanySQL;
using OverLoad_Server_Kernal.MaintenanceSQL;
using OverLoad_Server_Kernal.Objects;
using OverLoad_Server_Kernal.TradeSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace OverLoad_Server_Kernal
{
    internal class DataBaseFunctions
    {
        DatabaseInterface DB;
    
        internal DataBaseFunctions(DatabaseInterface  db)
        {
            DB = db;
        }
        public  double Account_GetAmountMoney(uint boxid, uint currencyid)
        {
            try
            {
                double moneyin, moneyin_Exchange, moneyin_moneytransform
                    , moneyout, moneyout_Exchange, moneyout_moneytransform;
                DataTable moneyin_Table = 
                    DB.GetData("select  sum(Value)  from Account_PayIN "
                    +" where CurrencyID = "
                    +currencyid
                    + " and MoneyBoxID ="+boxid 
                    );
               
                DataTable moneyin_Exchange_Table =
                    DB.GetData("select  SUM(OutMoneyValue * (TargetExchangeRate / SourceExchangeRate)) from Account_ExchangeOpr "
                    +" where TargetCurrencyID = "
                    +currencyid
                    + " and MoneyBoxID =" + boxid
                    );
              
                DataTable moneyout_Table =
                    DB.GetData("select  sum(Value)  from Account_PayOUT "
                    + " where CurrencyID = "
                    + currencyid
                     + " and MoneyBoxID =" + boxid
                    );
               
                DataTable moneyout_Exchange_Table =
                    DB.GetData("select  SUM(OutMoneyValue) from Account_ExchangeOpr "
                    + "where SourceCurrencyID = "+
                     +currencyid
                     + " and MoneyBoxID =" + boxid
                    );
                DataTable moneyin_moneytransform_Table =
                    DB.GetData("select  SUM(Value) from Account_MoneyTransFormOPR "
                    + "where CurrencyID = " +
                     +currencyid
                     + " and TargetMoneyBoxID =" + boxid
                      + " and Confirm_UserID is not null" 
                    );
                DataTable moneyout_moneytransform_Table =
                    DB.GetData("select  SUM(Value) from Account_MoneyTransFormOPR "
                    + "where CurrencyID = " +
                     +currencyid
                     + " and SourceMoneyBoxID =" + boxid
                    );
                try
                {
                    moneyin = Convert.ToDouble(moneyin_Table.Rows[0][0]);

                }
                catch
                {
                    moneyin = 0;
                }

                try
                {
                    moneyin_Exchange = Convert.ToDouble(moneyin_Exchange_Table.Rows[0][0]);

                }
                catch
                {
                    moneyin_Exchange = 0;
                }
                try
                {
                    moneyout = Convert.ToDouble(moneyout_Table.Rows[0][0]);

                }
                catch
                {
                    moneyout = 0;
                }
                try
                {
                    moneyout_Exchange = Convert.ToDouble(moneyout_Exchange_Table.Rows[0][0]);
                }
                catch
                {
                    moneyout_Exchange = 0;
                }

                try
                {
                    moneyin_moneytransform = Convert.ToDouble(moneyin_moneytransform_Table.Rows[0][0]);
                }
                catch
                {
                    moneyin_moneytransform = 0;
                }
                try
                {
                    moneyout_moneytransform = Convert.ToDouble(moneyout_moneytransform_Table.Rows[0][0]);
                }
                catch
                {
                    moneyout_moneytransform = 0;
                }

                return (moneyin +moneyin_Exchange-moneyout-moneyout_Exchange+ moneyin_moneytransform- moneyout_moneytransform);
            }
            catch(Exception ee)
            {
                throw new Exception ("Account_GetAmountMoney"+ee.Message );
                return -1;
            }
        }
        internal string Account_GetAmountMoneyOverAll(MoneyBox moneybox)
        {
            
            try
            {
                string returnstring = "";
                DataTable Currency
                    = DB.GetData(
                        "select distinct CurrencyID from "
                    + " (select CurrencyID from Account_PayIN where MoneyBoxID="+moneybox.BoxID
                     + "  union select CurrencyID from Account_PayOUT where MoneyBoxID=" + moneybox.BoxID
                     + "  union select SourceCurrencyID from Account_ExchangeOpr where MoneyBoxID=" + moneybox.BoxID
                     + "  union select TargetCurrencyID from Account_ExchangeOpr where MoneyBoxID=" + moneybox.BoxID
                     + "  union select CurrencyID from Account_MoneyTransFormOPR where SourceMoneyBoxID=" + moneybox.BoxID
                     + "  union select CurrencyID from Account_MoneyTransFormOPR where TargetMoneyBoxID=" + moneybox.BoxID
                     + ") as Currency"
                     );

                for(int i=0;i<Currency .Rows .Count;i++)
                {
                    uint currencyid = Convert.ToUInt32(Currency.Rows[i][0]);
                    string currency_symbol = DB.GetData("select CurrencySymbol from  "+CurrencySQL.CurrencyTable.TableName
                        +" where "
                        +CurrencySQL.CurrencyTable.CurrencyID
                        +"="+ currencyid).Rows [0][0].ToString ();

                    returnstring = returnstring + " [ "
                            +System .Math.Round (  Account_GetAmountMoney(moneybox.BoxID , currencyid ),2)
                        + currency_symbol + " ] ";
                }
                return returnstring ;
            }catch(Exception ee)
            {
                throw new Exception ("Account_GetAmountMoneyOverAll:" + ee.Message );
                return "";
            }
        }
        //internal string ConvertPayINList_TO_String(List <PayIN > payinlsit)
        //{
        //    try
        //    {
        //        string returnstring = "";


        //        for (int i = 0; i < payinlsit.Count; i++)
        //        {
        //            returnstring = returnstring + " [ "
        //                    + System.Math.Round(payinlsit [i].Value, 2)
        //                + payinlsit[i]._Currency .CurrencySymbol + " ] ";
        //        }
        //        return returnstring;
        //    }
        //    catch (Exception ee)
        //    {
        //        throw new Exception ("ConvertPayINList_TO_String:" + ee.Message);
        //        return "";
        //    }
        //}
  

        //internal string ConvertPayOutList_TO_String(List<PayOUT> payoutlsit)
        //{
        //    try
        //    {
        //        string returnstring = "";


        //        for (int i = 0; i < payoutlsit.Count; i++)
        //        {
        //            returnstring = returnstring + " [ "
        //                    + System.Math.Round(payoutlsit[i].Value, 2)
        //                + payoutlsit[i]._Currency.CurrencySymbol + " ] ";
        //        }
        //        return returnstring;
        //    }
        //    catch (Exception ee)
        //    {
        //        throw new Exception ("ConvertPayOutList_TO_String:" + ee.Message);
        //        return "";
        //    }
        //}
        internal string Account_GetMoneyIN_As_String(List<PayCurrencyReport> PayCurrencyReportList)
        {

            string return_str = "";
            try
            {
                for (int i = 0; i < PayCurrencyReportList.Count; i++)
                {
                    double in_value = System.Math.Round(PayCurrencyReportList[i].PaysIN_Sell +
                        PayCurrencyReportList[i].PaysIN_Maintenance +
                        PayCurrencyReportList[i].PaysIN_Exchange +
                        PayCurrencyReportList[i].PaysIN_NON, 2);
                    return_str = return_str + " [" + in_value + PayCurrencyReportList[i].CurrencySymbol + "] ";
                }


            }
            catch (Exception ee)
            {
                throw new Exception ("Account_GetMoneyIN_As_String:" + ee.Message);
            }
            return return_str;
        }
        internal string Account_GetMoneyOUT_As_String(List<PayCurrencyReport> PayCurrencyReportList)
        {

            string return_str = "";
            try
            {
                for (int i = 0; i < PayCurrencyReportList.Count; i++)
                {
                    double out_value = System.Math.Round(PayCurrencyReportList[i].PaysOUT_Buy +
                        PayCurrencyReportList[i].PaysOUT_Emp +
                        PayCurrencyReportList[i].PaysOUT_Exchange +
                        PayCurrencyReportList[i].PaysOUT_NON, 2);
                    return_str = return_str + " [" + out_value + PayCurrencyReportList[i].CurrencySymbol + "] ";
                }


            }
            catch(Exception ee)
            {
                throw new Exception ("Account_GetMoneyOUT_As_String:" + ee.Message  );
            }
            return return_str;
        }

        #region Report_Money
        internal List<PayCurrencyReport> Account_GetPays_DayReport(MoneyBox moneybox, int year, int month, int day)
        {
            List<PayCurrencyReport> PayCurrencyReportList = new List<PayCurrencyReport>();

            try
            {

                List<PayIN> allpayin_list = new PayINSQL(DB).Get_All_PayINList(moneybox );
                List<PayIN> day_payin_list
                    = allpayin_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month && x.PayOprDate.Day == day).ToList();
                List<PayOUT> allpayout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox );
                List<PayOUT> day_payout_list
                    = allpayout_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month && x.PayOprDate.Day == day).ToList();
                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox );
                List<ExchangeOPR> day_exchangeopr_list 
                     = allexchangeopr_list.Where(x => x.ExchangeOprDate .Year == year && x.ExchangeOprDate.Month == month && x.ExchangeOprDate.Day == day).ToList();
                List <MoneyTransFormOPR > day_moneytransform_list=
                    new MoneyTransFormOPRSQL (DB).Get_All_MoneyTransFormOPRList()
                    .Where(x => x.Confirm_User != null && x.MoneyTransFormOPRDate .Year == year && x.MoneyTransFormOPRDate.Month == month && x.MoneyTransFormOPRDate.Day == day).ToList();
                List<uint > curremcylist = new List<uint >();
                curremcylist.AddRange (day_payin_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(day_payout_list.Select(x => x._Currency.CurrencyID ).Distinct().ToList());
                curremcylist.AddRange(day_exchangeopr_list.Select(x => x.SourceCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(day_exchangeopr_list.Select(x => x.TargetCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(day_moneytransform_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());
                curremcylist = curremcylist.Distinct().ToList ();
                for(int i=0;i<curremcylist.Count;i++)
                {
                    
                    double payin_sell = day_payin_list.Where (y=>y._Currency .CurrencyID ==curremcylist[i] 
                    &&y._Bill !=null  && y._Bill._Operation.OperationType==Operation.BILL_SELL).Sum(x => x.Value);
                    double payin_mainenance = day_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_MAINTENANCE).Sum(x => x.Value);
                    double payin_moneytransform = day_moneytransform_list .Where(y => y._Currency.CurrencyID == curremcylist[i]
                   && y.TargetMoneyBox.BoxID==moneybox.BoxID).Sum(x => x.Value);
                    double payin_non = day_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill == null ).Sum(x => x.Value);
                    double payin_Exchange =System.Math .Round ( day_exchangeopr_list.Where(y => y.TargetCurrency .CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue* x.TargetExchangeRate / x.SourceExchangeRate),3);
                    double payout_buy = day_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_BUY).Sum(x => x.Value);
                    double payout_emp = day_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.Employee_PayOrder ).Sum(x => x.Value);
                    double payout_moneytransform = day_moneytransform_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                && y.SourceMoneyBox .BoxID == moneybox.BoxID).Sum(x => x.Value);
                    double payout_non = day_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill==null ).Sum(x => x.Value);
                    double payout_Exchange = day_exchangeopr_list.Where(y => y.SourceCurrency .CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue);
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(curremcylist[i]);

                    PayCurrencyReport paycurrency_report =
                        new PayCurrencyReport(currency.CurrencyID , currency.CurrencyName , currency.CurrencySymbol , payin_sell, payin_mainenance,payin_moneytransform, payin_non
                        , payin_Exchange, payout_buy, payout_emp,payout_moneytransform, payout_non, payout_Exchange);
                    PayCurrencyReportList.Add(paycurrency_report);
                }
                return PayCurrencyReportList;
            }
            catch (Exception ee)
            {
                throw new Exception ("Account_GetPays_DayReport:" + ee.Message);
                return PayCurrencyReportList;
            }
        }
        internal List<PayCurrencyReport> Account_GetPays_MonthReport(MoneyBox moneybox, int year, int month)
        {
            List<PayCurrencyReport> PayCurrencyReportList = new List<PayCurrencyReport>();

            try
            {

                List<PayIN> allpayin_list = new PayINSQL(DB).Get_All_PayINList(moneybox );
                List<PayIN> month_payin_list
                    = allpayin_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month ).ToList();
                List<PayOUT> allpayout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox );
                List<PayOUT> month_payout_list
                    = allpayout_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month ).ToList();
                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox );
                List<ExchangeOPR> month_exchangeopr_list
                     = allexchangeopr_list.Where(x => x.ExchangeOprDate.Year == year && x.ExchangeOprDate.Month == month).ToList();
                List<MoneyTransFormOPR> month_moneytransform_list =
                    new MoneyTransFormOPRSQL(DB).Get_All_MoneyTransFormOPRList()
                    .Where(x => x.Confirm_User != null && x.MoneyTransFormOPRDate.Year == year && x.MoneyTransFormOPRDate.Month == month ).ToList();
                List<uint> curremcylist = new List<uint>();
                curremcylist.AddRange(month_payin_list.Select(x => x._Currency.CurrencyID ).Distinct().ToList());
                curremcylist.AddRange(month_payout_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(month_exchangeopr_list.Select(x => x.SourceCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(month_exchangeopr_list.Select(x => x.TargetCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(month_moneytransform_list.Select(x => x._Currency.CurrencyID ).Distinct().ToList());

                curremcylist = curremcylist.Distinct().ToList();
      
                for (int i = 0; i < curremcylist.Count; i++)
                {
                    double payin_sell = month_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                   && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_SELL).Sum(x => x.Value);
                    double payin_mainenance = month_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_MAINTENANCE).Sum(x => x.Value);
                    double payin_moneytransform = month_moneytransform_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                  && y.TargetMoneyBox.BoxID == moneybox.BoxID).Sum(x => x.Value);

                    double payin_non = month_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill == null).Sum(x => x.Value);
                    double payin_Exchange = System.Math.Round(month_exchangeopr_list.Where(y => y.TargetCurrency.CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue * x.TargetExchangeRate / x.SourceExchangeRate), 3);
                    double payout_buy = month_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_BUY).Sum(x => x.Value);
                    double payout_emp = month_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.Employee_PayOrder).Sum(x => x.Value);
                    double payout_moneytransform = month_moneytransform_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                  && y.SourceMoneyBox .BoxID == moneybox.BoxID).Sum(x => x.Value);
                    double payout_non = month_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill == null).Sum(x => x.Value);
                    double payout_Exchange = month_exchangeopr_list.Where(y => y.SourceCurrency.CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue);
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(curremcylist[i]);
                    PayCurrencyReport paycurrency_report =
                        new PayCurrencyReport(currency.CurrencyID , currency.CurrencyName, currency.CurrencySymbol, payin_sell, payin_mainenance,payin_moneytransform, payin_non
                        , payin_Exchange, payout_buy, payout_emp,payout_moneytransform , payout_non, payout_Exchange);
                    PayCurrencyReportList.Add(paycurrency_report);
                }
                return PayCurrencyReportList;
            }
            catch (Exception ee)
            {
                throw new Exception ("Account_GetPays_MonthReport:" + ee.Message);
                return PayCurrencyReportList;
            }
        }
        internal List<PayCurrencyReport> Account_GetPays_YearReport(MoneyBox moneybox, int year)
        {
            List<PayCurrencyReport> PayCurrencyReportList = new List<PayCurrencyReport>();

            try
            {

                List<PayIN> allpayin_list = new PayINSQL(DB).Get_All_PayINList(moneybox);
                List<PayIN> year_payin_list
                    = allpayin_list.Where(x => x.PayOprDate.Year == year ).ToList();
                List<PayOUT> allpayout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox);
                List<PayOUT> year_payout_list
                    = allpayout_list.Where(x => x.PayOprDate.Year == year).ToList();
                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox);
                List<ExchangeOPR> year_exchangeopr_list
                     = allexchangeopr_list.Where(x => x.ExchangeOprDate.Year == year).ToList();
                List<MoneyTransFormOPR> year_moneytransform_list =
                    new MoneyTransFormOPRSQL(DB).Get_All_MoneyTransFormOPRList()
                    .Where(x => x.Confirm_User != null && x.MoneyTransFormOPRDate.Year == year).ToList();
                List<uint> curremcylist = new List<uint>();
                curremcylist.AddRange(year_payin_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_payout_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_exchangeopr_list.Select(x => x.SourceCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_exchangeopr_list.Select(x => x.TargetCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_moneytransform_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());

                curremcylist = curremcylist.Distinct().ToList();

                for (int i = 0; i < curremcylist.Count; i++)
                {
                    double payin_sell = year_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                   && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_SELL).Sum(x => x.Value);
                    double payin_mainenance = year_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_MAINTENANCE).Sum(x => x.Value);
                    double payin_moneytransform = year_moneytransform_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                  && y.TargetMoneyBox.BoxID == moneybox.BoxID).Sum(x => x.Value);

                    double payin_non = year_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill == null).Sum(x => x.Value);
                    double payin_Exchange = System.Math.Round(year_exchangeopr_list.Where(y => y.TargetCurrency.CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue * x.TargetExchangeRate / x.SourceExchangeRate), 3);
                    double payout_buy = year_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_BUY).Sum(x => x.Value);
                    double payout_emp = year_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.Employee_PayOrder).Sum(x => x.Value);
                    double payout_moneytransform = year_moneytransform_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                  && y.SourceMoneyBox.BoxID == moneybox.BoxID).Sum(x => x.Value);
                    double payout_non = year_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill == null).Sum(x => x.Value);
                    double payout_Exchange = year_exchangeopr_list.Where(y => y.SourceCurrency.CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue);
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(curremcylist[i]);
                    PayCurrencyReport paycurrency_report =
                        new PayCurrencyReport(currency.CurrencyID , currency.CurrencyName, currency.CurrencySymbol, payin_sell, payin_mainenance, payin_moneytransform, payin_non
                        , payin_Exchange, payout_buy, payout_emp, payout_moneytransform, payout_non, payout_Exchange);
                    PayCurrencyReportList.Add(paycurrency_report);
                }
                return PayCurrencyReportList;
            }
            catch (Exception ee)
            {
                throw new Exception ("Account_GetPays_YearReport:" + ee.Message);
                return PayCurrencyReportList;
            }
        }
        internal List<PayCurrencyReport> Account_GetPays_YearRangeReport(MoneyBox moneybox, int year1,int year2)
        {
            List<PayCurrencyReport> PayCurrencyReportList = new List<PayCurrencyReport>();

            try
            {
                int minyear, maxyear;
                if(year1 >year2 )
                {
                    minyear = year2;
                    maxyear = year1;
                }
                else
                {
                    minyear = year1;
                    maxyear = year2;
                }
                List<PayIN> allpayin_list = new PayINSQL(DB).Get_All_PayINList(moneybox);
                List<PayIN> year_range_payin_list
                    = allpayin_list.Where(x => x.PayOprDate.Year>= minyear &&x.PayOprDate .Year <=maxyear).ToList();
                List<PayOUT> allpayout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox);
                List<PayOUT> year_range_payout_list
                    = allpayout_list.Where(x => x.PayOprDate.Year >= minyear && x.PayOprDate.Year <= maxyear).ToList();
                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox);
                List<ExchangeOPR> year_range_exchangeopr_list
                     = allexchangeopr_list.Where(x => x.ExchangeOprDate .Year >= minyear && x.ExchangeOprDate .Year <= maxyear).ToList();
                List<MoneyTransFormOPR> year_range_moneytransform_list =
                    new MoneyTransFormOPRSQL(DB).Get_All_MoneyTransFormOPRList()
                    .Where(x => x.Confirm_User != null && x.MoneyTransFormOPRDate .Year >= minyear && x.MoneyTransFormOPRDate .Year <= maxyear).ToList();
                List<uint> curremcylist = new List<uint>();
                curremcylist.AddRange(year_range_payin_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_range_payout_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_range_exchangeopr_list.Select(x => x.SourceCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_range_exchangeopr_list.Select(x => x.TargetCurrency.CurrencyID).Distinct().ToList());
                curremcylist.AddRange(year_range_moneytransform_list.Select(x => x._Currency.CurrencyID).Distinct().ToList());

                curremcylist = curremcylist.Distinct().ToList();

                for (int i = 0; i < curremcylist.Count; i++)
                {
                    double payin_sell = year_range_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                   && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_SELL).Sum(x => x.Value);
                    double payin_mainenance = year_range_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_MAINTENANCE).Sum(x => x.Value);
                    double payin_moneytransform = year_range_moneytransform_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                  && y.TargetMoneyBox.BoxID == moneybox.BoxID).Sum(x => x.Value);

                    double payin_non = year_range_payin_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill == null).Sum(x => x.Value);
                    double payin_Exchange = System.Math.Round(year_range_exchangeopr_list.Where(y => y.TargetCurrency.CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue * x.TargetExchangeRate / x.SourceExchangeRate), 3);
                    double payout_buy = year_range_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.BILL_BUY).Sum(x => x.Value);
                    double payout_emp = year_range_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill != null && y._Bill._Operation.OperationType == Operation.Employee_PayOrder).Sum(x => x.Value);
                    double payout_moneytransform = year_range_moneytransform_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                  && y.SourceMoneyBox.BoxID == moneybox.BoxID).Sum(x => x.Value);
                    double payout_non = year_range_payout_list.Where(y => y._Currency.CurrencyID == curremcylist[i]
                    && y._Bill == null).Sum(x => x.Value);
                    double payout_Exchange = year_range_exchangeopr_list.Where(y => y.SourceCurrency.CurrencyID == curremcylist[i]
                     ).Sum(x => x.OutMoneyValue);
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(curremcylist[i]);
                    PayCurrencyReport paycurrency_report =
                        new PayCurrencyReport(currency.CurrencyID , currency.CurrencyName, currency.CurrencySymbol , payin_sell, payin_mainenance, payin_moneytransform, payin_non
                        , payin_Exchange, payout_buy, payout_emp, payout_moneytransform, payout_non, payout_Exchange);
                    PayCurrencyReportList.Add(paycurrency_report);
                }
                return PayCurrencyReportList;
            }
            catch (Exception ee)
            {
                throw new Exception ("Account_GetPays_YearRangeReport:" + ee.Message);
                return PayCurrencyReportList;
            }
        }
        internal List<AccountOprReportDetail> GetAccountOprReport_Details_InDay(MoneyBox moneybox, int year, int month, int day)
        {
            List<AccountOprReportDetail> AccountOprReportDetailList = new List<AccountOprReportDetail>();

            try
            {
                List<MoneyTransFormOPR> day_moneytransform_list =
                   new MoneyTransFormOPRSQL(DB).Get_All_MoneyTransFormOPRList()
                   .Where(x =>x.Confirm_User != null && x.MoneyTransFormOPRDate.Year == year && x.MoneyTransFormOPRDate.Month == month && x.MoneyTransFormOPRDate.Day == day).ToList();
                List<MoneyTransFormOPR> MoneyBox_IN_moneytransform_list = day_moneytransform_list
                                       .Where(x => x.TargetMoneyBox .BoxID  ==moneybox.BoxID ).ToList();
                List<MoneyTransFormOPR> MoneyBox_OUT_moneytransform_list = day_moneytransform_list
                                 .Where(x => x.SourceMoneyBox .BoxID == moneybox.BoxID).ToList();
                List<PayIN> allpayin_list = new PayINSQL(DB).Get_All_PayINList(moneybox );
                List<PayIN> day_payin_list
                    = allpayin_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month && x.PayOprDate.Day == day).ToList();
                List<PayOUT> allpayout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox );
                List<PayOUT> day_payout_list
                    = allpayout_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month && x.PayOprDate.Day == day).ToList();
                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox );
                List<ExchangeOPR> day_exchangeopr_list
                     = allexchangeopr_list.Where(x => x.ExchangeOprDate.Year == year && x.ExchangeOprDate.Month == month && x.ExchangeOprDate.Day == day).ToList();
                for (int i = 0; i < day_payout_list.Count; i++)
                {
                    DateTime accountoprdate = day_payout_list[i].PayOprDate;
                    uint  accountoprtype = AccountOprReportDetail.TYPE_PAY_OPR;
                    bool Direction_ = AccountOprReportDetail.DIRECTION_OUT;
                    uint accountopr_id = day_payout_list[i].PayOprID ;
                    string owner ;

                    if (day_payout_list[i]._EmployeePayOrder!= null) owner = "أمر صرف " +(day_payout_list[i]._EmployeePayOrder ._SalarysPayOrder ==null ?" مستقل ":"راتب شهر "
                            + day_payout_list[i]._EmployeePayOrder._SalarysPayOrder.ExecuteMonth +" , سنة :"+ day_payout_list[i]._EmployeePayOrder._SalarysPayOrder.ExecuteYear )
                            +" , ذو الرقم :"+ day_payout_list[i]._EmployeePayOrder .PayOrderID+
                            ", للموظف:" + day_payout_list[i]._EmployeePayOrder._Employee .EmployeeName ;
                    else if (day_payout_list[i]._Bill!=null && day_payout_list[i]._Bill._Operation.OperationType ==Operation.BILL_BUY) owner = " فاتورة شراء رقم:"+ day_payout_list[i]._Bill._Operation.OperationID;
                    else owner = "";
                    double value = day_payout_list[i].Value ;
                    double exchangerate =day_payout_list[i].ExchangeRate  ;
                    double realvalue = value /exchangerate ;
                    Currency   currency= day_payout_list[i]._Currency ;
                    AccountOprReportDetail accountopr_reportDetail
                        = new AccountOprReportDetail(accountoprdate, accountoprtype, Direction_, accountopr_id, owner
                        , value, currency.CurrencyID ,currency.CurrencyName,currency.CurrencySymbol, exchangerate, realvalue);
                    AccountOprReportDetailList.Add(accountopr_reportDetail);
                }
                for (int i = 0; i < day_payin_list.Count; i++)
                {
                    DateTime accountoprdate = day_payin_list[i].PayOprDate;
                    uint accountoprtype = AccountOprReportDetail.TYPE_PAY_OPR;
                    bool Direction_ = AccountOprReportDetail.DIRECTION_IN;
                    uint accountopr_id = day_payin_list[i].PayOprID;
                    string owner;

                    if (day_payin_list[i]._Bill == null) owner = "";
                    else if (day_payin_list[i]._Bill._Operation.OperationType == Operation.BILL_SELL) owner = " فاتورة مبيع رقم:" + day_payin_list[i]._Bill._Operation.OperationID;
                    else if (day_payin_list[i]._Bill._Operation.OperationType == Operation.BILL_MAINTENANCE) owner = " فاتورة صيانة رقم:" + day_payin_list[i]._Bill._Operation.OperationID;
                    else owner = "";
                    double value = day_payin_list[i].Value;
                    double exchangerate = day_payin_list[i].ExchangeRate;
                    double realvalue = value / exchangerate;
                    Currency  currency = day_payin_list[i]._Currency;
                    AccountOprReportDetail accountopr_reportDetail
                        = new AccountOprReportDetail(accountoprdate, accountoprtype, Direction_, accountopr_id, owner
                        , value, currency.CurrencyID,currency.CurrencyName,currency.CurrencySymbol, exchangerate, realvalue);
                    AccountOprReportDetailList.Add(accountopr_reportDetail);
                }
                for (int i = 0; i < MoneyBox_IN_moneytransform_list.Count; i++)
                {
                    DateTime accountoprdate = MoneyBox_IN_moneytransform_list[i].MoneyTransFormOPRDate ;
                    uint accountoprtype = AccountOprReportDetail.TYPE_MoneyTransform_OPR;
                    bool Direction_ = AccountOprReportDetail.DIRECTION_IN;
                    uint accountopr_id = MoneyBox_IN_moneytransform_list[i].MoneyTransFormOPRID ;
                    string owner="عملية تحويل مال من الصندوق :"+MoneyBox_IN_moneytransform_list[i].SourceMoneyBox .BoxName
                        +" الى "+ MoneyBox_IN_moneytransform_list[i].TargetMoneyBox .BoxName;

                    double value = MoneyBox_IN_moneytransform_list[i].Value;
                    double exchangerate = MoneyBox_IN_moneytransform_list[i].ExchangeRate;
                    double realvalue = value / exchangerate;
                    Currency  currency = MoneyBox_IN_moneytransform_list[i]._Currency;
                    AccountOprReportDetail accountopr_reportDetail
                        = new AccountOprReportDetail(accountoprdate, accountoprtype, Direction_, accountopr_id, owner
                        , value, currency.CurrencyID,currency.CurrencyName,currency.CurrencySymbol, exchangerate, realvalue);
                    AccountOprReportDetailList.Add(accountopr_reportDetail);
                }
                for (int i = 0; i < MoneyBox_OUT_moneytransform_list.Count; i++)
                {
                    DateTime accountoprdate = MoneyBox_OUT_moneytransform_list[i].MoneyTransFormOPRDate;
                    uint accountoprtype = AccountOprReportDetail.TYPE_MoneyTransform_OPR;
                    bool Direction_ = AccountOprReportDetail.DIRECTION_OUT;
                    uint accountopr_id = MoneyBox_OUT_moneytransform_list[i].MoneyTransFormOPRID;
                    string owner = "عملية تحويل مال من الصندوق :" + MoneyBox_OUT_moneytransform_list[i].SourceMoneyBox.BoxName
                        + " الى " + MoneyBox_OUT_moneytransform_list[i].TargetMoneyBox.BoxName;

                    double value = MoneyBox_OUT_moneytransform_list[i].Value;
                    double exchangerate = MoneyBox_OUT_moneytransform_list[i].ExchangeRate;
                    double realvalue = value / exchangerate;
                    Currency  currency = MoneyBox_OUT_moneytransform_list[i]._Currency;
                    AccountOprReportDetail accountopr_reportDetail
                        = new AccountOprReportDetail(accountoprdate, accountoprtype, Direction_, accountopr_id, owner
                        , value, currency.CurrencyID, currency.CurrencyName, currency.CurrencySymbol, exchangerate, realvalue);
                    AccountOprReportDetailList.Add(accountopr_reportDetail);
                }
                for (int i = 0; i < day_exchangeopr_list.Count; i++)
                {
                    DateTime accountoprdate= day_exchangeopr_list[i].ExchangeOprDate;
                    uint accountoprtype = AccountOprReportDetail.TYPE_EXCHANGE_OPR ;

                    uint accountopr_id = day_exchangeopr_list[i].ExchangeOprID;
                    string owner = "  عملية صرف من :" + day_exchangeopr_list[i].SourceCurrency.CurrencyName
                        + " ألى " + day_exchangeopr_list[i].TargetCurrency.CurrencyName;
                    double  invalue =System .Math .Round ( day_exchangeopr_list[i].OutMoneyValue * (day_exchangeopr_list[i].TargetExchangeRate / day_exchangeopr_list[i].SourceExchangeRate),3);
                    double  outvalue = day_exchangeopr_list[i].OutMoneyValue;
                    double in_exchangerate = day_exchangeopr_list[i].TargetExchangeRate;
                    double out_exchangerate = day_exchangeopr_list[i].SourceExchangeRate ;
                    Currency  in_currency = day_exchangeopr_list[i].TargetCurrency;
                    Currency  out_currency = day_exchangeopr_list[i].SourceCurrency;
                    double in_realvalue = invalue/ in_exchangerate;
                    double out_realvalue = outvalue / out_exchangerate;
                    AccountOprReportDetail accountopr_reportDetail_in
                         = new AccountOprReportDetail(accountoprdate, accountoprtype, AccountOprReportDetail.DIRECTION_IN , accountopr_id, owner
                         , invalue , in_currency.CurrencyID, in_currency.CurrencyName, in_currency.CurrencySymbol, in_exchangerate, in_realvalue);
                    AccountOprReportDetail accountopr_reportDetail_out
                         = new AccountOprReportDetail(accountoprdate, accountoprtype, AccountOprReportDetail.DIRECTION_OUT, accountopr_id, owner
                         , outvalue, out_currency.CurrencyID, out_currency.CurrencyName, out_currency.CurrencySymbol, out_exchangerate, out_realvalue);

                    AccountOprReportDetailList.Add(accountopr_reportDetail_in);
                    AccountOprReportDetailList.Add(accountopr_reportDetail_out);
                }
                return AccountOprReportDetailList.OrderBy  (x=>x.OprTime).ToList ();
            }
            catch (Exception ee)
            {
                throw new Exception ("GetAccountOprReport_Details_InDay:" + ee.Message);
                return AccountOprReportDetailList;
            }
        }
        internal List<AccountOprDayReportDetail> GetAccountOprReport_Details_InMonh(MoneyBox moneybox, int year, int month)
        {
            List<AccountOprDayReportDetail> AccountOprReportDetailList = new List<AccountOprDayReportDetail>();

            try
            {
                List<MoneyTransFormOPR> month_moneytransform_list =
                   new MoneyTransFormOPRSQL(DB).Get_All_MoneyTransFormOPRList()
                   .Where(x => x.Confirm_User != null && x.MoneyTransFormOPRDate.Year == year && x.MoneyTransFormOPRDate.Month == month ).ToList();
                List<MoneyTransFormOPR> MoneyBox_IN_moneytransform_list = month_moneytransform_list
                                       .Where(x => x.TargetMoneyBox.BoxID == moneybox.BoxID).ToList();
                List<MoneyTransFormOPR> MoneyBox_OUT_moneytransform_list = month_moneytransform_list
                                 .Where(x => x.SourceMoneyBox.BoxID == moneybox.BoxID).ToList();
                List<PayIN  > all_payin_list = new PayINSQL(DB).Get_All_PayINList(moneybox);

                List<PayOUT > all_payout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox);

                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox);
               
                for (int day_index = 1; day_index <= DateTime.DaysInMonth(year, month); day_index++)
                {
                    
                    List<Money_Currency> Day_monytransform_in_list_
                       = Money_Currency.Get_Money_Currency_List_From_MoneyTransform(MoneyBox_IN_moneytransform_list.Where(x => x.MoneyTransFormOPRDate .Year == year && x.MoneyTransFormOPRDate .Month == month && x.MoneyTransFormOPRDate .Day == day_index).ToList());
                    List<Money_Currency> Day_monytransform_out_list_
                      = Money_Currency.Get_Money_Currency_List_From_MoneyTransform(MoneyBox_OUT_moneytransform_list.Where(x => x.MoneyTransFormOPRDate.Year == year && x.MoneyTransFormOPRDate.Month == month && x.MoneyTransFormOPRDate.Day == day_index).ToList());

                    List<Money_Currency > Day_payin_list_ 
                        = Money_Currency.Get_Money_Currency_List_From_PayIN ( all_payin_list.Where (x=>x.PayOprDate.Year ==year && x.PayOprDate.Month ==month &&x.PayOprDate.Day == day_index).ToList ()) ;

                    List<Money_Currency> Day_payout_list_
                            = Money_Currency.Get_Money_Currency_List_From_PayOUT (all_payout_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month && x.PayOprDate.Day == day_index).ToList());
                    List<ExchangeOPR> day_exchangeopr_list_
                    = allexchangeopr_list.Where(x => x.ExchangeOprDate.Year == year && x.ExchangeOprDate.Month == month && x.ExchangeOprDate.Day == day_index).ToList();
                    List<Money_Currency> Day_ExchangeOPR_OUTDirection_list
                        = Money_Currency.Get_Money_Currency_List_From_ExchangeOPR_OUTDirection(day_exchangeopr_list_);
                    List<Money_Currency> Day_ExchangeOPR_INDirection_list
                        = Money_Currency.Get_Money_Currency_List_From_ExchangeOPR_INDirection(day_exchangeopr_list_);
                    List<Money_Currency> All_Money_IN_List = new List<Money_Currency>();
                    List<Money_Currency> All_Money_OUT_List = new List<Money_Currency>();
                    All_Money_IN_List.AddRange(Day_payin_list_);
                    All_Money_IN_List.AddRange(Day_ExchangeOPR_INDirection_list);
                    All_Money_IN_List.AddRange(Day_monytransform_in_list_);
                    All_Money_OUT_List.AddRange(Day_payout_list_);
                    All_Money_OUT_List.AddRange(Day_ExchangeOPR_OUTDirection_list);
                    All_Money_OUT_List.AddRange(Day_monytransform_out_list_);
                    double Paysin_realValue, Paysout_realValue;


                    Paysin_realValue = Math.Round(All_Money_IN_List.Sum(x => x.Value / x.ExchangeRate));
                    Paysout_realValue = Math.Round(All_Money_OUT_List.Sum(x => x.Value / x.ExchangeRate));
                    AccountOprDayReportDetail AccountOprDayReportDetail_
                        = new AccountOprDayReportDetail(day_index, new DateTime(year, month, day_index)
                        , Day_payin_list_.Count, Day_payout_list_.Count, day_exchangeopr_list_.Count,
                        Day_monytransform_in_list_.Count ,Day_monytransform_out_list_.Count 
                        , Money_Currency.ConvertMoney_CurrencyList_TOString (All_Money_IN_List), Paysin_realValue
                         , Money_Currency.ConvertMoney_CurrencyList_TOString (All_Money_OUT_List), Paysout_realValue);
                    AccountOprReportDetailList.Add(AccountOprDayReportDetail_);
                }
                
                return AccountOprReportDetailList;
            }
            catch (Exception ee)
            {
                throw new Exception ("GetAccountOprReport_Details_InMonh:" + ee.Message);
                return AccountOprReportDetailList;
            }
        }
        internal List<AccountOprMonthReportDetail> GetAccountOprReport_Details_InYear(MoneyBox moneybox, int year)
        {
            List<AccountOprMonthReportDetail> AccountOprReportDetailList = new List<AccountOprMonthReportDetail>();

            try
            {
                List<MoneyTransFormOPR> year_moneytransform_list =
                  new MoneyTransFormOPRSQL(DB).Get_All_MoneyTransFormOPRList()
                  .Where(x => x.Confirm_User != null && x.MoneyTransFormOPRDate.Year == year ).ToList();
                List<MoneyTransFormOPR> MoneyBox_IN_moneytransform_list = year_moneytransform_list
                                       .Where(x => x.TargetMoneyBox.BoxID == moneybox.BoxID).ToList();
                List<MoneyTransFormOPR> MoneyBox_OUT_moneytransform_list = year_moneytransform_list
                                 .Where(x => x.SourceMoneyBox.BoxID == moneybox.BoxID).ToList();
                List<PayIN> all_payin_list = new PayINSQL(DB).Get_All_PayINList(moneybox );

                List<PayOUT> all_payout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox );

                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox );

                for (int month_index= 1; month_index <= 12; month_index++)
                {
                    List<Money_Currency> Month_monytransform_in_list_
                       = Money_Currency.Get_Money_Currency_List_From_MoneyTransform(MoneyBox_IN_moneytransform_list.Where(x => x.MoneyTransFormOPRDate.Year == year && x.MoneyTransFormOPRDate.Month == month_index).ToList());
                    List<Money_Currency> Month_monytransform_out_list_
                      = Money_Currency.Get_Money_Currency_List_From_MoneyTransform(MoneyBox_OUT_moneytransform_list.Where(x => x.MoneyTransFormOPRDate.Year == year && x.MoneyTransFormOPRDate.Month == month_index).ToList());

                    List<Money_Currency> Month_payin_list_
                       = Money_Currency.Get_Money_Currency_List_From_PayIN(all_payin_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month_index ).ToList());

                    List<Money_Currency> Month_payout_list_
                            = Money_Currency.Get_Money_Currency_List_From_PayOUT(all_payout_list.Where(x => x.PayOprDate.Year == year && x.PayOprDate.Month == month_index ).ToList());
                    List<ExchangeOPR> Month_exchangeopr_list_
                    = allexchangeopr_list.Where(x => x.ExchangeOprDate.Year == year && x.ExchangeOprDate.Month == month_index ).ToList();
                    List<Money_Currency> Month_ExchangeOPR_OUTDirection_list
                        = Money_Currency.Get_Money_Currency_List_From_ExchangeOPR_OUTDirection(Month_exchangeopr_list_);
                    List<Money_Currency> Month_ExchangeOPR_INDirection_list
                        = Money_Currency.Get_Money_Currency_List_From_ExchangeOPR_INDirection(Month_exchangeopr_list_);
                    List<Money_Currency> All_Money_IN_List = new List<Money_Currency>();
                    List<Money_Currency> All_Money_OUT_List = new List<Money_Currency>();
                    All_Money_IN_List.AddRange(Month_payin_list_);
                    All_Money_IN_List.AddRange(Month_ExchangeOPR_INDirection_list);
                    All_Money_IN_List.AddRange(Month_monytransform_in_list_);
                    All_Money_OUT_List.AddRange(Month_payout_list_);
                    All_Money_OUT_List.AddRange(Month_ExchangeOPR_OUTDirection_list);
                    All_Money_OUT_List.AddRange(Month_monytransform_out_list_);
                    double Paysin_realValue, Paysout_realValue;

                    Paysin_realValue = Math.Round(All_Money_IN_List.Sum(x => x.Value / x.ExchangeRate));
                    Paysout_realValue = Math.Round(All_Money_OUT_List.Sum(x => x.Value / x.ExchangeRate));

                    CultureInfo AR_English = new CultureInfo("ar-SY");
                    DateTimeFormatInfo englishInfo = AR_English.DateTimeFormat;
                    string monthName = englishInfo.MonthNames[month_index-1];
                    AccountOprMonthReportDetail AccountOprMonthReportDetail_
                       = new AccountOprMonthReportDetail(month_index, monthName
                       , Month_payin_list_.Count, Month_payout_list_.Count, Month_exchangeopr_list_.Count,
                        Month_monytransform_in_list_.Count, Month_monytransform_out_list_.Count
                       , Money_Currency.ConvertMoney_CurrencyList_TOString(All_Money_IN_List), Paysin_realValue
                        , Money_Currency.ConvertMoney_CurrencyList_TOString(All_Money_OUT_List), Paysout_realValue);
                    AccountOprReportDetailList.Add(AccountOprMonthReportDetail_);
                }
                return AccountOprReportDetailList;
            }
            catch (Exception ee)
            {
                throw new Exception ("GetAccountOprReport_Details_InYear:" + ee.Message);
                return AccountOprReportDetailList;
            }
        }
        internal List<AccountOprYearReportDetail> GetAccountOprReport_Details_InYearRange(MoneyBox moneybox, int year1,int year2)
        {
            List<AccountOprYearReportDetail> AccountOprReportDetailList = new List<AccountOprYearReportDetail>();

            try
            {
                int min_year, max_year;
                if(year1 >year2 )
                {
                    min_year = year2;max_year = year1;
                }
                else
                {
                    min_year = year1; max_year = year2;
                }
                List<MoneyTransFormOPR> year_moneytransform_list =
            new MoneyTransFormOPRSQL(DB).Get_All_MoneyTransFormOPRList()
            .Where(x => x.Confirm_User != null && x.MoneyTransFormOPRDate .Year <=max_year &&x.MoneyTransFormOPRDate .Year >=min_year ).ToList();
                List<MoneyTransFormOPR> MoneyBox_IN_moneytransform_list = year_moneytransform_list
                                       .Where(x => x.TargetMoneyBox.BoxID == moneybox.BoxID).ToList();
                List<MoneyTransFormOPR> MoneyBox_OUT_moneytransform_list = year_moneytransform_list
                                 .Where(x => x.SourceMoneyBox.BoxID == moneybox.BoxID).ToList();
                List<PayIN> all_payin_list = new PayINSQL(DB).Get_All_PayINList(moneybox );

                List<PayOUT> all_payout_list = new PayOUTSQL(DB).Get_All_PaysOUT_List(moneybox );

                List<ExchangeOPR> allexchangeopr_list = new ExchangeOPRSQL(DB).Get_All_ExchangeOPRList(moneybox );

                for (int year_index = min_year ; year_index <= max_year ; year_index++)
                {
                    List<Money_Currency> Year_monytransform_in_list_
                      = Money_Currency.Get_Money_Currency_List_From_MoneyTransform(MoneyBox_IN_moneytransform_list.Where(x => x.MoneyTransFormOPRDate.Year == year_index ).ToList());
                    List<Money_Currency> Year_monytransform_out_list_
                      = Money_Currency.Get_Money_Currency_List_From_MoneyTransform(MoneyBox_OUT_moneytransform_list.Where(x => x.MoneyTransFormOPRDate.Year == year_index ).ToList());

                    List<Money_Currency> Year_payin_list_
                       = Money_Currency.Get_Money_Currency_List_From_PayIN(all_payin_list.Where(x => x.PayOprDate.Year == year_index ).ToList());

                    List<Money_Currency> Year_payout_list_
                            = Money_Currency.Get_Money_Currency_List_From_PayOUT(all_payout_list.Where(x => x.PayOprDate.Year == year_index).ToList());
                    List<ExchangeOPR> Year_exchangeopr_list_
                    = allexchangeopr_list.Where(x => x.ExchangeOprDate.Year == year_index ).ToList();
                    List<Money_Currency> Year_ExchangeOPR_OUTDirection_list
                        = Money_Currency.Get_Money_Currency_List_From_ExchangeOPR_OUTDirection(Year_exchangeopr_list_);
                    List<Money_Currency> Year_ExchangeOPR_INDirection_list
                        = Money_Currency.Get_Money_Currency_List_From_ExchangeOPR_INDirection(Year_exchangeopr_list_);
                    List<Money_Currency> All_Money_IN_List = new List<Money_Currency>();
                    List<Money_Currency> All_Money_OUT_List = new List<Money_Currency>();
                    All_Money_IN_List.AddRange(Year_payin_list_);
                    All_Money_IN_List.AddRange(Year_ExchangeOPR_INDirection_list);
                    All_Money_IN_List.AddRange(Year_monytransform_in_list_);
                    All_Money_OUT_List.AddRange(Year_payout_list_);
                    All_Money_OUT_List.AddRange(Year_ExchangeOPR_OUTDirection_list);
                    All_Money_OUT_List.AddRange(Year_monytransform_out_list_);
                    double Paysin_realValue, Paysout_realValue;

                    Paysin_realValue = Math.Round(All_Money_IN_List.Sum(x => x.Value / x.ExchangeRate));
                    Paysout_realValue = Math.Round(All_Money_OUT_List.Sum(x => x.Value / x.ExchangeRate));


                    AccountOprYearReportDetail AccountOprYearReportDetail_
                       = new AccountOprYearReportDetail(year_index
                       , Year_payin_list_.Count, Year_payout_list_.Count, Year_exchangeopr_list_.Count,
                         Year_monytransform_in_list_.Count, Year_monytransform_out_list_.Count
                       , Money_Currency.ConvertMoney_CurrencyList_TOString(All_Money_IN_List), Paysin_realValue
                        , Money_Currency.ConvertMoney_CurrencyList_TOString(All_Money_OUT_List), Paysout_realValue);

                   
                    AccountOprReportDetailList.Add(AccountOprYearReportDetail_);
                }
                return AccountOprReportDetailList;
            }
            catch (Exception ee)
            {
                throw new Exception ("GetAccountOprReport_Details_InYearRange:" + ee.Message);
                return AccountOprReportDetailList;
            }
        }
        #endregion
        #region Report_Buy
        internal List<Report_Buys_Day_ReportDetail> Get_Report_Buys_Day_ReportDetail(int year, int month, int day)
        {
            List<Report_Buys_Day_ReportDetail> List = new List<Report_Buys_Day_ReportDetail>();
            try
            {

                List<BillBuy> BillBuyList =
                     new BillBuySQL(DB).Get_All_BillBuy_List().Where(x => x.BillDate.Year == year && x.BillDate.Month == month && x.BillDate.Day == day).ToList();
                for (int i = 0; i < BillBuyList.Count; i++)
                {

                    DateTime billdate = BillBuyList[i].BillDate ;
                    uint billid = BillBuyList[i]._Operation.OperationID;
                    string owner = BillBuyList[i]._Contact.ContactName;
                    List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportList = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(BillBuyList[i]._Operation);
                    
                    double amountin = ItemIN_ItemOUTReportList.Sum(x => x._ItemIN.Amount);
                    double consume_amount=0 ;
                    List<ItemOUT> itemoutlist = new List<ItemOUT>();
                    for (int j = 0; j < ItemIN_ItemOUTReportList.Count; j++)
                    {
                        itemoutlist.AddRange(ItemIN_ItemOUTReportList[j].ItemOUTList);
                        double itemin_consumeunit_factor = ItemIN_ItemOUTReportList[j]._ItemIN._ConsumeUnit.Factor;
                        consume_amount
                            += ItemIN_ItemOUTReportList[j].ItemOUTList.Sum(y => (y.Amount * (y._ConsumeUnit.Factor / itemin_consumeunit_factor)));
                    }

                    double amountremain = amountin - consume_amount; ;

                    List<BillAdditionalClause> BillAdditionalClauseList = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(BillBuyList[i]._Operation);
                    int clause_Count = ItemIN_ItemOUTReportList.Count+ BillAdditionalClauseList.Count ;
                    double billvalue = ItemIN_ItemOUTReportList.Sum(x => x._ItemIN._INCost.Value*x._ItemIN .Amount )
                        + BillAdditionalClauseList.Sum (z=>z.Value )-BillBuyList [i].Discount ; 
                    Currency currency = BillBuyList[i]._Currency ;
                    double exchangerate = BillBuyList[i].ExchangeRate ;
                    List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(BillBuyList[i]._Operation);
               
                    string PaysAmount =Money_Currency.ConvertMoney_CurrencyList_TOString (
                Money_Currency.Get_Money_Currency_List_From_PayOUT (payoutlist));
                 
                    double paysremain = billvalue-new OperationSQL (DB).Get_OperationPaysValue_UPON_OperationCurrency(BillBuyList[i]._Operation ) ;
                    double billrealvalue =Math .Round ( billvalue/ BillBuyList[i].ExchangeRate ,2);
                    double bill_Pays_realvalue = payoutlist.Sum (b=>b.Value/b.ExchangeRate );
                  
                    string itemsoutvalue =
                        Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_ItemOUT (itemoutlist ));
 
                    double itemsoutRealvalue = itemoutlist.Sum (n=>n._OUTValue.Value /n._OUTValue.ExchangeRate );
                   
                    List<PayIN> billbuy_returns_PayIN = new BillBuySQL(DB).Get_Billbuy__Returns_Pays(BillBuyList[i]._Operation.OperationID );
                    List<Money_Currency> Money_CurrencyList = Money_Currency.Get_Money_Currency_List_From_PayIN(billbuy_returns_PayIN);
                    string bill_pays_returns_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_CurrencyList) ;
                     double bill_pays_returns_realvalue = Money_CurrencyList.Sum (x=>x.Value /x.ExchangeRate );
                    Report_Buys_Day_ReportDetail Report_Buys_Day_ReportDetail_
                        = new Report_Buys_Day_ReportDetail(billdate, billid, owner,
                        clause_Count, amountin, amountremain, billvalue, currency.CurrencyID,currency .CurrencyName,currency.CurrencySymbol , exchangerate
                        , PaysAmount, paysremain, billrealvalue, bill_Pays_realvalue, itemsoutvalue, itemsoutRealvalue
                        , bill_pays_returns_value, bill_pays_returns_realvalue);
                    List.Add(Report_Buys_Day_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_Buys_Day_ReportDetail:"+ee.Message );
                return List;
            }
        }
        internal List<Report_Buys_Month_ReportDetail> Get_Report_Buys_Month_ReportDetail(int year, int month)
        {
            List<Report_Buys_Month_ReportDetail> List = new List<Report_Buys_Month_ReportDetail>();
            try
            {

                List<BillBuy> AllBillBuyList =
                     new BillBuySQL(DB).Get_All_BillBuy_List();
                for (int day_index =1; day_index <= DateTime.DaysInMonth(year ,month ); day_index++)
                {
                    List<BillBuy> BillBuyList = AllBillBuyList.Where(x => x.BillDate.Year == year && x.BillDate.Month == month && x.BillDate.Day == day_index).ToList();
                    List<PayOUT> BillBuy_paysoutlist = new List<PayOUT>();
                    List<PayIN> billbuy_returns_PayIN = new List<PayIN>();
                    DateTime daydate = new DateTime(year, month, day_index); 
                    int bills_count = BillBuyList.Count ;
                    List<Money_Currency> Money_Currency_Billsvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysremain = new List<Money_Currency>();
                    List<ItemIN> ItemINList = new List<ItemIN>();
                    List<ItemOUT> ItemOUTList = new List<ItemOUT>();
                    double amountin =0;
                    double consume_amount = 0;
                    double bills_pays_remain_upon_billcurrency=0;
                    for (int j=0;j< BillBuyList.Count;j++)
                    {
                        List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportlist = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(BillBuyList[j]._Operation);
                        BillBuy_paysoutlist.AddRange( new PayOUTSQL(DB).GetPaysOUT_List(BillBuyList[j]._Operation));
                        OperationSQL OperationSQL_ = new OperationSQL(DB);
                        double billvalue = OperationSQL_.Get_OperationValue(BillBuyList[j]._Operation );
                        double paysvalue_upon_operationcurrency =  OperationSQL_.Get_OperationPaysValue_UPON_OperationCurrency(BillBuyList[j]._Operation);
                        bills_pays_remain_upon_billcurrency += billvalue - paysvalue_upon_operationcurrency;
                        Money_Currency_Billsvalues.Add(new Money_Currency(BillBuyList[j]._Currency, billvalue
                            , BillBuyList[j].ExchangeRate));
                        Money_Currency_Paysremain.Add(new Money_Currency(BillBuyList[j]._Currency, billvalue-paysvalue_upon_operationcurrency 
                            , BillBuyList[j].ExchangeRate));
                        billbuy_returns_PayIN.AddRange( new BillBuySQL(DB).Get_Billbuy__Returns_Pays(BillBuyList[j]._Operation.OperationID));
                        ItemINList.AddRange(ItemIN_ItemOUTReportlist.Select(x => x._ItemIN));
                        for (int k = 0; k < ItemIN_ItemOUTReportlist.Count; k++)
                        {
                            amountin += ItemIN_ItemOUTReportlist[k]._ItemIN.Amount;
                            double itemin_consumeunit_factor = ItemIN_ItemOUTReportlist[k]._ItemIN._ConsumeUnit.Factor;
                            consume_amount
                            += ItemIN_ItemOUTReportlist[k].ItemOUTList.Sum(y => (y.Amount * (y._ConsumeUnit.Factor / itemin_consumeunit_factor)));
                            ItemOUTList.AddRange(ItemIN_ItemOUTReportlist[k].ItemOUTList);

                        }
                    }


                    Money_Currency_Paysvalues = Money_Currency.Get_Money_Currency_List_From_PayOUT(BillBuy_paysoutlist);
                    double amountremain = amountin - consume_amount; ;
                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Billsvalues); 
                    string bills_pays_value
                        = Money_Currency.ConvertMoney_CurrencyList_TOString(
                            Money_Currency_Paysvalues);
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysremain);
                    double bills_realvalue =System.Math .Round ( Money_Currency_Billsvalues.Sum(x => x.Value / x.ExchangeRate),3);
                    double bills_pays_realvalue = System.Math.Round(Money_Currency_Paysvalues.Sum(x => x.Value / x.ExchangeRate),3);
                    List<Money_Currency> Money_Currency_ItemsOut = Money_Currency.Get_Money_Currency_List_From_ItemOUT(ItemOUTList);
                    string bills_itemsout_value = 
                        Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_ItemsOut);
                    double bills_itemsout_realvalue = System.Math.Round(Money_Currency_ItemsOut.Sum (x=>x.Value/x.ExchangeRate ),3);
                    List<Money_Currency> Money_Currency_Return_Pays = Money_Currency.Get_Money_Currency_List_From_PayIN(billbuy_returns_PayIN);
                    string bills_pays_returns_value
                        = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Return_Pays);
                    double bills_pays_returns_realvalue = System.Math.Round(Money_Currency_Return_Pays.Sum (x=>x.Value/x.ExchangeRate ),3);



                    
                    Report_Buys_Month_ReportDetail Report_Buys_Day_ReportDetail_
                        = new Report_Buys_Month_ReportDetail(day_index , daydate, bills_count
                        ,amountin,amountremain,bills_value ,bills_pays_value,bills_pays_remain
                        ,bills_pays_remain_upon_billcurrency,bills_realvalue ,bills_pays_realvalue
                        ,bills_itemsout_value,bills_itemsout_realvalue,bills_pays_returns_value,bills_pays_returns_realvalue);
                    List.Add(Report_Buys_Day_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_Buys_Month_ReportDetail:"+ee.Message );
                return List;
            }
        }
        internal List<Report_Buys_Year_ReportDetail> Get_Report_Buys_Year_ReportDetail(int year)
        {
            List<Report_Buys_Year_ReportDetail> List = new List<Report_Buys_Year_ReportDetail>();
            try
            {

                List<BillBuy> AllBillBuyList =
                     new BillBuySQL(DB).Get_All_BillBuy_List();
                for (int month_index = 1; month_index <= 12; month_index++)
                {
                    List<BillBuy> BillBuyList = AllBillBuyList.Where(x => x.BillDate.Year == year && x.BillDate.Month == month_index ).ToList();
                    List<PayOUT> BillBuy_paysoutlist =new List<PayOUT> () ;
                    List<PayIN> billbuy_returns_PayIN = new List<PayIN>();
                  
                    int bills_count = BillBuyList.Count;
                    List<Money_Currency> Money_Currency_Billsvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysremain = new List<Money_Currency>();
                    List<ItemIN> ItemINList = new List<ItemIN>();
                    List<ItemOUT> ItemOUTList = new List<ItemOUT>();
                    double amountin = 0;
                    double consume_amount = 0;
                    double bills_pays_remain_upon_billcurrency = 0;
                    for (int j = 0; j < BillBuyList.Count; j++)
                    {
                        List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportlist = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(BillBuyList[j]._Operation);
                        BillBuy_paysoutlist.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(BillBuyList[j]._Operation));
                        OperationSQL OperationSQL_ = new OperationSQL(DB);
                        double billvalue = OperationSQL_.Get_OperationValue(BillBuyList[j]._Operation);
                        double paysvalue_upon_operationcurrency = OperationSQL_.Get_OperationPaysValue_UPON_OperationCurrency(BillBuyList[j]._Operation);
                        bills_pays_remain_upon_billcurrency += billvalue - paysvalue_upon_operationcurrency;
                        Money_Currency_Billsvalues.Add(new Money_Currency(BillBuyList[j]._Currency, billvalue
                            , BillBuyList[j].ExchangeRate));
                        Money_Currency_Paysremain.Add(new Money_Currency(BillBuyList[j]._Currency, billvalue - paysvalue_upon_operationcurrency
                            , BillBuyList[j].ExchangeRate));
                        billbuy_returns_PayIN.AddRange(new BillBuySQL(DB).Get_Billbuy__Returns_Pays(BillBuyList[j]._Operation.OperationID));
                        ItemINList.AddRange(ItemIN_ItemOUTReportlist.Select(x => x._ItemIN));
                        for (int k = 0; k < ItemIN_ItemOUTReportlist.Count; k++)
                        {
                            amountin += ItemIN_ItemOUTReportlist[k]._ItemIN.Amount;
                            double itemin_consumeunit_factor = ItemIN_ItemOUTReportlist[k]._ItemIN._ConsumeUnit.Factor;
                            consume_amount
                            += ItemIN_ItemOUTReportlist[k].ItemOUTList.Sum(y => (y.Amount * (y._ConsumeUnit.Factor / itemin_consumeunit_factor)));
                            ItemOUTList.AddRange(ItemIN_ItemOUTReportlist[k].ItemOUTList);

                        }
                    }


                    Money_Currency_Paysvalues = Money_Currency.Get_Money_Currency_List_From_PayOUT(BillBuy_paysoutlist);

                    double amountremain = amountin - consume_amount; ;
                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Billsvalues);
                    string bills_pays_value
                        = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(BillBuy_paysoutlist));
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysremain);
                    double bills_realvalue = System.Math.Round(Money_Currency_Billsvalues.Sum(x => x.Value / x.ExchangeRate), 3);
                    double bills_pays_realvalue = System.Math.Round(Money_Currency_Paysvalues.Sum(x => x.Value / x.ExchangeRate), 3);
                    List<Money_Currency> Money_Currency_ItemsOut = Money_Currency.Get_Money_Currency_List_From_ItemOUT(ItemOUTList);
                    string bills_itemsout_value =
                        Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_ItemsOut);
                    double bills_itemsout_realvalue = System.Math.Round(Money_Currency_ItemsOut.Sum(x => x.Value / x.ExchangeRate), 3);
                    List<Money_Currency> Money_Currency_Return_Pays = Money_Currency.Get_Money_Currency_List_From_PayIN(billbuy_returns_PayIN);
                    string bills_pays_returns_value
                        = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Return_Pays);
                    double bills_pays_returns_realvalue = System.Math.Round(Money_Currency_Return_Pays.Sum(x => x.Value / x.ExchangeRate),3);


                    CultureInfo AR_English = new CultureInfo("ar-SY");
                    DateTimeFormatInfo englishInfo = AR_English.DateTimeFormat;
                    string monthName = englishInfo.MonthNames[month_index - 1];

                    Report_Buys_Year_ReportDetail Report_Buys_Year_ReportDetail_
                        = new Report_Buys_Year_ReportDetail(month_index , monthName, bills_count
                        , amountin, amountremain, bills_value, bills_pays_value, bills_pays_remain
                        , bills_pays_remain_upon_billcurrency, bills_realvalue, bills_pays_realvalue
                        , bills_itemsout_value, bills_itemsout_realvalue, bills_pays_returns_value, bills_pays_returns_realvalue);
                    List.Add(Report_Buys_Year_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_Buys_Year_ReportDetail:"+ee.Message );
                return List;
            }
        }
        internal List<Report_Buys_YearRange_ReportDetail> Get_Report_Buys_YearRange_ReportDetail(int year1,int year2)
        {
            List<Report_Buys_YearRange_ReportDetail> List = new List<Report_Buys_YearRange_ReportDetail>();
            int min_year, max_year;
            if(year1 >year2 )
            {
                min_year = year2;
                max_year = year1;
            }
            else
            {
                min_year = year1;
                max_year = year2;
            }
            try
            {

                List<BillBuy> AllBillBuyList =
                     new BillBuySQL(DB).Get_All_BillBuy_List();
                for (int year_index = min_year ; year_index <= max_year ; year_index++)
                {
                    List<BillBuy> BillBuyList = AllBillBuyList.Where(x => x.BillDate.Year==year_index ).ToList();
                    List<PayOUT> BillBuy_paysoutlist = new List<PayOUT>(); ;
                    List<PayIN> billbuy_returns_PayIN = new List<PayIN>();

                    int bills_count = BillBuyList.Count;
                    List<Money_Currency> Money_Currency_Billsvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysremain = new List<Money_Currency>();
                    List<ItemIN> ItemINList = new List<ItemIN>();
                    List<ItemOUT> ItemOUTList = new List<ItemOUT>();
                    double amountin = 0;
                    double consume_amount = 0;
                    double bills_pays_remain_upon_billcurrency = 0;
                    for (int j = 0; j < BillBuyList.Count; j++)
                    {
                        List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportlist = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(BillBuyList[j]._Operation);
                        BillBuy_paysoutlist.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(BillBuyList[j]._Operation));
                        OperationSQL OperationSQL_ = new OperationSQL(DB);
                        double billvalue = OperationSQL_.Get_OperationValue(BillBuyList[j]._Operation);
                        double paysvalue_upon_operationcurrency = OperationSQL_.Get_OperationPaysValue_UPON_OperationCurrency(BillBuyList[j]._Operation);
                        bills_pays_remain_upon_billcurrency += billvalue - paysvalue_upon_operationcurrency;
                        Money_Currency_Billsvalues.Add(new Money_Currency(BillBuyList[j]._Currency, billvalue
                            , BillBuyList[j].ExchangeRate));
                        Money_Currency_Paysremain.Add(new Money_Currency(BillBuyList[j]._Currency, billvalue - paysvalue_upon_operationcurrency
                            , BillBuyList[j].ExchangeRate));
                        billbuy_returns_PayIN.AddRange(new BillBuySQL(DB).Get_Billbuy__Returns_Pays(BillBuyList[j]._Operation.OperationID));
                        ItemINList.AddRange(ItemIN_ItemOUTReportlist.Select(x => x._ItemIN));
                        for (int k = 0; k < ItemIN_ItemOUTReportlist.Count; k++)
                        {
                            amountin += ItemIN_ItemOUTReportlist[k]._ItemIN.Amount;
                            double itemin_consumeunit_factor = ItemIN_ItemOUTReportlist[k]._ItemIN._ConsumeUnit.Factor;
                            consume_amount
                            += ItemIN_ItemOUTReportlist[k].ItemOUTList.Sum(y => (y.Amount * (y._ConsumeUnit.Factor / itemin_consumeunit_factor)));
                            ItemOUTList.AddRange(ItemIN_ItemOUTReportlist[k].ItemOUTList);

                        }
                    }


                    Money_Currency_Paysvalues = Money_Currency.Get_Money_Currency_List_From_PayOUT(BillBuy_paysoutlist);

                    double amountremain = amountin - consume_amount; ;
                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Billsvalues);
                    string bills_pays_value
                        = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(BillBuy_paysoutlist));
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysremain);
                    double bills_realvalue = System.Math.Round(Money_Currency_Billsvalues.Sum(x => x.Value / x.ExchangeRate), 3);
                    double bills_pays_realvalue = System.Math.Round(Money_Currency_Paysvalues.Sum(x => x.Value / x.ExchangeRate), 3);
                    List<Money_Currency> Money_Currency_ItemsOut = Money_Currency.Get_Money_Currency_List_From_ItemOUT(ItemOUTList);
                    string bills_itemsout_value =
                        Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_ItemsOut);
                    double bills_itemsout_realvalue = System.Math.Round(Money_Currency_ItemsOut.Sum(x => x.Value / x.ExchangeRate), 3);
                    List<Money_Currency> Money_Currency_Return_Pays = Money_Currency.Get_Money_Currency_List_From_PayIN(billbuy_returns_PayIN);
                    string bills_pays_returns_value
                        = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Return_Pays);
                    double bills_pays_returns_realvalue = System.Math.Round(Money_Currency_Return_Pays.Sum(x => x.Value / x.ExchangeRate),3);




                    Report_Buys_YearRange_ReportDetail Report_Buys_YearRange_ReportDetail_
                        = new Report_Buys_YearRange_ReportDetail(year_index, bills_count
                        , amountin, amountremain, bills_value, bills_pays_value, bills_pays_remain
                        , bills_pays_remain_upon_billcurrency, bills_realvalue, bills_pays_realvalue
                        , bills_itemsout_value, bills_itemsout_realvalue, bills_pays_returns_value, bills_pays_returns_realvalue);
                    List.Add(Report_Buys_YearRange_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_Buys_YearRange_ReportDetail:"+ee.Message );
                return List;
            }
        }
        internal List < Report_Buys_Month_ReportDetail> Get_Report_Buys_Day_Report(int year, int month, int day)
        {
            try
            {
                return  Get_Report_Buys_Month_ReportDetail(year, month).Where (x=>x.DayID ==day ).ToList ();
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_Buys_Day_Report:"+ee.Message );
            }
        }
        internal List < Report_Buys_Year_ReportDetail> Get_Report_Buys_Month_Report(int year, int month)
        {
            try
            {
                return  Get_Report_Buys_Year_ReportDetail(year).Where(x => x.MonthNO == month ).ToList();

            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_Buys_Month_Report:" + ee.Message);
            }
        }
        internal List < Report_Buys_YearRange_ReportDetail> Get_Report_Buys_Year_Report(int year)
        {
            try
            {
                return  Get_Report_Buys_YearRange_ReportDetail(year,year).Where(x => x.YearNO == year ).ToList();

            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_Buys_Year_Report:" + ee.Message);
            }
        }
        #endregion
        #region Report_PayOrder
        internal List<Report_PayOrders_Day_ReportDetail> Get_Report_PayOrders_Day_ReportDetail(int year, int month, int day)
        {
            int k = 0;
            List<Report_PayOrders_Day_ReportDetail> List = new List<Report_PayOrders_Day_ReportDetail>();
            try
            {

                List<EmployeePayOrder> EmployeePayOrderList
                    = new EmployeePayOrderSQL(DB).GetPayPayOrders_List().Where (x=>x.PayOrderDate.Year ==year &&x.PayOrderDate.Month ==month &&x.PayOrderDate.Day==day ).ToList();
                k = 1;
                for (int i = 0; i < EmployeePayOrderList.Count; i++)
                {

                    DateTime payordertime = EmployeePayOrderList[i].PayOrderDate ;
                    bool payordertype;
                    uint payorderid = EmployeePayOrderList[i].PayOrderID;
                    string payorderdesc ="";

                    k = 2;
                    if (EmployeePayOrderList[i]._SalarysPayOrder != null)
                    {
                        payordertype = Report_PayOrders_Day_ReportDetail.TYPE_SALARY_PAY_ODER;
                        payorderdesc = "صرف راتب شهر :"
                            + EmployeePayOrderList[i]._SalarysPayOrder.ExecuteMonth + " سنة " + EmployeePayOrderList[i]._SalarysPayOrder.ExecuteYear;
                    }
                    else
                    {
                        payordertype = Report_PayOrders_Day_ReportDetail.TYPE_PAY_ODER ;
                        payorderdesc = EmployeePayOrderList[i].PayOrderDesc;
                    }

                    k = 3;
                    uint employeeid = EmployeePayOrderList[i]._Employee.EmployeeID; ;
                    string employeename = EmployeePayOrderList[i]._Employee.EmployeeName;

                    double payordervalue = EmployeePayOrderList[i].Value;
                    k = 4;
                    Currency currency = EmployeePayOrderList[i]._Currency ;
                    double exchangerate = EmployeePayOrderList[i].ExchangeRate;
                    List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                    List<Money_Currency> Money_Currency_pays = Money_Currency.Get_Money_Currency_List_From_PayOUT(payoutlist);
                    k = 5;
                    string PaysAmount = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_pays);
                    double paysremain = payordervalue- new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                    double Payorder_realvalue = payordervalue/exchangerate ; ;
                    double Payorder_Pays_realvalue = payoutlist.Sum(x => x.Value / x.ExchangeRate);
                    k = 6;

                    Report_PayOrders_Day_ReportDetail Report_PayOrders_Day_ReportDetail_
                        = new Report_PayOrders_Day_ReportDetail(payordertime, payordertype, payorderid
                        , payorderdesc, employeeid, employeename, payordervalue, currency.CurrencyID, currency.CurrencyName, currency.CurrencySymbol, exchangerate
                        , PaysAmount, paysremain, Payorder_realvalue, Payorder_Pays_realvalue);
                    k = 7;
                    List.Add(Report_PayOrders_Day_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_PayOrders_Day_ReportDetail:K"+k+","+ee.Message );
            }
        }
        internal List<Report_PayOrders_Month_ReportDetail> Get_Report_PayOrders_Month_ReportDetail(int year, int month)
        {
            List<Report_PayOrders_Month_ReportDetail> List = new List<Report_PayOrders_Month_ReportDetail>();
            try
            {


                for (int day_index =1; day_index <= DateTime.DaysInMonth (year,month ); day_index++)
                {
                    List<EmployeePayOrder> EmployeePayOrderList
       = new EmployeePayOrderSQL(DB).GetPayPayOrders_List().Where(x => x.PayOrderDate.Year == year && x.PayOrderDate.Month == month && x.PayOrderDate.Day == day_index).ToList();
                    int dayno = day_index;
                    DateTime daydate = new DateTime(year, month, day_index) ;
                    int salarys_payorder_Count = EmployeePayOrderList.Where (x=>x._SalarysPayOrder!=null ).ToList ().Count ;
                    int others_payorder_Count = EmployeePayOrderList.Where(x => x._SalarysPayOrder == null).ToList().Count;
                    List<Money_Currency> Money_Currency_PayOrders_Value = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysremain = new List<Money_Currency>();
                    double payorders_pays_remain_upon_payordercurrency=0;
                    for (int j=0;j< EmployeePayOrderList.Count;j++)
                    {
                        Operation operation = new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[j].PayOrderID);
                        Money_Currency_PayOrders_Value.Add(new Money_Currency(EmployeePayOrderList[j]._Currency, EmployeePayOrderList[j].Value , 
                            EmployeePayOrderList[j].ExchangeRate));
                        List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(operation);
                        Money_Currency_Paysvalues.AddRange(Money_Currency.Get_Money_Currency_List_From_PayOUT (payoutlist));
                        double paysvalueupon_payordercurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(operation);
                        payorders_pays_remain_upon_payordercurrency += paysvalueupon_payordercurrency;
                        Money_Currency_Paysremain.Add(new Money_Currency(EmployeePayOrderList[j]._Currency, EmployeePayOrderList[j].Value - payorders_pays_remain_upon_payordercurrency,  EmployeePayOrderList[j].ExchangeRate ));
                    }

                    string payorders_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_PayOrders_Value);

                    string payorders_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysvalues);
                    string payorders_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysremain);
                    double payorders_realvalue = Money_Currency_PayOrders_Value.Sum (x=>x.Value /x.ExchangeRate );
                    double payorders_pays_realvalue = Money_Currency_Paysvalues.Sum(x => x.Value / x.ExchangeRate);

                    Report_PayOrders_Month_ReportDetail Report_PayOrders_Month_ReportDetail_
                        = new Report_PayOrders_Month_ReportDetail(dayno, daydate, salarys_payorder_Count, others_payorder_Count, payorders_value
                        , payorders_pays_value, payorders_pays_remain
                        , payorders_pays_remain_upon_payordercurrency, payorders_realvalue, payorders_pays_realvalue);
                    List.Add(Report_PayOrders_Month_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_PayOrders_Month_ReportDetail:"+ee.Message );
            }
        }
        internal List<Report_PayOrders_Year_ReportDetail> Get_Report_PayOrders_Year_ReportDetail(int year)
        {
            List<Report_PayOrders_Year_ReportDetail> List = new List<Report_PayOrders_Year_ReportDetail>();
            try
            {
                List<EmployeePayOrder> AllEmployeePayOrderList
      = new EmployeePayOrderSQL(DB).GetPayPayOrders_List();
                for (int i = 1; i <= 12; i++)
                {
                    List<EmployeePayOrder> EmployeePayOrderList
                            = AllEmployeePayOrderList.Where(x => x.PayOrderDate.Year == year && x.PayOrderDate.Month == i).ToList();
                    int monthno = i;
                    int salarys_payorder_Count = EmployeePayOrderList.Where(x => x._SalarysPayOrder != null).ToList().Count;
                    int others_payorder_Count = EmployeePayOrderList.Where(x => x._SalarysPayOrder == null).ToList().Count;
                    List<Money_Currency> Money_Currency_PayOrders_Value = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysremain = new List<Money_Currency>();
                    double payorders_pays_remain_upon_payordercurrency = 0;
                    for (int j = 0; j < EmployeePayOrderList.Count; j++)
                    {
                        Operation operation = new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[j].PayOrderID);
                        Money_Currency_PayOrders_Value.Add(new Money_Currency(EmployeePayOrderList[j]._Currency, EmployeePayOrderList[j].Value, EmployeePayOrderList[j].ExchangeRate));
                        List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(operation);
                        Money_Currency_Paysvalues.AddRange(Money_Currency.Get_Money_Currency_List_From_PayOUT(payoutlist));
                        double paysvalueupon_payordercurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(operation);
                        payorders_pays_remain_upon_payordercurrency += paysvalueupon_payordercurrency;
                        Money_Currency_Paysremain.Add(new Money_Currency(EmployeePayOrderList[j]._Currency, EmployeePayOrderList[j].Value - payorders_pays_remain_upon_payordercurrency, EmployeePayOrderList[j].ExchangeRate));
                    }
                    string payorders_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_PayOrders_Value);

                    string payorders_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysvalues);
                    string payorders_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysremain);
                    double payorders_realvalue = Money_Currency_PayOrders_Value.Sum(x => x.Value / x.ExchangeRate);
                    double payorders_pays_realvalue = Money_Currency_Paysvalues.Sum(x => x.Value / x.ExchangeRate);

                    CultureInfo AR_English = new CultureInfo("ar-SY");
                    DateTimeFormatInfo englishInfo = AR_English.DateTimeFormat;
                    string monthName = englishInfo.MonthNames[monthno - 1];
                    Report_PayOrders_Year_ReportDetail Report_PayOrders_Year_ReportDetail_
                        = new Report_PayOrders_Year_ReportDetail(monthno, monthName, salarys_payorder_Count, others_payorder_Count, payorders_value
                        , payorders_pays_value, payorders_pays_remain
                        , payorders_pays_remain_upon_payordercurrency, payorders_realvalue, payorders_pays_realvalue);

                    List.Add(Report_PayOrders_Year_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_PayOrders_Year_ReportDetail:"+ee.Message );
            }
        }
        internal List<Report_PayOrders_YearRange_ReportDetail> Get_Report_PayOrders_YearRange_ReportDetail(int year1, int year2)
        {
            List<Report_PayOrders_YearRange_ReportDetail> List = new List<Report_PayOrders_YearRange_ReportDetail>();
            try
            {
                int min_year, max_year;
                if(year1 >year2)
                {
                    min_year = year2;
                    max_year = year1;
                }
                else
                {
                    min_year = year1;
                    max_year = year2;
                }

                List<EmployeePayOrder> AllEmployeePayOrderList
                        = new EmployeePayOrderSQL(DB).GetPayPayOrders_List();

                for (int i = min_year ; i <= max_year ; i++)
                {

                    List<EmployeePayOrder> EmployeePayOrderList=AllEmployeePayOrderList.Where(x => x.PayOrderDate.Year == i ).ToList();
                    int yearno = i;
                    int salarys_payorder_Count = EmployeePayOrderList.Where(x => x._SalarysPayOrder != null).ToList().Count;
                    int others_payorder_Count = EmployeePayOrderList.Where(x => x._SalarysPayOrder == null).ToList().Count;
                    List<Money_Currency> Money_Currency_PayOrders_Value = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysvalues = new List<Money_Currency>();
                    List<Money_Currency> Money_Currency_Paysremain = new List<Money_Currency>();
                    double payorders_pays_remain_upon_payordercurrency = 0;
                    for (int j = 0; j < EmployeePayOrderList.Count; j++)
                    {
                        Operation operation = new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[j].PayOrderID);
                        Money_Currency_PayOrders_Value.Add(new Money_Currency(EmployeePayOrderList[j]._Currency, EmployeePayOrderList[j].Value, EmployeePayOrderList[j].ExchangeRate));
                        List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(operation);
                        Money_Currency_Paysvalues.AddRange(Money_Currency.Get_Money_Currency_List_From_PayOUT(payoutlist));
                        double paysvalueupon_payordercurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(operation);
                        payorders_pays_remain_upon_payordercurrency += paysvalueupon_payordercurrency;
                        Money_Currency_Paysremain.Add(new Money_Currency(EmployeePayOrderList[j]._Currency, EmployeePayOrderList[j].Value - payorders_pays_remain_upon_payordercurrency, EmployeePayOrderList[j].ExchangeRate));
                    }
                    string payorders_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_PayOrders_Value);

                    string payorders_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysvalues);
                    string payorders_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysremain);
                    double payorders_realvalue = Money_Currency_PayOrders_Value.Sum(x => x.Value / x.ExchangeRate);
                    double payorders_pays_realvalue = Money_Currency_Paysvalues.Sum(x => x.Value / x.ExchangeRate);

                    Report_PayOrders_YearRange_ReportDetail Report_PayOrders_YearRange_ReportDetail_
                        = new Report_PayOrders_YearRange_ReportDetail(yearno, salarys_payorder_Count, others_payorder_Count, payorders_value
                        , payorders_pays_value, payorders_pays_remain
                        , payorders_pays_remain_upon_payordercurrency, payorders_realvalue, payorders_pays_realvalue);
                    List.Add(Report_PayOrders_YearRange_ReportDetail_);
                }
                return List;
            }
            catch (Exception ee)
            {
                throw new Exception (" Get_Report_PayOrders_YearRange_ReportDetail: " + ee.Message);
            }
        }
        internal List < Report_PayOrders_Month_ReportDetail> Get_Report_PayOrders_Day_Report(int year, int month, int day)
        {
            try
            {
                return  Get_Report_PayOrders_Month_ReportDetail(year, month).Where(x => x.DayID == day).ToList();

            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_PayOrders_Day_Report:" + ee.Message);
            }
        }
        internal List < Report_PayOrders_Year_ReportDetail > Get_Report_PayOrders_Month_Report(int year, int month)
        {
            try
            {
                return  Get_Report_PayOrders_Year_ReportDetail(year).Where(x => x.MonthNO == month).ToList();
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_PayOrders_Month_Report:" + ee.Message);
            }
        }
        internal List < Report_PayOrders_YearRange_ReportDetail > Get_Report_PayOrders_Year_Report(int year)
        {
            try
            {
                return  Get_Report_PayOrders_YearRange_ReportDetail(year, year).Where(x => x.YearNO == year).ToList();

            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_PayOrders_Year_Report:" + ee.Message);
            }
        }
        #endregion
        #region Report_Sell
        internal List<Report_Sells_Day_ReportDetail> Get_Report_Sells_Day_ReportDetail(int year, int month, int day)
        {
            List<Report_Sells_Day_ReportDetail> List = new List<Report_Sells_Day_ReportDetail>();
            try
            {

                List<BillSell> AllBillSell = new BillSellSQL(DB).Get_All_BillSell_List();
                List<BillSell> BillSellList = AllBillSell.Where(x => x.BillDate.Year == year && x.BillDate.Month == month && x.BillDate.Day == day).ToList();
                for (int i = 0; i < BillSellList.Count; i++)
                {
                    List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(BillSellList[i]._Operation);
                    List<BillAdditionalClause> additionalclauselist = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(BillSellList[i]._Operation);
                    List<Money_Currency> payinlist = new PayINSQL(DB).GetPayINList_As_Money_Currency(BillSellList[i]._Operation);
                    DateTime billdate = BillSellList[i].BillDate;
                    uint billid = BillSellList[i]._Operation.OperationID;
                    string selltype = BillSellList[i]._SellType.SellTypeName;
                    string owner = BillSellList[i]._Contact.ContactName;
                    int clause_Count = itemoutlist.Count + additionalclauselist.Count;
                    double billvalue = itemoutlist.Sum(x => x._OUTValue.Value*x.Amount ) + additionalclauselist.Sum(x => x.Value)-BillSellList [i].Discount ;
                    Currency currency = BillSellList[i]._Currency;
                    double exchangerate = BillSellList[i].ExchangeRate;
                    int PaysCount = payinlist.Count;
                    string PaysAmount = Money_Currency.ConvertMoney_CurrencyList_TOString(payinlist);
                    double remain = billvalue - new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillSellList[i]._Operation); 
                    //List<ItemIN> sourceitemin = itemoutlist.Select(x => x._ItemIN).ToList();
                    List<Money_Currency> itemin_MOney_Currency = Money_Currency.Get_Money_Currency_List_From_ItemIN_By_ItemOUTList(itemoutlist );
                    string bill_source_itemsin_cost = Money_Currency.ConvertMoney_CurrencyList_TOString(itemin_MOney_Currency);
                    double bill_source_itemsin_realcost =System.Math.Round ( itemin_MOney_Currency.Sum(x => x.Value / x.ExchangeRate),3);
                    double itemsout_realvalue = System.Math.Round(itemoutlist.Sum(x => x._OUTValue.Value*x.Amount  / x._OUTValue.ExchangeRate),3);
                    double real_pays = System.Math.Round(payinlist.Sum(x => x.Value / x.ExchangeRate),3);

                    Report_Sells_Day_ReportDetail Report_Sells_Day_ReportDetail_
                        = new Report_Sells_Day_ReportDetail(billdate, billid, selltype, owner,
                        clause_Count, billvalue, currency.CurrencyID, currency.CurrencyName, currency.CurrencySymbol, exchangerate, PaysCount, PaysAmount, remain
                        , bill_source_itemsin_cost, bill_source_itemsin_realcost, itemsout_realvalue, real_pays);
                    List.Add(Report_Sells_Day_ReportDetail_);
                }
                return List;
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_Sells_Day_ReportDetail:"+ee.Message );
                return List;
            }
        }
         internal List<Report_Sells_Month_ReportDetail> Get_Report_Sells_Month_ReportDetail(int year, int month)
        {
            List<Report_Sells_Month_ReportDetail> List = new List<Report_Sells_Month_ReportDetail>();
            try
            {
               
                List<BillSell> AllBillSell = new BillSellSQL(DB).Get_All_BillSell_List();
                
                for (int day  = 1; day <= DateTime .DaysInMonth (year,month ); day++)
                {
                    List<BillSell> BillSellList = AllBillSell.Where(x => x.BillDate.Year == year && x.BillDate.Month == month && x.BillDate.Day == day).ToList();

                    int dayno = day;
                    DateTime daydate = new DateTime (year ,month ,day );
                    int bills_count = BillSellList.Count ;
                   

                    List<Money_Currency> itemoutlist_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> Sourceiteminlist_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> additionalclausesList_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> billvalue_MoneyCurrency = new List<Money_Currency>();
                    List<Money_Currency> paysAmount_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysRemain_money_Currency = new List<Money_Currency>();
                    
                    double bills_pays_remain_upon_billcurrency =0;

                    for (int i=0;i<BillSellList .Count;i++)
                    {
                        List<ItemOUT> itemoutlist = new List<ItemOUT>();
                        //List<ItemIN> sourceItemIN = new List<ItemIN>();

                        itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(BillSellList[i]._Operation);
                        //sourceItemIN .AddRange (itemoutlist.Select(x => x._ItemIN).ToList ());
                        Sourceiteminlist_Money_Currency.AddRange(Money_Currency.Get_Money_Currency_List_From_ItemIN_By_ItemOUTList (itemoutlist));
                        itemoutlist_Money_Currency.AddRange(Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist));
                        additionalclausesList_Money_Currency.AddRange(new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses_AS_Money_Currency(BillSellList[i]._Currency , BillSellList[i].ExchangeRate , BillSellList[i]._Operation));
                        double billvalue =new  OperationSQL(DB).Get_OperationValue(BillSellList[i]._Operation);
                        double pays_remain = billvalue - new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillSellList[i]._Operation);
                        bills_pays_remain_upon_billcurrency += pays_remain;
                        paysRemain_money_Currency.Add(new Money_Currency(BillSellList [i]._Currency ,pays_remain ,BillSellList[i].ExchangeRate ));
                        billvalue_MoneyCurrency.Add(new Money_Currency(BillSellList[i]._Currency
                            , billvalue, BillSellList[i].ExchangeRate));
                        paysAmount_money_Currency.AddRange(new PayINSQL(DB).GetPayINList_As_Money_Currency(BillSellList[i]._Operation));
                    }
                    int bills_clause_count = itemoutlist_Money_Currency.Count + additionalclausesList_Money_Currency.Count ;
                    //billvalue_MoneyCurrency.AddRange(itemoutlist_Money_Currency);
                    //billvalue_MoneyCurrency.AddRange(additionalclausesList_Money_Currency);
                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(billvalue_MoneyCurrency); ;
                    string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysAmount_money_Currency); ;
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysRemain_money_Currency);
                    string bills_itemsin_value = Money_Currency.ConvertMoney_CurrencyList_TOString (Sourceiteminlist_Money_Currency );
                    double bills_itemsin_realvalue = System.Math.Round(Sourceiteminlist_Money_Currency.Sum (x=>x.Value /x.ExchangeRate ),3);
                    double bills_realvalue = System.Math.Round(billvalue_MoneyCurrency.Sum (x=>x.Value /x.ExchangeRate ),3);
                    double bills_pays_realvalue = System.Math.Round(paysAmount_money_Currency.Sum (x=>x.Value /x.ExchangeRate ),3);



                    Report_Sells_Month_ReportDetail Report_Sells_Month_ReportDetail_
                        = new Report_Sells_Month_ReportDetail(dayno, daydate, bills_count, bills_clause_count, bills_value
                        , bills_pays_value, bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsin_value, bills_itemsin_realvalue, bills_realvalue, bills_pays_realvalue);
                    List.Add(Report_Sells_Month_ReportDetail_);
                }
                
            }
            catch(Exception ee)
            {
                throw new Exception ("Get_Report_Sells_Month_ReportDetail:" + ee.Message );
              
            }
            return List;
        }
        internal List<Report_Sells_Year_ReportDetail> Get_Report_Sells_Year_ReportDetail(int year)
        {
            List<Report_Sells_Year_ReportDetail> List = new List<Report_Sells_Year_ReportDetail>();
            try
            {

                List<BillSell> AllBillSell = new BillSellSQL(DB).Get_All_BillSell_List();

                for (int month = 1; month <=12; month++)
                {
                    List<BillSell> BillSellList = AllBillSell.Where(x => x.BillDate.Year == year && x.BillDate.Month == month ).ToList();

                    int monthno = month;
                    int bills_count = BillSellList.Count;


                    List<Money_Currency> itemoutlist_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> Sourceiteminlist_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> additionalclausesList_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> billvalue_MoneyCurrency = new List<Money_Currency>();
                    List<Money_Currency> paysAmount_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysRemain_money_Currency = new List<Money_Currency>();

                    double bills_pays_remain_upon_billcurrency = 0;

                    for (int i = 0; i < BillSellList.Count; i++)
                    {
                        List<ItemOUT> itemoutlist = new List<ItemOUT>();
                        //List<ItemIN> sourceItemIN = new List<ItemIN>();

                        itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(BillSellList[i ]._Operation);
                        //sourceItemIN.AddRange(itemoutlist.Select(x => x._ItemIN).ToList());
                        Sourceiteminlist_Money_Currency.AddRange(Money_Currency.Get_Money_Currency_List_From_ItemIN_By_ItemOUTList(itemoutlist));
                        itemoutlist_Money_Currency.AddRange(Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist));
                        additionalclausesList_Money_Currency.AddRange(new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses_AS_Money_Currency(BillSellList[i]._Currency, BillSellList[i].ExchangeRate, BillSellList[i]._Operation));
                        double billvalue = new OperationSQL(DB).Get_OperationValue(BillSellList[i]._Operation);
                        double pays_remain = billvalue - new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillSellList[i]._Operation);
                        bills_pays_remain_upon_billcurrency += pays_remain;
                        paysRemain_money_Currency.Add(new Money_Currency(BillSellList[i]._Currency, pays_remain, BillSellList[i].ExchangeRate));
                        billvalue_MoneyCurrency.Add(new Money_Currency(BillSellList[i]._Currency
                            , billvalue, BillSellList[i].ExchangeRate));
                        paysAmount_money_Currency.AddRange(new PayINSQL(DB).GetPayINList_As_Money_Currency(BillSellList[i]._Operation));
                    }
                    int bills_clause_count = itemoutlist_Money_Currency.Count + additionalclausesList_Money_Currency.Count;
                    //billvalue_MoneyCurrency.AddRange(itemoutlist_Money_Currency);
                    //billvalue_MoneyCurrency.AddRange(additionalclausesList_Money_Currency);
                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(billvalue_MoneyCurrency); ;
                    string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysAmount_money_Currency); ;
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysRemain_money_Currency);
                    string bills_itemsin_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Sourceiteminlist_Money_Currency);
                    double bills_itemsin_realvalue = System.Math.Round(Sourceiteminlist_Money_Currency.Sum(x => x.Value / x.ExchangeRate),3);
                    double bills_realvalue = System.Math.Round(billvalue_MoneyCurrency.Sum(x => x.Value / x.ExchangeRate),3);
                    double bills_pays_realvalue = System.Math.Round(paysAmount_money_Currency.Sum(x => x.Value / x.ExchangeRate),3);


                    CultureInfo AR_English = new CultureInfo("ar-SY");
                    DateTimeFormatInfo englishInfo = AR_English.DateTimeFormat;
                    string monthName = englishInfo.MonthNames[monthno - 1];

                    Report_Sells_Year_ReportDetail Report_Sells_Month_ReportDetail_
                        = new Report_Sells_Year_ReportDetail(monthno, monthName, bills_count, bills_clause_count, bills_value
                        , bills_pays_value, bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsin_value, bills_itemsin_realvalue, bills_realvalue, bills_pays_realvalue);
                    List.Add(Report_Sells_Month_ReportDetail_);
                }
                return List;
            }
            catch
            {
                throw new Exception ("فشل جلب تقرير  مبيعات السنة التفصيلي");
                return List;
            }
        }
        internal List<Report_Sells_YearRange_ReportDetail> Get_Report_Sells_YearRange_ReportDetail(int year1, int year2)
        {
            List<Report_Sells_YearRange_ReportDetail> List = new List<Report_Sells_YearRange_ReportDetail>();
            try
            {
                int min_year, max_year;
                if(year1 >year2 )
                {
                    min_year = year2;
                    max_year = year1;
                }else
                {
                    min_year = year1;
                    max_year = year2;
                }
                List<BillSell> AllBillSell = new BillSellSQL(DB).Get_All_BillSell_List();

                for (int year = min_year ; year <= max_year ; year++)
                {
                    List<BillSell> BillSellList = AllBillSell.Where(x => x.BillDate.Year == year ).ToList();

                    int yearno = year;
                    int bills_count = BillSellList.Count;


                    List<Money_Currency> itemoutlist_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> Sourceiteminlist_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> additionalclausesList_Money_Currency = new List<Money_Currency>();
                    List<Money_Currency> billvalue_MoneyCurrency = new List<Money_Currency>();
                    List<Money_Currency> paysAmount_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysRemain_money_Currency = new List<Money_Currency>();

                    double bills_pays_remain_upon_billcurrency = 0;

                    for (int i = 0; i < BillSellList.Count; i++)
                    {
                        List<ItemOUT> itemoutlist = new List<ItemOUT>();
                        //List<ItemIN> sourceItemIN = new List<ItemIN>();

                        itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(BillSellList[i]._Operation);
                        //sourceItemIN.AddRange(itemoutlist.Select(x => x._ItemIN).ToList());
                        Sourceiteminlist_Money_Currency.AddRange(Money_Currency.Get_Money_Currency_List_From_ItemIN_By_ItemOUTList(itemoutlist));
                        itemoutlist_Money_Currency.AddRange(Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist));
                        additionalclausesList_Money_Currency.AddRange(new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses_AS_Money_Currency(BillSellList[i]._Currency, BillSellList[i].ExchangeRate, BillSellList[i]._Operation));
                        double billvalue = new OperationSQL(DB).Get_OperationValue(BillSellList[i]._Operation);
                        double pays_remain = billvalue - new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillSellList[i]._Operation);
                        bills_pays_remain_upon_billcurrency += pays_remain;
                        paysRemain_money_Currency.Add(new Money_Currency(BillSellList[i]._Currency, pays_remain, BillSellList[i].ExchangeRate));
                        billvalue_MoneyCurrency.Add(new Money_Currency(BillSellList[i]._Currency
                            , billvalue, BillSellList[i].ExchangeRate));
                        paysAmount_money_Currency.AddRange(new PayINSQL(DB).GetPayINList_As_Money_Currency(BillSellList[i]._Operation));
                    }
                    int bills_clause_count = itemoutlist_Money_Currency.Count + additionalclausesList_Money_Currency.Count;
                    //billvalue_MoneyCurrency.AddRange(itemoutlist_Money_Currency);
                    //billvalue_MoneyCurrency.AddRange(additionalclausesList_Money_Currency);
                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(billvalue_MoneyCurrency); ;
                    string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysAmount_money_Currency); ;
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysRemain_money_Currency);
                    string bills_itemsin_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Sourceiteminlist_Money_Currency);
                    double bills_itemsin_realvalue = System.Math.Round(Sourceiteminlist_Money_Currency.Sum(x => x.Value / x.ExchangeRate),3);
                    double bills_realvalue = System.Math.Round(billvalue_MoneyCurrency.Sum(x => x.Value / x.ExchangeRate),3);
                    double bills_pays_realvalue = System.Math.Round(paysAmount_money_Currency.Sum(x => x.Value / x.ExchangeRate),3);




                    Report_Sells_YearRange_ReportDetail Report_Sells_YearRange_ReportDetail_
                        = new Report_Sells_YearRange_ReportDetail(yearno, bills_count, bills_clause_count, bills_value
                        , bills_pays_value, bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsin_value, bills_itemsin_realvalue, bills_realvalue, bills_pays_realvalue);
                    List.Add(Report_Sells_YearRange_ReportDetail_);
                }
                return List;
            }
            catch
            {
                throw new Exception ("فشل جلب تقرير  مبيعات السنة التفصيلي");
                return List;
            }
        }
        internal List < Report_Sells_Month_ReportDetail> Get_Report_Sells_Day_Report(int year, int month, int day)
        {
            try
            {
                return  Get_Report_Sells_Month_ReportDetail(year, month).Where(x => x.DayID == day).ToList();
       
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_Sells_Day_Report:" + ee.Message);
            }
        }
        internal List < Report_Sells_Year_ReportDetail > Get_Report_Sells_Month_Report(int year, int month)
        {
            try
            {
                return  Get_Report_Sells_Year_ReportDetail(year).Where(x => x.MonthNO == month).ToList();

                
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_Sells_Month_Report:" + ee.Message);
            }
        }
        internal List < Report_Sells_YearRange_ReportDetail> Get_Report_Sells_Year_Report(int year)
        {
            try
            {
                return  Get_Report_Sells_YearRange_ReportDetail(year, year).Where(x => x.YearNO == year).ToList();
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_Sells_Year_Report:" + ee.Message);
            }
        }
        #endregion 
        #region Report_Maintenance
        internal List<Report_MaintenanceOPRs_Day_ReportDetail> Get_Report_MaintenanceOPRs_Day_ReportDetail(int year, int month, int day)
        {
            List<Report_MaintenanceOPRs_Day_ReportDetail> List = new List<Report_MaintenanceOPRs_Day_ReportDetail>();
            try
            {

                List<MaintenanceOPR> AllMaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs();
                List<MaintenanceOPR> DayMaintenanceOPRList = AllMaintenanceOPRList.Where(x => x.EntryDate.Year == year
               && x.EntryDate.Month == month && x.EntryDate.Day == day).ToList();
                for (int i = 0; i < DayMaintenanceOPRList.Count; i++)
                {

                    DateTime maintenanceopr_time = DayMaintenanceOPRList[i].EntryDate ;
                    uint maintenance_opr_id = DayMaintenanceOPRList[i]._Operation.OperationID;
                    string owner = DayMaintenanceOPRList[i]._Contact.ContactName;
                    Item Item_ = DayMaintenanceOPRList[i]._Item;
                    string faultdesc = DayMaintenanceOPRList[i].FaultDesc ;
                    BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(DayMaintenanceOPRList[i]);

                    DateTime? endworkdate, deliverdate, endwarrantry;
                    bool? Repaired;
                    uint? BillID;
                    uint? CurrencyID;
                    string  CurrencyName;
                    string  CurrencySymbol;

                    double? billvalue, exchangerate, paysremain, itemsoutRealvalue, billrealvalue, bill_Pays_realvalue;
                    string PaysAmount, itemsoutvalue;
                    if (DayMaintenanceOPRList[i]._MaintenanceOPR_EndWork != null)
                    {
                        endworkdate = DayMaintenanceOPRList[i]._MaintenanceOPR_EndWork.EndWorkDate;
                        Repaired = DayMaintenanceOPRList[i]._MaintenanceOPR_EndWork.Repaired;
                        deliverdate = DayMaintenanceOPRList[i]._MaintenanceOPR_EndWork.DeliveredDate;
                        endwarrantry = DayMaintenanceOPRList[i]._MaintenanceOPR_EndWork.EndwarrantyDate;
                    }
                    else 
                    {
                        endworkdate = null;
                        Repaired = null;
                        deliverdate = null;
                        endwarrantry = null;
                    }
                    if (BillMaintenance_ !=null )
                    {

                        BillID = BillMaintenance_._Operation.OperationID; ;
                        billvalue = new OperationSQL(DB).Get_OperationValue(BillMaintenance_._Operation );
                        CurrencyID=BillMaintenance_._Currency.CurrencyID ;
                        CurrencyName = BillMaintenance_._Currency.CurrencyName;
                        CurrencySymbol = BillMaintenance_._Currency.CurrencySymbol;
                        exchangerate = BillMaintenance_.ExchangeRate ;

                        List<Money_Currency> paysamount_money_currency = new PayINSQL(DB).GetPayINList_As_Money_Currency(BillMaintenance_._Operation);
                        double pays_upon_operationcurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillMaintenance_._Operation);
                        PaysAmount = Money_Currency.ConvertMoney_CurrencyList_TOString(paysamount_money_currency);
                        paysremain = billvalue- pays_upon_operationcurrency;

                        List<Money_Currency> itemoutlist_Money_Currency = Money_Currency.Get_Money_Currency_List_From_ItemIN_By_ItemOUTList ( new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_).Where(x => x._ItemOUT != null).Select(x => x._ItemOUT).ToList());
                        itemsoutvalue = Money_Currency.ConvertMoney_CurrencyList_TOString(itemoutlist_Money_Currency);
                        itemsoutRealvalue = itemoutlist_Money_Currency.Sum (x=>x.Value /x.ExchangeRate );
                        billrealvalue = billvalue /exchangerate  ;
                        bill_Pays_realvalue = paysamount_money_currency.Sum (x=>x.Value/x.ExchangeRate );

                    }
                    else 
                    {
                        BillID = null;
                        billvalue = null;
                        CurrencyID =null ;
                        CurrencyName = "-";
                        CurrencySymbol ="-";
                        exchangerate = null;
                        PaysAmount = null;
                        paysremain = null;

                        itemsoutvalue = null;
                        itemsoutRealvalue = null;
                        billrealvalue = null;
                        bill_Pays_realvalue = null;
                    }



                    Report_MaintenanceOPRs_Day_ReportDetail Report_MaintenanceOPRs_Day_ReportDetail_
                        = new Report_MaintenanceOPRs_Day_ReportDetail(maintenanceopr_time
                        , maintenance_opr_id, owner, Item_.ItemID, Item_.ItemName, Item_.ItemCompany, Item_.folder.FolderName, faultdesc, endworkdate, Repaired, deliverdate
                        , endwarrantry, BillID, billvalue, CurrencyID, CurrencyName, CurrencySymbol, exchangerate, PaysAmount, paysremain,
                        itemsoutvalue, itemsoutRealvalue, billrealvalue, bill_Pays_realvalue);
                    List.Add(Report_MaintenanceOPRs_Day_ReportDetail_);
                }
                return List;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_MaintenanceOPRs_Day_ReportDetail: "+"," + ee.Message);
            }
        }
        internal List<Report_MaintenanceOPRs_Month_ReportDetail> Get_Report_MaintenanceOPRs_Month_ReportDetail(int year, int month)
        {
            List<Report_MaintenanceOPRs_Month_ReportDetail> List = new List<Report_MaintenanceOPRs_Month_ReportDetail>();
            try
            {

                List<MaintenanceOPR> AllMaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs();
                
                for (int i = 1; i <= DateTime .DaysInMonth(year,month ); i++)
                {
                    List<MaintenanceOPR> DayMaintenanceOPRList = AllMaintenanceOPRList.Where(x => x.EntryDate.Year == year
           && x.EntryDate.Month == month && x.EntryDate.Day == i).ToList();
                    int dayno = i;
                    DateTime daydate = new DateTime(year, month, i) ;
                    int maintenanceopr_count = DayMaintenanceOPRList.Count ;
                    List<MaintenanceOPR> DayMaintenanceOPRList_Finish = DayMaintenanceOPRList.Where(x => x._MaintenanceOPR_EndWork != null).ToList();
                    int maintenanceopr_endwork_count = DayMaintenanceOPRList_Finish.Count ;
                    int maintenanceopr_repaired_count = DayMaintenanceOPRList_Finish.Where (x=>x._MaintenanceOPR_EndWork.Repaired ==true ).ToList ().Count ;
                    int maintenanceopr_warranty_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.EndwarrantyDate != null ).ToList().Count;
                    int maintenanceopr_ENDwarranty_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.EndwarrantyDate != null && x._MaintenanceOPR_EndWork .EndwarrantyDate <DateTime .Now ).ToList().Count;
                 
                    int billscount = 0;
                    double bills_pays_remain_upon_billcurrency =0;
                    List<Money_Currency> billsvalue_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysamount_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysremain_money_Currency = new List<Money_Currency>();
                    List<ItemOUT> itemoutlist = new List<ItemOUT>();
                    for (int j=0;j<DayMaintenanceOPRList.Count;j++)
                    {
                        BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(DayMaintenanceOPRList[j]);
                        if (BillMaintenance_ != null)
                        {
                            billscount += 1;
                            double billvalue = new OperationSQL(DB).Get_OperationValue(BillMaintenance_._Operation);
                            double paysvalue_upon_operationcurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillMaintenance_._Operation);
                            bills_pays_remain_upon_billcurrency += billvalue - paysvalue_upon_operationcurrency;
                            billsvalue_money_Currency.Add(new Money_Currency(BillMaintenance_._Currency, billvalue, BillMaintenance_.ExchangeRate));
                            paysamount_money_Currency .AddRange (new PayINSQL (DB).GetPayINList_As_Money_Currency (BillMaintenance_._Operation ));
                            paysremain_money_Currency.Add(new Money_Currency (BillMaintenance_._Currency,billvalue -paysvalue_upon_operationcurrency ,BillMaintenance_.ExchangeRate ));
                            List<BillMaintenance_Clause> clausesList = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_);
                            itemoutlist .AddRange (clausesList.Where(x => x._ItemOUT != null).Select(x => x._ItemOUT).ToList() );
                        }
                    }

                    string bills_value = Money_Currency .ConvertMoney_CurrencyList_TOString (billsvalue_money_Currency );
                    string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysamount_money_Currency);
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysremain_money_Currency );

                    List<Money_Currency> itemsoutlist_money_currency = Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist);
                    string bills_itemsout_value =Money_Currency.ConvertMoney_CurrencyList_TOString (itemsoutlist_money_currency);
                    double bills_itemsout_realvalue =Math .Round ( itemsoutlist_money_currency.Sum(x => x.Value / x.ExchangeRate),3);

                    double bills_realvalue = Math.Round(billsvalue_money_Currency .Sum (x=>x.Value /x.ExchangeRate ),3);
                    double bills_pays_realvalue = Math.Round(paysamount_money_Currency.Sum(x => x.Value / x.ExchangeRate),3);
                    Report_MaintenanceOPRs_Month_ReportDetail Report_MaintenanceOPRs_Month_ReportDetail_
                        = new Report_MaintenanceOPRs_Month_ReportDetail(dayno, daydate, maintenanceopr_count, maintenanceopr_endwork_count
                        , maintenanceopr_repaired_count, maintenanceopr_warranty_count, maintenanceopr_ENDwarranty_count, billscount, bills_value, bills_pays_value
                        , bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsout_value, bills_itemsout_realvalue
                        , bills_realvalue, bills_pays_realvalue);
                    List.Add(Report_MaintenanceOPRs_Month_ReportDetail_);
                }
                return List;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_MaintenanceOPRs_Month_ReportDetail: " + ee.Message);
                return null;
            }
        }
        internal List<Report_MaintenanceOPRs_Year_ReportDetail> Get_Report_MaintenanceOPRs_Year_ReportDetail(int year)
        {
            List<Report_MaintenanceOPRs_Year_ReportDetail> List = new List<Report_MaintenanceOPRs_Year_ReportDetail>();
            try
            {

                List<MaintenanceOPR> AllMaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs();

                for (int i = 1; i <=12; i++)
                {
                    List<MaintenanceOPR> DayMaintenanceOPRList = AllMaintenanceOPRList.Where(x => x.EntryDate.Year == year
           && x.EntryDate.Month == i ).ToList();
                    int monthno = i;
                    int maintenanceopr_count = DayMaintenanceOPRList.Count;
                    List<MaintenanceOPR> DayMaintenanceOPRList_Finish = DayMaintenanceOPRList.Where(x => x._MaintenanceOPR_EndWork != null).ToList();
                    int maintenanceopr_endwork_count = DayMaintenanceOPRList_Finish.Count;
                    int maintenanceopr_repaired_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.Repaired == true).ToList().Count;
                    int maintenanceopr_warranty_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.EndwarrantyDate != null).ToList().Count;
                    int maintenanceopr_ENDwarranty_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.EndwarrantyDate != null && x._MaintenanceOPR_EndWork.EndwarrantyDate < DateTime.Now).ToList().Count;

                    int billscount = 0;
                    double bills_pays_remain_upon_billcurrency = 0;
                    List<Money_Currency> billsvalue_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysamount_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysremain_money_Currency = new List<Money_Currency>();
                    List<ItemOUT> itemoutlist = new List<ItemOUT>();
                    for (int j = 0; j < DayMaintenanceOPRList.Count; j++)
                    {
                        BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(DayMaintenanceOPRList[j]);
                        if (BillMaintenance_ != null)
                        {
                            billscount += 1;
                            double billvalue = new OperationSQL(DB).Get_OperationValue(BillMaintenance_._Operation);
                            double paysvalue_upon_operationcurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillMaintenance_._Operation);
                            bills_pays_remain_upon_billcurrency += billvalue - paysvalue_upon_operationcurrency;
                            billsvalue_money_Currency.Add(new Money_Currency(BillMaintenance_._Currency, billvalue, BillMaintenance_.ExchangeRate));
                            paysamount_money_Currency.AddRange(new PayINSQL(DB).GetPayINList_As_Money_Currency(BillMaintenance_._Operation));
                            paysremain_money_Currency.Add(new Money_Currency(BillMaintenance_._Currency, billvalue - paysvalue_upon_operationcurrency, BillMaintenance_.ExchangeRate));
                            List<BillMaintenance_Clause> clausesList = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_);
                            itemoutlist.AddRange(clausesList.Where(x => x._ItemOUT != null).Select(x => x._ItemOUT).ToList());
                        }
                    }

                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(billsvalue_money_Currency);
                    string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysamount_money_Currency);
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysremain_money_Currency);

                    List<Money_Currency> itemsoutlist_money_currency = Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist);
                    string bills_itemsout_value = Money_Currency.ConvertMoney_CurrencyList_TOString(itemsoutlist_money_currency);
                    double bills_itemsout_realvalue = Math.Round(itemsoutlist_money_currency.Sum(x => x.Value / x.ExchangeRate),3);

                    double bills_realvalue = Math.Round(billsvalue_money_Currency.Sum(x => x.Value / x.ExchangeRate),3);
                    double bills_pays_realvalue = Math.Round(paysamount_money_Currency.Sum(x => x.Value / x.ExchangeRate),3);

                    CultureInfo AR_English = new CultureInfo("ar-SY");
                    DateTimeFormatInfo englishInfo = AR_English.DateTimeFormat;
                    string monthName = englishInfo.MonthNames[monthno - 1];
                    Report_MaintenanceOPRs_Year_ReportDetail Report_MaintenanceOPRs_Year_ReportDetail_
                        = new Report_MaintenanceOPRs_Year_ReportDetail(monthno, monthName, maintenanceopr_count, maintenanceopr_endwork_count
                        , maintenanceopr_repaired_count, maintenanceopr_warranty_count, maintenanceopr_ENDwarranty_count, billscount, bills_value, bills_pays_value
                        , bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsout_value, bills_itemsout_realvalue
                        , bills_realvalue, bills_pays_realvalue);
                    List.Add(Report_MaintenanceOPRs_Year_ReportDetail_);
                }
                return List;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_MaintenanceOPRs_Year_ReportDetail: " + ee.Message);
                return List;
            }
        }
        internal List<Report_MaintenanceOPRs_YearRange_ReportDetail> Get_Report_MaintenanceOPRs_YearRange_ReportDetail(int year1, int year2)
        {
            List<Report_MaintenanceOPRs_YearRange_ReportDetail> List = new List<Report_MaintenanceOPRs_YearRange_ReportDetail>();
            try
            {
                int min_year, max_year;
                if(year1>year2)
                {
                    min_year = year2;
                    max_year = year1;
                }
                else
                {
                    min_year = year1;
                    max_year = year2;
                }

                List<MaintenanceOPR> AllMaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs();

                for (int i = min_year; i <= max_year; i++)
                {
                    List<MaintenanceOPR> DayMaintenanceOPRList = AllMaintenanceOPRList.Where(x => x.EntryDate.Year ==i).ToList();
                    int yearno = i;
                    int maintenanceopr_count = DayMaintenanceOPRList.Count;
                    List<MaintenanceOPR> DayMaintenanceOPRList_Finish = DayMaintenanceOPRList.Where(x => x._MaintenanceOPR_EndWork != null).ToList();
                    int maintenanceopr_endwork_count = DayMaintenanceOPRList_Finish.Count;
                    int maintenanceopr_repaired_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.Repaired == true).ToList().Count;
                    int maintenanceopr_warranty_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.EndwarrantyDate != null).ToList().Count;
                    int maintenanceopr_ENDwarranty_count = DayMaintenanceOPRList_Finish.Where(x => x._MaintenanceOPR_EndWork.EndwarrantyDate != null && x._MaintenanceOPR_EndWork.EndwarrantyDate < DateTime.Now).ToList().Count;

                    int billscount = 0;
                    double bills_pays_remain_upon_billcurrency = 0;
                    List<Money_Currency> billsvalue_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysamount_money_Currency = new List<Money_Currency>();
                    List<Money_Currency> paysremain_money_Currency = new List<Money_Currency>();
                    List<ItemOUT> itemoutlist = new List<ItemOUT>();
                    for (int j = 0; j < DayMaintenanceOPRList.Count; j++)
                    {
                        BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(DayMaintenanceOPRList[j]);
                        if (BillMaintenance_ != null)
                        {
                            billscount += 1;
                            double billvalue = new OperationSQL(DB).Get_OperationValue(BillMaintenance_._Operation);
                            double paysvalue_upon_operationcurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillMaintenance_._Operation);
                            bills_pays_remain_upon_billcurrency += billvalue - paysvalue_upon_operationcurrency;
                            billsvalue_money_Currency.Add(new Money_Currency(BillMaintenance_._Currency, billvalue, BillMaintenance_.ExchangeRate));
                            paysamount_money_Currency.AddRange(new PayINSQL(DB).GetPayINList_As_Money_Currency(BillMaintenance_._Operation));
                            paysremain_money_Currency.Add(new Money_Currency(BillMaintenance_._Currency, billvalue - paysvalue_upon_operationcurrency, BillMaintenance_.ExchangeRate));
                            List<BillMaintenance_Clause> clausesList = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_);
                            itemoutlist.AddRange(clausesList.Where(x => x._ItemOUT != null).Select(x => x._ItemOUT).ToList());
                        }
                    }

                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(billsvalue_money_Currency);
                    string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysamount_money_Currency);
                    string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysremain_money_Currency);

                    List<Money_Currency> itemsoutlist_money_currency = Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist);
                    string bills_itemsout_value = Money_Currency.ConvertMoney_CurrencyList_TOString(itemsoutlist_money_currency);
                    double bills_itemsout_realvalue = Math.Round(itemsoutlist_money_currency.Sum(x => x.Value / x.ExchangeRate),3);

                    double bills_realvalue = Math.Round(billsvalue_money_Currency.Sum(x => x.Value / x.ExchangeRate),3);
                    double bills_pays_realvalue = Math.Round(paysamount_money_Currency.Sum(x => x.Value / x.ExchangeRate),3);
                    Report_MaintenanceOPRs_YearRange_ReportDetail Report_MaintenanceOPRs_YearRange_ReportDetail_
                        = new Report_MaintenanceOPRs_YearRange_ReportDetail(yearno, maintenanceopr_count, maintenanceopr_endwork_count
                        , maintenanceopr_repaired_count, maintenanceopr_warranty_count, maintenanceopr_ENDwarranty_count, billscount, bills_value, bills_pays_value
                        , bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsout_value, bills_itemsout_realvalue
                        , bills_realvalue, bills_pays_realvalue);
                    List.Add(Report_MaintenanceOPRs_YearRange_ReportDetail_);
                }
                return List;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_MaintenanceOPRs_YearRange_ReportDetail: " + ee.Message);
                return List;
            }
        }
        internal List < Report_MaintenanceOPRs_Month_ReportDetail> Get_Report_MaintenanceOPRs_Day_Report(int year, int month, int day)
        {
            try
            {
                return  Get_Report_MaintenanceOPRs_Month_ReportDetail(year, month).Where(x => x.DayID == day).ToList();
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_MaintenanceOPRs_Day_Report:" + ee.Message);
            }
        }
        internal List<Report_MaintenanceOPRs_Year_ReportDetail>  Get_Report_MaintenanceOPRs_Month_Report(int year, int month)
        {
            try
            {
                 return  Get_Report_MaintenanceOPRs_Year_ReportDetail(year).Where(x => x.MonthNO == month).ToList();
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_MaintenanceOPRs_Month_Report:" + ee.Message);
            }
        }
        internal List<Report_MaintenanceOPRs_YearRange_ReportDetail > Get_Report_MaintenanceOPRs_Year_Report(int year)
        {
            try
            {
                return  Get_Report_MaintenanceOPRs_YearRange_ReportDetail(year, year).Where(x => x.YearNO == year).ToList();
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Report_MaintenanceOPRs_Year_Report:" + ee.Message);
            }
        }
        #endregion
        #region ContactReport
        public List<Contact_Pays_ReportDetail> Contact_Get_Pays_ReportDetail(uint ContactID)
        {
            List<Contact_Pays_ReportDetail> Contact_Pays_ReportDetailList = new List<Contact_Pays_ReportDetail>();
            try
            {
                List<BillBuy> Contact_billbuylist = new BillBuySQL(DB).Get_All_BillBuy_List().Where (x=>x._Contact.ContactID ==ContactID ).ToList ();
                List<BillSell> Contact_billselllist = new BillSellSQL(DB).Get_All_BillSell_List().Where(x => x._Contact.ContactID == ContactID).ToList();
                List<MaintenanceOPR> Contact_MaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs().Where(x => x._Contact.ContactID == ContactID).ToList();
                List<BillMaintenance> Contact_BillMaintenaceList = new List<BillMaintenance>();
                List<PayIN> Contact_payin_list = new List<PayIN>();
                List<PayOUT> Contact_payout_list = new List<PayOUT >();
                for (int i=0;i<Contact_MaintenanceOPRList.Count;i++)
                {
                    BillMaintenance BillMaintenance_= new BillMaintenanceSQL (DB).GetBillMaintenance_By_MaintenaceOPR (Contact_MaintenanceOPRList[i]);
                    if (BillMaintenance_ != null)
                    {
                        Contact_payin_list.AddRange(new PayINSQL(DB).GetPayINList(BillMaintenance_._Operation));
                    }

                }
                for (int i = 0; i < Contact_billselllist.Count; i++)
                {
      
                        Contact_payin_list.AddRange(new PayINSQL(DB).GetPayINList(Contact_billselllist[i]._Operation));


                }
                for (int i = 0; i < Contact_billbuylist.Count; i++)
                {

                    Contact_payout_list.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(Contact_billbuylist[i]._Operation));


                }
                for (int i = 0; i < Contact_payout_list.Count; i++)
                {
                    DateTime PayOprDate = Contact_payout_list[i].PayOprDate;
                    Bill Bill_ = Contact_payout_list[i]._Bill;
                    uint PayOprID = Contact_payout_list[i].PayOprID;
                    string owner;

                    if (Contact_payout_list[i]._Bill == null) owner = "";
                    else if (Contact_payout_list[i]._Bill._Operation.OperationType == Operation.BILL_BUY) owner = " فاتورة شراء رقم:" + Contact_payout_list[i]._Bill._Operation.OperationID;
                    else if (Contact_payout_list[i]._Bill._Operation.OperationType == Operation.Employee_PayOrder) owner = " امر صرف رقم:" + Contact_payout_list[i]._Bill._Operation.OperationID;
                    else owner = "";
                    double value = Contact_payout_list[i].Value;
                    double exchangerate = Contact_payout_list[i].ExchangeRate;
                    double realvalue = value / exchangerate;
                    Currency  currency = Contact_payout_list[i]._Currency;
                    Contact_Pays_ReportDetail Contact_Pays_ReportDetail_
                        = new Contact_Pays_ReportDetail(PayOprID, Contact_Pays_ReportDetail.DIRECTION_OUT
                        , PayOprDate,value , currency.CurrencyID ,currency.CurrencyName,currency.CurrencySymbol, exchangerate, realvalue
                        , Bill_._Operation.OperationID, Bill_._Operation.OperationType);
                    Contact_Pays_ReportDetailList.Add(Contact_Pays_ReportDetail_);
                }
                for (int i = 0; i < Contact_payin_list.Count; i++)
                {
                    DateTime PayOprDate = Contact_payin_list[i].PayOprDate;
                    Bill Bill_ = Contact_payin_list[i]._Bill;
                    uint PayOprID = Contact_payin_list[i].PayOprID;
          
                    double value = Contact_payin_list[i].Value;
                    double exchangerate = Contact_payin_list[i].ExchangeRate;
                    double realvalue = value / exchangerate;
                    Currency  currency = Contact_payin_list[i]._Currency;
                    Contact_Pays_ReportDetail Contact_Pays_ReportDetail_
                        = new Contact_Pays_ReportDetail(PayOprID, Contact_Pays_ReportDetail.DIRECTION_IN
                        , PayOprDate, value, currency.CurrencyID, currency.CurrencyName, currency.CurrencySymbol, exchangerate, realvalue
                        , Bill_._Operation.OperationID, Bill_._Operation.OperationType);
                    Contact_Pays_ReportDetailList.Add(Contact_Pays_ReportDetail_);
                }
                
                return Contact_Pays_ReportDetailList;
            }
            catch(Exception ee)
            {
                throw new Exception ("Contact_Get_Pays_ReportDetail:" + ee.Message );

            }
        }
        public List<Contact_MaintenanceOPRs_ReportDetail> Contact_Get_MaintenanceOPRs_ReportDetail(uint ContactID)
        {

            List<Contact_MaintenanceOPRs_ReportDetail> List = new List<Contact_MaintenanceOPRs_ReportDetail>();
            try
            {

                List<MaintenanceOPR> Contact_MaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs().Where(x => x._Contact.ContactID == ContactID).ToList();
                List<BillMaintenance> Contact_BillMaintenaceList = new List<BillMaintenance>();
                List<PayIN> Contact_payin_list = new List<PayIN>();
                List<PayOUT> Contact_payout_list = new List<PayOUT>();
                for (int i = 0; i < Contact_MaintenanceOPRList.Count; i++)
                {
                    BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(Contact_MaintenanceOPRList[i]);
       
                    DateTime maintenanceopr_date = Contact_MaintenanceOPRList[i].EntryDate;
                    uint maintenance_opr_id = Contact_MaintenanceOPRList[i]._Operation.OperationID ;
                    Item Item_ = Contact_MaintenanceOPRList[i]._Item;
                    string faultdesc = Contact_MaintenanceOPRList[i].FaultDesc ;
  


                    DateTime ?  endworkdate, deliverdate, endwarrantry;
                    bool? Repaired;
                    uint? BillID;
                    uint? CurrencyID;
                    string CurrencyName;
                    string CurrencySymbol;
                    double? billvalue, exchangerate, paysremain, itemsoutRealvalue, billrealvalue, bill_Pays_realvalue;
                    string PaysAmount, itemsoutvalue;


                    if (Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork !=null )
                    {
                        endworkdate = Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork.EndWorkDate;
                        Repaired = Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork.Repaired ;

                            deliverdate = Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork.DeliveredDate;

                            endwarrantry= Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork.EndwarrantyDate;

                    }
                    else 
                    {
                        endworkdate = null;
                        Repaired = null;
                        deliverdate = null;
                        endwarrantry = null;
                    }

                    if (BillMaintenance_ != null)
                    {
                        List<PayIN> payinlist = new PayINSQL(DB).GetPayINList(BillMaintenance_._Operation);
                        List<Money_Currency> payinlist_moneycurrency = Money_Currency.Get_Money_Currency_List_From_PayIN(payinlist);
                        BillID = BillMaintenance_._Operation.OperationID ;
                        billvalue = new OperationSQL(DB).Get_OperationValue(BillMaintenance_._Operation);
                        CurrencyID = BillMaintenance_._Currency.CurrencyID;
                         CurrencyName= BillMaintenance_._Currency.CurrencyName;
                         CurrencySymbol= BillMaintenance_._Currency.CurrencySymbol;

                        exchangerate = BillMaintenance_.ExchangeRate ;
                        PaysAmount =Money_Currency.ConvertMoney_CurrencyList_TOString( payinlist_moneycurrency);
                        paysremain = billvalue- new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillMaintenance_._Operation);
                        List<ItemOUT> itemoutlist
                            = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_).Where(y => y._ItemOUT != null).Select(x => x._ItemOUT).ToList();
                        List<Money_Currency> itemoutlist_money_Currency = Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist);
                        itemsoutvalue = Money_Currency.ConvertMoney_CurrencyList_TOString(itemoutlist_money_Currency);
                        itemsoutRealvalue = itemoutlist_money_Currency.Sum (x=>x.Value/x.ExchangeRate );
                        billrealvalue = billvalue /exchangerate  ;
                        bill_Pays_realvalue = payinlist_moneycurrency.Sum (x=>x.Value /x.ExchangeRate );
                     
                    }
                    else 
                    {
                        BillID = null;
                        billvalue = null;
                        CurrencyID = null ;
                        CurrencyName = "";
                        CurrencySymbol = "";
                        exchangerate = null;
                        PaysAmount = null;
                        paysremain = null;

                        itemsoutvalue = null;
                        itemsoutRealvalue = null;
                        billrealvalue = null;
                        bill_Pays_realvalue = null;
                    }

                    Contact_MaintenanceOPRs_ReportDetail Contact_MaintenanceOPRs_ReportDetail_
                       = new Contact_MaintenanceOPRs_ReportDetail(maintenanceopr_date
                       , maintenance_opr_id, Item_.ItemID, Item_.ItemName, Item_.ItemCompany, Item_.folder.FolderName, faultdesc, endworkdate, Repaired, deliverdate
                       , endwarrantry, BillID, billvalue, CurrencyID, CurrencyName, CurrencySymbol, exchangerate, PaysAmount, paysremain,
                       itemsoutvalue, itemsoutRealvalue, billrealvalue, bill_Pays_realvalue);
                    List.Add(Contact_MaintenanceOPRs_ReportDetail_);

                }

                return List;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Contact_MaintenanceOPRs_ReportDetail: "+ ee.Message);

            }
        }
        public List<Contact_Sells_ReportDetail> Contact_Get_Sells_ReportDetail(uint ContactID)
        {
            List<Contact_Sells_ReportDetail> List = new List<Contact_Sells_ReportDetail>();
            try
            {
                List<BillSell> Contact_billselllist = new BillSellSQL(DB).Get_All_BillSell_List().Where(x => x._Contact.ContactID == ContactID).ToList();

                for (int i = 0; i < Contact_billselllist.Count ; i++)
                {

                    DateTime billdate = Contact_billselllist[i].BillDate ;
                    uint billid = Contact_billselllist[i]._Operation.OperationID ;
                    string selltype = Contact_billselllist[i]._SellType .SellTypeName;
                    List<PayIN> payinlist = new PayINSQL(DB).GetPayINList(Contact_billselllist[i]._Operation);
                    List<Money_Currency> payinlist_moneycurrency = Money_Currency.Get_Money_Currency_List_From_PayIN(payinlist);
                    List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(Contact_billselllist[i]._Operation);
                
                    List<BillAdditionalClause> additionalclausesList = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(Contact_billselllist[i]._Operation);
                    int clause_Count = itemoutlist.Count +additionalclausesList .Count ;
                    double billvalue = itemoutlist.Sum(x => x._OUTValue.Value*x.Amount ) + additionalclausesList.Sum(x => x.Value) ;
                    Currency currency = Contact_billselllist[i]._Currency ;
                    double exchangerate = Contact_billselllist[i].ExchangeRate;
                    int PaysCount = payinlist_moneycurrency.Count ;
                    string PaysAmount = Money_Currency.ConvertMoney_CurrencyList_TOString(payinlist_moneycurrency);
                    double remain = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Contact_billselllist[i]._Operation ) ;
                    List<Money_Currency> itemin_MOney_Currency = Money_Currency.Get_Money_Currency_List_From_ItemIN_By_ItemOUTList(itemoutlist);
                    string bill_source_itemsin_cost = Money_Currency.ConvertMoney_CurrencyList_TOString(itemin_MOney_Currency);
                    double bill_source_itemsin_realcost = itemin_MOney_Currency.Sum(x => x.Value / x.ExchangeRate);
                    double bill_value_realvalue = billvalue/ Contact_billselllist[i].ExchangeRate  ;
                    double real_pays = payinlist.Sum(x => x.Value / x.ExchangeRate);


                    Contact_Sells_ReportDetail Contact_Sells_ReportDetail_
                        = new Contact_Sells_ReportDetail(billdate, billid , selltype,
                        clause_Count, billvalue , currency.CurrencyID, currency.CurrencyName, currency.CurrencySymbol, exchangerate, PaysCount, PaysAmount, remain
                        , bill_source_itemsin_cost, bill_source_itemsin_realcost, bill_value_realvalue, real_pays);
                    List.Add(Contact_Sells_ReportDetail_);
                }
                return List;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Contact_Sells_ReportDetail:" + ee.Message);

            }
        }
        public List<Contact_Buys_ReportDetail> Contact_Get_Buys_ReportDetail(uint ContactID)
        {
            List<Contact_Buys_ReportDetail> List = new List<Contact_Buys_ReportDetail>();
            try
            {
                Contact contact_ = new ContactSQL(DB).GetContactInforBYID(ContactID);
                List<BillBuy> Contact_billbuylist = new BillBuySQL(DB).Get_Contact_BillBuy_List(contact_);

                for (int i = 0; i < Contact_billbuylist.Count; i++)
                {
                    DateTime billdate = Contact_billbuylist[i].BillDate;
                    uint billid = Contact_billbuylist[i]._Operation.OperationID;
                    List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportList = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(Contact_billbuylist[i]._Operation);

                    double amountin = ItemIN_ItemOUTReportList.Sum(x => x._ItemIN.Amount);
                    double consume_amount = 0;
                    List<ItemOUT> itemoutlist = new List<ItemOUT>();
                    for (int j = 0; j < ItemIN_ItemOUTReportList.Count; j++)
                    {
                        itemoutlist.AddRange(ItemIN_ItemOUTReportList[j].ItemOUTList);
                        double itemin_consumeunit_factor = ItemIN_ItemOUTReportList[j]._ItemIN._ConsumeUnit.Factor;
                        consume_amount
                            += ItemIN_ItemOUTReportList[j].ItemOUTList.Sum(y => (y.Amount * (y._ConsumeUnit.Factor / itemin_consumeunit_factor)));
                    }

                    double amountremain = amountin - consume_amount; ;

                    List<BillAdditionalClause> BillAdditionalClauseList = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(Contact_billbuylist[i]._Operation);
                    int clause_Count = ItemIN_ItemOUTReportList.Count + BillAdditionalClauseList.Count;
                    double billvalue =new OperationSQL (DB).Get_OperationValue (Contact_billbuylist[i]._Operation );
                    Currency currency = Contact_billbuylist[i]._Currency;
                    double exchangerate = Contact_billbuylist[i].ExchangeRate;
                    List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(Contact_billbuylist[i]._Operation);
                    string PaysAmount =Money_Currency.ConvertMoney_CurrencyList_TOString(
                        Money_Currency.Get_Money_Currency_List_From_PayOUT(payoutlist));
                    double paysremain = billvalue - new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Contact_billbuylist[i]._Operation);
                    double billrealvalue = Math.Round(billvalue / Contact_billbuylist[i].ExchangeRate, 2);
                    double bill_Pays_realvalue = payoutlist.Sum(b => b.Value / b.ExchangeRate);

                    string itemsoutvalue =
                        Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_ItemOUT(itemoutlist));

                    double itemsoutRealvalue = itemoutlist.Sum(n => n._OUTValue.Value / n._OUTValue.ExchangeRate);
                    List<PayIN> billbuy_returns_PayIN = new BillBuySQL(DB).Get_Billbuy__Returns_Pays(Contact_billbuylist[i]._Operation.OperationID);
                    List<Money_Currency> Money_CurrencyList = Money_Currency.Get_Money_Currency_List_From_PayIN(billbuy_returns_PayIN);
                    string bill_pays_returns_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_CurrencyList);
                    double bill_pays_returns_realvalue = Money_CurrencyList.Sum(x => x.Value / x.ExchangeRate);

                    Contact_Buys_ReportDetail Contact_Buys_ReportDetail_
                        = new Contact_Buys_ReportDetail(billdate, billid,
                        clause_Count, amountin , amountremain, billvalue, currency.CurrencyID ,currency .CurrencyName,currency.CurrencySymbol , exchangerate
                        , PaysAmount, paysremain, billrealvalue, bill_Pays_realvalue, itemsoutvalue, itemsoutRealvalue
                        , bill_pays_returns_value, bill_pays_returns_realvalue);
                    List.Add(Contact_Buys_ReportDetail_);
                }
                return List;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Contact_Buys_ReportDetail:" + ee.Message);
            }
        }
        internal Contact_Sells_Report Contact_Get_Report_Sells(uint ContactID)
        {
            try
            {
                List<BillSell > Contact_BillSell_List = new BillSellSQL (DB).Get_All_BillSell_List ().Where (x=>x._Contact.ContactID ==ContactID).ToList ();
                int bills_count = Contact_BillSell_List.Count;
                List<Money_Currency> bills_value_Money_Currency = new List<Money_Currency>();
                List<Money_Currency> paysin_Money_Currency = new List<Money_Currency>();
                List<Money_Currency> paysremain_Money_Currency = new List<Money_Currency>();
                List<Money_Currency> source_itemsin_list_Money_Currency = new List<Money_Currency>();
                double bills_pays_remain_upon_billcurrency = 0;
                for (int i=0;i< Contact_BillSell_List.Count;i++)
                {

                    double billvalue = new OperationSQL(DB).Get_OperationValue(Contact_BillSell_List[i]._Operation);
                    bills_value_Money_Currency.Add(new Money_Currency(Contact_BillSell_List[i]._Currency ,
                       billvalue, Contact_BillSell_List[i].ExchangeRate));
                    
                    paysin_Money_Currency.AddRange(
                       Money_Currency.Get_Money_Currency_List_From_PayIN(
                           new PayINSQL (DB).GetPayINList (Contact_BillSell_List[i]._Operation)));
                    double bill_pays_remain_upon_billcurrency = billvalue- new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Contact_BillSell_List[i]._Operation);
                    bills_pays_remain_upon_billcurrency += bill_pays_remain_upon_billcurrency;
                    paysremain_Money_Currency.Add (new Money_Currency(Contact_BillSell_List[i]._Currency, bill_pays_remain_upon_billcurrency, Contact_BillSell_List[i].ExchangeRate));
                    List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(Contact_BillSell_List[i]._Operation );
                    source_itemsin_list_Money_Currency.AddRange(Money_Currency .Get_Money_Currency_List_From_ItemIN_By_ItemOUTList( itemoutlist));


                   

                }
                string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(bills_value_Money_Currency) ;
                string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysin_Money_Currency );
                string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysremain_Money_Currency );

                string bills_itemsin_value = Money_Currency.ConvertMoney_CurrencyList_TOString(source_itemsin_list_Money_Currency );
                double bills_itemsin_realvalue = source_itemsin_list_Money_Currency.Sum(x => x.Value / x.ExchangeRate);
                double bills_realvalue = bills_value_Money_Currency .Sum(x => x.Value / x.ExchangeRate);
                double bills_pays_realvalue = paysin_Money_Currency.Sum(x => x.Value / x.ExchangeRate);



                Contact_Sells_Report Contact_Sells_Report_
                    = new Contact_Sells_Report(bills_count, bills_value
                    , bills_pays_value, bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsin_value, bills_itemsin_realvalue, bills_realvalue, bills_pays_realvalue);
                return Contact_Sells_Report_;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Contact_Sells_Report:" + ee.Message);

            }
        }
        internal Contact_Buys_Report Contact_Get_Report_Buys(uint ContactID)
        {
            try
            {

                List<BillBuy > Contact_BillBuy_List = new BillBuySQL(DB).Get_All_BillBuy_List().Where(x => x._Contact.ContactID == ContactID).ToList();
                //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Contact_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                int bills_count = Contact_BillBuy_List.Count;
                List<PayOUT> BillBuy_paysoutlist = new List<PayOUT>();
                List<PayIN> billbuy_returns_PayIN = new List<PayIN>();
         

                List<Money_Currency> Money_Currency_Billsvalues = new List<Money_Currency>();
                List<Money_Currency> Money_Currency_Paysvalues = new List<Money_Currency>();
                List<Money_Currency> Money_Currency_Paysremain = new List<Money_Currency>();
                List<ItemIN> ItemINList = new List<ItemIN>();
                List<ItemOUT> ItemOUTList = new List<ItemOUT>();
                double amountin = 0;
                double consume_amount = 0;
                double bills_pays_remain_upon_billcurrency = 0;
                for (int j = 0; j < Contact_BillBuy_List.Count; j++)
                {
                    List<ItemIN_ItemOUTReport> ItemIN_ItemOUTReportlist = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(Contact_BillBuy_List[j]._Operation);
                    BillBuy_paysoutlist.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(Contact_BillBuy_List[j]._Operation));
                    OperationSQL OperationSQL_ = new OperationSQL(DB);
                    double billvalue = OperationSQL_.Get_OperationValue(Contact_BillBuy_List[j]._Operation);
                    double paysvalue_upon_operationcurrency = OperationSQL_.Get_OperationPaysValue_UPON_OperationCurrency(Contact_BillBuy_List[j]._Operation);
                    bills_pays_remain_upon_billcurrency += billvalue - paysvalue_upon_operationcurrency;
                    Money_Currency_Billsvalues.Add(new Money_Currency(Contact_BillBuy_List[j]._Currency, billvalue
                        , Contact_BillBuy_List[j].ExchangeRate));
                    Money_Currency_Paysremain.Add(new Money_Currency(Contact_BillBuy_List[j]._Currency, billvalue - paysvalue_upon_operationcurrency
                        , Contact_BillBuy_List[j].ExchangeRate));
                    billbuy_returns_PayIN.AddRange(new BillBuySQL(DB).Get_Billbuy__Returns_Pays(Contact_BillBuy_List[j]._Operation.OperationID));
                    ItemINList.AddRange(ItemIN_ItemOUTReportlist.Select(x => x._ItemIN));
                    for (int k = 0; k < ItemIN_ItemOUTReportlist.Count; k++)
                    {
                        amountin += ItemIN_ItemOUTReportlist[k]._ItemIN.Amount;
                        double itemin_consumeunit_factor = ItemIN_ItemOUTReportlist[k]._ItemIN._ConsumeUnit.Factor;
                        consume_amount
                        += ItemIN_ItemOUTReportlist[k].ItemOUTList.Sum(y => (y.Amount * (y._ConsumeUnit.Factor / itemin_consumeunit_factor)));
                        ItemOUTList.AddRange(ItemIN_ItemOUTReportlist[k].ItemOUTList);

                    }
                }



                double amountremain = amountin - consume_amount; ;
                string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Billsvalues);
                string bills_pays_value
                    = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(BillBuy_paysoutlist));
                string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Paysremain);
                double bills_realvalue = Money_Currency_Billsvalues.Sum(x => x.Value / x.ExchangeRate);
                double bills_pays_realvalue = Money_Currency_Paysvalues.Sum(x => x.Value / x.ExchangeRate);
                List<Money_Currency> Money_Currency_ItemsOut = Money_Currency.Get_Money_Currency_List_From_ItemOUT(ItemOUTList);
                string bills_itemsout_value =
                    Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_ItemsOut);
                double bills_itemsout_realvalue = Money_Currency_ItemsOut.Sum(x => x.Value / x.ExchangeRate);
                List<Money_Currency> Money_Currency_Return_Pays = Money_Currency.Get_Money_Currency_List_From_PayIN(billbuy_returns_PayIN);
                string bills_pays_returns_value
                    = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency_Return_Pays);
                double bills_pays_returns_realvalue = Money_Currency_Return_Pays.Sum(x => x.Value / x.ExchangeRate);



                Contact_Buys_Report Contact_Buys_Report_
                        = new Contact_Buys_Report(bills_count, amountin, amountremain, bills_value
                        , bills_pays_value, bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_realvalue, bills_pays_realvalue, bills_itemsout_value
                        , bills_itemsout_realvalue, bills_pays_returns_value, bills_pays_returns_realvalue);
                    return Contact_Buys_Report_;
           
               
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_Contact_Buys_Report:" + ee.Message);
                return null;
            }
        }
        internal Contact_MaintenanceOPRs_Report Contact_Get_Report_Maintenance(uint ContactID)
        {
            try
            {

                List<MaintenanceOPR> Contact_MaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs().Where(x => x._Contact.ContactID == ContactID).ToList();
                List<Money_Currency> Bills_value_moneycurrency = new List<Money_Currency>();
                List<Money_Currency> paysin_moneycurrency = new List<Money_Currency>();
                List<Money_Currency> paysremain_moneycurrency = new List<Money_Currency>();
                List<Money_Currency> itemoutlist_Money_Currency=new List<Money_Currency> ();
                int maintenanceopr_count = Contact_MaintenanceOPRList.Count ;
                int maintenanceopr_endwork_count =0;
                int maintenanceopr_repaired_count = 0;
                int maintenanceopr_warranty_count =0;
                int maintenanceopr_ENDwarranty_count = 0;
                double bills_pays_remain_upon_billcurrency = 0;
                int billscount = 0;
                for (int i = 0; i < Contact_MaintenanceOPRList.Count; i++)
                {


                    if (Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork != null)
                    {
                        maintenanceopr_endwork_count++;
                        if (Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork.Repaired == true)
                            maintenanceopr_repaired_count++;
                        if (Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork.EndwarrantyDate != null)
                        {
                            maintenanceopr_warranty_count++;
                            if (Contact_MaintenanceOPRList[i]._MaintenanceOPR_EndWork.EndwarrantyDate > DateTime.Now)
                                maintenanceopr_ENDwarranty_count++;
                        }
                    }
                    BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(Contact_MaintenanceOPRList[i]);

                    if (BillMaintenance_ != null)
                    {
                        billscount++;
                        double billvalue = new OperationSQL(DB).Get_OperationValue(BillMaintenance_._Operation);
                        Bills_value_moneycurrency.Add(new Money_Currency(BillMaintenance_._Currency, billvalue, BillMaintenance_.ExchangeRate));
                        paysin_moneycurrency.AddRange(Money_Currency.Get_Money_Currency_List_From_PayIN(new PayINSQL(DB).GetPayINList(BillMaintenance_._Operation)));
                        double pays_upon_operationcurrency = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(BillMaintenance_._Operation);
                        double pays_remain = billvalue - pays_upon_operationcurrency;
                        bills_pays_remain_upon_billcurrency += pays_remain;
                        paysremain_moneycurrency.Add(new Money_Currency(BillMaintenance_._Currency, pays_remain, BillMaintenance_.ExchangeRate));

                        itemoutlist_Money_Currency.AddRange(  Money_Currency.Get_Money_Currency_List_From_ItemIN_By_ItemOUTList(new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(BillMaintenance_).Where(x => x._ItemOUT != null).Select(x => x._ItemOUT).ToList()));


                    }
                }
                    
                   
                    string bills_value = Money_Currency.ConvertMoney_CurrencyList_TOString(Bills_value_moneycurrency);
                    string bills_pays_value = Money_Currency.ConvertMoney_CurrencyList_TOString(paysin_moneycurrency );
                string bills_pays_remain = Money_Currency.ConvertMoney_CurrencyList_TOString(paysremain_moneycurrency );
                string bills_itemsout_value = Money_Currency.ConvertMoney_CurrencyList_TOString(itemoutlist_Money_Currency );
                double bills_itemsout_realvalue = itemoutlist_Money_Currency.Sum (x=>x.Value /x.ExchangeRate );

                    double bills_realvalue = Bills_value_moneycurrency.Sum(x => x.Value / x.ExchangeRate);
                double bills_pays_realvalue = paysin_moneycurrency .Sum(x => x.Value / x.ExchangeRate);
               
            


                return  new Contact_MaintenanceOPRs_Report(maintenanceopr_count, maintenanceopr_endwork_count
                        , maintenanceopr_repaired_count, maintenanceopr_warranty_count, maintenanceopr_ENDwarranty_count, billscount, bills_value, bills_pays_value
                        , bills_pays_remain, bills_pays_remain_upon_billcurrency, bills_itemsout_value, bills_itemsout_realvalue
                        , bills_realvalue, bills_pays_realvalue);
            }
            catch (Exception ee)
            {
                throw new Exception ("Contact_Get_Report_Maintenance: " + ee.Message);
                return null ;
            }

        }
        public List<Contact_PayCurrencyReport> Contact_Get_Report_Pays(uint ContactID)
        {

            List<Contact_PayCurrencyReport> Contact_PayCurrencyReportList = new List<Contact_PayCurrencyReport>();
            try
            {
                List<Contact_Pays_ReportDetail> Contact_Pays_ReportDetail_List = Contact_Get_Pays_ReportDetail(ContactID);
                List<uint > currencyIDList = Contact_Pays_ReportDetail_List.Select(x => x.CurrencyID).Distinct().ToList();
                for (int i = 0; i < currencyIDList.Count ; i++)
                {
                    uint currencyid = currencyIDList[i];
                    Currency Currency_ = new CurrencySQL (DB).GetCurrencyINFO_ByID (currencyid);
                    double payin_sell = Contact_Pays_ReportDetail_List.Where (y=>y.OperationType 
                    ==Operation.BILL_SELL).Sum (x=>x.Value );
                    double payin_mainenance = Contact_Pays_ReportDetail_List.Where(y => y.OperationType
                   == Operation.BILL_MAINTENANCE ).Sum(x => x.Value);

                    double payout_buy = Contact_Pays_ReportDetail_List.Where(y => y.OperationType
                   == Operation.BILL_BUY).Sum(x => x.Value);

                    Contact_PayCurrencyReport Contact_PayCurrencyReport =
                        new Contact_PayCurrencyReport(Currency_.CurrencyID, Currency_.CurrencyName, Currency_.CurrencySymbol, payin_sell, payin_mainenance
                        , payout_buy);
                    Contact_PayCurrencyReportList.Add(Contact_PayCurrencyReport);
                }
                return Contact_PayCurrencyReportList;
            }
            catch (Exception ee)
            {
                throw new Exception ("Contact_Get_Report_Pays:" + ee.Message);

            }
        }
        public List<Contact_BillCurrencyReport> Contact_GetBillsReportList(uint ContactID)
        {
            List<Contact_BillCurrencyReport> BillCurrencyReportList = new List<Contact_BillCurrencyReport>();

            try
            {
                List<BillSell> Contact_BillSell_List = new BillSellSQL(DB).Get_All_BillSell_List().Where(x => x._Contact.ContactID == ContactID).ToList();

                List<BillBuy> Contact_BillBuy_List = new BillBuySQL(DB).Get_All_BillBuy_List().Where(x => x._Contact.ContactID == ContactID).ToList();

                List<MaintenanceOPR> Contact_MaintenanceOPRList = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs().Where(x => x._Contact.ContactID == ContactID).ToList();
                List<BillMaintenance> Contact_BillMaintenance_List = new List<BillMaintenance>();
                for (int i = 0; i < Contact_MaintenanceOPRList.Count; i++)
                {
                    BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(Contact_MaintenanceOPRList[i]);
                    if (BillMaintenance_ != null)
                        Contact_BillMaintenance_List.Add(BillMaintenance_);
                }


                List<uint > currencyIDlist = new List<uint >();
                currencyIDlist.AddRange(Contact_BillSell_List.Select(x => x._Currency.CurrencyID).ToList());
                currencyIDlist.AddRange(Contact_BillBuy_List.Select(x => x._Currency.CurrencyID).ToList());
                currencyIDlist.AddRange(Contact_BillMaintenance_List.Select(x => x._Currency.CurrencyID).ToList());
                currencyIDlist = currencyIDlist.Distinct().ToList();
                for (int i = 0; i < currencyIDlist.Count;i++)
                {
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(currencyIDlist[i]);
                    List<BillSell> Contact_BillSell_List_currency = Contact_BillSell_List.Where(x => x._Currency.CurrencyID == currencyIDlist[i]).ToList();
                    List<BillBuy> Contact_BillBuy_List_currency = Contact_BillBuy_List.Where(x => x._Currency.CurrencyID == currencyIDlist[i]).ToList();
                    List<BillMaintenance> Contact_BillMaintenance_List_currency = Contact_BillMaintenance_List.Where(x => x._Currency.CurrencyID == currencyIDlist[i]).ToList();
                    int billb_Count = Contact_BillBuy_List_currency.Count;
                    double billb_Value = 0;
                    double billb_Pays_Value = 0;

                    for (int j = 0; j < Contact_BillBuy_List_currency.Count; j++)
                    {
                        billb_Value += new OperationSQL(DB).Get_OperationValue(Contact_BillBuy_List_currency[j]._Operation);
                        billb_Pays_Value += new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Contact_BillBuy_List_currency[j]._Operation);
                    }

                    double bills_Value = 0;
                    double bills_Pays_Value = 0;
                    int bills_Count = Contact_BillSell_List_currency.Count;
                    for (int j = 0; j < Contact_BillSell_List_currency.Count; j++)
                    {
                        bills_Value += new OperationSQL(DB).Get_OperationValue(Contact_BillSell_List_currency[j]._Operation);
                        bills_Pays_Value += new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Contact_BillSell_List_currency[j]._Operation);
                    }
                    int billm_Count = Contact_BillMaintenance_List_currency.Count;
                    double billm_Value = 0;
                    double billm_Pays_Value = 0;
                    for (int j = 0; j < Contact_BillMaintenance_List_currency.Count; j++)
                    {
                        billm_Value += new OperationSQL(DB).Get_OperationValue(Contact_BillMaintenance_List_currency[j]._Operation);
                        billm_Pays_Value += new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Contact_BillMaintenance_List_currency[j]._Operation);
                    }

                    Contact_BillCurrencyReport billreportDetail =
                        new Contact_BillCurrencyReport(currency.CurrencyID, currency.CurrencyName, currency.CurrencySymbol, bills_Count, bills_Pays_Value, bills_Pays_Value
                        , billm_Count, billm_Value, billm_Pays_Value
                        , billb_Count, billb_Value, billb_Pays_Value);
                    BillCurrencyReportList.Add(billreportDetail);
                }
                return BillCurrencyReportList;
            }
            catch (Exception ee)
            {
                throw new Exception(" Contact_GetBillsReportList:" + ee.Message);

            }
        }


        #endregion
        #region EmployeeReport
        public List<EmployeeMent_Employee_Report> Company_Get_EmployeeMent_Employee_Report_List()
        {
            List<EmployeeMent_Employee_Report> list = new List<EmployeeMent_Employee_Report>();
            try
            {
                List<EmployeeMent> EmployeeMentlist = new EmployeeMentSQL(DB).Get_EmployeeMent_List();


                for (int i = 0; i < EmployeeMentlist.Count; i++)
                {
                    uint levelid = EmployeeMentlist[i].Level.LevelID;
                    string levelname = EmployeeMentlist[i].Level.LevelName;
                    uint employeement_id = EmployeeMentlist[i].EmployeeMentID;
                    string employeement_name = EmployeeMentlist[i].EmployeeMentName;
                    string part_name = EmployeeMentlist[i]._Part.PartName;
                    List<Document> documentlist = new DocumentSQL(DB).Get_DocumentReport_List();
                    List<Document> Assign_documentlist = documentlist
                        .Where(x => x._EmployeeMent != null && x._EmployeeMent.EmployeeMentID == EmployeeMentlist[i].EmployeeMentID
                       && x.DocumentType == Document.ASSIGN_DOCUMENT
                       && !documentlist.Where(y => y.DocumentType == Document.ENDASSIGN_DOCUMENT).Select(y => y.TargetDocument.DocumentID).Contains(x.DocumentID)).ToList();



                    //List<Document> EndAssign_documentlist = documentlist
                    //    .Where(x => x._EmployeeMent != null && x._EmployeeMent.EmployeeMentID == EmployeeMentlist[i].EmployeeMentID
                    //       && x.DocumentType == Document.ENDASSIGN_DOCUMENT ).ToList();
                    Document AffictiveDocument = null;
                    if (Assign_documentlist.Count > 0) AffictiveDocument = Assign_documentlist[0];
                    else AffictiveDocument = null;
                    //for (int j=0;j<Assign_documentlist.Count;j++)
                    //{
                    //   List < Document> tmpdocument=EndAssign_documentlist 
                    //        .Where (x=>x.TargetDocument.DocumentID  ==Assign_documentlist[i].DocumentID ).ToList ();
                    //    if(tmpdocument.Count >0)
                    //    {
                    //        AffictiveDocument = tmpdocument[0];
                    //        break;
                    //    }
                    //}
                    uint? employee_id;
                    uint? jobstartid;
                    string employee_name = "";
                    DateTime? jobstart_date;
                    uint? assign_id;
                    DateTime? assign_date;
                    if (AffictiveDocument != null)
                    {
                        employee_id = AffictiveDocument._Employee.EmployeeID;
                        employee_name = AffictiveDocument._Employee.EmployeeName;
                        jobstartid = AffictiveDocument.TargetDocument.DocumentID;
                        jobstart_date = AffictiveDocument.TargetDocument.ExecuteDate;
                        assign_id = AffictiveDocument.DocumentID;
                        assign_date = AffictiveDocument.DocumentDate;
                    }
                    else
                    {
                        employee_id = null;
                        employee_name = null;
                        jobstartid = null;
                        jobstart_date = null;
                        assign_id = null;
                        assign_date = null;
                    }


                    list.Add(new EmployeeMent_Employee_Report(levelid, levelname, employeement_id, employeement_name, part_name
                        , employee_id, employee_name, jobstartid, jobstart_date, assign_id, assign_date));
                }
                return list;
            }
            catch (Exception ee)
            {
                throw new Exception ("Get_EmployeeMent_Employee_Report_List" + ee.Message);
            }
        }
        public List<EmployeesReport> Company_GetEmployeesReportList()
        {
            List<EmployeesReport> list = new List<EmployeesReport>();
            try
            {


                List<Employee> employeesList = new EmployeeSQL(DB).Get_All_Employees();
                for (int i = 0; i < employeesList.Count; i++)
                {


                    string jobstate = "";
                    string employeementstate = "";
                    uint employeestatecode;
                    List<Document> documentlist = new DocumentSQL(DB).Get_Employee_Document_List(employeesList[i]);
                    List<Document> JobStart_documentlist = documentlist.Where(x => x.DocumentType == Document.JOBSTART_DOCUMENT).ToList();
                    if (JobStart_documentlist.Count == 0)
                    {
                        jobstate = " الموظف غير مباشر حتى الآن";
                        employeementstate = "لم يشغل اي وظيفة";
                        employeestatecode = EmployeesReport.EMPLOYEE_NOT_START_WORK;
                    }
                    else
                    {
                        List<Document> Assign_documentlist = documentlist.Where(x => x.DocumentType == Document.ASSIGN_DOCUMENT).ToList();
                        List<Document> EndAssign_documentlist = documentlist.Where(x => x.DocumentType == Document.ENDASSIGN_DOCUMENT).ToList();

                        List<Document> EndJobStart_documentlist = documentlist.Where(x => x.DocumentType == Document.ENDJOBSTART_DOCUMENT).ToList();
                        if (JobStart_documentlist.Count == EndJobStart_documentlist.Count)
                        {
                            jobstate = "الموظف غادر العمل";
                            employeestatecode = EmployeesReport.EMPLOYEE_LEFT_WORK;

                            if (Assign_documentlist.Count == 0)
                                employeementstate = "لم يتم تكليفه باي وظيفة";
                            else if (Assign_documentlist.Count == 1)
                                employeementstate = "شغل وظيفة :" + Assign_documentlist[0]._EmployeeMent.EmployeeMentName;
                            else
                                employeementstate = "شغل " + Assign_documentlist.Count + " وظائف ";


                        }
                        else
                        {
                            jobstate = "الموظف مباشر";
                            if (Assign_documentlist.Count == 0)
                            {
                                employeestatecode = EmployeesReport.EMPLOYEE_ON_WORK_NO_EMPLOYEEMENT;
                                employeementstate = "لم يتم تكليفه باي وظيفة";
                            }
                            else if (Assign_documentlist.Count == EndAssign_documentlist.Count)
                            {
                                employeestatecode = 3;
                                if (Assign_documentlist.Count == 1)
                                    employeementstate = " غير مكلف باي وظيفة حاليا , تكلف بسابقا بـ" + Assign_documentlist[0]._EmployeeMent.EmployeeMentName;
                                else
                                    employeementstate = " غير مكلف باي وظيفة حاليا , تكلف بسابقا بـ"
                                        + Assign_documentlist.Count + " وظائف ";

                            }
                            else
                            {
                                employeestatecode = EmployeesReport.EMPLOYEE_ON_WORK_ON_EMPLOYEEMENT;
                                List<Document> AffictiveDocument = new List<Document>(); ;
                                for (int j = 0; j < Assign_documentlist.Count; j++)
                                {
                                    List<Document> tmpdocument = EndAssign_documentlist
                                         .Where(x => x.TargetDocument.DocumentID == Assign_documentlist[j].DocumentID).ToList();
                                    if (tmpdocument.Count == 0)
                                    {
                                        AffictiveDocument.Add(Assign_documentlist[j]);

                                    }
                                }
                                if (AffictiveDocument.Count == 1)
                                    employeementstate = " يشغل حاليا وظيفة"
                                        + AffictiveDocument[0]._EmployeeMent.EmployeeMentName;
                                else
                                    employeementstate = " يشغل حاليا"
                                        + AffictiveDocument.Count + " وظائف ";
                            }
                        }
                    }
                    list.Add(new EmployeesReport(employeesList[i].EmployeeID, employeesList[i].EmployeeName, employeesList[i].Gender, employeesList[i].BirthDate
                        , employeesList[i].NationalID, employeesList[i]._MaritalStatus.MaritalStatusName, employeesList[i].Mobile, employeesList[i].Phone, employeesList[i].EmailAddress
                        , employeesList[i].Address, employeesList[i].Report, jobstate, employeementstate, employeestatecode));
                }
                return list;
            }
            catch (Exception ee)
            {
                throw new Exception ("Company_GetEmployeesReportList:" + ee.Message);

            }
        }
        #endregion
    }



}
