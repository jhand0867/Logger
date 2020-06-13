using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Logger
{
    public partial class Projects : Form
    {
        public Projects()
        {
            InitializeComponent();
        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.SelectedItems[0].SubItems[0].Text);
        }

        internal void Projects_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            listView1.Name = "ProjectsList";
            listView1.Columns.Add("Project Name", 120, HorizontalAlignment.Center);
            listView1.Columns.Add("Project Description", 240, HorizontalAlignment.Center);
            listView1.Columns.Add("Logs", 40, HorizontalAlignment.Center);
            listView1.SmallImageList = imageList1;

            listView1.LargeImageList = imageList1;

            Projects prForm = new Projects();
            prForm.TopMost = true;
            Project pr = new Project();
            Dictionary<string, Project> prjList = pr.getAllProjects();

            listView1.Items.Clear();
            foreach (Project p in prjList.Values)
            {
                var lvi = new ListViewItem(new string[] { p.Name, p.Brief, p.Logs.ToString().Trim() });
                lvi.Tag = p;
                lvi.ImageIndex = 1;
                listView1.Items.Add(lvi);
                listView1.Items[0].Selected = true;

                listView1.Refresh();


            }
            if (prjList.Count > 0)
            {
                treeView1.Nodes.Clear();
                listView1_Load(listView1.Items[0]);
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectInfo pi = new ProjectInfo();
            this.Close();
            pi.ShowDialog();
        }

        private void Projects_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.MainW mw = new Logger.MainW();
            mw.Activate();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = new ListViewItem();
            if (listView1.SelectedItems.Count == 1)
            {
                this.TopMost = false;

                ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
                item = items[0];
                Project prj = item.Tag as Project;
                App.Prj = prj;
                ProjectData prjData = new ProjectData();
                prjData.Show();
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            bool match = false;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                treeView1.Nodes.Clear();

                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        listView1_Load(item);
                    }
                    match = true;
                    continue;
                }
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        listView1.ContextMenuStrip = contextMenuStrip1;
                        match = true;
                        break;
                    }
                }
                if (match)
                {
                    listView1.ContextMenuStrip.Show(listView1, new Point(e.X, e.Y));
                }
             }

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listView1_Load(ListViewItem item)
        {
             Project pr = (Project)item.Tag;
            DataTable dt = pr.getAllLogs(pr.Key);

            if (dt.Rows.Count == 0)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Nodes.Clear();

                string logLocation = dr["logFile"].ToString();

                int logIndex = logLocation.LastIndexOf(@"\") + 1;
                tn.Text = logLocation.Substring(logIndex, logLocation.Length - logIndex);
                tn.ToolTipText = logLocation;

                Dictionary<string, int> dicBits = pr.showRecordBits(dr["id"].ToString());

                for (int x = 4; x < 15; x++)
                {
                    if (dr[x].ToString() == "True" || dr[x].ToString() == "true")
                    {
                        switch (x)
                        {
                            case 4:
                                tn.Nodes.Add("Screens " + dicBits["screens"]);
                                break;
                            case 5:
                                tn.Nodes.Add("States " + dicBits["states"]);
                                break;
                            case 6:
                                tn.Nodes.Add("Configuration Parameters " + dicBits["configParams"]);
                                break;
                            case 7:
                                tn.Nodes.Add("FIT " + dicBits["fit"]);
                                break;
                            case 8:
                                tn.Nodes.Add("Configuration ID " + dicBits["configId"]);
                                break;
                            case 9:
                                tn.Nodes.Add("Enhanced Parameters " + dicBits["enhancedParams"]);
                                break;
                            case 10:
                                tn.Nodes.Add("MAC " );
                                break;
                            case 11:
                                tn.Nodes.Add("Date and Time " + dicBits["dateTime"]);
                                break;
                            case 13:
                                tn.Nodes.Add("Transaction Request " + dicBits["treq"]);
                                break;
                            case 14:
                                tn.Nodes.Add("Transaction Reply " + dicBits["treply"]);
                                break;
                        }
                    }
                }
                treeView1.Nodes.Add(tn);
            }

        }


        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void listToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void iconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void attachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Title = "Upload Log to Project ";

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    string filename = fd.FileName;
                    Project pr = new Project();
                    pr.uploadLog(filename);
                }
            }

        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            editProject();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is Delete");
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editProject();
        }

        private void editProject()
        {
            string prjName = listView1.SelectedItems[0].Text;

            Dictionary<string, Project> dicData = new Dictionary<string, Project>();

            dicData = App.Prj.getProjectByName(prjName);

            ProjectInfo prjInfo = new ProjectInfo();
            Control[] formControls = prjInfo.Controls.Find("btnUpdate", false);
            if (formControls.Length > 0)
            {
                Button btn = (Button)formControls[0];
                btn.Enabled = true;
            }
            foreach (Project pr in dicData.Values)
            {
                App.Prj = pr;
                prjInfo.displayProjectInfo(pr.Name.ToString(), pr.Brief.ToString());
            }
            prjInfo.TopMost = true;
            prjInfo.Show();
        }
    }
}
