using OverLoad_Server_Kernal.AccountingSQL;
using OverLoad_Server_Kernal.CompanySQL;
using OverLoad_Server_Kernal.Objects;
using System;
using System.Collections.Generic;

using System.Data;

using System.Data.SQLite;


namespace OverLoad_Server_Kernal
{
    public class DatabaseInterface
    {
        //private User _User;
        //internal User __User
        //{
        //    get { return _User; }
        //}
        #region Tables_trail
        private const string MaintenanceOPRTable = "Trade_BillMaintenance_MaintenanceOPR";
        private const string BillBuyTable = "Trade_BillBuy";
        private const string BillSELLTable = "Trade_BillSell";
        private const string AssemblageTable = "Trade_Assemblage";
        private const string DisAssemblageTable = "Trade_DisAssemblage";
        private const string ItemTable = "Item_Item";
        private const string ItemINTable = "Trade_ItemIN";
        private const string ItemOUTTable = "Trade_ItemOUT";
        private const string PayOrderTable = "Company_Employee_PayOrder";
        private const string EmployeeTable = "Company_Employee";
        private const string ExchangeOPRTable = "Account_ExchangeOpr";
        private const string PayINTable = "Account_PayIN";
        private const string PayOUTTable = "Account_PayOUT";
        private const string MoneyTransFormOPRTable = "Account_MoneyTransFormOPR";

        #endregion
        //private bool DataBaseLocked;
        //public class SQL_CMD
        //{
        //    public const int CMD_TYPE_SELECT = 1;
        //    public const int CMD_TYPE_INSERT = 2;
        //    public const int CMD_TYPE_UPDATE = 3;
        //    public const int CMD_TYPE_DELETE = 4;

        //    public uint CMD_Type;
        //    public string TableName;
        //    public SQL_CMD(uint CMD_Type_, string TableName_)
        //    {
        //        CMD_Type = CMD_Type_;
        //        TableName = TableName_;
        //    }
        //}


        private SQLiteConnection DATABASE_CONNECTION;
        //private SQLiteCommand DATABASE_SQL_COMMAND;
        private DateTime App_Start_Date = new DateTime(2020, 3, 1);

        public Part COMPANY;
        public DatabaseInterface(string db,string ServerName)
        {
            string s = "Data Source=" + db + ";Password=password;";
            //string s = "Data Source=" + db + ";";

            DATABASE_CONNECTION = new SQLiteConnection(s);
            //DATABASE_SQL_COMMAND = new SQLiteCommand("", DATABASE_CONNECTION);
            COMPANY = new Part(0, ServerName, App_Start_Date, null);
        }
        public void Open_Connection()
        {
            this.DATABASE_CONNECTION.Open();
        }
        public void Close_Connection()
        {
            this.DATABASE_CONNECTION.Close();
        }
        public System.Data.ConnectionState Get_Connection_State()
        {
            return this.DATABASE_CONNECTION.State;
        }
        public SQLiteCommand Build_SQLiteCommand()
        {
            return new System.Data.SQLite.SQLiteCommand("", this.DATABASE_CONNECTION);
        }
        //public SQL_CMD Get_SQL_CMD_Info(string SQLCommand)
        //{

        //    try
        //    {
        //        string[] words = SQLCommand.Split(' ');

        //        for (int i = 0; i < words.Length; i++)
        //        {
        //            if (words[i].ToLower().Equals("insert"))
        //            {
        //                for (int j = i + 1; j < words.Length; j++)
        //                {
        //                    if (words[j].ToLower().Equals("into"))
        //                    {
        //                        for (int k = j + 1; k < words.Length; k++)
        //                        {
        //                            if (words[k].Replace(" ", string.Empty).Length != 0)
        //                            {
        //                                int ind = words[k].IndexOf("(");
        //                                if (ind == -1)
        //                                    return new SQL_CMD(SQL_CMD.CMD_TYPE_INSERT, words[k]);

