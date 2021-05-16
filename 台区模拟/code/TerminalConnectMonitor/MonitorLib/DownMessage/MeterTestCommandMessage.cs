using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class MeterTestCommandMessage : DownMessageBase
    {
        public byte MeterID;

        public uint MeterDataFlag;

        public MeterTestCommandMessage()
        {

        }
        public MeterTestCommandMessage(uint TerminalAddress)
        {
            InitInfo(TerminalAddress); 
            this.MessageLength = 33;
            this.MessageType = DownMessageTypeEnum.MeterTest_Command;
        }



        public override byte[] GetAllBytes()
        {
            byte[] result = new byte[this.MessageLength];

            for (int i = 0; i < 4; i++)
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
                result[8 + i] = addresBytes[i];
            }
            result[12] = MeterID;
            result[13] = 0;
            result[14] = 0;
            result[15] = 0;

            byte[] testDataFlagBytes = MessageCommon.GetBytesByUint32(MeterDataFlag);
            for (int i = 0; i < 4; i++)
            {
                result[16 + i] = addresBytes[i];
            }

            result[20] = 0;
            result[21] = 0;
                            result[22] = 0;
            result[23] = 0;
            result[24] = 0;
            result[25] = 0;
            result[26] = 0;
            result[27] = 0;


            result[28] = CrcLib.CRC8Cal(result, 28);

            for (int i = 0; i < 4; i++)
            {
                result[29 + i] = DownMessageTail[i];
            }
            return result;

        }

        public static MeterTestCommandMessage GetSampleMessage()
        {
            uint address = 1024;
            MeterTestCommandMessage result = new MeterTestCommandMessage(address);

            result.MeterID = 5;
            result.MeterDataFlag = 96;


            return result;

        }

        public static MeterTestCommandMessage GetMessageFromBytes(byte[] I_Source)
        {
            MeterTestCommandMessage result = new MeterTestCommandMessage();

            result.MessageLength = I_Source[4];
            result.Remain = I_Source[5];
            result.MessageType = (DownMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);
            result.MeterID = I_Source[12];
            result.MeterDataFlag = MessageCommon.GetUint32ByBytes(I_Source, 16);
 
            return result;
        }




    }
}
