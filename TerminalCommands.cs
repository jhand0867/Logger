using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct terminalCommands
    {
        private string rectype;
        private string messageClass;
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
        public string MessageClass { get => messageClass; set => messageClass = value; }
    }
    class TerminalCommands : App, IMessage
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

            string sql = @"SELECT * FROM terminalCommands WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
                                               "' AND logkey LIKE '" + logKey + "%' LIMIT 1";
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
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                    txtField += App.Prj.getOptionDescription(ss, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());

            return txtField;
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                terminalCommands tc = parseData(r.typeContent);

                string sql = @"INSERT INTO terminalCommands([logkey],[rectype],[messageClass],[responseFlag],[luno],[messageSeqNumber],
	                                                     [commandCode],[modifier],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + tc.Rectype + "','" + tc.MessageClass + "','" + tc.ResponseFlag + "','" + tc.Luno + "','" +
                             tc.MessageSeqNumber + "','" + tc.CommandCode + "','" + tc.Modifier + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public terminalCommands parseData(string r)
        {
            terminalCommands tc = new terminalCommands();

            string[] tmpTypes = r.Split((char)0x1c);

            tc.Rectype = "A";

            tc.MessageClass = tmpTypes[0].Substring(10, 1);

            if (tmpTypes[0].Length > 11)
                tc.ResponseFlag = tmpTypes[0].Substring(11, 1);

            tc.Luno = tmpTypes[1];
            tc.MessageSeqNumber = tmpTypes[2];

            tc.CommandCode = tmpTypes[3].Substring(0, 1);

            if (tmpTypes[3].Length > 1)
            {
                tc.Modifier = tmpTypes[3].Substring(1, 1);
            }

            return tc;
        }
    }
}

