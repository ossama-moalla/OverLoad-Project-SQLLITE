using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
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
    public partial class AddItemRelationShip : Form
    {
        DatabaseInterface DB;
        Item SourceItem;
        Item AnotherItem;
        ItemRelationShipsSQL ItemRelationShipsSQL_;
        private  Relation _UsedRelation;
        private Folder _UsedFolder;
        public Relation UsedRelation
        {
            get { return _UsedRelation; }
            
        }
        public Folder UsedFolder
        {
            get { return _UsedFolder; }

        }
        public AddItemRelationShip(DatabaseInterface db,Item sourceitem, Folder LastUsedFolder, Relation LastUsedRelation)
        {
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة العناصر, لا يمكنك فتح هذه النافذة");

            _UsedFolder = LastUsedFolder;
            ItemRelationShipsSQL_ = new ItemRelationShipsSQL(DB);
            SourceItem = sourceitem;
            InitializeComponent();
            textBox_ItemType.Text  = SourceItem.folder.FolderName;
            textBoxItemName.Text  = SourceItem.ItemName;
            textBoxItemCompany.Text = SourceItem.ItemCompany ;

            //MessageBox .Show (LastUsedRelation == null ? "null" : LastUsedRelation.Value.ToString());
            List<Relation> RelationList = Relation.GetRealtions();
            int selectedindex = 0;
            for (int i = 0; i < RelationList.Count; i++)
            {
                if (LastUsedRelation != null)
                    if (RelationList[i].Value  == LastUsedRelation.Value )
                        selectedindex = i;
                ComboboxItem ComboboxItem_ = new ComboboxItem(RelationList[i].Name, RelationList[i].Value);
                comboBoxRelation.Items.Add(ComboboxItem_);
            }
            comboBoxRelation.SelectedIndex = selectedindex;
            this.textBox_Another_ItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Second_ItemID_KeyDown);
            this.textBox_Another_ItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Another_ItemID_MouseDoubleClick);

        }
        public AddItemRelationShip(DatabaseInterface db, Item sourceitem,Item AnotherItem_,bool Edit)
        {
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة العناصر, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            ItemRelationShipsSQL_ = new ItemRelationShipsSQL(DB);
            SourceItem = sourceitem;
            AnotherItem = AnotherItem_;
            textBox_ItemType.Text = SourceItem.folder.FolderName;
            textBoxItemName.Text = SourceItem.ItemName;
            textBoxItemCompany.Text = SourceItem.ItemCompany;

            LoadItemData();
            ButtonAdd.Name = "ButtonSave";
            ButtonAdd.Text  = "تعديل";
         
            ItemRelation ItemRelation_= ItemRelationShipsSQL_.GetItemRealtion(SourceItem, AnotherItem);
            List<Relation> RelationList = Relation.GetRealtions();
            int selectedindex=0;
            for (int i = 0; i < RelationList.Count; i++)
            {
                if (ItemRelation_ != null)
                    if (RelationList[i].Value == ItemRelation_.Relation_)
                        selectedindex = i;
                ComboboxItem ComboboxItem_ = new ComboboxItem(RelationList[i].Name, RelationList[i].Value);
                comboBoxRelation.Items.Add(ComboboxItem_);
            }
            comboBoxRelation.SelectedIndex = selectedindex;
            if(Edit)
            {
                this.textBox_Another_ItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Second_ItemID_KeyDown);
                this.textBox_Another_ItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox_Another_ItemID_MouseDoubleClick);
                this.textBox_Another_ItemID.ReadOnly = false ;
                textBoxNotes.ReadOnly = false ;
                comboBoxRelation.Enabled = true ;
            }
            else
            {
                comboBoxRelation.Enabled = false;
                this.textBox_Another_ItemID.ReadOnly = true;
                textBoxNotes.ReadOnly = true;
            }
           

        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (comboBoxRelation.SelectedIndex < 0)
            {
                MessageBox.Show("يرجى تحديد علاقة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
                    
            try
            {


                ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxRelation.SelectedItem;
                _UsedRelation = Relation .GetRealtionByValue (ComboboxItem_.Value );
                _UsedFolder = AnotherItem.folder;
                if (ButtonAdd .Name == "ButtonAdd")
                {
                    if (ItemRelationShipsSQL_.AddItemRelation (SourceItem, AnotherItem, ComboboxItem_.Value , textBoxNotes.Text))
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageBox.Show("تمت الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("فشل اضافة البيانات", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    if (ItemRelationShipsSQL_.UpdateItemRelation (SourceItem, AnotherItem, ComboboxItem_.Value , textBoxNotes.Text))
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("فشل التعديل", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
               
              

            }
            catch(Exception ee)
            {
                MessageBox.Show("Error:"+ee.Message ,"خطا",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

        }

        private void textBox_Second_ItemID_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    uint itemid = Convert.ToUInt32(textBox_Another_ItemID.Text);
                    Item item__ = new ItemSQL(DB).GetItemInfoByID(itemid);
                    if (item__ != null)
                    {
                        AnotherItem = item__;
                        LoadItemData();
                    }
                    else
                    {
                        CleartemData ();
                        MessageBox.Show("لم يتم العثور على العنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
        }
        private void LoadItemData()
        {
            if (AnotherItem == null) return;
            textBox_Another_ItemID.Text = AnotherItem.ItemID.ToString();
            textBox_Another_Type.Text = AnotherItem.folder.FolderName;
            textBox_Another_ItemName.Text = AnotherItem.ItemName ;
            textBox_Another_ItemCompany.Text = AnotherItem.ItemCompany ;

        }
        private void CleartemData()
        {
            AnotherItem = null;
            textBox_Another_Type.Text = "";
            textBox_Another_ItemName.Text ="";
            textBox_Another_ItemCompany.Text = "";
        }
        private void textBox_Another_ItemID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ItemObj.Forms.User_ShowItemsForm  form = new ItemObj.Forms.User_ShowItemsForm(DB, _UsedFolder, User_ShowItemsForm.SELECT_ITEM);
                DialogResult dd = form.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    AnotherItem   = form.ReturnItem;
                    LoadItemData();
                }
            }
        }

        private void comboBoxRelation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxRelation.SelectedItem;
            switch (ComboboxItem_ .Value )
            {
                //case Relation.ITEM_EQUAL:
                //    panelRelationType .BackColor = Color.LimeGreen; break;
                case Relation.ITEM_CONTAIN:
                    panelRelationType.BackColor = Color.PaleTurquoise; break;
                case Relation.ITEM_FOUNDIN:
                    panelRelationType.BackColor = Color.MistyRose; break;
            }
             
        }
    }
   
}
