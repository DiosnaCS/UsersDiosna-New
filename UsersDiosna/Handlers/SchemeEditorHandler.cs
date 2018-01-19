using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Svg;
using System.Drawing;
using UsersDiosna.Sheme.Models;
using System.Xml.Serialization;
using System.IO;

namespace UsersDiosna.Handlers
{
    public class SchemeEditorHandler
    {
        public static void getGraphicLists(string pathSvgCfg, List<string> subGraphicDir, List<string> pathGraphicCfg,List<Graphiclist> graphicLists)
        {
            foreach (string subDir in subGraphicDir)
            {
                foreach (string pathCfg in pathGraphicCfg)
                {
                    var lines = System.IO.File.ReadAllLines(pathCfg).Select(line => line.Split(new char[] { '\t' }));
                    List<string[]> graphiclistItems = lines.Where(line => line.Length != 0).ToList();
                    Graphiclist graphiclist = new Graphiclist();
                    graphiclist.items = new List<GraphiclistItem>();
                    foreach (string[] item in graphiclistItems)
                    {
                        GraphiclistItem graphiclistItem = new GraphiclistItem();
                        graphiclistItem.index = int.Parse(item[0]);
                        if (item.Length > 1)
                        {
                            graphiclistItem.path = item[1];
                        }
                        else
                        {
                            string mask;
                            if (graphiclistItem.index < 10)
                            {
                                mask = "*_0" + graphiclistItem.index + "*";
                            }
                            else
                            {
                                mask = "*_" + graphiclistItem.index + "*";
                            }
                            string[] path = Directory.GetFiles(pathCfg.Substring(0, pathCfg.LastIndexOf("\\")) + subDir, mask);
                            if (path.Length == 0)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                graphiclistItem.path = path[0];
                            }
                        }
                        graphiclist.items.Add(graphiclistItem);
                        graphiclist.name = subDir;
                        graphiclist.name = graphiclist.name.Replace("/", string.Empty);
                    }
                    graphicLists.Add(graphiclist);
                }
            }
        }

        public static void getTextlists(List<string> pathesToCfg, string pathSvgCfg, List<Textlist> textLists)
        {
            foreach (string path in pathesToCfg)
            {
                var file = System.IO.File.ReadAllLines(path).Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
                List<string[]> textlistItems = file.Where(line => line.Length != 0).ToList();
                Textlist textlist = new Textlist();
                textlist.items = new List<TextlistItem>();
                foreach (string[] item in textlistItems)
                {
                    TextlistItem textlistItem = new TextlistItem();
                    textlistItem.index = int.Parse(item[0]);
                    textlistItem.value = item[1];
                    if (item.Length > 2)
                    {

                    } else
                    {
                        textlistItem.bgColor = ColorTranslator.ToHtml(Color.White);
                        textlistItem.textColor = ColorTranslator.ToHtml(Color.Black);
                    }
                    textlist.items.Add(textlistItem);
                }
                string textlistName = path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - path.LastIndexOf("\\")-1);
                textlist.name = textlistName;
                textLists.Add(textlist);
            }
        }

        public static void getAgeBar(string pathSvgCfg, string ageBarsCfgPath, List<AgeBar> ageBarList)
        {
            var lines = System.IO.File.ReadAllLines(ageBarsCfgPath).Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
            List<string[]> ageBars = lines.Where(line => line.Length != 0).ToList();

            foreach (string[] ageBar in ageBars)
            {
                AgeBar AB = new AgeBar();

                AB.id = ageBar[0];
                AB.maxAge = int.Parse(ageBar[3]);
                AB.firstColor = ageBar[4];
                AB.firstLimit = int.Parse(ageBar[5]);
                AB.secondColor = ageBar[6];
                AB.secLimit = int.Parse(ageBar[7]);
                AB.thirdColor = ageBar[8];

                ageBarList.Add(AB);
            }
        }

        public static void getDynValues(string pathSvgCfg, string dynValuesCfg, List<DynValue> values)
        {
            var lines = System.IO.File.ReadAllLines(dynValuesCfg).Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
            List<string[]> dynValueList = lines.Where(line => line.Length != 0).ToList();
            
            foreach (string[] dynValue in dynValueList)
            {
                DynValue value = new DynValue();

                value.id = dynValue[0];
                //value.column = dynValue[1];
                //value.table = dynValue[2];
                value.ratio = int.Parse(dynValue[1]);
                value.offset = int.Parse(dynValue[2]);
                value.unit = dynValue[3];
                value.textColor = dynValue[4];

                values.Add(value);
            }
        }

        public static void getBindingTags(string pathSvgCfg, string bindingTagsCfg, List<SchemeValue> bindingTagList)
        {
            List<string> lines = System.IO.File.ReadAllLines(bindingTagsCfg).ToList();

            //lines.ForEach(line => line.Where(p => char.IsWhiteSpace(p)).Select(q => line.Replace(q, '\t')));
            //List<string[]> bindingList = lines.Select(eachLine => eachLine.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)).ToList();

            //Changing whiteSpaces to tabulators and split via tabulators and all empty parts throw away            
            List<string[]> bindingList = lines.Select(eachLine => eachLine.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries)).ToList();
            foreach (string[] dynValue in bindingList)
            {
                SchemeValue bindingTag = new SchemeValue();

                bindingTag.id = dynValue[0];
                bindingTag.columnName = dynValue[1];
                bindingTag.tableName = dynValue[2];
                bindingTag.Type = dynValue[3];
                bindingTagList.Add(bindingTag);
            }
        }

        public static void writeToXML(string pathSvgCfg, SchemeEditor editor)
        {
            SchemeEditorXML schemeEditorXML = new SchemeEditorXML();
            schemeEditorXML.SchemeAgeBars = editor.SchemeAgeBars;
            schemeEditorXML.SchemeGraphicsList = editor.SchemeGraphicsList;
            schemeEditorXML.SchemeTags = editor.SchemeTags;
            schemeEditorXML.SchemeTextlist = editor.SchemeTextlist;
            schemeEditorXML.BindingTags = editor.BindingTags;
            XmlSerializer serializer = new XmlSerializer(typeof(SchemeEditorXML));
            using (StreamWriter writer = new StreamWriter(pathSvgCfg))
            {
                serializer.Serialize(writer, schemeEditorXML);
            }
        }
    }
}