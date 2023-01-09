using System;
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
