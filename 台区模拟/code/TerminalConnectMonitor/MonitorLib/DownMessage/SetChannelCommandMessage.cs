using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class SetChannelCommandMessage : DownMessageBase
    {
        public string FirstIp;

        public UInt16 FirstPort;

        public string BakIp;

        public UInt16 BakPort;

        public SetChannelCommandMessage()
        {

        }
        public SetChannelCommandMessage(uint TerminalAddress)
        {
            InitInfo(TerminalAddress); 
            this.MessageLength = 29;
            this.MessageType = DownMessageTypeEnum.SetChannel_Command;
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
            byte[] firstIpBytes = MessageCommon.GetByteByIp(FirstIp);
            byte[] firstPortBytes = MessageCommon.GetBytesByUint16(FirstPort);
            byte[] bakIpBytes = MessageCommon.GetByteByIp(BakIp);
            byte[] bakPortBytes = MessageCommon.GetBytesByUint16(BakPort);

            for (int i = 0; i < 4; i++)
            {
                result[12 + i] = firstIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[16 + i] = firstPortBytes[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[18 + i] = bakIpBytes[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[22 + i] = bakPortBytes[i];
            }

            result[24] = CrcLib.CRC8Cal(result, 24);

            for (int i = 0; i < 4; i++)
            {
                result[25 + i] = DownMessageTail[i];
            }
            return result;
             
        }

        public static SetChannelCommandMessage GetSampleMessage()
        {
            uint address = 1024;
            SetChannelCommandMessage result = new SetChannelCommandMessage(address);

            result.FirstIp = "192.168.0.1"; 
            result.FirstPort = 10060; 
            result.BakIp = "192.168.0.2"; 
            result.BakPort = 10060;

            return result;

        }

        public static SetChannelCommandMessage GetMessageFromBytes(byte[] I_Source)
        {
            SetChannelCommandMessage result = new SetChannelCommandMessage();

            result.MessageLength = I_Source[4];
            result.Remain = I_Source[5];
            result.MessageType = (DownMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);

            result.FirstIp = MessageCommon.GetIpByByte(I_Source, 12);
            result.FirstPort = MessageCommon.GetUint16ByBytes(I_Source, 16);
            result.BakIp = MessageCommon.GetIpByByte(I_Source, 18);
            result.BakPort = MessageCommon.GetUint16ByBytes(I_Source, 22);

            return result;
        }



    }
}
