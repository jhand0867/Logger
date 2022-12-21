using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Windows.Forms;

namespace Logger
{
    public partial class MainW : Form
    {
        public MainW()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.LightGray;
            //this.mainMenu.Font = new Font("Helvetica", 18);
            this.projectsToolStripMenuItem.Font = new Font("Arial", 10);
            this.aboutToolStripMenuItem.Font = new Font("Arial", 10);
            this.fileToolStripMenuItem.Font = new Font("Arial", 10);

            App.Prj.ValidateUser(this.adminToolStripMenuItem1);

            //DbCrud DB = new DbCrud();

            //DataTable dt = DB.GetTableFromDb("SELECT * FROM generalInfo");
            //foreach (DataRow row in dt.Rows)
            //{
            //    ToolStripMenuItem MI = new ToolStripMenuItem();
            //    MI.Name = row["logID"].ToString();
            //    MI.Text = row["logName"].ToString();

            //    this.fileToolStripMenuItem.DropDownItems.Add(MI);
            //}

            //int i = fileToolStripMenuItem.DropDownItems.Count;

            ToolStripManager.Renderer = new CustomProfessionalRenderer();
            //mainMenu.Renderer = new RedTextRenderer();

            License license = new License();
            App.Prj.Permissions = license.GetPermissions();
            App.Prj.LicenseKey = license.VerifyLicenseRegistry();
            double num = new JulianDate().JD(DateTime.Now);
            if (App.Prj.LicenseKey != null && Convert.ToDouble(App.Prj.LicenseKey.EndDate) >= num)
                return;
            App.Prj.Permissions = "Customer: 0001\n" +
                "LicenseType: \n" +
                "Starts on: \n" +
                "Ends on: \n" +
                "Project Options: 01100000\n" +
                "Data Options: 01100000\n" +
                "Scan Options: 00000000\n" +
                "LogView Logs Options: 11100000\n" +
                "LogView Files Options: 11111000\n" +
                "LogView Filter Options: 11000000\n";

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
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainW_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.LightGray;
            SplashWindow splasWindow = new SplashWindow();
            splasWindow.BringToFront();
            Cursor.Hide();
            splasWindow.ShowDialog();

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

            DataTable dt = DB.GetTableFromDb("SELECT * FROM generalInfo order by id desc");
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

            // AdvancedFilter
            // -- Select from sqlBuilder records source = 'U'
            // -- Select from sqlDetail records belonging to the source U

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
              WHERE a.source='U';
            drop table if exists sqlBuilderUpdate;
            create table sqlBuilderUpdate as
            SELECT name,
                   description,
                   date,
                   source,
                   filterKey
              FROM sqlBuilder 
              WHERE source='U';");




            int exitCode;
            Process p;

            ProcessStartInfo pI = new ProcessStartInfo("cmd", "/c" + " sqlite3.exe logger.db \".dump sqlBuilderUpdate sqlDetailUpdate --data-only \"  > sqlBuilderUpdateU.sql");
            pI.CreateNoWindow = true;
            pI.UseShellExecute = false;
            pI.RedirectStandardOutput = true;
            pI.RedirectStandardError = true;
            pI.WorkingDirectory = @"data";

            try
            {
                p = Process.Start(pI);
                p.WaitForExit();

                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();

                exitCode = p.ExitCode;

                p.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


            //MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //MessageBox.Show("ExitCode: " + exitCode.ToString(), "ExecuteCommand");


            // DataDescription
            // -- run script to import the data dataDescriptionUpdate.sql
            // -- run script to import sqlBuilderUpdate.sql and run script to add sqlBuilderUpdateU

            string inputScript = String.Empty;
            inputScript = @"drop table if exists dataDescription;
                            drop table if exists sqlBuilder;
                            drop table if exists sqlDetail;" + Environment.NewLine;
            
            inputScript += System.IO.File.ReadAllText(@"data\LoggerUpdate.sql") + Environment.NewLine;

            inputScript += System.IO.File.ReadAllText(@"data\sqlBuilderUpdateU.sql");

            inputScript = inputScript.Replace("sqlDetailUpdate", "sqlDetail");
            inputScript = inputScript.Replace("sqlBuilderUpdate", "sqlBuilder");

            // execute script 
            db.crudToDb(inputScript);

            
            



        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void genrateUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

            ProcessStartInfo pI = new ProcessStartInfo("cmd", "/c" + " sqlite3.exe logger.db \".dump sqlBuilderUpdate sqlDetailUpdate dataDescription\" > LoggerUpdate.sql");
//            ProcessStartInfo pI = new ProcessStartInfo("cmd", "/c" + " sqlite3.exe logger.db \".dump dataDescription\" >> sqlBuilderUpdate.sql");
            pI.CreateNoWindow = true;
            pI.UseShellExecute= false;
            pI.RedirectStandardOutput = true;
            pI.RedirectStandardError = true;
            pI.WorkingDirectory= @"data";

            p = Process.Start(pI);
            p.WaitForExit();

            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();

            exitCode = p.ExitCode;

            MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            MessageBox.Show("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
            p.Close();

        }
    }
}
