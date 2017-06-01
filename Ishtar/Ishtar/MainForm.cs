using System;
using System.Reflection;
using System.Windows.Forms;

namespace Ishtar
{
    /// <summary>
    /// Ishtar MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private PythonInterpreter pyInterpreter = null;

        /// <summary>
        /// Ctor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializePythonInterpreter();
        }

        /// <summary>
        /// Initialize IronPython interpreter.
        /// </summary>
        private void InitializePythonInterpreter()
        {
            this.pyInterpreter = new PythonInterpreter();
            this.pyInterpreter.SetTextBoxOutput(this.tbPyConsole);
        }

        /// <summary>
        /// Execute Python code.
        /// </summary>
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
                tbPyCode.Text = String.Empty;
                tbPyCode.Focus();
            }
        }

        /// <summary>
        /// Button 'Execute' click.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event args.</param>
        private void btnExecute_Click(object sender, System.EventArgs e)
        {
            ExecuteCode();
        }

        /// <summary>
        /// Handle multiline TextBox Enter and Shift+Enter.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event args.</param>
        private void tbPyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                ExecuteCode();
            }
        }

        /// <summary>
        /// Refresh list of assemblies.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event args.</param>
        private void btnAssembliesRefresh_Click(object sender, EventArgs e)
        {
            Assemblies.RefreshAssemblies(tvAssemblies);
        }

        /// <summary>
        /// Refresh selected node childrens.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event args.</param>
        private void tvAssemblies_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Ishtar.Assemblies.TreeElement te = (Ishtar.Assemblies.TreeElement) e.Node;
            Assemblies.Refresh(te);
        }

        /// <summary>
        /// Display current process information.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event args.</param>
        private void tpInfo_Enter(object sender, EventArgs e)
        {
            lblPid.Text = String.Format("{0}", Information.GetPid());
            lblName.Text = String.Format("{0}", Information.GetName());
        }

        /// <summary>
        /// Handle Python code TextBox multiline property change.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event args.</param>
        private void cbMultiline_CheckedChanged(object sender, EventArgs e)
        {
            tbPyCode.Multiline = cbMultiline.Checked;

            if (cbMultiline.Checked)
            {
                tbPyCode.Height = 80;
                tbPyCode.ScrollBars = ScrollBars.Horizontal;
            }
            else
            {
                tbPyCode.Height = 20;
            }
        }

    }
}
