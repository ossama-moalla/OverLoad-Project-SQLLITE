using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.Forms;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Maintenance.MaintenanceSQL;

namespace OverLoad_Client.ItemObj.Forms
{
    public partial class ItemForm : Form
    {
        
        Item item;
        DatabaseInterface DB;
        private ItemRelationShipsSQL ItemRelationShipsSQL_;
        
        MenuItem AddRelation;
        MenuItem OpenRelation;
        MenuItem ItemRelation_OpenAnotherItemPage;
        MenuItem UpdateRelation;
        MenuItem DeleteRelation;
        MenuItem AddConsumUnit;
        MenuItem UpdateConsumUnit;
        MenuItem deleteConsumUnit;
        MenuItem SetItemSpecValue;
        MenuItem UNSetItemSpecValue;
        MenuItem SetItemImage_MenuItem;
        MenuItem UnsetItemImage_MenuItem;

        MenuItem OpenFile_MenuItem;
        MenuItem AddFile_MenuItem;
        MenuItem SaveFile_MenuItem;
        MenuItem UpdateFileInfo_MenuItem;
        MenuItem DeleteFile_MenuItem;

        MenuItem Add_TO_Equivalence_Group_MenuItem;
        MenuItem Delete_From_Equivalence_Group_MenuItem;
        MenuItem Open_Equivalence_Group_MenuItem;

