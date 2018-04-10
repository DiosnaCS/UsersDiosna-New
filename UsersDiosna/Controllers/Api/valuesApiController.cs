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
    public class valuesApiController : ApiController
    {
        //users-dev.diosna.cz/api/valuesApi/putSnapshot/{projectId}/{pkTime}
        [System.Web.Mvc.HttpPut]
        public async Task<JsonResult<object>> putSnapshot([FromBody] Stream stream)
        {
            try
            {
                StreamReader streamReader = new StreamReader(stream);
                object data = new object();
                //List<ResponseValue> values = new List<ResponseValue>();
                BinaryFormatter binFormatter = new BinaryFormatter();
                string sStream = streamReader.ReadToEnd();
                if (sStream != "[]" || sStream != null || sStream != "")
                {

                    List<RequestValue> list = new List<RequestValue>();
                    data = binFormatter.Deserialize(stream);
                    if (data is List<RequestValue>)
                    {
                        list = (List<RequestValue>)binFormatter.Deserialize(stream);
                    }
                    else
                    {
                        data = "TypeMismatch via loading from request stream-Verfy structure-" + DateTime.Now.ToString();
                        Error.toFile("TypeMismatch via loading from request stream - Verfy structure", "ApiSchemesPutSnaschot");
                    }
                    if (list.Count != 0)
                    {
                        NewSchemesHandler schemesHandler = new NewSchemesHandler();
                        //data = await schemesHandler.putSnapshotData(values, list, projectId);
                    }
                }
                else
                {
                    return Json(data);
                }
                return Json(data);
            }
            catch (Exception e)
            {
                object data = new object();
                data = e;
                Error.toFile(e.Message + e.InnerException + e.StackTrace, "ApiSchemesPutSnaschot");
                return Json(data);
            }

        }/*
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        */
    }
}