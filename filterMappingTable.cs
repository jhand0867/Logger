using System.Data;

namespace Logger
{
    class filterMappingTable : Digester
    {
        //todo: method obsolete

        // .... 04 01 1 00010 01 2 00020 02 3 00100 02 4 00200 ....
        /* 
         cassette type 1 = 10 dollars
●        cassette type 2 = 20 dollars
●        cassette type 3 = 100 euros
●        cassette type 4 = 200 euros
         */
        //{0,2,%Currency Type}{2,1,%Currency Code}{3,5,%Denomination}

        public new string executeScript(string fieldType, string fieldValue)
        {
            string fieldDesc = "";
            if (fieldValue.Trim() != "")
                fieldDesc = System.Environment.NewLine;

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
