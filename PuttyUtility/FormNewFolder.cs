using System;
using System.Windows.Forms;

namespace PuttyUtility
{
    public partial class FormNewFolder : Form
    {
        public string FolderName { get; private set; }

        public FormNewFolder()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            FolderName = textBoxFolderName.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
