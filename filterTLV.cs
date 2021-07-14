using System.Data;

namespace Logger
{
    class filterTLV : Digester
    {
        public new string executeScript(string fieldType, string fieldValue)
        {
            string fieldDesc = "";

            //if (fieldValue.Trim() != "")
            //    fieldDesc = System.Environment.NewLine;

            string[] tmpfieldValue = fieldValue.Split(';');
            DataTable dataTable = getDescriptionX(fieldType);

            // Use fieldType = 1 when fieldvalue from message is a "Tag Length Value (TLV)" format

            foreach (string field in tmpfieldValue)
            {
                string[] tlv = field.Split(' ');

                if ((tlv.Length > 1) && (fieldDesc == ""))
                    fieldDesc = System.Environment.NewLine;

                    foreach (DataRow item in dataTable.Rows)
                {
                    if (item[2].ToString().Trim() == tlv[0])
                    {
                        if (tlv.Length > 1)
                        {
                            fieldDesc = fieldDesc + "   " + item[3].ToString().Trim() + " = " + tlv[0] +
                                        " Length = " + tlv[1] + " Value = " + tlv[2];
                        }
                        else
                        {
                            fieldDesc = fieldDesc + "   " + item[3].ToString().Trim();
                        }

                        fieldDesc = fieldDesc + System.Environment.NewLine;
                        break;
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
