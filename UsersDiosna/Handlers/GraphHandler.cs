﻿using System;
using System.Collections.Generic;
using System.Text;
using UsersDiosna.Graph.Models;
using UsersDiosna.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace UsersDiosna.Handlers
{
    public class GraphHandler
    {
        private static CIniFile config { get; set; }
        private static List<db> openDbList = new List<db>();
        private static List<DatabaseDef> dbDefList = new List<DatabaseDef>();

        #region UTC
        public static string pkTimeToUTC(double time)
        {
            double utcTime = (time / 86400) + 2451544.5;
            string utc = utcTime.ToString();
            if (utc.Contains(","))
            {
                utc = utc.Replace(",", ".");
            }
            return utc;
        }
        public long utcToPkTime(string time)
        {
            double utcTime;
            //zakrácení času v utc na fixní délku 'od ":" až do konce odmažeme'
            if (time.IndexOf(":") >= 0)
            {
                int idx = time.IndexOf(":");
                time = time.Substring(0, idx - 1);
            }
            if (time.Contains(".")) {
                time = time.Replace(".", ",");
            }
            try
            {
                utcTime = double.Parse(time);
            }
            catch (FormatException e)
            {
                time = time.Replace(".", ",");
                Error.toFile(e.Message.ToString() + " String time " + time, this.GetType().Name.ToString());
                utcTime = double.Parse(time);
            }

            utcTime = Math.Round((utcTime - (24515445E-1)) * 86400);
            return (long)utcTime;
        }
        #endregion
        private static string dbConfigPath = @"C:\Akce\java\grafy.ini";
        public async Task<DataRequest> proceedSQLquery(DataRequest dataRequest, CIniFile cConfig)
        {
            config = cConfig;
            getDbConfig();
            openDBconnections();

            int period = int.MinValue;
            string columns = null;
            string where;
            string[] conditions1 = { "\"UTC\"", "\"UTC\"" };
            string[] Operators = { ">=", "<=" };
            string[] conditions2 = { "'" + pkTimeToUTC(dataRequest.beginTime) + "'", "'" + pkTimeToUTC(dataRequest.beginTime + dataRequest.timeAxisLength) + "'" };
            List<int> tagsPos = new List<int>();
            List<double> tagMultipliers = new List<double>();
            List<object[]> objects = new List<object[]>();

            Error.TraceLog("GraphHandler.proceedSQLquery, point0");

            foreach (TableDef tabledef in config.TableDefList)
            {
                Error.TraceLog("GraphHandler.proceedSQLquery, point1");
                foreach (Tag tag in dataRequest.tags)
                {
                    if (tabledef.shortName == tag.table)
                    {
                        columns += " \"" + tag.column + "\",";
                        tagsPos.Add(dataRequest.tags.IndexOf(tag));

                        NameDef nd = cConfig.NameDefList.Find(p => (p.column == tag.column) & (p.table == tag.table));
                        double rMultiplier = 1.0;
                        if (nd != null)
                            rMultiplier = nd.multiplier;

                        tagMultipliers.Add(rMultiplier);
                        period = tag.period;
                        if (tag.vals == null)
                        {
                            // TK: tady to padalo na deleni nulou 2018-06-20
                            if (period == 0)
                                period = 20;        // DODELAT
                            tag.vals = new double[dataRequest.timeAxisLength / period];
                            tag.vals = Extension.Populate(tag.vals, double.MaxValue);
                        }
                    }
                }
                if (columns != null)
                {
                    columns = columns.Substring(0, columns.Length - 1);
                    db opennedDbConn = openDbList.Find(x => x.dbIdx == tabledef.dbIdx);
                    Error.TraceLog("GraphHandler.proceedSQLquery, point2");

                    if (tabledef.usePkTime == true) {
                        long timeTo = dataRequest.beginTime + dataRequest.timeAxisLength;
                        long timeFrom = dataRequest.beginTime;
                        string order = db.order("pktime", "ASC");
                        try
                        {
                            objects = await opennedDbConn.multipleItemSelectPostgresAsync("pktime," + columns, "\"" + tabledef.tabName + "\"", "(pktime BETWEEN " + timeFrom + " AND " + timeTo +")", null, order);
                        } catch (Exception e)
                        {
                            string error = "SQL problem: " + e.Message.ToString();
                            dataRequest.errorMessage = error;
                        }
                    }
                    else
                    {
                        where = db.whereMultiple(conditions1, Operators, conditions2);
                        string order = db.order("\"UTC\"", "ASC");
                        try
                        {
                            objects = await opennedDbConn.multipleItemSelectPostgresAsync("\"UTC\"," + columns, "\"" + tabledef.tabName + "\"", where, null, order);
                        }
                        catch (Exception e)
                        {
                            string error = "SQL problem: " + e.Message.ToString();
                            dataRequest.errorMessage = error;
                        }
                    }

                    Error.TraceLog("GraphHandler.proceedSQLquery, point5, " + (tabledef.usePkTime ? "pkTime" : "UTC"));
                    //readResponse(objects, dataRequest, tagsPos, tabledef);
                    //if (objects.Exists(p => p.Any(q => q.GetType() == typeof(DBNull))) == false) {
                    readResponseforTable(objects, tagsPos, tagMultipliers, period, dataRequest, tabledef, tabledef.usePkTime);
                    Error.TraceLog("GraphHandler.proceedSQLquery, point6");
                    //} else
                    //{
                    //   string k = "contains DBNull in the response";
                    //   Error.toFile(k, this.GetType().Name.ToString());
                    //}
                }
                columns = null;
                tagsPos.Clear();
                tagMultipliers.Clear();
            }
            foreach (db connection in openDbList)
            {
                connection.connection.Close();
            }
            return dataRequest;
        }



        /// <summary>
        /// Method to read response and prepare vals 
        /// </summary>
        /// <param name="rstObjects">result of SQL query</param>
        /// <param name="period">Period of signals in table</param>
        /// <param name="dataRequest">DataRequest</param>
        private void readResponseforTable(List<object[]> rstObjects, List<int> tagsPos, List<double> atagMultipliers, int period, DataRequest dataRequest, TableDef tabledef,bool usePkTime = false)
        {
            int rstPos = 0, buffPos = 0;
            object[] objectsArray;
            List<object[]> vals_agreg = new List<object[]>();
            double[][] vals_buffers = new double[(dataRequest.timeAxisLength / period)][];
            long time, startTime, endTime, low_buff_time, high_buff_time;
            startTime = dataRequest.beginTime;
            endTime = dataRequest.beginTime + dataRequest.timeAxisLength;
            for (int i = 0; i < rstObjects.Count; i++)
            {
                if (rstPos >= rstObjects.Count)
                {
                    break;
                }
                objectsArray = rstObjects[rstPos];
                low_buff_time = (startTime + (i * period));
                high_buff_time = (startTime + ((i + 1) * period));
                if (usePkTime == true)
                {
                    time = (int)objectsArray[0];
                }
                else
                {
                    time = utcToPkTime(objectsArray[0].ToString());
                }
                if (low_buff_time <= time && time <= high_buff_time)
                {
                    vals_agreg.Add(objectsArray); // Add value into vals for agragation

                    if ((time + tabledef.period) >= high_buff_time)
                    {
                        if (vals_agreg.Count != 0)
                        {
                            vals_agreg.Reverse();
                            for (int j = 1; j < (objectsArray.Length); j++)
                            {
                                if (vals_agreg[0][j] != DBNull.Value)
                                {
                                    double value = Convert.ToDouble(vals_agreg[0][j]);
                                    double multiplier = atagMultipliers[j - 1];
                                    dataRequest.tags[tagsPos[j - 1]].vals[buffPos] = value * multiplier; //Adding value to response
                                }
                            }
                            buffPos++;
                            vals_agreg.Clear();
                        }
                        else
                        {
                            if (buffPos <= vals_buffers.Length)
                            {
                                for (int j = 1; j < (objectsArray.Length); j++)
                                {
                                    dataRequest.tags[tagsPos[j - 1]].vals[buffPos] = double.MinValue; // missing data adding NaN value to response
                                }
                                buffPos++;
                            }
                        }
                        rstPos++;
                    }
                    else
                    {
                        rstPos++;
                        i--;
                    }
                }
                else
                {
                    if (time < high_buff_time && (time + period) >= low_buff_time)
                    {
                        if (vals_agreg.Count != 0)
                        {
                            vals_agreg.Reverse();
                            for (int j = 1; j < (objectsArray.Length); j++)
                            {
                                if (vals_agreg[0][j] != DBNull.Value)
                                {
                                    double value = Convert.ToDouble(vals_agreg[0][j]);
                                    dataRequest.tags[tagsPos[j - 1]].vals[buffPos] = value; //Adding value to response
                                }
                            }
                            i--;
                            buffPos++;
                            vals_agreg.Clear();
                        }
                        else
                        {
                            if (buffPos < vals_buffers.Length)
                            {
                                for (int j = 1; j < (objectsArray.Length); j++)
                                {
                                    dataRequest.tags[tagsPos[j - 1]].vals[buffPos] = double.MinValue;// missing data adding Double.MinValue(about -1.79 E+308) value to response
                                }
                                buffPos++;
                            }
                        }
                        rstPos++;
                    }

                    //Missing data
                    if (time > high_buff_time)
                    {
                        if (buffPos < vals_buffers.Length)
                        {
                            for (int j = 1; j < (objectsArray.Length); j++)
                            {
                                dataRequest.tags[tagsPos[j - 1]].vals[buffPos] = double.MinValue;// missing data adding  Double.MinValue (about -1.79 E+308) value to response
                            }
                            buffPos++;
                        }
                    }
                }
                //No rstPos++; because it could ovewrflow with index on the missing data
            }
        }

        // WARNING: very fast to transform but not very secure way
        private void getDbConfig()
        {
            int i = 0;
            int dataserverNumber, dbIndex;
            string databaseName = null;
            string[] separeted_string = null;
            string[] separators = { "URL=", ",jdbc", ".2.", ":5432/" };
            string[] lines = System.IO.File.ReadAllLines(dbConfigPath, Encoding.Default);
            foreach (string line in lines)
            {
                separeted_string = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (!(lines[i].StartsWith("#")) && (lines[i].Length != 0 && separeted_string.Length > 1))
                {
                    dbIndex = int.Parse(separeted_string[1]);
                    dataserverNumber = int.Parse(separeted_string[3]);
                    databaseName = separeted_string[4];
                    dbDefList.Add(new DatabaseDef() { dbIdx = dbIndex, database = databaseName, dataserverNumber = dataserverNumber });
                }
                i++;
            }
        }
        private void openDBconnections()
        {
            string database = null;
            int dbIndex, serverNumber = 0;

            foreach (TableDef TableDef in config.TableDefList)
            {
                dbIndex = TableDef.dbIdx;
                foreach (DatabaseDef DatabaseDef in dbDefList)
                {
                    if (DatabaseDef.dbIdx == dbIndex)
                    {
                        database = DatabaseDef.database;
                        serverNumber = DatabaseDef.dataserverNumber;
                    }
                }
                db db = new db(database, serverNumber, dbIndex);
                if (!(openDbList.Exists(x => x.dbIdx == dbIndex)))
                {
                    openDbList.Add(db);
                }
            }
        }

    }
}