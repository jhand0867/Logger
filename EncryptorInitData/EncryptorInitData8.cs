using System.Collections.Generic;

namespace Logger
{

    struct encryptorInit8
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;
        private string binaryDataLength;
        private string sstCertificate;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string BinaryDataLength { get => binaryDataLength; set => binaryDataLength = value; }
        public string SstCertificate { get => sstCertificate; set => sstCertificate = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class EncryptorInitData8 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit8 k8 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData8([logkey],[rectype],[messageClass],[messageSubClass],[luno],
	                        [informationIdentifier],[binaryDataLength],[sstCertificate],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k8.Rectype + "','" + k8.MessageClass + "','" + k8.MessageSubClass + "','" +
                               k8.Luno + "','" + k8.InformationIdentifier + "','" + k8.BinaryDataLength + "','" +
                               k8.SstCertificate + "','" + Key + "'," + logID + ")";




                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit8 parseData(string r)
        {
            encryptorInit8 k8 = new encryptorInit8();

            string[] tmpTypes = r.Split((char)0x1c);

            k8.Rectype = "K";
            k8.MessageClass = tmpTypes[0].Substring(10, 1);
            k8.MessageSubClass = tmpTypes[0].Substring(11, 1);
            k8.Luno = tmpTypes[1];
            k8.InformationIdentifier = tmpTypes[3];

            k8.BinaryDataLength = tmpTypes[4].Substring(0, 3);
            k8.SstCertificate = tmpTypes[4].Substring(3, tmpTypes[4].Length - 3);

            return k8;
        }
    }
}
