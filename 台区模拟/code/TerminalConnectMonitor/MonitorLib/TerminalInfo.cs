using MonitorLib.Enum;
using MonitorLib.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib
{
    /// <summary>
    /// 终端信息
    /// </summary>
    public class TerminalInfo
    {
        private TerminalTypeEnum type;

        private uint address;

        public TerminalTypeEnum Type { get => type; set => type = value; }
        public uint Address { get => address; set => address = value; }
    }
}
