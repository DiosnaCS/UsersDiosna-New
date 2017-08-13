using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsersDiosna.Handlers;

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
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            if (AlarmHelper.DB == null)
            {
                DBConnnection();
                model = await AlarmHelper.SelectAlarms(AlarmHelper.DB, 0, 30);
            }
            else
            {
                model = await AlarmHelper.SelectAlarms(AlarmHelper.DB, 0, 30);
            }
            ViewBag.page = 0;
            ViewBag.legend = "Notification from current alarms means only from the unique ones \n Other occurence of the alarm would not viewed";
            return View(model);
        }
        
        /// <param name="id">Id is page </param>s
        public async System.Threading.Tasks.Task<ActionResult> Page(int page = 0, int count = 30)
        {
            int offsetPage = page * count;
            if (page<0)
            {
                Session["tempforview"] = "You have reached the minimum alarm page";
                return RedirectToAction("Index");
            }
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            if (AlarmHelper.DB == null)
            {
                DBConnnection();
                model = await AlarmHelper.SelectAlarms(AlarmHelper.DB, offsetPage, count);
            }
            else
            {
                model = await AlarmHelper.SelectAlarms(AlarmHelper.DB, offsetPage, count);
            }
            if (model.Count == 0)
            {
                Session["tempforview"] = "No alarms has been found";
                return RedirectToAction("Index");
            }
            ViewBag.page = page;
            ViewBag.legend = "Notification from current alarms means only from the unique ones \n Other occurence of the alarm would not viewed";
            return View("Index",model);
        }
    }
}