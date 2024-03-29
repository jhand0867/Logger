﻿using System.Collections.Generic;

namespace Logger
{
    struct solicitedStaF5
    {
        private string rectype;
        private string messageClass;
        private string messageSubClass;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string messageIdentificer;
        private string typeOfDate;
        private string terminalDateTime;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string MessageIdentificer { get => messageIdentificer; set => messageIdentificer = value; }
        public string TypeOfDate { get => typeOfDate; set => typeOfDate = value; }
        public string TerminalDateTime { get => terminalDateTime; set => terminalDateTime = value; }
        public string Mac { get => mac; set => mac = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string MessageSubClass { get => messageSubClass; set => messageSubClass = value; }
    };

    class SolicitedStatusF5 : SolicitedStatus
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            log.Info($"Adding {this.GetType().Name}");

            foreach (typeRec r in typeRecs)
            {
                solicitedStaF5 ss = parseData(r.typeContent);

                string sql = @"INSERT INTO solicitedStatusF5([logkey],[rectype],[messageClass],[messageSubClass],
	                        [luno],[timeVariant],[statusDescriptor],[messageIdentificer],
	                        [typeOfDate],[terminalDateTime],[mac],[prjkey],[logID]) " +
                      " VALUES('" + r.typeIndex + "','" + ss.Rectype + "','" + ss.MessageClass + "','" + ss.MessageSubClass + "','" +
                               ss.Luno + "','" + ss.TimeVariant + "','" + ss.StatusDescriptor + "','" +
                               ss.MessageIdentificer + "','" + ss.TypeOfDate + "','" + ss.TerminalDateTime + "','" +
                               ss.Mac + "','" + Key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        public solicitedStaF5 parseData(string r)
        {
            log.Info($"Parsing {this.GetType().Name}");

            solicitedStaF5 ss = new solicitedStaF5();

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

            ss.StatusDescriptor = tmpTypes[i].Substring(0, 1);
            i++;
            ss.MessageIdentificer = tmpTypes[i].Substring(0, 1);
            ss.TypeOfDate = tmpTypes[i].Substring(1, 1);
            ss.TerminalDateTime = tmpTypes[i].Substring(2, 12);

            if (tmpTypes.Length > i + 1)
                ss.Mac = tmpTypes[i + 1];

            return ss;
        }
    }
}
