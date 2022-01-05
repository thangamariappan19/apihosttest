using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyBizErrorLogger
{
    public class TextFileLogger : BaseLogger
    {
        private bool IsFolderReadOnly(string Folder)
        {
            System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo(Folder);
            return ((oDir.Attributes & System.IO.FileAttributes.ReadOnly) > 0);
        }
        private void GrantAccess(string fullPath)
        {
            try
            {
                if (IsFolderReadOnly(fullPath))
                {
                    DirectoryInfo dInfo = new DirectoryInfo(fullPath);
                    DirectorySecurity dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public override LogResponse WriteToLog(LogRequest LogRequestObj)
        {
            LogResponse retObj = new LogResponse();
            try
            {
                //get the path where the error logs are written
                string LogFilePath = Application.StartupPath + "\\LogFiles\\"; //ConfigurationManager.AppSettings["LogFilesPath"];

                if(!Directory.Exists(LogFilePath))
                {
                    Directory.CreateDirectory(LogFilePath);
                }

                var Source = RemoveSpecialCharacters(LogRequestObj.Source);
                

                string LogFileName = DateTime.Now.ToString("dd-MM-yyyy-HHmm-ffffff f") + ".txt";

                //GrantAccess(LogFilePath);

                using (StreamWriter outFile = new StreamWriter(LogFilePath + "\\" + LogFileName))
                {
                    outFile.Write(LogRequestObj.Message);
                }

                retObj.StatusCode = LoggerEnums.LogOpStatusCode.Success;
            }
            catch (Exception ex)
            {
                retObj.StatusCode = LoggerEnums.LogOpStatusCode.Failure;
                retObj.DisplayMessage = "Error logging to text file.";
                retObj.ExceptionMessage = ex.Message;
                retObj.StackTrace = ex.StackTrace;
            }
            return retObj;
        }
        public override LogResponse WriteToSyncLog(LogRequest LogRequestObj)
        {
            LogResponse retObj = new LogResponse();
            try
            {
                //get the path where the error logs are written
                string LogFilePath = Application.StartupPath + "\\LogFiles\\"; //ConfigurationManager.AppSettings["LogFilesPath"];

                if (!Directory.Exists(LogFilePath))
                {
                    Directory.CreateDirectory(LogFilePath);
                }

                var Source = RemoveSpecialCharacters(LogRequestObj.Source);


                string LogFileName = LogRequestObj.Source + "-" + DateTime.Now.ToString("dd-MM-yyyy-HHmm-ffffff f") + ".txt";

                //GrantAccess(LogFilePath);

                using (StreamWriter outFile = new StreamWriter(LogFilePath + "\\" + LogFileName))
                {
                    outFile.Write(LogRequestObj.Message);
                }

                retObj.StatusCode = LoggerEnums.LogOpStatusCode.Success;
            }
            catch (Exception ex)
            {
                retObj.StatusCode = LoggerEnums.LogOpStatusCode.Failure;
                retObj.DisplayMessage = "Error logging to text file.";
                retObj.ExceptionMessage = ex.Message;
                retObj.StackTrace = ex.StackTrace;
            }
            return retObj;
        }
    }
}
