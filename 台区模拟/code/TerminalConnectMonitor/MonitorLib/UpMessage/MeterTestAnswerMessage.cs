using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
     public class MeterTestAnswerMessage : UpMessageBase
    {

        public uint AnswerTimestamp;

        public byte MeterID;

        public uint MeterAddress;

        public uint MeterDataFlag;

        public byte MeterDataLength;

        public byte[] MeterData;

         


        public MeterTestAnswerMessage()
        {

        }
        public MeterTestAnswerMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 33;
            this.MessageType = UpMessageTypeEnum.MeterTest_Answer;
            

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
 

            byte[] answerTimestampBytes = MessageCommon.GetBytesByUint32(AnswerTimestamp);

            for (int i = 0; i < 4; i++)
            {
                result[12 + i] = answerTimestampBytes[i];
            }

            result[16] = MeterID;


            byte[] meterAddressBytes = MessageCommon.GetBytesByUint32(MeterAddress);


            for (int i = 0; i < 6; i++)
            {
                result[17 + i] = meterAddressBytes[i];
            }

            result[18] = MeterDataLength;


            for (int i = 0; i < MeterDataLength; i++)
            {
                result[19 + i] = MeterData[i];
            }

            result[19 + MeterDataLength] = CrcLib.CRC8Cal(result, (uint)(19 + MeterDataLength));

            for (int i = 0; i < 4; i++)
            {
                result[20 + MeterDataLength + i] = UpMessageTail[i];
            }
            return result; 
        }

        public static MeterTestAnswerMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 1024;

            MeterTestAnswerMessage result = new MeterTestAnswerMessage(curInfo);


       
            result.AnswerTimestamp = MessageCommon.GetUnixstampByDateTime(DateTime.Now);

            result.MeterID = 0;

            result.MeterAddress =10240000;

            result.MeterDataFlag =1024;

            result.MeterDataLength = 0;
             

            return result;
        }

        public static MeterTestAnswerMessage GetMessageFromBytes(byte[] I_Source)
        {
            MeterTestAnswerMessage result = new MeterTestAnswerMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);


 

            result.AnswerTimestamp = MessageCommon.GetUint32ByBytes(I_Source, 12);


            result.MeterID = I_Source[16];

            result.MeterAddress = MessageCommon.GetUint32ByBCDBytes(I_Source, 17);

            result.MeterDataLength = I_Source[18];

            result.MeterData = new byte[result.MeterDataLength];
            for (int i = 0; i < result.MeterDataLength; i++)
            {
                result.MeterData[i] = I_Source[19 + i];
            }
  
            return result;
        }


    }
}
