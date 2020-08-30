using System;
using System.Text;
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

            //List<DataTable> dts = App.Prj.getRecord(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString());
            txtFieldData.Text = "";

            string recType = App.Prj.getRecord(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString());
            if (recType != "")
            {
                IMessage theRecord = MessageFactory.Create_Record(recType);
                if (theRecord != null) 
                    txtFieldData.Text = theRecord.parseToView(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString());
            }
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
        }
    }
}
