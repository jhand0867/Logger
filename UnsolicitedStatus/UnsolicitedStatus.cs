using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.UnsolicitedStatus
{
    class UnsolicitedStatus : IMessage
    {
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

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            throw new NotImplementedException();
        }
    }
}
