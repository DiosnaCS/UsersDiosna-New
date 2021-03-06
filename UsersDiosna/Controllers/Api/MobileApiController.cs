﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UsersDiosna.Handlers;
using UsersDiosna.Sheme.Models;

namespace UsersDiosna.Controllers
{
    public class MobileApiController : ApiController
    {
        // GET api/<controller>
        [System.Web.Mvc.HttpGet]
        public JsonResult<object> GetLoginStatus()
        {
            bool bLoggedIn = false;

            if (User.Identity.IsAuthenticated == true)
            {
                bLoggedIn = true;
            }
            else
            {
                bLoggedIn = false;
            }
            object loggedIn = bLoggedIn;
            return Json(loggedIn);
        }

        [System.Web.Mvc.Authorize(Roles="View")]
        [System.Web.Mvc.HttpPut]
        public async Task<JsonResult<object>> getSchemeValues([FromBody] Stream stream, [FromUri]int projectId=0)
        {
            StreamReader streamReader = new StreamReader(stream);
            object data = new object();
            List<ResponseValue> responseList = new List<ResponseValue>();
            string json = streamReader.ReadToEnd();
            if (json != "[]" || json != null || json != "")
            {
                List<SchemeValue> list = new JavaScriptSerializer().Deserialize<List<SchemeValue>>(json);
                if (list.Count != 0)
                {
                    NewSchemesHandler schemesHandler = new NewSchemesHandler();
                    //data = await schemesHandler.getSnapshotData(responseList, list, projectId);
                }
                return Json(data);
            }
            else
            {
                return Json(data);
            }

        }
        
        [System.Web.Mvc.Authorize(Roles = "View")]
        [System.Web.Mvc.HttpPut]
        public async Task<JsonResult<object>> getSchemeSnapshot([FromBody] Stream stream, [FromUri]int projectId, string schmeName)
        {
            StreamReader streamReader = new StreamReader(stream);
            object data = new object();
            List<ResponseValue> values = new List<ResponseValue>();
            string json = streamReader.ReadToEnd();
            if (json != "[]" || json != null || json != "")
            {
                List<SchemeValue> list = new JavaScriptSerializer().Deserialize<List<SchemeValue>>(json);
                if (list.Count != 0)
                {
                    NewSchemesHandler schemesHandler = new NewSchemesHandler();
                    //data = await schemesHandler.putSnapshotDataIntoFile(values, list, projectId);
                }
                return Json(data);
            }
            else
            {
                return Json(data);
            }

        }
    }
}