using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ObjectUtils
{
    public class Heap
    {
        public static unsafe List<object> FindObjectsByMTAddress32(int startAddress, int stopAddress, Int32 mt, Type t)
        {
            List<object> objects = new List<object>();

            IntPtr ptr = new IntPtr(startAddress);

            while (ptr.ToInt32() < stopAddress)
            {
                Int32 val = Marshal.ReadInt32(ptr);
                if (val == mt)
                {
                    objects.Add(Objects.GetInstance(ptr, t));
                }

                ptr += sizeof(int);
            }

            return objects;
        }


        public static List<object> FindObjectsByMTAddress64(long startAddress, long stopAddress, Int32 mt)
        {
            List<object> objects = new List<object>();

            IntPtr ptr = new IntPtr(startAddress);

            return objects;
        }
    }
}
