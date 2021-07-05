using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


    namespace OverLoadServer_Interface
{
    
        public class OverLoadServer
        {
            private string _ServerName;
            IPEndPoint _Server;
            public string ServerNAme
            {
                get { return _ServerName; }
            }
            public IPEndPoint Server
            {
                get { return _Server; }
            }


            public OverLoadServer(string ServerName__, IPEndPoint Server_)
            {
                _ServerName = ServerName__;
                _Server = Server_;
            }
        }
        public static class ServerMethods
        {
            public static List<IPAddress> GetLocalIPv4()
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
            public static byte[] BuildUdpPacket(string servername, IPEndPoint server)
            {
                if (servername.Length > 254) return null;
                List<byte> packet = new List<byte>();
                packet.Add(0xff);
                byte[] servername_bytes = Encoding.ASCII.GetBytes(servername);
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


        }
    }

