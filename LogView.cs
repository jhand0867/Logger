using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Logger
{
    public partial class LogView : Form
    {
        private IEnumerable<KeyValuePair<string, string>> _labels;

        public LogView()
        {
            InitializeComponent();
        }

        private void LogView_Load(object sender, EventArgs e)
        {

            dgvLog.DataSource = App.Prj.getALogByID(ProjectData.logID);
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
            dgvLog.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dgvLog.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;
            

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
            }            
        }

        ComboBox cmbColumHeader2 = new ComboBox();
       

        private void LogView_reLoad(object sender, EventArgs e)
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
            dgvLog.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dgvLog.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

            cmbColumHeader2.SelectedItem = null ;
                     
            

            

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Regular))
            {
                dgvLog.Columns["Log Data"].DefaultCellStyle.Font = font;
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
            cmbColumHeader2.Size = dgvLog.Columns[2].HeaderCell.Size;
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
            cmbColumHeader6.Items.Add("Created state");

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
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group4", c.Text);
                this.dgvLog.Refresh();
            }
        }

        private void cmbColumHeader4_SelectedIndexChanged(object sender, System.EventArgs e, ComboBox c, string logID)
        {
            if (c.Text != "")
            {
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group5", c.Text);
                this.dgvLog.Refresh();
            }

        }

        private void cmbColumHeader5_SelectedIndexChanged(object sender, System.EventArgs e, ComboBox c, string logID)
        {
            if (c.Text != "")
            {
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group6", c.Text);
                this.dgvLog.Refresh();
            }

        }

        private void cmbColumHeader6_SelectedIndexChanged(object sender, System.EventArgs e, ComboBox c, string logID)
        {
            if (c.Text != "")
            {
                this.dgvLog.DataSource = App.Prj.getALogByIDWithCriteria(logID, "group8", c.Text);
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
                this.setData += new passLogData(frmLogData.setData);

                setData(dgvLog.Rows[e.RowIndex]);
                frmLogData.TopMost = true;
                frmLogData.Show();
            }
        }

        private void searchTwoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogView_reLoad(sender, e);
        }

        private void menuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dgvLog_MouseDown(object sender, MouseEventArgs e)
        {
/*           if( e.Button == System.Windows.Forms.MouseButtons.Left)
           {
                foreach (DataGridViewRow item in dgvLog.Rows)
                {
                    if (item.

                }
           }*/

        }
    }
}
