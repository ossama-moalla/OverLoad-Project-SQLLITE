using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public partial class SelecObjectForm : Form
    {
        private uint _ReturnID;
        public uint ReturnID
        {
            get
            {
                return _ReturnID;
            }
        }
        public Action<ListView > adjustcolumns;
        public ListView listview
        {
            get { return _listView; }

        }
        public SelecObjectForm(string title)
        {
            InitializeComponent();
            label16.Text = title;
            _listView = new ListView();
            this._listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));
            this._listView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._listView.FullRowSelect = true;
            this._listView.GridLines = true;
            this._listView.Location = new System.Drawing.Point(3, 34);
            this._listView.Name = "_listView";
            this._listView.RightToLeftLayout = true;
            this._listView.Size = new System.Drawing.Size(790, 305);
            this._listView.TabIndex = 55;
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.View = System.Windows.Forms.View.Details;
            this.panelListview.Controls.Add(this._listView);
            _listView .Resize  += new System.EventHandler(this.listView_MissedFault_Item_Resize );
            this._listView .MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);

        }
        private void listView_MissedFault_Item_Resize(object sender, EventArgs e)
        {
           
            //MaintenanceFaultReport .AdjustlistViewFaultReportOPRColumnsWidth (ref listViewSubDiagnosticOPR);
            adjustcolumns(_listView );
         }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _listView .SelectedItems.Count > 0)
            {
                buttonSelect .PerformClick();
            }
        }
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if(_listView .SelectedItems .Count >0)
            {
               try
                {
                    _ReturnID = Convert.ToUInt32(_listView .SelectedItems[0].Name );
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Failed_To_Get_ID" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("يرجى الاختيار " , "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
