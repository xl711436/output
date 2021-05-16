using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class BoxMeterUploadDataMessage : UpMessageBase
    {
        public uint SamplingTime;


        public UInt16 EnvTemp;

        public UInt16 EnvHumidity;

        public uint TotalEnergy;

        public uint AveragePower;

        public UInt16 LineLossRate;

        public UInt16 AVoltage;
        public UInt16 BVoltage;
        public UInt16 CVoltage;

        public uint APower;
        public uint BPower;
        public uint CPower;

        public ElectricMeter Meter0;
        public ElectricMeter Meter1;
        public ElectricMeter Meter2;
        public ElectricMeter Meter3;
        public ElectricMeter Meter4;
        public ElectricMeter Meter5;


        public BoxMeterUploadDataMessage()
        {

        }

        public BoxMeterUploadDataMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 149;
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

            byte[] lineLossRate = MessageCommon.GetBytesByUint16(LineLossRate);  

            byte[] aVoltage = MessageCommon.GetBytesByUint16(AVoltage);
            byte[] bVoltage = MessageCommon.GetBytesByUint16(BVoltage);
            byte[] cVoltage = MessageCommon.GetBytesByUint16(CVoltage);

            byte[] aPower = MessageCommon.GetBytesByUint32(APower);
            byte[] bPower = MessageCommon.GetBytesByUint32(BPower);
            byte[] cPower = MessageCommon.GetBytesByUint32(CPower);

            byte[] meter0 = Meter0.GetElectricMeterBytes();
            byte[] meter1 = Meter0.GetElectricMeterBytes();
            byte[] meter2 = Meter0.GetElectricMeterBytes();
            byte[] meter3 = Meter0.GetElectricMeterBytes();
            byte[] meter4 = Meter0.GetElectricMeterBytes();
            byte[] meter5 = Meter0.GetElectricMeterBytes();



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
                result[28 + i] = lineLossRate[i];
            }


            for (int i = 0; i < 2; i++)
            {
                result[30 + i] = aVoltage[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[32 + i] = bVoltage[i];
            }
            for (int i = 0; i < 2; i++)
            {
                result[34 + i] = cVoltage[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[36 + i] = aPower[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[40 + i] = bPower[i];
            }
            for (int i = 0; i < 4; i++)
            {
                result[44 + i] = cPower[i];
            }

            for (int i = 0; i < 16; i++)
            {
                result[48 + i] = meter0[i];
            }

            for (int i = 0; i < 16; i++)
            {
                result[64 + i] = meter1[i];
            }

            for (int i = 0; i < 16; i++)
            {
                result[80 + i] = meter2[i];
            }


            for (int i = 0; i < 16; i++)
            {
                result[96 + i] = meter3[i];
            }


            for (int i = 0; i < 16; i++)
            {
                result[112 + i] = meter3[i];
            }


            for (int i = 0; i < 16; i++)
            {
                result[128 + i] = meter3[i];
            }


            result[144] = CrcLib.CRC8Cal(result, 144);

            for (int i = 0; i < 4; i++)
            {
                result[145 + i] = UpMessageTail[i];
            }
            return result;
        }

        public static BoxMeterUploadDataMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.BoxMeter;
            curInfo.Address = 1024;


            BoxMeterUploadDataMessage result = new BoxMeterUploadDataMessage(curInfo);

            result.SamplingTime = MessageCommon.GetUnixstampByDateTime(new DateTime(2021, 4, 30, 0, 37, 39));
            result.EnvTemp = MessageCommon.GetTempValue(20);
            result.EnvHumidity = MessageCommon.GetHumidityValue(50);


            result.TotalEnergy = MessageCommon.GetEnergyValue(1234.56);

            result.AveragePower = MessageCommon.GetPowerValue(5566);
            result.LineLossRate = MessageCommon.GetLineLossRateValue(0.55);

            result.AVoltage = MessageCommon.GetVoltageValue(225);
            result.BVoltage = MessageCommon.GetVoltageValue(214);
            result.CVoltage = MessageCommon.GetVoltageValue(232);

            result.APower = MessageCommon.GetBoxPowerValue(1234);
            result.BPower = MessageCommon.GetBoxPowerValue(1234);
            result.CPower = MessageCommon.GetBoxPowerValue(1234);

            result.Meter0 = ElectricMeter.GetSampleElectricMeter();
            result.Meter1 = ElectricMeter.GetSampleElectricMeter();
            result.Meter2 = ElectricMeter.GetSampleElectricMeter();
            result.Meter3 = ElectricMeter.GetSampleElectricMeter();
            result.Meter4 = ElectricMeter.GetSampleElectricMeter();
            result.Meter5 = ElectricMeter.GetSampleElectricMeter();
             
            return result;
        }



        public BoxMeterUploadDataMessage GetMessageFromBytes(byte[] I_Source)
        {

   


            BoxMeterUploadDataMessage result = new BoxMeterUploadDataMessage();

            result.MessageLength = I_Source[4];
            result.TerminalType = (TerminalTypeEnum)I_Source[5];
            result.MessageType = (UpMessageTypeEnum)I_Source[6];
            result.MessageFormatVersion = I_Source[7];
            result.TerminalAddress = MessageCommon.GetUint32ByBytes(I_Source, 8);


            result.SamplingTime = MessageCommon.GetUint32ByBytes(I_Source, 12);
            result.EnvTemp = MessageCommon.GetUint16ByBytes(I_Source, 16);
            result.EnvHumidity = MessageCommon.GetUint16ByBytes(I_Source, 18);
            result.TotalEnergy = MessageCommon.GetUint32ByBytes(I_Source,20);
            result.AveragePower = MessageCommon.GetUint32ByBytes(I_Source, 24);
            result.LineLossRate = MessageCommon.GetUint16ByBytes(I_Source, 28);
            result.AVoltage = MessageCommon.GetUint16ByBytes(I_Source, 30);
            result.BVoltage = MessageCommon.GetUint16ByBytes(I_Source, 32);
            result.CVoltage = MessageCommon.GetUint16ByBytes(I_Source, 34);
            result.APower = MessageCommon.GetUint32ByBytes(I_Source, 36);
            result.BPower = MessageCommon.GetUint32ByBytes(I_Source, 40);
            result.CPower = MessageCommon.GetUint32ByBytes(I_Source, 44);


            result.Meter0 = ElectricMeter.GetElectricMeterByBytes(I_Source,48);
            result.Meter1 = ElectricMeter.GetElectricMeterByBytes(I_Source, 64);
            result.Meter2 = ElectricMeter.GetElectricMeterByBytes(I_Source, 80);
            result.Meter3 = ElectricMeter.GetElectricMeterByBytes(I_Source, 96);
            result.Meter4 = ElectricMeter.GetElectricMeterByBytes(I_Source, 112);
            result.Meter5 = ElectricMeter.GetElectricMeterByBytes(I_Source, 128);

             
            return result;
        }

    }
}