        //                                else
        //                                    return new SQL_CMD(SQL_CMD.CMD_TYPE_INSERT, words[k].Substring(0, ind));
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //            else if (words[i].ToLower().Equals("select"))
        //            {
        //                for (int j = i + 1; j < words.Length; j++)
        //                {
        //                    if (words[j].ToLower().Equals("from"))
        //                    {
        //                        for (int k = j + 1; k < words.Length; k++)
        //                        {
        //                            if (words[k].Replace(" ", string.Empty).Length != 0)
        //                            {
        //                                return new SQL_CMD(SQL_CMD.CMD_TYPE_SELECT, words[k]);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else if (words[i].ToLower().Equals("delete"))
        //            {
        //                for (int j = i + 1; j < words.Length; j++)
        //                {
        //                    if (words[j].ToLower().Equals("from"))
        //                    {
        //                        for (int k = j + 1; k < words.Length; k++)
        //                        {
        //                            if (words[k].Replace(" ", string.Empty).Length != 0)
        //                            {
        //                                return new SQL_CMD(SQL_CMD.CMD_TYPE_DELETE, words[k]);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else if (words[i].ToLower().Equals("update"))
        //            {

        //                for (int k = i + 1; k < words.Length; k++)
        //                {
        //                    if (words[k].ToLower().Replace(" ", string.Empty).Length != 0)
        //                    {
        //                        return new SQL_CMD(SQL_CMD.CMD_TYPE_UPDATE, words[k]);
        //                    }
        //                }

        //            }
        //        }
        //        throw new Exception("تعليمة SQL غير صالحة");
        //    }
        //    catch (Exception ee)
        //    {
        //        throw new Exception("GetCMD_INFO:" + ee.Message);
        //    }


        //}
        public void ExecuteSQLCommand(string SQLCommand)
        {
            try
            {
                //SQL_CMD SQL_CMD_ = Get_SQL_CMD_Info(SQLCommand);

                //if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(ItemTable))
                //{

                //    DataTable t = Get_Table_Records("select * from " + ItemTable);
                //    if (t.Rows.Count > 149) throw new Exception("يمكنك فقط ادخال 150 عنصر في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(MaintenanceOPRTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + MaintenanceOPRTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 عملية صيانة في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(BillBuyTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + BillBuyTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 فاتورة شراء في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(BillSELLTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + BillSELLTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 فاتورة مبيع في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(AssemblageTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + AssemblageTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 عملية تجميع في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(DisAssemblageTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + DisAssemblageTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 عملية تفكيك في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(PayOrderTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + PayOrderTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 امر صرف  في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(EmployeeTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + EmployeeTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 موظف في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(ExchangeOPRTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + ExchangeOPRTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 عملية صرف في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(PayINTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + PayINTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 عملية دفع داخلة في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(PayOUTTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + PayOUTTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 عملية دفع خارجة في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(MoneyTransFormOPRTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + MoneyTransFormOPRTable);
                //    if (t.Rows.Count > 49) throw new Exception("يمكنك فقط ادخال 50 عملية تحويل مال في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(ItemINTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + ItemINTable);
                //    if (t.Rows.Count > 99) throw new Exception("يمكنك فقط ادخال 100 عملية ادخال عناصر في النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_INSERT && SQL_CMD_.TableName.Equals(ItemOUTTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + ItemOUTTable);
                //    if (t.Rows.Count > 99) throw new Exception("يمكنك فقط ادخال 100 عملية اخراج عناصر في النسخة التجريبية");
                //}

                SQLiteCommand DATABASE_SQL_COMMAND = new SQLiteCommand("", DATABASE_CONNECTION);

                DATABASE_SQL_COMMAND.CommandText = SQLCommand;
                if (DATABASE_CONNECTION.State == ConnectionState.Open) DATABASE_CONNECTION.Close();
                DATABASE_CONNECTION.Open();

                //DataTable t1 = new DataTable();
                //try
                //{
                //    t1 = GetData("select * from OverLoad_Log "
                //        + " where LogType=4 or LogType=2 or LogType=3 ");

                //}
                //catch 
                //{
                //    throw new Exception(" قاعدة بيانات غير صحيحة");
                //}
                //if (t1.Rows.Count > 5) throw new Exception(" تم استهلاك عدد العمليات المسموح بها في النسخة التجريبية");
                DATABASE_SQL_COMMAND.ExecuteNonQuery();
                DATABASE_CONNECTION.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("ExecuteSQLCommand:" + ex.Message);
            }


        }
        public void ExecuteSQLCommand_Serialize(SQLiteCommand SQLiteCommand_)
        {
            try
            {
                //throw new Exception(SQLiteCommand_.CommandText);
                if (DATABASE_CONNECTION.State == ConnectionState.Open) DATABASE_CONNECTION.Close();
                DATABASE_CONNECTION.Open();
                SQLiteCommand_.ExecuteNonQuery();
                DATABASE_CONNECTION.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("ExecuteSQLCommand_Serialize:" + ex.Message);
            }


        }
        //private DataTable Get_Table_Records(string SQLCommand)
        //{
        //    try
        //    {
        //        SQLiteCommand DATABASE_SQL_COMMAND = new SQLiteCommand("", DATABASE_CONNECTION);
        //        if (DATABASE_CONNECTION.State != ConnectionState.Open)
        //            DATABASE_CONNECTION.Open();
        //        DATABASE_SQL_COMMAND.CommandText = SQLCommand;
        //        SQLiteDataAdapter DATABASE_ADAPTER = new SQLiteDataAdapter();
        //        DATABASE_ADAPTER.SelectCommand = DATABASE_SQL_COMMAND;
        //        DataTable table = new DataTable();
        //        DATABASE_ADAPTER.Fill(table);
        //        //DATABASE_CONNECTION.Close();
        //        return table;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("GetData" + ex.Message);
        //    }
        //}

