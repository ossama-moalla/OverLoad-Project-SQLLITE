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
    public partial class MainWindow_Maintenance_Form : Form
    {
        DatabaseInterface DB;

        DateAccount MaintenanceOPRsAccount_;


        System.Windows.Forms.MenuItem Refresh_MenuItem;
      
        System.Windows.Forms.MenuItem CreateMaintenanceOPR_MenuItem;
        System.Windows.Forms.MenuItem OpenMaintenanceOPR_MenuItem;
        System.Windows.Forms.MenuItem EditMaintenanceOPR_MenuItem;
        System.Windows.Forms.MenuItem DeleteMaintenanceOPR_MenuItem;


        MenuItem AddPayIN_BillMaintenance_MenuItem;
   
        Currency ReferenceCurrency;
        public MainWindow_Maintenance_Form(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة عمليات الصيانة, لا يمكنك فتح هذه النافذة");

            labelUser.Text = DB.GetUser_EmployeeName(); 
            ReferenceCurrency = new CurrencySQL(DB).GetReferenceCurrency();
 
            DateAccount  .YearRange yearrange = new DateAccount  .YearRange(DateTime.Today.Year-5, DateTime.Today.Year+5);

      MaintenanceOPRsAccount_ = new DateAccount(DB, yearrange, DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            Initialize_MenuItems();
           
        }
        public async void Initialize_MenuItems()
        {
            Refresh_MenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);

            CreateMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("انشاء عملية صيانة", CreateMaintenanceOPR_MenuItem_Click);
            OpenMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("فتح", OpenMaintenanceOPR_MenuItem_Click);
            EditMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("تعديل", EditMaintenanceOPR_MenuItem_Click);
            DeleteMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteMaintenanceOPR_MenuItem_Click);
            AddPayIN_BillMaintenance_MenuItem = new MenuItem("اضافة دفعة تابعة للفاتورة", AddPayIN_BillMaintenance_MenuItem_Click);

        }
    
        #region ReportMaintenanceOPRs
        public async void MaintenanceOPRs_FillReport()
        {

            MaintenanceOPRsLabelAccountDate.Text = MaintenanceOPRsAccount_.GetAccountDateString();



            if (MaintenanceOPRsAccount_.Day != -1)
            {

                MaintenanceOPRsLabelAccountType.Text = "حساب اليوم";
                MaintenanceOPRsLabelReport.Text = "تقرير حساب اليوم : " + MaintenanceOPRsAccount_.GetAccountDateString();
                #region DaySection

                Report_MaintenanceOPRs_Month_ReportDetail Report_MaintenanceOPRs_DayReport
                    = new ReportMaintenanceOPRsSQL(DB).Get_Report_MaintenanceOPRs_Day_Report(MaintenanceOPRsAccount_.Year, MaintenanceOPRsAccount_.Month, MaintenanceOPRsAccount_.Day);
                textBoxMaintenanceOPRs_Count.Text = Report_MaintenanceOPRs_DayReport.MaintenanceOPRs_Count .ToString();
                textBoxMaintenanceOPRs_EndWorkCount.Text = Report_MaintenanceOPRs_DayReport.MaintenanceOPRs_EndWork_Count.ToString();
                textBoxMaintenanceOPRs_RepairedCount.Text = Report_MaintenanceOPRs_DayReport.MaintenanceOPRs_Repaired_Count.ToString(); ;
                textBoxMaintenanceOPRs_EndWarrantyCount.Text = Report_MaintenanceOPRs_DayReport.MaintenanceOPRs_EndWarranty_Count.ToString(); ;
                textBoxMaintenanceOPRs_Warrantycount.Text = Report_MaintenanceOPRs_DayReport.MaintenanceOPRs_Warranty_Count.ToString(); ;

                textBoxBillMaintenanceOPRs_Value.Text = Report_MaintenanceOPRs_DayReport.BillMaintenances_Value ;
                textBoxBillMaintenanceOPRs__PaysValue.Text = Report_MaintenanceOPRs_DayReport.BillMaintenances_Pays_Value;
                textBoxBillMaintenanceOPRs__PaysRmain.Text = Report_MaintenanceOPRs_DayReport.BillMaintenances_Pays_Remain;
                textBoxBillMaintenanceOPRs__ItemsOutValue.Text = Report_MaintenanceOPRs_DayReport.BillMaintenances_ItemsOut_Value;
                textBoxBillMaintenanceOPRs_ItemsOutRealValue.Text = Report_MaintenanceOPRs_DayReport.BillMaintenances_ItemsOut_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealValue.Text = Report_MaintenanceOPRs_DayReport.BillMaintenances_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealPays.Text = Report_MaintenanceOPRs_DayReport.BillMaintenances_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;

              
                #endregion
            }
            else if (MaintenanceOPRsAccount_.Month != -1)
            {
                MaintenanceOPRsLabelAccountType.Text = "حساب الشهر";
                MaintenanceOPRsLabelReport.Text = "تقرير حساب الشهر : " + MaintenanceOPRsAccount_.GetAccountDateString();

                #region MonthSection

                Report_MaintenanceOPRs_Year_ReportDetail Report_MaintenanceOPRs_MonthReport
                     = new ReportMaintenanceOPRsSQL(DB).Get_Report_MaintenanceOPRs_Month_Report(MaintenanceOPRsAccount_.Year, MaintenanceOPRsAccount_.Month);
                textBoxMaintenanceOPRs_Count.Text = Report_MaintenanceOPRs_MonthReport.MaintenanceOPRs_Count.ToString();
                textBoxMaintenanceOPRs_EndWorkCount.Text = Report_MaintenanceOPRs_MonthReport.MaintenanceOPRs_EndWork_Count.ToString();
                textBoxMaintenanceOPRs_RepairedCount.Text = Report_MaintenanceOPRs_MonthReport.MaintenanceOPRs_Repaired_Count.ToString(); ;
                textBoxMaintenanceOPRs_EndWarrantyCount.Text = Report_MaintenanceOPRs_MonthReport.MaintenanceOPRs_EndWarranty_Count.ToString(); ;
                textBoxMaintenanceOPRs_Warrantycount.Text = Report_MaintenanceOPRs_MonthReport.MaintenanceOPRs_Warranty_Count.ToString(); ;

                textBoxBillMaintenanceOPRs_Value.Text = Report_MaintenanceOPRs_MonthReport.BillMaintenances_Value;
                textBoxBillMaintenanceOPRs__PaysValue.Text = Report_MaintenanceOPRs_MonthReport.BillMaintenances_Pays_Value;
                textBoxBillMaintenanceOPRs__PaysRmain.Text = Report_MaintenanceOPRs_MonthReport.BillMaintenances_Pays_Remain;
                textBoxBillMaintenanceOPRs__ItemsOutValue.Text = Report_MaintenanceOPRs_MonthReport.BillMaintenances_ItemsOut_Value;
                textBoxBillMaintenanceOPRs_ItemsOutRealValue.Text = Report_MaintenanceOPRs_MonthReport.BillMaintenances_ItemsOut_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealValue.Text = Report_MaintenanceOPRs_MonthReport.BillMaintenances_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealPays.Text = Report_MaintenanceOPRs_MonthReport.BillMaintenances_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                #endregion
            }
            else if (MaintenanceOPRsAccount_.Year != -1)
            {
                MaintenanceOPRsLabelAccountType.Text = "حساب السنة";
                MaintenanceOPRsLabelReport.Text = "تقرير حساب السنة : " + MaintenanceOPRsAccount_.GetAccountDateString();

                #region YearSection

                Report_MaintenanceOPRs_YearRange_ReportDetail Report_MaintenanceOPRs_YearReport
                   = new ReportMaintenanceOPRsSQL(DB).Get_Report_MaintenanceOPRs_Year_Report(MaintenanceOPRsAccount_.Year);
                textBoxMaintenanceOPRs_Count.Text = Report_MaintenanceOPRs_YearReport.MaintenanceOPRs_Count.ToString();
                textBoxMaintenanceOPRs_EndWorkCount.Text = Report_MaintenanceOPRs_YearReport.MaintenanceOPRs_EndWork_Count.ToString();
                textBoxMaintenanceOPRs_RepairedCount.Text = Report_MaintenanceOPRs_YearReport.MaintenanceOPRs_Repaired_Count.ToString(); ;
                textBoxMaintenanceOPRs_EndWarrantyCount.Text = Report_MaintenanceOPRs_YearReport.MaintenanceOPRs_EndWarranty_Count.ToString(); ;
                textBoxMaintenanceOPRs_Warrantycount.Text = Report_MaintenanceOPRs_YearReport.MaintenanceOPRs_Warranty_Count.ToString(); ;

                textBoxBillMaintenanceOPRs_Value.Text = Report_MaintenanceOPRs_YearReport.BillMaintenances_Value;
                textBoxBillMaintenanceOPRs__PaysValue.Text = Report_MaintenanceOPRs_YearReport.BillMaintenances_Pays_Value;
                textBoxBillMaintenanceOPRs__PaysRmain.Text = Report_MaintenanceOPRs_YearReport.BillMaintenances_Pays_Remain;
                textBoxBillMaintenanceOPRs__ItemsOutValue.Text = Report_MaintenanceOPRs_YearReport.BillMaintenances_ItemsOut_Value;
                textBoxBillMaintenanceOPRs_ItemsOutRealValue.Text = Report_MaintenanceOPRs_YearReport.BillMaintenances_ItemsOut_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealValue.Text = Report_MaintenanceOPRs_YearReport.BillMaintenances_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealPays.Text = Report_MaintenanceOPRs_YearReport.BillMaintenances_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol;
                #endregion
            }
            else
            {
                MaintenanceOPRsLabelAccountType.Text = "حساب السنوات";
                MaintenanceOPRsLabelReport.Text = "تقرير حساب السنوات : " + MaintenanceOPRsAccount_.GetAccountDateString();

                #region YearRangeSection
                textBoxMaintenanceOPRs_Count.Text = "#####";
                textBoxMaintenanceOPRs_EndWorkCount.Text = "#####";
                textBoxMaintenanceOPRs_RepairedCount.Text = "#####";
                textBoxMaintenanceOPRs_EndWarrantyCount.Text = "#####";
                textBoxMaintenanceOPRs_Warrantycount.Text = "#####";

                textBoxBillMaintenanceOPRs_Value.Text = "#####";
                textBoxBillMaintenanceOPRs__PaysValue.Text = "#####";
                textBoxBillMaintenanceOPRs__PaysRmain.Text = "#####";
                textBoxBillMaintenanceOPRs__ItemsOutValue.Text = "#####";
                textBoxBillMaintenanceOPRs_ItemsOutRealValue.Text = "#####";
                textBoxBillMaintenanceOPRs__RealValue.Text = "#####";
                textBoxBillMaintenanceOPRs__RealPays.Text = "#####";
                #endregion
            }


        }
        public async void Refresh_ListViewMaintenanceOPRs()
        {

            listViewMaintenanceOPRs.Items.Clear();
            if (MaintenanceOPRsAccount_.Day != -1)
            {

                #region DaySection


                if (listViewMaintenanceOPRs.Name != "ListViewMaintenanceOPRs_Day")
                {
                    Report_MaintenanceOPRs_Day_ReportDetail.IntiliazeListView(ref listViewMaintenanceOPRs);

                }
                List<Report_MaintenanceOPRs_Day_ReportDetail> Report_MaintenanceOPRs_Day_ReportDetail_List
                          = new ReportMaintenanceOPRsSQL(DB).Get_Report_MaintenanceOPRs_Day_ReportDetail(MaintenanceOPRsAccount_.Year, MaintenanceOPRsAccount_.Month, MaintenanceOPRsAccount_.Day);
                for (int i = 0; i < Report_MaintenanceOPRs_Day_ReportDetail_List.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_Date  .ToShortTimeString());
                    item.Name = Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_ID.ToString();
                    item.UseItemStyleForSubItems = false;
                    item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_ID .ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_Owner );
                    item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].ItemName);
                    item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].ItemCompany );
                    item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].FolderName);
                    item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].FalutDesc);
                    for (int j = 0; j <=6; j++)
                        item.SubItems[j].BackColor = Color.LightYellow ;
                    if (Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_Endworkdate == null)
                    {
                        item.SubItems.Add("في الصيانة");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        for (int j = 7; j < 11; j++)
                            item.SubItems[j].BackColor = Color.Orange;

                    }
                    else
                    {
                    
                        item.SubItems.Add(
                            Convert.ToDateTime(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_Endworkdate).ToShortDateString());
                        item.SubItems[7].BackColor = Color.LimeGreen;
                        bool repaired = Convert.ToBoolean(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_Rpaired);
                        if (repaired)
                        {
                            item.SubItems.Add("تم الاصلاح");
                            item.SubItems[8].BackColor = Color.LimeGreen;
                        }
                        else
                        {
                            item.SubItems.Add("لم يتم الاصلاح");
                            item.SubItems[8].BackColor = Color.Orange ;
                        }
                        if(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_DeliverDate!=null )
                        {
                            item.SubItems.Add(
                                                  Convert.ToDateTime(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_DeliverDate).ToShortDateString());
                            item.SubItems[9].BackColor = Color.LimeGreen ;
                        }
                        else 
                        {
                            item.SubItems.Add("-");
                            item.SubItems[9].BackColor = Color.Orange;
                        }
                        if (Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_EndWarrantyDate != null)
                        {
                            item.SubItems.Add(
                                                     Convert.ToDateTime(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_EndWarrantyDate).ToShortDateString());

                            if(Convert.ToDateTime(Report_MaintenanceOPRs_Day_ReportDetail_List[i].MaintenanceOPR_EndWarrantyDate)>DateTime.Now )
                            item.SubItems[10].BackColor = Color.LimeGreen;
                            else
                                item.SubItems[10].BackColor = Color.Orange  ;
                        }
                        else 
                        {
                            item.SubItems.Add("لا يوجد ضمان");
                            item.SubItems[10].BackColor = Color.Yellow ;
                        }
                        

                    }
                    
                    try
                    {

                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].BillValue.ToString() + " " + Report_MaintenanceOPRs_Day_ReportDetail_List[i].CurrencySymbol);
                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].ExchangeRate.ToString());
                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].PaysAmount);

                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].PaysRemain.ToString() + " " + Report_MaintenanceOPRs_Day_ReportDetail_List[i].CurrencySymbol);
                        if (Report_MaintenanceOPRs_Day_ReportDetail_List[i].PaysRemain == 0)
                            for (int j = 11; j <= 13; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 11; j <= 13; j++)
                                item.SubItems[j].BackColor = Color.LimeGreen;
                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].Bill_ItemsOut_Value.ToString());
                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].Bill_ItemsOut_RealValue .ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].Bill_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(Report_MaintenanceOPRs_Day_ReportDetail_List[i].Bill_Pays_RealValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        if(Report_MaintenanceOPRs_Day_ReportDetail_List[i].Bill_Pays_RealValue> Report_MaintenanceOPRs_Day_ReportDetail_List[i].Bill_RealValue)
                            for (int j = 16; j <= 18; j++)
                                item.SubItems[j].BackColor = Color.LimeGreen;
                        else
                            for (int j = 16; j <= 18; j++)
                                item.SubItems[j].BackColor = Color.Orange ;
                    }
                    catch
                    {
                        
                        item.SubItems.Add("غير منشاة");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        for (int j = 11; j <= 18; j++)
                            item.SubItems[j].BackColor = Color.Orange ;
                    }
                  
                   
                    listViewMaintenanceOPRs.Items.Add(item);


                }
                #endregion
            }
            else if (MaintenanceOPRsAccount_.Month != -1)
            {

                #region MonthSection
                if (listViewMaintenanceOPRs.Name != "ListViewMaintenanceOPRs_Month")
                {
                    Report_MaintenanceOPRs_Month_ReportDetail.IntiliazeListView(ref listViewMaintenanceOPRs);
                }
                List<Report_MaintenanceOPRs_Month_ReportDetail> Report_MaintenanceOPRs_Month_ReportDetailList
                                    = new ReportMaintenanceOPRsSQL(DB).Get_Report_MaintenanceOPRs_Month_ReportDetail(MaintenanceOPRsAccount_.Year, MaintenanceOPRsAccount_.Month);
                for (int i = 0; i < Report_MaintenanceOPRs_Month_ReportDetailList.Count; i++)
                {
                    ListViewItem item = new ListViewItem(Report_MaintenanceOPRs_Month_ReportDetailList[i].DayDate.ToShortDateString());
                    item.Name = Report_MaintenanceOPRs_Month_ReportDetailList[i].DayID.ToString();
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Count .ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_EndWork_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Repaired_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Warranty_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_EndWarranty_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Pays_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Pays_Remain);

                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_ItemsOut_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);


                     item.UseItemStyleForSubItems = false;
                    if(Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Count>0)
                    {
                        if (Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Count ==
                        Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_EndWork_Count)
                        {
                            for (int j = 1; j <= 2; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            for (int j = 1; j <= 2; j++)
                                item.SubItems[j].BackColor = Color.Orange;

                        }
                        if (Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Count ==
                         Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Repaired_Count)
                        {
                            item.SubItems[3].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            item.SubItems[3].BackColor = Color.Orange;

                        }
                        if(Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Warranty_Count>0)
                        {
                            if (Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_Warranty_Count ==
                        Report_MaintenanceOPRs_Month_ReportDetailList[i].MaintenanceOPRs_EndWarranty_Count)
                            {
                                item.SubItems[4].BackColor = Color.LightGreen;
                                item.SubItems[5].BackColor = Color.LightGreen;
                            }
                            else
                            {
                                item.SubItems[4].BackColor = Color.Orange;
                                item.SubItems[5].BackColor = Color.Orange;

                            }
                        }
                        else
                        {
                            item.SubItems[4].BackColor = Color.LightYellow ;
                            item.SubItems[5].BackColor = Color.LightYellow;
                        }
                        
                    }
                    else
                        for (int j = 1; j <= 5; j++)
                            item.SubItems[j].BackColor = Color.LightYellow ;
                    if (Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_RealValue > 0 ||
                        Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Pays_RealValue > 0)
                    {
                        if (Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency == 0)
                            for (int j =6; j <= 8; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else 
                            for (int j = 6; j <=8; j++)
                                item.SubItems[j].BackColor = Color.Orange ;
                    }
                    else
                    {
                        for (int j = 6; j <= 8; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    }
                    if (Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_RealValue>0
                        || Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Pays_RealValue>0)
                    {
                        if (Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_Pays_RealValue >
                           Report_MaintenanceOPRs_Month_ReportDetailList[i].BillMaintenances_RealValue)
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                    }
                    else
                        for (int j = 9; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightYellow ;


                    listViewMaintenanceOPRs.Items.Add(item);

                }
                #endregion
            }
            else if (MaintenanceOPRsAccount_.Year != -1)
            {

                #region YearSection
                if (listViewMaintenanceOPRs.Name != "ListViewMaintenanceOPRs_Year")
                {

                    Report_MaintenanceOPRs_Year_ReportDetail.IntiliazeListView(ref listViewMaintenanceOPRs);

                }

                List<Report_MaintenanceOPRs_Year_ReportDetail> Report_MaintenanceOPRs_Year_ReportDetailList
                           = new ReportMaintenanceOPRsSQL(DB).Get_Report_MaintenanceOPRs_Year_ReportDetail(MaintenanceOPRsAccount_.Year);
                for (int i = 0; i < Report_MaintenanceOPRs_Year_ReportDetailList.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Report_MaintenanceOPRs_Year_ReportDetailList[i].MonthNO .ToString ());
                    item.Name = Report_MaintenanceOPRs_Year_ReportDetailList[i].MonthNO.ToString();
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].MonthName);
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_EndWork_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Repaired_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Warranty_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_EndWarranty_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Pays_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Pays_Remain);

                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_ItemsOut_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);


                    item.UseItemStyleForSubItems = false;
                    if (Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Count > 0)
                    {
                        if (Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Count ==
                        Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_EndWork_Count)
                        {
                            for (int j = 1; j <= 2; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            for (int j = 1; j <= 2; j++)
                                item.SubItems[j].BackColor = Color.Orange;

                        }
                        if (Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Count ==
                         Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Repaired_Count)
                        {
                            item.SubItems[3].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            item.SubItems[3].BackColor = Color.Orange;

                        }
                        if (Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Warranty_Count > 0)
                        {
                            if (Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_Warranty_Count ==
                        Report_MaintenanceOPRs_Year_ReportDetailList[i].MaintenanceOPRs_EndWarranty_Count)
                            {
                                item.SubItems[4].BackColor = Color.LightGreen;
                                item.SubItems[5].BackColor = Color.LightGreen;
                            }
                            else
                            {
                                item.SubItems[4].BackColor = Color.Orange;
                                item.SubItems[5].BackColor = Color.Orange;

                            }
                        }
                        else
                        {
                            item.SubItems[4].BackColor = Color.LightYellow;
                            item.SubItems[5].BackColor = Color.LightYellow;
                        }

                    }
                    else
                        for (int j = 1; j <= 5; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    if (Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_RealValue > 0 ||
                        Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Pays_RealValue > 0)
                    {
                        if (Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency == 0)
                            for (int j = 6; j <= 8; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 6; j <= 8; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                    }
                    else
                    {
                        for (int j = 6; j <= 8; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    }
                    if (Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_RealValue > 0
                        || Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Pays_RealValue > 0)
                    {
                        if (Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_Pays_RealValue >
                           Report_MaintenanceOPRs_Year_ReportDetailList[i].BillMaintenances_RealValue)
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                    }
                    else
                        for (int j = 9; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;


                    listViewMaintenanceOPRs.Items.Add(item);

                }
                #endregion
            }
            else
            {
                //MaintenanceOPRsLabelAccountType.Text = "حساب السنوات";
                //MaintenanceOPRsLabelReport.Text = "تقرير حساب السنوات : " + MaintenanceOPRsAccount_.GetAccountDateString();

                #region YearRangeSection
                if (listViewMaintenanceOPRs.Name != "ListViewMaintenanceOPRs_YearRange")
                {
                    Report_MaintenanceOPRs_YearRange_ReportDetail.IntiliazeListView(ref listViewMaintenanceOPRs);
                }
                List<Report_MaintenanceOPRs_YearRange_ReportDetail> Report_MaintenanceOPRs_YearRange_ReportDetailList
                           = new ReportMaintenanceOPRsSQL(DB).Get_Report_MaintenanceOPRs_YearRange_ReportDetail(MaintenanceOPRsAccount_.YearRange_.min_year, MaintenanceOPRsAccount_.YearRange_.max_year);

                for (int i = 0; i < Report_MaintenanceOPRs_YearRange_ReportDetailList.Count; i++)
                {
                    ListViewItem item = new ListViewItem(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].YearNO.ToString());
                    item.Name = Report_MaintenanceOPRs_YearRange_ReportDetailList[i].YearNO.ToString();
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_EndWork_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Repaired_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Warranty_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_EndWarranty_Count.ToString());
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Pays_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Pays_Remain);

                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_ItemsOut_Value);
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_ItemsOut_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_RealValue + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Pays_RealValue + " " + ReferenceCurrency.CurrencySymbol);


                    item.UseItemStyleForSubItems = false;
                    if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Count > 0)
                    {
                        if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Count ==
                        Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_EndWork_Count)
                        {
                            for (int j = 1; j <= 2; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            for (int j = 1; j <= 2; j++)
                                item.SubItems[j].BackColor = Color.Orange;

                        }
                        if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Count ==
                         Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Repaired_Count)
                        {
                            item.SubItems[3].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            item.SubItems[3].BackColor = Color.Orange;

                        }
                        if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Warranty_Count > 0)
                        {
                            if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_Warranty_Count ==
                        Report_MaintenanceOPRs_YearRange_ReportDetailList[i].MaintenanceOPRs_EndWarranty_Count)
                            {
                                item.SubItems[4].BackColor = Color.LightGreen;
                                item.SubItems[5].BackColor = Color.LightGreen;
                            }
                            else
                            {
                                item.SubItems[4].BackColor = Color.Orange;
                                item.SubItems[5].BackColor = Color.Orange;

                            }
                        }
                        else
                        {
                            item.SubItems[4].BackColor = Color.LightYellow;
                            item.SubItems[5].BackColor = Color.LightYellow;
                        }

                    }
                    else
                        for (int j = 1; j <= 5; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_RealValue > 0 ||
                        Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Pays_RealValue > 0)
                    {
                        if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Pays_Remain_UPON_MaintenanceOPRsCurrency == 0)
                            for (int j = 6; j <= 8; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 6; j <= 8; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                    }
                    else
                    {
                        for (int j = 6; j <= 8; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    }
                    if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_RealValue > 0
                        || Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Pays_RealValue > 0)
                    {
                        if (Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_Pays_RealValue >
                           Report_MaintenanceOPRs_YearRange_ReportDetailList[i].BillMaintenances_RealValue)
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                        else
                            for (int j = 9; j <= 12; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                    }
                    else
                        for (int j = 9; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;


                    listViewMaintenanceOPRs.Items.Add(item);

                }

                #endregion
            }

            MaintenanceOPRs_FillReport();


        }
        private void MaintenanceOPRsBack_Click(object sender, EventArgs e)
        {
            if (MaintenanceOPRsAccount_.Year == -1) return;
            MaintenanceOPRsAccount_.Account_Date_UP();
            Refresh_ListViewMaintenanceOPRs();
        }

        private void MaintenanceOPRsButtonLeftRight_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            bool left;
            if (b.Name == "MaintenanceOPRsButtonLeft") left = true;
            else left = false;

            if (MaintenanceOPRsAccount_.Day != -1)
            {
                if (left)
                {
                    if (MaintenanceOPRsAccount_.Day == DateTime.DaysInMonth(MaintenanceOPRsAccount_.Year, MaintenanceOPRsAccount_.Month))
                    {
                        if (MaintenanceOPRsAccount_.Month == 12)
                        { MaintenanceOPRsAccount_.Year++; MaintenanceOPRsAccount_.Month = 1; MaintenanceOPRsAccount_.Day = 1; }
                        else
                        { MaintenanceOPRsAccount_.Month++; MaintenanceOPRsAccount_.Day = 1; }

                    }
                    else MaintenanceOPRsAccount_.Day++;
                }
                else
                {
                    if (MaintenanceOPRsAccount_.Day == 1)
                    {

                        if (MaintenanceOPRsAccount_.Month == 1)
                        { MaintenanceOPRsAccount_.Year--; MaintenanceOPRsAccount_.Month = 12; }
                        else
                        { MaintenanceOPRsAccount_.Month--; }
                        MaintenanceOPRsAccount_.Day = DateTime.DaysInMonth(MaintenanceOPRsAccount_.Year, MaintenanceOPRsAccount_.Month);
                    }
                    else MaintenanceOPRsAccount_.Day--;
                }

            }
            else if (MaintenanceOPRsAccount_.Month != -1)
            {
                if (left)
                {
                    if (MaintenanceOPRsAccount_.Month == 12)
                    {
                        MaintenanceOPRsAccount_.Year++; MaintenanceOPRsAccount_.Month = 1;
                    }
                    else MaintenanceOPRsAccount_.Month++;
                }
                else
                {
                    if (MaintenanceOPRsAccount_.Month == 1)
                    {
                        MaintenanceOPRsAccount_.Year--; MaintenanceOPRsAccount_.Month = 12;
                    }
                    else MaintenanceOPRsAccount_.Month--;
                }
            }
            else if (MaintenanceOPRsAccount_.Year != -1)
            {
                if (left)
                {
                    MaintenanceOPRsAccount_.Year++;
                    MaintenanceOPRsAccount_.YearRange_.min_year++;
                    MaintenanceOPRsAccount_.YearRange_.max_year++;
                }
                else
                {
                    MaintenanceOPRsAccount_.Year--;
                    MaintenanceOPRsAccount_.YearRange_.min_year--;
                    MaintenanceOPRsAccount_.YearRange_.max_year--;
                }
            }
            else
            {
                if (left)
                {

                    MaintenanceOPRsAccount_.YearRange_.min_year += 10;
                    MaintenanceOPRsAccount_.YearRange_.max_year += 10;
                }
                else
                {
                    MaintenanceOPRsAccount_.YearRange_.min_year -= 10;
                    MaintenanceOPRsAccount_.YearRange_.max_year -= 10;
                }
            }
            Refresh_ListViewMaintenanceOPRs();
        }

        public void ListViewMaintenanceOPRsAccountDown()
        {
            try
            {
                if (MaintenanceOPRsAccount_.Year == -1 || MaintenanceOPRsAccount_.Month == -1 || MaintenanceOPRsAccount_.Day == -1)
                {
                    MaintenanceOPRsAccount_.Account_Date_Down(Convert.ToInt32(listViewMaintenanceOPRs.SelectedItems[0].Name));
                    Refresh_ListViewMaintenanceOPRs();
                }
                else
                {

                    OpenMaintenanceOPR_MenuItem.PerformClick();

                }


            }
            catch (Exception ee)
            {

            }


        }
        private void ListViewMaintenanceOPRs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewMaintenanceOPRs.SelectedItems.Count > 0)
                ListViewMaintenanceOPRsAccountDown();
        }
        private void ListViewMaintenanceOPRs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                ListViewMaintenanceOPRsAccountDown();
        }
        private void ListViewMaintenanceOPRs_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewMaintenanceOPRs.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewMaintenanceOPRs.Items)
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
                        if (MaintenanceOPRsAccount_.Day != -1)
                        {
                            MaintenanceOPR MaintenanceOPR_ =new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID ( Convert.ToUInt32(listitem.Name ));
                            BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(MaintenanceOPR_);

                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] {OpenMaintenanceOPR_MenuItem , EditMaintenanceOPR_MenuItem , DeleteMaintenanceOPR_MenuItem
                            , new MenuItem("-"),CreateMaintenanceOPR_MenuItem });
                            if(BillMaintenance_ !=null )
                                MenuItemList.AddRange(new MenuItem[] { new MenuItem("-"), AddPayIN_BillMaintenance_MenuItem });
                            listViewMaintenanceOPRs.ContextMenu = new ContextMenu(MenuItemList.ToArray());


                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            listViewMaintenanceOPRs.ContextMenu = new ContextMenu(mi1);


                        }


                    }
                    else
                    {
                        if (MaintenanceOPRsAccount_.Day != -1)
                        {
                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"), CreateMaintenanceOPR_MenuItem };
                            listViewMaintenanceOPRs.ContextMenu = new ContextMenu(mi1);
                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            listViewMaintenanceOPRs.ContextMenu = new ContextMenu(mi1);


                        }

                    }

                }
            }
        }

        public async void IntializeListViewMaintenanceOPRsColumnsWidth()
        {

            if (MaintenanceOPRsAccount_.Day != -1)
            {


                listViewMaintenanceOPRs.Columns[0].Width = 75;//time
                listViewMaintenanceOPRs.Columns[1].Width = 60;//id
                listViewMaintenanceOPRs.Columns[2].Width = 100;//owner
                listViewMaintenanceOPRs.Columns[3].Width = 60;//clause count
                listViewMaintenanceOPRs.Columns[4].Width = 125;//amount in
                listViewMaintenanceOPRs.Columns[5].Width = 125;//amount remain
                listViewMaintenanceOPRs.Columns[6].Width = 100;//value
                listViewMaintenanceOPRs.Columns[7].Width = 100;//exchangerate
                listViewMaintenanceOPRs.Columns[8].Width = 100;//paid
                listViewMaintenanceOPRs.Columns[9].Width = 100;//remain
                listViewMaintenanceOPRs.Columns[10].Width = 140;//قيمة الفاتور الفعلية
                listViewMaintenanceOPRs.Columns[11].Width = 150;// المدفوع الفعلي
                listViewMaintenanceOPRs.Columns[12].Width = 140;//قيمة  الخارج
                listViewMaintenanceOPRs.Columns[13].Width = 140;//عائدات الفاتورة
                listViewMaintenanceOPRs.Columns[14].Width = 140;//القيمة العلية للعائدات

            }
            else
            {
                listViewMaintenanceOPRs.Columns[0].Width = 100;//--
                listViewMaintenanceOPRs.Columns[1].Width = 120;//bills count
                listViewMaintenanceOPRs.Columns[2].Width = 115;//clause count
                listViewMaintenanceOPRs.Columns[3].Width = 125;//bill value
                listViewMaintenanceOPRs.Columns[4].Width = 125;//bills pays value
                listViewMaintenanceOPRs.Columns[5].Width = 120;//remain
                listViewMaintenanceOPRs.Columns[6].Width = 140;//item in value
                listViewMaintenanceOPRs.Columns[7].Width = 115;//item in real value
                listViewMaintenanceOPRs.Columns[8].Width = 145;//real value
                listViewMaintenanceOPRs.Columns[9].Width = 115;//profit
                listViewMaintenanceOPRs.Columns[10].Width = 125;//real p
            }
        }
        private void CreateMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB,null);
            MaintenanceOPRForm_.ShowDialog();
            if (MaintenanceOPRForm_.Refresh_ListViewMaintenanceOPRs_Flag ||MaintenanceOPRForm_ .Refresh_ListViewMoneyDataDetails_Flag )
            {
                Refresh_ListViewMaintenanceOPRs();
            }
        }
        private void OpenMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            uint MaintenanceOPRid = Convert.ToUInt32(listViewMaintenanceOPRs.SelectedItems[0].Name);
            MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(MaintenanceOPRid);
            Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, false);
            MaintenanceOPRForm_.ShowDialog();
            if (MaintenanceOPRForm_.Refresh_ListViewMaintenanceOPRs_Flag || MaintenanceOPRForm_.Refresh_ListViewMoneyDataDetails_Flag)
            {
                Refresh_ListViewMaintenanceOPRs();
            }
            MaintenanceOPRForm_.Dispose();
        }
        private void EditMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            uint MaintenanceOPRid = Convert.ToUInt32(listViewMaintenanceOPRs.SelectedItems[0].Name);
            MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(MaintenanceOPRid);
            Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, true );
            MaintenanceOPRForm_.ShowDialog();
            if (MaintenanceOPRForm_.Refresh_ListViewMaintenanceOPRs_Flag || MaintenanceOPRForm_.Refresh_ListViewMoneyDataDetails_Flag)
            {
                Refresh_ListViewMaintenanceOPRs();
            }
            MaintenanceOPRForm_.Dispose();
        }
        private void DeleteMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dd != DialogResult.OK) return;
            uint MaintenanceOPRid = Convert.ToUInt32(listViewMaintenanceOPRs.SelectedItems[0].Name);
            bool success = new MaintenanceOPRSQL(DB).DeleteMaintenanceOPR (MaintenanceOPRid);
            if (success)
            {
                Refresh_ListViewMaintenanceOPRs();
            }

        }
        private void AddPayIN_BillMaintenance_MenuItem_Click(object sender, EventArgs e)
        {

            if (listViewMaintenanceOPRs.SelectedItems.Count == 1)
            {

                uint sid = Convert.ToUInt32(listViewMaintenanceOPRs .SelectedItems[0].Name);

                MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(sid);
                BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(MaintenanceOPR_);
                PayINForm PayINForm_ = new PayINForm(DB, GetSelectedDate(MaintenanceOPRsAccount_), BillMaintenance_);
                PayINForm_.ShowDialog();
                if (PayINForm_.DialogResult == DialogResult.OK)
                {
                    Refresh_ListViewMaintenanceOPRs();
  
                }
            }


          


        }
       
        #endregion
       
        #region General
        private void tabPage1_Resize(object sender, EventArgs e)
        {
           
        }
        private void Refresh_MenuItem_Click(object sender, EventArgs e)
        {
            Refresh_ListViewMaintenanceOPRs();

        }
        //private void RefreshAccount()
        //{
        //    //TextBoxAccountMoney.Text = Account_.GetAccountMoneyOverAll();
        //    //MoneyAccount_ .GetAccountDetails(ref ListViewAccountDataDetails);
        //    //Account_.GetAccountReport(ref AccountListViewReport);
        //}
        #endregion




     

       

      
        private DateTime GetSelectedDate(DateAccount DateAccount_)
        {
            try
            {
                if (DateAccount_.Day != -1)
                    return (new DateTime(DateAccount_.Year, DateAccount_.Month, DateAccount_.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                else if (DateAccount_.Month != -1)
                {
                    
                    return (new DateTime(DateAccount_.Year, DateAccount_.Month, 1));
                }
                else if (DateAccount_.Year != -1)
                {
                    
                    return (new DateTime(DateAccount_.Year, 1, 1));
                }
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

                Refresh_ListViewMaintenanceOPRs();
               
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
                Trade.Forms.Container.User_ShowLocationsForm ShowLocations_ = new Trade.Forms.Container.User_ShowLocationsForm(DB, null , Trade.Forms.Container.User_ShowLocationsForm.SHOW_Data );
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


        private void الصيانةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Maintenance.Forms.MaintenanceOPRForm MaintenanceForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB,null);
                //MaintenanceForm_.Show();
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

   

     
        private void عملياتالصيانةالغيرمنتهيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Maintenance.Forms.MaintenanceOPR_NotFinish_Form MaintenanceOPR_NotFinish_Form_ = new Maintenance.Forms.MaintenanceOPR_NotFinish_Form(DB);
            MaintenanceOPR_NotFinish_Form_.Show();
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
