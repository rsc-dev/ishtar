﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ObjectUtils
{
    /// <summary>
    /// Heap helper class.
    /// </summary>
    public class Heap
    {
        /// <summary>
        /// Return list of objects of given System.Type.
        /// Use in 32b processes.
        /// 
        /// </summary>
        /// <param name="startAddress">Heap start address.</param>
        /// <param name="stopAddress">Heap stop address.</param>
        /// <param name="mt">Method Table (MT) address.</param>
        /// <param name="t">System.Type of objects.</param>
        /// <returns>List of objects found.</returns>
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

        /// <summary>
        /// Return list of objects of given System.Type.
        /// Use in 64b processes.
        /// 
        /// </summary>
        /// <param name="startAddress">Heap start address.</param>
        /// <param name="stopAddress">Heap stop address.</param>
        /// <param name="mt">Method Table (MT) address.</param>
        /// <param name="t">System.Type of objects.</param>
        /// <returns>List of objects found.</returns>
        public static List<object> FindObjectsByMTAddress64(long startAddress, long stopAddress, long mt, Type t)
        {
            List<object> objects = new List<object>();

            IntPtr ptr = new IntPtr(startAddress);

            while (ptr.ToInt64() < stopAddress)
            {
                long val = Marshal.ReadInt64(ptr);
                if (val == mt)
                {
                    objects.Add(Objects.GetInstance(ptr, t));
                }

                ptr += sizeof(int);
            }

            return objects;
        }
    }
}
