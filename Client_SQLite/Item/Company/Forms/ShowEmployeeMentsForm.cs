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
using OverLoad_Client.Company.Objects;
using OverLoad_Client.Company.CompanySQL;

namespace OverLoad_Client.Company.Forms
{
    public partial class ShowEmployeeMentsForm : Form
    {
        System.Windows.Forms.MenuItem  CreatePartMenuItem;
        System.Windows.Forms.MenuItem CreateEmployeeMentMenuItem;

        System.Windows.Forms.MenuItem OpenMenuItem;
        System.Windows.Forms.MenuItem EditMenuItem;
        System.Windows.Forms.MenuItem DeleteMenuItem;




        System.Windows.Forms.MenuItem CutMenuItem;
        System.Windows.Forms.MenuItem PasteMenuItem;


        DatabaseInterface DB;
        Part _Part;
        List<Part> PartsList = new List<Part>();
        List<EmployeeMent> EmployeeMentsList = new List<EmployeeMent>();
        PartSQL Partsql;
        EmployeeMentSQL EmployeeMentsql;
        Button front, end;
        int Path_startIndex = 0;
        Button[] b;
    
        List<Part> Moved_PartList;
        List<EmployeeMent> Moved_EmployeeMentList;

        Part Move_SourcePart;


        delegate void TreeviewVoidDelegate();
        public ShowEmployeeMentsForm(DatabaseInterface db, Part f)
        {
            InitializeComponent();

            CreatePartMenuItem = new System.Windows.Forms.MenuItem("انشاء قسم", CreatePart_MenuItem_Click);
            CreateEmployeeMentMenuItem = new System.Windows.Forms.MenuItem("انشاء وظيفة", CreateEmployeeMent_MenuItem_Click);
            OpenMenuItem = new System.Windows.Forms.MenuItem("فتح", Open_MenuItem_Click); ;
            EditMenuItem = new System.Windows.Forms.MenuItem("تعديل", Edit_MenuItem_Click); ;
            DeleteMenuItem = new System.Windows.Forms.MenuItem("حذف", Delete_MenuItem_Click); ;
            CutMenuItem = new MenuItem("قص", Cut_MenuItem_Click);
            PasteMenuItem = new MenuItem("لصق", Paste_MenuItem_Click);
            Moved_PartList = new List<Part>();
            Moved_EmployeeMentList = new List<EmployeeMent>();
            DB = db;
            //if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID) || DB.IS_Belong_To_Employee_Group(DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء أو مجموعة ادارة الموظفين, لا يمكنك فتح هذه النافذة");

            Partsql = new PartSQL(DB);
            EmployeeMentsql = new EmployeeMentSQL(DB);

            _Part = f;

            comboBox1.SelectedIndex = 0;
            comboBoxFilter.SelectedIndex = 0;
            comboBoxFilter.SelectedIndexChanged += new EventHandler(comboBoxFilter_SelectedIndexChanged);
            FillTreeViewPart();
            OpenPart();

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
            listView1.Focus();
            AdjustListviewColumnWidth();
        }
        public async  void OpenPart()
        {
            comboBoxFilter.SelectedIndex = 0;

            textBoxSearch.Text = "";
            //Thread thread1, thread2;
            //thread1 = new Thread(new ThreadStart(RefreshTreeView));
            //thread1.Start();


            //thread2 = new Thread(new ThreadStart(PartIDPath));
            //thread2.Start();

            PartIDPath();
            RefreshTreeView();
            PartsList = Partsql.GetPartChilds(_Part);
            EmployeeMentsList = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(_Part);
            RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);
  

        }
    
      
       
