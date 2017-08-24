using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UsersDiosna.Upload.Models;
using System.IO;
using System.Net;
using SimpleImpersonation;
using UsersDiosna.Handlers;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "Upload,Admin")]
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            try {
                string sessionID = "pathUpload" + Request.QueryString["plc"];
                string ServerPath = Session[sessionID].ToString();
                Session["network_path"] = ServerPath;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://remote.diosna.cz/" + ServerPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential("UsersDiosna", "Nordit0276", "FILESERVER3");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);                
                string files = reader.ReadToEnd();

                List<string> fileList = new List<string>();
                Extension.SplitToList(out fileList, files, "\r\n"); // ViewBag is dynamic object 
                ViewBag.fileList = fileList;

                /*
                files = Directory.GetFiles(ServerPath + @"9_Public\", "*.*");
                List<string> fileList = new List<string>();
                    List<string> fileNames = new List<string>();
                    int index;
                    string help_string_file;
                    string help_string_file_path;
                    foreach (string file in files) {
                        index = file.LastIndexOf(@"\");
                        help_string_file = file.Substring(index+1);
                        help_string_file_path = @"9_public\" + help_string_file;
                        fileList.Add(help_string_file);
                        fileNames.Add(help_string_file_path);
                    }
                    if (Session["tempforview"] != null)
                    {
                        ViewBag.message = Session["success"];
                        Session["tempforview"] = null;
                    }
                    ViewBag.fileName = fileList;
                    ViewBag.files = fileNames;
                */
                return View();
                }
            catch (Exception e)
            {
                ViewBag.message = "Problem with finding uploads to this bakery.";
                Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                Session["tempforview"] = Error.timestamp + "   Error " + Error.id.ToString() + " occured so please try it again after some time"; //To screen also with id 
                return View();
            }
        }

        [HttpPost]
        public ActionResult UploadFile(UploadFile model)
        {
            
            if (ModelState.IsValid)
            {
                try {
                    string filename = model.File.FileName;
                    string pathToLocalFile = System.IO.Path.GetFullPath(filename);
                    if (filename.Contains(@"\"))
                        filename.Substring(1);
                    FileHelper FH = new FileHelper();
                    FH.UploadFile(Session["network_path"].ToString(),pathToLocalFile, filename);
                    Session["success"] = "File: " + filename + " has been successfullly uploaded.";
                } catch(Exception ex)
                {
                    Error.toFile(ex.Message.ToString(), this.GetType().Name.ToString());
                    Session["tempforview"] = Error.timestamp + "   Error " + Error.id.ToString() + " An error has occured in file uploading"; //To screen also with id 
                    //Session["tempforview"] = "An error has occured in file uploading:" + ex;
                }
                
            }
            string sessionID = "pathUpload" + model.plcName;
            string ServerPath = Session[sessionID].ToString();
            Session["network_path"] = ServerPath;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://remote.diosna.cz/" + ServerPath);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential("UsersDiosna", "Nordit0276", "FILESERVER3");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string files = reader.ReadToEnd();

            List<string> fileList = new List<string>();
            Extension.SplitToList(out fileList, files, "\r\n"); // ViewBag is dynamic object 
            ViewBag.fileList = fileList;

            return View();
            //return RedirectToAction("Index", "Upload", new { plc=model.plcName});
        }
    }

} 