using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public  class OverLoad_Form : Form
    {
        public bool Refresh_ListViewMaintenanceOPRs_Flag {get;set;}=false ;
        public bool Refresh_ListViewMoneyDataDetails_Flag { get; set; } = false;
        public bool  Refresh_ListViewPayOrders_Flag { get; set; } = false;
        public bool  Refresh_ListViewSells_Flag { get; set; } = false;
        public bool  Refresh_ListViewBuys_Flag { get; set; } = false;

        public bool RefreshEmployeesReportList { get; set; } = false;
    }
}
