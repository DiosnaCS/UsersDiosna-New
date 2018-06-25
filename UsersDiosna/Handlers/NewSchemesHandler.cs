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
        public async Task<object> putSnapshotDataIntoFile
            (List<RequestValue> list, int projectId = 0, int pkTime = 0)
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
            if (!path.Contains(PathDef.PhysicalPath))
            {
                path = PathDef.PhysicalPath + path;
            }
            string xmlConfig = System.IO.File.ReadAllText(path);
            SvgConfig svgConfig = new SvgConfig();
            var serializer = new XmlSerializer(typeof(SvgConfig));
            //object result;
            //StringReader stringReader = new StringReader(xmlConfig);
            StreamReader reader = new StreamReader(path);
            svgConfig = (SvgConfig)serializer.Deserialize(reader);
            //svgConfig = (SvgConfig)result;
            return svgConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToSvg"></param>
        /// <param name="svgConfig"></param>
        public List<SvgElement> readScheme(string pathToSvg, SvgConfig svgConfig)
        {
            List<SvgElement> elements = new List<SvgElement>();
            SvgDocument svg = SvgDocument.Open(pathToSvg);
            foreach (SchemeValue schemeVal in svgConfig.BindingTags)
            {
                SvgElement element = readSchemeTag(svg, schemeVal.id);
                elements.Add(element);
            }
            return elements;
        }
        /// <summary>
        /// Read from svg a tag and 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private SvgElement readSchemeTag(SvgDocument svg, string bindingTagId)
        {
            SvgElement element = svg.GetElementById(bindingTagId);
            return element;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingTags"></param>
        /// <param name="projectId"></param>
        public List<ResponseValue> readData(List<SchemeValue> bindingTags, int projectId)
        {
            List<SchemeValue> listTagsToDB = new List<SchemeValue>();
            foreach (var tag in bindingTags)
            {
                string trimedColName = tag.columnName.Trim();
                if(trimedColName == "snapshot")
                {
                   
                }
                else
                {

                    listTagsToDB.Add(tag);
                }
            }
            if(listTagsToDB.Count >= 1)
            {
               return getDBData(listTagsToDB, projectId);
            }
            return null;
        }
        public SvgDocument setValue(List<ResponseValue> responseValues, SvgConfig config, string pathToSvg)
        {
            if (!pathToSvg.Contains(PathDef.PhysicalPath))
            { 
                pathToSvg = PathDef.PhysicalPath + pathToSvg;
            }
            SvgDocument svg = SvgDocument.Open(pathToSvg);           
            foreach (var responseVar in responseValues)
            {
                switch(responseVar.Type)
                {
                    case SchemeType.GraphicList:
                        var graphiclistConfig = config.SchemeGraphicsList.First(p => p.id == responseVar.Id);
                        int i = 0;
                        while (i < 1000)
                        {
                            string id = responseVar.Id + "#" + i;
                            if (svg.GetElementById(id) is SvgTextSpan)
                            {
                                var element = (SvgImage)svg.GetElementById(id);
                                setGraphiclist(responseVar, graphiclistConfig, ref element);
                            }
                            else
                            {
                                i++;
                            }
                        }
                        break;
                    case SchemeType.AgeBarVertical:
                        break; 
                    case SchemeType.DynValue:
                        if (config.SchemeTags.Exists(p => p.id == responseVar.Id))
                        {
                            var dynValueconfig = config.SchemeTags.First(p => p.id == responseVar.Id);
                            SvgTextSpan element;
                            int j = 0;
                            while (j<1000) {
                                string id = responseVar.Id + "#" + j;
                                if (svg.GetElementById(id) is SvgTextSpan)
                                {
                                    element = (SvgTextSpan)svg.GetElementById(id);
                                    setDynValue(responseVar, dynValueconfig, ref element);
                                    break;
                                }
                                else
                                {
                                    j++;
                                }
                            }
                        }
                        break;
                    case SchemeType.AgeBar:
                        break;
                }
            }
            return svg;
        }

        private void setDynValue(ResponseValue responseValue, DynValue dynValueConfig, ref SvgTextSpan svgElement)
        {
           
            //svg.Children.Remove(element);
            double value = (double)responseValue.value;
            double dValue = (value + dynValueConfig.offset) * dynValueConfig.ratio;
            string newValue =  dValue + dynValueConfig.unit;
            svgElement.Text = newValue;
            //svg.Nodes.Add(element);
        }

        private void setGraphiclist(ResponseValue responseValue, Graphiclist graphiclistConfig, ref SvgImage svgElement)
        {           
            
            int value = (int)responseValue.value;
            string path = graphiclistConfig.items[value].path;
            if (path.Contains(PathDef.PhysicalPath))
                path = path.Replace(PathDef.PhysicalPath, "");
            var newValue = path;

            svgElement.Href = newValue;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        private List<ResponseValue> getDBData(List<SchemeValue>list, int projectId)
        {
            object data = new object();
            List<ResponseValue> responseList = new List<ResponseValue>();
            List<string> dbNames = XMLHandler.readTag("dbName", projectId);
            db db = new db(dbNames[0], 12);
            foreach (var schemeValue in list)
            {
                object value = db.singleItemSelectPostgres(schemeValue.columnName, schemeValue.tableName, null);
                ResponseValue responseValue = new ResponseValue();
                responseValue.Id = schemeValue.id;
                responseValue.Type = schemeValue.Type;
                responseValue.value = value;
                responseList.Add(responseValue);
            }
            db.connection.Close();
            return responseList;
        }
    }
}