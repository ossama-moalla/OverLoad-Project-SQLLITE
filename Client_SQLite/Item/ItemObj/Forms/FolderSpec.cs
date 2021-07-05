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
    public partial class FolderSpec : Form
    {
        Folder Folder_;
        ListViewItem SelectedItem;
        ItemSpecSQL ItemSpecSQL_;
        ItemSpec_Restrict_SQL ItemSpec_Restrict_SQL_;
        DatabaseInterface DB;
        MenuItem UpdateSpe;
        MenuItem DeleteSpec;
        MenuItem ShowOptions;

        public FolderSpec(DatabaseInterface db,Folder Folder__)
        {
            DB = db;
            InitializeComponent();
            UpdateSpe =new MenuItem ("تعديل", UpdateSpec_MenuItem_Click);
            DeleteSpec = new MenuItem("حذف", DeleteSpec_MenuItem_Click);
            ShowOptions = new MenuItem("اظهار القيم التابعة لهذه الخاصية", ShowOptions_MenuItem_Click);
            ItemSpec_Restrict_SQL_ = new ItemSpec_Restrict_SQL(DB);
            ItemSpecSQL_ = new ItemSpecSQL(DB);
            comboBoxSpecType.SelectedIndex = 0;
            Folder_ = Folder__;
            textBoxFolderName.Text = Folder_.FolderName;
            FillListview();
            
        }
        public void FillListview()
        {
            listViewSpecs.Items.Clear();
            List<ItemSpecDisplay> ItemSpecDisplayList = new List<ItemSpecDisplay>();
            List<ItemSpec  > ItemSpecList = ItemSpecSQL_.GetItemSpecList (Folder_);
            List<ItemSpec_Restrict> ItemSpec_Restrict_List = ItemSpec_Restrict_SQL_.GetItemSpecRestrictList(Folder_);
            ItemSpec_Restrict_Options_SQL ItemSpec_Restrict_Options_SQL = new ItemSpec_Restrict_Options_SQL(DB);
            for (int i = 0; i < ItemSpec_Restrict_List.Count; i++)
            {
                ItemSpecDisplayList.Add(new ItemSpecDisplay (Folder_ , ItemSpec_Restrict_List[i].SpecID , ItemSpec_Restrict_List[i].SpecName , ItemSpec_Restrict_List[i].SpecIndex ,false ));
            }
            for (int i = 0; i < ItemSpecList.Count; i++)
            {
                ItemSpecDisplayList.Add(new ItemSpecDisplay(Folder_, ItemSpecList[i].SpecID, ItemSpecList[i].SpecName, ItemSpecList[i].SpecIndex,true ));
            }
            ItemSpecDisplayList =ItemSpecDisplayList.OrderBy (m => m.SpecIndex).ToList();
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
                    item.SubItems.Add(" مقيدة" );

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
                        MessageBox.Show("الفهرس يجب ان يكون عدد صحيح موجب","",MessageBoxButtons.OK,MessageBoxIcon.Error );
                        return;
                    }
                    bool result = ItemSpecSQL_.AddItemSpec(Folder_, textBoxSpecName.Text,index );

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
                    bool result = ItemSpec_Restrict_SQL_.AddItemSpecRestrict (Folder_, textBoxSpecName.Text,index );
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
                    bool result=false ;
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
                        Button_Spec_Cancel.DialogResult  = DialogResult.Cancel;
                        Button_Spec_Cancel.Text = "اغلاق";
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
                    if (listitem.Name.Substring (0, 1) == "0")
                     mi1 = new MenuItem[] {ShowOptions , UpdateSpe, DeleteSpec };
                    else
                        mi1 = new MenuItem[] {  UpdateSpe, DeleteSpec };
                    listViewSpecs.ContextMenu = new ContextMenu(mi1);
                }
     
            }
        }
        private void UpdateSpec_MenuItem_Click(object sender, EventArgs e)
        {
            Button_Spec_Add.Name  = "Button_Spec_Update";
            Button_Spec_Add.Text = "تعديل";
            Button_Spec_Cancel.Text = "الغاء";
            Button_Spec_Cancel.DialogResult = DialogResult.None;
            listViewSpecs.Enabled = false;
            SelectedItem = listViewSpecs.SelectedItems[0];
            textBoxSpecName.Text = listViewSpecs.SelectedItems[0].Text;
            textBoxIndex.Text = listViewSpecs.SelectedItems[0].SubItems [2].Text ;

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
                    string SpecName = listViewSpecs.SelectedItems[0].Text;
                    uint index = Convert.ToUInt32(listViewSpecs.SelectedItems[0].Name.Substring(2));

                    ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict(Folder_, specid, SpecName, index);
                    Form_Spec_Option Form_Spec_Option = new Form_Spec_Option(DB, ItemSpec_Restrict_);
                    Form_Spec_Option.ShowDialog();
                }
            }
        }
        private void Close_Click(object sender, EventArgs e)
        {
          if(Button_Spec_Add.Name == "Button_Spec_Update")
            {
                Button_Spec_Cancel.Text = "اغلاق";
                Button_Spec_Cancel.DialogResult = DialogResult.Cancel ;
            
                listViewSpecs.Enabled = true;
                Button_Spec_Add.Name = "Button_Spec_Add";
                Button_Spec_Add.Text = "اضافة";
                textBoxSpecName.Text  = "";
                comboBoxSpecType.SelectedIndex = 0;
                comboBoxSpecType.Enabled = true;

            }
        }

        private void listViewSpecs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewSpecs.SelectedItems.Count > 0)
            {
                if (listViewSpecs.SelectedItems[0].Name.Substring(0, 1) == "0")
                {
                    uint specid = Convert.ToUInt32(listViewSpecs.SelectedItems[0].Name.Substring(1));
                    string SpecName = listViewSpecs.SelectedItems[0].Text;
                    uint index = Convert.ToUInt32(listViewSpecs.SelectedItems[0].Name.Substring(2));

                    ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict(Folder_, specid, SpecName,index );
                    Form_Spec_Option Form_Spec_Option = new Form_Spec_Option(DB, ItemSpec_Restrict_);
                    Form_Spec_Option.ShowDialog();
                }
            }
        }

        private void listViewSpecs_Resize(object sender, EventArgs e)
        {
            for(int i=0;i<listViewSpecs .Columns .Count;i++)
            {
                listViewSpecs.Columns[i].Width = listViewSpecs.Width / 3;
            }
        }
    }
}
