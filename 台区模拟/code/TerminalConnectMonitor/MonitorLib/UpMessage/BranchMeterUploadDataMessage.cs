using MonitorLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{
    public class BranchMeterUploadDataMessage : UpMessageBase
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



        public BranchMeterUploadDataMessage()
        {

        }

        public BranchMeterUploadDataMessage(TerminalInfo I_TerminalInfo)
        {
            InitInfo(I_TerminalInfo);

            this.MessageLength = 51;
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
           
       


            result[46] = CrcLib.CRC8Cal(result, 46);

            for (int i = 0; i < 4; i++)
            {
                result[47 + i] = UpMessageTail[i];
            }
            return result;
        }

        public static BranchMeterUploadDataMessage GetSampleMessage()
        {
            TerminalInfo curInfo = new TerminalInfo();
            curInfo.Type = TerminalTypeEnum.BranchMeter;
            curInfo.Address = 0x11223344;


            BranchMeterUploadDataMessage result = new BranchMeterUploadDataMessage(curInfo);

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
             

            return result;
        }


        public BranchMeterUploadDataMessage GetMessageFromBytes(byte[] I_Source)
        {




            BranchMeterUploadDataMessage result = new BranchMeterUploadDataMessage();

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
