using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using System.Drawing;
using OverLoad_Client.Trade.Forms;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Maintenance.MaintenanceSQL;

namespace OverLoad_Client.AccountingObj
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
            internal string GetAccountMoneyOverAll(MoneyBox moneybox)
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                string money_amount = new DataBaseFunctions(DB).Account_GetAmountMoneyOverAll(moneybox);
                if (money_amount.Length == 0)
                    return " الصندوق فارغ ";
                else return   "قيمة الصندوق :" + money_amount;
            }
            internal List<PayCurrencyReport> GetPayReport_InDay(MoneyBox moneybox, int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));

                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year"] = year ;
                    row["Month"] = month ;
                    row["Day"] = day ;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_DayReport,
                       para);

                    return PayCurrencyReport.Get_PayCurrencyReport_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayReport_InDay:" + ee.Message);

                }

            }
            internal List<PayCurrencyReport> GetPayReport_InMonth(MoneyBox moneybox, int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));

                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_MonthReport,
                       para);

                    return PayCurrencyReport.Get_PayCurrencyReport_List_From_DataTable(t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayReport_InMonth:" + ee.Message);

                }

            }
            internal List<PayCurrencyReport> GetPayReport_INYear(MoneyBox moneybox, int year)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year", typeof(int));

                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_YearReport,
                       para);

                    return PayCurrencyReport.Get_PayCurrencyReport_List_From_DataTable(t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayReport_InYear:" + ee.Message);

                }
            }
            internal List<PayCurrencyReport> GetPayReport_betweenTwoYears(MoneyBox moneybox, int year1, int year2)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year1", typeof(int));
                    para.Columns.Add("Year2", typeof(int));


                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year1"] = year1;
                    row["Year2"] = year2;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_YearRangeReport ,
                       para);

                    return PayCurrencyReport.Get_PayCurrencyReport_List_From_DataTable(t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayReport_InYearRange:" + ee.Message);

                }
            }
            internal List<AccountOprReportDetail> GetAccountOprReport_Details_InDay(MoneyBox moneybox,int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year"] = year ;
                    row["Month"] = month ;
                    row["Day"] = day ;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InDay ,
                       para);

                    return AccountOprReportDetail.Get_AccountOprReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetAccountOprReport_Details_InDay:" + ee.Message);

                }
            }
            internal List<AccountOprDayReportDetail> GetAccountOprReport_Details_InMonth(MoneyBox moneybox, int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InMonh ,
                       para);

                    return AccountOprDayReportDetail.Get_AccountOprDayReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetAccountOprReport_Details_InMonth:" + ee.Message);

                }
            }
            internal List<AccountOprMonthReportDetail> GetAccountOprReport_Details_InYear(MoneyBox moneybox, int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InYear,
                       para);

                    return AccountOprMonthReportDetail.Get_AccountOprMonthReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetAccountOprReport_Details_InYear:" + ee.Message);

                }
            }
            internal List<AccountOprYearReportDetail> GetAccountOprReport_Details_InYearRange(MoneyBox moneybox, int year1, int year2)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("MoneyBoxID", typeof(uint));
                    para.Columns.Add("Year1", typeof(int));
                    para.Columns.Add("Year2", typeof(int));


                    DataRow row = para.NewRow();
                    row["MoneyBoxID"] = moneybox.BoxID;
                    row["Year1"] = year1;
                    row["Year2"] = year2;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InYearRange,
                       para);

                    return AccountOprYearReportDetail.Get_AccountOprYearReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("GetAccountOprReport_Details_InYearRange:" + ee.Message);

                }
            }

        }
        internal class ReportBuysSQL
        {
            DatabaseInterface DB;
            internal ReportBuysSQL(DatabaseInterface db)
            {
                DB = db;

            }

      
            #region BuyReports
       
            internal List<Report_Buys_Day_ReportDetail> Get_Report_Buys_Day_ReportDetail(int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group (DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;
                    row["Day"] = day;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Day_ReportDetail ,
                       para);

                    return Report_Buys_Day_ReportDetail.Get_Report_Buys_Day_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Day_ReportDetail:" + ee.Message);

                }

            }
            internal Report_Buys_Month_ReportDetail Get_Report_Buys_Day_Report(int year, int month, int day)
            {
                
                    try
                    {
                        if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                        DataTable para = new DataTable();
                        para.Columns.Add("Year", typeof(int));
                        para.Columns.Add("Month", typeof(int));
                        para.Columns.Add("Day", typeof(int));


                        DataRow row = para.NewRow();
                        row["Year"] = year;
                        row["Month"] = month;
                        row["Day"] = day;

                        para.Rows.Add(row);
                        DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Day_Report ,
                           para);

                        return Report_Buys_Month_ReportDetail.Get_Report_Buys_Month_ReportDetail_List_From_DataTable   (t)[0];

                    }
                    catch (Exception ee)
                    {
                        throw new Exception("Get_Report_Buys_Day_ReportDetail:" + ee.Message);

                    }

                }
            internal List<Report_Buys_Month_ReportDetail> Get_Report_Buys_Month_ReportDetail(int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group (DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Month_ReportDetail ,
                       para);

                    return Report_Buys_Month_ReportDetail.Get_Report_Buys_Month_ReportDetail_List_From_DataTable  (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Month_ReportDetail:" + ee.Message);

                }

            }
            internal Report_Buys_Year_ReportDetail Get_Report_Buys_Month_Report(int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Month_Report,
                       para);

                    return Report_Buys_Year_ReportDetail.Get_Report_Buys_Year_ReportDetail_List_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Month_ReportDetail:" + ee.Message);

                }


            }
            internal List<Report_Buys_Year_ReportDetail> Get_Report_Buys_Year_ReportDetail(int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Year_ReportDetail ,
                       para);

                    return Report_Buys_Year_ReportDetail.Get_Report_Buys_Year_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Year_ReportDetail:" + ee.Message);

                }

            }
            internal Report_Buys_YearRange_ReportDetail Get_Report_Buys_Year_Report(int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Year_Report ,
                       para);

                    return Report_Buys_YearRange_ReportDetail.Get_Report_Buys_YearRange_ReportDetail_List_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Year_ReportDetail:" + ee.Message);

                }
            }
            internal List<Report_Buys_YearRange_ReportDetail> Get_Report_Buys_YearRange_ReportDetail(int min_year, int max_year)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year1", typeof(int));
                    para.Columns.Add("Year2", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year1"] = min_year ;
                    row["Year2"] = max_year ;


                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_YearRange_ReportDetail,
                       para);

                    return Report_Buys_YearRange_ReportDetail.Get_Report_Buys_YearRange_ReportDetail_List_From_DataTable(t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_YearRange_ReportDetail:" + ee.Message);

                }
            }
            #endregion
        }
        internal class ReportPayOrdersSQL
        {
            DatabaseInterface DB;
            internal ReportPayOrdersSQL(DatabaseInterface db)
            {
                DB = db;

            }


            #region PayOrderReports

            internal List<Report_PayOrders_Day_ReportDetail> Get_Report_PayOrders_Day_ReportDetail(int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group (DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;
                    row["Day"] = day;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Day_ReportDetail ,
                       para);

                    return Report_PayOrders_Day_ReportDetail.Get_Report_PayOrders_Day_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Day_ReportDetail:" + ee.Message);

                }
            }
            internal Report_PayOrders_Month_ReportDetail Get_Report_PayOrders_Day_Report(int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;
                    row["Day"] = day;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Day_Report,
                       para);

                    return Report_PayOrders_Month_ReportDetail.Get_Report_PayOrders_Month_ReportDetail_List_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Day_ReportDetail:" + ee.Message);

                }
            }
            internal List<Report_PayOrders_Month_ReportDetail> Get_Report_PayOrders_Month_ReportDetail(int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Month_ReportDetail,
                       para);

                    return Report_PayOrders_Month_ReportDetail.Get_Report_PayOrders_Month_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Month_ReportDetail:" + ee.Message);

                }
            }
            internal Report_PayOrders_Year_ReportDetail Get_Report_PayOrders_Month_Report(int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Month_Report,
                       para);

                    return Report_PayOrders_Year_ReportDetail.Get_Report_PayOrders_Year_ReportDetail_List_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Month_Report:" + ee.Message);

                }
            }
            internal List<Report_PayOrders_Year_ReportDetail> Get_Report_PayOrders_Year_ReportDetail(int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Year_ReportDetail ,
                       para);

                    return Report_PayOrders_Year_ReportDetail.Get_Report_PayOrders_Year_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Year_ReportDetail:" + ee.Message);

                }
            }
            internal Report_PayOrders_YearRange_ReportDetail Get_Report_PayOrders_Year_Report(int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Year_Report ,
                       para);

                    return Report_PayOrders_YearRange_ReportDetail.Get_Report_PayOrders_YearRange_ReportDetail_List_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Year_Report:" + ee.Message);

                }
            }
            internal List<Report_PayOrders_YearRange_ReportDetail> Get_Report_PayOrders_YearRange_ReportDetail(int min_year, int max_year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year1", typeof(int));
                    para.Columns.Add("Year2", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year1"] = min_year ;
                    row["Year2"] = max_year ;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_YearRange_ReportDetail ,
                       para);

                    return Report_PayOrders_YearRange_ReportDetail.Get_Report_PayOrders_YearRange_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_YearRange_ReportDetail:" + ee.Message);

                }
            }
            #endregion
            //internal List<BillDayReportDetail> GetBillReport_Details_InMonth(string year, string month)
            //{
            //    List<BillDayReportDetail> BillDayReportDetailList = new List<BillDayReportDetail>();
            //    try
            //    {

            //        DataTable table = DB.GetData(
            //            " select * from "
            //            + AccountBillTables.BillMonthReport_Details
            //            + "("
            //            + year + ","
            //            + month
            //            + ")");
            //        for (int i = 0; i < table.Rows.Count; i++)
            //        {
            //            int dayid = Convert.ToInt32(table.Rows[i][0].ToString());
            //            DateTime daydate = Convert.ToDateTime(table.Rows[i][1].ToString());
            //            int billin_Count = Convert.ToInt32(table.Rows[i][2].ToString());
            //            string billin_Value = table.Rows[i][3].ToString();
            //            string billin_Pays_Value = table.Rows[i][4].ToString();
            //            int billm_Count = Convert.ToInt32(table.Rows[i][5].ToString());
            //            string billm_Value = table.Rows[i][6].ToString();
            //            string billm_Pays_Value = table.Rows[i][7].ToString();
            //            int billout_Count = Convert.ToInt32(table.Rows[i][8].ToString());
            //            string billout_Value = table.Rows[i][9].ToString();
            //            string billout_Pays_Value = table.Rows[i][10].ToString();

            //            BillDayReportDetail billreportdayDetail = new BillDayReportDetail(dayid, daydate, billin_Count, billin_Value, billin_Pays_Value
            //                , billm_Count, billm_Value, billm_Pays_Value
            //                , billout_Count, billout_Value, billout_Pays_Value);
            //            BillDayReportDetailList.Add(billreportdayDetail);
            //        }
            //        return BillDayReportDetailList;
            //    }
            //    catch
            //    {
            //        MessageBox.Show("فشل جلب تقرير الشهر التفصيلي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return BillDayReportDetailList;
            //    }
            //}
            //internal List<BillMonthReportDetail> GetBillReport_Details_InYear(string year)
            //{
            //    List<BillMonthReportDetail> BillMonthReportDetailList = new List<BillMonthReportDetail>();
            //    try
            //    {
            //        DataTable table = DB.GetData(
            //            " select *from "
            //            + AccountBillTables.BillYearReport_Details
            //            + "("
            //            + year
            //            + ")");
            //        for (int i = 0; i < table.Rows.Count; i++)
            //        {
            //            int monthid = Convert.ToInt32(table.Rows[i][0].ToString());
            //            string month = table.Rows[i][1].ToString();
            //            int billin_Count = Convert.ToInt32(table.Rows[i][2].ToString());
            //            string billin_Value = table.Rows[i][3].ToString();
            //            string billin_Pays_Value = table.Rows[i][4].ToString();
            //            int billm_Count = Convert.ToInt32(table.Rows[i][5].ToString());
            //            string billm_Value = table.Rows[i][6].ToString();
            //            string billm_Pays_Value = table.Rows[i][7].ToString();
            //            int billout_Count = Convert.ToInt32(table.Rows[i][8].ToString());
            //            string billout_Value = table.Rows[i][9].ToString();
            //            string billout_Pays_Value = table.Rows[i][10].ToString();

            //            BillMonthReportDetail billmonthreportdetail = new BillMonthReportDetail(monthid, month, billin_Count, billin_Value, billin_Pays_Value
            //                , billm_Count, billm_Value, billm_Pays_Value
            //                , billout_Count, billout_Value, billout_Pays_Value);
            //            BillMonthReportDetailList.Add(billmonthreportdetail);
            //        }
            //        return BillMonthReportDetailList;
            //    }
            //    catch
            //    {
            //        MessageBox.Show("فشل جلب تقرير السنة التفصيلي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return BillMonthReportDetailList;
            //    }
            //}
            //internal List<BillYearReportDetail> GetBillReport_Details_InYearRange(string year1, string year2)
            //{
            //    List<BillYearReportDetail> BillYearReportDetailList = new List<BillYearReportDetail>();
            //    try
            //    {

            //        DataTable table = DB.GetData(
            //            " select * from "
            //            + AccountBillTables.BillYearRangeReport_Details
            //            + "("
            //            + year1 + ","
            //            + year2
            //            + ")");
            //        for (int i = 0; i < table.Rows.Count; i++)
            //        {
            //            int year = Convert.ToInt32(table.Rows[i][0].ToString());
            //            int billin_Count = Convert.ToInt32(table.Rows[i][1].ToString());
            //            string billin_Value = table.Rows[i][2].ToString();
            //            string billin_Pays_Value = table.Rows[i][3].ToString();
            //            int billm_Count = Convert.ToInt32(table.Rows[i][4].ToString());
            //            string billm_Value = table.Rows[i][5].ToString();
            //            string billm_Pays_Value = table.Rows[i][6].ToString();
            //            int billout_Count = Convert.ToInt32(table.Rows[i][7].ToString());
            //            string billout_Value = table.Rows[i][8].ToString();
            //            string billout_Pays_Value = table.Rows[i][9].ToString();

            //            BillYearReportDetail BillYearReportDetail_ = new BillYearReportDetail(year, billin_Count, billin_Value, billin_Pays_Value
            //                , billm_Count, billm_Value, billm_Pays_Value
            //                , billout_Count, billout_Value, billout_Pays_Value);
            //            BillYearReportDetailList.Add(BillYearReportDetail_);
            //        }
            //        return BillYearReportDetailList;
            //    }
            //    catch (Exception ee)
            //    {
            //        MessageBox.Show(ee.Message);
            //        MessageBox.Show("فشل جلب تقرير السنوات التفصيلي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return BillYearReportDetailList;
            //    }
            //}
        }
        internal class ReportSellsSQL
        {
            DatabaseInterface DB;
    
            internal ReportSellsSQL(DatabaseInterface db)
            {
                DB = db;

            }

            #region SellReports
            internal List<Report_Sells_Day_ReportDetail > Get_Report_Sells_Day_ReportDetail(int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group (DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;
                    row["Day"] = day;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Day_ReportDetail ,
                       para);

                    return Report_Sells_Day_ReportDetail.Get_Report_Sells_Day_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Day_ReportDetail:" + ee.Message);

                }
            }
            internal Report_Sells_Month_ReportDetail Get_Report_Sells_Day_Report(int year, int month, int day)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;
                    row["Day"] = day;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Day_Report,
                       para);

                    return Report_Sells_Month_ReportDetail.Get_Report_Sells_Month_ReportDetail_List_From_DataTable  (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Day_Report:" + ee.Message);

                }
            }
            internal List <Report_Sells_Month_ReportDetail> Get_Report_Sells_Month_ReportDetail(int year, int month)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Month_ReportDetail ,
                       para);

                    return Report_Sells_Month_ReportDetail.Get_Report_Sells_Month_ReportDetail_List_From_DataTable  (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Month_ReportDetail:" + ee.Message);

                }
            }
            internal Report_Sells_Year_ReportDetail Get_Report_Sells_Month_Report(int year, int month)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Month_Report ,
                       para);

                    return Report_Sells_Year_ReportDetail.Get_Report_Sells_Year_ReportDetail_List_From_DataTable   (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Month_Report:" + ee.Message);

                }
            }
            internal List<Report_Sells_Year_ReportDetail> Get_Report_Sells_Year_ReportDetail(int year)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Year_ReportDetail,
                       para);

                    return Report_Sells_Year_ReportDetail.Get_Report_Sells_Year_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Year_ReportDetail:" + ee.Message);

                }
            }
            internal Report_Sells_YearRange_ReportDetail Get_Report_Sells_Year_Report(int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Year_Report,
                       para);

                    return Report_Sells_YearRange_ReportDetail.Get_Report_Sells_YearRange_ReportDetail_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Year_Report:" + ee.Message);

                }
            }
            internal List<Report_Sells_YearRange_ReportDetail> Get_Report_Sells_YearRange_ReportDetail(int min_year,int max_year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year1", typeof(int));
                    para.Columns.Add("Year2", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year1"] = min_year ;
                    row["Year2"] = max_year ;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_YearRange_ReportDetail,
                       para);

                    return Report_Sells_YearRange_ReportDetail.Get_Report_Sells_YearRange_ReportDetail_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_YearRange_ReportDetail:" + ee.Message);

                }
            }
            #endregion
            //internal List<BillDayReportDetail> GetBillReport_Details_InMonth(string year, string month)
            //{
            //    List<BillDayReportDetail> BillDayReportDetailList = new List<BillDayReportDetail>();
            //    try
            //    {

            //        DataTable table = DB.GetData(
            //            " select * from "
            //            + AccountBillTables.BillMonthReport_Details
            //            + "("
            //            + year + ","
            //            + month
            //            + ")");
            //        for (int i = 0; i < table.Rows.Count; i++)
            //        {
            //            int dayid = Convert.ToInt32(table.Rows[i][0].ToString());
            //            DateTime daydate = Convert.ToDateTime(table.Rows[i][1].ToString());
            //            int billin_Count = Convert.ToInt32(table.Rows[i][2].ToString());
            //            string billin_Value = table.Rows[i][3].ToString();
            //            string billin_Pays_Value = table.Rows[i][4].ToString();
            //            int billm_Count = Convert.ToInt32(table.Rows[i][5].ToString());
            //            string billm_Value = table.Rows[i][6].ToString();
            //            string billm_Pays_Value = table.Rows[i][7].ToString();
            //            int billout_Count = Convert.ToInt32(table.Rows[i][8].ToString());
            //            string billout_Value = table.Rows[i][9].ToString();
            //            string billout_Pays_Value = table.Rows[i][10].ToString();

            //            BillDayReportDetail billreportdayDetail = new BillDayReportDetail(dayid, daydate, billin_Count, billin_Value, billin_Pays_Value
            //                , billm_Count, billm_Value, billm_Pays_Value
            //                , billout_Count, billout_Value, billout_Pays_Value);
            //            BillDayReportDetailList.Add(billreportdayDetail);
            //        }
            //        return BillDayReportDetailList;
            //    }
            //    catch
            //    {
            //        MessageBox.Show("فشل جلب تقرير الشهر التفصيلي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return BillDayReportDetailList;
            //    }
            //}
            //internal List<BillMonthReportDetail> GetBillReport_Details_InYear(string year)
            //{
            //    List<BillMonthReportDetail> BillMonthReportDetailList = new List<BillMonthReportDetail>();
            //    try
            //    {
            //        DataTable table = DB.GetData(
            //            " select *from "
            //            + AccountBillTables.BillYearReport_Details
            //            + "("
            //            + year
            //            + ")");
            //        for (int i = 0; i < table.Rows.Count; i++)
            //        {
            //            int monthid = Convert.ToInt32(table.Rows[i][0].ToString());
            //            string month = table.Rows[i][1].ToString();
            //            int billin_Count = Convert.ToInt32(table.Rows[i][2].ToString());
            //            string billin_Value = table.Rows[i][3].ToString();
            //            string billin_Pays_Value = table.Rows[i][4].ToString();
            //            int billm_Count = Convert.ToInt32(table.Rows[i][5].ToString());
            //            string billm_Value = table.Rows[i][6].ToString();
            //            string billm_Pays_Value = table.Rows[i][7].ToString();
            //            int billout_Count = Convert.ToInt32(table.Rows[i][8].ToString());
            //            string billout_Value = table.Rows[i][9].ToString();
            //            string billout_Pays_Value = table.Rows[i][10].ToString();

            //            BillMonthReportDetail billmonthreportdetail = new BillMonthReportDetail(monthid, month, billin_Count, billin_Value, billin_Pays_Value
            //                , billm_Count, billm_Value, billm_Pays_Value
            //                , billout_Count, billout_Value, billout_Pays_Value);
            //            BillMonthReportDetailList.Add(billmonthreportdetail);
            //        }
            //        return BillMonthReportDetailList;
            //    }
            //    catch
            //    {
            //        MessageBox.Show("فشل جلب تقرير السنة التفصيلي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return BillMonthReportDetailList;
            //    }
            //}
            //internal List<BillYearReportDetail> GetBillReport_Details_InYearRange(string year1, string year2)
            //{
            //    List<BillYearReportDetail> BillYearReportDetailList = new List<BillYearReportDetail>();
            //    try
            //    {

            //        DataTable table = DB.GetData(
            //            " select * from "
            //            + AccountBillTables.BillYearRangeReport_Details
            //            + "("
            //            + year1 + ","
            //            + year2
            //            + ")");
            //        for (int i = 0; i < table.Rows.Count; i++)
            //        {
            //            int year = Convert.ToInt32(table.Rows[i][0].ToString());
            //            int billin_Count = Convert.ToInt32(table.Rows[i][1].ToString());
            //            string billin_Value = table.Rows[i][2].ToString();
            //            string billin_Pays_Value = table.Rows[i][3].ToString();
            //            int billm_Count = Convert.ToInt32(table.Rows[i][4].ToString());
            //            string billm_Value = table.Rows[i][5].ToString();
            //            string billm_Pays_Value = table.Rows[i][6].ToString();
            //            int billout_Count = Convert.ToInt32(table.Rows[i][7].ToString());
            //            string billout_Value = table.Rows[i][8].ToString();
            //            string billout_Pays_Value = table.Rows[i][9].ToString();

            //            BillYearReportDetail BillYearReportDetail_ = new BillYearReportDetail(year, billin_Count, billin_Value, billin_Pays_Value
            //                , billm_Count, billm_Value, billm_Pays_Value
            //                , billout_Count, billout_Value, billout_Pays_Value);
            //            BillYearReportDetailList.Add(BillYearReportDetail_);
            //        }
            //        return BillYearReportDetailList;
            //    }
            //    catch (Exception ee)
            //    {
            //        MessageBox.Show(ee.Message);
            //        MessageBox.Show("فشل جلب تقرير السنوات التفصيلي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return BillYearReportDetailList;
            //    }
            //}
        }
        internal class ReportMaintenanceOPRsSQL
        {
            DatabaseInterface DB;
            internal ReportMaintenanceOPRsSQL(DatabaseInterface db)
            {
                DB = db;

            }


            #region MaintenanceOPRReports

            internal List<Report_MaintenanceOPRs_Day_ReportDetail> Get_Report_MaintenanceOPRs_Day_ReportDetail(int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group (DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;
                    row["Day"] = day;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Day_ReportDetail,
                       para);

                    return Report_MaintenanceOPRs_Day_ReportDetail.Get_Report_Buys_Day_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Day_ReportDetail:" + ee.Message);

                }

            }
            internal Report_MaintenanceOPRs_Month_ReportDetail Get_Report_MaintenanceOPRs_Day_Report(int year, int month, int day)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));
                    para.Columns.Add("Day", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;
                    row["Day"] = day;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Day_Report,
                       para);

                    return Report_MaintenanceOPRs_Month_ReportDetail.Get_Report_MaintenanceOPRs_Month_ReportDetail_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Day_ReportDetail:" + ee.Message);

                }
            }
            internal List<Report_MaintenanceOPRs_Month_ReportDetail> Get_Report_MaintenanceOPRs_Month_ReportDetail(int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Month_ReportDetail,
                       para);

                    return Report_MaintenanceOPRs_Month_ReportDetail.Get_Report_MaintenanceOPRs_Month_ReportDetail_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Month_ReportDetail:" + ee.Message);

                }
            }
            internal Report_MaintenanceOPRs_Year_ReportDetail Get_Report_MaintenanceOPRs_Month_Report(int year, int month)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));
                    para.Columns.Add("Month", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;
                    row["Month"] = month;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Month_Report,
                       para);

                    return Report_MaintenanceOPRs_Year_ReportDetail.Get_Report_MaintenanceOPRs_Year_ReportDetail_List_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Month_Report:" + ee.Message);

                }
            }
            internal List<Report_MaintenanceOPRs_Year_ReportDetail> Get_Report_MaintenanceOPRs_Year_ReportDetail(int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Year_ReportDetail,
                       para);

                    return Report_MaintenanceOPRs_Year_ReportDetail.Get_Report_MaintenanceOPRs_Year_ReportDetail_List_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Year_ReportDetail:" + ee.Message);

                }
            }
            internal Report_MaintenanceOPRs_YearRange_ReportDetail Get_Report_MaintenanceOPRs_Year_Report(int year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year"] = year;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Year_Report,
                       para);

                    return Report_MaintenanceOPRs_YearRange_ReportDetail.Get_Report_MaintenanceOPRs_YearRange_ReportDetail_From_DataTable (t)[0];

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Year_Report:" + ee.Message);

                }
            }
            internal List<Report_MaintenanceOPRs_YearRange_ReportDetail> Get_Report_MaintenanceOPRs_YearRange_ReportDetail(int min_year, int max_year)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DataTable para = new DataTable();
                    para.Columns.Add("Year1", typeof(int));
                    para.Columns.Add("Year2", typeof(int));


                    DataRow row = para.NewRow();
                    row["Year1"] = min_year ;
                    row["Year2"] = max_year ;

                    para.Rows.Add(row);
                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_YearRange_ReportDetail,
                       para);

                    return Report_MaintenanceOPRs_YearRange_ReportDetail.Get_Report_MaintenanceOPRs_YearRange_ReportDetail_From_DataTable (t);

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_YearRange_ReportDetail:" + ee.Message);

                }
            }
            #endregion

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
                    MessageBox.Show("فشل جلب بيانات العملة"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null ;
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
                    MessageBox.Show("فشل جلبالعملة المرجعية:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            internal bool  AddCurrency(string name,string symbol,double referncefactor,bool Disable)
            {

                try
                {
                    if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(
                        " insert into "
                    + CurrencyTable.TableName 
                    + "("
                    + CurrencyTable.CurrencyName
                    + ","
                    + CurrencyTable.CurrencySymbol
                    + ","
                    + CurrencyTable.ReferenceFactor
                     + ","
                    + CurrencyTable.ReferenceCurrencyID
                      + ","
                    + CurrencyTable.Disable 
                    + ")"
                    + "values"
                    + "("
                    + "'" + name + "'"
                    + ","
                      + "'" + symbol  + "'"
                    + ","
                    + referncefactor 
                     + ","
                    + GetReferenceCurrency ().CurrencyID
                     + ","
                    + (Disable ?1:0)
                    + ")"
                    );
                    DB.AddLog(
                             DatabaseInterface.Log.LogType.INSERT  
                             , DatabaseInterface.Log.Log_Target.Currency 
                             , ""
                           , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Currency
                            , ""
                          , false , ee.Message );
                    MessageBox.Show("AddCurrency:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool UpdateCurrency(uint currencyid,string newname, string symbol, double referncefactor,bool disable)
            {

                   try
                {
                    if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    Currency currency = GetCurrencyINFO_ByID(currencyid);
                    if (currency.ReferenceCurrencyID == null) throw new Exception("لا يمكن حذف او تعديل العملة المرجعلية");
                    DB.ExecuteSQLCommand( "update  "
                    + CurrencyTable.TableName
                    + " set "
                    + CurrencyTable.CurrencyName+"='"+newname +"',"
                    + CurrencyTable.CurrencySymbol + "='" + symbol + "',"
                    + CurrencyTable.ReferenceFactor + "="+referncefactor +","
                    + CurrencyTable.Disable  + "=" + (disable ?1:0)
                    + " where "
                    + CurrencyTable.CurrencyID  +"="+currencyid
                    );
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Currency
                            , ""
                          , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                           DatabaseInterface.Log.LogType.UPDATE
                           , DatabaseInterface.Log.Log_Target.Currency
                           , ""
                         , false ,ee.Message );
                    MessageBox.Show("UpdateCurrency"+ee.Message , "خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool DeleteCurrency(uint currencyid)
            {
                try
                {
                    Currency currency = GetCurrencyINFO_ByID(currencyid);
                    if (currency.ReferenceCurrencyID == null) throw new Exception("لا يمكن حذف او تعديل العملة المرجعلية");

                    if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( "delete from   "
                    + CurrencyTable.TableName
                    + " where "
                    + CurrencyTable.CurrencyID + "=" + currencyid
                    );
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Currency
                            , ""
                          , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                               DatabaseInterface.Log.LogType.DELETE 
                               , DatabaseInterface.Log.Log_Target.Currency
                               , ""
                             , false , ee.Message );
                    MessageBox.Show("DeleteCurrency", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
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
                    MessageBox.Show("فشل جلب قائمة العملات"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
               
            }
            internal bool Add_ExchageOPR(uint moneyboxID, DateTime exchangeoprdate,Currency sourcecurrency,double source_exchangerate,double outmoneyvalue,Currency targetcurrency,double target_exchangerate,string notes)
            {
                try
                {
            
                    MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(moneyboxID);
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (sourcecurrency .CurrencyID == targetcurrency .CurrencyID)
                    {
                        throw new Exception("العملة الهدف يجب ان تكون مختلفة عن العملة المصدر");
                    }
                    double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(moneyboxID,sourcecurrency.CurrencyID);
                    if (money_amount < outmoneyvalue) throw new Exception("كمية المال غير كافية لنتفيذ هذه العملية");
                    if (sourcecurrency.ReferenceCurrencyID == null && source_exchangerate != 1) throw new Exception("سعر صرف العملة المرجعية يجب ان يكون واحد");
                    if (targetcurrency .ReferenceCurrencyID == null && target_exchangerate != 1) throw new Exception("سعر صرف العملة المرجعية يجب ان يكون واحد");

                    if (outmoneyvalue  <= 0 || source_exchangerate <=0 ||target_exchangerate <=0) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");

                    DB.ExecuteSQLCommand( " insert into "
                    + ExchangeOPRTable.TableName
                    + "("
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
                    + ")"
                    + "values"
                    + "("
                    +"'" + exchangeoprdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + sourcecurrency .CurrencyID 
                    + ","
                    +source_exchangerate  
                    + ","
                    + outmoneyvalue 
                    + ","
                    + targetcurrency .CurrencyID
                    + ","
                    + target_exchangerate 
                    + ","
                    + "'"+  notes + "'"
                      + ","
                    + moneyboxID 
                    + ")"
                    );
                    DB.AddLog(
                           DatabaseInterface.Log.LogType.INSERT 
                           , DatabaseInterface.Log.Log_Target.ExchangeOPR 
                           , ""
                         , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.INSERT
                          , DatabaseInterface.Log.Log_Target.ExchangeOPR 
                          , ""
                        , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add_ExchageOPR:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Update_ExchageOPR( uint exchangeoprid,DateTime exchangeoprdate, Currency sourcecurrency, double source_exchangerate, double outmoneyvalue, Currency targetcurrency, double target_exchangerate, string notes)
            {
                try
                {
                    if (sourcecurrency.CurrencyID == targetcurrency.CurrencyID)
                    {
                        throw new Exception("العملة الهدف يجب ان تكون مختلفة عن العملة المصدر");
                    }
                    ExchangeOPR exchangeopr = GetExchangeOPR_INFO_BYID(exchangeoprid);

                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, exchangeopr._MoneyBox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (outmoneyvalue <= 0 || source_exchangerate <= 0 || target_exchangerate <= 0) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");

                    double money_amount_OUT = new DataBaseFunctions(DB).Account_GetAmountMoney(exchangeopr._MoneyBox.BoxID, exchangeopr.SourceCurrency.CurrencyID);
                    double money_amount_IN = new DataBaseFunctions(DB).Account_GetAmountMoney(exchangeopr._MoneyBox.BoxID, exchangeopr.TargetCurrency.CurrencyID);

                    double exchangeopr_money_IN = (exchangeopr.TargetExchangeRate / exchangeopr.SourceExchangeRate) * exchangeopr.OutMoneyValue;
                    double exchangeopr_money_OUT = exchangeopr.OutMoneyValue;

                    double operation_money_IN = (exchangeopr.TargetExchangeRate / exchangeopr.SourceExchangeRate) * exchangeopr.OutMoneyValue;
                    double operation_money_OUT = exchangeopr.OutMoneyValue;

                    if (money_amount_IN + operation_money_IN - exchangeopr_money_IN < 0) throw new Exception("حذف عملية الصرف سيؤدي الى جعل كمية المال للعملة الهدف اصغر من الصفر");
                    if (money_amount_OUT - operation_money_OUT + exchangeopr_money_OUT < 0) throw new Exception("حذف عملية الصرف سيؤدي الى جعل كمية المال للعملة المصدر اصغر من الصفر");

                    if (sourcecurrency.ReferenceCurrencyID == null && source_exchangerate != 1) throw new Exception("سعر صرف العملة المرجعية يجب ان يكون واحد");
                    if (targetcurrency.ReferenceCurrencyID == null && target_exchangerate != 1) throw new Exception("سعر صرف العملة المرجعية يجب ان يكون واحد");

                    DB.ExecuteSQLCommand( "update  "
                    + ExchangeOPRTable.TableName
                    + " set "
                    + ExchangeOPRTable.ExchangeOprDate  + "="+"'" + exchangeoprdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                     + ExchangeOPRTable.SourceCurrencyID + "=" + sourcecurrency.CurrencyID
                    + ","
                     + ExchangeOPRTable.SourceExchangeRate + "=" + source_exchangerate
                    + ","
                    + ExchangeOPRTable.OutMoneyValue   + "=" + outmoneyvalue  
                    + ","
                      + ExchangeOPRTable.TargetCurrencyID + "=" + targetcurrency.CurrencyID 
                    + ","
                     + ExchangeOPRTable.TargetExchangeRate + "=" + target_exchangerate
                    + ","
                    + ExchangeOPRTable.Notes   + "='" + notes + "'"
                    + " where "
                    + ExchangeOPRTable.ExchangeOprID + "=" + exchangeoprid
                    );
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.UPDATE 
                          , DatabaseInterface.Log.Log_Target.ExchangeOPR 
                          , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE
                         , DatabaseInterface.Log.Log_Target.ExchangeOPR 
                         , ""
                       , false ,ee.Message );
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Delete_ExchageOPR(uint exchangeoprid)
            {
                try
                {
                    ExchangeOPR exchangeopr = GetExchangeOPR_INFO_BYID(exchangeoprid);
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, exchangeopr._MoneyBox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    double money_amount_OUT = new DataBaseFunctions(DB).Account_GetAmountMoney(exchangeopr._MoneyBox.BoxID, exchangeopr.SourceCurrency .CurrencyID);
                    double money_amount_IN = new DataBaseFunctions(DB).Account_GetAmountMoney(exchangeopr._MoneyBox.BoxID, exchangeopr.TargetCurrency .CurrencyID);

                    double exchangeopr_money_IN = (exchangeopr.TargetExchangeRate /exchangeopr.SourceExchangeRate)* exchangeopr.OutMoneyValue;
                    double exchangeopr_money_OUT = exchangeopr.OutMoneyValue;

                    if (money_amount_IN - exchangeopr_money_IN < 0) throw new Exception("حذف عملية الصرف سيؤدي الى جعل كمية المال للعملة الهدف اصغر من الصفر");
                    DB.ExecuteSQLCommand( "delete from   "
                    + ExchangeOPRTable.TableName
                    + " where "
                    + ExchangeOPRTable.ExchangeOprID  + "=" + exchangeoprid 
                    );
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.DELETE 
                          , DatabaseInterface.Log.Log_Target.ExchangeOPR
                          , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.DELETE 
                         , DatabaseInterface.Log.Log_Target.ExchangeOPR
                         , ""
                       , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal List<ExchangeOPR> Get_All_ExchangeOPRList(MoneyBox moneybox)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID,moneybox ))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

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
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
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
            internal bool Add_PayIN(uint moneyboxid, DateTime payindate, Operation Operation_, string description,  double value, double exchangerate, Currency currency, string notes)
            {
                try
                {
         
                    MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(moneyboxid);
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    string operationtype_Str, operationid_Str;
                    if(Operation_ ==null )
                    {
                        operationtype_Str = "null";
                        operationid_Str = "null";
                    }
                    else
                    {
                        operationtype_Str = Operation_.OperationType .ToString(); 
                        operationid_Str = Operation_.OperationID.ToString();
                    }
                    if (value  <= 0 || exchangerate  <= 0 ) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");
                    DB.ExecuteSQLCommand( " insert into "
                    + PayINTable.TableName
                    + "("
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
                    + ")"
                    + "values"
                    + "("
                     +"'" + payindate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + operationtype_Str
                    + ","
                    + operationid_Str 
                    + ","
                    + "'"+description +"'"
                    + ","
                    + value
                    + ","
                    + exchangerate
                    + ","
                    + currency .CurrencyID
                    + ","
                    + "'"+notes+"'"
                    + ","
                    +  moneyboxid 
                    + ")"
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.INSERT 
                         , DatabaseInterface.Log.Log_Target.PayIN 
                         , ""
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.PayIN
                            , ""
                          , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Update_PayIN(uint payinid, DateTime payindate, Operation Operation_, string description, double value, double exchangerate, Currency currency, string notes)
            {
                try
                {
                    PayIN payin = GetPayIN_INFO_BYID(payinid);

                    MoneyBox moneybox = payin ._MoneyBox ;
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(payin._MoneyBox.BoxID,payin._Currency.CurrencyID);
                    if (money_amount - payin.Value + value < 0) throw new Exception("عملية التعديل ستؤدي الى جعل كمية المال في الصندوق للعملة اصغر من الصفر");
                    string operationtype_Str, operationid_Str;
                    if (Operation_ == null)
                    {
                        operationtype_Str = "null";
                        operationid_Str = "null";
                    }
                    else
                    {
                        operationtype_Str = Operation_.OperationType.ToString();
                        operationid_Str = Operation_.OperationID.ToString();
                    }
                    if (value <= 0 || exchangerate <= 0) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");

                    DB.ExecuteSQLCommand( "update  "
                    + PayINTable.TableName
                    + " set "
                    +PayINTable .PayOprDate+"=" +"'" + payindate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + PayINTable.OperationType  + "=" + operationtype_Str
                    + ","
                    + PayINTable.OperationID  + "=" + operationid_Str
                    + ","
                    + PayINTable.PayDescription  + "='" + description+"'"
                    + ","
                    + PayINTable.Value + "=" + value 
                    + ","
                    + PayINTable.ExchangeRate  + "=" + exchangerate 
                    + ","
                    + PayINTable.CurrencyID  + "=" + currency .CurrencyID
                    + ","
                    + PayINTable.Notes +"='" + notes  + "'"
                    + " where "
                    + PayINTable.PayOprID + "=" + payinid 
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE 
                         , DatabaseInterface.Log.Log_Target.PayIN
                         , ""
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE
                         , DatabaseInterface.Log.Log_Target.PayIN
                         , ""
                       , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_PayIN:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Delete_PayIN(uint payinid)
            {
                try
                {
                    PayIN payin = GetPayIN_INFO_BYID(payinid);
                    MoneyBox moneybox =payin ._MoneyBox;
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(payin._MoneyBox.BoxID, payin._Currency.CurrencyID);
                    if (money_amount - payin.Value  < 0) throw new Exception("عملية الحذف ستؤدي الى جعل كمية المال في الصندوق للعملة اصغر من الصفر");
                    DB.ExecuteSQLCommand( "delete from   "
                    + PayINTable.TableName
                    + " where "
                    + PayINTable.PayOprID + "=" + payinid 
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.DELETE 
                         , DatabaseInterface.Log.Log_Target.PayIN
                         , ""
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.DELETE
                         , DatabaseInterface.Log.Log_Target.PayIN
                         , ""
                       , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
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
                    System.Windows.Forms.MessageBox.Show("GetPayINList:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("GetPayINList_As_Money_Currenncy:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return payinList;
            }

            internal List<PayIN> Get_All_PayINList(MoneyBox moneybox)
            {
                try
                {
  
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

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
                    System.Windows.Forms.MessageBox.Show("Get_All_PayINList:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            //internal List<Money_Currency> Get_All_PayINList_As_Money_Currency_INDAY(int year ,int month,int day)
            //{
            //    List<Money_Currency > payinList = new List<Money_Currency>();
            //    try
            //    {
                   
            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "
            //        + PayINTable.Value
            //        + ","
            //        + PayINTable.ExchangeRate
            //        + ","
            //        + PayINTable.CurrencyID
            //        + " from   "
            //        + PayINTable.TableName
            //        +" where "
            //         + " strftime('%Y', " + PayINTable.PayOprDate + ") = '" + year + "'"
            //        + " and "
            //        + " strftime('%m', " + PayINTable.PayOprDate + ") = '" + month.ToString("00") + "'"
            //        + " and "
            //        + " strftime('%d', " + PayINTable.PayOprDate + ") = '" + day.ToString("00") + "'"
            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {
            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            payinList.Add(new Money_Currency (currency, value, exchangerate));
            //        }

            //        return payinList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PayINList_As_Money_Currency:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            //internal List<Money_Currency> Get_All_PayINList_As_Money_Currency_IN_Month(int year, int month)
            //{
            //    List<Money_Currency> payinList = new List<Money_Currency>();
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "
            //        + PayINTable.Value
            //        + ","
            //        + PayINTable.ExchangeRate
            //        + ","
            //        + PayINTable.CurrencyID
            //        + " from   "
            //        + PayINTable.TableName
            //          + " where "
            //         + " strftime('%Y', " + PayINTable.PayOprDate + ") = '" + year + "'"
            //        + " and "
            //        + " strftime('%m', " + PayINTable.PayOprDate + ") = '" + month.ToString("00") + "'"



            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {
            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            payinList.Add(new Money_Currency(currency, value, exchangerate));
            //        }

            //        return payinList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PayINList_As_Money_Currency_IN_Month:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            //internal List<Money_Currency> Get_All_PayINList_As_Money_Currency_IN_Year(int year)
            //{
            //    List<Money_Currency> payinList = new List<Money_Currency>();
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "
            //        + PayINTable.Value
            //        + ","
            //        + PayINTable.ExchangeRate
            //        + ","
            //        + PayINTable.CurrencyID
            //        + " from   "
            //        + PayINTable.TableName
            //      + " where "
            //         + " strftime('%Y', " + PayINTable.PayOprDate + ") = '" + year + "'"


            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {
            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            payinList.Add(new Money_Currency(currency, value, exchangerate));
            //        }

            //        return payinList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PayINList_As_Money_Currency_IN_Year:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            //internal List<Money_Currency> Get_All_PayINList_As_Money_Currency_IN_YearRange(int year1, int year2)
            //{
            //    int min_year, max_year;
            //    if(year1>year2)
            //    {
            //        min_year = year2;
            //        max_year = year1;
            //    }else
            //    {
            //        min_year = year1;
            //        max_year = year2;
            //    }
            //    List<Money_Currency> payinList = new List<Money_Currency>();
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "
            //        + PayINTable.Value
            //        + ","
            //        + PayINTable.ExchangeRate
            //        + ","
            //        + PayINTable.CurrencyID
            //        + " from   "
            //        + PayINTable.TableName
            //        + " where "
            //        + " strftime('%Y', " + PayINTable.PayOprDate + ")> '" + min_year + "'"
            //        + " and "
            //        + " strftime('%Y', " + PayINTable.PayOprDate + ") < '" + max_year  + "'"


            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {
            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            payinList.Add(new Money_Currency(currency, value, exchangerate));
            //        }

            //        return payinList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PayINList_As_Money_Currency_IN_YearRange:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}

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
                        if(operation !=null && operation .OperationType ==Operation .Employee_PayOrder )
                        {
                            Company.Objects.EmployeePayOrder EmployeePayOrder_ = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(operation.OperationID);
                            if (EmployeePayOrder_ == null) throw new Exception("EmployeePayOrder_ is NULL");
                            return   new PayOUT(moneybox, payoutid, payindate, EmployeePayOrder_, description, value, exchangerate, currency, notes);

                        }
                        else
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
            internal bool Add_PayOUT(uint moneyboxid, DateTime payoutdate, Operation operation, string description, double value, double exchangerate, Currency currency, string notes)
            {
                try
                {
          
                    MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(moneyboxid);
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(moneyboxid,currency.CurrencyID);
                    if (money_amount -   value < 0) throw new Exception("لاتوجد كمية مال كافية في الصندوق لاجراء هذه العملية");
                    string operationtype_str, operationid_str;
                    if(operation ==null )
                    {
                        operationtype_str = "null";
                        operationid_str = "null";
                    }
                    else
                    {
                        operationtype_str = operation.OperationType.ToString() ;
                        operationid_str = operation.OperationID .ToString();
                    }
                    if (value <= 0 || exchangerate <= 0) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");

                    DB.ExecuteSQLCommand( " insert into "
                    + PayOUTTable.TableName
                    + "("
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
                    + ")"
                    + "values"
                    + "("
                    + "'" + payoutdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + operationtype_str
                    + ","
                    + operationid_str 
                    + ","
                    + "'" + description + "'"
                    + ","
                    + value
                    + ","
                    + exchangerate
                    + ","
                    + currency.CurrencyID
                    + ","
                    + "'" + notes + "'"
                    + ","
                    +moneyboxid 
                    + ")"
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.INSERT 
                         , DatabaseInterface.Log.Log_Target.PayOUT
                         , ""
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.INSERT
                        , DatabaseInterface.Log.Log_Target.PayOUT
                        , ""
                      , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add_PayOUT:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Update_PayOUT(uint payoutid, DateTime payoutdate, Operation Operation_,  string description, double value, double exchangerate, Currency currency, string notes)
            {
                try
                {
                    PayOUT payout = GetPayOUT_INFO_BYID(payoutid);
                    MoneyBox moneybox = payout ._MoneyBox ;
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(payout._MoneyBox.BoxID , payout._Currency.CurrencyID);
                    if (money_amount +payout .Value - value < 0) throw new Exception("عملية التعديل ستؤدي الى جعل كمية المال في الصندوق للعملة اصغر من الصفر");
                    string operationtype_Str, operationid_Str;
                    if (Operation_ == null)
                    {
                        operationtype_Str = "null";
                        operationid_Str = "null";
                    }
                    else
                    {
                        operationtype_Str = Operation_.OperationType.ToString();
                        operationid_Str = Operation_.OperationID.ToString();
                    }
                    if (value <= 0 || exchangerate <= 0) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");
                   
                    DB.ExecuteSQLCommand( "update  "
                    + PayOUTTable.TableName
                    + " set "
                    + PayOUTTable.PayOprDate + "=" + "'" + payoutdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                      + ","
                    + PayOUTTable.OperationType+ "=" + operationtype_Str
                    + ","
                    + PayOUTTable.OperationID+ "=" + operationid_Str
                    + ","
                    + PayOUTTable.PayDescription + "='" + description + "'"
                    + ","
                    + PayOUTTable.Value + "=" + value
                    + ","
                    + PayOUTTable.ExchangeRate + "=" + exchangerate
                    + ","
                      + PayOUTTable.CurrencyID  + "=" + currency .CurrencyID
                    + ","
                    + PayOUTTable.Notes + "='" + notes + "'"
                    + " where "
                    + PayOUTTable.PayOprID + "=" + payoutid 
                    );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.UPDATE 
                        , DatabaseInterface.Log.Log_Target.PayOUT
                        , ""
                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.UPDATE
                       , DatabaseInterface.Log.Log_Target.PayOUT
                       , ""
                     , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_PayOUT:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Delete_PayOUT(uint payoutid)
            {
                try
                {
                    PayOUT  payout = GetPayOUT_INFO_BYID(payoutid);
                    MoneyBox moneybox = payout ._MoneyBox ;
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(payout._MoneyBox.BoxID, payout._Currency.CurrencyID);
                    if (money_amount + payout.Value < 0) throw new Exception("عملية التعديل ستؤدي الى جعل كمية المال في الصندوق للعملة اصغر من الصفر");
                    DB.ExecuteSQLCommand("delete from   "
                    + PayOUTTable.TableName
                    + " where "
                    + PayOUTTable.PayOprID + "=" + payoutid
                    );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE 
                       , DatabaseInterface.Log.Log_Target.PayOUT
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE
                      , DatabaseInterface.Log.Log_Target.PayOUT
                      , ""
                    , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Delete_PayOUT:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal List<PayOUT > GetPaysOUT_List(Operation Operation_)
            {
                List<PayOUT> PayOUTList = new List<PayOUT>();

                try
                {
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
                    + PayOUTTable.OperationType   + "=" + Operation_ .OperationType
                    + " and "
                    + PayOUTTable.OperationID  + "=" + Operation_.OperationID 
                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint payinid = Convert.ToUInt32(t.Rows[i][PayOUTTable.PayOprID ].ToString());
                        DateTime payindate = Convert.ToDateTime(t.Rows[i][PayOUTTable.PayOprDate ].ToString());
                        string description = t.Rows[i][PayOUTTable.PayDescription ].ToString();
                        double value = Convert.ToDouble(t.Rows[i][PayOUTTable.Value ].ToString());
                        double exchangerate = Convert.ToDouble(t.Rows[i][PayOUTTable.ExchangeRate ].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][PayOUTTable.CurrencyID ].ToString()));
                        string notes = t.Rows[i][PayOUTTable.Notes ].ToString();
                        MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(Convert.ToUInt32(t.Rows[i][PayOUTTable.MoneyBoxID]));

                        if (Operation_ != null && Operation_.OperationType ==Operation .Employee_PayOrder  )
                        {
                            Company.Objects.EmployeePayOrder EmployeePayOrder_ = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(Operation_.OperationID);
                            PayOUTList.Add(new PayOUT(moneybox, payinid, payindate, EmployeePayOrder_, description, value, exchangerate, currency, notes));

                        }
                        else 
                        PayOUTList.Add(new PayOUT(moneybox , payinid, payindate, new OperationSQL(DB).GetOperationBill(Operation_), description, value, exchangerate, currency, notes));
                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetPaysOUT_List:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return PayOUTList;

            }
            internal List<PayOUT> Get_All_PaysOUT_List(MoneyBox moneybox)
            {
                try
                {
       
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

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
                        Bill  bill;
                        uint operationid;
                        if (uint.TryParse(t.Rows[i][7].ToString(), out operationid))
                        {
                             operationid = Convert.ToUInt32(t.Rows[i][7].ToString());
                            uint operationtype = Convert.ToUInt32(t.Rows[i][8].ToString());
                            if(operationtype ==Operation .Employee_PayOrder )
                            {
                                Company.Objects.EmployeePayOrder EmployeePayOrder_ = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(operationid);
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
                            bill  = null;
                            PayOUTList.Add(new PayOUT(moneybox, payinid, payindate, bill  , description, value, exchangerate, currency, notes));

                        }

                    }
                    return PayOUTList;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_All_PaysOUT_List:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            
            //internal List<Money_Currency > Get_All_PaysOUT_List_As_Money_Currency()
            //{
            //    List<Money_Currency> PayOUTList = new List<Money_Currency>();
            //    try
            //    {
                   
            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "

            //        + PayOUTTable.Value
            //        + ","
            //        + PayOUTTable.ExchangeRate
            //        + ","
            //        + PayOUTTable.CurrencyID

            //        + " from   "
            //        + PayOUTTable.TableName

            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {

            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));
    

            //            PayOUTList.Add(new Money_Currency (currency, value, exchangerate));
            //        }
            //        return PayOUTList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PaysOUT_List_As_Money_Currency:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            //internal List<Money_Currency> Get_All_PaysOUT_List_As_Money_Currency_IN_Day(int year,int month,int day)
            //{
            //    List<Money_Currency> PayOUTList = new List<Money_Currency>();
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "

            //        + PayOUTTable.Value
            //        + ","
            //        + PayOUTTable.ExchangeRate
            //        + ","
            //        + PayOUTTable.CurrencyID

            //        + " from   "
            //        + PayOUTTable.TableName
            //         + " where "
            //         + " strftime('%Y', " + PayOUTTable.PayOprDate + ") = '" + year + "'"
            //        + " and "
            //        + " strftime('%m', " + PayOUTTable.PayOprDate + ") = '" + month .ToString ("00")+ "'"
            //        + " and "
            //        + " strftime('%d', " + PayOUTTable.PayOprDate + ") = '" + day.ToString ("00") + "'"
            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {

            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            PayOUTList.Add(new Money_Currency(currency, value, exchangerate));
            //        }
            //        return PayOUTList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PaysOUT_List_As_Money_Currency_IN_Day:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            //internal List<Money_Currency> Get_All_PaysOUT_List_As_Money_Currency_IN_Month(int year, int month)
            //{
            //    List<Money_Currency> PayOUTList = new List<Money_Currency>();
            //    try
            //    {
         
            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "

            //        + PayOUTTable.Value
            //        + ","
            //        + PayOUTTable.ExchangeRate
            //        + ","
            //        + PayOUTTable.CurrencyID

            //        + " from   "
            //        + PayOUTTable.TableName
            //         + " where "
            //         + " strftime('%Y', " + PayOUTTable.PayOprDate + ") = '" + year + "'"
            //        + " and "
            //        + " strftime('%m', " + PayOUTTable.PayOprDate + ") = '" + month.ToString("00") + "'"
                  

            //      );
            //        MessageBox.Show(t.Rows .Count .ToString ());
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {

            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            PayOUTList.Add(new Money_Currency(currency, value, exchangerate));
            //        }
            //        return PayOUTList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PaysOUT_List_As_Money_Currency_IN_Month:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            //internal List<Money_Currency> Get_All_PaysOUT_List_As_Money_Currency_IN_Year(int year)
            //{
            //    List<Money_Currency> PayOUTList = new List<Money_Currency>();
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "

            //        + PayOUTTable.Value
            //        + ","
            //        + PayOUTTable.ExchangeRate
            //        + ","
            //        + PayOUTTable.CurrencyID

            //        + " from   "
            //        + PayOUTTable.TableName
            //            + " where "
            //         + " strftime('%Y', " + PayOUTTable.PayOprDate + ") = '" + year + "'"


            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {

            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            PayOUTList.Add(new Money_Currency(currency, value, exchangerate));
            //        }
            //        return PayOUTList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PaysOUT_List_As_Money_Currency_IN_Year:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            //internal List<Money_Currency> Get_All_PaysOUT_List_As_Money_Currency_IN_YearRange(int year1,int year2)
            //{
            //    int min_year, max_year;
            //    if (year1 > year2)
            //    {
            //        min_year = year2;
            //        max_year = year1;
            //    }
            //    else
            //    {
            //        min_year = year1;
            //        max_year = year2;
            //    }
            //    List<Money_Currency> PayOUTList = new List<Money_Currency>();
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "

            //        + PayOUTTable.Value
            //        + ","
            //        + PayOUTTable.ExchangeRate
            //        + ","
            //        + PayOUTTable.CurrencyID

            //        + " from   "
            //        + PayOUTTable.TableName
            //        + " where "
            //           + " strftime('%Y', " + PayOUTTable.PayOprDate + ")> '" + min_year + "'"
            //        + " and "
            //        + " strftime('%Y', " + PayOUTTable.PayOprDate + ") < '" + max_year + "'"



            //      );
            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {

            //            double value = Convert.ToDouble(t.Rows[i][0].ToString());
            //            double exchangerate = Convert.ToDouble(t.Rows[i][1].ToString());
            //            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][2].ToString()));


            //            PayOUTList.Add(new Money_Currency(currency, value, exchangerate));
            //        }
            //        return PayOUTList;
            //    }
            //    catch (Exception ee)
            //    {
            //        System.Windows.Forms.MessageBox.Show("Get_All_PaysOUT_List_As_Money_Currency_IN_Year:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}

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
            public bool AddMoneyBox(string MoneyBoxname)
            {
                try
                {
         
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) )) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" insert into "
                    + MoneyBoxTable.TableName
                    + "("
                    + MoneyBoxTable.MoneyBoxName
                    + ")"
                    + "values"
                    + "("
                    + "'" + MoneyBoxname + "'"

                    + ")"
                    );
                    DB.AddLog(
              DatabaseInterface.Log.LogType.INSERT
              , DatabaseInterface.Log.Log_Target.MoneyBox 
               , ""
               , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.MoneyBox 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("AddMoneyBox:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdateMoneyBox(uint MoneyBoxidid, string newname)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                    + MoneyBoxTable.TableName
                    + " set "
                       + MoneyBoxTable.MoneyBoxName + "='" + newname + "'"
                    + " where "
                    + MoneyBoxTable.MoneyBoxID + "=" + MoneyBoxidid
                    );

                    DB.AddLog(
                          DatabaseInterface.Log.LogType.UPDATE
                          , DatabaseInterface.Log.Log_Target.MoneyBox
                           , ""
                           , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE
                            , DatabaseInterface.Log.Log_Target.MoneyBox
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("UpdateMoneyBox:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteMoneyBox(uint MoneyBoxidid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from   "
                    + MoneyBoxTable.TableName
                    + " where "
                    + MoneyBoxTable.MoneyBoxID + "=" + MoneyBoxidid
                     );
                    DB.AddLog(
              DatabaseInterface.Log.LogType.DELETE
              , DatabaseInterface.Log.Log_Target.MoneyBox
               , ""
               , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE
                            , DatabaseInterface.Log.Log_Target.MoneyBox
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("DeleteMoneyBox:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
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
                    System.Windows.Forms.MessageBox.Show("GetMoneyBox_List:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

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
            internal bool Add_MoneyTransFormOPR( DateTime MoneyTransFormOPRdate, uint sourcemoneyboxid, uint targetmoneyboxid, double value, double exchangerate, Currency currency, string notes)
            {
                try
                {
               
                    MoneyBox moneybox =new MoneyBoxSQL(DB).GetMoneyBoxBYID(sourcemoneyboxid );
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (sourcemoneyboxid == targetmoneyboxid) throw new Exception("يجب ان يكون الصندوق الهدف غير الصندوق المصدر");
                    double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(sourcemoneyboxid , currency .CurrencyID);
                    if (money_amount < value ) throw new Exception("كمية المال غير كافية لنتفيذ هذه العملية");
                    if (currency.ReferenceCurrencyID == null && exchangerate  != 1) throw new Exception("سعر صرف العملة المرجعية يجب ان يكون واحد");
                    if (sourcemoneyboxid == targetmoneyboxid) throw new Exception("لا يمكن تحويل المال الى نفس الصندوق");
                    if (value <= 0 || exchangerate <= 0) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");

                    DB.ExecuteSQLCommand(" insert into "
                    + MoneyTransFormOPRTable.TableName
                    + "("
                    + MoneyTransFormOPRTable.Creator_UserID 
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
                    
                    + ")"
                    + "values"
                    + "("
                    + DB.__User.UserID
                    + ","
                     + "'" + MoneyTransFormOPRdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + sourcemoneyboxid 
                    + ","
                    + targetmoneyboxid 
                    + ","
                    + value
                    + ","
                    + exchangerate
                    + ","
                    + currency.CurrencyID
                    + ","
                    + "'" + notes + "'"
           
                    + ")"
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.INSERT
                         , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                         , "انشاء عملية تحويل"
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                            , "انشاء عملية تحويل"
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("Add_MoneyTransFormOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Update_MoneyTransFormOPR(uint MoneyTransFormOPRid, DateTime MoneyTransFormOPRdate, uint sourcemoneyboxid 
                , uint  targetmoneyboxid, double value, double exchangerate, Currency currency, string notes)
            {
                try
                {
                    MoneyTransFormOPR MoneyTransFormOPR_ = new MoneyTransFormOPRSQL(DB).GetMoneyTransFormOPR_INFO_BYID(MoneyTransFormOPRid);
                    MoneyBox moneybox = MoneyTransFormOPR_.SourceMoneyBox;
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    double money_amount_source = new DataBaseFunctions(DB).Account_GetAmountMoney(MoneyTransFormOPR_.SourceMoneyBox .BoxID, MoneyTransFormOPR_._Currency.CurrencyID);
                    double money_amount_target = new DataBaseFunctions(DB).Account_GetAmountMoney(MoneyTransFormOPR_.TargetMoneyBox .BoxID, MoneyTransFormOPR_._Currency.CurrencyID);

                    if (money_amount_source + MoneyTransFormOPR_.Value - value < 0) throw new Exception("عملية التعديل ستؤدي الى جعل كمية المال في الصندوق للعملة اصغر من الصفر");
                    if (money_amount_target - MoneyTransFormOPR_.Value + value < 0) throw new Exception("عملية التعديل ستؤدي الى جعل كمية المال في الصندوق للعملة اصغر من الصفر");

                    if (currency.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception("سعر صرف العملة المرجعية يجب ان يكون واحد");


                    if (MoneyTransFormOPR_.Confirm_User !=null && !DB.IS_Belong_To_Admin_Group(DB.__User.UserID))
                        throw new Exception("تم تاكيد عملية التحويل . يستطيع فقط المدراء تعديل بيانات العملية");

                    if (value <= 0 || exchangerate <= 0) throw new Exception("يجب أن تكون القيم المدخلة أكبر تماما من الصفر");

                    DB.ExecuteSQLCommand("update  "
                    + MoneyTransFormOPRTable.TableName
                    + " set "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRDate  + "=" + "'" + MoneyTransFormOPRdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + MoneyTransFormOPRTable.SourceMoneyBoxID + "=" + sourcemoneyboxid 
                    + ","
                    + MoneyTransFormOPRTable.TargetMoneyBoxID + "=" + targetmoneyboxid 
                    + ","
                    + MoneyTransFormOPRTable.Value + "=" + value
                    + ","
                    + MoneyTransFormOPRTable.ExchangeRate + "=" + exchangerate
                    + ","
                    + MoneyTransFormOPRTable.Notes + "='" + notes + "'"
                    + " where "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRID + "=" + MoneyTransFormOPRid
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE
                         , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                         , "تعديل بيانات عملية التحويل"
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE
                         , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                         , "تعديل بيانات عملية التحويل"
                       , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("Update_MoneyTransFormOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Confirm_MoneyTransFormOPR(uint MoneyTransFormOPRid)
            {
                try
                {
                    MoneyTransFormOPR MoneyTransFormOPR_ = new MoneyTransFormOPRSQL(DB).GetMoneyTransFormOPR_INFO_BYID(MoneyTransFormOPRid);
                    MoneyBox moneybox = MoneyTransFormOPR_.TargetMoneyBox ;
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                    + MoneyTransFormOPRTable.TableName
                    + " set "
                    + MoneyTransFormOPRTable.Confirm_UserID   + "=" +  DB.__User.UserID 
                    + " where "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRID + "=" + MoneyTransFormOPRid
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE
                         , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                         , "تأكيد عملية التحويل"
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE
                         , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                         , "تأكيد عملية التحويل"
                       , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("Update_MoneyTransFormOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            internal bool Delete_MoneyTransFormOPR(uint MoneyTransFormOPRid)
            {
                try
                {

                    MoneyTransFormOPR MoneyTransFormOPR = GetMoneyTransFormOPR_INFO_BYID(MoneyTransFormOPRid);
                    MoneyBox moneybox = MoneyTransFormOPR.SourceMoneyBox ;
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");


                    if (MoneyTransFormOPR.Confirm_User!=null   && DB.IS_Belong_To_Admin_Group (DB.__User.UserID))
                        throw new Exception("تم تاكيد عملية التحويل . يستطيع فقط المدراء حذف العملية");
                    if (MoneyTransFormOPR.Confirm_User != null)
                    {
                        double money_amount = new DataBaseFunctions(DB).Account_GetAmountMoney(MoneyTransFormOPR.TargetMoneyBox.BoxID, MoneyTransFormOPR._Currency.CurrencyID);
                        if (money_amount - MoneyTransFormOPR.Value < 0) throw new Exception("عملية الحذف ستؤدي الى جعل كمية المال في الصندوق للعملة اصغر من الصفر");

                    }
                    DB.ExecuteSQLCommand("delete from   "
                    + MoneyTransFormOPRTable.TableName
                    + " where "
                    + MoneyTransFormOPRTable.MoneyTransFormOPRID  + "=" + MoneyTransFormOPRid
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.DELETE
                         , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                         , "حذف عملية التحويل"
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.DELETE
                         , DatabaseInterface.Log.Log_Target.MoneyTransFormOPR
                         , "حذف عملية التحويل"
                       , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("Delete_MoneyTransFormOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
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
                    System.Windows.Forms.MessageBox.Show("GetMoneyTransFormOPRList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("GetMoneyTransFormOPRList_As_Money_Currenncy:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return MoneyTransFormOPRList;
            }

            internal List <MoneyTransFormOPR > Get_Stuck_MoneyTransformOPR_IN_List(MoneyBox moneybox)
            {
                try
                {
 
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

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
                    System.Windows.Forms.MessageBox.Show("Get_Stuck_MoneyTransformOPR_IN_List:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
               
         
            }
            internal List<MoneyTransFormOPR> Get_Stuck_MoneyTransformOPR_OUT_List(MoneyBox moneybox)
            {
                try
                {

                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, moneybox))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

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
                    System.Windows.Forms.MessageBox.Show("Get_Stuck_MoneyTransformOPR_IN_List:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
               
            }
        }
    }
}
