using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MonitorLib.Enum
{
    public enum DownMessageTypeEnum
    {
        StatusSearch_Command = 0,
        TimeSearch_Answer = 1,  
        SetHeartbeat_Command = 2,
        SetUpload_Command= 3,
        SetChannel_Command = 4,
        MeterTest_Command = 5,

    }
}
