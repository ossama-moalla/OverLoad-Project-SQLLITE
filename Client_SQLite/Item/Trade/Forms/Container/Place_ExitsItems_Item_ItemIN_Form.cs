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

namespace OverLoad_Client.Trade.Forms.Container
{
    public partial class Place_ExitsItems_Item_ItemIN_Form : Form
    {
        TradeStorePlace place;
        System.Windows.Forms.MenuItem OpenItemIN;
        Item _Item;
        DatabaseInterface DB;
        List <TradeItemStore_Report > TradeItemStore_Report_List;
        private ItemIN _ReturnItemIN;
        bool GetItemIN;
        public ItemIN ReturnItemIN
        {
            get { return _ReturnItemIN; }
        }
        public Place_ExitsItems_Item_ItemIN_Form(DatabaseInterface db,TradeStorePlace Place_,Item Item_,bool GetItemIN_)
        {
            InitializeComponent();
            DB = db;
            OpenItemIN = new System.Windows.Forms.MenuItem("عرض تفاصيل عملية الادخال", OpenItemIN_MenuItem_Click);

            GetItemIN = GetItemIN_;
            if (GetItemIN) Select.Visible = true;
            place = Place_;
            _Item = Item_;
            if (GetItemIN) Select.Visible = true;
            else Select.Visible = false;
            ConfigureListviewColumns();
        }

