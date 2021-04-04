using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Logger
{
    public delegate void RefreshData();
    public partial class Projects : Form
    {
        //public RefreshData ReloadDataListView;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // adding a list for the hamburger menu
        private ListBox hamburguerMenuOptions;


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

        internal void loadInfo()
        {
            listView1.View = View.Details;
            listView1.Name = "ProjectsList";
            listView1.Columns.Add("Project Name", 120, HorizontalAlignment.Center);
            listView1.Columns.Add("Project Description", 240, HorizontalAlignment.Center);
            listView1.Columns.Add("Logs", 40, HorizontalAlignment.Center);
            listView1.SmallImageList = imageList1;
            
            listView1.LargeImageList = imageList1;
            hamburguerMenu.ForeColor = Color.White;
            hamburguerMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            //hamburguerMenu.Text = char.ConvertFromUtf32('\u2630'); // "☰";

            Projects prForm = new Projects();
            prForm.TopMost = true;
            //prForm.MdiParent = MainW;

            //Project pr = new Project();
            //Dictionary<string, Project> prjList = pr.getAllProjects();
            DataTable dt = App.Prj.getAllProjects();

            log.Debug("Retrieving projects info");
            listView1.Items.Clear();

            foreach (DataRow projectData in dt.Rows)
            {

                var lvi = new ListViewItem(new string[] { projectData["prjName"].ToString(), 
                                                          projectData["prjBrief"].ToString(), 
                                                          projectData["prjLogs"].ToString().Trim() });
                lvi.Tag = projectData["prjKey"].ToString();
                lvi.ImageIndex = 0;
                listView1.Items.Add(lvi);
                listView1.Items[0].Selected = true;
                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    listView1.Columns[i].Width = 200;
                }

                listView1.Refresh();


            }

            treeView1.Nodes.Clear();

            if (dt.Rows.Count > 0)
                listView1_Load(listView1.Items[0]);
            else
            {
                editToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newProject();
        }

        private void newProject()
        {
            log.Debug("Open ProjectInfo");
            ProjectInfo pi = new ProjectInfo();
            pi.ReloadDataListView += new RefreshData(RefresDataListView);
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
                //Project prj = App.Prj.getProjectByID(item.Tag.ToString());
                App.Prj = App.Prj.getProjectByID(item.Tag.ToString());
                ProjectData prjData = new ProjectData();
                prjData.ReloadDataView += new RefreshData(RefresDataListView);

                prjData.ShowDialog();
            }
            
        }

        internal void RefresDataListView()
        {
            loadInfo();
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
            Project pr = new Project() ;
            DataTable dt = pr.getAllLogs(item.Tag.ToString());

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
                    tn.Tag = 0;
                    tn.ImageIndex = 0;
                    for (int x = 4; x < dt.Columns.Count - 1; x++)
                    {
                        if (dr[x].ToString() == "True" || dr[x].ToString() == "true")
//JMH
                        {
                            tn.Nodes.Add(dt.Columns[x].ColumnName + "  [" + dicBits[dt.Columns[x].ColumnName] + "]");
                            tn.LastNode.Tag = dicBits[dt.Columns[x].ColumnName];
                            if (dicBits[dt.Columns[x].ColumnName].ToString() != "0")
                                 tn.LastNode.ImageIndex = 1;
                            else
                                 tn.LastNode.ImageIndex = 0;
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
            if (listView1.SelectedItems.Count != 1)
            {
                return;
            }
            string prjName = listView1.SelectedItems[0].Text;

            DataTable projectData = new DataTable();

            projectData = App.Prj.getProjectByName(prjName);


            ProjectInfo prjInfo = LoggerFactory.Create_ProjectInfo();
            prjInfo.ReloadDataListView += new RefreshData(RefresDataListView);

            // prjInfo.Controls["btnUpdate"].Enabled = true
            Control[] formControls = prjInfo.Controls.Find("btnUpdate", false);
            Control[] formControls1 = prjInfo.Controls.Find("tbPName", false);
            if (formControls.Length > 0)
            {
                Button btn = (Button)formControls[0];
                btn.Enabled = true;
            }
            foreach (DataRow dr in projectData.Rows)
            {
                //App.Prj = pr;
                prjInfo.displayProjectInfo(dr["prjName"].ToString(), dr["prjBrief"].ToString());
            }

            prjInfo.TopMost = true;
            prjInfo.ShowDialog();
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

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void bigIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void hamburguerMenu_MouseDown(object sender, MouseEventArgs e)
        {
            hamburguerMenuOptions = new ListBox();
            hamburguerMenuOptions.SuspendLayout();
            hamburguerMenuOptions.Size = new Size(100, 30);
            hamburguerMenuOptions.Location = new Point(e.Location.X + 100, e.Location.Y + 100);
            hamburguerMenuOptions.Visible = true;
            hamburguerMenuOptions.Show();
            hamburguerMenuOptions.Items.Add("Detail");
            hamburguerMenuOptions.Items.Add("Big Icons");


            if (e.Button == MouseButtons.Left)
            {
                //create a list with the options
                this.Controls.Add(hamburguerMenuOptions);
                hamburguerMenuOptions.ResumeLayout();
            }
        }

        private void detailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void listToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void bigIconsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // new on context menu
            newProject();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            if (Convert.ToInt32(tv.SelectedNode.Tag) != 0)
            {
                tv.SelectedNode.ImageIndex = 1;
                tv.SelectedNode.SelectedImageIndex = 1;
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void treeView1_Click(object sender, EventArgs e)
        //{
        //    TreeView tv = (TreeView)sender;
        //    if (tv != null)
        //    {
        //        tv.SelectedNode.ImageIndex = 1;
        //    }
        //}

    }
}