        public async void RefreshEmployeeMents_Parts(List<Part> Parts, List<EmployeeMent> EmployeeMents)
        {
            listView1.Items .Clear();
            if (comboBoxFilter.SelectedIndex != 2)
            {

                for (int i = 0; i < Parts.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem(Parts[i].PartName);
                    ListViewItem_.Name ="P"+ Parts[i].PartID.ToString();
                    ListViewItem_.SubItems.Add("قسم");
                    ListViewItem_.SubItems.Add(Parts[i].CreateDate.ToShortDateString ());
                    if (textBoxSearch.Text.Length > 0)
                        ListViewItem_.SubItems.Add(Partsql.GetPartPath(Parts[i]));
                    else
                        ListViewItem_.SubItems.Add(EmployeeMentsql.GetEmployeeMentsCountInPart(Parts[i]).ToString() + " وظيفة , " + Partsql.GetPartChilds(Parts[i]).Count.ToString() + "  قسم ");
                    ListViewItem_.ImageIndex = 1;
                    ListViewItem_.BackColor = Color.Aqua;

                    listView1.Items .Add(ListViewItem_);

                }
            }

            if (comboBoxFilter.SelectedIndex != 1)
            {

   
                for (int i = 0; i < EmployeeMents.Count; i++)
                {
                    ListViewItem ListViewItem__ = new ListViewItem(EmployeeMents[i].EmployeeMentName);
                    ListViewItem__.Name ="E"+ EmployeeMents[i].EmployeeMentID.ToString();
                    ListViewItem__.SubItems.Add("وظيفة");
                    ListViewItem__.SubItems.Add(EmployeeMents[i].CreateDate.ToShortDateString());
                    if (textBoxSearch.Text.Length > 0)
                        ListViewItem__.SubItems.Add(EmployeeMentsql.GetEmployeeMentPath(EmployeeMents[i]));
                    ListViewItem__.BackColor = Color.LimeGreen;
                    ListViewItem__.ImageIndex = 0;
                    listView1.Items .Add(ListViewItem__);

                }

            }
        }
        public async void RefreshTreeView()
        {
            if (this.treeViewParts.InvokeRequired)
            {
                TreeviewVoidDelegate d = new TreeviewVoidDelegate(RefreshTreeView);
                this.Invoke(d, new object[] { });
            }
            else
            {
                string fid;
                if (_Part == null) fid = "null";
                else fid = _Part.PartID.ToString();
                TreeNode[] node = treeViewParts.Nodes.Find(fid, true);
                if (node.Length == 0) return;
                node[0].Expand();
                treeViewParts.SelectedNode = node[0];
            }

        }
        public async void PartIDPath()
        {

            if (this.PanelPath.InvokeRequired)
            {
                TreeviewVoidDelegate d = new TreeviewVoidDelegate(PartIDPath);
                this.Invoke(d, new object[] { });
            }
            else
            {

                Part TempPart = _Part;
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
        public async void FillTreeViewPart()
        {
            List<Part> PartsParents = new List<Part>();

            PartsParents = Partsql.GetPartChilds(null);
            treeViewParts.Nodes.Clear();
            TreeNode r = new TreeNode(DB.COMPANY.PartName);
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
        private async void OptimizePath()
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
        private void Button_Path_Click(object sender, EventArgs e)
        {

            Button bb = (Button)sender;
            try
            {
                _Part = Partsql.GetPartInfoByID(Convert.ToUInt32(bb.Name));
            }
            catch
            {
                _Part = null;
            }
            OpenPart();
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
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
            {
                OpenMenuItem.PerformClick();
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                List<MenuItem> MenuItemList = new List<MenuItem>();
                listView1.ContextMenu = null;
                bool match = false;
                ListViewItem listEmployeeMent = new ListViewItem();
                foreach (ListViewItem EmployeeMent1 in listView1.Items )
                {
                    if (EmployeeMent1.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        match = true;
                        listEmployeeMent = EmployeeMent1;
                        break;
                    }
                }
                if (match)
                {

                        MenuItem[] mi1 = new MenuItem[] {OpenMenuItem ,EditMenuItem ,DeleteMenuItem 
                            ,new MenuItem ("-")  };
                        MenuItemList.AddRange(mi1);
  
                    MenuItemList.Add(CutMenuItem);

                }
                //////////////

                if (Moved_EmployeeMentList.Count > 0 || Moved_PartList.Count > 0)
                {
                    MenuItemList.Add(PasteMenuItem);
                }
                MenuItem[] m_i = new MenuItem[] { new MenuItem("-"), CreatePartMenuItem, CreateEmployeeMentMenuItem, new MenuItem("-") };
                MenuItemList.AddRange(m_i);
                if (_Part == null)
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
        #region ContextMenuItemEWvents
        private void CreatePart_MenuItem_Click(object sender, EventArgs e)
        {

            List<Part> PartsInCurrentPart = Partsql.GetPartChilds(_Part);
            string name = null;
            int j = 1;
            bool Exists = true;
            name = "مجلد جديد" + j;
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
                    { j++; name = "قسم جديد " + j; }
                    else Exists = false;
                }
            }
            uint? p_id;
            if (_Part == null) p_id = null;
            else p_id = _Part.PartID;
            PartForm inp = new PartForm(this.DB, p_id, name);
            DialogResult dd = inp.ShowDialog();
            if (dd == DialogResult.OK)
            {

                FillTreeViewPart();
                OpenPart();

            }

        }
        private void CreateEmployeeMent_MenuItem_Click(object sender, EventArgs e)
        {
            EmployeeMent_Form  EmployeeMent_Form = new EmployeeMent_Form(DB, _Part);
            DialogResult d = EmployeeMent_Form.ShowDialog();
            if (d == DialogResult.OK)
            {
                EmployeeMentsList = new EmployeeMentSQL(DB).Get_EmployeeMent_List_IN_Part(_Part);
                RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);
            }
        }
        private void Open_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string s = listView1.SelectedItems[0].Name.Substring(0, 1);
                    if (s == "P")
                    {
                        uint sid = Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1));
                       _Part = Partsql.GetPartInfoByID(sid);
                        OpenPart();
                    }
                    else
                    {
                        uint sid = Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1));
                        EmployeeMent EmployeeMent_ = EmployeeMentsql.Get_EmployeeMent_InfoBYID(sid);
                        EmployeeMent_Form EmployeeMent_Form_ = new EmployeeMent_Form(DB, EmployeeMent_, false );
                        EmployeeMent_Form_.ShowDialog();
                        if (EmployeeMent_Form_.DialogResult == DialogResult.OK)
                        {
                            EmployeeMentsList = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(_Part);
                            RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);
                        }
                        EmployeeMent_Form_.Dispose();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Open_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Edit_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string s = listView1 .SelectedItems[0].Name.Substring(0, 1);
                    if (s == "P")
                    {
                        uint sid = Convert.ToUInt32(listView1 .SelectedItems[0].Name.Substring(1));
                        Part Part_ =Partsql .GetPartInfoByID(sid);
                        PartForm PartForm_ = new PartForm(DB, Part_, true);
                        PartForm_.ShowDialog();
                        if (PartForm_.DialogResult == DialogResult.OK)
                        {
                            PartsList = Partsql.GetPartChilds(_Part);
                            RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);
                        }
                        PartForm_.Dispose();
                    }
                    else
                    {
                        uint sid = Convert.ToUInt32(listView1 .SelectedItems[0].Name.Substring(1));
                        EmployeeMent EmployeeMent_ = EmployeeMentsql.Get_EmployeeMent_InfoBYID(sid);
                        EmployeeMent_Form EmployeeMent_Form_ = new EmployeeMent_Form(DB, EmployeeMent_, true);
                        EmployeeMent_Form_.ShowDialog();
                        if (EmployeeMent_Form_.DialogResult == DialogResult.OK)
                        {
                            EmployeeMentsList = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(_Part);
                            RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);
                        }
                        EmployeeMent_Form_.Dispose();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Edit_MenuItem_Click" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Delete_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string s = listView1.SelectedItems[0].Name.Substring(0, 1);
                if (s == "P")
                {
                    DialogResult dd = MessageBox.Show("هل انت متاكد من حذف القسم؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;

                    uint sid = Convert.ToUInt32(listView1.SelectedItems[0].Name.Substring(1));
                    bool success = Partsql .DeletePart (sid);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PartsList = Partsql.GetPartChilds(_Part);
                        RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);

                    }
                    else
                    {
                        MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    DialogResult dd = MessageBox.Show("هل انت متاكد من حذف الوظيفة ؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dd != DialogResult.OK) return;

                    uint sid = Convert.ToUInt32(listView1 .SelectedItems[0].Name.Substring(1));
                    bool success =  EmployeeMentsql.Delete_EmployeeMent (sid);
                    if (success)
                    {
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EmployeeMentsList = EmployeeMentsql.Get_EmployeeMent_List_IN_Part(_Part );
                        RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);

                    }
                    else
                    {
                        MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteJobStartOPR_MenuItem_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Paste_MenuItem_Click(object sender, EventArgs e)
        {
            if (_Part  == null && Moved_EmployeeMentList.Count > 0)
            {
                MessageBox.Show("يمكن نقل الوظائف لقسم فقط", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool success1 = Partsql.MoveParts(_Part  , Moved_PartList);
            bool success2 = EmployeeMentsql.MoveEmployeeMents  (_Part , Moved_EmployeeMentList);
            if (success1 || success2 )
            {
                FillTreeViewPart();
                OpenPart();
                Moved_PartList = new List<Part>();
                Moved_EmployeeMentList = new List<EmployeeMent>();
            }
           
            
        }
        private void Cut_MenuItem_Click(object sender, EventArgs e)
        {

            Moved_PartList.Clear();
            Moved_EmployeeMentList.Clear();
            Move_SourcePart = _Part ;
            for(int i=0;i<listView1 .SelectedItems .Count;i++)
            {
                if (listView1.SelectedItems[i].SubItems[2].Text == "مجلد")
                {
                    Moved_PartList.Add(Partsql.GetPartInfoByID(Convert .ToUInt32 (listView1.SelectedItems [i].Name )));
                }
                else
                {
                    Moved_EmployeeMentList.Add(EmployeeMentsql .Get_EmployeeMent_InfoBYID(Convert.ToUInt32(listView1.SelectedItems [i].Name)));
                }
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
            if (_Part   == null) return;
            _Part = Partsql.GetParentPart(_Part);
            OpenPart();
        }
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            RefreshEmployeeMents_Parts(PartsList ,EmployeeMentsList );
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearch.Text.Length > 0)
            {


                PartsList = Partsql.SearchPart(textBoxSearch.Text);
                EmployeeMentsList = EmployeeMentsql.SearchEmployeeMent(textBoxSearch.Text);
                RefreshEmployeeMents_Parts(PartsList, EmployeeMentsList);
            }
            else OpenPart();
        }


   

   

    
   

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData ==Keys.Back )
            {
                if (_Part  == null) return;
                _Part  = Partsql.GetParentPart(_Part );
                OpenPart();
                return;
            }
            if(listView1 .SelectedItems  .Count >0 )
            {
                
                switch (e.KeyData )
                {
                    case Keys.Enter:
                         OpenMenuItem.PerformClick();
                        break;
                    case Keys.Delete:

                        DeleteMenuItem.PerformClick();
                        break;
   


                }

            }
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            AdjustListviewColumnWidth();
        }
        public async void AdjustListviewColumnWidth()
        {
            listView1.Columns[0].Width = 300;
            listView1.Columns[1].Width = 150;
            listView1.Columns[2].Width = 150;
            listView1.Columns[3].Width = listView1 .Width -610;
        }
        private void treeViewParts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
            if (e.Button == MouseButtons.Left )
            {
                if (treeViewParts.SelectedNode != null)
                {
                    try
                    {
                        _Part  = Partsql.GetPartInfoByID(Convert.ToUInt32(treeViewParts.SelectedNode.Name));
                    }
                    catch
                    {
                        _Part = null;
                    }

                    OpenPart();
                }
          
            }
        }
    }
  
}
