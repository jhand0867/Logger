using System.Collections.Generic;

namespace Logger
{

    struct solicitedStaFK
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string sensorStatusId;
        private string sensorStatusData;
        private string tamperIndicatorId;
        private string tamperStatusData;
        private string extendedTamperIndicatorId;
        private string extendedTamperStatusData;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string SensorStatusId { get => sensorStatusId; set => sensorStatusId = value; }
        public string SensorStatusData { get => sensorStatusData; set => sensorStatusData = value; }
        public string TamperIndicatorId { get => tamperIndicatorId; set => tamperIndicatorId = value; }
        public string TamperStatusData { get => tamperStatusData; set => tamperStatusData = value; }
        public string ExtendedTamperIndicatorId { get => extendedTamperIndicatorId; set => extendedTamperIndicatorId = value; }
        public string ExtendedTamperStatusData { get => extendedTamperStatusData; set => extendedTamperStatusData = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class SolicitedStatusFK : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            // Digester digester = new Digester();


            log.Info($"Adding {this.GetType().Name}");

            foreach (typeRec r in typeRecs)
            {
                solicitedStaFK ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusFK([logkey],[rectype],[messageClass],[messageSubClass],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[sensorStatusId],
                        	[sensorStatusData],[tamperIndicatorId],[tamperStatusData],[extendedTamperIndicatorId],
                           	[extendedTamperStatusData],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.MessageClass + "','" + ss.MessageSubClass + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.SensorStatusId + "','" + ss.SensorStatusData + "','" +
                            ss.TamperIndicatorId + "','" + ss.TamperStatusData + "','" + ss.ExtendedTamperIndicatorId + "','" +
                            ss.ExtendedTamperStatusData + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaFK parseData(string r)
        {
            log.Info($"Parsing {this.GetType().Name}");

            solicitedStaFK ss = new solicitedStaFK();

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
            ss.MessageIdentifier = tmpTypes[i + 1].Substring(0, 1); ;

            // this is the initial element id A
            ss.SensorStatusId = tmpTypes[i + 1].Substring(1, 1);

            ss.SensorStatusData = tmpTypes[i + 1].Substring(2, tmpTypes[i + 1].Length - 2);

            // separating Group Data
            string[] statusInfo = tmpTypes[i + 2].Split((char)0x1d);

            ss.TamperIndicatorId = statusInfo[0].Substring(0, 1);
            ss.TamperStatusData = statusInfo[0].Substring(1, statusInfo[0].Length - 1);

            if (statusInfo.Length > 1)
            {
                ss.ExtendedTamperIndicatorId = statusInfo[1].Substring(0, 1);
                ss.ExtendedTamperStatusData = statusInfo[1].Substring(1, statusInfo[1].Length - 1);

                for (int x = 2; x < statusInfo.Length; x++)
                {
                    ss.ExtendedTamperStatusData += ";" + statusInfo[x];
                }
            }

            if (tmpTypes.Length > i + 3)
                ss.Mac = tmpTypes[i + 3];

            return ss;
        }
    }
}
