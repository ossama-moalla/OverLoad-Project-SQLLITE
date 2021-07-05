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
    public partial class MainWindow_PayOrder_Form : Form
    {
        DatabaseInterface DB;
   
        DateAccount PayOrdersAccount_;


        System.Windows.Forms.MenuItem Refresh_MenuItem;

        System.Windows.Forms.MenuItem CreatePayOrder_MenuItem;
        System.Windows.Forms.MenuItem OpenPayOrder_MenuItem;
        System.Windows.Forms.MenuItem EditPayOrder_MenuItem;
        System.Windows.Forms.MenuItem DeletePayOrder_MenuItem; 
        MenuItem AddPayOUT_PayOrder_MenuItem;
        Currency ReferenceCurrency;
        public MainWindow_PayOrder_Form(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            labelUser.Text = DB.GetUser_EmployeeName(); 
            ReferenceCurrency = new CurrencySQL(DB).GetReferenceCurrency();
            DateAccount  .YearRange yearrange = new DateAccount  .YearRange(DateTime.Today.Year-5, DateTime.Today.Year+5);
            PayOrdersAccount_ = new DateAccount(DB, yearrange, DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
           Initialize_MenuItems();
           
        }
        public async void Initialize_MenuItems()
        {
            Refresh_MenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);

           
            CreatePayOrder_MenuItem = new System.Windows.Forms.MenuItem("انشاء أمر صرف", CreatePayOrder_MenuItem_Click);
            OpenPayOrder_MenuItem = new System.Windows.Forms.MenuItem("فتح", OpenPayOrder_MenuItem_Click);
            EditPayOrder_MenuItem = new System.Windows.Forms.MenuItem("تعديل", EditPayOrder_MenuItem_Click);
            DeletePayOrder_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeletePayOrder_MenuItem_Click);
            AddPayOUT_PayOrder_MenuItem = new System.Windows.Forms.MenuItem("اضافة دفعة تابعة لأامر الصرف", AddPayOUT_PayOrder_MenuItem_Click);

           
        }
     
        #region ReportPayOrders
        public async void PayOrders_FillReport()
        {

            PayOrdersLabelAccountDate.Text = PayOrdersAccount_.GetAccountDateString();



            if (PayOrdersAccount_.Day != -1)
            {

                PayOrdersLabelAccountType.Text = "حساب اليوم";
                PayOrdersLabelReport.Text = "تقرير حساب اليوم : " + PayOrdersAccount_.GetAccountDateString();
                #region DaySection

                Report_PayOrders_Month_ReportDetail Report_PayOrders_DayReport
                    = new ReportPayOrdersSQL(DB).Get_Report_PayOrders_Day_Report(PayOrdersAccount_.Year, PayOrdersAccount_.Month, PayOrdersAccount_.Day);
                textBoxPayOrders_SalaryCount.Text = Report_PayOrders_DayReport.Salary_PayOrders_Count .ToString();
                textBoxPayOrders_OthersCount.Text = Report_PayOrders_DayReport.Other_PayOrders_Count .ToString();
                textBoxPayOrders_Value.Text = Report_PayOrders_DayReport.PayOrders_Value;
                textBoxPayOrdersPaysValue.Text = Report_PayOrders_DayReport.PayOrders_Pays_Value;
                textBoxPayOrdersPaysRmain.Text = Report_PayOrders_DayReport.PayOrders_Pays_Remain;

                textBoxPayOrderRealValue.Text = Report_PayOrders_DayReport.PayOrders_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxPayOrders_RealPays.Text = Report_PayOrders_DayReport.PayOrders_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;

               
                #endregion
            }
            else if (PayOrdersAccount_.Month != -1)
            {
                PayOrdersLabelAccountType.Text = "حساب الشهر";
                PayOrdersLabelReport.Text = "تقرير حساب الشهر : " + PayOrdersAccount_.GetAccountDateString();

                #region MonthSection

                Report_PayOrders_Year_ReportDetail Report_PayOrders_MonthReport
                     = new ReportPayOrdersSQL(DB).Get_Report_PayOrders_Month_Report(PayOrdersAccount_.Year, PayOrdersAccount_.Month);
                textBoxPayOrders_SalaryCount.Text = Report_PayOrders_MonthReport.Salary_PayOrders_Count.ToString();
                textBoxPayOrders_OthersCount.Text = Report_PayOrders_MonthReport.Other_PayOrders_Count.ToString();
                textBoxPayOrders_Value.Text = Report_PayOrders_MonthReport.PayOrders_Value;
                textBoxPayOrdersPaysValue.Text = Report_PayOrders_MonthReport.PayOrders_Pays_Value;
                textBoxPayOrdersPaysRmain.Text = Report_PayOrders_MonthReport.PayOrders_Pays_Remain;

                textBoxPayOrderRealValue.Text = Report_PayOrders_MonthReport.PayOrders_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxPayOrders_RealPays.Text = Report_PayOrders_MonthReport.PayOrders_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                #endregion
            }
            else if (PayOrdersAccount_.Year != -1)
            {
                PayOrdersLabelAccountType.Text = "حساب السنة";
                PayOrdersLabelReport.Text = "تقرير حساب السنة : " + PayOrdersAccount_.GetAccountDateString();

                #region YearSection

                Report_PayOrders_YearRange_ReportDetail Report_PayOrders_YearReport
                   = new ReportPayOrdersSQL(DB).Get_Report_PayOrders_Year_Report(PayOrdersAccount_.Year);
                textBoxPayOrders_SalaryCount.Text = Report_PayOrders_YearReport.Salary_PayOrders_Count.ToString();
                textBoxPayOrders_OthersCount.Text = Report_PayOrders_YearReport.Other_PayOrders_Count.ToString();
                textBoxPayOrders_Value.Text = Report_PayOrders_YearReport.PayOrders_Value;
                textBoxPayOrdersPaysValue.Text = Report_PayOrders_YearReport.PayOrders_Pays_Value;
                textBoxPayOrdersPaysRmain.Text = Report_PayOrders_YearReport.PayOrders_Pays_Remain;

                textBoxPayOrderRealValue.Text = Report_PayOrders_YearReport.PayOrders_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxPayOrders_RealPays.Text = Report_PayOrders_YearReport.PayOrders_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                #endregion
            }
            else
            {
                PayOrdersLabelAccountType.Text = "حساب السنوات";
                PayOrdersLabelReport.Text = "تقرير حساب السنوات : " + PayOrdersAccount_.GetAccountDateString();

                #region YearRangeSection

                textBoxPayOrders_SalaryCount.Text = "#####";
                textBoxPayOrders_OthersCount.Text = "#####";
                textBoxPayOrders_Value.Text = "#####";
                textBoxPayOrdersPaysValue.Text = "#####";
                textBoxPayOrdersPaysRmain.Text = "#####";
                textBoxPayOrderRealValue.Text = "#####";
                textBoxPayOrders_RealPays.Text = "#####";
                #endregion
            }


        }
        public async void Refresh_ListViewPayOrders()
        {

            listViewPayOrders.Items.Clear();
            if (PayOrdersAccount_.Day != -1)
            {

                #region DaySection


                if (listViewPayOrders.Name != "ListViewPayOrders_Day")
                {
                    Report_PayOrders_Day_ReportDetail.IntiliazeListView(ref listViewPayOrders);

                }
                List<Report_PayOrders_Day_ReportDetail> Report_PayOrders_Day_ReportDetail_List
                          = new ReportPayOrdersSQL(DB).Get_Report_PayOrders_Day_ReportDetail(PayOrdersAccount_.Year, PayOrdersAccount_.Month, PayOrdersAccount_.Day);
                for (int i = 0; i < Report_PayOrders_Day_ReportDetail_List.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Report_PayOrders_Day_ReportDetail_List[i].PayOrder_Time.ToShortTimeString());
                    item.Name = Report_PayOrders_Day_ReportDetail_List[i].PayOrderID.ToString();
                    if (Report_PayOrders_Day_ReportDetail_List[i].PayOrderType == Report_PayOrders_Day_ReportDetail.TYPE_SALARY_PAY_ODER)
                        item.SubItems.Add("تابع لامر صرف راتب");
                    else
                        item.SubItems.Add("أمر صرف مستقل");
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].PayOrderID.ToString ());
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].EmployeeName);
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].PayOrderDesc );
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].Value .ToString()
                       +" " + Report_PayOrders_Day_ReportDetail_List[i].CurrencySymbol );
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].ExchangeRate  .ToString ());
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].PaysAmount);
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].PaysRemain.ToString() + " " + Report_PayOrders_Day_ReportDetail_List[i].CurrencySymbol);
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].RealPays.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                   
                    //-Report_PayOrders_Day_ReportDetail_List[i].Source_ItemsIN_RealCost) + " " + ReferenceCurrency.CurrencySymbol);
                    //item.SubItems.Add(Report_PayOrders_Day_ReportDetail_List[i].RealPaysValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);

                    //item.UseItemStyleForSubItems = false;
                    item.BackColor = Color.PaleGoldenrod;


                    //if (Report_PayOrders_Day_ReportDetail_List[i].PaysRemain != 0)
                    //    for (int j = 0; j <= 8; j++)
                    //        item.SubItems[j].BackColor = Color.Orange;
                    //else
                    //    for (int j = 0; j <= 8; j++)
                    //        item.SubItems[j].BackColor = Color.LightGreen;

                    //if (Report_PayOrders_Day_ReportDetail_List[i].Bill_Pays_RealValue
                    //    > Report_PayOrders_Day_ReportDetail_List[i].Bill_Pays_Return_RealValue)
                    //    for (int j = 9; j <= 13; j++)
                    //        item.SubItems[j].BackColor = Color.Orange;
                    //else if (Report_PayOrders_Day_ReportDetail_List[i].Bill_Pays_RealValue
                    //    < Report_PayOrders_Day_ReportDetail_List[i].Bill_Pays_Return_RealValue)
                    //    for (int j = 9; j <= 13; j++)
                    //        item.SubItems[j].BackColor = Color.LightGreen;
                    //else
                    //    for (int j = 9; j <= 13; j++)
                    //        item.SubItems[j].BackColor = Color.Yellow;
                    listViewPayOrders.Items.Add(item);


                }
                #endregion
            }
            else if (PayOrdersAccount_.Month != -1)
            {

                #region MonthSection
                if (listViewPayOrders.Name != "ListViewPayOrders_Month")
                {
                    Report_PayOrders_Month_ReportDetail.IntiliazeListView(ref listViewPayOrders);
                }
                List<Report_PayOrders_Month_ReportDetail> Report_PayOrders_Month_ReportDetailList
                                    = new ReportPayOrdersSQL(DB).Get_Report_PayOrders_Month_ReportDetail(PayOrdersAccount_.Year, PayOrdersAccount_.Month);
                for (int i = 0; i < Report_PayOrders_Month_ReportDetailList.Count; i++)
                {
                    ListViewItem item = new ListViewItem(Report_PayOrders_Month_ReportDetailList[i].DayDate.ToShortDateString());
                    item.Name = Report_PayOrders_Month_ReportDetailList[i].DayID.ToString();
                    item.SubItems.Add(Report_PayOrders_Month_ReportDetailList[i].Salary_PayOrders_Count .ToString());
                    item.SubItems.Add(Report_PayOrders_Month_ReportDetailList[i].Other_PayOrders_Count .ToString());
                    item.SubItems.Add(Report_PayOrders_Month_ReportDetailList[i].PayOrders_Value);
                    item.SubItems.Add(Report_PayOrders_Month_ReportDetailList[i].PayOrders_Pays_Value);
                    item.SubItems.Add(Report_PayOrders_Month_ReportDetailList[i].PayOrders_Pays_Remain);

                    item.SubItems.Add(Report_PayOrders_Month_ReportDetailList[i].PayOrders_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_PayOrders_Month_ReportDetailList[i].PayOrders_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);

                    //item.UseItemStyleForSubItems = false;
                    item.BackColor = Color.PaleGoldenrod;

                    //if (Report_PayOrders_Month_ReportDetailList[i].Bills_RealValue > 0 ||
                    //    Report_PayOrders_Month_ReportDetailList[i].Bills_Pays_RealValue > 0)
                    //{
                    //    if (Report_PayOrders_Month_ReportDetailList[i].Bills_Pays_Remain_UPON_BillsCurrency == 0)
                    //        for (int j = 0; j <= 5; j++)
                    //            item.SubItems[j].BackColor = Color.LightGreen;
                    //    else if (Report_PayOrders_Month_ReportDetailList[i].Bills_Pays_Remain_UPON_BillsCurrency < 0)
                    //        for (int j = 0; j <= 5; j++)
                    //            item.SubItems[j].BackColor = Color.LightSkyBlue;
                    //    else
                    //        for (int j = 0; j <= 5; j++)
                    //            item.SubItems[j].BackColor = Color.Orange;

                    //    if (Report_PayOrders_Month_ReportDetailList[i].Bills_Pays_RealValue >
                    //       Report_PayOrders_Month_ReportDetailList[i].Bills_ItemsIN_RealValue)
                    //        for (int j = 6; j <= 10; j++)
                    //            item.SubItems[j].BackColor = Color.LightGreen;
                    //    else
                    //        for (int j = 6; j <= 10; j++)
                    //            item.SubItems[j].BackColor = Color.Orange;
                    //}
                    //else
                    //{
                    //    for (int j = 0; j <= 10; j++)
                    //        item.SubItems[j].BackColor = Color.LightYellow;
                    //}

                    listViewPayOrders.Items.Add(item);

                }
                #endregion
            }
            else if (PayOrdersAccount_.Year != -1)
            {

                #region YearSection
                if (listViewPayOrders.Name != "ListViewPayOrders_Year")
                {

                    Report_PayOrders_Year_ReportDetail.IntiliazeListView(ref listViewPayOrders);

                }

                List<Report_PayOrders_Year_ReportDetail> Report_PayOrders_Year_ReportDetailList
                           = new ReportPayOrdersSQL(DB).Get_Report_PayOrders_Year_ReportDetail(PayOrdersAccount_.Year);
                for (int i = 0; i < Report_PayOrders_Year_ReportDetailList.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Report_PayOrders_Year_ReportDetailList[i].MonthNO.ToString());
                    item.Name = Report_PayOrders_Year_ReportDetailList[i].MonthNO.ToString();
                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].MonthName);

                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].Salary_PayOrders_Count.ToString());
                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].Other_PayOrders_Count.ToString());
                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].PayOrders_Value);
                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].PayOrders_Pays_Value);
                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].PayOrders_Pays_Remain);

                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].PayOrders_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_PayOrders_Year_ReportDetailList[i].PayOrders_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    //item.UseItemStyleForSubItems = false;

                    item.BackColor = Color.PaleGoldenrod;
                    //if (Report_PayOrders_Year_ReportDetailList[i].Bills_RealValue > 0)
                    //{
                    //    if (Report_PayOrders_Year_ReportDetailList[i].Bills_Pays_Remain_UPON_BillsCurrency == 0)
                    //        for (int j = 0; j <= 5; j++)
                    //            item.SubItems[j].BackColor = Color.LightGreen;
                    //    else
                    //        for (int j = 0; j <= 5; j++)
                    //            item.SubItems[j].BackColor = Color.Orange;
                    //    if (Report_PayOrders_Year_ReportDetailList[i].Bills_Pays_RealValue >
                    //  Report_PayOrders_Year_ReportDetailList[i].Bills_ItemsIN_RealValue)
                    //        for (int j = 6; j <= 10; j++)
                    //            item.SubItems[j].BackColor = Color.LightGreen;
                    //    else
                    //        for (int j = 6; j <= 10; j++)
                    //            item.SubItems[j].BackColor = Color.Orange;
                    //}
                    //else
                    //{
                    //    for (int j = 0; j <= 10; j++)
                    //        item.SubItems[j].BackColor = Color.LightYellow;
                    //}

                    listViewPayOrders.Items.Add(item);

                }
                #endregion
            }
            else
            {
                //PayOrdersLabelAccountType.Text = "حساب السنوات";
                //PayOrdersLabelReport.Text = "تقرير حساب السنوات : " + PayOrdersAccount_.GetAccountDateString();

                #region YearRangeSection
                if (listViewPayOrders.Name != "ListViewPayOrders_YearRange")
                {
                    Report_PayOrders_YearRange_ReportDetail.IntiliazeListView(ref listViewPayOrders);
                }
                List<Report_PayOrders_YearRange_ReportDetail> Report_PayOrders_YearRange_ReportDetailList
                           = new ReportPayOrdersSQL(DB).Get_Report_PayOrders_YearRange_ReportDetail(PayOrdersAccount_.YearRange_.min_year, PayOrdersAccount_.YearRange_.max_year);

                for (int i = 0; i < Report_PayOrders_YearRange_ReportDetailList.Count; i++)
                {
                    ListViewItem item = new ListViewItem(Report_PayOrders_YearRange_ReportDetailList[i].YearNO.ToString());
                    item.Name = Report_PayOrders_YearRange_ReportDetailList[i].YearNO.ToString();
                    item.SubItems.Add(Report_PayOrders_YearRange_ReportDetailList[i].Salary_PayOrders_Count.ToString());
                    item.SubItems.Add(Report_PayOrders_YearRange_ReportDetailList[i].Other_PayOrders_Count.ToString());
                    item.SubItems.Add(Report_PayOrders_YearRange_ReportDetailList[i].PayOrders_Value);
                    item.SubItems.Add(Report_PayOrders_YearRange_ReportDetailList[i].PayOrders_Pays_Value);
                    item.SubItems.Add(Report_PayOrders_YearRange_ReportDetailList[i].PayOrders_Pays_Remain);

                    item.SubItems.Add(Report_PayOrders_YearRange_ReportDetailList[i].PayOrders_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_PayOrders_YearRange_ReportDetailList[i].PayOrders_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    //item.UseItemStyleForSubItems = false;
                    item.BackColor = Color.PaleGoldenrod;

                    //if (Report_PayOrders_YearRange_ReportDetailList[i].Bills_RealValue > 0)
                    //{
                    //    if (Report_PayOrders_YearRange_ReportDetailList[i].Bills_Pays_Remain_UPON_BillsCurrency == 0)
                    //        for (int j = 0; j <= 5; j++)
                    //            item.SubItems[j].BackColor = Color.LightGreen;
                    //    else
                    //        for (int j = 0; j <= 5; j++)
                    //            item.SubItems[j].BackColor = Color.Orange;
                    //    if (Report_PayOrders_YearRange_ReportDetailList[i].Bills_Pays_RealValue >
                    //  Report_PayOrders_YearRange_ReportDetailList[i].Bills_ItemsIN_RealValue)
                    //        for (int j = 6; j <= 10; j++)
                    //            item.SubItems[j].BackColor = Color.LightGreen;
                    //    else
                    //        for (int j = 6; j <= 10; j++)
                    //            item.SubItems[j].BackColor = Color.Orange;
                    //}
                    //else
                    //{
                    //    for (int j = 0; j <= 10; j++)
                    //        item.SubItems[j].BackColor = Color.LightYellow;
                    //}

                    listViewPayOrders.Items.Add(item);

                }

                #endregion
            }

            PayOrders_FillReport();


        }
        private void PayOrdersBack_Click(object sender, EventArgs e)
        {
            if (PayOrdersAccount_.Year == -1) return;
            PayOrdersAccount_.Account_Date_UP();
            Refresh_ListViewPayOrders();
        }

        private void PayOrdersButtonLeftRight_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            bool left;
            if (b.Name == "PayOrdersButtonLeft") left = true;
            else left = false;

            if (PayOrdersAccount_.Day != -1)
            {
                if (left)
                {
                    if (PayOrdersAccount_.Day == DateTime.DaysInMonth(PayOrdersAccount_.Year, PayOrdersAccount_.Month))
                    {
                        if (PayOrdersAccount_.Month == 12)
                        { PayOrdersAccount_.Year++; PayOrdersAccount_.Month = 1; PayOrdersAccount_.Day = 1; }
                        else
                        { PayOrdersAccount_.Month++; PayOrdersAccount_.Day = 1; }

                    }
                    else PayOrdersAccount_.Day++;
                }
                else
                {
                    if (PayOrdersAccount_.Day == 1)
                    {

                        if (PayOrdersAccount_.Month == 1)
                        { PayOrdersAccount_.Year--; PayOrdersAccount_.Month = 12; }
                        else
                        { PayOrdersAccount_.Month--; }
                        PayOrdersAccount_.Day = DateTime.DaysInMonth(PayOrdersAccount_.Year, PayOrdersAccount_.Month);
                    }
                    else PayOrdersAccount_.Day--;
                }

            }
            else if (PayOrdersAccount_.Month != -1)
            {
                if (left)
                {
                    if (PayOrdersAccount_.Month == 12)
                    {
                        PayOrdersAccount_.Year++; PayOrdersAccount_.Month = 1;
                    }
                    else PayOrdersAccount_.Month++;
                }
                else
                {
                    if (PayOrdersAccount_.Month == 1)
                    {
                        PayOrdersAccount_.Year--; PayOrdersAccount_.Month = 12;
                    }
                    else PayOrdersAccount_.Month--;
                }
            }
            else if (PayOrdersAccount_.Year != -1)
            {
                if (left)
                {
                    PayOrdersAccount_.Year++;
                    PayOrdersAccount_.YearRange_.min_year++;
                    PayOrdersAccount_.YearRange_.max_year++;
                }
                else
                {
                    PayOrdersAccount_.Year--;
                    PayOrdersAccount_.YearRange_.min_year--;
                    PayOrdersAccount_.YearRange_.max_year--;
                }
            }
            else
            {
                if (left)
                {

                    PayOrdersAccount_.YearRange_.min_year += 10;
                    PayOrdersAccount_.YearRange_.max_year += 10;
                }
                else
                {
                    PayOrdersAccount_.YearRange_.min_year -= 10;
                    PayOrdersAccount_.YearRange_.max_year -= 10;
                }
            }
            Refresh_ListViewPayOrders();
        }

        public void ListViewPayOrdersAccountDown()
        {
            try
            {
                if (PayOrdersAccount_.Year == -1 || PayOrdersAccount_.Month == -1 || PayOrdersAccount_.Day == -1)
                {
                    PayOrdersAccount_.Account_Date_Down(Convert.ToInt32(listViewPayOrders.SelectedItems[0].Name));
                    Refresh_ListViewPayOrders();
                }
                else
                {

                    OpenPayOrder_MenuItem.PerformClick();

                }


            }
            catch (Exception ee)
            {

            }


        }
        private void ListViewPayOrders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listViewPayOrders.SelectedItems .Count >0)
                ListViewPayOrdersAccountDown();
        }
        private void ListViewPayOrders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                ListViewPayOrdersAccountDown();
        }
        private void ListViewPayOrders_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewPayOrders.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewPayOrders.Items)
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
                        if (PayOrdersAccount_.Day != -1)
                        {

                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] {OpenPayOrder_MenuItem , EditPayOrder_MenuItem , DeletePayOrder_MenuItem
                            , new MenuItem("-"),CreatePayOrder_MenuItem });
                            MenuItemList.AddRange(new MenuItem[] { new MenuItem("-"), AddPayOUT_PayOrder_MenuItem });
                            listViewPayOrders.ContextMenu = new ContextMenu(MenuItemList.ToArray());


                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            listViewPayOrders.ContextMenu = new ContextMenu(mi1);


                        }


                    }
                    else
                    {
                        if (PayOrdersAccount_.Day != -1)
                        {
                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"), CreatePayOrder_MenuItem };
                            listViewPayOrders.ContextMenu = new ContextMenu(mi1);
                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            listViewPayOrders.ContextMenu = new ContextMenu(mi1);


                        }

                    }

                }
            }
        }

        public async void IntializeListViewPayOrdersColumnsWidth()
        {

            if (PayOrdersAccount_.Day != -1)
            {


                listViewPayOrders.Columns[0].Width = 75;//time
                listViewPayOrders.Columns[1].Width = 125;//type
                listViewPayOrders.Columns[2].Width = 100;//id
                listViewPayOrders.Columns[3].Width = 200;//owner
                listViewPayOrders.Columns[4].Width = 200;//desc
                listViewPayOrders.Columns[5].Width = 125;//value
                listViewPayOrders.Columns[6].Width = 100;//exchangerate
                listViewPayOrders.Columns[7].Width = 125;//paid
                listViewPayOrders.Columns[8].Width = 125;//remain
                listViewPayOrders.Columns[9].Width = 100;//real value
                listViewPayOrders.Columns[10].Width = 100;//real paid

            }
            else
            {
                listViewPayOrders.Columns[0].Width = 100;//daydate
                listViewPayOrders.Columns[1].Width = 175;//salary count
                listViewPayOrders.Columns[2].Width = 175;//other count

                listViewPayOrders.Columns[3].Width = 140;// value
                listViewPayOrders.Columns[4].Width = 140;//paid
                listViewPayOrders.Columns[5].Width = 140;//remain

                listViewPayOrders.Columns[6].Width = 125;//real value
                listViewPayOrders.Columns[7].Width = 125;//real pays value
            }
        }
        private void CreatePayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelecObjectForm SelecObjectForm_ = new SelecObjectForm("اختر موظف");
                List<Company.Objects.EmployeesReport> EmployeesReportList = new Company.CompanySQL.CompanyReportSQL(DB).GetEmployeesReportList();
                Company.Objects.EmployeesReport.InitializeListView(ref SelecObjectForm_._listView);
                Company.Objects.EmployeesReport.RefreshEmployeesReportList(ref SelecObjectForm_._listView, EmployeesReportList);
                SelecObjectForm_.adjustcolumns = f => Company.Objects.EmployeesReport.AdjustlistViewEmployeesColumnsWidth(ref SelecObjectForm_._listView);
                DialogResult dd = SelecObjectForm_.ShowDialog();
                if (dd != DialogResult.OK) return;
                Employee _Employee = new EmployeeSQL(DB).GetEmployeeInforBYID(SelecObjectForm_.ReturnID);
                EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, _Employee);
                EmployeePayOrderForm_.ShowDialog();
                if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                {
                    Refresh_ListViewPayOrders();
                }
                EmployeePayOrderForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenPayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewPayOrders.SelectedItems.Count > 0)
                {

                    uint sid = Convert.ToUInt32(listViewPayOrders.SelectedItems[0].Name);
                    EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                    EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, false );
                    EmployeePayOrderForm_.ShowDialog();
                    if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                    {
                        Refresh_ListViewPayOrders();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditPayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewPayOrders.SelectedItems.Count > 0)
                {
       
                    uint sid = Convert.ToUInt32(listViewPayOrders.SelectedItems[0].Name);
                    EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                    EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, true);
                    EmployeePayOrderForm_.ShowDialog();
                    if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                    {
                        Refresh_ListViewPayOrders();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeletePayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dd != DialogResult.OK) return;
            uint PayOrderid = Convert.ToUInt32(listViewPayOrders.SelectedItems[0].Name);
            bool success = new EmployeePayOrderSQL (DB).Delete_PayOrder (PayOrderid);
            if (success)
            {
                Refresh_ListViewPayOrders();
            }

        }
        private void AddPayOUT_PayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPayOrders .SelectedItems.Count == 1)
            {
                uint sid = Convert.ToUInt32(listViewPayOrders.SelectedItems[0].Name);
                Company.Objects.EmployeePayOrder EmployeePayOrder_ = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                PayOUTForm PayOUTForm_ = new PayOUTForm(DB, PayOrdersAccount_.GetDate(), EmployeePayOrder_);
                PayOUTForm_.ShowDialog();
                if (PayOUTForm_.DialogResult == DialogResult.OK)
                {
               
                    Refresh_ListViewPayOrders();
        
                }
            }




        }
        #endregion
        #region General
        private void tabPage1_Resize(object sender, EventArgs e)
        {
           
            IntializeListViewPayOrdersColumnsWidth();
        }
        private void Refresh_MenuItem_Click(object sender, EventArgs e)
        {

            Refresh_ListViewPayOrders();
            
        }
      
        #endregion

        private DateTime GetSelectedDate(DateAccount DateAccount_)
        {
            try
            {
                if (DateAccount_.Day != -1)
                    return (new DateTime(DateAccount_.Year, DateAccount_.Month, DateAccount_.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                else 
                {

                    return DateTime.Now;
                }
                
            }
            catch
            {
                return DateTime.Now;
            }

        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {

                Refresh_ListViewPayOrders();
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("MainWindow_Load"+ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
           
        }

        #region ToolStripMenuItem
    

        private void الوظائفوالموظفينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Company.Forms.CompanyManagmentForm CompanyManagmentForm_ = new Company.Forms.CompanyManagmentForm(DB);
                CompanyManagmentForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
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
