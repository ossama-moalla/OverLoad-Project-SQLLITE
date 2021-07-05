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
    public partial class JobStartForm : Form
    {

        DatabaseInterface DB;
        Document _JobStart;
        Employee _Employee;
        public JobStartForm(DatabaseInterface db, Employee Employee_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _Employee = Employee_;
            textboxEmployeeID.Text = _Employee.EmployeeID.ToString();
            textBoxName.Text = _Employee.EmployeeName;
            textBoxDocumentID.Text ="-";
            textBoxDocumentDate.Text = "-";
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        public JobStartForm(DatabaseInterface db, Document JobStart_,bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _JobStart = JobStart_;
            LoadForm(Edit);

        }
        public async void LoadForm(bool Edit)
        {
            textboxEmployeeID.Text = _JobStart._Employee.EmployeeID.ToString();
            textBoxName.Text = _JobStart._Employee.EmployeeName;
            dateTimePickerExecutedate.Value = _JobStart.ExecuteDate;
            textboxNotes.Text = _JobStart.Details;
            textBoxDocumentID.Text = _JobStart.DocumentID.ToString();
            textBoxDocumentDate.Text = _JobStart.DocumentDate.ToShortDateString();
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (Edit)
            {
                textboxNotes.ReadOnly = false;
                dateTimePickerExecutedate.Enabled = true;
                buttonSave.Visible = true;
            }
            else
            {
                textboxNotes.ReadOnly = true;
                dateTimePickerExecutedate.Enabled = false;
                buttonSave.Visible = false;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    bool success = new DocumentSQL(DB).Create_Document 
                      (_Employee.EmployeeID,Document.JOBSTART_DOCUMENT, dateTimePickerExecutedate.Value,null,null, textboxNotes.Text);
                    if (success == true )
                    {
    
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else MessageBox.Show(":تعذر انشاء امر المباشرة " , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر انشاء امر المباشرة " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_Employee != null)
                    {
                        bool success = new DocumentSQL(DB).Update_Document  
                      (_JobStart .DocumentID  , dateTimePickerExecutedate.Value, textboxNotes.Text);
                        if (success == true)
                        {

                            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
