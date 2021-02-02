using System.Collections.Generic;

namespace Logger
{

    struct encryptorInit7
    {
        private string rectype;
        private string luno;
        private string informationIdentifier;
        private string binaryDataLength;
        private string rsaKVV;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string BinaryDataLength { get => binaryDataLength; set => binaryDataLength = value; }
        public string RsaKVV { get => rsaKVV; set => rsaKVV = value; }
    };

    class EncryptorInitData7 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit7 k7 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData7([logkey],[rectype],[luno],
	                        [informationIdentifier],[binaryDataLength],[rsaKVV],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k7.Rectype + "','" +
                               k7.Luno + "','" + k7.InformationIdentifier + "','" + k7.BinaryDataLength + "','" +
                               k7.RsaKVV + "','" + Key + "'," + logID + ")";




                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit7 parseData(string r)
        {
            encryptorInit7 k7 = new encryptorInit7();

            string[] tmpTypes = r.Split((char)0x1c);

            k7.Rectype = "K";
            k7.Luno = tmpTypes[1];
            k7.InformationIdentifier = tmpTypes[3];

            k7.BinaryDataLength = tmpTypes[4].Substring(0, 3);
            k7.RsaKVV = tmpTypes[4].Substring(3, tmpTypes[4].Length - 3);

            return k7;
        }
    }
}
