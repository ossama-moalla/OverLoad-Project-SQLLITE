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
    public partial class AssignForm : Form
    {

        DatabaseInterface DB;
        Document _JobStart;
        EmployeeMent _EmployeeMent;
        Document _AssignDocument;
        public AssignForm(DatabaseInterface db, Document JobStart_)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _JobStart = JobStart_;
            textboxEmployeeID.Text = _JobStart._Employee.EmployeeID.ToString();
            textBoxName.Text = _JobStart._Employee.EmployeeName;
            textBoxJobStartID.Text = _JobStart.DocumentID.ToString();
            textBoxJobStartDate.Text = _JobStart.DocumentDate.ToShortDateString();
            textBoxJobStartExecuteDate.Text = _JobStart.ExecuteDate.ToShortDateString();

            this.textBoxEmployeeMentID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxEmployeeMentID_KeyDown);
            this.textBoxEmployeeMentID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEmployeeMentID_MouseDoubleClick);
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        public AssignForm(DatabaseInterface db, Document AssignDocument_, bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _AssignDocument = AssignDocument_;
            _JobStart  = _AssignDocument.TargetDocument;
            _EmployeeMent =_AssignDocument._EmployeeMent;
            LoadForm(Edit);

        }
        public async void LoadForm(bool Edit)
        {
            textboxEmployeeID.Text = _JobStart._Employee.EmployeeID.ToString();
            textBoxName.Text = _JobStart._Employee.EmployeeName;
            textBoxJobStartID.Text = _JobStart.DocumentID.ToString();
            textBoxJobStartDate.Text = _JobStart.DocumentDate.ToShortDateString();
            textBoxJobStartExecuteDate.Text = _JobStart.ExecuteDate .ToShortDateString();
            dateTimePickerExecutedate.Value = _AssignDocument .ExecuteDate;
            textboxNotes.Text = _AssignDocument.Details;
            textBoxDocumentID.Text = _AssignDocument.DocumentID.ToString();
            textBoxDocumentDate.Text = _AssignDocument.DocumentDate.ToShortDateString();
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            LoadEmployeeMentData();
            if (Edit)
            {
                this.textBoxEmployeeMentID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxEmployeeMentID_KeyDown);
                this.textBoxEmployeeMentID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEmployeeMentID_MouseDoubleClick);
                textBoxEmployeeMentID.ReadOnly = false;
                textboxNotes.ReadOnly = false;
                dateTimePickerExecutedate.Enabled = true;
                buttonSave.Visible = true;
            }
            else
            {
                textBoxEmployeeMentID.ReadOnly = true ;
                textboxNotes.ReadOnly = true;
                dateTimePickerExecutedate.Enabled = false;
                buttonSave.Visible = false;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {


            if (dateTimePickerExecutedate.Value < _JobStart.ExecuteDate)
            {
                MessageBox.Show("أمر انهاء المباشرة يجب ان يكون تاريخه بعد تاريخ أمر المباشرة", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    bool success = new DocumentSQL(DB).Create_Document
                  (_JobStart._Employee.EmployeeID, Document.ASSIGN_DOCUMENT, dateTimePickerExecutedate.Value, _JobStart
                  , _EmployeeMent, textboxNotes.Text);
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
                    if (_AssignDocument != null)
                    {
                        bool success = new DocumentSQL(DB).Update_Document
                       (_AssignDocument.DocumentID, dateTimePickerExecutedate.Value, textboxNotes.Text);
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
        private async void LoadEmployeeMentData()
        {

            textBoxEmployeeMentID.Text = _EmployeeMent.EmployeeMentID.ToString();
            textBoxEmployeeMentName.Text = _EmployeeMent.EmployeeMentName;
            if (_EmployeeMent._Part != null)
                textBoxEmployeementPart.Text = _EmployeeMent._Part.PartName;
            else
                textBoxEmployeementPart.Text = DB.COMPANY.PartName;
        }
        private void textBoxEmployeeMentID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    uint EmployeeMentid = Convert.ToUInt32(textBoxEmployeeMentID.Text);
                    EmployeeMent EmployeeMent__ = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID(EmployeeMentid);
                    if (EmployeeMent__ != null)
                    {
                        _EmployeeMent = EmployeeMent__;
                        LoadEmployeeMentData();
                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على الوظيفة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        private void textBoxEmployeeMentID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    ShowEmployeeMentsForm ShowEmployeeMentsForm_ = new ShowEmployeeMentsForm(DB, null);

                    SelecObjectForm SelecObjectForm_ = new SelecObjectForm("اختر وظيفة");
                    List<EmployeeMent> EmployeeMentList = new EmployeeMentSQL(DB).Get_EmployeeMent_List();
                    EmployeeMent.InitializeEmployeeMentListViewColumns(ref SelecObjectForm_._listView);
                   
                    EmployeeMent.RefreshEmployeeMents(ref SelecObjectForm_._listView, EmployeeMentList);
     
                    SelecObjectForm_.adjustcolumns = f => EmployeeMent.AdjustlistViewEmployeeMentColumnsWidth(ref SelecObjectForm_._listView);

                    SelecObjectForm_.ShowDialog();
                    if (SelecObjectForm_.DialogResult == DialogResult.OK)
                    {
                        try
                        {
                            _EmployeeMent = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID(SelecObjectForm_.ReturnID);
                            LoadEmployeeMentData();
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show("Failed_To_Get_ID" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("textBoxEmployeeMentID_MouseDoubleClick" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
             
                }

            }
        }
    }
}
