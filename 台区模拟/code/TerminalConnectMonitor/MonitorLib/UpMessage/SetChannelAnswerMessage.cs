using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
     public class SetChannelAnswerMessage : UpMessageBase
    {
        public byte SetResult;

        public string FirstIp;

        public UInt16 FirstPort;

        public string BakIp;

        public UInt16 BakPort;


        public SetChannelAnswerMessage()
        {

        }

        public SetChannelAnswerMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 30; 
            this.MessageType = UpMessageTypeEnum.SetChannel_Answer;
        
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
            result[12] = this.SetResult;

            byte[] firstIpBytes = MessageCommon.GetByteByIp(FirstIp);
            byte[] firstPortBytes = MessageCommon.GetBytesByUint16(FirstPort);
            byte[] bakIpBytes = MessageCommon.GetByteByIp(BakIp);
            byte[] bakPortBytes = MessageCommon.GetBytesByUint16(BakPort);

            for (int i = 0; i < 4; i++)
            {
                result[13 + i] = firstIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[17 + i] = firstPortBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[19 + i] = bakIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[23 + i] = bakPortBytes[i];
            }

            result[25] = CrcLib.CRC8Cal(result, 25);

            for (int i = 0; i < 4; i++)
            {
                result[26 + i] = UpMessageTail[i];
            }
            return result; 
        }

        public static SetChannelAnswerMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 1024;

            SetChannelAnswerMessage result = new SetChannelAnswerMessage(curInfo);

            result.SetResult = 0;

            result.FirstIp = "192.168.0.1";

            result.FirstPort = 10060;

            result.BakIp = "192.168.0.2";

            result.BakPort = 10060;

            return result;
        }


        public static SetChannelAnswerMessage GetMessageFromBytes(byte[] I_Source)
        {
            SetChannelAnswerMessage result = new SetChannelAnswerMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);

            result.SetResult = I_Source[12];

            result.FirstIp = MessageCommon.GetIpByByte(I_Source, 13);
            result.FirstPort = MessageCommon.GetUint16ByBytes(I_Source, 17);

            result.BakIp = MessageCommon.GetIpByByte(I_Source, 19);
            result.BakPort = MessageCommon.GetUint16ByBytes(I_Source, 21);
  
            return result;
        }

    }
}
