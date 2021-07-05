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
    public partial class BillAdditionalClauseForm : Form
    {
        Operation _Operation;
        BillAdditionalClause _BillAdditionalClause;
        DatabaseInterface DB;

        public BillAdditionalClauseForm(DatabaseInterface db, Operation Operation_)
        {
            DB = db;
            _Operation = Operation_;
            InitializeComponent();
            textBoxOperationID.Text = _Operation.OperationID.ToString();
            textBoxOperationDesc.Text =Operation.GetOperationName( _Operation.OperationType);
            textBoxCurrency.Text = new OperationSQL(DB).GetOperation_BillAdditionalClause_Currency(_Operation).CurrencyName;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "اضافة";

        }

        public BillAdditionalClauseForm(DatabaseInterface db, BillAdditionalClause BillAdditionalClause_, bool Edit)
        {
            try
            {
                DB = db;
                InitializeComponent();
                _BillAdditionalClause = BillAdditionalClause_;
                _Operation = BillAdditionalClause_._Operation;
                textBoxOperationID.Text = _Operation.OperationID.ToString();
                textBoxOperationDesc.Text = Operation.GetOperationName(_Operation.OperationType);
                textBoxCurrency.Text = new OperationSQL(DB).GetOperation_BillAdditionalClause_Currency(_Operation).CurrencyName;

                textBoxDesc.Text  = _BillAdditionalClause.Description;
                textBoxValue.Text = _BillAdditionalClause.Value.ToString();
                if (!Edit)
                {
                    textBoxDesc.ReadOnly = true;
                    textBoxValue.ReadOnly = true;
                    buttonSave.Visible = false;
                }
                else
                {

                    textBoxDesc.ReadOnly = false;
                    textBoxValue.ReadOnly = false;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(":فشل تحميل الصفحة" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
       
        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (_Operation == null) return;
            //ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
            //ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

            //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
            //string SellType_ = ComboboxItem_selltype.Text;
            double value;
            try
            {
                value = Convert.ToDouble(textBoxValue.Text);
            }
            catch
            {
                MessageBox.Show("القيمة يجب ان تكون رقم حقيقي او صحيح", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    bool success = new BillAdditionalClauseSQL(DB).AddBillAdditionalClause
                        (_Operation, textBoxDesc.Text, value);
                    if (success)
                    {

                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر اضافة عملية القياس " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    bool success = new BillAdditionalClauseSQL(DB).UpdateBillAdditionalClause
                        (_BillAdditionalClause.ClauseID , textBoxDesc.Text, value);
                    if (success)
                    {

                        MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":فشل التعديل" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
