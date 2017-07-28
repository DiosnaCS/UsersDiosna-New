using System;
using System.Net;
using System.Web.Mvc;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "View")]
    public class SchemeController : Controller
    {
        // GET: Scheme
        //[Authorize]
        public ActionResult Index()
        {
            string name = Request.QueryString["name"];
            string plc = Request.QueryString["plc"];
            foreach (String key in Session.Keys) {
                if (key.Contains(name+plc)) {
                    ViewBag.url = Session[key];
                }
            }

            Session["SchemeURLImage"] = ViewBag.url;
            ViewBag.name = name;
            return View();
        }

        public void getImage() {
            if (Session["SchemeURLImage"] != null)
            {
                try {
                    string url = Session["SchemeURLImage"].ToString();

                    WebClient client = new WebClient();
                    byte[] data = client.DownloadData(url);
                    Response.BinaryWrite(data);
                    Response.ContentType = "image/png";
                }
                catch (Exception e){
                    Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                    Session["tempforview"] = Error.timestamp + "   Error " + Error.id.ToString() + " occured so please try it again after some time"; //To screen also with id 
                }
            }
        }
    }
}