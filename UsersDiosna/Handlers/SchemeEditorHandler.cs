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
        /// <summary>
        /// The maximum image size supported.
        /// </summary>
        public static Size MaximumSize { get; set; }

        /// <summary>
        /// Converts an SVG file to a Bitmap image.
        /// </summary>
        /// <param name="filePath">The full path of the SVG image.</param>
        /// <returns>Returns the converted Bitmap image.</returns>
        public static Bitmap GetBitmapFromSVG(string filePath)
        {
            SvgDocument document = GetSvgDocument(filePath);

            Bitmap bmp = document.Draw();
            return bmp;
        }

        /// <summary>
        /// Gets a SvgDocument for manipulation using the path provided.
        /// </summary>
        /// <param name="filePath">The path of the Bitmap image.</param>
        /// <returns>Returns the SVG Document.</returns>
        public static SvgDocument GetSvgDocument(string filePath)
        {
            SvgDocument document = SvgDocument.Open(filePath);
            return document;
        }

        private static SvgDocument AdjustSize(SvgDocument document)
        {
            if (document.Height > MaximumSize.Height)
            {
                document.Width = (int)((document.Width / (double)document.Height) * MaximumSize.Height);
                document.Height = MaximumSize.Height;
            }
            return document;
        }

        public static void getGraphicLists(string pathSvgCfg, string subGraphicDir, List<string> pathGraphicCfg)
        {
            foreach (string pathCfg in pathGraphicCfg)
            {
                var lines = System.IO.File.ReadAllLines(pathCfg).Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
                var graphiclistItems = lines.Where(line => line.Length != 0).ToList();
                Graphiclist graphiclist = new Graphiclist();
                foreach (var item in graphiclistItems)
                {
                    GraphiclistItem graphiclistItem = new GraphiclistItem();
                    graphiclistItem.index = int.Parse(item[0]);
                    if (item.Length > 1)
                    {
                        graphiclistItem.path = item[1];
                    }
                    else
                    {
                        string[] path = Directory.GetFiles(pathCfg + subGraphicDir, graphiclistItem.index + ".*");
                        graphiclistItem.path = path[0];
                    }
                    graphiclist.items.Add(graphiclistItem);
                    graphiclist.name = subGraphicDir;
                    XmlSerializer serializer = new XmlSerializer(typeof(Textlist));
                    using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
                    {
                        serializer.Serialize(writer, graphiclist);
                    }
                }
            }
        }

        public static void getTextlists(List<string> pathesToCfg, string pathSvgCfg)
        {
            foreach (string path in pathesToCfg)
            {
                var file = System.IO.File.ReadAllLines(path).Select(line => line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries));
                var textlistItems = file.Where(line => line.Length != 0).ToList();
                Textlist textlist = new Textlist();
                textlist.items = new List<TextlistItem>();
                foreach (var item in textlistItems)
                {
                    TextlistItem textlistItem = new TextlistItem();
                    textlistItem.index = int.Parse(item[0]);
                    textlistItem.value = item[1];
                    textlistItem.bgColor = ColorTranslator.ToHtml(Color.White);
                    textlistItem.textColor = ColorTranslator.ToHtml(Color.Black);
                    textlist.items.Add(textlistItem);
                }
                string textlistName = path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - path.LastIndexOf("\\"));
                textlist.name = textlistName;
                XmlSerializer serializer = new XmlSerializer(typeof(Textlist));
                using (TextWriter writer = new StreamWriter(pathSvgCfg, append: true))
                {
                    serializer.Serialize(writer, textlist);
                }
            }
        }
    }
}