using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OverLoad_Server_Kernal
{
    public partial class OverLoad_Server_Kernal : ServiceBase
    {

        private DatabaseInterface DB;
        private int BroadCast_UDP_Port;
        private int TCP_OPORT;
        private Timer Timer_;
        UdpClient UdpClient_;
        byte[] UDP_Packet;
        private  BackgroundWorker TCP_Listening;
        private TcpListener _TcpListner;
        private OverLoadServer _OverLoadServer;
        private List<OverLoadServer_EndPoint> OverLoadServer_EndPointList;

        public OverLoad_Server_Kernal()
        {
            InitializeComponent();
      
 
            this.EventLog.Source = this.ServiceName;
            this.EventLog.Log = "Application";
           

           



        }

        protected override void OnStart(string[] args)
        {
   

          
            try
            {

                OverLoadServer_EndPointList = new List<OverLoadServer_EndPoint>();
                UdpClient_ = new UdpClient();
                TCP_Listening = new BackgroundWorker();
                TCP_Listening.DoWork += Server_Listner_TCP;
                Timer_ = new Timer();
                Timer_.Interval = 3000;
                Timer_.Elapsed += Timer_Elapsed;
              
                string servername = args[1];
                DB = ConnectDataBase(args[0], servername);
                TCP_OPORT = Convert.ToInt32(args[2]);
                 BroadCast_UDP_Port = Convert.ToInt32(args[3]);
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
                //IPEndPoint[] udpConnInfoArray = ipGlobalProperties.GetActiveUdpListeners();
                foreach (IPEndPoint endpoint in tcpConnInfoArray)
                {
                    if (endpoint.Port == TCP_OPORT)
                    {
                        throw new Exception("المنفذ TCP مستخدم");

                    }
                }
                //foreach (IPEndPoint endpoint in udpConnInfoArray)
                //{
                //    if (endpoint.Port == BroadCast_UDP_Port)
                //    {
                //        throw new Exception("المنفذ UDP  مستخدم");

                //    }
                //}

                //IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, TCP_OPORT);
                _TcpListner = new TcpListener(localEndPoint);

                _OverLoadServer = new OverLoadServer(System.Environment.MachineName, servername, (IPEndPoint)_TcpListner.LocalEndpoint );
                 UDP_Packet = BuildUdpPacket(_OverLoadServer.ComputerName, _OverLoadServer.ServerNAme, _OverLoadServer.Server);



                Timer_.Start();
                TCP_Listening.RunWorkerAsync();

            }
            catch (Exception ee)
            {
                EventLog.WriteEntry("OverLoad_Server_Kernal_ERROR_ONSTART:"+ee.Message );
                OnStop();
            }
        }

        protected override void OnStop()
        {
            Timer_.Stop();
            DisposeComponents();
        }
        private async void DisposeComponents()
        {
            for (int i=0;i<OverLoadServer_EndPointList.Count;i++)
            {
                try
                {

                    OverLoadServer_EndPointList[i].Dispose();
                }catch
                {

                }
            }
            _TcpListner.Stop ( );
            TCP_Listening.CancelAsync();
        }
        private async  void Monitor_Connections()
        {
            for (int i = 0; i < OverLoadServer_EndPointList.Count; i++)
            {
                try
                {
                    if (!OverLoadServer_EndPointList[i].WorkStation .Connected )
                    OverLoadServer_EndPointList[i].Dispose();
                }
                catch
                {

                }
            }
        }
        private void Timer_Elapsed(object sender, EventArgs e)
        {

            try
            {
                List<byte> UDP_Packet_Final = new List<byte>();
                if (GetActiveTcpConnecasons ()<2)
                {
                    UDP_Packet_Final.Add(0xff);
                    UDP_Packet_Final.AddRange(UDP_Packet);
                }
                else
                {
                    UDP_Packet_Final.Add(0xf0);
                    UDP_Packet_Final.AddRange(UDP_Packet);
                }
                UdpClient_.Send(UDP_Packet_Final.ToArray (), UDP_Packet_Final.Count , "255.255.255.255", BroadCast_UDP_Port);
                Monitor_Connections();

            }
            catch (Exception ee)
            {
                this.EventLog.WriteEntry("OverLoad_Server_Kernal_ERROR_UDP_BROADCAST:" + ee.Message, EventLogEntryType.Error );
                OnStop();
            }


        }
        public void Server_Listner_TCP(object sender, DoWorkEventArgs e)
        {
            try
            {
                //EventLog.WriteEntry("OverLoad_Server_Kernal__Server_Listner_TCP",EventLogEntryType.Information) ;

                _TcpListner.Start();

                while (true)
                {

                    TcpClient Client_ = null;
                        Client_ = _TcpListner.AcceptTcpClient();

                    //Refreshconnections(Client_);
                    OverLoadServer_EndPointList.Add(new OverLoadServer_EndPoint(Client_, DB));

                    while (GetActiveTcpConnecasons ()>1)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }

                }
            }
            catch(Exception ee)
            {
                EventLog.WriteEntry("OverLoad_Server_Kernal_Server_Listner_TCP_Error:" + ee.Message, EventLogEntryType.Error );
                OnStop();
            }

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
        public static DatabaseInterface ConnectDataBase(string path,string servername)
        {
            DatabaseInterface DB = new DatabaseInterface(path,servername );
            try
            {
                DB.Open_Connection ();
                if (DB.Get_Connection_State () == System.Data.ConnectionState.Open) DB.Close_Connection ();
                return new DatabaseInterface(path,servername );
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }

        public List<IPAddress> GetLocalIPv4()
        {
            List<IPAddress> IpAddressList = new List<IPAddress>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            IpAddressList.Add(ip.Address);
                        }
                    }
                }
            }
            return IpAddressList;
        }
        public byte[] BuildUdpPacket(string computername, string servername, IPEndPoint server)
        {

            if (servername.Length > 254) return null;
            List<byte> packet = new List<byte>();
           
            byte[] computername_bytes = Encoding.UTF8.GetBytes(computername);
            packet.Add(Convert.ToByte(computername_bytes.Length));
            packet.AddRange(computername_bytes);

            byte[] servername_bytes = Encoding.UTF8.GetBytes(servername);
            packet.Add(Convert.ToByte(servername_bytes.Length));
            packet.AddRange(servername_bytes);
            

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            bf.Serialize(ms, server);
            ms.Close();

            byte[] server_Bytes = ms.ToArray();
            byte[] server_Bytes_Length = BitConverter.GetBytes(server_Bytes.Length);

            packet.Add(Convert.ToByte(server_Bytes_Length.Length));
            packet.AddRange(server_Bytes_Length);
            packet.AddRange(server_Bytes);

            return (packet.ToArray());

        }
        private int  GetActiveTcpConnecasons()
        {
            int count = 0;
            try
            {

                //listViewClients.Items.Clear();
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
                foreach (TcpConnectionInformation c in connections)
                {
                    if (c.LocalEndPoint.Port ==((IPEndPoint ) _TcpListner.LocalEndpoint ).Port)
                    {
                        count += 1;
                    }
                }
                return count;
             
            }
            catch
            {
                return -1;
            }


        }
    }
}
