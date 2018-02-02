using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UsersDiosna.GraphReport.Models;
using UsersDiosna.Handlers;
using UsersDiosna.Report.Models;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GraphReportController : Controller
    {
        // GET: GraphReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> getData()
        {
            //response precreation
            GraphReportResponse response = new GraphReportResponse();
            response.dataSet = new List<double>();
            response.labels = new List<string>();

            string json = null;
            StreamReader stream = new StreamReader(Request.InputStream);
            if (json == null)
            {
                json = stream.ReadToEnd();
            }
            if (json != "")
            {
                //ReportModel RVM = new ReportModel();
                DataRequest dataRequest = new JavaScriptSerializer().Deserialize<DataRequest>(json);
                List<object[]> objects = new List<object[]>();
                string columns = "";
                switch (dataRequest.requestType)
                {
                    case RequestType.batches:

                        break;

                    case RequestType.frequency:

                        break;
                    case RequestType.differences:

                        break;

                    case RequestType.absoulteScale:
                        db db = new db("Dubravica", 12);
                        string[] conditions1 = { "\"UTC\"", "\"UTC\"" };
                        string[] Operators = { ">=", "<=" };
                        string[] conditions2 = { "'" + GraphReportHelper.pkTimeToUTC(dataRequest.beginTime) + "'", "'" + GraphReportHelper.pkTimeToUTC(dataRequest.endTime) + "'" };
                        string where = db.whereMultiple(conditions1, Operators, conditions2);
                        foreach (string table in dataRequest.definition.Select(tag=> tag.table).Distinct())
                        {
                            foreach (Tag tag in dataRequest.definition)
                            {
                                columns += columns += " \"" + tag.column + "\",";
                            }
                           objects = await db.multipleItemSelectPostgresAsync("\"UTC\"," + columns, table, where);
                           for(int i = 0;i<objects.Count;i++)
                           {
                                for(int j=0;j<objects[0].Length;j++)
                                {
                                    double doubleValue = (double)objects[i][j];
                                    //TODO add the name from NameDefinition not done because of writting in 
                                    string name = ;
                                    response.dataSet.Add(doubleValue);
                                    response.labels.Add();
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                return null;
            }
            return Json(data, "application/json", JsonRequestBehavior.AllowGet);
        }

        static double Double(double input) { return input*2; }
    }
}