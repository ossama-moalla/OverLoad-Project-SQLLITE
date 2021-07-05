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
    public partial class IndustrialForm : Form
    {
        DatabaseInterface DB;

        System.Windows.Forms.MenuItem OpenOperation_MenuItem;
        System.Windows.Forms.MenuItem CreateAssemblyOPR_MenuItem;
        System.Windows.Forms.MenuItem CreateDisAssemblyOPR_MenuItem;
        System.Windows.Forms.MenuItem EditOperation_MenuItem;
        System.Windows.Forms.MenuItem DeleteOperation_MenuItem;

        List<Industrial_OPR> Industrial_OPRList = new List<Industrial_OPR>();
        public IndustrialForm(DatabaseInterface db)
        {
            DB = db;
            InitializeComponent();

            OpenOperation_MenuItem  = new System.Windows.Forms.MenuItem("فتح تفاصيل عملية البيع", OpenOperation_MenuItem_Click);

            CreateAssemblyOPR_MenuItem = new System.Windows.Forms.MenuItem("اضافة عملية تجميع", CreateAssemblyOPR_MenuItem_Click);
            CreateDisAssemblyOPR_MenuItem = new System.Windows.Forms.MenuItem("اضافة عملية تفكيك", CreateDisAssemblyOPR_MenuItem_Click);

            EditOperation_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditOperation_MenuItem_Click);
            DeleteOperation_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteOperation_MenuItem_Click); ;
            comboBoxOperationType.Items.Add(new ComboboxItem("الكل",0));
            comboBoxOperationType.Items.Add(new ComboboxItem("تفكيك", Operation.DISASSEMBLAGE ));
            comboBoxOperationType.Items.Add (new ComboboxItem("تجميع", Operation.ASSEMBLAGE));

            Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
            RefreshOperations();
            //this.listView.Resize += new System.EventHandler(this.listView_Resize);
            //ConfigureListviewColumnSize();
        }



        private void RefreshOperations()
        {
            try
            {
                listView.Items.Clear();
                for(int i=0;i< Industrial_OPRList.Count;i++)
                {
                    if (comboBoxOperationType.SelectedIndex > 0)
                    {
                        uint opr_type = ((ComboboxItem)comboBoxOperationType.SelectedItem).Value;
                        if (Industrial_OPRList[i]._Operation.OperationType != opr_type)
                            continue; 
                    }
                    ListViewItem ListViewItem_ = new ListViewItem(Industrial_OPRList[i]._Operation.OperationType == Operation.ASSEMBLAGE ? "تجميع" : "تفكيك");
                    ListViewItem_.Name = (Industrial_OPRList[i]._Operation.OperationType == Operation.ASSEMBLAGE ? "A" : "D") + Industrial_OPRList[i]._Operation.OperationID.ToString();
                    ListViewItem_.SubItems.Add(Industrial_OPRList[i]._Operation.OperationID.ToString());
                    ListViewItem_.SubItems.Add(Industrial_OPRList[i].OPR_Date.ToShortDateString());
                    ListViewItem_.SubItems.Add(Industrial_OPRList[i].OprDesc);


                        if (Industrial_OPRList[i]._Item  !=null )
                        {
                            if (comboBoxCompanyFilter.SelectedIndex > 0 && Industrial_OPRList[i]._Item.ItemCompany != comboBoxCompanyFilter.SelectedItem.ToString()) continue;
                            if (comboBoxFolderFilter.SelectedIndex > 0 && Industrial_OPRList[i]._Item.folder.FolderName != comboBoxFolderFilter.SelectedItem.ToString()) continue;
                            if (comboBoxItemilter.SelectedIndex > 0 && Industrial_OPRList[i]._Item.ItemName != comboBoxItemilter.SelectedItem.ToString()) continue;
                            if (comboBoxOprStatus.SelectedIndex > 0 && Industrial_OPRList[i].OPRStatus != comboBoxOprStatus.SelectedItem.ToString()) continue;


                            ListViewItem_.SubItems.Add(Industrial_OPRList[i]._Item.folder .FolderName);
                            ListViewItem_.SubItems.Add(Industrial_OPRList[i]._Item.ItemName);
                            ListViewItem_.SubItems.Add(Industrial_OPRList[i]._Item.ItemCompany);
                            ListViewItem_.SubItems.Add(Industrial_OPRList[i].TradeStateName );
                             ListViewItem_.SubItems.Add(Industrial_OPRList[i].Amount .ToString ());
                            ListViewItem_.SubItems.Add(Industrial_OPRList[i]._ConsumeUnit.ConsumeUnitName);
                            ListViewItem_.SubItems.Add(Math .Round ( Industrial_OPRList[i]._Money_Currency .Value,2).ToString ()+ Industrial_OPRList[i]._Money_Currency._Currency.CurrencySymbol);
                            ListViewItem_.SubItems.Add(Industrial_OPRList[i]._Money_Currency._Currency.CurrencyName );
                            ListViewItem_.SubItems.Add(Industrial_OPRList[i]._Money_Currency.ExchangeRate .ToString());

                            ListViewItem_.BackColor = Color.YellowGreen;
                        }
                        else
                        {
                        if (comboBoxCompanyFilter.SelectedIndex > 0 || comboBoxFolderFilter.SelectedIndex > 0 
                            || comboBoxItemilter.SelectedIndex > 0 || comboBoxOprStatus.SelectedIndex > 0 ) continue;


                        ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");
                            ListViewItem_.SubItems.Add("-");

                            ListViewItem_.BackColor = Color.Orange ;
                        }
                    ListViewItem_.SubItems.Add(Industrial_OPRList[i].OPRStatus.ToString());




                    listView.Items.Add(ListViewItem_);
                }
            }catch(Exception ee)
            {
                MessageBox.Show("فشل تحديث العمليات:"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listView.SelectedItems.Count > 0)
            {
                OpenOperation_MenuItem .PerformClick();
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


                        MenuItem[] mi1 = new MenuItem[] { OpenOperation_MenuItem, EditOperation_MenuItem, DeleteOperation_MenuItem
                        , new MenuItem("-"), CreateDisAssemblyOPR_MenuItem,CreateAssemblyOPR_MenuItem };
                        listView.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { CreateDisAssemblyOPR_MenuItem, CreateAssemblyOPR_MenuItem };
                        listView.ContextMenu = new ContextMenu(mi);

                    }

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("listView_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
        private void DeleteOperation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dd = MessageBox.Show("هل أنت متأكد من الحذف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                string s = listView .SelectedItems[0].Name;
                if (s.Substring(0, 1) == "A")
                {

                    uint assemblyid = Convert.ToUInt32(s.Substring(1));
                    bool Success = new AssemblageSQL (DB).DeleteDisAssemblageOPR (assemblyid);

                    if (Success)
                    {
                        Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                        RefreshOperations();
                    }

                }
                else 
                {

                    uint disssemblyid = Convert.ToUInt32(s.Substring(1));
                    bool Success = new DisAssemblageSQL(DB).DeleteDisAssemblageOPR(disssemblyid);
                    if (Success)
                    {
                        Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                        RefreshOperations();
                    }

                }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show("حذف عملية" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditOperation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string s = listView .SelectedItems[0].Name;
                if (s.Substring(0, 1) == "A")
                {
                    uint assemblyid = Convert.ToUInt32(s.Substring(1));
                    AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID (assemblyid );
                    Trade.Forms.TradeForms.AssemblageForm AssemblageForm_ = new Trade.Forms.TradeForms.AssemblageForm (DB, AssemblabgeOPR_, true);
                    AssemblageForm_.ShowDialog();
                    if (AssemblageForm_.Changed)
                    {
                        Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                        RefreshOperations();
                    }

                }
                else 
                {
                    uint disassemblyid = Convert.ToUInt32(s.Substring(1));
                    DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(disassemblyid);
                    Trade.Forms.TradeForms.DisAssemblageForm DisAssemblageForm_ = new Trade.Forms.TradeForms.DisAssemblageForm(DB, DisAssemblabgeOPR_, true);
                    DisAssemblageForm_.ShowDialog();
                    if (DisAssemblageForm_.Changed)
                    {
                        Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                        RefreshOperations();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("تعديل عملية" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDisAssemblyOPR_MenuItem_Click(object sender, EventArgs e)
        {
           try
            {
                DisAssemblageForm DisAssemblageForm_ = new DisAssemblageForm(DB);
                DisAssemblageForm_.ShowDialog();
                if(DisAssemblageForm_.Changed )
                {
                    Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                    RefreshOperations();
                }
                DisAssemblageForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("انشاء عملية تفكيك"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void CreateAssemblyOPR_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AssemblageForm AssemblageForm_ = new AssemblageForm(DB);
                AssemblageForm_.ShowDialog();
                if (AssemblageForm_.Changed)
                {
                    Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                    RefreshOperations();
                }
                AssemblageForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("انشاء عملية تجميع" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenOperation_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string s = listView.SelectedItems[0].Name;
                if (s.Substring(0, 1) == "A")
                {
                    uint assemblyid = Convert.ToUInt32(s.Substring(1));
                    AssemblabgeOPR AssemblabgeOPR_ = new AssemblageSQL(DB).GetAssemblageOPR_INFO_BYID(assemblyid);
                    Trade.Forms.TradeForms.AssemblageForm AssemblageForm_ = new Trade.Forms.TradeForms.AssemblageForm(DB, AssemblabgeOPR_, false );
                    AssemblageForm_.ShowDialog();
                    if (AssemblageForm_.Changed)
                    {
                        Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                        RefreshOperations();
                    }

                }
                else
                {
                    uint disassemblyid = Convert.ToUInt32(s.Substring(1));
                    DisAssemblabgeOPR DisAssemblabgeOPR_ = new DisAssemblageSQL(DB).GetDisAssemblageOPR_INFO_BYID(disassemblyid);
                    Trade.Forms.TradeForms.DisAssemblageForm DisAssemblageForm_ = new Trade.Forms.TradeForms.DisAssemblageForm(DB, DisAssemblabgeOPR_, false );
                    DisAssemblageForm_.ShowDialog();
                    if (DisAssemblageForm_.Changed)
                    {
                        Industrial_OPRList = new IndustrySQL(DB).GetIndustrial_Operations();
                        RefreshOperations();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("استعراض تفاصيل العملية" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public void ConfigureListviewColumnSize()
        //{
     
        //    int w = (listView.Width - 980)/2;
        //    listView.Columns[0].Width = 55;
        //    listView.Columns[1].Width = 55;
        //    listView.Columns[2].Width = 100;
        //    listView.Columns[3].Width = 125;
        //    listView.Columns[4].Width = 125;
        //    listView.Columns[5].Width = 125;
        //    listView.Columns[6].Width = 125;
        //    listView.Columns[7].Width = 90;
        //    listView.Columns[8].Width = 90;
        //    listView.Columns[9].Width = 90;
        //    listView.Columns[10].Width = w - 1;
        //    listView.Columns[11].Width = w - 1;
        //}
        //private void listView_Resize(object sender, EventArgs e)
        //{
        //    ConfigureListviewColumnSize();
        //}

        private void IndustrialForm_Load(object sender, EventArgs e)
        {
            try
            {

                this.comboBoxOperationType.SelectedIndexChanged += new System.EventHandler(this.comboBoxOperationType_SelectedIndexChanged);
                this.comboBoxFolderFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFolderFilter_SelectedIndexChanged_1);
                this.comboBoxCompanyFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompanyFilter_SelectedIndexChanged);
                this.comboBoxItemilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemilter_SelectedIndexChanged_1);
                this.comboBoxOprStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxOprStatus_SelectedIndexChanged);
                comboBoxOperationType.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                MessageBox.Show("IndustrialForm_Load:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

  
        
        private void comboBoxFolderFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                comboBoxFolderFilter.SelectedIndex = 0;
        }
        private void comboBoxCompanyFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                comboBoxCompanyFilter.SelectedIndex = 0;
        }
        private void comboBoxItemilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                comboBoxItemilter.SelectedIndex = 0;
        }

        private void comboBoxOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<Industrial_OPR> filteredlist_byOprType = new List<Industrial_OPR>();
                if (comboBoxOperationType.SelectedIndex == 0)
                    filteredlist_byOprType = Industrial_OPRList;
                else
                {
                    uint opr_type = ((ComboboxItem)comboBoxOperationType.SelectedItem).Value;
                    filteredlist_byOprType = Industrial_OPRList.Where(x => x._Operation.OperationType == opr_type).ToList();
                }
                IEnumerable<string> distinctfolders = filteredlist_byOprType.Where(x => x._Item != null).Select(x => x._Item.folder.FolderName).Distinct();
                comboBoxFolderFilter.Items.Clear();
                comboBoxFolderFilter.Items.Add("الكل");
                foreach (var s in distinctfolders)
                    comboBoxFolderFilter.Items.Add(s);
                comboBoxFolderFilter.SelectedIndex = 0;

            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxOperationType_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          }
        private void comboBoxFolderFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                List<Industrial_OPR> filteredlist_byOprType = new List<Industrial_OPR>();
                if (comboBoxOperationType.SelectedIndex == 0)
                    filteredlist_byOprType = Industrial_OPRList;
                else
                {
                    uint opr_type = ((ComboboxItem)comboBoxOperationType.SelectedItem).Value;

                    filteredlist_byOprType = Industrial_OPRList.Where(x => x._Operation.OperationType == opr_type).ToList();
                }
                List<Industrial_OPR> filteredlist_byfoldername = new List<Industrial_OPR>();
                if (comboBoxFolderFilter.SelectedIndex == 0)
                    filteredlist_byfoldername = filteredlist_byOprType;
                else
                    filteredlist_byfoldername = filteredlist_byOprType.Where(x => x._Item != null && x._Item.folder.FolderName == comboBoxFolderFilter.SelectedItem.ToString()).ToList();

                IEnumerable<string> distinctcompanies = filteredlist_byfoldername.Where(x => x._Item != null).Select(x => x._Item.ItemCompany).Distinct();
                comboBoxCompanyFilter.Items.Clear();
                comboBoxCompanyFilter.Items.Add("الكل");
                foreach (var s in distinctcompanies)
                    comboBoxCompanyFilter.Items.Add(s);
                comboBoxCompanyFilter.SelectedIndex = 0;

            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxFolderFilter_SelectedIndexChanged_1:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         }
        private void comboBoxCompanyFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<Industrial_OPR> filteredlist_byOprType = new List<Industrial_OPR>();
                if (comboBoxOperationType.SelectedIndex == 0)
                    filteredlist_byOprType = Industrial_OPRList;
                else
                {
                    uint opr_type = ((ComboboxItem)comboBoxOperationType.SelectedItem).Value;

                    filteredlist_byOprType = Industrial_OPRList.Where(x => x._Operation.OperationType == opr_type).ToList();
                }
                List<Industrial_OPR> filteredlist_byfoldername = new List<Industrial_OPR>();
                if (comboBoxFolderFilter.SelectedIndex == 0)
                    filteredlist_byfoldername = filteredlist_byOprType;
                else
                    filteredlist_byfoldername = filteredlist_byOprType.Where(x => x._Item != null && x._Item.folder.FolderName == comboBoxFolderFilter.SelectedItem.ToString()).ToList();

                List<Industrial_OPR> filteredlist_bycompany = new List<Industrial_OPR>();
                if (comboBoxCompanyFilter.SelectedIndex == 0)
                    filteredlist_bycompany = filteredlist_byfoldername;
                else
                    filteredlist_bycompany = filteredlist_byfoldername.Where(x => x._Item != null && x._Item.ItemCompany == comboBoxCompanyFilter.SelectedItem.ToString()).ToList();

                IEnumerable<string> distinctitems = filteredlist_bycompany.Where(x => x._Item != null).Select(x => x._Item.ItemName).Distinct();
                comboBoxItemilter.Items.Clear();
                comboBoxItemilter.Items.Add("الكل");
                foreach (var s in distinctitems)
                    comboBoxItemilter.Items.Add(s);
                comboBoxItemilter.SelectedIndex = 0;

            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxCompanyFilter_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         }

        private void comboBoxItemilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                List<Industrial_OPR> filteredlist_byOprType = new List<Industrial_OPR>();
                if (comboBoxOperationType.SelectedIndex == 0)
                    filteredlist_byOprType = Industrial_OPRList;
                else
                {

                    uint opr_type = ((ComboboxItem)comboBoxOperationType.SelectedItem).Value;

                    filteredlist_byOprType = Industrial_OPRList.Where(x => x._Operation.OperationType == opr_type).ToList();
                }
                List<Industrial_OPR> filteredlist_byfoldername = new List<Industrial_OPR>();
                if (comboBoxFolderFilter.SelectedIndex == 0)
                    filteredlist_byfoldername = filteredlist_byOprType;
                else
                    filteredlist_byfoldername = filteredlist_byOprType.Where(x => x._Item != null && x._Item.folder.FolderName == comboBoxFolderFilter.SelectedItem.ToString()).ToList();

                List<Industrial_OPR> filteredlist_bycompany = new List<Industrial_OPR>();
                if (comboBoxCompanyFilter.SelectedIndex == 0)
                    filteredlist_bycompany = filteredlist_byfoldername;
                else
                    filteredlist_bycompany = filteredlist_byfoldername.Where(x => x._Item != null && x._Item.ItemCompany == comboBoxCompanyFilter.SelectedItem.ToString()).ToList();

                List<Industrial_OPR> filteredlist_byitem = new List<Industrial_OPR>();
                if (comboBoxItemilter.SelectedIndex == 0)
                    filteredlist_byitem = filteredlist_bycompany;
                else
                    filteredlist_byitem = filteredlist_bycompany.Where(x => x._Item != null && x._Item.ItemName == comboBoxItemilter.SelectedItem.ToString()).ToList();

                IEnumerable<string> distinctoprstatus = filteredlist_byitem.Select(x => x.OPRStatus).Distinct();
                comboBoxOprStatus.Items.Clear();
                comboBoxOprStatus.Items.Add("الكل");
                foreach (var s in distinctoprstatus)
                    comboBoxOprStatus.Items.Add(s);
                comboBoxOprStatus.SelectedIndex = 0;

            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxItemilter_SelectedIndexChanged_1:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         }

        private void comboBoxOprStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOperations();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
