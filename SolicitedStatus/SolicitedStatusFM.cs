﻿using System.Collections.Generic;

namespace Logger
{

    struct solicitedStaFM
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentifier;
        private string localOptionsId;
        private string localOptionsDigits;
        private string dataId;
        private string data;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string LocalOptionsId { get => localOptionsId; set => localOptionsId = value; }
        public string LocalOptionsDigits { get => localOptionsDigits; set => localOptionsDigits = value; }
        public string DataId { get => dataId; set => dataId = value; }
        public string Data { get => data; set => data = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class SolicitedStatusFM : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            log.Info($"Adding {this.GetType().Name}");

            foreach (typeRec r in typeRecs)
            {
                solicitedStaFM ss = parseData(r.typeContent);

                string sql = @"INSERT INTO SolicitedStatusFM([logkey],[rectype],[messageClass],[messageSubClass],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentifier],[localOptionsId],
	                        [localOptionsDigits],[dataId],[data],[mac],[prjkey],[logID]) " +
                            " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.MessageClass + "','" + ss.MessageSubClass + "','" +
                            ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                            ss.MessageIdentifier + "','" + ss.LocalOptionsId + "','" + ss.LocalOptionsDigits + "','" +
                            ss.DataId + "','" + ss.Data + "','" + ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaFM parseData(string r)
        {
            log.Info($"Parsing {this.GetType().Name}");

            solicitedStaFM ss = new solicitedStaFM();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.MessageClass = tmpTypes[0].Substring(10, 1);
            ss.MessageSubClass = tmpTypes[0].Substring(11, 1);
            ss.Luno = tmpTypes[1];
            int i = 3;

            if (tmpTypes[3].Length != 1)
            {
                i = 4;
                ss.TimeVariant = tmpTypes[3];
            }

            ss.StatusDescriptor = tmpTypes[i];
            ss.MessageIdentifier = tmpTypes[i + 1].Substring(0, 1);
            ss.LocalOptionsId = tmpTypes[i + 1].Substring(1, 1);
            ss.LocalOptionsDigits = tmpTypes[i + 1].Substring(2, tmpTypes[i + 1].Length - 2);

            if (tmpTypes.Length > i + 2)
            {
                ss.DataId = tmpTypes[i + 2].Substring(0, 1);
                ss.Data = tmpTypes[i + 2].Substring(1, tmpTypes[i + 2].Length - 1);
            }

            if (tmpTypes.Length > i + 3)
                ss.Mac = tmpTypes[i + 3];

            return ss;
        }
    }
}
