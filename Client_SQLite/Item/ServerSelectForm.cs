

using OverLoad_Client.OverLoadClientNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoad_Client
{
    public partial class ServerSelectForm : Form
    {
        public string ServerName { get; set; }
        private System.Windows.Forms.Timer Timer_;
        private readonly BackgroundWorker Udp_Listening;
        private Thread thread;
        UdpClient UdpClient_;
        List<ServersMonitor > serverslist;
       
        private TcpClient Local;
    
        public TcpClient Local_
        {
            get
            {
                return Local;
            }
        }
        public ServerSelectForm()
        {
            try
            {
                serverslist = new List<ServersMonitor>();

                Udp_Listening = new BackgroundWorker();


                InitializeComponent();
                Timer_ = new System.Windows.Forms.Timer();
                Timer_.Interval = 2000;
                Timer_.Tick += new EventHandler(Timer_Tick);
                textBoxUDP_Port.Text = ProgramGeneralMethods.Registry_Get_UDP_Port_Listner().ToString();

                //Udp_Listening.DoWork += getServer;
                //Udp_Listening.WorkerSupportsCancellation = true;
                //thread = new Thread(new ThreadStart(GetUDP_Packets));
            }
            catch (Exception ee)
            {
                throw new Exception("ServerSelectForm:" + ee.Message);
            }
           
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                bool changed = false;
                textBox_IP.Text = GetLocalIPAddress();
                for (int i = 0; i < serverslist.Count; i++)
                {
                    if ((DateTime.Now - serverslist[i].LastSeen).Seconds > 5)
                    {

                        serverslist.Remove(serverslist[i]);
                        changed = true;
                    }

                }
                if (listView1.Items.Count != serverslist.Count)
                    changed = true;

                if (changed)
                {
                    listView1.Items.Clear();
                    for (int i = 0; i < serverslist.Count; i++)
                    {

                        ListViewItem item = new ListViewItem(serverslist[i].OVServer.ComputerName);

                        item.SubItems.Add(serverslist[i].OVServer.ServerNAme.ToString());

                        item.SubItems.Add(serverslist[i].OVServer.Server.Address.ToString());
                        item.SubItems.Add(serverslist[i].OVServer.Server.Port.ToString());
                        if (serverslist[i].Available)
                        {
                            item.Name = "A" + i;
                            item.SubItems.Add("متوفر");
                            item.BackColor = Color.LimeGreen;
                        }
                        else
                        {
                            item.Name = "N" + i;
                            item.SubItems.Add("تم بلوغ عدد الطرفيات المتصلة الاعظمي");
                            item.BackColor = Color.Orange;
                        }
                        AddItem_2(listView1, item);
                    }
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Timer_Tick:" + ee.Message);
            }
         
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {

           
           


        }
        public static string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception ee)
            {
                throw new Exception("GetLocalIPAddress:" + ee.Message);
            }

        }
        //public void getServer(object sender, DoWorkEventArgs e)
        //{

        //    while (e.Cancel !=true  )
        //    {
        //        try
        //        {
        //            IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);



        //            byte[] recData = UdpClient_.Receive(ref anyIP);
        //            if (recData == null || recData.Length == 0)
        //                return;
        //            if (recData[0] == 0xff)
        //            {

        //                try
        //                {

        //                    int servername_bytesLength = Convert.ToInt32(recData[1]);
        //                    byte[] servername_bytes = new byte[servername_bytesLength];
        //                    Array.Copy(recData, 2, servername_bytes, 0, servername_bytes.Length);

        //                    int Server_bytes_Length_Int = Convert.ToInt32(recData[2 + servername_bytesLength]);
        //                    byte[] Server_bytes_Length_Array = new byte[Server_bytes_Length_Int];
        //                    Array.Copy(recData, 3 + servername_bytes.Length, Server_bytes_Length_Array, 0, Server_bytes_Length_Array.Length);
        //                    int Server_bytes_Length = BitConverter.ToInt32(Server_bytes_Length_Array, 0);

        //                    byte[] ServerBytes = new byte[Server_bytes_Length];
        //                    Array.Copy(recData, 3 + servername_bytes.Length + Server_bytes_Length_Int, ServerBytes, 0, ServerBytes.Length);


        //                    string ServerName = System.Text.Encoding.UTF8.GetString(servername_bytes, 0, servername_bytes.Length);
        //                    MemoryStream mss = new MemoryStream();
        //                    BinaryFormatter bf = new BinaryFormatter();
        //                    bf.AssemblyFormat = FormatterAssemblyStyle.Simple;

        //                    mss.Write(ServerBytes, 0, ServerBytes.Length);
        //                    mss.Position = 0;
        //                    IPEndPoint IPEndPoint_;
        //                    try
        //                    {
        //                        IPEndPoint_ = (IPEndPoint)bf.Deserialize(mss);
        //                    }
        //                    catch
        //                    {
        //                        continue;
        //                    }

        //                    OverLoadServer ov = new OverLoadServer(ServerName, IPEndPoint_);
        //                    int ind = -1;
        //                    for (int i = 0; i < serverslist.Count; i++)
        //                    {


        //                        if (serverslist[i].OVServer.ServerNAme == ov.ServerNAme &&
        //                           serverslist[i].ServerIP.ToString() == ov.Server.Address.ToString() &&
        //                            serverslist[i].OVServer.Server.Port == ov.Server.Port)
        //                        { ind = i; break; }

        //                    }

        //                    if (ind == -1)
        //                    {
        //                        serverslist.Add(new ServersMonitor(ov, anyIP.Address, DateTime.Now));



        //                    }
        //                    else
        //                    {
        //                        serverslist[ind].LastSeen = DateTime.Now;
        //                    }

        //                }
        //                catch (Exception ee)
        //                {
        //                    MessageBox.Show(ee.Message);
        //                }


        //            }
        //        }
        //        catch
        //        {

        //        }
               



        //    }
        //}

        public void GetUDP_Packets()
        {
            try
            {
                UdpClient_ = new UdpClient(Convert.ToInt32(textBoxUDP_Port.Text));
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    try
                    {


                        byte[] recData = UdpClient_.Receive(ref anyIP);
                        //if (recData == null || recData.Length == 0)
                        //    return;
                        if (recData[0] == 0xff || recData[0] == 0xf0)
                        {

                            try
                            {
                                bool Available = recData[0] == 0xff ? true : false;
                                int computername_bytesLength = Convert.ToInt32(recData[1]);
                                byte[] computername_bytes = new byte[computername_bytesLength];
                                Array.Copy(recData, 2, computername_bytes, 0, computername_bytes.Length);

                                int servername_bytesLength = Convert.ToInt32(recData[2 + computername_bytesLength]);
                                byte[] servername_bytes = new byte[servername_bytesLength];
                                Array.Copy(recData, 3 + computername_bytesLength, servername_bytes, 0, servername_bytes.Length);

                                int Server_bytes_Length_Int = Convert.ToInt32(recData[3 + computername_bytesLength + servername_bytesLength]);
                                byte[] Server_bytes_Length_Array = new byte[Server_bytes_Length_Int];
                                Array.Copy(recData, 4 + servername_bytesLength + computername_bytesLength, Server_bytes_Length_Array, 0, Server_bytes_Length_Array.Length);
                                int Server_bytes_Length = BitConverter.ToInt32(Server_bytes_Length_Array, 0);

                                byte[] ServerBytes = new byte[Server_bytes_Length];
                                Array.Copy(recData, 4 + servername_bytesLength + computername_bytesLength + Server_bytes_Length_Int, ServerBytes, 0, ServerBytes.Length);

                                string ComputerName = System.Text.Encoding.UTF8.GetString(computername_bytes, 0, computername_bytes.Length);


                                string ServerName = System.Text.Encoding.UTF8.GetString(servername_bytes, 0, servername_bytes.Length);
                                MemoryStream mss = new MemoryStream();
                                BinaryFormatter bf = new BinaryFormatter();
                                bf.AssemblyFormat = FormatterAssemblyStyle.Simple;

                                mss.Write(ServerBytes, 0, ServerBytes.Length);
                                mss.Position = 0;
                                IPEndPoint IPEndPoint_;
                                try
                                {
                                    IPEndPoint_ = (IPEndPoint)bf.Deserialize(mss);
                                }
                                catch
                                {
                                    continue;
                                }

                                OverLoadServer ov = new OverLoadServer(ComputerName, ServerName, IPEndPoint_);
                                int ind = -1;
                                for (int i = 0; i < serverslist.Count; i++)
                                {

                                    if (serverslist[i].OVServer.ServerNAme.SequenceEqual(ov.ServerNAme) &&
                                       serverslist[i].OVServer.Server.Address.Equals(ov.Server.Address) &&
                                        serverslist[i].OVServer.Server.Port == ov.Server.Port)
                                    { ind = i; break; }

                                }

                                if (ind == -1)
                                {
                                    serverslist.Add(new ServersMonitor(ov, anyIP.Address, DateTime.Now, Available));



                                }
                                else
                                {
                                    serverslist[ind].LastSeen = DateTime.Now;
                                    serverslist[ind].Available = Available;
                                }

                            }
                            catch (Exception ee)
                            {
                                MessageBox.Show(ee.Message);
                            }


                        }
                    }
                    catch
                    {

                    }




                }
            }
            catch (Exception ee)
            {
               if(thread.ThreadState==ThreadState.Running )
                    throw new Exception("GetUDP_Packets:" + ee.Message);
            }
           
        }

        private void AddItem_2(ListView listview, ListViewItem item)
        {
            try
            {
                // If the current thread is not the UI thread, InvokeRequired will be true
                if (listview.InvokeRequired)
                {
                    // If so, call Invoke, passing it a lambda expression which calls
                    // UpdateText with the same label and text, but on the UI thread instead.
                    listview.Invoke((Action)(() => AddItem_2(listview, item)));
                    return;
                }
                // If we're running on the UI thread, we'll get here, and can safely update 
                // the label's text.
                listview.Items.Add(item);
            }
            catch(Exception ee)
            {
                throw new Exception("AddItem_2:" + ee.Message);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && listView1.SelectedItems.Count > 0)
                {
                    Timer_.Stop();
                    try
                    {
                        if (listView1.SelectedItems[0].Name.Substring(1) == "N") throw new Exception("تم بلوغ العدد الأعظمي للطرفيات المتصلة");
                        this.Local = new TcpClient();
                        this.Local.Connect(new IPEndPoint(IPAddress.Parse(listView1.SelectedItems[0].SubItems[2].Text)
                            , Convert.ToInt32(listView1.SelectedItems[0].SubItems[3].Text)));

                        this.ServerName = listView1.SelectedItems[0].SubItems[1].Text;
                        ProgramGeneralMethods.Registry_Set_UDP_Port_Listner(Convert.ToInt32(textBoxUDP_Port.Text));
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ee)
                    {
                        Timer_.Start();
                        MessageBox.Show("Error:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            catch (Exception ee)
            {
                throw new Exception("listView1_MouseDoubleClick:" + ee.Message);
            }
           
        }

        private void button_StartListen_Click(object sender, EventArgs e)
        {
            try
            {
                if (button_StartListen.Name == "button_StartListen")
                {
                    try
                    {

                        thread = null;
                        thread = new Thread(GetUDP_Packets);
                        thread.IsBackground = true;
                        // Then restart the stopped Thread
                        thread.Start();
                        //Udp_Listening.RunWorkerAsync();
                        Timer_.Start();
                        textBoxUDP_Port.Enabled = false;
                        button_StartListen.Name = "button_StopListen";
                        button_StartListen.Text = "ايقاف التنصت";
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("خطأ:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    try
                    {

                        UdpClient_.Close();
                        while (thread.IsAlive)
                        {
                            button_StartListen.Enabled = false;
                            thread.Abort();
                        }
                        button_StartListen.Enabled = true;
                        //Udp_Listening.CancelAsync  ();
                        //Udp_Listening.Dispose();
                        Timer_.Stop();
                        textBoxUDP_Port.Enabled = true;
                        button_StartListen.Name = "button_StartListen";
                        button_StartListen.Text = " بدء التنصت";
                        serverslist.Clear();
                        listView1.Items.Clear();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("خطأ:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ee)
            {
                throw new Exception("button_StartListen_Click:" + ee.Message);
            }
         
        }



        private void buttonConnect_Click(object sender, EventArgs e)
        {

            try
            {
                this.Local = new TcpClient();
                this.Local.Connect(new IPEndPoint(IPAddress.Parse(textBoxIP.Text )
                    , Convert.ToInt32(textBoxport.Text )));

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ee)
            {
                Timer_.Start();
                MessageBox.Show("buttonConnect_Click:" + ee.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 
    }
 
   
    
}
