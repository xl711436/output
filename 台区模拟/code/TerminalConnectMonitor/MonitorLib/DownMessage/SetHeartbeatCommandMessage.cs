using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class SetHeartbeatCommandMessage : DownMessageBase
    {
        public UInt16 HeartbeatInterval;

        public SetHeartbeatCommandMessage()
        {
        }
        public SetHeartbeatCommandMessage(uint TerminalAddress)
        {
            InitInfo(TerminalAddress); 
            this.MessageLength = 19;
            this.MessageType = DownMessageTypeEnum.SetHeartbeat_Command;
        }



        public override byte[] GetAllBytes()
        {
            byte[] result = new byte[this.MessageLength];

            for(int i =0; i< 4; i++)
            {
                result[i] = DownMessageHead[i];
            }
            result[4] = MessageLength;
            result[5] = 0;
            result[6] = (byte)MessageType;
            result[7] = MessageFormatVersion;

            byte[] addresBytes = MessageCommon.GetBytesByUint32(TerminalAddress);
            for (int i = 0; i < 4; i++)
            {
                result[8+ i] = addresBytes[i];
            }
            byte[] heartbeatInterval = MessageCommon.GetBytesByUint16(HeartbeatInterval);

            for (int i = 0; i < 2; i++)
            {
                result[12 + i] = heartbeatInterval[i];
            }
 
            result[14] = CrcLib.CRC8Cal(result, 14);

            for (int i = 0; i < 4; i++)
            {
                result[15 + i] = DownMessageTail[i];
            }
            return result;
             
        }

        public static SetHeartbeatCommandMessage GetSampleMessage()
        {
            uint address = 1024;
            SetHeartbeatCommandMessage result = new SetHeartbeatCommandMessage(address);

            result.HeartbeatInterval = 60;

            return result;

        }


        public static SetHeartbeatCommandMessage GetMessageFromBytes(byte[] I_Source)
        {
            SetHeartbeatCommandMessage result = new SetHeartbeatCommandMessage();
             
            result.MessageLength = I_Source[4];
            result.Remain = I_Source[5];
            result.MessageType =  (DownMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);
            result.HeartbeatInterval = MessageCommon.GetUint16ByBytes(I_Source, 12);


            return result;
        }





    }
}
