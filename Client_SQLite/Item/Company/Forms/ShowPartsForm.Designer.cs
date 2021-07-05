namespace OverLoad_Client.Company.Forms
{
    partial class ShowPartsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPartsForm));
            this.PanelPart = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitPart1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.LocationName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LocationType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LocationDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFilterLocationEmployeeMent = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeViewParts = new System.Windows.Forms.TreeView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.PanelButtom = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.Button_Select = new System.Windows.Forms.Button();
            this.PanleHeader = new System.Windows.Forms.Panel();
            this.PanelPath = new System.Windows.Forms.Panel();
            this.Back = new System.Windows.Forms.Button();
            this.ImageListButton = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ادارةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelPart.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPart1)).BeginInit();
            this.splitPart1.Panel1.SuspendLayout();
            this.splitPart1.Panel2.SuspendLayout();
            this.splitPart1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PanelButtom.SuspendLayout();
            this.PanleHeader.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelPart
            // 
            this.PanelPart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelPart.Controls.Add(this.panel1);
            this.PanelPart.Controls.Add(this.PanelButtom);
            this.PanelPart.Controls.Add(this.PanleHeader);
            this.PanelPart.Location = new System.Drawing.Point(11, 30);
            this.PanelPart.Name = "PanelPart";
            this.PanelPart.Size = new System.Drawing.Size(934, 508);
            this.PanelPart.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.splitPart1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.treeViewParts);
            this.panel1.Location = new System.Drawing.Point(9, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 416);
            this.panel1.TabIndex = 9;
            // 
            // splitPart1
            // 
            this.splitPart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitPart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitPart1.Location = new System.Drawing.Point(3, 55);
            this.splitPart1.Name = "splitPart1";
            // 
            // splitPart1.Panel1
            // 
            this.splitPart1.Panel1.Controls.Add(this.listView1);
            this.splitPart1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitPart1.Panel2
            // 
            this.splitPart1.Panel2.Controls.Add(this.label3);
            this.splitPart1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitPart1.Panel2Collapsed = true;
            this.splitPart1.Size = new System.Drawing.Size(742, 356);
            this.splitPart1.SplitterDistance = 400;
            this.splitPart1.TabIndex = 11;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LocationName,
            this.LocationType,
            this.LocationDesc,
            this.columnHeader1});
            this.listView1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.RightToLeftLayout = true;
            this.listView1.Size = new System.Drawing.Size(734, 348);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // LocationName
            // 
            this.LocationName.Text = "الاسم";
            this.LocationName.Width = 211;
            // 
            // LocationType
            // 
            this.LocationType.Text = "النوع";
            this.LocationType.Width = 117;
            // 
            // LocationDesc
            // 
            this.LocationDesc.Text = "تاريخ الانشاء";
            this.LocationDesc.Width = 189;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 175;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "تنزيل.png");
            this.imageList1.Images.SetKeyName(1, "images (2).png");
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
            this.panel3.Size = new System.Drawing.Size(742, 46);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.comboBoxFilterLocationEmployeeMent);
            this.panel4.Location = new System.Drawing.Point(282, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(456, 38);
            this.panel4.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(387, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 29);
            this.button1.TabIndex = 17;
            this.button1.Text = "رجوع";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Back_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(289, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "تصفية";
            // 
            // comboBoxFilterLocationEmployeeMent
            // 
            this.comboBoxFilterLocationEmployeeMent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterLocationEmployeeMent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterLocationEmployeeMent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFilterLocationEmployeeMent.FormattingEnabled = true;
            this.comboBoxFilterLocationEmployeeMent.Items.AddRange(new object[] {
            "اظهار الكل",
            "أقسام",
            "وظائف"});
            this.comboBoxFilterLocationEmployeeMent.Location = new System.Drawing.Point(177, 4);
            this.comboBoxFilterLocationEmployeeMent.Name = "comboBoxFilterLocationEmployeeMent";
            this.comboBoxFilterLocationEmployeeMent.Size = new System.Drawing.Size(106, 24);
            this.comboBoxFilterLocationEmployeeMent.TabIndex = 14;
            this.comboBoxFilterLocationEmployeeMent.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterItemFolder_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBoxSearch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 38);
            this.panel2.TabIndex = 11;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(24, 7);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(165, 23);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(203, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "بحث";
            // 
            // treeViewParts
            // 
            this.treeViewParts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewParts.ImageIndex = 0;
            this.treeViewParts.ImageList = this.imageList2;
            this.treeViewParts.Location = new System.Drawing.Point(751, 3);
            this.treeViewParts.Name = "treeViewParts";
            this.treeViewParts.RightToLeftLayout = true;
            this.treeViewParts.SelectedImageIndex = 0;
            this.treeViewParts.Size = new System.Drawing.Size(160, 408);
            this.treeViewParts.TabIndex = 9;
            this.treeViewParts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewFolders_MouseDoubleClick);
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
            this.PanelButtom.Controls.Add(this.buttonCancel);
            this.PanelButtom.Controls.Add(this.Button_Select);
            this.PanelButtom.Location = new System.Drawing.Point(8, 465);
            this.PanelButtom.Name = "PanelButtom";
            this.PanelButtom.Size = new System.Drawing.Size(917, 43);
            this.PanelButtom.TabIndex = 8;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(20, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(91, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "إلغاء";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // Button_Select
            // 
            this.Button_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Select.Location = new System.Drawing.Point(140, 7);
            this.Button_Select.Name = "Button_Select";
            this.Button_Select.Size = new System.Drawing.Size(98, 27);
            this.Button_Select.TabIndex = 4;
            this.Button_Select.Text = "اختيار";
            this.Button_Select.UseVisualStyleBackColor = true;
            this.Button_Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // PanleHeader
            // 
            this.PanleHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanleHeader.Controls.Add(this.PanelPath);
            this.PanleHeader.Controls.Add(this.Back);
            this.PanleHeader.Location = new System.Drawing.Point(9, 3);
            this.PanleHeader.Name = "PanleHeader";
            this.PanleHeader.Size = new System.Drawing.Size(917, 35);
            this.PanleHeader.TabIndex = 7;
            // 
            // PanelPath
            // 
            this.PanelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelPath.Location = new System.Drawing.Point(165, 3);
            this.PanelPath.Name = "PanelPath";
            this.PanelPath.Size = new System.Drawing.Size(742, 32);
            this.PanelPath.TabIndex = 3;
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
            this.ادارةToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(950, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ادارةToolStripMenuItem
            // 
            this.ادارةToolStripMenuItem.Name = "ادارةToolStripMenuItem";
            this.ادارةToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.ادارةToolStripMenuItem.Text = "ادارة";
            // 
            // ShowPartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aquamarine;
            this.ClientSize = new System.Drawing.Size(950, 542);
            this.Controls.Add(this.PanelPart);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(752, 527);
            this.Name = "ShowPartsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ادارة أقسام الشركة";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PanelPart.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitPart1.Panel1.ResumeLayout(false);
            this.splitPart1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPart1)).EndInit();
            this.splitPart1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PanelButtom.ResumeLayout(false);
            this.PanleHeader.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PanelPart;
        private System.Windows.Forms.Panel PanelButtom;
        private System.Windows.Forms.Panel PanleHeader;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Panel PanelPath;
        private System.Windows.Forms.ImageList ImageListButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ادارةToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer  splitPart1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFilterLocationEmployeeMent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeViewParts;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader LocationName;
        private System.Windows.Forms.ColumnHeader LocationType;
        private System.Windows.Forms.ColumnHeader LocationDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button Button_Select;
    }
}

