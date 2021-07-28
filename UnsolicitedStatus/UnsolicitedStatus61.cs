using System.Collections.Generic;

namespace Logger
{

    struct unsolicitedSta61
    {
        private string rectype;
        private string messageClass;
        private string messageSubclass;
        private string luno;
        private string dig;
        private string deviceStatus;
        private string errorSeverity;
        private string diagnosticStatus;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string Dig { get => dig; set => dig = value; }
        public string DeviceStatus { get => deviceStatus; set => deviceStatus = value; }
        public string ErrorSeverity { get => errorSeverity; set => errorSeverity = value; }
        public string DiagnosticStatus { get => diagnosticStatus; set => diagnosticStatus = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
    };

    class UnsolicitedStatus61 : UnsolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                unsolicitedSta61 us = parseData(r.typeContent);

                string sql = @"INSERT INTO unsolicitedStatus61([logkey],[rectype],[messageClass],[messageSubclass],[luno],
	                        [dig],[deviceStatus],[errorSeverity],[diagnosticStatus],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + us.Rectype + "','" + us.MessageClass + "','" +
                               us.MessageSubclass + "','" + us.Luno + "','" +
                               us.Dig + "','" + us.DeviceStatus + "','" + us.ErrorSeverity + "','" +
                               us.DiagnosticStatus + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public unsolicitedSta61 parseData(string r)
        {
            unsolicitedSta61 us = new unsolicitedSta61();

            string[] tmpTypes = r.Split((char)0x1c);

            us.Rectype = "U";
            us.MessageClass = tmpTypes[0].Substring(10, 1);
            us.MessageSubclass = tmpTypes[0].Substring(11, 1);

            us.Luno = tmpTypes[1];
            int i = 3;

            us.Dig = tmpTypes[i].Substring(0, 1);
            us.DeviceStatus = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);

            if (tmpTypes.Length > i + 1)
            {
                us.ErrorSeverity = tmpTypes[i + 1];
            }

            if (tmpTypes.Length > i + 2)
            {
                us.DiagnosticStatus = tmpTypes[i + 2];
            }

            return us;
        }
    }
}

