using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorClient
{
    /// <summary>
    /// 保存配置信息
    /// </summary>
    public class ConfigHelper
    {
        public static string serverIP = System.Configuration.ConfigurationManager.AppSettings["serverIp"].ToString();

        public static int serverPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["serverPort"]);

        public static int generateType = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["generateType"]);

        public static int headWarn = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["headWarn"]);

        public static int branchWarn = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["branchWarn"]);

        public static int boxWarn = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["boxWarn"]);
        public static int thresholdValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["thresholdValue"]);
        public static int headNumber = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["headNumber"]);


    }
}
