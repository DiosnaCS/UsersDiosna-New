using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
            if (Context.Session != null)
            {
                if (Context.Session.IsNewSession)
                {
                    if (HttpContext.Current.Session.Count == 0)
                    {
                        Response.Redirect("~/Account/Login/");
                    }
                }
            }
        }

        protected void Session_OnEnd()
        {
            if (Context.Session != null)
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    Response.Redirect("~/Account/Login/");
                }
            }
        }

        public static int ErrorId { get; private set; }

        //TODO prepare some kind of my own errors with to log file mmethods - use Application_Error() method for that

        /// <summary>
        /// Some sort of try to catch all errors that wasnt cought
        /// </summary>
        /// <param name="e">Exception that was made and i hope it will work</param>
        protected void Application_Error(Exception e)
        {
            string PathToErrorFile = UsersDiosna.Error.PathToErrorFile;
            DateTime now = DateTime.Now;
            int id = ErrorId++;
            string timestamp = "\r\n" + now.ToString();
            if (PathToErrorFile != null)
            {
                System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                System.IO.File.AppendAllText(PathToErrorFile, id.ToString()); //set id  of Error
                System.IO.File.AppendAllText(PathToErrorFile, e.Message.ToString());
                Session["tempforview"] = timestamp + "    Error " + id.ToString() + " occured so please try it again after some time"; //To screen also with id 
            }
            else
            {
                if (Directory.Exists(Path.physicalPath + @"\ErroLog") == true &&
                    Directory.GetDirectories(Path.physicalPath, e.Source.ToString()) != null)
                {
                    PathToErrorFile = Path.physicalPath + @"\ErrorLog\" + e.Source.ToString() + @"\log.txt";
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                    System.IO.File.AppendAllText(PathToErrorFile, id.ToString()); //set id  of Error
                    System.IO.File.AppendAllText(PathToErrorFile, e.Message.ToString()); //Write Error to file
                    Session["tempforview"] = timestamp + "    Error " + id.ToString() + " occured so please try it again after some time";//To screen also with id 
                }
                else
                {
                    Directory.CreateDirectory(Path.physicalPath + @"\ErrorLog\" + e.Source.ToString());//If directory in the path does not exist create one 
                    PathToErrorFile = Path.physicalPath + @"\ErrorLog\" + e.Source.ToString() + @"\log.txt"; //Asign path to Path attribute
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                    System.IO.File.AppendAllText(PathToErrorFile, id.ToString()); //set id  of Error
                    System.IO.File.AppendAllText(PathToErrorFile, e.Message.ToString()); //Write Error to file
                    Session["tempforview"] = timestamp + "    Error " + id.ToString() + " occured so please try it again after some time"; //To screen also with id 
                }
            }
        }
    }
}
