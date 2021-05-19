using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Logger
{
    public class Digester : IFilter
    {

        // call this class to handle Data Descriptions with a value in fieldType  (field type not equal to Null)

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public readonly string emvTags = @",42,50,57,61,70,71,72,73,77,80,81,82,83,84,86,87,88,89,90,91,92,93,94," +
                                "95,97,98,99,4F,5A,5F20,5F24,5F25,5F28,5F2A,5F2D,5F30,5F34,5F36,5F50,5F53,5F54,5F55," +
                                "5F56,6F,8A,8C,8D,8E,8F,9A,9B,9C,9D,9F01,9F02,9F03,9F04,9F05,9F06,9F07,9F08,9F09,9F0B,9F0D," +
                                "9F0E,9F0F,9F10,9F11,9F12,9F13,9F14,9F15,9F16,9F17,9F18,9F1A,9F1B,9F1C,9F1D,9F1E,9F1F," +
                                "9F20,9F21,9F22,9F23,9F26,9F27,9F2D,9F2E,9F2F,9F32,9F33,9F34,9F35,9F36,9F37,9F38,9F39," +
                                "9F3A,9F3B,9F3C,9F3D,9F40,9F41,9F42,9F43,9F44,9F45,9F46,9F47,9F48,9F49,9F4A,9F4B,9F4C,9F4D," +
                                "9F4E,9F4F,A5,BF0C,";


        public string fieldDigester(string fieldType, string fieldValue, string scriptValue)
        {
            var recTypeDic = new Dictionary<string, Func<string>>();
            recTypeDic.Add("0", () => new FilterFieldDescriptionWithScript().executeScript(fieldValue, scriptValue));
            //recTypeDic.Add("1", () => new FilterHWConfigWithScript().executeScript(fieldType, fieldValue));
            recTypeDic.Add("1", () => new Digester().filterTLV(fieldType, fieldValue));
            //recTypeDic.Add("2", () => new filterSupplies().executeScript(fieldType, fieldValue));
            recTypeDic.Add("2", () => new Digester().filterHWConfigWithScript(fieldType, fieldValue));
            recTypeDic.Add("3", () => new Digester().filterSupplies(fieldType, fieldValue));
            recTypeDic.Add("4", () => new filterMappingTable().executeScript(fieldType, fieldValue));
            //recTypeDic.Add("4", () => new Digester().filterMappingTable(fieldType, fieldValue));
            recTypeDic.Add("5", () => new Digester().filterFieldNoDescription(fieldType, fieldValue));
            recTypeDic.Add("6", () => new Digester().filterConfiguration(fieldType, fieldValue));
            recTypeDic.Add("7", () => new Digester().filterHWConfigWithScript(fieldType, fieldValue));

            try
            {
                //todo: fix it gracefully
                //
                return recTypeDic[fieldType]();
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }

        }

        private string filterMappingTable(string fieldType, string fieldValue)
        {
            string fieldDesc = ""; // System.Environment.NewLine;
            DataTable dataTable = getDescriptionX(fieldType);
            int offset = 0;

            // this method expect to have the dataTable (dataDescription) ordered by subRecType
            // currently used by DispenserMapping class

            while (offset < fieldValue.Length)
            {
                fieldDesc = fieldDesc + "   " + dataTable.Rows[0]["fieldname"].ToString().Trim() + " = " + fieldValue.Substring(offset, 2) + System.Environment.NewLine;
                offset += 2;
                fieldDesc = fieldDesc + "   " + dataTable.Rows[1]["fieldname"].ToString().Trim() + " = " + fieldValue.Substring(offset, 1) + System.Environment.NewLine;
                offset += 1;
                fieldDesc = fieldDesc + "   " + dataTable.Rows[2]["fieldname"].ToString().Trim() + " = " + fieldValue.Substring(offset, 5) + System.Environment.NewLine;
                offset += 5;
            }

            return fieldDesc;
        }

        public string filterTLV(string fieldType, string fieldValue)
        {

            string fieldDesc = ""; // System.Environment.NewLine;
            string[] tmpfieldValue = fieldValue.Split(';');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 1 when fieldvalue from message is a "Tag Length Value (TLV)" format
            //  is it really TLV or just EMV Tag?

            foreach (string field in tmpfieldValue)
            {
                string[] tlv = field.Split(' ');
                foreach (DataRow item in dataTable.Rows)
                {
                    if (item[2].ToString().Trim() == tlv[0])
                    {
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + " = " + tlv[0];
                        if (tlv.Length > 1)
                        {
                            fieldDesc = fieldDesc + " Length = " + tlv[1] + " Value = " + tlv[2];
                        }
                        fieldDesc = fieldDesc + System.Environment.NewLine;
                        break;
                    }
                }
            }
            return fieldDesc;
        }

        public string filterConfiguration(string fieldType, string fieldValue)
        {

            string fieldDesc = ""; // System.Environment.NewLine;
            string[] tmpfieldValue = fieldValue.Split(';');
            //string[] tmpfieldValue = fieldValue.Split(',');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 2 when fieldvalue from message is equal to subRecType in the Data Description

            foreach (string field in tmpfieldValue)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    if (item[2].ToString().Trim() == field)
                    {
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + " = " + item[4].ToString().Trim() + System.Environment.NewLine;
                        break;
                    }
                }
            }

            return fieldDesc;
        }

        public string filterSupplies(string fieldType, string fieldValue)
        {

            string fieldDesc = ""; //System.Environment.NewLine;
            string[] tmpfieldValue = fieldValue.Split(';');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 3 when
            // first char of fieldvalue from message is equal to first char of subRecType in the Data Description
            // and the position of next char(s) of fieldvalue from message is equal to subRecType 
            // i.e. field value = E11121
            //      will search for E0 in data description table to show the description for E
            //      and then will search for each of the positions (E1,E2,E3,E4, etc) in data description table

            bool headerFlg = false;

            foreach (string field in tmpfieldValue)
            {
                int pos = 0;
                foreach (DataRow item in dataTable.Rows)
                {
                    if (!headerFlg && item[2].ToString().Trim().Length == 0)
                    {
                        fieldDesc = fieldDesc + item[3].ToString().Trim() + System.Environment.NewLine;
                        headerFlg = true;
                        continue;
                    }
                    if (item[2].ToString().Trim().Length > 0 &&
                        item[2].ToString().Trim().Substring(0, 1) == field.Substring(0, 1) &&
                        field.Length > pos)
                    {
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + " = " + field.Substring(pos, 1) + System.Environment.NewLine;
                        pos++;
                    }
                }
            }
            return fieldDesc;
        }
        /// <summary>
        /// Use fieldType = 5 when fieldvalue from message is equal to subRecType in the Data Description
        /// the parse will use the fieldName(item[3]) from Data Description and ignore the fieldDescription((item[4])
        /// </summary>
        /// <param name="fieldType">Data Description table FieldType</param>
        /// <param name="fieldValue">Data Description table subRecType</param>
        /// <returns></returns>
        private string filterFieldNoDescription(string fieldType, string fieldValue)
        {
            string fieldDesc = "";
            string[] tmpfieldValue = fieldValue.Split(';');
            DataTable dataTable = getDescriptionX(fieldType);


            foreach (string field in tmpfieldValue)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    if (item[2].ToString().Trim() == field)
                    {
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + System.Environment.NewLine;
                        break;
                    }
                }
            }

            return fieldDesc;
        }

        public DataTable getDescriptionX(string fieldType)
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'X'  AND fieldType = '" + fieldType + "' order by subRecType asc";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }


        /// <summary>
        ///  iccTLVTags method will split EMV tags from string received in strTag parameter
        ///  for the number of tags specified in tagsNumber
        ///  will return string with tags separated by space
        /// </summary>
        /// <param name="strTags"></param>
        /// <param name="tagsNumber"></param>
        /// <returns>tags</returns>

        public string iccTLVTags(string strTags, int tagsNumber)
        {
            //todo: should check for number of valif offset
            //todo: - F1 = 31 seem to show less tags than required
            //todo: - is it that the number of tags is a max?

            // what tags?
            string tags = "";
            int offset = 0;
            for (int x = 0; x < tagsNumber; x++)
            {
                if (strTags.Length <= offset + 1)
                {
                    continue;
                }
                if (emvTags.Contains("," + strTags.Substring(offset, 2) + ","))
                {
                    tags += strTags.Substring(offset, 2) + " ";
                    offset += 2;
                }
                else
                {
                    tags += strTags.Substring(offset, 4) + " ";
                    offset += 4;
                }
            }
            return tags;
        }

        /// <summary>
        /// iccTLVTags method will split EMV tags length and values from string received in strTag parameter
        /// it will return TLV separated by semicolon
        /// </summary>
        /// <param name="strTag"></param>
        /// <returns>tags</returns>

        public string iccTLVTags(string strTag)
        {
            string tags = "";
            int offset = 0;
            while (offset < strTag.Length)
            {
                if (emvTags.Contains("," + strTag.Substring(offset, 2) + ","))
                {
                    tags += strTag.Substring(offset, 2) + " ";
                    offset += 2;
                }
                else
                {
                    tags += strTag.Substring(offset, 4) + " ";
                    offset += 4;
                }
                tags += strTag.Substring(offset, 2) + " ";
                int hexLength = Convert.ToInt32(strTag.Substring(offset, 2), 16);
                offset += 2;
                tags += strTag.Substring(offset, hexLength * 2) + ";";
                offset += hexLength * 2;
            }
            return tags;
        }
        public string filterHardwareConfiguration(string fieldType, string fieldValue)
        {

            string fieldDesc = ""; // System.Environment.NewLine;
            string[] tmpfieldValue = fieldValue.Split(';');
            //string[] tmpfieldValue = fieldValue.Split(',');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 2 when fieldvalue from message is equal to subRecType in the Data Description

            foreach (string field in tmpfieldValue)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    if (item[2].ToString().Trim() == field)
                    {
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + System.Environment.NewLine;
                        break;
                    }
                    else if ((item[2].ToString().Trim().Substring(0, 1) == field.Substring(0, 1)) &&
                             (item[2].ToString().Trim() == field.Substring(0, item[2].ToString().Trim().Length)))
                    {
                        int offset = item[2].ToString().Trim().Length;
                        int reminder = field.Length - offset;
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + "  " + item[4].ToString().Trim() + "  (" + field.Substring(offset, reminder) + ")" + System.Environment.NewLine;

                    }
                }
            }

            return fieldDesc;
        }
        public string filterHWConfigWithScript(string fieldType, string fieldValue)
        {

            string fieldDesc = ""; // System.Environment.NewLine;
            string[] tmpfieldValue = fieldValue.Split(';');
            //string[] tmpfieldValue = fieldValue.Split(',');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 2 when fieldvalue from message is equal to subRecType in the Data Description

            foreach (string field in tmpfieldValue)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    if ((item[2].ToString().Trim().Substring(0, 1) == field.Substring(0, 1)) &&
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

                                    if (scriptOptions[2].IndexOf('%') == 0)
                                    {
                                        outputField += System.Environment.NewLine + "\t";
                                    }
                                    outputField += scriptOptions[2].Substring(1, scriptOptions[2].Length - 1) + " =" +
                                        workField.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                                }
                            }
                        }
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + "  " + outputField + " " + System.Environment.NewLine;

                    }
                }
            }

            return fieldDesc;
        }
        public string filterDeviceStatus(string fieldType, string fieldValue)
        {

            string fieldDesc = ""; // System.Environment.NewLine;
            string[] tmpfieldValue = fieldValue.Split(';');
            //string[] tmpfieldValue = fieldValue.Split(',');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 2 when fieldvalue from message is equal to subRecType in the Data Description

            foreach (string field in tmpfieldValue)
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    if ((item[2].ToString().Trim().Substring(0, 1) == field.Substring(0, 1)) &&
                             (item[2].ToString().Trim() == field.Substring(0, item[2].ToString().Trim().Length)))
                    {
                        int offset = item[2].ToString().Trim().Length;
                        int reminder = field.Length - offset;
                        string field4 = item[4].ToString().Trim();
                        string fieldResult = "";
                        string outputField = "";
                        int lengthOfField4 = field4.Length;

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

                                    if (scriptOptions[2].IndexOf('%') == 0)
                                    {
                                        outputField += System.Environment.NewLine + "\t";
                                    }
                                    outputField += scriptOptions[2].Substring(1, scriptOptions[2].Length - 1) + " =" +
                                        workField.Substring(Convert.ToInt32(scriptOptions[0]), Convert.ToInt32(scriptOptions[1]));
                                }
                            }
                        }
                        fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + "  " + outputField + " " + System.Environment.NewLine;

                    }
                }
            }

            return fieldDesc;
        }
        public string filterFieldDescriptionWithScript(string fieldValue, string fieldDescription)
        {

            string fieldDesc = "";

            // Use fieldType = 2 when fieldvalue from message is equal to subRecType in the Data Description

            int offset = 0;
            int reminder = fieldValue.Length - offset;
            string field4 = fieldDescription.Trim();
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

                        string workField = fieldValue.Substring(offset, reminder);

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

        public string executeScript(string fieldType, string fieldValue)
        {
            return "";
        }
    }
}
