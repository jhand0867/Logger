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

            string[] descriptionFields = new string[] { "", "", "" };


            //if (fieldValue.Trim() != "")
            //{
            //    descriptionFields[0] = fieldValue;
            //    descriptionFields[1] = "";
            //    descriptionFields[2] = "";

            //    fieldDesc = fieldDesc + App.Prj.insertRowRtf(descriptionFields);
            //}

            // /(\{.*\})/gUgs
            Regex handleBars = new Regex(@"(\{.*?\})", RegexOptions.Singleline);
            int field4Offset = 0;


            MatchCollection scriptsToApply = handleBars.Matches(scriptValue.Trim());

            DataTable descriptionTable = null;

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

                    // {0,2,!2B}{4,2,!2C}{6,2,!2D}
                    // i.e. parsing of first script:
                    // Parse fieldValue take from position 0 two characters.
                    // (! = Look for Table) Description labels are on DataDescription table fieldType=2
                    // Search in the list for subRecType = B + the two characters from the fieldValue


                    if (descriptionTable == null)
                        descriptionTable = getDescriptionX(scriptOptions[2].Substring(1, 1));

                    string field = scriptOptions[2].Substring(2, scriptOptions[2].Length - 2) +
                                   fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));

                    foreach (DataRow item in descriptionTable.Rows)
                    {
                        if (item[2].ToString().Trim() == field)
                        {

                            descriptionFields[0] = fieldValue.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                            descriptionFields[1] = item[3].ToString().Trim();
                            descriptionFields[2] = item[4].ToString().Trim();

                            fieldDesc = fieldDesc + App.Prj.insertRowRtf(descriptionFields);

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
