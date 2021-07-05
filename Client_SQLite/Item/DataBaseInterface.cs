using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using OverLoad_Client.Company.Objects;
using OverLoad_Client.OverLoadClientNET;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data.SQLite;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Trade.Objects;

namespace OverLoad_Client
{

    public class DatabaseInterface
    {
        public  class OverLoad_SQL_Functions_Code
        {
            public const byte TradeItemStoreSQL_Get_TradeItemStore_Report_List = 0X01;
            public const byte ContactSQL_Get_Contact_Buys_ReportDetail = 0X02;
            public const byte ContactSQL_Get_Contact_Buys_Report = 0x03;
            public const byte ContactSQL_Get_Contact_Pays_ReportDetail = 0x04;
            public const byte ContactSQL_Get_Contact_PayCurrencyReport = 0x05;
            public const byte ContactSQL_Get_Contact_Sells_ReportDetail = 0X06;
            public const byte ContactSQL_Get_Contact_Sells_Report = 0x07;
            public const byte ContactSQL_Get_Contact_Maintenance_ReportDetail = 0X08;
            public const byte ContactSQL_Get_Contact_Maintenance_Report = 0x09;
            public const byte ContactSQL_Contact_GetBillsReportList = 0x0a;

            public const byte AccountSQL_Account_GetPays_DayReport = 0x10;
            public const byte AccountSQL_Account_GetPays_MonthReport = 0x11;
            public const byte AccountSQL_Account_GetPays_YearReport = 0x12;
            public const byte AccountSQL_Account_GetPays_YearRangeReport = 0x13;
            public const byte AccountSQL_GetAccountOprReport_Details_InDay = 0x14;
            public const byte AccountSQL_GetAccountOprReport_Details_InMonh = 0x15;
            public const byte AccountSQL_GetAccountOprReport_Details_InYear = 0x16;
            public const byte AccountSQL_GetAccountOprReport_Details_InYearRange = 0x17;

            public const byte AccountSQL_Get_Report_Buys_Day_ReportDetail = 0x20;
            public const byte AccountSQL_Get_Report_Buys_Day_Report = 0x21;
            public const byte AccountSQL_Get_Report_Buys_Month_ReportDetail = 0x22;
            public const byte AccountSQL_Get_Report_Buys_Month_Report = 0x23;
            public const byte AccountSQL_Get_Report_Buys_Year_ReportDetail = 0x24;
            public const byte AccountSQL_Get_Report_Buys_Year_Report = 0x25;
            public const byte AccountSQL_Get_Report_Buys_YearRange_ReportDetail = 0x26;

            public const byte AccountSQL_Get_Report_PayOrders_Day_ReportDetail = 0x30;
            public const byte AccountSQL_Get_Report_PayOrders_Day_Report = 0x31;
            public const byte AccountSQL_Get_Report_PayOrders_Month_ReportDetail = 0x32;
            public const byte AccountSQL_Get_Report_PayOrders_Month_Report = 0x33;
            public const byte AccountSQL_Get_Report_PayOrders_Year_ReportDetail = 0x34;
            public const byte AccountSQL_Get_Report_PayOrders_Year_Report = 0x35;
            public const byte AccountSQL_Get_Report_PayOrders_YearRange_ReportDetail = 0x36;

            public const byte AccountSQL_Get_Report_Sells_Day_ReportDetail = 0x40;
            public const byte AccountSQL_Get_Report_Sells_Day_Report = 0x41;
            public const byte AccountSQL_Get_Report_Sells_Month_ReportDetail = 0x42;
            public const byte AccountSQL_Get_Report_Sells_Month_Report = 0x43;
            public const byte AccountSQL_Get_Report_Sells_Year_ReportDetail = 0x44;
            public const byte AccountSQL_Get_Report_Sells_Year_Report = 0x45;
            public const byte AccountSQL_Get_Report_Sells_YearRange_ReportDetail = 0x46;


            public const byte AccountSQL_Get_Report_MaintenanceOPRs_Day_ReportDetail = 0x50;
            public const byte AccountSQL_Get_Report_MaintenanceOPRs_Day_Report = 0x51;
            public const byte AccountSQL_Get_Report_MaintenanceOPRs_Month_ReportDetail = 0x52;
            public const byte AccountSQL_Get_Report_MaintenanceOPRs_Month_Report = 0x53;
            public const byte AccountSQL_Get_Report_MaintenanceOPRs_Year_ReportDetail = 0x54;
            public const byte AccountSQL_Get_Report_MaintenanceOPRs_Year_Report = 0x55;
            public const byte AccountSQL_Get_Report_MaintenanceOPRs_YearRange_ReportDetail = 0x56;

            public const byte TradeSQL_Get_Item_AvailableAmount_Report_List = 0x60;
            public const byte TradeSQL_Get_ItemIN_AvailableAmount_Report_List = 0x61;

            public const byte CompanySQL_GetEmployeesReportList = 0x70;
            public const byte CompanySQL_Get_EmployeeMent_Employee_Report_List = 0x71;
        }
        public static class MessageType
        {
            public const byte Error = 0xf0;
            public const byte ExecuteSQlCMD = 0xf1;
            public const byte GetData = 0xf2;
            public const byte ExecuteSQlCMD_INSERT_Serialize = 0xf3;
            public const byte GetData_ByteArray = 0xf4;
            public const byte GetData_ByteArray_NULL = 0xf5;
            public const byte Execute_Function = 0xf6;


        }
        public class OverLoad_SQLite_Parameter
        {

            public DbType _DbType;
            public string Parameter_Name;
            public bool IS_Blop;
            public byte[] Parameter_Value;
            public OverLoad_SQLite_Parameter(DbType DbType_,
             string Parameter_Name_, bool ISBlop,
             byte[] Parameter_Value_)
            {
                _DbType = DbType_;
                Parameter_Name = Parameter_Name_;
                IS_Blop = ISBlop;
                Parameter_Value = Parameter_Value_;
            }

        }
        //public class OverLoad_SQLite_Parameter_Serialize : DataTable
        //{
        //    public OverLoad_SQLite_Parameter_Serialize(string tablename, List<OverLoad_SQLite_Parameter> Parameters)
        //    {
        //        this.TableName = tablename;
        //        this.Columns.Add("DataType", typeof(DbType));
        //        this.Columns.Add("ParameterName", typeof(string));
        //        this.Columns.Add("IS_Blop", typeof(bool ));
        //        this.Columns.Add("ParameterValue", typeof(byte[]));
        //        for (int i = 0; i < Parameters.Count; i++)
        //        {
        //            DataRow row = this.NewRow();
        //            row["DataType"] = Parameters[i]._DbType;
        //            row["ParameterName"] = Parameters[i].Parameter_Name;
        //            row["IS_Blop"] = Parameters[i].IS_Blop ;
        //            row["ParameterValue"] = Parameters[i].Parameter_Value ;
        //            this.Rows.Add(row);
        //        }


        //    }
        //}
        private const int DataBaseID = 1;
        private DateTime App_Start_Date = new DateTime(2020, 3, 1);

        public const string ROWID_COLUMN = "ROWID";

        private  OverLoadClientNET.OverLoadEndPoint _OverLoadEndPoint;
        private  SQLiteConnection DATABASE_CONNECTION;
        private SQLiteCommand DATABASE_SQL_COMMAND;
        public Company.Objects.Part COMPANY;

