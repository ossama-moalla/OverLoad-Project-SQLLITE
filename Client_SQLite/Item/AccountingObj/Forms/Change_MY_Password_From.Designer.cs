namespace OverLoad_Client.AccountingObj.Forms
{
    partial class Change_MY_Password_From
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
            this.textBox_Old_Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_New_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Confirm_Password = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Old_Password
            // 
            this.textBox_Old_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Old_Password.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Old_Password.Location = new System.Drawing.Point(27, 37);
            this.textBox_Old_Password.Name = "textBox_Old_Password";
            this.textBox_Old_Password.Size = new System.Drawing.Size(285, 27);
            this.textBox_Old_Password.TabIndex = 0;
            this.textBox_Old_Password.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "كلمة المرور القديمة";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "كلمة المرور الجديدة";
            // 
            // textBox_New_Password
            // 
            this.textBox_New_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_New_Password.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_New_Password.Location = new System.Drawing.Point(27, 91);
            this.textBox_New_Password.Name = "textBox_New_Password";
            this.textBox_New_Password.Size = new System.Drawing.Size(285, 27);
            this.textBox_New_Password.TabIndex = 2;
            this.textBox_New_Password.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "تأكيد كلمة المرور ";
            // 
            // textBox_Confirm_Password
            // 
            this.textBox_Confirm_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Confirm_Password.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Confirm_Password.Location = new System.Drawing.Point(27, 145);
            this.textBox_Confirm_Password.Name = "textBox_Confirm_Password";
            this.textBox_Confirm_Password.Size = new System.Drawing.Size(285, 27);
            this.textBox_Confirm_Password.TabIndex = 4;
            this.textBox_Confirm_Password.UseSystemPasswordChar = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(27, 190);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(118, 31);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "تغيير";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(189, 190);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(123, 31);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "الغاء";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // Change_MY_Password_From
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 233);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Confirm_Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_New_Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Old_Password);
            this.Name = "Change_MY_Password_From";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Old_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_New_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Confirm_Password;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}