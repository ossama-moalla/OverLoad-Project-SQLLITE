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
    public partial class MaintenanceOPR_Accessories_Form : Form
    {
        System.Windows.Forms.MenuItem OpenAccessory_MenuItem;
        System.Windows.Forms.MenuItem AddAccessory_MenuItem;
        System.Windows.Forms.MenuItem EditAccessory_MenuItem;
        System.Windows.Forms.MenuItem DeleteAccessory_MenuItem;

        DatabaseInterface DB;
        MaintenanceOPR _MaintenanceOPR;
        List<MaintenanceOPR_Accessory> AccessoryList = new List<MaintenanceOPR_Accessory>();

        public MaintenanceOPR_Accessories_Form(DatabaseInterface db, MaintenanceOPR MaintenanceOPR_,bool Manage)
        {
            InitializeComponent();
            DB = db;
            _MaintenanceOPR = MaintenanceOPR_;
            OpenAccessory_MenuItem = new System.Windows.Forms.MenuItem("عرض تفاصيل", OpenAccessory_MenuItem_Click);
            AddAccessory_MenuItem = new System.Windows.Forms.MenuItem("اضافة ملحق صيانة", AddAccessory_MenuItem_Click);
            EditAccessory_MenuItem = new System.Windows.Forms.MenuItem("تعديل ", EditAccessory_MenuItem_Click);
            DeleteAccessory_MenuItem = new System.Windows.Forms.MenuItem("حذف", DeleteAccessory_MenuItem_Click);
            AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
            if (Manage )
            {
                this.listViewAccessories.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAccessories_MouseDoubleClick);
                this.listViewAccessories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewAccessories_MouseDown);

            }
            else
            {
                this.listViewAccessories.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAccessories_MouseDoubleClick);

            }
            RefreshAccessories(AccessoryList);
        }
        #region AccessoryRegion
        private void DeleteAccessory_MenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (listViewAccessories.SelectedItems.Count == 0) return;
                DialogResult dd = MessageBox.Show("هل انت متاكد من الحذف؟", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dd != DialogResult.OK) return;
                uint sid = Convert.ToUInt32(listViewAccessories.SelectedItems[0].Name);
                bool success = new MaintenanceAccessorySQL(DB).DeleteAccessory(sid);
                if (success)
                {
                    MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                    RefreshAccessories(AccessoryList);

                }
                else
                {
                    MessageBox.Show("فشل الحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch
            {

            }
        }
        private void EditAccessory_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewAccessories.SelectedItems.Count > 0)
                {
                    uint accessoryid = Convert.ToUInt32(listViewAccessories.SelectedItems[0].Name);
                    MaintenanceOPR_Accessory accessory = new MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(accessoryid);
                    MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new MaintenanceAccessoryForm(DB, accessory, true);
                    MaintenanceAccessoryForm_.ShowDialog();
                    if (MaintenanceAccessoryForm_.Changed)
                    {
                        AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                        RefreshAccessories(AccessoryList);
                    }
                    MaintenanceAccessoryForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditAccessory_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void OpenAccessory_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewAccessories.SelectedItems.Count > 0)
                {

                    uint accessoryid = Convert.ToUInt32(listViewAccessories.SelectedItems[0].Name);
                    MaintenanceOPR_Accessory accessory = new MaintenanceAccessorySQL(DB).Get_Accessory_INFO_BYID(accessoryid);
                    MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new MaintenanceAccessoryForm(DB, accessory, false);
                    MaintenanceAccessoryForm_.ShowDialog();
                    if (MaintenanceAccessoryForm_.Changed)
                    {
                        AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                        RefreshAccessories(AccessoryList);
                    }
                    MaintenanceAccessoryForm_.Dispose();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenAccessory_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void AddAccessory_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MaintenanceAccessoryForm MaintenanceAccessoryForm_ = new MaintenanceAccessoryForm(DB, _MaintenanceOPR);
                DialogResult d = MaintenanceAccessoryForm_.ShowDialog();
                if (MaintenanceAccessoryForm_.Changed)
                {
                    AccessoryList = new MaintenanceAccessorySQL(DB).GetMaintenanceOPR_Accessories_List(_MaintenanceOPR);
                    RefreshAccessories(AccessoryList);
                }
                MaintenanceAccessoryForm_.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddAccessory_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private async void RefreshAccessories(List<MaintenanceOPR_Accessory> AccessoriesList_)
        {
            try
            {
                listViewAccessories.Items.Clear();

                for (int i = 0; i < AccessoriesList_.Count; i++)
                {
                    ListViewItem ListViewItem_ = new ListViewItem((listViewAccessories.Items.Count + 1).ToString());
                    ListViewItem_.Name = AccessoriesList_[i].AccessoryID.ToString();
                    ListViewItem_.SubItems.Add(AccessoriesList_[i]._Item.ItemName);
                    ListViewItem_.SubItems.Add(AccessoriesList_[i]._Item.ItemCompany);
                    ListViewItem_.SubItems.Add(AccessoriesList_[i]._Item.folder.FolderName);
                    ListViewItem_.SubItems.Add(AccessoriesList_[i].ItemSerialNumber);
                    if (AccessoriesList_[i].Place == null)
                    {
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");
                        ListViewItem_.SubItems.Add("-");
                    }
                    else
                    {
                        ListViewItem_.SubItems.Add(AccessoriesList_[i].Place.PlaceName);
                        ListViewItem_.SubItems.Add(AccessoriesList_[i].Place.PlaceID.ToString ());
                        ListViewItem_.SubItems.Add(new Trade.TradeSQL.TradeStorePlaceSQL(DB).GetPlacePath( AccessoriesList_[i].Place));
                    }

                    ListViewItem_.BackColor = Color.LimeGreen;
                    listViewAccessories.Items.Add(ListViewItem_);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("RefreshAccessories:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }
        private void listViewAccessories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && listViewAccessories.SelectedItems.Count > 0)
            {
                OpenAccessory_MenuItem.PerformClick();
            }
        }
        private void listViewAccessories_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewAccessories.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewAccessories.Items)
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


                        MenuItem[] mi1 = new MenuItem[] { OpenAccessory_MenuItem, EditAccessory_MenuItem, DeleteAccessory_MenuItem, new MenuItem("-"), AddAccessory_MenuItem };
                        listViewAccessories.ContextMenu = new ContextMenu(mi1);


                    }
                    else
                    {

                        MenuItem[] mi = new MenuItem[] { AddAccessory_MenuItem };
                        listViewAccessories.ContextMenu = new ContextMenu(mi);

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewAccessories_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        #endregion
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
