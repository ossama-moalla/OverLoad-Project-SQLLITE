using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Reports.Objects;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Reports
{
    public partial class BillMaintenance_Print_Form : Form
    {
        DatabaseInterface DB;
        BillMaintenance _BillMaintenance;

        public BillMaintenance_Print_Form(DatabaseInterface db, BillMaintenance BillMaintenance_)
        {
            _BillMaintenance = BillMaintenance_;
            DB = db;
            InitializeComponent();

        }

        private void BillMaintenance_Print_Form_Load(object sender, EventArgs e)
        {
            double ItemOUT_Value = 0;
            double AdditionalValue = 0;
            double RepairOPR_Value = 0;
            double DiagnosticOPR_Value = 0;


            List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_BillMaintenance._Operation);
            List<PayIN> PayINList = new PayINSQL(DB).GetPayINList(_BillMaintenance._Operation);
            List<BillAdditionalClause> BillAdditionalClauseList = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(_BillMaintenance._Operation);
            List<BillMaintenance_Clause> BillMaintenance_ClauseList = new BillMaintenanceSQL(DB).BillMaintenance_GetClauses(_BillMaintenance);
            List<BillMaintenance_RepairClause_Report> BillMaintenance_RepairClause_ReportList =
                BillMaintenance_RepairClause_Report.GetBillMaintenance_RepairClause_ReportList(_BillMaintenance._Currency, BillMaintenance_ClauseList);

            List<BillMaintenance_DiagnosticOPR_Clause_Report> BillMaintenance_DiagnosticOPR_Clause_ReportList =
               BillMaintenance_DiagnosticOPR_Clause_Report.GetBillMaintenance_DiagnosticOPR_Clause_ReportList (_BillMaintenance._Currency, BillMaintenance_ClauseList);
            RepairOPR_Value = BillMaintenance_ClauseList.Where (y=>y.ClauseType ==BillMaintenance_Clause.REPAIR_OPR_TYPE
            &&y.Value !=null ).Sum(x =>Convert .ToDouble ( x.Value));
            DiagnosticOPR_Value = BillMaintenance_ClauseList.Where(y => y.ClauseType == BillMaintenance_Clause.DIAGNOSTIC_OPR_TYPE 
          && y.Value != null).Sum(x => Convert.ToDouble(x.Value));

            string Paid = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayIN(PayINList));

            double Paid_UPON_BillCurreny = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(_BillMaintenance._Operation);
            ItemOUT_Value = ItemOUTList.Sum(x => x._OUTValue.Value * x.Amount);
            AdditionalValue = BillAdditionalClauseList.Sum(x => x.Value);
            double BillValue = ItemOUT_Value + AdditionalValue - _BillMaintenance.Discount;
            List<ItemOUT_Report> ItemOUT_ReportList = Reports.Objects.ItemOUT_Report.GetItemOUT_ReportList (ItemOUTList);
            List<Bill_PayIN_Report> Bill_PayIN_ReportList = Reports.Objects.Bill_PayIN_Report.GetBill_PayIN_ReportList (PayINList);
            List<BillAdditionalClause_Report> BillAdditionalClause_ReporttList = Reports.Objects.BillAdditionalClause_Report.GetBillAdditionalClause_ReportList(_BillMaintenance._Currency, BillAdditionalClauseList);

            List<MaintenanceFaultReport> MaintenanceFaultReportList = new MaintenanceFaultSQL(DB).GetMaintenanceOPR_Report_Fault_List(_BillMaintenance._MaintenanceOPR);
            List<MaintenanceOPR_Accessory> MaintenanceOPR_AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_BillMaintenance._MaintenanceOPR);

            List<MaintenanceOPR_Accessory_Print_Report> MaintenanceOPR_Accessory_Print_ReportList = MaintenanceOPR_Accessory_Print_Report.GetMaintenanceOPR_Accessory_Print_ReportList(MaintenanceOPR_AccessoryList);
            List<MaintenanceFault_Print_Report> MaintenanceFault_Print_ReportList = MaintenanceFault_Print_Report.GetMaintenanceFault_Print_ReportList(MaintenanceFaultReportList);

            MaintenanceOPR_Accessory_Print_ReportBindingSource.DataSource = MaintenanceOPR_Accessory_Print_ReportList;
                MaintenanceFault_Print_ReportBindingSource.DataSource = MaintenanceFault_Print_ReportList;
            BillMaintenance_RepairClause_ReportBindingSource.DataSource = BillMaintenance_RepairClause_ReportList;
            BillMaintenance_DiagnosticOPR_Clause_ReportBindingSource.DataSource = BillMaintenance_DiagnosticOPR_Clause_ReportList;
            ItemOUT_ReportBindingSource.DataSource = ItemOUT_ReportList;
            BillAdditionalClause_ReportBindingSource.DataSource = BillAdditionalClause_ReporttList;

            Bill_PayIN_ReportBindingSource.DataSource = Bill_PayIN_ReportList;

            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("BillMaintenance_ID", _BillMaintenance._Operation.OperationID.ToString()));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("BillMaintenance_Date", _BillMaintenance.BillDate.ToString()));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("Contact", _BillMaintenance._Contact.Get_Complete_ContactName_WithHeader()));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("Currency", _BillMaintenance._Currency.CurrencyName));

            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("RepairOPR_Value", RepairOPR_Value + " " + _BillMaintenance._Currency.CurrencySymbol));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("DiagnosticOPR_Value", DiagnosticOPR_Value + " " + _BillMaintenance._Currency.CurrencySymbol));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("ItemOUT_Value", ItemOUT_Value + " " + _BillMaintenance._Currency.CurrencySymbol));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("Additional_Clause_Value", AdditionalValue + " " + _BillMaintenance._Currency.CurrencySymbol));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("Discount", _BillMaintenance.Discount + " " + _BillMaintenance._Currency.CurrencySymbol));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("ClearValue", BillValue + " " + _BillMaintenance._Currency.CurrencySymbol));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("Paid", Paid));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("Remain", (BillValue - Paid_UPON_BillCurreny) + " " + _BillMaintenance._Currency.CurrencySymbol));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("Printdate", DateTime.Now . ToString()));
            this.reportViewerBillMaintenance.LocalReport.SetParameters(new ReportParameter("ServerName", DB.COMPANY .PartName));




            this.reportViewerBillMaintenance.Refresh();
            this.reportViewerBillMaintenance.RefreshReport();

            this.reportViewerBillMaintenance.RefreshReport();
        }
    }
}
