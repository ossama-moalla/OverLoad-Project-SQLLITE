using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using OverLoad_Client.Trade.Forms.TradeForms;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Company.CompanySQL;
using OverLoad_Client.Company.Objects;
using OverLoad_Client.Company.Forms;

namespace OverLoad_Client.AccountingObj.Forms
{
    public partial class MainWindow_Buy_Form : Form
    {
        DatabaseInterface DB;

        DateAccount BuysAccount_;
        System.Windows.Forms.MenuItem Refresh_MenuItem;
        System.Windows.Forms.MenuItem CreateBillBuy_MenuItem;
        System.Windows.Forms.MenuItem OpenBillBuy_MenuItem;
        System.Windows.Forms.MenuItem EditBillBuy_MenuItem;
        System.Windows.Forms.MenuItem DeleteBillBuy_MenuItem;


        MenuItem AddPayOUT_BillBuy_MenuItem;
        Currency ReferenceCurrency;
        public MainWindow_Buy_Form(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة المشتريات, لا يمكنك فتح هذه النافذة");

            labelUser.Text = DB.GetUser_EmployeeName(); 
            ReferenceCurrency = new CurrencySQL(DB).GetReferenceCurrency();
            DateAccount  .YearRange yearrange = new DateAccount  .YearRange(DateTime.Today.Year-5, DateTime.Today.Year+5);
              BuysAccount_ = new DateAccount(DB, yearrange, DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
             Initialize_MenuItems();
           
        }
        public async void Initialize_MenuItems()
        {
            Refresh_MenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);

            CreateBillBuy_MenuItem = new System.Windows.Forms.MenuItem("انشاء فاتورة شراء", CreateBillBuy_MenuItem_Click);
            OpenBillBuy_MenuItem = new System.Windows.Forms.MenuItem("فتح", OpenBillBuy_MenuItem_Click);
            EditBillBuy_MenuItem = new System.Windows.Forms.MenuItem("تعديل", EditBillBuy_MenuItem_Click);
            DeleteBillBuy_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteBillBuy_MenuItem_Click);


            AddPayOUT_BillBuy_MenuItem = new System.Windows.Forms.MenuItem("اضافة دفعة تابعة للفاتورة", AddPayOUT_BillBuy_MenuItem_Click);

        }
   

