
using System;
using System.Collections.Generic;


namespace OverLoad_Server_Kernal
{
    namespace  Objects
    {
        public class Folder
        {
            public uint FolderID;
            public string FolderName;
            public uint? ParentFolderID;
            public DateTime CreateDate;
            public string DefaultConsumeUnit;
            public Folder(uint fid, string fname, uint? parentfid, DateTime d, string DefaultConsumeUnit_)
            {
                FolderID = fid;
                FolderName = fname;
                ParentFolderID = parentfid;
                CreateDate = d;
                DefaultConsumeUnit = DefaultConsumeUnit_;
            }

        }
        public class AvailableItem
        {

            internal Item _Item;
            internal string AvailableAmount;
            internal string FolderPath;
            public AvailableItem(Item Item_, string AvailableAmount_, string FolderPath_)
            {
                _Item  = Item_;
                AvailableAmount = AvailableAmount_;
                FolderPath = FolderPath_;
            }
        }
    
        public class Item
        {

            internal uint ItemID;
            internal string ItemName;
            internal string ItemCompany;
            internal Folder folder;
            internal string MarketCode;
            public DateTime CreateDate;
            public string DefaultConsumeUnit;
            //public byte[] ItemImage;

            public Item(Folder f, uint id, string name, string company, string market,DateTime t,string DefaultConsumeUnit_)
            {
                folder = f;
                ItemID = id;
                ItemName = name;
                ItemCompany = company;
                MarketCode = market;
                CreateDate = t;
                DefaultConsumeUnit = DefaultConsumeUnit_;

            }
            public string GetItemFullName()
            {
                return folder .FolderName +":"+ " [" + ItemCompany + "]-[" + ItemName + "]";
            }
        }
        public class ItemFile
        {
            internal Item Item_;
            internal UInt32  FileID;
            internal string FileName;
            internal string FileDescription;
            internal long FileSize;
            internal DateTime AddDate;
            public ItemFile (Item item__,UInt32 FileID_,string FileName_,string FileDescription_,long filesize,DateTime addate )
            {
                Item_ = item__;
                FileID = FileID_;
                FileName = FileName_;
                FileDescription =  FileDescription_;
                FileSize = filesize;
                AddDate = addate;
            }

        }


        public class ItemSpec_Restrict
        {
            internal Folder folder;
            internal uint SpecID;
            internal string SpecName;
            internal uint SpecIndex;
            public ItemSpec_Restrict(Folder folder_,uint SpecID_, string SpecName_,uint SpecIndex_)
            {
                folder = folder_;
                SpecID = SpecID_;
                SpecName = SpecName_;
                SpecIndex = SpecIndex_;
            }
        }

        public class ItemSpec_Restrict_Options
        {
            internal ItemSpec_Restrict ItemSpecRestrict_;
            internal string OptionName;
            internal uint OptionID;

            public ItemSpec_Restrict_Options(ItemSpec_Restrict ItemSpecRestrict__, uint OptionID_, string OptionName_)
            {
                ItemSpecRestrict_ = ItemSpecRestrict__;
                OptionID = OptionID_;
                OptionName = OptionName_;
            }
        }
        public class ItemSpec_Restrict_Value
        {
            internal Item item;
            internal ItemSpec_Restrict ItemSpecRestrict_;
            internal  ItemSpec_Restrict_Options ItemSpec_Restrict_Options_;
            public ItemSpec_Restrict_Value(Item item_, ItemSpec_Restrict ItemSpecRestrict__, ItemSpec_Restrict_Options ItemSpec_Restrict_Options__)
            {
                item = item_;
                ItemSpecRestrict_ = ItemSpecRestrict__;
                ItemSpec_Restrict_Options_ = ItemSpec_Restrict_Options__;
            }
        }
        public class ItemSpecDisplay
        {
            internal Folder folder;
            internal uint SpecID;
            internal string SpecName;
            internal uint SpecIndex;
            internal bool Spectype;
            public ItemSpecDisplay(Folder folder_, uint SpecID_, string SpecName_, uint SpecIndex_,bool SpecType_)
            {
                folder = folder_;
                SpecID = SpecID_;
                SpecName = SpecName_;
                SpecIndex = SpecIndex_;
                Spectype = SpecType_;
            }
        }
        public class ItemSpec
        {
            internal Folder folder;
            internal uint SpecID;
            internal string SpecName;
            internal uint SpecIndex;
            public ItemSpec(Folder folder_, uint SpecID_, string SpecName_, uint SpecIndex_)
            {
                folder = folder_;
                SpecID = SpecID_;
                SpecName = SpecName_;
                SpecIndex = SpecIndex_;
            }
        }
        public class ItemSpec_Value
        {
            internal Item Item_;
            internal ItemSpec ItemSpec_;
            internal string Value;
            public ItemSpec_Value(Item Item__, ItemSpec ItemSpec__, string Value_)
            {
                Item_ = Item__;
                ItemSpec_ = ItemSpec__;
                Value = Value_;
            }
        }
        
