﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedStaFJ
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string hardwareFitnessID;
        private string hardwareFitnessData;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string HardwareFitnessID { get => hardwareFitnessID; set => hardwareFitnessID = value; }
        public string HardwareFitnessData { get => hardwareFitnessData; set => hardwareFitnessData = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatusFJ : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                solicitedStaFJ ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusFJ([logkey],[rectype],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[hardwareFitnessID],
	                        [hardwareFitnessData],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.HardwareFitnessID + "','" + ss.HardwareFitnessData + "','" +
                            ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaFJ parseData(string r)
        {
            solicitedStaFJ ss = new solicitedStaFJ();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.Luno = tmpTypes[1];

            int i = 3;
            if (tmpTypes[3].Length != 1)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            ss.StatusDescriptor = tmpTypes[i];

            // separating data by device
            string[] statusInfo = tmpTypes[i + 1].Split((char)0x1d);

            ss.MessageIdentifier = statusInfo[0].Substring(0, 1);

            // this is the initial element id A
            ss.HardwareFitnessID = statusInfo[0].Substring(1, 1);

            ss.HardwareFitnessData = statusInfo[0].Substring(2, statusInfo[0].Length - 2);
            for (int x = 1; x < statusInfo.Length; x++)
            {
                ss.HardwareFitnessData += ";" + statusInfo[x];
            }

            if (tmpTypes.Length > i + 2)
                ss.Mac = tmpTypes[i + 2];
            return ss;
        }
    }
}

