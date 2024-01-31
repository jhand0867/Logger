using System;
using System.Windows.Forms;

namespace Logger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainW());
            }
            catch (Exception appEx)
            {

                MessageBox.Show(appEx.Message);
            }
            

        }
    }
}
