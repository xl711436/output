using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class VoltageChangerUploadDataMessage :UpMessageBase
    {
        public uint SamplingTime;

        public UInt16 SkinTemp;

        public UInt16 EnvTemp;

        public UInt16 EnvHumidity;

        public VoltageChangerUploadDataMessage()
        {

        }
        public VoltageChangerUploadDataMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 27;
            this.MessageType = UpMessageTypeEnum.UploadData; ;
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

            byte[] samplingTimeBytes = MessageCommon.GetBytesByUint32(SamplingTime);
            byte[] skinTempBytes = MessageCommon.GetBytesByUint16(SkinTemp);
            byte[] envTempBytes = MessageCommon.GetBytesByUint16(EnvTemp);
            byte[] envHumidityBytes = MessageCommon.GetBytesByUint16(EnvHumidity);


            for (int i = 0; i < 4; i++)
            {
                result[12 + i] = samplingTimeBytes[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[16 + i] = skinTempBytes[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[18 + i] = envTempBytes[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[20 + i] = envHumidityBytes[i];
            }
              

            result[22] = CrcLib.CRC8Cal(result, 22);

            for (int i = 0; i < 4; i++)
            {
                result[23 + i] = UpMessageTail[i];
            }
            return result;
        }

        public static VoltageChangerUploadDataMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.VoltageChanger;
            curInfo.Address = 0x11223344;
            

            VoltageChangerUploadDataMessage result = new VoltageChangerUploadDataMessage(curInfo);

            result.SamplingTime = MessageCommon.GetUnixstampByDateTime(new DateTime(1970, 1, 1, 0, 12, 11));
            result.SkinTemp = MessageCommon.GetTempValue(22.41);
            result.EnvTemp = MessageCommon.GetTempValue(24.18);
            result.EnvHumidity = MessageCommon.GetHumidityValue(56.36);

            return result;
        }


        public static VoltageChangerUploadDataMessage GetMessageFromBytes(byte[] I_Source)
        {
            VoltageChangerUploadDataMessage result = new VoltageChangerUploadDataMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);

            result.SamplingTime = MessageCommon.GetUint32ByBytes(I_Source, 12);
            result.SkinTemp = MessageCommon.GetUint16ByBytes(I_Source, 16);
            result.EnvTemp = MessageCommon.GetUint16ByBytes(I_Source, 18);
            result.EnvHumidity = MessageCommon.GetUint16ByBytes(I_Source, 20);
             
            return result;
        }

    }
}
