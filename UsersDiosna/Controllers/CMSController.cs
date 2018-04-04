using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDiosna.Models;
using System.Web.Mvc;
using System.Web.Security;
using UsersDiosna.CMS.Models;
using UsersDiosna.Handlers;
using UsersDiosna.DBML;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "View")]
    public class CMSController : Controller
    {
        // GET: CMS
        public ActionResult Index()
        {
            try
            {
                string path = "";
                string sessionID = "pathDownload" + Request.QueryString["plc"];
                foreach (string sessionKey in Session.Keys)
                {
                    if(sessionKey == sessionID)
                    {
                        path = Session[sessionID].ToString();
                    }
                }
                if (path != "")
                {
                    Session["network_path"] = path;
                }
                CMSDataContext db = new CMSDataContext();
                List<Article> ArticleData = new List<Article>();
                List<Section> SectionData = new List<Section>();
                foreach (string role in Roles.GetRolesForUser(User.Identity.Name))
                {
                    SectionData.AddRange(db.Sections.Where(p => p.Role == role && p.BakeryId == int.Parse(Session["id"].ToString())).ToList());
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
            catch (Exception e)
            {
                Error.toFile(e.Message + e.Source + e.StackTrace + " Something went wrong in the inxdex of articles" + e.Data + e.InnerException, this.GetType().Name.ToString());
                Session["tempforview"] = "Something went wrong in the inxdex of rticles";
                return Redirect("/Account/Login");
            }
        }

        #region section
        // GET: CMS/DetailSection/5
        public ActionResult DetailSection(int id)
        {
            CMSDataContext db = new CMSDataContext();
            Section sectionDetail = db.Sections.Single(p => p.Id == id);
            if (!(Roles.IsUserInRole(sectionDetail.Role)))
            {
                Session["tempforview"] = "You dont have a permission to view this article";
                return RedirectToAction("Login", "Account");
            }
            return View(sectionDetail);
        }
        public ActionResult Sections() {
            CMSDataContext db = new CMSDataContext();
            List<Section> SectionData = new List<Section>();
            foreach (string role in Roles.GetRolesForUser(User.Identity.Name)) {
                SectionData.AddRange(db.Sections.Where(p => p.Role == role && p.BakeryId == int.Parse(Session["id"].ToString())).ToList());
            }
            return View(SectionData);
        }
        public JsonResult FilterSection(string id)
        {
            CMSDataContext db = new CMSDataContext();
            if (id == null)
                return null;
            List<Section> sections = db.Sections.Where(p => p.Name.Contains(id) || p.Description.Contains(id)).ToList();
            if (sections.Count == 0)
                return null;
            List<SectionJSON> results = new List<SectionJSON>();
            foreach (Section section in sections) {
                SectionJSON sectionJSON = new SectionJSON();
                sectionJSON.Name = section.Name;
                sectionJSON.Description = section.Description;
                sectionJSON.ArticleId = section.ArticleId;
                sectionJSON.Role = section.Role;
                sectionJSON.BakeryId = section.BakeryId;
                results.Add(sectionJSON);
            }
            //var results = from se
            return Json(results);
        }

        // GET: CMS/CreateSection/
        [Authorize(Roles = "CMS")]
        public ActionResult CreateSection()
        {
            SectionModel addmodel = new SectionModel();
            CMSHandler CMSH = new CMSHandler();
            //Get dropdowns lists of roles and bakery ids
            addmodel.Ids = CMSH.getDropDownListBakeryIDs(int.Parse(Session["id"].ToString()));
            addmodel.Roles = CMSH.getDropDownListRoles();
                                    
            return View(addmodel);
        }

        // POST: CMS/CreateSection
        [Authorize(Roles = "CMS")]
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
        [Authorize(Roles = "CMS")]
        public ActionResult EditSection(int id)
        {
            CMSHandler CMSH = new CMSHandler();
            CMSDataContext db = new CMSDataContext();

            //Get the only one article to edit
            Section sectionToEdit = db.Sections.Single(p => p.Id == id);
            if (!(Roles.IsUserInRole(sectionToEdit.Role)))
            {
                Session["tempforview"] = "You dont have a permission to edit this article";
                return RedirectToAction("Login", "Account");
            }
            //prepare data from db
            SectionModel sectionModel = new SectionModel();
            sectionModel.BakeryId = sectionToEdit.BakeryId;
            sectionModel.Name = sectionToEdit.Name;
            sectionModel.Description = sectionToEdit.Description;

            //getdropdown list of roles and select previous role as a default
            sectionModel.Ids = CMSH.getDropDownListBakeryIDs(int.Parse(Session["id"].ToString()));
            sectionModel.Roles = CMSH.getDropDownListRoles(sectionToEdit.Role);

            return View(sectionModel);
        }

        // POST: CMS/EditSection/5
        [Authorize(Roles = "CMS")]
        [HttpPost]
        public ActionResult EditSection(int id, SectionModel collection)
        {
            CMSHandler CMSH = new CMSHandler();
            CMSDataContext db = new CMSDataContext();

            //Get the only one article to edit
            Section sectionToEdit = db.Sections.Single(p => p.Id == id);
            if (!(Roles.IsUserInRole(sectionToEdit.Role)))
            {
                Session["tempforview"] = "You dont have a permission to edit this article";
                return RedirectToAction("Login", "Account");
            }
            sectionToEdit.BakeryId = collection.BakeryId;
            sectionToEdit.Name = collection.Name;
            sectionToEdit.Description = collection.Description;
            sectionToEdit.Role = collection.Role;

            db.SubmitChanges();

            return RedirectToAction("Index");
           
        }

        // GET: CMS/DeleteSection/5
        [Authorize(Roles = "CMS")]
        public ActionResult DeleteSection(int id)
        {
            return View();
        }

        // POST: CMS/DeleteSection/5
        [Authorize(Roles = "CMS")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSection(int id, SectionModel collection)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                //select article
                Section section = db.Sections.Single(p => p.Id == id);
                if (!(Roles.IsUserInRole(section.Role)))
                {
                    Session["tempforview"] = "You dont have a permission to delete this article";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    //delete the article
                    db.Sections.DeleteOnSubmit(section);
                    db.SubmitChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
        #region articles
        public JsonResult FilterArticle(string id)
        {
            string what = id;
            CMSDataContext db = new CMSDataContext();
            List<Article> articles = db.Articles.Where(q => q.Text.Contains(what) || q.Header.Contains(what) || q.Description.Contains(what)).ToList();
            return Json(articles);
        }
        // GET: CMS/DetailArticle/5
        public ActionResult DetailArticle(int id)
        {
            CMSDataContext db = new CMSDataContext();
            Article articleDetail = db.Articles.Single(p => p.Id == id);
            if (!(Roles.IsUserInRole(articleDetail.Section.Role)))
            {
                Session["tempforview"] = "You dont have a permission to view this article";
                return RedirectToAction("Login", "Account");
            }
            return View(articleDetail);
        }

        // GET: CMS/Create
        [Authorize(Roles = "CMS")]
        public ActionResult CreateArticle()
        {
            CMSHandler CMSH = new CMSHandler();
            ArticleModel addmodel = new ArticleModel();
            
            CMSDataContext db = new CMSDataContext();
            List<SelectListItem> files = new List<SelectListItem>();
            //Gets the files to attach from downloads when they are present
            if (Session["network_path"] != null)
            {
               // files = CMSH.getDropDownListFiles((int)Session["id"], Session["network_path"].ToString(), Roles.GetRolesForUser());
            }
            ViewBag.FilesToAttach = files;

            addmodel.Ids = CMSH.getDropDownListBakeryIDs(int.Parse(Session["id"].ToString()));
            addmodel.Sections = CMSH.getDropDownListSections(int.Parse(Session["id"].ToString()));
            return View(addmodel);
        }

        // POST: CMS/CreateArticle
        [Authorize(Roles = "CMS")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(ArticleModel collection)
        {
            CMSDataContext db = new CMSDataContext();
            Article article = new Article();
            //section data into object form form
            article.bakeryId = collection.bakeryId;
            article.DateTime = DateTime.Now;
            article.DateTimeOrigin = collection.Date;
            article.Author = User.Identity.Name;
            article.Header = collection.Header;
            article.Text = collection.Text;
            article.Amount = collection.Amount;
            article.HoursSpend = collection.HoursSpend;
            article.Attachment = collection.Attachment;
            article.Description = collection.Description;
            article.SectionId = collection.SectionId;
            //Add section into db
            db.Articles.InsertOnSubmit(article);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }

        // GET: CMS/EditArticle/5
        [Authorize(Roles = "CMS")]
        public ActionResult EditArticle(int id)
        {
            CMSHandler CMSH = new CMSHandler();
            CMSDataContext db = new CMSDataContext();
            
            //Get the only one article to edit
            Article articleToEdit = db.Articles.Single(p => p.Id == id);
            if (!(Roles.IsUserInRole(articleToEdit.Section.Role)))
            {
                Session["tempforview"] = "You dont have a permission to edit this article";
                return RedirectToAction("Login", "Account");
            }
            //prepare data from db
            ArticleModel articleModel = new ArticleModel();
            articleModel.bakeryId = articleToEdit.bakeryId;
            articleModel.Date= articleToEdit.DateTimeOrigin;
            articleModel.Header = articleToEdit.Header;
            articleModel.Text = articleToEdit.Text;
            articleModel.Amount = articleToEdit.Amount;
            articleModel.HoursSpend = articleToEdit.HoursSpend;
            articleModel.Attachment = articleToEdit.Attachment;
            articleModel.Description = articleToEdit.Description;

            //getdropdown list and select article's section name as a default
            articleModel.Ids = CMSH.getDropDownListBakeryIDs(int.Parse(Session["id"].ToString()));
            articleModel.Sections = CMSH.getDropDownListSections(int.Parse(Session["id"].ToString()), articleToEdit.Section.Name);

            return View(articleModel);
        }

        // POST: CMS/EditArticle/5
        [Authorize(Roles = "CMS")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(int id, ArticleModel collection)
        {
            CMSDataContext db = new CMSDataContext();
            //Get the only one article to edit
            Article article = db.Articles.Single(p => p.Id == id);
            //test the security
            if (!(Roles.IsUserInRole(article.Section.Role)))
            {
                Session["tempforview"] = "You dont have a permission to edit this article";
                return RedirectToAction("Login", "Account");
            }
            //Change data from model and prepare them for save
            article.bakeryId = collection.bakeryId;
            article.DateTimeOrigin = collection.Date;
            article.Header = collection.Header;
            article.Text = collection.Text;
            article.Amount = collection.Amount;
            article.HoursSpend = collection.HoursSpend;
            article.Attachment = collection.Attachment;
            article.Description = collection.Description;
            Section section = db.Sections.Single(p => p.Id == collection.SectionId);
            article.Section = section;

            //save the data
            db.SubmitChanges();

            return RedirectToAction("Index");
        }

        // GET: CMS/DeleteArticle/5
        [Authorize(Roles = "CMS")]
        public ActionResult DeleteArticle (int id)
        {
            return View();
        }

        // POST: CMS/DeleteArticle/5
        [Authorize(Roles = "CMS")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArticle(int id, FormCollection collection)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                //select article
                Article article = db.Articles.Single(p => p.Id == id);
                if (!(Roles.IsUserInRole(article.Section.Role)))
                {
                    Session["tempforview"] = "You dont have a permission to delete this article";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    //delete the article
                    db.Articles.DeleteOnSubmit(article);
                    db.SubmitChanges();
                }
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
