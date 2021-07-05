namespace OverLoad_Client.ItemObj.Forms
{
    partial class Form_ItemSpecRestrict_SetValues
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
            this.ItemSpecName = new System.Windows.Forms.Label();
            this.buttonSetValues = new System.Windows.Forms.Button();
            this.listView_ItemValues = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_AvailableOptions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cancel = new System.Windows.Forms.Button();
            this.ItemInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ItemSpecName);
            this.panel1.Controls.Add(this.buttonSetValues);
            this.panel1.Controls.Add(this.listView_ItemValues);
            this.panel1.Controls.Add(this.listView_AvailableOptions);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 275);
            this.panel1.TabIndex = 0;
            // 
            // ItemSpecName
            // 
            this.ItemSpecName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ItemSpecName.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemSpecName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.ItemSpecName.Location = new System.Drawing.Point(0, 0);
            this.ItemSpecName.Name = "ItemSpecName";
            this.ItemSpecName.Size = new System.Drawing.Size(579, 39);
            this.ItemSpecName.TabIndex = 11;
            this.ItemSpecName.Text = "اسم الخاصية";
            // 
            // buttonSetValues
            // 
            this.buttonSetValues.Location = new System.Drawing.Point(273, 150);
            this.buttonSetValues.Name = "buttonSetValues";
            this.buttonSetValues.Size = new System.Drawing.Size(37, 40);
            this.buttonSetValues.TabIndex = 8;
            this.buttonSetValues.Text = ">>";
            this.buttonSetValues.UseVisualStyleBackColor = true;
            this.buttonSetValues.Click += new System.EventHandler(this.buttonSetValues_Click);
            // 
            // listView_ItemValues
            // 
            this.listView_ItemValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView_ItemValues.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_ItemValues.FullRowSelect = true;
            this.listView_ItemValues.Location = new System.Drawing.Point(15, 42);
            this.listView_ItemValues.Name = "listView_ItemValues";
            this.listView_ItemValues.RightToLeftLayout = true;
            this.listView_ItemValues.Size = new System.Drawing.Size(252, 228);
            this.listView_ItemValues.TabIndex = 7;
            this.listView_ItemValues.UseCompatibleStateImageBehavior = false;
            this.listView_ItemValues.View = System.Windows.Forms.View.Details;
            this.listView_ItemValues.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_ItemValues_KeyDown);
            this.listView_ItemValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_ItemValues_MouseDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "القيم المدخلة";
            this.columnHeader2.Width = 233;
            // 
            // listView_AvailableOptions
            // 
            this.listView_AvailableOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_AvailableOptions.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_AvailableOptions.FullRowSelect = true;
            this.listView_AvailableOptions.Location = new System.Drawing.Point(316, 42);
            this.listView_AvailableOptions.Name = "listView_AvailableOptions";
            this.listView_AvailableOptions.RightToLeftLayout = true;
            this.listView_AvailableOptions.Size = new System.Drawing.Size(252, 228);
            this.listView_AvailableOptions.TabIndex = 6;
            this.listView_AvailableOptions.UseCompatibleStateImageBehavior = false;
            this.listView_AvailableOptions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "الخيارات المتاحة";
            this.columnHeader1.Width = 240;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.Location = new System.Drawing.Point(263, 329);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(74, 35);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "اغلاق";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // ItemInfo
            // 
            this.ItemInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ItemInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemInfo.Enabled = false;
            this.ItemInfo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemInfo.Location = new System.Drawing.Point(0, 0);
            this.ItemInfo.Name = "ItemInfo";
            this.ItemInfo.Size = new System.Drawing.Size(605, 36);
            this.ItemInfo.TabIndex = 6;
            this.ItemInfo.Text = "label1";
            // 
            // Form_ItemSpecRestrict_SetValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 367);
            this.Controls.Add(this.ItemInfo);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.panel1);
            this.Name = "Form_ItemSpecRestrict_SetValues";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "خصائص العنصر";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button buttonSetValues;
        private System.Windows.Forms.ListView listView_ItemValues;
        private System.Windows.Forms.ListView listView_AvailableOptions;
        private System.Windows.Forms.Label ItemSpecName;
        private System.Windows.Forms.Label ItemInfo;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}