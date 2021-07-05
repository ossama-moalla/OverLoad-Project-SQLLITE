using OverLoad_Client.AccountingObj.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverLoad_Client.Reports
{
    namespace   Objects
    {
        public class BillAdditionalClause_Report
        {

            public uint ClauseID { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public BillAdditionalClause_Report(uint ClauseID_
                , string Description_, string Value_)
            {
                ClauseID = ClauseID_;
                Description = Description_;
                Value = Value_;

            }
            public static List<BillAdditionalClause_Report> GetBillAdditionalClause_ReportList(AccountingObj.Objects.Currency Currency_, List<Trade.Objects.BillAdditionalClause> BillAdditionalClauseList)
            {
                List<BillAdditionalClause_Report> list = new List<BillAdditionalClause_Report>();
                try
                {
                    for (int i = 0; i < BillAdditionalClauseList.Count; i++)
                    {
                        list.Add(new BillAdditionalClause_Report(BillAdditionalClauseList[i].ClauseID, BillAdditionalClauseList[i].Description
                            , BillAdditionalClauseList[i].Value + " " + Currency_.CurrencySymbol));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillAdditionalClause_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }
        }
        public class ItemOUT_Report
        {
            public uint ItemOUTID { get; set; }
            public string  ItemType { get; set; }
            public string  ItemName { get; set; }
            public string  ItemCompany { get; set; }
            public string  SingleValue { get; set; }
            public double Amount { get; set; }
            public string  ConsumeUnit { get; set; }
            public string TotlalValue { get; set; }
            public ItemOUT_Report( 
                uint ItemOUTID_,
             string ItemType_,
             string ItemName_,
             string ItemCompany_,
             string SingleValue_,
             double Amount_,
             string  ConsumeUnit_,
             string TotlalValue_)
            {
                ItemOUTID = ItemOUTID_;
             ItemType = ItemType_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                SingleValue = SingleValue_;
                Amount = Amount_;
                ConsumeUnit = ConsumeUnit_;
                TotlalValue = TotlalValue_;

            }
            public static List <ItemOUT_Report > GetItemOUT_ReportList(List <Trade.Objects.ItemOUT> ItemOUTList)
            {
                List < ItemOUT_Report > list= new List<ItemOUT_Report>();
                try
                {
                    for(int i=0;i<ItemOUTList.Count;i++)
                    {
                        list.Add(new ItemOUT_Report(ItemOUTList[i].ItemOUTID , ItemOUTList[i]._ItemIN._Item.folder .FolderName
                            , ItemOUTList[i]._ItemIN ._Item .ItemName, ItemOUTList[i]._ItemIN._Item .ItemCompany, ItemOUTList[i]._OUTValue .Value+" "+ ItemOUTList[i]._OUTValue._Currency.CurrencySymbol, ItemOUTList[i].Amount
                            , ItemOUTList[i]._ConsumeUnit.ConsumeUnitName,((ItemOUTList[i].Amount * ItemOUTList[i]._OUTValue .Value)+" "+ ItemOUTList[i]._OUTValue._Currency.CurrencySymbol )));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemOUT_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }
    }
        public class ItemIN_Report
        {
            public uint ItemINID { get; set; }
            public string ItemType { get; set; }
            public string ItemName { get; set; }
            public string ItemCompany { get; set; }
            public string  SingleValue { get; set; }
            public double Amount { get; set; }
            public string ConsumeUnit { get; set; }
            public string  TotlalValue { get; set; }
            public ItemIN_Report(
                uint ItemINID_,
             string ItemType_,
             string ItemName_,
             string ItemCompany_,
             string  SingleValue_,
             double Amount_,
             string ConsumeUnit_,
             string  TotlalValue_)
            {
                ItemINID = ItemINID_;
                ItemType = ItemType_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                SingleValue = SingleValue_;
                Amount = Amount_;
                ConsumeUnit = ConsumeUnit_;
                TotlalValue = TotlalValue_;

            }
            public static List<ItemIN_Report> GetItemIN_ReportList(List<Trade.Objects.ItemIN> ItemINList)
            {
                List<ItemIN_Report> list = new List<ItemIN_Report>();
                try
                {
                    for (int i = 0; i < ItemINList.Count; i++)
                    {
                        list.Add(new ItemIN_Report(ItemINList[i].ItemINID, ItemINList[i]._Item.folder.FolderName
                            , ItemINList[i]._Item.ItemName, ItemINList[i]._Item.ItemCompany, ItemINList[i]._INCost.Value+" "+ ItemINList[i]._INCost._Currency.CurrencySymbol, ItemINList[i].Amount
                            , ItemINList[i]._ConsumeUnit.ConsumeUnitName, (ItemINList[i].Amount * ItemINList[i]._INCost .Value) + " " + ItemINList[i]._INCost._Currency.CurrencySymbol));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetItemIN_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }
        }
        public class Bill_PayOUT_Report
        {
            public uint PayOprID { get; set; }
            public DateTime PayOprDate { get; set; }
            public string  Value { get; set; }
            public string  Currency { get; set; }
            public double ExchangeRate { get; set; }

            public Bill_PayOUT_Report(uint PayOprID_, DateTime PayOprDate_
                , string  Value_, double ExchangeRate_, string  Currency_
               )
            {
                PayOprID = PayOprID_;
                PayOprDate = PayOprDate_;
                Value = Value_;
                Currency = Currency_;
                ExchangeRate = ExchangeRate_;

            }
            public static List<Bill_PayOUT_Report> GetBill_PayOUT_ReportList(List<AccountingObj.Objects.PayOUT> PayOUTList)
            {
                List<Bill_PayOUT_Report> list = new List<Bill_PayOUT_Report>();
                try
                {
                    for (int i = 0; i < PayOUTList.Count; i++)
                    {
                        list.Add(new Bill_PayOUT_Report(PayOUTList[i].PayOprID, PayOUTList[i].PayOprDate 
                            , PayOUTList[i].Value +" "+PayOUTList[i]._Currency.CurrencySymbol, PayOUTList[i].ExchangeRate , PayOUTList[i]._Currency .CurrencyName ));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBill_PayOUT_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }
        }
        public class Bill_PayIN_Report
        {
            public uint PayOprID { get; set; }
            public DateTime PayOprDate { get; set; }
            public string Value { get; set; }
            public string Currency { get; set; }
            public double ExchangeRate { get; set; }

            public Bill_PayIN_Report(uint PayOprID_, DateTime PayOprDate_
                , string Value_, double ExchangeRate_, string Currency_
               )
            {
                PayOprID = PayOprID_;
                PayOprDate = PayOprDate_;
                Value = Value_;
                Currency = Currency_;
                ExchangeRate = ExchangeRate_;

            }
            public static List<Bill_PayIN_Report> GetBill_PayIN_ReportList(List<AccountingObj.Objects.PayIN> PayINList)
            {
                List<Bill_PayIN_Report> list = new List<Bill_PayIN_Report>();
                try
                {
                    for (int i = 0; i < PayINList.Count; i++)
                    {
                        list.Add(new Bill_PayIN_Report(PayINList[i].PayOprID, PayINList[i].PayOprDate
                            , PayINList[i].Value + " " + PayINList[i]._Currency.CurrencySymbol, PayINList[i].ExchangeRate, PayINList[i]._Currency.CurrencyName));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBill_PayIN_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }
        }
        public class BillMaintenance_RepairClause_Report
        {

            public uint  RepairOPD_ID { get; set; }
            public string RepairOPR_Desc { get; set; }
            public int InstalledItem_Count { get; set; }
            public int TestInstallOPR_Count { get; set; }
            public string  Value { get; set; }
            public BillMaintenance_RepairClause_Report(      uint RepairOPD_ID_,
             string RepairOPR_Desc_,
             int InstalledItem_Count_,
             int TestInstallOPR_Count_,
             string  Value_
             )
            {
                 RepairOPD_ID= RepairOPD_ID_;
                RepairOPR_Desc= RepairOPR_Desc_;
                InstalledItem_Count= InstalledItem_Count_;
                TestInstallOPR_Count= TestInstallOPR_Count_;
                Value= Value_;
            }
            public static List<BillMaintenance_RepairClause_Report> GetBillMaintenance_RepairClause_ReportList(Currency Currency_, List<Maintenance.Objects.BillMaintenance_Clause> BillMaintenance_ClauseList)
            {
                List<BillMaintenance_RepairClause_Report> list = new List<BillMaintenance_RepairClause_Report>();
                try
                {
                    BillMaintenance_ClauseList = BillMaintenance_ClauseList.Where(x => x.Value != null && x.ClauseType == Maintenance.Objects.BillMaintenance_Clause.REPAIR_OPR_TYPE).ToList();
                    for (int i = 0; i < BillMaintenance_ClauseList.Count; i++)
                    {
                        list.Add(new BillMaintenance_RepairClause_Report(BillMaintenance_ClauseList[i]._RepairOPR._Operation.OperationID, 
                            BillMaintenance_ClauseList[i]._RepairOPR .RepairDesc
                            , BillMaintenance_ClauseList[i]._RepairOPR .InstalledItem_Count, BillMaintenance_ClauseList[i]._RepairOPR.TestInstallOPR_Count ,
                           BillMaintenance_ClauseList[i].Value  + " " + Currency_.CurrencySymbol));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillMaintenance_RepairClause_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }


        }
        public class BillMaintenance_DiagnosticOPR_Clause_Report
        {

            public uint DiagnosticOPRID { get; set; }
            public string Desc { get; set; }
            public string ItemType { get; set; }
            public string ItemName { get; set; }
            public string ItemCompany { get; set; }
            public string Location { get; set; }
            public string  Normal { get; set; }
            public string Report { get; set; }
            public string Value { get; set; }

            public BillMaintenance_DiagnosticOPR_Clause_Report(
                    uint DiagnosticOPRID_,
             string Desc_,
             string Location_,
             string  Normal_,
             string ItemType_,
             string ItemName_,
             string ItemCompany_,
             string Report_,
             string Value_)
            {
                DiagnosticOPRID = DiagnosticOPRID_;
              Desc = Desc;
                ItemType = ItemType_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                Location = Location_;
                Normal = Normal_;
                Report = Report_;
                Value = Value_;
            }

            public static List<BillMaintenance_DiagnosticOPR_Clause_Report> GetBillMaintenance_DiagnosticOPR_Clause_ReportList(Currency Currency_, List<Maintenance.Objects.BillMaintenance_Clause> BillMaintenance_ClauseList)
            {
                List<BillMaintenance_DiagnosticOPR_Clause_Report> list = new List<BillMaintenance_DiagnosticOPR_Clause_Report>();
                try
                {
                    BillMaintenance_ClauseList = BillMaintenance_ClauseList.Where(x => x.Value != null && x.ClauseType == Maintenance.Objects.BillMaintenance_Clause.DIAGNOSTIC_OPR_TYPE ).ToList();
                    for (int i = 0; i < BillMaintenance_ClauseList.Count; i++)
                    {
                        string normal = " غير معروف ";
                        string item_type= "",item_company="",item_name="";
                        if(BillMaintenance_ClauseList[i]._DiagnosticOPR ._Item !=null  )
                        {
                            item_type = BillMaintenance_ClauseList[i]._DiagnosticOPR._Item.folder .FolderName;
                            item_company = BillMaintenance_ClauseList[i]._DiagnosticOPR._Item.ItemCompany ;
                            item_name = BillMaintenance_ClauseList[i]._DiagnosticOPR._Item.ItemName;
                        }
                        if (BillMaintenance_ClauseList[i]._DiagnosticOPR.Normal  != null)
                        {
                            normal = BillMaintenance_ClauseList[i]._DiagnosticOPR.Normal == true ? "طبيعي" : "غير طبيعي";
                        }
                            list.Add(new BillMaintenance_DiagnosticOPR_Clause_Report(BillMaintenance_ClauseList[i]._DiagnosticOPR .DiagnosticOPRID,
                            BillMaintenance_ClauseList[i]._DiagnosticOPR.Desc , BillMaintenance_ClauseList[i]._DiagnosticOPR.Location 
                            ,normal ,item_type,item_name,item_company
                            , BillMaintenance_ClauseList[i]._DiagnosticOPR.Report ,
                           BillMaintenance_ClauseList[i].Value + " " + Currency_.CurrencySymbol));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetBillMaintenance_DiagnosticOPR_Clause_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }


        }
        public class MaintenanceFault_Print_Report
        {

            public uint FaultID { get; set; }
            public string FaultDesc { get; set; }
            public string ItemType { get; set; }
            public string ItemName { get; set; }
            public string ItemCompany { get; set; }
            public string FaultReport { get; set; }
            public MaintenanceFault_Print_Report(
               uint FaultID_,  string FaultDesc_,string ItemType_,
             string ItemName_,
             string ItemCompany_, string FaultReport_)
            {

                FaultID = FaultID_;
                FaultDesc = FaultDesc_;
                ItemType = ItemType_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                FaultReport = FaultReport_;
            }
            public static List<MaintenanceFault_Print_Report> GetMaintenanceFault_Print_ReportList(List<Maintenance.Objects.MaintenanceFaultReport> MaintenanceFaultReportList)
            {
                List<MaintenanceFault_Print_Report> list = new List<MaintenanceFault_Print_Report>();
                try
                {
                    for (int i = 0; i < MaintenanceFaultReportList.Count; i++)
                    {

                        string item_type = "", item_company = "", item_name = "";
                        if (MaintenanceFaultReportList[i].Fault._Item != null)
                        {
                            item_type = MaintenanceFaultReportList[i].Fault._Item.folder.FolderName;
                            item_company = MaintenanceFaultReportList[i].Fault._Item.ItemCompany;
                            item_name = MaintenanceFaultReportList[i].Fault._Item.ItemName;
                        }

                        list.Add(new MaintenanceFault_Print_Report(MaintenanceFaultReportList[i].Fault.FaultID ,
                            MaintenanceFaultReportList[i].Fault.FaultDesc
                        , item_type, item_name, item_company, MaintenanceFaultReportList[i].Fault.FaultReport));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetMaintenanceFault_Print_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }

        }
        public class Missed_Fault_Item_Print_Report
        {


            public uint ID { get; set; }
            public string  Type { get; set; }
            public string Location { get; set; }
            public string ItemType { get; set; }
            public string ItemName { get; set; }
            public string ItemCompany { get; set; }
            public Missed_Fault_Item_Print_Report(
                uint ID_,
             string  Type_,
             string Location_,
              string ItemType_,
             string ItemName_,
             string ItemCompany_)
            {
                ID = ID_;
                Type = Type_;    
                Location = Location_;
                ItemType = ItemType_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;

            }
            public static List<Missed_Fault_Item_Print_Report> GetMissed_Fault_Item_Print_ReportList( List<Maintenance.Objects.Missed_Fault_Item > Missed_Fault_ItemList)
            {
                List<Missed_Fault_Item_Print_Report> list = new List<Missed_Fault_Item_Print_Report>();
                try
                {
                     for (int i = 0; i < Missed_Fault_ItemList.Count; i++)
                    {

                        string item_type = "", item_company = "", item_name = "";
                        if (Missed_Fault_ItemList[i]._Item != null)
                        {
                            item_type = Missed_Fault_ItemList[i]._Item.folder.FolderName;
                            item_company = Missed_Fault_ItemList[i]._Item.ItemCompany;
                            item_name = Missed_Fault_ItemList[i]._Item.ItemName;
                        }

                        list.Add(new Missed_Fault_Item_Print_Report(Missed_Fault_ItemList[i].ID ,
                            Missed_Fault_ItemList[i].GetDesc (), Missed_Fault_ItemList[i].Location
                        , item_type, item_name, item_company));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetMissed_Fault_Item_Print_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }

        }
        public class MaintenanceOPR_Accessory_Print_Report
        {

            public string ItemType { get; set; }
            public string ItemName { get; set; }
            public string ItemCompany { get; set; }
            public string ItemSerialNumber { get; set; }
            public MaintenanceOPR_Accessory_Print_Report(

                string ItemType_,
             string ItemName_,
             string ItemCompany_,
             string ItemSerialNumber_)
            {
                ItemType = ItemType_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                ItemSerialNumber = ItemSerialNumber_;

            }
            public static List<MaintenanceOPR_Accessory_Print_Report> GetMaintenanceOPR_Accessory_Print_ReportList(List<Maintenance.Objects.MaintenanceOPR_Accessory > MaintenanceOPR_AccessoryList)
            {
                List<MaintenanceOPR_Accessory_Print_Report> list = new List<MaintenanceOPR_Accessory_Print_Report>();
                try
                {
                    for (int i = 0; i < MaintenanceOPR_AccessoryList.Count; i++)
                    {

                        string item_type = "", item_company = "", item_name = "";
                        if (MaintenanceOPR_AccessoryList[i]._Item != null)
                        {
                            item_type = MaintenanceOPR_AccessoryList[i]._Item.folder.FolderName;
                            item_company = MaintenanceOPR_AccessoryList[i]._Item.ItemCompany;
                            item_name = MaintenanceOPR_AccessoryList[i]._Item.ItemName;
                        }

                        list.Add(
                            new MaintenanceOPR_Accessory_Print_Report( item_type, item_name, item_company, MaintenanceOPR_AccessoryList[i].ItemSerialNumber ));
                    }

                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("GetMaintenanceOPR_Accessory_Print_ReportList:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return list;
            }

        }
    }
}
