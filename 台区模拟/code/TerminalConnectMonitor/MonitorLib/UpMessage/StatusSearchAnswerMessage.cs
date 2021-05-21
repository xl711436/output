using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
     public class StatusSearchAnswerMessage : UpMessageBase
    {

        public UInt16 TerminalStatus;
        public byte TerminalCpu;
        public byte NetworkStatus;
        public uint ResponseTimeStamp;
        public uint StaticDataSaveTimeStamp;
        public uint TerminalLastOpenTimeStamp;
        public uint TerminalOpenCount;
        public uint TerminalHardwareErrorCount;
        public UInt16 TerminalLastErrorCode;
        public uint TerminalLastErrorCodeTimeStamp;
        public UInt64 DTUSendBytes;
        public uint DTUErrorCount;
        public UInt16 DTULastErrorCode;
        public uint DTULastErrorCodeTimeStamp;
        public uint DTULast1OnLineSeconds; 
        public uint DTULast2OnLineSeconds;
        public uint DTULast3OnLineSeconds;
        public uint DTULast4OnLineSeconds;
        public uint TerminalMadeTimeStamp;
        public uint TerminalSN;


        public UInt16 HeartbeatInterval;
        public UInt16 UploadDataInterval;
        public UInt16 UploadDataDelay;
        public string FirstIp;
        public UInt16 FirstPort;
        public string BakIp;
        public UInt16 BakPort;
        public byte[] APNUserName;
        public byte[] APNPassword;
        public byte AuthorType;
        public byte ISPType;
        public byte CardBindType; 
        public byte[] ICCIDCode;
  
   

        public StatusSearchAnswerMessage()
        {

        }
        public StatusSearchAnswerMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 170;
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


            byte[] terminalStatusBytes = MessageCommon.GetBytesByUint16(TerminalStatus);
            for (int i = 0; i < 2; i++)
            {
                result[12 + i] = terminalStatusBytes[i];
            }
            result[14] = TerminalCpu;
            result[15] = NetworkStatus;

            byte[] responseTimeStampBytes = MessageCommon.GetBytesByUint32(ResponseTimeStamp);
            byte[] staticDataSaveTimeStampBytes = MessageCommon.GetBytesByUint32(StaticDataSaveTimeStamp);
            byte[] terminalLastOpenTimeStampBytes = MessageCommon.GetBytesByUint32(TerminalLastOpenTimeStamp);
            byte[] terminalOpenCountBytes = MessageCommon.GetBytesByUint32(TerminalOpenCount);
            byte[] terminalHardwareErrorCountBytes = MessageCommon.GetBytesByUint32(TerminalHardwareErrorCount);

            for (int i = 0; i < 4; i++)
            {
                result[16 + i] = responseTimeStampBytes[i];
            }

            for (int i = 0; i < 4; i++)
            {
                result[20 + i] = staticDataSaveTimeStampBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[24 + i] = terminalLastOpenTimeStampBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[28 + i] = terminalOpenCountBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[32 + i] = terminalHardwareErrorCountBytes[i];
            }

            byte[] terminalLastErrorCodeBytes = MessageCommon.GetBytesByUint16(TerminalLastErrorCode);
            byte[] TerminalLastErrorCodeTimeStampBytes = MessageCommon.GetBytesByUint32(TerminalLastErrorCodeTimeStamp);
            byte[] dTUSendBytesBytes = MessageCommon.GetBytesByUint64(DTUSendBytes);
            byte[] dTUErrorCountBytes = MessageCommon.GetBytesByUint32(DTUErrorCount);
            byte[] dTULastErrorCodeBytes = MessageCommon.GetBytesByUint16(DTULastErrorCode);


            for (int i = 0; i < 2; i++)
            {
                result[36 + i] = terminalLastErrorCodeBytes[i];
            }

            for (int i = 0; i < 4; i++)
            {
                result[38 + i] = TerminalLastErrorCodeTimeStampBytes[i];
            }
            for (int i = 0; i < 8; i++)
            {
                result[42 + i] = dTUSendBytesBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[50 + i] = dTUErrorCountBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[54 + i] = dTULastErrorCodeBytes[i];
            }

            byte[] dTULastErrorCodeTimeStampBytes = MessageCommon.GetBytesByUint32(DTULastErrorCodeTimeStamp);
            byte[] dTULast1OnLineSecondsBytes = MessageCommon.GetBytesByUint32(DTULast1OnLineSeconds);
            byte[] dTULast2OnLineSecondsBytes = MessageCommon.GetBytesByUint32(DTULast2OnLineSeconds);
            byte[] dTULast3OnLineSecondsBytes = MessageCommon.GetBytesByUint32(DTULast3OnLineSeconds);
            byte[] dTULast4OnLineSecondsBytes = MessageCommon.GetBytesByUint32(DTULast4OnLineSeconds);
            byte[] terminalMadeTimeStampBytes = MessageCommon.GetBytesByUint32(TerminalMadeTimeStamp);
            byte[] terminalSNBytes = MessageCommon.GetBytesByUint32(TerminalSN);

            for (int i = 0; i < 4; i++)
            {
                result[56 + i] = dTULastErrorCodeTimeStampBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[60 + i] = dTULast1OnLineSecondsBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[64 + i] = dTULast2OnLineSecondsBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[68 + i] = dTULast3OnLineSecondsBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[72 + i] = dTULast4OnLineSecondsBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[76 + i] = terminalMadeTimeStampBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[80 + i] = terminalSNBytes[i];
            }

            byte[] heartbeatIntervalBytes = MessageCommon.GetBytesByUint16(HeartbeatInterval);
            byte[] uploadDataIntervalBytes = MessageCommon.GetBytesByUint16(UploadDataInterval);
            byte[] uploadDataDelayBytes = MessageCommon.GetBytesByUint16(UploadDataDelay);
            byte[] firstIpBytes = MessageCommon.GetByteByIp(FirstIp);
            byte[] firstPortBytes = MessageCommon.GetBytesByUint16(FirstPort);
            byte[] bakIpBytes = MessageCommon.GetByteByIp(BakIp);
            byte[] bakPortBytes = MessageCommon.GetBytesByUint16(BakPort);


            for (int i = 0; i < 2; i++)
            {
                result[84 + i] = heartbeatIntervalBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[86 + i] = uploadDataIntervalBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[88 + i] = uploadDataDelayBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[90 + i] = firstIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[94 + i] = firstPortBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[96 + i] = bakIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[100 + i] = bakPortBytes[i];
            }
             
            for (int i = 0; i < 20; i++)
            {
                result[102 + i] = APNUserName[i];
            }
            for (int i = 0; i < 20; i++)
            {
                result[122 + i] = APNPassword[i];
            }

            result[142] = AuthorType;
            result[143] = ISPType;
            result[144] = CardBindType;

            for (int i = 0; i < 20; i++)
            {
                result[145 +i ] = ICCIDCode[i];
            }
             
            result[165] = CrcLib.CRC8Cal(result, 165);

            for (int i = 0; i < 4; i++)
            {
                result[166  + i] = UpMessageTail[i];
            }
            return result; 
        }

        public static StatusSearchAnswerMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 123456789;

            StatusSearchAnswerMessage result = new StatusSearchAnswerMessage(curInfo);

            result.TerminalStatus = 0;
            result.TerminalCpu = 1;
            result.NetworkStatus=99;
            result.ResponseTimeStamp =MessageCommon.GetUnixstampByDateTime(new DateTime(2021,5,13,9,27,11));
            result.StaticDataSaveTimeStamp =0;
            result.TerminalLastOpenTimeStamp = MessageCommon.GetUnixstampByDateTime(new DateTime(2021, 5, 13, 9, 26, 40));
            result.TerminalOpenCount = 6;
            result.TerminalHardwareErrorCount = 1;
            result.TerminalLastErrorCode = 16;
            result.TerminalLastErrorCodeTimeStamp = MessageCommon.GetUnixstampByDateTime(new DateTime(2021, 5, 13, 9, 25, 0));
            result.DTUSendBytes = 6069;
            result.DTUErrorCount = 87;
            result.DTULastErrorCode = 10;
            result.DTULastErrorCodeTimeStamp = MessageCommon.GetUnixstampByDateTime(new DateTime(2021, 5, 13, 9, 15, 28));
            result.DTULast1OnLineSeconds =31;
            result.DTULast2OnLineSeconds = 284;
            result.DTULast3OnLineSeconds = 164;
            result.DTULast4OnLineSeconds = 0;
            result.TerminalMadeTimeStamp = MessageCommon.GetUnixstampByDateTime(new DateTime(2021, 1, 1, 0, 0, 0));
            result.TerminalSN = 123456789;


            result.HeartbeatInterval = 70;
            result.UploadDataInterval = 60;
            result.UploadDataDelay = 10;
            result.FirstIp = "106.54.98.19";
            result.FirstPort =44916;
            result.BakIp="0.0.0.0";
            result.BakPort = 30060;
            result.APNUserName = new byte[20];
            result.APNPassword = new byte[20];
            result.AuthorType=0;
            result.ISPType=2;
            result.CardBindType=0;
            result.ICCIDCode = new byte[20];
            result.ICCIDCode[0] = (byte)'1';
            result.ICCIDCode[1] = (byte)'2';
            result.ICCIDCode[2] = (byte)'3';
            result.ICCIDCode[3] = (byte)'4';
            result.ICCIDCode[4] = (byte)'5';
            result.ICCIDCode[5] = (byte)'6';
            result.ICCIDCode[6] = (byte)'7';
            result.ICCIDCode[7] = (byte)'8';
            result.ICCIDCode[8] = (byte)'1';
            result.ICCIDCode[9] = (byte)'2';
            result.ICCIDCode[10] = (byte)'3';
            result.ICCIDCode[11] = (byte)'4'; 
            result.ICCIDCode[12] = (byte)'5';
            result.ICCIDCode[13] = (byte)'6';
            result.ICCIDCode[14] = (byte)'7';
            result.ICCIDCode[15] = (byte)'8';
            result.ICCIDCode[16] = (byte)'1';
            result.ICCIDCode[17] = (byte)'2';
            result.ICCIDCode[18] = (byte)'3';
            result.ICCIDCode[19] = (byte)'4'; 

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



            result.TerminalStatus = MessageCommon.GetUint16ByBytes(I_Source, 12);
            result.TerminalCpu = I_Source[14];
            result.NetworkStatus = I_Source[15];
            result.ResponseTimeStamp =  MessageCommon.GetUint32ByBytes(I_Source, 16);
            result.StaticDataSaveTimeStamp = MessageCommon.GetUint32ByBytes(I_Source, 20);
            result.TerminalLastOpenTimeStamp = MessageCommon.GetUint32ByBytes(I_Source, 24);
            result.TerminalOpenCount = MessageCommon.GetUint32ByBytes(I_Source, 28);
            result.TerminalHardwareErrorCount = MessageCommon.GetUint32ByBytes(I_Source, 32);
            result.TerminalLastErrorCode = MessageCommon.GetUint16ByBytes(I_Source, 36);
            result.TerminalLastErrorCodeTimeStamp = MessageCommon.GetUint32ByBytes(I_Source, 38);
            result.DTUSendBytes =  MessageCommon.GetUint64ByBytes(I_Source, 42);
            result.DTUErrorCount = MessageCommon.GetUint32ByBytes(I_Source, 50);
            result.DTULastErrorCode = MessageCommon.GetUint16ByBytes(I_Source, 54);
            result.DTULastErrorCodeTimeStamp = MessageCommon.GetUint32ByBytes(I_Source, 56);
            result.DTULast1OnLineSeconds = MessageCommon.GetUint32ByBytes(I_Source, 60);
            result.DTULast2OnLineSeconds = MessageCommon.GetUint32ByBytes(I_Source, 64);
            result.DTULast3OnLineSeconds = MessageCommon.GetUint32ByBytes(I_Source, 68);
            result.DTULast4OnLineSeconds = MessageCommon.GetUint32ByBytes(I_Source, 72);
            result.TerminalMadeTimeStamp = MessageCommon.GetUint32ByBytes(I_Source, 76);
            result.TerminalSN = MessageCommon.GetUint32ByBytes(I_Source, 80);


            result.HeartbeatInterval = MessageCommon.GetUint16ByBytes(I_Source, 84);
            result.UploadDataInterval = MessageCommon.GetUint16ByBytes(I_Source, 86);
            result.UploadDataDelay = MessageCommon.GetUint16ByBytes(I_Source, 88);
            result.FirstIp = MessageCommon.GetIpByByte(I_Source, 90);
            result.FirstPort = MessageCommon.GetUint16ByBytes(I_Source, 94);
            result.BakIp = MessageCommon.GetIpByByte(I_Source, 96);
            result.BakPort = MessageCommon.GetUint16ByBytes(I_Source, 100);
            result.APNUserName = MessageCommon.ByteCopy(I_Source, 102,20);
            result.APNPassword = MessageCommon.ByteCopy(I_Source, 122, 20);


            result.AuthorType = I_Source[142];
            result.ISPType = I_Source[143];
            result.CardBindType = I_Source[144];
            result.ICCIDCode = MessageCommon.ByteCopy(I_Source, 145, 20);
        

            return result;

 
        }


    }
}
