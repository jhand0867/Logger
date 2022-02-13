using System;
using System.Windows.Forms;

namespace Logger
{
    public partial class LicenseUpload : Form
    {
        public LicenseUpload()
        {
            InitializeComponent();
        }

        private void btnLicenseUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Upload License";
            try
            {
                if (openFileDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                    return;
                this.Cursor = Cursors.AppStarting;
                License license = new License();
                if (openFileDialog.FileName.Contains("Logger.Lic"))
                {
                    license.UploadLicense(openFileDialog.FileName);
                    int num = (int)MessageBox.Show("Upload Successful");
                    license.GetPermissions();
                    this.Close();
                }
                else
                {
                    int num1 = (int)MessageBox.Show("File is not a valid license");
                }
                this.Cursor = Cursors.Default;
            }
            catch (AccessViolationException ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
