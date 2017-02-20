using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using XMLEditor.Code;
using System.IO;

namespace XMLEditor.Pages
{
    public partial class XMLGetAll : AuthenticatePage
    {
        #region Page Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                gvXML.DataSource = this.getDetail();
                gvXML.DataBind();
                Session["XMLValue"] = null;
                Session["IsTextChanged"] = null;
                Session["OriginalXMLValue"] = null;
            }
        }

        protected void gvXML_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    string url = string.Format(CultureInfo.CurrentCulture, "Update.aspx?Id={0}", e.CommandArgument.ToString());
                    Response.Redirect(url);
                }
            }
            if (e.CommandName == "Edit")
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    string url = string.Format(CultureInfo.CurrentCulture, "AddFile.aspx?Id={0}", e.CommandArgument.ToString());
                    Response.Redirect(url);
                }
            }
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddFile.aspx?Id=0");
        }

        protected void gvXML_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string xmlLocation = Convert.ToString(((DataRowView)e.Row.DataItem).Row.ItemArray[1]);
                string filename = Convert.ToString(((DataRowView)e.Row.DataItem).Row.ItemArray[2]);
                string prodPublishedFile = ConfigSettings.SourceFilePath + xmlLocation + "\\" + filename;
                string prodSourceFile = ConfigSettings.ProductionSVNWorkingFolderPath + xmlLocation + "\\" + filename;
                LinkButton lnkRevert = (LinkButton)e.Row.FindControl("lnkRevert");
                lnkRevert.OnClientClick = "return ShowConfirmBox('" + xmlLocation + "\\\\" + filename + "')";

                if (File.GetLastWriteTime(prodPublishedFile).ToString("MM/dd/yyyy hh tt") == File.GetLastWriteTime(prodSourceFile).ToString("MM/dd/yyyy hh tt"))
                {
                    Image imgWarning = (Image)e.Row.FindControl("imgWarning");                 
                    imgWarning.Visible = false;
                    lnkRevert.Visible = false;
                }
            }
        }

        protected void lnkRevert_Click(object sender, EventArgs e)
        {
            LinkButton lnkRevert = ((LinkButton)(sender));
            string xmlFilename = lnkRevert.CommandArgument;
            string prodPublishedFile = ConfigSettings.SourceFilePath + "\\" + xmlFilename;
            string prodSourceFile = ConfigSettings.ProductionSVNWorkingFolderPath + "\\" + xmlFilename;
            FileInfo file = new FileInfo(prodSourceFile);
            file.CopyTo(prodPublishedFile, true);
            gvXML.DataSource = this.getDetail();
            gvXML.DataBind();
        }

        #endregion Page Methods

        #region Helper Methods

        private DataSet getDetail()
        {
            return DB.GetAllXML();
        }

        #endregion Helper Methods
    }
}