        private void OpenItemIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemIN ItemIN_ = new ItemINSQL(DB).GetItemININFO_BYID(Convert.ToUInt32(listView1.SelectedItems[0].Name));
                TradeForms.ItemINForm ItemINInfoForm_ = new TradeForms.ItemINForm(DB, ItemIN_, false);
                ItemINInfoForm_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemIN_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void PlaceForm_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxPlaceID.Text  = place.PlaceID.ToString ();
                textBoxPlaceName.Text  = place.PlaceName;
                textBoxPlaceLocation.Text  = place._TradeStoreContainer.ContainerName;
                textBoxDesc.Text = place.Desc;
                textBoxItemName.Text = _Item.ItemName;
                textBoxItemCompany.Text = _Item.ItemCompany ;
                textBoxItemType.Text = _Item.folder .FolderName ;
                TradeItemStore_Report_List = new TradeItemStoreSQL(DB).Get_TradeItemStore_Report_List().Where (x=>x.ItemID ==_Item.ItemID  &&x.PlaceID ==place.PlaceID).ToList ();
                FillComboBox_TradeState();
                FillComboBox_ItemINSource();
                RefreshList();
            }
            catch
            {
                MessageBox.Show("فشل تحميل صفحة مكان التخزين","",MessageBoxButtons.OK ,MessageBoxIcon.Error );
                this.Close();
            }
        }
   
        private void FillComboBox_TradeState()
        {
            try
            { 
            }
            catch(Exception ee)
            {
                MessageBox.Show(":"+ee.Message ,"",MessageBoxButtons.OK ,MessageBoxIcon.Error );
             }

    IEnumerable<string> distincTradeStates = TradeItemStore_Report_List
            .Select(x => x.TradeStateName ).Distinct();
            comboBoxTradeStateFilter .Items.Clear();
            comboBoxTradeStateFilter.Items.Add("الكل");
            foreach (var s in distincTradeStates)
                comboBoxTradeStateFilter.Items.Add(s);
            comboBoxTradeStateFilter.SelectedIndex = 0;
        }
        private void FillComboBox_ItemINSource()
        {
            try
            {
                IEnumerable<string> distincTradeStates = TradeItemStore_Report_List
   .Select(x => Operation.GetOperationName(x.Source_OperationType)).Distinct();
                comboBoxSourceOperationFilter.Items.Clear();
                comboBoxSourceOperationFilter.Items.Add("الكل");
                foreach (var s in distincTradeStates)
                    comboBoxSourceOperationFilter.Items.Add(s);
                comboBoxSourceOperationFilter.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBox_ItemINSource:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void RefreshList()
        {
           try
            {
                listView1.Items.Clear();
                for(int i=0;i<TradeItemStore_Report_List .Count;i++)
                {

                    if (comboBoxTradeStateFilter.SelectedIndex > 0 && TradeItemStore_Report_List[i].TradeStateName != comboBoxTradeStateFilter.SelectedItem.ToString()) continue;
                    if (comboBoxSourceOperationFilter.SelectedIndex > 0 && Operation.GetOperationName(TradeItemStore_Report_List[i].Source_OperationType) != comboBoxSourceOperationFilter.SelectedItem.ToString()) continue;
                    ListViewItem ListViewItem_ = new ListViewItem(TradeItemStore_Report_List[i].TradeStateName);
                    ListViewItem_.Name = TradeItemStore_Report_List[i].ItemSourceOPR_ID.ToString();
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_List[i].ItemSourceOPR_ID.ToString());
                    ListViewItem_.SubItems.Add(Operation.GetOperationName(TradeItemStore_Report_List[i].Source_OperationType ));
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_List[i].Source_OperationID.ToString());
                        
                    

                    ListViewItem_.SubItems.Add(TradeItemStore_Report_List[i].ConsumUnitName );
                    double StoreAmount = TradeItemStore_Report_List[i].AvailableAmount + TradeItemStore_Report_List[i].SpentAmount ;
                    ListViewItem_.SubItems.Add(StoreAmount.ToString());
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_List[i].SpentAmount .ToString());
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_List[i].AvailableAmount.ToString());
                    ListViewItem_.BackColor = Color.LightSteelBlue;
                    listView1.Items.Add(ListViewItem_);
                }
            }

              catch(Exception ee)
            {
                MessageBox.Show("RefreshList:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void comboBoxTradeStateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void comboBoxContactFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshList();
        }
        private void Select_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].SubItems[1].Text == "مكان تخزين")
            {
                ReturItemIN();
            }
        }
        public void ReturItemIN()
        {
            try
            {
                _ReturnItemIN = new ItemINSQL(DB).GetItemININFO_BYID((Convert.ToUInt32(listView1.SelectedItems[0].SubItems[0].Name)));
                if (_ReturnItemIN != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else MessageBox.Show("حصل خطأ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ee)
            {
                MessageBox.Show("ReturItemIN:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 

        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
                {

                    if (!GetItemIN)
                    {

                        uint id = Convert.ToUInt32(listView1.SelectedItems[0].Name);

                        //if (s.Substring(0, 1) == "A")
                        //{
                        //    Maintenance.Objects.MaintenanceOPR_Accessory Accessory_ = new Maintenance.MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(id);
                        //    Maintenance.Forms.MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new Maintenance.Forms.MaintenanceAccessoryForm(DB, Accessory_, false);
                        //    MaintenanceAccessoryForm_.ShowDialog();
                        //}
                        //else if (s.Substring(0, 1) =="M")
                        //{
                        //    Maintenance.Objects.MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(id);
                        //    Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, false);
                        //    MaintenanceOPRForm_.ShowDialog();

                        //}
                        //else
                        //{
                        ItemIN ItemIN_ = new ItemINSQL(DB).GetItemININFO_BYID(id);
                        TradeForms.ItemINForm BuyOprInfoForm_ = new TradeForms.ItemINForm(DB, ItemIN_, false);
                        BuyOprInfoForm_.ShowDialog();

                        //}

                    }
                    else ReturItemIN();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {

                    if (e.KeyData == Keys.Enter)
                    {
                        if (!GetItemIN)
                        {

                            string s = listView1.SelectedItems[0].Name;
                            //uint id = Convert.ToUInt32(s.Substring(1));

                            //if (s.Substring(0, 1) == "A")
                            //{
                            //    Maintenance.Objects.MaintenanceOPR_Accessory Accessory_ = new Maintenance.MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(id);
                            //    Maintenance.Forms.MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new Maintenance.Forms.MaintenanceAccessoryForm(DB, Accessory_, false);
                            //    MaintenanceAccessoryForm_.ShowDialog();
                            //}
                            //else if (s.Substring(0, 1) == "M")
                            //{
                            //    Maintenance.Objects.MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(id);
                            //    Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, false);
                            //    MaintenanceOPRForm_.ShowDialog();

                            //}
                            //else
                            //{
                            ItemIN ItemIN_ = new ItemINSQL(DB).GetItemININFO_BYID(Convert.ToUInt32(s));
                            TradeForms.ItemINForm BuyOprInfoForm_ = new TradeForms.ItemINForm(DB, ItemIN_, false);
                            BuyOprInfoForm_.ShowDialog();

                            //}
                        }
                        else ReturItemIN();

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_KeyDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void ConfigureListviewColumns()
        {
            int columnswidth = (listView1.Width / listView1.Columns.Count) - 1; ;
            for (int i = 0; i < listView1.Columns.Count; i++)
                listView1.Columns[i].Width = columnswidth;
        }
        private void listView1_Resize(object sender, EventArgs e)
        {
            ConfigureListviewColumns();
        }
    }
}
