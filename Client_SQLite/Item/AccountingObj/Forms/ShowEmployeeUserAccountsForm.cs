using OverLoad_Client.Company.CompanySQL;
using OverLoad_Client.Company.Forms;
using OverLoad_Client.Company.Objects;
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
    public partial class ShowEmployeeUserAccountsForm : Form
    {
        System.Windows.Forms.MenuItem AddUserMenuItem;
        System.Windows.Forms.MenuItem OpenUserMenuItem;
        System.Windows.Forms.MenuItem EditUserMenuItem;
        System.Windows.Forms.MenuItem DeleteUserMenuItem;
        System.Windows.Forms.MenuItem ResetPassword;

        System.Windows.Forms.MenuItem OpenEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem EditEmployeeMentMenuItem;


        DatabaseInterface DB;
        List<Employee_User> EmployeesUserAccountsList;
        public ShowEmployeeUserAccountsForm(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
            if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("أنت غير منضم لمجموعة المدراء, لا يمكنك فتح هذه النافذة");

            AdjustlistViewEmployeesColumnsWidth();
            InitializeMenuItems();
            EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
            RefreshEmployeesUserAccountsList();

        }
        public void InitializeMenuItems()
        {

            OpenUserMenuItem = new System.Windows.Forms.MenuItem("استعراض بيانات الحساب", OpenUser_MenuItem_Click);
            AddUserMenuItem   = new System.Windows.Forms.MenuItem("انشاء اسم مستخدم", AddUser_MenuItem_Click);
            EditUserMenuItem  = new System.Windows.Forms.MenuItem(" تعديل اسم المستخدم أو الحالة أو الصلاحيات", EditUser_MenuItem_Click);
            DeleteUserMenuItem  = new System.Windows.Forms.MenuItem("حذف المستخدم ", DeleteUser_MenuItem_Click);
            ResetPassword  = new System.Windows.Forms.MenuItem("اعادة تعيين كلمة المرور", ResetPassWord_MenuItem_Click);

            OpenEmployeeMentMenuItem = new System.Windows.Forms.MenuItem("فتح صفحة الموظف", OpenEmployee_MenuItem_Click); ;
            EditEmployeeMentMenuItem = new System.Windows.Forms.MenuItem("تعديل بيانات الموظف", EditEmployee_MenuItem_Click); ;
            
            
        }
        #region Employee
        private async void RefreshEmployeesUserAccountsList()
        {

            listViewEmployees.Items.Clear();
            for (int i = 0; i < EmployeesUserAccountsList.Count; i++)
            {
                ListViewItem ListViewItem_ = new ListViewItem();

                ListViewItem_.Name = EmployeesUserAccountsList[i]._Employee .EmployeeID.ToString();
                ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._Employee.EmployeeID.ToString());
                ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._Employee.EmployeeName);
                if (EmployeesUserAccountsList[i]._Employee.Gender == Employee.GENDER_MALE)
                    ListViewItem_.SubItems.Add("ذكر");
                else
                    ListViewItem_.SubItems.Add("انثى");
                //double age_days = (DateTime.Now - EmployeesUserAccountsList[i].BirthDate).TotalDays;
                //double  age = System.Math.Round((age_days / 365), 0);
                ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._Employee.BirthDate.ToShortDateString());
                ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._Employee.NationalID);
                ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._Employee._MaritalStatus.MaritalStatusName);
                //ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i].Mobile  );
                //ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i].Phone );
                //ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i].EmailAddress);
                //ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i].Address );
                if(EmployeesUserAccountsList[i]._user!=null )
                {
                    ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._user.UserName);
                    ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._user.adddate.ToShortDateString());

                    ListViewItem_.SubItems.Add(EmployeesUserAccountsList[i]._user.Disabled ==true ?"معطل":"ممكن");

                   if(!EmployeesUserAccountsList[i]._user.Disabled)  ListViewItem_.BackColor = Color.LimeGreen;
                   else ListViewItem_.BackColor = Color.Gold  ;
                }
                else
                {
                    ListViewItem_.SubItems.Add("  -  ");
                    ListViewItem_.SubItems.Add("  -  ");
                    ListViewItem_.SubItems.Add("  -  ");
                    ListViewItem_.BackColor = Color.Orange ;
                }
                ListViewItem_.ImageIndex = 2;
                listViewEmployees.Items.Add(ListViewItem_);

            }

        }
     
        private void EditEmployee_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewEmployees.SelectedItems.Count > 0)
                {
                    uint Employeeid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                    Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(Employeeid);
                    EmployeeForm EmployeeForm_ = new EmployeeForm(DB, Employee_, true);
                    EmployeeForm_.ShowDialog();
                    if (EmployeeForm_.Changed)
                    {
                        EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
                        RefreshEmployeesUserAccountsList();
                    }
                    EmployeeForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditEmployee_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenEmployee_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewEmployees.SelectedItems.Count > 0)
                {
                    uint Employeeid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                    Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(Employeeid);
                    EmployeeForm EmployeeForm_ = new EmployeeForm(DB, Employee_, false);
                    EmployeeForm_.ShowDialog();
                    if (EmployeeForm_.Changed)
                    {
                        EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
                        RefreshEmployeesUserAccountsList();
                    }
                    EmployeeForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenEmployee_Page_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddUser_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint sid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                List<Employee_User> list = EmployeesUserAccountsList.Where(x => x._Employee.EmployeeID == sid).ToList();
                if (list.Count > 0)
                {
                   Employee Employee_ = list[0]._Employee ;
                    EmployeeUserForm EmployeeUserForm_ = new EmployeeUserForm(DB, Employee_);
                     EmployeeUserForm_.ShowDialog();
                    if (EmployeeUserForm_.Changed )
                    {
                        EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
                        RefreshEmployeesUserAccountsList();
                    }
                }
                else
                {
                    MessageBox.Show("فشل قراءة رقم الموظف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddUser_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditUser_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                uint sid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                List<Employee_User> list = EmployeesUserAccountsList.Where(x => x._Employee.EmployeeID == sid).ToList();
                if (list.Count > 0)
                {
                    DatabaseInterface.User user = list[0]._user;
                    if (user != null)
                    {
                        EmployeeUserForm EmployeeUserForm_ = new EmployeeUserForm(DB, user, EmployeeUserForm.EditUserData_Function );
                        EmployeeUserForm_.ShowDialog();
                        if (EmployeeUserForm_.Changed)
                        {
                            EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
                            RefreshEmployeesUserAccountsList();
                        }

                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ : المستخدم غير منشا", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("فشل قراءة رقم الموظف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("EditUser_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void OpenUser_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                uint sid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                List<Employee_User> list = EmployeesUserAccountsList.Where(x => x._Employee.EmployeeID == sid).ToList();
                if (list.Count > 0)
                {
                    DatabaseInterface.User user = list[0]._user;
                    if (user != null)
                    {
                        EmployeeUserForm EmployeeUserForm_ = new EmployeeUserForm(DB, user, EmployeeUserForm.OpenUserData_Function );
                        EmployeeUserForm_.ShowDialog();
                        if (EmployeeUserForm_.Changed)
                        {
                            EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
                            RefreshEmployeesUserAccountsList();
                        }

                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ : المستخدم غير منشا", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("فشل قراءة رقم الموظف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("EditUser_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void ResetPassWord_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                uint sid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                List<Employee_User> list = EmployeesUserAccountsList.Where(x => x._Employee.EmployeeID == sid).ToList();
                if (list.Count > 0)
                {
                    DatabaseInterface.User user = list[0]._user;
                    if (user != null)
                    {
                        EmployeeUserForm EmployeeUserForm_ = new EmployeeUserForm(DB, user, EmployeeUserForm.ResetPassWord_Function);
                        EmployeeUserForm_.ShowDialog();
                        if (EmployeeUserForm_.Changed)
                        {
                            EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
                            RefreshEmployeesUserAccountsList();
                        }

                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ : المستخدم غير منشا", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("فشل قراءة رقم الموظف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("EditUser_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void DeleteUser_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من حذف المستخدم؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                List<Employee_User> list = EmployeesUserAccountsList.Where(x => x._Employee.EmployeeID == sid).ToList();
                if (list.Count > 0)
                {
                    DatabaseInterface.User user = list[0]._user;
                    if (user != null)
                    {
                        bool succs = DB.DeleteUser(sid);
                        if (succs)
                        {
                            MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            EmployeesUserAccountsList = new EmployeeSQL(DB).GetEmployeeUserAccountList();
                            RefreshEmployeesUserAccountsList();
                        }
                        else
                        {
                            MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("المستخدم محذوف بالفعل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("فشل قراءة رقم الموظف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteUser_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void listViewEmployees_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewEmployees.SelectedItems.Count > 0)
            {
                OpenEmployeeMentMenuItem .PerformClick();
            }
        }
        private void listViewEmployees_MouseDown(object sender, MouseEventArgs e)
        {
            listViewEmployees.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewEmployees.Items)
                {
                    if (item1.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        match = true;
                        listitem = item1;
                        break;
                    }
                }
                if (match)
                {
                    MenuItem[] mi1;
                    uint sid = Convert.ToUInt32(listitem.Name);
                    List<Employee_User> list = EmployeesUserAccountsList.Where(x => x._Employee.EmployeeID == sid).ToList();
                    if (list.Count > 0)
                    {
                        DatabaseInterface.User user = list[0]._user;
                        if (user != null)
                        {
                            mi1 = new MenuItem[] {OpenUserMenuItem, EditUserMenuItem ,ResetPassword,DeleteUserMenuItem
                                , new MenuItem("-"), OpenEmployeeMentMenuItem ,EditEmployeeMentMenuItem };

                        }
                        else
                        {
                            mi1 = new MenuItem[] { AddUserMenuItem
                                , new MenuItem("-"), OpenEmployeeMentMenuItem ,EditEmployeeMentMenuItem };
                        }
                        listViewEmployees.ContextMenu = new ContextMenu(mi1);
                    }
                    else
                        listViewEmployees.ContextMenu = null;



                }
                

            }

        }
        private void listViewEmployees_Resize(object sender, EventArgs e)
        {
            //MaintenanceEmployeesReport .AdjustlistViewEmployeesReportOPRColumnsWidth (ref listViewSubDiagnosticOPR);
            AdjustlistViewEmployeesColumnsWidth();
        }
        public async void AdjustlistViewEmployeesColumnsWidth()
        {
            try
            {
                listViewEmployees.Columns[0].Width = 30;//id
                listViewEmployees.Columns[1].Width = 100;//id
                listViewEmployees.Columns[2].Width = 200;//name
                listViewEmployees.Columns[3].Width = 100;//gender
                listViewEmployees.Columns[4].Width = 125;//age
                listViewEmployees.Columns[5].Width = 100;//nationalid
                listViewEmployees.Columns[6].Width = 150;//martial state
                listViewEmployees.Columns[7].Width = 200;//jobstate
                listViewEmployees.Columns[8].Width = 150;//employeement
                listViewEmployees.Columns[9].Width = 150;//employeement
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewEmployeesColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
