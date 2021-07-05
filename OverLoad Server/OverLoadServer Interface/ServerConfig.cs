using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverLoadServer_Interface
{
    public partial class ServerConfig : Form
    {
        private Timer ServiceStatusTimmer;
        private Socket  _TcpListener;
        public Socket TcpListener_
        {
            get
            {
                return _TcpListener;
            }
        }
        ServiceController sc;
        public ServerConfig()
        {
            InitializeComponent();
            sc = new ServiceController();
            sc.ServiceName = "OverLoad_Server_Kernal";
            ServiceStatusTimmer = new Timer();
            ServiceStatusTimmer.Interval = 1000;
            ServiceStatusTimmer.Tick += Timer_Trik;
            ServiceStatusTimmer.Start();
            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

            var reg = localMachine.OpenSubKey("Software\\OverLoadServer_1.0.0", true);
            if (reg != null)
            {
                textBoxDatabasePath.Text=(string)reg.GetValue ("DataBase_Location");
                textBoxServerName.Text= (string)reg.GetValue ("Server_Name");
                textBox_IP .Text = (string)reg.GetValue("Server_IP_Address");
                textBoxTCPPort.Text= (string)reg.GetValue("Server_TCP_Port");
                textBoxUDP_Port.Text= (string)reg.GetValue("Server_UDP_Port");
            }
            
            }

        private void Timer_Trik(object sender, EventArgs e)
        {
            sc.Refresh();
            try
            {


               
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    textBox_IP.Text = GetLocalIPAddress();
                    label5.Text = "الخدمة متوقفة";
                    label5.BackColor = Color.Orange ;
                    panel2.Enabled = true ;
                    if (buttonStart.Enabled == false)
                        buttonStart.Enabled = true;
                    buttonStart.Name = "buttonStart";
                    buttonStart.Text = "تشغيل";
                    
                }
                else if(sc.Status == ServiceControllerStatus.Running )
                {
                    label5.Text = "الخدمة تعمل";
                    label5.BackColor = Color.LightGreen;
                    panel2.Enabled = false;
                    if (buttonStart.Enabled == false)
                        buttonStart.Enabled = true;
                    buttonStart.Name = "buttonStop";
                    buttonStart.Text = "ايقاف";
  
                }
                else 
                {
                    label5.Text = sc.Status.ToString ();
                    label5.BackColor = Color.Turquoise ;
                    buttonStart.Enabled  =false ;

                }


            }
            catch(Exception ee)
            {
                label5.Text = "Error:" +ee.Message;

            }
            ShowActiveTcpConnecasons();
        }

        public static bool ConnectDataBase(string path)
        {
            DatabaseInterface DB = new DatabaseInterface(path);
            try
            {
                DB.Open_Connection();
                if (DB.Get_Connection_State() == System.Data.ConnectionState.Open)
                {
                    DataTable t = new DataTable();
                    t = DB.GetData("select * from OverLoad_Log ");
                    DB.Close_Connection();
                    return true;
                }
                else throw new Exception("فشل الاتصال بقاعدة البيانات");
            }
            catch (Exception ee)
            {
                if (ee.Message.Substring(0, 22).ToLower().SequenceEqual("file is not a database")) throw new Exception("قاعدة بيانات غير صحيحة");
                else
                    throw new Exception ("ConnectDataBase:"+ee.Message);
            }
        }
        public async  void saveSettingToRegesitry()
        {
            try
            {
                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default); //here you specify where exactly you want your entry

                var reg = localMachine.OpenSubKey("Software\\OverLoadServer_1.0.0", true);
                if (reg == null)
                {
                    reg = localMachine.CreateSubKey("Software\\OverLoadServer_1.0.0");
                }

                reg.SetValue("DataBase_Location", textBoxDatabasePath.Text);
                reg.SetValue("Server_Name", textBoxServerName.Text);
                reg.SetValue("Server_IP_Address", textBox_IP.Text);
                reg.SetValue("Server_TCP_Port", textBoxTCPPort.Text);
                reg.SetValue("Server_UDP_Port", textBoxUDP_Port.Text);
                //if (reg.GetValue("someKey") == null)
                //{
                //    reg.SetValue("someKey", "someValue");
                //}
            }
            catch (Exception ee)
            {
                MessageBox.Show("saveSettingToRegesitry"+ee.Message );
            }
            
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonStart.Name == "buttonStart")
                {
                    if (textBoxDatabasePath.Text.Length == 0)
                    {
                        MessageBox.Show("يرجى تحديد قاعدة البيانات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!ConnectDataBase(textBoxDatabasePath.Text))
                    {

                        MessageBox.Show("فشل الاتصال بقاعدة البيانات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (textBoxServerName.Text.Length == 0)
                    {
                        MessageBox.Show("يرجى تحديد اسم السيرفر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    try
                    {
                        uint p = Convert.ToUInt32(textBoxTCPPort.Text);
                        //if (p > 60000 || p < 1024)
                        //{
                        //    MessageBox.Show("رقم المنفذ يجب ان اكبر من 1024 و اصغر من 60000", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                    }
                    catch
                    {
                        throw new Exception ("رقم المنفذ يجب ان يكون عدد صحيح");
                    }
                    try
                    {
                        uint tcp_p = Convert.ToUInt32(textBoxTCPPort.Text);
                        uint udp_p = Convert.ToUInt32(textBoxUDP_Port.Text);
                        if (udp_p > 60000 || udp_p < 1024 || tcp_p > 60000 || tcp_p < 1024)
                        {
                            throw new Exception("رقم المنفذ يجب ان اكبر من 1024 و اصغر من 60000");
                        }
                    }
                    catch
                    {
                        throw new Exception("رقم المنفذ يجب ان يكون عدد صحيح");
                    }

                    try
                    {
                        string[] arg = new string[] {textBoxDatabasePath.Text
                ,textBoxServerName .Text ,textBoxTCPPort.Text ,textBoxUDP_Port.Text };
                        if (ServiceController.GetServices()
        .FirstOrDefault(s => s.ServiceName == sc.ServiceName) == null)
                            throw new Exception("الخدمة غير منصبة , يجب اعادة تنصيب السيرفر");
                        sc.Start(arg);
                        saveSettingToRegesitry();

                    }
                    catch (Exception ee)
                    {
                        throw new Exception(ee.Message);
                    }

                }
                else
                {
                        if (sc.Status != ServiceControllerStatus.Stopped)
                            try
                            {
                                sc.Stop();
                            }
                            catch (Exception ee)
                            {
                                throw new Exception(ee.Message);
                            }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("buttonNext_Click:" + ee.Message ,"",MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
          
           
        }
        public bool serviceExists(string ServiceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(ServiceName));
        }
    


public static string GetLocalIPAddress()
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
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            try
            {
                filedialog.InitialDirectory = textBoxDatabasePath.Text;
            }
            catch
            {
                filedialog.InitialDirectory = Application.StartupPath;
            }
            
            filedialog.Filter = "SQLite DataBase(*.db)|*.db";
            DialogResult d = filedialog.ShowDialog();
            if (d == DialogResult.OK)
                textBoxDatabasePath.Text = filedialog.FileName;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void ShowActiveTcpConnecasons()
        {
            try
            {
                //listViewClients.Items.Clear();
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
                List<ListViewItem> itemslist = new List<ListViewItem>();
                foreach (TcpConnectionInformation c in connections)
                {
                    if (c.LocalEndPoint.Port ==Convert.ToInt32 ( textBoxTCPPort.Text ))
                    {
                        ListViewItem ListViewItem_ = new ListViewItem(c.RemoteEndPoint.Address.ToString());
                        ListViewItem_.Name = c.RemoteEndPoint.Address.ToString() + c.RemoteEndPoint.Port.ToString();
                        ListViewItem_.SubItems.Add(c.RemoteEndPoint.Port.ToString());
                        ListViewItem_.SubItems.Add(c.State.ToString());
             
                        itemslist.Add(ListViewItem_);
                        //if (c.State == TcpState.CloseWait || c.State == TcpState.Closing)
                        //{

                        //    IPEndPoint ac = (IPEndPoint)c.RemoteEndPoint;
                        //    for (int i = 0; i < OverLoadEndPointList.Count; i++)
                        //    {
                        //        //if(OverLoadEndPointList[i].workSocket .Connected )
                        //        IPEndPoint ipe = (IPEndPoint)OverLoadEndPointList[i].WorkStation.RemoteEndPoint;

                        //        if (ipe.Address.ToString() == ac.Address.ToString())
                        //        {
                        //            OverLoadEndPointList[i].Disposed = true;
                        //            OverLoadEndPointList[i].WorkStation.Shutdown(SocketShutdown.Both);
                        //            OverLoadEndPointList[i].WorkStation.Dispose();
                        //            OverLoadEndPointList.Remove(OverLoadEndPointList[i]);
                        //            //Refreshconnections(null);

                        //        }
                        //    }
                        //}
                        //bool found=false ;
                        //foreach(ListViewItem ListViewItem_ in listViewClients.Items  )
                        //{
                        //    if (ListViewItem_.Name == item.Name)
                        //    {
                        //        found = true;
                        //        break;
                        //    }
                        //}

                        //if (!found ) 
                        // listViewClients.Items.Add(item);
                    }
                }
                for(int i=0; i<itemslist .Count;i++)
                {
                    bool found = false;
                    for(int j=0;j<listViewClients .Items .Count;j++)
                    {
                        if(itemslist [i].Name ==listViewClients .Items [j].Name )
                        { found = true;break; }
                    }
      
                    if (!found )
                        listViewClients.Items.Add(itemslist[i]);

                }
                for (int i = 0; i < listViewClients.Items.Count; i++)
                {

                    bool found = false;
                    for (int j = 0; j < itemslist .Count; j++)
                    {
                        if (  listViewClients.Items[i].Name== itemslist[j].Name)
                        { found = true ; break; }
                    }
                    if (!found )
                        listViewClients.Items.RemoveAt(i );

                }
            }
            catch
            {

            }


        }

 
    }

}

