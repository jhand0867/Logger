using System;
using System.Reflection;
using System.Windows.Forms;
// using System.Timers;
namespace Logger
{
    partial class AboutDialog : Form
    {
        internal Timer T = new Timer();

        public AboutDialog()
        {
            InitializeComponent();
            T.Interval = 8000;
            this.Text = String.Format("About {0}", AssemblyTitle);
            T.Tick += T_Tick;
            this.lblVersion.Text = AssemblyVersion;

            // setting application help
            this.KeyPreview = true;
            KeyListener.CustomKeyEvent += CustomKeyEventHandler;

        }

        private void T_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            T.Enabled = true;

        }

        private void CustomKeyEventHandler(object sender, KeyEventArgs e)
        {
            if(Equals(e.KeyCode,Keys.F1))
                Help.ShowHelp(this, "C:\\Users\\jhand\\Downloads\\manualTest\\manualtest.chm");
        }

        private void AboutDialog_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //private void AboutDialog_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue == 112)
        //        Help.ShowHelp(this, "C:\\Users\\jhand\\Downloads\\manualTest\\manualtest.chm");

        //}
    }
}
