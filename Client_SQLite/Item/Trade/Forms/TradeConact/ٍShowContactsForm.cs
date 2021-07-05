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

namespace OverLoad_Client.Trade.Forms.TradeContact
{
    public partial class ShowContactsForm : Form
    {

        
        DatabaseInterface DB;
        MenuItem OpenContactMenuItem;
        MenuItem AddContactMenuItem;
        MenuItem EditContactMenuItem;
        MenuItem DeleteContactMenuItem;

        List<Contact>  _ContactList;
        private Contact Contact__;
        public Contact Contact_
        {
            get { return Contact__; }
            set { this.Contact__ = value; }
        }
        public ShowContactsForm(DatabaseInterface db,bool GetContact)
        {
            DB = db;
            InitializeComponent();
            if (!GetContact) buttonSelect.Visible = false;
            OpenContactMenuItem = new MenuItem("استعراض حساب الجهة",OpenSource_MenuItem_Click);

            AddContactMenuItem = new MenuItem("اضافة جهة", AddSource_MenuItem_Click);
            EditContactMenuItem = new MenuItem("تعديل", EditSource_MenuItem_Click);
            DeleteContactMenuItem = new MenuItem("حذف", DeleteSource_MenuItem_Click);
             _ContactList = (new ContactSQL(DB).GetContactList());
            FillContacts(_ContactList);
            comboBox1.SelectedIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);

        }
        public void FillContacts(List<Contact> ContactList)
        {
            try
            {
                listViewContact.Items.Clear();

                for (int i = 0; i < ContactList.Count; i++)
                {
                    if (comboBox1.SelectedIndex == 1 && ContactList[i].ContactType != Contact.CONTACT_PERSON) continue;
                    if (comboBox1.SelectedIndex == 2 && ContactList[i].ContactType != Contact.CONTACT_COMPANY) continue;
                    ListViewItem item = new ListViewItem(ContactList[i].GetContactTypeString());
                    item.Name = ContactList[i].ContactID.ToString();
                    item.SubItems.Add(ContactList[i].ContactName);
                    item.SubItems.Add(ContactList[i].Phone);
                    item.SubItems.Add(ContactList[i].Mobile);
                    item.SubItems.Add(ContactList[i].Address);
                    if (ContactList[i].ContactType == Contact.CONTACT_COMPANY)
                        item.BackColor = Color.Orange;
                    else
                        item.BackColor = Color.LimeGreen;
                    listViewContact.Items.Add(item);
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("FillContacts:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        

        private void listViewContact_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                listViewContact.ContextMenu = null;
                bool match = false;
                ListViewItem listitem = new ListViewItem();
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item1 in listViewContact.Items)
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
                        List<MenuItem> MenuItemList = new List<MenuItem>();
                        if (buttonSelect.Visible == false)
                            MenuItemList.Add(OpenContactMenuItem);
                        MenuItemList.AddRange(new MenuItem[] {EditContactMenuItem   ,DeleteContactMenuItem
                        ,new MenuItem ("-"),AddContactMenuItem });

                        listViewContact.ContextMenu = new ContextMenu(MenuItemList.ToArray());


                    }
                    else
                    {

                        MenuItem[] mi1 = new MenuItem[] { AddContactMenuItem };
                        listViewContact.ContextMenu = new ContextMenu(mi1);
                        ;

                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewContact_MouseDown:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void AddSource_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ContactAddForm p = new ContactAddForm(DB);
                DialogResult d = p.ShowDialog();
                if (d == DialogResult.OK)
                {
                    _ContactList = (new ContactSQL(DB).GetContactList());
                    FillContacts(_ContactList);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddSource_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void OpenSource_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Contact contact = new ContactSQL(DB).GetContactInforBYID(
               Convert.ToUInt32(listViewContact.SelectedItems[0].Name));
                ContactForm ContactForm_ = new ContactForm(DB, contact);
                DialogResult d = ContactForm_.ShowDialog();
                if (d == DialogResult.OK)
                {
                    _ContactList = (new ContactSQL(DB).GetContactList());
                    FillContacts(_ContactList);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("OpenSource_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void EditSource_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Contact contact = new ContactSQL(DB).GetContactInforBYID(
                Convert.ToUInt32(listViewContact.SelectedItems[0].Name));
                ContactAddForm p = new ContactAddForm(DB, contact);
                DialogResult d = p.ShowDialog();
                if (d == DialogResult.OK)
                {
                    _ContactList = (new ContactSQL(DB).GetContactList());
                    FillContacts(_ContactList);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("EditSource_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void DeleteSource_MenuItem_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ee)
            {
                MessageBox.Show("DeleteSource_MenuItem_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DialogResult d = MessageBox.Show("هل انت متاكد من الحذف؟", "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (d == DialogResult.OK)
            {
                ContactSQL contactsql = new ContactSQL(DB);
                for (int i=0;i<listViewContact .SelectedItems .Count;i++ )
                    contactsql.DeleteContact(
                    Convert.ToUInt32(listViewContact.SelectedItems[i].Name));
                 _ContactList = (new ContactSQL(DB).GetContactList());
                FillContacts(_ContactList);
            }
        }
       
        private void listViewContact_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (buttonSelect.Visible == true)
                {

                    if (listViewContact.SelectedItems.Count != 1)
                        MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        uint contactid = Convert.ToUInt32(listViewContact.SelectedItems[0].Name);
                        Contact_ = new ContactSQL(DB).GetContactInforBYID(contactid);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else if (e.Button == MouseButtons.Left && listViewContact.SelectedItems.Count > 0)
                {

                    ContactForm contactform = new ContactForm(DB, new ContactSQL(DB).GetContactInforBYID(Convert.ToUInt32(listViewContact.SelectedItems[0].Name)));
                    DialogResult d = contactform.ShowDialog();
                    if (d == DialogResult.OK)
                    {
                        _ContactList = (new ContactSQL(DB).GetContactList());
                        FillContacts(_ContactList);
                    }



                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewContact_MouseDoubleClick:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void listViewContact_Resize(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < listViewContact.Columns.Count; i++)
                {
                    listViewContact.Columns[i].Width = (listViewContact.Width - 2) / listViewContact.Columns.Count;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("listViewContact_Resize:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewContact.SelectedItems.Count != 1)
                    MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    uint contactid = Convert.ToUInt32(listViewContact.SelectedItems[0].Name);
                    Contact_ = new ContactSQL(DB).GetContactInforBYID(contactid);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonSelect_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             //_ContactList = (new ContactSQL(DB).GetContactList());
            FillContacts(_ContactList);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSearch.Text.Length > 0)
                {
                    _ContactList = (new ContactSQL(DB).SearchContact(textBoxSearch.Text));
                    FillContacts(_ContactList);

                }
                else
                {
                    _ContactList = (new ContactSQL(DB).GetContactList());
                    FillContacts(_ContactList);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("textBoxSearch_TextChanged:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
    }
}
