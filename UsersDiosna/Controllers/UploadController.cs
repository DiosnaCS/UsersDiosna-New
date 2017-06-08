using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UsersDiosna.Upload.Models;
using System.IO;

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
                    string[] files = Directory.GetFiles(ServerPath + @"9_Public\", "*.*");
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
                    return View();
                }
            catch (Exception e)
            {
                ViewBag.message = "Problem with finding uploads to this bakery.";
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
                    if (filename.Contains(@"\"))
                    {
                        int index = model.File.FileName.LastIndexOf(@"\");
                        filename = model.File.FileName.Substring(index + 1);
                    }
                    
                    string network_path = Session["network_path"].ToString();
                    // Use your file here
                    MemoryStream memoryStream = new MemoryStream();
                    //9_Public\ is important becasuse of download the file
                    model.File.SaveAs(network_path + @"9_Public\" + filename);
                    //Message for user
                    Session["success"] = "File: " + filename + " has been successfullly uploaded.";
                } catch(Exception ex)
                {
                    Session["tempforview"] = "An error has occured in file uploading:" + ex;
                }
                
            }

            return RedirectToAction("Index", "Upload");
        }
    }

} 