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
    public partial class Change_MY_Password_From : Form
    {
        DatabaseInterface DB;
        public Change_MY_Password_From(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string old_password = textBox_Old_Password.Text;
                string new_password = textBox_New_Password.Text;
                string confirm_password = textBox_Confirm_Password.Text;
                if (!new_password.SequenceEqual(confirm_password)) throw new Exception("لا يوجد تطابق بين كلمة المرور الجديدة و التأكيد");
               if(  DB.UpdateMYPassword(old_password, new_password))
                    MessageBox.Show("تم التغيير بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information );

            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }
    }
}
