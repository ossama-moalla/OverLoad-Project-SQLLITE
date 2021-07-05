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

namespace OverLoad_Client.Trade.Forms.Container
{
    public partial class User_ShowLocationsForm : Form
    {
        public const uint SHOW_Data = 0;
        public const uint SELECT_Container = 1;
        public const uint SELECT_Place = 2;
        System.Windows.Forms.MenuItem CreateContainerMenuItem;
        System.Windows.Forms.MenuItem CreatePlaceMenuItem;

        System.Windows.Forms.MenuItem OpenContainerMenuItem;
        System.Windows.Forms.MenuItem EditContainerMenuItem;
        System.Windows.Forms.MenuItem DeleteContainerMenuItem;


        System.Windows.Forms.MenuItem OpenPlaceMenuItem;
        System.Windows.Forms.MenuItem EditPlaceMenuItem;
        System.Windows.Forms.MenuItem DeletePlaceMenuItem;

        System.Windows.Forms.MenuItem CutMenuItem;
        System.Windows.Forms.MenuItem PasteMenuItem;
        TradeStoreContainer RootContainer;
        DatabaseInterface DB;
        TradeStoreContainer Container;
        List<TradeStoreContainer> ContainersListView = new List<TradeStoreContainer>();
        List<TradeStorePlace> PlacesListView = new List<TradeStorePlace>();
        TradeStoreContainerSQL Containersql;
        TradeStorePlaceSQL placesql;
        Button front, end;
        int Path_startIndex = 0;
        Button[] b;
        List<TradeStoreContainer> Moved_ContainerList;
        List<TradeStorePlace> Moved_PlaceList;

        //Container Move_SourceContainer;
        private TradeStorePlace _ReturnPlace;
        public TradeStorePlace ReturnPlace
        {
            get { return _ReturnPlace; }
        }
        delegate void TreeviewVoidDelegate();
 
        public TradeStoreContainer  _ReturnContainer;
        public TradeStoreContainer ReturnContainer
        {
            get { return _ReturnContainer; }
        }
        uint SelectType;
        public User_ShowLocationsForm(DatabaseInterface db, TradeStoreContainer f, uint SelectType_)
        {
            DB = db;

            InitializeComponent();
            if (DB.__User.AccessContainerPremessionList.Count == 0) throw new Exception("لم تمنح اي صلاحية لاستعراض اي حاوية");

            SelectType = SelectType_;
            if (SelectType_ != 0 && SelectType_ != 1 && SelectType_ != 2) throw new Exception("User_ShowLocationsForm:Incorrect_Function");
     
            if (SelectType_ == 0)
                Button_Select.Visible = false;
            CreateContainerMenuItem = new System.Windows.Forms.MenuItem("انشاء حاية جديدة", CreateContainer_MenuItem_Click);
            CreatePlaceMenuItem = new System.Windows.Forms.MenuItem("انشاء مكان تخزين", CreatePlace_MenuItem_Click);
            OpenContainerMenuItem = new System.Windows.Forms.MenuItem("استعراض", OpenContainer_MenuItem_Click); ;
            EditContainerMenuItem = new System.Windows.Forms.MenuItem("تعديل", EditContainer_MenuItem_Click); ;
            DeleteContainerMenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteContainer_MenuItem_Click); ;
            OpenPlaceMenuItem = new MenuItem("استعراض", OpenPlace_MenuItem_Click);
            EditPlaceMenuItem = new MenuItem("تعديل", EditPlace_MenuItem_Click);
            DeletePlaceMenuItem = new MenuItem("حذف", DeletePlace_MenuItem_Click);
            CutMenuItem = new MenuItem("قص", Cut_MenuItem_Click);
            PasteMenuItem = new MenuItem("لصق", Paste_MenuItem_Click);
            Moved_ContainerList = new List<TradeStoreContainer>();
            Moved_PlaceList = new List<TradeStorePlace>();
            Containersql = new TradeStoreContainerSQL(DB);
            placesql = new TradeStorePlaceSQL(DB);


