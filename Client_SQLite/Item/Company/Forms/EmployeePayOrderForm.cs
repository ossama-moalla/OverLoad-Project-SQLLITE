using OverLoad_Client.AccountingObj.Objects;
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
    public partial class EmployeePayOrderForm : OverLoad_Form
    {
        DatabaseInterface DB;
        EmployeePayOrder _EmployeePayOrder;
        SalarysPayOrder _SalarysPayOrder;
        Employee _Employee;
        public EmployeePayOrderForm(DatabaseInterface db, Employee Employee_)
        {

            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _Employee = Employee_;
            textboxEmployeeID.Text = _Employee.EmployeeID.ToString();
            textBoxName.Text = _Employee.EmployeeName;
           ProgramGeneralMethods .FillComboBoxCurrency (ref comboBoxPayOrderCurrency
               ,DB, _Employee.SalaryCurrency);
            textBoxPayOrderExchangeRate.Text = _Employee.SalaryCurrency .ExchangeRate.ToString();
            textBoxPayOrderID.Text = "-";
            dateTimePickerPayOrderDate.Value  = DateTime.Now; ;

            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        public EmployeePayOrderForm(DatabaseInterface db, SalarysPayOrder SalarysPayOrder_, Employee Employee_)
        {

            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _Employee = Employee_;
            _SalarysPayOrder = SalarysPayOrder_;
            textboxEmployeeID.Text = _Employee.EmployeeID.ToString();
            textBoxName.Text = _Employee.EmployeeName;
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxPayOrderCurrency
                , DB, _Employee.SalaryCurrency);
            textBoxPayOrderExchangeRate.Text = _Employee.SalaryCurrency.ExchangeRate.ToString();
            textBoxPayOrderID.Text = "-";
            dateTimePickerPayOrderDate.Value = DateTime.Now;
            textBoxPayOrderDesc.Text = "صرف راتب شهر :" + _SalarysPayOrder.ExecuteMonth.ToString()
                + " سنة" + _SalarysPayOrder.ExecuteYear.ToString();
            textBoxPayOrderDesc.ReadOnly = true;
            textBoxPayOrderValue.Text = new SalarysPayOrderSQL(DB).Get_Expected_Salary_ForEmployee_IN_Month(_Employee.EmployeeID, SalarysPayOrder_.ExecuteYear,
                SalarysPayOrder_.ExecuteMonth).ToString();
                buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        public EmployeePayOrderForm(DatabaseInterface db, EmployeePayOrder EmployeePayOrder_, bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _EmployeePayOrder = EmployeePayOrder_;
            _Employee = _EmployeePayOrder._Employee;
            _SalarysPayOrder = _EmployeePayOrder._SalarysPayOrder;
            LoadForm(Edit);

        }
        public async void LoadForm(bool Edit)
        {
            textboxEmployeeID.Text = _EmployeePayOrder._Employee.EmployeeID.ToString();
            textBoxName.Text = _EmployeePayOrder._Employee.EmployeeName;
            textBoxPayOrderID.Text = _EmployeePayOrder.PayOrderID.ToString();
            dateTimePickerPayOrderDate  .Value  = _EmployeePayOrder.PayOrderDate ;
           
            textBoxPayOrderValue.Text = _EmployeePayOrder.Value.ToString();
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxPayOrderCurrency
         , DB, _EmployeePayOrder._Currency );
            textBoxPayOrderExchangeRate.Text = _EmployeePayOrder.ExchangeRate.ToString();  
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            if (Edit)
            {
                textBoxPayOrderDesc.ReadOnly = false;
                textBoxPayOrderValue.ReadOnly = false;
                textBoxPayOrderExchangeRate.ReadOnly = false;
                comboBoxPayOrderCurrency .Enabled = true;

               
                buttonSave.Visible = true;
            }
            else
            {
                textBoxPayOrderDesc.ReadOnly = true ;
                textBoxPayOrderValue.ReadOnly = true ;
                textBoxPayOrderExchangeRate.ReadOnly = true ;
                comboBoxPayOrderCurrency.Enabled = false ;
                buttonSave.Visible = false;
            }
            if(_EmployeePayOrder._SalarysPayOrder  ==null )
                textBoxPayOrderDesc.Text = _EmployeePayOrder.PayOrderDesc;
            else
            {
                textBoxPayOrderDesc.Text = "صرف راتب شهر :" + _SalarysPayOrder.ExecuteMonth.ToString()
                + " سنة" + _SalarysPayOrder.ExecuteYear.ToString();
                textBoxPayOrderDesc.ReadOnly = true;
            }
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {


            double value,exchangerate;
            
            try
            {

                value = Convert.ToDouble(textBoxPayOrderValue.Text);
                exchangerate= Convert.ToDouble(textBoxPayOrderExchangeRate.Text);
                if (value <= 0 || exchangerate <=0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("سعر الصرف و قيمة امر الصرف يجب ان تكونا اكبر تماما من الصفر ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ComboboxItem item = (ComboboxItem)comboBoxPayOrderCurrency.SelectedItem;
            Currency currency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetCurrencyINFO_ByID(item.Value);

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    bool success;
                    if (_SalarysPayOrder ==null )
                     success = new EmployeePayOrderSQL(DB).Add_PayOrder
                      (dateTimePickerPayOrderDate.Value , _Employee.EmployeeID, textBoxPayOrderDesc.Text, 
                      currency,
                      exchangerate, value);
                    else
                        success = new EmployeePayOrderSQL(DB).Add__Salary_PayOrder
                          (dateTimePickerPayOrderDate.Value,  _Employee.EmployeeID,_SalarysPayOrder.SalarysPayOrderID
                          ,currency,
                          exchangerate, value);
                    if (success == true)
                    {

                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.RefreshEmployeesReportList = true;
                        this.Close();

                    }
                    else MessageBox.Show(":تعذر انشاء بند الاستقطاع  ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    if (_EmployeePayOrder != null)
                    {
                        bool success;
                        if(_EmployeePayOrder._SalarysPayOrder==null )
                         success = new EmployeePayOrderSQL(DB).Update_PayOrder 
                      (_EmployeePayOrder.PayOrderID,dateTimePickerPayOrderDate.Value , _EmployeePayOrder._Employee .EmployeeID, textBoxPayOrderDesc.Text,
                      currency,
                      exchangerate, value);
                        else
                            success = new EmployeePayOrderSQL(DB).Update_Salary_PayOrder
                        (_EmployeePayOrder.PayOrderID, dateTimePickerPayOrderDate.Value, _EmployeePayOrder._Employee.EmployeeID, 
                        currency,
                        exchangerate, value);
                        if (success == true)
                        {

                            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.RefreshEmployeesReportList = true;
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

        private void comboBoxPayOrderCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem item = (ComboboxItem)comboBoxPayOrderCurrency .SelectedItem;
            Currency currency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetCurrencyINFO_ByID(item.Value);
            textBoxPayOrderExchangeRate.Text = currency.ExchangeRate.ToString();
            if (currency.ReferenceCurrencyID == null)
                textBoxPayOrderExchangeRate.Enabled = false;
            else
                textBoxPayOrderExchangeRate.Enabled = true;
        }
    }
}
