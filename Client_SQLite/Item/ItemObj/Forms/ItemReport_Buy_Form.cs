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
    public partial class ItemReport_Buy_Form : Form
    {
        DatabaseInterface DB;
        Item Item_;
        MenuItem OpenBillBuy_MenuItem;
        public ItemReport_Buy_Form(DatabaseInterface db, Item item)
        {
            Item_ = item;
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Buy_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء او مجموعة المشتريات, لا يمكنك فتح هذه النافذة");

            InitializeComponent();
            textBoxItemID.Text  = Item_.ItemID.ToString ();
            textBoxItemName.Text = Item_.ItemName;
            textBoxItemCompany.Text = Item_.ItemCompany;
            textBoxItemType.Text = Item_.folder .FolderName;
            textBoxItemID.Text = Item_.ItemID.ToString();
            OpenBillBuy_MenuItem = new MenuItem("فتح", OpenBillBuy_MenuItem_Click);
            FillData();
        }

        private void FillData()
        {
            try
            {
                
                List <ItemIN_ItemOUTReport > ItemINlist = new ItemINSQL (DB).GetItemINList_ForItem (Item_).Where (x=>x._ItemIN._Operation.OperationType ==Operation.BILL_BUY ).ToList ();
                for(int i=0;i<ItemINlist .Count;i++)
                {
                    double consumeuint = 0;
                    for(int j=0;j< ItemINlist[i].ItemOUTList .Count;j++)
                    {
                        ItemOUT itemout = ItemINlist[i].ItemOUTList[j];
                        consumeuint += itemout.Amount * (ItemINlist[i]._ItemIN._ConsumeUnit.Factor / itemout._ConsumeUnit.Factor);
                    }
                    Bill bill = new OperationSQL(DB).GetOperationBill(ItemINlist[i]._ItemIN._Operation);
                    List<AccountingObj.Objects.Money_Currency> itemsoutValue = AccountingObj.Objects.Money_Currency.Get_Money_Currency_List_From_ItemOUT(ItemINlist[i].ItemOUTList);
                    ListViewItem ListViewItem_ = new ListViewItem(bill._Operation.OperationID .ToString ());
                    ListViewItem_.Name = bill._Operation.OperationID.ToString();
                    ListViewItem_.SubItems.Add(bill .BillDate.ToShortDateString ());
                    ListViewItem_.SubItems.Add(bill._Contact.ContactName);
                    ListViewItem_.SubItems.Add(ItemINlist[i]._ItemIN.ItemINID.ToString ());
                    ListViewItem_.SubItems.Add(ItemINlist[i]._ItemIN.Amount+" " + ItemINlist[i]._ItemIN._ConsumeUnit.ConsumeUnitName);
                    ListViewItem_.SubItems.Add(consumeuint  + " " + ItemINlist[i]._ItemIN._ConsumeUnit.ConsumeUnitName);
                    ListViewItem_.SubItems.Add(ItemINlist[i]._ItemIN._INCost.Value + " " + ItemINlist[i]._ItemIN._INCost._Currency .CurrencySymbol);
                    ListViewItem_.SubItems.Add((ItemINlist[i]._ItemIN._INCost.Value* ItemINlist[i]._ItemIN.Amount) + " " + ItemINlist[i]._ItemIN._INCost._Currency.CurrencySymbol);
                    ListViewItem_.SubItems.Add(AccountingObj.Objects.Money_Currency.ConvertMoney_CurrencyList_TOString(itemsoutValue));
                    listViewBuy.Items.Add(ListViewItem_);
                }
            }catch(Exception ee)
            {
                MessageBox.Show("FillData:"+ee.Message , "",MessageBoxButtons.OK ,MessageBoxIcon.Error );
            }
        }
        private void OpenBillBuy_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewBuy.SelectedItems.Count == 1)
            {
                try
                {
                    uint BillBuyid = Convert.ToUInt32(listViewBuy.SelectedItems[0].Name);
                    BillBuy  BillBuy_ = new BillBuySQL(DB).GetBillBuy_INFO_BYID(BillBuyid);
                    Trade.Forms.TradeForms.BillBuyForm BillBuyForm_ = new Trade.Forms.TradeForms.BillBuyForm(DB, BillBuy_, false);
                    BillBuyForm_.ShowDialog();

                }
                catch (Exception ee)
                {
                    MessageBox.Show("حدث خطأ " + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void listViewBuy_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewBuy.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();

                foreach (ListViewItem item1 in listViewBuy.Items)
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

                    MenuItem[] mi1 = new MenuItem[] { OpenBillBuy_MenuItem  };
                    listViewBuy.ContextMenu = new ContextMenu(mi1);
                }


            }
        }
        private void listViewBuy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewBuy.SelectedItems.Count > 0)
            {
                OpenBillBuy_MenuItem.PerformClick();
            }
        }
    }
}
