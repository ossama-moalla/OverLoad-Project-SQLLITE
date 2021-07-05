
using System;
using System.Collections.Generic;



namespace OverLoad_Server_Kernal
{
    namespace  Objects
    {
      
        public class BillMaintenance:Bill
        {
            public SellType _SellType { get; }
            public MaintenanceOPR _MaintenanceOPR { get; }
            public BillMaintenance(MaintenanceOPR MaintenanceOPR_,uint BillID_, DateTime BillDate_, SellType SellType_, Currency Currency_, double ExchangeRate_, double Discount_,string Notes_)
            {
                _MaintenanceOPR = MaintenanceOPR_;
                _Operation = new Operation(Operation.BILL_MAINTENANCE, BillID_);
                BillDate = BillDate_;
                _SellType = SellType_;
                BillDescription = "";
                _Contact = _MaintenanceOPR._Contact;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Discount = Discount_;
                Notes = Notes_;
            }
        }
        public class BillMaintenance_Clause
        {
            public  const uint ITEMOUT_TYPE = 1;
            public const uint REPAIR_OPR_TYPE = 2;
            public const uint DIAGNOSTIC_OPR_TYPE = 3;
            public const uint AdditionalClause_TYPE = 4;
            public readonly uint ClauseType;
            public uint BillID { get; }
            public RepairOPR _RepairOPR { get; }
            public ItemOUT _ItemOUT { get; }
            public BillAdditionalClause _BillAdditionalClause { get; }
            public DiagnosticOPR _DiagnosticOPR { get; }
            public double? Value { get; }
            public BillMaintenance_Clause(uint BillID_,ItemOUT ItemOUT_)
            {
                ClauseType = ITEMOUT_TYPE;
                BillID = BillID_;
                _ItemOUT  = ItemOUT_;
                Value = _ItemOUT._OUTValue.Value;
            }
            public BillMaintenance_Clause(uint BillID_, BillAdditionalClause BillAdditionalClause_)
            {
                ClauseType = ITEMOUT_TYPE;
                BillID = BillID_;
                _BillAdditionalClause   = BillAdditionalClause_;
                Value = _BillAdditionalClause.Value;
            }
            public BillMaintenance_Clause(uint BillID_, RepairOPR RepairOPR_ ,double? Value_)
            {
                ClauseType = REPAIR_OPR_TYPE ;
                BillID = BillID_;
                _RepairOPR  = RepairOPR_;
                Value = Value_;
            }
            public BillMaintenance_Clause(uint BillID_, DiagnosticOPR DiagnosticOPR_, double? Value_)
            {
                ClauseType = DIAGNOSTIC_OPR_TYPE ;
                BillID = BillID_;
                _DiagnosticOPR = DiagnosticOPR_;
                Value = Value_;
            }
        }
        //public class BillMaintenance_RepairOPR_Clause
        //{
        //    public uint BillID;
        //    public RepairOPR _RepairOPR;
        //    public double? Value;
        //    public BillMaintenance_RepairOPR_Clause(  uint BillID_,
        //     RepairOPR RepairOPR_,
        //     double? Value_)
        //    {
        //           BillID= BillID_;
        //      _RepairOPR= RepairOPR_;
        //     Value= Value_;
        //    }
        //}
        
        //public class BillMaintenance_DiagnosticOPR_Clause
        //{
        //    public uint BillID;
        //    public DiagnosticOPR _DiagnosticOPR;
        //    public double? Value;
        //    public BillMaintenance_DiagnosticOPR_Clause(uint BillID_,
        //    DiagnosticOPR DiagnosticOPR_,
        //    double? Value_)
        //    {
        //        BillID = BillID_;
        //        _DiagnosticOPR = DiagnosticOPR_;
        //        Value = Value_;
        //    }
        //}
        public class MaintenanceOPR_EndWork
        {
            public uint MaintenanceOPRID { get; }
            public DateTime EndWorkDate { get; }
            public bool Repaired { get; }
            public string Report { get; }
            public DateTime? DeliveredDate { get; }
            public DateTime? EndwarrantyDate { get; }

