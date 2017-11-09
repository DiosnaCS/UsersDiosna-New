using Svg;
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
        private SvgDocument svg { get; set; }
        // GET: SchemeEditor
        public ActionResult Index()
        {
            List<string> valuesforView = new List<string>();
            List<string> pathesToCfg = new List<string>();
            string pathSvgCfg = null;
            string dynValuesCfg = null;
            string ageBarsCfgPath = null;
            string pathToSvg = null;
            List<string> subGraphicDir = new List<string>();
            List<string> pathGraphicCfg = new List<string>();
            List<AgeBar> ageBarList = new List<AgeBar>();
            List<DynValue> values = new List<DynValue>();
            foreach (string key in Session.Keys)
            {
                if (key.Contains("pathScheme"))
                {
                    pathToSvg = Session[key].ToString();
                }
                if (key.Contains("pathCfg"))
                {
                    pathesToCfg.Add(Session[key].ToString());
                }
                if (key.Contains("dynValuesCfg"))
                {
                    dynValuesCfg = Session[key].ToString();
                }
                if (key.Contains("ageBarsCfgPath"))
                {
                    ageBarsCfgPath = Session[key].ToString();
                }
                if (key.Contains("subGraphicDir"))
                {
                   subGraphicDir.Add(Session[key].ToString());
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
            SchemeEditor model = new SchemeEditor();
            if (pathToSvg != null)
            {
                pathToSvg = pathToSvg.Replace(@"\", @"/");
                svg = SvgDocument.Open(Path.PhysicalPath + pathToSvg);

                
                model.relativePath = pathToSvg;
                model.SvgFile = svg;
            }
            else
            {
                Session["tempforview"] = "Problem with finding this svg path";
                return RedirectToAction("Index", "Menu");
            }
            if (dynValuesCfg != null || ageBarsCfgPath != null || pathGraphicCfg != null)
            {
                if (dynValuesCfg != null)
                {
                    SchemeEditorHandler.getDynValues(pathSvgCfg, dynValuesCfg, values);
                    foreach (DynValue value in values)
                    {
                        string valueToView = "";
                        valueToView += value.id + " \t" + value.column + " " + value.table;
                        valuesforView.Add(valueToView);
                    }
                }
                // Important ageBar age is not included in agegBar config 
                if (ageBarsCfgPath != null)
                {
                    SchemeEditorHandler.getAgeBar(pathSvgCfg, ageBarsCfgPath, ageBarList);
                }
                else
                {
                    Session["tempforview"] = "Config files pathes are not present in bakery config";
                }

                if (pathGraphicCfg != null && subGraphicDir != null && pathGraphicCfg != null)
                {
                    SchemeEditorHandler.getGraphicLists(pathSvgCfg, subGraphicDir, pathGraphicCfg);
                }
                else
                {
                    Session["tempforview"] = "Config files pathes are not present in bakery config";
                }

                if (pathSvgCfg != null && pathesToCfg != null)
                {
                    SchemeEditorHandler.getTextlists(pathesToCfg, pathSvgCfg);
                }
                else
                {
                    Session["tempforview"] = "Config files pathes are not present in bakery config";
                }
                return View(model);
            }
            else
            {
                return View("form", model);
            }
        }
    }
}   