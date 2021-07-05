namespace OverLoad_Client.Maintenance.Forms
{
    partial class AddDiagnosticFile
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxExtention = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFileDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ADD = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxExtention);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxFileName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxFileDescription);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 191);
            this.panel1.TabIndex = 0;
            // 
            // textBoxExtention
            // 
            this.textBoxExtention.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.textBoxExtention.Location = new System.Drawing.Point(402, 34);
            this.textBoxExtention.Name = "textBoxExtention";
            this.textBoxExtention.ReadOnly = true;
            this.textBoxExtention.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxExtention.Size = new System.Drawing.Size(78, 26);
            this.textBoxExtention.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label1.Location = new System.Drawing.Point(402, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "لاحقة الملف";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.textBoxFileName.Location = new System.Drawing.Point(26, 84);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(454, 26);
            this.textBoxFileName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label3.Location = new System.Drawing.Point(402, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "اسم الملف";
            // 
            // textBoxFileDescription
            // 
            this.textBoxFileDescription.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.textBoxFileDescription.Location = new System.Drawing.Point(26, 146);
            this.textBoxFileDescription.Name = "textBoxFileDescription";
            this.textBoxFileDescription.Size = new System.Drawing.Size(454, 26);
            this.textBoxFileDescription.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label2.Location = new System.Drawing.Point(402, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "وصف الملف";
            // 
            // ADD
            // 
            this.ADD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ADD.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.ADD.Location = new System.Drawing.Point(152, 209);
            this.ADD.Name = "ADD";
            this.ADD.Size = new System.Drawing.Size(88, 33);
            this.ADD.TabIndex = 3;
            this.ADD.Text = "اضف";
            this.ADD.UseVisualStyleBackColor = true;
            this.ADD.Click += new System.EventHandler(this.ADD_Click);
            // 
            // Close
            // 
            this.Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.Close.Location = new System.Drawing.Point(300, 209);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(88, 33);
            this.Close.TabIndex = 4;
            this.Close.Text = "اغلاق";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // AddDiagnosticFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 254);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.ADD);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "AddDiagnosticFile";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "اضافة ملف";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ADD;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFileDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxExtention;
        private System.Windows.Forms.Label label1;
    }
}