using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class DateAndTimeRec : App, IMessage
    {
        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            return null;
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            String sql = "";
            int loadNum = 0;
            foreach (typeRec r in typeRecs)
            {
                if (r.typeContent.Length < 10)
                {
                    continue;
                }

                loadNum++;


                sql = @"INSERT INTO DateTime([logkey],[rectype],[date],[time],[load],[prjkey],[logID])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   'D' + "','" +
                                    r.typeContent.Substring(0, 6) + "','" + // date
                                    r.typeContent.Substring(6, 4) + "','" + // time
                                    loadNum.ToString() + "','" + key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;
        }

        DataTable IMessage.getDescription()
        {
            throw new NotImplementedException();
        }

        List<DataTable> IMessage.getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }
    }
}
