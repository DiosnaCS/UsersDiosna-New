using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsersDiosna.Report.Models;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "View")]
    public class ReportOverviewController : Controller
    {
        private static string DB;
        private static string table;
        private static int configrationNumber;
        private static Dictionary<int, string> tankNames;
        // GET: ReportOverview
        public ActionResult Index()
        {
            const int startDay = 1;

            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            DateTime thisMonthStart = new DateTime(year, month, startDay, 0, 0, 0);
            DateTime thisMontEnd = new DateTime(year, month + 1, startDay, 0, 0, 0);

            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    DB = Session[key].ToString();
                }
                if (key.Contains("tableName" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    table = Session[key].ToString();
                }
                if (key.Contains("congirationNumber" + Request.QueryString["name"] + Request.QueryString["plc"]))
                {
                    configrationNumber = int.Parse(Session[key].ToString());
                }
            }

            ReportDBHelper db = new ReportDBHelper(DB, 2);
            OverviewReportModel data = db.SelectConsumption(thisMonthStart, thisMontEnd, table);

            ViewBag.month = month;
            ViewBag.year = year;

            return View(data);
        }
        public ActionResult Month(int year, int month)
        {
            const int startDay = 1;

            DateTime thisMonthStart = new DateTime(year, month, startDay, 0, 0, 0);
            DateTime thisMontEnd = new DateTime(year, month + 1, startDay, 0, 0, 0);

            ReportDBHelper db = new ReportDBHelper(DB, 2);
            OverviewReportModel data = db.SelectConsumption(thisMonthStart, thisMontEnd, table);

            ViewBag.month = month;
            ViewBag.year = year;

            return View("Index", data);
        }
    }
} 