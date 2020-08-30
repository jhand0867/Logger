using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class ExtEncryptionRec : App, IMessage
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
                if (r.typeContent.Length < 3)
                {
                    continue;
                }

                loadNum++;


                sql = @"INSERT INTO extEncryption([logkey],[rectype],[keySize],[keyData],[load],[prjkey],[logID])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   'X' + "','" +
                                    r.typeContent.Substring(0, 3) + "','" + // key data size
                                    r.typeContent.Substring(3, r.typeContent.Length - 3) + "','" + // Key Data
                                    loadNum.ToString() + "','" + key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.addToDb(sql) == false)
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
