using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    /// <summary>
    /// 电表信息
    /// </summary>
    public class ElectricMeter
    {
        public ElectricMeterTypeEnum ElectricMeterType;

        public UInt64 Address;

        public uint Power;

        public UInt16 MissRate;

        public UInt16 Temp;


        public byte[] GetElectricMeterBytes()
        {
            byte[] result = new byte[16];

            byte[] address = MessageCommon.GetBytesByUint64(Address);

            byte[] power = MessageCommon.GetBytesByUint32(Power);
            byte[] missRate = MessageCommon.GetBytesByUint16(MissRate);
            byte[] temp = MessageCommon.GetBytesByUint16(Temp);

            for (int i = 0; i < 4; i++)
            {
                result[i] = address[i+3];
            }

            for (int i = 0; i < 3; i++)
            {
                result[i+4] = address[i];
            }

            result[7] = (byte)ElectricMeterType;

            for (int i = 0; i < 4; i++)
            {
                result[8 + i] = power[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[12 + i] = missRate[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[14 + i] = temp[i];
            }

            return result;
        }

        public static ElectricMeter GetElectricMeterByBytes(byte[] I_Source,int I_StartIndex = 0)
        {
            ElectricMeter result = new ElectricMeter();

            byte[] addressBytes = new byte[8];

            for (int i = 0; i < 4; i++)
            {
                I_Source[i+ I_StartIndex] = addressBytes[i + 3];
            }

            for (int i = 0; i < 3; i++)
            {
                I_Source[i + 4 + I_StartIndex] = addressBytes[i];
            }

            result.Address = MessageCommon.GetUint64ByBytes(addressBytes); 
            result.ElectricMeterType = (ElectricMeterTypeEnum)I_Source[7 + I_StartIndex];

            result.Power = MessageCommon.GetUint32ByBytes(I_Source, 8 + I_StartIndex);

            result.MissRate = MessageCommon.GetUint16ByBytes(I_Source, 12 + I_StartIndex);

            result.Temp = MessageCommon.GetUint16ByBytes(I_Source, 14 + I_StartIndex);
 
            return result;
        }


        public static ElectricMeter GetSampleElectricMeter()
        {
            ElectricMeter result = new ElectricMeter();

            result.ElectricMeterType = ElectricMeterTypeEnum.OnePhase;
            result.Address = 123456;
            result.Power = MessageCommon.GetPowerValue(1234);
            result.MissRate = MessageCommon.GetMissRateValue(0.02);
            result.Temp = MessageCommon.GetTempValue(34);

            return result;
        }


    }
}
