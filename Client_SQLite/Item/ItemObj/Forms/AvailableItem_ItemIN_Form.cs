using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.ItemObj.Objects;
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

namespace OverLoad_Client.ItemObj.Forms
{
    public partial class AvailableItem_ItemIN_Form : Form
    {
        System.Windows.Forms.MenuItem OpenItemIN;
        Item _Item;
        DatabaseInterface DB;
        private ItemIN _ReturnItemIN;
        bool GetItemIN;
        public ItemIN ReturnItemIN
        {
            get { return _ReturnItemIN; }
        }
        List <ItemIN_AvailableAmount_Report > AvailbeItems_ItemINList;
        public AvailableItem_ItemIN_Form(DatabaseInterface db,Item Item_,bool GetItemIN_)
        {
            InitializeComponent();
            GetItemIN = GetItemIN_;
            DB = db;
            _Item = Item_;
            if (GetItemIN) Select.Visible = true;
            else Select.Visible = false;
            OpenItemIN = new System.Windows.Forms.MenuItem("عرض التفاصيل", OpenItemIN_MenuItem_Click);
            ConfigureListviewColumns();
            this.listView1.Resize += new System.EventHandler(this.listView1_Resize);

        }

        private void OpenItemIN_MenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            string id_s = listView1.SelectedItems[0].Name;

            try
            {
                

                ItemIN ItemIN_ = new ItemINSQL(DB).GetItemININFO_BYID (Convert.ToUInt32(id_s));
                Currency Currency_;
                switch (ItemIN_ ._Operation .OperationType )
                {
                    case Operation.BILL_BUY:
                        Bill bill = new BillBuySQL(DB).GetBillBuy_INFO_BYID(ItemIN_._Operation.OperationID);
                        Currency_ = new Currency(bill._Currency.CurrencyID, bill._Currency.CurrencyName
                            , bill._Currency.CurrencySymbol, bill.ExchangeRate, bill._Currency.ReferenceCurrencyID, bill._Currency.Disable );
                        break;
                    default:
                        Currency_=ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
                        break;
                }
                Trade.Forms.TradeForms.ItemINForm ItemINForm_ = new Trade.Forms.TradeForms.ItemINForm(DB,  ItemIN_, false);
                ItemINForm_.Show ();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

   

        private void AvailableItenForm_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxItemID.Text  = _Item .ItemID . ToString ();
                textBoxItemName.Text  = _Item.ItemName;
                textBoxItemCompany.Text  =_Item .ItemCompany ;
                textBoxItemFolder.Text = _Item.folder.FolderName ;
                AvailbeItems_ItemINList = new AvailableItemSQL (DB).Get_ItemIN_AvailableAmount_Report_List ().Where (x=>x.ItemID == _Item .ItemID ).ToList ();
       
                FillComboBox_TradeState_Folder();
                RefreshItemINs();
            }
            catch
            {
                MessageBox.Show("فشل تحميل عمليات الشراء ","",MessageBoxButtons.OK ,MessageBoxIcon.Error );
                this.Close();
            }
        }

