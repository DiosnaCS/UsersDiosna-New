using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UsersDiosna.Alarms.Models;
using UsersDiosna.Graph.Models;
using UsersDiosna.Handlers;
using UsersDiosna.Report.Models;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "View")]
    public class GraphController : Controller
    {
        // GET: Graph
        public static Config configSer { get; set; } // = new CIniFile();
        public static CIniFile config = new CIniFile();

        [HttpPost]
        public void getConfig(string pathConfig = null, string pathNames = null, string projectName = null)
        {
            string path;
            if (pathConfig == null && pathNames == null && projectName == null) {
                path = Path.PhysicalPath + @"\JSONconfig\" + Session["ProjectName"].ToString() + "_" + Session["pathConfig"].ToString().Substring(Session["pathConfig"].ToString().LastIndexOf(@"\") + 1) + ".json";
            }
            else {
                path = Path.PhysicalPath + @"\JSONconfig\" + projectName + "_" + pathConfig.Substring(pathConfig.LastIndexOf(@"\") + 1) + ".json";
            }
            string json;
            
            if (System.IO.File.Exists(path))
            {
                json = System.IO.File.ReadAllText(path);
                json = new string((from c in json
                                   where !char.IsWhiteSpace(c) || c.ToString().Contains(" ")
                                  select c).ToArray());
                int jsonLength = json.Length;
                configSer = new JavaScriptSerializer().Deserialize<Config>(json);
                config.LangEnbList = configSer.LangEnbList;
                config.NameDefList = configSer.NameDef;
                config.TableDefList = configSer.TableDef;
                config.TextlistDefList = configSer.TextlistDef;
            }
            else {
                if (config.ViewList.Count == 0)
                {
                    if (Session != null) {
                        pathConfig = Session["pathConfig"].ToString();
                        pathNames = Session["pathNames"].ToString();
                    }
                    Iniparser ini = new Iniparser(pathConfig, pathNames);
                    ini.ParseLangs(config);
                    ini.ParseNames(config, Const.separators);
                    ini.ParseCfg(config, Const.separators, config);
                    Iniparser.FindTableName(config); //Only to find missing(all are missing) tableName
                }
                json = config.toJSON(config);
                Directory.CreateDirectory(Path.PhysicalPath + @"\JSONconfig");
                System.IO.File.WriteAllText(path, json);
            }
            if (pathConfig == null && pathNames == null && projectName == null)
            {
                Response.ContentType = "application /json";
                Response.Write(json);
            }
        }


        public ActionResult Index()
        {
            config = new CIniFile();
            object pathConfig = null;
            object pathNames = null;
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
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> getData(string json = null)
        {
            StreamReader stream = new StreamReader(Request.InputStream);
            if (json == null)
            {
                json = stream.ReadToEnd();
            }
            if (json != "")
            {
                object data = new object();
                DataRequest dataRequest = new JavaScriptSerializer().Deserialize<DataRequest>(json);
                try
                {
                    GraphHandler GH = new GraphHandler();
                   
                    DataRequest dataResponse = await GH.proceedSQLquery(dataRequest, config);
                    if ((dataRequest.beginTime + dataRequest.timeAxisLength) <= AlarmHelper.DateTimeTopkTime(DateTime.Now))
                    {
                        if (dataResponse.tags.All(p => p.vals.All(q => (q == double.MaxValue)) == true))
                        {
                            for (int i = 0; i < dataResponse.tags.Count; i++)
                            {
                                dataResponse.tags[i].vals = null;
                            }
                        }
                    }
                    data = dataResponse;
                    return Json(data, "application/json", JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    data = dataRequest;
                    string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString();
                    string name = this.ControllerContext.RouteData.Values["controller"].ToString();
                    Error.toFile(k, name);
                    Session["tempforview"] = Error.timestamp + "   Error " + MvcApplication.ErrorId.ToString() + " occured so please try it again after some time"; //To screen also with id 
                    return Json(data, "application/json", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<JsonResult> getEventsHeader()
        {
            int batchStart = 0;
            int batchEnd = 0;
            StreamReader stream = new StreamReader(Request.InputStream);
            string json = stream.ReadToEnd();
            DataRequest dataRequest = new JavaScriptSerializer().Deserialize<DataRequest>(json);
            string DB = string.Empty;
            string table = string.Empty;
            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName") && key.Contains(Request.QueryString["plc"].ToString()))
                {
                    DB  = Session[key].ToString();
                }
                if (key.Contains("tableName") && key.Contains(Request.QueryString["plc"].ToString()))
                {
                    table = Session[key].ToString();
                }
            }
            ReportDBHelper db = new ReportDBHelper(DB, 2);
            DataReportModel data =  await db.SelectHeaderDataAsync(dataRequest.beginTime, dataRequest.beginTime + dataRequest.timeAxisLength, table);
            
            GraphEventsData GED = new GraphEventsData();
            GED.events = new List<ColumnGraphModel>();
            foreach (ColumnReportModel CRM in data.Data)
            {
                ColumnGraphModel CGM = new ColumnGraphModel();

                CGM.RecordNo = CRM.RecordNo;
                CGM.RecordType = (int)CRM.RecordType;
            
                CGM.TimeStart = AlarmHelper.DateTimeTopkTime(CRM.TimeStart);
                CGM.TimeEnd = AlarmHelper.DateTimeTopkTime(CRM.TimeEnd);
                CGM.BatchNo = CRM.BatchNo;
                CGM.Destination = CRM.Destination;
                CGM.Need = CRM.Need;
                CGM.Actual = CRM.Actual;

                CGM.Variant1 = CRM.Variant1;
                CGM.Variant2 = CRM.Variant2;
                CGM.Variant3 = CRM.Variant3;
                if (CGM.RecordType == 10 || CGM.RecordType == 14)
                {
                    if (data.Data.Exists(p=> (int)p.RecordType == 10 && p.BatchNo == CGM.BatchNo))
                        batchStart = AlarmHelper.DateTimeTopkTime(data.Data.Single(p => (int)p.RecordType == 10 && p.BatchNo == CGM.BatchNo).TimeStart);
                    if (data.Data.Exists(p => (int)p.RecordType == 14 && p.BatchNo == CGM.BatchNo)) 
                        batchEnd = AlarmHelper.DateTimeTopkTime(data.Data.Single(p => (int)p.RecordType == 14 && p.BatchNo == CGM.BatchNo).TimeStart);
                    if (data.Data.Exists(p => (int)p.RecordType == 10 && p.BatchNo == CGM.BatchNo) && data.Data.Exists(p => (int)p.RecordType == 14 && p.BatchNo == CGM.BatchNo))
                        CGM.Variant3 = batchEnd - batchStart;
                    //Duration is for George graphs in recordType 10 and 14
                }
                CGM.Variant4 = CRM.Variant4;

                GED.events.Add(CGM);
            }
            
            return Json(GED); //it add data in the reguested shape to client
        }

        [HttpPost]
        public JsonResult getAlarmConfig()
        {
            try
            {
                int plcID = 1;
                string DB = string.Empty;
                string table = string.Empty;
                foreach (string key in Session.Keys)
                {
                    if (key.Contains("plcID") && key.Contains(Request.QueryString["plc"].ToString()))
                    {
                        plcID = int.Parse(Session[key].ToString());
                    }
                    if (key.Contains("dbName") && key.Contains(Request.QueryString["plc"].ToString()))
                    {
                        DB = Session[key].ToString();
                    }
                    if (key.Contains("tableName") && key.Contains(Request.QueryString["plc"].ToString()))
                    {
                        table = Session[key].ToString();
                    }
                }
                AlarmHelper AH = new AlarmHelper();
                List<AlarmHelper.alarm_texts> data = new List<AlarmHelper.alarm_texts>();
                AlarmGraphConfig ALG = new AlarmGraphConfig();
                data = AH.SelectAlarmsTexts(DB, null, plcID);
                bool firstEn = true;
                bool firstCz = true;
                bool firstDe = true;
                bool firstPl = true;
                foreach (AlarmHelper.alarm_texts text in data)
                {
                    switch (text.lang)
                    {
                        case "en":
                            if (firstEn)
                                ALG.EN = new List<string>();
                            firstEn = false;
                            ALG.EN.Add(text.title);
                            break;
                        case "cs":
                            if (firstCz)
                                ALG.CZ = new List<string>();
                            firstCz = false;
                            ALG.CZ.Add(text.title);
                            break;
                        case "de":
                            if (firstDe)
                                ALG.DE = new List<string>();
                            firstDe = false;
                            ALG.DE.Add(text.title);
                            break;
                        case "pl":
                            if (firstPl)
                                ALG.PL = new List<string>();
                            firstPl = false;
                            ALG.PL.Add(text.title);
                            break;
                        default:
                            if (firstEn)
                                ALG.EN = new List<string>();
                            firstEn = false;
                            ALG.EN.Add(text.title);
                            break;
                    }
                }
                return Json(ALG);
            } catch(Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> getAlarmsData()
        {
            try
            {
                StreamReader stream = new StreamReader(Request.InputStream);
                string json = stream.ReadToEnd();
                DataRequest dataRequest = new JavaScriptSerializer().Deserialize<DataRequest>(json);
                string DB = string.Empty;
                string table = string.Empty;
                int plcID = 1;
                foreach (string key in Session.Keys)
                {
                    if (key.Contains("plcID") && key.Contains(Request.QueryString["plc"].ToString()))
                    {
                        plcID = (int)Session[key];
                    }
                    if (key.Contains("dbName") && key.Contains(Request.QueryString["plc"].ToString()))
                    {
                        DB = Session[key].ToString();
                    }
                    if (key.Contains("tableName") && key.Contains(Request.QueryString["plc"].ToString()))
                    {
                        table = Session[key].ToString();
                    }
                }
                List<AlarmGraphData> data = new List<AlarmGraphData>();
                AlarmHelper AH = new AlarmHelper();
                data = await AH.SelectAlarms(DB, dataRequest.beginTime, dataRequest.beginTime + dataRequest.timeAxisLength, plcID);
                return Json(data);
            } catch(Exception e)
            {
                List<AlarmGraphData> data = new List<AlarmGraphData>();
                AlarmGraphData mesData = new AlarmGraphData();
                mesData.title = e.Message.ToString();
                data.Add(mesData);
                AlarmGraphData source = new AlarmGraphData();
                source.title = e.Source.ToString();
                data.Add(source);
                return Json(data);
            }
        }

        [HttpPost]
        public JsonResult Config()
        {
            if (config.ViewList.Count == 0)
            {
                ViewData["pathConfig"] = Session["pathConfig"];
                ViewData["pathNames"] = Session["pathNames"];
                Iniparser ini = new Iniparser(ViewData["pathConfig"].ToString(), ViewData["pathNames"].ToString());
                ini.ParseNames(config, Const.separators);
                ini.ParseCfg(config, Const.separators, config);
            }
            object data = new object();
            data = config;
            var json = Json(data, "application/json", JsonRequestBehavior.AllowGet);
            return Json(data, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
