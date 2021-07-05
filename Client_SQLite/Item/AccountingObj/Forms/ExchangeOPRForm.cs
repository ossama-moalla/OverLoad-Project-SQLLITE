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
    public partial class ExchangeOPRForm : OverLoad_Form
    {
      
        DatabaseInterface DB;
        ExchangeOPR _ExchangeOPR;
        MoneyBox _MoneyBox;
        
        bool Edit;
        public ExchangeOPRForm(DatabaseInterface db,DateTime ExchangeOPRDate_,MoneyBox MoneyBox_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID)|| DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, MoneyBox_))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة هذا الصندوق, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _MoneyBox = MoneyBox_;
            dateTimePicker_.Value = ExchangeOPRDate_;
            Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxSourceCurrency, DB, defaultcurrency);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxTargetCurrency, DB, defaultcurrency);
            buttonSave.Name = "buttonAdd";
            buttonSave.Text  = "انشاء";

        }


        public ExchangeOPRForm(DatabaseInterface db, ExchangeOPR ExchangeOPR_, bool Edit_)
        {

            InitializeComponent();
            _MoneyBox = ExchangeOPR_._MoneyBox;
            Edit = Edit_;
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, ExchangeOPR_._MoneyBox))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة هذا الصندوق, لا يمكنك فتح هذه النافذة");

            _ExchangeOPR = ExchangeOPR_;
            dateTimePicker_.Value = _ExchangeOPR.ExchangeOprDate; ;
            loadForm(Edit);
            comboBox_MoneyBox.Enabled = false;
        }


        public void loadForm(bool edit)
        {

            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (_ExchangeOPR != null)
            {
                dateTimePicker_.Value = _ExchangeOPR.ExchangeOprDate;
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxSourceCurrency, DB, _ExchangeOPR.SourceCurrency);
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxTargetCurrency, DB, _ExchangeOPR.TargetCurrency);

                textBoxSourceExchangeRate.Text = _ExchangeOPR.SourceExchangeRate.ToString();
                textBoxTargetExchangeRate.Text = _ExchangeOPR.TargetExchangeRate.ToString();

                textBoxMoneyOUTValue.Text = _ExchangeOPR.OutMoneyValue .ToString();
                TextboxNotes.Text = _ExchangeOPR .Notes;
                if (!edit)
                {
                    dateTimePicker_.Enabled = false;
                    comboBoxSourceCurrency.Enabled = false;
                    textBoxSourceExchangeRate.ReadOnly = true;
                    comboBoxTargetCurrency.Enabled = false;
                    textBoxTargetExchangeRate.ReadOnly = true;
                    textBoxMoneyOUTValue.ReadOnly = true;
                    TextboxNotes.ReadOnly = true;
                    buttonSave.Visible = false;
                }
                else
                {
                    dateTimePicker_.Enabled = true ;
                    comboBoxSourceCurrency.Enabled = true ;
                    textBoxSourceExchangeRate.ReadOnly = false ;
                    comboBoxTargetCurrency.Enabled = true ;
                    textBoxTargetExchangeRate.ReadOnly = false ;
                    textBoxMoneyOUTValue.ReadOnly = false ;
                    TextboxNotes.ReadOnly = false ;
                    buttonSave.Visible = true ;
                }

            }
            else
            {
                Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxSourceCurrency, DB, defaultcurrency);
                textBoxSourceExchangeRate.Text = defaultcurrency.ExchangeRate.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem S_Currency_item = (ComboboxItem)comboBoxSourceCurrency.SelectedItem;
                Currency SourceCurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(S_Currency_item.Value);
                ComboboxItem T_Currency_item = (ComboboxItem)comboBoxTargetCurrency.SelectedItem;
                Currency TargetCurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(T_Currency_item.Value);
                ComboboxItem MoneyBoxitem = (ComboboxItem)comboBox_MoneyBox.SelectedItem;
                MoneyBox moneybox = new MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxitem.Value);
                if (SourceCurrency.CurrencyID == TargetCurrency.CurrencyID)
                {
                    throw new Exception("العملة الهدف يجب ان تكون مختلفة عن العملة المصدر");
                }
                double outmoneyvalue, source_exchangerate, target_exchangerate;
                try
                {
                    outmoneyvalue = Convert.ToDouble(textBoxMoneyOUTValue.Text);
                }
                catch
                {
                    throw new Exception("قيمة المبلغ الخارج يجب ان تكون رقم حقيقي او صحيح");
                  
                }
                try
                {
                    source_exchangerate = Convert.ToDouble(textBoxSourceExchangeRate.Text);
                    target_exchangerate = Convert.ToDouble(textBoxTargetExchangeRate.Text);

                }
                catch
                {
                    throw new Exception("سعر الصرف يجب ان يكون رقم حقيقي او صحيح");
                }
                if (buttonSave.Name == "buttonAdd")
                {

                        bool Succes = new ExchangeOPRSQL(DB).Add_ExchageOPR(moneybox.BoxID, dateTimePicker_.Value, SourceCurrency, source_exchangerate, outmoneyvalue, TargetCurrency, target_exchangerate, TextboxNotes.Text);
                    if (Succes)
                    {
                        MessageBox.Show("تم اضافة عملية الصرف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMoneyDataDetails_Flag = true;
                        this.Close();
                    }
                    else throw new Exception("فشل انشاء عملية الصرف");

                }

                else
                {

                        bool Succes = new ExchangeOPRSQL(DB).Update_ExchageOPR(_ExchangeOPR.ExchangeOprID, dateTimePicker_.Value, SourceCurrency, source_exchangerate, outmoneyvalue, TargetCurrency, target_exchangerate, TextboxNotes.Text);
                        if (Succes)
                        {
                            MessageBox.Show("تم تعديل عملية الصرف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Refresh_ListViewMoneyDataDetails_Flag = true;

                        this.Close();

                        }
                    else throw new Exception("فشل التعديل");


                }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("buttonSave_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
         
        }

        private void ValuesChanged(object sender, EventArgs e)
        {
            try
            {
                double source_exchangerate = Convert.ToDouble(textBoxSourceExchangeRate .Text );
                double target_exchangerate = Convert.ToDouble(textBoxTargetExchangeRate.Text);
                double exchangefactor = target_exchangerate / source_exchangerate;
                double money_out= Convert.ToDouble(textBoxMoneyOUTValue.Text);
                double inmoneyvalue = exchangefactor * money_out;
                textBoxExchangeFactor.Text = System.Math.Round (exchangefactor ,5).ToString ();
                textBoxMoneyINValue.Text = System.Math.Round(inmoneyvalue,2).ToString ();
            }
            catch
            {

                textBoxExchangeFactor.Text = "-";
                textBoxMoneyINValue.Text = "-";
            }
        }

        private void comboBoxSourceCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Currency Sourcecurrency = null;
                if (comboBoxSourceCurrency.SelectedIndex >= 0)
                {
                    ComboboxItem SourceComboboxItem_ = (ComboboxItem)comboBoxSourceCurrency.SelectedItem;
                    Sourcecurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(SourceComboboxItem_.Value);
                    textBoxSourceCurrencyName.Text = Sourcecurrency.CurrencyName;
                    textBoxSourceExchangeRate.Text = Sourcecurrency.ExchangeRate.ToString();
                    if (Sourcecurrency.ReferenceCurrencyID == null)
                        textBoxSourceExchangeRate.ReadOnly = true;
                    else
                        textBoxSourceExchangeRate.ReadOnly = false;
                }
                else
                {
                    textBoxSourceCurrencyName.Text = "-";
                    textBoxSourceExchangeRate.Text = "-";
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxSourceCurrency_SelectedIndexChanged:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

        }

        private void comboBoxTargetCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Currency Targecurrency = null;
                if (comboBoxTargetCurrency.SelectedIndex >= 0)
                {
                    ComboboxItem TargetComboboxItem_ = (ComboboxItem)comboBoxTargetCurrency.SelectedItem;
                    Targecurrency = new CurrencySQL(DB).GetCurrencyINFO_ByID(TargetComboboxItem_.Value);
                    textBoxTargetCurrencyName.Text = Targecurrency.CurrencyName;
                    textBoxTargetExchangeRate.Text = Targecurrency.ExchangeRate.ToString();
                    if (Targecurrency.ReferenceCurrencyID == null)
                        textBoxTargetExchangeRate.ReadOnly = true;
                    else
                        textBoxTargetExchangeRate.ReadOnly = false;
                }
                else
                {
                    textBoxTargetCurrencyName.Text = "-";
                    textBoxTargetExchangeRate.Text = "-";
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxTargetCurrency_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void ExchangeOPRForm_Load(object sender, EventArgs e)
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
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
