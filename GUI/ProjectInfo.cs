using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Logger
{
    public partial class ProjectInfo : Form
    {
        private string prevName = "";

        public ProjectInfo()
        {
            InitializeComponent();
            btnUpdate.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Projects pr = new Projects();
            pr.TopMost = true;

            pr.Focus();


        }

        private void createProject_Click(object sender, EventArgs e)
        {
            // create button 
            if (!validatePName())
            {
                // MessageBox.Show("Please enter a valid project name", "Errr", MessageBoxButtons.OK);
                tbPName.Focus();
            }
            else
            {
                Project pr = new Project();

                // do we have this project already?
                Dictionary<string, Project> pDict = pr.getProjectByName(tbPName.Text);
                if (pDict.Count > 0)
                {
                    // MessageBox.Show("Project already exists", "Error!!", MessageBoxButtons.OK);
                    validatePExist();
                    //errorProvider1.SetError(tbPName, "Project already exists");
                    tbPName.Focus();
                }
                else
                {
                    pr.createProject(tbPName.Text, tbPBrief.Text);
                    Projects prj = new Projects();
                    prj.TopMost = true;
                    this.Close();
                    prj.Show();
                }
            }


        }

        public void displayProjectInfo(string name, string brief)
        {
            this.tbPName.Text = name;
            this.tbPBrief.Text = brief;
            this.prevName = name;
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

            // do we have this project name already?

            Dictionary<string, Project> pDict = project.getProjectByName(tbPName.Text);

            if (pDict.Count > 0 &&
                prevName != tbPName.Text)
            {         
                validatePExist();
                tbPName.Focus();
            }
            else
            {
                project.updateProjectByName(App.Prj, tbPName.Text, tbPBrief.Text);
                this.Close();

                Projects obj = (Projects)Application.OpenForms["Projects"];
                obj.Projects_Load(null, null);
            }
        }

        private void tbPName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            validatePName();
        }

        private bool validatePName()
        {
            bool statusPName = true;
            if (tbPName.Text.Trim() == "untitled" ||
                 tbPName.Text.Trim() == "Untitled" ||
                 tbPName.Text.Trim() == "")
            {
                errorProvider1.SetError(tbPName, "Please enter a valid Project Name");
                statusPName = false;
            }
            else
            {
                errorProvider1.SetError(tbPName, "");
                statusPName = true;
            }
            return statusPName;
        }

        private bool validatePExist()
        {
            errorProvider1.SetError(tbPName, "Project already exists");
            return false;
        }
    }
}
