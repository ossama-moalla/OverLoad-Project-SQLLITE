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
    public partial class CompanySalarysForm : Form
    {
        MenuItem OpenSalarysPayOrder_MenuItem;
        MenuItem CreateSalarysPayOrder_MenuItem;
        MenuItem UpdateSalarysPayOrder_MenuItem;
        MenuItem DeleteSalarysPayOrder_MenuItem;
        DatabaseInterface DB;
        AccountingObj.Objects.Currency ReferenceCurrency;
        public CompanySalarysForm(DatabaseInterface db)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            ReferenceCurrency = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetReferenceCurrency();

            OpenSalarysPayOrder_MenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل أمر الصرف ", OpenSalarysPayOrder_MenuItem_Click);
            CreateSalarysPayOrder_MenuItem = new System.Windows.Forms.MenuItem("صرف راوتب هذا الشهر ", CreateSalarysPayOrder_MenuItem_Click);
            UpdateSalarysPayOrder_MenuItem = new System.Windows.Forms.MenuItem("تعديل أمر الصرف", UpdateSalarysPayOrder_MenuItem_Click);
            DeleteSalarysPayOrder_MenuItem = new System.Windows.Forms.MenuItem("حذف أمر الصرف ", DeleteSalarysPayOrder_MenuItem_Click);
            textBoxYear.Text = DateTime.Now.Year.ToString();
            GetYearSalaris(DateTime.Now.Year);
            this.textBoxYear.TextChanged += new System.EventHandler(this.textBox1_TextChanged);


        }
        public async  void GetYearSalaris(int year)
        {
            try
            {
                List<SalarysPayOrderMonthReport> SalarysPayOrderMonthReportList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrderMonthReport_List_In_Year(year);
                listViewYearSalaries.Items.Clear();

                for (int i = 0; i < SalarysPayOrderMonthReportList.Count; i++)
                {
                    ListViewItem ListViewItem__ = new ListViewItem(SalarysPayOrderMonthReportList[i].Year.ToString());
                    ListViewItem__.Name = SalarysPayOrderMonthReportList[i].Year.ToString("D4") +
                        SalarysPayOrderMonthReportList[i].MonthNO.ToString("D2") +
                        SalarysPayOrderMonthReportList[i].SalarysPayOrderID;
                    ListViewItem__.SubItems.Add(SalarysPayOrderMonthReportList[i].MonthNO.ToString());
                    ListViewItem__.SubItems.Add(SalarysPayOrderMonthReportList[i].MonthName);
                    ListViewItem__.SubItems.Add(SalarysPayOrderMonthReportList[i].SalarysPayOrderID);
                    ListViewItem__.SubItems.Add(SalarysPayOrderMonthReportList[i].SalarysPayOrderDate);
                    ListViewItem__.SubItems.Add(SalarysPayOrderMonthReportList[i].EmployeesCount);
                    ListViewItem__.SubItems.Add(SalarysPayOrderMonthReportList[i].MoneyAmount);


                    listViewYearSalaries.Items.Add(ListViewItem__);

                }
                FillReport(year);

            }
            catch (Exception ee)
            {
                MessageBox.Show("GetYearSalaris:" + ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error  );
            }
          
        }
        private async void FillReport(int year)
        {
            try
            {
                List<SalarysPayOrderReport_Currency> SalarysPayOrderYearReport_Currency_List =
               new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrder_Year_Report_Currency_List(year);

                double real_value_all = 0;
                string value_all = "";

                //List<SalarysPayOrderEmployeeReport> SalarysPayOrderEmployeeReportList_notnull = SalarysPayOrderEmployeeReportList_.Where(x => x.PayedSalaryValue != null).ToList();
                //List<Currency> ByCurrency = SalarysPayOrderEmployeeReportList_notnull.Select(x => x.PayedSalaryCurrecny).Distinct().ToList();

                for (int j = 0; j < SalarysPayOrderYearReport_Currency_List.Count; j++)
                {
                    value_all += " " + SalarysPayOrderYearReport_Currency_List[j].SalarysValue + SalarysPayOrderYearReport_Currency_List[j].CurrencySymbol ;
                    if (j != SalarysPayOrderYearReport_Currency_List.Count - 1)
                        value_all += " , ";
                    real_value_all += SalarysPayOrderYearReport_Currency_List[j].RealSalarysValue;
                }
                textBoxValueAll.Text = value_all;
                textBoxRealValueAll.Text = System.Math.Round(real_value_all, 2).ToString() + " " + ReferenceCurrency.CurrencySymbol;
                List<AccountingObj.Objects.PayOUT> PayOUTList = new SalarysPayOrderSQL(DB).Get_GetSalarysPayOrder_Year_PaysOut_List(Convert .ToInt32 (textBoxYear .Text ));
                textBox_PaysOUT.Text = AccountingObj.Objects.Money_Currency.ConvertMoney_CurrencyList_TOString
                    (AccountingObj.Objects.Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
                textBox_Real_PaysOUT.Text = Math.Round(PayOUTList.Sum(x => x.Value / x.ExchangeRate), 3).ToString() + " " + ReferenceCurrency.CurrencySymbol;
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillReport:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private async  void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxYear.Text.Length == 4)
                {
                    try
                    {
                        int year = Convert.ToInt32(textBoxYear.Text);
                        GetYearSalaris(year);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBox1_TextChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void ButtonLeftRight_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                bool left;
                if (b.Name == "ButtonLeft") left = true;
                else left = false;

                if (left)
                {
                    int year = Convert.ToInt32(textBoxYear.Text);
                    textBoxYear.Text = (year - 1).ToString();
                }
                else
                {
                    int year = Convert.ToInt32(textBoxYear.Text);
                    textBoxYear.Text = (year + 1).ToString();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("ButtonLeftRight_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

         
        }
        private void DeleteSalarysPayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewYearSalaries.SelectedItems.Count > 0)
                {

                    int year = Convert.ToInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(0, 4));
                    int month = Convert.ToInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(4, 2));

                    DialogResult dd = MessageBox.Show("هل انت متاكد من حذف أمر صرف الرواتب لسنة؟  :" + year.ToString() + " شهر " + month.ToString(), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;
                    uint salaryspayorderid = Convert.ToUInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(6));
                    bool success = new SalarysPayOrderSQL(DB).Delete_SalarysPayOrder(salaryspayorderid);

                    if (success)
                    {
                        try
                        {

                            int year1 = Convert.ToInt32(textBoxYear.Text);
                            GetYearSalaris(year1);
                        }
                        catch
                        {
                            MessageBox.Show("سنة غير صالحة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }



                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteSalarysPayOrder_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void UpdateSalarysPayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewYearSalaries.SelectedItems.Count > 0)
                {

                        uint salaryspayorderid = Convert.ToUInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(6));
                        SalarysPayOrder SalarysPayOrder_ = new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(salaryspayorderid);
                        SalarysPayOrderForm SalarysPayOrderForm_ = new SalarysPayOrderForm(DB, SalarysPayOrder_, true);
                        SalarysPayOrderForm_.ShowDialog();
                        if (SalarysPayOrderForm_.Changed)
                        {
                            int year = Convert.ToInt32(textBoxYear.Text);
                            GetYearSalaris(year);
                        }



                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("UpdateSalarysPayOrder_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void OpenSalarysPayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewYearSalaries.SelectedItems.Count > 0)
                {

                        uint salaryspayorderid = Convert.ToUInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(6));
                        SalarysPayOrder SalarysPayOrder_ = new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(salaryspayorderid);
                        SalarysPayOrderForm SalarysPayOrderForm_ = new SalarysPayOrderForm(DB, SalarysPayOrder_, false);
                        SalarysPayOrderForm_.ShowDialog();
                        if (SalarysPayOrderForm_.Changed)
                        {
                            int year = Convert.ToInt32(textBoxYear.Text);
                            GetYearSalaris(year);
                        }



                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenSalarysPayOrder_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void CreateSalarysPayOrder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewYearSalaries.SelectedItems.Count > 0)
                {

                        int year = Convert.ToInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(0, 4));
                        int month = Convert.ToInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(4, 2));
                        SalarysPayOrderForm SalarysPayOrderForm_ = new SalarysPayOrderForm(DB, year, month);
                        SalarysPayOrderForm_.ShowDialog();
                        if (SalarysPayOrderForm_.Changed)
                        {
                            int year1 = Convert.ToInt32(textBoxYear.Text);
                            GetYearSalaris(year1);
                        }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateSalarysPayOrder_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void listViewYearSalaries_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && listViewYearSalaries.SelectedItems.Count > 0)
                {
                    try
                    {

                        uint salaryspayorderid = Convert.ToUInt32(listViewYearSalaries.SelectedItems[0].Name.Substring(5));
                        OpenSalarysPayOrder_MenuItem.PerformClick();
                    }
                    catch
                    {
                        CreateSalarysPayOrder_MenuItem.PerformClick();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewYearSalaries_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
 
        private void listViewYearSalaries_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    List<MenuItem> MenuItemList = new List<MenuItem>();
                    listViewYearSalaries.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();
                    foreach (ListViewItem EmployeeMent1 in listViewYearSalaries.Items)
                    {
                        if (EmployeeMent1.Bounds.Contains(new Point(e.X, e.Y)))
                        {
                            match = true;
                            listitem = EmployeeMent1;
                            break;
                        }
                    }
                    if (match)
                    {
                        MenuItem[] mi1;
                        try
                        {

                            uint salaryspayorderid = Convert.ToUInt32(listitem.Name.Substring(5));
                            mi1 = new MenuItem[] {OpenSalarysPayOrder_MenuItem   ,UpdateSalarysPayOrder_MenuItem ,DeleteSalarysPayOrder_MenuItem
                              };
                        }
                        catch
                        {
                            mi1 = new MenuItem[] { CreateSalarysPayOrder_MenuItem };
                        }

                        MenuItemList.AddRange(mi1);
                        listViewYearSalaries.ContextMenu = new ContextMenu(MenuItemList.ToArray());


                    }
                    else listViewYearSalaries.ContextMenu = null;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewYearSalaries_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
      

    }
}

