using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Logger
{
    public partial class LogData : Form
    {


        private int prevHeight;

        public int PrevHeight { get => prevHeight; set => prevHeight = value; }

        public LogData()
        {
            InitializeComponent();
            PrevHeight = this.Height;

        }

        private void LogData_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Dispose();
            //GC.Collect();

        }

        private void LogData_Load(object sender, EventArgs e)
        {
            rtbRawData.Width = this.Width - 40;
            txtFieldData.Width = this.Width - 40;
            txtFieldData.Height = this.PrevHeight - 350;
            Point prevButtonLocation = btnPrev.Location;
            btnPrev.Location = new Point(this.Width - 80, prevButtonLocation.Y);
            Point nextButtonLocation = btnNext.Location;
            btnNext.Location = new Point(this.Width - 80, nextButtonLocation.Y);
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
            // MLH temporary comment out testing adding table
            txtFieldData.Text = "";

            string recType = App.Prj.getRecord(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString());
            if (recType != "")
            {
                IMessage theRecord = LoggerFactory.Create_Record(recType);
                // MLH temporary comment out testing adding table
                if (theRecord != null)
                {
                    StringBuilder strTable = new StringBuilder();

                    strTable.Append(@"{\rtf1");
                    strTable.Append(@"{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;
            \red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;\red0\green0\blue0;\red0\green0\blue0;}");
                    strTable.Append(@"\trbrdrt\brdrs\brdrw10
\trbrdrl\brdrs\brdrw10
\trbrdrb\brdrs\brdrw10
\trbrdrr\brdrs\brdrw10");
                    strTable.Append(@"\trowd\trgaph200");
                    strTable.Append(@"\clbrdrt\brdrw10\brdrs
\clbrdrl\brdrw10\brdrs
\clbrdrb\brdrw10\brdrs
\clbrdrr\brdrw10\brdrs \cellx3000");
                    strTable.Append(@"\clbrdrt\brdrw10\brdrs
\clbrdrl\brdrw10\brdrs
\clbrdrb\brdrw10\brdrs
\clbrdrr\brdrw10\brdrs \cellx5000");
                    strTable.Append(@"\clbrdrt\brdrw10\brdrs
\clbrdrl\brdrw10\brdrs
\clbrdrb\brdrw10\brdrs
\clbrdrr\brdrw10\brdrs \cellx9000");
                    //txtFieldData.Text = theRecord.parseToView(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString());
                    strTable.Append(theRecord.parseToView(logKey, logID, prjKey, dgvr.Cells["Log Data"].Value.ToString()));
                    strTable.Append(@"}");
                    txtFieldData.Rtf = strTable.ToString();
                }

            }
        }

        public ReceiveLogData getPrevRow;
        public ReceiveLogData getNextRow;

        private void btnNext_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = getNextRow();
            if (dgvr != null)
                setData(dgvr);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = getPrevRow();
            if (dgvr != null)
                setData(dgvr);
        }

        private void LogData_Resize(object sender, EventArgs e)
        {
            rtbRawData.Width = this.Width - 40;
            txtFieldData.Width = this.Width - 40;
            Point prevButtonLocation = btnPrev.Location;
            btnPrev.Location = new Point(this.Width - 80, prevButtonLocation.Y);
            Point nextButtonLocation = btnNext.Location;
            btnNext.Location = new Point(this.Width - 80, nextButtonLocation.Y);
            txtFieldData.Height = this.Height - 350;
        }

        private void txtFieldData_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFieldData_MouseClick(object sender, MouseEventArgs e)
        {
            //string linkContent = null;
            //RichTextBox contentFieldData = (RichTextBox)sender;
            //int indexOfLink = contentFieldData.Text.IndexOf(@"https://");
            //if (indexOfLink > -1)
            //    linkContent = contentFieldData.Text.Substring(indexOfLink, contentFieldData.Text.IndexOf("\n\t\n", indexOfLink) - indexOfLink);
            ////if(linkContent != null)
            ////    System.Diagnostics.Process.Start(linkContent);

        }
    }
}
