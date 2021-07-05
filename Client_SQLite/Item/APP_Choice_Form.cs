
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public partial class APP_Choice_Form : Form
    {
        private const uint BUY_GROUP = 1;
        private const uint SELL_GROUP = 2;
        private const uint MAINTENANCE_GROUP = 3;
        private const uint EMPLOYEE_GROUP = 4;
        private const uint ITEM_GROUP = 5;
        private const uint CONTACT_GROUP = 6;
        private const uint MONEY_BOX_GROUP = 7;
        DatabaseInterface DB;
        public APP_Choice_Form(DatabaseInterface db)
        {
            InitializeComponent();
            DB = db;
        }

        private void APP_Choice_Form_Load(object sender, EventArgs e)
        {
            if(DB.IS_Belong_To_Buy_Group (DB.__User .UserID ))
            {
                ListViewItem ListViewItem_ = new ListViewItem("ادارة المشتريات");
                ListViewItem_.Name = BUY_GROUP.ToString();
                listView1.Items.Add(ListViewItem_);
            }
            if (DB.IS_Belong_To_Sell_Group(DB.__User.UserID))
            {
                ListViewItem ListViewItem_ = new ListViewItem("ادارة المبيعات");
                ListViewItem_.Name = SELL_GROUP.ToString();
                listView1.Items.Add(ListViewItem_);
            }
            if (DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))
            {
                ListViewItem ListViewItem_ = new ListViewItem("ادارة الصيانة");
                ListViewItem_.Name = MAINTENANCE_GROUP.ToString();
                listView1.Items.Add(ListViewItem_);
            }
            if (DB.IS_Belong_To_Employee_Group(DB.__User.UserID))
            {
                ListViewItem ListViewItem_ = new ListViewItem("ادارة الموظفين");
                ListViewItem_.Name = EMPLOYEE_GROUP.ToString();
                listView1.Items.Add(ListViewItem_);
            }
            if (DB.IS_Belong_To_Item_Group(DB.__User.UserID))
            {
                ListViewItem ListViewItem_ = new ListViewItem("ادارة العناصر");
                ListViewItem_.Name = ITEM_GROUP.ToString();
                listView1.Items.Add(ListViewItem_);
            }
            if (DB.IS_Belong_To_Contact_Group(DB.__User.UserID))
            {
                ListViewItem ListViewItem_ = new ListViewItem("ادارة الموردين و الزبائن");
                ListViewItem_.Name = CONTACT_GROUP .ToString();
                listView1.Items.Add(ListViewItem_);
            }
            if (DB.Get_User_Allowed_MoneyBox().Count>0)
            {
                ListViewItem ListViewItem_ = new ListViewItem("ادارة صناديق المال");
                ListViewItem_.Name = MONEY_BOX_GROUP .ToString();
                listView1.Items.Add(ListViewItem_);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    uint groupid = Convert.ToUInt32(listView1.SelectedItems[0].Name);
                    switch (groupid)
                    {
                        case (BUY_GROUP):
                            AccountingObj.Forms.MainWindow_Buy_Form MainWindow_Buy_Form_ = new AccountingObj.Forms.MainWindow_Buy_Form(DB);
                            MainWindow_Buy_Form_.Show();
                            break;
                        case (SELL_GROUP):
                            AccountingObj.Forms.MainWindow_Sell_Form MainWindow_Sell_Form_ = new AccountingObj.Forms.MainWindow_Sell_Form(DB);
                            MainWindow_Sell_Form_.Show();
                            break;
                        case (MAINTENANCE_GROUP):
                            AccountingObj.Forms.MainWindow_Maintenance_Form MainWindow_MAINTENANCE_Form_ = new AccountingObj.Forms.MainWindow_Maintenance_Form(DB);
                            MainWindow_MAINTENANCE_Form_.Show();
                            break;
                        case (EMPLOYEE_GROUP):
                            Company.Forms.CompanyManagmentForm CompanyManagmentForm_ = new Company.Forms.CompanyManagmentForm(DB);
                            CompanyManagmentForm_.Show();
                            break;
                        case (ITEM_GROUP):
                            ItemObj.Forms.User_ShowItemsForm ShowItemsForm_ = new ItemObj.Forms.User_ShowItemsForm(DB, null, ItemObj.Forms.User_ShowItemsForm.SHOW_ITEMS);
                            ShowItemsForm_.Show();
                            break;
                        case (CONTACT_GROUP):
                            Trade.Forms.TradeContact.ShowContactsForm ShowContactsForm_ = new Trade.Forms.TradeContact.ShowContactsForm(DB, false);
                            ShowContactsForm_.Show();
                            break;
                        case (MONEY_BOX_GROUP):
                            AccountingObj.Forms.MainWindow_MoneyOPR_Form MainWindow_MoneyOPR_Form_ = new AccountingObj.Forms.MainWindow_MoneyOPR_Form(DB);
                            MainWindow_MoneyOPR_Form_.Show();
                            break;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

           
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
