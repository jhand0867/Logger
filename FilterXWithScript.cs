using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logger
{
    class FilterXWithScript : Digester
    {
        public new string executeScript(string fieldType, string fieldValue)
        {
            string fieldDesc = "";

            string[] tmpfieldValue = fieldValue.Split(';');
            //string[] tmpfieldValue = fieldValue.Split(',');

            if (tmpfieldValue.Length > 1)
                fieldDesc = System.Environment.NewLine;

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
                        string field4 = item[4].ToString().Trim();
                        string fieldResult = "";
                        string outputField = "";
                        int lengthOfField4 = field4.Length;

                        // /(\{.*\})/gUgs
                        Regex handleBars = new Regex(@"(\{.*?\})", RegexOptions.Singleline);
                        int field4Offset = 0;

                        while (field4Offset < lengthOfField4 - 1)
                        {
                            MatchCollection scriptsToApply = handleBars.Matches(field4);

                            if (scriptsToApply.Count == 0)
                            {
                                outputField += " = " + field4;
                                break;
                            }

                            foreach (Match hit in scriptsToApply)
                            {
                                field4Offset += hit.Length;
                                int indexOfScriptStart = hit.Value.IndexOf("{", 0);
                                int indexOfSctiptEnd = hit.Value.IndexOf("}", indexOfScriptStart);
                                if (indexOfScriptStart != -1)
                                {
                                    // what scripts do I have

                                    fieldResult = hit.Value.Substring(indexOfScriptStart + 1, indexOfSctiptEnd - 1);
                                    string[] scriptOptions = fieldResult.Split(',');

                                    string workField = field.Substring(offset, reminder);

                                    if (workField.Length >= Convert.ToInt32(scriptOptions[1]))
                                    {
                                        if (scriptOptions[2].IndexOf('%') == 0)
                                        {
                                            outputField += System.Environment.NewLine + "\t";
                                        }
                                        outputField += scriptOptions[2].Substring(1, scriptOptions[2].Length - 1) + " =" +
                                                     workField.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                                    }
                                }
                            }
                        }
                        if (tmpfieldValue.Length > 1)
                            fieldDesc += "   " + item[2].ToString().Trim() + "   " + item[3].ToString().Trim() + "  " + outputField + " " + System.Environment.NewLine;
                        else
                            fieldDesc += "   " + item[3].ToString().Trim() + "  " + outputField + " " + System.Environment.NewLine;

                        break;
                    }
                }
            }

            return fieldDesc;
        }
    }
}
