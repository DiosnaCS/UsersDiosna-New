using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VizuLibrabrarySnapshotVals;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.IO;

namespace UsersDiosna.Controllers.Api.Tests
{
    [TestClass()]
    public class valuesApiControllerTests
    {
        [TestMethod()]
        public void putSnapshotTest()
        {

            List<RequestValue> seznam = new List<RequestValue>();
            RequestValue x = new RequestValue();
            x.tableName = "tabulka";
            x.columnName = "iSF1_Mass";
            x.valueType = tValueType.integer;
            x.iValue = 12345;
            x.iQoS = 100;
            seznam.Add(x);

            BinaryFormatter bf = new BinaryFormatter();

            //string S = @"http://dev.nordit.cz/static/y2k38.php";
            //string S = @"http://users.nordit.cz/10680_InternDelights/interndelights_overview_sch_WS.htm";
            string S = @"https://users-dev.diosna.cz/api/valuesApi/putSnapshot/"; ///164017/123456789";
            string url = "https://localhost:44385/api/valuesApi/putSnapshot/";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //req.Credentials = new NetworkCredential("tk", "Nordit0276");
            req.Method = "POST";
            var reqStream = req.GetRequestStream();

            using (reqStream)
            {
                bf.Serialize(reqStream, seznam);
                //reqStream.Write()
            }

            WebResponse resp = req.GetResponse();
            Stream responseStream = resp.GetResponseStream();

            Assert.AreEqual(reqStream, responseStream);
        }
    }
}