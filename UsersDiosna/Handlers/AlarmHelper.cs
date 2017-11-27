﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsersDiosna.Alarms.Models;

namespace UsersDiosna.Handlers
{
    public class AlarmHelper
    {
        #region Alarms_Helpers

        //public static string DB { get; set; }
        //public static string lang { get; set; }
        //public static int plcID { get; set; }

        public static int DateTimeTopkTime(DateTime DT)
        {
            long preResult = (DT.Ticks - (630836424000000000 - 13608000000000)) / 10000000;
            int result = (int)preResult;
            return result;
        }

        public struct alarm_texts
        {
            public short id { get; set; }
            public string lang { get; set; }
            public string title { get; set; }
        }

        public static DateTime pkTimeToDateTime(long timeForFormat)
        {
            long timeInNanoSeconds = (timeForFormat * 10000000);
            //TimeSpan converted = TimeSpan.FromSeconds(timeForFormat);
            DateTime DateTime = new DateTime(((630836424000000000 - 13608000000000) + timeInNanoSeconds));
            //DateTime DateTime = new DateTime(years, months, days, hours, minutes, seconds);
            //CultureInfo cultureInfo = new CultureInfo("en-US");
            //string sDateTime = DateTime.ToString("yyyy-MM-dd h:mm tt", cultureInfo);
            return DateTime;
        }

        /// <summary>
        /// Select alarms texts and ids
        /// </summary>
        /// <param name="alarms">Select alarms which you defined</param>
        /// <returns></returns>
        public List<alarm_texts> SelectAlarmsTexts(string DB, List<int> alarms = null, int plcID = 1)
        {
            NpgsqlCommand cmd;
            string whereIds = string.Empty;
            string lang = "en";
            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "192.168.2.12", 5432, "postgres", "Nordit0276", DB);
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            if (alarms != null)
            {
                foreach (int id in alarms)
                {
                    whereIds += "alarm_id=" + id.ToString() + " OR ";
                }
                whereIds = whereIds.Substring(0, whereIds.Length - 4);
                // Execute the query and obtain a result set                
                string sql = string.Format("SELECT title,lang,alarm_id FROM alarm_texts WHERE (plc_id={0} AND {1})", plcID, whereIds);
                cmd = new NpgsqlCommand(sql, conn);
            }
            else
            {
                // Execute the query and obtain a result set                
                cmd = new NpgsqlCommand(string.Format("SELECT title,lang,alarm_id FROM alarm_texts WHERE plc_id={0}",plcID),conn);
            }
            //Prepare DataReader
            NpgsqlDataReader dataReader = cmd.ExecuteReader();
            List<alarm_texts> alarmList = new List<alarm_texts>();
            //Cycle reads data from result set 
            while (dataReader.Read())
            {
                alarm_texts alarm = new alarm_texts();
                alarm.title = dataReader["title"].ToString();
                alarm.lang = dataReader["lang"].ToString();
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
            public short id { get; set; }
            public string title { get; set; }
            
            public DateTime originTime { get; set; } //JS need that with special format
            public DateTime expiryTime { get; set; }
        }
               
