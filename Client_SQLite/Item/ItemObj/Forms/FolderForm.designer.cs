namespace OverLoad_Client.ItemObj.Forms
{
    partial class FolderForm
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
            this.panelInfo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxdefault_Consume_Unit = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.TextBoxFolderName = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelSpec = new System.Windows.Forms.Panel();
            this.panelSpecAdd = new System.Windows.Forms.Panel();
            this.textBoxIndex = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Button_Spec_Cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSpecType = new System.Windows.Forms.ComboBox();
            this.Button_Spec_Add = new System.Windows.Forms.Button();
            this.textBoxSpecName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewSpecs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelInfo.SuspendLayout();
            this.panelSpec.SuspendLayout();
            this.panelSpecAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.BackColor = System.Drawing.SystemColors.Control;
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.label6);
            this.panelInfo.Controls.Add(this.label1);
            this.panelInfo.Controls.Add(this.textBoxdefault_Consume_Unit);
            this.panelInfo.Controls.Add(this.label);
            this.panelInfo.Controls.Add(this.TextBoxFolderName);
            this.panelInfo.Location = new System.Drawing.Point(12, 12);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(801, 89);
            this.panelInfo.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Aquamarine;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(-1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(800, 32);
            this.label6.TabIndex = 16;
            this.label6.Text = "بيانات الصنف";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(257, 46);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(135, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "وحدة التوزيع الافتراضية";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxdefault_Consume_Unit
            // 
            this.textBoxdefault_Consume_Unit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxdefault_Consume_Unit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxdefault_Consume_Unit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxdefault_Consume_Unit.Location = new System.Drawing.Point(24, 46);
            this.textBoxdefault_Consume_Unit.Name = "textBoxdefault_Consume_Unit";
            this.textBoxdefault_Consume_Unit.Size = new System.Drawing.Size(227, 26);
            this.textBoxdefault_Consume_Unit.TabIndex = 1;
            this.textBoxdefault_Consume_Unit.Text = "قطعة";
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(720, 48);
            this.label.Name = "label";
            this.label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label.Size = new System.Drawing.Size(69, 19);
            this.label.TabIndex = 13;
            this.label.Text = "اسم الصنف";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextBoxFolderName
            // 
            this.TextBoxFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFolderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxFolderName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxFolderName.Location = new System.Drawing.Point(398, 46);
            this.TextBoxFolderName.Name = "TextBoxFolderName";
            this.TextBoxFolderName.Size = new System.Drawing.Size(316, 26);
            this.TextBoxFolderName.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(428, 440);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(78, 37);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "الغاء";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(288, 440);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(78, 37);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "موافق";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panelSpec
            // 
            this.panelSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpec.Controls.Add(this.panelSpecAdd);
            this.panelSpec.Controls.Add(this.label2);
            this.panelSpec.Controls.Add(this.listViewSpecs);
            this.panelSpec.Location = new System.Drawing.Point(12, 124);
            this.panelSpec.Name = "panelSpec";
            this.panelSpec.Size = new System.Drawing.Size(801, 294);
            this.panelSpec.TabIndex = 4;
            // 
            // panelSpecAdd
            // 
            this.panelSpecAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSpecAdd.Controls.Add(this.textBoxIndex);
            this.panelSpecAdd.Controls.Add(this.label4);
            this.panelSpecAdd.Controls.Add(this.Button_Spec_Cancel);
            this.panelSpecAdd.Controls.Add(this.label3);
            this.panelSpecAdd.Controls.Add(this.comboBoxSpecType);
            this.panelSpecAdd.Controls.Add(this.Button_Spec_Add);
            this.panelSpecAdd.Controls.Add(this.textBoxSpecName);
            this.panelSpecAdd.Controls.Add(this.label5);
            this.panelSpecAdd.Location = new System.Drawing.Point(566, 43);
            this.panelSpecAdd.Name = "panelSpecAdd";
            this.panelSpecAdd.Size = new System.Drawing.Size(224, 245);
            this.panelSpecAdd.TabIndex = 8;
            // 
            // textBoxIndex
            // 
            this.textBoxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxIndex.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIndex.Location = new System.Drawing.Point(11, 151);
            this.textBoxIndex.Name = "textBoxIndex";
            this.textBoxIndex.Size = new System.Drawing.Size(199, 23);
            this.textBoxIndex.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(162, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 19);
            this.label4.TabIndex = 18;
            this.label4.Text = "الفهرس";
            // 
            // Button_Spec_Cancel
            // 
            this.Button_Spec_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Spec_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Spec_Cancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Spec_Cancel.Location = new System.Drawing.Point(36, 180);
            this.Button_Spec_Cancel.Name = "Button_Spec_Cancel";
            this.Button_Spec_Cancel.Size = new System.Drawing.Size(66, 38);
            this.Button_Spec_Cancel.TabIndex = 15;
            this.Button_Spec_Cancel.Text = "الغاء";
            this.Button_Spec_Cancel.UseVisualStyleBackColor = true;
            this.Button_Spec_Cancel.Visible = false;
            this.Button_Spec_Cancel.Click += new System.EventHandler(this.Button_Spec_Cancel_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "نوع الخاصية";
            // 
            // comboBoxSpecType
            // 
            this.comboBoxSpecType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpecType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSpecType.FormattingEnabled = true;
            this.comboBoxSpecType.Items.AddRange(new object[] {
            "قيم الخاصية نصية",
            "قيم الخاصية مقيدة بخيارات محددة"});
            this.comboBoxSpecType.Location = new System.Drawing.Point(11, 104);
            this.comboBoxSpecType.Name = "comboBoxSpecType";
            this.comboBoxSpecType.Size = new System.Drawing.Size(199, 22);
            this.comboBoxSpecType.TabIndex = 16;
            // 
            // Button_Spec_Add
            // 
            this.Button_Spec_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Spec_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Spec_Add.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Spec_Add.Location = new System.Drawing.Point(138, 180);
            this.Button_Spec_Add.Name = "Button_Spec_Add";
            this.Button_Spec_Add.Size = new System.Drawing.Size(53, 38);
            this.Button_Spec_Add.TabIndex = 14;
            this.Button_Spec_Add.Text = "اضف";
            this.Button_Spec_Add.UseVisualStyleBackColor = true;
            this.Button_Spec_Add.Click += new System.EventHandler(this.ADD_Click);
            // 
            // textBoxSpecName
            // 
            this.textBoxSpecName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSpecName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSpecName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSpecName.Location = new System.Drawing.Point(11, 49);
            this.textBoxSpecName.Name = "textBoxSpecName";
            this.textBoxSpecName.Size = new System.Drawing.Size(199, 23);
            this.textBoxSpecName.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(138, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "اسم الخاصية";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Aquamarine;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(793, 32);
            this.label2.TabIndex = 0;
            this.label2.Text = "الخصائص المشتركة لعناصر هذا الصنف";
            // 
            // listViewSpecs
            // 
            this.listViewSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSpecs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewSpecs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSpecs.FullRowSelect = true;
            this.listViewSpecs.GridLines = true;
            this.listViewSpecs.Location = new System.Drawing.Point(3, 49);
            this.listViewSpecs.Name = "listViewSpecs";
            this.listViewSpecs.RightToLeftLayout = true;
            this.listViewSpecs.Size = new System.Drawing.Size(546, 240);
            this.listViewSpecs.TabIndex = 7;
            this.listViewSpecs.UseCompatibleStateImageBehavior = false;
            this.listViewSpecs.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "الخاصية";
            this.columnHeader1.Width = 260;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = " نوع الخاصية";
            this.columnHeader2.Width = 113;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "فهرس الخاصية";
            this.columnHeader3.Width = 127;
            // 
            // FolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 489);
            this.Controls.Add(this.panelSpec);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Name = "FolderForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "بيانات صنف";
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelSpec.ResumeLayout(false);
            this.panelSpecAdd.ResumeLayout(false);
            this.panelSpecAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelInfo;
        public System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox TextBoxFolderName;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxdefault_Consume_Unit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelSpec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewSpecs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panelSpecAdd;
        private System.Windows.Forms.TextBox textBoxIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Button_Spec_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxSpecType;
        private System.Windows.Forms.Button Button_Spec_Add;
        private System.Windows.Forms.TextBox textBoxSpecName;
        private System.Windows.Forms.Label label5;
    }
}