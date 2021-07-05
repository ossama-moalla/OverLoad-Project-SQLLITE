using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace OverLoad_Client.ItemObj.Forms
{
    public partial class FolderForm : Form
    {
        uint? ParentFolderID;
        Folder Folder_;
        FolderSQL foldersql;
        DatabaseInterface DB;
        ItemSpecSQL ItemSpecSQL_;
        ItemSpec_Restrict_SQL ItemSpec_Restrict_SQL_;
        MenuItem UpdateSpe;
        MenuItem DeleteSpec;
        MenuItem ShowOptions;
        ListViewItem SelectedItem;
        public bool _Changed;
        public bool Changed
        {
            get { return _Changed; }
        }
        public FolderForm(DatabaseInterface db,uint? pfolder,string foldername)
        {
            InitializeComponent();
            DB = db;
            ParentFolderID = pfolder;
            foldersql = new FolderSQL(DB);
            ItemSpec_Restrict_SQL_ = new ItemSpec_Restrict_SQL(DB);
            ItemSpecSQL_ = new ItemSpecSQL(DB);
            TextBoxFolderName.Text = foldername;
            this.TextBoxFolderName.SelectionStart = 0;
            this.TextBoxFolderName.SelectionLength = foldername.Length;
            TextBoxFolderName.Focus();
            panelSpec.Enabled = false;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
    
        public FolderForm(DatabaseInterface db, Folder Folder__,bool Edit)
        {
            InitializeComponent();
            DB = db;
            foldersql = new FolderSQL(DB);
            ItemSpec_Restrict_SQL_ = new ItemSpec_Restrict_SQL(DB);
            ItemSpecSQL_ = new ItemSpecSQL(DB);
            Folder_ = Folder__;
   
           
            loadForm(Edit);
        }
        public void loadForm(bool edit)
        {


            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (Folder_ != null)
            {
                UpdateSpe = new MenuItem("تعديل", UpdateSpec_MenuItem_Click);
                DeleteSpec = new MenuItem("حذف", DeleteSpec_MenuItem_Click);
                ShowOptions = new MenuItem("اظهار القيم التابعة لهذه الخاصية", ShowOptions_MenuItem_Click);

                comboBoxSpecType.SelectedIndex = 0;
                FillListview();
                panelSpec.Enabled = true ;
                this.TextBoxFolderName.Text = Folder_.FolderName;
                this.TextBoxFolderName.SelectionStart = 0;
                this.TextBoxFolderName.SelectionLength = Folder_.FolderName.Length;
                textBoxdefault_Consume_Unit.Text = Folder_.DefaultConsumeUnit;
                TextBoxFolderName.Focus();
                if (edit)
                {
                    TextBoxFolderName.ReadOnly = false ;
                    textBoxdefault_Consume_Unit.ReadOnly = false ;
                    this.listViewSpecs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSpecs_MouseDoubleClick);
                    this.listViewSpecs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewSpecs_MouseDown);
                    panelSpecAdd.Enabled = true ;
                    buttonSave.Visible = true ;
                }
                else
                {
                this.listViewSpecs.Resize += new System.EventHandler(this.listViewSpecs_Resize);
                    TextBoxFolderName.ReadOnly = true ;
                    textBoxdefault_Consume_Unit.ReadOnly = true ;
                    panelSpecAdd.Enabled = false ;
                    buttonSave.Visible = false ;
                }

            }
        }
 


        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (TextBoxFolderName.Text.Length == 0)
            { MessageBox.Show("اسم المجلد يجب ان لا يكون فارغا"); return; }
            if (buttonSave.Name == "buttonAdd")
            {
                Folder Folder__  = foldersql.CreateFolder(TextBoxFolderName.Text, ParentFolderID,textBoxdefault_Consume_Unit.Text );
                if (Folder__ != null)
                {
                    Folder_ = Folder__;

                    MessageBox.Show("تم انشاء الصنف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this._Changed   = true  ;
                    loadForm(true);

                }
            }
           else
            {
                bool r = foldersql.UpdateFolder (Folder_ , TextBoxFolderName.Text, textBoxdefault_Consume_Unit.Text);
                if (r == true)
                {
                    MessageBox.Show("تم الحفظ بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this._Changed = true;
                    this.Close();
                }
            }
        }
        #region  FolderSpec
        public void FillListview()
        {
            listViewSpecs.Items.Clear();
            List<ItemSpecDisplay> ItemSpecDisplayList = new List<ItemSpecDisplay>();
            List<ItemSpec> ItemSpecList = ItemSpecSQL_.GetItemSpecList(Folder_);
            List<ItemSpec_Restrict> ItemSpec_Restrict_List = ItemSpec_Restrict_SQL_.GetItemSpecRestrictList(Folder_);
            ItemSpec_Restrict_Options_SQL ItemSpec_Restrict_Options_SQL = new ItemSpec_Restrict_Options_SQL(DB);
            for (int i = 0; i < ItemSpec_Restrict_List.Count; i++)
            {
                ItemSpecDisplayList.Add(new ItemSpecDisplay(Folder_, ItemSpec_Restrict_List[i].SpecID, ItemSpec_Restrict_List[i].SpecName, ItemSpec_Restrict_List[i].SpecIndex, false));
            }
            for (int i = 0; i < ItemSpecList.Count; i++)
            {
                ItemSpecDisplayList.Add(new ItemSpecDisplay(Folder_, ItemSpecList[i].SpecID, ItemSpecList[i].SpecName, ItemSpecList[i].SpecIndex, true));
            }
            ItemSpecDisplayList = ItemSpecDisplayList.OrderBy(m => m.SpecIndex).ToList();
            if (ItemSpecDisplayList.Count > 0)
                textBoxIndex.Text = (ItemSpecDisplayList[ItemSpecDisplayList.Count - 1].SpecIndex + 1).ToString();
            else
                textBoxIndex.Text = "1";
            for (int i = 0; i < ItemSpecDisplayList.Count; i++)
            {
                ListViewItem item = new ListViewItem(ItemSpecDisplayList[i].SpecName);
                if (ItemSpecDisplayList[i].Spectype == false)
                {
                    item.Name = "0" + ItemSpecDisplayList[i].SpecID.ToString();
                    item.SubItems.Add(" مقيدة");

                }
                else
                {
                    item.Name = "1" + ItemSpecDisplayList[i].SpecID.ToString();
                    item.SubItems.Add("غير مقيدة");
                }
                item.SubItems.Add(ItemSpecDisplayList[i].SpecIndex.ToString());
                listViewSpecs.Items.Add(item);
            }

        }
        private void ADD_Click(object sender, EventArgs e)
        {
            if (textBoxSpecName.Text.Length == 0)
            {
                MessageBox.Show("ادخل اسم الخاصية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Button_Spec_Add.Name == "Button_Spec_Add")
            {

                if (comboBoxSpecType.SelectedIndex == 0)
                {
                    uint index;
                    try
                    {
                        index = Convert.ToUInt32(textBoxIndex.Text);

                    }
                    catch
                    {
                        MessageBox.Show("الفهرس يجب ان يكون عدد صحيح موجب", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    bool result = ItemSpecSQL_.AddItemSpec(Folder_, textBoxSpecName.Text, index);

                    if (result)
                    {
                        FillListview();
                        textBoxSpecName.Text = "";
                    }

                }
                else
                {
                    uint index;
                    try
                    {
                        index = Convert.ToUInt32(textBoxIndex.Text);

                    }
                    catch
                    {
                        MessageBox.Show("الفهرس يجب ان يكون عدد صحيح موجب", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    bool result = ItemSpec_Restrict_SQL_.AddItemSpecRestrict(Folder_, textBoxSpecName.Text, index);
                    if (result)
                    {
                        FillListview();
                        textBoxSpecName.Text = "";
                    }

                }
            }
            else
            {
                if (Button_Spec_Add.Name == "Button_Spec_Update")
                {
                    bool result = false;
                    uint specid = Convert.ToUInt32(SelectedItem.Name.Substring(1));
                    uint index;
                    try
                    {
                        index = Convert.ToUInt32(textBoxIndex.Text);

                    }
                    catch
                    {
                        MessageBox.Show("الفهرس يجب ان يكون عدد صحيح موجب", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (SelectedItem.Name.Substring(0, 1) == "0")
                    { result = ItemSpec_Restrict_SQL_.UpdatetemSpecRestrict(specid, textBoxSpecName.Text, index); }
                    else
                    { result = ItemSpecSQL_.UpdatetemSpec(specid, textBoxSpecName.Text, index); }
                    if (result)
                    {
                        Button_Spec_Add.Name = "Button_Spec_Add";
                        Button_Spec_Add.Text = "اضافة";
                        listViewSpecs.Enabled = true;
                        textBoxSpecName.Text = "";
                        comboBoxSpecType.SelectedIndex = 0;
                        comboBoxSpecType.Enabled = true;

                        FillListview();
                    }
                    else
                    { }

                }
            }

        }
        private void listViewSpecs_MouseDown(object sender, MouseEventArgs e)
        {
            listViewSpecs.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewSpecs.Items)
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
                    MenuItem[] mi1;
                    if (listitem.Name.Substring(0, 1) == "0")
                        mi1 = new MenuItem[] { ShowOptions, UpdateSpe, DeleteSpec };
                    else
                        mi1 = new MenuItem[] { UpdateSpe, DeleteSpec };
                    listViewSpecs.ContextMenu = new ContextMenu(mi1);
                }

            }
        }
        private void UpdateSpec_MenuItem_Click(object sender, EventArgs e)
        {
            Button_Spec_Add.Name = "Button_Spec_Update";
            Button_Spec_Add.Text = "تعديل";
            Button_Spec_Cancel.Visible  = true ;
            listViewSpecs.Enabled = false;
            SelectedItem = listViewSpecs.SelectedItems[0];
            textBoxSpecName.Text = listViewSpecs.SelectedItems[0].Text;
            textBoxIndex.Text = listViewSpecs.SelectedItems[0].SubItems[2].Text;

            if (listViewSpecs.SelectedItems[0].Name.Substring(0, 1) == "0")
                comboBoxSpecType.SelectedIndex = 1;
            else
                comboBoxSpecType.SelectedIndex = 0;
            comboBoxSpecType.Enabled = false;

        }
        private void DeleteSpec_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewSpecs.SelectedItems.Count > 0)
            {
                MessageBox.Show("هل انت متأكد من الحذف", "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                for (int i = 0; i < listViewSpecs.SelectedItems.Count; i++)
                {
                    bool result = false;
                    uint specid = Convert.ToUInt32(listViewSpecs.SelectedItems[i].Name.Substring(1));
                    if (listViewSpecs.SelectedItems[i].Name.Substring(0, 1) == "0")
                    { result = ItemSpec_Restrict_SQL_.DeleteItemSpecRestrict(specid); }
                    else
                    { result = ItemSpecSQL_.DeleteItemSpec(specid); }
                }
            }
            FillListview();

        }
        private void ShowOptions_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewSpecs.SelectedItems.Count > 0)
            {
                if (listViewSpecs.SelectedItems[0].Name.Substring(0, 1) == "0")
                {
                    uint specid = Convert.ToUInt32(listViewSpecs.SelectedItems[0].Name.Substring(1));
                    //string SpecName = listViewSpecs.SelectedItems[0].Text;
                    //uint index = Convert.ToUInt32(listViewSpecs.SelectedItems[0].Name.Substring(2));

                    ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict_SQL(DB).GetItemSpecRestrictInfoByID(specid);
                        //(Folder_, specid, SpecName, index);
                    Form_Spec_Option Form_Spec_Option = new Form_Spec_Option(DB, ItemSpec_Restrict_);
                    Form_Spec_Option.ShowDialog();
                }
            }
        }
     

        private void listViewSpecs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewSpecs.SelectedItems.Count > 0)
            {
                if (listViewSpecs.SelectedItems[0].Name.Substring(0, 1) == "0")
                {
                    uint specid = Convert.ToUInt32(listViewSpecs.SelectedItems[0].Name.Substring(1));
                    //string SpecName = listViewSpecs.SelectedItems[0].Text;
                    //uint index = Convert.ToUInt32(listViewSpecs.SelectedItems[0].SubItems [2].Text );
                    ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict_SQL(DB).GetItemSpecRestrictInfoByID(specid);

                    Form_Spec_Option Form_Spec_Option = new Form_Spec_Option(DB, ItemSpec_Restrict_);
                    Form_Spec_Option.ShowDialog();
                }
            }
        }

        private void listViewSpecs_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewSpecs.Columns.Count; i++)
            {
                listViewSpecs.Columns[i].Width = listViewSpecs.Width / 3;
            }
        }
        #endregion

        private void Button_Spec_Cancel_Click(object sender, EventArgs e)
        {

            Button_Spec_Cancel.Visible = false;

            listViewSpecs.Enabled = true;
            Button_Spec_Add.Name = "Button_Spec_Add";
            Button_Spec_Add.Text = "أضف";
            textBoxSpecName.Text = "";
            comboBoxSpecType.SelectedIndex = 0;
            comboBoxSpecType.Enabled = true;
        }
    }
}
