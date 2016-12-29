using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Ishtar
{
    public partial class MainForm : Form
    {
        private PythonInterpreter pyInterpreter = null;

        public MainForm()
        {
            InitializeComponent();
            InitializePythonInterpreter();
        }

        private void InitializePythonInterpreter()
        {
            this.pyInterpreter = new PythonInterpreter();
            this.pyInterpreter.SetTextBoxOutput(this.tbPyConsole);
        }

        private void btnExecute_Click(object sender, System.EventArgs e)
        {
            ExecuteCode();
        }

        private void tbPyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecuteCode();
            }
        }

        private void ExecuteCode()
        {
            try
            {
                string code = tbPyCode.Text.Trim();
                tbPyConsole.AppendText(string.Format(">>> {0}\n", code));

                object resp = pyInterpreter.Execute(code);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                tbPyCode.Clear();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = false;

            DialogResult dr = fd.ShowDialog(this);
            if (DialogResult.OK == dr)
            {
                tbVMMapCsv.Text = fd.FileName;
            }
        }

        private void btnVMMap_Click(object sender, EventArgs e)
        {
            lbManagedHeapItems.Items.Clear();
            
            List<ManagedHeapItem> managedHeapItems = ManagedHeap.ParseVMMapCsv(tbVMMapCsv.Text);
            foreach (var item in managedHeapItems)
            {
                lbManagedHeapItems.Items.Add(item);                
            }

            lbManagedHeapItems.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("flag");

            IntPtr i = ObjectUtils.Objects.GS_GetObjectAddr(sb);
            Object o = ObjectUtils.Objects.GS_GetInstance(i);

            MethodInfo[] instanceMethods = o.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo mi in instanceMethods)
            {
                MessageBox.Show(mi.ToString());
            }

            MessageBox.Show(String.Format("{0}", sb.ToString()));

            
        }

        private void btnAssembliesRefresh_Click(object sender, EventArgs e)
        {
            Assemblies.RefreshAssemblies(tvAssemblies);
        }

        private void tvAssemblies_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void tvAssemblies_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Ishtar.Assemblies.TreeElement te = (Ishtar.Assemblies.TreeElement) e.Node;
            Assemblies.Refresh(te);
        }

        private void tpInfo_Enter(object sender, EventArgs e)
        {
            lblPid.Text = String.Format("{0}", Information.GetPid());
            lblName.Text = String.Format("{0}", Information.GetName());
        }

        private void tpInject_Enter(object sender, EventArgs e)
        {
            Thread t = new Thread(UpdateProcessesList);
            t.IsBackground = true;
            t.Start();
        }


        private void UpdateProcessesList()
        {
            this.Invoke((MethodInvoker)(() => lbProcesses.Items.Clear()));
            this.Invoke((MethodInvoker)(() => lbProcesses.DisplayMember = "Display"));

            foreach (Process p in Processes.GetManagedProcesses())
            {
                var item = new { Name = p.ProcessName, 
                                 PID = p.Id,
                                 Display = String.Format("{0} (PID: {1})", p.ProcessName, p.Id)
                                };

                this.Invoke((MethodInvoker)(() => lbProcesses.Items.Add(item)));
            }

            this.Invoke((MethodInvoker)(() => lbProcesses.Update()));
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            var item = lbProcesses.SelectedItem;

            if (item != null)
            {
                Type t = item.GetType();
                PropertyInfo piName = t.GetProperty("Name");
                PropertyInfo piPid = t.GetProperty("PID");

                string name = piName.GetValue(item) as string;
                int pid = (int)piPid.GetValue(item);

                if (DialogResult.Yes == MessageBox.Show(
                                            String.Format("Do you want to inject Ishtar to {0} (PID: {1})?", name, pid), 
                                            "Confirm",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question
                                        ))
                { 
                    // INJECT code goes here
                }
            } 
            else 
            {
                MessageBox.Show("Please select process to inject into.");
            }
        }
    }
}
