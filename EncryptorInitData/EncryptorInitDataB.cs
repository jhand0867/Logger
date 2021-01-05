﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct encryptorInitB
    {
        private string rectype;
        private string luno;
        private string informationIdentifier;
        private string remoteKeyProtocol;
        private string certificateState;
        private string eppVarLgthSNCap;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string InformationIdentifier { get => informationIdentifier; set => informationIdentifier = value; }
        public string RemoteKeyProtocol { get => remoteKeyProtocol; set => remoteKeyProtocol = value; }
        public string CertificateState { get => certificateState; set => certificateState = value; }
        public string EppVarLgthSNCap { get => eppVarLgthSNCap; set => eppVarLgthSNCap = value; }
    };

    class EncryptorInitDataB : EncryptorInitData
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                encryptorInitB kB = parseData(r.typeContent);

                string sql = @"INSERT INTO encryptorInitDataB([logkey],[rectype],[luno],[informationIdentifier],
	                        [remoteKeyProtocol],[certificateState],[eppVarLgthSNCap,[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + kB.Rectype + "','" + kB.Luno + "','" +
                               kB.InformationIdentifier + "','" + kB.RemoteKeyProtocol+ "','" + 
                               kB.CertificateState + "','" + kB.EppVarLgthSNCap + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public encryptorInitB parseData(string r)
        {
            encryptorInitB kB = new encryptorInitB();

            string[] tmpTypes = r.Split((char)0x1c);

            kB.Rectype = "K";
            kB.Luno = tmpTypes[1];
            kB.InformationIdentifier = tmpTypes[3];

            kB.RemoteKeyProtocol = tmpTypes[4].Substring(0,2);
            kB.CertificateState = tmpTypes[4].Substring(2, 2);
            kB.EppVarLgthSNCap = tmpTypes[4].Substring(4, 1);

            return kB;
        }
    }
}