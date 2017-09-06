using System.Web.Mvc;
using UsersDiosna.Graph.Models;
using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;
using UsersDiosna.Handlers;
using System.Threading.Tasks;
using UsersDiosna.Report.Models;
using System.Collections.Generic;

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
                path = Path.physicalPath + @"\JSONconfig\" + Session["ProjectName"].ToString() + "_" + Session["pathConfig"].ToString().Substring(Session["pathConfig"].ToString().LastIndexOf(@"\") + 1) + ".json";
            }
            else {
                path = Path.physicalPath + @"\JSONconfig\" + projectName + "_" + pathConfig.Substring(pathConfig.LastIndexOf(@"\") + 1) + ".json";
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
                Directory.CreateDirectory(Path.physicalPath + @"\JSONconfig");
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
        public JsonResult getData()
        {
            StreamReader stream = new StreamReader(Request.InputStream);
            string json = stream.ReadToEnd();
            if (json != "")
            {
                object data = new object();
                DataRequest dataRequest = new JavaScriptSerializer().Deserialize<DataRequest>(json);
                try
                {
                    GraphHandler GH = new GraphHandler();
                   
                    DataRequest dataResponse = GH.proceedSQLquery(dataRequest, config);
                    data = dataResponse;
                    return Json(data, "application/json", JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    data = dataRequest;
                    string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString();
                    string name = this.ControllerContext.RouteData.Values["controller"].ToString();
                    Error.toFile(k, name);
                    Session["tempforview"] = Error.timestamp + "   Error " + Error.id.ToString() + " occured so please try it again after some time"; //To screen also with id 
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
            return Json(data);
        }

        [HttpPost]
        public JsonResult getAlarmConfig()
        {
            StreamReader stream = new StreamReader(Request.InputStream);
            string json = stream.ReadToEnd();
            DataRequest dataRequest = new JavaScriptSerializer().Deserialize<DataRequest>(json);
            string DB = string.Empty;
            string table = string.Empty;
            foreach (string key in Session.Keys)
            {
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
            object data = AH.SelectAlarmsTexts(DB).Cast<object>();
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> getAlarmsData()
        {
            StreamReader stream = new StreamReader(Request.InputStream);
            string json = stream.ReadToEnd();
            DataRequest dataRequest = new JavaScriptSerializer().Deserialize<DataRequest>(json);
            string DB = string.Empty;
            string table = string.Empty;
            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName") && key.Contains(Request.QueryString["plc"].ToString()))
                {
                    DB = Session[key].ToString();
                }
                if (key.Contains("tableName") && key.Contains(Request.QueryString["plc"].ToString()))
                {
                    table = Session[key].ToString();
                }
            }
            List<AlarmHelper.alarm> data = new List<AlarmHelper.alarm>();
            AlarmHelper AH = new AlarmHelper();
            data = await AH.SelectAlarms(DB,dataRequest.beginTime,dataRequest.beginTime + dataRequest.timeAxisLength);
            return Json(data);
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
