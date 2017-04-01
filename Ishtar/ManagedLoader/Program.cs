using System;
using System.Windows.Forms;

namespace ManagedLoader
{
    /// <summary>
    /// ManagedLoader Program class.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Parametrized version of Main method.
        /// </summary>
        /// <param name="param">Unused.</param>
        /// <returns>0</returns>
        static int Load(String param)
        {
            Program.Main();
            return 0;
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
