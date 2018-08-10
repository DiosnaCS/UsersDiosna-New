using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UsersDiosna.Controllers;
using UsersDiosna.Sheme.Models;
using VizuLibrabrarySnapshotVals;

namespace UsersDiosna.Handlers
{
    public class NewSchemesHandler
    {
        public void putSnapshotDataIntoFile
            (List<RequestValue> snapshotValues, int projectId = 0, int pkTime = 0)
        {
            //path to data for this day
            string pathForThisDay;
            if (Path.PhysicalPath.EndsWith(@"\"))
            {
                pathForThisDay = Path.PhysicalPath + DateTime.Now.ToShortDateString();
            } else
            {
                pathForThisDay = Path.PhysicalPath + @"\" + DateTime.Now.ToShortDateString();
            }

            if (Directory.Exists(pathForThisDay) == false)
            {
                //creates directory for this day
                Directory.CreateDirectory(pathForThisDay);
            }

            //solve path for this project
            string pathForthisProjectAndDay;
            if (projectId != 0)
            {
                pathForthisProjectAndDay = pathForThisDay + @"\" + projectId;
            }
            else
            {
                pathForthisProjectAndDay = pathForThisDay + @"\unknownProject";
            }

            if (Directory.Exists(pathForthisProjectAndDay) == false)
            {
                //creates directory for this project
                Directory.CreateDirectory(pathForthisProjectAndDay);
            }

            string dataPath = pathForthisProjectAndDay + @"\data.bin";
            if (System.IO.File.Exists(dataPath) == false)
            {
                System.IO.File.Create(dataPath);
            }
            Snapshot snapshot = new Snapshot();
            snapshot.TimeOfStorage = DateTime.Now;
            snapshot.SnapshotValues = snapshotValues;

            //serialize and write data into file
            BinaryFormatter binFormatter = new BinaryFormatter();
            //if (pkTime != 0)
            //{
            //    binFormatter.Serialize(System.IO.File.OpenWrite(dataPath), pkTime);
            //}
            binFormatter.Serialize(System.IO.File.OpenWrite(dataPath), snapshot);
        }

        public void SaveSnapshot(List<RequestValue> snapshotValues, int projectId = 0, int pkTime = 0)
        {
            Snapshot snapshot = new Snapshot();
            snapshot.TimeOfStorage = DateTime.Now;
            snapshot.SnapshotValues = snapshotValues;
        }

        private ResponseValue getDataFromSnapshot(SchemeValue tag, int projectId)
        {
            Snapshot snapshot = new Snapshot();
            string pathToDataForThisDay;
            if (Path.PhysicalPath.EndsWith(@"\"))
            {
                pathToDataForThisDay = Path.PhysicalPath + DateTime.Now.ToShortDateString();
            }
            else
            {
                pathToDataForThisDay = Path.PhysicalPath + @"\" + DateTime.Now.ToShortDateString();
            }

            RequestValue value = new RequestValue();
            if (System.IO.File.Exists(pathToDataForThisDay))
            {
                string pathToDataForThisDayAndThisProject = pathToDataForThisDay + @"\" + projectId;
                if (System.IO.File.Exists(pathToDataForThisDayAndThisProject) == true)
                {
                    FileStream fileStreamOfBinData = System.IO.File.OpenRead(pathToDataForThisDayAndThisProject + @"\data.bin");
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    snapshot = (Snapshot)binaryFormatter.Deserialize(fileStreamOfBinData);
                    //here should be only one element
                    value = snapshot.SnapshotValues.First(p => p.columnName == tag.columnName && p.tableName == tag.tableName);
                }
                else
                {

                }
            }
            else
            {
                Error.toFile("Warning no data found on : " +
                    DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), this.GetType().ToString());
            }

            ResponseValue responseValue = new ResponseValue();
            
            if (value.sValue != null)
            {
                responseValue.value = value.sValue;
            }
            else
            {
                responseValue.value = 0;
                if(value.rValue != 0)
                {
                    responseValue.value = value.rValue;
                }
                if(value.iValue != 0)
                {
                    responseValue.value = value.iValue;
                }
            }
            return responseValue;
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
            foreach (SchemeValue tag in bindingTags)
            {
                string trimedColName = tag.columnName.Trim().ToLower();
                if (trimedColName == "snapshot")
                {
                    getDataFromSnapshot(tag, projectId);
                }
                else
                {
                    listTagsToDB.Add(tag);
                }
            }
            if (listTagsToDB.Count >= 1)
            {
                return getDBData(listTagsToDB, projectId);
            }
            return null;
        }


        public SvgDocument setValue(List<ResponseValue> responseValues, SvgConfig config, string pathToSvg)
        {
            if (!pathToSvg.Contains(Path.PhysicalPath))
            {
                pathToSvg = Path.PhysicalPath + pathToSvg;
            }
            SvgDocument svg = SvgDocument.Open(pathToSvg);
            foreach (var responseVar in responseValues)
            {
                switch (responseVar.Type)
                {
                    case SchemeType.GraphicList:
                        refreshGraphicList(config, svg, responseVar);
                        break;
                    case SchemeType.Textlist:

                        break;
                    case SchemeType.AgeBarVertical:
                        
                        break;
                    case SchemeType.DynValue:
                        refreshDynValue(config, svg, responseVar);
                        break;
                    case SchemeType.AgeBar:
                        
                        break;
                }
            }
            return svg;
        }

        private void refreshTextlist(SvgConfig config, SvgDocument svg, ResponseValue responseVar)
        {
            string name = config.BindingTags.First(p => p.id == responseVar.Id).name;
            string idAB = config.BindingTags.First(p => p.id == responseVar.Id).id;
            Textlist textlistConfig = config.SchemeTextlist.First(p => p.id == responseVar.Id);
            int i = 0;
            while (i < 1000)
            {
                string idText = responseVar.Id + "#" + i;
                string idRect = responseVar.Id + "#" + ++i;
                if (svg.GetElementById(idRect) is SvgRectangle && svg.GetElementById(idText) is SvgTextSpan)
                {
                    var rectangle = (SvgRectangle)svg.GetElementById(idRect);
                    var svgText = (SvgTextSpan)svg.GetElementById(idText);
                    setTextlist(svg, responseVar, textlistConfig, ref rectangle, ref svgText);
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        private void refreshAgeBar(SvgConfig config, SvgDocument svg, ResponseValue responseVar)
        {
            AgeBar ageBarConfig = config.SchemeAgeBars.First(p => p.id == responseVar.Id);
            int i = 0;
            
            while (i < 1000)
            {
                string id = responseVar.Id + "#" + i;
                if (svg.GetElementById(id) is SvgRectangle)
                {
                    var element = (SvgRectangle)svg.GetElementById(id);
                    string defaultAgeBarId = responseVar.Id + "initial";
                    SvgRectangle defaultAgeBar;
                    if (svg.GetElementById(defaultAgeBarId) is SvgRectangle)
                    {
                        defaultAgeBar = element;
                    }
                    else
                    {
                        defaultAgeBar = createDefaultAgeBar(element, defaultAgeBarId);
                    }
                    setAgeBar(svg,responseVar, ageBarConfig, ref element,ref defaultAgeBar,false);
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        private SvgRectangle createDefaultAgeBar(SvgRectangle element, string defaultAgebarId)
        {
            SvgRectangle defaultRectangle = new SvgRectangle();
            defaultRectangle.X = element.X;
            defaultRectangle.Y = element.Y;
            defaultRectangle.Width = element.Width;
            defaultRectangle.Height = element.Height;
            defaultRectangle.ID = defaultAgebarId;
            defaultRectangle.Visible = false;
            return defaultRectangle;
        }

        private void refreshGraphicList(SvgConfig config, SvgDocument svg, ResponseValue responseVar)
        {
            string name = config.BindingTags.First(p => p.id == responseVar.Id).name;
            string idG = config.BindingTags.First(p => p.id == responseVar.Id).id;
            config.SchemeGraphicsList.First(p => p.name == name).id = idG;
            Graphiclist graphiclistConfig = config.SchemeGraphicsList.First(p => p.id == responseVar.Id);
            int i = 0;
            while (i < 1000)
            {
                string id = responseVar.Id + "#" + i;
                if (svg.GetElementById(id) is SvgImage)
                {
                    var element = (SvgImage)svg.GetElementById(id);
                    setGraphiclist(responseVar, graphiclistConfig, ref element);
                    break;
                }
                else
                {
                    i++;
                }
            }
        }
        
        private void refreshDynValue(SvgConfig config, SvgDocument svg, ResponseValue responseVar)
        {
            if (config.SchemeTags.Exists(p => p.id == responseVar.Id))
            {
                var dynValueconfig = config.SchemeTags.First(p => p.id == responseVar.Id);
                SvgTextSpan element;
                int j = 0;
                while (j < 1000)
                {
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
        }

        private void setTextlist(SvgDocument svg, ResponseValue responseVar, Textlist textlist, ref SvgRectangle rectangle,ref SvgTextSpan svgText)
        {
            int textId = int.Parse(responseVar.value.ToString());
            SvgColourServer rectColor = new SvgColourServer(Color.FromName(textlist.items[textId].bgColor));
            rectangle.Color = rectColor;

            svgText.Text = textlist.items[textId].value;
            SvgColourServer textColor = new SvgColourServer(Color.FromName(textlist.items[textId].textColor));
            svgText.Color = textColor; 
        }

        private void setAgeBar(SvgDocument svg, ResponseValue responseValue, AgeBar ageBar, ref SvgRectangle rectangle, ref SvgRectangle defaultRectangle, bool vertical = false)
        {
            float value = (float)responseValue.value;
            
            if(vertical == true)
            {
                var widthType = defaultRectangle.Width.Type;
                float widthValue = (defaultRectangle.Width.Value / ageBar.maxAge) * value;

                SvgUnit width = new SvgUnit(widthType, widthValue);
                rectangle.Width = width;
                if (widthValue > ageBar.firstLimit && widthValue <ageBar.secLimit)
                {
                    SvgColourServer rectangleColor = new SvgColourServer(Color.FromName(ageBar.firstColor));
                    rectangle.Color = rectangleColor;
                }
                else if (widthValue > ageBar.secLimit)
                {
                    SvgColourServer rectangleColor = new SvgColourServer(Color.FromName(ageBar.secondColor));
                    rectangle.Color = rectangleColor;
                }
                else 
                {
                    SvgColourServer rectangleColor = new SvgColourServer(Color.FromName(ageBar.thirdColor));
                    rectangle.Color = rectangleColor;
                }
            }
            else
            {
                //initial height is missing in config
                SvgColourServer rectangleColor = new SvgColourServer(Color.FromName(ageBar.thirdColor));
                rectangle.Color = rectangleColor;
                var heightType = defaultRectangle.Height.Type;
                float heightValue = (defaultRectangle.Height.Value / ageBar.maxAge) * value;
                SvgUnit height = new SvgUnit(heightType, heightValue);
                rectangle.Height = heightValue;
            }
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
            int value = int.Parse(responseValue.value.ToString());
            string path = graphiclistConfig.items[value].path;
            string physicalPathLower = Path.PhysicalPath.ToLowerInvariant();
            string oldPathLower = path.ToLowerInvariant();
            if (oldPathLower.Contains(physicalPathLower))
                oldPathLower = oldPathLower.Replace(physicalPathLower, "");
            var newValue = oldPathLower;

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