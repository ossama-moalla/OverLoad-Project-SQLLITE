namespace OverLoad_Client.Trade.Forms.TradeForms
{
    partial class ItemIN_ItemOutListForm
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
            this.listView = new System.Windows.Forms.ListView();
            this.columnNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxItemCompany = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxItemType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxItemINID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOperatonID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextboxOperationType = new System.Windows.Forms.TextBox();
            this.labelItemININFO = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.panelSellOPRs.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSellOPRs
            // 
            this.panelSellOPRs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSellOPRs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSellOPRs.Controls.Add(this.listView);
            this.panelSellOPRs.Location = new System.Drawing.Point(12, 171);
            this.panelSellOPRs.Name = "panelSellOPRs";
            this.panelSellOPRs.Size = new System.Drawing.Size(1147, 329);
            this.panelSellOPRs.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNo,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listView.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(3, 3);
            this.listView.Name = "listView";
            this.listView.RightToLeftLayout = true;
            this.listView.Size = new System.Drawing.Size(1139, 321);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // columnNo
            // 
            this.columnNo.Text = "متسلسل";
            this.columnNo.Width = 88;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "رقم عملية الاخراج";
            this.columnHeader4.Width = 139;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "تابع لـ";
            this.columnHeader5.Width = 152;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "الرقم";
            this.columnHeader6.Width = 78;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "العملة";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "مكان التخزين";
            this.columnHeader7.Width = 125;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "الكمية";
            this.columnHeader8.Width = 77;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "وحدة التوزيع";
            this.columnHeader9.Width = 106;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "المفرد";
            this.columnHeader10.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "المجمل";
            this.columnHeader11.Width = 138;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.Location = new System.Drawing.Point(1044, 591);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(114, 37);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "اغلاق";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBoxItemCompany);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBoxItemName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBoxItemType);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxItemINID);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxOperatonID);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.TextboxOperationType);
            this.panel2.Controls.Add(this.labelItemININFO);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1147, 153);
            this.panel2.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(399, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "الشركة";
            // 
            // textBoxItemCompany
            // 
            this.textBoxItemCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxItemCompany.Location = new System.Drawing.Point(264, 122);
            this.textBoxItemCompany.Name = "textBoxItemCompany";
            this.textBoxItemCompany.ReadOnly = true;
            this.textBoxItemCompany.Size = new System.Drawing.Size(186, 26);
            this.textBoxItemCompany.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(585, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "اسم العنصر";
            // 
            // textBoxItemName
            // 
            this.textBoxItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxItemName.Location = new System.Drawing.Point(477, 122);
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.ReadOnly = true;
            this.textBoxItemName.Size = new System.Drawing.Size(186, 26);
            this.textBoxItemName.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(850, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "الصنف";
            // 
            // textBoxItemType
            // 
            this.textBoxItemType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxItemType.Location = new System.Drawing.Point(709, 122);
            this.textBoxItemType.Name = "textBoxItemType";
            this.textBoxItemType.ReadOnly = true;
            this.textBoxItemType.Size = new System.Drawing.Size(186, 26);
            this.textBoxItemType.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1010, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "رقم عملية الادخال";
            // 
            // textBoxItemINID
            // 
            this.textBoxItemINID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemINID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemINID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxItemINID.Location = new System.Drawing.Point(941, 122);
            this.textBoxItemINID.Name = "textBoxItemINID";
            this.textBoxItemINID.ReadOnly = true;
            this.textBoxItemINID.Size = new System.Drawing.Size(186, 26);
            this.textBoxItemINID.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(767, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "رقم العملية المصدر";
            // 
            // textBoxOperatonID
            // 
            this.textBoxOperatonID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOperatonID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOperatonID.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOperatonID.Location = new System.Drawing.Point(709, 66);
            this.textBoxOperatonID.Name = "textBoxOperatonID";
            this.textBoxOperatonID.ReadOnly = true;
            this.textBoxOperatonID.Size = new System.Drawing.Size(186, 26);
            this.textBoxOperatonID.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1013, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "نوع عملية الادخال";
            // 
            // TextboxOperationType
            // 
            this.TextboxOperationType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextboxOperationType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextboxOperationType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxOperationType.Location = new System.Drawing.Point(941, 66);
            this.TextboxOperationType.Name = "TextboxOperationType";
            this.TextboxOperationType.ReadOnly = true;
            this.TextboxOperationType.Size = new System.Drawing.Size(186, 26);
            this.TextboxOperationType.TabIndex = 11;
            // 
            // labelItemININFO
            // 
            this.labelItemININFO.BackColor = System.Drawing.Color.LightGreen;
            this.labelItemININFO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelItemININFO.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelItemININFO.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemININFO.Location = new System.Drawing.Point(0, 0);
            this.labelItemININFO.Name = "labelItemININFO";
            this.labelItemININFO.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelItemININFO.Size = new System.Drawing.Size(1145, 36);
            this.labelItemININFO.TabIndex = 10;
            this.labelItemININFO.Text = "عمليات الاخراج التابعة لعملية ادخال";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.textBoxValue);
            this.panel3.Location = new System.Drawing.Point(12, 506);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1147, 79);
            this.panel3.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(206, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "اجمالي الكل";
            // 
            // textBoxValue
            // 
            this.textBoxValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxValue.Location = new System.Drawing.Point(14, 30);
            this.textBoxValue.Multiline = true;
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            this.textBoxValue.Size = new System.Drawing.Size(275, 31);
            this.textBoxValue.TabIndex = 6;
            // 
            // ItemIN_ItemOutListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1171, 637);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelSellOPRs);
            this.Controls.Add(this.buttonClose);
            this.Name = "ItemIN_ItemOutListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "عمليات الاخراج";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelSellOPRs.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSellOPRs;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ColumnHeader columnNo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelItemININFO;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxItemCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxItemType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxItemINID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOperatonID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextboxOperationType;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}