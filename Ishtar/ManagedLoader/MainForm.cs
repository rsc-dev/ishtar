using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            Assembly.LoadFile(tbAssembly.Text);

            tbAssembly.Text = String.Empty;
            btnLoad.Enabled = false;

            UpdateAssembliesList();
        }
    }
}
