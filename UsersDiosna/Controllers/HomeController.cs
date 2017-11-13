using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Configuration;

namespace UsersDiosna.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                string name = User.Identity.Name;
                XMLController XC = new XMLController();
                string[] existingRolesForUser = Roles.GetRolesForUser();
                List<int> Numbers = XC.GetAllConfigsProjectNumbers(existingRolesForUser);
                ViewBag.Numbers = Numbers;
                ViewBag.Count = Numbers.Count();
                List<string> Texts = XC.GetAllConfigsNames(existingRolesForUser);
                ViewBag.Text = Texts;
                ViewBag.menuDisable = true;

                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult News()
        {
            return View();
        }
        public ActionResult Compatibility()
        {
            return View();
        }

        [Authorize]
        public ActionResult GuestZone()
        {
            ViewBag.menuDisable = true;
            return View();
            #region old
            /*
            int id;
            int projectsCount = 0;

            foreach (String role in existingRolesForUser)
            {
                //if (Session["types"] != null) {
                  //  return RedirectToAction("Index", "Menu");
                //}
                if ((Int32.TryParse(role, out id)) == true)
                {
                    Session.Add("id", id);
                    projectsCount++;
                }
                if (projectsCount >= 2)
                {
                    String some = User.Identity.Name.ToString();
                    ViewBag.some = some;

                    XMLController XC = new XMLController();

                    List<int> Numbers = XC.GetAllConfigsProjectNumbers(existingRolesForUser);
                    ViewBag.Numbers = Numbers;
                    ViewBag.Count = Numbers.Count();
                    List<string> Texts = XC.GetAllConfigsNames(existingRolesForUser);
                    ViewBag.Text = Texts;

                    ViewBag.menuDisable = true;

                    return View();
                }
            }
            if (projectsCount <= 1)
            {
                return RedirectToAction("Index", "Menu");
            }
            return RedirectToAction("Index", "Menu");
            */
            #endregion
        }
    }
}