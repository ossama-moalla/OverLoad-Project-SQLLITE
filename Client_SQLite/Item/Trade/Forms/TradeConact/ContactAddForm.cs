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

namespace OverLoad_Client.Trade.Forms.TradeContact
{
    public partial class ContactAddForm : Form
    {
        private class ComboboxItem
        {


            public string Text { get; set; }
            public bool Value { get; set; }
            public ComboboxItem(string Text_, bool Value_)
            {
                Text = Text_;
                Value = Value_;
            }
            public override string ToString()
            {
                return Text;
            }
        }
        Contact _Contact;
        DatabaseInterface DB;
        public ContactAddForm(DatabaseInterface db)
        {
            DB = db;
            InitializeComponent();
            fillcombobox(false);
        }

        public ContactAddForm(DatabaseInterface db, Contact Contact_)
        {
            DB = db;
            _Contact = Contact_;
            InitializeComponent();
            fillcombobox(Contact_.ContactType);
            textBoxName.Text = _Contact.ContactName  ;
            textBoxPhone.Text = _Contact.Phone ;
            textBoxMobile.Text = _Contact.Mobile;
            TextboxAddress.Text = _Contact.Address;
            labeltype.Text = "تعديل بيانات الجهة";
            Add.Name = "Update";
            Add.Text = "تعديل";
        }
        public void fillcombobox(bool value)
        {
            ComboboxItem item1 = new ComboboxItem(Contact .ConvertTypeToString (Contact.CONTACT_PERSON), Contact.CONTACT_PERSON);
            ComboboxItem item2 = new ComboboxItem(Contact.ConvertTypeToString(Contact.CONTACT_COMPANY), Contact.CONTACT_COMPANY);
            comboBox1.Items.Add(item1);
            comboBox1.Items.Add(item2);
            if (value) comboBox1.SelectedIndex = 1;
            else comboBox1.SelectedIndex = 0;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if(textBoxName .Text .Length >0)
            {
                ComboboxItem item = (ComboboxItem)comboBox1.SelectedItem;
                if (Add.Name =="Add")
                {

                    bool success = (new ContactSQL (DB)).AddContact  (item.Value, textBoxName.Text, textBoxPhone.Text, textBoxMobile.Text,TextboxAddress .Text );
                    if (success)
                    {
                        MessageBox.Show("تم اضافة  الجهة الى قاعدة البيانات بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("فشل اضافة جهة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    bool success = (new ContactSQL(DB)).UpdateContact  (_Contact  .ContactID, item.Value, textBoxName.Text, textBoxPhone.Text, textBoxMobile.Text, TextboxAddress.Text);
                    if (success)
                    {
                        MessageBox.Show("تم تعديل بيانات الجهة بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("فشل التعديل", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error );

                    }
                }
             
            }
        }
    }
}
