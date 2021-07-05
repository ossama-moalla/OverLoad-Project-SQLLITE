using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using System.Data.SQLite;
using System.Drawing;
using System.IO;


namespace OverLoad_Client.ItemObj
{
    namespace ItemObjSQL
    {
        public class FolderSQL
        {
            DatabaseInterface DB;
            public static class FolderTable
            {
                public const string TableName = "Item_Folder";
                public const string FolderID = "FolderID";
                public const string FolderName = "FolderName";
                public const string ParentFolderID = "ParentFolderID";
                public const string CreateDate = "CreateDate";
                public const string DefaultConsumeUnit = "DefaultConsumeUnit";

            }
            public FolderSQL(DatabaseInterface db)
            {
                DB = db;
                
            }
            public string GetFolderPath(Folder Folder_)
            {

                if (Folder_ == null) return "Root\\";

                List<string> f_path = new List<string>();
                Folder f = Folder_;
                f_path.Add(f.FolderName);
                while (f.ParentFolderID != null)
                {
                    f = GetFolderInfoByID(Convert.ToUInt32(f.ParentFolderID));
                    f_path.Add(f.FolderName);
                }
                 f_path.Add("Root");
                string s = "";
                for (int i = f_path.Count - 1; i >= 0; i--)
                    s += f_path[i] + " /";
                return s;
            }
            public string GetFolderPath(Folder RootFolder, Folder Folder_)
            {
                if (Folder_ == RootFolder)
                {
                    if (RootFolder == null) return "Root\\";
                    else return RootFolder.FolderName;
                }


              

                if (Folder_ == null)  return "Root\\";

                List<string> f_path = new List<string>();
                Folder f = Folder_;
                f_path.Add(f.FolderName);
                while (f.ParentFolderID != null)
                {
                    f = GetFolderInfoByID(Convert.ToUInt32(f.ParentFolderID));
                    f_path.Add(f.FolderName);
                    if (RootFolder != null) if (f.ParentFolderID != RootFolder.FolderID) break;
                }
                if (RootFolder == null) f_path.Add("Root");
                else f_path.Add(RootFolder.FolderName);
                string s = "";
                for (int i = f_path.Count - 1; i >= 0; i--)
                    s += f_path[i] + " /";
                return s;
            }

