using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger
{
    public partial class LogData : Form
    {
        public LogData()
        {
            InitializeComponent();
        }

        private void LogData_Load(object sender, EventArgs e)
        {
            //txtGroup8.Text = "temp";
        }

        public void setData(DataGridViewRow dgvr)
        {
            txtTimestamp.Text = dgvr.Cells["Timestamp"].Value.ToString();
            txtLogLevel.Text = dgvr.Cells["Log Level"].Value.ToString();
            txtFileName.Text = dgvr.Cells["File Name"].Value.ToString();
            txtClass.Text = dgvr.Cells["Class"].Value.ToString();
            txtMethod.Text = dgvr.Cells["Method"].Value.ToString();
            txtType.Text = dgvr.Cells["Type"].Value.ToString();
            rtbRawData.Text = App.Prj.showBytes(Encoding.ASCII.GetBytes(dgvr.Cells["Log Data"].Value.ToString()));
            string logKey = dgvr.Cells["logKey"].Value.ToString();
            logKey = logKey.Substring(0, logKey.LastIndexOf("-"));
            string logID = dgvr.Cells["logID"].Value.ToString();

            string prjKey = dgvr.Cells["prjKey"].Value.ToString();


            DataTable dt = App.Prj.getRecord(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString());
            txtFieldData.Text = "";
            string stateType = "";
            int fieldNum = -1;

            if (dt.Rows.Count > 0)
            {
                for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                {
                    if (dt.Rows[rowNum][2].ToString() == "I")
                    {
                        txtFieldData.Text = dt.Columns[3].ColumnName + " = " + dt.Rows[0][3].ToString();
                    }

                    if (dt.Rows[rowNum][2].ToString() == "S")
                    {
                        for (fieldNum = 3; fieldNum < dt.Columns.Count - 5; fieldNum++)
                        {
                            if (fieldNum == 3)
                            {
                                txtFieldData.Text += dt.Columns[fieldNum].ColumnName + " = " + dt.Rows[rowNum][fieldNum].ToString() + " ";
                            }
                            else
                            {
                                txtFieldData.Text += dt.Rows[rowNum][fieldNum].ToString() + " ";
                            }
                            // what state type is it?
                            if (fieldNum == 4)
                            {
                                stateType = dt.Rows[rowNum][fieldNum].ToString();
                            }
                        }
                        stateRec stRec = new stateRec();
                        stRec.StateNumber = dt.Rows[rowNum][3].ToString();
                        stRec.StateType = dt.Rows[rowNum][4].ToString();
                        stRec.Val1 = dt.Rows[rowNum][5].ToString();
                        stRec.Val2 = dt.Rows[rowNum][6].ToString();
                        stRec.Val3 = dt.Rows[rowNum][7].ToString();
                        stRec.Val4 = dt.Rows[rowNum][8].ToString();
                        stRec.Val5 = dt.Rows[rowNum][9].ToString();
                        stRec.Val6 = dt.Rows[rowNum][10].ToString();
                        stRec.Val7 = dt.Rows[rowNum][11].ToString();
                        stRec.Val8 = dt.Rows[rowNum][12].ToString();
                        txtFieldData.Text += System.Environment.NewLine;
                        txtFieldData.Text += System.Environment.NewLine;
                        txtFieldData.Text += stRec.getInfo(stRec);
                    }
                }

            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            LogView frmLogView = (LogView)Application.OpenForms["LogView"];
            DataGridView dgv = (DataGridView)frmLogView.ActiveControl;
            int nrow = dgv.SelectedRows[0].Index;

            if (dgv.RowCount > nrow + 1)
            {
                dgv.Rows[nrow].Selected = false;
                dgv.Rows[nrow + 1].Selected = true;
                nrow = dgv.SelectedRows[0].Index;
                DataGridViewRow dgvr = dgv.SelectedRows[0];
                setData(dgvr);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            LogView frmLogView = (LogView)Application.OpenForms["LogView"];
            DataGridView dgv = (DataGridView)frmLogView.ActiveControl;
            int nrow = dgv.SelectedRows[0].Index;

            if (nrow != 0)
            {
                dgv.Rows[nrow].Selected = false;
                dgv.Rows[nrow - 1].Selected = true;
                nrow = dgv.SelectedRows[0].Index;
                DataGridViewRow dgvr = dgv.SelectedRows[0];
                setData(dgvr);
            }


            // dgvr.Selected = false;

            //System.Windows.Controls.Control control = frmLogView.Controls["dgvLog"].;

        }
    }
}
