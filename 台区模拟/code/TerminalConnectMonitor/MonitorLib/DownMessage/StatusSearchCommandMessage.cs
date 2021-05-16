using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class StatusSearchCommandMessage :DownMessageBase
    {
        public byte MatterID;

        public StatusSearchCommandMessage()
        {

        }
        public StatusSearchCommandMessage(uint TerminalAddress)
        {
            InitInfo(TerminalAddress);
            this.MatterID = 0;
            this.MessageLength = 18;
            this.MessageType = DownMessageTypeEnum.StatusSearch_Command;
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
            result[12] = MatterID;
            result[13] = CrcLib.CRC8Cal(result, 13);

            for (int i = 0; i < 4; i++)
            {
                result[14 + i] = DownMessageTail[i];
            }
            return result;
             
        }

        public static StatusSearchCommandMessage GetSampleMessage()
        {
            uint address = 1024;
            StatusSearchCommandMessage result = new StatusSearchCommandMessage(address);

            return result;

        }

        public static StatusSearchCommandMessage GetMessageFromBytes(byte[] I_Source)
        {
            StatusSearchCommandMessage result = new StatusSearchCommandMessage();

            result.MessageLength = I_Source[4];
            result.Remain = I_Source[5];
            result.MessageType = (DownMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);
            result.MatterID = I_Source[12]; 

            return result;
        }




    }
}
