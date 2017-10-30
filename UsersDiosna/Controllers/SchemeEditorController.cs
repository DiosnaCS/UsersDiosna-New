using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using UsersDiosna.Handlers;
using UsersDiosna.Sheme.Models;

namespace UsersDiosna.Controllers
{
    public class SchemeEditorController : Controller
    {
        // GET: SchemeEditor
        public ActionResult Index()
        {
            int i = 0;
            List<string> pathesToCfg = new List<string>();
            string pathSvgCfg = null;
            string subGraphicDir = null;
            List<string> pathGraphicCfg = new List<string>();
            foreach (string key in Session.Keys)
            {
                if (key.Contains("pathCfg"))
                {
                    pathesToCfg.Add(Session[key].ToString());
                }
                if (key.Contains("subGraphicDir"))
                {
                   subGraphicDir = Session[key].ToString();
                }
                if (key.Contains("pathGraphicCfg"))
                {
                    pathGraphicCfg.Add(Session[key].ToString());
                }
                if (key.Contains("pathSvgCfg"))
                {
                    pathSvgCfg = Session[key].ToString();
                }
            }

            if (pathGraphicCfg != null)
            {
                SchemeEditorHandler.getGraphicLists(pathSvgCfg, subGraphicDir, pathGraphicCfg);
            }
            else
            {
                Session["tempforview"] = "Config files pathes are not present in config";
            }
            if (pathSvgCfg != null)
            {
                SchemeEditorHandler.getTextlists(pathesToCfg, pathSvgCfg);
            }
            else
            {
                Session["tempforview"] = "Config files pathes are not present in config";
            }
            return View();
        }
    }
}   