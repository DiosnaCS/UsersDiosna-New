using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsersDiosna.Graph.Models;
using System.Web.Script.Serialization;
using UsersDiosna.Controllers;
using System.Net;
using System.Text;

namespace UsersDiosna.Tests
{
    [TestClass]
    public class GraphControllerTest
    {
        [TestMethod]
        public void getData()
        {
            for (int iteratation = 0; iteratation < 3; iteratation++) {
                //var data = new DataRequest();
                //data.beginTime = AlarmHelper.DateTimeTopkTime(DateTime.Today);
                //data.timeAxisLength = 172800;
                string json = " {\"beginTime\": 562201600, \"timeAxisLength\": 172800, \"tags\":[{\"table\": \"norm\", \"column\": \"diSF1_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"diSF2_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"diSF3_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"diSF4_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"diSF5_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"diSF6_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"diSF7_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"diSF8_Weight\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF1\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF2\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF3\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF4\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF5\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF6\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF7\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_TempSF8\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF1\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF2\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF3\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF4\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF5\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF6\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF7\", \"period\": 60}, {\"table\": \"norm\", \"column\": \"ipH_SF8\", \"period\": 60}]}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://users2.diosna.cz/graph?name=Graphs&plc=Bread%20fermentation");
                request.ContentType = "application/json; charset=utf-8"; //set the content type to JSON
                request.Method = "POST"; //make an HTTP POST
                using (var streamWriter = request.GetRequestStream())
                {
                   byte[] jsonBytes =  Encoding.ASCII.GetBytes(json);
                    streamWriter.Write(jsonBytes, 0, jsonBytes.Length);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var data = new JavaScriptSerializer().Deserialize<DataRequest>(json);
                for (int i = 0; i < data.tags.Count; i++)
                {
                    data.tags[i].vals = null;
                }
                var test = new GraphController();

                Assert.AreNotEqual(data, test.getData());
                var data2 = new JavaScriptSerializer().Deserialize<DataRequest>(json);
                for (int j = 0; j < data.tags.Count; j++)
                {
                    data2.tags[j].vals = new double[data.timeAxisLength];
                    data2.tags[j].vals = Extension.Populate(data2.tags[j].vals, double.MaxValue);
                }

                Assert.AreNotEqual(data2, test.getData());
            }
        }
    }
}
