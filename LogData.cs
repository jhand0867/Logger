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

            
            List<DataTable> dts = App.Prj.getRecord(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString());
            txtFieldData.Text = "";
            string stateType = "";
            int fieldNum = -1;
            int timerNum = 0;

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    {
                        if (dt.Rows[rowNum][2].ToString() == "I")
                        {
                            txtFieldData.Text = dt.Columns[3].ColumnName + " = " + dt.Rows[0][3].ToString();
                        }
                        // if the row data is for a State
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
                        // if the row data is for a FIT
                        if (dt.Rows[rowNum][2].ToString() == "F")
                        {
                            FitRec fitRec = new FitRec();
                            DataTable fitdt = fitRec.getDescription();

                            for (fieldNum = 3; fieldNum < dt.Columns.Count - 5; fieldNum++)
                            {
                                if (fieldNum == 3)
                                {
                                    txtFieldData.Text += @"==================================================" + System.Environment.NewLine;
                                    txtFieldData.Text += dt.Columns[fieldNum].ColumnName.ToUpper() + " = " + dt.Rows[rowNum][fieldNum].ToString() + System.Environment.NewLine;
                                    txtFieldData.Text += @"==================================================" + System.Environment.NewLine;
                                }
                                else
                                {
                                    txtFieldData.Text += dt.Columns[fieldNum].ColumnName.ToUpper() + " = " + dt.Rows[rowNum][fieldNum].ToString() + "\t\t" + fitdt.Rows[fieldNum - 3][3].ToString().Trim() + System.Environment.NewLine;
                                }

                            }
                        }
                        if (dt.Rows[rowNum][2].ToString() == "C")
                        {
                            EnhancedParamsRec paramRec = new EnhancedParamsRec();
                            DataTable paramRecDt = paramRec.getDescription();
                            string optionDesc = "";

                            if (dt.Rows[rowNum][5].ToString() == "1")
                            {
                                if (rowNum == 0)
                                {
                                    txtFieldData.Text += @"==================================================" + System.Environment.NewLine;
                                    txtFieldData.Text += @"OPTIONS" + System.Environment.NewLine;
                                    txtFieldData.Text += @"==================================================" + System.Environment.NewLine;

                                }
                                txtFieldData.Text += dt.Columns[3].ColumnName.ToUpper() + " " + dt.Rows[rowNum][3].ToString() + " = " + dt.Rows[rowNum][4].ToString();

                                // what's the description of the field
                                foreach (DataRow item in paramRecDt.Rows)
                                {
                                    if (item[2].ToString().Substring(0, 1) == "O" && item[2].ToString().Substring(1, 2) == dt.Rows[rowNum][3].ToString())
                                    {
                                        optionDesc = item[3].ToString().Trim();
                                        break;
                                    }

                                }


                                txtFieldData.Text += "\t" + optionDesc + System.Environment.NewLine;
                            }

                            if (dt.Rows[rowNum][5].ToString() == "2")
                            {
                                if (timerNum == 0)
                                {
                                    txtFieldData.Text += @"==================================================" + System.Environment.NewLine;
                                    txtFieldData.Text += @"TIMERS" + System.Environment.NewLine;
                                    txtFieldData.Text += @"==================================================" + System.Environment.NewLine;
                                    timerNum = 1;
                                }
                                txtFieldData.Text += dt.Columns[3].ColumnName.ToUpper() + " " + dt.Rows[rowNum][3].ToString() + " = " + dt.Rows[rowNum][4].ToString();

                                // what's the description of the field
                                foreach (DataRow item in paramRecDt.Rows)
                                {
                                    if (item[2].ToString().Substring(0, 1) == "T" && item[2].ToString().Substring(1, 2) == dt.Rows[rowNum][3].ToString())
                                    {
                                        optionDesc = item[3].ToString().Trim();
                                        break;
                                    }
                                }
                                txtFieldData.Text += "\t" + optionDesc + System.Environment.NewLine;
                            }
                        }
                        if (dt.Rows[rowNum][2].ToString() == "R")
                        {
                            DataTable tReplyDt = new TReply().getDescription();
                            int x = 0;
                            for (int field =3; field <= dt.Rows[rowNum].ItemArray.Length-5; field++)
                            {
                                if (field == 24)
                                {
                                    string printerData = getPrinterData(dts[1]);
                                }

                                if (field == 62)
                                {
                                    string checkProcessingData = getCheckProccessing(dts[2]);
                                }
                                string fieldContent = dt.Rows[rowNum].ItemArray[field].ToString().Trim();
                                if (fieldContent == "")
                                    continue;
                                else
                                {
                                    txtFieldData.Text += dt.Columns[field].ColumnName.ToUpper() + " = ";
                                    txtFieldData.Text += fieldContent;
                                    txtFieldData.Text += System.Environment.NewLine;
                                }

                            }

                        }

                    }

                }

            }
        }

        private string getCheckProccessing(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        private string getPrinterData(DataTable dataTable)
        {
            throw new NotImplementedException();
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
