namespace OverLoad_Client.ItemObj.Forms
{
    partial class Form_Spec_Option
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_SpecName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Button_Option_Cancel = new System.Windows.Forms.Button();
            this.listViewOptions = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Button_Option_Add = new System.Windows.Forms.Button();
            this.textBoxOptionName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.textBox_SpecName);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.Button_Option_Cancel);
            this.panel3.Controls.Add(this.listViewOptions);
            this.panel3.Controls.Add(this.Button_Option_Add);
            this.panel3.Controls.Add(this.textBoxOptionName);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(559, 272);
            this.panel3.TabIndex = 2;
            // 
            // textBox_SpecName
            // 
            this.textBox_SpecName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SpecName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_SpecName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_SpecName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SpecName.Location = new System.Drawing.Point(345, 40);
            this.textBox_SpecName.Name = "textBox_SpecName";
            this.textBox_SpecName.ReadOnly = true;
            this.textBox_SpecName.Size = new System.Drawing.Size(199, 25);
            this.textBox_SpecName.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(447, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "خيارات الخاصية ";
            // 
            // Button_Option_Cancel
            // 
            this.Button_Option_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Option_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Option_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Option_Cancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Option_Cancel.Location = new System.Drawing.Point(370, 142);
            this.Button_Option_Cancel.Name = "Button_Option_Cancel";
            this.Button_Option_Cancel.Size = new System.Drawing.Size(58, 28);
            this.Button_Option_Cancel.TabIndex = 7;
            this.Button_Option_Cancel.Text = "اغلاق";
            this.Button_Option_Cancel.UseVisualStyleBackColor = true;
            this.Button_Option_Cancel.Click += new System.EventHandler(this.Close_Click);
            // 
            // listViewOptions
            // 
            this.listViewOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewOptions.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewOptions.FullRowSelect = true;
            this.listViewOptions.GridLines = true;
            this.listViewOptions.Location = new System.Drawing.Point(7, 6);
            this.listViewOptions.MultiSelect = false;
            this.listViewOptions.Name = "listViewOptions";
            this.listViewOptions.RightToLeftLayout = true;
            this.listViewOptions.Size = new System.Drawing.Size(284, 261);
            this.listViewOptions.TabIndex = 7;
            this.listViewOptions.UseCompatibleStateImageBehavior = false;
            this.listViewOptions.View = System.Windows.Forms.View.Details;
            this.listViewOptions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewOptions_KeyDown);
            this.listViewOptions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewSpecs_MouseDown);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "الخيارات";
            this.columnHeader3.Width = 271;
            // 
            // Button_Option_Add
            // 
            this.Button_Option_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Option_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Option_Add.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Option_Add.Location = new System.Drawing.Point(447, 142);
            this.Button_Option_Add.Name = "Button_Option_Add";
            this.Button_Option_Add.Size = new System.Drawing.Size(52, 28);
            this.Button_Option_Add.TabIndex = 6;
            this.Button_Option_Add.Text = "اضف";
            this.Button_Option_Add.UseVisualStyleBackColor = true;
            this.Button_Option_Add.Click += new System.EventHandler(this.ADD_Click);
            // 
            // textBoxOptionName
            // 
            this.textBoxOptionName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOptionName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOptionName.Location = new System.Drawing.Point(345, 94);
            this.textBoxOptionName.Name = "textBoxOptionName";
            this.textBoxOptionName.Size = new System.Drawing.Size(199, 25);
            this.textBoxOptionName.TabIndex = 5;
            this.textBoxOptionName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxOptionName_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(478, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "اضافة خيار";
            // 
            // Form_Spec_Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 291);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(590, 329);
            this.Name = "Form_Spec_Option";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "خيارات الخاصية";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_SpecName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Button_Option_Cancel;
        private System.Windows.Forms.ListView listViewOptions;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button Button_Option_Add;
        private System.Windows.Forms.TextBox textBoxOptionName;
        private System.Windows.Forms.Label label6;
    }
}