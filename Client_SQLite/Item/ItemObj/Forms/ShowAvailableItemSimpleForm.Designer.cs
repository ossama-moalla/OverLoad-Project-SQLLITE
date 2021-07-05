namespace OverLoad_Client.ItemObj.Forms
{
    partial class ShowAvailableItemSimpleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowAvailableItemSimpleForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxAvailableOnly = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxItemilter = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCompanyFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFolderFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PanelButtom = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.Select = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PanelButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Aquamarine;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Location = new System.Drawing.Point(8, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 432);
            this.panel1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(6, 3);
            this.listView1.Name = "listView1";
            this.listView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listView1.RightToLeftLayout = true;
            this.listView1.Size = new System.Drawing.Size(998, 424);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "الصنف";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "اسم العنصر";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "الشركة";
            this.columnHeader3.Width = 150;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Graphicloads-100-Flat-Cart-add.ico");
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBoxAvailableOnly);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.comboBoxItemilter);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboBoxCompanyFilter);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.comboBoxFolderFilter);
            this.panel2.Location = new System.Drawing.Point(8, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1005, 71);
            this.panel2.TabIndex = 1;
            // 
            // checkBoxAvailableOnly
            // 
            this.checkBoxAvailableOnly.AutoSize = true;
            this.checkBoxAvailableOnly.Checked = true;
            this.checkBoxAvailableOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAvailableOnly.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxAvailableOnly.Location = new System.Drawing.Point(17, 33);
            this.checkBoxAvailableOnly.Name = "checkBoxAvailableOnly";
            this.checkBoxAvailableOnly.Size = new System.Drawing.Size(214, 22);
            this.checkBoxAvailableOnly.TabIndex = 25;
            this.checkBoxAvailableOnly.Text = "اظهار العناصر المتوفرة فقط";
            this.checkBoxAvailableOnly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(282, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "تصفية حسب اسم العنصر\r\n";
            // 
            // comboBoxItemilter
            // 
            this.comboBoxItemilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxItemilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxItemilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxItemilter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxItemilter.FormattingEnabled = true;
            this.comboBoxItemilter.Location = new System.Drawing.Point(266, 33);
            this.comboBoxItemilter.Name = "comboBoxItemilter";
            this.comboBoxItemilter.Size = new System.Drawing.Size(199, 26);
            this.comboBoxItemilter.TabIndex = 23;
            this.comboBoxItemilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxItemilter_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(570, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "تصفية حسب الشركة";
            // 
            // comboBoxCompanyFilter
            // 
            this.comboBoxCompanyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCompanyFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCompanyFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCompanyFilter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxCompanyFilter.FormattingEnabled = true;
            this.comboBoxCompanyFilter.Location = new System.Drawing.Point(524, 33);
            this.comboBoxCompanyFilter.Name = "comboBoxCompanyFilter";
            this.comboBoxCompanyFilter.Size = new System.Drawing.Size(199, 26);
            this.comboBoxCompanyFilter.TabIndex = 21;
            this.comboBoxCompanyFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxCompanyFilter_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(820, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "تصفية حسب الصنف";
            // 
            // comboBoxFolderFilter
            // 
            this.comboBoxFolderFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFolderFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxFolderFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxFolderFilter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxFolderFilter.FormattingEnabled = true;
            this.comboBoxFolderFilter.Items.AddRange(new object[] {
            "اظهار الكل",
            "مجلدات فقط",
            "عناصر فقط"});
            this.comboBoxFolderFilter.Location = new System.Drawing.Point(777, 33);
            this.comboBoxFolderFilter.Name = "comboBoxFolderFilter";
            this.comboBoxFolderFilter.Size = new System.Drawing.Size(190, 26);
            this.comboBoxFolderFilter.TabIndex = 19;
            this.comboBoxFolderFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxFolderFilter_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1005, 46);
            this.label3.TabIndex = 21;
            this.label3.Text = "جميع العناصر المتوفرة";
            // 
            // PanelButtom
            // 
            this.PanelButtom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelButtom.BackColor = System.Drawing.Color.Aquamarine;
            this.PanelButtom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelButtom.Controls.Add(this.button2);
            this.PanelButtom.Controls.Add(this.Select);
            this.PanelButtom.Location = new System.Drawing.Point(10, 573);
            this.PanelButtom.Name = "PanelButtom";
            this.PanelButtom.Size = new System.Drawing.Size(1005, 42);
            this.PanelButtom.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(6, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 33);
            this.button2.TabIndex = 5;
            this.button2.Text = "إلغاء";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Select
            // 
            this.Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Select.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Select.Location = new System.Drawing.Point(126, 4);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(98, 33);
            this.Select.TabIndex = 4;
            this.Select.Text = "اختيار";
            this.Select.UseVisualStyleBackColor = true;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // ShowAvailableItemSimpleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 623);
            this.Controls.Add(this.PanelButtom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ShowAvailableItemSimpleForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "العناصر المتوفرة";
            this.Load += new System.EventHandler(this.ShowAvailableItemSimpleForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.PanelButtom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCompanyFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFolderFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxItemilter;
        private System.Windows.Forms.CheckBox checkBoxAvailableOnly;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PanelButtom;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Select;
    }
}