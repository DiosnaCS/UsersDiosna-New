﻿using System.Collections.Generic;
//using System.Web.UI.WebControls;
using System.Drawing;
using System;
using UsersDiosna.Controllers;
using System.Reflection;
using System.Text;
using UsersDiosna.Graph.Models;

namespace UsersDiosna.Graph.Models
{
    public class DatabaseDef {
        public int dbIdx;
        public string database;
        public int dataserverNumber;
    }
    public class DataRequest {
        public long beginTime; //in pkTime
        public long timeAxisLength; //in pkTime
        //public int viewLength;
        public List<Tag> tags { get; set; }
        public string errorMessage { get; set; }
    }
    public class Tag {
        public string table;
        public string column;
        public int period;
        public double[] vals;
    }
    public class LangDef
    {
        public string LangAbbreviation { get; set; } //Abreviation = "shortcut" 
    }

    public class LangDefinition
    {
        public static List<LangDef> LangDefList = new List<LangDef>() { new LangDef() { LangAbbreviation = "EN" }, new LangDef() { LangAbbreviation = "CZ" }, new LangDef() { LangAbbreviation = "DE" }, new LangDef() { LangAbbreviation = "PL" } };
        public static List<LangDef> LangEnbList = new List<LangDef>();
        public int Find(CIniFile config, string lang)
        {
            foreach (LangDef LangDef in config.LangDefList)
            {
                if (LangDef.LangAbbreviation.Contains(lang))
                {
                    return config.LangDefList.IndexOf(LangDef);
                }
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lang">lang which you want to add</param>
        /// <param name="position">Optional parameter</param>
        public static void Add(CIniFile config, string lang, int position = 0)
        {
            if (position == 0)
            {
                config.LangEnbList.Add(new LangDef() { LangAbbreviation = lang });
            }
            else
            {
                config.LangEnbList.Insert(position, new LangDef() { LangAbbreviation = lang });
            }
        }
        public static string toJSON(CIniFile config)
        {
            string json = "\"LangDef\": [";
            foreach (LangDef LangDef in config.LangDefList) {
                json += "\"" + LangDef.LangAbbreviation + "\",";
            }
            json = json.Substring(0, json.Length - 1);
            json += "],";
            json += "\"LangEnb\": [";
            foreach (LangDef LangDef in config.LangEnbList)
            {
                json += "\"" + LangDef.LangAbbreviation + "\",";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            return json;
        }

        public static void DeleteOtherLangs(CIniFile config, int langCount)
        {
            for (int i = langCount; i < config.LangDefList.Count - 1; i++)
            {
                config.LangDefList.RemoveAt(i);
            }
        }
    }

    public class TableDef
    {
        public string shortName { get; set; }
        public bool usePkTime { get; set; }
        public int dbIdx { get; set; }
        public string tabName { get; set; }
        public int period { get; set; }
    }

    public class TableDefinition
    {
        
        // public static List 
        public static string Find(CIniFile config, int ConnNo, string TabName)
        {
            foreach (TableDef TableDef in config.TableDefList)
            {
                if (TableDef.dbIdx == ConnNo & TableDef.tabName.Contains(TabName))
                {
                    return TableDef.shortName;
                }
            }
            return null;
        }

        public static TableDef FindTable(CIniFile config, string shortName)
        {
            foreach (TableDef TableDef in config.TableDefList)
            {
                if (TableDef.tabName.Contains(shortName))
                {
                    return TableDef;
                }
            }
            return null;
        }
        public static string Add(CIniFile config, int ConnNo, string TabName)
        {
            int subscoreIdx, iperiod;
            try {
                subscoreIdx = TabName.LastIndexOf("_");
            } catch (ArgumentNullException e) {
                throw new Exception(e.Message);
            }
            string shortedName = TabName.Substring(subscoreIdx+1);
            
            switch (shortedName) {
                case "xslow":
                    iperiod = 300;
                    break;
                case "slow":
                    iperiod = 60;
                    break;
                case "norm":
                    iperiod = 20;
                    break;
                case "sec10":
                    iperiod = 10;
                    break;
                case "sec5":
                    iperiod = 5;
                    break;
                default:
                    iperiod = 20;
                    break;
            }
            config.TableDefList.Add(new TableDef() { shortName = shortedName, dbIdx = ConnNo, tabName = TabName , period = iperiod});
            return shortedName;
        }

        public static string toJSON(CIniFile config)
        {
            string json = "\"TableDef\": [ ";
            foreach (TableDef TableDef in config.TableDefList)
            {
                json += "{";
                json += "\"shortName\":\"" + TableDef.shortName + "\",";
                json += "\"dbIdx\":" + TableDef.dbIdx + ",";
                json += "\"tabName\":\"" + TableDef.tabName + "\",";
                json += "\"period\":" + TableDef.period;
                json += "},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            return json;
        }
    }

    public class Values
    {
        public int idx { get; set; }
        public string[] langTexts { get; set; }
    }

    public class TextlistDef
    {
        public string textlist { get; set; }
        public List<Values> values { get; set; }
    }

    public class TextlistDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textlist"></param>
        /// <returns></returns>
        public static string Find(CIniFile config,string textlist)
        {
            foreach (TextlistDef TextlistDef in config.TextlistDefList) {
                if (TextlistDef.textlist.Contains(textlist))
                {
                    return TextlistDef.textlist;
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="textsArray"></param>
        /// <param name="Idxs"></param>
        /// <returns></returns>
        public static string Add(CIniFile config,string name,List<string[]> textsArray, List<int> Idxs)
        {
            List<Values> tempValues = new List<Values>();
            
            for (int i = 0;i<textsArray.Count;i++)//First row is name row - textlist name 
            {
                tempValues.Add(new Values() { idx= Idxs[i], langTexts= textsArray[i]});
            }
            config.TextlistDefList.Add(new TextlistDef() { textlist = name, values = tempValues });
            return name;
        }
        public  static bool  UpdateName(CIniFile config, string oldName, string newName) {
            foreach (TextlistDef TextlistDef in config.TextlistDefList)
            {
                if (TextlistDef.textlist.Contains(oldName))
                {
                    TextlistDef.textlist = newName;
                    return true;
                }
            }
            return false;
        }

        public static string toJSON(CIniFile config) {
            string json = "\"TextlistDef\": [";
            foreach (TextlistDef TextlistDef in config.TextlistDefList)
            {
                json += "{\"textlist\": \"" + TextlistDef.textlist + "\",";
                json += "\"values\": [";
                foreach (Values Values in TextlistDef.values)
                {
                    json += "{\"idx\":" + Values.idx + ",";
                    for (int i=0;i<Values.langTexts.Length; i++)
                    {
                        LangDef LangDef = config.LangEnbList[i];
                        json += "\"text_" + LangDef.LangAbbreviation + "\":\"" + Values.langTexts[i] + "\",";
                    }
                    json = json.Substring(0, json.Length - 1);
                    json += "},";
                }
                json = json.Substring(0, json.Length - 1);
                json += "]},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            return json;
        }
    }

    public class ColumnTextlist {
        public string column;
        public string TextlistName;
    }

    public class ColumnTextlistDefine {
        public static List<ColumnTextlist> ColumnTextlistList = new List<ColumnTextlist>();

        public static string FindtextlistName(string column)
        {
            foreach (ColumnTextlist ColumntextlistInstance in ColumnTextlistList)
            {
                if (ColumntextlistInstance.column.Contains(column))
                {
                    return ColumntextlistInstance.TextlistName;
                }
            }
            return null;
        }
        public static string Findcolumn(string TextlistName)
        {
            foreach (ColumnTextlist ColumntextlistInstance in ColumnTextlistList)
            {
                if (ColumntextlistInstance.TextlistName.Contains(TextlistName))
                {
                    return ColumntextlistInstance.column;
                }
            }
            return null;
        }
        public static void Add(string column, string TextlistName) {
            ColumnTextlistList.Add(new ColumnTextlist() { column = column, TextlistName = TextlistName });
        }
    }
    public class NameDef {
        public string table { get; set; }
        public string column { get; set; }
        public string[] fullNames { get; set; }
        public List<string> units { get; set; }
        public double multiplier = 1.0;
    }
    public class NameDefinition {
        
        public static string Find(CIniFile config, string column)
        {
            foreach (NameDef NameDef in config.NameDefList)
            {
                if (NameDef.column.Contains(column))
                {
                    return NameDef.column;
                }
            }
            return null;
        }
        public static string Add(CIniFile config, string ascolumn, string[] asfullNames, List<string> asunits, string astable = null)
        {
            config.NameDefList.Add(new NameDef() { table = astable, column = ascolumn, fullNames = asfullNames, units = asunits });
            return ascolumn;
        }

        public static string toJSON(CIniFile config) {
            string json = "\"NameDef\": [";
            foreach (NameDef NameDef in config.NameDefList)
            {
                json += "{";
                json += "\"table\":\"" + NameDef.table + "\", \"column\":\"" + NameDef.column +"\",";
                for (int i = 0; i < NameDef.fullNames.Length; i++)
                {
                    LangDef LangDef = config.LangEnbList[i];
                    if (NameDef.fullNames[i] != null)
                    {
                        if (NameDef.fullNames[i].Length != 0)
                        {
                            json += "\"fullName_" + LangDef.LangAbbreviation + "\":\"" + NameDef.fullNames[i] + "\",";
                        }
                    }
                }
                if (NameDef.units != null)
                {
                    for (int i = 0; i < NameDef.units.Count; i++)
                    {
                        LangDef LangDef = config.LangEnbList[i];
                        json += "\"unit_" + LangDef.LangAbbreviation + "\":\"" + NameDef.units[i] + "\",";
                    }
                }
                json = json.Substring(0, json.Length - 1);
                json += "},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            return json;
        }
    }

    public class Const
    {
        public static readonly string[] separators = {":", "=", "  ", "             ", ";", "\n" };
        public static readonly string[] separators_signal = { ":", "=", " ", ",", "  ", "             ", ";;", "\n" };
        public static readonly string[] separators_view = { "$", ",", ":", "=", "  ", "             ", ";;", "\n" };
        public static readonly string[] separ_equate = { "=" };
        public static readonly string[] separ_dollar = { "$" };
        public static readonly string[] separ_semicolon = { ";" };
        public static readonly string[] separ_names = { "$", "             ", ";" };
        public static readonly string[] separ_backslash = { @"\", "$", "             ", ";" };
        //New separators
        public static readonly string[] separ_allNames = { "$", ":", "=", "             ", ";", "\n" };
        public static readonly string[] separ_backslashNew = { @"\"};
    }

    public class CSigMultitext
    {
        public string type = "multitext";
        public string Table;
        public string Column;
        public Color Color;
        public string textlist;
        CSigMultitext(string asTabDefName, string asColumn, Color acColor, string asTextListDef)
        {
            Table = asTabDefName;
            Column = asColumn;
            Color = acColor;
            textlist = asTextListDef;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="separ_names_string">First row of that </param>
        /// <param name="separ_cfg_string"></param>
        /// <returns></returns>
        public static CSigMultitext FromIni(CIniFile config, string[] separ_cfg_string) {
            string TableDefName, Column, TableName, textlist;
            int ConnectionStringNumber;
            Color Color;

            ConnectionStringNumber = int.Parse(separ_cfg_string[1]);
            Column = separ_cfg_string[2];
            TableName = separ_cfg_string[3];
            TableDefName = TableDefinition.Find(config, ConnectionStringNumber, TableName);
            if (TableDefName == null)
            {
                TableDefinition.Add(config, ConnectionStringNumber, TableName);
            }
            Color = Color.FromArgb(int.Parse(separ_cfg_string[5]), int.Parse(separ_cfg_string[6]), int.Parse(separ_cfg_string[7]));

            textlist = ColumnTextlistDefine.FindtextlistName(separ_cfg_string[2]);
            if (textlist == null) {

            }

            return new CSigMultitext(TableDefName, Column, Color, textlist);
        }

        public string toJSON(CSigMultitext SigMultitext) {
            string json = @"{";

            json += "\"type\": \"" + SigMultitext.type + "\", ";
            json += "\"column\": \"" + SigMultitext.Column + "\", ";
            json += "\"table\": \"" + SigMultitext.Table + "\", ";
            json += "\"color\": \"" + ColorTranslator.ToHtml(SigMultitext.Color).ToString() + "\", ";
            json += "\"textlist\":\"" + SigMultitext.textlist + "\"},";

            return json;
        }
    }

    public class CSignal
    {
        public string type = "analog";
        public string table;
        public string column;//column 
        public Color Color;
        public int Decimal;
        CSignal(string asSigName, string asTabDefName, int aiDecimal, Color acColor)
        {
            column = asSigName;
            table = asTabDefName;
            Decimal = aiDecimal;
            Color = acColor;
        }

        public static CSignal FromIni(CIniFile config, string[] separ_cfg_string)
        {
            int ConnectionStringNumber;
            string SignalName, TableName, TableDefName;
            int Decimal;
            Color Color;         
            // priklad:   Signal=3:iWMU_Temp  arBF_norm 1 255,0,0
            //            0      1 2          3         4 5   6 7

            ConnectionStringNumber =  int.Parse(separ_cfg_string[1]);
            SignalName = separ_cfg_string[2];
            TableName = separ_cfg_string[3];
            TableDefName = TableDefinition.Find(config, ConnectionStringNumber, TableName);
            if (TableDefName == null )
            {
                TableDefName = TableDefinition.Add(config, ConnectionStringNumber, TableName);
            }   
            Decimal = int.Parse(separ_cfg_string[4]);
            Color = Color.FromArgb(int.Parse(separ_cfg_string[5]), int.Parse(separ_cfg_string[6]), int.Parse(separ_cfg_string[7]));          
            
            return new CSignal(SignalName, TableDefName, Decimal, Color);
        }

        static public CSignal FromJson(string sJson)
        {
            return null;
        }

        public  string toJSON(CSignal signal)
        {
            string json =  @"{";

            json += "\"type\": \"" + signal.type + "\", ";
            json += "\"table\": \"" + signal.table + "\", ";
            json += "\"column\": \"" + signal.column + "\", ";
            json += "\"color\": \"" + ColorTranslator.ToHtml(signal.Color).ToString() + "\", ";
            json += "\"decimal\":" + signal.Decimal + "},";

            return json;
        }
    }

    public class CField
    {
        public readonly int minY = 0;
        public int maxY;
        public int relSize;
        public List<CSignal> SigList = new List<CSignal>();
        public List<CSigMultitext> SigMultiList = new List<CSigMultitext>();
        CField(int maximalY, int realSize) {
            maxY = maximalY;
            relSize = realSize;
        }

        public static CField FromIni(string[] separ_string)
        {
            int maximalY, realSize;
            maximalY = int.Parse(separ_string[1]);
            realSize = int.Parse(separ_string[2]);
            return new CField(maximalY, realSize);
        }

        public void AddSignal(CSignal aSig)
        {            
            SigList.Add(aSig);
        }
        public void AddSignalMultitext(CSigMultitext aSigMulti)
        {
            SigMultiList.Add(aSigMulti);
        }

        public string toJSON(CField field)
        {
            string json = "{";
            json += "\"minY\":" + field.minY + ", ";
            json += "\"maxY\":" + field.maxY + ", ";
            //json += "\"unit\":" + field.unit + ", ";
            json += "\"relSize\":" + field.relSize + ", ";
            json += "\"signal\":[";
            if (SigList.Count != 0)
            {
                
                foreach (CSignal signal in SigList)
                {
                    string signalJSON = signal.toJSON(signal);
                    json += signalJSON;
                }
            }
            if (SigMultiList.Count != 0)
            {
                foreach (CSigMultitext SigMultitext in SigMultiList)
                {
                    string sigMultitextJSON = SigMultitext.toJSON(SigMultitext);
                    json += sigMultitextJSON;
                }
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            json += "},";
            return json;
        }

    }

    public class CView
    {
        public List<string> Names;
        public string defLang;
        public List<CField> FieldList = new List<CField>();
        CView(List<string> anames) {
            Names = anames;
            defLang = LangDefinition.LangDefList[0].LangAbbreviation; // first index is default lang
        }
        public static CView FromIni(string[] separ_string)
        {
            List<string> tempNames = new List<string>();
            string s;
            //Cycle starts on two beacause of skiping position of word View and number of view (ex. 3)
            for (int i=3;i<=separ_string.Length;i++) {
                s = separ_string[i-1];
                tempNames.Add(s);
            }
            return new CView(tempNames);
        }

        public void AddField(CField CFieldInstatance)
        {
            FieldList.Add(CFieldInstatance);
        }
        public string toJSON(CView view, CIniFile config)
        {
            string json = "{";
            int pos = 0;
            foreach (LangDef LangDef in config.LangEnbList)
            {
                if (pos < view.Names.Count) {
                    json += "\"name_" + LangDef.LangAbbreviation + "\":\""+ view.Names[pos] +"\", ";
                }
                pos++;
            }
            json += "\"defLang\": \"" + view.defLang + "\", ";
            if (FieldList.Count != 0) {
                json += "\"field\":[";
                foreach (CField field in FieldList)
                {
                    string fieldJSON = field.toJSON(field);
                    json += fieldJSON;
                }
                json = json.Substring(0, json.Length - 1);
                json += "]";
            }
            else {
                json = json.Substring(0, json.Length - 1);
            }
            json += "},";
            return json;
        }
    }

    public class CIniFile {
        public List<CView> ViewList = new List<CView>();
        public List<NameDef> NameDefList = new List<NameDef>();
        public List<TextlistDef> TextlistDefList = new List<TextlistDef>();
        public List<TableDef> TableDefList = new List<TableDef>();
        public List<LangDef> LangDefList = new List<LangDef>() { new LangDef() { LangAbbreviation = "EN" }, new LangDef() { LangAbbreviation = "CZ" }, new LangDef() { LangAbbreviation = "DE" }, new LangDef() { LangAbbreviation = "PL" } };
        public List<LangDef> LangEnbList = new List<LangDef>();
        public void AddView(CView CViewInstance)
        {
            ViewList.Add(CViewInstance);
        }

        public string toJSON(CIniFile IniFile)
        {
            string json = "{";
            json += LangDefinition.toJSON(this);
            json += ",";
            
            json += TableDefinition.toJSON(this);
            json += ",";
            json += NameDefinition.toJSON(this);
            json += ",";
            json += TextlistDefinition.toJSON(this);
            json += ",";
            json += "\"View\":[";
            foreach (CView view in ViewList)
            {
                json += view.toJSON(view, IniFile);
            }
            json = json.Substring(0, json.Length - 1);
            json += "]}";
            return json;
        }
    }
    public class Config
    {
        public List<View> View { get; set; }
        public List<NameDef> NameDef { get; set; }
        public List<TextlistDef> TextlistDef { get; set; }
        public List<TableDef> TableDef { get; set; }
        public List<LangDef> LangDefList = new List<LangDef>() { new LangDef() { LangAbbreviation = "EN" }, new LangDef() { LangAbbreviation = "CZ" }, new LangDef() { LangAbbreviation = "DE" }, new LangDef() { LangAbbreviation = "PL" } };
        public List<LangDef> LangEnbList { get; set; }
    }

    public class Signal
    {
        public string type { get; set; }
        public string table { get; set; }
        public string column { get; set; }
        public Color Color { get; set; }
        public int Decimal { get; set; }
    }

    public class Field
    {
        public int minY { get; set; }
        public int maxY { get; set; }
        public int relSize { get; set; }
        public List<Signal> signal { get; set; }
    }
    public class View
    {
        public List<string> name { get; set; }
        public string defLang { get; set; }
        public List<Field> field { get; set;}
    }

    /// <summary>
    ///     CfgStructure defines Graph config cfg structure
    /// </summary>

    ///     NamesStructure defines Graph config  names structure
    /// </summary>
}