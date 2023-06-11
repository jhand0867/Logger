using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Logger
{
    public partial class UpdatesUpload : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public UpdatesUpload()
        {
            log.Info("Loading Updates");

            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Upload Update";
            openFileDialog.Filter = "Zip archive (*.zip) | *.zip";
            try
            {
                if (openFileDialog.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    log.Info($"Cleaning stagging area, making sure there is no previous sql");

                    if (File.Exists(Directory.GetCurrentDirectory() + @"\update\Logger\DB\LoggerUpdate.sql"))
                        File.Delete(Directory.GetCurrentDirectory() + @"\update\Logger\DB\LoggerUpdate.sql");

                    log.Info($"Opening file: {openFileDialog.FileName}");

                    ZipFile.ExtractToDirectory(openFileDialog.FileName, Directory.GetCurrentDirectory() + @"\update");

                    log.Info($"Archive: {openFileDialog.FileName} UNZIP to " + Directory.GetCurrentDirectory() + @"\update");

                    log.Info("Updating registry info");


                }
            }

            catch (AccessViolationException ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
            }

            catch (Exception ex1)
            {
                log.Error(ex1.ToString());

                MessageBox.Show("Installation failed, please contact vendor support");

                return;
            }
            lblUploadMessage.Text = "Installing Updates ...";

            uploadUpdates();

            lblUploadMessage.Text = "Updates Installed ...";

            this.Close();
        }

        private void uploadUpdates()
        {
            #region SQL Update
            ////
            /// Applying the INSERTS to the needed tables 
            /// 
            log.Info("Updating Logger");

            // AdvancedFilter
            // -- Select from sqlBuilder records source = 'U'
            // -- Select from sqlDetail records belonging to the source U

            log.Debug("Exporting to work tables sqlBuilderUpdate and sqlDetailUpdate User Filters");
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

            // start batch process

            log.Debug("About to dump sqlBuilderUpdate sqlDetailUpdate tables to sqlBuilderUpdate.sql");
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
                log.Error($"ERROR: Dump failed {ex.Message}");
            }

            // DataDescription
            // -- run script to import the data dataDescriptionUpdate.sql
            // -- run script to import sqlBuilderUpdate.sql and run script to add sqlBuilderUpdateU

            log.Debug("About to drop tables if exist dataDescription, sqlBuilder and sqlDetail");
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

        #endregion 
    }
}
