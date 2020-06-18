namespace Logger
{
    partial class LogData
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
            this.txtGroup8 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimestamp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtGroup8
            // 
            this.txtGroup8.Location = new System.Drawing.Point(150, 157);
            this.txtGroup8.Multiline = true;
            this.txtGroup8.Name = "txtGroup8";
            this.txtGroup8.Size = new System.Drawing.Size(608, 477);
            this.txtGroup8.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "TimeStamp:";
            // 
            // lblTimestamp
            // 
            this.lblTimestamp.AutoSize = true;
            this.lblTimestamp.Location = new System.Drawing.Point(146, 27);
            this.lblTimestamp.Name = "lblTimestamp";
            this.lblTimestamp.Size = new System.Drawing.Size(44, 20);
            this.lblTimestamp.TabIndex = 2;
            this.lblTimestamp.Text = "ttttttt";
            // 
            // LogData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 674);
            this.Controls.Add(this.lblTimestamp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGroup8);
            this.Name = "LogData";
            this.Text = "LogData";
            this.Load += new System.EventHandler(this.LogData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGroup8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTimestamp;
    }
}