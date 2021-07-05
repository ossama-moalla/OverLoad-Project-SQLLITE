namespace OverLoad_Client.Maintenance.Forms
{
    partial class MaintenanceOPR_NotFinish_Form
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
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listViewMaintenanceOPRs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelDateNow = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1235, 55);
            this.label3.TabIndex = 60;
            this.label3.Text = "عمليات الصيانة الغير منتيهة";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.LightBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.buttonCancel);
            this.panel5.Location = new System.Drawing.Point(2, 391);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1235, 53);
            this.panel5.TabIndex = 61;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(559, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(114, 37);
            this.buttonCancel.TabIndex = 58;
            this.buttonCancel.Text = "اغلاق";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listViewMaintenanceOPRs
            // 
            this.listViewMaintenanceOPRs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMaintenanceOPRs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader8});
            this.listViewMaintenanceOPRs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewMaintenanceOPRs.FullRowSelect = true;
            this.listViewMaintenanceOPRs.GridLines = true;
            this.listViewMaintenanceOPRs.Location = new System.Drawing.Point(0, 67);
            this.listViewMaintenanceOPRs.Name = "listViewMaintenanceOPRs";
            this.listViewMaintenanceOPRs.RightToLeftLayout = true;
            this.listViewMaintenanceOPRs.Size = new System.Drawing.Size(1237, 318);
            this.listViewMaintenanceOPRs.TabIndex = 62;
            this.listViewMaintenanceOPRs.UseCompatibleStateImageBehavior = false;
            this.listViewMaintenanceOPRs.View = System.Windows.Forms.View.Details;
            this.listViewMaintenanceOPRs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewMaintenanceOPRs_KeyDown);
            this.listViewMaintenanceOPRs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewMaintenanceOPRs_MouseDoubleClick);
            this.listViewMaintenanceOPRs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewMaintenanceOPRs_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "رقم الفاتورة";
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "التاريخ";
            this.columnHeader2.Width = 113;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "اسم الزبون";
            this.columnHeader3.Width = 212;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "صنف العنصر";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "الموديل";
            this.columnHeader5.Width = 149;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "الشركة";
            this.columnHeader6.Width = 153;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "العطل";
            this.columnHeader8.Width = 288;
            // 
            // labelDateNow
            // 
            this.labelDateNow.BackColor = System.Drawing.Color.LightGreen;
            this.labelDateNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDateNow.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateNow.Location = new System.Drawing.Point(942, 25);
            this.labelDateNow.Name = "labelDateNow";
            this.labelDateNow.Size = new System.Drawing.Size(295, 29);
            this.labelDateNow.TabIndex = 63;
            this.labelDateNow.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGreen;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(835, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 29);
            this.label2.TabIndex = 64;
            this.label2.Text = "تاريخ اليوم:";
            // 
            // MaintenanceOPR_NotFinish_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 445);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelDateNow);
            this.Controls.Add(this.listViewMaintenanceOPRs);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label3);
            this.Name = "MaintenanceOPR_NotFinish_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listViewMaintenanceOPRs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label labelDateNow;
        private System.Windows.Forms.Label label2;
    }
}