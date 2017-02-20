using System;
using System.Data;
using XMLEditor.Code;

namespace XMLEditor.Pages
{
    public partial class AddFile : AuthenticatePage
    {
        #region Variables

        int id;
        string path = string.Empty;
        string fileName = string.Empty;

        #endregion Variables

        #region Page Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["Id"]);
                hdnID.Value = id.ToString();
                if (id != 0)
                {
                    DataSet ds = DB.GetByID(id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtPath.Text = ds.Tables[0].Rows[0][0].ToString();
                        txtName.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtPage.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtCachedName.Text = ds.Tables[0].Rows[0][3].ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strFilePath = txtPath.Text;
            string strFileName = txtName.Text;
            string strPage = txtPage.Text;
            string strCachedName = txtCachedName.Text;
            if (!string.IsNullOrEmpty(strFilePath) && !string.IsNullOrEmpty(strFileName))
            {
                int ID = Convert.ToInt32(hdnID.Value);
                if (ID == 0)
                {
                    DB.AddDetail(strFilePath, strFileName, strPage, strCachedName);
                }
                else
                {
                    DB.UpdateDetail(ID, strFilePath, strFileName, strPage, strCachedName);
                }
                Response.Redirect("XMLGetAll.aspx");
            }
            else
            {
                lblMSG.Text = "Please Fill The Detail.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("XMLGetAll.aspx");
        }

        #endregion Page Methods
    }
}