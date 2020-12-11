using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class UnsolicitedStatus : IMessage
    {

        public Dictionary<string, string> usTypes = new Dictionary<string, string>();


        public UnsolicitedStatus()
        {
            usTypes.Add("A", "12B");
            usTypes.Add("B", "12B");
            usTypes.Add("E", "12B");
            usTypes.Add("P", "12B");
            usTypes.Add("R", "12B");
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
            throw new NotImplementedException();
        }

        public virtual bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                List<typeRec> OneTypeRec = new List<typeRec>();
                OneTypeRec.Add(r);

                string recordType = getRecordType(r.typeContent);

                IMessage theRecord = MessageFactory.Create_Record(recordType);

                if (theRecord.writeData(OneTypeRec, key, logID) == false)
                    return false;
            }
            return true;
        }

        internal string getRecordType(string recValue)
        {
            string[] tmpTypes = recValue.Split((char)0x1c);
            return usTypes[tmpTypes[3].Substring(0, 1)];
        }
    }
}
