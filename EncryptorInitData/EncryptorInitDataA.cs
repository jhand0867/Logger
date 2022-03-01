using System.Collections.Generic;

namespace Logger
{

    struct encryptorInitA
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;
        private string kvvNewDesKey;
        private string binaryDataLength;
        private string keyLoadAck;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string KvvNewDesKey { get => kvvNewDesKey; set => kvvNewDesKey = value; }
        public string BinaryDataLength { get => binaryDataLength; set => binaryDataLength = value; }
        public string KeyLoadAck { get => keyLoadAck; set => keyLoadAck = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class EncryptorInitDataA : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInitA kA = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitDataA([logkey],[rectype],[messageClass],[messageSubClass],[luno],
	                        [informationIdentifier],[kvvNewDesKey],[binaryDataLength],[keyLoadAck],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + kA.Rectype + "','" + kA.MessageClass + "','" + kA.MessageSubClass + "','" +
                               kA.Luno + "','" + kA.InformationIdentifier + "','" + kA.KvvNewDesKey + "','" +
                               kA.BinaryDataLength + "','" + kA.KeyLoadAck + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInitA parseData(string r)
        {
            encryptorInitA kA = new encryptorInitA();

            string[] tmpTypes = r.Split((char)0x1c);

            kA.Rectype = "K";
            kA.MessageClass = tmpTypes[0].Substring(10, 1);
            kA.MessageSubClass = tmpTypes[0].Substring(11, 1);
            kA.Luno = tmpTypes[1];
            kA.InformationIdentifier = tmpTypes[3];

            if (tmpTypes[4].Length > 5)
                kA.KvvNewDesKey = tmpTypes[4].Substring(0, 6);
            if (tmpTypes[4].Length > 8)
                kA.BinaryDataLength = tmpTypes[4].Substring(6, 3);
            if (tmpTypes[4].Length > 9)
                kA.KeyLoadAck = tmpTypes[4].Substring(9, tmpTypes[4].Length - 9);

            return kA;
        }
    }
}
