using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class HeadMeterUploadDataMessage : UpMessageBase
    {
        public uint SamplingTime;


        public UInt16 EnvTemp;

        public UInt16 EnvHumidity;

        public uint TotalEnergy;

        public uint AveragePower;

        public UInt16 AVoltage;
        public UInt16 BVoltage;
        public UInt16 CVoltage;

        public uint APower;
        public uint BPower;
        public uint CPower;

        public UInt16 TotalPowerFactor;

        public UInt16 APowerFactor;
        public UInt16 BPowerFactor;
        public UInt16 CPowerFactor;


        public HeadMeterUploadDataMessage()
        {

        }


        public HeadMeterUploadDataMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 59;
            this.MessageType = UpMessageTypeEnum.UploadData; 
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
            byte[] envTempBytes = MessageCommon.GetBytesByUint16(EnvTemp);
            byte[] envHumidityBytes = MessageCommon.GetBytesByUint16(EnvHumidity);
             
            byte[] totalEnergy = MessageCommon.GetBytesByUint32(TotalEnergy);

            byte[] averagePower = MessageCommon.GetBytesByUint32(AveragePower);

            byte[] aVoltage = MessageCommon.GetBytesByUint16(AVoltage);
            byte[] bVoltage = MessageCommon.GetBytesByUint16(BVoltage);
            byte[] cVoltage = MessageCommon.GetBytesByUint16(CVoltage);

            byte[] aPower = MessageCommon.GetBytesByUint32(APower);
            byte[] bPower = MessageCommon.GetBytesByUint32(BPower);
            byte[] cPower = MessageCommon.GetBytesByUint32(CPower);

            byte[] totalPowerFactor = MessageCommon.GetBytesByUint16(TotalPowerFactor);

            byte[] aPowerFactor = MessageCommon.GetBytesByUint16(APowerFactor);
            byte[] bPowerFactor = MessageCommon.GetBytesByUint16(BPowerFactor);
            byte[] cPowerFactor = MessageCommon.GetBytesByUint16(CPowerFactor);

     

            for (int i = 0; i < 4; i++)
            {
                result[12 + i] = samplingTimeBytes[i];
            }
       

            for (int i = 0; i < 2; i++)
            {
                result[16 + i] = envTempBytes[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[18 + i] = envHumidityBytes[i];
            }

            for (int i = 0; i < 4; i++)
            {
                result[20 + i] = totalEnergy[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[24 + i] = averagePower[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[28 + i] = aVoltage[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[30 + i] = bVoltage[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[32 + i] = cVoltage[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[34 + i] = aPower[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[38 + i] = bPower[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[42 + i] = cPower[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[46 + i] = totalPowerFactor[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[48 + i] = aPowerFactor[i];
            }

            for (int i = 0; i < 2; i++)
            {
                result[50 + i] = bPowerFactor[i];
            }


            for (int i = 0; i < 2; i++)
            {
                result[52 + i] = cPowerFactor[i];
            }

       


            result[54] = CrcLib.CRC8Cal(result, 54);

            for (int i = 0; i < 4; i++)
            {
                result[55 + i] = UpMessageTail[i];
            }
            return result;
        }

        public static HeadMeterUploadDataMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.HeadMeter;
            curInfo.Address = 0x11223344;


            HeadMeterUploadDataMessage result = new HeadMeterUploadDataMessage(curInfo);

            result.SamplingTime = MessageCommon.GetUnixstampByDateTime(new DateTime(1970, 1, 1, 0, 12, 11));
            result.EnvTemp = MessageCommon.GetTempValue(24.18);
            result.EnvHumidity = MessageCommon.GetHumidityValue(56.36);


            result.TotalEnergy = MessageCommon.GetEnergyValue(1000);

            result.AveragePower = MessageCommon.GetPowerValue(10000);

            result.AVoltage = MessageCommon.GetVoltageValue(221);
            result.BVoltage = MessageCommon.GetVoltageValue(222);
            result.CVoltage = MessageCommon.GetVoltageValue(223);

            result.APower = MessageCommon.GetPowerValue(10000);
            result.BPower = MessageCommon.GetPowerValue(20000);
            result.CPower = MessageCommon.GetPowerValue(30000);

            result.TotalPowerFactor = MessageCommon.GetPowerFactoryValue(0.3);

            result.APowerFactor = MessageCommon.GetPowerFactoryValue(0.4);
            result.BPowerFactor = MessageCommon.GetPowerFactoryValue(0.3);
            result.CPowerFactor = MessageCommon.GetPowerFactoryValue(0.2);

            return result;
        }


        public HeadMeterUploadDataMessage GetMessageFromBytes(byte[] I_Source)
        {

            HeadMeterUploadDataMessage result = new HeadMeterUploadDataMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);


            result.SamplingTime = MessageCommon.GetUint32ByBytes(I_Source, 12);
            result.EnvTemp = MessageCommon.GetUint16ByBytes(I_Source, 16);
            result.EnvHumidity = MessageCommon.GetUint16ByBytes(I_Source, 18);
            result.TotalEnergy = MessageCommon.GetUint32ByBytes(I_Source, 20);
            result.AveragePower = MessageCommon.GetUint32ByBytes(I_Source, 24);
            result.AVoltage = MessageCommon.GetUint16ByBytes(I_Source, 28);
            result.BVoltage = MessageCommon.GetUint16ByBytes(I_Source, 30);
            result.CVoltage = MessageCommon.GetUint16ByBytes(I_Source, 32);
            result.APower = MessageCommon.GetUint32ByBytes(I_Source, 34);
            result.BPower = MessageCommon.GetUint32ByBytes(I_Source, 38);
            result.CPower = MessageCommon.GetUint32ByBytes(I_Source, 42);
  

            return result;
        }


    }
}
