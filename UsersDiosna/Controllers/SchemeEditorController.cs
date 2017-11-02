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
            string dynValuesCfg = null;
            string ageBarsCfgPath = null;
            List<string> subGraphicDir = new List<string>();
            List<string> pathGraphicCfg = new List<string>();
            foreach (string key in Session.Keys)
            {
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

            if (dynValuesCfg != null)
            {
                var lines = System.IO.File.ReadAllLines(dynValuesCfg).Select(line => line.Split(new char[] { '\t' }));
                List<string[]> dynValueList = lines.Where(line => line.Length != 0).ToList();
                List<DynValue> values = new List<DynValue>();
                foreach (string[] dynValue in dynValueList)
                {
                    DynValue value = new DynValue();

                    value.index = int.Parse(dynValue[0]);
                    value.table = dynValue[1];
                    value.column = dynValue[2];
                    value.ratio = int.Parse(dynValue[3]);
                    value.offset = int.Parse(dynValue[4]);
                    value.unit = dynValue[5];
                    value.textColor = dynValue[6];

                    values.Add(value);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(List<DynValue>));
                using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
                {
                    serializer.Serialize(writer, values);
                }
            }
            // Important ageBar age is not included in agegBar config 
            if (ageBarsCfgPath != null)
            {
                var lines = System.IO.File.ReadAllLines(ageBarsCfgPath).Select(line => line.Split(new char[] { '\t' }));
                List<string[]> ageBars = lines.Where(line => line.Length != 0).ToList();
                List<AgeBar> ageBarList = new List<AgeBar>();
                foreach (string[] ageBar in ageBars)
                {
                    AgeBar AB = new AgeBar();

                    AB.maxAge = int.Parse(ageBar[0]);
                    AB.firstColor = ageBar[1];
                    AB.secondColor = ageBar[2];
                    AB.thirdColor = ageBar[3];

                    ageBarList.Add(AB);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(List<AgeBar>));
                using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
                {
                    serializer.Serialize(writer, ageBarList);
                }
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

            return View();
        }
    }
}   