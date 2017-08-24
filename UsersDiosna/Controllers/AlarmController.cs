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
            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    AlarmHelper.DB = Session[key].ToString();
                }
                if (key.Contains("lang" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    AlarmHelper.lang = Session[key].ToString();
                }
                if (key.Contains("plc" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    AlarmHelper.plcID = int.Parse(Session[key].ToString());
                }
            }
        }

        // GET: Alarm
        public async Task<ActionResult> Index()
        {
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            DBConnnection();
            model = await AlarmHelper.SelectAlarms(AlarmHelper.DB, 0, 30);
            ViewBag.page = 0;
            ViewBag.legend = "Notification from current alarms means only from the unique ones \n Other occurence of the alarm would not viewed";
            return View(model);
        }
        
        /// <param name="id">Id is page </param>s
        public async Task<ActionResult> Page(int page = 0, int count = 30)
        {
            int offsetPage = page * count;
            if (page<0)
            {
                Session["tempforview"] = "You have reached the minimum alarm page";
                return RedirectToAction("Index");
            }
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            model = await AlarmHelper.SelectAlarms(AlarmHelper.DB, 0, 30);
            if (model.Count == 0)
            {
                Session["tempforview"] = "No alarms has been found";
                return RedirectToAction("Index");
            }
            ViewBag.page = page;
            ViewBag.legend = "Notification from current alarms means only from the unique ones \n Other occurence of the alarm would not viewed";
            return View("Index",model);
        }

        public async Task<ActionResult> FilterCurrent() {
            return View();
        }

        public async Task<ActionResult> FilterAll()
        {
            return View();
        }
        /*
        public async Task<ActionResult> FilterCurrent( alarms)
        {
            
            return View(model);
        }

        public async Task<ActionResult> FilterAll()
        {
            return View(model);
        }
        */
    }
}