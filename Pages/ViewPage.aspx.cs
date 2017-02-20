using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using XMLEditor.Code;
using System.Web;

namespace XMLEditor.Pages
{
    public partial class ViewPage : AuthenticatePage
    {
        #region Variables

        private int id;

        #endregion Variables

        #region Page Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                hdnID.Value = Request.QueryString["id"].ToString();
                Pageload();
                id = Convert.ToInt32(hdnID.Value);
                DataSet ds = DB.GetByID(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnPath.Value = ds.Tables[0].Rows[0][0].ToString();
                    hdnFileName.Value = ds.Tables[0].Rows[0][1].ToString();
                }
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            //string fileName = hdnFileName.Value.ToString();
            //string strSource = ConfigSettings.SourceFilePath;
            //if (!string.IsNullOrEmpty(strSource) && !string.IsNullOrEmpty(fileName))
            //{
            //    string path = hdnPath.Value.ToString();
            //    string SVNUser = Convert.ToString(Session["SVNUser"]);
            //    if (!string.IsNullOrEmpty(SVNUser))
            //    {
            //        //->To Commit in Main Branch
            //        this.MergeXMLInBranch(path, fileName, SVNUser);
            //    }
            //    else
            //    {
            //        Response.Redirect("~/Login.aspx");
            //    }
            //}
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("XMLGetAll.aspx");
        }

        #endregion Page Methods

        #region Helper Methods

        protected void Pageload()
        {
            string strID = hdnID.Value;
            string strSite = ConfigSettings.LocalSite;
            //To redirct on website and clear cache of the site            
            string url = strSite + "ClearCache.aspx?id=" + strID;
            this.stagingLink.NavigateUrl = url;
            this.stagingLink.Text = url;
            this.stagingDiv.Visible = true;
        }

        private void SVNUpdateXML(string localPath, string SVNUser, string SVNPassword)
        {
            string str = string.Empty;
            string path = hdnPath.Value.ToString();
            string fileName = hdnFileName.Value.ToString();
            path = localPath + path + "\\" + fileName;
           // SVNClient.Update(path, SVNUser, SVNPassword);
        }

