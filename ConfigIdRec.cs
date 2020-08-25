using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class ConfigIdRec : App, IMessage
    {
        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            DataTable dt = new DataTable();
            List<DataTable> dts = new List<DataTable>();

            string sql = @"SELECT TOP 1 * FROM configId WHERE prjkey = '" +
                                                           projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                           logKey + "%'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            String sql = "";
            int loadNum = 0;
            foreach (typeRec r in typeRecs)
            {
                if (r.typeContent.Length < 4)
                {
                    continue;
                }

                loadNum++;


                sql = @"INSERT INTO configId([logkey],[rectype],[configID],[load],[prjkey],[logID])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   'I' + "','" +
                                    r.typeContent.Substring(0, 4) + "','" + // screenNum
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

        public string parseToView(DataTable dt, int rowNum, string txtField)
        {
            txtField = dt.Columns[3].ColumnName + " = " + dt.Rows[0][3].ToString();
            return txtField;
        }
    }
}
