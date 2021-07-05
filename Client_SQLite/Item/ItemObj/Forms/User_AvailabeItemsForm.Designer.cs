namespace OverLoad_Client.ItemObj.Forms
{
    partial class User_AvailabeItemsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_AvailabeItemsForm));
            this.PanelContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ContentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContentType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBoxAvailableOnly = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxCompanies = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFilterItemFolder = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxSearchType = new System.Windows.Forms.CheckBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeViewFolders = new System.Windows.Forms.TreeView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.PanelButtom = new System.Windows.Forms.Panel();
            this.Button_Close = new System.Windows.Forms.Button();
            this.Button_Select = new System.Windows.Forms.Button();
            this.PanleHeader = new System.Windows.Forms.Panel();
            this.PanelPath = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Back = new System.Windows.Forms.Button();
            this.ImageListButton = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.عرضToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.مجموعاتالمكافئاتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxFolderAccess = new System.Windows.Forms.ComboBox();
            this.PanelContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PanelButtom.SuspendLayout();
            this.PanleHeader.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelContainer
            // 
            this.PanelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelContainer.BackColor = System.Drawing.Color.PaleGreen;
            this.PanelContainer.Controls.Add(this.panel1);
            this.PanelContainer.Controls.Add(this.PanelButtom);
            this.PanelContainer.Controls.Add(this.PanleHeader);
            this.PanelContainer.Location = new System.Drawing.Point(0, 80);
            this.PanelContainer.Name = "PanelContainer";
            this.PanelContainer.Size = new System.Drawing.Size(1357, 458);
            this.PanelContainer.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.treeViewFolders);
            this.panel1.Location = new System.Drawing.Point(9, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1339, 366);
            this.panel1.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(3, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1165, 306);
            this.splitContainer1.SplitterDistance = 518;
            this.splitContainer1.TabIndex = 11;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ContentName,
            this.columnHeader1,
            this.ContentType,
            this.columnHeader2});
            this.listView1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.RightToLeftLayout = true;
            this.listView1.Size = new System.Drawing.Size(1157, 298);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // ContentName
            // 
            this.ContentName.Text = "الاسم";
            this.ContentName.Width = 135;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "الشركة";
            this.columnHeader1.Width = 117;
            // 
            // ContentType
            // 
            this.ContentType.Text = "النوع";
            this.ContentType.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 340;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "defult-text.png");
            this.imageList1.Images.SetKeyName(1, "if_folder_416376.png");
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "فلترة حسب الخصائص";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1165, 46);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Aquamarine;
            this.panel4.Controls.Add(this.checkBoxAvailableOnly);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.comboBoxCompanies);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.comboBoxFilterItemFolder);
            this.panel4.Location = new System.Drawing.Point(394, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(767, 38);
            this.panel4.TabIndex = 12;
            // 
            // checkBoxAvailableOnly
            // 
            this.checkBoxAvailableOnly.AutoSize = true;
            this.checkBoxAvailableOnly.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxAvailableOnly.Location = new System.Drawing.Point(25, 7);
            this.checkBoxAvailableOnly.Name = "checkBoxAvailableOnly";
            this.checkBoxAvailableOnly.Size = new System.Drawing.Size(214, 22);
            this.checkBoxAvailableOnly.TabIndex = 28;
            this.checkBoxAvailableOnly.Text = "اظهار العناصر المتوفرة فقط";
            this.checkBoxAvailableOnly.UseVisualStyleBackColor = true;
            this.checkBoxAvailableOnly.CheckedChanged += new System.EventHandler(this.checkBoxAvailableOnly_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(420, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "حسب الشركة";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(698, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 29);
            this.button1.TabIndex = 17;
            this.button1.Text = "رجوع";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Back_Click);
            // 
            // comboBoxCompanies
            // 
            this.comboBoxCompanies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompanies.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCompanies.FormattingEnabled = true;
            this.comboBoxCompanies.Location = new System.Drawing.Point(291, 8);
            this.comboBoxCompanies.Name = "comboBoxCompanies";
            this.comboBoxCompanies.Size = new System.Drawing.Size(123, 24);
            this.comboBoxCompanies.TabIndex = 16;
            this.comboBoxCompanies.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompanies_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(632, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "تصفية";
            // 
            // comboBoxFilterItemFolder
            // 
            this.comboBoxFilterItemFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterItemFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterItemFolder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFilterItemFolder.FormattingEnabled = true;
            this.comboBoxFilterItemFolder.Items.AddRange(new object[] {
            "اظهار الكل",
            "مجلدات فقط",
            "عناصر فقط"});
            this.comboBoxFilterItemFolder.Location = new System.Drawing.Point(520, 5);
            this.comboBoxFilterItemFolder.Name = "comboBoxFilterItemFolder";
            this.comboBoxFilterItemFolder.Size = new System.Drawing.Size(106, 24);
            this.comboBoxFilterItemFolder.TabIndex = 14;
            this.comboBoxFilterItemFolder.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterItemFolder_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBoxSearchType);
            this.panel2.Controls.Add(this.textBoxSearch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(374, 38);
            this.panel2.TabIndex = 11;
            // 
            // checkBoxSearchType
            // 
            this.checkBoxSearchType.AutoSize = true;
            this.checkBoxSearchType.Location = new System.Drawing.Point(6, 8);
            this.checkBoxSearchType.Name = "checkBoxSearchType";
            this.checkBoxSearchType.Size = new System.Drawing.Size(185, 20);
            this.checkBoxSearchType.TabIndex = 3;
            this.checkBoxSearchType.Text = "فقط العناصر ضمن هذه الصنف";
            this.checkBoxSearchType.UseVisualStyleBackColor = true;
            this.checkBoxSearchType.CheckedChanged += new System.EventHandler(this.checkBoxSearchType_CheckedChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(197, 7);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(133, 23);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(336, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "بحث";
            // 
            // treeViewFolders
            // 
            this.treeViewFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewFolders.ImageIndex = 0;
            this.treeViewFolders.ImageList = this.imageList2;
            this.treeViewFolders.Location = new System.Drawing.Point(1174, 3);
            this.treeViewFolders.Name = "treeViewFolders";
            this.treeViewFolders.RightToLeftLayout = true;
            this.treeViewFolders.SelectedImageIndex = 0;
            this.treeViewFolders.Size = new System.Drawing.Size(160, 358);
            this.treeViewFolders.TabIndex = 9;
            this.treeViewFolders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewFolders_MouseDoubleClick);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Folder.png");
            // 
            // PanelButtom
            // 
            this.PanelButtom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelButtom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelButtom.Controls.Add(this.Button_Close);
            this.PanelButtom.Controls.Add(this.Button_Select);
            this.PanelButtom.Location = new System.Drawing.Point(8, 415);
            this.PanelButtom.Name = "PanelButtom";
            this.PanelButtom.Size = new System.Drawing.Size(1340, 40);
            this.PanelButtom.TabIndex = 8;
            // 
            // Button_Close
            // 
            this.Button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Close.Location = new System.Drawing.Point(15, 7);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(91, 27);
            this.Button_Close.TabIndex = 5;
            this.Button_Close.Text = "إلغاء";
            this.Button_Close.UseVisualStyleBackColor = true;
            // 
            // Button_Select
            // 
            this.Button_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Select.Location = new System.Drawing.Point(135, 7);
            this.Button_Select.Name = "Button_Select";
            this.Button_Select.Size = new System.Drawing.Size(98, 27);
            this.Button_Select.TabIndex = 4;
            this.Button_Select.Text = "اختيار";
            this.Button_Select.UseVisualStyleBackColor = true;
            this.Button_Select.Click += new System.EventHandler(this.Button_Select_Click);
            // 
            // PanleHeader
            // 
            this.PanleHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanleHeader.Controls.Add(this.PanelPath);
            this.PanleHeader.Controls.Add(this.comboBox1);
            this.PanleHeader.Controls.Add(this.Back);
            this.PanleHeader.Location = new System.Drawing.Point(9, 3);
            this.PanleHeader.Name = "PanleHeader";
            this.PanleHeader.Size = new System.Drawing.Size(1340, 35);
            this.PanleHeader.TabIndex = 7;
            // 
            // PanelPath
            // 
            this.PanelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelPath.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.PanelPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelPath.Location = new System.Drawing.Point(165, 3);
            this.PanelPath.Name = "PanelPath";
            this.PanelPath.Size = new System.Drawing.Size(1171, 32);
            this.PanelPath.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "تفاصيل",
            "رموز",
            "قائمة",
            ""});
            this.comboBox1.Location = new System.Drawing.Point(16, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(64, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Back
            // 
            this.Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back.Location = new System.Drawing.Point(102, 3);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(57, 29);
            this.Back.TabIndex = 0;
            this.Back.Text = "رجوع";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // ImageListButton
            // 
            this.ImageListButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListButton.ImageStream")));
            this.ImageListButton.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListButton.Images.SetKeyName(0, "icons8-play-50.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.عرضToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1362, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // عرضToolStripMenuItem
            // 
            this.عرضToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.مجموعاتالمكافئاتToolStripMenuItem});
            this.عرضToolStripMenuItem.Name = "عرضToolStripMenuItem";
            this.عرضToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.عرضToolStripMenuItem.Text = "عرض";
            // 
            // مجموعاتالمكافئاتToolStripMenuItem
            // 
            this.مجموعاتالمكافئاتToolStripMenuItem.Name = "مجموعاتالمكافئاتToolStripMenuItem";
            this.مجموعاتالمكافئاتToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.مجموعاتالمكافئاتToolStripMenuItem.Text = "مجموعات المكافئات";
            this.مجموعاتالمكافئاتToolStripMenuItem.Click += new System.EventHandler(this.مجموعاتالمكافئاتToolStripMenuItem_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.comboBoxFolderAccess);
            this.panel5.Location = new System.Drawing.Point(9, 29);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1171, 45);
            this.panel5.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(959, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "الأصناف السموح الوصول اليها";
            // 
            // comboBoxFolderAccess
            // 
            this.comboBoxFolderAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFolderAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFolderAccess.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFolderAccess.FormattingEnabled = true;
            this.comboBoxFolderAccess.Items.AddRange(new object[] {
            "اظهار الكل",
            "مجلدات فقط",
            "عناصر فقط"});
            this.comboBoxFolderAccess.Location = new System.Drawing.Point(684, 10);
            this.comboBoxFolderAccess.Name = "comboBoxFolderAccess";
            this.comboBoxFolderAccess.Size = new System.Drawing.Size(269, 27);
            this.comboBoxFolderAccess.TabIndex = 16;
            this.comboBoxFolderAccess.SelectedIndexChanged += new System.EventHandler(this.comboBoxFolderAccess_SelectedIndexChanged);
            // 
            // User_AvailabeItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(1362, 542);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.PanelContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(752, 527);
            this.Name = "User_AvailabeItemsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "العناصر";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShowItemsForm_Load);
            this.PanelContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PanelButtom.ResumeLayout(false);
            this.PanleHeader.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PanelContainer;
        private System.Windows.Forms.Panel PanelButtom;
        private System.Windows.Forms.Panel PanleHeader;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Panel PanelPath;
        private System.Windows.Forms.ImageList ImageListButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxCompanies;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFilterItemFolder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeViewFolders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxSearchType;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem عرضToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem مجموعاتالمكافئاتToolStripMenuItem;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxFolderAccess;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Button Button_Select;
        private System.Windows.Forms.CheckBox checkBoxAvailableOnly;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ContentName;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader ContentType;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

