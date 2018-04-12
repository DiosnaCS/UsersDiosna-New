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

namespace UsersDiosna.Controllers.Api
{
    public class ValuesApiController : ApiController
    {
        //https://users-dev.diosna.cz/api/valuesApi/putSnapshot/{projectId}/{pkTime}
        //https://localhost:44385/api/ValuesApi/putSnapshot/{projectId}/{pkTime}
        [HttpPut]
        [Route("api/valuesApi/putSnapshot/{projectId}/{pkTime}")]
        public async Task<JsonResult<object>> putSnapshot(int projectId,int pkTime)
        {
            try
            {
                HttpContent requestContent = Request.Content;
                Stream stream = await requestContent.ReadAsStreamAsync();

                StreamReader streamReader = new StreamReader(stream);


                object data = new object();
                //List<ResponseValue> values = new List<ResponseValue>();
                BinaryFormatter binFormatter = new BinaryFormatter();
                string sStream = streamReader.ReadToEnd();
                if (sStream != "[]" || sStream != null || sStream != "")
                {
                    data = sStream + " has been received";
                    
                List<RequestValue> list = new List<RequestValue>();
                stream.Position = 0;
                data = binFormatter.Deserialize(stream);
                if (data is List<RequestValue>)
                {
                        list = (List<RequestValue>)data;//binFormatter.Deserialize(stream);
                }
                else
                {
                    data = "TypeMismatch via loading from request stream-Verfy structure-" + DateTime.Now.ToString();
                    Error.toFile("TypeMismatch via loading from request stream - Verfy structure", "ApiSchemesPutSnaschot");
                }
                if (list.Count != 0)
                {
                    NewSchemesHandler schemesHandler = new NewSchemesHandler();
                    data = await schemesHandler.putSnapshotDataIntoFile(list, projectId, pkTime);
                }
                }
                else
                {
                    data = "Error: Zero data sent stop sending no data requests";
                    return Json(data);
                }
                data = true;
                return Json(data);
            }
            catch (Exception e)
            {
                object data = new object();
                data = e;
                Error.toFile(e.Message + e.InnerException + e.StackTrace, "ApiSchemesPutSnaschot");
                return Json(data);
            }

        }


        //https://localhost:44385/api/ValuesApi/putSnapshotGet/ 
 
    }
}