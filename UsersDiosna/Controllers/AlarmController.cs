using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsersDiosna.Handlers;
using System.Threading.Tasks;

namespace UsersDiosna.Controllers
{
    public class AlarmController : Controller
    {
        public void DBConnnection()
        {
            string DB = string.Empty;
            string lang = string.Empty;
            int plcID = 1;
            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    DB = Session[key].ToString();
                }
                if (key.Contains("lang" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    lang = Session[key].ToString();
                }
                if (key.Contains("plc" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    plcID = int.Parse(Session[key].ToString());
                }
            }
            Session["AlarmDB"] = DB;
            Session["AlarmLang"] = lang;
            Session["AlarmPlcID"] = plcID;
        }

        // GET: Alarm
        public async Task<ActionResult> Index()
        {
            DBConnnection();
            List<int> alarms = new List<int>();
            if (Session["filteredAlarms"] != null)
            {
                alarms = (List<int>)Session["filteredAlarms"];
            }
            else {
                alarms = null;
            }
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            AlarmHelper AH = new AlarmHelper();
            model = await AH.SelectAlarms(Session["AlarmDB"].ToString(), 0, 30, alarms);
            ViewBag.page = 0;
            ViewBag.legend = "Notification from current alarms means only from the unique ones \n Other occurence of the alarm would not viewed";
            return View(model);
        }

        /// <param name="id">Id is page </param>s
        public async Task<ActionResult> Page(int page = 0, int count = 30)
        {
            List<int> alarms = new List<int>();
            if (Session["filteredAlarms"] != null)
            {
                alarms = (List<int>)Session["filteredAlarms"];
            }
            else
            {
                alarms = null;
            }
            int offsetPage = page * count;
            if (page < 0)
            {
                Session["tempforview"] = "You have reached the minimum alarm page";
                return RedirectToAction("Index");
            }
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            AlarmHelper AH = new AlarmHelper();
            model = await AH.SelectAlarms(Session["AlarmDB"].ToString(), 0, 30, alarms);
            if (model.Count == 0)
            {
                Session["tempforview"] = "No alarms has been found";
                return RedirectToAction("Index");
            }
            ViewBag.page = page;
            ViewBag.legend = "Notification from current alarms means only from the unique ones \n Other occurence of the alarm would not viewed";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult FilterFromAlarms() {
            List<int> alarms = new List<int>();
            string[] keys = Request.Form.AllKeys;
            string stringAlarms = "";
            foreach (string sId in keys) {
                alarms.Add(int.Parse(sId));
                stringAlarms += sId + " ";
            }
            Session["filteredAlarms"] = alarms;
            Session["success"] = "Filter on following alarms has been set: " + stringAlarms;
            Session["filtered"] = stringAlarms;
            return RedirectToAction("Index", "Menu", new { id = (int)Session["id"]});
        }        
        
        public ActionResult FilterCurrent()
        {
            List<int> alarms = (List<int>)Session["alarmIDs"];
            if (alarms != null)
            {
                Session["alarmIDs"] = null;

                AlarmHelper AH = new AlarmHelper();
                List<AlarmHelper.alarm_texts> model = new List<AlarmHelper.alarm_texts>();
                model = AH.SelectAlarmsTexts(Session["AlarmDB"].ToString(), alarms);
                return View("Filter", model);
            }
            Session["tempforview"] = "Problem with accesing current alarms";
            return RedirectToAction("Index", "Menu", new { id = (int)Session["id"] });
        }

        public ActionResult FilterAll()
        {
            AlarmHelper AH = new AlarmHelper();
            List<AlarmHelper.alarm_texts> model = new List<AlarmHelper.alarm_texts>();
            model = AH.SelectAlarmsTexts(Session["AlarmDB"].ToString());
            return View("Filter", model);
        }
        public ActionResult CancelFilter()
        {
            Session["filteredAlarms"] = null;
            Session["success"] = "Filter has been sucessfully canceled";
            return RedirectToAction("Index", "Menu", new { id = (int)Session["id"] });
        }
    }
}