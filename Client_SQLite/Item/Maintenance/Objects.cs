using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Trade.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Maintenance
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
                ClauseType = AdditionalClause_TYPE ;
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
            public static void RefreshSubDiagnosticOPRList(ref System.Windows.Forms.ListView ListView_, List<DiagnosticOPRReport> SubDiagnosticOPRReportList_)
            {

                ListView_.Items.Clear();
                for (int i = 0; i < SubDiagnosticOPRReportList_.Count; i++)
                {
                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
                    ListViewItem_.Name = SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.DiagnosticOPRDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Desc);
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Location);
                    if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item == null)
                    {
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");

                    }
                    else
                    {
                        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.folder.FolderName);
                        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemName);
                        ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR._Item.ItemCompany);

                    }
                    if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == null)
                    {
                        ListViewItem_.SubItems.Add("غير معروف");
                        ListViewItem_.BackColor = System.Drawing.Color.LightYellow;
                    }
                    else if (SubDiagnosticOPRReportList_[i]._DiagnosticOPR.Normal == true)
                    {
                        ListViewItem_.SubItems.Add("لا يوجد عطل");
                        ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    }
                    else
                    {
                        ListViewItem_.SubItems.Add(" يوجد عطل");
                        ListViewItem_.BackColor = System.Drawing.Color.Orange;
                    }
                    //ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i]._DiagnosticOPR .Report );
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].MeasureOPR_Count.ToString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Files_Count.ToString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].SubDiagnosticOPR_Count.ToString());
                    ListViewItem_.SubItems.Add(SubDiagnosticOPRReportList_[i].Tags_Count.ToString());

                    ListView_.Items.Add(ListViewItem_);

                }


            }
            public static void InitializeDiagnosticOPRListViewColumns(ref System.Windows.Forms.ListView ListView_)
            {
                try
                {
                    ListView_.Name = "DiagnosticOPRListView";
                    ListView_.Columns.Clear();
                    ListView_.Columns.Add("الرقم");
                    ListView_.Columns.Add("التاريخ");
                    ListView_.Columns.Add("الوصف");
                    ListView_.Columns.Add("الموقع");
                    ListView_.Columns.Add("الصنف");
                    ListView_.Columns.Add("الموديل");
                    ListView_.Columns.Add("االشركة");
                    ListView_.Columns.Add("النتيجة");
                    ListView_.Columns.Add("عمليات القياس");
                    ListView_.Columns.Add("الملفات");
                    ListView_.Columns.Add(" الفرعية");
                    ListView_.Columns.Add("عمليات الربط");
                    DiagnosticOPRReport.AdjustlistViewDiagnosticOPRColumnsWidth(ref ListView_);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("InitializeDiagnosticOPRListViewColumns" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            public static void AdjustlistViewDiagnosticOPRColumnsWidth(ref System.Windows.Forms.ListView ListView_)
            {
                ListView_.Columns[0].Width = 60;
                ListView_.Columns[1].Width = 100;
                ListView_.Columns[2].Width = ListView_.Width - 1010;

                ListView_.Columns[3].Width = 60;
                ListView_.Columns[4].Width = 80;
                ListView_.Columns[5].Width = 105;
                ListView_.Columns[6].Width = 95;
                ListView_.Columns[7].Width = 100;
                ListView_.Columns[8].Width = 120;
                ListView_.Columns[9].Width = 80;
                ListView_.Columns[10].Width = 100;
                ListView_.Columns[11].Width = 100;


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
            internal static void RefreshFaultReportList(ref ListView ListView_, List<MaintenanceFaultReport> MaintenanceFaultReportList)
            {
                ListView_.Items.Clear();
                for (int i = 0; i < MaintenanceFaultReportList.Count; i++)
                {

                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(MaintenanceFaultReportList[i].Fault.FaultID.ToString());
                    ListViewItem_.Name = MaintenanceFaultReportList[i].Fault.FaultID.ToString();
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault.FaultDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault.FaultDesc);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault._Item.ItemName);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Fault._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].MaintenanceFault_Affictive_RepairOPRList .Count .ToString());
                    ListViewItem_.SubItems.Add(MaintenanceFaultReportList[i].Tags_Count.ToString());
                    if (MaintenanceFaultReportList[i].MaintenanceFault_Affictive_RepairOPRList.Count == 0)
                        ListViewItem_.BackColor = System.Drawing.Color.Orange;
                    else
                        ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    ListView_.Items.Add(ListViewItem_);

                }


            }
            public static void InitializeFaultReportListViewColumns(ref System.Windows.Forms.ListView ListView_)
            {
                try
                {
                    ListView_.Name = "DiagnosticOPRListView";
                    ListView_.Columns.Clear();
                    ListView_.Columns.Add("الرقم");
                    ListView_.Columns.Add("التاريخ");
                    ListView_.Columns.Add("وصف العطل");
                    ListView_.Columns.Add("الصنف");
                    ListView_.Columns.Add("الموديل");
                    ListView_.Columns.Add("االشركة");
                    ListView_.Columns.Add("عمليات الاصلاح");
                    ListView_.Columns.Add("تم الاصلاح من خلال");
                    ListView_.Columns.Add("الربط مع عمليات الفحص");
                    MaintenanceFaultReport.AdjustlistViewFaultReportOPRColumnsWidth(ref ListView_);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("InitializeFaultReportListViewColumns" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            public static void AdjustlistViewFaultReportOPRColumnsWidth(ref System.Windows.Forms.ListView ListView_)
            {
                try
                {
                    ListView_.Columns[0].Width = 60;
                    ListView_.Columns[1].Width = 100;
                    ListView_.Columns[2].Width = ListView_.Width - 980;
                    ListView_.Columns[3].Width = 100;
                    ListView_.Columns[4].Width = 100;
                    ListView_.Columns[5].Width = 100;
                    ListView_.Columns[6].Width = 130;
                    ListView_.Columns[7].Width = 190;
                    ListView_.Columns[8].Width = 190;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("AdjustlistViewFaultReportOPRColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
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
            public static void RefreshRepairOPRList(ref System.Windows.Forms.ListView ListView_, List<RepairOPR> RepairOPRtList)
            {


                ListView_.Items.Clear();
                for (int i = 0; i < RepairOPRtList.Count; i++)
                {

                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(RepairOPRtList[i]._Operation.OperationID.ToString());
                    ListViewItem_.Name = RepairOPRtList[i]._Operation.OperationID.ToString();
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].RepairOPRDate.ToShortDateString());
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].RepairDesc);
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].RepairReport);
                    //if (RepairOPRtList[i].FaultRepair == true)
                    //{
                    //    ListViewItem_.SubItems.Add("عملية اصلاح فعالة");
                    //    ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    //}
                    //else
                    //{
                    //    ListViewItem_.SubItems.Add("-");
                    //    ListViewItem_.BackColor = System.Drawing.Color.Orange;
                    //}
                 
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].InstalledItem_Count.ToString());
                    ListViewItem_.SubItems.Add(RepairOPRtList[i].TestInstallOPR_Count.ToString());
                    ListViewItem_.BackColor = System.Drawing.Color.LimeGreen;
                    ListView_.Items.Add(ListViewItem_);

                }
            }
            public static void InitializeRepairOPRListViewColumns(ref System.Windows.Forms.ListView ListView_)
            {
                try
                {
                    ListView_.Name = "RepairOPRListView";
                    ListView_.Columns.Clear();
                    ListView_.Columns.Add("الرقم");
                    ListView_.Columns.Add("التاريخ");
                    ListView_.Columns.Add("الوصف");
                    ListView_.Columns.Add("التقرير");
                    ListView_.Columns.Add("العناصر المركبة");
                    ListView_.Columns.Add("اختبار تركيب العناصر");
                    RepairOPR .AdjustlistViewRepairOPRColumnsWidth(ref ListView_);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("InitializeRepairOPRListViewColumns" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            public static void AdjustlistViewRepairOPRColumnsWidth(ref System.Windows.Forms.ListView ListView_)
            {
                ListView_.Columns[0].Width = 60;
                ListView_.Columns[1].Width = 100;
                ListView_.Columns[2].Width = 200;

                ListView_.Columns[3].Width = ListView_.Width -600;
                ListView_.Columns[4].Width = 120;
                ListView_.Columns[5].Width = 120;
               


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
            internal static void RefreshMissed_FaultList(ref ListView ListView_, List<Missed_Fault_Item> Missed_Fault_ItemList)
            {
                ListView_.Items.Clear();
                for (int i = 0; i < Missed_Fault_ItemList.Count; i++)
                {

                    System.Windows.Forms.ListViewItem ListViewItem_ = new System.Windows.Forms.ListViewItem(Missed_Fault_ItemList[i].ID.ToString());
                    ListViewItem_.Name = Missed_Fault_ItemList[i].ID.ToString();
                    if (Missed_Fault_ItemList[i].Type == Missed_Fault_Item.FAULT_ITEM)
                    {
                        ListViewItem_.SubItems.Add("تالف");
                        ListViewItem_.BackColor = System.Drawing.Color.SandyBrown;
                    }
                    else
                    {
                        ListViewItem_.SubItems.Add("مفقود");
                        ListViewItem_.BackColor = System.Drawing.Color.PeachPuff;
                    }
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i]._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i]._Item.ItemName);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i]._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i].Location);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i].Notes);
                    ListViewItem_.SubItems.Add(Missed_Fault_ItemList[i].TagsCount.ToString());

                    ListView_.Items.Add(ListViewItem_);

                }


            }
            public static void InitializeMissed_Fault_ListViewColumns(ref System.Windows.Forms.ListView ListView_)
            {
                try
                {
                    ListView_.Name = "DiagnosticOPRListView";
                    ListView_.Columns.Clear();
                    ListView_.Columns.Add("المعرف");
                    ListView_.Columns.Add("النمط");
                    ListView_.Columns.Add("الصنف");
                    ListView_.Columns.Add("الموديل");
                    ListView_.Columns.Add("االشركة");
                    ListView_.Columns.Add("الموقع");
                    ListView_.Columns.Add("ملاحظات");
                    ListView_.Columns.Add("عمليات الربط");
                    Missed_Fault_Item.AdjustlistViewMissed_Fault_ColumnsWidth(ref ListView_);
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("InitializeMissed_Fault_ListViewColumns" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            public static void AdjustlistViewMissed_Fault_ColumnsWidth(ref System.Windows.Forms.ListView ListView_)
            {
                try
                {
                    ListView_.Columns[0].Width = 60;
                    ListView_.Columns[1].Width = 80;
                    ListView_.Columns[2].Width = 125;
                    ListView_.Columns[3].Width = 125;
                    ListView_.Columns[4].Width = 125;
                    ListView_.Columns[5].Width = 125;
                    ListView_.Columns[6].Width = ListView_.Width - 880;
                    ListView_.Columns[7].Width = 230;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("AdjustlistViewMissed_Fault_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
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
            public static void AdjustlistView_ColumnsWidth(ref System.Windows.Forms.ListView ListView_)
            {
                try
                {
                    ListView_.Columns[0].Width = 100;
                    ListView_.Columns[1].Width = 80;
                    ListView_.Columns[2].Width = 150;
                    ListView_.Columns[3].Width = ListView_.Width - 350;

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("AdjustlistViewMissed_Fault_ColumnsWidth" + Environment.NewLine + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
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
