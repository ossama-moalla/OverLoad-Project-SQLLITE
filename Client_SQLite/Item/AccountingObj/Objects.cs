using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Trade.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OverLoad_Client.Company.Objects;

namespace OverLoad_Client.AccountingObj
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

            //public void GetAccountDetails(ref ListView listview)
            //{
            //    int x = 5;
            //    switch (x)
            //    {

            //        case 1:
            //            listview.Items.Clear();
            //            #region BillSection
            //            if (this.Day != null)
            //            {
            //                #region BillDayReport
            //                if (listview.Name != "ListViewBillDay")
            //                {
            //                    BillReportDetail.IntiliazeListView(ref listview);
            //                }

            //                List<BillReportDetail> billreportdetail
            //                    = new AccountBillSQL(DB).GetBillReport_Details_InDay(this.Year.ToString(), this.Month.ToString(), this.Day.ToString());
            //                for (int i = 0; i < billreportdetail.Count; i++)
            //                {
            //                    ListViewItem item = new ListViewItem(billreportdetail[i].BillDate.ToShortTimeString());
            //                    if (billreportdetail[i]._BillType == "صيانة")
            //                    {
            //                        item.Name = 'M' + billreportdetail[i].BillID.ToString();
            //                        item.BackColor = Color.PaleGoldenrod;
            //                    }
            //                    else if (billreportdetail[i]._BillType == "شراء")
            //                    {
            //                        item.Name = 'O' + billreportdetail[i].BillID.ToString();
            //                        item.BackColor = Color.Orange;
            //                    }
            //                    else
            //                    {

            //                        item.Name = 'I' + billreportdetail[i].BillID.ToString();
            //                        item.BackColor = Color.LightGreen;
            //                    }
            //                    item.SubItems.Add(billreportdetail[i].BillID.ToString());
            //                    item.SubItems.Add(billreportdetail[i]._BillType.ToString());
            //                    item.SubItems.Add(billreportdetail[i].BillDescription.ToString());
            //                    item.SubItems.Add(billreportdetail[i].BillOwner.ToString());
            //                    item.SubItems.Add(billreportdetail[i].BillOperations.ToString());

            //                    item.SubItems.Add(billreportdetail[i]._Currency);
            //                    item.SubItems.Add(billreportdetail[i].BillValue.ToString());
            //                    item.SubItems.Add(billreportdetail[i].Paid.ToString());
            //                    item.SubItems.Add(billreportdetail[i].Remain.ToString());

            //                    listview.Items.Add(item);

            //                }
            //                #endregion
            //            }

            //            else if (this.Month != null)
            //            {
            //                #region BillMonthReport
            //                if (listview.Name != "ListViewBillMonth")
            //                {
            //                    BillDayReportDetail.IntiliazeListView(ref listview);
            //                }
            //                List<BillDayReportDetail> BillDayReportDetailList
            //                        = new AccountBillSQL(DB).GetBillReport_Details_InMonth(this.Year.ToString(), this.Month.ToString());
            //                for (int i = 0; i < BillDayReportDetailList.Count; i++)
            //                {
            //                    ListViewItem item = new ListViewItem(BillDayReportDetailList[i].DayDate.ToShortDateString());
            //                    item.UseItemStyleForSubItems = false;
            //                    item.Name = BillDayReportDetailList[i].DayID.ToString();
            //                    item.SubItems[0].BackColor = Color.LightGray;
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillINCount.ToString());
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillINValue);
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillIN_PaysValue);
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillMaintenanceCount.ToString());
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillMaintenanceValue);
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillMaintenance_PaysValue);
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillOUTCount.ToString());
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillOUTValue);
            //                    item.SubItems.Add(BillDayReportDetailList[i].BillOUT_PaysValue);
            //                    item.SubItems[1].BackColor = Color.LightGreen;
            //                    item.SubItems[2].BackColor = Color.LightGreen;
            //                    item.SubItems[3].BackColor = Color.LightGreen;
            //                    item.SubItems[4].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[5].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[6].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[7].BackColor = Color.Orange;
            //                    item.SubItems[8].BackColor = Color.Orange;
            //                    item.SubItems[9].BackColor = Color.Orange;
            //                    listview.Items.Add(item);

            //                }
            //                #endregion
            //            }
            //            else if (this.Year != null)
            //            {
            //                #region BillYearReport
            //                if (listview.Name != "ListViewBillYear")
            //                {
            //                    BillMonthReportDetail.IntiliazeListView(ref listview);
            //                }
            //                List<BillMonthReportDetail> BillMonthReportDetailList
            //                        = new AccountBillSQL(DB).GetBillReport_Details_InYear(this.Year.ToString());
            //                for (int i = 0; i < BillMonthReportDetailList.Count; i++)
            //                {
            //                    ListViewItem item = new ListViewItem(BillMonthReportDetailList[i].Month);
            //                    item.UseItemStyleForSubItems = false;
            //                    item.Name = BillMonthReportDetailList[i].MonthID.ToString();
            //                    item.SubItems[0].BackColor = Color.LightGray;
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillINCount.ToString());
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillINValue);
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillIN_PaysValue);
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillMaintenanceCount.ToString());
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillMaintenanceValue);
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillMaintenance_PaysValue);
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillOUTCount.ToString());
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillOUTValue);
            //                    item.SubItems.Add(BillMonthReportDetailList[i].BillOUT_PaysValue);
            //                    item.SubItems[1].BackColor = Color.LightGreen;
            //                    item.SubItems[2].BackColor = Color.LightGreen;
            //                    item.SubItems[3].BackColor = Color.LightGreen;
            //                    item.SubItems[4].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[5].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[6].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[7].BackColor = Color.Orange;
            //                    item.SubItems[8].BackColor = Color.Orange;
            //                    item.SubItems[9].BackColor = Color.Orange;
            //                    listview.Items.Add(item);

            //                }
            //                #endregion
            //            }
            //            else
            //            {
            //                #region BillRangeYearReport
            //                if (listview.Name != "ListViewBillYearRange")
            //                {
            //                    BillYearReportDetail.IntiliazeListView(ref listview);
            //                }
            //                List<BillYearReportDetail> BillYearReportDetailList
            //                        = new AccountBillSQL(DB).GetBillReport_Details_InYearRange(this.YearRange_.min_year.ToString(), this.YearRange_.max_year.ToString());
            //                for (int i = 0; i < BillYearReportDetailList.Count; i++)
            //                {
            //                    ListViewItem item = new ListViewItem(BillYearReportDetailList[i].Year.ToString());
            //                    item.UseItemStyleForSubItems = false;
            //                    item.Name = BillYearReportDetailList[i].Year.ToString();
            //                    item.SubItems[0].BackColor = Color.LightGray;
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillINCount.ToString());
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillINValue);
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillIN_PaysValue);
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillMaintenanceCount.ToString());
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillMaintenanceValue);
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillMaintenance_PaysValue);
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillOUTCount.ToString());
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillOUTValue);
            //                    item.SubItems.Add(BillYearReportDetailList[i].BillOUT_PaysValue);
            //                    item.SubItems[1].BackColor = Color.LightGreen;
            //                    item.SubItems[2].BackColor = Color.LightGreen;
            //                    item.SubItems[3].BackColor = Color.LightGreen;
            //                    item.SubItems[4].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[5].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[6].BackColor = Color.PaleGoldenrod;
            //                    item.SubItems[7].BackColor = Color.Orange;
            //                    item.SubItems[8].BackColor = Color.Orange;
            //                    item.SubItems[9].BackColor = Color.Orange;
            //                    listview.Items.Add(item);

            //                }
            //                #endregion
            //            }
            //            break;
            //        #endregion
            //        case 2:
            //            #region PaySection
            //            //if (this.Day != null)
            //            //{
            //                //#region PayDaySection
            //                //listview.Items.Clear();
            //                //if (listview.Name != "ListViewPayDay")
            //                //{
            //                //    AccountOprReportDetail.IntiliazeListView(ref listview);

            //                //}
            //                //List<AccountOprReportDetail> accountopr_reportlist
            //                //          = new AccountOprSQL(DB).GetAccountOprReport_Details_InDay(this.Year.ToString(), this.Month.ToString(), this.Day.ToString());
            //                //for (int i = 0; i < accountopr_reportlist.Count; i++)
            //                //{
            //                //    string payopridstr = accountopr_reportlist[i].OprType
            //                //        + accountopr_reportlist[i].OprDirection
            //                //        + accountopr_reportlist[i].OprID.ToString();
            //                //    string payoprtype = "";
            //                //    string Direction = "";
            //                //    int oprtypeColor = 1; ;
            //                //    if (accountopr_reportlist[i].OprType == "PAY")
            //                //    {

            //                //        payoprtype = "عملية دفع";
            //                //    }
            //                //    else
            //                //    {
            //                //        payoprtype = "عملية صرف";
            //                //        oprtypeColor = 0;
            //                //    }
            //                //    string oprdirection = accountopr_reportlist[i].OprDirection.Replace(" ", string.Empty);
            //                //    int oprtypeDirectionColor;
            //                //    if (oprdirection == "IN")
            //                //    {
            //                //        Direction = "داخل الى الصندوق";
            //                //        oprtypeDirectionColor = 0;
            //                //    }
            //                //    else
            //                //    {
            //                //        Direction = "خارج من الصندوق";
            //                //        oprtypeDirectionColor = 1;
            //                //    }
            //                //    ListViewItem item = new ListViewItem(accountopr_reportlist[i].OprTime.ToShortTimeString());
            //                //    item.Name = payopridstr;
            //                //    item.SubItems.Add(payoprtype);
            //                //    item.SubItems.Add(Direction);
            //                //    item.SubItems.Add(accountopr_reportlist[i].OprID.ToString());
            //                //    item.SubItems.Add(accountopr_reportlist[i].OprDescription);
            //                //    item.SubItems.Add(accountopr_reportlist[i].Value.ToString());
            //                //    item.SubItems.Add(accountopr_reportlist[i].Currency.ToString());
            //                //    item.SubItems.Add(accountopr_reportlist[i].Details.ToString());
            //                //    item.UseItemStyleForSubItems = false;
            //                //    Color color;
            //                //    if (oprtypeDirectionColor == 0 && oprtypeColor == 0) color = Color.YellowGreen;
            //                //    else if (oprtypeDirectionColor == 1 && oprtypeColor == 0) color = Color.DarkOrange;
            //                //    else if (oprtypeDirectionColor == 0 && oprtypeColor == 1) color = Color.LightGreen;
            //                //    else color = Color.Orange;
            //                //    item.UseItemStyleForSubItems = false;
            //                //    item.SubItems[0].BackColor = color;
            //                //    item.SubItems[1].BackColor = color;
            //                //    item.SubItems[2].BackColor = color;
            //                //    item.SubItems[3].BackColor = color;
            //                //    item.SubItems[4].BackColor = color;
            //                //    item.SubItems[5].BackColor = color;
            //                //    item.SubItems[6].BackColor = color;
            //                //    item.SubItems[7].BackColor = color;
            //                //    listview.Items.Add(item);

            //                //}
            //                //#endregion
            //            //}
            //            //else if (this.Month != null)
            //            //{
            //            //    #region PayMonthSection
            //            //    listview.Items.Clear();
            //            //    if (listview.Name != "ListViewPayMonth")
            //            //    {
            //            //        AccountOprDayReportDetail.IntiliazeListView(ref listview);
            //            //    }
            //            //    List<AccountOprDayReportDetail> accountoprdayeportlist
            //            //                        = new AccountOprSQL(DB).GetAccountOprReport_Details_InMonth(this.Year.ToString(), this.Month.ToString());
            //            //    for (int i = 0; i < accountoprdayeportlist.Count; i++)
            //            //    {
            //            //        ListViewItem item = new ListViewItem(accountoprdayeportlist[i].Date_day.ToShortDateString());
            //            //        item.Name = accountoprdayeportlist[i].DateDayNo.ToString();
            //            //        item.SubItems.Add(accountoprdayeportlist[i].PaysIN_Count.ToString());
            //            //        item.SubItems.Add(accountoprdayeportlist[i].PaysOUT_Count.ToString());
            //            //        item.SubItems.Add(accountoprdayeportlist[i].Exchange_Count.ToString());
            //            //        item.SubItems.Add(accountoprdayeportlist[i].PaysIN_Value);
            //            //        item.SubItems.Add(accountoprdayeportlist[i].PaysOUT_Value);
            //            //        item.UseItemStyleForSubItems = false;
            //            //        item.SubItems[0].BackColor = Color.LightGray;
            //            //        item.SubItems[1].BackColor = Color.LightGreen;
            //            //        item.SubItems[2].BackColor = Color.Orange;
            //            //        item.SubItems[3].BackColor = Color.Yellow;
            //            //        item.SubItems[4].BackColor = Color.LightGreen;
            //            //        item.SubItems[5].BackColor = Color.Orange;
            //            //        listview.Items.Add(item);

            //            //    }
            //            //    #endregion
            //            //}
            //            //else if (this.Year != null)
            //            //{
            //            //    #region PayYearSection
            //            //    listview.Items.Clear();
            //            //    if (listview.Name != "ListViewPayYear")
            //            //    {
            //            //        AccountOprMonthReportDetail.IntiliazeListView(ref listview);
            //            //    }
            //            //    List<AccountOprMonthReportDetail> accountoprmonthreportlist
            //            //           = new AccountOprSQL(DB).GetAccountOprReport_Details_InYear(this.Year.ToString());
            //            //    for (int i = 0; i < accountoprmonthreportlist.Count; i++)
            //            //    {
            //            //        ListViewItem item = new ListViewItem(accountoprmonthreportlist[i].Year_Month_Name.ToString());
            //            //        item.Name = accountoprmonthreportlist[i].Year_Month.ToString();
            //            //        item.SubItems.Add(accountoprmonthreportlist[i].PaysIN_Count.ToString());
            //            //        item.SubItems.Add(accountoprmonthreportlist[i].PaysOUT_Count.ToString());
            //            //        item.SubItems.Add(accountoprmonthreportlist[i].Exchange_Count.ToString());
            //            //        item.SubItems.Add(accountoprmonthreportlist[i].PaysIN_Value);
            //            //        item.SubItems.Add(accountoprmonthreportlist[i].PaysOUT_Value.ToString());
            //            //        item.UseItemStyleForSubItems = false;
            //            //        item.SubItems[0].BackColor = Color.LightGray;
            //            //        item.SubItems[1].BackColor = Color.LightGreen;
            //            //        item.SubItems[2].BackColor = Color.Orange;
            //            //        item.SubItems[3].BackColor = Color.Yellow;
            //            //        item.SubItems[4].BackColor = Color.LightGreen;
            //            //        item.SubItems[5].BackColor = Color.Orange;
            //            //        listview.Items.Add(item);

            //            //    }
            //            //    #endregion
            //            //}
            //            //else
            //            //{
            //            //    #region PayYearRangeSection
            //            //    listview.Items.Clear();
            //            //    if (listview.Name != "ListViewPayYearRange")
            //            //    {
            //            //        AccountOprYearReportDetail.IntiliazeListView(ref listview);
            //            //    }
            //            //    List<AccountOprYearReportDetail> accountopryearreportlist
            //            //            = new AccountOprSQL(DB).GetAccountOprReport_Details_InYearRange(this.YearRange_.min_year.ToString(), this.YearRange_.max_year.ToString());
            //            //    for (int i = 0; i < accountopryearreportlist.Count; i++)
            //            //    {
            //            //        ListViewItem item = new ListViewItem(accountopryearreportlist[i].AccountYear.ToString());
            //            //        item.Name = accountopryearreportlist[i].AccountYear.ToString();
            //            //        item.SubItems.Add(accountopryearreportlist[i].PaysIN_Count.ToString());
            //            //        item.SubItems.Add(accountopryearreportlist[i].PaysOUT_Count.ToString());
            //            //        item.SubItems.Add(accountopryearreportlist[i].Exchange_Count.ToString());
            //            //        item.SubItems.Add(accountopryearreportlist[i].PaysIN_Value);
            //            //        item.SubItems.Add(accountopryearreportlist[i].PaysOUT_Value.ToString());
            //            //        item.UseItemStyleForSubItems = false;
            //            //        item.SubItems[0].BackColor = Color.LightGray;
            //            //        item.SubItems[1].BackColor = Color.LightGreen;
            //            //        item.SubItems[2].BackColor = Color.Orange;
            //            //        item.SubItems[3].BackColor = Color.Yellow;
            //            //        item.SubItems[4].BackColor = Color.LightGreen;
            //            //        item.SubItems[5].BackColor = Color.Orange;
            //            //        listview.Items.Add(item);

            //            //    }
            //            //    #endregion
            //            //}
            //            #endregion
            //            break;
            //        default:
            //            listview.Items.Clear();
            //            break;
            //    }
            //}

            internal DateTime GetDate()
            {
                if (this.Year != -1 && this.Month != -1 && this.Day != -1)
                    return new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(Day)
                        , DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                else return DateTime.Now;
            }

            public void IntializeListViewColumnsWidth(ref ListView listview)
            {
                //    if (this.Day != null)
                //    {
                //        if (ShowType == (int)ShowAccountType.Bill)
                //        {
                //            BillReportDetail.IntializeListViewColumnsWidth(ref listview);

                //        }
                //        else
                //        {
                //            AccountOprReportDetail.IntializeListViewColumnsWidth(ref listview);

                //        }
                //    }
                //    else
                //    {
                //        if (ShowType == (int)ShowAccountType.Bill)
                //        {
                //            BillMonthReportDetail .IntializeListViewColumnsWidth(ref listview);
                //        }
                //        else
                //        {
                //            AccountOprMonthReportDetail .IntializeListViewColumnsWidth(ref listview);
                //        }
                //    }
            }
            public void GetAccountReport(ref ListView listview)
            {
                //switch (ShowType)
                //{
                //    case (int)ShowAccountType.Bill:
                //        listview.Items.Clear();
                //        if (listview.Name != "ListViewBillsReport")
                //        {
                //            listview.Name = "ListViewBillsReport";
                //            listview.Columns.Clear();
                //            listview.Columns.Add("العملة");
                //            listview.Columns.Add("عدد فواتير المبيع");
                //            listview.Columns.Add("قيمتها");
                //            listview.Columns.Add("محصلة المدفوع");
                //            listview.Columns.Add("عدد فواتير الصيانة");
                //            listview.Columns.Add("قيمتها");
                //            listview.Columns.Add("محصلة المدفوع");
                //            listview.Columns.Add("عدد فواتير الشراء");
                //            listview.Columns.Add("قيمتها");
                //            listview.Columns.Add("محصلة المدفوع");
                //            IntializeListViewReportColumnsWidth(ref listview);

                //        }
                //        List<BillCurrencyReport> BillCurrencyReportList = new List<BillCurrencyReport>();
                //        if (this.Day != null)
                //        {
                //            BillCurrencyReportList = new AccountBillSQL(DB).GetBillReport_InDay(this .Year .ToString (),this .Month .ToString (),this .Day .ToString ());
                //        }
                //        else if (this.Month != null)
                //        {
                //            BillCurrencyReportList = new AccountBillSQL(DB).GetBillReport_InMonth(this.Year.ToString(), this.Month.ToString());
                //        }
                //        else if (this.Year != null)
                //        {
                //            BillCurrencyReportList = new AccountBillSQL(DB).GetBillReport_InYear(this.Year.ToString());
                //        }
                //        else
                //        {
                //            BillCurrencyReportList 
                //                = new AccountBillSQL(DB).GetBillReport_BetweenTwoYears(this.YearRange_.min_year.ToString(), this.YearRange_.max_year .ToString());
                //        }
                //        for (int i = 0; i < BillCurrencyReportList.Count; i++)
                //        {
                //            ListViewItem item = new ListViewItem(BillCurrencyReportList[i].CurrencyName);
                //            item.Name = BillCurrencyReportList[i].CurrencyID.ToString();
                //            item.SubItems.Add(BillCurrencyReportList[i].BillINCount.ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillINValue.ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillIN_PaysValue.ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillMaintenanceCount .ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillMaintenanceValue.ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillMaintenance_PaysValue.ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillOUTCount  .ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillOUTValue .ToString());
                //            item.SubItems.Add(BillCurrencyReportList[i].BillOUT_PaysValue .ToString());
                //            item.UseItemStyleForSubItems = false;
                //            item.SubItems[0].BackColor = Color.LightGray;
                //            item.SubItems[1].BackColor = Color.LightGreen;
                //            item.SubItems[2].BackColor = Color.LightGreen;
                //            item.SubItems[3].BackColor = Color.LightGreen;
                //            item.SubItems[4].BackColor = Color.PaleGoldenrod;
                //            item.SubItems[5].BackColor = Color.PaleGoldenrod;
                //            item.SubItems[6].BackColor = Color.PaleGoldenrod;
                //            item.SubItems[7].BackColor = Color.Orange;
                //            item.SubItems[8].BackColor = Color.Orange;
                //            item.SubItems[9].BackColor = Color.Orange;
                //            listview.Items.Add(item);

                //        }

                //        break;
                //    case (int)ShowAccountType.Pay:
                //        listview.Items.Clear();
                //        if (listview.Name != "ListViewPaysReport")
                //        {
                //            listview.Name = "ListViewPaysReport";
                //            listview.Columns.Clear();
                //            listview.Columns.Add("العملة");
                //            listview.Columns.Add("داخل عمليات الدفع");
                //            listview.Columns.Add("داخل عمليات الصرف");
                //            listview.Columns.Add("اجمالي الداخل");
                //            listview.Columns.Add("خارج عمليات الدفع");
                //            listview.Columns.Add("خارج عمليات الصرف");
                //            listview.Columns.Add("اجمالي الخارج");
                //            listview.Columns.Add("الصافي");
                //            IntializeListViewReportColumnsWidth(ref listview);

                //        }
                //        List<PayCurrencyReport > PayCurrencyReportList = new List<PayCurrencyReport>();
                //        if (this.Day != null)
                //        {
                //            PayCurrencyReportList = new AccountOprSQL(DB).GetPayReport_InDay(this.Year.ToString(), this.Month.ToString(), this.Day.ToString());
                //        }
                //        else if (this.Month != null)
                //        {
                //            PayCurrencyReportList = new AccountOprSQL(DB).GetPayReport_InMonth(this.Year.ToString(), this.Month.ToString());
                //        }
                //        else if (this.Year != null)
                //        {
                //            PayCurrencyReportList = new AccountOprSQL(DB).GetPayReport_INYear(this.Year.ToString());
                //        }
                //        else
                //        {
                //            PayCurrencyReportList
                //                = new AccountOprSQL(DB).GetPayReport_betweenTwoYears(this.YearRange_.min_year.ToString(), this.YearRange_.max_year.ToString());
                //        }
                //        for (int i = 0; i < PayCurrencyReportList.Count; i++)
                //        {
                //            ListViewItem item = new ListViewItem(PayCurrencyReportList[i].CurrencyName);
                //            item.Name = PayCurrencyReportList[i].CurrencyID.ToString();
                //            item.SubItems.Add(PayCurrencyReportList[i].PaysIN_Pays.ToString());
                //            item.SubItems.Add(PayCurrencyReportList[i].PaysIN_Exchange.ToString());
                //            item.SubItems.Add(PayCurrencyReportList[i].PaysIN_ALL.ToString());
                //            item.SubItems.Add(PayCurrencyReportList[i].PaysOUT_Pays.ToString());
                //            item.SubItems.Add(PayCurrencyReportList[i].PaysOUT_Exchange.ToString());
                //            item.SubItems.Add(PayCurrencyReportList[i].PaysOUT_ALL.ToString());
                //            item.SubItems.Add(PayCurrencyReportList[i].ClearValue .ToString());
                //            item.UseItemStyleForSubItems = false;
                //            item.SubItems[0].BackColor = Color.LightGray;
                //            item.SubItems[1].BackColor = Color.LightGreen;
                //            item.SubItems[2].BackColor = Color.LightGreen;
                //            item.SubItems[3].BackColor = Color.LightGreen;
                //            item.SubItems[4].BackColor = Color.Orange;
                //            item.SubItems[5].BackColor = Color.Orange;
                //            item.SubItems[6].BackColor = Color.Orange;
                //            item.SubItems[7].BackColor = Color.LightBlue ;
                //            listview.Items.Add(item);

                //        }
                //        break;
                //    default:
                //        listview.Items.Clear();
                //        break;
                //}
            }
            public void IntializeListViewReportColumnsWidth(ref ListView listview)
            {
                //switch (ShowType)
                //{
                //    case (int)ShowAccountType.Bill:
                //        listview.Columns[0].Width = 100;
                //        listview.Columns[1].Width = 135;
                //        listview.Columns[2].Width = (listview.Width - 515) / 6;
                //        listview.Columns[3].Width = (listview.Width - 515) / 6;

                //        listview.Columns[4].Width = 135;
                //        listview.Columns[5].Width = (listview.Width - 515) / 6;
                //        listview.Columns[6].Width = (listview.Width - 515) / 6;

                //        listview.Columns[7].Width = 135;
                //        listview.Columns[8].Width = (listview.Width - 515) / 6;
                //        listview.Columns[9].Width = (listview.Width - 515) / 6;
                //        break;
                //    case (int)ShowAccountType.Pay:
                //        listview.Columns[0].Width = 100;
                //        listview.Columns[1].Width = (listview.Width - 100) / 7;
                //        listview.Columns[2].Width = (listview.Width - 100) / 7;
                //        listview.Columns[3].Width = (listview.Width - 100) / 7;
                //        listview.Columns[4].Width = (listview.Width - 100) / 7;
                //        listview.Columns[5].Width = (listview.Width - 100) / 7;
                //        listview.Columns[6].Width = (listview.Width - 100) / 7;
                //        listview.Columns[7].Width = (listview.Width - 100) / 7;
                //        break;
                //    default:
                //        break;
                //}
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
                catch
                {
                    MessageBox.Show("ConvertMoney_CurrencyList_TOString:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayIN:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayIN:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayIN:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_ExchangeOPR_INDirection:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                }
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_ExchangeOPR_OUTDirection:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                return Money_CurrencyList;
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
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayIN:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayIN:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                            ItemOUTList[i]._ItemIN._INCost.Value * (ItemOUTList[i]._ConsumeUnit.Factor / ItemOUTList[i]._ItemIN._ConsumeUnit.Factor), ItemOUTList[i]._ItemIN._INCost.ExchangeRate));
                    }
                }
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayIN:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayIN:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                return Money_CurrencyList;
            }
            public static List<Money_Currency> Get_Money_Currency_List_From_PayOrderReport(List<PayOrderReport> PayOrderReportList)
            {
                List<Money_Currency> Money_CurrencyList = new List<Money_Currency>();
                try
                {

                    for (int i = 0; i < PayOrderReportList.Count; i++)
                    {
                        Money_CurrencyList.Add(new Money_Currency(PayOrderReportList[i]._Currency, PayOrderReportList[i].Value, PayOrderReportList[i].ExchangeRate));
                    }
                }
                catch
                {
                    MessageBox.Show("Get_Money_Currency_List_From_PayOrderReport:", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        //public class BillReportDetail
        //{
        //    public DateTime BillDate;
        //    public int BillID;
        //    public string _BillType;
        //    public string BillDescription;
        //    public string BillOwner;
        //    public string BillOperations;
        //    public string   _Currency;
        //    public double BillValue;
        //    public double Paid;
        //    public double Remain;
        //    public BillReportDetail(DateTime BillDate_, int BillID_,
        //        string BillType_,
        //     string BillDescription_,
        //     string BillOwner_,
        //     string BillOperations_,
        //     string   Currency_,
        //     double BillValue_,
        //     double Paid_,
        //     double Remain_)
        //    {
        //        _BillType = BillType_;
        //        BillDate = BillDate_;
        //        BillID = BillID_;
        //        BillDescription = BillDescription_;
        //        BillOwner = BillOwner_;
        //        BillOperations = BillOperations_;
        //        _Currency = Currency_;
        //        BillValue = BillValue_;
        //        Paid = Paid_;
        //        Remain = Remain_;

        //    }
        //    public static void IntiliazeListView(ref ListView listview)
        //    {
        //       try
        //        {
        //            listview.Name = "ListViewBillDay";
        //            listview.Columns.Clear(); 
        //            listview.Columns.Add("الوقت");
        //            listview.Columns.Add("رقم الفاتورة");
        //            listview.Columns.Add("طبيعة الفاتورة");
        //            listview.Columns.Add("وصف الفاتورة");
        //            listview.Columns.Add("الفاتورة باسم");
        //            listview.Columns.Add("عمليات الفاتورة");
        //            listview.Columns.Add("العملة");
        //            listview.Columns.Add("قيمة الفاتورة");
        //            listview.Columns.Add("محصلة المدفوع");
        //            listview.Columns.Add("الباقي");
        //            IntializeListViewColumnsWidth(ref listview);
        //        }
        //        catch(Exception ee)
        //        {
        //            MessageBox.Show("BillReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "",MessageBoxButtons.OK,MessageBoxIcon.Error );
        //        }

        //    }
        //    public static void IntializeListViewColumnsWidth(ref ListView listview)
        //    {

        //        try
        //        {
        //            listview.Columns[0].Width = 75;
        //            listview.Columns[1].Width = 100;
        //            listview.Columns[2].Width = 125;
        //            listview.Columns[3].Width = (listview.Width - 725) / 3;
        //            listview.Columns[4].Width = (listview.Width - 725) / 3;
        //            listview.Columns[5].Width = (listview.Width - 725) / 3;
        //            listview.Columns[6].Width = 100;
        //            listview.Columns[7].Width = 100;
        //            listview.Columns[8].Width = 125;
        //            listview.Columns[9].Width = 100;

        //        }
        //        catch (Exception ee)
        //        {
        //            MessageBox.Show("BillReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
        //public class BillDayReportDetail
        //{
        //    public int DayID;
        //    public DateTime DayDate;
        //    public int BillINCount;
        //    public string BillINValue;
        //    public string BillIN_PaysValue;
        //    public int BillMaintenanceCount;
        //    public string BillMaintenanceValue;
        //    public string BillMaintenance_PaysValue;
        //    public int BillOUTCount;
        //    public string BillOUTValue;
        //    public string BillOUT_PaysValue;
        //    public BillDayReportDetail(
        //            int DayID_,
        //            DateTime DayDate_,
        //     int BillINCount_,
        //     string BillINValue_,
        //     string BillIN_PaysValue_,
        //     int BillMaintenanceCount_,
        //     string BillMaintenanceValue_,
        //     string BillMaintenance_PaysValue_,
        //     int BillOUTCount_,
        //     string BillOUTValue_,
        //     string BillOUT_PaysValue_)
        //    {
        //        DayID  = DayID_;
        //        DayDate = DayDate_;
        //        BillINCount = BillINCount_;
        //        BillINValue = BillINValue_;
        //        BillIN_PaysValue = BillIN_PaysValue_;
        //        BillMaintenanceCount = BillMaintenanceCount_;
        //        BillMaintenanceValue = BillMaintenanceValue_;
        //        BillMaintenance_PaysValue = BillMaintenance_PaysValue_;
        //        BillOUTCount = BillOUTCount_; ;
        //        BillOUTValue = BillOUTValue_;
        //        BillOUT_PaysValue = BillOUT_PaysValue_;
        //    }
        //    public static void IntiliazeListView(ref ListView listview)
        //    {

        //        try
        //        {
        //            listview.Name = "ListViewBillMonth";
        //            listview.Columns.Clear();
        //            listview.Columns.Add("اليوم");
        //            listview.Columns.Add("عدد فواتير المبيع");
        //            listview.Columns.Add("القيمة");
        //            listview.Columns.Add("المدفوع");
        //            listview.Columns.Add("عدد فواتير الصيانة");
        //            listview.Columns.Add("القيمة");
        //            listview.Columns.Add("المدفوع");
        //            listview.Columns.Add("عدد فواتير الشراء");
        //            listview.Columns.Add("القيمة");
        //            listview.Columns.Add("المدفوع");
        //            IntializeListViewColumnsWidth(ref listview);
        //        }
        //        catch(Exception ee)
        //        {
        //            MessageBox.Show("BillDayReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    public  static void IntializeListViewColumnsWidth(ref ListView listview)
        //    {

        //        try
        //        {
        //            listview.Columns[0].Width = 100;
        //            listview.Columns[1].Width = 135;
        //            listview.Columns[2].Width = (listview.Width - 515) / 6;
        //            listview.Columns[3].Width = (listview.Width - 515) / 6;

        //            listview.Columns[4].Width = 135;
        //            listview.Columns[5].Width = (listview.Width - 515) / 6;
        //            listview.Columns[6].Width = (listview.Width - 515) / 6;

        //            listview.Columns[7].Width = 135;
        //            listview.Columns[8].Width = (listview.Width - 515) / 6;
        //            listview.Columns[9].Width = (listview.Width - 515) / 6;

        //        }
        //        catch(Exception ee)
        //        {
        //            MessageBox.Show("BillDayReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }

        //}
        //public class BillMonthReportDetail
        //{
        //    public int MonthID;
        //    public string Month;
        //    public int BillINCount;
        //    public string BillINValue;
        //    public string BillIN_PaysValue;
        //    public int BillMaintenanceCount;
        //    public string BillMaintenanceValue;
        //    public string BillMaintenance_PaysValue;
        //    public int BillOUTCount;
        //    public string BillOUTValue;
        //    public string BillOUT_PaysValue;
        //    public BillMonthReportDetail(
        //         int MonthID_,
        //            string Month_,
        //     int BillINCount_,
        //     string BillINValue_,
        //     string BillIN_PaysValue_,
        //     int BillMaintenanceCount_,
        //     string BillMaintenanceValue_,
        //     string BillMaintenance_PaysValue_,
        //     int BillOUTCount_,
        //     string BillOUTValue_,
        //     string BillOUT_PaysValue_
        //        )
        //    {
        //        MonthID = MonthID_;
        //        Month = Month_;
        //        BillINCount = BillINCount_;
        //        BillINValue = BillINValue_;
        //        BillIN_PaysValue = BillIN_PaysValue_;
        //        BillMaintenanceCount = BillMaintenanceCount_;
        //        BillMaintenanceValue = BillMaintenanceValue_;
        //        BillMaintenance_PaysValue = BillMaintenance_PaysValue_;
        //        BillOUTCount = BillOUTCount_; ;
        //        BillOUTValue = BillOUTValue_;
        //        BillOUT_PaysValue = BillOUT_PaysValue_;
        //    }
        //    public static void IntiliazeListView(ref ListView listview)
        //    {

        //        try
        //        {
        //            listview.Name = "ListViewBillYear";
        //            listview.Columns.Clear();
        //            listview.Columns.Add("الشهر");
        //            listview.Columns.Add("عدد فواتير المبيع");
        //            listview.Columns.Add("القيمة");
        //            listview.Columns.Add("المدفوع");
        //            listview.Columns.Add("عدد فواتير الصيانة");
        //            listview.Columns.Add("القيمة");
        //            listview.Columns.Add("المدفوع");
        //            listview.Columns.Add("عدد فواتير الشراء");
        //            listview.Columns.Add("القيمة");
        //            listview.Columns.Add("المدفوع");
        //            IntializeListViewColumnsWidth(ref listview);

        //        }
        //        catch(Exception ee)
        //        {
        //            MessageBox.Show("BillMonthReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    public static void IntializeListViewColumnsWidth(ref ListView listview)
        //    {

        //        try
        //        {
        //            listview.Columns[0].Width = 100;
        //            listview.Columns[1].Width = 135;
        //            listview.Columns[2].Width = (listview.Width - 515) / 6;
        //            listview.Columns[3].Width = (listview.Width - 515) / 6;

        //            listview.Columns[4].Width = 135;
        //            listview.Columns[5].Width = (listview.Width - 515) / 6;
        //            listview.Columns[6].Width = (listview.Width - 515) / 6;

        //            listview.Columns[7].Width = 135;
        //            listview.Columns[8].Width = (listview.Width - 515) / 6;
        //            listview.Columns[9].Width = (listview.Width - 515) / 6;

        //        }
        //        catch(Exception ee)
        //        {
        //            MessageBox.Show("BillMonthReportDetail:IntializeListViewColumnsWidth"+Environment.NewLine+ee.Message  , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }

        //}
        //public class BillYearReportDetail
        //{

        //    public int Year;
        //    public int BillINCount;
        //    public string BillINValue;
        //    public string BillIN_PaysValue;
        //    public int BillMaintenanceCount;
        //    public string BillMaintenanceValue;
        //    public string BillMaintenance_PaysValue;
        //    public int BillOUTCount;
        //    public string BillOUTValue;
        //    public string BillOUT_PaysValue;
        //    public BillYearReportDetail(
        //            int Year_,
        //     int BillINCount_,
        //     string BillINValue_,
        //     string BillIN_PaysValue_,
        //     int BillMaintenanceCount_,
        //     string BillMaintenanceValue_,
        //     string BillMaintenance_PaysValue_,
        //     int BillOUTCount_,
        //     string BillOUTValue_,
        //     string BillOUT_PaysValue_)
        //    {
        //        Year = Year_;
        //        BillINCount = BillINCount_;
        //        BillINValue = BillINValue_;
        //        BillIN_PaysValue = BillIN_PaysValue_;
        //        BillMaintenanceCount = BillMaintenanceCount_;
        //        BillMaintenanceValue = BillMaintenanceValue_;
        //        BillMaintenance_PaysValue = BillMaintenance_PaysValue_;
        //        BillOUTCount = BillOUTCount_; ;
        //        BillOUTValue = BillOUTValue_;
        //        BillOUT_PaysValue = BillOUT_PaysValue_;
        //    }
        //    public static void IntiliazeListView(ref ListView listview)
        //    {
        //        listview.Name = "ListViewBillYearRange";
        //        listview.Columns.Clear();
        //        listview.Columns.Add("السنة");
        //        listview.Columns.Add("عدد فواتير المبيع");
        //        listview.Columns.Add("القيمة");
        //        listview.Columns.Add("المدفوع");
        //        listview.Columns.Add("عدد فواتير الصيانة");
        //        listview.Columns.Add("القيمة");
        //        listview.Columns.Add("المدفوع");
        //        listview.Columns.Add("عدد فواتير الشراء");
        //        listview.Columns.Add("القيمة");
        //        listview.Columns.Add("المدفوع");
        //        IntializeListViewColumnsWidth(ref listview);
        //    }
        //    public static void IntializeListViewColumnsWidth(ref ListView listview)
        //    {
        //        listview.Columns[0].Width = 100;
        //        listview.Columns[1].Width = 135;
        //        listview.Columns[2].Width = (listview.Width - 515) / 6;
        //        listview.Columns[3].Width = (listview.Width - 515) / 6;

        //        listview.Columns[4].Width = 135;
        //        listview.Columns[5].Width = (listview.Width - 515) / 6;
        //        listview.Columns[6].Width = (listview.Width - 515) / 6;

        //        listview.Columns[7].Width = 135;
        //        listview.Columns[8].Width = (listview.Width - 515) / 6;
        //        listview.Columns[9].Width = (listview.Width - 515) / 6;
        //    }

        //}
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
            internal static List <Report_Buys_Day_ReportDetail> Get_Report_Buys_Day_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_Buys_Day_ReportDetail> list = new List<Report_Buys_Day_ReportDetail>();


                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DateTime Bill_Time = Convert.ToDateTime(table.Rows[i]["Bill_Time"]);
                        uint Bill_ID = Convert.ToUInt32(table.Rows[i]["Bill_ID"]);
                        string Bill_Owner = table.Rows[i]["Bill_Owner"].ToString();
                        int ClauseS_Count = Convert.ToInt32(table.Rows[i]["ClauseS_Count"]);
                        double Amount_IN = Convert.ToDouble(table.Rows[i]["Amount_IN"]);
                        double Amount_Remain = Convert.ToDouble(table.Rows[i]["Amount_Remain"]);
                        double BillValue = Convert.ToDouble(table.Rows[i]["BillValue"]);
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        string PaysAmount = table.Rows[i]["PaysAmount"].ToString();
                        double PaysRemain = Convert.ToDouble(table.Rows[i]["PaysRemain"]);
                        double Bill_RealValue = Convert.ToDouble(table.Rows[i]["Bill_RealValue"]);
                        double Bill_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bill_Pays_RealValue"]);
                        string Bill_ItemsOut_Value = table.Rows[i]["Bill_ItemsOut_Value"].ToString();
                        double Bill_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["Bill_ItemsOut_RealValue"]);
                        string Bill_Pays_Return_Value = table.Rows[i]["Bill_Pays_Return_Value"].ToString();
                        double Bill_Pays_Return_RealValue = Convert.ToDouble(table.Rows[i]["Bill_Pays_Return_RealValue"]);


                        list.Add(new Report_Buys_Day_ReportDetail(
             Bill_Time,
             Bill_ID,
             Bill_Owner,
            ClauseS_Count,
             Amount_IN,
             Amount_Remain,
             BillValue,
             CurrencyID,
             CurrencyName,
             CurrencySymbol,
           ExchangeRate,
             PaysAmount,
            PaysRemain,
            Bill_RealValue,
            Bill_Pays_RealValue,
            Bill_ItemsOut_Value,
            Bill_ItemsOut_RealValue,
            Bill_Pays_Return_Value,
            Bill_Pays_Return_RealValue));
                    }
                    return list ;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Buys_ReportDetail_List_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {
                try
                {
                    listview.Name = "ListViewBuysDay";
                    listview.Columns.Clear();
                    listview.Columns.Add("الوقت");
                    listview.Columns.Add("الرقم");
                    listview.Columns.Add("باسم");
                    listview.Columns.Add("البنود");
                    listview.Columns.Add("اجمالي الكميات");
                    listview.Columns.Add("الكمية المتبقية");
                    listview.Columns.Add("قيمة الفاتورة");
                    listview.Columns.Add("سعر الصرف");
                    listview.Columns.Add("المدفوع");
                    listview.Columns.Add("الباقي");
                    listview.Columns.Add("قيمة الفاتور الفعلية");
                    listview.Columns.Add(" المدفوع الفعلي");
                    listview.Columns.Add("قيمة  الخارج");
                    listview.Columns.Add(" عائدات الفاتورة");
                    listview.Columns.Add("العائدات الفعلية");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Report_Buy_Day_Detail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 75;//time
                    listview.Columns[1].Width = 60;//id
                    listview.Columns[2].Width = 100;//owner
                    listview.Columns[3].Width = 60;//clause count
                    listview.Columns[4].Width = 125;//amount in
                    listview.Columns[5].Width = 125;//amount remain
                    listview.Columns[6].Width = 100;//value
                    listview.Columns[7].Width = 100;//exchangerate
                    listview.Columns[8].Width = 100;//paid
                    listview.Columns[9].Width = 100;//remain
                    listview.Columns[10].Width = 140;//قيمة الفاتور الفعلية
                    listview.Columns[11].Width = 150;// المدفوع الفعلي
                    listview.Columns[12].Width = 140;//قيمة  الخارج
                    listview.Columns[13].Width = 140;//عائدات الفاتورة
                    listview.Columns[14].Width = 140;//القيمة العلية للعائدات

                }
                catch (Exception ee)
                {
                    MessageBox.Show("Report_Buy_Day_Detail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List <Report_Buys_Month_ReportDetail> Get_Report_Buys_Month_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_Buys_Month_ReportDetail> list = new List<Report_Buys_Month_ReportDetail>();
                   
                    for (int i = 0; i < table .Rows .Count; i++)
                    {
                      int DayID=Convert .ToInt32 (  table .Rows [i]["DayID"]);
                      DateTime DayDate=Convert .ToDateTime (  table.Rows[i]["DayDate"] );
                        int Bills_Count=Convert .ToInt32 ( table.Rows[i]["Bills_Count"] );
                       double  Amount_IN =Convert .ToDouble ( table.Rows[i]["Amount_IN"] );
                    double Amount_Remain = Convert.ToDouble(table.Rows[i]["Amount_Remain"] );
                  string    Bills_Value = table.Rows[i]["Bills_Value"] .ToString ();
                  string   Bills_Pays_Value = table.Rows[i]["Bills_Pays_Value"] .ToString ();
                   string  Bills_Pays_Remain = table.Rows[i]["Bills_Pays_Remain"].ToString ();
                    double Bills_Pays_Remain_UPON_Bill_Currency = Convert.ToDouble(table.Rows[i]["Bills_Pays_Remain_UPON_Bill_Currency"]);
                    double Bills_RealValue = Convert.ToDouble(table.Rows[i]["Bills_RealValue"] );
                    double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_RealValue"] );
                    string  Bills_ItemsOut_Value = table.Rows[i]["Bills_ItemsOut_Value"].ToString ();
                   double   Bills_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["Bills_ItemsOut_RealValue"] );
                    string  Bills_Pays_Return_Value = table.Rows[i]["Bills_Pays_Return_Value"] .ToString ();
                    double Bills_Pays_Return_RealValue= Convert.ToDouble(table.Rows[i]["Bills_Pays_Return_RealValue"] );
                        list.Add(new Report_Buys_Month_ReportDetail( DayID,
             DayDate,
           Bills_Count,
             Amount_IN,
            Amount_Remain,
            Bills_Value,
            Bills_Pays_Value,
           Bills_Pays_Remain,
            Bills_Pays_Remain_UPON_Bill_Currency,
            Bills_RealValue,
            Bills_Pays_RealValue,
            Bills_ItemsOut_Value,
            Bills_ItemsOut_RealValue,
            Bills_Pays_Return_Value,
            Bills_Pays_Return_RealValue
        ));
                    }
                    return list ;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Month_ReportDetail_List_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewBuys_Month";
                    listview.Columns.Clear();
                    listview.Columns.Add("اليوم");
                    listview.Columns.Add("العدد الكلي");
                    listview.Columns.Add("الكمية الداخلة");
                    listview.Columns.Add("الكمية المتبقية");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    listview.Columns.Add("قيمة الخارج");
                    listview.Columns.Add(" الخارج الفعلي");
                    listview.Columns.Add(" قيمة العائدات المالية");
                    listview.Columns.Add(" العائد الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillMonthReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 120;//bills count

                    listview.Columns[2].Width = 125;//amount in
                    listview.Columns[3].Width = 125;//amount remain
                    listview.Columns[4].Width = 120;//bill value
                    listview.Columns[5].Width = 140;//paid
                    listview.Columns[6].Width = 115;//remain

                    listview.Columns[7].Width = 125;//real value
                    listview.Columns[8].Width = 125;//real pays value
                    listview.Columns[9].Width = 125;//out value
                    listview.Columns[10].Width = 125;//out real value
                    listview.Columns[11].Width = 125;//pays return value
                    listview.Columns[12].Width = 125;//pays return  real value


                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillMonthReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_Buys_Year_ReportDetail> Get_Report_Buys_Year_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_Buys_Year_ReportDetail> list = new List<Report_Buys_Year_ReportDetail>();

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        int MonthNO = Convert.ToInt32(table.Rows[i]["MonthNO"]);
                        string  MonthName =table.Rows[i]["MonthName"].ToString ();
                        int Bills_Count = Convert.ToInt32(table.Rows[i]["Bills_Count"]);
                        double Amount_IN = Convert.ToDouble(table.Rows[i]["Amount_IN"]);
                        double Amount_Remain = Convert.ToDouble(table.Rows[i]["Amount_Remain"]);
                        string Bills_Value = table.Rows[i]["Bills_Value"].ToString();
                        string Bills_Pays_Value = table.Rows[i]["Bills_Pays_Value"].ToString();
                        string Bills_Pays_Remain = table.Rows[i]["Bills_Pays_Remain"].ToString();
                        double Bills_Pays_Remain_UPON_Bill_Currency = Convert.ToDouble(table.Rows[i]["Bills_Pays_Remain_UPON_Bill_Currency"]);
                        double Bills_RealValue = Convert.ToDouble(table.Rows[i]["Bills_RealValue"]);
                        double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_RealValue"]);
                        string Bills_ItemsOut_Value = table.Rows[i]["Bills_ItemsOut_Value"].ToString();
                        double Bills_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["Bills_ItemsOut_RealValue"]);
                        string Bills_Pays_Return_Value = table.Rows[i]["Bills_Pays_Return_Value"].ToString();
                        double Bills_Pays_Return_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_Return_RealValue"]);
                        list.Add(new Report_Buys_Year_ReportDetail(MonthNO ,
             MonthName,
           Bills_Count,
             Amount_IN,
            Amount_Remain,
            Bills_Value,
            Bills_Pays_Value,
           Bills_Pays_Remain,
            Bills_Pays_Remain_UPON_Bill_Currency,
            Bills_RealValue,
            Bills_Pays_RealValue,
            Bills_ItemsOut_Value,
            Bills_ItemsOut_RealValue,
            Bills_Pays_Return_Value,
            Bills_Pays_Return_RealValue
        ));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_Year_ReportDetail_List_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewBuys_Year";
                    listview.Columns.Clear();
                    listview.Columns.Add("رقم الشهر");
                    listview.Columns.Add("الشهر");
                    listview.Columns.Add("العدد الكلي");
                    //listview.Columns.Add("اجمالي البنود");
                    listview.Columns.Add("الكمية الداخلة");
                    listview.Columns.Add("الكمية المتبقية");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    listview.Columns.Add("قيمة الخارج");
                    listview.Columns.Add(" الخارج الفعلي");
                    listview.Columns.Add("قيمة العائدات");
                    listview.Columns.Add(" العائد الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 100;//daydate
                    listview.Columns[2].Width = 120;//bills count

                    listview.Columns[3].Width = 125;//amount in
                    listview.Columns[4].Width = 125;//amount remain
                    listview.Columns[5].Width = 120;//bill value
                    listview.Columns[6].Width = 140;//paid
                    listview.Columns[7].Width = 115;//remain

                    listview.Columns[8].Width = 125;//real value
                    listview.Columns[9].Width = 125;//real pays value
                    listview.Columns[10].Width = 125;//out value
                    listview.Columns[11].Width = 125;//out real value
                    listview.Columns[12].Width = 125;//pays return value
                    listview.Columns[13].Width = 125;//pays return  real value


                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_Buys_YearRange_ReportDetail> Get_Report_Buys_YearRange_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_Buys_YearRange_ReportDetail> list = new List<Report_Buys_YearRange_ReportDetail>();

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        int YearNO = Convert.ToInt32(table.Rows[i]["YearNO"]);
                        int Bills_Count = Convert.ToInt32(table.Rows[i]["Bills_Count"]);
                        double Amount_IN = Convert.ToDouble(table.Rows[i]["Amount_IN"]);
                        double Amount_Remain = Convert.ToDouble(table.Rows[i]["Amount_Remain"]);
                        string Bills_Value = table.Rows[i]["Bills_Value"].ToString();
                        string Bills_Pays_Value = table.Rows[i]["Bills_Pays_Value"].ToString();
                        string Bills_Pays_Remain = table.Rows[i]["Bills_Pays_Remain"].ToString();
                        double Bills_Pays_Remain_UPON_Bill_Currency = Convert.ToDouble(table.Rows[i]["Bills_Pays_Remain_UPON_Bill_Currency"]);
                        double Bills_RealValue = Convert.ToDouble(table.Rows[i]["Bills_RealValue"]);
                        double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_RealValue"]);
                        string Bills_ItemsOut_Value = table.Rows[i]["Bills_ItemsOut_Value"].ToString();
                        double Bills_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["Bills_ItemsOut_RealValue"]);
                        string Bills_Pays_Return_Value = table.Rows[i]["Bills_Pays_Return_Value"].ToString();
                        double Bills_Pays_Return_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_Return_RealValue"]);
                        list.Add(new Report_Buys_YearRange_ReportDetail(YearNO,
           Bills_Count,
             Amount_IN,
            Amount_Remain,
            Bills_Value,
            Bills_Pays_Value,
           Bills_Pays_Remain,
            Bills_Pays_Remain_UPON_Bill_Currency,
            Bills_RealValue,
            Bills_Pays_RealValue,
            Bills_ItemsOut_Value,
            Bills_ItemsOut_RealValue,
            Bills_Pays_Return_Value,
            Bills_Pays_Return_RealValue
        ));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Buys_YearRange_ReportDetail_List_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewBuys_YearRange";
                    listview.Columns.Clear();
                    listview.Columns.Add("السنة");
                    listview.Columns.Add("العدد الكلي");
                    //listview.Columns.Add("اجمالي البنود");
                    listview.Columns.Add("الكمية الداخلة");
                    listview.Columns.Add("الكمية المتبقية");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    listview.Columns.Add("قيمة الخارج");
                    listview.Columns.Add(" الخارج الفعلي");
                    listview.Columns.Add("قيمة العائدات");
                    listview.Columns.Add(" العائد الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearRangeReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 120;//bills count

                    listview.Columns[2].Width = 125;//amount in
                    listview.Columns[3].Width = 125;//amount remain
                    listview.Columns[4].Width = 120;//bill value
                    listview.Columns[5].Width = 140;//paid
                    listview.Columns[6].Width = 115;//remain

                    listview.Columns[7].Width = 125;//real value
                    listview.Columns[8].Width = 125;//real pays value
                    listview.Columns[9].Width = 125;//out value
                    listview.Columns[10].Width = 125;//out real value
                    listview.Columns[11].Width = 125;//pays return value
                    listview.Columns[12].Width = 125;//pays return  real value


                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearRangeReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_MaintenanceOPRs_Day_ReportDetail>  Get_Report_Buys_Day_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_MaintenanceOPRs_Day_ReportDetail> list = new List<Report_MaintenanceOPRs_Day_ReportDetail>();


                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                       
                        DateTime MaintenanceOPR_Date = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_Date"]);
                        uint MaintenanceOPR_ID = Convert.ToUInt32(table.Rows[i]["MaintenanceOPR_ID"]);
                        string MaintenanceOPR_Owner = table.Rows[i]["MaintenanceOPR_Owner"].ToString();
                        uint ItemID = Convert.ToUInt32(table.Rows[i]["ItemID"]);
                        string ItemName = table.Rows[i]["ItemName"].ToString();
                        string ItemCompany = table.Rows[i]["ItemCompany"].ToString();
                        string FolderName = table.Rows[i]["FolderName"].ToString();
                        string FalutDesc = table.Rows[i]["FalutDesc"].ToString();
                        DateTime? MaintenanceOPR_Endworkdate;
                        if (table.Rows[i]["MaintenanceOPR_Endworkdate"] != DBNull.Value)

                        {
                            MaintenanceOPR_Endworkdate = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_Endworkdate"]);
                        }
                        else
                        {
                            MaintenanceOPR_Endworkdate = null;
                        }

                        bool? MaintenanceOPR_Rpaired;
                        try
                        {
                            MaintenanceOPR_Rpaired = Convert.ToBoolean(table.Rows[i]["MaintenanceOPR_Rpaired"]);
                        }
                        catch
                        {
                            MaintenanceOPR_Rpaired = null;
                        }


                        DateTime? MaintenanceOPR_DeliverDate;
                        try
                        {
                            MaintenanceOPR_DeliverDate = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_DeliverDate"]);
                        }
                        catch
                        {
                            MaintenanceOPR_DeliverDate = null;
                        }

                        DateTime? MaintenanceOPR_EndWarrantyDate;
                        if (table.Rows[i]["MaintenanceOPR_EndWarrantyDate"] != DBNull.Value)
                        {
                            MaintenanceOPR_EndWarrantyDate = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_EndWarrantyDate"]);
                        }
                        else
                        {
                            MaintenanceOPR_EndWarrantyDate = null;
                        }

                        uint? BillMaintenanceID;
                        try
                        {
                            BillMaintenanceID = Convert.ToUInt32(table.Rows[i]["BillMaintenanceID"]);
                        }
                        catch
                        {
                            BillMaintenanceID = null;
                        }

                        double? BillValue;
                        try
                        {
                            BillValue = Convert.ToDouble(table.Rows[i]["BillValue"]);
                        }
                        catch
                        {
                            BillValue = null;
                        }

                        uint? CurrencyID;
                        try
                        {
                            CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        }
                        catch
                        {
                            CurrencyID = null;
                        }

                        string CurrencyName;
                        try
                        {
                            CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        }
                        catch
                        {
                            CurrencyName = "";
                        }

                        string CurrencySymbol;
                        try
                        {
                            CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        }
                        catch
                        {
                            CurrencySymbol = "";
                        }

                        double? ExchangeRate;
                        try
                        {
                            ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        }
                        catch
                        {
                            ExchangeRate = null;
                        }

                        string PaysAmount;
                        try
                        {
                            PaysAmount = table.Rows[i]["PaysAmount"].ToString();
                        }
                        catch
                        {
                            PaysAmount = "";
                        }

                        double? PaysRemain;
                        try
                        {
                            PaysRemain = Convert.ToDouble(table.Rows[i]["PaysRemain"]);
                        }
                        catch
                        {
                            PaysRemain = null;
                        }

                        string Bill_ItemsOut_Value;
                        try
                        {
                            Bill_ItemsOut_Value = table.Rows[i]["Bill_ItemsOut_Value"].ToString();
                        }
                        catch
                        {
                            Bill_ItemsOut_Value = "";
                        }

                        double? Bill_ItemsOut_RealValue;
                        try
                        {
                            Bill_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["Bill_ItemsOut_RealValue"]);
                        }
                        catch
                        {
                            Bill_ItemsOut_RealValue = null;
                        }

                        double? Bill_RealValue;
                        try
                        {
                            Bill_RealValue = Convert.ToDouble(table.Rows[i]["Bill_RealValue"]);
                        }
                        catch
                        {
                            Bill_RealValue = null;
                        }

                        double? Bill_Pays_RealValue;
                        try
                        {
                            Bill_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bill_Pays_RealValue"]);
                        }
                        catch
                        {
                            Bill_Pays_RealValue = null;
                        }
                        list.Add(new Objects.Report_MaintenanceOPRs_Day_ReportDetail(MaintenanceOPR_Date,
             MaintenanceOPR_ID,
             MaintenanceOPR_Owner,
             ItemID,
            ItemName,
             ItemCompany,
             FolderName,
             FalutDesc,
            MaintenanceOPR_Endworkdate,
             MaintenanceOPR_Rpaired,
             MaintenanceOPR_DeliverDate,
            MaintenanceOPR_EndWarrantyDate,
             BillMaintenanceID,
             BillValue,
             CurrencyID,
            CurrencyName,
            CurrencySymbol,
            ExchangeRate,
             PaysAmount,
             PaysRemain,
             Bill_ItemsOut_Value,
             Bill_ItemsOut_RealValue,
             Bill_RealValue,
            Bill_Pays_RealValue));

                    }

                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_MaintenanceOPRs_ReportDetail_List_From_DataTable:" + ee.Message);
                }
            

        }
            public static void IntiliazeListView(ref ListView listview)
            {
                try
                {

                    listview.Name = "ListViewMaintenanceOPRs_Day";
                    listview.Columns.Clear();
                    listview.Columns.Add("الوقت");
                    listview.Columns.Add("الرقم");
                    listview.Columns.Add("باسم");
                    listview.Columns.Add("الموديل");
                    listview.Columns.Add("الشركة");
                    listview.Columns.Add("الصنف");
                    listview.Columns.Add("وصف العطل");
                    listview.Columns.Add(" انتهاء العمل");
                    listview.Columns.Add("الاصلاح");
                    listview.Columns.Add(" تسليم الجهاز");
                    listview.Columns.Add(" انتهاء الكفالة");
                    listview.Columns.Add("قيمة الفاتورة");
                    listview.Columns.Add(" سعر الصرف");
                    listview.Columns.Add(" المدفوع ");
                    listview.Columns.Add(" المتبقي ");
                    listview.Columns.Add("قيمة  المواد ");
                    listview.Columns.Add("قيمة  المواد  الفعلية");
                    listview.Columns.Add(" قيمة الفاتورة الفعلية");
                    listview.Columns.Add(" المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Report_MaintenanceOPR_Day_Detail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 75;//time
                    listview.Columns[1].Width = 60;//id
                    listview.Columns[2].Width = 100;//owner
                    listview.Columns[3].Width = 125;//model
                    listview.Columns[4].Width = 125;//company
                    listview.Columns[5].Width = 125;//type
                    listview.Columns[6].Width = 125;//fault desc
                    listview.Columns[7].Width = 125;//end work date
                    listview.Columns[8].Width = 125;//repair
                    listview.Columns[9].Width = 125;//deliver date
                    listview.Columns[10].Width = 125;//end warranty date
                    listview.Columns[11].Width = 125;//قيمة الفاتورة
                    listview.Columns[12].Width = 125;//سعر الصرف
                    listview.Columns[13].Width = 125;//المدفوع
                    listview.Columns[14].Width = 125;//المتبقي
                    listview.Columns[15].Width = 155;//قيمة  الخارج
                    listview.Columns[16].Width = 160;//الخارج الفعلي 
                    listview.Columns[17].Width = 155;//قيمة الفاتور الفعلية
                    listview.Columns[18].Width = 150;// المدفوع الفعلي

                }
                catch (Exception ee)
                {
                    MessageBox.Show("Report_MaintenanceOPR_Day_Detail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List < Report_MaintenanceOPRs_Month_ReportDetail  > Get_Report_MaintenanceOPRs_Month_ReportDetail_From_DataTable(DataTable table)
            {
                try
                {
                    List<Report_MaintenanceOPRs_Month_ReportDetail> list = new List<Report_MaintenanceOPRs_Month_ReportDetail>();
            for (int i=0;i<table .Rows .Count;i++)
                    {
                        int DayID = Convert.ToInt32(table.Rows[i]["DayID"]);
                        DateTime DayDate = Convert.ToDateTime(table.Rows[i]["DayDate"]);
                        int MaintenanceOPRs_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Count"]);
                        int MaintenanceOPRs_EndWork_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_EndWork_Count"]);
                        int MaintenanceOPRs_Repaired_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Repaired_Count"]);
                        int MaintenanceOPRs_Warranty_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Warranty_Count"]);
                        int MaintenanceOPRs_EndWarranty_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_EndWarranty_Count"]);

                        int BillMaintenances_Count = Convert.ToInt32(table.Rows[i]["BillMaintenances_Count"]);
                        string BillMaintenances_Value = table.Rows[i]["BillMaintenances_Value"].ToString();
                        string BillMaintenances_Pays_Value = table.Rows[i]["BillMaintenances_Pays_Value"].ToString();
                        string BillMaintenances_Pays_Remain = table.Rows[i]["BillMaintenances_Pays_Remain"].ToString();
                        double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = Convert.ToDouble(table.Rows[i]["MaintenanceOPRs_EndWarranty_Count"]);

                        string BillMaintenances_ItemsOut_Value = table.Rows[i]["BillMaintenances_ItemsOut_Value"].ToString();
                        double BillMaintenances_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_ItemsOut_RealValue"]);
                        double BillMaintenances_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_RealValue"]);
                        double BillMaintenances_Pays_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_Pays_RealValue"]);


                        list .Add ( new Report_MaintenanceOPRs_Month_ReportDetail(
                            DayID,
                            DayDate,
                            MaintenanceOPRs_Count,
                                     MaintenanceOPRs_EndWork_Count,
                                    MaintenanceOPRs_Repaired_Count,
                                    MaintenanceOPRs_Warranty_Count,
                                     MaintenanceOPRs_EndWarranty_Count,

                                    BillMaintenances_Count,
                                    BillMaintenances_Value,
                                    BillMaintenances_Pays_Value,
                                     BillMaintenances_Pays_Remain,
                                    BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency,

                                     BillMaintenances_ItemsOut_Value,
                                   BillMaintenances_ItemsOut_RealValue,
                                    BillMaintenances_RealValue,
                                     BillMaintenances_Pays_RealValue));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Month_ReportDetail_From_DataTable:" + ee.Message);
                }

            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewMaintenanceOPRs_Month";
                    listview.Columns.Clear();
                    listview.Columns.Add("اليوم");
                    listview.Columns.Add("عدد عمليات الصيانة");
                    listview.Columns.Add("ما تم انهاءه");
                    listview.Columns.Add("ماتم اصلاحه");
                    listview.Columns.Add("عدد المكفول");
                    listview.Columns.Add("المنتهي كفالته");
                    listview.Columns.Add(" قمة فواتير الصيانة");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("قيمة المواد المركبة");
                    listview.Columns.Add("قيمة المواد المركبة الفعلية");
                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillMonthReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 120;//عدد عمليات الصيانة
                    listview.Columns[2].Width = 115;//ما تم انهائه
                    listview.Columns[3].Width = 125;//ما تم اصلاحه
                    listview.Columns[4].Width = 125;//عدد المكفول
                    listview.Columns[5].Width = 120;//المنتهي كفالته
                    listview.Columns[6].Width = 140;//قيمة فواتير الصيانة
                    listview.Columns[7].Width = 115;//المدفوع

                    listview.Columns[8].Width = 125;//المتبقي
                    listview.Columns[9].Width = 125;//قيمة العناصر المركبة
                    listview.Columns[10].Width = 125;//قيمة المركب الفعلي
                    listview.Columns[11].Width = 125;//قيمة الفواتير الفعلية
                    listview.Columns[12].Width = 125;//قيمة المدفوع الفعلي



                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillMonthReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List < Report_MaintenanceOPRs_Year_ReportDetail> Get_Report_MaintenanceOPRs_Year_ReportDetail_List_From_DataTable(DataTable table)
            {
                try
                {
                    List<Report_MaintenanceOPRs_Year_ReportDetail> list = new List<Report_MaintenanceOPRs_Year_ReportDetail>();
                    for (int i=0;i<table .Rows .Count;i++)
                    {
                        int MonthNO = Convert.ToInt32(table.Rows[i]["MonthNO"]);
                        string MonthName = table.Rows[i]["MonthName"].ToString();
                        int MaintenanceOPRs_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Count"]);
                        int MaintenanceOPRs_EndWork_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_EndWork_Count"]);
                        int MaintenanceOPRs_Repaired_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Repaired_Count"]);
                        int MaintenanceOPRs_Warranty_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Warranty_Count"]);
                        int MaintenanceOPRs_EndWarranty_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_EndWarranty_Count"]);

                        int BillMaintenances_Count = Convert.ToInt32(table.Rows[i]["BillMaintenances_Count"]);
                        string BillMaintenances_Value = table.Rows[i]["BillMaintenances_Value"].ToString();
                        string BillMaintenances_Pays_Value = table.Rows[i]["BillMaintenances_Pays_Value"].ToString();
                        string BillMaintenances_Pays_Remain = table.Rows[i]["BillMaintenances_Pays_Remain"].ToString();
                        double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = Convert.ToDouble(table.Rows[i]["MaintenanceOPRs_EndWarranty_Count"]);

                        string BillMaintenances_ItemsOut_Value = table.Rows[i]["BillMaintenances_ItemsOut_Value"].ToString();
                        double BillMaintenances_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_ItemsOut_RealValue"]);
                        double BillMaintenances_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_RealValue"]);
                        double BillMaintenances_Pays_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_Pays_RealValue"]);


                        list .Add ( new Report_MaintenanceOPRs_Year_ReportDetail(
                            MonthNO,
                            MonthName,
                            MaintenanceOPRs_Count,
                                     MaintenanceOPRs_EndWork_Count,
                                    MaintenanceOPRs_Repaired_Count,
                                    MaintenanceOPRs_Warranty_Count,
                                     MaintenanceOPRs_EndWarranty_Count,

                                    BillMaintenances_Count,
                                    BillMaintenances_Value,
                                    BillMaintenances_Pays_Value,
                                     BillMaintenances_Pays_Remain,
                                    BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency,

                                     BillMaintenances_ItemsOut_Value,
                                   BillMaintenances_ItemsOut_RealValue,
                                    BillMaintenances_RealValue,
                                     BillMaintenances_Pays_RealValue));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_Year_ReportDetailList_From_DataTable:" + ee.Message);
                }

            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewMaintenanceOPRs_Year";
                    listview.Columns.Clear();
                    listview.Columns.Add("رقم الشهر");
                    listview.Columns.Add("الشهر");
                    listview.Columns.Add("عدد عمليات الصيانة");
                    listview.Columns.Add("ما تم انهاءه");
                    listview.Columns.Add("ماتم اصلاحه");
                    listview.Columns.Add("عدد المكفول");
                    listview.Columns.Add("المنتهي كفالته");
                    listview.Columns.Add(" قمة فواتير الصيانة");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("قيمة المواد المركبة");
                    listview.Columns.Add("قيمة المواد المركبة الفعلية");
                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//month
                    listview.Columns[1].Width = 100;//month
                    listview.Columns[2].Width = 120;//عدد عمليات الصيانة
                    listview.Columns[3].Width = 115;//ما تم انهائه
                    listview.Columns[4].Width = 125;//ما تم اصلاحه
                    listview.Columns[5].Width = 125;//عدد المكفول
                    listview.Columns[6].Width = 120;//المنتهي كفالته
                    listview.Columns[7].Width = 140;//قيمة فواتير الصيانة
                    listview.Columns[8].Width = 115;//المدفوع

                    listview.Columns[9].Width = 125;//المتبقي
                    listview.Columns[10].Width = 125;//قيمة العناصر المركبة
                    listview.Columns[11].Width = 125;//قيمة المركب الفعلي
                    listview.Columns[12].Width = 125;//قيمة الفواتير الفعلية
                    listview.Columns[13].Width = 125;//قيمة المدفوع الفعلي


                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List < Report_MaintenanceOPRs_YearRange_ReportDetail > Get_Report_MaintenanceOPRs_YearRange_ReportDetail_From_DataTable(DataTable table)
            {
                try
                {
                    List<Report_MaintenanceOPRs_YearRange_ReportDetail> list = new List<Report_MaintenanceOPRs_YearRange_ReportDetail>();
                    for(int i=0;i<table.Rows .Count;i++)
                    {
                        int YearNO = Convert.ToInt32(table.Rows[i]["YearNO"]);
                        int MaintenanceOPRs_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Count"]);
                        int MaintenanceOPRs_EndWork_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_EndWork_Count"]);
                        int MaintenanceOPRs_Repaired_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Repaired_Count"]);
                        int MaintenanceOPRs_Warranty_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_Warranty_Count"]);
                        int MaintenanceOPRs_EndWarranty_Count = Convert.ToInt32(table.Rows[i]["MaintenanceOPRs_EndWarranty_Count"]);

                        int BillMaintenances_Count = Convert.ToInt32(table.Rows[i]["BillMaintenances_Count"]);
                        string BillMaintenances_Value = table.Rows[i]["BillMaintenances_Value"].ToString();
                        string BillMaintenances_Pays_Value = table.Rows[i]["BillMaintenances_Pays_Value"].ToString();
                        string BillMaintenances_Pays_Remain = table.Rows[i]["BillMaintenances_Pays_Remain"].ToString();
                        double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = Convert.ToDouble(table.Rows[i]["MaintenanceOPRs_EndWarranty_Count"]);

                        string BillMaintenances_ItemsOut_Value = table.Rows[i]["BillMaintenances_ItemsOut_Value"].ToString();
                        double BillMaintenances_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_ItemsOut_RealValue"]);
                        double BillMaintenances_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_RealValue"]);
                        double BillMaintenances_Pays_RealValue = Convert.ToDouble(table.Rows[i]["BillMaintenances_Pays_RealValue"]);


                        list .Add ( new Report_MaintenanceOPRs_YearRange_ReportDetail(
                            YearNO,
                            MaintenanceOPRs_Count,
                                     MaintenanceOPRs_EndWork_Count,
                                    MaintenanceOPRs_Repaired_Count,
                                    MaintenanceOPRs_Warranty_Count,
                                     MaintenanceOPRs_EndWarranty_Count,

                                    BillMaintenances_Count,
                                    BillMaintenances_Value,
                                    BillMaintenances_Pays_Value,
                                     BillMaintenances_Pays_Remain,
                                    BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency,

                                     BillMaintenances_ItemsOut_Value,
                                   BillMaintenances_ItemsOut_RealValue,
                                    BillMaintenances_RealValue,
                                     BillMaintenances_Pays_RealValue));

                    }
                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_MaintenanceOPRs_YearRange_ReportDetail_List_From_DataTable:" + ee.Message);
                }

            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewMaintenanceOPRs_YearRange";
                    listview.Columns.Clear();
                    listview.Columns.Add("السنة");
                    listview.Columns.Add("عدد عمليات الصيانة");
                    listview.Columns.Add("ما تم انهاءه");
                    listview.Columns.Add("ماتم اصلاحه");
                    listview.Columns.Add("عدد المكفول");
                    listview.Columns.Add("المنتهي كفالته");
                    listview.Columns.Add(" قمة فواتير الصيانة");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("قيمة المواد المركبة");
                    listview.Columns.Add("قيمة المواد المركبة الفعلية");
                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearRangeReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//year
                    listview.Columns[1].Width = 120;//عدد عمليات الصيانة
                    listview.Columns[2].Width = 115;//ما تم انهائه
                    listview.Columns[3].Width = 125;//ما تم اصلاحه
                    listview.Columns[4].Width = 125;//عدد المكفول
                    listview.Columns[5].Width = 120;//المنتهي كفالته
                    listview.Columns[6].Width = 140;//قيمة فواتير الصيانة
                    listview.Columns[7].Width = 115;//المدفوع

                    listview.Columns[8].Width = 125;//المتبقي
                    listview.Columns[9].Width = 125;//قيمة العناصر المركبة
                    listview.Columns[10].Width = 125;//قيمة المركب الفعلي
                    listview.Columns[11].Width = 125;//قيمة الفواتير الفعلية
                    listview.Columns[12].Width = 125;//قيمة المدفوع الفعلي

                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearRangeReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_Sells_Day_ReportDetail> Get_Report_Sells_Day_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {

                    List<Report_Sells_Day_ReportDetail> list = new List<Objects.Report_Sells_Day_ReportDetail>();

                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        DateTime Bill_Date = Convert.ToDateTime(table.Rows[i]["Bill_Time"]);
                        uint Bill_ID = Convert.ToUInt32(table.Rows[i]["Bill_ID"]);
                        string SellType = table.Rows[i]["SellType"].ToString();
                        string Bill_Owner= table.Rows[i]["Bill_Owner"].ToString();
                        int ClauseS_Count = Convert.ToInt32(table.Rows[i]["ClauseS_Count"]);
                        double BillValue = Convert.ToDouble(table.Rows[i]["BillValue"]);
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        int PaysCount = Convert.ToInt32(table.Rows[i]["PaysCount"]);
                        string PaysAmount = table.Rows[i]["PaysAmount"].ToString();
                        double PaysRemain = Convert.ToDouble(table.Rows[i]["PaysRemain"]);
                        string Source_ItemsIN_Cost_Details = table.Rows[i]["Source_ItemsIN_Cost_Details"].ToString();
                        double Source_ItemsIN_RealCost = Convert.ToDouble(table.Rows[i]["Source_ItemsIN_RealCost"]);
                        double ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["ItemsOut_RealValue"]);
                        double RealPaysValue = Convert.ToDouble(table.Rows[i]["RealPaysValue"]);

                        list.Add(new Report_Sells_Day_ReportDetail(Bill_Date, Bill_ID, SellType,Bill_Owner , ClauseS_Count, BillValue,
                        CurrencyID, CurrencyName, CurrencySymbol, ExchangeRate, PaysCount, PaysAmount, PaysRemain,
                        Source_ItemsIN_Cost_Details, Source_ItemsIN_RealCost, ItemsOut_RealValue, RealPaysValue));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Day_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {
                try
                {
                    listview.Name = "ListViewSellsDay";
                    listview.Columns.Clear();
                    listview.Columns.Add("الوقت");
                    listview.Columns.Add("الرقم");
                    listview.Columns.Add("نمط البيع");
                    listview.Columns.Add("باسم");
                    listview.Columns.Add("البنود");
                    listview.Columns.Add("القيمة");
                    listview.Columns.Add("سعر الصرف");
                    //listview.Columns.Add("عدد الدفعات");
                    listview.Columns.Add("المدفوع");
                    listview.Columns.Add("الباقي");
                    listview.Columns.Add("تكلفة المواد");
                    listview.Columns.Add("التكلفة الفعلية");
                    listview.Columns.Add("قيمة الفاتورة الفعلية");
                    listview.Columns.Add("الربح الفعلي");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("OperationReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 75;//time
                    listview.Columns[1].Width = 60;//id
                    listview.Columns[2].Width = 75;//selltype
                    listview.Columns[3].Width = 100;//owner
                    listview.Columns[4].Width = 65;//clause count
                    listview.Columns[5].Width = 90;//value
                    listview.Columns[6].Width = 100;//exchangerate
                    listview.Columns[7].Width = 130;//paid
                    listview.Columns[8].Width = 90;//remain
                    listview.Columns[9].Width = 100;//item in cost
                    listview.Columns[10].Width = 130;//real item in cost
                    listview.Columns[11].Width = 150;//real items out cost
                    listview.Columns[12].Width = 130;//profit value
                    listview.Columns[13].Width = 130;//real pays value

                }
                catch (Exception ee)
                {
                    MessageBox.Show("OperationReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_Sells_Month_ReportDetail > Get_Report_Sells_Month_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_Sells_Month_ReportDetail> list = new List<Report_Sells_Month_ReportDetail>(); 
                    for (int i=0;i<table .Rows .Count;i++)
                    {
                        int DayID = Convert.ToInt32(table.Rows[i]["DayID"]);
                        DateTime DayDate = Convert.ToDateTime(table.Rows[i]["DayDate"]);
                        int Bills_Count = Convert.ToInt32(table.Rows[i]["Bills_Count"]);
                        int Bills_Clause_Count = Convert.ToInt32(table.Rows[i]["Bills_Clause_Count"]);
                        string Bills_Value = table.Rows[i]["Bills_Value"].ToString();
                        string Bills_Pays_Value = table.Rows[i]["Bills_Pays_Value"].ToString();
                        string Bills_Pays_Remain = table.Rows[i]["Bills_Pays_Remain"].ToString();
                        double Bills_Pays_Remain_UPON_BillsCurrency = Convert.ToDouble(table.Rows[i]["Bills_Pays_Remain_UPON_BillsCurrency"]);
                        string Bills_ItemsIN_Value = table.Rows[i]["Bills_ItemsIN_Value"].ToString();
                        double Bills_ItemsIN_RealValue = Convert.ToDouble(table.Rows[i]["Bills_ItemsIN_RealValue"]);
                        double Bills_RealValue = Convert.ToDouble(table.Rows[i]["Bills_RealValue"]);
                        double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_RealValue"]);


                        list.Add ( new Objects.Report_Sells_Month_ReportDetail(DayID, DayDate, Bills_Count, Bills_Clause_Count, Bills_Value, Bills_Pays_Value, Bills_Pays_Remain,
                 Bills_Pays_Remain_UPON_BillsCurrency, Bills_ItemsIN_Value, Bills_ItemsIN_RealValue, Bills_RealValue,
                Bills_Pays_RealValue));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Month_ReportDetail_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewSells_Month";
                    listview.Columns.Clear();
                    listview.Columns.Add("اليوم");
                    listview.Columns.Add("اجمالي الفواتير");
                    listview.Columns.Add("اجمالي البنود");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add("اجمالي المدفوع");
                    listview.Columns.Add("المتبقي");
                    listview.Columns.Add("كلفة المواد المباعة");
                    listview.Columns.Add("الكلفة الفعلية");
                    listview.Columns.Add("قيمة الفواتير الفعلية");
                    listview.Columns.Add("الربح الفعلي");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillMonthReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 120;//bills count
                    listview.Columns[2].Width = 115;//clause count
                    listview.Columns[3].Width = 125;//bill value
                    listview.Columns[4].Width = 125;//bills pays value
                    listview.Columns[5].Width = 120;//remain
                    listview.Columns[6].Width = 140;//item in value
                    listview.Columns[7].Width = 115;//item in real value
                    listview.Columns[8].Width = 145;//real value
                    listview.Columns[9].Width = 115;//profit
                    listview.Columns[10].Width = 125;//real p


                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillMonthReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List < Report_Sells_Year_ReportDetail> Get_Report_Sells_Year_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_Sells_Year_ReportDetail> list = new List<Report_Sells_Year_ReportDetail>();
                     for (int i = 0;i< table.Rows.Count ;i++)
                    {
                        int MonthNO = Convert.ToInt32(table.Rows[i]["MonthNO"]);
                        string MonthName = table.Rows[i]["MonthName"].ToString();
                        int Bills_Count = Convert.ToInt32(table.Rows[i]["Bills_Count"]);
                        int Bills_Clause_Count = Convert.ToInt32(table.Rows[i]["Bills_Clause_Count"]);
                        string Bills_Value = table.Rows[i]["Bills_Value"].ToString();
                        string Bills_Pays_Value = table.Rows[i]["Bills_Pays_Value"].ToString();
                        string Bills_Pays_Remain = table.Rows[i]["Bills_Pays_Remain"].ToString();
                        double Bills_Pays_Remain_UPON_BillsCurrency = Convert.ToDouble(table.Rows[i]["Bills_Pays_Remain_UPON_BillsCurrency"]);
                        string Bills_ItemsIN_Value = table.Rows[i]["Bills_ItemsIN_Value"].ToString();
                        double Bills_ItemsIN_RealValue = Convert.ToDouble(table.Rows[i]["Bills_ItemsIN_RealValue"]);
                        double Bills_RealValue = Convert.ToDouble(table.Rows[i]["Bills_RealValue"]);
                        double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_RealValue"]);


                        list .Add ( new Objects.Report_Sells_Year_ReportDetail(MonthNO, MonthName, Bills_Count, Bills_Clause_Count, Bills_Value, Bills_Pays_Value, Bills_Pays_Remain,
                 Bills_Pays_Remain_UPON_BillsCurrency, Bills_ItemsIN_Value, Bills_ItemsIN_RealValue, Bills_RealValue,
                Bills_Pays_RealValue));
                    }
                    return list; 
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_Year_ReportDetail_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewSells_Year";
                    listview.Columns.Clear();
                    listview.Columns.Add("رقم الشهر");
                    listview.Columns.Add("الشهر");
                    listview.Columns.Add("اجمالي الفواتير");
                    listview.Columns.Add("اجمالي البنود");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add("اجمالي المدفوع");
                    listview.Columns.Add("المتبقي");
                    listview.Columns.Add("كلفة المواد المباعة");
                    listview.Columns.Add("الكلفة الفعلية");
                    listview.Columns.Add("قيمة الفواتير الفعلية");
                    listview.Columns.Add("الربح الفعلي");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//month
                    listview.Columns[1].Width = 100;//month
                    listview.Columns[2].Width = 120;//bills count
                    listview.Columns[3].Width = 115;//clause count
                    listview.Columns[4].Width = 125;//bill value
                    listview.Columns[5].Width = 125;//bills pays value
                    listview.Columns[6].Width = 120;//remain
                    listview.Columns[7].Width = 140;//item in value
                    listview.Columns[8].Width = 115;//item in real value
                    listview.Columns[9].Width = 145;//real value
                    listview.Columns[10].Width = 115;//profit
                    listview.Columns[11].Width = 125;//real p


                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List < Report_Sells_YearRange_ReportDetail > Get_Report_Sells_YearRange_ReportDetail_From_DataTable(DataTable table)
            {

                try
                {
                    List<Report_Sells_YearRange_ReportDetail> list = new List<Report_Sells_YearRange_ReportDetail>();
                    for (int i=0;i<table .Rows .Count;i++ )
                    {
                        int YearNO = Convert.ToInt32(table.Rows[i]["YearNO"]);
                        int Bills_Count = Convert.ToInt32(table.Rows[i]["Bills_Count"]);
                        int Bills_Clause_Count = Convert.ToInt32(table.Rows[i]["Bills_Clause_Count"]);
                        string Bills_Value = table.Rows[i]["Bills_Value"].ToString();
                        string Bills_Pays_Value = table.Rows[i]["Bills_Pays_Value"].ToString();
                        string Bills_Pays_Remain = table.Rows[i]["Bills_Pays_Remain"].ToString();
                        double Bills_Pays_Remain_UPON_BillsCurrency = Convert.ToDouble(table.Rows[i]["Bills_Pays_Remain_UPON_BillsCurrency"]);
                        string Bills_ItemsIN_Value = table.Rows[i]["Bills_ItemsIN_Value"].ToString();
                        double Bills_ItemsIN_RealValue = Convert.ToDouble(table.Rows[i]["Bills_ItemsIN_RealValue"]);
                        double Bills_RealValue = Convert.ToDouble(table.Rows[i]["Bills_RealValue"]);
                        double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bills_Pays_RealValue"]);


                        list .Add ( new Objects.Report_Sells_YearRange_ReportDetail(YearNO, Bills_Count, Bills_Clause_Count, Bills_Value, Bills_Pays_Value, Bills_Pays_Remain,
                 Bills_Pays_Remain_UPON_BillsCurrency, Bills_ItemsIN_Value, Bills_ItemsIN_RealValue, Bills_RealValue,
                Bills_Pays_RealValue));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_Sells_YearRange_ReportDetail_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewSells_YearRange";
                    listview.Columns.Clear();
                    listview.Columns.Add("السنة");
                    listview.Columns.Add("اجمالي الفواتير");
                    listview.Columns.Add("اجمالي البنود");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add("اجمالي المدفوع");
                    listview.Columns.Add("المتبقي");
                    listview.Columns.Add("كلفة المواد المباعة");
                    listview.Columns.Add("الكلفة الفعلية");
                    listview.Columns.Add("قيمة الفواتير الفعلية");
                    listview.Columns.Add("الربح الفعلي");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearRangeReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//year
                    listview.Columns[1].Width = 120;//bills count
                    listview.Columns[2].Width = 115;//clause count
                    listview.Columns[3].Width = 125;//bill value
                    listview.Columns[4].Width = 125;//bills pays value
                    listview.Columns[5].Width = 120;//remain
                    listview.Columns[6].Width = 140;//item in value
                    listview.Columns[7].Width = 115;//item in real value
                    listview.Columns[8].Width = 145;//real value
                    listview.Columns[9].Width = 115;//profit
                    listview.Columns[10].Width = 125;//real p


                }
                catch (Exception ee)
                {
                    MessageBox.Show("BillYearRangeReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            internal static List<Report_PayOrders_Day_ReportDetail> Get_Report_PayOrders_Day_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {

                    List<Report_PayOrders_Day_ReportDetail> list = new List<Objects.Report_PayOrders_Day_ReportDetail>();

                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        DateTime PayOrder_Time = Convert.ToDateTime(table.Rows[i]["PayOrder_Time"]);
                        bool PayOrderType = Convert.ToBoolean (table.Rows[i]["PayOrderType"]);
                        uint PayOrderID =Convert .ToUInt32 ( table.Rows[i]["PayOrderID"]);
                        string PayOrderDesc = table.Rows[i]["PayOrderDesc"].ToString();
                        uint EmployeeID = Convert.ToUInt32(table.Rows[i]["EmployeeID"]);
                        string EmployeeName = table.Rows[i]["EmployeeName"].ToString ();
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        double  Value = Convert.ToDouble (table.Rows[i]["Value"]);
                        string PaysAmount = table.Rows[i]["PaysAmount"].ToString();
                        double PaysRemain = Convert.ToDouble(table.Rows[i]["PaysRemain"]);
                        double  RealValue =Convert .ToDouble ( table.Rows[i]["RealValue"]);
                        double RealPays = Convert.ToDouble(table.Rows[i]["RealPays"]);

                        list.Add(new Report_PayOrders_Day_ReportDetail(
            PayOrder_Time,
             PayOrderType,
             PayOrderID,

             PayOrderDesc,
             EmployeeID,
           EmployeeName,
          Value,

            CurrencyID,
             CurrencyName,
             CurrencySymbol,

             ExchangeRate,
            PaysAmount,
             PaysRemain,
            RealValue,
             RealPays));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Day_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }


            public static void IntiliazeListView(ref ListView listview)
            {
                try
                {
                    listview.Name = "ListViewEmployeesDay";
                    listview.Columns.Clear();
                    listview.Columns.Add("الوقت");
                    listview.Columns.Add("طبيعة الأمر");
                    listview.Columns.Add("الرقم");
                    listview.Columns.Add("الموظف");
                    listview.Columns.Add("الوصف");
                    listview.Columns.Add("القيمة");
                    listview.Columns.Add("سعر الصرف");
                    listview.Columns.Add("المدفوع");
                    listview.Columns.Add("الباقي");
                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");

                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Report_PayOrders_Day_ReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 75;//time
                    listview.Columns[1].Width = 125;//type
                    listview.Columns[2].Width = 100;//id
                    listview.Columns[3].Width = 200;//owner
                    listview.Columns[4].Width = 200;//desc
                    listview.Columns[5].Width = 125;//value
                    listview.Columns[6].Width = 100;//exchangerate
                    listview.Columns[7].Width = 125;//paid
                    listview.Columns[8].Width = 125;//remain
                    listview.Columns[9].Width = 100;//real value
                    listview.Columns[10].Width = 100;//real paid


                }
                catch (Exception ee)
                {
                    MessageBox.Show("Report_PayOrders_Day_ReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_PayOrders_Month_ReportDetail> Get_Report_PayOrders_Month_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {

                    List<Report_PayOrders_Month_ReportDetail> list = new List<Objects.Report_PayOrders_Month_ReportDetail>();
                   

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        int DayID = Convert.ToInt32(table.Rows[i]["DayID"]);
                        DateTime DayDate = Convert.ToDateTime(table.Rows[i]["DayDate"]);
                        int Salary_PayOrders_Count = Convert.ToInt32 (table.Rows[i]["Salary_PayOrders_Count"]);
                        int Other_PayOrders_Count = Convert.ToInt32 (table.Rows[i]["Other_PayOrders_Count"]);
                        string PayOrders_Value = table.Rows[i]["PayOrders_Value"].ToString();
                        string PayOrders_Pays_Value = table.Rows[i]["PayOrders_Pays_Value"].ToString();
                        string PayOrders_Pays_Remain = table.Rows[i]["PayOrders_Pays_Remain"].ToString();
                        double PayOrders_Pays_Remain_UPON_PayOrdersCurrency = Convert.ToDouble(table.Rows[i]["PayOrders_Pays_Remain_UPON_PayOrdersCurrency"]);
                        double PayOrders_RealValue = Convert.ToDouble(table.Rows[i]["PayOrders_RealValue"]);
                        double PayOrders_Pays_RealValue = Convert.ToDouble(table.Rows[i]["PayOrders_Pays_RealValue"]);
                  

                        list.Add(new Report_PayOrders_Month_ReportDetail(
             DayID,
            DayDate,
             Salary_PayOrders_Count,
            Other_PayOrders_Count,
             PayOrders_Value,
             PayOrders_Pays_Value,
             PayOrders_Pays_Remain,

             PayOrders_Pays_Remain_UPON_PayOrdersCurrency,
            PayOrders_RealValue,
             PayOrders_Pays_RealValue
));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Month_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewPayOrders_Month";
                    listview.Columns.Clear();
                    listview.Columns.Add("اليوم");
                    listview.Columns.Add("عدد اوامر صرف الرواتب");
                    listview.Columns.Add("عدد اوامر الصرف الاخرى");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("PayOrderMonthReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 175;//salary count
                    listview.Columns[2].Width = 175;//other count

                    listview.Columns[3].Width = 120;// value
                    listview.Columns[4].Width = 140;//paid
                    listview.Columns[5].Width = 115;//remain

                    listview.Columns[6].Width = 125;//real value
                    listview.Columns[7].Width = 125;//real pays value

                }
                catch (Exception ee)
                {
                    MessageBox.Show("PayOrderMonthReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_PayOrders_Year_ReportDetail> Get_Report_PayOrders_Year_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {

                    List<Report_PayOrders_Year_ReportDetail> list = new List<Objects.Report_PayOrders_Year_ReportDetail>();


                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        int MonthNO = Convert.ToInt32(table.Rows[i]["MonthNO"]);
                        string  MonthName =table.Rows[i]["MonthName"].ToString ();
                        int Salary_PayOrders_Count = Convert.ToInt32(table.Rows[i]["Salary_PayOrders_Count"]);
                        int Other_PayOrders_Count = Convert.ToInt32(table.Rows[i]["Other_PayOrders_Count"]);
                        string PayOrders_Value = table.Rows[i]["PayOrders_Value"].ToString();
                        string PayOrders_Pays_Value = table.Rows[i]["PayOrders_Pays_Value"].ToString();
                        string PayOrders_Pays_Remain = table.Rows[i]["PayOrders_Pays_Remain"].ToString();
                        double PayOrders_Pays_Remain_UPON_PayOrdersCurrency = Convert.ToDouble(table.Rows[i]["PayOrders_Pays_Remain_UPON_PayOrdersCurrency"]);
                        double PayOrders_RealValue = Convert.ToDouble(table.Rows[i]["PayOrders_RealValue"]);
                        double PayOrders_Pays_RealValue = Convert.ToDouble(table.Rows[i]["PayOrders_Pays_RealValue"]);


                        list.Add(new Report_PayOrders_Year_ReportDetail(
             MonthNO,
            MonthName,
             Salary_PayOrders_Count,
            Other_PayOrders_Count,
             PayOrders_Value,
             PayOrders_Pays_Value,
             PayOrders_Pays_Remain,

             PayOrders_Pays_Remain_UPON_PayOrdersCurrency,
            PayOrders_RealValue,
             PayOrders_Pays_RealValue
));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_Year_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewPayOrders_Month";
                    listview.Columns.Clear();
                    listview.Columns.Add("رقم الشهر");

                    listview.Columns.Add("الشهر");
                    listview.Columns.Add("عدد اوامر صرف الرواتب");
                    listview.Columns.Add("عدد اوامر الصرف الاخرى");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("PayOrderYearReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 100;//daydate

                    listview.Columns[2].Width = 175;//salary count
                    listview.Columns[3].Width = 175;//other count

                    listview.Columns[4].Width = 120;// value
                    listview.Columns[5].Width = 140;//paid
                    listview.Columns[6].Width = 115;//remain

                    listview.Columns[7].Width = 125;//real value
                    listview.Columns[8].Width = 125;//real pays value

                }
                catch (Exception ee)
                {
                    MessageBox.Show("PayOrderYearReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static List<Report_PayOrders_YearRange_ReportDetail> Get_Report_PayOrders_YearRange_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {

                    List<Report_PayOrders_YearRange_ReportDetail> list = new List<Objects.Report_PayOrders_YearRange_ReportDetail>();


                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        int YearNO = Convert.ToInt32(table.Rows[i]["YearNO"]);
                        int Salary_PayOrders_Count = Convert.ToInt32(table.Rows[i]["Salary_PayOrders_Count"]);
                        int Other_PayOrders_Count = Convert.ToInt32(table.Rows[i]["Other_PayOrders_Count"]);
                        string PayOrders_Value = table.Rows[i]["PayOrders_Value"].ToString();
                        string PayOrders_Pays_Value = table.Rows[i]["PayOrders_Pays_Value"].ToString();
                        string PayOrders_Pays_Remain = table.Rows[i]["PayOrders_Pays_Remain"].ToString();
                        double PayOrders_Pays_Remain_UPON_PayOrdersCurrency = Convert.ToDouble(table.Rows[i]["PayOrders_Pays_Remain_UPON_PayOrdersCurrency"]);
                        double PayOrders_RealValue = Convert.ToDouble(table.Rows[i]["PayOrders_RealValue"]);
                        double PayOrders_Pays_RealValue = Convert.ToDouble(table.Rows[i]["PayOrders_Pays_RealValue"]);


                        list.Add(new Report_PayOrders_YearRange_ReportDetail(
             YearNO,
             Salary_PayOrders_Count,
            Other_PayOrders_Count,
             PayOrders_Value,
             PayOrders_Pays_Value,
             PayOrders_Pays_Remain,

             PayOrders_Pays_Remain_UPON_PayOrdersCurrency,
            PayOrders_RealValue,
             PayOrders_Pays_RealValue
));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Report_PayOrders_YearRange_ReportDetail_List_AS_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {

                try
                {
                    listview.Name = "ListViewPayOrders_Month";
                    listview.Columns.Clear();
                    listview.Columns.Add("السنة");
                    listview.Columns.Add("عدد اوامر صرف الرواتب");
                    listview.Columns.Add("عدد اوامر الصرف الاخرى");
                    listview.Columns.Add("القيمة الكلية");
                    listview.Columns.Add(" المدفوع");
                    listview.Columns.Add("المتبقي");

                    listview.Columns.Add("القيمة الفعلية");
                    listview.Columns.Add("المدفوع الفعلي");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("PayOrderYearRangeReportDetail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 100;//daydate
                    listview.Columns[1].Width = 175;//salary count
                    listview.Columns[2].Width = 175;//other count

                    listview.Columns[3].Width = 120;// value
                    listview.Columns[4].Width = 140;//paid
                    listview.Columns[5].Width = 115;//remain

                    listview.Columns[6].Width = 125;//real value
                    listview.Columns[7].Width = 125;//real pays value

                }
                catch (Exception ee)
                {
                    MessageBox.Show("PayOrderYearRangeReportDetail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region AccountMoney
        public class PayCurrencyReport
        {

            public uint CurrencyID;
            public string  CurrencyName;
            public string  CurrencySymbol;

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
            internal static List<PayCurrencyReport> Get_PayCurrencyReport_List_From_DataTable(DataTable table)
            {
                try
                {

                    List<PayCurrencyReport> list = new List<Objects.PayCurrencyReport>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();

                        double PaysIN_Sell = Convert.ToDouble(table.Rows[i]["PaysIN_Sell"]);

                        double PaysIN_Maintenance = Convert.ToDouble(table.Rows[i]["PaysIN_Maintenance"]);

                        double PaysIN_MoneyTransform = Convert.ToDouble(table.Rows[i]["PaysIN_MoneyTransform"]);

                        double PaysIN_NON = Convert.ToDouble(table.Rows[i]["PaysIN_NON"]);

                        double PaysIN_Exchange = Convert.ToDouble(table.Rows[i]["PaysIN_Exchange"]);

                        double PaysOUT_Buy = Convert.ToDouble(table.Rows[i]["PaysOUT_Buy"]);

                        double PaysOUT_Emp = Convert.ToDouble(table.Rows[i]["PaysOUT_Emp"]);

                        double PaysOUT_MoneyTransform = Convert.ToDouble(table.Rows[i]["PaysOUT_MoneyTransform"]);

                        double PaysOUT_NON = Convert.ToDouble(table.Rows[i]["PaysOUT_NON"]);

                        double PaysOUT_Exchange= Convert.ToDouble(table.Rows[i]["PaysOUT_Exchange"]);

                        
                        list.Add(new PayCurrencyReport( CurrencyID, CurrencyName, CurrencySymbol,PaysIN_Sell ,PaysIN_Maintenance ,PaysIN_MoneyTransform 
                            ,PaysIN_NON ,PaysIN_Exchange,PaysOUT_Buy,PaysOUT_Emp ,PaysOUT_MoneyTransform ,PaysOUT_NON ,PaysOUT_Exchange));

                    }
                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_PayCurrencyReport_List_From_DataTable:" + ee.Message);
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
            internal static List<AccountOprReportDetail> Get_AccountOprReportDetail_List_From_DataTable(DataTable table)
            {
                try
                {

                    List<AccountOprReportDetail> list = new List<Objects.AccountOprReportDetail>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DateTime OprTime = Convert.ToDateTime(table.Rows[i]["OprTime"]);
                        uint OprType = Convert.ToUInt32(table.Rows[i]["OprType"]);
                        bool OprDirection = Convert.ToBoolean(table.Rows[i]["OprDirection"]); ;

                        uint OprID = Convert.ToUInt32(table.Rows[i]["OprID"]);
                        string OprOwner = table.Rows[i]["OprOwner"].ToString();
                        double Value = Convert.ToDouble(table.Rows[i]["Value"]);
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        double RealValue = Convert.ToDouble(table.Rows[i]["RealValue"]);
                        list.Add(new AccountOprReportDetail(OprTime ,OprType, OprDirection, OprID,OprOwner , Value,
                            CurrencyID, CurrencyName, CurrencySymbol, ExchangeRate,
                            RealValue));

                    }
                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprReportDetail_List_From_DataTable:" + ee.Message);
                }
            }

            public static void IntiliazeListView(ref ListView listview)
            {
                listview.Name = "ListViewMoneyDataDetails_Day";

                listview.Columns.Clear();
                listview.Columns.Add("الوقت");
                listview.Columns.Add("التصنيف");
                listview.Columns.Add("الاتجاه");
                listview.Columns.Add("المعرف");

                listview.Columns.Add("القيمة");
                listview.Columns.Add("سعر الصرف");
                listview.Columns.Add("القيمة الفعلية");
                listview.Columns.Add("عائدة لـ");
                for (int i = 0; i < listview.Columns.Count; i++)
                    listview.Columns[i].TextAlign = HorizontalAlignment.Center;
                IntializeListViewColumnsWidth(ref listview);
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {
                //MessageBox.Show("f");
                listview.Columns[0].Width = 100;//
                listview.Columns[1].Width = 100;
                listview.Columns[2].Width = 150;
                listview.Columns[3].Width = 100;
                listview.Columns[4].Width = 200;

                listview.Columns[5].Width = 150;
                listview.Columns[6].Width = 200;

                listview.Columns[7].Width = listview.Width - 905;
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

            internal static List<AccountOprDayReportDetail> Get_AccountOprDayReportDetail_List_From_DataTable(DataTable table)
            {
                try
                {

                    List<AccountOprDayReportDetail> list = new List<Objects.AccountOprDayReportDetail>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        
                        int DateDayNo = Convert.ToInt32(table.Rows[i]["DateDayNo"]);
                        DateTime Date_day = Convert.ToDateTime(table.Rows[i]["Date_day"]);

                        int PaysIN_Count = Convert.ToInt32 (table.Rows[i]["PaysIN_Count"]); ;
                        int PaysOUT_Count = Convert.ToInt32(table.Rows[i]["PaysOUT_Count"]); ;
                        int Exchange_Count = Convert.ToInt32(table.Rows[i]["Exchange_Count"]); ;
                        int MoneyTransform_IN_Count = Convert.ToInt32(table.Rows[i]["MoneyTransform_IN_Count"]); ;
                        int MoneyTransform_OUT_Count = Convert.ToInt32(table.Rows[i]["MoneyTransform_OUT_Count"]); ;

                        string PaysIN_Value = table.Rows[i]["PaysIN_Value"].ToString();
                        double PaysIN_Real_Value = Convert.ToDouble(table.Rows[i]["PaysIN_Real_Value"]);
                        string PaysOUT_Value = table.Rows[i]["PaysOUT_Value"].ToString();
                        double PaysOUT_Real_Value = Convert.ToDouble(table.Rows[i]["PaysOUT_Real_Value"]);
                        list.Add(new AccountOprDayReportDetail(DateDayNo ,Date_day ,PaysIN_Count,PaysOUT_Count 
                            ,Exchange_Count,MoneyTransform_IN_Count,MoneyTransform_OUT_Count
                            ,PaysIN_Value,PaysIN_Real_Value ,PaysOUT_Value ,PaysOUT_Real_Value));

                    }
                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprDayReportDetail_List_From_DataTable:" + ee.Message);
                }
            }

            public static void IntiliazeListView(ref ListView listview)
            {
                listview.Name = "ListViewMoneyDataDetails_Month";
                listview.Columns.Clear();
                listview.Columns.Add("اليوم");
                listview.Columns.Add(" الدفعات الداخلة");
                listview.Columns.Add(" الدفعات الخارجة");
                listview.Columns.Add(" عمليات الصرف");
                listview.Columns.Add(" عمليات التحويل الواردة");
                listview.Columns.Add(" عمليات التحويل الصادرة");
                listview.Columns.Add(" الداخل الى الصندوق");
                listview.Columns.Add(" الخارج من الصندوق");
                listview.Columns.Add(" قيمة الداخل الفعلية");
                listview.Columns.Add(" قيمة الخارج الفعلية");
                listview.Columns.Add(" الصافي الفعلي");
                for (int i = 0; i < listview.Columns.Count; i++)
                    listview.Columns[i].TextAlign = HorizontalAlignment.Center;


                IntializeListViewColumnsWidth(ref listview);
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {
                listview.Columns[0].Width = 100;
                listview.Columns[1].Width = 150;
                listview.Columns[2].Width = 150;
                listview.Columns[3].Width = 150;
                listview.Columns[4].Width = 150;
                listview.Columns[5].Width = 150;
                listview.Columns[6].Width = 150;
                listview.Columns[7].Width = 150;
                listview.Columns[8].Width = 150;
                listview.Columns[9].Width = 150;
                listview.Columns[10].Width = 150;
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
            internal static List<AccountOprMonthReportDetail> Get_AccountOprMonthReportDetail_List_From_DataTable(DataTable table)
            {
                try
                {

                    List<AccountOprMonthReportDetail> list = new List<Objects.AccountOprMonthReportDetail>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        int Year_Month = Convert.ToInt32(table.Rows[i]["Year_Month"]);
                        string  Year_Month_Name = table.Rows[i]["Year_Month_Name"].ToString ();

                        int PaysIN_Count = Convert.ToInt32(table.Rows[i]["PaysIN_Count"]); ;
                        int PaysOUT_Count = Convert.ToInt32(table.Rows[i]["PaysOUT_Count"]); ;
                        int Exchange_Count = Convert.ToInt32(table.Rows[i]["Exchange_Count"]); ;
                        int MoneyTransform_IN_Count = Convert.ToInt32(table.Rows[i]["MoneyTransform_IN_Count"]); ;
                        int MoneyTransform_OUT_Count = Convert.ToInt32(table.Rows[i]["MoneyTransform_OUT_Count"]); ;

                        string PaysIN_Value = table.Rows[i]["PaysIN_Value"].ToString();
                        double PaysIN_Real_Value = Convert.ToDouble(table.Rows[i]["PaysIN_Real_Value"]);
                        string PaysOUT_Value = table.Rows[i]["PaysOUT_Value"].ToString();
                        double PaysOUT_Real_Value = Convert.ToDouble(table.Rows[i]["PaysOUT_Real_Value"]);
                        list.Add(new AccountOprMonthReportDetail(Year_Month , Year_Month_Name, PaysIN_Count, PaysOUT_Count
                            , Exchange_Count, MoneyTransform_IN_Count, MoneyTransform_OUT_Count
                            , PaysIN_Value, PaysIN_Real_Value, PaysOUT_Value, PaysOUT_Real_Value));

                    }
                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprMonthReportDetail_List_From_DataTable:" + ee.Message);
                }
            }
            public static void IntiliazeListView(ref ListView listview)
            {
                listview.Name = "ListViewMoneyDataDetails_Year";
                listview.Columns.Clear();
                listview.Columns.Add("رقم الشهر");
                listview.Columns.Add("الشهر");
                listview.Columns.Add(" الدفعات الداخلة");
                listview.Columns.Add(" الدفعات الخارجة");
                listview.Columns.Add(" عمليات الصرف");
                listview.Columns.Add(" عمليات التحويل الواردة");
                listview.Columns.Add(" عمليات التحويل الصادرة");
                listview.Columns.Add(" الداخل الى الصندوق");
                listview.Columns.Add(" الخارج من الصندوق");
                listview.Columns.Add(" قيمة الداخل الفعلية");
                listview.Columns.Add(" قيمة الخارج الفعلية");
                listview.Columns.Add(" الصافي الفعلي");
                for (int i = 0; i < listview.Columns.Count; i++)
                    listview.Columns[i].TextAlign = HorizontalAlignment.Center;


                IntializeListViewColumnsWidth(ref listview);
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {
                listview.Columns[0].Width = 100;
                listview.Columns[1].Width = 100;
                listview.Columns[2].Width = 150;
                listview.Columns[3].Width = 150;
                listview.Columns[4].Width = 150;
                listview.Columns[5].Width = 150;
                listview.Columns[6].Width = 150;
                listview.Columns[7].Width = 150;
                listview.Columns[8].Width = 150;
                listview.Columns[9].Width = 150;
                listview.Columns[10].Width = 150;
                listview.Columns[11].Width = 150;
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
            internal static List<AccountOprYearReportDetail> Get_AccountOprYearReportDetail_List_From_DataTable(DataTable table)
            {
                try
                {

                    List<AccountOprYearReportDetail> list = new List<Objects.AccountOprYearReportDetail>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        int AccountYear = Convert.ToInt32(table.Rows[i]["AccountYear"]);

                        int PaysIN_Count = Convert.ToInt32(table.Rows[i]["PaysIN_Count"]); ;
                        int PaysOUT_Count = Convert.ToInt32(table.Rows[i]["PaysOUT_Count"]); ;
                        int Exchange_Count = Convert.ToInt32(table.Rows[i]["Exchange_Count"]); ;
                        int MoneyTransform_IN_Count = Convert.ToInt32(table.Rows[i]["MoneyTransform_IN_Count"]); ;
                        int MoneyTransform_OUT_Count = Convert.ToInt32(table.Rows[i]["MoneyTransform_OUT_Count"]); ;

                        string PaysIN_Value = table.Rows[i]["PaysIN_Value"].ToString();
                        double PaysIN_Real_Value = Convert.ToDouble(table.Rows[i]["PaysIN_Real_Value"]);
                        string PaysOUT_Value = table.Rows[i]["PaysOUT_Value"].ToString();
                        double PaysOUT_Real_Value = Convert.ToDouble(table.Rows[i]["PaysOUT_Real_Value"]);
                        list.Add(new AccountOprYearReportDetail(AccountYear, PaysIN_Count, PaysOUT_Count
                            , Exchange_Count, MoneyTransform_IN_Count, MoneyTransform_OUT_Count
                            , PaysIN_Value, PaysIN_Real_Value, PaysOUT_Value, PaysOUT_Real_Value));

                    }
                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AccountOprYearReportDetail_List_From_DataTable:" + ee.Message);
                }
            }

            public static void IntiliazeListView(ref ListView listview)
            {
                listview.Name = "ListViewMoneyDataDetails_Range";
                listview.Columns.Clear();
                listview.Columns.Add("السنة");
                listview.Columns.Add(" الدفعات الداخلة");
                listview.Columns.Add(" الدفعات الخارجة");
                listview.Columns.Add(" عمليات الصرف");
                listview.Columns.Add(" عمليات التحويل الواردة");
                listview.Columns.Add(" عمليات التحويل الصادرة");
                listview.Columns.Add(" الداخل الى الصندوق");
                listview.Columns.Add(" الخارج من الصندوق");
                listview.Columns.Add(" قيمة الداخل الفعلية");
                listview.Columns.Add(" قيمة الخارج الفعلية");
                listview.Columns.Add(" الصافي الفعلي");
                for (int i = 0; i < listview.Columns.Count; i++)
                    listview.Columns[i].TextAlign = HorizontalAlignment.Center;


                IntializeListViewColumnsWidth(ref listview);
            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {
                listview.Columns[0].Width = 100;
                listview.Columns[1].Width = 150;
                listview.Columns[2].Width = 150;
                listview.Columns[3].Width = 150;
                listview.Columns[4].Width = 150;
                listview.Columns[5].Width = 150;
                listview.Columns[6].Width = 150;
                listview.Columns[7].Width = 150;
                listview.Columns[8].Width = 150;
                listview.Columns[9].Width = 150;
                listview.Columns[10].Width = 150;
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
            internal static List<Contact_BillCurrencyReport> Get_Contact_BillCurrencyReport_From_DataTable(DataTable table)
            {

                try
                {
                    List<Contact_BillCurrencyReport> list = new List<Contact_BillCurrencyReport>() ;


                    ;
                    for (int i = 0; i < table .Rows .Count; i++)
                    {
                        uint CurrencyID = Convert .ToUInt32 ( table .Rows [i]["CurrencyID"]);
                       string CurrencyName= table.Rows[i]["CurrencyName"].ToString();
                       string CurrencySymbol= table.Rows[i]["CurrencySymbol"].ToString ();
                       int BillINCount= Convert .ToInt32 ( table.Rows[i]["BillINCount"]) ;
                      double BillINValue= Convert.ToDouble(table.Rows[i]["BillINValue"]) ;
                     double BillIN_PaysValue= Convert.ToDouble(table.Rows[i]["BillIN_PaysValue"]);
                      int BillMaintenanceCount= Convert.ToInt32(table.Rows[i]["BillMaintenanceCount"]) ;
                      double BillMaintenanceValue= Convert.ToDouble(table.Rows[i]["BillMaintenanceValue"]) ;
                      double BillMaintenance_PaysValue= Convert.ToDouble(table.Rows[i]["BillMaintenance_PaysValue"] );
                      int BillOUTCount= Convert.ToInt32(table.Rows[i]["BillOUTCount"] );
                        double BillOUTValue = Convert.ToDouble(table.Rows[i]["BillOUTValue"]) ;
                        double BillOUT_PaysValue = Convert.ToDouble(table.Rows[i]["BillOUT_PaysValue"]) ;
                        list.Add(new Contact_BillCurrencyReport(  CurrencyID,
                                     CurrencyName,
                                     CurrencySymbol,
                                    BillINCount,
                                     BillINValue,
                                     BillIN_PaysValue,
                                    BillMaintenanceCount,
                                   BillMaintenanceValue,
                                     BillMaintenance_PaysValue,
                                     BillOUTCount,
                                    BillOUTValue,
                                    BillOUT_PaysValue));
                    }
                    return list;
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
            internal static List<Contact_Pays_ReportDetail> Get_Contact_Contact_Pays_ReportDetail_List_From_DataTable(DataTable table)
            {
                try
                {

                    List<Contact_Pays_ReportDetail> list = new List<Objects.Contact_Pays_ReportDetail>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        uint PayOPR_ID = Convert.ToUInt32(table.Rows[i]["PayOPR_ID"]);
                        bool PayDirection = Convert.ToBoolean(table.Rows[i]["PayDirection"]); ;
                        DateTime PayDate = Convert.ToDateTime(table.Rows[i]["PayDate"]);
                        double Value = Convert.ToDouble(table.Rows[i]["Value"]);
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        double RealValue = Convert.ToDouble(table.Rows[i]["RealValue"]);
                        uint OperationID = Convert.ToUInt32(table.Rows[i]["OperationID"]);
                        uint OperationType = Convert.ToUInt32(table.Rows[i]["OperationType"]);
                        list.Add(new Contact_Pays_ReportDetail(PayOPR_ID,
                             PayDirection, PayDate, Value, CurrencyID, CurrencyName, CurrencySymbol, ExchangeRate,
                            RealValue, OperationID, OperationType));

                    }
                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Contact_Pays_ReportDetail_List_From_DataTable:" + ee.Message);
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
            internal static List<Contact_PayCurrencyReport> Get_Contact_PayCurrencyReport_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Contact_PayCurrencyReport> list = new List<Objects.Contact_PayCurrencyReport>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double PaysIN_Sell = Convert.ToDouble(table.Rows[i]["PaysIN_Sell"]);
                        double PaysIN_Maintenance = Convert.ToDouble(table.Rows[i]["PaysIN_Maintenance"]);
                        double PaysOUT_Buy = Convert.ToDouble(table.Rows[i]["PaysOUT_Buy"]);

                        list.Add(new Objects.Contact_PayCurrencyReport(CurrencyID, CurrencyName, CurrencySymbol, PaysIN_Sell
                            , PaysIN_Maintenance, PaysOUT_Buy));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_PayCurrencyReport_List_From_DataTable:" + ee.Message);
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
            internal static List<Contact_Buys_ReportDetail> Get_Contact_Buys_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Contact_Buys_ReportDetail> list = new List<Contact_Buys_ReportDetail>();


                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DateTime Bill_Date = Convert.ToDateTime(table.Rows[i]["Bill_Date"]);
                        uint Bill_ID = Convert.ToUInt32(table.Rows[i]["Bill_ID"]);
                        int ClauseS_Count = Convert.ToInt32(table.Rows[i]["ClauseS_Count"]);
                        double Amount_IN = Convert.ToDouble(table.Rows[i]["Amount_IN"]);
                        double Amount_Remain = Convert.ToDouble(table.Rows[i]["Amount_Remain"]);
                        double BillValue = Convert.ToDouble(table.Rows[i]["BillValue"]);
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        string PaysAmount = table.Rows[i]["PaysAmount"].ToString();
                        double PaysRemain = Convert.ToDouble(table.Rows[i]["PaysRemain"]);
                        double Bill_RealValue = Convert.ToDouble(table.Rows[i]["Bill_RealValue"]);
                        double Bill_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bill_Pays_RealValue"]);
                        string Bill_ItemsOut_Value = table.Rows[i]["Bill_ItemsOut_Value"].ToString();
                        double Bill_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["Bill_ItemsOut_RealValue"]);
                        string Bill_Pays_Return_Value = table.Rows[i]["Bill_Pays_Return_Value"].ToString();
                        double Bill_Pays_Return_RealValue = Convert.ToDouble(table.Rows[i]["Bill_Pays_Return_RealValue"]);

                        list.Add(
                            new Contact_Buys_ReportDetail(Bill_Date, Bill_ID, ClauseS_Count, Amount_IN, Amount_Remain, BillValue, CurrencyID, CurrencyName, CurrencySymbol, ExchangeRate
                            , PaysAmount, PaysRemain, Bill_RealValue, Bill_Pays_RealValue, Bill_ItemsOut_Value, Bill_ItemsOut_RealValue, Bill_Pays_Return_Value
                            , Bill_Pays_Return_RealValue));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_TradeItemStore_Report_List_From_DataTable:" + ee.Message);
                }
            }

            public static void IntiliazeListView(ref ListView listview)
            {
                try
                {
                    listview.Name = "ListViewBuysDay";
                    listview.Columns.Clear();
                    listview.Columns.Add("التاريخ");
                    listview.Columns.Add("الرقم");
                    listview.Columns.Add("البنود");
                    listview.Columns.Add("اجمالي الكميات");
                    listview.Columns.Add("الكمية المتبقية");
                    listview.Columns.Add("قيمة الفاتورة");
                    listview.Columns.Add("سعر الصرف");
                    listview.Columns.Add("المدفوع");
                    listview.Columns.Add("الباقي");
                    listview.Columns.Add("قيمة الفاتور الفعلية");
                    listview.Columns.Add(" المدفوع الفعلي");
                    listview.Columns.Add("قيمة  الخارج");
                    listview.Columns.Add(" عائدات الفاتورة");
                    listview.Columns.Add("العائدات الفعلية");
                    IntializeListViewColumnsWidth(ref listview);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Report_Buy_Day_Detail:IntiliazeListView" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            public static void IntializeListViewColumnsWidth(ref ListView listview)
            {

                try
                {
                    listview.Columns[0].Width = 75;//time
                    listview.Columns[1].Width = 60;//id
                    listview.Columns[2].Width = 60;//clause count
                    listview.Columns[3].Width = 125;//amount in
                    listview.Columns[4].Width = 125;//amount remain
                    listview.Columns[5].Width = 100;//value
                    listview.Columns[6].Width = 100;//exchangerate
                    listview.Columns[7].Width = 100;//paid
                    listview.Columns[8].Width = 100;//remain
                    listview.Columns[9].Width = 140;//قيمة الفاتور الفعلية
                    listview.Columns[10].Width = 150;// المدفوع الفعلي
                    listview.Columns[11].Width = 140;//قيمة  الخارج
                    listview.Columns[12].Width = 140;//عائدات الفاتورة
                    listview.Columns[13].Width = 140;//القيمة العلية للعائدات

                }
                catch (Exception ee)
                {
                    MessageBox.Show("Contact_Buy_Detail:IntializeListViewColumnsWidth" + Environment.NewLine + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            internal static Contact_Buys_Report Get_Contact_Buys_Report_From_DataTable(DataTable table)
            {

                try
                {
                    int Bills_Count = Convert.ToInt32(table.Rows[0]["Bills_Count"]);
                    double Amount_IN = Convert.ToDouble(table.Rows[0]["Amount_IN"]);
                    double Amount_Remain = Convert.ToDouble(table.Rows[0]["Amount_Remain"]);
                    string Bills_Value = table.Rows[0]["Bills_Value"].ToString();

                    string Bills_Pays_Value = table.Rows[0]["Bills_Pays_Value"].ToString();
                    string Bills_Pays_Remain = table.Rows[0]["Bills_Pays_Remain"].ToString();
                    double Bills_Pays_Remain_UPON_Bill_Currency = Convert.ToInt32(table.Rows[0]["Bills_Pays_Remain_UPON_Bill_Currency"]);

                    double Bills_RealValue = Convert.ToDouble(table.Rows[0]["Bills_RealValue"]);
                    double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[0]["Bills_Pays_RealValue"]);
                    string Bills_ItemsOut_Value = table.Rows[0]["Bills_ItemsOut_Value"].ToString();
                    double Bills_ItemsOut_RealValue = Convert.ToDouble(table.Rows[0]["Bills_ItemsOut_RealValue"]);
                    string Bills_Pays_Return_Value = table.Rows[0]["Bills_Pays_Return_Value"].ToString();
                    double Bills_Pays_Return_RealValue = Convert.ToDouble(table.Rows[0]["Bills_Pays_Return_RealValue"]);




                    return new Contact_Buys_Report(Bills_Count,
                     Amount_IN,
                     Amount_Remain,
                     Bills_Value,

                     Bills_Pays_Value,
                     Bills_Pays_Remain,
                     Bills_Pays_Remain_UPON_Bill_Currency,

                     Bills_RealValue,
                     Bills_Pays_RealValue,
                     Bills_ItemsOut_Value,
                     Bills_ItemsOut_RealValue,
                     Bills_Pays_Return_Value,
                     Bills_Pays_Return_RealValue);
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Buys_Report_From_DataTable:" + ee.Message);
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
            internal static List<Contact_Sells_ReportDetail> Get_Contact_Sells_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {

                    List<Contact_Sells_ReportDetail> list = new List<Objects.Contact_Sells_ReportDetail>();

                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        DateTime Bill_Date = Convert.ToDateTime(table.Rows[i]["Bill_Date"]);
                        uint Bill_ID = Convert.ToUInt32(table.Rows[i]["Bill_ID"]);
                        string SellType = table.Rows[i]["SellType"].ToString();
                        int ClauseS_Count = Convert.ToInt32(table.Rows[i]["ClauseS_Count"]);
                        double BillValue = Convert.ToDouble(table.Rows[i]["BillValue"]);
                        uint CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        string CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        string CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        double ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        int PaysCount = Convert.ToInt32(table.Rows[i]["PaysCount"]);
                        string PaysAmount = table.Rows[i]["PaysAmount"].ToString();
                        double PaysRemain = Convert.ToDouble(table.Rows[i]["PaysRemain"]);
                        string Source_ItemsIN_Cost_Details = table.Rows[i]["Source_ItemsIN_Cost_Details"].ToString();
                        double Source_ItemsIN_RealCost = Convert.ToDouble(table.Rows[i]["Source_ItemsIN_RealCost"]);
                        double BillValue_RealValue = Convert.ToDouble(table.Rows[i]["BillValue_RealValue"]);
                        double RealPaysValue = Convert.ToDouble(table.Rows[i]["RealPaysValue"]);

                        list.Add(new Contact_Sells_ReportDetail(Bill_Date, Bill_ID, SellType, ClauseS_Count, BillValue,
                        CurrencyID, CurrencyName, CurrencySymbol, ExchangeRate, PaysCount, PaysAmount, PaysRemain,
                        Source_ItemsIN_Cost_Details, Source_ItemsIN_RealCost, BillValue_RealValue, RealPaysValue));
                    }
                    return list;
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
            internal static Contact_Sells_Report Get_Contact_Sells_Report_From_DataTable(DataTable table)
            {

                try
                {


                    int Bills_Count = Convert.ToInt32(table.Rows[0]["Bills_Count"]);
                    string Bills_Value = table.Rows[0]["Bills_Value"].ToString();
                    string Bills_Pays_Value = table.Rows[0]["Bills_Pays_Value"].ToString();
                    string Bills_Pays_Remain = table.Rows[0]["Bills_Pays_Remain"].ToString();
                    double Bills_Pays_Remain_UPON_BillsCurrency = Convert.ToDouble(table.Rows[0]["Bills_Pays_Remain_UPON_BillsCurrency"]);
                    string Bills_ItemsIN_Value = table.Rows[0]["Bills_ItemsIN_Value"].ToString();
                    double Bills_ItemsIN_RealValue = Convert.ToDouble(table.Rows[0]["Bills_ItemsIN_RealValue"]);
                    double Bills_RealValue = Convert.ToDouble(table.Rows[0]["Bills_RealValue"]);
                    double Bills_Pays_RealValue = Convert.ToDouble(table.Rows[0]["Bills_Pays_RealValue"]);


                    return new Objects.Contact_Sells_Report(Bills_Count, Bills_Value, Bills_Pays_Value, Bills_Pays_Remain,
             Bills_Pays_Remain_UPON_BillsCurrency, Bills_ItemsIN_Value, Bills_ItemsIN_RealValue, Bills_RealValue,
            Bills_Pays_RealValue);
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_Sells_Report_From_DataTable:" + ee.Message);
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
            internal static List<Contact_MaintenanceOPRs_ReportDetail> Get_Contact_MaintenanceOPRs_ReportDetail_List_From_DataTable(DataTable table)
            {

                try
                {
                    List<Contact_MaintenanceOPRs_ReportDetail> list = new List<Objects.Contact_MaintenanceOPRs_ReportDetail>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DateTime MaintenanceOPR_Date = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_Date"]);
                        uint MaintenanceOPR_ID = Convert.ToUInt32(table.Rows[i]["MaintenanceOPR_ID"]);
                        uint ItemID = Convert.ToUInt32(table.Rows[i]["ItemID"]);
                        string ItemName = table.Rows[i]["ItemName"].ToString();
                        string ItemCompany = table.Rows[i]["ItemCompany"].ToString();
                        string FolderName = table.Rows[i]["FolderName"].ToString();
                        string FalutDesc = table.Rows[i]["FalutDesc"].ToString();

                        DateTime? MaintenanceOPR_Endworkdate;
                        if (table.Rows[i]["MaintenanceOPR_Endworkdate"] != DBNull.Value)

                        {
                            MaintenanceOPR_Endworkdate = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_Endworkdate"]);
                        }
                        else 
                        {
                            MaintenanceOPR_Endworkdate = null;
                        }

                        bool? MaintenanceOPR_Rpaired;
                        try
                        {
                            MaintenanceOPR_Rpaired = Convert.ToBoolean(table.Rows[i]["MaintenanceOPR_Rpaired"]);
                        }
                        catch
                        {
                            MaintenanceOPR_Rpaired = null;
                        }


                        DateTime? MaintenanceOPR_DeliverDate;
                        try
                        {
                            MaintenanceOPR_DeliverDate = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_DeliverDate"]);
                        }
                        catch
                        {
                            MaintenanceOPR_DeliverDate = null;
                        }
                     
                        DateTime? MaintenanceOPR_EndWarrantyDate;
                        if (table.Rows[i]["MaintenanceOPR_EndWarrantyDate"] != DBNull.Value)
                        {
                            MaintenanceOPR_EndWarrantyDate = Convert.ToDateTime(table.Rows[i]["MaintenanceOPR_EndWarrantyDate"]);
                        }
                        else 
                        {
                            MaintenanceOPR_EndWarrantyDate = null;
                        }

                        uint? BillMaintenanceID;
                        try
                        {
                            BillMaintenanceID = Convert.ToUInt32(table.Rows[i]["BillMaintenanceID"]);
                        }
                        catch
                        {
                            BillMaintenanceID = null;
                        }

                        double? BillValue;
                        try
                        {
                            BillValue = Convert.ToDouble(table.Rows[i]["BillValue"]);
                        }
                        catch
                        {
                            BillValue = null;
                        }

                        uint? CurrencyID;
                        try
                        {
                            CurrencyID = Convert.ToUInt32(table.Rows[i]["CurrencyID"]);
                        }
                        catch
                        {
                            CurrencyID = null;
                        }

                        string CurrencyName;
                        try
                        {
                            CurrencyName = table.Rows[i]["CurrencyName"].ToString();
                        }
                        catch
                        {
                            CurrencyName = "";
                        }

                        string CurrencySymbol;
                        try
                        {
                            CurrencySymbol = table.Rows[i]["CurrencySymbol"].ToString();
                        }
                        catch
                        {
                            CurrencySymbol = "";
                        }

                        double? ExchangeRate;
                        try
                        {
                            ExchangeRate = Convert.ToDouble(table.Rows[i]["ExchangeRate"]);
                        }
                        catch
                        {
                            ExchangeRate = null;
                        }

                        string PaysAmount;
                        try
                        {
                            PaysAmount = table.Rows[i]["PaysAmount"].ToString();
                        }
                        catch
                        {
                            PaysAmount = "";
                        }

                        double? PaysRemain;
                        try
                        {
                            PaysRemain = Convert.ToDouble(table.Rows[i]["PaysRemain"]);
                        }
                        catch
                        {
                            PaysRemain = null;
                        }

                        string Bill_ItemsOut_Value;
                        try
                        {
                            Bill_ItemsOut_Value = table.Rows[i]["Bill_ItemsOut_Value"].ToString();
                        }
                        catch
                        {
                            Bill_ItemsOut_Value = "";
                        }

                        double? Bill_ItemsOut_RealValue;
                        try
                        {
                            Bill_ItemsOut_RealValue = Convert.ToDouble(table.Rows[i]["Bill_ItemsOut_RealValue"]);
                        }
                        catch
                        {
                            Bill_ItemsOut_RealValue = null;
                        }

                        double? Bill_RealValue;
                        try
                        {
                            Bill_RealValue = Convert.ToDouble(table.Rows[i]["Bill_RealValue"]);
                        }
                        catch
                        {
                            Bill_RealValue = null;
                        }

                        double? Bill_Pays_RealValue;
                        try
                        {
                            Bill_Pays_RealValue = Convert.ToDouble(table.Rows[i]["Bill_Pays_RealValue"]);
                        }
                        catch
                        {
                            Bill_Pays_RealValue = null;
                        }
                        list.Add(new Objects.Contact_MaintenanceOPRs_ReportDetail(MaintenanceOPR_Date,
             MaintenanceOPR_ID,
             ItemID,
            ItemName,
             ItemCompany,
             FolderName,
             FalutDesc,
            MaintenanceOPR_Endworkdate,
             MaintenanceOPR_Rpaired,
             MaintenanceOPR_DeliverDate,
            MaintenanceOPR_EndWarrantyDate,
             BillMaintenanceID,
             BillValue,
             CurrencyID,
            CurrencyName,
            CurrencySymbol,
            ExchangeRate,
             PaysAmount,
             PaysRemain,
             Bill_ItemsOut_Value,
             Bill_ItemsOut_RealValue,
             Bill_RealValue,
            Bill_Pays_RealValue));

                    }

                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_MaintenanceOPRs_ReportDetail_List_From_DataTable:" + ee.Message);
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
            internal static Contact_MaintenanceOPRs_Report Get_Contact_MaintenanceOPRs_Report_From_DataTable(DataTable table)
            {
                try
                {
                    int MaintenanceOPRs_Count = Convert.ToInt32(table.Rows[0]["MaintenanceOPRs_Count"]);
                    int MaintenanceOPRs_EndWork_Count = Convert.ToInt32(table.Rows[0]["MaintenanceOPRs_EndWork_Count"]);
                    int MaintenanceOPRs_Repaired_Count = Convert.ToInt32(table.Rows[0]["MaintenanceOPRs_Repaired_Count"]);
                    int MaintenanceOPRs_Warranty_Count = Convert.ToInt32(table.Rows[0]["MaintenanceOPRs_Warranty_Count"]);
                    int MaintenanceOPRs_EndWarranty_Count = Convert.ToInt32(table.Rows[0]["MaintenanceOPRs_EndWarranty_Count"]);

                    int BillMaintenances_Count = Convert.ToInt32(table.Rows[0]["BillMaintenances_Count"]);
                    string BillMaintenances_Value = table.Rows[0]["BillMaintenances_Value"].ToString();
                    string BillMaintenances_Pays_Value = table.Rows[0]["BillMaintenances_Pays_Value"].ToString();
                    string BillMaintenances_Pays_Remain = table.Rows[0]["BillMaintenances_Pays_Remain"].ToString();
                    double BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency = Convert.ToDouble(table.Rows[0]["MaintenanceOPRs_EndWarranty_Count"]);

                    string BillMaintenances_ItemsOut_Value = table.Rows[0]["BillMaintenances_ItemsOut_Value"].ToString();
                    double BillMaintenances_ItemsOut_RealValue = Convert.ToDouble(table.Rows[0]["BillMaintenances_ItemsOut_RealValue"]);
                    double BillMaintenances_RealValue = Convert.ToDouble(table.Rows[0]["BillMaintenances_RealValue"]);
                    double BillMaintenances_Pays_RealValue = Convert.ToDouble(table.Rows[0]["BillMaintenances_Pays_RealValue"]);


                    return new Contact_MaintenanceOPRs_Report(MaintenanceOPRs_Count,
                                 MaintenanceOPRs_EndWork_Count,
                                MaintenanceOPRs_Repaired_Count,
                                MaintenanceOPRs_Warranty_Count,
                                 MaintenanceOPRs_EndWarranty_Count,

                                BillMaintenances_Count,
                                BillMaintenances_Value,
                                BillMaintenances_Pays_Value,
                                 BillMaintenances_Pays_Remain,
                                BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency,

                                 BillMaintenances_ItemsOut_Value,
                               BillMaintenances_ItemsOut_RealValue,
                                BillMaintenances_RealValue,
                                 BillMaintenances_Pays_RealValue);
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Contact_MaintenanceOPRs_Report_From_DataTable:" + ee.Message);
                }

            }
            #endregion
        }
    }
}
