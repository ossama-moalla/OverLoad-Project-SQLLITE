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
    public partial class CompanyPayOrdersForm : Form
    {
        MenuItem OpenPayOrder_MenuItem;
        MenuItem AddPayOrder_MenuItem;
        MenuItem UpdatePayOrder_MenuItem;
        MenuItem DeletePayOrder_MenuItem;
        MenuItem AddPayOUT_PayOrder_MenuItem;
        MenuItem OpenSourceSalarysPayOrder_MenuItem;

        DatabaseInterface DB;
        List<PayOrderReport> PayOrderReportList = new List<PayOrderReport>();
        Currency RefCurrency;
        AccountingObj.Objects.Currency ReferenceCurrency;
        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public CompanyPayOrdersForm(DatabaseInterface db)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            ReferenceCurrency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency();
            OpenPayOrder_MenuItem = new System.Windows.Forms.MenuItem("استعراض أمر الصرف", Open_EmployeePayOrder_MenuItem_Click);
            AddPayOrder_MenuItem = new System.Windows.Forms.MenuItem("انشاء أمر صرف", Create_EmployeePayOrder_MenuItem_Click);
            UpdatePayOrder_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_EmployeePayOrder_MenuItem_Click);
            DeletePayOrder_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_EmployeePayOrder_MenuItem_Click);
            AddPayOUT_PayOrder_MenuItem = new System.Windows.Forms.MenuItem("اضافة دفعة تابعة لأامر الصرف", AddPayOUT_PayOrder_MenuItem_Click);

            RefCurrency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency(); 
            OpenSourceSalarysPayOrder_MenuItem = new System.Windows.Forms.MenuItem("استعراض أمر الصرف", Open_EmployeePayOrder_MenuItem_Click);

            PayOrderReportList = new PayOrderReportSQL(DB).Get_Company_PayOrdersReportList();
            RefreshPayOrderList(PayOrderReportList);

        }
        #region EmployeePayOrders
        private void AddPayOUT_PayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewEmployeePayOrders.SelectedItems.Count == 1)
            {
                uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring (1));
                Company.Objects.EmployeePayOrder EmployeePayOrder_ = new Company.CompanySQL.EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                AccountingObj.Forms.PayOUTForm PayOUTForm_ = new AccountingObj.Forms.PayOUTForm(DB, DateTime .Now , EmployeePayOrder_);
                PayOUTForm_.ShowDialog();
                if (PayOUTForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;
                    PayOrderReportList = new PayOrderReportSQL(DB).Get_Company_PayOrdersReportList();
                    RefreshPayOrderList(PayOrderReportList);
                }
            }




        }
        private void Delete_EmployeePayOrder_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewEmployeePayOrders.SelectedItems.Count > 0)
                {
                    //if (listViewEmployeePayOrders.SelectedItems[0].Name.Substring(0, 1) != "P") return;
                    DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;
                    uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                    bool success = new EmployeePayOrderSQL(DB).Delete_PayOrder(sid);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        PayOrderReportList = new PayOrderReportSQL(DB).Get_Company_PayOrdersReportList();
                        RefreshPayOrderList(PayOrderReportList);

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
                    //if (listViewEmployeePayOrders.SelectedItems[0].Name.Substring(0, 1) != "P") return;

                    uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                    EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                    EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, true);
                    EmployeePayOrderForm_.ShowDialog();
                    if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                    {
                        this.Changed_ = true;
                        PayOrderReportList = new PayOrderReportSQL(DB).Get_Company_PayOrdersReportList();
                        RefreshPayOrderList (PayOrderReportList);
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
                    //if (listViewEmployeePayOrders.SelectedItems[0].Name.Substring(0, 1) == "P")
                    //{
                        uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                        EmployeePayOrder EmployeePayOrder_ = new EmployeePayOrderSQL(DB).GetPayOrder_INFO_BYID(sid);
                        EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, EmployeePayOrder_, true);
                        EmployeePayOrderForm_.ShowDialog();
                        if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                        {
                            this.Changed_ = true;
                            PayOrderReportList = new PayOrderReportSQL(DB).Get_Company_PayOrdersReportList();
                        RefreshPayOrderList (PayOrderReportList);
                        }
                    //}
                    //else
                    //{
                    //    uint sid = Convert.ToUInt32(listViewEmployeePayOrders.SelectedItems[0].Name.Substring(1));
                    //    SalarysPayOrder SalarysPayOrder_ = new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(sid);
                    //    SalarysPayOrderForm SalarysPayOrderForm_ = new SalarysPayOrderForm(DB, SalarysPayOrder_, false);
                    //    SalarysPayOrderForm_.ShowDialog();
                    //    if (SalarysPayOrderForm_.Changed)
                    //    {
                    //        this.Changed_ = true;
                    //        PayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
                    //        RefreshEmployeePayOrderList(PayOrderReportList);
                    //    }
                    //}



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
                SelecObjectForm SelecObjectForm_ = new SelecObjectForm("اختر موظف");
                List<Company.Objects.EmployeesReport> EmployeesReportList = new Company.CompanySQL.CompanyReportSQL(DB).GetEmployeesReportList();
                Company.Objects.EmployeesReport.InitializeListView(ref SelecObjectForm_._listView);
                Company.Objects.EmployeesReport.RefreshEmployeesReportList(ref SelecObjectForm_._listView, EmployeesReportList);
                SelecObjectForm_.adjustcolumns = f => Company.Objects.EmployeesReport.AdjustlistViewEmployeesColumnsWidth(ref SelecObjectForm_._listView);
                DialogResult dd= SelecObjectForm_.ShowDialog();
                if (dd != DialogResult.OK) return;
                Employee _Employee = new EmployeeSQL(DB).GetEmployeeInforBYID(SelecObjectForm_.ReturnID );
                EmployeePayOrderForm EmployeePayOrderForm_ = new EmployeePayOrderForm(DB, _Employee);
                EmployeePayOrderForm_.ShowDialog();
                if (EmployeePayOrderForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;
                    PayOrderReportList = new PayOrderReportSQL(DB).Get_Employee_PayOrdersReportList(_Employee.EmployeeID);
                    RefreshPayOrderList(PayOrderReportList);
                }
                EmployeePayOrderForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_EmployeePayOrder_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void AdjustlistViewPayOrders_ColumnsWidth()
        {
            try
            {
                listViewEmployeePayOrders.Columns[0].Width = 150;//type
                listViewEmployeePayOrders.Columns[1].Width = 100;//id
                listViewEmployeePayOrders.Columns[2].Width = 150;//date
                listViewEmployeePayOrders.Columns[3].Width = 150;//employee
                listViewEmployeePayOrders.Columns[4].Width = (listViewEmployeePayOrders.Width - 1005);//desc

                listViewEmployeePayOrders.Columns[5].Width = 175;//value
                listViewEmployeePayOrders.Columns[6].Width = 125;//exchangerate
                listViewEmployeePayOrders.Columns[7].Width = 150;//paysvalue





            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewPayOrders_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public async void RefreshPayOrderList(List<PayOrderReport> PayOrderReportList_)
        {
            listViewEmployeePayOrders.Items.Clear();

            for (int i = 0; i < PayOrderReportList_.Count; i++)
            {
                //MessageBox.Show(PayOrderReportList_[i].PayOrderType.ToString ());
                ListViewItem ListViewItem__ = new ListViewItem(PayOrderReportList_[i].PayOrderType ? "أمر صرف مستقل" : "أمر صرف راتب");
                if (PayOrderReportList_[i].PayOrderType == PayOrderReport.TYPE_PAY_ODER)
                {
                    ListViewItem__.Name = "P" + PayOrderReportList_[i].PayOrderID.ToString();
                    ListViewItem__.BackColor = Color.LightGreen;
                }
                else
                {
                    ListViewItem__.Name = "S" + PayOrderReportList_[i].PayOrderID.ToString();
                    ListViewItem__.BackColor = Color.LightGoldenrodYellow;
                }
                ListViewItem__.SubItems.Add(PayOrderReportList_[i].PayOrderID.ToString());
                ListViewItem__.SubItems.Add(PayOrderReportList_[i].PayOrderDate.ToShortDateString());
                ListViewItem__.SubItems.Add(PayOrderReportList_[i].EmployeeName);
                ListViewItem__.SubItems.Add(PayOrderReportList_[i].PayOrderDesc);
                ListViewItem__.SubItems.Add(PayOrderReportList_[i].Value.ToString() +
                   PayOrderReportList_[i]._Currency.CurrencySymbol);
                ListViewItem__.SubItems.Add(PayOrderReportList_[i].ExchangeRate.ToString());
                ListViewItem__.SubItems.Add(PayOrderReportList_[i].PaysAmount);

                listViewEmployeePayOrders.Items.Add(ListViewItem__);

            }
            FillReport(PayOrderReportList_);
        }
        private async void FillReport(List<PayOrderReport> PayOrderReportList_)
        {
            AllPayOrdersReport AllPayOrdersReport_= new PayOrderReportSQL(DB).Get_AllPayOrdersReport(); 
            if(AllPayOrdersReport_!=null )
            {
                textBoxValueAll.Text = AllPayOrdersReport_.Payorders_Value;
                textBoxPaid.Text = AllPayOrdersReport_.Payorders_PaysAmount;
                textBoxRealValueAll.Text = AllPayOrdersReport_.Payorders_RealValue.ToString ()+" "+RefCurrency .CurrencySymbol ;
                textBoxRealPaid.Text = AllPayOrdersReport_.Payorders_Pays_RealValue.ToString() + " " + RefCurrency.CurrencySymbol; ;
                textBox_remain.Text = AllPayOrdersReport_.Payorders_PaysRemain;


            }
            else
            {
                textBoxValueAll.Text = "  -  ";
                textBoxPaid.Text = "  -  ";
                textBoxRealValueAll.Text = "  -  "  + RefCurrency.CurrencySymbol; ;
                textBoxRealPaid.Text = "  -  " + RefCurrency.CurrencySymbol; ;
                textBox_remain.Text = "  -  ";


            }
            //    List<uint  > ByCurrency = PayOrderReportList_.Select(x => x._Currency .CurrencyID ).Distinct().ToList();

            //    for (int j = 0; j < ByCurrency.Count; j++)
            //    {

            //        List<PayOrderReport> TempPayOrderReport = PayOrderReportList_.Where(x => x._Currency .CurrencyID == ByCurrency[j]).ToList();
            //        double value = 0;
            //        for (int k=0; k<TempPayOrderReport .Count;k++)
            //        {
            //            value += TempPayOrderReport[k].Value  ;
            //            real_value_all += TempPayOrderReport[k].Value/ TempPayOrderReport[k].ExchangeRate ;
            //        }
            //        value_all += value + ByCurrency[j]+" ";
            //    }
            //    if (value_all.Length == 0)
            //        textBoxValueAll.Text = "      ---    ";
            //    else;
            //    textBoxValueAll.Text = value_all;
            //    textBoxRealValueAll.Text = System.Math.Round(real_value_all, 2).ToString() + " " + ReferenceCurrency.CurrencyName;
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
                    if (listitem.Name.Substring(0, 1) == "P")
                    {
                        MenuItem[] mi1 = new MenuItem[] { OpenPayOrder_MenuItem  ,UpdatePayOrder_MenuItem
                        ,DeletePayOrder_MenuItem,new MenuItem("-"),AddPayOUT_PayOrder_MenuItem,new MenuItem("-")  ,AddPayOrder_MenuItem  };
                        listViewEmployeePayOrders.ContextMenu = new ContextMenu(mi1.ToArray());

                    }
                    else
                    {
                        MenuItem[] mi1 = new MenuItem[] { OpenSourceSalarysPayOrder_MenuItem, AddPayOUT_PayOrder_MenuItem };
                        listViewEmployeePayOrders.ContextMenu = new ContextMenu(mi1.ToArray());

                    }

                }
                else
                {

                    MenuItem[] mi1 = new MenuItem[] { AddPayOrder_MenuItem };
                    listViewEmployeePayOrders.ContextMenu = new ContextMenu(mi1.ToArray());
                }

            }

        }
        private void listViewEmployeePayOrders_Resize(object sender, EventArgs e)
        {
            AdjustlistViewPayOrders_ColumnsWidth();
        }
        #endregion

        private void CompanyPayOrdersForm_Load(object sender, EventArgs e)
        {

            AdjustlistViewPayOrders_ColumnsWidth();
            this.listViewEmployeePayOrders.Resize += new System.EventHandler(this.listViewEmployeePayOrders_Resize);
        }
    }
}

