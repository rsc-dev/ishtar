using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ObjectUtils
{
    public class Objects
    {
        public enum CLR_VERSION
        { 
            VER_2_0 = 1,
            VER_4_0 = 2
        }

        public static object GetObjectByType(Type clazz)
        {
            ConstructorInfo ctor = clazz.GetConstructor(Type.EmptyTypes);
            object wantedObject = ctor.Invoke(new object[] { });

            return wantedObject;
        }

        public static Type GetTypeByName(String name)
        {
            return Type.GetType(name);
        }

        public static unsafe IntPtr GetObjectAddr(object o)
        {
            TypedReference typedRef = __makeref(o);
            IntPtr objPtr = **(IntPtr**)(&typedRef);

            return objPtr;
        }

        public static unsafe IntPtr GetObjectAddress(object o)
        {
            IntPtr objPtr = IntPtr.Zero;
            unsafe
            {
                objPtr = *(&objPtr - 3);
            }
            return objPtr; //0x260a4c8
        }

        public static Int32 GetMTAddress(IntPtr objAddress)
        {
            return Marshal.ReadInt32(objAddress);
        }

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
