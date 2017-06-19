using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDiosna.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace UsersDiosna.Controllers
{
    public class CMSController : Controller
    {
        // GET: CMS
        public ActionResult Index()
        {
            CMSDataContext db = new CMSDataContext();
            List<Article> ArticleData = new List<Article>();
            List<Section> SectionData = new List<Section>();
            foreach (string role in Roles.GetRolesForUser(User.Identity.Name)) {
                SectionData.AddRange(db.Sections.Where(p => p.Role == role).ToList());
            }
            if (SectionData != null)
            {
                foreach (Section section in SectionData)
                {
                     ArticleData.AddRange(db.Articles.Where(p => (p.bakeryId == int.Parse(Session["id"].ToString())) && p.SectionId == section.Id).ToList());
                }
            }
            return View(ArticleData);
        }
        #region section
        // GET: CMS/CreateSection/
        public ActionResult CreateSection()
        {
            CMS.Models.AddSection addmodel = new CMS.Models.AddSection();
            foreach (string role in Roles.GetAllRoles()) {
                if (int.TryParse(role, out int bakeryId)) {
                    addmodel.Ids.
                }
            }
            
            
            return View();
        }
        
        // POST: CMS/CreateSection
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSection(CMS.Models.AddSection collection)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                Section section = new Section();
                //section data into object form form
                section.Name = collection.Name;
                section.BakeryId = collection.BakeryId;
                section.Role = collection.Role;
                section.Description = collection.Description;
                //Add section into db
                db.Sections.InsertOnSubmit(section);
                db.SubmitChanges();                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CMS/EditSection/5
        public ActionResult EditSection(int id)
        {
            return View();
        }

        // POST: CMS/EditSection/5
        [HttpPost]
        public ActionResult EditSection(int id, FormCollection collection)
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

        // GET: CMS/DeleteSection/5
        public ActionResult DeleteSection(int id)
        {
            return View();
        }

        // POST: CMS/DeleteSection/5
        [HttpPost]
        public ActionResult DeleteSection(int id, FormCollection collection)
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
        #endregion
        #region articles
        // GET: CMS/DetailArticle/5
        public ActionResult DetailArticle(int id)
        {
            return View();
        }

        // GET: CMS/Create
        public ActionResult CreateArticle()
        {
            return View();
        }

        // POST: CMS/CreateArticle
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

        // GET: CMS/EditArticle/5
        public ActionResult EditArticle(int id)
        {
            return View();
        }

        // POST: CMS/EditArticle/5
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

        // GET: CMS/DeleteArticle/5
        public ActionResult DeleteArticle (int id)
        {
            return View();
        }

        // POST: CMS/DeleteArticle/5
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
        #endregion
    }
}
