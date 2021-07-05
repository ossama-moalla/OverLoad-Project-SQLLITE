namespace OverLoad_Client.ItemObj.Forms
{
    partial class AddItemFile
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
            this.buttonADD = new System.Windows.Forms.Button();
            this.bbuttonClose = new System.Windows.Forms.Button();
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
            // buttonADD
            // 
            this.buttonADD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonADD.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.buttonADD.Location = new System.Drawing.Point(152, 209);
            this.buttonADD.Name = "buttonADD";
            this.buttonADD.Size = new System.Drawing.Size(88, 33);
            this.buttonADD.TabIndex = 3;
            this.buttonADD.Text = "اضف";
            this.buttonADD.UseVisualStyleBackColor = true;
            this.buttonADD.Click += new System.EventHandler(this.ADD_Click);
            // 
            // bbuttonClose
            // 
            this.bbuttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bbuttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bbuttonClose.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.bbuttonClose.Location = new System.Drawing.Point(300, 209);
            this.bbuttonClose.Name = "bbuttonClose";
            this.bbuttonClose.Size = new System.Drawing.Size(88, 33);
            this.bbuttonClose.TabIndex = 4;
            this.bbuttonClose.Text = "اغلاق";
            this.bbuttonClose.UseVisualStyleBackColor = true;
            this.bbuttonClose.Click += new System.EventHandler(this.bbuttonClose_Click);
            // 
            // AddItemFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 254);
            this.Controls.Add(this.bbuttonClose);
            this.Controls.Add(this.buttonADD);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "AddItemFile";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "اضافة ملف";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonADD;
        private System.Windows.Forms.Button bbuttonClose;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFileDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxExtention;
        private System.Windows.Forms.Label label1;
    }
}