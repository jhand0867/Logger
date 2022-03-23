using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct ejOptionsTimers
    {
        private string rectype;
        private string messageClass;
        private string commandType;
        private string optionNumber;
        private string optionValue;
        private string optionNumber2;
        private string optionValue2;
        private string timerNumber;
        private string timerValue;

        public string Rectype { get => rectype; set => rectype = value; }
        public string OptionNumber { get => optionNumber; set => optionNumber = value; }
        public string OptionValue { get => optionValue; set => optionValue = value; }
        public string OptionNumber2 { get => optionNumber2; set => optionNumber2 = value; }
        public string OptionValue2 { get => optionValue2; set => optionValue2 = value; }
        public string TimerNumber { get => timerNumber; set => timerNumber = value; }
        public string TimerValue { get => timerValue; set => timerValue = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string CommandType { get => commandType; set => commandType = value; }
    }
    class EjOptionsTimers : App, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'M' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM ejOptionsTimers WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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

                ejOptionsTimers eot = parseData(r.typeContent);

                string sql = @"INSERT INTO ejOptionsTimers([logkey],[rectype],[messageClass],[commandType],[optionNumber],[optionValue],
                              [optionNumber2],[optionValue2],[timerNumber],[timerValue],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + eot.Rectype + "','" + eot.MessageClass + "','" + eot.CommandType + "','" +
                              eot.OptionNumber + "','" + eot.OptionValue + "','" + eot.OptionNumber2 + "','" + eot.OptionValue2 + "','" +
                              eot.TimerNumber + "','" + eot.TimerValue + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public ejOptionsTimers parseData(string r)
        {
            ejOptionsTimers eot = new ejOptionsTimers();

            string[] tmpTypes = r.Split((char)0x1c);

            eot.Rectype = "M";

            eot.MessageClass = tmpTypes[0].Substring(10, 1);
            eot.CommandType = tmpTypes[3].Substring(0, 1);

            if (tmpTypes[3].Length > 1)
            {
                eot.OptionNumber = tmpTypes[3].Substring(1, 2);
                eot.OptionValue = tmpTypes[3].Substring(3, 3);
            }

            if (tmpTypes[3].Length > 6)
            {
                eot.OptionNumber2 = tmpTypes[3].Substring(6, 2);
                eot.OptionValue2 = tmpTypes[3].Substring(8, 3);
            }

            if (tmpTypes[4].Length > 0)
            {
                eot.TimerNumber = tmpTypes[4].Substring(0, 2);
                eot.TimerValue = tmpTypes[4].Substring(2, 3);
            }
            return eot;
        }
    }
}
