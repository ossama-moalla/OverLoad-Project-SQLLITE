
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
    public partial class EmployeeMent_Form : Form
    {
        DatabaseInterface DB;
        Part _Part;
        EmployeeMent _EmployeeMent;
        Part LastUsedPart;

        List<EmployeeMent> EmployeeMentAssignReportList = new List<EmployeeMent>();


        MenuItem OpenEmployeeMentAssign_MenuItem;
        //MenuItem UpdateEmployeeMentAssign_MenuItem;
        //MenuItem DeleteEmployeeMentAssign_MenuItem;
        //private bool Changed_;
        //public bool Changed
        //{
        //    get { return Changed_; }
        //}
        public EmployeeMent_Form(DatabaseInterface db, Part Part_)
        {

            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            _Part = Part_;
            InitializeComponent();
            if (_Part != null)
            {
                textBoxPartName.Text = _Part.PartName;
                textBoxPartID.Text = _Part.PartID.ToString();
                dateTimePickerPartCreateDate.Value = _Part.CreateDate;
            }
            else
            {
                textBoxPartName.Text = DB.COMPANY.PartName;
                textBoxPartID.Text = "0";
                dateTimePickerPartCreateDate.Visible = false;
            }
    
           
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "اضافة";
            labelEmployeementid.Visible = false ;
            textBoxEmployeeID.Visible = false;
            FillComboboxLevel(null);

        }
        public EmployeeMent_Form(DatabaseInterface db, EmployeeMent EmployeeMent_, bool Edit)
        {
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _EmployeeMent = EmployeeMent_;
            _Part = _EmployeeMent._Part;
            if (_Part != null)
            {
                textBoxPartName.Text = _Part.PartName;
                textBoxPartID.Text = _Part.PartID.ToString();
                dateTimePickerPartCreateDate.Value = _Part.CreateDate;
            }
            else
            {
                textBoxPartName.Text = DB.COMPANY.PartName;
                textBoxPartID.Text = "0";
                dateTimePickerPartCreateDate.Visible = false;
            }




            LoadForm(Edit);
        }
        public void LoadForm(bool Edit)
        {
            try
            {
                if (_EmployeeMent  == null) return;
                OpenEmployeeMentAssign_MenuItem = new System.Windows.Forms.MenuItem("فتح تفاصيل ", OpenEmployeeMentDocument_MenuItem_Click);
                //UpdateEmployeeMentAssign_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditEmployeeMentDocument_MenuItem_Click);
                // DeleteEmployeeMentAssign_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteEmployeeMentDocument_MenuItem_Click);

                //EmployeeMentAssignReportList = new EmployeeMentAssignSQL(DB).Get_EmployeeMentAssignReport_List_ForEmployeeMent(_EmployeeMent);
                RefreshEmployeeMentAssignReportList();
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                _Part = _EmployeeMent._Part;
                labelEmployeementid.Visible = true;
                textBoxEmployeeID.Visible = true ;
                textBoxEmployeeID.Text = _EmployeeMent.EmployeeMentID.ToString();
                textBoxEmployeementName.Text = _EmployeeMent.EmployeeMentName;
                dateTimePickerEmployeeCreateDate.Value = _EmployeeMent.CreateDate; ;
                FillComboboxLevel(_EmployeeMent.Level);
                textBoxEmployeeID.ReadOnly = true;


                if (Edit)
                {
                    textBoxEmployeementName.ReadOnly = false;
                    dateTimePickerEmployeeCreateDate.Enabled  = true  ;
                    comboBoxLevel.Enabled = true;
                    this.listViewAssignReport.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewEmployeeMentAssign_MouseDoubleClick);
                    this.listViewAssignReport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewEmployeeMentAssign_MouseDown);

                }
                else
                {
                    this.listViewAssignReport.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewEmployeeMentAssign_MouseDoubleClick);

                    textBoxEmployeementName.ReadOnly = true ;
                    dateTimePickerEmployeeCreateDate.Enabled = false ;
                    comboBoxLevel.Enabled = false ;
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("حصل خطأ اثناء تحميل الصفحة:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public async void FillComboboxLevel(EmployeeMentLevel EmployeeMentLevel_)
        {
            comboBoxLevel .Items.Clear();

            int selected_index = 0;
            try
            {
                List<EmployeeMentLevel> EmployeeMentLevelList = new EmployeeMentLevelSQL(DB).Get_EmployeeMentLevel_List();
                for (int i = 0; i < EmployeeMentLevelList.Count; i++)
                {
                    ComboboxItem item = new ComboboxItem(EmployeeMentLevelList[i].LevelName , EmployeeMentLevelList[i].LevelID );
                    comboBoxLevel.Items.Add(item);
                    if (EmployeeMentLevel_ != null && EmployeeMentLevel_.LevelID  == EmployeeMentLevelList[i].LevelID ) selected_index = i;
                }
                comboBoxLevel.SelectedIndex = selected_index;

            }
            catch
            { }
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if (_Part == null) return;
            //ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
            //ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

            //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
            //string SellType_ = ComboboxItem_selltype.Text;
            ComboboxItem comboboxitem = (ComboboxItem)comboBoxLevel.SelectedItem;
            EmployeeMentLevel EmployeeMentLevel_ = new EmployeeMentLevel(comboboxitem.Value, comboboxitem.Text);

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                  bool success =
                        new EmployeeMentSQL (DB).Add_EmployeeMent (textBoxEmployeementName.Text ,dateTimePickerEmployeeCreateDate .Value , EmployeeMentLevel_.LevelID , _Part);

                    if (success)
                    {
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult  = DialogResult.OK;
                        LoadForm(true);

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر الاضافة " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_EmployeeMent  != null)
                    {

                        bool success =
                       new EmployeeMentSQL(DB).Update_EmployeeMent (_EmployeeMent.EmployeeMentID, textBoxEmployeementName.Text, dateTimePickerEmployeeCreateDate.Value, EmployeeMentLevel_.LevelID );
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //_MaintenanceFault = new MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(_MaintenanceFault.AccessoryID);
                            this.DialogResult = DialogResult.OK;
                            LoadForm(true);

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
        #region EmployeeMentAssigns
        //private void DeleteEmployeeMentAssign_MenuItem_Click(object sender, EventArgs e)
        //{

        //    //try
        //    //{

        //    //    DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        //    //    if (dd != DialogResult.OK) return;
        //    //    string s = listViewAssignReport.SelectedItems[0].Name.Substring(0, 1);
        //    //    if (s == "D")
        //    //    {
        //    //        uint sid = Convert.ToUInt32(listViewAssignReport.SelectedItems[0].Name.Substring(1));
        //    //        bool success = new DiagnosticOPR_MissedFaultItem_EmployeeMentAssignSQL(DB).Delete_DiagnosticOPR_MissedFaultItem_EmployeeMentAssign (sid);
        //    //        if (success)
        //    //        {
        //    //            MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //            EmployeeMentAssignReportList = new MaintenanceEmployeeMentAssignSQL(DB).GetMissedFaultItem_EmployeeMentAssignReportList(_EmployeeMent);
        //    //            RefreshEmployeeMentAssignReportList(EmployeeMentAssignReportList);

        //    //        }
        //    //        else
        //    //        {
        //    //            MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    //        }
        //    //    }
        //    //    else if (s == "M")
        //    //    {
        //    //        uint sid = Convert.ToUInt32(listViewAssignReport.SelectedItems[0].Name.Substring(1));
        //    //        bool success = new Fault_MissedFaultItem_EmployeeMentAssignSQL(DB).Delete__Fault_MissedFaultItem_EmployeeMentAssign(sid);
        //    //        if (success)
        //    //        {
        //    //            MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //            EmployeeMentAssignReportList = new MaintenanceEmployeeMentAssignSQL(DB).GetMissedFaultItem_EmployeeMentAssignReportList(_EmployeeMent);
        //    //            RefreshEmployeeMentAssignReportList(EmployeeMentAssignReportList);

        //    //        }
        //    //        else
        //    //        {
        //    //            MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    //        }
        //    //    }

        //    //}
        //    //catch
        //    //{

        //    //}
        //}
        private void EditEmployeeMentDocument_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewAssignReport.SelectedItems.Count > 0)
            {
                try
                {
                    string s = listViewAssignReport.SelectedItems[0].Name.Substring(0, 1);
                    if (s == "A")
                    {
                        uint id = Convert.ToUInt32(listViewAssignReport.SelectedItems[0].Name.Substring(1));
                        Document Document_ = new DocumentSQL(DB).Get_Document_Info_BYID(id);
                        AssignForm AssignForm_ = new AssignForm(DB, Document_, true );
                        AssignForm_.ShowDialog();
                        if (AssignForm_.DialogResult == DialogResult.OK)
                        {
                            RefreshEmployeeMentAssignReportList();
                        }
                        AssignForm_.Dispose();
                    }
                    else
                    {
                        uint id = Convert.ToUInt32(listViewAssignReport.SelectedItems[0].Name.Substring(1));
                        Document Document_ = new DocumentSQL(DB).Get_Document_Info_BYID(id);
                        EndAssignForm EndAssignForm_ = new EndAssignForm(DB, Document_, true );
                        EndAssignForm_.ShowDialog();
                        if (EndAssignForm_.DialogResult == DialogResult.OK)
                        {
                            RefreshEmployeeMentAssignReportList();
                        }
                        EndAssignForm_.Dispose();
                    }

                }
                catch (Exception ee)
                {
                    MessageBox.Show("EditEmployeeMentDocument_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void OpenEmployeeMentDocument_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewAssignReport.SelectedItems.Count > 0)
            {
                try
                {
                    string s = listViewAssignReport.SelectedItems[0].Name.Substring(0, 1);
                    if (s == "A")
                    {
                        uint id = Convert.ToUInt32(listViewAssignReport.SelectedItems[0].Name.Substring(1));
                        Document Document_ = new DocumentSQL(DB).Get_Document_Info_BYID(id);
                        AssignForm AssignForm_ = new AssignForm(DB, Document_, false);
                        AssignForm_.ShowDialog();
                        if (AssignForm_.DialogResult == DialogResult.OK)
                        {
                            RefreshEmployeeMentAssignReportList();
                        }
                        AssignForm_.Dispose();
                    }
                    else
                    {
                        uint id = Convert.ToUInt32(listViewAssignReport.SelectedItems[0].Name.Substring(1));
                        Document Document_ = new DocumentSQL(DB).Get_Document_Info_BYID(id);
                        EndAssignForm EndAssignForm_ = new EndAssignForm(DB, Document_, false);
                        EndAssignForm_.ShowDialog();
                        if (EndAssignForm_.DialogResult == DialogResult.OK)
                        {
                            RefreshEmployeeMentAssignReportList();
                        }
                        EndAssignForm_.Dispose();
                    }

                }
                catch(Exception ee)
                {
                    MessageBox.Show("OpenEmployeeMentDocument_MenuItem_Click:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
                }

            }
        }
        private async void RefreshEmployeeMentAssignReportList()
        {

            listViewAssignReport.Items.Clear();
            List<Document> TempDocument = new DocumentSQL(DB).Get_DocumentReport_List();
            List<Document> DocumentList = new List<Document>();
            List<Document> Assign_DocumentList = TempDocument.Where(x => x._EmployeeMent != null && x._EmployeeMent.EmployeeMentID == _EmployeeMent.EmployeeMentID).ToList();
            List<Document> EndAssign_DocumentList = TempDocument.Where(x => x.TargetDocument != null && Assign_DocumentList.Select(y => y.DocumentID).Contains(x.TargetDocument.DocumentID)).ToList();
            DocumentList.AddRange(Assign_DocumentList);
            DocumentList.AddRange(EndAssign_DocumentList);
            for (int i = 0; i < DocumentList.Count; i++)
            {
                string Header = "";
                Color color;
                string type = "";
                if (DocumentList[i].DocumentType == Document.ASSIGN_DOCUMENT)
                {
                    type = "تكليف";
                    color = Color.LimeGreen ;
                    Header = "A";
                }
                else if (DocumentList[i].DocumentType == Document.ENDASSIGN_DOCUMENT )
                {
                    type = "انهاء تكليف";
                    color = Color.Orange;
                    Header = "E";
                }
                else
                { type = ""; color = Color.White; }
                ListViewItem ListViewItem_ = new ListViewItem(type);
                ListViewItem_.BackColor = color;
                ListViewItem_.Name = Header + DocumentList[i].DocumentID .ToString();
                ListViewItem_.SubItems.Add(DocumentList[i].DocumentID.ToString());
                ListViewItem_.SubItems.Add(DocumentList[i].DocumentDate.ToShortDateString ());
                ListViewItem_.SubItems.Add(DocumentList[i]._Employee .EmployeeName);
                listViewAssignReport.Items.Add(ListViewItem_);

            }

        }
        private void listViewEmployeeMentAssign_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewAssignReport .SelectedItems.Count > 0)
            {
                OpenEmployeeMentAssign_MenuItem.PerformClick();
            }
        }
        private void listViewEmployeeMentAssign_MouseDown(object sender, MouseEventArgs e)
        {
            listViewAssignReport.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewAssignReport.Items)
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


                    MenuItem[] mi1 = new MenuItem[] { OpenEmployeeMentAssign_MenuItem };
                    listViewAssignReport.ContextMenu = new ContextMenu(mi1);


                }
                else
                {

                    //MenuItem[] mi = new MenuItem[] { ad };
                    //listViewAssignReport.ContextMenu = new ContextMenu(mi);

                }

            }

        }
        public async void AdjustlistViewEmployeeMentAssign_ColumnsWidth()
        {
            try
            {
                listViewAssignReport.Columns[0].Width = 150;
                listViewAssignReport.Columns[1].Width = 60;
                listViewAssignReport.Columns[2].Width = 150;
                listViewAssignReport.Columns[3].Width = listViewAssignReport.Width - 380;

            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewEmployeeMentAssign_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void listViewAssignReport_Resize(object sender, EventArgs e)
        {
            AdjustlistViewEmployeeMentAssign_ColumnsWidth();
        }
        #endregion

    }
}
