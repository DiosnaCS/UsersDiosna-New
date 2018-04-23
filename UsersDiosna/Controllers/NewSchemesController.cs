using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Svg;
using UsersDiosna.Handlers;
using System.Drawing;
using UsersDiosna.Sheme.Models;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using System.Drawing.Imaging;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles="Admin")]
    public class NewSchemesController : Controller
    {
        private SvgDocument svg { get; set; }
        private void getConfigPath(ref string pathToSvg,ref string pathToCfg)
        {            
            foreach (string key in Session.Keys)
            {
                if (key.Contains("pathScheme"))
                {
                    pathToSvg = Session[key].ToString();
                }
                if (key.Contains("pathSvgCfg"))
                {
                    pathToCfg = Session[key].ToString();
                }
            }
            
        }
        // GET: SvgScheme
        /// <summary>
        /// Action to view new schemes 
        /// </summary>
        /// <returns>View with path to svg in </returns>
        public ActionResult Index() {
            string pathToSvg = null, pathToCfg = null;
            List<SvgElement> elements = new List<SvgElement>();
            NewSchemesHandler schemesHandler = new NewSchemesHandler();

            //first get all needed session data
            getConfigPath(ref pathToSvg, ref pathToCfg);

            //read xml config
            SvgConfig svgConfig = schemesHandler.readSchemeConfig(pathToCfg);
            //get all values for svg scheme
            List<ResponseValue> responses = schemesHandler.readData(svgConfig.BindingTags, (int)Session["id"]); 
            //set values from responses 
            SvgDocument svg = schemesHandler.setValue(responses, svgConfig, pathToSvg);
            /*
            if (svgConfig != null)
                //read all dynamic members 
                elements = schemesHandler.readScheme(pathToSvg, svgConfig);
            */
            if (!pathToSvg.Contains(Path.PhysicalPath))
            {
                pathToSvg = Path.PhysicalPath + pathToSvg;
            }
            string pathToNewSvg = pathToSvg + "_scheme_" + DateTime.Now.Ticks + ".png";
            //System.IO.File.Create(pathToNewSvg);
            /*svg.Write(pathToNewSvg);

            SvgDocument newSvg = SvgDocument.Open(pathToNewSvg);
            */
            svg.
            var bitmap = svg.Draw();

            bitmap.Save(pathToNewSvg, ImageFormat.Png);
            //ViewBag.SvgXml = svg.GetXML();
            return View();
        }
        #region oldCode
        /*public ActionResult Index()
        {
            string pathToSvg = null;
            foreach( string key in Session.Keys)
            {
                if (key.Contains("pathScheme"))
                {
                    pathToSvg = Session[key].ToString();
                }
            }
            //string pathToSvg = @"\Config\svg\scheme.svg";
            if (pathToSvg != null)
            {
                pathToSvg = pathToSvg.Replace(@"\", @"/");
                svg = SvgDocument.Open(Path.PhysicalPath + pathToSvg);

                SchemeEditor model = new SchemeEditor();
                model.relativePath = pathToSvg;
                model.SvgFile = svg;
                return View(model);
            }
            else
            {
                Session["tempforview"] = "Problem with finding this svg path";
                return RedirectToAction("Index", "Menu");
            }
        }
        public async Task<JsonResult> getData()
        {
            StreamReader stream = new StreamReader(Request.InputStream);
            object data = new object();
            List<ResponseValue> responseList = new List<ResponseValue>();
            string json = stream.ReadToEnd();
            if (json != "[]" || json != null || json != "")
            {
                List<SchemeValue> list = new JavaScriptSerializer().Deserialize<List<SchemeValue>>(json);
                if (list.Count != 0)
                {
                    List<string> dbNames = XMLHandler.readTag("dbName", (int)Session["id"]);
                    db db = new db(dbNames[0], 12);
                    foreach (var schemeValue in list)
                    {
                        object value = db.singleItemSelectPostgres(schemeValue.columnName, schemeValue.tableName, null);
                        ResponseValue responseValue = new ResponseValue();
                        responseValue.tableName = schemeValue.tableName;
                        responseValue.columnName = schemeValue.columnName;
                        responseValue.value = value;
                        responseList.Add(responseValue);
                    }
                    db.connection.Close();
                    data = responseList;
                }
                return Json(data, "application/json", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(data);
            }
            
        }*/
        #endregion
    }
}