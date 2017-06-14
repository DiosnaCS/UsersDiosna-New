using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDiosna.Models;
using System.Web.Mvc;

namespace UsersDiosna.Controllers
{
    public class CMSController : Controller
    {
        // GET: CMS
        public ActionResult Index()
        {
            CMSDataContext db = new CMSDataContext();
            List<Section> SectionData = db.Sections.Where(p => p.bakeryId == int.Parse(Session["id"].ToString())).ToList();
            foreach (Section section in SectionData)
            {
                List<Article> ArticleData = db.Articles.Where(p => p.bakeryId == int.Parse(Session["id"].ToString())).ToList();
            }
            return View(SectionData);
        }

        // GET: CMS/Details/5
        public ActionResult ArticleDetails(int id)
        {
            return View();
        }

        // GET: CMS/Create
        public ActionResult CreateArticle()
        {
            return View();
        }

        // POST: CMS/Create
        [HttpPost]
        public ActionResult AddArticle(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CMS/Edit/5
        public ActionResult EditArticle(int id)
        {
            return View();
        }

        // POST: CMS/Edit/5
        [HttpPost]
        public ActionResult EditArticle(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CMS/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CMS/Delete/5
        [HttpPost]
        public ActionResult DeleteArticle(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
