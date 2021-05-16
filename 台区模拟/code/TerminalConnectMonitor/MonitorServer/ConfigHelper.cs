using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServer
{
    public class ConfigHelper
    {
        public static string serverIP = System.Configuration.ConfigurationManager.AppSettings["serverIp"].ToString();

        public static int serverPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["serverPort"]); 

    }
}