        public DataTable GetData(string SQLCommand)
        {
            try
            {
                //SQL_CMD SQL_CMD_ = Get_SQL_CMD_Info(SQLCommand);
                //if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(ItemTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + ItemTable);
                //    if (t.Rows.Count > 155) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(MaintenanceOPRTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + MaintenanceOPRTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(BillBuyTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + BillBuyTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(BillSELLTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + BillSELLTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(AssemblageTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + AssemblageTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(DisAssemblageTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + DisAssemblageTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(PayOrderTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + PayOrderTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(EmployeeTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + EmployeeTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(ExchangeOPRTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + ExchangeOPRTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(PayINTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + PayINTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(PayOUTTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + PayOUTTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(MoneyTransFormOPRTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + MoneyTransFormOPRTable);
                //    if (t.Rows.Count > 55) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(ItemINTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + ItemINTable);
                //    if (t.Rows.Count > 105) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                //else if (SQL_CMD_.CMD_Type == SQL_CMD.CMD_TYPE_SELECT && SQL_CMD_.TableName.Equals(ItemOUTTable))
                //{
                //    DataTable t = Get_Table_Records("select * from " + ItemOUTTable);
                //    if (t.Rows.Count > 105) throw new Exception("يبدو انه تم التلاعب في بيانات النسخة التجريبية");
                //}
                SQLiteCommand DATABASE_SQL_COMMAND = new SQLiteCommand("", DATABASE_CONNECTION);
                if (DATABASE_CONNECTION.State != ConnectionState.Open)
                    DATABASE_CONNECTION.Open();
                DATABASE_SQL_COMMAND.CommandText = SQLCommand;
                SQLiteDataAdapter DATABASE_ADAPTER = new SQLiteDataAdapter();
                DATABASE_ADAPTER.SelectCommand = DATABASE_SQL_COMMAND;
                DataTable table = new DataTable();
                DATABASE_ADAPTER.Fill(table);
                //DATABASE_CONNECTION.Close();
                return table;

            }
            catch (Exception ex)
            {

                throw new Exception("GetData" + ex.Message);
            }
        }
        //public byte [] GetBlop(string SQLCommand)
        //{
        //    try
        //    {
        //        string path1 = @"C:\SQL.txt";

