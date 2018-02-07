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
            //response precreation
            GraphReportResponse response = new GraphReportResponse();
            response.datasets = new List<DataSet>();
            //response.labels = new List<string>();
            string tagLabel = "";
            DateTime dateTimeLabel = new DateTime();

            string json = null;
            StreamReader stream = new StreamReader(Request.InputStream);
            if (json == null)
            {
                json = stream.ReadToEnd();
            }
            if (json != "")
            {
                //ReportModel RVM = new ReportModel();
                GraphReport.Models.DataRequest dataRequest = JsonConvert.DeserializeObject<GraphReport.Models.DataRequest>(json);
                List<object[]> objects = new List<object[]>();
                string columns = "";
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
                        db db = new db("InternDelights", 12);
                        string[] conditions1 = { "\"UTC\"", "\"UTC\"" };
                        string[] Operators = { ">=", "<=" };
                        string[] conditions2 = { "'" + GraphReportHelper.pkTimeToUTC(dataRequest.beginTime) + "'", "'" + GraphReportHelper.pkTimeToUTC(dataRequest.endTime) + "'" };
                        string where = db.whereMultiple(conditions1, Operators, conditions2);
                        foreach (string table in dataRequest.definition.Select(tag=> tag.table).Distinct())
                        {
                            foreach (GraphReport.Models.Tag tag in dataRequest.definition)
                            {
                                columns += " \"" + tag.column + "\",";
                            }
                            columns = columns.Substring(0, columns.Length - 1);
                            objects = await db.multipleItemSelectPostgresAsync("\"UTC\"," + columns, table, where);
                           for(int i = 0;i<objects.Count;i++)
                           {
                                for(int j=1;j<=objects[0].Length;j++)
                                {
                                    if (i == 0)
                                    {
                                        DataSet onlyColorDataSet = new DataSet();
                                        onlyColorDataSet.backgroundColor = ColorTranslator.ToHtml(Color.DarkOliveGreen);
                                        //onlyColorDataSet.fillColor = "#8DB986"; //ColorTranslator.ToHtml(Color.AliceBlue);
                                        //onlyColorDataSet.highlightColor = "#8DB986";//ColorTranslator.ToHtml(Color.Aqua);
                                        //onlyColorDataSet.highlightStroker = "#ACCE91";// ColorTranslator.ToHtml(Color.Beige);
                                        //onlyColorDataSet.strokeColor = "#ACCE91"; //ColorTranslator.ToHtml(Color.Blue);
                                        response.datasets.Add(onlyColorDataSet);
                                        if (response.datasets[j - 1].data == null)
                                        {
                                            response.datasets[j - 1].data = new List<double>();
                                        }
                                    }
                                    double doubleValue = (double)objects[i][j];
                                    response.datasets[j-1].data.Add(doubleValue);
                                    if (j < dataRequest.definition.Count)
                                    {
                                        tagLabel = dataRequest.definition[j - 1].label;
                                    }
                                    response.datasets[j - 1].label = tagLabel;
                                }
                                int lastDay = dateTimeLabel.Day;
                                dateTimeLabel = Helpers.ConvertpkTime2DT(Helpers.utcToPkTime(objects[i][0].ToString()));
                                if (dateTimeLabel.Day != lastDay)
                                {
                                    response.labels.Add(dateTimeLabel.ToString());
                                }
                                else
                                {
                                    response.labels.Add(dateTimeLabel.ToShortTimeString());
                                }
                            }
                        }
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