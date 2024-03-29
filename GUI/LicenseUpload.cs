﻿using System;
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
            LoggerLicense.LoggerLic license = new LoggerLicense.LoggerLic();
            if (txtLicenseFile.Text.Contains("Logger.Lic"))
            {
                license.UploadLicense(txtLicenseFile.Text);
                MessageBox.Show("Upload Successful");
                //license.GetPermissions();
                //MLH 
                App.Prj.LicenseKey = license.VerifyLicenseRegistry();
                App.Prj.Permissions = license.GetPermissions(App.Prj.LicenseKey);

                this.Close();
            }
            else
            {
                MessageBox.Show("File is not a valid license");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Upload License";
            try
            {
                if (openFileDialog.ShowDialog((IWin32Window)this) == DialogResult.OK)
                    txtLicenseFile.Text = openFileDialog.FileName;
            }
            catch (AccessViolationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
