using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Trade.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Trade
{
    namespace TradeSQL
    {
        public class IndustrySQL
        {
            DatabaseInterface DB;
     
            public IndustrySQL(DatabaseInterface db)
            {
                DB = db;

            }
            public List<Industrial_OPR > GetIndustrial_Operations()
            {
                List<Industrial_OPR> Industrial_OPRList = new List<Industrial_OPR>();
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    List<DisAssemblabgeOPR> DisAssemblabgeOPR_List = new DisAssemblageSQL(DB).Get_All_DisAssemblageOPR();
                    for (int i = 0; i < DisAssemblabgeOPR_List.Count; i++)
                    {
                        Money_Currency Money_Currency_ = null;
                        Item Item_ = null;
                        double? Amount = null;
                        ConsumeUnit consumeunit = null;
                        string TradeStateName = "-";
                        if (DisAssemblabgeOPR_List[i]._ItemOUT != null)
                        {
                            Money_Currency_ = new Money_Currency(DisAssemblabgeOPR_List[i]._ItemOUT._OUTValue._Currency,
                                DisAssemblabgeOPR_List[i]._ItemOUT._OUTValue.Value, DisAssemblabgeOPR_List[i]._ItemOUT._OUTValue.ExchangeRate);
                            Item_ = DisAssemblabgeOPR_List[i]._ItemOUT._ItemIN._Item;
                            Amount = DisAssemblabgeOPR_List[i]._ItemOUT.Amount;
                            consumeunit = DisAssemblabgeOPR_List[i]._ItemOUT._ConsumeUnit;
                            TradeStateName = DisAssemblabgeOPR_List[i]._ItemOUT._ItemIN._TradeState.TradeStateName;
                        }
                        int itemin_count = new ItemINSQL(DB).GetItemINList(DisAssemblabgeOPR_List[i]._Operation).Count();
                        Industrial_OPRList.Add(new Industrial_OPR(DisAssemblabgeOPR_List[i]._Operation, DisAssemblabgeOPR_List[i].OprDate
                            , DisAssemblabgeOPR_List[i].OprDesc, Amount, consumeunit,
                          Item_,TradeStateName, Money_Currency_, "عدد العناصر الناتجة :"+itemin_count));

                    }
                    List<AssemblabgeOPR> AssemblabgeOPR_List = new AssemblageSQL(DB).Get_All_AssemblageOPR();
                    for (int i = 0; i < AssemblabgeOPR_List.Count; i++)
                    {
                        Money_Currency Money_Currency_ = null;
                        Item Item_ = null;
                        double? Amount = null;
                        ConsumeUnit consumeunit = null;
                        string TradeStateName = "-";
                        if (AssemblabgeOPR_List[i]._ItemIN  != null)
                        {
                            Money_Currency_ = new Money_Currency(AssemblabgeOPR_List[i]._ItemIN._INCost._Currency,
                                AssemblabgeOPR_List[i]._ItemIN._INCost.Value, AssemblabgeOPR_List[i]._ItemIN ._INCost.ExchangeRate);
                            Item_ = AssemblabgeOPR_List[i]._ItemIN._Item;
                            Amount = AssemblabgeOPR_List[i]._ItemIN.Amount;
                            consumeunit = AssemblabgeOPR_List[i]._ItemIN._ConsumeUnit;
                            TradeStateName = AssemblabgeOPR_List[i]._ItemIN._TradeState.TradeStateName;
                        }
                        int itemsout_count = new ItemOUTSQL(DB).GetItemOUTList(AssemblabgeOPR_List[i]._Operation).Count ;
                        Industrial_OPRList.Add(new Industrial_OPR(AssemblabgeOPR_List[i]._Operation, AssemblabgeOPR_List[i].OprDate
                            , AssemblabgeOPR_List[i].OprDesc, Amount, consumeunit,
                          Item_,TradeStateName , Money_Currency_, "عدد العناصر المساهمة بتجميع:"+itemsout_count));

                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetIndustrial_Operations:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return Industrial_OPRList;
            }
        }
         public class AssemblageSQL
        {
            DatabaseInterface DB;
            private static class AssemblageTable
            {
                public const string TableName = "Trade_Assemblage";
                public const string AssemblageID = "AssemblageID";
                public const string OprDate = "OprDate";
                public const string OprDesc = "OprDesc";
                public const string Notes = "Notes";
                //public const string CurrencyID = "CurrencyID";
                //public const string ExchangeRate = "ExchangeRate";

            }
            public AssemblageSQL(DatabaseInterface db)
            {
                DB = db;

            }

            public AssemblabgeOPR GetAssemblageOPR_INFO_BYID(uint oprid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + AssemblageTable.OprDate + ","
                    + AssemblageTable.OprDesc  + ","
                    + AssemblageTable.Notes 
                    + " from   "
                    + AssemblageTable.TableName
                    + " where "
                    + AssemblageTable.AssemblageID  + "=" + oprid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[0][0]);
                        string oprdesc = t.Rows[0][1].ToString();
                        string notes = t.Rows[0][2].ToString();
 
                        Operation operation = new Operation(Operation.ASSEMBLAGE , oprid);
                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(operation);
                        ItemIN ItemIN_ = null;
                        if (iteminlist.Count > 0)
                            ItemIN_ = iteminlist[0];
                        return new AssemblabgeOPR (oprid, oprdate  ,oprdesc  
                            , notes, ItemIN_);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetAssemblageOPR_INFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public AssemblabgeOPR GetAssemblageOPR_INFO_BY_RowID(uint rowid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + AssemblageTable.OprDate + ","
                    + AssemblageTable.OprDesc  + ","
                    + AssemblageTable.Notes + ","
                    + AssemblageTable.AssemblageID
                    + " from   "
                    + AssemblageTable.TableName
                    + " where "
                   +DatabaseInterface.ROWID_COLUMN+ "=" + rowid 
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[0][0]);
                        string oprdesc = t.Rows[0][1].ToString();
                        string notes = t.Rows[0][2].ToString();
                        uint oprid = Convert.ToUInt32(t.Rows[0][3].ToString());
                        Operation operation = new Operation(Operation.ASSEMBLAGE, oprid);

                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(operation);
                        ItemIN ItemIN_ = null;
                        if (iteminlist.Count > 0)
                            ItemIN_ = iteminlist[0];
                        return new AssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemIN_);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetAssemblageOPR_INFO_BY_RowID:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }

            public AssemblabgeOPR CreateAssemblageOPR
                (DateTime oprdate,string oprdesc,  string notes
             )
            {
                try
                {
             
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = DB.GetData(" insert into "
                    + AssemblageTable.TableName
                    + "("
                    + AssemblageTable.OprDate + ","
                     + AssemblageTable.OprDesc  + ","
                    + AssemblageTable.Notes  
                    + ")"
                    + "values"
                    + "("
                    + "'" + oprdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + "'" + oprdesc  + "'"
                     + ","
                    + "'" + notes + "'"
                    + ")"
                    + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    AssemblabgeOPR AssemblabgeOPR_ = GetAssemblageOPR_INFO_BY_RowID(rowid);

     
                    
                    DB.AddLog(
                                    DatabaseInterface.Log.LogType.INSERT 
                                    , DatabaseInterface.Log.Log_Target.Trade_Assemblage
                                     , ""
                                     , true, "");
                    return AssemblabgeOPR_;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT 
                            , DatabaseInterface.Log.Log_Target.Trade_Assemblage
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("CreateAssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
            }
            public bool UpdateAssemblageOPR
                 (uint oprid, DateTime oprdate, string oprdesc,string notes
)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                      DB.ExecuteSQLCommand("update  "
                    + AssemblageTable.TableName
                    + " set "
                    + AssemblageTable.OprDate + "='" + oprdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + AssemblageTable.OprDesc  + "='" + oprdesc + "'"
                    + ","
                    + AssemblageTable.Notes  + "='" + notes  + "'"
                   

                    + " where "
                    + AssemblageTable.AssemblageID  + "=" + oprid
                    );
                    DB.AddLog(
                                    DatabaseInterface.Log.LogType.UPDATE
                                    , DatabaseInterface.Log.Log_Target.Trade_Assemblage 
                                     , ""
                                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE
                            , DatabaseInterface.Log.Log_Target.Trade_Assemblage 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("UpdateAssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteDisAssemblageOPR(uint oprid)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (new ItemOUTSQL(DB).Does_Operation_Has_ItemsOUT(Operation.ASSEMBLAGE , oprid))
                    {
                        throw new Exception("يجب اولا حذف العناصر المساهمة في عملية التجميع");
                    }
                    //   DB.ExecuteSQLCommand("","delete from   "
                    //  + ItemOUTSQL.ItemOUTTable.TableName
                    //  + " where "
                    //  + ItemOUTSQL.ItemOUTTable.OperationType + "=" + Operation.DISASSEMBLAGE
                    //  + " and "
                    //  + ItemOUTSQL.ItemOUTTable.OperationID + "=" + oprid

                    //  );
                   AssemblabgeOPR AssemblabgeOPR_= GetAssemblageOPR_INFO_BYID(oprid);
                    List<ItemIN> itemin_list = new ItemINSQL(DB).GetItemINList(AssemblabgeOPR_._Operation);
                    if (itemin_list.Count>0)
                    {
                        ItemIN itemin = itemin_list[0];

                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("DeleteDisAssemblageOPR", itemin);
                        if (itemoutlist.Count > 0)
                        {
                            throw new Exception("العنصر الناتج عن عملية التجميع تم اخراجه يجب اولا الغاء عملية اخراج العنصر");
                        }
                        DB.ExecuteSQLCommand("delete from   "
                          + ItemINSQL.ItemINTable.TableName
                          + " where "
                          + ItemINSQL.ItemINTable.OperationType + "=" + Operation.DISASSEMBLAGE
                          + " and "
                          + ItemINSQL.ItemINTable.OperationID + "=" + oprid
                          );
                    }
                    
                 

                    DB.ExecuteSQLCommand("delete from   "
                    + AssemblageTable.TableName
                    + " where "
                    + AssemblageTable.AssemblageID  + "=" + oprid
                    );
                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.DELETE 
                                     , DatabaseInterface.Log.Log_Target.Trade_Assemblage
                                      , ""
                                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_Assemblage
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("DeleteDisAssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<AssemblabgeOPR> Get_All_AssemblageOPR()
            {
                List<AssemblabgeOPR> list = new List<AssemblabgeOPR>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + AssemblageTable.OprDate + ","
                    + AssemblageTable.OprDesc + ","
                    + AssemblageTable.Notes + ","
                    + AssemblageTable.AssemblageID
                    + " from   "
                    + AssemblageTable.TableName
                      );
                    for(int i=0;i<t.Rows .Count;i++)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[i][0]);
                        string oprdesc = t.Rows[i][1].ToString();
                        string notes = t.Rows[i][2].ToString();
                        uint oprid = Convert.ToUInt32(t.Rows[i][3].ToString());
                        Operation operation = new Operation(Operation.ASSEMBLAGE, oprid);

                        List<ItemIN> iteminlist = new ItemINSQL(DB).GetItemINList(operation);
                        ItemIN ItemIN_ = null;
                        if (iteminlist.Count > 0)
                            ItemIN_ = iteminlist[0];
                        list.Add ( new AssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemIN_));

                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_All_AssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return list;
            }
        }
        public class DisAssemblageSQL
        {
            DatabaseInterface DB;
            private static class DisAssemblageTable
            {
                public const string TableName = "Trade_DisAssemblage";
                public const string DisAssemblageID = "DisAssemblageID";
                public const string OprDate = "OprDate";
                public const string OprDesc = "OprDesc";

                public const string Notes = "Notes";


            }
            public DisAssemblageSQL(DatabaseInterface db)
            {
                DB = db;

            }

            public DisAssemblabgeOPR GetDisAssemblageOPR_INFO_BYID(uint oprid)
            {
                try
                {
 
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + DisAssemblageTable.OprDate + ","
                     + DisAssemblageTable.OprDesc  + ","
                    + DisAssemblageTable.Notes  
                    + " from   "
                    + DisAssemblageTable.TableName
                    + " where "
                    + DisAssemblageTable.DisAssemblageID  + "=" + oprid
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[0][0]);
                        string oprdesc = t.Rows[0][1].ToString();
                        string notes = t.Rows[0][2].ToString();
                        Operation operation = new Operation(Operation.DISASSEMBLAGE, oprid);
                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(operation);
                        ItemOUT ItemOUT_=null ;
                        if (itemoutlist.Count > 0)
                            ItemOUT_ = itemoutlist[0];
                        return new DisAssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemOUT_);

          

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetAssemblageOPR_INFO_BYID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public DisAssemblabgeOPR GetDisAssemblageOPR_INFO_BY_RowID(uint rowid)
            {
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + DisAssemblageTable.OprDate + ","
                    + DisAssemblageTable.OprDesc  + ","
                    + DisAssemblageTable.Notes  + ","
                      + DisAssemblageTable.DisAssemblageID
                    + " from   "
                    + DisAssemblageTable.TableName
                    + " where "
                    + DatabaseInterface .ROWID_COLUMN + "=" + rowid 
                      );
                    if (t.Rows.Count == 1)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[0][0]);
                        string oprdesc = t.Rows[0][1].ToString();
                        string notes = t.Rows[0][2].ToString();
                        uint oprid = Convert.ToUInt32(t.Rows[0][3].ToString());
                        Operation operation = new Operation(Operation.DISASSEMBLAGE, oprid);

                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(operation);
                        ItemOUT ItemOUT_ = null;
                        if (itemoutlist.Count > 0)
                            ItemOUT_ = itemoutlist[0];
                        return new DisAssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemOUT_);

                    }
                    else
                        return null;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetDisAssemblageOPR_INFO_BY_RowID:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }

            public DisAssemblabgeOPR CreateDisAssemblageOPR
                (DateTime oprdate,string oprdesc, string notes
)
            {
                try
                {
               
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DataTable t = DB.GetData(" insert into "
                    + DisAssemblageTable.TableName
                    + "("
                    + DisAssemblageTable.OprDate + ","
                     + DisAssemblageTable.OprDesc  + ","
                    + DisAssemblageTable.Notes 
                    + ")"
                    + "values"
                    + "("
                    + "'" + oprdate.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                     + ","
                    + "'" + oprdesc + "'"
                    + ","
                    + "'" + notes  + "'"

                    + ")"
                     + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    DisAssemblabgeOPR DisAssemblabgeOPR_ = GetDisAssemblageOPR_INFO_BY_RowID(rowid);
      
                    
                    DB.AddLog(
                                       DatabaseInterface.Log.LogType.INSERT
                                       , DatabaseInterface.Log.Log_Target.Trade_DisAssemblage 
                                        , ""
                                        , true, "");
                    return DisAssemblabgeOPR_;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.INSERT
                            , DatabaseInterface.Log.Log_Target.Trade_DisAssemblage 
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("CreateDisAssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
            }
            public bool UpdateDisAssemblageOPR
                 (uint oprid, DateTime oprdate
                , string oprdesc, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

      
                    DB.ExecuteSQLCommand("update  "
                    + DisAssemblageTable.TableName
                    + " set "
                    + DisAssemblageTable.OprDate   + "='" + oprdate .ToString("yyyy-MM-dd HH:mm:ss") + "'"
                    + ","
                    + DisAssemblageTable.OprDesc  + "='" + oprdesc  + "'"
                    + ","
                    + DisAssemblageTable.Notes   + "='" + notes   + "'"

                    + " where "
                    + DisAssemblageTable.DisAssemblageID   + "=" + oprid  
                    );

                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.UPDATE 
                                     , DatabaseInterface.Log.Log_Target.Trade_DisAssemblage
                                      , ""
                                      , true, "");
                    return true ;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.UPDATE 
                            , DatabaseInterface.Log.Log_Target.Trade_DisAssemblage
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("UpdateDisAssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false ;
                }
            }
            public bool DeleteDisAssemblageOPR(uint oprid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Industry_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (new ItemINSQL(DB).Does_Operation_Has_ItemsIN(Operation.DISASSEMBLAGE , oprid))
                    {
                        throw new Exception("يجب اولا حذف العناصر الناتجة عن   عملية التفكيك");
                    }
                    //   DB.ExecuteSQLCommand("","delete from   "
                    //  + ItemOUTSQL.ItemOUTTable.TableName
                    //  + " where "
                    //  + ItemOUTSQL.ItemOUTTable.OperationType + "=" + Operation.DISASSEMBLAGE
                    //  + " and "
                    //  + ItemOUTSQL.ItemOUTTable.OperationID + "=" + oprid

                    //  );
                    DisAssemblabgeOPR DisAssemblabgeOPR_ = GetDisAssemblageOPR_INFO_BYID(oprid);
                    List<ItemIN > ItemINlist = new ItemINSQL(DB).GetItemINList(DisAssemblabgeOPR_._Operation );
                    if (ItemINlist.Count > 0)
                    {
                        throw new Exception("يجب اولا حذف العناصر الداخلة عن طريق عملية التفكيك ");
                    }
                 //   DB.ExecuteSQLCommand("","delete from   "
                 //+ ItemINSQL.ItemINTable.TableName
                 //+ " where "
                 //+ ItemINSQL.ItemINTable.OperationType + "=" + Operation.DISASSEMBLAGE
                 //+ " and "
                 //+ ItemINSQL.ItemINTable.OperationID + "=" + oprid
                 //);
                    DB.ExecuteSQLCommand( "delete from   "
                   + ItemOUTSQL .ItemOUTTable .TableName
                   + " where "
                   + ItemOUTSQL .ItemOUTTable .OperationType + "=" + Operation .DISASSEMBLAGE 
                   + " and "
                   + ItemOUTSQL.ItemOUTTable.OperationID + "=" +oprid
                    
                   );
                 //   DB.ExecuteSQLCommand("","delete from   "
                 //+ ItemINSQL.ItemINTable.TableName
                 //+ " where "
                 //+ ItemINSQL.ItemINTable.OperationType + "=" + Operation.DISASSEMBLAGE
                 //+ " and "
                 //+ ItemINSQL.ItemINTable.OperationID + "=" + oprid
                 //);
                    DB.ExecuteSQLCommand("delete from   "
                    + DisAssemblageTable.TableName
                    + " where "
                    + DisAssemblageTable.DisAssemblageID   + "=" + oprid  
                    );

                    DB.AddLog(
                                     DatabaseInterface.Log.LogType.DELETE 
                                     , DatabaseInterface.Log.Log_Target.Trade_DisAssemblage
                                      , ""
                                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                            DatabaseInterface.Log.LogType.DELETE 
                            , DatabaseInterface.Log.Log_Target.Trade_DisAssemblage
                            , ""
                          , false, ee.Message);
                    System.Windows.Forms.MessageBox.Show("DeleteDisAssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<DisAssemblabgeOPR> Get_All_DisAssemblageOPR()
            {
                List<DisAssemblabgeOPR> list = new List<DisAssemblabgeOPR>();
                try
                {

                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                    + DisAssemblageTable.OprDate + ","
                    + DisAssemblageTable.OprDesc + ","
                    + DisAssemblageTable.Notes + ","
                      + DisAssemblageTable.DisAssemblageID
                    + " from   "
                    + DisAssemblageTable.TableName

                      );
                    for(int i=0;i<t.Rows .Count;i++)
                    {
                        DateTime oprdate = Convert.ToDateTime(t.Rows[i][0]);
                        string oprdesc = t.Rows[i][1].ToString();
                        string notes = t.Rows[i][2].ToString();
                        uint oprid = Convert.ToUInt32(t.Rows[i][3].ToString());
                        Operation operation = new Operation(Operation.DISASSEMBLAGE, oprid);

                        List<ItemOUT> itemoutlist = new ItemOUTSQL(DB).GetItemOUTList(operation);
                        ItemOUT ItemOUT_ = null;
                        if (itemoutlist.Count > 0)
                            ItemOUT_ = itemoutlist[0];
                        list.Add ( new DisAssemblabgeOPR(oprid, oprdate, oprdesc
                            , notes, ItemOUT_));

                    }
           
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_All_DisAssemblageOPR:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return list;
            }
        }    
    }
}
