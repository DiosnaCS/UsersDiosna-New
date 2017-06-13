using UsersDiosna.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UsersDiosna.Controllers
{
    /// <summary>
    /// Class for parsing .ini configs using reading all lines. -- Bad written ini files (without sections)
    /// </summary>
    class Iniparser
    {
        private static string CfgPath;
        private static string NamePath;

        #region cfgparser
        public Iniparser(string aCfgPath, string aNamePath)
        {
            CfgPath = aCfgPath;
            NamePath = aNamePath;
        }
        public void ParseLangs(CIniFile config)
        {             
            string[] lines = System.IO.File.ReadAllLines(CfgPath, System.Text.Encoding.Default);

            for (int i = 0; i < lines.Length; i++)
            {
                if (!(lines[i].StartsWith("#")) && (lines[i].Length != 0))
                {
                    string[] langs = lines[i].Split(Const.separ_dollar, StringSplitOptions.None);
                    for (int j = 0; j< langs.Length; j++) {
                        if (langs[j].Length !=0) {
                            LangDefinition.Add(config, LangDefinition.LangDefList[j].LangAbbreviation);
                        }
                    }
                    i = lines.Length;
                }
            }
        }
        public void ParseCfg(CIniFile config, string[] separators, CIniFile iniFileInstance)
        {
            string[] separeted_string = null;

            CView lastView = null;
            CField lastField = null;
            CSignal lastSignal = null;
            CSigMultitext lastSigMultitext = null;
            string[] lines = System.IO.File.ReadAllLines(CfgPath, System.Text.Encoding.Default);

            for (int i = 0; i < lines.Length; i++)
            {
                separeted_string = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    if (!(lines[i].StartsWith("#")) && (lines[i].Length != 0))
                    {
                        switch (separeted_string[0])
                        {
                            case "View":
                                separeted_string = lines[i].Split(Const.separators_view, StringSplitOptions.RemoveEmptyEntries);
                                lastView = parseView(config, separeted_string);
                                iniFileInstance.AddView(lastView);
                                break;
                            case "Field":
                                lastField = parseField(config, separeted_string);
                                lastView.AddField(lastField);
                                break;
                            case "Signal":
                                separeted_string = lines[i].Split(Const.separators_signal, StringSplitOptions.RemoveEmptyEntries);
                                lastSignal = parseSignal(config, separeted_string);
                                lastField.AddSignal(lastSignal);
                                break;
                            case "SigMultitext":
                                separeted_string = lines[i].Split(Const.separators_signal, StringSplitOptions.RemoveEmptyEntries);
                                lastSigMultitext = parseSigMultitext(config, separeted_string);
                                lastField.AddSignalMultitext(lastSigMultitext);
                                break;
                        }
                    }
                }
                catch (Exception e) {
                    Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                }
            }
        }

        private CSigMultitext parseSigMultitext(CIniFile config,string[] separeted_string)
        {
            CSigMultitext sigMultitext = CSigMultitext.FromIni(config, separeted_string);
            return sigMultitext;
        }

        private CSignal parseSignal(CIniFile config, string[] separeted_string)
        {
           CSignal signal =  CSignal.FromIni(config, separeted_string);
           return signal;
        }

        private CField parseField(CIniFile config, string[] separeted_string)
        {
            CField field = CField.FromIni(separeted_string);
            return field;
        }

        private CView parseView(CIniFile config, string[] separeted_string)
        {
            CView view =CView.FromIni(separeted_string);
            return view;
        }
        #endregion

        #region newsectionNames
        /// <summary>
        /// New method to parse multitext
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private int parseMultitext(CIniFile config, List<string> lines, int startLineIdx, string name) {
            List<string[]> multitext_lines = new List<string[]>();
            List<int> Indexes = new List<int>();
            for (int i = startLineIdx; i < lines.Count; i++) {
                List<string> multitextLine = new List<string>();
                int id = 0;
                if (lines[i].Length == 0)
                {
                    TextlistDefinition.Add(config, name, multitext_lines, Indexes);
                    return i;
                }
                else {
                    multitextLine = lines[i].Split(Const.separ_semicolon, StringSplitOptions.RemoveEmptyEntries).Select( p => p.Trim()).ToList();
                    if (int.TryParse(multitextLine[0], out id) == true)
                    {
                        Indexes.Add(id); // 0 index
                        multitextLine = multitextLine.Skip(1).ToList();
                        multitext_lines.Add(multitextLine.ToArray());
                    }
                }
            }
            string error = "Multitext Definition doesn't ended!";
            Exception e = new Exception(error);
            
            string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString() + error;
            string controllerName = this.GetType().Name.ToString();
            Error.toFile(k, controllerName);
            return 0;
        }

        /// <summary>
        /// Parse NameDef
        /// </summary>
        /// <param name="config"></param>
        /// <param name="separators"></param>
        /// <param name="lines"></param>
        /// <param name="startLineIdx"></param>
        /// <returns>Position in the lines array</returns>
        public void ParseNames(CIniFile config, string[] separators)
        {
            string[] lines = null;
            try
            {
                lines = System.IO.File.ReadAllLines(NamePath, System.Text.Encoding.Default);
            }
            catch (Exception e) {
                string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString();
                string name = this.GetType().Name.ToString();
                Error.toFile(k, name);
            }
            string tableName = null;
            string TextListName = null;
            string column = null;
            List<string> linesList = lines.Select(p => p.Trim()).ToList();
            for (int i = 0; i<linesList.Count;i++) {
                if (!(linesList[i].StartsWith("#") && linesList[i].Length != 0)) {

                    //string[] nameLine = lines[i].Split(Const.separators, StringSplitOptions.RemoveEmptyEntries);
                    List<string> nameLine = linesList[i].Split(Const.separ_allNames, StringSplitOptions.RemoveEmptyEntries).ToList();
                    nameLine = nameLine.Select(p => p.Trim()).ToList();
                    string line = linesList[i];
                    if (nameLine.Count != 0)
                    {
                        column = nameLine[1].Trim();
                    }
                    string[] names = new string[config.LangEnbList.Count];
                    if (nameLine.Count != 0)
                    {
                        //tableName = FindTableName(config, tableName, nameLine);
                        //Resolve structure
                        if ((nameLine.FindIndex(s => s.Contains("DefineMultitext"))) != -1)
                        {
                            int multitextIndex = nameLine.FindIndex(s => s.Contains("DefineMultitext"));
                            string name = nameLine[multitextIndex + 1].Trim();
                            try
                            {
                                i = parseMultitext(config, linesList, i + 1, name);
                            }
                            catch (Exception e)
                            {
                                string nameController = this.GetType().Name.ToString();
                                string error = "Somethings in reading file went wrong";
                                string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString() + error;
                                Error.toFile(k, nameController);
                            }
                        }
                        else if ((nameLine.FindIndex(s => s.Contains("multitext"))) != -1)
                        {
                            try {
                                TextListName = parseMultitextLine(config, tableName, column, line, nameLine, names);
                            }  catch (Exception e) {
                                string nameController = this.GetType().Name.ToString();
                                string error = "Somethings in reading file went wrong";
                                string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString() + error;
                                Error.toFile(k, nameController);
                            }
                        }
                        else
                        {
                            if ((nameLine.FindIndex(s => s.Contains(@"\"))) != -1)
                            {
                                try
                                {
                                    parseMultitextInLine(config, tableName, column, line, names);
                                }
                                catch (Exception e)
                                {
                                    string nameController = this.GetType().Name.ToString();
                                    string error = "Somethings in reading file went wrong";
                                    string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString() + error;
                                    Error.toFile(k, nameController);
                                }
                            }
                            else
                            {
                                try
                                {
                                    parseNameDefLine(config, tableName, column, line, nameLine, names);
                                }
                                catch (Exception e)
                                {
                                    string nameController = this.GetType().Name.ToString();
                                    string error = "Somethings in reading file went wrong";
                                    string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString() + error;
                                    Error.toFile(k, nameController);
                                }
                            }
                        }
                    }
                } else {
                }
            }
        }

        #region new refractored methods(find tablename, names parsers)

        /// <summary>
        /// method to parse multitext defined in only one line
        /// </summary>
        /// <param name="config">config structure</param>
        /// <param name="tableName">name of table</param>
        /// <param name="column">name of column in db</param>
        /// <param name="line">a line in names</param>
        /// <param name="nameLine">a line in names</param>
        /// <param name="names">names which will contains name in all langs</param>
        private static string parseMultitextLine(CIniFile config, string tableName, string column, string line, List<string> nameLine, string[] names)
        {
            string TextListName;
            //Textlist on a tag
            int multitextIndex = nameLine.FindIndex(s => s.Contains("multitext"));
            TextListName = nameLine[multitextIndex + 1].Trim();
            ColumnTextlistDefine.Add(nameLine[1].Trim(), TextListName); //To add textlist into helper class 

            //Only add rest of tag also into NameDef
            string[] langs = line.Split(Const.separ_dollar, StringSplitOptions.RemoveEmptyEntries);
            langs = langs.Where(p => p.Length > 2).ToArray();
            for (int j = 0; j < langs.Length; j++)
            {
                names[j] = nameLine[((multitextIndex - 1) + (j + 2))];
            }
            List<string> units = null; //yes add only null
            NameDefinition.Add(config, column, names, units, tableName); //Adds name to names
            return TextListName;
        }

        /// <summary>
        /// method to parse line with NameDef
        /// </summary>
        /// <param name="config">config structure</param>
        /// <param name="tableName">name of table</param>
        /// <param name="column">name of column in db</param>
        /// <param name="line">a line in names</param>
        /// <param name="nameLine">a line in names</param>
        /// <param name="names">names which will contains name in all langs</param>
        private static void parseNameDefLine(CIniFile config, string tableName, string column, string line, List<string> nameLine, string[] names)
        {
            int iterator = 0; //for string[] names intaration
            List<string> units = new List<string>(); //here add units
            string lineTmp = line.Substring(line.IndexOf("=") + 1);
            List<string> nameDefLine = lineTmp.Split(Const.separ_names, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
            if (nameDefLine.Count <= config.LangEnbList.Count) //magic constant (dbidx + column + space)
            {
                for (int j = 0; j < nameDefLine.Count;j++)
                {
                    names[iterator] = nameDefLine[j];
                    iterator++;
                }
            }
            else
            {
                for (int j = 0; j < nameDefLine.Count;j += 2)
                {
                    names[iterator] = nameDefLine[j];
                    units.Add(nameDefLine[j+1]);
                    iterator++;
                }
            }
            NameDefinition.Add(config, column, names, units, tableName); //Adds name to names
        }

        /// <summary>
        /// method to parse multitext defined in only one line
        /// </summary>
        /// <param name="config">config structure</param>
        /// <param name="tableName">name of table</param>
        /// <param name="column">name of column in db</param>
        /// <param name="line">a line in names</param>
        /// <param name="names">names which will contains name in all langs</param>
        private static void parseMultitextInLine(CIniFile config, string tableName, string column, string line, string[] names)
        {
            string[] Tmp = line.Split(Const.separ_equate, StringSplitOptions.RemoveEmptyEntries);
            string Variable = Tmp[0];   // not used
            string DefBody = Tmp[1];

            string[] langs = DefBody.Split(Const.separ_dollar, StringSplitOptions.RemoveEmptyEntries);
            langs = langs.Where(p => p.Length > 2).ToArray();
            int LangCnt = langs.Length;
            string[][] items = new string[LangCnt][];

            for (int j = 0; j < LangCnt; j++) {
                string[] Tmp2 = langs[j].Split(Const.separ_names, StringSplitOptions.RemoveEmptyEntries);
                names[j] = Tmp2[0];
                string itemList = Tmp2[1];

                items[j] = itemList.Split(Const.separ_backslashNew, StringSplitOptions.RemoveEmptyEntries);
            }

            // pricny pruchod
            List<string[]> values = new List<string[]>();//For the ending state of values
            List<int> Idxs = new List<int>();//Initial defintion

            int itemCnt = items[0].Length;  // bacha, spoleha se, ze jsou vsechna pole stejne dlouha - patri se monzna Minimum delek
            for (int x = 0; x < itemCnt; x++)
            {
                string[] tmpPole = new string[LangCnt];
                for (int L = 0; L < LangCnt; L++)
                    tmpPole[L] = items[L][x];   // transpozice matice

                Idxs.Add(x);
                values.Add(tmpPole);
            }
            for(int i = 0;i < names.Length; i++) {
                if (names[i].Contains(@"\")) {
                    names[i] = names[i].Replace(@"\", "-");
                }
            }
            TextlistDefinition.Add(config, column, values, Idxs); //name is not present so add name as tablename
            List<string> units = null; //yes add only null
            NameDefinition.Add(config, column, names, units, tableName); //Adds name to names
        }

        /// <summary>
        /// Method to find tableName in config
        /// </summary>
        /// <param name="config">ini config</param>
        /// <param name="tableName">name of the table which we are finding</param>
        /// <param name="nameLine">name line to check that contains tablename</param>
        public static void FindTableName(CIniFile config)
        {
            foreach (NameDef namedef in config.NameDefList) //Find table foreach namedef in namdefList
            {
                try
                {
                    foreach (CView view in config.ViewList) //find tableName in all signals
                    {
                        if (namedef.table == null) //if has been fill break it
                        {
                            foreach (CField field in view.FieldList)
                            {
                                if (namedef.table == null)
                                {
                                    foreach (CSignal signal in field.SigList)
                                    {
                                        if (signal.column.Contains(namedef.column))
                                        {
                                            namedef.table = signal.table;
                                            break;
                                        }
                                    }
                                    foreach (CSigMultitext sigMultitext in field.SigMultiList)
                                    {
                                        if (sigMultitext.Column.Contains(namedef.column))
                                        {
                                            namedef.table = sigMultitext.Table;
                                            break;
                                        }
                                    }
                                }
                                else { break; }
                            }
                        } else { break; }
                    }
                }
                catch (Exception e)
                {
                    string error = "Probably some mess in this line";
                    string k = e.Message.ToString() + e.Source.ToString() + e.StackTrace.ToString() + error;
                    string name = "Iniparser";
                    Error.toFile(k, name);
                }
            }
        }

        #endregion

        #endregion
    }
}
