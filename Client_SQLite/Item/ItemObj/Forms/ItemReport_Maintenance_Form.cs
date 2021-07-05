using OverLoad_Client.ItemObj.Objects;
using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
using OverLoad_Client.Trade.Objects;
using OverLoad_Client.Trade.TradeSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.ItemObj.Forms
{
    public partial class ItemReport_Maintenance_Form : Form
    {
        DatabaseInterface DB;
        Item item;
        MenuItem OpenMaintenanceOPR_MenuItem;
        MenuItem OpenMaintenanceFault_MenuItem;
        MenuItem OpenDiagnosticOPR_MenuItem;
        public ItemReport_Maintenance_Form(DatabaseInterface db, Item item_)
        {
            item = item_;
            DB = db;
            if (!(DB.IS_Belong_To_Admin_Group(DB.__User.UserID)|| DB.IS_Belong_To_Maintenance_Group (DB.__User.UserID))) throw new Exception("أنت غير منضم لمجموعة المدراء او مجموعة الصيانة, لا يمكنك فتح هذه النافذة");
            InitializeComponent();
            textBoxItemID.Text  = item.ItemID.ToString ();
            textBoxItemName.Text = item.ItemName;
            textBoxItemCompany.Text = item.ItemCompany;
            textBoxItemType.Text = item.folder .FolderName;
            textBoxItemID.Text = item.ItemID.ToString();

            OpenMaintenanceOPR_MenuItem = new MenuItem("فتح ", OpenMaintenanceOPR_MenuItem_Click);
            OpenMaintenanceFault_MenuItem = new MenuItem("فتح ", OpenMaintenanceFault_MenuItem_Click);
            OpenDiagnosticOPR_MenuItem = new MenuItem("فتح ", OpenDiagnosticOPR_MenuItem_Click);
            GetMaintenanceOperations();
        }

        #region Maintenance
        public async void GetMaintenanceOperations()
        {
            try
            {
                listViewMaintenance.Items.Clear();
                List<MaintenanceOPR> MaintenanceOPRlist
                    = new MaintenanceOPRSQL(DB).GetAllMaintenanceOPRs_forItem(item);
                for (int i = 0; i < MaintenanceOPRlist.Count; i++)
                {
                    ListViewItem listviewitem = new ListViewItem(MaintenanceOPRlist[i]._Operation.OperationID.ToString());
                    listviewitem.Name = MaintenanceOPRlist[i]._Operation.OperationID.ToString();
                    listviewitem.SubItems.Add(MaintenanceOPRlist[i].EntryDate.ToShortDateString());
                    listviewitem.SubItems.Add(MaintenanceOPRlist[i]._Contact.ContactName);
                    listviewitem.SubItems.Add(MaintenanceOPRlist[i].FaultDesc);
                    if (MaintenanceOPRlist[i]._MaintenanceOPR_EndWork != null)
                    {
                        listviewitem.SubItems.Add(MaintenanceOPRlist[i]._MaintenanceOPR_EndWork.EndWorkDate.ToString());
                        if (MaintenanceOPRlist[i]._MaintenanceOPR_EndWork.Repaired == true)
                            listviewitem.SubItems.Add("تم الاصلاح");
                        else
                            listviewitem.SubItems.Add("تعذر الاصلاح");
                    }
                    else
                    {
                        listviewitem.SubItems.Add("مازال في الصيانة");
                        listviewitem.SubItems.Add("-");
                    }
                    listViewMaintenance.Items.Add(listviewitem);

                }

                listViewFaults.Items.Clear();
                List<MaintenanceFaultReport> faults_list = new MaintenanceFaultSQL(DB).GetItem_Fault_List(item);
                for (int i = 0; i < faults_list.Count; i++)
                {
                    ListViewItem listviewitem = new ListViewItem(faults_list[i].Fault.FaultID.ToString());
                    listviewitem.Name = faults_list[i].Fault.FaultID.ToString();
                    listviewitem.SubItems.Add(faults_list[i].Fault.FaultDesc);
                    listviewitem.SubItems.Add(faults_list[i].MaintenanceFault_Affictive_RepairOPRList .Count .ToString());
                    listviewitem.SubItems.Add(faults_list[i].Fault._MaintenanceOPR._Operation.OperationID.ToString());
                    listviewitem.SubItems.Add(faults_list[i].Fault._MaintenanceOPR._Contact.ContactName);

                    listViewFaults.Items.Add(listviewitem);

                }

                listViewDiagnosticOPR.Items.Clear();
                List<DiagnosticOPRReport> DiagnosticOPRReport_list = new DiagnosticOPRSQL(DB).Get_All_DiagnosticOPRReportList_ForItem(item);
                for (int i = 0; i < DiagnosticOPRReport_list.Count; i++)
                {
                    ListViewItem listviewitem = new ListViewItem(DiagnosticOPRReport_list[i]._DiagnosticOPR.DiagnosticOPRID.ToString());
                    listviewitem.Name = DiagnosticOPRReport_list[i]._DiagnosticOPR.DiagnosticOPRID.ToString();
                    listviewitem.SubItems.Add(DiagnosticOPRReport_list[i]._DiagnosticOPR.DiagnosticOPRDate.ToShortDateString());
                    listviewitem.SubItems.Add(DiagnosticOPRReport_list[i]._DiagnosticOPR.Desc);
                    listviewitem.SubItems.Add(DiagnosticOPRReport_list[i]._DiagnosticOPR.Location);
                    if (DiagnosticOPRReport_list[i]._DiagnosticOPR.Normal == true)
                    {
                        listviewitem.SubItems.Add("طبيعي");
                        listviewitem.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        listviewitem.SubItems.Add(" غير طبيعي");
                        listviewitem.BackColor = Color.Orange;
                    }
                    listviewitem.SubItems.Add(DiagnosticOPRReport_list[i].MeasureOPR_Count.ToString());
                    listviewitem.SubItems.Add(DiagnosticOPRReport_list[i].Files_Count.ToString());
                    listviewitem.SubItems.Add(DiagnosticOPRReport_list[i].SubDiagnosticOPR_Count.ToString());
                    listViewDiagnosticOPR.Items.Add(listviewitem);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("GetMaintenanceOperations:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenDiagnosticOPR_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewDiagnosticOPR.SelectedItems.Count == 1)
            {
                try
                {
                    uint diagnosticoprid = Convert.ToUInt32(listViewDiagnosticOPR.SelectedItems[0].Name);
                    DiagnosticOPR DiagnosticOPR_ = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(diagnosticoprid);
                    Maintenance.Forms.DiagnosticOPRForm DiagnosticOPRForm_ = new Maintenance.Forms.DiagnosticOPRForm(DB, DiagnosticOPR_, false);
                    DiagnosticOPRForm_.ShowDialog();

                }
                catch (Exception ee)
                {
                    MessageBox.Show("حدث خطأ " + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenMaintenanceFault_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFaults.SelectedItems.Count == 1)
            {
                try
                {
                    uint faultid = Convert.ToUInt32(listViewFaults.SelectedItems[0].Name);
                    MaintenanceFault MaintenanceFault_ = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(faultid);
                    Maintenance.Forms.FaultForm FaultForm_ = new Maintenance.Forms.FaultForm(DB, MaintenanceFault_, false);
                    FaultForm_.ShowDialog();

                }
                catch (Exception ee)
                {
                    MessageBox.Show("حدث خطأ " + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenMaintenanceOPR_MenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMaintenance.SelectedItems.Count == 1)
            {
                try
                {
                    uint maintenanceoprid = Convert.ToUInt32(listViewMaintenance.SelectedItems[0].Name);
                    MaintenanceOPR MaintenanceOPR_ = new MaintenanceOPRSQL(DB).GetMaintenancePRINFO_BYID(maintenanceoprid);
                    Maintenance.Forms.MaintenanceOPRForm MaintenanceOPRForm_ = new Maintenance.Forms.MaintenanceOPRForm(DB, MaintenanceOPR_, false);
                    MaintenanceOPRForm_.ShowDialog();

                }
                catch (Exception ee)
                {
                    MessageBox.Show("حدث خطأ " + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void listViewMaintenance_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewMaintenance.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();

                foreach (ListViewItem item1 in listViewMaintenance.Items)
                {

                    if (item1.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        match = true;
                        listitem = item1;
                        break;
                    }
                }

                if (match)
                {

                    MenuItem[] mi1 = new MenuItem[] { OpenMaintenanceOPR_MenuItem };
                    listViewMaintenance.ContextMenu = new ContextMenu(mi1);
                }


            }
        }
        private void listViewMaintenance_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewMaintenance.SelectedItems.Count > 0)
            {
                OpenMaintenanceOPR_MenuItem.PerformClick();
            }
        }
        private void listViewFault_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewFaults.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();

                foreach (ListViewItem item1 in listViewFaults.Items)
                {

                    if (item1.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        match = true;
                        listitem = item1;
                        break;
                    }
                }

                if (match)
                {

                    MenuItem[] mi1 = new MenuItem[] { OpenMaintenanceFault_MenuItem };
                    listViewFaults.ContextMenu = new ContextMenu(mi1);
                }


            }
        }
        private void listViewFaults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewFaults.SelectedItems.Count > 0)
            {
                OpenMaintenanceFault_MenuItem.PerformClick();
            }
        }
        private void listViewDiagnosticOPR_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                listViewDiagnosticOPR.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();

                foreach (ListViewItem item1 in listViewDiagnosticOPR.Items)
                {

                    if (item1.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        match = true;
                        listitem = item1;
                        break;
                    }
                }

                if (match)
                {

                    MenuItem[] mi1 = new MenuItem[] { OpenDiagnosticOPR_MenuItem };
                    listViewDiagnosticOPR.ContextMenu = new ContextMenu(mi1);
                }


            }
        }
        private void listViewDiagnosticOPR_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewDiagnosticOPR.SelectedItems.Count > 0)
            {
                OpenDiagnosticOPR_MenuItem.PerformClick();
            }
        }
        #endregion
    }
}
