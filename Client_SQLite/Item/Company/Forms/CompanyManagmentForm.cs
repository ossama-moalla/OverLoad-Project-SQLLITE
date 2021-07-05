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
    public partial class CompanyManagmentForm : Form
    {
        MenuItem OpenEmployee_Page_MenuItem;
        MenuItem AddEmployee_MenuItem;
        MenuItem EditEmployee_MenuItem;
        MenuItem DeleteEmployee_MenuItem;

        System.Windows.Forms.MenuItem RefreshMenuItem;

        System.Windows.Forms.MenuItem CreatePartMenuItem;
        System.Windows.Forms.MenuItem EditPartMenuItem;
        System.Windows.Forms.MenuItem DeletePartMenuItem;

        System.Windows.Forms.MenuItem CreateEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem OpenEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem EditEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem DeleteEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem CutEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem PasteEmployeeMentMenuItem;

        ContextMenu TreeviewPartMenuItem = new ContextMenu();

        delegate void TreeviewVoidDelegate();
        Part _Part;
        PartSQL Partsql;
        List<Part> PartsList = new List<Part>();
        List<EmployeeMent> EmployeeMentsList = new List<EmployeeMent>();
        List<EmployeeMent> Moved_EmployeeMentList;
        DatabaseInterface DB;
        List<EmployeesReport> EmployeesReportList;
        public CompanyManagmentForm(DatabaseInterface db)
        {
            InitializeComponent();
            InitializeMenuItems();

            TreeviewPartMenuItem.MenuItems.AddRange(new MenuItem[] { CreatePartMenuItem, EditPartMenuItem, DeletePartMenuItem, new MenuItem("-"), CreatePartMenuItem });
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            _Part = null;
            Moved_EmployeeMentList = new List<EmployeeMent>();
            Partsql = new CompanySQL.PartSQL(DB);
            FillTreeViewPart();
            OpenPart();

            EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
            RefreshEmployeesReportList();

            List<Document> DocumentList_ = new DocumentSQL(DB).Get_DocumentReport_List();
            RefreshDocumentList(DocumentList_);
            List<EmployeeMent_Employee_Report> EmployeeMent_Employee_ReportList_ = new CompanyReportSQL(DB).Get_EmployeeMent_Employee_Report_List();
            Refresh_EmployeeMent_Employee_ReportList(EmployeeMent_Employee_ReportList_);


        }
        public void InitializeMenuItems()
        {
            RefreshMenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);


            OpenEmployee_Page_MenuItem = new System.Windows.Forms.MenuItem("فتح صفحة الموظف", OpenEmployee_Page_MenuItem_Click);
            AddEmployee_MenuItem = new System.Windows.Forms.MenuItem("اضافة موظف جديد", AddEmployee_MenuItem_Click);
            EditEmployee_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditEmployee_MenuItem_Click);
            DeleteEmployee_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteEmployee_MenuItem_Click);

            CreateEmployeeMentMenuItem = new System.Windows.Forms.MenuItem("انشاء وظيفة", CreateEmployeeMent_MenuItem_Click);
            OpenEmployeeMentMenuItem = new System.Windows.Forms.MenuItem("فتح", OpenEmployeeMent_MenuItem_Click); ;
            EditEmployeeMentMenuItem = new System.Windows.Forms.MenuItem("تعديل", EditEmployeeMent_MenuItem_Click); ;
            DeleteEmployeeMentMenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteEmployeeMent_MenuItem_Click); ;
            CutEmployeeMentMenuItem = new MenuItem("قص", CutEmployeeMent_MenuItem_Click);
            PasteEmployeeMentMenuItem = new MenuItem("لصق", PasteEmployeeMent_MenuItem_Click);

            CreatePartMenuItem = new System.Windows.Forms.MenuItem("انشاء قسم", CreatePart_MenuItem_Click);
            EditPartMenuItem = new System.Windows.Forms.MenuItem("تعديل", EditPart_MenuItem_Click); ;
            DeletePartMenuItem = new System.Windows.Forms.MenuItem("حذف", DeletePart_MenuItem_Click); ;

            //OpenAccessory_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", OpenAccessory_MenuItem_Click);
            //AddAccessory_MenuItem = new System.Windows.Forms.MenuItem("اضافة ملحق صيانة", AddAccessory_MenuItem_Click);
            //EditAccessory_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditAccessory_MenuItem_Click);
            //DeleteAccessory_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteAccessory_MenuItem_Click);

            //OpenDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية الفحص", OpenDiagnosticOPR_MenuItem_Click);
            //AddDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("اضافة عملية فحص فرعية", AddDiagnosticOPR_MenuItem_Click);
            //UpdateDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditDiagnosticOPR_MenuItem_Click);
            //DeleteDiagnosticOPR_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteDiagnosticOPR_MenuItem_Click);

            //OpenEmployee_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", OpenEmployee_MenuItem_Click);
            //AddEmployee_MenuItem = new System.Windows.Forms.MenuItem("اضافة عطل", AddEmployee_MenuItem_Click);
            //UpdateEmployee_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditEmployee_MenuItem_Click);
            //DeleteEmployee_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteEmployee_MenuItem_Click);

            //Open_MissedEmployee_Item_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", Open_MissedEmployee_Item_MenuItem_Click);
            //Add_MissedEmployee_Item_MenuItem = new System.Windows.Forms.MenuItem("اضافة عنصر مفقود او تالف", Add_MissedEmployee_Item_MenuItem_Click);
            //Update_MissedEmployee_Item_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_MissedEmployee_Item_MenuItem_Click);
            //Delete_MissedEmployee_Item_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_MissedEmployee_Item_MenuItem_Click);

        }
        #region Employee
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshEmployeesReportList();
        }
        private async void RefreshEmployeesReportList()
        {
            try
            {
                listViewEmployees.Items.Clear();
                for (int i = 0; i < EmployeesReportList.Count; i++)
                {
                    if (checkBox1.Checked && !(EmployeesReportList[i].EmployeeStateCode == EmployeesReport.EMPLOYEE_ON_WORK_NO_EMPLOYEEMENT
                        || EmployeesReportList[i].EmployeeStateCode == EmployeesReport.EMPLOYEE_ON_WORK_ON_EMPLOYEEMENT))
                        continue;
                    ListViewItem ListViewItem_ = new ListViewItem();

                    ListViewItem_.Name = EmployeesReportList[i].EmployeeID.ToString();
                    ListViewItem_.SubItems.Add(EmployeesReportList[i].EmployeeID.ToString());
                    ListViewItem_.SubItems.Add(EmployeesReportList[i].EmployeeName);
                    if (EmployeesReportList[i].Gender == Employee.GENDER_MALE)
                        ListViewItem_.SubItems.Add("ذكر");
                    else
                        ListViewItem_.SubItems.Add("انثى");
                    //double age_days = (DateTime.Now - EmployeesReportList[i].BirthDate).TotalDays;
                    //double  age = System.Math.Round((age_days / 365), 0);
                    ListViewItem_.SubItems.Add(EmployeesReportList[i].BirthDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(EmployeesReportList[i].NationalID);
                    ListViewItem_.SubItems.Add(EmployeesReportList[i].MaritalStatusName);
                    //ListViewItem_.SubItems.Add(EmployeesReportList[i].Mobile  );
                    //ListViewItem_.SubItems.Add(EmployeesReportList[i].Phone );
                    //ListViewItem_.SubItems.Add(EmployeesReportList[i].EmailAddress);
                    //ListViewItem_.SubItems.Add(EmployeesReportList[i].Address );
                    ListViewItem_.SubItems.Add(EmployeesReportList[i].JobState);
                    ListViewItem_.SubItems.Add(EmployeesReportList[i].EmployeeMentState);
                    ListViewItem_.ImageIndex = 2;
                    switch (EmployeesReportList[i].EmployeeStateCode)
                    {
                        case EmployeesReport.EMPLOYEE_NOT_START_WORK:
                            ListViewItem_.BackColor = Color.Orange ;
                            break;
                        case EmployeesReport.EMPLOYEE_ON_WORK_ON_EMPLOYEEMENT:
                            ListViewItem_.BackColor = Color.LimeGreen ;

                            break;
                        case EmployeesReport.EMPLOYEE_ON_WORK_NO_EMPLOYEEMENT:
                            ListViewItem_.BackColor = Color.Orange ;

                            break;
                        case EmployeesReport.EMPLOYEE_LEFT_WORK:
                            ListViewItem_.BackColor = Color.LightYellow;

                            break;
                    }


                    listViewEmployees.Items.Add(ListViewItem_);

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshEmployeesReportList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void DeleteEmployee_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من حذف العطل و جميع العمليات التابعة له؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                bool success = new EmployeeSQL(DB).DeleteEmployee(sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
                    RefreshEmployeesReportList();

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteSubDiagnosticOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void EditEmployee_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewEmployees.SelectedItems.Count > 0)
                {
                    uint Employeeid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                    Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(Employeeid);
                    EmployeeForm EmployeeForm_ = new EmployeeForm(DB, Employee_, true);
                    EmployeeForm_.ShowDialog();
                    if (EmployeeForm_.Changed)
                    {
                        EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
                        RefreshEmployeesReportList();
                    }
                    EmployeeForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditEmployee_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenEmployee_Page_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewEmployees.SelectedItems.Count > 0)
                {
                    uint Employeeid = Convert.ToUInt32(listViewEmployees.SelectedItems[0].Name);
                    Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(Employeeid);
                    EmployeeForm EmployeeForm_ = new EmployeeForm(DB, Employee_, false);
                    EmployeeForm_.ShowDialog();
                    if (EmployeeForm_.Changed)
                    {
                        EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
                        RefreshEmployeesReportList();
                    }
                    EmployeeForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenEmployee_Page_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddEmployee_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeForm EmployeeForm_ = new EmployeeForm(DB);
                DialogResult d = EmployeeForm_.ShowDialog();
                if (EmployeeForm_.Changed)
                {

                    EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
                    RefreshEmployeesReportList();
                }
                EmployeeForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddEmployee_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewEmployees_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewEmployees.SelectedItems.Count > 0)
            {
                OpenEmployee_Page_MenuItem.PerformClick();
            }
        }
        private void listViewEmployees_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewEmployees.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewEmployees.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenEmployee_Page_MenuItem
                        , EditEmployee_MenuItem , DeleteEmployee_MenuItem, new MenuItem("-"), AddEmployee_MenuItem, new MenuItem("-"), RefreshMenuItem  };
                        listViewEmployees.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddEmployee_MenuItem, new MenuItem("-"), RefreshMenuItem };
                        listViewEmployees.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewEmployees_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
 
        #endregion

        private void الاقساموالوظائفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowEmployeeMentsForm ShowEmployeeMentsForm_ = new Forms.ShowEmployeeMentsForm(DB, null);
                ShowEmployeeMentsForm_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(":" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #region Part_EmployeeMents


        public async void FillTreeViewPart()
        {
            try
            {
                List<Part> PartsParents = new List<Part>();

                PartsParents = Partsql.GetPartChilds(null);
                treeViewParts.Nodes.Clear();
                TreeNode r = new TreeNode(DB.COMPANY.PartName);
                r.Name = "null";
                r.ImageIndex = 0;
                treeViewParts.Nodes.Add(r);
                while (PartsParents.Count != 0)
                {
                    List<Part> PartsChilds = new List<Part>();
                    for (int i = 0; i < PartsParents.Count; i++)
                    {
                        TreeNode n = new TreeNode(PartsParents[i].PartName);
                        n.Name = PartsParents[i].PartID.ToString();
                        n.ImageIndex = 0;
                        string parentid = "";
                        if (PartsParents[i].ParentPartID == null)
                            parentid = "null";
                        else parentid = PartsParents[i].ParentPartID.ToString();
                        TreeNode[] nodes = treeViewParts.Nodes.Find(parentid, true);
                        nodes[0].Nodes.Add(n);
                        PartsChilds.AddRange(Partsql.GetPartChilds(PartsParents[i]));

                    }

                    PartsParents.Clear();
                    PartsParents = PartsChilds;

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillTreeViewPart:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void TreeViewPart_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // Make sure this is the right button.
                if (e.Button != MouseButtons.Right) return;

                // Select this node.
                TreeNode node = treeViewParts.GetNodeAt(e.X, e.Y);
                treeViewParts.SelectedNode = node;

                //See if we got a node.
                if (node == null) return;

                // See what kind of object this is and
                // display the appropriate popup menu.

                TreeviewPartMenuItem.Show(treeViewParts, new Point(e.X, e.Y));

            }
            catch (Exception ee)
            {
                MessageBox.Show("TreeViewPart_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void treeViewParts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (treeViewParts.SelectedNode != null)
                    {
                        try
                        {
                            _Part = Partsql.GetPartInfoByID(Convert.ToUInt32(treeViewParts.SelectedNode.Name));
                        }
                        catch
                        {
                            _Part = null;
                        }

                        OpenPart();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("treeViewParts_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public async void OpenPart()
        {
            try
            {
                //Thread thread1, thread2;
                //thread1 = new Thread(new ThreadStart(RefreshTreeView));
                //thread1.Start();


                //thread2 = new Thread(new ThreadStart(PartIDPath));
                //thread2.Start();

                labelemployeements.Text = "الوظائف الموجودة في  :" + (_Part == null ? DB.COMPANY.PartName : _Part.PartName);
                RefreshTreeView();
                PartsList = Partsql.GetPartChilds(_Part);
                EmployeeMentsList = new EmployeeMentSQL(DB).Get_EmployeeMent_List_IN_Part(_Part);
                RefreshEmployeeMents_Parts(EmployeeMentsList);
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenPart:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        public async void RefreshTreeView()
        {
            try
            {
                if (this.treeViewParts.InvokeRequired)
                {
                    TreeviewVoidDelegate d = new TreeviewVoidDelegate(RefreshTreeView);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    string fid;
                    if (_Part == null) fid = "null";
                    else fid = _Part.PartID.ToString();
                    TreeNode[] node = treeViewParts.Nodes.Find(fid, true);
                    if (node.Length == 0) return;
                    node[0].Expand();
                    treeViewParts.SelectedNode = node[0];
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshTreeView:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public async void RefreshEmployeeMents_Parts(List<EmployeeMent> EmployeeMents)
        {
            try
            {
                listViewEmployeements.Items.Clear();

                for (int i = 0; i < EmployeeMents.Count; i++)
                {
                    ListViewItem ListViewItem__ = new ListViewItem(EmployeeMents[i].EmployeeMentID.ToString());
                    ListViewItem__.Name = EmployeeMents[i].EmployeeMentID.ToString();
                    ListViewItem__.SubItems.Add(EmployeeMents[i].EmployeeMentName);

                    ListViewItem__.SubItems.Add(EmployeeMents[i].CreateDate.ToShortDateString());

                    listViewEmployeements.Items.Add(ListViewItem__);

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshEmployeeMents_Parts:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void listViewEmployeements_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && listViewEmployeements.SelectedItems.Count > 0)
            {
                OpenEmployeeMentMenuItem.PerformClick();
            }
        }
        private void treeViewParts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (treeViewParts.SelectedNode != null)
                {
                    try
                    {
                        _Part = Partsql.GetPartInfoByID(Convert.ToUInt32(treeViewParts.SelectedNode.Name));
                    }
                    catch
                    {
                        _Part = null;
                    }

                    OpenPart();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("treeViewParts_AfterSelect:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void listViewEmployeements_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    List<MenuItem> MenuItemList = new List<MenuItem>();
                    listViewEmployeements.ContextMenu = null;
                    bool match = false;
                    ListViewItem listEmployeeMent = new ListViewItem();
                    foreach (ListViewItem EmployeeMent1 in listViewEmployeements.Items)
                    {
                        if (EmployeeMent1.Bounds.Contains(new Point(e.X, e.Y)))
                        {
                            match = true;
                            listEmployeeMent = EmployeeMent1;
                            break;
                        }
                    }
                    if (match)
                    {

                        MenuItem[] mi1 = new MenuItem[] {OpenEmployeeMentMenuItem  ,EditEmployeeMentMenuItem ,DeleteEmployeeMentMenuItem
                            , new MenuItem("-") };
                        MenuItemList.AddRange(mi1);

                        MenuItemList.Add(CutEmployeeMentMenuItem);

                    }
                    //////////////

                    if (Moved_EmployeeMentList.Count > 0)
                    {
                        MenuItemList.Add(PasteEmployeeMentMenuItem);
                    }
                    MenuItem[] m_i = new MenuItem[] { new MenuItem("-"), CreatePartMenuItem, CreateEmployeeMentMenuItem, new MenuItem("-") };
                    MenuItemList.AddRange(m_i);

                    listViewEmployeements.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewEmployeements_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion
        #region ContextMenuItemEWvents
        private void CreatePart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Part> PartsInCurrentPart = Partsql.GetPartChilds(_Part);
                string name = null;
                int j = 1;
                bool Exists = true;
                name = "قسم جديد" + j;
                if (PartsInCurrentPart.Count > 0)
                {
                    while (Exists)
                    {
                        bool found = false;
                        for (int i = 0; i < PartsInCurrentPart.Count; i++)
                        {
                            if (PartsInCurrentPart[i].PartName == name)
                                found = true;
                        }
                        if (found == true)
                        { j++; name = "قسم جديد " + j; }
                        else Exists = false;
                    }
                }
                uint? p_id;
                if (_Part == null) p_id = null;
                else p_id = _Part.PartID;
                PartForm inp = new PartForm(this.DB, p_id, name);
                DialogResult dd = inp.ShowDialog();
                if (dd == DialogResult.OK)
                {

                    FillTreeViewPart();
                    OpenPart();

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("CreatePart_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void EditPart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewParts.SelectedNode != null)
                {

                    if (treeViewParts.SelectedNode.Name == "null")
                        throw new Exception("لا يمكن حذف أو تعديل العقدة الجذر");

                    Part part = Partsql.GetPartInfoByID(Convert.ToUInt32(treeViewParts.SelectedNode.Name));
                    uint? P_ID = part.ParentPartID;
                    PartForm PartForm_ = new Forms.PartForm(DB, part, true);
                    PartForm_.ShowDialog();
                    if (PartForm_.DialogResult == DialogResult.OK)
                    {
                        FillTreeViewPart();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditPart_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeletePart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (treeViewParts.SelectedNode != null)
                {
                    if (treeViewParts.SelectedNode.Name == "null")
                        throw new Exception("لا يمكن حذف أو تعديل العقدة الجذر");

                    DialogResult dd = MessageBox.Show("هل انت متاكد من حذف القسم :" + treeViewParts.SelectedNode.Text + "?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;
                    Part part = Partsql.GetPartInfoByID(Convert.ToUInt32(treeViewParts.SelectedNode.Name));
                    uint? P_ID = part.ParentPartID;
                    bool success = Partsql.DeletePart(Convert.ToUInt32(treeViewParts.SelectedNode.Name));

                    if (success)
                    {
                        FillTreeViewPart();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeletePart_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeMent_Form EmployeeMent_Form = new EmployeeMent_Form(DB, _Part);
                DialogResult d = EmployeeMent_Form.ShowDialog();
                if (d == DialogResult.OK)
                {
                    EmployeeMentsList = new EmployeeMentSQL(DB).Get_EmployeeMent_List_IN_Part(_Part);
                    RefreshEmployeeMents_Parts(EmployeeMentsList);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateEmployeeMent_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void OpenEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewEmployeements.SelectedItems.Count > 0)
                {

                    uint sid = Convert.ToUInt32(listViewEmployeements.SelectedItems[0].Name);
                    EmployeeMent EmployeeMent_ = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID(sid);
                    EmployeeMent_Form EmployeeMent_Form_ = new EmployeeMent_Form(DB, EmployeeMent_, false);
                    EmployeeMent_Form_.ShowDialog();
                    if (EmployeeMent_Form_.DialogResult == DialogResult.OK)
                    {
                        EmployeeMentsList = new EmployeeMentSQL(DB).Get_EmployeeMent_List_IN_Part(_Part);
                        RefreshEmployeeMents_Parts(EmployeeMentsList);
                    }
                    EmployeeMent_Form_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewEmployeements.SelectedItems.Count > 0)
                {

                    uint sid = Convert.ToUInt32(listViewEmployeements.SelectedItems[0].Name);
                    EmployeeMent EmployeeMent_ = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID(sid);
                    EmployeeMent_Form EmployeeMent_Form_ = new EmployeeMent_Form(DB, EmployeeMent_, true);
                    EmployeeMent_Form_.ShowDialog();
                    if (EmployeeMent_Form_.DialogResult == DialogResult.OK)
                    {
                        EmployeeMentsList = new EmployeeMentSQL(DB).Get_EmployeeMent_List_IN_Part(_Part);
                        RefreshEmployeeMents_Parts(EmployeeMentsList);
                    }
                    EmployeeMent_Form_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                DialogResult dd = MessageBox.Show("هل انت متاكد من حذف الوظيفة ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;

                uint sid = Convert.ToUInt32(listViewEmployeements.SelectedItems[0].Name);
                bool success = new EmployeeMentSQL(DB).Delete_EmployeeMent(sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EmployeeMentsList = new EmployeeMentSQL(DB).Get_EmployeeMent_List_IN_Part(_Part);
                    RefreshEmployeeMents_Parts(EmployeeMentsList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteEmployeeMent_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void PasteEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool success2 = new EmployeeMentSQL(DB).MoveEmployeeMents(_Part, Moved_EmployeeMentList);
                if (success2)
                {
                    OpenPart();
                    Moved_EmployeeMentList = new List<EmployeeMent>();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("PasteEmployeeMent_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        private void CutEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Moved_EmployeeMentList.Clear();
                for (int i = 0; i < listViewEmployeements.SelectedItems.Count; i++)
                {

                    Moved_EmployeeMentList.Add(new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID(Convert.ToUInt32(listViewEmployeements.SelectedItems[i].Name)));

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("CutEmployeeMent_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        #endregion
        #region EmployeeOPR_Log
        public async void RefreshDocumentList(List<Document> DocumentList)
        {
            try
            {
                listViewDocuments.Items.Clear();

                for (int i = 0; i < DocumentList.Count; i++)
                {
                    ListViewItem ListViewItem__ = new ListViewItem(DocumentList[i].GetDocumentDesc());

                    ListViewItem__.SubItems.Add(DocumentList[i].DocumentID.ToString());
                    ListViewItem__.SubItems.Add(DocumentList[i].DocumentDate.ToShortDateString());
                    ListViewItem__.SubItems.Add(DocumentList[i]._Employee.EmployeeName);

                    ListViewItem__.SubItems.Add(DocumentList[i].ExecuteDate.ToShortDateString());
                    if (DocumentList[i]._EmployeeMent != null)
                        ListViewItem__.SubItems.Add(DocumentList[i]._EmployeeMent.EmployeeMentName);
                    else
                        ListViewItem__.SubItems.Add("");
                    switch (DocumentList[i].DocumentType)
                    {
                        case Document.JOBSTART_DOCUMENT:
                            ListViewItem__.Name = "0" + DocumentList[i].DocumentID.ToString();
                            ListViewItem__.BackColor = Color.LimeGreen;
                            break;
                        case Document.ENDJOBSTART_DOCUMENT:
                            ListViewItem__.Name = "1" + DocumentList[i].DocumentID.ToString();
                            ListViewItem__.BackColor = Color.Orange;
                            ListViewItem__.SubItems.Add("انهاء امر المباشرة ذو الرقم :" + DocumentList[i].TargetDocument.DocumentID
                               + " تاريخ: " + DocumentList[i].TargetDocument.DocumentDate);

                            break;
                        case Document.ASSIGN_DOCUMENT:
                            ListViewItem__.Name = "2" + DocumentList[i].DocumentID.ToString();
                            ListViewItem__.BackColor = Color.PaleGreen;
                            break;
                        case Document.ENDASSIGN_DOCUMENT:
                            ListViewItem__.Name = "3" + DocumentList[i].DocumentID.ToString();
                            ListViewItem__.BackColor = Color.Bisque;
                            ListViewItem__.SubItems.Add("انهاء امر التكليف ذو الرقم :" + DocumentList[i].TargetDocument.DocumentID
                              + " تاريخ: " + DocumentList[i].TargetDocument.DocumentDate);

                            break;
                    }
                    listViewDocuments.Items.Add(ListViewItem__);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshDocumentList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void Refresh_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
                RefreshEmployeesReportList();
                List<Document> DocumentList_ = new DocumentSQL(DB).Get_DocumentReport_List();
                RefreshDocumentList(DocumentList_);
                List<EmployeeMent_Employee_Report> EmployeeMent_Employee_ReportList_ = new CompanyReportSQL(DB).Get_EmployeeMent_Employee_Report_List();
                Refresh_EmployeeMent_Employee_ReportList(EmployeeMent_Employee_ReportList_);
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshEmployeesOPR_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listViewDocuments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewDocuments.SelectedItems.Count > 0)
            {
                //OpenEmployee_Page_MenuItem.PerformClick();
            }
        }
        private void listViewDocuments_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewDocuments.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewDocuments.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { RefreshMenuItem
                       };
                        listViewDocuments.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { RefreshMenuItem };
                        listViewDocuments.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewDocuments_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        #endregion
        #region EmployeeMent_Employee_Report
        public async void Refresh_EmployeeMent_Employee_ReportList(List<EmployeeMent_Employee_Report> EmployeeMent_Employee_ReportList)
        {
            try
            {
                listViewEmployeeMent_Employee_Report.Items.Clear();

                for (int i = 0; i < EmployeeMent_Employee_ReportList.Count; i++)
                {
                    ListViewItem ListViewItem__ = new ListViewItem(EmployeeMent_Employee_ReportList[i].LevelName);

                    ListViewItem__.Name = EmployeeMent_Employee_ReportList[i].EmployeeMentID.ToString();
                    ListViewItem__.SubItems.Add(EmployeeMent_Employee_ReportList[i].EmployeeMentID.ToString());
                    ListViewItem__.SubItems.Add(EmployeeMent_Employee_ReportList[i].EmployeeMentName);
                    ListViewItem__.SubItems.Add(EmployeeMent_Employee_ReportList[i].PartName);
                    ListViewItem__.BackColor = Color.Orange;
                    if (EmployeeMent_Employee_ReportList[i].EmployeeID != null)
                    {
                        ListViewItem__.SubItems.Add(EmployeeMent_Employee_ReportList[i].EmployeeID.ToString());
                        ListViewItem__.SubItems.Add(EmployeeMent_Employee_ReportList[i].EmployeeName);
                        ListViewItem__.SubItems.Add(EmployeeMent_Employee_ReportList[i].JobstartID.ToString());
                        ListViewItem__.SubItems.Add(Convert.ToDateTime(EmployeeMent_Employee_ReportList[i].JobStartDate).ToShortDateString());
                        ListViewItem__.SubItems.Add(EmployeeMent_Employee_ReportList[i].AssignID.ToString());
                        ListViewItem__.SubItems.Add(Convert.ToDateTime(EmployeeMent_Employee_ReportList[i].AssignDate).ToShortDateString());
                        ListViewItem__.BackColor = Color.LimeGreen;
                    }

                    listViewEmployeeMent_Employee_Report.Items.Add(ListViewItem__);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Refresh_EmployeeMent_Employee_ReportList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void listView_EmployeeMent_Employee_Report_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewEmployeeMent_Employee_Report.SelectedItems.Count > 0)
            {
                OpenEmployee_Page_MenuItem.PerformClick();
            }
        }
        private void listView_EmployeeMent_Employee_Report_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewEmployeeMent_Employee_Report.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewDocuments.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { RefreshMenuItem
                       };
                        listViewEmployeeMent_Employee_Report.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { RefreshMenuItem };
                        listViewEmployeeMent_Employee_Report.ContextMenu = new ContextMenu(mi);

                    }

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("listView_EmployeeMent_Employee_Report_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion
        private void CompanyManagmentForm_Load(object sender, EventArgs e)
        {
            

        }


        private void الرواتبالشهريةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CompanySalarysForm CompanyPayOrdersForm_ = new CompanySalarysForm(DB);
                CompanyPayOrdersForm_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void سجلالرواتبوأوامرالصرفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CompanyPayOrdersForm CompanyPayOrdersForm_ = new CompanyPayOrdersForm(DB);
                CompanyPayOrdersForm_.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

  

    }

}
