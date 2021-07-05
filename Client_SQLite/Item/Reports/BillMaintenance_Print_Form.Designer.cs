namespace OverLoad_Client.Reports
{
    partial class BillMaintenance_Print_Form
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.MaintenanceOPR_Accessory_Print_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MaintenanceFault_Print_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ItemOUT_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BillAdditionalClause_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Bill_PayIN_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BillMaintenance_RepairClause_ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewerBillMaintenance = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.MaintenanceOPR_Accessory_Print_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaintenanceFault_Print_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemOUT_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillAdditionalClause_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_PayIN_ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillMaintenance_RepairClause_ReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MaintenanceOPR_Accessory_Print_ReportBindingSource
            // 
            this.MaintenanceOPR_Accessory_Print_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.MaintenanceOPR_Accessory_Print_Report);
            // 
            // MaintenanceFault_Print_ReportBindingSource
            // 
            this.MaintenanceFault_Print_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.MaintenanceFault_Print_Report);
            // 
            // BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource
            // 
            this.BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.BillMaintenance_DiagnosticOPR_Clause_Report);
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
            // BillMaintenance_RepairClause_ReportBindingSource
            // 
            this.BillMaintenance_RepairClause_ReportBindingSource.DataSource = typeof(OverLoad_Client.Reports.Objects.BillMaintenance_RepairClause_Report);
            // 
            // reportViewerBillMaintenance
            // 
            this.reportViewerBillMaintenance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "BillMaintenance_Accessories";
            reportDataSource1.Value = this.MaintenanceOPR_Accessory_Print_ReportBindingSource;
            reportDataSource2.Name = "BillMaintenance_Faults";
            reportDataSource2.Value = this.MaintenanceFault_Print_ReportBindingSource;
            reportDataSource3.Name = "BillMaintenance_DiagnosticOPR";
            reportDataSource3.Value = this.BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource;
            reportDataSource4.Name = "BillMaintenance_ItemsOUT";
            reportDataSource4.Value = this.ItemOUT_ReportBindingSource;
            reportDataSource5.Name = "BillMaintenance_AdditionalClauses";
            reportDataSource5.Value = this.BillAdditionalClause_ReportBindingSource;
            reportDataSource6.Name = "BillMaintenance_PayIN";
            reportDataSource6.Value = this.Bill_PayIN_ReportBindingSource;
            reportDataSource7.Name = "BillMaintenance_RepairOPR";
            reportDataSource7.Value = this.BillMaintenance_RepairClause_ReportBindingSource;
            this.reportViewerBillMaintenance.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerBillMaintenance.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewerBillMaintenance.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewerBillMaintenance.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewerBillMaintenance.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewerBillMaintenance.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewerBillMaintenance.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewerBillMaintenance.LocalReport.ReportEmbeddedResource = "OverLoad_Client.Reports.ReportBillMaintenance.rdlc";
            this.reportViewerBillMaintenance.Location = new System.Drawing.Point(4, 7);
            this.reportViewerBillMaintenance.Name = "reportViewerBillMaintenance";
            this.reportViewerBillMaintenance.Size = new System.Drawing.Size(623, 396);
            this.reportViewerBillMaintenance.TabIndex = 0;
            // 
            // BillMaintenance_Print_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 406);
            this.Controls.Add(this.reportViewerBillMaintenance);
            this.Name = "BillMaintenance_Print_Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "طباعة فاتورة صيانة";
            this.Load += new System.EventHandler(this.BillMaintenance_Print_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MaintenanceOPR_Accessory_Print_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaintenanceFault_Print_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemOUT_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillAdditionalClause_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bill_PayIN_ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillMaintenance_RepairClause_ReportBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerBillMaintenance;
        private System.Windows.Forms.BindingSource MaintenanceOPR_Accessory_Print_ReportBindingSource;
        private System.Windows.Forms.BindingSource MaintenanceFault_Print_ReportBindingSource;
        private System.Windows.Forms.BindingSource BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource;
        private System.Windows.Forms.BindingSource ItemOUT_ReportBindingSource;
        private System.Windows.Forms.BindingSource BillAdditionalClause_ReportBindingSource;
        private System.Windows.Forms.BindingSource Bill_PayIN_ReportBindingSource;
        private System.Windows.Forms.BindingSource BillMaintenance_RepairClause_ReportBindingSource;
    }
}