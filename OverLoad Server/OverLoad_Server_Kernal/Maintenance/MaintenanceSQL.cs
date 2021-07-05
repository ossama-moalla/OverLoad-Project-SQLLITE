

using OverLoad_Server_Kernal.AccountingSQL;
using OverLoad_Server_Kernal.ItemObjSQL;
using OverLoad_Server_Kernal.Objects;
using OverLoad_Server_Kernal.TradeSQL;
using System;
using System.Collections.Generic;
using System.Data;

namespace OverLoad_Server_Kernal
{
    namespace MaintenanceSQL
    {
        public class BillMaintenanceSQL
        {
            DatabaseInterface DB;
            private static class BillMaintenanceTable
            {
                public const string TableName = "Trade_BillMaintenance";
                public const string MaintenanceOPRID = "MaintenanceOPRID";
                public const string BillMaintenanceID = "BillMaintenanceID";
                public const string BillDate = "BillDate";
                public const string SellTypeID = "SellTypeID";
                public const string CurrencyID = "CurrencyID";
                public const string ExchangeRate = "ExchangeRate";
                public const string Discount = "Discount";
                public const string Notes = "Notes";

            }
        
            public BillMaintenanceSQL(DatabaseInterface db)
            {
                DB = db;

            }
            internal BillMaintenance GetBillMaintenance_By_MaintenaceOPR(MaintenanceOPR MaintenanceOPR_)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + BillMaintenanceTable.BillMaintenanceID + ","
                    + BillMaintenanceTable.BillDate + ","
                    + BillMaintenanceTable.CurrencyID + ","
                    + BillMaintenanceTable.ExchangeRate + ","
                    + BillMaintenanceTable.Discount + ","
                    + BillMaintenanceTable.Notes + ","
                    + BillMaintenanceTable.SellTypeID 
                    + " from   "
                    + BillMaintenanceTable.TableName
                    + " where "
                    + BillMaintenanceTable.MaintenanceOPRID + "=" + MaintenanceOPR_._Operation.OperationID
                      );
                    if (t.Rows.Count == 1)
                    {
                        uint  billid =Convert.ToUInt32(t.Rows[0][0]);
                        DateTime billdate = Convert.ToDateTime(t.Rows[0][1]);
                        //string desc = t.Rows[0][1].ToString ();
                        //Contact  Contact_ = new ContactSQL(DB).GetContactInforBYID (Convert.ToUInt32(t.Rows[0][2].ToString()));
                        Currency Currency_ = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][2].ToString()));
                        double exchangerate = Convert.ToDouble(t.Rows[0][3].ToString());
                        double discount = Convert.ToDouble(t.Rows[0][4].ToString());
                        string notes = t.Rows[0][5].ToString();
                        SellType selltype=new SellTypeSql (DB).GetSellTypeinfo(Convert.ToUInt32(t.Rows[0][6].ToString()));
                        if (Currency_.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                        return new BillMaintenance(MaintenanceOPR_, billid, billdate,selltype , Currency_, exchangerate, discount, notes);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetBillMaintenance_By_MaintenaceOPR:" + ee.Message);
                    return null;
                }
            }
            public BillMaintenance GetBillMaintenance_INFO_BYID(uint billid)
            {
                try
                {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                 + BillMaintenanceTable.MaintenanceOPRID  + ","
                + BillMaintenanceTable.BillDate+","
                + BillMaintenanceTable.CurrencyID + ","
                + BillMaintenanceTable.ExchangeRate  + ","
                + BillMaintenanceTable.Discount + ","
                + BillMaintenanceTable.Notes + ","
                    + BillMaintenanceTable.SellTypeID
                + " from   "
                + BillMaintenanceTable.TableName
                + " where "
                + BillMaintenanceTable.BillMaintenanceID  + "=" + billid  
                  );
                if (t.Rows.Count == 1)
                {
                    MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert .ToUInt32 (t.Rows[0][0]));
                    DateTime billdate = Convert.ToDateTime(t.Rows [0][1]);
                    //string desc = t.Rows[0][1].ToString ();
                    //Contact  Contact_ = new ContactSQL(DB).GetContactInforBYID (Convert.ToUInt32(t.Rows[0][2].ToString()));
                    Currency Currency_ = new CurrencySQL(DB).GetCurrencyINFO_ByID(Convert.ToUInt32(t.Rows[0][2].ToString()));
                    double exchangerate = Convert.ToDouble(t.Rows[0][3].ToString());
                    double discount =Convert .ToDouble ( t.Rows[0][4].ToString());
                    string notes = t.Rows[0][5].ToString();
                    SellType selltype = new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(t.Rows[0][6].ToString()));

                    if (Currency_.ReferenceCurrencyID == null && exchangerate != 1) throw new Exception(" بيانات خاطئة,معامل صرف العملة الرجعية يجب أن يكون 1");

                    return new BillMaintenance  (MaintenanceOPR_,billid , billdate ,selltype  ,Currency_,exchangerate ,discount ,notes );

                }
                else
                    return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetBillMaintenance_INFO_BYID:" + ee.Message);
                    return null;
                }
            }       
            public List<ItemOUT> Get_BillMaintenance_ItemsOUT(BillMaintenance BillMaintenance_)
            {
                List<ItemOUT> List = new List<ItemOUT>();
                try
                {
                    List.AddRange(new ItemOUTSQL(DB).GetItemOUTList(BillMaintenance_._Operation));

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_BillMaintenance_ItemsOUT:" + ee.Message);

                }
                return List;
            }
            #region Bill Clauses
            private static class BillMaintenance_RepairOPR_Clause_Table
            {
                public const string TableName = "Trade_BillMaintenance_Clause_RepairOPR";
                public const string BillMaintenanceID = "BillMaintenanceID";
                public const string RepairOPRID = "RepairOPRID";
                public const string Value_ = "Value_";


            }
            private static class BillMaintenance_DiagnosticOPR_Clause_Table
            {
                public const string TableName = "Trade_BillMaintenance_Clause_DiagnosticOPR";
                public const string BillMaintenanceID = "BillMaintenanceID";
                public const string DiagnosticOPRID = "DiagnosticOPRID";
                public const string Value_ = "Value_";


            }

            public double? GetRepairOPRClauseValue(uint BillID, uint RepairOPRID)
            {
                try
                {
                   DataTable t= DB.GetData ("select     "
                   + BillMaintenance_RepairOPR_Clause_Table.Value_ 
                   + " from "
                   + BillMaintenance_RepairOPR_Clause_Table.TableName
                   + " where "
                   + BillMaintenance_RepairOPR_Clause_Table.BillMaintenanceID + "=" + BillID
                   + " and "
                   + BillMaintenance_RepairOPR_Clause_Table.RepairOPRID + "=" + RepairOPRID
                   );

                    if (t.Rows.Count == 1)
                    {
                        return Convert.ToDouble(t.Rows[0][0]);
                    }
                    else return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetRepairOPRClauseValue:" + ee.Message);
                    return null;
                }
            }
      

            public double? GetDiagnosticOPRClauseValue(uint BillID, uint DiagnosticOPRID)
            {
                try
                {
                    DataTable t = DB.GetData("select     "
                    + BillMaintenance_DiagnosticOPR_Clause_Table.Value_
                    + " from "
                    + BillMaintenance_DiagnosticOPR_Clause_Table.TableName
                    + " where "
                    + BillMaintenance_DiagnosticOPR_Clause_Table.BillMaintenanceID + "=" + BillID
                    + " and "
                    + BillMaintenance_DiagnosticOPR_Clause_Table.DiagnosticOPRID + "=" + DiagnosticOPRID
                    );

                    if (t.Rows.Count == 1)
                    {
                        return Convert.ToDouble(t.Rows[0][0]);
                    }
                    else return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetDiagnosticOPRClauseValue:" + ee.Message);
                    return null;
                }
            }
            public List<BillMaintenance_Clause> BillMaintenance_GetClauses(BillMaintenance BillMaintenance_)
            {
                List<BillMaintenance_Clause> List = new List<Objects.BillMaintenance_Clause>();

                    try
                    {
                        List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(BillMaintenance_._Operation);
                        for (int i = 0; i < ItemOUTList.Count; i++)
                            List.Add
                                (new Objects.BillMaintenance_Clause
                                (BillMaintenance_._Operation.OperationID, ItemOUTList[i]));
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("BillMaintenance_ItemOutClauses:" + ee.Message);

                    }
                    try
                    {




                        List<RepairOPR> RepairOPRList = new RepairOPRSQL(DB).Get_MaintenanceOPR_RepairOPR_List(BillMaintenance_._MaintenanceOPR);
                        for (int i = 0; i < RepairOPRList.Count; i++)
                        {
                        List.Add
                             (new Objects.BillMaintenance_Clause
                             (BillMaintenance_._Operation.OperationID, RepairOPRList[i],
                                 GetRepairOPRClauseValue(BillMaintenance_._Operation.OperationID, RepairOPRList[i]._Operation.OperationID)));
                        List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(RepairOPRList[i]._Operation);
                        for (int j = 0; j < ItemOUTList.Count; j++)
                            List.Add
                                (new Objects.BillMaintenance_Clause
                                (RepairOPRList[i]._Operation.OperationID, ItemOUTList[j]));
                    }
                 
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("BillMaintenance_RepairOPRClauses:" + ee.Message);
                    }
                    try
                    {
                        List<DiagnosticOPRReport> DiagnosticOPRList = new DiagnosticOPRSQL(DB).GetSubDiagnosticOPRReportList(BillMaintenance_._MaintenanceOPR, null);
                        for (int i = 0; i < DiagnosticOPRList.Count; i++)
                            List.Add
                                (new Objects.BillMaintenance_Clause
                                (BillMaintenance_._Operation.OperationID, DiagnosticOPRList[i]._DiagnosticOPR,
                                GetDiagnosticOPRClauseValue(BillMaintenance_._Operation.OperationID, DiagnosticOPRList[i]._DiagnosticOPR.DiagnosticOPRID)));
                    }
                    catch (Exception ee)
                    {
                        throw new Exception("BillMaintenance_DiagnosticOPRClauses:" + ee.Message);
                    }
                try
                {
                    List<BillAdditionalClause > BillAdditionalClauseList = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses (BillMaintenance_._Operation );
                    for (int i = 0; i < BillAdditionalClauseList.Count; i++)
                        List.Add
                            (new Objects.BillMaintenance_Clause
                            (BillMaintenance_._Operation.OperationID, BillAdditionalClauseList[i]));
                }
                catch (Exception ee)
                {
                    throw new Exception("BillMaintenance_AdditionalClauses:" + ee.Message);
                }


                return List;
            }

            #endregion

            internal double GetBillMaintenanceValue(uint billmid)
            {
                try
                {

                    return new OperationSQL(DB).Get_OperationValue(Operation.BILL_MAINTENANCE, billmid );
                }
                catch (Exception ee)
                {
                    throw new Exception("GetBillMaintenanceValue:" + ee.Message);
                    return -1;
                }
            }
            internal double GetBillMaintenance_PaysValue(uint billmid)
            {
                try
                {
                    return new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(Operation.BILL_MAINTENANCE, billmid );
                }
                catch (Exception ee)
                {
                    throw new Exception("GetBillMaintenance_PaysValue:" + ee.Message);
                    return -1;
                }
            }
        }
        public class MaintenanceOPRSQL
        {
            DatabaseInterface DB;
            internal  static class MaintenanceOPRTable
            {
               
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR";
                public const string MaintenanceOPRID = "MaintenanceOPRID";
                public const string EntryDate = "EntryDate";
                public const string ContactID = "ContactID";
                public const string ItemID = "ItemID";
                public const string ItemSerial = "ItemSerial";
                public const string FaultDesc = "FaultDesc";
                public const string Notes = "Notes";

            }
            
            public MaintenanceOPRSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public MaintenanceOPR GetMaintenancePRINFO_BYID(uint MaintenanceOPRid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + MaintenanceOPRTable.EntryDate  + ","
                     + MaintenanceOPRTable.ContactID  + ","
                     + MaintenanceOPRTable.ItemID + ","
                     + MaintenanceOPRTable.ItemSerial + ","
                     + MaintenanceOPRTable.FaultDesc + ","
                     + MaintenanceOPRTable.Notes 
                     + " from   "
                    + MaintenanceOPRTable.TableName
                    + " where "
                    + MaintenanceOPRTable.MaintenanceOPRID  + "=" + MaintenanceOPRid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime entrydate = Convert.ToDateTime(t.Rows[0][0].ToString());
                        Contact Contact_ = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[0][1].ToString()));
                        Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][2].ToString()));
                        string itemserial = t.Rows[0][3].ToString();
                        string  faultdesc = t.Rows[0][4].ToString();
                        string notes = t.Rows[0][5].ToString();
                        TradeStorePlace place = new TradeItemStoreSQL(DB).GetMaintenanceStorePlace(MaintenanceOPRid);
                        MaintenanceOPR_EndWork MaintenanceOPR_EndWork_ = Get_MaintenanceOPR_EndWork_ForMaintenanceOPR(MaintenanceOPRid);
                        return new MaintenanceOPR (MaintenanceOPRid,entrydate ,Contact_
                            , item, itemserial , faultdesc, place, MaintenanceOPR_EndWork_, notes);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMaintenanceOPRINFO_BYID" + ee.Message);
                    return null;
                }

            }

            public List<MaintenanceOPR> GetNOT_Finsh_MaintenanceOPRList()
            {
                try
                {

            List<MaintenanceOPR> MaintenanceOPRlist = new List<MaintenanceOPR>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + MaintenanceOPRTable.MaintenanceOPRID + ","
                     + MaintenanceOPRTable.EntryDate  + ","
                     + MaintenanceOPRTable.ContactID  + ","
                     + MaintenanceOPRTable.ItemID  + ","
                     + MaintenanceOPRTable.ItemSerial  + ","
                     + MaintenanceOPRTable.FaultDesc  + ","
                     + MaintenanceOPRTable.Notes
                     + " from   "
                    + MaintenanceOPRTable.TableName
                    + " where not exists(select * from "
                    +MaintenanceOPR_EndWorkTable.TableName 
                    +" where "
                     + MaintenanceOPR_EndWorkTable.TableName
                     +"."
                     + MaintenanceOPR_EndWorkTable.MaintenanceOPRID 
                     +"="
                     + MaintenanceOPRTable.TableName
                     + "."
                    + MaintenanceOPRTable.MaintenanceOPRID  + ")  order by "
                    + MaintenanceOPRTable.EntryDate
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint maintenanceoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime entrydate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        Contact contact = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][3].ToString()));
                        string itemserial = t.Rows[i][4].ToString();
                        string faultdesc = t.Rows[i][5].ToString();
                        string notes = t.Rows[i][6].ToString();
                        TradeStorePlace place = new TradeItemStoreSQL(DB).GetMaintenanceStorePlace(maintenanceoprid);



                        MaintenanceOPRlist.Add(new MaintenanceOPR(maintenanceoprid,entrydate ,contact ,item ,itemserial ,faultdesc
                            ,place,null ,notes ));

                    }
                    return MaintenanceOPRlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetNOT_Finsh_MaintenanceOPRList" + ee.Message);
                    return null;
                }
            }
            #region Maintenance_EndWork
            internal static class MaintenanceOPR_EndWorkTable
            {

                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_EndWork";
                public const string MaintenanceOPRID = "MaintenanceOPRID";
                public const string EndWorkDate = "EndWorkDate";
                public const string Repaired = "Repaired";
                public const string Report = "Report";
                public const string DeliveredDate = "DeliveredDate";
                public const string EndwarrantyDate = "EndwarrantyDate";


            }
            public  MaintenanceOPR_EndWork Get_MaintenanceOPR_EndWork_ForMaintenanceOPR(uint maintenanceOPRid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + MaintenanceOPR_EndWorkTable.EndWorkDate + ","
                     + MaintenanceOPR_EndWorkTable.Repaired + ","
                     + MaintenanceOPR_EndWorkTable.Report + ","
                     + MaintenanceOPR_EndWorkTable.DeliveredDate + ","
                     + MaintenanceOPR_EndWorkTable.EndwarrantyDate
                     + " from   "
                    + MaintenanceOPR_EndWorkTable.TableName
                    + " where "
                    + MaintenanceOPR_EndWorkTable.MaintenanceOPRID + "=" + maintenanceOPRid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime endworkdate = Convert.ToDateTime(t.Rows[0][0]);
                        bool repaired = Convert.ToInt32 (t.Rows[0][1]) == 1 ? true : false;
                        string report = t.Rows[0][2].ToString();
                        DateTime? deliverdate, endwarrantydate;
                        try
                        {
                            deliverdate = Convert.ToDateTime(t.Rows[0][3]);
                        }
                        catch
                        {
                            deliverdate = null;
                        }
                        try
                        {
                            endwarrantydate = Convert.ToDateTime(t.Rows[0][4]);
                        }
                        catch
                        {
                            endwarrantydate = null;
                        }
                        return new MaintenanceOPR_EndWork(maintenanceOPRid, endworkdate
                            , repaired, report, deliverdate, endwarrantydate);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_MaintenanceOPR_EndWork_ForMaintenanceOPR" + ee.Message);
                    return null;
                }
            }

            #endregion
            internal List<MaintenanceOPR> GetAllMaintenanceOPRs()
            {
                try
                {

                    List<MaintenanceOPR> MaintenanceOPRlist = new List<MaintenanceOPR>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + MaintenanceOPRTable.MaintenanceOPRID + ","
                     + MaintenanceOPRTable.EntryDate + ","
                     + MaintenanceOPRTable.ContactID + ","
                     + MaintenanceOPRTable.ItemID + ","
                     + MaintenanceOPRTable.ItemSerial + ","
                     + MaintenanceOPRTable.FaultDesc + ","
                     + MaintenanceOPRTable.Notes
                     + " from   "
                    + MaintenanceOPRTable.TableName
                    +"  order by "
                    + MaintenanceOPRTable.EntryDate
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint maintenanceoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime entrydate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        Contact contact = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][3].ToString()));
                        string itemserial = t.Rows[i][4].ToString();
                        string faultdesc = t.Rows[i][5].ToString();
                        string notes = t.Rows[i][6].ToString();
                        TradeStorePlace place = new TradeItemStoreSQL(DB).GetMaintenanceStorePlace(maintenanceoprid);
                        MaintenanceOPR_EndWork MaintenanceOPR_EndWork_ = Get_MaintenanceOPR_EndWork_ForMaintenanceOPR(maintenanceoprid);


                        MaintenanceOPRlist.Add(new MaintenanceOPR(maintenanceoprid, entrydate, contact, item, itemserial, faultdesc
                            , place, MaintenanceOPR_EndWork_, notes));

                    }
                    return MaintenanceOPRlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetAllMaintenanceOPRs:" + ee.Message);
                    return null;
                }
            }
            internal List<MaintenanceOPR> GetAllMaintenanceOPRs_forItem(Item item)
            {
                try
                {

                    List<MaintenanceOPR> MaintenanceOPRlist = new List<MaintenanceOPR>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + MaintenanceOPRTable.MaintenanceOPRID + ","
                     + MaintenanceOPRTable.EntryDate + ","
                     + MaintenanceOPRTable.ContactID + ","
                     + MaintenanceOPRTable.ItemSerial + ","
                     + MaintenanceOPRTable.FaultDesc + ","
                     + MaintenanceOPRTable.Notes
                     + " from   "
                    + MaintenanceOPRTable.TableName
                    + "  order by "
                    + MaintenanceOPRTable.EntryDate
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint maintenanceoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime entrydate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        Contact contact = new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        string itemserial = t.Rows[i][3].ToString();
                        string faultdesc = t.Rows[i][4].ToString();
                        string notes = t.Rows[i][5].ToString();
                        TradeStorePlace place = new TradeItemStoreSQL(DB).GetMaintenanceStorePlace(maintenanceoprid);
                        MaintenanceOPR_EndWork MaintenanceOPR_EndWork_ = Get_MaintenanceOPR_EndWork_ForMaintenanceOPR(maintenanceoprid);


                        MaintenanceOPRlist.Add(new MaintenanceOPR(maintenanceoprid, entrydate, contact, item, itemserial, faultdesc
                            , place, MaintenanceOPR_EndWork_, notes));

                    }
                    return MaintenanceOPRlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetAllMaintenanceOPRs_forItem:" + ee.Message);
                    return null;
                }
            }
        }
        public class MaintenanceAccessorySQL
        {
            DatabaseInterface DB;
            private static class MaintenanceAccessoryTable
            {
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_Accessory";
                public const string AccessoryID = "AccessoryID";
                public const string MaintenanceOPRID = "MaintenanceOPRID";
                public const string ItemID = "ItemID";
                public const string ItemSerial = "ItemSerial";
                public const string Notes = "Notes";


            }
            public MaintenanceAccessorySQL(DatabaseInterface db)
            {
                DB = db;

            }
            public MaintenanceOPR_Accessory Get_Accessory_INFO_BYID(uint accessoryid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MaintenanceAccessoryTable.MaintenanceOPRID + ","
                    + MaintenanceAccessoryTable.ItemID + ","
                    + MaintenanceAccessoryTable.ItemSerial + ","
                    + MaintenanceAccessoryTable.Notes
                    + " from   "
                    + MaintenanceAccessoryTable.TableName
                    + " where "
                    + MaintenanceAccessoryTable.AccessoryID + "=" + accessoryid
                      );
                    if (t.Rows.Count == 1)
                    {
                        MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        Item Item_ = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][1].ToString()));
                        string itemserial = t.Rows[0][2].ToString();
                        string notes = t.Rows[0][3].ToString();
                        TradeStorePlace place = new TradeItemStoreSQL(DB).GetAccessoryStorePlace(accessoryid);
                        return new MaintenanceOPR_Accessory(accessoryid, MaintenanceOPR_, Item_, itemserial, notes, place);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Accessory_INFO_BYID:"+ee.Message  );
                    return null;
                }
                
            }

            public List<MaintenanceOPR_Accessory > GetMaintenanceOPR_Accessories_List(MaintenanceOPR MaintenanceOPR_)
            {
                List<MaintenanceOPR_Accessory> Accessorylist = new List<MaintenanceOPR_Accessory>();

                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MaintenanceAccessoryTable.AccessoryID  + ","
                    + MaintenanceAccessoryTable.ItemID + ","
                    + MaintenanceAccessoryTable.ItemSerial + ","
                    + MaintenanceAccessoryTable.Notes
                    + " from   "
                    + MaintenanceAccessoryTable.TableName
                    + " where "
                    + MaintenanceAccessoryTable.MaintenanceOPRID  + "=" + MaintenanceOPR_._Operation.OperationID
                      );
                    for(int i=0;i<t.Rows .Count;i++)
                    {
                        uint accessoryid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        Item Item_ = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][1].ToString()));
                        string itemserial = t.Rows[i][2].ToString();
                        string notes = t.Rows[i][3].ToString();
                        TradeStorePlace place = new TradeItemStoreSQL(DB).GetAccessoryStorePlace(accessoryid);
                        Accessorylist.Add ( new MaintenanceOPR_Accessory(accessoryid, MaintenanceOPR_, Item_, itemserial, notes, place));

                    }
                    return Accessorylist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMaintenanceOPR_Accessories_List:" + ee.Message);
                    return Accessorylist;
                }
            }

        }
        public class DiagnosticOPRSQL
        {
            DatabaseInterface DB;
            internal static class DiagnosticOPRTable
            {

                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_DiagnosticOPR";
                public const string MaintenanceOPRID = "MaintenanceOPRID";
                public const string ParentDiagnosticOPRID = "ParentDiagnosticOPRID";
                public const string DiagnosticOPRID = "DiagnosticOPRID";
                public const string DiagnosticOPRDate = "DiagnosticOPRDate";
                public const string ItemID = "ItemID";
                public const string Desc = "Description_";
                public const string Location = "Location";
                public const string Normal = "Normal";
                public const string Report = "Report";

            }
            public DiagnosticOPRSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public DiagnosticOPR GetDiagnosticOPRINFO_BYID(uint DiagnosticOPRid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + DiagnosticOPRTable.MaintenanceOPRID + ","
                     + DiagnosticOPRTable.ParentDiagnosticOPRID  + ","
                     + DiagnosticOPRTable.DiagnosticOPRDate + ","
                     + DiagnosticOPRTable.ItemID  + ","
                     + DiagnosticOPRTable.Desc  + ","
                     + DiagnosticOPRTable.Location + ","
                     + DiagnosticOPRTable.Normal  + ","
                     + DiagnosticOPRTable.Report 
                     + " from   "
                    + DiagnosticOPRTable.TableName
                    + " where "
                    + DiagnosticOPRTable.DiagnosticOPRID  + "=" + DiagnosticOPRid
                      );
                    if (t.Rows.Count == 1)
                    {
                        MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        uint? ParentDiagnosticID;
                        try
                        {
                            ParentDiagnosticID = Convert.ToUInt32(t.Rows[0][1].ToString());
                        }
                        catch
                        {
                            ParentDiagnosticID = null;
                        }

                        DateTime OprDate = Convert.ToDateTime(t.Rows [0][2].ToString ());
                        Item item;
                        try
                        {
                             item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][3].ToString()));

                        }
                        catch
                        {
                            item = null;
                        }
                        string desc = t.Rows[0][4].ToString();
                        string location = t.Rows[0][5].ToString();

                        bool? normal;
                        try
                        {
                            normal = Convert.ToInt32 (t.Rows[0][6].ToString()) == 1 ? true : false;
                        }
                        catch
                        {
                            normal = null;
                        }

                        string report = t.Rows[0][7].ToString();


                        return new DiagnosticOPR(MaintenanceOPR_, ParentDiagnosticID, DiagnosticOPRid, OprDate
                            , item, desc, location, normal, report);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetDiagnosticOPRINFO_BYID:" + ee.Message);
                    return null;
                }

            }

            public List<DiagnosticOPRReport> GetSubDiagnosticOPRReportList(MaintenanceOPR MaintenanceOPR_,DiagnosticOPR  ParentDiagnosticOPR)
            {
                List<DiagnosticOPRReport> list = new List<DiagnosticOPRReport>();
                try
                {
                    string parentid = (ParentDiagnosticOPR == null ? " is null" :"="+ ParentDiagnosticOPR.DiagnosticOPRID.ToString());

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + DiagnosticOPRTable.DiagnosticOPRID + ","
                     + DiagnosticOPRTable.DiagnosticOPRDate + ","
                     + DiagnosticOPRTable.ItemID + ","
                     + DiagnosticOPRTable.Desc + ","
                     + DiagnosticOPRTable.Location + ","
                     + DiagnosticOPRTable.Normal + ","
                     + DiagnosticOPRTable.Report
                     + " from   "
                    + DiagnosticOPRTable.TableName
                    + " where "
                    + DiagnosticOPRTable.MaintenanceOPRID + "=" + MaintenanceOPR_._Operation.OperationID
                     + " and "
                    + DiagnosticOPRTable.ParentDiagnosticOPRID + parentid
                      );
                    uint? p_id;
                    try
                    {
                        p_id = ParentDiagnosticOPR.DiagnosticOPRID;
                        }
                    catch
                    {
                        p_id = null;
                    }
                    MeasureOPRSQL MeasureOPRSQL_ = new MeasureOPRSQL(DB);
                    DiagnosticOPRFileSQL DiagnosticOPRFileSQL_ = new DiagnosticOPRFileSQL(DB);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint diagnosticoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime OprDate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        Item item;
                        try
                        {
                            item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][2].ToString()));

                        }
                        catch
                        {
                            item = null;
                        }
                        string desc = t.Rows[i][3].ToString();
                        string location = t.Rows[i][4].ToString();

                        bool? normal;
                        try
                        {
                            normal = Convert.ToInt32(t.Rows[i][5].ToString()) == 1 ? true : false; 
                        }
                        catch
                        {
                            normal = null;
                        }

                        string report = t.Rows[i][6].ToString();
                        int tagscount = new MaintenanceTagSQL(DB).GetDiagnosticOPR_TagsCount(diagnosticoprid);
                        list.Add(new DiagnosticOPRReport(new DiagnosticOPR(MaintenanceOPR_, p_id, diagnosticoprid , OprDate
                            , item, desc, location, normal, report), MeasureOPRSQL_.GetDiagnostic_MeasureOPRCount(diagnosticoprid)
                            , DiagnosticOPRFileSQL_.GetDiagnostic_FileCount (diagnosticoprid),GetDiagnosticOPR_SubDiagnosticOPRCount(diagnosticoprid),tagscount));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetSubDiagnosticOPRReportList:" + ee.Message);
                    return list ;
                }

            }
            public List<DiagnosticOPRReport> Get_All_DiagnosticOPRReportList(MaintenanceOPR MaintenanceOPR_)
            {
                List<DiagnosticOPRReport> list = new List<DiagnosticOPRReport>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + DiagnosticOPRTable.DiagnosticOPRID + ","
                     + DiagnosticOPRTable.DiagnosticOPRDate + ","
                     + DiagnosticOPRTable.ItemID + ","
                     + DiagnosticOPRTable.Desc + ","
                     + DiagnosticOPRTable.Location + ","
                     + DiagnosticOPRTable.Normal + ","
                     + DiagnosticOPRTable.Report + ","
                      + DiagnosticOPRTable.ParentDiagnosticOPRID 
                     + " from   "
                    + DiagnosticOPRTable.TableName
                    + " where "
                    + DiagnosticOPRTable.MaintenanceOPRID + "=" + MaintenanceOPR_._Operation.OperationID

                      );
               
                    MeasureOPRSQL MeasureOPRSQL_ = new MeasureOPRSQL(DB);
                    DiagnosticOPRFileSQL DiagnosticOPRFileSQL_ = new DiagnosticOPRFileSQL(DB);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint diagnosticoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime OprDate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        Item item;
                        try
                        {
                            item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][2].ToString()));

                        }
                        catch
                        {
                            item = null;
                        }
                        string desc = t.Rows[i][3].ToString();
                        string location = t.Rows[i][4].ToString();

                        bool? normal;
                        try
                        {
                            normal = Convert.ToInt32 (t.Rows[i][5].ToString()) == 1 ? true : false;
                        }
                        catch
                        {
                            normal = null;
                        }

                        string report = t.Rows[i][6].ToString();
                        uint? p_id;
                        try
                        {
                            p_id = Convert.ToUInt32(t.Rows[i][7]);
                        }
                        catch
                        {
                            p_id = null;
                        }
                        int tagscount = new MaintenanceTagSQL(DB).GetDiagnosticOPR_TagsCount(diagnosticoprid);
                        list.Add(new DiagnosticOPRReport(new DiagnosticOPR(MaintenanceOPR_, p_id, diagnosticoprid, OprDate
                            , item, desc, location, normal, report), MeasureOPRSQL_.GetDiagnostic_MeasureOPRCount(diagnosticoprid)
                            , DiagnosticOPRFileSQL_.GetDiagnostic_FileCount(diagnosticoprid), GetDiagnosticOPR_SubDiagnosticOPRCount(diagnosticoprid), tagscount));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetAllDiagnosticOPRReportList:" + ee.Message);
                    return list;
                }

            }
            public List<DiagnosticOPRReport> Get_All_DiagnosticOPRReportList_ForItem(Item item)
            {
                List<DiagnosticOPRReport> list = new List<DiagnosticOPRReport>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                     + DiagnosticOPRTable.DiagnosticOPRID + ","
                     + DiagnosticOPRTable.DiagnosticOPRDate + ","
                     + DiagnosticOPRTable.MaintenanceOPRID  + ","
                     + DiagnosticOPRTable.Desc + ","
                     + DiagnosticOPRTable.Location + ","
                     + DiagnosticOPRTable.Normal + ","
                     + DiagnosticOPRTable.Report + ","
                      + DiagnosticOPRTable.ParentDiagnosticOPRID
                     + " from   "
                    + DiagnosticOPRTable.TableName
                    + " where "
                    + DiagnosticOPRTable.ItemID  + "=" + item.ItemID 

                      );

                    MeasureOPRSQL MeasureOPRSQL_ = new MeasureOPRSQL(DB);
                    DiagnosticOPRFileSQL DiagnosticOPRFileSQL_ = new DiagnosticOPRFileSQL(DB);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint diagnosticoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime OprDate = Convert.ToDateTime(t.Rows[i][1].ToString());
                        MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(t.Rows[i][2].ToString()));

                        string desc = t.Rows[i][3].ToString();
                        string location = t.Rows[i][4].ToString();

                        bool? normal;
                        try
                        {
                            normal = Convert.ToInt32 (t.Rows[i][5].ToString()) == 1 ? true : false;
                        }
                        catch
                        {
                            normal = null;
                        }

                        string report = t.Rows[i][6].ToString();
                        uint? p_id;
                        try
                        {
                            p_id = Convert.ToUInt32(t.Rows[i][7]);
                        }
                        catch
                        {
                            p_id = null;
                        }
                        int tagscount = new MaintenanceTagSQL(DB).GetDiagnosticOPR_TagsCount(diagnosticoprid);
                        list.Add(new DiagnosticOPRReport(new DiagnosticOPR(MaintenanceOPR_, p_id, diagnosticoprid, OprDate
                            , item, desc, location, normal, report), MeasureOPRSQL_.GetDiagnostic_MeasureOPRCount(diagnosticoprid)
                            , DiagnosticOPRFileSQL_.GetDiagnostic_FileCount(diagnosticoprid), GetDiagnosticOPR_SubDiagnosticOPRCount(diagnosticoprid), tagscount));

                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_All_DiagnosticOPRReportList_ForItem:" + ee.Message);
                    return list;
                }

            }

            public int GetDiagnosticOPR_SubDiagnosticOPRCount(uint DiagnosticOPRID_)
            {

                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select count(*) from "
                        + DiagnosticOPRTable.TableName
                         + " where "
                        + DiagnosticOPRTable.ParentDiagnosticOPRID  + "=" + DiagnosticOPRID_
                       );
                    if (t.Rows.Count > 0)
                    {
                        return Convert.ToInt32(t.Rows[0][0].ToString());
                    }
                    else return 0;

                }
                catch (Exception ee)
                {
                    throw new Exception("GetDiagnosticOPR_SubDiagnosticOPRCount" + ee.Message);
                    return -1;
                }

            }
        }
        public class MeasureOPRSQL
        {
            public static class MeasureOPRTable
            {
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_DiagnosticOPR_MeasureOPR";
                public const string DiagnosticOPRID = "DiagnosticOPRID";
                public const string MeasureOPRID = "MeasureOPRID";
                public const string Desc = "Description_";
                public const string Value = "Value";
                public const string MeasureUnit = "MeasureUnit";
                public const string Normal = "Normal";
            }
            DatabaseInterface DB;
            public MeasureOPRSQL(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public MeasureOPR GetMeasureOPRinfo_ByID(uint MeasureOPRid)
            {
                try
                {
                    DataTable t = DB.GetData("select "
                    + MeasureOPRTable.DiagnosticOPRID + ","
                       + MeasureOPRTable.Desc + ","
                        + MeasureOPRTable.Value + ","
                         + MeasureOPRTable.MeasureUnit + ","
                         + MeasureOPRTable.Normal 
                       + " from  "
                       + MeasureOPRTable.TableName
                       + " where "
                       + MeasureOPRTable.MeasureOPRID + "=" + MeasureOPRid);
                    if (t.Rows.Count == 1)
                    {
                        DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        string desc = t.Rows[0][1].ToString();
                        string  value = t.Rows [0][2].ToString ();
                        string measureunit = t.Rows[0][3].ToString();
                        bool? normal;
                        try
                        {
                            normal = Convert.ToInt32 (t.Rows[0][4].ToString()) == 1 ? true : false;
                        }
                        catch
                        {
                            normal = null;
                        }
                        return new MeasureOPR (DiagnosticOPR_,MeasureOPRid,desc,value ,measureunit ,normal );
                    } 
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMeasureOPRinfo_ByID" + ee.Message);
                    return null;
                }

            }

            public List<MeasureOPR> GetMeasureOPRList(DiagnosticOPR DiagnosticOPR_)
            {
                List<MeasureOPR> list = new List<MeasureOPR>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + MeasureOPRTable.MeasureOPRID  + ","
                           + MeasureOPRTable.Desc  + ","
                             + MeasureOPRTable.Value  + ","
                          + MeasureOPRTable.MeasureUnit + ","
                          + MeasureOPRTable.Normal 
                        + " from "
                        + MeasureOPRTable.TableName
                         + " where "
                        + MeasureOPRTable.DiagnosticOPRID  + "=" + DiagnosticOPR_.DiagnosticOPRID
                       );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint MeasureOPRID = Convert.ToUInt32(t.Rows [i][0].ToString ());
                        string desc = t.Rows[i][1].ToString();
                        string  value = t.Rows[i][2].ToString();
                        string MeasureUnit = t.Rows[i][3].ToString();
                        bool? normal;
                        try
                        {
                            normal = Convert.ToInt32 (t.Rows[i][4].ToString()) == 1 ? true : false;
                        }
                        catch
                        {
                            normal = null;
                        }
                        MeasureOPR m = new MeasureOPR(DiagnosticOPR_, MeasureOPRID, desc, value, MeasureUnit,normal );
                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMeasureOPRList" + ee.Message);
                }

            }
            public int GetDiagnostic_MeasureOPRCount(uint  DiagnosticOPRID_)
            {

                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select count(*) from "
                        + MeasureOPRTable.TableName
                         + " where "
                        + MeasureOPRTable.DiagnosticOPRID + "=" + DiagnosticOPRID_
                       );
                    if (t.Rows.Count > 0)
                    {
                        return Convert.ToInt32(t.Rows[0][0].ToString());
                    }
                    else return 0;
                    
                }
                catch (Exception ee)
                {
                    throw new Exception("GetDiagnostic_MeasureOPRCount" + ee.Message);
                    return -1;
                }

            }
            public List<string > GetMeasureUnitList()
            {
                List<string > list = new List<string >();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select Distinct "
                          + MeasureOPRTable.MeasureUnit
                        + " from "
                        + MeasureOPRTable.TableName);

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        string MeasureUnit = t.Rows[i][0].ToString();
                        list.Add(MeasureUnit);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMeasureUnitList" + ee.Message);
                    return list;
                }

            }
        }
        public class DiagnosticOPRFileSQL
        {
            DatabaseInterface DB;
            public static class DiagnosticOPRFileTable
            {
                public const string TableName = "Item_ItemFiles";
                public const string DiagnosticOPRID = "DiagnosticOPRID";
                public const string FileID = "FileID";
                public const string Item_FileName = "Item_FileName";
                public const string FileDescription = "FileDescription";
                public const string AddDate = "AddDate";
                public const string FileData = "FileData";
            }

            public DiagnosticOPRFileSQL(DatabaseInterface db)
            {
                DB = db;
            }
            public List<DiagnosticFile> GetDiagnosticOPRFileList(DiagnosticOPR DiagnosticOPR_)
            {
                List<DiagnosticFile> DiagnosticFileList = new List<DiagnosticFile>();
                try
                {

                    DataTable t = DB.GetData("select "
                    + DiagnosticOPRFileTable.FileID + ","
                    + DiagnosticOPRFileTable.Item_FileName + ","
                    + DiagnosticOPRFileTable.FileDescription + ","
                    + DiagnosticOPRFileTable.AddDate
                    + " from "
                    + DiagnosticOPRFileTable.TableName
                    + " where "
                    + DiagnosticOPRFileTable.DiagnosticOPRID + "=" + DiagnosticOPR_.DiagnosticOPRID
                    );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint fileid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string filename = t.Rows[i][1].ToString();
                        string filedescription = t.Rows[i][2].ToString();
                        DateTime datetime = Convert.ToDateTime(t.Rows[i][3].ToString());
                        long filesize = GetFileSize(fileid);
                        DiagnosticFileList.Add(new DiagnosticFile(DiagnosticOPR_, fileid, filename, filedescription, filesize, datetime));
                    }
                    return DiagnosticFileList;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetDiagnosticOPRFileList" + ee.Message);
                    return DiagnosticFileList;
                }
            }
            public long GetFileSize(uint fileid)
            {
                try
                {

                    DataTable t = DB.GetData("select length("
                    + DiagnosticOPRFileTable.FileData
                    + ") from "
                    + DiagnosticOPRFileTable.TableName
                    + " where "
                    + DiagnosticOPRFileTable.FileID + "=" + fileid
                    );
                    if (t.Rows.Count > 0)
                    {
                        return (long)t.Rows[0][0];

                    }
                    else return -1;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetFileSize" + ee.Message);
                    return -1;
                }
            }
            public int GetDiagnostic_FileCount(uint DiagnosticOPRID_)
            {

                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select count(*) from "
                        + DiagnosticOPRFileTable.TableName
                         + " where "
                        + DiagnosticOPRFileTable.DiagnosticOPRID + "=" + DiagnosticOPRID_
                       );
                    if (t.Rows.Count > 0)
                    {
                        return Convert.ToInt32(t.Rows[0][0].ToString());
                    }
                    else return 0;

                }
                catch (Exception ee)
                {
                    throw new Exception("GetDiagnostic_FileCount" + ee.Message);
                    return -1;
                }

            }
        }
        public class MaintenanceFaultSQL
        {
            DatabaseInterface DB;
            private static class MaintenanceFaultTable
            {
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_Fault";
                public const string MaintenanceOPRID = "MaintenanceOPRID";
                public const string ItemID = "ItemID";
                public const string FaultID = "FaultID";
                public const string FaultDate = "FaultDate";
                public const string FaultDesc = "FaultDesc";
                public const string FaultReport = "FaultReport";
            }
            private static class MaintenanceFault_Affictive_RepairOPR_Table
            {
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_Fault_Affictive_RepairOPR";
                public const string FaultID = "FaultID";
                public const string RepairOPRID = "RepairOPRID";
              
            }
            public MaintenanceFaultSQL(DatabaseInterface db)
            {
                DB = db;

            }

            public MaintenanceFault  Get_Fault_INFO_BYID(uint faultid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MaintenanceFaultTable.MaintenanceOPRID + ","
                    + MaintenanceFaultTable.ItemID + ","
                    + MaintenanceFaultTable.FaultDesc + ","
                    + MaintenanceFaultTable.FaultDate + ","
                    + MaintenanceFaultTable.FaultReport
                    + " from   "
                    + MaintenanceFaultTable.TableName
                    + " where "
                    + MaintenanceFaultTable.FaultID + "=" + faultid
                      );
                    if (t.Rows.Count == 1)
                    {
                        MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        Item Item_ = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][1].ToString()));
                        string faultdesc = t.Rows[0][2].ToString();
                        DateTime faultdate = Convert.ToDateTime(t.Rows[0][3].ToString());
                        string faultreport = t.Rows[0][4].ToString();
                        return new MaintenanceFault(MaintenanceOPR_, Item_, faultid, faultdate, faultdesc, faultreport);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Fault_INFO_BYID" + ee.Message);
                    return null;
                }
            
            }
            public List<MaintenanceFaultReport> GetMaintenanceOPR_Report_Fault_List(MaintenanceOPR MaintenanceOPR_)
            {
                List<MaintenanceFaultReport> FaultReportlist = new List<MaintenanceFaultReport>();

                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MaintenanceFaultTable.FaultID + ","
                    + MaintenanceFaultTable.ItemID + ","
                    + MaintenanceFaultTable.FaultDesc + ","
                    + MaintenanceFaultTable.FaultDate + ","
                    + MaintenanceFaultTable.FaultReport 
                    + " from   "
                    + MaintenanceFaultTable.TableName
                    + " where "
                    + MaintenanceFaultTable.MaintenanceOPRID + "=" + MaintenanceOPR_._Operation.OperationID
                      );
                    RepairOPRSQL RepairOPRSQL_ = new RepairOPRSQL(DB);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint faultid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        Item Item_ = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][1].ToString()));
                        string faultdesc = t.Rows[i][2].ToString();
                        DateTime  faultdate =Convert .ToDateTime ( t.Rows[i][3].ToString());
                        string faultreport = t.Rows[i][4].ToString();
                        List <RepairOPR> MaintenanceFault_Affictive_RepairOPRList =new  MaintenanceFaultSQL(DB).GetFault_Affictive_RepairOPR_List (faultid);
                        int tagscount = new MaintenanceTagSQL (DB).GetFault_MaintenanceTagCount(faultid);
                        FaultReportlist.Add(new MaintenanceFaultReport ( new MaintenanceFault (MaintenanceOPR_, Item_, faultid, faultdate, faultdesc,faultreport  ), MaintenanceFault_Affictive_RepairOPRList, tagscount));
                        
                    }
                    return FaultReportlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMaintenanceOPR_Report_Fault_List" + ee.Message);
                }
            }
            public List<MaintenanceFaultReport> GetItem_Fault_List(Item  Item_)
            {
                List<MaintenanceFaultReport> FaultReportlist = new List<MaintenanceFaultReport>();

                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MaintenanceFaultTable.FaultID + ","
                    + MaintenanceFaultTable.MaintenanceOPRID + ","
                    + MaintenanceFaultTable.FaultDesc + ","
                    + MaintenanceFaultTable.FaultDate + ","
                    + MaintenanceFaultTable.FaultReport 
                    + " from   "
                    + MaintenanceFaultTable.TableName
                    + " where "
                    + MaintenanceFaultTable.ItemID + "=" + Item_.ItemID
                      );
                    RepairOPRSQL RepairOPRSQL_ = new RepairOPRSQL(DB);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint faultid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(t.Rows[i][1].ToString()));
                        string faultdesc = t.Rows[i][2].ToString();
                        DateTime faultdate = Convert.ToDateTime(t.Rows[i][3].ToString());
                        string faultreport = t.Rows[i][4].ToString();
                        int tagscount = new MaintenanceTagSQL(DB).GetFault_MaintenanceTagCount(faultid);

                        List<RepairOPR > RepairOPRList = new MaintenanceFaultSQL (DB).GetFault_Affictive_RepairOPR_List (faultid);
                        FaultReportlist.Add(new MaintenanceFaultReport(new MaintenanceFault(MaintenanceOPR_, Item_, faultid, faultdate, faultdesc,faultreport  ), RepairOPRList, tagscount));

                    }
                    return FaultReportlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetItem_Fault_List" + ee.Message);
                    return FaultReportlist;
                }
            }
            public List<string > GetItem_FaultDescList(Item Item_)
            {
                List<string> FaultDesclist = new List<string>();

                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select distinct "
                    + MaintenanceFaultTable.FaultDesc 
                    + " from   "
                    + MaintenanceFaultTable.TableName
                    + " where "
                    + MaintenanceFaultTable.ItemID + "=" + Item_.ItemID
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        string faultdesc = t.Rows[i][0].ToString();
                        FaultDesclist.Add( faultdesc);

                    }
                    return FaultDesclist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetItem_FaultDescList" + ee.Message);
                    return FaultDesclist;
                }
            }
            public List<RepairOPR> GetFault_Affictive_RepairOPR_List(uint FaultID)
            {
                List<RepairOPR> RepairOPRlist = new List<RepairOPR>();

                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + MaintenanceFault_Affictive_RepairOPR_Table.RepairOPRID 
                    + " from   "
                    + MaintenanceFault_Affictive_RepairOPR_Table.TableName
                    + " where "
                    + MaintenanceFault_Affictive_RepairOPR_Table.FaultID + "=" + FaultID
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        RepairOPRlist.Add(new RepairOPRSQL(DB).Get_RepairOPR_INFO_BYID(Convert.ToUInt32(t.Rows[i][0].ToString())));

                    }
                    return RepairOPRlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetFault_Affictive_RepairOPR_List:" + ee.Message);
                    return RepairOPRlist;
                }
            }
        }
        public class RepairOPRSQL
        {
            DatabaseInterface DB;
            private static class RepairOPRTable
            {
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_RepairOPR";
                public const string MaintenanceOPRID = "MaintenanceOPRID";
                public const string RepairOPRID = "RepairOPRID";
                public const string RepairOPRDate = "RepairOPRDate";
   
                public const string RepairOPRDesc = "RepairOPRDesc";
                public const string RepairOPRReport = "RepairOPRReport";



            }
            public RepairOPRSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public RepairOPR Get_RepairOPR_INFO_BYID(uint repairopr_id)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + RepairOPRTable.MaintenanceOPRID  + ","
                    + RepairOPRTable.RepairOPRDate + ","
                    + RepairOPRTable.RepairOPRDesc + ","
                    + RepairOPRTable.RepairOPRReport
                    
                    + " from   "
                    + RepairOPRTable.TableName
                    + " where "
                   + RepairOPRTable.RepairOPRID + "=" + repairopr_id
                      );
                    if (t.Rows.Count == 1)
                    {
                        MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        DateTime repairopr_date = Convert.ToDateTime(t.Rows[0][1].ToString());
                        string repairdesc = t.Rows[0][2].ToString();
                        string repairreport = t.Rows[0][3].ToString();

      
                        int installeditems_count = new ItemOUTSQL(DB).GetItemsOUT_Count(new Operation(Operation.REPAIROPR, repairopr_id));

                        return new RepairOPR(repairopr_id, MaintenanceOPR_, repairopr_date,
                            repairdesc, repairreport, installeditems_count, 0);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_RepairOPR_INFO_BYID " + ee.Message);
                    return null;
                }
            }
            public List<RepairOPR> Get_MaintenanceOPR_RepairOPR_List(MaintenanceOPR MaintenanceOPR_)
            {
                List<RepairOPR> RepairOPRlist = new List<RepairOPR>();

                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + RepairOPRTable.RepairOPRID + ","
                    + RepairOPRTable.RepairOPRDate + ","
                    + RepairOPRTable.RepairOPRDesc + ","
                    + RepairOPRTable.RepairOPRReport
                    + " from   "
                    + RepairOPRTable.TableName
                    + " where "
                    + RepairOPRTable.MaintenanceOPRID  + "=" + MaintenanceOPR_._Operation.OperationID
                      );
                    for(int i=0;i<t.Rows .Count;i++)
                    {
                        uint  repairoprid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        DateTime repairopr_date = Convert.ToDateTime(t.Rows[i][1].ToString());
                        string repairdesc = t.Rows[i][2].ToString();
                        string repairreport = t.Rows[i][3].ToString();
                        int installeditems_count = new ItemOUTSQL(DB).GetItemsOUT_Count(new Operation(Operation.REPAIROPR, repairoprid));
                        RepairOPRlist.Add ( new RepairOPR(repairoprid, MaintenanceOPR_, repairopr_date,
                            repairdesc, repairreport, installeditems_count,0));

                    }
                    return RepairOPRlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_MaintenanceOPR_RepairOPR_List:" + ee.Message);
                    return RepairOPRlist;
                }
            }
           
        }
        public class MissedFaultItemSQL
        {
            DatabaseInterface DB;
            private static class Missed_Fault_Item_Table
            {
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_DiagnosticOPR_MissedFaultItem";
                public const string ID = "MissedFaultID";
                public const string Type = "Type";
                public const string DiagnosticOPRID = "DiagnosticOPRID";
                public const string ItemID = "ItemID";
                public const string Location = "Location";
                public const string Notes = "Notes";
            }
  
            public MissedFaultItemSQL(DatabaseInterface db)
            {
                DB = db;

            }
            public Missed_Fault_Item  GetMissedFaultItem_INFO_BYID(uint id)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + Missed_Fault_Item_Table.DiagnosticOPRID + ","
                    + Missed_Fault_Item_Table.ItemID + ","
                    + Missed_Fault_Item_Table.Type + ","
                    + Missed_Fault_Item_Table.Location + ","
                     + Missed_Fault_Item_Table.Notes
                    + " from   "
                    + Missed_Fault_Item_Table.TableName
                    + " where "
                    + Missed_Fault_Item_Table.ID + "=" + id
                      );
                    if (t.Rows.Count == 1)
                    {
                        DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
                        Item Item_ = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][1].ToString()));
                        bool type = Convert.ToInt32(t.Rows[0][2].ToString()) == 1 ? true : false; 
                        string location = t.Rows[0][3].ToString();
                        string notes = t.Rows[0][4].ToString();
                        int tags_count = new MaintenanceTagSQL(DB).GetMissedFaultItem_MaintenanceTagCount(id);
                        return new Missed_Fault_Item(id, type, DiagnosticOPR_, Item_, location, notes, tags_count);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMissedFaultItem_INFO_BYID:" + ee.Message  );
                    return null;
                }
              
            }
            public List<Missed_Fault_Item> DiagnosticOPR_GetMissed_Fault_Item_List(DiagnosticOPR DiagnosticOPR_)
            {
                List<Missed_Fault_Item> Missed_Fault_Itemlist = new List<Missed_Fault_Item>();

                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + Missed_Fault_Item_Table.ID  + ","
                    + Missed_Fault_Item_Table.Type  + ","
                    + Missed_Fault_Item_Table.ItemID + ","
                    + Missed_Fault_Item_Table.Location  + ","
                    + Missed_Fault_Item_Table.Notes 
                    + " from   "
                    + Missed_Fault_Item_Table.TableName
                    + " where "
                    + Missed_Fault_Item_Table.DiagnosticOPRID   + "=" + DiagnosticOPR_.DiagnosticOPRID
                      );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint id = Convert.ToUInt32(t.Rows[i][0].ToString());
                        bool type = Convert.ToInt32(t.Rows[i][1].ToString()) == 1 ? true : false ;
                        Item Item_ = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][2].ToString()));
                        string location = t.Rows[i][3].ToString();
                        string  notes = t.Rows[i][4].ToString();
                        int tags_count = new MaintenanceTagSQL (DB).GetMissedFaultItem_MaintenanceTagCount(id);
                        Missed_Fault_Itemlist.Add(new Missed_Fault_Item (id,type, DiagnosticOPR_, Item_, location , notes , tags_count ));

                    }
                    return Missed_Fault_Itemlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("DiagnosticOPR_GetMissed_Fault_Item_List:" + ee.Message);
                    return Missed_Fault_Itemlist;
                }
            }
            public List<Missed_Fault_Item> MaintenanceOPR_GetMissed_Fault_Item_List(MaintenanceOPR MaintenanceOPR_)
            {
                List<Missed_Fault_Item> Missed_Fault_Itemlist = new List<Missed_Fault_Item>();

                try
                {
                    List<DiagnosticOPRReport> DiagnosticOPRList = new DiagnosticOPRSQL(DB).Get_All_DiagnosticOPRReportList(MaintenanceOPR_);
                    for (int i = 0; i < DiagnosticOPRList.Count; i++)
                        Missed_Fault_Itemlist.AddRange(DiagnosticOPR_GetMissed_Fault_Item_List(DiagnosticOPRList[i]._DiagnosticOPR));
                    return Missed_Fault_Itemlist;
                }
                catch (Exception ee)
                {
                    throw new Exception("MaintenanceOPR_GetMissed_Fault_Item_List " + ee.Message);
                    return Missed_Fault_Itemlist;
                }
            }

        }
        public class MaintenanceTagSQL
        {
            public static class MaintenanceTagTable
            {
                public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_Tag";
                public const string TagID = "TagID";
                public const string FaultID = "FaultID";
                public const string DiagnosticOPRID = "DiagnosticOPRID";
                public const string MissedFaultItemID = "MissedFaultItemID";
                public const string TagINFO = "TagINFO";
            }
            //public static class DiagnosticOPR_Fault_TagTable
            //{
            //    public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_DiagnosticOPR_Fault_Link";
            //    public const string TagID = "TagID";
            //    public const string DiagnosticOPRID = "DiagnosticOPRID";
            //    public const string FaultID = "FaultID";
            //    public const string TagINFO = "TagINFO";
            //}
            //public static class DiagnosticOPR_MissedFaultItem_TagTable
            //{
            //    public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_DiagnosticOPR_MissedFaultItem_Link";
            //    public const string TagID = "TagID";
            //    public const string DiagnosticOPRID = "DiagnosticOPRID";
            //    public const string MissedFaultID = "MissedFaultID";
            //    public const string TagINFO = "TagINFO";
            //}
            //public static class Fault_MissedFaultItem_TagTable
            //{
            //    public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_Fault_MissedFaultItem_Link";
            //    public const string TagID = "TagID";
            //    public const string MissedFaultID = "MissedFaultID";
            //    public const string FaultID = "FaultID";
            //    public const string TagINFO = "TagINFO";
            //}
            DatabaseInterface DB;
            public MaintenanceTagSQL(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public MaintenanceTag GetMaintenanceTaginfo_ByID(uint Tagid)
            {
                try
                {
                    DataTable t = DB.GetData("select "
                    + MaintenanceTagTable.FaultID  + ","
                       + MaintenanceTagTable.DiagnosticOPRID   + ","
                        + MaintenanceTagTable.MissedFaultItemID  + ","
                         + MaintenanceTagTable.TagINFO
                       + " from  "
                       + MaintenanceTagTable.TableName
                       + " where "
                       + MaintenanceTagTable.TagID + "=" + Tagid);
                    if (t.Rows.Count == 1)
                    {
                        MaintenanceFault MaintenanceFault_;
                        try
                        {
                             MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(Convert.ToUInt32(t.Rows[0][0]));
                        }
                        catch
                        {
                            MaintenanceFault_ = null;
                        }
                        DiagnosticOPR DiagnosticOPR_;
                        try
                        {
                            DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID (Convert.ToUInt32(t.Rows[0][1]));
                        }
                        catch
                        {
                            DiagnosticOPR_ = null;
                        }
                        Missed_Fault_Item Missed_Fault_Item_;
                        try
                        {
                            Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(Convert.ToUInt32(t.Rows[0][2]));
                        }
                        catch
                        {
                            Missed_Fault_Item_ = null;
                        }
                        string taginfo = t.Rows[0][3].ToString();
                        return new MaintenanceTag(Tagid, taginfo, MaintenanceFault_, DiagnosticOPR_, Missed_Fault_Item_);
                    }
                    else return null;
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMaintenanceTaginfo_ByID" + ee.Message);
                    return null;
                }

            }

            public List<MaintenanceTag> Get_DiagnosticOPR_Tag_List(DiagnosticOPR DiagnosticOPR_)
            {
                List<MaintenanceTag> list = new List<MaintenanceTag>();
                try
                {
                    DataTable t = DB.GetData("select "
                            + MaintenanceTagTable.TagID  + ","
                     + MaintenanceTagTable.FaultID + ","
                         + MaintenanceTagTable.MissedFaultItemID + ","
                          + MaintenanceTagTable.TagINFO
                        + " from  "
                        + MaintenanceTagTable.TableName
                        + " where "
                       + MaintenanceTagTable.DiagnosticOPRID  + "=" + DiagnosticOPR_ .DiagnosticOPRID);
                    for (int i=0;i<t.Rows .Count;i++)
                    {
                        uint tagid = Convert.ToUInt32(Convert.ToUInt32(t.Rows[i][0]));
                        MaintenanceFault MaintenanceFault_;
                        try
                        {
                            MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(Convert.ToUInt32(t.Rows[i][1]));
                        }
                        catch
                        {
                            MaintenanceFault_ = null;
                        }
                        
                        Missed_Fault_Item Missed_Fault_Item_;
                        try
                        {
                            Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(Convert.ToUInt32(t.Rows[i][2]));
                        }
                        catch
                        {
                            Missed_Fault_Item_ = null;
                        }
                        string taginfo = t.Rows[0][3].ToString();
                        list.Add(new MaintenanceTag (tagid ,taginfo ,MaintenanceFault_,DiagnosticOPR_,Missed_Fault_Item_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_DiagnosticOPR_Tag_List" + ee.Message);
                    return list;
                }

            }
            public List<MaintenanceTag> Get_Fault_Tag_List(MaintenanceFault MaintenanceFault_)
            {
                List<MaintenanceTag> list = new List<MaintenanceTag>();
                try
                {
                    DataTable t = DB.GetData("select "
                            + MaintenanceTagTable.TagID + ","
                     + MaintenanceTagTable.DiagnosticOPRID  + ","
                         + MaintenanceTagTable.MissedFaultItemID + ","
                          + MaintenanceTagTable.TagINFO
                        + " from  "
                        + MaintenanceTagTable.TableName
                        + " where "
                       + MaintenanceTagTable.FaultID  + "=" + MaintenanceFault_.FaultID );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint tagid = Convert.ToUInt32(Convert.ToUInt32(t.Rows[i][0]));
                        DiagnosticOPR DiagnosticOPR_;
                        try
                        {
                            DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID (Convert.ToUInt32(t.Rows[i][1]));
                        }
                        catch
                        {
                            DiagnosticOPR_ = null;
                        }

                        Missed_Fault_Item Missed_Fault_Item_;
                        try
                        {
                            Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(Convert.ToUInt32(t.Rows[i][2]));
                        }
                        catch
                        {
                            Missed_Fault_Item_ = null;
                        }
                        string taginfo = t.Rows[0][3].ToString();
                        list.Add(new MaintenanceTag(tagid, taginfo, MaintenanceFault_, DiagnosticOPR_, Missed_Fault_Item_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_DiagnosticOPR_Tag_List" + ee.Message);
                    return list;
                }

            }
            public List<MaintenanceTag> GetMissedFaultItem_TagList(Missed_Fault_Item Missed_Fault_Item_)
            {
                List<MaintenanceTag> list = new List<MaintenanceTag>();
                try
                {
                    DataTable t = DB.GetData("select "
                      + MaintenanceTagTable.TagID + ","
                     + MaintenanceTagTable.FaultID  + ","
                     + MaintenanceTagTable.DiagnosticOPRID + ","

                          + MaintenanceTagTable.TagINFO
                        + " from  "
                        + MaintenanceTagTable.TableName
                        + " where "
                       + MaintenanceTagTable.MissedFaultItemID  + "=" + Missed_Fault_Item_.ID);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint tagid = Convert.ToUInt32(Convert.ToUInt32(t.Rows[i][0]));
                        MaintenanceFault MaintenanceFault_;
                        try
                        {
                            MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(Convert.ToUInt32(t.Rows[i][1]));
                        }
                        catch
                        {
                            MaintenanceFault_ = null;
                        }
                        DiagnosticOPR DiagnosticOPR_;
                        try
                        {
                            DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(Convert.ToUInt32(t.Rows[i][2]));
                        }
                        catch
                        {
                            DiagnosticOPR_ = null;
                        }

                        
                        string taginfo = t.Rows[0][3].ToString();
                        list.Add(new MaintenanceTag(tagid, taginfo, MaintenanceFault_, DiagnosticOPR_, Missed_Fault_Item_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_DiagnosticOPR_Tag_List" + ee.Message);
                    return list;
                }

            }
            public int GetDiagnosticOPR_TagsCount(uint DiagnosticOPRID_)
            {

                try
                {
                    DataTable t = DB.GetData("select count (*)   from  "
                    + MaintenanceTagTable.TableName
                    + " where "
                   + MaintenanceTagTable.DiagnosticOPRID  + "=" +  DiagnosticOPRID_);
                    return Convert.ToInt32(t.Rows [0][0]);
                }
                catch (Exception ee)
                {
                    throw new Exception("GetDiagnosticOPR_TagsCount" + ee.Message);
                    return -1;
                }

            }
       
         
            public int GetMissedFaultItem_MaintenanceTagCount(uint ID)
            {
                try
                {
                    DataTable t = DB.GetData("select count (*)   from  "
                    + MaintenanceTagTable.TableName
                    + " where "
                   + MaintenanceTagTable.MissedFaultItemID  + "=" + ID );
                    return Convert.ToInt32(t.Rows[0][0]);
                }
                catch (Exception ee)
                {
                    throw new Exception("GetMissedFaultItem_MaintenanceTagCount" + ee.Message);
                    return -1;
                }
            }
            public int GetFault_MaintenanceTagCount(uint FaultID)
            {
                try
                {
                    DataTable t = DB.GetData("select count (*)   from  "
                    + MaintenanceTagTable.TableName
                    + " where "
                   + MaintenanceTagTable.FaultID  + "=" + FaultID);
                    return Convert.ToInt32(t.Rows[0][0]);
                }
                catch (Exception ee)
                {
                    throw new Exception("GetFault_MaintenanceTagCount" + ee.Message);
                    return -1;
                }
            }
            public bool  Delete_Fault_Tags(uint FaultID)
            {
                try
                {
                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = DB.GetData("delete   from  "
                    + MaintenanceTagTable.TableName
                    + " where "
                   + MaintenanceTagTable.FaultID + "=" + FaultID);
                    return true;
                }
                catch (Exception ee)
                {
                    throw new Exception("Delete_Fault_Tags" + ee.Message);
                    return false ;
                }
            }
            public bool Delete_DiagnosticOPR_Tags(uint diagnosticoprID)
            {
                try
                {
                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = DB.GetData("delete   from  "
                    + MaintenanceTagTable.TableName
                    + " where "
                   + MaintenanceTagTable.DiagnosticOPRID  + "=" + diagnosticoprID);
                    return true;
                }
                catch (Exception ee)
                {
                    throw new Exception("Delete_DiagnosticOPR_Tags" + ee.Message);
                    return false;
                }
            }
            public bool Delete_MissedFault_Tags(uint ID)
            {
                try
                {
                    //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Maintenance_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = DB.GetData("delete   from  "
                    + MaintenanceTagTable.TableName
                    + " where "
                   + MaintenanceTagTable.MissedFaultItemID  + "=" + ID);
                    return true;
                }
                catch (Exception ee)
                {
                    throw new Exception("Delete_MissedFault_Tags" + ee.Message);
                    return false;
                }
            }


        }
        //public class DiagnosticOPR_Fault_TagSQL
        //{
          
        //    public static class DiagnosticOPR_Fault_TagTable
        //    {
        //        public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_DiagnosticOPR_Fault_Link";
        //        public const string TagID = "TagID";
        //        public const string DiagnosticOPRID = "DiagnosticOPRID";
        //        public const string FaultID = "FaultID";
        //        public const string TagINFO = "TagINFO";
        //    }
           
        //    DatabaseInterface DB;
        //    public DiagnosticOPR_Fault_TagSQL(DatabaseInterface DB_)
        //    {
        //        DB = DB_;
        //    }
        //    public DiagnosticOPR_Fault_Tag Get_DiagnosticOPR_Fault_Tag_info_ByID(uint Tagid)
        //    {
        //        try
        //        {
        //            DataTable t = DB.GetData("select "
        //            + DiagnosticOPR_Fault_TagTable.DiagnosticOPRID + ","
        //                + DiagnosticOPR_Fault_TagTable.FaultID + ","
        //                 + DiagnosticOPR_Fault_TagTable.TagINFO
        //               + " from  "
        //               + DiagnosticOPR_Fault_TagTable.TableName
        //               + " where "
        //               + DiagnosticOPR_Fault_TagTable.TagID + "=" + Tagid);
        //            if (t.Rows.Count == 1)
        //            {
        //                DiagnosticOPR DiagnosticOPR_= new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
        //                 MaintenanceFault MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(Convert.ToUInt32(t.Rows[0][1].ToString()));
        //                string taginfo = t.Rows[0][2].ToString();
        //                return new DiagnosticOPR_Fault_Tag(Tagid, MaintenanceFault_, DiagnosticOPR_, taginfo);
        //            }
        //            else
        //                return null;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_DiagnosticOPR_Faul_Tag_info_ByID" + ee.Message);
        //            return null;
        //        }

        //    }
        //    public bool Add_DiagnosticOPR_Fault_Tag(DiagnosticOPR DiagnosticOPR_, 
        //        MaintenanceFault MaintenanceFault_, string taginfo)
        //    {
        //        try
        //        {

        //            DB.ExecuteSQLCommand("","insert into  "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                + " ( "
        //                + DiagnosticOPR_Fault_TagTable.DiagnosticOPRID + ","
        //                 + DiagnosticOPR_Fault_TagTable.FaultID + ","
        //                + DiagnosticOPR_Fault_TagTable.TagINFO
        //                + ")values( "
        //                +  DiagnosticOPR_.DiagnosticOPRID.ToString() + ","
        //                +  MaintenanceFault_.FaultID.ToString() + ","
        //                + "'" + taginfo + "'"
        //                + ")"
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Add_DiagnosticOPR_Fault_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public bool Update_DiagnosticOPR_Fault_Tag(uint TagID, string INFO)
        //    {
        //        try
        //        {
        //            DB.ExecuteSQLCommand("","update   "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                + " set "
        //                + DiagnosticOPR_Fault_TagTable.TagINFO + "='" + INFO + "',"
        //                + " where "
        //                + DiagnosticOPR_Fault_TagTable.TagID + "=" + TagID
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("_DiagnosticOPR_Fault_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public bool Delete_DiagnosticOPR_Fault_Tag(uint TagID)
        //    {
        //        try
        //        {


        //            DB.ExecuteSQLCommand("","Delete from    "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                + " where "
        //                + DiagnosticOPR_Fault_TagTable.TagID + "=" + TagID 
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("DeleteMeasureOPR" + ee.Message);
        //            return false;
        //        }
        //    }
        
        //    public List<DiagnosticOPR_Fault_Tag> Get_DiagnosticOPR_Fault_TagList(DiagnosticOPR DiagnosticOPR_)
        //    {
        //        List<MaintenanceTagSummary> list = new List<MaintenanceTagSummary>();
        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select "
        //                + DiagnosticOPR_Fault_TagTable.TagID + ","
        //                 + DiagnosticOPR_Fault_TagTable.FaultID + ","
        //                 + DiagnosticOPR_Fault_TagTable.TagINFO
        //                + " from "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_Fault_TagTable.DiagnosticOPRID + "=" + DiagnosticOPR_.DiagnosticOPRID
        //               );

        //            for (int i = 0; i < t.Rows.Count; i++)
        //            {
        //                uint Tagid = Convert.ToUInt32(t.Rows[i][0].ToString());
        //                uint id;
        //                string desc;

        //                MaintenanceFault MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(Convert.ToUInt32(t.Rows[0][1].ToString()));
        //                id = MaintenanceFault_.FaultID;
        //                desc = MaintenanceFault_.FaultDesc;
        //                string taginfo = t.Rows[0][2].ToString();
        //                list.Add(new MaintenanceTagSummary(Tagid, MaintenanceTagSummary.DiagnosicOPR_Fault_Tag_Type, id, desc, taginfo));
        //            }
        //            return list;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_DiagnosticOPR_Fault_TagList" + ee.Message);
        //            return list;
        //        }

        //    }
        //    public int Get_DiagnosticOPR_Fault_TagCount_ForDiagnosticOPR(uint DiagnosticOPRID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select count(*) from "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_Fault_TagTable.DiagnosticOPRID + "=" + DiagnosticOPRID
        //               );
        //            if (t.Rows.Count > 0)
        //            {
        //                return Convert.ToInt32(t.Rows[0][0].ToString());
        //            }
        //            else return 0;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("GetDiagnostic_MaintenanceTagCount" + ee.Message);
        //            return -1;
        //        }

        //    }
        //    public bool Delete_DiagnosticOPR_Fault_Tags_ByDiagnosticOPR(uint DiagnosticOPRID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("delete  from "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_Fault_TagTable.DiagnosticOPRID + "=" + DiagnosticOPRID 
        //               );
        //            return true;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("DeleteDiagnosticOPR_Tags" + ee.Message);
        //        }

        //    }
        //    public List<DiagnosticOPR_Fault_Tag> Get_DiagnosticOPR_Fault_TagList(MaintenanceFault MaintenanceFault_)
        //    {
        //        List<MaintenanceTagSummary> list = new List<MaintenanceTagSummary>();
        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select "
        //                + DiagnosticOPR_Fault_TagTable.TagID + ","
        //                 + DiagnosticOPR_Fault_TagTable.DiagnosticOPRID + ","
        //                 + DiagnosticOPR_Fault_TagTable.TagINFO
        //                + " from "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_Fault_TagTable.FaultID + "=" + MaintenanceFault_.FaultID
        //               );

        //            for (int i = 0; i < t.Rows.Count; i++)
        //            {
        //                uint Tagid = Convert.ToUInt32(t.Rows[i][0].ToString());
        //                uint id;
        //                string desc;
        //                 DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(Convert.ToUInt32(t.Rows[0][1].ToString()));
        //                 id = DiagnosticOPR_.DiagnosticOPRID;
        //                 desc = DiagnosticOPR_.Desc;


        //                string taginfo = t.Rows[0][2].ToString();
        //                list.Add(new MaintenanceTagSummary(Tagid, MaintenanceTagSummary.DiagnosicOPR_Fault_Tag_Type, id, desc, taginfo));
        //            }
        //            return list;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("GetFault_TagSummarryList" + ee.Message);
        //            return list;
        //        }

        //    }
        //    public int Get_DiagnosticOPR_Fault_TagCount_ForFault(uint FaultID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select count(*) from "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_Fault_TagTable.FaultID + "=" + FaultID
        //               );
        //            if (t.Rows.Count > 0)
        //            {
        //                return Convert.ToInt32(t.Rows[0][0].ToString());
        //            }
        //            else return 0;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("GetDiagnosticOPR_Fault_TagCount" + ee.Message);
        //            return -1;
        //        }

        //    }
        //    public bool Delete_DiagnosticOPR_Fault_Tags_ByFault(uint FaultID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("delete  from "
        //                + DiagnosticOPR_Fault_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_Fault_TagTable.FaultID + "=" + FaultID
        //               );
        //            return true;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Delete_DiagnosticOPR_Fault_Tags" + ee.Message);
        //        }

        //    }
        //}
        //public class DiagnosticOPR_MissedFaultItem_TagSQL
        //{
  
        //    public static class DiagnosticOPR_MissedFaultItem_TagTable
        //    {
        //        public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_DiagnosticOPR_MissedFaultItem_Link";
        //        public const string TagID = "TagID";
        //        public const string DiagnosticOPRID = "DiagnosticOPRID";
        //        public const string MissedFaultID = "MissedFaultID";
        //        public const string TagINFO = "TagINFO";
        //    }
    
        //    DatabaseInterface DB;
        //    public DiagnosticOPR_MissedFaultItem_TagSQL(DatabaseInterface DB_)
        //    {
        //        DB = DB_;
        //    }
        //    public DiagnosticOPR_MissedFaultItem_Tag Get_DiagnosticOPR_MissedFaultItem_Tag_info_ByID(uint Tagid)
        //    {
        //        try
        //        {
        //            DataTable t = DB.GetData("select "
        //            + DiagnosticOPR_MissedFaultItem_TagTable.DiagnosticOPRID + ","
        //               + DiagnosticOPR_MissedFaultItem_TagTable.MissedFaultID + ","
        //                 + DiagnosticOPR_MissedFaultItem_TagTable.TagINFO
        //               + " from  "
        //               + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //               + " where "
        //               + DiagnosticOPR_MissedFaultItem_TagTable.TagID + "=" + Tagid);
        //            if (t.Rows.Count == 1)
        //            {
        //                DiagnosticOPR DiagnosticOPR_= new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
        //                Missed_Fault_Item Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(Convert.ToUInt32(t.Rows[0][1].ToString()));
        //                string taginfo = t.Rows[0][2].ToString();
        //                return new DiagnosticOPR_MissedFaultItem_Tag(Tagid, DiagnosticOPR_, Missed_Fault_Item_, taginfo);
        //            }
        //            else
        //                return null;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_DiagnosticOPR_MissedFaultItem_Tag_info_ByID" + ee.Message);
        //            return null;
        //        }

        //    }
        //    public bool Add_DiagnosticOPR_MissedFaultItem_Tag(DiagnosticOPR DiagnosticOPR_, Missed_Fault_Item Missed_Fault_Item_,
        //         string taginfo)
        //    {

        //        try
        //        {

        //            DB.ExecuteSQLCommand("","insert into  "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                + " ( "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.DiagnosticOPRID + ","
        //                + DiagnosticOPR_MissedFaultItem_TagTable.MissedFaultID + ","
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TagINFO
        //                + ")values( "
        //                + DiagnosticOPR_.DiagnosticOPRID.ToString() + ","
        //                +  Missed_Fault_Item_.ID.ToString() + ","
        //                + "'" + taginfo + "'"
        //                + ")"
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Add_DiagnosticOPR_MissedFaultItem_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public bool Update_DiagnosticOPR_MissedFaultItem_Tag(uint TagID, string INFO)
        //    {
        //        try
        //        {
        //            DB.ExecuteSQLCommand("","update   "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                + " set "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TagINFO + "='" + INFO + "',"
        //                + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TagID + "=" + TagID
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Update_DiagnosticOPR_MissedFaultItem_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public bool Delete_DiagnosticOPR_MissedFaultItem_Tag(uint TagID)
        //    {
        //        try
        //        {


        //            DB.ExecuteSQLCommand("","Delete from    "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TagID + "=" + TagID
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Delete_DiagnosticOPR_MissedFaultItem_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public List<MaintenanceTagSummary> Get_DiagnosticOPR_MissedFaultItem_TagList(DiagnosticOPR DiagnosticOPR_)
        //    {
        //        List<MaintenanceTagSummary> list = new List<MaintenanceTagSummary>();
        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TagID + ","
        //                 + DiagnosticOPR_MissedFaultItem_TagTable.MissedFaultID + ","
        //                 + DiagnosticOPR_MissedFaultItem_TagTable.TagINFO
        //                + " from "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.DiagnosticOPRID + "=" + DiagnosticOPR_.DiagnosticOPRID
        //               );

        //            for (int i = 0; i < t.Rows.Count; i++)
        //            {
        //                uint Tagid = Convert.ToUInt32(t.Rows[i][0].ToString());
        //                uint id;
        //                string desc;
        //                    Missed_Fault_Item Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID (Convert.ToUInt32(t.Rows[i][1].ToString()));

        //                    id = Missed_Fault_Item_.ID;
        //                    desc = "الموقع :" + Missed_Fault_Item_.Location;
   

        //                string taginfo = t.Rows[i][2].ToString();
        //                list.Add(new MaintenanceTagSummary(Tagid, MaintenanceTagSummary.DiagnosicOPR_MissedFaultItem_Tag_Type, id, desc, taginfo));
        //            }
        //            return list;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_DiagnosticOPR_MissedFaultItem_TagList" + ee.Message);
        //            return list;
        //        }

        //    }
        //    public int Get_DiagnosticOPR_MissedFaultItem_TagCount_ForDiagnosticOPR(uint DiagnosticOPRID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select count(*) from "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.DiagnosticOPRID + "=" + DiagnosticOPRID
        //               );
        //            if (t.Rows.Count > 0)
        //            {
        //                return Convert.ToInt32(t.Rows[0][0].ToString());
        //            }
        //            else return 0;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_DiagnosticOPR_MissedFaultItem_TagCount" + ee.Message);
        //            return -1;
        //        }

        //    }
        //    public bool Delete_DiagnosticOPR_MissedFaultItem_Tags_ByDiagnosticOPR(uint DiagnosticOPRID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("delete  from "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.DiagnosticOPRID + "=" + DiagnosticOPRID
        //               );
        //            return true;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Delete_DiagnosticOPR_MissedFaultItem_Tags" + ee.Message);
        //        }

        //    }
        //    public List<MaintenanceTagSummary> Get_DiagnosticOPR_MissedFaultItem_TagList(Missed_Fault_Item Missed_Fault_Item_)
        //    {
        //        List<MaintenanceTagSummary> list = new List<MaintenanceTagSummary>();
        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TagID + ","
        //                 + DiagnosticOPR_MissedFaultItem_TagTable.DiagnosticOPRID + ","
        //                 + DiagnosticOPR_MissedFaultItem_TagTable.TagINFO
        //                + " from "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.MissedFaultID + "=" + Missed_Fault_Item_.ID
        //               );

        //            for (int i = 0; i < t.Rows.Count; i++)
        //            {
        //                uint Tagid = Convert.ToUInt32(t.Rows[i][0].ToString());
        //                uint id;
        //                string desc;
        //                DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(Convert.ToUInt32(t.Rows[i][1].ToString()));
        //                id = DiagnosticOPR_.DiagnosticOPRID;
        //                desc = DiagnosticOPR_.Desc;
        //                string taginfo = t.Rows[i][2].ToString();
        //                list.Add(new MaintenanceTagSummary(Tagid, MaintenanceTagSummary.DiagnosicOPR_MissedFaultItem_Tag_Type, id, desc, taginfo));
        //            }
        //            return list;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_DiagnosticOPR_MissedFaultItem_TagList" + ee.Message);
        //            return list;
        //        }

        //    }
        //    public int Get_DiagnosticOPR_MissedFaultItem_TagCount_ForMissedFaultItem(uint ID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select count(*) from "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.MissedFaultID + "=" + ID
        //               );
        //            if (t.Rows.Count > 0)
        //            {
        //                return Convert.ToInt32(t.Rows[0][0].ToString());
        //            }
        //            else return 0;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Ge_DiagnosticOPR_MissedFaultItem_TagCount" + ee.Message);
        //            return -1;
        //        }

        //    }
        //    public bool Delete_DiagnosticOPR_MissedFaultItem_Tags_ByMissedFaultItem(uint ID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("delete  from "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + DiagnosticOPR_MissedFaultItem_TagTable.MissedFaultID + "=" + ID 
        //               );
        //            return true;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Delete_DiagnosticOPR_MissedFaultItem_Tags" + ee.Message);
        //        }

        //    }
       
        //}
        //public class Fault_MissedFaultItem_TagSQL
        //{
  
        //    public static class Fault_MissedFaultItem_TagTable
        //    {
        //        public const string TableName = "Trade_BillMaintenance_MaintenanceOPR_Fault_MissedFaultItem_Link";
        //        public const string TagID = "TagID";
        //        public const string MissedFaultID = "MissedFaultID";
        //        public const string FaultID = "FaultID";
        //        public const string TagINFO = "TagINFO";
        //    }
        //    DatabaseInterface DB;
        //    public Fault_MissedFaultItem_TagSQL(DatabaseInterface DB_)
        //    {
        //        DB = DB_;
        //    }
        //    public Fault_MissedFaultItem_Tag Get_Fault_MissedFaultItem_Tag_info_ByID(uint Tagid)
        //    {
        //        try
        //        {
        //            DataTable t = DB.GetData("select "
        //            + Fault_MissedFaultItem_TagTable.FaultID  + ","
        //               + Fault_MissedFaultItem_TagTable.MissedFaultID + ","
        //                 + Fault_MissedFaultItem_TagTable.TagINFO
        //               + " from  "
        //               + Fault_MissedFaultItem_TagTable.TableName
        //               + " where "
        //               + Fault_MissedFaultItem_TagTable.TagID + "=" + Tagid);
        //            if (t.Rows.Count == 1)
        //            {
        //                MaintenanceFault MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(Convert.ToUInt32(t.Rows[0][0].ToString()));
        //                Missed_Fault_Item Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID (Convert.ToUInt32(t.Rows[0][1].ToString()));
        //                string taginfo = t.Rows[0][2].ToString();
        //                return new Fault_MissedFaultItem_Tag(Tagid, MaintenanceFault_, Missed_Fault_Item_, taginfo);
        //            }
        //            else
        //                return null;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_Fault_MissedFaultItem_Tag_info_ByID" + ee.Message);
        //            return null;
        //        }

        //    }
        //    public bool Add_Fault_MissedFaultItem_Tag(MaintenanceFault MaintenanceFault_, Missed_Fault_Item Missed_Fault_Item_,
        //         string taginfo)
        //    {
        //        try
        //        {

        //            DB.ExecuteSQLCommand("","insert into  "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                + " ( "
        //                + Fault_MissedFaultItem_TagTable.FaultID  + ","
        //                + Fault_MissedFaultItem_TagTable.MissedFaultID + ","
        //                + Fault_MissedFaultItem_TagTable.TagINFO
        //                + ")values( "
        //                + MaintenanceFault_.FaultID .ToString() + ","
        //                + Missed_Fault_Item_.ID.ToString() + ","
        //                + "'" + taginfo + "'"
        //                + ")"
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Add_Fault_MissedFaultItem_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public bool Update_Fault_MissedFaultItem_Tag(uint TagID, string INFO)
        //    {
        //        try
        //        {
        //            DB.ExecuteSQLCommand("","update   "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                + " set "
        //                + Fault_MissedFaultItem_TagTable.TagINFO + "='" + INFO + "',"
        //                + " where "
        //                + Fault_MissedFaultItem_TagTable.TagID + "=" + TagID
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Update_Fault_MissedFaultItem_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public bool Delete__Fault_MissedFaultItem_Tag(uint TagID)
        //    {
        //        try
        //        {


        //            DB.ExecuteSQLCommand("","Delete from    "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                + " where "
        //                + Fault_MissedFaultItem_TagTable.TagID + "=" + TagID
        //                );
        //            return true;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Delete__Fault_MissedFaultItem_Tag" + ee.Message);
        //            return false;
        //        }
        //    }
        //    public List<MaintenanceTagSummary> Get_Fault_MissedFaultItem_TagList(MaintenanceFault MaintenanceFault_)
        //    {
        //        List<MaintenanceTagSummary> list = new List<MaintenanceTagSummary>();
        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select "
        //                + Fault_MissedFaultItem_TagTable.TagID + ","
        //                 + Fault_MissedFaultItem_TagTable.MissedFaultID + ","
        //                 + Fault_MissedFaultItem_TagTable.TagINFO
        //                + " from "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + Fault_MissedFaultItem_TagTable.FaultID  + "=" + MaintenanceFault_.FaultID 
        //               );

        //            for (int i = 0; i < t.Rows.Count; i++)
        //            {
        //                uint Tagid = Convert.ToUInt32(t.Rows[i][0].ToString());
        //                uint id;
        //                string desc;
        //                Missed_Fault_Item Missed_Fault_Item_ = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID (Convert.ToUInt32(t.Rows[i][1].ToString()));
        //                id = Missed_Fault_Item_.ID;
        //                desc = "الموقع :" + Missed_Fault_Item_.Location;
        //                string taginfo = t.Rows[i][2].ToString();
        //                list.Add(new MaintenanceTagSummary(Tagid, MaintenanceTagSummary.Fault_MissedFaultItem_Tag_Type , id, desc, taginfo));
        //            }
        //            return list;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_Fault_MissedFaultItem_TagList" + ee.Message);
        //            return list;
        //        }

        //    }
        //    public int Get_Fault_MissedFaultItem_TagCount_ForFault(uint FaultID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select count(*) from "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + Fault_MissedFaultItem_TagTable.FaultID  + "=" + FaultID 
        //               );
        //            if (t.Rows.Count > 0)
        //            {
        //                return Convert.ToInt32(t.Rows[0][0].ToString());
        //            }
        //            else return 0;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_Fault_MissedFaultItem_TagCount" + ee.Message);
        //            return -1;
        //        }

        //    }
        //    public bool Delete_Fault_MissedFaultItem_Tags_ByFault(uint FaultID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("delete  from "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + Fault_MissedFaultItem_TagTable.FaultID  + "=" + FaultID 
        //               );
        //            return true;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Delete_Fault_MissedFaultItem_TagTags" + ee.Message);
        //        }

        //    }
        //    public List<MaintenanceTagSummary> Get_Fault_MissedFaultItem_TagList(Missed_Fault_Item Missed_Fault_Item_)
        //    {
        //        List<MaintenanceTagSummary> list = new List<MaintenanceTagSummary>();
        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select "
        //                + Fault_MissedFaultItem_TagTable.TagID + ","
        //                 + Fault_MissedFaultItem_TagTable.FaultID  + ","
        //                 + Fault_MissedFaultItem_TagTable.TagINFO
        //                + " from "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + Fault_MissedFaultItem_TagTable.MissedFaultID + "=" + Missed_Fault_Item_.ID
        //               );

        //            for (int i = 0; i < t.Rows.Count; i++)
        //            {
        //                uint Tagid = Convert.ToUInt32(t.Rows[i][0].ToString());
        //                uint id;
        //                string desc;
        //                MaintenanceFault MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID (Convert.ToUInt32(t.Rows[i][1].ToString()));
        //                id = MaintenanceFault_.FaultID ;
        //                desc = MaintenanceFault_.FaultDesc ;
        //                string taginfo = t.Rows[i][2].ToString();
        //                list.Add(new MaintenanceTagSummary(Tagid, MaintenanceTagSummary.Fault_MissedFaultItem_Tag_Type , id, desc, taginfo));
        //            }
        //            return list;
        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_Fault_MissedFaultItem_TagList" + ee.Message);
        //            return list;
        //        }

        //    }
        //    public int Get_Fault_MissedFaultItem_TagCount_ForMissedFaultItem(uint ID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("select count(*) from "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + Fault_MissedFaultItem_TagTable.MissedFaultID + "=" + ID
        //               );
        //            if (t.Rows.Count > 0)
        //            {
        //                return Convert.ToInt32(t.Rows[0][0].ToString());
        //            }
        //            else return 0;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Get_Fault_MissedFaultItem_TagCount" + ee.Message);
        //            return -1;
        //        }

        //    }
        //    public bool Delete_Fault_MissedFaultItem_Tags_ByMissedFaultItem(uint ID)
        //    {

        //        try
        //        {

        //            DataTable t = new DataTable();
        //            t = DB.GetData("delete  from "
        //                + Fault_MissedFaultItem_TagTable.TableName
        //                 + " where "
        //                + Fault_MissedFaultItem_TagTable.MissedFaultID + "=" + ID
        //               );
        //            return true;

        //        }
        //        catch (Exception ee)
        //        {
        //            throw new Exception("Delete_Fault_MissedFaultItem_Tags" + ee.Message);
        //        }

        //    }
        //}
    }
}
