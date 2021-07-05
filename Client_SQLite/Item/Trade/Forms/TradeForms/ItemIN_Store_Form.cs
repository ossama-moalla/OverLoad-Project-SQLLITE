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

namespace OverLoad_Client.Trade.Forms.TradeForms
{
    public partial class ItemIN_Store_Form : Form
    {
        System.Windows.Forms.MenuItem OpenPlaceDetailsMenuItem;
        System.Windows.Forms.MenuItem UnStoreAmountMenuItem;
        System.Windows.Forms.MenuItem EditStoreAMountMenuItem;
        //System.Windows.Forms.MenuItem DisAssemblageMenuItem;

        DatabaseInterface DB;
        ItemIN _ItemIN;


        public const int GET_PLACE_FUNCTION = 0;
        public const int ONLY_READ_FUNCTION = 1;
        public const int STORE_CONFIG_FUNCTION = 2;

        int FUNCTION;
        private TradeStorePlace _ReturnPlace;
        public TradeStorePlace ReturnPlace
        {
            get
            {
                return _ReturnPlace;
            }
        }
        TradeStorePlace _TempStorePlace;
        public ItemIN_Store_Form(DatabaseInterface db,ItemIN ItemIN_,int FUNCTION_)
        {
            InitializeComponent();
            DB = db;
            _ItemIN = ItemIN_;
            FUNCTION = FUNCTION_;
            FillComboBoxConsumeUnit(null);
            switch (FUNCTION )
            {
                case GET_PLACE_FUNCTION:
                    panelStoreConfig.Enabled = false;
                    buttonGetPlace.Visible = true;
                    this.listViewItemStorePlace.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);

                    break;
                case ONLY_READ_FUNCTION:
                    panelStoreConfig.Enabled = false;
                    buttonGetPlace.Visible = false ;
                    break;
                case STORE_CONFIG_FUNCTION:
                    panelStoreConfig.Enabled = true ;
                    buttonGetPlace.Visible = false ;
                    this.listViewItemStorePlace.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);

                    this.listViewItemStorePlace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);

