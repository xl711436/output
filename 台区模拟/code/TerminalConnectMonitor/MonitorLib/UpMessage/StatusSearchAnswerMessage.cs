using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
     public class StatusSearchAnswerMessage : UpMessageBase
    {
        public byte HardwareErrorCode;

        public byte HardwareStatusCode;

        public uint AnswerTimestamp;

        public UInt16 HeartbeatInterval;

        public UInt16 UploadDataInterval;

        public UInt16 UploadDataDelay;

        public string FirstIp;

        public UInt16 FirstPort;

        public string BakIp;

        public UInt16 BakPort;


        public StatusSearchAnswerMessage()
        {

        }
        public StatusSearchAnswerMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 41;
            this.MessageType = UpMessageTypeEnum.StatusSearch_Answer;
            

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

            result[12] = this.HardwareErrorCode;
            result[13] = this.HardwareStatusCode;

            byte[] answerTimestampBytes = MessageCommon.GetBytesByUint32(AnswerTimestamp);

            byte[] heartbeatIntervalBytes = MessageCommon.GetBytesByUint16(HeartbeatInterval);
            byte[] uploadDataIntervalBytes = MessageCommon.GetBytesByUint16(UploadDataInterval);
            byte[] uploadDataDelayBytes = MessageCommon.GetBytesByUint16(UploadDataDelay);
            byte[] firstIpBytes = MessageCommon.GetByteByIp(FirstIp);
            byte[] firstPortBytes = MessageCommon.GetBytesByUint16(FirstPort);
            byte[] bakIpBytes = MessageCommon.GetByteByIp(BakIp);
            byte[] bakPortBytes = MessageCommon.GetBytesByUint16(BakPort);

            for (int i = 0; i < 4; i++)
            {
                result[14 + i] = answerTimestampBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[18 + i] = heartbeatIntervalBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[20 + i] = uploadDataIntervalBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[22 + i] = uploadDataDelayBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[24 + i] = firstIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[28 + i] = firstPortBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[30 + i] = bakIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[34 + i] = bakPortBytes[i];
            }


            result[36] = CrcLib.CRC8Cal(result, 36);

            for (int i = 0; i < 4; i++)
            {
                result[37 + i] = UpMessageTail[i];
            }
            return result; 
        }

        public static StatusSearchAnswerMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 1024;

            StatusSearchAnswerMessage result = new StatusSearchAnswerMessage(curInfo);


            result.HardwareErrorCode = 0;
            result.HardwareStatusCode=0;
            result.AnswerTimestamp = MessageCommon.GetUnixstampByDateTime(DateTime.Now);

            result.HeartbeatInterval = 60;

            result.UploadDataInterval =60;

            result.UploadDataDelay =60;

            result.FirstIp ="192.168.0.1";

            result.FirstPort= 10060;

            result.BakIp = "192.168.0.2";

            result.BakPort = 10060;



            return result;
        }

        public static StatusSearchAnswerMessage GetMessageFromBytes(byte[] I_Source)
        {
            StatusSearchAnswerMessage result = new StatusSearchAnswerMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);

            result.HardwareErrorCode = I_Source[12];
            result.HardwareStatusCode = I_Source[13];

            result.AnswerTimestamp = MessageCommon.GetUint32ByBytes(I_Source, 14);
            result.HeartbeatInterval = MessageCommon.GetUint16ByBytes(I_Source, 18);
            result.UploadDataInterval = MessageCommon.GetUint16ByBytes(I_Source, 20);
            result.UploadDataDelay = MessageCommon.GetUint16ByBytes(I_Source, 22);
            result.FirstIp = MessageCommon.GetIpByByte(I_Source, 24);
            result.FirstPort = MessageCommon.GetUint16ByBytes(I_Source, 28);
            result.BakIp = MessageCommon.GetIpByByte(I_Source, 30);
            result.BakPort = MessageCommon.GetUint16ByBytes(I_Source, 34);



            return result;
        }


    }
}
