using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace OverLoad_Client
{
    [RunInstaller(true)]
    public partial class OverLoad__Installer : System.Configuration.Install.Installer
    {
        public OverLoad__Installer()
        {
            InitializeComponent();
        }
    }
}
