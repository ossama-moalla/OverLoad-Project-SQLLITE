using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Trade.Forms.TradeForms;
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

namespace OverLoad_Client.Maintenance.Forms
{
    public partial class RepairOPRForm : Form
    {

        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        RepairOPR _RepairOPR;
        //Folder LastUsedFolder;
        List<ItemOUT> _ItemOUTList = new List<ItemOUT>();


        ////////////////////////////////
        System.Windows.Forms.MenuItem OpenItemOUTMenuItem;
        System.Windows.Forms.MenuItem AddItemOUTMenuItem;
        System.Windows.Forms.MenuItem EditItemOUTMenuItem;
        System.Windows.Forms.MenuItem DeleteItemOUTMenuItem;

        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public RepairOPRForm(DatabaseInterface db,MaintenanceOPR MaintenanceOPR_)
        {
            DB = db;
            _MaintenanceOPR = MaintenanceOPR_;
            InitializeComponent();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            textBoxMaintenanceOPRID.Text = _MaintenanceOPR ._Operation.OperationID.ToString();
            textBoxContactName.Text = _MaintenanceOPR._Contact.ContactName;
            textBox_MaintenanceOPR_Date.Text = _MaintenanceOPR.EntryDate.ToString();


            InitializeMenuItems();
            //DiagnosticOPRReport. InitializeDiagnosticOPRListViewColumns (ref listViewSubDiagnosticOPR);
        }

