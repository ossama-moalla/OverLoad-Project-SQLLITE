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
    public partial class Form_ItemSpecRestrict_SetValues : Form
    {
        DatabaseInterface DB;
        Item Item_;
        ItemSpec_Restrict ItemSpec_Restrict_;
        ItemSpec_Restrict_Options_SQL ItemSpec_Restrict_Options_SQL_;
        ItemSpec_Restrict_Value_SQL ItemSpec_Restrict_Value_SQL_;
        public bool Changed = false;
        MenuItem deletevalue;

        public Form_ItemSpecRestrict_SetValues(DatabaseInterface db, Item Item__, ItemSpec_Restrict ItemSpec_Restrict__)
        {
            InitializeComponent();
            DB = db;

            deletevalue = new MenuItem("حذف", DeleteValue_MenuItem_Click);
            Item_ = Item__;
            ItemSpec_Restrict_ = ItemSpec_Restrict__;
            ItemSpec_Restrict_Options_SQL_ = new ItemSpec_Restrict_Options_SQL(DB);
            ItemSpec_Restrict_Value_SQL_ = new ItemSpec_Restrict_Value_SQL(DB);
            ItemInfo.Text = Item__.folder.FolderName + ":" + Item_.ItemName + "-" + Item_.ItemCompany;
            ItemSpecName.Text = ItemSpec_Restrict__.SpecName;
            Refresh_Values();

        }

        private void DeleteValue_MenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult d = MessageBox.Show("هل انت متاكد من  الحذف", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                this.Changed = true;
                try
                {
                    bool result = false;
                    for (int i = 0; i < listView_ItemValues.SelectedItems.Count; i++)
                    {
                        uint optionid = Convert.ToUInt32(listView_ItemValues.SelectedItems[i].Name);
                        ItemSpec_Restrict_Options ItemSpec_Restrict_Options_ = new ItemSpec_Restrict_Options(ItemSpec_Restrict_, optionid, listView_ItemValues.SelectedItems[i].Name);
                        result=new ItemSpec_Restrict_Value_SQL(DB).Delete_ItemValueRestrict(Item_, ItemSpec_Restrict_Options_);
                  
                    }
                    Refresh_Values();
                    
                }
                catch
                {
                    MessageBox.Show("تم الحذف بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
             


            }
        }

        public void Refresh_Values()
        {
            List<ItemSpec_Restrict_Options> ItemSpec_Restrict_Options_List = ItemSpec_Restrict_Options_SQL_.GetItemSpec_Restrict_Options_List(ItemSpec_Restrict_) ;
            List<ItemSpec_Restrict_Value> ItemSpec_Restrict_Value_List = ItemSpec_Restrict_Value_SQL_.Get_ItemValuesList_For_SpecRestrict(Item_, ItemSpec_Restrict_);
            listView_AvailableOptions.Items.Clear();
            listView_ItemValues.Items.Clear();
            for (int i=0;i< ItemSpec_Restrict_Options_List.Count;i++)
            {
                ListViewItem ListViewItem_ = new ListViewItem(ItemSpec_Restrict_Options_List[i].OptionName);
                ListViewItem_.Name = ItemSpec_Restrict_Options_List[i].OptionID.ToString();
                listView_AvailableOptions.Items.Add(ListViewItem_);
            }
            for (int i = 0; i < ItemSpec_Restrict_Value_List.Count; i++)
            {
                ListViewItem ListViewItem_ = new ListViewItem(ItemSpec_Restrict_Value_List[i].ItemSpec_Restrict_Options_.OptionName);
                ListViewItem_.Name = ItemSpec_Restrict_Value_List[i].ItemSpec_Restrict_Options_.OptionID .ToString();
                listView_ItemValues.Items.Add(ListViewItem_);
            }
        }

        private void buttonSetValues_Click(object sender, EventArgs e)
        {
            buttonSetValues.Enabled = false;
            for(int i=0;i<listView_AvailableOptions .SelectedItems .Count;i++)
            {
                uint optionid = Convert.ToUInt32(listView_AvailableOptions.SelectedItems[i].Name );
                for(int j=0;j<listView_ItemValues .Items .Count;j++)
                {
                    if(Convert .ToUInt32( listView_ItemValues.Items[j].Name )==optionid )
                    {
                        MessageBox.Show("القيمة مدخلة مسبقا","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                }
                ItemSpec_Restrict_Options ItemSpec_Restrict_Options_ = new ItemSpec_Restrict_Options(ItemSpec_Restrict_, optionid, listView_AvailableOptions.SelectedItems[i].Name);
                new ItemSpec_Restrict_Value_SQL(DB).Add_ItemSpec_Restrict_Value(Item_, ItemSpec_Restrict_Options_);
            }
            this.Changed = true;
            Refresh_Values();
            buttonSetValues.Enabled = true ;

        }

        private void listView_ItemValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listView_ItemValues .ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                foreach (ListViewItem item1 in listView_ItemValues.Items)
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
                    MenuItem[] mi1 = new MenuItem[] { deletevalue  };
                    listView_ItemValues.ContextMenu = new ContextMenu(mi1);

                }

            }
        }

        private void listView_ItemValues_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData==Keys.Delete)
            {
                deletevalue.PerformClick();
            }
        }
    }
}