                    break;
            }
            RefreshItemStorePlaces();
            OpenPlaceDetailsMenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل مكان التخزين", OpenPlaceDetails_MenuItem_Click);
            UnStoreAmountMenuItem = new System.Windows.Forms.MenuItem("الغاء تخزين ", UnStoreAmount_MenuItem_Click);
            EditStoreAMountMenuItem = new System.Windows.Forms.MenuItem("تعديل الكمية", EditStoreAMount_MenuItem_Click);
            //DisAssemblageMenuItem = new System.Windows.Forms.MenuItem("تفكيك عنصر", DisAssemblage_MenuItem_Click);


            this.textBoxPlaceID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.textBoxPlaceID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPlace_MouseDoubleClick);
            this.buttonStoreAdd.Click += new System.EventHandler(this.buttonStore_Click);
        }
        private void FillComboBoxConsumeUnit(ConsumeUnit consumeunit)
        {
            try
            {

                comboBoxConsumeUnt.Items.Clear();
                int selected_index = 0;
                try
                {

                    List<ConsumeUnit> ConsumeUnitList = new ItemObj.ItemObjSQL.ConsumeUnitSql(DB).GetConsumeUnitList(_ItemIN. _Item);
                    for (int i = 0; i < ConsumeUnitList.Count; i++)
                    {
                        string consumeunit_name = "";
                        if (ConsumeUnitList[i].ConsumeUnitID == 0)
                            consumeunit_name = ConsumeUnitList[i].ConsumeUnitName;
                        else
                            consumeunit_name = ConsumeUnitList[i].ConsumeUnitName + " (" + ConsumeUnitList[i].Factor
                                + " " + _ItemIN._Item.DefaultConsumeUnit + ")";

                        ComboboxItem item = new ComboboxItem(consumeunit_name, ConsumeUnitList[i].ConsumeUnitID);
                        comboBoxConsumeUnt.Items.Add(item);
                        if (consumeunit != null && consumeunit.ConsumeUnitID == ConsumeUnitList[i].ConsumeUnitID) selected_index = i;
                    }
                    comboBoxConsumeUnt.SelectedIndex = selected_index;
                    comboBoxConsumeUnt.Enabled = true;
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("FillComboBoxConsumeUnit:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBoxConsumeUnit:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    uint placeid = Convert.ToUInt32(textBoxPlaceID.Text);
                    TradeStorePlace place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(placeid);
                    if (place != null)
                    {

                        _TempStorePlace = place;
                        textBoxPlaceInfo.Text = _TempStorePlace.GetPlaceInfo();
                        textBoxPlaceID.Text = _TempStorePlace.PlaceID.ToString();
                        buttonClearStoreData.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على مكان التخزين", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void FillComboBoxStoreConsumeUnit(ConsumeUnit consumeunit)
        {

            comboBoxConsumeUnt.Items.Clear();
            int selected_index = 0;
            try
            {

                List<ConsumeUnit> ConsumeUnitList = new ItemObj.ItemObjSQL.ConsumeUnitSql(DB).GetConsumeUnitList(_ItemIN . _Item);
                for (int i = 0; i < ConsumeUnitList.Count; i++)
                {
                    string consumeunit_name = "";
                    if (ConsumeUnitList[i].ConsumeUnitID == 0)
                        consumeunit_name = ConsumeUnitList[i].ConsumeUnitName;
                    else
                        consumeunit_name = ConsumeUnitList[i].ConsumeUnitName + " (" + ConsumeUnitList[i].Factor
                            + " " + _ItemIN._Item.DefaultConsumeUnit + ")";

                    ComboboxItem item = new ComboboxItem(consumeunit_name, ConsumeUnitList[i].ConsumeUnitID);

                    comboBoxConsumeUnt.Items.Add(item);
                    if (consumeunit != null && consumeunit.ConsumeUnitID == ConsumeUnitList[i].ConsumeUnitID) selected_index = i;
                }

                comboBoxConsumeUnt.SelectedIndex = selected_index;

            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("FillComboBoxStoreConsumeUnit:" + ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
        private void textBoxPlace_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TradeStoreContainer container = null;
                if (_TempStorePlace != null) container = _TempStorePlace._TradeStoreContainer;
                Trade.Forms.Container.User_ShowLocationsForm frm = new Trade.Forms.Container.User_ShowLocationsForm(DB, container, Trade.Forms.Container.User_ShowLocationsForm.SELECT_Place);
                DialogResult dd = frm.ShowDialog();

                if (dd == DialogResult.OK)
                {
                    _TempStorePlace = frm.ReturnPlace;
                    textBoxPlaceInfo.Text = _TempStorePlace.GetPlaceInfo();
                    textBoxPlaceID.Text = _TempStorePlace.PlaceID.ToString();
                    buttonClearStoreData.Visible = true;
                }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show( ee.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
          
        }
        private void buttonStore_Click(object sender, EventArgs e)
        {
            try
            {

                ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
                ConsumeUnit _ConsumeUnit = new ItemObj.ItemObjSQL.ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

                double store_amount;
                try
                {

                    store_amount = Convert.ToDouble(textBoxStoreAmount.Text);
                }
                catch
                {
                    MessageBox.Show("الكمية يجب ان تكون رقم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (_TempStorePlace == null)
                {
                    MessageBox.Show("يرجى تحديد مكان التخزين", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (buttonStoreAdd.Name == "buttonStoreAdd")
                {
                    bool success = new TradeItemStoreSQL(DB).Store_Item_INPlace(_TempStorePlace.PlaceID, _ItemIN.ItemINID, TradeItemStore.ITEMIN_STORE_TYPE, store_amount, _ConsumeUnit);
                    if (success)
                    {
                        buttonClearStoreData.PerformClick();
                        RefreshItemStorePlaces();
                        textBoxStoreAmount.Text = ""; textBoxPlaceID.Text = ""; textBoxPlaceInfo.Text = "";
                        buttonClearStoreData.Visible = false;
                    }
                }
                else
                {
                    bool success = new TradeItemStoreSQL(DB).UpdateItemAmountStored(_TempStorePlace.PlaceID, _ItemIN.ItemINID, TradeItemStore.ITEMIN_STORE_TYPE, store_amount, _ConsumeUnit);
                    if (success)
                    {
                        buttonClearStoreData.PerformClick();
                        RefreshItemStorePlaces();
                        textBoxStoreAmount.Text = ""; textBoxPlaceID.Text = ""; textBoxPlaceInfo.Text = "";
                        buttonClearStoreData.Visible = false;

                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonStore_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void RefreshItemStorePlaces()
        {

            try
            {
                listViewItemStorePlace.Items.Clear();
                List<TradeItemStore> TradeItemStoreList = new TradeItemStoreSQL(DB).Get_ItemIN_StoredPlaces(_ItemIN);
                AvailableItemSQL AvailableItemSQL_ = new AvailableItemSQL(DB);
                double nonsotred = _ItemIN.Amount - TradeItemStoreList.Sum(x => x.Amount * (x._ConsumeUnit.Factor / _ItemIN._ConsumeUnit.Factor));

                if (nonsotred > 0)
                {
                    panelStoreConfig.Enabled = true;
                    textBoxStoreAmount.Text = nonsotred.ToString();
                    ListViewItem ListViewItem_ = new ListViewItem();
                    ListViewItem_.Name = "null";
                    ListViewItem_.SubItems.Add("غير مخزن");
                    ListViewItem_.SubItems.Add("-");
                    ListViewItem_.SubItems.Add(_ItemIN._ConsumeUnit.ConsumeUnitName);
                    ListViewItem_.SubItems.Add(nonsotred.ToString());
                    ListViewItem_.SubItems.Add(AvailableItemSQL_.Get_SpentAmount_by_Place(_ItemIN, null).ToString());
                    ListViewItem_.BackColor = Color.Orange;
                    listViewItemStorePlace.Items.Add(ListViewItem_);
                }
                else panelStoreConfig.Enabled = false;

                for (int i = 0; i < TradeItemStoreList.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(TradeItemStoreList[i]._TradeStorePlace.PlaceID.ToString());
                    ListViewItem_.Name = TradeItemStoreList[i]._TradeStorePlace.PlaceID.ToString();
                    ListViewItem_.SubItems.Add(TradeItemStoreList[i]._TradeStorePlace.PlaceName);
                    ListViewItem_.SubItems.Add(new TradeStorePlaceSQL(DB).GetPlacePath( TradeItemStoreList[i]._TradeStorePlace));
                    ListViewItem_.SubItems.Add(TradeItemStoreList[i]._ConsumeUnit.ConsumeUnitName);
                    ListViewItem_.SubItems.Add(TradeItemStoreList[i].Amount.ToString());
                    ListViewItem_.SubItems.Add(AvailableItemSQL_.Get_SpentAmount_by_Place(_ItemIN, TradeItemStoreList[i]._TradeStorePlace).ToString());

                    ListViewItem_.BackColor = Color.LimeGreen;
                    listViewItemStorePlace.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshItemStorePlaces" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewItemStorePlace.SelectedItems.Count > 0)
            {
                if (FUNCTION == GET_PLACE_FUNCTION)
                    SetReturnPlace();
                else if (FUNCTION == STORE_CONFIG_FUNCTION)
                    OpenPlaceDetailsMenuItem.PerformClick();
                else return;



            }
        }
        private void SetReturnPlace()
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
                    MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void listView_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                listViewItemStorePlace.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                foreach (ListViewItem item1 in listViewItemStorePlace.Items)
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

                    uint placeid;
                    MenuItem[] mi;
                    try
                    {
                        placeid = Convert.ToUInt32(listitem.Name);
                        mi = new MenuItem[] { OpenPlaceDetailsMenuItem, EditStoreAMountMenuItem, UnStoreAmountMenuItem, new MenuItem("-") };
                    }
                    catch
                    {
                        //mi = new MenuItem[] { DisAssemblageMenuItem };
                        mi = null;
                    }

                    listViewItemStorePlace.ContextMenu = new ContextMenu(mi);


                }
                else
                {

                    //MenuItem[] mi = new MenuItem[] { AddBuyOprMenuItem };
                    //listViewItemStorePlace.ContextMenu = new ContextMenu(mi);

                }

            }
        }
        private void EditStoreAMount_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemStorePlace.SelectedItems.Count > 0)
                {
                    uint placeid;

                    placeid = Convert.ToUInt32(listViewItemStorePlace.SelectedItems[0].Name);


                    TradeStorePlace place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(placeid);
                    TradeItemStore TradeItemStore_ = new TradeItemStoreSQL(DB).GetTradeItemStoreINFO(place, _ItemIN.ItemINID, TradeItemStore.ITEMIN_STORE_TYPE);
                    buttonStoreAdd.Name = "buttonStoreEdit";
                    buttonStoreAdd.Text = "تعديل";
                    textBoxStoreAmount.Text = TradeItemStore_.Amount.ToString();
                    FillComboBoxStoreConsumeUnit(TradeItemStore_._ConsumeUnit);
                    _TempStorePlace = place;
                    textBoxPlaceInfo.Text = _TempStorePlace.GetPlaceInfo();
                    textBoxPlaceID.Text = _TempStorePlace.PlaceID.ToString();
                    buttonClearStoreData.Visible = true;
                    panelStoreAmount.Enabled = true;


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditStoreAMount_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void UnStoreAmount_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemStorePlace.SelectedItems.Count > 0)
                {
                    uint placeid;
                    try
                    {
                        placeid = Convert.ToUInt32(listViewItemStorePlace.SelectedItems[0].Name);
                    }
                    catch
                    {
                        return;
                    }
                    DialogResult dd = MessageBox.Show("متأكد من الغاء التخزين", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd == DialogResult.OK)
                    {
                        TradeStorePlace place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(placeid);
                        bool success = new TradeItemStoreSQL(DB).UNStore_Item_INPlace(place.PlaceID, _ItemIN.ItemINID, TradeItemStore.ITEMIN_STORE_TYPE);
                        if (success) RefreshItemStorePlaces();
                    }



                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("UnStoreAmount_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //private void DisAssemblage_MenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TradeStorePlace place;
        //        try
        //        {

        //            uint placeid = Convert.ToUInt32(listViewItemStorePlace.SelectedItems[0].Name);
        //            place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(placeid);
        //        }
        //        catch
        //        {
        //            place = null;
        //        }

        //        DisAssemblageForm DisAssemblageForm_ = new DisAssemblageForm(DB, _ItemIN, place);
        //        DisAssemblageForm_.ShowDialog();
        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show("DisAssemblage_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //}
        private void OpenPlaceDetails_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemStorePlace.SelectedItems.Count > 0)
                {
                    uint placeid;
                    try
                    {
                        placeid = Convert.ToUInt32(listViewItemStorePlace.SelectedItems[0].Name);
                    }
                    catch
                    {
                        return;
                    }
                    TradeStorePlace place = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(placeid);

                    Container.Place_ExistsItems_Form PlaceItemsForm_ = new Forms.Container.Place_ExistsItems_Form(DB, place, false);
                    PlaceItemsForm_.ShowDialog();


                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("OpenPlaceDetails_MenuItem_Click:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void buttonClearStoreData_Click(object sender, EventArgs e)
        {
            _TempStorePlace = null;
            textBoxPlaceInfo.Text = "";
            textBoxPlaceID.Text = "";
            buttonStoreAdd.Name = "buttonStoreAdd";
            buttonStoreAdd.Text = "أضف";
            buttonClearStoreData.Visible = false;
            panelStoreAmount.Enabled = true;
            RefreshItemStorePlaces();
        }
    }
}
