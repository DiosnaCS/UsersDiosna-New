using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UsersDiosna.Controllers
{
    public class AlarmNotificationController : Controller
    {
 
        public static int DateTimetTopkTime(DateTime DT) {
            long preResult = (DT.Ticks - (630836424000000000 - 13608000000000))/10000000;
            int result = (int)preResult;
            return result;
        }
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

        public ActionResult FromCurrent(List<int> alarms)
        {
            string DB = null;
            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    DB = Session[key].ToString();
                }
            }
            ViewBag.Alarms = AlarmNotificationHandler.SelectAlarmsTexts(DB,alarms);
            return View("All"); //One view is enough :)
        }
        
    }
}