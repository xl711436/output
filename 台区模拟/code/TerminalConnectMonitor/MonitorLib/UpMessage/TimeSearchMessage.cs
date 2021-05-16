using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
     public class TimeSearchMessage : UpMessageBase
    {
        public byte TimeFormate;

        public TimeSearchMessage()
        {

        }


        public TimeSearchMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 18; 
            this.MessageType = UpMessageTypeEnum.TimeSearch;
            this.TimeFormate = 0;
        }



        public override byte[] GetAllBytes()
        {
            byte[] result = new byte[this.MessageLength];

            for (int i = 0; i < 4; i++)
            {
                result[i] = UpMessageHead[i];
            }
            result[4] = MessageLength;
            result[5] = (byte)TerminalType;
            result[6] = (byte)MessageType;
            result[7] = MessageFormatVersion;

            byte[] addresBytes = MessageCommon.GetBytesByUint32(TerminalAddress);
            for (int i = 0; i < 4; i++)
            {
                result[8 + i] = addresBytes[i];
            }
            result[12] = this.TimeFormate;

            result[13] = CrcLib.CRC8Cal(result, 13);

            for (int i = 0; i < 4; i++)
            {
                result[14 + i] = UpMessageTail[i];
            }
            return result; 
        }

        public static TimeSearchMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 1024;

            TimeSearchMessage result = new TimeSearchMessage(curInfo);

            return result;
        }


        public static TimeSearchMessage GetMessageFromBytes(byte[] I_Source)
        {
            TimeSearchMessage result = new TimeSearchMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);

            result.TimeFormate = I_Source[12];
 

            return result;
        }



    }
}
