using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDiosna.Graph.Models;

namespace UsersDiosna.OldCode
{
    public class Iniparser_old
    {
        private static string NamePath = "";

        #region oldsectionNames
        public void ParseNames(CIniFile config, string[] separators)
        {

            //string[] separeted_string = null;
            List<string> separated_string = new List<string>();
            int result, rowNumber = 0;

            string[] lines = System.IO.File.ReadAllLines(NamePath, System.Text.Encoding.Default);

            for (int i = 0; i < lines.Length; i++)
            {
                //separeted_string = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                separated_string = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
                if (!(separated_string.Count == 0))
                {
                    if (separated_string[0].Contains("DefineMultitext"))
                    {
                        rowNumber = parseTextListDefinition(config, separators, lines, separated_string, i);
                    }

                    if (int.TryParse(separated_string[0], out result) == true)
                    {
                        try
                        {
                            //rowNumber =  parseNameDefinitionNew(config, separators, lines, i);
                        }
                        catch (Exception e)
                        {
                            Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                        }
                    }
                }
                if (rowNumber != 0)
                {
                    i = rowNumber;
                    rowNumber = 0;
                }
            }
        }
        private int parseTextListDefinition(CIniFile config, string[] separators, string[] lines, List<string> separeted_string, int startLineIdx)
        {
            string[] multitext_line = null, line_with_id = null;
            string multitext_name = null, line_without_id = null;
            List<string[]> multitext_lines = new List<string[]>();
            List<int> Idxs = new List<int>();
            int position;
            int id;

            multitext_name = separeted_string[1];

            for (int i = startLineIdx + 1; i < lines.Length; i++)
            {
                if (lines[i].Length == 0)
                {
                    TextlistDefinition.Add(config, multitext_name, multitext_lines, Idxs);
                    return i;
                }
                else
                {
                    line_with_id = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(line_with_id[0], out id) == true)
                    {
                        Idxs.Add(id);
                        position = lines[i].IndexOf(';');
                        line_without_id = lines[i].Substring(position + 1); // Because of spliting this string. Before semicolon is first index
                        multitext_line = line_without_id.Split(Const.separ_semicolon, StringSplitOptions.RemoveEmptyEntries);
                        multitext_lines.Add(multitext_line);
                    }
                }
            }
            return 0;
        }
        private int parseNameDefinition(CIniFile config, string[] separators, string[] lines, int startLineIdx)
        {
            string[] nameLine = null, nameLineFirstPart = null, nameLineLangMutate = null;
            string tableName = null;
            List<string[]> multitext_lines = new List<string[]>();

            for (int i = startLineIdx; i < lines.Length; i++)
            {
                List<string> units = new List<string>();
                string[] langs = new string[config.LangEnbList.Count];
                if (!(lines[i].StartsWith("#")) && (lines[i].Length != 0))
                {
                    nameLine = lines[i].Split(Const.separ_equate, StringSplitOptions.RemoveEmptyEntries);
                    nameLineFirstPart = nameLine[0].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    nameLineLangMutate = nameLine[1].Split(Const.separ_names, StringSplitOptions.RemoveEmptyEntries);
                    foreach (CView view in config.ViewList)
                    {
                        foreach (CField field in view.FieldList)
                        {
                            foreach (CSignal signal in field.SigList)
                            {
                                if (signal.column.Contains(nameLineFirstPart[1]))
                                {
                                    tableName = signal.table;
                                }
                            }
                        }
                    }
                    int j = 0;
                    for (int idx = 0; idx < nameLineLangMutate.Length; idx = idx + 2)
                    {
                        if (!(nameLineLangMutate[idx + 1].Contains(@"\")) && !(nameLineLangMutate[idx + 1].Contains("multitext:")))
                        {
                            if (nameLineLangMutate.Length > config.LangEnbList.Count)
                            {
                                langs[j] = nameLineLangMutate[idx];
                                units.Insert(j, nameLineLangMutate[idx + 1]);
                                j++;
                            }
                            else
                            {
                                langs[j] = nameLineLangMutate[idx];
                                j++;
                            }
                        }
                        else
                        {
                            langs[j] = nameLineLangMutate[idx];
                            if (!(nameLineLangMutate[idx + 1].Contains("multitext:")))
                            {
                                multitextInline(config, tableName, nameLine[1]);
                                idx = nameLineLangMutate.Length;
                            }
                            else
                            {
                                int pos = nameLineLangMutate[idx + 1].LastIndexOf(":");
                                string TextlistName = nameLineLangMutate[idx + 1].Substring(pos + 1);
                                ColumnTextlistDefine.Add(nameLineFirstPart[1], TextlistName);
                            }
                            j++;
                        }
                    }
                    NameDefinition.Add(config, nameLineFirstPart[1], langs, units, tableName);
                }
                else
                {
                    return i;
                }
            }
            return 0;
        }
        private void multitextInline(CIniFile config, string tableName, string nameLineSecondPart)
        {
            List<string[]> textListValues = new List<string[]>();
            List<int> Idxs = new List<int>();
            string[] separatedFromDollars = nameLineSecondPart.Split(Const.separ_dollar, StringSplitOptions.RemoveEmptyEntries);
            string[] langs = new string[separatedFromDollars.Length];
            string[] valsForLength = separatedFromDollars[0].Split(Const.separ_backslash, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < valsForLength.Length; i++)
            {
                for (int j = 0; j < separatedFromDollars.Length; j++)
                {
                    string[] values = separatedFromDollars[j].Split(Const.separ_backslash, StringSplitOptions.RemoveEmptyEntries);
                    langs[j] = values[i];
                }
                textListValues.Add(langs);
                Idxs.Add(i - 1);
                TextlistDefinition.Add(config, tableName, textListValues, Idxs);
            }
        }
        #endregion

        private static void parseMultitextInLine2(CIniFile config, string tableName, string column, string line, string[] names)
        {
            List<string[]> values = new List<string[]>();//For the ending state of values
            List<int> Idxs = new List<int>();//Initial defintion
            string[] langs = line.Split(Const.separ_dollar, StringSplitOptions.RemoveEmptyEntries);
            string[][] vals = vals = new string[langs.Length][];


            for (int j = 0; j < langs.Length; j++)
            {
                int equalPos = langs[j].LastIndexOf(@"=");
                int semicolonPos = langs[j].IndexOf(@";");
                if (equalPos != -1)
                {
                    names[j] = langs[j].Substring(equalPos + 1, (semicolonPos - equalPos));
                    langs[j] = langs[j].Substring(equalPos + 1);
                }
                else
                {
                    names[j] = langs[j].Substring(0, (semicolonPos));
                    langs[j] = langs[j].Substring(semicolonPos + 1);
                }
                string[] prevals = langs[j].Split(Const.separ_backslashNew, StringSplitOptions.RemoveEmptyEntries);
                int index = 0;

                for (int k = 0; k < prevals.Length; k++)
                {
                    //vals[j][index] = new string[];
                    vals[j][index] = prevals[index];
                    index++;
                }
            }
            for (int k = 0; k < vals.Length; k++)
            {
                Idxs.Add(k);
                values.Add(vals[k]);
            }
            TextlistDefinition.Add(config, tableName, values, Idxs); //name is not present so add name as tablename
            List<string> units = null; //yes add only null
            NameDefinition.Add(config, column, names, units, tableName); //Adds name to names
        }
    }
}