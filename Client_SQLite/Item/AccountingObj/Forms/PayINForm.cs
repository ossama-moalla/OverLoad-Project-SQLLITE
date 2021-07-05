using OverLoad_Client.AccountingObj.AccountingSQL;
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

namespace OverLoad_Client.AccountingObj.Forms
{
    public partial class PayINForm : OverLoad_Form 
    {
      
        DatabaseInterface DB;
        Bill _Bill;
        PayIN _PayIN;
        private MoneyBox _MoneyBox;
        
        bool Edit;
        public PayINForm(DatabaseInterface db,DateTime PayINDate_,MoneyBox MoneyBox_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, MoneyBox_))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة هذا الصندوق, لا يمكنك فتح هذه النافذة");

            _MoneyBox = MoneyBox_;
            InitializeComponent();
            this.Controls.Remove(panelBillIN_INFO);

            int form_h = this.Height;
            this.MinimumSize = new Size(panelPayInfo.Size .Width  +60, form_h);
            this.MaximumSize = new Size(panelPayInfo.Size.Width + 60, form_h);
            this.Size = new Size(panelPayInfo.Size .Width  + 60, form_h);


            dateTimePicker_.Value = PayINDate_;
            Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, defaultcurrency);
            //Operation.FillComboBoxBillType_PayIN(ref comboBoxOperationType, Operation.BILL_SELL);
            textBoxPayExchangeRate.Text = defaultcurrency.ExchangeRate .ToString();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text  = "انشاء";

        }
        public PayINForm(DatabaseInterface db, DateTime PayINDate_,Bill Bill_)
        {
            DB = db;
            //if (DB.Get_User_Allowed_MoneyBox().Count == 0) throw new Exception("لم تمنح اي صلاحية لادارة اي صندوق مال");

            _MoneyBox = null;
            InitializeComponent();
            _Bill    = Bill_ ;
           
            dateTimePicker_.Value = PayINDate_;
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _Bill._Currency);
            textBoxPayExchangeRate.Text = _Bill ._Currency.ExchangeRate .ToString();
            buttonSave.Name = "buttonAdd";
            FillBillIN_PanelINFO(true );
        }

        public PayINForm(DatabaseInterface db, PayIN PayIN_,bool Edit_)
        {

            InitializeComponent();
            _MoneyBox = PayIN_._MoneyBox;
            Edit = Edit_;
            DB = db;
            //if (DB.Get_User_Allowed_MoneyBox().Count == 0) throw new Exception("لم تمنح اي صلاحية لادارة اي صندوق مال");

            _PayIN = PayIN_;
            _Bill   = PayIN_._Bill  ;
 
            dateTimePicker_.Value = _PayIN.PayOprDate; ;
            comboBox_MoneyBox.Enabled = false;
            loadForm(Edit);
        }

        private void FillBillIN_PanelINFO(bool SetValue_Remain)
        {

            if (_Bill == null) return;
            labelOwner.Text = "عائدة لـ " + Operation.GetOperationName(_Bill ._Operation .OperationType);
                textBoxBillIN_ID.Text = _Bill._Operation.OperationID .ToString();
                textBoxContact.Text = _Bill._Contact.Get_Complete_ContactName_WithHeader();

                textBoxBillINDate.Text = _Bill.BillDate .ToString("yyyy-mm-dd hh:mm");
                textBoxCurrency.Text = _Bill._Currency.CurrencyName;
                textBoxBillIN_ExchangeRate.Text = _Bill.ExchangeRate.ToString();
               
                double billvalue , paid ;
            billvalue = new OperationSQL(DB).Get_OperationValue(_Bill._Operation);
            paid = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(_Bill._Operation);
            
                textBoxBillINValue.Text = billvalue.ToString()+" "+_Bill._Currency .CurrencySymbol ;
                textBoxPaid.Text = paid.ToString() + " " + _Bill._Currency.CurrencySymbol;
            textBoxRemain.Text = (billvalue - paid).ToString() + " " + _Bill._Currency.CurrencySymbol;
            if (SetValue_Remain) textBoxPayValue.Text = (billvalue - paid).ToString();
            else
                textBoxPayValue.Text = _PayIN .Value.ToString();
            textBoxPayValue.Focus();
        
  
            
        }
    
        
  

      
        public void loadForm(bool edit)
        {

             buttonSave.Name = "buttonSave";
            if (_PayIN  !=null)
            {
                if (_PayIN ._Bill !=null )FillBillIN_PanelINFO(false );
                dateTimePicker_.Value = _PayIN .PayOprDate;
                textBoxPayDesc.Text = _PayIN.PayDescription;
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _PayIN._Currency);
                textBoxPayExchangeRate.Text = _PayIN._Currency.ExchangeRate .ToString();
                textBoxPayValue.Text = _PayIN.Value.ToString();
                TextboxNotes.Text = _PayIN.Notes;
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
                textBoxPayExchangeRate.Text = defaultcurrency.ExchangeRate .ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            ComboboxItem item = (ComboboxItem)comboBoxCurrency.SelectedItem;
            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(item.Value);
            ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
            MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
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
            Operation opr = (_Bill == null ? null : _Bill._Operation);
            if (buttonSave.Name == "buttonAdd")
            {

                try
                {
                    
                    bool Succes = new PayINSQL(DB).Add_PayIN(moneybox.BoxID  , dateTimePicker_.Value, opr, textBoxPayDesc.Text, payvalue, exchangerate, currency, TextboxNotes.Text);
                    if (Succes)
                    {
                        MessageBox.Show("تم اضافة الدفعة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMoneyDataDetails_Flag  = true;
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
                    bool Succes = new PayINSQL(DB).Update_PayIN(_PayIN.PayOprID, dateTimePicker_.Value, opr, textBoxPayDesc.Text, payvalue, exchangerate, currency, TextboxNotes.Text);
                    if (Succes)
                    {
                        MessageBox.Show("تم تعديل الدفعة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMoneyDataDetails_Flag = true;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("buttonSave_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }

            }
        }

        private void PayINForm_Load(object sender, EventArgs e)
        {
            try
            {
                DB.FillComboBox_MoneyBox( ref comboBox_MoneyBox, _MoneyBox);
            }catch(Exception ee)
            {
                MessageBox.Show(ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
                this.Close();
            }
            //if(_Bill  !=null )
            //{
            //    FillBillIN_PanelINFO();
            //}
    

        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxCurrency.SelectedItem;
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);
                textBoxPayExchangeRate.Text = currency.ExchangeRate .ToString();
                if (currency.ReferenceCurrencyID == null)
                    textBoxPayExchangeRate.ReadOnly = true;
                else
                    textBoxPayExchangeRate.ReadOnly = false ;
            }
            catch
            {

            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
