using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.IO;
using System.Web;
using Utilities;
using System.Net;

namespace XMLEditor.Code
{
    public class Utils
    {
        internal static string TransferFileToProduction(string path, string fileName)
        {
            long fileLength = 0;

            string sourceFilePath = ConfigSettings.SourceFilePath;
            string destinationFilePath = ConfigSettings.DestinationFilePath + path;
            string filePath = path + "\\" + fileName;
            string completeXmlFilePath = sourceFilePath + filePath;

            string FileUpload = @"<Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
                                           <Header>
                                                <FileName xmlns=""http://tempuri.org/"">{0}</FileName>
                                                <Length xmlns=""http://tempuri.org/"">{1}</Length>                                                
                                                <LocationToUpload xmlns=""http://tempuri.org/"">{2}</LocationToUpload>  
                                                <Password xmlns=""http://tempuri.org/"">{3}</Password>
                                                <UserName xmlns=""http://tempuri.org/"">{4}</UserName>
                                           </Header>
                                          <Body>
                                             <RemoteFileInfo xmlns=""http://tempuri.org/"">
                                                <FileByteStream>{5}</FileByteStream>
                                             </RemoteFileInfo>
                                          </Body>
                                         </Envelope>";


            string password = GetEncryptedPassword();

            string stream = Base64Encode(completeXmlFilePath, out fileLength);
            var uploadResponse = AccessWebService("http://tempuri.org/ITransferService/UploadFile", ConfigSettings.WebServiceUrlToUploadFile, String.Format(FileUpload, fileName, fileLength, destinationFilePath, password, MagicStrings.USERNAME, stream));

            return RetrieveErrorMessage(uploadResponse);
        }

        //public static T SOAPToObject<T>(string SOAP)
        //{
        //    if (string.IsNullOrEmpty(SOAP))
        //    {
        //        throw new ArgumentException("SOAP can not be null/empty");
        //    }
        //    try
        //    {
        //        using (MemoryStream Stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(SOAP)))
        //        {
        //            SoapFormatter Formatter = new SoapFormatter();
        //            return (T)Formatter.Deserialize(Stream);
        //        }
        //    }
        //    catch { throw; }
        //}

        private static string RetrieveErrorMessage(string response)
        {
            int indexOfMessage = response.IndexOf("<Message>") + 9;
            string message = response.Substring(indexOfMessage, response.IndexOf("</Message>") - indexOfMessage);
            return message;
        }

        private static string GetEncryptedPassword()
        {
            string cryptKey = ConfigSettings.CryptKey;
            string password = CryptoEngine.Encrypt(MagicStrings.PASSWORD, true, cryptKey);
            return password;
        }

        internal static void SendErrorMail(Exception ex, string section)
        {
            while (ex.InnerException != null) ex = ex.InnerException;

            string mailFrom = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]);
            string mailTo = Convert.ToString(ConfigurationManager.AppSettings["MailTo"]);
            string subject = Convert.ToString(ConfigurationManager.AppSettings["MailSubject"]);
            string mailHost = Convert.ToString(ConfigurationManager.AppSettings["MailHost"]);

            TaskHelper.SendMail("The following error occurred for the section (" + section + ") :" + ex.ToString(), mailFrom, mailTo, subject, mailHost);
        }

        internal static string ResetAppPool()
        {
            string request = @"<Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
                                    <Body>
                                        <Credentials xmlns=""http://tempuri.org/"">
                                            <Password>{0}</Password>
                                            <UserName>{1}</UserName>
                                        </Credentials>
                                    </Body>
                                </Envelope>";
            string response = AccessWebService("http://tempuri.org/IResetAppPoolService/RecycleAppPool", ConfigSettings.WebServiceUrlToRecycleAppPool, String.Format(request, GetEncryptedPassword(), MagicStrings.USERNAME));

            return RetrieveErrorMessage(response);
        }

        public static string AccessWebService(string soapAction, string webServiceUrl, string data)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            client.Headers.Add(HttpRequestHeader.ContentType, @"text/xml; charset=""utf-8""");
            client.Headers.Add("SOAPAction", soapAction);
            string response = client.UploadString(webServiceUrl, data);

            return response;
        }

        private static string Base64Encode(string filename, out long fileLength)
        {
            string encodedData;
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] filebytes = new byte[fs.Length];
                fileLength = fs.Length;
                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
                encodedData = Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);
            }
            return encodedData;
        }
    }
}