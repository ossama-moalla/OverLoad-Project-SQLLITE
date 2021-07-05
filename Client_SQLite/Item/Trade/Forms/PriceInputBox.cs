using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OverLoad_Client.Trade.Forms
{
    public partial class PriceInputBox : Form
    {
        private double price;
        public double Price
        {
            get { return price; }
        }
        public PriceInputBox(string title,string selltype,string tradestate,string consumeunit,string currency,string pricestr)
        {
            InitializeComponent();
            textBoxSellType.Text = selltype;
            textBoxItemState.Text = tradestate;
            textBoxConsumUnit.Text = consumeunit;
            textBoxCurrency.Text = currency;
            textBoxPrice.Text = pricestr;
            this.Text = title;
            textBoxPrice.Select ();
        }

        private void textBoxPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (IsDouble(textBoxPrice.Text))
                {
                    double price_ = Convert.ToDouble(textBoxPrice.Text);
                    if (price_ < 0)
                    {
                        MessageBox.Show(" السعر يجب ان يكون اكبر او يساوي الصفر");
                        return;
                    }
                    price = price_;

                    this.DialogResult = DialogResult.OK;
                this.Close();
                }
                else MessageBox.Show("يرجى ادخال رقم حقيقي");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsDouble (textBoxPrice.Text ))
            {
                double price_ = Convert.ToDouble(textBoxPrice.Text);
                if (price_ < 0)
                {
                    MessageBox.Show(" السعر يجب ان يكون اكبر او يساوي الصفر");
                    return;
                }
                price = price_;
               
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("يرجى ادخال رقم حقيقي");
        }
        public bool IsDouble(string s)
        {
            try
            {
                double d = Convert.ToDouble(s);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
