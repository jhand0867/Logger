using System.Collections.Generic;

namespace Logger
{

    struct unsolicitedStaB
    {
        private string rectype;
        private string messageClass;
        private string messageSubclass;
        private string luno;
        private string dig;
        private string deviceStatus;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string Dig { get => dig; set => dig = value; }
        public string DeviceStatus { get => deviceStatus; set => deviceStatus = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
    };

    class UnsolicitedStatusB : UnsolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                unsolicitedStaB us = parseData(r.typeContent);

                string sql = @"INSERT INTO unsolicitedStatusB([logkey],[rectype],[messageClass],[messageSubclass],[luno],
	                        [dig],[deviceStatus],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + us.Rectype + "','" + us.MessageClass + "','" +
                               us.MessageSubclass + "','" + us.Luno + "','" +
                               us.Dig + "','" + us.DeviceStatus + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public unsolicitedStaB parseData(string r)
        {
            unsolicitedStaB us = new unsolicitedStaB();

            string[] tmpTypes = r.Split((char)0x1c);

            us.Rectype = "U";
            us.MessageClass = tmpTypes[0].Substring(10, 1);
            us.MessageSubclass = tmpTypes[0].Substring(11, 1);

            us.Luno = tmpTypes[1];
            int i = 3;

            us.Dig = tmpTypes[i].Substring(0, 1);
            us.DeviceStatus = tmpTypes[i].Substring(1, tmpTypes[i].Length - 1);

            return us;
        }
    }
}

