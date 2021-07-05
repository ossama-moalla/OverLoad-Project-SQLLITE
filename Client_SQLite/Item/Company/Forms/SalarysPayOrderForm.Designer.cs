namespace OverLoad_Client.Company.Forms
{
    partial class SalarysPayOrderForm
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxfilterbyemployeestate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxfilterbysalaryset = new System.Windows.Forms.ComboBox();
            this.textboxNotes = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dateTimePickerExecutedate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSalaryPayOrderDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSalaryPayOrderID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_Real_PaysOUT = new System.Windows.Forms.TextBox();
            this.textBox_PaysOUT = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRealValueAll = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxValueAll = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listViewEmployeesSalaries = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.CadetBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.buttonCancel);
            this.panel5.Controls.Add(this.buttonSave);
            this.panel5.Location = new System.Drawing.Point(1, 457);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1109, 65);
            this.panel5.TabIndex = 51;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(404, 12);
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
            this.buttonSave.Location = new System.Drawing.Point(593, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 37);
            this.buttonSave.TabIndex = 55;
            this.buttonSave.Text = "حفظ";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.groupBoxFilter);
            this.panel6.Controls.Add(this.textboxNotes);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.dateTimePickerExecutedate);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.textBoxSalaryPayOrderDate);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.textBoxSalaryPayOrderID);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Location = new System.Drawing.Point(1, 21);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1109, 113);
            this.panel6.TabIndex = 50;
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.label5);
            this.groupBoxFilter.Controls.Add(this.comboBoxfilterbyemployeestate);
            this.groupBoxFilter.Controls.Add(this.label1);
            this.groupBoxFilter.Controls.Add(this.comboBoxfilterbysalaryset);
            this.groupBoxFilter.Location = new System.Drawing.Point(9, 35);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(451, 77);
            this.groupBoxFilter.TabIndex = 101;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "فلتر";
            this.groupBoxFilter.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(273, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 24);
            this.label5.TabIndex = 108;
            this.label5.Text = "حسب حالة الموظف";
            // 
            // comboBoxfilterbyemployeestate
            // 
            this.comboBoxfilterbyemployeestate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxfilterbyemployeestate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxfilterbyemployeestate.FormattingEnabled = true;
            this.comboBoxfilterbyemployeestate.Location = new System.Drawing.Point(211, 45);
            this.comboBoxfilterbyemployeestate.Name = "comboBoxfilterbyemployeestate";
            this.comboBoxfilterbyemployeestate.Size = new System.Drawing.Size(222, 26);
            this.comboBoxfilterbyemployeestate.TabIndex = 107;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 24);
            this.label1.TabIndex = 106;
            this.label1.Text = "حسب حالة الراتب";
            // 
            // comboBoxfilterbysalaryset
            // 
            this.comboBoxfilterbysalaryset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxfilterbysalaryset.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxfilterbysalaryset.FormattingEnabled = true;
            this.comboBoxfilterbysalaryset.Items.AddRange(new object[] {
            "الكل",
            "من لم يتم تحديد راتبه",
            "من تم تحديد راتبه"});
            this.comboBoxfilterbysalaryset.Location = new System.Drawing.Point(18, 45);
            this.comboBoxfilterbysalaryset.Name = "comboBoxfilterbysalaryset";
            this.comboBoxfilterbysalaryset.Size = new System.Drawing.Size(170, 26);
            this.comboBoxfilterbysalaryset.TabIndex = 105;
            // 
            // textboxNotes
            // 
            this.textboxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxNotes.BackColor = System.Drawing.SystemColors.Window;
            this.textboxNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxNotes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textboxNotes.Location = new System.Drawing.Point(466, 78);
            this.textboxNotes.Name = "textboxNotes";
            this.textboxNotes.Size = new System.Drawing.Size(534, 26);
            this.textboxNotes.TabIndex = 100;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1014, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 16);
            this.label13.TabIndex = 99;
            this.label13.Text = "ملاحظات";
            // 
            // dateTimePickerExecutedate
            // 
            this.dateTimePickerExecutedate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerExecutedate.CustomFormat = "MMM yyyy";
            this.dateTimePickerExecutedate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerExecutedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerExecutedate.Location = new System.Drawing.Point(466, 35);
            this.dateTimePickerExecutedate.Name = "dateTimePickerExecutedate";
            this.dateTimePickerExecutedate.ShowUpDown = true;
            this.dateTimePickerExecutedate.Size = new System.Drawing.Size(193, 27);
            this.dateTimePickerExecutedate.TabIndex = 98;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(665, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 22);
            this.label4.TabIndex = 75;
            this.label4.Text = "لشهر";
            // 
            // textBoxSalaryPayOrderDate
            // 
            this.textBoxSalaryPayOrderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSalaryPayOrderDate.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxSalaryPayOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSalaryPayOrderDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxSalaryPayOrderDate.Location = new System.Drawing.Point(732, 40);
            this.textBoxSalaryPayOrderDate.Name = "textBoxSalaryPayOrderDate";
            this.textBoxSalaryPayOrderDate.ReadOnly = true;
            this.textBoxSalaryPayOrderDate.Size = new System.Drawing.Size(129, 26);
            this.textBoxSalaryPayOrderDate.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1006, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 22);
            this.label2.TabIndex = 73;
            this.label2.Text = "رقم الأمر";
            // 
            // textBoxSalaryPayOrderID
            // 
            this.textBoxSalaryPayOrderID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSalaryPayOrderID.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxSalaryPayOrderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSalaryPayOrderID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxSalaryPayOrderID.Location = new System.Drawing.Point(924, 40);
            this.textBoxSalaryPayOrderID.Name = "textBoxSalaryPayOrderID";
            this.textBoxSalaryPayOrderID.ReadOnly = true;
            this.textBoxSalaryPayOrderID.Size = new System.Drawing.Size(76, 26);
            this.textBoxSalaryPayOrderID.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(867, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 26);
            this.label3.TabIndex = 71;
            this.label3.Text = "التاريخ";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.BackColor = System.Drawing.Color.Aquamarine;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(-1, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1109, 29);
            this.label22.TabIndex = 31;
            this.label22.Text = "أمرصرف الرواتب";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBox_Real_PaysOUT);
            this.panel1.Controls.Add(this.textBox_PaysOUT);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBoxRealValueAll);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBoxValueAll);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.listViewEmployeesSalaries);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(1, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1109, 311);
            this.panel1.TabIndex = 57;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 251);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(158, 26);
            this.label9.TabIndex = 90;
            this.label9.Text = "القيمة الفعلية للمدفوع";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_Real_PaysOUT
            // 
            this.textBox_Real_PaysOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Real_PaysOUT.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_Real_PaysOUT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Real_PaysOUT.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBox_Real_PaysOUT.Location = new System.Drawing.Point(27, 280);
            this.textBox_Real_PaysOUT.Name = "textBox_Real_PaysOUT";
            this.textBox_Real_PaysOUT.ReadOnly = true;
            this.textBox_Real_PaysOUT.Size = new System.Drawing.Size(137, 26);
            this.textBox_Real_PaysOUT.TabIndex = 82;
            // 
            // textBox_PaysOUT
            // 
            this.textBox_PaysOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_PaysOUT.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_PaysOUT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_PaysOUT.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBox_PaysOUT.Location = new System.Drawing.Point(187, 280);
            this.textBox_PaysOUT.Name = "textBox_PaysOUT";
            this.textBox_PaysOUT.ReadOnly = true;
            this.textBox_PaysOUT.Size = new System.Drawing.Size(344, 26);
            this.textBox_PaysOUT.TabIndex = 80;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(435, 251);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 26);
            this.label10.TabIndex = 79;
            this.label10.Text = "الملبغ المدفوع";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxRealValueAll
            // 
            this.textBoxRealValueAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRealValueAll.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxRealValueAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRealValueAll.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxRealValueAll.Location = new System.Drawing.Point(562, 280);
            this.textBoxRealValueAll.Name = "textBoxRealValueAll";
            this.textBoxRealValueAll.ReadOnly = true;
            this.textBoxRealValueAll.Size = new System.Drawing.Size(133, 26);
            this.textBoxRealValueAll.TabIndex = 78;
            this.textBoxRealValueAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(562, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 26);
            this.label7.TabIndex = 77;
            this.label7.Text = "القيمة الفعلية الكلية";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValueAll
            // 
            this.textBoxValueAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValueAll.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxValueAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxValueAll.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxValueAll.Location = new System.Drawing.Point(722, 280);
            this.textBoxValueAll.Name = "textBoxValueAll";
            this.textBoxValueAll.ReadOnly = true;
            this.textBoxValueAll.Size = new System.Drawing.Size(375, 26);
            this.textBoxValueAll.TabIndex = 76;
            this.textBoxValueAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxValueAll.WordWrap = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1001, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 26);
            this.label6.TabIndex = 75;
            this.label6.Text = "المبلغ الكلي";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewEmployeesSalaries
            // 
            this.listViewEmployeesSalaries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewEmployeesSalaries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader7,
            this.columnHeader10,
            this.columnHeader1,
            this.columnHeader3});
            this.listViewEmployeesSalaries.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewEmployeesSalaries.FullRowSelect = true;
            this.listViewEmployeesSalaries.GridLines = true;
            this.listViewEmployeesSalaries.Location = new System.Drawing.Point(3, 33);
            this.listViewEmployeesSalaries.Name = "listViewEmployeesSalaries";
            this.listViewEmployeesSalaries.RightToLeftLayout = true;
            this.listViewEmployeesSalaries.Size = new System.Drawing.Size(1101, 212);
            this.listViewEmployeesSalaries.TabIndex = 32;
            this.listViewEmployeesSalaries.UseCompatibleStateImageBehavior = false;
            this.listViewEmployeesSalaries.View = System.Windows.Forms.View.Details;
            this.listViewEmployeesSalaries.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewEmployeesSalaries_MouseDoubleClick);
            this.listViewEmployeesSalaries.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewEmployeesSalaries_MouseDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "رقم الموظف";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "اسم الموظف";
            this.columnHeader5.Width = 144;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "الراتب المتوقع";
            this.columnHeader4.Width = 112;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "الراتب المصروف";
            this.columnHeader6.Width = 123;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "المدفوع";
            this.columnHeader8.Width = 107;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "الباقي";
            this.columnHeader9.Width = 83;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "القيمة الفعلية";
            this.columnHeader7.Width = 127;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "المدفوع الفعلي";
            this.columnHeader10.Width = 139;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "حالة العمل";
            this.columnHeader1.Width = 107;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "حالة التوظيف";
            this.columnHeader3.Width = 165;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.Aquamarine;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(-1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1109, 30);
            this.label8.TabIndex = 31;
            this.label8.Text = "الموظفين المدرجين";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SalarysPayOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Name = "SalarysPayOrderForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "صرف الواتب";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SalarysPayOrderForm_Load);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox textBoxSalaryPayOrderDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSalaryPayOrderID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerExecutedate;
        private System.Windows.Forms.TextBox textboxNotes;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListView listViewEmployeesSalaries;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxfilterbyemployeestate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxfilterbysalaryset;
        private System.Windows.Forms.TextBox textBoxRealValueAll;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxValueAll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Real_PaysOUT;
        private System.Windows.Forms.TextBox textBox_PaysOUT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}