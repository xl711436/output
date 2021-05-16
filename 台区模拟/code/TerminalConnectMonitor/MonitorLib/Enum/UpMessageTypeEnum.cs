using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Enum
{
    public enum UpMessageTypeEnum
    {
        Heartbeat = 0,
        TimeSearch = 1,
        StatusSearch_Answer = 2,
        UploadData = 3,
        SetHeartbeat_Answer = 4,
        SetUpload_Answer = 5,
        SetChannel_Answer = 6,
        MeterTest_Answer = 7,

    }
}
