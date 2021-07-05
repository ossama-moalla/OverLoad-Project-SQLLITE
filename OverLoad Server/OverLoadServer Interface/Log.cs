using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoadServer_Interface
{
    public partial class Log : Form
    {
        private List<LogMessage> msglist;
        private List<LogMessage> Tmpmsglist;
        System.ComponentModel.BackgroundWorker Compilemessages;
        bool Accessmsglist;
        private int sortColumn = -1;

        public Log(string title)
        {
            InitializeComponent();
            Accessmsglist = false;
            this.Text = title;
   
            msglist = new List<LogMessage>();
            Tmpmsglist = new List<LogMessage>();
            Compilemessages = new System.ComponentModel.BackgroundWorker();
            Compilemessages.DoWork += RefreshBuffer;
            Compilemessages.RunWorkerAsync();



        }

        private void RefreshBuffer(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(100);
                Accessmsglist = false;
                for(int i=0;i<Tmpmsglist.Count;i++)
                {
                    while(!Tmpmsglist [i].AddedToList) { }
                    msglist.Add(Tmpmsglist[i]);
                }
                Tmpmsglist.Clear();
                Accessmsglist = true ;
                for (int i = 0; i < msglist.Count; i++)
                {
                    AddText(msglist[i]);
                   
                }
                msglist.Clear();
            }
        }
        private void AddText(LogMessage lm)
        {
            if (listView1.InvokeRequired)
            {

                listView1.Invoke(new Action(() => AddText(lm)));
            }
            else
            {
                try
                {
                    ListViewItem item = new ListViewItem(lm.msgtime.ToString("HH:mm:ss.fff"));
                    item.SubItems.Add(lm.msg);
                    listView1.Items.Add(item);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("server log -add text" + ee.Message);
                }

            }
        }
        public   void addlog(DateTime tt, string msg)
        {
            while (!Accessmsglist )
            {

            }
            LogMessage logm = new LogMessage(tt, msg);
            Tmpmsglist .Add(logm);
            logm.AddedToList = true;
        }
        public class LogMessage

        {
            public DateTime msgtime;
            public string msg;
             public bool AddedToList;
            public LogMessage(DateTime t, string m)
            {
                AddedToList = false;
                msgtime = t;
                msg = m;
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.  
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.  
                sortColumn = e.Column;
                // Set the sort order to ascending by default.  
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.  
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }
            // Call the sort method to manually sort.  
            listView1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer  
            // object.  
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              listView1.Sorting);
        }
        /// <summary>
        /// This class is an implementation of the 'IComparer' interface.
        /// </summary>
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                int returnVal;
                // Determine whether the type being compared is a date type.  
                try
                {
                    // Parse the two objects passed as a parameter as a DateTime.  
                    System.DateTime firstDate =
                            DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
                    System.DateTime secondDate =
                            DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
                    // Compare the two dates.  
                    returnVal = DateTime.Compare(firstDate, secondDate);
                }
                // If neither compared object has a valid date format, compare  
                // as a string.  
                catch
                {
                    // Compare the two items as a string.  
                    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                ((ListViewItem)y).SubItems[col].Text);
                }
                // Determine whether the sort order is descending.  
                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.  
                    returnVal *= -1;
                return returnVal;
            }
        }
    }
}
