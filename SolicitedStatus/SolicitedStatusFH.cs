using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaFH
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string configgurationID;
        private string productClass;
        private string hwConfigurationID;
        private string hwConfigurationData;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string ConfiggurationID { get => configgurationID; set => configgurationID = value; }
        public string ProductClass { get => productClass; set => productClass = value; }
        public string HwConfigurationID { get => hwConfigurationID; set => hwConfigurationID = value; }
        public string HwConfigurationData { get => hwConfigurationData; set => hwConfigurationData = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class SolicitedStatusFH : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            log.Info($"Adding {this.GetType().Name}");

            foreach (typeRec r in typeRecs)
            {
                solicitedStaFH ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusFH([logkey],[rectype],[messageClass],[messageSubClass],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[configgurationID],
	                        [productClass],[hwConfigurationID],[hwConfigurationData],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.MessageClass + "','" + ss.MessageSubClass + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.ConfiggurationID + "','" + ss.ProductClass + "','" +
                            ss.HwConfigurationID + "','" + ss.HwConfigurationData + "','" + ss.Mac + "','" +
                            Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaFH parseData(string r)
        {
            log.Info($"Parsing {this.GetType().Name}");

            solicitedStaFH ss = new solicitedStaFH();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.MessageClass = tmpTypes[0].Substring(10, 1);
            ss.MessageSubClass = tmpTypes[0].Substring(11, 1);
            ss.Luno = tmpTypes[1];

            int i = 3;
            if (tmpTypes[3].Length != 1)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            ss.StatusDescriptor = tmpTypes[i];
            ss.MessageIdentifier = tmpTypes[i + 1].Substring(0, 1);
            ss.ConfiggurationID = tmpTypes[i + 1].Substring(1, 5);
            ss.ProductClass = tmpTypes[i + 2];

            string[] statusInfo = tmpTypes[i + 3].Split((char)0x1d);


            ss.HwConfigurationID = statusInfo[0].Substring(0, 1);
            ss.HwConfigurationData = statusInfo[0].Substring(1, 3);
            for (int x = 1; x < statusInfo.Length; x++)
            {

                ss.HwConfigurationData += ";" + statusInfo[x];
            }

            if (tmpTypes.Length > i + 4)
                ss.Mac = tmpTypes[i + 4];
            return ss;
        }

        new public string parseToView(string logKey, string logID, string projectKey, string recValue)
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
                    string dataElement = dts[0].Rows[0][colNum].ToString();
                    //if (dataElement.Length == 5)



                    txtField += App.Prj.getOptionDescription(ss, recordType.Substring(2, recordType.Length - 2) + colNum.ToString("00"),
                                                         dts[0].Rows[0][colNum].ToString());

                }
            }
            return txtField;

        }
    }
}
