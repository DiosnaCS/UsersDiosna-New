using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace UsersDiosna.Handlers
{
    public class FileHelper
    {
        public byte[] DownloadFile(string relativePath, string fileName, string downloadPath = "")
        {
            try
            {
                if (relativePath.StartsWith(@"\"))
                    relativePath.Substring(1);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://remote.diosna.cz/" + relativePath + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential("UsersDiosna", "Nordit0276", "FILESERVER3");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                response.Close();
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                return ms.ToArray();
            }
            catch (Exception e) {
                Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                return null;
            }
            
        }

        public bool UploadFile(string relativePath, string pathToLocalFile, string fileName)
        {
            try
            {
                if (relativePath.StartsWith(@"\"))
                    relativePath.Substring(1);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://remote.diosna.cz/" + relativePath + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential("UsersDiosna", "Nordit0276", "FILESERVER3");
                StreamReader sourceStream = new StreamReader(pathToLocalFile);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
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