namespace OverLoad_Client.Company.Forms
{
    partial class EmployeePayOrderForm
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
            this.dateTimePickerPayOrderDate = new System.Windows.Forms.DateTimePicker();
            this.labelExchangeRate = new System.Windows.Forms.Label();
            this.textBoxPayOrderExchangeRate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxPayOrderCurrency = new System.Windows.Forms.ComboBox();
            this.textBoxPayOrderValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxPayOrderDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textboxEmployeeID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPayOrderID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.panel5.Location = new System.Drawing.Point(6, 309);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(581, 65);
            this.panel5.TabIndex = 47;
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
            this.panel6.Controls.Add(this.dateTimePickerPayOrderDate);
            this.panel6.Controls.Add(this.labelExchangeRate);
            this.panel6.Controls.Add(this.textBoxPayOrderExchangeRate);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.comboBoxPayOrderCurrency);
            this.panel6.Controls.Add(this.textBoxPayOrderValue);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.textBoxPayOrderDesc);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.textBoxName);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.textboxEmployeeID);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.textBoxPayOrderID);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Location = new System.Drawing.Point(6, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(581, 300);
            this.panel6.TabIndex = 46;
            // 
            // dateTimePickerPayOrderDate
            // 
            this.dateTimePickerPayOrderDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePickerPayOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerPayOrderDate.Location = new System.Drawing.Point(343, 93);
            this.dateTimePickerPayOrderDate.Name = "dateTimePickerPayOrderDate";
            this.dateTimePickerPayOrderDate.Size = new System.Drawing.Size(135, 26);
            this.dateTimePickerPayOrderDate.TabIndex = 100;
            // 
            // labelExchangeRate
            // 
            this.labelExchangeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExchangeRate.AutoSize = true;
            this.labelExchangeRate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExchangeRate.Location = new System.Drawing.Point(161, 235);
            this.labelExchangeRate.Name = "labelExchangeRate";
            this.labelExchangeRate.Size = new System.Drawing.Size(82, 16);
            this.labelExchangeRate.TabIndex = 99;
            this.labelExchangeRate.Text = "سعر الصرف";
            // 
            // textBoxPayOrderExchangeRate
            // 
            this.textBoxPayOrderExchangeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayOrderExchangeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPayOrderExchangeRate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPayOrderExchangeRate.Location = new System.Drawing.Point(41, 231);
            this.textBoxPayOrderExchangeRate.Name = "textBoxPayOrderExchangeRate";
            this.textBoxPayOrderExchangeRate.Size = new System.Drawing.Size(105, 26);
            this.textBoxPayOrderExchangeRate.TabIndex = 98;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(493, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 16);
            this.label7.TabIndex = 97;
            this.label7.Text = "العملة";
            // 
            // comboBoxPayOrderCurrency
            // 
            this.comboBoxPayOrderCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPayOrderCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPayOrderCurrency.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxPayOrderCurrency.FormattingEnabled = true;
            this.comboBoxPayOrderCurrency.Location = new System.Drawing.Point(272, 230);
            this.comboBoxPayOrderCurrency.Name = "comboBoxPayOrderCurrency";
            this.comboBoxPayOrderCurrency.Size = new System.Drawing.Size(204, 26);
            this.comboBoxPayOrderCurrency.TabIndex = 96;
            this.comboBoxPayOrderCurrency.SelectedIndexChanged += new System.EventHandler(this.comboBoxPayOrderCurrency_SelectedIndexChanged);
            // 
            // textBoxPayOrderValue
            // 
            this.textBoxPayOrderValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayOrderValue.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxPayOrderValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPayOrderValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPayOrderValue.Location = new System.Drawing.Point(372, 183);
            this.textBoxPayOrderValue.Name = "textBoxPayOrderValue";
            this.textBoxPayOrderValue.Size = new System.Drawing.Size(104, 26);
            this.textBoxPayOrderValue.TabIndex = 90;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(490, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 89;
            this.label8.Text = "القيمة";
            // 
            // textBoxPayOrderDesc
            // 
            this.textBoxPayOrderDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayOrderDesc.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxPayOrderDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPayOrderDesc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPayOrderDesc.Location = new System.Drawing.Point(210, 138);
            this.textBoxPayOrderDesc.Name = "textBoxPayOrderDesc";
            this.textBoxPayOrderDesc.Size = new System.Drawing.Size(266, 26);
            this.textBoxPayOrderDesc.TabIndex = 80;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(493, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 79;
            this.label1.Text = "الوصف";
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(478, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 73;
            this.label2.Text = "رقم الأمر";
            // 
            // textBoxPayOrderID
            // 
            this.textBoxPayOrderID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayOrderID.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxPayOrderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPayOrderID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxPayOrderID.Location = new System.Drawing.Point(343, 55);
            this.textBoxPayOrderID.Name = "textBoxPayOrderID";
            this.textBoxPayOrderID.ReadOnly = true;
            this.textBoxPayOrderID.Size = new System.Drawing.Size(129, 26);
            this.textBoxPayOrderID.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(490, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
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
            this.label22.Size = new System.Drawing.Size(581, 37);
            this.label22.TabIndex = 31;
            this.label22.Text = "أمر صرف";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EmployeePayOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 377);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Name = "EmployeePayOrderForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "امر صرف";
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
        private System.Windows.Forms.TextBox textBoxPayOrderValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxPayOrderDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textboxEmployeeID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPayOrderID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label labelExchangeRate;
        private System.Windows.Forms.TextBox textBoxPayOrderExchangeRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxPayOrderCurrency;
        private System.Windows.Forms.DateTimePicker dateTimePickerPayOrderDate;
    }
}