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
    public partial class ItemINForm : Form
    {
      

        System.Windows.Forms.MenuItem setPriceMenuItem;
        System.Windows.Forms.MenuItem UnsetpriceMenuItem;

       
       
        List<SellType> selltypelist = new List<SellType>();
        List<TradeState> TradeStateList = new List<TradeState>();
        List<ItemINSellPrice> ItemINSellPriceList = new List<ItemINSellPrice>();
        List<ConsumeUnit> ConsumUnitsList = new List<ConsumeUnit>();
        DatabaseInterface DB;
        ItemIN _ItemIN;
        Operation _Operation;
        Currency _Currency;
        Item _Item;
        Folder LastUsedFolder;
        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        //bool Edit;
   
        public ItemINForm(DatabaseInterface db, Operation Operation_)
        {
            InitializeComponent();
            DB = db;
          
            
            setPriceMenuItem = new System.Windows.Forms.MenuItem("ضبط السعر", setprice_MenuItem_Click);
            UnsetpriceMenuItem = new System.Windows.Forms.MenuItem("الغاء ضبط السعر", Unsetprice_MenuItem_Click);


            TradeStateList = new TradeStateSQL(DB).GetTradeStateList();
            selltypelist = new SellTypeSql(DB).GetSellTypeList();

            _Operation = Operation_;
            textBox_ItemIN_OperationType.Text = Operation.GetOperationName(_Operation.OperationType);
            textBox_ItemIN_OperationID.Text =_Operation.OperationID.ToString ();
            switch (_Operation .OperationType)
            {
                case Operation.BILL_BUY:
                    panelINCost.Visible = true;
                    break;
                case Operation.ASSEMBLAGE :
                    panelINCost.Visible = false ;
                    break;
                case Operation.DISASSEMBLAGE:
                    labelBuyCost.Text = "التكلفة من التكلفة الاجمالية للمفكك";
                    panelINCost.Visible = true;

                    break;
                default :
                    throw new Exception("عملية ادخال عنصر غير صحيحة");
            }
            _Currency = new OperationSQL(DB).GetOperationItemINCurrency(_Operation);
            LastUsedFolder = null;

            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            this.textBoxAmount.TextChanged += new System.EventHandler(this.RefreshTotalCost_TextChanged);
            this.textBoxBuyPrice.TextChanged += new System.EventHandler(this.RefreshTotalCost_TextChanged);
            textBoxExchangeRate.Text = _Currency.ExchangeRate  .ToString();
            textBoxCurrency.Text = _Currency.CurrencyName;
            FillComboBoxTradeState(null);
            comboBoxConsumeUnt.Enabled = false;
            textBoxAmount.Text = "1";
            textBoxBuyPrice.Text = "0";
            InitialzeDataGridViewItemSellPrices();
            this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
            this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
            this.comboBoxConsumeUnt.SelectedIndexChanged += new System.EventHandler(this.comboBoxConsumeUnt_SelectedIndexChanged);


        }

        public ItemINForm(DatabaseInterface db,  ItemIN ItemIN_, bool? Edit_)
        {
            
            InitializeComponent();
            DB = db;
            TradeStateList = new TradeStateSQL(DB).GetTradeStateList();
            selltypelist = new SellTypeSql(DB).GetSellTypeList();
            InitialzeDataGridViewItemSellPrices();

            _ItemIN = ItemIN_;
            _Currency = new OperationSQL(DB).GetOperationItemINCurrency(_ItemIN._Operation) ;
            _Operation = _ItemIN._Operation;
            textBox_ItemIN_OperationType.Text = Operation.GetOperationName(_Operation.OperationType);
            textBox_ItemIN_OperationID.Text = _Operation.OperationID.ToString();
            switch (_Operation.OperationType)
            {
                case Operation.BILL_BUY:
                    panelINCost.Visible = true;
                    break;
                case Operation.ASSEMBLAGE:
                    panelINCost.Visible = false;
                    break;
                case Operation.DISASSEMBLAGE:
                    labelBuyCost.Text = "التكلفة من التكلفة الاجمالية للمفكك";

                    panelINCost.Visible = true;

                    break;
                default:
                    throw new Exception("عملية ادخال عنصر غير صحيحة");
            }
            _Item = _ItemIN._Item;
      
            setPriceMenuItem = new System.Windows.Forms.MenuItem("ضبط السعر", setprice_MenuItem_Click);
            UnsetpriceMenuItem = new System.Windows.Forms.MenuItem("الغاء ضبط السعر", Unsetprice_MenuItem_Click);
            this.textBoxBuyPrice.TextChanged += new System.EventHandler(this.RefreshTotalCost_TextChanged);




            textBoxExchangeRate.Text = _Currency .ExchangeRate .ToString();
            textBoxCurrency.Text =_Currency.CurrencyName;
            ////FillComboBoxConsumeUnit(_ItemIN ._ConsumeUnit);
            ////FillComboBoxTradeState (_ItemIN ._TradeState );
            LoadForm(Edit_);
 
            


        }

        public void LoadForm(bool? Edit)
        {
            try
            {
                this.comboBoxConsumeUnt.SelectedIndexChanged -= new System.EventHandler(this.comboBoxConsumeUnt_SelectedIndexChanged);
                this.textBoxItemID.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
                this.textBoxItemID.MouseDoubleClick -= new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
                ItemINSellPriceList = new ItemINSellPriceSql(DB).GetItemINPrices(_ItemIN);
                FillComboBoxConsumeUnit(_ItemIN._ConsumeUnit);
                FillComboBoxTradeState(_ItemIN._TradeState);
                _Item = _ItemIN._Item;
                LastUsedFolder = _Item.folder;
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxSellCurrency, DB, _Currency);

                LoadItemData(false, false);
                textBoxSellExchangeRate.Text = _Currency.ExchangeRate.ToString();
                labelBuyCost.Text = "تكلفة المفرد من " + _ItemIN._ConsumeUnit.ConsumeUnitName;
                textBoxItemID.ReadOnly = true;
                comboBoxTradestate.Enabled = false;
                textBox_ItemINID.Text = _ItemIN.ItemINID.ToString();
                textBox_IN_Date.Text = _ItemIN.IN_Date .ToString();
                textBoxAmount.Text = _ItemIN.Amount.ToString();
                textBoxBuyPrice.Text = _ItemIN._INCost.Value.ToString();
                panelSellConfig.Enabled = true;

                FillItemConsumeUnitsAndSellPrices("LoadForm", ItemINSellPriceList);
                this.comboBoxSellCurrency.SelectedIndexChanged += new System.EventHandler(this.comboBoxSellCurrency_SelectedIndexChanged);
                menuStrip1 .Enabled  = true;
                if (Edit == null)
                {
   
                    buttonSave.Visible = false;

                    textBoxItemID.ReadOnly = true;
                    textBoxAmount.ReadOnly = true;
                    textBoxBuyPrice.ReadOnly = true;
                    comboBoxConsumeUnt.Enabled = false;
                    comboBoxTradestate.Enabled = false;
                    TextboxNotes.ReadOnly = true;
                }
                else if (Edit == true)
                {
                    buttonSave.Name = "buttonSave";
                    buttonSave.Text = "حفظ";
                    this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
                    this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
                    this.textBoxAmount.TextChanged += new System.EventHandler(this.RefreshTotalCost_TextChanged);
                    this.textBoxBuyPrice.TextChanged += new System.EventHandler(this.RefreshTotalCost_TextChanged);
                    this.dataGridViewSellConfig.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
                    this.dataGridViewSellConfig.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
                    comboBoxTradestate.Enabled = true;
                    this.comboBoxConsumeUnt.SelectedIndexChanged += new System.EventHandler(this.comboBoxConsumeUnt_SelectedIndexChanged);

                }
                else
                {

                    labelItemInfo.Text = "معلومات العنصر";
                    textBoxItemID.ReadOnly = true;
                    buttonSave.Visible = false;



                    textBoxItemID.ReadOnly = true;
                    textBoxAmount.ReadOnly = true;
                    textBoxBuyPrice.ReadOnly = true;
                    comboBoxConsumeUnt.Enabled = false;
                    comboBoxTradestate.Enabled = false;
                    TextboxNotes.ReadOnly = true;


                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadForm:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

           
        }
        private void LoadItemData(bool SetConsumeUnit,bool RefreshSellPrices)
        {
            try
            {
                textBoxItemID.Text = _Item.ItemID.ToString();
                textBoxItemName.Text = _Item.ItemName;
                textBoxItemCompany.Text = _Item.ItemCompany;
                textBoxItemType.Text = _Item.folder.FolderName;
                if (SetConsumeUnit) FillComboBoxConsumeUnit(null);
                ConsumUnitsList = new ConsumeUnitSql(DB).GetConsumeUnitList(_Item);
                if (RefreshSellPrices) FillItemConsumeUnitsAndSellPrices("LoadItemData", ItemINSellPriceList);
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadItemData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
      
        private void FillComboBoxConsumeUnit(ConsumeUnit  consumeunit)
        {
            try
            {
                if (_Item == null) return;
                comboBoxConsumeUnt.Items.Clear();
                int selected_index = 0;
                try
                {

                    List<ConsumeUnit> ConsumeUnitList = new ConsumeUnitSql(DB).GetConsumeUnitList(_Item);
                    for (int i = 0; i < ConsumeUnitList.Count; i++)
                    {
                        string consumeunit_name = "";
                        if (ConsumeUnitList[i].ConsumeUnitID == 0)
                            consumeunit_name = ConsumeUnitList[i].ConsumeUnitName;
                        else
                            consumeunit_name = ConsumeUnitList[i].ConsumeUnitName + " (" + ConsumeUnitList[i].Factor
                                + " " + _Item.DefaultConsumeUnit + ")";

                        ComboboxItem item = new ComboboxItem(consumeunit_name, ConsumeUnitList[i].ConsumeUnitID);
                        comboBoxConsumeUnt.Items.Add(item);
                        if (consumeunit != null && consumeunit.ConsumeUnitID == ConsumeUnitList[i].ConsumeUnitID) selected_index = i;
                    }
                    comboBoxConsumeUnt.SelectedIndex = selected_index;
                    comboBoxConsumeUnt.Enabled = true;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("FillComboBoxConsumeUnit:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBoxConsumeUnit:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
   
        private void FillComboBoxTradeState(TradeState tradestate)
        {
            try
            {
                comboBoxTradestate.Items.Clear();
                int selected_index = 0;

                List<TradeState> TradeStateList = new TradeStateSQL(DB).GetTradeStateList();
                for (int i = 0; i < TradeStateList.Count; i++)
                {
                    ComboboxItem item = new ComboboxItem(TradeStateList[i].TradeStateName, TradeStateList[i].TradeStateID);
                    comboBoxTradestate.Items.Add(item);
                    if (tradestate != null && tradestate.TradeStateID == TradeStateList[i].TradeStateID) selected_index = i;
                }
                comboBoxTradestate.SelectedIndex = selected_index;
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBoxTradeState:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
      
        }
       
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
     
                if (_Item == null) throw new Exception("يرجى تحديد العنصر");
                ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
                ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo( consumeunititem.Value);
                ComboboxItem tradestateitem = (ComboboxItem)comboBoxTradestate.SelectedItem;
                TradeState tradestate = new TradeStateSQL(DB).GetTradeStateBYID(tradestateitem.Value);
                double? cost = null;
                switch (_Operation.OperationType)
                {
                    case Operation.BILL_BUY:
                        cost= Convert.ToDouble(textBoxBuyPrice.Text);
                        break;
                    case Operation.ASSEMBLAGE:
                        cost =null ;
                        break;
                    case Operation.DISASSEMBLAGE:

                        DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_Operation.OperationID);
                        if (DisAssemblabgeOPR_._ItemOUT == null) throw new Exception("لا يمكن ادخال عنصر عن طريق التفكبك قبل اخراج العنثر المفكك");
                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(_Operation);
                        cost = Convert.ToDouble(textBoxBuyPrice.Text);
                        double itemins_cost = iteminlist.Sum(x => x.Amount * x._INCost.Value);

                        if (buttonSave.Name == "buttonAdd")
                        {
                            if (itemins_cost + cost > DisAssemblabgeOPR_._ItemOUT._OUTValue.Value * DisAssemblabgeOPR_._ItemOUT.Amount)
                                throw new Exception(" يجب ان يكون مجموع تكلفة المفرد للعناصر المدخلة  ضرب الكمية اقل او يساوي تكلفة العنصر المفكك ضرب الكمية");
                        }
                        else
                        {

                            if (itemins_cost + cost -_ItemIN._INCost.Value > DisAssemblabgeOPR_._ItemOUT._OUTValue.Value * DisAssemblabgeOPR_._ItemOUT.Amount)
                                throw new Exception(" يجب ان يكون مجموع تكلفة المفرد للعناصر المدخلة  ضرب الكمية اقل او يساوي تكلفة العنصر المفكك ضرب الكمية");

                        }

                        break;
                    default:
                        throw new Exception("عملية ادخال غير صحيحة");
                }
      

                if (buttonSave.Name == "buttonAdd")
                {
                    if (_Item == null)
                    {
                        MessageBox.Show("يرجى تحديد العنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ItemIN itemin = new ItemINSQL(DB).AddItemIN(_Operation, _Item, tradestate
                        , Convert.ToDouble(textBoxAmount.Text), _ConsumeUnit, cost
                        , TextboxNotes.Text);


                    if (itemin != null)
                    {
                        _ItemIN = itemin;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        LoadForm(true);

                    }
                }

                else
                {

                    if (_Operation != null)
                    {
                        if (_Item == null)
                        {
                            MessageBox.Show("يرجى تحديد العنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    
                        bool success = new ItemINSQL(DB).UpdateItemIN(_ItemIN.ItemINID, _Item, tradestate,
                            Convert.ToDouble(textBoxAmount.Text), _ConsumeUnit, cost
                        , TextboxNotes.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _ItemIN = new ItemINSQL(DB).GetItemININFO_BYID(_ItemIN.ItemINID);
                            this.Changed_ = true;
                            LoadForm(true);
                        }
                        else MessageBox.Show("لم يتم الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           
        }
         private async  void RefreshTotalCost_TextChanged(object sender, EventArgs e)
        {
            double amount, buyprice;

            try
            {
                amount = Convert.ToDouble(textBoxAmount.Text);
                buyprice = Convert.ToDouble(textBoxBuyPrice.Text);

                TextboxTotalValue.Text = (amount * buyprice).ToString() + " " + _Currency.CurrencySymbol;
                FillItemConsumeUnitsAndSellPrices("RefreshTotalCost_TextChanged",ItemINSellPriceList  );
            }
            catch { TextboxTotalValue.Text = " -- " + _Currency .CurrencySymbol; }
        }
  
        private void textBoxItemID_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
                {
                    try
                    {
                        if (_ItemIN != null)
                            if (new ItemOUTSQL(DB).GetItemIN_ItemOUTList("textBoxItemID_KeyDown",_ItemIN).Count > 0 || new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(_ItemIN).Count > 0)
                                throw new Exception("يجب الغاء العناصر المخرجة و إلغاء التخزين قبل تعديل العنصر");
                        uint itemid = Convert.ToUInt32(textBoxItemID.Text);
                        Item item__ = new ItemSQL(DB).GetItemInfoByID(itemid);
                        if (item__ != null)
                        {
                            _Item = item__;
                            LoadItemData(true,true );
                        }
                        else
                        {
                            MessageBox.Show("لم يتم العثور على العنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch(Exception ee)
                    {
                        MessageBox.Show("textBoxItemID_KeyDown:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }


        }
        private void textBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.Button == MouseButtons.Left)
                {
                    if (_ItemIN != null)
                        if (new ItemOUTSQL(DB).GetItemIN_ItemOUTList("textBoxItem_MouseDoubleClick",_ItemIN).Count > 0 || new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(_ItemIN).Count > 0)
                            throw new Exception("يجب الغاء العناصر المخرجة و إلغاء التخزين قبل تعديل العنصر");
                    ItemObj.Forms.User_ShowItemsForm SelectItem_ = new ItemObj.Forms.User_ShowItemsForm(DB, LastUsedFolder, ItemObj.Forms.User_ShowItemsForm.SELECT_ITEM);
                    DialogResult dd = SelectItem_.ShowDialog();
                    if (dd == DialogResult.OK)
                    {
                        _Item = SelectItem_.ReturnItem;
                        LoadItemData(true, true);
                    }
                }



            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxItem_MouseDoubleClick:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void AdjustmentDatagridviewColumnsWidth()
        {
            try
            {
                int columnscount = dataGridViewSellConfig.Columns.Count + 1;
                dataGridViewSellConfig.RowHeadersWidth = dataGridViewSellConfig.Width / columnscount;
                dataGridViewSellProfit.RowHeadersWidth = dataGridViewSellConfig.Width / columnscount;
                dataGridViewBuyCost.RowHeadersWidth = dataGridViewSellConfig.Width / columnscount;
                dataGridViewBuyCost.Columns[0].Width = (dataGridViewSellConfig.Width - 5) / columnscount;
                for (int i = 0; i < columnscount - 1; i++)
                {
                    dataGridViewSellConfig.Columns[i].Width = (dataGridViewSellConfig.Width - 5) / columnscount;
                    dataGridViewSellProfit.Columns[i].Width = (dataGridViewSellConfig.Width - 5) / columnscount;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AdjustmentDatagridviewColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    dataGridViewSellConfig.CurrentCell = dataGridViewSellConfig.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    ConsumeUnit Selected_ConsumeUnit = null;
                    for (int i = 0; i < ConsumUnitsList.Count; i++)
                    {
                        if (ConsumUnitsList[i].ConsumeUnitName.Equals(dataGridViewSellConfig.Rows[e.RowIndex].HeaderCell.Value))
                        {
                            Selected_ConsumeUnit = ConsumUnitsList[i];
                            break;
                        }
                    }
                    ComboboxItem tradestate_item = (ComboboxItem)comboBoxTradestate.SelectedItem;
                    TradeState TradeState_ = new TradeState(tradestate_item.Value, tradestate_item.Text);
                    SellType Selected_SellType = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(dataGridViewSellConfig.Columns[e.ColumnIndex].Name));
                    List<ItemINSellPrice> ff = ItemINSellPriceList.Where(x => (x._ItemIN.ItemINID == _ItemIN.ItemINID && x.SellType_.SellTypeID == Selected_SellType.SellTypeID && x.ConsumeUnit_.ConsumeUnitID == Selected_ConsumeUnit.ConsumeUnitID)).ToList();
                    ContextMenu m = new ContextMenu();
                    if (ff.Count > 0)
                    {
                        m.MenuItems.Add(setPriceMenuItem);
                        m.MenuItems.Add(UnsetpriceMenuItem);
                    }
                    else
                    {
                        m.MenuItems.Add(setPriceMenuItem);
                    }
                    int currentMouseOverRow = dataGridViewSellConfig.HitTest(e.X, e.Y).RowIndex;
                    m.Show(dataGridViewSellConfig, new Point(e.X, e.Y));

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("dataGridView1_CellMouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                if (_Item.ItemID != _ItemIN._Item.ItemID) throw new Exception("قد قمت تغير العنصر يجب اولا الحفظ ثم المتابعة");

            ConsumeUnit SelectedConsumeUnit = null;
            for (int i = 0; i < ConsumUnitsList.Count; i++)
            {
                if (ConsumUnitsList[i].ConsumeUnitName.Equals(dataGridViewSellConfig.Rows[e.RowIndex].HeaderCell.Value))
                {
                    SelectedConsumeUnit = ConsumUnitsList[i];
                    break;
                }
            }
            ComboboxItem combobox_SellCurrencyitem = (ComboboxItem)comboBoxSellCurrency.SelectedItem;
            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(combobox_SellCurrencyitem.Value);
            ComboboxItem tradestate_item = (ComboboxItem)comboBoxTradestate.SelectedItem;
            TradeState TradeState_ = new TradeState(tradestate_item.Value, tradestate_item.Text);
           
            SellType SelectedSellType = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(dataGridViewSellConfig.Columns[e.ColumnIndex].Name));
          Trade.Forms.PriceInputBox inp = new Trade.Forms.PriceInputBox("ضبط السعر", SelectedSellType.SellTypeName, TradeState_.TradeStateName , SelectedConsumeUnit.ConsumeUnitName, currency.CurrencyName,""  );
            inp.ShowDialog();
            if (inp.DialogResult == DialogResult.OK)
            {

                    double price = Convert.ToDouble(inp.Price);
                    if (price < 0)
                    {
                        MessageBox.Show("السعر يجب ان يكون اكبر من الصفر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    new ItemINSellPriceSql(DB).SetItemINPrice(_ItemIN, SelectedConsumeUnit, SelectedSellType, price / currency.ExchangeRate );
                    ItemINSellPriceList = new ItemINSellPriceSql(DB).GetItemINPrices(_ItemIN);
                    FillItemConsumeUnitsAndSellPrices("dataGridView1_CellMouseDoubleClick",ItemINSellPriceList);


            }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("dataGridView1_CellMouseDoubleClick:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }
        public void InitialzeDataGridViewItemSellPrices()
        {
            try
            {
                dataGridViewBuyCost.Columns.Add("ConsumeUnitBuCost", "التكلفة حسب وحدة التوزيع");
                dataGridViewBuyCost.EnableHeadersVisualStyles = false;
                dataGridViewBuyCost.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridViewBuyCost.RowHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridViewBuyCost.TopLeftHeaderCell.Style.BackColor = Color.Orange;
                for (int i = 0; i < selltypelist.Count; i++)
                {
                    dataGridViewSellConfig.Columns.Add(selltypelist[i].SellTypeID.ToString(), selltypelist[i].SellTypeName);
                    dataGridViewSellProfit.Columns.Add(selltypelist[i].SellTypeID.ToString(), selltypelist[i].SellTypeName);

                }
                dataGridViewSellConfig.EnableHeadersVisualStyles = false;
                dataGridViewSellConfig.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridViewSellConfig.RowHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridViewSellConfig.TopLeftHeaderCell.Style.BackColor = Color.Orange;
                dataGridViewSellProfit.EnableHeadersVisualStyles = false;
                dataGridViewSellProfit.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridViewSellProfit.RowHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridViewSellProfit.TopLeftHeaderCell.Style.BackColor = Color.Orange;
                AdjustmentDatagridviewColumnsWidth();
            }

            catch(Exception ee)
            {
                MessageBox.Show("InitialzeDataGridViewItemSellPrices:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void comboBoxTradestate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillItemConsumeUnitsAndSellPrices(ItemSellPriceList);
        //}
        private void Unsetprice_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Item.ItemID != _ItemIN._Item.ItemID) throw new Exception("قد قمت تغير العنصر يجب اولا الحفظ ثم المتابعة");
                DialogResult dd = MessageBox.Show("هل انت متاكد من الغاء ضبط السعر", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd == DialogResult.OK)
                {
                    ConsumeUnit Selected_ConsumeUnit = null;
                    for (int i = 0; i < ConsumUnitsList.Count; i++)
                    {
                        if (ConsumUnitsList[i].ConsumeUnitName.Equals(dataGridViewSellConfig.Rows[dataGridViewSellConfig.CurrentCell.RowIndex].HeaderCell.Value))
                        {
                            Selected_ConsumeUnit = ConsumUnitsList[i];
                            break;
                        }
                    }
                    SellType Selected_SellType = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(dataGridViewSellConfig.Columns[dataGridViewSellConfig.CurrentCell.ColumnIndex].Name));
                    bool success = new ItemINSellPriceSql(DB).UNSetBuyOPRPrice(_ItemIN, Selected_ConsumeUnit, Selected_SellType);
                    if (success)
                    {
                        ItemINSellPriceList = new ItemINSellPriceSql(DB).GetItemINPrices(_ItemIN);
                        FillItemConsumeUnitsAndSellPrices("Unsetprice_MenuItem_Click", ItemINSellPriceList);
                    }
                }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("Unsetprice_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }

        }
        private void setprice_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Item.ItemID != _ItemIN._Item.ItemID) throw new Exception("قد قمت تغير العنصر يجب اولا الحفظ ثم المتابعة");

                ConsumeUnit Selected_ConsumeUnit = null;
                for (int i = 0; i < ConsumUnitsList.Count; i++)
                {
                    if (ConsumUnitsList[i].ConsumeUnitName.Equals(dataGridViewSellConfig.Rows[dataGridViewSellConfig.CurrentCell.RowIndex].HeaderCell.Value))
                    {
                        Selected_ConsumeUnit = ConsumUnitsList[i];
                        break;
                    }
                }
                ComboboxItem combobox_SellCurrencyitem = (ComboboxItem)comboBoxSellCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(combobox_SellCurrencyitem.Value);
                ComboboxItem tradestate_item = (ComboboxItem)comboBoxTradestate.SelectedItem;
                TradeState TradeState_ = new TradeState(tradestate_item.Value, tradestate_item.Text);
                SellType Selected_SellType = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(dataGridViewSellConfig.Columns[dataGridViewSellConfig.CurrentCell.ColumnIndex].Name));


                Trade.Forms.PriceInputBox inp = new Trade.Forms.PriceInputBox("ضبط السعر", Selected_SellType.SellTypeName, TradeState_.TradeStateName, Selected_ConsumeUnit.ConsumeUnitName, currency.CurrencyName, "");
                inp.ShowDialog();
                if (inp.DialogResult == DialogResult.OK)
                {

                    double price = Convert.ToDouble(inp.Price);
                    if (price < 0)
                    {
                        MessageBox.Show("السعر يجب ان يكون اكبر من الصفر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    new ItemINSellPriceSql(DB).SetItemINPrice(_ItemIN, Selected_ConsumeUnit, Selected_SellType, price / currency.ExchangeRate);
                    ItemINSellPriceList = new ItemINSellPriceSql(DB).GetItemINPrices(_ItemIN);
                    FillItemConsumeUnitsAndSellPrices("setprice_MenuItem_Click", ItemINSellPriceList);


                }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("setprice_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }
        public async  void FillItemConsumeUnitsAndSellPrices(string source, List<ItemINSellPrice> ItemINSellPriceList)
        {
            try
            {
                if (_ItemIN == null)
                {
                    panelItemINSellPrices.Enabled = false;
                    panelItemINSellsProfit.Enabled = false;
                    return;

                }
                panelItemINSellPrices.Enabled = true;
                panelItemINSellsProfit.Enabled = true;
                
                ComboboxItem combobox_SellCurrencyitem = (ComboboxItem)comboBoxSellCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(combobox_SellCurrencyitem.Value);
                
                ComboboxItem combobox_consumeunit_item = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
                ConsumeUnit ConsumeUnit__;
                if (combobox_consumeunit_item.Value == 0)
                    ConsumeUnit__ = new ConsumeUnit(0, _Item.DefaultConsumeUnit, _Item, 1);
                else
                    ConsumeUnit__ = new ConsumeUnitSql(DB).GetConsumeAmountinfo(combobox_consumeunit_item.Value);
        
                double exchangerate;
                try
                {
                    exchangerate = Convert.ToDouble(textBoxSellExchangeRate.Text);
                }
                catch
                {
                    throw new Exception("معدل الصرف يجب ان يكون رقم حقيقي");
                }
                ComboboxItem comboboxitem = (ComboboxItem)comboBoxTradestate.SelectedItem;
                dataGridViewBuyCost.TopLeftHeaderCell.Value = comboboxitem.Text;
                dataGridViewSellConfig.TopLeftHeaderCell.Value = comboboxitem.Text;
                dataGridViewSellProfit.TopLeftHeaderCell.Value = comboboxitem.Text;

                dataGridViewSellConfig.Rows.Clear();
                dataGridViewSellProfit.Rows.Clear();
                dataGridViewBuyCost.Rows.Clear();
                //if (_Item == null) return;
                double buyprice_=0;
                double buyamount_=0;
                bool values_ok = false;
                try
                {
                    if (_ItemIN ._Operation .OperationType ==Operation .BILL_BUY )
                    {
                        buyprice_ = Convert.ToDouble(textBoxBuyPrice.Text);
                        buyamount_ = Convert.ToDouble(textBoxAmount.Text);
                        values_ok = true;
                    }
                    else
                    {
                        buyprice_ = _ItemIN ._INCost.Value ;
                        buyamount_ = _ItemIN.Amount;
                        values_ok = true;
                    }
                  
                }
                catch
                {
                    values_ok = false;
                }
                
                if (selltypelist.Count != 0)
                {


                    for (int i = 0; i < ConsumUnitsList.Count; i++)
                    {
                        dataGridViewBuyCost.Rows.Add();
                        dataGridViewSellConfig.Rows.Add();
                        dataGridViewSellProfit.Rows.Add();
                       
                        dataGridViewBuyCost.Rows[i].HeaderCell.Value = ConsumUnitsList[i].ConsumeUnitName;

                        if (values_ok )
                        dataGridViewBuyCost.Rows[i].Cells[0].Value = System.Math.Round(buyprice_ *(ConsumUnitsList [i].Factor / ConsumeUnit__.Factor )*(exchangerate / _Currency .ExchangeRate ), 3) + " " + currency.CurrencySymbol.Replace(" ", string.Empty);
                        else
                            dataGridViewBuyCost.Rows[i].Cells[0].Value = "-" + " " + currency.CurrencySymbol.Replace(" ", string.Empty);
                       

                        dataGridViewSellConfig.Rows[i].HeaderCell.Value = ConsumUnitsList[i].ConsumeUnitName;
                        dataGridViewSellProfit.Rows[i].HeaderCell.Value = ConsumUnitsList[i].ConsumeUnitName;
                        
                        for (int j = 0; j < selltypelist.Count; j++)
                        {
                            if(_Item .ItemID==_ItemIN._Item.ItemID)
                            {
                               
                                List<ItemINSellPrice> dd = ItemINSellPriceList.Where(x => (x._ItemIN._Operation.OperationID == _ItemIN._Operation.OperationID && x.SellType_.SellTypeID == selltypelist[j].SellTypeID && x.ConsumeUnit_.ConsumeUnitID == ConsumUnitsList[i].ConsumeUnitID)).ToList();
                                if (dd.Count == 0)
                                {
                                    dataGridViewSellConfig.Rows[i].Cells[j].Value = " - " + " " + currency.CurrencySymbol.Replace(" ", string.Empty);
                                    dataGridViewSellProfit.Rows[i].Cells[j].Value = " - " + " " + currency.CurrencySymbol.Replace(" ", string.Empty);

                                }
                                else
                                {
                                    double sellprice = System.Math.Round(Convert.ToDouble(dd[0].Price) * exchangerate, 3);
                                    dataGridViewSellConfig.Rows[i].Cells[j].Value = sellprice.ToString() + " " + currency.CurrencySymbol.Replace(" ", string.Empty);
                                    if (values_ok)
                                    {
                                        double Buyprice_Factor = buyprice_ * (ConsumUnitsList[i].Factor / ConsumeUnit__.Factor) * (exchangerate / _Currency.ExchangeRate);
                                        double BuyAmount_Factor = buyamount_ * (ConsumeUnit__.Factor / ConsumUnitsList[i].Factor);
                                        double profit = (sellprice - Buyprice_Factor) * BuyAmount_Factor;
                                        dataGridViewSellProfit.Rows[i].Cells[j].Value = System.Math.Round(profit, 3).ToString() + " " + currency.CurrencySymbol.Replace(" ", string.Empty);

                                    }
                                    else
                                    {
                                        dataGridViewSellProfit.Rows[i].Cells[j].Value = " - " + " " + currency.CurrencySymbol.Replace(" ", string.Empty);
                                    }

                                }
                            }
                            else
                            {
                                dataGridViewSellConfig.Rows[i].Cells[j].Value = " - " + " " + currency.CurrencySymbol.Replace(" ", string.Empty);
                                dataGridViewSellProfit.Rows[i].Cells[j].Value = " - " + " " + currency.CurrencySymbol.Replace(" ", string.Empty);
                            }
                           


                            //    double? price = new ItemSellPriceSql(DB).GetPrice(_Item, TradeState_, selltypelist[j], ConsumUnitsList[i]);
                            //    if (price == null) dataGridView1.Rows[i].Cells[j].Value = " - " + " " + _BillOUT._Currency.CurrencySymbol.Replace (" ",string.Empty );

                            //    else dataGridView1.Rows[i].Cells[j].Value = System.Math.Round((Convert.ToDouble(price) * _BillOUT .ExchangeRate), 3).ToString() + " " + _BillOUT._Currency .CurrencySymbol.Replace(" ", string.Empty);
                            //}


                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(" FillItemConsumeUnitsAndSellPrices- SourceMethod:" + source +"//"+ ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    


    }
        private void ToolStripMenuItem_ItemsOUTReport_Click(object sender, EventArgs e)
        {
            try
            {
                ItemIN_ItemOutListForm ItemIN_ItemOutListForm_ = new ItemIN_ItemOutListForm(DB, _ItemIN);
                ItemIN_ItemOutListForm_.ShowDialog();
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("عرض تقرير العناصر الخارجة:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }


        private void comboBoxSellCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem combobox_SellCurrencyitem = (ComboboxItem)comboBoxSellCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(combobox_SellCurrencyitem.Value);
                //if (currency.ReferenceCurrencyID == null)
                //    textBoxSellExchangeRate.ReadOnly = true;
                //else
                //    textBoxSellExchangeRate.ReadOnly = false ;
                textBoxSellExchangeRate.Text = currency.ExchangeRate.ToString();
                FillItemConsumeUnitsAndSellPrices("comboBoxSellCurrency_SelectedIndexChanged", ItemINSellPriceList);
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxSellCurrency_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void comboBoxConsumeUnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem comboBoxConsumeUntitem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
                labelBuyCost.Text = "تكلفة المفرد من " + comboBoxConsumeUntitem.Text;
                if (_ItemIN != null) FillItemConsumeUnitsAndSellPrices("comboBoxConsumeUnt_SelectedIndexChanged", ItemINSellPriceList);
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxConsumeUnt_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void ToolStripMenuItem_StoreConfig__Click(object sender, EventArgs e)
        {
            try
            {if(buttonSave.Visible ==true )
                {
                    ItemIN_Store_Form ItemIN_Store_Form_ = new ItemIN_Store_Form(DB, _ItemIN, ItemIN_Store_Form.STORE_CONFIG_FUNCTION);
                    ItemIN_Store_Form_.ShowDialog();
                }
            else
                {
                    ItemIN_Store_Form ItemIN_Store_Form_ = new ItemIN_Store_Form(DB, _ItemIN, ItemIN_Store_Form.ONLY_READ_FUNCTION );
                    ItemIN_Store_Form_.ShowDialog();
                }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("ItemIN_StoreForm:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}