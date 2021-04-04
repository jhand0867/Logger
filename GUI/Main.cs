using System;
using System.Windows.Forms;
using System.Timers;

namespace Logger
{
    public partial class MainW : Form
    {
        public MainW()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.LightGray;

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projects prForm = new Projects();
            prForm.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString());
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projects projectInfoForm = new Projects();
            projectInfoForm.ShowDialog();
        }

        private void MainW_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainW_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.LightGray;

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Timers.Timer T = new System.Timers.Timer(100);
            AboutDialog about = new AboutDialog();
            //about.Controls["splashTimer"]
            about.ShowDialog();
            //T.Start();
            //about.Close();
        }
    }
}
