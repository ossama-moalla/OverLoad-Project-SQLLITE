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
    public partial class MeasureOPRForm : Form
    {
        DiagnosticOPR _DiagnosticOPR;
        MeasureOPR _MeasureOPR;
        DatabaseInterface DB;
        public MeasureOPRForm()
        {
            InitializeComponent();
        }
        public MeasureOPRForm(DatabaseInterface db, DiagnosticOPR DiagnosticOPR_)
        {
            DB = db;
            _DiagnosticOPR = DiagnosticOPR_;
            InitializeComponent();
            textBoxDiagnosticOPRID.Text = _DiagnosticOPR.DiagnosticOPRID.ToString();
            textBoxDiagnosticOPRDesc.Text = _DiagnosticOPR.Desc;
            buttonSave.Name = "buttonAdd";
            buttonSave.Text = "اضافة";
            FillComboBox("");
            comboBoxNormal.SelectedIndex = 0;
        }

        public MeasureOPRForm(DatabaseInterface db, MeasureOPR MeasureOPR_, bool Edit)
        {
            try
            {
                DB = db;
                InitializeComponent();
                _MeasureOPR = MeasureOPR_;
                _DiagnosticOPR = MeasureOPR_._DiagnosticOPR;
                textBoxDiagnosticOPRID.Text = _DiagnosticOPR.DiagnosticOPRID.ToString();
                textBoxDiagnosticOPRDesc.Text = _DiagnosticOPR.Desc;
                textBoxDesc.Text = _MeasureOPR.Desc;
                textBoxValue.Text = _MeasureOPR.Value.ToString();
                FillComboBox(_MeasureOPR .MeasureUnit);
                if (_MeasureOPR.Normal == null)
                    comboBoxNormal.SelectedIndex = 0;
                else if (_MeasureOPR.Normal == true)
                    comboBoxNormal.SelectedIndex = 1;
                else
                    comboBoxNormal.SelectedIndex = 2;
                if (!Edit )
                {
                    textBoxDesc.ReadOnly  = true ;
                    textBoxValue.ReadOnly  = true ;
                    comboBoxUnit.Enabled = false;
                    comboBoxNormal.Enabled = false;
                    buttonSave.Visible = false;
                }
                else
                {
                    
                    textBoxDesc.ReadOnly = false ;
                    textBoxValue.ReadOnly = false ;
                    comboBoxUnit.Enabled = true ;
                    comboBoxNormal.Enabled = true ;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(":فشل تحميل الصفحة" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void FillComboBox(string measureunit)
        {
            comboBoxUnit.Items.Clear();
            int selectedIndex = -1;
            List<string> MeasureUnitList = new MeasureOPRSQL(DB).GetMeasureUnitList();
            for (int i = 0; i < MeasureUnitList.Count; i++)
            {
                if (MeasureUnitList[i] == measureunit) selectedIndex = i;
                comboBoxUnit.Items.Add(MeasureUnitList[i]);
            }
            comboBoxUnit.SelectedIndex = selectedIndex;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (_DiagnosticOPR  == null) return;
            //ComboboxItem consumeunititem = (ComboboxItem)comboBoxConsumeUnt.SelectedItem;
            //ConsumeUnit _ConsumeUnit = new ConsumeUnitSql(DB).GetConsumeAmountinfo(consumeunititem.Value);

            //ComboboxItem ComboboxItem_selltype = (ComboboxItem)comboBoxSellType.SelectedItem;
            //string SellType_ = ComboboxItem_selltype.Text;
  
            bool? normal;
            if (comboBoxNormal.SelectedIndex == 0) normal = null;
            else if (comboBoxNormal.SelectedIndex == 1)
                normal = true;
            else
                normal = false;
            if (buttonSave.Name == "buttonAdd")
            {
                try
                {
                    bool success = new MeasureOPRSQL(DB).AddMeasureOPR
                        (_DiagnosticOPR.DiagnosticOPRID, textBoxDesc.Text, textBoxValue.Text, comboBoxUnit.Text,normal );
                    if (success)
                    {

                        MessageBox.Show("تم الاضافة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":تعذر اضافة عملية القياس " + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                try
                {
                    bool success = new MeasureOPRSQL(DB).UpdateMeasureOPR
                        (_MeasureOPR.MeasureID, textBoxDesc.Text, textBoxValue.Text, comboBoxUnit.Text,normal );
                    if (success)
                    {

                        MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(":فشل التعديل" + ee.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
