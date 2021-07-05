using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public partial class LogINForm : Form
    {
        private DatabaseInterface _DatabaseInterface;

        public LogINForm(DatabaseInterface DatabaseInterface_)
        {
            InitializeComponent();
            _DatabaseInterface = DatabaseInterface_;
            textboxUserName .Text = ProgramGeneralMethods.Registry_GetUserName();
        }

       
       private async  void LogIN()
        {
            buttonNext.Enabled = false;
            try
            {
                

                    _DatabaseInterface.LogIN(textboxUserName.Text, textBoxPassWord.Text);
                    ProgramGeneralMethods.Registry_SetUserName(textboxUserName.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("LogIN:"+ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            buttonNext.Enabled = true ;
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
           
             LogIN(); 
        }
 
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBoxPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) buttonNext.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Trade.TradeSQL.TradeItemStoreSQL(_DatabaseInterface).Clear_Places();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 ff = new Form1(_DatabaseInterface);
            ff.Show ();
        }
    }
}