        #region ReportBuys
        public async void Buys_FillReport()
        {

            BuysLabelAccountDate.Text = BuysAccount_.GetAccountDateString();



            if (BuysAccount_.Day != -1)
            {

                BuysLabelAccountType.Text = "حساب اليوم";
                BuysLabelReport.Text = "تقرير حساب اليوم : " + BuysAccount_.GetAccountDateString();
                #region DaySection

                Report_Buys_Month_ReportDetail Report_Buys_DayReport
                    = new ReportBuysSQL(DB).Get_Report_Buys_Day_Report(BuysAccount_.Year, BuysAccount_.Month, BuysAccount_.Day);
                textBoxBuys_AmountIN.Text = Report_Buys_DayReport.Amount_IN.ToString ();
                textBoxBuys_AmountRemain.Text = Report_Buys_DayReport.Amount_Remain.ToString ();
                textBoxBuys_Value.Text = Report_Buys_DayReport.Bills_Value;
                textBoxBuysPaysValue.Text = Report_Buys_DayReport.Bills_Pays_Value;
                textBoxBuysPaysRmain.Text = Report_Buys_DayReport.Bills_Pays_Remain;
                if (Report_Buys_DayReport.Bills_Pays_Remain_UPON_Bill_Currency > 0)
                    textBoxBuysPaysRmain.BackColor = Color.Orange;
                else
                    textBoxBuysPaysRmain.BackColor = Color.LimeGreen;
                textBoxBuyRealValue.Text = Report_Buys_DayReport.Bills_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_RealPays.Text = Report_Buys_DayReport.Bills_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;

                textBoxBuys_OutValue.Text = Report_Buys_DayReport.Bills_ItemsOut_Value;
                textBoxBuys_OutRealValue.Text = Report_Buys_DayReport.Bills_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_Out_Pays.Text = Report_Buys_DayReport.Bills_Pays_Return_Value;
                textBoxBuys_Out_Pays_RealValue.Text = Report_Buys_DayReport.Bills_Pays_Return_RealValue + " " + ReferenceCurrency.CurrencySymbol;
                #endregion
            }
            else if (BuysAccount_.Month != -1)
            {
                BuysLabelAccountType.Text = "حساب الشهر";
                BuysLabelReport.Text = "تقرير حساب الشهر : " + BuysAccount_.GetAccountDateString();

                #region MonthSection

                Report_Buys_Year_ReportDetail Report_Buys_MonthReport
                     = new ReportBuysSQL(DB).Get_Report_Buys_Month_Report(BuysAccount_.Year, BuysAccount_.Month);
                textBoxBuys_AmountIN.Text = Report_Buys_MonthReport.Amount_IN.ToString();
                textBoxBuys_AmountRemain.Text = Report_Buys_MonthReport.Amount_Remain.ToString();
                textBoxBuys_Value.Text = Report_Buys_MonthReport.Bills_Value;
                textBoxBuysPaysValue.Text = Report_Buys_MonthReport.Bills_Pays_Value;
                textBoxBuysPaysRmain.Text = Report_Buys_MonthReport.Bills_Pays_Remain;
                if (Report_Buys_MonthReport.Bills_Pays_Remain_UPON_Bill_Currency > 0)
                    textBoxBuysPaysRmain.BackColor = Color.Orange;
                else
                    textBoxBuysPaysRmain.BackColor = Color.LimeGreen;
                textBoxBuyRealValue.Text = Report_Buys_MonthReport.Bills_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_RealPays.Text = Report_Buys_MonthReport.Bills_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;

                textBoxBuys_OutValue.Text = Report_Buys_MonthReport.Bills_ItemsOut_Value;
                textBoxBuys_OutRealValue.Text = Report_Buys_MonthReport.Bills_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_Out_Pays.Text = Report_Buys_MonthReport.Bills_Pays_Return_Value;
                textBoxBuys_Out_Pays_RealValue.Text = Report_Buys_MonthReport.Bills_Pays_Return_RealValue + " " + ReferenceCurrency.CurrencySymbol;
                #endregion
            }
            else if (BuysAccount_.Year != -1)
            {
                BuysLabelAccountType.Text = "حساب السنة";
                BuysLabelReport.Text = "تقرير حساب السنة : " + BuysAccount_.GetAccountDateString();

                #region YearSection

                Report_Buys_YearRange_ReportDetail Report_Buys_YearReport
                   = new ReportBuysSQL(DB).Get_Report_Buys_Year_Report(BuysAccount_.Year);
                textBoxBuys_AmountIN.Text = Report_Buys_YearReport.Amount_IN.ToString();
                textBoxBuys_AmountRemain.Text = Report_Buys_YearReport.Amount_Remain.ToString();
                textBoxBuys_Value.Text = Report_Buys_YearReport.Bills_Value;
                textBoxBuysPaysValue.Text = Report_Buys_YearReport.Bills_Pays_Value;
                textBoxBuysPaysRmain.Text = Report_Buys_YearReport.Bills_Pays_Remain;
                if (Report_Buys_YearReport.Bills_Pays_Remain_UPON_Bill_Currency > 0)
                    textBoxBuysPaysRmain.BackColor = Color.Orange;
                else
                    textBoxBuysPaysRmain.BackColor = Color.LimeGreen;
                textBoxBuyRealValue.Text = Report_Buys_YearReport.Bills_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_RealPays.Text = Report_Buys_YearReport.Bills_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;

                textBoxBuys_OutValue.Text = Report_Buys_YearReport.Bills_ItemsOut_Value;
                textBoxBuys_OutRealValue.Text = Report_Buys_YearReport.Bills_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_Out_Pays.Text = Report_Buys_YearReport.Bills_Pays_Return_Value;
                textBoxBuys_Out_Pays_RealValue.Text = Report_Buys_YearReport.Bills_Pays_Return_RealValue + " " + ReferenceCurrency.CurrencySymbol;
                #endregion
            }
            else
            {
                BuysLabelAccountType.Text = "حساب السنوات";
                BuysLabelReport.Text = "تقرير حساب السنوات : " + BuysAccount_.GetAccountDateString();

                #region YearRangeSection

                textBoxBuys_AmountIN.Text = "-";
                textBoxBuys_Value.Text = "-";
                textBoxBuysPaysValue.Text = "-";
                textBoxBuysPaysRmain.Text = "-";
                textBoxBuys_Out_Pays.Text = "-";
                textBoxBuyRealValue.Text = "-";
                textBoxBuys_OutValue.Text = "-";
                textBoxBuys_RealPays.Text = "-";
                #endregion
            }


        }
        public async void Refresh_ListViewBuys()
        {

            listViewBuys.Items.Clear();
            if (BuysAccount_.Day != -1)
            {

                #region DaySection


                if (listViewBuys.Name != "ListViewBuys_Day")
                {
                    Report_Buys_Day_ReportDetail.IntiliazeListView(ref listViewBuys);

                }
                List<Report_Buys_Day_ReportDetail> Report_Buys_Day_ReportDetail_List
                          = new ReportBuysSQL(DB).Get_Report_Buys_Day_ReportDetail(BuysAccount_.Year, BuysAccount_.Month, BuysAccount_.Day);
                for (int i = 0; i < Report_Buys_Day_ReportDetail_List.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Report_Buys_Day_ReportDetail_List[i].Bill_Time.ToShortTimeString());
                    item.Name = Report_Buys_Day_ReportDetail_List[i].Bill_ID.ToString();
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Bill_ID.ToString());
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Bill_Owner);
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].ClauseS_Count.ToString());
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Amount_IN .ToString());
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Amount_Remain.ToString());
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].BillValue .ToString() + " " + Report_Buys_Day_ReportDetail_List[i].CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].ExchangeRate.ToString());
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].PaysAmount);
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].PaysRemain.ToString() + " " + Report_Buys_Day_ReportDetail_List[i].CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Bill_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Bill_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Bill_ItemsOut_Value.ToString() );
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Bill_Pays_Return_Value);
                    item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].Bill_Pays_Return_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);

                    //-Report_Buys_Day_ReportDetail_List[i].Source_ItemsIN_RealCost) + " " + ReferenceCurrency.CurrencySymbol);
                    //item.SubItems.Add(Report_Buys_Day_ReportDetail_List[i].RealPaysValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);

                     item.UseItemStyleForSubItems = false;
                    if(Report_Buys_Day_ReportDetail_List[i].Amount_Remain ==0)
                        for (int j =4; j <= 5; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else
                        for (int j = 4; j <= 5; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    if (Report_Buys_Day_ReportDetail_List[i].PaysRemain != 0)
                        for (int j = 6; j <= 9; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else
                        for (int j = 6; j <= 9; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;

                    if (Report_Buys_Day_ReportDetail_List[i].Bill_Pays_RealValue
                        > Report_Buys_Day_ReportDetail_List[i].Bill_Pays_Return_RealValue)
                        for (int j = 10; j <= 13; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else if (Report_Buys_Day_ReportDetail_List[i].Bill_Pays_RealValue
                        < Report_Buys_Day_ReportDetail_List[i].Bill_Pays_Return_RealValue)
                        for (int j = 10; j <= 13; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    else
                        for (int j = 10; j <= 13; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    listViewBuys.Items.Add(item);


                }
                #endregion
            }
            else if (BuysAccount_.Month != -1)
            {

                #region MonthSection
                if (listViewBuys.Name != "ListViewBuys_Month")
                {
                    Report_Buys_Month_ReportDetail.IntiliazeListView(ref listViewBuys);
                }
                List<Report_Buys_Month_ReportDetail> Report_Buys_Month_ReportDetailList
                                    = new ReportBuysSQL(DB).Get_Report_Buys_Month_ReportDetail(BuysAccount_.Year, BuysAccount_.Month);
                for (int i = 0; i < Report_Buys_Month_ReportDetailList.Count; i++)
                {
                    ListViewItem item = new ListViewItem(Report_Buys_Month_ReportDetailList[i].DayDate.ToShortDateString());
                    item.Name = Report_Buys_Month_ReportDetailList[i].DayID.ToString();
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_Count.ToString());
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Amount_IN .ToString());
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Amount_Remain.ToString());
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_Value.ToString());
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_Pays_Value);
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_Pays_Remain);

                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);

                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_ItemsOut_Value);
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_Pays_Return_Value);
                    item.SubItems.Add(Report_Buys_Month_ReportDetailList[i].Bills_Pays_Return_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.UseItemStyleForSubItems = false;
                    if(Report_Buys_Month_ReportDetailList[i].Amount_IN > 0)
                    {
                        if (Report_Buys_Month_ReportDetailList[i].Amount_Remain == 0)
                            for (int j = 3; j <= 4; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 3; j <= 4; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                    }
                   else
                        for (int j = 3; j <= 4; j++)
                            item.SubItems[j].BackColor = Color.LightYellow ;
                    if(Report_Buys_Month_ReportDetailList[i].Bills_RealValue > 0)
                    {
                        if (Report_Buys_Month_ReportDetailList[i].Bills_Pays_Remain_UPON_Bill_Currency != 0)
                            for (int j = 5; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 5; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                    }
                    else
                        for (int j = 5; j <= 7; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;

                    if (Report_Buys_Month_ReportDetailList[i].Bills_Pays_RealValue
                        > Report_Buys_Month_ReportDetailList[i].Bills_Pays_Return_RealValue)
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else if (Report_Buys_Month_ReportDetailList[i].Bills_Pays_RealValue
                        < Report_Buys_Month_ReportDetailList[i].Bills_Pays_Return_RealValue)
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    else
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;

                    listViewBuys.Items.Add(item);

                }
                #endregion
            }
            else if (BuysAccount_.Year != -1)
            {

                #region YearSection
                if (listViewBuys.Name != "ListViewBuys_Year")
                {

                    Report_Buys_Year_ReportDetail.IntiliazeListView(ref listViewBuys);

                }

                List<Report_Buys_Year_ReportDetail> Report_Buys_Year_ReportDetailList
                           = new ReportBuysSQL(DB).Get_Report_Buys_Year_ReportDetail(BuysAccount_.Year);
                for (int i = 0; i < Report_Buys_Year_ReportDetailList.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Report_Buys_Year_ReportDetailList[i].MonthNO.ToString ());
                    item.Name = Report_Buys_Year_ReportDetailList[i].MonthNO.ToString();
                    item .SubItems .Add (Report_Buys_Year_ReportDetailList[i].MonthName);
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_Count.ToString());
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Amount_IN.ToString());
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Amount_Remain.ToString());
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_Value.ToString());
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_Pays_Value);
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_Pays_Remain);

                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);

                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_ItemsOut_Value);
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_Pays_Return_Value);
                    item.SubItems.Add(Report_Buys_Year_ReportDetailList[i].Bills_Pays_Return_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.UseItemStyleForSubItems = false;

                    if (Report_Buys_Year_ReportDetailList[i].Amount_IN > 0)
                    {
                        if (Report_Buys_Year_ReportDetailList[i].Amount_Remain == 0)
                            for (int j = 3; j <= 4; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 3; j <= 4; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                    }
                    else
                        for (int j = 3; j <= 4; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    if (Report_Buys_Year_ReportDetailList[i].Bills_RealValue > 0)
                    {
                        if (Report_Buys_Year_ReportDetailList[i].Bills_Pays_Remain_UPON_Bill_Currency != 0)
                            for (int j = 5; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 5; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                    }
                    else
                        for (int j = 5; j <= 7; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;

                    if (Report_Buys_Year_ReportDetailList[i].Bills_Pays_RealValue
                        > Report_Buys_Year_ReportDetailList[i].Bills_Pays_Return_RealValue)
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else if (Report_Buys_Year_ReportDetailList[i].Bills_Pays_RealValue
                        < Report_Buys_Year_ReportDetailList[i].Bills_Pays_Return_RealValue)
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    else
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;

                    listViewBuys.Items.Add(item);

                }
                #endregion
            }
            else
            {
                //BuysLabelAccountType.Text = "حساب السنوات";
                //BuysLabelReport.Text = "تقرير حساب السنوات : " + BuysAccount_.GetAccountDateString();

                #region YearRangeSection
                if (listViewBuys.Name != "ListViewBuys_YearRange")
                {
                    Report_Buys_YearRange_ReportDetail.IntiliazeListView(ref listViewBuys);
                }
                List<Report_Buys_YearRange_ReportDetail> Report_Buys_YearRange_ReportDetailList
                           = new ReportBuysSQL(DB).Get_Report_Buys_YearRange_ReportDetail(BuysAccount_.YearRange_.min_year, BuysAccount_.YearRange_.max_year);

                for (int i = 0; i < Report_Buys_YearRange_ReportDetailList.Count; i++)
                {
                    ListViewItem item = new ListViewItem(Report_Buys_YearRange_ReportDetailList[i].YearNO.ToString());
                    item.Name = Report_Buys_YearRange_ReportDetailList[i].YearNO.ToString();
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_Count.ToString());
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Amount_IN.ToString());
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Amount_Remain.ToString());
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_Value.ToString());
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_Value);
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_Remain);

                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);

                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_ItemsOut_Value);
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_Return_Value);
                    item.SubItems.Add(Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_Return_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.UseItemStyleForSubItems = false;

                    if (Report_Buys_YearRange_ReportDetailList[i].Amount_IN > 0)
                    {
                        if (Report_Buys_YearRange_ReportDetailList[i].Amount_Remain == 0)
                            for (int j = 3; j <= 4; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 3; j <= 4; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                    }
                    else
                        for (int j = 3; j <= 4; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    if (Report_Buys_YearRange_ReportDetailList[i].Bills_RealValue > 0)
                    {
                        if (Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_Remain_UPON_Bill_Currency != 0)
                            for (int j = 5; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 5; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                    }
                    else
                        for (int j = 5; j <= 7; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;

                    if (Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_RealValue
                        > Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_Return_RealValue)
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else if (Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_RealValue
                        < Report_Buys_YearRange_ReportDetailList[i].Bills_Pays_Return_RealValue)
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    else
                        for (int j = 8; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;

                    listViewBuys.Items.Add(item);

                }

                #endregion
            }

            Buys_FillReport();


        }
        private void BuysBack_Click(object sender, EventArgs e)
        {
            if (BuysAccount_.Year == -1) return;
            BuysAccount_.Account_Date_UP();
            Refresh_ListViewBuys();
        }

        private void BuysButtonLeftRight_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            bool left;
            if (b.Name == "BuysButtonLeft") left = true;
            else left = false;

            if (BuysAccount_.Day != -1)
            {
                if (left)
                {
                    if (BuysAccount_.Day == DateTime.DaysInMonth(BuysAccount_.Year, BuysAccount_.Month))
                    {
                        if (BuysAccount_.Month == 12)
                        { BuysAccount_.Year++; BuysAccount_.Month = 1; BuysAccount_.Day = 1; }
                        else
                        { BuysAccount_.Month++; BuysAccount_.Day = 1; }

                    }
                    else BuysAccount_.Day++;
                }
                else
                {
                    if (BuysAccount_.Day == 1)
                    {

                        if (BuysAccount_.Month == 1)
                        { BuysAccount_.Year--; BuysAccount_.Month = 12; }
                        else
                        { BuysAccount_.Month--; }
                        BuysAccount_.Day = DateTime.DaysInMonth(BuysAccount_.Year, BuysAccount_.Month);
                    }
                    else BuysAccount_.Day--;
                }

            }
            else if (BuysAccount_.Month != -1)
            {
                if (left)
                {
                    if (BuysAccount_.Month == 12)
                    {
                        BuysAccount_.Year++; BuysAccount_.Month = 1;
                    }
                    else BuysAccount_.Month++;
                }
                else
                {
                    if (BuysAccount_.Month == 1)
                    {
                        BuysAccount_.Year--; BuysAccount_.Month = 12;
                    }
                    else BuysAccount_.Month--;
                }
            }
            else if (BuysAccount_.Year != -1)
            {
                if (left)
                {
                    BuysAccount_.Year++;
                    BuysAccount_.YearRange_.min_year++;
                    BuysAccount_.YearRange_.max_year++;
                }
                else
                {
                    BuysAccount_.Year--;
                    BuysAccount_.YearRange_.min_year--;
                    BuysAccount_.YearRange_.max_year--;
                }
            }
            else
            {
                if (left)
                {

                    BuysAccount_.YearRange_.min_year += 10;
                    BuysAccount_.YearRange_.max_year += 10;
                }
                else
                {
                    BuysAccount_.YearRange_.min_year -= 10;
                    BuysAccount_.YearRange_.max_year -= 10;
                }
            }
            Refresh_ListViewBuys();
        }

        public void ListViewBuysAccountDown()
        {
            try
            {
                if (BuysAccount_.Year == -1 || BuysAccount_.Month == -1 || BuysAccount_.Day == -1)
                {
                    BuysAccount_.Account_Date_Down(Convert.ToInt32(listViewBuys.SelectedItems[0].Name));
                    Refresh_ListViewBuys();
                }
                else
                {

                    OpenBillBuy_MenuItem.PerformClick();

                }


            }
            catch (Exception ee)
            {

            }


        }
        private void ListViewBuys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewBuys.SelectedItems.Count > 0)
                ListViewBuysAccountDown();
        }
        private void ListViewBuys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                ListViewBuysAccountDown();
        }
        private void ListViewBuys_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewBuys.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewBuys.Items)
                    {
                        if (item1.Bounds.Contains(new Point(e.X, e.Y)))
                        {
                            match = true;
                            listitem = item1;
                            break;
                        }
                    }

                    if (match)
                    {
                        if (BuysAccount_.Day != -1)
                        {

                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] {OpenBillBuy_MenuItem , EditBillBuy_MenuItem , DeleteBillBuy_MenuItem
                            , new MenuItem("-"),CreateBillBuy_MenuItem });
                            MenuItemList.AddRange(new MenuItem[] { new MenuItem("-"),AddPayOUT_BillBuy_MenuItem });
                            listViewBuys.ContextMenu = new ContextMenu(MenuItemList.ToArray());


                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            listViewBuys.ContextMenu = new ContextMenu(mi1);


                        }


                    }
                    else
                    {
                        if (BuysAccount_.Day != -1)
                        {
                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"), CreateBillBuy_MenuItem};
                            listViewBuys.ContextMenu = new ContextMenu(mi1);
                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            listViewBuys.ContextMenu = new ContextMenu(mi1);


                        }

                    }

                }
            }
        }

        public async void IntializeListViewBuysColumnsWidth()
        {

            if (BuysAccount_.Day != -1)
            {


                listViewBuys .Columns[0].Width = 75;//time
                listViewBuys.Columns[1].Width = 60;//id
                listViewBuys.Columns[2].Width = 100;//owner
                listViewBuys.Columns[3].Width = 60;//clause count
                listViewBuys.Columns[4].Width = 125;//amount in
                listViewBuys.Columns[5].Width = 125;//amount remain
                listViewBuys.Columns[6].Width = 100;//value
                listViewBuys.Columns[7].Width = 100;//exchangerate
                listViewBuys.Columns[8].Width = 100;//paid
                listViewBuys.Columns[9].Width = 100;//remain
                listViewBuys.Columns[10].Width = 140;//قيمة الفاتور الفعلية
                listViewBuys.Columns[11].Width = 150;// المدفوع الفعلي
                listViewBuys.Columns[12].Width = 140;//قيمة  الخارج
                listViewBuys.Columns[13].Width = 140;//عائدات الفاتورة
                listViewBuys.Columns[14].Width = 140;//القيمة العلية للعائدات

            }
            else
            {
                listViewBuys.Columns[0].Width = 100;//--
                listViewBuys.Columns[1].Width = 120;//bills count
                listViewBuys.Columns[2].Width = 115;//clause count
                listViewBuys.Columns[3].Width = 125;//bill value
                listViewBuys.Columns[4].Width = 125;//bills pays value
                listViewBuys.Columns[5].Width = 120;//remain
                listViewBuys.Columns[6].Width = 140;//item in value
                listViewBuys.Columns[7].Width = 115;//item in real value
                listViewBuys.Columns[8].Width = 145;//real value
                listViewBuys.Columns[9].Width = 115;//profit
                listViewBuys.Columns[10].Width = 125;//real p
            }
        }
        private void CreateBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            BillBuyForm BillOUTForm_ = new BillBuyForm(DB, GetSelectedDate(), null);
            BillOUTForm_.FormClosed += Form_Closed;
            BillOUTForm_.Show();

        }
        private void OpenBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            uint billbuyid = Convert.ToUInt32(listViewBuys.SelectedItems [0].Name );
            BillBuy BillBuy_ = new BillBuySQL(DB).GetBillBuy_INFO_BYID(billbuyid);
            BillBuyForm BillOUTForm_ = new BillBuyForm(DB, BillBuy_, false );
            BillOUTForm_.FormClosed += Form_Closed;
            BillOUTForm_.Show ();
           }
        private void EditBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            uint billbuyid = Convert.ToUInt32(listViewBuys.SelectedItems[0].Name);
            BillBuy BillBuy_ = new BillBuySQL(DB).GetBillBuy_INFO_BYID(billbuyid);
            BillBuyForm BillOUTForm_ = new BillBuyForm(DB, BillBuy_, true );
            BillOUTForm_.FormClosed += Form_Closed;
            BillOUTForm_.Show ();
         }
        private void DeleteBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف؟","",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning );
            if (dd != DialogResult.OK) return;
            uint billbuyid = Convert.ToUInt32(listViewBuys.SelectedItems[0].Name);
            bool success = new BillBuySQL(DB).DeleteBillBuy(billbuyid);
            if (success)
            {
                Refresh_ListViewBuys();
            }

        }
        private void AddPayOUT_BillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewBuys .SelectedItems.Count == 1)
            {
                //ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                //MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                uint sid = Convert.ToUInt32(listViewBuys.SelectedItems[0].Name);
                BillBuy BillBuy_ = new BillBuySQL(DB).GetBillBuy_INFO_BYID(sid);
                PayOUTForm PayOUTForm_ = new PayOUTForm(DB,DateTime.Now , BillBuy_);
                PayOUTForm_.FormClosed += Form_Closed;
                PayOUTForm_.Show ();
                
            }

        }
        #endregion
    
        #region General
        private void tabPage1_Resize(object sender, EventArgs e)
        {

        }
        private void Refresh_MenuItem_Click(object sender, EventArgs e)
        {

            Refresh_ListViewBuys();
  
        }
    
        #endregion




     

       

      
        private DateTime GetSelectedDate()
        {
            //try
            //{
            //    if (Account_.Day != -1)
            //        return (new DateTime(Account_.Year), Account_.Month), Account_.Day), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            //    else if (Account_.Month != -1)
            //    {
            //        int day = ListViewAccountDataDetails.SelectedItems[0].Name);
            //        return (new DateTime(Account_.Year), Account_.Month), day));
            //    }
            //    else if (Account_.Year != -1)
            //    {
            //        int month = ListViewAccountDataDetails.SelectedItems[0].Name);
            //        return (new DateTime(Account_.Year), month, 1));
            //    }
            //    else
            //    {
            //        int year = ListViewAccountDataDetails.SelectedItems[0].Name);
            //        return (new DateTime(year, 1, 1));
            //    }
            //}
            //catch
            //{
            //    return DateTime.Now;
            //}
            return DateTime.Now;
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {

                Refresh_ListViewBuys();
               
            }
            catch (Exception ee)
            {
                MessageBox.Show("MainWindow_Load"+ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
           
        }

        #region ToolStripMenuItem

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                OverLoad_Form form = (OverLoad_Form)sender;
                if (form.Refresh_ListViewBuys_Flag|| form.Refresh_ListViewMoneyDataDetails_Flag) Refresh_ListViewBuys();



            }
            catch (Exception ee)
            {
                MessageBox.Show("Form_Closed:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void فاتورةشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BillBuyForm BillOUTForm_ = new BillBuyForm(DB, GetSelectedDate(), null);
            BillOUTForm_.FormClosed += Form_Closed;
            BillOUTForm_.Show ();
            
        }

        private void تيجبلخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DB.LogOut();
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void تغييركلمةالمرورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Change_MY_Password_From Change_MY_Password_From_ = new Change_MY_Password_From(DB);
                Change_MY_Password_From_.Show();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion




    }


}
