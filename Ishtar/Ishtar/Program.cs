using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Ishtar
{
    static class Program
    {
        public static String DEAD = "deadbeef";

        [STAThread]
        public static void LoadIshtar()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

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
