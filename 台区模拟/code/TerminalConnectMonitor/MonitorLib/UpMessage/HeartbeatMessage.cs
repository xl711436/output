using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
     public class HeartbeatMessage : UpMessageBase
    {
        public HeartbeatMessage()
        {

        }
        public HeartbeatMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);
            this.MessageLength = 17;
            this.MessageType = UpMessageTypeEnum.Heartbeat;
        }


        public static HeartbeatMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 1024;

            HeartbeatMessage result = new HeartbeatMessage(curInfo);

            return result;
        }


        public static HeartbeatMessage GetMessageFromBytes(byte[] I_Source)
        {
            HeartbeatMessage result = new HeartbeatMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);

            return result;
        }
    }
}
