using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Forms;
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

namespace OverLoad_Client.Configuration
{
    public partial class ConfigurationForm : Form
    {
        DatabaseInterface DB;
        Button SelectedButton;

        MenuItem AddCurrency;
        MenuItem EditCurrency;
        MenuItem DeleteCurrency;
        MenuItem SetDefaultCurrency;

        MenuItem AddSellType;
        MenuItem EditSellType;
        MenuItem DeleteSellType;

        MenuItem AddTradeState;
        MenuItem EditTradeState;
        MenuItem DeleteTradeState;

        MenuItem AddMoneyBox;
        MenuItem EditMoneyBox;
        MenuItem DeleteMoneyBox;

        MenuItem AddEmployeeMentLevel;
        MenuItem EditEmployeeMentLevel;
        MenuItem DeleteEmployeeMentLevel;
        

        public bool MoneyBoxChanged;
        public bool CurrencyChanged;
        public ConfigurationForm(DatabaseInterface db)
        {
            DB = db;
            if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("أنت غير منضم لمجموعة المدراء , لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            MoneyBoxChanged = false;
            CurrencyChanged = false;
            AddCurrency = new System.Windows.Forms.MenuItem("اضافة عملة", AddCurrency_MenuItem_Click);
            EditCurrency= new System.Windows.Forms.MenuItem("تعديل العملة", EditCurrency_MenuItem_Click); ;
            DeleteCurrency  = new System.Windows.Forms.MenuItem("حذف العملة", DeleteCurrency_MenuItem_Click); ;
            SetDefaultCurrency = new System.Windows.Forms.MenuItem("ضبط كافتراضي", SetDefaultCurrency_MenuItem_Click); ;


            AddSellType = new System.Windows.Forms.MenuItem("اضافة نمط بيع", AddSellType_MenuItem_Click);
            EditSellType = new System.Windows.Forms.MenuItem("تعديل نط البيع", EditSellType_MenuItem_Click); ;
            DeleteSellType = new System.Windows.Forms.MenuItem("حذف نمط البيع", DeleteSellType_MenuItem_Click); ;

            AddTradeState  = new System.Windows.Forms.MenuItem("اضافة جديد", AddTradeState_MenuItem_Click );
            EditTradeState  = new System.Windows.Forms.MenuItem("تعديل", EditTradeState_MenuItem_Click); 
            DeleteTradeState  = new System.Windows.Forms.MenuItem("حذف ", DeleteTradeState_MenuItem_Click);

            AddMoneyBox = new System.Windows.Forms.MenuItem("اضافة جديد", AddMoneyBox_MenuItem_Click);
            EditMoneyBox = new System.Windows.Forms.MenuItem("تعديل", EditMoneyBox_MenuItem_Click);
            DeleteMoneyBox = new System.Windows.Forms.MenuItem("حذف ", DeleteMoneyBox_MenuItem_Click);

            AddEmployeeMentLevel = new System.Windows.Forms.MenuItem("اضافة جديد", AddEmployeeMentLevel_MenuItem_Click);
            EditEmployeeMentLevel = new System.Windows.Forms.MenuItem("تعديل", EditEmployeeMentLevel_MenuItem_Click);
            DeleteEmployeeMentLevel = new System.Windows.Forms.MenuItem("حذف ", DeleteEmployeeMentLevel_MenuItem_Click);

        }


        #region TradeStateMenuItem
        private void DeleteTradeState_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewData.SelectedItems.Count == 1)
                {
                    DialogResult dd = MessageBox.Show("هل انت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd == DialogResult.OK)
                    {
                        uint stateid = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                        bool success = new TradeStateSQL (DB).DeleteTradestate (stateid);
                        if (success) buttonTradeStateSetting.PerformClick();
                    }

                }
            }
            catch
            {
                MessageBox.Show("حدث خطأ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void EditTradeState_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewData.SelectedItems.Count == 1)
            {
                uint tradestateid = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                InputBox InputBox_ = new InputBox("تعديل حالة بيع شراء", "أدخل وصف حالة العنصر ", listViewData.SelectedItems[0].SubItems [0].Text );
                DialogResult dd = InputBox_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    bool success = new TradeStateSQL(DB).UpdateTradeState (tradestateid, InputBox_.textBox1.Text);
                    if (success) buttonTradeStateSetting.PerformClick();
                }
            }

        }

