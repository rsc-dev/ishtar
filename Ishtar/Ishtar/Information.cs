using System.Diagnostics;

namespace Ishtar
{
    /// <summary>
    /// Current Process information class.
    /// </summary>
    class Information
    {
        /// <summary>
        /// Returns current process identifier (PID).
        /// </summary>
        /// <returns>Process identifier.</returns>
        public static int GetPid()
        {
            return Process.GetCurrentProcess().Id;
        }

        /// <summary>
        /// Returns current process name.
        /// </summary>
        /// <returns>Process name.</returns>
        public static string GetName()
        {
            return Process.GetCurrentProcess().ProcessName;
        }

    }
}
