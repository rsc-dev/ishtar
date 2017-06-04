using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ObjectUtils
{
    /// <summary>
    /// Helper class for .NET objects.
    /// </summary>
    public class Objects
    {
        /// <summary>
        /// CLR version enum offset.
        /// </summary>
        public enum CLR_VERSION
        { 
            VER_2_0 = 1,
            VER_4_0 = 2
        }

        /// <summary>
        /// Return object instance of given type.
        /// </summary>
        /// <param name="clazz">Class type.</param>
        /// <returns>Class instance.</returns>
        public static object GetObjectByType(Type clazz)
        {
            ConstructorInfo ctor = clazz.GetConstructor(Type.EmptyTypes);
            object wantedObject = ctor.Invoke(new object[] { });

            return wantedObject;
        }

        /// <summary>
        /// Get Type by its name.
        /// </summary>
        /// <param name="name">Type name.</param>
        /// <returns>System.Type with the specified name.</returns>
        public static Type GetTypeByName(String name)
        {
            return Type.GetType(name);
        }

        /// <summary>
        /// Return object address.
        /// </summary>
        /// <param name="o">Object instance.</param>
        /// <returns>Object address.</returns>
        public static unsafe IntPtr GetObjectAddress(object o)
        {
            TypedReference typedRef = __makeref(o);
            IntPtr objPtr = **(IntPtr**)(&typedRef);

            return objPtr;
        }

        /// <summary>
        /// Return MT address for given object address.
        /// </summary>
        /// <param name="objAddress">Object address.</param>
        /// <returns>Method Table (MT) address.</returns>
        public static Int32 GetMTAddress(IntPtr objAddress)
        {
            return Marshal.ReadInt32(objAddress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static object GetInstance2(IntPtr ptr)
        {
            IntPtr objPtr = new IntPtr(ptr.ToInt32());

            if (objPtr == IntPtr.Zero)
            {
                return null;
            }
            
            GCHandle handle = (GCHandle)objPtr;
            object objHandler = handle.Target;
            handle.Free();

            return objHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="clrVersion"></param>
        /// <returns></returns>
        public static object GetInstance(IntPtr ptr, CLR_VERSION clrVersion)
        {
            object refer = ptr.GetType();
            IntPtr objPtr = ptr;
            unsafe
            {
                *(&objPtr - (int)clrVersion) = *(&objPtr);
            }
            
            return refer;
        }

        public static object GetInstance(IntPtr ptr, Type t)
        {
            object refer = t;
            IntPtr objPtr = ptr;
            unsafe
            {
                *(&objPtr - (int)2) = *(&objPtr);
            }

            return refer;
        }

        public static IntPtr GS_GetObjectAddr(object wantedObject)
        {
            if (wantedObject == null)
                return IntPtr.Zero;

            IntPtr objectPointer = IntPtr.Zero;
            unsafe
            {
                //     System.Windows.Forms.MessageBox.Show("Address of objectPointer:" + (uint)(&objectPointer) + " " + *(&objectPointer));
                //     System.Windows.Forms.MessageBox.Show("Address of refer:" + (uint)(&objectPointer- 3) + " " + *(&objectPointer - 3));
                return *(&objectPointer - 3);
            }
            // return objectPointer;
        }

        public static object GS_GetInstance(IntPtr ptrIN)
        {
            object refer = ptrIN.GetType();
            IntPtr pointer = ptrIN;

            unsafe
            {
                *(&pointer - 2) = *(&pointer); //move the pointer of our object into the actual object on the stack! This tricks the Framework to think that "object" was declared here! 
            }
            //System.Windows.Forms.MessageBox.Show(refer.ToString());
            return refer;
        }
    }
}