        //        System.IO.TextWriter tw = new System.IO.StreamWriter(path1, true);
        //        tw.WriteLine(DateTime.Now.ToShortTimeString()
        //            + "SQL:" +
        //           SQLCommand);
        //        tw.Close();
        //        SQLiteCommand DATABASE_SQL_COMMAND = new SQLiteCommand("", DATABASE_CONNECTION);
        //        if (DATABASE_CONNECTION.State != ConnectionState.Open)
        //            DATABASE_CONNECTION.Open();
        //        DATABASE_SQL_COMMAND.CommandText = SQLCommand;
        //        SQLiteDataAdapter DATABASE_ADAPTER = new SQLiteDataAdapter();
        //        DATABASE_ADAPTER.SelectCommand = DATABASE_SQL_COMMAND;
        //        DataTable table = new DataTable();
        //        DATABASE_ADAPTER.Fill(table);
        //        //DATABASE_CONNECTION.Close();
        //        if (table.Rows.Count == 1) return (byte[])table.Rows[0][0];
        //        else return null;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("GetBlop:" + ex.Message);
        //    }
        //}
        #region User
        public class User
        {
            public uint UserID;
            public string UserName;
            public DateTime adddate;
            public bool Disabled;
            internal Employee _Employee;
            public User(uint UserID_, string UserName_, DateTime adddate_, bool Disabled_, Employee Employee_
               )
            {
                UserID = UserID_;
                _Employee = Employee_;
                UserName = UserName_;
                adddate = adddate_;
                Disabled = Disabled_;
            }
        }

        private static class UserTable
        {
            public const string TableName = "OverLoad_User";
            public const string OV_UserID = "OV_UserID";
            public const string OV_UserName = "OV_UserName";
            public const string OV_Password = "OV_Password";
            public const string AddDate = "AddDate";
            public const string Disabled_ = "Disabled_";
            public const string EmployeeID = "EmployeeID";
        }
        public User GetEmployeeUser(Employee Employee_)
        {
            try
            {
                DataTable t = new DataTable();
                t = this.GetData("select "
                 + UserTable.OV_UserID + ","
                 + UserTable.OV_UserName + ","
                 + UserTable.AddDate + ","
                 + UserTable.Disabled_
                + " from   "
                + UserTable.TableName
                + " where "
                + UserTable.EmployeeID + "=" + Employee_.EmployeeID
                  );
                if (t.Rows.Count == 1)
                {
                    uint userid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    string username = t.Rows[0][1].ToString();
                    DateTime adddate = Convert.ToDateTime(t.Rows[0][2].ToString());
                    bool disabled = Convert.ToInt32(t.Rows[0][3].ToString()) == 1 ? true : false;

                    return new User(userid, username, adddate, disabled, Employee_);
                }
                else
                    return null;
            }
            catch (Exception ee)
            {
                throw new Exception("GetEmployeeUser:" + ee.Message);
                return null;
            }
        }
        public User GetUser_BY_ID(uint userid)
        {
            try
            {
                DataTable t = new DataTable();
                t = this.GetData("select "
                 + UserTable.OV_UserID + ","
                 + UserTable.OV_UserName + ","
                 + UserTable.AddDate + ","
                 + UserTable.Disabled_ + ","
                 + UserTable.EmployeeID
                + " from   "
                + UserTable.TableName
                + " where "
                + UserTable.OV_UserID + "=" + userid
                  );
                if (t.Rows.Count == 1)
                {
                    string username = t.Rows[0][1].ToString();
                    DateTime adddate = Convert.ToDateTime(t.Rows[0][2].ToString());
                    bool disabled = Convert.ToInt32(t.Rows[0][3].ToString()) == 1 ? true : false;
                    Employee employee;
                    try
                    {
                        employee = new EmployeeSQL(this).GetEmployeeInforBYID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                    }
                    catch
                    {

                        employee = null;
                    }
                    return new User(userid, username, adddate, disabled, employee);
                }
                else
                    throw new Exception("User Not Found");
             }
            catch (Exception ee)
            {
                throw new Exception("GetUser_By_ID:" + ee.Message);
            }
        }
        private User Get_Administrator_User()
        {
            try
            {
                DataTable t = new DataTable();
                t = this.GetData("select "
                 + UserTable.OV_UserID + ","
                 + UserTable.OV_UserName + ","
                 + UserTable.AddDate + ","
                 + UserTable.Disabled_
                + " from   "
                + UserTable.TableName
                + " where "
                + UserTable.OV_UserName + "='Administrator'"
                  );
                if (t.Rows.Count == 1)
                {
                    uint UserID = Convert.ToUInt32(t.Rows[0][0].ToString());
                    string username = t.Rows[0][1].ToString();
                    DateTime adddate = Convert.ToDateTime(t.Rows[0][2].ToString());
                    bool disabled = Convert.ToInt32(t.Rows[0][3].ToString()) == 1 ? true : false;
                   
                    return new User(UserID, username, adddate, disabled,null);
                }
                else
                    return null;
            }
            catch (Exception ee)
            {
                throw new Exception("Get_A_User:" + ee.Message);
            }
        }
  




