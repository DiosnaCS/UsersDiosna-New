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

            int prevouseBatchNo = 0;
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
            DataReportModel data = db.SelectConsumption(thisMonthStart, thisMontEnd, table);
            OverviewReportModel ORM = new OverviewReportModel();
            ORM.Data = new List<OverviewReportDataModel>();

            foreach (var batch in data.Data)
            {                
                if (batch.BatchNo != prevouseBatchNo)
                {
                    OverviewReportDataModel ORDM = new OverviewReportDataModel();

                    prevouseBatchNo = batch.BatchNo;
                    if (data.Data.Exists(p => p.RecordType == Operations.FillingMotherCulture && p.BatchNo == batch.BatchNo))
                    {
                        var MotherCultureFilling = data.Data.Where(p => p.RecordType == Operations.FillingMotherCulture && p.BatchNo == batch.BatchNo);
                        ORDM.MotherCultureBatchCount = MotherCultureFilling.Count();
                        ORDM.MotherCultureAmnt = MotherCultureFilling.Sum(p => p.Actual);
                    }
                    if (data.Data.Exists(p => p.RecordType == Operations.FillingFlour && p.BatchNo == batch.BatchNo))
                    {
                        var FillingFlour = data.Data.Where(p => p.RecordType == Operations.FillingFlour && p.BatchNo == batch.BatchNo);
                        ORDM.FlourBatchCount = FillingFlour.Count();
                        ORDM.FlourAmnt = FillingFlour.Sum(p => p.Actual);
                    }
                    if (data.Data.Exists(p => p.RecordType == Operations.FillingOldBread && p.BatchNo == batch.BatchNo))
                    {
                        var FillingOldBread = data.Data.Where(p => p.RecordType == Operations.FillingOldBread && p.BatchNo == batch.BatchNo);
                        ORDM.OldBreadBatchCount = FillingOldBread.Count();
                        ORDM.OldBreadAmnt = FillingOldBread.Sum(p => p.Actual);
                    }
                    if (data.Data.Exists(p => p.RecordType == Operations.FillingLiquidYeast && p.BatchNo == batch.BatchNo))
                    {
                        var FillingLiquidYeast = data.Data.Where(p => p.RecordType == Operations.FillingLiquidYeast && p.BatchNo == batch.BatchNo);
                        ORDM.LiquidYeastBatchCount = FillingLiquidYeast.Count();
                        ORDM.LiquidYeastAmnt = FillingLiquidYeast.Sum(p => p.Actual);
                    }
                    if (data.Data.Exists(p => p.RecordType == Operations.FillingMixture && p.BatchNo == batch.BatchNo))
                    {
                        var FillingMixture = data.Data.Where(p => p.RecordType == Operations.FillingMixture && p.BatchNo == batch.BatchNo);
                        ORDM.MixtureBatchCount = FillingMixture.Count();
                        ORDM.MixtureAmnt = FillingMixture.Sum(p => p.Actual);
                    }
                    if (data.Data.Exists(p => p.RecordType == Operations.FillingGenericComponent && p.BatchNo == batch.BatchNo))
                    {
                        var FillingGenericComponent = data.Data.Where(p => p.RecordType == Operations.FillingGenericComponent && p.BatchNo == batch.BatchNo);
                        ORDM.GenericBatchCount = FillingGenericComponent.Count();
                        ORDM.GenericAmnt = FillingGenericComponent.Sum(p => p.Actual);
                    }
                    ORM.Data.Add(ORDM);
                }
                
            }
            ViewBag.month = month;
            ViewBag.year = year;

            return View(ORM);
        }
        public ActionResult Month(int year, int month)
        {
            return View();
        }
    }
}