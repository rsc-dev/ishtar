using System;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
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
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public static Int32 GetMTAddress(IntPtr objAddress)
        {
            return Marshal.ReadInt32(objAddress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
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
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
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

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
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

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public static T GetInstanceTyped<T>(IntPtr ptr) where T: class
        {
            T o = Objects.GetInstance(ptr, typeof(T)) as T;
            return o;
        }

    }
}
