using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorClient
{
 
    /// <summary>
    /// 界面上显示的统计信息
    /// </summary>
    public class ClientStaticInfo
    {
        public int VoltageChangerNumber;
        public int VoltageChangerConnectNumber;
        public int VoltageChangerNotConnectNumber;

        public int HeadMeterNumber;
        public int HeadMeterConnectNumber;
        public int HeadMeterNotConnectNumber;

        public int BranchMeterNumber;
        public int BranchMeterConnectNumber;
        public int BranchMeterNotConnectNumber;

        public int BoxMeterNumber;
        public int BoxMeterConnectNumber;
        public int BoxMeterNotConnectNumber;

        public int TotalNumber;
        public int TotalConnectNumber;
        public int TotalNotConnectNumber;

        public double HandlingCapacity;
        public double ErrorRate;


    }
 
}
