namespace OverLoad_Client.ItemObj.Forms
{
    partial class ConsumeUnitAddForm
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
            this.Close = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxFactor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxConumeUnitName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Close);
            this.panel1.Controls.Add(this.Add);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TextBoxFactor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TextBoxConumeUnitName);
            this.panel1.Location = new System.Drawing.Point(18, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 239);
            this.panel1.TabIndex = 0;
            // 
            // Close
            // 
            this.Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(52, 177);
            this.Close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(104, 42);
            this.Close.TabIndex = 13;
            this.Close.Text = "اغلاق";
            this.Close.UseVisualStyleBackColor = true;
            // 
            // Add
            // 
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.Location = new System.Drawing.Point(192, 177);
            this.Add.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(110, 42);
            this.Add.TabIndex = 10;
            this.Add.Text = "أضف";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(192, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 50);
            this.label2.TabIndex = 11;
            this.label2.Text = "معامل الضرب بوحدة التوزيع الافتراضية";
            // 
            // TextBoxFactor
            // 
            this.TextBoxFactor.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxFactor.Location = new System.Drawing.Point(22, 101);
            this.TextBoxFactor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TextBoxFactor.Name = "TextBoxFactor";
            this.TextBoxFactor.Size = new System.Drawing.Size(158, 26);
            this.TextBoxFactor.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(216, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "اسم وحدة التوزيع";
            // 
            // TextBoxConumeUnitName
            // 
            this.TextBoxConumeUnitName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxConumeUnitName.Location = new System.Drawing.Point(50, 44);
            this.TextBoxConumeUnitName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TextBoxConumeUnitName.Name = "TextBoxConumeUnitName";
            this.TextBoxConumeUnitName.Size = new System.Drawing.Size(158, 26);
            this.TextBoxConumeUnitName.TabIndex = 7;
            // 
            // ConsumeUnitAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(400, 275);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ConsumeUnitAddForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "وحدات التوزيع";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button Close;
        internal System.Windows.Forms.Button Add;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox TextBoxFactor;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox TextBoxConumeUnitName;
    }
}