        private void FillComboBox_TradeState_Folder()
        {

            IEnumerable<string> distincTradeState = AvailbeItems_ItemINList.Select(x => x  .TradeStateName ).Distinct();
            comboBoxTradeStateFilter.Items.Clear();
            comboBoxTradeStateFilter.Items.Add("الكل");
            foreach (var s in distincTradeState)
                comboBoxTradeStateFilter.Items.Add(s);
            comboBoxTradeStateFilter.SelectedIndex = 0;
        }
        private void comboBoxTradeStateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ItemIN_AvailableAmount_Report> filteredlist_byTradeState = new List<ItemIN_AvailableAmount_Report>();
            if (comboBoxTradeStateFilter .SelectedIndex == 0)
                filteredlist_byTradeState = AvailbeItems_ItemINList;
            else
                filteredlist_byTradeState = AvailbeItems_ItemINList.Where(item => item.TradeStateName== comboBoxTradeStateFilter .SelectedItem.ToString()).ToList();
            IEnumerable<uint > distinctSource = filteredlist_byTradeState.Select(x => x.OperationType).Distinct();
            comboBoxSourceFilter.Items.Clear();
            comboBoxSourceFilter.Items.Add("الكل");
            foreach (var s in distinctSource)
                comboBoxSourceFilter.Items.Add(Operation .GetOperationName (s));
            comboBoxSourceFilter.SelectedIndex = 0;
        }
        private void RefreshItemINs()
        {
           try
            {
                listView1.Items.Clear();
                for(int i=0;i< AvailbeItems_ItemINList.Count;i++)
                {
                    if (comboBoxTradeStateFilter.SelectedIndex > 0 && AvailbeItems_ItemINList[i] .TradeStateName  != comboBoxTradeStateFilter.SelectedItem.ToString()) continue;
                    if (comboBoxSourceFilter.SelectedIndex > 0 && Operation .GetOperationName ( AvailbeItems_ItemINList[i].OperationType ) != comboBoxSourceFilter.SelectedItem.ToString()) continue;
                    ListViewItem ListViewItem_ = new ListViewItem(AvailbeItems_ItemINList[i].TradeStateName.ToString());
                    ListViewItem_.Name = AvailbeItems_ItemINList[i] .ItemINID .ToString();
                    ListViewItem_.SubItems.Add(AvailbeItems_ItemINList[i].ItemINID .ToString());
                    ListViewItem_.SubItems.Add(Operation .GetOperationName ( AvailbeItems_ItemINList[i] .OperationType) );
                    ListViewItem_.SubItems.Add(AvailbeItems_ItemINList[i].OperationID .ToString());
                    ListViewItem_.SubItems.Add(AvailbeItems_ItemINList[i].ConsumeUnit .ToString());
                    ListViewItem_.SubItems.Add(AvailbeItems_ItemINList[i].Amount  .ToString());
                    ListViewItem_.SubItems.Add((AvailbeItems_ItemINList[i].Amount -AvailbeItems_ItemINList[i].AvailableAmount ).ToString());
                    ListViewItem_.SubItems.Add(AvailbeItems_ItemINList[i].AvailableAmount.ToString());
                    ListViewItem_.BackColor = Color.LightSteelBlue;
                    listView1.Items.Add(ListViewItem_);

                }
            }

              catch
            {
                MessageBox.Show("حصل خطأ اثناء تحديث القائمة","",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItemINs();
        }

        //private void comboBoxFolderFilter_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    List<PlaceAvailbeItems_ItemDetails> filteredlist_byFolder = new List<PlaceAvailbeItems_ItemDetails>();
        //    if (comboBoxFolderFilter.SelectedIndex == 0)
        //        filteredlist_byFolder = StoredItems;
        //    else
        //        filteredlist_byFolder = StoredItems.Where(item => item.ItemFolder == comboBoxFolderFilter.SelectedItem.ToString ()).ToList ();
        //    IEnumerable<string> distinctcompanies = filteredlist_byFolder.Select(x => x.ItemCompany).Distinct();
        //    comboBoxCompanyFilter.Items.Clear();
        //    comboBoxCompanyFilter.Items.Add("الكل");
        //    foreach (var s in distinctcompanies)
        //        comboBoxCompanyFilter.Items.Add(s);
        //    comboBoxCompanyFilter.SelectedIndex = 0;
        //}
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
            {
                if (GetItemIN) SetItemIN();
                else
                {
                    OpenItemIN.PerformClick();

                }


            }
        }
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
    
            if (listView1.SelectedItems.Count > 0)
            {

                if (e.KeyData==Keys .Enter)
                {
                    if (GetItemIN) SetItemIN();
                    else OpenItemIN.PerformClick();

                }

            }
        }
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                List<MenuItem> MenuItemList = new List<MenuItem>();
                listView1.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                foreach (ListViewItem item1 in listView1.Items)
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

                        MenuItem[] mi1 = new MenuItem[] { OpenItemIN };
                        MenuItemList.AddRange(mi1);

                }
                //////////////

                listView1.ContextMenu = new ContextMenu(MenuItemList.ToArray());

            }

        }
        private void Select_Click(object sender, EventArgs e)
        {
            //if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].SubItems[1].Text == "مكان تخزين")
            //{
                SetItemIN();
            //}
        }
        public void SetItemIN()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                _ReturnItemIN = new ItemINSQL (DB).GetItemININFO_BYID((Convert.ToUInt32(listView1.SelectedItems[0].SubItems[0].Name)));
                if (_ReturnItemIN != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else MessageBox.Show("حصل خطأ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else        
                 MessageBox.Show("يرجى اختيار عملية شراء", "", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void comboBoxSourceFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItemINs();
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            ConfigureListviewColumns();
        }
        private void ConfigureListviewColumns()
        {
            int columnswidth = (listView1.Width / listView1.Columns.Count) - 1; ;
            for (int i = 0; i < listView1.Columns.Count; i++)
                listView1.Columns[i].Width = columnswidth;
        }
    }
}
