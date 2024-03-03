using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Logger
{
    public delegate void RefreshData();
    public partial class Projects : Form
    {
        //public RefreshData ReloadDataListView;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string HELP_TOPIC = "LoggerBuildProcess\\LoggerBuildProcess.html";

        // adding a list for the hamburger menu
        private ListBox hamburguerMenuOptions;


        public Projects()
        {
            InitializeComponent();
            this.menuStrip1.Font = new Font("Arial", 10);
            this.contextMenuStrip1.Font = new Font("Arial", 10);
            this.contextMenuStrip2.Font = new Font("Arial", 10);

            // settings for application help
            this.KeyPreview = true;
        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.SelectedItems[0].SubItems[0].Text);
        }

        internal async void Projects_Load(object sender, EventArgs e)
        {
            new App().MenuPermissions(App.Prj.Permissions, this.menuStrip1.Items, menusTypes.ProjectOptions);

            Projects.log.Debug((object)"Loading projects info");


            bool result = await loadInfoAsync();


        }

        private void CustomKeyEventHandler(object sender, KeyEventArgs e)
        {
            if(Equals(e.KeyValue,112))
                Help.ShowHelp(this, System.Windows.Forms.Application.StartupPath + "\\manualtest.chm");
        }

        //internal void loadInfo()
        //{
        //    listView1.View = View.Details;
        //    listView1.Name = "ProjectsList";
        //    listView1.Columns.Add("Project Name", 120, HorizontalAlignment.Center);
        //    listView1.Columns.Add("Project Description", 240, HorizontalAlignment.Center);
        //    listView1.Columns.Add("Logs", 40, HorizontalAlignment.Center);
        //    listView1.SmallImageList = imageList1;

        //    listView1.LargeImageList = imageList1;
        //    hamburguerMenu.ForeColor = Color.White;
        //    hamburguerMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        //    //hamburguerMenu.Text = char.ConvertFromUtf32('\u2630'); // "☰";

        //    Projects prForm = new Projects();
        //    prForm.BringToFront();

        //    DataTable dt = App.Prj.getAllProjects();

        //    log.Debug("Retrieving projects info");
        //    listView1.Items.Clear();

        //    foreach (DataRow projectData in dt.Rows)
        //    {

        //        var lvi = new ListViewItem(new string[] { projectData["prjName"].ToString(),
        //                                                  projectData["prjBrief"].ToString(),
        //                                                  projectData["prjLogs"].ToString().Trim() });
        //        lvi.Tag = projectData["prjKey"].ToString();
        //        lvi.ImageIndex = 0;
        //        listView1.Items.Add(lvi);

        //        for (int i = 0; i < listView1.Columns.Count; i++)
        //        {
        //            listView1.Columns[i].Width = 200;
        //        }
        //        listView1.Refresh();
        //    }
        //    if (listView1.Items.Count > 0)
        //    {
        //        listView1.Items[0].Selected = true;
        //        selectedProject = listView1.SelectedItems[0].Text;
        //    }
        //    treeView1.Nodes.Clear();

        //    if (dt.Rows.Count > 0)
        //        listView1_Load(listView1.Items[0]);
        //    else
        //    {
        //        editToolStripMenuItem.Enabled = false;
        //        deleteToolStripMenuItem.Enabled = false;
        //    }
        //}

        internal async Task<bool> loadInfoAsync()
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
            prForm.BringToFront();
            Task<DataTable> dtTask = App.Prj.getAllProjectsAsync();
            DataTable dt = await dtTask;

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

                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    listView1.Columns[i].Width = 200;
                }
                listView1.Refresh();
            }
            //if (!listView1.Items[0].Selected)
            //listView1.Items[0].Selected = true;
            //selectedProject = listView1.SelectedItems[0].Text;

            treeView1.Nodes.Clear();

            if (dt.Rows.Count > 0)
            {
                listView1_Load(listView1.Items[0]);
                editToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                editToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
            return true;

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newProject();
        }

        private void newProject()
        {
            log.Info("Open ProjectInfo");
            ProjectInfo pi = new ProjectInfo();
            pi.ReloadDataListView += new RefreshData(RefresDataListView);
            pi.StartPosition = FormStartPosition.CenterParent;
            pi.BringToFront();
            pi.ShowDialog();
        }

        private void Projects_FormClosing(object sender, FormClosingEventArgs e)
        {
            App.Prj.Admin = false;
            log.Info("Exiting Logger");
            //Logger.MainW mw = new Logger.MainW();
            //mw.Activate();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                log.Info("Listing attched logs " + listView1.SelectedItems.Count.ToString());
                ListViewItem item = new ListViewItem();
                if (listView1.SelectedItems.Count == 1)
                {
                    selectedProject = "";
                    this.TopMost = false;

                    ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
                    item = items[0];
                    Project prj = new Project();
                    prj = App.Prj.getProjectByID(item.Tag.ToString());
                    ProjectData prjData = new ProjectData();
                    prjData.ReloadDataView += new RefreshData(RefresDataListView);
                    prjData.BringToFront();
                    prjData.ShowDialog();
                }
            }

        }

        internal void RefresDataListView()
        {
            loadInfoAsync();
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            bool match = false;

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
            Project pr = new Project();
            DataTable dt = pr.getAllLogs(item.Tag.ToString());

            if ((dt == null) || (dt.Rows.Count == 0))
                return;

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = tn.Text.ToString();

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
                    // mlh adding log id as tag
                    tn.Tag = dr["id"].ToString();
                    tn.ImageIndex = 0;
                    //for (int x = 4; x < dt.Columns.Count - 1; x++)
                    //{
                    //    // if the log was scanned
                    //    if (dr[x].ToString() == "True" || dr[x].ToString() == "true")
                    //    //JMH
                    //    {
                    //        tn.Nodes.Add(dt.Columns[x].ColumnName + "  [" + dicBits[dt.Columns[x].ColumnName] + "]");
                    //        tn.LastNode.Tag = dicBits[dt.Columns[x].ColumnName];
                    //        if (dicBits[dt.Columns[x].ColumnName].ToString() != "0")
                    //            tn.LastNode.ImageIndex = 1;
                    //        else
                    //            tn.LastNode.ImageIndex = 0;
                    //    }
                    //}
                    //mlh

                    for (int x = 4; x < dt.Columns.Count - 1; x++)
                    {
                        // if at least one log was scanned
                        if (dr[x].ToString() == "True" || dr[x].ToString() == "true")
                        {
                            foreach (KeyValuePair<string, int> i in dicBits)
                            {

                                tn.Nodes.Add(i.Key + "  [" + i.Value + "]");
                                tn.LastNode.Tag = i.Value;
                                if (i.Value.ToString() != "0")
                                    tn.LastNode.ImageIndex = 1;
                                else
                                    tn.LastNode.ImageIndex = 0;

                            }
                            break;
                        }
                    }
                    //mlh 
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
            loadInfoAsync();
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

            prjInfo.BringToFront();
            prjInfo.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;

            deleteInfo();
            loadInfoAsync();

        }

        private void deleteInfo()
        {
            if (listView1.Items.Count == 0)
                return;

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

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LogView logView = new LogView();
            TreeView tn = sender as TreeView;
            string nodeText = tn.SelectedNode.Text;
            int indexStartOfNodeText = nodeText.IndexOf("[");
            int indexEndOfNodeText = nodeText.IndexOf("]");
            if ((indexStartOfNodeText > 0) && (indexEndOfNodeText > 0))
            {
                nodeText = tn.SelectedNode.Text.Substring((indexStartOfNodeText + 1),
                            (indexEndOfNodeText - indexStartOfNodeText) - 1);
            }

            if (nodeText == "0")
            {
                return;
            }

            if (nodeText.IndexOf("log") > -1)
                logView.Tag = tn.SelectedNode.Tag + ";" + tn.SelectedNode.Text;
            else
                logView.Tag = tn.Nodes[tn.SelectedNode.Parent.Index].Tag.ToString() + ';' +
                 tn.SelectedNode.Text.Substring(0, tn.SelectedNode.Text.IndexOf("[") - 1).Trim();

            logView.BringToFront();
            logView.Show();

        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            App.Prj.Key = App.Prj.getProjectIDByName(e.Item.Text);

        }

        private void listView1_Click(object sender, EventArgs e)
        {


        }

        private string selectedProject = "";

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {

            if ((e.Clicks == 1) &&
               (e.Button != System.Windows.Forms.MouseButtons.Right))
            {
                if (selectedProject == String.Empty || selectedProject != listView1.SelectedItems[0].Text)
                {
                    selectedProject = listView1.SelectedItems[0].Text;
                    treeView1.Nodes.Clear();
                }
                else return;

                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        listView1_Load(item);
                    }
                    continue;
                }
            }

        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38 && selectedProject == String.Empty)
                return;

            if (listView1.SelectedIndices.Count < 1)
                return;

            int currIndex = listView1.SelectedIndices[0];
            selectedProject = listView1.SelectedItems[0].Text;

            if (e.KeyValue == 38 || e.KeyValue == 40)
            {
                treeView1.Nodes.Clear();

                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Index == currIndex)
                    {
                        listView1_Load(item);
                    }
                    continue;
                }
            }
        }

        private void Projects_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 112)
                // Help.ShowHelp(this, "C:\\Users\\jhand\\Downloads\\manualTest\\manualtest.chm");
                Help.ShowHelp(this, System.Windows.Forms.Application.StartupPath + "\\manualtest.chm", HELP_TOPIC);
                //Help.ShowHelp(this, "C:\\Users\\jhand\\Downloads\\manualTest\\manualtest.chm", HelpNavigator.);
        }
    }
}
