using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Logger

{


    public partial class ProjectData : Form
    {


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static string logID = "";
        private static int HOSTTOEXIT = 19;
        private static int EXITTOHOST = 26;
        private static int HOSTTOATMOPT = 4;
        private static int HOSTTOATMOPTTOSKIP = 12;
        private static int ATMTOHOSTOPT = 22;
        private static int ATMTOHOSTOPTTOSKIP = 6;
        private static int EMVOPT = 28;


        public Dictionary<int, int> optionDbfieldMatch = new Dictionary<int, int>();


        public ProjectData()
        {
            InitializeComponent();

            this.FormClosing += ProjectData_FormClosing;
            this.menuStrip1.Font = new System.Drawing.Font("Arial", 10);
            this.logsToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10);
            this.scanToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10);
            
        }
        public RefreshData ReloadDataView;

        private void ProjectData_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReloadDataView();
        }

        private void ProjectData_Load(object sender, EventArgs e)
        {
            App app = new App();
            app.MenuPermissions(App.Prj.Permissions, this.logsToolStripMenuItem.DropDownItems, menusTypes.ProjectData);
            app.MenuPermissions(App.Prj.Permissions, this.scanToolStripMenuItem.DropDownItems, menusTypes.ScanOptions);

            scanToolStripMenuItem.Enabled = false;
            dataGridView1.DataSource = buildDataGridView1();

            for (int i = 4; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "File Name";
            dataGridView1.Columns[2].Width = 400;
            dataGridView1.Columns[3].HeaderText = "Uploaded on";
            dataGridView1.Columns[3].Width = 250;

            if (this.dataGridView1.DataSource == null || this.dataGridView1.Rows.Count <= 0)
                return;
            this.scanToolStripMenuItem.Enabled = true;

            fixLogNames(dataGridView1);
            dataGridView1.Refresh();
        }

        private void attachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Upload Log";
            try
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {

                    this.Cursor = Cursors.AppStarting;

                    App.Prj.uploadLog(ofd.FileName);
                    this.Cursor = Cursors.Default;
                    scanToolStripMenuItem.Enabled = true;
                    dataGridView1.DataSource = buildDataGridView1();
                    fixLogNames(dataGridView1);
                }

            }
            catch (AccessViolationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fixLogNames(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string logLocation = dgv.Rows[i].Cells[2].Value.ToString();
                dgv.Rows[i].Cells[2].ToolTipText = logLocation;
                int logIndex = logLocation.LastIndexOf(@"\") + 1;
                dgv.Rows[i].Cells[2].Value = logLocation.Substring(logIndex, logLocation.Length - logIndex);
                dgv.Rows[i].Cells[2].Tag = 0;

            }
        }

        public DataTable buildDataGridView1()
        {
            DataTable dtLogs = new DataTable();

            string sql = @"SELECT * FROM logs WHERE prjKey ='" + App.Prj.Key + "' AND uploaded = 1";
            DbCrud db = new DbCrud();
            dtLogs = db.GetTableFromDb(sql);
            return dtLogs;
        }

        private void detachToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show($"This will remove the log\n Do you want to continue",
                "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

                string logID = dgvr.Cells[0].Value.ToString();

                App.Prj.detachLogByID(logID);

                dataGridView1.DataSource = buildDataGridView1();

                if (dataGridView1.Rows.Count < 1)
                    scanToolStripMenuItem.Enabled = false;

                fixLogNames(dataGridView1);
            }

        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void transactionRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // optionDbfieldMatch.Add(09, 07);       // enhancedParametersLoad

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            if (dgvr.Cells[09].Value.ToString() != "True" && dgvr.Cells[09].Value.ToString() != "true")
            {
                optionSelected(07, false);
            }

            optionSelected(00, true);
        }

        private void transactionReplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // optionDbfieldMatch.Add(09, 07);       // enhancedParametersLoad

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            if (dgvr.Cells[09].Value.ToString() != "True" && dgvr.Cells[09].Value.ToString() != "true")
            {
                optionSelected(07, false);
            }

            optionSelected(01, true);
        }


        private void screensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(02, true);
        }

        private void statesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(03, true);
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(04, true);
        }

        private void fITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(05, true);
        }

        private void configurationIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(06, true);
        }

        private void enhancedConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(07, true);
        }

        private void mACToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(08, true);
        }

        private void dateAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(09, true);

        }

        private void dispenseCurrencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(10, true);
        }

        private void iCCCurrencyDOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(11, true);
        }

        private void iCCTransactionDOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(12, true);
        }

        private void iCCLanguageSupportTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(13, true);

        }

        private void iCCTerminalDOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(14, true);
        }

        private void iCCTerminalAcceptableAIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(15, true);

        }
        private void optionSelected(int option, bool refresh)
        {
            // MLH changed here to allow ALL when at least one is still to SCAN
            // option is the entry position in the RecordTypes array

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            int nrow = dgvr.Index;

            if (option >= 0)
            {
                string regExStr = App.Prj.RecordTypes[option, 0] + "%";
                string recordType = App.Prj.RecordTypes[option, 3];

                log.Debug($"Scanning for '{recordType}' records started");

                string logID = dgvr.Cells[0].Value.ToString();

                App.Prj.getData(regExStr, recordType, logID, option);
                log.Debug($"Scanning for '{recordType}' records completed");
            }
            if (refresh)
            {
                dataGridView1.DataSource = buildDataGridView1();
                dataGridView1.Rows[nrow].Selected = true;
                //dataGridView1
                fixLogNames(dataGridView1);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show(e.RowIndex.ToString());
        }

        private void scanToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MLH changed here to allow ALL when at least one message type is still to SCAN

            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            scanToolStripMenuItem.DropDownItems[0].Enabled = false;

            for (int i = 4; i < dgvr.Cells.Count - 1; i++)
            {
                if (i == EXITTOHOST || i == HOSTTOEXIT) continue;

                if (dgvr.Cells[i].Value.ToString() != "True" && dgvr.Cells[i].Value.ToString() != "true")
                { 
                    scanToolStripMenuItem.DropDownItems[0].Enabled = true;
                    break;
                }
            }

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            logID = dgvr.Cells["id"].Value.ToString();

            LogView logView = new LogView();
            logView.BringToFront();
            logView.Show();

        }

        private void hostToATMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            for (int x = 0, i = HOSTTOATMOPT; i < dgvr.Cells.Count - HOSTTOATMOPTTOSKIP; i++, x++)
            {
                if (dgvr.Cells[i].Value.ToString() == "True" || dgvr.Cells[i].Value.ToString() == "true")
                {
                    hostToATMToolStripMenuItem1.DropDownItems[x].Enabled = false;
                }
                else
                {
                    hostToATMToolStripMenuItem1.DropDownItems[x].Enabled = true;
                }
            }
        }

        private void aTMToHostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            for (int x = 0, i = ATMTOHOSTOPT; i < dgvr.Cells.Count - ATMTOHOSTOPTTOSKIP; i++, x++)
            {
                if (dgvr.Cells[i].Value.ToString() == "True" || dgvr.Cells[i].Value.ToString() == "true")
                {
                    aTMToHostToolStripMenuItem.DropDownItems[x].Enabled = false;
                }
                else
                {
                    aTMToHostToolStripMenuItem.DropDownItems[x].Enabled = true;
                }
            }
        }

        private void eMVConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            for (int x = 0, i = EMVOPT; i < dgvr.Cells.Count - 1; i++, x++)
            {
                if (dgvr.Cells[i].Value.ToString() == "True" || dgvr.Cells[i].Value.ToString() == "true")
                {
                    eMVConfigurationToolStripMenuItem.DropDownItems[x].Enabled = false;
                }
                else
                {
                    eMVConfigurationToolStripMenuItem.DropDownItems[x].Enabled = true;
                }
            }
        }

        private void solicitedStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(16, true);
        }

        private void unsolicitedStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(17, true);
        }

        private void encryptorInitializationDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(18, true);
        }

        private void uploadEJDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(19, true);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            optionSelected(20, true);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            optionSelected(21, true);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            optionSelected(22, true);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            optionSelected(23, true);
        }
        private void terminalCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(24, true);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            optionSelected(25, true);

        }

        private void voiceGuidanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(26, true);
        }


        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MLH changed here to allow ALL when at least one is still to SCAN
            log.Debug("All options selected for Scan");
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            if (optionDbfieldMatch.Count == 0)
            {
                //optionDbfieldMatch.Add(#column in Log table, #entry in RecordTypes);       

                optionDbfieldMatch.Add(09, 07);     // enhancedParametersLoad
                optionDbfieldMatch.Add(05, 03);     // states
                optionDbfieldMatch.Add(06, 04);     // configParametersLoad
                optionDbfieldMatch.Add(07, 05);     // fit
                optionDbfieldMatch.Add(08, 06);     // configID
                optionDbfieldMatch.Add(04, 02);     // screens
                optionDbfieldMatch.Add(10, 08);     // mac
                optionDbfieldMatch.Add(11, 09);     // dateandtime
                optionDbfieldMatch.Add(12, 10);     // dispenserCurrency
                optionDbfieldMatch.Add(13, 01);     // treply
                optionDbfieldMatch.Add(14, 25);     // interactiveTranResponse
                optionDbfieldMatch.Add(15, 23);     // extendedEncrypKeyChange
                optionDbfieldMatch.Add(16, 20);     // ejAckBlock
                optionDbfieldMatch.Add(17, 21);     // ejAckStop
                optionDbfieldMatch.Add(18, 22);     // ejOptionsTimers
                optionDbfieldMatch.Add(19, 99);        // hostToExit
                optionDbfieldMatch.Add(20, 24);     // terminalCommands
                optionDbfieldMatch.Add(21, 26);     // voiceGuidance
                optionDbfieldMatch.Add(22, 00);     // treq
                optionDbfieldMatch.Add(23, 16);     // solicitedStatus
                optionDbfieldMatch.Add(24, 17);     // unsolicitedStatus
                optionDbfieldMatch.Add(25, 18);     // encryptorInitData
                optionDbfieldMatch.Add(26, 99);        // exitToHost
                optionDbfieldMatch.Add(27, 19);     // uploadEjData
                optionDbfieldMatch.Add(28, 11);     // iccCurrencyDOT
                optionDbfieldMatch.Add(29, 12);     // iccTransactionDOT
                optionDbfieldMatch.Add(30, 13);     // iccLanguageSupportT
                optionDbfieldMatch.Add(31, 14);     // iccTerminalDOT
                optionDbfieldMatch.Add(32, 15);     // iccApplicationIDT

            }

            // using Dictionary order

            foreach (int recKey in optionDbfieldMatch.Keys)
            {
                int recType = optionDbfieldMatch[recKey];

                if (recType == 99 ) continue;

                if (dgvr.Cells[recKey].Value.ToString() != "True" && dgvr.Cells[recKey].Value.ToString() != "true")
                {
                    optionSelected(recType, false);
                }
            }
            
            optionSelected(-1, true);

        }

        private void completed()
        {
            MessageBox.Show("Completed");
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
                detachToolStripMenuItem.Enabled = false;
            else
                detachToolStripMenuItem.Enabled = true;
        }

    }
}
