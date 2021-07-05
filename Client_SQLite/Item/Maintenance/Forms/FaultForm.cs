using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
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
    public partial class FaultForm : Form
    {

        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        MaintenanceFault _MaintenanceFault;
        Folder LastUsedFolder;
        Item _Item;
        List<RepairOPR> RepairOPRList = new List<RepairOPR>();
        List<MaintenanceTag> FaultTagList_ = new List<MaintenanceTag>();


        MenuItem Add_Affictive_RepairOPR;
        MenuItem Delete_Affictive_RepairOPR;
        MenuItem Open_Affictive_RepairOPR;

        MenuItem OpenTag_Operation_MenuItem;
        MenuItem OpenTag_MenuItem;
        MenuItem AddDiagnosticOPRTag_MenuItem;
        MenuItem AddMissedFaultItemTag_MenuItem;
        MenuItem UpdateTag_MenuItem;
        MenuItem DeleteTag_MenuItem;


        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public FaultForm(DatabaseInterface db, MaintenanceOPR MaintenanceOPR_)
        {

            DB = db;
            _MaintenanceOPR = MaintenanceOPR_;
            InitializeComponent();
            textBoxContact.Text = _MaintenanceOPR._Contact.Get_Complete_ContactName_WithHeader();
            textBoxMOPR.Text = _MaintenanceOPR._Operation.OperationID.ToString();
            dateTimePickerMainteneaceOPRDate.Value = _MaintenanceOPR.EntryDate ;
            _Item = MaintenanceOPR_._Item ;
            LoadItemData();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "اضافة";
            this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
            this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);

            Initialze_MenuItems();





        }

        private void Initialze_MenuItems()
        {
            OpenTag_Operation_MenuItem = new System.Windows.Forms.MenuItem("استعراض العملية", OpenTag_Operation_MenuItem_Click);
            OpenTag_MenuItem = new System.Windows.Forms.MenuItem("فتح ", OpenTag_MenuItem_Click);
            AddDiagnosticOPRTag_MenuItem = new System.Windows.Forms.MenuItem("ربط مع عملية فحص", AddDiagnosticOPRTag_MenuItem_Click);
            AddMissedFaultItemTag_MenuItem = new System.Windows.Forms.MenuItem("ربط مع عنصر مفقود او تالف", AddMissedFaultItemTag_MenuItem_Click);
            UpdateTag_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditTag_MenuItem_Click);
            DeleteTag_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteTag_MenuItem_Click);

            Add_Affictive_RepairOPR = new System.Windows.Forms.MenuItem("اضافة عملية اصلاح ", Add_Affictive_RepairOPR_Click);
            Delete_Affictive_RepairOPR = new System.Windows.Forms.MenuItem("حذف", Delete_Affictive_RepairOPR_Click);
            Open_Affictive_RepairOPR = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية الاصلاح", Open_Affictive_RepairOPR_Click);
        }

        public FaultForm(DatabaseInterface db, MaintenanceFault MaintenanceFault_, bool Edit)
        {
            DB = db;
            _MaintenanceFault = MaintenanceFault_;
            _MaintenanceOPR = _MaintenanceFault._MaintenanceOPR ;
            InitializeComponent();


            Initialze_MenuItems();


            LoadForm(Edit);
        }
        public void LoadForm(bool Edit)
        {
            try
            {


                if (_MaintenanceFault == null) return;
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                _Item = _MaintenanceFault._Item;
                _MaintenanceOPR = _MaintenanceFault._MaintenanceOPR;
                FillComboBox(_MaintenanceFault.FaultDesc );
                dateTimePickerFaultDate.Value = _MaintenanceFault.FaultDate;
                textBoxContact.Text = _MaintenanceOPR._Contact.Get_Complete_ContactName_WithHeader();
                textBoxMOPR.Text = _MaintenanceOPR._Operation.OperationID.ToString();
                dateTimePickerMainteneaceOPRDate.Value = _MaintenanceOPR.EntryDate ;
                LoadItemData();
                RepairOPRList = new MaintenanceFaultSQL (DB).GetFault_Affictive_RepairOPR_List (_MaintenanceFault.FaultID);
                FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List(_MaintenanceFault);

                RefreshRepairOPRList( RepairOPRList);
                RefreshTagList(FaultTagList_);
                if (Edit)
                {
         
                    buttonSave.Visible = true;
                    textBoxItemID.ReadOnly = false;
                    comboBoxFaultDesc .Enabled  = true ;
                    dateTimePickerFaultDate .Enabled  = true ;

                    this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
                    this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);

                    this.listViewTags.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewTag_MouseDoubleClick);
                    this.listViewTags.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewTag_MouseDown);

                    this.listViewRepairOPR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewRepairOPR_MouseDoubleClick);
                    this.listViewRepairOPR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewRepairOPR_MouseDown);


                }
                else
                {
 
                    buttonSave.Visible = false;
                    TextBox textboxdesc = new TextBox();
                    textboxdesc.ReadOnly = true;
                    textboxdesc.Location = comboBoxFaultDesc.Location;
                    textboxdesc.Size = comboBoxFaultDesc.Size;
                    textboxdesc.Text = _MaintenanceFault.FaultDesc;
                    textboxdesc.Font = comboBoxFaultDesc.Font;
                    textboxdesc.BorderStyle = BorderStyle.FixedSingle;
                    textboxdesc .Anchor  = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                    panel2.Controls.Add(textboxdesc);
                    comboBoxFaultDesc.Visible = false;
                    textBoxItemID.ReadOnly = true;
                    comboBoxFaultDesc.Enabled = false ;
                    dateTimePickerFaultDate.Enabled = false ;

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("حصل خطأ اثناء تحميل الصفحة:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void FillComboBox(string faultdesc)
        {
            try
            {
                comboBoxFaultDesc.Items.Clear();
                int selectedIndex = -1;
                List<string> faultdescList = new MaintenanceFaultSQL(DB).GetItem_FaultDescList(_Item);
                for (int i = 0; i < faultdescList.Count; i++)
                {
                    if (faultdescList[i] == faultdesc) selectedIndex = i;
                    comboBoxFaultDesc.Items.Add(faultdescList[i]);
                }
                comboBoxFaultDesc.SelectedIndex = selectedIndex;
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBox:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           
        }
        private async void LoadItemData()
        {
            try
            {
                if (_Item != null)
                {
                    LastUsedFolder = _Item.folder;
                    textBoxItemID.Text = _Item.ItemID.ToString();
                    textBoxItemName.Text = _Item.ItemName;
                    textBoxItemCompany.Text = _Item.ItemCompany;
                    textBoxItemType.Text = _Item.folder.FolderName;
                }
                else
                {
                    textBoxItemID.Text = "";
                    textBoxItemName.Text = "";
                    textBoxItemCompany.Text = "";
                    textBoxItemType.Text = "";
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadItemData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private void textBoxItemID_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    uint itemid = Convert.ToUInt32(textBoxItemID.Text);
                    Item item__ = new ItemObj.ItemObjSQL.ItemSQL(DB).GetItemInfoByID(itemid);
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
                    ItemObj.Forms.User_ShowItemsForm SelectItem_ = new ItemObj.Forms.User_ShowItemsForm(DB, null, ItemObj.Forms.User_ShowItemsForm.SELECT_ITEM);
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
                MessageBox.Show(":" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (_MaintenanceOPR == null) return;
            //ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
            //ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

            //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
            //string SellType_ = ComboboxItem_selltype.Text;

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    if (_Item == null)
                    {

                        MessageBox.Show("يرجى تحديد المادة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MaintenanceFault MaintenanceFault_ =
                        new MaintenanceSQL.MaintenanceFaultSQL(DB).AddFault 
                        (_MaintenanceOPR._Operation.OperationID, _Item.ItemID , dateTimePickerFaultDate.Value ,comboBoxFaultDesc .Text 
                        ,textBoxReport .Text );
                    
                    if (MaintenanceFault_ != null)
                    {
                        _MaintenanceFault = MaintenanceFault_;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        LoadForm(true );

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر اضافة العطل " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_MaintenanceFault != null)
                    {

                        bool success = new MaintenanceSQL.MaintenanceFaultSQL(DB).UpdateFault 
                            (_MaintenanceFault.FaultID, _Item.ItemID
                           , dateTimePickerFaultDate.Value, comboBoxFaultDesc.Text  .ToString () , textBoxReport.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //_MaintenanceFault = new MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(_MaintenanceFault.AccessoryID);
                            this.Changed_ = true;
                            LoadForm(true );
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

   
        #region Tags
        private void DeleteTag_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                    uint sid = Convert.ToUInt32(listViewTags.SelectedItems[0].Name);
                    bool success = new MaintenanceTagSQL(DB).DeleteMaintenanceTag(sid);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List (_MaintenanceFault );
                        RefreshTagList(FaultTagList_);

                    }
                    else
                    {
                        MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                

            }
            catch(Exception ee)
            {
                MessageBox.Show("DeleteTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void EditTag_MenuItem_Click(object sender, EventArgs e)
        {

       
            if (listViewTags.SelectedItems.Count > 0)
            {
                try
                {
                    string s = listViewTags.SelectedItems[0].Name.Substring(0, 1);
                    if (s == "D")
                    {
                        uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                        MaintenanceTag DiagnosticOPR_Fault_Tag_ = new MaintenanceTagSQL(DB).GetMaintenanceTaginfo_ByID(id);
                        DiagnosticOPR_Fault_TagForm DiagnosticOPR_Fault_TagForm_ = new DiagnosticOPR_Fault_TagForm(DB, DiagnosticOPR_Fault_Tag_, true);
                        DiagnosticOPR_Fault_TagForm_.ShowDialog();
                        if (DiagnosticOPR_Fault_TagForm_.DialogResult == DialogResult.OK)
                        {
                            FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List(_MaintenanceFault);
                            RefreshTagList(FaultTagList_);
                        }
                        DiagnosticOPR_Fault_TagForm_.Dispose();
                    }
                    else
                    {
                        uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                        MaintenanceTag Fault_MissedFaultItem_Tag_ = new MaintenanceTagSQL(DB).GetMaintenanceTaginfo_ByID (id);
                        Fault_MissedFaultItem_TagForm Fault_MissedFaultItem_TagForm_ = new Fault_MissedFaultItem_TagForm(DB, Fault_MissedFaultItem_Tag_, true);
                        Fault_MissedFaultItem_TagForm_.ShowDialog();
                        if (Fault_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                        {
                            FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List(_MaintenanceFault);
                            RefreshTagList(FaultTagList_);
                        }
                        Fault_MissedFaultItem_TagForm_.Dispose();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("EditTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void OpenTag_Operation_MenuItem_Click(object sender, EventArgs e)
        {


            if (listViewTags.SelectedItems.Count > 0)
            {
                try
                {
                    string s = listViewTags.SelectedItems[0].Name.Substring(0, 1);
                    if (s == "D")
                    {
                        uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].SubItems [1].Text );
                        DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID (id);
                        DiagnosticOPRForm DiagnosticOPRForm_ = new DiagnosticOPRForm(DB, DiagnosticOPR_, false);
                        DiagnosticOPRForm_.ShowDialog();

                        DiagnosticOPRForm_.Dispose();
                    }
                    else
                    {
                        uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].SubItems[1].Text);
                        Missed_Fault_Item Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(id);
                        MissedFault_Item_Form MissedFault_Item_Form_ = new MissedFault_Item_Form(DB, Missed_Fault_Item_, false);
                        MissedFault_Item_Form_.ShowDialog();

                        MissedFault_Item_Form_.Dispose();
                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show("OpenTag_Operation_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void OpenTag_MenuItem_Click(object sender, EventArgs e)
        {


            if (listViewTags.SelectedItems.Count > 0)
            {
                try
                {
                    string s = listViewTags.SelectedItems[0].Name.Substring(0, 1);
                    if (s == "D")
                    {
                        uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                        MaintenanceTag  DiagnosticOPR_Fault_Tag_ = new MaintenanceTagSQL(DB).GetMaintenanceTaginfo_ByID (id);
                        DiagnosticOPR_Fault_TagForm DiagnosticOPR_Fault_TagForm_ = new DiagnosticOPR_Fault_TagForm(DB, DiagnosticOPR_Fault_Tag_, false);
                        DiagnosticOPR_Fault_TagForm_.ShowDialog();
                        if (DiagnosticOPR_Fault_TagForm_.DialogResult == DialogResult.OK)
                        {
                            FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List(_MaintenanceFault);
                            RefreshTagList(FaultTagList_);
                        }
                        DiagnosticOPR_Fault_TagForm_.Dispose();
                    }
                    else
                    {
                        uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                        MaintenanceTag Fault_MissedFaultItem_Tag_ = new MaintenanceTagSQL(DB).GetMaintenanceTaginfo_ByID (id);
                        Fault_MissedFaultItem_TagForm Fault_MissedFaultItem_TagForm_ = new Fault_MissedFaultItem_TagForm(DB, Fault_MissedFaultItem_Tag_, false );
                        Fault_MissedFaultItem_TagForm_.ShowDialog();
                        if (Fault_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                        {
                            FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List(_MaintenanceFault);
                            RefreshTagList(FaultTagList_);
                        }
                        Fault_MissedFaultItem_TagForm_.Dispose();
                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show("OpenTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void AddDiagnosticOPRTag_MenuItem_Click(object sender, EventArgs e)
        {

    
            try
            {
                DiagnosticOPR_Fault_TagForm DiagnosticOPR_Fault_TagForm_ = new DiagnosticOPR_Fault_TagForm(DB, _MaintenanceFault );
                DialogResult d = DiagnosticOPR_Fault_TagForm_.ShowDialog();
                if (DiagnosticOPR_Fault_TagForm_.DialogResult == DialogResult.OK)
                {
                    FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List(_MaintenanceFault);
                    RefreshTagList(FaultTagList_);
                }
                DiagnosticOPR_Fault_TagForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddDiagnosticOPRTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AddMissedFaultItemTag_MenuItem_Click(object sender, EventArgs e)
        {
  
        
            try
            {
                Fault_MissedFaultItem_TagForm Fault_MissedFaultItem_TagForm_ = new Fault_MissedFaultItem_TagForm(DB, _MaintenanceFault );
                DialogResult d = Fault_MissedFaultItem_TagForm_.ShowDialog();
                if (Fault_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                {
                    FaultTagList_ = new MaintenanceTagSQL(DB).Get_Fault_Tag_List(_MaintenanceFault);
                    RefreshTagList(FaultTagList_);
                }
                Fault_MissedFaultItem_TagForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddMissedFaultItemTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private async void RefreshTagList(List<MaintenanceTag> TagSummaryList)
        {
            try
            {
                listViewTags.Items.Clear();
                for (int i = 0; i < TagSummaryList.Count; i++)
                {
                    if(TagSummaryList[i]._DiagnosticOPR!=null )
                    {
                        ListViewItem ListViewItem_ = new ListViewItem("ربط مع عملية فحص");

                        ListViewItem_.Name ="D"+ TagSummaryList[i].TagID.ToString();
                        //ListViewItem_.SubItems.Add(TagSummaryList[i].TagID.ToString());
                        ListViewItem_.SubItems.Add(TagSummaryList[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
                        ListViewItem_.SubItems.Add(TagSummaryList[i]._DiagnosticOPR.Desc);
                        ListViewItem_.SubItems.Add(TagSummaryList[i].TagInfo);
                        ListViewItem_.BackColor = Color.PaleGoldenrod;
                        listViewTags.Items.Add(ListViewItem_);

                    }
                    else if(TagSummaryList[i]._Missed_Fault_Item != null)
                    {
                        ListViewItem ListViewItem_ = new ListViewItem("ربط مع عنصر مفقود او تالف");

                        ListViewItem_.Name = "M" + TagSummaryList[i].TagID.ToString();
                        //ListViewItem_.SubItems.Add(TagSummaryList[i].TagID.ToString());
                        ListViewItem_.SubItems.Add(TagSummaryList[i]._Missed_Fault_Item.ID .ToString());
                        ListViewItem_.SubItems.Add(TagSummaryList[i]._Missed_Fault_Item._Item.GetItemFullName()+
                            " ,Location :"+ TagSummaryList[i]._Missed_Fault_Item.Location );
                        ListViewItem_.SubItems.Add(TagSummaryList[i].TagInfo);
                        ListViewItem_.BackColor = Color.PaleGoldenrod;
                        listViewTags.Items.Add(ListViewItem_);
                    }
                    

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshTagList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private void listViewTag_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewTags.SelectedItems.Count > 0)
            {
                OpenTag_MenuItem.PerformClick();
            }
        }
        private void listViewTag_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewTags.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewTags.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenTag_Operation_MenuItem, OpenTag_MenuItem, UpdateTag_MenuItem, DeleteTag_MenuItem, new MenuItem("-"), AddDiagnosticOPRTag_MenuItem, AddMissedFaultItemTag_MenuItem };
                        listViewTags.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddDiagnosticOPRTag_MenuItem, AddMissedFaultItemTag_MenuItem };
                        listViewTags.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewTag_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        public async void AdjustlistViewTag_ColumnsWidth()
        {
            //try
            //{
            //    listViewTags.Columns[0].Width = 150;
            //    listViewTags.Columns[1].Width = 60;
            //    listViewTags.Columns[2].Width = 150;
            //    listViewTags.Columns[3].Width = listViewTags.Width - 380;

            //}
            //catch (Exception ee)
            //{
            //    System.Windows.Forms.MessageBox.Show("AdjustlistViewMissed_Fault_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //}
        }
        private void listViewTags_Resize(object sender, EventArgs e)
        {
            AdjustlistViewTag_ColumnsWidth();
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
                    ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].InstalledItem_Count.ToString());
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].TestInstallOPR_Count.ToString());
                    listViewRepairOPR.Items.Add(ListViewItem_);
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshRepairOPRList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        private void Delete_Affictive_RepairOPR_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewRepairOPR.SelectedItems[0].Name);
                bool success = new MaintenanceFaultSQL (DB).Fault_Delete_Affictive_RepairOPR(_MaintenanceFault.FaultID , sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<RepairOPR> Affictive_RepairOPRList = new MaintenanceFaultSQL(DB).GetFault_Affictive_RepairOPR_List(_MaintenanceFault.FaultID);
                    RefreshRepairOPRList(Affictive_RepairOPRList);

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_Affictive_RepairOPR_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }
        private void Add_Affictive_RepairOPR_Click(object sender, EventArgs e)
        {
            try
            {
                SelecObjectForm SelecObjectForm_ = new SelecObjectForm("اختر عملية اصبلاح");
                List<RepairOPR > RepairOPRList = new RepairOPRSQL(DB).Get_MaintenanceOPR_RepairOPR_List(_MaintenanceOPR);
                RepairOPR.InitializeRepairOPRListViewColumns(ref SelecObjectForm_._listView);
                RepairOPR.RefreshRepairOPRList(ref SelecObjectForm_._listView, RepairOPRList);
                SelecObjectForm_.adjustcolumns = f => RepairOPR.AdjustlistViewRepairOPRColumnsWidth (ref SelecObjectForm_._listView);
                SelecObjectForm_.ShowDialog();
                if (SelecObjectForm_.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        RepairOPR RepairOPR_= new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(SelecObjectForm_.ReturnID);
                        bool success = new MaintenanceFaultSQL(DB).Fault_Add_Affictive_RepairOPR(_MaintenanceFault.FaultID, RepairOPR_._Operation.OperationID);
                        if(success )
                        {
                            List<RepairOPR> Affictive_RepairOPRList = new MaintenanceFaultSQL(DB).GetFault_Affictive_RepairOPR_List(_MaintenanceFault.FaultID);
                            RefreshRepairOPRList(Affictive_RepairOPRList);
                        }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Failed_To_Get_ID" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("Add_Affictive_RepairOPR_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Open_Affictive_RepairOPR_Click(object sender, EventArgs e)
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
                MessageBox.Show("Open_Affictive_RepairOPR_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void listViewRepairOPR_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewRepairOPR.SelectedItems.Count > 0)
            {
                Open_Affictive_RepairOPR.PerformClick();
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


                        MenuItem[] mi1 = new MenuItem[] {Open_Affictive_RepairOPR, Delete_Affictive_RepairOPR, new MenuItem("-"), Add_Affictive_RepairOPR };
                        listViewRepairOPR.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { Add_Affictive_RepairOPR };
                        listViewRepairOPR.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewRepairOPR_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        #endregion
    }
}
