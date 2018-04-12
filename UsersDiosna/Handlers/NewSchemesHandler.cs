using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsersDiosna.Controllers;
using UsersDiosna.Sheme.Models;
using VizuLibrabrarySnapshotVals;

namespace UsersDiosna.Handlers
{
    public class NewSchemesHandler
    {
        public async Task<object> putSnapshotDataIntoFile(List<RequestValue> list, int projectId = 0, int pkTime = 0)
        {
            object data = new object();
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
           // data = responseList;
            return data;
        }
    }
}