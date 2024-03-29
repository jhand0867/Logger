﻿using System.Collections.Generic;

namespace Logger
{

    struct encryptorInitE
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string informationIdentifier;
        private string eppSerialNumber;
        private string snMultipliedbySKvendor;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string EppSerialNumber { get => eppSerialNumber; set => eppSerialNumber = value; }
        public string SnMultipliedbySKvendor { get => snMultipliedbySKvendor; set => snMultipliedbySKvendor = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class EncryptorInitDataE : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInitE kE = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitDataE([logkey],[rectype],[messageClass],[messageSubClass],[luno],
	                        [informationIdentifier],[eppSerialNumber],[snMultipliedbySKvendor],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + kE.Rectype + "','" + kE.MessageClass + "','" + kE.MessageSubClass + "','" +
                               kE.Luno + "','" + kE.InformationIdentifier + "','" + kE.EppSerialNumber + "','" +
                               kE.SnMultipliedbySKvendor + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInitE parseData(string r)
        {
            encryptorInitE kE = new encryptorInitE();

            string[] tmpTypes = r.Split((char)0x1c);

            kE.Rectype = "K";
            kE.MessageClass = tmpTypes[0].Substring(10, 1);
            kE.MessageSubClass = tmpTypes[0].Substring(11, 1);
            kE.Luno = tmpTypes[1];
            kE.InformationIdentifier = tmpTypes[3];

            string[] eppAttributes = tmpTypes[4].Split((char)0x1d);

            if (eppAttributes.Length > 0)
                kE.EppSerialNumber = eppAttributes[0];
            if (eppAttributes.Length > 1)
                kE.SnMultipliedbySKvendor = eppAttributes[1];

            return kE;
        }
    }
}
