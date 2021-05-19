using System.Collections.Generic;

namespace Logger
{

    struct unsolicitedStaP
    {
        private string rectype;
        private string luno;
        private string dig;
        private string dsbyte1;
        private string dsbyte2;
        private string dsbyte3;
        private string dsbyte4;
        private string dsbyte5;
        private string dsbyte6;
        private string dsbyte7;
        private string dsbyte8;
        private string dsbyte9;
        private string dsbyte10;
        private string dsbyte11;
        private string dsbyte12;
        private string dsbyte13;
        private string dsbyte14;
        private string dsbyte15;
        private string dsbyte16;
        private string dsbyte17;
        private string dsbyte18;
        private string dsbyte19;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string Dig { get => dig; set => dig = value; }
        public string Dsbyte1 { get => dsbyte1; set => dsbyte1 = value; }
        public string Dsbyte2 { get => dsbyte2; set => dsbyte2 = value; }
        public string Dsbyte3 { get => dsbyte3; set => dsbyte3 = value; }
        public string Dsbyte4 { get => dsbyte4; set => dsbyte4 = value; }
        public string Dsbyte5 { get => dsbyte5; set => dsbyte5 = value; }
        public string Dsbyte6 { get => dsbyte6; set => dsbyte6 = value; }
        public string Dsbyte7 { get => dsbyte7; set => dsbyte7 = value; }
        public string Dsbyte8 { get => dsbyte8; set => dsbyte8 = value; }
        public string Dsbyte9 { get => dsbyte9; set => dsbyte9 = value; }
        public string Dsbyte10 { get => dsbyte10; set => dsbyte10 = value; }
        public string Dsbyte11 { get => dsbyte11; set => dsbyte11 = value; }
        public string Dsbyte12 { get => dsbyte12; set => dsbyte12 = value; }
        public string Dsbyte13 { get => dsbyte13; set => dsbyte13 = value; }
        public string Dsbyte14 { get => dsbyte14; set => dsbyte14 = value; }
        public string Dsbyte15 { get => dsbyte15; set => dsbyte15 = value; }
        public string Dsbyte16 { get => dsbyte16; set => dsbyte16 = value; }
        public string Dsbyte17 { get => dsbyte17; set => dsbyte17 = value; }
        public string Dsbyte18 { get => dsbyte18; set => dsbyte18 = value; }
        public string Dsbyte19 { get => dsbyte19; set => dsbyte19 = value; }
    };

    class UnsolicitedStatusP : UnsolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                unsolicitedStaP us = parseData(r.typeContent);

                string sql = @"INSERT INTO unsolicitedStatusP([logkey],[rectype],[luno],
	                        [dig],[dsbyte1],[dsbyte2],[dsbyte3],[dsbyte4],[dsbyte5],[dsbyte6],[dsbyte7],[dsbyte8],
                            [dsbyte9],[dsbyte10],[dsbyte11],[dsbyte12],[dsbyte13],[dsbyte14],[dsbyte15],[dsbyte16],
                            [dsbyte17],[dsbyte18],[dsbyte19],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + us.Rectype + "','" + us.Luno + "','" +
                               us.Dig + "','" + us.Dsbyte1 + "','" + us.Dsbyte2 + "','" + us.Dsbyte3 + "','" +
                               us.Dsbyte4 + "','" + us.Dsbyte5 + "','" + us.Dsbyte6 + "','" + us.Dsbyte7 + "','" +
                               us.Dsbyte8 + "','" + us.Dsbyte9 + "','" + us.Dsbyte10 + "','" + us.Dsbyte11 + "','" +
                               us.Dsbyte12 + "','" + us.Dsbyte13 + "','" + us.Dsbyte14 + "','" + us.Dsbyte15 + "','" +
                               us.Dsbyte16 + "','" + us.Dsbyte17 + "','" + us.Dsbyte18 + "','" + us.Dsbyte19 + "','" +
                               Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public unsolicitedStaP parseData(string r)
        {
            unsolicitedStaP us = new unsolicitedStaP();

            string[] tmpTypes = r.Split((char)0x1c);

            us.Rectype = "U";
            us.Luno = tmpTypes[1];
            int i = 3;

            us.Dig = tmpTypes[i].Substring(0, 1);
            us.Dsbyte1 = tmpTypes[i].Substring(1, 1);
            us.Dsbyte2 = tmpTypes[i].Substring(2, 1);

            if (us.Dsbyte1 == "2") return us;

            us.Dsbyte3 = tmpTypes[i].Substring(3, 1);
            us.Dsbyte4 = tmpTypes[i].Substring(4, 1);
            us.Dsbyte5 = tmpTypes[i].Substring(5, 1);
            us.Dsbyte6 = tmpTypes[i].Substring(6, 1);
            us.Dsbyte7 = tmpTypes[i].Substring(7, 1);
            us.Dsbyte8 = tmpTypes[i].Substring(8, 1);
            us.Dsbyte9 = tmpTypes[i].Substring(9, 1);
            us.Dsbyte10 = tmpTypes[i].Substring(10, 1);
            us.Dsbyte11 = tmpTypes[i].Substring(11, 1);
            us.Dsbyte12 = tmpTypes[i].Substring(12, 1);
            us.Dsbyte13 = tmpTypes[i].Substring(13, 1);

            if (us.Dsbyte1 != "5") return us;

            us.Dsbyte14 = tmpTypes[i].Substring(14, 1);
            us.Dsbyte15 = tmpTypes[i].Substring(15, 1);
            us.Dsbyte16 = tmpTypes[i].Substring(16, 1);
            us.Dsbyte17 = tmpTypes[i].Substring(17, 1);
            us.Dsbyte18 = tmpTypes[i].Substring(18, 1);
            us.Dsbyte19 = tmpTypes[i].Substring(19, 1);

            return us;
        }
    }
}

