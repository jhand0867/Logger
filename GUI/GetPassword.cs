using System;
using System.Windows.Forms;

namespace Logger
{
    public partial class GetPassword : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GetPassword()
        {
            InitializeComponent();
            log.Info("Requesting Password");
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "Joe1234")
                {
                    App.Prj.Admin = true;
                    log.Info("Password correct, access granted");
                    this.Close();
                    return;
                }
                else
                {
                    log.Info("Password incorrect, access denied");
                    return;

                }
            }
        }
    }
}
