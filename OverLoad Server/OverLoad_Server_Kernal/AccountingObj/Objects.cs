
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OverLoad_Server_Kernal
{
    namespace Objects
    {
        public class DateAccount
        {
            public class YearRange
            {
                public int min_year;
                public int max_year;
                public YearRange(int y1, int y2)
                {
                    if (y1 > y2)
                    {
                        min_year = y2;
                        max_year = y1;
                    }
                    else
                    {
                        min_year = y1;
                        max_year = y2;
                    }
                }

            }
            public YearRange YearRange_;
            public int Year;
            public int Month;
            public int Day;
            private DatabaseInterface DB;
            public DateAccount(DatabaseInterface db, YearRange YearRange__, int year, int month, int day)
            {
                DB = db;
                YearRange_ = YearRange__;
                Year = year;
                Month = month;
                Day = day;
            }


            public string GetAccountDateString()
            {
                string returnstring = "";
                if (Day != -1)
                    returnstring = "[" + Day.ToString() + "] \\ [" + Month.ToString() + "] \\ [" + Year.ToString() + " ]";

                else if (Month != -1)
                {
                    returnstring = "[" + Month.ToString() + " ] [ " + Year.ToString() + " ]";
                }
                else if (Year != -1)
                {
                    returnstring = Year.ToString();
                }
                else
                {
                    returnstring = YearRange_.max_year.ToString() + "-" + YearRange_.min_year.ToString();
                }
                return returnstring;
            }

            public void Account_Date_UP()
            {
                if (this.Day != -1) this.Day = -1;
                else if (this.Month != -1) this.Month = -1;
                else if (this.Year != -1) this.Year = -1;
                else return;
            }
            public void Account_Date_Down(int value)
            {
                if (this.Year == -1)
                {
                    if (Year < 1990 && Year > 2200) return;
                    Year = value;
                }
                else if (Month == -1)
                {
                    if (Month < 1 && Month > 12) return;
                    Month = value;
                }
                else if (Day == -1)
                {

                    if (Day < 1 && Day > DateTime.DaysInMonth(Convert.ToInt32(Year), Convert.ToInt32(Month))) return;
                    Day = value;

                }
                else return;
            }


            internal DateTime GetDate()
            {
                if (this.Year != -1 && this.Month != -1 && this.Day != -1)
                    return new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(Day)
                        , DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                else return DateTime.Now;
            }

        }
        public class Currency
        {
            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;
            public double ExchangeRate;
            public uint? ReferenceCurrencyID;
            public bool Disable;
            public Currency(uint CurrencyID_, string CurrencyName_, string CurrencySymbol_, double ExchangeRate_, uint? ReferenceCurrencyID_, bool Disable_)
            {
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                ExchangeRate = ExchangeRate_;
                ReferenceCurrencyID = ReferenceCurrencyID_;
                Disable = Disable_;
            }

        }
        public class Money_Currency
        {
            public Currency _Currency;
            public double Value;
            public double ExchangeRate;
            public Money_Currency(Currency Currency_,
             double Value_,
             double ExchangeRate_)
            {
                _Currency = Currency_;
                Value = Value_;
                ExchangeRate = ExchangeRate_;
            }
            public static string ConvertMoney_CurrencyList_TOString(List<Money_Currency> Money_CurrencyList)
            {
                string returnstring = "";
                try
                {
                    List<Currency> currencyList = Money_CurrencyList.Select(x => x._Currency).ToList();
                    List<uint> currencylist = Money_CurrencyList.Select(x => x._Currency.CurrencyID).Distinct().ToList();
                    for (int i = 0; i < currencylist.Count; i++)
                    {
                        Currency currency = currencyList.Where(y => y.CurrencyID == currencylist[i]).ToList()[0];
                        returnstring += Math.Round(Money_CurrencyList.Where(x => x._Currency.CurrencyID == currencylist[i]).Sum(y => y.Value), 3)
                            + currency.CurrencySymbol;
                        if (i < currencyList.Count - 1) returnstring += " , ";
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("ConvertMoney_CurrencyList_TOString:" + ee.Message);

                }
                if (returnstring.Length == 0) return " - ";
                else return returnstring;
            }
            public static List<Money_Currency> Get_Money_Currency_List_From_MoneyTransform(List<MoneyTransFormOPR> MoneyTransFormOPRList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < MoneyTransFormOPRList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(MoneyTransFormOPRList[i]._Currency, MoneyTransFormOPRList[i].Value, MoneyTransFormOPRList[i].ExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_PayIN:" + ee.Message);

                }
                return Money_CurrencyList;
            }
            public static List<Money_Currency> Get_Money_Currency_List_From_PayIN(List<PayIN> PayINList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < PayINList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(PayINList[i]._Currency, PayINList[i].Value, PayINList[i].ExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_PayIN:" + ee.Message);

                }
                return Money_CurrencyList;
            }
            public static List<Money_Currency> Get_Money_Currency_List_From_PayOUT(List<PayOUT> PayOUTList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < PayOUTList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(PayOUTList[i]._Currency, PayOUTList[i].Value, PayOUTList[i].ExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_PayIN:" + ee.Message);

                }
                return Money_CurrencyList;
            }

            public static List<Money_Currency> Get_Money_Currency_List_From_ExchangeOPR_INDirection(List<ExchangeOPR> ExchangeOPRList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < ExchangeOPRList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(ExchangeOPRList[i].TargetCurrency, ExchangeOPRList[i].OutMoneyValue * (ExchangeOPRList[i].TargetExchangeRate / ExchangeOPRList[i].SourceExchangeRate), ExchangeOPRList[i].TargetExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_ExchangeOPR_INDirection:" + ee.Message);

                }
                return Money_CurrencyList;
            }
            public static List<Money_Currency> Get_Money_Currency_List_From_ExchangeOPR_OUTDirection(List<ExchangeOPR> ExchangeOPRList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < ExchangeOPRList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(ExchangeOPRList[i].SourceCurrency, ExchangeOPRList[i].OutMoneyValue, ExchangeOPRList[i].SourceExchangeRate));
                    }
                    return Money_CurrencyList;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_ExchangeOPR_OUTDirection:" + ee.Message);

                }

            }


            public static List<Money_Currency> Get_Money_Currency_List_From_ItemOUT(List<ItemOUT> ItemOUTList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < ItemOUTList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(ItemOUTList[i]._OUTValue._Currency, ItemOUTList[i]._OUTValue.Value * ItemOUTList[i].Amount , ItemOUTList[i]._OUTValue.ExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_PayIN:" + ee.Message);

                }
                return Money_CurrencyList;
            }
            public static List<Money_Currency> Get_Money_Currency_List_From_ItemIN(List<ItemIN> ItemINList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < ItemINList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(ItemINList[i]._INCost._Currency, ItemINList[i]._INCost.Value, ItemINList[i]._INCost.ExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_PayIN:" + ee.Message);

                }
                return Money_CurrencyList;
            }
            public static List<Money_Currency> Get_Money_Currency_List_From_ItemIN_By_ItemOUTList(List<ItemOUT> ItemOUTList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < ItemOUTList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(ItemOUTList[i]._ItemIN._INCost._Currency,
                            ItemOUTList[i]._ItemIN._INCost.Value* ItemOUTList[i] .Amount * (ItemOUTList[i]._ConsumeUnit.Factor / ItemOUTList[i]._ItemIN._ConsumeUnit.Factor), ItemOUTList[i]._ItemIN._INCost.ExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_PayIN:" + ee.Message);

                }
                return Money_CurrencyList;
            }

            public static List<Money_Currency> Get_Money_Currency_List_From_EmployeePayOrder(List<EmployeePayOrder> employeePayOrderList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < employeePayOrderList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(employeePayOrderList[i]._Currency, employeePayOrderList[i].Value, employeePayOrderList[i].ExchangeRate));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Money_Currency_List_From_PayIN:" + ee.Message);

                }
                return Money_CurrencyList;
            }
        }
        public class MoneyTransFormOPR
        {
            public DatabaseInterface.User Creator_User;
            public uint MoneyTransFormOPRID;
            public DateTime MoneyTransFormOPRDate;
            public MoneyBox SourceMoneyBox;
            public MoneyBox TargetMoneyBox;
            public double Value;
            public Currency _Currency;
            public double ExchangeRate;
            public string Notes;
            public DatabaseInterface.User Confirm_User;
            public MoneyTransFormOPR(
               DatabaseInterface.User Creator_User_,
            uint MoneyTransFormOPRID_,
             DateTime MoneyTransFormOPRDate_,
             MoneyBox SourceMoneyBox_,
             MoneyBox TargetMoneyBox_,
             double Value_,
             double ExchangeRate_,
             Currency Currency_,
             string Notes_,
             DatabaseInterface.User Confirm_User_)
            {
                Creator_User = Creator_User_;
                MoneyTransFormOPRID = MoneyTransFormOPRID_;
                MoneyTransFormOPRDate = MoneyTransFormOPRDate_;
                SourceMoneyBox = SourceMoneyBox_;
                TargetMoneyBox = TargetMoneyBox_;
                Value = Value_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Notes = Notes_;
                Confirm_User = Confirm_User_;
            }

        }
        public class ExchangeOPR
        {
            public MoneyBox _MoneyBox;
            public uint ExchangeOprID;
            public DateTime ExchangeOprDate;
            public Currency SourceCurrency;
            public double SourceExchangeRate;
            public double OutMoneyValue;
            public Currency TargetCurrency;
            public double TargetExchangeRate;
            public string Notes;
            public ExchangeOPR(MoneyBox MoneyBox_, uint ExchangeOprID_, DateTime ExchangeOprDate_,
                Currency SourceCurrency_, double SourceExchangeRate_, double OutMoneyValue_, Currency TargetCurrency_, double TargetExchangeRate_, string Notes_)
            {
                _MoneyBox = MoneyBox_;
                ExchangeOprID = ExchangeOprID_;
                ExchangeOprDate = ExchangeOprDate_;
                SourceCurrency = SourceCurrency_;
                SourceExchangeRate = SourceExchangeRate_;
                OutMoneyValue = OutMoneyValue_;
                TargetCurrency = TargetCurrency_;
                TargetExchangeRate = TargetExchangeRate_;
                Notes = Notes_;
            }
        }
        public class PayIN
        {
            public MoneyBox _MoneyBox;
            public uint PayOprID;
            public DateTime PayOprDate;
            public Bill _Bill;
            public string PayDescription;
            public double Value;
            public Currency _Currency;
            public double ExchangeRate;
            public string Notes;

            public PayIN(MoneyBox MoneyBox_, uint PayOprID_, DateTime PayOprDate_, Bill Bill_, string PayDescription_, double Value_, double ExchangeRate_, Currency Currency_, string Notes_)
            {

                _MoneyBox = MoneyBox_;
                PayOprID = PayOprID_;
                PayOprDate = PayOprDate_;
                _Bill = Bill_;
                PayDescription = PayDescription_;
                Value = Value_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Notes = Notes_;
            }


        }
        public class PayOUT
        {
            public MoneyBox _MoneyBox;
            public uint PayOprID;
            public DateTime PayOprDate;
            public Bill _Bill;
            public EmployeePayOrder _EmployeePayOrder;

            public string PayDescription;
            public double Value;
            public Currency _Currency;
            public double ExchangeRate;
            public string Notes;

            public PayOUT(MoneyBox MoneyBox_, uint PayOprID_, DateTime PayOprDate_, Bill Bill_, string PayDescription_, double Value_, double ExchangeRate_, Currency Currency_, string Notes_)
            {
                _MoneyBox = MoneyBox_;
                PayOprID = PayOprID_;
                PayOprDate = PayOprDate_;
                _Bill = Bill_;
                PayDescription = PayDescription_;
                Value = Value_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Notes = Notes_;
            }
            public PayOUT(MoneyBox MoneyBox_, uint PayOprID_, DateTime PayOprDate_, EmployeePayOrder EmployeePayOrder_, string PayDescription_, double Value_, double ExchangeRate_, Currency Currency_, string Notes_)
            {
                _MoneyBox = MoneyBox_;
                PayOprID = PayOprID_;
                PayOprDate = PayOprDate_;
                _EmployeePayOrder = EmployeePayOrder_;
                PayDescription = PayDescription_;
                Value = Value_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Notes = Notes_;
            }

        }

        public class MoneyBox
        {
            public uint BoxID;
            public string BoxName;
            public MoneyBox(uint BoxID_,
             string BoxName_)
            {
                BoxID = BoxID_;
                BoxName = BoxName_;
            }
        }
        #region Report_Buy
        public class Report_Buys_Day_ReportDetail
        {

            public DateTime Bill_Time;
            public uint Bill_ID;
            public string Bill_Owner;
            public int ClauseS_Count;
            public double Amount_IN;
            public double Amount_Remain;
            public double BillValue;
            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;
            public double ExchangeRate;
            public string PaysAmount;
            public double PaysRemain;
            public double Bill_RealValue;
            public double Bill_Pays_RealValue;
            public string Bill_ItemsOut_Value;
            public double Bill_ItemsOut_RealValue;
            public string Bill_Pays_Return_Value;
            public double Bill_Pays_Return_RealValue;

            public Report_Buys_Day_ReportDetail(DateTime Bill_Time_,
             uint Bill_ID_,
             string Bill_Owner_,
             int ClauseS_Count_,
             double Amount_IN_,
             double Amount_Remain_,
             double BillValue_,
              uint CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
             double ExchangeRate_,
             string PaysAmount_,
             double PaysRemain_,
             double Bill_RealValue_,
             double Bill_Pays_RealValue_,
             string Bill_ItemsOut_Value_,
             double Bill_ItemsOut_RealValue_,
             string Bill_Pays_Return_Value_,
             double Bill_Pays_Return_RealValue_
               )
            {
                Bill_Time = Bill_Time_;
                Bill_ID = Bill_ID_;
                Bill_Owner = Bill_Owner_;
                ClauseS_Count = ClauseS_Count_;
                Amount_IN = Amount_IN_;
                Amount_Remain = Amount_Remain_;
                BillValue = BillValue_;
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                ExchangeRate = ExchangeRate_;
                PaysAmount = PaysAmount_;
                PaysRemain = PaysRemain_;
                Bill_RealValue = Bill_RealValue_;
                Bill_Pays_RealValue = Bill_Pays_RealValue_;
                Bill_ItemsOut_Value = Bill_ItemsOut_Value_;
                Bill_ItemsOut_RealValue = Bill_ItemsOut_RealValue_;
                Bill_Pays_Return_Value = Bill_Pays_Return_Value_;
                Bill_Pays_Return_RealValue = Bill_Pays_Return_RealValue_;

            }
            internal static DataTable Get_Report_Buys_Day_ReportDetail_List_AS_DataTable(List<Report_Buys_Day_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("Bill_Time", typeof(DateTime));
                    table.Columns.Add("Bill_Owner", typeof(string));
                    table.Columns.Add("Bill_ID", typeof(uint));
                    table.Columns.Add("ClauseS_Count", typeof(int));
                    table.Columns.Add("Amount_IN", typeof(double));
                    table.Columns.Add("Amount_Remain", typeof(double));
                    table.Columns.Add("BillValue", typeof(double));
                    table.Columns.Add("CurrencyID", typeof(uint));
                    table.Columns.Add("CurrencyName", typeof(string));
                    table.Columns.Add("CurrencySymbol", typeof(string));
                    table.Columns.Add("ExchangeRate", typeof(double));
                    table.Columns.Add("PaysAmount", typeof(string));
                    table.Columns.Add("PaysRemain", typeof(double));
                    table.Columns.Add("Bill_RealValue", typeof(double));
                    table.Columns.Add("Bill_Pays_RealValue", typeof(double));
                    table.Columns.Add("Bill_ItemsOut_Value", typeof(string));
                    table.Columns.Add("Bill_ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("Bill_Pays_Return_Value", typeof(string));
                    table.Columns.Add("Bill_Pays_Return_RealValue", typeof(double));
                    ;
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["Bill_Time"] = list[i].Bill_Time;
                        row["Bill_Owner"] = list[i].Bill_Owner;
                        row["Bill_ID"] = list[i].Bill_ID;
                        row["ClauseS_Count"] = list[i].ClauseS_Count;
                        row["Amount_IN"] = list[i].Amount_IN;
                        row["Amount_Remain"] = list[i].Amount_Remain;
                        row["BillValue"] = list[i].BillValue;
                        row["CurrencyID"] = list[i].CurrencyID;
                        row["CurrencyName"] = list[i].CurrencyName;
                        row["CurrencySymbol"] = list[i].CurrencySymbol;
                        row["ExchangeRate"] = list[i].ExchangeRate;
                        row["PaysAmount"] = list[i].PaysAmount;
                        row["PaysRemain"] = list[i].PaysRemain;
                        row["Bill_RealValue"] = list[i].Bill_RealValue;
                        row["Bill_Pays_RealValue"] = list[i].Bill_Pays_RealValue;
                        row["Bill_ItemsOut_Value"] = list[i].Bill_ItemsOut_Value;
                        row["Bill_ItemsOut_RealValue"] = list[i].Bill_ItemsOut_RealValue;
                        row["Bill_Pays_Return_Value"] = list[i].Bill_Pays_Return_Value;
                        row["Bill_Pays_Return_RealValue"] = list[i].Bill_Pays_Return_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Buys_ReportDetail_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_Buys_Month_ReportDetail
        {

            public int DayID;
            public DateTime DayDate;
            public int Bills_Count;
            public double Amount_IN;
            public double Amount_Remain;
            public string Bills_Value;
            public string Bills_Pays_Value;
            public string Bills_Pays_Remain;
            public double Bills_Pays_Remain_UPON_Bill_Currency;
            public double Bills_RealValue;
            public double Bills_Pays_RealValue;
            public string Bills_ItemsOut_Value;
            public double Bills_ItemsOut_RealValue;
            public string Bills_Pays_Return_Value;
            public double Bills_Pays_Return_RealValue;
            public Report_Buys_Month_ReportDetail(
                    int DayID_,
                    DateTime DayDate_,
              int Bills_Count_,
             double Amount_IN_,
             double Amount_Remain_,
             string Bills_Value_,
             string Bills_Pays_Value_,
             string Bills_Pays_Remain_,
             double Bills_Pays_Remain_UPON_Bill_Currency_,
             double Bills_RealValue_,
             double Bills_Pays_RealValue_,
             string Bills_ItemsOut_Value_,
             double Bills_ItemsOut_RealValue_,
             string Bills_Pays_Return_Value_,
             double Bills_Pays_Return_RealValue_)
            {
                DayID = DayID_;
                DayDate = DayDate_;
                Bills_Count = Bills_Count_;
                Amount_IN = Amount_IN_;
                Amount_Remain = Amount_Remain_;
                Bills_Value = Bills_Value_;

                Bills_Pays_Value = Bills_Pays_Value_;
                Bills_Pays_Remain = Bills_Pays_Remain_;
                Bills_Pays_Remain_UPON_Bill_Currency = Bills_Pays_Remain_UPON_Bill_Currency_;

                Bills_RealValue = Bills_RealValue_;
                Bills_Pays_RealValue = Bills_Pays_RealValue_;
                Bills_ItemsOut_Value = Bills_ItemsOut_Value_;
                Bills_ItemsOut_RealValue = Bills_ItemsOut_RealValue_;
                Bills_Pays_Return_Value = Bills_Pays_Return_Value_;
                Bills_Pays_Return_RealValue = Bills_Pays_Return_RealValue_;
            }
            internal static DataTable Get_Report_Buys_Month_ReportDetail_List_AS_DataTable(List<Report_Buys_Month_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("DayID", typeof(int));
                    table.Columns.Add("DayDate", typeof(DateTime));
                    table.Columns.Add("Bills_Count", typeof(int));
                    table.Columns.Add("Amount_IN", typeof(double));
                    table.Columns.Add("Amount_Remain", typeof(double));
                    table.Columns.Add("Bills_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain_UPON_Bill_Currency", typeof(double));
                    table.Columns.Add("Bills_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_RealValue", typeof(double));
                    table.Columns.Add("Bills_ItemsOut_Value", typeof(string));
                    table.Columns.Add("Bills_ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_Return_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Return_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["DayID"] = list[i].DayID;
                        row["DayDate"] = list[i].DayDate;
                        row["Bills_Count"] = list[i].Bills_Count;
                        row["Amount_IN"] = list[i].Amount_IN;
                        row["Amount_Remain"] = list[i].Amount_Remain;
                        row["Bills_Value"] = list[i].Bills_Value;
                        row["Bills_Pays_Value"] = list[i].Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = list[i].Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_Bill_Currency"] = list[i].Bills_Pays_Remain_UPON_Bill_Currency;
                        row["Bills_RealValue"] = list[i].Bills_RealValue;
                        row["Bills_Pays_RealValue"] = list[i].Bills_Pays_RealValue;
                        row["Bills_ItemsOut_Value"] = list[i].Bills_ItemsOut_Value;
                        row["Bills_ItemsOut_RealValue"] = list[i].Bills_ItemsOut_RealValue;
                        row["Bills_Pays_Return_Value"] = list[i].Bills_Pays_Return_Value;
                        row["Bills_Pays_Return_RealValue"] = list[i].Bills_Pays_Return_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Month_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_Buys_Year_ReportDetail
        {

            public int MonthNO;
            public string MonthName;
            public int Bills_Count;
            public double Amount_IN;
            public double Amount_Remain;
            public string Bills_Value;

            public string Bills_Pays_Value;
            public string Bills_Pays_Remain;
            public double Bills_Pays_Remain_UPON_Bill_Currency;

            public double Bills_RealValue;
            public double Bills_Pays_RealValue;
            public string Bills_ItemsOut_Value;
            public double Bills_ItemsOut_RealValue;
            public string Bills_Pays_Return_Value;
            public double Bills_Pays_Return_RealValue;

            public Report_Buys_Year_ReportDetail(
                    int MOnthNO_,
                    string MonthName_,
               int Bills_Count_,
             double Amount_IN_,
             double Amount_Remain_,
             string Bills_Value_,
             string Bills_Pays_Value_,
             string Bills_Pays_Remain_,

             double Bills_Pays_Remain_UPON_Bill_Currency_,

             double Bills_RealValue_,
             double Bills_Pays_RealValue_,
             string Bills_ItemsOut_Value_,
             double Bills_ItemsOut_RealValue_,
             string Bills_Pays_Return_Value_,
             double Bills_Pays_Return_RealValue_)
            {
                MonthNO = MOnthNO_;
                MonthName = MonthName_;
                Bills_Count = Bills_Count_;
                Amount_IN = Amount_IN_;
                Amount_Remain = Amount_Remain_;

                Bills_Value = Bills_Value_;
                Bills_Pays_Value = Bills_Pays_Value_;
                Bills_Pays_Remain = Bills_Pays_Remain_;
                Bills_Pays_Remain_UPON_Bill_Currency = Bills_Pays_Remain_UPON_Bill_Currency_;

                Bills_RealValue = Bills_RealValue_;
                Bills_Pays_RealValue = Bills_Pays_RealValue_;
                Bills_ItemsOut_Value = Bills_ItemsOut_Value_;
                Bills_ItemsOut_RealValue = Bills_ItemsOut_RealValue_;
                Bills_Pays_Return_Value = Bills_Pays_Return_Value_;
                Bills_Pays_Return_RealValue = Bills_Pays_Return_RealValue_;
            }

            internal static DataTable Get_Report_Buys_Year_ReportDetail_List_AS_DataTable(List<Report_Buys_Year_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("MonthNO", typeof(int));
                    table.Columns.Add("MonthName", typeof(string));
                    table.Columns.Add("Bills_Count", typeof(int));
                    table.Columns.Add("Amount_IN", typeof(double));
                    table.Columns.Add("Amount_Remain", typeof(double));
                    table.Columns.Add("Bills_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain_UPON_Bill_Currency", typeof(double));
                    table.Columns.Add("Bills_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_RealValue", typeof(double));
                    table.Columns.Add("Bills_ItemsOut_Value", typeof(string));
                    table.Columns.Add("Bills_ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_Return_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Return_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["MonthNO"] = list[i].MonthNO;
                        row["MonthName"] = list[i].MonthName;
                        row["Bills_Count"] = list[i].Bills_Count;
                        row["Amount_IN"] = list[i].Amount_IN;
                        row["Amount_Remain"] = list[i].Amount_Remain;
                        row["Bills_Value"] = list[i].Bills_Value;
                        row["Bills_Pays_Value"] = list[i].Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = list[i].Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_Bill_Currency"] = list[i].Bills_Pays_Remain_UPON_Bill_Currency;
                        row["Bills_RealValue"] = list[i].Bills_RealValue;
                        row["Bills_Pays_RealValue"] = list[i].Bills_Pays_RealValue;
                        row["Bills_ItemsOut_Value"] = list[i].Bills_ItemsOut_Value;
                        row["Bills_ItemsOut_RealValue"] = list[i].Bills_ItemsOut_RealValue;
                        row["Bills_Pays_Return_Value"] = list[i].Bills_Pays_Return_Value;
                        row["Bills_Pays_Return_RealValue"] = list[i].Bills_Pays_Return_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Year_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_Buys_YearRange_ReportDetail
        {

            public int YearNO;
            public int Bills_Count;
            public double Amount_IN;
            public double Amount_Remain;
            public string Bills_Value;

            public string Bills_Pays_Value;
            public string Bills_Pays_Remain;
            public double Bills_Pays_Remain_UPON_Bill_Currency;

            public double Bills_RealValue;
            public double Bills_Pays_RealValue;
            public string Bills_ItemsOut_Value;
            public double Bills_ItemsOut_RealValue;
            public string Bills_Pays_Return_Value;
            public double Bills_Pays_Return_RealValue;
            public Report_Buys_YearRange_ReportDetail(
                    int YearNO_,
              int Bills_Count_,
             double Amount_IN_,
             double Amount_Remain_,
             string Bills_Value_,
             string Bills_Pays_Value_,
             string Bills_Pays_Remain_,

             double Bills_Pays_Remain_UPON_Bill_Currency_,

             double Bills_RealValue_,
             double Bills_Pays_RealValue_,
             string Bills_ItemsOut_Value_,
             double Bills_ItemsOut_RealValue_,
             string Bills_Pays_Return_Value_,
             double Bills_Pays_Return_RealValue_)
            {
                YearNO = YearNO_;

                Bills_Count = Bills_Count_;
                Amount_IN = Amount_IN_;
                Amount_Remain = Amount_Remain_;
                Bills_Value = Bills_Value_;

                Bills_Pays_Value = Bills_Pays_Value_;
                Bills_Pays_Remain = Bills_Pays_Remain_;
                Bills_Pays_Remain_UPON_Bill_Currency = Bills_Pays_Remain_UPON_Bill_Currency_;

                Bills_RealValue = Bills_RealValue_;
                Bills_Pays_RealValue = Bills_Pays_RealValue_;
                Bills_ItemsOut_Value = Bills_ItemsOut_Value_;
                Bills_ItemsOut_RealValue = Bills_ItemsOut_RealValue_;
                Bills_Pays_Return_Value = Bills_Pays_Return_Value_;
                Bills_Pays_Return_RealValue = Bills_Pays_Return_RealValue_;
            }

            internal static DataTable Get_Report_Buys_YearRange_ReportDetail_List_AS_DataTable(List<Report_Buys_YearRange_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("YearNO", typeof(int));
                    table.Columns.Add("Bills_Count", typeof(int));
                    table.Columns.Add("Amount_IN", typeof(double));
                    table.Columns.Add("Amount_Remain", typeof(double));
                    table.Columns.Add("Bills_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain_UPON_Bill_Currency", typeof(double));
                    table.Columns.Add("Bills_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_RealValue", typeof(double));
                    table.Columns.Add("Bills_ItemsOut_Value", typeof(string));
                    table.Columns.Add("Bills_ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_Return_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Return_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["YearNO"] = list[i].YearNO;
                        row["Bills_Count"] = list[i].Bills_Count;
                        row["Amount_IN"] = list[i].Amount_IN;
                        row["Amount_Remain"] = list[i].Amount_Remain;
                        row["Bills_Value"] = list[i].Bills_Value;
                        row["Bills_Pays_Value"] = list[i].Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = list[i].Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_Bill_Currency"] = list[i].Bills_Pays_Remain_UPON_Bill_Currency;
                        row["Bills_RealValue"] = list[i].Bills_RealValue;
                        row["Bills_Pays_RealValue"] = list[i].Bills_Pays_RealValue;
                        row["Bills_ItemsOut_Value"] = list[i].Bills_ItemsOut_Value;
                        row["Bills_ItemsOut_RealValue"] = list[i].Bills_ItemsOut_RealValue;
                        row["Bills_Pays_Return_Value"] = list[i].Bills_Pays_Return_Value;
                        row["Bills_Pays_Return_RealValue"] = list[i].Bills_Pays_Return_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_YearRange_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }

        }

        #endregion
        #region Report_MaintenanceOPR

        public class Report_MaintenanceOPRs_Day_ReportDetail
        {
            public DateTime MaintenanceOPR_Date;
            public uint MaintenanceOPR_ID;
            public string MaintenanceOPR_Owner;

            public uint ItemID;
            public string ItemName;
            public string ItemCompany;
            public string FolderName;
            public string FalutDesc;
            public DateTime? MaintenanceOPR_Endworkdate;
            public bool? MaintenanceOPR_Rpaired;
            public DateTime? MaintenanceOPR_DeliverDate;
            public DateTime? MaintenanceOPR_EndWarrantyDate;
            public uint? BillMaintenanceID;
            public double? BillValue;
            public uint? CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;
            public double? ExchangeRate;
            public string PaysAmount;
            public double? PaysRemain;
            public string Bill_ItemsOut_Value;
            public double? Bill_ItemsOut_RealValue;
            public double? Bill_RealValue;
            public double? Bill_Pays_RealValue;





            public Report_MaintenanceOPRs_Day_ReportDetail(
                DateTime MaintenanceOPR_Date_,
             uint MaintenanceOPR_ID_,
             string MaintenanceOPR_Owner_,

            uint ItemID_,
             string ItemName_,
             string ItemCompany_,
             string FolderName_,
            string FalutDesc_,
              DateTime? MaintenanceOPR_Endworkdate_,
             bool? MaintenanceOPR_Rpaired_,
             DateTime? MaintenanceOPR_DeliverDate_,
            DateTime? MaintenanceOPR_EndWarrantyDate_,
             uint? BillMaintenanceID_,
              double? BillValue_,
               uint? CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
            double? ExchangeRate_,
             string PaysAmount_,
             double? PaysRemain_,
            string Bill_ItemsOut_Value_,
             double? Bill_ItemsOut_RealValue_,
             double? Bill_RealValue_,
             double? Bill_Pays_RealValue_


               )
            {
                MaintenanceOPR_Date = MaintenanceOPR_Date_;
                MaintenanceOPR_ID = MaintenanceOPR_ID_;
                MaintenanceOPR_Owner = MaintenanceOPR_Owner_;
                ItemID = ItemID_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                FolderName = FolderName_;
                FalutDesc = FalutDesc_;
                MaintenanceOPR_Endworkdate = MaintenanceOPR_Endworkdate_;
                MaintenanceOPR_Rpaired = MaintenanceOPR_Rpaired_;
                MaintenanceOPR_DeliverDate = MaintenanceOPR_DeliverDate_;
                MaintenanceOPR_EndWarrantyDate = MaintenanceOPR_EndWarrantyDate_;
                BillMaintenanceID = BillMaintenanceID_;
                BillValue = BillValue_;
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                ExchangeRate = ExchangeRate_;
                PaysAmount = PaysAmount_;
                PaysRemain = PaysRemain_;
                Bill_ItemsOut_Value = Bill_ItemsOut_Value_;
                Bill_ItemsOut_RealValue = Bill_ItemsOut_RealValue_;
                Bill_RealValue = Bill_RealValue_;
                Bill_Pays_RealValue = Bill_Pays_RealValue_;


            }
            internal static DataTable Get_Report_MaintenanceOPRs_Day_ReportDetail_List_AS_DataTable(List<Report_MaintenanceOPRs_Day_ReportDetail> list)
            {

                try
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("MaintenanceOPR_Date", typeof(DateTime));
                    table.Columns.Add("MaintenanceOPR_ID", typeof(uint));
                    table.Columns.Add("MaintenanceOPR_Owner", typeof(string));

                    table.Columns.Add("ItemID", typeof(uint));
                    table.Columns.Add("ItemName", typeof(string));
                    table.Columns.Add("ItemCompany", typeof(string));
                    table.Columns.Add("FolderName", typeof(string));
                    table.Columns.Add("FalutDesc", typeof(string));
                    table.Columns.Add("MaintenanceOPR_Endworkdate", typeof(object));
                    table.Columns.Add("MaintenanceOPR_Rpaired", typeof(object));
                    table.Columns.Add("MaintenanceOPR_DeliverDate", typeof(object));
                    table.Columns.Add("MaintenanceOPR_EndWarrantyDate", typeof(object));
                    table.Columns.Add("BillMaintenanceID", typeof(object));
                    table.Columns.Add("BillValue", typeof(object));
                    table.Columns.Add("CurrencyID", typeof(object));
                    table.Columns.Add("CurrencyName", typeof(string));
                    table.Columns.Add("CurrencySymbol", typeof(string));
                    table.Columns.Add("ExchangeRate", typeof(object));
                    table.Columns.Add("PaysAmount", typeof(string));
                    table.Columns.Add("PaysRemain", typeof(object));
                    table.Columns.Add("Bill_ItemsOut_Value", typeof(string));
                    table.Columns.Add("Bill_ItemsOut_RealValue", typeof(object));
                    table.Columns.Add("Bill_RealValue", typeof(object));
                    table.Columns.Add("Bill_Pays_RealValue", typeof(object));


                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();

                        row["MaintenanceOPR_Date"] = list[i].MaintenanceOPR_Date;
                        row["MaintenanceOPR_ID"] = list[i].MaintenanceOPR_ID;
                        row["MaintenanceOPR_Owner"] = list[i].MaintenanceOPR_Owner;

                        row["ItemID"] = list[i].ItemID;
                        row["ItemName"] = list[i].ItemName;
                        row["ItemCompany"] = list[i].ItemCompany;
                        row["FolderName"] = list[i].FolderName;
                        row["FalutDesc"] = list[i].FalutDesc;

                        row["MaintenanceOPR_Endworkdate"] = list[i].MaintenanceOPR_Endworkdate == null ? DBNull.Value : (object)list[i].MaintenanceOPR_Endworkdate;

                        row["MaintenanceOPR_Rpaired"] = list[i].MaintenanceOPR_Rpaired == null ? DBNull.Value : (object)list[i].MaintenanceOPR_Rpaired;
                        row["MaintenanceOPR_DeliverDate"] = list[i].MaintenanceOPR_DeliverDate == null ? DBNull.Value : (object)list[i].MaintenanceOPR_DeliverDate;
                        row["MaintenanceOPR_EndWarrantyDate"] = list[i].MaintenanceOPR_EndWarrantyDate == null ? DBNull.Value : (object)list[i].MaintenanceOPR_EndWarrantyDate;
                        row["BillMaintenanceID"] = list[i].BillMaintenanceID == null ? DBNull.Value : (object)list[i].BillMaintenanceID;
                        row["BillValue"] = list[i].BillValue == null ? DBNull.Value : (object)list[i].BillValue;
                        row["CurrencyID"] = list[i].CurrencyID == null ? DBNull.Value : (object)list[i].CurrencyID;
                        row["CurrencyName"] = list[i].CurrencyName;
                        row["CurrencySymbol"] = list[i].CurrencySymbol;
                        row["ExchangeRate"] = list[i].ExchangeRate == null ? DBNull.Value : (object)list[i].ExchangeRate;
                        row["PaysAmount"] = list[i].PaysAmount;
                        row["PaysRemain"] = list[i].PaysRemain == null ? DBNull.Value : (object)list[i].PaysRemain;
                        row["Bill_ItemsOut_Value"] = list[i].Bill_ItemsOut_Value;
                        row["Bill_ItemsOut_RealValue"] = list[i].Bill_ItemsOut_RealValue == null ? DBNull.Value : (object)list[i].Bill_ItemsOut_RealValue;
                        row["Bill_RealValue"] = list[i].Bill_RealValue == null ? DBNull.Value : (object)list[i].Bill_RealValue;
                        row["Bill_Pays_RealValue"] = list[i].Bill_Pays_RealValue == null ? DBNull.Value : (object)list[i].Bill_Pays_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Day_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_MaintenanceOPRs_Month_ReportDetail
        {

            public int DayID;
            public DateTime DayDate;
            public int MaintenanceOPRs_Count;
            public int MaintenanceOPRs_EndWork_Count;
            public int MaintenanceOPRs_Repaired_Count;
            public int MaintenanceOPRs_Warranty_Count;
            public int MaintenanceOPRs_EndWarranty_Count;

            public int BillMaintenances_Count;
            public string BillMaintenances_Value;
            public string BillMaintenances_Pays_Value;
            public string BillMaintenances_Pays_Remain;
            public double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;

            public string BillMaintenances_ItemsOut_Value;
            public double BillMaintenances_ItemsOut_RealValue;
            public double BillMaintenances_RealValue;
            public double BillMaintenances_Pays_RealValue;
            public Report_MaintenanceOPRs_Month_ReportDetail(
                    int DayID_,
                    DateTime DayDate_,
               int MaintenanceOPRs_Count_,
             int MaintenanceOPRs_EndWork_Count_,
             int MaintenanceOPRs_Repaired_Count_,
             int MaintenanceOPRs_Warranty_Count_,
             int MaintenanceOPRs_EndWarranty_Count_,

               int BillMaintenances_Count_,
             string BillMaintenances_Value_,
             string BillMaintenances_Pays_Value_,
             string BillMaintenances_Pays_Remain_,
             double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_,

             string BillMaintenances_ItemsOut_Value_,
             double BillMaintenances_ItemsOut_RealValue_,
             double BillMaintenances_RealValue_,
             double BillMaintenances_Pays_RealValue_)
            {
                DayID = DayID_;
                DayDate = DayDate_;
                MaintenanceOPRs_Count = MaintenanceOPRs_Count_;
                MaintenanceOPRs_EndWork_Count = MaintenanceOPRs_EndWork_Count_;
                MaintenanceOPRs_Repaired_Count = MaintenanceOPRs_Repaired_Count_;
                MaintenanceOPRs_Warranty_Count = MaintenanceOPRs_Warranty_Count_;
                MaintenanceOPRs_EndWarranty_Count = MaintenanceOPRs_EndWarranty_Count_;

                BillMaintenances_Count = BillMaintenances_Count_;
                BillMaintenances_Value = BillMaintenances_Value_;
                BillMaintenances_Pays_Value = BillMaintenances_Pays_Value_;
                BillMaintenances_Pays_Remain = BillMaintenances_Pays_Remain_;
                BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_;

                BillMaintenances_ItemsOut_Value = BillMaintenances_ItemsOut_Value_;
                BillMaintenances_ItemsOut_RealValue = BillMaintenances_ItemsOut_RealValue_;
                BillMaintenances_RealValue = BillMaintenances_RealValue_;
                BillMaintenances_Pays_RealValue = BillMaintenances_Pays_RealValue_;
            }
            internal static DataTable Get_Report_MaintenanceOPRs_Month_ReportDetail_List_AS_DataTable(List<Report_MaintenanceOPRs_Month_ReportDetail> list)
            {

                try
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("DayID", typeof(int));
                    table.Columns.Add("DayDate", typeof(DateTime));
                    table.Columns.Add("MaintenanceOPRs_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_EndWork_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_Repaired_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_Warranty_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_EndWarranty_Count", typeof(string));
                    table.Columns.Add("BillMaintenances_Count", typeof(int));
                    table.Columns.Add("BillMaintenances_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Remain", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency", typeof(double));
                    table.Columns.Add("BillMaintenances_ItemsOut_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("BillMaintenances_RealValue", typeof(double));
                    table.Columns.Add("BillMaintenances_Pays_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();

                        row["DayID"] = list[i].DayID;
                        row["DayDate"] = list[i].DayDate;
                        row["MaintenanceOPRs_Count"] = list[i].MaintenanceOPRs_Count;
                        row["MaintenanceOPRs_EndWork_Count"] = list[i].MaintenanceOPRs_EndWork_Count;
                        row["MaintenanceOPRs_Repaired_Count"] = list[i].MaintenanceOPRs_Repaired_Count;
                        row["MaintenanceOPRs_Warranty_Count"] = list[i].MaintenanceOPRs_Warranty_Count;
                        row["MaintenanceOPRs_EndWarranty_Count"] = list[i].MaintenanceOPRs_EndWarranty_Count;
                        row["BillMaintenances_Count"] = list[i].BillMaintenances_Count;
                        row["BillMaintenances_Value"] = list[i].BillMaintenances_Value;
                        row["BillMaintenances_Pays_Value"] = list[i].BillMaintenances_Pays_Value;
                        row["BillMaintenances_Pays_Remain"] = list[i].BillMaintenances_Pays_Remain;
                        row["BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency"] = list[i].BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;
                        row["BillMaintenances_ItemsOut_Value"] = list[i].BillMaintenances_ItemsOut_Value;
                        row["BillMaintenances_ItemsOut_RealValue"] = list[i].BillMaintenances_ItemsOut_RealValue;
                        row["BillMaintenances_RealValue"] = list[i].BillMaintenances_RealValue;
                        row["BillMaintenances_Pays_RealValue"] = list[i].BillMaintenances_Pays_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Month_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_MaintenanceOPRs_Year_ReportDetail
        {

            public int MonthNO;
            public string MonthName;
            public int MaintenanceOPRs_Count;
            public int MaintenanceOPRs_EndWork_Count;
            public int MaintenanceOPRs_Repaired_Count;
            public int MaintenanceOPRs_Warranty_Count;
            public int MaintenanceOPRs_EndWarranty_Count;

            public int BillMaintenances_Count;
            public string BillMaintenances_Value;
            public string BillMaintenances_Pays_Value;
            public string BillMaintenances_Pays_Remain;
            public double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;

            public string BillMaintenances_ItemsOut_Value;
            public double BillMaintenances_ItemsOut_RealValue;
            public double BillMaintenances_RealValue;
            public double BillMaintenances_Pays_RealValue;

            public Report_MaintenanceOPRs_Year_ReportDetail(
                    int MOnthNO_,
                    string MonthName_,
                 int MaintenanceOPRs_Count_,
             int MaintenanceOPRs_EndWork_Count_,
             int MaintenanceOPRs_Repaired_Count_,
             int MaintenanceOPRs_Warranty_Count_,
             int MaintenanceOPRs_EndWarranty_Count_,

               int BillMaintenances_Count_,
             string BillMaintenances_Value_,
             string BillMaintenances_Pays_Value_,
             string BillMaintenances_Pays_Remain_,
             double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_,

             string BillMaintenances_ItemsOut_Value_,
             double BillMaintenances_ItemsOut_RealValue_,
             double BillMaintenances_RealValue_,
             double BillMaintenances_Pays_RealValue_)
            {
                MonthNO = MOnthNO_;
                MonthName = MonthName_;
                MaintenanceOPRs_Count = MaintenanceOPRs_Count_;
                MaintenanceOPRs_EndWork_Count = MaintenanceOPRs_EndWork_Count_;
                MaintenanceOPRs_Repaired_Count = MaintenanceOPRs_Repaired_Count_;
                MaintenanceOPRs_Warranty_Count = MaintenanceOPRs_Warranty_Count_;
                MaintenanceOPRs_EndWarranty_Count = MaintenanceOPRs_EndWarranty_Count_;

                BillMaintenances_Count = BillMaintenances_Count_;
                BillMaintenances_Value = BillMaintenances_Value_;
                BillMaintenances_Pays_Value = BillMaintenances_Pays_Value_;
                BillMaintenances_Pays_Remain = BillMaintenances_Pays_Remain_;
                BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_;

                BillMaintenances_ItemsOut_Value = BillMaintenances_ItemsOut_Value_;
                BillMaintenances_ItemsOut_RealValue = BillMaintenances_ItemsOut_RealValue_;
                BillMaintenances_RealValue = BillMaintenances_RealValue_;
                BillMaintenances_Pays_RealValue = BillMaintenances_Pays_RealValue_;
            }
            internal static DataTable Get_Report_MaintenanceOPRs_Year_ReportDetail_List_AS_DataTable(List<Report_MaintenanceOPRs_Year_ReportDetail> list)
            {

                try
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("MonthNO", typeof(int));
                    table.Columns.Add("MonthName", typeof(string));
                    table.Columns.Add("MaintenanceOPRs_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_EndWork_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_Repaired_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_Warranty_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_EndWarranty_Count", typeof(string));
                    table.Columns.Add("BillMaintenances_Count", typeof(int));
                    table.Columns.Add("BillMaintenances_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Remain", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency", typeof(double));
                    table.Columns.Add("BillMaintenances_ItemsOut_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("BillMaintenances_RealValue", typeof(double));
                    table.Columns.Add("BillMaintenances_Pays_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();

                        row["MonthNO"] = list[i].MonthNO;
                        row["MonthName"] = list[i].MonthName;
                        row["MaintenanceOPRs_Count"] = list[i].MaintenanceOPRs_Count;
                        row["MaintenanceOPRs_EndWork_Count"] = list[i].MaintenanceOPRs_EndWork_Count;
                        row["MaintenanceOPRs_Repaired_Count"] = list[i].MaintenanceOPRs_Repaired_Count;
                        row["MaintenanceOPRs_Warranty_Count"] = list[i].MaintenanceOPRs_Warranty_Count;
                        row["MaintenanceOPRs_EndWarranty_Count"] = list[i].MaintenanceOPRs_EndWarranty_Count;
                        row["BillMaintenances_Count"] = list[i].BillMaintenances_Count;
                        row["BillMaintenances_Value"] = list[i].BillMaintenances_Value;
                        row["BillMaintenances_Pays_Value"] = list[i].BillMaintenances_Pays_Value;
                        row["BillMaintenances_Pays_Remain"] = list[i].BillMaintenances_Pays_Remain;
                        row["BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency"] = list[i].BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;
                        row["BillMaintenances_ItemsOut_Value"] = list[i].BillMaintenances_ItemsOut_Value;
                        row["BillMaintenances_ItemsOut_RealValue"] = list[i].BillMaintenances_ItemsOut_RealValue;
                        row["BillMaintenances_RealValue"] = list[i].BillMaintenances_RealValue;
                        row["BillMaintenances_Pays_RealValue"] = list[i].BillMaintenances_Pays_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Year_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
        }
        public class Report_MaintenanceOPRs_YearRange_ReportDetail
        {

            public int YearNO;
            public int MaintenanceOPRs_Count;
            public int MaintenanceOPRs_EndWork_Count;
            public int MaintenanceOPRs_Repaired_Count;
            public int MaintenanceOPRs_Warranty_Count;
            public int MaintenanceOPRs_EndWarranty_Count;

            public int BillMaintenances_Count;
            public string BillMaintenances_Value;
            public string BillMaintenances_Pays_Value;
            public string BillMaintenances_Pays_Remain;
            public double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;

            public string BillMaintenances_ItemsOut_Value;
            public double BillMaintenances_ItemsOut_RealValue;
            public double BillMaintenances_RealValue;
            public double BillMaintenances_Pays_RealValue;
            public Report_MaintenanceOPRs_YearRange_ReportDetail(
                    int YearNO_,
                 int MaintenanceOPRs_Count_,
             int MaintenanceOPRs_EndWork_Count_,
             int MaintenanceOPRs_Repaired_Count_,
             int MaintenanceOPRs_Warranty_Count_,
             int MaintenanceOPRs_EndWarranty_Count_,
              int BillMaintenances_Count_,
            string BillMaintenances_Value_,
             string BillMaintenances_Pays_Value_,
             string BillMaintenances_Pays_Remain_,
             double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_,

             string BillMaintenances_ItemsOut_Value_,
             double BillMaintenances_ItemsOut_RealValue_,
             double BillMaintenances_RealValue_,
             double BillMaintenances_Pays_RealValue_)
            {
                YearNO = YearNO_;

                MaintenanceOPRs_Count = MaintenanceOPRs_Count_;
                MaintenanceOPRs_EndWork_Count = MaintenanceOPRs_EndWork_Count_;
                MaintenanceOPRs_Repaired_Count = MaintenanceOPRs_Repaired_Count_;
                MaintenanceOPRs_Warranty_Count = MaintenanceOPRs_Warranty_Count_;
                MaintenanceOPRs_EndWarranty_Count = MaintenanceOPRs_EndWarranty_Count_;

                BillMaintenances_Count = BillMaintenances_Count_;
                BillMaintenances_Value = BillMaintenances_Value_;
                BillMaintenances_Pays_Value = BillMaintenances_Pays_Value_;
                BillMaintenances_Pays_Remain = BillMaintenances_Pays_Remain_;
                BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_;

                BillMaintenances_ItemsOut_Value = BillMaintenances_ItemsOut_Value_;
                BillMaintenances_ItemsOut_RealValue = BillMaintenances_ItemsOut_RealValue_;
                BillMaintenances_RealValue = BillMaintenances_RealValue_;
                BillMaintenances_Pays_RealValue = BillMaintenances_Pays_RealValue_;
            }
            internal static DataTable Get_Report_MaintenanceOPRs_YearRange_ReportDetail_List_AS_DataTable(List<Report_MaintenanceOPRs_YearRange_ReportDetail> list)
            {

                try
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("YearNO", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_EndWork_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_Repaired_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_Warranty_Count", typeof(int));
                    table.Columns.Add("MaintenanceOPRs_EndWarranty_Count", typeof(string));
                    table.Columns.Add("BillMaintenances_Count", typeof(int));
                    table.Columns.Add("BillMaintenances_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Remain", typeof(string));
                    table.Columns.Add("BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency", typeof(double));
                    table.Columns.Add("BillMaintenances_ItemsOut_Value", typeof(string));
                    table.Columns.Add("BillMaintenances_ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("BillMaintenances_RealValue", typeof(double));
                    table.Columns.Add("BillMaintenances_Pays_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();

                        row["YearNO"] = list[i].YearNO;
                        row["MaintenanceOPRs_Count"] = list[i].MaintenanceOPRs_Count;
                        row["MaintenanceOPRs_EndWork_Count"] = list[i].MaintenanceOPRs_EndWork_Count;
                        row["MaintenanceOPRs_Repaired_Count"] = list[i].MaintenanceOPRs_Repaired_Count;
                        row["MaintenanceOPRs_Warranty_Count"] = list[i].MaintenanceOPRs_Warranty_Count;
                        row["MaintenanceOPRs_EndWarranty_Count"] = list[i].MaintenanceOPRs_EndWarranty_Count;
                        row["BillMaintenances_Count"] = list[i].BillMaintenances_Count;
                        row["BillMaintenances_Value"] = list[i].BillMaintenances_Value;
                        row["BillMaintenances_Pays_Value"] = list[i].BillMaintenances_Pays_Value;
                        row["BillMaintenances_Pays_Remain"] = list[i].BillMaintenances_Pays_Remain;
                        row["BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency"] = list[i].BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;
                        row["BillMaintenances_ItemsOut_Value"] = list[i].BillMaintenances_ItemsOut_Value;
                        row["BillMaintenances_ItemsOut_RealValue"] = list[i].BillMaintenances_ItemsOut_RealValue;
                        row["BillMaintenances_RealValue"] = list[i].BillMaintenances_RealValue;
                        row["BillMaintenances_Pays_RealValue"] = list[i].BillMaintenances_Pays_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_YearRange_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
        }

        #endregion
        #region Report_Sell
        public class Report_Sells_Day_ReportDetail
        {

            public DateTime Bill_Time;
            public uint Bill_ID;
            public string SellType;
            public string Bill_Owner;
            public int ClauseS_Count;
            public double BillValue;
            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;
            public double ExchangeRate;
            public int PaysCount;
            public string PaysAmount;
            public double PaysRemain;
            public string Source_ItemsIN_Cost_Details;
            public double Source_ItemsIN_RealCost;
            public double ItemsOut_RealValue;
            public double RealPaysValue;
            public Report_Sells_Day_ReportDetail(DateTime Bill_Time_,
             uint Bill_ID_,
             string SellType_,
             string Bill_Owner_,
             int ClauseS_Count_,
             double BillValue_,
             uint CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
             double ExchangeRate_,
             int PaysCount_,
             string PaysAmount_,
             double PaysRemain_,
             string Source_ItemsIN_Cost_Details_,
             double Source_ItemsIN_RealCost_,
             double ItemsOut_RealValue_,
             double RealPaysValue_
               )
            {
                Bill_Time = Bill_Time_;
                Bill_ID = Bill_ID_;
                SellType = SellType_;
                Bill_Owner = Bill_Owner_;
                ClauseS_Count = ClauseS_Count_;
                BillValue = BillValue_;
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                ExchangeRate = ExchangeRate_;
                PaysCount = PaysCount_;
                PaysAmount = PaysAmount_;
                PaysRemain = PaysRemain_;
                Source_ItemsIN_Cost_Details = Source_ItemsIN_Cost_Details_;
                Source_ItemsIN_RealCost = Source_ItemsIN_RealCost_;
                ItemsOut_RealValue = ItemsOut_RealValue_;
                RealPaysValue = RealPaysValue_;
            }
            internal static DataTable Get_Report_Sells_Day_ReportDetail_List_AS_DataTable(List<Report_Sells_Day_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("Bill_Time", typeof(DateTime));
                    table.Columns.Add("Bill_ID", typeof(uint));
                    table.Columns.Add("SellType", typeof(string));
                    table.Columns.Add("Bill_Owner", typeof(string));
                    table.Columns.Add("ClauseS_Count", typeof(int));
                    table.Columns.Add("BillValue", typeof(double));
                    table.Columns.Add("CurrencyID", typeof(uint));
                    table.Columns.Add("CurrencyName", typeof(string));
                    table.Columns.Add("CurrencySymbol", typeof(string));
                    table.Columns.Add("ExchangeRate", typeof(double));
                    table.Columns.Add("PaysCount", typeof(int));
                    table.Columns.Add("PaysAmount", typeof(string));
                    table.Columns.Add("PaysRemain", typeof(double));
                    table.Columns.Add("Source_ItemsIN_Cost_Details", typeof(string));
                    table.Columns.Add("Source_ItemsIN_RealCost", typeof(double));
                    table.Columns.Add("ItemsOut_RealValue", typeof(double));
                    table.Columns.Add("RealPaysValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["Bill_Time"] = list[i].Bill_Time;
                        row["Bill_ID"] = list[i].Bill_ID;
                        row["SellType"] = list[i].SellType;
                        row["Bill_Owner"] = list[i].Bill_Owner;

                        row["ClauseS_Count"] = list[i].ClauseS_Count;
                        row["BillValue"] = list[i].BillValue;
                        row["CurrencyID"] = list[i].CurrencyID;
                        row["CurrencyName"] = list[i].CurrencyName;
                        row["CurrencySymbol"] = list[i].CurrencySymbol;
                        row["ExchangeRate"] = list[i].ExchangeRate;
                        row["PaysCount"] = list[i].PaysCount;
                        row["PaysAmount"] = list[i].PaysAmount;
                        row["PaysRemain"] = list[i].PaysRemain;
                        row["Source_ItemsIN_Cost_Details"] = list[i].Source_ItemsIN_Cost_Details;
                        row["Source_ItemsIN_RealCost"] = list[i].Source_ItemsIN_RealCost;
                        row["ItemsOut_RealValue"] = list[i].ItemsOut_RealValue;
                        row["RealPaysValue"] = list[i].RealPaysValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Sells_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_Sells_Month_ReportDetail
        {

            public int DayID;
            public DateTime DayDate;
            public int Bills_Count;
            public int Bills_Clause_Count;
            public string Bills_Value;
            public string Bills_Pays_Value;
            public string Bills_Pays_Remain;
            public double Bills_Pays_Remain_UPON_BillsCurrency;
            public string Bills_ItemsIN_Value;
            public double Bills_ItemsIN_RealValue;
            public double Bills_RealValue;
            public double Bills_Pays_RealValue;
            public Report_Sells_Month_ReportDetail(
                    int DayID_,
                    DateTime DayDate_,
              int Bills_Count_,
             int Bills_Clause_Count_,
             string Bills_Value_,
             string Bills_Pays_Value_,
             string Bills_Pays_Remain_,
             double Bills_Pays_Remain_UPON_BillsCurrency_,
             string Bills_ItemsIN_Value_,
             double Bills_ItemsIN_RealValue_,
             double Bills_RealValue_,
             double Bills_Pays_RealValue_)
            {
                DayID = DayID_;
                DayDate = DayDate_;
                Bills_Count = Bills_Count_;
                Bills_Clause_Count = Bills_Clause_Count_;
                Bills_Value = Bills_Value_;
                Bills_Pays_Value = Bills_Pays_Value_;
                Bills_Pays_Remain = Bills_Pays_Remain_;
                Bills_Pays_Remain_UPON_BillsCurrency = Bills_Pays_Remain_UPON_BillsCurrency_;
                Bills_ItemsIN_Value = Bills_ItemsIN_Value_;
                Bills_ItemsIN_RealValue = Bills_ItemsIN_RealValue_;
                Bills_RealValue = Bills_RealValue_;
                Bills_Pays_RealValue = Bills_Pays_RealValue_;
            }
            internal static DataTable Get_Report_Sells_Month_ReportDetail_AS_DataTable(List<Report_Sells_Month_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("DayID", typeof(int));
                    table.Columns.Add("DayDate", typeof(DateTime));
                    table.Columns.Add("Bills_Count", typeof(int));
                    table.Columns.Add("Bills_Clause_Count", typeof(int));
                    table.Columns.Add("Bills_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain_UPON_BillsCurrency", typeof(double));
                    table.Columns.Add("Bills_ItemsIN_Value", typeof(string));
                    table.Columns.Add("Bills_ItemsIN_RealValue", typeof(double));
                    table.Columns.Add("Bills_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_RealValue", typeof(double));
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["DayID"] = list[i].DayID;
                        row["DayDate"] = list[i].DayDate;
                        row["Bills_Count"] = list[i].Bills_Count;
                        row["Bills_Clause_Count"] = list[i].Bills_Clause_Count;
                        row["Bills_Value"] = list[i].Bills_Value;
                        row["Bills_Pays_Value"] = list[i].Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = list[i].Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_BillsCurrency"] = list[i].Bills_Pays_Remain_UPON_BillsCurrency;
                        row["Bills_ItemsIN_Value"] = list[i].Bills_ItemsIN_Value;
                        row["Bills_ItemsIN_RealValue"] = list[i].Bills_ItemsIN_RealValue;
                        row["Bills_RealValue"] = list[i].Bills_RealValue;
                        row["Bills_Pays_RealValue"] = list[i].Bills_Pays_RealValue;

                        table.Rows.Add(row);

                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Month_ReportDetail_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_Sells_Year_ReportDetail
        {

            public int MonthNO;
            public string MonthName;
            public int Bills_Count;
            public int Bills_Clause_Count;
            public string Bills_Value;
            public string Bills_Pays_Value;
            public string Bills_Pays_Remain;
            public double Bills_Pays_Remain_UPON_BillsCurrency;
            public string Bills_ItemsIN_Value;
            public double Bills_ItemsIN_RealValue;
            public double Bills_RealValue;
            public double Bills_Pays_RealValue;
            public Report_Sells_Year_ReportDetail(
                    int MOnthNO_,
                    string MonthName_,
              int Bills_Count_,
             int Bills_Clause_Count_,
             string Bills_Value_,
             string Bills_Pays_Value_,
             string Bills_Pays_Remain_,
             double Bills_Pays_Remain_UPON_BillsCurrency_,
             string Bills_ItemsIN_Value_,
             double Bills_ItemsIN_RealValue_,
             double Bills_RealValue_,
             double Bills_Pays_RealValue_)
            {
                MonthNO = MOnthNO_;
                MonthName = MonthName_;
                Bills_Count = Bills_Count_;
                Bills_Clause_Count = Bills_Clause_Count_;
                Bills_Value = Bills_Value_;
                Bills_Pays_Value = Bills_Pays_Value_;
                Bills_Pays_Remain = Bills_Pays_Remain_;
                Bills_Pays_Remain_UPON_BillsCurrency = Bills_Pays_Remain_UPON_BillsCurrency_;
                Bills_ItemsIN_Value = Bills_ItemsIN_Value_;
                Bills_ItemsIN_RealValue = Bills_ItemsIN_RealValue_;
                Bills_RealValue = Bills_RealValue_;
                Bills_Pays_RealValue = Bills_Pays_RealValue_;
            }
            internal static DataTable Get_Report_Sells_Year_ReportDetail_AS_DataTable(List<Report_Sells_Year_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("MonthNO", typeof(int));
                    table.Columns.Add("MonthName", typeof(string));
                    table.Columns.Add("Bills_Count", typeof(int));
                    table.Columns.Add("Bills_Clause_Count", typeof(int));
                    table.Columns.Add("Bills_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain_UPON_BillsCurrency", typeof(double));
                    table.Columns.Add("Bills_ItemsIN_Value", typeof(string));
                    table.Columns.Add("Bills_ItemsIN_RealValue", typeof(double));
                    table.Columns.Add("Bills_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_RealValue", typeof(double));
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["MonthNO"] = list[i].MonthNO;
                        row["MonthName"] = list[i].MonthName;
                        row["Bills_Count"] = list[i].Bills_Count;
                        row["Bills_Clause_Count"] = list[i].Bills_Clause_Count;
                        row["Bills_Value"] = list[i].Bills_Value;
                        row["Bills_Pays_Value"] = list[i].Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = list[i].Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_BillsCurrency"] = list[i].Bills_Pays_Remain_UPON_BillsCurrency;
                        row["Bills_ItemsIN_Value"] = list[i].Bills_ItemsIN_Value;
                        row["Bills_ItemsIN_RealValue"] = list[i].Bills_ItemsIN_RealValue;
                        row["Bills_RealValue"] = list[i].Bills_RealValue;
                        row["Bills_Pays_RealValue"] = list[i].Bills_Pays_RealValue;

                        table.Rows.Add(row);

                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Year_ReportDetail_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_Sells_YearRange_ReportDetail
        {

            public int YearNO;
            public int Bills_Count;
            public int Bills_Clause_Count;
            public string Bills_Value;
            public string Bills_Pays_Value;
            public string Bills_Pays_Remain;
            public double Bills_Pays_Remain_UPON_BillsCurrency;
            public string Bills_ItemsIN_Value;
            public double Bills_ItemsIN_RealValue;
            public double Bills_RealValue;
            public double Bills_Pays_RealValue;
            public Report_Sells_YearRange_ReportDetail(
                    int YearNO_,
              int Bills_Count_,
             int Bills_Clause_Count_,
             string Bills_Value_,
             string Bills_Pays_Value_,
             string Bills_Pays_Remain_,
             double Bills_Pays_Remain_UPON_BillsCurrency_,
             string Bills_ItemsIN_Value_,
             double Bills_ItemsIN_RealValue_,
             double Bills_RealValue_,
             double Bills_Pays_RealValue_)
            {
                YearNO = YearNO_;

                Bills_Count = Bills_Count_;
                Bills_Clause_Count = Bills_Clause_Count_;
                Bills_Value = Bills_Value_;
                Bills_Pays_Value = Bills_Pays_Value_;
                Bills_Pays_Remain = Bills_Pays_Remain_;
                Bills_Pays_Remain_UPON_BillsCurrency = Bills_Pays_Remain_UPON_BillsCurrency_;
                Bills_ItemsIN_Value = Bills_ItemsIN_Value_;
                Bills_ItemsIN_RealValue = Bills_ItemsIN_RealValue_;
                Bills_RealValue = Bills_RealValue_;
                Bills_Pays_RealValue = Bills_Pays_RealValue_;
            }
            internal static DataTable Get_Report_Sells_YearRange_ReportDetail_AS_DataTable(List<Report_Sells_YearRange_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("YearNO", typeof(int));
                    table.Columns.Add("Bills_Count", typeof(int));
                    table.Columns.Add("Bills_Clause_Count", typeof(int));
                    table.Columns.Add("Bills_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Value", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain", typeof(string));
                    table.Columns.Add("Bills_Pays_Remain_UPON_BillsCurrency", typeof(double));
                    table.Columns.Add("Bills_ItemsIN_Value", typeof(string));
                    table.Columns.Add("Bills_ItemsIN_RealValue", typeof(double));
                    table.Columns.Add("Bills_RealValue", typeof(double));
                    table.Columns.Add("Bills_Pays_RealValue", typeof(double));
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["YearNO"] = list[i].YearNO;
                        row["Bills_Count"] = list[i].Bills_Count;
                        row["Bills_Clause_Count"] = list[i].Bills_Clause_Count;
                        row["Bills_Value"] = list[i].Bills_Value;
                        row["Bills_Pays_Value"] = list[i].Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = list[i].Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_BillsCurrency"] = list[i].Bills_Pays_Remain_UPON_BillsCurrency;
                        row["Bills_ItemsIN_Value"] = list[i].Bills_ItemsIN_Value;
                        row["Bills_ItemsIN_RealValue"] = list[i].Bills_ItemsIN_RealValue;
                        row["Bills_RealValue"] = list[i].Bills_RealValue;
                        row["Bills_Pays_RealValue"] = list[i].Bills_Pays_RealValue;

                        table.Rows.Add(row);

                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_YearRange_ReportDetail_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class OperationCurrencyReport
        {
            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;
            public int OperationsCount;
            public double OperationsValue;
            public string Operations_PaysAmount;
            public double Operations_RemainValue;

            public OperationCurrencyReport(
          uint CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
                int OperationsCount_,
             double OperationsValue_,
             string Operations_PaysAmount_,
             double Operations_RemainValue_)
            {
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                OperationsCount = OperationsCount_;
                OperationsValue = OperationsValue_;
                Operations_PaysAmount = Operations_PaysAmount_;
                Operations_RemainValue = Operations_RemainValue_;
            }
        }
        #endregion
        #region Report_PayOrder
        public class Report_PayOrders_Day_ReportDetail
        {

            public const bool TYPE_SALARY_PAY_ODER = false;
            public const bool TYPE_PAY_ODER = true;

            public DateTime PayOrder_Time;
            public bool PayOrderType;
            public uint PayOrderID;

            public string PayOrderDesc;
            public uint EmployeeID;
            public string EmployeeName;
            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;

            public double ExchangeRate;
            public double Value;
            public string PaysAmount;
            public double PaysRemain;
            public double RealValue;
            public double RealPays;
            public Report_PayOrders_Day_ReportDetail(
                 DateTime PayOrder_Time_,
                bool PayOrderType_,
                uint PayOrderID_,

             string PayOrderDesc_,
             uint EmployeeID_,
             string EmployeeName_,
             double Value_,
             uint CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
            double ExchangeRate_,

             string PaysAmount_,
             double PaysRemain_,
             double RealValue_,
             double RealPays_)
            {
                PayOrder_Time = PayOrder_Time_;
                PayOrderType = PayOrderType_;
                PayOrderID = PayOrderID_;
                PayOrderDesc = PayOrderDesc_;
                EmployeeID = EmployeeID_;
                EmployeeName = EmployeeName_;
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                ExchangeRate = ExchangeRate_;
                Value = Value_;
                PaysAmount = PaysAmount_;
                PaysRemain = PaysRemain_;
                RealValue = RealValue_;
                RealPays = RealPays_;
            }

            internal static DataTable Get_Report_PayOrders_Day_ReportDetail_List_AS_DataTable(List<Report_PayOrders_Day_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("PayOrder_Time", typeof(DateTime));
                    table.Columns.Add("PayOrderType", typeof(bool));
                    table.Columns.Add("PayOrderID", typeof(uint));
                    table.Columns.Add("PayOrderDesc", typeof(string));
                    table.Columns.Add("EmployeeID", typeof(uint));
                    table.Columns.Add("EmployeeName", typeof(string));
                    table.Columns.Add("CurrencyID", typeof(uint));
                    table.Columns.Add("CurrencyName", typeof(string));
                    table.Columns.Add("CurrencySymbol", typeof(string));
                    table.Columns.Add("ExchangeRate", typeof(double));
                    table.Columns.Add("Value", typeof(double));
                    table.Columns.Add("PaysAmount", typeof(string));
                    table.Columns.Add("PaysRemain", typeof(double));
                    table.Columns.Add("RealValue", typeof(double));
                    table.Columns.Add("RealPays", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["PayOrder_Time"] = list[i].PayOrder_Time;
                        row["PayOrderType"] = list[i].PayOrderType;
                        row["PayOrderID"] = list[i].PayOrderID;
                        row["PayOrderDesc"] = list[i].PayOrderDesc;

                        row["EmployeeID"] = list[i].EmployeeID;
                        row["EmployeeName"] = list[i].EmployeeName;
                        row["CurrencyID"] = list[i].CurrencyID;
                        row["CurrencyName"] = list[i].CurrencyName;
                        row["CurrencySymbol"] = list[i].CurrencySymbol;
                        row["ExchangeRate"] = list[i].ExchangeRate;
                        row["Value"] = list[i].Value;
                        row["PaysAmount"] = list[i].PaysAmount;
                        row["PaysRemain"] = list[i].PaysRemain;
                        row["RealValue"] = list[i].RealValue;
                        row["RealPays"] = list[i].RealPays;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Day_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Report_PayOrders_Month_ReportDetail
        {

            public int DayID;
            public DateTime DayDate;
            public int Salary_PayOrders_Count;
            public int Other_PayOrders_Count;
            public string PayOrders_Value;
            public string PayOrders_Pays_Value;
            public string PayOrders_Pays_Remain;

            public double PayOrders_Pays_Remain_UPON_PayOrdersCurrency;
            public double PayOrders_RealValue;
            public double PayOrders_Pays_RealValue;


            public Report_PayOrders_Month_ReportDetail(
                    int DayID_,
                    DateTime DayDate_,
                int Salary_PayOrders_Count_,
             int Other_PayOrders_Count_,
             string PayOrders_Value_,
             string PayOrders_Pays_Value_,
             string PayOrders_Pays_Remain_,

             double PayOrders_Pays_Remain_UPON_PayOrdersCurrency_,
             double PayOrders_RealValue_,
             double PayOrders_Pays_RealValue_)
            {
                DayID = DayID_;
                DayDate = DayDate_;
                Salary_PayOrders_Count = Salary_PayOrders_Count_;
                Other_PayOrders_Count = Other_PayOrders_Count_;
                PayOrders_Value = PayOrders_Value_;
                PayOrders_Pays_Value = PayOrders_Pays_Value_;
                PayOrders_Pays_Remain = PayOrders_Pays_Remain_;

                PayOrders_Pays_Remain_UPON_PayOrdersCurrency = PayOrders_Pays_Remain_UPON_PayOrdersCurrency_;
                PayOrders_RealValue = PayOrders_RealValue_;
                PayOrders_Pays_RealValue = PayOrders_Pays_RealValue_;
            }
            internal static DataTable Get_Report_PayOrders_Month_ReportDetail_List_AS_DataTable(List<Report_PayOrders_Month_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("DayID", typeof(int));
                    table.Columns.Add("DayDate", typeof(DateTime));
                    table.Columns.Add("Salary_PayOrders_Count", typeof(int));
                    table.Columns.Add("Other_PayOrders_Count", typeof(int));
                    table.Columns.Add("PayOrders_Value", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Value", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Remain", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Remain_UPON_PayOrdersCurrency", typeof(double));
                    table.Columns.Add("PayOrders_RealValue", typeof(double));
                    table.Columns.Add("PayOrders_Pays_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["DayID"] = list[i].DayID;
                        row["DayDate"] = list[i].DayDate;
                        row["Salary_PayOrders_Count"] = list[i].Salary_PayOrders_Count;
                        row["Other_PayOrders_Count"] = list[i].Other_PayOrders_Count;

                        row["PayOrders_Value"] = list[i].PayOrders_Value;
                        row["PayOrders_Pays_Value"] = list[i].PayOrders_Pays_Value;
                        row["PayOrders_Pays_Remain"] = list[i].PayOrders_Pays_Remain;
                        row["PayOrders_Pays_Remain_UPON_PayOrdersCurrency"] = list[i].PayOrders_Pays_Remain_UPON_PayOrdersCurrency;
                        row["PayOrders_RealValue"] = list[i].PayOrders_RealValue;
                        row["PayOrders_Pays_RealValue"] = list[i].PayOrders_Pays_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Month_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
        }
        public class Report_PayOrders_Year_ReportDetail
        {

            public int MonthNO;
            public string MonthName;
            public int Salary_PayOrders_Count;
            public int Other_PayOrders_Count;
            public string PayOrders_Value;
            public string PayOrders_Pays_Value;
            public string PayOrders_Pays_Remain;

            public double PayOrders_Pays_Remain_UPON_PayOrdersCurrency;
            public double PayOrders_RealValue;
            public double PayOrders_Pays_RealValue;

            public Report_PayOrders_Year_ReportDetail(
                    int MOnthNO_,
                    string MonthName_,
              int Salary_PayOrders_Count_,
             int Other_PayOrders_Count_,
             string PayOrders_Value_,
             string PayOrders_Pays_Value_,
             string PayOrders_Pays_Remain_,

             double PayOrders_Pays_Remain_UPON_PayOrdersCurrency_,
             double PayOrders_RealValue_,
             double PayOrders_Pays_RealValue_)
            {
                MonthNO = MOnthNO_;
                MonthName = MonthName_;
                Salary_PayOrders_Count = Salary_PayOrders_Count_;
                Other_PayOrders_Count = Other_PayOrders_Count_;
                PayOrders_Value = PayOrders_Value_;
                PayOrders_Pays_Value = PayOrders_Pays_Value_;
                PayOrders_Pays_Remain = PayOrders_Pays_Remain_;

                PayOrders_Pays_Remain_UPON_PayOrdersCurrency = PayOrders_Pays_Remain_UPON_PayOrdersCurrency_;
                PayOrders_RealValue = PayOrders_RealValue_;
                PayOrders_Pays_RealValue = PayOrders_Pays_RealValue_;
            }
            internal static DataTable Get_Report_PayOrders_Year_ReportDetail_List_AS_DataTable(List<Report_PayOrders_Year_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("MonthNO", typeof(int));
                    table.Columns.Add("MonthName", typeof(string));
                    table.Columns.Add("Salary_PayOrders_Count", typeof(int));
                    table.Columns.Add("Other_PayOrders_Count", typeof(int));
                    table.Columns.Add("PayOrders_Value", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Value", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Remain", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Remain_UPON_PayOrdersCurrency", typeof(double));
                    table.Columns.Add("PayOrders_RealValue", typeof(double));
                    table.Columns.Add("PayOrders_Pays_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["MonthNO"] = list[i].MonthNO;
                        row["MonthName"] = list[i].MonthName;
                        row["Salary_PayOrders_Count"] = list[i].Salary_PayOrders_Count;
                        row["Other_PayOrders_Count"] = list[i].Other_PayOrders_Count;

                        row["PayOrders_Value"] = list[i].PayOrders_Value;
                        row["PayOrders_Pays_Value"] = list[i].PayOrders_Pays_Value;
                        row["PayOrders_Pays_Remain"] = list[i].PayOrders_Pays_Remain;
                        row["PayOrders_Pays_Remain_UPON_PayOrdersCurrency"] = list[i].PayOrders_Pays_Remain_UPON_PayOrdersCurrency;
                        row["PayOrders_RealValue"] = list[i].PayOrders_RealValue;
                        row["PayOrders_Pays_RealValue"] = list[i].PayOrders_Pays_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Year_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
        }
        public class Report_PayOrders_YearRange_ReportDetail
        {

            public int YearNO;
            public int Salary_PayOrders_Count;
            public int Other_PayOrders_Count;
            public string PayOrders_Value;
            public string PayOrders_Pays_Value;
            public string PayOrders_Pays_Remain;

            public double PayOrders_Pays_Remain_UPON_PayOrdersCurrency;
            public double PayOrders_RealValue;
            public double PayOrders_Pays_RealValue;
            public Report_PayOrders_YearRange_ReportDetail(
                    int YearNO_,
            int Salary_PayOrders_Count_,
             int Other_PayOrders_Count_,
             string PayOrders_Value_,
             string PayOrders_Pays_Value_,
             string PayOrders_Pays_Remain_,

             double PayOrders_Pays_Remain_UPON_PayOrdersCurrency_,
             double PayOrders_RealValue_,
             double PayOrders_Pays_RealValue_)
            {
                YearNO = YearNO_;

                Salary_PayOrders_Count = Salary_PayOrders_Count_;
                Other_PayOrders_Count = Other_PayOrders_Count_;
                PayOrders_Value = PayOrders_Value_;
                PayOrders_Pays_Value = PayOrders_Pays_Value_;
                PayOrders_Pays_Remain = PayOrders_Pays_Remain_;

                PayOrders_Pays_Remain_UPON_PayOrdersCurrency = PayOrders_Pays_Remain_UPON_PayOrdersCurrency_;
                PayOrders_RealValue = PayOrders_RealValue_;
                PayOrders_Pays_RealValue = PayOrders_Pays_RealValue_;
            }
            internal static DataTable Get_Report_PayOrders_YearRange_ReportDetail_List_AS_DataTable(List<Report_PayOrders_YearRange_ReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("YearNO", typeof(int));
                    table.Columns.Add("Salary_PayOrders_Count", typeof(int));
                    table.Columns.Add("Other_PayOrders_Count", typeof(int));
                    table.Columns.Add("PayOrders_Value", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Value", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Remain", typeof(string));
                    table.Columns.Add("PayOrders_Pays_Remain_UPON_PayOrdersCurrency", typeof(double));
                    table.Columns.Add("PayOrders_RealValue", typeof(double));
                    table.Columns.Add("PayOrders_Pays_RealValue", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["YearNO"] = list[i].YearNO;
                        row["Salary_PayOrders_Count"] = list[i].Salary_PayOrders_Count;
                        row["Other_PayOrders_Count"] = list[i].Other_PayOrders_Count;

                        row["PayOrders_Value"] = list[i].PayOrders_Value;
                        row["PayOrders_Pays_Value"] = list[i].PayOrders_Pays_Value;
                        row["PayOrders_Pays_Remain"] = list[i].PayOrders_Pays_Remain;
                        row["PayOrders_Pays_Remain_UPON_PayOrdersCurrency"] = list[i].PayOrders_Pays_Remain_UPON_PayOrdersCurrency;
                        row["PayOrders_RealValue"] = list[i].PayOrders_RealValue;
                        row["PayOrders_Pays_RealValue"] = list[i].PayOrders_Pays_RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_YearRange_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
        }
        #endregion

        #region AccountMoney
        public class PayCurrencyReport
        {

            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;

            public double PaysIN_Sell;
            public double PaysIN_Maintenance;
            public double PaysIN_MoneyTransform;
            public double PaysIN_NON;
            public double PaysIN_Exchange;
            public double PaysOUT_Buy;
            public double PaysOUT_Emp;
            public double PaysOUT_MoneyTransform;
            public double PaysOUT_NON;
            public double PaysOUT_Exchange;

            public PayCurrencyReport(

             uint CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
             double PaysIN_Sell_,
             double PaysIN_Maintenance_,
             double PaysIN_MoneyTransform_,
            double PaysIN_NON_,
             double PaysIN_Exchange_,
             double PaysOUT_Buy_,
             double PaysOUT_Emp_,
             double PaysOUT_MoneyTransform_,
            double PaysOUT_NON_,
             double PaysOUT_Exchange_)
            {
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                PaysIN_Sell = PaysIN_Sell_;
                PaysIN_Maintenance = PaysIN_Maintenance_;
                PaysIN_MoneyTransform = PaysIN_MoneyTransform_;
                PaysIN_NON = PaysIN_NON_;
                PaysIN_Exchange = PaysIN_Exchange_;
                PaysOUT_Buy = PaysOUT_Buy_;
                PaysOUT_Emp = PaysOUT_Emp_;
                PaysOUT_MoneyTransform = PaysOUT_MoneyTransform_;
                PaysOUT_NON = PaysOUT_NON_;
                PaysOUT_Exchange = PaysOUT_Exchange_;
            }
            internal static DataTable Get_PayCurrencyReport_List_AS_DataTable(List<PayCurrencyReport> list)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("CurrencyID", typeof(uint));
                    table.Columns.Add("CurrencyName", typeof(string));
                    table.Columns.Add("CurrencySymbol", typeof(string));
                    table.Columns.Add("PaysIN_Sell", typeof(double));
                    table.Columns.Add("PaysIN_Maintenance", typeof(double));
                    table.Columns.Add("PaysIN_MoneyTransform", typeof(double));
                    table.Columns.Add("PaysIN_NON", typeof(double));
                    table.Columns.Add("PaysIN_Exchange", typeof(double));
                    table.Columns.Add("PaysOUT_Buy", typeof(double));
                    table.Columns.Add("PaysOUT_Emp", typeof(double));
                    table.Columns.Add("PaysOUT_MoneyTransform", typeof(double));
                    table.Columns.Add("PaysOUT_NON", typeof(double));
                    table.Columns.Add("PaysOUT_Exchange", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["CurrencyID"] = list[i].CurrencyID;
                        row["CurrencyName"] = list[i].CurrencyName;
                        row["CurrencySymbol"] = list[i].CurrencySymbol;
                        row["PaysIN_Sell"] = list[i].PaysIN_Sell;
                        row["PaysIN_Maintenance"] = list[i].PaysIN_Maintenance;
                        row["PaysIN_MoneyTransform"] = list[i].PaysIN_MoneyTransform;
                        row["PaysIN_NON"] = list[i].PaysIN_NON;
                        row["PaysIN_Exchange"] = list[i].PaysIN_Exchange;
                        row["PaysOUT_Buy"] = list[i].PaysOUT_Buy;
                        row["PaysOUT_Emp"] = list[i].PaysOUT_Emp;
                        row["PaysOUT_MoneyTransform"] = list[i].PaysOUT_MoneyTransform;

                        row["PaysOUT_NON"] = list[i].PaysOUT_NON;
                        row["PaysOUT_Exchange"] = list[i].PaysOUT_Exchange;
                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_PayCurrencyReport_List_AS_DataTable:" + ee.Message);
                }
            }


        }
        public class AccountOprReportDetail
        {
            public const bool DIRECTION_IN = false;
            public const bool DIRECTION_OUT = true;

            public const uint TYPE_PAY_OPR = 0;
            public const uint TYPE_EXCHANGE_OPR = 1;
            public const uint TYPE_MoneyTransform_OPR = 2;

            public DateTime OprTime;
            public uint OprType;
            public bool OprDirection;
            public uint OprID;
            public string OprOwner;

            public double Value;
            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;
            public double ExchangeRate;
            public double RealValue;
            public AccountOprReportDetail(
             DateTime OprTime_,
             uint OprType_,
             bool OprDirection_,
             uint OprID_,
             string OprOwner_,
             double Value_,
              uint CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
            double ExchangeRate_,
             double RealValue_
             )
            {

                OprTime = OprTime_;
                OprType = OprType_;
                OprID = OprID_;
                OprOwner = OprOwner_;
                OprDirection = OprDirection_;
                Value = Value_;
                CurrencyID = CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
                ExchangeRate = ExchangeRate_;
                RealValue = RealValue_;
            }
            internal static DataTable Get_AccountOprReportDetail_List_AS_DataTable(List<AccountOprReportDetail> list)
            {

                try
                {


                    DataTable table = new DataTable();
                    table.Columns.Add("OprTime", typeof(DateTime));
                    table.Columns.Add("OprType", typeof(uint));
                    table.Columns.Add("OprDirection", typeof(bool));
                    table.Columns.Add("OprID", typeof(uint));
                    table.Columns.Add("OprOwner", typeof(string));
                    table.Columns.Add("Value", typeof(double));
                    table.Columns.Add("CurrencyID", typeof(uint));
                    table.Columns.Add("CurrencyName", typeof(string));
                    table.Columns.Add("CurrencySymbol", typeof(string));
                    table.Columns.Add("ExchangeRate", typeof(double));
                    table.Columns.Add("RealValue", typeof(double));


                    ;
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["OprTime"] = list[i].OprTime;
                        row["OprType"] = list[i].OprType;
                        row["OprDirection"] = list[i].OprDirection;
                        row["OprID"] = list[i].OprID;
                        row["OprOwner"] = list[i].OprOwner;
                        row["Value"] = list[i].Value;
                        row["CurrencyID"] = list[i].CurrencyID;
                        row["CurrencyName"] = list[i].CurrencyName;
                        row["CurrencySymbol"] = list[i].CurrencySymbol;
                        row["ExchangeRate"] = list[i].ExchangeRate;
                        row["RealValue"] = list[i].RealValue;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }


        }
        public class AccountOprDayReportDetail
        {
            public int DateDayNo;
            public DateTime Date_day;
            public int PaysIN_Count;
            public int PaysOUT_Count;
            public int Exchange_Count;
            public int MoneyTransform_IN_Count;
            public int MoneyTransform_OUT_Count;
            public string PaysIN_Value;
            public double PaysIN_Real_Value;
            public string PaysOUT_Value;
            public double PaysOUT_Real_Value;
            public AccountOprDayReportDetail(
                           int DateDayNo_,
             DateTime Date_day_,
             int PaysIN_Count_,
             int PaysOUT_Count_,
             int Exchange_Count_,
             int MoneyTransform_IN_Count_,
            int MoneyTransform_OUT_Count_,
            string PaysIN_Value_,
             double PaysIN_Real_Value_,
             string PaysOUT_Value_,
             double PaysOUT_Real_Value_)
            {
                DateDayNo = DateDayNo_;
                Date_day = Date_day_;
                PaysIN_Count = PaysIN_Count_;
                PaysOUT_Count = PaysOUT_Count_;
                Exchange_Count = Exchange_Count_;
                MoneyTransform_IN_Count = MoneyTransform_IN_Count_;
                MoneyTransform_OUT_Count = MoneyTransform_OUT_Count_;
                PaysIN_Value = PaysIN_Value_;
                PaysIN_Real_Value = PaysIN_Real_Value_;
                PaysOUT_Value = PaysOUT_Value_;
                PaysOUT_Real_Value = PaysOUT_Real_Value_;
            }


            internal static DataTable Get_AccountOprDayReportDetail_List_AS_DataTable(List<AccountOprDayReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("DateDayNo", typeof(int));
                    table.Columns.Add("Date_day", typeof(DateTime));
                    table.Columns.Add("PaysIN_Count", typeof(int));
                    table.Columns.Add("PaysOUT_Count", typeof(int));
                    table.Columns.Add("Exchange_Count", typeof(int));
                    table.Columns.Add("MoneyTransform_IN_Count", typeof(int));
                    table.Columns.Add("MoneyTransform_OUT_Count", typeof(int));
                    table.Columns.Add("PaysIN_Value", typeof(string));
                    table.Columns.Add("PaysIN_Real_Value", typeof(double));
                    table.Columns.Add("PaysOUT_Value", typeof(string));
                    table.Columns.Add("PaysOUT_Real_Value", typeof(double));


                    ;
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["DateDayNo"] = list[i].DateDayNo;
                        row["Date_day"] = list[i].Date_day;
                        row["PaysIN_Count"] = list[i].PaysIN_Count;
                        row["PaysOUT_Count"] = list[i].PaysOUT_Count;
                        row["Exchange_Count"] = list[i].Exchange_Count;
                        row["MoneyTransform_IN_Count"] = list[i].MoneyTransform_IN_Count;
                        row["MoneyTransform_OUT_Count"] = list[i].MoneyTransform_OUT_Count;
                        row["PaysIN_Value"] = list[i].PaysIN_Value;
                        row["PaysIN_Real_Value"] = list[i].PaysIN_Real_Value;
                        row["PaysOUT_Value"] = list[i].PaysOUT_Value;
                        row["PaysOUT_Real_Value"] = list[i].PaysOUT_Real_Value;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprDayReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
        }
        public class AccountOprMonthReportDetail
        {
            public int Year_Month;
            public string Year_Month_Name;
            public int PaysIN_Count;
            public int PaysOUT_Count;
            public int Exchange_Count;
            public int MoneyTransform_IN_Count;
            public int MoneyTransform_OUT_Count;
            public string PaysIN_Value;
            public double PaysIN_Real_Value;
            public string PaysOUT_Value;
            public double PaysOUT_Real_Value;
            public AccountOprMonthReportDetail(
                int Year_Month_,
             string Year_Month_Name_,
            int PaysIN_Count_,
             int PaysOUT_Count_,
             int Exchange_Count_,
                 int MoneyTransform_IN_Count_,
             int MoneyTransform_OUT_Count_,
            string PaysIN_Value_,
             double PaysIN_Real_Value_,
             string PaysOUT_Value_,
             double PaysOUT_Real_Value_)
            {
                Year_Month = Year_Month_;
                Year_Month_Name = Year_Month_Name_;
                PaysIN_Count = PaysIN_Count_;
                PaysOUT_Count = PaysOUT_Count_;
                Exchange_Count = Exchange_Count_;
                MoneyTransform_IN_Count = MoneyTransform_IN_Count_;
                MoneyTransform_OUT_Count = MoneyTransform_OUT_Count_;
                PaysIN_Value = PaysIN_Value_;
                PaysIN_Real_Value = PaysIN_Real_Value_;
                PaysOUT_Value = PaysOUT_Value_;
                PaysOUT_Real_Value = PaysOUT_Real_Value_;
            }
            internal static DataTable Get_AccountOprMonthReportDetail_List_AS_DataTable(List<AccountOprMonthReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("Year_Month", typeof(int));
                    table.Columns.Add("Year_Month_Name", typeof(string));
                    table.Columns.Add("PaysIN_Count", typeof(int));
                    table.Columns.Add("PaysOUT_Count", typeof(int));
                    table.Columns.Add("Exchange_Count", typeof(int));
                    table.Columns.Add("MoneyTransform_IN_Count", typeof(int));
                    table.Columns.Add("MoneyTransform_OUT_Count", typeof(int));
                    table.Columns.Add("PaysIN_Value", typeof(string));
                    table.Columns.Add("PaysIN_Real_Value", typeof(double));
                    table.Columns.Add("PaysOUT_Value", typeof(string));
                    table.Columns.Add("PaysOUT_Real_Value", typeof(double));


                    ;
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["Year_Month"] = list[i].Year_Month;
                        row["Year_Month_Name"] = list[i].Year_Month_Name;
                        row["PaysIN_Count"] = list[i].PaysIN_Count;
                        row["PaysOUT_Count"] = list[i].PaysOUT_Count;
                        row["Exchange_Count"] = list[i].Exchange_Count;
                        row["MoneyTransform_IN_Count"] = list[i].MoneyTransform_IN_Count;
                        row["MoneyTransform_OUT_Count"] = list[i].MoneyTransform_OUT_Count;
                        row["PaysIN_Value"] = list[i].PaysIN_Value;
                        row["PaysIN_Real_Value"] = list[i].PaysIN_Real_Value;
                        row["PaysOUT_Value"] = list[i].PaysOUT_Value;
                        row["PaysOUT_Real_Value"] = list[i].PaysOUT_Real_Value;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprMonthReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }



        }
        public class AccountOprYearReportDetail
        {

            public int AccountYear;
            public int PaysIN_Count;
            public int PaysOUT_Count;
            public int Exchange_Count;
            public int MoneyTransform_IN_Count;
            public int MoneyTransform_OUT_Count;
            public string PaysIN_Value;
            public double PaysIN_Real_Value;
            public string PaysOUT_Value;
            public double PaysOUT_Real_Value;
            public AccountOprYearReportDetail(
                int AccountYear_,
             int PaysIN_Count_,
             int PaysOUT_Count_,
             int Exchange_Count_,
              int MoneyTransform_IN_Count_,
             int MoneyTransform_OUT_Count_,
            string PaysIN_Value_,
             double PaysIN_Real_Value_,
             string PaysOUT_Value_,
             double PaysOUT_Real_Value_)
            {
                AccountYear = AccountYear_;
                PaysIN_Count = PaysIN_Count_;
                PaysOUT_Count = PaysOUT_Count_;
                Exchange_Count = Exchange_Count_;
                MoneyTransform_IN_Count = MoneyTransform_IN_Count_;
                MoneyTransform_OUT_Count = MoneyTransform_OUT_Count_;
                PaysIN_Value = PaysIN_Value_;
                PaysIN_Real_Value = PaysIN_Real_Value_;
                PaysOUT_Value = PaysOUT_Value_;
                PaysOUT_Real_Value = PaysOUT_Real_Value_;
            }
            internal static DataTable Get_AccountOprYearReportDetail_List_AS_DataTable(List<AccountOprYearReportDetail> list)
            {

                try
                {

                    DataTable table = new DataTable();
                    table.Columns.Add("AccountYear", typeof(int));
                    table.Columns.Add("PaysIN_Count", typeof(int));
                    table.Columns.Add("PaysOUT_Count", typeof(int));
                    table.Columns.Add("Exchange_Count", typeof(int));
                    table.Columns.Add("MoneyTransform_IN_Count", typeof(int));
                    table.Columns.Add("MoneyTransform_OUT_Count", typeof(int));
                    table.Columns.Add("PaysIN_Value", typeof(string));
                    table.Columns.Add("PaysIN_Real_Value", typeof(double));
                    table.Columns.Add("PaysOUT_Value", typeof(string));
                    table.Columns.Add("PaysOUT_Real_Value", typeof(double));


                    ;
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["AccountYear"] = list[i].AccountYear;
                        row["PaysIN_Count"] = list[i].PaysIN_Count;
                        row["PaysOUT_Count"] = list[i].PaysOUT_Count;
                        row["Exchange_Count"] = list[i].Exchange_Count;
                        row["MoneyTransform_IN_Count"] = list[i].MoneyTransform_IN_Count;
                        row["MoneyTransform_OUT_Count"] = list[i].MoneyTransform_OUT_Count;
                        row["PaysIN_Value"] = list[i].PaysIN_Value;
                        row["PaysIN_Real_Value"] = list[i].PaysIN_Real_Value;
                        row["PaysOUT_Value"] = list[i].PaysOUT_Value;
                        row["PaysOUT_Real_Value"] = list[i].PaysOUT_Real_Value;

                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprYearReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
        }
            #endregion
            #region ContactReport
            public class Contact_BillCurrencyReport
            {
                public uint CurrencyID;
                public string CurrencyName;
                public string CurrencySymbol;
                public int BillINCount;
                public double BillINValue;
                public double BillIN_PaysValue;
                public int BillMaintenanceCount;
                public double BillMaintenanceValue;
                public double BillMaintenance_PaysValue;
                public int BillOUTCount;
                public double BillOUTValue;
                public double BillOUT_PaysValue;
                public Contact_BillCurrencyReport(
                       uint CurrencyID_,
                 string CurrencyName_,
                 string CurrencySymbol_,
                int BillINCount_,
                 double BillINValue_,
                 double BillIN_PaysValue_,
                 int BillMaintenanceCount_,
                 double BillMaintenanceValue_,
                 double BillMaintenance_PaysValue_,
                 int BillOUTCount_,
                 double BillOUTValue_,
                 double BillOUT_PaysValue_)
                {
                    CurrencyID = CurrencyID_;
                    CurrencyName = CurrencyName_;
                    CurrencySymbol = CurrencySymbol_;

                    BillINCount = BillINCount_;
                    BillINValue = BillINValue_;
                    BillIN_PaysValue = BillIN_PaysValue_;
                    BillMaintenanceCount = BillMaintenanceCount_;
                    BillMaintenanceValue = BillMaintenanceValue_;
                    BillMaintenance_PaysValue = BillMaintenance_PaysValue_;
                    BillOUTCount = BillOUTCount_; ;
                    BillOUTValue = BillOUTValue_;
                    BillOUT_PaysValue = BillOUT_PaysValue_;
                }
                internal static DataTable Get_Contact_BillCurrencyReport_AS_DataTable(List<Contact_BillCurrencyReport> list)
                {

                    try
                    {

                        DataTable table = new DataTable();
                        table.Columns.Add("CurrencyID", typeof(uint));
                        table.Columns.Add("CurrencyName", typeof(string));
                        table.Columns.Add("CurrencySymbol", typeof(string));
                        table.Columns.Add("BillINCount", typeof(int));
                        table.Columns.Add("BillINValue", typeof(double));
                        table.Columns.Add("BillIN_PaysValue", typeof(double));
                        table.Columns.Add("BillMaintenanceCount", typeof(int));
                        table.Columns.Add("BillMaintenanceValue", typeof(double));
                        table.Columns.Add("BillMaintenance_PaysValue", typeof(double));
                        table.Columns.Add("BillOUTCount", typeof(int));
                        table.Columns.Add("BillOUTValue", typeof(double));
                        table.Columns.Add("BillOUT_PaysValue", typeof(double));

                        ;
                        for (int i = 0; i < list.Count; i++)
                        {
                            DataRow row = table.NewRow();
                            row["CurrencyID"] = list[i].CurrencyID;
                            row["CurrencyName"] = list[i].CurrencyName;
                            row["CurrencySymbol"] = list[i].CurrencySymbol;
                            row["BillINCount"] = list[i].BillINCount;
                            row["BillINValue"] = list[i].BillINValue;
                            row["BillIN_PaysValue"] = list[i].BillIN_PaysValue;
                            row["BillMaintenanceCount"] = list[i].BillMaintenanceCount;
                            row["BillMaintenanceValue"] = list[i].BillMaintenanceValue;
                            row["BillMaintenance_PaysValue"] = list[i].BillMaintenance_PaysValue;
                            row["BillOUTCount"] = list[i].BillOUTCount;
                            row["BillOUTValue"] = list[i].BillOUTValue;
                            row["BillOUT_PaysValue"] = list[i].BillOUT_PaysValue;
                            table.Rows.Add(row);
                        }
                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_Buys_ReportDetail_AS_DataTable:" + ee.Message);
                    }
                }

            }

            public class Contact_Pays_ReportDetail
            {
                public const bool DIRECTION_IN = false;
                public const bool DIRECTION_OUT = true;

                public uint PayOPR_ID;
                public bool PayDirection;
                public DateTime PayDate;
                public double Value;
                public uint CurrencyID;
                public string CurrencyName;
                public string CurrencySymbol;
                public double ExchangeRate;
                public double RealValue;
                public uint OperationID;
                public uint OperationType;
                public Contact_Pays_ReportDetail(uint PayOPR_ID_, bool PayDirection_, DateTime PayDate_, double Value_,
                 uint CurrencyID_, string CurrencyName_, string CurrencySymbol_, double ExchangeRate_, double RealValue_,
                 uint OperationID_, uint OperationType_
                 )
                {
                    PayOPR_ID = PayOPR_ID_;
                    PayDirection = PayDirection_; ;
                    PayDate = PayDate_; ;
                    Value = Value_; ;
                    CurrencyID = CurrencyID_; ;
                    CurrencyName = CurrencyName_;
                    CurrencySymbol = CurrencySymbol_;
                    ExchangeRate = ExchangeRate_;
                    RealValue = RealValue_;
                    OperationID = OperationID_;
                    OperationType = OperationType_;
                }
                internal static DataTable Get_Contact_Contact_Pays_ReportDetail_List_AS_DataTable(List<Contact_Pays_ReportDetail> list)
                {

                    try
                    {

                        DataTable table = new DataTable();
                        table.Columns.Add("PayOPR_ID", typeof(uint));
                        table.Columns.Add("PayDirection", typeof(bool));
                        table.Columns.Add("PayDate", typeof(DateTime));
                        table.Columns.Add("Value", typeof(double));
                        table.Columns.Add("CurrencyID", typeof(uint));
                        table.Columns.Add("CurrencyName", typeof(string));
                        table.Columns.Add("CurrencySymbol", typeof(string));
                        table.Columns.Add("ExchangeRate", typeof(double));
                        table.Columns.Add("RealValue", typeof(double));
                        table.Columns.Add("OperationID", typeof(uint));
                        table.Columns.Add("OperationType", typeof(uint));

                        ;
                        for (int i = 0; i < list.Count; i++)
                        {
                            DataRow row = table.NewRow();
                            row["PayOPR_ID"] = list[i].PayOPR_ID;
                            row["PayDirection"] = list[i].PayDirection;
                            row["PayDate"] = list[i].PayDate;
                            row["Value"] = list[i].Value;
                            row["CurrencyID"] = list[i].CurrencyID;
                            row["CurrencyName"] = list[i].CurrencyName;
                            row["CurrencySymbol"] = list[i].CurrencySymbol;
                            row["ExchangeRate"] = list[i].ExchangeRate;
                            row["RealValue"] = list[i].RealValue;
                            row["OperationID"] = list[i].OperationID;
                            row["OperationType"] = list[i].OperationType;
                            table.Rows.Add(row);
                        }
                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_Buys_ReportDetail_AS_DataTable:" + ee.Message);
                    }
                }


            }
            public class Contact_PayCurrencyReport
            {

                public uint CurrencyID;
                public string CurrencyName;
                public string CurrencySymbol;
                public double PaysIN_Sell;
                public double PaysIN_Maintenance;
                public double PaysOUT_Buy;


                public Contact_PayCurrencyReport(uint CurrencyID_, string CurrencyName_, string CurrencySymbol_,
                double PaysIN_Sell_, double PaysIN_Maintenance_, double PaysOUT_Buy_)
                {
                    CurrencyID = CurrencyID_;
                    CurrencyName = CurrencyName_;
                    CurrencySymbol = CurrencySymbol_;
                    PaysIN_Sell = PaysIN_Sell_;
                    PaysIN_Maintenance = PaysIN_Maintenance_;
                    PaysOUT_Buy = PaysOUT_Buy_;
                }
                internal static DataTable Get_Contact_PayCurrencyReport_AS_DataTable(List<Contact_PayCurrencyReport> list)
                {

                    try
                    {

                        DataTable table = new DataTable();
                        table.Columns.Add("CurrencyID", typeof(uint));
                        table.Columns.Add("CurrencyName", typeof(string));
                        table.Columns.Add("CurrencySymbol", typeof(string));
                        table.Columns.Add("PaysIN_Sell", typeof(double));
                        table.Columns.Add("PaysIN_Maintenance", typeof(double));
                        table.Columns.Add("PaysOUT_Buy", typeof(double));

                        ;
                        for (int i = 0; i < list.Count; i++)
                        {
                            DataRow row = table.NewRow();
                            row["CurrencyID"] = list[i].CurrencyID;
                            row["CurrencyName"] = list[i].CurrencyName;
                            row["CurrencySymbol"] = list[i].CurrencySymbol;
                            row["PaysIN_Sell"] = list[i].PaysIN_Sell;
                            row["PaysIN_Maintenance"] = list[i].PaysIN_Maintenance;
                            row["PaysOUT_Buy"] = list[i].PaysOUT_Buy;

                            table.Rows.Add(row);
                        }
                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_Buys_ReportDetail_AS_DataTable:" + ee.Message);
                    }
                }

            }

            public class Contact_Buys_ReportDetail
            {

                public DateTime Bill_Date;
                public uint Bill_ID;
                public int ClauseS_Count;
                public double Amount_IN;
                public double Amount_Remain;
                public double BillValue;
                public uint CurrencyID;
                public string CurrencyName;
                public string CurrencySymbol;
                public double ExchangeRate;
                public string PaysAmount;
                public double PaysRemain;
                public double Bill_RealValue;
                public double Bill_Pays_RealValue;
                public string Bill_ItemsOut_Value;
                public double Bill_ItemsOut_RealValue;
                public string Bill_Pays_Return_Value;
                public double Bill_Pays_Return_RealValue;

                public Contact_Buys_ReportDetail(DateTime Bill_Date_,
                 uint Bill_ID_,
                 int ClauseS_Count_,
                 double Amount_IN_,
                 double Amount_Remain_,
                 double BillValue_,
                 uint CurrencyID_,
                 string CurrencyName_,
                 string CurrencySymbol_,
                 double ExchangeRate_,
                 string PaysAmount_,
                 double PaysRemain_,
                 double Bill_RealValue_,
                 double Bill_Pays_RealValue_,
                 string Bill_ItemsOut_Value_,
                 double Bill_ItemsOut_RealValue_,
                 string Bill_Pays_Return_Value_,
                 double Bill_Pays_Return_RealValue_
                   )
                {
                    Bill_Date = Bill_Date_;
                    Bill_ID = Bill_ID_;
                    ClauseS_Count = ClauseS_Count_;
                    Amount_IN = Amount_IN_;
                    Amount_Remain = Amount_Remain_;
                    BillValue = BillValue_;
                    CurrencyID = CurrencyID_;
                    CurrencyName = CurrencyName_;
                    CurrencySymbol = CurrencySymbol_;
                    ExchangeRate = ExchangeRate_;
                    PaysAmount = PaysAmount_;
                    PaysRemain = PaysRemain_;
                    Bill_RealValue = Bill_RealValue_;
                    Bill_Pays_RealValue = Bill_Pays_RealValue_;
                    Bill_ItemsOut_Value = Bill_ItemsOut_Value_;
                    Bill_ItemsOut_RealValue = Bill_ItemsOut_RealValue_;
                    Bill_Pays_Return_Value = Bill_Pays_Return_Value_;
                    Bill_Pays_Return_RealValue = Bill_Pays_Return_RealValue_;

                }
                internal static DataTable Get_Contact_Buys_ReportDetail_List_AS_DataTable(List<Contact_Buys_ReportDetail> list)
                {

                    try
                    {

                        DataTable table = new DataTable();
                        table.Columns.Add("Bill_Date", typeof(DateTime));
                        table.Columns.Add("Bill_ID", typeof(uint));
                        table.Columns.Add("ClauseS_Count", typeof(int));
                        table.Columns.Add("Amount_IN", typeof(double));
                        table.Columns.Add("Amount_Remain", typeof(double));
                        table.Columns.Add("BillValue", typeof(double));
                        table.Columns.Add("CurrencyID", typeof(uint));
                        table.Columns.Add("CurrencyName", typeof(string));
                        table.Columns.Add("CurrencySymbol", typeof(string));
                        table.Columns.Add("ExchangeRate", typeof(double));
                        table.Columns.Add("PaysAmount", typeof(string));
                        table.Columns.Add("PaysRemain", typeof(double));
                        table.Columns.Add("Bill_RealValue", typeof(double));
                        table.Columns.Add("Bill_Pays_RealValue", typeof(double));
                        table.Columns.Add("Bill_ItemsOut_Value", typeof(string));
                        table.Columns.Add("Bill_ItemsOut_RealValue", typeof(double));
                        table.Columns.Add("Bill_Pays_Return_Value", typeof(string));
                        table.Columns.Add("Bill_Pays_Return_RealValue", typeof(double));
                        ;
                        for (int i = 0; i < list.Count; i++)
                        {
                            DataRow row = table.NewRow();
                            row["Bill_Date"] = list[i].Bill_Date;
                            row["Bill_ID"] = list[i].Bill_ID;
                            row["ClauseS_Count"] = list[i].ClauseS_Count;
                            row["Amount_IN"] = list[i].Amount_IN;
                            row["Amount_Remain"] = list[i].Amount_Remain;
                            row["BillValue"] = list[i].BillValue;
                            row["CurrencyID"] = list[i].CurrencyID;
                            row["CurrencyName"] = list[i].CurrencyName;
                            row["CurrencySymbol"] = list[i].CurrencySymbol;
                            row["ExchangeRate"] = list[i].ExchangeRate;
                            row["PaysAmount"] = list[i].PaysAmount;
                            row["PaysRemain"] = list[i].PaysRemain;
                            row["Bill_RealValue"] = list[i].Bill_RealValue;
                            row["Bill_Pays_RealValue"] = list[i].Bill_Pays_RealValue;
                            row["Bill_ItemsOut_Value"] = list[i].Bill_ItemsOut_Value;
                            row["Bill_ItemsOut_RealValue"] = list[i].Bill_ItemsOut_RealValue;
                            row["Bill_Pays_Return_Value"] = list[i].Bill_Pays_Return_Value;
                            row["Bill_Pays_Return_RealValue"] = list[i].Bill_Pays_Return_RealValue;

                            table.Rows.Add(row);
                        }
                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_Buys_ReportDetail_AS_DataTable:" + ee.Message);
                    }
                }

            }
            public class Contact_Buys_Report
            {

                public int Bills_Count;
                public double Amount_IN;
                public double Amount_Remain;
                public string Bills_Value;

                public string Bills_Pays_Value;
                public string Bills_Pays_Remain;
                public double Bills_Pays_Remain_UPON_Bill_Currency;

                public double Bills_RealValue;
                public double Bills_Pays_RealValue;
                public string Bills_ItemsOut_Value;

                public double Bills_ItemsOut_RealValue;
                public string Bills_Pays_Return_Value;
                public double Bills_Pays_Return_RealValue;
                public Contact_Buys_Report(
                  int Bills_Count_,
                 double Amount_IN_,
                 double Amount_Remain_,
                 string Bills_Value_,
                 string Bills_Pays_Value_,
                 string Bills_Pays_Remain_,

                 double Bills_Pays_Remain_UPON_Bill_Currency_,

                 double Bills_RealValue_,
                 double Bills_Pays_RealValue_,
                 string Bills_ItemsOut_Value_,
                 double Bills_ItemsOut_RealValue_,
                 string Bills_Pays_Return_Value_,
                 double Bills_Pays_Return_RealValue_)
                {
                    Bills_Count = Bills_Count_;
                    Amount_IN = Amount_IN_;
                    Amount_Remain = Amount_Remain_;
                    Bills_Value = Bills_Value_;

                    Bills_Pays_Value = Bills_Pays_Value_;
                    Bills_Pays_Remain = Bills_Pays_Remain_;
                    Bills_Pays_Remain_UPON_Bill_Currency = Bills_Pays_Remain_UPON_Bill_Currency_;

                    Bills_RealValue = Bills_RealValue_;
                    Bills_Pays_RealValue = Bills_Pays_RealValue_;
                    Bills_ItemsOut_Value = Bills_ItemsOut_Value_;
                    Bills_ItemsOut_RealValue = Bills_ItemsOut_RealValue_;
                    Bills_Pays_Return_Value = Bills_Pays_Return_Value_;
                    Bills_Pays_Return_RealValue = Bills_Pays_Return_RealValue_;
                }
                internal static DataTable Get_Contact_Buys_Report_AS_DataTable(Contact_Buys_Report Contact_Buys_Report_)
                {

                    try
                    {
                        DataTable table = new DataTable();
                        table.Columns.Add("Bills_Count", typeof(int));
                        table.Columns.Add("Amount_IN", typeof(double));
                        table.Columns.Add("Amount_Remain", typeof(double));
                        table.Columns.Add("Bills_Value", typeof(string));

                        table.Columns.Add("Bills_Pays_Value", typeof(string));
                        table.Columns.Add("Bills_Pays_Remain", typeof(string));
                        table.Columns.Add("Bills_Pays_Remain_UPON_Bill_Currency", typeof(double));

                        table.Columns.Add("Bills_RealValue", typeof(double));
                        table.Columns.Add("Bills_Pays_RealValue", typeof(double));
                        table.Columns.Add("Bills_ItemsOut_Value", typeof(string));

                        table.Columns.Add("Bills_ItemsOut_RealValue", typeof(double));
                        table.Columns.Add("Bills_Pays_Return_Value", typeof(string));
                        table.Columns.Add("Bills_Pays_Return_RealValue", typeof(double));

                        DataRow row = table.NewRow();
                        row["Bills_Count"] = Contact_Buys_Report_.Bills_Count;
                        row["Amount_IN"] = Contact_Buys_Report_.Amount_IN;
                        row["Amount_Remain"] = Contact_Buys_Report_.Amount_Remain;
                        row["Bills_Value"] = Contact_Buys_Report_.Bills_Value;
                        row["Bills_Pays_Value"] = Contact_Buys_Report_.Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = Contact_Buys_Report_.Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_Bill_Currency"] = Contact_Buys_Report_.Bills_Pays_Remain_UPON_Bill_Currency;
                        row["Bills_RealValue"] = Contact_Buys_Report_.Bills_RealValue;
                        row["Bills_Pays_RealValue"] = Contact_Buys_Report_.Bills_Pays_RealValue;
                        row["Bills_ItemsOut_Value"] = Contact_Buys_Report_.Bills_ItemsOut_Value;
                        row["Bills_ItemsOut_RealValue"] = Contact_Buys_Report_.Bills_ItemsOut_RealValue;
                        row["Bills_Pays_Return_Value"] = Contact_Buys_Report_.Bills_Pays_Return_Value;
                        row["Bills_Pays_Return_RealValue"] = Contact_Buys_Report_.Bills_Pays_Return_RealValue;

                        table.Rows.Add(row);

                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_Buys_Report_AS_DataTable:" + ee.Message);
                    }
                }

            }
            public class Contact_Sells_ReportDetail
            {

                public DateTime Bill_Date;
                public uint Bill_ID;
                public string SellType;
                public int ClauseS_Count;
                public double BillValue;
                public uint CurrencyID;
                public string CurrencyName;
                public string CurrencySymbol;
                public double ExchangeRate;
                public int PaysCount;
                public string PaysAmount;
                public double PaysRemain;
                public string Source_ItemsIN_Cost_Details;
                public double Source_ItemsIN_RealCost;
                public double BillValue_RealValue;
                public double RealPaysValue;
                public Contact_Sells_ReportDetail(DateTime Bill_Date_,
                 uint Bill_ID_,
                 string SellType_,
                 int ClauseS_Count_,
                 double BillValue_,
                              uint CurrencyID_,
                 string CurrencyName_,
                 string CurrencySymbol_,
                double ExchangeRate_,
                 int PaysCount_,
                 string PaysAmount_,
                 double PaysRemain_,
                 string Source_ItemsIN_Cost_Details_,
                 double Source_ItemsIN_RealCost_,
                 double BillValue_RealValue_,
                 double RealPaysValue_
                   )
                {
                    Bill_Date = Bill_Date_;
                    Bill_ID = Bill_ID_;
                    SellType = SellType_;
                    ClauseS_Count = ClauseS_Count_;
                    BillValue = BillValue_;
                    CurrencyID = CurrencyID_;
                    CurrencyName = CurrencyName_;
                    CurrencySymbol = CurrencySymbol_;
                    ExchangeRate = ExchangeRate_;
                    PaysCount = PaysCount_;
                    PaysAmount = PaysAmount_;
                    PaysRemain = PaysRemain_;
                    Source_ItemsIN_Cost_Details = Source_ItemsIN_Cost_Details_;
                    Source_ItemsIN_RealCost = Source_ItemsIN_RealCost_;
                    BillValue_RealValue = BillValue_RealValue_;
                    RealPaysValue = RealPaysValue_;
                }
                internal static DataTable Get_Contact_Sells_ReportDetail_List_AS_DataTable(List<Contact_Sells_ReportDetail> list)
                {

                    try
                    {

                        DataTable table = new DataTable();
                        table.Columns.Add("Bill_Date", typeof(DateTime));
                        table.Columns.Add("Bill_ID", typeof(uint));
                        table.Columns.Add("SellType", typeof(string));
                        table.Columns.Add("ClauseS_Count", typeof(int));
                        table.Columns.Add("BillValue", typeof(double));
                        table.Columns.Add("CurrencyID", typeof(uint));
                        table.Columns.Add("CurrencyName", typeof(string));
                        table.Columns.Add("CurrencySymbol", typeof(string));
                        table.Columns.Add("ExchangeRate", typeof(double));
                        table.Columns.Add("PaysCount", typeof(int));
                        table.Columns.Add("PaysAmount", typeof(string));
                        table.Columns.Add("PaysRemain", typeof(double));
                        table.Columns.Add("Source_ItemsIN_Cost_Details", typeof(string));
                        table.Columns.Add("Source_ItemsIN_RealCost", typeof(double));
                        table.Columns.Add("BillValue_RealValue", typeof(double));
                        table.Columns.Add("RealPaysValue", typeof(double));

                        for (int i = 0; i < list.Count; i++)
                        {
                            DataRow row = table.NewRow();
                            row["Bill_Date"] = list[i].Bill_Date;
                            row["Bill_ID"] = list[i].Bill_ID;
                            row["SellType"] = list[i].SellType;
                            row["ClauseS_Count"] = list[i].ClauseS_Count;
                            row["BillValue"] = list[i].BillValue;
                            row["CurrencyID"] = list[i].CurrencyID;
                            row["CurrencyName"] = list[i].CurrencyName;
                            row["CurrencySymbol"] = list[i].CurrencySymbol;
                            row["ExchangeRate"] = list[i].ExchangeRate;
                            row["PaysCount"] = list[i].PaysCount;
                            row["PaysAmount"] = list[i].PaysAmount;
                            row["PaysRemain"] = list[i].PaysRemain;
                            row["Source_ItemsIN_Cost_Details"] = list[i].Source_ItemsIN_Cost_Details;
                            row["Source_ItemsIN_RealCost"] = list[i].Source_ItemsIN_RealCost;
                            row["BillValue_RealValue"] = list[i].BillValue_RealValue;
                            row["RealPaysValue"] = list[i].RealPaysValue;

                            table.Rows.Add(row);
                        }
                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_Sells_ReportDetail_List_AS_DataTable:" + ee.Message);
                    }
                }


            }
            public class Contact_Sells_Report
            {

                public int Bills_Count;
                public string Bills_Value;
                public string Bills_Pays_Value;
                public string Bills_Pays_Remain;
                public double Bills_Pays_Remain_UPON_BillsCurrency;
                public string Bills_ItemsIN_Value;
                public double Bills_ItemsIN_RealValue;
                public double Bills_RealValue;
                public double Bills_Pays_RealValue;
                public Contact_Sells_Report(
                  int Bills_Count_,
                 string Bills_Value_,
                 string Bills_Pays_Value_,
                 string Bills_Pays_Remain_,
                 double Bills_Pays_Remain_UPON_BillsCurrency_,
                 string Bills_ItemsIN_Value_,
                 double Bills_ItemsIN_RealValue_,
                 double Bills_RealValue_,
                 double Bills_Pays_RealValue_)
                {

                    Bills_Count = Bills_Count_;
                    Bills_Value = Bills_Value_;
                    Bills_Pays_Value = Bills_Pays_Value_;
                    Bills_Pays_Remain = Bills_Pays_Remain_;
                    Bills_Pays_Remain_UPON_BillsCurrency = Bills_Pays_Remain_UPON_BillsCurrency_;
                    Bills_ItemsIN_Value = Bills_ItemsIN_Value_;
                    Bills_ItemsIN_RealValue = Bills_ItemsIN_RealValue_;
                    Bills_RealValue = Bills_RealValue_;
                    Bills_Pays_RealValue = Bills_Pays_RealValue_;
                }
                internal static DataTable Get_Contact_Sells_Report_AS_DataTable(Contact_Sells_Report Contact_Sells_Report_)
                {

                    try
                    {

                        DataTable table = new DataTable();
                        table.Columns.Add("Bills_Count", typeof(int));
                        table.Columns.Add("Bills_Value", typeof(string));
                        table.Columns.Add("Bills_Pays_Value", typeof(string));
                        table.Columns.Add("Bills_Pays_Remain", typeof(string));
                        table.Columns.Add("Bills_Pays_Remain_UPON_BillsCurrency", typeof(double));
                        table.Columns.Add("Bills_ItemsIN_Value", typeof(string));
                        table.Columns.Add("Bills_ItemsIN_RealValue", typeof(double));
                        table.Columns.Add("Bills_RealValue", typeof(double));
                        table.Columns.Add("Bills_Pays_RealValue", typeof(double));

                        DataRow row = table.NewRow();
                        row["Bills_Count"] = Contact_Sells_Report_.Bills_Count;
                        row["Bills_Value"] = Contact_Sells_Report_.Bills_Value;
                        row["Bills_Pays_Value"] = Contact_Sells_Report_.Bills_Pays_Value;
                        row["Bills_Pays_Remain"] = Contact_Sells_Report_.Bills_Pays_Remain;
                        row["Bills_Pays_Remain_UPON_BillsCurrency"] = Contact_Sells_Report_.Bills_Pays_Remain_UPON_BillsCurrency;
                        row["Bills_ItemsIN_Value"] = Contact_Sells_Report_.Bills_ItemsIN_Value;
                        row["Bills_ItemsIN_RealValue"] = Contact_Sells_Report_.Bills_ItemsIN_RealValue;
                        row["Bills_RealValue"] = Contact_Sells_Report_.Bills_RealValue;
                        row["Bills_Pays_RealValue"] = Contact_Sells_Report_.Bills_Pays_RealValue;

                        table.Rows.Add(row);

                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_Sells_ReportDetail_List_AS_DataTable:" + ee.Message);
                    }
                }

            }
            public class Contact_MaintenanceOPRs_ReportDetail
            {

                public DateTime MaintenanceOPR_Date;
                public uint MaintenanceOPR_ID;
                public uint ItemID;
                public string ItemName;
                public string ItemCompany;
                public string FolderName;
                public string FalutDesc;
                public DateTime? MaintenanceOPR_Endworkdate;
                public bool? MaintenanceOPR_Rpaired;
                public DateTime? MaintenanceOPR_DeliverDate;
                public DateTime? MaintenanceOPR_EndWarrantyDate;
                public uint? BillMaintenanceID;
                public double? BillValue;
                public uint? CurrencyID;
                public string CurrencyName;
                public string CurrencySymbol;
                public double? ExchangeRate;
                public string PaysAmount;
                public double? PaysRemain;
                public string Bill_ItemsOut_Value;
                public double? Bill_ItemsOut_RealValue;
                public double? Bill_RealValue;
                public double? Bill_Pays_RealValue;





                public Contact_MaintenanceOPRs_ReportDetail(
                    DateTime MaintenanceOPR_Date_,
                 uint MaintenanceOPR_ID_,
                   uint ItemID_,
                 string ItemName_,
                 string ItemCompany_,
                 string FolderName_,
                string FalutDesc_,
                  DateTime? MaintenanceOPR_Endworkdate_,
                 bool? MaintenanceOPR_Rpaired_,
                 DateTime? MaintenanceOPR_DeliverDate_,
                DateTime? MaintenanceOPR_EndWarrantyDate_,
                 uint? BillMaintenanceID_,
                  double? BillValue_,
                   uint? CurrencyID_,
                 string CurrencyName_,
                 string CurrencySymbol_,
                double? ExchangeRate_,
                 string PaysAmount_,
                 double? PaysRemain_,
                string Bill_ItemsOut_Value_,
                 double? Bill_ItemsOut_RealValue_,
                 double? Bill_RealValue_,
                 double? Bill_Pays_RealValue_


                   )
                {
                    MaintenanceOPR_Date = MaintenanceOPR_Date_;
                    MaintenanceOPR_ID = MaintenanceOPR_ID_;
                    ItemID = ItemID_;
                    ItemName = ItemName_;
                    ItemCompany = ItemCompany_;
                    FolderName = FolderName_;
                    FalutDesc = FalutDesc_;
                    MaintenanceOPR_Endworkdate = MaintenanceOPR_Endworkdate_;
                    MaintenanceOPR_Rpaired = MaintenanceOPR_Rpaired_;
                    MaintenanceOPR_DeliverDate = MaintenanceOPR_DeliverDate_;
                    MaintenanceOPR_EndWarrantyDate = MaintenanceOPR_EndWarrantyDate_;
                    BillMaintenanceID = BillMaintenanceID_;
                    BillValue = BillValue_;
                    CurrencyID = CurrencyID_;
                    CurrencyName = CurrencyName_;
                    CurrencySymbol = CurrencySymbol_;
                    ExchangeRate = ExchangeRate_;
                    PaysAmount = PaysAmount_;
                    PaysRemain = PaysRemain_;
                    Bill_ItemsOut_Value = Bill_ItemsOut_Value_;
                    Bill_ItemsOut_RealValue = Bill_ItemsOut_RealValue_;
                    Bill_RealValue = Bill_RealValue_;
                    Bill_Pays_RealValue = Bill_Pays_RealValue_;


                }
                internal static DataTable Get_Contact_MaintenanceOPRs_ReportDetail_List_AS_DataTable(List<Contact_MaintenanceOPRs_ReportDetail> list)
                {

                    try
                    {
                        DataTable table = new DataTable();

                        table.Columns.Add("MaintenanceOPR_Date", typeof(DateTime));
                        table.Columns.Add("MaintenanceOPR_ID", typeof(uint));
                        table.Columns.Add("ItemID", typeof(uint));
                        table.Columns.Add("ItemName", typeof(string));
                        table.Columns.Add("ItemCompany", typeof(string));
                        table.Columns.Add("FolderName", typeof(string));
                        table.Columns.Add("FalutDesc", typeof(string));
                        table.Columns.Add("MaintenanceOPR_Endworkdate", typeof(object));
                        table.Columns.Add("MaintenanceOPR_Rpaired", typeof(object));
                        table.Columns.Add("MaintenanceOPR_DeliverDate", typeof(object));
                        table.Columns.Add("MaintenanceOPR_EndWarrantyDate", typeof(object));
                        table.Columns.Add("BillMaintenanceID", typeof(object));
                        table.Columns.Add("BillValue", typeof(object));
                        table.Columns.Add("CurrencyID", typeof(object));
                        table.Columns.Add("CurrencyName", typeof(string));
                        table.Columns.Add("CurrencySymbol", typeof(string));
                        table.Columns.Add("ExchangeRate", typeof(object));
                        table.Columns.Add("PaysAmount", typeof(string));
                        table.Columns.Add("PaysRemain", typeof(object));
                        table.Columns.Add("Bill_ItemsOut_Value", typeof(string));
                        table.Columns.Add("Bill_ItemsOut_RealValue", typeof(object));
                        table.Columns.Add("Bill_RealValue", typeof(object));
                        table.Columns.Add("Bill_Pays_RealValue", typeof(object));


                        for (int i = 0; i < list.Count; i++)
                        {
                            DataRow row = table.NewRow();

                            row["MaintenanceOPR_Date"] = list[i].MaintenanceOPR_Date;
                            row["MaintenanceOPR_ID"] = list[i].MaintenanceOPR_ID;
                            row["ItemID"] = list[i].ItemID;
                            row["ItemName"] = list[i].ItemName;
                            row["ItemCompany"] = list[i].ItemCompany;
                            row["FolderName"] = list[i].FolderName;
                            row["FalutDesc"] = list[i].FalutDesc;

                            row["MaintenanceOPR_Endworkdate"] = list[i].MaintenanceOPR_Endworkdate == null ? DBNull.Value : (object)list[i].MaintenanceOPR_Endworkdate;

                            row["MaintenanceOPR_Rpaired"] = list[i].MaintenanceOPR_Rpaired == null ? DBNull.Value : (object)list[i].MaintenanceOPR_Rpaired;
                            row["MaintenanceOPR_DeliverDate"] = list[i].MaintenanceOPR_DeliverDate == null ? DBNull.Value : (object)list[i].MaintenanceOPR_DeliverDate;
                            row["MaintenanceOPR_EndWarrantyDate"] = list[i].MaintenanceOPR_EndWarrantyDate == null ? DBNull.Value : (object)list[i].MaintenanceOPR_EndWarrantyDate;
                            row["BillMaintenanceID"] = list[i].BillMaintenanceID == null ? DBNull.Value : (object)list[i].BillMaintenanceID;
                            row["BillValue"] = list[i].BillValue == null ? DBNull.Value : (object)list[i].BillValue;
                            row["CurrencyID"] = list[i].CurrencyID == null ? DBNull.Value : (object)list[i].CurrencyID;
                            row["CurrencyName"] = list[i].CurrencyName;
                            row["CurrencySymbol"] = list[i].CurrencySymbol;
                            row["ExchangeRate"] = list[i].ExchangeRate == null ? DBNull.Value : (object)list[i].ExchangeRate;
                            row["PaysAmount"] = list[i].PaysAmount;
                            row["PaysRemain"] = list[i].PaysRemain == null ? DBNull.Value : (object)list[i].PaysRemain;
                            row["Bill_ItemsOut_Value"] = list[i].Bill_ItemsOut_Value;
                            row["Bill_ItemsOut_RealValue"] = list[i].Bill_ItemsOut_RealValue == null ? DBNull.Value : (object)list[i].Bill_ItemsOut_RealValue;
                            row["Bill_RealValue"] = list[i].Bill_RealValue == null ? DBNull.Value : (object)list[i].Bill_RealValue;
                            row["Bill_Pays_RealValue"] = list[i].Bill_Pays_RealValue == null ? DBNull.Value : (object)list[i].Bill_Pays_RealValue;

                            table.Rows.Add(row);
                        }
                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_MaintenanceOPRs_ReportDetail_List_AS_DataTable:" + ee.Message);
                    }
                }


            }
            public class Contact_MaintenanceOPRs_Report
            {

                public int MaintenanceOPRs_Count;
                public int MaintenanceOPRs_EndWork_Count;
                public int MaintenanceOPRs_Repaired_Count;
                public int MaintenanceOPRs_Warranty_Count;
                public int MaintenanceOPRs_EndWarranty_Count;

                public int BillMaintenances_Count;
                public string BillMaintenances_Value;
                public string BillMaintenances_Pays_Value;
                public string BillMaintenances_Pays_Remain;
                public double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;

                public string BillMaintenances_ItemsOut_Value;
                public double BillMaintenances_ItemsOut_RealValue;
                public double BillMaintenances_RealValue;
                public double BillMaintenances_Pays_RealValue;
                public Contact_MaintenanceOPRs_Report(
                   int MaintenanceOPRs_Count_,
                 int MaintenanceOPRs_EndWork_Count_,
                 int MaintenanceOPRs_Repaired_Count_,
                 int MaintenanceOPRs_Warranty_Count_,
                 int MaintenanceOPRs_EndWarranty_Count_,

                   int BillMaintenances_Count_,
                 string BillMaintenances_Value_,
                 string BillMaintenances_Pays_Value_,
                 string BillMaintenances_Pays_Remain_,
                 double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_,

                 string BillMaintenances_ItemsOut_Value_,
                 double BillMaintenances_ItemsOut_RealValue_,
                 double BillMaintenances_RealValue_,
                 double BillMaintenances_Pays_RealValue_)
                {
                    MaintenanceOPRs_Count = MaintenanceOPRs_Count_;
                    MaintenanceOPRs_EndWork_Count = MaintenanceOPRs_EndWork_Count_;
                    MaintenanceOPRs_Repaired_Count = MaintenanceOPRs_Repaired_Count_;
                    MaintenanceOPRs_Warranty_Count = MaintenanceOPRs_Warranty_Count_;
                    MaintenanceOPRs_EndWarranty_Count = MaintenanceOPRs_EndWarranty_Count_;

                    BillMaintenances_Count = BillMaintenances_Count_;
                    BillMaintenances_Value = BillMaintenances_Value_;
                    BillMaintenances_Pays_Value = BillMaintenances_Pays_Value_;
                    BillMaintenances_Pays_Remain = BillMaintenances_Pays_Remain_;
                    BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency_;

                    BillMaintenances_ItemsOut_Value = BillMaintenances_ItemsOut_Value_;
                    BillMaintenances_ItemsOut_RealValue = BillMaintenances_ItemsOut_RealValue_;
                    BillMaintenances_RealValue = BillMaintenances_RealValue_;
                    BillMaintenances_Pays_RealValue = BillMaintenances_Pays_RealValue_;
                }
                internal static DataTable Get_Contact_MaintenanceOPRs_Report_List_AS_DataTable(Contact_MaintenanceOPRs_Report Contact_MaintenanceOPRs_Report_)
                {

                    try
                    {
                        DataTable table = new DataTable();
                        table.Columns.Add("MaintenanceOPRs_Count", typeof(int));
                        table.Columns.Add("MaintenanceOPRs_EndWork_Count", typeof(int));
                        table.Columns.Add("MaintenanceOPRs_Repaired_Count", typeof(int));
                        table.Columns.Add("MaintenanceOPRs_Warranty_Count", typeof(int));
                        table.Columns.Add("MaintenanceOPRs_EndWarranty_Count", typeof(int));

                        table.Columns.Add("BillMaintenances_Count", typeof(int));
                        table.Columns.Add("BillMaintenances_Value", typeof(string));
                        table.Columns.Add("BillMaintenances_Pays_Value", typeof(string));
                        table.Columns.Add("BillMaintenances_Pays_Remain", typeof(string));
                        table.Columns.Add("BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency", typeof(double));

                        table.Columns.Add("BillMaintenances_ItemsOut_Value", typeof(string));
                        table.Columns.Add("BillMaintenances_ItemsOut_RealValue", typeof(double));
                        table.Columns.Add("BillMaintenances_RealValue", typeof(double));
                        table.Columns.Add("BillMaintenances_Pays_RealValue", typeof(double));


                        DataRow row = table.NewRow();

                        row["MaintenanceOPRs_Count"] = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_Count;
                        row["MaintenanceOPRs_EndWork_Count"] = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_EndWork_Count;
                        row["MaintenanceOPRs_Repaired_Count"] = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_Repaired_Count;
                        row["MaintenanceOPRs_Warranty_Count"] = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_Warranty_Count;
                        row["MaintenanceOPRs_EndWarranty_Count"] = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_EndWarranty_Count;

                        row["BillMaintenances_Count"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_Count;
                        row["BillMaintenances_Value"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_Value;
                        row["BillMaintenances_Pays_Value"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_Pays_Value;
                        row["BillMaintenances_Pays_Remain"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_Pays_Remain;
                        row["BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency;

                        row["BillMaintenances_ItemsOut_Value"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_ItemsOut_Value;
                        row["BillMaintenances_ItemsOut_RealValue"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_ItemsOut_RealValue;
                        row["BillMaintenances_RealValue"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_RealValue;
                        row["BillMaintenances_Pays_RealValue"] = Contact_MaintenanceOPRs_Report_.BillMaintenances_Pays_RealValue;


                        table.Rows.Add(row);

                        return table;
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Contact_MaintenanceOPRs_Report_List_AS_DataTable:" + ee.Message);
                    }
                }

            }
            #endregion
        }
    
}