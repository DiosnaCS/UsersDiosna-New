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
                    XmlSerializer serializer = new XmlSerializer(typeof(Graphiclist));
                    using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
                    {
                        serializer.Serialize(writer, graphiclist);
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
                XmlSerializer serializer = new XmlSerializer(typeof(Textlist));
                using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
                {
                    serializer.Serialize(writer, textlist);
                }
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

                AB.id = int.Parse(ageBar[0]);
                AB.column = ageBar[1];
                AB.table = ageBar[2];
                AB.maxAge = int.Parse(ageBar[3]);
                AB.firstColor = ageBar[4];
                AB.firstLimit = int.Parse(ageBar[5]);
                AB.secondColor = ageBar[6];
                AB.secLimit = int.Parse(ageBar[7]);
                AB.thirdColor = ageBar[8];

                ageBarList.Add(AB);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<AgeBar>));
            using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
            {
                serializer.Serialize(writer, ageBarList);
            }
        }

        public static void getDynValues(string pathSvgCfg, string dynValuesCfg, List<DynValue> values)
        {
            var lines = System.IO.File.ReadAllLines(dynValuesCfg).Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
            List<string[]> dynValueList = lines.Where(line => line.Length != 0).ToList();
            
            foreach (string[] dynValue in dynValueList)
            {
                DynValue value = new DynValue();

                value.id = int.Parse(dynValue[0]);
                value.column = dynValue[1];
                value.table = dynValue[2];
                value.ratio = int.Parse(dynValue[3]);
                value.offset = int.Parse(dynValue[4]);
                value.unit = dynValue[5];
                value.textColor = dynValue[6];

                values.Add(value);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<DynValue>));
            using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
            {
                serializer.Serialize(writer, values);
            }
        }
    }
}