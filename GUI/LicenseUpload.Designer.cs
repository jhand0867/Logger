
namespace Logger
{
    partial class LicenseUpload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLicenseUpload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLicenseUpload
            // 
            this.btnLicenseUpload.Location = new System.Drawing.Point(59, 109);
            this.btnLicenseUpload.Name = "btnLicenseUpload";
            this.btnLicenseUpload.Size = new System.Drawing.Size(299, 59);
            this.btnLicenseUpload.TabIndex = 0;
            this.btnLicenseUpload.Text = "License Upload";
            this.btnLicenseUpload.UseVisualStyleBackColor = true;
            this.btnLicenseUpload.Click += new System.EventHandler(this.btnLicenseUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(424, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(299, 59);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // LicenseUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 293);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLicenseUpload);
            this.Name = "LicenseUpload";
            this.Text = "LicenseUpload";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLicenseUpload;
        private System.Windows.Forms.Button btnCancel;
    }
}