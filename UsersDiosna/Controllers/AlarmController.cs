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
        public static string DB { get; set;}
        public static string lang { get; set; }
        public static int plcID { get; set; }
        public void DBConnnection()
        {
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
        }

        // GET: Alarm
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            if (DB == null)
            {
                DBConnnection();
                model = await AlarmHelper.SelectAlarms(DB, 35);
            }
            else
            {
                model = await AlarmHelper.SelectAlarms(DB, 35);
            }
            ViewBag.page = 0;
            return View(model);
        }
        
        /// <param name="id">Id is page </param>s
        public async System.Threading.Tasks.Task<ActionResult> Page(int id)
        {
            if(id<0)
            {
                Session["tempforview"] = "You have reached the minimum alarm page";
                return RedirectToAction("Index");
            }
            List<AlarmHelper.alarm> model = new List<AlarmHelper.alarm>();
            if (DB == null)
            {
                DBConnnection();
                model = await AlarmHelper.SelectAlarms(DB, 35);
            }
            else
            {
                model = await AlarmHelper.SelectAlarms(DB, 35);
            }
            if (model.Count == 0)
            {
                Session["tempforview"] = "No alarms has been found";
                return RedirectToAction("Index");
            }
            
            
            return View(model);
        }
    }
}