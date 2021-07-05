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
    public partial class MainWindow_Sell_Form : Form
    {
        DatabaseInterface DB;

        DateAccount SellsAccount_;


        System.Windows.Forms.MenuItem Refresh_MenuItem;

        System.Windows.Forms.MenuItem CreateBillSell_MenuItem;
        System.Windows.Forms.MenuItem OpenBillSell_MenuItem;
        System.Windows.Forms.MenuItem EditBillSell_MenuItem;
        System.Windows.Forms.MenuItem DeleteBillSell_MenuItem;

        MenuItem AddPayIN_BillSell_MenuItem;

        Currency ReferenceCurrency;
        public MainWindow_Sell_Form(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة المبيعات, لا يمكنك فتح هذه النافذة");

            labelUser.Text = DB.GetUser_EmployeeName(); 
            ReferenceCurrency = new CurrencySQL(DB).GetReferenceCurrency();

            DateAccount  .YearRange yearrange = new DateAccount  .YearRange(DateTime.Today.Year-5, DateTime.Today.Year+5);

            SellsAccount_ = new DateAccount(DB, yearrange, DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
             Initialize_MenuItems();
           
        }
        public async void Initialize_MenuItems()
        {
            Refresh_MenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);

  
            CreateBillSell_MenuItem = new MenuItem("انشاء فاتورة مبيع", CreateBillSell_MenuItem_Click);
            OpenBillSell_MenuItem = new System.Windows.Forms.MenuItem("فتح", OpenBillSell_MenuItem_Click);
            EditBillSell_MenuItem = new System.Windows.Forms.MenuItem("تعديل", EditBillSell_MenuItem_Click);
            DeleteBillSell_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteBillSell_MenuItem_Click);

            AddPayIN_BillSell_MenuItem = new MenuItem("اضافة دفعة تابعة للفاتورة", AddPayIN_BillSell_MenuItem_Click);
        
        }
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                OverLoad_Form form = (OverLoad_Form)sender;
                //if (form.Refresh_ListViewMaintenanceOPRs_Flag) Refresh_ListViewMaintenanceOPRs();
                //if (form.Refresh_ListViewMoneyDataDetails_Flag)
                //{
                //    Refresh_ListViewMoneyDataDetails();
                //    ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                //    MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                //    TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);

                //}
                //if (form.Refresh_ListViewPayOrders_Flag) Refresh_ListViewPayOrders();
                if (form.Refresh_ListViewSells_Flag || form .Refresh_ListViewMoneyDataDetails_Flag ) Refresh_ListViewSells();
                //if (form.Refresh_ListViewBuys_Flag) Refresh_ListViewBuys();



            }
            catch (Exception ee)
            {
                MessageBox.Show("Form_Closed:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }




        #region ReportSells
        public async  void Sells_FillReport()
        {
            try
            {
                SellsLabelAccountDate.Text = SellsAccount_.GetAccountDateString();



                if (SellsAccount_.Day != -1)
                {

                    SellsLabelAccountType.Text = "حساب اليوم";
                    SellsLabelReport.Text = "تقرير حساب اليوم : " + SellsAccount_.GetAccountDateString();
                    #region DaySection

                    Report_Sells_Month_ReportDetail Report_Sells_DayReport
                        = new ReportSellsSQL(DB).Get_Report_Sells_Day_Report(SellsAccount_.Year, SellsAccount_.Month, SellsAccount_.Day);
                    textBoxSells_ItemsINValue.Text = Report_Sells_DayReport.Bills_ItemsIN_Value;
                    textBoxSells_Value.Text = Report_Sells_DayReport.Bills_Value;
                    textBoxSellsPaysValue.Text = Report_Sells_DayReport.Bills_Pays_Value;
                    textBoxSellsPaysRmain.Text = Report_Sells_DayReport.Bills_Pays_Remain;
                    textBoxSells_ItemsIn_RealValue.Text = Report_Sells_DayReport.Bills_ItemsIN_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSellRealValue.Text = Report_Sells_DayReport.Bills_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSells_RealProfit.Text = System.Math.Round((Report_Sells_DayReport.Bills_RealValue - Report_Sells_DayReport.Bills_ItemsIN_RealValue), 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSells_RealPays.Text = Report_Sells_DayReport.Bills_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    #endregion
                }
                else if (SellsAccount_.Month != -1)
                {
                    SellsLabelAccountType.Text = "حساب الشهر";
                    SellsLabelReport.Text = "تقرير حساب الشهر : " + SellsAccount_.GetAccountDateString();

                    #region MonthSection

                    Report_Sells_Year_ReportDetail Report_Sells_MonthReport
                         = new ReportSellsSQL(DB).Get_Report_Sells_Month_Report(SellsAccount_.Year, SellsAccount_.Month);
                    textBoxSells_ItemsINValue.Text = Report_Sells_MonthReport.Bills_ItemsIN_Value;
                    textBoxSells_Value.Text = Report_Sells_MonthReport.Bills_Value;
                    textBoxSellsPaysValue.Text = Report_Sells_MonthReport.Bills_Pays_Value;
                    textBoxSellsPaysRmain.Text = Report_Sells_MonthReport.Bills_Pays_Remain;
                    textBoxSells_ItemsIn_RealValue.Text = Report_Sells_MonthReport.Bills_ItemsIN_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSellRealValue.Text = Report_Sells_MonthReport.Bills_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSells_RealProfit.Text = System.Math.Round((Report_Sells_MonthReport.Bills_RealValue - Report_Sells_MonthReport.Bills_ItemsIN_RealValue), 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSells_RealPays.Text = Report_Sells_MonthReport.Bills_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    #endregion
                }
                else if (SellsAccount_.Year != -1)
                {
                    SellsLabelAccountType.Text = "حساب السنة";
                    SellsLabelReport.Text = "تقرير حساب السنة : " + SellsAccount_.GetAccountDateString();

                    #region YearSection

                    Report_Sells_YearRange_ReportDetail Report_Sells_YearReport
                       = new ReportSellsSQL(DB).Get_Report_Sells_Year_Report(SellsAccount_.Year);
                    textBoxSells_ItemsINValue.Text = Report_Sells_YearReport.Bills_ItemsIN_Value;
                    textBoxSells_Value.Text = Report_Sells_YearReport.Bills_Value;
                    textBoxSellsPaysValue.Text = Report_Sells_YearReport.Bills_Pays_Value;
                    textBoxSellsPaysRmain.Text = Report_Sells_YearReport.Bills_Pays_Remain;
                    textBoxSells_ItemsIn_RealValue.Text = Report_Sells_YearReport.Bills_ItemsIN_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSellRealValue.Text = Report_Sells_YearReport.Bills_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSells_RealProfit.Text = System.Math.Round((Report_Sells_YearReport.Bills_RealValue - Report_Sells_YearReport.Bills_ItemsIN_RealValue), 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    textBoxSells_RealPays.Text = Report_Sells_YearReport.Bills_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                    #endregion
                }
                else
                {
                    SellsLabelAccountType.Text = "حساب السنوات";
                    SellsLabelReport.Text = "تقرير حساب السنوات : " + SellsAccount_.GetAccountDateString();

                    #region YearRangeSection

                    textBoxSells_ItemsINValue.Text = "-";
                    textBoxSells_Value.Text = "-";
                    textBoxSellsPaysValue.Text = "-";
                    textBoxSellsPaysRmain.Text = "-";
                    textBoxSells_ItemsIn_RealValue.Text = "-";
                    textBoxSellRealValue.Text = "-";
                    textBoxSells_RealProfit.Text = "-";
                    textBoxSells_RealPays.Text = "-";
                    #endregion
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("Sells_FillReport:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        public async void Refresh_ListViewSells()
        {
            try
            {
                listViewSells.Items.Clear();
                if (SellsAccount_.Day != -1)
                {

                    #region DaySection


                    if (listViewSells.Name != "ListViewSells_Day")
                    {
                        Report_Sells_Day_ReportDetail.IntiliazeListView(ref listViewSells);

                    }
                    List<Report_Sells_Day_ReportDetail> Report_Sells_Day_ReportDetail_List
                              = new ReportSellsSQL(DB).Get_Report_Sells_Day_ReportDetail(SellsAccount_.Year, SellsAccount_.Month, SellsAccount_.Day);

                    for (int i = 0; i < Report_Sells_Day_ReportDetail_List.Count; i++)
                    {

                        ListViewItem item = new ListViewItem(Report_Sells_Day_ReportDetail_List[i].Bill_Time.ToShortTimeString());
                        item.Name = Report_Sells_Day_ReportDetail_List[i].Bill_ID.ToString();
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].Bill_ID.ToString());
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].SellType);
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].Bill_Owner);
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].ClauseS_Count.ToString());
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].BillValue.ToString() + " " + Report_Sells_Day_ReportDetail_List[i].CurrencySymbol);
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].ExchangeRate.ToString());
                        //item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].PaysCount.ToString());
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].PaysAmount);
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].PaysRemain.ToString() + " " + Report_Sells_Day_ReportDetail_List[i].CurrencySymbol);
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].Source_ItemsIN_Cost_Details);
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].Source_ItemsIN_RealCost.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(System.Math.Round((Report_Sells_Day_ReportDetail_List[i].BillValue /
                            Report_Sells_Day_ReportDetail_List[i].ExchangeRate), 3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add((Report_Sells_Day_ReportDetail_List[i].ItemsOut_RealValue
                            - Report_Sells_Day_ReportDetail_List[i].Source_ItemsIN_RealCost) + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].RealPaysValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        item.UseItemStyleForSubItems = false;
                        if (Report_Sells_Day_ReportDetail_List[i].BillValue > 0
                            || Report_Sells_Day_ReportDetail_List[i].RealPaysValue > 0)
                        {
                            if (Report_Sells_Day_ReportDetail_List[i].PaysRemain != 0)
                                for (int j = 4; j <= 8; j++)
                                    item.SubItems[j].BackColor = Color.Orange;
                            else
                                for (int j = 4; j <= 8; j++)
                                    item.SubItems[j].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            for (int j = 4; j <= 8; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        }

                        if (Report_Sells_Day_ReportDetail_List[i].Source_ItemsIN_RealCost
                            > Report_Sells_Day_ReportDetail_List[i].ItemsOut_RealValue)
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else if (Report_Sells_Day_ReportDetail_List[i].Source_ItemsIN_RealCost
                           < Report_Sells_Day_ReportDetail_List[i].ItemsOut_RealValue)
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.LightYellow;

                        if (Report_Sells_Day_ReportDetail_List[i].Source_ItemsIN_RealCost
                            > Report_Sells_Day_ReportDetail_List[i].RealPaysValue)
                            item.SubItems[13].BackColor = Color.Orange;
                        else if (Report_Sells_Day_ReportDetail_List[i].Source_ItemsIN_RealCost
                           < Report_Sells_Day_ReportDetail_List[i].RealPaysValue)
                            item.SubItems[13].BackColor = Color.LightGreen;
                        else
                            item.SubItems[13].BackColor = Color.LightYellow;
                        listViewSells.Items.Add(item);


                    }
                    #endregion
                }
                else if (SellsAccount_.Month != -1)
                {

                    #region MonthSection
                    if (listViewSells.Name != "ListViewSells_Month")
                    {
                        Report_Sells_Month_ReportDetail.IntiliazeListView(ref listViewSells);
                    }
                    List<Report_Sells_Month_ReportDetail> Report_Sells_Month_ReportDetailList
                                        = new ReportSellsSQL(DB).Get_Report_Sells_Month_ReportDetail(SellsAccount_.Year, SellsAccount_.Month);
                    for (int i = 0; i < Report_Sells_Month_ReportDetailList.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(Report_Sells_Month_ReportDetailList[i].DayDate.ToShortDateString());
                        item.Name = Report_Sells_Month_ReportDetailList[i].DayID.ToString();
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_Count.ToString());
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_Clause_Count.ToString());
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_Value.ToString());
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_Pays_Value);
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_Pays_Remain);
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_ItemsIN_Value);
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_ItemsIN_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add((Report_Sells_Month_ReportDetailList[i].Bills_RealValue - Report_Sells_Month_ReportDetailList[i].Bills_ItemsIN_RealValue) + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_Sells_Month_ReportDetailList[i].Bills_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);

                        item.UseItemStyleForSubItems = false;
                        if (Report_Sells_Month_ReportDetailList[i].Bills_RealValue > 0
                            || Report_Sells_Month_ReportDetailList[i].Bills_Pays_RealValue > 0)
                        {
                            if (Report_Sells_Month_ReportDetailList[i].Bills_Pays_Remain_UPON_BillsCurrency == 0
                                )
                                for (int j = 3; j <= 5; j++)
                                    item.SubItems[j].BackColor = Color.LightGreen;
                            else
                                for (int j = 3; j <= 5; j++)
                                    item.SubItems[j].BackColor = Color.Orange;


                        }
                        else
                        {
                            for (int j = 3; j <= 5; j++)
                                item.SubItems[j].BackColor = Color.LightYellow;
                        }



                        if (Report_Sells_Month_ReportDetailList[i].Bills_ItemsIN_RealValue
                             > Report_Sells_Month_ReportDetailList[i].Bills_RealValue)
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else if (Report_Sells_Month_ReportDetailList[i].Bills_ItemsIN_RealValue
                           < Report_Sells_Month_ReportDetailList[i].Bills_RealValue)
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.LightYellow;

                        if (Report_Sells_Month_ReportDetailList[i].Bills_ItemsIN_RealValue
                            > Report_Sells_Month_ReportDetailList[i].Bills_Pays_RealValue)
                            item.SubItems[10].BackColor = Color.Orange;
                        else if (Report_Sells_Month_ReportDetailList[i].Bills_ItemsIN_RealValue
                           < Report_Sells_Month_ReportDetailList[i].Bills_Pays_RealValue)
                            item.SubItems[10].BackColor = Color.LightGreen;
                        else
                            item.SubItems[10].BackColor = Color.LightYellow;

                        listViewSells.Items.Add(item);

                    }
                    #endregion
                }
                else if (SellsAccount_.Year != -1)
                {

                    #region YearSection
                    if (listViewSells.Name != "ListViewSells_Year")
                    {

                        Report_Sells_Year_ReportDetail.IntiliazeListView(ref listViewSells);

                    }

                    List<Report_Sells_Year_ReportDetail> Report_Sells_Year_ReportDetailList
                               = new ReportSellsSQL(DB).Get_Report_Sells_Year_ReportDetail(SellsAccount_.Year);
                    for (int i = 0; i < Report_Sells_Year_ReportDetailList.Count; i++)
                    {

                        ListViewItem item = new ListViewItem(Report_Sells_Year_ReportDetailList[i].MonthNO.ToString());
                        item.Name = Report_Sells_Year_ReportDetailList[i].MonthNO.ToString();
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].MonthName);
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_Count.ToString());
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_Clause_Count.ToString());
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_Value.ToString());
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_Pays_Value);
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_Pays_Remain);
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_ItemsIN_Value);
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_ItemsIN_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add((Report_Sells_Year_ReportDetailList[i].Bills_RealValue - Report_Sells_Year_ReportDetailList[i].Bills_ItemsIN_RealValue) + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_Sells_Year_ReportDetailList[i].Bills_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);

                        item.UseItemStyleForSubItems = false;
                        if (Report_Sells_Year_ReportDetailList[i].Bills_RealValue > 0
                            || Report_Sells_Year_ReportDetailList[i].Bills_Pays_RealValue > 0)
                        {
                            if (Report_Sells_Year_ReportDetailList[i].Bills_Pays_Remain_UPON_BillsCurrency == 0)
                                for (int j = 3; j <= 5; j++)
                                    item.SubItems[j].BackColor = Color.LightGreen;
                            else
                                for (int j = 3; j <= 5; j++)
                                    item.SubItems[j].BackColor = Color.Orange;


                        }
                        else
                        {
                            for (int j = 3; j <= 5; j++)
                                item.SubItems[j].BackColor = Color.LightYellow;
                        }



                        if (Report_Sells_Year_ReportDetailList[i].Bills_ItemsIN_RealValue
                             > Report_Sells_Year_ReportDetailList[i].Bills_RealValue)
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else if (Report_Sells_Year_ReportDetailList[i].Bills_ItemsIN_RealValue
                           < Report_Sells_Year_ReportDetailList[i].Bills_RealValue)
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.LightYellow;

                        if (Report_Sells_Year_ReportDetailList[i].Bills_ItemsIN_RealValue
                            > Report_Sells_Year_ReportDetailList[i].Bills_Pays_RealValue)
                            item.SubItems[10].BackColor = Color.Orange;
                        else if (Report_Sells_Year_ReportDetailList[i].Bills_ItemsIN_RealValue
                           < Report_Sells_Year_ReportDetailList[i].Bills_Pays_RealValue)
                            item.SubItems[10].BackColor = Color.LightGreen;
                        else
                            item.SubItems[10].BackColor = Color.LightYellow;


                        listViewSells.Items.Add(item);

                    }
                    #endregion
                }
                else
                {
                    //SellsLabelAccountType.Text = "حساب السنوات";
                    //SellsLabelReport.Text = "تقرير حساب السنوات : " + SellsAccount_.GetAccountDateString();

                    #region YearRangeSection
                    if (listViewSells.Name != "ListViewSells_YearRange")
                    {
                        Report_Sells_YearRange_ReportDetail.IntiliazeListView(ref listViewSells);
                    }
                    List<Report_Sells_YearRange_ReportDetail> Report_Sells_YearRange_ReportDetailList
                               = new ReportSellsSQL(DB).Get_Report_Sells_YearRange_ReportDetail(SellsAccount_.YearRange_.min_year, SellsAccount_.YearRange_.max_year);

                    for (int i = 0; i < Report_Sells_YearRange_ReportDetailList.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(Report_Sells_YearRange_ReportDetailList[i].YearNO.ToString());
                        item.Name = Report_Sells_YearRange_ReportDetailList[i].YearNO.ToString();
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_Count.ToString());
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_Clause_Count.ToString());
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_Value.ToString());
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_Pays_Value);
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_Pays_Remain);
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_ItemsIN_Value);
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_ItemsIN_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(System.Math.Round((Report_Sells_YearRange_ReportDetailList[i].Bills_RealValue - Report_Sells_YearRange_ReportDetailList[i].Bills_ItemsIN_RealValue), 2) + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_Sells_YearRange_ReportDetailList[i].Bills_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);

                        item.UseItemStyleForSubItems = false;
                        if (Report_Sells_YearRange_ReportDetailList[i].Bills_RealValue > 0
                            || Report_Sells_YearRange_ReportDetailList[i].Bills_Pays_RealValue > 0)
                        {
                            if (Report_Sells_YearRange_ReportDetailList[i].Bills_Pays_Remain_UPON_BillsCurrency == 0)
                                for (int j = 3; j <= 5; j++)
                                    item.SubItems[j].BackColor = Color.LightGreen;
                            else
                                for (int j = 3; j <= 5; j++)
                                    item.SubItems[j].BackColor = Color.Orange;


                        }
                        else
                        {
                            for (int j = 3; j <= 5; j++)
                                item.SubItems[j].BackColor = Color.LightYellow;
                        }



                        if (Report_Sells_YearRange_ReportDetailList[i].Bills_ItemsIN_RealValue
                             > Report_Sells_YearRange_ReportDetailList[i].Bills_RealValue)
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else if (Report_Sells_YearRange_ReportDetailList[i].Bills_ItemsIN_RealValue
                           < Report_Sells_YearRange_ReportDetailList[i].Bills_RealValue)
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 6; j <= 9; j++)
                                item.SubItems[j].BackColor = Color.LightYellow;

                        if (Report_Sells_YearRange_ReportDetailList[i].Bills_ItemsIN_RealValue
                            > Report_Sells_YearRange_ReportDetailList[i].Bills_Pays_RealValue)
                            item.SubItems[10].BackColor = Color.Orange;
                        else if (Report_Sells_YearRange_ReportDetailList[i].Bills_ItemsIN_RealValue
                           < Report_Sells_YearRange_ReportDetailList[i].Bills_Pays_RealValue)
                            item.SubItems[10].BackColor = Color.LightGreen;
                        else
                            item.SubItems[10].BackColor = Color.LightYellow;


                        listViewSells.Items.Add(item);

                    }

                    #endregion
                }

                Sells_FillReport();

            }
            catch (Exception ee)
            {
                MessageBox.Show("Refresh_ListViewSells:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          


        }
        private void SellsBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellsAccount_.Year == -1) return;
                SellsAccount_.Account_Date_UP();
                Refresh_ListViewSells();
            }
            catch (Exception ee)
            {
                MessageBox.Show("SellsBack_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        private void SellsButtonLeftRight_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                bool left;
                if (b.Name == "SellsButtonLeft") left = true;
                else left = false;

                if (SellsAccount_.Day != -1)
                {
                    if (left)
                    {
                        if (SellsAccount_.Day == DateTime.DaysInMonth(SellsAccount_.Year, SellsAccount_.Month))
                        {
                            if (SellsAccount_.Month == 12)
                            { SellsAccount_.Year++; SellsAccount_.Month = 1; SellsAccount_.Day = 1; }
                            else
                            { SellsAccount_.Month++; SellsAccount_.Day = 1; }

                        }
                        else SellsAccount_.Day++;
                    }
                    else
                    {
                        if (SellsAccount_.Day == 1)
                        {

                            if (SellsAccount_.Month == 1)
                            { SellsAccount_.Year--; SellsAccount_.Month = 12; }
                            else
                            { SellsAccount_.Month--; }
                            SellsAccount_.Day = DateTime.DaysInMonth(SellsAccount_.Year, SellsAccount_.Month);
                        }
                        else SellsAccount_.Day--;
                    }

                }
                else if (SellsAccount_.Month != -1)
                {
                    if (left)
                    {
                        if (SellsAccount_.Month == 12)
                        {
                            SellsAccount_.Year++; SellsAccount_.Month = 1;
                        }
                        else SellsAccount_.Month++;
                    }
                    else
                    {
                        if (SellsAccount_.Month == 1)
                        {
                            SellsAccount_.Year--; SellsAccount_.Month = 12;
                        }
                        else SellsAccount_.Month--;
                    }
                }
                else if (SellsAccount_.Year != -1)
                {
                    if (left)
                    {
                        SellsAccount_.Year++;
                        SellsAccount_.YearRange_.min_year++;
                        SellsAccount_.YearRange_.max_year++;
                    }
                    else
                    {
                        SellsAccount_.Year--;
                        SellsAccount_.YearRange_.min_year--;
                        SellsAccount_.YearRange_.max_year--;
                    }
                }
                else
                {
                    if (left)
                    {

                        SellsAccount_.YearRange_.min_year += 10;
                        SellsAccount_.YearRange_.max_year += 10;
                    }
                    else
                    {
                        SellsAccount_.YearRange_.min_year -= 10;
                        SellsAccount_.YearRange_.max_year -= 10;
                    }
                }
                Refresh_ListViewSells();
            }
            catch (Exception ee)
            {
                MessageBox.Show("SellsButtonLeftRight_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        public void ListViewSellsAccountDown()
        {
            try
            {
                if (SellsAccount_.Year == -1 || SellsAccount_.Month == -1 || SellsAccount_.Day == -1)
                {
                    SellsAccount_.Account_Date_Down(Convert.ToInt32(listViewSells.SelectedItems[0].Name));
                    Refresh_ListViewSells();
                }
                else
                {

                    OpenBillSell_MenuItem.PerformClick();
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("ListViewSellsAccountDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        



        }
        private void ListViewSells_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewSells .SelectedItems.Count > 0)
                ListViewSellsAccountDown();
        }
        private void ListViewSells_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                ListViewSellsAccountDown();
        }
        private void ListViewSells_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    listViewSells.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        foreach (ListViewItem item1 in listViewSells.Items)
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
                            if (SellsAccount_.Day != -1)
                            {

                                List<MenuItem> MenuItemList = new List<MenuItem>();
                                MenuItemList.Add(Refresh_MenuItem);
                                MenuItemList.Add(new MenuItem("-"));
                                MenuItemList.AddRange(new MenuItem[] {OpenBillSell_MenuItem , EditBillSell_MenuItem , DeleteBillSell_MenuItem
                            , new MenuItem("-"),CreateBillSell_MenuItem });
                                MenuItemList.AddRange(new MenuItem[] { new MenuItem("-"), AddPayIN_BillSell_MenuItem });
                                listViewSells.ContextMenu = new ContextMenu(MenuItemList.ToArray());


                            }
                            else
                            {

                                MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                                listViewSells.ContextMenu = new ContextMenu(mi1);


                            }


                        }
                        else
                        {
                            if (SellsAccount_.Day != -1)
                            {
                                MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"), CreateBillSell_MenuItem };
                                listViewSells.ContextMenu = new ContextMenu(mi1);
                            }
                            else
                            {

                                MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                                listViewSells.ContextMenu = new ContextMenu(mi1);


                            }

                        }

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("ListViewSells_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public async void IntializeListViewSellsColumnsWidth()
        {
            try
            {
                if (SellsAccount_.Day != -1)
                {


                    listViewSells.Columns[0].Width = 75;//time
                    listViewSells.Columns[1].Width = 60;//id
                    listViewSells.Columns[2].Width = 75;//selltype
                    listViewSells.Columns[3].Width = 100;//owner
                    listViewSells.Columns[4].Width = 75;//clause count
                    listViewSells.Columns[5].Width = 90;//value
                    listViewSells.Columns[6].Width = 100;//exchangerate
                    listViewSells.Columns[7].Width = 130;//paid
                    listViewSells.Columns[8].Width = 90;//remain
                    listViewSells.Columns[9].Width = 100;//item in cost
                    listViewSells.Columns[10].Width = 130;//real item in cost
                    listViewSells.Columns[11].Width = 130;//real items out cost
                    listViewSells.Columns[12].Width = 130;//real profit value
                    listViewSells.Columns[13].Width = 130;//real pays value
                }
                else
                {
                    listViewSells.Columns[0].Width = 100;//--
                    listViewSells.Columns[1].Width = 120;//bills count
                    listViewSells.Columns[2].Width = 115;//clause count
                    listViewSells.Columns[3].Width = 125;//bill value
                    listViewSells.Columns[4].Width = 125;//bills pays value
                    listViewSells.Columns[5].Width = 120;//remain
                    listViewSells.Columns[6].Width = 140;//item in value
                    listViewSells.Columns[7].Width = 115;//item in real value
                    listViewSells.Columns[8].Width = 145;//real value
                    listViewSells.Columns[9].Width = 115;//profit
                    listViewSells.Columns[10].Width = 125;//real p
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("IntializeListViewSellsColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        private void CreateBillSell_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BillSellForm BillINForm_ = new BillSellForm(DB, DateTime.Now, null);
                BillINForm_.FormClosed += Form_Closed ;
                BillINForm_.Show ();
              
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateBillSell_MenuItem_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
       
        }
        private void OpenBillSell_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint billSellid = Convert.ToUInt32(listViewSells.SelectedItems[0].Name);
                BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(billSellid);
                BillSellForm BillOUTForm_ = new BillSellForm(DB, BillSell_, false);
                BillOUTForm_.FormClosed += Form_Closed;
                BillOUTForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenBillSell_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void EditBillSell_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint billSellid = Convert.ToUInt32(listViewSells.SelectedItems[0].Name);
                BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(billSellid);
                BillSellForm BillOUTForm_ = new BillSellForm(DB, BillSell_, true);
                BillOUTForm_.FormClosed += Form_Closed;
                BillOUTForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditBillSell_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void DeleteBillSell_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint billSellid = Convert.ToUInt32(listViewSells.SelectedItems[0].Name);
                bool success = new BillSellSQL(DB).DeleteBillSell(billSellid);
                if (success)
                {
                    Refresh_ListViewSells();
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteBillSell_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
   
        }
        private void AddPayIN_BillSell_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSells.SelectedItems.Count == 1)
                {

                    uint sid = Convert.ToUInt32(listViewSells.SelectedItems[0].Name);
                    BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(sid);
                    PayINForm PayINForm_ = new PayINForm(DB, DateTime.Now, BillSell_);
                    PayINForm_.FormClosed += Form_Closed;
                    PayINForm_.Show();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddPayIN_BillSell_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
            

        }
        #endregion
       
        #region General
        private void tabPage1_Resize(object sender, EventArgs e)
        {
            
            IntializeListViewSellsColumnsWidth();
  
        }
        private void Refresh_MenuItem_Click(object sender, EventArgs e)
        {

            Refresh_ListViewSells();

        }

        #endregion




     

       

      
   
        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {

                Refresh_ListViewSells();
   
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("MainWindow_Load"+ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
           
        }

        #region ToolStripMenuItem
   
        private void ادارةالعناصرToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                ItemObj.Forms.User_ShowItemsForm ShowItemsForm_ = new ItemObj.Forms.User_ShowItemsForm(DB, null, ItemObj.Forms.User_ShowItemsForm.SHOW_ITEMS);
                ShowItemsForm_.Show();
            }
            catch(Exception ee)
            {
                MessageBox.Show(""+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void ادارةالعملاءToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Trade.Forms.TradeContact.ShowContactsForm ShowContactsForm_ = new Trade.Forms.TradeContact.ShowContactsForm(DB, false);
                ShowContactsForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ادارةالمستودعToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Trade.Forms.Container.User_ShowLocationsForm ShowLocations_ = new Trade.Forms.Container.User_ShowLocationsForm(DB, null , Trade.Forms.Container.User_ShowLocationsForm.SELECT_Place);
                ShowLocations_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void عرضالموادالمتوفرةحسبالصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OverLoad_Client.ItemObj.Forms.User_AvailabeItemsForm AvailabeItemsForm_ = new OverLoad_Client.ItemObj.Forms.User_AvailabeItemsForm(DB, null, false);
                AvailabeItemsForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void عرضكلالموادالمتوفرةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OverLoad_Client.ItemObj.Forms.ShowAvailableItemSimpleForm ShowAvailableItemSimpleForm_ = new OverLoad_Client.ItemObj.Forms.ShowAvailableItemSimpleForm(DB, false);
                ShowAvailableItemSimpleForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void التفكيكوالتجميعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IndustrialForm IndustrialForm_ = new IndustrialForm(DB);
            IndustrialForm_.Show();
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
