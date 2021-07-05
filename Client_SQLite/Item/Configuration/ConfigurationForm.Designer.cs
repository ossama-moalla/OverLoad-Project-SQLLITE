namespace OverLoad_Client.Configuration
{
    partial class ConfigurationForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonEmployeementLevel = new System.Windows.Forms.Button();
            this.buttonMoneyBox = new System.Windows.Forms.Button();
            this.buttonTradeStateSetting = new System.Windows.Forms.Button();
            this.buttonSellTypeSetting = new System.Windows.Forms.Button();
            this.buttonCurrencySetting = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelHeader = new System.Windows.Forms.Label();
            this.listViewData = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonEmployeementLevel);
            this.panel1.Controls.Add(this.buttonMoneyBox);
            this.panel1.Controls.Add(this.buttonTradeStateSetting);
            this.panel1.Controls.Add(this.buttonSellTypeSetting);
            this.panel1.Controls.Add(this.buttonCurrencySetting);
            this.panel1.Location = new System.Drawing.Point(5, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 351);
            this.panel1.TabIndex = 0;
            // 
            // buttonEmployeementLevel
            // 
            this.buttonEmployeementLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEmployeementLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEmployeementLevel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonEmployeementLevel.Location = new System.Drawing.Point(3, 167);
            this.buttonEmployeementLevel.Name = "buttonEmployeementLevel";
            this.buttonEmployeementLevel.Size = new System.Drawing.Size(175, 35);
            this.buttonEmployeementLevel.TabIndex = 4;
            this.buttonEmployeementLevel.Text = "ضبط المستويات الوظيفة";
            this.buttonEmployeementLevel.UseVisualStyleBackColor = true;
            this.buttonEmployeementLevel.Click += new System.EventHandler(this.buttonEmployeementLevel_Click);
            // 
            // buttonMoneyBox
            // 
            this.buttonMoneyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoneyBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoneyBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonMoneyBox.Location = new System.Drawing.Point(3, 126);
            this.buttonMoneyBox.Name = "buttonMoneyBox";
            this.buttonMoneyBox.Size = new System.Drawing.Size(175, 35);
            this.buttonMoneyBox.TabIndex = 3;
            this.buttonMoneyBox.Text = "ضبط صناديق المال";
            this.buttonMoneyBox.UseVisualStyleBackColor = true;
            this.buttonMoneyBox.Click += new System.EventHandler(this.buttonMoneyBox_Click);
            // 
            // buttonTradeStateSetting
            // 
            this.buttonTradeStateSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTradeStateSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTradeStateSetting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonTradeStateSetting.Location = new System.Drawing.Point(3, 85);
            this.buttonTradeStateSetting.Name = "buttonTradeStateSetting";
            this.buttonTradeStateSetting.Size = new System.Drawing.Size(175, 35);
            this.buttonTradeStateSetting.TabIndex = 2;
            this.buttonTradeStateSetting.Text = "ضبط حالات بيع و شراء العنصر";
            this.buttonTradeStateSetting.UseVisualStyleBackColor = true;
            this.buttonTradeStateSetting.Click += new System.EventHandler(this.buttonTradeStateSetting_Click);
            // 
            // buttonSellTypeSetting
            // 
            this.buttonSellTypeSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSellTypeSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSellTypeSetting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonSellTypeSetting.Location = new System.Drawing.Point(3, 44);
            this.buttonSellTypeSetting.Name = "buttonSellTypeSetting";
            this.buttonSellTypeSetting.Size = new System.Drawing.Size(175, 35);
            this.buttonSellTypeSetting.TabIndex = 1;
            this.buttonSellTypeSetting.Text = "ضبط أنماط البيع";
            this.buttonSellTypeSetting.UseVisualStyleBackColor = true;
            this.buttonSellTypeSetting.Click += new System.EventHandler(this.buttonSellTypeSetting_Click);
            // 
            // buttonCurrencySetting
            // 
            this.buttonCurrencySetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCurrencySetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCurrencySetting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonCurrencySetting.Location = new System.Drawing.Point(3, 3);
            this.buttonCurrencySetting.Name = "buttonCurrencySetting";
            this.buttonCurrencySetting.Size = new System.Drawing.Size(175, 35);
            this.buttonCurrencySetting.TabIndex = 0;
            this.buttonCurrencySetting.Text = "ضبط العملات";
            this.buttonCurrencySetting.UseVisualStyleBackColor = true;
            this.buttonCurrencySetting.Click += new System.EventHandler(this.buttonCurrencySetting_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelHeader);
            this.panel2.Controls.Add(this.listViewData);
            this.panel2.Location = new System.Drawing.Point(194, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(587, 350);
            this.panel2.TabIndex = 1;
            // 
            // labelHeader
            // 
            this.labelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHeader.BackColor = System.Drawing.Color.Aquamarine;
            this.labelHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelHeader.Location = new System.Drawing.Point(3, 3);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(578, 41);
            this.labelHeader.TabIndex = 2;
            // 
            // listViewData
            // 
            this.listViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewData.FullRowSelect = true;
            this.listViewData.GridLines = true;
            this.listViewData.Location = new System.Drawing.Point(0, 47);
            this.listViewData.Name = "listViewData";
            this.listViewData.RightToLeftLayout = true;
            this.listViewData.Size = new System.Drawing.Size(583, 301);
            this.listViewData.TabIndex = 1;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.Details;
            this.listViewData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            this.listViewData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 115;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.panel3.Location = new System.Drawing.Point(5, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(776, 39);
            this.panel3.TabIndex = 2;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 434);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigurationForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "الضبط";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonTradeStateSetting;
        private System.Windows.Forms.Button buttonSellTypeSetting;
        private System.Windows.Forms.Button buttonCurrencySetting;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView listViewData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Button buttonMoneyBox;
        private System.Windows.Forms.Button buttonEmployeementLevel;
    }
}