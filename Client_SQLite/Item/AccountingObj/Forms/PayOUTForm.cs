using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Company.Objects;
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

namespace OverLoad_Client.AccountingObj.Forms
{
    public partial class PayOUTForm : OverLoad_Form 
    {

        DatabaseInterface DB;
        Bill _Bill;
        EmployeePayOrder _EmployeePayOrder;
        PayOUT _PayOUT;
        MoneyBox _MoneyBox;
        
        bool Edit;
        public PayOUTForm(DatabaseInterface db, DateTime PayOUTDate_,MoneyBox MoneyBox_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, MoneyBox_))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة هذا الصندوق, لا يمكنك فتح هذه النافذة");

            _MoneyBox = MoneyBox_;
            InitializeComponent();
            this.Controls.Remove(panelOwner_INFO);
            int form_h = this.Height;
            this.MinimumSize = new Size(panelPayOUTfo.Size.Width + 60, form_h);
            this.MaximumSize = new Size(panelPayOUTfo.Size.Width + 60, form_h);
            this.Size = new Size(panelPayOUTfo .Size.Width + 60, form_h);

            dateTimePicker_.Value = PayOUTDate_;
            Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, defaultcurrency);
            //Operation.FillComboBoxBillType_PayOUT(ref comboBoxOperationType, Operation.BILL_SELL);
            textBoxPayExchangeRate.Text = defaultcurrency.ExchangeRate.ToString();
            buttonSave.Name = "buttonAdd";

        }
        public PayOUTForm(DatabaseInterface db, DateTime PayOUTDate_, BillBuy Bill_)
        {
            DB = db;
            //if (DB.Get_User_Allowed_MoneyBox().Count == 0) throw new Exception("لم تمنح اي صلاحية لادارة اي صندوق مال");

            _MoneyBox = null;
            InitializeComponent();
            _Bill = Bill_;
            _EmployeePayOrder = null;
            dateTimePicker_.Value = PayOUTDate_;
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _Bill._Currency);
            textBoxPayExchangeRate.Text = _Bill._Currency.ExchangeRate.ToString();
            buttonSave.Name = "buttonAdd";
            FillOwner_PanelINFO(true );
        }
        public PayOUTForm(DatabaseInterface db, DateTime PayOUTDate_, EmployeePayOrder EmployeePayOrder_)
        {
            DB = db;
            //if (DB.Get_User_Allowed_MoneyBox().Count == 0) throw new Exception("لم تمنح اي صلاحية لادارة اي صندوق مال");

            _MoneyBox = null;
            InitializeComponent();
            _Bill = null ;
            _EmployeePayOrder = EmployeePayOrder_;
            dateTimePicker_.Value = PayOUTDate_;
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _EmployeePayOrder._Currency);
            textBoxPayExchangeRate.Text = _EmployeePayOrder._Currency.ExchangeRate.ToString();
            buttonSave.Name = "buttonAdd";
            FillOwner_PanelINFO(true );
        }

        public PayOUTForm(DatabaseInterface db, PayOUT PayOUT_, bool Edit_)
        {

            InitializeComponent();
            _MoneyBox = PayOUT_._MoneyBox;
            Edit = Edit_;
            DB = db;
            //if (DB.Get_User_Allowed_MoneyBox().Count == 0) throw new Exception("لم تمنح اي صلاحية لادارة اي صندوق مال");

            _PayOUT = PayOUT_;
            _Bill = PayOUT_._Bill;
            _EmployeePayOrder = PayOUT_._EmployeePayOrder;
            dateTimePicker_.Value = _PayOUT.PayOprDate; ;
            //FillOwner_PanelINFO(false );
            comboBox_MoneyBox.Enabled = false;
            loadForm(Edit);
        }

        private async  void FillOwner_PanelINFO(bool SetValue_Remain)
        {
            if (_Bill != null)
            {
                labelOwner.Text = "عائدة لـ " + Operation.GetOperationName(_Bill._Operation.OperationType);
                textBoxBillOUT_ID.Text = _Bill._Operation.OperationID.ToString();
                textBoxContact.Text = _Bill._Contact.Get_Complete_ContactName_WithHeader();

                textBoxBillOUTDate.Text = _Bill.BillDate.ToString("yyyy-mm-dd hh:mm");
                textBoxCurrency.Text = _Bill._Currency.CurrencyName;
                textBoxBillOUT_ExchangeRate.Text = _Bill.ExchangeRate.ToString();

                double billvalue, paid;
                billvalue = new OperationSQL(DB).Get_OperationValue(_Bill._Operation);
                paid = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(_Bill._Operation);

                textBoxBillOUTValue.Text = billvalue.ToString() + " " + _Bill._Currency.CurrencySymbol;
                textBoxPaid.Text = paid.ToString() + " " + _Bill._Currency.CurrencySymbol;
                textBoxRemain.Text = (billvalue - paid).ToString() + " " + _Bill._Currency.CurrencySymbol;
                if (SetValue_Remain) textBoxPayValue.Text = (billvalue - paid).ToString();
                else
                    textBoxPayValue.Text = _PayOUT.Value.ToString();
                textBoxPayValue.Focus();
            }
            else if (_EmployeePayOrder !=null )
            {

                labelOwner.Text = "عائدة لأمر صرف رقم: " +_EmployeePayOrder.PayOrderID.ToString ();
                textBoxBillOUT_ID.Text = _EmployeePayOrder.PayOrderID.ToString();
                textBoxContact.Text = _EmployeePayOrder._Employee .EmployeeName;

                textBoxBillOUTDate.Text = _EmployeePayOrder.PayOrderDate.ToString("yyyy-mm-dd hh:mm");
                textBoxCurrency.Text = _EmployeePayOrder._Currency.CurrencyName;
                textBoxBillOUT_ExchangeRate.Text = _EmployeePayOrder.ExchangeRate.ToString();

                double billvalue, paid;
                billvalue = new OperationSQL(DB).Get_OperationValue(Operation.Employee_PayOrder, _EmployeePayOrder.PayOrderID );
                paid = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Operation.Employee_PayOrder, _EmployeePayOrder.PayOrderID);

                textBoxBillOUTValue.Text = billvalue.ToString() + " " + _EmployeePayOrder._Currency.CurrencySymbol;
                textBoxPaid.Text = paid.ToString() + " " + _EmployeePayOrder._Currency.CurrencySymbol;
                textBoxRemain.Text = (billvalue - paid).ToString() + " " + _EmployeePayOrder._Currency.CurrencySymbol;
                if (SetValue_Remain) textBoxPayValue.Text = (billvalue - paid).ToString();
                else
                    textBoxPayValue.Text = _PayOUT.Value.ToString();
                textBoxPayValue.Focus();
            }
            



        }





        public void loadForm(bool edit)
        {
            
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (_PayOUT != null)
            {
                if (_PayOUT._Bill != null ||_PayOUT ._EmployeePayOrder !=null ) FillOwner_PanelINFO(false );
                dateTimePicker_.Value = _PayOUT.PayOprDate;
                textBoxPayDesc.Text = _PayOUT.PayDescription;
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _PayOUT._Currency);
                textBoxPayExchangeRate.Text = _PayOUT._Currency.ExchangeRate.ToString();
                textBoxPayValue.Text = _PayOUT.Value.ToString();
                TextboxNotes.Text = _PayOUT.Notes;
                if (!edit)
                {
                    dateTimePicker_.Enabled = false;
                    textBoxPayDesc.ReadOnly = true;
                    comboBoxCurrency.Enabled = false;
                    textBoxPayExchangeRate.ReadOnly = true;
                    textBoxPayValue.ReadOnly = true;
                    TextboxNotes.ReadOnly = true;
                    buttonSave.Visible = false;
                }

            }
            else
            {
                Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, defaultcurrency);
                textBoxPayExchangeRate.Text = defaultcurrency.ExchangeRate.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            ComboboxItem item = (ComboboxItem)comboBoxCurrency.SelectedItem;
            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(item.Value);
            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox  moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
            double payvalue, exchangerate;
            try
            {
                payvalue = Convert.ToDouble(textBoxPayValue.Text);
            }
            catch
            {
                MessageBox.Show("قيمة الدفعة يجب ان تكون رقم حقيقي او صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                exchangerate = Convert.ToDouble(textBoxPayExchangeRate.Text);
            }
            catch
            {
                MessageBox.Show("سعر الصرف يجب ان يكون رقم حقيقي او صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Operation opr;
            if (_Bill != null) opr = _Bill._Operation;
            else if (_EmployeePayOrder != null) opr = new Operation(Operation.Employee_PayOrder, _EmployeePayOrder.PayOrderID);
            else opr = null;
            if (buttonSave.Name == "buttonAdd")
            {

                try
                {

                    bool Succes = new PayOUTSQL(DB).Add_PayOUT(moneybox .BoxID , dateTimePicker_.Value, opr, textBoxPayDesc.Text, payvalue, exchangerate, currency, TextboxNotes.Text);
                    if (Succes)
                    {
                        MessageBox.Show("تم اضافة الدفعة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMoneyDataDetails_Flag = true;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("buttonSave_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }

            }

            else
            {
                try
                {
                    bool Succes = new PayOUTSQL(DB).Update_PayOUT(_PayOUT.PayOprID, dateTimePicker_.Value, opr, textBoxPayDesc.Text, payvalue, exchangerate, currency, TextboxNotes.Text);
                    if (Succes)
                    {
                        MessageBox.Show("تم تعديل الدفعة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMoneyDataDetails_Flag  = true;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("buttonSave_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }

            }
        }

        private void PayOUTForm_Load(object sender, EventArgs e)
        {
            try
            {
                DB.FillComboBox_MoneyBox(ref comboBox_MoneyBox, _MoneyBox);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            //FillOwner_PanelINFO();
      
        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);
                textBoxPayExchangeRate.Text = currency.ExchangeRate.ToString();
                if (currency.ReferenceCurrencyID == null)
                    textBoxPayExchangeRate.ReadOnly = true;
                else
                    textBoxPayExchangeRate.ReadOnly = false;
            }
            catch
            {

            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