            public MaintenanceOPR_EndWork(uint MaintenanceOPRID_,
             DateTime EndWorkDate_,
             bool Repaired_,
             string Report_,
             DateTime? DeliveredDate_,
             DateTime? EndwarrantyDate_
             )
            {
             MaintenanceOPRID= MaintenanceOPRID_;
             EndWorkDate= EndWorkDate_;
             Repaired = Repaired_;
              DeliveredDate = DeliveredDate_;
                EndwarrantyDate = EndwarrantyDate_;
             Report = Report_;
        }
 
        }

        public class MaintenanceOPR
        {

            /// <summary>
            /// ////
            /// </summary>
            public Operation _Operation { get; }
            public DateTime EntryDate { get; }
            public Contact _Contact { get; }
            public Item _Item { get; }
            public string ItemSerial { get; }
            public string  FaultDesc { get; }
            public TradeStorePlace Place { get; }
            public MaintenanceOPR_EndWork _MaintenanceOPR_EndWork { get; set; }
            public string Notes { get; }
            public MaintenanceOPR(uint MaintenanceOPRID_
                , DateTime EntryDate_
                 , Contact Contact_
                , Item Item_
                , string ItemSerial_
                , string FaultDesc_
                , TradeStorePlace Place_
                , MaintenanceOPR_EndWork MaintenanceOPR_EndWork_
                , string Notes_)
                {
                    _Operation =new Operation (Operation.MAINTENANCE_OPR, MaintenanceOPRID_);
                EntryDate = EntryDate_;
                _Contact = Contact_;
                    _Item= Item_;
                    ItemSerial= ItemSerial_;
                    FaultDesc = FaultDesc_; ;
                    Place = Place_;
                    _MaintenanceOPR_EndWork = MaintenanceOPR_EndWork_;
                    Notes = Notes_;

                }
        }
        //public class MaintenanceOPR_Report
        //{
        //    public MaintenanceOPR _MaintenanceOPR { get; }
        //    public BillMaintenance _BillMaintenance { get; }
        //    public MaintenanceOPR_Report(  MaintenanceOPR MaintenanceOPR_,
        //     BillMaintenance BillMaintenance_)
        //    {
        //        _MaintenanceOPR = MaintenanceOPR_;
        //        _BillMaintenance = BillMaintenance_;
        //    }
        //}
        public class MaintenanceOPR_Accessory
        {
            public uint AccessoryID;
            public MaintenanceOPR _MaintenanceOPR;
            public Item _Item;
            public string ItemSerialNumber;
            public string Notes;
            public TradeStorePlace Place;
            public MaintenanceOPR_Accessory(
             uint AccessoryID_,
             MaintenanceOPR MaintenanceOPR_,
             Item Item_,
             string ItemSerialNumber_,
             string Notes_,
             TradeStorePlace Place_)
            {
                AccessoryID = AccessoryID_;
                _MaintenanceOPR = MaintenanceOPR_;
                _Item = Item_;
                ItemSerialNumber = ItemSerialNumber_;
                Notes = Notes_;
                Place = Place_;
            }
        }
        public class DiagnosticOPR
        {
            public MaintenanceOPR _MaintenanceOPR;
            public uint? ParentDiagnosticOPRID;
            public uint DiagnosticOPRID;
            public DateTime DiagnosticOPRDate;
            public Item _Item;
            public string Desc;
            public string Location;
            public bool? Normal;
            public string Report;
            public DiagnosticOPR(
                MaintenanceOPR MaintenanceOPR_,
                uint? ParentDiagnosticOPRID_,
                uint DiagnosticOPRID_,
                DateTime DiagnosticOPRDate_,
                 Item Item_,
                string Desc_,
                 string Location_,
                 bool? Normal_,
             string Report_)
            {
                _MaintenanceOPR = MaintenanceOPR_;
                ParentDiagnosticOPRID = ParentDiagnosticOPRID_;
                DiagnosticOPRID = DiagnosticOPRID_;
                DiagnosticOPRDate = DiagnosticOPRDate_;
                _Item = Item_;
                Desc = Desc_;
                Location = Location_;
                Normal = Normal_;
                Report = Report_;
            }

        }
        public class DiagnosticOPRReport
        {
            public DiagnosticOPR _DiagnosticOPR;
            public int  MeasureOPR_Count;
            public int Files_Count;
            public int  SubDiagnosticOPR_Count;
            public int Tags_Count;
            public DiagnosticOPRReport(  DiagnosticOPR DiagnosticOPR_,
             int MeasureOPR_Count_,
             int Files_Count_,
             int SubDiagnosticOPR_Count_ ,
             int Tags_Count_)
            {
                _DiagnosticOPR = DiagnosticOPR_;
                MeasureOPR_Count = MeasureOPR_Count_;
                Files_Count = Files_Count_;
                SubDiagnosticOPR_Count = SubDiagnosticOPR_Count_;
                Tags_Count = Tags_Count_;
            }
       
        }
        public class MeasureOPR
        {
            public DiagnosticOPR _DiagnosticOPR;
            public uint MeasureID;
            public string Desc;
            public string  Value;
            public string MeasureUnit;
            public bool? Normal;
            public MeasureOPR(
                  DiagnosticOPR DiagnosticOPR_,
             uint MeasureID_,
             string Desc_,
             string  Value_,
             string MeasureUnit_,
                bool? Normal_)
            {
                _DiagnosticOPR = DiagnosticOPR_;
                MeasureID = MeasureID_;
                Desc = Desc_;
                Value = Value_;
                MeasureUnit = MeasureUnit_;
                Normal = Normal_;
            }

        }
        public class DiagnosticFile
        {
            internal DiagnosticOPR _DiagnosticOPR;
            internal UInt32 FileID;
            internal string FileName;
            internal string FileDescription;
            internal long FileSize;
            internal DateTime AddDate;
            public DiagnosticFile(DiagnosticOPR DiagnosticOPR_, UInt32 FileID_, string FileName_, string FileDescription_, long filesize, DateTime addate)
            {
                _DiagnosticOPR = DiagnosticOPR_;
                FileID = FileID_;
                FileName = FileName_;
                FileDescription = FileDescription_;
                FileSize = filesize;
                AddDate = addate;
            }

        }
      
