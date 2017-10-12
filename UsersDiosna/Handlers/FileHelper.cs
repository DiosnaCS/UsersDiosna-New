using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using UsersDiosna.Controllers;

namespace UsersDiosna.Handlers
{
    public class FileHelper
    {
        /// <summary>
        /// Method to select all mask for current bakery
        /// </summary>
        /// <returns>List<string> masks</returns>
        public List<string> selectMasks(int id, string[] roles)
        {
            db db = new db();
            string mask;
            List<string> masks = new List<string>();
            string someName = "maskRole='" + roles[0] + "'";
            for (int i = 1; i <= (roles.Count() - 1); i++)
            {
                someName += " OR maskRole='" + roles[i] + "'";
            }
            string where = "bakeryId=" + id + " AND (" + someName + ")";
            List<object> objectList = db.multipleItemSelect("maskFile", "mask_directory", where);
            foreach (object o in objectList)
            {
                mask = o.ToString();
                masks.Add(mask);
            }
            return masks;
        }
        /// <summary>
        /// method to select roles for all masks
        /// </summary>
        /// <param>bakery id</param>
        /// <returns>List<string> maskRoles</returns>
        public List<string> selectMasksRoles(int id)
        {
            db db = new db();
            string maskRole;
            List<string> masksRoles = new List<string>();
            List<object> objectList = db.multipleItemSelect("maskRole", "mask_directory", "bakeryId='" + id + "'");
            foreach (object o in objectList)
            {
                maskRole = o.ToString();
                masksRoles.Add(maskRole);
            }
            return masksRoles;
        }
        /// <summary>
        /// Method to select all masks names
        /// </summary>
        /// <param>bakery id</param>
        /// <returns>List<string> masksNames</returns>
        public List<string> selectMasksNames(int id)
        {
            db db = new db();
            string maskName;
            List<string> masksNames = new List<string>();
            List<object> objectList = db.multipleItemSelect("maskName", "mask_directory", "bakeryId='" + id + "'");
            foreach (object o in objectList)
            {
                maskName = o.ToString();
                masksNames.Add(maskName);
            }
            return masksNames;
        }
 
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
                    directoryMask = "/" + maskFile.Substring(0, maskFile.LastIndexOf('/') + 1);

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