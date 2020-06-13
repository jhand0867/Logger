using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Logger
{
    public partial class ProjectInfo : Form
    {
        public ProjectInfo()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Projects pr = new Projects();
            pr.TopMost = true;

            pr.Focus();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbPName.Text == "Untitled" || tbPName.Text == "")
            {
                MessageBox.Show("Please enter a valid project name", "Errr", MessageBoxButtons.OK);
                tbPName.Focus();
            }
            else
            {
                Project pr = new Project();

                // do we have this project already?
                Dictionary<string, Project> pDict = pr.getProjectByName(tbPName.Text);
                if (pDict.Count > 0)
                {
                    MessageBox.Show("Project already exists", "Error!!", MessageBoxButtons.OK);
                    tbPName.Focus();
                }
                else
                {
                    pr.createProject(tbPName.Text, tbPBrief.Text);
                }
                Projects prj = new Projects();
                prj.TopMost = true;
                this.Close();
                prj.Show();

            }


        }

        public void displayProjectInfo(string name, string brief)
        {
            this.tbPName.Text = name;
            this.tbPBrief.Text = brief;
        }

        private void tbPName_TextChanged(object sender, EventArgs e)
        {
            if (btnUpdate.Enabled == false)
                btnCreate.Enabled = true;
        }

        private void ProjectInfo_Load(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
        }

        private void ProjectInfo_Activated(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Project project = new Project();
            project.updateProjectByName(App.Prj, tbPName.Text, tbPBrief.Text);
            this.Close();

            Projects obj = (Projects)Application.OpenForms["Projects"];
            obj.Projects_Load(null, null);
        }
    }
}
