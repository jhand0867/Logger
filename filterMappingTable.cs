using System.Data;

namespace Logger
{
    class filterMappingTable : Digester
    {
        public new string executeScript(string fieldType, string fieldValue)
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
        public string formattedOutput(string value, string formatter)
        {
            return "";
        }

    }
}
