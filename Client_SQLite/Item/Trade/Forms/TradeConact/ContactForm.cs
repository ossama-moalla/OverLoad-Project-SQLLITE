using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Forms;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Maintenance.Forms;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Trade.Forms.TradeForms;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Trade.Forms.TradeContact
{
    public partial class ContactForm : Form
    {

        System.Windows.Forms.MenuItem Refresh_MenuItem;
        System.Windows.Forms.MenuItem CreateBillBuy_MenuItem;
        System.Windows.Forms.MenuItem OpenBillBuy_MenuItem;
        System.Windows.Forms.MenuItem EditBillBuy_MenuItem;
        System.Windows.Forms.MenuItem DeleteBillBuy_MenuItem;

        System.Windows.Forms.MenuItem CreateMaintenanceOPR_MenuItem;
        System.Windows.Forms.MenuItem OpenMaintenanceOPR_MenuItem;
        System.Windows.Forms.MenuItem EditMaintenanceOPR_MenuItem;
        System.Windows.Forms.MenuItem DeleteMaintenanceOPR_MenuItem;


        System.Windows.Forms.MenuItem CreateBillSell_MenuItem;
        System.Windows.Forms.MenuItem OpenBillSell_MenuItem;
        System.Windows.Forms.MenuItem EditBillSell_MenuItem;
        System.Windows.Forms.MenuItem DeleteBillSell_MenuItem;

        System.Windows.Forms.MenuItem Open_MoneyOPR_MenuItem;
        System.Windows.Forms.MenuItem Edit_MoneyOPR_MenuItem;
        System.Windows.Forms.MenuItem Delete_MoneyOPR_MenuItem;

        MenuItem AddPayIN_BillSell_MenuItem;
        MenuItem AddPayIN_BillMaintenance_MenuItem;
        MenuItem AddPayOUT_BillBuy_MenuItem;

        DatabaseInterface DB;
        Contact _Contact;
        Currency ReferenceCurrency;
        //List<BillReportDetail> BillReportDetailList;
        public ContactForm(DatabaseInterface db, Contact Contact_)
        {

            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Contact_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لفتح هذه النافذة");
            _Contact = Contact_;
            InitializeComponent();
            Initialize_MenuItems();
            ReferenceCurrency = new CurrencySQL(DB).GetReferenceCurrency();

            Refresh_ListViewMoneyDataDetails();
            Refresh_ListViewSells();
            Refresh_ListViewBuys( );
            Refresh_ListViewMaintenanceOPRs();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
              dataGridView1.Columns[i].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);

            dataGridView1.TopLeftHeaderCell.Value = "العملة";

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
            dataGridView1.Columns[2].HeaderCell.Style.BackColor = Color.LightGreen;
            dataGridView1.Columns[4].HeaderCell.Style.BackColor = Color.Orange;


            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Aqua;
            dataGridView1.TopLeftHeaderCell.Style.BackColor = Color.LightYellow;
            //BillReportDetailList = new ContactSQL(DB).GetContactBillsList(this._Contact);


            textBoxID.Text = _Contact.ContactID.ToString ();
            textBoxName.Text = _Contact.ContactName ;
            TextboxAddress .Text = _Contact.Address;
            TextBoxPhone .Text = _Contact.Phone;
            TextboxMobile.Text = _Contact.Mobile;
            FillReport();

        }

        public async void Initialize_MenuItems()
        {
            Refresh_MenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);

            CreateBillBuy_MenuItem = new System.Windows.Forms.MenuItem("انشاء فاتورة شراء", CreateBillBuy_MenuItem_Click);
            OpenBillBuy_MenuItem = new System.Windows.Forms.MenuItem("فتح", OpenBillBuy_MenuItem_Click);
            EditBillBuy_MenuItem = new System.Windows.Forms.MenuItem("تعديل", EditBillBuy_MenuItem_Click);
            DeleteBillBuy_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteBillBuy_MenuItem_Click);

            CreateMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("انشاء عملية صيانة", CreateMaintenanceOPR_MenuItem_Click);
            OpenMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("فتح", OpenMaintenanceOPR_MenuItem_Click);
            EditMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("تعديل", EditMaintenanceOPR_MenuItem_Click);
            DeleteMaintenanceOPR_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteMaintenanceOPR_MenuItem_Click);

            CreateBillSell_MenuItem = new MenuItem("انشاء فاتورة مبيع", CreateBillSell_MenuItem_Click);
            OpenBillSell_MenuItem = new System.Windows.Forms.MenuItem("فتح", OpenBillSell_MenuItem_Click);
            EditBillSell_MenuItem = new System.Windows.Forms.MenuItem("تعديل", EditBillSell_MenuItem_Click);
            DeleteBillSell_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteBillSell_MenuItem_Click);

            Open_MoneyOPR_MenuItem = new MenuItem("تعديل", Open_MoneyOPR_MenuItem_Click);
            Edit_MoneyOPR_MenuItem = new MenuItem("تعديل", Edit_MoneyOPR_MenuItem_Click);
            Delete_MoneyOPR_MenuItem = new MenuItem("حذف", Delete_MoneyOPR_MenuItem_Click);

            AddPayOUT_BillBuy_MenuItem = new System.Windows.Forms.MenuItem("اضافة دفعة تابعة للفاتورة", AddPayOUT_BillBuy_MenuItem_Click);
            AddPayIN_BillSell_MenuItem = new MenuItem("اضافة دفعة تابعة للفاتورة", AddPayIN_BillSell_MenuItem_Click);
            AddPayIN_BillMaintenance_MenuItem = new MenuItem("اضافة دفعة تابعة للفاتورة", AddPayIN_BillMaintenance_MenuItem_Click);

        }
        private void Refresh_MenuItem_Click(object sender, EventArgs e)
        {
            Refresh_ListViewMaintenanceOPRs();
            Refresh_ListViewMoneyDataDetails();
            Refresh_ListViewSells();
            Refresh_ListViewBuys( );
            FillReport();
        }
        private async  void FillReport()
        {
            try
            {

                List<Contact_BillCurrencyReport> BillCurrencyReportList = new List<Contact_BillCurrencyReport>();
                listViewReport.Items.Clear();
                BillCurrencyReportList = new ContactSQL(DB).Contact_GetBillsReportList(this._Contact.ContactID);

                for (int i = 0; i < BillCurrencyReportList.Count; i++)
                {

                    ListViewItem item = new ListViewItem(BillCurrencyReportList[i].CurrencyName);
                    item.Name = BillCurrencyReportList[i].CurrencyID.ToString();
                    item.SubItems.Add(BillCurrencyReportList[i].BillINCount.ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillINValue.ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillIN_PaysValue.ToString());
                    item.SubItems.Add((BillCurrencyReportList[i].BillINValue-BillCurrencyReportList[i].BillIN_PaysValue).ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillOUTCount.ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillOUTValue.ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillOUT_PaysValue.ToString());
                    item.SubItems.Add((BillCurrencyReportList[i].BillOUTValue - BillCurrencyReportList[i].BillOUT_PaysValue).ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillMaintenanceCount .ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillMaintenanceValue.ToString());
                    item.SubItems.Add(BillCurrencyReportList[i].BillMaintenance_PaysValue.ToString());
                    item.SubItems.Add((BillCurrencyReportList[i].BillMaintenanceValue - BillCurrencyReportList[i].BillMaintenance_PaysValue).ToString());
                  


                    item.UseItemStyleForSubItems = false;
                    item.SubItems[0].BackColor = Color.LightGray;
                    if (BillCurrencyReportList[i].BillINValue == 0 && BillCurrencyReportList[i].BillIN_PaysValue == 0)
                    {
                        item.SubItems[1].BackColor = Color.PaleGoldenrod ;
                        item.SubItems[2].BackColor = Color.PaleGoldenrod;
                        item.SubItems[3].BackColor = Color.PaleGoldenrod;
                        item.SubItems[4].BackColor = Color.PaleGoldenrod;
                    }
                    else
                    {
                        if (BillCurrencyReportList[i].BillINValue != BillCurrencyReportList[i].BillIN_PaysValue)
                        {
                            item.SubItems[1].BackColor = Color.Orange;
                            item.SubItems[2].BackColor = Color.Orange;
                            item.SubItems[3].BackColor = Color.Orange;
                            item.SubItems[4].BackColor = Color.Orange;
                        }
                        else
                        {
                            item.SubItems[1].BackColor = Color.LightGreen;
                            item.SubItems[2].BackColor = Color.LightGreen;
                            item.SubItems[3].BackColor = Color.LightGreen;
                            item.SubItems[4].BackColor = Color.LightGreen;
                        }
                    }
                    if (BillCurrencyReportList[i].BillOUTValue == 0 && BillCurrencyReportList[i].BillOUT_PaysValue==0)
                    {
                        item.SubItems[5].BackColor = Color.PaleGoldenrod;
                        item.SubItems[6].BackColor = Color.PaleGoldenrod;
                        item.SubItems[7].BackColor = Color.PaleGoldenrod;
                        item.SubItems[8].BackColor = Color.PaleGoldenrod;
                    }
                    else
                    {
                        if (BillCurrencyReportList[i].BillOUTValue != BillCurrencyReportList[i].BillOUT_PaysValue)
                        {
                            item.SubItems[5].BackColor = Color.Orange;
                            item.SubItems[6].BackColor = Color.Orange;
                            item.SubItems[7].BackColor = Color.Orange;
                            item.SubItems[8].BackColor = Color.Orange;
                        }
                        else
                        {
                            item.SubItems[5].BackColor = Color.LightGreen;
                            item.SubItems[6].BackColor = Color.LightGreen;
                            item.SubItems[7].BackColor = Color.LightGreen;
                            item.SubItems[8].BackColor = Color.LightGreen;
                        }
                    }
                    if (BillCurrencyReportList[i].BillMaintenanceValue == 0 && BillCurrencyReportList[i].BillMaintenance_PaysValue == 0)
                    {
                        item.SubItems[9].BackColor = Color.PaleGoldenrod;
                        item.SubItems[10].BackColor = Color.PaleGoldenrod;
                        item.SubItems[11].BackColor = Color.PaleGoldenrod;
                        item.SubItems[12].BackColor = Color.PaleGoldenrod;
                    }
                    else
                    {
                        if (BillCurrencyReportList[i].BillMaintenanceValue != BillCurrencyReportList[i].BillMaintenance_PaysValue)
                        {
                            item.SubItems[9].BackColor = Color.Orange;
                            item.SubItems[10].BackColor = Color.Orange;
                            item.SubItems[11].BackColor = Color.Orange;
                            item.SubItems[12].BackColor = Color.Orange;
                        }
                        else
                        {
                            item.SubItems[9].BackColor = Color.LightGreen;
                            item.SubItems[10].BackColor = Color.LightGreen;
                            item.SubItems[11].BackColor = Color.LightGreen;
                            item.SubItems[12].BackColor = Color.LightGreen;
                        }
                    }
                    
                    listViewReport.Items.Add(item);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(" FillReport:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
  
            }
        }

        private void tabPage1_Resize(object sender, EventArgs e)
        {
            AdjustmentDatagridviewColumnsWidth();
            //IntializeListViewMoneyDataDetailsColumnsWidth();
            //IntializeListViewSellsColumnsWidth();

        }
        #region ReportSells
        public async void Sells_FillReport()
        {
            try
            {
                Contact_Sells_Report Contact_Sells_Report_
               = new ContactSQL(DB).Get_Contact_Sells_Report(_Contact.ContactID);
                textBoxSells_ItemsINValue.Text = Contact_Sells_Report_.Bills_ItemsIN_Value;
                textBoxSells_Value.Text = Contact_Sells_Report_.Bills_Value;
                textBoxSellsPaysValue.Text = Contact_Sells_Report_.Bills_Pays_Value;
                textBoxSellsPaysRmain.Text = Contact_Sells_Report_.Bills_Pays_Remain;
                textBoxSells_ItemsIn_RealValue.Text = System.Math.Round(Contact_Sells_Report_.Bills_ItemsIN_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxSellRealValue.Text = System.Math.Round(Contact_Sells_Report_.Bills_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxSells_RealProfit.Text = System.Math.Round((Contact_Sells_Report_.Bills_RealValue - Contact_Sells_Report_.Bills_ItemsIN_RealValue), 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxSells_RealPays.Text = System.Math.Round(Contact_Sells_Report_.Bills_Pays_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;

            }
            catch (Exception ee)
            {
                MessageBox.Show("Sells_FillReport:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

           

        }
        public async void Refresh_ListViewSells()
        {
            try
            {
                listViewSells.Items.Clear();

                List<Contact_Sells_ReportDetail> Contact_Sells_ReportDetailList
                          = new ContactSQL(DB).Get_Contact_Sells_ReportDetail(_Contact.ContactID);

                for (int i = 0; i < Contact_Sells_ReportDetailList.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Contact_Sells_ReportDetailList[i].Bill_Date.ToShortDateString());
                    item.Name = Contact_Sells_ReportDetailList[i].Bill_ID.ToString();
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].Bill_ID.ToString());
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].SellType);
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].ClauseS_Count.ToString());
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].BillValue.ToString() + " " + Contact_Sells_ReportDetailList[i].CurrencySymbol);
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].ExchangeRate.ToString());
                    //item.SubItems.Add(Report_Sells_Day_ReportDetail_List[i].PaysCount.ToString());
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].PaysAmount);
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].PaysRemain.ToString() + " " + Contact_Sells_ReportDetailList[i].CurrencySymbol);
                    item.SubItems.Add(Contact_Sells_ReportDetailList[i].Source_ItemsIN_Cost_Details);
                    item.SubItems.Add(System.Math.Round(Contact_Sells_ReportDetailList[i].Source_ItemsIN_RealCost,3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(System.Math.Round(Contact_Sells_ReportDetailList[i].BillValue_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(System.Math.Round((Contact_Sells_ReportDetailList[i].BillValue_RealValue
                        - Contact_Sells_ReportDetailList[i].Source_ItemsIN_RealCost),3) + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add (System.Math.Round(Contact_Sells_ReportDetailList[i].RealPaysValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.UseItemStyleForSubItems = false;
                    if (Contact_Sells_ReportDetailList[i].BillValue > 0
                        || Contact_Sells_ReportDetailList[i].RealPaysValue > 0)
                    {
                        if (Contact_Sells_ReportDetailList[i].PaysRemain != 0)
                            for (int j = 3; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                        else
                            for (int j = 3; j <= 7; j++)
                                item.SubItems[j].BackColor = Color.LightGreen;
                    }
                    else
                    {
                        for (int j = 3; j <= 7; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    }

                    if (Contact_Sells_ReportDetailList[i].Source_ItemsIN_RealCost
                        > Contact_Sells_ReportDetailList[i].BillValue_RealValue)
                        for (int j = 8; j <= 11; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else if (Contact_Sells_ReportDetailList[i].Source_ItemsIN_RealCost
                       < Contact_Sells_ReportDetailList[i].BillValue_RealValue)
                        for (int j = 8; j <= 11; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    else
                        for (int j = 8; j <= 11; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;

                    if (Contact_Sells_ReportDetailList[i].Source_ItemsIN_RealCost
                        > Contact_Sells_ReportDetailList[i].RealPaysValue)
                        item.SubItems[12].BackColor = Color.Orange;
                    else if (Contact_Sells_ReportDetailList[i].Source_ItemsIN_RealCost
                       < Contact_Sells_ReportDetailList[i].RealPaysValue)
                        item.SubItems[12].BackColor = Color.LightGreen;
                    else
                        item.SubItems[12].BackColor = Color.LightYellow;
                    listViewSells.Items.Add(item);


                }

                Sells_FillReport();

            }
            catch (Exception ee)
            {
                MessageBox.Show("Refresh_ListViewSells:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
       
        private void ListViewSells_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left )
                if (listViewSells.SelectedItems.Count > 0)
                OpenBillSell_MenuItem.PerformClick();
        }
        private void ListViewSells_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                OpenBillSell_MenuItem.PerformClick();
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
                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] {OpenBillSell_MenuItem  , EditBillSell_MenuItem, DeleteBillSell_MenuItem
                            , new MenuItem("-"),CreateBillSell_MenuItem });
                            MenuItemList.AddRange(new MenuItem[] { new MenuItem("-"), AddPayIN_BillSell_MenuItem });
                            listViewSells.ContextMenu = new ContextMenu(MenuItemList.ToArray());
                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"), CreateBillSell_MenuItem };
                            listViewSells.ContextMenu = new ContextMenu(mi1);

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
            catch (Exception ee)
            {
                MessageBox.Show("IntializeListViewSellsColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        private void CreateBillSell_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BillSellForm BillOUTForm_ = new BillSellForm(DB, DateTime.Now, _Contact);
                BillOUTForm_.FormClosed += Form_Closed;
                BillOUTForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateBillSell_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                BillOUTForm_.Show ();
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
                    Refresh_ListViewMoneyDataDetails();
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
        #region MoneyAccount
        public void AdjustmentDatagridviewColumnsWidth()
        {
            try
            {
                int columnscount = dataGridView1.Columns.Count + 1;
                int w = (dataGridView1.Width) / columnscount; ;
                dataGridView1.RowHeadersWidth = w - 2;
                for (int i = 0; i < columnscount - 1; i++) dataGridView1.Columns[i].Width = w;
            }
            catch (Exception ee)
            {
                MessageBox.Show("AdjustmentDatagridviewColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }

        public async void Refresh_ListViewMoneyDataDetails()
        {
            try
            {
                Money_FillReport();
                double realValue_in = 0, realValue_out = 0;
                #region PayDaySection
                ListViewMoneyDataDetails.Items.Clear();
                List<Contact_Pays_ReportDetail> Contact_Pays_ReportDetailList
                          = new ContactSQL(DB).Get_Contact_Pays_ReportDetail(_Contact.ContactID);
                for (int i = 0; i < Contact_Pays_ReportDetailList.Count; i++)
                {

                    string payopridstr = (Contact_Pays_ReportDetailList[i].PayDirection == Contact_Pays_ReportDetail.DIRECTION_IN ? "I" : "O")
                        + Contact_Pays_ReportDetailList[i].PayOPR_ID.ToString();

                    string Direction = "";

                    if (Contact_Pays_ReportDetailList[i].PayDirection == Contact_Pays_ReportDetail.DIRECTION_IN)
                    {
                        Direction = "داخل الى الصندوق";
                        realValue_in += Contact_Pays_ReportDetailList[i].RealValue;
                    }
                    else
                    {
                        Direction = "خارج من الصندوق";
                        realValue_out += Contact_Pays_ReportDetailList[i].RealValue;

                    }
                    ListViewItem item = new ListViewItem(Contact_Pays_ReportDetailList[i].PayDate.ToShortDateString());
                    item.Name = payopridstr;
                    item.SubItems.Add(Direction);
                    item.SubItems.Add(Contact_Pays_ReportDetailList[i].PayOPR_ID.ToString());

                    item.SubItems.Add(Contact_Pays_ReportDetailList[i].Value.ToString() + " "
                        + Contact_Pays_ReportDetailList[i].CurrencySymbol);
                    item.SubItems.Add(Contact_Pays_ReportDetailList[i].ExchangeRate.ToString());
                    item.SubItems.Add(System.Math.Round(Contact_Pays_ReportDetailList[i].RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Operation.GetOperationName(Contact_Pays_ReportDetailList[i].OperationType) + " رقم " + Contact_Pays_ReportDetailList[i].OperationID.ToString());
                    //item.UseItemStyleForSubItems = false;
                    if (Contact_Pays_ReportDetailList[i].PayDirection == Contact_Pays_ReportDetail.DIRECTION_IN)
                        item.BackColor = Color.LightGreen;
                    else
                        item.BackColor = Color.Orange;
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

                    ListViewMoneyDataDetails.Items.Add(item);

                }

                #endregion

                textBox_Real_In_Money.Text = System.Math.Round(realValue_in,3).ToString() + ReferenceCurrency.CurrencySymbol;
                textBox_Real_out_Money.Text = System.Math.Round(realValue_out,3).ToString() + ReferenceCurrency.CurrencySymbol;
                textBox_Real_Clear_value.Text = System.Math.Round((realValue_in - realValue_out),3).ToString() + ReferenceCurrency.CurrencySymbol;
                if ((realValue_in - realValue_out) < 0)
                    textBox_Real_Clear_value.BackColor = Color.Orange;
                else
                    textBox_Real_Clear_value.BackColor = Color.LimeGreen;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Refresh_ListViewMoneyDataDetails:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public void Money_FillReport()
        {
            try
            {
                dataGridView1.Rows.Clear();

                List<Contact_PayCurrencyReport> Contact_PayCurrencyReportList = new ContactSQL(DB).Get_Contact_PayCurrencyReport(_Contact.ContactID); ;

                string In_money = "", Out_Money = "";

                for (int i = 0; i < Contact_PayCurrencyReportList.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    double in_all = Contact_PayCurrencyReportList[i].PaysIN_Sell
                        + Contact_PayCurrencyReportList[i].PaysIN_Maintenance;

                    double out_all = Contact_PayCurrencyReportList[i].PaysOUT_Buy;

                    double clear_value = in_all - out_all;
                    dataGridView1.Rows[i].HeaderCell.Value = Contact_PayCurrencyReportList[i].CurrencyName;

                    dataGridView1.Rows[i].Cells[0].Value = Contact_PayCurrencyReportList[i].PaysIN_Sell.ToString() + " " + Contact_PayCurrencyReportList[i].CurrencySymbol;
                    dataGridView1.Rows[i].Cells[1].Value = Contact_PayCurrencyReportList[i].PaysIN_Maintenance.ToString() + " " + Contact_PayCurrencyReportList[i].CurrencySymbol;

                    dataGridView1.Rows[i].Cells[2].Value = in_all + " " + Contact_PayCurrencyReportList[i].CurrencySymbol;

                    dataGridView1.Rows[i].Cells[3].Value = Contact_PayCurrencyReportList[i].PaysOUT_Buy.ToString() + " " + Contact_PayCurrencyReportList[i].CurrencySymbol;

                    dataGridView1.Rows[i].Cells[4].Value = out_all + " " + Contact_PayCurrencyReportList[i].CurrencySymbol;
                    dataGridView1.Rows[i].Cells[5].Value = clear_value + " " + Contact_PayCurrencyReportList[i].CurrencySymbol;

                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LightGreen;
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGreen;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGreen;

                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Orange;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Orange;

                    if (clear_value > 0)
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.LightGreen;
                    else
                        dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Orange;
                    if (in_all > 0)
                        In_money += in_all + Contact_PayCurrencyReportList[i].CurrencySymbol + " ";
                    if (out_all > 0)
                        Out_Money += out_all + Contact_PayCurrencyReportList[i].CurrencySymbol;
                    if (i != Contact_PayCurrencyReportList.Count - 1)
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
            catch (Exception ee)
            {
                MessageBox.Show("Money_FillReport:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        
        private void ListViewMoneyDataDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                if (ListViewMoneyDataDetails.SelectedItems.Count > 0)
                    Open_MoneyOPR_MenuItem.PerformClick();
        }
        private void ListViewMoneyDataDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                if (ListViewMoneyDataDetails.SelectedItems.Count > 0)
                    Open_MoneyOPR_MenuItem.PerformClick();
        }
        private void ListViewMoneyDataDetails_MouseDown(object sender, MouseEventArgs e)
        {
            try
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
                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] { Edit_MoneyOPR_MenuItem, Delete_MoneyOPR_MenuItem });
                            ListViewMoneyDataDetails.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                        }
                        else
                        {
                            ListViewMoneyDataDetails.ContextMenu = null;
                        }

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("ListViewMoneyDataDetails_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public async void IntializeListViewMoneyDataDetailsColumnsWidth()
        {
            try
            {
                ListViewMoneyDataDetails.Columns[0].Width = 100;//
                ListViewMoneyDataDetails.Columns[1].Width = 150;
                ListViewMoneyDataDetails.Columns[2].Width = 150;
                ListViewMoneyDataDetails.Columns[3].Width = 200;
                ListViewMoneyDataDetails.Columns[4].Width = 200;

                ListViewMoneyDataDetails.Columns[5].Width = 150;
                ListViewMoneyDataDetails.Columns[6].Width = 200;
                //if (ListViewMoneyDataDetails.Width > 1010)
                //    ListViewMoneyDataDetails.Columns[7].Width = ListViewMoneyDataDetails.Width - 1005;
            }
            catch (Exception ee)
            {
                MessageBox.Show("IntializeListViewMoneyDataDetailsColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       

        }

        private void Delete_MoneyOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                string s = ListViewMoneyDataDetails.SelectedItems[0].Name;
  
                    if (s.Substring(0, 1) == "I")
                    {
                        uint payinid = Convert.ToUInt32(s.Substring(1));
                        bool success = new PayINSQL(DB).Delete_PayIN(payinid);
                        if (success)
                        {
                            Refresh_ListViewMoneyDataDetails();
                        }
                    }
                    else
                    {
                        uint payoutid = Convert.ToUInt32(s.Substring(1));
                        bool success = new PayOUTSQL(DB).Delete_PayOUT(payoutid);
                        if (success)
                        {
                            Refresh_ListViewMoneyDataDetails();
                        }
                    }


              

            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_MoneyOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        private void Edit_MoneyOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string s = ListViewMoneyDataDetails.SelectedItems[0].Name;

                if (s.Substring(0, 1) == "I")
                {
                    uint payinid = Convert.ToUInt32(s.Substring(1));
                    PayIN PayIN_ = new PayINSQL(DB).GetPayIN_INFO_BYID(payinid);
                    PayINForm PayINForm_ = new PayINForm(DB, PayIN_, true );
                    PayINForm_.FormClosed += Form_Closed;
                    PayINForm_.Show();

                }
                else
                {
                    uint payoutid = Convert.ToUInt32(s.Substring(1));
                    PayOUT PayOUT_ = new PayOUTSQL(DB).GetPayOUT_INFO_BYID(payoutid);
                    PayOUTForm PayOUTForm_ = new PayOUTForm(DB, PayOUT_, true );
                    PayOUTForm_.FormClosed += Form_Closed;
                    PayOUTForm_.Show();

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_MoneyOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private void Open_MoneyOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string s = ListViewMoneyDataDetails.SelectedItems[0].Name;

                    if (s.Substring(0, 1) == "I")
                    {
                        uint payinid = Convert.ToUInt32(s.Substring(1));
                        PayIN PayIN_ = new PayINSQL(DB).GetPayIN_INFO_BYID(payinid);
                        PayINForm PayINForm_ = new PayINForm(DB, PayIN_, false);
                    PayINForm_.FormClosed += Form_Closed;
                    PayINForm_.Show();

                }
                else
                    {
                        uint payoutid = Convert.ToUInt32(s.Substring(1));
                        PayOUT PayOUT_ = new PayOUTSQL(DB).GetPayOUT_INFO_BYID(payoutid);
                        PayOUTForm PayOUTForm_ = new PayOUTForm(DB, PayOUT_, false);
                    PayOUTForm_.FormClosed += Form_Closed;
                    PayOUTForm_.Show();

                }




            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_MoneyOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        #endregion

        #region ReportBuys
        public async void Buys_FillReport()
        {
            try
            {
                Contact_Buys_Report Contact_Buys_Report_
               = new ContactSQL(DB).Get_Contact_Buys_Report(_Contact.ContactID);
                textBoxBuys_AmountIN.Text = Contact_Buys_Report_.Amount_IN.ToString();
                textBoxBuys_AmountRemain.Text = Contact_Buys_Report_.Amount_Remain.ToString();
                textBoxBuys_Value.Text = Contact_Buys_Report_.Bills_Value;
                textBoxBuysPaysValue.Text = Contact_Buys_Report_.Bills_Pays_Value;
                textBoxBuysPaysRmain.Text = Contact_Buys_Report_.Bills_Pays_Remain;
                if (Contact_Buys_Report_.Bills_Pays_Remain_UPON_Bill_Currency > 0)
                    textBoxBuysPaysRmain.BackColor = Color.Orange;
                else
                    textBoxBuysPaysRmain.BackColor = Color.LimeGreen;
                textBoxBuyRealValue.Text = System.Math.Round(Contact_Buys_Report_.Bills_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_RealPays.Text = System.Math.Round(Contact_Buys_Report_.Bills_Pays_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;

                textBoxBuys_OutValue.Text = Contact_Buys_Report_.Bills_ItemsOut_Value;
                textBoxBuys_OutRealValue.Text = System.Math.Round(Contact_Buys_Report_.Bills_ItemsOut_RealValue,3) + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBuys_Out_Pays.Text = Contact_Buys_Report_.Bills_Pays_Return_Value;
                textBoxBuys_Out_Pays_RealValue.Text = System.Math.Round(Contact_Buys_Report_.Bills_Pays_Return_RealValue,3) + " " + ReferenceCurrency.CurrencySymbol;



            }
            catch (Exception ee)
            {
                MessageBox.Show("Buys_FillReport:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        public async void Refresh_ListViewBuys()
        {
            try
            {

                listViewBuys.Items.Clear();
                #region DaySection

                List<Contact_Buys_ReportDetail> Contact_Buys_ReportDetailList
                          = new ContactSQL(DB).Get_Contact_Buys_ReportDetail(_Contact.ContactID);
                for (int i = 0; i < Contact_Buys_ReportDetailList.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Contact_Buys_ReportDetailList[i].Bill_Date.ToShortDateString());
                    item.Name = Contact_Buys_ReportDetailList[i].Bill_ID.ToString();
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].Bill_ID.ToString());
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].ClauseS_Count.ToString());
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].Amount_IN.ToString());
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].Amount_Remain.ToString());
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].BillValue.ToString() + " " + Contact_Buys_ReportDetailList[i].CurrencySymbol);
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].ExchangeRate.ToString());
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].PaysAmount);
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].PaysRemain.ToString() + " " + Contact_Buys_ReportDetailList[i].CurrencySymbol);
                    item.SubItems.Add(System.Math.Round(Contact_Buys_ReportDetailList[i].Bill_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(System.Math.Round(Contact_Buys_ReportDetailList[i].Bill_Pays_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].Bill_ItemsOut_Value.ToString());
                    item.SubItems.Add(Contact_Buys_ReportDetailList[i].Bill_Pays_Return_Value);
                    item.SubItems.Add(System.Math.Round(Contact_Buys_ReportDetailList[i].Bill_Pays_Return_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol);

                    //-Contact_Buys_ReportDetailList[i].Source_ItemsIN_RealCost) + " " + ReferenceCurrency.CurrencySymbol);
                    //item.SubItems.Add(Contact_Buys_ReportDetailList[i].RealPaysValue.ToString() + " " + ReferenceCurrency.CurrencySymbol);

                    item.UseItemStyleForSubItems = false;
                    if (Contact_Buys_ReportDetailList[i].Amount_Remain == 0)
                        for (int j = 3; j <= 4; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else
                        for (int j = 3; j <= 4; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    if (Contact_Buys_ReportDetailList[i].PaysRemain != 0)
                        for (int j = 5; j <= 8; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else
                        for (int j = 5; j <= 8; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;

                    if (Contact_Buys_ReportDetailList[i].Bill_Pays_RealValue
                        > Contact_Buys_ReportDetailList[i].Bill_Pays_Return_RealValue)
                        for (int j = 9; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    else if (Contact_Buys_ReportDetailList[i].Bill_Pays_RealValue
                        < Contact_Buys_ReportDetailList[i].Bill_Pays_Return_RealValue)
                        for (int j = 9; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightGreen;
                    else
                        for (int j = 9; j <= 12; j++)
                            item.SubItems[j].BackColor = Color.LightYellow;
                    listViewBuys.Items.Add(item);


                }
                #endregion
                Buys_FillReport();


            }
            catch (Exception ee)
            {
                MessageBox.Show("Refresh_ListViewBuys:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
      
        private void ListViewBuys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                if (listViewBuys.SelectedItems.Count > 0)
                OpenBillBuy_MenuItem.PerformClick();
        }
        private void ListViewBuys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                OpenBillBuy_MenuItem.PerformClick();
        }
        private void ListViewBuys_MouseDown(object sender, MouseEventArgs e)
        {
            try
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
                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] {OpenBillBuy_MenuItem , EditBillBuy_MenuItem , DeleteBillBuy_MenuItem
                            , new MenuItem("-"),CreateBillBuy_MenuItem });
                            MenuItemList.AddRange(new MenuItem[] { new MenuItem("-"), AddPayOUT_BillBuy_MenuItem });
                            listViewBuys.ContextMenu = new ContextMenu(MenuItemList.ToArray());
                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"), CreateBillBuy_MenuItem };
                            listViewBuys.ContextMenu = new ContextMenu(mi1);

                        }

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("ListViewBuys_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public async void IntializeListViewBuysColumnsWidth()
        {
            try
            {
                listViewBuys.Columns[0].Width = 75;//time
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
            catch (Exception ee)
            {
                MessageBox.Show("IntializeListViewBuysColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

            
        }
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                bool Refresh_FillReport = false;
                OverLoad_Form form = (OverLoad_Form)sender;
                if ( form.Refresh_ListViewMoneyDataDetails_Flag)
                    {
                        Refresh_ListViewMoneyDataDetails();
                    Refresh_FillReport = true;
                    }
                if (form.Refresh_ListViewBuys_Flag )
                {
                    Refresh_ListViewBuys();
                    Refresh_FillReport = true;
                }
                if (form.Refresh_ListViewMaintenanceOPRs_Flag  || form.Refresh_ListViewMoneyDataDetails_Flag)
                {
                    Refresh_ListViewMaintenanceOPRs ();
                    Refresh_FillReport = true;
                }
                if (form.Refresh_ListViewSells_Flag )
                {
                    Refresh_ListViewSells();
                    Refresh_FillReport = true;
                }
                if (Refresh_FillReport) FillReport();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Form_Closed:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void CreateBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BillBuyForm BillOUTForm_ = new BillBuyForm(DB, DateTime.Now, _Contact );
                BillOUTForm_.FormClosed += Form_Closed;
                BillOUTForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateBillBuy_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }
        private void OpenBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint billbuyid = Convert.ToUInt32(listViewBuys.SelectedItems[0].Name);
                BillBuy BillBuy_ = new BillBuySQL(DB).GetBillBuy_INFO_BYID(billbuyid);
                BillBuyForm BillOUTForm_ = new BillBuyForm(DB, BillBuy_, false);
                BillOUTForm_.FormClosed += Form_Closed;
                BillOUTForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenBillBuy_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void EditBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint billbuyid = Convert.ToUInt32(listViewBuys.SelectedItems[0].Name);
                BillBuy BillBuy_ = new BillBuySQL(DB).GetBillBuy_INFO_BYID(billbuyid);
                BillBuyForm BillOUTForm_ = new BillBuyForm(DB, BillBuy_, true);
                BillOUTForm_.FormClosed += Form_Closed;
                BillOUTForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditBillBuy_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void DeleteBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint billbuyid = Convert.ToUInt32(listViewBuys.SelectedItems[0].Name);
                bool success = new BillBuySQL(DB).DeleteBillBuy(billbuyid);
                if (success)
                {
                    Refresh_ListViewBuys( );
                    Refresh_ListViewMoneyDataDetails();
                    FillReport();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteBillBuy_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void AddPayOUT_BillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewBuys.SelectedItems.Count == 1)
                {
                    uint sid = Convert.ToUInt32(listViewBuys.SelectedItems[0].Name);
                    BillBuy BillBuy_ = new BillBuySQL(DB).GetBillBuy_INFO_BYID(sid);
                    PayOUTForm PayOUTForm_ = new PayOUTForm(DB, DateTime.Now, BillBuy_);
                    PayOUTForm_.FormClosed += Form_Closed;
                    PayOUTForm_.Show ();
                    
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("AddPayOUT_BillBuy_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        #endregion
        #region ReportMaintenanceOPRs
        public async void MaintenanceOPRs_FillReport()
        {
            try
            {
                Contact_MaintenanceOPRs_Report Contact_MaintenanceOPRs_Report_
               = new ContactSQL(DB).Get_Contact_MaintenanceOPRs_Report(_Contact.ContactID);
                textBoxMaintenanceOPRs_Count.Text = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_Count.ToString();
                textBoxMaintenanceOPRs_EndWorkCount.Text = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_EndWork_Count.ToString();
                textBoxMaintenanceOPRs_RepairedCount.Text = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_Repaired_Count.ToString(); ;
                textBoxMaintenanceOPRs_EndWarrantyCount.Text = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_EndWarranty_Count.ToString(); ;
                textBoxMaintenanceOPRs_Warrantycount.Text = Contact_MaintenanceOPRs_Report_.MaintenanceOPRs_Warranty_Count.ToString(); ;

                textBoxBillMaintenanceOPRs_Value.Text = Contact_MaintenanceOPRs_Report_.BillMaintenances_Value;
                textBoxBillMaintenanceOPRs__PaysValue.Text = Contact_MaintenanceOPRs_Report_.BillMaintenances_Pays_Value;
                textBoxBillMaintenanceOPRs__PaysRmain.Text = Contact_MaintenanceOPRs_Report_.BillMaintenances_Pays_Remain;
                textBoxBillMaintenanceOPRs__ItemsOutValue.Text = Contact_MaintenanceOPRs_Report_.BillMaintenances_ItemsOut_Value;
                textBoxBillMaintenanceOPRs_ItemsOutRealValue.Text = System.Math.Round(Contact_MaintenanceOPRs_Report_.BillMaintenances_ItemsOut_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealValue.Text = System.Math.Round(Contact_MaintenanceOPRs_Report_.BillMaintenances_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                textBoxBillMaintenanceOPRs__RealPays.Text = System.Math.Round(Contact_MaintenanceOPRs_Report_.BillMaintenances_Pays_RealValue,3).ToString() + " " + ReferenceCurrency.CurrencySymbol;


            }
            catch (Exception ee)
            {
                MessageBox.Show("MaintenanceOPRs_FillReport:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        public async void Refresh_ListViewMaintenanceOPRs()
        {
            try
            {
                listViewMaintenanceOPRs.Items.Clear();
                #region DaySection

                List<Contact_MaintenanceOPRs_ReportDetail> Contact_MaintenanceOPRs_ReportDetailList
                          = new ContactSQL(DB).Get_Contact_MaintenanceOPRs_ReportDetail(_Contact.ContactID);
                for (int i = 0; i < Contact_MaintenanceOPRs_ReportDetailList.Count; i++)
                {

                    ListViewItem item = new ListViewItem(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_Date.ToShortDateString());
                    item.Name = Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_ID.ToString();
                    item.UseItemStyleForSubItems = false;
                    item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_ID.ToString());
                    item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].ItemName);
                    item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].ItemCompany);
                    item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].FolderName);
                    item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].FalutDesc);
                    for (int j = 0; j <= 5; j++)
                        item.SubItems[j].BackColor = Color.LightYellow;
                    if (Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_Endworkdate == null)
                    {
                        item.SubItems.Add("في الصيانة");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        for (int j = 6; j < 10; j++)
                            item.SubItems[j].BackColor = Color.Orange;

                    }
                    else
                    {

                        item.SubItems.Add(
                            Convert.ToDateTime(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_Endworkdate).ToShortDateString());
                        item.SubItems[6].BackColor = Color.LimeGreen;
                        bool repaired = Convert.ToBoolean(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_Rpaired);
                        if (repaired)
                        {
                            item.SubItems.Add("تم الاصلاح");
                            item.SubItems[7].BackColor = Color.LimeGreen;
                        }
                        else
                        {
                            item.SubItems.Add("لم يتم الاصلاح");
                            item.SubItems[7].BackColor = Color.Orange;
                        }
                        if(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_DeliverDate!=null)
                        {
                            item.SubItems.Add(
                                                  Convert.ToDateTime(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_DeliverDate).ToShortDateString());
                            item.SubItems[8].BackColor = Color.LimeGreen;
                        }
                        else 
                        {
                            item.SubItems.Add("-");
                            item.SubItems[8].BackColor = Color.Orange;
                        }
                        if(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_EndWarrantyDate!=null )
                        {
                            item.SubItems.Add(
                                                     Convert.ToDateTime(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_EndWarrantyDate).ToShortDateString());

                            if (Convert.ToDateTime(Contact_MaintenanceOPRs_ReportDetailList[i].MaintenanceOPR_EndWarrantyDate) > DateTime.Now)
                                item.SubItems[9].BackColor = Color.LimeGreen;
                            else
                                item.SubItems[9].BackColor = Color.Orange;
                        }
                        else 
                        {
                            item.SubItems.Add("لا يوجد ضمان");
                            item.SubItems[9].BackColor = Color.Yellow;
                        }


                    }

                    if(Contact_MaintenanceOPRs_ReportDetailList[i].BillMaintenanceID!=null )
                    {

                        item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].BillValue.ToString() + " " + Contact_MaintenanceOPRs_ReportDetailList[i].CurrencySymbol);
                        item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].ExchangeRate.ToString());
                        item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].PaysAmount);
                        item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].PaysRemain.ToString() + " " + Contact_MaintenanceOPRs_ReportDetailList[i].CurrencySymbol);
                        if (Contact_MaintenanceOPRs_ReportDetailList[i].PaysRemain == 0)
                            for (int j = 10; j <= 13; j++)
                                item.SubItems[j].BackColor = Color.LimeGreen;
                        else
                            for (int j = 10; j <= 13; j++)
                                item.SubItems[j].BackColor = Color.Orange ;
                        item.SubItems.Add(Contact_MaintenanceOPRs_ReportDetailList[i].Bill_ItemsOut_Value.ToString());
                        item.SubItems.Add(System.Math.Round(Convert .ToDouble ( Contact_MaintenanceOPRs_ReportDetailList[i].Bill_ItemsOut_RealValue),3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(System.Math.Round(Convert.ToDouble(Contact_MaintenanceOPRs_ReportDetailList[i].Bill_RealValue),3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        item.SubItems.Add(System.Math.Round(Convert.ToDouble(Contact_MaintenanceOPRs_ReportDetailList[i].Bill_Pays_RealValue),3).ToString() + " " + ReferenceCurrency.CurrencySymbol);
                        if (Contact_MaintenanceOPRs_ReportDetailList[i].Bill_Pays_RealValue >= Contact_MaintenanceOPRs_ReportDetailList[i].Bill_RealValue)
                            for (int j = 16; j <= 17; j++)
                                item.SubItems[j].BackColor = Color.LimeGreen;
                        else
                            for (int j = 16; j <= 17; j++)
                                item.SubItems[j].BackColor = Color.Orange;
                    }
                    else
                    {

                        item.SubItems.Add("غير منشاة");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        for (int j = 10; j <= 17; j++)
                            item.SubItems[j].BackColor = Color.Orange;
                    }


                    listViewMaintenanceOPRs.Items.Add(item);


                }
                #endregion

                MaintenanceOPRs_FillReport();


            }
            catch (Exception ee)
            {
                MessageBox.Show("Refresh_ListViewMaintenanceOPRs:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
       
        private void ListViewMaintenanceOPRs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                if (listViewMaintenanceOPRs.SelectedItems.Count > 0)
                OpenMaintenanceOPR_MenuItem.PerformClick();
        }
        private void ListViewMaintenanceOPRs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                OpenMaintenanceOPR_MenuItem.PerformClick();
        }
        private void ListViewMaintenanceOPRs_MouseDown(object sender, MouseEventArgs e)
        {
            try
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
                            MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(listitem.Name));
                            BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(MaintenanceOPR_);

                            List<MenuItem> MenuItemList = new List<MenuItem>();
                            MenuItemList.Add(Refresh_MenuItem);
                            MenuItemList.Add(new MenuItem("-"));
                            MenuItemList.AddRange(new MenuItem[] {OpenMaintenanceOPR_MenuItem , EditMaintenanceOPR_MenuItem , DeleteMaintenanceOPR_MenuItem
                            , new MenuItem("-"),CreateMaintenanceOPR_MenuItem });
                            if (BillMaintenance_ != null)
                                MenuItemList.AddRange(new MenuItem[] { new MenuItem("-"), AddPayIN_BillMaintenance_MenuItem });
                            listViewMaintenanceOPRs.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                        }
                        else
                        {

                            MenuItem[] mi1 = new MenuItem[] { Refresh_MenuItem, new MenuItem("-"), CreateMaintenanceOPR_MenuItem };
                            listViewMaintenanceOPRs.ContextMenu = new ContextMenu(mi1);

                        }

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("ListViewMaintenanceOPRs_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public async void IntializeListViewMaintenanceOPRsColumnsWidth()
        {
            try
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
            catch (Exception ee)
            {
                MessageBox.Show("IntializeListViewMaintenanceOPRsColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void CreateMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, _Contact);
                MaintenanceOPRForm_.FormClosed += Form_Closed;
                MaintenanceOPRForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateMaintenanceOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       
        }
        private void OpenMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint MaintenanceOPRid = Convert.ToUInt32(listViewMaintenanceOPRs.SelectedItems[0].Name);
                MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(MaintenanceOPRid);
                Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, false);
                MaintenanceOPRForm_.FormClosed += Form_Closed;
                MaintenanceOPRForm_.Show ();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenMaintenanceOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void EditMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint MaintenanceOPRid = Convert.ToUInt32(listViewMaintenanceOPRs.SelectedItems[0].Name);
                MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(MaintenanceOPRid);
                Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, true);
                MaintenanceOPRForm_.FormClosed += Form_Closed;
                MaintenanceOPRForm_.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditMaintenanceOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void DeleteMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint MaintenanceOPRid = Convert.ToUInt32(listViewMaintenanceOPRs.SelectedItems[0].Name);
                bool success = new MaintenanceOPRSQL(DB).DeleteMaintenanceOPR(MaintenanceOPRid);
                if (success)
                {
                    Refresh_ListViewMaintenanceOPRs();
                    Refresh_ListViewMoneyDataDetails();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteMaintenanceOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }
        private void AddPayIN_BillMaintenance_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewMaintenanceOPRs.SelectedItems.Count == 1)
                {
                    uint sid = Convert.ToUInt32(listViewMaintenanceOPRs.SelectedItems[0].Name);

                    MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(sid);
                    BillMaintenance BillMaintenance_ = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(MaintenanceOPR_);
                    PayINForm PayINForm_ = new PayINForm(DB, DateTime.Now, BillMaintenance_);
                    PayINForm_.FormClosed += Form_Closed;
                    PayINForm_.Show();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddPayIN_BillMaintenance_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            





        }

        #endregion

        private void ContactForm_Load(object sender, EventArgs e)
        {
            try
            {
                IntializeListViewMoneyDataDetailsColumnsWidth();
                //IntializeListAccountListViewReport_ColumnsWidth();
                AdjustmentDatagridviewColumnsWidth();
                this.tabPage1.Resize += new System.EventHandler(this.tabPage1_Resize);
                IntializeListViewMoneyDataDetailsColumnsWidth();
                //IntializeListAccountListViewReport_ColumnsWidth();
                AdjustmentDatagridviewColumnsWidth();
                this.tabPage1.Resize += new System.EventHandler(this.tabPage1_Resize);
            }
            catch (Exception ee)
            {
                MessageBox.Show("ContactForm_Load:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }


    }
}
