using System;
using System.Configuration;
using System.Data;
using System.IO;
using XMLEditor.Code;

namespace XMLEditor.Pages
{
    public partial class Update : AuthenticatePage
    {
        #region Page Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                int id;
                id = Convert.ToInt32(Request.QueryString["Id"]);
                this.hdnID.Value = id.ToString();
                DataSet ds = DB.GetByID(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.hdnPath.Value = ds.Tables[0].Rows[0][0].ToString();
                    this.hdnFileName.Value = ds.Tables[0].Rows[0][1].ToString();                
                    this.ReadXML();
                }
            }


            // Integrate CKFinder with CKEditor
            CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
            _FileBrowser.BasePath = "/ckfinder/";
            _FileBrowser.SetupCKEditor(txtXML);
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string strID = hdnID.Value;
            string str = txtXML.Text;
            string url = string.Empty;
            string originalXMLText = Convert.ToString(Session["OriginalXMLValue"]);
            if (originalXMLText.Contains("<content>"))
            {
                originalXMLText = originalXMLText.Substring(originalXMLText.IndexOf("<content>"), originalXMLText.IndexOf("</content>") - originalXMLText.IndexOf("<content>"));
                string modifiedXMLText = str.Substring(str.IndexOf("<content>"), str.IndexOf("</content>") - str.IndexOf("<content>"));
                if (originalXMLText != modifiedXMLText)
                {
                    Session["IsTextChanged"] = "true";
                }
            }
            Session["XMLValue"] = str;
            Session["XMLId"] = strID;

            url = ConfigSettings.RediretionURL + "Confirm.aspx?id=" + strID;

            Response.Redirect(url);
        }

        #endregion Page Methods

        #region Helper Methods

        //private void SVNUpdateXML()
        //{
        //    string str = string.Empty;
        //    string path = hdnPath.Value.ToString();
        //    string fileName = hdnFileName.Value.ToString();
        //    string strPath = ConfigSettings.SourceFilePath;
        //    path = strPath + path + "\\" + fileName;
        //    string SVNUser = Convert.ToString(Session["SVNUser"]);
        //    string SVNPassword = Convert.ToString(Session["SVNPassword"]);
        //    if (!string.IsNullOrEmpty(SVNUser) && !string.IsNullOrEmpty(SVNPassword))
        //    {
        //        //SVNClient.Update(path, SVNUser, SVNPassword);
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}

        private void ReadXML()
        {
            if (Convert.ToString(Session["XMLId"]) == hdnID.Value && !ReferenceEquals(Session["XMLValue"], null))
            {
                txtXML.Text = Convert.ToString(Session["XMLValue"]);
            }
            else
            {
                string str = string.Empty;
                string path = hdnPath.Value.ToString();
                string fileName = hdnFileName.Value.ToString();
                string strPath = ConfigSettings.SourceFilePath;
                path = strPath + path + "\\" + fileName;
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line = sr.ReadToEnd();
                        str = str + line;
                    }
                    txtXML.Text = str;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                Session["XMLId"] = hdnID.Value;
                Session["OriginalXMLValue"] = txtXML.Text;
            }
        }

        #endregion Helper Methods
    }
}