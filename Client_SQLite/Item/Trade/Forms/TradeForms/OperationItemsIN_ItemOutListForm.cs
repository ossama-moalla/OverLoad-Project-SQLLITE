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
    public partial class OperationItemsIN_ItemOutListForm : Form
    {
        
        DatabaseInterface DB;

        System.Windows.Forms.MenuItem OpenSourceOperation_MenuItem;
        System.Windows.Forms.MenuItem OpenItemOUT_MenuItem;
        Operation _Operation;
        Currency _Currency;
        List<ItemOUTWithCurrencyInfo> _ItemOUT_Currency_List ;
        public OperationItemsIN_ItemOutListForm(DatabaseInterface db,Operation Operation_)
        {
            DB = db;
            InitializeComponent();
            _Operation = Operation_;
            _Currency  = new OperationSQL(DB).GetOperationItemINCurrency(_Operation);
            textBoxCurrency.Text = _Currency.CurrencyName;
            textBoxExchangeRate.Text = _Currency.ExchangeRate.ToString();
            _ItemOUT_Currency_List = new List<ItemOUTWithCurrencyInfo>();
         
            TextboxOperationType.Text = Operation.GetOperationName(_Operation.OperationType);
            textBoxOperatonID.Text = _Operation.OperationID .ToString();
            //textBoxItemINID.Text = ItemIN_.ItemINID.ToString();
            //textBoxItemName.Text = ItemIN_._Item.ItemName;
            //textBoxItemType.Text = ItemIN_._Item.folder .FolderName ;
            //textBoxItemCompany.Text = ItemIN_._Item.ItemCompany ;
   
           OpenSourceOperation_MenuItem = new System.Windows.Forms.MenuItem("استعراض العملية المصدر", OpenSourceOperation_MenuItem_Click);
            OpenItemOUT_MenuItem = new System.Windows.Forms.MenuItem("استعراض تفاصيل عملية الاخراج", OpenItemOUT_MenuItem_MenuItem_Click);
            LoadForm();


        }
        public async void LoadForm()
        {
            try
            {
                double iteminvalue = 0;
                List<ItemIN> ItemINList = new ItemINSQL(DB).GetItemINList(_Operation);
                for (int i = 0; i < ItemINList.Count; i++)
                {
                    iteminvalue = iteminvalue + ItemINList[i]._INCost.Value;
                    List<ItemOUT> ItemIN_ItemoutList = new ItemOUTSQL(DB).GetItemIN_ItemOUTList("OperationItemsIN_ItemOutListForm-LoadForm",ItemINList[i]);
                    for (int j = 0; j < ItemIN_ItemoutList.Count; j++)
                        _ItemOUT_Currency_List.Add(new ItemOUTWithCurrencyInfo(DB, ItemIN_ItemoutList[j]));

                }
                textBoxItemIN_Value.Text = iteminvalue.ToString()+" "+_Currency .CurrencySymbol ; 
                textBoxItemIN_Dollar_value.Text =  System.Math.Round ((iteminvalue / _Currency .ExchangeRate),2).ToString()+" $";
                FillComboBox_ITEM();
                this.comboBox_ITEM.SelectedIndexChanged += new System.EventHandler(this.comboBox_ITEM_SelectedIndexChanged);


            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("فشل تحميل التقرير :" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }

            RefreshItemsOut(_ItemOUT_Currency_List);
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
                    case Operation.RavageOPR:
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


        private async  void RefreshItemsOut(List <ItemOUTWithCurrencyInfo  > ItemOUT_Currency_List)
        {
            listView.Items.Clear();
            //ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxCurrency.SelectedItem;
            //Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);
             double dollare_value = 0;
            for (int i=0;i< ItemOUT_Currency_List.Count;i++)
            {
                double sellprice = ItemOUT_Currency_List[i]._ItemOUT._OUTValue .Value   ;
                dollare_value = dollare_value+(sellprice / ItemOUT_Currency_List[i]._Currency.ExchangeRate);
                double total_sellprice = System.Math.Round(sellprice * ItemOUT_Currency_List[i]._ItemOUT.Amount,3);
                ListViewItem ListViewItem_ = new ListViewItem((listView .Items .Count +1).ToString ());
                ListViewItem_.Name = ItemOUT_Currency_List[i]._ItemOUT.ItemOUTID .ToString();
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT._ItemIN .ItemINID.ToString());
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT._ItemIN._Item .GetItemFullName ());

                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT._ItemIN._TradeState .TradeStateName);

                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT.ItemOUTID.ToString());
                ListViewItem_.SubItems.Add(Operation .GetOperationItemOutDesc ( ItemOUT_Currency_List[i]._ItemOUT._Operation .OperationType));
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._ItemOUT._Operation.OperationID.ToString());
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._Currency.CurrencySymbol );
                ListViewItem_.SubItems.Add(ItemOUT_Currency_List[i]._Currency.ExchangeRate .ToString ());
                if (ItemOUT_Currency_List[i]._ItemOUT.Place!=null )
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
            textBoxDollar_Value.Text = System.Math.Round(dollare_value, 2).ToString() + "$"; ;
            textBoxValue.Text = ItemOUTWithCurrencyInfo.GetTotalItemsOUTValue(ItemOUT_Currency_List);

        }
        private void FillComboBox_ITEM()
        {

            IEnumerable<string> distincItemsIN = _ItemOUT_Currency_List.Select(x => x._ItemOUT ._ItemIN._Item .GetItemFullName()).Distinct();
            comboBox_ITEM .Items.Clear();
            comboBox_ITEM.Items.Add("الكل");
            foreach (var s in distincItemsIN)
                comboBox_ITEM.Items.Add(s);
            comboBox_ITEM.SelectedIndex = 0;

        }
        //public   string  GetTotalItemsOUTValue(List <ItemOUTWithCurrencyInfo > ItemOUTWithCurrencyInfoList)
        //{
        //    string value_str = "";
        //    try
        //    {

        //        List<uint> currencyIDlist = ItemOUTWithCurrencyInfoList.Select  (x => x._Currency.CurrencyID ).Distinct().ToList ();
        //        for (int i=0;i< currencyIDlist.Count;i++)
        //        {

        //            double valuecurrency = 0;
        //            List <ItemOUTWithCurrencyInfo > templist= ItemOUTWithCurrencyInfoList.Where(x => x._Currency.CurrencyID == currencyIDlist[i]).ToList();
        //            Currency cuurency = templist[0]._Currency;
        //            for (int j = 0; j < templist.Count; j++)
        //            {
        //                valuecurrency = valuecurrency + templist[j]._ItemOUT.Cost;
        //            }
        //            value_str = value_str + valuecurrency+ " " + cuurency.CurrencySymbol.Replace(" ", string.Empty)+"  ";
        //        }
        //        if (value_str.Length < 1)
        //            return  "-";
        //        else
        //             return  value_str;
        //    }
        //    catch(Exception ee)
        //    {
        //        return  "حصل خطأ"+ee.Message;
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
            AdjustListViewColumnsWidth();
        }
        public void AdjustListViewColumnsWidth()
        {
            listView.Columns[0].Width = 80;
            
            listView.Columns[1].Width = 100;
            listView.Columns[2].Width = 200;
            for (int i=3;i<13;i++)
                listView.Columns[i].Width = 100;
            //listView.Columns[4].Width = 100;
            //listView.Columns[5].Width = 80;
            //listView.Columns[6].Width = 70;
            //listView.Columns[7].Width = 85;
            //listView.Columns[8].Width = 125;
            //listView.Columns[9].Width = 80;
            //listView.Columns[10].Width = 100;
            //listView.Columns[11].Width = 95;
            //listView.Columns[12].Width = 80;


        }

        private void comboBox_ITEM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_ITEM.SelectedIndex > 0)
                {
                    string selectitem = comboBox_ITEM.SelectedItem.ToString();
                    List<ItemOUTWithCurrencyInfo> List_ = _ItemOUT_Currency_List.Where(x => x._ItemOUT._ItemIN._Item.GetItemFullName() == selectitem).ToList();
                    RefreshItemsOut(List_);
                }
                else
                    RefreshItemsOut(_ItemOUT_Currency_List);
            }
            catch
            {

            }
        }
        //private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    RefreshSellOperations(_ItemOUTList );
        //}
    }
}
