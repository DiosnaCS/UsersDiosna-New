using Svg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SvgConfig readSchemeConfig(string path)
        {
            if (!path.Contains(Path.PhysicalPath))
            {
                path = Path.PhysicalPath + path;
            }
            string xmlConfig = System.IO.File.ReadAllText(path);
            SvgConfig svgConfig = new SvgConfig();
            var serializer = new XmlSerializer(typeof(SvgConfig));
            object result;

            using (TextReader reader = new StringReader(xmlConfig))
            {
                result = serializer.Deserialize(reader);
            }
            svgConfig = (SvgConfig)result;
            return svgConfig;
        }
        /// <summary>
        /// Read from svg a tag and 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public SvgElement readSchemeTag(string pathToSvg)
        {
            return ;
        }
    }
}