using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class TimeSearchAnswerMessage : DownMessageBase
    {
        public DateTime SearcheDateTime;

        public TimeSearchAnswerMessage()
        {

        }
        public TimeSearchAnswerMessage(uint TerminalAddress)
        {
            InitInfo(TerminalAddress); 
            this.MessageLength = 21;
            this.MessageType = DownMessageTypeEnum.TimeSearch_Answer;
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

            byte[] searcheDateTimeBytes =   MessageCommon.GetBytesByUint32( MessageCommon.GetUnixstampByDateTime(SearcheDateTime));

            for (int i = 0; i < 4; i++)
            {
                result[12 + i] = searcheDateTimeBytes[i];
            }

            result[16] = CrcLib.CRC8Cal(result, 16);

            for (int i = 0; i < 4; i++)
            {
                result[17 + i] = DownMessageTail[i];
            }
            return result;
             
        }

        public static TimeSearchAnswerMessage GetSampleMessage()
        {
            uint address = 1024;
            TimeSearchAnswerMessage result = new TimeSearchAnswerMessage(address);
            result.SearcheDateTime = DateTime.Now;

            return result;

        }

        public static TimeSearchAnswerMessage GetMessageFromBytes(byte[] I_Source)
        {
            TimeSearchAnswerMessage result = new TimeSearchAnswerMessage();

            result.MessageLength = I_Source[4];
            result.Remain = I_Source[5];
            result.MessageType = (DownMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);
            result.SearcheDateTime = MessageCommon.GetDateTimeByUnixstamp(MessageCommon.GetUint32ByBytes(I_Source, 12));

            return result;
        }



    }
}
