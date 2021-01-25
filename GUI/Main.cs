using System;
using System.Windows.Forms;

namespace Logger
{
    public partial class MainW : Form
    {
        public MainW()
        {
            InitializeComponent();

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
            MessageBox.Show(sender.ToString());
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projects prForm = new Projects();
            prForm.ShowDialog();
        }

        private void MainW_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
