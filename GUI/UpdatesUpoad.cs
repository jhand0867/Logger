using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Logger.GUI
{
    public partial class UpdatesUpoad : Form
    {
        public UpdatesUpoad()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Upload Update";
            try
            {
                if (openFileDialog.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.AppStarting;
                    Process.Start("notepad.exe");
                }
            }
            catch (AccessViolationException ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
            }
            this.Close();
        }
    }
}
