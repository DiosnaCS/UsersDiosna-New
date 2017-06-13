using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsersDiosna.Controllers;

namespace UsersDiosna
{
    public class GraphNotificationHandler
    {
        public static async Task<Notification> getNotifcations(Notification ActiveNotif)
        {
            List<string> dbNames = XMLHandler.readTag("dbName", ActiveNotif.BakeryID);
            List<tag> notifications = new List<tag>();
            string[] separator1 = new string[] { "," };
            string[] separator2 = new string[] { "AND" };
            string[] tables = ActiveNotif.Tables.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
            bool bExistsnotif = false;
            foreach (string table in tables)
            {
                string tags = null;
                string definition = null;
                List<string> definitionArray = ActiveNotif.Definition.Split(separator2, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<string> tagsArray = ActiveNotif.Tags.Split(separator2, StringSplitOptions.RemoveEmptyEntries).ToList();

                tags = string.Join(",", tagsArray.Where(p => p.Contains(table)));
                definition = string.Join("AND", definitionArray.Where(p => p.Contains(table)));
                if (tags == "" || definition == "")
                    break;
                notifications = await SelectTags(dbNames[ActiveNotif.PlcID - 1], table, tags, definition, ActiveNotif.TimestampCreated);

                if (notifications != null)
                {
                    ActiveNotif.Occurred = DateTime.Now;
                    foreach (tag tag in notifications)
                    {
                        ActiveNotif.Detail += "\n Tag reaches following value: " + tag.value + " at: " + tag.timestamp;
                    }
                    ActiveNotif.Status = 1; //This status shows that notification became
                    bExistsnotif = true; //notification is not empty

                }
            }
            if (bExistsnotif == false)
            {
                return null;
            }
            else
            {
                return ActiveNotif;
            }
        }

        public struct tag
        {
            public string column;
            public double value;
            public DateTime timestamp;
        }
        public static async Task<List<tag>> SelectTags(string DB, string table, string tags, string definition, DateTime notifCreated)
        {
            List<object[]> results = new List<object[]>();
            List<tag> forNotification = new List<tag>();
            db db = new db(DB, 12);
            string sql = "SELECT " + tags + " FROM \"" + table + "\" WHERE (\"UTC\" IN(SELECT \"UTC\" FROM \"" + table + "\" ORDER BY \"UTC\" DESC LIMIT 1)) AND " + definition;
            results = await db.multipleItemSelectPostgresAsync(sql);
            foreach (object o in results[0])
            {
                tag tag = new tag();
                tag.value = double.Parse(o.ToString());
                tag.timestamp = DateTime.Now;
                forNotification.Add(tag);
            }
            return forNotification;
        }
    }
}