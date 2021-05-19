using System.Collections.Generic;

namespace Logger
{

    struct unsolicitedStaE
    {
        private string rectype;
        private string luno;
        private string dig;
        private string deviceStatus;
        private string dispenseCount;
        private string sprayCashDispenser;
        private string errorSeverity;
        private string diagnosticStatus;
        private string suppliesStatus;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string Dig { get => dig; set => dig = value; }
        public string DeviceStatus { get => deviceStatus; set => deviceStatus = value; }
        public string DispenseCount { get => dispenseCount; set => dispenseCount = value; }
        public string SprayCashDispenser { get => sprayCashDispenser; set => sprayCashDispenser = value; }
        public string ErrorSeverity { get => errorSeverity; set => errorSeverity = value; }
        public string DiagnosticStatus { get => diagnosticStatus; set => diagnosticStatus = value; }
        public string SuppliesStatus { get => suppliesStatus; set => suppliesStatus = value; }
    };

    class UnsolicitedStatusE : UnsolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                unsolicitedStaE us = parseData(r.typeContent);

                string sql = @"INSERT INTO unsolicitedStatusE([logkey],[rectype],[luno],
	                        [dig],[deviceStatus],[dispenseCount],[sprayCashDispenser],[errorSeverity],
                            [diagnosticStatus], [suppliesStatus],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + us.Rectype + "','" + us.Luno + "','" +
                               us.Dig + "','" + us.DeviceStatus + "','" + us.DispenseCount + "','" +
                               us.SprayCashDispenser + "','" + us.ErrorSeverity + "','" +
                               us.DiagnosticStatus + "','" + us.SuppliesStatus + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public unsolicitedStaE parseData(string r)
        {
            unsolicitedStaE us = new unsolicitedStaE();

            string[] tmpTypes = r.Split((char)0x1c);

            us.Rectype = "U";
            us.Luno = tmpTypes[1];
            int i = 3;

            us.Dig = tmpTypes[i].Substring(0, 1);
            us.DeviceStatus = tmpTypes[i].Substring(1, 1);

            int count = tmpTypes[i].Length - 2;
            if (count == 8 || count == 16)
            {
                us.DispenseCount = tmpTypes[i].Substring(2, 8);
                if (count == 16)
                    us.SprayCashDispenser = tmpTypes[i].Substring(10, 8);
            }
            else
            {
                us.DispenseCount = tmpTypes[i].Substring(2, 14);
                if (count > 14)
                    us.SprayCashDispenser = tmpTypes[i].Substring(16, 14);
            }

            if (tmpTypes.Length > i + 1)
            {
                us.ErrorSeverity = tmpTypes[i + 1];
            }

            if (tmpTypes.Length > i + 2)
            {
                us.DiagnosticStatus = tmpTypes[i + 2];
            }

            if (tmpTypes.Length > i + 3)
            {
                us.SuppliesStatus = tmpTypes[i + 3];
            }

            return us;
        }
    }
}

