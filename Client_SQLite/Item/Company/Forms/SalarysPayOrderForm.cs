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
    public partial class SalarysPayOrderForm : Form
    {
        MenuItem OpenEmployeePayOrder_MenuItem;
        MenuItem SetEmployeePayOrder_MenuItem;
        MenuItem UpdateEmployeePayOrder_MenuItem;
        MenuItem UnSetEmployeePayOrder_MenuItem;
        MenuItem EmployeePayOrder_AddPayOUT;
        MenuItem OpenEmployeePage_MenuItem;

        DatabaseInterface DB;
        SalarysPayOrder _SalarysPayOrder;
        List<SalarysPayOrderEmployeeReport> SalarysPayOrderEmployeeReportList = new List<SalarysPayOrderEmployeeReport>();
        List<SalarysPayOrderReport_Currency> SalarysPayOrderEmployeeReport_Currency_List = new List<SalarysPayOrderReport_Currency>();

        AccountingObj.Objects.Currency ReferenceCurrency;

        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public SalarysPayOrderForm(DatabaseInterface db,int year,int month)
        {
            this.Changed_ = false ;
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            textBoxSalaryPayOrderID.Text = "-";
            textBoxSalaryPayOrderDate.Text = "-";
            DateTime datetime = new DateTime(year, month, 1);
            dateTimePickerExecutedate.Value = datetime;
            ReferenceCurrency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
        }
        //public SalarysPayOrderForm(DatabaseInterface db)
        //{
        //    this.Changed_ = false ;
        //    DB = db;
        //    InitializeComponent();
        //    textBoxSalaryPayOrderID.Text = "-";
        //    textBoxSalaryPayOrderDate.Text = "-";
        //    DateTime datetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //    dateTimePickerExecutedate.Value = datetime;
        //    buttonSave.Name = "buttonAdd";
        //    buttonSave.Text = "انشاء";
        //}
        public SalarysPayOrderForm(DatabaseInterface db, SalarysPayOrder SalarysPayOrder_, bool Edit)
        {
            if (SalarysPayOrder_ == null) throw new Exception( "SalarysPayOrder is Null" );
            this.Changed_ = false ;
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _SalarysPayOrder = SalarysPayOrder_;
            ReferenceCurrency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency();

            LoadForm(Edit);

        }
        public async void LoadForm(bool Edit)
        {
            groupBoxFilter.Visible = true;
            comboBoxfilterbysalaryset.SelectedIndex = 0;
            OpenEmployeePayOrder_MenuItem = new System.Windows.Forms.MenuItem("استعراض تفاصيل", Open_EmployeePayOrder_MenuItem_Click);
            SetEmployeePayOrder_MenuItem = new System.Windows.Forms.MenuItem("اضافة الراتب ", Set_EmployeePayOrder_MenuItem_Click);
            UpdateEmployeePayOrder_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_EmployeePayOrder_MenuItem_Click);
            UnSetEmployeePayOrder_MenuItem = new System.Windows.Forms.MenuItem("الغاءالراتب", UnSet_EmployeePayOrder_MenuItem_Click);
            OpenEmployeePage_MenuItem = new System.Windows.Forms.MenuItem("فتح صفحة البيانات المالية للموظف", Open_EmployeePage_MenuItem_Click);
            EmployeePayOrder_AddPayOUT = new System.Windows.Forms.MenuItem("اضافة دفعة تابعة لامر الصرف هذا", EmployeePayOrder_AddPayOUT_Click);

            SetComboBoxFilterByJobStat();
            textBoxSalaryPayOrderID.Text = _SalarysPayOrder.SalarysPayOrderID.ToString();
            textBoxSalaryPayOrderDate.Text = _SalarysPayOrder.OrderDate.ToShortDateString();
            DateTime datetime = new DateTime(_SalarysPayOrder.ExecuteYear , _SalarysPayOrder.ExecuteMonth, 1);
            dateTimePickerExecutedate.Value = datetime;
            textboxNotes.Text = _SalarysPayOrder.Notes;
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "حفظ";
            
            SalarysPayOrderEmployeeReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Report_List(_SalarysPayOrder);
            SalarysPayOrderEmployeeReport_Currency_List = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Currency_Report_List(_SalarysPayOrder.SalarysPayOrderID);
            RefreshEmployeePayOrderList();
            if (Edit)
            {
                textBoxSalaryPayOrderID.ReadOnly = false;
                textBoxSalaryPayOrderDate.ReadOnly = false;
                textboxNotes.ReadOnly  = false ;
                dateTimePickerExecutedate.Enabled = true;
                buttonSave.Visible = true;
            }
            else
            {
                textBoxSalaryPayOrderID.ReadOnly = true ;
                textBoxSalaryPayOrderDate.ReadOnly = true ;
                textboxNotes.ReadOnly = true;
                dateTimePickerExecutedate.Enabled = false;
                buttonSave.Visible = false;
            }
        }

        private void EmployeePayOrder_AddPayOUT_Click(object sender, EventArgs e)
        {
            try
            {
                uint sid = Convert.ToUInt32(listViewEmployeesSalaries.SelectedItems[0].SubItems[3].Name);
                EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                AccountingObj.Forms.PayOUTForm PayOUTForm_ = new AccountingObj.Forms.PayOUTForm(DB, DateTime.Now, EmployeePayOrder_);
                PayOUTForm_.ShowDialog();
                if (PayOUTForm_.DialogResult == DialogResult.OK)
                {
                    SalarysPayOrderEmployeeReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Report_List(_SalarysPayOrder);
                    RefreshEmployeePayOrderList();

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("EmployeePayOrder_AddPayOUT_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public  void SetComboBoxFilterByJobStat()
        {
            comboBoxfilterbyemployeestate.Items.Add(new ComboboxItem("الكل", 0));
            comboBoxfilterbyemployeestate.Items.Add(new ComboboxItem(" المباشرين فقط ", 1));
            comboBoxfilterbyemployeestate.Items.Add(new ComboboxItem("  غير المباشرين فقط", 2));
            comboBoxfilterbyemployeestate.SelectedIndex = 1;

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {


            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                   SalarysPayOrder SalarysPayOrder_ = new SalarysPayOrderSQL(DB).Add_SalarysPayOrder 
                      (dateTimePickerExecutedate .Value.Year,dateTimePickerExecutedate.Value.Month,textboxNotes.Text );
                    if (SalarysPayOrder_!= null )
                    {
                        _SalarysPayOrder = SalarysPayOrder_;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
          
                        LoadForm(true );

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
                    if (_SalarysPayOrder  != null)
                    {
                        bool success = new SalarysPayOrderSQL(DB).Update_SalarysPayOrder 
            (_SalarysPayOrder.SalarysPayOrderID ,dateTimePickerExecutedate.Value.Year, dateTimePickerExecutedate.Value.Month, textboxNotes.Text);
                        if (success == true)
                        {
                            _SalarysPayOrder = new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(_SalarysPayOrder.SalarysPayOrderID);
                            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Changed_ = true;
                            LoadForm(true );

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
        #region Employees
        private void UnSet_EmployeePayOrder_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewEmployeesSalaries.SelectedItems[0].SubItems[3].Name);
                bool success = new EmployeePayOrderSQL (DB).Delete_PayOrder  (sid);
                if (success)
                {
                    this.Changed_ = true;
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SalarysPayOrderEmployeeReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Report_List(_SalarysPayOrder);
                    SalarysPayOrderEmployeeReport_Currency_List = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Currency_Report_List(_SalarysPayOrder.SalarysPayOrderID);
                    RefreshEmployeePayOrderList();

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_EmployeePayOrder_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Edit_EmployeePayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewEmployeesSalaries.SelectedItems.Count > 0)
                {
                    uint sid = Convert.ToUInt32(listViewEmployeesSalaries.SelectedItems[0].SubItems[3].Name);
                    EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                    EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, true);
                    EmployeePayOrderForm_.ShowDialog();
                    if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                    {
                        this.Changed_ = true;
                        SalarysPayOrderEmployeeReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Report_List(_SalarysPayOrder);
                        SalarysPayOrderEmployeeReport_Currency_List = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Currency_Report_List(_SalarysPayOrder.SalarysPayOrderID);
                        RefreshEmployeePayOrderList();
                    }
                    EmployeePayOrderForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Open_EmployeePayOrder_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewEmployeesSalaries.SelectedItems.Count > 0)
                {
                     
                    uint sid = Convert.ToUInt32(listViewEmployeesSalaries.SelectedItems[0].SubItems[3].Name);
                    EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                    EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, false );
                    EmployeePayOrderForm_.ShowDialog();
                    if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                    {
                        this.Changed_ = true;
                        SalarysPayOrderEmployeeReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Report_List(_SalarysPayOrder);
                        SalarysPayOrderEmployeeReport_Currency_List = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Currency_Report_List(_SalarysPayOrder.SalarysPayOrderID);
                        RefreshEmployeePayOrderList();

                    }
                    EmployeePayOrderForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Open_EmployeePage_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewEmployeesSalaries.SelectedItems.Count > 0)
                {
                    uint sid = Convert.ToUInt32(listViewEmployeesSalaries.SelectedItems[0].Name);
                    Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(sid);

                    EmployeeSalaryClauseForm EmployeeSalaryClauseForm_ = new EmployeeSalaryClauseForm(DB, Employee_, true );
                    EmployeeSalaryClauseForm_.ShowDialog();
                    if (EmployeeSalaryClauseForm_.Changed  ==true )
                    {
                        this.Changed_ = true;
                        SalarysPayOrderEmployeeReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Report_List(_SalarysPayOrder);
                        SalarysPayOrderEmployeeReport_Currency_List = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Currency_Report_List(_SalarysPayOrder.SalarysPayOrderID);
                        RefreshEmployeePayOrderList();
                    }
                    EmployeeSalaryClauseForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Set_EmployeePayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint sid = Convert.ToUInt32(listViewEmployeesSalaries.SelectedItems[0].Name);
                Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(sid);
                EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, _SalarysPayOrder ,Employee_);
                EmployeePayOrderForm_.ShowDialog();
                if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;
                    SalarysPayOrderEmployeeReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Report_List(_SalarysPayOrder);
                    SalarysPayOrderEmployeeReport_Currency_List = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderEmployees_Currency_Report_List(_SalarysPayOrder.SalarysPayOrderID);
                    RefreshEmployeePayOrderList();
                }
                EmployeePayOrderForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void RefreshEmployeePayOrderList()
        {
            listViewEmployeesSalaries.Items.Clear();

            for (int i = 0; i < SalarysPayOrderEmployeeReportList.Count; i++)
            {

                if (comboBoxfilterbysalaryset.SelectedIndex == 1 && SalarysPayOrderEmployeeReportList[i].PayedSalaryValue != null) continue;
                if (comboBoxfilterbysalaryset.SelectedIndex == 2 && SalarysPayOrderEmployeeReportList[i].PayedSalaryValue == null) continue;

                if (comboBoxfilterbyemployeestate.SelectedIndex == 1 && 
                    !(SalarysPayOrderEmployeeReportList[i].EmployeeStateCode == EmployeesReport.EMPLOYEE_ON_WORK_ON_EMPLOYEEMENT)
                    &&
                   !( SalarysPayOrderEmployeeReportList[i].EmployeeStateCode == EmployeesReport.EMPLOYEE_ON_WORK_NO_EMPLOYEEMENT)
                    ) continue;
                if (comboBoxfilterbyemployeestate.SelectedIndex == 2 && (
                    SalarysPayOrderEmployeeReportList[i].EmployeeStateCode == EmployeesReport.EMPLOYEE_ON_WORK_ON_EMPLOYEEMENT
                    ||
                    SalarysPayOrderEmployeeReportList[i].EmployeeStateCode == EmployeesReport.EMPLOYEE_ON_WORK_NO_EMPLOYEEMENT)
                    )
                    continue;

                ListViewItem ListViewItem__ = new ListViewItem(SalarysPayOrderEmployeeReportList[i].EmployeeID .ToString ());
                ListViewItem__.Name = SalarysPayOrderEmployeeReportList[i].EmployeeID.ToString();
                ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].EmployeeName);
                ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].ExcpectedSalary);
                if (SalarysPayOrderEmployeeReportList[i].PayOrderID != null)
                {

                    ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].PayedSalaryValue.ToString()
                       + "  " + SalarysPayOrderEmployeeReportList[i].PayedSalaryCurrecny.CurrencySymbol);
                    ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].Paid );
                    ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].Remain .ToString ());
                    ListViewItem__.SubItems[3].Name = SalarysPayOrderEmployeeReportList[i].PayOrderID.ToString();
                    double realvalue = Convert.ToDouble(SalarysPayOrderEmployeeReportList[i].PayedSalaryValue)
                        / Convert.ToDouble(SalarysPayOrderEmployeeReportList[i].PayedSalaryExchangeRate);
                    ListViewItem__.SubItems.Add(System.Math.Round(realvalue, 2).ToString() + "  " + ReferenceCurrency.CurrencySymbol);
                    ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].PaysRealValue.ToString () + "  " + ReferenceCurrency.CurrencySymbol);
                    ListViewItem__.BackColor = Color.LimeGreen;

                }
                else
                {

                    ListViewItem__.SubItems.Add("-");
                    ListViewItem__.SubItems.Add("-");
                    ListViewItem__.SubItems.Add("-");
                    ListViewItem__.SubItems.Add("-");
                    ListViewItem__.SubItems.Add("-");
                    ListViewItem__.BackColor = Color.Orange ;
                }
                ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].JobState);
                ListViewItem__.SubItems.Add(SalarysPayOrderEmployeeReportList[i].EmployeeMentState);
       
                listViewEmployeesSalaries.Items.Add(ListViewItem__);

            }
            FillReport();
        }
        private async  void FillReport()
        {
            double real_value_all = 0;
            string value_all = "";

            //List<SalarysPayOrderEmployeeReport> SalarysPayOrderEmployeeReportListnotnull = SalarysPayOrderEmployeeReportList.Where(x => x.PayedSalaryValue != null).ToList();
            //List<Currency> ByCurrency = SalarysPayOrderEmployeeReportListnotnull.Select(x => x.PayedSalaryCurrecny).Distinct().ToList();

            for (int j = 0; j < SalarysPayOrderEmployeeReport_Currency_List .Count; j++)
            {
                

                value_all += SalarysPayOrderEmployeeReport_Currency_List[j].SalarysValue  + " " + SalarysPayOrderEmployeeReport_Currency_List[j].CurrencySymbol;
                if (j != SalarysPayOrderEmployeeReport_Currency_List.Count - 1)
                    value_all += " , ";
                real_value_all += SalarysPayOrderEmployeeReport_Currency_List[j].RealSalarysValue;
            }
            if (value_all.Length == 0) value_all = "-";
            textBoxValueAll.Text = value_all;
            textBoxRealValueAll.Text = System.Math.Round(real_value_all, 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;
            List<PayOUT> PayOUTList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrder_Year_Month_PaysOut_List(dateTimePickerExecutedate .Value .Year, dateTimePickerExecutedate.Value.Month );
            textBox_PaysOUT.Text = Money_Currency.ConvertMoney_CurrencyList_TOString
                (Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
            textBox_Real_PaysOUT.Text = Math.Round(PayOUTList.Sum(x => x.Value / x.ExchangeRate),3).ToString () + " " + ReferenceCurrency.CurrencySymbol;
        }
        private void listViewEmployeesSalaries_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewEmployeesSalaries .SelectedItems.Count > 0)
            {
                uint sid = Convert.ToUInt32(listViewEmployeesSalaries .SelectedItems [0].Name);
                List<SalarysPayOrderEmployeeReport> selecteditem =
                    SalarysPayOrderEmployeeReportList.Where(x => x.EmployeeID == sid).ToList();

                if (selecteditem[0].PayedSalaryValue == null)
                    SetEmployeePayOrder_MenuItem.PerformClick();
                else
                    OpenEmployeePayOrder_MenuItem .PerformClick();


            }
        }
        private void listViewEmployeesSalaries_MouseDown(object sender, MouseEventArgs e)
        {
            listViewEmployeesSalaries.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewEmployeesSalaries.Items)
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
                    uint sid = Convert.ToUInt32(listitem.Name);
                    List<SalarysPayOrderEmployeeReport> selecteditem =
                        SalarysPayOrderEmployeeReportList.Where(x => x.EmployeeID == sid).ToList();
                    try
                    {
                        List<MenuItem> mi1 = new List<MenuItem>() ;
                        if (selecteditem[0].PayOrderID ==null )
                            mi1.Add ( SetEmployeePayOrder_MenuItem   );
                        else
                            mi1.AddRange (new MenuItem[] { OpenEmployeePayOrder_MenuItem ,UpdateEmployeePayOrder_MenuItem
                                ,UnSetEmployeePayOrder_MenuItem,EmployeePayOrder_AddPayOUT} );
                        mi1.AddRange(new MenuItem[] {new MenuItem ("-"),OpenEmployeePage_MenuItem  });
                        listViewEmployeesSalaries.ContextMenu = new ContextMenu(mi1.ToArray());
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("listViewEmployeesSalaries_MouseDown:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }


            }

        }
        #endregion

        private void SalarysPayOrderForm_Load(object sender, EventArgs e)
        {
            this.comboBoxfilterbyemployeestate.SelectedIndexChanged += new System.EventHandler(this.comboBoxfilter_SelectedIndexChanged);
            this.comboBoxfilterbysalaryset.SelectedIndexChanged += new System.EventHandler(this.comboBoxfilter_SelectedIndexChanged);

        }

     

        private void comboBoxfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEmployeePayOrderList();
        }
    }
}

