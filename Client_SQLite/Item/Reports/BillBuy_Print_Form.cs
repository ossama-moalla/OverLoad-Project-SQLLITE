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
    public partial class BillBuy_Print_Form : Form
    {
        DatabaseInterface DB;
        BillBuy _BillBuy;
       
        public BillBuy_Print_Form(DatabaseInterface db,BillBuy BillBuy_)
        {
            _BillBuy = BillBuy_;
            DB = db;
            InitializeComponent();
          
          }

        private void BillBuy_Print_Form_Load(object sender, EventArgs e)
        {
            double ItemIN_Value=0;
            double AdditionalValue=0;




            List<ItemIN> ItemINList = new ItemINSQL(DB).GetItemINList(_BillBuy._Operation);
            List<PayOUT> PayOUTList = new PayOUTSQL(DB).GetPaysOUT_List(_BillBuy._Operation);
            List<BillAdditionalClause> BillAdditionalClauseList = new BillAdditionalClauseSQL(DB).GetBill_AdditionalClauses(_BillBuy._Operation);

            string Paid= Money_Currency.ConvertMoney_CurrencyList_TOString(Money_Currency.Get_Money_Currency_List_From_PayOUT(PayOUTList));

            double Paid_UPON_BillCurreny = new OperationSQL(DB).Get_OperationPaysValue_UPON_OperationCurrency(_BillBuy._Operation);
            ItemIN_Value = ItemINList.Sum(x => x._INCost.Value * x.Amount);
            AdditionalValue = BillAdditionalClauseList.Sum(x => x.Value);
            double BillValue = ItemIN_Value + AdditionalValue - _BillBuy.Discount;
            List<ItemIN_Report>  ItemIN_ReportList = Reports.Objects.ItemIN_Report.GetItemIN_ReportList(ItemINList);
            List<Bill_PayOUT_Report>  Bill_PayOUT_ReportList = Reports.Objects.Bill_PayOUT_Report.GetBill_PayOUT_ReportList(PayOUTList);
            List<BillAdditionalClause_Report>  BillAdditionalClause_ReporttList = Reports.Objects.BillAdditionalClause_Report.GetBillAdditionalClause_ReportList(_BillBuy._Currency, BillAdditionalClauseList);


            ItemIN_ReportBindingSource.DataSource = ItemIN_ReportList;
            Bill_PayOUT_ReportBindingSource.DataSource = Bill_PayOUT_ReportList;
            BillAdditionalClause_ReportBindingSource.DataSource = BillAdditionalClause_ReporttList;


            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("BillBuy_ID", _BillBuy._Operation.OperationID.ToString()));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("BillBuy_Date", _BillBuy.BillDate .ToString()));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Contact", _BillBuy._Contact.Get_Complete_ContactName_WithHeader()));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Currency", _BillBuy._Currency .CurrencyName));
            
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("ItemIN_Value", ItemIN_Value  + " " + _BillBuy._Currency.CurrencySymbol));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Additional_Clause_Value", AdditionalValue + " " + _BillBuy._Currency.CurrencySymbol));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Discount", _BillBuy.Discount + " " + _BillBuy._Currency.CurrencySymbol));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("ClearValue", BillValue + " " + _BillBuy._Currency.CurrencySymbol));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Paid", Paid));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Remain", (BillValue - Paid_UPON_BillCurreny) + " " + _BillBuy._Currency.CurrencySymbol));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Printdate", DateTime.Now.ToString()));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("ServerName", DB.COMPANY.PartName));

            this.reportViewer1.Refresh();
            this.reportViewer1.RefreshReport();

        }
    }
}
