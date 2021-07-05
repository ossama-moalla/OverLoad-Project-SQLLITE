using OverLoad_Client.Company.CompanySQL;
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

namespace OverLoad_Client.Company.Forms
{
    public partial class CertificateForm : Form
    {

        DatabaseInterface DB;
        EmployeeCertificate  _EmployeeCertificate;
        Employee _Employee;
        public CertificateForm(DatabaseInterface db, Employee Employee_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _Employee = Employee_;
            textboxEmployeeID.Text = _Employee.EmployeeID.ToString();
            textBoxName.Text = _Employee.EmployeeName;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        public CertificateForm(DatabaseInterface db, EmployeeCertificate EmployeeCertificate_, bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _EmployeeCertificate = EmployeeCertificate_;

            textboxEmployeeID.Text = _EmployeeCertificate._Employee.EmployeeID.ToString();
            textBoxName.Text = _EmployeeCertificate._Employee.EmployeeName;
            dateTimePickerStartDate.Value = _EmployeeCertificate.StartDate ;
            dateTimePickerEndDate.Value = _EmployeeCertificate.EndDate  ;
            textBoxCertificate.Text = _EmployeeCertificate.CertificatesDesc   ;
            textBoxUniversity.Text = _EmployeeCertificate.University ;
            textboxNotes.Text = _EmployeeCertificate.Notes;
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (Edit)
            {
                textBoxUniversity.ReadOnly = false;
                textboxNotes.ReadOnly = false;
                dateTimePickerEndDate.Enabled = true;
                dateTimePickerStartDate.Enabled = true;
                textBoxCertificate.ReadOnly = false;
                buttonSave.Visible = true;
            }
            else
            {
                textBoxUniversity.ReadOnly = true ;
                textboxNotes.ReadOnly = true ;
                dateTimePickerEndDate.Enabled = false ;
                dateTimePickerStartDate.Enabled = false ;
                textBoxCertificate.ReadOnly = true ;
                buttonSave.Visible = false ;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    bool success = new EmployeeCertificateSQL(DB).Add_Certificate
                      (_Employee.EmployeeID, textBoxCertificate.Text,textBoxUniversity.Text , dateTimePickerStartDate.Value
                      , dateTimePickerEndDate.Value , textboxNotes.Text );
                    if (success == true )
                    {
    
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else MessageBox.Show("لم يتم الاضافة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر االخبرة " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_Employee != null)
                    {
                        bool success = new EmployeeCertificateSQL(DB).Update_Certificate  
                     (_Employee.EmployeeID,_EmployeeCertificate.CertificatesDesc, textBoxCertificate.Text , textBoxCertificate.Text, dateTimePickerStartDate.Value
                     , dateTimePickerEndDate.Value, textboxNotes.Text);
                        if (success == true)
                        {

                            MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();

                        }
                        else MessageBox.Show("لم يتم الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذرالحفظ  " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }
    }
}
