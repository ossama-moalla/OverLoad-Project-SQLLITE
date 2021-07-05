using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.AccountingObj.Forms
{
    public partial class Stuck_MoneyTransformOPR_OUT_Form : OverLoad_Form 
    {
        MoneyBox _MoneyBox;
        DatabaseInterface DB;
        MenuItem OpenMoneyTransFormOPR_MenuItem;
        MenuItem AddMoneyTransFormOPR_MenuItem;
        MenuItem EditMoneyTransFormOPR_MenuItem;
        MenuItem DeleteMoneyTransFormOPR_MenuItem;
        public Stuck_MoneyTransformOPR_OUT_Form(DatabaseInterface db,MoneyBox MoneyBox_)
        {
            InitializeComponent();
            _MoneyBox = MoneyBox_;
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_MoneyBox_Group(DB.__User.UserID, MoneyBox_))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة هذا الصندوق, لا يمكنك فتح هذه النافذة");

            Fill_Stuck_MoneyTransformOPR();
            OpenMoneyTransFormOPR_MenuItem = new MenuItem("استعراض", OpenMoneyTransFormOPR_MenuItem_Click);
            AddMoneyTransFormOPR_MenuItem = new MenuItem("انشاء عملية تحويل", AddMoneyTransFormOPR_MenuItem_Click);
            EditMoneyTransFormOPR_MenuItem = new MenuItem("تعديل", EditMoneyTransFormOPR_MenuItem_Click);
            DeleteMoneyTransFormOPR_MenuItem = new MenuItem("حذف", DeleteMoneyTransFormOPR_MenuItem_Click);

        }

        private void DeleteMoneyTransFormOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint moneytransformoprid = Convert.ToUInt32(listViewOperation.SelectedItems[0].Name);
                bool success = new MoneyTransFormOPRSQL(DB).Delete_MoneyTransFormOPR(moneytransformoprid);
                if (success)
                {
                    this.Refresh_ListViewMoneyDataDetails_Flag = true;
                    Fill_Stuck_MoneyTransformOPR();
                }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("DeleteMoneyTransFormOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }


        }

        private void EditMoneyTransFormOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint moneytransformoprid = Convert.ToUInt32(listViewOperation .SelectedItems [0].Name );
                MoneyTransFormOPR MoneyTransFormOPR_ = new MoneyTransFormOPRSQL(DB).GetMoneyTransFormOPR_INFO_BYID(moneytransformoprid);
                MoneyTransFormOPRForm MoneyTransFormOPRForm_ = new MoneyTransFormOPRForm(DB, MoneyTransFormOPR_, true);
                MoneyTransFormOPRForm_.ShowDialog();
                if (MoneyTransFormOPRForm_.Refresh_ListViewMoneyDataDetails_Flag )
                {
                    this.Refresh_ListViewMoneyDataDetails_Flag = true;
                    Fill_Stuck_MoneyTransformOPR();

                }
                MoneyTransFormOPRForm_.Dispose();
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("EditMoneyTransFormOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }

        private void AddMoneyTransFormOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               MoneyTransFormOPRForm MoneyTransFormOPRForm_ = new MoneyTransFormOPRForm(DB,DateTime .Now , _MoneyBox );
                MoneyTransFormOPRForm_.ShowDialog();
                if (MoneyTransFormOPRForm_.Refresh_ListViewMoneyDataDetails_Flag )
                {
                    this.Refresh_ListViewMoneyDataDetails_Flag = true;
                    Fill_Stuck_MoneyTransformOPR();

                }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AddMoneyTransFormOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }

        private void OpenMoneyTransFormOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint moneytransformoprid = Convert.ToUInt32(listViewOperation.SelectedItems[0].Name);
                MoneyTransFormOPR MoneyTransFormOPR_ = new MoneyTransFormOPRSQL(DB).GetMoneyTransFormOPR_INFO_BYID(moneytransformoprid);
                MoneyTransFormOPRForm MoneyTransFormOPRForm_ = new MoneyTransFormOPRForm(DB, MoneyTransFormOPR_, false );
                MoneyTransFormOPRForm_.ShowDialog();
                if (MoneyTransFormOPRForm_.Refresh_ListViewMoneyDataDetails_Flag )
                {
                    this.Refresh_ListViewMoneyDataDetails_Flag = true;
                    Fill_Stuck_MoneyTransformOPR();

                }
                MoneyTransFormOPRForm_.Dispose();
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("EditMoneyTransFormOPR_MenuItem_Click:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }

        public void Fill_Stuck_MoneyTransformOPR()
        {
            listViewOperation.Items.Clear();

            List<MoneyTransFormOPR> MoneyTransFormOPRList = new MoneyTransFormOPRSQL(DB).Get_Stuck_MoneyTransformOPR_OUT_List(_MoneyBox); 
            for(int i=0;i<MoneyTransFormOPRList.Count;i++)
            {
                ListViewItem ListViewItem_ = new ListViewItem(MoneyTransFormOPRList[i].MoneyTransFormOPRID.ToString());
                ListViewItem_.Name = MoneyTransFormOPRList[i].MoneyTransFormOPRID.ToString();
                ListViewItem_.SubItems.Add(MoneyTransFormOPRList[i].MoneyTransFormOPRDate.ToString ());
                ListViewItem_.SubItems.Add(MoneyTransFormOPRList[i].SourceMoneyBox .BoxName);
                ListViewItem_.SubItems.Add(MoneyTransFormOPRList[i].Value .ToString());
                ListViewItem_.SubItems.Add(MoneyTransFormOPRList[i]._Currency .CurrencyName);
                if(MoneyTransFormOPRList[i].Creator_User._Employee!=null )
                ListViewItem_.SubItems.Add(MoneyTransFormOPRList[i].Creator_User ._Employee.EmployeeName);
                else
                    ListViewItem_.SubItems.Add("مدير النظام");

                ListViewItem_.BackColor = Color.Orange ;
                listViewOperation.Items.Add(ListViewItem_);

            }

        }

        private void listViewOperation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewOperation.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewOperation.Items)
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

                        MenuItem[] mi1 = new MenuItem[] { OpenMoneyTransFormOPR_MenuItem ,EditMoneyTransFormOPR_MenuItem ,DeleteMoneyTransFormOPR_MenuItem
                        ,new MenuItem ("-"),AddMoneyTransFormOPR_MenuItem};
                        listViewOperation.ContextMenu = new ContextMenu(mi1);
                    }
                    else
                    {
                        MenuItem[] mi1 = new MenuItem[] { AddMoneyTransFormOPR_MenuItem};
                        listViewOperation.ContextMenu = new ContextMenu(mi1);
                    }

                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
