using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Svg;
using UsersDiosna.Handlers;
using System.Drawing;
using UsersDiosna.Sheme.Models;

namespace UsersDiosna.Controllers
{
    public class SchemeEditorController : Controller
    {
        // GET: SchemeEditor
        public ActionResult Index()
        {
            string pathToSvg = @"\Config\svg\vectorpaint.svg";
            SvgDocument svg = SvgDocument.Open(Path.physicalPath + pathToSvg);
            SchemeEditor model = new SchemeEditor();
            model.relativePath = pathToSvg.Replace(@"\", @"/");
            model.SvgFile = svg;
            return View(model);
        }
    }
}