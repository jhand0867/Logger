using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    class SolicitedStatus : IMessage
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
                List<typeRec> OneTypeRec = new List<typeRec>();
                OneTypeRec.Add(r);

                string[] tmpTypes = r.typeContent.Split((char)0x1c);
                string recordType = ssTypes[tmpTypes[3]];

                // todo: this is if temporal until others types are implemented.

                if ((recordType == "229") || (recordType == "22B"))
                {
                    IMessage theRecord = MessageFactory.Create_Record(recordType);

                    if (theRecord.writeData(OneTypeRec, Key, logID) == false)
                        return false;
                }
            }
            return true;
        }

    }
}
