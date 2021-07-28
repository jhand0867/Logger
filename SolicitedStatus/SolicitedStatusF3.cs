using System.Collections.Generic;

namespace Logger
{

    struct solicitedStaF3
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentificer;
        private string groupNumber;
        private string dateTimeLastCleared;
        private string tallyData;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentificer { get => messageIdentificer; set => messageIdentificer = value; }
        public string GroupNumber { get => groupNumber; set => groupNumber = value; }
        public string DateTimeLastCleared { get => dateTimeLastCleared; set => dateTimeLastCleared = value; }
        public string TallyData { get => tallyData; set => tallyData = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class SolicitedStatusF3 : SolicitedStatus

    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedStaF3 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusF3([logkey],[rectype],[messageClass],[messageSubClass],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentificer],[groupNumber],
	                        [dateTimeLastCleared],[tallyData],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.MessageClass + "','" + ss.MessageSubClass + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                               ss.MessageIdentificer + "','" + ss.GroupNumber + "','" + ss.DateTimeLastCleared + "','" +
                               ss.TallyData + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaF3 parseData(string r)
        {
            solicitedStaF3 ss = new solicitedStaF3();

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

            ss.StatusDescriptor = tmpTypes[i].Substring(0, 1);
            i++;
            ss.MessageIdentificer = tmpTypes[i].Substring(0, 1);
            ss.GroupNumber = tmpTypes[i].Substring(1, 1);
            ss.DateTimeLastCleared = tmpTypes[i].Substring(2, 12);
            ss.TallyData = tmpTypes[i].Substring(14, 6);

            if (tmpTypes.Length > i + 1)
                ss.Mac = tmpTypes[i + 1];

            return ss;
        }
    }
}

