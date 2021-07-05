using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Company.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Company
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
            //        System.Windows.Forms.MessageBox.Show("Get_EmployeesOPRReport_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return null;
            //    }
            //}
            public List<EmployeeMent_Employee_Report> Get_EmployeeMent_Employee_Report_List()
            {

                List<EmployeeMent_Employee_Report> EmployeeMent_Employee_Report_List = new List<EmployeeMent_Employee_Report>();
                try
                {

                    DataTable para = new DataTable();
                    para.Columns.Add("UserID", typeof(uint));

                    DataRow row = para.NewRow();
                    row["UserID"] = DB.__User.UserID;

                    para.Rows.Add(row);

                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.CompanySQL_Get_EmployeeMent_Employee_Report_List,
                       para);

                    return EmployeeMent_Employee_Report.Get_EmployeeMent_Employee_Report_List_From_DataTable (t);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Get_EmployeeMent_Employee_Report_List:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return EmployeeMent_Employee_Report_List;
                }
            }
            public List<EmployeesReport> GetEmployeesReportList()
            {

                List<EmployeesReport> EmployeeMent_Employee_Report_List = new List<EmployeesReport>();
                try
                {

                    DataTable para = new DataTable();
                    para.Columns.Add("UserID", typeof(uint));

                    DataRow row = para.NewRow();
                    row["UserID"] = DB.__User.UserID;

                    para.Rows.Add(row);

                    DataTable t = DB.Execute_Function(DatabaseInterface.OverLoad_SQL_Functions_Code.CompanySQL_GetEmployeesReportList ,
                       para);

                    return EmployeesReport.Get_EmployeesReport_List_From_DataTable (t);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetEmployeesReportList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return EmployeeMent_Employee_Report_List;
                }
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
                string s =DB .COMPANY .PartName+ " / ";
                while (f.ParentPartID  != null)
                {
                    f = GetPartInfoByID(Convert.ToUInt32(f.ParentPartID ));
                    f_path.Add(f.PartName );
                }
                for (int i = f_path.Count - 1; i >= 0; i--)
                    s += f_path[i] + " /";
                return s;
            }
            public bool CreatePart(string name,DateTime CreateDate, uint? parentid)
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                string parentid_string;
                if (parentid == null)
                    parentid_string = "null";
                else
                    parentid_string = parentid.ToString();
               try
                {
                    DB.ExecuteSQLCommand(" insert into "
                    + CompanyPartTable.TableName
                    + "("
                    + CompanyPartTable.PartName
                    + ","
                    + CompanyPartTable.ParentPartID 
                    + ","
                    + CompanyPartTable.CreateDate
                    + ")"
                    + "values"
                    + "("
                    + "'" + name + "'"
                    + ","
                    + parentid_string
                     + ","
                    + "'" + CreateDate .ToString("yyyy-MM-dd") + "'"
                    + ")"
                    );
                    DB.AddLog(
                  DatabaseInterface.Log.LogType.INSERT 
              , DatabaseInterface.Log.Log_Target.CompanyPart 
              , ""
                , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.CompanyPart
                            , ""
                          , false , ee.Message );
                    MessageBox.Show("CreatePart" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool UpdatePart(uint PartID, string newname, DateTime CreateDate)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                    + CompanyPartTable.TableName
                    + " set "
                    + CompanyPartTable.PartName  + "='" + newname + "'"
                     + ","
                    + CompanyPartTable.CreateDate  + "=" + "'" + CreateDate.ToString("yyyy-MM-dd") + "'"
                    + " where "
                    + CompanyPartTable.PartID + "=" +PartID 
                    );
                    DB.AddLog(
                           DatabaseInterface.Log.LogType.UPDATE 
                           , DatabaseInterface.Log.Log_Target.CompanyPart
                           , ""
                         , true , "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.UPDATE
                          , DatabaseInterface.Log.Log_Target.CompanyPart
                          , ""
                        , false, ee.Message);
                    MessageBox.Show("UpdatePart" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeletePart(uint  partid)
            {
                //List<Item> Items = new ItemSQL(DB).GetItemsInPart(Part__);
                //List<Part> Parts = new PartSQL(DB).GetPartChilds(Part__);
                //if (Parts.Count > 0 || Items.Count > 0)
                //{
                //    MessageBox.Show("المجلد" + Part__.PartName + " غير فارغ لا يمكن حذفه!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    Part part = GetPartInfoByID(partid);
                    if (new EmployeeMentSQL(DB).GetEmployeeMentsCountInPart(part) > 0)
                        throw new Exception("يجب حذف الوظائف التابعة لهذا القسم");
                    DB.ExecuteSQLCommand(
                        "delete from   "
                    + CompanyPartTable.TableName
                    + " where "
                    + CompanyPartTable.PartID + "=" + partid
                    );
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.DELETE 
                          , DatabaseInterface.Log.Log_Target.CompanyPart
                          , ""
                        , true, "");
                    return true;
                }
                catch(Exception ee)
                {
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.DELETE
                         , DatabaseInterface.Log.Log_Target.CompanyPart
                         , ""
                       , false , ee.Message );
                    MessageBox.Show("DeletePart"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
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
                    MessageBox.Show("GetParentPart" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("GetPartChilds" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("SearchPart" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return list;
                }

               
            }
            //public int GetPartIDByName(string name,int? id)
            //{
            //    string parentid;
            //    if (id == null) parentid = " is null";
            //    else parentid = "=" + id.ToString();

            //    System.Data.DataTable table = DB.GetData("select " + CompanyPartTable.PartID
            //        + " from " + CompanyPartTable.Part
            //        + " where " + CompanyPartTable.PartName + "='" + name + "'"
            //        );
            //    return Convert.ToInt32(table.Rows[0][0]);
            //}
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
            public bool IS_Move_Able(Part DestinationPart, Part Part)
            {

                if (DestinationPart == Part) return false;
                if (DestinationPart == null) return true;
                Part Parent_temp, Child_Temp;
                Child_Temp = DestinationPart;

                while (true)
                {

                    Parent_temp = GetParentPart(Child_Temp);
                    if (Parent_temp == Part) return false;
                    if (Parent_temp == null) return true;

                    Child_Temp = Parent_temp;
                }

            }
            public bool MoveParts(Part DestinationPart, List<Part> PartsList)
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                if (PartsList.Count == 0) return false;
                for (int i = 0; i < PartsList.Count; i++)
                {
                    if (!IS_Move_Able(DestinationPart, PartsList[i]))
                    {
                        MessageBox.Show("لا يمكن نقل قسم الى قسم ابن له", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                try
                {
                    for (int i = 0; i < PartsList.Count; i++)
                    {
                        string desteniationPart_id_str;
                        if (DestinationPart == null)
                            desteniationPart_id_str = "null";
                        else
                            desteniationPart_id_str = DestinationPart.PartID.ToString();
                        DB.ExecuteSQLCommand( "update "
                            + CompanyPartTable.TableName
                            + " set "
                            + CompanyPartTable.ParentPartID + "=" + desteniationPart_id_str
                            + " where "
                             + CompanyPartTable.PartID + "=" + PartsList[i].PartID
                            );
                        DB.AddLog(
                         DatabaseInterface.Log.LogType.UPDATE 
                         , DatabaseInterface.Log.Log_Target.CompanyPart
                         , ""
                       , true, "");
                    }
                    return true;
                }
                catch (Exception   ee)
                {
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.UPDATE
                        , DatabaseInterface.Log.Log_Target.CompanyPart
                        , ""
                      , false , ee.Message );
                    MessageBox.Show("MoveParts"+ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
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
                    MessageBox.Show("Get_EmployeeMentLevel_Info" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public bool Add_EmployeeMentLevel( string levelname)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( " insert into "
                    + EmployeeMentLevelTable.TableName
                    + "("
                    + EmployeeMentLevelTable.LevelName 
                    + ")"
                    + "values"
                    + "("
                     + "'" + levelname + "'"
                    + ")"
                    );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.INSERT 
                        , DatabaseInterface.Log.Log_Target.Employeement_Level
                        , ""
                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.INSERT
                       , DatabaseInterface.Log.Log_Target.Employeement_Level
                       , ""
                     , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add_EmployeeMentLevel" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_EmployeeMentLevel(uint levelid  ,string Newlevelname)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                       + EmployeeMentLevelTable.TableName
                       + " set "
                       + EmployeeMentLevelTable.LevelName  + "='" + Newlevelname + "'"
                       + " where "
                       + EmployeeMentLevelTable.LevelID  + "=" + levelid 
                    );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Employeement_Level
                      , ""
                    , true ,"");
                    return true;
                }
                catch (Exception ee)
                {

                    DB.AddLog(
                       DatabaseInterface.Log.LogType.UPDATE 
                       , DatabaseInterface.Log.Log_Target.Employeement_Level
                       , ""
                     , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("Update_EmployeeMentLevel" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            public bool Delete_EmployeeMentLevel(uint  levelid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    DB.ExecuteSQLCommand( "delete from  "
                    + EmployeeMentLevelTable.TableName
                    + " where "
                    + EmployeeMentLevelTable.LevelID  + "="+levelid 
                    );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Employeement_Level
                      , ""
                    , true , "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.DELETE
                     , DatabaseInterface.Log.Log_Target.Employeement_Level
                     , ""
                   , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("Delete_EmployeeMentLevel" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
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
                    System.Windows.Forms.MessageBox.Show("Get_EmployeeMentLevel_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_EmployeeMent_InfoBYID" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
                
            public bool Add_EmployeeMent(string name,  DateTime CreateDate,uint levelid, Part  Part_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" insert into "
                    + EmployeeMentTable.TableName
                    + "("
                    + EmployeeMentTable.EmployeeMentName  + ","
                    + EmployeeMentTable.CreateDate  + ","
                    + EmployeeMentTable.LevelID + ","
                    + EmployeeMentTable.PartID
                    + ")"
                    + "values"
                    + "("
                     + "'" + name + "'"
                    + ","
                    + "'" + CreateDate .ToString("yyyy-MM-dd") + "'"
                    + ","
                    +levelid
                    + ","
                    +(Part_ ==null ?"null": Part_ .PartID.ToString ())
                    + ")"
                    );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.INSERT
                    , DatabaseInterface.Log.Log_Target.Employeement
                    , name
                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Employeement
                   , name
                     , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("AddEmployee" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_EmployeeMent(uint ID, string name,  DateTime CreatDate,uint levelid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(
                         "update  "
                       + EmployeeMentTable.TableName
                       + " set "
                       + EmployeeMentTable.EmployeeMentName  + "='" + name + "'"
                       + ","
                       + EmployeeMentTable.CreateDate + "='" + CreatDate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                        + ","
                       + EmployeeMentTable.LevelID  + "=" + levelid 

                       + " where "
                       + EmployeeMentTable.EmployeeMentID  + "=" + ID
                    );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Employeement
                   , "تعديل الوظيفة ذات الرقم :"+ID .ToString ()
                     , true ,"");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                  , DatabaseInterface.Log.Log_Target.Employeement
                  , "تعديل الوظيفة ذات الرقم :" + ID.ToString()
                    , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_EmployeeMent" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool MoveEmployeeMents(Part DestinationPart, List<EmployeeMent> EmployeeMentList)
            {
                if (EmployeeMentList.Count == 0) return false;
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    for (int i = 0; i < EmployeeMentList.Count; i++)
                    {
                        DB.ExecuteSQLCommand( "update "
                            + EmployeeMentTable.TableName
                            + " set "
                            + EmployeeMentTable.PartID  + "=" +(DestinationPart ==null ?"null": DestinationPart.PartID.ToString ())
                            + " where "
                             + EmployeeMentTable.EmployeeMentID  + "=" + EmployeeMentList[i].EmployeeMentID
                            );
                    }
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.UPDATE
                 , DatabaseInterface.Log.Log_Target.Employeement
                 ,""
                   , true ,"");
                    return true;
                }
                catch(Exception ee)
                {
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.UPDATE
                , DatabaseInterface.Log.Log_Target.Employeement
                , ""
                  , false , ee.Message );
                    MessageBox.Show("فشل النقل ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Delete_EmployeeMent(uint ID)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(
                        "update  "
                    + EmployeeMentTable.TableName
                    + " where "
                    + EmployeeMentTable.EmployeeMentID  + "=" + ID
                    );
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.DELETE 
               , DatabaseInterface.Log.Log_Target.Employeement
               , ""
                 , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                  DatabaseInterface.Log.LogType.DELETE
              , DatabaseInterface.Log.Log_Target.Employeement
              , ""
                , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Delete_EmployeeMent" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
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
                        uint ID = Convert.ToUInt32(t.Rows[i][EmployeeMentTable.EmployeeMentID]);
                        string name = t.Rows[i][EmployeeMentTable.EmployeeMentName ].ToString();
                        DateTime createdate = Convert.ToDateTime(t.Rows[i][EmployeeMentTable.CreateDate ].ToString());
                        EmployeeMentLevel EmployeeMentLevel_= new EmployeeMentLevelSQL(DB).Get_EmployeeMentLevel_Info_BY_ID (Convert.ToUInt32(t.Rows[i][EmployeeMentTable.LevelID ]));
                        Part Part_;
                        try
                        {
                            Part_ = new PartSQL(DB).GetPartInfoByID(Convert.ToUInt32(t.Rows[i][EmployeeMentTable.PartID]));

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
                    System.Windows.Forms.MessageBox.Show("Get_EmployeeMent_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("Get_EmployeeMent_List_IN_Part" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("GetEmployeeMentsCountInPart" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            public Employee GetEmployeeInforBY_RowID(uint rowid)
            {
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
                        + " where "
                        + DatabaseInterface.ROWID_COLUMN + "=" + rowid
                      );
                    if (t.Rows.Count == 1)
                    {

                        string name = t.Rows[0][0].ToString();
                        bool gender = Convert.ToInt32(t.Rows[0][1].ToString()) == 1 ? true : false;
                        DateTime birthdate = Convert.ToDateTime(t.Rows[0][2].ToString());
                        string nationalid = t.Rows[0][3].ToString();
                        MaritalStatus MaritalStatus_ = MaritalStatus.Get_MaritalStatus_BY_ID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                        string mobile = t.Rows[0][5].ToString();
                        string phone = t.Rows[0][6].ToString();
                        string emailaddress = t.Rows[0][7].ToString();
                        string address = t.Rows[0][8].ToString();
                        string notes = t.Rows[0][9].ToString();
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][10].ToString()));
                        uint EmployeeID = Convert.ToUInt32(t.Rows[0][11].ToString());
                        return new Employee(EmployeeID, name, gender, birthdate, nationalid, MaritalStatus_
                            , mobile, phone, emailaddress, address, notes, currency);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetEmployeeInforBY_RowID:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

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
                    System.Windows.Forms.MessageBox.Show("GetEmployeeInforBYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public Employee  AddEmployee( string name, bool gender,DateTime birthdate
               , string nationalid,MaritalStatus MaritalStatus_,  string mobile, string phone,string emailaddress, string address,string report,uint CurrencyID)
            {
                try
                {
              
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = DB.GetData (" insert into "
                    + EmployeeTable.TableName
                    + "("
                    + EmployeeTable.EmployeeName  + ","
                    + EmployeeTable.Gender + ","
                    + EmployeeTable.BirthDate  + ","
                    + EmployeeTable.NationalID  + ","
                    + EmployeeTable.MaritalStatus  + ","
                    + EmployeeTable.Mobile + ","
                     + EmployeeTable.Phone  + ","
                      + EmployeeTable.EmailAddress  + ","
                    + EmployeeTable.Address + ","
                    + EmployeeTable.Report + ","
                    + EmployeeTable.CurrencyID 
                    + ")"
                    + "values"
                    + "("
                     + "'" + name + "'"
                    + ","
                    + (gender  ? 1 : 0).ToString()
                    + ","
                    + "'" + birthdate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                     + ","
                    + "'" + nationalid  + "'"
                     + ","
                    +MaritalStatus_.MaritalStatusID
                    + ","
                    + "'" + mobile + "'"
                    + ","
                    + "'" + phone  + "'"
                    + ","
                    + "'" + emailaddress  + "'"
                    + ","
                    + "'" + address + "'"
                    + ","
                    + "'" + report + "'"
                      + ","
                    + CurrencyID
                    + ")"
                     + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.INSERT 
                     , DatabaseInterface.Log.Log_Target.Employee
                     , ""
                   , true ,"");
                    return GetEmployeeInforBY_RowID(rowid );
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.INSERT
                    , DatabaseInterface.Log.Log_Target.Employee
                    , ""
                  , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("AddEmployee" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
            }
            public bool UpdateEmpolyee(uint employeeidid, string name, bool gender, DateTime birthdate
               , string nationalid, MaritalStatus MaritalStatus_, string mobile, string phone, string emailaddress, string address, string report,uint CurrecnyID)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                       + EmployeeTable.TableName
                       + " set "
                       + EmployeeTable.EmployeeName  + "='" + name + "'"
                       + ","
                       + EmployeeTable.Gender  + "=" + (gender ? 1 : 0).ToString()
                       + ","
                        + EmployeeTable.BirthDate  +"='" + birthdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                      + ","
                       + EmployeeTable.NationalID  + "='" + nationalid  + "'"
                       + ","
                       + EmployeeTable.MaritalStatus  + "=" + MaritalStatus_.MaritalStatusID 
                        + ","
                       + EmployeeTable.Mobile + "='" + mobile + "'"
                       + ","
                       + EmployeeTable.Phone + "='" + phone + "'"
                         + ","
                       + EmployeeTable.EmailAddress  + "='" + emailaddress  + "'"
                       + ","
                       + EmployeeTable.Address + "='" + address + "'"
                        + ","
                       + EmployeeTable.Report   + "='" + report + "'"
                        + ","
                       + EmployeeTable.CurrencyID  + "=" + CurrecnyID
                    + " where "
                    + EmployeeTable.EmployeeID  + "=" + employeeidid 
                    );
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.UPDATE 
                     , DatabaseInterface.Log.Log_Target.Employee
                     , ""
                   , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.UPDATE
                    , DatabaseInterface.Log.Log_Target.Employee
                    , ""
                  , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("UpdateEmpolyee" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteEmployee(uint employeeid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from   "
                    + EmployeeTable.TableName
                    + " where "
                    + EmployeeTable.EmployeeID  + "=" + employeeid
                    );
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.DELETE 
                     , DatabaseInterface.Log.Log_Target.Employee
                     , ""
                   , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.DELETE
                     , DatabaseInterface.Log.Log_Target.Employee
                     , ""
                   , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("DeleteEmployee" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public byte[] GetEmployeeImage(uint  EmployeeID_ )
            {
                byte[] image_array;
                try
                {
                    return  DB.GetData_ByteArray   ("select "
                        + EmployeeImageTable.Employee_Image
                        + " from "
                        + EmployeeImageTable.TableName
                        + " where "
                        + EmployeeImageTable.EmployeeID + " = " + EmployeeID_);
                    //if (t.Rows.Count > 0)
                    //{
                    //    image_array = (byte[])t.Rows[0][0];
                    //    return image_array;
                    //}
                    //else return null;

                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetEmployeeImage:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            public bool SetEmployeeImage(uint EmployeeID_, byte[] Image_)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (!UnSetEmployeeImage(EmployeeID_)) throw new Exception("تعذر اعادة ضبط الصورة");


                    List<byte> FullMessage = new List<byte>();
                    FullMessage.Add(DatabaseInterface.MessageType.ExecuteSQlCMD_INSERT_Serialize);

                    byte[] TableName_Field = Encoding.UTF8.GetBytes(EmployeeImageTable.TableName);
                    FullMessage.AddRange(BitConverter.GetBytes(TableName_Field.Length));
                    FullMessage.AddRange(TableName_Field);

                    FullMessage.AddRange(BitConverter.GetBytes(2));//rows count

                    byte[] ItemID_Field = Encoding.UTF8.GetBytes(EmployeeImageTable.EmployeeID );
                    byte[] ItemID_Value = Encoding.UTF8.GetBytes(EmployeeID_.ToString());
                    FullMessage.Add(0x00);
                    FullMessage.AddRange(BitConverter.GetBytes(ItemID_Field.Length));
                    FullMessage.AddRange(ItemID_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(ItemID_Value.Length));
                    FullMessage.AddRange(ItemID_Value);


                    byte[] FileData_Field = Encoding.UTF8.GetBytes(EmployeeImageTable.Employee_Image);
                    FullMessage.Add(0x01);
                    FullMessage.AddRange(BitConverter.GetBytes(FileData_Field.Length));
                    FullMessage.AddRange(FileData_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(Image_ .Length));
                    FullMessage.AddRange(Image_);

                    string path = Application.StartupPath + "\\" + "OverLoadTemp";
                    if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

                    string Packe_File_Path = path + "\\" + "tmp." + 0;
                    int j = 1;
                    while (System.IO.File.Exists(Packe_File_Path))
                    {

                        Packe_File_Path = path + "\\" + "tmp." + j;


                    }




                    System.IO.File.WriteAllBytes(Packe_File_Path, DB.OverLoadEndPoint_.Encrypt(FullMessage.ToArray()));
                    int File_numBytes = Convert.ToInt32(new System.IO.FileInfo(Packe_File_Path).Length);
                    if (File_numBytes <= 0) throw new Exception("فشل انشاء حزمة البيانات");
                    ExecuteSQLCommand_Insert_Serialiaze_Form ExecuteSQLCommand_Insert_Serialiaze_Form_
                       = new ExecuteSQLCommand_Insert_Serialiaze_Form(DB.OverLoadEndPoint_, Packe_File_Path
                       );

                    FullMessage = new List<byte>();
                    GC.Collect();
                    ExecuteSQLCommand_Insert_Serialiaze_Form_.ShowDialog();
                    if (ExecuteSQLCommand_Insert_Serialiaze_Form_.DialogResult == DialogResult.OK)
                    {
                        DB.AddLog(
                    DatabaseInterface.Log.LogType.INSERT
                    , DatabaseInterface.Log.Log_Target.Employee_Image
                    , ""
                  , true, "");
                        GC.Collect();
                        return true;
                    }
                    else
                    {
                        GC.Collect(); return false;
                    }

                }
                catch (Exception ee)
                {
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.INSERT
                        , DatabaseInterface.Log.Log_Target.Employee_Image
                        , ""
                      , false , ee.Message );
                    MessageBox.Show("SetEmployeeImage:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool UnSetEmployeeImage(uint EmployeeID_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( "delete from "
                        + EmployeeImageTable.TableName
                        + " where "
                        + EmployeeImageTable.EmployeeID + "=" + EmployeeID_
                        );
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.DELETE 
                     , DatabaseInterface.Log.Log_Target.Employee_Image
                     , ""
                   , true, "");
                    return true;
                }
                catch(Exception ee)
                {
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.DELETE
                     , DatabaseInterface.Log.Log_Target.Employee_Image
                     , ""
                   , false ,ee.Message );
                    return false;
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
                    System.Windows.Forms.MessageBox.Show("GetEmployeeUserAccountList" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("Get_All_Employees:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_Qualification_Info" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public bool Add_Qualification(uint EmployeeID, string QualificationDesc,
                DateTime startdate, DateTime enddate, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (startdate >=enddate )
                    {
                        MessageBox.Show("تاريخ النعاية يجب ان يكون اكبر من تاريخ البداية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false ;
                    }
                    DB.ExecuteSQLCommand( " insert into "
                    + EmployeeQualificationTable.TableName
                    + "("
                    + EmployeeQualificationTable.EmployeeID + ","
                    + EmployeeQualificationTable.QualificationDesc + ","
                    + EmployeeQualificationTable.StartDate + ","
                    + EmployeeQualificationTable.EndDate + ","
                    + EmployeeQualificationTable.Notes
                    + ")"
                    + "values"
                    + "("
                    + EmployeeID
                    + ","
                     + "'" + QualificationDesc + "'"
                    + ","
                    + "'" + startdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                     + "'" + enddate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + "'" + notes + "'"
                    + ")"
                    );
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.INSERT 
                     , DatabaseInterface.Log.Log_Target.Employee_Qualification 
                     , ""
                   , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.INSERT
                     , DatabaseInterface.Log.Log_Target.Employee_Qualification
                     , ""
                   , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add_Qualification" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_Qualification(uint EmployeeID, string QualificationDesc, string NewQualificationDesc,  DateTime startdate
                , DateTime enddate, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                       + EmployeeQualificationTable.TableName
                       + " set "
                       + EmployeeQualificationTable.QualificationDesc + "='" + NewQualificationDesc + "'"
                       + ","
                       + EmployeeQualificationTable.StartDate + "='" + startdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                       + ","
                       + EmployeeQualificationTable.EndDate + "='" + enddate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                       + ","
                       + EmployeeQualificationTable.Notes + "='" + notes + "'"
                       + " where "
                       + EmployeeQualificationTable.QualificationDesc + "='" + QualificationDesc + "'"
                         + " and "
                        + EmployeeQualificationTable.EmployeeID + "=" + EmployeeID
                       );
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.UPDATE 
                     , DatabaseInterface.Log.Log_Target.Employee_Qualification
                     , ""
                   , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.UPDATE
                    , DatabaseInterface.Log.Log_Target.Employee_Qualification
                    , ""
                  , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_Qualification" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            public bool Delete_Qualification(uint EmployeeID, string QualificationDesc)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( "update  "
                    + EmployeeQualificationTable.TableName
                    + " where "
                    + EmployeeQualificationTable.QualificationDesc + "='" + QualificationDesc + "'"
                     + " and "
                        + EmployeeQualificationTable.EmployeeID + "=" + EmployeeID
                    );
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.DELETE 
                    , DatabaseInterface.Log.Log_Target.Employee_Qualification
                    , ""
                  , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.DELETE
                    , DatabaseInterface.Log.Log_Target.Employee_Qualification
                    , ""
                  , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Delete_Qualification" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
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
                    System.Windows.Forms.MessageBox.Show("Get_Qualification_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_Certificate_Info" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public bool Add_Certificate(uint EmployeeID, string CertificateDesc, string university,
                DateTime  startdate,DateTime enddate, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( " insert into "
                    + EmployeeCertificateTable.TableName
                    + "("
                    + EmployeeCertificateTable.EmployeeID + ","
                    + EmployeeCertificateTable.CertificateDesc + ","
                    + EmployeeCertificateTable.University  + ","
                    + EmployeeCertificateTable.StartDate  + ","
                    + EmployeeCertificateTable.EndDate  + ","
                    + EmployeeCertificateTable.Notes
                    + ")"
                    + "values"
                    + "("
                    + EmployeeID 
                    + ","
                     + "'" + CertificateDesc + "'"
                    + ","
                     + "'" + university  + "'"
                    + ","
                    + "'" + startdate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                     + "'" + enddate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + "'" + notes + "'"
                    + ")"
                    );
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.INSERT 
                    , DatabaseInterface.Log.Log_Target.Employee_Certificate 
                    , ""
                  , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Employee_Certificate
                   , ""
                 , false ,ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add_Certificate" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_Certificate(uint EmployeeID, string CertificateDesc, string NewCertificateDesc,string university, DateTime startdate
                ,DateTime enddate, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( "update  "
                       + EmployeeCertificateTable.TableName
                       + " set "
                       + EmployeeCertificateTable.CertificateDesc + "='" + NewCertificateDesc + "'"
                       + ","
                       + EmployeeCertificateTable.University + "='" + startdate  + "'"
                       + ","
                       + EmployeeCertificateTable.StartDate  + "='" + startdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                       + ","
                       + EmployeeCertificateTable.EndDate  + "='" +enddate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                       + ","
                       + EmployeeCertificateTable.Notes + "='" + notes + "'"
                       + " where "
                       + EmployeeCertificateTable.CertificateDesc + "='" + CertificateDesc + "'"
                         + " and "
                        + EmployeeCertificateTable.EmployeeID + "=" + EmployeeID
                       );
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Employee_Certificate
                   , ""
                 , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                  DatabaseInterface.Log.LogType.UPDATE
                  , DatabaseInterface.Log.Log_Target.Employee_Certificate
                  , ""
                , false ,ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_Certificate" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            public bool Delete_Certificate(uint EmployeeID, string CertificateDesc)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( "update  "
                    + EmployeeCertificateTable.TableName
                    + " where "
                    + EmployeeCertificateTable.CertificateDesc + "='" + CertificateDesc + "'"
                     + " and "
                        + EmployeeCertificateTable.EmployeeID + "=" + EmployeeID
                    );
                    DB.AddLog(
                  DatabaseInterface.Log.LogType.DELETE 
                  , DatabaseInterface.Log.Log_Target.Employee_Certificate
                  , ""
                , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                 DatabaseInterface.Log.LogType.DELETE
                 , DatabaseInterface.Log.Log_Target.Employee_Certificate
                 , ""
               , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Delete_Certificate" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
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
                    System.Windows.Forms.MessageBox.Show("Get_Certificate_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_Document_Info_BYID" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public bool Create_Document(uint EmployeeID,uint  type, DateTime executedate,Document targetdocument,EmployeeMent EmployeeMent_, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    Check_Document( EmployeeID,   type,  executedate,  targetdocument,  EmployeeMent_);
                    DB.ExecuteSQLCommand( " insert into "
                    + DocumentTable.TableName
                    + "("
                     + DocumentTable.EmployeeID + ","
                        + DocumentTable.DocumentType + ","
                        + DocumentTable.ExcuteDate + ","
                        + DocumentTable.TargetDocumentID + ","
                        + DocumentTable.EmployeementID + ","
                        + DocumentTable.Notes
                    + ")"
                    + "values"
                    + "("
                    + EmployeeID
                    + ","
                    + type 
                    + ","
                    + "'" + executedate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                     + (targetdocument ==null ?"null":targetdocument .DocumentID.ToString ())
                    + ","
                     + (EmployeeMent_ == null ? "null" : EmployeeMent_.EmployeeMentID .ToString())
                    + ","
                    + "'" + notes + "'"
                    + ")"
                    );
                    DB.AddLog(
                         DatabaseInterface.Log.LogType.INSERT 
                         , DatabaseInterface.Log.Log_Target.Document 
                         , ""
                       , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.INSERT
                        , DatabaseInterface.Log.Log_Target.Document
                        , ""
                      , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Create_Document:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_Document(uint DocumentID, DateTime executedate, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    Document document = Get_Document_Info_BYID(DocumentID);
                    List<Document> documentlist = Get_Employee_Document_List(document._Employee);
                    List<Document> Up_documentlist = documentlist.Where(x => x.ExecuteDate >= document.ExecuteDate).ToList();
                    List<Document> Down_documentlist = documentlist.Where(x => x.ExecuteDate <= document.ExecuteDate).ToList();
                    if (Up_documentlist.Where(x => x.ExecuteDate < executedate).ToList().Count > 0
                        ||Down_documentlist.Where(x => x.ExecuteDate > executedate).ToList().Count > 0)
                        throw new Exception("تاريخ التنفيذ يجب ان يكون اكبر من تاريخ التنفيذ للمستند السابق و اصغر من تاريخ التنفيذ للمستند الاحق");
                    DB.ExecuteSQLCommand("update  "
                       + DocumentTable.TableName
                       + " set "
                       + DocumentTable.ExcuteDate  + "='" + executedate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                       + ","
                       + DocumentTable.Notes + "='" + notes + "'"
                       + " where "
                       + DocumentTable.DocumentID + "=" + DocumentID
                    );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.UPDATE 
                        , DatabaseInterface.Log.Log_Target.Document
                        , ""
                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.UPDATE
                       , DatabaseInterface.Log.Log_Target.Document
                       , ""
                     , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_Document" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            public bool Delete_Document(uint DocumentID_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    Document document = Get_Document_Info_BYID(DocumentID_);
                    List<Document> documentlist = Get_Employee_Document_List(document._Employee);
                    List<Document> Up_documentlist = documentlist.Where(x => x.DocumentID  > document.DocumentID ).ToList();
                    if (Up_documentlist.Count > 0)
                        throw new Exception("  يمكن حذف اخر مستند فقط");
                    DB.ExecuteSQLCommand( "delete from  "
                    + DocumentTable.TableName
                    + " where "
                    + DocumentTable.DocumentID + "=" + DocumentID_
                    );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE 
                       , DatabaseInterface.Log.Log_Target.Document
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE
                      , DatabaseInterface.Log.Log_Target.Document
                      , ""
                    , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("فشل حذف المستند" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
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
                    System.Windows.Forms.MessageBox.Show("Get_Document_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("Get_Employee_Document_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            //        System.Windows.Forms.MessageBox.Show("Get_Employee_Active_Document" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_SalaryClause_Info" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            public bool Add_SalaryClause(Employee Employee_, string desc,bool type,DateTime  executedate,uint? monthcount,double value,string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");



                    List<SalaryClause> SalaryClauseList = Get_SalaryClause_List(Employee_);
                    SalaryClauseList.Add(new SalaryClause(Employee_, 0, DateTime.Now, "", type, executedate, monthcount, value, ""));

                    List<SalaryClause> SalaryClauseList_End_not_null = SalaryClauseList.Where(x => x.MonthsCount != null).ToList();
                    DateTime  min_date= SalaryClauseList.Select(x => x.ExecuteDate).Min();

                    List<SalaryClause> SalaryClauseList_End_null = SalaryClauseList.Where(x => x.MonthsCount == null).ToList();
                    if(SalaryClauseList_End_not_null.Count >0)
                    {
                        DateTime max_date = SalaryClauseList_End_not_null.Select(x => x.ExecuteDate.AddMonths(Convert.ToInt32(x.MonthsCount))).Max();

                        for (int i = min_date.Year; i <= max_date.Year; i++)
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                double UNLimited_due = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                  &&
                                  x.ExecuteDate.Year >= i && x.ExecuteDate.Month >= j).Sum(y => y.Value);
                                double UNLimited_dedu = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                  &&
                                  x.ExecuteDate.Year >= i && x.ExecuteDate.Month >= j).Sum(y => y.Value);

                                double Limited_due = SalaryClauseList_End_not_null
                                    .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                    && x.ExecuteDate.Year == i && x.ExecuteDate.Month == j).Sum(y => y.Value);

                                double Limited_dedu = SalaryClauseList_End_not_null
                                   .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   && x.ExecuteDate.Year == i && x.ExecuteDate.Month == j).Sum(y => y.Value);

                                if (UNLimited_due + Limited_due - UNLimited_dedu - Limited_dedu < 0)
                                    throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث في سنة:" + i + ",شهر:" + j);
                            }
                        }
                        double due_before_range = SalaryClauseList_End_null
                           .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                             &&
                            x.ExecuteDate.Year < min_date.Year && x.ExecuteDate.Month < min_date.Month).Sum(y => y.Value);
                        double dedu_before_range = SalaryClauseList_End_null
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   &&
                                  x.ExecuteDate.Year < min_date.Year && x.ExecuteDate.Month < min_date.Month).Sum(y => y.Value);
                        if ((due_before_range - dedu_before_range) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث قبل تاريخ:" + min_date);

                        double due_after_range = SalaryClauseList_End_null
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                   &&
                                  x.ExecuteDate.Year > max_date.Year && x.ExecuteDate.Month > max_date.Month).Sum(y => y.Value);
                        double dedu_after_range = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   &&
                                  x.ExecuteDate.Year > max_date.Year && x.ExecuteDate.Month > max_date.Month).Sum(y => y.Value);
                        if ((due_before_range - dedu_before_range) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث بعد تاريخ:" + max_date);
                    }
                    else
                    {

                        double due_all = SalaryClauseList
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_DUE).Sum(y => y.Value);
                        double dedu_all = SalaryClauseList
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction).Sum(y => y.Value);
                        if ((due_all - dedu_all) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر:" );
                    }

                    DB.ExecuteSQLCommand( " insert into "
                    + SalaryClauseTable.TableName
                    + "("
                    + SalaryClauseTable.EmployeeID + ","
                        + SalaryClauseTable.SalaryClauseDesc + ","
                        + SalaryClauseTable.ClauseType + ","
                        + SalaryClauseTable.ExecuteDate  + ","
                        + SalaryClauseTable.MonthCount + ","
                        + SalaryClauseTable.Value + ","
                        + SalaryClauseTable.Notes + ","
                        + SalaryClauseTable.CreateDate 
                    + ")"
                    + "values"
                    + "("
                    + Employee_. EmployeeID 
                    + ","
                     + "'" + desc  + "'"
                    + ","
                     + (type ==true ?"1":"0")
                    + ","
                     + "'" + executedate.ToString("yyyy-MM-dd") + "'"
                    + ","
                     + (monthcount ==null ?"null":monthcount .ToString ())
                    + ","
                     + value 
                    + ","
                    + "'" + notes + "'"
                     + ","
                      + "'" + DateTime .Now .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ")"
                    );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT 
                      , DatabaseInterface.Log.Log_Target.Employee_SalaryClause 
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                      , DatabaseInterface.Log.Log_Target.Employee_SalaryClause
                      , ""
                    , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add_SalaryClause:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_SalaryClause(uint clauseid, string Desc_,DateTime  executedate, uint?  monthcount,double value, string  notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DialogResult dd = MessageBox.Show(" تعديل بند الراتب سيؤدي الى خلل في ارشيف الرواتب في حال تم استخدامه سابقا في صرف راتب", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return false;

                    SalaryClause SalaryClause_ = Get_SalaryClause_Info_BYID(clauseid);
        
                    List<SalaryClause> SalaryClauseList = Get_SalaryClause_List(SalaryClause_._Employee );
                    SalaryClauseList.RemoveAll(x => x.SalaryClauseID == clauseid);
                    SalaryClauseList.Add(new SalaryClause(SalaryClause_._Employee, 0, DateTime.Now, "", SalaryClause_.ClauseType , executedate, monthcount, value, ""));
                    List<SalaryClause> SalaryClauseList_End_not_null = SalaryClauseList.Where(x => x.MonthsCount != null).ToList();
                    DateTime min_date = SalaryClauseList.Select(x => x.ExecuteDate).Min();

                    List<SalaryClause> SalaryClauseList_End_null = SalaryClauseList.Where(x => x.MonthsCount == null).ToList();
                    if (SalaryClauseList_End_not_null.Count > 0)
                    {
                        DateTime max_date = SalaryClauseList_End_not_null.Select(x => x.ExecuteDate.AddMonths(Convert.ToInt32(x.MonthsCount))).Max();

                        for (int i = min_date.Year; i <= max_date.Year; i++)
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                double UNLimited_due = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                  &&
                                  x.ExecuteDate.Year >= i && x.ExecuteDate.Month >= j).Sum(y => y.Value);
                                double UNLimited_dedu = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                  &&
                                  x.ExecuteDate.Year >= i && x.ExecuteDate.Month >= j).Sum(y => y.Value);
                                double Limited_due = SalaryClauseList_End_not_null
                                    .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                    && x.ExecuteDate.Year == i && x.ExecuteDate.Month == j).Sum(y => y.Value);
                                double Limited_dedu = SalaryClauseList_End_not_null
                                   .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   && x.ExecuteDate.Year == i && x.ExecuteDate.Month == j).Sum(y => y.Value);
                                if (UNLimited_due + Limited_due - UNLimited_dedu - Limited_dedu < 0)
                                    throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث في سنة:" + i + ",شهر:" + j);
                            }
                        }
                        double due_before_range = SalaryClauseList_End_null
                           .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                             &&
                            x.ExecuteDate.Year < min_date.Year && x.ExecuteDate.Month < min_date.Month).Sum(y => y.Value);
                        double dedu_before_range = SalaryClauseList_End_null
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   &&
                                  x.ExecuteDate.Year < min_date.Year && x.ExecuteDate.Month < min_date.Month).Sum(y => y.Value);
                        if ((due_before_range - dedu_before_range) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث قبل تاريخ:" + min_date);

                        double due_after_range = SalaryClauseList_End_null
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                   &&
                                  x.ExecuteDate.Year > max_date.Year && x.ExecuteDate.Month > max_date.Month).Sum(y => y.Value);
                        double dedu_after_range = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   &&
                                  x.ExecuteDate.Year > max_date.Year && x.ExecuteDate.Month > max_date.Month).Sum(y => y.Value);
                        if ((due_before_range - dedu_before_range) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث بعد تاريخ:" + max_date);
                    }
                    else
                    {

                        double due_all = SalaryClauseList
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_DUE).Sum(y => y.Value);
                        double dedu_all = SalaryClauseList
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction).Sum(y => y.Value);
                        if ((due_all - dedu_all) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر:");
                    }

                    DB.ExecuteSQLCommand("update  "
                       + SalaryClauseTable.TableName
                       + " set "
                       + SalaryClauseTable.SalaryClauseDesc   + "='" + Desc_  + "'"
                       + ","
                        + SalaryClauseTable.ExecuteDate   + "='" + executedate.ToString("yyyy-MM-dd") + "'"
                        + ","
                       + SalaryClauseTable.MonthCount   + "=" + (monthcount == null ? "null" : monthcount.ToString())
                        + ","
                        + SalaryClauseTable.Value  + "=" + value 
                        + ","
                       + SalaryClauseTable.Notes  + "='" + notes  + "'"
                       + " where "
                       + SalaryClauseTable.SalaryClauseID  + "=" + clauseid
                    );
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.UPDATE 
                     , DatabaseInterface.Log.Log_Target.Employee_SalaryClause
                     , ""
                   , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.UPDATE
                     , DatabaseInterface.Log.Log_Target.Employee_SalaryClause
                     , ""
                   , false ,ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_SalaryClause" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            public bool Delete_SalaryClause(uint clauseid)
            {
                int k = 0;
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DialogResult dd = MessageBox.Show(" حذف بند الراتب سيؤدي الى خلل في ارشيف الرواتب في حال تم استخدامه سابقا في صرف راتب","",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning );
                    if (dd != DialogResult.OK) return false ;
                    SalaryClause SalaryClause_ = Get_SalaryClause_Info_BYID(clauseid);
                    k = 1;
                    List<SalaryClause> SalaryClauseList = Get_SalaryClause_List(SalaryClause_._Employee);
                    k = 11;
                    SalaryClauseList.RemoveAll(x => x.SalaryClauseID == clauseid);
                    k = 12;
                    List<SalaryClause> SalaryClauseList_End_not_null = SalaryClauseList.Where(x => x.MonthsCount != null).ToList();
                    DateTime min_date = SalaryClauseList.Select(x => x.ExecuteDate).Min();

                    List<SalaryClause> SalaryClauseList_End_null = SalaryClauseList.Where(x => x.MonthsCount == null).ToList();
                    if (SalaryClauseList_End_not_null.Count > 0)
                    {
                        DateTime max_date = SalaryClauseList_End_not_null.Select(x => x.ExecuteDate.AddMonths(Convert.ToInt32(x.MonthsCount))).Max();

                        for (int i = min_date.Year; i <= max_date.Year; i++)
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                double UNLimited_due = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                  &&
                                  x.ExecuteDate.Year >= i && x.ExecuteDate.Month >= j).Sum(y => y.Value);
                                double UNLimited_dedu = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                  &&
                                  x.ExecuteDate.Year >= i && x.ExecuteDate.Month >= j).Sum(y => y.Value);
                                double Limited_due = SalaryClauseList_End_not_null
                                    .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                    && x.ExecuteDate.Year == i && x.ExecuteDate.Month == j).Sum(y => y.Value);
                                double Limited_dedu = SalaryClauseList_End_not_null
                                   .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   && x.ExecuteDate.Year == i && x.ExecuteDate.Month == j).Sum(y => y.Value);
                                if (UNLimited_due + Limited_due - UNLimited_dedu - Limited_dedu < 0)
                                    throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث في سنة:" + i + ",شهر:" + j);
                            }
                        }
                        double due_before_range = SalaryClauseList_End_null
                           .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                             &&
                            x.ExecuteDate.Year < min_date.Year && x.ExecuteDate.Month < min_date.Month).Sum(y => y.Value);
                        double dedu_before_range = SalaryClauseList_End_null
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   &&
                                  x.ExecuteDate.Year < min_date.Year && x.ExecuteDate.Month < min_date.Month).Sum(y => y.Value);
                        if ((due_before_range - dedu_before_range) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث قبل تاريخ:" + min_date);

                        double due_after_range = SalaryClauseList_End_null
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_DUE
                                   &&
                                  x.ExecuteDate.Year > max_date.Year && x.ExecuteDate.Month > max_date.Month).Sum(y => y.Value);
                        double dedu_after_range = SalaryClauseList_End_null
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction
                                   &&
                                  x.ExecuteDate.Year > max_date.Year && x.ExecuteDate.Month > max_date.Month).Sum(y => y.Value);
                        if ((due_before_range - dedu_before_range) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر , التعارض حدث بعد تاريخ:" + max_date);
                    }
                    else
                    {

                        double due_all = SalaryClauseList
                                 .Where(x => x.ClauseType == SalaryClause.TYPE_DUE).Sum(y => y.Value);
                        double dedu_all = SalaryClauseList
                                  .Where(x => x.ClauseType == SalaryClause.TYPE_Deduction).Sum(y => y.Value);
                        if ((due_all - dedu_all) < 0)
                            throw new Exception("لا يمكن ان يكون الراتب اقل من الصفر:");
                    }

                    DB.ExecuteSQLCommand("delete from   "
                    + SalaryClauseTable.TableName
                    + " where "
                    + SalaryClauseTable.SalaryClauseID  + "=" + clauseid
                    );
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.DELETE 
                    , DatabaseInterface.Log.Log_Target.Employee_SalaryClause
                    , ""
                  , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                    DatabaseInterface.Log.LogType.DELETE
                    , DatabaseInterface.Log.Log_Target.Employee_SalaryClause
                    , ""
                  , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Delete_SalaryClause:"+k + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
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
                    System.Windows.Forms.MessageBox.Show("Get_SalaryClause_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_SalarysPayOrder_Info" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            public SalarysPayOrder Get_SalarysPayOrder_Info_By_RowID(uint rowid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + SalarysPayOrderTable.SalarysPayOrderID+","
                        + SalarysPayOrderTable.OrderDate + ","
                        + SalarysPayOrderTable.ExecuteYear + ","
                        + SalarysPayOrderTable.ExecuteMonth + ","
                        + SalarysPayOrderTable.Notes
                        + " from   "
                        + SalarysPayOrderTable.TableName
                        + " where "
                        + DatabaseInterface.ROWID_COLUMN + "=" + rowid 
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint SalarysPayOrderID = Convert.ToUInt32(t.Rows[0][SalarysPayOrderTable.SalarysPayOrderID].ToString());
                        DateTime orderdate = Convert.ToDateTime(t.Rows[0][SalarysPayOrderTable.OrderDate ].ToString());
                        int executeyear = Convert.ToInt32(t.Rows[0][SalarysPayOrderTable.ExecuteYear].ToString());
                        int executemonth = Convert.ToInt32(t.Rows[0][SalarysPayOrderTable.ExecuteMonth].ToString());
                        string notes = t.Rows[0][SalarysPayOrderTable.Notes ].ToString();
                        return new SalarysPayOrder(SalarysPayOrderID, orderdate, executeyear, executemonth, notes);

                    }
                    else
                        return null;
                }

                catch (Exception ee)
                {
                    MessageBox.Show("Get_SalarysPayOrder_Info" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_SalarysPayOrder_Info" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            public SalarysPayOrder Add_SalarysPayOrder(int exe_year, int exe_month, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t= DB.GetData(" insert into "
                    + SalarysPayOrderTable.TableName
                    + "("
                    + SalarysPayOrderTable.ExecuteYear + ","
                    + SalarysPayOrderTable.ExecuteMonth  + ","
                     + SalarysPayOrderTable.OrderDate  + ","
                    + SalarysPayOrderTable.Notes 
                    + ")"
                    + "values"
                    + "("
                    + exe_year 
                    + ","
                    + exe_month
                    +","
                     + "'" + DateTime .Now .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                     + "'" + notes  + "'"
                    + ")"
                    + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());

                    DB.AddLog(
                    DatabaseInterface.Log.LogType.INSERT 
                    , DatabaseInterface.Log.Log_Target.Salary_PayOrder 
                    , ""
                  , true, "");
                    return Get_SalarysPayOrder_Info_By_RowID(rowid);
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Salary_PayOrder
                   , ""
                 , false ,ee.Message );
                    throw new Exception ("Add_SalarysPayOrder" + ee.Message);

                }
            }
            public bool Update_SalarysPayOrder(uint id,int exe_year, int exe_month, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    //ShowSqlQuery dd = new ShowSqlQuery("update  "
                    //   + SalarysPayOrderTable.TableName
                    //   + " set "

                    //   + SalarysPayOrderTable.ExecuteMonth + "=" + exe_month
                    //   + ","
                    //   + SalarysPayOrderTable.ExecuteMonth + "=" + exe_year
                    //    + ","
                    //   + SalarysPayOrderTable.Notes + "='" + notes + "'"

                    //   + " where "
                    //   + SalarysPayOrderTable.SalarysPayOrderID + "=" + id
                    //);
                    //dd.ShowDialog();
                    DB.ExecuteSQLCommand("update  "
                       + SalarysPayOrderTable.TableName
                       + " set "

                       + SalarysPayOrderTable.ExecuteMonth + "=" + exe_month 
                       + ","
                       + SalarysPayOrderTable.ExecuteYear  + "=" + exe_year
                        + ","
                       + SalarysPayOrderTable.Notes + "='" + notes  + "'"
                       
                       + " where "
                       + SalarysPayOrderTable.SalarysPayOrderID  + "=" + id
                    );
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Salary_PayOrder
                   , ""
                 , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                  DatabaseInterface.Log.LogType.UPDATE
                  , DatabaseInterface.Log.Log_Target.Salary_PayOrder
                  , ""
                , false ,ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_SalarysPayOrder:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Delete_SalarysPayOrder(uint id)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    SalarysPayOrder SalarysPayOrder_ = Get_SalarysPayOrder_Info_ByID(id);
                    if (SalarysPayOrder_ == null) throw new Exception("SalarysPayOrder_ NULL");
                    if (new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where(x => x._SalarysPayOrder != null
                   && x._SalarysPayOrder.SalarysPayOrderID == SalarysPayOrder_.SalarysPayOrderID).Count() > 0)
                        throw new Exception("يجب أولا الغاء جميع الرواتب المصروفة ضمن امر صرف الرواتب هذا");
                    DB.ExecuteSQLCommand("delete from   "
                    + SalarysPayOrderTable.TableName
                    + " where "
                    + SalarysPayOrderTable.SalarysPayOrderID    + "=" + id
                    );
                    DB.AddLog(
                  DatabaseInterface.Log.LogType.DELETE 
                  , DatabaseInterface.Log.Log_Target.Salary_PayOrder
                  , ""
                , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                 DatabaseInterface.Log.LogType.DELETE
                 , DatabaseInterface.Log.Log_Target.Salary_PayOrder
                 , ""
               , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Delete_SalarysPayOrder" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
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
                    System.Windows.Forms.MessageBox.Show("Get_SalarysPayOrder_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public List<SalarysPayOrderMonthReport> Get_GetSalarysPayOrderMonthReport_List_In_Year(int year)
            {
                List<SalarysPayOrderMonthReport> list = new List<SalarysPayOrderMonthReport>();
                try
                {
                    List<SalarysPayOrder> SalarysPayOrderList = Get_SalarysPayOrder_List().Where(x => x.ExecuteYear == year).ToList();
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where(x => x._SalarysPayOrder != null && x._SalarysPayOrder.ExecuteYear == year).ToList();

                    //for (int i = 0; i < SalarysPayOrderList.Count; i++)
                    //{
                        for(int j=1;j<=12; j++)
                        {
                            int monthno = j;
                            System.Globalization.CultureInfo AR_English = new System.Globalization.CultureInfo("ar-SY");
                            System.Globalization.DateTimeFormatInfo englishInfo = AR_English.DateTimeFormat;
                            string monthname = englishInfo.MonthNames[j - 1];
                        List<SalarysPayOrder> SalarysPayOrderList_Month = SalarysPayOrderList.Where(x => x.ExecuteMonth == j).ToList();
                        string payorderid, orderdate;
                        if (SalarysPayOrderList_Month.Count >0)
                        {
                             payorderid = SalarysPayOrderList_Month[0].SalarysPayOrderID.ToString();
                             orderdate = SalarysPayOrderList_Month[0].OrderDate.ToShortDateString();
                        }
                          else
                        {
                             payorderid ="-";
                             orderdate = "-";
                        }  


                            List<EmployeePayOrder> Month_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._SalarysPayOrder.ExecuteMonth == j).ToList();
                        string employeescount;
                        if (Month_EmployeePayOrderList.Count > 0)
                            employeescount = Month_EmployeePayOrderList.Count.ToString();
                        else
                            employeescount = "-";
                            List<Money_Currency> Month_EmployeePayOrderList_moneycurrency = Money_Currency.Get_Money_Currency_List_From_EmployeePayOrder(Month_EmployeePayOrderList);
                            string moneyamount= Money_Currency.ConvertMoney_CurrencyList_TOString(Month_EmployeePayOrderList_moneycurrency);

                            list.Add(new SalarysPayOrderMonthReport(year, monthno, monthname, payorderid, orderdate, employeescount, moneyamount));

                        }
                    //}

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_GetSalarysPayOrderMonthReport_List_In_Year" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;

            }
            public List<SalarysPayOrderEmployeeReport> Get_GetSalarysPayOrderEmployees_Report_List(SalarysPayOrder SalarysPayOrder_)
            {
                List<SalarysPayOrderEmployeeReport> list = new List<SalarysPayOrderEmployeeReport>();
                try
                {

                    List<EmployeesReport> EmployeesReportList = new CompanyReportSQL(DB).GetEmployeesReportList();
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where (x=>x._SalarysPayOrder !=null ).ToList ();

                    for (int i = 0; i < EmployeesReportList.Count; i++)
                    {
             
                        uint employeeid = EmployeesReportList[i].EmployeeID ;
                        string employeename = EmployeesReportList[i].EmployeeName ;
                        string jobsate = EmployeesReportList[i].JobState ;
                        string employeementstate = EmployeesReportList[i].EmployeeMentState;
                        uint employeestatecode = EmployeesReportList[i].EmployeeStateCode;
                      
                        string expectedsalary = Get_Expected_Salary_ForEmployee_IN_Month(EmployeesReportList[i].EmployeeID, SalarysPayOrder_.ExecuteYear, SalarysPayOrder_.ExecuteMonth).ToString ();
                        uint? payorderid;
                        double? salarypayordervalue;
                        Currency salarypayorderCurency;
                        double? salarypayorderExchangerate;
                        string Paid_;
             double? Remain_;
             double? PaysRealValue_;
                        List < EmployeePayOrder> employeeid_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._Employee.EmployeeID == EmployeesReportList[i].EmployeeID
                        &&x._SalarysPayOrder .SalarysPayOrderID== SalarysPayOrder_.SalarysPayOrderID).ToList();
                        if(employeeid_EmployeePayOrderList.Count >0)
                        {
                            payorderid = employeeid_EmployeePayOrderList[0].PayOrderID;
                            salarypayordervalue = employeeid_EmployeePayOrderList[0].Value ;
                            salarypayorderCurency = employeeid_EmployeePayOrderList[0]._Currency ;
                            salarypayorderExchangerate = employeeid_EmployeePayOrderList[0].ExchangeRate;
                            List<PayOUT> payoutlist = new PayOUTSQL(DB).GetPaysOUT_List(
                                new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, employeeid_EmployeePayOrderList[0].PayOrderID));
                            Paid_ = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(payoutlist));
                            Remain_ = salarypayordervalue - new Trade.TradeSQL.OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, employeeid_EmployeePayOrderList[0].PayOrderID));
                            PaysRealValue_ = System.Math.Round(payoutlist.Sum(x => x.Value / x.ExchangeRate), 2);
                        }
                        else 
                        {
                            payorderid = null;
                            salarypayordervalue = null ;
                            salarypayorderCurency = null ;
                            salarypayorderExchangerate = null ;
                            Paid_ ="-";
                            Remain_ = null;
                            PaysRealValue_ = null;
                        }


                        list.Add(new SalarysPayOrderEmployeeReport
                            (employeeid , employeename, jobsate , employeementstate,employeestatecode
                            , expectedsalary,payorderid , salarypayordervalue, salarypayorderCurency,salarypayorderExchangerate,Paid_ ,Remain_ ,PaysRealValue_ ));
                    }

                   
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_GetSalarysPayOrderEmployees_Report_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    MessageBox.Show("Get_Expected_Salary_ForEmployee_IN_Month:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
                    return -1;
                }
            }
            #region SalarysPayOrder_Employee_Report_Currency
           
            public List<SalarysPayOrderReport_Currency> Get_GetSalarysPayOrderEmployees_Currency_Report_List(uint salaryspayorderid)
            {
                List<SalarysPayOrderReport_Currency> list = new List<SalarysPayOrderReport_Currency>();
                try
                {
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where(x => x._SalarysPayOrder != null
                    && x._SalarysPayOrder.SalarysPayOrderID == salaryspayorderid).ToList();

                    List<uint> CurrencyID_List = EmployeePayOrderList.Select(x => x._Currency.CurrencyID).Distinct().ToList();
                    for (int i = 0; i < CurrencyID_List.Count; i++)
                    {
                        List<EmployeePayOrder> Currency_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._Currency.CurrencyID == CurrencyID_List[i]).ToList();
                        Currency tempcurrency = Currency_EmployeePayOrderList[0]._Currency ;
                        uint currencyid = CurrencyID_List[i];
                        string currencyname = tempcurrency.CurrencyName;
                        string currencysymbol= tempcurrency.CurrencySymbol;
                        //List<Money_Currency> Money_CurrencyLis = Money_Currency.Get_Money_Currency_List_From_EmployeePayOrder(Currency_EmployeePayOrderList);
                        double salarysvalue = Currency_EmployeePayOrderList.Sum (x=>x.Value );
                        List<PayOUT> PayOUTList = new List<PayOUT>();
                        for(int j=0;j< Currency_EmployeePayOrderList.Count;j++)
                        {
                            Trade.Objects.Operation Operation_ = new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, Currency_EmployeePayOrderList[j].PayOrderID);
                            PayOUTList.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(Operation_));

                        }
                        List<Money_Currency> Payout_Money_CurrencyList = Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList);
                        string paysvalues = Money_Currency.ConvertMoney_CurrencyList_TOString(Payout_Money_CurrencyList); 
                        double realsalarysvalue = Math .Round (Currency_EmployeePayOrderList.Sum (x=>x.Value /x.ExchangeRate ),3);
                        double realpaysvalues = Math.Round(Payout_Money_CurrencyList.Sum(x => x.Value / x.ExchangeRate), 3);


                        list.Add(new SalarysPayOrderReport_Currency
                            (currencyid , currencyname, currencysymbol, salarysvalue , paysvalues , realsalarysvalue
                            , realpaysvalues));
                    }

              
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_GetSalarysPayOrderEmployees_Currency_Report_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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



                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where(x => x._SalarysPayOrder != null
                && x._SalarysPayOrder.ExecuteYear == year).ToList();

                    List<uint> CurrencyID_List = EmployeePayOrderList.Select(x => x._Currency.CurrencyID).Distinct().ToList();
                    for (int i = 0; i < CurrencyID_List.Count; i++)
                    {
                        List<EmployeePayOrder> Currency_EmployeePayOrderList = EmployeePayOrderList.Where(x => x._Currency.CurrencyID == CurrencyID_List[i]).ToList();
                        Currency tempcurrency = Currency_EmployeePayOrderList[0]._Currency;
                        uint currencyid = CurrencyID_List[i];
                        string currencyname = tempcurrency.CurrencyName;
                        string currencysymbol = tempcurrency.CurrencySymbol ;
                        //List<Money_Currency> Money_CurrencyLis = Money_Currency.Get_Money_Currency_List_From_EmployeePayOrder(Currency_EmployeePayOrderList);
                        double salarysvalue = Currency_EmployeePayOrderList.Sum(x => x.Value);
                        List<PayOUT> PayOUTList = new List<PayOUT>();
                        for (int j = 0; j < Currency_EmployeePayOrderList.Count; j++)
                        {
                            Trade.Objects.Operation Operation_ = new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, Currency_EmployeePayOrderList[j].PayOrderID);
                            PayOUTList.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(Operation_));

                        }
                        List<Money_Currency> Payout_Money_CurrencyList = Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList);
                        string paysvalues = Money_Currency.ConvertMoney_CurrencyList_TOString(Payout_Money_CurrencyList);
                        double realsalarysvalue = Math.Round(Currency_EmployeePayOrderList.Sum(x => x.Value / x.ExchangeRate), 3);
                        double realpaysvalues = Math.Round(Payout_Money_CurrencyList.Sum(x => x.Value / x.ExchangeRate), 3);


                        list.Add(new SalarysPayOrderReport_Currency
                            (currencyid, currencyname, currencysymbol, salarysvalue, paysvalues, realsalarysvalue
                            , realpaysvalues));
                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_GetSalarysPayOrder_Year_Report_Currency_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
                return list;
            }
            #endregion
            public List<PayOUT > Get_GetSalarysPayOrder_Year_PaysOut_List(int year)
            {
                List<PayOUT > list = new List<PayOUT>();
                try
                {



                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where(x => x._SalarysPayOrder != null
                && x._SalarysPayOrder.ExecuteYear == year).ToList();
                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        Trade.Objects.Operation operation = new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder,
                            EmployeePayOrderList[i].PayOrderID);

                      

                        list.AddRange(new PayOUTSQL (DB).GetPaysOUT_List (operation ));
                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_GetSalarysPayOrder_Year_PaysOut_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
                return list;
            }
            public List<PayOUT> Get_GetSalarysPayOrder_Year_Month_PaysOut_List(int year,int month)
            {
                List<PayOUT> list = new List<PayOUT>();
                try
                {



                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where(x => x._SalarysPayOrder != null
                && x._SalarysPayOrder.ExecuteYear == year && x._SalarysPayOrder.ExecuteMonth == month ).ToList();

                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        Trade.Objects.Operation operation = new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder,
                            EmployeePayOrderList[i].PayOrderID);



                        list.AddRange(new PayOUTSQL(DB).GetPaysOUT_List(operation));
                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_GetSalarysPayOrder_Year_PaysOut_List" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
                return list;
            }


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
                    System.Windows.Forms.MessageBox.Show("GetPayOrder_INFO_BYID:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("GetPayOrder_INFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    System.Windows.Forms.MessageBox.Show("GetEmployeeSalaryPayOrder_By_Month" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public bool Add_PayOrder(DateTime PayOrderdate, uint EmployeeID,  string description, Currency currency, double exchangerate, double  value)
            {
                try
                {
             
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" insert into "
                    + PayOrderTable.TableName
                    + "("
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
                    + ")"
                    + "values"
                    + "("
                    + "'" + PayOrderdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + "'" + description + "'"
                    + ","
                    + EmployeeID  
                    + ","
                    + exchangerate
                    + ","
                    + currency.CurrencyID
                    + ","
                    + value 
                    + ")"
                    );
                    DB.AddLog(
                 DatabaseInterface.Log.LogType.INSERT 
                 , DatabaseInterface.Log.Log_Target.Employee_PayOrder
                 , ""
               , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
               DatabaseInterface.Log.LogType.INSERT
               , DatabaseInterface.Log.Log_Target.Employee_PayOrder
               , ""
             , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add_PayOrder" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Add__Salary_PayOrder(DateTime PayOrderdate, uint EmployeeID, uint SalaryPayOrderID, Currency currency, double exchangerate, double value)
            {
                try
                {
               
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( " insert into "
                    + PayOrderTable.TableName
                    + "("
                    + PayOrderTable.PayOrderDate
                    + ","
                    + PayOrderTable.SalarysPayOrderID
                    + ","
                    + PayOrderTable.EmployeeID
                    + ","
                    + PayOrderTable.ExchangeRate
                    + ","
                    + PayOrderTable.CurrencyID
                    + ","
                    + PayOrderTable.PayOrderValue
                    + ")"
                    + "values"
                    + "("
                    + "'" + PayOrderdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + SalaryPayOrderID
                    + ","
                    + EmployeeID
                    + ","
                    + exchangerate
                    + ","
                    + currency.CurrencyID
                    + ","
                    + value
                    + ")"
                    );
                    DB.AddLog(
                           DatabaseInterface.Log.LogType.INSERT
                           , DatabaseInterface.Log.Log_Target.Employee_PayOrder
                           , ""
                         , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                                    DB.AddLog(
                               DatabaseInterface.Log.LogType.INSERT
                               , DatabaseInterface.Log.Log_Target.Employee_PayOrder
                               , ""
                             , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Add__Salary_PayOrder" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_PayOrder(uint PayOrderid, DateTime PayOrderdate , uint  EmployeeID,  string description, Currency currency, double exchangerate,  double  value)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                    + PayOrderTable.TableName
                    + " set "
                    + PayOrderTable.PayOrderDate + "=" + "'" + PayOrderdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + PayOrderTable.PayOrderDesc + "='" + description + "'"
                    + ","
                    + PayOrderTable.EmployeeID    + "=" + EmployeeID    
                    + ","
                    + PayOrderTable.ExchangeRate + "=" + exchangerate
                    + ","
                    + PayOrderTable.PayOrderValue   + "="+value
                    + " where "
                    + PayOrderTable.PayOrderID + "=" + PayOrderid
                    );
                    DB.AddLog(
                              DatabaseInterface.Log.LogType.UPDATE 
                              , DatabaseInterface.Log.Log_Target.Employee_PayOrder 
                              , ""
                            , true ,"");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                              DatabaseInterface.Log.LogType.UPDATE 
                              , DatabaseInterface.Log.Log_Target.Employee_PayOrder
                              , ""
                            , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("Update_PayOrder" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Update_Salary_PayOrder(uint PayOrderid, DateTime PayOrderdate, uint EmployeeID , Currency currency, double exchangerate, double value)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand( "update  "
                    + PayOrderTable.TableName
                    + " set "
                    + PayOrderTable.PayOrderDate + "=" + "'" + PayOrderdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + PayOrderTable.EmployeeID + "=" + EmployeeID
                    + ","
                    + PayOrderTable.ExchangeRate + "=" + exchangerate
                    + ","
                    + PayOrderTable.PayOrderValue + "=" + value
                    + " where "
                    + PayOrderTable.PayOrderID + "=" + PayOrderid
                    );
                    DB.AddLog(
                             DatabaseInterface.Log.LogType.UPDATE
                             , DatabaseInterface.Log.Log_Target.Employee_PayOrder 
                             , ""
                           , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                             DatabaseInterface.Log.LogType.UPDATE
                             , DatabaseInterface.Log.Log_Target.Employee_PayOrder 
                             , ""
                           , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Update_Salary_PayOrder" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Delete_PayOrder(uint PayOrderid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    EmployeePayOrder EmployeePayOrder_ = GetPayOrder_INFO_BYID(PayOrderid);
                    if (EmployeePayOrder_ == null) throw new Exception("EmployeePayOrder_ Null");
                    if (new PayOUTSQL(DB).GetPaysOUT_List(new Trade.Objects.Operation(
                        Trade .Objects .Operation .Employee_PayOrder , EmployeePayOrder_.PayOrderID )).Count >0)
                        throw new Exception("يجب اولا حذف الدفعات التابعة لأمر الصرف هذا");
                    DB.ExecuteSQLCommand("delete from   "
                    + PayOrderTable.TableName
                    + " where "
                    + PayOrderTable.PayOrderID + "=" + PayOrderid
                    );
                    DB.AddLog(
                             DatabaseInterface.Log.LogType.DELETE 
                             , DatabaseInterface.Log.Log_Target.Employee_PayOrder
                             , ""
                           , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE
                            , DatabaseInterface.Log.Log_Target.Employee_PayOrder
                            , ""
                          , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("Delete_PayOrder" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<EmployeePayOrder> Get_PayOrders_List()
            {
                List<EmployeePayOrder> PayOrderList = new List<EmployeePayOrder>();

                try
                {
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
                        uint PayOrderID = Convert.ToUInt32(t.Rows[i][PayOrderTable.PayOrderID].ToString());
                        DateTime PayOrderDate = Convert.ToDateTime(t.Rows[i][PayOrderTable.PayOrderDate ].ToString());
                        string description = t.Rows[i][PayOrderTable.PayOrderDesc ].ToString();
                        Employee Employee_ = new EmployeeSQL(DB).GetEmployeeInforBYID (Convert.ToUInt32(t.Rows[i][PayOrderTable.EmployeeID ].ToString()));
                        double exchangerate = Convert.ToDouble(t.Rows[i][PayOrderTable.ExchangeRate ].ToString());
                        Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[i][PayOrderTable.CurrencyID ].ToString()));
                        double Value = Convert.ToDouble(t.Rows[i][PayOrderTable.PayOrderValue ].ToString());
                        SalarysPayOrder SalarysPayOrder_;
                            try
                        {
                            SalarysPayOrder_=new SalarysPayOrderSQL(DB).Get_SalarysPayOrder_Info_ByID(Convert.ToUInt32(t.Rows[i][PayOrderTable.SalarysPayOrderID ].ToString()));
                        }
                        catch
                        {
                            SalarysPayOrder_ = null;
                        }
  
                        PayOrderList.Add(new EmployeePayOrder(PayOrderID, PayOrderDate, SalarysPayOrder_, description, Employee_  , currency, exchangerate, Value ));
                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_PayOrders_List:" + ee.Message, "" , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        
                }
                return PayOrderList;

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
                    System.Windows.Forms.MessageBox.Show("GetPayOrders_List_For_Employee" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            internal double GetPayOrderValue(uint payorderid)
            {
                try
                {

                    return new Trade.TradeSQL.OperationSQL(DB).Get_OperationValue(Trade.Objects.Operation.Employee_PayOrder , payorderid );
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetPayOrderValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return -1;
                }
            }
            internal double GetPayOrder_PaysValue(uint payorderid)
            {
                try
                {
                    return new Trade.TradeSQL.OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency (Trade.Objects.Operation.Employee_PayOrder, payorderid);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetPayOrder_PaysValue:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List();

                   
                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        uint payinid = EmployeePayOrderList[i].PayOrderID;
                        DateTime payindate = EmployeePayOrderList[i].PayOrderDate ;
                        string description  ;
                        uint EmployeeID_ = EmployeePayOrderList[i]._Employee.EmployeeID;
                        string employeename = EmployeePayOrderList[i]._Employee.EmployeeName;
                        double Value = EmployeePayOrderList[i].Value ;

                        Currency Currency_ = EmployeePayOrderList[i]._Currency ;
                        double exchangerate = EmployeePayOrderList[i].ExchangeRate;
                        bool type;
                        if (EmployeePayOrderList[i]._SalarysPayOrder == null)
                        {
                            description = EmployeePayOrderList[i].PayOrderDesc;
                            type = PayOrderReport.TYPE_PAY_ODER;
                        }
                        else
                        { type = PayOrderReport.TYPE_SALARY_PAY_ODER;
                            description="صرف راتب  شهر :"+EmployeePayOrderList[i]._SalarysPayOrder.ExecuteMonth +" سنة :"
                                +EmployeePayOrderList[i]._SalarysPayOrder.ExecuteYear;
                                }

                        List<PayOUT> PayOUTList = new PayOUTSQL(DB).GetPaysOUT_List(new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                        string pays_amount = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
                        PayOrderList.Add(new PayOrderReport(type, payinid, payindate, description, EmployeeID_, employeename,
                            Currency_, exchangerate, Value, pays_amount));

                    }
                   
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_Company_PayOrdersReportList" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return PayOrderList;
            }
            public List<PayOrderReport> Get_Employee_PayOrdersReportList(uint EmployeeID)
            {
                List<PayOrderReport> PayOrderList = new List<PayOrderReport>();
                try
                {
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List().Where (x=>x._Employee .EmployeeID ==EmployeeID ).ToList ();


                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        uint payinid = EmployeePayOrderList[i].PayOrderID;
                        DateTime payindate = EmployeePayOrderList[i].PayOrderDate;
                        string description ;
                        uint EmployeeID_ = EmployeePayOrderList[i]._Employee.EmployeeID;
                        string employeename = EmployeePayOrderList[i]._Employee.EmployeeName;
                        double Value = EmployeePayOrderList[i].Value;

                        Currency Currency_ = EmployeePayOrderList[i]._Currency;
                        double exchangerate = EmployeePayOrderList[i].ExchangeRate;
                        bool type;
                        if (EmployeePayOrderList[i]._SalarysPayOrder == null)
                        {
                            type = PayOrderReport.TYPE_PAY_ODER;
                            description =EmployeePayOrderList[i].PayOrderDesc;
                        }
                        else
                        {
                            type = PayOrderReport.TYPE_SALARY_PAY_ODER;
                            description = "صرف راتب شهر :" + EmployeePayOrderList[i]._SalarysPayOrder.ExecuteMonth + "  سنة:" + EmployeePayOrderList[i]._SalarysPayOrder.ExecuteYear;
                        }

                        List<PayOUT> PayOUTList = new PayOUTSQL(DB).GetPaysOUT_List(new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                        string pays_amount = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
                        PayOrderList.Add(new PayOrderReport(type, payinid, payindate, description, EmployeeID_, employeename,
                            Currency_, exchangerate, Value, pays_amount));

                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_Employee_PayOrdersReportList" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return PayOrderList;
            }
           public AllPayOrdersReport Get_AllPayOrdersReport()
            {
                try
                {
                    List<EmployeePayOrder> EmployeePayOrderList = new EmployeePayOrderSQL(DB).Get_PayOrders_List();
                    List<Money_Currency> Payorders_Value_MoneyCurrency = new List<Money_Currency>();
                    List<Money_Currency> Payorders_PaysAmount_MoneyCurrency = new List<Money_Currency>();
                    List<Money_Currency> Payorders_PaysRemain_MoneyCurrency = new List<Money_Currency>();

                    for (int i = 0; i < EmployeePayOrderList.Count; i++)
                    {
                        Payorders_Value_MoneyCurrency.Add(new Money_Currency(EmployeePayOrderList[i]._Currency, EmployeePayOrderList[i].Value, EmployeePayOrderList[i].ExchangeRate));
                        List<PayOUT> PayOUTList = new PayOUTSQL(DB).GetPaysOUT_List(new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
                        Payorders_PaysAmount_MoneyCurrency.AddRange(Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));
                        double pays_remain = EmployeePayOrderList[i].Value- new Trade.TradeSQL.OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(new Trade.Objects.Operation(Trade.Objects.Operation.Employee_PayOrder, EmployeePayOrderList[i].PayOrderID));
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
                    System.Windows.Forms.MessageBox.Show("Get_AllPayOrdersReport" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
        }
    }
}
