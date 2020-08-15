using System.Collections.Generic;
using System.Data;

namespace Logger
{
    public interface IMessage
    {
        bool writeData(List<typeRec> typeRecs, string key, string logID);

        List<DataTable> getRecord(string logKey, string logID, string projectKey);

        DataTable getDescription();
    }
}
