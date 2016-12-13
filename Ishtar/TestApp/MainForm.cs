using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        private SampleClass instance = new SampleClass();

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(string.Format("SampleClass object: {0}; name: {1}",
                instance.ToString(), instance.Name), "SampleClass instance", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSetName_Click(object sender, System.EventArgs e)
        {
            instance.Name = tbName.Text;
        }
    }
}
