using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
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
            string bindingTagsCfg = null;
            List<string> subGraphicDir = new List<string>();
            List<string> pathGraphicCfg = new List<string>();
            List<AgeBar> ageBarList = new List<AgeBar>();
            List<DynValue> values = new List<DynValue>();
            List<Textlist> textLists = new List<Textlist>();
            List<Graphiclist> graphicLists = new List<Graphiclist>();
            List<SchemeValue> bindingTagList = new List<SchemeValue>();

            getConfigPathes(pathesToCfg, ref pathSvgCfg, ref dynValuesCfg, ref bindingTagsCfg, ref ageBarsCfgPath, ref pathToSvg, subGraphicDir, pathGraphicCfg);

            SchemeEditor model = new SchemeEditor();
            if (dynValuesCfg != null || ageBarsCfgPath != null || pathGraphicCfg != null)
            {
                System.IO.File.Delete(pathSvgCfg);
                if (dynValuesCfg != null)
                {
                    SchemeEditorHandler.getDynValues(pathSvgCfg, dynValuesCfg, values);
                    model.SchemeTags = values;
                }
                if (bindingTagsCfg != null)
                {
                    SchemeEditorHandler.getBindingTags(pathSvgCfg, bindingTagsCfg, bindingTagList);
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
                if (pathToSvg != null)
                {
                    model.relativePath = pathToSvg;
                    model.SvgFile = svg;
                    //pathToSvg = pathToSvg.Replace(@"\", @"/");
                    if (pathToSvg.IndexOf(@"\") == 0)
                    {
                        pathToSvg = pathToSvg.Substring(1);
                    }
                    //write whole SchemeEditor model to xml
                    SchemeEditorHandler.writeToXML(pathSvgCfg, model);
                    svg = SvgDocument.Open(Path.PhysicalPath + pathToSvg);

                    string SvgXml = svg.GetXML();
                    string ConfigXml = System.IO.File.ReadAllText(pathSvgCfg);
                    
                    SvgDocument newSvg = new SvgDocument();
                    string secondSvgPart = SvgXml.Substring((SvgXml.IndexOf("<defs>") + "defs".Length));
                    string firstSvgPart = SvgXml.Substring(0, SvgXml.IndexOf("<defs>"));
                    string svgFileContent = firstSvgPart + "<config>" + ConfigXml + "</config>" + secondSvgPart;
                    System.IO.File.WriteAllText(Path.PhysicalPath + pathToSvg, svgFileContent);

                }
                else
                {
                    //write whole SchemeEditor model to xml
                    SchemeEditorHandler.writeToXML(pathSvgCfg, model);
                    Session["tempforview"] = "Problem with finding this svg";
                    return RedirectToAction("Index", "Menu");
                }
                
                return View(model);
            }
            else
            {
                return View("form", model);
            }
        }

        private void getConfigPathes(List<string> pathesToCfg, ref string pathSvgCfg, ref string dynValuesCfg, ref string bindingTagsCfg,  ref string ageBarsCfgPath, ref string pathToSvg, List<string> subGraphicDir, List<string> pathGraphicCfg)
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
                if (key.Contains("bindingTagsCfg"))
                {
                    bindingTagsCfg = Session[key].ToString();
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