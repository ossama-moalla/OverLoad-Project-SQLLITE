namespace OverLoad_Client.Trade.Forms.TradeForms
{
    partial class RavageOPR_Form
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
            this.panelSellOPRs = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.listViewItemOUT = new System.Windows.Forms.ListView();
            this.columnNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItemCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItemFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnItemstate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConsumeUnit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_Part_hint = new System.Windows.Forms.Label();
            this.panelPart = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPartPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPartName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPartID = new System.Windows.Forms.TextBox();
            this.labelExchangeRate = new System.Windows.Forms.Label();
            this.textboxOPRID = new System.Windows.Forms.TextBox();
            this.dateTimePicker_ = new System.Windows.Forms.DateTimePicker();
            this.labelOprINFO = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextboxNotes = new System.Windows.Forms.TextBox();
            this.panelSellOPRs.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelPart.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSellOPRs
            // 
            this.panelSellOPRs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSellOPRs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSellOPRs.Controls.Add(this.label15);
            this.panelSellOPRs.Controls.Add(this.listViewItemOUT);
            this.panelSellOPRs.Location = new System.Drawing.Point(12, 166);
            this.panelSellOPRs.Name = "panelSellOPRs";
            this.panelSellOPRs.Size = new System.Drawing.Size(1219, 407);
            this.panelSellOPRs.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.LightGreen;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1211, 28);
            this.label15.TabIndex = 12;
            this.label15.Text = "العناصر المدرجة";
            // 
            // listViewItemOUT
            // 
            this.listViewItemOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewItemOUT.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNo,
            this.columnItem,
            this.columnItemCompany,
            this.columnItemFolder,
            this.columnItemstate,
            this.columnAmount,
            this.columnConsumeUnit});
            this.listViewItemOUT.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.listViewItemOUT.FullRowSelect = true;
            this.listViewItemOUT.GridLines = true;
            this.listViewItemOUT.Location = new System.Drawing.Point(3, 31);
            this.listViewItemOUT.Name = "listViewItemOUT";
            this.listViewItemOUT.RightToLeftLayout = true;
            this.listViewItemOUT.Size = new System.Drawing.Size(1211, 371);
            this.listViewItemOUT.TabIndex = 11;
            this.listViewItemOUT.UseCompatibleStateImageBehavior = false;
            this.listViewItemOUT.View = System.Windows.Forms.View.Details;
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
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.Location = new System.Drawing.Point(658, 588);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(114, 37);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "اغلاق";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.buttonSave.Location = new System.Drawing.Point(492, 588);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(114, 37);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "حفظ";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label_Part_hint);
            this.panel2.Controls.Add(this.panelPart);
            this.panel2.Controls.Add(this.labelExchangeRate);
            this.panel2.Controls.Add(this.textboxOPRID);
            this.panel2.Controls.Add(this.dateTimePicker_);
            this.panel2.Controls.Add(this.labelOprINFO);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.TextboxNotes);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1219, 148);
            this.panel2.TabIndex = 14;
            // 
            // label_Part_hint
            // 
            this.label_Part_hint.AutoSize = true;
            this.label_Part_hint.Location = new System.Drawing.Point(419, 71);
            this.label_Part_hint.Name = "label_Part_hint";
            this.label_Part_hint.Size = new System.Drawing.Size(295, 13);
            this.label_Part_hint.TabIndex = 20;
            this.label_Part_hint.Text = "ادخل رقم القسم ثم اضغط enter  او نقرتين لاستعراض الاقسام";
            // 
            // panelPart
            // 
            this.panelPart.Controls.Add(this.label6);
            this.panelPart.Controls.Add(this.textBoxPartPath);
            this.panelPart.Controls.Add(this.label2);
            this.panelPart.Controls.Add(this.textBoxPartName);
            this.panelPart.Controls.Add(this.label1);
            this.panelPart.Controls.Add(this.textBoxPartID);
            this.panelPart.Location = new System.Drawing.Point(57, 87);
            this.panelPart.Name = "panelPart";
            this.panelPart.Size = new System.Drawing.Size(654, 53);
            this.panelPart.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(225, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "التسلسل الهرمي";
            // 
            // textBoxPartPath
            // 
            this.textBoxPartPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartPath.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPartPath.Location = new System.Drawing.Point(3, 24);
            this.textBoxPartPath.Name = "textBoxPartPath";
            this.textBoxPartPath.ReadOnly = true;
            this.textBoxPartPath.Size = new System.Drawing.Size(336, 26);
            this.textBoxPartPath.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(481, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "اسم القسم";
            // 
            // textBoxPartName
            // 
            this.textBoxPartName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPartName.Location = new System.Drawing.Point(358, 23);
            this.textBoxPartName.Name = "textBoxPartName";
            this.textBoxPartName.ReadOnly = true;
            this.textBoxPartName.Size = new System.Drawing.Size(200, 26);
            this.textBoxPartName.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(569, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "رقم القسم";
            // 
            // textBoxPartID
            // 
            this.textBoxPartID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPartID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPartID.Location = new System.Drawing.Point(574, 23);
            this.textBoxPartID.Name = "textBoxPartID";
            this.textBoxPartID.Size = new System.Drawing.Size(67, 26);
            this.textBoxPartID.TabIndex = 19;
            // 
            // labelExchangeRate
            // 
            this.labelExchangeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExchangeRate.AutoSize = true;
            this.labelExchangeRate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExchangeRate.Location = new System.Drawing.Point(1119, 41);
            this.labelExchangeRate.Name = "labelExchangeRate";
            this.labelExchangeRate.Size = new System.Drawing.Size(79, 16);
            this.labelExchangeRate.TabIndex = 18;
            this.labelExchangeRate.Text = "رقم العملية";
            // 
            // textboxOPRID
            // 
            this.textboxOPRID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxOPRID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxOPRID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxOPRID.Location = new System.Drawing.Point(1082, 64);
            this.textboxOPRID.Name = "textboxOPRID";
            this.textboxOPRID.ReadOnly = true;
            this.textboxOPRID.Size = new System.Drawing.Size(113, 26);
            this.textboxOPRID.TabIndex = 16;
            // 
            // dateTimePicker_
            // 
            this.dateTimePicker_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker_.CalendarFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_.CustomFormat = " yyyy/MM/dd      mm:hh";
            this.dateTimePicker_.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePicker_.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_.Location = new System.Drawing.Point(855, 64);
            this.dateTimePicker_.Name = "dateTimePicker_";
            this.dateTimePicker_.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePicker_.Size = new System.Drawing.Size(209, 26);
            this.dateTimePicker_.TabIndex = 15;
            // 
            // labelOprINFO
            // 
            this.labelOprINFO.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.labelOprINFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOprINFO.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelOprINFO.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOprINFO.Location = new System.Drawing.Point(0, 0);
            this.labelOprINFO.Name = "labelOprINFO";
            this.labelOprINFO.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelOprINFO.Size = new System.Drawing.Size(1217, 36);
            this.labelOprINFO.TabIndex = 10;
            this.labelOprINFO.Text = "عملية اتلاف او ترحيل مستهلكات";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1010, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "التاريخ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1136, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "ملاحظات";
            // 
            // TextboxNotes
            // 
            this.TextboxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextboxNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextboxNotes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxNotes.Location = new System.Drawing.Point(855, 114);
            this.TextboxNotes.Name = "TextboxNotes";
            this.TextboxNotes.Size = new System.Drawing.Size(340, 26);
            this.TextboxNotes.TabIndex = 2;
            // 
            // RavageOPR_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1243, 637);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelSellOPRs);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Name = "RavageOPR_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelSellOPRs.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelPart.ResumeLayout(false);
            this.panelPart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSellOPRs;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelExchangeRate;
        private System.Windows.Forms.TextBox textboxOPRID;
        private System.Windows.Forms.DateTimePicker dateTimePicker_;
        private System.Windows.Forms.Label labelOprINFO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextboxNotes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListView listViewItemOUT;
        private System.Windows.Forms.ColumnHeader columnNo;
        private System.Windows.Forms.ColumnHeader columnItem;
        private System.Windows.Forms.ColumnHeader columnItemCompany;
        private System.Windows.Forms.ColumnHeader columnItemFolder;
        private System.Windows.Forms.ColumnHeader columnItemstate;
        private System.Windows.Forms.ColumnHeader columnAmount;
        private System.Windows.Forms.ColumnHeader columnConsumeUnit;
        private System.Windows.Forms.Panel panelPart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPartPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPartName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPartID;
        private System.Windows.Forms.Label label_Part_hint;
    }
}