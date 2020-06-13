using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Logger
{
    public partial class Projects : Form
    {
        public Projects()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Project pr = (Project)listView1.SelectedItems[0].SubItems[0].;

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listView1.SelectedItems[0].SubItems[0].Text);
        }

        //lore 
        // private void Projects_Load(object sender, EventArgs e)
        internal void Projects_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            listView1.Name = "ProjectsList";
            listView1.Columns.Add("Project Name", 120, HorizontalAlignment.Center);
            listView1.Columns.Add("Project Description", 240, HorizontalAlignment.Center);
            listView1.Columns.Add("Logs", 40, HorizontalAlignment.Center);

            // setup listview
            // imageList1.ImageSize = new Size(32, 32);
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
                listView1.Refresh();


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
                        Project pr = (Project)item.Tag;
                        DataTable dt = pr.getAllLogs(pr.Key);

                        if (dt.Rows.Count == 0)
                            continue;
                        foreach (DataRow dr in dt.Rows)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Nodes.Clear();
                            
                            string logLocation = dr["logFile"].ToString();

                            int logIndex = logLocation.LastIndexOf(@"\") + 1;
                            tn.Text = logLocation.Substring(logIndex, logLocation.Length - logIndex);
                            tn.ToolTipText = logLocation;

                            for (int x = 4; x < 14; x++)
                            {
                                if (dr[x].ToString() == "True")
                                    {
                                    switch (x)
                                    {
                                        case 4:
                                            tn.Nodes.Add("Screens");
                                            break;
                                        case 5:
                                            tn.Nodes.Add("States");
                                            break;
                                        case 6:
                                            tn.Nodes.Add("Configuration Parameters");
                                            break;
                                        case 7:
                                            tn.Nodes.Add("FIT");
                                            break;
                                        case 8:
                                            tn.Nodes.Add("Configuration ID");
                                            break;
                                        case 9:
                                            tn.Nodes.Add("Enhanced Parameters");
                                            break;
                                        case 10:
                                            tn.Nodes.Add("MAC");
                                            break;
                                        case 11:
                                            tn.Nodes.Add("Date and Time");
                                            break;
                                        case 12:
                                            tn.Nodes.Add("Transaction Request");
                                            break;
                                        case 13:
                                            tn.Nodes.Add("Transaction Reply");
                                            break;
                                    }
                                }
                            }
                            treeView1.Nodes.Add(tn);
                        }
                    }

                    
                    match = true;
                    continue;
                }
            }
            if (match)
            {
                //listView1.ContextMenuStrip.Show(listView1, new Point(e.X, e.Y));
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
                else
                {
                    // show
                }
            }

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
{
    this.Hide();
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
