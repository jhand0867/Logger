using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct encryptorInit2
    {
        private string rectype;
        private string luno;
        private string informationIdentifier;
        private string eppPublicKey;
        private string eppPublicKeySignature;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string EppPublicKey { get => eppPublicKey; set => eppPublicKey = value; }
        public string EppPublicKeySignature { get => eppPublicKeySignature; set => eppPublicKeySignature = value; }
    };

    class EncryptorInitData2 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit2 k2 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData2([logkey],[rectype],[luno],
	                        [informationIdentifier],[eppPublicKey],[eppPublicKeySignature],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k2.Rectype + "','" +
                               k2.Luno + "','" + k2.InformationIdentifier + "','" + k2.EppPublicKey + "','" + 
                               k2.EppPublicKeySignature + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit2 parseData(string r)
        {
            encryptorInit2 k2 = new encryptorInit2();

            string[] tmpTypes = r.Split((char)0x1c);

            k2.Rectype = "K";
            k2.Luno = tmpTypes[1];
            k2.InformationIdentifier = tmpTypes[3];

            k2.EppPublicKey = tmpTypes[4].Substring(0,320);
            k2.EppPublicKeySignature = tmpTypes[4].Substring(320, 320);

            return k2;
        }
    }
}
