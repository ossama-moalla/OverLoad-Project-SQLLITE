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
    public partial class EndAssignForm : Form
    {
        DatabaseInterface DB;
        Document  _AssignDocument;
        Document _EndAssignDocument;
        public EndAssignForm(DatabaseInterface db, Document AssignDocument_)
        {
   
                DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
                _AssignDocument = AssignDocument_;
                buttonSave.Name = "buttonAdd";
                buttonSave.Text = "انشاء";
                textboxEmployeeID.Text = _AssignDocument._Employee.EmployeeID.ToString();
                textBoxName.Text = _AssignDocument._Employee.EmployeeName;
                textBoxAssignID.Text = _AssignDocument.DocumentID.ToString();
                textBoxAssignDate.Text = _AssignDocument.DocumentDate.ToShortDateString();
                textBoxAssignExecuteDate.Text = _AssignDocument.ExecuteDate.ToShortDateString();
                textBoxEmployeeMentID.Text = _AssignDocument._EmployeeMent.EmployeeMentID.ToString();
                textBoxEmployeeMentName.Text = _AssignDocument._EmployeeMent.EmployeeMentName;
                if (_AssignDocument._EmployeeMent._Part != null)
                    textBoxEmployeementPart.Text = _AssignDocument._EmployeeMent._Part.PartName;
                else
                    textBoxEmployeementPart.Text = DB.COMPANY.PartName;



        }
        public EndAssignForm(DatabaseInterface db, Document EndAssignDocument_, bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _EndAssignDocument = EndAssignDocument_;
            _AssignDocument = _EndAssignDocument.TargetDocument;
            LoadForm(Edit);
        }
        public async void LoadForm(bool Edit)
        {
            textboxEmployeeID.Text = _AssignDocument._Employee.EmployeeID.ToString();
            textBoxName.Text = _AssignDocument._Employee.EmployeeName;
            textBoxAssignID.Text = _AssignDocument.DocumentID.ToString();
            textBoxAssignDate.Text = _AssignDocument.DocumentDate.ToShortDateString();
            textBoxAssignExecuteDate.Text = _AssignDocument.ExecuteDate.ToShortDateString();

            textBoxEmployeeMentID.Text = _AssignDocument._EmployeeMent.EmployeeMentID.ToString();
            textBoxEmployeeMentName.Text = _AssignDocument ._EmployeeMent.EmployeeMentName;
            if (_AssignDocument._EmployeeMent._Part != null)
                textBoxEmployeementPart.Text = _AssignDocument._EmployeeMent._Part.PartName;
            else
                textBoxEmployeementPart.Text = DB.COMPANY.PartName;

            dateTimePickerExecutedate.Value = _EndAssignDocument.ExecuteDate;
            textboxNotes.Text = _EndAssignDocument.Details;
            textBoxDocumentID.Text = _EndAssignDocument.DocumentID.ToString();
            textBoxDocumentDate.Text = _EndAssignDocument.DocumentDate.ToShortDateString();
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
            if(dateTimePickerExecutedate  .Value <_AssignDocument  .ExecuteDate   )
            {
                MessageBox.Show("أمر انهاء التكليف يجب ان يكون تاريخه بعد تاريخ أمر التكليف", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    bool success = new DocumentSQL(DB).Create_Document
                  (_AssignDocument  ._Employee.EmployeeID, Document.ENDASSIGN_DOCUMENT , dateTimePickerExecutedate.Value, _AssignDocument  
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
                    if (_EndAssignDocument   != null)
                    {
                        bool success = new DocumentSQL(DB).Update_Document
                       (_EndAssignDocument.DocumentID, dateTimePickerExecutedate.Value, textboxNotes.Text);
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
