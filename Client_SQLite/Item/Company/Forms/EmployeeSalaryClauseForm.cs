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

    public partial class EmployeeSalaryClauseForm : Form
    {
        MenuItem OpenSalaryClause_MenuItem;
        MenuItem AddSalaryClauseDue_MenuItem;
        MenuItem AddSalaryClauseDeduction_MenuItem;
        MenuItem UpdateSalaryClause_MenuItem;
        MenuItem DeleteSalaryClause_MenuItem;

        MenuItem OpenPayOrder_MenuItem;
        MenuItem AddPayOrder_MenuItem;
        MenuItem UpdatePayOrder_MenuItem;
        MenuItem DeletePayOrder_MenuItem;

        MenuItem OpenSourceSalarysPayOrder_MenuItem;

        List<SalaryClause> SalaryClauseList = new List<SalaryClause>();
        List<PayOrderReport> EmployeePayOrderReportList = new List<PayOrderReport>(); AccountingObj.Objects.Currency ReferenceCurrency;
        Employee _Employee;
        DatabaseInterface DB;
        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public EmployeeSalaryClauseForm(DatabaseInterface db,Employee Employee_,bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            Changed_ = false;
            _Employee = Employee_;
            label5.Text = "المستحقات و الاستقطاعات المالية للموظف: " + _Employee.EmployeeName;
            label6.Text = "أوامر الصرف التابعة للموظف: " + _Employee.EmployeeName;
            ReferenceCurrency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency();

            OpenSalaryClause_MenuItem = new System.Windows.Forms.MenuItem("استعراض البند", Open_SalaryClause_MenuItem_Click);
            AddSalaryClauseDue_MenuItem = new System.Windows.Forms.MenuItem("اضافة استحقاق", Create_SalaryClauseDue_MenuItem_Click);
            AddSalaryClauseDeduction_MenuItem = new System.Windows.Forms.MenuItem("اضافة استقطاع", Create_SalaryClauseDeduction_MenuItem_Click);

            UpdateSalaryClause_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_SalaryClause_MenuItem_Click);
            DeleteSalaryClause_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_SalaryClause_MenuItem_Click);

            OpenPayOrder_MenuItem  = new System.Windows.Forms.MenuItem("استعراض أمر الصرف", Open_EmployeePayOrder_MenuItem_Click);
            AddPayOrder_MenuItem = new System.Windows.Forms.MenuItem("انشاء أمر صرف", Create_EmployeePayOrder_MenuItem_Click);
            UpdatePayOrder_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_EmployeePayOrder_MenuItem_Click);
            DeletePayOrder_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_EmployeePayOrder_MenuItem_Click);

            OpenSourceSalarysPayOrder_MenuItem = new System.Windows.Forms.MenuItem("استعراض أمر الصرف", Open_EmployeePayOrder_MenuItem_Click);
            if (Edit )
            {
                this.listViewSalaryClauses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSalaryClauses_MouseDoubleClick);
                this.listViewSalaryClauses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewSalaryClauses_MouseDown);

                this.listViewEmployeePayOrders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewEmployeePayOrders_MouseDoubleClick);
                this.listViewEmployeePayOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewEmployeePayOrders_MouseDown);
            }
            SalaryClauseList = new SalaryClauseSQL(DB).Get_SalaryClause_List(_Employee);
            RefreshSalaryClauseList(SalaryClauseList);
            EmployeePayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
            RefreshEmployeePayOrderList(EmployeePayOrderReportList);

        }
        #region EmployeeSalaryClause
        private void Delete_SalaryClause_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;

                bool success = new SalaryClauseSQL(DB).Delete_SalaryClause(Convert .ToUInt32 ( listViewSalaryClauses.SelectedItems[0].Name) );
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Changed_ = true;
                    SalaryClauseList = new SalaryClauseSQL(DB).Get_SalaryClause_List(_Employee);
                    RefreshSalaryClauseList(SalaryClauseList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_SalaryClause_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Edit_SalaryClause_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSalaryClauses.SelectedItems.Count > 0)
                {
                    bool Changed = false;
                    SalaryClause SalaryClause_ = new SalaryClauseSQL(DB).Get_SalaryClause_Info_BYID(Convert.ToUInt32(listViewSalaryClauses.SelectedItems[0].Name));
                    if(SalaryClause_.ClauseType ==SalaryClause.TYPE_Deduction)
                    {
                        SalaryClauseDeductionForm SalaryClauseDeductionForm_ = new SalaryClauseDeductionForm(DB, SalaryClause_, true);
                        SalaryClauseDeductionForm_.ShowDialog();
                        if (SalaryClauseDeductionForm_.DialogResult == DialogResult.OK)
                            Changed = true;

                    }
                    else
                    {
                        SalaryClauseDueForm SalaryClauseDueForm_ = new SalaryClauseDueForm(DB, SalaryClause_, true);
                        SalaryClauseDueForm_.ShowDialog();
                        if (SalaryClauseDueForm_.DialogResult == DialogResult.OK)
                            Changed = true;
                    }
                    if (Changed)
                    {
                        this.Changed_ = true;
                        SalaryClauseList = new SalaryClauseSQL(DB).Get_SalaryClause_List(_Employee);
                        RefreshSalaryClauseList(SalaryClauseList);
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_SalaryClause_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Open_SalaryClause_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewSalaryClauses.SelectedItems.Count > 0)
                {


                    bool Changed = false;
                    SalaryClause SalaryClause_ = new SalaryClauseSQL(DB).Get_SalaryClause_Info_BYID(Convert.ToUInt32(listViewSalaryClauses.SelectedItems[0].Name));
                    if (SalaryClause_.ClauseType == SalaryClause.TYPE_Deduction)
                    {
                        SalaryClauseDeductionForm SalaryClauseDeductionForm_ = new SalaryClauseDeductionForm(DB, SalaryClause_, false );
                        SalaryClauseDeductionForm_.ShowDialog();
                        if (SalaryClauseDeductionForm_.DialogResult == DialogResult.OK)
                            Changed = true;

                    }
                    else
                    {
                        SalaryClauseDueForm SalaryClauseDueForm_ = new SalaryClauseDueForm(DB, SalaryClause_, false );
                        SalaryClauseDueForm_.ShowDialog();
                        if (SalaryClauseDueForm_.DialogResult == DialogResult.OK)
                            Changed = true;
                    }
                    if (Changed)
                    {
                        this.Changed_ = true;
                        SalaryClauseList = new SalaryClauseSQL(DB).Get_SalaryClause_List(_Employee);
                        RefreshSalaryClauseList(SalaryClauseList);
                    }

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_SalaryClause_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_SalaryClauseDue_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SalaryClauseDueForm SalaryClauseDueForm_ = new SalaryClauseDueForm(DB, _Employee);
                SalaryClauseDueForm_.ShowDialog();
                if (SalaryClauseDueForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;
                    SalaryClauseList = new SalaryClauseSQL(DB).Get_SalaryClause_List(_Employee);
                    RefreshSalaryClauseList(SalaryClauseList);
                }
                SalaryClauseDueForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_SalaryClauseDue_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_SalaryClauseDeduction_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SalaryClauseDeductionForm SalaryClauseDeductionForm_ = new SalaryClauseDeductionForm(DB, _Employee);
                SalaryClauseDeductionForm_.ShowDialog();
                if (SalaryClauseDeductionForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;
                    SalaryClauseList = new SalaryClauseSQL(DB).Get_SalaryClause_List(_Employee);
                    RefreshSalaryClauseList(SalaryClauseList);
                }
                SalaryClauseDeductionForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_SalaryClauseDue_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void AdjustlistViewSalaryClauses_ColumnsWidth()
        {
            try
            {
                listViewSalaryClauses.Columns[0].Width = 150;//date
                listViewSalaryClauses.Columns[1].Width = 150;//type

                listViewSalaryClauses.Columns[2].Width = listViewSalaryClauses.Width -905;//desc
                listViewSalaryClauses.Columns[3].Width = 200;//value
                listViewSalaryClauses.Columns[5].Width = 200;//executedate
                listViewSalaryClauses.Columns[4].Width = 200;//enddate
        
  



            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewDocuments_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public async void RefreshSalaryClauseList(List<SalaryClause> SalaryClauseList_)
        {
            listViewSalaryClauses.Items.Clear();

            for (int i = 0; i < SalaryClauseList_.Count; i++)
            {
                ListViewItem ListViewItem__ = new ListViewItem(SalaryClauseList_[i].CreateDate.ToShortDateString());
                ListViewItem__.Name = SalaryClauseList_[i].SalaryClauseID .ToString ();
                ListViewItem__.SubItems.Add(SalaryClauseList_[i].ClauseType == SalaryClause.TYPE_DUE ? "بند استحقاق" : "بند استقطاع");
                ListViewItem__.SubItems.Add(SalaryClauseList_[i].SalaryClauseDesc );
                ListViewItem__.SubItems.Add(SalaryClauseList_[i].Value.ToString() + " " + _Employee.SalaryCurrency.CurrencySymbol);
                ListViewItem__.SubItems.Add(SalaryClauseList_[i].ExecuteDate .ToString   ("yyyy - MM"));
                if(SalaryClauseList_[i].MonthsCount!=null )
                    ListViewItem__.SubItems.Add(SalaryClauseList_[i].MonthsCount.ToString ()+" شهور ");
                else
                    ListViewItem__.SubItems.Add("يصرف دائما");


                if (SalaryClauseList_[i].ClauseType == SalaryClause.TYPE_DUE)
                    ListViewItem__.BackColor = Color.LimeGreen;
                else
                    ListViewItem__.BackColor = Color.Orange ;
                listViewSalaryClauses.Items.Add(ListViewItem__);

            }
            FillSalaryClauseInMonth(SalaryClauseList_);
        }
        private void listViewSalaryClauses_Resize(object sender, EventArgs e)
        {
            AdjustlistViewSalaryClauses_ColumnsWidth();
        }
        private void listViewSalaryClauses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewSalaryClauses.SelectedItems.Count > 0)
            {
                OpenSalaryClause_MenuItem.PerformClick();
            }
        }
        private void listViewSalaryClauses_MouseDown(object sender, MouseEventArgs e)
        {
            listViewSalaryClauses.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewSalaryClauses.Items)
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

                    MenuItem[] mi1 = new MenuItem[] { OpenSalaryClause_MenuItem ,UpdateSalaryClause_MenuItem
                        ,DeleteSalaryClause_MenuItem,new MenuItem("-") ,AddSalaryClauseDue_MenuItem,AddSalaryClauseDeduction_MenuItem  };
                    listViewSalaryClauses.ContextMenu = new ContextMenu(mi1.ToArray());
                }
                else
                {

                    MenuItem[] mi1 = new MenuItem[] {  AddSalaryClauseDue_MenuItem ,AddSalaryClauseDeduction_MenuItem };
                    listViewSalaryClauses.ContextMenu = new ContextMenu(mi1.ToArray());
                }

            }

        }

        #endregion
        #region Due_Dudetion
        public async  void FillSalaryClauseInMonth (List < SalaryClause > SalaryClauseList_)
        {
                listViewDue.Items.Clear();
            listViewDeduction.Items.Clear();
            DateTime targetmonth = new DateTime(dateTimePickerYearMonth .Value.Year,
dateTimePickerYearMonth.Value.Month, 1);

            List<SalaryClause> SalaryClauseDueMonthList = SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_DUE
            && x.ExecuteDate<= targetmonth
            &&x.MonthsCount ==null ).ToList();
            SalaryClauseDueMonthList.AddRange(SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_DUE
            && x.ExecuteDate.AddMonths(Convert.ToInt32(x.MonthsCount)) >targetmonth
             && x.ExecuteDate <= targetmonth
            && x.MonthsCount != null
            ).ToList());
            double DueValue = 0;
            for (int i = 0; i < SalaryClauseDueMonthList.Count; i++)
                {
                    ListViewItem ListViewItem__ = new ListViewItem(SalaryClauseDueMonthList[i].CreateDate .ToShortDateString ());
                    ListViewItem__.Name = SalaryClauseDueMonthList[i].SalaryClauseID.ToString();
                    ListViewItem__.SubItems.Add(SalaryClauseDueMonthList[i].SalaryClauseDesc);
                    ListViewItem__.SubItems.Add(SalaryClauseDueMonthList[i].Value.ToString() + " " + _Employee.SalaryCurrency.CurrencySymbol);
                ListViewItem__.BackColor = Color.LimeGreen;
                DueValue += SalaryClauseDueMonthList[i].Value;
                listViewDue.Items.Add(ListViewItem__);

                }

            List<SalaryClause> SalaryClauseDeductionMonthList = SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
         && x.ExecuteDate <= targetmonth
         && x.MonthsCount == null).ToList();
            SalaryClauseDeductionMonthList.AddRange(SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
            && x.ExecuteDate.AddMonths(Convert.ToInt32(x.MonthsCount)) > targetmonth
             && x.ExecuteDate <= targetmonth
            && x.MonthsCount != null
            ).ToList());
            double DeductionValue = 0;
            for (int i = 0; i < SalaryClauseDeductionMonthList.Count; i++)
            {
                ListViewItem ListViewItem__ = new ListViewItem(SalaryClauseDeductionMonthList[i].CreateDate.ToShortDateString());
                ListViewItem__.Name = SalaryClauseDeductionMonthList[i].SalaryClauseID.ToString();
                ListViewItem__.SubItems.Add(SalaryClauseDeductionMonthList[i].SalaryClauseDesc);
                ListViewItem__.SubItems.Add(SalaryClauseDeductionMonthList[i].Value.ToString() + " " + _Employee.SalaryCurrency.CurrencySymbol);
                ListViewItem__.BackColor = Color.Orange;
                DeductionValue += SalaryClauseDeductionMonthList[i].Value;
                listViewDeduction.Items.Add(ListViewItem__);

            }
            labelDue.Text = DueValue.ToString() + " " + _Employee.SalaryCurrency.CurrencySymbol;
            labelDeduction.Text = DeductionValue .ToString() + " " + _Employee.SalaryCurrency.CurrencySymbol;
            labelClearSalary .Text =( DueValue- DeductionValue).ToString() + " " + _Employee.SalaryCurrency.CurrencySymbol;
            GetPaySalaryINMonth();
        }
        public async  void GetPaySalaryINMonth()
        {
            EmployeePayOrder EmployeeSalaryPayOrder_ = new EmployeePayOrderSQL(DB).GetEmployeeSalaryPayOrder_By_Month(_Employee, dateTimePickerYearMonth.Value.Year, dateTimePickerYearMonth.Value.Month);
            if (EmployeeSalaryPayOrder_ != null)
                labelPayedSalary.Text = EmployeeSalaryPayOrder_.Value + " " + EmployeeSalaryPayOrder_._Currency.CurrencySymbol;
            else
                labelPayedSalary.Text = "-";
        }
        #endregion

        #region EmployeePayOrders
        private void Delete_EmployeePayOrder_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if(listViewEmployeePayOrders.SelectedItems .Count >0)
                {
                    if (listViewEmployeePayOrders.SelectedItems[0].Name.Substring(0, 1) != "P") return;
                    DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;
                    uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                    bool success = new EmployeePayOrderSQL(DB).Delete_PayOrder(sid);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        EmployeePayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
                        RefreshEmployeePayOrderList(EmployeePayOrderReportList);

                    }
                    else
                    {
                        MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
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
                if (listViewEmployeePayOrders.SelectedItems.Count > 0)
                {
                    if (listViewEmployeePayOrders.SelectedItems[0].Name.Substring(0, 1) != "P") return;

                    uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                     EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                    EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, true);
                    EmployeePayOrderForm_.ShowDialog();
                    if (EmployeePayOrderForm_.DialogResult==DialogResult.OK)
                    {
                        this.Changed_ = true;
                        EmployeePayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
                        RefreshEmployeePayOrderList(EmployeePayOrderReportList);
                    }

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
                if (listViewEmployeePayOrders.SelectedItems.Count > 0)
                {
                    if (listViewEmployeePayOrders.SelectedItems[0].Name.Substring(0, 1) == "P")
                    {
                        uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                        EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                        EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, true);
                        EmployeePayOrderForm_.ShowDialog();
                        if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                        {
                            this.Changed_ = true;
                            EmployeePayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
                            RefreshEmployeePayOrderList(EmployeePayOrderReportList);
                        }
                    }
                    else
                    {
                        uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                        EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);

                        SalarysPayOrderForm SalarysPayOrderForm_ = new SalarysPayOrderForm(DB, EmployeePayOrder_._SalarysPayOrder, false );
                        SalarysPayOrderForm_.ShowDialog();
                        if (SalarysPayOrderForm_.Changed )
                        {
                            this.Changed_ = true;
                            EmployeePayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
                            RefreshEmployeePayOrderList(EmployeePayOrderReportList);
                        }
                    }

                   

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_EmployeePayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, _Employee);
                EmployeePayOrderForm_.ShowDialog();
                if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;
                    EmployeePayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
                    RefreshEmployeePayOrderList(EmployeePayOrderReportList);
                }
                EmployeePayOrderForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void AdjustlistViewEmployeePayOrders_ColumnsWidth()
        {
            try
            {
                listViewEmployeePayOrders.Columns[0].Width = 150;//type
                listViewEmployeePayOrders.Columns[1].Width = 100;//id
                listViewEmployeePayOrders.Columns[2].Width = 150;//date
                listViewEmployeePayOrders.Columns[3].Width = (listViewEmployeePayOrders.Width -905);//desc

                listViewEmployeePayOrders.Columns[4].Width = 100;//value
                listViewEmployeePayOrders.Columns[5].Width = 100;//currency
                listViewEmployeePayOrders.Columns[6].Width = 100;//exchangerate
                listViewEmployeePayOrders.Columns[7].Width = 200;//paysvalue





            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewEmployeePayOrders_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public async void RefreshEmployeePayOrderList(List<PayOrderReport> EmployeePayOrderReportList_)
        {
            listViewEmployeePayOrders.Items.Clear();

            for (int i = 0; i < EmployeePayOrderReportList_.Count; i++)
            {
                //MessageBox.Show(EmployeePayOrderReportList_[i].PayOrderType.ToString ());
                ListViewItem ListViewItem__ = new ListViewItem(EmployeePayOrderReportList_[i].PayOrderType ?"أمر صرف مستقل":"أمر صرف راتب");

                if (EmployeePayOrderReportList_[i].PayOrderType == PayOrderReport.TYPE_PAY_ODER)
                {
                    ListViewItem__.Name = "P" + EmployeePayOrderReportList_[i].PayOrderID.ToString();
                    ListViewItem__.BackColor = Color.LightGreen;
                }
                else
                {
                    ListViewItem__.Name = "S" + EmployeePayOrderReportList_[i].PayOrderID.ToString();
                    ListViewItem__.BackColor = Color.LightGoldenrodYellow;
                }
                ListViewItem__.SubItems.Add(EmployeePayOrderReportList_[i].PayOrderID .ToString ());
                ListViewItem__.SubItems.Add(EmployeePayOrderReportList_[i].PayOrderDate.ToShortDateString ());
                ListViewItem__.SubItems.Add(EmployeePayOrderReportList_[i].PayOrderDesc );
                ListViewItem__.SubItems.Add(EmployeePayOrderReportList_[i].Value .ToString ());
                ListViewItem__.SubItems.Add(EmployeePayOrderReportList_[i]._Currency .CurrencySymbol);
                ListViewItem__.SubItems.Add(EmployeePayOrderReportList_[i].ExchangeRate .ToString());
                ListViewItem__.SubItems.Add(EmployeePayOrderReportList_[i].PaysAmount );
      
                listViewEmployeePayOrders.Items.Add(ListViewItem__);

            }
            FillReport(EmployeePayOrderReportList_);
        }
        private async void FillReport(List<PayOrderReport> PayOrderReportList_)
        {

            List<AccountingObj.Objects.Money_Currency> Money_CurrencyList =
                AccountingObj.Objects.Money_Currency.Get_Money_Currency_List_From_PayOrderReport(PayOrderReportList_);


            textBoxValueAll.Text = AccountingObj.Objects.Money_Currency
                .ConvertMoney_CurrencyList_TOString (Money_CurrencyList);
            textBoxRealValueAll.Text = System.Math.Round(Money_CurrencyList.Sum (x=>x.Value /x.ExchangeRate ), 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;
            List<AccountingObj.Objects.PayOUT> PayOUTList = new List<AccountingObj.Objects.PayOUT>();
            for (int i=0;i< PayOrderReportList_.Count;i++)
            {

                PayOUTList.AddRange(new AccountingObj.AccountingSQL.PayOUTSQL(DB).GetPaysOUT_List(
                    new Trade.Objects.Operation (Trade .Objects .Operation .Employee_PayOrder , PayOrderReportList_[i].PayOrderID)));
            }
            textBox_PayOUT.Text = AccountingObj.Objects.Money_Currency.ConvertMoney_CurrencyList_TOString(
                AccountingObj.Objects.Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList)
                );
            textBox_RealPayOUT.Text = Math.Round(PayOUTList.Sum(x => x.Value / x.ExchangeRate), 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;

        }
        private void listViewEmployeePayOrders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewEmployeePayOrders.SelectedItems.Count > 0)
            {
                OpenPayOrder_MenuItem.PerformClick();
            }
        }
        private void listViewEmployeePayOrders_MouseDown(object sender, MouseEventArgs e)
        {
            listViewEmployeePayOrders.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewEmployeePayOrders.Items)
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
                    if(listitem.Name .Substring (0,1)=="P")
                    {
                        MenuItem[] mi1 = new MenuItem[] { OpenPayOrder_MenuItem  ,UpdatePayOrder_MenuItem
                        ,DeletePayOrder_MenuItem,new MenuItem("-") ,AddPayOrder_MenuItem  };
                        listViewEmployeePayOrders.ContextMenu = new ContextMenu(mi1.ToArray());

                    }
                    else
                    {
                        MenuItem[] mi1 = new MenuItem[] { OpenSourceSalarysPayOrder_MenuItem   };
                        listViewEmployeePayOrders.ContextMenu = new ContextMenu(mi1.ToArray());

                    }

                }
                else
                {

                    MenuItem[] mi1 = new MenuItem[] { AddPayOrder_MenuItem  };
                    listViewEmployeePayOrders.ContextMenu = new ContextMenu(mi1.ToArray());
                }

            }

        }
        private void listViewEmployeePayOrders_Resize(object sender, EventArgs e)
        {
            AdjustlistViewEmployeePayOrders_ColumnsWidth();
        }
        #endregion
        private void EmployeeSalaryClauseForm_Load(object sender, EventArgs e)
        {
            AdjustlistViewSalaryClauses_ColumnsWidth();
            AdjustlistViewEmployeePayOrders_ColumnsWidth();
            this.listViewSalaryClauses.Resize += new System.EventHandler(this.listViewSalaryClauses_Resize);
            this.listViewEmployeePayOrders.Resize += new System.EventHandler(this.listViewEmployeePayOrders_Resize);
        }

  

        private void dateTimePickerYearMonth_ValueChanged(object sender, EventArgs e)
        {
            FillSalaryClauseInMonth(SalaryClauseList);
        }
    }
}