            internal List <Folder > Get_User_Allowed_Folders(uint userid)
            {
                List<Folder> list = new List<Objects.Folder>();
                try
                {
                    List<Folder> TempFolders = DB.__User.AccessFolderPremessionList.Select(x => x._Folder).ToList();

                    List<Folder> Folders = new List<Folder>();

                    Folders.AddRange(TempFolders);
                    for (int i = 0; i < TempFolders.Count; i++)
                    {

                        Folders.AddRange(Get_Folder_Tree(TempFolders[i]));
   
                    }
                    Folders = Folders.Where(x => x != null).ToList();
                    List<uint> FolderIDList = Folders.Select(x => x.FolderID).Distinct().ToList();
                    for (int i = 0; i < FolderIDList.Count; i++)
                    {
                        list.Add(Folders.Where(x => x.FolderID == FolderIDList[i]).ToList()[0]);
                    }
      

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_User_Allowed_Folders:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return list;
            }

            public Folder  CreateFolder(string name, uint? parentid,string default_consumeunit)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (default_consumeunit.Replace(" ", string.Empty).Length == 0) throw new Exception("وحدة التوزيع الافتراضية يجب ان لا تكون فارغة");
                    DateTime time = DateTime.Now;              // Use current time
                    string format = "yyyy-MM-dd HH:mm:ss";
                    string datetime = "'" + time.ToString(format) + "'";
                    string parentid_string;
                    if (parentid == null)
                        parentid_string = "null";
                    else
                        parentid_string = parentid.ToString();


     
                   DataTable t= DB.GetData ( " insert into "
                    + FolderTable.TableName 
                    + "("
                    + FolderTable.FolderName
                    + ","
                    + FolderTable.ParentFolderID
                    + ","
                    + FolderTable.CreateDate
                     + ","
                    + FolderTable.DefaultConsumeUnit
                    + ")"
                    + "values"
                    + "("
                    + "'" + name + "'"
                    + ","
                    + parentid_string
                     + ","
                    + datetime
                      + ","
                      + "'" + default_consumeunit + "'"
                    + ")"
                    + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT 
                      , DatabaseInterface.Log.Log_Target.Item_Folder 
                      , ""
                    , true, "");
                    return GetFolder_INFO_BY_RowID(rowid); ;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                      , DatabaseInterface.Log.Log_Target.Item_Folder
                      , ""
                    , false , ee.Message );
                    System.Windows.Forms.MessageBox.Show("CreateFolder:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null ;
                }
            }
            public bool UpdateFolder(Folder folder,string newname, string default_consumeunit)
            {
                   try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (default_consumeunit.Replace(" ", string.Empty).Length == 0) throw new Exception("وحدة التوزيع الافتراضية يجب ان لا تكون فارغة");

                    DB.ExecuteSQLCommand( "update  "
                    + FolderTable.TableName
                    + " set "
                    + FolderTable.FolderName+"='"+newname +"'"
                    +","
                    + FolderTable.DefaultConsumeUnit  + "='" + default_consumeunit  + "'"
                    + " where "
                    +FolderTable.FolderID  +"="+folder .FolderID 
                    );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                      , DatabaseInterface.Log.Log_Target.Item_Folder
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                      , DatabaseInterface.Log.Log_Target.Item_Folder
                      , ""
                    , false , ee.Message  );
                    MessageBox.Show("UpdateFolder:", "خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteFolder(Folder folder__)
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                List<Item> Items = new ItemSQL(DB).GetItemsInFolder(folder__);
                List<Folder> Folders = new FolderSQL(DB).GetFolderChilds(folder__);
                if (Folders.Count >0 || Items .Count >0)
                {
                    MessageBox.Show("المجلد"+folder__ .FolderName +" غير فارغ لا يمكن حذفه!","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
                try
                {
                    try
                    {
                        ItemSpec_Restrict_SQL ItemSpec_Restrict_SQL_= new ItemSpec_Restrict_SQL(DB);
                        List <ItemSpec_Restrict> ItemSpec_Restrict_List = ItemSpec_Restrict_SQL_.GetItemSpecRestrictList(folder__);
                        for(int i=0; i< ItemSpec_Restrict_List.Count;i++)
                        {
                            ItemSpec_Restrict_SQL_.DeleteItemSpecRestrict(ItemSpec_Restrict_List[i].SpecID);
                        }
                        DB.ExecuteSQLCommand("delete from   "
                          + ItemSpecSQL.ItemSpecTable.TableName
                          + " where "
                          + ItemSpecSQL.ItemSpecTable.FolderID + "=" + folder__.FolderID
                          );

                    }
                    catch(SqlException  ee)
                    {
                        MessageBox.Show(ee.Message);
                        MessageBox.Show("فشل حذف الخصائص المرتبطة بهذا المجلد , فشل حذف المجلد!", "حدث خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    DB.ExecuteSQLCommand( "delete from   "
                    + FolderTable.TableName
                    + " where "
                    + FolderTable.FolderID + "=" + folder__.FolderID
                    );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_Folder
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_Folder
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("DeleteFolder:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public Folder GetParentFolder(Folder f)
            {
                if (f == null) return null;
                if (f.ParentFolderID == null) return null;
                DataTable t = new DataTable();
                try
                {
                    t = DB.GetData("select * from " + FolderTable.TableName
                   + " where " + FolderTable.FolderID + "=" + f.ParentFolderID);
                }
                catch(Exception ee)
                {
                    MessageBox.Show("فشل الاتصال بقاعدة البيانات","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error );
                    return null;
                }
                
                
                uint fid =Convert .ToUInt32  ( f.ParentFolderID);
                string fname = t.Rows[0][1].ToString ();
                uint? p;
                try
                {
                    p = Convert.ToUInt32(t.Rows[0][2]);
                }catch
                {
                    p = null;
                }
                DateTime d = Convert.ToDateTime(t.Rows[0][3]);
                string default_consumeUnit = t.Rows[0][4].ToString();
                return new Folder(fid, fname, p, d,default_consumeUnit);
              
            }
            public List<Folder > GetFolderChilds(Folder folder)
            {
                List<Folder> list = new List<Folder>();
                try
                {
                    
                    string parentid;
                    if (folder == null) parentid = " is null";
                    else parentid = "=" + folder.FolderID.ToString();

                    DataTable t = DB.GetData("select * from " + FolderTable.TableName
                       + " where " + FolderTable.ParentFolderID + parentid
                       + " order by " + FolderTable.FolderName
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint fid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string fname = t.Rows[i][1].ToString();
                        uint? p;
                        if (folder == null) p = null;
                        else p = folder.FolderID;
                        DateTime d = Convert.ToDateTime(t.Rows[i][3]);
                        string default_consumeUnit = t.Rows[i][4].ToString();
                        list.Add(new Folder(fid, fname, p, d, default_consumeUnit));
                    }
                }
                catch(Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetFolderChilds:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                //MessageBox.Show("g:"+list .Count .ToString ());
                return list ;
            }
            public List<Folder> Get_Folder_Tree(Folder folder)
            {
                List<Folder> list = new List<Folder>();
                if(folder !=null ) list.Add(folder);
                try
                {
                    List<Folder> templist = GetFolderChilds(folder);
                    list.AddRange(templist);
                    for(int i=0;i<templist .Count;i++)
                    {
                       
                        list.AddRange(GetFolderChilds(templist[i]));
                    }
                }catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_Folder_Tree:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
                return list;
            }

            public List<Folder> SearchFolder(Folder  RootFolder, string  n_)
            {
                List<Folder> list = new List<Folder>();
              
           
                try
                {
                    List<Folder> AllowedFolders = Get_Folder_Tree(RootFolder);
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + FolderTable.FolderID + ","
                        + FolderTable.FolderName + ","
                        + FolderTable.ParentFolderID + ","
                        + FolderTable.CreateDate + ","
                        + FolderTable.DefaultConsumeUnit 
                        + " from " + FolderTable.TableName
                       + " where " + FolderTable.FolderName + " like  '%" + n_ + "%'");
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint fid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        if (AllowedFolders.Where(x => x.FolderID == fid).ToList().Count == 0) continue;
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
                        string default_consumeUnit = t.Rows[i][4].ToString();
                        list.Add(new Folder(fid, fname, p, d, default_consumeUnit));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("SearchFolder:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list ;
                }

            }
            //public int GetFolderIDByName(string name,int? id)
            //{
            //    string parentid;
            //    if (id == null) parentid = " is null";
            //    else parentid = "=" + id.ToString();

            //    System.Data.DataTable table = DB.GetData("select " + FolderTable.FolderID
            //        + " from " + FolderTable.Folder
            //        + " where " + FolderTable.FolderName + "='" + name + "'"
            //        );
            //    return Convert.ToInt32(table.Rows[0][0]);
            //}
            public Folder  GetFolderInfoByID(uint id)
            {
                try
                {
                    if (id == 0) return null;
                    DataTable t = DB.GetData("select * from " + FolderTable.TableName
                   + " where " + FolderTable.FolderID + "=" + id);
                    if (t.Rows.Count == 1)
                    {
                        uint fid = Convert.ToUInt32(t.Rows[0][0].ToString());
                        string fname = t.Rows[0][1].ToString();

                        uint? p;
                        try
                        {
                            p = Convert.ToUInt32(t.Rows[0][2]);
                        }
                        catch
                        {
                            p = null;
                        }
                        DateTime d = Convert.ToDateTime(t.Rows[0][3]);
                        string default_consumeUnit = t.Rows[0][4].ToString();
                        return new Folder(fid, fname, p, d, default_consumeUnit);
                    }
                    else throw new Exception("لا يوجد صنف بهذا الرقم");
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetFolderInfoByID:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }
                
                
            }
            public Folder  GetFolder_INFO_BY_RowID(uint rowid)
            {
                try
                {
                    DataTable t = DB.GetData("select * from " + FolderTable.TableName
                  + " where "
                    + DatabaseInterface.ROWID_COLUMN + "=" + rowid);

                    uint fid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    string fname = t.Rows[0][1].ToString();

                    uint? p;
                    try
                    {
                        p = Convert.ToUInt32(t.Rows[0][2]);
                    }
                    catch
                    {
                        p = null;
                    }
                    DateTime d = Convert.ToDateTime(t.Rows[0][3]);
                    string default_consumeUnit = t.Rows[0][4].ToString();
                    return new Folder(fid, fname, p, d, default_consumeUnit);
                   
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetFolder_INFO_BY_RowID" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return null;
                }

            }
            public List <Folder >  GetFoldersList()
            {
                List<Folder> list = new List<Folder>();
                DataTable t = DB.GetData("select * from " + FolderTable.TableName);
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
                    string default_consumeUnit = t.Rows[0][4].ToString();
                    list.Add(new Folder(fid, fname, p, d,default_consumeUnit ));
                }
                return list;
            }
            public bool IS_Move_Able(Folder DestinationFolder,Folder folder)
            {
                
                if (DestinationFolder == folder) return false;
                if (DestinationFolder == null) return true;
                Folder Parent_temp,Child_Temp;
                Child_Temp = DestinationFolder;

                while (true )
                {

                    Parent_temp = GetParentFolder(Child_Temp);
                    if (Parent_temp == folder ) return false ;
                    if (Parent_temp == null) return true ;

                    Child_Temp = Parent_temp;
                }

            }
            public bool MoveFolders(Folder DestinationFolder,List <Folder> FoldersList)
            {
                if (FoldersList.Count == 0) return false;
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                for (int i = 0; i < FoldersList.Count; i++)
                {
                    if(!IS_Move_Able(DestinationFolder,FoldersList[i] ))
                    {
                        MessageBox.Show("لا يمكن نقل مجلد الى مجلد ابن له","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error );
                        return false;
                    }
                }
                    try
                {
                    for(int i=0;i<FoldersList.Count;i++)
                    {
                        string desteniationFolder_id_str;
                        if (DestinationFolder == null)
                            desteniationFolder_id_str = "null";
                        else
                            desteniationFolder_id_str = DestinationFolder.FolderID.ToString();
                        DB.ExecuteSQLCommand("update "
                            +FolderSQL .FolderTable .TableName 
                            +" set "
                            + FolderSQL.FolderTable.ParentFolderID +"="+ desteniationFolder_id_str
                            + " where "
                             + FolderSQL.FolderTable.FolderID  + "=" + FoldersList[i].FolderID
                            );
                    }
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_Folder
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_Folder
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("MoveFolders:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
         }

        public class ItemSQL
        {
            DatabaseInterface DB;
            public static class ItemTable
            {

                public const string TableName = "Item_Item";
                public const string ItemID = "ItemID";
                public const string ItemName = "ItemName";
                public const string ItemCompany = "ItemCompany";
                public const string FolderID = "FolderID";

                public const string MarketCode = "MarketCode";
                public const string CreateDate = "CreateDate";
                public const string DefaultConsumeUnit = "DefaultConsumeUnit";


            }
            public static class ItemImageTable
            {
                public const string TableName = "Item_ItemImage";
                public const string ItemID = "ItemID";
                public const string Item_Image = "Item_Image";
            }
            //public static class ItemFileTable
            //{
            //    public const string TableName = "Item_ItemFiles";
            //    public const string ItemID = "ItemID";
            //    public const string ItemImage = "ItemImage";
            //}
            public ItemSQL(DatabaseInterface db)
            {
                DB = db;
            }

            //public string GetItemPath(Item Item_)
            //{
            //    FolderSQL FolderSQL_ = new FolderSQL(DB);
            //    List<string> f_path = new List<string>();
            //    Folder f = Item_.folder;
            //    string s = "ROOT /";

            //    while (f.ParentFolderID != null)
            //    {
            //        f_path.Add(f.FolderName);
            //        f = FolderSQL_.GetFolderInfoByID(Convert.ToUInt32(f.ParentFolderID));

            //    }
            //    f_path.Add(f.FolderName);
            //    for (int i = f_path.Count - 1; i >= 0; i--)
            //        s += f_path[i] + "/";
            //    return s;
            //}
            //public string GetItemPath(AvailableItem AvailableItem_)
            //{
            //    FolderSQL FolderSQL_ = new FolderSQL(DB);
            //    List<string> f_path = new List<string>();
            //    Folder f = AvailableItem_._Item.folder;
            //    string s = "ROOT /";

            //    while (f.ParentFolderID != null)
            //    {
            //        f_path.Add(f.FolderName);
            //        f = FolderSQL_.GetFolderInfoByID(Convert.ToUInt32(f.ParentFolderID));

            //    }
            //    f_path.Add(f.FolderName);
            //    for (int i = f_path.Count - 1; i >= 0; i--)
            //        s += f_path[i] + "/";
            //    return s;
            //}
            public Item GetItemInfo_By_RowID(uint rowid)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ItemTable.ItemID + ","
                        + ItemTable.ItemName + ","
                        + ItemTable.ItemCompany + ","
                        + ItemTable.FolderID + ","
                        + ItemTable.MarketCode + ","
                        + ItemTable.CreateDate + ","
                         + ItemTable.DefaultConsumeUnit
                        + " from " 
                        + ItemTable.TableName
                       + " where "
                    + DatabaseInterface.ROWID_COLUMN + "=" + rowid);
                    if (t.Rows.Count == 1)
                    {
                        uint itemid = Convert.ToUInt32(t.Rows[0][ItemTable.ItemID].ToString());
                        string itemname = t.Rows[0][ItemTable.ItemName ].ToString();
                        string itemcompany = t.Rows[0][ItemTable.ItemCompany].ToString();
                        uint folderid = Convert.ToUInt32(t.Rows[0][ItemTable.FolderID ].ToString());
                        FolderSQL f = new FolderSQL(this.DB);

                        string marketcode = t.Rows[0][ItemTable.MarketCode ].ToString();
                        DateTime d = Convert.ToDateTime(t.Rows[0][ItemTable.CreateDate ]);
                        string DefaultConsumeUnit_ = t.Rows[0][ItemTable.DefaultConsumeUnit ].ToString();
                        return (new Item(f.GetFolderInfoByID(folderid), itemid, itemname, itemcompany, marketcode, d, DefaultConsumeUnit_));

                    }

                    else return null;

                }
                catch (Exception ee)
                {
                    throw new Exception ("GetItemInfo_By_RowID:" + ee.Message);
                }


            }



            public Item  CreateItem(Folder folder, string itemname, string itemcompany, string marketcode, string DefaultConsumeUnit_)
            {
       
               
                   
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (itemname.Replace(" ", string.Empty).Length == 0 || DefaultConsumeUnit_.Replace(" ", string.Empty).Length == 0)
                        throw new Exception("اسم العنصر ووحدة التوزيع يجب ان لا تكون فارغة");
                    DateTime time = DateTime.Now;              // Use current time
                    string format = "yyyy-MM-dd HH:mm:ss";
                    string datetime = "'" + time.ToString(format) + "'";
                    if (folder == null)
                    {
                        throw new Exception ("لا يمكن اضافة عناصر للمجلد الجذر");
                    }
                    DataTable t= DB.GetData (" insert into "
                    + ItemTable .TableName
                    + "("
                     + ItemTable.FolderID
                      + ","
                    + ItemTable.ItemName 
                    + ","
                    + ItemTable.ItemCompany 
                    + ","
                    + ItemTable.MarketCode
                     + ","
                    + ItemTable.CreateDate
                       + ","
                    + ItemTable.DefaultConsumeUnit 
                    + ")"
                    + "values"
                    + "("
                    +folder .FolderID
                     + ","
                    + "'" + itemname  + "'"
                    + ","
                     +"'" + itemcompany  + "'"
                     + ","
                    + "'" + marketcode  + "'"
                     + ","
                     +datetime
                      + ","
                     + "'" + DefaultConsumeUnit_ + "'"
                    + ")"
                     + ";select last_insert_rowid() "
                    );
                    uint rowid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.INSERT 
                       , DatabaseInterface.Log.Log_Target.Item_Item 
                       , ""
                     , true, "");
                    return GetItemInfo_By_RowID(rowid);
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT 
                      , DatabaseInterface.Log.Log_Target.Item_Item 
                      , ""
                    , false, ee.Message);
                   throw new Exception ("CreateItem"+ee.Message );
                }

            }
            public bool UpdateItem(Item Item_ , string new_itemname, string new_item_company, string new_marketcode,  string new_DefaultConsumeUnit_)
            {


                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    if (new_itemname.Replace(" ", string.Empty).Length == 0 || new_DefaultConsumeUnit_.Replace(" ", string.Empty).Length == 0)
                        throw new Exception("اسم العنصر ووحدة التوزيع يجب ان لا تكون فارغة");
                    DB.ExecuteSQLCommand(" update  "
                    + ItemTable.TableName
                    + " set "
                     + ItemTable.ItemName + "='" + new_itemname+"'"
                      + ","
                    + ItemTable.ItemCompany + "='" + new_item_company + "'"
                    + ","
                    + ItemTable.MarketCode + "='" + new_marketcode + "'"
                     + ","
                    + ItemTable.DefaultConsumeUnit + "='" + new_DefaultConsumeUnit_ + "'"
                    + " where "
                    + ItemTable.ItemID +"="+Item_.ItemID 
                    );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_Item
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_Item
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("UpdateItem:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool DeleteItem(Item item_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" delete from   "
               + ItemSellPriceSql.ItemSellPriceTable.TableName 
               + " where  "
               + ItemSellPriceSql.ItemSellPriceTable.ItemID + "=" + item_.ItemID);


                    DB.ExecuteSQLCommand(" delete from   "
               + ConsumeUnitSql.ConsumeUnitTable.TableName
               + " where  "
               + ConsumeUnitSql.ConsumeUnitTable.ItemID + "=" + item_.ItemID);

                    DB.ExecuteSQLCommand(" delete from   "
                   + ItemRelationShipsSQL .ItemRelationShipsTable.TableName
                   + " where  "
                   + ItemRelationShipsSQL.ItemRelationShipsTable.ItemID + "=" + item_.ItemID
                   +" or "
                   + ItemRelationShipsSQL.ItemRelationShipsTable.AnotherItemID + "=" + item_.ItemID
                   );

                    DB.ExecuteSQLCommand(" delete from   "
                    + ItemSpec_Value_SQL.ItemSpec_Value_Table.TableName
                    + " where  "
                    + ItemSpec_Value_SQL.ItemSpec_Value_Table.ItemID + "=" + item_.ItemID);

                    DB.ExecuteSQLCommand(" delete from   "
                    + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble .TableName 
                    + " where  "
                    + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble.ItemID  + "=" + item_.ItemID);

                    DB.ExecuteSQLCommand(" delete from   "
              + ItemFileSQL .ItemFileTable.TableName
              + " where  "
              + ItemFileSQL.ItemFileTable.ItemID + "=" + item_.ItemID);

                    DB.ExecuteSQLCommand(" delete from   "
              + ItemImageTable.TableName
              + " where  "
              + ItemImageTable.ItemID + "=" + item_.ItemID);

                    DB.ExecuteSQLCommand(" delete from   "
                     + ItemTable.TableName
                     + " where  "
                     + ItemTable.ItemID + "=" + item_.ItemID);
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE 
                       , DatabaseInterface.Log.Log_Target.Item_Item
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_Item
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("DeleteItem:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<Item> FilterItemsBySpec(List<ItemSpec_Restrict_Options> ItemSpec_Restrict_Options_List,List <ItemSpec_Value> ItemSpec_Value_List)
            {
                string Cmd_Statemanet= "  ";
                for (int i = 0; i < ItemSpec_Restrict_Options_List.Count; i++)
                {
                    Cmd_Statemanet += "select "
                        + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble.ItemID
                        + " from "
                        + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble.TableName
                        + " where "
                        + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble.SpecID + "=" + ItemSpec_Restrict_Options_List[i].ItemSpecRestrict_.SpecID.ToString()
                        + " and "
                        + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble.OptionID + "=" + ItemSpec_Restrict_Options_List[i].OptionID.ToString();
                   
                    if (i != ItemSpec_Restrict_Options_List.Count - 1)
                        Cmd_Statemanet += "  INTERSECT ";
                }
                if (ItemSpec_Value_List.Count > 0 && ItemSpec_Restrict_Options_List.Count > 0) Cmd_Statemanet += " INTERSECT ";
                for (int i = 0; i < ItemSpec_Value_List.Count; i++)
                {
                    Cmd_Statemanet += "select "
                                      + ItemSpec_Value_SQL.ItemSpec_Value_Table.ItemID 
                                      +" from "
                                      + ItemSpec_Value_SQL.ItemSpec_Value_Table.TableName
                                      +" where "
                                      + ItemSpec_Value_SQL.ItemSpec_Value_Table.SpecID + "=" + ItemSpec_Value_List[i].ItemSpec_.SpecID.ToString()
                                      + " and "
                                      + ItemSpec_Value_SQL.ItemSpec_Value_Table.Value + " like '%" + ItemSpec_Value_List[i].Value+"%'"
                                            ;
                    if (i != ItemSpec_Value_List.Count - 1)
                        Cmd_Statemanet += "  INTERSECT ";
                }
                List<Item> Item_list = new List<Item>();
                DataTable t = new DataTable();
                t = DB.GetData(Cmd_Statemanet
                   );
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    Item_list.Add(new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[i][0])));
                }
                return Item_list;
            }
            public List<Item> GetItemsInFolder(Folder folder)
            {
                List<Item> list = new List<Item>();
                if (folder == null) return list ;

                DataTable t = new DataTable();
                t = DB.GetData("select "
                    +ItemTable .ItemID+","
                    + ItemTable.ItemName  + ","
                    + ItemTable.ItemCompany  + ","
                    + ItemTable.MarketCode + ","
                    + ItemTable.CreateDate + ","
                    +ItemTable .DefaultConsumeUnit 
                    + " from " + ItemTable .TableName
                   + " where " + ItemTable.FolderID  +"="+ folder.FolderID
                    + " order by " + ItemTable.ItemName 
                   );
                for (int i = 0; i < t.Rows.Count; i++)
                {

                    uint itemid = Convert.ToUInt32(t.Rows[i][0].ToString());
                    string itemname = t.Rows[i][1].ToString();
                    string itemcompany = t.Rows[i][2].ToString();
                    string marketcode = t.Rows[i][3].ToString();
                    DateTime d = Convert.ToDateTime(t.Rows[i][4]);
                    string DefaultConsumeUnit_= t.Rows[i][5].ToString();

                    list.Add(new Item(folder,itemid, itemname , itemcompany,marketcode  , d, DefaultConsumeUnit_));
                }
                return list;
            }
            public List<Item> SearchItem(Folder RootFolder, string n)
            {
                List<Item> list = new List<Item>();
                try
                {
                 
                    if (n.Length == 0) return list;
                    List<Folder> AllowedFolders =new FolderSQL (DB). Get_Folder_Tree (RootFolder);
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ItemTable.ItemID + ","
                        + ItemTable.ItemName + ","
                        + ItemTable.ItemCompany + ","
                        + ItemTable.MarketCode + ","
                        + ItemTable.CreateDate + ","
                         + ItemTable.DefaultConsumeUnit + ","
                        + ItemTable.FolderID
                        + " from " + ItemTable.TableName
                       + " where " + ItemTable.ItemName + " like '%" + n + "%'"
                        + " or " + ItemTable.MarketCode  + " like '%" + n + "%'"
                         + " order by " + ItemTable.ItemName
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint itemid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string itemname = t.Rows[i][1].ToString();
                        string itemcompany = t.Rows[i][2].ToString();
                        string marketcode = t.Rows[i][3].ToString();
                        DateTime d = Convert.ToDateTime(t.Rows[i][4]);
                        string DefaultConsumeUnit_ = t.Rows[i][5].ToString();
                        uint folderid = Convert.ToUInt32(t.Rows[i][6]);
                        if (AllowedFolders.Where(x => x.FolderID == folderid).ToList().Count == 0) continue;

                        list.Add(new Item(new FolderSQL(DB).GetFolderInfoByID(folderid), itemid, itemname, itemcompany, marketcode, d, DefaultConsumeUnit_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("SearchItem:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list;
                }
                
            }
            public List<Item> SearchItemInFolder(Folder Folder_, string n)
            {
                List<Item> list = new List<Item>();
                if (Folder_ == null) return list;
                try
                {
                    if (n.Length == 0) return list;
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ItemTable.ItemID + ","
                        + ItemTable.ItemName + ","
                        + ItemTable.ItemCompany + ","
                        + ItemTable.MarketCode + ","
                        + ItemTable.CreateDate + ","
                         + ItemTable.DefaultConsumeUnit 
                       + " from " + ItemTable.TableName
                       + " where " + ItemTable.ItemName + " like '%" + n + "%'"
                       +" and " + ItemTable.FolderID+"="+ Folder_.FolderID 

                       + " order by " + ItemTable.ItemName
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint itemid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string itemname = t.Rows[i][1].ToString();
                        string itemcompany = t.Rows[i][2].ToString();
                        string marketcode = t.Rows[i][3].ToString();
                        DateTime d = Convert.ToDateTime(t.Rows[i][4]);
                        string DefaultConsumeUnit_ = t.Rows[i][5].ToString();

                        list.Add(new Item(Folder_, itemid, itemname, itemcompany, marketcode, d, DefaultConsumeUnit_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("SearchItemINFolder:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list;
                }

            }
          public Item GetItemInfoByID(uint id)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ItemTable.ItemID + ","
                        + ItemTable.ItemName + ","
                        + ItemTable.ItemCompany + ","
                        + ItemTable.FolderID + ","
                        + ItemTable.MarketCode + ","
                        + ItemTable.CreateDate + ","
                         + ItemTable.DefaultConsumeUnit  
                        + " from " + ItemTable.TableName
                       + " where " + ItemTable.ItemID + "=" + id);
                    if (t.Rows.Count == 1)
                    {
                        uint itemid = Convert.ToUInt32(t.Rows[0][0].ToString());
                        string itemname = t.Rows[0][1].ToString();
                        string itemcompany = t.Rows[0][2].ToString();
                        uint folderid = Convert.ToUInt32(t.Rows[0][3].ToString());
                        FolderSQL f = new FolderSQL(this.DB);

                        string marketcode = t.Rows[0][4].ToString();
                        DateTime d = Convert.ToDateTime(t.Rows[0][5]);
                        string DefaultConsumeUnit_ = t.Rows[0][6].ToString();
                        return (new Item(f.GetFolderInfoByID(folderid), itemid, itemname, itemcompany, marketcode, d, DefaultConsumeUnit_));

                    }

                    else return null;

                }
                catch (Exception ee)
                {
                    MessageBox.Show("جلب بيانات العنصر:"+ee.Message);
                    return null;
                }
               

            }
            public Item GetItemInfoByName(string name, string company)
            {
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ItemTable.ItemID + ","
                        + ItemTable.ItemName + ","
                        + ItemTable.ItemCompany + ","
                        + ItemTable.FolderID + ","
                        + ItemTable.MarketCode + ","
                        + ItemTable.CreateDate + ","
                        + ItemTable.DefaultConsumeUnit  
                        + " from " + ItemTable.TableName
                       + " where " + ItemTable.ItemName  + "='" + name +"'"
                       + " and "  +ItemTable .ItemCompany +"='"+company +"'"
                       );

                    uint itemid = Convert.ToUInt32(t.Rows[0][0].ToString());
                    string itemname = t.Rows[0][1].ToString();
                    string itemcompany = t.Rows[0][2].ToString();
                    uint folderid = Convert.ToUInt32(t.Rows[0][3].ToString());
                    FolderSQL f = new FolderSQL(this.DB);

                    string marketcode = t.Rows[0][4].ToString();
                    DateTime d = Convert.ToDateTime(t.Rows[0][5]);
                    string DefaultConsumeUnit_ = t.Rows[0][6].ToString();
                  

                    return (new Item(f.GetFolderInfoByID(folderid), itemid, itemname, itemcompany, marketcode, d, DefaultConsumeUnit_));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("جلب بيانات العنصر:" + ee.Message);
                    return null;
                }


            }
            public bool MoveItems(Folder DestinationFolder, List<Item > ItemsList)
            {
                if (ItemsList.Count == 0) return false;
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                try
                {
                    for (int i = 0; i < ItemsList.Count; i++)
                    {
                        DB.ExecuteSQLCommand("update "
                            + ItemSQL .ItemTable.TableName
                            + " set "
                            + ItemSQL.ItemTable.FolderID  + "=" + DestinationFolder.FolderID
                            + " where "
                             + ItemSQL.ItemTable.ItemID + "=" + ItemsList[i].ItemID 
                            );
                    }
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.UPDATE
                       , DatabaseInterface.Log.Log_Target.Item_Item
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_Item
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            public byte [] GetItemImage(Item Item_)
            {

                try
                {

                    byte [] array=  DB.GetData_ByteArray ("select "
                        +ItemImageTable.Item_Image
                        + " from "
                        +ItemImageTable.TableName
                        +" where "
                        +ItemImageTable.ItemID+" = "+ Item_.ItemID );
                    if (array != null) return array;
                    else return null;

                }
                catch(Exception ee)
                {
                    throw new Exception("GetItemImage:" + ee.Message);
                }
            }
   
            public bool SetItemImage(uint ItemID, byte [] Image_)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (!UnSetItemImage(ItemID)) throw new Exception("تعذر اعادة ضبط الصورة");



                    List<byte> FullMessage = new List<byte>();
                    FullMessage.Add(DatabaseInterface.MessageType.ExecuteSQlCMD_INSERT_Serialize);

                    byte[] TableName_Field = Encoding.UTF8.GetBytes(ItemImageTable.TableName);
                    FullMessage.AddRange(BitConverter.GetBytes(TableName_Field.Length));
                    FullMessage.AddRange(TableName_Field);

                    FullMessage.AddRange(BitConverter.GetBytes(2));//rows count

                    byte[] ItemID_Field = Encoding.UTF8.GetBytes(ItemImageTable.ItemID);
                    byte[] ItemID_Value = Encoding.UTF8.GetBytes(ItemID.ToString());
                    FullMessage.Add(0x00);
                    FullMessage.AddRange(BitConverter.GetBytes(ItemID_Field.Length));
                    FullMessage.AddRange(ItemID_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(ItemID_Value.Length));
                    FullMessage.AddRange(ItemID_Value);


                    byte[] FileData_Field = Encoding.UTF8.GetBytes(ItemImageTable.Item_Image);
                    FullMessage.Add(0x01);
                    FullMessage.AddRange(BitConverter.GetBytes(FileData_Field.Length));
                    FullMessage.AddRange(FileData_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(Image_.Length));
                    FullMessage.AddRange(Image_);

                    string path = Application.StartupPath + "\\" + "OverLoadTemp";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string Packe_File_Path = path + "\\" + "tmp." + 0;
                    int j = 1;
                    while (File.Exists(Packe_File_Path))
                    {

                        Packe_File_Path = path + "\\" + "tmp." + j;


                    }




                    File.WriteAllBytes(Packe_File_Path, DB.OverLoadEndPoint_.Encrypt(FullMessage.ToArray()));
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
                    , DatabaseInterface.Log.Log_Target.Item_Image
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
                      , DatabaseInterface.Log.Log_Target.Item_Image
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("SetItemImage:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool UnSetItemImage(uint ItemID)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from "
                        +ItemImageTable.TableName
                        +" where "
                        + ItemImageTable.ItemID +"="+ItemID
                        );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE 
                       , DatabaseInterface.Log.Log_Target.Item_Image 
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_Image 
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public List <string > GetAllItemsNameList()
            {
                List<string > list = new List<string>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select distinct "
                        + ItemTable.ItemName
                        + " from " + ItemTable.TableName);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        string itemname = t.Rows[i][0].ToString();
                        list.Add(itemname);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetAllItemsNameList:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return list ;
                }
            }
            public List<string> GetAllCompaniesNameList()
            {
                List<string> list = new List<string>();
                try
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select distinct "
                        + ItemTable.ItemCompany
                        + " from " + ItemTable.TableName);
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        string companyname = t.Rows[i][0].ToString();
                        list.Add(companyname);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetAllCompaniesNameList:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return list;
                }
            }

            internal List<Item> Get_ALL_Item_List()
            {
                List<Item> list = new List<Item>();
                try
                {
                    List<Folder> foldersList = new FolderSQL(DB).Get_User_Allowed_Folders(DB.__User.UserID);
                  
                    DataTable t = DB.GetData("select "
                        + ItemTable.ItemID + ","
                        + ItemTable.ItemName + ","
                        + ItemTable.ItemCompany + ","
                        + ItemTable.MarketCode + ","
                        + ItemTable.CreateDate + ","
                         + ItemTable.DefaultConsumeUnit + ","
                        + ItemTable.FolderID
                        + " from " + ItemTable.TableName
                         + " order by " + ItemTable.ItemName
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        uint folderid = Convert.ToUInt32(t.Rows[i][6]);
                        if (foldersList.Where(x => x.FolderID == folderid).ToList ().Count  == 0) continue;
                        uint itemid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string itemname = t.Rows[i][1].ToString();
                        string itemcompany = t.Rows[i][2].ToString();
                        string marketcode = t.Rows[i][3].ToString();
                        DateTime d = Convert.ToDateTime(t.Rows[i][4]);
                        string DefaultConsumeUnit_ = t.Rows[i][5].ToString();
                        //uint folderid = Convert.ToUInt32(t.Rows[i][6]);

                        list.Add(new Item(new FolderSQL(DB).GetFolderInfoByID(folderid), itemid, itemname, itemcompany, marketcode, d, DefaultConsumeUnit_));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("Get_ALL_Item_List:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return list;
                }
            }
        }
        public class ItemFileSQL
        {
            DatabaseInterface DB;
            public static class ItemFileTable
            {
                public const string TableName = "Item_ItemFiles";
                public const string ItemID = "ItemID";
                public const string FileID = "FileID";
                public const string Item_FileName = "Item_FileName";
                public const string FileDescription = "FileDescription";
                public const string AddDate = "AddDate";
                public const string FileData = "FileData";
            }
        
            public ItemFileSQL(DatabaseInterface db)
            {
                DB = db;
            }
            public bool AddItemFile(uint ItemID, string File_Name,string File_Description,string File_Path)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                    //byte[] FileData = FileToByteArray(FileLocation);
                    
                    byte[] FileData = null;
                    FileStream fs = new FileStream(File_Path,
                                                   FileMode.Open,
                                                   FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    long numBytes = new FileInfo(File_Path).Length;
                    FileData = br.ReadBytes((int)numBytes);
                    fs.Close();

                    List<byte> FullMessage = new List<byte>();
                    FullMessage.Add(DatabaseInterface.MessageType.ExecuteSQlCMD_INSERT_Serialize);
                   
                    byte[] TableName_Field = Encoding.UTF8.GetBytes(ItemFileTable.TableName );
                    FullMessage.AddRange(BitConverter.GetBytes(TableName_Field.Length));
                    FullMessage.AddRange(TableName_Field);

                    FullMessage.AddRange(BitConverter.GetBytes(5));//rows count

                    byte[] ItemID_Field = Encoding.UTF8.GetBytes(ItemFileTable.ItemID);
                    byte[] ItemID_Value = Encoding.UTF8.GetBytes(ItemID.ToString());
                    FullMessage.Add(0x00);
                    FullMessage.AddRange(BitConverter.GetBytes(ItemID_Field.Length ));
                    FullMessage.AddRange(ItemID_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(ItemID_Value.Length));
                    FullMessage.AddRange(ItemID_Value);

                    byte[] File_Name_Field = Encoding.UTF8.GetBytes(ItemFileTable.Item_FileName);
                    byte[] File_Name_Value = Encoding.UTF8.GetBytes(File_Name);
                    FullMessage.Add(0x00);
                    FullMessage.AddRange(BitConverter.GetBytes(File_Name_Field.Length));
                    FullMessage.AddRange(File_Name_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(File_Name_Value.Length));
                    FullMessage.AddRange(File_Name_Value);

                    byte[] FileDescription_Field = Encoding.UTF8.GetBytes(ItemFileTable.FileDescription);
                    byte[] FileDescription_Value = Encoding.UTF8.GetBytes(File_Description);
                    FullMessage.Add(0x00);
                    FullMessage.AddRange(BitConverter.GetBytes(FileDescription_Field.Length));
                    FullMessage.AddRange(FileDescription_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(FileDescription_Value.Length));
                    FullMessage.AddRange(FileDescription_Value);

                    byte[] AddDate_Field = Encoding.UTF8.GetBytes(ItemFileTable.AddDate);
                    byte[] AddDate_Value = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                    FullMessage.Add(0x00);
                    FullMessage.AddRange(BitConverter.GetBytes(AddDate_Field.Length));
                    FullMessage.AddRange(AddDate_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(AddDate_Value.Length));
                    FullMessage.AddRange(AddDate_Value);

                    byte[] FileData_Field = Encoding.UTF8.GetBytes(ItemFileTable.FileData);
                    FullMessage.Add(0x01);
                    FullMessage.AddRange(BitConverter.GetBytes(FileData_Field.Length));
                    FullMessage.AddRange(FileData_Field);
                    FullMessage.AddRange(BitConverter.GetBytes(FileData.Length));
                    FullMessage.AddRange(FileData);

                    string path = Application.StartupPath + "\\" + "OverLoadTemp";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string  Packe_File_Path = path + "\\" + "tmp." + 0;
                    int j = 1;
                    while (File.Exists(Packe_File_Path))
                    {

                        Packe_File_Path = path + "\\" + "tmp." + j;


                    }




                    File.WriteAllBytes(Packe_File_Path, DB.OverLoadEndPoint_.Encrypt(FullMessage.ToArray()));
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
                    , DatabaseInterface.Log.Log_Target.Item_File
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
                      , DatabaseInterface.Log.Log_Target.Item_File 
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("AddItemFile:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteItemFile(uint file_id)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" delete from  "
                        + ItemFileTable.TableName
                        + " where "
                        + ItemFileTable.FileID + "=" + file_id
                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_File
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_File 
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("DeleteItemFile"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdateFileInfo(uint File_ID,string file_name,string file_description)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand(" update "
                        +ItemFileTable .TableName 
                        +" set "
                        + ItemFileTable.Item_FileName +"='"+file_name +"',"
                        + ItemFileTable.FileDescription + "='" + file_description + "'"
                        +" where "
                        + ItemFileTable.FileID + "=" + File_ID
                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_File 
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE
                      , DatabaseInterface.Log.Log_Target.Item_File 
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("UpdateFileInfo"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<ItemFile> GetItemFileList(Item Item_)
            {
                List<ItemFile> ItemFilesList = new List<ItemFile>();
                try
                {
               
                    DataTable t =DB.GetData("select "
                    + ItemFileTable.FileID +","
                    + ItemFileTable.Item_FileName + ","
                    + ItemFileTable.FileDescription + ","
                    + ItemFileTable.AddDate 
                    + " from "
                    + ItemFileTable.TableName
                    + " where "
                    + ItemFileTable.ItemID  + "=" + Item_.ItemID
                    );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint fileid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string filename = t.Rows[i][1].ToString();
                        string filedescription = t.Rows[i][2].ToString();
                        DateTime datetime = Convert.ToDateTime(t.Rows[i][3].ToString());
                        long filesize = GetFileSize (fileid );
                        ItemFilesList.Add(new ItemFile(Item_, fileid, filename,filedescription , filesize, datetime));
                    }
                   
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetItemFileList:"+ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error );

                }
                return ItemFilesList;
            }    
            public  long  GetFileSize(uint fileid)
            {
                try
                {

                    DataTable t = DB.GetData("select length("
                    + ItemFileTable.FileData
                    + ") from "
                    + ItemFileTable.TableName
                    + " where "
                    + ItemFileTable.FileID + "=" + fileid
                    );
                    if (t.Rows.Count > 0)
                    {
                        return (long)t.Rows[0][0];

                    }
                    else return -1;
                }
                catch (Exception ee)
                {
                    return -1;
                }
            }   
            public byte []  GetFileData(uint fileid)
            {
                try
                {

                    //DataTable t = new DataTable();
                    return DB.GetData_ByteArray ("select "
                    + ItemFileTable.FileData
                    + " from "
                    + ItemFileTable.TableName
                    + " where "
                    + ItemFileTable.FileID + "=" + fileid
                    );
                    //if (t.Rows.Count > 0)
                    //{
                    //    return (byte[])t.Rows[0][0];

                    //}
                    //else return null;
                }
                catch (Exception ee)
                {
                    throw new Exception ("Get file data: "+ee.Message );

                }
            }

        }

        public class ItemSpec_Restrict_SQL
        {
            DatabaseInterface DB;
            public static class ItemSpec_Restrict_Table
            {
                public const string TableName = "Item_ItemSpec_Restrict";
                public const string SpecID = "SpecID";
                public const string SpecName = "SpecName";
                public const string FolderID = "FolderID"; 
                public const string SpecIndex = "SpecIndex";
            }

            public ItemSpec_Restrict_SQL(DatabaseInterface db)
            {
                DB = db;
            }
   

            public bool AddItemSpecRestrict(Folder Folder_,string ItemSpecName_, uint specindex)
            {
                    try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("insert into " + ItemSpec_Restrict_Table.TableName
                            + " ("
                            + ItemSpec_Restrict_Table.FolderID +","
                            + ItemSpec_Restrict_Table.SpecName + ","
                             + ItemSpec_Restrict_Table.SpecIndex  
                            + ")values("
                            +Folder_ .FolderID +","
                            +"'"+ ItemSpecName_ + "'" + ","
                            +specindex 
                            + ")"
                         );
                    DB.AddLog(
                   DatabaseInterface.Log.LogType.INSERT 
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict
                   , ""
                 , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool UpdatetemSpecRestrict(uint id,string newname, uint specindex)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update " + ItemSpec_Restrict_Table.TableName
                            + " set "
                            + ItemSpec_Restrict_Table.SpecName + "='"+newname +"',"
                            + ItemSpec_Restrict_Table.SpecIndex  + "="+specindex 
                            + " where "
                            + ItemSpec_Restrict_Table.SpecID +"="+id
                         );
                    DB.AddLog(
                 DatabaseInterface.Log.LogType.UPDATE 
                 , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict
                 , ""
               , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool DeleteItemSpecRestrict(uint id)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from "
                           + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble.TableName
                           + " where "
                           + ItemSpec_Restrict_Value_SQL.ItemSpec_Restrict_Value_TAble.SpecID + "=" + id
                       );
                    DB.ExecuteSQLCommand("delete from " 
                            + ItemSpec_Restrict_Options_SQL.ItemSpec_Restrict_Option_Table.TableName
                            + " where "
                            + ItemSpec_Restrict_Options_SQL.ItemSpec_Restrict_Option_Table.SpecID + "=" + id
                        );
                    DB.ExecuteSQLCommand("delete from " 
                            + ItemSpec_Restrict_Table.TableName
                            + " where "
                            + ItemSpec_Restrict_Table.SpecID + "=" + id
                         );
                    DB.AddLog(
                 DatabaseInterface.Log.LogType.DELETE 
                 , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict
                 , ""
               , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public ItemSpec_Restrict  GetItemSpecRestrictInfoByID(uint specid)
            {
                try
                {
                    FolderSQL foldersql = new FolderSQL(DB);
                    ItemSpec_Restrict m;
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + ItemSpec_Restrict_Table.FolderID + ","
                        + ItemSpec_Restrict_Table.SpecName +","
                         + ItemSpec_Restrict_Table.SpecIndex 
                       + " from " + ItemSpec_Restrict_Table.TableName
                       + " where " + ItemSpec_Restrict_Table.SpecID + "=" + specid);
                    if (t.Rows.Count > 0)
                    {
                        m = new ItemSpec_Restrict(foldersql.GetFolderInfoByID(Convert.ToUInt32(t.Rows[0][0])), specid,  t.Rows[0][1].ToString(), Convert.ToUInt32(t.Rows[0][2]));

                    }
                    else m = null;
                    return m;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب بيانات الخاصية"+ee.Message );
                    return null;
                }
               
            }

            public List<ItemSpec_Restrict  > GetItemSpecRestrictList(Folder  Folder_)
            {
                List<ItemSpec_Restrict> list = new List<ItemSpec_Restrict>();
                try
                {
  
                     DataTable  t = DB.GetData("select "
                        + ItemSpec_Restrict_Table.SpecID + ","
                        + ItemSpec_Restrict_Table.SpecName + ","
                        + ItemSpec_Restrict_Table.SpecIndex 
                       + " from " + ItemSpec_Restrict_Table.TableName
                       + " where " + ItemSpec_Restrict_Table.FolderID + "=" + Folder_.FolderID
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint specid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string specname = t.Rows[i][1].ToString();
                        uint specindex= Convert.ToUInt32(t.Rows[i][2].ToString());

                        list.Add(new ItemSpec_Restrict(Folder_, specid, specname, specindex));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب خاصيات المجلد" + ee.Message);
                    return null;
                }

            }
        }
        public class ItemSpec_Restrict_Options_SQL
        {
            public static class ItemSpec_Restrict_Option_Table
            {
                public const string TableName = "Item_ItemSpec_Restrict_Options";
                public const string SpecID = "SpecID";
                public const string OptionID = "OptionID";
                public const string OptionName = "OptionName";
   

            }
            DatabaseInterface DB;
            public ItemSpec_Restrict_Options_SQL(DatabaseInterface db)
            {
                DB = db;
            }
            public ItemSpec_Restrict_Options Get_ItemSpec_Restrict_Options_Info_ByName(ItemSpec_Restrict ItemSpec_Restrict_, string  option_name)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                   + ItemSpec_Restrict_Option_Table.OptionID
                   + " from  "
                  + ItemSpec_Restrict_Option_Table.TableName
                  + " where "
                  + ItemSpec_Restrict_Option_Table.SpecID  + "=" + ItemSpec_Restrict_.SpecID 
                  + " and "
                  + ItemSpec_Restrict_Option_Table.OptionName + "='" + option_name+"'"

                  );
                if (t.Rows.Count == 1) return new ItemSpec_Restrict_Options(ItemSpec_Restrict_, Convert.ToUInt32(t.Rows[0][0]), option_name);
                else return null;
            }
            public string Get_ItemSpec_Restrict_Options_Name_ByID(uint optionid)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select "
                   +ItemSpec_Restrict_Option_Table.OptionName
                   + " from  "
                  + ItemSpec_Restrict_Option_Table.TableName
                  + " where "
                  + ItemSpec_Restrict_Option_Table.OptionID  + "=" + optionid

                  );
                if (t.Rows.Count > 0) return t.Rows [0][0].ToString ();
                else return null;
            }
            public bool Add_ItemSpec_Restrict_Option(ItemSpec_Restrict  ItemSpecRestrict_,string optionname)
            {

                    if(optionname.Length  == 0)
                    {
                        MessageBox.Show("القيمة يجب ان لا تكون فارغة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false ;
                    }
                 if (IsExists(ItemSpecRestrict_,optionname ))
                {
                    MessageBox.Show("القيمة مودخلة بلفعل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("insert into  "
                         + ItemSpec_Restrict_Option_Table.TableName
                         + " ( "
                         + ItemSpec_Restrict_Option_Table.SpecID + ","
                         + ItemSpec_Restrict_Option_Table.OptionName 
                         + ")values( "
                         + ItemSpecRestrict_.SpecID + ","
                         + "'"+ optionname + "'"  
                         + ")"
                         );
                    DB.AddLog(
              DatabaseInterface.Log.LogType.INSERT
              , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Option
              , ""
            , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Option
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool Update_ItemSpec_Restrict_Option(uint OptionID, string new_optioname)
            {

                    if (new_optioname.Length == 0)
                    {
                        MessageBox.Show("القيمة يجب ان لا تكون فارغة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                 
                    try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update  "
                         + ItemSpec_Restrict_Option_Table.TableName
                         + " set "
                         + ItemSpec_Restrict_Option_Table.OptionName + "=" + "'" + new_optioname + "'" 
                         + " where  "
                         + ItemSpec_Restrict_Option_Table.OptionID + "=" + OptionID
                         );
                    DB.AddLog(
                          DatabaseInterface.Log.LogType.UPDATE 
                          , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Option
                          , ""
                        , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Option
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Delete_ItemSpec_Restrict_Option(uint OptionID)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from   "
                     + ItemSpec_Restrict_Option_Table.TableName
                     + " where  "
                     + ItemSpec_Restrict_Option_Table.OptionID + "=" + OptionID
                     );
                    DB.AddLog(
              DatabaseInterface.Log.LogType.DELETE 
              , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Option
              , ""
            , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Option
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool IsExists(ItemSpec_Restrict  ItemSpecRestrict_,string optionname)
            {
                DataTable t = new DataTable();
                t = DB.GetData("select * from  "
                  + ItemSpec_Restrict_Option_Table.TableName
                  + " where "
                  + ItemSpec_Restrict_Option_Table.SpecID  + "=" + ItemSpecRestrict_.SpecID 
                  +" and "
                  + ItemSpec_Restrict_Option_Table.OptionName+"='"+ optionname + "'"
                  );
                if (t.Rows.Count > 0) return true;
                else return false;
            }
            public List<ItemSpec_Restrict_Options > GetItemSpec_Restrict_Options_List(ItemSpec_Restrict ItemSpec_Restrict_)
            {
                try
                {
                    List<ItemSpec_Restrict_Options> ItemSpec_Restrict_Options_List = new List<ItemSpec_Restrict_Options>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                      + ItemSpec_Restrict_Option_Table.OptionID + ","
                      + ItemSpec_Restrict_Option_Table.OptionName
                       + " from  "
                     + ItemSpec_Restrict_Option_Table.TableName
                     + " where "
                     + ItemSpec_Restrict_Option_Table.SpecID + "=" + ItemSpec_Restrict_.SpecID
                     );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        ItemSpec_Restrict_Options ItemSpec_Restrict_Options_ = new ItemSpec_Restrict_Options(ItemSpec_Restrict_, Convert.ToUInt32(t.Rows[i][0].ToString()), t.Rows[i][1].ToString());
                        ItemSpec_Restrict_Options_List.Add(ItemSpec_Restrict_Options_);
                    }
                    return ItemSpec_Restrict_Options_List;
                }
                catch(Exception ee)
                {
                    MessageBox.Show("GetItemSpec_Restrict_Options_List:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
                    return null;
                }
               
            }
            
  
        }
        public  class ItemSpec_Restrict_Value_SQL
        {
            public static class ItemSpec_Restrict_Value_TAble
            {
                public const string TableName = "Item_ItemSpec_Restrict_Value";
                public const string ItemID = "ItemID";
                public const string OptionID = "OptionID";
                public const string SpecID = "SpecID";
            }
            DatabaseInterface DB;
            public ItemSpec_Restrict_Value_SQL(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public List <ItemSpec_Restrict_Value> Get_ItemValuesList_For_SpecRestrict(Item Item_, ItemSpec_Restrict ItemSpec_Restrict_)
            {
               
                try
                {
                    List<ItemSpec_Restrict_Value> ItemSpec_Restrict_ValueList = new List<ItemSpec_Restrict_Value>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ItemSpec_Restrict_Value_TAble.OptionID  
                       + " from " + ItemSpec_Restrict_Value_TAble.TableName
                       + " where "
                       + ItemSpec_Restrict_Value_TAble.ItemID  + "=" + Item_.ItemID
                       + " and  "
                       + ItemSpec_Restrict_Value_TAble.SpecID + "=" + ItemSpec_Restrict_.SpecID 
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint optionid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string optionname = new ItemSpec_Restrict_Options_SQL(DB).Get_ItemSpec_Restrict_Options_Name_ByID(optionid);

                        ItemSpec_Restrict_ValueList.Add(new ItemSpec_Restrict_Value(Item_, ItemSpec_Restrict_, new ItemSpec_Restrict_Options(ItemSpec_Restrict_, optionid, optionname)));
                    }
                    return ItemSpec_Restrict_ValueList;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("   فشل جلب قيم العنصر للخاصية" + ee.Message);
                    return null;
                }

            }
            public bool Add_ItemSpec_Restrict_Value(Item Item_,ItemSpec_Restrict_Options ItemSpec_Restrict_Options_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (IS_Exists_ItemSpec_Restrict_Value(Item_, ItemSpec_Restrict_Options_) )
                    {
                        MessageBox.Show("القيمة مدخلة بلفعل","خطا",MessageBoxButtons.OK ,MessageBoxIcon.Error );
                        return false;
                    }
   
                    DB.ExecuteSQLCommand("insert into "
                        + ItemSpec_Restrict_Value_TAble.TableName 
                        +"("
                        + ItemSpec_Restrict_Value_TAble.ItemID+","
                        + ItemSpec_Restrict_Value_TAble.SpecID  + ","
                        + ItemSpec_Restrict_Value_TAble.OptionID
                        + ")values("
                        +Item_ .ItemID +","
                        + ItemSpec_Restrict_Options_.ItemSpecRestrict_.SpecID +","
                        + ItemSpec_Restrict_Options_.OptionID 
                        +")"

                        );
                    DB.AddLog(
             DatabaseInterface.Log.LogType.INSERT
             , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Value 
             , ""
           , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Value
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool IS_Exists_ItemSpec_Restrict_Value(Item Item_, ItemSpec_Restrict_Options ItemSpec_Restrict_Options_)
            {
                try
                {
                    DataTable t =   DB.GetData ("select * from  "
                        + ItemSpec_Restrict_Value_TAble.TableName
                        + " where "
                        + ItemSpec_Restrict_Value_TAble.ItemID + "=" + Item_.ItemID
                        +" and "
                        + ItemSpec_Restrict_Value_TAble.SpecID + "="+ ItemSpec_Restrict_Options_.ItemSpecRestrict_.SpecID
                        + " and "
                        + ItemSpec_Restrict_Value_TAble.OptionID + "=" + ItemSpec_Restrict_Options_.OptionID
                        );
                    if (t.Rows.Count > 0)
                        return true;
                    else return false;
                }
                catch
                {
                    return false;
                }
            }
            public bool Delete_ItemValueRestrict(Item Item_, ItemSpec_Restrict_Options ItemSpec_Restrict_Options_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from  "
                        + ItemSpec_Restrict_Value_TAble.TableName
                        + " where "
                        + ItemSpec_Restrict_Value_TAble.ItemID + "=" + Item_.ItemID
                        +" and "
                        + ItemSpec_Restrict_Value_TAble.SpecID + "="+ ItemSpec_Restrict_Options_.ItemSpecRestrict_.SpecID
                        + " and "
                        + ItemSpec_Restrict_Value_TAble.OptionID  + "="+ ItemSpec_Restrict_Options_.OptionID 
                       
                      

                        );
                    DB.AddLog(
            DatabaseInterface.Log.LogType.DELETE 
            , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Value
            , ""
          , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Restrict_Value
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public class ItemSpecSQL
        {
            DatabaseInterface DB;
            public static class ItemSpecTable
            {
                public const string TableName = "Item_ItemSpec";
                public const string SpecID = "SpecID";
                public const string SpecName = "SpecName";
                public const string FolderID = "FolderID";
                public const string SpecIndex = "SpecIndex";
            }

            public ItemSpecSQL(DatabaseInterface db)
            {
                DB = db;
            }


            public bool AddItemSpec(Folder Folder_, string ItemSpecName_,uint specindex)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("insert into " + ItemSpecTable.TableName
                            + " ("
                            + ItemSpecTable.FolderID + ","
                            + ItemSpecTable.SpecName + ","
                            + ItemSpecTable.SpecIndex 
                            + ")values("
                            + Folder_.FolderID + ","
                            + "'" + ItemSpecName_ + "'," 
                            +specindex 
                            + ")"
                         );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.INSERT
                        , DatabaseInterface.Log.Log_Target.Item_Spec
                        , ""
                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_Spec
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool UpdatetemSpec(uint id, string newname,uint specindex)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update " + ItemSpecTable.TableName
                            + " set "
                            + ItemSpecTable.SpecName + "='" + newname + "',"
                            + ItemSpecTable.SpecIndex  + "=" + specindex  
                            + " where "
                            + ItemSpecTable.SpecID + "=" + id
                         );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                      , DatabaseInterface.Log.Log_Target.Item_Spec
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public bool DeleteItemSpec(uint id)
            {

                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from " + ItemSpec_Value_SQL .ItemSpec_Value_Table.TableName
                            + " where "
                            + ItemSpec_Value_SQL.ItemSpec_Value_Table.SpecID + "=" + id
                         );
                    DB.ExecuteSQLCommand("delete from " + ItemSpecTable.TableName
                            + " where "
                            + ItemSpecTable.SpecID + "=" + id
                         );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_Spec
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            public ItemSpec GetItemSpecInfoByID(uint itemspecid)
            {
                try
                {
                    FolderSQL foldersql = new FolderSQL(DB);;
                    ItemSpec m;
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + ItemSpecTable.FolderID + ","
                        + ItemSpecTable.SpecName+","
                        + ItemSpecTable.SpecIndex 
                       + " from " + ItemSpecTable.TableName
                       + " where " + ItemSpecTable.SpecID + "=" + itemspecid);
                    if (t.Rows.Count > 0)
                    {
 
                        m = new ItemSpec(foldersql.GetFolderInfoByID(Convert.ToUInt32(t.Rows[0][0])), itemspecid, t.Rows[0][1].ToString(), Convert.ToUInt32(t.Rows[0][2]));

                    }
                    else m = null;
                    return m;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب بيانات الخاصية" + ee.Message);
                    return null;
                }

            }

            public List<ItemSpec> GetItemSpecList(Folder Folder_)
            {
                try
                {
                    List<ItemSpec> list = new List<ItemSpec>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + ItemSpecTable.SpecID + ","
                        + ItemSpecTable.SpecName + ","
                         + ItemSpecTable.SpecIndex 
                       + " from " + ItemSpecTable.TableName
                       + " where " + ItemSpecTable.FolderID + "=" + Folder_.FolderID);

                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint specid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        string specname = t.Rows[i][1].ToString();
                        uint specindex= Convert.ToUInt32(t.Rows[i][2].ToString());
                        list.Add(new ItemSpec(Folder_, specid, specname, specindex));
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("   فشل جلب خاصيات المجلد النصية" + ee.Message);
                    return null;
                }

            }
        }
        public class ItemSpec_Value_SQL
        {
            public static class ItemSpec_Value_Table
            {
                public const string TableName = "Item_ItemSpec_Value";
                public const string ItemID = "ItemID";
                public const string SpecID = "SpecID";
                public const string Value = "Value";

            }
            DatabaseInterface DB;
            public ItemSpec_Value_SQL(DatabaseInterface db)
            {
                DB = db;
            }
            public bool SetItemValue(Item Item_, ItemSpec ItemSpec_,string value)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    UNSetItemValueRestrict(Item_, ItemSpec_);

                    DB.ExecuteSQLCommand("insert into "
                        + ItemSpec_Value_Table.TableName
                        + "("
                        + ItemSpec_Value_Table.ItemID + ","
                        + ItemSpec_Value_Table.SpecID + ","
                        + ItemSpec_Value_Table.Value 
                        + ")values("
                        + Item_.ItemID + ","
                        + ItemSpec_.SpecID + ","
                        + "'"+value +"'"
                        + ")"

                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                      , DatabaseInterface.Log.Log_Target.Item_Spec_Value
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Value 
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("SetItemValue:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UNSetItemValueRestrict(Item Item_, ItemSpec ItemSpec_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from  "
                        + ItemSpec_Value_Table.TableName
                        + " where "
                        + ItemSpec_Value_Table.ItemID + "=" + Item_.ItemID
                        +" and "
                        + ItemSpec_Value_Table.SpecID + "=" + ItemSpec_.SpecID


                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_Spec_Value
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_Spec_Value
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("UNSetItemValueRestrict:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            public ItemSpec_Value  GetItemSpec_Value(Item item, ItemSpec ItemSpec_)
            {
                DataTable t = DB.GetData("select "
                     + ItemSpec_Value_Table.Value 
                   + " from " + ItemSpec_Value_Table.TableName
                   + " where " + ItemSpec_Value_Table.SpecID + "=" + ItemSpec_.SpecID
                   + " and " + ItemSpec_Value_Table.ItemID + "=" + item.ItemID);
                if (t.Rows.Count > 0)
                {

                    return new ItemSpec_Value(item, ItemSpec_, t.Rows[0][0].ToString ());
                }
                else
                {
                    return null;
                }
            }
 
        }
        public class ItemRelationShipsSQL
        {
            public static class ItemRelationShipsTable
            {
                public const string TableName = "Item_ItemRelationShips";
                public const string ItemID = "ItemID";
                public const string AnotherItemID = "AnotherItemID";
                public const string RelationShip = "RelationShip";
                public const string Notes = "Notes";
            }

            DatabaseInterface DB;
            public ItemRelationShipsSQL(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public bool ISDataExists(Item item, Item anotheritem)
            {
                try
                {
                    DataTable t = DB.GetData("select * from  "
                  + ItemRelationShipsTable.TableName
                  + " where ("
                  + ItemRelationShipsTable.ItemID + "=" + item.ItemID 
                  + " and "
                  + ItemRelationShipsTable.AnotherItemID     + "=" + anotheritem  .ItemID
                  + ") or ("
                  + ItemRelationShipsTable.ItemID   + "=" + anotheritem .ItemID
                  + " and "
                  + ItemRelationShipsTable.AnotherItemID + "=" + item  .ItemID +")"

                        );
                    if (t.Rows.Count > 0) return true;
                    else return false;
                }
                catch(SqlException sqlEx)
                {
                    throw new Exception("حدث خطأ اثناء الاتصال بقاعدة البيانات", sqlEx);
                }
            }
            public bool AddItemRelation(Item item, Item anotheritem, uint relation,string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (ISDataExists(item, anotheritem))
                    {
                        MessageBox.Show("توجد علاقة بين العنصرين معرفة مسبقا","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }
                    DB.ExecuteSQLCommand("insert into  "
                        + ItemRelationShipsTable.TableName
                        + " ( "
                        + ItemRelationShipsTable.ItemID + ","
                        + ItemRelationShipsTable.AnotherItemID  + ","
                        + ItemRelationShipsTable.RelationShip   + ","
                        + ItemRelationShipsTable.Notes 
                        + ")values( "
                        + item .ItemID  + ","
                        + anotheritem .ItemID + ","
                        +relation  + ","
                        +"'"+notes +"'"
                        + ")"
                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                      , DatabaseInterface.Log.Log_Target.Item_RealtionShip
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_RealtionShip
                      , ""
                    , false, ee.Message);
                    throw new Exception("AddItemRelation:" + ee.Message);
                    MessageBox.Show("AddItemRelation:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdateItemRelation(Item item, Item anotheritem, uint relation, string notes)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("update   "
                        + ItemRelationShipsTable.TableName   
                        +" set "         
                        + ItemRelationShipsTable.RelationShip + "="+relation +","
                         + ItemRelationShipsTable.AnotherItemID + "=" + anotheritem.ItemID + ","
                        + ItemRelationShipsTable.Notes+"='"+notes +"'"
                        + " where  "
                        + ItemRelationShipsTable.ItemID + "="+item .ItemID 
                        +" and "
                        + ItemRelationShipsTable.AnotherItemID + "="+anotheritem .ItemID 
                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                      , DatabaseInterface.Log.Log_Target.Item_RealtionShip
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Item_RealtionShip
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteItemRelation(uint  first_itemid,uint second_itemid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from   "
                                            + ItemRelationShipsTable.TableName
                                            + " where "
                                            + ItemRelationShipsTable.ItemID + "=" + first_itemid
                                            + "  and  "
                                            + ItemRelationShipsTable.AnotherItemID  + "=" + second_itemid
                                            );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                      , DatabaseInterface.Log.Log_Target.Item_RealtionShip
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_RealtionShip
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            internal ItemRelation GetItemRealtion(Item sourceItem, Item anotherItem)
            {
                try
                {

                    DataTable t = DB.GetData("select "
                   + ItemRelationShipsTable.RelationShip + ","
                   + ItemRelationShipsTable.Notes
                   + " from  "
                   + ItemRelationShipsTable.TableName
                   + " where "
                   + ItemRelationShipsTable.ItemID + "=" + sourceItem.ItemID
                   + " and "
                   + ItemRelationShipsTable.AnotherItemID + "=" + anotherItem.ItemID
                   );
                    if (t.Rows.Count == 1)
                    {
       
                        uint relation = Convert.ToUInt32(t.Rows[0][0].ToString());
                        string notes = t.Rows[0][1].ToString();
                        return new ItemRelation(sourceItem, anotherItem, relation, false, notes);
                    }
                    else 
                    return null;

                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    return null;
                }
               
            }
            public List <ItemRelation > GetItemRelationsList(Item item)
            {
                
                List<ItemRelation> ItemRelationList = new List<ItemRelation>();
                DataTable t = DB.GetData("select "
                  + ItemRelationShipsTable.ItemID +","
                  + ItemRelationShipsTable.AnotherItemID  + ","
                  + ItemRelationShipsTable.RelationShip + ","
                  + ItemRelationShipsTable.Notes  
                  + " from  "
                  + ItemRelationShipsTable.TableName
                  + " where "
                  + ItemRelationShipsTable.ItemID + "=" + item.ItemID
                  + " or "
                  + ItemRelationShipsTable.AnotherItemID  + "=" + item.ItemID
                  );
                
                ItemSQL ItemSQL_ = new ItemSQL(DB);
                for (int i=0;i<t.Rows .Count;i++)
                {
                    uint table_itemid1 = Convert.ToUInt32(t.Rows[i][0].ToString());
                    uint table_itemid2 = Convert.ToUInt32(t.Rows[i][1].ToString());
                    uint relation = Convert.ToUInt32 (t.Rows [i][2].ToString ());
                    string notes = t.Rows[i][3].ToString();
                    ItemRelation ItemRelation_;
                    if (table_itemid1 == item .ItemID )
                     ItemRelation_ = new ItemRelation(item ,ItemSQL_.GetItemInfoByID (table_itemid2),relation,false , notes );
                    else
                    {
                        switch (relation)
                        {
                            case 0:break;
                            case 1:relation = 2;break;
                            case 2:relation = 1;break;
                        }
                        ItemRelation_ = new ItemRelation(item, ItemSQL_.GetItemInfoByID(table_itemid1), relation, true , notes);

                    }

                    ItemRelationList.Add(ItemRelation_);
                }
                return ItemRelationList;
            }

         
        }
        public class ConsumeUnitSql
        {
            public static class ConsumeUnitTable
            {
                public const string TableName = "Item_ComsumeUnits";
                public const string ConsumeUnitID = "ConsumeUnitID";
                public const string ConsumeUnitName = "ConsumeUnitName";
                public const string ItemID = "ItemID";
                public const string Factor = "Factor";
            }
            DatabaseInterface DB;
            public ConsumeUnitSql(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public ConsumeUnit GetConsumeAmountinfo( uint ConsumeUnitid)
            {
                try
                {
                    DataTable t = DB.GetData("select "
                     + ConsumeUnitTable.ConsumeUnitID + ","
                     + ConsumeUnitTable.ConsumeUnitName + ","
                      + ConsumeUnitTable.ItemID + ","
                       + ConsumeUnitTable.Factor
                     + " from  "
                     + ConsumeUnitTable.TableName
                     + " where "
                     + ConsumeUnitTable.ConsumeUnitID + "=" + ConsumeUnitid);
                    if (t.Rows.Count == 0) return null;
                    else return new ConsumeUnit(Convert.ToUInt32(t.Rows[0][0].ToString()), t.Rows[0][1].ToString(), new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(t.Rows[0][2].ToString())), Convert.ToDouble(t.Rows[0][3].ToString()));

                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetConsumeAmountinfo:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
       

            public bool AddConsumeUnit(Item item, string ConsumeUnitname,double factor)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (ConsumeUnitname ==item .DefaultConsumeUnit )
                    {
                        MessageBox.Show("وسم وحدة التوزيع يجب ان يكون مختلف عن اسم وحدة التوزيع الافتراضية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
  
                    DB.ExecuteSQLCommand("insert into  "
                        + ConsumeUnitTable.TableName
                        + " ( "
                        + ConsumeUnitTable.ConsumeUnitName+","
                         + ConsumeUnitTable.ItemID  + ","
                          + ConsumeUnitTable.Factor  
                        + ")values( "
                        + "'" + ConsumeUnitname + "'"+","
                        + item .ItemID  + ","
                        +factor 
                        + ")"
                        );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.INSERT
                       , DatabaseInterface.Log.Log_Target.Item_ConsumeUint
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_ConsumeUint
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("AddConsumeUnit:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdateConsumeUnit(Item item,uint ConsumeUnitid, string newConsumeUnitname,double Factor)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (item.DefaultConsumeUnit == newConsumeUnitname)
                    {
                        MessageBox.Show("وسم وحدة التوزيع يجب ان يكون مختلف عن اسم وحدة التوزيع الافتراضية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    DB.ExecuteSQLCommand("update   "
                        + ConsumeUnitTable.TableName
                        + " set "
                        + ConsumeUnitTable.ConsumeUnitName + "='" + newConsumeUnitname + "',"
                         + ConsumeUnitTable.Factor  + "=" + Factor  
                        + " where "
                        + ConsumeUnitTable.ConsumeUnitID + "=" + ConsumeUnitid
                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                      , DatabaseInterface.Log.Log_Target.Item_ConsumeUint
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Item_ConsumeUint
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("UpdateConsumeUnit:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteConsumeUnit(uint ConsumeUnitid)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("Delete from    "
                        + ItemSellPriceSql.ItemSellPriceTable .TableName
                        + " where "
                        + ItemSellPriceSql.ItemSellPriceTable.ConsumeUnitID + "=" + ConsumeUnitid
                        );
                    DB.ExecuteSQLCommand("Delete from    "
                        + ConsumeUnitTable.TableName
                        + " where "
                        + ConsumeUnitTable.ConsumeUnitID + "=" + ConsumeUnitid
                        );
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.DELETE 
                       , DatabaseInterface.Log.Log_Target.Item_ConsumeUint
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_ConsumeUint
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("DeleteConsumeUnit:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<ConsumeUnit > GetConsumeUnitList(Item item_)
            {
                try
                {
                    List<ConsumeUnit> list = new List<ConsumeUnit>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + ConsumeUnitTable.ConsumeUnitID + ","
                           + ConsumeUnitTable.ConsumeUnitName + ","
                          + ConsumeUnitTable.Factor
                        + " from "
                        + ConsumeUnitTable.TableName
                         + " where "
                        + ConsumeUnitTable.ItemID  + "=" + item_.ItemID 
                       );

                    list.Add(new ConsumeUnit(0, item_.DefaultConsumeUnit, item_, 1));
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        ConsumeUnit m = new ConsumeUnit(Convert.ToUInt32(t.Rows[i][0]), t.Rows[i][1].ToString(), item_, Convert.ToDouble(t.Rows[i][2].ToString()));
                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب قائمة وحدات التوزيع:", ee.Message);
                    return null;
                }

            }

            internal bool IsConsumeUnitBelongToItem(Item _Item, ConsumeUnit consumeUnit_)
            {
                try
                {
                    if (consumeUnit_ == null || consumeUnit_.ConsumeUnitID == 0) return true;
                    DataTable  t = DB.GetData("select *  from "
                       + ConsumeUnitTable.TableName
                        + " where "
                       + ConsumeUnitTable.ItemID + "=" + _Item.ItemID
                        + " and  "
                       + ConsumeUnitTable.ConsumeUnitID + "=" + consumeUnit_.ConsumeUnitID 
                      );
                    if (t.Rows.Count == 1) return true;
                    else return false;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("IsConsumeUnitBelongToItem:"+ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public class ItemSellPriceSql
        {
            public static class ItemSellPriceTable
            {
                public const string TableName = "Item_SellPrices";
                public const string SellPriceID = "SellPriceID";
                public const string ItemID = "ItemID";
                public const string TradeStateID = "TradeStateID";
                public const string ConsumeUnitID = "ConsumeUnitID";
                public const string SellTypeID = "SellTypeID";
                public const string Price = "Price";
            }
            DatabaseInterface DB;
            public ItemSellPriceSql(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public bool IsPriceSet(Item item_, TradeState TradeState_, ConsumeUnit ConsumeUnit_, SellType SellType_)
            {
                try
                {
                    string cid_string = " is null";
                    if(ConsumeUnit_ !=null )
                    if (ConsumeUnit_.ConsumeUnitID != 0 ) cid_string = "=" + ConsumeUnit_.ConsumeUnitID.ToString();

                    DataTable t = DB.GetData("select * from   "
                        + ItemSellPriceTable.TableName
                        + " where "
                       + ItemSellPriceTable.ItemID + "=" + item_.ItemID + " and "
                        + ItemSellPriceTable.TradeStateID + "=" + TradeState_.TradeStateID + " and "
                        + ItemSellPriceTable.ConsumeUnitID + cid_string + " and "
                        + ItemSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID

                        );
                    if (t.Rows.Count > 0)
                        return true;
                    else return false;

                }
                catch (Exception ee)
                {
                    throw new Exception ("IsPriceSet :" + ee.Message);

                }
                
            }
             public bool SetItemPrice(Item item_, TradeState TradeState_, ConsumeUnit ConsumeUnit_, SellType SellType_,double price)
            {
                bool is_price_set = false;
                try
                {
                    is_price_set = IsPriceSet(item_, TradeState_, ConsumeUnit_, SellType_);
                }
                catch(Exception ee)
                {
                    MessageBox.Show("فشل ضبط التسعير:" +ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
                if(!is_price_set )
                {
                    string cid_string = "  null";
                    if (ConsumeUnit_ != null)
                        if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = ConsumeUnit_.ConsumeUnitID.ToString();
                    try
                    {

                        if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                        DB.ExecuteSQLCommand("insert into  "
                            + ItemSellPriceTable.TableName
                            + " ( "
                            + ItemSellPriceTable.ItemID + ","
                            + ItemSellPriceTable.TradeStateID + ","
                             + ItemSellPriceTable.ConsumeUnitID + ","
                              + ItemSellPriceTable.SellTypeID + ","
                            + ItemSellPriceTable.Price
                            + ")values( "
                            + item_.ItemID + ","
                            + TradeState_.TradeStateID  + ","
                            + cid_string + ","
                            + SellType_.SellTypeID + ","
                            + price
                            + ")"
                            );
                        DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                      , DatabaseInterface.Log.Log_Target.Item_SellPrice 
                      , ""
                    , true, "");
                        return true;
                    }
                    catch (Exception ee)
                    {
                        DB.AddLog(
                          DatabaseInterface.Log.LogType.INSERT
                       , DatabaseInterface.Log.Log_Target.Item_SellPrice
                          , ""
                        , false, ee.Message);
                        MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {

                    string cid_string = " is null";
                    if (ConsumeUnit_ != null)
                        if (ConsumeUnit_.ConsumeUnitID != 0) cid_string ="="+ ConsumeUnit_.ConsumeUnitID.ToString();
                    try
                    {
                        if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                        DB.ExecuteSQLCommand( "update  "
                            + ItemSellPriceTable.TableName
                            + " set "
                            + ItemSellPriceTable.Price+"="+price 
                            +" where "
                            + ItemSellPriceTable.ItemID +"="+ item_.ItemID + " and "
                            + ItemSellPriceTable.TradeStateID + "=" + TradeState_.TradeStateID  + " and "
                             + ItemSellPriceTable.ConsumeUnitID + cid_string + " and "
                              + ItemSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID 
                            );
                        DB.AddLog(
                     DatabaseInterface.Log.LogType.UPDATE 
                     , DatabaseInterface.Log.Log_Target.Item_SellPrice
                     , ""
                   , true, "");
                        return true;
                    }
                    catch (Exception ee)
                    {
                        DB.AddLog(
                          DatabaseInterface.Log.LogType.UPDATE 
                       , DatabaseInterface.Log.Log_Target.Item_SellPrice
                          , ""
                        , false, ee.Message);
                        MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                
            }

            public bool UNSetItemPrice(Item item_, TradeState TradeState_, ConsumeUnit ConsumeUnit_, SellType SellType_)
            {
                string cid_string = " is null";
                if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = "=" + ConsumeUnit_.ConsumeUnitID.ToString();
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from   "
                        + ItemSellPriceTable.TableName
                        + " where "
                       + ItemSellPriceTable.ItemID + "=" + item_.ItemID + " and "
                       + ItemSellPriceTable.TradeStateID + "=" + TradeState_.TradeStateID  + " and "
                        + ItemSellPriceTable.ConsumeUnitID + cid_string + " and "
                        + ItemSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID
                        
                        );
                    DB.AddLog(
                     DatabaseInterface.Log.LogType.DELETE 
                     , DatabaseInterface.Log.Log_Target.Item_SellPrice
                     , ""
                   , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_SellPrice
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public double? GetPrice(Item item, TradeState TradeState_, SellType SellType_, ConsumeUnit ConsumeUnit_)
            {
                try
                {

                    string cid_string = " is null";
                    if(ConsumeUnit_ !=null)
                    if (ConsumeUnit_.ConsumeUnitID != 0) cid_string = "="+ConsumeUnit_.ConsumeUnitID.ToString();
                    double? price;

                    DataTable t = DB.GetData("select "
                          + ItemSellPriceTable.Price
                        + " from "
                        + ItemSellPriceTable.TableName
                         + " where "
                        + ItemSellPriceTable.ItemID + "=" + item.ItemID
                        + " and "
                        + ItemSellPriceTable.TradeStateID + "=" + TradeState_.TradeStateID
                        + " and "
                         + ItemSellPriceTable.SellTypeID + "=" + SellType_.SellTypeID
                        + " and "
                         + ItemSellPriceTable.ConsumeUnitID + cid_string);

                    if (t.Rows.Count == 1)
                    {
                        price = Convert.ToDouble(t.Rows[0][0]);
                    }
                    else price = null;
                    return price;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);return null;
                }
            }
            public List<ItemSellPrice > GetItemPrices(Item item_,TradeState TradeState_)
            {
                try
                {
                    List<ItemSellPrice> list = new List<ItemSellPrice>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                         + ItemSellPriceTable.ConsumeUnitID + ","
                           + ItemSellPriceTable.SellTypeID  + ","
                          + ItemSellPriceTable.Price 
                        + " from "
                        + ItemSellPriceTable.TableName
                         + " where "
                        + ItemSellPriceTable.ItemID + "=" + item_.ItemID
                        + " and "
                        + ItemSellPriceTable.TradeStateID + "=" + TradeState_.TradeStateID

                       );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        ConsumeUnit CU;
                        try
                        {
                            CU = new ConsumeUnitSql(DB).GetConsumeAmountinfo(Convert.ToUInt32(t.Rows[i][0].ToString()));
                        }
                        catch
                        {
                            CU = new ConsumeUnit (0,item_ .DefaultConsumeUnit ,item_ ,1);
                        }
                        ItemSellPrice m = new ItemSellPrice( item_, TradeState_, CU, new SellTypeSql (DB).GetSellTypeinfo(Convert .ToUInt32 (t.Rows[i][1].ToString())),Convert .ToDouble (t.Rows[i][2].ToString()));
                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب اسعار العنصر:", ee.Message);
                    return null;
                }

            }
            public List<ItemSellPrice> GetItemPrices(Item item_)
            {
                try
                {
                    List<ItemSellPrice> list = new List<ItemSellPrice>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        +ItemSellPriceTable .TradeStateID +","
                         + ItemSellPriceTable.ConsumeUnitID + ","
                           + ItemSellPriceTable.SellTypeID + ","
                          + ItemSellPriceTable.Price
                        + " from "
                        + ItemSellPriceTable.TableName
                         + " where "
                        + ItemSellPriceTable.ItemID + "=" + item_.ItemID


                       );

                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        TradeState TradeState_ = new TradeStateSQL(DB).GetTradeStateBYID(Convert.ToUInt32(t.Rows[i][0].ToString()));
                        ConsumeUnit CU;
                        try
                        {
                            CU = new ConsumeUnitSql(DB).GetConsumeAmountinfo(Convert.ToUInt32(t.Rows[i][1].ToString()));
                        }
                        catch
                        {
                            CU = new ConsumeUnit(0, item_.DefaultConsumeUnit, item_, 1);
                        }
                        ItemSellPrice m = new ItemSellPrice(item_, TradeState_, CU, new SellTypeSql(DB).GetSellTypeinfo(Convert.ToUInt32(t.Rows[i][2].ToString())), Convert.ToDouble(t.Rows[i][3].ToString()));
                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("فشل جلب اسعار العنصر:", ee.Message);
                    return null;
                }

            }
        }

        public class Equivalence_GroupSQL
        {
            public static class Equivalence_GroupTable
            {
                public const string TableName = "Item_Equivalence_Group";
                public const string Equivalence_GroupID = "Equivalence_GroupID";
                public const string Equivalence_GroupName = "Equivalence_GroupName";
            }
            DatabaseInterface DB;
            public Equivalence_GroupSQL(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public Equivalence_Group GetEquivalence_Groupinfo_By_ID(uint Equivalence_Groupid)
            {
                try
                {

                    DataTable t = DB.GetData("select "
                    + Equivalence_GroupTable.Equivalence_GroupID + ","
                    + Equivalence_GroupTable.Equivalence_GroupName
                    + " from  "
                    + Equivalence_GroupTable.TableName
                    + " where "
                    + Equivalence_GroupTable.Equivalence_GroupID + "=" + Equivalence_Groupid);
                    if (t.Rows.Count == 0) return null;
                    else return new Equivalence_Group(Convert.ToUInt32(t.Rows[0][0].ToString()), t.Rows[0][1].ToString());
                }
                catch (Exception ee)
                {
                    throw new Exception("GetEquivalence_Groupinfo_By_ID:" + ee.Message );
                   
                }
                
            }
            public Equivalence_Group GetEquivalence_Groupinfo(string Equivalence_Group_Name)
            {
                DataTable t = DB.GetData("select "
                 + Equivalence_GroupTable.Equivalence_GroupID + ","
                + Equivalence_GroupTable.Equivalence_GroupName
                + " from  "
                + Equivalence_GroupTable.TableName
                + " where "
            + Equivalence_GroupTable.Equivalence_GroupName + "='" + Equivalence_Group_Name + "'");
                if (t.Rows.Count == 0) return null;
                else return new Equivalence_Group(Convert.ToUInt32(t.Rows[0][0].ToString()), t.Rows[0][1].ToString());
            }
            public bool IsEquivalence_GroupExists(string Equivalence_Group_name)
            {
                try
                {
                    DataTable t = DB.GetData("select * from  "
                  + Equivalence_GroupTable.TableName
                  + " where "
                  + Equivalence_GroupTable.Equivalence_GroupName + "='" + Equivalence_Group_name + "'");
                    if (t.Rows.Count > 0) return true;
                    else return false;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("حدث خطأ اثناء الاتصال بقاعدة البيانات", sqlEx);
                }
            }

            public bool AddEquivalence_Group(string Equivalence_Groupname)
            {
                try
                {
                    if (IsEquivalence_GroupExists(Equivalence_Groupname))
                    {
                        MessageBox.Show("الاسم موجود سابقا", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
    
                    DB.ExecuteSQLCommand( "insert into  "
                        + Equivalence_GroupTable.TableName
                        + " ( "
                        + Equivalence_GroupTable.Equivalence_GroupName
                        + ")values( "
                        + "'" + Equivalence_Groupname + "'"
                        + ")"
                        );
                   
                    DB.AddLog(
                       DatabaseInterface.Log.LogType.INSERT
                       , DatabaseInterface.Log.Log_Target.Item_Equivalence_Group
                       , ""
                     , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_Equivalence_Group
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("AddEquivalence_Group:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UpdatEquivalence_Group(uint Equivalence_Groupid, string new_Equivalence_Group_name)
            {
                try
                {
                    DB.ExecuteSQLCommand( "update   "
                        + Equivalence_GroupTable.TableName
                        + " set "
                        + Equivalence_GroupTable.Equivalence_GroupName + "='" + new_Equivalence_Group_name + "'"
                        + " where "
                        + Equivalence_GroupTable.Equivalence_GroupID + "=" + Equivalence_Groupid
                        );
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                      , DatabaseInterface.Log.Log_Target.Item_Equivalence_Group
                      , ""
                    , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.UPDATE 
                   , DatabaseInterface.Log.Log_Target.Item_Equivalence_Group
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("update_Equivalence_Group:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool DeleteEquivalence_Group(uint Equivalence_Groupid)
            {
                try
                {
                    bool success = new Item_Equivalence_Relation_SQL(DB).Delete_Item_Equivalence_Relation_For_Group(Equivalence_Groupid);
                    if (!success) return false;
                    DB.ExecuteSQLCommand( "delete from    "
                        + Equivalence_GroupTable.TableName
                        + " where "
                        + Equivalence_GroupTable.Equivalence_GroupID + "=" + Equivalence_Groupid
                        );
                    DB.AddLog(
                        DatabaseInterface.Log.LogType.DELETE 
                        , DatabaseInterface.Log.Log_Target.Item_Equivalence_Group
                        , ""
                      , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE 
                   , DatabaseInterface.Log.Log_Target.Item_Equivalence_Group
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("DeleteEquivalence_Group:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public List<Equivalence_Group> GetEquivalence_GroupList()
            {
                try
                {
                    List<Equivalence_Group> list = new List<Equivalence_Group>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select * from "
                        + Equivalence_GroupTable.TableName
                       );


                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        Equivalence_Group m = new Equivalence_Group(Convert.ToUInt32(t.Rows[i][0]), t.Rows[i][1].ToString());
                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("GetEquivalence_GroupList:"+ ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
                    return null;
                }

            }
        }

        public class Item_Equivalence_Relation_SQL
        {
            public static class Item_Equivalence_Relation_TAble
            {
                public const string TableName = "Item_Item_Equivalence_Relation";
                public const string ItemID = "ItemID";
                public const string Equivalence_GroupID = "Equivalence_GroupID";
            }
            DatabaseInterface DB;
            public Item_Equivalence_Relation_SQL(DatabaseInterface DB_)
            {
                DB = DB_;
            }
            public List<Item_Equivalence_Relation> Get_Item_Equivalence_Relation_By_Group(Equivalence_Group Equivalence_Group_)
            {

                try
                {
                    List<Item_Equivalence_Relation> Item_Equivalence_RelationList = new List<Item_Equivalence_Relation>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + Item_Equivalence_Relation_TAble.ItemID 
                       + " from " + Item_Equivalence_Relation_TAble.TableName
                       + " where "
                       + Item_Equivalence_Relation_TAble.Equivalence_GroupID + "=" + Equivalence_Group_.GroupID 
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint itemid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        Item item  = new ItemSQL(DB).GetItemInfoByID(itemid);

                        Item_Equivalence_RelationList.Add(new Item_Equivalence_Relation(Equivalence_Group_,item));
                    }
                    return Item_Equivalence_RelationList;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(" Get_Item_Equivalence_Relation_By_Group" + ee.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
                    return null;
                }

            }
            public List<Item_Equivalence_Relation> Get_Item_Equivalence_Relation_By_Item(Item item_)
            {

                try
                {
                    List<Item_Equivalence_Relation> Item_Equivalence_RelationList = new List<Item_Equivalence_Relation>();
                    DataTable t = new DataTable();
                    t = DB.GetData("select "
                        + Item_Equivalence_Relation_TAble.Equivalence_GroupID 
                       + " from " + Item_Equivalence_Relation_TAble.TableName
                       + " where "
                       + Item_Equivalence_Relation_TAble.ItemID  + "=" + item_ .ItemID 
                       );
                    for (int i = 0; i < t.Rows.Count; i++)
                    {

                        uint groupid = Convert.ToUInt32(t.Rows[i][0].ToString());
                        Equivalence_Group Equivalence_Group_ = new Equivalence_GroupSQL(DB).GetEquivalence_Groupinfo_By_ID (groupid );

                        Item_Equivalence_RelationList.Add(new Item_Equivalence_Relation(Equivalence_Group_, item_));
                    }
                    return Item_Equivalence_RelationList;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(" Get_Item_Equivalence_Relation_By_Group" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            public bool Set_Item_Equivalence_Relation(Item Item_, Equivalence_Group Equivalence_Group_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    if (IS_Exists_Item_Equivalence_Relation(Item_, Equivalence_Group_))
                    {
                        MessageBox.Show("القيمة مضبوطة بلفعل", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    DB.ExecuteSQLCommand("insert into "
                        + Item_Equivalence_Relation_TAble.TableName
                        + "("
                        + Item_Equivalence_Relation_TAble.ItemID + ","
                        + Item_Equivalence_Relation_TAble.Equivalence_GroupID 
                        + ")values("
                        + Item_.ItemID + ","
                        + Equivalence_Group_.GroupID  
                        + ")"

                        );
                    DB.AddLog(
             DatabaseInterface.Log.LogType.INSERT
             , DatabaseInterface.Log.Log_Target.Item_Equivalence_Relation
             , ""
           , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.INSERT
                   , DatabaseInterface.Log.Log_Target.Item_Equivalence_Relation
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("Set_Item_Equivalence_Relation:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool IS_Exists_Item_Equivalence_Relation(Item Item_, Equivalence_Group Equivalence_Group_)
            {
                try
                {
                    DataTable t = DB.GetData("select * from  "
                        + Item_Equivalence_Relation_TAble.TableName
                        + " where "
                        + Item_Equivalence_Relation_TAble.ItemID + "=" + Item_.ItemID
                        +" and "
                        + Item_Equivalence_Relation_TAble.Equivalence_GroupID + "=" + Equivalence_Group_.GroupID 
                        );
                    if (t.Rows.Count > 0)
                        return true;
                    else return false;
                }
                catch(Exception ee)
                {
                    MessageBox.Show("IS_Exists_Item_Equivalence_Relation:" + ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool UNSet_Item_Equivalence_Relation(uint Itemid_, uint  Equivalence_Groupid_)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from  "
                        + Item_Equivalence_Relation_TAble.TableName
                        + " where "
                        + Item_Equivalence_Relation_TAble.ItemID + "=" + Itemid_ 
                        + " and "
                        + Item_Equivalence_Relation_TAble.Equivalence_GroupID + "=" + Equivalence_Groupid_



                        );
                    DB.AddLog(
            DatabaseInterface.Log.LogType.DELETE
            , DatabaseInterface.Log.Log_Target.Item_Equivalence_Relation
            , ""
          , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE
                   , DatabaseInterface.Log.Log_Target.Item_Equivalence_Relation
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("UNSet_Item_Equivalence_Relation:"+ee.Message , "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool Delete_Item_Equivalence_Relation_For_Group( uint Equivalence_GroupID)
            {
                try
                {
                    if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Item_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                    DB.ExecuteSQLCommand("delete from  "
                        + Item_Equivalence_Relation_TAble.TableName
                        + " where "
                        + Item_Equivalence_Relation_TAble.Equivalence_GroupID + "=" + Equivalence_GroupID



                        );
                    DB.AddLog(
            DatabaseInterface.Log.LogType.DELETE
            , DatabaseInterface.Log.Log_Target.Item_Equivalence_Relation
            , ""
          , true, "");
                    return true;
                }
                catch (Exception ee)
                {
                    DB.AddLog(
                      DatabaseInterface.Log.LogType.DELETE
                   , DatabaseInterface.Log.Log_Target.Item_Equivalence_Relation
                      , ""
                    , false, ee.Message);
                    MessageBox.Show("Delete_Item_Equivalence_Relation_For_Group:" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

        }
    }
}