        private const string Administrator = "Administrator";
        private User _User;
        internal User __User
        {
            get { return _User; }
        }
        internal OverLoadClientNET.OverLoadEndPoint OverLoadEndPoint_
        {
            get { return _OverLoadEndPoint; }
        }
        public DatabaseInterface(OverLoadClientNET.OverLoadEndPoint OverLoadEndPoint_, string ServerName)
        {


            COMPANY = new Part(0, ServerName, App_Start_Date, null);
            _OverLoadEndPoint = OverLoadEndPoint_;


        }
        public void LogIN(string UserName, string PassWord)
        {
            try
            {
                DataTable t1 = GetData(" select  * from "
                     + UserTable.TableName
                      + " where "
                     + UserTable.OV_UserName + "='" + UserName + "'"
                     );
                if (t1.Rows.Count == 0)
                    throw new Exception("اسم المستخدم غير صحيح");
   
                //ShowSqlQuery dd = new ShowSqlQuery();
                DataTable t2 = GetData(" select "
                    + UserTable.OV_UserID
                    + ","
                     + UserTable.AddDate
                    + ","
                     + UserTable.Disabled_
                     + ","
                     + UserTable.EmployeeID
                    + " from "
                     + UserTable.TableName
                    + " where "
                     + UserTable.OV_UserName + "='" + UserName + "'"
                     + " and "
                     + UserTable.OV_Password + "='" + PassWord + "'"
                    );

                if (t2.Rows.Count == 1)
                {
                    List<AccessFolderPremession> AccessFolderPremessionList = new List<AccessFolderPremession>();
                    List<AccessContainerPremession> AccessContainerPremessionList = new List<AccessContainerPremession>();
                    List<AccessSellTypePremession> AccessSellTypePremessionList = new List<AccessSellTypePremession>();
                    uint userid = Convert.ToUInt32(t2.Rows[0][0].ToString());
                    DateTime adddate = Convert.ToDateTime(t2.Rows[0][1].ToString());
                    Employee Employee_;
                    bool disabled = (Convert.ToInt32(t2.Rows[0][2].ToString()) == 1 ? true : false);
                    try
                    {
                        Employee_ = new Company.CompanySQL.EmployeeSQL(this).GetEmployeeInforBYID(Convert.ToUInt32(t2.Rows[0][3].ToString()));
                        AccessFolderPremessionList = Get_AccessFolderPremession_List(userid);
                        AccessContainerPremessionList = Get_AccessContainerPremession_List(userid);
                        AccessSellTypePremessionList = Get_AccessSellTypePremession_List(userid);
                        this._User = new User(userid, UserName, adddate, disabled, Employee_, AccessFolderPremessionList, AccessContainerPremessionList, AccessSellTypePremessionList);
                    }
                    catch
                    {

                        if (UserName != Administrator)
                            throw new Exception("على مايبدو تم تعديل قاعدة البيانات بشكل مخالف للتصميم :)");
                        AccessFolderPremessionList.Add(new AccessFolderPremession(0, userid, null));
                        AccessContainerPremessionList.Add(new AccessContainerPremession(0, userid, null));

                        List<SellType> selltypelist = new Trade.TradeSQL.SellTypeSql(this).GetSellTypeList();
                        for (int i = 0; i < selltypelist.Count; i++)
                            AccessSellTypePremessionList.Add(new AccessSellTypePremession(userid, selltypelist[i], true));
                        this._User = new User(userid, UserName, adddate, disabled, null, AccessFolderPremessionList, AccessContainerPremessionList, AccessSellTypePremessionList);
                    }

                    AddLog(Log.LogType.LOGIN
                    , Log.Log_Target.User
                    , UserName, true, "");
                }
                else throw new Exception("كلمة المرور غير صحيحة");


            }
            catch (Exception ee)
            {
                AddLog(Log.LogType.LOGIN
                    , Log.Log_Target.User
                    , UserName, false, ee.Message);
                throw new Exception("LogIN:" + ee.Message);
            }
        }
        public string GetUser_EmployeeName()
        {
            if (this._User._Employee == null) return " مدير النظام";
            else return this._User._Employee.EmployeeName;
        }

        public void ExecuteSQLCommand(string SQLCommand)
        {

            //if (DataBaseLocked) throw new Exception("قاعدة البيانات مقفولة");

            //    DATABASE_SQL_COMMAND.CommandText = SQLCommand;
            //    if (DATABASE_CONNECTION.State != ConnectionState.Open) /*DATABASE_CONNECTION.Close();*/
            //        DATABASE_CONNECTION.Open();
            //    DATABASE_SQL_COMMAND.ExecuteNonQuery();
            //DATABASE_CONNECTION.Close();
            List<byte> order = new List<byte>();
            order.Add(MessageType.ExecuteSQlCMD);
            order.AddRange(System.Text.Encoding.UTF8.GetBytes(SQLCommand));
            byte[] orderarray = order.ToArray();
            MessageSend messagesend = _OverLoadEndPoint.NewMessageSend(-1);
            //_OverLoadEndPoint. log.addlog(DateTime.Now, "error ms length" + orderarray.Length.ToString());
            messagesend.SendMessage(_OverLoadEndPoint, orderarray);
            MessageReceive MessageReceive_ = _OverLoadEndPoint.GetReplayMessage(messagesend);
            if (MessageReceive_ == null) throw new Exception(" Replay IS Null");
            byte[] replay = MessageReceive_.GetFullMessage();
            if(replay ==null ) throw new Exception(" Replay Byte Array IS Null");
            //if (replay[0] == MessageType.Error)
            //{
            //    byte[] errorstream = new byte[replay.Length - 1];
            //    Array.Copy(replay, 1, errorstream, 0, errorstream.Length);
            //    throw new Exception(System.Text.Encoding.UTF8.GetString(errorstream));
            //}

        }
        public DataTable GetData(string SQLCommand)
        {
            try
            {

                List<byte> order = new List<byte>();
                order.Add(MessageType.GetData);
                order.AddRange(System.Text.Encoding.UTF8.GetBytes(SQLCommand));
                byte[] orderarray = order.ToArray();
                MessageSend messagesend = _OverLoadEndPoint.NewMessageSend(-1);
                //_OverLoadEndPoint.log.addlog(DateTime.Now, "error ms length" + orderarray.Length.ToString());
                messagesend.SendMessage(_OverLoadEndPoint, orderarray);

                MessageReceive MessageReceive_ = _OverLoadEndPoint.GetReplayMessage(messagesend);
                if(MessageReceive_ ==null ) throw new Exception(" Replay IS Null");

                byte[] replay = MessageReceive_.GetFullMessage();
                if (replay == null) throw new Exception(" Replay byte array IS Null");

                //byte[] data = new byte[replay.Length - 1];
                //Array.Copy(replay, 1, data, 0, data.Length);
                //if (replay[0] == MessageType.Error)
                //{

                //    throw new Exception(System.Text.Encoding.UTF8.GetString(data));
                //}

                BinaryFormatter bformatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(replay);
                return (DataTable)bformatter.Deserialize(stream);




            }
            catch (Exception ex)
            {

                throw new Exception ("GetData:" + ex.Message);

            }

        }
        public byte []  GetData_ByteArray(string SQLCommand)
        {
            try
            {

                List<byte> order = new List<byte>();
                order.Add(MessageType.GetData_ByteArray);
                order.AddRange(System.Text.Encoding.UTF8.GetBytes(SQLCommand));
                byte[] orderarray = order.ToArray();
                MessageSend messagesend = _OverLoadEndPoint.NewMessageSend(-1);
                //_OverLoadEndPoint.log.addlog(DateTime.Now, "error ms length" + orderarray.Length.ToString());
                messagesend.SendMessage(_OverLoadEndPoint, orderarray);

                MessageReceive MessageReceive_ = _OverLoadEndPoint.GetReplayMessage(messagesend);
                if (MessageReceive_ == null) throw new Exception(" Replay IS Null");

                return  MessageReceive_.GetFullMessage_ByteArray();


            }
            catch (Exception ex)
            {

                throw new Exception("GetData_ByteArray:" + ex.Message);

            }

        }
        public DataTable Execute_Function(byte FunctionCode,DataTable Parameters)
        {
            try
            {
                BinaryFormatter bformatter1 = new BinaryFormatter();
                MemoryStream stream1 = new MemoryStream();
                bformatter1.Serialize(stream1, Parameters);
                List<byte> order = new List<byte>();
                order.Add(MessageType.Execute_Function);
                order.Add(FunctionCode);
                order.AddRange(stream1 .ToArray ());
                byte[] orderarray = order.ToArray();
                MessageSend messagesend = _OverLoadEndPoint.NewMessageSend(-1);
                //_OverLoadEndPoint.log.addlog(DateTime.Now, "error ms length" + orderarray.Length.ToString());
                messagesend.SendMessage(_OverLoadEndPoint, orderarray);

                MessageReceive MessageReceive_ = _OverLoadEndPoint.GetReplayMessage(messagesend);
                if (MessageReceive_ == null) throw new Exception(" Replay IS Null");

                byte[] replay = MessageReceive_.GetFullMessage();
                if (replay == null) throw new Exception(" Replay byte array IS Null");

                //byte[] data = new byte[replay.Length - 1];
                //Array.Copy(replay, 1, data, 0, data.Length);
                //if (replay[0] == MessageType.Error)
                //{

                //    throw new Exception(System.Text.Encoding.UTF8.GetString(data));
                //}

                BinaryFormatter bformatter2 = new BinaryFormatter();
                MemoryStream stream2 = new MemoryStream(replay);
                return (DataTable)bformatter2.Deserialize(stream2);




            }
            catch (Exception ex)
            {

                throw new Exception("GetData:" + ex.Message);

            }

        }
        #region Log
        public void FillComboBoxLogTypes(ref ComboBox ComboBox_)
        {
            ComboBox_.Items.Clear();
            ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
            ComboBox_.Items.Add(new ComboboxItem("تسجيل الدخول", Log.LogType.LOGIN));
            ComboBox_.Items.Add(new ComboboxItem("ادخال بيانات", Log.LogType.INSERT));
            ComboBox_.Items.Add(new ComboboxItem("تعديل بيانات", Log.LogType.UPDATE));
            ComboBox_.Items.Add(new ComboboxItem("حذف بيانات", Log.LogType.DELETE));

        }

