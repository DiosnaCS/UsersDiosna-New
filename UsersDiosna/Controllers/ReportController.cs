using System;
using System.Web.Mvc;
using UsersDiosna.Report.Models;
using System.Collections.Generic;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "View")]
    public class ReportController : Controller
    {
        private static string DB;
        private static string table;
        private static int configrationNumber;
        private static Dictionary<int, string>  tankNames;

        // GET: ReportCalender
        public ActionResult Index()
        {
            const int startDay = 1;
            tankNames = new Dictionary<int, string>();
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
            
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            DateTime thisMonthStart = new DateTime(year, month, startDay,0,0,0);
            DateTime thisMontEnd = new DateTime(year, month+1, startDay,0,0,0);
            ReportHandler RH = new ReportHandler();
            if(configrationNumber != 0)
                tankNames = RH.getTanknames(configrationNumber);
            tankNames = RH.getTanknames();
           
            ReportDBHelper db = new ReportDBHelper(DB, 2);
            DataReportModel model = db.SelectHeaderData(thisMonthStart, thisMontEnd, table);
            foreach (var report in model.Data)
            {
                report.Destination = tankNames[int.Parse(report.Destination)];
            }
            ViewBag.month = month;
            ViewBag.year = year;
            return View("calender", model);     
        }

        public ActionResult Month(int month, int year)
        {
            const int startDay = 1;

            //int month = DateTime.Now.Month;
            //int year = DateTime.Now.Year;
            DateTime thisMonthStart = new DateTime(year, month, startDay, 0, 0, 0);
            DateTime thisMontEnd = new DateTime(year, month + 1, startDay, 0, 0, 0);
            ReportHandler RH = new ReportHandler();

            ReportDBHelper db = new ReportDBHelper(DB, 2);
            DataReportModel model = db.SelectHeaderData(thisMonthStart, thisMontEnd, table);
            foreach (var report in model.Data)
            {
                report.Destination = tankNames[int.Parse(report.Destination)];
            }
            ViewBag.month = month;
            ViewBag.year = year;
            return View("calender", model);
        }
    }
}