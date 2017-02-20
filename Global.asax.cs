using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace XMLEditor
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            if (HttpContext.Current.Session != null)
            {
                Session["SYS_EXCEPTION"] = Server.GetLastError();
                Server.ClearError();
            }
            else
            {
                HttpContext.Current.Items.Add("SYS_EXCEPTION", Server.GetLastError());
                Server.ClearError();
            }
            //Server.Transfer("~/Pages/Error.aspx");
            Response.Redirect("~/Pages/Error.aspx", false);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        public override void Init()
        {
            base.Init();
            try
            {
                // Get the app name from config file...
                string appName = ConfigurationManager.AppSettings["ApplicationName"];
                if (!string.IsNullOrEmpty(appName))
                {
                    foreach (string moduleName in this.Modules)
                    {
                        IHttpModule module = this.Modules[moduleName];
                        SessionStateModule ssm = module as SessionStateModule;
                        if (ssm != null)
                        {
                            FieldInfo storeInfo = typeof(SessionStateModule).GetField("_store", BindingFlags.Instance | BindingFlags.NonPublic);
                            SessionStateStoreProviderBase store = (SessionStateStoreProviderBase)storeInfo.GetValue(ssm);
                            if (store == null) //In IIS7 Integrated mode, module.Init() is called later
                            {
                                FieldInfo runtimeInfo = typeof(HttpRuntime).GetField("_theRuntime", BindingFlags.Static | BindingFlags.NonPublic);
                                HttpRuntime theRuntime = (HttpRuntime)runtimeInfo.GetValue(null);
                                FieldInfo appNameInfo = typeof(HttpRuntime).GetField("_appDomainAppId", BindingFlags.Instance | BindingFlags.NonPublic);
                                appNameInfo.SetValue(theRuntime, appName);
                            }
                            else
                            {
                                Type storeType = store.GetType();
                                if (storeType.Name.Equals("OutOfProcSessionStateStore"))
                                {
                                    FieldInfo uribaseInfo = storeType.GetField("s_uribase", BindingFlags.Static | BindingFlags.NonPublic);
                                    uribaseInfo.SetValue(storeType, appName);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // log.Error(ex.Message, ex);
            }
        }

    }
}
