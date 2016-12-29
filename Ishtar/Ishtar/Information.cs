using System.Diagnostics;

namespace Ishtar
{
    class Information
    {
        public static int GetPid()
        {
            return Process.GetCurrentProcess().Id;
        }

        public static string GetName()
        {
            return Process.GetCurrentProcess().ProcessName;
        }

    }
}
