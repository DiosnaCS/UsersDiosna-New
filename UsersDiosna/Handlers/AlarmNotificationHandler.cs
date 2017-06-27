using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsersDiosna.Controllers;

namespace UsersDiosna
{
    public class AlarmNotificationHandler
    {
        /// <summary>
        /// Will check alarms 
        /// </summary>
        /// <param name="ActiveNotif">current notification</param>
        /// <returns></returns>
        public static async Task<Notification> getNotifcations(Notification ActiveNotif)
        {
            if (ActiveNotif.Tables == null || ActiveNotif.Definition == null || ActiveNotif.Tags == null || ActiveNotif.BakeryID == 0)
                return null;
            string where = "";
            string definition = ActiveNotif.Definition;
            List<alarm> happenedAlarms = new List<alarm>();

            string[] alarmsIds = definition.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries); //Initial separators are enough
            foreach (string s in alarmsIds)
            {
                where += "alarm_id=" + s + " OR ";
            }
            where = where.Substring(0, where.Length - 3);
            //Find dbName
            List<string> dbNames = XMLHandler.readTag("dbName", ActiveNotif.BakeryID);
            int FrompkTime = DateTimetTopkTime(ActiveNotif.TimestampCreated);
            List<alarm> alarms = await SelectAlarms(dbNames[ActiveNotif.PlcID - 1], where, FrompkTime);
            if (alarms != null)
            {
                foreach (alarm a in alarms)
                {
                    if (!happenedAlarms.Exists(p => p.id == a.id))
                    {
                        ActiveNotif.Occurred = DateTime.Now;
                        ActiveNotif.Detail += "\n Alarm " + a.id + " has occurred: " + a.title;
                        ActiveNotif.Status = 1; //This status shows that notification became
                        happenedAlarms.Add(a);
                    }
                }
                return ActiveNotif;
            }
            else
            {
                return null;
            }
        }
        #region Alarms_Helpers

        public static int DateTimetTopkTime(DateTime DT)
        {
            long preResult = (DT.Ticks - (630836424000000000 - 13608000000000)) / 10000000;
            int result = (int)preResult;
            return result;
        }

        public struct alarm_texts
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        public static string pkTimeToDateTime(long timeForFormat)
        {
            long timeInNanoSeconds = (timeForFormat * 10000000);
            //TimeSpan converted = TimeSpan.FromSeconds(timeForFormat);
            DateTime DateTime = new DateTime(((630836424000000000 - 13608000000000) + timeInNanoSeconds));
            //DateTime DateTime = new DateTime(years, months, days, hours, minutes, seconds);
            CultureInfo cultureInfo = new CultureInfo("en-US");
            string sDateTime = DateTime.ToString("yyyy-MM-dd h:mm tt", cultureInfo);
            return sDateTime;
        }

        /// <summary>
        /// Select alarms texts and ids
        /// </summary>
        /// <param name="alarms">Select alarms which you defined</param>
        /// <returns></returns>
        public static List<alarm_texts> SelectAlarmsTexts(string DB, List<int> alarms = null)
        {
            NpgsqlCommand cmd;
            string whereIds = "";
            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "192.168.2.12", 5432, "postgres", "Nordit0276", DB);
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            if (alarms != null)
            {
                foreach (int id in alarms)
                {
                    whereIds += "id=" + id.ToString() + " OR ";
                }
                // Execute the query and obtain a result set                
                cmd = new NpgsqlCommand("SELECT title,alarm_id FROM alarm_texts WHERE (lang='en' AND plc_id=1) AND (", conn);
            }
            else
            {
                // Execute the query and obtain a result set                
                cmd = new NpgsqlCommand("SELECT title,alarm_id FROM alarm_texts WHERE lang='en' AND plc_id=1", conn);
            }
            //Prepare DataReader
            NpgsqlDataReader dataReader = cmd.ExecuteReader();
            List<alarm_texts> alarmList = new List<alarm_texts>();
            //Cycle reads data from result set 
            while (dataReader.Read())
            {
                alarm_texts alarm = new alarm_texts();
                alarm.title = dataReader["title"].ToString();
                alarm.id = Int16.Parse(dataReader["alarm_id"].ToString());
                alarmList.Add(alarm);
            }
            //We need to close connection to select texts
            //cmd.Dispose();
            conn.Close();
            return alarmList;
        }
        public struct alarm
        {
            public int id { get; set; }
            public string title { get; set; }

            public string originTime { get; set; } //JS need that with special format
            public string expiryTime { get; set; }
        }
        public static async Task<List<alarm>> SelectAlarms(string DB, string where, int from)
        {
            int i = 0;
            List<alarm> alarms = new List<alarm>();
            List<alarm_texts> titles = SelectAlarmsTexts(DB);
            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "192.168.2.12", 5432, "postgres", "Nordit0276", DB);
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            string sql = "SELECT * FROM alarm_history WHERE origin_pktime>" + from + " AND (" + where + ") ORDER BY origin_pktime DESC";
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            //Prepare DataReader
            System.Data.Common.DbDataReader dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                alarm alarm = new alarm();
                alarm.id = short.Parse(dr["alarm_id"].ToString());
                int id = alarm.id;
                //small improvment beacause alarm_id in table alarm_texts and alarm_id in table alarm_history are bind
                alarm.title = titles[id].title;

                int originTime = int.Parse(dr["origin_pktime"].ToString());
                alarm.originTime = pkTimeToDateTime(originTime);
                int expTime = Int32.Parse(dr["expiry_pktime"].ToString());
                alarm.expiryTime = pkTimeToDateTime(expTime);
                alarms.Add(alarm);
                i++;
            }
            conn.Close();
            return alarms;
        }
        #endregion
    }
}