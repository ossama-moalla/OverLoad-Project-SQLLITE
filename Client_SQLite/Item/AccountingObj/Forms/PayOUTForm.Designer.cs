using System;
using OverLoad_Client.Company.Objects;

namespace OverLoad_Client.AccountingObj.Forms
{
    partial class PayOUTForm
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
            this.panelPayOUTfo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker_ = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPayDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPayExchangeRate = new System.Windows.Forms.TextBox();
            this.TextboxNotes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPayValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
            this.labelBillID = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelOwner_INFO = new System.Windows.Forms.Panel();
            this.labelOwner = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxBillOUT_ExchangeRate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxRemain = new System.Windows.Forms.TextBox();
            this.textBoxBillOUTDate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxBillOUTValue = new System.Windows.Forms.TextBox();
            this.textBoxPaid = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxCurrency = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxContact = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.textBoxBillOUT_ID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_MoneyBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelPayOUTfo.SuspendLayout();
            this.panelOwner_INFO.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPayOUTfo
            // 
            this.panelPayOUTfo.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panelPayOUTfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPayOUTfo.Controls.Add(this.label6);
            this.panelPayOUTfo.Controls.Add(this.label3);
            this.panelPayOUTfo.Controls.Add(this.dateTimePicker_);
            this.panelPayOUTfo.Controls.Add(this.label1);
            this.panelPayOUTfo.Controls.Add(this.textBoxPayDesc);
            this.panelPayOUTfo.Controls.Add(this.label4);
            this.panelPayOUTfo.Controls.Add(this.textBoxPayExchangeRate);
            this.panelPayOUTfo.Controls.Add(this.TextboxNotes);
            this.panelPayOUTfo.Controls.Add(this.label7);
            this.panelPayOUTfo.Controls.Add(this.textBoxPayValue);
            this.panelPayOUTfo.Controls.Add(this.label5);
            this.panelPayOUTfo.Controls.Add(this.comboBoxCurrency);
            this.panelPayOUTfo.Controls.Add(this.labelBillID);
            this.panelPayOUTfo.Location = new System.Drawing.Point(12, 59);
            this.panelPayOUTfo.Name = "panelPayOUTfo";
            this.panelPayOUTfo.Size = new System.Drawing.Size(366, 364);
            this.panelPayOUTfo.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(291, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "الوصف";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(298, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "التاريخ";
            // 
            // dateTimePicker_
            // 
            this.dateTimePicker_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker_.CalendarFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_.CustomFormat = " yyyy/MM/dd      mm:hh";
            this.dateTimePicker_.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.dateTimePicker_.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_.Location = new System.Drawing.Point(82, 65);
            this.dateTimePicker_.Name = "dateTimePicker_";
            this.dateTimePicker_.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePicker_.Size = new System.Drawing.Size(255, 26);
            this.dateTimePicker_.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(261, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "سعر الصرف";
            // 
            // textBoxPayDesc
            // 
            this.textBoxPayDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPayDesc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPayDesc.Location = new System.Drawing.Point(82, 113);
            this.textBoxPayDesc.Name = "textBoxPayDesc";
            this.textBoxPayDesc.Size = new System.Drawing.Size(255, 26);
            this.textBoxPayDesc.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(283, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "ملاحظات";
            // 
            // textBoxPayExchangeRate
            // 
            this.textBoxPayExchangeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayExchangeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPayExchangeRate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPayExchangeRate.Location = new System.Drawing.Point(82, 211);
            this.textBoxPayExchangeRate.Name = "textBoxPayExchangeRate";
            this.textBoxPayExchangeRate.Size = new System.Drawing.Size(255, 26);
            this.textBoxPayExchangeRate.TabIndex = 16;
            // 
            // TextboxNotes
            // 
            this.TextboxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextboxNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextboxNotes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxNotes.Location = new System.Drawing.Point(82, 317);
            this.TextboxNotes.Name = "TextboxNotes";
            this.TextboxNotes.Size = new System.Drawing.Size(260, 26);
            this.TextboxNotes.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(264, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "قيمة الدفعة";
            // 
            // textBoxPayValue
            // 
            this.textBoxPayValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPayValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPayValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPayValue.Location = new System.Drawing.Point(82, 259);
            this.textBoxPayValue.Multiline = true;
            this.textBoxPayValue.Name = "textBoxPayValue";
            this.textBoxPayValue.Size = new System.Drawing.Size(255, 31);
            this.textBoxPayValue.TabIndex = 6;
            this.textBoxPayValue.Text = "0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(296, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "العملة";
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrency.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.Location = new System.Drawing.Point(82, 161);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(255, 26);
            this.comboBoxCurrency.TabIndex = 11;
            this.comboBoxCurrency.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrency_SelectedIndexChanged);
            // 
            // labelBillID
            // 
            this.labelBillID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBillID.BackColor = System.Drawing.Color.Aquamarine;
            this.labelBillID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBillID.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBillID.Location = new System.Drawing.Point(-1, 0);
            this.labelBillID.Name = "labelBillID";
            this.labelBillID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelBillID.Size = new System.Drawing.Size(366, 30);
            this.labelBillID.TabIndex = 10;
            this.labelBillID.Text = "دفعة خارجة من الصندوق";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.Location = new System.Drawing.Point(397, 429);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(138, 37);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "اغلاق";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.buttonSave.Location = new System.Drawing.Point(244, 429);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(134, 37);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "انشاء";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panelOwner_INFO
            // 
            this.panelOwner_INFO.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panelOwner_INFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOwner_INFO.Controls.Add(this.labelOwner);
            this.panelOwner_INFO.Controls.Add(this.label14);
            this.panelOwner_INFO.Controls.Add(this.textBoxBillOUT_ExchangeRate);
            this.panelOwner_INFO.Controls.Add(this.label10);
            this.panelOwner_INFO.Controls.Add(this.label13);
            this.panelOwner_INFO.Controls.Add(this.textBoxRemain);
            this.panelOwner_INFO.Controls.Add(this.textBoxBillOUTDate);
            this.panelOwner_INFO.Controls.Add(this.label11);
            this.panelOwner_INFO.Controls.Add(this.textBoxBillOUTValue);
            this.panelOwner_INFO.Controls.Add(this.textBoxPaid);
            this.panelOwner_INFO.Controls.Add(this.label9);
            this.panelOwner_INFO.Controls.Add(this.textBoxCurrency);
            this.panelOwner_INFO.Controls.Add(this.label12);
            this.panelOwner_INFO.Controls.Add(this.label8);
            this.panelOwner_INFO.Controls.Add(this.textBoxContact);
            this.panelOwner_INFO.Controls.Add(this.labelID);
            this.panelOwner_INFO.Controls.Add(this.textBoxBillOUT_ID);
            this.panelOwner_INFO.Location = new System.Drawing.Point(394, 59);
            this.panelOwner_INFO.Name = "panelOwner_INFO";
            this.panelOwner_INFO.Size = new System.Drawing.Size(390, 364);
            this.panelOwner_INFO.TabIndex = 14;
            // 
            // labelOwner
            // 
            this.labelOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOwner.BackColor = System.Drawing.Color.Aquamarine;
            this.labelOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOwner.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOwner.Location = new System.Drawing.Point(-1, -1);
            this.labelOwner.Name = "labelOwner";
            this.labelOwner.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelOwner.Size = new System.Drawing.Size(387, 31);
            this.labelOwner.TabIndex = 28;
            this.labelOwner.Text = "عائدة لفاتورة  ";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(216, 241);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(153, 16);
            this.label14.TabIndex = 24;
            this.label14.Text = "سعر الصرف عند الانشاء";
            // 
            // textBoxBillOUT_ExchangeRate
            // 
            this.textBoxBillOUT_ExchangeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBillOUT_ExchangeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBillOUT_ExchangeRate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBillOUT_ExchangeRate.Location = new System.Drawing.Point(177, 260);
            this.textBoxBillOUT_ExchangeRate.Name = "textBoxBillOUT_ExchangeRate";
            this.textBoxBillOUT_ExchangeRate.ReadOnly = true;
            this.textBoxBillOUT_ExchangeRate.Size = new System.Drawing.Size(189, 26);
            this.textBoxBillOUT_ExchangeRate.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(94, 302);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 16);
            this.label10.TabIndex = 20;
            this.label10.Text = "المتبقي";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(327, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 16);
            this.label13.TabIndex = 22;
            this.label13.Text = "التاريخ";
            // 
            // textBoxRemain
            // 
            this.textBoxRemain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRemain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRemain.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRemain.Location = new System.Drawing.Point(40, 321);
            this.textBoxRemain.Name = "textBoxRemain";
            this.textBoxRemain.ReadOnly = true;
            this.textBoxRemain.Size = new System.Drawing.Size(108, 26);
            this.textBoxRemain.TabIndex = 19;
            // 
            // textBoxBillOUTDate
            // 
            this.textBoxBillOUTDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBillOUTDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBillOUTDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBillOUTDate.Location = new System.Drawing.Point(123, 107);
            this.textBoxBillOUTDate.Name = "textBoxBillOUTDate";
            this.textBoxBillOUTDate.ReadOnly = true;
            this.textBoxBillOUTDate.Size = new System.Drawing.Size(245, 26);
            this.textBoxBillOUTDate.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(204, 302);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 16);
            this.label11.TabIndex = 18;
            this.label11.Text = "المدفوع";
            // 
            // textBoxBillOUTValue
            // 
            this.textBoxBillOUTValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBillOUTValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBillOUTValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBillOUTValue.Location = new System.Drawing.Point(263, 321);
            this.textBoxBillOUTValue.Name = "textBoxBillOUTValue";
            this.textBoxBillOUTValue.ReadOnly = true;
            this.textBoxBillOUTValue.Size = new System.Drawing.Size(106, 26);
            this.textBoxBillOUTValue.TabIndex = 15;
            // 
            // textBoxPaid
            // 
            this.textBoxPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPaid.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPaid.Location = new System.Drawing.Point(154, 321);
            this.textBoxPaid.Name = "textBoxPaid";
            this.textBoxPaid.ReadOnly = true;
            this.textBoxPaid.Size = new System.Drawing.Size(103, 26);
            this.textBoxPaid.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(325, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "العملة";
            // 
            // textBoxCurrency
            // 
            this.textBoxCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrency.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCurrency.Location = new System.Drawing.Point(177, 204);
            this.textBoxCurrency.Name = "textBoxCurrency";
            this.textBoxCurrency.ReadOnly = true;
            this.textBoxCurrency.Size = new System.Drawing.Size(191, 26);
            this.textBoxCurrency.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(323, 302);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 16);
            this.label12.TabIndex = 16;
            this.label12.Text = "القيمة";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(328, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "الجهة";
            // 
            // textBoxContact
            // 
            this.textBoxContact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContact.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxContact.Location = new System.Drawing.Point(123, 155);
            this.textBoxContact.Name = "textBoxContact";
            this.textBoxContact.ReadOnly = true;
            this.textBoxContact.Size = new System.Drawing.Size(245, 26);
            this.textBoxContact.TabIndex = 11;
            // 
            // labelID
            // 
            this.labelID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelID.AutoSize = true;
            this.labelID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(329, 42);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(40, 16);
            this.labelID.TabIndex = 3;
            this.labelID.Text = "الرقم";
            // 
            // textBoxBillOUT_ID
            // 
            this.textBoxBillOUT_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBillOUT_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBillOUT_ID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBillOUT_ID.Location = new System.Drawing.Point(228, 61);
            this.textBoxBillOUT_ID.Name = "textBoxBillOUT_ID";
            this.textBoxBillOUT_ID.ReadOnly = true;
            this.textBoxBillOUT_ID.Size = new System.Drawing.Size(140, 26);
            this.textBoxBillOUT_ID.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(689, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 19);
            this.label2.TabIndex = 18;
            this.label2.Text = "الصندوق";
            // 
            // comboBox_MoneyBox
            // 
            this.comboBox_MoneyBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_MoneyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MoneyBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_MoneyBox.FormattingEnabled = true;
            this.comboBox_MoneyBox.Location = new System.Drawing.Point(428, 7);
            this.comboBox_MoneyBox.Name = "comboBox_MoneyBox";
            this.comboBox_MoneyBox.Size = new System.Drawing.Size(255, 27);
            this.comboBox_MoneyBox.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBox_MoneyBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 44);
            this.panel1.TabIndex = 19;
            // 
            // PayOUTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(796, 478);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelOwner_INFO);
            this.Controls.Add(this.panelPayOUTfo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(812, 487);
            this.Name = "PayOUTForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "دفعة صادرة";
            this.Load += new System.EventHandler(this.PayOUTForm_Load);
            this.panelPayOUTfo.ResumeLayout(false);
            this.panelPayOUTfo.PerformLayout();
            this.panelOwner_INFO.ResumeLayout(false);
            this.panelOwner_INFO.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelPayOUTfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPayValue;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panelOwner_INFO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPayExchangeRate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxCurrency;
        private System.Windows.Forms.Label labelBillID;
        private System.Windows.Forms.TextBox textBoxPayDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxBillOUT_ID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextboxNotes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRemain;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxPaid;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxBillOUTValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxCurrency;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxContact;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxBillOUTDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxBillOUT_ExchangeRate;
        private System.Windows.Forms.Label labelOwner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_MoneyBox;
        private System.Windows.Forms.Panel panel1;
    }
}