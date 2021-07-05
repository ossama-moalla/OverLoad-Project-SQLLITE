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
    public partial class ItemIN_StoreReport_Form : Form
    {
        DatabaseInterface DB;
        ItemIN _ItemIN;
        private TradeStorePlace _ReturnPlace;
        public TradeStorePlace ReturnPlace
        {
            get
            {
                return _ReturnPlace;
            }
        }
        public ItemIN_StoreReport_Form(DatabaseInterface db,ItemIN ItemIN_)
        {
            DB = db;
            InitializeComponent();
            _ItemIN = ItemIN_;
            TextboxOperationType.Text = Operation.GetOperationName(_ItemIN._Operation.OperationType);
            textBoxOperatonID.Text = _ItemIN._Operation.OperationID.ToString ();
            textBoxItemINID.Text = _ItemIN.ItemINID.ToString();
            textBoxItemType.Text = _ItemIN._Item.folder.FolderName;
            textBoxItemName.Text = _ItemIN._Item.ItemName;
            textBoxItemCompany.Text = _ItemIN._Item.ItemCompany;
            RefreshItemStorePlaces();

        }
        private void RefreshItemStorePlaces()
        {

            try
            {
                List<ItemIN_StoreReport> iteminstore_placelist = new ItemINSQL(DB).GetItemIN_StoreReportList(_ItemIN);

                List<ItemIN_StoreReport> iteminstore_placelist_NonStore = iteminstore_placelist.Where(x => x.Place == null).ToList();
                List<ItemIN_StoreReport> iteminstore_placelist_Store = iteminstore_placelist.Where(x => x.Place != null).ToList();
                listViewItemStorePlace.Items.Clear();
                AvailableItemSQL AvailableItemSQL_ = new AvailableItemSQL(DB);
                if (iteminstore_placelist_NonStore.Count > 0)
                {

                    ListViewItem ListViewItem_ = new ListViewItem();
                    ListViewItem_.Name = "null";
                    ListViewItem_.SubItems.Add("غير مخزن");
                    ListViewItem_.SubItems.Add(_ItemIN._ConsumeUnit.ConsumeUnitName);
                    ListViewItem_.SubItems.Add(iteminstore_placelist_NonStore[0].StoreAmount.ToString());
                    ListViewItem_.SubItems.Add(AvailableItemSQL_.Get_SpentAmount_by_Place (_ItemIN, null).ToString());
                    ListViewItem_.BackColor = Color.Orange;
                    listViewItemStorePlace.Items.Add(ListViewItem_);
                }

                for (int i = 0; i < iteminstore_placelist_Store.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(iteminstore_placelist_Store[i].Place.PlaceID.ToString());
                    ListViewItem_.Name = iteminstore_placelist_Store[i].Place.PlaceID.ToString();
                    ListViewItem_.SubItems.Add(iteminstore_placelist_Store[i].Place.GetPlaceInfo());
                    ListViewItem_.SubItems.Add(iteminstore_placelist_Store[i]._ConsumeUnit.ConsumeUnitName);
                    ListViewItem_.SubItems.Add(iteminstore_placelist_Store[i].StoreAmount.ToString());
                    ListViewItem_.SubItems.Add(iteminstore_placelist_Store[i].SpentAmount.ToString());

                    ListViewItem_.BackColor = Color.LimeGreen;
                    listViewItemStorePlace.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshItemStorePlaces" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (listViewItemStorePlace.SelectedItems.Count == 1)
            {
                try
                {
                    if (listViewItemStorePlace.SelectedItems[0].Name == "null")
                        _ReturnPlace = null;
                    else
                        _ReturnPlace = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(listViewItemStorePlace.SelectedItems[0].Name));

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("buttonSelect_Click", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listViewItemStorePlace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && listViewItemStorePlace.SelectedItems.Count == 1)
                buttonSelect.PerformClick();
        }

        private void listViewItemStorePlace_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left )
                buttonSelect.PerformClick();

        }
    }
}
