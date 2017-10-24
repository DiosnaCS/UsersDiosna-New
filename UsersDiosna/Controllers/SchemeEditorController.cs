using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using UsersDiosna.Sheme.Models;

namespace UsersDiosna.Controllers
{
    public class SchemeEditorController : Controller
    {
        // GET: SchemeEditor
        public ActionResult Index()
        {
            List<string> pathesToCfg = new List<string>();
            string pathSvgCfg = null;
            foreach (string key in Session.Keys)
            {
                if (key.Contains("pathCfg"))
                {
                    pathesToCfg.Add(Session[key].ToString());
                }
                if (key.Contains("pathSvgCfg"))
                {
                    pathSvgCfg = Session[key].ToString();
                }
            }

            /*if (System.IO.File.ReadAllLines(pathToCfg).Length != 0)
            {
                string[] lines = System.IO.File.ReadAllLines(pathToCfg);
                foreach(string line in lines)
                {
                    string[] splittedArray = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }*/
            foreach (string path in pathesToCfg)
            {
                var file = System.IO.File.ReadAllLines(path).Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
                var textlistItems = file.Where(line => line.Length != 0).ToList();
                Textlist textlist = new Textlist();
                textlist.values = new List<TextlistItem>();
                foreach(var item in textlistItems)
                {
                    TextlistItem textlistItem = new TextlistItem();
                    textlistItem.index = int.Parse(item[0]);
                    textlistItem.value = item[1];
                    textlistItem.bgColor = ColorTranslator.ToHtml(Color.White);
                    textlistItem.textColor = ColorTranslator.ToHtml(Color.Black);
                    textlist.values.Add(textlistItem);
                }
                string textlistName = path.Substring(path.LastIndexOf("\\")+1, path.LastIndexOf(".") - path.LastIndexOf("\\"));
                textlist.name = textlistName;
                XmlSerializer serializer = new XmlSerializer(typeof(Textlist));
                using (TextWriter writer = new StreamWriter(pathSvgCfg))
                {
                    serializer.Serialize(writer, textlist);
                }
            }
            return View();
        }
    }
}  