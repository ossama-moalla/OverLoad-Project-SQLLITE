namespace OverLoad_Client.Reports
{
    partial class BillSell_Print_Form
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewerBillSell = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ItemOUT_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BillAdditionalClause_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Bill_PayIN_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ItemOUT_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillAdditionalClause_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_PayIN_ReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewerBillSell
            // 
            this.reportViewerBillSell.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "BillSell_ItemOUT";
            reportDataSource1.Value = this.ItemOUT_ReportBindingSource;
            reportDataSource2.Name = "BillSell_AdditionalClause";
            reportDataSource2.Value = this.BillAdditionalClause_ReportBindingSource;
            reportDataSource3.Name = "BillSell_PayIN";
            reportDataSource3.Value = this.Bill_PayIN_ReportBindingSource;
            this.reportViewerBillSell.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerBillSell.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewerBillSell.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewerBillSell.LocalReport.ReportEmbeddedResource = "OverLoad_Client.Reports.ReportBillSell.rdlc";
            this.reportViewerBillSell.Location = new System.Drawing.Point(4, 7);
            this.reportViewerBillSell.Name = "reportViewerBillSell";
            this.reportViewerBillSell.Size = new System.Drawing.Size(623, 396);
            this.reportViewerBillSell.TabIndex = 0;
            // 
            // ItemOUT_ReportBindingSource
            // 
            this.ItemOUT_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.ItemOUT_Report);
            // 
            // BillAdditionalClause_ReportBindingSource
            // 
            this.BillAdditionalClause_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.BillAdditionalClause_Report);
            // 
            // Bill_PayIN_ReportBindingSource
            // 
            this.Bill_PayIN_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.Bill_PayIN_Report);
            // 
            // BillSell_Print_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 406);
            this.Controls.Add(this.reportViewerBillSell);
            this.Name = "BillSell_Print_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "طباعة فاتورة  مبيع";
            this.Load += new System.EventHandler(this.BillSell_Print_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ItemOUT_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillAdditionalClause_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_PayIN_ReportBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerBillSell;
        private System.Windows.Forms.BindingSource ItemOUT_ReportBindingSource;
        private System.Windows.Forms.BindingSource BillAdditionalClause_ReportBindingSource;
        private System.Windows.Forms.BindingSource Bill_PayIN_ReportBindingSource;
    }
}