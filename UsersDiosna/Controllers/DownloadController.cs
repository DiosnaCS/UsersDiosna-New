using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using System.Web.Security;
using UsersDiosna.Handlers;
using UsersDiosna.Download.Models;
using System.Text.RegularExpressions;

namespace UsersDiosna.Controllers
{
    
    public class DownloadController :Controller
    {
        public static String path = PathDef.PhysicalPath + @"\Config";
        List <String> ProjectNames = new List<string>();
        List<int> ProjectNumbers = new List<int>();
        public string[] absoulte_path;

        /*
         * @param void, @return void
         * Method for download a file
         */
        [Authorize]
        public void downloadFile() {
                WebClient client = new WebClient();
                String absoultePathToFile = null;
                string nameFile = Request.QueryString["nameFile"].ToString();
                //string sessionID = "pathDownload" + Request.QueryString["plc"];
                string networkPath = Session["network_path"].ToString();
                FileHelper FH = new FileHelper();

                //Check the privilage to download from that mask
                bool hasAccess = false;
                string mask = "";
                List<string> masks = FH.selectMasks((int)Session["id"], Roles.GetRolesForUser());
                foreach (string maskFile in masks)
                {
                    mask = maskFile;
                    if (maskFile.Contains('\\'))
                        mask = maskFile.Replace('\\', '/');
                    Regex regex = new Regex('^' + mask.Replace(".", "[.]").Replace("*", ".*").Replace("?", ".") + '$'); //regex of mask
                    if (regex.IsMatch(nameFile)){
                        hasAccess = true;
                    }
                }
                absoultePathToFile = networkPath+nameFile;

                Response.ContentType = "application/octet-stream";

            if (hasAccess == true)
            {
                if (networkPath.Contains("/9_Public/"))
                {
                    nameFile = nameFile.Substring(nameFile.LastIndexOf('/') + 1);
                }
                if (absoultePathToFile == null)
                {
                    Session["tempforview"] = "Error: Your file has been not found";
                }
                else
                {

                    if (Request.QueryString["View"] != null)
                    {
                        if (absoultePathToFile.Contains(".pdf"))
                        {
                            Response.ContentType = "application/pdf"; //change content type for pdf files
                        }
                        if (absoultePathToFile.Contains(".txt"))
                        {
                            Response.ContentType = "text/plain"; //change content type for txt files
                        }
                        if (absoultePathToFile.Contains(".html"))
                        {
                            Response.ContentType = "text/html"; //change content type for html files
                        }
                        if (absoultePathToFile.Contains(".mp4"))
                        {
                            Response.ContentType = "video/mp4"; //change content type for mp4 files
                        }

                        //Response.TransmitFile(absoultePathToFile);
                        Response.BinaryWrite(FH.DownloadFile(networkPath, nameFile));//For View the file
                    }
                    else
                    {
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + nameFile);
                        //Response.TransmitFile(absoultePathToFile);
                        Response.BinaryWrite(FH.DownloadFile(networkPath, nameFile)); //For download file
                        Response.Flush(); //For download file
                    }
                }
            }
            else
            {
                Session["tempforview"] = "Error: You dont have a permission to download this file";
            }
        }

        [Authorize(Roles = "Download")]
        public ActionResult Index()
        {
            try {
                    string sessionID = "pathDownload" + Request.QueryString["plc"];
                    string network_path = Session[sessionID].ToString();
                    Session["network_path"] = network_path;
                    
                    List <string> filesToView = new List<string>();
                    int i = 0;
                    List<string> masks = new List<string>();
                    List<string> masksNames = new List<string>();

                    List<FileForDownload> model = new List<FileForDownload>();
                    FileHelper FH = new FileHelper();
                    masksNames = FH.selectMasksNames((int)Session["id"]);
                    masks = FH.selectMasks((int)Session["id"],Roles.GetRolesForUser());
                    foreach (string mask in masks)
                    {
                        FileForDownload FFD = new FileForDownload();
                        if (masksNames[i] != "")
                        {
                            FFD.maskName = masksNames[i];
                        }
                        FFD.pathes = FH.findFilesOnServer(network_path, mask);
                        FFD.files = new List<string>();
                        foreach (string path in FFD.pathes) {
                            string fileName = path.Substring(path.LastIndexOf('/')+1);
                            FFD.files.Add(fileName);
                        }
                        if (FFD.files == null)
                        {
                            ViewBag.warning = "No files has been found";
                            filesToView.Add("No files has been found");
                        }
                        i++;
                        model.Add(FFD);
                    }
                    return View(model);
            }
            catch {
                ViewBag.message = "Problem with finding downloads to this bakery.";
                return View();
            }
}
    }
}