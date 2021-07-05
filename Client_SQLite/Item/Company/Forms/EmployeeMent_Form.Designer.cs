namespace OverLoad_Client.Company.Forms
{
    partial class EmployeeMent_Form
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxPartName = new System.Windows.Forms.TextBox();
            this.dateTimePickerPartCreateDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxPartID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelSubDiagnosticOPR = new System.Windows.Forms.Panel();
            this.listViewAssignReport = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dateTimePickerEmployeeCreateDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelItemInfo = new System.Windows.Forms.Label();
            this.labelEmployeementid = new System.Windows.Forms.Label();
            this.textBoxEmployeementName = new System.Windows.Forms.TextBox();
            this.textBoxEmployeeID = new System.Windows.Forms.TextBox();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelSubDiagnosticOPR.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.buttonCancel);
            this.panel4.Controls.Add(this.buttonSave);
            this.panel4.Location = new System.Drawing.Point(4, 405);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(839, 53);
            this.panel4.TabIndex = 8;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(267, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(114, 37);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "اغلاق";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(456, 7);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 37);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "حفظ";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.textBoxPartName);
            this.panel3.Controls.Add(this.dateTimePickerPartCreateDate);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.textBoxPartID);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(5, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(838, 92);
            this.panel3.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(629, 39);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 16);
            this.label20.TabIndex = 66;
            this.label20.Text = "اسم القسم";
            // 
            // textBoxPartName
            // 
            this.textBoxPartName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartName.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxPartName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPartName.Location = new System.Drawing.Point(479, 58);
            this.textBoxPartName.Name = "textBoxPartName";
            this.textBoxPartName.ReadOnly = true;
            this.textBoxPartName.Size = new System.Drawing.Size(223, 26);
            this.textBoxPartName.TabIndex = 65;
            // 
            // dateTimePickerPartCreateDate
            // 
            this.dateTimePickerPartCreateDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerPartCreateDate.Enabled = false;
            this.dateTimePickerPartCreateDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerPartCreateDate.Location = new System.Drawing.Point(253, 58);
            this.dateTimePickerPartCreateDate.Name = "dateTimePickerPartCreateDate";
            this.dateTimePickerPartCreateDate.Size = new System.Drawing.Size(204, 26);
            this.dateTimePickerPartCreateDate.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(378, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "تاريخ الانشاء";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(755, 39);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(75, 16);
            this.label23.TabIndex = 62;
            this.label23.Text = "رقم القسم";
            // 
            // textBoxPartID
            // 
            this.textBoxPartID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartID.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxPartID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPartID.Location = new System.Drawing.Point(728, 58);
            this.textBoxPartID.Name = "textBoxPartID";
            this.textBoxPartID.ReadOnly = true;
            this.textBoxPartID.Size = new System.Drawing.Size(99, 26);
            this.textBoxPartID.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(839, 31);
            this.label1.TabIndex = 60;
            this.label1.Text = "معلومات القسم";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panelSubDiagnosticOPR);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(5, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(839, 286);
            this.panel1.TabIndex = 6;
            // 
            // panelSubDiagnosticOPR
            // 
            this.panelSubDiagnosticOPR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSubDiagnosticOPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSubDiagnosticOPR.Controls.Add(this.listViewAssignReport);
            this.panelSubDiagnosticOPR.Controls.Add(this.label16);
            this.panelSubDiagnosticOPR.Location = new System.Drawing.Point(0, 103);
            this.panelSubDiagnosticOPR.Name = "panelSubDiagnosticOPR";
            this.panelSubDiagnosticOPR.Size = new System.Drawing.Size(839, 178);
            this.panelSubDiagnosticOPR.TabIndex = 51;
            // 
            // listViewAssignReport
            // 
            this.listViewAssignReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAssignReport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewAssignReport.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewAssignReport.FullRowSelect = true;
            this.listViewAssignReport.GridLines = true;
            this.listViewAssignReport.Location = new System.Drawing.Point(6, 34);
            this.listViewAssignReport.Name = "listViewAssignReport";
            this.listViewAssignReport.RightToLeftLayout = true;
            this.listViewAssignReport.Size = new System.Drawing.Size(827, 139);
            this.listViewAssignReport.TabIndex = 55;
            this.listViewAssignReport.UseCompatibleStateImageBehavior = false;
            this.listViewAssignReport.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "نوع المستند";
            this.columnHeader6.Width = 183;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "الرقم";
            this.columnHeader1.Width = 142;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "التاريخ";
            this.columnHeader2.Width = 186;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "الموظف";
            this.columnHeader3.Width = 206;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.BackColor = System.Drawing.Color.Aquamarine;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(1, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(833, 30);
            this.label16.TabIndex = 52;
            this.label16.Text = "السجل";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.comboBoxLevel);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.dateTimePickerEmployeeCreateDate);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.labelItemInfo);
            this.panel2.Controls.Add(this.labelEmployeementid);
            this.panel2.Controls.Add(this.textBoxEmployeementName);
            this.panel2.Controls.Add(this.textBoxEmployeeID);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(831, 94);
            this.panel2.TabIndex = 38;
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxLevel.FormattingEnabled = true;
            this.comboBoxLevel.Items.AddRange(new object[] {
            "ذكر",
            "انثى"});
            this.comboBoxLevel.Location = new System.Drawing.Point(53, 58);
            this.comboBoxLevel.Name = "comboBoxLevel";
            this.comboBoxLevel.Size = new System.Drawing.Size(200, 26);
            this.comboBoxLevel.TabIndex = 71;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(134, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 16);
            this.label17.TabIndex = 70;
            this.label17.Text = "المستوى الوظيفي";
            // 
            // dateTimePickerEmployeeCreateDate
            // 
            this.dateTimePickerEmployeeCreateDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerEmployeeCreateDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerEmployeeCreateDate.Location = new System.Drawing.Point(495, 59);
            this.dateTimePickerEmployeeCreateDate.Name = "dateTimePickerEmployeeCreateDate";
            this.dateTimePickerEmployeeCreateDate.Size = new System.Drawing.Size(204, 26);
            this.dateTimePickerEmployeeCreateDate.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(620, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 65;
            this.label2.Text = "تاريخ الانشاء";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(405, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 16);
            this.label13.TabIndex = 49;
            this.label13.Text = "اسم الوظيفة";
            // 
            // labelItemInfo
            // 
            this.labelItemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemInfo.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelItemInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelItemInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelItemInfo.Location = new System.Drawing.Point(-1, 0);
            this.labelItemInfo.Name = "labelItemInfo";
            this.labelItemInfo.Size = new System.Drawing.Size(831, 37);
            this.labelItemInfo.TabIndex = 31;
            this.labelItemInfo.Text = "بيانات الوظيفة";
            // 
            // labelEmployeementid
            // 
            this.labelEmployeementid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEmployeementid.AutoSize = true;
            this.labelEmployeementid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmployeementid.Location = new System.Drawing.Point(745, 39);
            this.labelEmployeementid.Name = "labelEmployeementid";
            this.labelEmployeementid.Size = new System.Drawing.Size(82, 16);
            this.labelEmployeementid.TabIndex = 42;
            this.labelEmployeementid.Text = "رقم الوظيفة";
            // 
            // textBoxEmployeementName
            // 
            this.textBoxEmployeementName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEmployeementName.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxEmployeementName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEmployeementName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxEmployeementName.Location = new System.Drawing.Point(279, 59);
            this.textBoxEmployeementName.Name = "textBoxEmployeementName";
            this.textBoxEmployeementName.Size = new System.Drawing.Size(210, 26);
            this.textBoxEmployeementName.TabIndex = 0;
            // 
            // textBoxEmployeeID
            // 
            this.textBoxEmployeeID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEmployeeID.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxEmployeeID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEmployeeID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxEmployeeID.Location = new System.Drawing.Point(725, 59);
            this.textBoxEmployeeID.Name = "textBoxEmployeeID";
            this.textBoxEmployeeID.Size = new System.Drawing.Size(93, 26);
            this.textBoxEmployeeID.TabIndex = 41;
            // 
            // EmployeeMent_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 460);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeMent_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "بيانات وظيفة";
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelSubDiagnosticOPR.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxPartName;
        private System.Windows.Forms.DateTimePicker dateTimePickerPartCreateDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBoxPartID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSubDiagnosticOPR;
        private System.Windows.Forms.ListView listViewAssignReport;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelItemInfo;
        private System.Windows.Forms.Label labelEmployeementid;
        private System.Windows.Forms.TextBox textBoxEmployeementName;
        private System.Windows.Forms.TextBox textBoxEmployeeID;
        private System.Windows.Forms.DateTimePicker dateTimePickerEmployeeCreateDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.Label label17;
    }
}