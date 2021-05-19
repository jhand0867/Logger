﻿using System;
using System.Data;
using System.Windows.Forms;

namespace Logger

//todo: check the form refresh after log is attached for first time, not enabling scan option from menu.
{


    public partial class ProjectData : Form
    {


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static string logID = "";
        public ProjectData()
        {
            InitializeComponent();

            this.FormClosing += ProjectData_FormClosing;

        }
        public RefreshData ReloadDataView;

        private void ProjectData_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReloadDataView();
        }

        private void ProjectData_Load(object sender, EventArgs e)
        {
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

            if (dataGridView1.DataSource != null &&
                dataGridView1.Rows.Count > 0)
            {
                scanToolStripMenuItem.Enabled = true;
            }
            else
            {
                return;
            }

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
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            string logID = dgvr.Cells[0].Value.ToString();

            App.Prj.detachLogByID(logID);

            dataGridView1.DataSource = buildDataGridView1();

            if (dataGridView1.Rows.Count < 1)
                scanToolStripMenuItem.Enabled = false;

            fixLogNames(dataGridView1);

        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void transactionRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionSelected(00, true);
        }

        private void transactionReplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            // option is the entry position in the RecordTypes array

            string regExStr = App.Prj.RecordTypes[option, 0] + "%";
            string recordType = App.Prj.RecordTypes[option, 3];

            log.Debug($"Scanning for '{recordType}' records started");

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            int nrow = dgvr.Index;
            string logID = dgvr.Cells[0].Value.ToString();

            App.Prj.getData(regExStr, recordType, logID, option);
            log.Debug($"Scanning for '{recordType}' records completed");

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
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            scanToolStripMenuItem.DropDownItems[0].Enabled = true;

            for (int i = 4; i < dgvr.Cells.Count - 1; i++)
            {
                if (dgvr.Cells[i].Value.ToString() == "True" || dgvr.Cells[i].Value.ToString() == "true")
                {
                    scanToolStripMenuItem.DropDownItems[0].Enabled = false;
                    break;
                }
            }

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            logID = dgvr.Cells["id"].Value.ToString();

            LogView logView = new LogView();
            logView.Show();

        }

        private void hostToATMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];

            for (int x = 0, i = 4; i < dgvr.Cells.Count - 12; i++, x++)
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

            for (int x = 0, i = 21; i < dgvr.Cells.Count - 6; i++, x++)
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

            for (int x = 0, i = 27; i < dgvr.Cells.Count - 1; i++, x++)
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

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 25; i++)
                optionSelected(i, false);

            optionSelected(25, true);

            // completed();

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
