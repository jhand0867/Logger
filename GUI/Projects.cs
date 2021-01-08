using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Logger
{
    public partial class Projects : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            log.Debug("Loading projects info");

            loadInfo();

        }

        private void loadInfo()
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
            //prForm.MdiParent = MainW;

            //Project pr = new Project();
            //Dictionary<string, Project> prjList = pr.getAllProjects();
            Dictionary<string, Project> prjList = App.Prj.getAllProjects();

            log.Debug("Retrieving projects info");
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
            else
            {
                editToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Debug("Open ProjectInfo");
            ProjectInfo pi = new ProjectInfo();
            this.Close();
            pi.ShowDialog();
        }

        private void Projects_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.Debug("Exiting Logger");
            Logger.MainW mw = new Logger.MainW();
            mw.Activate();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            log.Debug("Listing attched logs " + listView1.SelectedItems.Count.ToString());
            ListViewItem item = new ListViewItem();
            if (listView1.SelectedItems.Count == 1)
            {
                this.TopMost = false;

                ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
                item = items[0];
                Project prj = item.Tag as Project;
                App.Prj = prj;
                ProjectData prjData = new ProjectData();
                prjData.ShowDialog();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
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

                var fname = dt.Columns[0].ColumnName;

                if (dicBits != null)
                {
                    for (int x = 4; x < dt.Columns.Count - 1; x++)
                    {
                        if (dr[x].ToString() == "True" || dr[x].ToString() == "true")
                        {
                            tn.Nodes.Add(dt.Columns[x].ColumnName + "  [" + dicBits[dt.Columns[x].ColumnName] + "]");
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
            deleteInfo();
            loadInfo();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editProject();
        }

        private void editProject()
        {
            if (listView1.Items.Count == 0)
            {
                return;
            }
            string prjName = listView1.SelectedItems[0].Text;

            Dictionary<string, Project> dicData = new Dictionary<string, Project>();

            dicData = App.Prj.getProjectByName(prjName);

            ProjectInfo prjInfo = MessageFactory.Create_ProjectInfo();
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteInfo();

            loadInfo();
        }

        private void deleteInfo()
        {
            if (listView1.Items.Count == 0)
            {
                return;
            }
            string prjName = listView1.SelectedItems[0].Text;
            var result = MessageBox.Show($"This will delete the project: {prjName} and all its logs\n do you want to continue",
                "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (App.Prj.deleteProjectByName(prjName))
                {
                    log.Info($"Project Deleted: {prjName}");
                }
                else
                {
                    log.Info($"Unable to delete Project: {prjName}");
                }

            }
        }
    }
}
