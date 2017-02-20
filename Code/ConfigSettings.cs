using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace XMLEditor.Code
{
    public class ConfigSettings
    {
        public static string RediretionURL
        {
            get
            {
                string rediretionURL = ConfigurationManager.AppSettings["RediretionURL"].ToString();
                CheckBackSlash(ref rediretionURL);

                return rediretionURL;
            }
        }

        public static string SourceFilePath
        {
            get
            {
                string sourceFilePath = ConfigurationManager.AppSettings["SourceFilePath"].ToString();
                CheckBackSlash(ref sourceFilePath);

                return sourceFilePath;
            }
        }

        public static string DestinationFilePath
        {
            get
            {
                string destinationFilePath = ConfigurationManager.AppSettings["DestinationFilePath"].ToString();
                CheckBackSlash(ref destinationFilePath);

                return destinationFilePath;
            }
        }

        public static string BatchFilePath
        {
            get
            {
                string batchFilePath = ConfigurationManager.AppSettings["BatchFilePath"].ToString();
                CheckBackSlash(ref batchFilePath);

                return batchFilePath;
            }
        }

        public static string ProductionSVNWorkingFolderPath
        {
            get
            {
                string productionSVNWorkingFolderPath = ConfigurationManager.AppSettings["ProductionSVNWorkingFolderPath"].ToString();
                CheckBackSlash(ref productionSVNWorkingFolderPath);

                return productionSVNWorkingFolderPath;
            }
        }

        public static string DevSVNWorkingFolderPath
        {
            get
            {
                string devSVNWorkingFolderPath = ConfigurationManager.AppSettings["DevSVNWorkingFolderPath"].ToString();
                CheckBackSlash(ref devSVNWorkingFolderPath);

                return devSVNWorkingFolderPath;
            }
        }

        public static string QASVNWorkingFolderPath
        {
            get
            {
                string qaSVNWorkingFolderPath = ConfigurationManager.AppSettings["QASVNWorkingFolderPath"].ToString();
                CheckBackSlash(ref qaSVNWorkingFolderPath);

                return qaSVNWorkingFolderPath;
            }
        }

        public static string LocalSite
        {
            get
            {
                string localSite = ConfigurationManager.AppSettings["LocalSite"].ToString();
                CheckForwardSlash(ref localSite);

                return localSite;
            }
        }

        public static string ProductionSite
        {
            get
            {
                string productionSite = ConfigurationManager.AppSettings["ProductionSite"].ToString();
                CheckForwardSlash(ref productionSite);

                return productionSite;
            }
        }

        public static string ProductionSVNPath
        {
            get
            {
                string productionSVNPath = ConfigurationManager.AppSettings["ProductionSVNPath"].ToString();
                CheckForwardSlash(ref productionSVNPath);

                return productionSVNPath;
            }
        }

        public static string XMLUtilityApplicationPath
        {
            get
            {
                string xmlUtilityApplicationPath = ConfigurationManager.AppSettings["XMLUtilityApplicationPath"].ToString();
                CheckForwardSlash(ref xmlUtilityApplicationPath);

                return xmlUtilityApplicationPath;
            }
        }

        internal static string CryptKey
        {
            get
            {
                return ConfigurationManager.AppSettings["CryptKey"].ToString();
            }
        }

        internal static string WebServiceUrlToUploadFile
        {
            get
            {
                return ConfigurationManager.AppSettings["WebServiceUrlToUploadFile"].ToString();
            }
        }

        internal static string WebServiceUrlToRecycleAppPool
        {
            get
            {
                return ConfigurationManager.AppSettings["WebServiceUrlToRecycleAppPool"].ToString();
            }
        }

        private static void CheckBackSlash(ref string value)
        {
            if (!value.EndsWith("\\"))
            {
                value = value + "\\";
            }
        }

        private static void CheckForwardSlash(ref string value)
        {
            if (!value.EndsWith("/"))
            {
                value = value + "/";
            }
        }

    }
}