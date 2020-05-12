using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KtuLab4
{
    public partial class CompanyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            foreach (string branch in IO.GetAllBranches())
            {
                BranchDDL.Items.Add(branch);
            }
        }

        protected void ExecButton_Click(object sender, EventArgs e)
        {
            string selectedBranchName = BranchDDL.SelectedValue;
            CompanyProgram.Run(DataPanel, ResultPanel, selectedBranchName);
        }
    }
}