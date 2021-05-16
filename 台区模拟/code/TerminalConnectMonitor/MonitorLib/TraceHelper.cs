using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib
{
    public class TraceHelper
    {
        private static ILog log;
        static TraceHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            //创建日志记录组件实例
              log = log4net.LogManager.GetLogger(typeof(TraceHelper));
        }
        public static void TraceInfo(string I_Trace)
        {
            System.Diagnostics.Trace.WriteLine(I_Trace);
            log.Info(I_Trace);
        }
    }
}
