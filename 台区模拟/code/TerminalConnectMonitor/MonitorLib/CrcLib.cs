using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MonitorLib
{
    /// <summary>
    /// crc校验方法
    /// </summary>
    public class CrcLib
    {


        [DllImport("hqdmath64.dll", EntryPoint = "CRC8Cal", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static byte CRC8Cal(byte[] ptr, uint len);


     //   byte ([MarshalAs(UnmanagedType.LPArray)] byte[],uint
    }
}
