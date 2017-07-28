using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UsersDiosna.Controllers
{
    public class MenuController : Controller
    {
        /*
        // GET: Menu\
        [Authorize]
        public ActionResult Index()
        {
            int i = 0;
            getMenu();


            int id = 00000;
            db db = new db();
            object s = db.singleItemSelect("DefaultView", "AspNetUsers", "UserName = '" + User.Identity.Name.ToString() + "'");
            string name = s.ToString();
            foreach (String key in Session.Keys)
            {
                if (key.Contains(name))
                {
                    ViewBag.url = Session[key];
                    i++;
                }
            }

            Session["SchemeURLImage"] = ViewBag.url;
            XMLController XC = new XMLController();
            if ((Int32.TryParse(Session["id"].ToString(), out id)) == true)
            {
                ViewBag.ProjectName = XC.readNodeXML("name", id);
            }
            
            return View();
        }
        */
        [Authorize]
        public ActionResult Index(int id)
        {
            bool bMenu = getMenu(id);
            if (!bMenu)
                return RedirectToAction("Login", "Account");
            NotificationDataContext db = new NotificationDataContext();
            List<Notification> data = db.Notifications.Where(p => p.Owner.Contains(User.Identity.Name) && p.BakeryID == id).ToList();
            return View(data);
        }

        /*
         * @param void, @return redirect 
         * Method to get Menu o
         * to Menu on Index view method
         */
        public bool getMenu(int id = 0)
        {
            String preId;
            if (id == 0)
            {

                if (Request.QueryString["id"] != null)
                {
                    preId = Request.QueryString["id"].ToString();
                    if (User.IsInRole(preId))
                    {
                        id = Int32.Parse(preId);
                    }
                    else
                    {
                        return false;//RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    if ((Int32.TryParse(Session["id"].ToString(), out id)) == true)
                    {
                        //out int id = int id 
                    }
                    else
                    {
                        return true;//RedirectToAction("Index", "Home");
                    }
                }
            }
            

            XMLController XC = new XMLController();
            List<String> values = XC.readXML("plc", id);
            List<String> items = XC.readNodesNameXML("plc", id, 3);
            List<String> plc = XC.readNodesNameXML("plc", id, 1);
            List<String> names = XC.readNodesNameXML("plc", id, 2);
            List<String> types = XC.XMLgetTypes("plc", id);
            string ProjectName = XC.readNodeXML("name", id);
            int i = 0;
            foreach (String value in values)
            {
                //String name = names[i] + items[i];
                Session.Add(items[i], value);
                i++;
            }
            Session.Add("values", values);

            Session.Add("id", id);
            Session.Add("names", names);
            Session.Add("plc", plc);
            Session.Add("types", types);
            Session.Add("ProjectName", ProjectName);

            return true;//return RedirectToAction("Index", "Menu");
        }
    }
}