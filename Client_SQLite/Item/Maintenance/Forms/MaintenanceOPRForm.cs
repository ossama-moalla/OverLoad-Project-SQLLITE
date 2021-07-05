using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
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

namespace OverLoad_Client.Maintenance.Forms
{
    public partial class MaintenanceOPRForm : OverLoad_Form 
    {
        System.Windows.Forms.MenuItem OpenItemOUTMenuItem;
        System.Windows.Forms.MenuItem AddItemOUTMenuItem;
        System.Windows.Forms.MenuItem EditItemOUTMenuItem;
        System.Windows.Forms.MenuItem DeleteItemOUTMenuItem;


        System.Windows.Forms.MenuItem OpenAccessory_MenuItem;
        System.Windows.Forms.MenuItem AddAccessory_MenuItem;
        System.Windows.Forms.MenuItem EditAccessory_MenuItem;
        System.Windows.Forms.MenuItem DeleteAccessory_MenuItem;

        MenuItem OpenDiagnosticOPR_MenuItem;
        MenuItem AddDiagnosticOPR_MenuItem;
        MenuItem UpdateDiagnosticOPR_MenuItem;
        MenuItem DeleteDiagnosticOPR_MenuItem;

        MenuItem OpenFault_MenuItem;
        MenuItem AddFault_MenuItem;
        MenuItem UpdateFault_MenuItem;
        MenuItem DeleteFault_MenuItem;

        MenuItem Open_MissedFault_Item_MenuItem;
        //MenuItem Add_MissedFault_Item_MenuItem;
        MenuItem Update_MissedFault_Item_MenuItem;
        MenuItem Delete_MissedFault_Item_MenuItem;


        System.Windows.Forms.MenuItem OpenRepairOPR_MenuItem;
        System.Windows.Forms.MenuItem AddRepairOPR_MenuItem;
        System.Windows.Forms.MenuItem EditRepairOPR_MenuItem;
        System.Windows.Forms.MenuItem DeleteRepairOPR_MenuItem;

        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        BillMaintenance _BillMaintenance;
        private Contact _Contact;
        Item _Item;
        TradeStorePlace _Place;
        Folder LastUsedFolder;
        DiagnosticOPRSQL DiagnosticOPRSQL_;
        //List<DiagnosticOPRReport> DiagnosticOPRList = new List<DiagnosticOPRReport>();
        //List<MaintenanceFaultReport > FaultReportList = new List<MaintenanceFaultReport>();
        //List<Missed_Fault_Item> Missed_Fault_Item_List = new List<Missed_Fault_Item>();

