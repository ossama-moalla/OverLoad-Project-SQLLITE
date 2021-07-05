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
    public partial class EmployeeForm : Form
    {
        MenuItem SetEmployeeImage_MenuItem;
        MenuItem UnsetEmployeeImage_MenuItem;

        System.Windows.Forms.MenuItem Open_Document_MenuItem;
        System.Windows.Forms.MenuItem Edit_Document_MenuItem;
        System.Windows.Forms.MenuItem delete_Document_MenuItem;
        System.Windows.Forms.MenuItem RefreshMenuItem;

        MenuItem OpenQualification_MenuItem;
        MenuItem AddQualification_MenuItem;
        MenuItem UpdateQualification_MenuItem;
        MenuItem DeleteQualification_MenuItem;

        MenuItem OpenCertificate_MenuItem;
        MenuItem AddCertificate_MenuItem;
        MenuItem UpdateCertificate_MenuItem;
        MenuItem DeleteCertificate_MenuItem;

        MenuItem Create_JobStart_MenuItem;
        MenuItem Create_EndJobStart_MenuItem;
        MenuItem Create_Assign_MenuItem;
        MenuItem Create_EndAssign_MenuItem;

        DatabaseInterface DB;
        Employee _Employee;
        byte[] _EmployeeImage;
        List<Document > DocumemtList = new List<Document>();
        //List<EmployeeMentAssignReport> EmployeeMentAssignReportList = new List<EmployeeMentAssignReport>();
        //List<MaintenanceFaultReport > FaultReportList = new List<MaintenanceFaultReport>();
        //List<Missed_Fault_Item> Missed_Fault_Item_List = new List<Missed_Fault_Item>();
        //List<Employee_Accessory> AccessoryList = new List<Employee_Accessory>();

        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public EmployeeForm(DatabaseInterface db)
        {
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            comboBoxGender.SelectedIndex = 0;
            InitializeMenuItems();
            //DiagnosticOPRReport.InitializeDiagnosticOPRListViewColumns(ref listViewSubDiagnosticOPR);
            //MaintenanceFaultReport.InitializeFaultReportListViewColumns(ref listViewFault);
            //Missed_Fault_Item.InitializeMissed_Fault_ListViewColumns(ref listViewMissedFaultItem);
            //AdjustListViewItemsOUTColumnsWidth();
            tabControl1.TabPages[0].Enabled = false;
            tabControl1.TabPages[1].Enabled = false;
            FillComboboxMaritalStatus(null);
            ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, null);
            pictureBoxEmployeeImage.Image = Properties.Resources.EmployeeImage;
        }
        public void InitializeMenuItems()
        {
            RefreshMenuItem = new System.Windows.Forms.MenuItem("تحديث", Refresh_MenuItem_Click);

            SetEmployeeImage_MenuItem = new MenuItem("ضبط صورة الموظف", SetEmployeeImage_MenuItem_Click);
            UnsetEmployeeImage_MenuItem = new MenuItem("حذف صورة الموظف", UNSetEmployeeImage_MenuItem_Click);


            Create_JobStart_MenuItem = new System.Windows.Forms.MenuItem("انشاء امر مباشرة", Create_JobStart_MenuItem_Click);
            Create_EndJobStart_MenuItem = new System.Windows.Forms.MenuItem(" انهاء المباشرة الحالية", Create_EndJobStart_MenuItem_Click);
            Create_Assign_MenuItem = new System.Windows.Forms.MenuItem("انشاء امر تكليف بوظيفة", Create_Assign_MenuItem_Click);
            Create_EndAssign_MenuItem = new System.Windows.Forms.MenuItem(" انهاء التكليف", Create_EndAssign_MenuItem_Click);


            Open_Document_MenuItem = new System.Windows.Forms.MenuItem("استعراض", Open_Document_MenuItem_Click);
            Edit_Document_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_Document_MenuItem_Click);
            delete_Document_MenuItem  = new System.Windows.Forms.MenuItem("حذف", Delete_Document_MenuItem_Click);


            OpenQualification_MenuItem = new System.Windows.Forms.MenuItem("استعراض تفاصيل", Open_Qualification_MenuItem_Click);
            AddQualification_MenuItem = new System.Windows.Forms.MenuItem("اضافة خبرة", Create_Qualification_MenuItem_Click);
            UpdateQualification_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_Qualification_MenuItem_Click);
            DeleteQualification_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_Qualification_MenuItem_Click);

            OpenCertificate_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", Open_Certificate_MenuItem_Click);
            AddCertificate_MenuItem = new System.Windows.Forms.MenuItem("اضافة شهادة", Create_Certificate_MenuItem_Click);
            UpdateCertificate_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_Certificate_MenuItem_Click);
            DeleteCertificate_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_Certificate_MenuItem_Click);

            //Open_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", Open_MissedFault_Item_MenuItem_Click);
            //Add_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("اضافة عنصر مفقود او تالف", Add_MissedFault_Item_MenuItem_Click);
            //Update_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", Edit_MissedFault_Item_MenuItem_Click);
            //Delete_MissedFault_Item_MenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_MissedFault_Item_MenuItem_Click);

        }
        public EmployeeForm(DatabaseInterface db, Employee Employee_, bool Edit)
        {
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            _Employee = Employee_;
            InitializeMenuItems();
            LoadForm(Edit);
            //DiagnosticOPRReport.InitializeDiagnosticOPRListViewColumns (ref listViewSubDiagnosticOPR);
            //MaintenanceFaultReport.InitializeFaultReportListViewColumns(ref listViewFault);
            //Missed_Fault_Item.InitializeMissed_Fault_ListViewColumns(ref listViewMissedFaultItem);

        }
      public void LoadForm(bool Edit)
        {
            try
            {
                if (_Employee == null) return;
                tabControl1.TabPages[0].Enabled = true;
                tabControl1.TabPages[1].Enabled = true;
                ProgramGeneralMethods.FillComboBoxCurrency(ref comboBoxCurrency, DB, _Employee.SalaryCurrency );
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                textboxEmployeeID.Text = _Employee.EmployeeID.ToString();
                textBoxName.Text = _Employee.EmployeeName;
                dateTimePickerBirthDate.Value = _Employee.BirthDate;
                textBoxNationalID.Text = _Employee.NationalID;
                FillComboboxMaritalStatus(_Employee._MaritalStatus);
                textBoxMobile.Text = _Employee.Mobile;
                textBoxPhone.Text = _Employee.Phone;
                textBoxEmailAddress.Text = _Employee.EmailAddress;
                textBoxAddress.Text = _Employee.Address;
                if (_Employee.Gender == Employee.GENDER_MALE) comboBoxGender.SelectedIndex = 0;
                else comboBoxGender.SelectedIndex = 1;
               
                DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                RefreshDocumentList(DocumemtList);
                List<EmployeeCertificate> CertificateList = new EmployeeCertificateSQL(DB).Get_Certificate_List(_Employee);
                RefreshCertificateList(CertificateList);
                List<EmployeeQualification> QualificationList = new EmployeeQualificationSQL(DB).Get_Qualification_List(_Employee);
                RefreshQualificationList(QualificationList);
                GetEmployeeImage();
                if (Edit)
                {
                    menuStrip1.Enabled = true;

                    buttonSave.Visible = true;
                    textBoxName.ReadOnly = false;
                    dateTimePickerBirthDate.Enabled = true;
                    textBoxNationalID.ReadOnly = false;
                    comboBoxMartialstate.Enabled = true;
                    textBoxMobile.ReadOnly = false;
                    textBoxPhone.ReadOnly = false;
                    textBoxEmailAddress.ReadOnly = false;
                    textBoxAddress.ReadOnly = false;
                    textBoxReport.ReadOnly = false;
                    comboBoxGender.Enabled = true;
                    comboBoxCurrency.Enabled = true ;
                    this.listViewDocuments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDocuments_MouseDoubleClick);
                    this.listViewDocuments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewDocuments_MouseDown);

                    this.listViewQualifications.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewQualifications_MouseDoubleClick);
                    this.listViewQualifications.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewQualifications_MouseDown);

                    this.listViewCertificates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewCertificates_MouseDoubleClick);
                    this.listViewCertificates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewCertificates_MouseDown);
                    this.pictureBoxEmployeeImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEmployeeImage_MouseDown);

                }
                else
                {
                    this.listViewDocuments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDocuments_MouseDoubleClick);
                    this.listViewQualifications.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewQualifications_MouseDoubleClick);
                    this.listViewCertificates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewCertificates_MouseDoubleClick);

                    menuStrip1.Enabled = false;
                    buttonSave.Visible = false;
                    textBoxName.ReadOnly = true;
                    dateTimePickerBirthDate.Enabled = false;
                    textBoxNationalID.ReadOnly = true;
                    comboBoxMartialstate.Enabled = false;
                    textBoxMobile.ReadOnly = true;
                    textBoxPhone.ReadOnly = true;
                    textBoxEmailAddress.ReadOnly = true;
                    textBoxAddress.ReadOnly = true;
                    textBoxReport.ReadOnly = true ;
                    comboBoxGender.Enabled = false;
                    comboBoxCurrency.Enabled = false;

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(":حدث خطأ " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void FillComboboxMaritalStatus(MaritalStatus MaritalStatus_)
        {
            comboBoxMartialstate.Items.Clear();
            int selected_index = 0;
            try
            {
                List<MaritalStatus> MaritalStatusList = MaritalStatus.Get_MaritalStatus_List();
                for (int i = 0; i < MaritalStatusList.Count; i++)
                {
                    ComboboxItem item = new ComboboxItem(MaritalStatusList[i].MaritalStatusName, MaritalStatusList[i].MaritalStatusID);
                    comboBoxMartialstate.Items.Add(item);
                    if (MaritalStatus_ != null && MaritalStatus_.MaritalStatusID == MaritalStatusList[i].MaritalStatusID) selected_index = i;
                }
                comboBoxMartialstate.SelectedIndex = selected_index;

            }
            catch
            { }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ComboboxItem comboboxitem = (ComboboxItem)comboBoxMartialstate.SelectedItem;
            MaritalStatus MaritalStatus_ = new MaritalStatus(comboboxitem.Value, comboboxitem.Text);
            ComboboxItem comboboxitemCurrency = (ComboboxItem)comboBoxCurrency .SelectedItem;
            Currency Currency_ = new AccountingObj.AccountingSQL.CurrencySQL(DB).GetCurrencyINFO_ByID (comboboxitemCurrency.Value);

            bool gender;
            if (comboBoxGender.SelectedIndex == 0) gender = Employee.GENDER_MALE;
            else gender = Employee.GENDER_FEMALE;

            if (buttonSave.Name == "buttonAdd")
            {
                try
                {

                    Employee Employee_ = new EmployeeSQL(DB).AddEmployee
                      (textBoxName.Text, gender, dateTimePickerBirthDate.Value, textBoxNationalID.Text, MaritalStatus_, textBoxMobile.Text
                      , textBoxPhone.Text, textBoxEmailAddress.Text, textBoxAddress.Text, textBoxReport.Text, Currency_.CurrencyID);
                    if (Employee_ != null)
                    {
                        _Employee = Employee_;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        LoadForm(true );

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر انشاء عملية الصيانة " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_Employee != null)
                    {
                        bool success = new EmployeeSQL(DB).UpdateEmpolyee
                          (_Employee.EmployeeID, textBoxName.Text, gender, dateTimePickerBirthDate.Value, textBoxNationalID.Text, MaritalStatus_, textBoxMobile.Text
                      , textBoxPhone.Text, textBoxEmailAddress.Text, textBoxAddress.Text, textBoxReport.Text, Currency_.CurrencyID);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _Employee = new EmployeeSQL(DB).GetEmployeeInforBYID(_Employee.EmployeeID);
                            this.Changed_ = true;
                            LoadForm(true );
                        }
                        //else MessageBox.Show("لم يتم الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذرالحفظ  " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }
        private void tabControl1_Resize(object sender, EventArgs e)
        {
            //AdjustlistView_EmployeeMent_Employee_Report_ColumnsWidth();
            //AdjustlistViewEmployeesColumnsWidth();
            AdjustlistViewDocuments_ColumnsWidth();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            //AdjustlistViewEmployeesColumnsWidth();
            AdjustlistViewDocuments_ColumnsWidth();
            //AdjustlistView_EmployeeMent_Employee_Report_ColumnsWidth();
            this.tabControl1.Resize += new System.EventHandler(this.tabControl1_Resize);

        }
        #region EmployeeDocuments
        private void Delete_Document_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewDocuments.SelectedItems.Count > 0)
                {

                    DialogResult dd = MessageBox.Show("هل انت متاكد من حذف المستند؟  :"  , "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;
                    uint sid = Convert.ToUInt32(listViewDocuments.SelectedItems[0].Name);
                    bool success = new DocumentSQL (DB).Delete_Document (sid);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح");
                        DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                        RefreshDocumentList(DocumemtList);
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_Document_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Edit_Document_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewDocuments.SelectedItems.Count > 0)
                {

                    uint sid = Convert.ToUInt32(listViewDocuments.SelectedItems[0].Name);
                    Document Document_ = new DocumentSQL(DB).Get_Document_Info_BYID(sid);
                    bool Document_Chnged = false;
                    switch (Document_.DocumentType )
                    {
                        case Document.JOBSTART_DOCUMENT:
                            JobStartForm JobStartForm_ = new JobStartForm(DB, Document_,true );
                            JobStartForm_.ShowDialog();
                            if (JobStartForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                        case Document.ENDJOBSTART_DOCUMENT:
                            EndJobStartForm EndJobStartForm_ = new EndJobStartForm(DB, Document_, true);
                            EndJobStartForm_.ShowDialog();
                            if (EndJobStartForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                        case Document.ASSIGN_DOCUMENT :
                            AssignForm AssignForm_ = new AssignForm(DB, Document_, true);
                            AssignForm_.ShowDialog();
                            if (AssignForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                        case Document.ENDASSIGN_DOCUMENT :
                            EndAssignForm EndAssignForm_ = new EndAssignForm(DB, Document_, true);
                            EndAssignForm_.ShowDialog();
                            if (EndAssignForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                    }

                    if (Document_Chnged)
                    {
                        DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                        RefreshDocumentList(DocumemtList);
                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_Document_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Open_Document_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewDocuments.SelectedItems.Count > 0)
                {

                    uint sid = Convert.ToUInt32(listViewDocuments.SelectedItems[0].Name);
                    Document Document_ = new DocumentSQL(DB).Get_Document_Info_BYID(sid);
                    bool Document_Chnged = false;
                    switch (Document_.DocumentType)
                    {
                        case Document.JOBSTART_DOCUMENT:
                            JobStartForm JobStartForm_ = new JobStartForm(DB, Document_, false );
                            JobStartForm_.ShowDialog();
                            if (JobStartForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                        case Document.ENDJOBSTART_DOCUMENT:
                            EndJobStartForm EndJobStartForm_ = new EndJobStartForm(DB, Document_, false );
                            EndJobStartForm_.ShowDialog();
                            if (EndJobStartForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                        case Document.ASSIGN_DOCUMENT:
                            AssignForm AssignForm_ = new AssignForm(DB, Document_, false );
                            AssignForm_.ShowDialog();
                            if (AssignForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                        case Document.ENDASSIGN_DOCUMENT:
                            EndAssignForm EndAssignForm_ = new EndAssignForm(DB, Document_, false );
                            EndAssignForm_.ShowDialog();
                            if (EndAssignForm_.DialogResult == DialogResult.OK)
                                Document_Chnged = true;
                            break;
                    }

                    if (Document_Chnged)
                    {
                        DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                        RefreshDocumentList(DocumemtList);
                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_JobStart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                JobStartForm JobStartForm_ = new JobStartForm(DB, _Employee);
                JobStartForm_.ShowDialog();
                if (JobStartForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;
                    DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                    RefreshDocumentList(DocumemtList);
                }
                JobStartForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateJobStartOPR_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_EndJobStart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                List<Document> jobstartdocument = DocumemtList.Where(x => x.DocumentType == Document.JOBSTART_DOCUMENT ).ToList(); ;
                if (jobstartdocument.Count == 0)
                {
                    MessageBox.Show("لم يتم انشاء مباشرات بعد" , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                EndJobStartForm EndJobStartForm_ = new EndJobStartForm(DB, jobstartdocument[jobstartdocument.Count -1]);
                EndJobStartForm_.ShowDialog();
                if (EndJobStartForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;

                    DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                    RefreshDocumentList(DocumemtList);
                }
                EndJobStartForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_EndJobStart_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_Assign_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                List<Document> jobstartdocument = DocumemtList.Where(x => x.DocumentType == Document.JOBSTART_DOCUMENT).ToList(); ;
                if (jobstartdocument.Count == 0)
                {
                    MessageBox.Show("لم يتم انشاء مباشرات بعد", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AssignForm AssignForm_ = new AssignForm(DB, jobstartdocument[jobstartdocument.Count - 1]);
                AssignForm_.ShowDialog();
                if (AssignForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;

                    DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                    RefreshDocumentList(DocumemtList);
                }
                AssignForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_Assign_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_EndAssign_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                uint documentid = Convert.ToUInt32(listViewDocuments.SelectedItems[0].Name);
                Document Document_ = new DocumentSQL(DB).Get_Document_Info_BYID(documentid);
                if (Document_.DocumentType != Document.ASSIGN_DOCUMENT)
                {
                    MessageBox.Show("خطأ برمجي : المستند ليس مستند تكليف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                List<Document> t = DocumemtList.Where(x => x.DocumentType == Document.ENDASSIGN_DOCUMENT && x.TargetDocument.DocumentID == Document_.DocumentID).ToList();
                if (t.Count > 0)
                {
                    MessageBox.Show("المستند تم انهائه سابقا من خلال المستند رقم:" + t[0].DocumentID.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                EndAssignForm EndAssignForm_ = new EndAssignForm(DB, Document_ );
                EndAssignForm_.ShowDialog();
                if (EndAssignForm_.DialogResult == DialogResult.OK)
                {
                    this.Changed_ = true;

                    DocumemtList = new DocumentSQL(DB).Get_Employee_Document_List(_Employee);
                    RefreshDocumentList(DocumemtList);
                }
                EndAssignForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_EndAssign_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void RefreshDocumentList(List<Document> DocumentList)
        {
            listViewDocuments.Items.Clear();

            for (int i = 0; i < DocumentList.Count; i++)
            {
                ListViewItem ListViewItem__ = new ListViewItem(DocumentList[i].GetDocumentDesc());

                ListViewItem__.SubItems.Add(DocumentList[i].DocumentID.ToString());
                ListViewItem__.SubItems.Add(DocumentList[i].DocumentDate.ToShortDateString());

                ListViewItem__.SubItems.Add(DocumentList[i].ExecuteDate.ToShortDateString());
                if (DocumentList[i]._EmployeeMent != null)
                    ListViewItem__.SubItems.Add(DocumentList[i]._EmployeeMent.EmployeeMentName);
                else
                    ListViewItem__.SubItems.Add("");
                switch (DocumentList[i].DocumentType)
                {
                    case Document.JOBSTART_DOCUMENT:
                        ListViewItem__.Name =  DocumentList[i].DocumentID.ToString();
                        ListViewItem__.BackColor = Color.LimeGreen;
                        break;
                    case Document.ENDJOBSTART_DOCUMENT:
                        ListViewItem__.Name =  DocumentList[i].DocumentID.ToString();
                        ListViewItem__.BackColor = Color.Orange;
                        ListViewItem__.SubItems.Add("انهاء امر المباشرة ذو الرقم :" + DocumentList[i].TargetDocument.DocumentID
                           + " تاريخ: " + DocumentList[i].TargetDocument.DocumentDate);

                        break;
                    case Document.ASSIGN_DOCUMENT:
                        ListViewItem__.Name = DocumentList[i].DocumentID.ToString();
                        ListViewItem__.BackColor = Color.PaleGreen;
                        break;
                    case Document.ENDASSIGN_DOCUMENT:
                        ListViewItem__.Name =  DocumentList[i].DocumentID.ToString();
                        ListViewItem__.BackColor = Color.Bisque;
                        ListViewItem__.SubItems.Add("انهاء امر التكليف ذو الرقم :" + DocumentList[i].TargetDocument.DocumentID
                          + " تاريخ: " + DocumentList[i].TargetDocument.DocumentDate);

                        break;
                }
                listViewDocuments.Items.Add(ListViewItem__);

            }
        }

        public async void AdjustlistViewDocuments_ColumnsWidth()
        {
            try
            {
                listViewDocuments.Columns[0].Width = 250;//desc
                listViewDocuments.Columns[1].Width = 150;//id
                listViewDocuments.Columns[2].Width = 150;//date
                listViewDocuments.Columns[3].Width = 150;//executedate
                listViewDocuments.Columns[4].Width = 250;//employeement
                listViewDocuments.Columns[5].Width = listViewDocuments.Width -955;//employeement


            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("AdjustlistViewDocuments_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private async void Refresh_MenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    List<EmployeesReport> EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
            //    RefreshEmployeesReportList(EmployeesReportList);
            //    List<Document> DocumentList_ = new DocumentSQL(DB).Get_DocumentReport_List();
            //    RefreshDocumentList(DocumentList_);
            //    List<EmployeeMent_Employee_Report> EmployeeMent_Employee_ReportList_ = new CompanyReportSQL(DB).Get_EmployeeMent_Employee_Report_List();
            //    Refresh_EmployeeMent_Employee_ReportList(EmployeeMent_Employee_ReportList_);
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show("RefreshEmployeesOPR_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void listViewDocuments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewDocuments.SelectedItems.Count > 0)
            {
                Open_Document_MenuItem.PerformClick();
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


                        List<MenuItem> mi1 = new List<MenuItem>();
                        mi1.Add(Edit_Document_MenuItem);
                        uint sid = Convert.ToUInt32(listitem.Name);
                        List<Document> l1 = DocumemtList.Where(x => x.DocumentID == sid).ToList(); ;
                        DateTime t = l1[0].ExecuteDate;
                        List<Document> l2 = DocumemtList.Where(x => x.ExecuteDate > t).ToList(); ;
                        if (l2.Count == 0)
                            mi1.Add(delete_Document_MenuItem);
                        mi1.Add(new MenuItem("-"));
                        if (l1[0].DocumentType == Document.ASSIGN_DOCUMENT)
                        {
                            List<Document> l3 = DocumemtList.Where(x => x.DocumentType == Document.ENDASSIGN_DOCUMENT && x.TargetDocument.DocumentID == l1[0].DocumentID).ToList(); ;
                            if (l3.Count == 0)
                                mi1.Add(Create_EndAssign_MenuItem);
                        }
                        mi1.Add(new MenuItem("-"));
                        //if (l1[0].DocumentType == Document.JOBSTART_DOCUMENT )
                        //{
                        //    List<Document> l3 = DocumemtList.Where(x => x.DocumentType == Document.ENDJOBSTART_DOCUMENT && x.TargetDocument.DocumentID == l1[0].DocumentID).ToList(); ;
                        //    if (l3.Count == 0)
                        //        mi1.Add(Create_EndJobStart_MenuItem);
                        //}
                        int jobstartcount = DocumemtList.Where(x => x.DocumentType == Document.JOBSTART_DOCUMENT).ToList().Count;
                        int endjobstartcount = DocumemtList.Where(x => x.DocumentType == Document.ENDJOBSTART_DOCUMENT).ToList().Count;
                        if (jobstartcount > endjobstartcount)
                        {
                            mi1.Add(Create_Assign_MenuItem);
                            mi1.Add(Create_EndJobStart_MenuItem);

                        }
                        else
                            mi1.Add(Create_JobStart_MenuItem);
                        listViewDocuments.ContextMenu = new ContextMenu(mi1.ToArray());


                    }
                    else
                    {
                        List<MenuItem> mi1 = new List<MenuItem>();
                        int jobstartcount = DocumemtList.Where(x => x.DocumentType == Document.JOBSTART_DOCUMENT).ToList().Count;
                        int endjobstartcount = DocumemtList.Where(x => x.DocumentType == Document.ENDJOBSTART_DOCUMENT).ToList().Count;
                        if (jobstartcount > endjobstartcount)
                        {
                            mi1.Add(Create_Assign_MenuItem);
                            mi1.Add(Create_EndJobStart_MenuItem);

                        }
                        else
                            mi1.Add(Create_JobStart_MenuItem);
                        listViewDocuments.ContextMenu = new ContextMenu(mi1.ToArray());


                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewDocuments_MouseDown" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

           

        }

        #endregion
        #region  EmployeeImage
        private async void GetEmployeeImage()
        {
            _EmployeeImage = (new EmployeeSQL(DB)).GetEmployeeImage(_Employee.EmployeeID);
            if (_EmployeeImage == null)
            {

                pictureBoxEmployeeImage.Image = Properties.Resources.EmployeeImage;
            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(_EmployeeImage);
                pictureBoxEmployeeImage.Image = Image.FromStream(ms);
            }
        }
        private void pictureBoxEmployeeImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MenuItem[] Mi;
                pictureBoxEmployeeImage .ContextMenu = null;
                if (_EmployeeImage == null)
                {
                    Mi = new MenuItem[] { SetEmployeeImage_MenuItem };
                }
                else
                    Mi = new MenuItem[] { SetEmployeeImage_MenuItem, UnsetEmployeeImage_MenuItem  };

                pictureBoxEmployeeImage .ContextMenu = new ContextMenu(Mi);

            }
        }

        private void UNSetEmployeeImage_MenuItem_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("هل انت متاكد من حذف صورة الموظف!", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (d != DialogResult.OK) return;
            (new EmployeeSQL  (DB)).UnSetEmployeeImage (_Employee .EmployeeID);
            GetEmployeeImage();
        }
        private async  void SetEmployeeImage_MenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Reset();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg) | *.jpg; *.jpeg;";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image image = Image.FromFile(openFileDialog1.FileName);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bool success = (new EmployeeSQL (DB)).SetEmployeeImage (_Employee.EmployeeID, ms.ToArray());
                    if (success) GetEmployeeImage ();
                    else throw new Exception();
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل ضبط صورة المظف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
        #endregion
        #region EmployeeQualification
        private void Delete_Qualification_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                    DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;

                    bool success = new EmployeeQualificationSQL (DB).Delete_Qualification (_Employee.EmployeeID, listViewQualifications.SelectedItems[0].Text);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        List <EmployeeQualification > QualificationList= new EmployeeQualificationSQL(DB).Get_Qualification_List(_Employee);
                        RefreshQualificationList (QualificationList);

                    }
                    else
                    {
                        MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_Qualification_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Edit_Qualification_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewQualifications.SelectedItems.Count > 0)
                {

                    EmployeeQualification EmployeeQualification_ = new EmployeeQualificationSQL(DB).Get_Qualification_Info(_Employee, listViewQualifications.SelectedItems[0].Text );
                    QualificationForm QualificationForm_ = new QualificationForm(DB, EmployeeQualification_, true );
                    QualificationForm_.ShowDialog();
                    if (QualificationForm_.DialogResult == DialogResult.OK)
                    {
                        List<EmployeeQualification> QualificationList = new EmployeeQualificationSQL(DB).Get_Qualification_List(_Employee);
                        RefreshQualificationList(QualificationList);
                    }
                    QualificationForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_Qualification_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Open_Qualification_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewQualifications.SelectedItems.Count > 0)
                {

                    EmployeeQualification EmployeeQualification_ = new EmployeeQualificationSQL(DB).Get_Qualification_Info(_Employee, listViewQualifications.SelectedItems[0].Text);
                    QualificationForm QualificationForm_ = new QualificationForm(DB, EmployeeQualification_, false);
                    QualificationForm_.ShowDialog();
                    if (QualificationForm_.DialogResult == DialogResult.OK)
                    {
                        List<EmployeeQualification> QualificationList = new EmployeeQualificationSQL(DB).Get_Qualification_List(_Employee);
                        RefreshQualificationList(QualificationList);
                    }
                    QualificationForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_Qualification_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_Qualification_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                QualificationForm QualificationForm_ = new QualificationForm(DB, _Employee);
                QualificationForm_.ShowDialog();
                if (QualificationForm_.DialogResult == DialogResult.OK)
                {
                    List<EmployeeQualification> QualificationList = new EmployeeQualificationSQL(DB).Get_Qualification_List(_Employee);
                    RefreshQualificationList(QualificationList);
                }
                QualificationForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_Qualification_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void RefreshQualificationList(List<EmployeeQualification > QualificationList)
        {
            listViewQualifications.Items.Clear();

            for (int i = 0; i < QualificationList.Count; i++)
            {
                ListViewItem ListViewItem__ = new ListViewItem(QualificationList[i].QualificationDesc );
                ListViewItem__.Name =  QualificationList[i].QualificationDesc;
                ListViewItem__.SubItems.Add(QualificationList[i].StartDate .ToShortDateString ());
                ListViewItem__.SubItems.Add(QualificationList[i].EndDate .ToShortDateString());
                ListViewItem__.SubItems.Add(QualificationList[i].Notes );
                
                listViewQualifications.Items.Add(ListViewItem__);

            }
        }
  
        private void listViewQualifications_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewQualifications.SelectedItems.Count > 0)
            {
                OpenQualification_MenuItem.PerformClick();
            }
        }
        private void listViewQualifications_MouseDown(object sender, MouseEventArgs e)
        {
            listViewQualifications.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewQualifications.Items)
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

                    MenuItem[] mi1 = new MenuItem[] { OpenQualification_MenuItem ,UpdateQualification_MenuItem
                        ,DeleteQualification_MenuItem,new MenuItem("-") ,AddQualification_MenuItem  };
                    listViewQualifications.ContextMenu = new ContextMenu(mi1.ToArray());
                }
                else
                {

                    MenuItem[] mi1 = new MenuItem[] { AddQualification_MenuItem  };
                    listViewQualifications.ContextMenu = new ContextMenu(mi1.ToArray());
                }

            }

        }

        #endregion
        #region EmployeeCertificate
        private void Delete_Certificate_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;

                bool success = new EmployeeCertificateSQL(DB).Delete_Certificate(_Employee.EmployeeID, listViewCertificates.SelectedItems[0].Text);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<EmployeeCertificate> CertificateList = new EmployeeCertificateSQL(DB).Get_Certificate_List(_Employee);
                    RefreshCertificateList(CertificateList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Delete_Certificate_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Edit_Certificate_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewCertificates.SelectedItems.Count > 0)
                {

                    EmployeeCertificate EmployeeCertificate_ = new EmployeeCertificateSQL(DB).Get_Certificate_Info(_Employee, listViewCertificates.SelectedItems[0].Text);
                    CertificateForm CertificateForm_ = new CertificateForm(DB, EmployeeCertificate_, true);
                    CertificateForm_.ShowDialog();
                    if (CertificateForm_.DialogResult == DialogResult.OK)
                    {
                        List<EmployeeCertificate> CertificateList = new EmployeeCertificateSQL(DB).Get_Certificate_List(_Employee);
                        RefreshCertificateList(CertificateList);
                    }
                    CertificateForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_Certificate_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Open_Certificate_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewCertificates.SelectedItems.Count > 0)
                {

                    EmployeeCertificate EmployeeCertificate_ = new EmployeeCertificateSQL(DB).Get_Certificate_Info(_Employee, listViewCertificates.SelectedItems[0].Text);
                    CertificateForm CertificateForm_ = new CertificateForm(DB, EmployeeCertificate_, false);
                    CertificateForm_.ShowDialog();
                    if (CertificateForm_.DialogResult == DialogResult.OK)
                    {
                        List<EmployeeCertificate> CertificateList = new EmployeeCertificateSQL(DB).Get_Certificate_List(_Employee);
                        RefreshCertificateList(CertificateList);
                    }
                    CertificateForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_Certificate_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Create_Certificate_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CertificateForm CertificateForm_ = new CertificateForm(DB, _Employee);
                CertificateForm_.ShowDialog();
                if (CertificateForm_.DialogResult == DialogResult.OK)
                {
                    List<EmployeeCertificate> CertificateList = new EmployeeCertificateSQL(DB).Get_Certificate_List(_Employee);
                    RefreshCertificateList(CertificateList);
                }
                CertificateForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Create_Certificate_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void RefreshCertificateList(List<EmployeeCertificate> CertificateList)
        {
            listViewCertificates.Items.Clear();

            for (int i = 0; i < CertificateList.Count; i++)
            {
                ListViewItem ListViewItem__ = new ListViewItem(CertificateList[i].CertificatesDesc );
                ListViewItem__.Name = CertificateList[i].CertificatesDesc ;
                ListViewItem__.SubItems.Add(CertificateList[i].University );
                ListViewItem__.SubItems.Add(CertificateList[i].StartDate.ToShortDateString());
                ListViewItem__.SubItems.Add(CertificateList[i].EndDate.ToShortDateString());
                ListViewItem__.SubItems.Add(CertificateList[i].Notes);

                listViewCertificates.Items.Add(ListViewItem__);

            }
        }

        private void listViewCertificates_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewCertificates.SelectedItems.Count > 0)
            {
                OpenCertificate_MenuItem.PerformClick();
            }
        }
        private void listViewCertificates_MouseDown(object sender, MouseEventArgs e)
        {
            listViewCertificates.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listViewCertificates.Items)
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

                    MenuItem[] mi1 = new MenuItem[] { OpenCertificate_MenuItem ,UpdateCertificate_MenuItem
                        ,DeleteCertificate_MenuItem,new MenuItem("-") ,AddCertificate_MenuItem  };
                    listViewCertificates.ContextMenu = new ContextMenu(mi1.ToArray());
                }
                else
                {

                    MenuItem[] mi1 = new MenuItem[] { AddCertificate_MenuItem };
                    listViewCertificates.ContextMenu = new ContextMenu(mi1.ToArray());
                }

            }

        }

        #endregion

        private void البياناتالماليةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeSalaryClauseForm EmployeeSalaryClauseForm_ = new Forms.EmployeeSalaryClauseForm(DB, _Employee,true );
            EmployeeSalaryClauseForm_.ShowDialog();
        }

        private void pictureBoxEmployeeImage_MouseDown_1(object sender, MouseEventArgs e)
        {

        }
    }
}
