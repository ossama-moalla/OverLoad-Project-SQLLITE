using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace OverLoad_Client.Trade.Forms.Container
{
    public partial class AddPlaceForm : Form
    {
        int? ParentContainerID;
        TradeStoreContainer _Container;
        TradeStorePlace Place;
        TradeStorePlaceSQL placesql;
        DatabaseInterface DB;
        public AddPlaceForm(DatabaseInterface db, TradeStoreContainer Container_)
        {
            InitializeComponent();
            DB = db;
            _Container = Container_;
            placesql = new TradeStorePlaceSQL(DB);
            TextBoxName.Focus();
        }
        public AddPlaceForm(DatabaseInterface db, TradeStorePlace Place_)
        {
            InitializeComponent();
            DB = db;
            placesql = new TradeStorePlaceSQL(DB);
            this.Text = "تعديل"; 
            Place = Place_;
            this.TextBoxName.Text = Place.PlaceName;
            this.TextBoxName.SelectionStart = 0;
            this.TextBoxName.SelectionLength = Place.PlaceName.Length;
            this.textBoxDesc.Text = Place.Desc;
            TextBoxName.Focus();
            Add.Name = "Update";
            Add.Text = "تعديل";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (TextBoxName.Text.Length == 0)
            { MessageBox.Show("اسم مكان التخزين يجب ان لا يكون فارغا"); return; }
            if (Add.Name == "Add")
            {
                bool r = placesql .AddPlace  (_Container  , TextBoxName.Text,textBoxDesc.Text );
                if (r == true)
                {
                    MessageBox.Show("تم الادخال بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK; this.Dispose();
                }
            }
           if(Add.Name =="Update")
            {
                bool r = placesql.UpdatePlace   (Place.PlaceID, TextBoxName.Text,textBoxDesc .Text );
                if (r == true)
                {
                    MessageBox.Show("تم التعديل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Information );
                    this.DialogResult = DialogResult.OK; this.Dispose();
                }
            }
        }

 
    }
}
