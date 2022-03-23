using System.Collections.Generic;

namespace Logger
{

    struct encryptorInit5
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class EncryptorInitData5 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit5 k5 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData5([logkey],[rectype],[messageClass],[messageSubClass],[luno],
	                        [informationIdentifier],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k5.Rectype + "','" + k5.MessageClass + "','" + k5.MessageSubClass + "','" +
                               k5.Luno + "','" + k5.InformationIdentifier + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit5 parseData(string r)
        {
            encryptorInit5 k5 = new encryptorInit5();

            string[] tmpTypes = r.Split((char)0x1c);

            k5.Rectype = "K";
            k5.MessageClass = tmpTypes[0].Substring(10, 1);
            k5.MessageSubClass = tmpTypes[0].Substring(11, 1);
            k5.Luno = tmpTypes[1];
            k5.InformationIdentifier = tmpTypes[3];

            return k5;
        }
    }
}
