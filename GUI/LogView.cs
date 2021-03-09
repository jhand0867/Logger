﻿using System;
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
            label2.Text = "Machine Information";
            richTextBox1.Font = new Font(FontFamily.GenericSansSerif, 10);
            richTextBox1.Text = "Testing Testing Testing";


        }
    }
}
