using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct terminalCommands
    {
        private string rectype;
        private string responseFlag;
        private string luno;
        private string messageSeqNumber;
        private string commandCode;
        private string modifier;

        public string Rectype { get => rectype; set => rectype = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MessageSeqNumber { get => messageSeqNumber; set => messageSeqNumber = value; }
        public string CommandCode { get => commandCode; set => commandCode = value; }
        public string Modifier { get => modifier; set => modifier = value; }
    };

    class TerminalCommands : IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'A' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM terminalCommands WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
                                               "' AND logkey LIKE '" + logKey + "%'";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable ss = getDescription();

            if (dts[0].Rows.Count > 0)
            {
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                {
                    txtField += App.Prj.getOptionDescription(ss, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());
                    txtField += "\t" + System.Environment.NewLine;

                }
            }
            return txtField;
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                terminalCommands tc = parseData(r.typeContent);

                string sql = @"INSERT INTO terminalCommands([logkey],[rectype],[responseFlag],[luno],[messageSeqNumber],
	                                                     [commandCode],[modifier],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + tc.Rectype + "','" + tc.ResponseFlag + "','" + tc.Luno + "','" +
                             tc.MessageSeqNumber + "','" + tc.CommandCode + "','" + tc.Modifier + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public terminalCommands parseData(string r)
        {
            terminalCommands tc = new terminalCommands();

            string[] tmpTypes = r.Split((char)0x1c);

            tc.Rectype = "A";
            if (tmpTypes[0].Length > 11)
            { tc.ResponseFlag = tmpTypes[0].Substring(11, 1); }
            tc.Luno = tmpTypes[1];
            tc.MessageSeqNumber = tmpTypes[2];

            tc.CommandCode = tmpTypes[3].Substring(0, 1);

            if (tmpTypes[3].Length > 1)
            {
                tc.Modifier = tmpTypes[3].Substring(1, 1);
            }

            return tc;
        }

        //internal string getOptionDescription(DataTable dataTable, string field, string fieldValue)
        //{

        //    // todo: enter data descriptions for all records
        //    // todo: put together the digesting routines for all record types

        //    string optionDesc = "";
        //    string fieldDesc = "";

        //    // what's the description of the field
        //    foreach (DataRow item in dataTable.Rows)
        //    {
        //        if (item[2].ToString().Trim() == field)
        //        {
        //            optionDesc = item[3].ToString().Trim();

        //            if (item[5].ToString() != null && item[5].ToString() != "")
        //            {
        //                Digester myDigester = LoggerFactory.Create_Digester();
        //                fieldDesc = myDigester.fieldDigester(item[5].ToString(), fieldValue);
        //                fieldValue = fieldValue.Replace(";", " ");
        //            }
        //            optionDesc += " = " + fieldValue + insertDescription(item[4].ToString()) + fieldDesc;

        //            break;
        //        }
        //    }
        //    return optionDesc;
        //}

        //private string insertDescription(string fieldDescription)
        //{
        //    string description = "";

        //    if (fieldDescription != "")
        //    {
        //        if (fieldDescription.Contains("\r\n"))
        //        {
        //            description += System.Environment.NewLine + fieldDescription.Trim() + System.Environment.NewLine;
        //        }
        //        else
        //        {
        //            description += "\t" + fieldDescription.Trim() + System.Environment.NewLine;
        //        }
        //    }
        //    else
        //    {
        //        description += fieldDescription.Trim();
        //    }
        //    return description;
        //}
    }
}

