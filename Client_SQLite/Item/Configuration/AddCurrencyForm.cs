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

namespace OverLoad_Client.Configuration
{
    public partial class AddCurrencyForm : Form
    {
        Currency _Currency;
        DatabaseInterface DB;
        public AddCurrencyForm(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "اضافة";
            textBoxRef.Text = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB).CurrencyName;

        }
        public AddCurrencyForm(DatabaseInterface db,Currency currency)
        {
            _Currency = currency;
            InitializeComponent();
            DB = db;
            buttonSave.Name = "buttonUpdate";
            buttonSave.Text = "حفظ";
            textBoxCurrencyName.Text = _Currency.CurrencyName;
            textBoxCurrencySymbol.Text = currency.CurrencySymbol;
            textBoxExhangeRate.Text = currency.ExchangeRate .ToString();
            textBoxRef.Text = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB).CurrencyName;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            double exchangerate;
            try
            {
                exchangerate = Convert.ToDouble(textBoxExhangeRate.Text);
            }
            catch
            {
                MessageBox.Show("سعر الصرف يجب ان يكون رقم حقيقي", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool disable = false;
            if (comboBox1.SelectedIndex == 0) disable = false;
            else disable = true;
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    bool success = new CurrencySQL(DB).AddCurrency(textBoxCurrencyName.Text ,textBoxCurrencySymbol.Text , exchangerate,disable );
                    if(success )
                    {
                        MessageBox.Show("تم اضافة العملة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch(Exception ee)
                {
                    MessageBox.Show("فشل انشاء العملة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                try
                {
                    bool success = new CurrencySQL(DB).UpdateCurrency (_Currency .CurrencyID,textBoxCurrencyName.Text, textBoxCurrencySymbol.Text, exchangerate,disable );
                    if (success)
                    {
                        MessageBox.Show("تم تعديل العملة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل تعديل  العملة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }


        private void AddCurrency_Load(object sender, EventArgs e)
        {
            if (_Currency != null)
                if (_Currency.ReferenceCurrencyID == null)
                {
                    MessageBox.Show("لايمكن التعديل على العملة المرجعية", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();

                }
            comboBox1.SelectedIndex = 0;
        }
    }
}
