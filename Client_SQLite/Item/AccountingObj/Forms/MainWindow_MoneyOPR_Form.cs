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
    public partial class MainWindow_MoneyOPR_Form : Form
    {
        DatabaseInterface DB;
     
        DateAccount MoneyAccount_;
       
        MoneyAccountSQL MoneyAccountSQL_;

        System.Windows.Forms.MenuItem Refresh_MenuItem;
       
        System.Windows.Forms.MenuItem AddPayIN_MenuItem;
        System.Windows.Forms.MenuItem AddPayOUT_MenuItem;
        System.Windows.Forms.MenuItem AddMoneyTransFormOPR_MenuItem;
        System.Windows.Forms.MenuItem AddExchangeOPR_MenuItem;
        System.Windows.Forms.MenuItem Open_MoneyOPR_MenuItem;
        System.Windows.Forms.MenuItem Edit_MoneyOPR_MenuItem;
        System.Windows.Forms.MenuItem Delete_MoneyOPR_MenuItem;

        Currency ReferenceCurrency;
        public MainWindow_MoneyOPR_Form(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;

            labelUser.Text = DB.GetUser_EmployeeName(); 
            ReferenceCurrency = new CurrencySQL(DB).GetReferenceCurrency();
            MoneyAccountSQL_ = new MoneyAccountSQL(DB);
            DateAccount  .YearRange yearrange = new DateAccount  .YearRange(DateTime.Today.Year-5, DateTime.Today.Year+5);

            MoneyAccount_ = new DateAccount (DB,yearrange,DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
          Initialize_MenuItems();
           
        }
        public async void Initialize_MenuItems()
        {
            Refresh_MenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);

           
            Open_MoneyOPR_MenuItem = new MenuItem("استعراض", Open_MoneyOPR_MenuItem_Click);
            Edit_MoneyOPR_MenuItem = new MenuItem("تعديل", Edit_MoneyOPR_MenuItem_Click);
            Delete_MoneyOPR_MenuItem = new MenuItem("حذف", Delete_MoneyOPR_MenuItem_Click);
            AddPayOUT_MenuItem = new System.Windows.Forms.MenuItem("انشاء دفعة خارجة من الصندوق", AddPayOUT_MenuItem_Click);
            AddPayIN_MenuItem = new MenuItem("انشاء دفعة واردة الى الصندوق", AddPayIN_MenuItem_Click);
            AddExchangeOPR_MenuItem = new MenuItem("انشاء عملية صرف", AddExchangeOPR_MenuItem_Click);
           AddMoneyTransFormOPR_MenuItem = new MenuItem("انشاء عملية تحويل مال الى صندوق آخر", AddMoneyTransFormOPR_MenuItem_Click);


           
        }
        #region MoneyAccount
        public void AdjustmentDatagridviewColumnsWidth()
        {
            int columnscount = dataGridView1.Columns.Count + 1;
            int w = (dataGridView1.Width ) / columnscount; ;
            dataGridView1.RowHeadersWidth = w-2;
            for (int i = 0; i < columnscount - 1; i++) dataGridView1.Columns[i].Width = w;

        }

        public async void Refresh_ListViewMoneyDataDetails()
        {
            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
            
            AccountLabelAccountDate.Text = MoneyAccount_.GetAccountDateString();
            Refresh_ListViewMoneyDataReport();
            int Stuck_MoneyTransformOpr_IN = new MoneyTransFormOPRSQL(DB).Get_Stuck_MoneyTransformOPR_IN_List(moneybox).Count;
            int Stuck_MoneyTransformOpr_OUT = new MoneyTransFormOPRSQL(DB).Get_Stuck_MoneyTransformOPR_OUT_List(moneybox).Count ;
            
            linkLabel_Stuck_MoneyTransformOpr_IN.Text = "عمليات تحويل المال المعلقة الواردة:" + Stuck_MoneyTransformOpr_IN;
            linkLabel_Stuck_MoneyTransformOpr_OUT.Text = "عمليات تحويل المال المعلقة الصادرة:" + Stuck_MoneyTransformOpr_OUT;
            if (Stuck_MoneyTransformOpr_IN == 0)
                linkLabel_Stuck_MoneyTransformOpr_IN.BackColor = Color.LimeGreen;
            else
                linkLabel_Stuck_MoneyTransformOpr_IN.BackColor = Color.Orange ;
            if (Stuck_MoneyTransformOpr_OUT == 0)
                linkLabel_Stuck_MoneyTransformOpr_OUT.BackColor = Color.LimeGreen;
            else
                linkLabel_Stuck_MoneyTransformOpr_OUT.BackColor = Color.Orange;


            #region PaySection
            double realValue_in = 0, realValue_out = 0;
            if (MoneyAccount_.Day != -1)
            {
              
                AccountLabelAccountType.Text = "حساب اليوم";
                AccountLabelReport.Text = "تقرير حساب اليوم : " + MoneyAccount_.GetAccountDateString();
                #region PayDaySection
                ListViewMoneyDataDetails .Items.Clear();
            
                if (ListViewMoneyDataDetails.Name != "ListViewMoneyDataDetails_Day")
                {
                    AccountOprReportDetail.IntiliazeListView(ref ListViewMoneyDataDetails);

                }
                List<AccountOprReportDetail> accountopr_reportlist
                          = new MoneyAccountSQL(DB).GetAccountOprReport_Details_InDay(moneybox, MoneyAccount_.Year, MoneyAccount_.Month, MoneyAccount_.Day);
                for (int i = 0; i < accountopr_reportlist.Count; i++)
                {

                    string payopridstr="";
                        //= (accountopr_reportlist[i].OprType== AccountOprReportDetail.TYPE_PAY_OPR?"P":"E")
                        //+ (accountopr_reportlist[i].OprDirection== AccountOprReportDetail.DIRECTION_IN?"I":"O")
                        //+ accountopr_reportlist[i].OprID.ToString();
                    string payoprtype = "";
                    string Direction = "";

                    if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_PAY_OPR)
                    {
                        payopridstr += "P";
                        payoprtype = "عملية دفع";
                    }
                    else if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_EXCHANGE_OPR)
                    {
                        payopridstr += "E";
                        payoprtype = "عملية صرف";

                    }
                    else if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_MoneyTransform_OPR)
                    {
                        payopridstr += "T";
                        payoprtype = "عملية تحويل مال";

                    }

                    if (accountopr_reportlist[i].OprDirection == AccountOprReportDetail.DIRECTION_IN)
                    {
                        payopridstr += "I";
                        Direction = "داخل الى الصندوق";
                        realValue_in += accountopr_reportlist[i].RealValue;
                    }
                    else
                    {
                        payopridstr += "O";
                        Direction = "خارج من الصندوق";
                        realValue_out += accountopr_reportlist[i].RealValue;

                    }
                    payopridstr += accountopr_reportlist[i].OprID.ToString();
                    ListViewItem item = new ListViewItem(accountopr_reportlist[i].OprTime.ToShortTimeString());
                    item.Name = payopridstr;
                    item.SubItems.Add(payoprtype);
                    item.SubItems.Add(Direction);
                    item.SubItems.Add(accountopr_reportlist[i].OprID.ToString());
                  
                    item.SubItems.Add(accountopr_reportlist[i].Value.ToString()+" "
                        + accountopr_reportlist[i].CurrencySymbol );
                    item.SubItems.Add(accountopr_reportlist[i].ExchangeRate .ToString());
                    item.SubItems.Add(accountopr_reportlist[i].RealValue .ToString()+" "+ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(accountopr_reportlist[i].OprOwner);
                    item.UseItemStyleForSubItems = false;
                    Color color;
                    if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_EXCHANGE_OPR) color = Color.LightGoldenrodYellow;
                    else if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_PAY_OPR &&
                        accountopr_reportlist[i].OprDirection == AccountOprReportDetail.DIRECTION_OUT)
                        color = Color.Orange;
                    else if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_PAY_OPR &&
                        accountopr_reportlist[i].OprDirection == AccountOprReportDetail.DIRECTION_IN)
                        color = Color.LightGreen;
                    else if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_MoneyTransform_OPR &&
                        accountopr_reportlist[i].OprDirection == AccountOprReportDetail.DIRECTION_OUT)
                        color = Color.LightPink;
                    else if (accountopr_reportlist[i].OprType == AccountOprReportDetail.TYPE_MoneyTransform_OPR &&
                        accountopr_reportlist[i].OprDirection == AccountOprReportDetail.DIRECTION_IN)
                        color = Color.LimeGreen ;
                    else color = Color.LightGray;
                    //if (oprtypeDirectionColor == 0 && oprtypeColor == 0) color = Color.YellowGreen;
                    //else if (oprtypeDirectionColor == 1 && oprtypeColor == 0) color = Color.DarkOrange;
                    //else if (oprtypeDirectionColor == 0 && oprtypeColor == 1) color = Color.LightGreen;
                    //else color = Color.Orange;
                    //item.UseItemStyleForSubItems = false;
                    //item.SubItems[0].BackColor = color;
                    //item.SubItems[1].BackColor = color;
                    //item.SubItems[2].BackColor = color;
                    //item.SubItems[3].BackColor = color;
                    //item.SubItems[4].BackColor = color;
                    //item.SubItems[5].BackColor = color;
                    //item.SubItems[6].BackColor = color;
                    //item.SubItems[7].BackColor = color;
                    item.UseItemStyleForSubItems = true ;
                    item.BackColor = color;
  
                    ListViewMoneyDataDetails.Items.Add(item);

                }
              
                #endregion
            }
            else if (MoneyAccount_.Month != -1)
            {
                AccountLabelAccountType.Text = "حساب الشهر";
                AccountLabelReport.Text = "تقرير حساب الشهر : " + MoneyAccount_.GetAccountDateString();

                #region PayMonthSection
                ListViewMoneyDataDetails.Items.Clear();
                if (ListViewMoneyDataDetails.Name != "ListViewMoneyDataDetails_Month")
                {
                    AccountOprDayReportDetail.IntiliazeListView(ref ListViewMoneyDataDetails);
                }
                List<AccountOprDayReportDetail> accountoprdayeportlist
                                    = new MoneyAccountSQL(DB).GetAccountOprReport_Details_InMonth(moneybox, MoneyAccount_.Year, MoneyAccount_.Month);
                for (int i = 0; i < accountoprdayeportlist.Count; i++)
                {
                    ListViewItem item = new ListViewItem(accountoprdayeportlist[i].Date_day.ToShortDateString());
                    item.Name = accountoprdayeportlist[i].DateDayNo.ToString();
                    item.SubItems.Add(accountoprdayeportlist[i].PaysIN_Count.ToString());
                    item.SubItems.Add(accountoprdayeportlist[i].PaysOUT_Count.ToString());
                    item.SubItems.Add(accountoprdayeportlist[i].Exchange_Count.ToString());
                    item.SubItems.Add(accountoprdayeportlist[i].MoneyTransform_IN_Count.ToString());
                    item.SubItems.Add(accountoprdayeportlist[i].MoneyTransform_OUT_Count.ToString());
                    item.SubItems.Add(accountoprdayeportlist[i].PaysIN_Value);
                    item.SubItems.Add(accountoprdayeportlist[i].PaysOUT_Value);
                    item.SubItems.Add(accountoprdayeportlist[i].PaysIN_Real_Value+" "+ReferenceCurrency.CurrencySymbol );
                    item.SubItems.Add(accountoprdayeportlist[i].PaysOUT_Real_Value + " " + ReferenceCurrency.CurrencySymbol);
                    realValue_in += accountoprdayeportlist[i].PaysIN_Real_Value;
                    realValue_out += accountoprdayeportlist[i].PaysOUT_Real_Value;
                    double clear_real_value = accountoprdayeportlist[i].PaysIN_Real_Value -
                        accountoprdayeportlist[i].PaysOUT_Real_Value;
                    item.SubItems.Add(clear_real_value + " " + ReferenceCurrency.CurrencySymbol);
                    //item.UseItemStyleForSubItems = false;
                    //item.SubItems[0].BackColor = Color.LightGray;
                    //item.SubItems[1].BackColor = Color.LightGreen;
                    //item.SubItems[2].BackColor = Color.Orange;
                    //item.SubItems[3].BackColor = Color.Yellow;
                    //item.SubItems[4].BackColor = Color.LightGreen;
                    //item.SubItems[5].BackColor = Color.Orange;
                    //item.SubItems[6].BackColor = Color.LightGreen;
                    //item.SubItems[7].BackColor = Color.Orange;
                    if (clear_real_value > 0)
                        item.BackColor = Color.LightGreen;
                    else if (clear_real_value < 0)
                        item.BackColor = Color.Orange;
                    else
                        item.BackColor = Color.LightYellow;
                    ListViewMoneyDataDetails.Items.Add(item);

                }
                #endregion
            }
            else if (MoneyAccount_.Year != -1)
            {
                AccountLabelAccountType.Text = "حساب السنة";
                AccountLabelReport.Text = "تقرير حساب السنة : " + MoneyAccount_.GetAccountDateString();

                #region PayYearSection
                ListViewMoneyDataDetails.Items.Clear();
                if (ListViewMoneyDataDetails.Name != "ListViewMoneyDataDetails_Year")
                {
                    AccountOprMonthReportDetail.IntiliazeListView(ref ListViewMoneyDataDetails);
                }
                List<AccountOprMonthReportDetail> accountoprmonthreportlist
                       = new MoneyAccountSQL(DB).GetAccountOprReport_Details_InYear(moneybox, MoneyAccount_.Year);
                for (int i = 0; i < accountoprmonthreportlist.Count; i++)
                {
                    ListViewItem item = new ListViewItem(accountoprmonthreportlist[i].Year_Month.ToString());
                    item.Name = accountoprmonthreportlist[i].Year_Month.ToString();
                    item.SubItems.Add(accountoprmonthreportlist[i].Year_Month_Name);
                    item.SubItems.Add(accountoprmonthreportlist[i].PaysIN_Count.ToString());
                    item.SubItems.Add(accountoprmonthreportlist[i].PaysOUT_Count.ToString());
                    item.SubItems.Add(accountoprmonthreportlist[i].Exchange_Count.ToString());
                    item.SubItems.Add(accountoprmonthreportlist[i].MoneyTransform_IN_Count.ToString());
                    item.SubItems.Add(accountoprmonthreportlist[i].MoneyTransform_OUT_Count.ToString());
                    item.SubItems.Add(accountoprmonthreportlist[i].PaysIN_Value);
                    item.SubItems.Add(accountoprmonthreportlist[i].PaysOUT_Value);
                    item.SubItems.Add(accountoprmonthreportlist[i].PaysIN_Real_Value + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(accountoprmonthreportlist[i].PaysOUT_Real_Value + " " + ReferenceCurrency.CurrencySymbol);
                    realValue_in += accountoprmonthreportlist[i].PaysIN_Real_Value;
                    realValue_out += accountoprmonthreportlist[i].PaysOUT_Real_Value;
                    double clear_real_value = accountoprmonthreportlist[i].PaysIN_Real_Value -
                        accountoprmonthreportlist[i].PaysOUT_Real_Value;
                    item.SubItems.Add(clear_real_value + " " + ReferenceCurrency.CurrencySymbol);
                    //item.UseItemStyleForSubItems = false;
                    //item.SubItems[0].BackColor = Color.LightGray;
                    //item.SubItems[1].BackColor = Color.LightGreen;
                    //item.SubItems[2].BackColor = Color.Orange;
                    //item.SubItems[3].BackColor = Color.Yellow;
                    //item.SubItems[4].BackColor = Color.LightGreen;
                    //item.SubItems[5].BackColor = Color.Orange;
                    //item.SubItems[6].BackColor = Color.LightGreen;
                    //item.SubItems[7].BackColor = Color.Orange;
                    if (clear_real_value > 0)
                        item.BackColor = Color.LightGreen;
                    else if (clear_real_value < 0)
                        item.BackColor = Color.Orange;
                    else
                        item.BackColor = Color.LightYellow;
                    ListViewMoneyDataDetails.Items.Add(item);

                }
                #endregion
            }
            else
            {
                AccountLabelAccountType.Text = "حساب السنوات";
                AccountLabelReport.Text = "تقرير حساب السنوات : " + MoneyAccount_.GetAccountDateString();

                #region PayYearRangeSection
                ListViewMoneyDataDetails.Items.Clear();
                if (ListViewMoneyDataDetails.Name != "ListViewMoneyDataDetails_YearRange")
                {
                    AccountOprYearReportDetail.IntiliazeListView(ref ListViewMoneyDataDetails);
                }
                List<AccountOprYearReportDetail> accountopryearreportlist
                        = new MoneyAccountSQL(DB).GetAccountOprReport_Details_InYearRange(moneybox, MoneyAccount_.YearRange_.min_year, MoneyAccount_.YearRange_.max_year);
                for (int i = 0; i < accountopryearreportlist.Count; i++)
                {
                    ListViewItem item = new ListViewItem(accountopryearreportlist[i].AccountYear.ToString());
                    item.Name = accountopryearreportlist[i].AccountYear.ToString();
                    item.SubItems.Add(accountopryearreportlist[i].PaysIN_Count.ToString());
                    item.SubItems.Add(accountopryearreportlist[i].PaysOUT_Count.ToString());
                    item.SubItems.Add(accountopryearreportlist[i].Exchange_Count.ToString());
                    item.SubItems.Add(accountopryearreportlist[i].MoneyTransform_IN_Count.ToString());
                    item.SubItems.Add(accountopryearreportlist[i].MoneyTransform_OUT_Count.ToString());
                    item.SubItems.Add(accountopryearreportlist[i].PaysIN_Value);
                    item.SubItems.Add(accountopryearreportlist[i].PaysOUT_Value);
                    item.SubItems.Add(accountopryearreportlist[i].PaysIN_Real_Value + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(accountopryearreportlist[i].PaysOUT_Real_Value + " " + ReferenceCurrency.CurrencySymbol);
                    realValue_in += accountopryearreportlist[i].PaysIN_Real_Value;
                    realValue_out += accountopryearreportlist[i].PaysOUT_Real_Value;
                    double clear_real_value = accountopryearreportlist[i].PaysIN_Real_Value -
                         accountopryearreportlist[i].PaysOUT_Real_Value;
                    item.SubItems.Add(clear_real_value + " " + ReferenceCurrency.CurrencySymbol);
                    //item.UseItemStyleForSubItems = false;
                    //item.SubItems[0].BackColor = Color.LightGray;
                    //item.SubItems[1].BackColor = Color.LightGreen;
                    //item.SubItems[2].BackColor = Color.Orange;
                    //item.SubItems[3].BackColor = Color.Yellow;
                    //item.SubItems[4].BackColor = Color.LightGreen;
                    //item.SubItems[5].BackColor = Color.Orange;
                    //item.SubItems[6].BackColor = Color.LightGreen;
                    //item.SubItems[7].BackColor = Color.Orange;
                    if (clear_real_value > 0)
                        item.BackColor = Color.LightGreen;
                    else if (clear_real_value < 0)
                        item.BackColor = Color.Orange;
                    else
                        item.BackColor = Color.LightYellow;
                    ListViewMoneyDataDetails.Items.Add(item);

                }
                #endregion
            }
            #endregion
            textBox_Real_In_Money.Text = realValue_in.ToString() + ReferenceCurrency.CurrencySymbol;
            textBox_Real_out_Money.Text = realValue_out.ToString() + ReferenceCurrency.CurrencySymbol;
            textBox_Real_Clear_value.Text = (realValue_in - realValue_out).ToString() + ReferenceCurrency.CurrencySymbol;
            if ((realValue_in - realValue_out) < 0)
                textBox_Real_Clear_value.BackColor = Color.Orange;
            else
                textBox_Real_Clear_value.BackColor = Color.LimeGreen;
        }
        public void Refresh_ListViewMoneyDataReport()
        {
            dataGridView1.Rows.Clear();
            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
            List<PayCurrencyReport> PayCurrencyReportList = new List<PayCurrencyReport>();
            if (MoneyAccount_.Day != -1)
            {
                PayCurrencyReportList = MoneyAccountSQL_.GetPayReport_InDay(moneybox, MoneyAccount_.Year, MoneyAccount_.Month, MoneyAccount_.Day);
            }
            else if (this.MoneyAccount_.Month  != -1)
            {
                PayCurrencyReportList = MoneyAccountSQL_.GetPayReport_InMonth(moneybox, MoneyAccount_.Year, MoneyAccount_.Month);
            }
            else if (this.MoneyAccount_.Year  != -1)
            {
                PayCurrencyReportList = MoneyAccountSQL_.GetPayReport_INYear(moneybox, MoneyAccount_.Year);
            }
            else
            {
                PayCurrencyReportList
                    = MoneyAccountSQL_.GetPayReport_betweenTwoYears(moneybox, MoneyAccount_.YearRange_.min_year, MoneyAccount_.YearRange_.max_year);
            }
            string In_money="",Out_Money="";

            for (int i = 0; i < PayCurrencyReportList.Count; i++)
            {
                dataGridView1.Rows.Add();
                double in_all = PayCurrencyReportList[i].PaysIN_Sell
                    + PayCurrencyReportList[i].PaysIN_Maintenance
                    + PayCurrencyReportList[i].PaysIN_NON
                    + PayCurrencyReportList[i].PaysIN_Exchange
                    + PayCurrencyReportList[i].PaysIN_MoneyTransform;
                double out_all = PayCurrencyReportList[i].PaysOUT_Buy
               + PayCurrencyReportList[i].PaysOUT_Emp
               + PayCurrencyReportList[i].PaysOUT_NON
               + PayCurrencyReportList[i].PaysOUT_Exchange
               + PayCurrencyReportList[i].PaysOUT_MoneyTransform;
                double clear_value = in_all - out_all;
                dataGridView1.Rows[i].HeaderCell.Value = PayCurrencyReportList[i] .CurrencyName;

                dataGridView1.Rows[i].Cells[0].Value = PayCurrencyReportList[i]. PaysIN_Sell.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[1].Value = PayCurrencyReportList[i].PaysIN_Maintenance.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[2].Value = PayCurrencyReportList[i].PaysIN_NON.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[3].Value = PayCurrencyReportList[i].PaysIN_Exchange.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[4].Value = PayCurrencyReportList[i].PaysIN_MoneyTransform.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;

                dataGridView1.Rows[i].Cells[5].Value =in_all  + " " + PayCurrencyReportList[i].CurrencySymbol;

                dataGridView1.Rows[i].Cells[6].Value = PayCurrencyReportList[i].PaysOUT_Buy.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[7].Value = PayCurrencyReportList[i].PaysOUT_Emp.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[8].Value = PayCurrencyReportList[i].PaysOUT_NON.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[9].Value = PayCurrencyReportList[i].PaysOUT_Exchange.ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[10].Value = PayCurrencyReportList[i].PaysOUT_MoneyTransform .ToString() + " " + PayCurrencyReportList[i].CurrencySymbol;

                dataGridView1.Rows[i].Cells[11].Value = out_all + " " + PayCurrencyReportList[i].CurrencySymbol;
                dataGridView1.Rows[i].Cells[12].Value = clear_value + " " + PayCurrencyReportList[i].CurrencySymbol;

                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[i].Cells[6].Style.BackColor = Color.Orange;
                dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.Orange;
                dataGridView1.Rows[i].Cells[8].Style.BackColor = Color.Orange;
                dataGridView1.Rows[i].Cells[9].Style.BackColor = Color.Orange;
                dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.Orange;
                dataGridView1.Rows[i].Cells[11].Style.BackColor = Color.Orange;
                if(clear_value >0)
                    dataGridView1.Rows[i].Cells[12].Style.BackColor = Color.LightGreen;
                else
                    dataGridView1.Rows[i].Cells[12].Style.BackColor = Color.Orange ;
                if(in_all >0)
                In_money += in_all + PayCurrencyReportList[i].CurrencySymbol+" ";
                if (out_all > 0)
                    Out_Money += out_all +  PayCurrencyReportList[i].CurrencySymbol;
                if (i != PayCurrencyReportList.Count - 1)
                {
                    if (out_all > 0)
                        Out_Money += " , ";
                    if (in_all > 0)
                        In_money += " , ";
                }
            }
            if (In_money.Length < 1)
                In_money = "-";
            if (Out_Money.Length < 1)
                Out_Money = "-";
            textBox_In_Money.Text = In_money.ToString();
            textBox_out_Money.Text = Out_Money.ToString();
        }
       
        private void AccountBack_Click(object sender, EventArgs e)
        {
            if (MoneyAccount_.Year == -1) return;
            MoneyAccount_.Account_Date_UP();
            Refresh_ListViewMoneyDataDetails();
        }

        private void AccountButtonLeftRight_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            bool left;
            if (b.Name == "AccountButtonLeft") left = true;
            else left = false;

            if (MoneyAccount_.Day != -1)
            {
                if (left)
                {
                    if (MoneyAccount_.Day == DateTime.DaysInMonth(MoneyAccount_.Year, MoneyAccount_.Month))
                    {
                        if (MoneyAccount_.Month == 12)
                        { MoneyAccount_.Year++; MoneyAccount_.Month = 1; MoneyAccount_.Day = 1; }
                        else
                        { MoneyAccount_.Month++; MoneyAccount_.Day = 1; }

                    }
                    else MoneyAccount_.Day++;
                }
                else
                {
                    if (MoneyAccount_.Day == 1)
                    {

                        if (MoneyAccount_.Month == 1)
                        { MoneyAccount_.Year--; MoneyAccount_.Month = 12; }
                        else
                        { MoneyAccount_.Month--; }
                        MoneyAccount_.Day = DateTime.DaysInMonth(MoneyAccount_.Year, MoneyAccount_.Month);
                    }
                    else MoneyAccount_.Day--;
                }

            }
            else if (MoneyAccount_.Month != -1)
            {
                if (left)
                {
                    if (MoneyAccount_.Month == 12)
                    {
                        MoneyAccount_.Year++; MoneyAccount_.Month = 1;
                    }
                    else MoneyAccount_.Month++;
                }
                else
                {
                    if (MoneyAccount_.Month == 1)
                    {
                        MoneyAccount_.Year--; MoneyAccount_.Month = 12;
                    }
                    else MoneyAccount_.Month--;
                }
            }
            else if (MoneyAccount_.Year != -1)
            {
                if (left)
                {
                    MoneyAccount_.Year++;
                    MoneyAccount_.YearRange_.min_year++;
                    MoneyAccount_.YearRange_.max_year++;
                }
                else
                {
                    MoneyAccount_.Year--;
                    MoneyAccount_.YearRange_.min_year--;
                    MoneyAccount_.YearRange_.max_year--;
                }
            }
            else
            {
                if (left)
                {

                    MoneyAccount_.YearRange_.min_year += 10;
                    MoneyAccount_.YearRange_.max_year += 10;
                }
                else
                {
                    MoneyAccount_.YearRange_.min_year -= 10;
                    MoneyAccount_.YearRange_.max_year -= 10;
                }
            }
            Refresh_ListViewMoneyDataDetails();
        }

        public void ListViewMoneyDataDetailsAccountDown()
        {
            try
            {
                if (MoneyAccount_.Year == -1 || MoneyAccount_.Month == -1 || MoneyAccount_.Day == -1)
                {
                    MoneyAccount_.Account_Date_Down(Convert .ToInt32( ListViewMoneyDataDetails.SelectedItems[0].Name));
                    Refresh_ListViewMoneyDataDetails();
                }
                else
                {

                    Open_MoneyOPR_MenuItem.PerformClick();
                        //string s = ListViewMoneyDataDetails.SelectedItems[0].Name;
                        //if (s.Substring(0,1) == "P")
                        //{
                        //    if (s.Substring(3, 3) == "I")
                        //    {
                        //        uint payinid = Convert.ToUInt32(s.Substring(6));
                        //        PayIN PayIN_ = new PayINSQL(DB).GetPayIN_INFO_BYID(payinid);
                        //        PayINForm PayINForm_ = new PayINForm(DB, PayIN_, false);
                        //        PayINForm_.ShowDialog();
                        //        if (PayINForm_.Changed)
                        //        {
                        //            RefreshAccount();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        uint payoutid = Convert.ToUInt32(s.Substring(6));
                        //        PayOUT PayOUT_ = new PayOUTSQL(DB).GetPayOUT_INFO_BYID(payoutid);
                        //        PayOUTForm PayOUTForm_ = new PayOUTForm(DB, PayOUT_, false);
                        //        PayOUTForm_.ShowDialog();
                        //        if (PayOUTForm_.Changed)
                        //        {
                        //            RefreshAccount();
                        //        }
                        //    }

                        //}
                        //else
                        //{
                        //    uint exchangeoprid = Convert.ToUInt32(s.Substring(6));
                        //    ExchangeOPR ExchangeOPR_ = new ExchangeOPRSQL(DB).GetExchangeOPR_INFO_BYID(exchangeoprid);
                        //    ExchangeOPRForm ExchangeOPRForm_ = new ExchangeOPRForm(DB, ExchangeOPR_, false);
                        //    ExchangeOPRForm_.ShowDialog();
                        //    if (ExchangeOPRForm_.Changed)
                        //    {
                        //        RefreshAccount();
                        //    }
                        //    ExchangeOPRForm_.Dispose();

                        //}
                    }

             
            }
            catch (Exception ee)
            {

            }


        }
        private void ListViewMoneyDataDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListViewMoneyDataDetails.SelectedItems.Count > 0)
                ListViewMoneyDataDetailsAccountDown();
        }
        private void ListViewMoneyDataDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                ListViewMoneyDataDetailsAccountDown();
        }
        private void ListViewMoneyDataDetails_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListViewMoneyDataDetails.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in ListViewMoneyDataDetails.Items)
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
                        if (MoneyAccount_ .Day != -1)
                        {

                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] {Open_MoneyOPR_MenuItem, Edit_MoneyOPR_MenuItem, Delete_MoneyOPR_MenuItem });
                            MenuItemList.Add(new MenuItem("-"));
                             MenuItemList.AddRange(new MenuItem[] { AddPayIN_MenuItem, AddPayOUT_MenuItem, new MenuItem("-"), AddExchangeOPR_MenuItem
                              ,new MenuItem("-"), AddMoneyTransFormOPR_MenuItem });
                            ListViewMoneyDataDetails.ContextMenu = new ContextMenu(MenuItemList.ToArray());


                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            ListViewMoneyDataDetails.ContextMenu = new ContextMenu(mi1);


                        }


                    }
                    else
                    {
                        if (MoneyAccount_.Day != -1)
                        {
                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"),
                                AddPayIN_MenuItem, AddPayOUT_MenuItem, new MenuItem("-"), AddExchangeOPR_MenuItem
                             ,new MenuItem("-"), AddMoneyTransFormOPR_MenuItem };
                            ListViewMoneyDataDetails.ContextMenu = new ContextMenu(mi1);
                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem };
                            ListViewMoneyDataDetails.ContextMenu = new ContextMenu(mi1);


                        }

                    }

                }
            }
        }
  
        public async  void IntializeListViewMoneyDataDetailsColumnsWidth()
        {

            if (MoneyAccount_.Day != -1)
            {
  
                
                ListViewMoneyDataDetails.Columns[0].Width = 100;//
                ListViewMoneyDataDetails.Columns[1].Width = 100;
                ListViewMoneyDataDetails.Columns[2].Width = 150;
                ListViewMoneyDataDetails.Columns[3].Width = 100;
                ListViewMoneyDataDetails.Columns[4].Width = 200;

                ListViewMoneyDataDetails.Columns[5].Width = 150;
                ListViewMoneyDataDetails.Columns[6].Width = 200;
                if (ListViewMoneyDataDetails.Width>1010)
                    ListViewMoneyDataDetails.Columns[7].Width = ListViewMoneyDataDetails.Width - 1005;
            }
            else
            {
                ListViewMoneyDataDetails.Columns[0].Width = 100;
                ListViewMoneyDataDetails.Columns[1].Width = 150;
                ListViewMoneyDataDetails.Columns[2].Width = 150;
                ListViewMoneyDataDetails.Columns[3].Width = 150;
                ListViewMoneyDataDetails.Columns[4].Width = (ListViewMoneyDataDetails.Width - 1005) / 2;
                ListViewMoneyDataDetails.Columns[5].Width = (ListViewMoneyDataDetails.Width - 1005) / 2;
                ListViewMoneyDataDetails.Columns[6].Width = 150;
                ListViewMoneyDataDetails.Columns[7].Width = 150;
                ListViewMoneyDataDetails.Columns[8].Width = 150;
            }
        }
       
        public async  void  IntializeListAccountListViewReport_ColumnsWidth()
        {
            //ListViewMoneyDataReport.Columns[0].Width = 100;
            //int w= (ListViewMoneyDataReport.Width - 101) / 7; ;
            //ListViewMoneyDataReport.Columns[1].Width = w;
            //ListViewMoneyDataReport.Columns[2].Width = w;
            //ListViewMoneyDataReport.Columns[3].Width = w;
            //ListViewMoneyDataReport.Columns[4].Width = w;
            //ListViewMoneyDataReport.Columns[5].Width = w;
            //ListViewMoneyDataReport.Columns[6].Width = w;
            //ListViewMoneyDataReport.Columns[7].Width = w;
        }
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                OverLoad_Form form = (OverLoad_Form)sender;
                if ( form.Refresh_ListViewMoneyDataDetails_Flag)
                {
                    ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                    MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                    Refresh_ListViewMoneyDataDetails();
                    TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);

                }



            }
            catch (Exception ee)
            {
                MessageBox.Show("Form_Closed:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AddExchangeOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                ExchangeOPRForm ExchangeOPRForm_ = new ExchangeOPRForm(DB, GetSelectedDate(MoneyAccount_), moneybox);
                ExchangeOPRForm_.FormClosed += Form_Closed;
                ExchangeOPRForm_.Show ();
                


            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AddExchangeOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }
        private void AddMoneyTransFormOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                MoneyTransFormOPRForm MoneyTransFormOPRForm_ = new MoneyTransFormOPRForm(DB, GetSelectedDate(MoneyAccount_), moneybox);
                MoneyTransFormOPRForm_.FormClosed += Form_Closed;
                MoneyTransFormOPRForm_.Show ();
                
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AddMoneyTransFormOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }
        private void AddPayIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                PayINForm PayINForm_ = new PayINForm(DB, MoneyAccount_.GetDate(), moneybox);
                PayINForm_.ShowDialog();
                if (PayINForm_.DialogResult == DialogResult.OK)
                {
                    Refresh_ListViewMoneyDataDetails();
                }


                TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);

            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AddPayIN_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
      

        }

        private void AddPayOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                PayOUTForm PayOUTForm_ = new PayOUTForm(DB, MoneyAccount_.GetDate(), moneybox);
                PayOUTForm_.ShowDialog();
                if (PayOUTForm_.DialogResult == DialogResult.OK)
                {
                    Refresh_ListViewMoneyDataDetails();
                    TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);
                }
            }
            catch (Exception ee)
            {
                 System.Windows.Forms.MessageBox.Show("AddPayOUT_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
 


          

        }

        private void Delete_MoneyOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                string s = ListViewMoneyDataDetails.SelectedItems[0].Name;
                if (s.Substring(0, 1) == "P")
                {
                    if (s.Substring(1, 1) == "I")
                    {
                        uint payinid = Convert.ToUInt32(s.Substring(2));
                        bool success = new PayINSQL(DB).Delete_PayIN(payinid);
                        if (success)
                        {
                            Refresh_ListViewMoneyDataDetails();
                            TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);
                        }
                    }
                    else
                    {
                        uint payoutid = Convert.ToUInt32(s.Substring(2));
                        bool success = new PayOUTSQL(DB).Delete_PayOUT(payoutid);
                        if (success)
                        {
                            Refresh_ListViewMoneyDataDetails();
                            TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);
                        }
                    }


                }
                else if (s.Substring(0, 1) == "E")
                {

                    uint exchangeoprid = Convert.ToUInt32(s.Substring(2));
                    bool success = new ExchangeOPRSQL(DB).Delete_ExchageOPR(exchangeoprid);
                    if (success)
                    {
                        Refresh_ListViewMoneyDataDetails();
                        TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);
                    }

                }
                else if (s.Substring(0, 1) == "T")
                {

                    uint moneytransformoprid = Convert.ToUInt32(s.Substring(2));
                    bool success = new MoneyTransFormOPRSQL(DB).Delete_MoneyTransFormOPR(moneytransformoprid);
                    if (success)
                    {
                        Refresh_ListViewMoneyDataDetails();
                        TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);
                    }

                }
            }
            catch (Exception ee)
            {
                 System.Windows.Forms.MessageBox.Show("Delete_MoneyOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }



        }

        private void Edit_MoneyOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                string s = ListViewMoneyDataDetails.SelectedItems[0].Name;
                if (s.Substring(0, 1) == "P")
                {
                    if (s.Substring(1, 1) == "I")
                    {
                        uint payinid = Convert.ToUInt32(s.Substring(2));
                        PayIN PayIN_ = new PayINSQL(DB).GetPayIN_INFO_BYID(payinid);
                        PayINForm PayINForm_ = new PayINForm(DB, PayIN_, true);
                        PayINForm_.FormClosed += Form_Closed;
                        PayINForm_.Show ();
                        
                    }
                    else
                    {
                        uint payoutid = Convert.ToUInt32(s.Substring(2));
                        PayOUT PayOUT_ = new PayOUTSQL(DB).GetPayOUT_INFO_BYID(payoutid);
                        PayOUTForm PayOUTForm_ = new PayOUTForm(DB, PayOUT_, true);
                        PayOUTForm_.FormClosed += Form_Closed;
                        PayOUTForm_.Show();
                    }


                }
                else if (s.Substring(0, 1) == "E")
                {
                    uint exchangeoprid = Convert.ToUInt32(s.Substring(2));
                    ExchangeOPR ExchangeOPR_ = new ExchangeOPRSQL(DB).GetExchangeOPR_INFO_BYID(exchangeoprid);
                    ExchangeOPRForm ExchangeOPRForm_ = new ExchangeOPRForm(DB, ExchangeOPR_, true);
                    ExchangeOPRForm_.FormClosed  += Form_Closed;
                    ExchangeOPRForm_.Show ();
                  
                }
                else if (s.Substring(0, 1) == "T")
                {
                    uint moneytransformoprid = Convert.ToUInt32(s.Substring(2));
                    MoneyTransFormOPR MoneyTransFormOPR_ = new MoneyTransFormOPRSQL(DB).GetMoneyTransFormOPR_INFO_BYID(moneytransformoprid);
                    MoneyTransFormOPRForm MoneyTransFormOPRForm_ = new MoneyTransFormOPRForm(DB, MoneyTransFormOPR_, true);
                    MoneyTransFormOPRForm_.FormClosed += Form_Closed;
                    MoneyTransFormOPRForm_.Show();
                }
            }
            catch (Exception ee)
            {
               System.Windows.Forms.MessageBox.Show("Edit_MoneyOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }


        }
        private void Open_MoneyOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                string s = ListViewMoneyDataDetails.SelectedItems[0].Name;
                if (s.Substring(0, 1) == "P")
                {
                    if (s.Substring(1, 1) == "I")
                    {
                        uint payinid = Convert.ToUInt32(s.Substring(2));
                        PayIN PayIN_ = new PayINSQL(DB).GetPayIN_INFO_BYID(payinid);
                        PayINForm PayINForm_ = new PayINForm(DB, PayIN_, false);
                        PayINForm_.FormClosed += Form_Closed;
                        PayINForm_.Show();

                    }
                    else
                    {
                        uint payoutid = Convert.ToUInt32(s.Substring(2));
                        PayOUT PayOUT_ = new PayOUTSQL(DB).GetPayOUT_INFO_BYID(payoutid);
                        PayOUTForm PayOUTForm_ = new PayOUTForm(DB, PayOUT_, false);
                        PayOUTForm_.FormClosed += Form_Closed;
                        PayOUTForm_.Show();

                    }


                }
                else if (s.Substring(0, 1) == "E")
                {
                    uint exchangeoprid = Convert.ToUInt32(s.Substring(2));
                    ExchangeOPR ExchangeOPR_ = new ExchangeOPRSQL(DB).GetExchangeOPR_INFO_BYID(exchangeoprid);
                    ExchangeOPRForm ExchangeOPRForm_ = new ExchangeOPRForm(DB, ExchangeOPR_, false);
                    ExchangeOPRForm_.FormClosed += Form_Closed;
                    ExchangeOPRForm_.Show();

                }
                else if (s.Substring(0, 1) == "T")
                {
                    uint moneytransformoprid = Convert.ToUInt32(s.Substring(2));
                    MoneyTransFormOPR MoneyTransFormOPR_ = new MoneyTransFormOPRSQL(DB).GetMoneyTransFormOPR_INFO_BYID(moneytransformoprid);
                    MoneyTransFormOPRForm MoneyTransFormOPRForm_ = new MoneyTransFormOPRForm(DB, MoneyTransFormOPR_, false);
                    MoneyTransFormOPRForm_.FormClosed += Form_Closed;
                    MoneyTransFormOPRForm_.Show();
                }
            }
            catch (Exception ee)
            {
                 System.Windows.Forms.MessageBox.Show("Open_MoneyOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
      

        }
        #endregion

      
        #region General
        private void tabPage1_Resize(object sender, EventArgs e)
        {
            AdjustmentDatagridviewColumnsWidth();
            IntializeListViewMoneyDataDetailsColumnsWidth();
     
            IntializeListAccountListViewReport_ColumnsWidth();
 
        }
        private void Refresh_MenuItem_Click(object sender, EventArgs e)
        {

            Refresh_ListViewMoneyDataDetails();

            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
            TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox );
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
                DB.FillComboBox_MoneyBox(ref comboBox_MoneyBox, null);
                if (comboBox_MoneyBox.Items.Count == 0) throw new Exception("ليس لديك الصلاحية لادارة اي صندوق");
                Refresh_ListViewMoneyDataDetails();

                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox );
       
                 for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);

                dataGridView1.TopLeftHeaderCell.Value = "العملة";

                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView1.Columns[5].HeaderCell.Style.BackColor = Color.LightGreen;
                dataGridView1.Columns[11].HeaderCell.Style.BackColor = Color.Orange;
                dataGridView1.Columns[12].HeaderCell.Style.BackColor = Color.LightYellow;

                dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView1.TopLeftHeaderCell.Style.BackColor = Color.LightYellow;
                //tabControl1.TabPages.RemoveAt(5);
              
                IntializeListViewMoneyDataDetailsColumnsWidth();
                IntializeListAccountListViewReport_ColumnsWidth();
                AdjustmentDatagridviewColumnsWidth();
             this.comboBox_MoneyBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_MoneyBox_SelectedIndexChanged);

            }
            catch (Exception ee)
            {
                MessageBox.Show("MainWindow_Load"+ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
           
        }

        #region ToolStripMenuItem
  
        private void تسجيلخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
                DB.LogOut();
            this.Close();
        }

       
        private void comboBox_MoneyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
            Refresh_ListViewMoneyDataDetails();
            TextBoxAccountMoney.Text = MoneyAccountSQL_.GetAccountMoneyOverAll(moneybox);

            //int Stuck_MoneyTransformOpr_IN = new MoneyTransFormOPRSQL(DB).Get_Stuck_MoneyTransformOPR_IN(moneybox);
            //int Stuck_MoneyTransformOpr_OUT = new MoneyTransFormOPRSQL(DB).Get_Stuck_MoneyTransformOPR_OUT(moneybox);

            //linkLabel_Stuck_MoneyTransformOpr_IN.Text = "عمليات التحويل المال المعلقة الواردة:" + Stuck_MoneyTransformOpr_IN;
            //linkLabel_Stuck_MoneyTransformOpr_OUT.Text = "عمليات التحويل المال المعلقة الصادرة:" + Stuck_MoneyTransformOpr_OUT;

        }

        private void linkLabel_Stuck_MoneyTransformOpr_IN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
            Stuck_MoneyTransformOPR_IN_Form Stuck_MoneyTransformOPR_IN_Form_ = new Stuck_MoneyTransformOPR_IN_Form(DB, moneybox);
            Stuck_MoneyTransformOPR_IN_Form_.FormClosed += Form_Closed;

            Stuck_MoneyTransformOPR_IN_Form_.Show();
        }

        private void linkLabel_Stuck_MoneyTransformOpr_OUT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
            Stuck_MoneyTransformOPR_OUT_Form Stuck_MoneyTransformOPR_OUT_Form_ = new Stuck_MoneyTransformOPR_OUT_Form(DB, moneybox);
            Stuck_MoneyTransformOPR_OUT_Form_.FormClosed += Form_Closed;

            Stuck_MoneyTransformOPR_OUT_Form_.Show ();
           
        }



        #endregion
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



    }


}
