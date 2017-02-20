using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SharpSvn;
using SharpSvn.Security;
using System.Configuration;
using System.Collections.Generic;

namespace XMLEditor.Code
{
    public class SVNClient
    {
        public static void ExportFile(string source, string target, string SVNUser, string SVNPassword)
        {
            using (SvnClient client = new SvnClient())
            {
                client.LoadConfiguration(Path.Combine(Path.GetTempPath(), "Svn"), true);
                client.Authentication.SslServerTrustHandlers += delegate(object sender, SvnSslServerTrustEventArgs e)
                {
                    e.Cancel = false;
                };
                client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(SVNUser, SVNPassword);
                client.Export(new SvnUriTarget(source), target, new SvnExportArgs());
            }
        }

        public static void CheckOut(string source, string target, string SVNUser, string SVNPassword)
        {
            using (SvnClient client = new SvnClient())
            {
                client.LoadConfiguration(Path.Combine(Path.GetTempPath(), "Svn"), true);
                client.Authentication.SslServerTrustHandlers += delegate(object sender, SvnSslServerTrustEventArgs e)
                {
                    e.Cancel = false;
                };
                client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(SVNUser, SVNPassword);
                client.CheckOut(new SvnUriTarget(source), target);
            }
        }

        public static void Update(string target, string SVNUser, string SVNPassword)
        {
            SvnClient client = InitializeSvnClient(SVNUser, SVNPassword);
            bool success = client.Update(target);
        }

        private static SvnClient InitializeSvnClient(string SVNUser, string SVNPassword)
        {
            SvnClient client = new SvnClient();

            client.LoadConfiguration(Path.Combine(Path.GetTempPath(), "Svn"), true);
            client.Authentication.SslServerTrustHandlers += delegate(object sender, SvnSslServerTrustEventArgs e)
            {
                e.Cancel = false;
            };
            client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(SVNUser, SVNPassword);


            return client;

        }

        public static long Commit(string source, string SVNUser, string SVNPassword, string comment)
        {
            using (SvnClient client = InitializeSvnClient(SVNUser, SVNPassword))
            {
                return CommitNGetRevisionNumber(source, comment, client);
            }
        }

        private static long CommitNGetRevisionNumber(string source, string comment, SvnClient client)
        {
            SvnCommitArgs ca = new SvnCommitArgs();
            ca.LogMessage = comment;
            SvnCommitResult commitResults;
            long revision;

            bool success = client.Commit(source, ca, out commitResults);

            if (ReferenceEquals(commitResults, null))
            {
                SvnInfoEventArgs svnInfo = GetLatestRevisionInfo(client, client.GetRepositoryRoot(source).AbsoluteUri);
                revision = svnInfo.LastChangeRevision;
            }
            else
            {
                revision = commitResults.Revision;
            }

            return revision;
        }

        public static void Merge(string targetPath, string svnSourceURL, string svnUsername, string svnPassword, long revisionNumber)
        {
            using (SvnClient client = InitializeSvnClient(svnUsername, svnPassword))
            {
                SvnMergeArgs mergeArgs = new SvnMergeArgs();
                mergeArgs.Depth = SvnDepth.Infinity;
                
                SvnUriTarget svnTargetSource = new SvnUriTarget(new Uri(svnSourceURL));                
                SvnRevisionRange svnRange = new SvnRevisionRange(revisionNumber - 1, revisionNumber);
                bool success = client.Merge(targetPath, svnTargetSource, svnRange, mergeArgs);
            }
        }

        public static SvnInfoEventArgs GetLatestRevisionInfo(SvnClient client, string svnSourceURL)
        {
            SvnInfoEventArgs svnInfo;
            Uri repos = new Uri(svnSourceURL);
            client.GetInfo(repos, out svnInfo);

            return svnInfo;
        }

        public static long AddFile(ICollection<string> fileNames, string source, string svnUsername, string svnPassword, string comment)
        {
            using (SvnClient client = InitializeSvnClient(svnUsername, svnPassword))
            {
                foreach (string filename in fileNames)
                {
                    client.Add(filename);
                }

                return CommitNGetRevisionNumber(source, comment, client);
            }
        }
    }
}