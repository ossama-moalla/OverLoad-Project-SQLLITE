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
using OverLoad_Client.Company.CompanySQL;
using OverLoad_Client.Company.Objects;

namespace OverLoad_Client.Company.Forms
{
    public partial class ShowPartsForm : Form
    {
        System.Windows.Forms.MenuItem CreatePartMenuItem;
        System.Windows.Forms.MenuItem CreateEmployeeMentMenuItem;

        System.Windows.Forms.MenuItem OpenPartMenuItem;
        System.Windows.Forms.MenuItem EditPartMenuItem;
        System.Windows.Forms.MenuItem DeletePartMenuItem;


        System.Windows.Forms.MenuItem OpenEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem EditEmployeeMentMenuItem;
        System.Windows.Forms.MenuItem DeleteEmployeeMentMenuItem;

        System.Windows.Forms.MenuItem CutMenuItem;
        System.Windows.Forms.MenuItem PasteMenuItem;

        public const uint SHOW_Data = 0;
        public const uint SELECT_Part = 1;
        public const uint SELECT_EmployeeMent = 2;
        DatabaseInterface DB;
        Part Part;
        List<Part> PartsListView = new List<Part>();
        List<EmployeeMent> EmployeeMentsListView = new List<EmployeeMent>();
        PartSQL Partsql;
        EmployeeMentSQL EmployeeMentsql;
        Button front, end;
        int Path_startIndex = 0;
        Button[] b;
        List<Part> Moved_PartList;
        List<EmployeeMent> Moved_EmployeeMentList ;

        //Folder Move_SourceFolder;
 
        private EmployeeMent _ReturnEmployeeMent;
        public EmployeeMent ReturnEmployeeMent
        {
            get { return _ReturnEmployeeMent; }
        }

