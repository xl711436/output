using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorLib.Message
{

    /// <summary>通用帮助方法
    /// </summary>
    public class MessageCommon
    {

        public static byte[] GetBytesByUint64(UInt64 I_Value)
        {
            byte[] result = BitConverter.GetBytes(I_Value);

            return result;
        }

        public static UInt64 GetUint64ByBytes(byte[] I_Value, int I_StartIndex = 0)
        {
            UInt64 result = BitConverter.ToUInt64(I_Value, I_StartIndex);

            return result;
        }


        public static byte[] GetBytesByUint32(uint I_Value)
        {
            byte[] result = BitConverter.GetBytes(I_Value);

            return result;
        }


        public static uint GetUint32ByBytes(byte[] I_Value, int I_StartIndex = 0)
        {
            uint result = BitConverter.ToUInt32(I_Value, I_StartIndex);

            return result;
        }

        public static byte[] GetBytesByUint16(UInt16 I_Value)
        {
            byte[] result = BitConverter.GetBytes(I_Value);

            return result;
        }

        public static byte[] GetBCDBytesByUint32(uint I_Value)
        {
            byte[] result = new byte[6];
            string strVaue = I_Value.ToString().PadLeft(12, '0');


            result[0] = (byte)(Convert.ToInt32(strVaue[11]) + Convert.ToInt32(strVaue[10]) * 16);
            result[1] = (byte)(Convert.ToInt32(strVaue[9]) + Convert.ToInt32(strVaue[8]) * 16);
            result[2] = (byte)(Convert.ToInt32(strVaue[7]) + Convert.ToInt32(strVaue[6]) * 16);
            result[3] = (byte)(Convert.ToInt32(strVaue[5]) + Convert.ToInt32(strVaue[4]) * 16);
            result[4] = (byte)(Convert.ToInt32(strVaue[3]) + Convert.ToInt32(strVaue[2]) * 16);
            result[5] = (byte)(Convert.ToInt32(strVaue[1]) + Convert.ToInt32(strVaue[0]) * 16);

            return result;
        }

        public static uint GetUint32ByBCDBytes(byte[] I_Value, int I_StartIndex = 0)
        {
            uint result = 0;

            result = (uint)((byte)(I_Value[0 + I_StartIndex] % 16) + (byte)(I_Value[0 + I_StartIndex] / 16) * 10
                + (byte)(I_Value[1 + I_StartIndex] % 16) * 100 + (byte)(I_Value[1 + I_StartIndex] / 16) * 1000
                 + (byte)(I_Value[2 + I_StartIndex] % 16) * 10000 + (byte)(I_Value[2 + I_StartIndex] / 16) * 100000
            + (byte)(I_Value[3 + I_StartIndex] % 16) * 1000000 + (byte)(I_Value[3 + I_StartIndex] / 16) * 10000000
            + (byte)(I_Value[4 + I_StartIndex] % 16) * 100000000 + (byte)(I_Value[4 + I_StartIndex] / 16) * 1000000000
            + (byte)(I_Value[5 + I_StartIndex] % 16) * 10000000000 + (byte)(I_Value[5 + I_StartIndex] / 16) * 100000000000);


            return result;
        }


        public static UInt16 GetUint16ByBytes(byte[] I_Value, int I_StartIndex = 0)
        {
            UInt16 result = BitConverter.ToUInt16(I_Value, I_StartIndex);
           
            return result;
        }
 


        public static uint GetUnixstampByDateTime(DateTime I_DateTime)
        {
            uint result = 0;

            DateTime dtStart = new DateTime(1970, 1, 1);

            TimeSpan toNow = I_DateTime.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);

            result = (uint)Convert.ToInt32(timeStamp);

            return result;
        }

        public static DateTime GetDateTimeByUnixstamp(uint I_Unixstamp)
        {
            string timeStamp = I_Unixstamp.ToString();
            DateTime dtStart = new DateTime(1970, 1, 1);
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }


        public static UInt16 GetTempValue(double I_RealTemp)
        {
            UInt16 result = 0;

            result = (UInt16)(I_RealTemp * 100 + 10000);

            return result;
        }


        public static double GetTempReal(UInt16 I_ValueTemp)
        {
            double result = 0;

            result = (double)(I_ValueTemp - 10000 / (double)100);

            return result;
        }


        public static UInt16 GetHumidityValue(double I_RealHumidity)
        {
            UInt16 result = 0;

            result = (UInt16)(I_RealHumidity * 100);

            return result;
        }

        public static double GetHumidityReal(UInt16 I_ValueHumidity)
        {
            double result = 0;

            result = (double)(I_ValueHumidity / (double)100);

            return result;
        }

        public static uint GetEnergyValue(double I_RealEnergy)
        {
            uint result = 0;

            result = (uint)(I_RealEnergy * 100 + 100000000);

            return result;
        }

        public static double GetEnergyReal(uint I_ValueEnergy)
        {
            double result = 0;

            result = (double)(I_ValueEnergy - 100000000 / (double)100);

            return result;
        }


        public static uint GetPowerValue(double I_RealPower)
        {
            uint result = 0;

            result = (uint)(I_RealPower + 10000000);

            return result;
        }

        public static double GetPowerReal(uint I_ValuePower)
        {
            double result = 0;

            result = (double)(I_ValuePower - 10000000);

            return result;
        }


        public static UInt16 GetLineLossRateValue(double I_RealRate)
        {
            UInt16 result = 0;

            result = (UInt16)(I_RealRate * 10000 + 10000);

            return result;
        }

        public static double GetLineLossRateReal(UInt16 I_ValueRate)
        {
            double result = 0;

            result = (double)(I_ValueRate - 10000 / (double)10000);

            return result;
        }


        public static uint GetBoxPowerValue(double I_RealPower)
        {
            uint result = 0;

            result = (uint)(I_RealPower * 10 + 1000000);

            return result;
        }

        public static double GetBoxPowerReal(uint I_ValuePower)
        {
            double result = 0;

            result = (double)(I_ValuePower - 1000000 / (double)10);

            return result;
        }

        public static UInt16 GetVoltageValue(double I_RealVoltage)
        {
            UInt16 result = 0;

            result = (UInt16)(I_RealVoltage * 10);

            return result;
        }

        public static double GetVoltageReal(UInt16 I_ValueVoltage)
        {
            double result = 0;

            result = (double)(I_ValueVoltage / (double)10);

            return result;
        }

        public static UInt16 GetPowerFactoryValue(double I_RealPowerFactory)
        {
            UInt16 result = 0;

            result = (UInt16)(I_RealPowerFactory * 1000);

            return result;
        }

        public static double GetPowerFactoryReal(UInt16 I_ValuePowerFactory)
        {
            double result = 0;

            result = (double)(I_ValuePowerFactory / (double)1000);

            return result;
        }

        public static UInt16 GetMissRateValue(double I_RealMissRate)
        {
            UInt16 result = 0;

            result = (UInt16)(I_RealMissRate * 10000 + 10000);

            return result;
        }

        public static double GetMissRateReal(UInt16 I_ValueMissRate)
        {
            double result = 0;

            result = (double)(I_ValueMissRate - 10000) / (double)10000;

            return result;
        }


        public static byte[] GetByteByIp(string I_Ip)
        {
            byte[] result = new byte[4];

            string[] tempArray = I_Ip.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (tempArray.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    result[3-i] = (byte)Convert.ToInt32(tempArray[i]);
                }
            }


            return result;
        }

        public static string GetIpByByte(byte[] I_Bytes, int I_StartIndex)
        {
            string result = "";

            result = I_Bytes[3 + I_StartIndex].ToString() + "." + I_Bytes[2 + I_StartIndex].ToString() + "." + I_Bytes[1 + I_StartIndex].ToString() + "." + I_Bytes[0 + I_StartIndex].ToString();

            return result;
        }

        public static byte[] ByteCopy(byte[] I_Source, int I_Length)
        {
            return ByteCopy(I_Source, 0, I_Length);
        }

        public static byte[] ByteCopy(byte[] I_Source, int I_Start, int I_Length)
        {
            byte[] result = new byte[I_Length];

            for (int i = 0; i < I_Length; i++)
            {
                result[i] = I_Source[i + I_Start];
            }

            return result;
        }


        /// <summary>将一个总数随机分成 n份，总和等于总数
        /// </summary> 
        public static List<int> GetRandomList(int I_TotalNumber, int I_Divided)
        {
            List<int> result = new List<int>();
            Random curRandom = new Random();

            int remain = I_TotalNumber;

            for (int i = 0; i < I_Divided - 1; i++)
            {
                double bigDivide = 1 / (double)I_Divided;
                double smallDivide = 1 / (double)(I_Divided - 1);

                double curDivide = smallDivide + (bigDivide - smallDivide) * curRandom.NextDouble();

                int tempDivided = (int)(I_TotalNumber * curDivide);

                result.Add(tempDivided);
                remain = remain - tempDivided;
            }

            result.Add(remain);


            return result;
        }

        /// <summary>得到随机的线损值
        /// </summary>
        /// <param name="I_TotalCount"></param>
        /// <param name="I_WarnCount"></param>
        /// <param name="I_Threshold"></param>
        /// <returns></returns>
        public static List<int> GetRandomThreshold(int I_TotalCount, int I_WarnCount, int I_Threshold)
        {
            List<int> result = new List<int>(I_TotalCount);

            Random curRandom = new Random();

            for (int i = 0; i < I_TotalCount; i++)
            {
                result.Add(curRandom.Next(1, I_Threshold));
            }

            HashSet<int> warnIndex = new HashSet<int>();
            while (warnIndex.Count < I_WarnCount)
            {
                int curIndex = curRandom.Next(0, I_TotalCount);
                if (!warnIndex.Contains(curIndex))
                {
                    warnIndex.Add(curIndex);
                }
            }

            foreach (int curIndex in warnIndex)
            {
                result[curIndex] = curRandom.Next(I_Threshold, 90);
            }

            return result;
        }

    }


}
