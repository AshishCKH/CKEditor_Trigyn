using System;
using XMLEditor.Code;

namespace XMLEditor.Pages
{
    public partial class Error : AuthenticatePage
    {
        #region Page Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);

            if (!ReferenceEquals(Session["SYS_EXCEPTION"], null))
            {
                lblErrorDetails.Text = " Error Details: " + ((Exception)Session["SYS_EXCEPTION"]).ToString();
            }
        }

        #endregion Page Methods
    }
}
