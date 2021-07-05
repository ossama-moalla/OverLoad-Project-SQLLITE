using OverLoad_Client.Maintenance.MaintenanceSQL;
using OverLoad_Client.Maintenance.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client.Maintenance.Forms
{
    public partial class Fault_MissedFaultItem_TagForm : Form
    {
        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        MaintenanceFault _MaintenanceFault;
        Missed_Fault_Item _Missed_Fault_Item;
        MaintenanceTag _MaintenanceTag;
        public Fault_MissedFaultItem_TagForm(DatabaseInterface db, MaintenanceFault MaintenanceFault_)
        {
            DB = db;
            InitializeComponent();
            buttonSelectFault .Enabled = false;
            this.textBoxMissedFaultIItemD.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMissedFaultIItemD_MouseDoubleClick);

            _MaintenanceFault = MaintenanceFault_;
            _MaintenanceOPR = _MaintenanceFault._MaintenanceOPR;
            textBoxFaultID.Text = _MaintenanceFault.FaultID.ToString();
            textBoxFaultDes.Text = _MaintenanceFault.FaultDesc;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء رابط";
        }
        public Fault_MissedFaultItem_TagForm(DatabaseInterface db, Missed_Fault_Item Missed_Fault_Item_)
        {
            DB = db;
            InitializeComponent();
            buttonSelectMissedFaultItem.Enabled = false;
            this.textBoxFaultID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxFaultID_MouseDoubleClick);

            _Missed_Fault_Item = Missed_Fault_Item_;
            _MaintenanceOPR= Missed_Fault_Item_._DiagnosticOPR . _MaintenanceOPR;
            textBoxMissedFaultIItemD.Text = _Missed_Fault_Item.ID.ToString();
            textBoxMissedFaultItemDes.Text = _Missed_Fault_Item.GetDesc();
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء رابط";
        }
        public Fault_MissedFaultItem_TagForm(DatabaseInterface db, MaintenanceTag MaintenanceTag_, bool Edit)
        {
            DB = db;
            InitializeComponent();
            if (Edit)
            {
                buttonSave.Visible = true;
                textBoxTagINFO.ReadOnly = false;
            }
            else
            {
                buttonSave.Visible = false;
                textBoxTagINFO.ReadOnly = true;
            }
            buttonSelectMissedFaultItem.Enabled = false;
            buttonSelectFault.Enabled = false;
            _MaintenanceTag = MaintenanceTag_;
            _Missed_Fault_Item   = _MaintenanceTag._Missed_Fault_Item ;
            _MaintenanceFault = _MaintenanceTag._MaintenanceFault ;
            _MaintenanceOPR = _MaintenanceTag._Missed_Fault_Item._DiagnosticOPR  ._MaintenanceOPR;
            textBoxMissedFaultIItemD.Text = _Missed_Fault_Item .ID .ToString();
            textBoxMissedFaultItemDes.Text = _Missed_Fault_Item.GetDesc ();
            textBoxFaultID.Text = _MaintenanceFault .FaultID.ToString();
            textBoxFaultDes.Text = _MaintenanceFault.FaultDesc ;
            buttonSave.Name = "buttonSave";
            buttonSave.Text  = "تعديل الرابط";
        }


       

        private void buttonSelectMissed_Fault_Item_Click(object sender, EventArgs e)
        {
            try
            {
                SelecObjectForm SelecObjectForm_ = new SelecObjectForm("اختر عنصر مفقود او تالف");
                List<Missed_Fault_Item> Missed_Fault_ItemList = new MissedFaultItemSQL(DB).MaintenanceOPR_GetMissed_Fault_Item_List(_MaintenanceOPR);
                Missed_Fault_Item.InitializeMissed_Fault_ListViewColumns (ref SelecObjectForm_._listView);

                Missed_Fault_Item.RefreshMissed_FaultList(ref SelecObjectForm_._listView, Missed_Fault_ItemList);
                SelecObjectForm_.adjustcolumns = f => Missed_Fault_Item.AdjustlistViewMissed_Fault_ColumnsWidth(ref SelecObjectForm_._listView);
                SelecObjectForm_.ShowDialog();
                if (SelecObjectForm_.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        _Missed_Fault_Item = new MissedFaultItemSQL(DB).GetMissedFaultItem_INFO_BYID(SelecObjectForm_.ReturnID);
                        textBoxMissedFaultIItemD.Text = _Missed_Fault_Item.ID.ToString();
                        textBoxMissedFaultItemDes.Text = _Missed_Fault_Item.GetDesc();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Failed_To_Get_ID" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSelectMissed_Fault_Item_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
       

        }
        private void buttonSelectFault_Click(object sender, EventArgs e)
        {
            try
            {
                SelecObjectForm SelecObjectForm_ = new SelecObjectForm("اختر عطل");
                List<MaintenanceFaultReport> FaultList = new MaintenanceFaultSQL(DB).GetMaintenanceOPR_Report_Fault_List(_MaintenanceOPR);
                MaintenanceFaultReport.InitializeFaultReportListViewColumns(ref SelecObjectForm_._listView);
                MaintenanceFaultReport.RefreshFaultReportList(ref SelecObjectForm_._listView, FaultList);
                SelecObjectForm_.adjustcolumns = f => MaintenanceFaultReport.AdjustlistViewFaultReportOPRColumnsWidth(ref SelecObjectForm_._listView);
                SelecObjectForm_.ShowDialog();
                if (SelecObjectForm_.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        _MaintenanceFault = new MaintenanceFaultSQL(DB).Get_Fault_INFO_BYID(SelecObjectForm_.ReturnID);
                        textBoxFaultID.Text = _MaintenanceFault.FaultID.ToString();
                        textBoxFaultDes.Text = _MaintenanceFault.FaultDesc;
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Failed_To_Get_ID" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSelectFault_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            try
            {


                if (buttonSave.Name == "buttonAdd")
                {
                   bool success= new MaintenanceTagSQL (DB).CreateMaintenanceTag  (textBoxTagINFO.Text, _MaintenanceFault,null  , _Missed_Fault_Item);
                    if(success )
                    {
                        MessageBox.Show("تم الربط بنجاح" ,"", MessageBoxButtons.OK, MessageBoxIcon.Information );

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    bool success = new MaintenanceTagSQL(DB).UpdateMaintenanceTag  (_MaintenanceTag.TagID, textBoxTagINFO.Text);
                    MessageBox.Show("تم التعديل  بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxMissedFaultIItemD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSelectMissedFaultItem.PerformClick();
        }

        private void textBoxFaultID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSelectFault.PerformClick();
        }
    }
}
