using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct encryptorInit3
    {
        private string rectype;
        private string luno;
        private string informationIdentifier;
        private string newKVV;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string NewKVV { get => newKVV; set => newKVV = value; }
    };

    class EncryptorInitData3 : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInit3 k3 = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitData3([logkey],[rectype],[luno],
	                        [informationIdentifier],[newKVV],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + k3.Rectype + "','" + k3.Luno + "','" +
                              k3.InformationIdentifier + "','" + k3.NewKVV + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInit3 parseData(string r)
        {
            encryptorInit3 k3 = new encryptorInit3();

            string[] tmpTypes = r.Split((char)0x1c);

            k3.Rectype = "K";
            k3.Luno = tmpTypes[1];
            k3.InformationIdentifier = tmpTypes[3];
            k3.NewKVV = tmpTypes[4];

            return k3;
        }
    }
}
