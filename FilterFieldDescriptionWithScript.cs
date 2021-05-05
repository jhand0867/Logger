using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logger
{
    class FilterFieldDescriptionWithScript : IFilter
    {
        public string executeScript(string fieldType, string fieldValue)
        {
            string fieldDesc = "";

            // Use fieldType = 2 when fieldType from message is equal to subRecType in the Data Description

            int offset = 0;
            int reminder = fieldType.Length - offset;
            string field4 = fieldValue.Trim();
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

                        string workField = fieldType.Substring(offset, reminder);

                        if (scriptOptions[2].IndexOf('%') == 0)
                        {
                            outputField += System.Environment.NewLine + "\t";
                        }
                        outputField += workField.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1])) +
                                       scriptOptions[2].Substring(0, scriptOptions[2].Length);
                    }
                }
            }
            fieldDesc = fieldDesc + "   " + "  " + outputField + " " + System.Environment.NewLine;

            return fieldDesc;
        }
    }
}
