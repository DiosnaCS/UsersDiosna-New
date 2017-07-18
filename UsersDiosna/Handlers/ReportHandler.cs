using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace UsersDiosna
{
    public class ReportHandler
    {
        private static string path = Path.physicalPath + @"\Config";

        public Dictionary<int, string> getTanknames(int congirationNumber = 1)
        {
            XmlDocument xml = new XmlDocument();
            const string fileName = "Report_config*";
            Dictionary<int, string> tankNames = new Dictionary<int, string>();


            string[] absoulte_path = Directory.GetFiles(path, fileName);
            xml.Load(absoulte_path[0]);
            string xpath = string.Format("/configuration[{0}]/tankNames", congirationNumber);
            XmlNodeList xnList = xml.SelectNodes(xpath);
            foreach (XmlNode node in xnList[0].ChildNodes) {
                string tankName = node.Name;
                int tankIdx = int.Parse(node.InnerText);
                tankNames.Add(tankIdx, tankName);
            }
            return tankNames;
        }
    }
}