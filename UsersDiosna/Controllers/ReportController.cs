using System;
using System.Web.Mvc;
using UsersDiosna.Report.Models;
using System.Collections.Generic;

namespace UsersDiosna.Controllers
{
    public class ReportController : Controller
    {
        private static string DB;
        private static string table;
        // GET: ReportCalender
        public ActionResult Index()
        {
            const int startDay = 1;

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
                    table = Session[key].ToString();
                }
            }
            
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            DateTime thisMonthStart = new DateTime(year, month, startDay,0,0,0);
            DateTime thisMontEnd = new DateTime(year, month+1, startDay,0,0,0);
            ReportHandler RH = new ReportHandler();
            List<string> tankNames = RH.getTanknames();

            ReportDBHelper db = new ReportDBHelper(DB, 2);
            DataReportModel model = db.SelectHeaderData(thisMonthStart, thisMontEnd, table);
            return View("calender", model);     
        }
    }
}