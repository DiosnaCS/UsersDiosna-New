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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="snapshotValues"></param>
        /// <param name="projectId"></param>
        /// <param name="pkTime"></param>
        /// <returns></returns>
        public object putSnapshotDataIntoFile
            (List<RequestValue> snapshotValues, int projectId = 0, int pkTime = 0)
        {
            //path to data for this day
            string pathForThisDay;
            if (PathDef.PhysicalPath.EndsWith(@"\"))
            {
                pathForThisDay = PathDef.PhysicalPath + DateTime.Now.ToShortDateString();
            } else
            {
                pathForThisDay = PathDef.PhysicalPath + @"\" + DateTime.Now.ToShortDateString();
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
                FileStream dataFile = System.IO.File.Create(dataPath);
                dataFile.Close();
            }
            Snapshot snapshot = new Snapshot();
            snapshot.TimeOfStorage = DateTime.Now;
            snapshot.SnapshotValues = snapshotValues;

            //serialize and write data into file
            BinaryFormatter binFormatter = new BinaryFormatter();
            using (FileStream dataFileStream = System.IO.File.Open(dataPath, FileMode.Create))
            {
                binFormatter.Serialize(dataFileStream, snapshot);
                //close file after append
            }
            //returns object with data which was sent do the user
            return snapshot;
        }

        public void SaveSnapshot(List<RequestValue> snapshotValues, int projectId = 0, int pkTime = 0)
        {
            Snapshot snapshot = new Snapshot();
            snapshot.TimeOfStorage = DateTime.Now;
            snapshot.SnapshotValues = snapshotValues;
        }

        private ResponseValue getDataFromSnapshot(SvgConfig config, SchemeValue tag, int projectId)
        {
            Snapshot snapshot = new Snapshot();
            string pathToDataForThisDay;
            if (PathDef.PhysicalPath.EndsWith(@"\"))
            {
                pathToDataForThisDay = PathDef.PhysicalPath + DateTime.Now.ToShortDateString();
            }
            else
            {
                pathToDataForThisDay = PathDef.PhysicalPath + @"\" + DateTime.Now.ToShortDateString();
            }
            if (pathToDataForThisDay.Contains("/"))
            {
                pathToDataForThisDay = pathToDataForThisDay.Replace("/", ".");
            }
            
            if (Directory.Exists(pathToDataForThisDay) == true)
            {
                string pathToDataForThisDayAndThisProject = pathToDataForThisDay + @"\" + projectId;
                if (Directory.Exists(pathToDataForThisDayAndThisProject) == true)
                {
                    FileStream fileStreamOfBinData = System.IO.File.OpenRead(pathToDataForThisDayAndThisProject + @"\data.bin");
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    snapshot = (Snapshot)binaryFormatter.Deserialize(fileStreamOfBinData);
                    //here should be only one element
                    RequestValue value = snapshot.SnapshotValues.FirstOrDefault(
                        p => tag.id.EndsWith(p.tableName) && tag.id.StartsWith(p.columnName));
                    ResponseValue responseValue = new ResponseValue();
                    if (value != null)
                    {
                        if (value.sValue != null)
                        {
                            responseValue.value = value.sValue;
                        }
                        else
                        {
                            responseValue.value = 0;
                            if (value.rValue != 0)
                            {
                                responseValue.value = value.rValue;
                            }
                            if (value.iValue != 0)
                            {
                                responseValue.value = value.iValue;
                            }
                        }
                        var bindingTag = config.BindingTags.FirstOrDefault(
                            p => p.id.EndsWith(value.tableName) && p.id.StartsWith(value.columnName));
                        if (bindingTag != null)
                        {
                            responseValue.Id = bindingTag.id;
                            responseValue.Type = bindingTag.Type;
                            return responseValue;
                        }
                    }
                }
                else
                {
                    Error.toFile("Warning no data found on : " +
                    DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), this.GetType().ToString());
                }
            }
            else
            {
                Error.toFile("Warning no data found on : " +
                    DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), this.GetType().ToString());
            }

            return null;
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
        public List<ResponseValue> readData(SvgConfig config, List<SchemeValue> bindingTags, int projectId)
        {
            List<ResponseValue> listTags = new List<ResponseValue>();
            List<SchemeValue> listTagsToDB = new List<SchemeValue>();
            foreach (SchemeValue tag in bindingTags)
            {
                string trimedColName = tag.columnName.Trim().ToLower();
                if (trimedColName == "snapshot")
                {
                    var valueFromSnapshot = getDataFromSnapshot(config, tag, projectId);

                    if (valueFromSnapshot != null)
                    {
                        if (valueFromSnapshot.Type != tag.Type)
                        {
                            valueFromSnapshot.Type = tag.Type;
                        }
                        listTags.Add(valueFromSnapshot);
                    }
                }
                else
                {
                    listTagsToDB.Add(tag);
                }
            }
            if (listTagsToDB.Count >= 1)
            {
               listTags.AddRange(getDBData(listTagsToDB, projectId));
            }
            return listTags;
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
                switch (responseVar.Type)
                {
                    case SchemeType.GraphicList:
                        refreshGraphicList(config, svg, responseVar);
                        break;
                    case SchemeType.Textlist:
                        refreshTextlist(config, svg, responseVar);
                        break;
                    case SchemeType.AgeBarVertical:
                        refreshAgeBar(config, svg, responseVar, true);
                        break;
                    case SchemeType.DynValue:
                        refreshDynValue(config, svg, responseVar);
                        break;
                    case SchemeType.AgeBar:
                        refreshAgeBar(config, svg, responseVar, false);
                        break;
                }
            }
            return svg;
        }

        private void refreshTextlist(SvgConfig config, SvgDocument svg, ResponseValue responseVar)
        {
            string name = config.BindingTags.First(p => p.id == responseVar.Id).name;
            string idAB = config.BindingTags.First(p => p.id == responseVar.Id).id;

            //yes we should save it (textlistConfig.Id) but this will be in the future
            var bindingTag = config.BindingTags.First(p => p.id == responseVar.Id);
            Textlist textlistConfig = config.SchemeTextlist.First(
                textlist => textlist.name == bindingTag.name);
            textlistConfig.id = bindingTag.id;
            
            int i = 0;
            while (i < 1000)
            {
                string idRect = responseVar.Id + "#" + i;
                string idText = responseVar.Id + "#" + ++i;
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

        private void refreshAgeBar(SvgConfig config, SvgDocument svg, ResponseValue responseVar,bool vertical)
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
                    setAgeBar(svg,responseVar, ageBarConfig, ref element,ref defaultAgeBar,vertical);
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
            rectangle.Fill = rectColor;

            svgText.Text = textlist.items[textId].value;
            SvgColourServer textColor = new SvgColourServer(Color.FromName(textlist.items[textId].textColor));
            svgText.Fill = textColor; 
        }

        private void setAgeBar(SvgDocument svg, ResponseValue responseValue, AgeBar ageBar, ref SvgRectangle rectangle, ref SvgRectangle defaultRectangle, bool vertical = false)
        {
            float value = float.Parse(responseValue.value.ToString());
            
            if(vertical == false)
            {
                var widthType = defaultRectangle.Width.Type;
                float widthValue = (defaultRectangle.Width.Value / ageBar.maxAge) * value;

                SvgUnit width = new SvgUnit(widthType, widthValue);
                if (widthValue > ageBar.firstLimit && widthValue <ageBar.secLimit)
                {
                    SvgColourServer rectangleColor = new SvgColourServer(Color.FromName(ageBar.firstColor));
                    rectangle.Fill = rectangleColor;
                }
                else if (widthValue > ageBar.secLimit)
                {
                    SvgColourServer rectangleColor = new SvgColourServer(Color.FromName(ageBar.secondColor));
                    rectangle.Fill = rectangleColor;
                }
                else 
                {
                    SvgColourServer rectangleColor = new SvgColourServer(Color.FromName(ageBar.thirdColor));
                    rectangle.Fill = rectangleColor;
                }
                rectangle.Width = width;
            }
            else
            {
                var c = Color.FromName(ageBar.thirdColor);
                //initial height is missing in config
                SvgColourServer rectangleColor = new SvgColourServer(c);
                rectangle.Fill = rectangleColor;
                var heightType = defaultRectangle.Height.Type;
                float heightValue = (defaultRectangle.Height.Value / ageBar.maxAge) * value;
                SvgUnit height = new SvgUnit(heightType, heightValue);
                rectangle.Height = heightValue;
            }
        }
        private void setDynValue(ResponseValue responseValue, DynValue dynValueConfig, ref SvgTextSpan svgElement)
        {
            string newValue = string.Empty;
            int iValue;
            double doubleValue;
            //svg.Children.Remove(element);
            if (responseValue.value is double)
            {
                doubleValue = (double)responseValue.value;
                double dValue = (doubleValue + dynValueConfig.offset) * dynValueConfig.ratio;
                newValue = dValue + dynValueConfig.unit;
            }
            else
            {
                if (int.TryParse(responseValue.value.ToString(), out iValue) == true)
                {
                    double dValue = (iValue + dynValueConfig.offset) * dynValueConfig.ratio;
                    newValue = dValue + dynValueConfig.unit;
                }
                if (newValue == string.Empty)
                {
                    newValue = responseValue.value.ToString();
                }
            }
            svgElement.Text = newValue;
            //svg.Nodes.Add(element);
        }

        private void setGraphiclist(ResponseValue responseValue, Graphiclist graphiclistConfig, ref SvgImage svgElement)
        {           
            int value = int.Parse(responseValue.value.ToString());
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