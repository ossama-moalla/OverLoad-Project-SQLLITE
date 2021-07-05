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
    public partial class QualificationForm : Form
    {

        DatabaseInterface DB;
        EmployeeQualification _EmployeeQualification;
        Employee _Employee;
        public QualificationForm(DatabaseInterface db, Employee Employee_)
        {
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _Employee = Employee_;
            textboxEmployeeID.Text = _Employee.EmployeeID.ToString();
            textBoxName.Text = _Employee.EmployeeName;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        public QualificationForm(DatabaseInterface db, EmployeeQualification EmployeeQualification_, bool Edit)
        {
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _EmployeeQualification = EmployeeQualification_;

            textboxEmployeeID.Text = _EmployeeQualification._Employee.EmployeeID.ToString();
            textBoxName.Text = _EmployeeQualification._Employee.EmployeeName;
            dateTimePickerStartDate.Value = _EmployeeQualification.StartDate ;
            dateTimePickerEndDate.Value = _EmployeeQualification.EndDate  ;
            textBoxQualification.Text = _EmployeeQualification.QualificationDesc  ;
            textboxNotes.Text = _EmployeeQualification.Notes;
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (Edit)
            {
                textboxNotes.ReadOnly = false;
                dateTimePickerEndDate.Enabled = true;
                dateTimePickerStartDate.Enabled = true;
                textBoxQualification.ReadOnly = false;
                buttonSave.Visible = true;
            }
            else
            {
                textboxNotes.ReadOnly = true ;
                dateTimePickerEndDate.Enabled = false ;
                dateTimePickerStartDate.Enabled = false ;
                textBoxQualification.ReadOnly = true ;
                buttonSave.Visible = false ;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    bool success = new EmployeeQualificationSQL(DB).Add_Qualification
                      (_Employee.EmployeeID, textBoxQualification.Text, dateTimePickerStartDate.Value
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
                        bool success = new EmployeeQualificationSQL(DB).Update_Qualification 
                     (_Employee.EmployeeID,_EmployeeQualification.QualificationDesc, textBoxQualification.Text, dateTimePickerStartDate.Value
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
