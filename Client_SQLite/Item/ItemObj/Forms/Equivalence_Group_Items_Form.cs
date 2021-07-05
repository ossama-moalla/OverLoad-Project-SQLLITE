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
    public partial class Equivalence_Group_Items_Form : Form
    {
        DatabaseInterface DB;
        MenuItem Add_Equivalence_Item;
        MenuItem Delete_Equivalence_Item;
        MenuItem Open_Item;
        private Equivalence_Group _Equivalence_Group;
    

        public Equivalence_Group_Items_Form(DatabaseInterface db, Equivalence_Group Equivalence_Group_)
        {
            InitializeComponent();
            _Equivalence_Group = Equivalence_Group_;
            DB = db;
            Add_Equivalence_Item = new MenuItem("اضافة عنصر", Add_Equivalence_Item_Click);
            Delete_Equivalence_Item = new MenuItem("استبعاد", Delete_Equivalence_Item_Click);
            Open_Item = new MenuItem("استعراض العنصر", Open_Item_Click);
            Fill_Item_List();
        }

        private void Open_Item_Click(object sender, EventArgs e)
        {
            try
            {
                uint _ItemID = Convert.ToUInt32(listView1.SelectedItems[0].Name);
                Item item = new ItemSQL(DB).GetItemInfoByID(_ItemID);
                ItemForm itemform = new ItemForm(DB, item);
                itemform.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_Item_Click:"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void Delete_Equivalence_Item_Click(object sender, EventArgs e)
        {
            try
            {
                uint _ItemID = Convert.ToUInt32(listView1.SelectedItems[0].Name);
                DialogResult dd = MessageBox.Show("هل انت متاكد من نزع العنصر من المجموعة؟","",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);

                if (dd == DialogResult.OK)
                {
                    bool success = new Item_Equivalence_Relation_SQL(DB).UNSet_Item_Equivalence_Relation   (_ItemID ,_Equivalence_Group .GroupID);
                    if (success)
                    {
                        MessageBox.Show("تمت العملية  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Fill_Item_List();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_Item_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void Update_Item_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        uint _ItemID = Convert.ToUInt32(listView1.SelectedItems[0].Name);
        //        _Item = new ItemSQL(DB).GetIteminfo(_ItemID);

        //        Forms.InputBox inp = new Forms.InputBox("مجموعة مكافئات جديدة", "ادخل اسم المجموعة", _Item.GroupName);
        //        inp.ShowDialog();
        //        if (inp.DialogResult == DialogResult.OK)
        //        {
        //            bool success = new ItemSQL(DB).UpdatItem (_Item.GroupID,inp.textBox1.Text);
        //            if (success)
        //            {
        //                MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                Fill_Item_List();
        //            }
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show("Update_Item_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void Add_Equivalence_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ItemObj.Forms.User_ShowItemsForm SelectItem_ = new ItemObj.Forms.User_ShowItemsForm(DB, null , User_ShowItemsForm.SELECT_ITEM);

                SelectItem_.ShowDialog();
                
                if(SelectItem_.DialogResult ==DialogResult.OK )
                {
                    bool success = new Item_Equivalence_Relation_SQL(DB).Set_Item_Equivalence_Relation (SelectItem_.ReturnItem , _Equivalence_Group);
                    if (success )
                    {
                        MessageBox.Show("تم الاضافة بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        Fill_Item_List();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Add_Equivalence_Item_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fill_Item_List()
        {
            try
            {
                listView1.Items.Clear();
                List<Item_Equivalence_Relation> list = new Item_Equivalence_Relation_SQL(DB).Get_Item_Equivalence_Relation_By_Group(_Equivalence_Group);
                for(int i=0;i<list .Count;i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(list[i]._Item .ItemID.ToString());
                    ListViewItem_.Name = list[i]._Item.ItemID.ToString();
                    ListViewItem_.SubItems.Add(list [i]._Item .folder .FolderName);
                    ListViewItem_.SubItems.Add(list[i]._Item.ItemName);
                    ListViewItem_.SubItems.Add(list[i]._Item.ItemCompany);
                    listView1.Items.Add(ListViewItem_);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Fill_Item_List:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView1 .SelectedItems.Count > 0)
            {
                    Open_Item.PerformClick();
            }
        }
    

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            listView1 .ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listView1 .Items)
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


                    MenuItem[] mi1 = new MenuItem[] { Open_Item
                        ,  Delete_Equivalence_Item, new MenuItem("-"), Add_Equivalence_Item };
                    listView1 .ContextMenu = new ContextMenu(mi1);


                }
                else
                {

                    MenuItem[] mi = new MenuItem[] { Add_Equivalence_Item  };
                    listView1 .ContextMenu = new ContextMenu(mi);

                }

            }
        }
    }
}
