using OverLoad_Client.ItemObj.Objects;
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

namespace OverLoad_Client.Maintenance.Forms
{
    public partial class MaintenanceAccessoryForm : Form
    {

        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        MaintenanceOPR_Accessory _MaintenanceOPR_Accessory;
        Folder LastUsedFolder;
        Item _Item;
        TradeStorePlace _Place;
        private bool Changed_;
        public bool Changed
        {
            get { return Changed_; }
        }
        public MaintenanceAccessoryForm(DatabaseInterface db,MaintenanceOPR MaintenanceOPR_)
        {
            _MaintenanceOPR = MaintenanceOPR_;
            InitializeComponent();
            textBoxContact.Text = _MaintenanceOPR._Contact.Get_Complete_ContactName_WithHeader();
            textBoxMOPR.Text = _MaintenanceOPR._Operation.OperationID.ToString();
            dateTimePickerEntryDate.Value = _MaintenanceOPR.EntryDate ;

            DB = db;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء";
            this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
            this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
            this.textBoxPlaceID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.textBoxPlaceID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPlace_MouseDoubleClick);

        }
        public MaintenanceAccessoryForm(DatabaseInterface db, MaintenanceOPR_Accessory MaintenanceOPR_Accessory_,bool Edit)
        {
            DB = db;
            _MaintenanceOPR_Accessory = MaintenanceOPR_Accessory_;
            _MaintenanceOPR = _MaintenanceOPR_Accessory._MaintenanceOPR;
            InitializeComponent();
            LoadForm(Edit);
        }
        public void LoadForm(bool Edit)
        {
            try
            {
                if (_MaintenanceOPR_Accessory == null) return;
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                _Item = _MaintenanceOPR_Accessory._Item;
                _Place = _MaintenanceOPR_Accessory.Place;
                textBoxItemSerial.Text = _MaintenanceOPR_Accessory.ItemSerialNumber;
                textBoxNotes.Text = _MaintenanceOPR_Accessory.Notes;
                textBoxContact.Text = _MaintenanceOPR._Contact.Get_Complete_ContactName_WithHeader();
                textBoxMOPR.Text = _MaintenanceOPR._Operation.OperationID.ToString();
                dateTimePickerEntryDate.Value = _MaintenanceOPR.EntryDate ;
                LoadItemData();

               
                if (_MaintenanceOPR_Accessory.Place != null)
                {
                    
                    textBoxPlaceID.Text = _MaintenanceOPR_Accessory.Place.PlaceID.ToString();
                    textBoxPlaceID.Name = _MaintenanceOPR_Accessory.Place.PlaceID.ToString();
                    textBoxPlaceInfo.Text = new TradeStoreContainerSQL(DB).GetContainerPath(_MaintenanceOPR_Accessory.Place ._TradeStoreContainer) + "\\" + _MaintenanceOPR_Accessory.Place ._TradeStoreContainer.ContainerName + "\\" + _MaintenanceOPR_Accessory.Place .PlaceName;

                }



                if (Edit)
                {
                    if(_Place !=null)
                        buttonClearstoreinfo.Visible = true;
                    else
                        buttonClearstoreinfo.Visible = false;
                    this.textBoxItemID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemID_KeyDown);
                    this.textBoxItemID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxItem_MouseDoubleClick);
                    this.textBoxPlaceID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
                    this.textBoxPlaceID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPlace_MouseDoubleClick);
                    buttonSave.Visible = true;
                    textBoxItemID.ReadOnly = false;
                    textBoxItemSerial.ReadOnly = false;
                    textBoxNotes.ReadOnly = false;
                    textBoxPlaceID.ReadOnly = false;


                }
                else
                {
                    buttonSave.Visible = false;

                    textBoxItemID.ReadOnly = true;
                    textBoxItemSerial.ReadOnly = true;
                    textBoxNotes.ReadOnly = true;
                    textBoxPlaceID.ReadOnly = true;

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("حصل خطأ اثناء تحميل الصفحة:"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

        }
        private async void LoadItemData()
        {
            try
            {
                LastUsedFolder = _Item.folder;
                textBoxItemID.Text = _Item.ItemID.ToString();
                textBoxItemName.Text = _Item.ItemName;
                textBoxItemCompany.Text = _Item.ItemCompany;
                textBoxItemType.Text = _Item.folder.FolderName;
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadItemData:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           

        }
        private void textBoxItemID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    uint itemid = Convert.ToUInt32(textBoxItemID.Text);
                    Item item__ = new ItemObj.ItemObjSQL.ItemSQL(DB).GetItemInfoByID(itemid);
                    if (item__ != null)
                    {
                        _Item = item__;
                        LoadItemData();
                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على العنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
        private void textBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ItemObj.Forms.User_ShowItemsForm SelectItem_ = new ItemObj.Forms.User_ShowItemsForm(DB, null, ItemObj.Forms.User_ShowItemsForm.SELECT_ITEM);
                    DialogResult dd = SelectItem_.ShowDialog();
                    if (dd == DialogResult.OK)
                    {
                        _Item = SelectItem_.ReturnItem;
                        LoadItemData();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxItem_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_MaintenanceOPR  == null) return;
            //ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
            //ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

            //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
            //string SellType_ = ComboboxItem_selltype.Text;
            
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    if (_Item == null)
                    {

                        MessageBox.Show("يرجى تحديد المادة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                   
                    MaintenanceOPR_Accessory MaintenanceOPR_Accessory_ =
                        new MaintenanceSQL.MaintenanceAccessorySQL(DB).AddAccessory 
                        (_MaintenanceOPR ._Operation.OperationID, _Item.ItemID, textBoxItemSerial.Text
                        , textBoxNotes.Text,_Place);
                    if (MaintenanceOPR_Accessory_ != null)
                    {
                        _MaintenanceOPR_Accessory  = MaintenanceOPR_Accessory_;
                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Changed_ = true;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر اضافة الملحق " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    if (_MaintenanceOPR_Accessory  != null)
                    {
                        
                        bool success = new MaintenanceSQL.MaintenanceAccessorySQL(DB).UpdateAccessory 
                            (_MaintenanceOPR_Accessory.AccessoryID, _Item.ItemID
                            , textBoxItemSerial.Text, textBoxNotes.Text,_Place);
                        if (success == true)
                        {
                            MessageBox.Show("تم حفظ  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _MaintenanceOPR_Accessory  = new MaintenanceSQL.MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(_MaintenanceOPR_Accessory.AccessoryID);
                            this.Changed_ = true;
                            this.Close();
                        }
                        else MessageBox.Show("لم يتم الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذرالحفظ  " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    uint placeid = Convert.ToUInt32(textBoxPlaceID.Text);
                    TradeStorePlace place = new Trade.TradeSQL.TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(placeid);
                    if (place != null)
                    {
                        buttonClearstoreinfo.Visible = true;
                        _Place = place;
                        textBoxPlaceInfo.Text = new TradeStoreContainerSQL(DB).GetContainerPath(_Place._TradeStoreContainer) + "\\" + _Place._TradeStoreContainer.ContainerName + "\\" + _Place.PlaceName;
                        textBoxPlaceID.Text = _Place.PlaceID.ToString();
                        textBoxPlaceID.Name = _Place.PlaceID.ToString();

                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على مكان التخزين", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("يرجى ادخال عدد صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void textBoxPlace_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TradeStoreContainer container;
                try
                {
                    container = new Trade.TradeSQL.TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID(Convert.ToUInt32(textBoxPlaceID.Name))._TradeStoreContainer;
                }
                catch
                {
                    container = null;
                }

                Trade.Forms.Container.User_ShowLocationsForm frm = new Trade.Forms.Container.User_ShowLocationsForm(DB, container, Trade.Forms.Container.User_ShowLocationsForm.SELECT_Place);
                DialogResult dd = frm.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    TradeStorePlace place = frm.ReturnPlace;
                    buttonClearstoreinfo.Visible = true;
                    _Place = place;
                    textBoxPlaceInfo.Text = new TradeStoreContainerSQL(DB).GetContainerPath(_Place._TradeStoreContainer) + "\\" + _Place._TradeStoreContainer.ContainerName + "\\" + _Place.PlaceName;

                    textBoxPlaceID.Text = _Place.PlaceID.ToString();
                    textBoxPlaceID.Name = _Place.PlaceID.ToString();

                }
                frm.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxPlace_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void buttonClearstoreinfo_Click(object sender, EventArgs e)
        {
            buttonClearstoreinfo.Visible = false ;
            _Place = null;
            textBoxPlaceInfo.Text = "";
            textBoxPlaceID.Text = "";
        }


    }
}
