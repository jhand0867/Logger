using System;
using System.Text.RegularExpressions;

namespace Logger
{
    class FilterFieldDescriptionWithScript : Digester
    {
        public new string executeScript(string fieldValue, string scriptValue)
        {
            string fieldDesc = "";

            string fieldResult;
            string outputField = "";


            // /(\{.*\})/gUgs
            Regex handleBars = new Regex(@"(\{.*?\})", RegexOptions.Singleline);
            int field4Offset = 0;


            MatchCollection scriptsToApply = handleBars.Matches(scriptValue.Trim());

            if (scriptsToApply.Count == 0)
                outputField += " = " + fieldValue;

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

                    if (scriptOptions[2].IndexOf('%') == 0)
                    {
                        outputField += System.Environment.NewLine + "\t";
                    }
                    outputField += fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1])) +
                                   scriptOptions[2].Substring(0, scriptOptions[2].Length);
                }
            }
            fieldDesc = fieldDesc + "   " + "  " + outputField + " " + System.Environment.NewLine;
            return fieldDesc;
        }
        public string formattedOutput(string value, string formatter)
        {
            return "";
        }

    }
}