        //List<ItemOUT> _ItemOUTList = new List<ItemOUT>();
        //List<MaintenanceOPR_Accessory> AccessoryList = new List<MaintenanceOPR_Accessory>();
        //List <RepairOPR > 
        public MaintenanceOPRForm(DatabaseInterface db,Contact Contact_)
        {
            DB = db;
            DiagnosticOPRSQL_ = new DiagnosticOPRSQL(DB);
            _Contact = Contact_;
             InitializeComponent();

            buttonBill_Create .Enabled  = false;
            buttonBill_Edit.Visible = false;
            buttonBill_Delete.Visible = false;
            buttonEndWork_Create.Enabled  = false;
            buttonEndWork_Edit.Visible = false;
            buttonEndWork_Delete.Visible = false;
            panelBIll.Enabled = false;
            panelEndWork.Enabled = false;
            tabControlData.Enabled = false ;
            if (_Contact!=null)
                textBoxContact.Text = _Contact.Get_Complete_ContactName_WithHeader();
            textBoxMaintenanceOPRID.Text = "-";
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            //LoadBillData();
            this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
            this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
            textBoxContact.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxContact_MouseDoubleClick);
            this.textBoxPlaceID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPlaceID_KeyDown);
            this.textBoxPlaceID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPlaceID_MouseDoubleClick);

            InitializeMenuItems();
            DiagnosticOPRReport.InitializeDiagnosticOPRListViewColumns(ref listViewDiagnosticOPR);
            MaintenanceFaultReport.InitializeFaultReportListViewColumns(ref listViewFault);
            Missed_Fault_Item.InitializeMissed_Fault_ListViewColumns(ref listViewMissedFaultItem);
            //AdjustListViewItemsOUTColumnsWidth();
            //panelFaults.Enabled = false;
            //panelDiagnosticOPR.Enabled = false;
        }
   
        public MaintenanceOPRForm(DatabaseInterface db, MaintenanceOPR MaintenanceOPR_, bool Edit)
        {
            DB = db;
            DiagnosticOPRSQL_ = new DiagnosticOPRSQL(DB);
            InitializeComponent();
            _MaintenanceOPR = MaintenanceOPR_;

            //AdjustListViewItemsOUTColumnsWidth();
            InitializeMenuItems();
            LoadForm(Edit);
            //DiagnosticOPRReport.InitializeDiagnosticOPRListViewColumns (ref listViewDiagnosticOPR);
            //MaintenanceFaultReport.InitializeFaultReportListViewColumns(ref listViewFault);
            //Missed_Fault_Item.InitializeMissed_Fault_ListViewColumns(ref listViewMissedFaultItem);

        }
        public void LoadForm(bool Edit)
        {
            try
            {
                if (_MaintenanceOPR == null) return;
                _Contact = _MaintenanceOPR._Contact;
                _Item = _MaintenanceOPR._Item;
                _Place = _MaintenanceOPR.Place;
                _BillMaintenance = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(_MaintenanceOPR);
                textBoxContact.Text = _Contact.Get_Complete_ContactName_WithHeader();

                tabControlData.Enabled = true ;
                panelBIll.Enabled = true ;
                panelEndWork.Enabled = true ;
                LoadItemData();
                LoadBillData(Edit );
                LoadEndWorkData(Edit);
                GetSubData();
            buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
               
                dateTimePickerEntryDate.Value = _MaintenanceOPR.EntryDate;
                textBoxMaintenanceOPRID.Text = _MaintenanceOPR._Operation.OperationID.ToString();
                textBoxItemSerial.Text = _MaintenanceOPR.ItemSerial;
                textBoxFaultDesc.Text = _MaintenanceOPR.FaultDesc;
                textBoxNotes.Text = _MaintenanceOPR.Notes;
                

                if (_MaintenanceOPR.Place != null)
                {

                    textBoxPlaceID.Text = _MaintenanceOPR.Place.PlaceID.ToString();
                    textBoxPlaceID.Name = _MaintenanceOPR.Place.PlaceID.ToString();
                    textBoxPlaceInfo.Text = new TradeStoreContainerSQL(DB).GetContainerPath(_MaintenanceOPR.Place._TradeStoreContainer) + "\\" + _MaintenanceOPR.Place._TradeStoreContainer.ContainerName+"\\"+ _MaintenanceOPR.Place.PlaceName ;
                }

                if (Edit)
                {
                    if (_Place != null)
                        buttonClearstoreinfo.Visible = true;
                    else
                        buttonClearstoreinfo.Visible = false;
                    //this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
                    this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
                    this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
                    this.textBoxPlaceID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPlaceID_KeyDown);
                    this.textBoxPlaceID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPlaceID_MouseDoubleClick);
                    //this.listViewItemOUT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewItemOUT_MouseDoubleClick);
                    //this.listViewItemOUT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewItemOUT_MouseDown);

                    this.listViewDiagnosticOPR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewDiagnosticOPR_MouseDown);
                    this.listViewFault.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewFault_MouseDown);
                    this.listViewMissedFaultItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MissedFault_Item_MouseDown);
                    this.listViewAccessories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewAccessories_MouseDown);
                    this .listViewRepairOPR.MouseDown+= new System.Windows.Forms.MouseEventHandler(this.listViewRepairOPR_MouseDown);

                    this.listViewFault.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewFault_MouseDoubleClick);
                    this.listViewMissedFaultItem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MissedFault_Item_MouseDoubleClick);
                    this.listViewDiagnosticOPR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDiagnosticOPR_MouseDoubleClick);
                    this .listViewAccessories .MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAccessories_MouseDoubleClick);
                    this.listViewRepairOPR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewRepairOPR_MouseDoubleClick);

                }
                else
                {

                    this.listViewFault.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewFault_MouseDoubleClick);
                    this.listViewMissedFaultItem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MissedFault_Item_MouseDoubleClick);
                    this.listViewDiagnosticOPR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDiagnosticOPR_MouseDoubleClick);
                    this.listViewAccessories.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAccessories_MouseDoubleClick);
                    this.listViewRepairOPR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewRepairOPR_MouseDoubleClick);

                    buttonSave.Visible = false;
                    dateTimePickerEntryDate.Enabled = false;
                    textBoxPlaceID.ReadOnly = true;
                    textBoxItemID.ReadOnly = true;
                    textBoxItemSerial.ReadOnly = true;
                    textBoxFaultDesc.ReadOnly = true;
                    textBoxNotes.ReadOnly = true;
                    //comboBoxStatus.Enabled = false;
                    //checkBox1.Enabled = false;
                    //dateTimePickerDeliver.Enabled = false;

                }
            }
  
            catch (Exception ee)
            {
                MessageBox.Show("LoadForm:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }
        public void InitializeMenuItems()
        {
            //OpenItemOUTMenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية التركيب", OpenItemOUT_MenuItem_Click);
            //AddItemOUTMenuItem = new System.Windows.Forms.MenuItem("تركيب عنصر", AddItemOUT_MenuItem_Click);
            //EditItemOUTMenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditItemOUT_MenuItem_Click);
            //DeleteItemOUTMenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteItemOUT_MenuItem_Click);

            OpenAccessory_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", OpenAccessory_MenuItem_Click);
            AddAccessory_MenuItem = new System.Windows.Forms.MenuItem("اضافة ملحق صيانة", AddAccessory_MenuItem_Click);
            EditAccessory_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditAccessory_MenuItem_Click);
            DeleteAccessory_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteAccessory_MenuItem_Click);

            OpenDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية الفحص", OpenDiagnosticOPR_MenuItem_Click);
            AddDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("اضافة عملية فحص فرعية", AddDiagnosticOPR_MenuItem_Click);
            UpdateDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditDiagnosticOPR_MenuItem_Click);
            DeleteDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteDiagnosticOPR_MenuItem_Click);

            OpenFault_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", OpenFault_MenuItem_Click);
            AddFault_MenuItem = new System.Windows.Forms.MenuItem("اضافة عطل", AddFault_MenuItem_Click);
            UpdateFault_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditFault_MenuItem_Click);
            DeleteFault_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteFault_MenuItem_Click);

            Open_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", Open_MissedFault_Item_MenuItem_Click);
            //Add_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("اضافة عنصر مفقود او تالف", Add_MissedFault_Item_MenuItem_Click);
            Update_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_MissedFault_Item_MenuItem_Click);
            Delete_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_MissedFault_Item_MenuItem_Click);

            OpenRepairOPR_MenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية الاصلاح", OpenRepairOPR_MenuItem_Click);
            AddRepairOPR_MenuItem = new System.Windows.Forms.MenuItem("اضافة عملية اصلاح", AddRepairOPR_MenuItem_Click);
            EditRepairOPR_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditRepairOPR_MenuItem_Click);
            DeleteRepairOPR_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteRepairOPR_MenuItem_Click);

        }
        private void textBoxContact_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Trade.Forms.TradeContact.ShowContactsForm form = new Trade.Forms.TradeContact.ShowContactsForm(DB, true);
                    DialogResult dd = form.ShowDialog();
                    if (dd == DialogResult.OK)
                    {
                        _Contact = form.Contact_;
                        textBoxContact.Text = _Contact.GetContactTypeHeader() + ":" + _Contact.ContactName;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxContact_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
     
        public async void GetSubData()
        {
            try
            {
                //_ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_MaintenanceOPR._Operation);
                List<MaintenanceOPR_Accessory>  AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                List<DiagnosticOPRReport>  DiagnosticOPRList = new DiagnosticOPRSQL(DB).GetSubDiagnosticOPRReportList(_MaintenanceOPR, null);
                List<MaintenanceFaultReport> FaultReportList = new MaintenanceFaultSQL(DB).GetMaintenanceOPR_Report_Fault_List(_MaintenanceOPR);
                List <Missed_Fault_Item > Missed_Fault_Item_List = new MissedFaultItemSQL(DB).MaintenanceOPR_GetMissed_Fault_Item_List(_MaintenanceOPR);
                List<RepairOPR> RepairOPRList = new RepairOPRSQL(DB).Get_MaintenanceOPR_RepairOPR_List(_MaintenanceOPR);
                RefreshAccessories(AccessoryList);
                RefreshSubDiagnosticOPRList( DiagnosticOPRList);
                RefreshFaultReportList( FaultReportList);
                RefreshMissed_FaultList( Missed_Fault_Item_List);
                RefreshRepairOPRList(RepairOPRList);
                //RefreshInstalledItems(_ItemOUTList);
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetSubData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (_Item == null)
            {

                MessageBox.Show("يرجى تحديد المادة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_Contact  == null)
            {

                MessageBox.Show("يرجى تحديد الجهة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                   
                    MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).AddMaintenanceOPR
                        (dateTimePickerEntryDate.Value, _Contact.ContactID, _Item.ItemID, textBoxItemSerial.Text
                        , textBoxFaultDesc.Text, textBoxNotes.Text, _Place);
                    if (MaintenanceOPR_ != null)
                    {
                        _MaintenanceOPR = MaintenanceOPR_;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMoneyDataDetails_Flag  = true;
                        this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                        LoadForm(true );

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر انشاء عملية الصيانة " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_MaintenanceOPR  != null)
                    {
                        
                        bool success = new MaintenanceOPRSQL(DB).UpdateMaintenanceOPR
                            (_MaintenanceOPR._Operation.OperationID,dateTimePickerEntryDate.Value, _Contact.ContactID , _Item.ItemID
                            , textBoxItemSerial.Text
                        , textBoxFaultDesc.Text, textBoxNotes.Text, _Place);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _MaintenanceOPR = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(_MaintenanceOPR._Operation.OperationID);
                            this.Refresh_ListViewMoneyDataDetails_Flag = true;
                            this.Refresh_ListViewMaintenanceOPRs_Flag = true; ;
                            LoadForm(true);
                            //LoadForm(true );
                        }
                        else MessageBox.Show("لم يتم الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذرالحفظ  " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }
 
        private void textBoxItemID_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    uint itemid = Convert.ToUInt32(textBoxItemID.Text);
                    Item item__ = new ItemSQL(DB).GetItemInfoByID(itemid);
                    if (item__ != null)
                    {
                        _Item = item__;
                        LoadItemData();
                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على العنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        private void textBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ItemObj.Forms.User_ShowItemsForm SelectItem_ = new ItemObj.Forms.User_ShowItemsForm(DB, LastUsedFolder, ItemObj.Forms.User_ShowItemsForm.SELECT_ITEM);
                    DialogResult dd = SelectItem_.ShowDialog();
                    if (dd == DialogResult.OK)
                    {
                        _Item = SelectItem_.ReturnItem;
                        LoadItemData();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxItem_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         
        }
        private async  void LoadItemData()
        {
            try
            {
                LastUsedFolder = _Item.folder;
                textBoxItemID.Text = _Item.ItemID.ToString();
                textBoxItemName.Text = _Item.ItemName;
                textBoxItemCompany.Text = _Item.ItemCompany;
                textBoxItemType.Text = _Item.folder.FolderName;
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadItemData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        //#region ItemOUT
        //private void listViewItemOUT_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left && listViewItemOUT.SelectedItems.Count > 0)
        //    {
        //        OpenItemOUTMenuItem.PerformClick();
        //    }
        //}
        //private void listViewItemOUT_MouseDown(object sender, MouseEventArgs e)
        //{
        //    listViewItemOUT.ContextMenu = null;
        //    bool match = false;
        //    ListViewItem listitem = new ListViewItem();
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {
        //        foreach (ListViewItem item1 in listViewItemOUT.Items)
        //        {
        //            if (item1.Bounds.Contains(new Point(e.X, e.Y)))
        //            {
        //                match = true;
        //                listitem = item1;
        //                break;
        //            }
        //        }
        //        if (match)
        //        {


        //            MenuItem[] mi1 = new MenuItem[] { OpenItemOUTMenuItem, EditItemOUTMenuItem, DeleteItemOUTMenuItem, new MenuItem("-"), AddItemOUTMenuItem };
        //            listViewItemOUT.ContextMenu = new ContextMenu(mi1);


        //        }
        //        else
        //        {

        //            MenuItem[] mi = new MenuItem[] { AddItemOUTMenuItem };
        //            listViewItemOUT.ContextMenu = new ContextMenu(mi);

        //        }

        //    }

        //}
        //private void listViewItemsOUT_Resize(object sender, EventArgs e)
        //{
        //    AdjustListViewItemsOUTColumnsWidth();
        //}
        //public async  void AdjustListViewItemsOUTColumnsWidth()
        //{
        //    listViewItemOUT.Columns[0].Width = 80;
        //    int w= (listViewItemOUT.Width - 80) / 8 - 1;
        //    listViewItemOUT.Columns[1].Width = w;
        //    listViewItemOUT.Columns[2].Width = w;
        //    listViewItemOUT.Columns[3].Width = w;
        //    listViewItemOUT.Columns[4].Width = w;
        //    listViewItemOUT.Columns[5].Width = w;
        //    listViewItemOUT.Columns[6].Width = w;
        //    listViewItemOUT.Columns[7].Width = w;
        //    listViewItemOUT.Columns[8].Width = w;


        //}
        //private void DeleteItemOUT_MenuItem_Click(object sender, EventArgs e)
        //{

        //    try
        //    {

        //        DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        //        if (dd != DialogResult.OK) return;
        //        uint sid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
        //        bool success = new ItemOUTSQL(DB).DeleteItemOUT(sid);
        //        if (success)
        //        {
        //            MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_MaintenanceOPR ._Operation);
        //            RefreshInstalledItems(_ItemOUTList);

        //        }
        //        else
        //        {
        //            MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        //private void EditItemOUT_MenuItem_Click(object sender, EventArgs e)
        //{
        //    if (listViewItemOUT.SelectedItems.Count > 0)
        //    {
        //        uint itemoutid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
        //        ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
        //        ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, true);
        //        ItemOUTForm_.ShowDialog();
        //        if (ItemOUTForm_.Changed)
        //        {
        //            _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_MaintenanceOPR._Operation);
        //            RefreshInstalledItems(_ItemOUTList);
        //        }
        //        ItemOUTForm_.Dispose();
        //    }
        //}
        //private void OpenItemOUT_MenuItem_Click(object sender, EventArgs e)
        //{
        //    if (listViewItemOUT.SelectedItems.Count > 0)
        //    {

        //        uint itemoutid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
        //        ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
        //        ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, false);
        //        ItemOUTForm_.ShowDialog();
        //        if (ItemOUTForm_.Changed)
        //        {
        //            _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_MaintenanceOPR._Operation);
        //            RefreshInstalledItems(_ItemOUTList);
        //        }
        //        ItemOUTForm_.Dispose();
        //    }
        //}
        //private async void RefreshInstalledItems(List<ItemOUT> ItemOUTList)
        //{

        //    listViewItemOUT.Items.Clear();
        //    double totalcost = 0;
        //    for (int i = 0; i < ItemOUTList.Count; i++)
        //    {
        //        double sellprice = ItemOUTList[i]._OUTValue .Value ;
        //        double total_sellprice = System.Math.Round(sellprice * ItemOUTList[i].Amount, 2);
        //        totalcost = totalcost + total_sellprice;
        //        ListViewItem ListViewItem_ = new ListViewItem((listViewItemOUT.Items.Count + 1).ToString());
        //        ListViewItem_.Name = ItemOUTList[i].ItemOUTID.ToString();
        //        ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemName);
        //        ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemCompany);
        //        ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.folder.FolderName);
        //        ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._TradeState.TradeStateName);
        //        ListViewItem_.SubItems.Add(ItemOUTList[i].Amount.ToString());
        //        ListViewItem_.SubItems.Add(ItemOUTList[i]._ConsumeUnit.ConsumeUnitName.ToString());


        //        ListViewItem_.SubItems.Add(sellprice.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol.Replace(" ", string.Empty));
        //        ListViewItem_.SubItems.Add((total_sellprice).ToString() + " " + _BillMaintenance._Currency.CurrencySymbol.Replace(" ", string.Empty));
        //        ListViewItem_.SubItems.Add(ItemOUTList[i].Notes);
        //        ListViewItem_.BackColor = Color.Orange;
        //        listViewItemOUT.Items.Add(ListViewItem_);

        //    }


        //}

        //private void AddItemOUT_MenuItem_Click(object sender, EventArgs e)
        //{
        //    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _MaintenanceOPR._Operation);
        //    DialogResult d = ItemOUTForm_.ShowDialog();
        //    if (ItemOUTForm_.Changed)
        //    {
        //        _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_MaintenanceOPR._Operation);
        //        RefreshInstalledItems(_ItemOUTList);
        //    }
        //    ItemOUTForm_.Dispose();
        //}
        //#endregion
        #region AccessoryRegion
        private void DeleteAccessory_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewAccessories.SelectedItems.Count == 0) return;
                    DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewAccessories .SelectedItems[0].Name);
                bool success = new MaintenanceAccessorySQL(DB).DeleteAccessory (sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List <MaintenanceOPR_Accessory > AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                    RefreshAccessories(AccessoryList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("DeleteAccessory_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void EditAccessory_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewAccessories.SelectedItems.Count > 0)
                {
                    uint accessoryid = Convert.ToUInt32(listViewAccessories.SelectedItems[0].Name);
                    MaintenanceOPR_Accessory accessory = new MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(accessoryid);
                    MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new MaintenanceAccessoryForm(DB, accessory, true);
                    MaintenanceAccessoryForm_.ShowDialog();
                    if (MaintenanceAccessoryForm_.Changed)
                    {
                        List<MaintenanceOPR_Accessory> AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                        RefreshAccessories(AccessoryList);
                    }
                    MaintenanceAccessoryForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditAccessory_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void OpenAccessory_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewAccessories.SelectedItems.Count > 0)
                {

                    uint accessoryid = Convert.ToUInt32(listViewAccessories.SelectedItems[0].Name);
                    MaintenanceOPR_Accessory accessory = new MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(accessoryid);
                    MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new MaintenanceAccessoryForm(DB, accessory, false);
                    MaintenanceAccessoryForm_.ShowDialog();
                    if (MaintenanceAccessoryForm_.Changed)
                    {
                        List<MaintenanceOPR_Accessory> AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                        RefreshAccessories(AccessoryList);
                    }
                    MaintenanceAccessoryForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenAccessory_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void AddAccessory_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new MaintenanceAccessoryForm(DB, _MaintenanceOPR);
                DialogResult d = MaintenanceAccessoryForm_.ShowDialog();
                if (MaintenanceAccessoryForm_.Changed)
                {
                    List<MaintenanceOPR_Accessory> AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                    RefreshAccessories(AccessoryList);
                }
                MaintenanceAccessoryForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddAccessory_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private async void RefreshAccessories(List<MaintenanceOPR_Accessory> AccessoriesList_)
        {
            try
            {
                listViewAccessories.Items.Clear();

                for (int i = 0; i < AccessoriesList_.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem((listViewAccessories.Items.Count + 1).ToString());
                    ListViewItem_.Name = AccessoriesList_[i].AccessoryID.ToString();
                    ListViewItem_.SubItems.Add(AccessoriesList_[i]._Item.ItemName);
                    ListViewItem_.SubItems.Add(AccessoriesList_[i]._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(AccessoriesList_[i]._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(AccessoriesList_[i].ItemSerialNumber);
                    if (AccessoriesList_[i].Place == null)
                    {
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");
                    }
                    else
                    {
                        ListViewItem_.SubItems.Add(AccessoriesList_[i].Place.PlaceName);
                        ListViewItem_.SubItems.Add(AccessoriesList_[i].Place.PlaceID.ToString());
                        ListViewItem_.SubItems.Add(new Trade.TradeSQL.TradeStorePlaceSQL(DB).GetPlacePath(AccessoriesList_[i].Place));
                    }

                    ListViewItem_.BackColor = Color.LimeGreen;
                    listViewAccessories.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshAccessories:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           


        }
        private void listViewAccessories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewAccessories .SelectedItems.Count > 0)
            {
                OpenAccessory_MenuItem.PerformClick();
            }
        }
        private void listViewAccessories_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewAccessories.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewAccessories.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenAccessory_MenuItem, EditAccessory_MenuItem, DeleteAccessory_MenuItem, new MenuItem("-"), AddAccessory_MenuItem };
                        listViewAccessories.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddAccessory_MenuItem };
                        listViewAccessories.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewAccessories_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            

        }
        #endregion
  
        private void textBoxPlaceID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    uint placeid;
                    try
                    {
                         placeid = Convert.ToUInt32(textBoxPlaceID.Text);
                    }catch
                    {
                        throw new Exception("يرجى ادخال عدد صحيح");
                    }
                    TradeStorePlace place = new Trade.TradeSQL.TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(placeid);
                    if (place != null)
                    {
                        buttonClearstoreinfo.Visible = true;
                        _Place = place;
                        textBoxPlaceInfo.Text = new TradeStoreContainerSQL(DB).GetContainerPath(_Place._TradeStoreContainer) + "\\" + _Place._TradeStoreContainer.ContainerName+"\\"+_Place .PlaceName ;

                        textBoxPlaceID.Text = _Place.PlaceID.ToString();
                        textBoxPlaceID.Name = _Place.PlaceID.ToString();

                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على مكان التخزين", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxPlaceID_KeyDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void textBoxPlaceID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TradeStoreContainer container;
                try
                {
                    container = new Trade.TradeSQL.TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(textBoxPlaceID.Name))._TradeStoreContainer;
                }
                catch
                {
                    container = null;
                }

                Trade.Forms.Container.User_ShowLocationsForm frm = new Trade.Forms.Container.User_ShowLocationsForm(DB, container, Trade.Forms.Container.User_ShowLocationsForm.SELECT_Place);
                DialogResult dd = frm.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    TradeStorePlace place = frm.ReturnPlace;
                    buttonClearstoreinfo.Visible = true;
                    _Place = place;
                    textBoxPlaceInfo.Text = new TradeStoreContainerSQL(DB).GetContainerPath(_Place._TradeStoreContainer)+"\\"+ _Place._TradeStoreContainer.ContainerName+"\\"+_Place .PlaceName ;
                        textBoxPlaceID.Text = _Place.PlaceID.ToString();
                    textBoxPlaceID.Name = _Place.PlaceID.ToString();

                }
                frm.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxPlaceID_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }
        //private void buttonStore_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (buttonStore.Name == "buttonStore")
        //        {
        //            TradeStorePlace place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(textBoxPlaceID.Name));
        //            if (place == null)
        //            {
        //                MessageBox.Show("يرجى تحديد مكان التخزين", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }
        //            bool success = new TradeItemStoreSQL(DB).Store_Item_INPlace(place .PlaceID , _MaintenanceOPR ._Operation .OperationID ,TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE, 1, null);
        //            if (success)
        //            {

        //                LoadStoreData();

        //            }
        //        }
        //        else
        //        {
        //            TradeStorePlace place = new TradeItemStoreSQL(DB).GetMaintenanceStorePlace(_MaintenanceOPR ._Operation .OperationID );
        //            bool success = new TradeItemStoreSQL(DB).UNStore_Item_INPlace(place.PlaceID  , _MaintenanceOPR._Operation.OperationID, TradeItemStore.MAINTENANCE_ITEM_STORE_TYPE);
        //            if (success)
        //            {
        //                LoadStoreData();

        //            }
        //        }


        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show("ضبط التخزين:" + ee.Message);
        //    }


        //}
        private void buttonClearstoreinfo_Click(object sender, EventArgs e)
        {
            buttonClearstoreinfo.Visible = false;
            _Place = null;
            textBoxPlaceInfo.Text = "";
            textBoxPlaceID.Text = "";
        }


        #region SubDiagnosticOPR
        public  void RefreshSubDiagnosticOPRList( List<DiagnosticOPRReport> SubDiagnosticOPRReportList_)
        {
            try
            {
                listViewDiagnosticOPR.Items.Clear();
                for (int i = 0; i < SubDiagnosticOPRReportList_.Count; i++)
                {
                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
                    ListViewItem_.Name = SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Desc);
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Location);
                    if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item == null)
                    {
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");

                    }
                    else
                    {
                        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.folder.FolderName);
                        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemName);
                        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemCompany);

                    }
                    if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == null)
                    {
                        ListViewItem_.SubItems.Add("غير معروف");
                        ListViewItem_.BackColor = System.Drawing.Color.LightYellow;
                    }
                    else if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == true)
                    {
                        ListViewItem_.SubItems.Add("لا يوجد عطل");
                        ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    }
                    else
                    {
                        ListViewItem_.SubItems.Add(" يوجد عطل");
                        ListViewItem_.BackColor = System.Drawing.Color.Orange;
                    }
                    //ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR .Report );
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].MeasureOPR_Count.ToString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Files_Count.ToString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].SubDiagnosticOPR_Count.ToString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Tags_Count.ToString());

                    listViewDiagnosticOPR.Items.Add(ListViewItem_);

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(":"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           

        }
        private void DeleteDiagnosticOPR_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewDiagnosticOPR.SelectedItems[0].Name);
                bool success = DiagnosticOPRSQL_.DeleteDiagnosticOPR_All (sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List <DiagnosticOPRReport > DiagnosticOPRList = DiagnosticOPRSQL_.GetSubDiagnosticOPRReportList(_MaintenanceOPR, null );
                    RefreshSubDiagnosticOPRList( DiagnosticOPRList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteSubDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void EditDiagnosticOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewDiagnosticOPR.SelectedItems.Count > 0)
                {
                    uint itemoutid = Convert.ToUInt32(listViewDiagnosticOPR.SelectedItems[0].Name);
                    DiagnosticOPR DiagnosticOPR_ = DiagnosticOPRSQL_.GetDiagnosticOPRINFO_BYID(itemoutid);
                    DiagnosticOPRForm DiagnosticOPRForm_ = new DiagnosticOPRForm(DB, DiagnosticOPR_, true);
                    DiagnosticOPRForm_.ShowDialog();
                    if (DiagnosticOPRForm_.Changed)
                    {
                        List<DiagnosticOPRReport> DiagnosticOPRList = DiagnosticOPRSQL_.GetSubDiagnosticOPRReportList(_MaintenanceOPR, null);
                        RefreshSubDiagnosticOPRList(DiagnosticOPRList);
                    }
                    DiagnosticOPRForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void OpenDiagnosticOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewDiagnosticOPR.SelectedItems.Count > 0)
                {
                    uint itemoutid = Convert.ToUInt32(listViewDiagnosticOPR.SelectedItems[0].Name);
                    DiagnosticOPR DiagnosticOPR_ = DiagnosticOPRSQL_.GetDiagnosticOPRINFO_BYID(itemoutid);
                    DiagnosticOPRForm DiagnosticOPRForm_ = new DiagnosticOPRForm(DB, DiagnosticOPR_, false);
                    DiagnosticOPRForm_.ShowDialog();
                    if (DiagnosticOPRForm_.Changed)
                    {
                        List<DiagnosticOPRReport> DiagnosticOPRList = DiagnosticOPRSQL_.GetSubDiagnosticOPRReportList(_MaintenanceOPR, null);
                        RefreshSubDiagnosticOPRList(DiagnosticOPRList);
                    }
                    DiagnosticOPRForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }
        private void AddDiagnosticOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DiagnosticOPRForm DiagnosticOPRForm_ = new DiagnosticOPRForm(DB, _MaintenanceOPR, null);
                DialogResult d = DiagnosticOPRForm_.ShowDialog();
                if (DiagnosticOPRForm_.Changed)
                {
                    List<DiagnosticOPRReport> DiagnosticOPRList = DiagnosticOPRSQL_.GetSubDiagnosticOPRReportList(_MaintenanceOPR, null);
                    RefreshSubDiagnosticOPRList(DiagnosticOPRList);
                }
                DiagnosticOPRForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        //private async void RefreshDiagnosticOPRList(List<DiagnosticOPRReport> SubDiagnosticOPRReportList_)
        //{

        //    listViewSubDiagnosticOPR.Items.Clear();
        //    for (int i = 0; i < SubDiagnosticOPRReportList_.Count; i++)
        //    {
        //        ListViewItem ListViewItem_ = new ListViewItem(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
        //        ListViewItem_.Name = SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRDate.ToShortDateString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Desc);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Location);
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item == null)
        //        {
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");

        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.folder.FolderName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemCompany);

        //        }
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == null)
        //        {
        //            ListViewItem_.SubItems.Add("غير معروف");
        //            ListViewItem_.BackColor = Color.LightYellow;
        //        }
        //        else if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == true)
        //        {
        //            ListViewItem_.SubItems.Add("لا يوجد عطل");
        //            ListViewItem_.BackColor = Color.LimeGreen;
        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(" يوجد عطل");
        //            ListViewItem_.BackColor = Color.Orange;
        //        }
        //        //ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Report);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].MeasureOPR_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Files_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].SubDiagnosticOPR_Count.ToString());

        //        listViewSubDiagnosticOPR.Items.Add(ListViewItem_);

        //    }



        //}
        private void listViewDiagnosticOPR_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && listViewDiagnosticOPR.SelectedItems.Count > 0)
            {
                OpenDiagnosticOPR_MenuItem.PerformClick();
            }
        }
        private void listViewDiagnosticOPR_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewDiagnosticOPR.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewDiagnosticOPR.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenDiagnosticOPR_MenuItem
                        , UpdateDiagnosticOPR_MenuItem, DeleteDiagnosticOPR_MenuItem, new MenuItem("-"), AddDiagnosticOPR_MenuItem };
                        listViewDiagnosticOPR.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddDiagnosticOPR_MenuItem };
                        listViewDiagnosticOPR.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewDiagnosticOPR_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           

        }


        #endregion

        #region Fault
        internal void RefreshFaultReportList( List<MaintenanceFaultReport> MaintenanceFaultReportList)
        {
            try
            {
                listViewFault.Items.Clear();
                for (int i = 0; i < MaintenanceFaultReportList.Count; i++)
                {

                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(MaintenanceFaultReportList[i].Fault.FaultID.ToString());
                    ListViewItem_.Name = MaintenanceFaultReportList[i].Fault.FaultID.ToString();
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault.FaultDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault.FaultDesc);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault._Item.ItemName);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].MaintenanceFault_Affictive_RepairOPRList.Count.ToString());
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Tags_Count.ToString());
                    if (MaintenanceFaultReportList[i].MaintenanceFault_Affictive_RepairOPRList.Count == 0)
                        ListViewItem_.BackColor = System.Drawing.Color.Orange;
                    else
                        ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    listViewFault.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshFaultReportList:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
   


        }

        private void DeleteFault_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من حذف العطل و جميع العمليات التابعة له؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewFault.SelectedItems[0].Name);
                bool success = new MaintenanceFaultSQL(DB).DeleteFault (sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List <MaintenanceFaultReport > FaultReportList = new MaintenanceFaultSQL(DB).GetMaintenanceOPR_Report_Fault_List(_MaintenanceOPR );
                    RefreshFaultReportList( FaultReportList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteSubDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void EditFault_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewFault.SelectedItems.Count > 0)
                {
                    uint faultid = Convert.ToUInt32(listViewFault.SelectedItems[0].Name);
                    MaintenanceFault MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(faultid);
                    FaultForm FaultForm_ = new FaultForm(DB, MaintenanceFault_, true);
                    FaultForm_.ShowDialog();
                    if (FaultForm_.Changed)
                    {
                        List<MaintenanceFaultReport> FaultReportList = new MaintenanceFaultSQL(DB).GetMaintenanceOPR_Report_Fault_List(_MaintenanceOPR);
                        RefreshFaultReportList(FaultReportList);
                    }
                    FaultForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditFault_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }
        private void OpenFault_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewFault.SelectedItems.Count > 0)
                {
                    uint faultid = Convert.ToUInt32(listViewFault.SelectedItems[0].Name);
                    MaintenanceFault MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(faultid);
                    FaultForm FaultForm_ = new FaultForm(DB, MaintenanceFault_, false);
                    FaultForm_.ShowDialog();
                    if (FaultForm_.Changed)
                    {
                        List<MaintenanceFaultReport> FaultReportList = new MaintenanceFaultSQL(DB).GetMaintenanceOPR_Report_Fault_List(_MaintenanceOPR);
                        RefreshFaultReportList(FaultReportList);
                    }
                    FaultForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenFault_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }
        private void AddFault_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FaultForm FaultForm_ = new FaultForm(DB, _MaintenanceOPR);
                DialogResult d = FaultForm_.ShowDialog();
                if (FaultForm_.Changed)
                {
                    List<MaintenanceFaultReport> FaultReportList = new MaintenanceFaultSQL(DB).GetMaintenanceOPR_Report_Fault_List(_MaintenanceOPR);
                    RefreshFaultReportList(FaultReportList);
                }
                FaultForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddFault_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
        }
        //private async void RefreshDiagnosticOPRList(List<DiagnosticOPRReport> SubDiagnosticOPRReportList_)
        //{

        //    listViewSubDiagnosticOPR.Items.Clear();
        //    for (int i = 0; i < SubDiagnosticOPRReportList_.Count; i++)
        //    {
        //        ListViewItem ListViewItem_ = new ListViewItem(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
        //        ListViewItem_.Name = SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRDate.ToShortDateString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Desc);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Location);
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item == null)
        //        {
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");

        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.folder.FolderName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemCompany);

        //        }
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == null)
        //        {
        //            ListViewItem_.SubItems.Add("غير معروف");
        //            ListViewItem_.BackColor = Color.LightYellow;
        //        }
        //        else if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == true)
        //        {
        //            ListViewItem_.SubItems.Add("لا يوجد عطل");
        //            ListViewItem_.BackColor = Color.LimeGreen;
        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(" يوجد عطل");
        //            ListViewItem_.BackColor = Color.Orange;
        //        }
        //        //ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Report);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].MeasureOPR_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Files_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].SubDiagnosticOPR_Count.ToString());

        //        listViewSubDiagnosticOPR.Items.Add(ListViewItem_);

        //    }



        //}
        private void listViewFault_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewFault.SelectedItems.Count > 0)
            {
                OpenFault_MenuItem .PerformClick();
            }
        }
        private void listViewFault_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewFault.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewFault.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenFault_MenuItem
                        , UpdateFault_MenuItem, DeleteFault_MenuItem, new MenuItem("-"), AddFault_MenuItem };
                        listViewFault.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddFault_MenuItem };
                        listViewFault.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewFault_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           

        }
        private void listViewFault_Resize(object sender, EventArgs e)
        {
            //MaintenanceFaultReport .AdjustlistViewFaultReportOPRColumnsWidth (ref listViewSubDiagnosticOPR);
            AdjustlistViewFaultReportOPRColumnsWidth();
        }
        public async  void AdjustlistViewFaultReportOPRColumnsWidth()
        {
            try
            {
                listViewFault .Columns[0].Width = 60;
                listViewFault.Columns[1].Width = 100;
                listViewFault.Columns[2].Width = listViewFault.Width - 1010;
                listViewFault.Columns[3].Width = 110;
                listViewFault.Columns[4].Width = 110;
                listViewFault.Columns[5].Width = 110;
                listViewFault.Columns[6].Width = 130;
                listViewFault.Columns[7].Width = 190;
                //listViewFault.Columns[8].Width = 190;
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewFaultReportOPRColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Missed_Fault_Item
        internal void RefreshMissed_FaultList(List<Missed_Fault_Item> Missed_Fault_ItemList)
        {
            try
            {
                listViewMissedFaultItem.Items.Clear();
                for (int i = 0; i < Missed_Fault_ItemList.Count; i++)
                {

                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(Missed_Fault_ItemList[i].ID.ToString());
                    ListViewItem_.Name = Missed_Fault_ItemList[i].ID.ToString();
                    if (Missed_Fault_ItemList[i].Type == Missed_Fault_Item.FAULT_ITEM)
                    {
                        ListViewItem_.SubItems.Add("تالف");
                        ListViewItem_.BackColor = System.Drawing.Color.SandyBrown;
                    }
                    else
                    {
                        ListViewItem_.SubItems.Add("مفقود");
                        ListViewItem_.BackColor = System.Drawing.Color.PeachPuff;
                    }
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i]._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i]._Item.ItemName);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i]._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i].Location);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i].Notes);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i].TagsCount.ToString());

                    listViewMissedFaultItem.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshMissed_FaultList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          


        }

        private void Delete_MissedFault_Item_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من حذف العطل و جميع العمليات التابعة له؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewMissedFaultItem.SelectedItems[0].Name);
                bool success = new MissedFaultItemSQL (DB).DeleteMissed_Fault_Item (sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Missed_Fault_Item > Missed_Fault_Item_List = new MissedFaultItemSQL(DB).MaintenanceOPR_GetMissed_Fault_Item_List  (_MaintenanceOPR);
                    RefreshMissed_FaultList(Missed_Fault_Item_List);
                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteSubDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Edit_MissedFault_Item_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewMissedFaultItem.SelectedItems.Count > 0)
                {
                    uint id = Convert.ToUInt32(listViewMissedFaultItem.SelectedItems[0].Name);
                    Missed_Fault_Item Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(id);
                    MissedFault_Item_Form MissedFault_Item_Form_ = new MissedFault_Item_Form(DB, Missed_Fault_Item_, true);
                    MissedFault_Item_Form_.ShowDialog();
                    if (MissedFault_Item_Form_.Changed)
                    {
                        List<Missed_Fault_Item> Missed_Fault_Item_List = new MissedFaultItemSQL(DB).MaintenanceOPR_GetMissed_Fault_Item_List(_MaintenanceOPR);
                        RefreshMissed_FaultList(Missed_Fault_Item_List);
                    }
                    MissedFault_Item_Form_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_MissedFault_Item_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void Open_MissedFault_Item_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewMissedFaultItem.SelectedItems.Count > 0)
                {
                    uint id = Convert.ToUInt32(listViewMissedFaultItem.SelectedItems[0].Name);
                    Missed_Fault_Item Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(id);
                    MissedFault_Item_Form MissedFault_Item_Form_ = new MissedFault_Item_Form(DB, Missed_Fault_Item_, false);
                    MissedFault_Item_Form_.ShowDialog();
                    if (MissedFault_Item_Form_.Changed)
                    {
                        List<Missed_Fault_Item> Missed_Fault_Item_List = new MissedFaultItemSQL(DB).MaintenanceOPR_GetMissed_Fault_Item_List(_MaintenanceOPR);
                        RefreshMissed_FaultList(Missed_Fault_Item_List);
                    }
                    MissedFault_Item_Form_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_MissedFault_Item_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        //private void Add_MissedFault_Item_MenuItem_Click(object sender, EventArgs e)
        //{
        //    MissedFault_Item_Form MissedFault_Item_Form_ = new MissedFault_Item_Form(DB, _MaintenanceOPR);
        //    DialogResult d = MissedFault_Item_Form_.ShowDialog();
        //    if (MissedFault_Item_Form_.Changed)
        //    {
        //        Missed_Fault_Item_List = new MissedFaultItemSQL(DB).MaintenanceOPR_GetMissed_Fault_Item_List(_MaintenanceOPR);
        //        Missed_Fault_Item.RefreshMissed_FaultList(ref listViewMissedFaultItem, Missed_Fault_Item_List);
        //    }
        //    MissedFault_Item_Form_.Dispose();
        //}
        //private async void RefreshDiagnosticOPRList(List<DiagnosticOPRReport> SubDiagnosticOPRReportList_)
        //{

        //    listViewSubDiagnosticOPR.Items.Clear();
        //    for (int i = 0; i < SubDiagnosticOPRReportList_.Count; i++)
        //    {
        //        ListViewItem ListViewItem_ = new ListViewItem(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
        //        ListViewItem_.Name = SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRDate.ToShortDateString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Desc);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Location);
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item == null)
        //        {
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");

        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.folder.FolderName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemCompany);

        //        }
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == null)
        //        {
        //            ListViewItem_.SubItems.Add("غير معروف");
        //            ListViewItem_.BackColor = Color.LightYellow;
        //        }
        //        else if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == true)
        //        {
        //            ListViewItem_.SubItems.Add("لا يوجد عطل");
        //            ListViewItem_.BackColor = Color.LimeGreen;
        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(" يوجد عطل");
        //            ListViewItem_.BackColor = Color.Orange;
        //        }
        //        //ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Report);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].MeasureOPR_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Files_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].SubDiagnosticOPR_Count.ToString());

        //        listViewSubDiagnosticOPR.Items.Add(ListViewItem_);

        //    }



        //}
        private void listView_MissedFault_Item_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewMissedFaultItem.SelectedItems.Count > 0)
            {
                Open_MissedFault_Item_MenuItem .PerformClick();
            }
        }
        private void listView_MissedFault_Item_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewMissedFaultItem.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewMissedFaultItem.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { Open_MissedFault_Item_MenuItem
                        , Update_MissedFault_Item_MenuItem, Delete_MissedFault_Item_MenuItem };
                        listViewMissedFaultItem.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { };
                        listViewMissedFaultItem.ContextMenu = null;
                        

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView_MissedFault_Item_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           

        }
        private void listView_MissedFault_Item_Resize(object sender, EventArgs e)
        {
            //MaintenanceFaultReport .AdjustlistViewFaultReportOPRColumnsWidth (ref listViewSubDiagnosticOPR);
            AdjustlistView_MissedFault_ItemOPRColumnsWidth( listViewMissedFaultItem);
        }
        public async void AdjustlistView_MissedFault_ItemOPRColumnsWidth( ListView listview)
        {
            try
            {
                listview.Columns[0].Width = 60;
                listview.Columns[1].Width = 80;
                listview.Columns[2].Width = 125;
                listview.Columns[3].Width = 125;
                listview.Columns[4].Width = 125;
                listview.Columns[5].Width = 125;
                listview.Columns[6].Width = listview.Width - 880;
                listview.Columns[7].Width = 230;
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewMissedFaultItemColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region BillMaintenance
        private void buttonBill_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (_BillMaintenance == null)
                {
                    BillMaintenanceForm BillMaintenanceForm_ = new BillMaintenanceForm(DB, DateTime.Now, _MaintenanceOPR);
                    BillMaintenanceForm_.ShowDialog();
                    if (BillMaintenanceForm_.Refresh_ListViewMaintenanceOPRs_Flag  )
                    {
                        this.Refresh_ListViewMaintenanceOPRs_Flag   = true;
                        _BillMaintenance = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(_MaintenanceOPR);
                        LoadBillData(true);
                    }
                    if (BillMaintenanceForm_.Refresh_ListViewMoneyDataDetails_Flag) this.Refresh_ListViewMoneyDataDetails_Flag = true;
                }
                else
                {
                    BillMaintenanceForm BillMaintenanceForm_ = new BillMaintenanceForm(DB, _BillMaintenance, false);
                    BillMaintenanceForm_.ShowDialog();
                    if (BillMaintenanceForm_.Refresh_ListViewMaintenanceOPRs_Flag )
                    {
                        this.Refresh_ListViewMaintenanceOPRs_Flag  = true;

                        _BillMaintenance = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(_MaintenanceOPR);
                        LoadBillData(true);
                    }
                    if (BillMaintenanceForm_.Refresh_ListViewMoneyDataDetails_Flag) this.Refresh_ListViewMoneyDataDetails_Flag = true;

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonBill_Create_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private async void LoadBillData(bool Edit)
        {
            try
            {
                if (_BillMaintenance == null)
                {
                    buttonBill_Create.Enabled = true;
                    buttonBill_Create.Text = "انشاء فاتورة";
                    buttonBill_Edit.Visible = false;
                    buttonBill_Delete.Visible = false;
                    textBoxBillCurrency.Text = "-";
                    textBoxBillExchangeRte.Text = "-";
                    textBoxBillValue.Text = "-";

                }
                else
                {
                    textBoxBillCurrency.Text = _BillMaintenance._Currency.CurrencyName;
                    textBoxBillExchangeRte.Text = _BillMaintenance.ExchangeRate.ToString();
                    textBoxBillValue.Text = new OperationSQL(DB).Get_OperationValue(_BillMaintenance._Operation.OperationType, _BillMaintenance._Operation.OperationID).ToString()
                       + " " + _BillMaintenance._Currency.CurrencySymbol;
                    buttonBill_Create.Enabled = true;
                    buttonBill_Create.Text = "استعراض";
                    buttonBill_Edit.Visible = true;
                    buttonBill_Delete.Visible = true;
                }
                if (!Edit)
                {
                    buttonBill_Create.Visible = true   ;
                    buttonBill_Edit.Visible = false;
                    buttonBill_Delete.Visible = false;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadBillData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            

        }
        private void buttonBill_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                BillMaintenanceForm BillMaintenanceForm_ = new BillMaintenanceForm(DB, _BillMaintenance, true);
                BillMaintenanceForm_.ShowDialog();
                if (BillMaintenanceForm_.Refresh_ListViewMaintenanceOPRs_Flag )
                {
                    _BillMaintenance = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(_MaintenanceOPR);
                    LoadBillData(true);
                    this.Refresh_ListViewMaintenanceOPRs_Flag  = true;
                }
                if (BillMaintenanceForm_.Refresh_ListViewMoneyDataDetails_Flag ) this.Refresh_ListViewMoneyDataDetails_Flag  = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonBill_Edit_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void buttonBill_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                bool sussecc = new BillMaintenanceSQL(DB).DeleteBillMaintenance(_BillMaintenance._Operation.OperationID);
                if (sussecc)
                {
                    _BillMaintenance = null;
                    LoadBillData(true);
                    this.Refresh_ListViewMoneyDataDetails_Flag  = true;
                    this.Refresh_ListViewMaintenanceOPRs_Flag  = true;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonBill_Delete_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }
        #endregion
        #region EndWork    
        private async void LoadEndWorkData(bool Edit)
        {
            try
            {
                if (_MaintenanceOPR._MaintenanceOPR_EndWork == null)
                {
                    buttonEndWork_Create.Enabled = true;
                    buttonEndWork_Create.Text = "انتهاء العمل";
                    buttonEndWork_Edit.Visible = false;
                    buttonEndWork_Delete.Visible = false;
                    textBoxEndWorkDate.Text = "-";
                    textBoxEndWorkResult.Text = "-";
                    textBoxDeilverdate.Text = "-";
                    textBoxEndWarrantyDate.Text = "-";
                    textBoxEndWorkReport.Text = "-";

                }
                else
                {
                    textBoxEndWorkDate.Text = _MaintenanceOPR._MaintenanceOPR_EndWork.EndWorkDate.ToShortDateString();
                    if (_MaintenanceOPR._MaintenanceOPR_EndWork.Repaired)
                    {
                        textBoxEndWorkResult.Text = "تم الاصلاح";
                        textBoxEndWorkResult.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        textBoxEndWorkResult.Text = "لم يتم الاصلاح";
                        textBoxEndWorkResult.BackColor = Color.Orange;
                    }
                    if (_MaintenanceOPR._MaintenanceOPR_EndWork.DeliveredDate != null)
                    {
                        textBoxDeilverdate.Text = Convert.ToDateTime(_MaintenanceOPR._MaintenanceOPR_EndWork.DeliveredDate).ToShortDateString();
                        textBoxDeilverdate.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        textBoxDeilverdate.Text = "-";
                        textBoxDeilverdate.BackColor = Color.Orange;
                    }
                    if (_MaintenanceOPR._MaintenanceOPR_EndWork.EndwarrantyDate != null)
                    {
                        textBoxEndWarrantyDate.Text = Convert.ToDateTime(_MaintenanceOPR._MaintenanceOPR_EndWork.EndwarrantyDate).ToShortDateString();
                        if (DateTime.Now > _MaintenanceOPR._MaintenanceOPR_EndWork.EndwarrantyDate)
                            textBoxEndWarrantyDate.BackColor = Color.Orange;
                        else
                            textBoxEndWarrantyDate.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        textBoxEndWarrantyDate.Text = "  لا يوجد ضمان";
                    }


                    textBoxEndWorkReport.Text = _MaintenanceOPR._MaintenanceOPR_EndWork.Report;
                    buttonEndWork_Create.Enabled = true;
                    buttonEndWork_Create.Text = "استعراض";
                    buttonEndWork_Edit.Visible = true;
                    buttonEndWork_Delete.Visible = true;
                }
                if (!Edit)
                {
                    buttonEndWork_Create.Visible = false  ;
                    buttonEndWork_Edit.Visible = false;
                    buttonEndWork_Delete.Visible = false;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadEndWorkData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           


        }
        private void buttonEndWork_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (_MaintenanceOPR._MaintenanceOPR_EndWork == null)
                {
                    MaintenanceOPR_EndWorkForm MaintenanceOPR_EndWorkForm_ = new MaintenanceOPR_EndWorkForm(DB, _MaintenanceOPR);
                    MaintenanceOPR_EndWorkForm_.ShowDialog();
                    if (MaintenanceOPR_EndWorkForm_.Changed)
                    {
                        _MaintenanceOPR._MaintenanceOPR_EndWork = new MaintenanceOPRSQL(DB).Get_MaintenanceOPR_EndWork_ForMaintenanceOPR(_MaintenanceOPR._Operation.OperationID);
                        LoadEndWorkData(true);
                    }
                }
                else
                {
                    MaintenanceOPR_EndWorkForm MaintenanceOPR_EndWorkForm_ = new MaintenanceOPR_EndWorkForm(DB, _MaintenanceOPR._MaintenanceOPR_EndWork, false);
                    MaintenanceOPR_EndWorkForm_.ShowDialog();
                    if (MaintenanceOPR_EndWorkForm_.Changed)
                    {
                        _MaintenanceOPR._MaintenanceOPR_EndWork = new MaintenanceOPRSQL(DB).Get_MaintenanceOPR_EndWork_ForMaintenanceOPR(_MaintenanceOPR._Operation.OperationID);
                        LoadEndWorkData(true);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonEndWork_Create_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
        }
        private void buttonEndWork_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                MaintenanceOPR_EndWorkForm MaintenanceOPR_EndWorkForm_ = new MaintenanceOPR_EndWorkForm(DB, _MaintenanceOPR._MaintenanceOPR_EndWork, true);
                MaintenanceOPR_EndWorkForm_.ShowDialog();
                if (MaintenanceOPR_EndWorkForm_.Changed)
                {
                    _MaintenanceOPR._MaintenanceOPR_EndWork = new MaintenanceOPRSQL(DB).Get_MaintenanceOPR_EndWork_ForMaintenanceOPR(_MaintenanceOPR._Operation.OperationID);
                    LoadEndWorkData(true);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonEndWork_Edit_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }

        private void buttonEndWork_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                bool sussecc = new MaintenanceOPRSQL(DB).Delete_MaintenanceOPREndWork(_MaintenanceOPR._Operation.OperationID);
                if (sussecc)
                {
                    _MaintenanceOPR._MaintenanceOPR_EndWork = null;
                    LoadEndWorkData(true);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonEndWork_Delete_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        #endregion
        #region RepairOPR
        internal void RefreshRepairOPRList( List<RepairOPR> RepairOPRtList)
        {
            try
            {
                listViewRepairOPR.Items.Clear();
                for (int i = 0; i < RepairOPRtList.Count; i++)
                {

                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(RepairOPRtList[i]._Operation.OperationID.ToString());
                    ListViewItem_.Name = RepairOPRtList[i]._Operation.OperationID.ToString();
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].RepairOPRDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].RepairDesc);
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].RepairReport);
                    //if (RepairOPRtList[i].FaultRepair == true)
                    //{
                    //    ListViewItem_.SubItems.Add("عملية اصلاح فعالة");
                    //    ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    //}
                    //else
                    //{
                    //    ListViewItem_.SubItems.Add("-");
                    //    ListViewItem_.BackColor = System.Drawing.Color.Orange;
                    //}

                    ListViewItem_.SubItems.Add(RepairOPRtList[i].InstalledItem_Count.ToString());
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].TestInstallOPR_Count.ToString());
                    ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    listViewRepairOPR.Items.Add(ListViewItem_);
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshRepairOPRList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }

        private void DeleteRepairOPR_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من حذف بند الاصلاح و جميع العمليات التابعة له؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewRepairOPR.SelectedItems[0].Name);
                bool success = new RepairOPRSQL(DB).DeleteRepairOPR(sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<RepairOPR > RepairOPRList = new RepairOPRSQL(DB).Get_MaintenanceOPR_RepairOPR_List(_MaintenanceOPR );
                    RefreshRepairOPRList( RepairOPRList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteSubDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void EditRepairOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewRepairOPR.SelectedItems.Count > 0)
                {
                    uint faultid = Convert.ToUInt32(listViewRepairOPR.SelectedItems[0].Name);
                    RepairOPR RepairOPR_ = new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(faultid);
                    RepairOPRForm RepairOPRForm_ = new RepairOPRForm(DB, RepairOPR_, true);
                    RepairOPRForm_.ShowDialog();
                    if (RepairOPRForm_.Changed)
                    {
                        List<RepairOPR> RepairOPRList = new RepairOPRSQL(DB).Get_MaintenanceOPR_RepairOPR_List(_MaintenanceOPR);
                        RefreshRepairOPRList(RepairOPRList);
                    }
                    RepairOPRForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditRepairOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void OpenRepairOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewRepairOPR.SelectedItems.Count > 0)
                {
                    uint faultid = Convert.ToUInt32(listViewRepairOPR.SelectedItems[0].Name);
                    RepairOPR RepairOPR_ = new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(faultid);
                    RepairOPRForm RepairOPRForm_ = new RepairOPRForm(DB, RepairOPR_, false);
                    RepairOPRForm_.ShowDialog();
                    if (RepairOPRForm_.Changed)
                    {
                        List<RepairOPR> RepairOPRList = new RepairOPRSQL(DB).Get_MaintenanceOPR_RepairOPR_List(_MaintenanceOPR);
                        RefreshRepairOPRList(RepairOPRList);
                    }
                    RepairOPRForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenRepairOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void AddRepairOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RepairOPRForm RepairOPRForm_ = new RepairOPRForm(DB, _MaintenanceOPR);
                DialogResult d = RepairOPRForm_.ShowDialog();
                if (RepairOPRForm_.Changed)
                {
                    List<RepairOPR> RepairOPRList = new RepairOPRSQL(DB).Get_MaintenanceOPR_RepairOPR_List(_MaintenanceOPR);
                    RefreshRepairOPRList(RepairOPRList);
                }
                RepairOPRForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddRepairOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        //private async void RefreshDiagnosticOPRList(List<DiagnosticOPRReport> SubDiagnosticOPRReportList_)
        //{

        //    listViewSubDiagnosticOPR.Items.Clear();
        //    for (int i = 0; i < SubDiagnosticOPRReportList_.Count; i++)
        //    {
        //        ListViewItem ListViewItem_ = new ListViewItem(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
        //        ListViewItem_.Name = SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRDate.ToShortDateString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Desc);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Location);
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item == null)
        //        {
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");
        //            ListViewItem_.SubItems.Add("-");

        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.folder.FolderName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemName);
        //            ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemCompany);

        //        }
        //        if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == null)
        //        {
        //            ListViewItem_.SubItems.Add("غير معروف");
        //            ListViewItem_.BackColor = Color.LightYellow;
        //        }
        //        else if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == true)
        //        {
        //            ListViewItem_.SubItems.Add("لا يوجد عطل");
        //            ListViewItem_.BackColor = Color.LimeGreen;
        //        }
        //        else
        //        {
        //            ListViewItem_.SubItems.Add(" يوجد عطل");
        //            ListViewItem_.BackColor = Color.Orange;
        //        }
        //        //ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Report);
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].MeasureOPR_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Files_Count.ToString());
        //        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].SubDiagnosticOPR_Count.ToString());

        //        listViewSubDiagnosticOPR.Items.Add(ListViewItem_);

        //    }



        //}
        private void listViewRepairOPR_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewRepairOPR.SelectedItems.Count > 0)
            {
                OpenRepairOPR_MenuItem.PerformClick();
            }
        }
        private void listViewRepairOPR_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewRepairOPR.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewRepairOPR.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenRepairOPR_MenuItem
                        , EditRepairOPR_MenuItem, DeleteRepairOPR_MenuItem, new MenuItem("-"), AddRepairOPR_MenuItem };
                        listViewRepairOPR.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddRepairOPR_MenuItem };
                        listViewRepairOPR.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewRepairOPR_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }
        private void listViewRepairOPR_Resize(object sender, EventArgs e)
        {
            //MaintenanceFaultReport .AdjustlistViewFaultReportOPRColumnsWidth (ref listViewSubDiagnosticOPR);
            AdjustlistViewRepairOPRColumnsWidth();
        }
        public async void AdjustlistViewRepairOPRColumnsWidth()
        {
            try
            {
                listViewRepairOPR.Columns[0].Width = 60;
                listViewRepairOPR.Columns[1].Width = 100;
                listViewRepairOPR.Columns[2].Width = 150;
                listViewRepairOPR.Columns[3].Width = listViewRepairOPR.Width - 770;
                listViewRepairOPR.Columns[4].Width = 150;
                listViewRepairOPR.Columns[5].Width = 150;
                listViewRepairOPR.Columns[6].Width = 150;
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewRepairOPRColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
