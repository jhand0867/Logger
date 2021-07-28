using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Logger
{
    class FilterDescriptionWithScriptAndTable : Digester
    {
        public new string executeScript(string fieldValue, string scriptValue)
        {
            string fieldDesc = "";
            string fieldResult;

            if (fieldValue.Trim() != "")
                fieldDesc = " = " + fieldValue + System.Environment.NewLine;

            // /(\{.*\})/gUgs
            Regex handleBars = new Regex(@"(\{.*?\})", RegexOptions.Singleline);
            int field4Offset = 0;


            MatchCollection scriptsToApply = handleBars.Matches(scriptValue.Trim());

            DataTable descriptionTable = null;

            foreach (Match hit in scriptsToApply)
            {
                if (hit.Index == 0)
                    continue;
                field4Offset += hit.Length;
                int indexOfScriptStart = hit.Value.IndexOf("{", 0);
                int indexOfSctiptEnd = hit.Value.IndexOf("}", indexOfScriptStart);
                if (indexOfScriptStart != -1)
                {
                    // what scripts do I have

                    fieldResult = hit.Value.Substring(indexOfScriptStart + 1, indexOfSctiptEnd - 1);
                    string[] scriptOptions = fieldResult.Split(',');

                    // {0,2,!2B}{4,2,!2C}{6,2,!2DD}
                    // i.e. parsing of first script:
                    // Take field value from position 0 two characters and use table 2 and search for value 'B' +
                    // the two characters from the field value

                    if (descriptionTable == null)
                        descriptionTable = getDescriptionX(scriptOptions[2].Substring(1, 1));

                    string field = scriptOptions[2].Substring(2, scriptOptions[2].Length - 2 ) +
                                   fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));

                    foreach (DataRow item in descriptionTable.Rows)
                    {
                        if (item[2].ToString().Trim() == field)
                        {
                            fieldDesc += fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1])) +
                                           " = " + item[3].ToString().Trim() + " " + item[4].ToString().Trim() + " " + System.Environment.NewLine;
                            break;
                        }
                    }
                }
            }
            return fieldDesc;
        }
        public string formattedOutput(string value, string formatter)
        {
            return "";
        }

    }
}