        public class MaintenanceFault
        {
            public MaintenanceOPR _MaintenanceOPR;
            public Item _Item;
            public uint FaultID;
            public DateTime FaultDate;
            public string FaultDesc;
            public string FaultReport;
            public MaintenanceFault(MaintenanceOPR MaintenanceOPR_, Item Item_,
               uint FaultID_, DateTime FaultDate_ ,string FaultDesc_,string FaultReport_)
            {
                _MaintenanceOPR = MaintenanceOPR_;
                _Item = Item_;
                FaultID = FaultID_;
                FaultDate = FaultDate_;
                FaultDesc = FaultDesc_;
                FaultReport = FaultReport_;
            }


        }

        public class MaintenanceFaultReport
        {
            public MaintenanceFault Fault { get; }
            public List<RepairOPR> MaintenanceFault_Affictive_RepairOPRList { get; }
            public int Tags_Count { get; }
            public MaintenanceFaultReport(MaintenanceFault Fault_, List<RepairOPR> MaintenanceFault_Affictive_RepairOPRList_, int Tags_Count_)
            {
                Fault = Fault_;
                MaintenanceFault_Affictive_RepairOPRList = MaintenanceFault_Affictive_RepairOPRList_;
                Tags_Count = Tags_Count_;
            }

        }
        public class RepairOPR
        {

