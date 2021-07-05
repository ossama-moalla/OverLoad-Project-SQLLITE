using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OverLoad_Server_Kernal
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            string comand= @"/c  sc sidtype OverLoad_Server_Kernal unrestricted & netsh advfirewall firewall add rule name = ""OverLoad_Service_IN"" dir =in action = allow service = ""OverLoad_Server_Kernal"" enable = yes profile = any protocol = any localip = any remoteip = any & netsh advfirewall firewall add rule name = ""OverLoad_Service_OUT"" dir =out action = allow service = ""OverLoad_Server_Kernal"" enable = yes profile = any protocol = any localip = any remoteip = any";
            System.Diagnostics.Process.Start("CMD.exe", comand);


        }
        public override void Uninstall(IDictionary savedState)
        {

            base.Uninstall(savedState);
            try
            {
                string comand = @"/c  netsh advfirewall firewall delete rule name =  ""OverLoad_Service_IN""  & netsh advfirewall firewall delete rule name =  ""OverLoad_Service_OUT"" ";
                System.Diagnostics.Process.Start("CMD.exe", comand);

            }
            catch
            {

            }
            //base.Commit(savedState);


        }

    }
}
