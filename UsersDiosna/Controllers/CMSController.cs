using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDiosna.Models;
using System.Web.Mvc;
using System.Web.Security;
using UsersDiosna.CMS.Models;

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
            SectionModel addmodel = new SectionModel();
            Handlers.AddRoleDataContext db = new Handlers.AddRoleDataContext();
            addmodel.Ids = new List<SelectListItem>();
            addmodel.Roles = new List<SelectListItem>();
            int bakeryId;
            bool first = true;
            foreach (string role in Roles.GetAllRoles()) {
                SelectListItem roleItem = new SelectListItem();
                if (first == true) {
                    roleItem.Selected = true;
                }
                roleItem.Value = role;
                string roleDescription = db.aspnet_Roles.Single(p => p.RoleName == role.ToString()).Description;
                if (roleDescription != null) {
                    roleItem.Text = roleDescription;
                }
                else {
                    roleItem.Text = role;
                }
                //For bakery id list
                if (int.TryParse(role, out bakeryId)) {
                    SelectListItem id = new SelectListItem();
                    if (bakeryId == 10000) {
                        id.Selected = true;
                    }
                    
                    id.Value = bakeryId.ToString();
                    if (roleDescription != null) {
                        id.Text = roleDescription;
                    }
                    else {
                        id.Text = bakeryId.ToString();
                    }
                    addmodel.Ids.Add(id);

                }
                addmodel.Roles.Add(roleItem);
                first=false;
            }
                        
            return View(addmodel);
        }
        
        // POST: CMS/CreateSection
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSection(SectionModel collection)
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
            int bakeryId;
            ArticleModel addmodel = new ArticleModel();
            Handlers.AddRoleDataContext addRole = new Handlers.AddRoleDataContext();
            CMSDataContext db = new CMSDataContext();
            addmodel.Ids = new List<SelectListItem>();
            foreach (string role in Roles.GetAllRoles()) {
                string roleDescription = addRole.aspnet_Roles.Single(p => p.RoleName == role.ToString()).Description;

                if (int.TryParse(role, out bakeryId))
                {
                    SelectListItem id = new SelectListItem();
                    if (bakeryId == 10000)
                    {
                        id.Selected = true;
                    }

                    id.Value = bakeryId.ToString();
                    if (roleDescription != null)
                    {
                        id.Text = roleDescription;
                    }
                    else
                    {
                        id.Text = bakeryId.ToString();
                    }
                    addmodel.Ids.Add(id);

                }
            }
            foreach (Section section in db.Sections) {
                if (Roles.IsUserInRole(section.Role) && section.BakeryId == int.Parse(Session["id"].ToString())) {
                    SelectListItem item = new SelectListItem();
                    item.Value = section.Id.ToString();
                    item.Text = section.Name;
                }
            }
            return View(addmodel);
        }

        // POST: CMS/CreateArticle
        [HttpPost]
        public ActionResult AddArticle(ArticleModel collection)
        {
            CMSDataContext db = new CMSDataContext();
            Article article = new Article();
            //section data into object form form
            article.bakeryId = collection.bakeryId;
            article.Header = collection.Header;
            article.Text = collection.Text;
            article.Amount = collection.Amount;
            article.HoursSpend = collection.HoursSpend;
            article.Description = collection.Description;
            article.SectionId = collection.SectionId;
            //Add section into db
            db.Articles.InsertOnSubmit(article);
            db.SubmitChanges();

            return RedirectToAction("Index");
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