        protected void SaveFile(string strPath)
        {
            string str = Session["XMLValue"].ToString();
            if (str.Contains("<content><!"))
            {
                str = str.Replace("<content>", "<content>\r\n");
            }
            string strOrgPath = string.Empty;
            string path = hdnPath.Value.ToString();
            string fileName = hdnFileName.Value.ToString();
            str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + str;
            strOrgPath = strPath + path + "\\" + fileName;
            string strNewPath = string.Empty;
            StreamWriter sw = null;
            FileInfo file = new FileInfo(strOrgPath);
            strNewPath = strPath + path + "\\Copy_" + fileName;
            FileInfo NewFile = new FileInfo(strNewPath);
            NewFile.Delete();
            file.CopyTo(strNewPath);
            file.Delete();
            FileStream fileStream = File.Open(strOrgPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            sw = new StreamWriter(fileStream);
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();
            fileStream.Close();
        }

        private void MergeXMLInBranch(string path, string fileName, string SVNUser)
        {
            try
            {
                string comment, svnCommitUser, svnCommitUserPassword, url, message = "";
                string productionSVNWorkingFolderPath, devSVNWorkingFolderPath, qaSVNWorkingFolderPath, productionSVNPath;
                long latestRevision = 0;
                ICollection<string> images = new Collection<string>();

                svnCommitUser = MagicStrings.USERNAME;
                svnCommitUserPassword = MagicStrings.PASSWORD;
                productionSVNWorkingFolderPath = ConfigSettings.ProductionSVNWorkingFolderPath;
                devSVNWorkingFolderPath = ConfigSettings.DevSVNWorkingFolderPath;
                qaSVNWorkingFolderPath = ConfigSettings.QASVNWorkingFolderPath;

                productionSVNPath = ConfigSettings.ProductionSVNPath;

                //Update Prod branch
                //SVNClient.Update(productionSVNWorkingFolderPath, svnCommitUser, svnCommitUserPassword);

                //Save XML file
                this.SaveFile(productionSVNWorkingFolderPath);

                // Add new image files to SVN and commit those files to Prod Branch
                if (Session["Images"] != null)
                {
                    images = (ICollection<string>)(Session["Images"]);
                    ICollection<string> finalListOfImages = new Collection<string>();
                    foreach (string imageFileName in images)
                    {
                        string physicalLocationOfUploadedImages = Server.MapPath("~") + imageFileName;
                        string saveImageToLocation = productionSVNWorkingFolderPath + imageFileName;
                        if (!File.Exists(saveImageToLocation))
                        {
                            finalListOfImages.Add(saveImageToLocation);
                            FileInfo imageFile = new FileInfo(physicalLocationOfUploadedImages);
                            imageFile.CopyTo(saveImageToLocation, true);
                        }
                    }

                    comment = ConfigurationManager.AppSettings["SVNComment"] + " - Added new file to Images folder " + fileName + " - Modified by: " + SVNUser;
                    //latestRevision = SVNClient.AddFile(finalListOfImages, productionSVNWorkingFolderPath, svnCommitUser, svnCommitUserPassword, comment);
                }
                else //Commit only modified files in Prod branch 
                {
                    comment = ConfigurationManager.AppSettings["SVNComment"] + " - Updated XML Content of " + fileName + " - Modified by: " + SVNUser;
                    //latestRevision = SVNClient.Commit(productionSVNWorkingFolderPath, svnCommitUser, svnCommitUserPassword, comment);
                }

                //Update Dev branch
                //SVNClient.Update(devSVNWorkingFolderPath, svnCommitUser, svnCommitUserPassword);

                // Merge and Commit Dev Branch
                //SVNClient.Merge(devSVNWorkingFolderPath, productionSVNPath, svnCommitUser, svnCommitUserPassword, latestRevision);
                comment = comment + " - Merging from prod";
                //long devRevision = SVNClient.Commit(devSVNWorkingFolderPath, svnCommitUser, svnCommitUserPassword, comment);

                //Update QA branch
                //SVNClient.Update(qaSVNWorkingFolderPath, svnCommitUser, svnCommitUserPassword);

                // Merge and Commit QA Branch
                //SVNClient.Merge(qaSVNWorkingFolderPath, productionSVNPath, svnCommitUser, svnCommitUserPassword, latestRevision);
                //long qaRevision = SVNClient.Commit(qaSVNWorkingFolderPath, svnCommitUser, svnCommitUserPassword, comment);
                
                // Transfer images and file to Production Server
                foreach (string imageFileName in images)
                {
                    message = "<br/>" + Utils.TransferFileToProduction("", imageFileName.TrimStart(new char[] { '\\' }));
                    DisplayErrorMessage(message);
                }
                message = "<br/>" + Utils.TransferFileToProduction(path, fileName);

                if (!DisplayErrorMessage(message))
                {
                    string resetStatusMessage = Utils.ResetAppPool();
                    DisplayErrorMessage(resetStatusMessage);
                }

                spnMessage.InnerText = "The xml file is saved successfully on production site and changes are commited to Production, Dev and QA branch of SVN. Please click on the below link to see the production changes";
                url = ConfigSettings.ProductionSite + "ClearCache.aspx?id=" + hdnID.Value;

                this.productionLink.NavigateUrl = url;
                this.productionLink.Text = url;
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                spnErrorMessage.InnerHtml = "Error: <br/>The xml file was not saved successfully. Please check the email for further details.";
                Utils.SendErrorMail(ex, "Error while commiting");
            }

            this.productionDiv.Visible = true;
        }

        private bool DisplayErrorMessage(string message)
        {
            if (message.StartsWith("<br/>Error Occurred") || message.StartsWith("<br/>Invalid Credentials"))
            {
                spnErrorMessage.InnerHtml = "The following occurred while executing the web service: " + message + spnErrorMessage.InnerHtml;
                Exception ex = new Exception(message);
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                return true;
            }
            return false;
        }

        #endregion Helper Methods
    }
}