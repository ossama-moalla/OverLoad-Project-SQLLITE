using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public partial class Form1 : Form
    {
        DatabaseInterface DB;
        public Form1(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                DataTable t = DB.GetData(textBox1.Text);
                dataGridView1.DataSource = t;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DB.ExecuteSQLCommand(textBox1.Text);

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message );
            }
            
        }
    }
}
