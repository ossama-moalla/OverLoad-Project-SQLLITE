using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.ItemObj.ItemObjSQL;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;

namespace OverLoad_Client.ItemObj.Forms
{
    public partial class User_AvailabeItemsForm : Form
    {
 

        System.Windows.Forms.MenuItem OpenFolderMenuItem;
 
        //System.Windows.Forms.MenuItem AddFolderSpec;

        System.Windows.Forms.MenuItem OpenItemMenuItem;



        System.Windows.Forms.MenuItem SpecFilter;


        DatabaseInterface DB;
        Folder folder;
        List<Folder> FoldersListView = new List<Folder>();
        List<Item_AvailableAmount_Report> ItemsListView = new List<Item_AvailableAmount_Report>();
        FolderSQL foldersql;
        AvailableItemSQL AvailableItemSQL_;
        Button front, end;
        int Path_startIndex = 0;
        Button[] b;
        ComboBox[] ComboboxSpec_Value;
        TextBox[] TextBoxSpec_Value;
        Folder RootFolder;

        delegate void TreeviewVoidDelegate();

        public Item ReturnItem;
        public Folder  ReturnFolder;
        private bool GetAvailableItem;
        List<TradeState> tradestateList;
        public User_AvailabeItemsForm(DatabaseInterface db, Folder f,  bool GetAvailableItem_)
        {
            DB = db;
            InitializeComponent();
            if (DB.__User.AccessFolderPremessionList.Count == 0) throw new Exception("لم تمنح اي صلاحية لاستعراض اي صنف");

            GetAvailableItem = GetAvailableItem_;
            if (GetAvailableItem) Button_Select.Visible = true;
            else Button_Select.Visible = false;
            tradestateList = new TradeStateSQL(DB).GetTradeStateList();
            for (int j = 0; j < tradestateList.Count; j++)
            {
                listView1.Columns.Add(tradestateList[j].TradeStateName,150);
            }

            OpenFolderMenuItem = new System.Windows.Forms.MenuItem("فتح", OpenFolder_MenuItem_Click); ;

            OpenItemMenuItem = new MenuItem("فتح صفحة العنصر", OpenItem_MenuItem_Click);
            SpecFilter = new MenuItem("فلترة حسب الخصائص", SpecFilter_MenuItem_Click);
         
            foldersql = new FolderSQL(DB);
            AvailableItemSQL_ = new AvailableItemSQL(DB);

            folder = f;
            FillComboBoxFolderAccess(folder);
            comboBox1.SelectedIndex = 0;
            comboBoxFilterItemFolder.SelectedIndex = 0;
            comboBoxFilterItemFolder.SelectedIndexChanged += new EventHandler(comboBoxFilterItemFolder_SelectedIndexChanged);
            FillTreeViewFolder();
            OpenFolder();

            front = new Button();
            end = new Button();
            front.Font = new Font(front.Font.FontFamily, 6);

            front.Size = new Size(25, PanelPath.Height);
            front.TextAlign = ContentAlignment.MiddleCenter;
            front.Text = ">>";
            front.BackColor = Color.SkyBlue;
            front.Location = new Point(0, 0);
            end.Size = new Size(25, PanelPath.Height);

            end.Text = "<<";
            end.Font = new Font(front.Font.FontFamily, 6);
            end.BackColor = Color.SkyBlue;
            front.Click += new EventHandler(Front_Click);
            end.Click += new EventHandler(End_Click);

        }
        public void FillComboBoxFolderAccess(Folder f)
        {
            comboBoxFolderAccess.Items.Clear();
            if (DB.__User.AccessFolderPremessionList.Count == 0) throw new Exception("لم تمنح اي صلاحية لاستعراض اي صنف");
            int s = DB.__User.AccessFolderPremessionList.Where(x => x._Folder == null).Count();
            if (s > 0)
            {
                ComboboxItem ComboboxItem_ = new ComboboxItem("جميع الأصناف", 0);
                comboBoxFolderAccess.Items.Add(ComboboxItem_);
                comboBoxFolderAccess.Enabled = false;
                comboBoxFolderAccess.SelectedIndex = 0;


            }
            else
            {
                int selected_index = 0;
                for (int i = 0; i < DB.__User.AccessFolderPremessionList.Count; i++)
                {
                    ComboboxItem ComboboxItem_ = new ComboboxItem(DB.__User.AccessFolderPremessionList[i]._Folder.FolderName, DB.__User.AccessFolderPremessionList[i]._Folder .FolderID);
                    comboBoxFolderAccess.Items.Add(ComboboxItem_);
                    if (f != null && f.FolderID == DB.__User.AccessFolderPremessionList[i]._Folder.FolderID) selected_index = i;
                   

                }
                comboBoxFolderAccess.SelectedIndex = selected_index;
                if (comboBoxFolderAccess.Items.Count == 1)
                    comboBoxFolderAccess.Enabled = false;
            }
         
        }
        private void comboBoxFolderAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem ComboBoxItem_ = (ComboboxItem)comboBoxFolderAccess.SelectedItem;
                if (ComboBoxItem_.Value == 0) RootFolder = null;
                else RootFolder = DB.__User.AccessFolderPremessionList.Where(x => x._Folder.FolderID == ComboBoxItem_.Value).ToList()[0]._Folder;
                FillTreeViewFolder();
                folder = RootFolder;
                OpenFolder();
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxFolderAccess_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public async  void OpenFolder()
        {
            try
            {
                comboBoxFilterItemFolder.SelectedIndex = 0;

                textBoxSearch.Text = "";
                //Thread thread1, thread2;
                //thread1 = new Thread(new ThreadStart(RefreshTreeView));
                //thread1.Start();


                //thread2 = new Thread(new ThreadStart(FolderIDPath));
                //thread2.Start();

                FolderIDPath();
                RefreshTreeView();
                FoldersListView = foldersql.GetFolderChilds(folder);
                if (folder != null)
                    ItemsListView = new AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List().Where(x => x.FolderID == folder.FolderID).ToList();
                else
                    ItemsListView = new List<Item_AvailableAmount_Report>();
                FillComboBoxComapines(ItemsListView);
                RefreshItems(FoldersListView, ItemsListView);
                if (splitContainer1.Panel2Collapsed == false)
                    if (folder == null)
                        splitContainer1.Panel2Collapsed = true;
                    else FillFolderSpec();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenFolder:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        public async  void FillFolderSpec()
        {
            try
            {
                splitContainer1.Panel2.Controls.Clear();
                if (folder == null) splitContainer1.Panel2Collapsed = true;
                List<ItemSpecDisplay> ItemSpecDisplayList = new List<ItemSpecDisplay>();
                List<ItemSpec> ItemSpecList_ = new ItemSpecSQL(DB).GetItemSpecList(folder);
                List<ItemSpec_Restrict> ItemSpec_Restrict_List_ = new ItemSpec_Restrict_SQL(DB).GetItemSpecRestrictList(folder);
                ItemSpec_Restrict_Options_SQL ItemSpec_Restrict_Options_SQL = new ItemSpec_Restrict_Options_SQL(DB);
                for (int i = 0; i < ItemSpec_Restrict_List_.Count; i++)
                {
                    ItemSpecDisplayList.Add(new ItemSpecDisplay(folder, ItemSpec_Restrict_List_[i].SpecID, ItemSpec_Restrict_List_[i].SpecName, ItemSpec_Restrict_List_[i].SpecIndex, false));
                }
                for (int i = 0; i < ItemSpecList_.Count; i++)
                {
                    ItemSpecDisplayList.Add(new ItemSpecDisplay(folder, ItemSpecList_[i].SpecID, ItemSpecList_[i].SpecName, ItemSpecList_[i].SpecIndex, true));
                }
                ComboboxSpec_Value = new ComboBox[ItemSpec_Restrict_List_.Count];
                TextBoxSpec_Value = new TextBox[ItemSpecList_.Count];
                ItemSpecDisplayList = ItemSpecDisplayList.OrderBy(m => m.SpecIndex).ToList();


                if (ItemSpecDisplayList.Count == 0)
                {
                    MessageBox.Show("لا توجد خصائص مدخلة لهذا الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    splitContainer1.Panel2Collapsed = true;
                    return;
                }
                Label LabelPanelTitle = new Label();
                LabelPanelTitle.BackColor = Color.LightGreen;
                LabelPanelTitle.BorderStyle = BorderStyle.FixedSingle;
                LabelPanelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)));
                LabelPanelTitle.AutoSize = false;
                LabelPanelTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                LabelPanelTitle.Location = new System.Drawing.Point(0, 0);
                //this.label2.Name = "label2";
                LabelPanelTitle.Size = new System.Drawing.Size(splitContainer1.Panel2.Width, 20);
                LabelPanelTitle.TextAlign = ContentAlignment.MiddleLeft;
                LabelPanelTitle.Text = "فلترة العناصر حسب الخصائص";


                Button ButonSpecFilterClose = new Button();
                ButonSpecFilterClose.Location = new System.Drawing.Point(splitContainer1.Panel2.Width / 2 - 40, splitContainer1.Panel2.Height - 40);
                ButonSpecFilterClose.Size = new System.Drawing.Size(57, 29);
                ButonSpecFilterClose.Click += new EventHandler(ButonSpecFilterClose_Click);
                ButonSpecFilterClose.Text = "اغلاق";
                ButonSpecFilterClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom)));
                Panel panelFolderSpecs = new Panel();
                panelFolderSpecs.BorderStyle = BorderStyle.FixedSingle;
                panelFolderSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom)));
                panelFolderSpecs.AutoScroll = true;
                panelFolderSpecs.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                panelFolderSpecs.Location = new System.Drawing.Point(5, 30);
                //this.label2.Name = "label2";
                panelFolderSpecs.Size = new System.Drawing.Size(splitContainer1.Panel2.Width - 10, splitContainer1.Panel2.Height - 75);
                panelFolderSpecs.Controls.Clear();



                Label[] label = new Label[ItemSpecDisplayList.Count];

                int Start_Paint_Position = 10;
                int h = 10;
                int w = panelFolderSpecs.Width - 40;
                int TextBoxSpec_Value_Index = 0;
                int ComboboxSpec_Value_Index = 0;
                for (int i = 0; i < ItemSpecDisplayList.Count; i++)
                {
                    label[i] = new Label();
                    label[i].BackColor = Color.Aquamarine;
                    label[i].BorderStyle = BorderStyle.FixedSingle;
                    label[i].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)));
                    label[i].AutoSize = false;
                    label[i].Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label[i].Location = new System.Drawing.Point(Start_Paint_Position, h);
                    //this.label2.Name = "label2";
                    label[i].Size = new System.Drawing.Size(w, 20);
                    label[i].TextAlign = ContentAlignment.MiddleLeft;
                    label[i].TabIndex = 15;
                    label[i].Text = ItemSpecDisplayList[i].SpecName;
                    panelFolderSpecs.Controls.Add(label[i]);
                    if (ItemSpecDisplayList[i].Spectype == false)
                    {
                        ComboboxSpec_Value[ComboboxSpec_Value_Index] = new ComboBox();
                        ComboboxSpec_Value[ComboboxSpec_Value_Index].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)));
                        ComboboxSpec_Value[ComboboxSpec_Value_Index].AutoSize = false;
                        ComboboxSpec_Value[ComboboxSpec_Value_Index].Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        if (h + 25 > panelFolderSpecs.Height)
                            w = panelFolderSpecs.Width - 40;// - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

                        ComboboxSpec_Value[ComboboxSpec_Value_Index].Location = new System.Drawing.Point(Start_Paint_Position, h + 25);
                        ComboboxSpec_Value[ComboboxSpec_Value_Index].Name = ItemSpecDisplayList[i].SpecID.ToString();
                        ComboboxSpec_Value[ComboboxSpec_Value_Index].BackColor = Color.White;
                        ComboboxSpec_Value[ComboboxSpec_Value_Index].Size = new System.Drawing.Size(w, 20);
                        ComboboxSpec_Value[ComboboxSpec_Value_Index].DropDownStyle = ComboBoxStyle.DropDownList;
                        List<ItemSpec_Restrict_Options> ItemSpec_Restrict_Options_ = new List<ItemSpec_Restrict_Options>();
                        ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict(folder, ItemSpecDisplayList[i].SpecID, ItemSpecDisplayList[i].SpecName, ItemSpecDisplayList[i].SpecIndex);
                        ItemSpec_Restrict_Options_ = new ItemSpec_Restrict_Options_SQL(DB).GetItemSpec_Restrict_Options_List(ItemSpec_Restrict_);
                        Dictionary<uint, string> combosource = new Dictionary<uint, string>();
                        if (ItemSpec_Restrict_Options_.Count > 0)
                        {
                            combosource.Add(0, "غير محدد");
                            for (int k = 0; k < ItemSpec_Restrict_Options_.Count; k++)
                            {
                                combosource.Add(ItemSpec_Restrict_Options_[k].OptionID, ItemSpec_Restrict_Options_[k].OptionName);
                            }

                            ComboboxSpec_Value[ComboboxSpec_Value_Index].DataSource = new BindingSource(combosource, null);
                            ComboboxSpec_Value[ComboboxSpec_Value_Index].DisplayMember = "Value";
                            ComboboxSpec_Value[ComboboxSpec_Value_Index].ValueMember = "Key";
                            ComboboxSpec_Value[ComboboxSpec_Value_Index].SelectedIndexChanged += new System.EventHandler(this.comboBox_ItemSpec_Value_SelectedIndexChanged);

                        }
                        else
                        {
                            combosource.Add(0, " لم يتم ادخال قيم");
                            ComboboxSpec_Value[ComboboxSpec_Value_Index].DataSource = new BindingSource(combosource, null);
                            ComboboxSpec_Value[ComboboxSpec_Value_Index].DisplayMember = "Value";
                            ComboboxSpec_Value[ComboboxSpec_Value_Index].ValueMember = "Key";
                        }

                        panelFolderSpecs.Controls.Add(ComboboxSpec_Value[ComboboxSpec_Value_Index]);
                        h = h + 60;
                        if (h > panelFolderSpecs.Height)
                            w = panelFolderSpecs.Width - 40; //- System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                        ComboboxSpec_Value_Index += 1;
                    }
                    else
                    {
                        TextBoxSpec_Value[TextBoxSpec_Value_Index] = new TextBox();
                        TextBoxSpec_Value[TextBoxSpec_Value_Index].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)));
                        TextBoxSpec_Value[TextBoxSpec_Value_Index].AutoSize = false;
                        TextBoxSpec_Value[TextBoxSpec_Value_Index].Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        if (h + 25 > panelFolderSpecs.Height)
                            w = panelFolderSpecs.Width - 40;
                        TextBoxSpec_Value[TextBoxSpec_Value_Index].Location = new System.Drawing.Point(Start_Paint_Position, h + 25);
                        TextBoxSpec_Value[TextBoxSpec_Value_Index].Name = ItemSpecDisplayList[i].SpecID.ToString();
                        TextBoxSpec_Value[TextBoxSpec_Value_Index].Size = new System.Drawing.Size(w, 20);
                        TextBoxSpec_Value[TextBoxSpec_Value_Index].TextChanged += new EventHandler(textBox_ItemSpec_Value_TextChanged);
                        panelFolderSpecs.Controls.Add(TextBoxSpec_Value[TextBoxSpec_Value_Index]);
                        h = h + 60;
                        if (h > panelFolderSpecs.Height)
                            w = panelFolderSpecs.Width - 40;
                        TextBoxSpec_Value_Index += 1;
                    }

                }


                //Label[] label_ = new Label[ItemSpec_List.Count];
                //TextBoxSpec_Value = new TextBox[ItemSpec_List.Count];
                //for (int i = 0; i < ItemSpec_List.Count; i++)
                //{
                //    label_[i] = new Label();
                //    label_[i].BackColor = Color.Aquamarine;
                //    label_[i].BorderStyle = BorderStyle.FixedSingle;
                //    label_[i].Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left)));
                //    label_[i].AutoSize = false;
                //    label_[i].Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //    label_[i].Location = new System.Drawing.Point(Start_Paint_Position, h);
                //    //this.label2.Name = "label2";
                //    label_[i].Size = new System.Drawing.Size(w, 20);
                //    label_[i].TextAlign = ContentAlignment.MiddleLeft;
                //    label_[i].Text = ItemSpec_List[i].SpecName;
                //    panelFolderSpecs.Controls.Add(label_[i]);

                //}
                Label padding = new Label();
                padding.Location = new System.Drawing.Point(Start_Paint_Position, h);
                //this.label2.Name = "label2";
                padding.Size = new System.Drawing.Size(w, 20);
                panelFolderSpecs.Controls.Add(padding);
                splitContainer1.Panel2.Controls.Add(LabelPanelTitle);
                splitContainer1.Panel2.Controls.Add(panelFolderSpecs);
                splitContainer1.Panel2.Controls.Add(ButonSpecFilterClose);

            }
            catch (Exception ee)
            {
                MessageBox.Show("FillFolderSpec:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void comboBox_ItemSpec_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterItemsBySpec();
        }
        private void textBox_ItemSpec_Value_TextChanged(object sender, EventArgs e)
        {
            FilterItemsBySpec();
        }
        public async void FilterItemsBySpec()
        {
            try
            {
                ItemSpec_Restrict_Options_SQL ItemSpec_Restrict_Options_SQL_ = new ItemSpec_Restrict_Options_SQL(DB);
                List<ItemSpec_Restrict_Options> ItemSpec_Restrict_Options_List = new List<ItemSpec_Restrict_Options>();
                List<ItemSpec_Value> ItemSpec_Value_List = new List<ItemSpec_Value>();

                for (int i = 0; i < ComboboxSpec_Value.Length; i++)
                {
                    if (ComboboxSpec_Value[i].SelectedIndex >= 1)
                    {
                        ItemSpec_Restrict ItemSpec_Restrict_ = new ItemSpec_Restrict_SQL(DB).GetItemSpecRestrictInfoByID(Convert.ToUInt32(ComboboxSpec_Value[i].Name));
                        ItemSpec_Restrict_Options_List.Add(new ItemSpec_Restrict_Options(ItemSpec_Restrict_, ((KeyValuePair<uint, string>)ComboboxSpec_Value[i].SelectedItem).Key, ((KeyValuePair<uint, string>)ComboboxSpec_Value[i].SelectedItem).Value));

                    }
                }
                for (int i = 0; i < TextBoxSpec_Value.Length; i++)
                {
                    if (TextBoxSpec_Value[i].Text.Length > 0)
                    {
                        ItemSpec ItemSpec_ = new ItemSpecSQL(DB).GetItemSpecInfoByID(Convert.ToUInt32(TextBoxSpec_Value[i].Name));
                        ItemSpec_Value_List.Add(new ItemSpec_Value(null, ItemSpec_, TextBoxSpec_Value[i].Text));

                    }
                }
                if (ItemSpec_Restrict_Options_List.Count == 0 && ItemSpec_Value_List.Count == 0)
                {
                    OpenFolder();
                    return;
                }
                List<Item_AvailableAmount_Report> FilteredItems = new AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List_CUSTOM
                    (new ItemSQL(DB).FilterItemsBySpec(ItemSpec_Restrict_Options_List, ItemSpec_Value_List));
                RefreshItems(new List<Folder>(), FilteredItems);
            }
            catch (Exception ee)
            {
                MessageBox.Show("FilterItemsBySpec:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        public async void FillComboBoxComapines(List<Item_AvailableAmount_Report> items)
        {
            try
            {
                comboBoxCompanies.Items.Clear();
                List<string> companies = items.Select(x => x.ItemCompany).ToList();
                if (companies.Count > 0)
                {

                    comboBoxCompanies.SelectedIndexChanged -= new EventHandler(comboBoxCompanies_SelectedIndexChanged);
                    comboBoxCompanies.Enabled = true;
                    comboBoxCompanies.Items.Add("الكل");
                    comboBoxCompanies.Items.AddRange(companies.ToArray());
                    comboBoxCompanies.SelectedIndex = 0;
                    comboBoxCompanies.SelectedIndexChanged += new EventHandler(comboBoxCompanies_SelectedIndexChanged);

                }
                else comboBoxCompanies.Enabled = false;

            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBoxComapines:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public async void RefreshItems(List<Folder> folders, List<Item_AvailableAmount_Report> items)
        {
            try
            {
                listView1.Items.Clear();

                if (comboBoxFilterItemFolder.SelectedIndex != 2)
                {

                    for (int i = 0; i < folders.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(folders[i].FolderName);
                        item.Name = "F" + folders[i].FolderID.ToString();
                        item.SubItems.Add("");
                        item.SubItems.Add("صنف");
                        if (textBoxSearch.Text.Length > 0)
                            item.SubItems.Add(foldersql.GetFolderPath(RootFolder, folders[i]));
                        else
                            item.SubItems.Add(new ItemSQL(DB).GetItemsInFolder(folders[i]).Count.ToString() + " عنصر , " + foldersql.GetFolderChilds(folders[i]).Count.ToString() + "  صنف ");
                        item.ImageIndex = 1;
                        listView1.Items.Add(item);

                    }
                }

                if (comboBoxFilterItemFolder.SelectedIndex != 1)
                {

                    List<string> companies = new List<string>();
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (comboBoxCompanies.SelectedIndex > 0 && items[i].ItemCompany.ToString() != comboBoxCompanies.SelectedItem.ToString()) continue;

                        ListViewItem item = new ListViewItem(items[i].ItemName);
                        item.Name = "I" + items[i].ItemID.ToString();
                        item.SubItems.Add(items[i].ItemCompany.ToString());
                        item.SubItems.Add("عنصر");
                        item.SubItems.Add("");
                        double available_amount = 0;
                        for (int j = 0; j < tradestateList.Count; j++)
                        {
                            double a_v = items[i].TradeState_AvailableAmount .Where (x => x._TradeState .TradeStateID  == tradestateList[j].TradeStateID ).Sum(y => y.AvailableAmount);
                            available_amount += a_v;
                            item.SubItems.Add(a_v.ToString());
                        }
                        if (available_amount == 0 && checkBoxAvailableOnly.Checked) continue;
                        if (available_amount > 0) item.BackColor = Color.LimeGreen;
                        else item.BackColor = Color.Orange;
                        if (textBoxSearch.Text.Length > 0)
                            item.SubItems.Add( items[i].FolderPath );

                        item.ImageIndex = 0;
                        listView1.Items.Add(item);

                    }

                }
                if (comboBoxFilterItemFolder.SelectedIndex == 1) comboBoxCompanies.Enabled = false;
                else
                {
                    if (comboBoxCompanies.Items.Count > 0)
                    {
                        comboBoxCompanies.Enabled = true;
                        //comboBoxCompanies.SelectedIndex = 0;
                    }
                    else comboBoxCompanies.Enabled = false;
                }



            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshItems:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        public async void RefreshTreeView()
        {
            try
            {
                if (this.treeViewFolders.InvokeRequired)
                {
                    TreeviewVoidDelegate d = new TreeviewVoidDelegate(RefreshTreeView);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    string fid;
                    if (folder == null) fid = "null";
                    else fid = folder.FolderID.ToString();
                    TreeNode[] node = treeViewFolders.Nodes.Find(fid, true);
                    if (node.Length == 0) return;
                    node[0].Expand();
                    treeViewFolders.SelectedNode = node[0];
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshTreeView:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }
        public async void FolderIDPath()
        {
            try
            {
                if (this.PanelPath.InvokeRequired)
                {
                    TreeviewVoidDelegate d = new TreeviewVoidDelegate(FolderIDPath);
                    this.Invoke(d, new object[] { });
                }
                else
                {

                    Folder TempFolder = folder;
                    List<Folder> list = new List<Folder>();
                    if (RootFolder != null && folder != null && folder.FolderID == RootFolder.FolderID)
                        list.Add(folder);
                    else
                    {
                        while (true)
                        {

                            if (TempFolder != null) { list.Add(TempFolder); TempFolder = foldersql.GetParentFolder(TempFolder); }
                            else { list.Add(TempFolder); break; }

                            if (RootFolder != null && TempFolder != null)
                                if (RootFolder.FolderID == TempFolder.FolderID)
                                {
                                    list.Add(TempFolder); break;
                                }


                        }
                    }
                    Button[] b = new Button[list.Count];
                    for (int j = 0; j < list.Count; j++)
                    {
                        int i = list.Count - j - 1;
                        b[i] = new Button();
                        b[i].Image = ImageListButton.Images[0];

                        b[i].AutoSize = true;
                        b[i].AutoSizeMode = AutoSizeMode.GrowAndShrink;
                        b[i].AutoEllipsis = false;
                        b[i].Font = new Font(b[i].Font.FontFamily, 10);
                        b[i].FlatStyle = FlatStyle.Flat;
                        b[i].FlatAppearance.BorderSize = 0;
                        if (list[j] == null)
                        {
                            b[i].Name = "null";
                            b[i].Text = "ROOT";
                        }
                        else
                        {
                            b[i].Name = list[j].FolderID.ToString();
                            b[i].Text = list[j].FolderName.ToString(); ;
                        }
                        b[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                        b[i].Click += new EventHandler(Button_Path_Click);

                    }
                    PanelPath.Controls.Clear();
                    int ButtonWidth = 0;
                    for (int i = 0; i < b.Length; i++)
                    {
                        ButtonWidth = ButtonWidth + b[i].Width;
                    }
                    if (ButtonWidth > PanelPath.Width)
                    {
                        int availablewidth = 0;
                        PanelPath.Controls.Add(front);
                        availablewidth = PanelPath.Width - front.Width - end.Width;
                        int buton_x = front.Width;
                        Path_startIndex = b.Length - 1;
                        int wid = 0;
                        for (int j = b.Length - 1; j > 0; j--)
                        {
                            wid = wid + b[j].Width;
                            if (wid > availablewidth) { Path_startIndex = j + 1; break; }
                        }
                        int i = Path_startIndex;
                        while (i < b.Length)
                        {
                            b[i].Location = new Point(buton_x, PanelPath.Location.Y - 2);
                            PanelPath.Controls.Add(b[i]);
                            availablewidth = availablewidth - b[i].Width;
                            buton_x = buton_x + b[i].Width;
                            i++;
                        }
                    }
                    else
                    {
                        int x1 = 0;
                        for (int i = 0; i < b.Length; i++)
                        {
                            b[i].Location = new Point(x1, PanelPath.Location.Y - 2);
                            PanelPath.Controls.Add(b[i]);
                            b[i].Show();
                            x1 = x1 + b[i].Width;
                        }

                    }
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("FolderIDPath:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        public async void FillTreeViewFolder()
        {
            try
            {
                List<Folder> FoldersParents = new List<Folder>();

                FoldersParents = foldersql.GetFolderChilds(RootFolder);
                treeViewFolders.Nodes.Clear();

                TreeNode r = new TreeNode("الجذر");
                if (RootFolder == null)
                {
                    r = new TreeNode("الجذر");
                    r.Name = "null";
                }
                else
                {
                    r = new TreeNode(RootFolder.FolderName);
                    r.Name = RootFolder.FolderID.ToString();
                }

                r.ImageIndex = 0;
                treeViewFolders.Nodes.Add(r);
                while (FoldersParents.Count != 0)
                {
                    List<Folder> FoldersChilds = new List<Folder>();
                    for (int i = 0; i < FoldersParents.Count; i++)
                    {
                        TreeNode n = new TreeNode(FoldersParents[i].FolderName);
                        n.Name = FoldersParents[i].FolderID.ToString();
                        n.ImageIndex = 0;
                        string parentid = "";
                        if (FoldersParents[i].ParentFolderID == null)
                            parentid = "null";
                        else parentid = FoldersParents[i].ParentFolderID.ToString();
                        TreeNode[] nodes = treeViewFolders.Nodes.Find(parentid, true);
                        nodes[0].Nodes.Add(n);
                        FoldersChilds.AddRange(foldersql.GetFolderChilds(FoldersParents[i]));

                    }

                    FoldersParents.Clear();
                    FoldersParents = FoldersChilds;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillTreeViewFolder:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        private async void OptimizePath()
        {
            try
            {
                PanelPath.Controls.Clear();
                int buton_x = 0;
                int front_width = 0;
                if (Path_startIndex > 0)
                {
                    PanelPath.Controls.Add(front);
                    buton_x = front.Width;
                    front_width = front.Width;
                }
                int i = Path_startIndex;
                int availablewidth = PanelPath.Width - front_width - end.Width;

                while (i < b.Length)
                {

                    b[i].Location = new Point(buton_x, PanelPath.Location.Y - 2);
                    if (b[i].Width > availablewidth)
                    {
                        end.Location = new Point(buton_x, 0);
                        PanelPath.Controls.Add(end);
                        break;
                    }
                    PanelPath.Controls.Add(b[i]);
                    availablewidth = availablewidth - b[i].Width;
                    buton_x = buton_x + b[i].Width;
                    i++;
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("OptimizePath:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void Button_Path_Click(object sender, EventArgs e)
        {
            try
            {
                Button bb = (Button)sender;
                try
                {
                    folder = foldersql.GetFolderInfoByID(Convert.ToUInt32(bb.Name));
                }
                catch
                {
                    folder = null;
                }
                OpenFolder();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Button_Path_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

         
        }
        private void End_Click(object sender, EventArgs e)
        {
            Path_startIndex = Path_startIndex + 1;
            OptimizePath();
        }
        private void Front_Click(object sender, EventArgs e)
        {
            if (Path_startIndex == 0) return;
            Path_startIndex = Path_startIndex - 1;
            OptimizePath();
        }
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    List<MenuItem> MenuItemList = new List<MenuItem>();
                    listView1.ContextMenu = null;
                    bool match = false;
                    ListViewItem listitem = new ListViewItem();
                    foreach (ListViewItem item1 in listView1.Items)
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

                        if (listitem.Name.Substring(0, 1) == "F")
                        {
                            MenuItem[] mi1 = new MenuItem[] {OpenFolderMenuItem   /*,AddFolderSpec*/
                            ,new MenuItem ("-")  };
                            MenuItemList.AddRange(mi1);
                        }
                        else
                        {
                            MenuItem[] mi1 = new MenuItem[] {OpenItemMenuItem
                            ,new MenuItem ("-") };
                            MenuItemList.AddRange(mi1);

                        }


                    }
                    //////////////


                    MenuItem[] m_i = new MenuItem[] { new MenuItem("-"), SpecFilter };
                    MenuItemList.AddRange(m_i);
                    if (folder == null)
                    {
                        SpecFilter.Enabled = false;
                    }
                    else
                    {
                        SpecFilter.Enabled = true;
                    }
                    //if(textBoxSearch .Text .Length >0)
                    //{
                    //    CreateFolderMenuItem.Enabled = false;
                    //    CreateItemMenuItem.Enabled = false;
                    //    SpecFilter.Enabled = false;
                    //}
                    listView1.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #region ContextMenuItemEWvents

        private void OpenFolder_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string id_s = listView1.SelectedItems[0].Name.Substring(1);
                folder = foldersql.GetFolderInfoByID(Convert.ToUInt32(id_s));
                OpenFolder();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenFolder_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       

        }

        private void OpenItem_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                    Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(id_s1));
                    ItemForm itemform = new ItemForm(this.DB, item);
                    itemform.Show();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenItem_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void SpecFilter_MenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = false;
            FillFolderSpec();
        }
        #endregion
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 2: listView1.View = View.List; break;
                case 1: listView1.View = View.SmallIcon ; break;
                case 0: listView1.View = View.Details ; break;
            }
           
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
                {

                    switch (listView1.SelectedItems[0].Name.Substring(0, 1))
                    {
                        case "F":
                            string id_s = listView1.SelectedItems[0].Name.Substring(1);

                            folder = foldersql.GetFolderInfoByID(Convert.ToUInt32(id_s));
                            OpenFolder();

                            break;
                        case "I":

                            if (GetAvailableItem) ReturnItem_();
                            else
                            {
                                string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                                Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(id_s1));
                                ItemForm itemform = new ItemForm(this.DB, item);
                                itemform.Show();
                            }
                           


                            break;
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

             
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (folder  == null ) return;
            if (RootFolder != null) if (folder.FolderID == RootFolder.FolderID) return;
                    folder = foldersql.GetParentFolder(folder);
            OpenFolder();
        }
        private void comboBoxFilterItemFolder_SelectedIndexChanged(object sender, EventArgs e)
        {

            RefreshItems(FoldersListView ,ItemsListView );
        }

        private void comboBoxCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItems(FoldersListView, ItemsListView);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSearch.Text.Length > 0)
                {

                    splitContainer1.Panel2Collapsed = true;
                    if (checkBoxSearchType.Checked == false)
                    {
                        FoldersListView = foldersql.SearchFolder(RootFolder, textBoxSearch.Text);
                        ItemsListView = new AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List_CUSTOM(new ItemSQL(DB).SearchItem(RootFolder, textBoxSearch.Text));
                        FillComboBoxComapines(ItemsListView);
                        RefreshItems(FoldersListView, ItemsListView);
                    }
                    else
                    {
                        ItemsListView = new AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List_CUSTOM(new ItemSQL(DB).SearchItemInFolder(folder, textBoxSearch.Text));
                        FillComboBoxComapines(ItemsListView);
                        RefreshItems(FoldersListView, ItemsListView);
                    }
                }
                else OpenFolder();
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxSearch_TextChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void checkBoxSearchType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSearch.Text.Length > 0)
                {

                    splitContainer1.Panel2Collapsed = true;
                    if (checkBoxSearchType.Checked == false)
                    {
                        FoldersListView = foldersql.SearchFolder(RootFolder, textBoxSearch.Text);
                        ItemsListView = new AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List_CUSTOM(new ItemSQL(DB).SearchItem(RootFolder, textBoxSearch.Text));
                        FillComboBoxComapines(ItemsListView);
                        RefreshItems(FoldersListView, ItemsListView);
                    }
                    else
                    {
                        ItemsListView = new AvailableItemSQL(DB).Get_Item_AvailableAmount_Report_List_CUSTOM(new ItemSQL(DB).SearchItemInFolder(folder, textBoxSearch.Text));
                        FillComboBoxComapines(ItemsListView);
                        RefreshItems(FoldersListView, ItemsListView);
                    }
                }
                else OpenFolder();
            }
            catch (Exception ee)
            {
                MessageBox.Show("checkBoxSearchType_CheckedChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }



        private void ButonSpecFilterClose_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed  = true;
            OpenFolder();
        }

    
   

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Back)
                {
                    if (folder == null) return;
                    folder = foldersql.GetParentFolder(folder);
                    OpenFolder();
                    return;
                }
                if (listView1.SelectedItems.Count > 0)
                {

                    switch (e.KeyData)
                    {
                        case Keys.Enter:
                            switch (listView1.SelectedItems[0].Name.Substring(0, 1))
                            {
                                case "F":
                                    string id_s = listView1.SelectedItems[0].Name.Substring(1);
                                    try
                                    {
                                        folder = foldersql.GetFolderInfoByID(Convert.ToUInt32(id_s));
                                        OpenFolder();
                                    }
                                    catch (Exception ee)
                                    {
                                        MessageBox.Show(ee.Message);
                                    }
                                    break;
                                case "I":
                                    string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                                    Item item = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(id_s1));
                                    ItemForm itemform = new ItemForm(this.DB, item);
                                    itemform.Show();
                                    break;
                            }
                            break;




                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_KeyDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            Adjust_ListViewItemsColumns();
        }
        public async void Adjust_ListViewItemsColumns()
        {
            //listView1.Columns[0].Width = 250;//itemname
            //listView1.Columns[1].Width = 150;//company
            //listView1.Columns[2].Width = 100;//Type
            //listView1.Columns[3].Width = 200;//createdate
            //listView1.Columns[4].Width = 150;//marketcode
            //listView1.Columns[5].Width = 150;//defaultconsumeunbit

        }

        private void ShowItemsForm_Load(object sender, EventArgs e)
        {
            Adjust_ListViewItemsColumns(); ;
            textBoxSearch.Focus();
            //this.listView1.Resize += new System.EventHandler(this.listView1_Resize);

        }

        private void مجموعاتالمكافئاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            Equivalence_Group_Form Equivalence_Group_Form_ = new Equivalence_Group_Form(DB, false);
            Equivalence_Group_Form_.ShowDialog();

            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        private void Button_Select_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    ReturnItem_();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Button_Select_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }
        public void ReturnItem_()
        {
            try
            {
                ReturnItem = new ItemSQL(DB).GetItemInfoByID(Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring (1)));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ee)
            {
                MessageBox.Show("ReturnItem_:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
       
        }

        private void checkBoxAvailableOnly_CheckedChanged(object sender, EventArgs e)
        {
            RefreshItems(FoldersListView, ItemsListView);
        }

        private void treeViewFolders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (treeViewFolders.SelectedNode != null)
                    {
                        try
                        {
                            folder = foldersql.GetFolderInfoByID(Convert.ToUInt32(treeViewFolders.SelectedNode.Name));
                        }
                        catch
                        {
                            folder = null;
                        }

                        OpenFolder();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("treeViewFolders_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
  
}
