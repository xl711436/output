﻿using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
     public class SetHeartbeatAnswerMessage : UpMessageBase
    {
        public byte SetResult;

        public UInt16 SetInterval;

        public SetHeartbeatAnswerMessage()
        {

        }
        public SetHeartbeatAnswerMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 20; 
            this.MessageType = UpMessageTypeEnum.SetHeartbeat_Answer;
        
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

            byte[] setIntervalBytes = MessageCommon.GetBytesByUint16(SetInterval);

            for (int i = 0; i < 2; i++)
            {
                result[13 + i] = setIntervalBytes[i];
            }

            result[15] = CrcLib.CRC8Cal(result, 15);

            for (int i = 0; i < 4; i++)
            {
                result[16 + i] = UpMessageTail[i];
            }
            return result; 
        }

        public static SetHeartbeatAnswerMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 1024;

            SetHeartbeatAnswerMessage result = new SetHeartbeatAnswerMessage(curInfo);

            result.SetResult = 0;

            result.SetInterval = 30;

            return result;
        }

        public static SetHeartbeatAnswerMessage GetMessageFromBytes(byte[] I_Source)
        {
            SetHeartbeatAnswerMessage result = new SetHeartbeatAnswerMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);

            result.SetResult = I_Source[12];

            result.SetInterval = MessageCommon.GetUint16ByBytes(I_Source, 13);
  
            return result;
        }

    }
}
