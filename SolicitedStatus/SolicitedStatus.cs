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
            ssTypes.Add("F1", "22F1");
            ssTypes.Add("F2", "22F2");
            ssTypes.Add("F3", "22F3");
            ssTypes.Add("F4", "22F4");
            ssTypes.Add("F5", "22F5");
            ssTypes.Add("F6", "22F6");
            ssTypes.Add("FF", "22FF");
            ssTypes.Add("FH", "22FH");
            ssTypes.Add("FI", "22FI");
            ssTypes.Add("FJ", "22FJ");
            ssTypes.Add("FK", "22FK");
            ssTypes.Add("FL", "22FL");
            ssTypes.Add("FM", "22FM");
            ssTypes.Add("FN", "22FN");
        }

        // todo: Solicited Status F-1 to 6

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'N' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            string[] tmpTypes = recValue.Split((char)0x1c);
            string recordType = "";
            int i = 3;

            string[] tmp = tmpTypes[3].Split((char)0x1d);

            if (tmp[0].Length != 1) i = 4;

            if (tmpTypes[i] == "F")
            {
                recordType = ssTypes[tmpTypes[i] + tmpTypes[i + 1].Substring(0, 1)];
            }
            else
            {
                recordType = ssTypes[tmpTypes[i].Substring(0, 1)];
            }

            IMessage theRecord = MessageFactory.Create_Record(recordType);
            return theRecord.parseToView(logKey, logID, projectKey, recValue);

        }

        public virtual bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            foreach (typeRec r in typeRecs)
            {
                List<typeRec> OneTypeRec = new List<typeRec>();
                OneTypeRec.Add(r);

                string[] tmpTypes = r.typeContent.Split((char)0x1c);

                string recordType = "";
                int i = 3;

                string[] tmp = tmpTypes[3].Split((char)0x1d);

                if (tmp[0].Length != 1) i = 4;

                if (tmpTypes[i] == "F")
                {
                    recordType = ssTypes[tmpTypes[i] + tmpTypes[i+1].Substring(0, 1)];
                }
                else
                {
                    recordType = ssTypes[tmpTypes[i].Substring(0,1)];
                }

                // todo: this is if temporal until others types are implemented.

                //if ((recordType == "229") || (recordType == "22B") || (recordType == "228") ||
                //    (recordType == "22C") || (recordType == "22F1") || (recordType == "22F2" ||
                //    (recordType == "22F3") || (recordType == "22F4") || (recordType == "22F5") ||
                //    (recordType == "22F6") || (recordType == "22FH") || (recordType == "22FI") ||
                //    (recordType == "22FJ") || (recordType == "22FK") || (recordType == "22FL") ||
                //    (recordType == "22FM") || (recordType == "22FN")))
                //{
                    IMessage theRecord = MessageFactory.Create_Record(recordType);

                    if (theRecord.writeData(OneTypeRec, Key, logID) == false)
                        return false;
                //}
            }
            return true;
        }

    }
}
