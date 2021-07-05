


using OverLoad_Server_Kernal.Objects;
using OverLoad_Server_Kernal.TradeSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OverLoad_Server_Kernal
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

            internal List<Folder> Get_User_Allowed_Folders(uint userid)
            {
                try
                {
                    List<Folder> list = new List<Objects.Folder>();

                    List<DatabaseInterface.AccessFolderPremession> AccessFolderPremession_List = DB.Get_AccessFolderPremession_List(userid);
                    
                    List<Folder> TempFolders = AccessFolderPremession_List.Select(x => x._Folder).ToList();

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

                    return list;

                }
                catch (Exception ee)
                {
                    throw new Exception("Get_User_Allowed_Folders:" + ee.Message);

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
                   throw new Exception("فشل الاتصال بقاعدة البيانات");
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
                    throw new Exception("GetFolderChilds:" + ee.Message);

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
                    throw new Exception("Get_Folder_Tree:" + ee.Message);

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
                    throw new Exception("SearchFolder:" + ee.Message);
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
                    throw new Exception("GetFolderInfoByID:" + ee.Message);
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
                    throw new Exception("SearchItem:" + ee.Message);
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
                    throw new Exception("SearchItemINFolder:" + ee.Message);
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
                   throw new Exception("جلب بيانات العنصر:"+ee.Message);
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
                   throw new Exception("جلب بيانات العنصر:" + ee.Message);
                    return null;
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
                   throw new Exception("GetAllItemsNameList:" + ee.Message);
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
                   throw new Exception("GetAllCompaniesNameList:" + ee.Message);
                    return list;
                }
            }

            internal List<Item> Get_ALL_Item_List(uint UserID)
            {
                try
                {
                    List<Item> list = new List<Item>();


                    List<Folder> foldersList = new FolderSQL(DB).Get_User_Allowed_Folders(UserID);
                    bool Is_Belong_To_Admin_Group = DB.IS_Belong_To_Admin_Group(UserID);
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
                        if (!Is_Belong_To_Admin_Group) if (foldersList.Where(x => x.FolderID == folderid).ToList().Count == 0) continue;
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
                    throw new Exception("Get_ALL_Item_List:" + ee.Message);
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
                   throw new Exception("GetItemFileList:"+ee.Message );

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
                   throw new Exception("فشل جلب بيانات الخاصية"+ee.Message );
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
                   throw new Exception("فشل جلب خاصيات المجلد" + ee.Message);
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
                   throw new Exception("GetItemSpec_Restrict_Options_List:"+ee.Message  );
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
                   throw new Exception("   فشل جلب قيم العنصر للخاصية" + ee.Message);
                    return null;
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
                   throw new Exception("فشل جلب بيانات الخاصية" + ee.Message);
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
                   throw new Exception("   فشل جلب خاصيات المجلد النصية" + ee.Message);
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
                catch(System.Data.SqlClient.SqlException sqlEx)
                {
                    throw new Exception("حدث خطأ اثناء الاتصال بقاعدة البيانات", sqlEx);
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
                   throw new Exception(ee.Message);
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
                   throw new Exception("GetConsumeAmountinfo:" + ee.Message);
                    return null;
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
                   throw new Exception("فشل جلب قائمة وحدات التوزيع:"+ ee.Message);
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
                   throw new Exception("IsConsumeUnitBelongToItem:"+ee.Message);
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
                   throw new Exception(ee.Message);return null;
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
                   throw new Exception("فشل جلب اسعار العنصر:"+ ee.Message);
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
                   throw new Exception("فشل جلب اسعار العنصر:"+ ee.Message);
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
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    throw new Exception("حدث خطأ اثناء الاتصال بقاعدة البيانات", sqlEx);
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
                   throw new Exception("GetEquivalence_GroupList:"+ ee.Message );
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
                   throw new Exception(" Get_Item_Equivalence_Relation_By_Group" + ee.Message );
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
                   throw new Exception(" Get_Item_Equivalence_Relation_By_Group" + ee.Message);
                    return null;
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
                   throw new Exception("IS_Exists_Item_Equivalence_Relation:" + ee.Message );
                    return false;
                }
            }

        }
    }
}
