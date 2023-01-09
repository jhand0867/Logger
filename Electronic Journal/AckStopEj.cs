using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct ackStopEj
    {
        private string rectype;
        private string messageClass;
        private string commandType;
        private string lastCharReceived;

        public string Rectype { get => rectype; set => rectype = value; }
        public string LastCharReceived { get => lastCharReceived; set => lastCharReceived = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string CommandType { get => commandType; set => commandType = value; }
    };

    class AckStopEj : App, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'L' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * ackStopEj WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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
            lpb.LblTitle = "EJ Acknowldege Stop";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                ackStopEj ase = parseData(r.typeContent);

                string sql = @"INSERT INTO ackStopEj([logkey],[rectype],[messageClass],[commandType],[lastCharReceived],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ase.Rectype + "','" + ase.MessageClass + "','" + ase.CommandType + "','" +
                               ase.LastCharReceived + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public ackStopEj parseData(string r)
        {
            ackStopEj ase = new ackStopEj();

            string[] tmpTypes = r.Split((char)0x1c);

            ase.Rectype = "L";
            ase.MessageClass = tmpTypes[0].Substring(10, 1);
            ase.CommandType = tmpTypes[3].Substring(0, 1);
            ase.LastCharReceived = tmpTypes[3].Substring(1, 1);

            return ase;
        }


    }
}
