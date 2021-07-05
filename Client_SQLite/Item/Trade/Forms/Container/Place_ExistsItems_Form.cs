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
    public partial class Place_ExistsItems_Form : Form
    {
        System.Windows.Forms.MenuItem ShowSources;
        System.Windows.Forms.MenuItem OpenItem;
        System.Windows.Forms.MenuItem OpenMaintenanceOPR;
        System.Windows.Forms.MenuItem EditMaintenanceOPR;
        System.Windows.Forms.MenuItem OpenMaintenanceItem;

        TradeStorePlace place;
        DatabaseInterface DB;
        private Item  _ReturnItem;
        bool GetItem;
        public Item ReturnItem
        {
            get { return _ReturnItem; }
        }
        List<TradeItemStore_Report> TradeItemStore_Report_Trade_List;
        List<TradeItemStore_Report> TradeItemStore_Report_Maintenance_List;

        List<TradeState> tradestateList;
        public Place_ExistsItems_Form(DatabaseInterface db,TradeStorePlace Place_,bool GetItem_)
        {
            try
            {
                InitializeComponent();
                DB = db;
                GetItem = GetItem_;
                place = Place_;
                tradestateList = new TradeStateSQL(DB).GetTradeStateList();
                for (int j = 0; j < tradestateList.Count; j++)
                {
                    listViewTrade.Columns.Add(tradestateList[j].TradeStateName, 125);
                }
                List<TradeItemStore_Report> TradeItemStore_Report_List
                = new TradeItemStoreSQL(DB).Get_TradeItemStore_Report_List().Where(x => x.PlaceID == place.PlaceID).ToList();
                TradeItemStore_Report_Trade_List = TradeItemStore_Report_List.Where(x => x.StoreType == TradeItemStore_Report.ITEMIN_STORE_TYPE).ToList();
                TradeItemStore_Report_Maintenance_List = TradeItemStore_Report_List.Where(x => x.StoreType != TradeItemStore_Report.ITEMIN_STORE_TYPE).ToList();
                FillComboBox_Trade_Company_Folder();
                RefreshStoredItems();
               

               
                if (GetItem)
                {
                    Select.Visible = true;
                    tabControl1.TabPages[1].Hide();
                }
                else
                {

                    LoadMaintenanceData();
                    Select.Visible = false; 
                }
                

                ShowSources = new System.Windows.Forms.MenuItem("استعراض المصادر", ShowSources_MenuItem_Click);
                OpenItem = new System.Windows.Forms.MenuItem("فتح صفحة العنصر", OpenItem_MenuItem_Click);
                OpenMaintenanceOPR = new System.Windows.Forms.MenuItem("استعراض عملية الصيانة", OpenMaintenanceOPR_MenuItem_Click);
                EditMaintenanceOPR = new System.Windows.Forms.MenuItem("تعديل", EditMaintenanceOPR_MenuItem_Click);
                OpenMaintenanceItem = new System.Windows.Forms.MenuItem("فتح صفحة العنصر", Open_Maintenance_Item_MenuItem_Click);

            }
            catch (Exception ee)
            {
                throw new Exception("Place_ExistsItems_Form:"+ee.Message );
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
           
                //ConfigureListViewColumns();
                //this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
                
            }
            catch(Exception ee)
            {
                throw new Exception ("PlaceForm_Load:"+ee.Message );

            }
        }

        #region Trade

        private void OpenItem_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string id_s = listViewTrade.SelectedItems[0].Name;


                Item item = new ItemObj.ItemObjSQL.ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(id_s));
                OverLoad_Client.ItemObj.Forms.ItemForm form = new OverLoad_Client.ItemObj.Forms.ItemForm(DB, item);
                form.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItem_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ShowSources_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string id_s = listViewTrade.SelectedItems[0].Name;


                Item item = new ItemObj.ItemObjSQL.ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(id_s));
                Place_ExitsItems_Item_ItemIN_Form form = new Place_ExitsItems_Item_ItemIN_Form(DB, place, item, false);
                form.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("ShowSources_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        private void FillComboBox_Trade_Company_Folder()
        {
            try
            {
                IEnumerable<string> distincfolders = TradeItemStore_Report_Trade_List.Select(x => x.FolderName).Distinct();
                comboBoxTradeFolderFilter.Items.Clear();
                comboBoxTradeFolderFilter.Items.Add("الكل");
                foreach (var s in distincfolders)
                    comboBoxTradeFolderFilter.Items.Add(s);
                comboBoxTradeFolderFilter.SelectedIndex = 0;

                IEnumerable<string> distinctcompanies = TradeItemStore_Report_Trade_List.Select(x => x.ItemCompany).Distinct();
                comboBoxTradeCompanyFilter.Items.Clear();
                comboBoxTradeCompanyFilter.Items.Add("الكل");
                foreach (var s in distinctcompanies)
                    comboBoxTradeCompanyFilter.Items.Add(s);
                comboBoxTradeCompanyFilter.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBox_Trade_Company_Folder:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           
        }

        private void RefreshStoredItems()
        {
           try
            {
                listViewTrade.Items.Clear();
                List<uint> ItemID_List = TradeItemStore_Report_Trade_List.Select(x => x.ItemID).Distinct().ToList();
                for (int i=0;i< ItemID_List.Count;i++)
                {
                    List<TradeItemStore_Report> TradeItemStore_Report_Trade_List_Item = TradeItemStore_Report_Trade_List.Where(x => x.ItemID == ItemID_List[i]).ToList();
                    if (comboBoxTradeCompanyFilter.SelectedIndex > 0 && TradeItemStore_Report_Trade_List_Item[0] .ItemCompany != comboBoxTradeCompanyFilter.SelectedItem.ToString()) continue;
                    if (comboBoxTradeFolderFilter.SelectedIndex > 0 && TradeItemStore_Report_Trade_List_Item[0] .FolderName != comboBoxTradeFolderFilter.SelectedItem.ToString()) continue;

                    ListViewItem ListViewItem_ = new ListViewItem(TradeItemStore_Report_Trade_List_Item[0].ItemID .ToString ());
                    ListViewItem_.Name = TradeItemStore_Report_Trade_List_Item[0].ItemID.ToString();
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_Trade_List_Item[0].ItemName .ToString());
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_Trade_List_Item[0].ItemCompany .ToString());
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_Trade_List_Item[0] .FolderName  .ToString());
                  
                    for (int j = 0; j < tradestateList.Count; j++)
                    {

                        double a_v = TradeItemStore_Report_Trade_List_Item.Where(x => x.TradeStateID== tradestateList[j].TradeStateID  ).Sum(y => y.AvailableAmount);
                        ListViewItem_.SubItems.Add(a_v.ToString());
                    }

                    ListViewItem_.BackColor = Color.LightSteelBlue  ;
                    listViewTrade.Items.Add(ListViewItem_);
                    
                }
            }

              catch
            {
                MessageBox.Show("حصل خطأ اثناء تحديث القائمة","",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

 
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && listViewTrade.SelectedItems.Count > 0)
                {
                    if (!GetItem)
                        ShowSources.PerformClick();
                    else ReturnItemDialog();

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
                if (listViewTrade.SelectedItems.Count > 0)
                {

                    if (e.KeyData == Keys.Enter)
                    {
                        if (!GetItem)
                            ShowSources.PerformClick();
                        else ReturnItemDialog();

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
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    List<MenuItem> MenuItemList = new List<MenuItem>();
                    listViewTrade.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();
                    foreach (ListViewItem item1 in listViewTrade.Items)
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

                        MenuItem[] mi1 = new MenuItem[] { ShowSources, OpenItem };
                        MenuItemList.AddRange(mi1);

                    }
                    //////////////

                    listViewTrade.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void Select_Click(object sender, EventArgs e)
        {
            if (listViewTrade.SelectedItems.Count > 0 && listViewTrade.SelectedItems[0].SubItems[1].Text == "مكان تخزين")
            {
                ReturnItemDialog();
            }
        }
        private  void ReturnItemDialog()
        {
            try
            {
                _ReturnItem = new ItemObj.ItemObjSQL.ItemSQL(DB).GetItemInfoByID((Convert.ToUInt32(listViewTrade.SelectedItems[0].SubItems[0].Name)));
                if (_ReturnItem != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else MessageBox.Show("حصل خطأ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ee)
            {
                MessageBox.Show("ReturnItemDialog:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        #endregion

        #region Maintenance
        private void LoadMaintenanceData()
        {
            FillComboBox_Maintenance_Company_Folder();
            RefreshMaintenanceItems();
        }
        private void FillComboBox_Maintenance_Company_Folder()
        {
            try
            {

                List<string> distincfolders =
               TradeItemStore_Report_Maintenance_List.Select(x => x.FolderName).Distinct().ToList();
                //distincfolders.AddRange(TradeItemStore_Report_List.Where(y => y._TradeItemStore.StoreType == TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE).Select(x => x._TradeItemStore._MaintenanceOPR_Accessory._Item.folder.FolderName).Distinct());

                comboBoxMaintenanceFolderFilter.Items.Clear();
                comboBoxMaintenanceFolderFilter.Items.Add("الكل");

                foreach (var s in distincfolders)
                    comboBoxMaintenanceFolderFilter.Items.Add(s);
                comboBoxMaintenanceFolderFilter.SelectedIndex = 0;

                IEnumerable<string> distinctcompanies =
     TradeItemStore_Report_Maintenance_List.Select(x => x.ItemCompany).Distinct().ToList();
                //distincfolders.AddRange(TradeItemStore_Report_List.Where(y => y._TradeItemStore.StoreType == TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE).Select(x => x._TradeItemStore._MaintenanceOPR_Accessory._Item.ItemCompany).Distinct());

                comboBoxMaintenanceCompanyFilter.Items.Clear();
                comboBoxMaintenanceCompanyFilter.Items.Add("الكل");
                foreach (var s in distinctcompanies)
                    comboBoxMaintenanceCompanyFilter.Items.Add(s);
                comboBoxMaintenanceCompanyFilter.SelectedIndex = 0;

            }
            catch (Exception ee)
            {
                MessageBox.Show ("FillComboBox_Maintenance_Company_Folder:"+ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error  );
            }
           
        }
        private void RefreshMaintenanceItems()
        {
            try
            {
                listViewMaintenance .Items.Clear();
                for (int i = 0; i < TradeItemStore_Report_Maintenance_List.Count; i++)
                {
                    if (comboBoxMaintenanceFolderFilter.SelectedIndex > 0 && TradeItemStore_Report_Maintenance_List[i].FolderName != comboBoxMaintenanceFolderFilter.SelectedItem.ToString()) continue;
                    if (comboBoxMaintenanceCompanyFilter.SelectedIndex > 0 && TradeItemStore_Report_Maintenance_List[i].ItemCompany != comboBoxMaintenanceCompanyFilter.SelectedItem.ToString()) continue;

                    ListViewItem ListViewItem_;
                    if (TradeItemStore_Report_Maintenance_List[i].StoreType == TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE)
                    {

                        ListViewItem_ = new ListViewItem("عنصر صيانة");
                    }
                    else if (TradeItemStore_Report_Maintenance_List[i].StoreType == TradeItemStore.MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE)
                    {
                        ListViewItem_ = new ListViewItem("ملحق صيانة");

                    }
                    else continue;
                    ListViewItem_.Name = TradeItemStore_Report_Maintenance_List[i].ItemSourceOPR_ID.ToString();

                 ListViewItem_.SubItems.Add(TradeItemStore_Report_Maintenance_List[i].ItemID.ToString());
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_Maintenance_List[i].ItemName.ToString());
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_Maintenance_List[i].ItemCompany.ToString());
                    ListViewItem_.SubItems.Add(TradeItemStore_Report_Maintenance_List[i].FolderName.ToString());

                    ListViewItem_.BackColor = Color.LightSteelBlue;
                    listViewMaintenance.Items.Add(ListViewItem_);

                }
         
            }

            catch(Exception ee)
            {
                MessageBox.Show("RefreshMaintenanceItems:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listViewMaintenance_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && listViewMaintenance.SelectedItems.Count > 0)
            {
                if (!GetItem)
                    ShowSources.PerformClick();
                else ReturnItemDialog();

            }
        }
        private void listViewMaintenance_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (listViewMaintenance.SelectedItems.Count > 0)
                {

                    if (e.KeyData == Keys.Enter)
                    {

                        OpenMaintenanceOPR.PerformClick();


                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewMaintenance_KeyDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        private void listViewMaintenance_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    List<MenuItem> MenuItemList = new List<MenuItem>();
                    listViewMaintenance.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();
                    foreach (ListViewItem item1 in listViewMaintenance.Items)
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

                        MenuItem[] mi1 = new MenuItem[] { OpenMaintenanceOPR, EditMaintenanceOPR, OpenItem };
                        MenuItemList.AddRange(mi1);

                    }
                    //////////////

                    listViewMaintenance.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewMaintenance_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        private void Open_Maintenance_Item_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string id_s = listViewMaintenance.SelectedItems[0].Name;


                Maintenance.Objects.MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(id_s));
                OverLoad_Client.ItemObj.Forms.ItemForm form = new OverLoad_Client.ItemObj.Forms.ItemForm(DB, MaintenanceOPR_._Item);
                form.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_Maintenance_Item_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

 

        }
        private void OpenMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string id_s = listViewMaintenance.SelectedItems[0].Name;


                Maintenance.Objects.MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(id_s));
                Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, false);
                MaintenanceOPRForm_.ShowDialog();
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
                string id_s = listViewMaintenance.SelectedItems[0].Name;


                Maintenance.Objects.MaintenanceOPR MaintenanceOPR_ = new Maintenance.MaintenanceSQL.MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(id_s));
                Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, true);
                MaintenanceOPRForm_.ShowDialog();
                if (MaintenanceOPRForm_.Refresh_ListViewMaintenanceOPRs_Flag || MaintenanceOPRForm_ .Refresh_ListViewMoneyDataDetails_Flag )
                {
                    LoadMaintenanceData();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditMaintenanceOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion

        private void comboBoxMaintenanceFolderFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMaintenanceItems();
        }

        private void comboBoxMaintenanceCompanyFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMaintenanceItems();
        }
        private void comboBoxTradeFolderFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStoredItems();
        }

        private void comboBoxTradeCompanyFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStoredItems();
        }
    }
}
