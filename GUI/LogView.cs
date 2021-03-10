using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
using System.Runtime.InteropServices;
//using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Logger
{
    public partial class LogView : Form
    {
        public LogView()
        {
            InitializeComponent();
            this.FormClosing += LogView_FormClosing;
            dgvLog.DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 9);
        }

        private void LogView_FormClosing(object sender, FormClosingEventArgs e)
        {
            dgvLog.DataSource = null;
            dgvLog.Rows.Clear();
            GC.Collect();
        }


        private void LogView_Load(object sender, EventArgs e)
        {
            dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);

            if (dgvLog.DataSource == null)
            {
                MessageBox.Show("unable to access database, please retry");
                return;
            }


            //dgvLog.Columns["Timestamp"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvLog.Columns["Timestamp"].Width = 134;
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
            dgvLog.AlternatingRowsDefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 9);

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
            }
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
            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.SelectedItem = null;
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.SelectedItem = null;
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.SelectedItem = null;
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.SelectedItem = null;
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.SelectedItem = null;

            dgvLog.AlternatingRowsDefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 9);
            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
                dgvLog.Columns["Log Data"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }

            dgvLog.Columns["Timestamp"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void doDgvColumns()
        {
            dgvLog.Dock = DockStyle.Fill;
            dgvLog.ColumnHeadersVisible = true;
            dgvLog.Columns["id"].Visible = false;
            dgvLog.Columns["logKey"].Visible = false;
            //  dgvLog.Columns["Log"].Visible = false;
            dgvLog.Columns["group9"].Visible = false;
            dgvLog.Columns["LogID"].Visible = false;
            dgvLog.Columns["prjKey"].Visible = false;
            dgvLog.Columns["Log Data"].Width = 620;
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
            //this.dgvLog.DataSource = null;
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
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader4_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            //this.dgvLog.DataSource = null;
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
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader5_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            //this.dgvLog.DataSource = null;

            string sqlLike = " LIKE '%[[]" + c.Text + "%'";
            this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group6", sqlLike);
            
            //doDgvColumns();

            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.SelectedItem = null;
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.SelectedItem = null;
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.SelectedItem = null;
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.SelectedItem = null;
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader7_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            //this.dgvLog.DataSource = null;
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
            this.dgvLog.Refresh();
        }

        private void cmbColumHeader6_SelectionChangeCommitted(object sender, System.EventArgs e, System.Windows.Forms.ComboBox c, string logID)
        {
            //this.dgvLog.DataSource = null;
            
            string sqlLike = " LIKE '%" + c.Text + "%'";
            this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group8", sqlLike);

            //doDgvColumns();

            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.SelectedItem = null;
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.SelectedItem = null;
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.SelectedItem = null;
            cmbColumHeader7.SelectedIndex = -1;
            cmbColumHeader7.SelectedItem = null;
            this.dgvLog.Refresh();
        }

        public delegate void passLogData(DataGridViewRow dgvr);
        public passLogData setData;

        private void dgvLog_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                LogData frmLogData = (LogData)Application.OpenForms["LogData"];
                if (frmLogData == null)
                {
                    frmLogData = new LogData();
                }

                setData = new passLogData(frmLogData.setData);
                
                //mlh  -> e.RowIndex could be -1 and thrown an exception

                setData(dgvLog.Rows[e.RowIndex]);
                frmLogData.TopMost = true;
                frmLogData.Show();
            }

            if (e.Button == MouseButtons.Right)
            {
                dgvLog.ContextMenuStrip = this.contextMenuStrip1;
            }
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
            dgvLog.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            int rowIndex = dgvLog.SelectedCells[0].RowIndex;

            if (dataWrappingToolStripMenuItem1.Text == "Data Wrapping")
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataWrappingToolStripMenuItem.Text = "Data Unwrapping";
                dataWrappingToolStripMenuItem1.Text = "Data Unwrapping";
                dgvLog.Refresh();
            }
            else
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            AdvancedFilterw advancedFilter = new AdvancedFilterw();
            advancedFilter.ShowDialog();
        }

        private void btExport_MouseClick(object sender, MouseEventArgs e)
        {
            ExportDataFromSQLServer();

        }



        protected void ExportDataFromSQLServer()
        {
            object dt1 = dgvLog.DataSource;

            DataTable dt2 = dt1 as DataTable;

            var excelApplication = new Microsoft.Office.Interop.Excel.Application();

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
            DataTable dt = sQLSearchCondition.getAllQueries();

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
            DataTable dt = new DataTable();
            SQLSearchCondition ssc = new SQLSearchCondition();
           

            dt = ssc.getSearchCondition(cbQueryName.Text);

            string sqlLike = "";
            string temp = "";

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < 6; i++)
                {

                    if (dt.Rows[i][2].ToString() != "" && dt.Rows[i][3].ToString() != "" && dt.Rows[i][4].ToString() != "")
                    {
                        temp = dt.Rows[i][4].ToString();

                        if (dt.Rows[i][3].ToString() == "Like")
                        {
                            if (dt.Rows[i][4].ToString().StartsWith("["))
                                temp = dt.Rows[i][4].ToString().Substring(1, dt.Rows[i][4].ToString().Length - 1);
                            temp = "%" + temp + "%";
                        }
                        sqlLike += " " + dt.Rows[i][2].ToString() + dt.Rows[i][3].ToString() +
                               " '" + temp + "' ";
                    }

                    if (i < 5 &&
                        dt.Rows[i][5].ToString() != "" &&
                        dt.Rows[i + 1][2].ToString() != "" && dt.Rows[i + 1][3].ToString() != "" && dt.Rows[i + 1][4].ToString() != "")
                    {
                        sqlLike += dt.Rows[i][5].ToString();
                    }

                }
            }
            if (sqlLike != "")
            {
                dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(ProjectData.logID, "", sqlLike);
                dgvLog.Refresh();
            }

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(cbQueryName, cbQueryName.Items[cbQueryName.SelectedIndex].ToString());
            ToolTip1.AutoPopDelay = 5000;
            ToolTip1.InitialDelay = 1000;
            ToolTip1.ReshowDelay = 500;
            ToolTip1.ShowAlways = true;
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


        Bitmap bmp;

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // lets create an empty graphic space
            Graphics g = CreateGraphics();

            g.PageUnit = GraphicsUnit.Pixel;
            g.Clear(Color.White);
            
            Screen screenFormIsOn = Screen.FromControl(this);

            bmp = new Bitmap(actualPixelsX, actualPixelsY, g);          
            bmp.SetResolution(300,300);
                  
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.ShowDialog();
        }
        PrinterResolution pkResolution;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            pkResolution = printDocument1.PrinterSettings.PrinterResolutions[0];
            e.PageSettings.PrinterResolution = pkResolution;
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void tabDetail_Selected(object sender, TabControlEventArgs e)
        {
            
            label2.ForeColor = Color.Brown;
            label2.Text = @"==========================================================================================================
 - Machine Number      = I57005
 - Application Version = 02.03.40.00
 - SDK Version         = 03.00.01.36
 - Model               = 5700
 - IP Address          = 15.180.1.5
 - Port Number         = 9999
 - Time Zone           = Central Standard Time (Mexico)
 - Nextware Version    = 029-57
 - EMV Kernel 6 Version  = 6.0.0.6 86016 bytes MWIOCX = 2, 1, 0, 0 
 -         PIN Version = SP[  V 04.21.75], EP[V 10.00.07]
 -         CDM Version = SP[  V 04.21.09], EP[V 10.00.25 ]
 -         SIU Version = SP[  V 04.20.62], EP[   V020031]
 -         IDC Version = SP[  V 04.30.35], EP[  4017-01J]
 -         SPR Version = SP[  V 04.30.76], EP[V 30.00.25]
 -         BCR Version = SP[  V 04.00.31], EP[V2.1.23H_Release/S2.0.8/A2.2.6]
==========================================================================================================
Installed Programs:
 - A2iA CheckReader V4.4 R2, Installed: 07/18/2017, Version: 
 - A2iA CheckReader V9.0, Installed: 09/14/2017, Version: 9.0
 - Altiris Application Metering Agent, Installed: 07/25/2018, Version: 8.1.4515.0
 - Altiris Inventory Agent, Installed: 07/25/2018, Version: 8.1.4515.0
 - ATMOSPHERE, Installed: 07/17/2018, Version: 7.0.2
 - BCR20_SP_V040031_R1, Installed: 05/16/2018, Version: 04.00.31
 - BCR20_SP_V040031_US_REG_R1, Installed: 05/16/2018, Version: 04.00.31
 - CDU10_EP_V010165, Installed: 05/16/2018, Version: 01.01.65
 - CDU10_EP_V020005_R2, Installed: 05/16/2018, Version: 02.00.05
 - CDU10_SP_V042109_R1, Installed: 05/16/2018, Version: 04.21.09
 - CDU10_SP_V042109_US_REG_R1, Installed: 05/16/2018, Version: 04.21.09
 - Common_SP_V043094_R1, Installed: 05/16/2018, Version: 04.30.94
 - EMET 5.5, Installed: 09/24/2018, Version: 5.5
 - EPP60(USB)_SP_V042173_R1, Installed: 05/16/2018, Version: 04.21.73
 - EPP60(USB)_SP_V042173_US_REG_R1, Installed: 05/16/2018, Version: 04.21.73
 - EPP60_EP_V072105_11_R3, Installed: 05/16/2018, Version: 07.21.05
 - EPP80(USB)_EP_V10000500_R2, Installed: 05/16/2018, Version: 10.00.05.00
 - FGRNG_SP_V042013_US_REG, Installed: 07/18/2017, Version: 04.20.13
 - FGRNG_SP_V042016_R1, Installed: 05/16/2018, Version: 04.20.16
 - Intel(R) Management Engine Components, Installed: , Version: 10.0.38.1036
 - Intel(R) ME UninstallLegacy, Installed: 07/18/2017, Version: 1.0.1.0
 - Intel(R) Network Connections 18.2.63.0, Installed: 07/18/2017, Version: 18.2.63.0
 - Intel(R) Processor Graphics, Installed: , Version: 9.18.10.3204
 - Intel(R) Rapid Storage Technology, Installed: , Version: 12.8.0.1016
 - Intel(R) SDK for OpenCL - CPU Only Runtime Package, Installed: , Version: 3.0.0.66956
 - Intel(R) USB 3.0 eXtensible Host Controller Driver, Installed: , Version: 4.0.4.51
 - Intel® Trusted Connect Service Client, Installed: 07/18/2017, Version: 1.35.133.1
 - Java Auto Updater, Installed: 07/17/2018, Version: 2.0.2.4
 - Java Media Framework 2.1.1e, Installed: , Version: 
 - Java(TM) 6 Update 21, Installed: 07/17/2018, Version: 6.0.210
 - MCU12(USB)_EP_V401701I_R1, Installed: 05/16/2018, Version: 40.17.01
 - MCU12_SP_V043035_R1, Installed: 05/16/2018, Version: 04.30.35
 - MCU12_SP_V043035_US_REG_R1, Installed: 05/16/2018, Version: 04.30.35
 - Microsoft .NET Framework 4.5.2, Installed: 08/06/2019, Version: 4.5.51209
 - Microsoft Visual C++ 2010  x86 Redistributable - 10.0.30319, Installed: 01/23/2014, Version: 10.0.30319
 - Microsoft Visual C++ 2013 Redistributable (x86) - 12.0.30501, Installed: , Version: 12.0.30501.0
 - Microsoft Visual C++ 2013 x86 Additional Runtime - 12.0.21005, Installed: 07/18/2017, Version: 12.0.21005
 - Microsoft Visual C++ 2013 x86 Minimum Runtime - 12.0.21005, Installed: 07/18/2017, Version: 12.0.21005
 - MP2s Banorte Global, Installed: 08/06/2019, Version: 01.00.02.00
 - MP2s Global, Installed: 08/06/2019, Version: 02.03.40.00
 - Nextware_V032957, Installed: 08/06/2019, Version: 03.29.57
 - Nuvoton Communcations Port 32-bits Driver, Installed: 07/18/2017, Version: 1.0.2011.1109
 - Patch Management Agent, Installed: 07/25/2018, Version: 8.1.4538.0
 - PNCU0_EP_V020031_R2, Installed: 05/16/2018, Version: 02.00.31
 - PNCU0_SP_V042062_R1, Installed: 05/16/2018, Version: 04.20.62
 - PNCU0_SP_V042062_US_REG_R1, Installed: 05/16/2018, Version: 04.20.62
 - Power Scheme Plug-in Setup, Installed: 07/25/2018, Version: 8.1.4504.0
 - PulseIR, Installed: , Version: 
 - Realtek High Definition Audio Driver, Installed: 07/18/2017, Version: 6.0.1.6865
 - Realtek PC Camera, Installed: 01/23/2014, Version: 6.2.9200.10233
 - RFM00_SP_V042030_R1, Installed: 05/16/2018, Version: 04.20.30
 - SCN20_SP_V041013_R2, Installed: 05/16/2018, Version: 04.10.13
 - SNTouch Driver For USB & Serial 5.2A, Installed: 05/24/2016, Version: 5.1.0.0
 - Software Management Solution Plugin, Installed: 07/25/2018, Version: 8.1.4504.0
 - SPRK(USB)_SP_V043076_R1, Installed: 05/16/2018, Version: 04.30.76
 - SPRK(USB)_SP_V043076_US_REG_R1, Installed: 05/16/2018, Version: 04.30.76
 - SPRK5_EP_V020035_R2, Installed: 05/16/2018, Version: 02.00.35
 - Touch Service Installation v1.48, Installed: 08/07/2016, Version: 1.48
 - tpdrv, Installed: , Version: 
 - Trend Micro Deep Security Agent, Installed: 07/29/2019, Version: 11.0.760
 - TTU00_SP_V040333_R1, Installed: 05/16/2018, Version: 04.03.33
 - TTU00_SP_V040333_US_REG_R1, Installed: 05/16/2018, Version: 04.03.33
 - VC_CRT_x86, Installed: 07/18/2017, Version: 1.02.0000
 - VDM_Core_V01.00.60, Installed: 07/18/2017, Version: 01.00.60
 - VDM_Core_V010353_R1, Installed: 05/16/2018, Version: 01.03.53
 - VDM_Data_V010353_R1, Installed: 05/16/2018, Version: 01.03.53
 - Windows Driver Package - Nuvoton Technology Corporation (Serial) Ports  (11/09/2011 1.0.2011.1109), Installed: , Version: 11/09/2011 1.0.2011.1109
==========================================================================================================
Installed Packages:
 - Basic Media-V01.03.03.00, Installed: Sat 04/12/2014
==========================================================================================================";
            richTextBox1.Font = new Font(FontFamily.GenericSansSerif, 10);
            richTextBox1.Text = "Testing Testing Testing";


        }
    }
}