            public Operation _Operation { get; }
            public MaintenanceOPR _MaintenanceOPR { get; }
            public DateTime RepairOPRDate { get; }
            public string RepairDesc { get; }
            public string RepairReport { get; }
            public int InstalledItem_Count { get; }
            public int TestInstallOPR_Count { get; }
            public RepairOPR(  uint  RepairOPRID,
             MaintenanceOPR MaintenanceOPR_,
             DateTime RepairOPRDate_,
             string RepairDesc_,
             string RepairReport_,
             int InstalledItem_Count_,
             int TestInstallOPR_Count_
             )
            {
                _Operation = new Operation(Operation.REPAIROPR, RepairOPRID);
                _MaintenanceOPR = MaintenanceOPR_;
                RepairOPRDate = RepairOPRDate_;
                RepairDesc = RepairDesc_;
                RepairReport = RepairReport_;
                TestInstallOPR_Count = TestInstallOPR_Count_;
                TestInstallOPR_Count = TestInstallOPR_Count_;
            }
 
        }
  
        public class Missed_Fault_Item
        {
            public const bool MISSED_ITEM = true;
            public const  bool FAULT_ITEM = false;

            public uint ID { get; }
            public bool Type { get; }
            public DiagnosticOPR _DiagnosticOPR { get; }
            public Item _Item { get; }
            public string Location { get; }
            public string Notes { get; }
            public int TagsCount { get; }
            public Missed_Fault_Item(  uint ID_,
             bool Type_,
             DiagnosticOPR DiagnosticOPR_,
             Item Item_,
             string Location_,
             string Notes_,
              int TagsCount_)
             {
                ID = ID_;
                Type = Type_;
                _DiagnosticOPR = DiagnosticOPR_;
                _Item = Item_;
                Location = Location_;
                Notes = Notes_;
                TagsCount = TagsCount_;

             }
            public string GetDesc()
            {
                if (Type == Missed_Fault_Item.MISSED_ITEM) return "عنصر مفقود ";
                else return "عنصر تالف ";
            }
  }
        public class MaintenanceTag
        {


            public uint TagID { get; }
            public MaintenanceFault _MaintenanceFault { get; }
            public DiagnosticOPR _DiagnosticOPR { get; }
            public Missed_Fault_Item _Missed_Fault_Item { get; }
            public string TagInfo;
            public MaintenanceTag(uint TagID_, string TagInfo_, MaintenanceFault MaintenanceFault_, DiagnosticOPR DiagnosticOPR_, Missed_Fault_Item Missed_Fault_Item_)
            {
                TagID = TagID_;
                TagInfo = TagInfo_;
                _MaintenanceFault = MaintenanceFault_;
                _DiagnosticOPR = DiagnosticOPR_;
                _Missed_Fault_Item = Missed_Fault_Item_;
            }

        }
        public class MaintenanceTagSummary
        {
      
            public uint TagID { get; }
            public uint TagType { get; }
            public uint ID { get; }
            public string Desc { get; }
            public string TagINFO { get; }
            public MaintenanceTagSummary(uint TagID_,
             uint TagType_,
             uint ID_,
             string Desc_,
             string TagINFO_)
            {
                TagID = TagID_;
                TagType = TagType_;
                ID = ID_;
                Desc = Desc_;
                TagINFO = TagINFO_;
            }
        }

     
        ////public class DiagnosticOPR_MissedFaultItem_Tag
        ////{
        ////    public uint TagID;
        ////    public DiagnosticOPR _DiagnosticOPR;
        ////    public Missed_Fault_Item _Missed_Fault_Item;
        ////    public string TagInfo;
        ////    public DiagnosticOPR_MissedFaultItem_Tag(uint TagID_, DiagnosticOPR DiagnosticOPR_, Missed_Fault_Item Missed_Fault_Item_, string TagInfo_)
        ////    {
        ////        TagID = TagID_;
        ////        _DiagnosticOPR = DiagnosticOPR_;
        ////        _Missed_Fault_Item = Missed_Fault_Item_;
        ////        TagInfo = TagInfo_;
        ////    }
        ////}


    }
}
