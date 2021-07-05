using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Forms;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
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
    public partial class BillMaintenanceForm : OverLoad_Form   
    {
       
        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        BillMaintenance _BillMaintenance;
        
        System.Windows.Forms.MenuItem OpenPayIN_MenuItem;
        System.Windows.Forms.MenuItem AddPayIN_MenuItem;
        System.Windows.Forms.MenuItem EditPayIN_MenuItem;
        System.Windows.Forms.MenuItem DeletePayIN_MenuItem;

        System.Windows.Forms.MenuItem OpenRepairOPR_MenuItem;
        System.Windows.Forms.MenuItem EditRepairOPR_MenuItem;
        System.Windows.Forms.MenuItem SetRepairOPRClauseValue_MenuItem;
        System.Windows.Forms.MenuItem UNSetRepairOPRClauseValue_MenuItem;

        System.Windows.Forms.MenuItem OpenDiagnosticOPR_MenuItem;
        System.Windows.Forms.MenuItem EditDiagnosticOPR_MenuItem;
        System.Windows.Forms.MenuItem SetDiagnosticOPRClauseValue_MenuItem;
        System.Windows.Forms.MenuItem UNSetDiagnosticOPRClauseValue_MenuItem;

        System.Windows.Forms.MenuItem OpenItemOut_MenuItem;
        System.Windows.Forms.MenuItem AddItemOut_MenuItem;
        System.Windows.Forms.MenuItem EditItemOut_MenuItem;
        System.Windows.Forms.MenuItem DeleteItemOut_MenuItem;

        System.Windows.Forms.MenuItem OpenAdditionalClause_MenuItem;
        System.Windows.Forms.MenuItem AddAdditionalClause_MenuItem;
        System.Windows.Forms.MenuItem EditAdditionalClause_MenuItem;
        System.Windows.Forms.MenuItem DeleteAdditionalClause_MenuItem;

        //public bool Changed { get; set; } = false;
        double BillValue_;
        double PaysValue;

        public BillMaintenanceForm(DatabaseInterface db,DateTime BillDate_,MaintenanceOPR MaintenanceOPR_)
        {
            DB = db;

            InitializeComponent();

           ProgramGeneralMethods.FillComboBoxSellType (ref comboBoxSellType,DB, null);
         
            BillValue_ = 0;
            _MaintenanceOPR = MaintenanceOPR_;
            textBoxBillMaintenanceExchangeRate.Enabled = true;
            comboBoxBillMaintenanceCurrency.Enabled = true;

            LoadMaintenanceOPRData();
            dateTimePicker_.Value = BillDate_;
            Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxBillMaintenanceCurrency, DB, defaultcurrency);
            textBoxBillMaintenanceExchangeRate.Text = defaultcurrency.ExchangeRate.ToString();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text  = "انشاء فاتورة";
            textBoxDiscount.Enabled = false;
            panelListViews.Enabled = false;
            InitializeMenuItems();


        }
        public BillMaintenanceForm(DatabaseInterface db, BillMaintenance BillMaintenance_, bool edit)
        {
            InitializeComponent();
            DB = db;
            ProgramGeneralMethods.FillComboBoxSellType(ref comboBoxSellType, DB, BillMaintenance_._SellType );

            _MaintenanceOPR = BillMaintenance_._MaintenanceOPR;
            _BillMaintenance = BillMaintenance_;
            InitializeMenuItems();
            LoadMaintenanceOPRData();
            loadForm(edit );


        }
        private async void LoadMaintenanceOPRData()
        {
            try
            {
                textBoxItemID.Text = _MaintenanceOPR._Item.ItemID.ToString();
                textBoxItemName.Text = _MaintenanceOPR._Item.ItemName;
                textBoxItemCompany.Text = _MaintenanceOPR._Item.ItemCompany;
                textBoxItemType.Text = _MaintenanceOPR._Item.folder.FolderName;
                textBoxContact.Text = _MaintenanceOPR._Contact.Get_Complete_ContactName_WithHeader();
                textBoxMaintenenaceOPRID.Text = _MaintenanceOPR._Operation.OperationID.ToString();
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadMaintenanceOPRData:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           

        }
        public void InitializeMenuItems()
        {
            OpenRepairOPR_MenuItem = new MenuItem("فتح", OpenRepairOPR_MenuItem_Click); 
            EditRepairOPR_MenuItem = new MenuItem("تعديل", EditRepairOPR_MenuItem_Click); 
            SetRepairOPRClauseValue_MenuItem = new MenuItem("ضبط القيمة", SetRepairOPRClauseValue_MenuItem_Click); ;
            UNSetRepairOPRClauseValue_MenuItem = new MenuItem("الغاء ضبط القيمة", UNSetRepairOPRClauseValue_MenuItem_Click); ;

            OpenDiagnosticOPR_MenuItem  = new MenuItem("فتح", OpenDiagnosticOPR_MenuItem_Click);
            EditDiagnosticOPR_MenuItem = new MenuItem("تعديل", EditDiagnosticOPR_MenuItem_Click);
            SetDiagnosticOPRClauseValue_MenuItem = new MenuItem("ضبط القيمة", SetDiagnosticOPRClauseValue_MenuItem_Click); ;
            UNSetDiagnosticOPRClauseValue_MenuItem = new MenuItem("الغاء ضبط القيمة", UNSetDiagnosticOPRClauseValue_MenuItem_Click); ;

            OpenItemOut_MenuItem = new MenuItem("فتح", OpenItemOUT_MenuItem_Click); ;
            AddItemOut_MenuItem = new MenuItem("اضافة عنصر مركب", AddItemOUT_MenuItem_Click); ;
             EditItemOut_MenuItem = new MenuItem("تعديل", EditItemOUT_MenuItem_Click); ;
             DeleteItemOut_MenuItem = new MenuItem("حذف", DeleteItemOUT_MenuItem_Click); ;

            OpenAdditionalClause_MenuItem  = new MenuItem("فتح", OpenAdditionalClause_MenuItem_Click); ;
            AddAdditionalClause_MenuItem = new MenuItem("انشاء بند اضافي", AddAdditionalClause_MenuItem_Click); ;
            EditAdditionalClause_MenuItem = new MenuItem("تعديل", EditAdditionalClause_MenuItem_Click); ;
            DeleteAdditionalClause_MenuItem = new MenuItem("حذف", DeleteAdditionalClause_MenuItem_Click); ;

            OpenPayIN_MenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية الدفع", OpenPayIN_MenuItem_Click);

            AddPayIN_MenuItem = new MenuItem("اضافة دفعة تابعة لهذه الفاتورة", AddPayIN_MenuItem_Click);
            EditPayIN_MenuItem = new MenuItem("تعديل", EditPayIN_MenuItem_Click);
            DeletePayIN_MenuItem = new MenuItem("حذف", DeletePayIN_MenuItem_Click);

        }
        private void OpenPayIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (listViewPays.SelectedItems.Count > 0)
                {
                    uint PayINid = Convert.ToUInt32(listViewPays.SelectedItems[0].Name);
                    PayIN PayIN_ = new PayINSQL(DB).GetPayIN_INFO_BYID(PayINid);
                    AccountingObj.Forms.PayINForm PayINForm_ = new AccountingObj.Forms.PayINForm(DB, PayIN_, false);
                    PayINForm_.Show ();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenPayIN_MenuItem_Click:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void DeletePayIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint PayINid = Convert.ToUInt32(listViewPays.SelectedItems[0].Name);
                bool success = new PayINSQL(DB).Delete_PayIN(PayINid);
                if (success)
                {
                    FillBillMaintenancePays();
                    this .Refresh_ListViewMoneyDataDetails_Flag    = true ;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeletePayIN_MenuItem_Click:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       
        }

        private void EditPayIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewPays.SelectedItems.Count > 0)
                {
                    uint PayINid = Convert.ToUInt32(listViewPays.SelectedItems[0].Name);
                    PayIN PayIN_ = new PayINSQL(DB).GetPayIN_INFO_BYID(PayINid);
                    AccountingObj.Forms.PayINForm PayINForm_ = new AccountingObj.Forms.PayINForm(DB, PayIN_, true);
                    PayINForm_.ShowDialog();
                    if (PayINForm_.Refresh_ListViewMoneyDataDetails_Flag )
                    {
                        this.Refresh_ListViewMoneyDataDetails_Flag  = true;

                        FillBillMaintenancePays();

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditPayIN_MenuItem_Click:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

       
        }

        private void AddPayIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PayINForm PayINForm_ = new PayINForm(DB, DateTime.Now, _BillMaintenance);
                PayINForm_.ShowDialog();
                if (PayINForm_.DialogResult == DialogResult.OK)
                {
                    this.Refresh_ListViewMoneyDataDetails_Flag  = true;

                    FillBillMaintenancePays();
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("AddPayIN_MenuItem_Click:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
        }

        private async  void RefreshBillClauses()
        {
            try
            {
                listViewItemsOut.Items.Clear();
                listViewDiagnostic.Items.Clear();
                listViewRepair.Items.Clear();
                listViewAdditionalClauses.Items.Clear();

                ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxBillMaintenanceCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);

                double itemsoutvalue = 0, diagnosticclausevalue = 0, repairclausevalue = 0, additionalclausesvalue = 0;
                //double billvalue = 0;
                List<BillMaintenance_Clause> ClausesList = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(_BillMaintenance);
                List<BillMaintenance_Clause> ClausesList_Notnull = ClausesList.Where(x => x.Value != null).ToList();
                List<BillMaintenance_Clause> ClausesList_null = ClausesList.Where(x => x.Value == null).ToList();
                for (int i = 0; i < ClausesList_Notnull.Count; i++)
                {

                    if (ClausesList_Notnull[i].ClauseType == BillMaintenance_Clause.ITEMOUT_TYPE)
                    {
                        ListViewItem ListViewItem_ItemOut_ = new ListViewItem((listViewItemsOut.Items.Count + 1).ToString());

                        ListViewItem_ItemOut_.Name = ClausesList_Notnull[i]._ItemOUT.ItemOUTID.ToString();
                        ListViewItem_ItemOut_.SubItems.Add(ClausesList_Notnull[i]._ItemOUT._ItemIN._Item.folder.FolderName);
                        ListViewItem_ItemOut_.SubItems.Add(ClausesList_Notnull[i]._ItemOUT._ItemIN._Item.ItemName);
                        ListViewItem_ItemOut_.SubItems.Add(ClausesList_Notnull[i]._ItemOUT._ItemIN._Item.ItemCompany);

                        ListViewItem_ItemOut_.SubItems.Add(ClausesList_Notnull[i]._ItemOUT._ItemIN._TradeState.TradeStateName);
                        ListViewItem_ItemOut_.SubItems.Add(ClausesList_Notnull[i]._ItemOUT.Amount.ToString());

                        ListViewItem_ItemOut_.SubItems.Add(ClausesList_Notnull[i]._ItemOUT._ConsumeUnit.ConsumeUnitName.ToString());
                        ListViewItem_ItemOut_.SubItems.Add(ClausesList_Notnull[i]._ItemOUT._OUTValue.Value.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol);
                        ListViewItem_ItemOut_.SubItems.Add((ClausesList_Notnull[i]._ItemOUT.Amount * ClausesList_Notnull[i]._ItemOUT._OUTValue.Value).ToString() + " " + _BillMaintenance._Currency.CurrencySymbol);
                       
                       if (ClausesList_Notnull[i]._ItemOUT._Operation.OperationType == Operation.REPAIROPR)
                                ListViewItem_ItemOut_.SubItems.Add("مركب ضمن عملية اصلاح  رقم:" + ClausesList_Notnull[i]._ItemOUT._Operation.OperationID);             
                        else
                            ListViewItem_ItemOut_.SubItems.Add("");

                        ListViewItem_ItemOut_.BackColor  = Color.LimeGreen;
                        listViewItemsOut.Items.Add(ListViewItem_ItemOut_);
                        itemsoutvalue += Convert.ToDouble(ClausesList_Notnull[i].Value);
                    }
                    else if (ClausesList_Notnull[i].ClauseType == BillMaintenance_Clause.AdditionalClause_TYPE)
                    {
                        ListViewItem ListViewItem_AdditionalClause = new ListViewItem((listViewAdditionalClauses.Items.Count + 1).ToString());

                        ListViewItem_AdditionalClause.Name = ClausesList_Notnull[i]._BillAdditionalClause.ClauseID.ToString();
                        ListViewItem_AdditionalClause.SubItems.Add(ClausesList_Notnull[i]._BillAdditionalClause.ClauseID.ToString());

                        ListViewItem_AdditionalClause.SubItems.Add(ClausesList_Notnull[i]._BillAdditionalClause.Description);
                        ListViewItem_AdditionalClause.SubItems.Add(ClausesList_Notnull[i].Value.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol);


                        ListViewItem_AdditionalClause.BackColor = Color.LimeGreen;
                        listViewAdditionalClauses .Items.Add(ListViewItem_AdditionalClause);
                        additionalclausesvalue += Convert.ToDouble(ClausesList_Notnull[i].Value);
                    }
                    else if (ClausesList_Notnull[i].ClauseType == BillMaintenance_Clause.DIAGNOSTIC_OPR_TYPE)
                    {
                        ListViewItem ListViewItemDiagnostic = new ListViewItem((listViewDiagnostic.Items.Count + 1).ToString());

                        ListViewItemDiagnostic.Name = "V" + ClausesList_Notnull[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
                        ListViewItemDiagnostic.SubItems.Add(ClausesList_Notnull[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
                        ListViewItemDiagnostic.SubItems.Add(ClausesList_Notnull[i].Value.ToString());
                        ListViewItemDiagnostic.SubItems.Add(ClausesList_Notnull[i]._DiagnosticOPR.Desc);
                        if (ClausesList_Notnull[i]._DiagnosticOPR._Item != null)
                            ListViewItemDiagnostic.SubItems.Add(ClausesList_Notnull[i]._DiagnosticOPR._Item.GetItemFullName());
                        else
                            ListViewItemDiagnostic.SubItems.Add("-----");
                        if (ClausesList_Notnull[i]._DiagnosticOPR.Normal != null)
                        {
                            if (ClausesList_Notnull[i]._DiagnosticOPR.Normal == true)
                                ListViewItemDiagnostic.SubItems.Add("سليم");
                            else
                                ListViewItemDiagnostic.SubItems.Add("توجد مشكلة");

                        }
                        else
                            ListViewItemDiagnostic.SubItems.Add("-----");

                        ListViewItemDiagnostic.SubItems.Add(ClausesList_Notnull[i]._DiagnosticOPR.Report);


                        ListViewItemDiagnostic.BackColor = Color.LimeGreen;
                        listViewDiagnostic.Items.Add(ListViewItemDiagnostic);
                        diagnosticclausevalue += Convert.ToDouble(ClausesList_Notnull[i].Value);
                    }
                    else
                    {
                        ListViewItem ListViewItemRepair = new ListViewItem((listViewRepair.Items.Count + 1).ToString());

                        ListViewItemRepair.Name = "V" + ClausesList_Notnull[i]._RepairOPR._Operation.OperationID.ToString();
                        ListViewItemRepair.SubItems.Add(ClausesList_Notnull[i]._RepairOPR._Operation.OperationID.ToString());

                        ListViewItemRepair.SubItems.Add(ClausesList_Notnull[i].Value.ToString());
                        ListViewItemRepair.SubItems.Add(ClausesList_Notnull[i]._RepairOPR.RepairDesc);
                        //ListViewItemRepair.SubItems.Add(ClausesList_Notnull[i]._RepairOPR._MaintenanceFault.FaultDesc );


                        //if (ClausesList_Notnull[i]._RepairOPR. == true)
                        //    ListViewItemRepair.SubItems.Add("تم الاصلاح");
                        //else
                        //    ListViewItemRepair.SubItems.Add("لم يتم الاصلاح");
                        ListViewItemRepair.BackColor = Color.LimeGreen;
                        listViewRepair.Items.Add(ListViewItemRepair);
                        repairclausevalue += Convert.ToDouble(ClausesList_Notnull[i].Value);
                    }


                }
                for (int i = 0; i < ClausesList_null.Count; i++)
                {

                    if (ClausesList_null[i].ClauseType == BillMaintenance_Clause.DIAGNOSTIC_OPR_TYPE)
                    {
                        ListViewItem ListViewItemDiagnostic = new ListViewItem("-");

                        ListViewItemDiagnostic.Name = "N" + ClausesList_null[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
                        ListViewItemDiagnostic.SubItems.Add(ClausesList_null[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
                        ListViewItemDiagnostic.SubItems.Add("-");
                        ListViewItemDiagnostic.SubItems.Add(ClausesList_null[i]._DiagnosticOPR.Desc);
                        if (ClausesList_null[i]._DiagnosticOPR._Item != null)
                            ListViewItemDiagnostic.SubItems.Add(ClausesList_null[i]._DiagnosticOPR._Item.GetItemFullName());
                        else
                            ListViewItemDiagnostic.SubItems.Add("-----");
                        if (ClausesList_null[i]._DiagnosticOPR.Normal != null)
                        {
                            if (ClausesList_null[i]._DiagnosticOPR.Normal == true)
                                ListViewItemDiagnostic.SubItems.Add("سليم");
                            else
                                ListViewItemDiagnostic.SubItems.Add("توجد مشكلة");

                        }
                        else
                            ListViewItemDiagnostic.SubItems.Add("-----");

                        ListViewItemDiagnostic.SubItems.Add(ClausesList_null[i]._DiagnosticOPR.Report);


                        ListViewItemDiagnostic.BackColor = Color.LightGray;
                        listViewDiagnostic.Items.Add(ListViewItemDiagnostic);
                    }
                    else
                    {
                        ListViewItem ListViewItemRepair = new ListViewItem("-");

                        ListViewItemRepair.Name = "N" + ClausesList_null[i]._RepairOPR._Operation.OperationID.ToString();
                        ListViewItemRepair.SubItems.Add(ClausesList_null[i]._RepairOPR._Operation.OperationID.ToString());

                        ListViewItemRepair.SubItems.Add("-");
                        ListViewItemRepair.SubItems.Add(ClausesList_null[i]._RepairOPR.RepairDesc);
                        //ListViewItemRepair.SubItems.Add(ClausesList_null[i]._RepairOPR._MaintenanceFault.FaultDesc);


                        //if (ClausesList_null[i]._RepairOPR.Normal == true)
                        //    ListViewItemRepair.SubItems.Add("تم الاصلاح");
                        //else
                        //    ListViewItemRepair.SubItems.Add("لم يتم الاصلاح");
                        ListViewItemRepair.BackColor = Color.LightGray;
                        listViewRepair.Items.Add(ListViewItemRepair);
                    }


                }
                textBoxItemsOUT_Value.Text = itemsoutvalue.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                textBoxRepairOPR_Value.Text = repairclausevalue.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                textBoxDiagnosticOPR_Value.Text = diagnosticclausevalue.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                textBoxAdditionalClause_Value.Text = additionalclausesvalue.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                BillValue_ = itemsoutvalue + diagnosticclausevalue + repairclausevalue + additionalclausesvalue;
                if (BillValue_ > 0)
                {
                    double discount;
                    try
                    {
                        discount = Convert.ToDouble(textBoxDiscount.Text);
                        textBoxValue.Text = BillValue_.ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                        textBoxDiscount.Enabled = true;
                        textBoxDiscount.Text = _BillMaintenance.Discount.ToString();
                        textBoxClearValue.Text = (BillValue_ - _BillMaintenance.Discount).ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                        textBoxPays.Text = (PaysValue).ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                        textBoxRemain.Text = (BillValue_ - discount - PaysValue).ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
                    }
                    catch
                    {
                        textBoxClearValue.Text = "-";
                        textBoxRemain.Text = "-";

                    }

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshBillClauses:" + ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void loadForm(bool edit)
        {

            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            textBoxDiscount.Enabled = false;
            panelListViews.Enabled = true;
            if (_BillMaintenance != null)
            {
                //_MaintenanceOPRList = new MaintenanceOPRSQL(DB).GetMaintenanceOPRList(_BillMaintenance);
                //_MaintenanceOPR = _BillMaintenance._MaintenanceOPR;
                //textBoxMaintenanceOPR.Text = _BillMaintenance._MaintenanceOPR.Get_Complete_MaintenanceOPRName_WithHeader();


                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxBillMaintenanceCurrency, DB, _BillMaintenance ._Currency);

                textBoxBillMaintenanceExchangeRate.Text = _BillMaintenance.ExchangeRate.ToString();
     

                labelBillID.Text = "فاتورة صيانة رقم:" + _BillMaintenance._Operation.OperationID   .ToString();
                FillBillMaintenancePays();
                RefreshBillClauses();

                if (edit)
                {

                    dateTimePicker_.Enabled = true;
                    textBoxBillMaintenanceExchangeRate.ReadOnly = false;
                    comboBoxBillMaintenanceCurrency.Enabled = true;
                    TextboxNotes.ReadOnly = false;

                    textBoxDiscount.ReadOnly = false;
                    buttonSave.Visible = true;

                    this.listViewItemsOut .MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewItemsOut_MouseDoubleClick);
                    this.listViewItemsOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewItemsOut_MouseDown);

                    this.listViewRepair.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewRepairOPR_MouseDoubleClick);
                    this.listViewRepair.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewRepairOPR_MouseDown);

                    this.listViewDiagnostic.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDiagnosticOPR_MouseDoubleClick);
                    this.listViewDiagnostic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewDiagnosticOPR_MouseDown);

                    this.listViewAdditionalClauses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAdditionalClauses_MouseDoubleClick);
                    this.listViewAdditionalClauses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewAdditionalClauses_MouseDown);
                    this.listViewPays.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewPays_MouseDoubleClick);
                    this.listViewPays.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewPays_MouseDown);

                }
                else
                {
                    textBoxDiscount.ReadOnly = true ;
                    buttonSave.Visible = false ;
                    dateTimePicker_.Enabled = false;
                    textBoxBillMaintenanceExchangeRate.ReadOnly = true;
                    comboBoxBillMaintenanceCurrency.Enabled = false;
                    TextboxNotes.ReadOnly = true;

                }
                listViewPays.Enabled = true;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem item = (ComboboxItem)comboBoxBillMaintenanceCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(item.Value);

                ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
                SellType SellType_ = new SellTypeSql(DB).GetSellTypeinfo(ComboboxItem_selltype.Value);
                if (_MaintenanceOPR == null)
                {
                    MessageBox.Show("لم يتم تحميل بيانات عملية الصيانة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (buttonSave.Name == "buttonAdd")
                {

                    double exchangerate;
                    try
                    {
                        exchangerate = Convert.ToDouble(textBoxBillMaintenanceExchangeRate.Text);
                    }
                    catch
                    {
                        MessageBox.Show("سعر الصرف يجب ان يكون رقم حقيقي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    BillMaintenance billMaintenance = new BillMaintenanceSQL(DB).AddBillMaintenance(_MaintenanceOPR._Operation.OperationID, dateTimePicker_.Value
                       , SellType_, currency, exchangerate, 0, TextboxNotes.Text);
                    if (billMaintenance != null)
                    {
                        _BillMaintenance = billMaintenance;

                        MessageBox.Show("تم انشاء الفاتورة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMaintenanceOPRs_Flag  = true;

                        loadForm(true);

                    }
                }

                else
                {
                    if (_BillMaintenance != null)
                    {
                        double exchangerate;
                        try
                        {
                            exchangerate = Convert.ToDouble(textBoxBillMaintenanceExchangeRate.Text);
                        }
                        catch
                        {
                            MessageBox.Show("سعر الصرف يجب ان يكون رقم حقيقي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        double discount;
                        try
                        {

                            discount = Convert.ToDouble(textBoxDiscount.Text);
                            if (discount > BillValue_)
                            {
                                MessageBox.Show("الخصم يجب ان يكون اقل من قيمة الفاتورة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("الخصم يجب ان يكون رقم حقيقي", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        bool success = new BillMaintenanceSQL(DB).UpdateBillMaintenance(_BillMaintenance._Operation.OperationID
                            , dateTimePicker_.Value, SellType_, currency, exchangerate, discount, TextboxNotes.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ الفاتورة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _BillMaintenance = new BillMaintenanceSQL(DB).GetBillMaintenance_INFO_BYID(_BillMaintenance._Operation.OperationID);
                            this.Refresh_ListViewMaintenanceOPRs_Flag = true;
                            loadForm(true);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        #region Itemout
        private void DeleteItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint itemoutid = Convert.ToUInt32(listViewItemsOut.SelectedItems[0].Name);
                bool success = new ItemOUTSQL(DB).DeleteItemOUT(itemoutid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshBillClauses();

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
        private void OpenItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint itemoutid = Convert.ToUInt32(listViewItemsOut.SelectedItems[0].Name);
                ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                Trade.Forms.TradeForms.ItemOUTForm ItemOUTForm_ = new Trade.Forms.TradeForms.ItemOUTForm(DB, ItemOUT_, false);
                ItemOUTForm_.ShowDialog();
                if (ItemOUTForm_.Changed)
                {
                    RefreshBillClauses();
                }
                ItemOUTForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void EditItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint itemoutid = Convert.ToUInt32(listViewItemsOut.SelectedItems[0].Name);
                ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                Trade.Forms.TradeForms.ItemOUTForm ItemOUTForm_ = new Trade.Forms.TradeForms.ItemOUTForm(DB, ItemOUT_, true);
                ItemOUTForm_.ShowDialog();
                if (ItemOUTForm_.Changed)
                {
                    RefreshBillClauses();
                }
                ItemOUTForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void AddItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Trade.Forms.TradeForms.ItemOUTForm ItemOUTForm_ = new Trade.Forms.TradeForms.ItemOUTForm(DB, _BillMaintenance._Operation);
                ItemOUTForm_.ShowDialog();
                if (ItemOUTForm_.Changed)
                {
                    RefreshBillClauses();
                }
                ItemOUTForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void listViewItemsOut_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewItemsOut .SelectedItems.Count > 0)
            {
                OpenItemOut_MenuItem .PerformClick();
            }
        }
        private void listViewItemsOut_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewItemsOut.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewItemsOut.Items)
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
                        List<MenuItem> mi1 = new List<MenuItem>();

                        mi1.AddRange(new MenuItem[] { OpenItemOut_MenuItem  ,EditItemOut_MenuItem,DeleteItemOut_MenuItem
                        ,new MenuItem ("-"),AddItemOut_MenuItem });

                        listViewItemsOut.ContextMenu = new ContextMenu(mi1.ToArray());


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddItemOut_MenuItem };
                        listViewItemsOut.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemsOut_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        #endregion
        #region RepairOPR
        private void UNSetRepairOPRClauseValue_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint repairoprid = Convert.ToUInt32(listViewRepair.SelectedItems[0].Name.Substring(1));
                bool success = new BillMaintenanceSQL(DB).BillMaintenance_UNSet_RepairOPR_Clause_Value(_BillMaintenance._Operation.OperationID, repairoprid);
                if (success)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;
                    RefreshBillClauses();
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("UNSetRepairOPRClauseValue_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetRepairOPRClauseValue_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint repairoprid = Convert.ToUInt32(listViewRepair .SelectedItems[0].Name.Substring(1));
                OverLoad_Client.Forms.InputBox inp_box = new OverLoad_Client.Forms.InputBox("ضبط قيمة البند", "أدخل القيمة");
                inp_box.ShowDialog();
                if (inp_box.DialogResult == DialogResult.OK)
                {
                    double value;
                    try
                    {
                        value = Convert.ToDouble(inp_box.textBox1.Text);
                    }
                    catch
                    {
                       throw new Exception ("القيمة يجب ان تكون رقم حقيقي");

                    }
                    bool success = new BillMaintenanceSQL(DB).BillMaintenance_Set_RepairOPR_Clause_Value(_BillMaintenance._Operation.OperationID, repairoprid, value);
                    if (success)
                    {
                        this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                        RefreshBillClauses();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("SetRepairOPRClauseValue_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenRepairOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                uint repairoprid = Convert.ToUInt32(listViewRepair .SelectedItems[0].Name.Substring(1));
                RepairOPR RepairOPR_ = new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(repairoprid);
                RepairOPRForm RepairOPRForm_ = new RepairOPRForm(DB, RepairOPR_, false);
                RepairOPRForm_.ShowDialog();
                if (RepairOPRForm_.Changed)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();

                }
                RepairOPRForm_.Dispose();

            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenRepairOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditRepairOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                uint repairoprid = Convert.ToUInt32(listViewRepair.SelectedItems[0].Name.Substring(1));
                RepairOPR RepairOPR_ = new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(repairoprid);
                RepairOPRForm RepairOPRForm_ = new RepairOPRForm(DB, RepairOPR_, true );
                RepairOPRForm_.ShowDialog();
                if (RepairOPRForm_.Changed)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();

                }
                RepairOPRForm_.Dispose();

            }
            catch (Exception ee)
            {
                MessageBox.Show("EditRepairOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewRepairOPR_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewRepair .SelectedItems.Count > 0)
            {
                SetRepairOPRClauseValue_MenuItem .PerformClick();
            }
        }
        private void listViewRepairOPR_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewRepair.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewRepair.Items)
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
                        List<MenuItem> mi1 = new List<MenuItem>();

                        string valueset = listitem.Name.Substring(0, 1);
                        if (valueset == "V")
                        {
                            mi1.AddRange(new MenuItem[] { SetRepairOPRClauseValue_MenuItem , UNSetRepairOPRClauseValue_MenuItem
                            ,new MenuItem ("-"),OpenRepairOPR_MenuItem,EditRepairOPR_MenuItem});
                        }
                        else
                        {
                            mi1.AddRange(new MenuItem[] { SetRepairOPRClauseValue_MenuItem
                            ,new MenuItem ("-"),OpenRepairOPR_MenuItem,EditRepairOPR_MenuItem});
                        }
                        listViewRepair.ContextMenu = new ContextMenu(mi1.ToArray());


                    }
                    else
                    {

                        listViewRepair.ContextMenu = null;

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewRepairOPR_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        #endregion
        #region DiagnosticOPR
        private void UNSetDiagnosticOPRClauseValue_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint DiagnosticOPRid = Convert.ToUInt32(listViewDiagnostic.SelectedItems[0].Name.Substring(1));
                bool success = new BillMaintenanceSQL(DB).BillMaintenance_UNSet_DiagnosticOPR_Clause_Value(_BillMaintenance._Operation.OperationID, DiagnosticOPRid);
                if (success)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("UNSetDiagnosticOPRClauseValue_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetDiagnosticOPRClauseValue_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint DiagnosticOPRid = Convert.ToUInt32(listViewDiagnostic.SelectedItems[0].Name.Substring(1));

                OverLoad_Client.Forms.InputBox inp_box = new OverLoad_Client.Forms.InputBox("ضبط قيمة البند", "أدخل القيمة");
                inp_box.ShowDialog();
                if (inp_box.DialogResult == DialogResult.OK)
                {
                    double value;
                    try
                    {
                        value = Convert.ToDouble(inp_box.textBox1.Text);

                    }
                    catch
                    {
                        MessageBox.Show("القيمة يجب ان تكون رقم حقيقي", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    bool success = new BillMaintenanceSQL(DB).BillMaintenance_Set_DiagnosticOPR_Clause_Value(_BillMaintenance._Operation.OperationID, DiagnosticOPRid, value);
                    if (success)
                    {
                        this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                        RefreshBillClauses();
                    }

                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("SetDiagnosticOPRClauseValue_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenDiagnosticOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint DiagnosticOPRid = Convert.ToUInt32(listViewDiagnostic.SelectedItems[0].Name.Substring(1));
                DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID (DiagnosticOPRid);
                DiagnosticOPRForm DiagnosticOPRForm_ = new DiagnosticOPRForm(DB, DiagnosticOPR_, false);
                DiagnosticOPRForm_.ShowDialog();
                if (DiagnosticOPRForm_.Changed)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();

                }
                DiagnosticOPRForm_.Dispose();

            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditDiagnosticOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                uint DiagnosticOPRid = Convert.ToUInt32(listViewDiagnostic.SelectedItems[0].Name.Substring(1));
                DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID (DiagnosticOPRid);
                DiagnosticOPRForm DiagnosticOPRForm_ = new DiagnosticOPRForm(DB, DiagnosticOPR_, true);
                DiagnosticOPRForm_.ShowDialog();
                if (DiagnosticOPRForm_.Changed)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();

                }
                DiagnosticOPRForm_.Dispose();

            }
            catch (Exception ee)
            {
                MessageBox.Show("EditDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listViewDiagnosticOPR_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewDiagnostic.SelectedItems.Count > 0)
            {
                SetDiagnosticOPRClauseValue_MenuItem.PerformClick();
            }
        }
        private void listViewDiagnosticOPR_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewDiagnostic.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewDiagnostic.Items)
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
                        List<MenuItem> mi1 = new List<MenuItem>();

                        string valueset = listitem.Name.Substring(0, 1);
                        if (valueset == "V")
                        {
                            mi1.AddRange(new MenuItem[] { SetDiagnosticOPRClauseValue_MenuItem , UNSetDiagnosticOPRClauseValue_MenuItem
                            ,new MenuItem ("-"),OpenDiagnosticOPR_MenuItem,EditDiagnosticOPR_MenuItem});
                        }
                        else
                        {
                            mi1.AddRange(new MenuItem[] { SetDiagnosticOPRClauseValue_MenuItem
                            ,new MenuItem ("-"),OpenDiagnosticOPR_MenuItem,EditDiagnosticOPR_MenuItem});
                        }
                        listViewDiagnostic.ContextMenu = new ContextMenu(mi1.ToArray());


                    }
                    else
                    {

                        listViewDiagnostic.ContextMenu = null;

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewDiagnosticOPR_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }
        #endregion
        #region AdditionalClause
        private void DeleteAdditionalClause_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint AdditionalClauseid = Convert.ToUInt32(listViewAdditionalClauses.SelectedItems[0].Name);
                bool success = new BillAdditionalClauseSQL(DB).DeleteBillAdditionalClause(AdditionalClauseid);
                if (success)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshBillClauses();

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteAdditionalClause_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
          
        }
        private void OpenAdditionalClause_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint AdditionalClauseid = Convert.ToUInt32(listViewAdditionalClauses.SelectedItems[0].Name);
                BillAdditionalClause AdditionalClause_ = new BillAdditionalClauseSQL(DB).Get_BillAdditionalClause_INFO_BYID(AdditionalClauseid);
                Trade.Forms.TradeForms.BillAdditionalClauseForm AdditionalClauseForm_ = new Trade.Forms.TradeForms.BillAdditionalClauseForm(DB, AdditionalClause_, false);
                DialogResult dd = AdditionalClauseForm_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();
                }
                AdditionalClauseForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenAdditionalClause_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void EditAdditionalClause_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint AdditionalClauseid = Convert.ToUInt32(listViewAdditionalClauses.SelectedItems[0].Name);
                BillAdditionalClause AdditionalClause_ = new BillAdditionalClauseSQL(DB).Get_BillAdditionalClause_INFO_BYID(AdditionalClauseid);
                Trade.Forms.TradeForms.BillAdditionalClauseForm AdditionalClauseForm_ = new Trade.Forms.TradeForms.BillAdditionalClauseForm(DB, AdditionalClause_, false);
                DialogResult dd = AdditionalClauseForm_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();
                }
                AdditionalClauseForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditAdditionalClause_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void AddAdditionalClause_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Trade.Forms.TradeForms.BillAdditionalClauseForm AdditionalClauseForm_ = new Trade.Forms.TradeForms.BillAdditionalClauseForm(DB, _BillMaintenance._Operation);
                DialogResult dd = AdditionalClauseForm_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    this.Refresh_ListViewMaintenanceOPRs_Flag = true;

                    RefreshBillClauses();
                }
                AdditionalClauseForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddAdditionalClause_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void listViewAdditionalClauses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewAdditionalClauses.SelectedItems.Count > 0)
            {
                OpenAdditionalClause_MenuItem.PerformClick();
            }
        }
        private void listViewAdditionalClauses_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewAdditionalClauses.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewAdditionalClauses.Items)
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
                        List<MenuItem> mi1 = new List<MenuItem>();

                        mi1.AddRange(new MenuItem[] { OpenAdditionalClause_MenuItem , EditAdditionalClause_MenuItem, DeleteAdditionalClause_MenuItem
                        ,new MenuItem ("-"),AddAdditionalClause_MenuItem});

                        listViewAdditionalClauses.ContextMenu = new ContextMenu(mi1.ToArray());


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddAdditionalClause_MenuItem };
                        listViewAdditionalClauses.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewAdditionalClauses_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        #endregion
        

        private void textBoxDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = Convert.ToDouble(textBoxDiscount.Text);
                textBoxClearValue.Text = (BillValue_ - discount).ToString() + " " + _BillMaintenance._Currency.CurrencySymbol;
            }
            catch
            {
                textBoxClearValue.Text = " - " + _BillMaintenance._Currency.CurrencySymbol;
            }

        }
        private void comboBoxBillMaintenanceCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxBillMaintenanceCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);
                textBoxBillMaintenanceExchangeRate.Text = currency.ExchangeRate.ToString();
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxBillMaintenanceCurrency_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void FillBillMaintenancePays()
        {
            try
            {
                listViewPays.Items.Clear();
                if (_BillMaintenance  == null)
                {
                    textBoxPays.Text = "-";
                    return;
                }

                List<PayIN> Bill_Pays = new List<PayIN>();
                Bill_Pays = new PayINSQL(DB).GetPayINList(_BillMaintenance._Operation);
                textBoxPays.Text = GetPaysReport(Bill_Pays);
                for (int i = 0; i < Bill_Pays.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(Bill_Pays[i].PayOprID.ToString());
                    ListViewItem_.Name = Bill_Pays[i].PayOprID.ToString();
                    ListViewItem_.SubItems.Add(Bill_Pays[i].PayOprDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(Bill_Pays[i].PayDescription);
                    ListViewItem_.SubItems.Add(Bill_Pays[i].Value.ToString());
                    ListViewItem_.SubItems.Add(Bill_Pays[i]._Currency.CurrencyName);
                    ListViewItem_.SubItems.Add(Bill_Pays[i].ExchangeRate.ToString());
                    ListViewItem_.BackColor = Color.Orange;
                    listViewPays.Items.Add(ListViewItem_);
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("FillBillMaintenancePays:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private string GetPaysReport(List<PayIN > bill_Pays)
        {
            try
            {
                PaysValue = 0;
                if (bill_Pays.Count == 0)
                {
                    PaysValue = 0; return "-";
                }
                string pays = "";
                try
                {
                    List<Currency> currencyList = bill_Pays.Select(x => x._Currency).Distinct().ToList();

                    for (int i = 0; i < currencyList.Count; i++)
                    {

                        List<PayIN> temppaysList = bill_Pays.Where(x => x._Currency == currencyList[i]).ToList();
                        for (int j = 0; j < temppaysList.Count; j++)
                        {
                            double factor;
                            if (temppaysList[j]._Currency.CurrencyID != _BillMaintenance._Currency.CurrencyID)
                                factor = _BillMaintenance._Currency.ExchangeRate / temppaysList[j]._Currency.ExchangeRate;
                            else
                                factor = 1;
                            PaysValue = PaysValue + temppaysList[j].Value * factor;
                            pays = System.Math.Round((temppaysList[j].Value), 3) + currencyList[i].CurrencySymbol + " ";
                        }
                    }

                }
                catch
                {

                }
                return pays;
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetPaysReport:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-";
            }
          
        }

        private void listViewPays_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenPayIN_MenuItem.PerformClick();

        }

        private void listViewPays_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewPays.Columns.Count; i++)
                listViewPays.Columns[i].Width = listViewPays.Width / 3;
        }

        private void listViewPays_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewPays.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewPays.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenPayIN_MenuItem, EditPayIN_MenuItem, DeletePayIN_MenuItem, new MenuItem("-"), AddPayIN_MenuItem };
                        listViewPays.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddPayIN_MenuItem };
                        listViewPays.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewPays_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonClose_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void طباعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_BillMaintenance  == null) throw new Exception("يجب انشاء الفاتورة أولا");

                OverLoad_Client.Reports.BillMaintenance_Print_Form BillMaintenance_Print_Form_
                = new Reports.BillMaintenance_Print_Form (DB, _BillMaintenance );
                BillMaintenance_Print_Form_.Show ();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
