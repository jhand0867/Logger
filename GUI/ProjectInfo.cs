using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Logger
{
   

    public partial class ProjectInfo : Form
    {
        public RefreshData ReloadDataListView;

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
                tbPName.Focus();
            }
            else
            {
                Project pr = new Project();

                // do we have this project already?
                DataTable  projectData = pr.getProjectByName(tbPName.Text);
                if (projectData.Rows.Count > 0)
                {
                    // Project already exists
                    validatePExist();
                    tbPName.Focus();
                }
                else
                {
                    pr.createProject(tbPName.Text, tbPBrief.Text);
                    ReloadDataListView();
                    
                    this.Close();
                    

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

            DataTable projectData = project.getProjectByName(tbPName.Text);

            if (projectData.Rows.Count > 0 &&
                prevName != tbPName.Text)
            {
                validatePExist();
                tbPName.Focus();
            }
            else
            {
                project.updateProjectByName(App.Prj, tbPName.Text, tbPBrief.Text);
                this.Close();
                ReloadDataListView();
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
