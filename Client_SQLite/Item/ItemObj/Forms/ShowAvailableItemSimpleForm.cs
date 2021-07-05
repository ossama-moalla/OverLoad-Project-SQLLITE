using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
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
    public partial class ShowAvailableItemSimpleForm : Form
    {
        System.Windows.Forms.MenuItem OpenAvailableItem;

        List<Item_AvailableAmount_Report > itemslist;
        DatabaseInterface DB;
        private Item _ReturnItem;
        public Item ReturnItem
        {
            get { return _ReturnItem; }
        }
        bool GetITEM;
        List<TradeState> tradestateList;
        public ShowAvailableItemSimpleForm(DatabaseInterface db,bool GetITEM_)
        {
            DB = db;
            GetITEM = GetITEM_;
            InitializeComponent();
            tradestateList = new TradeStateSQL(DB).GetTradeStateList();
            for (int j = 0; j < tradestateList.Count; j++)
            {
                listView1.Columns.Add(tradestateList[j].TradeStateName, 100);
            }
            listView1.Columns.Add("مسار الصنف", 400);
            if (GetITEM == false) Select.Visible = false;
            itemslist = new List<Item_AvailableAmount_Report>();
            OpenAvailableItem  = new System.Windows.Forms.MenuItem("عرض التفاصيل", OpenAvailableItem_MenuItem_Click);
            LoadForm();
        }
        private void OpenAvailableItem_MenuItem_Click(object sender, EventArgs e)
        {

            string id_s = listView1.SelectedItems[0].Name;

            try
            {
                Item item  = new ItemSQL (DB).GetItemInfoByID(Convert.ToUInt32(id_s));
                AvailableItem_ItemIN_Form form = new AvailableItem_ItemIN_Form(DB, item,false );
                form.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private async  void LoadForm()
        {

            itemslist = new Trade.TradeSQL.AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List();


            optimaizeColumnsWidth();
            this.checkBoxAvailableOnly.CheckedChanged += new System.EventHandler(this.checkBoxAvailableOnly_CheckedChanged);
            this.comboBoxItemilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemilter_SelectedIndexChanged);
            this.comboBoxCompanyFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilter_SelectedIndexChanged);
            this.comboBoxFolderFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFolderFilter_SelectedIndexChanged);

            FillComboBox_Folder();
        }
        private void ShowAvailableItemSimpleForm_Load(object sender, EventArgs e)
        {
            

        }
        private void comboBoxFolderFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<Item_AvailableAmount_Report> filteredlist_byFolder = new List<Item_AvailableAmount_Report>();
            if (comboBoxFolderFilter.SelectedIndex == 0)
                filteredlist_byFolder = itemslist;
            else
                filteredlist_byFolder = itemslist.Where(x => x .FolderName == comboBoxFolderFilter.SelectedItem.ToString()).ToList();
            IEnumerable<string> distinctcompanies = filteredlist_byFolder.Select(x => x.ItemCompany).Distinct();
            comboBoxCompanyFilter.Items.Clear();
            comboBoxCompanyFilter.Items.Add("الكل");
            foreach (var s in distinctcompanies)
                comboBoxCompanyFilter.Items.Add(s);
            comboBoxCompanyFilter.SelectedIndex = 0;
        }
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<Item_AvailableAmount_Report> filteredlist_byFolder = new List<Item_AvailableAmount_Report>();
            if (comboBoxFolderFilter.SelectedIndex == 0)
                filteredlist_byFolder = itemslist;
            else
                filteredlist_byFolder = itemslist.Where(x => x.FolderName == comboBoxFolderFilter.SelectedItem.ToString()).ToList();
            List<Item_AvailableAmount_Report> filteredlist_byCompany = new List<Item_AvailableAmount_Report>();
            if (comboBoxCompanyFilter .SelectedIndex == 0)
                filteredlist_byCompany = filteredlist_byFolder;
            else
                filteredlist_byCompany = filteredlist_byFolder.Where(x => x.ItemCompany == comboBoxCompanyFilter.SelectedItem.ToString()).ToList();

            IEnumerable<string> distinctitems= filteredlist_byCompany.Select(x => x.ItemName).Distinct();
            comboBoxItemilter.Items.Clear();
            comboBoxItemilter.Items.Add("الكل");
            foreach (var s in distinctitems)
                comboBoxItemilter.Items.Add(s);
            comboBoxItemilter.SelectedIndex = 0;
  
            //RefereshList();
        }
        private void comboBoxItemilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefereshList();
        }
        private void FillComboBox_Folder()
        {

            IEnumerable<string> distincfolders = itemslist.Select(x => x .FolderName ).Distinct();
            comboBoxFolderFilter.Items.Clear();
            comboBoxFolderFilter.Items.Add("الكل");
            foreach (var s in distincfolders)
                comboBoxFolderFilter.Items.Add(s);
            comboBoxFolderFilter.SelectedIndex = 0;

        }
 
        public async  void RefereshList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < itemslist.Count; i++)
            {
                if (comboBoxCompanyFilter.SelectedIndex > 0 && itemslist[i] .ItemCompany != comboBoxCompanyFilter.SelectedItem.ToString()) continue;
                if (comboBoxFolderFilter.SelectedIndex > 0 && itemslist[i].FolderName != comboBoxFolderFilter.SelectedItem.ToString()) continue;
                if (comboBoxItemilter.SelectedIndex > 0 && itemslist[i].ItemName != comboBoxItemilter.SelectedItem.ToString()) continue;
    

                ListViewItem ListViewItem_ = new ListViewItem(itemslist[i] .FolderName);
                ListViewItem_.Name = itemslist[i].ItemID.ToString();
                ListViewItem_.SubItems.Add(itemslist[i].ItemName);
                ListViewItem_.SubItems.Add(itemslist[i].ItemCompany);

                double available_amount = 0;
                for (int j = 0; j < tradestateList.Count; j++)
                {
                    double a_v = itemslist[i].TradeState_AvailableAmount .Where(x => x._TradeState.TradeStateID == tradestateList[j].TradeStateID).Sum(y => y.AvailableAmount);
                    available_amount += a_v;
                    ListViewItem_.SubItems.Add(a_v.ToString());
                }
                if (available_amount == 0 && checkBoxAvailableOnly.Checked) continue;
                if (available_amount > 0) ListViewItem_.BackColor = Color.LimeGreen;
                else ListViewItem_.BackColor = Color.Orange;

                if (available_amount == 0 && checkBoxAvailableOnly.Checked) continue;
                ListViewItem_.SubItems.Add( itemslist[i].FolderPath );
                //ListViewItem_.UseItemStyleForSubItems = false;
                //ListViewItem_.SubItems[0].BackColor = Color.YellowGreen;
                //ListViewItem_.SubItems[1].BackColor = Color.Lime;
                //ListViewItem_.SubItems[2].BackColor = Color.Orange;
                //ListViewItem_.SubItems[3].BackColor = Color.Aquamarine;
                //ListViewItem_.SubItems[4].BackColor = Color.Cyan;
                if (available_amount == 0)
                    ListViewItem_.BackColor = Color.Orange;
                else
                    ListViewItem_.BackColor = Color.LightGreen;
                ListViewItem_.ImageIndex = 0;
                listView1.Items.Add(ListViewItem_);

            }

        }
        public async void optimaizeColumnsWidth()
        {
            //listView1.Columns[0].Width = 150;
            //listView1.Columns[1].Width = 150;
            //listView1.Columns[2].Width = 150;
            //listView1.Columns[3].Width = (listView1 .Width -360)/2;
            //listView1.Columns[4].Width = (listView1.Width - 360) / 2;
        }
        private void listView1_Resize(object sender, EventArgs e)
        {
            optimaizeColumnsWidth();
        }

        private void comboBoxFolderFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                comboBoxFolderFilter.SelectedIndex = 0;
        }

        private void comboBoxCompanyFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                comboBoxCompanyFilter .SelectedIndex = 0;
        }

     

        private void comboBoxItemilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                comboBoxItemilter.SelectedIndex = 0;
        }

        private void checkBoxAvailableOnly_CheckedChanged(object sender, EventArgs e)
        {
            RefereshList();
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
            {
                if(GetITEM )
                    SetReturnItem();
                else 
                OpenAvailableItem.PerformClick();

            }
        }
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {

                if (e.KeyData == Keys.Enter)
                {
                    if (GetITEM)
                        SetReturnItem();
                    else
                        OpenAvailableItem.PerformClick();

                }

            }
        }
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                List<MenuItem> MenuItemList = new List<MenuItem>();
                listView1.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                foreach (ListViewItem item1 in listView1.Items)
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

                    MenuItem[] mi1 = new MenuItem[] { OpenAvailableItem };
                    MenuItemList.AddRange(mi1);

                }
                //////////////

                listView1.ContextMenu = new ContextMenu(MenuItemList.ToArray());

            }

        }
        private void Select_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0 )
            {
                SetReturnItem();
            }
        }
        private void SetReturnItem()
        {
            if(listView1 .SelectedItems .Count >0)
            {
                _ReturnItem = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(listView1.SelectedItems[0].SubItems[0].Name));
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
