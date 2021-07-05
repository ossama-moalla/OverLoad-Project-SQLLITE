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
    public partial class EndJobStartForm : Form
    {
        DatabaseInterface DB;
        Document  _JobStart;
        Document _EndJobStart;
        public EndJobStartForm(DatabaseInterface db, Document JobStart_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _JobStart = JobStart_;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            textboxEmployeeID.Text = _JobStart._Employee.EmployeeID.ToString();
            textBoxName.Text = _JobStart._Employee.EmployeeName;
            textBoxJobStartID.Text = _JobStart.DocumentID .ToString();
            textBoxJobStartDate.Text = _JobStart.DocumentDate .ToShortDateString();
            textBoxJobStartExecuteDate.Text = _JobStart.ExecuteDate.ToShortDateString();

        }
        public EndJobStartForm(DatabaseInterface db, Document EndJobStart_,bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _EndJobStart = EndJobStart_;
            _JobStart = _EndJobStart.TargetDocument;
            LoadForm(Edit);
        }
        public async void LoadForm(bool Edit)
        {
            textboxEmployeeID.Text = _JobStart._Employee.EmployeeID.ToString();
            textBoxName.Text = _JobStart._Employee.EmployeeName;
            textBoxJobStartID.Text = _JobStart.DocumentID.ToString();
            textBoxJobStartDate.Text = _JobStart.DocumentDate.ToShortDateString();
            textBoxJobStartExecuteDate.Text = _JobStart.ExecuteDate.ToShortDateString();

            dateTimePickerExecutedate.Value = _EndJobStart .ExecuteDate;
            textboxNotes.Text = _EndJobStart .Details;
            textBoxDocumentID.Text = _EndJobStart .DocumentID.ToString();
            textBoxDocumentDate.Text = _EndJobStart.DocumentDate.ToShortDateString();
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
            if(dateTimePickerExecutedate  .Value <_JobStart .ExecuteDate   )
            {
                MessageBox.Show("أمر انهاء المباشرة يجب ان يكون تاريخه بعد تاريخ أمر المباشرة", "", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    DialogResult dd = MessageBox.Show("انهاء المباشرة الحالية يعني انهاء جميع اوامر التكليف, استمرار؟  :", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;
                    bool success = new DocumentSQL(DB).Create_Document
                  (_JobStart ._Employee.EmployeeID, Document.ENDJOBSTART_DOCUMENT, dateTimePickerExecutedate.Value, _JobStart 
                  , null, textboxNotes.Text);
                    if (success == true)
                    {

                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else MessageBox.Show(":تعذر انشاء امر المباشرة ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    if (_EndJobStart  != null)
                    {
                        bool success = new DocumentSQL(DB).Update_Document
                       (_EndJobStart .DocumentID, dateTimePickerExecutedate.Value, textboxNotes.Text);
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
