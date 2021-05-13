using System.Collections.Generic;
using System.Data;

namespace Logger
{
    // todo:  revise write and parse need to include response flag, Luno, message sequence

    class ConfigIdRec : App, IMessage
    {
        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();

            string sql = @"SELECT TOP 1 * FROM configId WHERE prjkey = '" +
                                                           projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                           logKey + "%'";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            int loadNum = 0;
            foreach (typeRec r in typeRecs)
            {
                if (r.typeContent.Length < 4)
                {
                    continue;
                }

                loadNum++;


                string sql = @"INSERT INTO configId([logkey],[rectype],[configID],[load],[prjkey],[logID])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   'I' + "','" +
                                    r.typeContent.Substring(0, 4) + "','" + // screenNum
                                    loadNum.ToString() + "','" + key + "'," + logID + ")";

                DbCrud db = new DbCrud();
                if (db.crudToDb(sql) == false)
                    return false;
            }
            return true;

        }

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'I' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable configId = getDescription();

            txtField = getOptionDescription(configId, "01", dts[0].Rows[0][3].ToString());

            return txtField;
        }
    }
}
