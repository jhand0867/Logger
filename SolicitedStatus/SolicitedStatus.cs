using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    class SolicitedStatus : App, IMessage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public Dictionary<string, string> ssTypes = new Dictionary<string, string>();


        public SolicitedStatus()
        {
            ssTypes.Add("A", "229");
            ssTypes.Add("B", "22B");
            ssTypes.Add("8", "228");
            ssTypes.Add("9", "229");
            ssTypes.Add("C", "22C");
            ssTypes.Add("F1", "22F1");
            ssTypes.Add("F2", "22F2");
            ssTypes.Add("F3", "22F3");
            ssTypes.Add("F4", "22F4");
            ssTypes.Add("F5", "22F5");
            ssTypes.Add("F6", "22F6");
            ssTypes.Add("F7", "22F7");
            ssTypes.Add("FF", "22FF");
            ssTypes.Add("FH", "22FH");
            ssTypes.Add("FI", "22FI");
            ssTypes.Add("FJ", "22FJ");
            ssTypes.Add("FK", "22FK");
            ssTypes.Add("FL", "22FL");
            ssTypes.Add("FM", "22FM");
            ssTypes.Add("FN", "22FN");
        }

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
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'N'  AND subRecType like '" + recType + "%'";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public new List<DataTable> getRecord(string logKey, string logID, string projectKey, string recType)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM solicitedStatus" + recType + " WHERE prjkey = '" + projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%' LIMIT 1";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            string recordType = getRecordType(recValue);
            List<DataTable> dts = getRecord(logKey, logID, projectKey, recordType.Substring(2, recordType.Length - 2));
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable ss = getDescription(recordType.Substring(2, recordType.Length - 2));

            if (dts[0].Rows.Count > 0)
            {
                for (int colNum = 3; colNum < dts[0].Columns.Count - 2; colNum++)
                {
                    string[] colData = dts[0].Rows[0][colNum].ToString().Split(';');



                    txtField += App.Prj.getOptionDescription(ss, recordType.Substring(2, recordType.Length - 2) + colNum.ToString("00"),
                                                         dts[0].Rows[0][colNum].ToString());

                }
            }
            return txtField;

        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = "Solicited Status";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                List<typeRec> OneTypeRec = new List<typeRec>();
                OneTypeRec.Add(r);

                string recordType = getRecordType(r.typeContent);

                IMessage theRecord = LoggerFactory.Create_Record(recordType);

                if (theRecord.writeData(OneTypeRec, Key, logID) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        internal string getRecordType(string recValue)
        {
            string[] tmpTypes = recValue.Split((char)0x1c);

            string recordType = "";
            int i = 3;

            string[] tmp = tmpTypes[3].Split((char)0x1d);

            if (tmp[0].Length != 1) i = 4;

            if (tmpTypes[i] == "F")
            {
                recordType = ssTypes[tmpTypes[i] + tmpTypes[i + 1].Substring(0, 1)];
            }
            else
            {
                recordType = ssTypes[tmpTypes[i].Substring(0, 1)];
            }

            return recordType;
        }

    }
}
