using System;
using System.Text.RegularExpressions;

namespace Logger
{
    class FilterDescriptionWithScript : Digester
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
                outputField += " = " + @"\cell " + fieldValue;

            int offset = 0;
            while (offset < fieldValue.Length)
            {

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

                        // % = new line
                        // ^ = at the start
                        // $ = at the end

                        if (scriptOptions[3].Contains("%"))
                        {
                            //outputField += System.Environment.NewLine + "\t";
                            outputField += @"\par ";
                        }

                        if (scriptOptions[3].Contains("$"))
                        {
                            outputField += fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1])) +
                                           scriptOptions[2].Substring(0, scriptOptions[2].Length);
                        }
                        else if (scriptOptions[3].Contains("^"))
                        {
                            outputField += scriptOptions[2].Substring(0, scriptOptions[2].Length) + " =" +
                            fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                        }
                        else
                        {
                            outputField += fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                        }

                        offset += Convert.ToInt32(scriptOptions[1]);
                    }
                }
            }
            //fieldDesc = fieldDesc + "   " + "  " + outputField + " " + @"\par ";
            fieldDesc = @"\cell " + outputField + " " + @"\par ";

            return fieldDesc;
        }
        public string formattedOutput(string value, string formatter)
        {
            return "";
        }

    }
}
