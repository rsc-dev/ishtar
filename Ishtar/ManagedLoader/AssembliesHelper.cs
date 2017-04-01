using System;
using System.Linq;
using System.Reflection;

namespace ManagedLoader
{
    class AssembliesHelper
    {
        public static Assembly GetAssemblyByName(String name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name.Equals(name));
        }

        public static Type GetTypeByName(Assembly a, String name)
        {
            return a.GetTypes().Single(t => t.FullName.Equals(name));
        }
    }
}
