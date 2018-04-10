using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsersDiosna.Controllers;
using UsersDiosna.Sheme.Models;

namespace UsersDiosna.Handlers
{
    public class NewSchemesHandler
    {
        public async Task<object> getSnapshotData(List<ResponseValue> responseList, List<SchemeValue> list, int projectId = 0)
        {
            object data;
            List<string> dbNames = XMLHandler.readTag("dbName", projectId);
            /*db db = new db(dbNames[0], 12);
            foreach (var schemeValue in list)
            {
                object value = db.singleItemSelectPostgres(schemeValue.columnName, schemeValue.tableName, null);
                ResponseValue responseValue = new ResponseValue();
                responseValue.tableName = schemeValue.tableName;
                responseValue.columnName = schemeValue.columnName;
                responseValue.value = value;
                responseList.Add(responseValue);
            }
            db.connection.Close();*/
            data = responseList;
            return data;
        }
    }
}