using LoggerUtil;

using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Logger
{
    public partial class MainW : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly int MIN_BACKUP_DAYS = 5;
        private static readonly string SQL_SRC_FOLDER = @"C:\Logger Update Build\sources\Data";
        private static readonly string APP_SRC_FOLDER = @"C:\Logger Update Build\sources\App";
        private static readonly string SQL_OUT_FOLDER = @"C:\Logger Update Build\output\Data";
        private static readonly string APP_OUT_FOLDER = @"C:\Logger Update Build\output\App";

        public MainW()
        {
            log.Info($"Logger Started");

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            InitializeComponent();
            this.BackColor = System.Drawing.Color.LightGray;
            mainMenu.ForeColor = System.Drawing.Color.White;
            //this.mainMenu.Font = new Font("Helvetica", 18);
            this.projectsToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10);
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10);
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10);
            this.Text = "Logger NDC Inspector";

            // check if admin switch was included
            log.Info($"Checking for admin");
            App.Prj.ValidateUser(this.adminToolStripMenuItem1);


            // display version

            lblVersion.Text = "Version " + getVersion();
            //lblVersion.ForeColor = System.Drawing.Color.White;

            log.Info($"Logger Version {lblVersion.Text}");

            ToolStripManager.Renderer = new CustomProfessionalRenderer();
            //mainMenu.Renderer = new RedTextRenderer();

            log.Info("Checking License");

            License license = new License();

            App.Prj.LicenseKey = license.VerifyLicenseRegistry();
            App.Prj.Permissions = license.GetPermissions(App.Prj.LicenseKey);

            double num = new JulianDate().JD(DateTime.Now);
            if (App.Prj.LicenseKey != null && Convert.ToDouble(App.Prj.LicenseKey.EndDate) >= num)
            {
                log.Info("Licesense is current");
                return;
            }

            log.Info("License is not current");
            log.Debug($"License: {license.ToString()}");

            App.Prj.Permissions = "Customer: 0001\n" +
                "LicenseType: \n" +
                "Starts on: \n" +
                "Ends on: \n" +
                "Project Options: 01100000\n" +
                "Data Options: 11100000\n" +
                "Scan Options: 00000000\n" +
                "LogView Logs Options: 11100000\n" +
                "LogView Files Options: 11111000\n" +
                "LogView Filter Options: 11000000\n";
        }

        internal static bool deleteSelf()
        {
            try
            {


                Process.Start(new ProcessStartInfo()
                {
                    Arguments = "/C choice /C Y /N /D Y /T 3 & del  \"" + Process.GetCurrentProcess().MainModule.FileName + "\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                });
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }
        private void OnProcessExit(object sender, EventArgs e)
        {
            log.Info("Checking out of Logger now bye bye");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projects prForm = new Projects();
            prForm.BringToFront();
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
            System.Windows.Forms.Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void MainW_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.LightGray;
            SplashWindow splasWindow = new SplashWindow();
            splasWindow.BringToFront();
            Cursor.Hide();
            splasWindow.ShowDialog();

            // check if recent update was done
            // Compare the staging folder existance and check creation date
            // Compute weekend days and add to the minimum 

            string message = @"Logger recent update kept a backup, Would you like to remove the backup?";

            if (Directory.Exists(Directory.GetCurrentDirectory() + @"\update"))
            {
                DateTime checkLastUpdate = Directory.GetCreationTime(Directory.GetCurrentDirectory() + @"\update");

                int nonWorkingDays = 0;

                for (int i = 0; i <= MIN_BACKUP_DAYS; i++)
                {
                    if ((checkLastUpdate.AddDays(i).DayOfWeek == DayOfWeek.Saturday) ||
                        (checkLastUpdate.AddDays(i).DayOfWeek == DayOfWeek.Sunday))
                        nonWorkingDays++;
                }

                DialogResult result = MessageBox.Show(message, "Backup Copy", MessageBoxButtons.YesNo);
                if ((result == DialogResult.Yes) ||
                   (DateTime.Compare(checkLastUpdate.AddDays(MIN_BACKUP_DAYS + nonWorkingDays), DateTime.Now) < 0))
                {
                    Directory.Delete(Directory.GetCurrentDirectory() + @"\update", true);
                }

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseUpload lu = new LicenseUpload();
            lu.ShowDialog();
        }

        private void aboutLoggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog about = new AboutDialog();
            about.ShowDialog();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem O = (ToolStripMenuItem)sender;

            for (int i = O.DropDownItems.Count; i > 2; i--)
            {
                O.DropDownItems.RemoveAt(i - 1);
            }

            DbCrud DB = new DbCrud();

            System.Data.DataTable dt = DB.GetTableFromDb("SELECT * FROM generalInfo order by id desc");
            foreach (DataRow row in dt.Rows)
            {
                ToolStripMenuItem MI = new ToolStripMenuItem();
                MI.Name = row["logID"].ToString();
                MI.Text = row["logName"].ToString();

                O.DropDownItems.Add(MI);
            }
        }

        private void fileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem O = (ToolStripMenuItem)e.ClickedItem;

            if (O.Name != "exitToolStripMenuItem")
            {
                LogView LV = new LogView();
                LV.Tag = O.Name + ";" + O.Text;
                LV.BringToFront();
                LV.Show();
            }

        }

        private void updatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Debug("About to Update Logger");
            int exitCode;
            Process p;

            ProcessStartInfo installAppUpdates = new ProcessStartInfo(Directory.GetCurrentDirectory() + @"\update.exe");

            installAppUpdates.CreateNoWindow = true;
            installAppUpdates.UseShellExecute = false;
            //installAppUpdates.RedirectStandardOutput = true;
            //installAppUpdates.RedirectStandardError = true;
            installAppUpdates.WorkingDirectory = "Update";
            installAppUpdates.Verb = "RUNAS";
            installAppUpdates.UseShellExecute = true;

            try
            {
                p = Process.Start(installAppUpdates);
                p.WaitForExit();

                //string output = p.StandardOutput.ReadToEnd();
                //string error = p.StandardError.ReadToEnd();

                exitCode = p.ExitCode;

                p.Close();

            }
            catch (Exception ex)
            {
                log.Error($"ERROR: Dump failed {ex.Message}");
            }
        }

        private string getVersion()
        {
            //int offset = 0;
            //int keyLenght = 0;

            //RegistryManager rm = new RegistryManager();
            //string keyContent = rm.ReadKey(@"SOFTWARE\Logger");
            //string[] subKeys = keyContent.Split('\n');

            //offset = subKeys[0].IndexOf("=") + 1;
            //keyLenght = subKeys[0].Length;

            //return subKeys[0].Substring(offset, keyLenght - offset);

            return Assembly.GetExecutingAssembly().GetName().Version.ToString();

        }

        private string getUpdateFlag()
        {
            int offset = 0;
            int keyLenght = 0;

            RegistryManager rm = new RegistryManager();
            string keyContent = rm.ReadKey(@"SOFTWARE\Logger");
            string[] subKeys = keyContent.Split('\n');
            string keyValue = string.Empty;
            foreach (string subKey in subKeys)
            {
                if (subKey.Contains("Updated="))
                {
                    offset = subKey.IndexOf("=") + 1;
                    keyLenght = subKey.Length;
                    keyValue = subKey.Substring(offset, keyLenght - offset);
                }
            }


            return keyValue;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void genrateUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateDataUpdates();
            generateAppUpdates();

        }
        #region SQL Update
        private void generateDataUpdates()
        {

            if (Directory.Exists(SQL_SRC_FOLDER))
            {
                Directory.Delete(SQL_SRC_FOLDER, true);
            }
            Directory.CreateDirectory(SQL_SRC_FOLDER);

            if (Directory.Exists(SQL_OUT_FOLDER))
            {
                Directory.Delete(SQL_OUT_FOLDER, true);
            }
            Directory.CreateDirectory(SQL_OUT_FOLDER);


            DbCrud db = new DbCrud();
            db.crudToDb(@"drop table if exists sqlDetailUpdate;
            create table sqlDetailUpdate as
            SELECT b.sqlId,
                   b.fieldName,
                   b.condition,
                   b.fieldValue,
                   b.andOr,
                   b.fieldOutput,
                   b.filterKey,
                   b.lineNumber
              FROM sqlBuilder as a JOIN sqlDetail as b
              ON a.filterKey = b.filterKey
              WHERE a.source='I';
            drop table if exists sqlBuilderUpdate;
            create table sqlBuilderUpdate as
            SELECT name,
                   description,
                   date,
                   source,
                   filterKey
              FROM sqlBuilder 
              WHERE source='I';");

            int exitCode;
            Process p;


            //System.Diagnostics.Process.Start(Application.StartupPath + "\\Data\\LoggerUpdate.bat");

            ProcessStartInfo pI = new ProcessStartInfo();
            pI.FileName = System.Windows.Forms.Application.StartupPath + "\\Data\\LoggerUpdate.bat";
            pI.CreateNoWindow = true;
            pI.UseShellExecute = false;
            pI.RedirectStandardOutput = true;
            pI.RedirectStandardError = true;

            //pI.WorkingDirectory = SQL_OUT_FOLDER;
            

            try
            {
                log.Info($"Starting to process {pI.FileName} ");

                p = Process.Start(pI);
                p.WaitForExit();

                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();

                exitCode = p.ExitCode;

                p.Close();
            }
            catch (Exception ex)
            {
                log.Error($"ERROR: Dump failed {ex.Message}");
            }

            try
            {
                foreach (string item in (Directory.GetFileSystemEntries(SQL_SRC_FOLDER)))
                {
                    File.Copy(item, SQL_OUT_FOLDER + "\\" + item.Substring(item.LastIndexOf("\\"), (item.Length) - (item.LastIndexOf("\\"))), true);

                }
            }
            catch (Exception ex)
            {

                log.Error($"ERROR: Unable to copy SQL source to Output - Dump failed {ex.Message}");
            }

        }
        #endregion

        #region App Update

        // What to do to update the appplication
        // if there are any files in the path
        // - Drop the needed members in the  a new Update Build folder sources\App
        //      Include that file in the Logger Updata archive with its version
        //
        // 
        internal bool generateAppUpdates()
        {
            // stage the area


            if (Directory.Exists( APP_OUT_FOLDER ))
            {
                Directory.Delete(APP_OUT_FOLDER, true);
            }
            Directory.CreateDirectory(APP_OUT_FOLDER);

            // there are some files here, let's open a new archive

            try
            {
                foreach (string item in (Directory.GetFileSystemEntries(APP_SRC_FOLDER)))
                {
                    File.Copy(item, APP_OUT_FOLDER + "\\" + item.Substring(item.LastIndexOf("\\"), (item.Length) - (item.LastIndexOf("\\"))), true);

                }
            }
            catch (Exception ex)
            {

                log.Error($"ERROR: Unable to copy APP source to Output - Dump failed {ex.Message}");
            }


            return true;
        }




        #endregion
    }
}