        private void AddTradeState_MenuItem_Click(object sender, EventArgs e)
        {
            InputBox InputBox_ = new InputBox("اضافة حالة بيع شراء", "أدخل وصف حالة العنصر");
            DialogResult dd = InputBox_.ShowDialog();
            if (dd == DialogResult.OK)
            {
                bool success = new TradeStateSQL (DB).AddTradeState (InputBox_.textBox1.Text);
                if (success) buttonTradeStateSetting.PerformClick();
            }
        }
        #endregion
        #region SellTypeMenuItem
        private void DeleteSellType_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewData.SelectedItems.Count == 1)
                {
                    DialogResult dd = MessageBox.Show("هل انت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd == DialogResult.OK)
                    {
                        uint selltypeid = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                        bool success = new SellTypeSql(DB).DeleteSellType(selltypeid);
                        if (success) buttonSellTypeSetting.PerformClick();
                    }

                }
            }
            catch
            {
                MessageBox.Show("حدث خطأ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void EditSellType_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewData.SelectedItems.Count == 1)
            {
                uint selltypeid = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                InputBox InputBox_ = new InputBox("تعديل نمط بيع", "أدخل اسم نمط البيع الجديد", listViewData.SelectedItems[0].SubItems[0].Text);
                DialogResult dd = InputBox_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    bool success = new SellTypeSql(DB).UpdateSellType (selltypeid,InputBox_.textBox1.Text);
                    if (success) buttonSellTypeSetting.PerformClick();
                }
            }

        }

