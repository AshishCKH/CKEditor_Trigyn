using System;
using System.Configuration;

namespace XMLEditor
{
   public partial class SiteMaster : System.Web.UI.MasterPage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
          if (!IsPostBack)
          {
              if (Session["SVNUser"] != null)
              {
                  lnkHome.Visible = true;
                  lnkLogout.Visible = true;
              }
              else
              {
                  lnkHome.Visible = false;
                  lnkLogout.Visible = false;
              }
          }
      }

      protected void lnkLogout_Click(object sender, EventArgs e)
      {
          string url = string.Empty;
          Session.RemoveAll();
          Session.Abandon();

          url = ConfigurationSettings.AppSettings["RediretionURL"];
          url = url + "/Login.aspx";
          Response.Redirect(url);
      }
   }
}
