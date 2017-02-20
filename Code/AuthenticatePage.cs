using System;
using System.Collections.Generic;
using System.Web;

namespace XMLEditor.Code
{
    public class AuthenticatePage : System.Web.UI.Page
    {
        public new void OnLoad(EventArgs e)
        {            
            string SVNUser = Convert.ToString(Session["SVNUser"]);
            string SVNPassword = Convert.ToString(Session["SVNPassword"]);
            if (string.IsNullOrEmpty(SVNUser) || string.IsNullOrEmpty(SVNPassword))
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}