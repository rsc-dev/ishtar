using System;
using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        private string MSG = "TEST ME";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MSG);    
        }
    }
}
