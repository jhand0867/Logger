using System;
using System.Drawing;
using System.Windows.Forms;

namespace Logger
{
    public partial class LogView : Form
    {
        public LogView()
        {
            InitializeComponent();
        }

        private void LogView_Load(object sender, EventArgs e)
        {

            dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);
            dgvLog.Columns["Timestamp"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            AddHeaders(dgvLog);

            dgvLog.Dock = DockStyle.Fill;
            dgvLog.ColumnHeadersVisible = true;
            dgvLog.Columns["id"].Visible = false;
            dgvLog.Columns["logKey"].Visible = false;
            dgvLog.Columns["group7"].Visible = false;
            dgvLog.Columns["group9"].Visible = false;
            dgvLog.Columns["LogID"].Visible = false;
            dgvLog.Columns["prjKey"].Visible = false;
            dgvLog.Columns["Log Data"].Width = 660;
            //         dgvLog.RowsDefaultCellStyle.BackColor =  Color.LightGray; Color.LightSteelBlue;
            dgvLog.RowsDefaultCellStyle.BackColor = Color.Honeydew;
            dgvLog.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
            }
        }

        ComboBox cmbColumHeader2 = new ComboBox();


        private void LogView_reLoad(object sender, EventArgs e)
        {
            reloadData();
        }

        private void reloadData()
        {
            dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);

            dgvLog.Dock = DockStyle.Fill;
            dgvLog.ColumnHeadersVisible = true;
            dgvLog.Columns["id"].Visible = false;
            dgvLog.Columns["logKey"].Visible = false;
            dgvLog.Columns["group7"].Visible = false;
            dgvLog.Columns["group9"].Visible = false;
            dgvLog.Columns["LogID"].Visible = false;
            dgvLog.Columns["prjKey"].Visible = false;
            dgvLog.Columns["Log Data"].Width = 660;
            // dgvLog.RowsDefaultCellStyle.BackColor = Color.LightGray; Color.LightSteelBlue;
            dgvLog.RowsDefaultCellStyle.BackColor = Color.Honeydew;
            dgvLog.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            cmbColumHeader2.SelectedItem = null;

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
                dgvLog.Columns["Log Data"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }

        private void AddHeaders(DataGridView dataGridView)
        {
            Point loc;
            string logID = dgvLog.Rows[0].Cells["LogID"].Value.ToString();

            // header group4
            // ComboBox cmbColumHeader2 = new ComboBox();

            cmbColumHeader2.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                cmbColumHeader2_SelectedIndexChanged(sender, e, cmbColumHeader2, logID);
            };
            cmbColumHeader2.DataSource = App.Prj.getGroupOptions(logID, "group4");
            cmbColumHeader2.DisplayMember = "group4";
            cmbColumHeader2.DropDownStyle = ComboBoxStyle.DropDownList;
            loc = dgvLog.GetCellDisplayRectangle(2, -1, true).Location;
            cmbColumHeader2.Location = new Point(loc.X + dgvLog.Columns[2].Width, 1);
            //  cmbColumHeader2.Size = dgvLog.Columns[2].HeaderCell.Size;
            cmbColumHeader2.Width = dgvLog.Columns[3].Width;
            dgvLog.Controls.Clear();
            dgvLog.Controls.Add(cmbColumHeader2);
            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.Visible = true;

            // header group5
            ComboBox cmbColumHeader4 = new ComboBox();
            cmbColumHeader4.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                cmbColumHeader4_SelectedIndexChanged(sender, e, cmbColumHeader4, logID);
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
            ComboBox cmbColumHeader5 = new ComboBox();
            cmbColumHeader5.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumHeader5.Items.Add("NORMAL");
            cmbColumHeader5.Items.Add("RECV");
            cmbColumHeader5.Items.Add("SEND");
            cmbColumHeader5.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                cmbColumHeader5_SelectedIndexChanged(sender, e, cmbColumHeader5, logID);
            };

            loc = dgvLog.GetCellDisplayRectangle(4, -1, true).Location;
            cmbColumHeader5.Location = new Point(loc.X + dgvLog.Columns[4].Width, 1);
            cmbColumHeader5.Size = dgvLog.Columns[4].HeaderCell.Size;
            dgvLog.Controls.Add(cmbColumHeader5);
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.Visible = true;

            // header Log Data
            ComboBox cmbColumHeader6 = new ComboBox();
            cmbColumHeader6.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumHeader6.Items.Add("ATM2HOST");
            cmbColumHeader6.Items.Add("HOST2ATM");
            cmbColumHeader6.Items.Add("Host Connected");
            cmbColumHeader6.Items.Add("Host Disconnected");
            cmbColumHeader6.Items.Add("CashDispenser");
            cmbColumHeader6.Items.Add("State Created");

            cmbColumHeader6.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                cmbColumHeader6_SelectedIndexChanged(sender, e, cmbColumHeader6, logID);
            };

            loc = dgvLog.GetCellDisplayRectangle(5, -1, true).Location;
            cmbColumHeader6.Location = new Point(loc.X + dgvLog.Columns[5].Width, 1);
            cmbColumHeader6.Width = 660;
            cmbColumHeader6.Height = 21;
            dgvLog.Controls.Add(cmbColumHeader6);
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.Visible = true;
        }

        public delegate void passLogData(DataGridViewRow dgvr);
        public passLogData setData;



        private void cmbColumHeader2_SelectedIndexChanged(object sender, System.EventArgs e, ComboBox c, string logID)
        {
            if (c.Text != "")
            {
                string sqlLike = "='" + c.Text + "'";
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group4", sqlLike);
                this.dgvLog.Refresh();
            }
        }

        private void cmbColumHeader4_SelectedIndexChanged(object sender, System.EventArgs e, ComboBox c, string logID)
        {
            if (c.Text != "")
            {
                string sqlLike = "='" + c.Text + "'";
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group5", sqlLike);
                this.dgvLog.Refresh();
            }

        }

        private void cmbColumHeader5_SelectedIndexChanged(object sender, System.EventArgs e, ComboBox c, string logID)
        {
            if (c.Text != "")
            {
                string sqlLike = " LIKE '%[[]" + c.Text + "%'";
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group6", sqlLike);
                this.dgvLog.Refresh();
            }

        }

        private void cmbColumHeader6_SelectedIndexChanged(object sender, System.EventArgs e, ComboBox c, string logID)
        {
            if (c.Text != "")
            {
                string sqlLike = " LIKE '%" + c.Text + "%'";
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group8", sqlLike);
                this.dgvLog.Refresh();
            }

        }

        private void dgvLog_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                LogData frmLogData = (LogData)Application.OpenForms["LogData"];
                if (frmLogData == null)
                {
                    frmLogData = new LogData();
                }
                //setData = null;
                setData = new passLogData(frmLogData.setData);
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

        private void menuStrip1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dgvLog_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dgvLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void showInContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logInContext();
            
        }

        private void logInContext()
        {
            //string key = dgvLog.SelectedRows[0].Cells[1].Value.ToString();

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

                    // MessageBox.Show("Gotcha, it is " + row.Index);
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
          //  dgvLog.Columns["Timestamp"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            if (dataWrappingToolStripMenuItem1.Text == "Data Wrapping")
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
 //               contextMenuStrip1.Items[2].Text = "Data Unwrapping";
 //               ContextMenuStrip.Items[2].Text = "Data Unwrapping";
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
            //dgvLog.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            Clipboard.SetDataObject(dgvLog.GetClipboardContent());
            //dgvLog.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataCopy();
        }
    }
}
