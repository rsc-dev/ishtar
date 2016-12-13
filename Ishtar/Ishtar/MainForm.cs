using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Ishtar
{
    public partial class MainForm : Form
    {
        private PythonInterpreter pyInterpreter = null;

        private System.Text.StringBuilder sb1 = new System.Text.StringBuilder("sb1");
        private System.Text.StringBuilder sb2 = new System.Text.StringBuilder("sb2");

        public MainForm()
        {
            InitializeComponent();
            InitializePythonInterpreter();
        }

        private void InitializePythonInterpreter()
        {
            this.pyInterpreter = new PythonInterpreter();
            this.pyInterpreter.SetTextBoxOutput(this.tbPyConsole);
            this.pyInterpreter.SetVariable("test1", sb1);
            this.pyInterpreter.SetVariable("test2", sb2);
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

        private unsafe void btnTest_Click(object sender, EventArgs e)
        {
            TypedReference tr1 = __makeref(sb1);
            IntPtr ptr1 = **(IntPtr**)(&tr1);
            TypedReference tr2 = __makeref(sb2);
            IntPtr ptr2 = **(IntPtr**)(&tr2);

            //ObjectUtils.Objects ou = new ObjectUtils.Objects();

            MessageBox.Show(String.Format("Address1: {0:X} | ou1: {1:X}\nAddress2: {2:X} | ou2: {3:X}",
                ptr1.ToInt32(), ObjectUtils.Objects.GetObjectAddr(sb1).ToInt32(),
                ptr2.ToInt32(), ObjectUtils.Objects.GetObjectAddr(sb2).ToInt32()
                ));
        }

        private unsafe void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String typeName = tbTestType.Text.Trim();

                Type t = ObjectUtils.Objects.GetTypeByName(typeName);
                Object o = ObjectUtils.Objects.GetObjectByType(t);

                IntPtr objPtr = ObjectUtils.Objects.GetObjectAddr(o);

                tbTestAddress.Text = String.Format("{0:X}", objPtr.ToInt32());

                int mtAddress = System.Runtime.InteropServices.Marshal.ReadInt32(objPtr);

                tbTestMt.Text = String.Format("{0:X}", mtAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHeapManually_Click(object sender, EventArgs e)
        {

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


    }
}
