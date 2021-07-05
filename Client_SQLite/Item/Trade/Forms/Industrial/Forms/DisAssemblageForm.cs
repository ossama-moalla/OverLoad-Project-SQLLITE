using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
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
    public partial class DisAssemblageForm : Form
    {


        DisAssemblabgeOPR _DisAssemblabgeOPR;
        DatabaseInterface DB;

        public bool _Changed;
        public bool Changed
        {
            get { return _Changed; }
        }

        System.Windows.Forms.MenuItem OpenItemINMenuItem;
        System.Windows.Forms.MenuItem AddItemINMenuItem;
        System.Windows.Forms.MenuItem EditItemINMenuItem;
        System.Windows.Forms.MenuItem DeleteItemINMenuItem;

        List<ItemIN_ItemOUTReport   > ItemIN_ItemOUTReportList = new List<ItemIN_ItemOUTReport>();
        public DisAssemblageForm(DatabaseInterface db)
        {
            DB = db;
            InitializeComponent();


            _Changed = false;
            dateTimePicker_.Value = DateTime.Now;

            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء عملية تفكيك";
            panelItemOUT.Enabled = false;
            InitializeMenuItems();
            AdjustListViewColumnsWidth();


        }
        public DisAssemblageForm(DatabaseInterface db, DisAssemblabgeOPR DisAssemblabgeOPR_, bool edit)
        {
            InitializeComponent();
            DB = db;

            _DisAssemblabgeOPR = DisAssemblabgeOPR_;

            _Changed = false;

            InitializeMenuItems();

            loadForm(edit);
            AdjustListViewColumnsWidth();


        }
        private void LoadItemOUTData(bool Edit)
        {
            try
            {
                if (_DisAssemblabgeOPR == null) return;
                if (_DisAssemblabgeOPR._ItemOUT==null )
                {
  
                    buttonItemOUT_Select.Name = "buttonItemOUT_Select";
                    buttonItemOUT_Select.Text = "تحديد";
                    if (Edit)
                    {
                        buttonItemOUT_Select.Visible = true;
                        buttonItemOUT_Edit.Visible = false;
                        buttonItemOUT_Delete.Visible = false;
                    }
                    else
                    {
                        buttonItemOUT_Select.Visible = false;
                        buttonItemOUT_Edit.Visible = false;
                        buttonItemOUT_Delete.Visible = false;
                    }
                    textBoxItemID.Text = "";
                    textBoxItemName.Text = "";
                    textBoxItemCompany.Text = "";
                    textBoxItemType.Text = "";

                    textBoxcost.Text = "";
                    textBoxAvailableAmount.Text = "";
                    textBoxCurrency.Text = "";
                    textboxEchangeRate.Text = "";

                    textBoxItemINID.Text = "";
                    textBoxOperationID.Text = "";
                    textBoxOperationType.Text = "";
                    textBoxAmount.Text = "";
                    textBoxTradestate.Text = "";
                    textBoxConsumeUnit.Text = "";
                    textBoxPlaceID.Text = "";
                    textBoxPlaceName.Text = "";
                    textBoxPlacePath.Text = "";



                }
                else
                {
    
                    buttonItemOUT_Select.Name = "buttonItemOUT_Open";
                    buttonItemOUT_Select.Text = "استعراض";
                    if (Edit)
                    {
                        buttonItemOUT_Select.Visible = true;
                        buttonItemOUT_Edit.Visible = true;
                        buttonItemOUT_Delete.Visible = true;
                    }
                    else
                    {
                        buttonItemOUT_Select.Visible = true;
                        buttonItemOUT_Edit.Visible = false;
                        buttonItemOUT_Delete.Visible = false;
                    }

                    textBoxItemID.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._Item.ItemID.ToString();
                    textBoxItemName.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._Item.ItemName;
                    textBoxItemCompany.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._Item.ItemCompany;
                    textBoxItemType.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._Item.folder.FolderName;

                    textBoxcost.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._INCost.Value.ToString();
                    textBoxAvailableAmount.Text = new AvailableItemSQL(DB).Get_AvailabeAmount_by_ItemIN(_DisAssemblabgeOPR._ItemOUT._ItemIN).ToString();
                    textBoxCurrency.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._INCost._Currency.CurrencyName;
                    textboxEchangeRate.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._INCost.ExchangeRate.ToString();

                    textBoxItemINID.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN.ItemINID.ToString();
                    textBoxOperationID.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._Operation.OperationID.ToString();
                    textBoxOperationType.Text = Operation.GetOperationName(_DisAssemblabgeOPR._ItemOUT._ItemIN._Operation.OperationType);
                    textBoxAmount.Text = _DisAssemblabgeOPR._ItemOUT.Amount.ToString();
                    textBoxTradestate.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._TradeState.TradeStateName;

                    if (_DisAssemblabgeOPR._ItemOUT._ConsumeUnit == null) textBoxConsumeUnit.Text = _DisAssemblabgeOPR._ItemOUT._ItemIN._Item.DefaultConsumeUnit;
                    else textBoxConsumeUnit.Text = _DisAssemblabgeOPR._ItemOUT._ConsumeUnit.ConsumeUnitName;

                    if (_DisAssemblabgeOPR._ItemOUT.Place == null)
                    {
                        textBoxPlaceID.Text = "-";
                        textBoxPlaceName.Text = "مخرج من المواد غير المخزنة";
                        textBoxPlacePath.Text = "-";
                    }
                    else
                    {
                        textBoxPlaceID.Text = _DisAssemblabgeOPR._ItemOUT.Place.PlaceID.ToString();
                        textBoxPlaceName.Text = _DisAssemblabgeOPR._ItemOUT.Place.PlaceName;
                        textBoxPlacePath.Text = new TradeStorePlaceSQL(DB).GetPlacePath(_DisAssemblabgeOPR._ItemOUT.Place);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadItemOUTData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        public void InitializeMenuItems()
        {
            OpenItemINMenuItem = new System.Windows.Forms.MenuItem("استعراض تفاصيل", OpenItemIN_MenuItem_Click);

            AddItemINMenuItem = new System.Windows.Forms.MenuItem("ادخال عنصر جديد", AddItemIN_MenuItem_Click);
            EditItemINMenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditItemIN_MenuItem_Click);
            DeleteItemINMenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteItemIN_MenuItem_Click); ;

        }

        private void DeleteItemIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                string iteminid_Str = listView.SelectedItems[0].Name;
                uint iteminid = Convert.ToUInt32(iteminid_Str);
                bool Success = new ItemINSQL(DB).DeleteItemIN(iteminid);
                if (Success)
                {
                    this._Changed = true;
                    ItemIN_ItemOUTReportList = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(_DisAssemblabgeOPR._Operation);
                    RefreshBuyOperations();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteItemIN_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void EditItemIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count > 0)
                {
                    uint iteminid = Convert.ToUInt32(listView.SelectedItems[0].Name);
                    ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(iteminid);
                    ItemINForm ItemINForm_ = new ItemINForm(DB, itemin, true);
                    ItemINForm_.ShowDialog();
                    if (ItemINForm_.Changed)
                    {
                        this._Changed = true;
                        ItemIN_ItemOUTReportList = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(_DisAssemblabgeOPR._Operation);
                        RefreshBuyOperations();
                    }
                    ItemINForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditItemIN_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void OpenItemIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count > 0)
                {
                    uint iteminid = Convert.ToUInt32(listView.SelectedItems[0].Name);
                    ItemIN itemin = new ItemINSQL(DB).GetItemININFO_BYID(iteminid);
                    ItemINForm ItemINForm_ = new ItemINForm(DB, itemin, false);
                    ItemINForm_.ShowDialog();
                    if (ItemINForm_.Changed)
                    {
                        this._Changed = true;
                        ItemIN_ItemOUTReportList = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(_DisAssemblabgeOPR._Operation);
                        RefreshBuyOperations();
                    }
                    ItemINForm_.Dispose();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemIN_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void AddItemIN_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_DisAssemblabgeOPR._ItemOUT == null) throw new Exception("يجب أولا ادخال العنصر المفكك");
                ItemINForm ItemINForm_ = new ItemINForm(DB, _DisAssemblabgeOPR._Operation);
                DialogResult d = ItemINForm_.ShowDialog();
                if (ItemINForm_.Changed)
                {
                    this._Changed = true;
                    ItemIN_ItemOUTReportList = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(_DisAssemblabgeOPR._Operation);
                    RefreshBuyOperations();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddItemIN_MenuItem_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
           
        }
        private void RefreshBuyOperations()
        {
            try
            {
                listView.Items.Clear();
                if (_DisAssemblabgeOPR == null) return;





                double totalcost = 0;

                for (int i = 0; i < ItemIN_ItemOUTReportList.Count; i++)
                {

                    double buyprice = System.Math.Round(ItemIN_ItemOUTReportList[i]._ItemIN._INCost.Value, 2);
                    double total_buyprice = System.Math.Round(buyprice * ItemIN_ItemOUTReportList[i]._ItemIN.Amount, 3);
                    totalcost = totalcost + total_buyprice;
                    ListViewItem ListViewItem_ = new ListViewItem((listView.Items.Count + 1).ToString());
                    //ListViewItem_.UseItemStyleForSubItems = false;
                    ListViewItem_.Name = ItemIN_ItemOUTReportList[i]._ItemIN.ItemINID.ToString();
                    ListViewItem_.SubItems.Add(ItemIN_ItemOUTReportList[i]._ItemIN._Item.ItemName);
                    ListViewItem_.SubItems.Add(ItemIN_ItemOUTReportList[i]._ItemIN._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(ItemIN_ItemOUTReportList[i]._ItemIN._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(ItemIN_ItemOUTReportList[i]._ItemIN._TradeState.TradeStateName);
                    ListViewItem_.SubItems.Add(ItemIN_ItemOUTReportList[i]._ItemIN.Amount.ToString());
                    ListViewItem_.SubItems.Add(ItemIN_ItemOUTReportList[i]._ItemIN._ConsumeUnit.ConsumeUnitName.ToString());
                    ListViewItem_.SubItems.Add(buyprice.ToString() + " " + _DisAssemblabgeOPR._ItemOUT._ItemIN._INCost._Currency.CurrencySymbol.Replace(" ", string.Empty));
                    ListViewItem_.SubItems.Add((total_buyprice).ToString() + " " + _DisAssemblabgeOPR._ItemOUT._ItemIN._INCost._Currency.CurrencySymbol.Replace(" ", string.Empty));
                    if (checkBoxShowDetails.Checked && listView.Name == "listViewDetails")
                    {
                        double outamount = GetOUTAmount(ItemIN_ItemOUTReportList[i]);
                        if (outamount == 0)
                        {
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                        }
                        else
                        {
                            ListViewItem_.SubItems.Add(outamount + ItemIN_ItemOUTReportList[i]._ItemIN._ConsumeUnit.ConsumeUnitName);
                            ListViewItem_.SubItems.Add(GetItemOUTsCost(ItemIN_ItemOUTReportList[i].ItemOUTList));
                        }
                        if (outamount == ItemIN_ItemOUTReportList[i]._ItemIN.Amount) ListViewItem_.BackColor = Color.Orange;
                        else
                            ListViewItem_.BackColor = Color.LimeGreen;
                    }
                    else ListViewItem_.BackColor = Color.LimeGreen;


                    listView.Items.Add(ListViewItem_);

                }
                textBoxtotalvalue.Text = totalcost.ToString();

            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshBuyOperations:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private string  GetItemOUTsCost(List <ItemOUT   > ItemOUTList)
        {
            try
            {
                if (ItemOUTList.Count == 0) return "-";
                string outcost = "";


                List<Currency> currencyList = ItemOUTList.Select(x => new OperationSQL(DB).GetOperationItemOUTCurrency(x._Operation)).Distinct().ToList();

                for (int i = 0; i < currencyList.Count; i++)
                {
                    List<ItemOUT> tempoutlist = ItemOUTList.Where(x => new OperationSQL(DB).GetOperationItemOUTCurrency(x._Operation) == currencyList[i]).ToList();
                    for (int j = 0; j < tempoutlist.Count; j++)
                    {
                        //outcost = System.Math.Round((tempoutlist[j].Cost  * tempoutlist[j].Amount), 3) + currencyList[i].CurrencySymbol + " ";
                    }
                }


                return outcost;
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetItemOUTsCost:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        

        }

        private double GetOUTAmount(ItemIN_ItemOUTReport ItemIN_ItemOUTReport_)
        {
            try
            {
                if (ItemIN_ItemOUTReport_.ItemOUTList.Count == 0) return 0;
                double amount = 0;

                for (int j = 0; j < ItemIN_ItemOUTReport_.ItemOUTList.Count; j++)
                {

                    amount = amount + System.Math.Round((ItemIN_ItemOUTReport_.ItemOUTList[j].Amount * (ItemIN_ItemOUTReport_.ItemOUTList[j]._ConsumeUnit.Factor / ItemIN_ItemOUTReport_._ItemIN._ConsumeUnit.Factor)), 3);

                }

                return amount;
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetOUTAmount:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
   
        }
          public void loadForm(bool edit)
        {


            try
            {
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                panelItemOUT.Enabled = true;
                if (_DisAssemblabgeOPR != null)
                {
                    LoadItemOUTData(edit );
                    ItemIN_ItemOUTReportList = new ItemINSQL(DB).GetItemIN_ItemOUTReport_List(_DisAssemblabgeOPR._Operation);
                   

                    dateTimePicker_.Value = _DisAssemblabgeOPR.OprDate;

                    TextboxNotes .Text = _DisAssemblabgeOPR.Notes;
                    labeldisassemplyinfo.Text = "عملية تفكيك رقم:" + _DisAssemblabgeOPR._Operation.OperationID.ToString();

                    textBoxOprDesc.Text = _DisAssemblabgeOPR.OprDesc;
                    textBoxDisAssemblageID.Text = _DisAssemblabgeOPR._Operation.OperationID.ToString();
                    RefreshBuyOperations();

                    if (edit)
                    {
                       
                        TextboxNotes.ReadOnly = false;
                        textBoxOprDesc.ReadOnly = false ;
                        dateTimePicker_.Enabled = true;
                        this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
                        this.listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);

                    }
                    else
                    {
    

                        textBoxOprDesc.ReadOnly = true ;

                        TextboxNotes.ReadOnly = true;


                        dateTimePicker_.Enabled = false;




                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("loadForm:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonSave.Name == "buttonAdd")
                {
                    DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).CreateDisAssemblageOPR
                        (dateTimePicker_.Value, textBoxOprDesc.Text, TextboxNotes.Text);
                    if (DisAssemblabgeOPR_ != null)
                    {
                        _DisAssemblabgeOPR = DisAssemblabgeOPR_;

                        MessageBox.Show("تم انشاء عملية التفكيك بنجاح ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this._Changed = true;
                        loadForm(true );

                    }


                }

                else
                {
                    if (_DisAssemblabgeOPR != null)
                    {


                        bool success = new DisAssemblageSQL(DB).UpdateDisAssemblageOPR
                            (_DisAssemblabgeOPR._Operation.OperationID, dateTimePicker_.Value, textBoxOprDesc.Text, TextboxNotes.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _DisAssemblabgeOPR = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_DisAssemblabgeOPR._Operation.OperationID);
                            this._Changed = true;
                            loadForm(true );
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView.SelectedItems.Count > 0)
            {
                OpenItemINMenuItem.PerformClick();
            }
        }
        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            try
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


                        MenuItem[] mi1 = new MenuItem[] { OpenItemINMenuItem, EditItemINMenuItem, DeleteItemINMenuItem, new MenuItem("-"), AddItemINMenuItem };
                        listView.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddItemINMenuItem };
                        listView.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }
        private void listView_Resize(object sender, EventArgs e)
        {
            AdjustListViewColumnsWidth();
        }
        public void AdjustListViewColumnsWidth()
        {
            listView.Columns[0].Width = 80;
            
            for(int i=1;i<listView .Columns .Count;i++)
              listView.Columns[i].Width = ((listView.Width - 80) / (listView.Columns.Count-1 ))-1;
        }  

        private void عرضالعناصرالخارجةمنالعناصرالداخلةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationItemsIN_ItemOutListForm OperationItemsIN_ItemOutListForm_ = new OperationItemsIN_ItemOutListForm(DB, _DisAssemblabgeOPR ._Operation);
            OperationItemsIN_ItemOutListForm_.ShowDialog();
        }
        private void buttonItemOUT_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (_DisAssemblabgeOPR ._ItemOUT == null)
                {
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _DisAssemblabgeOPR._Operation);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        _DisAssemblabgeOPR = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_DisAssemblabgeOPR._Operation.OperationID);
                        loadForm (true);
                    }
                }
                else
                {
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _DisAssemblabgeOPR._ItemOUT, false);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        _DisAssemblabgeOPR = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_DisAssemblabgeOPR._Operation.OperationID);
                        loadForm(true);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonItemOUT_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (_DisAssemblabgeOPR._ItemOUT == null)
                {
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _DisAssemblabgeOPR._Operation);
                    ItemOUTForm_.ShowDialog ();
                    if (ItemOUTForm_.Changed)
                    {
                        _DisAssemblabgeOPR = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(_DisAssemblabgeOPR._Operation.OperationID);
                        loadForm(true);
                    }
                }
                else
                {
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _DisAssemblabgeOPR._ItemOUT, true);
                    ItemOUTForm_.Show();
                    if (ItemOUTForm_.Changed)
                    {
                        _DisAssemblabgeOPR = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID (_DisAssemblabgeOPR._Operation.OperationID);
                        loadForm(true);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonItemOUT_Delete_Click(object sender, EventArgs e)
        {
            try
            {

                if (_DisAssemblabgeOPR._ItemOUT != null)
                {
                    DialogResult dd = MessageBox.Show("هل انت متأكد من الحذف؟","",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning  );
                    if (dd != DialogResult.OK) return;
                    if (new ItemINSQL(DB).GetItemINList(_DisAssemblabgeOPR._Operation).Count > 0) throw new Exception("لا يمكن الحذف قبل حذف جميع العناصر المدخلة عن طريق عملية التفكيك");
                    bool success = new ItemOUTSQL(DB).DeleteItemOUT(_DisAssemblabgeOPR._ItemOUT.ItemOUTID);
                    if (success)
                    {
                        _DisAssemblabgeOPR = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID (_DisAssemblabgeOPR._Operation.OperationID);
                        loadForm(true);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
