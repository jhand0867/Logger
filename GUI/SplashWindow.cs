using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger
{
    public partial class SplashWindow : Form
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        private void SplashWindow_Shown(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            timer1.Enabled = true;
            timer1.Tick += new System.EventHandler(OnTimerEvent);
            return;
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            Cursor.Show();
            this.Close();
        }
    }
}
