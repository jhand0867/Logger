﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    struct solicitedSta
    {
        private string rectype;
        private string luno;
        private string timeVariant;
        private string statusDescriptor;
        private string statusInformation;
        private string mac;

        public string Rectype { get => rectype; set => rectype = value; }
        public string Luno { get => luno; set => luno = value; }
        public string TimeVariant { get => timeVariant; set => timeVariant = value; }
        public string StatusDescriptor { get => statusDescriptor; set => statusDescriptor = value; }
        public string StatusInformation { get => statusInformation; set => statusInformation = value; }
        public string Mac { get => mac; set => mac = value; }
    };

    class SolicitedStatus : App, IMessage
    {

        public Dictionary<string, string> ssTypes = new Dictionary<string, string>();

    
        public SolicitedStatus()
        {
            ssTypes.Add("A", "229");
            ssTypes.Add("B", "22B");
            ssTypes.Add("8", "228");
            ssTypes.Add("9", "229");
            ssTypes.Add("C", "22C");
            ssTypes.Add("F", "22F");
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public DataTable getDescription()
        {
            throw new NotImplementedException();
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            return null;
        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                string ss1 = ssTypes[tmpTypes[3]];

                // todo: this is if temporal until others types are implemented.

                if (ss1 == "229")
                {
                    IMessage ss2 = MessageFactory.Create_Record(ss1);
                    List<typeRec> OneTypeRec = new List<typeRec>();
                    OneTypeRec.Add(r);
                    if (ss2.writeData(OneTypeRec, Key, logID) == false)
                        return false;
                }
            }
            return true;
        }

        public solicitedSta parseData(string r)
        {
            solicitedSta ss = new solicitedSta();

            string[] tmpTypes = r.Split((char)0x1c);

            ss.Rectype = "N";
            ss.Luno = tmpTypes[1];
            ss.TimeVariant = tmpTypes[2];
            ss.StatusDescriptor = tmpTypes[3];

            if (ss.StatusDescriptor == "9" || ss.StatusDescriptor == "A")
            {
                if (tmpTypes.Length > 4)
                    ss.StatusInformation = tmpTypes[4];

                if (tmpTypes.Length > 5)
                    ss.Mac = tmpTypes[5];
            }
            return ss;
        }
    }
}
