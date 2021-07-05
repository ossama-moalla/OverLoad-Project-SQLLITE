using OverLoad_Client.ItemObj.ItemObjSQL;
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

namespace OverLoad_Client.AccountingObj.Forms
{
    public partial class Add_AccessFolderPremession_Form : Form
    {
        DatabaseInterface DB;
        DatabaseInterface.User _user;
        public Add_AccessFolderPremession_Form(DatabaseInterface db, DatabaseInterface.User User_)
        {
            InitializeComponent();
            DB = db;
            //if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("أنت غير منضم لمجموعة المدراء, لا يمكنك فتح هذه النافذة");

            _user = User_;
            LoadFolderData(null);
        }
        private void textBoxFolderID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    uint folderid = Convert.ToUInt32(textBoxFolderID .Text);
                    if (folderid == 0) LoadFolderData(null);
                    else
                    {
                        Folder Folder__ = new FolderSQL(DB).GetFolderInfoByID(folderid);
                        if (Folder__ == null)
                        {
                            MessageBox.Show("لا يوجد صنف  بهذا الرقم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        LoadFolderData(Folder__);
                    }
                }
                catch
                {
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        private void textBoxFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ItemObj.Forms.User_ShowItemsForm ShowItemsForm_ = new ItemObj.Forms.User_ShowItemsForm(DB, null, ItemObj.Forms.User_ShowItemsForm.SELECT_FOLDER);
                DialogResult dd = ShowItemsForm_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    LoadFolderData(ShowItemsForm_.ReturnFolder );
                        }
            }
        }
        private void LoadFolderData(Folder  folder)
        {
            if (folder == null)
            {
                textBoxFolderID.Text = "0";
                textBoxFolderName.Text = "جميع الأصناف";
                textBoxFolderPath.Text = "/";

            }
            else
            {
                textBoxFolderID.Text = folder.FolderID .ToString();
                textBoxFolderName.Text = folder.FolderName;
                textBoxFolderPath.Text = new FolderSQL(DB).GetFolderPath(folder );

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
                
                uint Folderid = Convert.ToUInt32(textBoxFolderID.Text);
                Folder Folder__;
                if (Folderid == 0)
                    Folder__ = null;
                else 
                     Folder__ = new ItemObj.ItemObjSQL.FolderSQL(DB).GetFolderInfoByID(Folderid);
                bool success = DB.Add_AccessFolderPremession(_user .UserID, Folder__);
                if(success )
                {
                    MessageBox.Show("تم منح الصلاحية بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
