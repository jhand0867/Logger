using System.Collections.Generic;

namespace Logger
{

    struct encryptorInit6
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;
        private string keyEntryMode;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string KeyEntryMode { get => keyEntryMode; set => keyEntryMode = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class EncryptorInitData6 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit6 k6 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData6([logkey],[rectype],[messageClass],[messageSubClass],[luno],
	                        [informationIdentifier],[keyEntryMode],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k6.Rectype + "','" + k6.MessageClass + "','" + k6.MessageSubClass + "','" +
                               k6.Luno + "','" + k6.InformationIdentifier + "','" +
                               k6.KeyEntryMode + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit6 parseData(string r)
        {
            encryptorInit6 k6 = new encryptorInit6();

            string[] tmpTypes = r.Split((char)0x1c);

            k6.Rectype = "K";
            k6.MessageClass = tmpTypes[0].Substring(10, 1);
            k6.MessageSubClass = tmpTypes[0].Substring(11, 1);
            k6.Luno = tmpTypes[1];
            k6.InformationIdentifier = tmpTypes[3];
            k6.KeyEntryMode = tmpTypes[4];

            return k6;
        }
    }
}
