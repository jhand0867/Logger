using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
//using System.Windows;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace Logger
{
    public delegate DataGridViewRow ReceiveLogData();
    //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
    //System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public struct scSqlLikeAndRegExp
    {
        private string sqlLike;
        private string regExpStr;

        public string SqlLike { get => sqlLike; set => sqlLike = value; }
        public string RegExpStr { get => regExpStr; set => regExpStr = value; }
    }
    public partial class LogView : Form
    {
        // needed to set the column's width of the datagridview.
        public const int TIMESTAMP_COLUMN_WIDTH = 165;

        // needed for printing 

        private Font printFont;
        private string streamToPrint;

        int count = 0;
        int pagesCount = 0;

        public LogView()
        {
            InitializeComponent();
            this.FormClosing += LogView_FormClosing;
            dgvLog.DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 9);
            //dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            mainMenu.Font = new Font("Arial", 10);
            fileToolStripMenuItem.Font = new Font("Arial", 10);
            viewToolStripMenuItem.Font = new Font("Arial", 10);
            searchToolStripMenuItem.Font = new Font("Arial", 10);
            contextMenuStrip1.Font = new Font("Arial", 10);
            //cbQueryName.Font = new Font("Arial", 10);
        }

        private void LogView_FormClosing(object sender, FormClosingEventArgs e)
        {
            dgvLog.DataSource = null;
            dgvLog.Rows.Clear();
            GC.Collect();
        }


        private void LogView_Load(object sender, EventArgs e)
        {
            App app = new App();
            app.MenuPermissions(App.Prj.Permissions, this.viewToolStripMenuItem.DropDownItems, menusTypes.LogViewLogs);
            app.MenuPermissions(App.Prj.Permissions, this.fileToolStripMenuItem.DropDownItems, menusTypes.LogViewFiles);
            app.MenuPermissions(App.Prj.Permissions, this.searchToolStripMenuItem.DropDownItems, menusTypes.LogViewFilter);
            try
            {
                //dgvLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                // Tag is populated with the logID;MessageClass
                // MessageClass must match a SearchCondition table record.

                if (Tag != null)
                {
                    string[] tagSplit = Tag.ToString().Split(';');
                    ProjectData.logID = tagSplit[0];
                    scSqlLikeAndRegExp sql = new scSqlLikeAndRegExp();

                    if (Tag.ToString().IndexOf("log") != -1)
                    {
                        dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);
                    }
                    else
                    {
                        sql = searchConditionBuilt(tagSplit[1]);
                        dgvLog.DataSource = App.Prj.getALogByIDWithRegExp(ProjectData.logID, sql.SqlLike, sql.RegExpStr);
                    }
                }
                else
                {
                    dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);
                }

                writeGeneralInfo(ProjectData.logID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (dgvLog.DataSource == null)
            {
                MessageBox.Show("unable to access database, please retry");
                return;
            }

            if (dgvLog.Rows.Count == 0)
                this.Close();

            //dgvLog.Columns["Timestamp"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            if (dgvLog.ColumnCount > 0)
            {
                dgvLog.Columns["Timestamp"].Width = TIMESTAMP_COLUMN_WIDTH;
            }
            else
            {
                this.Hide();
                this.Close();
                return;
            }

            AddHeaders(dgvLog);

            dgvLog.Dock = DockStyle.Fill;
            dgvLog.ColumnHeadersVisible = true;
            dgvLog.Columns["id"].Visible = false;
            dgvLog.Columns["logKey"].Visible = false;
            //  dgvLog.Columns["Log"].Visible = false;
            dgvLog.Columns["group9"].Visible = false;
            dgvLog.Columns["LogID"].Visible = false;
            dgvLog.Columns["prjKey"].Visible = false;
            dgvLog.Columns["Log Data"].Width = 620;
            dgvLog.RowsDefaultCellStyle.BackColor = Color.Honeydew;
            dgvLog.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvLog.AlternatingRowsDefaultCellStyle.Font = new Font(Font.FontFamily, 9);

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
            }
            //  dgvLog.ClearSelection();
        }

        private void writeGeneralInfo(string logID)
        {
            Project pr = new Project();

            // write generalInfo record with prname and logid

            DbCrud db = new DbCrud();
            DataTable dt = new DataTable();

            // Remove record if exists
            string sql = "SELECT COUNT(1) FROM generalInfo WHERE logID =" + logID;
            if (db.GetScalarIntFromDb(sql) > 0)
            {
                sql = "DELETE FROM generalInfo where logID ='" + logID + "'";
                db.crudToDb(sql);
            }

            // Remove oldest record if number of logs browsed >= 10

            sql = "SELECT COUNT(1) FROM generalInfo";
            if (db.GetScalarIntFromDb(sql) >= 3)
            {
                sql = "SELECT * FROM generalInfo limit 1";
                dt = db.GetTableFromDb(sql);

                sql = "DELETE FROM generalInfo where logID ='" + dt.Rows[0]["logID"].ToString() + "'";
                db.crudToDb(sql);
            }

            // Insert record with new id

            dt = pr.getALogByID(logID);
            string prjKey = dt.Rows[0]["prjKey"].ToString();
            string logName = pr.getLogName(logID);
            string projectName = pr.getProjectNameByProjectKey(prjKey);

            sql = $@"INSERT INTO generalInfo (logID, logName)
                        VALUES ( '{logID}','Project: {projectName} File: {logName.Substring(logName.LastIndexOf("\\")+1, logName.Length - (logName.LastIndexOf("\\")+1))}')";

            db.crudToDb(sql);

            this.Text = "LogView." + projectName + ": " +
                        logName.Substring(logName.LastIndexOf("\\") + 1, logName.Length - (logName.LastIndexOf("\\") + 1));

        }

        private System.Windows.Forms.ComboBox cmbColumHeader2 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbColumHeader4 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbColumHeader5 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbColumHeader6 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbColumHeader7 = new System.Windows.Forms.ComboBox();

        private void LogView_reLoad(object sender, EventArgs e)
        {
            reloadData();
        }

        private void reloadData()
        {
            dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);

            if (dgvLog.DataSource == null)
            {
                MessageBox.Show("unable to access database, please retry");
                return;
            }

            doDgvColumns();

            dgvLog.RowsDefaultCellStyle.BackColor = Color.Honeydew;
            dgvLog.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            //cmbColumHeader2.SelectedIndex = -1;
            //cmbColumHeader2.SelectedItem = null;
            //cmbColumHeader4.SelectedIndex = -1;
            //cmbColumHeader4.SelectedItem = null;
            //cmbColumHeader5.SelectedIndex = -1;
            //cmbColumHeader5.SelectedItem = null;
            //cmbColumHeader6.SelectedIndex = -1;
            //cmbColumHeader6.SelectedItem = null;
            //cmbColumHeader7.SelectedIndex = -1;
            //cmbColumHeader7.SelectedItem = null;

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
                dgvLog.AlternatingRowsDefaultCellStyle.Font = font;
            }
            dgvLog.Columns["Timestamp"].Width = TIMESTAMP_COLUMN_WIDTH;
            dgvLog.ClearSelection();
        }

        private void doDgvColumns()
        {
            dgvLog.Dock = DockStyle.Fill;
            dgvLog.ColumnHeadersVisible = true;
            dgvLog.Columns["id"].Visible = false;
            dgvLog.Columns["logKey"].Visible = false;
            dgvLog.Columns["group9"].Visible = false;
            dgvLog.Columns["LogID"].Visible = false;
            dgvLog.Columns["prjKey"].Visible = false;
            dgvLog.Columns["Log Data"].Width = 620;
            dgvLog.Columns["Timestamp"].Width = TIMESTAMP_COLUMN_WIDTH;

            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.SelectedItem = null;
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.SelectedItem = null;
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.SelectedItem = null;
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.SelectedItem = null;
            dgvLog.ClearSelection();
            this.dgvLog.Refresh();

        }

        private void AddHeaders(DataGridView dataGridView)
        {
            Point loc;
            string logID = dgvLog.Rows[0].Cells["LogID"].Value.ToString();

            // header group4

            cmbColumHeader2.SelectionChangeCommitted += delegate (object sender, EventArgs e)
            {
                cmbColumHeader2_SelectionChangeCommitted(sender, e, cmbColumHeader2, logID);
            };

            cmbColumHeader2.DataSource = App.Prj.getGroupOptions(logID, "group4");
            cmbColumHeader2.DisplayMember = "group4";
            cmbColumHeader2.DropDownStyle = ComboBoxStyle.DropDownList;
            loc = dgvLog.GetCellDisplayRectangle(2, -1, true).Location;
            cmbColumHeader2.Location = new Point(loc.X + dgvLog.Columns[2].Width, 1);
            cmbColumHeader2.Width = dgvLog.Columns[3].Width;
            dgvLog.Controls.Clear();
            dgvLog.Controls.Add(cmbColumHeader2);
            cmbColumHeader2.ResetText();
            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.Visible = true;

            // header group5

            cmbColumHeader4.SelectionChangeCommitted += delegate (object sender, EventArgs e)
            {
                cmbColumHeader4_SelectionChangeCommitted(sender, e, cmbColumHeader4, logID);
            };
            cmbColumHeader4.DataSource = App.Prj.getGroupOptions(logID, "group5");
            cmbColumHeader4.DisplayMember = "group5";
            cmbColumHeader4.DropDownStyle = ComboBoxStyle.DropDownList;
            loc = dgvLog.GetCellDisplayRectangle(3, -1, true).Location;
            cmbColumHeader4.Location = new Point(loc.X + dgvLog.Columns[3].Width, 1);
            cmbColumHeader4.Size = dgvLog.Columns[3].HeaderCell.Size;
            dgvLog.Controls.Add(cmbColumHeader4);
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.Visible = true;

            // header group6
            cmbColumHeader5.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumHeader5.Items.Add("NORMAL");
            cmbColumHeader5.Items.Add("RECV");
            cmbColumHeader5.Items.Add("SEND");

            cmbColumHeader5.SelectionChangeCommitted += delegate (object sender, EventArgs e)
            {
                cmbColumHeader5_SelectionChangeCommitted(sender, e, cmbColumHeader5, logID);
            };

            loc = dgvLog.GetCellDisplayRectangle(4, -1, true).Location;
            cmbColumHeader5.Location = new Point(loc.X + dgvLog.Columns[4].Width, 1);
            cmbColumHeader5.Size = dgvLog.Columns[4].HeaderCell.Size;
            dgvLog.Controls.Add(cmbColumHeader5);
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.Visible = true;

            // header group7 - Log

            cmbColumHeader7.SelectionChangeCommitted += delegate (object sender, EventArgs e)
            {
                cmbColumHeader7_SelectionChangeCommitted(sender, e, cmbColumHeader7, logID);
            };

            cmbColumHeader7.DataSource = App.Prj.getGroupOptions(logID, "group7");
            cmbColumHeader7.DisplayMember = "group7";
            cmbColumHeader7.DropDownStyle = ComboBoxStyle.DropDownList;
            loc = dgvLog.GetCellDisplayRectangle(5, -1, true).Location;
            cmbColumHeader7.Location = new Point(loc.X + dgvLog.Columns[5].Width, 1);
            cmbColumHeader7.Size = dgvLog.Columns[5].HeaderCell.Size;
            dgvLog.Controls.Add(cmbColumHeader7);
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.Visible = true;

            // header Log Data

            cmbColumHeader6.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumHeader6.Items.Add("ATM2HOST");
            cmbColumHeader6.Items.Add("HOST2ATM");
            cmbColumHeader6.Items.Add("Host Connected");
            cmbColumHeader6.Items.Add("Host Disconnected");
            cmbColumHeader6.Items.Add("CashDispenser");
            cmbColumHeader6.Items.Add("State Created");

            cmbColumHeader6.SelectionChangeCommitted += delegate (object sender, EventArgs e)
            {
                cmbColumHeader6_SelectionChangeCommitted(sender, e, cmbColumHeader6, logID);
            };

            // where to display the dropdown 
            // Location, width, height
            loc = dgvLog.GetCellDisplayRectangle(6, -1, true).Location;
            cmbColumHeader6.Location = new Point(loc.X + dgvLog.Columns[6].Width, 1);
            cmbColumHeader6.Width = 620;
            cmbColumHeader6.Height = 21;
            dgvLog.Controls.Add(cmbColumHeader6);
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.Visible = true;
        }

        private void cmbColumHeader2_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            string sqlLike = "='" + c.Text + "'";
            this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group4", sqlLike);
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.SelectedItem = null;
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.SelectedItem = null;
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.SelectedItem = null;
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.SelectedItem = null;
            dgvLog.ClearSelection();
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader4_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            string sqlLike = "='" + c.Text + "'";
            this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group5", sqlLike);
            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.SelectedItem = null;
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.SelectedItem = null;
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.SelectedItem = null;
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.SelectedItem = null;
            dgvLog.ClearSelection();
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader5_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            string sqlLike = " LIKE '%[[]" + c.Text + "%'";
            this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group6", sqlLike);

            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.SelectedItem = null;
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.SelectedItem = null;
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.SelectedItem = null;
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.SelectedItem = null;
            dgvLog.ClearSelection();
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader7_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            string sqlLike = "='" + c.Text + "'";
            this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group7", sqlLike);
            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.SelectedItem = null;
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.SelectedItem = null;
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.SelectedItem = null;
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.SelectedItem = null;
            dgvLog.ClearSelection();
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader6_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {

            //string sqlLike = " LIKE '%" + c.Text + "%'";
            //this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group8", sqlLike);

            ComboBox cb = sender as ComboBox;
            string sqlLike;

            if (cb.SelectedIndex < 6)
            {
                sqlLike = " LIKE '%" + c.Text + "%'";
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group8", sqlLike);
            }
            else
            {
                // work in rogress - future feature
                // doesn't work.
                sqlLike = " LIKE '%" + c.Text.Substring(0, 8) + "%'";
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria2(logID, "group8", sqlLike);
            }

            doDgvColumns();
        }


        /// <summary>
        /// To pass data to the LogData we use a delegate
        /// </summary>
        /// <param name="dgvr"></param>
        public delegate void passLogData(DataGridViewRow dgvr);

        public passLogData setData;

        public LogData frmLogData;

        private void dgvLog_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Clicks == 2)
            {

                if (frmLogData == null || frmLogData.IsDisposed)
                {
                    frmLogData = new LogData();
                    setData = new passLogData(frmLogData.setData);
                    frmLogData.getPrevRow = new ReceiveLogData(MovePrevRowOnDGV);
                    frmLogData.getNextRow = new ReceiveLogData(MoveNextRowOnDGV);
                }

                setData(dgvLog.Rows[e.RowIndex]);

                frmLogData.BringToFront();
                frmLogData.Show();
            }

            if (e.Button == MouseButtons.Right)
            {
                dgvLog.ContextMenuStrip = this.contextMenuStrip1;
            }
        }

        private DataGridViewRow MoveNextRowOnDGV()
        {
            DataGridViewRow dgvr = null;
            int nrow = dgvLog.SelectedCells[0].RowIndex;
            if (dgvLog.RowCount > nrow - 1 && nrow > 0)
            {
                int col = dgvLog.CurrentCell.ColumnIndex;
                dgvLog.CurrentCell = dgvLog[col, nrow - 1];
                dgvr = dgvLog.CurrentCell.OwningRow;
            }
            return dgvr;
        }

        private DataGridViewRow MovePrevRowOnDGV()
        {
            DataGridViewRow dgvr = null;
            if (dgvLog.SelectedCells.Count > 0)
            {
                int nrow = dgvLog.SelectedCells[0].RowIndex;
                if (dgvLog.RowCount > nrow + 1)
                {
                    int col = dgvLog.CurrentCell.ColumnIndex;
                    dgvLog.CurrentCell = dgvLog[col, nrow + 1];
                    dgvr = dgvLog.CurrentCell.OwningRow;
                }
            }
            return dgvr;
        }

        private void searchTwoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reloadData();
        }

        private void showInContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logInContext();
        }

        private void logInContext()
        {
            int rowIndex = dgvLog.SelectedCells[0].RowIndex;
            string key = dgvLog.Rows[rowIndex].Cells["logkey"].Value.ToString();

            dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);
            // search the key in the datagridview
            foreach (DataGridViewRow row in dgvLog.Rows)
            {
                Console.WriteLine(row.Cells[1].Value.ToString());

                if (row.Cells[1].Value.ToString().Contains(key))
                {
                    dgvLog.FirstDisplayedScrollingRowIndex = row.Index;
                    dgvLog.Rows[row.Index].Selected = true;
                }
            }
        }

        private void logInContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logInContext();
        }

        private void dataWrappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataWrapping(e);
        }

        private void dataWrappingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataWrapping(e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataCopy();
        }

        private void dataWrapping(EventArgs e)
        {
            if (dataWrappingToolStripMenuItem1.Text == "Data Wrapping")
            {
                dgvLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                //dgvLog.RowTemplate.Height = 28;
                //dgvLog.RowTemplate.MinimumHeight = 3;
                dgvLog.Columns["Log Data"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataWrappingToolStripMenuItem.Text = "Data Unwrapping";
                dataWrappingToolStripMenuItem1.Text = "Data Unwrapping";
                dgvLog.Refresh();
            }
            else
            {
                dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvLog.Columns["Log Data"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dataWrappingToolStripMenuItem.Text = "Data Wrapping";
                dataWrappingToolStripMenuItem1.Text = "Data Wrapping";
                dgvLog.Refresh();
            }

        }

        private void dataCopy()
        {
            Clipboard.SetDataObject(dgvLog.GetClipboardContent());

        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataCopy();
        }

        private void serchOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            advancedFilter();
        }

        private void advancedFilter()
        {

            if (Application.OpenForms["AdvancedFilterw"] != null &&
                Application.OpenForms["AdvancedFilterw"].Owner == this)
            {
                Form advancedFilter = Application.OpenForms["AdvancedFilterw"];
                advancedFilter.BringToFront();
                advancedFilter.Show();
            }
            else
            {
                AdvancedFilterw advancedFilter = new AdvancedFilterw();
                advancedFilter.PassDataGridView += new ToPassDataGridView(SendDataGridView);
                advancedFilter.Owner = this;
                advancedFilter.BringToFront();
                advancedFilter.Show(); // Dialog();
            }

        }

        private DataGridView SendDataGridView()
        {
            return dgvLog;
        }

        private void btExport_MouseClick(object sender, MouseEventArgs e)
        {
            ExportDataFromSQLServer();

        }



        protected void ExportDataFromSQLServer()
        {
            object dt1 = dgvLog.DataSource;

            DataTable dt2 = dt1 as DataTable;

            var excelApplication = new Excel.Application();

            var excelWorkBook = excelApplication.Application.Workbooks.Add(Type.Missing);

            DataColumnCollection dataColumnCollection = dt2.Columns;

            // including the header = +1 

            if (dt2.Rows.Count > 1000)
            {
                string message = "This query will take long time\nDo you want to continue?";
                string title = "Long Query Warning";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result = MessageBox.Show(message, title, buttons, icon);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }


            for (int i = 1; i <= dt2.Rows.Count - 2; i++)
            //                  for (int i = 1; i <= 200; i++)
            {
                for (int j = 3; j <= dt2.Columns.Count; j++)
                {
                    if (j > 10) continue;
                    if (i == 1)
                        excelApplication.Cells[i, j - 2] = dataColumnCollection[j - 1].ToString();
                    else
                    {
                        string columnData = dt2.Rows[i - 2][j - 1].ToString();

                        excelApplication.Cells[i, j - 2] = "'" + columnData;

                    }
                }
            }

            try
            {

                excelApplication.ActiveWorkbook.SaveCopyAs(@"C:\temp\test.xlsx");

                excelApplication.ActiveWorkbook.Saved = true;
                excelApplication.ActiveWorkbook.Close();

                // Close the Excel Application
                //excelApplication.Quit();

                excelWorkBook = excelApplication.Workbooks.Open(@"C:\temp\test.xlsx", 0, false, 5, "", "", true,
                    Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                excelApplication.Visible = true;

                //Release or clear the COM object
                releaseObject(excelWorkBook);
                releaseObject(excelApplication);
            }
            catch (COMException ec)
            {
                //todo: add exception handling fro file i/o
                MessageBox.Show("File in use by other application", ec.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Application Error: {0}", e.ToString());
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {

        }

        private void cbQueryName_Click(object sender, EventArgs e)
        {
            SQLSearchCondition sQLSearchCondition = new SQLSearchCondition();
            DataTable dt = sQLSearchCondition.getAllQueries("");

            // is there any saved queries?
            if (dt.Rows.Count > -1)
            {
                // fill the combo box with the names
                cbQueryName.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    cbQueryName.Items.Add(row["name"]);
                }
            }

        }

        private void cbQueryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            scSqlLikeAndRegExp sql = new scSqlLikeAndRegExp();

            sql = searchConditionBuilt(cbQueryName.Text);

            if (sql.SqlLike != "")
            {
                if (sql.RegExpStr != "")
                {
                    dgvLog.DataSource = App.Prj.getALogByIDWithRegExp(ProjectData.logID, sql.SqlLike, sql.RegExpStr);

                }
                else
                {
                    dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(ProjectData.logID, "", sql.SqlLike);
                }
                dgvLog.ClearSelection();
                dgvLog.Refresh();
            }


            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(cbQueryName, cbQueryName.Text);
            ToolTip1.AutoPopDelay = 5000;
            ToolTip1.InitialDelay = 1000;
            ToolTip1.ReshowDelay = 500;
            ToolTip1.ShowAlways = true;
        }

        private scSqlLikeAndRegExp searchConditionBuilt(string queryName)
        {
            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();

            dt = ssc.getSearchCondition(queryName);

            scSqlLikeAndRegExp sql = new scSqlLikeAndRegExp();

            string temp = "";
            sql.RegExpStr = "";
            sql.SqlLike = "";

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < 6; i++)
                {

                    if (dt.Rows[i][2].ToString() != "" && dt.Rows[i][3].ToString() != "" &&
                        dt.Rows[i][4].ToString() != "" && dt.Rows[i][3].ToString() != "RegExp")
                    {
                        temp = dt.Rows[i][4].ToString();

                        if (dt.Rows[i][3].ToString() == "Like")
                        {
                            if (dt.Rows[i][4].ToString().StartsWith("["))
                                temp = dt.Rows[i][4].ToString().Substring(1, dt.Rows[i][4].ToString().Length - 1);
                            temp = "%" + temp + "%";
                        }
                        sql.SqlLike += " " + dt.Rows[i][2].ToString() + dt.Rows[i][3].ToString() +
                               " '" + temp + "' ";
                    }

                    if (i < 5 &&
                        dt.Rows[i][5].ToString() != "" &&
                        dt.Rows[i + 1][2].ToString() != "" && dt.Rows[i + 1][3].ToString() != "" && dt.Rows[i + 1][4].ToString() != "")
                    {
                        sql.SqlLike += dt.Rows[i][5].ToString();
                    }

                    if (dt.Rows[i][3].ToString() == "RegExp")
                    {
                        sql.RegExpStr = dt.Rows[i][4].ToString();
                    }
                }

            }

            return sql;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportDataFromSQLServer();
        }

        private void cbQueryName_MouseHover(object sender, EventArgs e)
        {

        }

        private void cbQueryName_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            //if (e. >= 0) { }
            foreach (object cbi in cb.Items)
            {
                //ComboBoxItem item = (ComboBoxItem)cbi;
                //item.ToolTip = "Hello!";
            }
            //System.Windows.Controls.ComboBoxItem cbi = cb.SelectionBoxItem as System.Windows.Controls.ComboBoxItem;
            // cbi.ToolTip = "jajajaja ";


        }

        // todo JMH Review tooltip functionality 

        private void cbQueryName_DrawItem(object sender, DrawItemEventArgs e)
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            if (e.Index >= 0) { }
            System.Windows.Controls.ComboBoxItem cbi = cb.Items[0] as System.Windows.Controls.ComboBoxItem;
            cbi.ToolTip = "jajajaja ";
        }

        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        static IntPtr primary = GetDC(IntPtr.Zero);

        static int DESKTOPVERTRES = 117;
        static int DESKTOPHORZRES = 118;

        static int actualPixelsX = GetDeviceCaps(primary, DESKTOPHORZRES);
        static int actualPixelsY = GetDeviceCaps(primary, DESKTOPVERTRES);

        static int rel = ReleaseDC(IntPtr.Zero, primary);

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoggerPrint lp1 = LoggerPrint.GetInstance();

            // housekeeping
            // lets create an empty graphic space
            Graphics g = CreateGraphics();

            g.PageUnit = GraphicsUnit.Pixel;
            g.Clear(Color.White);

            Screen screenFormIsOn = Screen.FromControl(this);

            Bitmap bmp = new Bitmap(actualPixelsX, actualPixelsY, g);
            bmp.SetResolution(300, 300);

            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(0, 0, 0, 0, bmp.Size);

            lp1.Bmp = bmp;
            PrintDocument pdPreview = new PrintDocument();

            pdPreview.DefaultPageSettings.Landscape = true;
            pdPreview.PrintPage += new PrintPageEventHandler(lp1.pdPreview_PrintPage);
            pdPreview.BeginPrint += new PrintEventHandler(lp1.pdPreview_BeginPrint);
            pdPreview.EndPrint += new PrintEventHandler(lp1.EnPrint);

            printPreviewDialog1.Document = pdPreview;
            printPreviewDialog1.BringToFront();
            printPreviewDialog1.ShowDialog();
        }

        private void tabDetail_Selected(object sender, TabControlEventArgs e)
        {
            label2.ForeColor = Color.Brown;
            rtbMahineAndSoftwareInfo.Text = "";
            foreach (DataRow row in App.Prj.getLogDetailByID(ProjectData.logID).Rows)
            {
                rtbMahineAndSoftwareInfo.Text += row["detailInfo"].ToString() + "\n";
            }

            rtbMahineAndSoftwareInfo.Font = new Font(FontFamily.GenericMonospace, 9);
            rtbMahineAndSoftwareInfo.SelectionFont = new Font(FontFamily.GenericSansSerif, 12);
            rtbMahineAndSoftwareInfo.SelectionColor = Color.Red;
            ///richTextBox1.AppendText(System.Environment.NewLine + "Testing Testing Testing");


        }

        private void printToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //bool OKToPrint = true;

            //if (dgvLog.Rows.Count > 1500)
            //    OKToPrint = false;
            /**
             * testing for singleton
             * **/

            LoggerPrint lp1 = LoggerPrint.GetInstance();

            try
            {
                PrintDocument pd = new PrintDocument();

                pd.PrintPage += new PrintPageEventHandler(lp1.PrintDocPage);
                pd.BeginPrint += new PrintEventHandler(lp1.BeginDocPrint);
                pd.QueryPageSettings += new QueryPageSettingsEventHandler(lp1.QueryPageSettings);
                pd.EndPrint += new PrintEventHandler(lp1.EnPrint);

                printDialog1.Document = pd;
                // Create a new instance of Margins with 1-inch margins.
                Margins margins = new Margins(50, 50, 50, 50);
                pd.DefaultPageSettings.Margins = margins;
                pd.DefaultPageSettings.Landscape = false;


                lp1.DocToPrint = pd_docToPrint();
                lp1.SelToPrint = pd_selToPrint();

                if (lp1.SelToPrint == "" && lp1.DocToPrint == "")
                {
                    MessageBox.Show("Selection will take a long time to process", "Warning", MessageBoxButtons.OK);
                    return;
                }
                if (lp1.SelToPrint != "")
                    pd.PrinterSettings.PrintRange = PrintRange.Selection;

                //if (printDialog1.ShowDialog() != DialogResult.OK)
                //    return;
                //pd.Print();

                printPreviewDialog1.Document = pd;
                printPreviewDialog1.BringToFront();
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string pd_docToPrint()
        {

            string docToPrint = "";
            if (tabDetail.SelectedTab.Text == "LogData")
            {
                if (dgvLog.Rows.Count > 1500)
                {
                    // return null;
                    return docToPrint;
                }
                foreach (DataGridViewRow dgvr in dgvLog.Rows)
                {
                    string tempStr = "";

                    foreach (DataGridViewCell cell in dgvr.Cells)
                    {
                        if (cell.ColumnIndex < 2 || cell.ColumnIndex > 9) continue;
                        tempStr += cell.Value.ToString().Trim() + " ";
                    }
                    docToPrint += tempStr + "\n";
                }
            }
            else
            {
                docToPrint = rtbMahineAndSoftwareInfo.Text;
            }
            return docToPrint;
        }

        private string pd_selToPrint()
        {
            string selToPrint = "";
            if (tabDetail.SelectedTab.Text == "LogData")
            {
                if (dgvLog.SelectedCells.Count > 0 &&
                    dgvLog.SelectedCells[0].ColumnIndex > 1)
                {
                    Clipboard.SetDataObject(dgvLog.GetClipboardContent());
                    selToPrint = Clipboard.GetText();
                }
            }
            else
            {
                if (rtbMahineAndSoftwareInfo.SelectedText != null)
                {
                    // Clipboard.SetDataObject(richTextBox1.Text);
                    Clipboard.SetText(rtbMahineAndSoftwareInfo.SelectedText);
                    selToPrint = Clipboard.GetText();
                }
            }
            return selToPrint;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