        private void AddSellType_MenuItem_Click(object sender, EventArgs e)
        {
            InputBox InputBox_ = new InputBox("اضافة نمط بيع","أدخل اسم نمط البيع");
            DialogResult dd = InputBox_.ShowDialog();
            if (dd == DialogResult.OK)
            {
                bool success = new SellTypeSql(DB).AddSellType(InputBox_.textBox1 .Text );
                if(success ) buttonSellTypeSetting .PerformClick();
            }
        }
        #endregion
        #region CurrencyMenuItem
        private void SetDefaultCurrency_MenuItem_Click(object sender, EventArgs e)
        {
           try
            {
                if (listViewData.SelectedItems.Count == 1)
                {
                    uint currencyid = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                    bool success= ProgramGeneralMethods.Registry_SetDefaultCurrency(currencyid);
                    if(success ) buttonCurrencySetting.PerformClick();
                }
                
            }catch
            {

            }
        }
        private void DeleteCurrency_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewData.SelectedItems.Count == 1)
                {
                    DialogResult dd = MessageBox.Show("هل انت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd == DialogResult.OK)
                    {
                        uint currencyid = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                        CurrencySQL currencysql = new CurrencySQL(DB);
                        Currency currency = currencysql.GetCurrencyINFO_ByID(currencyid);
                        if (currency.ReferenceCurrencyID == null)
                        {
                            MessageBox.Show("غير مسموح حذف او تعديل بيانات العملة المرجعية", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        bool success = currencysql.DeleteCurrency(currencyid);
                        if (success)
                        { buttonCurrencySetting.PerformClick(); CurrencyChanged = true; }
                    }

                }
            }
            catch
            {
                MessageBox.Show("حدث خطأ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void EditCurrency_MenuItem_Click(object sender, EventArgs e)
        {
            if(listViewData .SelectedItems .Count ==1)
            {
                uint currencyid = Convert.ToUInt32(listViewData.SelectedItems[0].Name );
                Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(currencyid);
                if(currency .ReferenceCurrencyID ==null )
                {
                    MessageBox.Show("غير مسموح تعديل بيانات العملة المرجعية","",MessageBoxButtons.OK ,MessageBoxIcon.Error );
                    return;
                }
                AddCurrencyForm AddCurrencyForm_ = new AddCurrencyForm(DB, currency);
                DialogResult dd = AddCurrencyForm_.ShowDialog();
                if (dd == DialogResult.OK) { buttonCurrencySetting.PerformClick(); CurrencyChanged = true;
            }
        }
           
        }

        private void AddCurrency_MenuItem_Click(object sender, EventArgs e)
        {
            AddCurrencyForm AddCurrencyForm_ = new AddCurrencyForm(DB);
            DialogResult dd = AddCurrencyForm_.ShowDialog();
            if (dd == DialogResult.OK) { buttonCurrencySetting.PerformClick(); CurrencyChanged = true;
        }
    }
        #endregion
        #region MoneyBoxMenuItem
        private void DeleteMoneyBox_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewData.SelectedItems.Count == 1)
                {
                    DialogResult dd = MessageBox.Show("هل انت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd == DialogResult.OK)
                    {
                        uint id = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                        bool success = new MoneyBoxSQL(DB).DeleteMoneyBox (id );
                        if (success) { buttonMoneyBox.PerformClick(); MoneyBoxChanged = true;   }
                    }

                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("DeleteMoneyBox:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void EditMoneyBox_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewData.SelectedItems.Count == 1)
            {
                uint id = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                InputBox InputBox_ = new InputBox("تعديل اسم صندوق المال", "أدخل اسم صندوق المال ", listViewData.SelectedItems[0].SubItems[1].Text);
                DialogResult dd = InputBox_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    bool success = new MoneyBoxSQL(DB).UpdateMoneyBox(id, InputBox_.textBox1.Text);
                    if (success)
                    {
                        buttonMoneyBox.PerformClick(); MoneyBoxChanged = true;
                    }
                }
            }
        }

        private void AddMoneyBox_MenuItem_Click(object sender, EventArgs e)
        {
            InputBox InputBox_ = new InputBox("اضافة صندوق المال الجديد", "أدخل اسم صندوق المال");
            DialogResult dd = InputBox_.ShowDialog();
            if (dd == DialogResult.OK)
            {
                bool success = new MoneyBoxSQL(DB).AddMoneyBox (InputBox_.textBox1.Text);
                if (success)
                {
                    buttonMoneyBox.PerformClick(); MoneyBoxChanged = true;
                }
            }
        }
        #endregion
        #region EmployeeMentLevelMenuItem
        private void DeleteEmployeeMentLevel_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewData.SelectedItems.Count == 1)
                {
                    DialogResult dd = MessageBox.Show("هل انت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd == DialogResult.OK)
                    {
                        uint id = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                        bool success = new Company.CompanySQL.EmployeeMentLevelSQL(DB).Delete_EmployeeMentLevel (id);
                        if (success) { buttonEmployeementLevel.PerformClick();  }
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteEmployeeMentLevel:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void EditEmployeeMentLevel_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewData.SelectedItems.Count == 1)
            {
                uint id = Convert.ToUInt32(listViewData.SelectedItems[0].Name);
                InputBox InputBox_ = new InputBox("تعديل وصف المسمى الوظيفي", "أدخل وصف المسمى الوظيفي ", listViewData.SelectedItems[0].SubItems[1].Text);
                DialogResult dd = InputBox_.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    bool success = new Company.CompanySQL.EmployeeMentLevelSQL(DB).Update_EmployeeMentLevel (id, InputBox_.textBox1.Text);
                    if (success)
                    {
                        buttonEmployeementLevel .PerformClick(); 
                    }
                }
            }
        }

        private void AddEmployeeMentLevel_MenuItem_Click(object sender, EventArgs e)
        {
            InputBox InputBox_ = new InputBox("اضافة  مسمى وظيفي جديد", "أدخل وصف المسمى الوظيفي");
            DialogResult dd = InputBox_.ShowDialog();
            if (dd == DialogResult.OK)
            {
                bool success = new Company.CompanySQL.EmployeeMentLevelSQL(DB).Add_EmployeeMentLevel (InputBox_.textBox1.Text);
                if (success)
                {
                    buttonEmployeementLevel.PerformClick(); 
                }
            }
        }
        #endregion
        public void OptimizeListViewColumns()
        {
            for (int i = 0; i < listViewData .Columns  .Count; i++)
            {
                listViewData.Columns[i].Width = listViewData.Width / listViewData.Columns.Count;
            }
       }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            buttonCurrencySetting.PerformClick();
        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            listViewData.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewData.Items)
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
                    try
                    {
                        if (SelectedButton == buttonCurrencySetting)
                        {
                            MenuItem[] mi1 = new MenuItem[] { SetDefaultCurrency ,AddCurrency, EditCurrency, DeleteCurrency };
                            listViewData.ContextMenu = new ContextMenu(mi1);
                        }
                        else if (SelectedButton == buttonSellTypeSetting )
                        {
                            MenuItem[] mi1 = new MenuItem[] { AddSellType , EditSellType , DeleteSellType  };
                            listViewData.ContextMenu = new ContextMenu(mi1);
                        }
                        else if (SelectedButton == buttonTradeStateSetting )
                        {
                            MenuItem[] mi1 = new MenuItem[] { AddTradeState, EditTradeState, DeleteTradeState };
                            listViewData.ContextMenu = new ContextMenu(mi1);
                        }
                        else if(SelectedButton ==buttonMoneyBox)
                        {
                            MenuItem[] mi1 = new MenuItem[] { AddMoneyBox, EditMoneyBox, DeleteMoneyBox };
                            listViewData.ContextMenu = new ContextMenu(mi1);
                        }
                        else if (SelectedButton == buttonEmployeementLevel )
                        {
                            MenuItem[] mi1 = new MenuItem[] { AddEmployeeMentLevel, EditEmployeeMentLevel, DeleteEmployeeMentLevel };
                            listViewData.ContextMenu = new ContextMenu(mi1);
                        }

                    }
                    catch
                    {
                        return;
                    }



                }
                else
                {
                    if (SelectedButton == buttonCurrencySetting)
                    {
                        MenuItem[] mi1 = new MenuItem[] { AddCurrency };
                        listViewData.ContextMenu = new ContextMenu(mi1);
                    }
                    else if (SelectedButton == buttonSellTypeSetting)
                    {
                        MenuItem[] mi1 = new MenuItem[] { AddSellType };
                        listViewData.ContextMenu = new ContextMenu(mi1);
                    }
                    else if (SelectedButton == buttonTradeStateSetting)
                    {
                        MenuItem[] mi1 = new MenuItem[] { AddTradeState };
                        listViewData.ContextMenu = new ContextMenu(mi1);
                    }
                    else if (SelectedButton == buttonMoneyBox)
                    {
                        MenuItem[] mi1 = new MenuItem[] { AddMoneyBox };
                        listViewData.ContextMenu = new ContextMenu(mi1);
                    }
                    else if (SelectedButton == buttonEmployeementLevel)
                    {
                        MenuItem[] mi1 = new MenuItem[] { AddEmployeeMentLevel };
                        listViewData.ContextMenu = new ContextMenu(mi1);
                    }

                }

                }
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewData.SelectedItems.Count > 0)
            {
                //OpenPlaceDetailsMenuItem.PerformClick();
            }
        }

        private void buttonCurrencySetting_Click(object sender, EventArgs e)
        {
            SelectedButton = buttonCurrencySetting;
            labelHeader.Text = "ضبط العملات";
            try
            {
                Currency defaultcurrency = ProgramGeneralMethods.Registry_GetDefaultCurrency(DB);
                listViewData.Items.Clear();
                listViewData.Columns.Clear();
                listViewData.ContextMenu = null;
                List<Currency> CurrencyList = new CurrencySQL(DB).GetCurrencyList();
                listViewData.Columns.Add("اسم العملة" );
                listViewData.Columns.Add("رمز العملة");
                listViewData.Columns.Add("سعر الصرف");
                listViewData.Columns.Add("", 50);
                for (int i=0;i<CurrencyList .Count;i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(CurrencyList[i].CurrencyName);
                    ListViewItem_.Name = CurrencyList[i].CurrencyID.ToString();
                    ListViewItem_.SubItems.Add(CurrencyList[i].CurrencySymbol );
                    ListViewItem_.SubItems.Add(CurrencyList[i].ExchangeRate.ToString());
                   if(defaultcurrency !=null )
                    if (CurrencyList[i].CurrencyID == defaultcurrency.CurrencyID)
                        ListViewItem_.SubItems.Add("افتراضية");
                    listViewData.Items.Add(ListViewItem_);
                }
                OptimizeListViewColumns();
        }
            catch (Exception ee)
            {
                MessageBox.Show("buttonCurrencySetting_Click"+ee.Message );
            }
}

        private void buttonSellTypeSetting_Click(object sender, EventArgs e)
        {

            SelectedButton = buttonSellTypeSetting ;
            labelHeader.Text = "ضبط أنماط البيع";
            try
            {
                listViewData.Items.Clear();
                listViewData.Columns.Clear();
                listViewData.ContextMenu = null;
                List<SellType > SellTypeList = new SellTypeSql(DB).GetSellTypeList();
                listViewData.Columns.Add("اسم نمط البيع");
                for (int i = 0; i < SellTypeList.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(SellTypeList[i].SellTypeName);
                    ListViewItem_.Name = SellTypeList[i].SellTypeID .ToString();
                    listViewData.Items.Add(ListViewItem_);
                }
                OptimizeListViewColumns();
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSellTypeSetting_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonTradeStateSetting_Click(object sender, EventArgs e)
        {
            SelectedButton = buttonTradeStateSetting ;
            labelHeader.Text = "ضبط حالات بيع شراء العنصر";
            try
            {
                listViewData.Items.Clear();
                listViewData.Columns.Clear();
                listViewData.ContextMenu = null;
                List<TradeState > TradeStateList = new TradeStateSQL(DB).GetTradeStateList();
                listViewData.Columns.Add("وصف حالة العنصر");
                for (int i = 0; i < TradeStateList.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(TradeStateList[i].TradeStateName );
                    ListViewItem_.Name = TradeStateList[i].TradeStateID.ToString();
                    listViewData.Items.Add(ListViewItem_);
                }
                OptimizeListViewColumns();
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonTradeStateSetting_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonMoneyBox_Click(object sender, EventArgs e)
        {
            SelectedButton = buttonMoneyBox;
            labelHeader.Text = "ضبط صناديق المال";
            try
            {
                listViewData.Items.Clear();
                listViewData.Columns.Clear();
                listViewData.ContextMenu = null;
                List<MoneyBox> MoneyBoxList = new MoneyBoxSQL(DB).GetMoneyBox_List();
                listViewData.Columns.Add("رقم الصندوق");
                listViewData.Columns.Add("اسم الصندوق");
                listViewData.Columns.Add("", 50);
                for (int i = 0; i < MoneyBoxList.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(MoneyBoxList[i].BoxID.ToString ());
                    ListViewItem_.Name = MoneyBoxList[i].BoxID.ToString();
                    ListViewItem_.SubItems.Add(MoneyBoxList[i].BoxName);

                    listViewData.Items.Add(ListViewItem_);
                }
                listViewData.Columns[0].Width = 150;
                listViewData.Columns[1].Width = listViewData.Width - 200;
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonMoneyBox_Click:" + ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void buttonEmployeementLevel_Click(object sender, EventArgs e)
        {
            SelectedButton = buttonEmployeementLevel;
            labelHeader.Text = "ضبط المستويات الوظيفة";
            try
            {
                listViewData.Items.Clear();
                listViewData.Columns.Clear();
                listViewData.ContextMenu = null;
                List<Company.Objects.EmployeeMentLevel> EmployeeMentLevelList = new Company.CompanySQL.EmployeeMentLevelSQL(DB).Get_EmployeeMentLevel_List();
                listViewData.Columns.Add("الرقم");
                listViewData.Columns.Add("الوصف");
                listViewData.Columns.Add("", 50);
                for (int i = 0; i < EmployeeMentLevelList.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(EmployeeMentLevelList[i].LevelID .ToString());
                    ListViewItem_.Name = EmployeeMentLevelList[i].LevelID .ToString();
                    ListViewItem_.SubItems.Add(EmployeeMentLevelList[i].LevelName);

                    listViewData.Items.Add(ListViewItem_);
                }
                listViewData.Columns[0].Width = 150;
                listViewData.Columns[1].Width = listViewData.Width - 200;
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonEmployeementLevel_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
