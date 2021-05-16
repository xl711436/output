using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{

    /// <summary>
    /// 上行消息基类
    /// </summary>
    public abstract class UpMessageBase
    {

        public static byte[] UpMessageHead = new byte[] { 255, 255, 255, 90 };
        public static byte[] UpMessageTail = new byte[] { 255, 255, 255, 83 };

        public byte MessageLength;

        public TerminalTypeEnum TerminalType;

        public UpMessageTypeEnum MessageType;


        public byte MessageFormatVersion;

        public uint TerminalAddress;

  
         
        public void InitInfo(TerminalInfo I_TerminalInfo)
        {
            this.MessageLength = 17;

            this.TerminalType = I_TerminalInfo.Type;
            this.MessageFormatVersion = 0;

            this.TerminalAddress = I_TerminalInfo.Address;
        }




        public virtual byte[] GetAllBytes()
        {
            byte[] result = new byte[this.MessageLength];

            for(int i =0; i< 4; i++)
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
                result[8+ i] = addresBytes[i];
            }
            result[12] = CrcLib.CRC8Cal(result, 12);

            for (int i = 0; i < 4; i++)
            {
                result[13 + i] = UpMessageTail[i];
            }
            return result;


        }





    }
}
