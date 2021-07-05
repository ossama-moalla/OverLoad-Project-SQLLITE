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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.AccountingObj.Forms
{
    public partial class Add_AccessContainerPremession_Form : Form
    {
        DatabaseInterface DB;
        DatabaseInterface.User _user;
        public Add_AccessContainerPremession_Form(DatabaseInterface db, DatabaseInterface.User User_)
        {
           
            InitializeComponent();
            DB = db;
            //if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("أنت غير منضم لمجموعة المدراء, لا يمكنك فتح هذه النافذة");
                _user = User_;
            LoadContainerData(null);
        }
        private void textBoxContainerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    uint Containerid = Convert.ToUInt32(textBoxContainerID.Text);
                    if(Containerid ==0) LoadContainerData(null );
                    else
                    {
                        TradeStoreContainer Container__ = new TradeStoreContainerSQL(DB).GetContainerBYID(Containerid);
                        if (Container__ == null)
                        {
                            MessageBox.Show("لا توجد حاوية تخزين بهذا الرقم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        LoadContainerData(Container__);
                    }
          
                }
                catch
                {
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        private void textBoxContainer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Trade.Forms.Container.User_ShowLocationsForm User_ShowLocationsForm_ = new Trade.Forms.Container.User_ShowLocationsForm(DB, null, Trade.Forms.Container.User_ShowLocationsForm.SELECT_Container);
                    DialogResult dd = User_ShowLocationsForm_.ShowDialog();
                    if (dd == DialogResult.OK)
                    {
                        LoadContainerData(User_ShowLocationsForm_._ReturnContainer);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private  void LoadContainerData(TradeStoreContainer container)
        {
            if(container ==null )
            {
                textBoxContainerID.Text = "0";
                textBoxContainerName.Text = "جميع الحاويات";
                textBoxContainerPath.Text = "/";

            }
            else
            {
                textBoxContainerID.Text = container.ContainerID.ToString();
                textBoxContainerName.Text = container.ContainerName;
                textBoxContainerPath.Text = new TradeStoreContainerSQL(DB).GetContainerPath(container);

            }
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                uint Containerid = Convert.ToUInt32(textBoxContainerID.Text);
                TradeStoreContainer Container__;
                if (Containerid == 0)
                    Container__ = null;
                else 
                     Container__ = new TradeStoreContainerSQL(DB).GetContainerBYID (Containerid);
                bool success = DB.Add_AccessContainerPremession(_user .UserID, Container__);
                if(success )
                {
                    MessageBox.Show("تم منح الصلاحية بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }
    }
}
