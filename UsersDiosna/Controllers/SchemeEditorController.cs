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
            List<Textlist> textLists = new List<Textlist>();
            List<Graphiclist> graphicLists = new List<Graphiclist>();

            getConfigPathes(pathesToCfg, ref pathSvgCfg, ref dynValuesCfg, ref ageBarsCfgPath, ref pathToSvg, subGraphicDir, pathGraphicCfg);

            SchemeEditor model = new SchemeEditor();
            if (pathToSvg != null)
            {
                //pathToSvg = pathToSvg.Replace(@"\", @"/");
                if (pathToSvg.IndexOf(@"\") == 0)
                {
                    pathToSvg = pathToSvg.Substring(1);
                }
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
                    model.SchemeTags = values;
                }
                // Important ageBar age is not included in agegBar config 
                if (ageBarsCfgPath != null)
                {
                    SchemeEditorHandler.getAgeBar(pathSvgCfg, ageBarsCfgPath, ageBarList);
                    model.SchemeAgeBars = ageBarList;
                }
                else
                {
                    Session["tempforview"] = "Config files pathes are not present in bakery config";
                }

                if (pathGraphicCfg != null && subGraphicDir != null && pathGraphicCfg != null)
                {

                    SchemeEditorHandler.getGraphicLists(pathSvgCfg, subGraphicDir, pathGraphicCfg, graphicLists);
                    model.SchemeGraphicsList = graphicLists;
                }
                else
                {
                    Session["tempforview"] = "Config files pathes are not present in bakery config";
                }

                if (pathSvgCfg != null && pathesToCfg != null)
                {
                    SchemeEditorHandler.getTextlists(pathesToCfg, pathSvgCfg, textLists);
                    model.SchemeTextlist = textLists;
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

        private void getConfigPathes(List<string> pathesToCfg, ref string pathSvgCfg, ref string dynValuesCfg, ref string ageBarsCfgPath, ref string pathToSvg, List<string> subGraphicDir, List<string> pathGraphicCfg)
        {
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
        }
    }
}   