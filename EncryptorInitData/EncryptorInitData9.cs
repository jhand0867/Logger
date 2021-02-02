using System.Collections.Generic;

namespace Logger
{

    struct encryptorInit9
    {
        private string rectype;
        private string luno;
        private string informationIdentifier;
        private string sstRandomNumber;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string SstRandomNumber { get => sstRandomNumber; set => sstRandomNumber = value; }
    };

    class EncryptorInitData9 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit9 k9 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData9([logkey],[rectype],[luno],
	                        [informationIdentifier],[sstRandomNumber],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k9.Rectype + "','" +
                               k9.Luno + "','" + k9.InformationIdentifier + "','" +
                               k9.SstRandomNumber + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit9 parseData(string r)
        {
            encryptorInit9 k9 = new encryptorInit9();

            string[] tmpTypes = r.Split((char)0x1c);

            k9.Rectype = "K";
            k9.Luno = tmpTypes[1];
            k9.InformationIdentifier = tmpTypes[3];

            k9.SstRandomNumber = tmpTypes[4];

            return k9;
        }
    }
}
