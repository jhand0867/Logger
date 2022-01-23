
namespace Logger.GUI
{
    partial class UpdatesUpoad
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
            this.SuspendLayout();
            // 
            // lblUpdateSelectFile
            // 
            this.lblUpdateSelectFile.AutoSize = true;
            this.lblUpdateSelectFile.Location = new System.Drawing.Point(94, 117);
            this.lblUpdateSelectFile.Name = "lblUpdateSelectFile";
            this.lblUpdateSelectFile.Size = new System.Drawing.Size(237, 25);
            this.lblUpdateSelectFile.TabIndex = 0;
            this.lblUpdateSelectFile.Text = "Select Update Package";
            this.lblUpdateSelectFile.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(386, 111);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(156, 46);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // UpdatesUpoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lblUpdateSelectFile);
            this.Name = "UpdatesUpoad";
            this.Text = "Updates Upoad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUpdateSelectFile;
        private System.Windows.Forms.Button btnSelectFile;
    }
}