

using OverLoad_Server_Kernal.ItemObjSQL;
using OverLoad_Server_Kernal.MaintenanceSQL;
using OverLoad_Server_Kernal.Objects;
using OverLoad_Server_Kernal.TradeSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OverLoad_Server_Kernal
{
    internal   class OverLoad_SQL_Functions
    {
        private class OverLoad_SQL_Functions_Code
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
        DatabaseInterface DB;
        public OverLoad_SQL_Functions(DatabaseInterface db)
        {
            DB = db;
        }
        public DataTable Execute_Function(byte [] Order)
        {
            try
            {
                byte Code = Order[0];

                byte[] NextOrderData = new byte[Order.Length - 1];
                Array.Copy(Order, 1, NextOrderData, 0, NextOrderData.Length);
                BinaryFormatter bformatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(NextOrderData);
                DataTable Table_Parameter=(DataTable ) bformatter.Deserialize (stream );


                switch (Code )
                {
                    case OverLoad_SQL_Functions_Code.TradeItemStoreSQL_Get_TradeItemStore_Report_List:
                        return TradeItemStore_Report.Get_TradeItemStore_Report_AS_DataTable( new TradeItemStoreSQL(DB).Get_TradeItemStore_Report_List());

                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Buys_ReportDetail:
                        uint contactID1 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_Buys_ReportDetail.Get_Contact_Buys_ReportDetail_List_AS_DataTable (new ContactSQL(DB).Get_Contact_Buys_ReportDetail(contactID1));
                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Buys_Report:
                        uint contactID2 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_Buys_Report.Get_Contact_Buys_Report_AS_DataTable ( new ContactSQL(DB).Get_Contact_Buys_Report(contactID2));

                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Pays_ReportDetail:
                        uint contactID3 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_Pays_ReportDetail.Get_Contact_Contact_Pays_ReportDetail_List_AS_DataTable (new ContactSQL(DB).Get_Contact_Pays_ReportDetail(contactID3));

                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_PayCurrencyReport:
                        uint contactID4 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_PayCurrencyReport.Get_Contact_PayCurrencyReport_AS_DataTable (new ContactSQL(DB).Get_Contact_PayCurrencyReport(contactID4));

                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Sells_ReportDetail:
                        uint contactID5= Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_Sells_ReportDetail.Get_Contact_Sells_ReportDetail_List_AS_DataTable (new ContactSQL(DB).Get_Contact_Sells_ReportDetail(contactID5));

                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Sells_Report:
                        uint contactID6 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_Sells_Report.Get_Contact_Sells_Report_AS_DataTable (new ContactSQL(DB).Get_Contact_Sells_Report(contactID6));


                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Maintenance_ReportDetail:
                        uint contactID7 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_MaintenanceOPRs_ReportDetail.Get_Contact_MaintenanceOPRs_ReportDetail_List_AS_DataTable (new ContactSQL(DB).Get_Contact_MaintenanceOPRs_ReportDetail(contactID7));

                    case OverLoad_SQL_Functions_Code.ContactSQL_Get_Contact_Maintenance_Report:
                        uint contactID8 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_MaintenanceOPRs_Report.Get_Contact_MaintenanceOPRs_Report_List_AS_DataTable (new ContactSQL(DB).Get_Contact_MaintenanceOPRs_Report(contactID8));
                    case OverLoad_SQL_Functions_Code.ContactSQL_Contact_GetBillsReportList:
                        uint contactID9 = Convert.ToUInt32(Table_Parameter.Rows[0][0].ToString());
                        return Contact_BillCurrencyReport.Get_Contact_BillCurrencyReport_AS_DataTable (new DataBaseFunctions (DB).Contact_GetBillsReportList(contactID9));


                    case OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_DayReport:
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return PayCurrencyReport.Get_PayCurrencyReport_List_AS_DataTable(new DataBaseFunctions(DB).Account_GetPays_DayReport(moneybox, Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Account_GetPays_DayReport:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_MonthReport: 
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return PayCurrencyReport.Get_PayCurrencyReport_List_AS_DataTable(new DataBaseFunctions(DB).Account_GetPays_MonthReport(moneybox, Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Account_GetPays_MonthReport:" + ee.Message); }

                    case OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_YearReport:
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return PayCurrencyReport.Get_PayCurrencyReport_List_AS_DataTable(new DataBaseFunctions(DB).Account_GetPays_YearReport(moneybox, Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Account_GetPays_YearReport:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Account_GetPays_YearRangeReport:
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year1 = Convert.ToInt32(Table_Parameter.Rows[0]["Year1"]);
                            int Year2 = Convert.ToInt32(Table_Parameter.Rows[0]["Year2"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return PayCurrencyReport.Get_PayCurrencyReport_List_AS_DataTable(new DataBaseFunctions(DB).Account_GetPays_YearRangeReport(moneybox, Year1,Year2));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Account_GetPays_YearRangeReport:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InDay:
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return AccountOprReportDetail .Get_AccountOprReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).GetAccountOprReport_Details_InDay(moneybox, Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_GetAccountOprReport_Details_InDay:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InMonh:
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return AccountOprDayReportDetail.Get_AccountOprDayReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).GetAccountOprReport_Details_InMonh(moneybox, Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_GetAccountOprReport_Details_InMonh:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InYear:
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return AccountOprMonthReportDetail.Get_AccountOprMonthReportDetail_List_AS_DataTable  (new DataBaseFunctions(DB).GetAccountOprReport_Details_InYear(moneybox, Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_GetAccountOprReport_Details_InYear:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_GetAccountOprReport_Details_InYearRange:
                        try{
                            uint MoneyBoxID = Convert.ToUInt32(Table_Parameter.Rows[0]["MoneyBoxID"]);
                            int Year1 = Convert.ToInt32(Table_Parameter.Rows[0]["Year1"]);
                            int Year2 = Convert.ToInt32(Table_Parameter.Rows[0]["Year2"]);
                            MoneyBox moneybox = new AccountingSQL.MoneyBoxSQL(DB).GetMoneyBoxBYID(MoneyBoxID);
                            return AccountOprYearReportDetail.Get_AccountOprYearReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).GetAccountOprReport_Details_InYearRange(moneybox, Year1, Year2));
                        }catch (Exception ee) { throw new Exception("AccountSQL_GetAccountOprReport_Details_InYearRange:" + ee.Message); }

                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Day_ReportDetail:
                        try{
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            return Report_Buys_Day_ReportDetail .Get_Report_Buys_Day_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Buys_Day_ReportDetail( Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Buys_Day_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Day_Report:
                        try{
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            return Report_Buys_Month_ReportDetail .Get_Report_Buys_Month_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Buys_Day_Report( Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Buys_Day_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Month_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_Buys_Month_ReportDetail.Get_Report_Buys_Month_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Buys_Month_ReportDetail( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Buys_Month_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Month_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_Buys_Year_ReportDetail.Get_Report_Buys_Year_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Buys_Month_Report( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Buys_Month_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Year_ReportDetail: 
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_Buys_Year_ReportDetail.Get_Report_Buys_Year_ReportDetail_List_AS_DataTable  (new DataBaseFunctions(DB).Get_Report_Buys_Year_ReportDetail( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Buys_Year_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_Year_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_Buys_YearRange_ReportDetail.Get_Report_Buys_YearRange_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Buys_Year_Report( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Buys_Year_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Buys_YearRange_ReportDetail:
                        try{
                            
                            int Year1 = Convert.ToInt32(Table_Parameter.Rows[0]["Year1"]);
                            int Year2 = Convert.ToInt32(Table_Parameter.Rows[0]["Year2"]);
                            
                            return Report_Buys_YearRange_ReportDetail.Get_Report_Buys_YearRange_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Buys_YearRange_ReportDetail(Year1, Year2));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Buys_YearRange_ReportDetail:" + ee.Message); }

                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Day_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            
                            return Report_PayOrders_Day_ReportDetail .Get_Report_PayOrders_Day_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_PayOrders_Day_ReportDetail( Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_PayOrders_Day_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Day_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            
                            return Report_PayOrders_Month_ReportDetail.Get_Report_PayOrders_Month_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_PayOrders_Day_Report( Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_PayOrders_Day_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Month_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_PayOrders_Month_ReportDetail.Get_Report_PayOrders_Month_ReportDetail_List_AS_DataTable  (new DataBaseFunctions(DB).Get_Report_PayOrders_Month_ReportDetail( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_PayOrders_Month_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Month_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_PayOrders_Year_ReportDetail.Get_Report_PayOrders_Year_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_PayOrders_Month_Report( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_PayOrders_Month_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Year_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_PayOrders_Year_ReportDetail.Get_Report_PayOrders_Year_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_PayOrders_Year_ReportDetail( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_PayOrders_Year_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_Year_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_PayOrders_YearRange_ReportDetail.Get_Report_PayOrders_YearRange_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_PayOrders_Year_Report( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_PayOrders_Year_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_PayOrders_YearRange_ReportDetail:
                        try{
                            
                            int Year1 = Convert.ToInt32(Table_Parameter.Rows[0]["Year1"]);
                            int Year2 = Convert.ToInt32(Table_Parameter.Rows[0]["Year2"]);
                            
                            return Report_PayOrders_YearRange_ReportDetail.Get_Report_PayOrders_YearRange_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_PayOrders_YearRange_ReportDetail( Year1, Year2));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_PayOrders_YearRange_ReportDetail:" + ee.Message); }

                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Day_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            
                            return Report_Sells_Day_ReportDetail .Get_Report_Sells_Day_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Sells_Day_ReportDetail( Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Sells_Day_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Day_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            
                            return Report_Sells_Month_ReportDetail.Get_Report_Sells_Month_ReportDetail_AS_DataTable  (new DataBaseFunctions(DB).Get_Report_Sells_Day_Report( Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Sells_Day_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Month_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_Sells_Month_ReportDetail.Get_Report_Sells_Month_ReportDetail_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Sells_Month_ReportDetail( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Sells_Month_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Month_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_Sells_Year_ReportDetail.Get_Report_Sells_Year_ReportDetail_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Sells_Month_Report( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Sells_Month_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Year_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_Sells_Year_ReportDetail.Get_Report_Sells_Year_ReportDetail_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Sells_Year_ReportDetail( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Sells_Year_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_Year_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_Sells_YearRange_ReportDetail.Get_Report_Sells_YearRange_ReportDetail_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Sells_Year_Report( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Sells_Year_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_Sells_YearRange_ReportDetail:
                        try{
                            
                            int Year1 = Convert.ToInt32(Table_Parameter.Rows[0]["Year1"]);
                            int Year2= Convert.ToInt32(Table_Parameter.Rows[0]["Year2"]);
                            
                            return Report_Sells_YearRange_ReportDetail.Get_Report_Sells_YearRange_ReportDetail_AS_DataTable (new DataBaseFunctions(DB).Get_Report_Sells_YearRange_ReportDetail(Year1, Year2));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_Sells_YearRange_ReportDetail:" + ee.Message); }


                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Day_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            
                            return Report_MaintenanceOPRs_Day_ReportDetail .Get_Report_MaintenanceOPRs_Day_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_MaintenanceOPRs_Day_ReportDetail(Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_MaintenanceOPRs_Day_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Day_Report:
                        try{
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            int Day = Convert.ToInt32(Table_Parameter.Rows[0]["Day"]);
                            
                            return Report_MaintenanceOPRs_Month_ReportDetail.Get_Report_MaintenanceOPRs_Month_ReportDetail_List_AS_DataTable  (new DataBaseFunctions(DB).Get_Report_MaintenanceOPRs_Day_Report( Year, Month, Day));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_MaintenanceOPRs_Day_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Month_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_MaintenanceOPRs_Month_ReportDetail.Get_Report_MaintenanceOPRs_Month_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_MaintenanceOPRs_Month_ReportDetail( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_MaintenanceOPRs_Month_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Month_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            int Month = Convert.ToInt32(Table_Parameter.Rows[0]["Month"]);
                            
                            return Report_MaintenanceOPRs_Year_ReportDetail.Get_Report_MaintenanceOPRs_Year_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_MaintenanceOPRs_Month_Report( Year, Month));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_MaintenanceOPRs_Month_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Year_ReportDetail:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_MaintenanceOPRs_Year_ReportDetail.Get_Report_MaintenanceOPRs_Year_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_MaintenanceOPRs_Year_ReportDetail( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_MaintenanceOPRs_Year_ReportDetail:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_Year_Report:
                        try{
                            
                            int Year = Convert.ToInt32(Table_Parameter.Rows[0]["Year"]);
                            
                            return Report_MaintenanceOPRs_YearRange_ReportDetail.Get_Report_MaintenanceOPRs_YearRange_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_MaintenanceOPRs_Year_Report( Year));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_MaintenanceOPRs_Year_Report:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.AccountSQL_Get_Report_MaintenanceOPRs_YearRange_ReportDetail:
                        try{
                            
                            int Year1 = Convert.ToInt32(Table_Parameter.Rows[0]["Year1"]);
                            int Year2 = Convert.ToInt32(Table_Parameter.Rows[0]["Year2"]);
                            
                            return Report_MaintenanceOPRs_YearRange_ReportDetail.Get_Report_MaintenanceOPRs_YearRange_ReportDetail_List_AS_DataTable (new DataBaseFunctions(DB).Get_Report_MaintenanceOPRs_YearRange_ReportDetail( Year1, Year2));
                        }catch (Exception ee) { throw new Exception("AccountSQL_Get_Report_MaintenanceOPRs_YearRange_ReportDetail:" + ee.Message); }

                    case OverLoad_SQL_Functions_Code.TradeSQL_Get_ItemIN_AvailableAmount_Report_List:
                        try
                        {

                            uint UserID = Convert.ToUInt32(Table_Parameter.Rows[0]["UserID"]);

                            return ItemIN_AvailableAmount_Report.Get_ItemIN_AvailableAmount_Report_List_AS_DataTable  (new AvailableItemSQL  (DB).Get_ItemIN_AvailableAmount_Report_List (UserID ));
                        }
                        catch (Exception ee) { throw new Exception("TradeSQL_Get_ItemIN_AvailableAmount_Report_List:" + ee.Message); }
                    case OverLoad_SQL_Functions_Code.TradeSQL_Get_Item_AvailableAmount_Report_List:
                        try
                        {

                            uint UserID = Convert.ToUInt32(Table_Parameter.Rows[0]["UserID"]);

                            return Item_AvailableAmount_Report.Get_Item_AvailableAmount_Report_List_AS_DataTable(new AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List(UserID));
                        }
                        catch (Exception ee) { throw new Exception("TradeSQL_Get_Item_AvailableAmount_Report_List:" + ee.Message); }

                    case OverLoad_SQL_Functions_Code.CompanySQL_GetEmployeesReportList :
                        try
                        {

                            uint UserID = Convert.ToUInt32(Table_Parameter.Rows[0]["UserID"]);

                            return EmployeesReport .Get_EmployeesReport_List_AS_DataTable  (new DataBaseFunctions (DB).Company_GetEmployeesReportList ());
                        }
                        catch (Exception ee) { throw new Exception("CompanySQL_GetEmployeesReportList:" + ee.Message); }

                    case OverLoad_SQL_Functions_Code.CompanySQL_Get_EmployeeMent_Employee_Report_List:
                        try
                        {

                            uint UserID = Convert.ToUInt32(Table_Parameter.Rows[0]["UserID"]);

                            return EmployeeMent_Employee_Report.Get_EmployeeMent_Employee_Report_List_AS_DataTable (new DataBaseFunctions(DB).Company_Get_EmployeeMent_Employee_Report_List());
                        }
                        catch (Exception ee) { throw new Exception("CompanySQL_Get_EmployeeMent_Employee_Report_List:" + ee.Message); }
                    default:
                        throw new Exception("Execute_Function :Function UnIndified");

                }
            }
            catch (Exception ee)
            {
                throw new Exception("Execute_Function:" + ee.Message);
            }
        }
    }
    
}
