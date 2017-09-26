using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using UsersDiosna;

namespace UsersDiosna
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            if (done == false)
            {
                SessionIDManager manager = new SessionIDManager();
                string newID = manager.CreateSessionID(Context);
                bool redirected = false;
                bool isAdded = false;
                manager.SaveSessionID(Context, newID, out redirected, out isAdded);
                done = true;
            }
            string sessionId = Session.SessionID;
            if (Context.Session != null)
            {
                if (Context.Session.IsNewSession)
                {
                    if (HttpContext.Current.Session.Count == 0)
                    {
                        HttpContext.Current.Response.Redirect("~/Account/Login/");
                        //UsersDiosna.Error.toFile("Session_Start hapened", this.GetType().Name.ToString());
                    }
                }
            }
        }

        protected void Session_OnEnd()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session == null)
                {
                    HttpContext.Current.Response.Redirect("~/Account/Login/");
                    UsersDiosna.Error.toFile("Session_onEnd hapened", this.GetType().Name.ToString());
                }
            }
            else {
                Server.ClearError();
                UsersDiosna.Error.toFile("Session_onEnd hapened with null current context", this.GetType().Name.ToString());
                try
                {
                    Process.Start(@"C:\WINDOWS\system32\iisreset.exe", "/force");
                    UsersDiosna.Error.toFile("Proccess started", this.GetType().Name.ToString());
                } catch (Exception e) {
                    UsersDiosna.Error.toFile("Process start error" + e.Message.ToString(), this.GetType().Name.ToString());
                }
            }
        }

        public static int ErrorId { get; set; }
        public static bool done { get; private set; }
        //TODO prepare some kind of my own errors with to log file mmethods - use Application_Error() method for that

        /// <summary>
        /// Some sort of try to catch all errors that wasnt cought
        /// </summary>
        /// <param name="e">Exception that was made and i hope it will work</param>
        protected void Application_Error()
        {
            Exception e =Server.GetLastError();
            string PathToErrorFile = UsersDiosna.Error.PathToErrorFile;
            DateTime now = DateTime.Now;
            ErrorId++;
            string timestamp = "\r\n" + now.ToString();
            if (PathToErrorFile != null)
            {
                System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                System.IO.File.AppendAllText(PathToErrorFile, ErrorId.ToString()); //set id  of Error
                System.IO.File.AppendAllText(PathToErrorFile, e.Message.ToString());
                System.IO.File.AppendAllText(PathToErrorFile, e.StackTrace.ToString()); //Write Error to file
                Session["tempforview"] = timestamp + "    Error Id:" + ErrorId.ToString() + " occured so please try it again after some time"; //To screen also with id 
            }
            else
            {
                if (Directory.Exists(Path.PhysicalPath + @"\ErroLog") == true &&
                    Directory.GetDirectories(Path.PhysicalPath, e.Source.ToString()) != null)
                {
                    PathToErrorFile = Path.PhysicalPath + @"\ErrorLog\" + e.Source.ToString() + @"\log.txt";
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                    System.IO.File.AppendAllText(PathToErrorFile, ErrorId.ToString()); //set id  of Error
                    System.IO.File.AppendAllText(PathToErrorFile, e.Message.ToString()); //Write Error to file
                    System.IO.File.AppendAllText(PathToErrorFile, e.StackTrace.ToString()); //Write Error to file
                    Session["tempforview"] = timestamp + "    Error Id:" + ErrorId.ToString() + " occured so please try it again after some time";//To screen also with id 
                }
                else
                {
                    Directory.CreateDirectory(Path.PhysicalPath + @"\ErrorLog\" + e.Source.ToString());//If directory in the path does not exist create one 
                    PathToErrorFile = Path.PhysicalPath + @"\ErrorLog\" + e.Source.ToString() + @"\log.txt"; //Asign path to Path attribute
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                    System.IO.File.AppendAllText(PathToErrorFile, ErrorId.ToString()); //set id  of Error
                    System.IO.File.AppendAllText(PathToErrorFile, e.Message.ToString()); //Write Error to file
                    System.IO.File.AppendAllText(PathToErrorFile, e.StackTrace.ToString()); //Write Error to file
                    Session["tempforview"] = timestamp + "    Error Id:" + ErrorId.ToString() + " occured so please try it again after some time"; //To screen also with id 
                }
            }
            Server.ClearError();
        }
    }
}
