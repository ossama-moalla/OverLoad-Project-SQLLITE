using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Forms;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Company.Objects;
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
    public partial class RavageOPR_Form : Form
    {
        public const int OPERATION_RAVAGE = 0;
        public const int OPERATION_CONSUME = 1;

        DatabaseInterface DB;
        RavageOPR _RavageOPR;
        public bool _Changed;
        public bool Changed
        {
            get { return _Changed; }
        }
        Part _Part;

        System.Windows.Forms.MenuItem OpenItemOUTMenuItem;
        System.Windows.Forms.MenuItem AddItemOUTMenuItem;
        System.Windows.Forms.MenuItem EditItemOUTMenuItem;
        System.Windows.Forms.MenuItem DeleteItemOUTMenuItem;

        List<ItemOUT > _ItemOUTList = new List<ItemOUT>();

        public RavageOPR_Form(DatabaseInterface db,DateTime OPRDate,uint function)
        {
            DB = db;
            InitializeComponent();
            if (function == OPERATION_CONSUME)
            {
                this.textBoxPartID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPartID_KeyDown);
                this.textBoxPartID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPartID_MouseDoubleClick);

                labelOprINFO.Text = "انشاء عملية ترحيل مستهلكات";
                panelPart.Visible = true;
            }
            else if (function == OPERATION_RAVAGE)
            {
                labelOprINFO.Text = "انشاء عملية اتلاف";
                panelPart.Visible = false;
                label_Part_hint.Visible = false;

            }
            else throw new Exception("نمط تشغيل غير صحيح");

            panelSellOPRs.Enabled = false;
            textboxOPRID.Text  = " - ";
          _Changed = false;
            dateTimePicker_.Value = OPRDate;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text  = "انشاء ";
            InitializeMenuItems();


        }
        public RavageOPR_Form(DatabaseInterface db, RavageOPR RavageOPR_, bool edit)
        {
            try
            {
                InitializeComponent();

                DB = db;
                _RavageOPR = RavageOPR_;
                _Changed = false;
                _Part = RavageOPR_ ._Part;
                InitializeMenuItems();
                loadForm(edit);
            }catch(Exception ee)
            {
                MessageBox.Show(ee.Message );
            }
         


        }
         public void InitializeMenuItems()
        {
            OpenItemOUTMenuItem  = new System.Windows.Forms.MenuItem("فتح تفاصيل ", OpenItemOUT_MenuItem_Click);
            AddItemOUTMenuItem  = new System.Windows.Forms.MenuItem("اضافة مادة", AddItemOUT_MenuItem_Click);
            EditItemOUTMenuItem  = new System.Windows.Forms.MenuItem("تعديل ", EditItemOUT_MenuItem_Click);
            DeleteItemOUTMenuItem  = new System.Windows.Forms.MenuItem("حذف", DeleteItemOUT_MenuItem_Click); ;

        }



        private void RefreshRavageOperations(List <ItemOUT > ItemOUTList)
        {
            try
            {
                listViewItemOUT.Items.Clear();
                //ComboboxItem ComboboxItem_ = (ComboboxItem)comboBoxCurrency.SelectedItem;
                //Currency currency = new CurrencySQL(DB).GetCurrencyINFO_ByID(ComboboxItem_.Value);


                for (int i = 0; i < ItemOUTList.Count; i++)
                {


                    ListViewItem ListViewItem_ = new ListViewItem((listViewItemOUT.Items.Count + 1).ToString());
                    ListViewItem_.Name = ItemOUTList[i].ItemOUTID.ToString();
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ItemIN._TradeState.TradeStateName);
                    ListViewItem_.SubItems.Add(ItemOUTList[i].Amount.ToString());
                    ListViewItem_.SubItems.Add(ItemOUTList[i]._ConsumeUnit.ConsumeUnitName.ToString());

                    ListViewItem_.BackColor = Color.LimeGreen;
                    listViewItemOUT.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshRavageOperations:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

          


        }

  

     
        public void loadForm(bool edit)
        {
            try
            {
                if (_RavageOPR == null) return;
                buttonSave.Name = "buttonSave";
                buttonSave.Text = "حفظ";
                panelSellOPRs.Enabled = true;
                if (_RavageOPR._Part != null)
                {
                    labelOprINFO.Text = " عملية ترحيل مستهلكات";
                    panelPart.Visible = true;
                    textBoxPartID.Text = _RavageOPR._Part.PartID.ToString();
                    textBoxPartName.Text = _RavageOPR._Part.PartName;
                    textBoxPartPath.Text = new Company.CompanySQL.PartSQL(DB).GetPartPath(_RavageOPR._Part);
                    if (edit)
                    {
                        this.textBoxPartID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPartID_KeyDown);
                        this.textBoxPartID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPartID_MouseDoubleClick);
                        textBoxPartID.ReadOnly = false;
                    }
                    else
                    {
                        textBoxPartID.ReadOnly = true;
                    }
                }
                else if (_RavageOPR._Part == null)
                {
                    labelOprINFO.Text = " عملية اتلاف";
                    panelPart.Visible = false;
                    label_Part_hint.Visible = false;

                }
                panelSellOPRs.Enabled = true;

                if (_RavageOPR != null)
                {

                    _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RavageOPR._Operation);
                    RefreshRavageOperations(_ItemOUTList);
                    dateTimePicker_.Value = _RavageOPR.RavageOPRDate;

                    textboxOPRID.Text = _RavageOPR._Operation.OperationID.ToString();
                    TextboxNotes.Text = _RavageOPR.Notes;

                    if (edit)
                    {
                        this.listViewItemOUT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewItemOUT_MouseDoubleClick);
                        this.listViewItemOUT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewItemOUT_MouseDown);
                        dateTimePicker_.Enabled = true;
                        TextboxNotes.ReadOnly = false;
                        buttonSave.Visible = true;
                    }
                    else
                    {

                        dateTimePicker_.Enabled = false;
                        TextboxNotes.ReadOnly = true;
                        buttonSave.Visible = false;

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
                if (panelPart.Visible == true && _Part == null) throw new Exception("يرجى اختيار القسم");

                if (buttonSave.Name == "buttonAdd")
                {

                    RavageOPR RavageOPR = new RavageOPRSQL(DB).AddRavageOPR(dateTimePicker_.Value,
                        _Part, TextboxNotes.Text);
                    if (RavageOPR != null)
                    {
                        _RavageOPR = RavageOPR;

                        MessageBox.Show("تم الانشاء بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this._Changed = true;
                        this.textBoxPartID.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.textBoxPartID_KeyDown);
                        this.textBoxPartID.MouseDoubleClick -= new System.Windows.Forms.MouseEventHandler(this.textBoxPartID_MouseDoubleClick);
                        loadForm(true);

                    }
                }

                else
                {
                    if (_RavageOPR != null)
                    {

                        bool success = new RavageOPRSQL(DB).UpdateRavageOPR(_RavageOPR._Operation.OperationID, dateTimePicker_.Value
                            , _Part, TextboxNotes.Text);
                        if (success == true)
                        {
                            MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _RavageOPR = new RavageOPRSQL(DB).GetRavageOPR_INFO_BYID(_RavageOPR._Operation.OperationID);
                            this._Changed = true;
                            loadForm(true);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        #region ItemOUT
        private void DeleteItemOUT_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
                bool success = new ItemOUTSQL(DB).DeleteItemOUT(sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RavageOPR._Operation);
                    RefreshRavageOperations(_ItemOUTList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("DeleteItemOUT_MenuItem_Click:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void EditItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemOUT.SelectedItems.Count > 0)
                {
                    uint itemoutid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
                    ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, true);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RavageOPR._Operation);
                        RefreshRavageOperations(_ItemOUTList);
                    }
                    ItemOUTForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void OpenItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewItemOUT.SelectedItems.Count > 0)
                {

                    uint itemoutid = Convert.ToUInt32(listViewItemOUT.SelectedItems[0].Name);
                    ItemOUT ItemOUT_ = new ItemOUTSQL(DB).GetItemOUTINFO_BYID(itemoutid);
                    ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, ItemOUT_, false);
                    ItemOUTForm_.ShowDialog();
                    if (ItemOUTForm_.Changed)
                    {
                        _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RavageOPR._Operation);
                        RefreshRavageOperations(_ItemOUTList);
                    }
                    ItemOUTForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void AddItemOUT_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemOUTForm ItemOUTForm_ = new ItemOUTForm(DB, _RavageOPR._Operation);
                DialogResult d = ItemOUTForm_.ShowDialog();
                if (ItemOUTForm_.Changed)
                {
                    _ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_RavageOPR._Operation);
                    RefreshRavageOperations(_ItemOUTList);
                }
                ItemOUTForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddItemOUT_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }
        private void listViewItemOUT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewItemOUT.SelectedItems.Count > 0)
            {
                OpenItemOUTMenuItem .PerformClick();
            }
        }
        private void listViewItemOUT_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewItemOUT.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewItemOUT.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenItemOUTMenuItem, EditItemOUTMenuItem, DeleteItemOUTMenuItem, new MenuItem("-"), AddItemOUTMenuItem };
                        listViewItemOUT.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddItemOUTMenuItem };
                        listViewItemOUT.ContextMenu = new ContextMenu(mi);

                    }

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewItemOUT_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        #endregion

        private void textBoxPartID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {

                    uint partid = Convert.ToUInt32(textBoxPartID.Text);
                    Part Part_ = new Company.CompanySQL.PartSQL(DB).GetPartInfoByID(partid);
                    if (Part_ != null)
                    {
                        _Part = Part_;
                        LoadPartData();
                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على القسم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxPartID_KeyDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void textBoxPartID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Company.Forms.ShowPartsForm PartForm_ = new Company.Forms.ShowPartsForm(DB, null, Company.Forms.ShowPartsForm.SELECT_Part);
                    DialogResult dd = PartForm_.ShowDialog();
                    if (dd == DialogResult.OK)
                    {
                        _Part = PartForm_.ReturnPart;
                        LoadPartData();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxPartID_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public void LoadPartData()
        {
            try
            {
                textBoxPartID.Text = _Part.PartID.ToString();
                textBoxPartName.Text = _Part.PartName;
                textBoxPartPath.Text = new Company.CompanySQL.PartSQL(DB).GetPartPath(_Part);
            }
            catch (Exception ee)
            {
                MessageBox.Show("LoadPartData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
   
        }

    }
}
