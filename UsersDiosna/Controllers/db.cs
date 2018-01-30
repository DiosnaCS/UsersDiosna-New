using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UsersDiosna.Report.Models;

namespace UsersDiosna.Controllers
{
    //Class for MSSQL db
    public class db
    {
        // Reference to db on which is open connection
        public int dbIdx;
        //Only shared connection
        private SqlConnection conn;
        public NpgsqlConnection connection;
        /// <summary>
        /// MSSQL constructor
        /// </summary>
        public db()
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            conn = new SqlConnection(ConnStr);
        }
        /// <summary>
        /// Constructor to establish db connection on PostgreSQL database
        /// </summary>
        /// <param name="adb">database name</param>
        public db(string aDB, int dataserverNumber)
        {
            string db = aDB;
            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
              "192.168.2." + dataserverNumber, 5432, "postgres", "Nordit0276", db);
            connection = new NpgsqlConnection(connstring);
        }

        /// <summary>
        /// Constructor to establish db connection on PostgreSQL database
        /// </summary>
        /// <param name="adb">database name</param>
        public db(string aDB, int dataserverNumber, int dbIndex)
        {
            string db = aDB;
            dbIdx = dbIndex;
            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
              "192.168.2." + dataserverNumber, 5432, "postgres", "Nordit0276", db);
            connection = new NpgsqlConnection(connstring);
        }

        /// <summary>
        /// Method to get one single element as an object
        /// </summary>
        /// <param name="column">columnn SQL to select</param>
        /// <param name="table">table SQL to select</param>
        /// <param name="where">where statement to specify select</param>
        /// <returns>Data in object</returns>
        public object singleItemSelect(string column, string table, string where = null)
        {
            if (conn.State.ToString().Contains("Closed"))
            {
                conn.Open();
            }
            object result = new object();

            if (where == null)
            {
                //SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM "+ table, conn);
                string sql = string.Format("SELECT {0} FROM {1}", column, table);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    result = r.GetValue(0);
                }
            }
            else
            {
                //SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM "+ table +" WHERE " + where, conn);
                string sql = string.Format("SELECT {0} FROM {1} WHERE {2}", column, table, where);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    result = r.GetValue(0);
                }
            }

            return result;
        }

        /// <summary>
        /// Method to get multiple elements as an list of objects
        /// </summary>
        /// <param name="column">columnn SQL to select</param>
        /// <param name="table">table SQL to select</param>
        /// <param name="where">where statement to specify select</param>
        /// <returns>Data in list of objects</returns>
        public List<object> multipleItemSelect(string column, string table, string where = null)
        {
            if (conn.State.ToString().Contains("Closed"))
            {
                conn.Open();
            }
            List<object> result = new List<object>();

            if (where == null)
            {
                string sql = string.Format("SELECT {0} FROM {1}", column, table);
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM " + table, conn);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        result.Add(r[i]);
                    }
                }
            }
            else
            {
                string sql = string.Format("SELECT {0} FROM {1} WHERE {2}", column, table, where);
                SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlCommand cmd = new SqlCommand("SELECT "+ column +" FROM " + table + " WHERE bakeryId = @where" , conn);
                //cmd.Parameters.AddWithValue("@where", 15014);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        result.Add(r[i]);
                    }
                }
                r.Close();
            }            
            return result;
        }

        /// <summary>
        /// Method to update one single element
        /// </summary>
        /// <param name="table">columnn SQL to select<</param>
        /// <param name="set">values to set</param>
        /// <param name="where">where statement to specify select</param>
        public void singleItemUpdate(string table, string set, string where = null)
        {
            if (conn.State.ToString().Contains("Closed"))
            {
                conn.Open();
            }
            if (where == null)
            {
                string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", table, set, where);
                SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlCommand cmd = new SqlCommand("UPDATE " + table + " SET  @set WHERE " + where, conn);
                //cmd.Parameters.AddWithValue("@set", set);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sql = string.Format("UPDATE {0} SET {1}", table, set);
                SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlCommand cmd = new SqlCommand("UPDATE " + table + " SET  @set", conn);
                //cmd.Parameters.AddWithValue("@set", set);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method to insert one single element
        /// </summary>
        /// <param name="table">table SQL in which do you need insert</param>
        /// <param name="column">column SQL in which do you need insert</param>
        /// <param name="value">values which you want to insert</param>
        /// <param name="where">where statement to specify select</param>
        public void singleItemInsert(string table, string column, string value, string where = null)
        {
            if (conn.State.ToString().Contains("Closed"))
            {
                conn.Open();
            }
            if (where == null)
            {
                string sql = string.Format("INSERT INTO {0} ({1}) VALUES {2} WHERE {3}", table, column, value, where);
                SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlCommand cmd = new SqlCommand("INSERT INTO " + table + "(" + column + ") VALUES  @values WHERE " + where, conn);
                //cmd.Parameters.AddWithValue("@value", value);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sql = string.Format("INSERT INTO {0} ({1}) VALUES {2}", table, column, value);
                SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlCommand cmd = new SqlCommand("INSERT INTO " + table + " VALUES  @values", conn);
                //cmd.Parameters.AddWithValue("@value", value);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Async method 
        /// Method to insert one single element
        /// </summary>
        /// <param name="table">string SQL table to write into that</param>
        /// <param name="column">string SQL column</param>
        /// <param name="value">string value to defined values to write into database</param>
        /// <param name="where">string where condition</param>
        public async void singleItemInsertAsync(string table, string column, string value, string where = null)
        {
            if (conn.State.ToString().Contains("Closed"))
            {
                await conn.OpenAsync();
            }
            string tableSQL = string.Format(@"{0}", table);
            string columnSQL = string.Format(@"{0}", column);
            string valueSQL = string.Format(@"{0}", value);

            if (where == null)
            {
                string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, column, value);
                SqlCommand cmd = new SqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
            else
            {
                string whereSQL = string.Format(@"{0}", where);
                string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2}) WHERE {3}", table, column, value, where);
                SqlCommand cmd = new SqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Async method
        /// Method to delete selected item(s)
        /// </summary>
        /// <param name="table">table SQL to delete</param>
        /// <param name="where">string where condition</param>
        public async void singleItemDeleteAsync(string table, string where)
        {
            if (conn.State.ToString().Contains("Closed"))
            {
                await conn.OpenAsync();
            }
            SqlCommand cmd = new SqlCommand("DELETE FROM " + table + " WHERE " + where, conn);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Async method
        /// Method to update one single element
        /// </summary>
        /// <param name="table">table SQL to update</param>
        /// <param name="set">values to set</param>
        /// <param name="where">string where condition</param>
        public async void singleItemUpdateAsync(string table, string set, string where = null)
        {
            if (conn.State.ToString().Contains("Closed"))
            {
                await conn.OpenAsync();
            }
            if (where == null)
            {
                //SqlCommand cmd = new SqlCommand("UPDATE " + table + " SET "+ set, conn);
                string sql = string.Format("UPDATE {0} SET {1}", table, set);
                SqlCommand cmd = new SqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
            else
            {
                //SqlCommand cmd = new SqlCommand("UPDATE " + table + " SET "+ set +" WHERE " + where, conn);
                string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", table, set, where);
                SqlCommand cmd = new SqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Select single item from 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="table"></param>
        /// <param name="where">string where condition - function result</param>
        /// <returns></returns>
        public object singleItemSelectPostgres(string column, string table, Func<string> where = null)
        {
            object result = new object();

            if (connection.FullState == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            if (where == null)
            {
                //SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM "+ table, conn);
                string sql = string.Format("SELECT \"{0}\" FROM \"{1}\" ORDER BY \"UTC\" DESC LIMIT 1", column, table);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    result = r.GetValue(0);
                }
                r.Close();
                cmd.Dispose();
            }
            else
            {
                //SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM "+ table +" WHERE " + where, conn);
                string sql = string.Format("SELECT \"{0}\" FROM \"{1}\" WHERE {2}", column, table, where);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    result = r.GetValue(0);
                }
                r.Close();
                cmd.Dispose();
            }

            return result;
        }
        public object singleItemSelectPostgres(string column, string table, string whereMultiple = null, string groupBy = null, string order = null)
        {
            object result = new object();

            if (whereMultiple == null)
            {
                //SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM "+ table, conn);
                string sql = string.Format("SELECT \"{0}\" FROM \"{1}\"", column, table);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    result = r.GetValue(0);
                }
            }
            else
            {
                //SqlCommand cmd = new SqlCommand("SELECT " + column + " FROM "+ table +" WHERE " + where, conn);
                string sql = string.Format("SELECT \"{0}\" FROM \"{1}\" WHERE {2}", column, table, whereMultiple);
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    result = r.GetValue(0);
                }
            }

            return result;
        }
        public object singleItemSelectPostgres(string column, string table, string sum = null, string whereMultiple = null, string groupBy = null, string order = null)
        {
            object result = new object();
            string sql = null;

            if (sum == null)
            {
                sql = string.Format("SELECT {0} FROM {1}", column, table);
            }
            else
            {
                if (whereMultiple == null)
                {
                    if (groupBy == null)
                    {
                        if (order == null)
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2}", column, sum, table);
                        }
                        else
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2} {3}", column, sum, table, order);
                        }
                    }
                    else
                    {
                        if (order == null)
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2} {3}", column, sum, table, groupBy);
                        }
                        else
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2} {3} {4}", column, sum, table, groupBy, order);
                        }
                    }
                }
                else
                {
                    if (groupBy == null)
                    {
                        if (order == null)
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2} WHERE {3}", column, sum, table, whereMultiple);
                        }
                        else
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2} WHERE {3} {4}", column, sum, table, whereMultiple, order);
                        }
                    }
                    else
                    {
                        if (order == null)
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2} WHERE {3} {4}", column, sum, table, whereMultiple, groupBy);
                        }
                        else
                        {
                            sql = string.Format("SELECT {0}, {1} FROM {2} WHERE {3} {4} {5}", column, sum, table, whereMultiple, groupBy, order);
                        }
                    }
                }
            }

            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                result = r.GetValue(0);
            }

            return result;
        }

        public List<object[]> multipleItemSelectPostgres(string sql)
        {
            List<object[]> result = new List<object[]>();
            if (connection.FullState == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            NpgsqlTransaction tran = connection.BeginTransaction();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;

            NpgsqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                object[] tmpObjectArray = new object[r.FieldCount];
                for (int i = 0; i < (r.FieldCount); i++)
                {
                    tmpObjectArray[i] = r[i];

                }
                result.Add(tmpObjectArray);
            }
            r.Close();
            cmd.Dispose();
            return result;
        }

        public async Task<List<object[]>> multipleItemSelectPostgresAsync(string sql)
        {
            List<object[]> result = new List<object[]>();
            if (connection.FullState == System.Data.ConnectionState.Closed)
            {
               await connection.OpenAsync();
            }

            NpgsqlTransaction tran = connection.BeginTransaction();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (await r.ReadAsync())
            {
                object[] tmpObjectArray = new object[r.FieldCount];
                for (int i = 0; i < (r.FieldCount); i++)
                {
                    tmpObjectArray[i] = r[i];
                }
                result.Add(tmpObjectArray);
            }
            r.Close();
            cmd.Dispose();
            return result;
        }
        public async Task<List<T[]>> multipleItemSelectPostgresAsync<T>(string sql)
        {
            List<T[]> result = new List<T[]>();
            if (connection.FullState == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }


            NpgsqlCommand cmd = new NpgsqlCommand("SELECT FROM WHERE", connection);
            cmd.Parameters.AddWithValue("", "");
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (await r.ReadAsync())
            {
                T[] tmpObjectArray = new T[r.FieldCount];
                for (int i = 0; i < (r.FieldCount); i++)
                {
                    tmpObjectArray[i] = (T) r[i];
                }
                result.Add(tmpObjectArray);
            }
            r.Close();
            cmd.Dispose();
            conn.Close();
            return result;
        }
        /// <summary>
        /// Method to select multiple items from postrgres db
        /// </summary>
        /// <param name="column"></param>
        /// <param name="table"></param>
        /// <param name="whereMultiple">string where condition - function result</param>
        /// <param name="groupBy"></param>
        /// <param name="order"></param>
        /// <returns></returns>        
        public List<object[]> multipleItemSelectPostgres(string column, string table, string whereMultiple = null, string groupBy = null, string order = null)
        {
            string sql = null;
            List<object[]> result = new List<object[]>();
            if (connection.FullState != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            if (whereMultiple == null)
            {
                if (groupBy == null)
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1}", column, table);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} {2}", column, table, order);
                    }
                }
                else
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1} {2}", column, table, groupBy);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} {2} {3}", column, table, groupBy, order);
                    }
                }
            }
            else
            {
                if (groupBy == null)
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2}", column, table, whereMultiple);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2} {3}", column, table, whereMultiple, order);
                    }
                }
                else
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2} {3}", column, table, whereMultiple, groupBy);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2} {3} {4}", column, table, whereMultiple, groupBy, order);
                    }
                }
            }

            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                object[] tmpObjectArray = new object[r.FieldCount];
                for (int i = 0; i < (r.FieldCount); i++)
                {
                    tmpObjectArray[i] = r[i];
                }
                result.Add(tmpObjectArray);
            }
            r.Close();
            cmd.Dispose();
            return result;
        }

        /// <summary>
        /// Async method
        /// Method to select multiple items from postrgres db
        /// </summary>
        /// <param name="column">columnn SQL to select</param>
        /// <param name="table">table SQL to select</param>
        /// <param name="whereMultiple">string where condition - function result</param>
        /// <param name="groupBy">string groupBy condition - function result</param>
        /// <param name="order">string orderBy condition - function result</param>
        /// <returns>List of rows of data</returns>
        public async Task<List<object[]>> multipleItemSelectPostgresAsync(string columns, string table, string whereMultiple = null, string groupBy = null, string order = null)
        {
            string sql = null;
            List<object[]> result = new List<object[]>();
            if (connection.FullState != System.Data.ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            if (connection.FullState != ConnectionState.Open) {
                connection.Dispose();
                connection.Open();
            }
            if (whereMultiple == null)
            {
                if (groupBy == null)
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1}", columns, table);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} {2}", columns, table, order);
                    }
                }
                else
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1} {2}", columns, table, groupBy);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} {2} {3}", columns, table, groupBy, order);
                    }
                }
            }
            else
            {
                if (groupBy == null)
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2}", columns, table, whereMultiple);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2} {3}", columns, table, whereMultiple, order);
                    }
                }
                else
                {
                    if (order == null)
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2} {3}", columns, table, whereMultiple, groupBy);
                    }
                    else
                    {
                        sql = string.Format("SELECT {0}  FROM {1} WHERE {2} {3} {4}", columns, table, whereMultiple, groupBy, order);
                    }
                }
            }

            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (await r.ReadAsync())
            {
                object[] tmpObjectArray = new object[r.FieldCount];
                for (int i = 0; i < (r.FieldCount); i++)
                {
                    tmpObjectArray[i] = r[i];
                }
                result.Add(tmpObjectArray);
                //tmpObjectArray.c
                //for (int i = 0; i < r.FieldCount; i++) {
                //    tmpObjectArray[i] = null;
                //}
            }
            r.Close();
            cmd.Dispose();
            return result;
        }

        public static string where(string conditionVariable1, string Operator, string conditionVariable2)
        {
            string whereSQL = conditionVariable1 + Operator + conditionVariable2;
            return whereSQL;
        }
        public static string whereMultiple(string[] conditionVariables1, string[] Operators, string[] conditionVariables2)
        {
            string whereSQL = null;
            if (conditionVariables1.Length == conditionVariables2.Length && conditionVariables1.Length == Operators.Length && conditionVariables2.Length == Operators.Length)
            {
                whereSQL = conditionVariables1[0] + Operators[0] + conditionVariables2[0];
                for (int i = 1; i < conditionVariables1.Length; i++)
                {
                    whereSQL += " AND " + conditionVariables1[i] + Operators[i] + conditionVariables2[i];
                }
            }
            return whereSQL;
        }
        public static string sum(string column, string newcolumn = null)
        {
            string sumSQL = null;
            if (newcolumn == null)
            {
                sumSQL = "SUM(" + column + ")";
            }
            else
            {
                sumSQL = "SUM(" + column + ") AS " + newcolumn;
            }
            return sumSQL;
        }
        public static string order(string column, string order)
        {
            string orderBySQL = "ORDER BY " + column + " " + order;
            return orderBySQL;
        }
        public static string groupBy(string column, string order)
        {
            string groupBySQL = "GROUP BY " + column + " " + order;
            return groupBySQL;
        }

    }

    public class ReportDBHelper {
        public Int32 ConvertDT2pkTime(DateTime dateTime)
        {
            DateTime pkTimeStart = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc);
            Int32 pkTime = 0;
            pkTime = (Int32)(dateTime.ToUniversalTime() - pkTimeStart).TotalSeconds;

            return pkTime;
        }

        public NpgsqlConnection connection;
        /// <summary>
        /// Constructor to establish db connection on PostgreSQL database
        /// </summary>
        /// <param name="adb">database name</param>
        public ReportDBHelper(string aDB, int dataserverNumber)
        {
            string DB = aDB;
            string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
              "192.168.2.1" + dataserverNumber, 5432, "postgres", "Nordit0276", DB);
            connection = new NpgsqlConnection(connstring);
            connection.Open();
        }

        public async Task<DataReportModel> SelectHeaderDataAsync(long from, long to, string table)
        {
            DataReportModel data = new DataReportModel();
            data.Data = new List<ColumnReportModel>();


            //gett where condition
            /* Indexes of result set
             0 => RecordNo 
            1 => RecordType 
            2 =>TimeStart 
            3 => TimeEnd 
            4 =>BatchNo 
            5 => Destination 
            6 => Need 
            7 => Actual 
            8 => Variant1 
            9 => Variant2 
            10 => Variant3 
            11 => Variant4 
            */

            /*"AND(\"RecordType\" = {3} OR \"RecordType\" = {4}" +
                                        " OR \"RecordType\" = {5} OR \"RecordType\" = {6}  OR \"RecordType\" = {7})",*/
            /*" OR \"RecordType\" = {8} OR \"RecordType\" = {9}" +
            " OR \"RecordType\" = {10} OR \"RecordType\" = {11} OR \"RecordType\" = {12})",*/
            /*, (int)Operations.RecipeStart, (int)Operations.Interrupt, (int)Operations.Continue, (int)Operations.StepSkip, (int)Operations.RecipeEnd/*,
                (int)Operations.DosingOut, (int)Operations.PipWorkCleaning, (int)Operations.Pigging, (int)Operations.FermenterCleaning, (int)Operations.YeastCleaning*/

            //string sql = string.Format("SELECT * FROM \"{0}\" WHERE \"TimeStart\" > {1} AND \"TimeStart\" < {2}", table, from, to);
            string sql = string.Format(" SELECT * FROM \"events\" WHERE \"BatchNo\" IN (SELECT \"BatchNo\" FROM {0} WHERE (\"TimeEnd\" > {1} AND \"TimeStart\" <= {2} AND \"BatchNo\" > 0) GROUP BY \"BatchNo\") ORDER BY \"BatchNo\"", table, from, to);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (await r.ReadAsync())
            {
                ColumnReportModel CRM = new ColumnReportModel();
                if (r[0] != DBNull.Value)
                {
                    CRM.RecordNo = int.Parse(r[0].ToString());
                }
                if (r[1] != DBNull.Value)
                {
                    CRM.RecordType = (Operations)int.Parse(r[1].ToString());
                }
                if (r[2] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[2].ToString()) * 10000000;
                    CRM.TimeStart = new DateTime(((630822816000000000) + timeInNanoSeconds));
                }
                if (r[3] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[3].ToString()) * 10000000;
                    CRM.TimeEnd = new DateTime(((630822816000000000) + timeInNanoSeconds));
                }
                if (r[4] != DBNull.Value)
                {
                    CRM.BatchNo = int.Parse(r[4].ToString());
                }
                if (r[5] != DBNull.Value)
                {
                    CRM.Destination = r[5].ToString();
                }
                if (r[6] != DBNull.Value)
                {
                    if ((int)r[6] != 0)
                        CRM.Need = int.Parse(r[6].ToString());
                }
                if (r[7] != DBNull.Value)
                {
                    if ((int)r[7] != 0)
                        CRM.Actual = int.Parse(r[7].ToString());
                }
                if (r[8] != DBNull.Value)
                {
                    //Variant1 is iRCP_NO
                    CRM.Variant1 = int.Parse(r[8].ToString());
                }
                if (r[9] != DBNull.Value)
                {
                    if ((int)r[9] != 0)
                        CRM.Variant2 = int.Parse(r[9].ToString());
                }
                // r[10] should be Variant3 and Variant3 is  irrelevant
                if (r[11] != DBNull.Value)
                {
                    CRM.Variant4 = int.Parse(r[11].ToString());
                }
                data.Data.Add(CRM);
            }
            r.Dispose();
            cmd.Dispose();
            return data;
        }

        public DataReportModel SelectHeaderData(DateTime from, DateTime to, string table)
        {
            // variables initialization
            DataReportModel data = new DataReportModel();
            data.Data = new List<ColumnReportModel>();

                
            int pkTimeStart = ConvertDT2pkTime(from);
            int pkTimeEnd = ConvertDT2pkTime(to);

            //gett where condition
            /* Indexes of result set
             0 => RecordNo 
            1 => RecordType 
            2 =>TimeStart 
            3 => TimeEnd 
            4 =>BatchNo 
            5 => Destination 
            6 => Need 
            7 => Actual 
            8 => Variant1 
            9 => Variant2 
            10 => Variant3 
            11 => Variant4 
            */
            string sql = string.Format("SELECT * FROM \"{0}\" WHERE \"TimeStart\" > {1} AND \"TimeStart\" < {2} " +
                                        "AND(\"RecordType\" = {3} OR \"RecordType\" = {4}" +
                                        " OR \"RecordType\" = {5} OR \"RecordType\" = {6}  OR \"RecordType\" = {7} OR \"RecordType\" = {8} OR \"RecordType\" = {9}" +
                                        " OR \"RecordType\" = {10} OR \"RecordType\" = {11} OR \"RecordType\" = {12})",
                table, pkTimeStart, pkTimeEnd, (int)Operations.RecipeStart, (int)Operations.Interrupt, (int)Operations.Continue, (int) Operations.StepSkip, (int)Operations.RecipeEnd,
                (int)Operations.DosingOut, (int)Operations.PipWorkCleaning, (int)Operations.Pigging, (int)Operations.FermenterCleaning, (int)Operations.YeastCleaning);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                ColumnReportModel CRM = new ColumnReportModel();                
                if (r[0] != DBNull.Value)
                {
                    CRM.RecordNo = int.Parse(r[0].ToString());
                }
                if (r[1] != DBNull.Value)
                {
                    CRM.RecordType = (Operations) int.Parse(r[1].ToString());
                }
                if (r[2] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[2].ToString()) * 10000000;
                    CRM.TimeStart = new DateTime(((630822816000000000) + timeInNanoSeconds));
                }
                // r[3] should be TimeEnd and TimeEnd is irrelevant for header data
                if (r[4] != DBNull.Value)
                {
                    CRM.BatchNo = int.Parse(r[4].ToString());
                }
                if (r[5] != DBNull.Value)
                {
                    CRM.Destination = r[5].ToString();
                }
                if (r[6] != DBNull.Value)
                {
                    if ((int) r[6] != 0)
                        CRM.Need = int.Parse(r[6].ToString());
                }
                if (r[7] != DBNull.Value)
                {
                    if ((int)r[7] != 0)
                        CRM.Actual = int.Parse(r[7].ToString());
                }
                if (r[8] != DBNull.Value)
                {
                    //Variant1 is iRCP_NO
                    CRM.Variant1 = int.Parse(r[8].ToString());
                }
                if (r[9] != DBNull.Value)
                {
                    if ((int)r[9] != 0)
                        CRM.Variant2 = int.Parse(r[9].ToString());
                }
                // r[10] should be Variant3 and Variant3 is  irrelevant
                if (r[11] != DBNull.Value)
                {
                    CRM.Variant4 = int.Parse(r[11].ToString());
                }
                data.Data.Add(CRM);
            }
            r.Dispose();
            cmd.Dispose();
            return data;
        }

        public DataReportModel SelectSteps(int batchNo, string table)
        {
            // variables initialization
            DataReportModel data = new DataReportModel();
            data.Data = new List<ColumnReportModel>();
            string columns = string.Empty;
            string where = string.Empty;

            //gett where condition
            /* Indexes of result set
             0 => RecordNo 
            1 => RecordType 
            2 =>TimeStart 
            3 => TimeEnd 
            4 =>BatchNo 
            5 => Destination 
            6 => Need 
            7 => Actual 
            8 => Variant1 
            9 => Variant2 
            10 => Variant3 
            11 => Variant4 
            */
            string sql = string.Format("SELECT * FROM \"{0}\" WHERE \"BatchNo\" = {1}", table, batchNo);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                ColumnReportModel CRM = new ColumnReportModel();
                if (r[0] != DBNull.Value)
                {
                    CRM.RecordNo = int.Parse(r[0].ToString());
                }
                if (r[1] != DBNull.Value)
                {
                    CRM.RecordType = (Operations) int.Parse(r[1].ToString());
                }
                if (r[2] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[2].ToString()) * 10000000;
                    CRM.TimeStart = new DateTime(((630822816000000000) + timeInNanoSeconds));
                }
                if (r[3] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[3].ToString()) * 10000000;
                    CRM.TimeEnd = new DateTime(((630822816000000000) + timeInNanoSeconds));
                }
                //r[4] is BatchNo and we dont need BatchNo
                if (r[5] != DBNull.Value)
                {
                    CRM.Destination = r[5].ToString();
                }
                if (r[6] != DBNull.Value)
                {
                    if ((int)r[6] != 0)
                        CRM.Need = int.Parse(r[6].ToString());
                }
                if (r[7] != DBNull.Value)
                {
                    if ((int)r[7] != 0)
                        CRM.Actual = int.Parse(r[7].ToString());
                }
                if (r[8] != DBNull.Value)
                {
                    //Variant1 is iRCP_NO
                    CRM.Variant1 = int.Parse(r[8].ToString());
                }
                if (r[9] != DBNull.Value)
                {
                    if ((int)r[9] != 0)
                        CRM.Variant2 = int.Parse(r[9].ToString());
                }
                if (r[10] != DBNull.Value)
                {
                    if ((int)r[9] != 0)
                        CRM.Variant3 = int.Parse(r[10].ToString());
                }
                if (r[11] != DBNull.Value)
                {
                    CRM.Variant4 = int.Parse(r[11].ToString());
                }
                data.Data.Add(CRM);
            }
            r.Dispose();
            cmd.Dispose();
            return data;
        }

        public OverviewReportModel SelectConsumption(DateTime from, DateTime to, string table)
        {
            // variables initialization
            OverviewReportModel data = new OverviewReportModel();
            data.Data = new List<OverviewReportDataModel>();
            string columns = string.Empty;
            string where = string.Empty;
            int oldDay = 1;
            int newDay = 1;
            int thisBatchNo = 0;
            int pkTimeStart = ConvertDT2pkTime(from);
            int pkTimeEnd = ConvertDT2pkTime(to);
            OverviewReportDataModel ORDM = new OverviewReportDataModel();
            //gett where condition
            /* Indexes of result set
             0 => RecordNo 
            1 => RecordType 
            2 =>TimeStart 
            3 => TimeEnd 
            4 =>BatchNo 
            5 => Destination 
            6 => Need 
            7 => Actual 
            8 => Variant1 
            9 => Variant2 
            10 => Variant3 
            11 => Variant4 
            */
            string sql = string.Format("SELECT * FROM \"{0}\" WHERE (\"TimeStart\" > {1} AND \"TimeStart\" < {2}) AND \"RecordType\" BETWEEN 20 AND 28 ORDER BY \"TimeEnd\" ASC", table, pkTimeStart, pkTimeEnd);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                ColumnReportModel CRM = new ColumnReportModel();
                
                if (r[1] != DBNull.Value)
                {
                    CRM.RecordType = (Operations)int.Parse(r[1].ToString());
                }
                if (r[3] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[3].ToString()) * 10000000;
                    CRM.TimeEnd = new DateTime((630822816000000000) + timeInNanoSeconds);
                }
                if (r[4] != DBNull.Value)
                {
                    CRM.BatchNo = int.Parse(r[4].ToString());
                }
                if (r[7] != DBNull.Value)
                {
                    if ((int)r[7] != 0)
                        CRM.Actual = int.Parse(r[7].ToString());
                    if (CRM.TimeEnd.Day > newDay)
                    {
                        data.Data.Add(ORDM);
                        ORDM = new OverviewReportDataModel();
                        newDay = CRM.TimeEnd.Day;
                    }

                    switch (CRM.RecordType)
                    {
                        case Operations.FillingMotherCulture:
                            ORDM.MotherCultureAmnt += CRM.Actual;
                            if (CRM.BatchNo != thisBatchNo)
                            {
                                ORDM.MotherCultureBatchCount++;
                                thisBatchNo = CRM.BatchNo;
                            }
                            break;
                        case Operations.FillingFlour:
                            ORDM.FlourAmnt += CRM.Actual;
                            if (CRM.BatchNo != thisBatchNo)
                            {
                                ORDM.FlourBatchCount++;
                                thisBatchNo = CRM.BatchNo;
                            }
                            break;
                        case Operations.FillingWater:
                            ORDM.WaterAmnt += CRM.Actual;
                            if (CRM.BatchNo != thisBatchNo)
                            {
                                ORDM.WaterBatchCount++;
                                thisBatchNo = CRM.BatchNo;
                            }
                            break;
                        case Operations.FillingOldBread:
                            ORDM.OldBreadAmnt += CRM.Actual;
                            if (CRM.BatchNo != thisBatchNo)
                            {
                                ORDM.OldBreadBatchCount++;
                                thisBatchNo = CRM.BatchNo;
                            }
                            break;
                        case Operations.FillingLiquidYeast:
                            ORDM.LiquidYeastAmnt += CRM.Actual;
                            if (CRM.BatchNo != thisBatchNo)
                            {
                                ORDM.LiquidYeastBatchCount++;
                                thisBatchNo = CRM.BatchNo;
                            }
                            break;
                        case Operations.FillingMixture:
                            ORDM.MixtureAmnt += CRM.Actual;
                            if (CRM.BatchNo != thisBatchNo)
                            {
                                ORDM.MixtureBatchCount++;
                                thisBatchNo = CRM.BatchNo;
                            }
                            break;
                        case Operations.FillingGenericComponent:
                            ORDM.MixtureBatchCount += CRM.Actual;
                            if (CRM.BatchNo != thisBatchNo)
                            {
                                ORDM.GenericBatchCount++;
                                thisBatchNo = CRM.BatchNo;
                            }
                            break;
                        default:
                            break;
                    }
                    ORDM.day = newDay;
                }
            }
            
            r.Dispose();
            cmd.Dispose();
            return data;
        }

        public ReportDosing GetReportDosing(DateTime from, DateTime to, string table)
        {
            ReportDosing data = new ReportDosing();
            data.Batches = new List<Dosing>();

            int pkTimeFrom = ConvertDT2pkTime(from);
            int pkTimeTo = ConvertDT2pkTime(to);

            string sql = string.Format("SELECT * FROM \"{0}\" WHERE (\"TimeStart\" BETWEEN {1} AND {2}) AND (\"RecordType\"=44) ORDER BY \"TimeStart\" ASC", table, pkTimeFrom, pkTimeTo);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                Dosing dosing = new Dosing();
                if (r[0] != DBNull.Value)
                {
                    dosing.RecordNo = int.Parse(r[0].ToString());
                }
                //r[1] is RecordType and we know that is DosingOut
                if (r[2] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[2].ToString()) * 10000000;
                    dosing.TimeStart = new DateTime(((630822816000000000) + timeInNanoSeconds));
                }
                if (r[3] != DBNull.Value)
                {
                    long timeInNanoSeconds = long.Parse(r[3].ToString()) * 10000000;
                    dosing.TimeEnd = new DateTime(((630822816000000000) + timeInNanoSeconds));
                }
                //r[4] is BatchNo and we dont need BatchNo
                if (r[5] != DBNull.Value)
                {
                    dosing.Destination = r[5].ToString();
                }
                //r[6] Need have no value
                if (r[7] != DBNull.Value)
                {
                    if ((int)r[7] != 0)
                        dosing.Actual = int.Parse(r[7].ToString());
                }
                if (r[8] != DBNull.Value)
                {
                    if (r[8].ToString() !="")
                        dosing.SrcTank = r[8].ToString();
                }
                if (r[9] != DBNull.Value)
                {
                    if ((int)r[9] != 0)
                        dosing.StartedBy = (StartedBy) int.Parse(r[9].ToString());
                }
                if (r[10] != DBNull.Value)
                {
                    if ((int)r[10] != 0)
                        dosing.SrcBatchNo = int.Parse(r[10].ToString());
                }
                data.Batches.Add(dosing);
            }
            r.Dispose();
            cmd.Dispose();
            return data;

            return data;
        }
        
        public int SelectPrevBatchNo(int BatchNo, string table) {
            //id is BatchNo
            int id = 0;

            string sql = string.Format("SELECT \"BatchNo\" FROM \"{0}\" WHERE \"BatchNo\"<{1} ORDER BY \"BatchNo\" DESC FETCH FIRST 1 ROWS ONLY", table, BatchNo);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (r.Read()) {
                id = r.GetInt32(0);
            }
            r.Dispose();
            cmd.Dispose();
            return id;
        }

        public int SelectNextBatchNo(int BatchNo, string table)
        {
            //id is BatchNo
            int id = 0;

            string sql = string.Format("SELECT \"BatchNo\" FROM \"{0}\" WHERE \"BatchNo\">{1} ORDER BY \"BatchNo\" ASC FETCH FIRST 1 ROWS ONLY", table, BatchNo);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                id = r.GetInt32(0);
            }
            r.Dispose();
            cmd.Dispose();
            return id;
        }
    }
}