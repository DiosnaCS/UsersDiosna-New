using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace UsersDiosna.Handlers
{
    public class FileHelper
    {
        public byte[] DownloadFile(string relativePath, string fileName, string downloadPath = "")
        {
            try
            {
                if (relativePath.StartsWith(@"/"))
                    relativePath.Substring(1);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://remote.diosna.cz/" + relativePath + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential("UsersDiosna", "Nordit0276");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                response.Close();
                return ms.ToArray();
            }
            catch (Exception e) {
            Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                return null;
            }
            
        }

        /*
         * @param Stirng NetworkPath, string maskfile = null, String extension = "*", @return string[] absoulte_path
         * Method to get all files from server with exact params as mask of the file
         */
        public List<string> findFilesOnServer(string networkPath, string maskFile = null)
        {
            string directoryMask = "";
            if (maskFile.Contains('\\'))
                maskFile = maskFile.Replace('\\', '/');
            if (maskFile.StartsWith("/")) {
                if (maskFile.Count(f => f == '/') >= 2) {
                    directoryMask = maskFile.Substring(maskFile.IndexOf('/'), maskFile.LastIndexOf('/')+1);
                }
            } else {
                if (maskFile.Count( f => f == '/') >= 1) {
                    directoryMask = "/" + maskFile.Substring(maskFile.IndexOf('/'), maskFile.LastIndexOf('/') + 1);

                }
            }
            
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://remote.diosna.cz/" + networkPath + directoryMask);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.KeepAlive = false;
            request.UsePassive = true;
            request.UseBinary = false;
            request.Credentials = new NetworkCredential("UsersDiosna", "Nordit0276", "FILESERVER3");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string files = reader.ReadToEnd();

            List<string> fileList = new List<string>();
            List<string> resultFileList = new List<string>();
            Extension.SplitToList(out fileList, files, "\r\n");
            Regex mask = new Regex('^' + maskFile.Replace(".", "[.]").Replace("*", ".*").Replace("?", ".") + '$'); //regex of mask
            foreach (string fileName in fileList) {
                string filePath = directoryMask + fileName;
                if (mask.IsMatch(filePath))
                {
                    resultFileList.Add(filePath);
                }
            }
            return resultFileList;
        }

        public bool UploadFile(string relativePath, byte[] data, string fileName)
        {
            try
            {
                if (relativePath.StartsWith(@"/"))
                    relativePath.Substring(1);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://remote.diosna.cz/" + relativePath + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.KeepAlive = false;
                request.UsePassive = true;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential("UsersDiosna", "Nordit0276", "FILESERVER3");
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();      
            }
            catch (Exception e) {
                Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                return false;
            }
            return true;
        }
    }
}