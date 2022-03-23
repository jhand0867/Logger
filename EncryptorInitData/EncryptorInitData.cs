using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class EncryptorInitData : App, IMessage

    {

        public Dictionary<string, string> kTypes = new Dictionary<string, string>();


        public EncryptorInitData()
        {
            kTypes.Add("1", "231");
            kTypes.Add("2", "232");
            kTypes.Add("3", "233");
            kTypes.Add("4", "234");
            kTypes.Add("5", "235");
            kTypes.Add("6", "236");
            kTypes.Add("7", "237");
            kTypes.Add("8", "238");
            kTypes.Add("9", "239");
            kTypes.Add("A", "23A");
            kTypes.Add("B", "23B");
            kTypes.Add("C", "23C");
            kTypes.Add("D", "23D");
            kTypes.Add("E", "23E");
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            throw new NotImplementedException();
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        public DataTable getDescription(string recType)
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'K'  AND subRecType like '" + recType + "%'";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey, string recType)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM encryptorInitData" + recType + " WHERE prjkey = '" + projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%' LIMIT 1 ";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            string recordType = getRecordType(recValue);
            List<DataTable> dts = getRecord(logKey, logID, projectKey, recordType.Substring(2, recordType.Length - 2));
            string txtField = "";

            if (dts == null || dts[0] == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable ss = getDescription(recordType.Substring(2, recordType.Length - 2));

            if (dts[0].Rows.Count > 0)
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                    txtField += App.Prj.getOptionDescription(ss, recordType.Substring(2, recordType.Length - 2) + colNum.ToString("00"),
                                                         dts[0].Rows[0][colNum].ToString());

            return txtField;

        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                List<typeRec> OneTypeRec = new List<typeRec>();
                OneTypeRec.Add(r);

                string recordType = getRecordType(r.typeContent);

                IMessage theRecord = LoggerFactory.Create_Record(recordType);

                if (theRecord == null) { continue; }

                if (theRecord.writeData(OneTypeRec, Key, logID) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        internal string getRecordType(string recValue)
        {
            string[] tmpTypes = recValue.Split((char)0x1c);
            return kTypes[tmpTypes[3]];
        }
    }
}
