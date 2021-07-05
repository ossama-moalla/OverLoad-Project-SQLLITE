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
    public partial class SalaryClauseDueForm : Form
    {
        DatabaseInterface DB;
        SalaryClause _SalaryClause;
        Employee _Employee;
        public SalaryClauseDueForm(DatabaseInterface db, Employee Employee_)
        {

            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _Employee = Employee_;
            textboxEmployeeID.Text = _Employee.EmployeeID.ToString();
            textBoxName.Text = _Employee.EmployeeName;
            labelCurrency.Text = _Employee.SalaryCurrency.CurrencyName;
            textBoxClauseID.Text = "-";
            textBoxCreateDate.Text = "-";
            dateTimePickerExecutedate.Value = DateTime.Now;
            //textBoxStartMonth.Text = DateTime.Now.Month.ToString();
            //textBoxStartYear.Text = DateTime.Now.Year.ToString();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        public SalaryClauseDueForm(DatabaseInterface db, SalaryClause SalaryClause_, bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _SalaryClause = SalaryClause_;
            _Employee = _SalaryClause._Employee;
            LoadForm(Edit);

        }
        public async void LoadForm(bool Edit)
        {
            textboxEmployeeID.Text = _SalaryClause._Employee.EmployeeID.ToString();
            textBoxName.Text = _SalaryClause._Employee.EmployeeName;
            textBoxClauseID.Text = _SalaryClause.SalaryClauseID.ToString();
            textBoxCreateDate.Text = _SalaryClause.CreateDate.ToShortDateString();
            textBoxClauseDesc.Text = _SalaryClause.SalaryClauseDesc;
            textBoxClauseValue.Text = _SalaryClause.Value.ToString();
            dateTimePickerExecutedate.Value = _SalaryClause.ExecuteDate;
            //textBoxStartYear.Text = _SalaryClause.StartYear.ToString();
            textboxNotes.Text = _SalaryClause.Notes;
            if (_SalaryClause.MonthsCount == null)
            {
                checkBoxEnableMonthCount.Checked = false;
                textBoxMonthCount.Text = "-";
            }
            else
            {
                checkBoxEnableMonthCount.Checked = true;
                textBoxMonthCount.Text = _SalaryClause.MonthsCount.ToString();
            }
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (Edit)
            {
                textBoxClauseDesc.ReadOnly = false;
                textBoxClauseValue.ReadOnly = false;
                dateTimePickerExecutedate.Enabled = true ;
                //textBoxStartYear.ReadOnly = false;
                checkBoxEnableMonthCount.Enabled = true;
                textBoxMonthCount.ReadOnly = false;
                textboxNotes.ReadOnly = false;
                buttonSave.Visible = true;
            }
            else
            {
                textBoxClauseDesc.ReadOnly = true;
                textBoxClauseValue.ReadOnly = true;
                dateTimePickerExecutedate.Enabled = false ;
                //textBoxStartYear.ReadOnly = true;
                checkBoxEnableMonthCount.Enabled = false;
                textBoxMonthCount.ReadOnly = true;
                textboxNotes.ReadOnly = true;
                buttonSave.Visible = false;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            DateTime executedate =new DateTime ( dateTimePickerExecutedate.Value.Year ,
                dateTimePickerExecutedate.Value.Month ,1);

            double value;
            uint? monthcount;
            if (checkBoxEnableMonthCount.Checked == false)
                monthcount = null;
            else
            {
                try
                {

                    monthcount = Convert.ToUInt32(textBoxMonthCount.Text);
                    if (monthcount <= 0) throw new Exception();

                }
                catch
                {
                    MessageBox.Show("عدد مرات الصرف يجب ان يكون رقم اكبر تماما من الصفر ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            try
            {

                value = Convert.ToDouble(textBoxClauseValue.Text);
                if (value < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("قيمة البند يجب ان تكون اكبر تماما من الصفر ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    bool success = new SalaryClauseSQL(DB).Add_SalaryClause
                      (_Employee, textBoxClauseDesc.Text, SalaryClause.TYPE_DUE,
                      executedate, monthcount, value, textboxNotes.Text);
                    if (success == true)
                    {

                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    //else MessageBox.Show(":تعذر انشاء بند الاستحقاق  ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر انشاء بند الاستقطاع  " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_Employee != null)
                    {
                        bool success = new SalaryClauseSQL(DB).Update_SalaryClause
                      (_SalaryClause.SalaryClauseID, textBoxClauseDesc.Text,
                      executedate, monthcount, value, textboxNotes.Text);
                        if (success == true)
                        {

                            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();

                        }
                        //else MessageBox.Show("لم يتم الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذرالحفظ  " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }

        private void checkBoxEnableMonthCount_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableMonthCount.Checked)
                textBoxMonthCount.Enabled = true;
            else
            {
                textBoxMonthCount.Enabled = false;
            }
        }
    }
}
