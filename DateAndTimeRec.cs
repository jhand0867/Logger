using System.Collections.Generic;
using System.Data;


namespace Logger
{
    // todo:  revise write and parse need to include response flag, Luno, message sequence

    struct dateAndTimeRec
    {
        private string rectype;
        private string messageClass;
        private string responseFlag;
        private string luno;
        private string messageSeqNum;
        private string messageSubClass;
        private string messageIdentifier;
        private string messageDate;
        private string messageTime;

        public string Rectype { get => rectype; set => rectype = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MessageSeqNum { get => messageSeqNum; set => messageSeqNum = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageDate { get => messageDate; set => messageDate = value; }
        public string MessageTime { get => messageTime; set => messageTime = value; }
    }
    class DateAndTimeRec : App, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();

            string sql = @"SELECT * FROM dateTime WHERE prjkey = '" +
            projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" + logKey + "%' LIMIT 1";


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

                if (r.typeContent.Length < 10)
                {
                    continue;
                }

                dateAndTimeRec dtr = parseData(r.typeContent);

                loadNum++;

                string sql = @"INSERT INTO dateTime([logkey],[rectype],[messageClass],[responseFlag],
	                           [luno],[messageSeqNum],[messageSubClass],[messageIdentifier],[date],[time],[load],[prjkey],[logID])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   dtr.Rectype + "','" +
                                   dtr.MessageClass + "','" +
                                   dtr.ResponseFlag + "','" +
                                   dtr.Luno + "','" +
                                   dtr.MessageSeqNum + "','" +
                                   dtr.MessageSubClass + "','" +
                                   dtr.MessageIdentifier + "','" +
                                   dtr.MessageDate + "','" + // date
                                   dtr.MessageTime + "','" + // time // configID
                                   loadNum.ToString() + "','" + key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;

        }

        public dateAndTimeRec parseData(string r)
        {
            dateAndTimeRec dtr = new dateAndTimeRec();

            string[] tmpTypes = r.Split((char)0x1c);

            dtr.Rectype = "D";
            dtr.MessageClass = tmpTypes[0].Substring(10, 1);
            if (tmpTypes[0].Length > 11)
            {
                dtr.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
            }
            dtr.Luno = tmpTypes[1];
            dtr.MessageSeqNum = tmpTypes[2];
            dtr.MessageSubClass = tmpTypes[3].Substring(0, 1);
            if (tmpTypes[3].Length == 2)
                dtr.MessageIdentifier = tmpTypes[3].Substring(1, 1);
            dtr.MessageDate = tmpTypes[4].Substring(0, 6);
            dtr.MessageTime = tmpTypes[4].Substring(6, 4);

            return dtr;
        }

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'D' ";

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

            DataTable dateAndTime = getDescription();



            if (dts[0].Rows.Count > 0)

                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)

                    txtField += App.Prj.getOptionDescription(dateAndTime, colNum.ToString("00"), dts[0].Rows[0][colNum].ToString());

            return txtField;
        }
    }
}
