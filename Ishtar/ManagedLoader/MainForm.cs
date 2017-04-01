using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagedLoader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateAssembliesList();
            UdateProcessInfo();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog())
            {
                tbAssembly.Text = ofd.FileName;
                btnLoad.Enabled = true;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAssembly();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not load assembly!", "Error");
            }
        }

        private void lbAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAssembly();
        }

        private void tbExecuteAssembly_TextChanged(object sender, EventArgs e)
        {
            UpdateAssemblyTypes();
        }

        private void cbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAssemblyTypeMethods();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void UdateProcessInfo()
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

            string name = currentProcess.ProcessName;
            int pid = currentProcess.Id;

            processInfoLabel.Text = String.Format("Current process name: {0} \tPID: {1}", name, pid);
        }

        private void LoadAssembly()
        {
            Assembly.LoadFile(tbAssembly.Text);

            tbAssembly.Text = String.Empty;
            btnLoad.Enabled = false;

            UpdateAssembliesList();
        }

        private void UpdateAssembliesList()
        {
            var assemblies = from a in AppDomain.CurrentDomain.GetAssemblies() select a.GetName().Name;
            lbAssemblies.Items.Clear();

            foreach (String assembly in assemblies)
            {
                lbAssemblies.Items.Add(assembly);
            }
        }

        private void UpdateAssembly()
        {
            cbMethods.Items.Clear();
            cbTypes.Items.Clear();

            var assembly = lbAssemblies.SelectedItem as String;
            tbExecuteAssembly.Text = assembly;
        }

        private void UpdateAssemblyTypes()
        {
            String assemblyName = tbExecuteAssembly.Text;

            if (!String.IsNullOrEmpty(assemblyName))
            {
                var assembly = AssembliesHelper.GetAssemblyByName(assemblyName);
                var typez = assembly.GetTypes().Select(t => t.FullName).ToArray();
                
                cbTypes.Items.Clear();
                cbTypes.Items.AddRange(typez);

            }
            
        }

        private void UpdateAssemblyTypeMethods()
        {
            var assemblyName = tbExecuteAssembly.Text;
            var typeName = cbTypes.SelectedItem as String;

            if (!String.IsNullOrEmpty(typeName) && !String.IsNullOrEmpty(assemblyName))
            {
                var assembly = AssembliesHelper.GetAssemblyByName(assemblyName);
                var type = AssembliesHelper.GetTypeByName(assembly, typeName);

                var methods = type.GetMethods().Select(m => m.Name).ToArray();

                cbMethods.Items.Clear();
                cbMethods.Items.AddRange(methods);

            }
        }

        private void Execute()
        {
            var assemblyName = tbExecuteAssembly.Text;
            var typeName = cbTypes.SelectedItem as String;
            var methodName = cbMethods.SelectedItem as String;

            if (!String.IsNullOrEmpty(typeName) && !String.IsNullOrEmpty(methodName))
            {
                var assembly = AssembliesHelper.GetAssemblyByName(assemblyName);

                Type t = assembly.GetType(typeName);
                MethodInfo mi = t.GetMethod(methodName);

                object t_object = null;
                if (!mi.IsStatic)
                {
                    t_object = assembly.CreateInstance(typeName);
                }

                System.Threading.Thread invokeThread;
                invokeThread = new System.Threading.Thread(
                                new System.Threading.ThreadStart(() => {
                                    try
                                    {
                                        mi.Invoke(t_object, BindingFlags.Default | BindingFlags.Static | BindingFlags.InvokeMethod,
                                                    null, null, CultureInfo.CurrentCulture);
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(String.Format("Error: {0}", e), "Error");
                                    }
                                })
                                );

                invokeThread.Start();
            }
        }
        
    }
}
