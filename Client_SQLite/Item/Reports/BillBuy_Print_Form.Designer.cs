namespace OverLoad_Client.Reports
{
    partial class BillBuy_Print_Form
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ItemIN_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Bill_PayOUT_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BillAdditionalClause_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ItemIN_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_PayOUT_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillAdditionalClause_ReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "BillBuy_ItemIN";
            reportDataSource1.Value = this.ItemIN_ReportBindingSource;
            reportDataSource2.Name = "BillBuy_PaysOut";
            reportDataSource2.Value = this.Bill_PayOUT_ReportBindingSource;
            reportDataSource3.Name = "Billbuy_AdditionalClause";
            reportDataSource3.Value = this.BillAdditionalClause_ReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "OverLoad_Client.Reports.ReportBillBuy.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(5, 8);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(625, 397);
            this.reportViewer1.TabIndex = 0;
            // 
            // ItemIN_ReportBindingSource
            // 
            this.ItemIN_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.ItemIN_Report);
            // 
            // Bill_PayOUT_ReportBindingSource
            // 
            this.Bill_PayOUT_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.Bill_PayOUT_Report);
            // 
            // BillAdditionalClause_ReportBindingSource
            // 
            this.BillAdditionalClause_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.BillAdditionalClause_Report);
            // 
            // BillBuy_Print_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 406);
            this.Controls.Add(this.reportViewer1);
            this.Name = "BillBuy_Print_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "طباعة فاتورة شراء";
            this.Load += new System.EventHandler(this.BillBuy_Print_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ItemIN_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_PayOUT_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillAdditionalClause_ReportBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ItemIN_ReportBindingSource;
        private System.Windows.Forms.BindingSource Bill_PayOUT_ReportBindingSource;
        private System.Windows.Forms.BindingSource BillAdditionalClause_ReportBindingSource;
    }
}