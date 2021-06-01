using System.Data;

namespace Logger
{
    class filterSupplyData : Digester
    {
        // Use fieldType = 3 when
        // first char of fieldvalue from message is equal to first char of subRecType in the Data Description
        // and the position of next char(s) of fieldvalue from message is equal to subRecType 
        // i.e. field value = E11121
        //      will search for E0 in data description table to show the description for E
        //      and then will search for each of the positions (E1,E2,E3,E4, etc) in data description table

        public new string executeScript(string fieldType, string fieldValue)
        {

                string fieldDesc = "";
                if (fieldValue.Trim() != "")
                    fieldDesc = System.Environment.NewLine;

                string[] tmpfieldValue = fieldValue.Split(';');
                DataTable dataTable = getDescriptionX(fieldType);

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
        public string formattedOutput(string value, string formatter)
        {
            return "";
        }

    }
}
