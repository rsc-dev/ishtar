using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ishtar
{
    class Processes
    {

        public static IEnumerable<Process> GetManagedProcesses()
        {
            var managedList = Process.GetProcesses()
                                .OfType<Process>()
                                .Where(p => Processes.IsProcessManaged(p));

            return managedList;
        }

        public static bool IsProcessManaged(Process p)
        { 
            bool isManaged = false;

            try
            {
                foreach (ProcessModule pm in p.Modules)
                {
                    if (pm.ModuleName.StartsWith("mscor", StringComparison.InvariantCultureIgnoreCase))
                    {
                        isManaged = true;
                        break;
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception)
            { 
                // pass
            }

            return isManaged;
        }
    }
}
