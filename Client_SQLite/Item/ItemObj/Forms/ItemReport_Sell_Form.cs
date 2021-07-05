using OverLoad_Client.ItemObj.Objects;
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

namespace OverLoad_Client.ItemObj.Forms
{
    public partial class ItemReport_Sell_Form : Form
    {
        DatabaseInterface DB;
        Item Item_;
        MenuItem OpenBillSell_MenuItem;
        public ItemReport_Sell_Form(DatabaseInterface db, Item item)
        {
            Item_ = item;
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Sell_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء او مجموعة المبيعات, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            textBoxItemID.Text  = Item_.ItemID.ToString ();
            textBoxItemName.Text = Item_.ItemName;
            textBoxItemCompany.Text = Item_.ItemCompany;
            textBoxItemType.Text = Item_.folder .FolderName;
            textBoxItemID.Text = Item_.ItemID.ToString();
            OpenBillSell_MenuItem = new MenuItem("فتح", OpenBillSell_MenuItem_Click);
            FillData();
        }

        private void FillData()
        {
            try
            {

                List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList_ForItem(Item_).Where (x=>x._Operation .OperationType ==Operation.BILL_SELL).ToList ();

                for (int i=0;i< ItemOUTList.Count;i++)
                {

                    Bill bill = new OperationSQL(DB).GetOperationBill(ItemOUTList[i]._Operation);
                    //List<AccountingObj.Objects.Money_Currency> itemsoutValue = AccountingObj.Objects.Money_Currency.Get_Money_Currency_List_From_ItemOUT(ItemINlist[i].ItemOUTList);
                    ListViewItem ListViewItem_ = new ListViewItem(bill._Operation.OperationID .ToString ());
                    ListViewItem_.Name = bill._Operation.OperationID.ToString();
                    ListViewItem_.SubItems.Add(bill .BillDate.ToShortDateString ());
                    ListViewItem_.SubItems.Add(bill._Contact.ContactName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i].ItemOUTID .ToString ());
                    ListViewItem_.SubItems.Add(ItemOUTList[i].Amount+" " + ItemOUTList [i]._ConsumeUnit.ConsumeUnitName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._OUTValue.Value + " " + ItemOUTList[i]._OUTValue._Currency .CurrencySymbol);
                    ListViewItem_.SubItems.Add((ItemOUTList[i]._OUTValue.Value * ItemOUTList [i].Amount) + " " + ItemOUTList[i]._OUTValue._Currency.CurrencySymbol);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN ._INCost.Value /**(ItemOUTList[i]._ConsumeUnit.Factor / ItemOUTList[i]._ItemIN ._ConsumeUnit.Factor )*/ + " " + ItemOUTList[i]._ItemIN._INCost._Currency.CurrencySymbol);

                    listViewSell.Items.Add(ListViewItem_);
                }
            }catch(Exception ee)
            {
                MessageBox.Show("FillData:"+ee.Message , "",MessageBoxButtons.OK ,MessageBoxIcon.Error );
            }
        }
        private void OpenBillSell_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewSell.SelectedItems.Count == 1)
            {
                try
                {
                    uint BillSellid = Convert.ToUInt32(listViewSell.SelectedItems[0].Name);
                    BillSell BillSell_ = new BillSellSQL(DB).GetBillSell_INFO_BYID(BillSellid);
                    Trade.Forms.TradeForms.BillSellForm BillSellForm_ = new Trade.Forms.TradeForms.BillSellForm(DB, BillSell_, false);
                    BillSellForm_.ShowDialog();

                }
                catch (Exception ee)
                {
                    MessageBox.Show("حدث خطأ " + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void listViewSell_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewSell.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();

                foreach (ListViewItem item1 in listViewSell.Items)
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

                    MenuItem[] mi1 = new MenuItem[] { OpenBillSell_MenuItem };
                    listViewSell.ContextMenu = new ContextMenu(mi1);
                }


            }
        }
        private void listViewSell_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewSell.SelectedItems.Count > 0)
            {
                OpenBillSell_MenuItem.PerformClick();
            }
        }
    }
}
