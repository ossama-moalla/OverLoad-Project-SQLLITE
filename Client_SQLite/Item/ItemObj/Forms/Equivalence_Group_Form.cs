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
    public partial class Equivalence_Group_Form : Form
    {
        DatabaseInterface DB;
        MenuItem Add_Equivalence_Group;
        MenuItem Update_Equivalence_Group;
        MenuItem Delete_Equivalence_Group;
        MenuItem Open_Equivalence_Group;
        private Equivalence_Group _Equivalence_Group;
        public Equivalence_Group Equivalence_Group
        {
            get
            {
                return _Equivalence_Group;
            }
        }

        public Equivalence_Group_Form(DatabaseInterface db, bool Get_Equivalence_Group)
        {
            InitializeComponent();
            DB = db;
            Add_Equivalence_Group = new MenuItem("اضافة مجموعة مكافئات", Add_Equivalence_Group_Click);
            Update_Equivalence_Group = new MenuItem("تعديل ", Update_Equivalence_Group_Click);
            Delete_Equivalence_Group = new MenuItem("حذف", Delete_Equivalence_Group_Click);
            Open_Equivalence_Group = new MenuItem("استعراض المجموعة", Open_Equivalence_Group_Click);
            if (!Get_Equivalence_Group) buttonSelect.Visible = false;
            Fill_Equivalence_Group_List();
        }

        private void Open_Equivalence_Group_Click(object sender, EventArgs e)
        {
            try
            {
                uint _Equivalence_GroupID = Convert.ToUInt32(listView1 .SelectedItems[0].Name);
                Equivalence_Group Equivalence_Group__ = new Equivalence_GroupSQL(DB).GetEquivalence_Groupinfo_By_ID (_Equivalence_GroupID);
                Equivalence_Group_Items_Form Equivalence_Group_Items_Form_ = new Equivalence_Group_Items_Form(DB, Equivalence_Group__);
                Equivalence_Group_Items_Form_.ShowDialog();
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_Equivalence_Group_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_Equivalence_Group_Click(object sender, EventArgs e)
        {
            try
            {
                
                uint _Equivalence_GroupID = Convert.ToUInt32(listView1.SelectedItems[0].Name);
                _Equivalence_Group = new Equivalence_GroupSQL(DB).GetEquivalence_Groupinfo_By_ID(_Equivalence_GroupID);

                DialogResult dd = MessageBox.Show("هل انت متاكد من حذف المجموعة؟","",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);

                if (dd == DialogResult.OK)
                {
                    bool success = new Equivalence_GroupSQL(DB).DeleteEquivalence_Group (_Equivalence_Group.GroupID);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Fill_Equivalence_Group_List();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_Equivalence_Group_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Update_Equivalence_Group_Click(object sender, EventArgs e)
        {
            try
            {
                uint _Equivalence_GroupID = Convert.ToUInt32(listView1.SelectedItems[0].Name);
                _Equivalence_Group = new Equivalence_GroupSQL(DB).GetEquivalence_Groupinfo_By_ID (_Equivalence_GroupID);

                OverLoad_Client.Forms.InputBox inp = new OverLoad_Client.Forms.InputBox("مجموعة مكافئات جديدة", "ادخل اسم المجموعة", _Equivalence_Group.GroupName);
                inp.ShowDialog();
                if (inp.DialogResult == DialogResult.OK)
                {
                    bool success = new Equivalence_GroupSQL(DB).UpdatEquivalence_Group (_Equivalence_Group.GroupID,inp.textBox1.Text);
                    if (success)
                    {
                        MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Fill_Equivalence_Group_List();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Update_Equivalence_Group_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Add_Equivalence_Group_Click(object sender, EventArgs e)
        {
            try
            {
                OverLoad_Client.Forms.InputBox inp = new OverLoad_Client.Forms.InputBox("مجموعة مكافئات جديدة", "ادخل اسم المجموعة");
                inp.ShowDialog();
                if(inp .DialogResult ==DialogResult.OK )
                {
                    bool success = new Equivalence_GroupSQL(DB).AddEquivalence_Group(inp.textBox1.Text);
                    if(success )
                    {
                        MessageBox.Show("تم الاضافة بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        Fill_Equivalence_Group_List();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Add_Equivalence_Group_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fill_Equivalence_Group_List()
        {
            try
            {
                listView1.Items.Clear();
                List<Equivalence_Group> list = new Equivalence_GroupSQL(DB).GetEquivalence_GroupList();
                for(int i=0;i<list .Count;i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(list[i].GroupID.ToString());
                    ListViewItem_.Name = list[i].GroupID.ToString();
                    ListViewItem_.SubItems.Add(list [i].GroupName);
                    listView1.Items.Add(ListViewItem_);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Fill_Equivalence_Group_List:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView1 .SelectedItems.Count > 0)
            {
                if (buttonSelect.Visible == true)
                    buttonSelect.PerformClick();
                else
                    Open_Equivalence_Group.PerformClick();
            }
        }
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if(listView1 .SelectedItems .Count >0)
            {
               try
                {
                    uint _Equivalence_GroupID = Convert.ToUInt32(listView1 .SelectedItems[0].Name );
                    _Equivalence_Group = new Equivalence_GroupSQL(DB).GetEquivalence_Groupinfo_By_ID (_Equivalence_GroupID);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Failed_To_Get_Equivalence_Group" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("يرجى الاختيار " , "", MessageBoxButtons.OK, MessageBoxIcon.Error);

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


                    MenuItem[] mi1 = new MenuItem[] { Open_Equivalence_Group
                        , Update_Equivalence_Group, Delete_Equivalence_Group, new MenuItem("-"), Add_Equivalence_Group };
                    listView1 .ContextMenu = new ContextMenu(mi1);


                }
                else
                {

                    MenuItem[] mi = new MenuItem[] { Add_Equivalence_Group };
                    listView1 .ContextMenu = new ContextMenu(mi);

                }

            }
        }
    }
}
