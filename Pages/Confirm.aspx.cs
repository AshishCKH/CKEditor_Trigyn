using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using XMLEditor.Code;

namespace XMLEditor.Pages
{
    public partial class Confirm : AuthenticatePage
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
                }

                if (Session["XMLValue"] != null)
                {
                    DataSet dsPageLayout = new DataSet();
                    dsPageLayout.ReadXml(Server.MapPath("~/Config/PageLayout.xml"));

                    DataRow[] drPageLayout = dsPageLayout.Tables[0].Select(" xmlFilePath = '" + this.hdnPath.Value + "' and xmlFileName = '" + this.hdnFileName.Value + "' ");

                    foreach (DataRow dr in drPageLayout)
                    {
                        if (Convert.ToString(dr["leftExists"]) == "NO")
                        {
                            this.divXmlContent.Style.Remove("padding-left");
                        }
                        if (Convert.ToString(dr["rightExists"]) == "NO")
                        {
                            this.divXmlContent.Style.Remove("padding-right");
                        }

                        this.divXmlContent.Style["width"] = Convert.ToString(dr["width"]);
                    }

                    string str = Session["XMLValue"].ToString();
                    this.divXmlContent.InnerHtml = str;
                }
                else
                {
                    this.divXmlContent.InnerHtml = "Error In Load";
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string xmlData = divXmlContent.InnerHtml;
           
            //Save the images to Prod Published folder
            ICollection<string> images = new Collection<string>();
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(xmlData);
            var src = htmlDoc.DocumentNode.Descendants("img").Select(x => x.Attributes).Select(x => x.AttributesWithName("src"));
            string sourcePath = ConfigSettings.SourceFilePath;
            string prodSourceFile = ConfigSettings.ProductionSVNWorkingFolderPath;
            Session["Images"] = null;
            string filePath, imagePath, directoryName;

            foreach (var imageSrc in src)
            {
                imagePath = imageSrc.First().Value.Replace("/", "\\").Replace("..", "").Replace("images", "Images");
                imagePath = Server.UrlDecode(imagePath);
                filePath = sourcePath + imagePath;

                if (!File.Exists(filePath) || !File.Exists(prodSourceFile + imagePath))
                {                    
                    directoryName = filePath.Substring(0, filePath.LastIndexOf("\\"));
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    FileInfo imageFile = new FileInfo(Server.MapPath("~") + imagePath);
                    imageFile.CopyTo(filePath, true);

                    images.Add(imagePath);
                }
            }
            if (images.Count > 0)
            {
                Session["Images"] = images;
            }

            if (xmlData.Contains("<content><!"))
            {
                xmlData = xmlData.Replace("<content>", "<content>\r\n");
            }
            string strOrgPath = string.Empty;
            string path = hdnPath.Value.ToString();
            string fileName = hdnFileName.Value.ToString();
            if (!xmlData.Contains("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n"))
            { 
                xmlData = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + xmlData;
            }

            strOrgPath = sourcePath + path + "\\" + fileName;
            string strNewPath = string.Empty;
            StreamWriter sw = null;
            FileInfo file = new FileInfo(strOrgPath);
            strNewPath = sourcePath + path + "\\Copy_" + fileName;
            FileInfo NewFile = new FileInfo(strNewPath);
            NewFile.Delete();
            file.CopyTo(strNewPath);
            file.Delete();
            FileStream fileStream = File.Open(strOrgPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            sw = new StreamWriter(fileStream);
            sw.WriteLine(xmlData);
            sw.Flush();
            sw.Close();
            fileStream.Close();
            string strID = hdnID.Value;
            Response.Redirect("ViewPage.aspx?id=" + strID);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string strID = hdnID.Value;
            Response.Redirect("Update.aspx?id=" + strID);
        }

        #endregion Page Methods
    }
}