using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct extEncryption
    {
        private string rectype;
        private string messageClass;
        private string responseFlag;
        private string luno;
        private string messageSeqNumber;
        private string messageSubclass;
        private string modifier;
        private string keySize;
        private string keyData;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Modifier { get => modifier; set => modifier = value; }
        public string KeySize { get => keySize; set => keySize = value; }
        public string KeyData { get => keyData; set => keyData = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MessageSeqNumber { get => messageSeqNumber; set => messageSeqNumber = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
    }
    class ExtEncryption : App, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'O' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT TOP 1 * FROM extEncryption WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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

                extEncryption eek = parseData(r.typeContent);

                string sql = @"INSERT INTO extEncryption([logkey],[rectype],[messageClass],
	                         [responseFlag],[luno],[messageSeqNumber],[messageSubclass],[modifier],
                             [keySize],[keyData],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + eek.Rectype + "','" + eek.MessageClass + "','" +
                            eek.ResponseFlag + "','" + eek.Luno + "','" + eek.MessageSeqNumber + "','" +
                            eek.MessageSubclass + "','" + eek.Modifier + "','" + eek.KeySize + "" +
                            "','" + eek.KeyData + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public extEncryption parseData(string r)
        {
            extEncryption eek = new extEncryption();

            string[] tmpTypes = r.Split((char)0x1c);

            eek.Rectype = "O";
            eek.MessageClass = tmpTypes[0].Substring(10, 1);
            if (tmpTypes[0].Length > 11)
            {
                eek.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
            }

            eek.Luno = tmpTypes[1];
            eek.MessageSeqNumber = tmpTypes[2];
            eek.MessageSubclass = tmpTypes[3].Substring(0, 1);
            eek.Modifier = tmpTypes[3].Substring(1, 1);

            if (tmpTypes.Length > 4 && tmpTypes[4].Length > 0)
            {
                eek.KeySize = tmpTypes[4].Substring(0, 3);
                eek.KeyData = tmpTypes[4].Substring(3, tmpTypes[4].Length - 3);
            }

            return eek;
        }
    }
}

