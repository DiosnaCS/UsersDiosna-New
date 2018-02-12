using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UsersDiosna.Graph.Models;
using UsersDiosna.GraphReport.Models;
using UsersDiosna.Handlers;
using UsersDiosna.Report.Models;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GraphReportController : Controller
    {
        // GET: GraphReport
        public ActionResult Index()
        {
            object pathConfig = null;
            object pathNames = null;
            GraphReportForm form = new GraphReportForm();
            foreach (string key in Session.Keys)
            {
                if (key.Contains("pathConfig") && key.Contains(Request.QueryString["plc"].ToString()))
                {
                    pathConfig = Session[key];
                }
                if (key.Contains("pathNames") && key.Contains(Request.QueryString["plc"].ToString()))
                {
                    pathNames = Session[key];
                }
            }

            Session.Add("pathConfig", pathConfig);
            Session.Add("pathNames", pathNames);
            GraphController GC = new GraphController();
            GC.getConfig(Session["pathConfig"].ToString(), Session["pathNames"].ToString(), Session["ProjectName"].ToString());
            List<NameDef> namedefinition = GraphController.config.NameDefList;
            List<SelectListItem> tags = new List<SelectListItem>();
            foreach (NameDef namedef in namedefinition)
            {
                GraphReport.Models.Tag tag = new GraphReport.Models.Tag();
                //this will change namedef table to full tablename from tabledef 
                tag.table = GraphController.configSer.TableDef.Find(p => p.tabName.Contains(namedef.table)).tabName;
                tag.column = namedef.column;
                if (namedef.fullNames == null || namedef.fullNames.Length == 0)
                {
                    tag.label = tag.column;
                }
                else
                {
                    tag.label = namedef.fullNames[0];
                }
                SelectListItem item = new SelectListItem() { Value = tag.column, Text=tag.label};
                tags.Add(item);
            }
            form.beginDateTime = new DateTime();
            form.endDateTime = new DateTime();
            MultiSelectList multiSelect = new MultiSelectList(tags, "Value", "Text");
            form.tagList = multiSelect;
            form.graphsCount = 1;
            return View(form);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GraphReportForm form)
        {
            GraphReport.Models.DataRequest dataRequest = new GraphReport.Models.DataRequest();
            dataRequest.beginTime = Helpers.ConvertDT2pkTime(form.beginDateTime);
            dataRequest.endTime = Helpers.ConvertDT2pkTime(form.endDateTime);
            List<GraphReport.Models.Tag> selectedTags = new List<GraphReport.Models.Tag>();
            foreach(string column in form.tags)
            {
                GraphReport.Models.Tag tag = new GraphReport.Models.Tag();
                NameDef nameDef = GraphController.configSer.NameDef.Find(p => p.column == column);
                tag.column = column;
                if (nameDef.fullNames == null || nameDef.fullNames.Length == 0)
                {
                    tag.label = tag.column;
                }
                else
                {
                    tag.label = nameDef.fullNames[0];
                }
                tag.table = GraphController.configSer.TableDef.Find(p => p.tabName.Contains(nameDef.table)).tabName;
                selectedTags.Add(tag);
            }
            dataRequest.definition = selectedTags;
            dataRequest.requestType = RequestType.absoulteScale;
            string json = JsonConvert.SerializeObject(dataRequest);
            ViewBag.json = json;
            return View("data");
        }
        public async Task<JsonResult> getData()
        {
            string json = null;
            GraphReportResponse response = new GraphReportResponse();
            StreamReader stream = new StreamReader(Request.InputStream);
            if (json == null)
            {
                json = stream.ReadToEnd();
            }
            if (json != "")
            {
                //ReportModel RVM = new ReportModel();
                GraphReport.Models.DataRequest dataRequest = JsonConvert.DeserializeObject<GraphReport.Models.DataRequest>(json);
                switch (dataRequest.requestType)
                {
                    case RequestType.batches:
                        db conn = new db("InternDelights", 12);

                        break;

                    case RequestType.frequency:

                        break;
                    case RequestType.differences:

                        break;

                    case RequestType.absoulteScale:
                        GraphReportHelper graphReportHelper = new GraphReportHelper();
                        response = await graphReportHelper.GetAbsoulteScale(dataRequest);
                        break;
                }
            }
            else
            {
                return null;
            }
            return Json(response, "application/json", JsonRequestBehavior.AllowGet);
        }

        static double Double(double input) { return input*2; }
    }
}