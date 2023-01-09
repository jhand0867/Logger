
namespace Logger
{
    partial class UpdatesUpload
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
            this.lblUpdateSelectFile = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lblUploadMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUpdateSelectFile
            // 
            this.lblUpdateSelectFile.AutoSize = true;
            this.lblUpdateSelectFile.Location = new System.Drawing.Point(47, 61);
            this.lblUpdateSelectFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateSelectFile.Name = "lblUpdateSelectFile";
            this.lblUpdateSelectFile.Size = new System.Drawing.Size(121, 13);
            this.lblUpdateSelectFile.TabIndex = 0;
            this.lblUpdateSelectFile.Text = "Select Update Package";
            this.lblUpdateSelectFile.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(252, 55);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(78, 24);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // lblUploadMessage
            // 
            this.lblUploadMessage.AutoSize = true;
            this.lblUploadMessage.Location = new System.Drawing.Point(75, 109);
            this.lblUploadMessage.Name = "lblUploadMessage";
            this.lblUploadMessage.Size = new System.Drawing.Size(35, 13);
            this.lblUploadMessage.TabIndex = 2;
            this.lblUploadMessage.Text = "label1";
            // 
            // UpdatesUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 144);
            this.Controls.Add(this.lblUploadMessage);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lblUpdateSelectFile);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UpdatesUpload";
            this.Text = "Updates Upoad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUpdateSelectFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lblUploadMessage;
    }
}