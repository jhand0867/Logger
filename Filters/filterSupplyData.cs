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

        public new string executeScript(string fieldValue, string fieldType)
        {

            string fieldDesc = "";
            if (fieldValue.Trim() != "")
                fieldDesc = @"\par ";

            string[] tmpfieldValue = fieldValue.Split(';');
            DataTable deviceNames = getDescriptionX(fieldType);
            DataTable supplyStatus = getDescriptionX("S", "SD");

            foreach (string field in tmpfieldValue)
            {
                int pos = 0;
                foreach (DataRow item in deviceNames.Rows)
                {
                    if (item[2].ToString().Trim().Length > 0 &&
                        item[2].ToString().Trim().Substring(0, 1) == field.Substring(0, 1) &&
                        field.Length > pos)
                    {
                        /*
                         *  Scan values matching on supplyStatus by record number 
                         *  if pos > 0 plug in the fieldName from the record with the field.Substring(pos, 1)
                         */
                        string ssDescr = "";
                        if (pos > 0)
                            foreach (DataRow ss in supplyStatus.Rows)
                            {
                                if (ss[2].ToString().Substring(2, 1) == field.Substring(pos, 1))
                                    ssDescr = ss[3].ToString().Trim();
                            }

                        // MLH Here!! does it need to finish with \row ? 

                        fieldDesc = fieldDesc + "\\cell " + item[3].ToString().Trim() + " = \\cell " + field.Substring(pos, 1) +
                                    "\\cell " + ssDescr + @"\row ";
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
