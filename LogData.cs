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
            txtGroup8.Text = dgvr.Cells["group8"].Value.ToString();

        }
    }
}