        public void FillComboBox_MainTarget(ref ComboBox ComboBox_)
        {
            ComboBox_.Items.Clear();
            ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
            ComboBox_.Items.Add(new ComboboxItem("المستخدمين و تسجيل الدخول", 1));
            ComboBox_.Items.Add(new ComboboxItem("العملة وحركة النقود", 2));
            ComboBox_.Items.Add(new ComboboxItem("الوظائف و الاقسام و الموظفين", 3));
            ComboBox_.Items.Add(new ComboboxItem("الاصناف و العناصر", 4));
            ComboBox_.Items.Add(new ComboboxItem("التفكيك و التجميع", 5));
            ComboBox_.Items.Add(new ComboboxItem("الصيانة و فواتير الصيانة", 6));
            ComboBox_.Items.Add(new ComboboxItem("المبيع و الشراء وجهات الاتصال ", 7));
            ComboBox_.Items.Add(new ComboboxItem("المستودع و عمليات التخزين", 8));
            ComboBox_.Items.Add(new ComboboxItem("حركة المواد", 9));
        }
        public void FillComboBox_SlaveTareget(ref ComboBox ComboBox_, uint MaintType)
        {
            ComboBox_.Items.Clear();
            switch (MaintType)
            {
                case 0:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));

                    break;
                case 1:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));

                    break;
                case 2:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    break;
                case 3:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("الموظفين", 1));
                    ComboBox_.Items.Add(new ComboboxItem("الرواتب و أوامر الصرف", 2));
                    ComboBox_.Items.Add(new ComboboxItem("الوظائف و الأقسام", 3));
                    break;
                case 4:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("الاصناف", 1));
                    ComboBox_.Items.Add(new ComboboxItem("العناصر", 2));
                    ComboBox_.Items.Add(new ComboboxItem("خصائص العناصر و ضبط القيم", 3));
                    break;
                case 5:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    break;
                case 6:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("فواتير الصيانة", 1));
                    ComboBox_.Items.Add(new ComboboxItem("عمليات الصيانة", 2));
                    ComboBox_.Items.Add(new ComboboxItem("عمليات الفحص و الاعطال", 3));
                    break;
                case 7:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    break;
                case 8:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    break;
                case 9:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    break;

            }

        }
        public void FillComboBox_Targets(ref ComboBox ComboBox_, uint MaintType, uint SlaveType)
        {
            ComboBox_.Items.Clear();
            switch (MaintType)
            {
                case 0:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    break;
                case 1:
                    ComboBox_.Items.Add(new ComboboxItem("المستخدمين", Log.Log_Target.User));

                    break;
                case 2:

                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("العملة", Log.Log_Target.Currency));
                    ComboBox_.Items.Add(new ComboboxItem("عمليات الصرف", Log.Log_Target.ExchangeOPR));
                    ComboBox_.Items.Add(new ComboboxItem("الدفعات الواردة", Log.Log_Target.PayIN));
                    ComboBox_.Items.Add(new ComboboxItem("الدفعات الخارجة", Log.Log_Target.PayOUT));
                    break;

                case 3:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    switch (SlaveType)
                    {
                        case 1:
                            ComboBox_.Items.Add(new ComboboxItem("الوثائق", Log.Log_Target.Document));
                            ComboBox_.Items.Add(new ComboboxItem("الموظفين", Log.Log_Target.Employee));
                            ComboBox_.Items.Add(new ComboboxItem("الشهادات", Log.Log_Target.Employee_Certificate));
                            ComboBox_.Items.Add(new ComboboxItem("المؤهلات", Log.Log_Target.Employee_Qualification));
                            ComboBox_.Items.Add(new ComboboxItem("الصور", Log.Log_Target.Employee_Image));
                            break;
                        case 2:
                            ComboBox_.Items.Add(new ComboboxItem("بنود الراتب", Log.Log_Target.Employee_SalaryClause));
                            ComboBox_.Items.Add(new ComboboxItem("اوامر الصرف", Log.Log_Target.Employee_PayOrder));
                            ComboBox_.Items.Add(new ComboboxItem("صرف رواتب", Log.Log_Target.Salary_PayOrder));

                            break;
                        case 3:
                            ComboBox_.Items.Add(new ComboboxItem("الوظائف", Log.Log_Target.Employeement));
                            ComboBox_.Items.Add(new ComboboxItem("المستويات الوظيفية", Log.Log_Target.Employeement_Level));
                            ComboBox_.Items.Add(new ComboboxItem("الاقسام", Log.Log_Target.CompanyPart));
                            break;
                    }

                    break;

                case 4:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    switch (SlaveType)
                    {
                        case 1:
                            ComboBox_.Items.Add(new ComboboxItem("الاصناف", Log.Log_Target.Item_Folder));
                            break;
                        case 2:
                            ComboBox_.Items.Add(new ComboboxItem("العناصر", Log.Log_Target.Item_Item));
                            ComboBox_.Items.Add(new ComboboxItem("وحدات التوزيع العناصر", Log.Log_Target.Item_ConsumeUint));
                            ComboBox_.Items.Add(new ComboboxItem("الاسعار الدارجة", Log.Log_Target.Item_SellPrice));
                            ComboBox_.Items.Add(new ComboboxItem("الصور", Log.Log_Target.Item_Image));
                            ComboBox_.Items.Add(new ComboboxItem("الملفات", Log.Log_Target.Item_File));
                            ComboBox_.Items.Add(new ComboboxItem("العلاقات", Log.Log_Target.Item_RealtionShip));
                            break;
                        case 3:
                            ComboBox_.Items.Add(new ComboboxItem("الخصائص غير المقيدة", Log.Log_Target.Item_Spec));
                            ComboBox_.Items.Add(new ComboboxItem("الخصائص المقيدة", Log.Log_Target.Item_Spec_Restrict));
                            ComboBox_.Items.Add(new ComboboxItem("خيارات الخصائص المقيدة", Log.Log_Target.Item_Spec_Restrict_Option));
                            ComboBox_.Items.Add(new ComboboxItem("قيم الخصائص غير المقيدة", Log.Log_Target.Item_Spec_Value));
                            ComboBox_.Items.Add(new ComboboxItem("قيم الخصائص  المقيدة", Log.Log_Target.Item_Spec_Restrict_Value));

                            break;

                    }
                    break;
                    ;
                case 5:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("التجميع", Log.Log_Target.Trade_Assemblage));
                    ComboBox_.Items.Add(new ComboboxItem("التفكيك", Log.Log_Target.Trade_DisAssemblage));
                    break;


                case 6:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    switch (SlaveType)
                    {
                        case 1:
                            ComboBox_.Items.Add(new ComboboxItem("فواتير الصيانة", Log.Log_Target.Item_Folder));
                            ComboBox_.Items.Add(new ComboboxItem("فواتير الصيانة-بند عملية فحص", Log.Log_Target.Item_Folder));
                            ComboBox_.Items.Add(new ComboboxItem("فواتير الصيانة-بند عملية اصلاح", Log.Log_Target.Item_Folder));

                            break;
                        case 2:
                            ComboBox_.Items.Add(new ComboboxItem("عمليات الصيانة", Log.Log_Target.Item_Folder));
                            ComboBox_.Items.Add(new ComboboxItem("ملحقات الصيانة", Log.Log_Target.Item_Folder));

                            break;
                        case 3:
                            ComboBox_.Items.Add(new ComboboxItem("عمليات الفحص", Log.Log_Target.Item_Spec));
                            ComboBox_.Items.Add(new ComboboxItem("ملف عملية فحص", Log.Log_Target.Item_Spec_Restrict));
                            ComboBox_.Items.Add(new ComboboxItem("عمليات القياس", Log.Log_Target.Item_Spec_Restrict_Option));
                            ComboBox_.Items.Add(new ComboboxItem("العناصر المفقودة و التالفة", Log.Log_Target.Item_Spec_Value));
                            ComboBox_.Items.Add(new ComboboxItem("الاعطال", Log.Log_Target.Item_Spec_Restrict_Value));
                            ComboBox_.Items.Add(new ComboboxItem("انتهاء العمل في عملية الصيانة", Log.Log_Target.Item_Spec_Value));
                            ComboBox_.Items.Add(new ComboboxItem(" عمليات الاصلاح", Log.Log_Target.Item_Spec_Restrict_Value));
                            ComboBox_.Items.Add(new ComboboxItem(" عمليات الربط", Log.Log_Target.Item_Spec_Restrict_Value));

                            break;

                    }
                    break;

                case 7:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("الجهات", Log.Log_Target.Trade_Contact));
                    ComboBox_.Items.Add(new ComboboxItem("فواتير الشراء", Log.Log_Target.Trade_BillBuy));
                    ComboBox_.Items.Add(new ComboboxItem("فواتير المبيع", Log.Log_Target.Trade_BillSell));
                    ComboBox_.Items.Add(new ComboboxItem("اتلاف", Log.Log_Target.Trade_RavageOPR));
                    ComboBox_.Items.Add(new ComboboxItem("حالة البيع و الشراء", Log.Log_Target.Item_TradeState));
                    ComboBox_.Items.Add(new ComboboxItem("انماط البيع", Log.Log_Target.Trade_SellTypes));
                    break;
                //            //in out item

                case 8:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("ادخال عناصر", Log.Log_Target.Trade_ItemIN));
                    ComboBox_.Items.Add(new ComboboxItem("اخراج عناصر", Log.Log_Target.Trade_ItemOut));
                    ComboBox_.Items.Add(new ComboboxItem("ضبط اسعار العناصر الداخلة", Log.Log_Target.Trade_ItemIN_SellPrice));
                    break;
                //             public const uint Trade_ItemsStore = 801;
                //public const uint Trade_Store_Container = 802;
                //public const uint Trade_Store_Place = 803;
                case 9:
                    ComboBox_.Items.Add(new ComboboxItem("اظهار الكل", 0));
                    ComboBox_.Items.Add(new ComboboxItem("حاويات اماكن التخزين", Log.Log_Target.Trade_Store_Container));
                    ComboBox_.Items.Add(new ComboboxItem("اماكن التخزين", Log.Log_Target.Trade_Store_Place));
                    ComboBox_.Items.Add(new ComboboxItem("تخزين المواد", Log.Log_Target.Trade_ItemsStore));
                    break;

            }

        }

        public class Log
        {
            public static class LogType
            {
                public const uint LOGIN = 1;
                public const uint INSERT = 2;
                public const uint UPDATE = 3;
                public const uint DELETE = 4;
            }
            public static string GetLogType_Name(uint logtype)
            {
                switch (logtype)
                {
                    case LogType.LOGIN:
                        return "تسجيل دخول";
                    case LogType.INSERT:
                        return "ادخال";
                    case LogType.UPDATE:
                        return "تعديل";
                    case LogType.DELETE:
                        return "حذف";
                    default:
                        return "----";
                }
            }
            public static string GetLogTarget_Name(uint logtarget)
            {
                switch (logtarget)
                {
                    case Log_Target.User: return "المستخدمين";
                    //Money
                    case Log_Target.Currency: return "العملات";
                    case Log_Target.ExchangeOPR: return "عمليات الصرف";
                    case Log_Target.PayIN: return "الدفعات الواردة";
                    case Log_Target.PayOUT: return "الدفعات الصادرة";

                    //Company

                    //Employee
                    case Log_Target.Document: return "وثائق موظفين";
                    case Log_Target.Employee: return "الموظفين";
                    case Log_Target.Employee_Certificate: return "شهادات الموظفين";
                    case Log_Target.Employee_Image: return "صور الموظفين";
                    case Log_Target.Employee_Qualification: return "مؤهلات الموظفين";
                    case Log_Target.Employee_SalaryClause: return "بنود راتب موظف";
                    //Salary And Payorder
                    case Log_Target.Employee_PayOrder: return "اوامر الصرف";
                    case Log_Target.Salary_PayOrder: return "اوامر صرف راتب";
                    //Part and EmployeeMent
                    case Log_Target.Employeement: return "الوظائف";
                    case Log_Target.Employeement_Level: return "المستويات الوظيفية";
                    case Log_Target.CompanyPart: return "الاقسام";


                    //Item
                    case Log_Target.Item_Folder: return "اصناف العناصر";
                    //
                    case Log_Target.Item_ConsumeUint: return "وحدات توزيع العناصر";
                    case Log_Target.Item_Item: return "العناصر";
                    case Log_Target.Item_File: return "ملفات العناصر";
                    case Log_Target.Item_Image: return "صورة عنصر";
                    case Log_Target.Item_RealtionShip: return "العلاقات بين العناصر";
                    case Log_Target.Item_SellPrice: return "اسعار العناصر الدارجة";
                    //
                    case Log_Target.Item_Spec: return "خصائص العناصر الغير مقيدة";
                    case Log_Target.Item_Spec_Value: return "قيمة خاصية غير مقيدة";
                    case Log_Target.Item_Spec_Restrict: return "خصائص العناصر المقيدة";
                    case Log_Target.Item_Spec_Restrict_Option: return "خيارات العناصر المقيدة";
                    case Log_Target.Item_Spec_Restrict_Value: return "قيمة خاصية مقيدة";
                    case Log_Target.Item_Equivalence_Group: return "مجموعات المكافئات";
                    case Log_Target.Item_Equivalence_Relation: return "ضبط مكافئات العنصر";


                    //INdustry
                    case Log_Target.Trade_Assemblage: return "تجميع";
                    case Log_Target.Trade_DisAssemblage: return "تفكيك";

                    //Mainteance
                    case Log_Target.Trade_BillMaintenenace: return "فاتورة صيانة";
                    case Log_Target.Trade_BillMaintenenace_Clause_DiagnosticOPR: return "بند فحص-فاتورة صيانة";
                    case Log_Target.Trade_BillMaintenenace_Clause_RepairOPR: return "بند اصلاح- فاتورة صيانة";
                    //
                    case Log_Target.Maintenenace_MaintenenaceOPR: return "عملية صيانة";
                    case Log_Target.Maintenenace_Accessory: return "ملحق صيانة";

                    case Log_Target.Maintenenace_DiagnosticOPR: return "عملية فحص";
                    case Log_Target.Maintenenace_DiagnosticOPR_File: return "ملف عملية فحص";
                    case Log_Target.Maintenenace_DiagnosticOPR_MeasureOPR: return "عملية قياس";
                    case Log_Target.Maintenenace_DiagnosticOPR_MissedFaultItem: return "عنصر تالف او مفقود";
                    case Log_Target.Maintenenace_EndWork: return "عملية صيانة-انتهاء العمل";
                    case Log_Target.Maintenenace_Fault: return "عملية صيانة-عطل";
                    case Log_Target.Maintenenace_Fault_RepairOPR: return "عملية اصلاح مساهمة في اصلاح عطل";
                    case Log_Target.Maintenenace_Tag: return "عملية صيانة -رابط";
                    case Log_Target.Maintenenace_RepairOPR: return "عملية اصلاح";

                    //Trade
                    case Log_Target.Trade_Contact: return "جهات التعامل";
                    case Log_Target.Trade_BillBuy: return "فاتورة شراء";
                    case Log_Target.Trade_BillSell: return "فاتورة مبيعات";
                    case Log_Target.Trade_RavageOPR: return "عملية اتلاف";


                    case Log_Target.Item_TradeState: return "حالة البيع و الشراء لعنصر";
                    case Log_Target.Trade_SellTypes: return "انماط البيع";

                    //in out item
                    case Log_Target.Trade_ItemOut: return "اخراج عناصر";
                    case Log_Target.Trade_ItemIN: return "ادخال عناصر";
                    case Log_Target.Trade_ItemIN_SellPrice: return "اسعار العناصر الداخلة";
                    case Log_Target.Trade_BillAdditionalClause: return "بند فاتورة اضافي";
                    //Store
                    case Log_Target.Trade_ItemsStore: return "تخزين عناصر";
                    case Log_Target.Trade_Store_Container: return "حاويات اماكن التخزين";
                    case Log_Target.Trade_Store_Place: return "اماكن التخزين";
                    default: return "----";
                }
            }
            public static class Log_Target
            {
                //User
                public const uint User = 101;
                //Money
                public const uint Currency = 201;
                public const uint ExchangeOPR = 202;
                public const uint PayIN = 203;
                public const uint PayOUT = 204;
                public const uint MoneyBox = 205;
                public const uint MoneyTransFormOPR = 206;

                //Company

                //Employee
                public const uint Document = 311;
                public const uint Employee = 312;
                public const uint Employee_Certificate = 313;
                public const uint Employee_Image = 314;
                public const uint Employee_Qualification = 315;
                public const uint Employee_SalaryClause = 316;
                //Salary And Payorder
                public const uint Employee_PayOrder = 321;
                public const uint Salary_PayOrder = 322;
                //Part and EmployeeMent
                public const uint Employeement = 331;
                public const uint Employeement_Level = 332;
                public const uint CompanyPart = 333;


                //Item
                public const uint Item_Folder = 411;
                //
                public const uint Item_ConsumeUint = 421;
                public const uint Item_Item = 422;
                public const uint Item_File = 423;
                public const uint Item_Image = 424;
                public const uint Item_RealtionShip = 425;
                public const uint Item_SellPrice = 426;
                //
                public const uint Item_Spec = 431;
                public const uint Item_Spec_Value = 432;
                public const uint Item_Spec_Restrict = 433;
                public const uint Item_Spec_Restrict_Option = 434;
                public const uint Item_Spec_Restrict_Value = 435;
                public const uint Item_Equivalence_Group = 436;
                public const uint Item_Equivalence_Relation = 437;


                //INdustry
                public const uint Trade_Assemblage = 501;
                public const uint Trade_DisAssemblage = 502;

                //Mainteance
                public const uint Trade_BillMaintenenace = 611;
                public const uint Trade_BillMaintenenace_Clause_DiagnosticOPR = 612;
                public const uint Trade_BillMaintenenace_Clause_RepairOPR = 613;
                //
                public const uint Maintenenace_MaintenenaceOPR = 621;
                public const uint Maintenenace_Accessory = 622;

                public const uint Maintenenace_DiagnosticOPR = 631;
                public const uint Maintenenace_DiagnosticOPR_File = 632;
                public const uint Maintenenace_DiagnosticOPR_MeasureOPR = 633;
                public const uint Maintenenace_DiagnosticOPR_MissedFaultItem = 634;
                public const uint Maintenenace_EndWork = 635;
                public const uint Maintenenace_Fault = 636;
                public const uint Maintenenace_Fault_RepairOPR = 637;
                public const uint Maintenenace_Tag = 638;
                public const uint Maintenenace_RepairOPR = 639;

                //Trade
                public const uint Trade_Contact = 701;
                public const uint Trade_BillBuy = 702;
                public const uint Trade_BillSell = 703;
                public const uint Trade_RavageOPR = 704;


                public const uint Item_TradeState = 708;
                public const uint Trade_SellTypes = 709;

                //in out item

                public const uint Trade_ItemOut = 901;
                public const uint Trade_ItemIN = 902;
                public const uint Trade_ItemIN_SellPrice = 903;
                public const uint Trade_BillAdditionalClause = 904;
                //Store
                public const uint Trade_ItemsStore = 801;
                public const uint Trade_Store_Container = 802;
                public const uint Trade_Store_Place = 803;

            }


            public uint LogID;
            public DateTime LogDateTime;
            public uint _LogType;
            public uint _LogTarget;
            public string LogDesc;
            public string EmployeeName;
            public bool Success;
            internal string ErrorMessage;

            public Log(uint LogID_,
                  DateTime LogDateTime_,
                    uint LogType_,
             uint LogTarget_,
            string LogDesc_,
             string EmployeeName_,
             bool Success_,
             string ErrorMessage_)
            {
                LogID = LogID_;
                _LogType = LogType_;
                _LogTarget = LogTarget_;
                LogDesc = LogDesc_;
                LogDateTime = LogDateTime_;
                EmployeeName = EmployeeName_;
                Success = Success_;
                ErrorMessage = ErrorMessage_;
            }
        }
        private static class LogTable
        {
            public const string TableName = "OverLoad_Log";
            public const string LogID = "LogID";
            public const string LogDateTime = "LogDateTime";
            public const string LogType = "LogType";
            public const string LogTarget = "LogTarget";
            public const string LogDesc = "LogDesc";
            public const string EmployeeName = "EmployeeName";

            public const string Success = "Success";
            public const string ErrorMessage = "ErrorMessage";
        }
        internal async void AddLog(uint LogType, uint LogTarget, string LogDesc, bool Success, string ErrorMessage)
        {

            ErrorMessage = ErrorMessage.Replace("'", string.Empty);
            //if (ErrorMessage.Length > 37)
            //    ErrorMessage=ErrorMessage.Substring(37);

            try
            {
                //ShowSqlQuery dd = new ShowSqlQuery(" insert into "
                //+ LogTable.TableName
                //+ "("
                //+ LogTable.LogType
                //+ ","
                // + LogTable.LogTarget
                //+ ","
                //+ LogTable.LogDesc
                //+ ","
                //+ LogTable.EmployeeName
                //+ ","
                //+ LogTable.Success
                // + ","
                //+ LogTable.ErrorMessage
                //+ ")"
                //+ "values"
                //+ "("
                //+ LogType
                //+ ","
                //+ LogTarget
                //+ ","
                //+ "'" + LogDesc + "'"
                //+ ","
                // + (this._User == null ? "''" : (this._User._Employee == null ? "'مدير النظام'" :
                // "'" + this._User._Employee.EmployeeName + "'"))
                //+ ","
                //  + (Success == true ? "1" : "0")
                //+ ","
                //+ "'" + ErrorMessage+ "',hh"
                //+ ")");
                //dd.ShowDialog();
                ExecuteSQLCommand(" insert into "
                + LogTable.TableName
                + "("
                + LogTable.LogType
                + ","
                 + LogTable.LogTarget
                + ","
                + LogTable.LogDesc
                + ","
                + LogTable.EmployeeName
                + ","
                + LogTable.Success
                 + ","
                + LogTable.ErrorMessage
                + ")"
                + "values"
                + "("
                + LogType
                + ","
                + LogTarget
                + ","
                + "'" + LogDesc + "'"
                + ","
                 + (this._User == null ? "''" : (this._User._Employee == null ? "'مدير النظام'" :
                 "'" + this._User._Employee.EmployeeName + "'"))
                + ","
                  + (Success == true ? "1" : "0")
                + ","
                + "'" + ErrorMessage + "'"
                + ")"
                );
                //if(DATABASE_SQL_COMMAND==null ) MessageBox.Show("hh");
                // //ShowSqlQuery dd = new ShowSqlQuery(SQLCommand); dd.ShowDialog();
                // DATABASE_SQL_COMMAND.CommandText = SQLCommand;

                // if (DATABASE_CONNECTION.State != ConnectionState.Open) /*DATABASE_CONNECTION.Close();*/
                //     DATABASE_CONNECTION.Open();
                // DATABASE_SQL_COMMAND.ExecuteNonQuery();


            }
            catch (Exception ee)
            {
                //DataBaseLocked = true;
                //throw new Exception(ee.Message);

            }
        }
        public void ClearLog()
        {
            if (!IS_Belong_To_Admin_Group(__User.UserID)) throw new Exception("لا تملك الصلاحية لتنفيذ هذا الاجراء");

            ExecuteSQLCommand(" delete  from  "
               + LogTable.TableName
               );
        }
        public List<Log> GetLogList()
        {
            List<Log> list = new List<Log>();
            try
            {
                DataTable t = this.GetData("select "
                    + LogTable.LogID + ","
                    + LogTable.LogDateTime + ","
                    + LogTable.LogType + ","
                    + LogTable.LogTarget + ","
                    + LogTable.LogDesc + ","
                    + LogTable.EmployeeName + ","
                    + LogTable.Success + ","
                    + LogTable.ErrorMessage
                    + " from "
                    + LogTable.TableName
                    + " order by "
                    + LogTable.LogDateTime
                    + " desc "
                    );
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    uint logid = Convert.ToUInt32(t.Rows[i][0]);
                    DateTime logdaetime = Convert.ToDateTime(t.Rows[i][1]);
                    uint logtype = Convert.ToUInt32(t.Rows[i][2]);
                    uint logtarget = Convert.ToUInt32(t.Rows[i][3]);
                    string desc = t.Rows[i][4].ToString();
                    string employee_name = t.Rows[i][5].ToString();
                    bool success = Convert.ToBoolean(t.Rows[i][6]);
                    string errormesage = t.Rows[i][7].ToString();
                    list.Add(new Log(logid, logdaetime, logtype, logtarget, desc, employee_name, success, errormesage));
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("GetLogList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }
        #endregion
        #region User
        public class User
        {
            public uint UserID;
            public string UserName;
            public DateTime adddate;
            public bool Disabled;
            internal Employee _Employee;
            internal List<AccessFolderPremession> AccessFolderPremessionList;
            internal List<AccessContainerPremession> AccessContainerPremessionList;
            internal List<AccessSellTypePremession> AccessSellTypePremessionList;
            public User(uint UserID_, string UserName_, DateTime adddate_, bool Disabled_, Employee Employee_
                , List<AccessFolderPremession> AccessFolderPremessionList_, List<AccessContainerPremession> AccessContainerPremessionList_
                , List<AccessSellTypePremession> AccessSellTypePremessionList_)
            {
                UserID = UserID_;
                _Employee = Employee_;
                UserName = UserName_;
                adddate = adddate_;
                Disabled = Disabled_;
                AccessFolderPremessionList = AccessFolderPremessionList_;
                AccessContainerPremessionList = AccessContainerPremessionList_;
                AccessSellTypePremessionList = AccessSellTypePremessionList_;
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

                    return new User(userid, username, adddate, disabled, Employee_,
                        Get_AccessFolderPremession_List(userid), Get_AccessContainerPremession_List(userid)
                        , Get_AccessSellTypePremession_List(userid));
                }
                else
                    return null;
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetEmployeeUser:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public User GetUser_BY_ID(uint userid)
        {
            try
            {
                List<AccessFolderPremession> AccessFolderPremessionList = new List<AccessFolderPremession>();
                List<AccessContainerPremession> AccessContainerPremessionList = new List<AccessContainerPremession>();
                List<AccessSellTypePremession> AccessSellTypePremessionList = new List<AccessSellTypePremession>();
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
                        AccessFolderPremessionList = Get_AccessFolderPremession_List(userid);
                        AccessContainerPremessionList = Get_AccessContainerPremession_List(userid);
                        AccessSellTypePremessionList = Get_AccessSellTypePremession_List(userid);
                        employee = new Company.CompanySQL.EmployeeSQL(this).GetEmployeeInforBYID(Convert.ToUInt32(t.Rows[0][4].ToString()));
                    }
                    catch
                    {

                        AccessFolderPremessionList.Add(new AccessFolderPremession(0, userid, null));
                        AccessContainerPremessionList.Add(new AccessContainerPremession(0, userid, null));

                        List<SellType> selltypelist = new Trade.TradeSQL.SellTypeSql(this).GetSellTypeList();
                        for (int i = 0; i < selltypelist.Count; i++)
                            AccessSellTypePremessionList.Add(new AccessSellTypePremession(userid, selltypelist[i], true));

                        employee = null;
                    }
                    return new User(userid, username, adddate, disabled, employee, AccessFolderPremessionList,
                        AccessContainerPremessionList, AccessSellTypePremessionList);
                }
                else
                    return null;
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetUser_By_ID:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private User Get_Administrator_User()
        {
            try
            {
                List<AccessFolderPremession> AccessFolderPremessionList = new List<AccessFolderPremession>();
                List<AccessContainerPremession> AccessContainerPremessionList = new List<AccessContainerPremession>();
                List<AccessSellTypePremession> AccessSellTypePremessionList = new List<AccessSellTypePremession>();
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
                    AccessFolderPremessionList.Add(new AccessFolderPremession(0, UserID, null));
                    AccessContainerPremessionList.Add(new AccessContainerPremession(0, UserID, null));

                    List<SellType> selltypelist = new Trade.TradeSQL.SellTypeSql(this).GetSellTypeList();
                    for (int i = 0; i < selltypelist.Count; i++)
                        AccessSellTypePremessionList.Add(new AccessSellTypePremession(UserID, selltypelist[i], true));

                    return new User(UserID, username, adddate, disabled, null, AccessFolderPremessionList,
                        AccessContainerPremessionList, AccessSellTypePremessionList);
                }
                else
                    return null;
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetUser_By_ID:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public bool AddUser(uint EmployeeID, string UserName, string Password, bool Disabled)
        {
            try
            {
                if (UserName == "Administrator") throw new Exception("يرجى اختيار اسم مستخدم غير Administrator ");
                this.ExecuteSQLCommand(
                    " insert into "
                + UserTable.TableName
                + "("
                + UserTable.OV_UserName
                + ","
                + UserTable.OV_Password
                  + ","
                + UserTable.Disabled_
                + ","
                + UserTable.EmployeeID

                + ")"
                + "values"
                + "("
                + "'" + UserName + "'"
                + ","
                  + "'" + Password + "'"
               + ","
                + (Disabled == true ? "1" : "0")
                  + ","
                + EmployeeID
                + ")"
                );
                AddLog(Log.LogType.INSERT
                    , Log.Log_Target.User
                    , UserName
                    , true
                    , "");

                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddUser" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog(Log.LogType.INSERT
                   , Log.Log_Target.User
                   , UserName
                   , false
                   , ee.Message);
                return false;
            }

        }
        public bool EditUserData(uint EmployeeID, string UserName, bool Disable)
        {
            try
            {
                this.ExecuteSQLCommand("update  "
                + UserTable.TableName
                + " set "
                + UserTable.OV_UserName + "='" + UserName + "',"
                  + UserTable.Disabled_ + "=" + (Disable ? "1" : "0")
                + " where "
                + UserTable.EmployeeID + "=" + EmployeeID
                );
                AddLog(Log.LogType.UPDATE
                   , Log.Log_Target.User
                   , "تعديل اسم المستخدم للموظف رقم:" + EmployeeID.ToString()
                   , true
                   , "");
                return true;
            }
            catch (Exception ee)
            {
                AddLog(Log.LogType.UPDATE
                  , Log.Log_Target.User
                  , "تعديل اسم المستخدم للموظف رقم:" + EmployeeID.ToString()
                  , false
                  , ee.Message);
                MessageBox.Show("UpdateUserName" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public bool DeleteUser(uint EmployeeID)
        {
            try
            {
                this.ExecuteSQLCommand("delete from   "
                + UserTable.TableName
                + " where "
                + UserTable.EmployeeID + "=" + EmployeeID
                );
                AddLog(Log.LogType.DELETE
                  , Log.Log_Target.User
                  , "حذف  المستخدم للموظف رقم:" + EmployeeID.ToString()
                  , true
                  , "");
                return true;
            }
            catch (Exception ee)
            {
                AddLog(Log.LogType.DELETE
                  , Log.Log_Target.User
                  , "حذف اسم المستخدم للموظف رقم:" + EmployeeID.ToString()
                  , true
                  , ee.Message);
                MessageBox.Show("DeleteUser", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public bool DisableUser(uint EmployeeID)
        {
            try
            {
                this.ExecuteSQLCommand("update  "
                + UserTable.TableName
                + " set "
                + UserTable.Disabled_ + "=1"
                + " where "
                + UserTable.EmployeeID + "=" + EmployeeID
                );
                AddLog(Log.LogType.UPDATE
                    , Log.Log_Target.User
                    , "تعطيل حساب الموظف رقم::" + EmployeeID.ToString()
                    , true, "");
                return true;
            }
            catch (Exception ee)
            {
                AddLog(Log.LogType.UPDATE
                   , Log.Log_Target.User
                   , "تعطيل حساب الموظف رقم::" + EmployeeID.ToString()
                   , false, ee.Message);
                MessageBox.Show("DisableUser" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public bool ResetUserPassword(uint EmployeeID, string NewPassword)
        {
            try
            {
                this.ExecuteSQLCommand("update  "
                + UserTable.TableName
                + " set "
                + UserTable.OV_Password + "='" + NewPassword + "'"
                + " where "
                + UserTable.EmployeeID + "=" + EmployeeID
                );
                AddLog(Log.LogType.UPDATE
                    , Log.Log_Target.User
                    , "اعادة تعيين كلمة المرور للموظف رقم:" + EmployeeID.ToString()
                    , true, "");
                return true;
            }
            catch (Exception ee)
            {
                AddLog(Log.LogType.UPDATE
                    , Log.Log_Target.User
                    , "اعادة تعيين كلمة المرور للموظف رقم:" + EmployeeID.ToString()
                    , false, ee.Message);
                MessageBox.Show("ResetUserPassword" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public bool UpdateMYPassword(string oldPassword, string NewPassword)
        {
            try
            {
                DataTable t2 = GetData(" select *  from "
                      + UserTable.TableName
                     + " where "
                      + UserTable.OV_UserID  + "=" + this._User.UserID 
                      + " and "
                      + UserTable.OV_Password + "='" + oldPassword + "'"
                     );
                if (t2.Rows.Count == 1)
                {
                    this.ExecuteSQLCommand("update  "
                   + UserTable.TableName
                   + " set "
                   + UserTable.OV_Password + "='" + NewPassword + "'"
                   + " where "
                  + UserTable.OV_UserID + "=" + this._User.UserID
                   );
                    AddLog(Log.LogType.UPDATE
                    , Log.Log_Target.User
                    , "تعديل كلمة المرور:" + this._User.UserName
                       , true, "");
                    return true;
                }
                else throw new Exception("كلمة المرور غير صحيحة");
            }
            catch (Exception ee)
            {
                AddLog(Log.LogType.UPDATE
                   , Log.Log_Target.User
                   , "تعديل كلمة المرور:" + this._User.UserName
                      , false, ee.Message);
                throw new Exception(ee.Message);
            }

        }

        internal void LogOut()
        {
            this._User = null;
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
            public ItemObj.Objects.Folder _Folder;
            public AccessFolderPremession(uint PermissionID_,
             uint UserID_,
             ItemObj.Objects.Folder Folder_)
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
            User user = GetUser_BY_ID(userid);
            if (user.UserName == "Administrator") return true;
            try
            {
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
                throw new Exception(ee.Message);
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
                MessageBox.Show("GetUserPremessions:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }
        internal bool IS_Belong_To_MoneyBox_Group(uint usedid, AccountingObj.Objects.MoneyBox MoneyBox_)
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
                List<MoneyBox> moneyboxlist = new AccountingObj.AccountingSQL.MoneyBoxSQL(this).GetMoneyBox_List();
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
                MessageBox.Show("Get_MoneyBoxPremession_List:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                List<SellType> SellTypelist = new Trade.TradeSQL.SellTypeSql(this).GetSellTypeList();

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
                MessageBox.Show("Get_AccessSellTypePremession_List:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        Container_ = new Trade.TradeSQL.TradeStoreContainerSQL(this).GetContainerBYID(containerid);
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
                MessageBox.Show("Get_AccessContainerPremession_List:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }
        internal bool IS_Belong_To_AccessFolderPremession_Group(uint usedid, ItemObj.Objects.Folder Folder_)
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
            List<AccessFolderPremession> list = new List<AccessFolderPremession>();

            try
            {
                DataTable t1 = GetData(" select  "
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
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    uint premessionid = Convert.ToUInt32(t1.Rows[i][0].ToString());
                    ItemObj.Objects.Folder Folder_;
                    try
                    {
                        uint folderid = Convert.ToUInt32(t1.Rows[i][1].ToString());
                        Folder_ = new ItemObj.ItemObjSQL.FolderSQL(this).GetFolderInfoByID(folderid);
                    }
                    catch
                    {
                        Folder_ = null;
                    }



                    list.Add(new AccessFolderPremession(premessionid, userid, Folder_));
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("Get_AccessContainerPremession_List:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }

        #endregion
        public List<MoneyBox> Get_User_Allowed_MoneyBox()
        {
            List<MoneyBox> list = new List<MoneyBox>();
            try
            {
                DataTable t1 = new DataTable();
                if (IS_Belong_To_Admin_Group(_User.UserID))
                {

                    list = new AccountingObj.AccountingSQL.MoneyBoxSQL(this).GetMoneyBox_List();
                    if (list.Count == 0) throw new Exception("لا يوجد اي صندوق مال منشىء");
                    else return list;
                }
                else
                {
                    t1 = GetData(" select  "
                        + MoneyBoxGroup_Table.MoneyBoxID
                        + " from "
                                       + MoneyBoxGroup_Table.TableName
                                       + " where "
                                       + MoneyBoxGroup_Table.OV_UserID + "=" + _User.UserID
                                       );
                }
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    list.Add(new AccountingObj.AccountingSQL.MoneyBoxSQL(this).GetMoneyBoxBYID
                        (Convert.ToUInt32(t1.Rows[i][0])));
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Get_User_Allowed_MoneyBox:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }
        public void FillComboBox_MoneyBox(ref System.Windows.Forms.ComboBox comboBoxMoneyBox, MoneyBox MoneyBox_)
        {
            List<MoneyBox> MoneyBoxList = Get_User_Allowed_MoneyBox();
            if (MoneyBoxList.Count == 0) throw new Exception("ليس لديك اي صندوق مال تمتلك صلاحية الوصول اليه");
            if (MoneyBox_ == null) MoneyBox_ = MoneyBoxList[0];
            comboBoxMoneyBox.Items.Clear();
            int selected_index = 0;

            for (int i = 0; i < MoneyBoxList.Count; i++)
            {
                ComboboxItem item = new ComboboxItem(MoneyBoxList[i].BoxName, MoneyBoxList[i].BoxID);
                comboBoxMoneyBox.Items.Add(item);
                if (MoneyBox_ != null && MoneyBox_.BoxID == MoneyBoxList[i].BoxID)
                    selected_index = i;
            }
            comboBoxMoneyBox.SelectedIndex = selected_index;


        }
        #region Give_remove_Premession

        internal bool Add_To_Admin_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Admin_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة المدراء");
                if (this._User.UserID != Get_Administrator_User().UserID) throw new Exception("فقط حساب Administrator يستطيع الضم الى مجموعة المدراء");

                ExecuteSQLCommand(" insert into "
                    + AdminGroup_Table.TableName
                    + "("
                    + AdminGroup_Table.OV_UserID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Admin_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Admin_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة المدراء");
                if (this._User.UserID != Get_Administrator_User().UserID) throw new Exception("فقط حساب Administrator يستطيع الحذف من مجموعة المدراء");

                ExecuteSQLCommand(" delete from "
                    + AdminGroup_Table.TableName
                    + " where "
                    + AdminGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Buy_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Buy_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة المشتريات");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + BuyGroup_Table.TableName
                    + "("
                    + BuyGroup_Table.OV_UserID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Buy_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Buy_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة المشتريات");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + BuyGroup_Table.TableName
                    + " where "
                    + BuyGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Sell_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Sell_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة المبيعات");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + SellGroup_Table.TableName
                    + "("
                    + SellGroup_Table.OV_UserID

                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Sell_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Sell_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة المبيعات");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + SellGroup_Table.TableName
                    + " where "
                    + SellGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Maintenance_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Maintenance_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة الصيانة");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + MaintenanceGroup_Table.TableName
                    + "("
                    + MaintenanceGroup_Table.OV_UserID

                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Maintenance_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Maintenance_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة الصيانة");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + MaintenanceGroup_Table.TableName
                    + " where "
                    + MaintenanceGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Employee_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Employee_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة ادارة الموظفين");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + EmployeeGroup_Table.TableName
                    + "("
                    + EmployeeGroup_Table.OV_UserID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Employee_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Employee_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة ادارة الموظفين");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + EmployeeGroup_Table.TableName
                    + " where "
                    + EmployeeGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Item_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Item_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة ادارة العناصر");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + ItemGroup_Table.TableName
                    + "("
                    + ItemGroup_Table.OV_UserID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Item_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Item_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة ادارة العناصر");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + ItemGroup_Table.TableName
                    + " where "
                    + ItemGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Contact_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Contact_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة ادارة الموردين و الزبائن");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + ContactGroup_Table.TableName
                    + "("
                    + ContactGroup_Table.OV_UserID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Contact_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Contact_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة ادارة الموردين و الزبائن");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + ContactGroup_Table.TableName
                    + " where "
                    + ContactGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Industry_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Industry_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة ادارة عمليات التفكيك و التجميع");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + IndustryGroup_Table.TableName
                    + "("
                    + IndustryGroup_Table.OV_UserID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Industry_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Industry_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة ادارة عمليات التفكيك و التجميع");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + IndustryGroup_Table.TableName
                    + " where "
                    + IndustryGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_Container_Group(uint userid)
        {
            try
            {
                if (IS_Belong_To_Container_Group(userid)) throw new Exception("المستخدم موجود مسبقا في مجموعة ادارة المستودع");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + ContainerGroup_Table.TableName
                    + "("
                    + ContainerGroup_Table.OV_UserID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_Container_Group(uint userid)
        {
            try
            {
                if (!IS_Belong_To_Container_Group(userid)) throw new Exception("المستخدم غير موجود  في مجموعة ادارة المستودع");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + ContainerGroup_Table.TableName
                    + " where "
                    + ContainerGroup_Table.OV_UserID + "=" + userid
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Add_To_MoneyBox_Group(uint userid, MoneyBox moneybox)
        {
            try
            {
                if (IS_Belong_To_MoneyBox_Group(userid, moneybox)) throw new Exception("المستخدم موجود مسبقا في مجموعة ادارة الصندوق:" + moneybox.BoxName);
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + MoneyBoxGroup_Table.TableName
                    + "("
                    + MoneyBoxGroup_Table.OV_UserID
                    + ","
                    + MoneyBoxGroup_Table.MoneyBoxID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ","
                    + moneybox.BoxID
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_From_MoneyBox_Group(uint userid, MoneyBox moneybox)
        {
            try
            {
                if (!IS_Belong_To_MoneyBox_Group(userid, moneybox)) throw new Exception("المستخدم غير موجود  في مجموعة ادارة الصندوق:" + moneybox.BoxName);
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + MoneyBoxGroup_Table.TableName
                    + " where "
                    + MoneyBoxGroup_Table.OV_UserID + "=" + userid
                     + " and "
                    + MoneyBoxGroup_Table.MoneyBoxID + "=" + moneybox.BoxID
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        internal bool Add_AccessSellTypePremession(uint userid, SellType SellType_)
        {
            try
            {
                if (IS_Belong_To_AccessSellTypePremession_Group(userid, SellType_)) throw new Exception("المستخدم يمتلك هذه الصلاحية:" + SellType_.SellTypeName);
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + AccessSellType_Table.TableName
                    + "("
                    + AccessSellType_Table.OV_UserID
                    + ","
                    + AccessSellType_Table.SellTypeID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ","
                    + SellType_.SellTypeID
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_AccessSellTypePremession(uint userid, SellType SellType_)
        {
            try
            {
                if (!IS_Belong_To_AccessSellTypePremession_Group(userid, SellType_)) throw new Exception("صلاحية الوصول لنمط البيع  :" + SellType_.SellTypeName + " ملغية مسبقا");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + AccessSellType_Table.TableName
                    + " where "
                    + AccessSellType_Table.OV_UserID + "=" + userid
                     + " and "
                    + AccessSellType_Table.SellTypeID + "=" + SellType_.SellTypeID
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        internal bool Add_AccessFolderPremession(uint userid, ItemObj.Objects.Folder Folder_)
        {
            try
            {
                if (IS_Belong_To_AccessFolderPremession_Group(userid, Folder_)) throw new Exception("المستخدم يمتلك  مسبقا صلاحية الوصول لهذا الصنف:" + Folder_.FolderName);
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + AccessFolder_Table.TableName
                    + "("
                    + AccessFolder_Table.OV_UserID
                    + ","
                    + AccessFolder_Table.FolderID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ","
                    + (Folder_ == null ? "null" : Folder_.FolderID.ToString())
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_AccessFolderPremession(uint userid, ItemObj.Objects.Folder Folder_)
        {
            try
            {
                if (!IS_Belong_To_AccessFolderPremession_Group(userid, Folder_)) throw new Exception("الصلاحية غير ممنوحة لالغائها");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + AccessFolder_Table.TableName
                    + " where "
                    + AccessFolder_Table.OV_UserID + "=" + userid
                     + " and "
                    + AccessFolder_Table.FolderID + (Folder_ == null ? " is null" : "=" + Folder_.FolderID)
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        internal bool Add_AccessContainerPremession(uint userid, TradeStoreContainer Container_)
        {
            try
            {
                if (IS_Belong_To_AccessContainerPremession_Group(userid, Container_)) throw new Exception("المستخدم يمتلك مسبقا صلاحية الوصول لحاوية التخزين هذه:" + Container_.ContainerName);
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" insert into "
                    + AccessContainer_Table.TableName
                    + "("
                    + AccessContainer_Table.OV_UserID
                    + ","
                    + AccessContainer_Table.ContainerID
                    + ")"
                    + "values"
                    + "("
                    + userid
                    + ","
                    + (Container_ == null ? "null" : Container_.ContainerID.ToString())
                    + ")"
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        internal bool Remove_AccessContainerPremession(uint userid, TradeStoreContainer Container_)
        {
            try
            {
                if (!IS_Belong_To_AccessContainerPremession_Group(userid, Container_)) throw new Exception(" صلاحية الوصول ملغية مسبقا");
                if (!IS_Belong_To_Admin_Group(_User.UserID)) throw new Exception("انت لست موجود في جماعة المدراء لذلك لا يمكنك القيام بهذه العملية");

                ExecuteSQLCommand(" delete from "
                    + AccessContainer_Table.TableName
                    + " where "
                    + AccessContainer_Table.OV_UserID + "=" + userid
                     + " and "
                    + AccessContainer_Table.ContainerID + (Container_ == null ? " is null" : "=" + Container_.ContainerID)
                    );
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }


        #endregion
        #endregion


    }

}
