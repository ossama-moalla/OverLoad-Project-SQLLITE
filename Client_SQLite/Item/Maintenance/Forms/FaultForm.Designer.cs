namespace OverLoad_Client.Maintenance.Forms
{
    partial class FaultForm
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxItemType = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxReport = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxFaultDesc = new System.Windows.Forms.ComboBox();
            this.dateTimePickerFaultDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxItemCompany = new System.Windows.Forms.TextBox();
            this.labelItemInfo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.textBoxItemID = new System.Windows.Forms.TextBox();
            this.panelRepairOPR = new System.Windows.Forms.Panel();
            this.listViewRepairOPR = new System.Windows.Forms.ListView();
            this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader41 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader42 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader43 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader44 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader45 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMOPR = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxContact = new System.Windows.Forms.TextBox();
            this.dateTimePickerMainteneaceOPRDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLink = new System.Windows.Forms.Panel();
            this.panelSubDiagnosticOPR = new System.Windows.Forms.Panel();
            this.listViewTags = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label16 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelRepairOPR.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelLink.SuspendLayout();
            this.panelSubDiagnosticOPR.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(583, 7);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 37);
            this.buttonSave.TabIndex = 57;
            this.buttonSave.Text = "حفظ";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.buttonCancel);
            this.panel4.Controls.Add(this.buttonSave);
            this.panel4.Location = new System.Drawing.Point(5, 617);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1092, 53);
            this.panel4.TabIndex = 5;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(394, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(114, 37);
            this.buttonCancel.TabIndex = 58;
            this.buttonCancel.Text = "اغلاق";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(584, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 16);
            this.label13.TabIndex = 49;
            this.label13.Text = "اسم العنصر";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(211, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 16);
            this.label18.TabIndex = 48;
            this.label18.Text = "الصنف\r\n";
            // 
            // textBoxItemType
            // 
            this.textBoxItemType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemType.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxItemType.Location = new System.Drawing.Point(88, 59);
            this.textBoxItemType.Name = "textBoxItemType";
            this.textBoxItemType.ReadOnly = true;
            this.textBoxItemType.Size = new System.Drawing.Size(168, 26);
            this.textBoxItemType.TabIndex = 47;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(412, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 16);
            this.label17.TabIndex = 46;
            this.label17.Text = "الشركة";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(992, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 51;
            this.label2.Text = "وصف العطل";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBoxReport);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.comboBoxFaultDesc);
            this.panel2.Controls.Add(this.dateTimePickerFaultDate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.textBoxItemType);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.textBoxItemCompany);
            this.panel2.Controls.Add(this.labelItemInfo);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBoxItemName);
            this.panel2.Controls.Add(this.textBoxItemID);
            this.panel2.Location = new System.Drawing.Point(5, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1091, 150);
            this.panel2.TabIndex = 37;
            // 
            // textBoxReport
            // 
            this.textBoxReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReport.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxReport.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxReport.Location = new System.Drawing.Point(88, 114);
            this.textBoxReport.Name = "textBoxReport";
            this.textBoxReport.Size = new System.Drawing.Size(574, 26);
            this.textBoxReport.TabIndex = 71;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(614, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 16);
            this.label9.TabIndex = 70;
            this.label9.Text = "تفاصيل";
            // 
            // comboBoxFaultDesc
            // 
            this.comboBoxFaultDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFaultDesc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxFaultDesc.FormattingEnabled = true;
            this.comboBoxFaultDesc.Location = new System.Drawing.Point(866, 114);
            this.comboBoxFaultDesc.Name = "comboBoxFaultDesc";
            this.comboBoxFaultDesc.Size = new System.Drawing.Size(206, 26);
            this.comboBoxFaultDesc.TabIndex = 69;
            // 
            // dateTimePickerFaultDate
            // 
            this.dateTimePickerFaultDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerFaultDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerFaultDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFaultDate.Location = new System.Drawing.Point(866, 59);
            this.dateTimePickerFaultDate.Name = "dateTimePickerFaultDate";
            this.dateTimePickerFaultDate.Size = new System.Drawing.Size(208, 26);
            this.dateTimePickerFaultDate.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1032, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 67;
            this.label4.Text = "التاريخ";
            // 
            // textBoxItemCompany
            // 
            this.textBoxItemCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemCompany.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxItemCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxItemCompany.Location = new System.Drawing.Point(291, 59);
            this.textBoxItemCompany.Name = "textBoxItemCompany";
            this.textBoxItemCompany.ReadOnly = true;
            this.textBoxItemCompany.Size = new System.Drawing.Size(172, 26);
            this.textBoxItemCompany.TabIndex = 45;
            // 
            // labelItemInfo
            // 
            this.labelItemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemInfo.BackColor = System.Drawing.Color.Aquamarine;
            this.labelItemInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelItemInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelItemInfo.Location = new System.Drawing.Point(-1, 0);
            this.labelItemInfo.Name = "labelItemInfo";
            this.labelItemInfo.Size = new System.Drawing.Size(1091, 37);
            this.labelItemInfo.TabIndex = 31;
            this.labelItemInfo.Text = "أدخل معرف العنصر او نقرتين لفتح النافذة ";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(719, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "رقم العنصر";
            // 
            // textBoxItemName
            // 
            this.textBoxItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemName.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxItemName.Location = new System.Drawing.Point(490, 59);
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.ReadOnly = true;
            this.textBoxItemName.Size = new System.Drawing.Size(172, 26);
            this.textBoxItemName.TabIndex = 43;
            // 
            // textBoxItemID
            // 
            this.textBoxItemID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemID.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxItemID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxItemID.Location = new System.Drawing.Point(685, 59);
            this.textBoxItemID.Name = "textBoxItemID";
            this.textBoxItemID.Size = new System.Drawing.Size(107, 26);
            this.textBoxItemID.TabIndex = 41;
            // 
            // panelRepairOPR
            // 
            this.panelRepairOPR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRepairOPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRepairOPR.Controls.Add(this.listViewRepairOPR);
            this.panelRepairOPR.Controls.Add(this.label3);
            this.panelRepairOPR.Location = new System.Drawing.Point(5, 438);
            this.panelRepairOPR.Name = "panelRepairOPR";
            this.panelRepairOPR.Size = new System.Drawing.Size(1092, 173);
            this.panelRepairOPR.TabIndex = 39;
            // 
            // listViewRepairOPR
            // 
            this.listViewRepairOPR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRepairOPR.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader40,
            this.columnHeader41,
            this.columnHeader42,
            this.columnHeader43,
            this.columnHeader44,
            this.columnHeader45});
            this.listViewRepairOPR.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewRepairOPR.FullRowSelect = true;
            this.listViewRepairOPR.GridLines = true;
            this.listViewRepairOPR.Location = new System.Drawing.Point(1, 34);
            this.listViewRepairOPR.Name = "listViewRepairOPR";
            this.listViewRepairOPR.RightToLeftLayout = true;
            this.listViewRepairOPR.Size = new System.Drawing.Size(1086, 130);
            this.listViewRepairOPR.TabIndex = 56;
            this.listViewRepairOPR.UseCompatibleStateImageBehavior = false;
            this.listViewRepairOPR.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader40
            // 
            this.columnHeader40.Text = "الرقم";
            this.columnHeader40.Width = 70;
            // 
            // columnHeader41
            // 
            this.columnHeader41.Text = "التاريخ";
            this.columnHeader41.Width = 114;
            // 
            // columnHeader42
            // 
            this.columnHeader42.Text = "الوصف";
            this.columnHeader42.Width = 217;
            // 
            // columnHeader43
            // 
            this.columnHeader43.Text = "التقرير";
            this.columnHeader43.Width = 445;
            // 
            // columnHeader44
            // 
            this.columnHeader44.Text = "العناصر المركبة";
            this.columnHeader44.Width = 125;
            // 
            // columnHeader45
            // 
            this.columnHeader45.Text = "اختبار تركيب العناصر";
            this.columnHeader45.Width = 156;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Aquamarine;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1086, 30);
            this.label3.TabIndex = 52;
            this.label3.Text = "عمليات الاصلاح المساهمة في اصلاح العطل";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.textBoxMOPR);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.textBoxContact);
            this.panel3.Controls.Add(this.dateTimePickerMainteneaceOPRDate);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(5, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1091, 93);
            this.panel3.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(969, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 16);
            this.label6.TabIndex = 68;
            this.label6.Text = "رقم  عملية الصيانة";
            // 
            // textBoxMOPR
            // 
            this.textBoxMOPR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMOPR.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxMOPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMOPR.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxMOPR.Location = new System.Drawing.Point(969, 58);
            this.textBoxMOPR.Name = "textBoxMOPR";
            this.textBoxMOPR.ReadOnly = true;
            this.textBoxMOPR.Size = new System.Drawing.Size(120, 26);
            this.textBoxMOPR.TabIndex = 67;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(654, 39);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 16);
            this.label20.TabIndex = 66;
            this.label20.Text = "اسم الزبون";
            // 
            // textBoxContact
            // 
            this.textBoxContact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContact.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContact.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxContact.Location = new System.Drawing.Point(469, 58);
            this.textBoxContact.Name = "textBoxContact";
            this.textBoxContact.ReadOnly = true;
            this.textBoxContact.Size = new System.Drawing.Size(258, 26);
            this.textBoxContact.TabIndex = 65;
            // 
            // dateTimePickerMainteneaceOPRDate
            // 
            this.dateTimePickerMainteneaceOPRDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerMainteneaceOPRDate.Enabled = false;
            this.dateTimePickerMainteneaceOPRDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerMainteneaceOPRDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerMainteneaceOPRDate.Location = new System.Drawing.Point(752, 58);
            this.dateTimePickerMainteneaceOPRDate.Name = "dateTimePickerMainteneaceOPRDate";
            this.dateTimePickerMainteneaceOPRDate.Size = new System.Drawing.Size(211, 26);
            this.dateTimePickerMainteneaceOPRDate.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(917, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "التاريخ";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1092, 35);
            this.label1.TabIndex = 60;
            this.label1.Text = "معلومات عملية الصيانة";
            // 
            // panelLink
            // 
            this.panelLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLink.Controls.Add(this.panelSubDiagnosticOPR);
            this.panelLink.Location = new System.Drawing.Point(6, 275);
            this.panelLink.Name = "panelLink";
            this.panelLink.Size = new System.Drawing.Size(1090, 157);
            this.panelLink.TabIndex = 42;
            // 
            // panelSubDiagnosticOPR
            // 
            this.panelSubDiagnosticOPR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSubDiagnosticOPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSubDiagnosticOPR.Controls.Add(this.listViewTags);
            this.panelSubDiagnosticOPR.Controls.Add(this.label16);
            this.panelSubDiagnosticOPR.Location = new System.Drawing.Point(-4, 4);
            this.panelSubDiagnosticOPR.Name = "panelSubDiagnosticOPR";
            this.panelSubDiagnosticOPR.Size = new System.Drawing.Size(1089, 148);
            this.panelSubDiagnosticOPR.TabIndex = 42;
            // 
            // listViewTags
            // 
            this.listViewTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewTags.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewTags.FullRowSelect = true;
            this.listViewTags.GridLines = true;
            this.listViewTags.Location = new System.Drawing.Point(6, 34);
            this.listViewTags.Name = "listViewTags";
            this.listViewTags.RightToLeftLayout = true;
            this.listViewTags.Size = new System.Drawing.Size(1077, 109);
            this.listViewTags.TabIndex = 55;
            this.listViewTags.UseCompatibleStateImageBehavior = false;
            this.listViewTags.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "نوع الربط";
            this.columnHeader6.Width = 243;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "الرقم";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "الوصف";
            this.columnHeader2.Width = 335;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "تفاصيل عملية الربط";
            this.columnHeader3.Width = 335;
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
            this.label16.Size = new System.Drawing.Size(1083, 30);
            this.label16.TabIndex = 52;
            this.label16.Text = "عمليات الربط";
            // 
            // FaultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 678);
            this.Controls.Add(this.panelLink);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelRepairOPR);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Name = "FaultForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "عطل";
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelRepairOPR.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelLink.ResumeLayout(false);
            this.panelSubDiagnosticOPR.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxItemType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dateTimePickerFaultDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxItemCompany;
        private System.Windows.Forms.Label labelItemInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.TextBox textBoxItemID;
        private System.Windows.Forms.ComboBox comboBoxFaultDesc;
        private System.Windows.Forms.Panel panelRepairOPR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMOPR;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxContact;
        private System.Windows.Forms.DateTimePicker dateTimePickerMainteneaceOPRDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLink;
        private System.Windows.Forms.Panel panelSubDiagnosticOPR;
        private System.Windows.Forms.ListView listViewTags;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxReport;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listViewRepairOPR;
        private System.Windows.Forms.ColumnHeader columnHeader40;
        private System.Windows.Forms.ColumnHeader columnHeader41;
        private System.Windows.Forms.ColumnHeader columnHeader42;
        private System.Windows.Forms.ColumnHeader columnHeader43;
        private System.Windows.Forms.ColumnHeader columnHeader44;
        private System.Windows.Forms.ColumnHeader columnHeader45;
    }
}