using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDiosna.Models;
using System.Web.Mvc;
using System.Web.Security;
using UsersDiosna.CMS.Models;
using UsersDiosna.Handlers;

namespace UsersDiosna.Controllers
{
    [Authorize(Roles = "CMS")]
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
            CMSHandler CMSH = new CMSHandler();
            //Get dropdowns lists of roles and bakery ids
            addmodel.Ids = CMSH.GetDropDownListBakeryIDs(int.Parse(Session["id"].ToString()));
            addmodel.Roles = CMSH.GetDropDownListRoles();
                                    
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
            sectionModel.Roles = CMSH.GetDropDownListRoles(sectionToEdit.Role);

            return View(sectionModel);
        }

        // POST: CMS/EditSection/5
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
        public ActionResult DeleteSection(int id)
        {
            return View();
        }

        // POST: CMS/DeleteSection/5
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
        public ActionResult CreateArticle()
        {
            CMSHandler CMSH = new CMSHandler();
            ArticleModel addmodel = new ArticleModel();
            
            CMSDataContext db = new CMSDataContext();
            addmodel.Ids = CMSH.GetDropDownListBakeryIDs(int.Parse(Session["id"].ToString()));

            
            addmodel.Sections = CMSH.GetDropDownListSections(int.Parse(Session["id"].ToString()));
            return View(addmodel);
        }

        // POST: CMS/CreateArticle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(ArticleModel collection)
        {
            CMSDataContext db = new CMSDataContext();
            Article article = new Article();
            //section data into object form form
            article.bakeryId = collection.bakeryId;
            article.DateTime = DateTime.Now;
            article.Author = User.Identity.Name;
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
            articleModel.Header = articleToEdit.Header;
            articleModel.Text = articleToEdit.Text;
            articleModel.Amount = articleToEdit.Amount;
            articleModel.HoursSpend = articleToEdit.HoursSpend;
            articleModel.Attachment = articleToEdit.Attachment;
            articleModel.Description = articleToEdit.Description;

            //getdropdown list and select article's section name as a default
            articleModel.Sections = CMSH.GetDropDownListSections(int.Parse(Session["id"].ToString()), articleToEdit.Section.Name);

            return View(articleModel);
        }

        // POST: CMS/EditArticle/5
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
            article.DateTime = DateTime.Now;
            article.Header = collection.Header;
            article.Text = collection.Text;
            article.Amount = collection.Amount;
            article.HoursSpend = collection.HoursSpend;
            article.Attachment = collection.Attachment;
            article.Description = collection.Description;

            //save the data
            db.SubmitChanges();

            return RedirectToAction("Index");
        }

        // GET: CMS/DeleteArticle/5
        public ActionResult DeleteArticle (int id)
        {
            return View();
        }

        // POST: CMS/DeleteArticle/5
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
