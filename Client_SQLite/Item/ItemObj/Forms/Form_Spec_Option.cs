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

    public partial class Form_Spec_Option : Form
    {
        ItemSpec_Restrict ItemSpec_Restrict_;
        ListViewItem SelectedItem;
        ItemSpec_Restrict_Options_SQL ItemSpec_Restrict_Options_SQL_;
        DatabaseInterface DB;
        MenuItem Update_Option;
        MenuItem Delete_Option;
        public Form_Spec_Option(DatabaseInterface db, ItemSpec_Restrict ItemSpec_Restrict__)
        {
            DB = db;
            InitializeComponent();
            Update_Option = new MenuItem("تعديل", Update_Option_MenuItem_Click);
            Delete_Option = new MenuItem("حذف", Delete_Option_MenuItem_Click);
            ItemSpec_Restrict_Options_SQL_ = new ItemSpec_Restrict_Options_SQL(DB);

            ItemSpec_Restrict_ = ItemSpec_Restrict__;
            textBox_SpecName.Text = ItemSpec_Restrict__.SpecName;
            FillListview();

        }
        public void FillListview()
        {
            listViewOptions .Items.Clear();
            List<ItemSpec_Restrict_Options > ItemSpec_Restrict_Options_List= ItemSpec_Restrict_Options_SQL_.GetItemSpec_Restrict_Options_List(ItemSpec_Restrict_ );
            for (int i = 0; i < ItemSpec_Restrict_Options_List.Count; i++)
            {
                ListViewItem item = new ListViewItem(ItemSpec_Restrict_Options_List[i].OptionName);
                item.Name =  ItemSpec_Restrict_Options_List[i].OptionID .ToString();
                listViewOptions.Items.Add(item);
            }
     
        }
        private void ADD_Click(object sender, EventArgs e)
        {
            if (textBoxOptionName  .Text.Length == 0)
            {
                MessageBox.Show("ادخل اسم القيمة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Button_Option_Add.Name == "Button_Option_Add")
            {
                bool result = ItemSpec_Restrict_Options_SQL_.Add_ItemSpec_Restrict_Option(ItemSpec_Restrict_, textBoxOptionName.Text);
                
                    if (result)
                    {
                        textBoxOptionName.Text = "";
                        FillListview();
                    }

            }
            else
            {
                if (Button_Option_Add.Name == "Button_Option_Update")
                {
                    bool result = false;
                    uint optionid = Convert.ToUInt32(SelectedItem.Name);
                     result = ItemSpec_Restrict_Options_SQL_.Update_ItemSpec_Restrict_Option(optionid , textBoxOptionName.Text); 
                    if (result)
                    {
                        Button_Option_Add.Name = "Button_Option_Add";
                        Button_Option_Add.Text = "أضف";
                        Button_Option_Cancel.DialogResult = DialogResult.Cancel;
                        Button_Option_Cancel.Text = "اغلاق";
                        listViewOptions .Enabled = true;
                        textBoxOptionName.Text = "";


                        FillListview();
                    }

                }
            }

        }
        private void listViewSpecs_MouseDown(object sender, MouseEventArgs e)
        {
            listViewOptions .ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewOptions.Items)
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
                    MenuItem[] mi1 = new MenuItem[] { Update_Option, Delete_Option  };
                    listViewOptions.ContextMenu = new ContextMenu(mi1);
                }

            }
        }
        private void Update_Option_MenuItem_Click(object sender, EventArgs e)
        {
            Button_Option_Add.Name = "Button_Option_Update";
            Button_Option_Add.Text = "تعديل";
            Button_Option_Cancel.Text = "الغاء";
            Button_Option_Cancel.DialogResult = DialogResult.None;
            listViewOptions.Enabled = false;
            SelectedItem = listViewOptions.SelectedItems[0];
            textBoxOptionName.Text = listViewOptions.SelectedItems[0].Text;
            textBoxOptionName.Focus();


        }
        private void Delete_Option_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewOptions.SelectedItems.Count > 0)
            {
                MessageBox.Show("هل انت متأكد من الحذف", "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                for (int i = 0; i < listViewOptions.SelectedItems.Count; i++)
                {
                    bool result = false;
                    uint optionid = Convert.ToUInt32(listViewOptions.SelectedItems[i].Name);
                    result = ItemSpec_Restrict_Options_SQL_.Delete_ItemSpec_Restrict_Option(optionid);
                  
                }
                FillListview();
            }
            

        }

        private void Close_Click(object sender, EventArgs e)
        {
            if (Button_Option_Add.Name == "Button_Option_Update")
            {
                Button_Option_Cancel .Text = "اغلاق";
                Button_Option_Cancel.DialogResult = DialogResult.Cancel;

                listViewOptions .Enabled = true;
                Button_Option_Add.Name = "Button_Option_Add";
                Button_Option_Add.Text = "اضافة";
                textBoxOptionName .Text = "";


            }
        }

        private void listViewOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                if (listViewOptions.SelectedItems.Count > 0)
                    Delete_Option.PerformClick();
        }

        private void textBoxOptionName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                Button_Option_Add.PerformClick();
        }
    }
}