        private Part _ReturnPart;
        public Part ReturnPart
        {
            get { return _ReturnPart; }
        }
        delegate void TreeviewVoidDelegate();
        uint SelectType;
        public ShowPartsForm(DatabaseInterface db,Part f,uint SelectType_)
        {
            InitializeComponent();
            Moved_EmployeeMentList = new List<EmployeeMent>();
            if (SelectType_ != 0 && SelectType_ != 1 && SelectType_ != 2) throw new Exception("User_ShowLocationsForm:Incorrect_Function");

            if (SelectType_ == 0)
                Button_Select.Visible = false;
            SelectType = SelectType_;
              CreatePartMenuItem   = new System.Windows.Forms.MenuItem("انشاء حاية جديدة", CreatePart_MenuItem_Click);
            CreateEmployeeMentMenuItem  = new System.Windows.Forms.MenuItem("انشاء مكان تخزين", CreateEmployeeMent_MenuItem_Click);
            OpenPartMenuItem   = new System.Windows.Forms.MenuItem("استعراض", OpenPart_MenuItem_Click); ;
            EditPartMenuItem   = new System.Windows.Forms.MenuItem("تعديل", EditPart_MenuItem_Click); ;
            DeletePartMenuItem   = new System.Windows.Forms.MenuItem("حذف", DeletePart_MenuItem_Click); ;
            OpenEmployeeMentMenuItem  = new MenuItem("استعراض", OpenEmployeeMent_MenuItem_Click);
            EditEmployeeMentMenuItem  = new MenuItem("تعديل", EditEmployeeMent_MenuItem_Click);
            DeleteEmployeeMentMenuItem  = new MenuItem("حذف", DeleteEmployeeMent_MenuItem_Click);

            DB = db;
            Partsql  = new PartSQL (DB);
            EmployeeMentsql  = new EmployeeMentSQL (DB);
       
            Part  = f;
            comboBoxFilterLocationEmployeeMent.SelectedIndex = 0;
            comboBoxFilterLocationEmployeeMent.SelectedIndexChanged += new EventHandler(comboBoxFilterItemFolder_SelectedIndexChanged);
            FillTreeViewPart();
            OpenPart();
           
            front = new Button();
             end = new Button();
            front.Font = new Font(front.Font.FontFamily, 6 );
            
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
            OptimizeDatagridVeiwSpec_Columns_Width();
            listView1.Focus();
        }
        public void OpenPart()
        {
            try
            {
                comboBoxFilterLocationEmployeeMent.SelectedIndex = 0;

                textBoxSearch.Text = "";
                //Thread thread1, thread2;
                //thread1 = new Thread(new ThreadStart(RefreshTreeView));
                //thread1.Start();


                //thread2 = new Thread(new ThreadStart(FolderIDPath));
                //thread2.Start();

                PartIDPath();
                RefreshTreeView();
                PartsListView = Partsql.GetPartChilds(Part);
                EmployeeMentsListView = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(Part);
                RefreshList(PartsListView, EmployeeMentsListView);
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenPart:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
          }       

        public void RefreshList(List <Part  > Parts,List <EmployeeMent> EmployeeMents)
        {
            try
            {
                listView1.Items.Clear();
                if (comboBoxFilterLocationEmployeeMent.SelectedIndex != 2)
                {

                    for (int i = 0; i < Parts.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(Parts[i].PartName);
                        item.Name = "P" + Parts[i].PartID.ToString();
                        item.SubItems.Add("قسم");

                        if (textBoxSearch.Text.Length > 0)
                            item.SubItems.Add(Partsql.GetPartPath(Parts[i]));
                        else
                            item.SubItems.Add(EmployeeMentsql.Get_EmployeeMent_List_IN_Part(Parts[i]).Count.ToString() + " وظيفة , " + Partsql.GetPartChilds(Parts[i]).Count.ToString() + "  قسم ");
                        item.ImageIndex = 0;
                        item.BackColor = Color.PaleGoldenrod;
                        listView1.Items.Add(item);

                    }
                }

                if (comboBoxFilterLocationEmployeeMent.SelectedIndex != 1)
                {

                    for (int i = 0; i < EmployeeMents.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(EmployeeMents[i].EmployeeMentName);
                        item.Name = "E" + EmployeeMents[i].EmployeeMentID.ToString();
                        item.SubItems.Add("وظيفة");


                        if (textBoxSearch.Text.Length > 0)
                            item.SubItems.Add(EmployeeMentsql.GetEmployeeMentPath(EmployeeMents[i]));
                        //else
                        //{
                        //    item.SubItems.Add("عدد أنواع السلع :"+new AvailableItemSQL(DB).GetStored_TradeItems(EmployeeMents[i] ).Select (x=>x._Item.ItemID).ToList() .Count .ToString ());
                        //}
                        item.ImageIndex = 1;
                        item.BackColor = Color.Azure;
                        listView1.Items.Add(item);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshList:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          





        }
        public void RefreshTreeView()
        {
            try
            {
                if (this.treeViewParts.InvokeRequired)
                {
                    TreeviewVoidDelegate d = new TreeviewVoidDelegate(RefreshTreeView);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    string fid;
                    if (Part == null) fid = "null";
                    else fid = Part.PartID.ToString();
                    TreeNode[] node = treeViewParts.Nodes.Find(fid, true);
                    if (node.Length == 0) return;
                    node[0].Expand();
                    treeViewParts.SelectedNode = node[0];
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshTreeView:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
          
        }
        public void PartIDPath()
        {
            try
            {
                if (this.PanelPath.InvokeRequired)
                {
                    TreeviewVoidDelegate d = new TreeviewVoidDelegate(PartIDPath);
                    this.Invoke(d, new object[] { });
                }
                else
                {

                    Part TempPart = Part;
                    List<Part> list = new List<Part>();
                    while (true)
                    {

                        if (TempPart != null) { list.Add(TempPart); TempPart = Partsql.GetParentPart(TempPart); }
                        else { list.Add(TempPart); break; }


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
                            b[i].Name = list[j].PartID.ToString();
                            b[i].Text = list[j].PartName.ToString(); ;
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
                MessageBox.Show("PartIDPath:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

          

        }
        public void FillTreeViewPart()
        {
            try
            {
                List<Part> PartsParents = new List<Part>();
                PartsParents = Partsql.GetPartChilds(null);
                treeViewParts.Nodes.Clear();
                TreeNode r = new TreeNode("الشركة");
                r.Name = "null";
                r.ImageIndex = 0;
                treeViewParts.Nodes.Add(r);
                while (PartsParents.Count != 0)
                {
                    List<Part> PartsChilds = new List<Part>();
                    for (int i = 0; i < PartsParents.Count; i++)
                    {
                        TreeNode n = new TreeNode(PartsParents[i].PartName);
                        n.Name = PartsParents[i].PartID.ToString();
                        n.ImageIndex = 0;
                        string parentid = "";
                        if (PartsParents[i].ParentPartID == null)
                            parentid = "null";
                        else parentid = PartsParents[i].ParentPartID.ToString();
                        TreeNode[] nodes = treeViewParts.Nodes.Find(parentid, true);
                        nodes[0].Nodes.Add(n);
                        PartsChilds.AddRange(Partsql.GetPartChilds(PartsParents[i]));

                    }

                    PartsParents.Clear();
                    PartsParents = PartsChilds;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("FillTreeViewPart:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
       

        }
        private void OptimizePath()
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
                    Part = Partsql.GetPartInfoByID(Convert.ToUInt32(bb.Name));
                }
                catch
                {
                    Part = null;
                }
                OpenPart();
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
        private void CreatePart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Part> PartsInCurrentPart = Partsql.GetPartChilds(Part);
                string name = null;
                int j = 1;
                bool Exists = true;
                name = "قسم جديد " + j;
                if (PartsInCurrentPart.Count > 0)
                {
                    while (Exists)
                    {
                        bool found = false;
                        for (int i = 0; i < PartsInCurrentPart.Count; i++)
                        {
                            if (PartsInCurrentPart[i].PartName == name)
                                found = true;
                        }
                        if (found == true)
                        { j++; name = "مجلد جديد" + j; }
                        else Exists = false;
                    }
                }
                uint? p_id;
                if (Part == null) p_id = null;
                else p_id = Part.PartID;
                PartForm inp = new PartForm(this.DB, p_id, name);
                DialogResult dd = inp.ShowDialog();
                if (dd == DialogResult.OK)
                {

                    FillTreeViewPart();
                    OpenPart();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreatePart_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
            
        }
       
        private void OpenPart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string id_s = listView1.SelectedItems[0].Name.Substring(1);


                Part = Partsql.GetPartInfoByID(Convert.ToUInt32(id_s));
                OpenPart();
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenPart_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        private void EditPart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Part trmpPart = Partsql.GetPartInfoByID(Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1)));
                PartForm inp = new PartForm(this.DB, trmpPart, true);
                DialogResult dd = inp.ShowDialog();
                if (dd == DialogResult.OK)
                {

                    FillTreeViewPart();
                    OpenPart();

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditPart_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        private void DeletePart_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Part trmpPart = Partsql.GetPartInfoByID(Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1)));
                DialogResult d = MessageBox.Show("لا يمكن حذف المجلد في حال لم يكن فارغ,هل انت متأكد من حذف المجلد؟", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (d == DialogResult.OK)
                    if (Partsql.DeletePart(trmpPart.PartID))
                    {
                        FillTreeViewPart();
                        OpenPart();
                    }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeletePart_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          

        }

        private void CreateEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeMent_Form addEmployeeMentform = new EmployeeMent_Form(DB, Part);
                DialogResult d = addEmployeeMentform.ShowDialog();
                if (d == DialogResult.OK)
                {
                    //ItemForm itemform = new ItemForm(DB, itemsql.GetItemInfoByName(itemadd.textBoxItemName.Text, itemadd.textBoxCompanyName.Text));
                    //itemform.ShowDialog();
                    EmployeeMentsListView = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(Part);
                    RefreshList(PartsListView, EmployeeMentsListView);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("CreateEmployeeMent_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
       
        }
        private void OpenEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                    EmployeeMent EmployeeMent = EmployeeMentsql.Get_EmployeeMent_InfoBYID(Convert.ToUInt32(id_s1));
                    EmployeeMent_Form EmployeeMentform = new EmployeeMent_Form(DB, EmployeeMent, false);
                    EmployeeMentform.ShowDialog();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenEmployeeMent_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void EditEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                EmployeeMent EmployeeMent = EmployeeMentsql.Get_EmployeeMent_InfoBYID(Convert.ToUInt32(id_s1));
                EmployeeMent_Form editEmployeeMentform = new EmployeeMent_Form(DB, EmployeeMent, true);
                DialogResult d = editEmployeeMentform.ShowDialog();
                if (d == DialogResult.OK)
                {
                    //ItemForm itemform = new ItemForm(DB, itemsql.GetItemInfoByName(itemadd.textBoxItemName.Text, itemadd.textBoxCompanyName.Text));
                    //itemform.ShowDialog();
                    EmployeeMentsListView = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(Part);
                    RefreshList(PartsListView, EmployeeMentsListView);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditEmployeeMent_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          
        }
        private void DeleteEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show(" هل أنت متاكد من اتمام عملية الحذف؟  ", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (d != DialogResult.OK) return;
                string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                EmployeeMent EmployeeMent = EmployeeMentsql.Get_EmployeeMent_InfoBYID(Convert.ToUInt32(id_s1));
                bool success = EmployeeMentsql.Delete_EmployeeMent(EmployeeMent.EmployeeMentID);
                if (success)
                {
                    EmployeeMentsListView = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(Part);
                    RefreshList(PartsListView, EmployeeMentsListView);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteEmployeeMent_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         
        }

        #endregion

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

                        if (listitem.Name.Substring(0, 1) == "P")
                        {
                            MenuItem[] mi1 = new MenuItem[] {OpenPartMenuItem ,EditPartMenuItem  ,DeletePartMenuItem
                            ,new MenuItem ("-")  };
                            MenuItemList.AddRange(mi1);
                        }
                        else if (listitem.Name.Substring(0, 1) == "E")
                        {
                            MenuItem[] mi1 = new MenuItem[] {OpenEmployeeMentMenuItem ,EditEmployeeMentMenuItem  ,DeleteEmployeeMentMenuItem
                            ,new MenuItem ("-") };
                            MenuItemList.AddRange(mi1);

                        }
                        //MenuItemList.Add(CutMenuItem);

                    }
                    //////////////

                    //if (Moved_EmployeeMentList.Count > 0 || Moved_PartList.Count > 0)
                    //{
                    //    MenuItemList.Add(PasteMenuItem);
                    //}
                    MenuItem[] m_i = new MenuItem[] { new MenuItem("-"), CreatePartMenuItem, CreateEmployeeMentMenuItem, new MenuItem("-") };
                    MenuItemList.AddRange(m_i);
                    if (Part == null)
                    {
                        CreateEmployeeMentMenuItem.Enabled = false;
                    }
                    else
                    {
                        CreateEmployeeMentMenuItem.Enabled = true;
                    }
                    listView1.ContextMenu = new ContextMenu(MenuItemList.ToArray());

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listView1_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                        case "P":
                            string id_s = listView1.SelectedItems[0].Name.Substring(1);
                            try
                            {
                                Part = Partsql.GetPartInfoByID(Convert.ToUInt32(id_s));
                                OpenPart();
                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show(ee.Message);
                            }
                            break;
                        case "E":
                            try
                            {
                                string id_s1 = listView1.SelectedItems[0].Name.Substring(1);
                                EmployeeMent EmployeeMent = EmployeeMentsql.Get_EmployeeMent_InfoBYID(Convert.ToUInt32(id_s1));
                                if (SelectType == SELECT_EmployeeMent)
                                {
                                    _ReturnEmployeeMent = EmployeeMent;
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();

                                }
                                else
                                {
                                    EmployeeMent_Form EmployeeMentform = new EmployeeMent_Form(DB, EmployeeMent, false);
                                    EmployeeMentform.ShowDialog();
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

        private void Back_Click(object sender, EventArgs e)
        {
            try
            {
                if (Part == null) return;
                Part = Partsql.GetParentPart(Part);
                OpenPart();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Back_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        
        }



        private void comboBoxFilterItemFolder_SelectedIndexChanged(object sender, EventArgs e)
        {

            RefreshList (PartsListView ,EmployeeMentsListView );
        }



        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSearch.Text.Length > 0)
                {
                    splitPart1.Panel2Collapsed = true;

                    PartsListView = Partsql.SearchPart(textBoxSearch.Text);
                    EmployeeMentsListView = EmployeeMentsql.SearchEmployeeMent(textBoxSearch.Text);
                    RefreshList(PartsListView, EmployeeMentsListView);
                }
                else OpenPart();
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxSearch_TextChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }


  

        //private void dataGridViewSpec_Resize(object sender, EventArgs e)
        //{
        //    OptimizeDatagridVeiwSpec_Columns_Width();
        //}
        public void OptimizeDatagridVeiwSpec_Columns_Width()
        {
            //dataGridViewSpec.RowHeadersWidth = dataGridViewSpec.Width / 2;
            //dataGridViewSpec.Columns[0].Width = dataGridViewSpec.Width / 2;
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Back)
                {
                    if (Part == null) return;
                    Part = Partsql.GetParentPart(Part);
                    OpenPart();
                    return;
                }
                if (listView1.SelectedItems.Count > 0)
                {

                    switch (e.KeyData)
                    {
                        case Keys.Enter:
                            switch (listView1.SelectedItems[0].Name.Substring(0, 1))
                            {
                                case "P":
                                    string id_s = listView1.SelectedItems[0].Name.Substring(1);
                                    try
                                    {
                                        Part = Partsql.GetPartInfoByID(Convert.ToUInt32(id_s));
                                        OpenPart();
                                    }
                                    catch (Exception ee)
                                    {
                                        MessageBox.Show(ee.Message);
                                    }
                                    break;
                                case "E":
                                    if (SelectType == SELECT_EmployeeMent)
                                    {

                                        __ReturnEmployeeMent();

                                    }
                                    else
                                    {
                                        EmployeeMent EmployeeMent__ = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID((Convert.ToUInt32(listView1.SelectedItems[0].SubItems[0].Name.Substring(1))));

                                        EmployeeMent_Form EmployeeMentform = new EmployeeMent_Form(DB, EmployeeMent__, false);
                                        EmployeeMentform.ShowDialog();
                                    }
                                    break;
                            }
                            break;
                        case Keys.Delete:

                            switch (listView1.SelectedItems[0].Name.Substring(0, 1))
                            {
                                case "P":
                                    DeletePartMenuItem.PerformClick();
                                    break;
                                case "E":
                                    DeleteEmployeeMentMenuItem.PerformClick();
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

        private void Select_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectType == SELECT_EmployeeMent && listView1.SelectedItems[0].Name.Substring(0, 1) != "E") throw new Exception("يرجى تحديد وظيفة");
                if (SelectType == SELECT_Part && listView1.SelectedItems[0].Name.Substring(0, 1) != "P") throw new Exception("يرجى تحديد قسم");

                if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Name.Substring(0, 1) == "E" && SelectType == SELECT_EmployeeMent)
                {
                    __ReturnEmployeeMent();
                }
                else
       if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Name.Substring(0, 1) == "P" && SelectType == SELECT_Part)
                {
                    __ReturnPart();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Select_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
       
        }
        public void __ReturnEmployeeMent()
        {
            try
            {
                _ReturnEmployeeMent = new EmployeeMentSQL(DB).Get_EmployeeMent_InfoBYID((Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1))));
                if (_ReturnEmployeeMent != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else throw new Exception ( "حصل خطا");
            }
            catch (Exception ee)
            {
                MessageBox.Show("__ReturnEmployeeMent:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        
          
        }
        public void __ReturnPart()
        {
            try
            {
                _ReturnPart = new PartSQL(DB).GetPartInfoByID((Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1))));
                if (_ReturnPart != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else throw new Exception("حصل خطا");

            }
            catch (Exception ee)
            {
                 MessageBox.Show(":"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
            }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeViewFolders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (treeViewParts.SelectedNode != null)
                    {
                        try
                        {
                            Part = Partsql.GetPartInfoByID(Convert.ToUInt32(treeViewParts.SelectedNode.Name));
                        }
                        catch
                        {
                            Part = null;
                        }

                        OpenPart();
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
