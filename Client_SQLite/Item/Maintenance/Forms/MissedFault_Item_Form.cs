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
    public partial class MissedFault_Item_Form : Form
    {
        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        DiagnosticOPR _DiagnosticOPR;
        Missed_Fault_Item _Missed_Fault_Item;
        Folder LastUsedFolder;
        Item _Item;
        List<MaintenanceTag> _TagSummaryList = new List<MaintenanceTag>();


        MenuItem OpenTag_MenuItem;
        MenuItem AddDiagnosticOPRTag_MenuItem;
        MenuItem AddFaultTag_MenuItem;
        MenuItem UpdateTag_MenuItem;
        MenuItem DeleteTag_MenuItem;
        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public MissedFault_Item_Form(DatabaseInterface db, DiagnosticOPR DiagnosticOPR_)
        {

            DB = db;
            _DiagnosticOPR = DiagnosticOPR_;
            _MaintenanceOPR = _DiagnosticOPR. _MaintenanceOPR;
            InitializeComponent();
            textBoxContact.Text = _MaintenanceOPR._Contact.Get_Complete_ContactName_WithHeader();
            textBoxMOPR.Text = _MaintenanceOPR._Operation.OperationID.ToString();
            dateTimePickerEntryDate.Value = _MaintenanceOPR.EntryDate ;
            comboBoxType.SelectedIndex = 0;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "اضافة";
            this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
            this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);

        }
        public MissedFault_Item_Form(DatabaseInterface db, Missed_Fault_Item Missed_Fault_Item_, bool Edit)
        {
            DB = db;
            _Missed_Fault_Item = Missed_Fault_Item_;
            _MaintenanceOPR = _Missed_Fault_Item._DiagnosticOPR._MaintenanceOPR;
            InitializeComponent();

            LoadForm(Edit);
        }
        public void LoadForm(bool Edit)
        {
            try
            {
                if (_Missed_Fault_Item  == null) return;
                OpenTag_MenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل الرابط", OpenTag_MenuItem_Click);
                AddDiagnosticOPRTag_MenuItem = new System.Windows.Forms.MenuItem("ربط مع عطل", AddDiagnosticOPRTag_MenuItem_Click);
                AddFaultTag_MenuItem = new System.Windows.Forms.MenuItem("ربط مع عطل", AddMissedFaultItemTag_MenuItem_Click);
                UpdateTag_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditTag_MenuItem_Click);
                DeleteTag_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteTag_MenuItem_Click);

                _TagSummaryList = new MaintenanceTagSQL(DB).GetMissedFaultItem_TagList(_Missed_Fault_Item);
                RefreshTagList(_TagSummaryList);
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                _Item = _Missed_Fault_Item._Item;
                _MaintenanceOPR = _Missed_Fault_Item._DiagnosticOPR ._MaintenanceOPR;
                if (_Missed_Fault_Item.Type == Missed_Fault_Item.FAULT_ITEM)
                    comboBoxType.SelectedIndex = 0;
                else
                    comboBoxType.SelectedIndex = 1;
                textBoxLocation.Text = _Missed_Fault_Item.Location;
                textBoxNotes.Text = _Missed_Fault_Item.Notes;

                textBoxContact.Text = _MaintenanceOPR._Contact.Get_Complete_ContactName_WithHeader();
                textBoxMOPR.Text = _MaintenanceOPR._Operation.OperationID.ToString();
                dateTimePickerEntryDate.Value = _MaintenanceOPR.EntryDate ;
                LoadItemData();

                if (Edit)
                {
                    this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
                    this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
                    buttonSave.Visible = true;
                    textBoxItemID.ReadOnly = false;
                    comboBoxType.Enabled = true; 
                    textBoxLocation.ReadOnly  = false ;
                    textBoxNotes.ReadOnly = false ;



                }
                else
                {
                    this.listViewTags.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewTag_MouseDoubleClick);
                    this.listViewTags.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewTag_MouseDown);

                    buttonSave.Visible = false;
                    textBoxItemID.ReadOnly = true;
                    comboBoxType.Enabled = false ;
                    textBoxLocation.ReadOnly = true ;
                    textBoxNotes.ReadOnly = true ;

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("حصل خطأ اثناء تحميل الصفحة:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private async void LoadItemData()
        {
            try
            {

            }catch (Exception ee)
            {
                MessageBox.Show(":"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
            LastUsedFolder = _Item.folder;
            textBoxItemID.Text = _Item.ItemID.ToString();
            textBoxItemName.Text = _Item.ItemName;
            textBoxItemCompany.Text = _Item.ItemCompany;
            textBoxItemType.Text = _Item.folder.FolderName;

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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_MaintenanceOPR == null) return;
            //ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
            //ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

            //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
            //string SellType_ = ComboboxItem_selltype.Text;
            if (_Item == null)
            {

                MessageBox.Show("يرجى تحديد المادة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxType.SelectedIndex < 0)
            {

                MessageBox.Show("يرجى تحديد ان كان تالف او مفقود", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool type;
            if (comboBoxType.SelectedIndex == 0)
                type = Missed_Fault_Item.FAULT_ITEM;
            else
                type = Missed_Fault_Item.MISSED_ITEM;
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    Missed_Fault_Item Missed_Fault_Item_ =
                        new MaintenanceSQL.MissedFaultItemSQL  (DB).AddMissedFaultItem  
                        (_DiagnosticOPR.DiagnosticOPRID, _Item.ItemID, type, 
                        textBoxLocation .Text ,textBoxNotes .Text );

                    if (Missed_Fault_Item_ != null)
                    {
                        _Missed_Fault_Item = Missed_Fault_Item_;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر الاضافة " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_Missed_Fault_Item  != null)
                    {

                        bool success = new MaintenanceSQL.MissedFaultItemSQL  (DB).UpdateMissed_Fault_Item  
                            (_Missed_Fault_Item.ID , _Item.ItemID
                            , type,
                             textBoxLocation.Text, textBoxNotes.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //_MaintenanceFault = new MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(_MaintenanceFault.AccessoryID);
                            this.Changed_ = true;
                            this.Close();
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
                string s = listViewTags.SelectedItems[0].Name;
                if (s == "D")
                {
                    uint sid = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                    bool success = new MaintenanceTagSQL(DB).DeleteMaintenanceTag  (sid);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _TagSummaryList = new MaintenanceTagSQL(DB).GetMissedFaultItem_TagList(_Missed_Fault_Item);
                        RefreshTagList(_TagSummaryList);

                    }
                    else
                    {
                        MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                

            }

            catch (Exception ee)
            {
                MessageBox.Show("DeleteTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void EditTag_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewTags.SelectedItems.Count > 0)
                {

                    string s = listViewTags.SelectedItems[0].Name.Substring(0, 1);
                    if (s == "D")
                    {
                        //uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                        //DiagnosticOPR_MissedFaultItem_Tag DiagnosticOPR_MissedFaultItem_Tag_ = new DiagnosticOPR_MissedFaultItem_TagSQL(DB).Get_DiagnosticOPR_MissedFaultItem_Tag_info_ByID(id);
                        //DiagnosticOPR_MissedFaultItem_TagForm DiagnosticOPR_MissedFaultItem_TagForm_ = new DiagnosticOPR_MissedFaultItem_TagForm(DB, DiagnosticOPR_MissedFaultItem_Tag_, true);
                        //DiagnosticOPR_MissedFaultItem_TagForm_.ShowDialog();
                        //if (DiagnosticOPR_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                        //{
                        //    _TagSummaryList = new MaintenanceTagSQL(DB).GetMissedFaultItem_TagList(_Missed_Fault_Item);
                        //    RefreshTagList(_TagSummaryList);
                        //}
                        //DiagnosticOPR_MissedFaultItem_TagForm_.Dispose();
                    }
                    else
                    {
                        uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                        MaintenanceTag MaintenanceTag_ = new MaintenanceTagSQL(DB).GetMaintenanceTaginfo_ByID(id);
                        Fault_MissedFaultItem_TagForm Fault_MissedFaultItem_TagForm_ = new Fault_MissedFaultItem_TagForm(DB, MaintenanceTag_, true);
                        Fault_MissedFaultItem_TagForm_.ShowDialog();
                        if (Fault_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                        {
                            _TagSummaryList = new MaintenanceTagSQL(DB).GetMissedFaultItem_TagList(_Missed_Fault_Item);
                            RefreshTagList(_TagSummaryList);
                        }
                        Fault_MissedFaultItem_TagForm_.Dispose();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void OpenTag_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewTags.SelectedItems.Count > 0)
                {

                    string s = listViewTags.SelectedItems[0].Name;

                    uint id = Convert.ToUInt32(listViewTags.SelectedItems[0].Name.Substring(1));
                    MaintenanceTag MaintenanceTag_ = new MaintenanceTagSQL(DB).GetMaintenanceTaginfo_ByID(id);
                    Fault_MissedFaultItem_TagForm Fault_MissedFaultItem_TagForm_ = new Fault_MissedFaultItem_TagForm(DB, MaintenanceTag_, false);
                    Fault_MissedFaultItem_TagForm_.ShowDialog();
                    if (Fault_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                    {
                        _TagSummaryList = new MaintenanceTagSQL(DB).GetMissedFaultItem_TagList(_Missed_Fault_Item);
                        RefreshTagList(_TagSummaryList);
                    }
                    Fault_MissedFaultItem_TagForm_.Dispose();



                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenTag_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void AddDiagnosticOPRTag_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //DiagnosticOPR_MissedFaultItem_TagForm DiagnosticOPR_MissedFaultItem_TagForm_ = new DiagnosticOPR_MissedFaultItem_TagForm(DB, _Missed_Fault_Item);
                //DialogResult d = DiagnosticOPR_MissedFaultItem_TagForm_.ShowDialog();
                //if (DiagnosticOPR_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                //{
                //    _TagSummaryList = new MaintenanceTagSQL(DB).GetMissedFaultItem_TagList(_Missed_Fault_Item);
                //    RefreshTagList(_TagSummaryList);
                //}
                //DiagnosticOPR_MissedFaultItem_TagForm_.Dispose();
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
                Fault_MissedFaultItem_TagForm Fault_MissedFaultItem_TagForm_ = new Fault_MissedFaultItem_TagForm(DB, _Missed_Fault_Item);
                DialogResult d = Fault_MissedFaultItem_TagForm_.ShowDialog();
                if (Fault_MissedFaultItem_TagForm_.DialogResult == DialogResult.OK)
                {
                    _TagSummaryList = new MaintenanceTagSQL(DB).GetMissedFaultItem_TagList(_Missed_Fault_Item);
                    RefreshTagList(_TagSummaryList);
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

                    ListViewItem ListViewItem_ = new ListViewItem(TagSummaryList[i].TagID.ToString());

                    ListViewItem_.Name = TagSummaryList[i].TagID.ToString();
                    ListViewItem_.SubItems.Add(TagSummaryList[i]._MaintenanceFault.FaultID.ToString());
                    ListViewItem_.SubItems.Add(TagSummaryList[i]._MaintenanceFault.FaultDesc);
                    ListViewItem_.SubItems.Add(TagSummaryList[i].TagInfo);
                    listViewTags.Items.Add(ListViewItem_);

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


                        MenuItem[] mi1 = new MenuItem[] { OpenTag_MenuItem, UpdateTag_MenuItem, DeleteTag_MenuItem, new MenuItem("-"), AddDiagnosticOPRTag_MenuItem, AddFaultTag_MenuItem };
                        listViewTags.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddDiagnosticOPRTag_MenuItem, AddFaultTag_MenuItem };
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
            try
            {
                listViewTags.Columns[0].Width = 150;
                listViewTags.Columns[1].Width = 60;
                listViewTags.Columns[2].Width = 150;
                listViewTags.Columns[3].Width = listViewTags.Width - 380;

            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewMissed_Fault_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void listViewTags_Resize(object sender, EventArgs e)
        {
            AdjustlistViewTag_ColumnsWidth();
        }
        #endregion

    }
}
