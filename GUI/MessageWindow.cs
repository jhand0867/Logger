using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;


namespace Logger.GUI
{
    public partial class MessageWindow : Form
    {
        public MessageWindow()
        {
            InitializeComponent();
        }

        private void MessageWindow_Load(object sender, EventArgs e)
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 100;
            timer.Elapsed += onTimeEvent;
            timer.Enabled = true;
            timer.Start();

            
        }

        private void onTimeEvent(object sender, ElapsedEventArgs e)
        {
            this.Close();
        }
    }
}
