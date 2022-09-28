using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct interactiveTranRsp
    {
        private string rectype;
        private string messageClass;
        private string responseFlag;
        private string luno;
        private string messageSeqNumber;
        private string messageSubclass;
        private string displayFlag;
        private string activeKeys;
        private string screenTimerField;
        private string screenDataField;

        public string Rectype { get => rectype; set => rectype = value; }
        public string DisplayFlag { get => displayFlag; set => displayFlag = value; }
        public string ActiveKeys { get => activeKeys; set => activeKeys = value; }
        public string ScreenTimerField { get => screenTimerField; set => screenTimerField = value; }
        public string ScreenDataField { get => screenDataField; set => screenDataField = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MessageSeqNumber { get => messageSeqNumber; set => messageSeqNumber = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
    }
    class InteractiveTranResponse : App, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'Q' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM interactiveTranResponse WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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
            lpb.LblTitle = "Interactive Transaction Response";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                interactiveTranRsp itr = parseData(r.typeContent);

                string sql = @"INSERT INTO interactiveTranResponse([logkey],[rectype],[messageClass],
	                         [responseFlag],[luno],[messageSeqNumber],[messageSubclass],[displayFlag],
	                                    [activeKeys],[screenTimerField],[screenDataField],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + itr.Rectype + "','" + itr.MessageClass + "','" +
                            itr.ResponseFlag + "','" + itr.Luno + "','" + itr.MessageSeqNumber + "','" +
                            itr.MessageSubclass + "','" + itr.DisplayFlag + "','" +
                              itr.ActiveKeys + "','" + itr.ScreenTimerField + "','" + itr.ScreenDataField + "','" +
                              Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public interactiveTranRsp parseData(string r)
        {
            interactiveTranRsp itr = new interactiveTranRsp();

            string[] tmpTypes = r.Split((char)0x1c);

            itr.Rectype = "Q";

            itr.MessageClass = tmpTypes[0].Substring(10, 1);
            if (tmpTypes[0].Length > 11)
            {
                itr.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
            }

            itr.Luno = tmpTypes[1];
            itr.MessageSeqNumber = tmpTypes[2];
            itr.MessageSubclass = tmpTypes[3].Substring(0, 1);
            itr.DisplayFlag = tmpTypes[3].Substring(1, 1);
            itr.ActiveKeys = tmpTypes[3].Substring(2, tmpTypes[3].Length - 2);
            itr.ScreenTimerField = tmpTypes[4];
            itr.ScreenDataField = tmpTypes[5];

            return itr;
        }
    }
}

