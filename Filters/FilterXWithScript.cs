﻿using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Logger
{
    class FilterXWithScript : Digester
    {
        public new string executeScript(string fieldValue, string fieldType)
        {
            string fieldDesc = "";

            string[] tmpfieldValue = fieldValue.Split(';');
            string[] descriptionFields = new string[] { "", "", "" };

            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 2 when fieldvalue from message is equal to subRecType in the Data Description

            foreach (string field in tmpfieldValue)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    if ((item[2].ToString().Trim().Substring(0, 1) == field.Substring(0, 1)) &&
                        (item[2].ToString().Trim().Length <= field.Length) &&
                       (item[2].ToString().Trim() == field.Substring(0, item[2].ToString().Trim().Length)))
                    {
                        int offset = item[2].ToString().Trim().Length;
                        int reminder = field.Length - offset;
                        string field7 = item[7].ToString().Trim();
                        string fieldResult = "";
                        string outputField = "";
                        int lengthOfField7 = field7.Length;

                        // /(\{.*\})/gUgs
                        Regex handleBars = new Regex(@"(\{.*?\})", RegexOptions.Singleline);
                        int field7Offset = 0;

                        while (field7Offset < lengthOfField7 - 1)
                        {
                            MatchCollection scriptsToApply = handleBars.Matches(field7);

                            if (scriptsToApply.Count == 0)
                            {
                                outputField = field7;
                                break;
                            }
                            if (reminder <= 0)
                            {
                                break;
                            }

                            string workField = field.Substring(offset, reminder);

                            foreach (Match hit in scriptsToApply)
                            {
                                field7Offset += hit.Length;
                                int indexOfScriptStart = hit.Value.IndexOf("{", 0);
                                int indexOfSctiptEnd = hit.Value.IndexOf("}", indexOfScriptStart);
                                if (indexOfScriptStart != -1)
                                {
                                    // what scripts do I have

                                    fieldResult = hit.Value.Substring(indexOfScriptStart + 1, indexOfSctiptEnd - 1);
                                    string[] scriptOptions = fieldResult.Split(',');


                                    if (workField.Length >= Convert.ToInt32(scriptOptions[0]) + Convert.ToInt32(scriptOptions[1]))
                                    {
                                        // % = new line
                                        // ^ = at the start
                                        // $ = at the end

                                        if (scriptOptions[3].Contains("%"))
                                        {
                                            outputField += @" \par ";
                                        }

                                        if (scriptOptions[3].Contains("$"))
                                        {
                                            outputField += workField.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1])) +
                                                           scriptOptions[2].Substring(0, scriptOptions[2].Length);
                                        }
                                        else if (scriptOptions[3].Contains("^"))
                                        {
                                            outputField += scriptOptions[2].Substring(0, scriptOptions[2].Length) + " =" +
                                            workField.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                                        }
                                        else
                                        {
                                            outputField += workField.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                                        }

                                    }
                                }
                            }
                        }
                        if (tmpfieldValue.Length > 1)
                        {
                            descriptionFields[0] = "";
                            descriptionFields[1] = item[3].ToString().Trim();
                            descriptionFields[2] = item[4].ToString().Trim() + outputField;
                            fieldDesc = fieldDesc + App.Prj.insertRowRtf(descriptionFields);
                        }
                        else
                            fieldDesc += item[3].ToString().Trim() + "  " + item[4].ToString().Trim() + " " + outputField;

                        break;
                    }
                }
            }

            return fieldDesc;
        }
    }
}
