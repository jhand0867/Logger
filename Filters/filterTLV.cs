﻿using System.Data;

namespace Logger
{
    class filterTLV : Digester
    {
        public new string executeScript(string fieldValue, string fieldType)
        {
            string fieldDesc = "";

            string[] tmpfieldValue = fieldValue.Split(';');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 1 when fieldvalue from message is a "Tag Length Value (TLV)" format

            int i = 1;
            string[] descriptionFields = new string[] { "", "", "" };

            foreach (string field in tmpfieldValue)
            {
                string[] tlv = field.Split(' ');

                foreach (DataRow item in dataTable.Rows)
                {
                    if (item[2].ToString().Trim() == tlv[0])
                    {
                        if (tlv.Length > 1)
                        {
                            descriptionFields[0] = "";
                            descriptionFields[1] = "Tag = " + tlv[0] + @" \par " + item[3].ToString().Trim();
                            descriptionFields[2] = " Length = " + tlv[1] + @" \par " + " Value = " + tlv[2];
                            fieldDesc = fieldDesc + App.Prj.insertRowRtf(descriptionFields);
                        }
                        else
                        {
                            descriptionFields[0] = "";
                            descriptionFields[1] = item[3].ToString().Trim();
                            descriptionFields[2] = "";
                            fieldDesc = fieldDesc + App.Prj.insertRowRtf(descriptionFields);
                        }
                        break;
                    }
                }
                i++;
            }
            return fieldDesc;
        }
        public string formattedOutput(string value, string formatter)
        {
            return "";
        }

    }
}
