using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsersDiosna.Controllers;
using UsersDiosna.GraphReport.Models;

namespace UsersDiosna.Handlers
{
    public class GraphReportHelper
    {
        #region UTC
        public static string pkTimeToUTC(double time)
        {
            double utcTime = (time / 86400) + 2451544.5;
            string utc = utcTime.ToString();
            if (utc.Contains(","))
            {
                utc = utc.Replace(",", ".");
            }
            return utc;
        }
        public long utcToPkTime(string time)
        {
            double utcTime;
            //zakrácení času v utc na fixní délku 'od ":" až do konce odmažeme'
            if (time.IndexOf(":") >= 0)
            {
                int idx = time.IndexOf(":");
                time = time.Substring(0, idx - 1);
            }
            if (time.Contains("."))
            {
                time = time.Replace(".", ",");
            }
            try
            {
                utcTime = double.Parse(time);
            }
            catch (FormatException e)
            {
                time = time.Replace(".", ",");
                Error.toFile(e.Message.ToString() + " String time " + time, this.GetType().Name.ToString());
                utcTime = double.Parse(time);
            }

            utcTime = Math.Round((utcTime - (24515445E-1)) * 86400);
            return (long)utcTime;
        }
        #endregion
        public async Task<GraphReportResponse> GetAbsoulteScale(DataRequest dataRequest)
        {
            //response precreation
            GraphReportResponse response = new GraphReportResponse();
            response.datasets = new List<DataSet>();
            //response.labels = new List<string>();
            string tagLabel = "";
            DateTime dateTimeLabel = new DateTime();
            List<object[]> objects = new List<object[]>();
            string columns = "";
            db db = new db("InternDelights", 12);
            string[] conditions1 = { "\"UTC\"", "\"UTC\"" };
            string[] Operators = { ">=", "<=" };
            string[] conditions2 = { "'" + GraphReportHelper.pkTimeToUTC(dataRequest.beginTime) + "'", "'" + GraphReportHelper.pkTimeToUTC(dataRequest.endTime) + "'" };
            string where = db.whereMultiple(conditions1, Operators, conditions2);
            foreach (string table in dataRequest.definition.Select(tag => tag.table).Distinct())
            {
                foreach (GraphReport.Models.Tag tag in dataRequest.definition)
                {
                    columns += " \"" + tag.column + "\",";
                }
                columns = columns.Substring(0, columns.Length - 1);
                objects = await db.multipleItemSelectPostgresAsync("\"UTC\"," + columns, table, where);
                for (int i = 0; i < objects.Count; i++)
                {
                    for (int j = 1; j <= objects[0].Length; j++)
                    {
                        if (i == 0)
                        {
                            DataSet onlyColorDataSet = new DataSet();
                            onlyColorDataSet.backgroundColor = ColorTranslator.ToHtml(Color.DarkOliveGreen);
                            response.datasets.Add(onlyColorDataSet);
                            if (response.datasets[j - 1].data == null)
                            {
                                response.datasets[j - 1].data = new List<double>();
                            }
                        }
                        double doubleValue = (double)objects[i][j];
                        response.datasets[j - 1].data.Add(doubleValue);
                        if (j < dataRequest.definition.Count)
                        {
                            tagLabel = dataRequest.definition[j - 1].label;
                        }
                        response.datasets[j - 1].label = tagLabel;
                    }
                    int lastDay = dateTimeLabel.Day;
                    dateTimeLabel = Helpers.ConvertpkTime2DT(Helpers.utcToPkTime(objects[i][0].ToString()));
                    if (dateTimeLabel.Day != lastDay)
                    {
                        response.labels.Add(dateTimeLabel.ToString());
                    }
                    else
                    {
                        response.labels.Add(dateTimeLabel.ToShortTimeString());
                    }
                }
            }
            return response;
        }
    }
}