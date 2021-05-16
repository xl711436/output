using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class SetUploadCommandMessage : DownMessageBase
    {
        public UInt16 SetInterval;
        public UInt16 SetDelay;

        public SetUploadCommandMessage()
        {

        }

        public SetUploadCommandMessage(uint TerminalAddress)
        {
            InitInfo(TerminalAddress); 
            this.MessageLength = 21;
            this.MessageType = DownMessageTypeEnum.SetUpload_Command;
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

            byte[] setIntervalBytes = MessageCommon.GetBytesByUint16(SetInterval);

            for (int i = 0; i < 2; i++)
            {
                result[12 + i] = setIntervalBytes[i];
            }

            byte[] setDelayBytes = MessageCommon.GetBytesByUint16(SetDelay);

            for (int i = 0; i < 2; i++)
            {
                result[14 + i] = setDelayBytes[i];
            }
             
            result[16] = CrcLib.CRC8Cal(result, 16);

            for (int i = 0; i < 4; i++)
            {
                result[17 + i] = DownMessageTail[i];
            }
            return result;
             
        }

        public static SetUploadCommandMessage GetSampleMessage()
        {
            uint address = 1024;
            SetUploadCommandMessage result = new SetUploadCommandMessage(address);

            result.SetInterval = 60;
            result.SetDelay = 60; 

            return result;

        }


        public static SetUploadCommandMessage GetMessageFromBytes(byte[] I_Source)
        {
            SetUploadCommandMessage result = new SetUploadCommandMessage();

            result.MessageLength = I_Source[4];
            result.Remain = I_Source[5];
            result.MessageType = (DownMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);
            result.SetInterval = MessageCommon.GetUint16ByBytes(I_Source, 12);
            result.SetDelay = MessageCommon.GetUint16ByBytes(I_Source, 14);

            return result;
        }





    }
}
