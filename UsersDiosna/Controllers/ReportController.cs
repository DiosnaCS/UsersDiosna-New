﻿    using System;
using System.Web.Mvc;
using UsersDiosna.Report.Models;
using System.Collections.Generic;
using System.Linq;

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
                int dest = int.Parse(report.Destination);
                if (tankNames.Keys.Contains(dest))
                    report.Destination = tankNames[dest];
                else
                    Session["tempforview"] = "Check configuration of report config";
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
            List<ColumnReportModel> cleaning = model.Data.Where(p=> p.RecordType == Operations.PipWorkCleaning || p.RecordType == Operations.YeastCleaning).ToList();
            foreach (var report in model.Data)
            {
                int dest = int.Parse(report.Destination);
                if (tankNames.Keys.Contains(dest))
                    report.Destination = tankNames[dest];
                else
                    Session["tempforview"] = "Check configuration of report config";
                //report.Destination = tankNames[int.Parse(report.Destination)];
            }
            ViewBag.month = month;
            ViewBag.year = year;
            return View("calender", model);
        }

        public ActionResult Detail(int id)
        {
            ReportDBHelper db = new ReportDBHelper(DB, 2);
            DataReportModel data = db.SelectSteps(id, table);

            //if (data.Data.Exists(p => p.RecordType == Operations.Interrupt))
            ViewBag.AmntTotal = data.Data.Single(p => p.RecordType == Operations.RecipeStart).Need;
            //if (data.Data.Exists(p => p.RecordType == Operations.Interrupt))
                ViewBag.InteruptedCounts = data.Data.Where(p => p.RecordType == Operations.Interrupt).Count();
            //if (data.Data.Exists(p=> p.RecordType == Operations.StepSkip))
                ViewBag.NumberOfStepsSkipped = data.Data.Where(p => p.RecordType == Operations.StepSkip).Count();
            ViewBag.BatchNo = id;
            int dest = int.Parse(data.Data[0].Destination);
            if (tankNames.Keys.Contains(dest))
                ViewBag.Destination = tankNames[dest];
            else
                Session["tempforview"] = "Check configuration of report config"; 
            return View(data.Data);
        }

        public ActionResult GetPrevBatch(int id)
        {
            ReportDBHelper db = new ReportDBHelper(DB, 2);
            int BatchNo = db.SelectPrevBatchNo(id, table);
            if (BatchNo == 0)
            {
                Session["tempforview"] = "You have reached the minimum batch";
                return RedirectToAction("Detail", new { id = id });
            }
            DataReportModel data = db.SelectSteps(BatchNo, table);

            ViewBag.BatchNo = BatchNo;
            int dest = int.Parse(data.Data[0].Destination);
            if (tankNames.Keys.Contains(dest))
                ViewBag.Destination = tankNames[dest];
            else
                Session["tempforview"] = "Check configuration of report config";

            return View("Detail", data.Data);
        }

        public ActionResult GetNextBatch(int id)
        {
            ReportDBHelper db = new ReportDBHelper(DB, 2);
            int BatchNo = db.SelectNextBatchNo(id, table);
             if (BatchNo == 0)
            {
                Session["tempforview"] = "You have reached the maximum batch";
                return RedirectToAction("Detail", new { id = id});
            }
            DataReportModel data = db.SelectSteps(BatchNo, table);

            ViewBag.BatchNo = BatchNo;
            int dest = int.Parse(data.Data[0].Destination);
            if (tankNames.Keys.Contains(dest))
                ViewBag.Destination = tankNames[dest];
            else
                Session["tempforview"] = "Check configuration of report config";

            return View("Detail", data.Data);
        }
    }
}