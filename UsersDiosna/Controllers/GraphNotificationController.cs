using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UsersDiosna.Graph.Models;

namespace UsersDiosna.Controllers
{
    public class GraphNotificationController : Controller
    {
        // GET: GraphNotification
        public ActionResult Index()
        {
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
            GraphController GC = new GraphController();
            GC.getConfig(Session["pathConfig"].ToString(), Session["pathNames"].ToString(), Session["ProjectName"].ToString());
            List<NameDef> namedefinition = GraphController.configSer.NameDef;
            foreach (NameDef namedef in namedefinition) {
                //this will change namedef table to full tablename from tabledef 
                namedef.table = GraphController.configSer.TableDef.Find(p => p.tabName.Contains(namedef.table)).tabName;
            }
            return View(namedefinition);
        }
        [HttpPost]
        // POST: AlarmNofication
        public RedirectToRouteResult NewNotification()
        {
            string definition = "";
            string tags = "";
            List<string> tablesList = new List<string>();
            string table=null;
            foreach (string key in Request.Form.AllKeys) {
                string[] values = Request.Form.GetValues(key);
                if (key.Contains("table")) {
                    table = values[0]; // Change table from hidden
                    values = null;
                    if (tablesList.Exists(p => p.Contains(table)) == false) {
                        tablesList.Add(table); //Add table which is not 
                    }
                }
                if (values != null) {
                    if (values[0].Contains("on"))
                    {
                        definition += " AND " + "\""+ table + "\".\"" + key + "\" ";
                        tags += "\"" + table + "\".\"" + key + "\",";
                    }
                    else
                    {
                        definition += " " + values[0] + " ";
                    }
                }
            }
            definition = definition.TrimEnd();
            string tables = string.Join(",", tablesList.ToArray());
            tags = tags.Substring(0, tags.Length-1);//substring the last comma
            //definition = definition.Substring(3);//Substring the string from AND
            string projectName = Session["ProjectName"].ToString();
            string userName = User.Identity.Name;
            int bakeryID = int.Parse(Session["id"].ToString());
            int type = 2;
            Session["success"] = "Notification on following alarms has been set: " + definition;
            NotificationController.Add(definition, projectName, userName, bakeryID, type, tags, tables);
            return RedirectToAction("Index", "Notification");
        }
    }
}