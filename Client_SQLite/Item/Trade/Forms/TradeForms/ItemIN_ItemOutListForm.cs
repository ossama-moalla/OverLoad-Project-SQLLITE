using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Maintenance.Forms;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Trade.Forms.TradeForms
{
    public partial class ItemIN_ItemOutListForm : Form
    {
        
        DatabaseInterface DB;

        System.Windows.Forms.MenuItem OpenSourceOperation_MenuItem;
        System.Windows.Forms.MenuItem OpenItemOUT_MenuItem;
        ItemIN _ItemIN;
        List<ItemOUTWithCurrencyInfo> _ItemOUT_Currency_List = new List<ItemOUTWithCurrencyInfo>();
        public ItemIN_ItemOutListForm(DatabaseInterface db,ItemIN ItemIN_)
        {
            DB = db;
            InitializeComponent();
            _ItemIN = ItemIN_;
            TextboxOperationType.Text = Operation.GetOperationName(ItemIN_._Operation.OperationType);
            textBoxOperatonID.Text = ItemIN_._Operation.OperationID .ToString();
            textBoxItemINID.Text = ItemIN_.ItemINID.ToString();
            textBoxItemName.Text = ItemIN_._Item.ItemName;
            textBoxItemType.Text = ItemIN_._Item.folder .FolderName ;
            textBoxItemCompany.Text = ItemIN_._Item.ItemCompany ;

            List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("ItemIN_ItemOutListForm",_ItemIN);
            _ItemOUT_Currency_List = ItemOUTWithCurrencyInfo.ConvertTo_ItemOUTWithCurrencyInfoList(DB,ItemOUTList);
            RefreshItemsOut(_ItemOUT_Currency_List);
           OpenSourceOperation_MenuItem = new System.Windows.Forms.MenuItem("استعراض العملية المصدر", OpenSourceOperation_MenuItem_Click);
            OpenItemOUT_MenuItem = new System.Windows.Forms.MenuItem("استعراض تفاصيل عملية الاخراج", OpenItemOUT_MenuItem_MenuItem_Click);



        }

        private void OpenSourceOperation_MenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {

                uint itemoutid = Convert.ToUInt32(listView.SelectedItems[0].Name);
                ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                switch (ItemOUT_ ._Operation .OperationType)
                {
                    case Operation.BILL_SELL:
                        BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(ItemOUT_._Operation.OperationID);
                        BillSellForm BillSellForm_ = new BillSellForm(DB, BillSell_, false);
                        BillSellForm_.ShowDialog();
                        break;
                    case Operation.MAINTENANCE_OPR :
                        MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(ItemOUT_._Operation.OperationID);
                        MaintenanceOPRForm MaintenanceOPRForm_ = new MaintenanceOPRForm(DB, MaintenanceOPR_, false);
                        MaintenanceOPRForm_.ShowDialog();
                        break;
                    case Operation.ASSEMBLAGE:
                        AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(ItemOUT_._Operation.OperationID);
                        AssemblageForm AssemblageForm_ = new AssemblageForm(DB, AssemblabgeOPR_, false);
                        AssemblageForm_.ShowDialog();
                        break;
                    case Operation.DISASSEMBLAGE :
                        DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID (ItemOUT_._Operation.OperationID);
                        DisAssemblageForm DisAssemblageForm_ = new DisAssemblageForm(DB, DisAssemblabgeOPR_, false);
                        DisAssemblageForm_.ShowDialog();
                        break;
                    case Operation.RavageOPR   :
                        //MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(ItemOUT_._Operation.OperationID);
                        //MaintenanceOPRForm MaintenanceOPRForm_ = new MaintenanceOPRForm(DB, MaintenanceOPR_, false);
                        //MaintenanceOPRForm_.ShowDialog();
                        break;
                }
            }
        }
        private void OpenItemOUT_MenuItem_MenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {

                uint itemoutid = Convert.ToUInt32(listView.SelectedItems[0].Name);
                ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, false);
                ItemOUTForm_.ShowDialog(); 
                ItemOUTForm_.Dispose();
            }
        }


        private void RefreshItemsOut(List <ItemOUTWithCurrencyInfo  > ItemOUT_Currency_List)
        {

            listView.Items.Clear();
            //ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxCurrency.SelectedItem;
            //Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);
   
            for (int i=0;i< ItemOUT_Currency_List.Count;i++)
            {
                double sellprice = ItemOUT_Currency_List[i]._ItemOUT._OUTValue .Value   ;
                double total_sellprice = System.Math.Round(sellprice * ItemOUT_Currency_List[i]._ItemOUT.Amount,3);
                ListViewItem ListViewItem_ = new ListViewItem((listView .Items .Count +1).ToString ());
                ListViewItem_.Name = ItemOUT_Currency_List[i]._ItemOUT.ItemOUTID .ToString();
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT.ItemOUTID.ToString());
                ListViewItem_.SubItems.Add(Operation .GetOperationName ( ItemOUT_Currency_List[i]._ItemOUT._Operation .OperationType));
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT._Operation.OperationID.ToString());
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._Currency.CurrencyName);
               if(ItemOUT_Currency_List[i]._ItemOUT.Place!=null )
                    ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT .Place.GetPlaceInfo() );
               else
                    ListViewItem_.SubItems.Add("");

                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT.Amount.ToString());
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT._ConsumeUnit.ConsumeUnitName.ToString());


                ListViewItem_.SubItems.Add(sellprice.ToString() + " " + ItemOUT_Currency_List[i]._Currency .CurrencySymbol.Replace (" ",string.Empty ));
                ListViewItem_.SubItems.Add((total_sellprice).ToString ()+" " + ItemOUT_Currency_List[i]._Currency.CurrencySymbol.Replace(" ", string.Empty));
                ListViewItem_.BackColor = Color.LimeGreen ;
                listView.Items.Add(ListViewItem_);
            }
            textBoxValue.Text = ItemOUTWithCurrencyInfo.GetTotalItemsOUTValue(ItemOUT_Currency_List);


        }
        //private  void SetTotalItemsOUTValue(List <ItemOUTWithCurrencyInfo > ItemOUTWithCurrencyInfoList)
        //{
        //    string value_str = "";
        //    try
        //    {
        //        List<Currency> currencylist = ItemOUTWithCurrencyInfoList.Select(x => x._Currency).Distinct().ToList ();
        //        for(int i=0;i<currencylist .Count;i++)
        //        {

        //            double valuecurrency = 0;
        //            List <ItemOUTWithCurrencyInfo > templist= ItemOUTWithCurrencyInfoList.Where (x => x._Currency== currencylist[i]).ToList();
        //            for (int j = 0; j < templist.Count; j++)
        //            {
        //                valuecurrency = valuecurrency + templist[j]._ItemOUT.Cost;
        //            }
        //            value_str = value_str + valuecurrency+ " " + currencylist[i].CurrencySymbol.Replace(" ", string.Empty)+"  ";
        //        }
        //        if (value_str.Length < 1)
        //            textBoxValue.Text = "-";
        //        else
        //            textBoxValue.Text = value_str;
        //    }
        //    catch
        //    {
        //        textBoxValue.Text = "حصل خطأ";
        //    }
        //}





        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView.SelectedItems.Count > 0)
            {
                OpenItemOUT_MenuItem  .PerformClick();
            }
        }
        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            listView.ContextMenu = null;
            bool match = false;
            ListViewItem listitem = new ListViewItem();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item1 in listView.Items)
                {
                    if (item1.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        match = true;
                        listitem = item1;
                        break;
                    }
                }
                if (match)
                {


                        MenuItem[] mi1 = new MenuItem[] {OpenItemOUT_MenuItem  , OpenSourceOperation_MenuItem };
                        listView.ContextMenu = new ContextMenu(mi1);


                }  

            }

        }

        private void listView_Resize(object sender, EventArgs e)
        {
            //AdjustListViewColumnsWidth();
        }
        public void AdjustListViewColumnsWidth()
        {
            listView.Columns[0].Width = 80;
            
            listView.Columns[1].Width = (listView.Width - 80) /8 - 1;
            listView.Columns[2].Width = (listView.Width - 80) /8 - 1;
            listView.Columns[3].Width = (listView.Width - 80) / 8 - 1;
            listView.Columns[4].Width = (listView.Width - 80) / 8 - 1;
            listView.Columns[5].Width = (listView.Width - 80) /8 - 1;
            listView.Columns[6].Width = (listView.Width - 80) / 8 - 1;
            listView.Columns[7].Width = (listView.Width - 80) / 8 - 1;
            listView.Columns[8].Width = (listView.Width - 80) / 8 - 1;


        } 
        //private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    RefreshSellOperations(_ItemOUTList );
        //}
    }
}
