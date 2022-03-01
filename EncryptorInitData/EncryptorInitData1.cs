using System.Collections.Generic;

namespace Logger
{

    struct encryptorInit1
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;
        private string eppSerialNumber;
        private string eppSerialNumberSignature;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string EppSerialNumber { get => eppSerialNumber; set => eppSerialNumber = value; }
        public string EppSerialNumberSignature { get => eppSerialNumberSignature; set => eppSerialNumberSignature = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    }
    class EncryptorInitData1 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit1 k1 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData1([logkey],[rectype],[messageClass],[messageSubClass],[luno],
	                        [informationIdentifier],[eppSerialNumber],[eppSerialNumberSignature],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k1.Rectype + "','" + k1.MessageClass + "','" + k1.MessageSubClass + "','" +
                               k1.Luno + "','" + k1.InformationIdentifier + "','" + k1.EppSerialNumber + "','" +
                               k1.EppSerialNumberSignature + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit1 parseData(string r)
        {
            encryptorInit1 k1 = new encryptorInit1();

            string[] tmpTypes = r.Split((char)0x1c);

            k1.Rectype = "K";
            k1.MessageClass = tmpTypes[0].Substring(10, 1);
            k1.MessageSubClass = tmpTypes[0].Substring(11, 1);
            k1.Luno = tmpTypes[1];
            k1.InformationIdentifier = tmpTypes[3];

            if (tmpTypes[4].Length > 7)
                k1.EppSerialNumber = tmpTypes[4].Substring(0, 8);

            // epp Serial Number Signature should have 320 characters
            if (tmpTypes[4].Length > 8)
                k1.EppSerialNumberSignature = tmpTypes[4].Substring(8, tmpTypes[4].Length - 8);

            return k1;
        }
    }
}
