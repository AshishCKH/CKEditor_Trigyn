using System;
using System.Configuration;
using System.Data;
using XMLEditor.Code;

namespace XMLEditor
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (Session["SVNUser"] != null)
            {
                Response.Redirect("Pages/XMLGetAll.aspx", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    txtUser.Focus();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = DB.Login(txtUser.Text.Trim(), txtPass.Text.Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["SVNUser"] = ds.Tables[0].Rows[0]["SVNUser"].ToString();
                Session["SVNPassword"] = ds.Tables[0].Rows[0]["SVNPassword"].ToString();
                Response.Redirect("Pages/XMLGetAll.aspx", false);
            }
            else
            {
                Clear();
                lblError.Text = "Invalid Login Credential";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtPass.Attributes.Add("Text", string.Empty);
            txtUser.Text = string.Empty;
            lblError.Text = string.Empty;
        }
    }
}