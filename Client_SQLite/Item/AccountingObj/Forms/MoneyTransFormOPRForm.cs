using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
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
    public partial class MoneyTransFormOPRForm : OverLoad_Form 
    {
        DatabaseInterface DB;
        MoneyTransFormOPR _MoneyTransFormOPR;
        MoneyBox _SourceMoneyBox;
        MoneyBox _TargetMoneyBox;
        
        bool Edit;
        public MoneyTransFormOPRForm(DatabaseInterface db, DateTime MoneyTransFormOPRDate_, MoneyBox SourceMoneyBox_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, SourceMoneyBox_ ))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة هذا الصندوق, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _SourceMoneyBox = SourceMoneyBox_;
            dateTimePicker_.Value = MoneyTransFormOPRDate_;
            Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, defaultcurrency);
            textBoxExchangeRate.Text = defaultcurrency.ExchangeRate.ToString();
            TextboxSourceMoneyBox.Text = _SourceMoneyBox.BoxName;
            DB.FillComboBox_MoneyBox(ref comboBoxTargetMoneyBox, null);
            if(DB.__User._Employee ==null )
            {
                textBoxCreatorUser.Text = "مدير النظام";
            }
            else
            {
                textBoxCreatorUser.Text = DB.__User._Employee.EmployeeName;
            }
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";

        }
        public MoneyTransFormOPRForm(DatabaseInterface db, MoneyTransFormOPR MoneyTransFormOPR_, bool Edit_)
        {

            InitializeComponent();
            _MoneyTransFormOPR = MoneyTransFormOPR_;
            _SourceMoneyBox  = _MoneyTransFormOPR.SourceMoneyBox ;
            _TargetMoneyBox = _MoneyTransFormOPR.TargetMoneyBox;
            TextboxSourceMoneyBox.Text = _SourceMoneyBox.BoxName;

            Edit = Edit_;
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, MoneyTransFormOPR_.SourceMoneyBox ))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة هذا الصندوق, لا يمكنك فتح هذه النافذة");

            dateTimePicker_.Value = _MoneyTransFormOPR.MoneyTransFormOPRDate ; ;
            loadForm(Edit);

        }


        public void loadForm(bool edit)
        {

            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (_MoneyTransFormOPR  != null)
            {
                dateTimePicker_.Value = _MoneyTransFormOPR .MoneyTransFormOPRDate ;
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _MoneyTransFormOPR ._Currency );
                DB.FillComboBox_MoneyBox(ref comboBoxTargetMoneyBox, _TargetMoneyBox );
                textBoxExchangeRate.Text = _MoneyTransFormOPR.ExchangeRate.ToString();
                textBoxValue.Text = _MoneyTransFormOPR.Value.ToString();
                TextboxNotes.Text = _MoneyTransFormOPR.Notes;
                if (_MoneyTransFormOPR.Creator_User._Employee == null)
                {
                    textBoxCreatorUser.Text = "مدير النظام";
                }
                else
                {
                    textBoxCreatorUser.Text = _MoneyTransFormOPR.Creator_User._Employee.EmployeeName;
                }
                if (_MoneyTransFormOPR.Confirm_User == null)
                {
                    textBoxConfirmUser.Text = "لم يتم التاكيد بعد";
                    textBoxConfirmUser.BackColor = Color.Orange;
                }
                else
                {
                    textBoxConfirmUser.BackColor = Color.LimeGreen;
                    if (_MoneyTransFormOPR.Confirm_User ._Employee== null)
                    {
                        textBoxConfirmUser.Text = "مدير النظام";
                    }
                    else
                    {
                        textBoxConfirmUser.Text = _MoneyTransFormOPR.Confirm_User._Employee.EmployeeName;
                    }
                }
                    
                if (!edit || _MoneyTransFormOPR .Confirm_User !=null )
                {
                    dateTimePicker_.Enabled = false;
                    comboBoxCurrency.Enabled = false;
                    textBoxExchangeRate.ReadOnly = true;
                    comboBoxTargetMoneyBox.Enabled = false;
                    comboBoxCurrency.Enabled  = false ;
                    textBoxValue.ReadOnly = true;
                    TextboxNotes.ReadOnly = true;
                    buttonSave.Visible = false;
                }
                else
                {
                    dateTimePicker_.Enabled = true ;
                    comboBoxCurrency.Enabled = true ;
                    textBoxExchangeRate.ReadOnly = false ;
                    comboBoxTargetMoneyBox.Enabled = true ;
                    comboBoxCurrency.Enabled = true ;
                    textBoxValue.ReadOnly = false ;
                    TextboxNotes.ReadOnly = false ;
                    buttonSave.Visible = true ;
                }

            }
     
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            ComboboxItem Currency_item = (ComboboxItem)comboBoxCurrency .SelectedItem;
            Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Currency_item.Value);
            ComboboxItem TargetMoneyBox_item = (ComboboxItem)comboBoxTargetMoneyBox.SelectedItem;
            MoneyBox TargetMoneyBox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(TargetMoneyBox_item.Value);
      
            double value, exchangerate;
            try
            {
                value = Convert.ToDouble(textBoxValue.Text);
            }
            catch
            {
                MessageBox.Show("قيمة المبلغ  يجب ان تكون رقم حقيقي او صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
               exchangerate = Convert.ToDouble(textBoxExchangeRate.Text);

            }
            catch
            {
                MessageBox.Show("سعر الصرف يجب ان يكون رقم حقيقي او صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (buttonSave.Name == "buttonAdd")
            {

                try
                {
                    bool Succes = new MoneyTransFormOPRSQL(DB).Add_MoneyTransFormOPR ( dateTimePicker_.Value, 
                        _SourceMoneyBox .BoxID , TargetMoneyBox.BoxID , value,  exchangerate, currency, TextboxNotes.Text);
                    if (Succes)
                    {
                        MessageBox.Show("تم اضافة عملية التحويل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    bool Succes = new MoneyTransFormOPRSQL(DB).Update_MoneyTransFormOPR 
                        (_MoneyTransFormOPR .MoneyTransFormOPRID , dateTimePicker_.Value, 
                        _SourceMoneyBox .BoxID , TargetMoneyBox.BoxID, value, 
                        exchangerate,currency , TextboxNotes.Text);
                    if (Succes)
                    {
                        MessageBox.Show("تم تعديل عملية التحويل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            Currency currency = null;
            if (comboBoxCurrency.SelectedIndex >= 0)
            {
                ComboboxItem CurrencyComboboxItem_ = (ComboboxItem)comboBoxCurrency.SelectedItem;
                currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(CurrencyComboboxItem_.Value);
                textBoxExchangeRate.Text = currency.ExchangeRate.ToString();

            }
            else
            {
                textBoxExchangeRate.Text = "-";
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
