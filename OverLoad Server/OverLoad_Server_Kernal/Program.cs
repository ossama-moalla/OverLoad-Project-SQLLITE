using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OverLoad_Server_Kernal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] _OverLoad_Server_Kernal;
            _OverLoad_Server_Kernal = new ServiceBase[]
            {
                new OverLoad_Server_Kernal()
            };
            ServiceBase.Run(_OverLoad_Server_Kernal);
        }
    }
}
