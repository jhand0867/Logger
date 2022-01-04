using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct uploadEjData
    {
        private string rectype;
        private string messageClass;
        private string messageSubclass;
        private string machineNumRange;
        private string dateRange;
        private string timeRange;
        private string lastCharPrevBlock;
        private string lastCharThisBlock;
        private string blockLength;
        private string responseData;

        public string Rectype { get => rectype; set => rectype = value; }
        public string MachineNumRange { get => machineNumRange; set => machineNumRange = value; }
        public string DateRange { get => dateRange; set => dateRange = value; }
        public string TimeRange { get => timeRange; set => timeRange = value; }
        public string LastCharPrevBlock { get => lastCharPrevBlock; set => lastCharPrevBlock = value; }
        public string LastCharThisBlock { get => lastCharThisBlock; set => lastCharThisBlock = value; }
        public string BlockLength { get => blockLength; set => blockLength = value; }
        public string ResponseData { get => responseData; set => responseData = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
    }
    class UploadEjData : App, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'H' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM uploadEjData WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
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

                uploadEjData ud = parseData(r.typeContent);

                string sql = @"INSERT INTO uploadEjData([logkey],[rectype],[messageClass],[messageSubclass],[machineNumRange],
	                            [dateRange],[timeRange],[lastCharPrevBlock],[lastCharThisBlock],[blockLength],
	                            [responseData],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ud.Rectype + "','" + ud.MessageClass + "','" + ud.MessageSubclass + "','" +
                               ud.MachineNumRange + "','" + ud.DateRange + "','" + ud.TimeRange + "','" + ud.LastCharPrevBlock + "','" +
                               ud.LastCharThisBlock + "','" + ud.BlockLength + "','" + ud.ResponseData + "','" +
                               Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }

            lpb.Visible = false;
            return true;
        }

        public uploadEjData parseData(string r)
        {
            uploadEjData ud = new uploadEjData();

            string[] tmpTypes = r.Split((char)0x1c);

            ud.Rectype = "H";

            ud.MessageClass = tmpTypes[0].Substring(10, 1);

            if (tmpTypes[0].Length > 11)
                ud.MessageSubclass = tmpTypes[0].Substring(11, 1);

            ud.MachineNumRange = tmpTypes[4].Substring(0, 6);
            ud.DateRange = tmpTypes[4].Substring(6, 6);
            ud.TimeRange = tmpTypes[4].Substring(12, 6);
            ud.LastCharPrevBlock = tmpTypes[4].Substring(18, 6);
            ud.LastCharThisBlock = tmpTypes[4].Substring(24, 6);
            ud.BlockLength = tmpTypes[4].Substring(30, 3);
            ud.ResponseData = tmpTypes[4].Substring(33, tmpTypes[4].Length - 33);

            return ud;
        }
    }
}
