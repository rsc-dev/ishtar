using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Ishtar
{
    /// <summary>
    /// Ishtar.
    /// </summary>
    static class Program
    {
        // Test string.
        public static String DEAD = "deadbeef";

        /// <summary>
        /// Static method for use with ManagedLoader.
        /// </summary>
        [STAThread]
        public static void LoadIshtar()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Assembly resolver for dynamic loading of Ishtar into another application.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="args">Arguments</param>
        /// <returns>Assembly</returns>
        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
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