        public class ItemRelation
        {
            public Item Item_;
            public Item AnotherItem;
            public uint Relation_;
            public bool Inherit;
            public string Notes;
            public ItemRelation(Item Item__, Item AnotherItem_, uint Relation__,bool Inherit_, string notes)
            {
                Item_ = Item__;
                AnotherItem = AnotherItem_;
                Relation_ = Relation__;
                Inherit = Inherit_;
                Notes = notes;
            }
        }
        public class Relation
        {
            //public const uint ITEM_EQUAL = 0;
            public const uint ITEM_CONTAIN = 1;
            public const uint ITEM_FOUNDIN = 2;
            public readonly  uint Value;
            public readonly string  Name;
            private Relation(uint Value_,string Name_)
            {
                Value = Value_;
                Name = Name_;
            }
            public static  List <Relation> GetRealtions()
            {
                List<Relation> RelationList = new List<Relation>();
                //RelationList.Add(new Relation(Relation.ITEM_EQUAL, "مكافىء"));
                RelationList.Add(new Relation(Relation.ITEM_CONTAIN, "يحتوي"));
                RelationList.Add(new Relation(Relation.ITEM_FOUNDIN, "موجود في"));
                return RelationList;
            }
            public static Relation  GetRealtionByValue(uint value)
            {
                switch (value )
                {
                    //case ITEM_EQUAL:
                    //    return new Relation(ITEM_EQUAL, "مكافىء"); 
                    case ITEM_CONTAIN:
                        return new Relation(ITEM_CONTAIN, "يحوي");
                    case ITEM_FOUNDIN:
                        return new Relation(ITEM_FOUNDIN, "موجود في");
                    default:
                        return null;
                }
            }
   
        }

        public class Equivalence_Group
        {
            public uint GroupID;
            public string GroupName;
            public Equivalence_Group(uint GroupID_,
             string GroupName_)
            {
                GroupID = GroupID_;
                GroupName = GroupName_;
            }
        }
        public class Item_Equivalence_Relation
        {
            public Equivalence_Group _Equivalence_Group;
            public Item _Item;
            public Item_Equivalence_Relation(Equivalence_Group Equivalence_Group_,
             Item Item_)
            {
                _Equivalence_Group = Equivalence_Group_;
                _Item = Item_;
            }
        }
        public class ConsumeUnit
        {

            public uint ConsumeUnitID;
            public string ConsumeUnitName;
            public Item Item_;
            public double Factor;
            public ConsumeUnit(uint ConsumeUnitID_, string ConsumeUnitName_, Item Item__, double Factor_)
            {
                ConsumeUnitID = ConsumeUnitID_;
                ConsumeUnitName = ConsumeUnitName_;
                Item_ = Item__;
                Factor = Factor_;
            }

        }
     
        public class ItemSellPrice
        {

 
            public Item Item_;
            public TradeState _TradeState;
            public ConsumeUnit ConsumeUnit_;
            public SellType SellType_;
            public double Price;
            public ItemSellPrice( Item Item__, TradeState TradeState_, ConsumeUnit ConsumeUnit__, SellType SellType__, double Price_)
            {
 
                Item_ = Item__;
                _TradeState = TradeState_;
                ConsumeUnit_ = ConsumeUnit__;
                SellType_ = SellType__;
                Price = Price_;
            }

        }


    }
}