        #endregion
        #region Premmession

        public class Premession
        {
            internal const uint ADMIN_GROUP = 0;
            internal const uint BUY_GROUP = 1;
            internal const uint SELL_GROUP = 2;
            internal const uint MAINTENANCE_GROUP = 3;
            internal const uint EMPLOYEE_GROUP = 4;
            internal const uint ITEM_GROUP = 5;
            internal const uint CONTACT_GROUP = 6;
            internal const uint INDUSTRY_GROUP = 7;
            internal const uint CONTAINER_GROUP = 8;
            public uint GroupID;
            public bool Join;
            public Premession(uint GroupID_, bool Join_)
            {
                GroupID = GroupID_;
                Join = Join_;
            }

        }
        public class AccessSellTypePremession
        {



            public uint UserID;
            public SellType _SellType;
            public bool Access;
            public AccessSellTypePremession(
             uint UserID_,
             SellType SellType_,
             bool Access_)
            {

                UserID = UserID_;
                _SellType = SellType_;
                Access = Access_;
            }

        }
        public class AccessContainerPremession
        {
            public uint PermissionID;
            public uint UserID;
            public TradeStoreContainer Container;
            public AccessContainerPremession(uint PermissionID_,
             uint UserID_,
             TradeStoreContainer Container_)
            {
                PermissionID = PermissionID_;
                UserID = UserID_;
                Container = Container_;
            }

        }
        public class AccessFolderPremession
        {
            public uint PermissionID;
            public uint UserID;
            public Folder _Folder;
            public AccessFolderPremession(uint PermissionID_,
             uint UserID_,
             Folder Folder_)
            {
                PermissionID = PermissionID_;
                UserID = UserID_;
                _Folder = Folder_;
            }

        }
        public class MoneyBoxPremession
        {

