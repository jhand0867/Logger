using System.Collections.Generic;

namespace Logger
{

    struct encryptorInit4
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;
        private string masterKVV;
        private string comKVV;
        private string macKVV;
        private string bkeyKVV;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string MasterKVV { get => masterKVV; set => masterKVV = value; }
        public string ComKVV { get => comKVV; set => comKVV = value; }
        public string MacKVV { get => macKVV; set => macKVV = value; }
        public string BkeyKVV { get => bkeyKVV; set => bkeyKVV = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class EncryptorInitData4 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit4 k4 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData4([logkey],[rectype],[messageClass],[messageSubClass],[luno],
	                        [informationIdentifier],[masterKVV],[comKVV],[macKVV],[bkeyKVV],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k4.Rectype + "','" + k4.MessageClass + "','" + k4.MessageSubClass + "','" + k4.Luno + "','" +
                               k4.InformationIdentifier + "','" + k4.MasterKVV + "','" + k4.ComKVV + "','" +
                               k4.MacKVV + "','" + k4.BkeyKVV + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit4 parseData(string r)
        {
            encryptorInit4 k4 = new encryptorInit4();

            string[] tmpTypes = r.Split((char)0x1c);

            k4.Rectype = "K";
            k4.MessageClass = tmpTypes[0].Substring(10, 1);
            k4.MessageSubClass = tmpTypes[0].Substring(11, 1);
            k4.Luno = tmpTypes[1];
            k4.InformationIdentifier = tmpTypes[3];

            if (tmpTypes[4].Length > 5)
                k4.MasterKVV = tmpTypes[4].Substring(0, 6);
            if (tmpTypes[4].Length > 11)
                k4.ComKVV = tmpTypes[4].Substring(6, 6);
            if (tmpTypes[4].Length > 17)
                k4.MacKVV = tmpTypes[4].Substring(12, 6);
            if (tmpTypes[4].Length > 23)
                k4.BkeyKVV = tmpTypes[4].Substring(18, 6);

            return k4;
        }
    }
}
