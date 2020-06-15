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
            dgvLog.Columns["group8"].Width = 660;
            dgvLog.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dgvLog.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

            using (Font font = new Font(
                dgvLog.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Regular))
            {
                dgvLog.Columns["group8"].DefaultCellStyle.Font = font;
            }            
        }

        private void AddHeaders(DataGridView dataGridView)
        {
            //dataGridView.AutoResizeColumns();

            Point loc;
            // header group4
            ComboBox cmbColumHeader2 = new ComboBox();
            cmbColumHeader2.DataSource = App.Prj.getGroup8Options("1");
            cmbColumHeader2.DisplayMember = "group4";
            cmbColumHeader2.DropDownStyle = ComboBoxStyle.DropDownList;
            
            loc = dgvLog.GetCellDisplayRectangle(2, -1, true).Location;
            cmbColumHeader2.Location = new Point(loc.X + dgvLog.Columns[2].Width, 1);
            cmbColumHeader2.Size = dgvLog.Columns[2].HeaderCell.Size;
            dgvLog.Controls.Add(cmbColumHeader2);
            cmbColumHeader2.SelectedIndex = -1;
            cmbColumHeader2.Visible = true;

            // header group5
            ComboBox cmbColumHeader4 = new ComboBox();
            cmbColumHeader4.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumHeader4.Items.Add("Option I");
            cmbColumHeader4.Items.Add("Option II");
            cmbColumHeader4.Items.Add("Option III");
            loc = dgvLog.GetCellDisplayRectangle(3, -1, true).Location;
            cmbColumHeader4.Location = new Point(loc.X + dgvLog.Columns[3].Width, 1);
            cmbColumHeader4.Size = dgvLog.Columns[3].HeaderCell.Size;
            dgvLog.Controls.Add(cmbColumHeader4);
            cmbColumHeader4.SelectedIndex = -1;
            cmbColumHeader4.Visible = true;
            
            // header group6
            ComboBox cmbColumHeader5 = new ComboBox();
            cmbColumHeader5.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumHeader5.Items.Add("Option a");
            cmbColumHeader5.Items.Add("Option b");
            cmbColumHeader5.Items.Add("Option c");
            loc = dgvLog.GetCellDisplayRectangle(4, -1, true).Location;
            cmbColumHeader5.Location = new Point(loc.X + dgvLog.Columns[4].Width, 1);
            cmbColumHeader5.Size = dgvLog.Columns[4].HeaderCell.Size;
            dgvLog.Controls.Add(cmbColumHeader5);
            cmbColumHeader5.SelectedIndex = -1;
            cmbColumHeader5.Visible = true;

            // header group8
            ComboBox cmbColumHeader6 = new ComboBox();
            cmbColumHeader6.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumHeader6.Items.Add("ATM2HOST");
            cmbColumHeader6.Items.Add("HOST2ATM");
            cmbColumHeader6.Items.Add("Host Connected");

            loc = dgvLog.GetCellDisplayRectangle(5, -1, true).Location;
            cmbColumHeader6.Location = new Point(loc.X + dgvLog.Columns[5].Width, 1);
            cmbColumHeader6.Width = 660;
            cmbColumHeader6.Height = 21;
            dgvLog.Controls.Add(cmbColumHeader6);
            cmbColumHeader6.SelectedIndex = -1;
            cmbColumHeader6.Visible = true;
        }

        private void dgvLog_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // MessageBox.Show(e.X.ToString(), e.Y.ToString()) ;

            //DataGridViewRow dgvr = dgvLog.SelectedRows[0];
            //string cellContent = dgvr.Cells["group8"].Value.ToString();

            //MessageBox.Show(cellContent);

        }
    }
}