            public MoneyBox _MoneyBox;
            public bool Allow;
            public MoneyBoxPremession(MoneyBox MoneyBox_, bool Allow_)
            {
                _MoneyBox = MoneyBox_;
                Allow = Allow_;
            }

        }
        private static class AdminGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Admin";
            public const string OV_UserID = "OV_UserID";

        }
        private static class SellGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Sell";
            public const string OV_UserID = "OV_UserID";

        }
        private static class AccessSellType_Table
        {
            public const string TableName = "OverLoad_Permission_AccessSellType";
            public const string OV_UserID = "OV_UserID";
            public const string SellTypeID = "SellTypeID";

        }
        private static class AccessContainer_Table
        {
            public const string TableName = "OverLoad_Permission_AccessContainer";
            public const string PermissionID = "PermissionID";
            public const string OV_UserID = "OV_UserID";
            public const string ContainerID = "ContainerID";

        }
        private static class AccessFolder_Table
        {
            public const string TableName = "OverLoad_Permission_AccessFolder";
            public const string PermissionID = "PermissionID";
            public const string OV_UserID = "OV_UserID";
            public const string FolderID = "FolderID";

        }
        private static class BuyGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Buy";
            public const string OV_UserID = "OV_UserID";

        }
        private static class MaintenanceGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Maintenance";
            public const string OV_UserID = "OV_UserID";

        }
        private static class EmployeeGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Employee";
            public const string OV_UserID = "OV_UserID";

        }
        private static class ItemGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Item";
            public const string OV_UserID = "OV_UserID";

        }
        private static class ContactGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Contact";
            public const string OV_UserID = "OV_UserID";

        }
        private static class IndustryGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Industry";
            public const string OV_UserID = "OV_UserID";

        }
        private static class ContainerGroup_Table
        {
            public const string TableName = "OverLoad_Permission_Container";
            public const string OV_UserID = "OV_UserID";

        }
        private static class MoneyBoxGroup_Table
        {
            public const string TableName = "OverLoad_Permission_MoneyBox";
            public const string OV_UserID = "OV_UserID";
            public const string MoneyBoxID = "MoneyBoxID";

        }
        #region QueryPremession
        internal bool IS_Belong_To_Admin_Group(uint userid)
        {
            
            try
            {

                User user = GetUser_BY_ID(userid);
                if (user.UserName == "Administrator") return true;
                DataTable t1 = GetData(" select  * from "
                     + AdminGroup_Table.TableName
                      + " where "
                     + AdminGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception("I_B_T_A_G:"+ee.Message);
            }
        }
        internal bool IS_Belong_To_Buy_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + BuyGroup_Table.TableName
                      + " where "
                     + BuyGroup_Table.OV_UserID + "=" + userid
                     );

                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool IS_Belong_To_Sell_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + SellGroup_Table.TableName
                      + " where "
                     + SellGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        internal bool IS_Belong_To_Maintenance_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + MaintenanceGroup_Table.TableName
                      + " where "
                     + MaintenanceGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        internal bool IS_Belong_To_Employee_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + EmployeeGroup_Table.TableName
                      + " where "
                     + EmployeeGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        internal bool IS_Belong_To_Item_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;
                DataTable t1 = GetData(" select  * from "
                     + ItemGroup_Table.TableName
                      + " where "
                     + ItemGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        internal bool IS_Belong_To_Contact_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + ContactGroup_Table.TableName
                      + " where "
                     + ContactGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool IS_Belong_To_Industry_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + IndustryGroup_Table.TableName
                      + " where "
                     + IndustryGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool IS_Belong_To_Container_Group(uint userid)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(userid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + ContainerGroup_Table.TableName
                      + " where "
                     + ContainerGroup_Table.OV_UserID + "=" + userid
                     );
                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        public List<Premession> GetUserPremessions(uint userid)
        {
            List<Premession> list = new List<Premession>();
            try
            {
                if (IS_Belong_To_Admin_Group(userid))
                    list.Add(new Premession(Premession.ADMIN_GROUP, true));
                else
                    list.Add(new Premession(Premession.ADMIN_GROUP, false));
                if (IS_Belong_To_Buy_Group(userid))
                    list.Add(new Premession(Premession.BUY_GROUP, true));
                else
                    list.Add(new Premession(Premession.BUY_GROUP, false));

                if (IS_Belong_To_Sell_Group(userid))
                    list.Add(new Premession(Premession.SELL_GROUP, true));
                else
                    list.Add(new Premession(Premession.SELL_GROUP, false));

                if (IS_Belong_To_Maintenance_Group(userid))
                    list.Add(new Premession(Premession.MAINTENANCE_GROUP, true));
                else
                    list.Add(new Premession(Premession.MAINTENANCE_GROUP, false));

                if (IS_Belong_To_Employee_Group(userid))
                    list.Add(new Premession(Premession.EMPLOYEE_GROUP, true));
                else
                    list.Add(new Premession(Premession.EMPLOYEE_GROUP, false));

                if (IS_Belong_To_Item_Group(userid))
                    list.Add(new Premession(Premession.ITEM_GROUP, true));
                else
                    list.Add(new Premession(Premession.ITEM_GROUP, false));

                if (IS_Belong_To_Contact_Group(userid))
                    list.Add(new Premession(Premession.CONTACT_GROUP, true));
                else
                    list.Add(new Premession(Premession.CONTACT_GROUP, false));

                if (IS_Belong_To_Industry_Group(userid))
                    list.Add(new Premession(Premession.INDUSTRY_GROUP, true));
                else
                    list.Add(new Premession(Premession.INDUSTRY_GROUP, false));

                if (IS_Belong_To_Container_Group(userid))
                    list.Add(new Premession(Premession.CONTAINER_GROUP, true));
                else
                    list.Add(new Premession(Premession.CONTAINER_GROUP, false));
            }
            catch (Exception ee)
            {
                throw new Exception("GetUserPremessions:" + ee.Message);
            }
            return list;
        }
        internal bool IS_Belong_To_MoneyBox_Group(uint usedid, MoneyBox MoneyBox_)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(usedid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + MoneyBoxGroup_Table.TableName
                      + " where "
                     + MoneyBoxGroup_Table.OV_UserID + "=" + usedid
                     + " and "
                     + MoneyBoxGroup_Table.MoneyBoxID + "=" + MoneyBox_.BoxID
                     );

                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        public List<MoneyBoxPremession> Get_MoneyBoxPremession_List(uint userid)
        {
            List<MoneyBoxPremession> list = new List<MoneyBoxPremession>();
            try
            {
                List<MoneyBox> moneyboxlist = new MoneyBoxSQL(this).GetMoneyBox_List();
                for (int i = 0; i < moneyboxlist.Count; i++)
                {
                    if (IS_Belong_To_MoneyBox_Group(userid, moneyboxlist[i]))
                        list.Add(new MoneyBoxPremession(moneyboxlist[i], true));
                    else
                        list.Add(new MoneyBoxPremession(moneyboxlist[i], false));
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Get_MoneyBoxPremession_List:" + ee.Message);
            }
            return list;
        }
        internal bool IS_Belong_To_AccessSellTypePremession_Group(uint usedid, SellType SellType_)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(usedid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + AccessSellType_Table.TableName
                      + " where "
                     + AccessSellType_Table.OV_UserID + "=" + usedid
                     + " and "
                     + AccessSellType_Table.SellTypeID + "=" + SellType_.SellTypeID
                     );

                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        public List<AccessSellTypePremession> Get_AccessSellTypePremession_List(uint userid)
        {
            List<AccessSellTypePremession> list = new List<AccessSellTypePremession>();
            try
            {
                List<SellType> SellTypelist = new TradeSQL.SellTypeSql(this).GetSellTypeList();

                for (int i = 0; i < SellTypelist.Count; i++)
                {
                    if (IS_Belong_To_AccessSellTypePremession_Group(userid, SellTypelist[i]))
                        list.Add(new AccessSellTypePremession(userid, SellTypelist[i], true));
                    else
                        list.Add(new AccessSellTypePremession(userid, SellTypelist[i], false));
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Get_AccessSellTypePremession_List:" + ee.Message);
            }
            return list;
        }
        internal bool IS_Belong_To_AccessContainerPremession_Group(uint usedid, TradeStoreContainer Container_)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(usedid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + AccessContainer_Table.TableName
                      + " where "
                     + AccessContainer_Table.OV_UserID + "=" + usedid
                     + " and "
                     + AccessContainer_Table.ContainerID + (Container_ == null ? " is null " : "=" + Container_.ContainerID.ToString())
                     );

                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        public List<AccessContainerPremession> Get_AccessContainerPremession_List(uint userid)
        {
            List<AccessContainerPremession> list = new List<AccessContainerPremession>();
            try
            {
                DataTable t1 = GetData(" select  "
                    + AccessContainer_Table.PermissionID
                    + " , "
                    + AccessContainer_Table.ContainerID
                     + " , "
                     + AccessContainer_Table.PermissionID
                    + " from "
                   + AccessContainer_Table.TableName
                    + " where "
                   + AccessContainer_Table.OV_UserID + "=" + userid

                   );
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    uint premessionid = Convert.ToUInt32(t1.Rows[i][0].ToString());
                    TradeStoreContainer Container_;
                    try
                    {
                        uint containerid = Convert.ToUInt32(t1.Rows[i][1].ToString());
                        Container_ = new TradeSQL.TradeStoreContainerSQL(this).GetContainerBYID(containerid);
                    }
                    catch
                    {
                        Container_ = null;
                    }



                    list.Add(new AccessContainerPremession(premessionid, userid, Container_));
                }

            }
            catch (Exception ee)
            {
                throw new Exception("Get_AccessContainerPremession_List:" + ee.Message);
            }
            return list;
        }
        internal bool IS_Belong_To_AccessFolderPremession_Group(uint usedid, Folder Folder_)
        {
            try
            {
                //if (IS_Belong_To_Admin_Group(usedid)) return true;

                DataTable t1 = GetData(" select  * from "
                     + AccessFolder_Table.TableName
                      + " where "
                     + AccessFolder_Table.OV_UserID + "=" + usedid
                     + " and "
                     + AccessFolder_Table.FolderID + (Folder_ == null ? " is null" : "=" + Folder_.FolderID.ToString())
                     );

                if (t1.Rows.Count > 0)
                    return true;
                else return false;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        public List<AccessFolderPremession> Get_AccessFolderPremession_List(uint userid)
        {

            try
            {
                List<AccessFolderPremession> list = new List<AccessFolderPremession>();

                DataTable t1;
                if (IS_Belong_To_Admin_Group (userid ))
                {
                    list.Add(new AccessFolderPremession(0, userid, null));
                    return list;
                }
                else
                {
                    t1 = GetData(" select  "
                    + AccessFolder_Table.PermissionID
                    + " , "
                    + AccessFolder_Table.FolderID
                     + " , "
                     + AccessFolder_Table.PermissionID
                    + " from "
                   + AccessFolder_Table.TableName
                    + " where "
                   + AccessFolder_Table.OV_UserID + "=" + userid

                   );
                }
                
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    uint premessionid = Convert.ToUInt32(t1.Rows[i][0].ToString());
                    Folder Folder_;
                    try
                    {
                        uint folderid = Convert.ToUInt32(t1.Rows[i][1].ToString());
                        Folder_ = new ItemObjSQL.FolderSQL(this).GetFolderInfoByID(folderid);
                    }
                    catch
                    {
                        Folder_ = null;
                    }



                    list.Add(new AccessFolderPremession(premessionid, userid, Folder_));
                }
                return list;

            }
            catch (Exception ee)
            {
                throw new Exception("Get_AccessContainerPremession_List:" + ee.Message);
            }
        }

        #endregion
        //public List<MoneyBox> Get_User_Allowed_MoneyBox()
        //{
        //    List<MoneyBox> list = new List<MoneyBox>();
        //    try
        //    {
        //        DataTable t1 = new DataTable();
        //        if (IS_Belong_To_Admin_Group(_User.UserID))
        //        {

        //            list = new MoneyBoxSQL(this).GetMoneyBox_List();
        //            if (list.Count == 0) throw new Exception("لا يوجد اي صندوق مال منشىء");
        //            else return list;
        //        }
        //        else
        //        {
        //            t1 = GetData(" select  "
        //                + MoneyBoxGroup_Table.MoneyBoxID
        //                + " from "
        //                               + MoneyBoxGroup_Table.TableName
        //                               + " where "
        //                               + MoneyBoxGroup_Table.OV_UserID + "=" + _User.UserID
        //                               );
        //        }
        //        for (int i = 0; i < t1.Rows.Count; i++)
        //        {
        //            list.Add(new MoneyBoxSQL(this).GetMoneyBoxBYID
        //                (Convert.ToUInt32(t1.Rows[i][0])));
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        throw new Exception("Get_User_Allowed_MoneyBox:" + ee.Message);
        //    }
        //    return list;
        //}
        #endregion
    }

}
