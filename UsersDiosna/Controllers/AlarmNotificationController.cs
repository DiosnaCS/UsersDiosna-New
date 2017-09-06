using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using UsersDiosna.Handlers;

namespace UsersDiosna.Controllers
{
    public class AlarmNotificationController : Controller
    {
        [HttpPost]
        // POST: AlarmNofication
        public RedirectToRouteResult NewNotification()
        {
            string sIds = "";
            string[] keys = Request.Form.AllKeys;
            foreach (string sId in keys) {
                //ids.Add(int.Parse(sId));
                sIds += sId + " ";
            }
            sIds = sIds.Substring(0, sIds.Length-1);//substring the last comma
            string projectName = Session["ProjectName"].ToString();
            string userName = User.Identity.Name;
            int bakeryID = int.Parse(Session["id"].ToString());
            int type = 1;
            Session["success"] = "Notification on following alarms has been set: " + sIds;
            NotificationController.Add(sIds, projectName, userName, bakeryID,type);
            return RedirectToAction("Index", "Notification");
        }
        // GET: AlarmNofication
        public ActionResult All()
        {
            string DB = null;
            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    DB = Session[key].ToString();
                }
            }
            ViewBag.Alarms = AlarmNotificationHandler.SelectAlarmsTexts(DB);
            return View();
        }

        public ActionResult FromCurrent()
        {
            List<int> alarms = (List<int>)Session["alarmIDs"];
            if (alarms != null) {
                Session["alarmIDs"] = null;
                if (Session["AlarmDB"].ToString() == "")
                {
                    string DB = string.Empty;
                    foreach (string key in Session.Keys)
                    {
                        if (key.Contains("dbName" + Request.QueryString["name"] + Request.QueryString["plc"]))
                        {
                            DB = Session[key].ToString();
                        }
                    }
                    Session["AlarmDB"] = DB;
                }
                ViewBag.Alarms = AlarmNotificationHandler.SelectAlarmsTexts(Session["AlarmDB"].ToString(), alarms);
                return View("All");
            }
            Session["tempforview"] = "Problem with accesing current alarms";
            return RedirectToAction("Index", "Alarm");
        }
        
    }
}