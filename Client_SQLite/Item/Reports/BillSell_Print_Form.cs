using OverLoad_Client.AccountingObj.AccountingSQL;
using OverLoad_Client.AccountingObj.Objects;
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
    public partial class BillSell_Print_Form : Form
    {
        DatabaseInterface DB;
        BillSell _BillSell;

        public BillSell_Print_Form(DatabaseInterface db, BillSell BillSell_)
        {
            _BillSell = BillSell_;
            DB = db;
            InitializeComponent();

        }

        private void BillSell_Print_Form_Load(object sender, EventArgs e)
        {
            double ItemOUT_Value = 0;
            double AdditionalValue = 0;




            List<ItemOUT> ItemOUTList = new ItemOUTSQL(DB).GetItemOUTList(_BillSell._Operation);
            List<PayIN> PayINList = new PayINSQL(DB).GetPayINList(_BillSell._Operation);
            List<BillAdditionalClause> BillAdditionalClauseList = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(_BillSell._Operation);

            string Paid = Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayIN(PayINList));

            double Paid_UPON_BillCurreny = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(_BillSell._Operation);
            ItemOUT_Value = ItemOUTList.Sum(x => x._OUTValue.Value * x.Amount);
            AdditionalValue = BillAdditionalClauseList.Sum(x => x.Value);
            double BillValue = ItemOUT_Value + AdditionalValue - _BillSell.Discount;
            List<ItemOUT_Report> ItemOUT_ReportList = Reports.Objects.ItemOUT_Report.GetItemOUT_ReportList (ItemOUTList);
            List<Bill_PayIN_Report> Bill_PayIN_ReportList = Reports.Objects.Bill_PayIN_Report.GetBill_PayIN_ReportList (PayINList);
            List<BillAdditionalClause_Report> BillAdditionalClause_ReporttList = Reports.Objects.BillAdditionalClause_Report.GetBillAdditionalClause_ReportList(_BillSell._Currency, BillAdditionalClauseList);


            ItemOUT_ReportBindingSource.DataSource = ItemOUT_ReportList;
            Bill_PayIN_ReportBindingSource.DataSource = Bill_PayIN_ReportList;
            BillAdditionalClause_ReportBindingSource.DataSource = BillAdditionalClause_ReporttList;

            
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("BillSell_ID", _BillSell._Operation.OperationID.ToString()));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("BillSell_Date", _BillSell.BillDate.ToString()));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("Contact", _BillSell._Contact.Get_Complete_ContactName_WithHeader()));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("Currency", _BillSell._Currency.CurrencyName));

            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("ItemOUT_Value", ItemOUT_Value + " " + _BillSell._Currency.CurrencySymbol));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("Additional_Clause_Value", AdditionalValue + " " + _BillSell._Currency.CurrencySymbol));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("Discount", _BillSell.Discount + " " + _BillSell._Currency.CurrencySymbol));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("ClearValue", BillValue + " " + _BillSell._Currency.CurrencySymbol));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("Paid", Paid));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("Remain", (BillValue - Paid_UPON_BillCurreny) + " " + _BillSell._Currency.CurrencySymbol));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("Printdate", DateTime.Now . ToString()));
            this.reportViewerBillSell.LocalReport.SetParameters(new ReportParameter("ServerName", DB.COMPANY .PartName));

            this.reportViewerBillSell.Refresh();
            this.reportViewerBillSell.RefreshReport();

            this.reportViewerBillSell.RefreshReport();
        }
    }
}
