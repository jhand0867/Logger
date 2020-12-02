using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    public class Digester
    {
        public string fieldDigester(string fieldType, string fieldValue)
        {
            var recTypeDic = new Dictionary<string, Func<string>>();
            recTypeDic.Add("1", () => new Digester().filterTLV(fieldType, fieldValue));
            recTypeDic.Add("2", () => new Digester().filterConfiguration(fieldType, fieldValue));
            recTypeDic.Add("3", () => new Digester().filterSupplies(fieldType, fieldValue));

            try
            {
                return recTypeDic[fieldType]();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public string filterTLV(string fieldType, string fieldValue)
        {

            string fieldDesc = System.Environment.NewLine;
            string[] tmpfieldValue = fieldValue.Split(';');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 1 when fieldvalue from message is a "Tag Length Value (TLV)" format

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

            string fieldDesc = System.Environment.NewLine;
            //string[] tmpfieldValue = fieldValue.Split(';');
            string[] tmpfieldValue = fieldValue.Split(',');
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

            string fieldDesc = System.Environment.NewLine;
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


        public DataTable getDescriptionX(string fieldType)
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'X'  AND fieldType = '" + fieldType + "' order by subRecType asc";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

    }
}
