using System.Windows.Forms;

namespace PuttyUtility
{
    public partial class FormOptions : Form
    {
        public Configuration Configuration { get; private set; }

        public FormOptions(Configuration configuration)
        {
            InitializeComponent();

            Configuration = configuration;

            textBoxPuttyExe.Text = Configuration.PuttyPath;
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            Configuration.PuttyPath = textBoxPuttyExe.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonPuttyExeBrowse_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxPuttyExe.Text = fileDialog.FileName;
            }
        }
    }
}
