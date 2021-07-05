namespace OverLoad_Client
{
    partial class ExecuteSQLCommand_Insert_Serialiaze_Form
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
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.labelreplay = new System.Windows.Forms.Label();
            this.labelsend = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBoxReplayReceive = new System.Windows.Forms.CheckBox();
            this.checkBoxSend = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelSize = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelSize);
            this.panel1.Controls.Add(this.progressBar3);
            this.panel1.Controls.Add(this.labelreplay);
            this.panel1.Controls.Add(this.labelsend);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.checkBoxReplayReceive);
            this.panel1.Controls.Add(this.checkBoxSend);
            this.panel1.Location = new System.Drawing.Point(17, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 142);
            this.panel1.TabIndex = 0;
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(30, 114);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(311, 23);
            this.progressBar3.TabIndex = 2;
            // 
            // labelreplay
            // 
            this.labelreplay.BackColor = System.Drawing.Color.Orange;
            this.labelreplay.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelreplay.Location = new System.Drawing.Point(30, 90);
            this.labelreplay.Name = "labelreplay";
            this.labelreplay.Size = new System.Drawing.Size(292, 19);
            this.labelreplay.TabIndex = 5;
            this.labelreplay.Text = "انتظار الرد";
            // 
            // labelsend
            // 
            this.labelsend.BackColor = System.Drawing.Color.Orange;
            this.labelsend.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelsend.Location = new System.Drawing.Point(30, 29);
            this.labelsend.Name = "labelsend";
            this.labelsend.Size = new System.Drawing.Size(292, 19);
            this.labelsend.TabIndex = 4;
            this.labelsend.Text = "ارسال البيانات";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(30, 51);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.RightToLeftLayout = true;
            this.progressBar1.Size = new System.Drawing.Size(311, 20);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 3;
            // 
            // checkBoxReplayReceive
            // 
            this.checkBoxReplayReceive.Enabled = false;
            this.checkBoxReplayReceive.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxReplayReceive.Location = new System.Drawing.Point(320, 88);
            this.checkBoxReplayReceive.Name = "checkBoxReplayReceive";
            this.checkBoxReplayReceive.Size = new System.Drawing.Size(21, 24);
            this.checkBoxReplayReceive.TabIndex = 2;
            this.checkBoxReplayReceive.UseVisualStyleBackColor = true;
            // 
            // checkBoxSend
            // 
            this.checkBoxSend.Enabled = false;
            this.checkBoxSend.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSend.Location = new System.Drawing.Point(320, 30);
            this.checkBoxSend.Name = "checkBoxSend";
            this.checkBoxSend.Size = new System.Drawing.Size(21, 24);
            this.checkBoxSend.TabIndex = 1;
            this.checkBoxSend.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(120, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "الغاء";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelSize
            // 
            this.labelSize.BackColor = System.Drawing.Color.Aquamarine;
            this.labelSize.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSize.Location = new System.Drawing.Point(-1, -1);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(375, 21);
            this.labelSize.TabIndex = 6;
            this.labelSize.Text = "حجم الرسالة";
            // 
            // ExecuteSQLCommand_Insert_Serialiaze_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 212);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "ExecuteSQLCommand_Insert_Serialiaze_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Load += new System.EventHandler(this.ExecuteSQLCommand_Insert_Serialiaze_Form_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxReplayReceive;
        private System.Windows.Forms.CheckBox checkBoxSend;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelreplay;
        private System.Windows.Forms.Label labelsend;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label labelSize;
    }
}