using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using VizuLibrabrarySnapshotVals;
using UsersDiosna.Handlers;
using UsersDiosna.Sheme.Models;
using System.Text;

namespace UsersDiosna.Controllers.Api
{
    public class TestApiController : ApiController
    {
        //https://users-dev.diosna.cz/api/TestApi/putSnapshot
        //https://localhost:44385/api/TestApi/putSnapshot
        [HttpGet]
        public string putSnapshot()
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
            MemoryStream memStreamReq = new MemoryStream();
            object responseObject = new object();
            List<RequestValue> responseList = new List<RequestValue>();
            responseList.Add(new RequestValue { tableName = "Data Not solved", columnName = "Response is bad" });
            string url = @"https://users-dev.diosna.cz/api/ValuesApi/putSnapshot/164017/123456789/";
            url = "https://localhost:44385/api/ValuesApi/putSnapshot/164017/123456789/";
            //var byteArray = Encoding.UTF8.GetBytes("Neco desne zajimave3h0oweg");


            bf.Serialize(memStreamReq, seznam);

            byte[] listBytes = memStreamReq.ToArray();
            using (var client = new System.Net.WebClient())
            {
                byte[] responseData = client.UploadData(url, "PUT", listBytes);
               /* 
                
                MemoryStream memStreamResp = new MemoryStream(responseData);
                responseObject = bf.Deserialize(memStreamResp);

                if (responseObject is List<RequestValue>)
                {
                    responseList = (List<RequestValue>)responseObject;
                }
                */
            }

            return responseList[0].tableName + " " + responseList[0].columnName;
        }

    }
}