        List<ConsumeUnit> ConsumUnitsList=new List<ConsumeUnit> ();
        List<SellType> selltypelist = new List<SellType>();
        List<TradeState> TradeStateList = new List<TradeState>();
        bool  ItemImage_Set ;
        string LastPath;
        Relation _LastUsedRelation;
        Folder _LastUsedFolder;
        public ItemForm(DatabaseInterface db,Item Item_)
        {
         
            DB = db;
            LastPath = Application.StartupPath;
            item = Item_;
            this.Text = item.GetItemFullName();
            ItemRelationShipsSQL_ = new ItemRelationShipsSQL(DB);
    
            InitializeComponent();
            if(DB.IS_Belong_To_Admin_Group(DB.__User .UserID ) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))
            {
                this.listViewConsumUnits.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewConsumUnits_MouseDown);
                this.pictureBoxItemImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxItemImage_MouseDown);
                this.listView_Equivalence_Group.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_Equivalence_Group_MouseDown);
                this.listViewItemRelations.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewItemRelations_MouseDown);
                this.listViewItemFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewItemFiles_MouseDown);
                this.listViewItemSpec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewItemSpec_MouseDown);
            }
                

            this.Text = item .folder .FolderName +" : "+item .ItemCompany  +" - "+item .ItemName  ;
            Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, defaultcurrency);
            ProgramGeneralMethods.FillComboBoxTradeState(ref comboBoxTradestate, DB, null);
          textBoxExchangeRate .Text = defaultcurrency.ExchangeRate.ToString();

            Relation.FillComboBox(ref comboBoxFilterRelations);
            textBoxItemID.Text = item.ItemID.ToString();
            textBoxFolderName.Text = item.folder.FolderName;
            textBoxItemName.Text = item.ItemName;
            textBoxItemCompany.Text = item.ItemCompany;
            textBoxMarketCode.Text = item.MarketCode;
            TextBoxDefaultConsumeUnit.Text = item.DefaultConsumeUnit;
            InitializeMenuItems();
            this.comboBoxFilterRelations.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterRelations_SelectedIndexChanged);


            try
            {
                SellTypeSql STS = new SellTypeSql(DB);
                TradeStateList = new TradeStateSQL(DB).GetTradeStateList();
                selltypelist = STS.GetSellTypeList();
                for (int i = 0; i < selltypelist.Count; i++)
                {
                    dataGridView1.Columns.Add(selltypelist[i].SellTypeID.ToString(), selltypelist[i].SellTypeName);
                }
            }
        
            catch
            {
                MessageBox.Show("فشل في جلب قائمة انماط البيع","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
            
            dataGridView1.EnableHeadersVisualStyles = true;
            AdjustmentDatagridviewColumnsWidth();
            UpdateConsumeUnitsList();
            FillItemSpec();
            FillItemRelations();

            GetItemImage();
            GetItemFilesList();
            ConfifureItemFilesListViewColumnsWidth();
            if (comboBoxTradestate.Items.Count == 0)
            {
                groupBox1.Text = groupBox1.Text + "-- انماط التسعير او حالات بيع شارء العنصر غير مضبوطة";
                groupBox1 .Enabled = false;
            }
            else
            {
                dataGridView1.Focus();
            }

            this.comboBoxTradestate.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemBuySellState_SelectedIndexChanged);
            this.comboBoxCurrency.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrency_SelectedIndexChanged);

        }


        public async void InitializeMenuItems()
        {
            AddRelation = new MenuItem("اضافة علاقة ", AddItemRelation_MenuItem_Click);
            OpenRelation = new MenuItem("فتح  ", OpenItemRelation_MenuItem_Click);
            ItemRelation_OpenAnotherItemPage =new MenuItem("فتح صفحة العنصر  ", ItemRelation_OpenAnotherItemPage_MenuItem_Click);
            UpdateRelation = new MenuItem("تعديل  ", UpdateItemRelation_MenuItem_Click);
            DeleteRelation = new MenuItem("حذف ", DeleteItemRelation_MenuItem_Click);
            AddConsumUnit = new MenuItem("اضافة", AddConsumUnit_MenuItem_Click);
            UpdateConsumUnit = new MenuItem("تعديل", UpdateConsumUnit_MenuItem_Click);
            deleteConsumUnit = new MenuItem("حذف", DeleteConsumUnit_MenuItem_Click);
            SetItemSpecValue = new MenuItem("ضبط القيمة", SetItemSpecValue_MenuItem_Click);
            UNSetItemSpecValue = new MenuItem("الغاء ضبط القيمة", UNSetItemSpecValue_MenuItem_Click);
            SetItemImage_MenuItem = new MenuItem("ضبط صورة العنصر", SetItemImage_MenuItem_Click);
            UnsetItemImage_MenuItem = new MenuItem("حذف صورة العنصر", UNSetItemImage_MenuItem_Click);

            OpenFile_MenuItem = new MenuItem("فتح الملف", OpenFile_MenuItem_Click);
            AddFile_MenuItem = new MenuItem("اضافة ملف", AddFile_MenuItem_Click);
            SaveFile_MenuItem = new MenuItem("حفظ الملف على وحدة تخزين", SaveFile_MenuItem_Click);
            UpdateFileInfo_MenuItem = new MenuItem("تعديل معلومات الملف", UpdateFileInfo_MenuItem_Click);
            DeleteFile_MenuItem = new MenuItem("حذف الملف", DeleteFile_MenuItem_Click);

            Add_TO_Equivalence_Group_MenuItem = new MenuItem("ضم الى مجموعة مكافئات", Add_TO_Equivalence_Group_MenuItem_Click);
            Delete_From_Equivalence_Group_MenuItem = new MenuItem("الغاء علاقة التكافىء ", Delete_From_Equivalence_Group_MenuItem_Click);
            Open_Equivalence_Group_MenuItem = new MenuItem("استعراض المجموعة ", Open_Equivalence_Group_MenuItem_Click);
        }

     

        #region ItemSpec
        public async void FillItemSpec()
        {
            try
            {
                listViewItemSpec.Items.Clear();
                List<ItemSpecDisplay> ItemSpecDisplayList = new List<ItemSpecDisplay>();
                List<ItemSpec> ItemSpecList = new ItemSpecSQL(DB).GetItemSpecList(item.folder);
                List<ItemSpec_Restrict> ItemSpec_Restrict_List = new ItemSpec_Restrict_SQL(DB).GetItemSpecRestrictList(item.folder);
                ItemSpec_Restrict_Options_SQL ItemSpec_Restrict_Options_SQL = new ItemSpec_Restrict_Options_SQL(DB);
                for (int i = 0; i < ItemSpec_Restrict_List.Count; i++)
                {
                    ItemSpecDisplayList.Add(new ItemSpecDisplay(item.folder, ItemSpec_Restrict_List[i].SpecID, ItemSpec_Restrict_List[i].SpecName, ItemSpec_Restrict_List[i].SpecIndex, false));
                }
                for (int i = 0; i < ItemSpecList.Count; i++)
                {
                    ItemSpecDisplayList.Add(new ItemSpecDisplay(item.folder, ItemSpecList[i].SpecID, ItemSpecList[i].SpecName, ItemSpecList[i].SpecIndex, true));
                }
                ItemSpecDisplayList = ItemSpecDisplayList.OrderBy(m => m.SpecIndex).ToList();
                for (int i = 0; i < ItemSpecDisplayList.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(ItemSpecDisplayList[i].SpecName);
                    if (ItemSpecDisplayList[i].Spectype == false)
                    {
                        ListViewItem_.Name = "0" + ItemSpecDisplayList[i].SpecID.ToString();
                        ListViewItem_.SubItems.Add(" مقيدة");
                        string s = "";
                        ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict(item.folder, ItemSpecDisplayList[i].SpecID, ItemSpecDisplayList[i].SpecName, ItemSpecDisplayList[i].SpecIndex);
                        List<ItemSpec_Restrict_Value> ItemSpec_Restrict_Value = new ItemSpec_Restrict_Value_SQL(DB).Get_ItemValuesList_For_SpecRestrict(item, ItemSpec_Restrict_);
                        for (int j = 0; j < ItemSpec_Restrict_Value.Count; j++)
                        {
                            s = s+ItemSpec_Restrict_Value[j].ItemSpec_Restrict_Options_.OptionName  ;
                            if (j != ItemSpec_Restrict_Value.Count - 1) s += " , ";
                        }
                        if (s.Length == 0) s = "------";
                        ListViewItem_.SubItems.Add(s);



                    }
                    else
                    {
                        ListViewItem_.Name = "1" + ItemSpecDisplayList[i].SpecID.ToString();
                        ListViewItem_.SubItems.Add("غير مقيدة");
                        ItemSpec ItemSpec_ = new ItemSpec(item.folder, ItemSpecDisplayList[i].SpecID, ItemSpecDisplayList[i].SpecName, ItemSpecDisplayList[i].SpecIndex);
                        string s = "";
                        ItemSpec_Value ItemSpec_Value_ = new ItemSpec_Value_SQL(DB).GetItemSpec_Value(item, ItemSpec_);
                        if (ItemSpec_Value_ == null) s = "------";
                        else s = ItemSpec_Value_.Value;
                        ListViewItem_.SubItems.Add(s);
                    }
                    listViewItemSpec.Items.Add(ListViewItem_);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillItemSpec:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           
        }
       
        private void UNSetItemSpecValue_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint specid = Convert.ToUInt32(listViewItemSpec.SelectedItems[0].Name.Substring(1));
                ItemSpec ItemSpec_ = new ItemSpec(item.folder, specid, listViewItemSpec.SelectedItems[0].Text, 0);
                new ItemSpec_Value_SQL(DB).UNSetItemValueRestrict(item, ItemSpec_);
                FillItemSpec();
            }
            catch (Exception ee)
            {
                MessageBox.Show("UNSetItemSpecValue_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    
        }
        private void SetItemSpecValue_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemSpec.SelectedItems.Count == 0) return;
                if (listViewItemSpec.SelectedItems[0].Name.Substring(0, 1) == "0")
                {
                    uint specid = Convert.ToUInt32(listViewItemSpec.SelectedItems[0].Name.Substring(1));
                    ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict_SQL(DB).GetItemSpecRestrictInfoByID(specid);
                    if (new ItemSpec_Restrict_Options_SQL(DB).GetItemSpec_Restrict_Options_List(ItemSpec_Restrict_).Count == 0)
                    {
                        MessageBox.Show("لم يتم ادخال قيم للخاصية", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Form_ItemSpecRestrict_SetValues ItemSpecValueADD_ = new Form_ItemSpecRestrict_SetValues(DB, item, ItemSpec_Restrict_);
                    ItemSpecValueADD_.ShowDialog();
                    if (ItemSpecValueADD_.Changed)
                        FillItemSpec();
                    return;
                }
                if (listViewItemSpec.SelectedItems[0].Name.Substring(0, 1) == "1")
                {
                    uint spec_id = Convert.ToUInt32(listViewItemSpec.SelectedItems[0].Name.Substring(1));
                    ItemSpec ItemSpec_ = new ItemSpec(item.folder, spec_id, listViewItemSpec.SelectedItems[0].SubItems[0].Text, 0);
                    ItemSpec_Value ItemSpec_Value_ = new ItemSpec_Value_SQL(DB).GetItemSpec_Value(item, ItemSpec_);
                    InputBox inp;
                    if (ItemSpec_Value_ == null)
                        inp = new InputBox("ضبط قيمة الخاصية", ItemSpec_.SpecName);
                    else
                        inp = new InputBox("ضبط قيمة الخاصية", ItemSpec_.SpecName, ItemSpec_Value_.Value);
                    inp.ShowDialog();
                    if (inp.DialogResult == DialogResult.OK)
                    {

                        bool success = new ItemSpec_Value_SQL(DB).SetItemValue(item, ItemSpec_, inp.textBox1.Text);
                        if (success)
                            FillItemSpec();
                        return;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("SetItemSpecValue_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           



        }
        private void listViewItemSpec_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    listViewItemSpec.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();

                    foreach (ListViewItem item1 in listViewItemSpec.Items)
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
                        {
                            mi1 = new MenuItem[] { SetItemSpecValue };
                        }
                        else
                        {
                            mi1 = new MenuItem[] { SetItemSpecValue, UNSetItemSpecValue };
                        }
                        listViewItemSpec.ContextMenu = new ContextMenu(mi1);
                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemSpec_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        private void listViewItemSpec_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewItemSpec.SelectedItems.Count > 0)
            {
                SetItemSpecValue.PerformClick();
            }
        }
        #endregion
        #region ItemRelation
        private void comboBoxFilterRelations_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillItemRelations();
        }
        public async void FillItemRelations()
        {
            try
            {
                List<ItemRelation> ItemRelationList = ItemRelationShipsSQL_.GetItemRelationsList(item);
                ItemRelationList = ItemRelationList.OrderBy(x => x.AnotherItem.ItemName).ToList();
                listViewItemRelations.Items.Clear();
                for (int i = 0; i < ItemRelationList.Count; i++)
                {
                    if (comboBoxFilterRelations.SelectedIndex > 0)
                    {
                        ComboboxItem comboboxitem = (ComboboxItem)comboBoxFilterRelations.SelectedItem;
                        if (ItemRelationList[i].Relation_ != comboboxitem.Value) continue;
                    }
                    ListViewItem list_item = new ListViewItem(Relation.GetRealtionByValue(ItemRelationList[i].Relation_).Name);
                    list_item.Name = ItemRelationList[i].AnotherItem.ItemID.ToString();
                    list_item.SubItems.Add(ItemRelationList[i].AnotherItem.folder.FolderName);
                    list_item.SubItems.Add(ItemRelationList[i].AnotherItem.ItemName);
                    list_item.SubItems.Add(ItemRelationList[i].AnotherItem.ItemCompany);

                    if (ItemRelationList[i].Inherit)
                    {
                        list_item.SubItems.Add("موروثة");
                    }
                    else list_item.SubItems.Add("");

                    list_item.SubItems.Add(ItemRelationList[i].Notes);
                    switch (ItemRelationList[i].Relation_)
                    {
                        //case Relation.ITEM_EQUAL:
                        //    list_item.BackColor = Color.LimeGreen; break;
                        case Relation.ITEM_CONTAIN:
                            list_item.BackColor = Color.PaleTurquoise; break;
                        case Relation.ITEM_FOUNDIN:
                            list_item.BackColor = Color.MistyRose; break;
                    }

                    listViewItemRelations.Items.Add(list_item);
                }


                listView_Equivalence_Group.Items.Clear();
                List<Item_Equivalence_Relation> Item_Equivalence_RelationList = new Item_Equivalence_Relation_SQL(DB).Get_Item_Equivalence_Relation_By_Item(item);
                for (int i = 0; i < Item_Equivalence_RelationList.Count; i++)
                {

                    ListViewItem list_item = new ListViewItem(Item_Equivalence_RelationList[i]._Equivalence_Group.GroupID.ToString());
                    list_item.Name = Item_Equivalence_RelationList[i]._Equivalence_Group.GroupID.ToString();

                    //list_item.SubItems.Add(Item_Equivalence_RelationList[i]._Equivalence_Group .GroupID.ToString ());
                    list_item.SubItems.Add(Item_Equivalence_RelationList[i]._Equivalence_Group.GroupName);
                    //list_item.SubItems.Add(Item_Equivalence_RelationList[i].Notes );
                    list_item.SubItems.Add(new Item_Equivalence_Relation_SQL(DB).Get_Item_Equivalence_Relation_By_Group(Item_Equivalence_RelationList[i]._Equivalence_Group).Count.ToString());

                    list_item.BackColor = Color.LimeGreen;
                    listView_Equivalence_Group.Items.Add(list_item);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillItemRelations:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void ItemRelation_OpenAnotherItemPage_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                string id_s1 = listViewItemRelations.SelectedItems[0].Name;
                Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(id_s1));
                ItemForm itemform = new ItemForm(this.DB, item);
                itemform.Show();
            }
            catch (Exception ee)
            {
                MessageBox.Show("ItemRelation_OpenAnotherItemPage_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AddItemRelation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddItemRelationShip AddItemRelationShip_ = new AddItemRelationShip(DB, this.item, _LastUsedFolder, _LastUsedRelation);
                DialogResult dd = AddItemRelationShip_.ShowDialog();
                if (dd == DialogResult.OK)
                    FillItemRelations();
                _LastUsedRelation = AddItemRelationShip_.UsedRelation;
                _LastUsedFolder = AddItemRelationShip_.UsedFolder;
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

         

        }
        private void OpenItemRelation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemRelations.SelectedItems.Count == 1)
                {

                        uint anotheritemid = Convert.ToUInt32(listViewItemRelations.SelectedItems[0].Name);
                        Item AnotherItem = new ItemSQL(DB).GetItemInfoByID(anotheritemid);
                        AddItemRelationShip AddItemRelationShip_ = new AddItemRelationShip(DB, this.item, AnotherItem, false);
                        DialogResult dd = AddItemRelationShip_.ShowDialog();
                        if (dd == DialogResult.OK)
                            FillItemRelations();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemRelation_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        private void UpdateItemRelation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemRelations.SelectedItems.Count == 1)
                {

                        uint anotheritemid = Convert.ToUInt32(listViewItemRelations.SelectedItems[0].Name);
                        Item AnotherItem = new ItemSQL(DB).GetItemInfoByID(anotheritemid);
                        AddItemRelationShip AddItemRelationShip_ = new AddItemRelationShip(DB, this.item, AnotherItem, true);
                        DialogResult dd = AddItemRelationShip_.ShowDialog();
                        if (dd == DialogResult.OK)
                            FillItemRelations();
  
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("UpdateItemRelation_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private void DeleteItemRelation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("هل انت متاكد من  الحذف", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    if (listViewItemRelations.SelectedItems[0].SubItems[4].Text.Length > 0)
                    {
                        MessageBox.Show("العلاقة موروثة  لايمكن الحذف من صفحة العنصر هذا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    uint another_itemid = Convert.ToUInt32(listViewItemRelations.SelectedItems[0].Name.Substring (1));
                    if (ItemRelationShipsSQL_.DeleteItemRelation(item.ItemID, another_itemid))
                    {
                        MessageBox.Show("تم الحذف بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillItemRelations();
                    }
                    else
                        MessageBox.Show("فشل الحذف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteItemRelation_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void Open_Equivalence_Group_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint groupid = Convert.ToUInt32(listView_Equivalence_Group .SelectedItems[0].Name);
                Equivalence_Group Equivalence_Group_ = new Equivalence_GroupSQL(DB).GetEquivalence_Groupinfo_By_ID (groupid);
                Equivalence_Group_Items_Form Equivalence_Group_Items_Form_ = new Equivalence_Group_Items_Form(DB, Equivalence_Group_);
                Equivalence_Group_Items_Form_.ShowDialog();
                FillItemRelations();
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_Equivalence_Group_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Delete_From_Equivalence_Group_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("هل انت متاكد من  الغاء العلاقة", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                
                    uint groupid = Convert.ToUInt32(listView_Equivalence_Group.SelectedItems[0].Name);
                    if (new Item_Equivalence_Relation_SQL(DB).UNSet_Item_Equivalence_Relation  (item.ItemID, groupid))
                    {
                        MessageBox.Show("تم الإلغاء بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillItemRelations();
                    }
                    else
                        MessageBox.Show("فشل الإلغاء", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_From_Equivalence_Group_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Add_TO_Equivalence_Group_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Equivalence_Group_Form Equivalence_Group_Form_ = new Equivalence_Group_Form(DB, true);
                Equivalence_Group_Form_.ShowDialog();
                if (Equivalence_Group_Form_.DialogResult  == DialogResult.OK)
                {

                   ;
                    if (new Item_Equivalence_Relation_SQL(DB).Set_Item_Equivalence_Relation (item, Equivalence_Group_Form_.Equivalence_Group))
                    {
                        MessageBox.Show("تم الاضافة بنجاح ", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillItemRelations();
                    }
                    else
                        MessageBox.Show("فشل الاضافة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Add_TO_Equivalence_Group_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void listViewItemRelations_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewItemRelations.SelectedItems.Count > 0)
            {
                    ItemRelation_OpenAnotherItemPage.PerformClick();

            }
        }

        private void listViewItemRelations_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewItemRelations.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewItemRelations.Items)
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

                        if (listitem.SubItems[4].Text.Length == 0)
                        {
                            MenuItem[] mi1 = new MenuItem[] { ItemRelation_OpenAnotherItemPage, new MenuItem("-"), OpenRelation, UpdateRelation, DeleteRelation, new MenuItem("-"), AddRelation };
                            listViewItemRelations.ContextMenu = new ContextMenu(mi1);
                        }

                    }



                    else
                    {
                        MenuItem[] mi = new MenuItem[] { AddRelation };
                        listViewItemRelations.ContextMenu = new ContextMenu(mi);
                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemRelations_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void listView_Equivalence_Group_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView_Equivalence_Group .SelectedItems.Count > 0)
            {
                    Open_Equivalence_Group_MenuItem.PerformClick();
            }
        }

        private void listView_Equivalence_Group_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listView_Equivalence_Group.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listView_Equivalence_Group.Items)
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

                        MenuItem[] mi1 = new MenuItem[] { Open_Equivalence_Group_MenuItem, Delete_From_Equivalence_Group_MenuItem, new MenuItem("-"), Add_TO_Equivalence_Group_MenuItem };
                        listView_Equivalence_Group.ContextMenu = new ContextMenu(mi1);
                    }



                    else
                    {
                        MenuItem[] mi = new MenuItem[] { Add_TO_Equivalence_Group_MenuItem };
                        listView_Equivalence_Group.ContextMenu = new ContextMenu(mi);
                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView_Equivalence_Group_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        #endregion
        #region Unspecified
        private void comboBoxItemBuySellState_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillItemConsumeUnitsAndSellPrices();
        }
      
        public void FillItemConsumeUnitsAndSellPrices()
        {
            try
            {
                if (TradeStateList.Count != 0 && selltypelist.Count != 0)
                {
                    if (comboBoxTradestate.SelectedIndex < 0) return;
                    ComboboxItem comboboxitem = (ComboboxItem)comboBoxTradestate.SelectedItem;
                    dataGridView1.TopLeftHeaderCell.Value = comboboxitem.Text;

                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                    dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Aqua;
                    dataGridView1.TopLeftHeaderCell.Style.BackColor = Color.Orange;
                    ComboboxItem currencyitem = (ComboboxItem)comboBoxCurrency.SelectedItem;
                    Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(currencyitem.Value);
                    TradeState TradeState_ = new TradeStateSQL(DB).GetTradeStateBYID (comboboxitem.Value);
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < ConsumUnitsList.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        for (int j= 0; j < selltypelist.Count; j++)
                        {
                            
                            dataGridView1.Rows[i].HeaderCell.Value = ConsumUnitsList[i].ConsumeUnitName;
                            double? price = new ItemSellPriceSql(DB).GetPrice(item, TradeState_, selltypelist[j], ConsumUnitsList[i]);

                            if (price == null) dataGridView1.Rows[i].Cells[j].Value = " - " + " " + currency.CurrencySymbol;

                            else dataGridView1.Rows[i].Cells[j].Value = System.Math.Round((Convert.ToDouble(price) * currency.ExchangeRate), 3).ToString() + " " + currency.CurrencySymbol;
                        }


                    }

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("FillItemConsumeUnitsAndSellPrices:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem currencyitem = (ComboboxItem)comboBoxCurrency.SelectedItem;
            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(currencyitem.Value);
            textBoxExchangeRate.Text = currency.ExchangeRate.ToString();
            FillItemConsumeUnitsAndSellPrices();
        }
        public void AdjustmentDatagridviewColumnsWidth()
        {
            try
            {
                int columnscount = dataGridView1.Columns.Count + 1;
                dataGridView1.RowHeadersWidth = dataGridView1.Width / columnscount;
                for (int i = 0; i < columnscount - 1; i++) dataGridView1.Columns[i].Width = dataGridView1.Width / columnscount;
            }
            catch (Exception ee)
            {
                MessageBox.Show("AdjustmentDatagridviewColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


    

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            AdjustmentDatagridviewColumnsWidth();
        }
        #region ConsumeUnit
        private void listViewConsumUnits_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    listViewConsumUnits.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();
                    foreach (ListViewItem item1 in listViewConsumUnits.Items)
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
                        if (listitem.Name == "0")
                        {
                            mi1 = new MenuItem[] { AddConsumUnit };
                        }
                        else
                        {
                            mi1 = new MenuItem[] { AddConsumUnit, UpdateConsumUnit, deleteConsumUnit };
                        }
                        listViewConsumUnits.ContextMenu = new ContextMenu(mi1);
                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddConsumUnit };
                        listViewConsumUnits.ContextMenu = new ContextMenu(mi);
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewConsumUnits_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
       
        private void UpdateConsumUnit_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint CID = Convert.ToUInt32 (listViewConsumUnits.SelectedItems[0].Name);
                ConsumeUnit CU = new ConsumeUnitSql(DB).GetConsumeAmountinfo( CID);
                ConsumeUnitAddForm CUA = new ConsumeUnitAddForm(DB, CU   );
                DialogResult d = CUA.ShowDialog();
                if (d == DialogResult.OK)
                {
                    UpdateConsumeUnitsList();
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("UpdateConsumUnit_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AddConsumUnit_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ConsumeUnitAddForm CUA = new ConsumeUnitAddForm(DB, item);
                DialogResult d = CUA.ShowDialog();
                if (d == DialogResult.OK)
                {
                    UpdateConsumeUnitsList();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddConsumUnit_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void DeleteConsumUnit_MenuItem_Click(object sender, EventArgs e)
        {

            DialogResult d = MessageBox.Show("هل انت متاكد من الحذف؟", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (d != DialogResult.OK) return;
        

            try
            {
                uint CID = Convert.ToUInt32(listViewConsumUnits.SelectedItems[0].Name);
                if (new ConsumeUnitSql(DB).DeleteConsumeUnit(CID))
                {
                    UpdateConsumeUnitsList();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteConsumUnit_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public void UpdateConsumeUnitsList()
        {
            try
            {
                ConsumeUnitSql CUS = new ConsumeUnitSql(DB);
                ConsumUnitsList = CUS.GetConsumeUnitList(item);
                listViewConsumUnits.Items.Clear();


                for (int i = 0; i < ConsumUnitsList.Count; i++)
                {
                    ListViewItem Listitem = new ListViewItem(ConsumUnitsList[i].ConsumeUnitName);
                    Listitem.Name = ConsumUnitsList[i].ConsumeUnitID.ToString();
                    Listitem.SubItems.Add(ConsumUnitsList[i].Factor.ToString());
                    if (ConsumUnitsList[i].ConsumeUnitName == item.DefaultConsumeUnit) Listitem.BackColor = Color.Gray;
                    listViewConsumUnits.Items.Add(Listitem);

                   

                }
                FillItemConsumeUnitsAndSellPrices();
            }
            catch (Exception ee)
            {
                MessageBox.Show("UpdateConsumeUnitsList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion
        #region ItemImage
        private async void GetItemImage()
        {
            try
            {
                byte [] ItemImage = (new ItemSQL(DB)).GetItemImage(item);

                if (ItemImage == null)
                {
                    ItemImage_Set = false;

                    pictureBoxItemImage.Image = OverLoad_Client.Properties.Resources.ds;
                }
                else
                {
                    ItemImage_Set = true ;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(ItemImage);
                    pictureBoxItemImage.Image = Image.FromStream(ms, true);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetItemImage" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void UNSetItemImage_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("هل انت متاكد من حذف صورة العنصر!", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (d != DialogResult.OK) return;
                (new ItemSQL(DB)).UnSetItemImage(item.ItemID);
                GetItemImage();
            }
            catch (Exception ee)
            {
                MessageBox.Show("UNSetItemImage_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       
        }
        private void SetItemImage_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Reset();
                openFileDialog1.InitialDirectory = LastPath ;
                openFileDialog1.Filter = "Image files (*.jpg, *.jpeg) | *.jpg; *.jpeg;";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    LastPath = openFileDialog1.FileName;
                    Image image = Image.FromFile(openFileDialog1.FileName);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bool success = (new ItemSQL(DB)).SetItemImage(item.ItemID , ms.ToArray());
                    if (success) GetItemImage();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("SetItemImage_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion
        #region ItemFile
        public async void GetItemFilesList()
        {
            try
            {
                listViewItemFiles.Items.Clear();
                List<ItemFile> ItemFilesList = new List<ItemFile>();
                ItemFilesList = new ItemFileSQL(DB).GetItemFileList(item);
                for (int i = 0; i < ItemFilesList.Count; i++)
                {
                    ListViewItem list_item = new ListViewItem(ItemFilesList[i].FileName);
                    list_item.Name = ItemFilesList[i].FileID.ToString();
                    list_item.SubItems.Add(ItemFilesList[i].FileDescription);
                    list_item.SubItems.Add(ItemFilesList[i].AddDate.ToString());
                    string size = "";
                    if (ItemFilesList[i].FileSize < 1000)
                    {
                        size = ItemFilesList[i].FileSize + " بايت ";
                    }
                    else if (ItemFilesList[i].FileSize < 1000000)
                    {
                        size = ItemFilesList[i].FileSize / 1000 + " كيلو بايت ";
                    }
                    else
                    {
                        size = ItemFilesList[i].FileSize / 1000000 + " ميغا بايت ";
                    }

                    list_item.SubItems.Add(size);
                    listViewItemFiles.Items.Add(list_item);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetItemFilesList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void listViewItemFiles_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    listViewItemFiles.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();
                    foreach (ListViewItem item1 in listViewItemFiles.Items)
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
                        MenuItem[] mi1 = new MenuItem[] { OpenFile_MenuItem, SaveFile_MenuItem, UpdateFileInfo_MenuItem, DeleteFile_MenuItem, new MenuItem("-"), AddFile_MenuItem };
                        listViewItemFiles.ContextMenu = new ContextMenu(mi1);
                    }
                    else
                    {
                        MenuItem[] mi = new MenuItem[] { AddFile_MenuItem };
                        listViewItemFiles.ContextMenu = new ContextMenu(mi);
                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemFiles_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        public async void OpenItemFile(uint fileid, string FileName)
        {
            Set_listViewItemFiles_Status(false);
            try
            {
             

                if (!Path.HasExtension(FileName))
                {
                    MessageBox.Show("غير قابل للفتح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                string path = Application.StartupPath + "\\" + "OverLoadTemp";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string fileName = path + "\\" + "OV_C_tmp" + 0 + FileName;
                int j = 0;
                while (System.IO.File.Exists(fileName))
                {
                    j++;
                    fileName = path + "\\" + "OV_C_tmp." + j + FileName;


                }

                File .WriteAllBytes (fileName,(new ItemFileSQL(DB)).GetFileData(fileid));
                Process p = Process.Start(fileName);
                new Thread(delegate () {
                    kill_process( p, fileName);
                }).Start();



            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemFile:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Set_listViewItemFiles_Status(true );

        }

        private    void kill_process( Process p, string filename)
        {
            try
            {
                if (p != null)
                {             
                    p.WaitForExit();

                    File.Delete(filename);
                }
       


            }
            catch (Exception ee)
            {
                MessageBox.Show("kill_process:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void OpenFile_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                uint id = Convert.ToUInt32(listViewItemFiles.SelectedItems[0].Name);
                string filename = listViewItemFiles.SelectedItems[0].SubItems[0].Text;
                Task.Factory.StartNew(() => {
                    OpenItemFile(id, filename);
                });
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemFiles_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void ConfifureItemFilesListViewColumnsWidth()
        {
            try
            {
                listViewItemFiles.Columns[2].Width = 200;
                listViewItemFiles.Columns[3].Width = 100;
                listViewItemFiles.Columns[0].Width = (listViewItemFiles.Width - 300) / 2;
                listViewItemFiles.Columns[1].Width = (listViewItemFiles.Width - 300) / 2;
            }
            catch (Exception ee)
            {
                MessageBox.Show("ConfifureItemFilesListViewColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void listViewItemFiles_Resize(object sender, EventArgs e)
        {
            ConfifureItemFilesListViewColumnsWidth();
        }
        private void Set_listViewItemFiles_Status(bool status)
        {
            try
            {

                    // If the current thread is not the UI thread, InvokeRequired will be true
                    if (listViewItemFiles .InvokeRequired)
                    {
                    // If so, call Invoke, passing it a lambda expression which calls
                    // UpdateText with the same label and text, but on the UI thread instead.
                    listViewItemFiles.Invoke((Action)(() => Set_listViewItemFiles_Status( status)));
                        return;
                    }
                // If we're running on the UI thread, we'll get here, and can safely update 
                // the label's text.
                listViewItemFiles.Enabled = status ;


            }
            catch
            {
                //MessageBox.Show("Change_CheckBox_Status:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listViewItemFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                uint id = Convert.ToUInt32(listViewItemFiles.SelectedItems[0].Name);
                string filename = listViewItemFiles.SelectedItems[0].SubItems[0].Text;
                Task.Factory.StartNew(() => {
                    OpenItemFile(id,filename  );
                });
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemFiles_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private async  void AddFile_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Reset();
                openFileDialog1.InitialDirectory = LastPath ;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    LastPath = openFileDialog1.FileName;
                    AddItemFile AddItemFile_ = new AddItemFile(DB, item, openFileDialog1);
                    AddItemFile_.FormClosed += ItemFile_Form_Closed;
                    AddItemFile_.Show();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddFile_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private async   void SaveFile_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog FolderBrowserDialog_ = new FolderBrowserDialog();
                FolderBrowserDialog_.Reset();
                if (FolderBrowserDialog_.ShowDialog() != DialogResult.OK) return;
                List<ListViewItem> list = new List<ListViewItem>();
                for (int i = 0; i < listViewItemFiles.SelectedItems.Count; i++) list.Add(listViewItemFiles.SelectedItems[i]);


                Task.Run (() => { Save_Files(list, FolderBrowserDialog_.SelectedPath); });
            }
            catch (Exception ee)
            {
                MessageBox.Show("SaveFiles:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        [STAThread]
        private void Save_Files( List<ListViewItem > list,string SavePath)
        {
            Set_listViewItemFiles_Status(false);
            try
            {


                for (int i = 0; i < list.Count; i++)
                {
                    uint fileid = Convert.ToUInt32(list[i].Name);

                    string f = SavePath + "\\" + list[i].SubItems[0].Text;
                    if (System.IO.File.Exists(f))
                    {
                        DialogResult d = MessageBox.Show("الملف " + f + " موجود بلفعل هل تريد استبداله !", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (d != DialogResult.OK) continue;
                    }
                    System.IO.File.WriteAllBytes(f, (new ItemFileSQL(DB)).GetFileData(fileid));
                }
                MessageBox.Show("تم ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ee)
            {
                MessageBox.Show("SaveFiles:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Set_listViewItemFiles_Status(true );
        }
        private async  void UpdateFileInfo_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemFile itemfile = new ItemFile(item,
               Convert.ToUInt32(listViewItemFiles.SelectedItems[0].Name)
               , listViewItemFiles.SelectedItems[0].SubItems[0].Text
               , listViewItemFiles.SelectedItems[0].SubItems[1].Text
               , -1, DateTime.Now);
                AddItemFile AddItemFile_ = new AddItemFile(DB, itemfile);
                AddItemFile_.FormClosed += ItemFile_Form_Closed;
                AddItemFile_.Show ();
               
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("UpdateFileInfo_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void ItemFile_Form_Closed(object sender, FormClosedEventArgs e)
        {
            Form form = (Form)sender;
            if (form.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("تم التنفيذ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetItemFilesList();
            }
        }

        private void DeleteFile_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("هل انت متاكد من  حذف الملف؟" + listViewItemFiles.SelectedItems[0].SubItems[0].Text, "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (d != DialogResult.OK) return;


                uint fileid = Convert.ToUInt32(listViewItemFiles.SelectedItems[0].Name);
                if ((new ItemFileSQL(DB)).DeleteItemFile(fileid))
                {
                    UpdateConsumeUnitsList();
                    GetItemFilesList();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteFile_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
  
   
        }
        #endregion
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            //{
            //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "-";
            //    return;
            //}
            //ConsumeUnit SelectedConsumeUnit = null;
            //SellType SelectedSellType = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(dataGridView1.Columns[e.ColumnIndex].Name));
 
            //for (int i = 0; i < ConsumUnitsList.Count; i++)
            //{
            //    if (ConsumUnitsList[i].ConsumeUnitName.Equals(dataGridView1.Rows[e.RowIndex].HeaderCell .Value  ))
            //    {
            //        SelectedConsumeUnit = ConsumUnitsList[i];
            //        break;
            //    }
            //}
            //double price;
            //try
            //{
            //    price = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            //    if(price <0)
            //    {
            //        MessageBox.Show("السعر يجب ان يكون اكبر من الصفر","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    
            //        return;
            //    }
            //    try
            //    {
            //        if (comboBoxItemBuySellState.SelectedIndex < 0) return;
            //        ComboboxItem currencyitem = (ComboboxItem)comboBoxCurrency.SelectedItem;
            //        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(currencyitem.Value);

            //        ItemBuySellState ItemBuySellState_ = new ItemBuySellState(((KeyValuePair<uint, string>)comboBoxItemBuySellState.SelectedItem).Key, ((KeyValuePair<uint, string>)comboBoxItemBuySellState.SelectedItem).Value);

            //        new ItemSellPriceSql(DB).SetItemPrice(item, ItemBuySellState_, SelectedConsumeUnit, SelectedSellType, price/currency .ExchangeRate );
            //    }
            //    catch (Exception ee)
            //    {
            //        MessageBox.Show("price value set:"+ee.Message);
            //    }
            //}
            //catch
            //{
            //    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value==null|| dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()=="-")
            //    {
            //        try
            //        {
            //            if (comboBoxItemBuySellState.SelectedIndex < 0) return;
            //            ItemBuySellState ItemBuySellState_ = new ItemBuySellState(((KeyValuePair<uint, string>)comboBoxItemBuySellState.SelectedItem).Key, ((KeyValuePair<uint, string>)comboBoxItemBuySellState.SelectedItem).Value);

            //            new ItemSellPriceSql(DB).UNSetItemPrice(item, ItemBuySellState_, SelectedConsumeUnit, SelectedSellType);
            //        }
            //        catch (Exception ee)
            //        {
            //            MessageBox.Show(ee.Message);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("السعر يجب ان يكون قيمة حقيقية");
            //        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "-";
            //        return;
            //    }
            //}
            //FillItemConsumeUnitsAndSellPrices();
           

         

        }

        private void pictureBoxItemImage_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    MenuItem[] Mi;
                    pictureBoxItemImage.ContextMenu = null;
                    if (ItemImage_Set == false )
                    {
                        Mi = new MenuItem[] { SetItemImage_MenuItem };
                    }
                    else
                        Mi = new MenuItem[] { SetItemImage_MenuItem, UnsetItemImage_MenuItem };

                    pictureBoxItemImage.ContextMenu = new ContextMenu(Mi);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("pictureBoxItemImage_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
       
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                ConsumeUnit SelectedConsumeUnit = null;
                for (int i = 0; i < ConsumUnitsList.Count; i++)
                {
                    if (ConsumUnitsList[i].ConsumeUnitName.Equals(dataGridView1.Rows[e.RowIndex].HeaderCell.Value))
                    {
                        SelectedConsumeUnit = ConsumUnitsList[i];
                        break;
                    }
                }
                ComboboxItem tradestate_item = (ComboboxItem)comboBoxTradestate.SelectedItem;
                TradeState TradeState_ = new TradeState(tradestate_item.Value, tradestate_item.Text);
                SellType SelectedSellType = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(dataGridView1.Columns[e.ColumnIndex].Name));
                ComboboxItem currencyitem = (ComboboxItem)comboBoxCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(currencyitem.Value);
                Trade.Forms.PriceInputBox inp = new Trade.Forms.PriceInputBox("ضبط السعر", SelectedSellType.SellTypeName, TradeState_.TradeStateName, SelectedConsumeUnit.ConsumeUnitName, currency.CurrencyName, "");
                inp.ShowDialog();
                if (inp.DialogResult == DialogResult.OK)
                {
                    double price;
                    try
                    {
                         price = Convert.ToDouble(inp.Price);
                        

                    }
                    catch (Exception ee)
                    {
                        throw new Exception("يرجى ادخال رقم حقيقي");
                    }
                    if (price < 0)
                    {
                        throw new Exception("السعر يجب ان يكون اكبر من الصفر");
                    }

                    new ItemSellPriceSql(DB).SetItemPrice(item, TradeState_, SelectedConsumeUnit, SelectedSellType, price / currency.ExchangeRate);

                    FillItemConsumeUnitsAndSellPrices();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("dataGridView1_CellMouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
            //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            //dataGridView1.BeginEdit(true);

            ////optionally set the EditMode before you call BeginEdit
            //dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }




        #endregion

        private void تقريرالمشترياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemReport_Buy_Form ItemReport_Buy_Form_ = new ItemReport_Buy_Form(DB, item);
                ItemReport_Buy_Form_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void عرضتقريرالمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemReport_Sell_Form ItemReport_Sell_Form_ = new ItemReport_Sell_Form(DB, item);
                ItemReport_Sell_Form_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show( ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         
        }

        private void عرضتقريرالصيانةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemReport_Maintenance_Form ItemReport_Maintenance_Form_ = new ItemReport_Maintenance_Form(DB, item);
                ItemReport_Maintenance_Form_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
     
        }

 
    }
}
