namespace OverLoad_Client.AccountingObj.Forms
{
    partial class Stuck_MoneyTransformOPR_OUT_Form
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
            this.labelBillID = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listViewOperation = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelBillID
            // 
            this.labelBillID.BackColor = System.Drawing.Color.Orange;
            this.labelBillID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBillID.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelBillID.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBillID.Location = new System.Drawing.Point(0, 0);
            this.labelBillID.Name = "labelBillID";
            this.labelBillID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelBillID.Size = new System.Drawing.Size(928, 36);
            this.labelBillID.TabIndex = 12;
            this.labelBillID.Text = "عمليات التحويل الصادرة المعلقة";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.listViewOperation);
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(928, 339);
            this.panel2.TabIndex = 13;
            // 
            // listViewOperation
            // 
            this.listViewOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewOperation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader15,
            this.columnHeader1});
            this.listViewOperation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewOperation.FullRowSelect = true;
            this.listViewOperation.GridLines = true;
            this.listViewOperation.Location = new System.Drawing.Point(2, 3);
            this.listViewOperation.Name = "listViewOperation";
            this.listViewOperation.RightToLeftLayout = true;
            this.listViewOperation.Size = new System.Drawing.Size(920, 331);
            this.listViewOperation.TabIndex = 16;
            this.listViewOperation.UseCompatibleStateImageBehavior = false;
            this.listViewOperation.View = System.Windows.Forms.View.Details;
            this.listViewOperation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewOperation_MouseDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "الرقم";
            this.columnHeader2.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "التاريخ";
            this.columnHeader3.Width = 132;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "الصندوق المصدر";
            this.columnHeader7.Width = 205;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "المبلغ";
            this.columnHeader8.Width = 119;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "العملة";
            this.columnHeader15.Width = 148;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "الموظف المنشىء";
            this.columnHeader1.Width = 171;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(395, 384);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(114, 37);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "اغلاق";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // Stuck_MoneyTransformOPR_OUT_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 424);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelBillID);
            this.Name = "Stuck_MoneyTransformOPR_OUT_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "عمليات التحويل العالقة الصادرة";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelBillID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listViewOperation;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}