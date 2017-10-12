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
    [Authorize(Roles = "Download,Admin")]
    public class DownloadController :Controller
    {
        public static String path = Path.PhysicalPath + @"\Config";
        //public static string network_path = @"\\192.168.2.20\Public\0\Marek\db\";
        public static string local_path = @"C:\0\00\db\";
        List <String> ProjectNames = new List<string>();
        List<int> ProjectNumbers = new List<int>();
        public string[] absoulte_path;

        /*
         * @param void, @return void
         * Method for download a file
         */
        public void downloadFile() {
                WebClient client = new WebClient();
                String absoultePathToFile = null;
                string nameFile = Request.QueryString["nameFile"].ToString();
                //string sessionID = "pathDownload" + Request.QueryString["plc"];
                string network_path = Session["network_path"].ToString();
                FileHelper FH = new FileHelper();

                //Check the privilage to download from that mask
                bool hasAccess = false; 
                List<string> masks = FH.selectMasks((int)Session["id"], Roles.GetRolesForUser());
                foreach (string mask in masks)
                {
                    Regex regex = new Regex('^' + mask.Replace(".", "[.]").Replace("*", ".*").Replace("?", ".") + '$'); //regex of mask
                    if (regex.IsMatch(nameFile)){
                        hasAccess = true;
                    }
                }
                absoultePathToFile = network_path+nameFile;

                Response.ContentType = "application/octet-stream";

            if (hasAccess == true)
            {
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
                        Response.BinaryWrite(FH.DownloadFile(network_path, nameFile));//For View the file
                    }
                    else
                    {
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + nameFile);
                        //Response.TransmitFile(absoultePathToFile);
                        Response.BinaryWrite(FH.DownloadFile(network_path, nameFile)); //For download file
                        Response.Flush(); //For download file
                    }
                }
            }
            else
            {
                Session["tempforview"] = "Error: You dont hsave a permission to download this file";
            }
        }

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