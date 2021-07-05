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
    public partial class ContainerAddForm : Form
    {
       uint? ParentContainerID;
        private  TradeStoreContainer _Container;
        TradeStoreContainerSQL containersql;
        DatabaseInterface DB;
        bool _Function;
        public ContainerAddForm( DatabaseInterface db,uint? ParentContainerID_,string ContainerName_,string Desc)
        {
            InitializeComponent();
            DB = db;
            ParentContainerID = ParentContainerID_;
            containersql = new TradeStoreContainerSQL(DB);
            TextBoxName.Text = ContainerName_;
            textBoxDesc.Text = Desc;
            this.TextBoxName.SelectionStart = 0;
            this.TextBoxName.SelectionLength = ContainerName_ .Length;
            TextBoxName.Focus();
        }
        public ContainerAddForm(DatabaseInterface db, TradeStoreContainer Container_)
        {
            InitializeComponent();
            DB = db;
            containersql = new TradeStoreContainerSQL (DB);
            _Container = Container_;
            this.Text = "تعديل";
            this.TextBoxName.Text = Container_.ContainerName ;
            this.TextBoxName.SelectionStart = 0;
            this.TextBoxName.SelectionLength = Container_.ContainerName.Length;
            this.textBoxDesc.Text = Container_.Desc;
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
            { MessageBox.Show("اسم المجلد يجب ان لا يكون فارغا"); return; }
            if (Add.Name == "Add")
            {
                bool r = containersql .AddContainer (ParentContainerID , TextBoxName.Text,textBoxDesc.Text );
                if (r == true)
                {
                    this.DialogResult = DialogResult.OK; this.Dispose();
                }
            }
           if(Add.Name =="Update")
            {
                bool r = containersql.UpdateContainer  (_Container .ContainerID , TextBoxName.Text,textBoxDesc .Text );
                if (r == true)
                {
                    this.DialogResult = DialogResult.OK; this.Dispose();
                }
            }
        }

 
    }
}
