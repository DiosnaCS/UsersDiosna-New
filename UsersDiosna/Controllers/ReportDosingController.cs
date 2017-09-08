using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsersDiosna.Report.Models;

namespace UsersDiosna.Controllers
{
    public class ReportDosingController : Controller
    {
        // GET: ReportDosing
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Day(int day, int month, int year)
        {
            string DB = string.Empty;
            string table = string.Empty;
            int cfNum = 1;
            foreach (string key in Session.Keys)
            {
                if (key.Contains("dbName"))
                {
                    DB = Session[key].ToString();
                }
                if (key.Contains("tableName"))
                {
                    table = Session[key].ToString();
                }
                if (key.Contains("congirationNumber"))
                {
                    cfNum = int.Parse(Session[key].ToString());
                }
            }
            DateTime from = new DateTime(year, month, day);
            DateTime to = new DateTime(year, month, day + 1);
            ReportDBHelper db = new ReportDBHelper(DB,2);
            ReportDosing model = new ReportDosing();
            model = db.GetReportDosing(from, to, table);
            model.Day = day;
            model.Month = month;
            model.Year = year;
            return View(model); 
        }
    }
}