            Container = f;
            FillComboBoxContainerAccess(Container);
            comboBox1.SelectedIndex = 0;
            comboBoxFilterLocationPlace.SelectedIndex = 0;
            comboBoxFilterLocationPlace.SelectedIndexChanged += new EventHandler(comboBoxFilterItemContainer_SelectedIndexChanged);
            FillTreeViewContainer();
            OpenContainer();

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
        public void FillComboBoxContainerAccess(TradeStoreContainer f)
        {
            try
            {
                comboBoxContainerAccess.Items.Clear();
                if (DB.__User.AccessContainerPremessionList.Count == 0) throw new Exception("لم تمنح اي صلاحية لاستعراض اي حاوية");
                int s = DB.__User.AccessContainerPremessionList.Where(x => x.Container == null).Count();
                if (s > 0)
                {
                    ComboboxItem ComboboxItem_ = new ComboboxItem("جميع الحاويات", 0);
                    comboBoxContainerAccess.Items.Add(ComboboxItem_);
                    comboBoxContainerAccess.Enabled = false;
                    comboBoxContainerAccess.SelectedIndex = 0;


                }
                else
                {
                    int selected_index = 0;
                    for (int i = 0; i < DB.__User.AccessContainerPremessionList.Count; i++)
                    {
                        ComboboxItem ComboboxItem_ = new ComboboxItem(DB.__User.AccessContainerPremessionList[i].Container.ContainerName, DB.__User.AccessContainerPremessionList[i].Container.ContainerID);
                        comboBoxContainerAccess.Items.Add(ComboboxItem_);
                        if (f != null && f.ContainerID == DB.__User.AccessContainerPremessionList[i].Container.ContainerID) selected_index = i;


                    }
                    comboBoxContainerAccess.SelectedIndex = selected_index;
                    if (comboBoxContainerAccess.Items.Count == 1)
                        comboBoxContainerAccess.Enabled = false;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillComboBoxContainerAccess:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
  
         
        }
        private void comboBoxContainerAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem ComboBoxItem_ = (ComboboxItem)comboBoxContainerAccess.SelectedItem;
                if (ComboBoxItem_.Value == 0) RootContainer = null;
                else RootContainer = DB.__User.AccessContainerPremessionList.Where(x => x.Container.ContainerID == ComboBoxItem_.Value).ToList()[0].Container;
                FillTreeViewContainer();
                Container = RootContainer;
                OpenContainer();
            }
            catch (Exception ee)
            {
                MessageBox.Show("comboBoxContainerAccess_SelectedIndexChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        public async  void OpenContainer()
        {
            try
            {
                comboBoxFilterLocationPlace.SelectedIndex = 0;

                textBoxSearch.Text = "";
                //Thread thread1, thread2;
                //thread1 = new Thread(new ThreadStart(RefreshTreeView));
                //thread1.Start();


                //thread2 = new Thread(new ThreadStart(ContainerIDPath));
                //thread2.Start();

                ContainerIDPath();
                RefreshTreeView();
                ContainersListView = Containersql.GetContainerChildsList(Container);
                PlacesListView = placesql.GetPlacesINContainer(Container);
                RefershData(ContainersListView, PlacesListView);
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenContainer:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }
        public async void RefershData(List<TradeStoreContainer > Containers, List<TradeStorePlace> places)
        {
            try
            {
                List<TradeItemStore_Report> TradeItemStore_Report_List
                    = new TradeItemStoreSQL(DB).Get_TradeItemStore_Report_List();
                listView1.Items.Clear();
                if (comboBoxFilterLocationPlace.SelectedIndex != 2)
                {

                    for (int i = 0; i < Containers.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(Containers[i].ContainerName);
                        item.Name = "C" + Containers[i].ContainerID.ToString();
                        item.SubItems.Add("حاوية");
                        item.SubItems.Add(Containers[i].Desc);
                        if (textBoxSearch.Text.Length > 0)
                            item.SubItems.Add(Containersql.GetContainerPath(Containers[i]));
                        else
                            item.SubItems.Add(placesql.GetPlacesINContainer(Containers[i]).Count.ToString() + " مكان تخزين , " + Containersql.GetContainerChildsList(Containers[i]).Count.ToString() + "  حاوية ");
                        item.ImageIndex = 0;
                        item.BackColor = Color.PaleGoldenrod;
                        listView1.Items.Add(item);

                    }
                }

                if (comboBoxFilterLocationPlace.SelectedIndex != 1)
                {

                    for (int i = 0; i < places.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(places[i].PlaceName);
                        item.Name = "P" + places[i].PlaceID.ToString();
                        item.SubItems.Add("مكان تخزين");
                        item.SubItems.Add(places[i].Desc);

                        if (textBoxSearch.Text.Length > 0)
                            item.SubItems.Add(placesql.GetPlacePath(places[i]));
                        else
                        {
                            item.SubItems.Add("عدد أنواع السلع :"
                                + TradeItemStore_Report_List.Where (x=>x.PlaceID ==places [i].PlaceID ).ToList ().Count ());
                        }
                        item.ImageIndex = 1;
                        item.BackColor = Color.Azure;
                        listView1.Items.Add(item);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefershData:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           




        }
        public async void RefreshTreeView()
        {
            try
            {
                if (this.treeViewContainers.InvokeRequired)
                {
                    TreeviewVoidDelegate d = new TreeviewVoidDelegate(RefreshTreeView);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    string fid;
                    if (Container == null) fid = "null";
                    else fid = Container.ContainerID.ToString();
                    TreeNode[] node = treeViewContainers.Nodes.Find(fid, true);
                    if (node.Length == 0) return;
                    node[0].Expand();
                    treeViewContainers.SelectedNode = node[0];
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshTreeView:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        public async void ContainerIDPath()
        {
            try
            {
                if (this.PanelPath.InvokeRequired)
                {
                    TreeviewVoidDelegate d = new TreeviewVoidDelegate(ContainerIDPath);
                    this.Invoke(d, new object[] { });
                }
                else
                {

                    TradeStoreContainer TempContainer = Container;
                    List<TradeStoreContainer> list = new List<TradeStoreContainer>();
                    if (RootContainer  != null && Container != null && Container.ContainerID  == RootContainer .ContainerID )
                        list.Add(Container);
                    else
                    {
                        while (true)
                        {

                            if (TempContainer != null) { list.Add(TempContainer); TempContainer = Containersql.GetParentContainer(TempContainer); }
                            else { list.Add(TempContainer); break; }

                            if (RootContainer != null && TempContainer != null)
                                if (RootContainer.ContainerID == TempContainer.ContainerID )
                                {
                                    list.Add(TempContainer); break;
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
                            b[i].Name = list[j].ContainerID.ToString();
                            b[i].Text = list[j].ContainerName.ToString(); ;
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
                MessageBox.Show("ContainerIDPath:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           


        }
        public async void FillTreeViewContainer()
        {
            try
            {
                List<TradeStoreContainer> ContainersParents = new List<TradeStoreContainer>();

                ContainersParents = Containersql.GetContainerChildsList(RootContainer);
                treeViewContainers.Nodes.Clear();

                TreeNode r = new TreeNode("الجذر");
                if (RootContainer == null)
                {
                    r = new TreeNode("الجذر");
                    r.Name = "null";
                }
                else
                {
                    r = new TreeNode(RootContainer.ContainerName);
                    r.Name = RootContainer.ContainerID.ToString();
                }

                r.ImageIndex = 0;
                treeViewContainers.Nodes.Add(r);
                while (ContainersParents.Count != 0)
                {
                    List<TradeStoreContainer> ContainersChilds = new List<TradeStoreContainer>();
                    for (int i = 0; i < ContainersParents.Count; i++)
                    {
                        TreeNode n = new TreeNode(ContainersParents[i].ContainerName);
                        n.Name = ContainersParents[i].ContainerID.ToString();
                        n.ImageIndex = 0;
                        string parentid = "";
                        if (ContainersParents[i].ParentContainerID == null)
                            parentid = "null";
                        else parentid = ContainersParents[i].ParentContainerID.ToString();
                        TreeNode[] nodes = treeViewContainers.Nodes.Find(parentid, true);
                        nodes[0].Nodes.Add(n);
                        ContainersChilds.AddRange(Containersql.GetContainerChildsList(ContainersParents[i]));

                    }

                    ContainersParents.Clear();
                    ContainersParents = ContainersChilds;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillTreeViewContainer:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Container = Containersql.GetContainerBYID(Convert.ToUInt32(bb.Name));
                }
                catch
                {
                    Container = null;
                }
                OpenContainer();
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
        #region ContextMenuItemEWvents
        private void CreateContainer_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");
                List<TradeStoreContainer> containersInCurrentContainer = Containersql.GetContainerChildsList(Container);
                string name = null;
                int j = 1;
                bool Exists = true;
                name = "حاوية جديدة" + j;
                if (containersInCurrentContainer.Count > 0)
                {
                    while (Exists)
                    {
                        bool found = false;
                        for (int i = 0; i < containersInCurrentContainer.Count; i++)
                        {
                            if (containersInCurrentContainer[i].ContainerName == name)
                                found = true;
                        }
                        if (found == true)
                        { j++; name = "مجلد جديد" + j; }
                        else Exists = false;
                    }
                }
                uint? p_id;
                if (Container == null) p_id = null;
                else p_id = Container.ContainerID;
                ContainerAddForm inp = new ContainerAddForm(this.DB, p_id, name, "");
                DialogResult dd = inp.ShowDialog();
                if (dd == DialogResult.OK)
                {

                    FillTreeViewContainer();
                    OpenContainer();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateContainer_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void OpenContainer_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string id_s = listView1.SelectedItems[0].Name.Substring(1);


                    Container = Containersql.GetContainerBYID(Convert.ToUInt32(id_s));
                    OpenContainer();

            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenContainer_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void EditContainer_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                TradeStoreContainer trmpcontainer = Containersql.GetContainerBYID(Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1)));
                ContainerAddForm inp = new ContainerAddForm(this.DB, trmpcontainer);
                DialogResult dd = inp.ShowDialog();
                if (dd == DialogResult.OK)
                {

                    FillTreeViewContainer();
                    OpenContainer();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditContainer_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        private void DeleteContainer_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                TradeStoreContainer trmpcontainer = Containersql.GetContainerBYID(Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1)));
                DialogResult d = MessageBox.Show("لا يمكن حذف المجلد في حال لم يكن فارغ,هل انت متأكد من حذف المجلد؟", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (d == DialogResult.OK)
                    if (Containersql.DeleteContainer(trmpcontainer))
                    {
                        FillTreeViewContainer();
                        OpenContainer();
                    }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteContainer_MenuItem_Click:"+ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }

           

        }
        private void Paste_MenuItem_Click(object sender, EventArgs e)
        {
            //if(Container==null && Moved_ItemList.Count >0)
            //{
            //    MessageBox.Show("لا يمكن نقل العناصر للمجلد الجذر","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    return;
            //}
            // Containersql.MoveContainers(Container, Moved_ContainerList).ToString ();
            //itemsql.MoveItems(Container, Moved_ItemList);
            //FillTreeViewContainer();
            //OpenContainer();

        }
        private void Cut_MenuItem_Click(object sender, EventArgs e)
        {

            //Moved_ContainerList.Clear();
            //Moved_ItemList.Clear();
            //Move_SourceContainer = Container;
            //for(int i=0;i<listView1 .SelectedItems .Count;i++)
            //{
            //    if (listView1.SelectedItems[i].SubItems[2].Text == "مجلد")
            //    {
            //        Moved_ContainerList.Add(Containersql.GetContainerInfoByID(Convert .ToUInt32 (listView1.SelectedItems[i].Name )));
            //    }
            //    else
            //    {
            //        Moved_ItemList.Add(itemsql .GetItemInfoByID(Convert.ToUInt32(listView1.SelectedItems[i].Name)));
            //    }
            //}
            //if(Moved_ItemList.Count >0)
            //{
            //    DialogResult d = MessageBox.Show("نقل العناصر سيؤدي الى حذف جميع قيم الخصائص المضبوطة للعناصر المنقولة, هل انت متاكد من الاستمرار بلعملية؟", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    if(d!=DialogResult.OK)
            //    {
            //        Moved_ContainerList.Clear();
            //        Moved_ItemList.Clear();
            //        return;
            //    }
            //}

        }
        private void CreatePlace_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                AddPlaceForm addplaceform = new AddPlaceForm(DB, Container);
                DialogResult d = addplaceform.ShowDialog();
                if (d == DialogResult.OK)
                {
                    //ItemForm itemform = new ItemForm(DB, itemsql.GetItemInfoByName(itemadd.textBoxItemName.Text, itemadd.textBoxCompanyName.Text));
                    //itemform.ShowDialog();
                    PlacesListView = placesql.GetPlacesINContainer(Container);
                    RefershData(ContainersListView, PlacesListView);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreatePlace_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void OpenPlace_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                    TradeStorePlace place = placesql.GetTradeStorePlaceBYID(Convert.ToUInt32(id_s1));
                    Place_ExistsItems_Form placeform = new Place_ExistsItems_Form(DB, place, false);
                    placeform.ShowDialog();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenPlace_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void EditPlace_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                TradeStorePlace place = placesql.GetTradeStorePlaceBYID(Convert.ToUInt32(id_s1));
                AddPlaceForm editplaceform = new AddPlaceForm(DB, place);
                DialogResult d = editplaceform.ShowDialog();
                if (d == DialogResult.OK)
                {
                    //ItemForm itemform = new ItemForm(DB, itemsql.GetItemInfoByName(itemadd.textBoxItemName.Text, itemadd.textBoxCompanyName.Text));
                    //itemform.ShowDialog();
                    PlacesListView = placesql.GetPlacesINContainer(Container);
                    RefershData(ContainersListView, PlacesListView);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditPlace_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void DeletePlace_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Container_Group(DB.__User.UserID))) throw new Exception("لا تملك الصلاحية لتنفيذ هذه الإجراء");

                DialogResult d = MessageBox.Show(" هل أنت متاكد من اتمام عملية الحذف؟  ", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (d != DialogResult.OK) return;
                string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                TradeStorePlace place = placesql.GetTradeStorePlaceBYID(Convert.ToUInt32(id_s1));
                bool success = placesql.DeletePlace(place);
                if (success)
                {
                    PlacesListView = placesql.GetPlacesINContainer(Container);
                    RefershData(ContainersListView, PlacesListView);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeletePlace_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 
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


        private void Back_Click(object sender, EventArgs e)
        {
            if (Container == null) return;
            if (RootContainer != null) if (Container.ContainerID  == RootContainer .ContainerID ) return;
            Container = Containersql.GetParentContainer(Container);
            OpenContainer();
        }
        private void comboBoxFilterItemContainer_SelectedIndexChanged(object sender, EventArgs e)
        {

            RefershData(ContainersListView ,PlacesListView );
        }


        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSearch.Text.Length > 0)
                {

                    if (checkBoxSearchType.Checked == false)
                    {
                        ContainersListView = Containersql.SearchContainer(textBoxSearch.Text);
                        PlacesListView = placesql.SearchPlace(textBoxSearch.Text);
                        RefershData(ContainersListView, PlacesListView);
                    }
                    else
                    {
                        PlacesListView = placesql.SearchPlacesInContainer(Container, textBoxSearch.Text);
                        RefershData(ContainersListView, PlacesListView);
                    }
                }
                else OpenContainer();
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


                    if (checkBoxSearchType.Checked == false)
                    {
                        ContainersListView = Containersql.SearchContainer(textBoxSearch.Text);
                        PlacesListView = placesql.SearchPlace(textBoxSearch.Text);
                        RefershData(ContainersListView, PlacesListView);
                    }
                    else
                    {
                        PlacesListView = placesql.SearchPlacesInContainer(Container, textBoxSearch.Text);
                        RefershData(ContainersListView, PlacesListView);
                    }
                }
                else OpenContainer();
            }
            catch (Exception ee)
            {
                MessageBox.Show("checkBoxSearchType_CheckedChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

       
    

        private void ShowItemsForm_Load(object sender, EventArgs e)
        {

            textBoxSearch.Focus();

        }

        private void Button_Select_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count ==1 && listView1.SelectedItems[0].Name .Substring (0,1) =="P"&& SelectType ==SELECT_Place)
            {
                ReturPlace();
            }
            else
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Name.Substring(0, 1) == "C" && SelectType == SELECT_Container)
            {
                ReturnContainer_();
            }
        }
        public void ReturPlace()
        {
            try
            {
                _ReturnPlace = new TradeStorePlaceSQL(DB).GetTradeStorePlaceBYID((Convert.ToUInt32(listView1.SelectedItems[0].SubItems[0].Name.Substring(1))));
                if (_ReturnPlace != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else MessageBox.Show("حصل خطأ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ee)
            {
                MessageBox.Show("ReturPlace:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
        public void ReturnContainer_()
        {
            try
            {
                _ReturnContainer = Containersql.GetContainerBYID(Convert.ToUInt32(listView1.SelectedItems[0].SubItems[0].Name.Substring(1)));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("ReturnContainer_:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
     
        }

        private void treeViewContainers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (treeViewContainers.SelectedNode != null)
                    {
                        try
                        {
                            Container = Containersql.GetContainerBYID(Convert.ToUInt32(treeViewContainers.SelectedNode.Name));
                        }
                        catch
                        {
                            Container = null;
                        }

                        OpenContainer();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("treeViewContainers_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        
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

                        if (listitem.SubItems[1].Text == "حاوية")
                        {
                            MenuItem[] mi1 = new MenuItem[] {OpenContainerMenuItem ,EditContainerMenuItem  ,DeleteContainerMenuItem
                            ,new MenuItem ("-")  };
                            MenuItemList.AddRange(mi1);
                        }
                        else
                        {
                            MenuItem[] mi1 = new MenuItem[] {OpenPlaceMenuItem ,EditPlaceMenuItem  ,DeletePlaceMenuItem
                            ,new MenuItem ("-") };
                            MenuItemList.AddRange(mi1);

                        }
                        MenuItemList.Add(CutMenuItem);

                    }
                    //////////////

                    if (Moved_PlaceList.Count > 0 || Moved_ContainerList.Count > 0)
                    {
                        MenuItemList.Add(PasteMenuItem);
                    }
                    MenuItem[] m_i = new MenuItem[] { new MenuItem("-"), CreateContainerMenuItem, CreatePlaceMenuItem, new MenuItem("-") };
                    MenuItemList.AddRange(m_i);
                    if (Container == null)
                    {
                        CreatePlaceMenuItem.Enabled = false;
                    }
                    else
                    {
                        CreatePlaceMenuItem.Enabled = true;
                    }
                    listView1.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         

        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
                {

                    switch (listView1.SelectedItems[0].Name.Substring(0, 1))
                    {
                        case "C":
                            string id_s = listView1.SelectedItems[0].Name.Substring(1);
                            try
                            {
                                Container = Containersql.GetContainerBYID(Convert.ToUInt32(id_s));
                                OpenContainer();
                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show(ee.Message);
                            }
                            break;
                        case "P":
                            try
                            {
                                string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                                TradeStorePlace place = placesql.GetTradeStorePlaceBYID(Convert.ToUInt32(id_s1));
                                if (SelectType == SELECT_Place)
                                {

                                    _ReturnPlace = place;
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();

                                }
                                else
                                {
                                    Place_ExistsItems_Form placeform = new Place_ExistsItems_Form(DB, place, false);
                                    placeform.ShowDialog();
                                }

                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show(ee.Message);
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
    }
  
}
