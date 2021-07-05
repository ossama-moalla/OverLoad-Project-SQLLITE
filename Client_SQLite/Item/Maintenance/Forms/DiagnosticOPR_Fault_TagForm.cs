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
    public partial class DiagnosticOPR_Fault_TagForm : Form
    {
        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        DiagnosticOPR _DiagnosticOPR;
        MaintenanceFault _MaintenanceFault;
        MaintenanceTag _DiagnosticOPR_Fault_Tag;
        public DiagnosticOPR_Fault_TagForm(DatabaseInterface db, DiagnosticOPR DiagnosticOPR_)
        {
            DB = db;
            InitializeComponent();
            buttonSelectDiagnosticOPR.Enabled = false;
            this.textBoxFaultID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxFaultID_MouseDoubleClick);

            _DiagnosticOPR = DiagnosticOPR_;
            _MaintenanceOPR = _DiagnosticOPR._MaintenanceOPR;
            textBoxDiagnosticID.Text = _DiagnosticOPR.DiagnosticOPRID.ToString();
            textBoxDiagnosticDesc.Text = _DiagnosticOPR.Desc;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء رابط";
        }
        public DiagnosticOPR_Fault_TagForm(DatabaseInterface db, MaintenanceFault MaintenanceFault_)
        {
            DB = db;
            InitializeComponent();
            buttonSelectFault.Enabled = false;
            this.textBoxDiagnosticID.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiagnosticID_MouseDoubleClick);

            _MaintenanceFault = MaintenanceFault_;
            _MaintenanceOPR= MaintenanceFault_._MaintenanceOPR;
            textBoxFaultID.Text = _MaintenanceFault.FaultID.ToString();
            textBoxFaultDes.Text = _MaintenanceFault.FaultDesc;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "انشاء رابط";
        }
        public DiagnosticOPR_Fault_TagForm(DatabaseInterface db, MaintenanceTag  DiagnosticOPR_Fault_Tag_,bool Edit)
        {
            DB = db;
            InitializeComponent();
            if(Edit )
            {
                buttonSave.Visible = true;
                textBoxTagINFO.ReadOnly = false;
            }
            else
            {
                buttonSave.Visible = false ;
                textBoxTagINFO.ReadOnly = true ;
            }
            buttonSelectFault.Enabled = false;
            buttonSelectDiagnosticOPR.Enabled = false;
            _DiagnosticOPR_Fault_Tag = DiagnosticOPR_Fault_Tag_;
            _MaintenanceFault = _DiagnosticOPR_Fault_Tag._MaintenanceFault ;
            _DiagnosticOPR = _DiagnosticOPR_Fault_Tag._DiagnosticOPR ;
            _MaintenanceOPR = _DiagnosticOPR_Fault_Tag._MaintenanceFault ._MaintenanceOPR;
            textBoxFaultID.Text = _MaintenanceFault.FaultID.ToString();
            textBoxFaultDes.Text = _MaintenanceFault.FaultDesc;
            textBoxDiagnosticID.Text = _DiagnosticOPR.DiagnosticOPRID.ToString();
            textBoxDiagnosticDesc.Text = _DiagnosticOPR.Desc;
            buttonSave.Name = "buttonSave";
            buttonSave.Text = "تعديل الرابط";
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
                MessageBox.Show("buttonSelectFault_Click:"+ee.Message , "",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
         
        }

        private void buttonSelectDiagnosticOPR_Click(object sender, EventArgs e)
        {
            try
            {
                SelecObjectForm SelecObjectForm_ = new SelecObjectForm("اختر عطل");
                List<DiagnosticOPRReport> DiagnosticOPRList = new DiagnosticOPRSQL(DB).Get_All_DiagnosticOPRReportList(_MaintenanceOPR);
                DiagnosticOPRReport.InitializeDiagnosticOPRListViewColumns(ref SelecObjectForm_._listView);
                DiagnosticOPRReport.RefreshSubDiagnosticOPRList(ref SelecObjectForm_._listView, DiagnosticOPRList);
                SelecObjectForm_.adjustcolumns = f => DiagnosticOPRReport.AdjustlistViewDiagnosticOPRColumnsWidth(ref SelecObjectForm_._listView);
                SelecObjectForm_.ShowDialog();
                if (SelecObjectForm_.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        _DiagnosticOPR = new DiagnosticOPRSQL(DB).GetDiagnosticOPRINFO_BYID(SelecObjectForm_.ReturnID);
                        textBoxDiagnosticID.Text = _DiagnosticOPR.DiagnosticOPRID.ToString();
                        textBoxDiagnosticDesc.Text = _DiagnosticOPR.Desc;
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Failed_To_Get_ID" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSelectDiagnosticOPR_Click:"+ee.Message , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonSave.Name == "buttonAdd")
                {
                    bool success= new MaintenanceTagSQL(DB).CreateMaintenanceTag (textBoxTagINFO.Text,_MaintenanceFault , _DiagnosticOPR,null  );
                    if (success)
                    {
                        MessageBox.Show("تم الربط بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    bool success = new MaintenanceTagSQL(DB).UpdateMaintenanceTag (_DiagnosticOPR_Fault_Tag.TagID, textBoxTagINFO.Text);
                    if (success)
                    {
                        MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSave_Click" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxFaultID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSelectFault.PerformClick();
        }

        private void textBoxDiagnosticID_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                        buttonSelectDiagnosticOPR.PerformClick();

        }
    }
}
