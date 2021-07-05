
using OverLoad_Server_Kernal.AccountingSQL;
using OverLoad_Server_Kernal.Objects;
using OverLoad_Server_Kernal.TradeSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OverLoad_Server_Kernal
{
    namespace CompanySQL
    {
        public class CompanyReportSQL
        {
            DatabaseInterface DB;
        
            public CompanyReportSQL(DatabaseInterface db)
            {
                DB = db;

            }
            //public List<EmployeesOPRReport > Get_EmployeesOPRReport_List()
            //{
            //    List<EmployeesOPRReport> list = new List<EmployeesOPRReport>();
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "
            //            + EmployeesOPRReportTable.OPR_Type + ","
            //         + EmployeesOPRReportTable.OPR_Desc + ","
            //          + EmployeesOPRReportTable.OPR_ID + ","
            //           + EmployeesOPRReportTable.OPR_Date + ","
            //            + EmployeesOPRReportTable.OPR_Employee + ","
            //        + EmployeesOPRReportTable.OPR_EmployeeMent
            //        + " from   "
            //        + EmployeesOPRReportTable.TableName+"()"
            //        +" order by "
            //        + EmployeesOPRReportTable.OPR_Date

            //      );

            //        for (int i = 0; i < t.Rows.Count; i++)
            //        {
            //            uint oprtype = Convert.ToUInt32(t.Rows [i][0].ToString ());
            //            string oprdesc = t.Rows[i][1].ToString();
            //            uint oprid = Convert.ToUInt32(t.Rows[i][2].ToString());
            //            DateTime oprdate = Convert.ToDateTime(t.Rows[i][3].ToString());

            //            string employee_name = t.Rows[i][4].ToString();
            //            string employeement_name = t.Rows[i][5].ToString();
            //            list.Add(new EmployeesOPRReport(oprtype , oprdesc
            //                , oprid, oprdate , employee_name ,employeement_name));
            //        }
            //        return list;
            //    }
            //    catch (Exception ee)
            //    {
            //        throw new Exception("Get_EmployeesOPRReport_List" + ee.Message);
            //        return null;
            //    }
            //}
            public List<EmployeeMent_Employee_Report> Get_EmployeeMent_Employee_Report_List()
            {

                //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                return new DataBaseFunctions(DB).Company_Get_EmployeeMent_Employee_Report_List();
            }
            public List<EmployeesReport> GetEmployeesReportList()
            {
                //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                return new DataBaseFunctions(DB).Company_GetEmployeesReportList();
            }
        }
  
        public class PartSQL
        {

            DatabaseInterface DB;
            public static class CompanyPartTable
            {
                public const string TableName = "Company_Part";
                public const string PartID = "PartID";
                public const string PartName = "PartName";
                public const string ParentPartID = "ParentPartID";
                public const string CreateDate = "CreateDate";

            }
            public PartSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public string GetPartPath(Part  Part_)
            {
                List<string> f_path = new List<string>();
                Part f = Part_ ;
                string s =/*DB .COMPANY +*/ " / ";
                while (f.ParentPartID  != null)
                {
                    f = GetPartInfoByID(Convert.ToUInt32(f.ParentPartID ));
                    f_path.Add(f.PartName );
                }
                for (int i = f_path.Count - 1; i >= 0; i--)
                    s += f_path[i] + " /";
                return s;
            }
       
            public Part GetParentPart(Part f)
            {
                try
                {
                    if (f.ParentPartID == null) return null;
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + CompanyPartTable.PartName  + ","
                        + CompanyPartTable.ParentPartID + ","
                        + CompanyPartTable.CreateDate  
                        + " from   "
                        + CompanyPartTable.TableName
                        + " where "
                        + CompanyPartTable.PartID  + "=" + f.ParentPartID
                      );
                    if (t.Rows.Count == 1)
                    {

                        uint fid = Convert.ToUInt32(f.ParentPartID);
                        string fname = t.Rows[0][0].ToString();
                        uint? p;
                        try
                        {
                            p = Convert.ToUInt32(t.Rows[0][1]);
                        }
                        catch
                        {
                            p = null;
                        }
                        DateTime d = Convert.ToDateTime(t.Rows[0][2]);
                        return new Part(fid, fname, d, p );
                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetParentPart" + ee.Message);
                    return null ;
                }
               

            }
            public List<Part> GetPartChilds(Part Part)
            {
                List<Part> list = new List<Part>();
                try
                {
                    string parentid_str;
                    if (Part == null) parentid_str = " is null";
                    else parentid_str = "=" + Part.PartID.ToString();

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + CompanyPartTable.PartID + ","
                        + CompanyPartTable.PartName + ","
                        + CompanyPartTable.CreateDate
                        + " from   "
                        + CompanyPartTable.TableName
                        + " where "
                        + CompanyPartTable.ParentPartID + parentid_str
                        );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint fid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string fname = t.Rows[i][1].ToString();
                        DateTime d = Convert.ToDateTime(t.Rows[i][2]);  
                        uint? p;
                        if (Part == null) p = null;
                        else p = Part.PartID;

                        list.Add(new Part(fid, fname, d, p));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPartChilds" + ee.Message);
                    return list;
                }
                
            }
            public List<Part> SearchPart(string n_)
            {
                List<Part> list = new List<Part>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + CompanyPartTable.PartID + ","
                        + CompanyPartTable.PartName + ","
                        + CompanyPartTable.ParentPartID + ","
                        + CompanyPartTable.CreateDate 
                        + " from " + CompanyPartTable.TableName
                       + " where " + CompanyPartTable.PartName + " like  '%" + n_ + "%'");
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint fid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string fname = t.Rows[i][1].ToString();
                        uint? p;
                        try
                        {
                            p = Convert.ToUInt32(t.Rows[i][2].ToString());
                        }
                        catch
                        {
                            p = null;
                        }

                        DateTime d = Convert.ToDateTime(t.Rows[i][3]);
                        list.Add(new Part(fid, fname, d, p));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("SearchPart" + ee.Message);
                    return list;
                }

               
            }
            public Part GetPartInfoByID(uint id)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                    + CompanyPartTable.PartName + ","
                    + CompanyPartTable.ParentPartID + ","
                    + CompanyPartTable.CreateDate
                    + " from " + CompanyPartTable.TableName
                    + " where " + CompanyPartTable.PartID  +"="+id
                    );

                string fname = t.Rows[0][0].ToString();

                uint? p;
                try
                {
                    p = Convert.ToUInt32(t.Rows[0][1]);
                }
                catch
                {
                    p = null;
                }
                DateTime d = Convert.ToDateTime(t.Rows[0][2]);
                return new Part(id, fname, d, p );
            }
            public List<Part> GetPartsList()
            {
                List<Part> list = new List<Part>();
                DataTable t = DB.GetData("select * from " + CompanyPartTable.TableName);
                for (int i = 0; i < t.Rows.Count; i++)
                {

                    uint fid = Convert.ToUInt32(t.Rows[i][0].ToString());
                    string fname = t.Rows[i][1].ToString();
                    uint? p;
                    try
                    {
                        p = Convert.ToUInt32(t.Rows[i][2]);
                    }
                    catch
                    {
                        p = null;
                    }
                    DateTime d = Convert.ToDateTime(t.Rows[i][3]);
                    list.Add(new Part(fid, fname, d,p ));
                }
                return list;
            }
        }
        public class EmployeeMentLevelSQL
        {
            DatabaseInterface DB;
            public static class EmployeeMentLevelTable
            {
                public const string TableName = "Company_EmployeeMent_Level";
                public const string LevelID = "LevelID";
                public const string LevelName = "LevelName";
            }
            public EmployeeMentLevelSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public EmployeeMentLevel Get_EmployeeMentLevel_Info_BY_ID(uint  levelid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeMentLevelTable.LevelName 
                        + " from   "
                        + EmployeeMentLevelTable.TableName
                        + " where "
                        + EmployeeMentLevelTable.LevelID + "=" + levelid
                      );
                    if (t.Rows.Count == 1)
                    {

                        string  levelname = t.Rows[0][0].ToString();
                        return new EmployeeMentLevel(levelid , levelname);

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_EmployeeMentLevel_Info" + ee.Message);
                    return null;
                }
            }

          
            public List<EmployeeMentLevel> Get_EmployeeMentLevel_List()
            {
                List<EmployeeMentLevel> list = new List<EmployeeMentLevel>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeMentLevelTable.LevelID  + ","
                    + EmployeeMentLevelTable.LevelName
                    + " from   "
                    + EmployeeMentLevelTable.TableName
                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint levelid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string levelname = t.Rows[i][1].ToString();
                        
                        list.Add(new EmployeeMentLevel(levelid, levelname ));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_EmployeeMentLevel_List" + ee.Message);
                    return null;
                }
            }
        }
        public class EmployeeMentSQL
        {

            DatabaseInterface DB;
            private static class EmployeeMentTable
            {
                public const string TableName = "Company_EmployeeMent";
                public const string EmployeeMentID = "EmployeeMentID";
                public const string EmployeeMentName = "EmployeeMentName";
                public const string LevelID = "LevelID";
                public const string CreateDate = "CreateDate";
                public const string PartID = "PartID";
      
            }
            public EmployeeMentSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public EmployeeMent Get_EmployeeMent_InfoBYID(uint ID)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeMentTable.EmployeeMentName + ","
                        + EmployeeMentTable.CreateDate + ","
                        + EmployeeMentTable.LevelID + ","
                        + EmployeeMentTable.PartID
                        + " from   "
                        + EmployeeMentTable.TableName
                        + " where "
                        + EmployeeMentTable.EmployeeMentID + "=" + ID
                      );
                    if (t.Rows.Count == 1)
                    {

                        string name = t.Rows[0][0].ToString();
                        DateTime createdate = Convert.ToDateTime(t.Rows[0][1].ToString());
                        EmployeeMentLevel EmployeeMentLevel_  = new EmployeeMentLevelSQL(DB).Get_EmployeeMentLevel_Info_BY_ID(Convert.ToUInt32(t.Rows[0][2]));
                        Part Part_;
                        try
                        {
                             Part_ = new PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[0][3]));

                        }
                        catch
                        {
                            Part_ = null;
                        }
                        return new EmployeeMent(ID, name, createdate,EmployeeMentLevel_, Part_);

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_EmployeeMent_InfoBYID" + ee.Message);
                    return null;
                }
            }
                
         
            public List<EmployeeMent> Get_EmployeeMent_List()
            {
                List<EmployeeMent> list = new List<EmployeeMent>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeMentTable.EmployeeMentID   + ","
                    + EmployeeMentTable.EmployeeMentName  + ","
                    + EmployeeMentTable.CreateDate + ","
                    + EmployeeMentTable.LevelID  + ","
                    + EmployeeMentTable.PartID  
                    + " from   "
                    + EmployeeMentTable.TableName
                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint ID = Convert.ToUInt32(t.Rows[i][0]);
                        string name = t.Rows[i][1].ToString();
                        DateTime createdate = Convert.ToDateTime(t.Rows[i][2].ToString());
                        EmployeeMentLevel EmployeeMentLevel_= new EmployeeMentLevelSQL(DB).Get_EmployeeMentLevel_Info_BY_ID (Convert.ToUInt32(t.Rows[0][3]));
                        Part Part_;
                        try
                        {
                            Part_ = new PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[0][4]));

                        }
                        catch
                        {
                            Part_ = DB.COMPANY ;
                        }
                        list.Add(new EmployeeMent(ID, name, createdate,EmployeeMentLevel_, Part_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetEmployeeList" + ee.Message);
                    return null;
                }
            }
            public List<EmployeeMent> Get_EmployeeMent_List_IN_Part(Part Part_)
            {
                
                List<EmployeeMent> list = new List<EmployeeMent>();
                    try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeMentTable.EmployeeMentID + ","
                    + EmployeeMentTable.EmployeeMentName + ","
                    + EmployeeMentTable.CreateDate + ","
                    + EmployeeMentTable.LevelID 
                    + " from   "
                    + EmployeeMentTable.TableName
                    + " where   "
                    + EmployeeMentTable.PartID +(Part_ ==null ?" is null":"="+ Part_.PartID.ToString () )

                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint ID = Convert.ToUInt32(t.Rows[i][0]);
                        string name = t.Rows[i][1].ToString();
                        DateTime createdate = Convert.ToDateTime(t.Rows[i][2].ToString());
                        EmployeeMentLevel EmployeeMentLevel_ = new EmployeeMentLevelSQL(DB).Get_EmployeeMentLevel_Info_BY_ID(Convert.ToUInt32(t.Rows[0][3]));

                        list.Add(new EmployeeMent(ID, name, createdate,EmployeeMentLevel_, Part_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_EmployeeMent_List_IN_Part" + ee.Message);
                    return null;
                }
            }

            internal int GetEmployeeMentsCountInPart(Part Part_)
            {
                try
                {
                    DataTable t= DB.GetData ("select count (*) from   "
                    + EmployeeMentTable.TableName
                    + " where "
                    + EmployeeMentTable.PartID + (Part_ == null ? " is null" : "=" + Part_.PartID.ToString())
                    );
                    return Convert.ToInt32(t.Rows [0][0].ToString ()); ;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetEmployeeMentsCountInPart" + ee.Message);
                    return -1;
                }
            }

            internal  string  GetEmployeeMentPath(EmployeeMent employeeMent)
            {
                PartSQL PartSQL_ = new PartSQL(DB);
                List<string> f_path = new List<string>();
                Part f = employeeMent._Part;
                string s =DB.COMPANY.PartName+ " \\";

                while (f.ParentPartID != null)
                {
                    f_path.Add(f.PartName);
                    f = PartSQL_.GetPartInfoByID(Convert.ToUInt32(f.ParentPartID));

                }
                f_path.Add(f.PartName);
                for (int i = f_path.Count - 1; i >= 0; i--)
                    s += f_path[i] + "/";
                return s;
            }

            internal List<EmployeeMent> SearchEmployeeMent(string text)
            {
                List<EmployeeMent> list = new List<EmployeeMent>();
                if (text .Length == 0) return list;
                DataTable t = new DataTable();
                t = DB.GetData("select "
                    + EmployeeMentTable.EmployeeMentID  + ","
                    + EmployeeMentTable.EmployeeMentName + ","
                    + EmployeeMentTable.CreateDate  + ","
                    + EmployeeMentTable.LevelID  + ","
                    + EmployeeMentTable.PartID  
                    + " from " + EmployeeMentTable.TableName
                   + " where " + EmployeeMentTable.EmployeeMentName  + " like '%" + text + "%'");
                for (int i = 0; i < t.Rows.Count; i++)
                {

                    uint employeement_id = Convert.ToUInt32(t.Rows[i][0].ToString());
                    string employeement_name = t.Rows[i][1].ToString();
                    DateTime createdate = Convert.ToDateTime(t.Rows[i][2]);
                    EmployeeMentLevel EmployeeMentLevel_ = new EmployeeMentLevelSQL(DB).Get_EmployeeMentLevel_Info_BY_ID(Convert.ToUInt32(t.Rows[0][3]));

                    Part Part_;
                    try
                    {
                        Part_ = new PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[0][4]));

                    }
                    catch
                    {
                        Part_ = null;
                    }
                    list.Add(new EmployeeMent(employeement_id, employeement_name, createdate,EmployeeMentLevel_, Part_));
                }
                return list;
            }

          
        }
        public class EmployeeSQL
        {
            DatabaseInterface DB;
            private static class EmployeeTable
            {
                public const string TableName = "Company_Employee";
                public const string EmployeeID = "EmployeeID";
                public const string EmployeeName = "EmployeeName";
                public const string Gender = "Gender";
                public const string BirthDate = "BirthDate";
                public const string NationalID = "NationalID";
                public const string MaritalStatus = "MaritalStatus";
                public const string Mobile = "Mobile";
                public const string Phone = "Phone";
                public const string Address = "Address_";
                public const string EmailAddress = "EmailAddress";
                public const string Report = "Report";
                public const string CurrencyID = "CurrencyID";
            }
            public static class EmployeeImageTable
            {
                public const string TableName = "Company_Employee_Image";
                public const string EmployeeID = "EmployeeID";
                public const string Employee_Image = "Employee_Image";
            }
            public EmployeeSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public Employee  GetEmployeeInforBYID(uint EmployeeID)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeTable.EmployeeName + ","
                        + EmployeeTable.Gender + ","
                        + EmployeeTable.BirthDate + ","
                        + EmployeeTable.NationalID  + ","
                        + EmployeeTable.MaritalStatus  + ","
                        + EmployeeTable.Mobile + ","
                        + EmployeeTable.Phone + ","
                        + EmployeeTable.EmailAddress  + ","
                        + EmployeeTable.Address + ","
                        + EmployeeTable.Report + ","
                         + EmployeeTable.CurrencyID 
                        + " from   "
                        + EmployeeTable.TableName
                        + " where "
                        + EmployeeTable.EmployeeID + "=" + EmployeeID
                      );
                    if (t.Rows.Count == 1)
                    {

                        string name = t.Rows[0][0].ToString();
                        bool gender = Convert.ToInt32(t.Rows[0][1].ToString()) == 1 ? true : false;
                        DateTime birthdate = Convert.ToDateTime(t.Rows[0][2].ToString());
                        string nationalid = t.Rows[0][3].ToString();
                        MaritalStatus MaritalStatus_ = MaritalStatus .Get_MaritalStatus_BY_ID (Convert.ToUInt32 ( t.Rows[0][4].ToString()));
                        string mobile = t.Rows[0][5].ToString();
                        string phone = t.Rows[0][6].ToString();
                        string emailaddress = t.Rows[0][7].ToString();
                        string address = t.Rows[0][8].ToString();
                        string notes = t.Rows[0][9].ToString();
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert .ToUInt32(t.Rows[0][10].ToString()));
                        return new Employee(EmployeeID, name, gender, birthdate,nationalid,MaritalStatus_
                            , mobile, phone,emailaddress, address, notes,currency );

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetEmployeeInforBYID" + ee.Message);
                    return null;
                }

            }
            public List<Employee_User > GetEmployeeUserAccountList()
            {
                List<Employee_User> list = new List<Employee_User>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeTable.EmployeeID + ","
                        + EmployeeTable.EmployeeName + ","
                        + EmployeeTable.Gender + ","
                        + EmployeeTable.BirthDate + ","
                        + EmployeeTable.NationalID + ","
                        + EmployeeTable.MaritalStatus + ","
                        + EmployeeTable.Mobile + ","
                        + EmployeeTable.Phone + ","
                        + EmployeeTable.EmailAddress + ","
                        + EmployeeTable.Address + ","
                        + EmployeeTable.Report + ","
                         + EmployeeTable.CurrencyID
                        + " from   "
                        + EmployeeTable.TableName

                      );
                    for(int i=0;i<t.Rows .Count;i++)
                    {
                        uint EmployeeID = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string name = t.Rows[i][1].ToString();
                        bool gender = Convert.ToInt32 (t.Rows[i][2].ToString())==1?true :false ;
                        DateTime birthdate = Convert.ToDateTime(t.Rows[i][3].ToString());
                        string nationalid = t.Rows[i][4].ToString();
                        MaritalStatus MaritalStatus_ = MaritalStatus.Get_MaritalStatus_BY_ID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        string mobile = t.Rows[i][6].ToString();
                        string phone = t.Rows[i][7].ToString();
                        string emailaddress = t.Rows[i][8].ToString();
                        string address = t.Rows[i][9].ToString();
                        string notes = t.Rows[i][10].ToString();
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][11].ToString()));
                        Employee Employee_= new Employee(EmployeeID, name, gender, birthdate, nationalid, MaritalStatus_
                            , mobile, phone, emailaddress, address, notes, currency);
                        DatabaseInterface.User User_ = DB.GetEmployeeUser(Employee_);
                        list .Add ( new Employee_User (Employee_, User_));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetEmployeeUserAccountList" + ee.Message);
                    return list;
                }
            }
            internal List<Employee> Get_All_Employees()
            {
                List<Employee> employeelist = new List<Employee>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeTable.EmployeeName + ","
                        + EmployeeTable.Gender + ","
                        + EmployeeTable.BirthDate + ","
                        + EmployeeTable.NationalID + ","
                        + EmployeeTable.MaritalStatus + ","
                        + EmployeeTable.Mobile + ","
                        + EmployeeTable.Phone + ","
                        + EmployeeTable.EmailAddress + ","
                        + EmployeeTable.Address + ","
                        + EmployeeTable.Report + ","
                         + EmployeeTable.CurrencyID + ","
                         + EmployeeTable.EmployeeID
                        + " from   "
                        + EmployeeTable.TableName
                      );
                    for(int i=0;i<t.Rows .Count;i++)
                    {

                        string name = t.Rows[i][0].ToString();
 
                        bool gender = Convert.ToInt32(t.Rows[i][1].ToString())==1?true :false ;
     
                        DateTime birthdate = Convert.ToDateTime(t.Rows[i][2].ToString());
                        string nationalid = t.Rows[i][3].ToString();
                        MaritalStatus MaritalStatus_ = MaritalStatus.Get_MaritalStatus_BY_ID(Convert.ToUInt32(t.Rows[i][4].ToString()));
                        string mobile = t.Rows[i][5].ToString();
                        string phone = t.Rows[i][6].ToString();
                        string emailaddress = t.Rows[i][7].ToString();
                        string address = t.Rows[i][8].ToString();
                        string notes = t.Rows[i][9].ToString();
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][10].ToString()));
                        uint EmployeeID = Convert.ToUInt32(t.Rows[i][11].ToString());
                        employeelist .Add ( new Employee(EmployeeID, name, gender, birthdate, nationalid, MaritalStatus_
                            , mobile, phone, emailaddress, address, notes, currency));

                    }
                    
                        return employeelist;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_All_Employees:" + ee.Message);
                    return employeelist;
                }
            }
        }
        public class EmployeeQualificationSQL
        {
            DatabaseInterface DB;
            private static class EmployeeQualificationTable
            {
                public const string TableName = "Company_Employee_Qualification";
                public const string EmployeeID = "EmployeeID";
                public const string QualificationDesc = "QualificationDesc";
                public const string StartDate = "StartDate";
                public const string EndDate = "EndDate";
                public const string Notes = "Notes";
            }
            public EmployeeQualificationSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public EmployeeQualification Get_Qualification_Info(Employee Employee_, string QualificationDesc_)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeQualificationTable.StartDate + ","
                        + EmployeeQualificationTable.EndDate + ","
                        + EmployeeQualificationTable.Notes
                        + " from   "
                        + EmployeeQualificationTable.TableName
                        + " where "
                        + EmployeeQualificationTable.QualificationDesc + "='" + QualificationDesc_ + "'"
                        + " and "
                        + EmployeeQualificationTable.EmployeeID + "=" + Employee_.EmployeeID
                      );
                    if (t.Rows.Count == 1)
                    {
   
                        DateTime startdate = Convert.ToDateTime(t.Rows[0][0].ToString());
                        DateTime enddate = Convert.ToDateTime(t.Rows[0][1].ToString());
                        string notes = t.Rows[0][1].ToString();

                        return new EmployeeQualification(Employee_, QualificationDesc_, startdate, enddate, notes);

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_Qualification_Info" + ee.Message);
                    return null;
                }
            }

           
            public List<EmployeeQualification> Get_Qualification_List(Employee Employee_)
            {
                List<EmployeeQualification> list = new List<EmployeeQualification>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeQualificationTable.QualificationDesc + ","
                     + EmployeeQualificationTable.StartDate + ","
                      + EmployeeQualificationTable.EndDate + ","
                    + EmployeeQualificationTable.Notes
                    + " from   "
                    + EmployeeQualificationTable.TableName
                     + " where   "
                    + EmployeeQualificationTable.EmployeeID + "=" + Employee_.EmployeeID 
                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        string QualificationDesc_ = t.Rows[i][0].ToString();
                        DateTime startdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        DateTime enddate = Convert.ToDateTime(t.Rows[i][2].ToString());
                        string notes = t.Rows[i][3].ToString();
                        list.Add(new EmployeeQualification(Employee_, QualificationDesc_, startdate, enddate, notes));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Qualification_List" + ee.Message);
                    return null;
                }
            }
        }
        public class EmployeeCertificateSQL
        {
            DatabaseInterface DB;
            private static class EmployeeCertificateTable
            {
                public const string TableName = "Company_Employee_Certificate";
                public const string EmployeeID = "EmployeeID";
                public const string CertificateDesc = "CertificateDesc";
                public const string University = "University";
                public const string StartDate = "StartDate";
                public const string EndDate = "EndDate";
                public const string Notes = "Notes";
            }
            public EmployeeCertificateSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public EmployeeCertificate Get_Certificate_Info(Employee Employee_, string CertificateDesc_)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeCertificateTable.University  + ","
                        + EmployeeCertificateTable.StartDate   + ","
                        + EmployeeCertificateTable.EndDate  + ","
                        + EmployeeCertificateTable.Notes
                        + " from   "
                        + EmployeeCertificateTable.TableName
                        + " where "
                        + EmployeeCertificateTable.CertificateDesc + "='" + CertificateDesc_+"'"
                        + " and "
                        + EmployeeCertificateTable.EmployeeID  + "=" + Employee_.EmployeeID 
                      );
                    if (t.Rows.Count == 1)
                    {
                        string university = t.Rows[0][0].ToString();
                        DateTime  startdate = Convert.ToDateTime (t.Rows[0][1].ToString());
                        DateTime enddate = Convert.ToDateTime(t.Rows[0][2].ToString());
                        string notes = t.Rows[0][3].ToString();

                        return new EmployeeCertificate(Employee_, CertificateDesc_, university ,startdate ,enddate  , notes);

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_Certificate_Info" + ee.Message);
                    return null;
                }
            }

         
            public List<EmployeeCertificate> Get_Certificate_List(Employee Employee_)
            {
                List<EmployeeCertificate> list = new List<EmployeeCertificate>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + EmployeeCertificateTable.CertificateDesc + ","
                    + EmployeeCertificateTable.University  + ","
                     + EmployeeCertificateTable.StartDate  + ","
                      + EmployeeCertificateTable.EndDate  + ","
                    + EmployeeCertificateTable.Notes
                    + " from   "
                    + EmployeeCertificateTable.TableName
                     + " where   "
                    + EmployeeCertificateTable.EmployeeID + "=" + Employee_.EmployeeID
                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        string CertificateDesc_ = t.Rows[i][0].ToString();
                        string university = t.Rows[i][1].ToString();
                        DateTime startdate = Convert.ToDateTime(t.Rows[i][2].ToString());
                        DateTime enddate = Convert.ToDateTime(t.Rows[i][3].ToString());
                        string notes = t.Rows[i][4].ToString();
                        list.Add(new EmployeeCertificate(Employee_, CertificateDesc_, university ,startdate ,enddate , notes));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Certificate_List" + ee.Message);
                    return null;
                }
            }
        }
        public class DocumentSQL
        {
            DatabaseInterface DB;
            private static class DocumentTable
            {
                public const string TableName = "Company_Document";
                public const string DocumentID = "DocumentID";
                public const string DocumentDate = "DocumentDate";
                public const string EmployeeID = "EmployeeID";
                public const string DocumentType = "DocumentType";
                public const string ExcuteDate = "ExcuteDate";
                public const string TargetDocumentID = "TargetDocumentID";
                public const string EmployeementID = "EmployeementID";
                public const string Notes = "Notes";

            }
            public DocumentSQL(DatabaseInterface db)
            {
                DB = db;
            }
            public void Check_Document(uint EmployeeID, uint type, DateTime executedate, Document targetdocument, EmployeeMent EmployeeMent)
            {
                Employee Employee = new EmployeeSQL(DB).GetEmployeeInforBYID(EmployeeID);
                List<Document> documentlist = Get_Employee_Document_List(Employee);
                List<Document> JobStart_documentlist = documentlist.Where(x => x.DocumentType == Document.JOBSTART_DOCUMENT).ToList();
                List<Document> Assign_documentlist = documentlist.Where(x => x.DocumentType == Document.ASSIGN_DOCUMENT).ToList();
                List<Document> EndAssign_documentlist = documentlist.Where(x => x.DocumentType == Document.ENDASSIGN_DOCUMENT).ToList();

                List<Document> EndJobStart_documentlist = documentlist.Where(x => x.DocumentType == Document.ENDJOBSTART_DOCUMENT).ToList();

                if(documentlist .Where (x=>x.ExecuteDate >executedate ).ToList ().Count >0)
                    throw new Exception("تاريخ التنفيذ يجب ان يكون اكبر من تاريخ التنفيذ لاخر مستند");
                if (type < 1 || type > 4)
                {
                    throw new Exception ("نوع مستند غير صحيح");
                    
                }
                else if(type ==Document.JOBSTART_DOCUMENT)
                {
                    if(JobStart_documentlist .Count !=EndJobStart_documentlist.Count )
                        throw new Exception("لا يمكن بدء مباشرة قبل انهاء المباشرة الحالية");
                    if(targetdocument !=null )
                        throw new Exception("بيانات مستند غير صحيحة");
                }
                else if (type == Document.ENDJOBSTART_DOCUMENT)
                {
                    if (JobStart_documentlist.Count <= EndJobStart_documentlist.Count)
                        throw new Exception("لا توجد مباشرة غير منهية لانهائها ");
                    if (targetdocument.DocumentType  != Document.JOBSTART_DOCUMENT)
                        throw new Exception("بيانات مستند غير صحيحة");
                }
                else if (type == Document.ASSIGN_DOCUMENT )
                {
                    
                    if (targetdocument.DocumentType != Document.JOBSTART_DOCUMENT)
                        throw new Exception("بيانات مستند غير صحيحة");
                 
                    if (JobStart_documentlist.Count == EndJobStart_documentlist.Count)
                        throw new Exception("يجب انشاء امر مباشرة اولا  ");
                
                   List <Document >tmp_documentlist=  new DocumentSQL(DB).Get_DocumentReport_List();
                    List<Document> Employeement_tmp_documentlist = tmp_documentlist.Where(x =>x._EmployeeMent !=null && x._EmployeeMent.EmployeeMentID == EmployeeMent.EmployeeMentID).ToList();
                    List<Document> Assign_Employeement_tmp_documentlist = Employeement_tmp_documentlist.Where(x => x.DocumentType==Document.ASSIGN_DOCUMENT).ToList();
                    List<Document> EndAssign_Employeement_tmp_documentlist = Employeement_tmp_documentlist.Where(x => x.DocumentType == Document.ENDASSIGN_DOCUMENT ).ToList();
                    if (Assign_Employeement_tmp_documentlist.Count != EndAssign_Employeement_tmp_documentlist.Count )
                        throw new Exception("الوظيفة غير فارغة ");
                }
                else
                {
                    if (targetdocument.DocumentType != Document.ASSIGN_DOCUMENT)
                        throw new Exception("بيانات مستند غير صحيحة");
                    if (JobStart_documentlist.Count == EndJobStart_documentlist.Count)
                        throw new Exception("يجب انشاء امر مباشرة اولا  ");
                    List<Document> tmp_documentlist = new DocumentSQL(DB).Get_DocumentReport_List();
                    List<Document> Employeement_tmp_documentlist = tmp_documentlist.Where(x => x._EmployeeMent != null && x._EmployeeMent.EmployeeMentID ==targetdocument ._EmployeeMent .EmployeeMentID).ToList();
                    List<Document> Assign_Employeement_tmp_documentlist = Employeement_tmp_documentlist.Where(x => x.DocumentType == Document.ASSIGN_DOCUMENT).ToList();
                    List<Document> EndAssign_Employeement_tmp_documentlist = Employeement_tmp_documentlist.Where(x => x.DocumentType == Document.ENDASSIGN_DOCUMENT).ToList();
                    if (Assign_Employeement_tmp_documentlist.Count == EndAssign_Employeement_tmp_documentlist.Count)
                        throw new Exception("لا يوجد امر تكليف لانهائه ");
                }
            }
            public Document  Get_Document_Info_BYID(uint DocumentID_)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + DocumentTable.DocumentDate + ","
                        + DocumentTable.EmployeeID + ","
                        + DocumentTable.DocumentType  + ","
                        + DocumentTable.ExcuteDate  + ","
                        + DocumentTable.TargetDocumentID  + ","
                        + DocumentTable.EmployeementID + ","
                        + DocumentTable.Notes
                        + " from   "
                        + DocumentTable.TableName
                        + " where "
                        + DocumentTable.DocumentID + "=" + DocumentID_
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime documentdate = Convert.ToDateTime(t.Rows[0][0].ToString());
                        Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(Convert.ToUInt32(t.Rows[0][1].ToString()));
                        uint DocumnetType = Convert.ToUInt32(t.Rows[0][2].ToString());
                        DateTime executeDdte = Convert.ToDateTime(t.Rows[0][3].ToString());
                        Document target_document;
                        try
                        {
                            target_document = Get_Document_Info_BYID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                        }
                        catch
                        {
                            target_document = null;
                        }
                        EmployeeMent EmployeeMent_;
                        try
                        {
                            EmployeeMent_ = new EmployeeMentSQL (DB).Get_EmployeeMent_InfoBYID(Convert.ToUInt32(t.Rows[0][5].ToString()));
                        }
                        catch
                        {
                            EmployeeMent_ = null;
                        }
                        string notes = t.Rows[0][6].ToString();
                        return new Document(DocumentID_, documentdate, Employee_,DocumnetType,executeDdte ,target_document ,EmployeeMent_, notes);

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_Document_Info_BYID" + ee.Message);
                    return null;
                }
            }

 
            public List<Document > Get_DocumentReport_List()
            {
                List<Document> list = new List<Document>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + DocumentTable.DocumentID + ","
                        + DocumentTable.DocumentDate + ","
                        + DocumentTable.EmployeeID + ","
                        + DocumentTable.DocumentType + ","
                        + DocumentTable.ExcuteDate + ","
                        + DocumentTable.TargetDocumentID + ","
                        + DocumentTable.EmployeementID + ","
                        + DocumentTable.Notes
                    + " from   "
                    + DocumentTable.TableName
                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint DocumentID_ = Convert.ToUInt32(t.Rows[i][0].ToString());

                        DateTime documentdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        uint DocumnetType = Convert.ToUInt32(t.Rows[i][3].ToString());
                        DateTime executeDdte = Convert.ToDateTime(t.Rows[i][4].ToString());
                        Document target_document;
                        try
                        {
                            target_document = Get_Document_Info_BYID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        }
                        catch
                        {
                            target_document = null;
                        }
                        EmployeeMent EmployeeMent_;
                        try
                        {
                            EmployeeMent_ = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID(Convert.ToUInt32(t.Rows[i][6].ToString()));
                        }
                        catch
                        {
                            EmployeeMent_ = null;
                        }
                        string notes = t.Rows[i][7].ToString();
                        list .Add ( new Document(DocumentID_, documentdate, Employee_, DocumnetType, executeDdte, target_document, EmployeeMent_, notes));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Document_List" + ee.Message);
                    return list;
                }
            }
            public List<Document > Get_Employee_Document_List(Employee Employee_)
            {
                List<Document> list = new List<Document>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + DocumentTable.DocumentID + ","
                        + DocumentTable.DocumentDate + ","
                        + DocumentTable.DocumentType + ","
                        + DocumentTable.ExcuteDate + ","
                        + DocumentTable.TargetDocumentID + ","
                        + DocumentTable.EmployeementID + ","
                        + DocumentTable.Notes
                    + " from   "
                    + DocumentTable.TableName
                      + " where   "
                    + DocumentTable.EmployeeID +"="+ Employee_.EmployeeID 
                  );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint DocumentID_ = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime documentdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        uint DocumnetType = Convert.ToUInt32(t.Rows[i][2].ToString());
                        DateTime executeDdte = Convert.ToDateTime(t.Rows[i][3].ToString());
                        Document target_document;
                        try
                        {
                            target_document = Get_Document_Info_BYID(Convert.ToUInt32(t.Rows[i][4].ToString()));
                        }
                        catch
                        {
                            target_document = null;
                        }
                        EmployeeMent EmployeeMent_;
                        try
                        {
                            EmployeeMent_ = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID(Convert.ToUInt32(t.Rows[i][5].ToString()));
                        }
                        catch
                        {
                            EmployeeMent_ = null;
                        }
                        string notes = t.Rows[i][6].ToString();
                        list.Add(new Document(DocumentID_, documentdate, Employee_, DocumnetType, executeDdte, target_document, EmployeeMent_, notes));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Employee_Document_List" + ee.Message);
                    return list;
                }
            }

            //public Document Get_Employee_Active_Document(Employee Employee_)
            //{
            //    try
            //    {

            //        DataTable t = new DataTable();
            //        t = DB.GetData("select "
            //            + DocumentTable.DocumentID  + ","
            //            + DocumentTable.OprDate + ","
            //            + DocumentTable.Notes
            //         + " from   "
            //         + DocumentTable.TableName
            //         + " where "
            //        + DocumentTable.EmployeeID  + "="+ Employee_.EmployeeID
            //         + " and  "
            //        + DocumentTable.EndDate + " is  null"
            //      );

            //        if (t.Rows.Count == 1)
            //        {
            //            uint Documentid = Convert.ToUInt32(t.Rows[0][0]);
            //            DateTime OprDate = Convert.ToDateTime(t.Rows[0][1].ToString());

            //            string notes = t.Rows[0][2].ToString();
            //            List<EmployeeMentAssign> EmployeeMentAssignList = new EmployeeMentAssignSQL(DB).Get_Document_EmployeeMentAssign_List(Documentid);
            //            return new Document(Documentid, OprDate, Employee_, null , notes, EmployeeMentAssignList);

            //        }
            //        else return null;
            //    }
            //    catch (Exception ee)
            //    {
            //        throw new Exception("Get_Employee_Active_Document" + ee.Message);
            //        return null;
            //    }
            //}

        }
        public class SalaryClauseSQL
        {

            DatabaseInterface DB;
            private static class SalaryClauseTable
            {
                public const string TableName = "Company_Employee_SalaryClause";
                public const string EmployeeID = "EmployeeID";
                public const string SalaryClauseID = "SalaryClauseID";
                public const string CreateDate = "CreateDate";
                public const string SalaryClauseDesc = "SalaryClauseDesc";
                public const string ClauseType = "ClauseType";
                public const string ExecuteDate = "ExecuteDate";
                public const string MonthCount = "MonthCount";
                public const string Value = "ClauseValue";
                public const string Notes = "Notes";
            }
            public SalaryClauseSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public SalaryClause Get_SalaryClause_Info_BYID(uint  clauseid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + SalaryClauseTable.EmployeeID + ","
                        + SalaryClauseTable.CreateDate  + ","
                        + SalaryClauseTable.SalaryClauseDesc + ","
                        + SalaryClauseTable.ClauseType + ","
                        + SalaryClauseTable.ExecuteDate + ","
                        + SalaryClauseTable.MonthCount  + ","
                        + SalaryClauseTable.Value  + ","
                        + SalaryClauseTable.Notes  
                        + " from   "
                        + SalaryClauseTable.TableName
                        + " where "
                        + SalaryClauseTable.SalaryClauseID  + "=" + clauseid 
                      );
                    if (t.Rows.Count == 1)
                    {

                        Employee employee =new EmployeeSQL(DB ).GetEmployeeInforBYID ( Convert.ToUInt32(t.Rows[0][0].ToString()));
                        DateTime createdate = Convert.ToDateTime(t.Rows[0][1].ToString());
                        string desc = t.Rows[0][2].ToString();
                        bool type = Convert.ToInt32 (t.Rows[0][3].ToString())==1?true :false ;
                        DateTime executedate = Convert.ToDateTime(t.Rows[0][4].ToString());
                        uint? monthcount;
                        try
                        {
                            monthcount = Convert.ToUInt32 (t.Rows[0][5].ToString());
                        }
                        catch
                        {
                            monthcount = null;
                        }
                        double value = Convert.ToDouble(t.Rows[0][6].ToString());
                        string notes = t.Rows[0][7].ToString();
                        return new SalaryClause(employee,clauseid, createdate,desc, type , executedate ,monthcount,value ,notes  );

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_SalaryClause_Info" + ee.Message);
                    return null;
                }
            }

      
            public List<SalaryClause> Get_SalaryClause_List(Employee  employee)
            {
                List<SalaryClause> list = new List<SalaryClause>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + SalaryClauseTable.SalaryClauseID + ","
                        + SalaryClauseTable.CreateDate + ","
                        + SalaryClauseTable.SalaryClauseDesc + ","
                        + SalaryClauseTable.ClauseType + ","
                        + SalaryClauseTable.ExecuteDate  + ","
                        + SalaryClauseTable.MonthCount + ","
                        + SalaryClauseTable.Value + ","
                        + SalaryClauseTable.Notes
                        + " from   "
                        + SalaryClauseTable.TableName
                        + " where "
                        + SalaryClauseTable.EmployeeID  + "=" + employee.EmployeeID
                      );
                    for(int i=0;i<t.Rows .Count;i++)
                    {

                        uint clauseid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime createdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string desc = t.Rows[i][2].ToString();
                        bool type = Convert.ToInt32 (t.Rows[i][3].ToString())==1 ? true : false; ;
                        DateTime executedate = Convert.ToDateTime(t.Rows[i][4].ToString());
                        uint? monthcount;
                        try
                        {
                            monthcount = Convert.ToUInt32(t.Rows[i][5].ToString());
                        }
                        catch
                        {
                            monthcount = null;
                        }
                        double value = Convert.ToDouble(t.Rows[i][6].ToString());
                        string notes = t.Rows[i][7].ToString();
                        list.Add ( new SalaryClause(employee, clauseid, createdate, desc, type, executedate , monthcount, value, notes));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_SalaryClause_List" + ee.Message);
                    return null;
                }
            }

        }
        public class SalarysPayOrderSQL
        {
            DatabaseInterface DB;
            private static class SalarysPayOrderTable
            {
                public const string TableName = "Company_SalarysPayOrder";
                public const string SalarysPayOrderID = "SalarysPayOrderID";
                public const string OrderDate = "OrderDate";
                public const string ExecuteYear = "ExecuteYear";
                public const string ExecuteMonth = "ExecuteMonth";
                public const string Notes = "Notes";


            }
            public SalarysPayOrderSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public SalarysPayOrder Get_SalarysPayOrder_Info_ByID(uint  id)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + SalarysPayOrderTable.OrderDate  + ","
                        + SalarysPayOrderTable.ExecuteYear + ","
                        + SalarysPayOrderTable.ExecuteMonth + ","
                        + SalarysPayOrderTable.Notes 
                        + " from   "
                        + SalarysPayOrderTable.TableName
                        + " where "
                        + SalarysPayOrderTable.SalarysPayOrderID   + "=" + id
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime orderdate = Convert.ToDateTime(t.Rows [0][0].ToString ());
                        int executeyear = Convert.ToInt32(t.Rows[0][1].ToString());
                        int executemonth = Convert.ToInt32(t.Rows[0][2].ToString());
                        string  notes = t.Rows[0][3].ToString();
                        return new SalarysPayOrder(id ,orderdate ,executeyear,executemonth,notes );

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_SalarysPayOrder_Info" + ee.Message);
                    return null;
                }
            }
            public SalarysPayOrder Get_SalarysPayOrder_Info_ByMonth_Year(int year,int month)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + SalarysPayOrderTable.SalarysPayOrderID  + ","
                        + SalarysPayOrderTable.OrderDate + ","
  
                        + SalarysPayOrderTable.Notes
                        + " from   "
                        + SalarysPayOrderTable.TableName
                        + " where "
                        + SalarysPayOrderTable.ExecuteYear+"="+year 
                        + " and "
                        + SalarysPayOrderTable.ExecuteMonth + "=" + month 
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint id = Convert.ToUInt32(t.Rows[0][0].ToString());
                        DateTime orderdate = Convert.ToDateTime(t.Rows[0][1].ToString());
                        string notes = t.Rows[0][2].ToString();
                        return new SalarysPayOrder(id, orderdate, year , month , notes);

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    throw new Exception("Get_SalarysPayOrder_Info" + ee.Message);
                    return null;
                }
            }
    
            public List<SalarysPayOrder> Get_SalarysPayOrder_List()
            {
                List<SalarysPayOrder> list = new List<SalarysPayOrder>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + SalarysPayOrderTable.SalarysPayOrderID  + ","
                        + SalarysPayOrderTable.OrderDate + ","
                        + SalarysPayOrderTable.ExecuteYear + ","
                        + SalarysPayOrderTable.ExecuteMonth + ","
                        + SalarysPayOrderTable.Notes
                        + " from   "
                        + SalarysPayOrderTable.TableName
                      );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint id = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime orderdate = Convert.ToDateTime(t.Rows[i][1].ToString());
                       int executeyear = Convert.ToInt32(t.Rows[i][2].ToString());
                        int executemonth = Convert.ToInt32(t.Rows[i][3].ToString());
                        string notes = t.Rows[i][4].ToString();
                        list.Add(new SalarysPayOrder(id, orderdate, executeyear, executemonth, notes));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_SalarysPayOrder_List" + ee.Message);
                    return null;
                }
            }
            public List<SalarysPayOrderMonthReport> Get_GetSalarysPayOrderMonthReport_List_In_Year(int year)
            {
                List<SalarysPayOrderMonthReport> list = new List<SalarysPayOrderMonthReport>();
                try
                {
                    List<SalarysPayOrder> SalarysPayOrderList = Get_SalarysPayOrder_List().Where(x => x.ExecuteYear == year).ToList();
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).GetPayPayOrders_List().Where(x => x._SalarysPayOrder != null && x._SalarysPayOrder.ExecuteYear == year).ToList();

                    for (int i = 0; i < SalarysPayOrderList.Count; i++)
                    {
                        for(int j=1;j<=12; j++)
                        {
                            int monthno = i;
                            System.Globalization.CultureInfo AR_English = new System.Globalization.CultureInfo("ar-SY");
                            System.Globalization.DateTimeFormatInfo englishInfo = AR_English.DateTimeFormat;
                            string monthname = englishInfo.MonthNames[i - 1];
                            string payorderid= SalarysPayOrderList[i].SalarysPayOrderID .ToString ();
                            string orderdate = SalarysPayOrderList[i].OrderDate.ToShortDateString();


                            List<EmployeePayOrder> Month_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._SalarysPayOrder.ExecuteMonth == i).ToList();
                            string employeescount= Month_EmployeePayOrderList.Count .ToString ();
                            List<Money_Currency> Month_EmployeePayOrderList_moneycurrency = Money_Currency.Get_Money_Currency_List_From_EmployeePayOrder(Month_EmployeePayOrderList);
                            string moneyamount= Money_Currency.ConvertMoney_CurrencyList_TOString(Month_EmployeePayOrderList_moneycurrency);

                            list.Add(new SalarysPayOrderMonthReport(year, monthno, monthname, payorderid, orderdate, employeescount, moneyamount));

                        }
                    }
                   
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_GetSalarysPayOrderMonthReport_List_In_Year" + ee.Message);
                    return null;
                }
            }
            public List<SalarysPayOrderEmployeeReport> Get_GetSalarysPayOrderEmployees_Report_List(SalarysPayOrder SalarysPayOrder_)
            {
                List<SalarysPayOrderEmployeeReport> list = new List<SalarysPayOrderEmployeeReport>();
                try
                {

                    List<EmployeesReport> EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).GetPayPayOrders_List().Where (x=>x._SalarysPayOrder !=null ).ToList ();

                    for (int i = 0; i < EmployeesReportList.Count; i++)
                    {
             
                        uint employeeid = EmployeesReportList[i].EmployeeID ;
                        string employeename = EmployeesReportList[i].EmployeeName ;
                        string jobsate = EmployeesReportList[i].JobState ;
                        string employeementstate = EmployeesReportList[i].EmployeeMentState;
                        uint employeestatecode = EmployeesReportList[i].EmployeeStateCode;
                      
                        string expectedsalary = Get_Expected_Salary_ForEmployee_IN_Month(EmployeesReportList[i].EmployeeID , SalarysPayOrder_.ExecuteYear, SalarysPayOrder_.ExecuteMonth).ToString ();
                        uint? payorderid;
                        double? salarypayordervalue;
                        Currency salarypayorderCurency;
                        double? salarypayorderExchangerate;
                        List < EmployeePayOrder> employeeid_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._Employee.EmployeeID == EmployeesReportList[i].EmployeeID
                        &&x._SalarysPayOrder .SalarysPayOrderID== SalarysPayOrder_.SalarysPayOrderID).ToList();
                        if(employeeid_EmployeePayOrderList.Count >0)
                        {
                            payorderid = employeeid_EmployeePayOrderList[0].PayOrderID;
                            salarypayordervalue = employeeid_EmployeePayOrderList[0].Value ;
                            salarypayorderCurency = employeeid_EmployeePayOrderList[0]._Currency ;
                            salarypayorderExchangerate = employeeid_EmployeePayOrderList[0].ExchangeRate;
                        }
                        else 
                        {
                            payorderid = null;
                            salarypayordervalue = null ;
                            salarypayorderCurency = null ;
                            salarypayorderExchangerate = null ;
                        }


                        list.Add(new SalarysPayOrderEmployeeReport
                            (employeeid , employeename, jobsate , employeementstate,employeestatecode
                            , expectedsalary,payorderid , salarypayordervalue, salarypayorderCurency,salarypayorderExchangerate));
                    }

                   
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_GetSalarysPayOrderEmployees_Report_List" + ee.Message);
                }
                return list;
            }
            public double Get_Expected_Salary_ForEmployee_IN_Month(uint  EmployeeID, int year,int month)
            {
                try
                {
                    DateTime targetmonth = new DateTime(year, month, 1);
                    Employee _Employee = new EmployeeSQL(DB).GetEmployeeInforBYID(EmployeeID);
                    List <SalaryClause > SalaryClauseList_ = new SalaryClauseSQL(DB).Get_SalaryClause_List(_Employee);

                    List<SalaryClause> SalaryClauseDueMonthList = SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                       && x.ExecuteDate <= targetmonth
                       && x.MonthsCount == null).ToList();
                    SalaryClauseDueMonthList.AddRange(SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                    && x.ExecuteDate.AddMonths(Convert.ToInt32(x.MonthsCount)) > targetmonth
                     && x.ExecuteDate <= targetmonth
                    && x.MonthsCount != null
                    ).ToList());
                    double DueValue = SalaryClauseDueMonthList.Sum (x=>x.Value );

                                    List<SalaryClause> SalaryClauseDeductionMonthList = SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                 && x.ExecuteDate <= targetmonth
                 && x.MonthsCount == null).ToList();
                    SalaryClauseDeductionMonthList.AddRange(SalaryClauseList_.Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                    && x.ExecuteDate.AddMonths(Convert.ToInt32(x.MonthsCount)) > targetmonth
                     && x.ExecuteDate <= targetmonth
                    && x.MonthsCount != null
                    ).ToList());

                    double DeductionValue = SalaryClauseDeductionMonthList.Sum (x=>x.Value);

                    return DueValue - DeductionValue;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Expected_Salary_ForEmployee_IN_Month:" + ee.Message  );
                    return -1;
                }
            }
            #region SalarysPayOrder_Employee_Report_Currency
           
            public List<SalarysPayOrderReport_Currency> Get_GetSalarysPayOrderEmployees_Currency_Report_List(uint salaryspayorderid)
            {
                List<SalarysPayOrderReport_Currency> list = new List<SalarysPayOrderReport_Currency>();
                try
                {
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).GetPayPayOrders_List().Where(x => x._SalarysPayOrder != null
                    && x._SalarysPayOrder.SalarysPayOrderID == salaryspayorderid).ToList();

                    List<uint> CurrencyID_List = EmployeePayOrderList.Select(x => x._Currency.CurrencyID).Distinct().ToList();
                    for (int i = 0; i < CurrencyID_List.Count; i++)
                    {
                        List<EmployeePayOrder> Currency_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._Currency.CurrencyID == CurrencyID_List[i]).ToList();
                        Currency tempcurrency = Currency_EmployeePayOrderList[0]._Currency ;
                        uint currencyid = CurrencyID_List[i];
                        string currencyname = tempcurrency.CurrencyName;
                        //List<Money_Currency> Money_CurrencyLis = Money_Currency.Get_Money_Currency_List_From_EmployeePayOrder(Currency_EmployeePayOrderList);
                        double salarysvalue = Currency_EmployeePayOrderList.Sum (x=>x.Value );
                        List<PayOUT> PayOUTList = new List<PayOUT>();
                        for(int j=0;j< Currency_EmployeePayOrderList.Count;j++)
                        {
                            Operation Operation_ = new Operation(Operation.Employee_PayOrder, Currency_EmployeePayOrderList[j].PayOrderID);
                            PayOUTList.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(Operation_));

                        }
                        List<Money_Currency> Payout_Money_CurrencyList = Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList);
                        string paysvalues = Money_Currency.ConvertMoney_CurrencyList_TOString(Payout_Money_CurrencyList); 
                        double realsalarysvalue = Math .Round (Currency_EmployeePayOrderList.Sum (x=>x.Value /x.ExchangeRate ),3);
                        double realpaysvalues = Math.Round(Payout_Money_CurrencyList.Sum(x => x.Value / x.ExchangeRate), 3);


                        list.Add(new SalarysPayOrderReport_Currency
                            (currencyid , currencyname, salarysvalue , paysvalues , realsalarysvalue
                            , realpaysvalues));
                    }

              
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_GetSalarysPayOrderEmployees_Currency_Report_List" + ee.Message);
                }
                return list;
            }
            #endregion
            #region SalarysPayOrder_Year_Report_Currency
         
            public List<SalarysPayOrderReport_Currency> Get_GetSalarysPayOrder_Year_Report_Currency_List(int  year)
            {
                List<SalarysPayOrderReport_Currency> list = new List<SalarysPayOrderReport_Currency>();
                try
                {



                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).GetPayPayOrders_List().Where(x => x._SalarysPayOrder != null
                && x._SalarysPayOrder.ExecuteYear == year).ToList();

                    List<uint> CurrencyID_List = EmployeePayOrderList.Select(x => x._Currency.CurrencyID).Distinct().ToList();
                    for (int i = 0; i < CurrencyID_List.Count; i++)
                    {
                        List<EmployeePayOrder> Currency_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._Currency.CurrencyID == CurrencyID_List[i]).ToList();
                        Currency tempcurrency = Currency_EmployeePayOrderList[0]._Currency;
                        uint currencyid = CurrencyID_List[i];
                        string currencyname = tempcurrency.CurrencyName;
                        //List<Money_Currency> Money_CurrencyLis = Money_Currency.Get_Money_Currency_List_From_EmployeePayOrder(Currency_EmployeePayOrderList);
                        double salarysvalue = Currency_EmployeePayOrderList.Sum(x => x.Value);
                        List<PayOUT> PayOUTList = new List<PayOUT>();
                        for (int j = 0; j < Currency_EmployeePayOrderList.Count; j++)
                        {
                            Operation Operation_ = new Operation(Operation.Employee_PayOrder, Currency_EmployeePayOrderList[j].PayOrderID);
                            PayOUTList.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(Operation_));

                        }
                        List<Money_Currency> Payout_Money_CurrencyList = Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList);
                        string paysvalues = Money_Currency.ConvertMoney_CurrencyList_TOString(Payout_Money_CurrencyList);
                        double realsalarysvalue = Math.Round(Currency_EmployeePayOrderList.Sum(x => x.Value / x.ExchangeRate), 3);
                        double realpaysvalues = Math.Round(Payout_Money_CurrencyList.Sum(x => x.Value / x.ExchangeRate), 3);


                        list.Add(new SalarysPayOrderReport_Currency
                            (currencyid, currencyname, salarysvalue, paysvalues, realsalarysvalue
                            , realpaysvalues));
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_GetSalarysPayOrder_Year_Report_Currency_List" + ee.Message);
                    return null;
                }
                return list;
            }
            #endregion
        }
        public class EmployeePayOrderSQL
        {

            DatabaseInterface DB;
            internal static class PayOrderTable
            {
                public const string TableName = "Company_Employee_PayOrder";
                public const string PayOrderID = "PayOrderID";
                public const string PayOrderDate = "PayOrderDate";
                public const string SalarysPayOrderID = "SalarysPayOrderID";
                public const string PayOrderDesc = "PayOrderDesc";
                public const string EmployeeID = "EmployeeID";
                public const string CurrencyID = "CurrencyID";
                public const string ExchangeRate = "ExchangeRate";
                public const string PayOrderValue = "PayOrderValue";
            }
            public EmployeePayOrderSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public EmployeePayOrder GetPayOrder_INFO_BYID(uint PayOrderid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + PayOrderTable.PayOrderDate
                        + ","
                        + PayOrderTable.PayOrderDesc
                        + ","
                        + PayOrderTable.EmployeeID 
                        + ","
                        + PayOrderTable.ExchangeRate
                        + ","
                        + PayOrderTable.CurrencyID
                        + ","
                        + PayOrderTable.PayOrderValue
                        + ","
                        + PayOrderTable.SalarysPayOrderID 
                        + " from   "
                        + PayOrderTable.TableName
                        + " where "
                        + PayOrderTable.PayOrderID + "=" + PayOrderid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime payorderDate = Convert.ToDateTime(t.Rows[0][0].ToString());
                        string description = t.Rows[0][1].ToString();
                        Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID(Convert.ToUInt32(t.Rows[0][2].ToString()));

                        double exchangerate = Convert.ToDouble(t.Rows[0][3].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                        double  Value =Convert .ToDouble ( t.Rows[0][5].ToString());
  
                        SalarysPayOrder SalarysPayOrder_;
                        try
                        { SalarysPayOrder_ =
                            new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(Convert.ToUInt32(t.Rows[0][6].ToString()));

                        }catch
                        {
                            SalarysPayOrder_ = null;
                        }
                        return new EmployeePayOrder(PayOrderid, payorderDate,SalarysPayOrder_, description,Employee_ , currency, exchangerate, Value);
                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayOrder_INFO_BYID:" + ee.Message);
                    return null ;
                }
               
            }
            public EmployeePayOrder GetPayOrder_INFO_BY_SalarysPayOrderID(SalarysPayOrder SalarysPayOrder_, Employee Employee_)
            {
                try
                {
                    if (SalarysPayOrder_ == null || Employee_ == null) return null;
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + PayOrderTable.PayOrderID
                         + ","
                        + PayOrderTable.PayOrderDate
                        + ","
                        + PayOrderTable.PayOrderDesc
                        + ","
                        + PayOrderTable.ExchangeRate
                        + ","
                        + PayOrderTable.CurrencyID
                        + ","
                        + PayOrderTable.PayOrderValue

                        + " from   "
                        + PayOrderTable.TableName
                        + " where "
                         + PayOrderTable.EmployeeID  +"="+Employee_.EmployeeID
                           + " and "
                         + PayOrderTable.SalarysPayOrderID  + "=" + SalarysPayOrder_.SalarysPayOrderID 
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint PayOrderid= Convert.ToUInt32 (t.Rows[0][0].ToString());
                        DateTime payorderDate = Convert.ToDateTime(t.Rows[0][1].ToString());
                        string description = t.Rows[0][2].ToString();
                        double exchangerate = Convert.ToDouble(t.Rows[0][3].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                        double Value = Convert.ToDouble(t.Rows[0][5].ToString());
                       return new EmployeePayOrder(PayOrderid, payorderDate, SalarysPayOrder_, description, Employee_, currency, exchangerate, Value);
                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayOrder_INFO_BYID" + ee.Message);
                    return null;
                }

            }
            public EmployeePayOrder GetEmployeeSalaryPayOrder_By_Month(Employee Employee_, int year, int month)
            {
                try
                {
                    SalarysPayOrder SalarysPayOrder_ = new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByMonth_Year(year, month);
                    if (SalarysPayOrder_ != null)
                    {
                        EmployeePayOrder EmployeeSalaryPayOrder_ = GetPayOrder_INFO_BY_SalarysPayOrderID(SalarysPayOrder_, Employee_);
                        return EmployeeSalaryPayOrder_;
                    }
                    else return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetEmployeeSalaryPayOrder_By_Month" + ee.Message);
                    return null;
                }
            }

            public List<EmployeePayOrder> GetPayPayOrders_List()
            {
                try
                {
                    List<EmployeePayOrder> PayOrderList = new List<EmployeePayOrder>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + PayOrderTable.PayOrderID
                    + ","
                    + PayOrderTable.PayOrderDate
                    + ","
                    + PayOrderTable.PayOrderDesc
                    + ","
                    + PayOrderTable.EmployeeID   
                    + ","
                    + PayOrderTable.ExchangeRate
                    + ","
                    + PayOrderTable.CurrencyID
                    + ","
                    + PayOrderTable.PayOrderValue
                     + ","
                    + PayOrderTable.SalarysPayOrderID 
                    + " from   "
                    + PayOrderTable.TableName

                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint payinid = Convert.ToUInt32(t.Rows[i][PayOrderTable.PayOrderID].ToString());
                        DateTime payindate = Convert.ToDateTime(t.Rows[i][PayOrderTable.PayOrderDate ].ToString());
                        string description = t.Rows[i][PayOrderTable.PayOrderDesc].ToString();
                        Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID (Convert.ToUInt32(t.Rows[i][PayOrderTable.EmployeeID ].ToString()));
                        double exchangerate = Convert.ToDouble(t.Rows[i][PayOrderTable.ExchangeRate ].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][PayOrderTable.CurrencyID ].ToString()));
                        double Value = Convert.ToDouble(t.Rows[i][PayOrderTable.PayOrderValue ].ToString());
                        SalarysPayOrder SalarysPayOrder_;
                        try
                        {
                            uint Get_SalarysPayOrderID = Convert.ToUInt32(t.Rows[i][PayOrderTable.SalarysPayOrderID].ToString());

                            SalarysPayOrder_= new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(Get_SalarysPayOrderID);
                        }
                        catch
                        {
                            SalarysPayOrder_ = null;
                        }
                        PayOrderList.Add(new EmployeePayOrder(payinid, payindate, SalarysPayOrder_, description, Employee_, currency, exchangerate, Value));

                    }
                    return PayOrderList;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayOrders_List:" + ee.Message);
                }
            }
            public List<EmployeePayOrder> GetPayOrders_List_For_Employee(Employee  Employee_)
            {
                try
                {
                    List<EmployeePayOrder> PayOrderList = new List<EmployeePayOrder>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + PayOrderTable.PayOrderID
                    + ","
                    + PayOrderTable.PayOrderDate
                    + ","
                    + PayOrderTable.PayOrderDesc
                    + ","
                    + PayOrderTable.ExchangeRate
                    + ","
                    + PayOrderTable.CurrencyID
                    + ","
                    + PayOrderTable.PayOrderValue
                     + ","
                    + PayOrderTable.SalarysPayOrderID 
                    + " from   "
                    + PayOrderTable.TableName
                      + " from   "
                    + PayOrderTable.EmployeeID   +"="+Employee_  .EmployeeID    

                  );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint payinid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime payindate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string description = t.Rows[i][2].ToString();
                        double exchangerate = Convert.ToDouble(t.Rows[i][3].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][4].ToString()));
                        double Value = Convert.ToDouble(t.Rows[0][5].ToString());
                        SalarysPayOrder SalarysPayOrder_ =
   new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(Convert.ToUInt32(t.Rows[0][6].ToString()));
                        PayOrderList.Add(new EmployeePayOrder(payinid, payindate,SalarysPayOrder_, description, Employee_  , currency, exchangerate, Value ));
                    }
                    return PayOrderList;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayOrders_List_For_Employee" + ee.Message);
                    return null;
                }
            }
            internal double GetPayOrderValue(uint payorderid)
            {
                try
                {

                    return new OperationSQL(DB).Get_OperationValue(Operation.Employee_PayOrder , payorderid );
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayOrderValue:" + ee.Message);
                    return -1;
                }
            }
            internal double GetPayOrder_PaysValue(uint payorderid)
            {
                try
                {
                    return new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency (Operation.Employee_PayOrder, payorderid);
                }
                catch (Exception ee)
                {
                    throw new Exception("GetPayOrder_PaysValue:" + ee.Message);
                    return -1;
                }
            }
        }
        public class PayOrderReportSQL
        {
            DatabaseInterface DB;
     
      
            public PayOrderReportSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public List <PayOrderReport > Get_Company_PayOrdersReportList()
            {
                List<PayOrderReport> PayOrderList = new List<PayOrderReport>();
                try
                {
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).GetPayPayOrders_List();

                   
                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        uint payinid = EmployeePayOrderList[i].PayOrderID;
                        DateTime payindate = EmployeePayOrderList[i].PayOrderDate ;
                        string description = EmployeePayOrderList[i].PayOrderDesc ;
                        uint EmployeeID_ = EmployeePayOrderList[i]._Employee.EmployeeID;
                        string employeename = EmployeePayOrderList[i]._Employee.EmployeeName;
                        double Value = EmployeePayOrderList[i].Value ;

                        Currency Currency_ = EmployeePayOrderList[i]._Currency ;
                        double exchangerate = EmployeePayOrderList[i].ExchangeRate;
                        bool type;
                        if (EmployeePayOrderList[i]._SalarysPayOrder == null)
                            type = PayOrderReport.TYPE_PAY_ODER;
                        else
                            type = PayOrderReport.TYPE_SALARY_PAY_ODER;

                        List<PayOUT> PayOUTList = new PayOUTSQL(DB).GetPaysOUT_List(new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                        string pays_amount = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
                        PayOrderList.Add(new PayOrderReport(type, payinid, payindate, description, EmployeeID_, employeename,
                            Currency_, exchangerate, Value, pays_amount));

                    }
                   
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Company_PayOrdersReportList" + ee.Message);
                }
                return PayOrderList;
            }
            public List<PayOrderReport> Get_Employee_PayOrdersReportList(uint EmployeeID)
            {
                List<PayOrderReport> PayOrderList = new List<PayOrderReport>();
                try
                {
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).GetPayPayOrders_List().Where (x=>x._Employee .EmployeeID ==EmployeeID ).ToList ();


                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        uint payinid = EmployeePayOrderList[i].PayOrderID;
                        DateTime payindate = EmployeePayOrderList[i].PayOrderDate;
                        string description = EmployeePayOrderList[i].PayOrderDesc;
                        uint EmployeeID_ = EmployeePayOrderList[i]._Employee.EmployeeID;
                        string employeename = EmployeePayOrderList[i]._Employee.EmployeeName;
                        double Value = EmployeePayOrderList[i].Value;

                        Currency Currency_ = EmployeePayOrderList[i]._Currency;
                        double exchangerate = EmployeePayOrderList[i].ExchangeRate;
                        bool type;
                        if (EmployeePayOrderList[i]._SalarysPayOrder == null)
                            type = PayOrderReport.TYPE_PAY_ODER;
                        else
                            type = PayOrderReport.TYPE_SALARY_PAY_ODER;

                        List<PayOUT> PayOUTList = new PayOUTSQL(DB).GetPaysOUT_List(new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                        string pays_amount = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
                        PayOrderList.Add(new PayOrderReport(type, payinid, payindate, description, EmployeeID_, employeename,
                            Currency_, exchangerate, Value, pays_amount));

                    }

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Employee_PayOrdersReportList" + ee.Message);
                }
                return PayOrderList;
            }
           public AllPayOrdersReport Get_AllPayOrdersReport()
            {
                try
                {
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).GetPayPayOrders_List();
                    List<Money_Currency> Payorders_Value_MoneyCurrency = new List<Money_Currency>();
                    List<Money_Currency> Payorders_PaysAmount_MoneyCurrency = new List<Money_Currency>();
                    List<Money_Currency> Payorders_PaysRemain_MoneyCurrency = new List<Money_Currency>();

                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        Payorders_Value_MoneyCurrency.Add(new Money_Currency(EmployeePayOrderList[i]._Currency, EmployeePayOrderList[i].Value, EmployeePayOrderList[i].ExchangeRate));
                        List<PayOUT> PayOUTList = new PayOUTSQL(DB).GetPaysOUT_List(new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                        Payorders_PaysAmount_MoneyCurrency.AddRange(Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
                        double pays_remain = EmployeePayOrderList[i].Value- new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(new Operation(Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                        Payorders_PaysRemain_MoneyCurrency.Add(new Money_Currency(EmployeePayOrderList[i]._Currency, pays_remain, EmployeePayOrderList[i].ExchangeRate));
                    }
                    string Payorders_Value= Money_Currency.ConvertMoney_CurrencyList_TOString(Payorders_Value_MoneyCurrency);
                    string Payorders_PaysAmount = Money_Currency.ConvertMoney_CurrencyList_TOString(Payorders_PaysAmount_MoneyCurrency);
                    string Payorders_PaysRemain = Money_Currency.ConvertMoney_CurrencyList_TOString(Payorders_PaysRemain_MoneyCurrency);
                    double Payorders_RealValue = System.Math.Round(Payorders_Value_MoneyCurrency.Sum(x => x.Value / x.ExchangeRate), 3);
                    double Payorders_Pays_RealValue = System.Math.Round(Payorders_PaysAmount_MoneyCurrency.Sum(x => x.Value / x.ExchangeRate), 3);

                    return new AllPayOrdersReport(Payorders_Value, Payorders_PaysAmount, Payorders_PaysRemain
                        , Payorders_RealValue, Payorders_Pays_RealValue) ;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_AllPayOrdersReport" + ee.Message);
                    return null;
                }
            }
        }
    }
}
