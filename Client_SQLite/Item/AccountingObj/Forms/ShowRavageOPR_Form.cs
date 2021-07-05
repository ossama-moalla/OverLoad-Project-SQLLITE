using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Trade.Forms.TradeForms
{
    public partial class ShowRavageOPR_Form : Form
    {
        DatabaseInterface DB;

        System.Windows.Forms.MenuItem OpenOperation_MenuItem;
        System.Windows.Forms.MenuItem CreateRavageOPR_MenuItem;
        System.Windows.Forms.MenuItem CreateConsumeOPR_MenuItem;
        System.Windows.Forms.MenuItem EditOperation_MenuItem;
        System.Windows.Forms.MenuItem DeleteOperation_MenuItem;

        List<RavageOPR > RavageOPRList = new List<RavageOPR>();
        public ShowRavageOPR_Form(DatabaseInterface db)
        {
            DB = db;
            InitializeComponent();

            OpenOperation_MenuItem  = new System.Windows.Forms.MenuItem("استعراض", OpenOperation_MenuItem_Click);

            CreateRavageOPR_MenuItem = new System.Windows.Forms.MenuItem("انشاء عملية اتلاف", CreateRavageOPR_MenuItem_Click);
            CreateConsumeOPR_MenuItem = new System.Windows.Forms.MenuItem("انشاء عملية ترحيل مستهلكات", CreateConsumeOPR_MenuItem_Click);

            EditOperation_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditOperation_MenuItem_Click);
            DeleteOperation_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteOperation_MenuItem_Click); ;
            comboBoxOperationType.Items.Add(new ComboboxItem("الكل",0));
            comboBoxOperationType.Items.Add(new ComboboxItem("اتلاف",1 ));
            comboBoxOperationType.Items.Add (new ComboboxItem("مستهلكات", 2));

            RavageOPRList = new RavageOPRSQL(DB).Get_All_RavageOPR_List();
            RefreshOperations();
            comboBoxOperationType.SelectedIndex = 0;
            this.comboBoxOperationType.SelectedIndexChanged += new System.EventHandler(this.comboBoxOprtype_SelectedIndexChanged);

        }



        private void RefreshOperations()
        {
            try
            {
                listView.Items.Clear();
                for(int i=0;i< RavageOPRList.Count;i++)
                {
                    if (comboBoxOperationType.SelectedIndex > 0)
                    {
                        if (comboBoxOperationType.SelectedIndex == 1 && RavageOPRList[i]._Part != null) continue;
                        if (comboBoxOperationType.SelectedIndex == 2 &&RavageOPRList[i]._Part == null)
                            continue;
                    }


                    ListViewItem ListViewItem_ = new ListViewItem(RavageOPRList[i]._Part ==null ?"اتلاف":"مستهلكات");
                    ListViewItem_.Name =  RavageOPRList[i]._Operation.OperationID.ToString();
                    ListViewItem_.SubItems.Add(RavageOPRList[i]._Operation.OperationID.ToString());
                    ListViewItem_.SubItems.Add(RavageOPRList[i].RavageOPRDate .ToShortDateString());
                    if (RavageOPRList[i]._Part != null)
                        ListViewItem_.SubItems.Add(RavageOPRList[i]._Part.PartName);
                    else
                        ListViewItem_.SubItems.Add("-");
ListViewItem_.SubItems.Add(RavageOPRList[i].ClauseCount .ToString () );
                    ListViewItem_.SubItems.Add(RavageOPRList[i].Notes  );


                    if (RavageOPRList[i]._Part == null )
                        ListViewItem_.BackColor = Color.Orange;
                    else
                        ListViewItem_.BackColor = Color.YellowGreen ;
                    listView.Items.Add(ListViewItem_);
                }
            }catch(Exception ee)
            {
                MessageBox.Show("فشل تحديث العمليات:"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView.SelectedItems.Count > 0)
            {
                OpenOperation_MenuItem .PerformClick();
            }
        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {

            listView.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listView.Items)
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


                    MenuItem[] mi1 = new MenuItem[] { OpenOperation_MenuItem, EditOperation_MenuItem, DeleteOperation_MenuItem
                        , new MenuItem("-"), CreateConsumeOPR_MenuItem,CreateRavageOPR_MenuItem };
                    listView.ContextMenu = new ContextMenu(mi1);


                }
                else
                {

                    MenuItem[] mi = new MenuItem[] { CreateConsumeOPR_MenuItem, CreateRavageOPR_MenuItem };
                    listView.ContextMenu = new ContextMenu(mi);

                }

            }

        }
        private void DeleteOperation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                string s = listView .SelectedItems[0].Name;

                    uint oprid = Convert.ToUInt32(s);
                    bool Success = new RavageOPRSQL (DB).DeleteRavageOPR  (oprid);

                    if (Success)
                    {
                        RavageOPRList = new RavageOPRSQL(DB).Get_All_RavageOPR_List();
                        RefreshOperations();
                    }


                
            }
            catch (Exception ee)
            {
                MessageBox.Show("حذف عملية" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditOperation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string s = listView .SelectedItems[0].Name;

                    uint oprid = Convert.ToUInt32(s);
                    RavageOPR RavageOPR_ = new RavageOPRSQL(DB).GetRavageOPR_INFO_BYID (oprid);
                    Trade.Forms.TradeForms.RavageOPR_Form RavageOPR_Form_ = new Trade.Forms.TradeForms.RavageOPR_Form  (DB, RavageOPR_, true);
                RavageOPR_Form_.ShowDialog();
                    if (RavageOPR_Form_.Changed)
                    {
                        RavageOPRList = new RavageOPRSQL(DB).Get_All_RavageOPR_List();
                        RefreshOperations();
                    }


            }
            catch (Exception ee)
            {
                MessageBox.Show("تعديل عملية" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateRavageOPR_MenuItem_Click(object sender, EventArgs e)
        {
           try
            {
                RavageOPR_Form RavageOPR_Form_ = new RavageOPR_Form(DB,DateTime .Now , RavageOPR_Form.OPERATION_RAVAGE);
                RavageOPR_Form_.ShowDialog();
                if(RavageOPR_Form_.Changed )
                {
                    RavageOPRList = new RavageOPRSQL(DB).Get_All_RavageOPR_List();
                    RefreshOperations();
                }
                RavageOPR_Form_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateRavageOPR_MenuItem_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void CreateConsumeOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RavageOPR_Form RavageOPR_Form_ = new RavageOPR_Form(DB, DateTime.Now, RavageOPR_Form.OPERATION_CONSUME);
                RavageOPR_Form_.ShowDialog();
                if (RavageOPR_Form_.Changed)
                {
                    RavageOPRList = new RavageOPRSQL(DB).Get_All_RavageOPR_List();
                    RefreshOperations();
                }
                RavageOPR_Form_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateConsumeOPR_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenOperation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string s = listView.SelectedItems[0].Name;

                uint oprid = Convert.ToUInt32(s);
                RavageOPR RavageOPR_ = new RavageOPRSQL(DB).GetRavageOPR_INFO_BYID(oprid);
                Trade.Forms.TradeForms.RavageOPR_Form RavageOPR_Form_ = new Trade.Forms.TradeForms.RavageOPR_Form(DB, RavageOPR_, false );
                RavageOPR_Form_.ShowDialog();
                if (RavageOPR_Form_.Changed)
                {
                    RavageOPRList = new RavageOPRSQL(DB).Get_All_RavageOPR_List();
                    RefreshOperations();
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("استعراض تفاصيل العملية" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
      
  
        
  

        private void comboBoxOprtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOperations();
        }
    }
}
