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

namespace UsersDiosna.Controllers
{
    [Authorize(Roles="Admin")]
    public class NewSchemesController : Controller
    {
        private SvgDocument svg { get; set; } 

        // GET: SchemeEditor
        public ActionResult Index()
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
                svg = SvgDocument.Open(Path.PhysicalPath + pathToSvg);

                SchemeEditor model = new SchemeEditor();
                model.relativePath = pathToSvg.Replace(@"\", @"/");
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
            
        }
    }
}