
namespace Logger
{
    partial class LoadQuery
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
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbQueryName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(469, 145);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(127, 41);
            this.btOk.TabIndex = 11;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(172, 145);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(127, 41);
            this.btCancel.TabIndex = 10;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name ";
            // 
            // cbQueryName
            // 
            this.cbQueryName.FormattingEnabled = true;
            this.cbQueryName.Location = new System.Drawing.Point(172, 66);
            this.cbQueryName.Name = "cbQueryName";
            this.cbQueryName.Size = new System.Drawing.Size(424, 33);
            this.cbQueryName.TabIndex = 12;
            this.cbQueryName.SelectedIndexChanged += new System.EventHandler(this.cbQueryName_SelectedIndexChanged);
            this.cbQueryName.Click += new System.EventHandler(this.cbQueryName_Click);
            // 
            // LoadQuery
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(704, 249);
            this.ControlBox = false;
            this.Controls.Add(this.cbQueryName);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.label1);
            this.Name = "LoadQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoadQuery";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbQueryName;
    }
}