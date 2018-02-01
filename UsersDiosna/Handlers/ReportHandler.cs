using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using UsersDiosna.Report.Models;
using UsersDiosna.Handlers;

namespace UsersDiosna
{
    public class ReportHandler
    {
        private static string path = Path.PhysicalPath + @"\Config";
        public Dictionary<int, string> getTanknames(int congirationNumber = 1)
        {
            XmlDocument xml = new XmlDocument();
            const string fileName = "Report_config*";
            Dictionary<int, string> tankNames = new Dictionary<int, string>();


            string[] absoulte_path = Directory.GetFiles(path, fileName);
            xml.Load(absoulte_path[0]);
            string xpath = string.Format("/configuration/names[{0}]", congirationNumber);
            XmlNodeList xnList = xml.SelectNodes(xpath);
            foreach (XmlNode node in xnList[0].ChildNodes) {
                string tankName = node.Name;
                int tankIdx = int.Parse(node.InnerText);
                tankNames.Add(tankIdx, tankName);
            }
            return tankNames;
        }

        /// <summary>
        /// it only switch label of info column in batch detail
        /// </summary>
        /// <param name="item">recipe step</param>
        public static string getInfoColumn(ColumnReportModel item) {
            string info = "";
            #region OPerationsSwitch
            switch ((int) item.RecordType)
            {
                case 10:
                    if(item.Variant1 != 0)
                        info += string.Format("Recipe number: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Recipe type: {0}", (RcpType) item.Variant2);
                    if (item.Variant4 != 0)
                        info += string.Format(" Jumped to step: {0}", (Operations) item.Variant4);
                    return info;
                case 11:
                    if (item.Variant1 != 0)
                        info += string.Format("Recipe number: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Caused by: {0}", (Cause) item.Variant2);
                    return info;
                case 12:
                    if (item.Variant1 != 0)
                        info += string.Format("Recipe number: {0}", item.Variant1);
                    return info;
                case 13:
                    if (item.Variant1 != 0)
                        info += string.Format("Recipe number: {0}", item.Variant1);
                    if (item.Variant4 != 0)
                        info += string.Format(" Caused by: {0}", (Operations)item.Variant4);
                    return info;
                case 20:
                    if (item.Variant1 != 0)
                        info += string.Format("Source: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Filling type: {0}", (FillType)item.Variant2);
                    if (item.Variant3 != 0)
                        info += string.Format(" Batch number: {0}", item.Variant3);
                    if (item.Variant4 != 0)
                        info += string.Format(" Current temp.: {0}.{1} °C", (item.Variant4 / 10), (item.Variant4 % 10));
                    return info;
                case 21:
                    if (item.Variant1 != 0)
                        info += string.Format("Source Tank: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Filling type: {0}", (FillType)item.Variant2);
                    if (item.Variant3 != 0)
                        info += string.Format(" Source batch number: {0}", item.Variant3);
                    if (item.Variant4 != 0)
                        info += string.Format(" Current temp.: {0}.{1} °C", (item.Variant4 / 10), (item.Variant4 % 10));
                    return info;
                case 22:
                    if (item.Variant1 != 0)
                        info += string.Format(" Water source: {0}", (WaterSource)item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Filling type: {0}", (FillType)item.Variant2);
                    if (item.Variant3 != 0)
                        info += string.Format(" Need temp.: {0}.{1} °C", (item.Variant3 / 10), (item.Variant3 % 10));
                    if (item.Variant4 != 0)
                        info += string.Format(" Current temp.: {0}.{1} °C", (item.Variant4 / 10), (item.Variant4 % 10));
                    return info;
                case 23:
                    if (item.Variant2 != 0)
                        info += string.Format(" Filling type: {0}", (FillType)item.Variant1);
                    return info;
                case 24:
                    if (item.Variant1 != 0)
                        info += string.Format("Silo: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Filling type: {0}", (FillType)item.Variant2);
                    if (item.Variant3 != 0)
                        info += string.Format(" Lot number: {0}", item.Variant3);
                    if (item.Variant4 != 0)
                        info += string.Format(" Material number: {0}", item.Variant4);
                    return info;
                case 28:
                    if (item.Variant1 != 0)
                        info += string.Format("Silo: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Filling type: {0}", (FillType)item.Variant2);
                    if (item.Variant3 != 0)
                        info += string.Format(" Lot number: {0}", item.Variant3);
                    if (item.Variant4 != 0)
                        info += string.Format(" Material number: {0}", item.Variant4);
                    return info;
                case 29:
                    if (item.Variant1 != 0)
                        info += string.Format("Silo: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Filling type: {0}", (FillType)item.Variant2);
                    if (item.Variant3 != 0)
                        info += string.Format(" Lot number: {0}", item.Variant3);
                    if (item.Variant4 != 0)
                        info += string.Format(" Material number: {0}", item.Variant4);
                    return info;
                case 31:
                    if (item.Variant1 != 0)
                        info += string.Format("Capacity: {0}", Helpers.ratio(item.Variant1,1));
                    if (item.Variant2 != 0)
                        info += string.Format(" Pump speed: {0} Hz", Helpers.ratio(item.Variant2,1));
                    if (item.Variant3 != 0)
                        info += string.Format(" needed TA: {0}", item.Variant3);
                    if (item.Variant4 != 0)
                        info += string.Format(" actual TA: {0}", item.Variant4);
                    return info;
                case 32:
                    if (item.Variant3 != 0)
                        info += string.Format("FWD: pause {0} s run {1} s", (int)item.Variant3/10000, item.Variant3%10000);
                    if (item.Variant4 != 0)
                        info += string.Format(" REV: pause {0} s run {1} s", (int)item.Variant4/10000, item.Variant4 % 10000);
                    return info;
                case 33:
                    if (item.Variant3 != 0)
                        info += string.Format("FWD: pause {0} s run {1} s", (int)item.Variant3/10000, item.Variant3 % 10000);
                    if (item.Variant4 != 0)
                        info += string.Format(" REV: pause {0} s run {1} s", (int)item.Variant4/10000, item.Variant4 % 10000);
                    return info;
                case 35:
                    if (item.Variant1 != 0)
                        info += string.Format("Need temp.: {0}.{1} °C", (item.Variant1 / 10), (item.Variant1 % 10));
                    if (item.Variant3 != 0)
                        info += string.Format(" FWD: pause {0} s run {1} s", (int)(item.Variant3/10000), item.Variant3 % 10000);
                    if (item.Variant4 != 0)
                        info += string.Format(" REV: pause {0} s run {1} s", (int)item.Variant4 / 10000, item.Variant4 % 10000);
                    return info;
                case 36:
                    if (item.Variant1 != 0)
                        info += string.Format("Need temp.: {0}.{1} °C", (item.Variant1 / 10), (item.Variant1 % 10));
                    if (item.Variant2 != 0)
                        info += string.Format(" End temp.: {0}.{1} °C", (item.Variant2 / 10), (item.Variant2 % 10));
                    if (item.Variant3 != 0)
                        info += string.Format(" FWD: pause {0} s run {1} s", (int)item.Variant3/10000, item.Variant3 % 10000);
                    if (item.Variant4 != 0)
                        info += string.Format(" REV: pause {0} s run {1} s", (int)item.Variant4/10000, item.Variant4 % 10000);
                    return info;
                case 37:
                    if (item.Variant3 != 0)
                        info += string.Format("FWD: pause {0} s run {1} s", (int)item.Variant3/10000, item.Variant3 % 10000);
                    if (item.Variant4 != 0)
                        info += string.Format(" REV: pause {0} s run {1} s", (int)item.Variant4/10000, item.Variant4 % 10000);
                    return info;
                case 39:
                    if (item.Variant1 != 0)
                        info += string.Format("Current temp.: {0}.{1} °C", (item.Variant1 / 10), (item.Variant1 % 10));
                    if (item.Variant2 != 0)
                        info += string.Format(" Current ph: {0} ", Helpers.ratio(item.Variant2,2));
                    if (item.Variant3 != 0)
                        info += string.Format("FWD: pause {0} s run {1} s", (int)item.Variant3/10000, item.Variant3 % 10000);
                    if (item.Variant4 != 0)
                        info += string.Format(" REV: pause {0} s run {1} s", (int)item.Variant4/10000, item.Variant4 % 10000);
                    return info;
                case 45:
                    if (item.Variant1 != 0)
                        info += string.Format("Second  destination: {0}", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Started by: {0} ",(StartedBy) item.Variant2);
                    return info;
                case 46:
                    info += string.Format("Clean type: {0}",CleanNames.ClnTypeName[item.Variant1]);
                    if (item.Variant2 != 0)
                        info += string.Format(" Started by: {0} ",(StartedBy) item.Variant2);
                    return info;

                default:
                    if (item.Variant1 != 0)
                        info += string.Format("Source tank: {0} ", item.Variant1);
                    if (item.Variant2 != 0)
                        info += string.Format(" Started by: {0} ",(StartedBy) item.Variant2);
                    if (item.Variant3 != 0)
                        info += string.Format("Source batch number", item.Variant3);
                    return info;
            }
#endregion
        }
    }

} 