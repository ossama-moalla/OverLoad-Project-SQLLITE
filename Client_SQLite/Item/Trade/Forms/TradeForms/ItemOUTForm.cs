using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.ItemObj.Forms;
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Trade.Forms.Container;
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
    public partial class ItemOUTForm : Form
    {

        List<SellType> selltypelist = new List<SellType>();
        List<ItemINSellPrice> ItemINSellPriceList = new List<ItemINSellPrice>();
        //List<ConsumeUnit> ConsumUnitsList = new List<ConsumeUnit>();
        DatabaseInterface DB;
        ItemOUT _ItemOUT;
        Operation _Operation;
        Currency _Currency;

        Folder LastUsedFolder;
        private bool Changed_;

        ItemIN _TempItemIN;
        TradeStorePlace _TempPlace;
        public bool Changed
        {
            get { return Changed_; }
        }

        //SellType SelectedSellType;
        //ConsumeUnit SelectedConsumeUnit;

        public ItemOUTForm(DatabaseInterface db, Operation Operation_)
        {
            DB = db;
            InitializeComponent();
            selltypelist = new SellTypeSql(DB).GetSellTypeList();

            InitialzeDataGridViewItemSellPrices();


            _Operation = Operation_;
            LastUsedFolder = null;
            PanelSellInfo .Enabled = false;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            _Currency = new OperationSQL(DB).GetOperationItemOUTCurrency(_Operation);
            


            textBoxExchangeRate.Text = _Currency.ExchangeRate.ToString();
            textBoxCurrency.Text = _Currency.CurrencyName;
            textBoxAmount.Text = "1";
            textBoxItemOUT_ID.Text = "-";
            textBoxItemOUT_OUTDATE.Text = "-";
            textBox_ItemOUT_OperationID.Text = _Operation.OperationID.ToString();

            switch (_Operation .OperationType)
            {

                case Operation.BILL_SELL:
                    BillSell billsell = new TradeSQL.BillSellSQL(DB).GetBillSell_INFO_BYID(_Operation.OperationID);
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(Operation_.OperationType) 
                        + " -نمط البيع:"+ billsell._SellType.SellTypeName;
                    break;

                case Operation.BILL_MAINTENANCE:
                    Maintenance.Objects.BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL (DB).GetBillMaintenance_INFO_BYID(_Operation.OperationID);
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(Operation_.OperationType)
                        + " -نمط البيع:" + BillMaintenance_._SellType.SellTypeName;

                    break;
                case Operation.ASSEMBLAGE:
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(Operation_.OperationType);
                    panelItemINSellPrices.Visible = false;
                    panelCost.Visible = false;
                    break;
                case Operation.DISASSEMBLAGE:
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(Operation_.OperationType);

                    panelItemINSellPrices.Visible = false;
                    panelCost.Visible = false;
                    break;
                case Operation.RavageOPR:
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(Operation_.OperationType);

                    panelItemINSellPrices.Visible = false;
                    panelCost.Visible = false;
                    break;
                case Operation.REPAIROPR:

                    Maintenance.Objects.RepairOPR RepairOPR_ = new Maintenance.MaintenanceSQL.RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(_Operation.OperationID);
                    Maintenance.Objects.BillMaintenance BillMaintenance__ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(RepairOPR_._MaintenanceOPR);
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(Operation_.OperationType) + "- تابع لعملية صيانة رقم:"
                       + RepairOPR_._MaintenanceOPR._Operation.OperationID +" - فاتورة صيانة رقم:"+ BillMaintenance__._Operation.OperationID
                       + " -نمط البيع:" + BillMaintenance__._SellType.SellTypeName;
                    break;
                default:
                    throw new Exception("عملية غير صحيحة");
            }

        }
        public ItemOUTForm(DatabaseInterface db, ItemOUT ItemOUT_, bool Edit)
        {
            DB = db;
            InitializeComponent();
            selltypelist = new SellTypeSql(DB).GetSellTypeList();
            InitialzeDataGridViewItemSellPrices();

            _ItemOUT = ItemOUT_;
            _Operation  = _ItemOUT._Operation  ;

            _Currency = new OperationSQL(DB).GetOperationItemOUTCurrency(_Operation );
            textBoxExchangeRate.Text = _Currency .ExchangeRate.ToString();
            textBoxCurrency.Text = _Currency.CurrencyName;
            _TempItemIN = _ItemOUT._ItemIN;
            _TempPlace = _ItemOUT.Place;
            textBox_ItemOUT_OperationID.Text = _Operation.OperationID.ToString();

            switch (_Operation.OperationType)
            {

                case Operation.BILL_SELL:
                    BillSell billsell = new TradeSQL.BillSellSQL(DB).GetBillSell_INFO_BYID(_Operation.OperationID);
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(_Operation.OperationType)
                        + " -نمط البيع:" + billsell._SellType.SellTypeName;
                    break;

                case Operation.BILL_MAINTENANCE:
                    Maintenance.Objects.BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(_Operation.OperationID);
                    textBox_ItemOUT_OperationType.Text = Operation.GetOperationName(_Operation.OperationType)
                        + " -نمط البيع:" + BillMaintenance_._SellType.SellTypeName;

                    break;
                case Operation.ASSEMBLAGE:
                    LabelOperationInfo.Text = Operation.GetOperationName(_Operation.OperationType);
                    panelItemINSellPrices.Visible = false;
                    panelCost.Visible = false;
                    break;
                case Operation.DISASSEMBLAGE:
                    LabelOperationInfo.Text = Operation.GetOperationName(_Operation.OperationType);

                    panelItemINSellPrices.Visible = false;
                    panelCost.Visible = false;
                    break;
                case Operation.RavageOPR:
                    LabelOperationInfo.Text = Operation.GetOperationName(_Operation.OperationType);

                    panelItemINSellPrices.Visible = false;
                    panelCost.Visible = false;
                    break;
                case Operation.REPAIROPR:

                    Maintenance.Objects.RepairOPR RepairOPR_ = new Maintenance.MaintenanceSQL.RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(_Operation.OperationID);
                    Maintenance.Objects.BillMaintenance BillMaintenance__ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(RepairOPR_._MaintenanceOPR);
                    LabelOperationInfo.Text = Operation.GetOperationName(_Operation.OperationType) + "- تابع لعملية صيانة رقم:"
                       + RepairOPR_._MaintenanceOPR._Operation.OperationID + " - فاتورة صيانة رقم:" + BillMaintenance__._Operation.OperationID
                       + " -نمط البيع:" + BillMaintenance__._SellType.SellTypeName;
                    break;
                default:
                    throw new Exception("عملية غير صحيحة");
            }
            LoadForm(Edit );
            

        }

       
        private void FillComboBoxConsumeUnit(ConsumeUnit consumeunit)
        {
            try
            {

                if (_TempItemIN == null) return;
                List<ConsumeUnit> ConsumUnitsList = new ConsumeUnitSql(DB).GetConsumeUnitList(_TempItemIN._Item);
                comboBoxConsumeUnt.Items.Clear();
                int selected_index = 0;

                for (int i = 0; i < ConsumUnitsList.Count; i++)
                {
                    ComboboxItem item;
                    if (ConsumUnitsList[i].ConsumeUnitID == 0)
                        item = new ComboboxItem(ConsumUnitsList[i].ConsumeUnitName, ConsumUnitsList[i].ConsumeUnitID);
                    else
                        item = new ComboboxItem(ConsumUnitsList[i].ConsumeUnitName + " (" + ConsumUnitsList[i].Factor + " " +
                           _TempItemIN._Item.DefaultConsumeUnit + ")", ConsumUnitsList[i].ConsumeUnitID);

                    comboBoxConsumeUnt.Items.Add(item);
                    if (consumeunit != null && consumeunit.ConsumeUnitID == ConsumUnitsList[i].ConsumeUnitID) selected_index = i;
                }
                comboBoxConsumeUnt.SelectedIndex = selected_index;
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBoxConsumeUnit:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
    

        }
       
        public void LoadItemIN_Data(bool FillConsumeUnit)
        {
            try
            {
                if (_TempItemIN == null) return;
                PanelSellInfo.Enabled = true;
                if(FillConsumeUnit) FillComboBoxConsumeUnit(null);
                LastUsedFolder = _TempItemIN._Item.folder;
                textBoxItemState.Text = _TempItemIN._TradeState.TradeStateName;
                textBoxItemID.Text = _TempItemIN._Item.ItemID.ToString();
                textBoxItemName.Text = _TempItemIN._Item.ItemName;
                textBoxItemCompany.Text = _TempItemIN._Item.ItemCompany;
                textBoxItemType.Text = _TempItemIN._Item.folder.FolderName;

                textBox_ItemIN_OperationID.Text = _TempItemIN._Operation.OperationID.ToString();
                textBox_ItemIN_OperationType.Text =Operation .GetOperationName( _TempItemIN._Operation.OperationType);
                textBoxItemINID.Text = _TempItemIN.ItemINID.ToString();
                if (_TempPlace != null)
                    textBoxStorePlace.Text = _TempPlace.GetPlaceInfo();
                else
                    textBoxStorePlace.Text = "-";

                textBoxAvailableAmount.Text = new AvailableItemSQL(DB).Get_AvailabeAmount_by_Place(_TempItemIN, _TempPlace).ToString();
                textBoxItemIN_ConsumeUnit.Text = _TempItemIN._ConsumeUnit.ConsumeUnitName;
                FillComboBoxConsumeUnit(null);

                ItemINSellPriceList = new ItemINSellPriceSql(DB).GetItemINPrices(_TempItemIN );

                FillItemConsumeUnitsAndSellPrices(ItemINSellPriceList);


                if (_Operation.OperationType == Operation.ASSEMBLAGE || _Operation.OperationType == Operation.DISASSEMBLAGE)
                {
                    Currency tempcur = new OperationSQL(DB).GetOperationItemINCurrency(_TempItemIN._Operation);
                    Currency targcur = new OperationSQL(DB).GetOperationItemOUTCurrency(_Operation);
                    textBoxCost.Text = (_TempItemIN._INCost.Value * (targcur.ExchangeRate / tempcur.ExchangeRate)).ToString();
                }
                else if (_Operation.OperationType == Operation.BILL_SELL || _Operation.OperationType == Operation.BILL_MAINTENANCE
                   || _Operation.OperationType == Operation.REPAIROPR)

                {
                    SellType SellType_ = null; double? sellprice;
                    switch (_Operation.OperationType)
                    {
                        case Operation. BILL_SELL:
                            BillSell billsell = new BillSellSQL(DB).GetBillSell_INFO_BYID(_Operation.OperationID);
                             SellType_ = billsell._SellType;
                            sellprice= new ItemINSellPriceSql(DB).GetPrice(_TempItemIN, SellType_, null);
                            if (sellprice != null) textBoxCost.Text  =Math .Round ( Convert.ToDouble(sellprice)*billsell.ExchangeRate,3).ToString();
                            else textBoxCost.Text  = "السعر غير مضبوط";
                            break;

                        case Operation.BILL_MAINTENANCE:
                            Maintenance.Objects.BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL (DB).GetBillMaintenance_INFO_BYID(_Operation.OperationID);
                             SellType_ = BillMaintenance_._SellType;
                             sellprice = new ItemINSellPriceSql(DB).GetPrice(_TempItemIN, SellType_, null);
                            if (sellprice != null) textBoxCost.Text = Math.Round(Convert.ToDouble(sellprice) * BillMaintenance_.ExchangeRate, 3).ToString();
                            else textBoxCost.Text = "السعر غير مضبوط";
                            break;
                        case Operation.ASSEMBLAGE:
                            break;
                        case Operation.DISASSEMBLAGE:
                            break;
                        case Operation.RavageOPR:
                            textBoxCost.Text = "0";
                            break;
                        case Operation.REPAIROPR:
                            Maintenance.Objects.RepairOPR RepairOPR_ = new Maintenance.MaintenanceSQL.RepairOPRSQL (DB).Get_RepairOPR_INFO_BYID(_Operation.OperationID);
                            Maintenance.Objects.BillMaintenance BillMaintenance__ =
                                new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(RepairOPR_._MaintenanceOPR);
                            SellType_ = BillMaintenance__._SellType;
                            sellprice = new ItemINSellPriceSql(DB).GetPrice(_TempItemIN, SellType_, null);
                            if (sellprice != null) textBoxCost.Text = Math.Round(Convert.ToDouble(sellprice) * BillMaintenance__.ExchangeRate, 3).ToString();
                            else textBoxCost.Text = "السعر غير مضبوط";
                            break;
                        default:
                            throw new Exception("عملي اخراج غير صحيحة");
                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadItemIN_Data:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        public async  void InitialzeDataGridViewItemSellPrices()
        {
            try
            {
                for (int i = 0; i < selltypelist.Count; i++)
                {
                    dataGridView1.Columns.Add(selltypelist[i].SellTypeID.ToString(), selltypelist[i].SellTypeName);

                }
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView1.TopLeftHeaderCell.Style.BackColor = Color.Orange;
                AdjustmentDatagridviewColumnsWidth();
            }

            catch
            {
                MessageBox.Show("فشل في جلب قائمة انماط البيع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void AdjustmentDatagridviewColumnsWidth()
        {
            try
            {

                int columnscount = dataGridView1.Columns.Count + 1;
                dataGridView1.RowHeadersWidth = dataGridView1.Width / columnscount;
                for (int i = 0; i < columnscount - 1; i++)
                {
                    dataGridView1.Columns[i].Width = (dataGridView1.Width - 5) / columnscount;
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("AdjustmentDatagridviewColumnsWidth:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        public void FillItemConsumeUnitsAndSellPrices(List<ItemINSellPrice> ItemINSellPriceList)
        {
            //if (_TempItemIN  == null)
            //{
            //    panelItemINSellPrices.Enabled = false;
            //    return;

            //}
            //panelItemINSellPrices.Enabled = true;
            //dataGridView1.TopLeftHeaderCell.Value = _TempItemIN._TradeState .TradeStateName;

            //dataGridView1.Rows.Clear();;
            ////if (_Item == null) return;
            //if (selltypelist.Count != 0)
            //{


            //    for (int i = 0; i < ConsumUnitsList.Count; i++)
            //    {
            //        dataGridView1.Rows.Add();
            //        dataGridView1.Rows[i].HeaderCell.Value = ConsumUnitsList[i].ConsumeUnitName;
            //        for (int j = 0; j < selltypelist.Count; j++)
            //        {
            //            try
            //            {

            //                List<ItemINSellPrice> dd = ItemINSellPriceList.Where(x => (x._ItemIN._Operation.OperationID == _TempItemIN ._Operation.OperationID && x.SellType_.SellTypeID == selltypelist[j].SellTypeID && x.ConsumeUnit_.ConsumeUnitID == ConsumUnitsList[i].ConsumeUnitID)).ToList();
            //                if (dd.Count != 1) dataGridView1.Rows[i].Cells[j].Value = " - " + " " + _Currency.CurrencySymbol.Replace(" ", string.Empty);
            //                else
            //                {
            //                    double sellprice = System.Math.Round(Convert.ToDouble(dd[0].Price) * _Currency.ExchangeRate, 3);
            //                    dataGridView1.Rows[i].Cells[j].Value = sellprice.ToString() + " " + _Currency.CurrencySymbol.Replace(" ", string.Empty);

            //                }
            //            }
            //            catch (Exception ee)
            //            {
            //                MessageBox.Show(" gg " + ee.Message);
            //            }
            //        }
            //        //    double? price = new ItemSellPriceSql(DB).GetPrice(_Item, TradeState_, selltypelist[j], ConsumUnitsList[i]);
            //        //    if (price == null) dataGridView1.Rows[i].Cells[j].Value = " - " + " " + _BillOUT._Currency.CurrencySymbol.Replace (" ",string.Empty );

            //        //    else dataGridView1.Rows[i].Cells[j].Value = System.Math.Round((Convert.ToDouble(price) * _BillOUT .ExchangeRate), 3).ToString() + " " + _BillOUT._Currency .CurrencySymbol.Replace(" ", string.Empty);
            //        //}


            //    }
            //}

        }
        public void LoadForm(bool Edit)
        {
            try
            {

                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";

                PanelSellInfo.Enabled = true;

                if (Edit)
                {


                    dataGridView1.Enabled = true;
                    comboBoxConsumeUnt.Enabled = true;
                    buttonAllAvailableItems.Enabled = true;
                    buttonAvailableItemsINFolder.Enabled = true;
                    buttonAvailableItemsINPlace.Enabled = true;
                    textBoxAmount.ReadOnly = false;
                    //textBoxCost.ReadOnly = false;
                    TextboxNotes.ReadOnly = false;
                    //this.comboBoxSellType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSellType_SelectedIndexChanged);
                    this.textBoxCost.TextChanged += new System.EventHandler(this.textBoxSellPrice_TextChanged);
                    this.textBoxAmount.TextChanged += new System.EventHandler(this.textBoxSellPrice_TextChanged);
                    //this.comboBoxConsumeUnt.SelectedIndexChanged += new System.EventHandler(this.comboBoxConsumeUnt_SelectedIndexChanged_1);

                }
                else
                {
                    dataGridView1.Enabled = false;
                    comboBoxConsumeUnt.Enabled = false;
                    buttonAllAvailableItems.Enabled = false;
                    buttonAvailableItemsINFolder.Enabled = false;
                    buttonAvailableItemsINPlace.Enabled = false;
                    textBoxAmount.ReadOnly = true;
                    //textBoxCost.ReadOnly = true;
                    TextboxNotes.ReadOnly = true;
                }
                ItemINSellPriceList = new ItemINSellPriceSql(DB).GetItemINPrices(_TempItemIN);

                LoadItemIN_Data(false);
                textBoxItemOUT_ID.Text = _ItemOUT.ItemOUTID.ToString();
                textBoxItemOUT_OUTDATE.Text = _ItemOUT.OUT_Date.ToString();
                FillComboBoxConsumeUnit(_ItemOUT._ConsumeUnit);
                linkLabelShowBuyOPR.Visible = true;
                textBoxAmount.Text = _ItemOUT.Amount.ToString();
                textBoxCost.Text = _ItemOUT._OUTValue.Value.ToString();


            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadForm:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (_TempItemIN == null)
                {
                    MessageBox.Show("يرجى تحديد المادة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                double amount;
                double? cost;

                if (_Operation.OperationType == Operation.BILL_MAINTENANCE
                    || _Operation.OperationType == Operation.REPAIROPR || _Operation.OperationType == Operation.BILL_SELL)
                {
                    try
                    {
                        cost = Convert.ToDouble(textBoxCost.Text);
                    }
                    catch
                    {
                        MessageBox.Show("السعر يجب ان يكون رقم حقيقي ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                }
                else
                {
                    cost = null;
                }
                try
                {
                    amount = Convert.ToDouble(textBoxAmount.Text);
                }
                catch
                {
                    MessageBox.Show("الكمية يجب ان يكون رقم حقيقي ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
                ConsumeUnit _ConsumeUnit;
                if (consumeunititem.Value == 0)
                    _ConsumeUnit = null;
                else
                    _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

                //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
                //string  SellType_ =  ComboboxItem_selltype.Text;

                if (buttonSave.Name == "buttonAdd")
                {
                    try
                    {

                        TradeState tradestate = _TempItemIN._TradeState;
                        ItemOUT ItemOUT_ = new ItemOUTSQL(DB).AddItemOUT(_Operation, _TempItemIN, _TempPlace
                            , amount, _ConsumeUnit, cost
                            , TextboxNotes.Text);
                        if (ItemOUT_ != null)
                        {
                            _ItemOUT = ItemOUT_;
                            MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Changed_ = true;
                            this.Close();

                        }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(":تعذر اخراج العنصر " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                else
                {
                    try
                    {
                        if (_ItemOUT != null)
                        {

                            bool success = new ItemOUTSQL(DB).UpdateItemOUT(_ItemOUT.ItemOUTID, _TempItemIN, _TempPlace
                                , amount, _ConsumeUnit, cost
                            , TextboxNotes.Text);
                            if (success == true)
                            {
                                MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _ItemOUT = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(_ItemOUT.ItemOUTID);
                                this.Changed_ = true;
                                this.Close();
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
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private void buttonAllAvailableItems_Click(object sender, EventArgs e)
        {
            try
            {

                ItemObj.Forms.ShowAvailableItemSimpleForm ShowAvailableItemSimpleForm_ = new ItemObj.Forms.ShowAvailableItemSimpleForm(DB, true);
                DialogResult d1 = ShowAvailableItemSimpleForm_.ShowDialog();
                if (d1 == DialogResult.OK)
                {
                    Item item_ = ShowAvailableItemSimpleForm_.ReturnItem;
                    List<ItemIN_AvailableAmount_Report> AvailbeItems_ItemINList = new AvailableItemSQL(DB).Get_ItemIN_AvailableAmount_Report_List ()
                        .Where (x=>x.ItemID ==item_.ItemID).ToList ();
                    ItemIN ItemIN_;

                    if (AvailbeItems_ItemINList.Count == 1)
                        ItemIN_ = new ItemINSQL (DB).GetItemININFO_BYID ( AvailbeItems_ItemINList[0].ItemINID) ;
                    else
                    {
                        ItemObj.Forms.AvailableItem_ItemIN_Form AvailableItem_ItemINS_Form = new ItemObj.Forms.AvailableItem_ItemIN_Form(DB, item_, true);
                        DialogResult d2 = AvailableItem_ItemINS_Form.ShowDialog();
                        if (d2 == DialogResult.OK)
                        {
                            ItemIN_ = AvailableItem_ItemINS_Form.ReturnItemIN;
                        }
                        else ItemIN_ = null;
                        AvailableItem_ItemINS_Form.Dispose();
                    }

                    if (ItemIN_ != null)
                    {

                        if (_Operation.OperationType == Operation.ASSEMBLAGE)
                        {
                            AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_Operation.OperationID);
                            List<ItemIN> itemin = new ItemINSQL(DB).GetItemINList(AssemblabgeOPR_._Operation);
                            if (itemin.Count > 0)
                            {
                                if (itemin[0].ItemINID == ItemIN_.ItemINID)
                                {
                                    MessageBox.Show("العنصر المساهم بالتجميع يجب ان لا يكون هو نفسه العنصر المجمع ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        TradeStorePlace place = null;
                        List<ItemIN_StoreReport> iteminstore_placelist = new ItemINSQL(DB).GetItemIN_StoreReportList(ItemIN_);
                        DialogResult d3;

                        if (iteminstore_placelist.Count == 1)
                        {
                            place = iteminstore_placelist[0].Place;
                            d3 = DialogResult.OK;
                        }
                        else
                        {
                            ItemIN_StoreReport_Form ItemIN_StoreReport_Form_ = new ItemIN_StoreReport_Form(DB, ItemIN_);
                            DialogResult dd = ItemIN_StoreReport_Form_.ShowDialog();
                            if (dd == DialogResult.OK)
                            {
                                place = ItemIN_StoreReport_Form_.ReturnPlace;
                                d3 = DialogResult.OK;
                            }
                            else
                                d3 = DialogResult.Cancel;


                            ItemIN_StoreReport_Form_.Dispose();
                        }
                        if (d3 == DialogResult.OK)
                        {

                            _TempPlace = place;
                            _TempItemIN = ItemIN_;

                            LoadItemIN_Data(true);

                        }


                    }


                }
                ShowAvailableItemSimpleForm_.Dispose();

            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonAllAvailableItems_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void buttonAvailableItemsINFolder_Click(object sender, EventArgs e)
        {
            try
            {

                ItemObj.Forms.User_AvailabeItemsForm AvailabeItemsForm_ = new ItemObj.Forms.User_AvailabeItemsForm(DB, LastUsedFolder, true);
                DialogResult d1 = AvailabeItemsForm_.ShowDialog();
                if (d1 == DialogResult.OK)
                {
                    Item item_ = AvailabeItemsForm_.ReturnItem;
                    List<ItemIN_AvailableAmount_Report> AvailbeItems_ItemINList = new AvailableItemSQL(DB).Get_ItemIN_AvailableAmount_Report_List ()
                        .Where (x=>x.ItemID  ==item_.ItemID).ToList ();
                    ItemIN ItemIN_;
                    if (AvailbeItems_ItemINList.Count == 1)
                        ItemIN_ = new ItemINSQL (DB).GetItemININFO_BYID ( AvailbeItems_ItemINList[0].ItemINID) ;
                    else
                    {
                        ItemObj.Forms.AvailableItem_ItemIN_Form AvailableItem_ItemINS_Form = new ItemObj.Forms.AvailableItem_ItemIN_Form(DB, item_, true);
                        DialogResult d2 = AvailableItem_ItemINS_Form.ShowDialog();
                        if (d2 == DialogResult.OK)
                        {
                            ItemIN_ = AvailableItem_ItemINS_Form.ReturnItemIN;
                        }
                        else ItemIN_ = null;
                        AvailableItem_ItemINS_Form.Dispose();
                    }

                    if (ItemIN_ != null)
                    {

                        if (_Operation.OperationType == Operation.ASSEMBLAGE)
                        {
                            AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_Operation.OperationID);
                            List<ItemIN> itemin = new ItemINSQL(DB).GetItemINList(AssemblabgeOPR_._Operation);
                            if (itemin.Count > 0)
                            {
                                if (itemin[0].ItemINID == ItemIN_.ItemINID)
                                {
                                    MessageBox.Show("العنصر المساهم بالتجميع يجب ان لا يكون هو نفسه العنصر المجمع ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        //if (_Operation.OperationType == Operation.DISASSEMBLAGE)
                        //{
                        //    DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_Operation.OperationID);
                        //    if (DisAssemblabgeOPR_._ItemOUT._ItemIN.ItemINID == ItemIN_.ItemINID)
                        //    {
                        //        MessageBox.Show("العنصر الناتج عن التفكيك يجب ان لا يكون هو نفسه العنصر المفكك ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        return;
                        //    }
                        //}
                        TradeStorePlace place = null;
                        List<ItemIN_StoreReport> iteminstore_placelist = new ItemINSQL(DB).GetItemIN_StoreReportList(ItemIN_);
                        DialogResult d3;
                        if (iteminstore_placelist.Count == 1)
                        {
                            place = iteminstore_placelist[0].Place;
                            d3 = DialogResult.OK;
                        }
                        else
                        {
                            ItemIN_StoreReport_Form ItemIN_StoreReport_Form_ = new ItemIN_StoreReport_Form(DB, ItemIN_);
                            if (ItemIN_StoreReport_Form_.ShowDialog() == DialogResult.OK)
                            {
                                place = ItemIN_StoreReport_Form_.ReturnPlace;
                                d3 = DialogResult.OK;
                            }
                            else
                                d3 = DialogResult.Cancel;


                            ItemIN_StoreReport_Form_.Dispose();
                        }
                        if (d3 == DialogResult.OK)
                        {

                            _TempPlace = place;
                            _TempItemIN = ItemIN_;

                            LoadItemIN_Data(true);

                        }

                    }


                }
                AvailabeItemsForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonAvailableItemsINFolder_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void buttonAvailableItemsINPlace_Click(object sender, EventArgs e)
        {
            try
            {
                User_ShowLocationsForm showlocations = new User_ShowLocationsForm(DB, null, User_ShowLocationsForm.SELECT_Place);
                DialogResult d1 = showlocations.ShowDialog();
                if (d1 == DialogResult.OK)
                {
                    TradeStorePlace Place = showlocations.ReturnPlace;
                    showlocations.Dispose();
                    Container.Place_ExistsItems_Form PlaceItemsForm_ = new Container.Place_ExistsItems_Form(DB, Place, true);
                    DialogResult d2 = PlaceItemsForm_.ShowDialog();
                    if (d2 == DialogResult.OK)
                    {
                        Item item = PlaceItemsForm_.ReturnItem;
                        PlaceItemsForm_.Dispose();
                        ItemIN ItemIN_;
                        List<TradeStoreItems_AvailableAmount_Report> StoredItems = new TradeItemStoreSQL(DB).GetItemsStoredINPlace_BY_Item(Place, item);
                        List<TradeStoreItems_AvailableAmount_Report> StoredItems_ItemIN = StoredItems.Where(x => x._TradeItemStore.StoreType == TradeItemStore.ITEMIN_STORE_TYPE).ToList();

                        if (StoredItems_ItemIN.Count == 1)
                        {
                            ItemIN_ = StoredItems_ItemIN[0]._TradeItemStore._ItemIN;
                        }
                        else
                        {
                            Container.Place_ExitsItems_Item_ItemIN_Form PlaceItemBuyOprForm_ = new Container.Place_ExitsItems_Item_ItemIN_Form(DB, Place, item, true);
                            DialogResult d3 = PlaceItemBuyOprForm_.ShowDialog();
                            if (d3 == DialogResult.OK)
                            {
                                if (_Operation.OperationType == Operation.ASSEMBLAGE)
                                {
                                    AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(_Operation.OperationID);
                                    List<ItemIN> itemin = new ItemINSQL(DB).GetItemINList(AssemblabgeOPR_._Operation);
                                    if (itemin.Count > 0)
                                    {
                                        if (itemin[0].ItemINID == PlaceItemBuyOprForm_.ReturnItemIN.ItemINID)
                                        {
                                            MessageBox.Show("العنصر المساهم بالتجميع يجب ان لا يكون هو نفسه العنصر المجمع ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                }

                                ItemIN_ = PlaceItemBuyOprForm_.ReturnItemIN;

                            }
                            else ItemIN_ = null;

                            PlaceItemBuyOprForm_.Dispose();

                        }
                        if (ItemIN_ != null)
                        {
                            _TempItemIN = ItemIN_;
                            _TempPlace = Place;

                            LoadItemIN_Data(true);
                        }
                    }


                    PlaceItemsForm_.Dispose();
                }
                showlocations.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
          
        }
    

        private void textBoxSellPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double amount = Convert.ToDouble(textBoxAmount .Text );
                double sellprice = Convert.ToDouble(textBoxCost .Text);
                TextboxTotalValue.Text = (amount * sellprice).ToString()+_Currency .CurrencySymbol ;

            }
            catch
            {
                TextboxTotalValue.Text = "-";
            }
        }

        private void linkLabelShowBuyOPR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                ItemINForm BuyOprForm_ = new ItemINForm(DB, _TempItemIN, false);
                BuyOprForm_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show("linkLabelShowBuyOPR_LinkClicked:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            AdjustmentDatagridviewColumnsWidth();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    ConsumeUnit SelectedConsumeUnit = null;
            //    for (int i = 0; i < ConsumUnitsList.Count; i++)
            //    {
            //        if (ConsumUnitsList[i].ConsumeUnitName.Equals(dataGridView1.Rows[e.RowIndex].HeaderCell.Value))
            //        {
            //            SelectedConsumeUnit = ConsumUnitsList[i];
            //            break;
            //        }
            //    }
            //    FillComboBoxConsumeUnit(SelectedConsumeUnit);
            //    TradeState TradeState_ = _TempItemIN._TradeState ;
            //    SellType SelectedSellType = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(dataGridView1.Columns[e.ColumnIndex].Name));


            //    double? price_ = new ItemINSellPriceSql(DB).GetPrice(_TempItemIN, SelectedSellType, SelectedConsumeUnit);
            //    if (price_ == null) MessageBox.Show("السعر غير مضبوط", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    else
            //    {
            //        double price = Convert.ToDouble(price_);
            //        price = price * Convert.ToDouble(textBoxExchangeRate .Text );
                    
            //        textBoxCost.Text =System.Math .Round ( price,2).ToString();
            //    }
            //}catch
            //{
               
            //}
        }

        private void comboBoxConsumeUnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try{

                ConsumeUnit ConsumeUnit_;
                ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
                if (ComboboxItem_.Value == 0)
                    ConsumeUnit_ = null;
                else
                    ConsumeUnit_ = new ConsumeUnitSql(DB).GetConsumeAmountinfo(ComboboxItem_.Value);
                SellType SellType_ = null; double? sellprice;
                switch (_Operation.OperationType)
                {
                    case Operation.BILL_SELL:
                        BillSell billsell = new BillSellSQL(DB).GetBillSell_INFO_BYID(_Operation.OperationID);
                        SellType_ = billsell._SellType;
                        sellprice = new ItemINSellPriceSql(DB).GetPrice(_TempItemIN, SellType_, ConsumeUnit_);
                        if (sellprice != null)
                        {
                            textBoxCost.Text = System.Math.Round((Convert.ToDouble(sellprice) * billsell.ExchangeRate), 3).ToString();
                        }
                        else textBoxCost.Text = "السعر غير مضبوط";
                        break;

                    case Operation.BILL_MAINTENANCE:
                        Maintenance.Objects.BillMaintenance BillMaintenance_ = new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(_Operation.OperationID);
                        SellType_ = BillMaintenance_._SellType;
                        sellprice = new ItemINSellPriceSql(DB).GetPrice(_TempItemIN, SellType_, ConsumeUnit_);
                        if (sellprice != null)
                        {
                            textBoxCost.Text = System.Math.Round((Convert.ToDouble(sellprice) * BillMaintenance_.ExchangeRate), 3).ToString();
                        }
                        else textBoxCost.Text = "السعر غير مضبوط";
                        break;
                    case Operation.ASSEMBLAGE:

                        break;
                    case Operation.DISASSEMBLAGE:
                        break;
                    case Operation.RavageOPR:
                        textBoxCost.Text = "0";
                        break;
                    case Operation.REPAIROPR:
                        Maintenance.Objects.RepairOPR RepairOPR_ = new Maintenance.MaintenanceSQL.RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(_Operation.OperationID);
                        Maintenance.Objects.BillMaintenance BillMaintenance__ =
                            new Maintenance.MaintenanceSQL.BillMaintenanceSQL(DB).GetBillMaintenance_By_MaintenaceOPR(RepairOPR_._MaintenanceOPR);
                        SellType_ = BillMaintenance__._SellType;
                        sellprice = new ItemINSellPriceSql(DB).GetPrice(_TempItemIN, SellType_, ConsumeUnit_);
                        if (sellprice != null)
                        {
                            textBoxCost.Text = System.Math.Round((Convert.ToDouble(sellprice) * BillMaintenance__.ExchangeRate), 3).ToString();
                        }
                        else textBoxCost.Text = "السعر غير مضبوط";
                        break;
                    default:
                        throw new Exception("عملية اخراج غير صحيحة");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxConsumeUnt_SelectedIndexChanged:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
