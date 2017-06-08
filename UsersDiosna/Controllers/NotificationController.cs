using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UsersDiosna.Controllers
{
    public class NotificationController : AsyncController
    {
        public List<Notification> ActiveNotifications { get; set; }
        public static void Add(string definition, string projectName, string userName, int bakeryID, int type,string tags=null,string tables=null)
        {
            NotificationDataContext db = new NotificationDataContext(); // Prepare Database context
            Notification notification = new Notification();
            notification.ProjectName = projectName;
            notification.PlcID = 1; // for this moment static
            notification.Owner = userName;
            notification.BakeryID = bakeryID;
            notification.Type = type; //1 - for alarm type notification
            if (tables != null) {
                notification.Tables = tables;
            }
            if (tags != null) {
                notification.Tags = tags;
            }
            notification.Definition = definition;
            notification.Active = true;
            notification.TimestampCreated = DateTime.Now;

            //Add new notification to database
            db.Notifications.InsertOnSubmit(notification);

            //Save changes to Database.
            db.SubmitChanges();
        }
        // GET: Notification
        /// <summary>
        /// Notifiacations configuration via form
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            NotificationDataContext db = new NotificationDataContext();
            List<Notification> data = db.Notifications.Where(p => p.Owner.Contains(User.Identity.Name)).ToList();
            return View(data);
        }

        public RedirectToRouteResult Delete(int id)
        {
            NotificationDataContext db = new NotificationDataContext();
            Notification notification = db.Notifications.Single(p => p.Id == id && p.Owner.Contains(User.Identity.Name));
            db.Notifications.DeleteOnSubmit(notification);
            db.SubmitChanges();
            Session["success"] = "Notification with id " + id + " has been successfully deleted";
            return RedirectToAction("Index", "Notification");
        }
        public ActionResult Edit(int id) {
            NotificationDataContext db = new NotificationDataContext();
            Notification notification = db.Notifications.Single(p => p.Id == id);
            return View(notification);
        }
        [HttpPost]
        public RedirectToRouteResult Edit(Notification model)
        {
            NotificationDataContext db = new NotificationDataContext();
            Notification editNotif = db.Notifications.Single(p => p.Id == model.Id);
            db.Notifications.DeleteOnSubmit(editNotif);
            db.Notifications.InsertOnSubmit(model);
            db.SubmitChanges();
            Session["success"] = "Notification with id " + model.Id + " has been successfully Edited";
            return RedirectToAction("Index", "Notification");
        }
        /// <summary>
        /// get notifications 
        /// </summary>
        /// <param name="ActiveNotifications"></param>
        /// <returns></returns>
        public async Task<JsonResult> getNotifications(List<Notification> ActiveNotifications = null)
        {
            List<Notification> resultNotifications = new List<Notification>();
            NotificationDataContext db = new NotificationDataContext();
            if (ActiveNotifications == null)
            {
                ActiveNotifications = db.Notifications.Where(p => p.Owner.Contains(User.Identity.Name)).ToList();
            }
            foreach (Notification notification in ActiveNotifications)
            {
                if (notification.Active == true)
                {
                    switch (notification.Type)
                    { //In the future here will come next type for other type of notifications
                        case 1:
                            AlarmNotificationController ANC = new AlarmNotificationController();
                            Notification Anotif = new Notification();
                            Anotif = await AlarmNotificationHandler.getNotifcations(notification);
                            if (Anotif.Status == 1)
                            {
                                resultNotifications.Add(Anotif);
                            }
                            break;
                        case 2:
                            GraphNotificationController GNC = new GraphNotificationController();
                            Notification Gnotif = new Notification();
                            Gnotif = await GraphNotificationHandler.getNotifcations(notification);
                            if (Gnotif.Status == 1)
                            {
                                resultNotifications.Add(Gnotif);
                            }
                            break;
                    }
                }
            }
            return Json(resultNotifications, "application/json", JsonRequestBehavior.AllowGet);
        }
        public RedirectToRouteResult turnOff(int id) {
            NotificationDataContext db = new NotificationDataContext();
            Notification notification = db.Notifications.Single(p => p.Id == id && p.Owner.Contains(User.Identity.Name));
            notification.Occurred = null;
            notification.Tables = null;
            notification.Tags = null;
            notification.Detail = null;
            notification.Status = 2;
            notification.TimestampCreated = DateTime.Now;
            db.SubmitChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}