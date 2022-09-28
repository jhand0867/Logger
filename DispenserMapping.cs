using System.Collections.Generic;
using System.Data;

namespace Logger
{
    struct dispenserMapping
    {
        private string rectype;
        private string messageClass;
        private string responseFlag;
        private string luno;
        private string messageSeqNumber;
        private string messageSubclass;
        private string messageIdentifier;
        private string numberMappingEntries;
        private string mappingTable;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string NumberMappingEntries { get => numberMappingEntries; set => numberMappingEntries = value; }
        public string MappingTable { get => mappingTable; set => mappingTable = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MessageSeqNumber { get => messageSeqNumber; set => messageSeqNumber = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
    }
    class DispenserMapping : App, IMessage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'G' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM dispenserMapping WHERE prjkey = '" + projectKey + "' AND logID = ' LIMIT 1" + logID +
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
            lpb.LblTitle = "Dispenser Mapping";
            lpb.Maximum = typeRecs.Count + 1;

            foreach (typeRec r in typeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                dispenserMapping dm = parseData(r.typeContent);

                string sql = @"INSERT INTO dispenserMapping([logkey],[rectype],[messageClass],
	                         [responseFlag],[luno],[messageSeqNumber],[messageSubclass],[messageIdentifier],[numberMappingEntries],
                                        	[mappingTable],[mac],[prjkey],[logID])" +
                            " VALUES('" + r.typeIndex + "','" + dm.Rectype + "','" + dm.MessageClass + "','" +
                            dm.ResponseFlag + "','" + dm.Luno + "','" + dm.MessageSeqNumber + "','" +
                            dm.MessageSubclass + "','" + dm.MessageIdentifier + "','" + dm.NumberMappingEntries + "','" +
                              dm.MappingTable + "','" + dm.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public dispenserMapping parseData(string r)
        {
            dispenserMapping dm = new dispenserMapping();

            string[] tmpTypes = r.Split((char)0x1c);

            dm.Rectype = "G";

            dm.MessageClass = tmpTypes[0].Substring(10, 1);
            if (tmpTypes[0].Length > 11)
            {
                dm.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
            }

            dm.Luno = tmpTypes[1];
            dm.MessageSeqNumber = tmpTypes[2];
            dm.MessageSubclass = tmpTypes[3].Substring(0, 1);
            dm.MessageIdentifier = tmpTypes[3].Substring(1, 1);

            dm.NumberMappingEntries = tmpTypes[4].Substring(0, 2);
            dm.MappingTable = tmpTypes[4].Substring(2, tmpTypes[4].Length - 2);

            if (tmpTypes.Length > 5)
                dm.Mac = tmpTypes[5];

            return dm;
        }
    }
}

