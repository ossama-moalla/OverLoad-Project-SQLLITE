namespace OverLoad_Client.Maintenance.Forms
{
    partial class RepairOPRForm
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
            this.panelSubDiagnosticOPR = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listViewItemOUT = new System.Windows.Forms.ListView();
            this.columnNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItemCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItemFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItemstate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConsumeUnit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItemPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTotalPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_MaintenanceOPR_Date = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMaintenanceOPRID = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxContactName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxReport = new System.Windows.Forms.TextBox();
            this.dateTimePickerOPRDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.panelSubDiagnosticOPR.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSubDiagnosticOPR
            // 
            this.panelSubDiagnosticOPR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSubDiagnosticOPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSubDiagnosticOPR.Controls.Add(this.label16);
            this.panelSubDiagnosticOPR.Location = new System.Drawing.Point(5, 423);
            this.panelSubDiagnosticOPR.Name = "panelSubDiagnosticOPR";
            this.panelSubDiagnosticOPR.Size = new System.Drawing.Size(1106, 156);
            this.panelSubDiagnosticOPR.TabIndex = 6;
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
            this.label16.Size = new System.Drawing.Size(1100, 30);
            this.label16.TabIndex = 52;
            this.label16.Text = "اختبارات تركيب عناصر";
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.CadetBlue;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.buttonCancel);
            this.panel9.Controls.Add(this.buttonSave);
            this.panel9.Location = new System.Drawing.Point(5, 585);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1106, 59);
            this.panel9.TabIndex = 8;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(401, 9);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(114, 37);
            this.buttonCancel.TabIndex = 56;
            this.buttonCancel.Text = "اغلاق";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(584, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 37);
            this.buttonSave.TabIndex = 55;
            this.buttonSave.Text = "حفظ";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.listViewItemOUT);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(5, 237);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1106, 180);
            this.panel3.TabIndex = 40;
            // 
            // listViewItemOUT
            // 
            this.listViewItemOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewItemOUT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewItemOUT.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNo,
            this.columnItem,
            this.columnItemCompany,
            this.columnItemFolder,
            this.columnItemstate,
            this.columnAmount,
            this.columnConsumeUnit,
            this.columnItemPrice,
            this.columnTotalPrice});
            this.listViewItemOUT.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.listViewItemOUT.FullRowSelect = true;
            this.listViewItemOUT.GridLines = true;
            this.listViewItemOUT.Location = new System.Drawing.Point(3, 29);
            this.listViewItemOUT.Name = "listViewItemOUT";
            this.listViewItemOUT.RightToLeftLayout = true;
            this.listViewItemOUT.Size = new System.Drawing.Size(1098, 146);
            this.listViewItemOUT.TabIndex = 56;
            this.listViewItemOUT.UseCompatibleStateImageBehavior = false;
            this.listViewItemOUT.View = System.Windows.Forms.View.Details;
            this.listViewItemOUT.Resize += new System.EventHandler(this.listViewItemsOUT_Resize);
            // 
            // columnNo
            // 
            this.columnNo.Text = "متسلسل";
            this.columnNo.Width = 80;
            // 
            // columnItem
            // 
            this.columnItem.Text = "العنصر";
            this.columnItem.Width = 140;
            // 
            // columnItemCompany
            // 
            this.columnItemCompany.Text = "الشركة";
            this.columnItemCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnItemCompany.Width = 124;
            // 
            // columnItemFolder
            // 
            this.columnItemFolder.Text = "الصنف";
            this.columnItemFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnItemFolder.Width = 112;
            // 
            // columnItemstate
            // 
            this.columnItemstate.Text = "حالة العنصر";
            this.columnItemstate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnItemstate.Width = 125;
            // 
            // columnAmount
            // 
            this.columnAmount.Text = "الكمية";
            this.columnAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnAmount.Width = 85;
            // 
            // columnConsumeUnit
            // 
            this.columnConsumeUnit.Text = "الوحدة";
            this.columnConsumeUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnConsumeUnit.Width = 100;
            // 
            // columnItemPrice
            // 
            this.columnItemPrice.Text = "السعر المفرد";
            this.columnItemPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnItemPrice.Width = 163;
            // 
            // columnTotalPrice
            // 
            this.columnTotalPrice.Text = "الاجمالي";
            this.columnTotalPrice.Width = 104;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.YellowGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1102, 26);
            this.label2.TabIndex = 50;
            this.label2.Text = "القطع المركبة";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBox_MaintenanceOPR_Date);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxMaintenanceOPRID);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.textBoxContactName);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(5, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1104, 83);
            this.panel2.TabIndex = 71;
            // 
            // textBox_MaintenanceOPR_Date
            // 
            this.textBox_MaintenanceOPR_Date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_MaintenanceOPR_Date.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_MaintenanceOPR_Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_MaintenanceOPR_Date.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBox_MaintenanceOPR_Date.Location = new System.Drawing.Point(717, 50);
            this.textBox_MaintenanceOPR_Date.Name = "textBox_MaintenanceOPR_Date";
            this.textBox_MaintenanceOPR_Date.ReadOnly = true;
            this.textBox_MaintenanceOPR_Date.Size = new System.Drawing.Size(230, 26);
            this.textBox_MaintenanceOPR_Date.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(953, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 68;
            this.label1.Text = "رقم  عملية الصيانة";
            // 
            // textBoxMaintenanceOPRID
            // 
            this.textBoxMaintenanceOPRID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMaintenanceOPRID.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxMaintenanceOPRID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMaintenanceOPRID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxMaintenanceOPRID.Location = new System.Drawing.Point(953, 50);
            this.textBoxMaintenanceOPRID.Name = "textBoxMaintenanceOPRID";
            this.textBoxMaintenanceOPRID.ReadOnly = true;
            this.textBoxMaintenanceOPRID.Size = new System.Drawing.Size(120, 26);
            this.textBoxMaintenanceOPRID.TabIndex = 67;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(638, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 16);
            this.label20.TabIndex = 66;
            this.label20.Text = "اسم الزبون";
            // 
            // textBoxContactName
            // 
            this.textBoxContactName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContactName.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxContactName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContactName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxContactName.Location = new System.Drawing.Point(453, 50);
            this.textBoxContactName.Name = "textBoxContactName";
            this.textBoxContactName.ReadOnly = true;
            this.textBoxContactName.Size = new System.Drawing.Size(258, 26);
            this.textBoxContactName.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(901, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 16);
            this.label9.TabIndex = 63;
            this.label9.Text = "التاريخ";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.Color.Aquamarine;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(-2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1105, 24);
            this.label11.TabIndex = 60;
            this.label11.Text = "معلومات عملية الصيانة";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.textBoxID);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.textBoxReport);
            this.panel4.Controls.Add(this.dateTimePickerOPRDate);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.textBoxDesc);
            this.panel4.Location = new System.Drawing.Point(5, 95);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1104, 136);
            this.panel4.TabIndex = 70;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(971, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 16);
            this.label8.TabIndex = 67;
            this.label8.Text = "رقم عملية الاصلاح";
            // 
            // textBoxID
            // 
            this.textBoxID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxID.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxID.Location = new System.Drawing.Point(960, 49);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(127, 26);
            this.textBoxID.TabIndex = 66;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.Aquamarine;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(-1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1104, 28);
            this.label15.TabIndex = 64;
            this.label15.Text = " معلومات عملية اصلاح";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1041, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 54;
            this.label4.Text = "تفاصيل";
            // 
            // textBoxReport
            // 
            this.textBoxReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReport.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxReport.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxReport.Location = new System.Drawing.Point(14, 97);
            this.textBoxReport.Multiline = true;
            this.textBoxReport.Name = "textBoxReport";
            this.textBoxReport.Size = new System.Drawing.Size(1073, 32);
            this.textBoxReport.TabIndex = 53;
            // 
            // dateTimePickerOPRDate
            // 
            this.dateTimePickerOPRDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerOPRDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerOPRDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerOPRDate.Location = new System.Drawing.Point(763, 49);
            this.dateTimePickerOPRDate.Name = "dateTimePickerOPRDate";
            this.dateTimePickerOPRDate.Size = new System.Drawing.Size(174, 26);
            this.dateTimePickerOPRDate.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(895, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 51;
            this.label5.Text = "التاريخ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(611, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 48;
            this.label3.Text = "وصف عملية الصيانة";
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDesc.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDesc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxDesc.Location = new System.Drawing.Point(14, 49);
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(726, 26);
            this.textBoxDesc.TabIndex = 47;
            // 
            // RepairOPRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 644);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panelSubDiagnosticOPR);
            this.Name = "RepairOPRForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "عملية اصلاح";
            this.panelSubDiagnosticOPR.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelSubDiagnosticOPR;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView listViewItemOUT;
        private System.Windows.Forms.ColumnHeader columnNo;
        private System.Windows.Forms.ColumnHeader columnItem;
        private System.Windows.Forms.ColumnHeader columnItemCompany;
        private System.Windows.Forms.ColumnHeader columnItemFolder;
        private System.Windows.Forms.ColumnHeader columnItemstate;
        private System.Windows.Forms.ColumnHeader columnAmount;
        private System.Windows.Forms.ColumnHeader columnConsumeUnit;
        private System.Windows.Forms.ColumnHeader columnItemPrice;
        private System.Windows.Forms.ColumnHeader columnTotalPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_MaintenanceOPR_Date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMaintenanceOPRID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxContactName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxReport;
        private System.Windows.Forms.DateTimePicker dateTimePickerOPRDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDesc;
    }
}