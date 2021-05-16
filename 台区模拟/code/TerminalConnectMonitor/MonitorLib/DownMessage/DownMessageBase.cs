using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MonitorLib.Message
{
    /// <summary>
    /// 下行消息基类
    /// </summary>
    public abstract class DownMessageBase
    {
         
        public static byte[] DownMessageHead = new byte[] { 255, 255, 255, 91 };
        public static byte[] DownMessageTail = new byte[] { 255, 255, 255, 83 };

        public byte MessageLength;


        public byte Remain = 0;
 

        public DownMessageTypeEnum MessageType;


        public byte MessageFormatVersion;

        public uint TerminalAddress;

  
         
        public void InitInfo(uint TerminalAddress)
        {
            this.MessageLength = 17;
            Remain = 0;
            this.MessageFormatVersion = 0;
            this.TerminalAddress = TerminalAddress; 
        }




        public virtual byte[] GetAllBytes()
        {
            byte[] result = new byte[this.MessageLength];

            for(int i =0; i< 4; i++)
            {
                result[i] = DownMessageHead[i];
            }
            result[4] = MessageLength;
            result[5] = Remain;
            result[6] = (byte)MessageType;
            result[7] = MessageFormatVersion;

            byte[] addresBytes = MessageCommon.GetBytesByUint32(TerminalAddress);
            for (int i = 0; i < 4; i++)
            {
                result[8+ i] = addresBytes[i];
            }
            result[12] = CrcLib.CRC8Cal(result, 12);

            for (int i = 0; i < 4; i++)
            {
                result[13 + i] = DownMessageTail[i];
            }
            return result;


        }





    }
}
