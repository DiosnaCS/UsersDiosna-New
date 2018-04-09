using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace UsersDiosna.Controllers
{
    public class MobileApiController : ApiController
    {
        // GET api/<controller>
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/


        // GET api/<controller>
        [System.Web.Mvc.HttpGet]
        public JsonResult<object> GetLoginStatus()
        {
            bool bLoggedIn = false;
            
            if (User.Identity.IsAuthenticated == true) {
                bLoggedIn = true;
            }
            else
            {
                bLoggedIn = false;
            }
            object loggedIn = bLoggedIn;
            return Json(loggedIn);
        }
        /*
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