        public RepairOPRForm(DatabaseInterface db, RepairOPR RepairOPR_, bool Edit)
        {
  
                DB = db;
                InitializeComponent();
                _RepairOPR = RepairOPR_;
                _MaintenanceOPR  = RepairOPR_._MaintenanceOPR  ;

                textBoxMaintenanceOPRID.Text = _MaintenanceOPR._Operation.OperationID.ToString();
                textBoxContactName.Text = _MaintenanceOPR._Contact.ContactName;
                textBox_MaintenanceOPR_Date.Text = _MaintenanceOPR.EntryDate.ToString();


                //DiagnosticOPRReport.InitializeDiagnosticOPRListViewColumns(ref listViewSubDiagnosticOPR);
                InitializeMenuItems();
                LoadForm(Edit);
 
           




        }
        public void InitializeMenuItems()
        {
            OpenItemOUTMenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية التركيب", OpenItemOUT_MenuItem_Click);
            AddItemOUTMenuItem = new System.Windows.Forms.MenuItem("تركيب عنصر", AddItemOUT_MenuItem_Click);
            EditItemOUTMenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditItemOUT_MenuItem_Click);
            DeleteItemOUTMenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteItemOUT_MenuItem_Click);

        }
        public void LoadForm(bool Edit)
        {
            if (_RepairOPR   == null) return;
            _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RepairOPR ._Operation);


            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            textBoxID.Text = _RepairOPR._Operation.OperationID.ToString();
            textBoxDesc .Text = _RepairOPR .RepairDesc  ;
            textBoxReport .Text = _RepairOPR.RepairReport ;
            dateTimePickerOPRDate.Value = _RepairOPR .RepairOPRDate ;

            RefreshInstalledItems(_ItemOUTList );
  

            if (Edit)
            {
                textBoxDesc.ReadOnly = false;
                textBoxReport .ReadOnly = false;

                dateTimePickerOPRDate .Enabled  = true ;
                this.listViewItemOUT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewItemOUT_MouseDoubleClick);
                this.listViewItemOUT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewItemOUT_MouseDown);


            }
            else
            {

                buttonSave.Visible = false;
                textBoxDesc.ReadOnly = true ;
                textBoxReport.ReadOnly = true;

                dateTimePickerOPRDate.Enabled = false ;
                ;

            }


        }
        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (_MaintenanceOPR    == null) return;
            //ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
            //ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

            //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
            //string SellType_ = ComboboxItem_selltype.Text;

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    
        

                    RepairOPR RepairOPR_ = new RepairOPRSQL(DB).AddRepairOPR  
                        (_MaintenanceOPR._Operation.OperationID  ,dateTimePickerOPRDate.Value
                        , textBoxDesc  .Text, textBoxReport.Text );
                    if (RepairOPR_ != null)
                    {
                        _RepairOPR  = RepairOPR_;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        LoadForm(true  );

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر انشاء عملية الاصلاح " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_RepairOPR     != null)
                    {
                       
                        bool success = new RepairOPRSQL(DB).UpdateRepairOPR 
                        (_RepairOPR ._Operation.OperationID, dateTimePickerOPRDate.Value
                        , textBoxDesc.Text, textBoxReport.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _RepairOPR   = new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID (_RepairOPR ._Operation.OperationID );
                            this.Changed_ = true;
                            LoadForm(true );
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
        #region ItemOUT
        private void listViewItemOUT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewItemOUT.SelectedItems.Count > 0)
            {
                OpenItemOUTMenuItem.PerformClick();
            }
        }
        private void listViewItemOUT_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewItemOUT.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewItemOUT.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenItemOUTMenuItem, EditItemOUTMenuItem, DeleteItemOUTMenuItem, new MenuItem("-"), AddItemOUTMenuItem };
                        listViewItemOUT.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddItemOUTMenuItem };
                        listViewItemOUT.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemOUT_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private void listViewItemsOUT_Resize(object sender, EventArgs e)
        {
            AdjustListViewItemsOUTColumnsWidth();
        }
        public async void AdjustListViewItemsOUTColumnsWidth()
        {
            try
            {
                listViewItemOUT.Columns[0].Width = 80;
                int w = (listViewItemOUT.Width - 80) / 8 - 1;
                listViewItemOUT.Columns[1].Width = w;
                listViewItemOUT.Columns[2].Width = w;
                listViewItemOUT.Columns[3].Width = w;
                listViewItemOUT.Columns[4].Width = w;
                listViewItemOUT.Columns[5].Width = w;
                listViewItemOUT.Columns[6].Width = w;
                listViewItemOUT.Columns[7].Width = w;
                listViewItemOUT.Columns[8].Width = w;
            }
            catch (Exception ee)
            {
                MessageBox.Show("AdjustListViewItemsOUTColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          


        }
        private void DeleteItemOUT_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
                bool success = new ItemOUTSQL(DB).DeleteItemOUT(sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RepairOPR ._Operation);
                    RefreshInstalledItems(_ItemOUTList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemOUT.SelectedItems.Count > 0)
                {
                    uint itemoutid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
                    ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, true);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RepairOPR._Operation);
                        RefreshInstalledItems(_ItemOUTList);
                    }
                    ItemOUTForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void OpenItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemOUT.SelectedItems.Count > 0)
                {

                    uint itemoutid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
                    ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, false);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RepairOPR._Operation);
                        RefreshInstalledItems(_ItemOUTList);
                    }
                    ItemOUTForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemOUT_MenuItem_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           
        }
        private async void RefreshInstalledItems(List<ItemOUT> ItemOUTList)
        {
            try
            {
                listViewItemOUT.Items.Clear();
                double totalcost = 0;
                for (int i = 0; i < ItemOUTList.Count; i++)
                {
                    double sellprice = ItemOUTList[i]._OUTValue.Value;
                    double total_sellprice = System.Math.Round(sellprice * ItemOUTList[i].Amount, 2);
                    totalcost = totalcost + total_sellprice;
                    ListViewItem ListViewItem_ = new ListViewItem((listViewItemOUT.Items.Count + 1).ToString());
                    ListViewItem_.Name = ItemOUTList[i].ItemOUTID.ToString();
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._TradeState.TradeStateName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i].Amount.ToString());
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ConsumeUnit.ConsumeUnitName.ToString());


                    //ListViewItem_.SubItems.Add(sellprice.ToString() + " " +_MaintenanceFault._MaintenanceOPR. _BillMaintenance._Currency.CurrencySymbol.Replace(" ", string.Empty));
                    //ListViewItem_.SubItems.Add((total_sellprice).ToString() + " " + _MaintenanceFault._MaintenanceOPR._BillMaintenance._Currency.CurrencySymbol.Replace(" ", string.Empty));
                    ListViewItem_.SubItems.Add(ItemOUTList[i].Notes);
                    ListViewItem_.BackColor = Color.Orange;
                    listViewItemOUT.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshInstalledItems:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }

        private void AddItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BillMaintenance billmaintenance = new BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(_RepairOPR._MaintenanceOPR);
                if (billmaintenance == null) throw new Exception("يجب انشاء فاتورة صيانة");
                ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _RepairOPR._Operation);
                DialogResult d = ItemOUTForm_.ShowDialog();
                if (ItemOUTForm_.Changed)
                {
                    _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RepairOPR._Operation);
                    RefreshInstalledItems(_ItemOUTList);
                }
                ItemOUTForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddItemOUT_MenuItem_Click:"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
          
        }
        #endregion
    }
}
