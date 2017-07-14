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

        public List<string> getTanknames(int congirationNumber = 0)
        {
            XmlDocument xml = new XmlDocument();
            const string fileName = "Report_config*";
            List<string> tankNames = new List<string>();


            string[] absoulte_path = Directory.GetFiles(path, fileName);
            xml.Load(absoulte_path[0]);
            XmlNodeList xnList = xml.SelectNodes("//");
            foreach (XmlNode node in xnList) {
                string tankName = node.InnerText;
                tankNames.Add(tankName);
            }
            return tankNames;
        }
    }
}