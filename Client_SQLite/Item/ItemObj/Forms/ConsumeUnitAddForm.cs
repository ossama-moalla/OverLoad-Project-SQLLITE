
using OverLoad_Client.ItemObj.ItemObjSQL ;
using OverLoad_Client.ItemObj.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.ItemObj.Forms
{

    public partial class ConsumeUnitAddForm : Form
    {
        Item item;
        ConsumeUnit ConsumeUnit_;
        ConsumeUnitSql ConsumeUnitSql_;
        DatabaseInterface DB;
        public ConsumeUnitAddForm(DatabaseInterface db, Item item_)
        {
            InitializeComponent();
            DB = db;
            item = item_;
            ConsumeUnitSql_ = new ConsumeUnitSql(DB);
            TextBoxConumeUnitName.Text = "";
            TextBoxFactor.Text = "";
            Add.Name = "Add";
            Add.Text = "اضافة";
        }
        public ConsumeUnitAddForm(DatabaseInterface db, ConsumeUnit ConsumeUnit__)
        {
            InitializeComponent();
            DB = db;
            ConsumeUnitSql_ = new ConsumeUnitSql(DB);
            ConsumeUnit_ = ConsumeUnit__;
            TextBoxConumeUnitName.Text  = ConsumeUnit_.ConsumeUnitName;
            TextBoxFactor.Text = ConsumeUnit_.Factor .ToString ();
            Add.Name = "Update";
            Add.Text = "تعديل";
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if (TextBoxConumeUnitName.Text.Length == 0)
            {
                MessageBox.Show("يرجى ادخال اسم وحدة التوزيع ");
                return;
            }
            double factor;
            if (!double.TryParse(TextBoxFactor.Text, out factor))
            {
                MessageBox.Show("يرجى ادخال معامل التحويل رقم حقيقي ");
                return;
            }

            if (Add.Name == "Add")
            {
                bool result = ConsumeUnitSql_.AddConsumeUnit(item, TextBoxConumeUnitName.Text , factor);
                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                if (Add.Name == "Update")
                {
                    bool result;
                    result = ConsumeUnitSql_.UpdateConsumeUnit(ConsumeUnit_.Item_, ConsumeUnit_.ConsumeUnitID, TextBoxConumeUnitName.Text, factor);
                    if (result)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }


                }
            }

        }
    }
}
