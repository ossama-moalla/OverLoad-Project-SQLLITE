using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
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

namespace OverLoad_Client.Trade.Forms.TradeForms
{
    public partial class AssemblageForm : Form
    {
        System.Windows.Forms.MenuItem OpenItemOUTMenuItem;
        System.Windows.Forms.MenuItem AddItemOUTMenuItem;
        System.Windows.Forms.MenuItem EditItemOUTMenuItem;
        System.Windows.Forms.MenuItem DeleteItemOUTMenuItem;




        List<ItemOUT> _ItemOUTList = new List<ItemOUT>();

        DatabaseInterface DB;
        AssemblabgeOPR _AssemblabgeOPR;
        //Currency _Currency;
        private bool _Changed;
        public bool Changed
        {
            get { return _Changed; }
        }
        //bool Edit;

        //TradeStorePlace _TempStorePlace;
        double TotalItemsout_value = 0;
        public AssemblageForm(DatabaseInterface db)
        {

            InitializeComponent();
            InitializeMenuItems();
            DB = db;
            panelItemIN.Enabled = false;
            panelItemsOUT.Enabled = false;
            AdjustListViewColumnsWidth();
      
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            Currency  _Currency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _Currency);
            textBoxExchangeRate.Text = _Currency.ExchangeRate  .ToString();
            textBoxBuyPrice.Text = "0";
            this.listView.Resize += new System.EventHandler(this.listView_Resize);

        }

