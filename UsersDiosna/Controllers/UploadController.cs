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
                for(int i= 0;i < fileList.Count;i++)
                {
                    fileList[i] = "/9_Public/" + fileList[i];
                }
                ViewBag.fileList = fileList;
                return View();
                }
            catch (Exception e)
            {
                ViewBag.message = "Problem with finding uploads to this bakery or you dont have enough permission to see uploads check by admin";
                Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                Session["tempforview"] = Error.timestamp + "   Error " + MvcApplication.ErrorId.ToString() + " occured so please try it again after some time"; //To screen also with id 
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Upload,Admin")]
        public ActionResult UploadFile(UploadFile model)
        {
            
            if (ModelState.IsValid)
            {
                try {
                    string filename = model.File.FileName;
                    Stream uploadFileStream = model.File.InputStream;
                    byte[] data;
                    using (Stream inputStream = model.File.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        data = memoryStream.ToArray();
                    }
                    FileHelper FH = new FileHelper();
                    FH.UploadFile(Session["network_path"].ToString(), data, filename);
                    Session["success"] = "File: " + filename + " has been successfullly uploaded.";
                } catch(Exception ex)
                {
                    Error.toFile(ex.Message.ToString(), this.GetType().Name.ToString());
                    Session["tempforview"] = Error.timestamp + "   Error " + MvcApplication.ErrorId.ToString() + " An error has occured in file uploading"; //To screen also with id 
                    //Session["tempforview"] = "An error has occured in file uploading:" + ex;
                }
                
            }
            string sessionID = "pathUpload" + model.plcName;
            ViewBag.plcName = model.plcName;
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

            return View("Index");
            //return RedirectToAction("Index", "Upload", new { plc=model.plcName});
        }
    }

} 