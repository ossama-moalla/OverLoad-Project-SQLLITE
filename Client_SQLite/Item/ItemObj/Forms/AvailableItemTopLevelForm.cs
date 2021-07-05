using OverLoad_Client.ItemObj.Objects;
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
    public partial class AvailableItemTopLevelForm : Form
    {
        public Item _Item;
        public Item ReturnItem
        {
            get { return _Item; }
        }

        DatabaseInterface DB;
        User_AvailabeItemsForm AvailabeItemsForm_;
        ShowAvailableItemSimpleForm ShowAvailableItemSimpleForm_;
        Form selectedform;
        Folder LastUsedFolder;
        bool GetAvailableItem;
        public AvailableItemTopLevelForm(DatabaseInterface db, Folder f, bool GetAvailableItem_)
        {
            DB = db;
            GetAvailableItem = GetAvailableItem_;
            LastUsedFolder = f;
            InitializeComponent();


            AvailabeItemsForm_ = new User_AvailabeItemsForm(DB, LastUsedFolder, GetAvailableItem);
            AvailabeItemsForm_.TopLevel = false;
            AvailabeItemsForm_.Dock = DockStyle.Fill;
            AvailabeItemsForm_.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            AvailabeItemsForm_.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Sub_FormClosed);

            ShowAvailableItemSimpleForm_ = new ShowAvailableItemSimpleForm(DB, GetAvailableItem);
            ShowAvailableItemSimpleForm_.TopLevel = false;
            ShowAvailableItemSimpleForm_.Dock = DockStyle.Fill;
            ShowAvailableItemSimpleForm_.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ShowAvailableItemSimpleForm_.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Sub_FormClosed);

            comboBoxMethod.SelectedIndex = 1;

        }
        private async void ConvertTO_ShowAvailableItemSimpleForm_()
        {
            selectedform = ShowAvailableItemSimpleForm_;

            panelBody.Controls.Add(ShowAvailableItemSimpleForm_);
            ShowAvailableItemSimpleForm_.Show();
        }
        private async void ConvertTO_AvailabeItemsForm()
        {
       
            selectedform = AvailabeItemsForm_;


            panelBody.Controls.Add(AvailabeItemsForm_);
            AvailabeItemsForm_.Show();
        }
        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxMethod.SelectedIndex == 1)
            {
                panelBody.Controls.Clear();
                ConvertTO_AvailabeItemsForm();
            }
            else
            {
                panelBody.Controls.Clear();

                ConvertTO_ShowAvailableItemSimpleForm_();
            }
        }

        private void Sub_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (selectedform == AvailabeItemsForm_)
            {
                if (AvailabeItemsForm_.DialogResult == DialogResult.OK)
                {
                    _Item = AvailabeItemsForm_.ReturnItem;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                if (ShowAvailableItemSimpleForm_.DialogResult == DialogResult.OK)
                {
                    _Item = ShowAvailableItemSimpleForm_.ReturnItem;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }
    }
}