        public AssemblageForm(DatabaseInterface db,  AssemblabgeOPR AssemblabgeOPR_, bool Edit_)
        {
            InitializeComponent();
            InitializeMenuItems();
            AdjustListViewColumnsWidth();

            DB = db;


            _AssemblabgeOPR  = AssemblabgeOPR_;


            Currency currency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);

            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, currency);
            LoadForm(Edit_);
            this.listView.Resize += new System.EventHandler(this.listView_Resize);

        }
        private void LoadItemINData(bool Edit)
        {
            try
            {
                if (_AssemblabgeOPR == null) return;

                if (_AssemblabgeOPR._ItemIN == null)
                {
                    buttonItemIN_Create.Name = "buttonItemIN_Create";
                    buttonItemIN_Create.Text  = "انشاء";
                    if (Edit)
                    {
                        buttonItemIN_Create.Visible = true;
                        buttonItemIN_Edit.Visible = false;
                        buttonItemIN_Delete.Visible = false;
                    }
                    else
                    {
                        buttonItemIN_Create.Visible = false;
                        buttonItemIN_Edit.Visible = false;
                        buttonItemIN_Delete.Visible = false;
                    }
                    textBoxItemID.Text = "";
                    textBoxItemName.Text = "";
                    textBoxItemCompany.Text = "";
                    textBoxItemType.Text = "";
                    textBoxConsumeUnit.Text = "";
                    textBoxAmount.Text = "";
                    textBoxTradestate.Text = "";

                }
                else
                {
                    buttonItemIN_Create.Name = "buttonItemIN_Open";
                    buttonItemIN_Create.Text = "استعراض";
                    if (Edit)
                    {
                        buttonItemIN_Create.Visible = true;
                        buttonItemIN_Edit.Visible = true;
                        buttonItemIN_Delete.Visible = true;
                    }
                    else
                    {
                        buttonItemIN_Create.Visible = true;
                        buttonItemIN_Edit.Visible = false;
                        buttonItemIN_Delete.Visible = false;
                    }

                    textBoxItemID.Text = _AssemblabgeOPR._ItemIN._Item.ItemID.ToString();
                    textBoxItemName.Text = _AssemblabgeOPR._ItemIN._Item.ItemName;
                    textBoxItemCompany.Text = _AssemblabgeOPR._ItemIN._Item.ItemCompany;
                    textBoxItemType.Text = _AssemblabgeOPR._ItemIN._Item.folder.FolderName;
                    if (_AssemblabgeOPR._ItemIN._ConsumeUnit == null) textBoxConsumeUnit.Text = _AssemblabgeOPR._ItemIN._Item.DefaultConsumeUnit;
                    else textBoxConsumeUnit.Text = _AssemblabgeOPR._ItemIN._ConsumeUnit.ConsumeUnitName;
                    textBoxAmount.Text = _AssemblabgeOPR._ItemIN.Amount.ToString();
                    textBoxTradestate.Text = _AssemblabgeOPR._ItemIN._TradeState.TradeStateName;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadItemINData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
           
        }
        public void InitializeMenuItems()
        {
            OpenItemOUTMenuItem = new System.Windows.Forms.MenuItem("عرض التافصيل", OpenItemOUT_MenuItem_Click);

            AddItemOUTMenuItem = new System.Windows.Forms.MenuItem("اضافة عنصر مساهم في عملية التجميع", AddItemOUT_MenuItem_Click);
            EditItemOUTMenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditItemOUT_MenuItem_Click);
            DeleteItemOUTMenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteItemOUT_MenuItem_Click); ;


        }
        public void LoadForm(bool edit)
        {
            try
            {
                panelItemIN.Enabled = true ;
                panelItemsOUT.Enabled = true ;
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                if (_AssemblabgeOPR == null) return;
                textBoxOprDesc.Text = _AssemblabgeOPR.OprDesc;
                textBoxAssemblageID.Text = _AssemblabgeOPR._Operation.OperationID.ToString();
                dateTimePicker_.Value = _AssemblabgeOPR.OprDate;
                TextboxNotes.Text = _AssemblabgeOPR.Notes;
                LoadItemINData(edit );
                _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation);
                RefreshAssemblageItemOutLIst(_ItemOUTList);
                if (edit)
                {
                    this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
                    this.listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);

                    dateTimePicker_.Enabled = true ;
                  
                    TextboxNotes.ReadOnly = false ;
                }
                else
                {

                    dateTimePicker_.Enabled = false ;

                    TextboxNotes.ReadOnly = true ;

                }

  
            }
            catch(Exception ee)
            {
                MessageBox.Show("LoadingForm:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }




        }
      
       
       
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                AssemblageSQL AssemblageSQL_ = new AssemblageSQL(DB);
                if (buttonSave.Name == "buttonAdd")
                {

                    AssemblabgeOPR AssemblabgeOPR_ = AssemblageSQL_.CreateAssemblageOPR
                        (dateTimePicker_.Value, textBoxOprDesc.Text
                        , TextboxNotes.Text);
                    if (AssemblabgeOPR_ != null)
                    {
                        _AssemblabgeOPR = AssemblabgeOPR_;
                        MessageBox.Show("تم انشاء عملية التجميع بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this._Changed = true;
                        LoadForm(true);

                    }
                }

                else
                {

                    if (_AssemblabgeOPR != null)
                    {


                        bool success = AssemblageSQL_.UpdateAssemblageOPR
                            (_AssemblabgeOPR._Operation.OperationID, dateTimePicker_.Value, textBoxOprDesc.Text
                        , TextboxNotes.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _AssemblabgeOPR = AssemblageSQL_.GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);
                            this._Changed = true;
                            LoadForm(true);
                        }
                        else MessageBox.Show("لم يتم الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

          
        }
        
   

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView.SelectedItems.Count > 0)
            {
                OpenItemOUTMenuItem.PerformClick();
            }
        }
        
        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listView.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listView.Items)
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
                        listView.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddItemOUTMenuItem };
                        listView.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

           

        }
           
 
   
        private void DeleteItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                //if (new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation).Count <= 2 && _AssemblabgeOPR._ItemIN != null) throw new Exception("يجب حذف العنصر المجمع اولا");

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listView.SelectedItems[0].Name);
                bool success = new ItemOUTSQL(DB).DeleteItemOUT(sid);
                if (success)
                {
                    this._Changed = true;
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);

                    _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation);
                    RefreshAssemblageItemOutLIst(_ItemOUTList);

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
                if (listView.SelectedItems.Count > 0)
                {
                    uint itemoutid = Convert.ToUInt32(listView.SelectedItems[0].Name);
                    ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, true);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        this._Changed = true;
                        _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation);
                        _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);

                        RefreshAssemblageItemOutLIst(_ItemOUTList);
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
                if (listView.SelectedItems.Count > 0)
                {

                    uint itemoutid = Convert.ToUInt32(listView.SelectedItems[0].Name);
                    ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, false);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        this._Changed = true;
                        _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);

                        _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation);
                        RefreshAssemblageItemOutLIst(_ItemOUTList);
                    }
                    ItemOUTForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
        }

        private void AddItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _AssemblabgeOPR._Operation);
                DialogResult d = ItemOUTForm_.ShowDialog();
                if (ItemOUTForm_.Changed)
                {
                    this._Changed = true;
                    _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);

                    _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation);
                    RefreshAssemblageItemOutLIst(_ItemOUTList);
                }
                ItemOUTForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void RefreshAssemblageItemOutLIst(List<ItemOUT> ItemOUTList)
        {

            try
            {
                listView.Items.Clear();
                ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);





                TotalItemsout_value = 0;

                for (int i = 0; i < ItemOUTList.Count; i++)
                {
                    double sellprice = ItemOUTList[i]._ItemIN._INCost.Value;
                    double total_sellprice = System.Math.Round(sellprice * ItemOUTList[i].Amount, 3);
                    TotalItemsout_value = TotalItemsout_value + (total_sellprice / ItemOUTList[i]._ItemIN._INCost.ExchangeRate);
                    ListViewItem ListViewItem_ = new ListViewItem((listView.Items.Count + 1).ToString());
                    ListViewItem_.Name = ItemOUTList[i].ItemOUTID.ToString();
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._TradeState.TradeStateName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i].Amount.ToString());
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ConsumeUnit.ConsumeUnitName.ToString());


                    ListViewItem_.SubItems.Add(sellprice.ToString() + " " + ItemOUTList[i]._ItemIN._INCost._Currency.CurrencySymbol.Replace(" ", string.Empty));
                    ListViewItem_.SubItems.Add((total_sellprice).ToString() + " " + ItemOUTList[i]._ItemIN._INCost._Currency.CurrencySymbol.Replace(" ", string.Empty));
                    ListViewItem_.SubItems.Add(ItemOUTList[i].Notes);
                    ListViewItem_.BackColor = Color.Orange;
                    listView.Items.Add(ListViewItem_);

                }
                ComboboxItem it = (ComboboxItem)comboBoxCurrency.SelectedItem;
                Currency curency = new CurrencySQL(DB).GetCurrencyINFO_ByID(it.Value);
                textBoxExchangeRate.Text = curency.ExchangeRate.ToString();
                textBoxbuyprice_ALL.Text = (TotalItemsout_value * curency.ExchangeRate).ToString() + " " +
                    curency.CurrencySymbol;
                if (_AssemblabgeOPR._ItemIN != null)
                    textBoxBuyPrice.Text = (TotalItemsout_value / (_AssemblabgeOPR._ItemIN._ConsumeUnit.Factor * _AssemblabgeOPR._ItemIN.Amount) * curency.ExchangeRate).ToString() + " " +
                        curency.CurrencySymbol;
                else
                    textBoxBuyPrice.Text = "-";
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshAssemblageItemOutLIst:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            AdjustListViewColumnsWidth();
        }
        public void AdjustListViewColumnsWidth()
        {
            try
            {
                listView.Columns[0].Width = 80;

                listView.Columns[1].Width = (listView.Width - 80) / 8 - 1;
                listView.Columns[2].Width = (listView.Width - 80) / 8 - 1;
                listView.Columns[3].Width = (listView.Width - 80) / 8 - 1;
                listView.Columns[4].Width = (listView.Width - 80) / 8 - 1;
                listView.Columns[5].Width = (listView.Width - 80) / 8 - 1;
                listView.Columns[6].Width = (listView.Width - 80) / 8 - 1;
                listView.Columns[7].Width = (listView.Width - 80) / 8 - 1;
                listView.Columns[8].Width = (listView.Width - 80) / 8 - 1;
            }
            catch (Exception ee)
            {
                MessageBox.Show("AdjustListViewColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        


        }

        private void عرضالعناصرالخارجةمنالعنصرالناتجعنهذهالعمليةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<ItemIN> itemin = new ItemINSQL(DB).GetItemINList(_AssemblabgeOPR._Operation);
                if (itemin.Count > 0)
                {
                    ItemIN_ItemOutListForm ItemIN_ItemOutListForm_ = new ItemIN_ItemOutListForm(DB, itemin[0]);
                    ItemIN_ItemOutListForm_.ShowDialog();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(":" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
      
          
        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem it = (ComboboxItem)comboBoxCurrency.SelectedItem;
                Currency curency = new CurrencySQL(DB).GetCurrencyINFO_ByID(it.Value);
                textBoxExchangeRate.Text = curency.ExchangeRate.ToString();
                textBoxBuyPrice.Text =Math .Round ( (TotalItemsout_value * curency.ExchangeRate),3).ToString() + " " +
                    curency.CurrencySymbol;
                try{

                    textBoxbuyprice_ALL.Text = Math.Round(_AssemblabgeOPR ._ItemIN .Amount* (TotalItemsout_value * curency.ExchangeRate), 3).ToString() + " " +
                  curency.CurrencySymbol;
                }
                catch
                {
                    textBoxbuyprice_ALL.Text = "-";
                }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxCurrency_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
 
        }

        private void buttonItemIN_Create_Click(object sender, EventArgs e)
        {

            try
            {
                if (_AssemblabgeOPR._ItemIN==null )
                {
                    //if (new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation).Count <2) throw new Exception("لا يمكن ادخال عنصر مجمع قبل اخراج عنصرين على الاقل");
                    ItemINForm ItemINForm_ = new ItemINForm(DB, _AssemblabgeOPR._Operation);
                    ItemINForm_.ShowDialog();
                    if (ItemINForm_.Changed)
                    {
                        _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);
                        LoadForm(true);
                    }
                }
                else
                {
                    ItemINForm ItemINForm_ = new ItemINForm(DB, _AssemblabgeOPR._ItemIN ,false );
                    ItemINForm_.ShowDialog();
                    if (ItemINForm_.Changed)
                    {
                        _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);
                        LoadForm(true);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonItemIN_Create_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
            
        }

        private void buttonItemIN_Edit_Click(object sender, EventArgs e)
        {

            try
            {
                if (_AssemblabgeOPR._ItemIN == null)
                {
                    ItemINForm ItemINForm_ = new ItemINForm(DB, _AssemblabgeOPR._Operation);
                    ItemINForm_.ShowDialog();
                    if (ItemINForm_.Changed)
                    {
                        _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);
                        LoadForm(true);
                    }
                }
                else
                {
                    ItemINForm ItemINForm_ = new ItemINForm(DB, _AssemblabgeOPR._ItemIN, true );
                    ItemINForm_.ShowDialog();
                    if (ItemINForm_.Changed)
                    {
                        _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);
                        LoadForm(true);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonItemIN_Edit_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonItemIN_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (new ItemOUTSQL(DB).GetItemOUTList(_AssemblabgeOPR._Operation).Count > 0) throw new Exception("يجب اولا حذف العناصر الداخلة في التجميع");
                if (_AssemblabgeOPR._ItemIN != null)
                {
                    DialogResult dd = MessageBox.Show("هل انت متأكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning );
                    if (dd != DialogResult.OK) return;
                    bool success  = new ItemINSQL (DB).DeleteItemIN (_AssemblabgeOPR._ItemIN.ItemINID);
                    if (success)
                    {
                        _AssemblabgeOPR = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_AssemblabgeOPR._Operation.OperationID);
                        LoadForm(true);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonItemIN_Delete_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
