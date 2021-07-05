using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.ItemObj.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Company
{
    namespace  Objects
    {
        public class Part
        {
            public uint PartID;
            public string PartName;
            public DateTime CreateDate;
            public uint? ParentPartID;

            public Part(uint PartID_,string PartName_,DateTime CreateDate_,uint? ParentPartID_)
            {
                PartID = PartID_;
                PartName = PartName_;
                CreateDate = CreateDate_;
                ParentPartID = ParentPartID_;

            }
        }
        public class EmployeeMentLevel
        {
            public uint LevelID;
            public string LevelName;
            public EmployeeMentLevel(uint LevelID_,string LevelName_)
            {
                LevelID = LevelID_;
                LevelName = LevelName_;
            }
        }
        public class EmployeeMent
        {
            public uint EmployeeMentID;
            public string EmployeeMentName;
            public DateTime CreateDate;
            public EmployeeMentLevel Level;
            public Part  _Part;
            public EmployeeMent(uint EmployeeMentID_, string EmployeeMentName_, DateTime CreateDate_,EmployeeMentLevel Level_, Part Part_)
            {
                EmployeeMentID = EmployeeMentID_;
                EmployeeMentName = EmployeeMentName_;
                CreateDate = CreateDate_;
                Level = Level_;
                _Part  = Part_;
            }
            public static  void RefreshEmployeeMents(ref System.Windows.Forms.ListView ListView_, List<EmployeeMent> EmployeeMents)
            {
                ListView_.Items.Clear();

                for (int i = 0; i < EmployeeMents.Count; i++)
                {
                    System.Windows.Forms.ListViewItem ListViewItem__ = new System.Windows.Forms.ListViewItem(EmployeeMents[i].EmployeeMentID.ToString());
                    ListViewItem__.Name = EmployeeMents[i].EmployeeMentID.ToString();
                    ListViewItem__.SubItems.Add(EmployeeMents[i].EmployeeMentName);

                    ListViewItem__.SubItems.Add(EmployeeMents[i].CreateDate.ToShortDateString());
                    ListViewItem__.SubItems.Add(EmployeeMents[i]._Part .PartName);
                    ListView_.Items.Add(ListViewItem__);
                   
                }
      
            }
            public static void AdjustlistViewEmployeeMentColumnsWidth(ref System.Windows.Forms.ListView ListView_)
            {
                if(ListView_.Columns .Count ==4)
                {
                    ListView_.Columns[0].Width = 150;
                    ListView_.Columns[1].Width = 150;
                    ListView_.Columns[2].Width = (ListView_.Width - 305) / 2;
                    ListView_.Columns[3].Width = (ListView_.Width - 305) / 2;
                }

            }
            internal static void InitializeEmployeeMentListViewColumns(ref ListView ListView_)
            {
                try
                {
                    ListView_.Name = "DiagnosticOPRListView";
                    ListView_.Columns.Clear();
                    ListView_.Columns.Add("رقم الوظيفة");
                    ListView_.Columns.Add("تاريخ الانشاء");
                    ListView_.Columns.Add("اسم الوظيفة");
                    ListView_.Columns.Add("القسم");
       
                    EmployeeMent .AdjustlistViewEmployeeMentColumnsWidth (ref ListView_);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("InitializeDiagnosticOPRListViewColumns" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
        public class EmployeeMent_Employee_Report
        {
            public uint LevelID;
            public string LevelName;
            public uint EmployeeMentID;
            public string EmployeeMentName;
            public string PartName;
            public uint? EmployeeID;
            public string EmployeeName;
            public uint? JobstartID;
            public DateTime? JobStartDate;
            public uint? AssignID;
            public DateTime? AssignDate;
            public EmployeeMent_Employee_Report( uint LevelID_, string LevelName_, uint EmployeeMentID_,string EmployeeMentName_, string PartName_,
             uint? EmployeeID_, string EmployeeName_, uint? JobstartID_, DateTime? JobStartDate_, uint? AssignID_, DateTime? AssignDate_)
            {
                LevelID = LevelID_;
                LevelName= LevelName_;
                EmployeeMentID = EmployeeMentID_;
                EmployeeMentName = EmployeeMentName_;
                PartName = PartName_;
                EmployeeID = EmployeeID_;
                EmployeeName = EmployeeName_;
                JobstartID = JobstartID_;
                JobStartDate = JobStartDate_;
                AssignID = AssignID_;
                AssignDate = AssignDate_;
            }
            internal static List<EmployeeMent_Employee_Report> Get_EmployeeMent_Employee_Report_List_From_DataTable(DataTable table)
            {
                try
                {
                    List<EmployeeMent_Employee_Report> list = new List<EmployeeMent_Employee_Report>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        uint LevelID = Convert.ToUInt32(table.Rows[i]["LevelID"]);
                        string  LevelName =table.Rows[i]["LevelName"].ToString ();

                        uint EmployeeMentID = Convert.ToUInt32(table.Rows[i]["EmployeeMentID"]);
                        string EmployeeMentName = table.Rows[i]["EmployeeMentName"].ToString();
                        string  PartName = table.Rows[i]["PartName"].ToString ();
                        uint? EmployeeID;
                        
                        if (table.Rows[i]["EmployeeID"] != DBNull.Value)
                            EmployeeID = Convert.ToUInt32(table.Rows[i]["EmployeeID"]);
                        else
                            EmployeeID = null;
                        string EmployeeName;
                        
                        EmployeeName = table.Rows[i]["EmployeeName"].ToString();
                        uint? JobstartID;
                       
                        if (table.Rows[i]["JobstartID"] != DBNull.Value)

                            JobstartID = Convert.ToUInt32(table.Rows[i]["JobstartID"]);
                        else
                            JobstartID = null;
                        DateTime? JobStartDate;
                       
                        if (table.Rows[i]["JobStartDate"] != DBNull.Value)
                            JobStartDate = Convert.ToDateTime (table.Rows[i]["JobStartDate"]);
                        else
                            JobStartDate = null;

                        uint? AssignID;
                        if (table.Rows[i]["AssignID"] != DBNull.Value)
                            AssignID = Convert.ToUInt32(table.Rows[i]["AssignID"]);

                        else

                            AssignID = null;

                        DateTime? AssignDate;

                        if (table.Rows[i]["AssignDate"] != DBNull.Value)

                            AssignDate = Convert.ToDateTime(table.Rows[i]["AssignDate"]);
                        else
                            AssignDate = null;

                        list.Add(new EmployeeMent_Employee_Report(  LevelID,LevelName,
             EmployeeMentID,EmployeeMentName, PartName, EmployeeID, EmployeeName,JobstartID, JobStartDate, AssignID,AssignDate));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_EmployeeMent_Employee_Report_List_From_DataTable:" + "," + ee.Message);
                }
            }

        }
        public class Document
        {
            public const uint JOBSTART_DOCUMENT = 1;
            public const uint ENDJOBSTART_DOCUMENT = 2;
            public const uint ASSIGN_DOCUMENT = 3;
            public const uint ENDASSIGN_DOCUMENT = 4;

            public uint DocumentID;
            public DateTime DocumentDate;
            public Employee _Employee;
            public uint DocumentType;
            public DateTime ExecuteDate;
            public Document TargetDocument;
            public EmployeeMent _EmployeeMent;
            public string Details;
            public Document(uint DocumentID_, DateTime DocumentDate_, Employee Employee_,
                uint DocumentType_, DateTime ExecuteDate_, Document TargetDocument_, EmployeeMent EmployeeMent_, string Details_)
            {
                DocumentID = DocumentID_;
                DocumentDate = DocumentDate_;
                _Employee = Employee_;
                DocumentType = DocumentType_;
                ExecuteDate = ExecuteDate_;
                TargetDocument = TargetDocument_;
                _EmployeeMent = EmployeeMent_;
                Details = Details_;
            }
            public string GetDocumentDesc()
            {
                switch (this.DocumentType)
                {
                    case Document.JOBSTART_DOCUMENT:
                        return "أمر مباشرة";
                    case Document.ENDJOBSTART_DOCUMENT:
                        return "أمر انهاء مباشرة";

                    case Document.ASSIGN_DOCUMENT:

                        return "أمر تكليف وظيفة";
                    case Document.ENDASSIGN_DOCUMENT:
                        return "أمر انهاء تكليف";
                    default:
                        return "مستند غير معروف";

                }
            }
        }
        public class MaritalStatus
        {
            public const uint SINGLE = 0;
            public const uint MARRIED = 1;
            public const uint divorce = 2;
            public const uint Widowed = 3;

            public uint MaritalStatusID;
            public string MaritalStatusName;
            public   MaritalStatus(uint MaritalStatusID_, string MaritalStatusName_)
            {
                MaritalStatusID = MaritalStatusID_;
                MaritalStatusName = MaritalStatusName_;
            }
            public static MaritalStatus Get_MaritalStatus_BY_ID(uint ID)
            {
                switch (ID)
                {
                    case MaritalStatus.SINGLE:
                        return new MaritalStatus(ID, "عازب");
                    case MaritalStatus.MARRIED  :
                        return new MaritalStatus(ID, "متزوج");
                    case MaritalStatus.divorce :
                        return new MaritalStatus(ID, "مطلق");
                    case MaritalStatus.Widowed :
                        return new MaritalStatus(ID, "أرمل");
                    default:
                        return new MaritalStatus(ID, "غير محدد");
                }
            }
            public static List <MaritalStatus > Get_MaritalStatus_List()
            {
                List<MaritalStatus> list = new List<MaritalStatus>();
                list.Add(new MaritalStatus (0,"عازب"));
                list.Add(new MaritalStatus(1, "متزوج"));
                list.Add(new MaritalStatus(2, "مطلق"));
                list.Add(new MaritalStatus(3, "أرمل"));
                return list;
            }
        }
        public class Employee
        {
            public const bool GENDER_MALE = true;
            public const bool GENDER_FEMALE = false ;


            public uint EmployeeID;
            public string EmployeeName;
            public bool Gender;
            public DateTime  BirthDate;
            public string  NationalID ;
            public  MaritalStatus _MaritalStatus;
            public string Mobile;
            public string Phone;
            public string EmailAddress;
            public string Address;
            public string Report;
            public Currency SalaryCurrency;
            public Employee(uint EmployeeID_,string EmployeeName_,bool Gender_, DateTime BirthDate_
                , string NationalID_, MaritalStatus MaritalStatus_
                , string Mobile_,string Phone_, string EmailAddress_,string Address_ , string Report_, Currency SalaryCurrency_)
            {
                EmployeeID = EmployeeID_;
                EmployeeName = EmployeeName_;
                BirthDate = BirthDate_;
                NationalID = NationalID_;
                _MaritalStatus = MaritalStatus_;
                Gender = Gender_;
                Mobile = Mobile_;
                Phone = Phone_;
                EmailAddress=  EmailAddress_;
                 Address = Address_;
                Report = Report_;
                SalaryCurrency = SalaryCurrency_;
            }

        }
        public class Employee_User
        {
            public Employee _Employee;
            public DatabaseInterface.User _user;
            public Employee_User (Employee Employee_, DatabaseInterface.User User_)
            {
                _Employee = Employee_;
                _user = User_;
            }
        }
        public class EmployeesReport
        {
            public const uint EMPLOYEE_NOT_START_WORK = 1;
            public const uint EMPLOYEE_LEFT_WORK =2;
            public const uint EMPLOYEE_ON_WORK_NO_EMPLOYEEMENT=3;
            public const uint EMPLOYEE_ON_WORK_ON_EMPLOYEEMENT = 4;

            public const bool GENDER_MALE = true;
            public const bool GENDER_FEMALE = false;

            public uint EmployeeID;
            public string EmployeeName;
            public bool Gender;
            public DateTime BirthDate;
            public string NationalID;
            public string MaritalStatusName;
            public string Mobile;
            public string Phone;
            public string EmailAddress;
            public string Address;
            public string Report;

            public string JobState;
            public string EmployeeMentState;
            public uint EmployeeStateCode;
            public EmployeesReport( uint EmployeeID_, string EmployeeName_,bool Gender_, DateTime BirthDate_,string NationalID_,
             string MaritalStatusName_, string Mobile_,string Phone_,string EmailAddress_,string Address_, string Report_, string JobState_, string EmployeeMentState_, uint EmployeeStateCode_)
            {
                EmployeeID = EmployeeID_;
                EmployeeName = EmployeeName_;
                Gender = Gender_;
                BirthDate = BirthDate_;
                NationalID = NationalID_;
                MaritalStatusName = MaritalStatusName_;
                Mobile = Mobile_;
                Phone = Phone_;
                EmailAddress = EmailAddress_;
                Address = Address_;
                Report = Report_;
                JobState = JobState_;
                EmployeeMentState = EmployeeMentState_;
                EmployeeStateCode = EmployeeStateCode_;

            }
            internal static List<EmployeesReport> Get_EmployeesReport_List_From_DataTable(DataTable table)
            {
                try
                {
                    List<EmployeesReport> list = new List<EmployeesReport>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        uint EmployeeID = Convert.ToUInt32(table.Rows[i]["EmployeeID"]);
                        string EmployeeName = table.Rows[i]["EmployeeName"].ToString();

                        bool  Gender = Convert.ToBoolean (table.Rows[i]["Gender"]);
                        DateTime  BirthDate = Convert.ToDateTime (table.Rows[i]["BirthDate"].ToString());
                        string  NationalID = table.Rows[i]["NationalID"].ToString ();
                        string MaritalStatusName = table.Rows[i]["MaritalStatusName"].ToString();
                        string Mobile = table.Rows[i]["Mobile"].ToString();
                        string Phone = table.Rows[i]["Phone"].ToString();
                        string EmailAddress = table.Rows[i]["EmailAddress"].ToString();
                        string Address = table.Rows[i]["Address"].ToString();
                        string Report = table.Rows[i]["Report"].ToString();

                        string JobState = table.Rows[i]["JobState"].ToString();
                        string EmployeeMentState = table.Rows[i]["EmployeeMentState"].ToString();
                        uint EmployeeStateCode = Convert.ToUInt32(table.Rows[i]["EmployeeStateCode"]);

                      
                        list.Add(new EmployeesReport( EmployeeID,EmployeeName, Gender,BirthDate,NationalID, MaritalStatusName, Mobile,
            Phone, EmailAddress, Address,Report,JobState, EmployeeMentState, EmployeeStateCode));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_EmployeeMent_Employee_Report_List_From_DataTable:" + "," + ee.Message);
                }
            }
            public static void SetComboBoxFilter(ref System.Windows.Forms.ComboBox combobox)
            {
                combobox.Items.Add(new ComboboxItem("الكل",0));
                combobox.Items.Add(new ComboboxItem("مباشر و مسند له وظيفة", EmployeesReport.EMPLOYEE_ON_WORK_ON_EMPLOYEEMENT));
                combobox.Items.Add(new ComboboxItem("مباشر و غير مسند له وظيفة", EmployeesReport.EMPLOYEE_ON_WORK_NO_EMPLOYEEMENT));
                combobox.Items.Add(new ComboboxItem("لم يباشر العمل بعد", EmployeesReport.EMPLOYEE_NOT_START_WORK));
                combobox.Items.Add(new ComboboxItem("غادر العمل", EmployeesReport.EMPLOYEE_LEFT_WORK));
                combobox.SelectedIndex = 0;

            }
            public static   void RefreshEmployeesReportList(ref ListView listViewEmployees, List<EmployeesReport> EmployeesReportList_)
            {

                listViewEmployees.Items.Clear();
                for (int i = 0; i < EmployeesReportList_.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(EmployeesReportList_[i] .EmployeeID.ToString());

                    ListViewItem_.Name = EmployeesReportList_[i].EmployeeID.ToString();
                    ListViewItem_.SubItems.Add(EmployeesReportList_[i].EmployeeName);
                    if (EmployeesReportList_[i].Gender == Employee.GENDER_MALE)
                        ListViewItem_.SubItems.Add("ذكر");
                    else
                        ListViewItem_.SubItems.Add("انثى");

                    ListViewItem_.SubItems.Add(EmployeesReportList_[i].BirthDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(EmployeesReportList_[i].NationalID);
                    ListViewItem_.SubItems.Add(EmployeesReportList_[i].MaritalStatusName);

                    ListViewItem_.SubItems.Add(EmployeesReportList_[i].JobState);
                    ListViewItem_.SubItems.Add(EmployeesReportList_[i].EmployeeMentState);
           



                    listViewEmployees.Items.Add(ListViewItem_);

                }
           

            }
            public static void InitializeListView(ref ListView listViewEmployees)
            {
                listViewEmployees.Columns.Clear();
                listViewEmployees.Columns.Add("الرقم",81);
                listViewEmployees.Columns.Add("الاسم", 99);
                listViewEmployees.Columns.Add("الجنس", 71);
                listViewEmployees.Columns.Add("تاريخ الميلاد", 110);

                listViewEmployees.Columns.Add("الرقم الوطني", 99);
                listViewEmployees.Columns.Add("الحالة الاجتماعية", 71);
                listViewEmployees.Columns.Add("حالة العمل", 166);
                listViewEmployees.Columns.Add("حالة الوظائف", 148);
    
            }
            public static void AdjustlistViewEmployeesColumnsWidth(ref ListView listViewEmployees)
            {
                try
                {
                    listViewEmployees.Columns[0].Width = 100;//id
                    listViewEmployees.Columns[1].Width = (listViewEmployees.Width - 555) / 3;//name
                    listViewEmployees.Columns[2].Width = 100;//gender
                    listViewEmployees.Columns[3].Width = 100;//age
                    listViewEmployees.Columns[4].Width = 125;//nationalid
                    listViewEmployees.Columns[5].Width = 125;//martial state
                                                             //listViewEmployees.Columns[6].Width = 80;//mobile
                                                             //listViewEmployees.Columns[7].Width = 80;//phone
                                                             //listViewEmployees.Columns[8].Width = 125;//email adress
                                                             //listViewEmployees.Columns[9].Width = 125;//adress
                    listViewEmployees.Columns[6].Width = (listViewEmployees.Width - 585) / 3;//jobstate
                    listViewEmployees.Columns[7].Width = (listViewEmployees.Width - 585) / 3;//employeement
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("AdjustlistViewEmployeesColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

        public class EmployeeQualification
        {
            public Employee _Employee;
            public string QualificationDesc;
            public DateTime StartDate;
            public DateTime EndDate;
            public string Notes;
            public  EmployeeQualification(Employee Employee_, string QualificationDesc_,
                 DateTime StartDate_, DateTime EndDate_, string Notes_)
            {
                _Employee = Employee_;
                QualificationDesc = QualificationDesc_;
                StartDate = StartDate_;
                EndDate = EndDate_;
                Notes = Notes_;
            }
        }
        public class EmployeeCertificate
        {
            public Employee _Employee;
            public string CertificatesDesc;
            public string University;
            public DateTime StartDate;
            public DateTime EndDate;
            public string Notes;
            public EmployeeCertificate(Employee Employee_, string CertificatesDesc_
               , string University_, DateTime StartDate_, DateTime EndDate_, string Notes_)
            {
                _Employee = Employee_;
                CertificatesDesc = CertificatesDesc_;
                University = University_;
                StartDate = StartDate_;
                EndDate = EndDate_;
                Notes = Notes_;
            }
        }
        //public class JobStart
        //{
        //    public uint JobStartID;
        //    public DateTime OprDate;
        //    public Employee _Employee;
        //    public  string Notes;

        //    public JobStart (uint JobStartID_, DateTime OprDate_,Employee Employee_,string Notes_
        //      )
        //    {
        //        JobStartID = JobStartID_;
        //        OprDate = OprDate_;
        //        _Employee = Employee_;
        //        Notes = Notes_;
        //    }

        //}
        //public class EndJobStart
        //{
        //    public uint EndJobStartID;
        //    public DateTime OprDate;
        //    public JobStart JobStart;
        //    public string Notes;
        //    public EndJobStart(uint EndJobStartID_, DateTime OprDate_, JobStart  JobStart_, string Notes_)
        //    {
        //        EndJobStartID = EndJobStartID_;
        //        OprDate =OprDate_;
        //        JobStart = JobStart_;
        //         Notes = Notes_;

        //    }

        //}
        //public class JobStartReport
        //{
        //    public JobStart _JobStart;
        //    public EndJobStart _EndJobStart;
        //    public JobStartReport(JobStart JobStart_, EndJobStart EndJobStart_)
        //    {
        //        _JobStart = JobStart_;
        //        _EndJobStart = EndJobStart_;
        //    }
        //}
        //public class EmployeeMentAssignReport
        //{
        //    public EmployeeMentAssign _EmployeeMentAssign;
        //    public EndEmployeeMentAssign _EndEmployeeMentAssign;

        //    public EmployeeMentAssignReport(EmployeeMentAssign EmployeeMentAssign_,
        //     EndEmployeeMentAssign EndEmployeeMentAssign_)
        //    {
        //        _EmployeeMentAssign = EmployeeMentAssign_;
        //        _EndEmployeeMentAssign = EndEmployeeMentAssign_;
        //    }
        //}
        //public class EmployeeMentAssign
        //{
        //    public uint AssignID;
        //    public DateTime OprDate;
        //    public JobStart _JobStart;
        //    public EmployeeMent _EmployeeMent;
        //    public string Notes;
        //    public EmployeeMentAssign(uint AssignID_, DateTime OprDate_,
        //     JobStart JobStart_, EmployeeMent EmployeeMent_, string Notes_)
        //    {
        //        AssignID = AssignID_;
        //        OprDate = OprDate_;
        //        _JobStart = JobStart_;
        //        _EmployeeMent = EmployeeMent_;
        //        Notes = Notes_;
        //    }
        //}
        //public class EndEmployeeMentAssign
        //{
        //    public uint EndAssignID;
        //    public DateTime OprDate;
        //    public EmployeeMentAssign _EmployeeMentAssign;
        //    public string Notes;
        //    public EndEmployeeMentAssign(uint EndAssignID_, DateTime OprDate_, EmployeeMentAssign EmployeeMentAssign_, string Notes_)
        //    {
        //        EndAssignID = EndAssignID_;
        //        OprDate = OprDate_;
        //        _EmployeeMentAssign = EmployeeMentAssign_;
        //        Notes = Notes_;

        //    }

        //}

        public class SalaryClause
        {
            public const bool TYPE_DUE = false;//استحقاق
            public const bool TYPE_Deduction = true ;//استقطاع
            /// <summary>
            /// ////
            /// </summary>
            public Employee _Employee;
            public uint SalaryClauseID;
            public DateTime CreateDate;
            public string SalaryClauseDesc;
            public bool ClauseType;
            public DateTime ExecuteDate;
            public uint? MonthsCount;
            public double Value;
            public string Notes;
            public SalaryClause(Employee Employee_, uint SalaryClauseID_,DateTime CreateDate_, string SalaryClauseDesc_,
                bool ClauseType_, DateTime ExecuteDate_,  uint? MonthsCount_, double Value_,string Notes_)
            {
                _Employee = Employee_;
                SalaryClauseID = SalaryClauseID_;
                CreateDate = CreateDate_;
                SalaryClauseDesc = SalaryClauseDesc_;
                ClauseType = ClauseType_;
                ExecuteDate = ExecuteDate_;
                MonthsCount = MonthsCount_;
                Value = Value_;
                Notes = Notes_;
            }
        }
        public class SalarysPayOrder
        {
            public uint SalarysPayOrderID;
            public DateTime OrderDate;
            public int ExecuteYear;
            public int ExecuteMonth;
            public string Notes;

            public SalarysPayOrder(uint SalarysPayOrderID_, DateTime OrderDate_, int ExecuteYear_, int ExecuteMonth_,  
               string Notes_)
            {
                SalarysPayOrderID = SalarysPayOrderID_;
                OrderDate = OrderDate_;
                ExecuteYear = ExecuteYear_;
                ExecuteMonth = ExecuteMonth_;
                Notes = Notes_;
            }
        }
        public class SalarysPayOrderReport_Currency
        {
            public uint CurrencyID;
            public string CurrencyName;
            public string CurrencySymbol;
            public double SalarysValue;
            public string   PaysValue;
            public double RealSalarysValue;
            public double RealPaysValue;
            public SalarysPayOrderReport_Currency( 
                 uint CurrencyID_,
             string CurrencyName_,
             string CurrencySymbol_,
             double SalarysValue_,
             string  PaysValue_,
             double RealSalarysValue_,
             double RealPaysValue_)
            {
              CurrencyID= CurrencyID_;
                CurrencyName = CurrencyName_;
                CurrencySymbol = CurrencySymbol_;
             SalarysValue = SalarysValue_;
             PaysValue= PaysValue_;
             RealSalarysValue= RealSalarysValue_;
             RealPaysValue= RealPaysValue_;
             }
        }
        public class SalarysPayOrderEmployeeReport
        {
            public uint EmployeeID;
            public string  EmployeeName;
            public string JobState;
            public string EmployeeMentState;
            public uint EmployeeStateCode;
            public string ExcpectedSalary;
            public uint? PayOrderID;
            public double?  PayedSalaryValue;
            public Currency  PayedSalaryCurrecny;
            public double?  PayedSalaryExchangeRate;
            public string Paid;
            public double? Remain;
            public double? PaysRealValue;
            public SalarysPayOrderEmployeeReport( uint EmployeeID_,
             string EmployeeName_,
             string JobState_,
             string EmployeeMentState_,
             uint EmployeeStateCode_,
             string ExcpectedSalary_,
             uint? PayOrderID_,
             double? PayedSalaryValue_,
             Currency PayedSalaryCurrecny_,
             double? PayedSalaryExchangeRate_,
                string Paid_,
             double? Remain_,
             double? PaysRealValue_
               )
            {
                 EmployeeID= EmployeeID_;
            EmployeeName= EmployeeName_;
            JobState= JobState_;
             EmployeeMentState= EmployeeMentState_;
             EmployeeStateCode = EmployeeStateCode_;
             ExcpectedSalary = ExcpectedSalary_;
                PayOrderID = PayOrderID_;
            PayedSalaryValue = PayedSalaryValue_;
            PayedSalaryCurrecny= PayedSalaryCurrecny_;
            PayedSalaryExchangeRate= PayedSalaryExchangeRate_;
                Paid = Paid_;
                Remain = Remain_;
                PaysRealValue = PaysRealValue_;
        }
        }
        public class SalarysPayOrderMonthReport
        {
            public int Year;
            public int  MonthNO;
            public string  MonthName;
            public string SalarysPayOrderID;
            public string SalarysPayOrderDate;
            public string EmployeesCount;
            public string MoneyAmount;
            public SalarysPayOrderMonthReport(int Year_,  int MonthNO_,
             string  MonthName_,
             string  SalarysPayOrderID_,
            string SalarysPayOrderDate_,
             string EmployeesCount_,
             string MoneyAmount_)
            {
                Year = Year_;
                MonthNO = MonthNO_;
                MonthName = MonthName_;
                SalarysPayOrderID = SalarysPayOrderID_;
                SalarysPayOrderDate = SalarysPayOrderDate_;
                EmployeesCount = EmployeesCount_;
                MoneyAmount = MoneyAmount_;
            }

        }
        //public class EmployeeSalaryPayOrder
        //{
        //    public uint EmployeeSalaryPayOrderID;
        //    public SalarysPayOrder _SalarysPayOrder;
        //    public Employee _Employee;
        //    public Currency _Currency;
        //    public double ExchangeRate;
        //    public double Value;
        //    public EmployeeSalaryPayOrder(uint EmployeeSalaryPayOrderID_,
        //        SalarysPayOrder SalarysPayOrder_,
        //             Employee Employee_,
        //             Currency Currency_,
        //             double ExchangeRate_,
        //             double Value_)
        //    {
        //        EmployeeSalaryPayOrderID = EmployeeSalaryPayOrderID_;
        //        _Employee = Employee_;
        //        _SalarysPayOrder = SalarysPayOrder_;
        //        _Currency = Currency_;
        //        ExchangeRate = ExchangeRate_;
        //        Value = Value_;
        //    }
        //}
        public class EmployeePayOrder
        {
            public uint PayOrderID;
            public DateTime PayOrderDate;
            public SalarysPayOrder _SalarysPayOrder;
            public string PayOrderDesc;
            public Employee _Employee;
            public Currency _Currency;
            public double ExchangeRate;
            public double Value;
            public EmployeePayOrder(       uint PayOrderID_,
             DateTime PayOrderDate_,
             SalarysPayOrder SalarysPayOrder_,
             string PayOrderDesc_,
             Employee Employee_,
             Currency Currency_,
             double ExchangeRate_,
             double Value_)
            {
               PayOrderID= PayOrderID_;
              PayOrderDate= PayOrderDate_;
                _SalarysPayOrder = SalarysPayOrder_;
              PayOrderDesc = PayOrderDesc_;
              _Employee = Employee_;
             _Currency= Currency_;
             ExchangeRate= ExchangeRate_;
             Value= Value_;
            }

        }
        public class PayOrderReport
        {
            public const bool TYPE_SALARY_PAY_ODER = false;
            public const bool TYPE_PAY_ODER = true ;

            public bool PayOrderType;
            public uint PayOrderID;
            public DateTime PayOrderDate;
            public string PayOrderDesc;
            public uint EmployeeID;
            public string EmployeeName;
            public Currency _Currency;
            public double ExchangeRate;
            public double Value;
            public string PaysAmount;
            public PayOrderReport(bool PayOrderType_,
                uint PayOrderID_,
             DateTime PayOrderDate_,
             string PayOrderDesc_,
             uint EmployeeID_,
             string EmployeeName_,
             Currency Currency_,
            double ExchangeRate_,
             double Value_,
             string PaysAmount_)
            {
                PayOrderType = PayOrderType_;
                PayOrderID = PayOrderID_;
                PayOrderDate = PayOrderDate_;
                PayOrderDesc = PayOrderDesc_;
                EmployeeID = EmployeeID_;
                EmployeeName = EmployeeName_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Value = Value_;
                PaysAmount = PaysAmount_;
            }

        }
        public class AllPayOrdersReport
        {
            public string Payorders_Value;
            public string Payorders_PaysAmount;
            public string Payorders_PaysRemain;
            public double Payorders_RealValue;
            public double Payorders_Pays_RealValue;
            public AllPayOrdersReport(   string Payorders_Value_,
             string Payorders_PaysAmount_,
             string Payorders_PaysRemain_,
             double Payorders_RealValue_,
             double Payorders_Pays_RealValue_)
            {
                    Payorders_Value= Payorders_Value_;
                 Payorders_PaysAmount= Payorders_PaysAmount_;
                 Payorders_PaysRemain= Payorders_PaysRemain_;
                 Payorders_RealValue= Payorders_RealValue_;
                 Payorders_Pays_RealValue= Payorders_RealValue_;
             }

        }
        //public class EmployeeFixedDue
        //{
        //    public uint FixedDueID;
        //    public string DueDesc;
        //    public double  Value;
        //    public EmployeeFixedDue(uint FixedDueID_, string DueDesc_, double  Value_)
        //    {
        //        FixedDueID = FixedDueID_;
        //        DueDesc = DueDesc_;
        //        Value = Value_;
        //    }
        //}
        //public class EmployeeDue
        //{
        //    public uint DueID;
        //    public uint EmployeeID;
        //    public string DueDesc;
        //    public DateTime DueDate;
        //    public double Value;
        //    public EmployeeDue(uint EmployeeID_, string DueDesc_, double Value_)
        //    {
        //        EmployeeID = EmployeeID_;
        //        DueDesc = DueDesc_;
        //        Value = Value_;
        //    }
        //}
        //public class EmployeeDeduction
        //{
        //    public uint DeductionID;
        //    public uint EmployeeID;
        //    public string DueDesc;
        //    public DateTime DeductionDate;
        //    public double Value;
        //    public EmployeeDeduction(uint EmployeeID_, string DueDesc_, double Value_)
        //    {
        //        EmployeeID = EmployeeID_;
        //        DueDesc = DueDesc_;
        //        Value = Value_;
        //    }
        //}
        //public class PayOrder
        //{
        //   public  uint PayOrderID;
        //    public DateTime PayOrderDate;
        //    public string PayOrderDesc;
        //    public Employee _Employee;
        //    public Currency _Currency;
        //    public double ExchangeRate;
        //    public string Notes;
        //    public PayOrder(  uint PayOrderID_,DateTime PayOrderDate_,string PayOrderDesc_,
        //     Employee Employee_, Currency Currency_,double ExchangeRate_,string Notes_)
        //    {
        //          PayOrderID= PayOrderID_;
        //        PayOrderDate = PayOrderDate_;
        //        PayOrderDesc = PayOrderDesc_;
        //        _Employee = Employee_;
        //        _Currency = Currency_;
        //        ExchangeRate = ExchangeRate_;
        //        Notes = Notes_;
        //    }
        //}
        //public class PayOrderFixedDeduction
        //{
        //    public uint PayOrderID;
        //    public string DeductionDesc;
        //    public double  Value;
        //    public PayOrderFixedDeduction(uint PayOrderID_, string DeductionDesc_, double  Value_)
        //    {
        //        PayOrderID = PayOrderID_;
        //        DeductionDesc = DeductionDesc_;
        //        Value = Value_;
        //    }
        //}
        //public class PayOrderFixedDue
        //{
        //    public uint PayOrderID;
        //    public string DueDesc;
        //    public double  Value;
        //    public PayOrderFixedDue(uint PayOrderID_, string DueDesc_, double  Value_)
        //    {
        //        PayOrderID = PayOrderID_;
        //        DueDesc = DueDesc_;
        //        Value = Value_;
        //    }
        //}
        //public class PayOrderDeduction
        //{
        //    public uint PayOrderID;
        //    public EmployeeDeduction _EmployeeDeduction;
        //    public PayOrderDeduction(uint PayOrderID_, EmployeeDeduction EmployeeDeduction_)
        //    {
        //        PayOrderID = PayOrderID_;
        //        _EmployeeDeduction = EmployeeDeduction_;
        //    }
        //}
        //public class PayOrderDue
        //{
        //    public uint PayOrderID;
        //    public EmployeeDue _EmployeeDue;
        //    public PayOrderDue(uint PayOrderID_, EmployeeDue EmployeeDue_)
        //    {
        //        PayOrderID = PayOrderID_;
        //        _EmployeeDue = EmployeeDue_;
        //    }
        //}

        //public class JobStartDocument : Document
        //{


        //    public JobStartDocument(uint DocumentID_, DateTime DocumentDate_, Employee Employee_)
        //    {
        //        DocumentID = DocumentID_;
        //        DocumentDate = DocumentDate_;
        //        DocumentType = Document.JOBSTART_DOCUMENT;
        //        _Employee = Employee_;
        //    }
        //}
        //public class EndJobStartDocument : Document
        //{
        //    public JobStartDocument _JobStartDocument;
        //    public EndJobStartDocument(uint DocumentID_, DateTime DocumentDate_, Employee Employee_, JobStartDocument JobStartDocument_)
        //    {
        //        DocumentID = DocumentID_;
        //        DocumentDate = DocumentDate_;
        //        DocumentType = Document.ENDJOBSTART_DOCUMENT;
        //        _JobStartDocument = JobStartDocument_;
        //        _Employee = Employee_;

        //    }
        //}
        //public class AssignDocument : Document
        //{
        //    public JobStartDocument _JobStartDocument;
        //    public EmployeeMent _EmployeeMent;
        //    public AssignDocument(uint DocumentID_, DateTime DocumentDate_, Employee Employee_, JobStartDocument JobStartDocument_, EmployeeMent EmployeeMent_)
        //    {
        //        DocumentID = DocumentID_;
        //        DocumentDate = DocumentDate_;
        //        DocumentType = Document.ASSIGN_DOCUMENT;
        //        _Employee = Employee_;
        //        _JobStartDocument = JobStartDocument_;
        //        _EmployeeMent = EmployeeMent_;
        //    }
        //}
        //public class EndAssignDocument : Document
        //{
        //    public AssignDocument _AssignDocument;
        //    public EndAssignDocument(uint DocumentID_, DateTime DocumentDate_, Employee Employee_, AssignDocument AssignDocument_)
        //    {
        //        DocumentID = DocumentID_;
        //        DocumentDate = DocumentDate_;
        //        DocumentType = Document.ENDASSIGN_DOCUMENT;
        //        _Employee = Employee_;
        //        _AssignDocument = AssignDocument_;
        //    }
        //}
    }
}
