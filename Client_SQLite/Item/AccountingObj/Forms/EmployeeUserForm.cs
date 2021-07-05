using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Company.Objects;
using OverLoad_Client.Trade.Objects;
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

    public partial class EmployeeUserForm : Form
    {
       

        public const uint ResetPassWord_Function=1;
        public const uint EditUserData_Function = 2;
        public const uint OpenUserData_Function = 3;
        private Employee _Employee;
        private DatabaseInterface.User _User;
        private DatabaseInterface DB;
        private uint Function;
        public bool Changed { get; set; }
        private  MenuItem AddToGroup_MenuItem;
        private MenuItem RemoveFromGroup_MenuItem;
        private MenuItem GrantMoneyBox_MenuItem;
        private MenuItem UNGrantMoneyBox_MenuItem;

        private MenuItem Grant_AccessFolder_MenuItem;
        private MenuItem UNGrant_AccessFolder_MenuItem;

        private MenuItem Grant_AccessContainer_MenuItem;
        private MenuItem UNGrant_AccessContainer_MenuItem;

        private MenuItem Grant_SellType_MenuItem;
        private MenuItem UNGrant_SellType_MenuItem;
        public EmployeeUserForm(DatabaseInterface db,Employee Employee_)
        {
            InitializeComponent();
            DB = db;
            //if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("أنت غير منضم لمجموعة المدراء, لا يمكنك فتح هذه النافذة");

            _Employee = Employee_;
            textBoxEmployeeID.Text = _Employee .EmployeeID.ToString();
            textBoxEmployeeName.Text = _Employee.EmployeeName ;

            comboBoxDisable.SelectedIndex = 0;
           
        }





     private void InitializeMenuItems()
        {
            AddToGroup_MenuItem = new MenuItem("ضم الى المجموعة", AddToGroup_MenuItem_Click);
            RemoveFromGroup_MenuItem = new MenuItem("اخراج من المجموعة", RemoveFromGroup_MenuItem_Click);

            GrantMoneyBox_MenuItem = new MenuItem("منح صلاحية الوصول لصندوق مال", GrantMoneyBox_MenuItem_Click);
            UNGrantMoneyBox_MenuItem = new MenuItem("الغاء صلاحية الوصول", UNGrantMoneyBox_MenuItem_Click);

            Grant_AccessFolder_MenuItem = new MenuItem("منح الصلاحية للوصول لصنف", Grant_AccessFolder_MenuItem_Click);
            UNGrant_AccessFolder_MenuItem = new MenuItem("الغاء صلاحية الوصول", UNGrant_AccessFolder_MenuItem_Click);

            Grant_AccessContainer_MenuItem = new MenuItem("منح صلاحية الوصول لحاوية تخزين", Grant_AccessContainer_MenuItem_Click);
            UNGrant_AccessContainer_MenuItem = new MenuItem("الغاء صلاحية الوصول", UNGrant_AccessContainer_MenuItem_Click);

            Grant_SellType_MenuItem = new MenuItem("منح الصلاحية للوصول لنمط مبيع في فواتير المبيع و الصيانة", Grant_SellType_MenuItem_Click);
            UNGrant_SellType_MenuItem = new MenuItem("الغاء صلاحية الوصول", UNGrant_SellType_MenuItem_Click);

        }
        public EmployeeUserForm(DatabaseInterface db, DatabaseInterface.User User_, uint Function_)
        {
            try
            {
                InitializeComponent();

                Function = Function_;
                if (Function != ResetPassWord_Function && Function != EditUserData_Function && Function != OpenUserData_Function)
                    throw new Exception("تابع غير معروف");
                if (Function == OpenUserData_Function) buttonADD.Visible = false;
                Function = Function_;
                DB = db;
                //if (!DB.IS_Belong_To_Admin_Group(DB.__User.UserID)) throw new Exception("أنت غير منضم لمجموعة المدراء, لا يمكنك فتح هذه النافذة");

                _User  = User_;
                _Employee  = _User._Employee ;
                InitializeMenuItems();
                LoadForm();
               
            }
            catch (Exception ee)
            {
                MessageBox.Show(":فشل تحميل الصفحة" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void LoadForm()
        {
            try
            {
                if (_User == null || _Employee == null) throw new Exception("Employee OR User is NULL");
                buttonADD.Name = "buttonSave";
                buttonADD.Text  = "حفظ";
                textBoxEmployeeID.Text = _Employee.EmployeeID.ToString();
                textBoxEmployeeName.Text = _Employee.EmployeeName;
                textBoxUserName.Text = _User.UserName;
                if (_User.Disabled) comboBoxDisable.SelectedIndex = 1;
                else comboBoxDisable.SelectedIndex = 0;
                FillPremession();
                InitializeMenuItems();
                FillFolderPremessionData();
                FillMoneyBoxDataPremession();
                FillSellTypePremessionData();
                FillContainerPremessionData();


                if (Function == ResetPassWord_Function)
                {

                    textBoxUserName.ReadOnly = true;
                    comboBoxDisable.Enabled = false;
                    textBoxPassword.ReadOnly = false;
                    textBoxPasswordConfirm.ReadOnly = false;
                    textBoxUserName.BackColor = Color.Gray;
                    panelPremession.Enabled = false;
                }
                else if (Function == EditUserData_Function)
                {

                    textBoxPassword.Enabled = false;
                    textBoxPasswordConfirm.Enabled = false;
                    textBoxUserName.ReadOnly = false;
                    comboBoxDisable.Enabled = true;
                    textBoxPassword.BackColor = Color.Gray;
                    textBoxPasswordConfirm.BackColor = Color.Gray;
                    this.listViewPremession.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewPremession_MouseDown);
                    this.listViewMoneyBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewMoneyBox_MouseDown);

                    this.listViewFolderPremession.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewFolderPremession_MouseDown);
                    //this.listViewFolderPremession.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewFolderPremession_MouseDown);

                    this.listViewContainerPremession.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewContainerPremession_MouseDown);
                    //this.listViewContainerPremession.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewContainerPremession_MouseDown);

                    this.listViewSellTypePremession.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewSellTypePremession_MouseDown);
                    //this.listViewSellTypePremession.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewSellTypePremession_MouseDown);

                }
                else
                {
                    textBoxUserName.ReadOnly = true;
                    comboBoxDisable.Enabled = false;
                    textBoxPassword.Enabled = false;
                    textBoxPasswordConfirm.Enabled = false;
                    textBoxPassword.BackColor = Color.Gray;
                    textBoxPasswordConfirm.BackColor = Color.Gray;
                    panelPremession.Enabled = true;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadForm:"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }
       private async void FillPremession()
        {
            listViewPremession.Items.Clear();
            List<DatabaseInterface.Premession> list = DB.GetUserPremessions(_User.UserID);
            for(int i=0;i<list .Count;i++)
            {
                string groupname="", groupdetails="";
                switch (list [i].GroupID )
                {
                    case DatabaseInterface.Premession.ADMIN_GROUP:
                        groupname = "مجموعة المدراء";
                        groupdetails = "";
   
                        break;
                    case DatabaseInterface.Premession.BUY_GROUP :
                        groupname = "مجموعة المشتريات";
                        groupdetails = "";


                        break;
                    case DatabaseInterface.Premession.SELL_GROUP :
                        groupname = "مجموعة المبيعات";
                        groupdetails = "";
        

                        break;
                    case DatabaseInterface.Premession.MAINTENANCE_GROUP :
                        groupname = "مجموعة الصيانة";
                        groupdetails = "";
                        break;
                    case DatabaseInterface.Premession.EMPLOYEE_GROUP :
                        groupname = "مجموعة ادارة الموظفين";
                        groupdetails = "";
                        break;
                    case DatabaseInterface.Premession.ITEM_GROUP :
                        groupname = "مجموعة ادارة العناصر";
                        groupdetails = "";
                        break;
                    case DatabaseInterface.Premession.CONTACT_GROUP :
                        groupname = "مجموعة ادارة الموردين و الزبائن";
                        groupdetails = "";
                        break;
                    case DatabaseInterface.Premession.INDUSTRY_GROUP:
                        groupname = "ادارة التجميع و التصنيع";
                        groupdetails = "";
                        break;
                    case DatabaseInterface.Premession.CONTAINER_GROUP:
                        groupname = "ادارة حاويات التخزين";
                        groupdetails = "";
                        break;

                }
                ListViewItem ListViewItem_ = new ListViewItem(groupname);
                ListViewItem_.Name = (list[i].Join ? "J" : "N")+list[i].GroupID.ToString();
                ListViewItem_.SubItems.Add(list[i].Join ? "منضم" : "غير منضم");
                ListViewItem_.SubItems.Add(groupdetails);
                ListViewItem_.BackColor= (list[i].Join ? Color.LimeGreen  : Color.Orange );
                listViewPremession.Items.Add(ListViewItem_);
            }

           
           

        }
  
    
 
        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (_Employee  == null) return;

            if(!textBoxPassword.Text .SequenceEqual(textBoxPasswordConfirm.Text ))
            {
                MessageBox.Show("كلمة المرور غير متطابقة في الحقلين", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxPassword.Text.Contains (" "))
            {
                MessageBox.Show("كلمة المرور يجب ان لا تحوي فراغات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool disabled;
            if (comboBoxDisable.SelectedIndex == 0) disabled  = false ;
            else
                disabled = true ;
            if (buttonADD.Name == "buttonADD")
            {
                try
                {
                    bool success =DB.AddUser 
                        (_Employee .EmployeeID, textBoxUserName.Text, textBoxPassword.Text ,disabled );
                    if (success)
                    {

                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         this.Changed = true;
                        Function = EditUserData_Function;
                        _User = DB.GetEmployeeUser(_Employee);
                        LoadForm();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر اضافة المستخدم " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (Function == ResetPassWord_Function)
                    {
                        bool success = DB.ResetUserPassword
                          (_Employee.EmployeeID, textBoxPassword .Text );
                        if (success)
                        {

                            MessageBox.Show("تم اعادة تعيين كلمة المرور بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();

                        }
                    }
                    else
                    {
                        bool success = DB.EditUserData 
                          (_Employee.EmployeeID, textBoxUserName.Text,disabled );
                        if (success)
                        {

                            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Changed = true;
                            LoadForm();

                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":حدث خطأ" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listViewPremession_MouseDown(object sender, MouseEventArgs e)
        {
            listViewPremession.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewPremession.Items)
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
                    MenuItem[] mi1;
                    if(listitem.Name.Substring (0,1)=="J")
                        mi1 = new MenuItem[] { RemoveFromGroup_MenuItem  };
                    else
                        mi1 = new MenuItem[] { AddToGroup_MenuItem };
                    listViewPremession.ContextMenu = new ContextMenu(mi1);
                }
            }
        }
        private void AddToGroup_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPremession.SelectedItems.Count == 1)
            {
                try
                {
                    uint groupid = Convert.ToUInt32(listViewPremession.SelectedItems[0].Name.Substring(1));
                    bool result;
                    switch (groupid)
                    {
                        case DatabaseInterface.Premession.ADMIN_GROUP:
                            result= DB.Add_To_Admin_Group(_User.UserID);

                            break;
                        case DatabaseInterface.Premession.BUY_GROUP:
                            result = DB.Add_To_Buy_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.SELL_GROUP:
                            result = DB.Add_To_Sell_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.MAINTENANCE_GROUP:
                            result = DB.Add_To_Maintenance_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.EMPLOYEE_GROUP:
                            result = DB.Add_To_Employee_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.ITEM_GROUP:
                            result = DB.Add_To_Item_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.CONTACT_GROUP:
                            result = DB.Add_To_Contact_Group(_User.UserID);
                            break;

                        case DatabaseInterface.Premession.INDUSTRY_GROUP:
                            result = DB.Add_To_Industry_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.CONTAINER_GROUP:
                            result = DB.Add_To_Container_Group(_User.UserID);
                            break;
                        default:
                            throw new Exception("مجموعة غير معروفة");
                    }
                    if (result)
                    {
                        MessageBox.Show("تم الضم الى المجموعة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillPremession();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("AddToGroup_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoveFromGroup_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPremession.SelectedItems.Count == 1)
            {
                try
                {
                    uint groupid = Convert.ToUInt32(listViewPremession.SelectedItems[0].Name.Substring(1));
                    bool result;
                    switch (groupid)
                    {
                        case DatabaseInterface.Premession.ADMIN_GROUP:
                            result = DB.Remove_From_Admin_Group(_User.UserID);

                            break;
                        case DatabaseInterface.Premession.BUY_GROUP:
                            result = DB.Remove_From_Buy_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.SELL_GROUP:
                            result = DB.Remove_From_Sell_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.MAINTENANCE_GROUP:
                            result = DB.Remove_From_Maintenance_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.EMPLOYEE_GROUP:
                            result = DB.Remove_From_Employee_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.ITEM_GROUP:
                            result = DB.Remove_From_Item_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.CONTACT_GROUP:
                            result = DB.Remove_From_Contact_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.INDUSTRY_GROUP:
                            result = DB.Remove_From_Industry_Group(_User.UserID);
                            break;
                        case DatabaseInterface.Premession.CONTAINER_GROUP:
                            result = DB.Remove_From_Container_Group(_User.UserID);
                            break;
                        default:
                            throw new Exception("مجموعة غير معروفة");
                    }
                    if(result )
                    {
                        MessageBox.Show("تم الازالة من المجموعة بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        FillPremession();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("AddToGroup_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region MoneyBox
        private void FillMoneyBoxDataPremession()
        {
            listViewMoneyBox.Items.Clear();
            List<DatabaseInterface.MoneyBoxPremession> MoneyBoxPremessionlist = DB.Get_MoneyBoxPremession_List(_User.UserID);
            for (int i = 0; i < MoneyBoxPremessionlist.Count; i++)
            {

                ListViewItem ListViewItem_ = new ListViewItem(MoneyBoxPremessionlist[i]._MoneyBox.BoxID.ToString());
                ListViewItem_.Name = (MoneyBoxPremessionlist[i].Allow ? "A" : "N") + MoneyBoxPremessionlist[i]._MoneyBox.BoxID.ToString();
                ListViewItem_.SubItems.Add(MoneyBoxPremessionlist[i]._MoneyBox.BoxName);
                ListViewItem_.SubItems.Add(MoneyBoxPremessionlist[i].Allow ? "يملك الصلاحية" : "لا يملك الصلاحية");
                ListViewItem_.BackColor = (MoneyBoxPremessionlist[i].Allow ? Color.LimeGreen : Color.Orange);
                listViewMoneyBox.Items.Add(ListViewItem_);
            }
        }
        private void GrantMoneyBox_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMoneyBox.SelectedItems.Count == 1)
            {
                try
                {
                    uint moneyboxid = Convert.ToUInt32(listViewMoneyBox.SelectedItems[0].Name.Substring(1));
                    MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(moneyboxid);
                    bool result = DB.Add_To_MoneyBox_Group(_User.UserID, moneybox);

                    if (result)
                    {
                        MessageBox.Show("تم منح صلاحية الوصول بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillMoneyBoxDataPremession();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("AddToGroup_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void UNGrantMoneyBox_MenuItem_Click(object sender, EventArgs e)
        {
 
            if (listViewMoneyBox.SelectedItems.Count == 1)
            {
                try
                {
                    uint moneyboxid = Convert.ToUInt32(listViewMoneyBox.SelectedItems[0].Name.Substring(1));
                    MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(moneyboxid);
                    bool result = DB.Remove_From_MoneyBox_Group(_User.UserID, moneybox);

                    if (result)
                    {
                        MessageBox.Show("تم الغاء صلاحية الوصول بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillMoneyBoxDataPremession();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("UNGrantMoneyBox_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listViewMoneyBox_MouseDown(object sender, MouseEventArgs e)
        {
            listViewMoneyBox.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewMoneyBox.Items)
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
                    MenuItem[] mi1;
                    if (listitem.Name.Substring(0, 1) == "A")
                        mi1 = new MenuItem[] { UNGrantMoneyBox_MenuItem };
                    else
                        mi1 = new MenuItem[] { GrantMoneyBox_MenuItem };
                    listViewMoneyBox.ContextMenu = new ContextMenu(mi1);
                }
            }
        }
        #endregion

        #region AccessFolder
        public async void FillFolderPremessionData()
        {
            try
            {
                listViewFolderPremession.Items.Clear();
                List<DatabaseInterface.AccessFolderPremession> AccessFolderPremessionlist = DB.Get_AccessFolderPremession_List(_User.UserID);
                for (int i = 0; i < AccessFolderPremessionlist.Count; i++)
                {
                    ItemObj.Objects.Folder Folder_ = AccessFolderPremessionlist[i]._Folder;

                    string id;
                    string name;
                    String path;
                    if (Folder_ == null)
                    {
                        id = "0";
                        name = "  جميع الأصناف";
                        path = "/";
                    }
                    else
                    {
                        id = Folder_.FolderID.ToString();
                        name = Folder_.FolderName;
                        path = new ItemObj.ItemObjSQL.FolderSQL(DB).GetFolderPath(Folder_);
                    }
                    ListViewItem ListViewItem_ = new ListViewItem(id);
                    ListViewItem_.Name = id;
                    ListViewItem_.SubItems.Add(name);
                    ListViewItem_.SubItems.Add(path);
                    ListViewItem_.BackColor = Color.LimeGreen;
                    listViewFolderPremession.Items.Add(ListViewItem_);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillFolderPremessionData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void UNGrant_AccessFolder_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFolderPremession.SelectedItems.Count == 1)
            {
                try
                {
                    uint folderid = Convert.ToUInt32(listViewFolderPremession.SelectedItems[0].Name);
                    ItemObj.Objects.Folder folder;
                    if (folderid == 0) folder = null;
                    else folder = new ItemObj.ItemObjSQL.FolderSQL(DB).GetFolderInfoByID(folderid);

                    bool result = DB.Remove_AccessFolderPremession(_User.UserID, folder);

                    if (result)
                    {
                        MessageBox.Show("تم الغاء صلاحية الوصول بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillFolderPremessionData();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("UNGrant_AccessFolder_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Grant_AccessFolder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Add_AccessFolderPremession_Form Add_AccessFolderPremession_Form_ = new Add_AccessFolderPremession_Form(DB, _User);
                Add_AccessFolderPremession_Form_.ShowDialog();
                if (Add_AccessFolderPremession_Form_.DialogResult == DialogResult.OK)
                {
                    FillFolderPremessionData();
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("Grant_AccessFolder_MenuItem_Click:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           
        }
        private void listViewFolderPremession_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewFolderPremession.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewFolderPremession.Items)
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
                        listViewFolderPremession.ContextMenu = new ContextMenu(new MenuItem[] { UNGrant_AccessFolder_MenuItem, new MenuItem("-"), Grant_AccessFolder_MenuItem });
                    }
                    else
                    {
                        listViewFolderPremession.ContextMenu = new ContextMenu(new MenuItem[] { Grant_AccessFolder_MenuItem });
                    }
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("listViewFolderPremession_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
        }


        #endregion

        #region AccessContainer
        public async void FillContainerPremessionData()
        {
            try
            {
                listViewContainerPremession.Items.Clear();
                List<DatabaseInterface.AccessContainerPremession> AccessContainerPremessionlist = DB.Get_AccessContainerPremession_List(_User.UserID);
                for (int i = 0; i < AccessContainerPremessionlist.Count; i++)
                {
                    Trade.Objects.TradeStoreContainer Container_ = AccessContainerPremessionlist[i].Container;

                    string id;
                    string name;
                    String path;
                    if (Container_ == null)
                    {
                        id = "0";
                        name = "  جميع حاويات التخزين";
                        path = "/";
                    }
                    else
                    {
                        id = Container_.ContainerID.ToString();
                        name = Container_.ContainerName;
                        path = new Trade.TradeSQL.TradeStoreContainerSQL(DB).GetContainerPath(Container_);
                    }
                    ListViewItem ListViewItem_ = new ListViewItem(id);
                    ListViewItem_.Name = id;
                    ListViewItem_.SubItems.Add(name);
                    ListViewItem_.SubItems.Add(path);
                    ListViewItem_.BackColor = Color.LimeGreen;
                    listViewContainerPremession.Items.Add(ListViewItem_);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillContainerPremessionData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void UNGrant_AccessContainer_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewContainerPremession.SelectedItems.Count == 1)
            {
                try
                {
                    uint Containerid = Convert.ToUInt32(listViewContainerPremession.SelectedItems[0].Name);
                    TradeStoreContainer Container_;
                    if (Containerid == 0) Container_ = null;
                    else Container_ = new Trade.TradeSQL.TradeStoreContainerSQL(DB).GetContainerBYID (Containerid);

                    bool result = DB.Remove_AccessContainerPremession(_User.UserID, Container_);

                    if (result)
                    {
                        MessageBox.Show("تم الغاء صلاحية الوصول بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillContainerPremessionData();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("UNGrant_AccessContainer_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Grant_AccessContainer_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Add_AccessContainerPremession_Form Add_AccessContainerPremession_Form_ = new Add_AccessContainerPremession_Form(DB, _User);
                Add_AccessContainerPremession_Form_.ShowDialog();
                if (Add_AccessContainerPremession_Form_.DialogResult == DialogResult.OK)
                {
                    FillContainerPremessionData ();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Grant_AccessContainer_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listViewContainerPremession_MouseDown(object sender, MouseEventArgs e)
        {
            try { 
            listViewContainerPremession.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewContainerPremession.Items)
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
                    listViewContainerPremession.ContextMenu = new ContextMenu(new MenuItem[] { UNGrant_AccessContainer_MenuItem, new MenuItem("-"), Grant_AccessContainer_MenuItem });
                }
                else
                {
                    listViewContainerPremession.ContextMenu = new ContextMenu(new MenuItem[] {Grant_AccessContainer_MenuItem });
                }
            }
        }
            catch(Exception ee)
            {
                MessageBox.Show("listViewContainerPremession_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
}
        #endregion

        #region AccessSellType
        public async void FillSellTypePremessionData()
        {
            try
            {
                listViewSellTypePremession.Items.Clear();
                List<DatabaseInterface.AccessSellTypePremession> AccessSellTypePremessionlist = DB.Get_AccessSellTypePremession_List(_User.UserID);
                for (int i = 0; i < AccessSellTypePremessionlist.Count; i++)
                {

                    ListViewItem ListViewItem_ = new ListViewItem(AccessSellTypePremessionlist[i]._SellType.SellTypeID.ToString());
                    ListViewItem_.Name = (AccessSellTypePremessionlist[i].Access ? "A" : "N") + AccessSellTypePremessionlist[i]._SellType.SellTypeID.ToString();
                    ListViewItem_.SubItems.Add(AccessSellTypePremessionlist[i]._SellType.SellTypeName);
                    ListViewItem_.SubItems.Add(AccessSellTypePremessionlist[i].Access ? "يملك الصلاحية" : "لا يملك الصلاحية");
                    ListViewItem_.BackColor = (AccessSellTypePremessionlist[i].Access ? Color.LimeGreen : Color.Orange);
                    listViewSellTypePremession.Items.Add(ListViewItem_);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillSellTypePremessionData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void UNGrant_SellType_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewSellTypePremession.SelectedItems.Count == 1)
            {
                try
                {
                    uint selltypeid = Convert.ToUInt32(listViewSellTypePremession.SelectedItems[0].Name.Substring(1));
                    SellType SellType_ = new Trade.TradeSQL.SellTypeSql(DB).GetSellTypeinfo(selltypeid);
                    bool result = DB.Remove_AccessSellTypePremession(_User.UserID, SellType_);

                    if (result)
                    {
                        MessageBox.Show("تم منح صلاحية الوصول بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillPremession();
                        FillSellTypePremessionData();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("AddToGroup_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Grant_SellType_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewSellTypePremession.SelectedItems.Count == 1)
            {
                try
                {
                    uint selltypeid = Convert.ToUInt32(listViewSellTypePremession.SelectedItems[0].Name.Substring(1));
                    SellType SellType_ = new Trade.TradeSQL.SellTypeSql(DB).GetSellTypeinfo(selltypeid);
                    bool result = DB.Add_AccessSellTypePremession(_User.UserID, SellType_);

                    if (result)
                    {
                        MessageBox.Show("تم منح صلاحية الوصول بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillPremession();
                        FillSellTypePremessionData();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("AddToGroup_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void listViewSellTypePremession_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

            
            listViewSellTypePremession.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewSellTypePremession.Items)
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
                    MenuItem[] mi1;
                    if (listitem.Name.Substring(0, 1) == "A")
                        mi1 = new MenuItem[] { UNGrant_SellType_MenuItem };
                    else
                        mi1 = new MenuItem[] { Grant_SellType_MenuItem };
                    listViewSellTypePremession.ContextMenu = new ContextMenu(mi1);
                }
            }
        }
            catch(Exception ee)
            {
                MessageBox.Show("listViewSellTypePremession_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
}
        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
