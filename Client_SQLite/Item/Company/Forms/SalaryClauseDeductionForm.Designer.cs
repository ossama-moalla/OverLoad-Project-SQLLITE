namespace OverLoad_Client.Company.Forms
{
    partial class SalaryClauseDeductionForm
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
            this.dateTimePickerExecutedate = new System.Windows.Forms.DateTimePicker();
            this.textBoxMonthCount = new System.Windows.Forms.TextBox();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.textBoxClauseValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxEnableMonthCount = new System.Windows.Forms.CheckBox();
            this.textBoxClauseDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textboxEmployeeID = new System.Windows.Forms.TextBox();
            this.textBoxCreateDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxClauseID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxNotes = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
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
            this.panel5.Location = new System.Drawing.Point(6, 344);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(581, 65);
            this.panel5.TabIndex = 45;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(140, 12);
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
            this.buttonSave.Location = new System.Drawing.Point(329, 12);
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
            this.panel6.Controls.Add(this.dateTimePickerExecutedate);
            this.panel6.Controls.Add(this.textBoxMonthCount);
            this.panel6.Controls.Add(this.labelCurrency);
            this.panel6.Controls.Add(this.textBoxClauseValue);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.checkBoxEnableMonthCount);
            this.panel6.Controls.Add(this.textBoxClauseDesc);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.textBoxName);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.textboxEmployeeID);
            this.panel6.Controls.Add(this.textBoxCreateDate);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.textBoxClauseID);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.textboxNotes);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Location = new System.Drawing.Point(6, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(581, 335);
            this.panel6.TabIndex = 44;
            // 
            // dateTimePickerExecutedate
            // 
            this.dateTimePickerExecutedate.CustomFormat = "MMM yyyy";
            this.dateTimePickerExecutedate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerExecutedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerExecutedate.Location = new System.Drawing.Point(140, 210);
            this.dateTimePickerExecutedate.Name = "dateTimePickerExecutedate";
            this.dateTimePickerExecutedate.ShowUpDown = true;
            this.dateTimePickerExecutedate.Size = new System.Drawing.Size(176, 27);
            this.dateTimePickerExecutedate.TabIndex = 95;
            // 
            // textBoxMonthCount
            // 
            this.textBoxMonthCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMonthCount.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMonthCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMonthCount.Enabled = false;
            this.textBoxMonthCount.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxMonthCount.Location = new System.Drawing.Point(264, 252);
            this.textBoxMonthCount.Name = "textBoxMonthCount";
            this.textBoxMonthCount.Size = new System.Drawing.Size(59, 26);
            this.textBoxMonthCount.TabIndex = 94;
            // 
            // labelCurrency
            // 
            this.labelCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrency.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelCurrency.Location = new System.Drawing.Point(209, 175);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(157, 26);
            this.labelCurrency.TabIndex = 91;
            this.labelCurrency.Text = "ليرة سورية";
            // 
            // textBoxClauseValue
            // 
            this.textBoxClauseValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClauseValue.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxClauseValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxClauseValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxClauseValue.Location = new System.Drawing.Point(372, 175);
            this.textBoxClauseValue.Name = "textBoxClauseValue";
            this.textBoxClauseValue.Size = new System.Drawing.Size(104, 26);
            this.textBoxClauseValue.TabIndex = 90;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(490, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 89;
            this.label8.Text = "القيمة";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(209, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 26);
            this.label6.TabIndex = 88;
            this.label6.Text = "شهر";
            // 
            // checkBoxEnableMonthCount
            // 
            this.checkBoxEnableMonthCount.AutoSize = true;
            this.checkBoxEnableMonthCount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkBoxEnableMonthCount.Location = new System.Drawing.Point(329, 258);
            this.checkBoxEnableMonthCount.Name = "checkBoxEnableMonthCount";
            this.checkBoxEnableMonthCount.Size = new System.Drawing.Size(130, 20);
            this.checkBoxEnableMonthCount.TabIndex = 86;
            this.checkBoxEnableMonthCount.Text = "عدد مرات الصرف";
            this.checkBoxEnableMonthCount.UseVisualStyleBackColor = true;
            this.checkBoxEnableMonthCount.CheckedChanged += new System.EventHandler(this.checkBoxEnableMonthCount_CheckedChanged);
            // 
            // textBoxClauseDesc
            // 
            this.textBoxClauseDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClauseDesc.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxClauseDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxClauseDesc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxClauseDesc.Location = new System.Drawing.Point(210, 138);
            this.textBoxClauseDesc.Name = "textBoxClauseDesc";
            this.textBoxClauseDesc.Size = new System.Drawing.Size(266, 26);
            this.textBoxClauseDesc.TabIndex = 80;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(493, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 79;
            this.label1.Text = "وصف البند";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(264, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 78;
            this.label4.Text = "الاسم";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxName.Location = new System.Drawing.Point(21, 87);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(210, 26);
            this.textBoxName.TabIndex = 77;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(235, 59);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 16);
            this.label23.TabIndex = 76;
            this.label23.Text = "رقم الموظف";
            // 
            // textboxEmployeeID
            // 
            this.textboxEmployeeID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxEmployeeID.BackColor = System.Drawing.SystemColors.Menu;
            this.textboxEmployeeID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxEmployeeID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textboxEmployeeID.Location = new System.Drawing.Point(125, 55);
            this.textboxEmployeeID.Name = "textboxEmployeeID";
            this.textboxEmployeeID.ReadOnly = true;
            this.textboxEmployeeID.Size = new System.Drawing.Size(106, 26);
            this.textboxEmployeeID.TabIndex = 75;
            // 
            // textBoxCreateDate
            // 
            this.textBoxCreateDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCreateDate.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxCreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCreateDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxCreateDate.Location = new System.Drawing.Point(343, 87);
            this.textBoxCreateDate.Name = "textBoxCreateDate";
            this.textBoxCreateDate.ReadOnly = true;
            this.textBoxCreateDate.Size = new System.Drawing.Size(129, 26);
            this.textBoxCreateDate.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(478, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 73;
            this.label2.Text = "رقم البند";
            // 
            // textBoxClauseID
            // 
            this.textBoxClauseID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxClauseID.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxClauseID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxClauseID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxClauseID.Location = new System.Drawing.Point(343, 55);
            this.textBoxClauseID.Name = "textBoxClauseID";
            this.textBoxClauseID.ReadOnly = true;
            this.textBoxClauseID.Size = new System.Drawing.Size(129, 26);
            this.textBoxClauseID.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(490, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 71;
            this.label3.Text = "تاريخ الانشاء";
            // 
            // textboxNotes
            // 
            this.textboxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxNotes.BackColor = System.Drawing.SystemColors.Window;
            this.textboxNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxNotes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textboxNotes.Location = new System.Drawing.Point(21, 300);
            this.textboxNotes.MaxLength = 200;
            this.textboxNotes.Name = "textboxNotes";
            this.textboxNotes.Size = new System.Drawing.Size(455, 26);
            this.textboxNotes.TabIndex = 65;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(490, 300);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 16);
            this.label13.TabIndex = 64;
            this.label13.Text = "ملاحظات";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(329, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 16);
            this.label5.TabIndex = 50;
            this.label5.Text = "يدخل في الراتب بدا من بداية شهر";
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
            this.label22.Size = new System.Drawing.Size(581, 37);
            this.label22.TabIndex = 31;
            this.label22.Text = "بند استقطاع";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SalaryClauseDeductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 412);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Name = "SalaryClauseDeductionForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "استقطاع";
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox textBoxClauseDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textboxEmployeeID;
        private System.Windows.Forms.TextBox textBoxCreateDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxClauseID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxNotes;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox checkBoxEnableMonthCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelCurrency;
        private System.Windows.Forms.TextBox textBoxClauseValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMonthCount;
        private System.Windows.Forms.DateTimePicker dateTimePickerExecutedate;
    }
}