        /// <summary>
        /// Method to get alarms
        /// </summary>
        /// <param name="DB">db name</param>
        /// <param name="count">count of alarms that you want to select</param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<alarm>> SelectAlarms(string DB,int offsetPage = 0,int count = 0, List<int> ids = null,int plcID = 1)
        {
            int i = 0;
            string sql = string.Empty;
            string whereIds = string.Empty;
            List<alarm> alarms = new List<alarm>();

            List<alarm_texts> titles = SelectAlarmsTexts(DB, null, plcID);

            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "192.168.2.12", 5432, "postgres", "Nordit0276", DB);
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            if (ids != null)
            {
                foreach (int id in ids)
                {
                    whereIds += "alarm_id=" + id.ToString() + " OR ";
                }
                whereIds = whereIds.Substring(0, whereIds.Length - 4);
                // Execute the query and obtain a result set                
                sql = string.Format("SELECT * FROM alarm_history WHERE {0} AND plc_id={1} ORDER BY origin_pktime DESC LIMIT {2}", whereIds, plcID, count);
            }
            else
            {
                if (count != 0)
                    sql = string.Format("SELECT * FROM alarm_history WHERE plc_id={0} ORDER BY origin_pktime DESC LIMIT {1}",plcID, count);
                else
                    sql = "SELECT * FROM alarm_history ORDER BY origin_pktime DESC";
            }
            if (offsetPage > 0 && count > 0) {
                if (ids != null)
                {
                    foreach (int id in ids)
                    {
                        whereIds += "alarm_id=" + id.ToString() + " OR ";
                    }
                    // Execute the query and obtain a result set                
                    sql = string.Format("SELECT * FROM alarm_history WHERE plc_id={0} ORDER BY origin_pktime DESC LIMIT {1} OFFSET {2}", plcID, whereIds, count, offsetPage);
                }
                sql = string.Format("SELECT * FROM alarm_history WHERE plc_id={0} ORDER BY origin_pktime DESC LIMIT {0} OFFSET {1}",plcID, count, offsetPage);
            }
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            //Prepare DataReader
            System.Data.Common.DbDataReader dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                alarm alarm = new alarm();
                alarm.id = short.Parse(dr["alarm_id"].ToString());
                alarm.id++;
                int id = alarm.id;
                //small improvment beacause alarm_id in table alarm_texts and alarm_id in table alarm_history are bind
                if (titles.Exists(p => (p.id) == id))
                {
                    alarm.title = titles[id].title;
                } else
                {
                    alarm.title = "Title does not match with any of alarm id in texts and in db. DB id:" + id;
                }
                int originTime = int.Parse(dr["origin_pktime"].ToString());                  
                int expTime = Int32.Parse(dr["expiry_pktime"].ToString());
                alarm.originTime = pkTimeToDateTime(originTime);
                alarm.expiryTime = pkTimeToDateTime(expTime);
                alarms.Add(alarm);
                i++;
            }
            conn.Close();
            return alarms;
        }

        /// <summary>
        /// Method to get alarms async
        /// </summary>
        /// <param name="DB">db name</param>
        /// <param name="count">count of alarms that you want to select</param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<AlarmGraphData>> SelectAlarms(string DB, long pktimeFrom, long pkTimeTo, int plcID = 1)
        {
            int i = 0;
            string sql = string.Empty;
            string whereIds = string.Empty;
            List<AlarmGraphData> alarms = new List<AlarmGraphData>();

            List<alarm_texts> titles = SelectAlarmsTexts(DB,null, plcID);

            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
            "192.168.2.12", 5432, "postgres", "Nordit0276", DB);
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            sql = string.Format("SELECT * FROM alarm_history WHERE (origin_pktime BETWEEN {0} AND {1}) AND plc_id={2} ORDER BY origin_pktime ASC",pktimeFrom, pkTimeTo, plcID);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            //Prepare DataReader
            System.Data.Common.DbDataReader dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                AlarmGraphData alarm = new AlarmGraphData();
                alarm.id = short.Parse(dr["alarm_id"].ToString());
//                alarm.id++;
                int id = alarm.id;
                //small improvment beacause alarm_id in table alarm_texts and alarm_id in table alarm_history are bind
                if (titles.Exists(p => (p.id-1) == id))
                {
                    alarm.title = titles[id-1].title;
                }
                else
                {
                    alarm.title = "Title does not match with any of alarm id in texts and in db. DB id:" + id;
                }
                int originTime = int.Parse(dr["origin_pktime"].ToString());
                
                int expTime = Int32.Parse(dr["expiry_pktime"].ToString());
                alarm.originTime = originTime;
                alarm.expiryTime = expTime;
                alarms.Add(alarm);
                i++;
            }
            conn.Close();
            return alarms;
        }
        #endregion
    }
}