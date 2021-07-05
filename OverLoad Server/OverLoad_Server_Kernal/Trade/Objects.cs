
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace OverLoad_Server_Kernal
{
    namespace Objects
    {
        public  class Operation
        {
            public const uint BILL_BUY = 1;
            public const uint BILL_SELL= 2;
            public const uint BILL_MAINTENANCE = 3;
            public const uint Employee_PayOrder = 4;
            public const uint MAINTENANCE_OPR = 5;
            public const uint ASSEMBLAGE = 6;
            public const uint DISASSEMBLAGE = 7;
            public const uint RavageOPR = 8;
            public const uint REPAIROPR = 9;
            /// <summary>
            /// ////////////
            /// </summary>
            public uint OperationType { get; }
            public uint OperationID { get; }
            public Operation (uint OperationType_,uint OperationID_)
            {
                OperationType = OperationType_;
                OperationID = OperationID_;
            }
            public static  string GetOperationName(uint operationtype)
            {
                string operationname = "";
                switch (operationtype)
                {
                    case BILL_BUY:
                        operationname= "فاتور شراء";
                        break;
                    case BILL_SELL :
                        operationname = "فاتور مبيع";
                        break;
                    case BILL_MAINTENANCE:
                        operationname = "فاتور صيانة";
                        break;
                    case Employee_PayOrder:
                        operationname = "امر صرف";
                        break;
                    case MAINTENANCE_OPR:
                        operationname = "عملية صيانة";
                        break;
                    case ASSEMBLAGE:
                        operationname = "عملية تجميع";
                        break;
                    case DISASSEMBLAGE:
                        operationname = "عملية تفكيك";
                        break;
                    case RavageOPR:
                        operationname = "عملية اتلاف";
                        break;
                    case REPAIROPR:
                        operationname = "عملية اصلاح";
                        break;
                }
                return operationname;
            }
            public static string GetOperationItemOutDesc(uint operationtype)
            {

                string operationname = "";
                switch (operationtype)
                {

                    case BILL_SELL:
                        operationname = "اخراج مادة عن طريق فاتورة مبيع";
                        break;

                    case BILL_MAINTENANCE:
                        operationname = "اخراج مادة عن طريق فاتورة صيانة";
                        break;
                    case ASSEMBLAGE:
                        operationname = "ادراج عنصر في عملية تجميع";
                        break;
                    case DISASSEMBLAGE:
                        operationname = "تفكيك عنصر";
                        break;
                    case RavageOPR:
                        operationname = "اتلاف عنصر";
                        break;
                    case REPAIROPR:
                        operationname = "اخراج مادة عن طريق عملية اصلاح";
                        break;
                    default:
                        throw new Exception("عملية غير صحيحة");
                }
                return operationname;
            }
  }
        public class Bill
        {
            public Operation _Operation { set; get; }
            public DateTime BillDate { set; get; }
            public string BillDescription { set; get; }
            public Contact _Contact { set; get; }
            public Currency _Currency { set; get; }
            public double ExchangeRate { set; get; }
            public double Discount { set; get; }
            public string Notes { set; get; }
            //public Bill(uint BillID_, DateTime BillDate_, string BillDescription_,  Contact Contact_, Currency Currency_, double ExchangeRate_, double Discount_, string Notes_)
            //{
            //    BillID = BillID_;
            //    BillDate = BillDate_;
            //    BillDescription = BillDescription_;
            //    _Contact = Contact_;
            //    _Currency = Currency_;
            //    ExchangeRate = ExchangeRate_;
            //    Discount = Discount_;
            //    Notes = Notes_;
            //}
        }
        public class BillAdditionalClause
        {
            public Operation _Operation { get; }
            public uint ClauseID { get; }
            public string Description { get; }
            public double Value { get; }
            public BillAdditionalClause (Operation Operation_,uint ClauseID_
                ,string Description_,double Value_)
            {
                _Operation = Operation_;
                ClauseID = ClauseID_;
                Description = Description_;
                Value = Value_;

            }

        }
        public class BillSell:Bill
        {

            public SellType  _SellType { get; }
            public BillSell(uint BillID_, DateTime BillDate_,string BillDescription_, SellType   SellType_, Contact Contact_, Currency Currency_, double ExchangeRate_, double Discount_,string Notes_)
                //:base (BillID_,BillDate_ ,BillDescription_ ,Contact_,Currency_ ,ExchangeRate_ ,Discount_ ,Notes_ )
            {
                _Operation = new Operation (Operation.BILL_SELL,BillID_ );
                BillDate = BillDate_;
                BillDescription = BillDescription_;
                _SellType = SellType_;
                _Contact = Contact_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Discount = Discount_;
                Notes = Notes_;
            }
        }

        public class BillBuy : Bill
        {
            public BillBuy(uint BillID_, DateTime BillDate_, string BillDescription_, Contact Contact_, Currency Currency_, double ExchangeRate_, double Discount_, string Notes_)

            {
                _Operation = new Operation(Operation.BILL_BUY, BillID_);
                BillDate = BillDate_;
                BillDescription = BillDescription_;
                _Contact = Contact_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
                Discount = Discount_;
                Notes = Notes_;
            }

        }
        public class ItemIN
        {
            public uint ItemINID { get; }
            public DateTime IN_Date { get; }

            public Operation _Operation { get; }
            public Item _Item { get; }
            public TradeState _TradeState { get; }
            public double Amount { get; }
            public ConsumeUnit _ConsumeUnit { get; }
            public INCost _INCost { get; }
            public string Notes { get; }
            public ItemIN(uint ItemINID_, DateTime IN_Date_, Operation Operation_, 
                Item Item_, TradeState TradeState_, double Amount_,
                ConsumeUnit ConsumeUnit_, INCost INCost_, string Notes_)
            {
                if (ConsumeUnit_ == null) throw new Exception("OverLoad_Client.Trade._ItemIN_ConsumeUnit NULL");
                ItemINID = ItemINID_;
                IN_Date = IN_Date_;
                _Operation = Operation_;
                _Item = Item_;
                _TradeState = TradeState_;
                Amount = Amount_;
                _ConsumeUnit = ConsumeUnit_;
                _INCost = INCost_;
                Notes = Notes_;

            }
        }
        public class ItemIN_StoreReport
        {
            public ItemIN _ItemIN{ get; }
            public TradeStorePlace Place { get; }
            public ConsumeUnit _ConsumeUnit { get; }
            public double StoreAmount { get; }
            public double SpentAmount { get; }
            public ItemIN_StoreReport(   ItemIN ItemIN_,
             TradeStorePlace Place_,
             ConsumeUnit ConsumeUnit_,
             double StoreAmount_,
             double SpentAmount_)
            {
                   _ItemIN= ItemIN_;
             Place= Place_;
             _ConsumeUnit= ConsumeUnit_;
             StoreAmount= StoreAmount_;
            SpentAmount= SpentAmount_;
        }
        }
        public class INCost
        {
            public double Value { get; }
            public Currency _Currency { get; }
            public double ExchangeRate { get; }
            public INCost ( double Value_,
             Currency Currency_,
             double ExchangeRate_)
            {
                Value = Value_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
            }
        }
        public class OUTValue
        {
            public double Value { get; }
            public Currency _Currency { get; }
            public double ExchangeRate { get; }
            public OUTValue(double Value_,
             Currency Currency_,
             double ExchangeRate_)
            {
                Value = Value_;
                _Currency = Currency_;
                ExchangeRate = ExchangeRate_;
            }
        }
        public class ItemOUT
        {
            public uint ItemOUTID {get; }
            public DateTime  OUT_Date { get; }
            public Operation _Operation { get; set; }
            public ItemIN _ItemIN { get; set; }
            public TradeStorePlace Place { get; set; }
            public double Amount { get; set; }
            public ConsumeUnit _ConsumeUnit { get; set; }
            public OUTValue _OUTValue { get; set; }
            public string Notes { get; set; }
            public ItemOUT(uint ItemOUTID_, DateTime OUT_Date_, Operation Operation_, ItemIN ItemIN_, TradeStorePlace Place_,  double Amount_, ConsumeUnit ConsumeUnit_, OUTValue OUTValue_, string Notes_)
            {
                ItemOUTID = ItemOUTID_;
                OUT_Date = OUT_Date_;
                _Operation = Operation_;
                _ItemIN = ItemIN_;
                Place = Place_;
                Amount = Amount_;
                _ConsumeUnit = ConsumeUnit_;
                _OUTValue = OUTValue_;
                Notes = Notes_;

            }
           

        }
        internal class ItemOUTWithCurrencyInfo
        {
            public ItemOUT _ItemOUT { get; }
            public Currency _Currency { get; }
            public ItemOUTWithCurrencyInfo(DatabaseInterface DB, ItemOUT ItemOUT_)
            {
                _ItemOUT = ItemOUT_;
                _Currency = new TradeSQL.OperationSQL(DB).GetOperationItemOUTCurrency(ItemOUT_._Operation);
            }
            public static List<ItemOUTWithCurrencyInfo> ConvertTo_ItemOUTWithCurrencyInfoList(DatabaseInterface DB, List<ItemOUT> ItemOUTList)
            {
                List<ItemOUTWithCurrencyInfo> list = new List<ItemOUTWithCurrencyInfo>();
                try
                {
                    for (int i = 0; i < ItemOUTList.Count; i++)
                    {
                        list.Add(new ItemOUTWithCurrencyInfo(DB, ItemOUTList[i]));
                    }
                    return list;
                }
                catch
                {
                    throw new Exception("فشل جلب التفاصيل العملة");

                    return list;
                }
            }
            public static string GetTotalItemsOUTValue(List<ItemOUTWithCurrencyInfo> ItemOUTWithCurrencyInfoList)
            {
                string value_str = "";
                try
                {

                    List<uint> currencyIDlist = ItemOUTWithCurrencyInfoList.Select(x => x._Currency.CurrencyID).Distinct().ToList();
                    for (int i = 0; i < currencyIDlist.Count; i++)
                    {

                        double valuecurrency = 0;
                        List<ItemOUTWithCurrencyInfo> templist = ItemOUTWithCurrencyInfoList.Where(x => x._Currency.CurrencyID == currencyIDlist[i]).ToList();
                        Currency cuurency = templist[0]._Currency;
                        for (int j = 0; j < templist.Count; j++)
                        {
                            valuecurrency = valuecurrency + templist[j]._ItemOUT._OUTValue.Value ;
                        }
                        value_str = value_str + valuecurrency + " " + cuurency.CurrencySymbol.Replace(" ", string.Empty) + "  ";
                    }
                    if (value_str.Length < 1)
                        return "-";
                    else
                        return value_str;
                }
                catch (Exception ee)
                {
                    return "حصل خطأ" + ee.Message;
                }
            }


        }
        public class ItemIN_ItemOUTReport
        {
            public ItemIN _ItemIN { get; }
            public List<ItemOUT  > ItemOUTList { get; }
            public ItemIN_ItemOUTReport(ItemIN ItemIN_, List<ItemOUT > ItemOUTList_)
            {
                _ItemIN = ItemIN_;
                ItemOUTList  = ItemOUTList_ ;
            }
        }
        //public class BuyOPR
        //{
        //    public uint BuyOPRID;
        //    public BillOUT _BillOUT;
        //    public Item _Item;
        //    public TradeState _TradeState;
        //    public double Amount;
        //    public ConsumeUnit _ConsumeUnit;
        //    public double BuyPrice;
        //    public string Notes;
        //    public BuyOPR(uint BuyOPRID_, BillOUT BillOUT_, Item  Item_, TradeState  TradeState_, double Amount_, ConsumeUnit ConsumeUnit_, double BuyPrice_,  string Notes_)
        //    {
        //        BuyOPRID = BuyOPRID_;
        //        _BillOUT = BillOUT_;
        //        _Item  = Item_ ;
        //        _TradeState = TradeState_;
        //        Amount = Amount_;
        //        _ConsumeUnit = ConsumeUnit_;
        //        BuyPrice = BuyPrice_;
        //        Notes = Notes_;

        //    }
        //}
        public class ItemINSellPrice
        {


            public ItemIN _ItemIN { get; }
            public ConsumeUnit ConsumeUnit_ { get; }
            public SellType SellType_ { get; }
            public double Price { get; }
            public ItemINSellPrice(ItemIN ItemIN_, ConsumeUnit ConsumeUnit__, SellType SellType__, double Price_)
            {

                _ItemIN = ItemIN_;
                ConsumeUnit_ = ConsumeUnit__;
                SellType_ = SellType__;
                Price = Price_;
            }

        }
        //public class SellOPR
        //{
        //    public uint SellOPRID;
        //    public BillSell _BillSell;
        //    public MaintenanceOPR _MaintenanceOPR;
        //    public BuyOPR _BuyOPR;
        //    public TradeStorePlace Place;
        //    public string _SellType;
        //    public double Amount;
        //    public ConsumeUnit _ConsumeUnit;
        //    public double SellPrice;
        //    public string Notes;
        //    public SellOPR(uint SellOPRID_, BillSell BillIN_, MaintenanceOPR MaintenanceOPR_, BuyOPR BuyOPR_, TradeStorePlace Place_, string  SellType_, double Amount_, ConsumeUnit ConsumeUnit_, double SellPrice_,string Notes_)
        //    {
        //        SellOPRID = SellOPRID_;
        //        _BillSell = BillIN_;
        //        _MaintenanceOPR = MaintenanceOPR_;
        //        _BuyOPR = BuyOPR_;
        //        Place = Place_;
        //        _SellType = SellType_;
        //        Amount = Amount_;
        //        _ConsumeUnit = ConsumeUnit_;
        //        SellPrice = SellPrice_;
        //        Notes = Notes_;

        //    }

        
        public class Contact
        {
            public const bool CONTACT_PERSON = false;
            public const bool CONTACT_COMPANY = true;

            public uint ContactID { get; }
            public bool ContactType { get; }
            public  string ContactName { get; }
            public string Phone { get; }
            public string Mobile { get; }
            public string Address { get; }
            public Contact(uint ContactID_,bool ContactType_, string ContactName_, string Phone_, string Mobile_, string Address_)
            {
                ContactID = ContactID_;
                ContactType = ContactType_;
                ContactName = ContactName_;
                Phone = Phone_;
                Mobile = Mobile_;
                Address = Address_;
            }
            public string GetContactTypeString()
            {
                if (ContactType == Contact.CONTACT_COMPANY ) return "شركة";
                else return "شخص";
            }
            public string GetContactTypeHeader()
            {
                if (ContactType == Contact.CONTACT_COMPANY) return "شركة";
                else return "السيد";
            }
            public static  string ConvertTypeToString(bool type)
            {
                if (type == Contact.CONTACT_COMPANY) return "شركة";
                else return "شخص";
            }
            public string Get_Complete_ContactName_WithHeader()
            {
                return GetContactTypeHeader()+":" + ContactName;
            }
        }
        public class TradeState
        {
            public uint TradeStateID { get; }
            public string TradeStateName { get; }

            public TradeState(uint TradeStateID_, string TradeStateName_)
            {
                TradeStateID = TradeStateID_;
                TradeStateName = TradeStateName_;

            }
        }
        public class SellType
        {
            public uint SellTypeID { get; }
            public string SellTypeName { get; }
            public SellType(uint SellTypeID_, string SellTypeName_)
            {
                SellTypeID = SellTypeID_;
                SellTypeName = SellTypeName_;
            }

        }
        public class TradeStoreContainer
        {
            public uint ContainerID { get; }
            public string ContainerName { get; }
            public uint? ParentContainerID { get; }
            public string Desc { get; }
            public TradeStoreContainer (uint ContainerID_,string ContainerName_,uint? ParentContainerID_,string Desc_)
            {
                ContainerID = ContainerID_;
                ContainerName = ContainerName_;
                ParentContainerID = ParentContainerID_;
                Desc = Desc_;
            }
        }
        public class PlaceAvailbeItems_ItemDetails
        {
            public TradeStorePlace Place { get; }
            public Item _Item { get; }
            public string AvailableItemStates { get; }
            public PlaceAvailbeItems_ItemDetails(TradeStorePlace Place_, Item Item_, string AvailableItemStates_ )
            {
                Place = Place_;
                _Item = Item_;
              AvailableItemStates= AvailableItemStates_;
            }
        }

        public class TradeStoreItems_AvailableAmount_Report
        {
            public TradeItemStore _TradeItemStore { get; }
            public double AvailableAmount { get; }
            public TradeStoreItems_AvailableAmount_Report(
             TradeItemStore TradeItemStore_,
             double AvailableAmount_)
            {
                _TradeItemStore = TradeItemStore_;
                AvailableAmount = AvailableAmount_;
            }
        }

        public class ItemIN_AvailableAmount_Report
        {
            public uint OperationType { get; }
            public uint OperationID { get; }
            public uint ItemINID { get; }
            public string ConsumeUnit { get; }
            public double Amount { get; }
            public uint ItemID { get; }
            public string ItemName { get; }

            public string ItemCompany { get; }
            public uint FolderID { get; }

            public string FolderName { get; }
            public string FolderPath { get; }
            public uint TradeStateID { get; }
            public string TradeStateName { get; }
            public double AvailableAmount { get; }

            public ItemIN_AvailableAmount_Report(
                  uint OperationType_,
             uint OperationID_,
            uint ItemINID_,
             string ConsumeUnit_,
             double Amount_,
            uint ItemID_,
             string ItemName_,

             string ItemCompany_,
             uint FolderID_,
             string FolderName_,
             string FolderPath_,
             uint TradeStateID_,
             string TradeStateName_,
             double AvailableAmount_

)
            {
                OperationType = OperationType_;
                OperationID = OperationID_;
                ItemINID = ItemINID_;
                ConsumeUnit = ConsumeUnit_;
                Amount = Amount_;
                ItemID = ItemID_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                FolderID = FolderID_;
                FolderName = FolderName_;
                FolderPath = FolderPath_;
                TradeStateID = TradeStateID_;
                TradeStateName = TradeStateName_;
                AvailableAmount = AvailableAmount_;

            }
            internal static  DataTable  Get_ItemIN_AvailableAmount_Report_List_AS_DataTable(List  <ItemIN_AvailableAmount_Report> list)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("OperationType", typeof(uint));
                    table.Columns.Add("OperationID", typeof(uint));
                    table.Columns.Add("ItemINID", typeof(uint));
                    table.Columns.Add("ConsumeUnit", typeof(string ));
                    table.Columns.Add("Amount", typeof(uint));

                    table.Columns.Add("ItemID", typeof(uint));
                    table.Columns.Add("ItemName", typeof(string));
                    table.Columns.Add("ItemCompany", typeof(string));
                    table.Columns.Add("FolderID", typeof(uint));
                    table.Columns.Add("FolderName", typeof(string));
                    table.Columns.Add("FolderPath", typeof(string));
                    table.Columns.Add("TradeStateID", typeof(uint));
                    table.Columns.Add("TradeStateName", typeof(string));
                    table.Columns.Add("AvailableAmount", typeof(double));

                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["OperationType"] = list[i].OperationType;
                        row["OperationID"] = list[i].OperationID;
                        row["ItemINID"] = list[i].ItemINID;
                        row["ConsumeUnit"] = list[i].ConsumeUnit;
                        row["Amount"] = list[i].Amount;
                        row["ItemID"] = list[i].ItemID;
                        row["ItemName"] = list[i].ItemName;
                        row["ItemCompany"] = list[i].ItemCompany;
                        row["FolderID"] = list[i].FolderID;
                        row["FolderName"] = list[i].FolderName;
                        row["FolderPath"] = list[i].FolderPath;
                        row["TradeStateID"] = list[i].TradeStateID;
                        row["TradeStateName"] = list[i].TradeStateName;
                        row["AvailableAmount"] = list[i].AvailableAmount;
                        table.Rows.Add(row);
                    }
                    return table;

                 
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_ItemIN_AvailableAmount_Report_List_AS_DataTable:" + "," + ee.Message);
                }
            }

        }
        public class ItemIN_AvailableAmount_Report_PlaceDetail
        {
            public ItemIN _ItemIN { get; }
            public TradeStorePlace Place { get; }
            public double StoreAmount { get; }
            public double AvailableAmount { get; }
            public double SpentAmount { get; }

            public ItemIN_AvailableAmount_Report_PlaceDetail(
            ItemIN ItemIN_,
            TradeStorePlace Place_,
            double StoreAmount_,
             double AvailableAmount_,
            double SpentAmount_
         )
            {
                _ItemIN = ItemIN_;
                Place = Place_;
                StoreAmount = StoreAmount_;
                SpentAmount = SpentAmount_;
                AvailableAmount = AvailableAmount_;
            }
        }
        public class Item_AvailableAmount_Report
        {
            public uint ItemID { get; }
            public string ItemName { get; }

            public string ItemCompany { get; }
            public uint FolderID { get; }

            public string FolderName { get; }
            public string FolderPath { get; }
            public uint TradeStateID { get; }
            public string TradeStateName { get; }
            public double AvailableAmount { get; }

            public Item_AvailableAmount_Report(
                uint ItemID_,
             string ItemName_,

             string ItemCompany_,
             uint FolderID_,
             string FolderName_,
             string FolderPath_,
             uint TradeStateID_,
             string TradeStateName_,
             double AvailableAmount_
)
            {
                ItemID = ItemID_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                FolderID = FolderID_;
                FolderName = FolderName_;
                FolderPath = FolderPath_;
                TradeStateID = TradeStateID_;
                TradeStateName = TradeStateName_;
                AvailableAmount = AvailableAmount_;
            }
            internal static  DataTable  Get_Item_AvailableAmount_Report_List_AS_DataTable(List<Item_AvailableAmount_Report> list)
            {

                try
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("ItemID", typeof(uint ));
                    table.Columns.Add("ItemName", typeof(string ));
                    table.Columns.Add("ItemCompany", typeof(string));
                    table.Columns.Add("FolderID", typeof(uint ));
                    table.Columns.Add("FolderName", typeof(string));
                    table.Columns.Add("FolderPath", typeof(string));
                    table.Columns.Add("TradeStateID", typeof(uint ));
                    table.Columns.Add("TradeStateName", typeof(string));
                    table.Columns.Add("AvailableAmount", typeof(double));
                   
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();

                        row["ItemID"]=list [i].ItemID ;
                        row["ItemName"] = list[i].ItemName;
                        row["ItemCompany"] = list[i].ItemCompany;
                        row["FolderID"] = list[i].FolderID;
                        row["FolderName"] = list[i].FolderName;
                        row["FolderPath"] = list[i].FolderPath;
                        row["TradeStateID"] = list[i].TradeStateID;
                        row["TradeStateName"] = list[i].TradeStateName;
                        row["AvailableAmount"] = list[i].AvailableAmount;
                        table.Rows.Add(row);
                    }
                    return table ;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_Item_AvailableAmount_Report_List_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class TradeState_AvailableAmount
        {
            public TradeState _TradeState { get; }
            public double AvailableAmount { get; }
            public TradeState_AvailableAmount(
             TradeState _TradeState_,
             double AvailableAmount_)
            {
                _TradeState = _TradeState_;
                AvailableAmount = AvailableAmount_;
            }
        }
        public class TradeStorePlace
        {
            public uint PlaceID { get; }
            public string PlaceName{ get; }
        public TradeStoreContainer _TradeStoreContainer { get; }
            public string Desc { get; }
            public TradeStorePlace(uint PlaceID_, string PlaceName_, TradeStoreContainer TradeStoreContainer_ ,string Desc_)
            {
                PlaceID = PlaceID_;
                PlaceName = PlaceName_;
                _TradeStoreContainer = TradeStoreContainer_;
                Desc = Desc_;
            }

            internal string GetPlaceInfo()
            {
                if (_TradeStoreContainer == null) return PlaceName;
                return  _TradeStoreContainer.ContainerName + " : " + PlaceName;
            }
        }
        public class TradeItemStore
        {
            public const uint ITEMIN_STORE_TYPE = 0;
            public const uint MAINTENANCE_ITEM_STORE_TYPE = 1;
            public const uint MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE = 2;
            ///// 
            public TradeStorePlace _TradeStorePlace { get; }
            public ItemIN _ItemIN { get; }
            public MaintenanceOPR _MaintenanceOPR { get; }
            public MaintenanceOPR_Accessory _MaintenanceOPR_Accessory { get; }
            public uint StoreType { get; }
            public double   Amount { get; }
            public ConsumeUnit _ConsumeUnit { get; }
            public TradeItemStore ( TradeStorePlace TradeStorePlace_, ItemIN ItemIN_, double  Amount_, ConsumeUnit ConsumeUnit_)
            {

                _TradeStorePlace = TradeStorePlace_;
                _ItemIN = ItemIN_ ;
                StoreType = ITEMIN_STORE_TYPE;
                Amount = Amount_;
                _ConsumeUnit = ConsumeUnit_;
            }
            public TradeItemStore(TradeStorePlace TradeStorePlace_, MaintenanceOPR MaintenanceOPR_, double Amount_, ConsumeUnit ConsumeUnit_)
            {

                _TradeStorePlace = TradeStorePlace_;
                _MaintenanceOPR = MaintenanceOPR_;
                StoreType = MAINTENANCE_ITEM_STORE_TYPE;
                Amount = Amount_;
                _ConsumeUnit = ConsumeUnit_;
            }
            public TradeItemStore(TradeStorePlace TradeStorePlace_, MaintenanceOPR_Accessory MaintenanceOPR_Accessory_, double Amount_, ConsumeUnit ConsumeUnit_)
            {

                _TradeStorePlace = TradeStorePlace_;
                _MaintenanceOPR_Accessory = MaintenanceOPR_Accessory_;
                StoreType = MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE;
                Amount = Amount_;
                _ConsumeUnit = ConsumeUnit_;
            }
        }
        public class RavageOPR
        {
            public Operation _Operation { get; }
            public DateTime RavageOPRDate { get; }
            public Part _Part { get; }
            public int ClauseCount { get; }
            public string Notes { get; }

            public RavageOPR( uint RavageOPRID_,  DateTime RavageOPRDate_,
                 Part Part_, int ClauseCount_,string Notes_)
            {

                _Operation = new Operation (Operation.RavageOPR, RavageOPRID_);

                RavageOPRDate = RavageOPRDate_;
                _Part = Part_;

                ClauseCount = ClauseCount_;
                Notes = Notes_;
            }

        }
        ////////////////////////
        public class Place_Store_Operation
        {

        }
        public class Place_Move_Operation
        {

        }
        public class TradeItemStore_Report
        {
            public const uint ITEMIN_STORE_TYPE = 0;
            public const uint MAINTENANCE_ITEM_STORE_TYPE = 1;
            public const uint MAINTENANCE_ACCESSORIES_ITEM_STORE_TYPE = 2;
            public uint PlaceID { get; }
            public uint ItemSourceOPR_ID { get; }
            public uint StoreType { get; }
            public uint Source_OperationType { get; }
            public uint Source_OperationID { get; }
            public string ConsumUnitName { get; }
            public uint ItemID { get; }
            public string FolderName { get; }
            public string ItemName { get; }
            public string ItemCompany { get; }
            public uint  TradeStateID { get; }
            public string TradeStateName { get; }
            public double AvailableAmount { get; }
            public double SpentAmount { get; }

            public TradeItemStore_Report(
                 uint PlaceID_,
             uint ItemSourceOPR_ID_,
             uint StoreType_,
             uint Source_OperationType_,
             uint Source_OperationID_,
             string ConsumUnitName_,
             uint ItemID_,
             string FolderName_,
             string ItemName_,
             string ItemCompany_,
              uint TradeStateID_,
             string TradeStateName_,
            double AvailableAmount_,
             double SpentAmount_
         )
            {
                PlaceID = PlaceID_;
                ItemSourceOPR_ID = ItemSourceOPR_ID_;
                StoreType = StoreType_;
                Source_OperationType = Source_OperationType_;
                Source_OperationID = Source_OperationID_;
                ConsumUnitName = ConsumUnitName_;
                ItemID = ItemID_;
                FolderName = FolderName_;
                ItemName = ItemName_;
                ItemCompany = ItemCompany_;
                TradeStateID = TradeStateID_; 
                TradeStateName = TradeStateName_;
                AvailableAmount = AvailableAmount_;
                SpentAmount = SpentAmount_;
            }
            internal static  DataTable Get_TradeItemStore_Report_AS_DataTable(List<TradeItemStore_Report> list)
            {

                try
                { 

            DataTable table = new DataTable();
                    table.Columns.Add("PlaceID", typeof(uint));
                    table.Columns.Add("ItemSourceOPR_ID", typeof(uint));
                    table.Columns.Add("StoreType", typeof(uint));
                    table.Columns.Add("Source_OperationType", typeof(uint));
                    table.Columns.Add("Source_OperationID", typeof(uint));
                    table.Columns.Add("ConsumUnitName", typeof(string ));
                    table.Columns.Add("ItemID", typeof(uint));
                    table.Columns.Add("FolderName", typeof(string));
                    table.Columns.Add("ItemName", typeof(string));
                    table.Columns.Add("ItemCompany", typeof(string));
                    table.Columns.Add("TradeStateID", typeof(uint ));
                    table.Columns.Add("TradeStateName", typeof(string));
                    table.Columns.Add("AvailableAmount", typeof(double));
                    table.Columns.Add("SpentAmount", typeof(double));
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row["PlaceID"] = list[i].PlaceID;
                        row["ItemSourceOPR_ID"] = list[i].ItemSourceOPR_ID;
                        row["StoreType"] = list[i].StoreType;
                        row["Source_OperationType"] = list[i].Source_OperationType;
                        row["Source_OperationID"] = list[i].Source_OperationID;
                        row["ConsumUnitName"] = list[i].ConsumUnitName ;
                        row["ItemID"] = list[i].ItemID;
                        row["FolderName"] = list[i].FolderName;
                        row["ItemName"] = list[i].ItemName;
                        row["ItemCompany"] = list[i].ItemCompany;
                        row["TradeStateID"] = list[i].TradeStateID;
                        row["TradeStateName"] = list[i].TradeStateName;
                        row["AvailableAmount"] = list[i].AvailableAmount;
                        row["SpentAmount"] = list[i].SpentAmount;
                        table.Rows.Add(row);
                    }
                    return table;
                }
                catch (Exception ee)
                {
                    throw new Exception("Get_TradeItemStore_Report_AS_DataTable:" + ee.Message);
                }
            }

        }
        public class Industrial_OPR
        {
            /// <summary>
            /// ///
            /// </summary>
            public Operation _Operation;
            public DateTime OPR_Date;
            public string OprDesc;
            public double? Amount;
            public ConsumeUnit _ConsumeUnit;
            public Item _Item;
            public string TradeStateName;
            public Money_Currency _Money_Currency;

            public string OPRStatus;
            public Industrial_OPR(Operation Operation_
                , DateTime OPR_Date_, string OprDesc_, double? Amount_, ConsumeUnit ConsumeUnit_,
                    Item Item_, string TradeStateName_, Money_Currency Money_Currency_, string OPRStatus_)
            {
                _Operation = Operation_;
                OPR_Date = OPR_Date_;
                OprDesc = OprDesc_;
                Amount = Amount_;
                _ConsumeUnit = ConsumeUnit_;
                _Item = Item_;
                TradeStateName = TradeStateName_;
                _Money_Currency = Money_Currency_;

                OPRStatus = OPRStatus_;
            }
        }

        public class DisAssemblabgeOPR
        {
            public Operation _Operation;
            public DateTime OprDate;
            public string OprDesc;
            public string Notes;
            public ItemOUT _ItemOUT;

            //public Currency _Currency;
            //public double ExchangeRate;
            public DisAssemblabgeOPR(uint OperationID, DateTime OprDate_, string OprDesc_,
                string Notes_, ItemOUT ItemOUT_)
            {
                _Operation = new Operation(Operation.DISASSEMBLAGE, OperationID);
                OprDate = OprDate_;
                Notes = Notes_;
                OprDesc = OprDesc_;
                _ItemOUT = ItemOUT_;

                //_Currency = Currency_;
                //ExchangeRate = ExchangeRate_;

            }
        }
        public class AssemblabgeOPR
        {
            public Operation _Operation;
            public DateTime OprDate;
            public string OprDesc;
            public string Notes;
            public ItemIN _ItemIN;

            public AssemblabgeOPR(uint OperationID, DateTime OprDate_, string OprDesc_,
            string Notes_, ItemIN ItemIN_
                )
            {
                _Operation = new Operation(Operation.ASSEMBLAGE, OperationID);
                OprDate = OprDate_;
                Notes = Notes_;
                OprDesc = OprDesc_;
                _ItemIN = ItemIN_;

                //_Currency = Currency_;
                //ExchangeRate = ExchangeRate_;


            }
        }
    }
}
