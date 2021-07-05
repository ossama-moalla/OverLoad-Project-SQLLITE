namespace OverLoad_Client.Company.Forms
{
    partial class CompanySalarysForm
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.ButtonLeft = new System.Windows.Forms.Button();
            this.ButtonRight = new System.Windows.Forms.Button();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_Real_PaysOUT = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_PaysOUT = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRealValueAll = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxValueAll = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewYearSalaries = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContentType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CreateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.ButtonLeft);
            this.panel6.Controls.Add(this.ButtonRight);
            this.panel6.Controls.Add(this.textBoxYear);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Location = new System.Drawing.Point(1, 21);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1093, 74);
            this.panel6.TabIndex = 50;
            // 
            // ButtonLeft
            // 
            this.ButtonLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLeft.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLeft.Location = new System.Drawing.Point(834, 36);
            this.ButtonLeft.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonLeft.Name = "ButtonLeft";
            this.ButtonLeft.Size = new System.Drawing.Size(16, 24);
            this.ButtonLeft.TabIndex = 76;
            this.ButtonLeft.Text = ">";
            this.ButtonLeft.UseVisualStyleBackColor = true;
            this.ButtonLeft.Click += new System.EventHandler(this.ButtonLeftRight_Click);
            // 
            // ButtonRight
            // 
            this.ButtonRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonRight.Location = new System.Drawing.Point(940, 36);
            this.ButtonRight.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonRight.Name = "ButtonRight";
            this.ButtonRight.Size = new System.Drawing.Size(16, 24);
            this.ButtonRight.TabIndex = 75;
            this.ButtonRight.Text = "<";
            this.ButtonRight.UseVisualStyleBackColor = true;
            this.ButtonRight.Click += new System.EventHandler(this.ButtonLeftRight_Click);
            // 
            // textBoxYear
            // 
            this.textBoxYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxYear.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBoxYear.Location = new System.Drawing.Point(853, 35);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(84, 27);
            this.textBoxYear.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(964, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 22);
            this.label2.TabIndex = 73;
            this.label2.Text = "رواتب سنة";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.BackColor = System.Drawing.Color.Aquamarine;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(-1, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1093, 29);
            this.label22.TabIndex = 31;
            this.label22.Text = "الرواتب";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox_Real_PaysOUT);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBox_PaysOUT);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBoxRealValueAll);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBoxValueAll);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listViewYearSalaries);
            this.panel1.Location = new System.Drawing.Point(1, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1093, 334);
            this.panel1.TabIndex = 57;
            // 
            // textBox_Real_PaysOUT
            // 
            this.textBox_Real_PaysOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Real_PaysOUT.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_Real_PaysOUT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Real_PaysOUT.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBox_Real_PaysOUT.Location = new System.Drawing.Point(9, 296);
            this.textBox_Real_PaysOUT.Name = "textBox_Real_PaysOUT";
            this.textBox_Real_PaysOUT.ReadOnly = true;
            this.textBox_Real_PaysOUT.Size = new System.Drawing.Size(152, 26);
            this.textBox_Real_PaysOUT.TabIndex = 90;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 264);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 26);
            this.label9.TabIndex = 89;
            this.label9.Text = "القيمة الفعلية للمدفوع";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_PaysOUT
            // 
            this.textBox_PaysOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_PaysOUT.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_PaysOUT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_PaysOUT.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBox_PaysOUT.Location = new System.Drawing.Point(186, 296);
            this.textBox_PaysOUT.Name = "textBox_PaysOUT";
            this.textBox_PaysOUT.ReadOnly = true;
            this.textBox_PaysOUT.Size = new System.Drawing.Size(360, 26);
            this.textBox_PaysOUT.TabIndex = 88;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(335, 267);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(214, 26);
            this.label10.TabIndex = 87;
            this.label10.Text = "الملبغ المدفوع";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxRealValueAll
            // 
            this.textBoxRealValueAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRealValueAll.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxRealValueAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRealValueAll.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxRealValueAll.Location = new System.Drawing.Point(563, 296);
            this.textBoxRealValueAll.Name = "textBoxRealValueAll";
            this.textBoxRealValueAll.ReadOnly = true;
            this.textBoxRealValueAll.Size = new System.Drawing.Size(133, 26);
            this.textBoxRealValueAll.TabIndex = 86;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(563, 264);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 26);
            this.label7.TabIndex = 85;
            this.label7.Text = "القيمة الفعلية الكلية";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValueAll
            // 
            this.textBoxValueAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValueAll.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxValueAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxValueAll.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.textBoxValueAll.Location = new System.Drawing.Point(702, 296);
            this.textBoxValueAll.Name = "textBoxValueAll";
            this.textBoxValueAll.ReadOnly = true;
            this.textBoxValueAll.Size = new System.Drawing.Size(380, 26);
            this.textBoxValueAll.TabIndex = 84;
            this.textBoxValueAll.WordWrap = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(986, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 26);
            this.label6.TabIndex = 83;
            this.label6.Text = "المبلغ الكلي";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1093, 29);
            this.label1.TabIndex = 32;
            this.label1.Text = "أوامر صرف الرواتب";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewYearSalaries
            // 
            this.listViewYearSalaries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewYearSalaries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader4,
            this.ContentName,
            this.ContentType,
            this.CreateDate,
            this.columnHeader1,
            this.columnHeader3});
            this.listViewYearSalaries.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewYearSalaries.FullRowSelect = true;
            this.listViewYearSalaries.GridLines = true;
            this.listViewYearSalaries.Location = new System.Drawing.Point(3, 32);
            this.listViewYearSalaries.Name = "listViewYearSalaries";
            this.listViewYearSalaries.RightToLeftLayout = true;
            this.listViewYearSalaries.Size = new System.Drawing.Size(1085, 220);
            this.listViewYearSalaries.TabIndex = 4;
            this.listViewYearSalaries.UseCompatibleStateImageBehavior = false;
            this.listViewYearSalaries.View = System.Windows.Forms.View.Details;
            this.listViewYearSalaries.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewYearSalaries_MouseDoubleClick);
            this.listViewYearSalaries.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewYearSalaries_MouseDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "السنة";
            this.columnHeader2.Width = 94;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "رقم الشهر";
            this.columnHeader4.Width = 97;
            // 
            // ContentName
            // 
            this.ContentName.Text = "الشهر";
            this.ContentName.Width = 135;
            // 
            // ContentType
            // 
            this.ContentType.Text = "رقم أمر ";
            this.ContentType.Width = 116;
            // 
            // CreateDate
            // 
            this.CreateDate.Text = "تاريخه";
            this.CreateDate.Width = 123;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "عدد الموظفين المشمولين";
            this.columnHeader1.Width = 171;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "مجموع كتلة الرواتب";
            this.columnHeader3.Width = 191;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.CadetBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(5, 441);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1088, 71);
            this.panel5.TabIndex = 58;
            // 
            // CompanySalarysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 524);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Name = "CompanySalarysForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "الرواتب";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewYearSalaries;
        private System.Windows.Forms.ColumnHeader ContentName;
        private System.Windows.Forms.ColumnHeader ContentType;
        private System.Windows.Forms.ColumnHeader CreateDate;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Button ButtonLeft;
        private System.Windows.Forms.Button ButtonRight;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox_Real_PaysOUT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_PaysOUT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRealValueAll;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxValueAll;
        private System.Windows.Forms.Label label6;
    }
}