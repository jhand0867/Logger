using System.Collections.Generic;
using System.Data;

namespace Logger
{
    // todo:  revise write and parse need to include response flag, Luno, message sequence

    struct configIdRec
    {
        private string rectype;
        private string messageClass;
        private string responseFlag;
        private string luno;
        private string messageSeqNum;
        private string messageSubClass;
        private string messageIdentifier;
        private string configId;

        public string Rectype { get => rectype; set => rectype = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MessageSeqNum { get => messageSeqNum; set => messageSeqNum = value; }
        public string ConfigId { get => configId; set => configId = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
    }
    class ConfigIdRec : App, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();

            string sql = @"SELECT * FROM configId WHERE prjkey = '" +
                           projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                logKey + "%' LIMIT 1";
            log.Debug("Database query:" + sql);

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = typeRecs.Count + 1;

            int loadNum = 0;
            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                if (r.typeContent.Length < 4)
                {
                    continue;
                }

                configIdRec cir = parseData(r.typeContent);

                loadNum++;

                string sql = @"INSERT INTO configId([logkey],[rectype],[messageClass],[responseFlag],
	                           [luno],[messageSeqNum],[messageSubClass],[messageIdentifier],[configID],[load],[prjkey],[logID])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   cir.Rectype + "','" +
                                   cir.MessageClass + "','" +
                                   cir.ResponseFlag + "','" +
                                   cir.Luno + "','" +
                                   cir.MessageSeqNum + "','" +
                                   cir.MessageSubClass + "','" +
                                   cir.MessageIdentifier + "','" +
                                   cir.ConfigId + "','" +   // configID
                                   loadNum.ToString() + "','" + key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;

        }

        public configIdRec parseData(string r)
        {
            configIdRec cir = new configIdRec();

            string[] tmpTypes = r.Split((char)0x1c);

            cir.Rectype = "I";
            cir.MessageClass = tmpTypes[0].Substring(10, 1);
            if (tmpTypes[0].Length > 11)
            {
                cir.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
            }
            cir.Luno = tmpTypes[1];
            cir.MessageSeqNum = tmpTypes[2];
            cir.MessageSubClass = tmpTypes[3].Substring(0, 1);
            if (tmpTypes[3].Length == 2)
                cir.MessageIdentifier = tmpTypes[3].Substring(1, 1);
            cir.ConfigId = tmpTypes[4];

            return cir;
        }

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'I' ";

            log.Debug("Database query: " + sql);

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable configId = getDescription();



            if (dts[0].Rows.Count > 0)

                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)

                    txtField += App.Prj.getOptionDescription(configId, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());

            return txtField;
        }
    }
}
