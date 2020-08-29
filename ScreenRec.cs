using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    public class screenRec : App, IMessage
    {
        private string plogkey;
        private string prectype;
        private string pscrnum;
        private string pscrdata;
        private string pload;

        public bool ValidateScreen(string scrNum)
        {
            string sql = @"SELECT * FROM [screeninfo] " +
                                 "WHERE [scrnum ] ='" + scrNum + "'";

            Dictionary<string, screenRec> resultData = readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error screen number does not exist");
                return false;
            }
            return true;

        }

        public new Dictionary<string, screenRec> readData(string sql)
        {
            // here mlh

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            Dictionary<string, screenRec> dicData = new Dictionary<string, screenRec>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    screenRec sr = new screenRec();
                    sr.prectype = row[2].ToString();
                    sr.pscrnum = row[3].ToString();
                    sr.pscrdata = row[4].ToString();
                    sr.pload = row[5].ToString();
                    dicData.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), sr);

                }
            }
            return dicData;
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
        {
            String sql = "";
            int loadNum = 0;
            foreach (typeRec r in typeRecs)
            {
                string scrdata = null;
                string scrnum = null;
                if (r.typeContent.Length < 3)
                {
                    continue;
                }
                scrnum = r.typeContent.Substring(0, 3);

                if (r.typeContent.Substring(0, 3) == "C00")
                {
                    loadNum++;
                }

                if (r.typeContent.Length > 3)
                {
                    scrdata = r.typeContent.Substring(3, r.typeContent.Length - 3);
                    scrdata = scrdata.Replace(@"'", @"''");
                }

                sql = @"INSERT INTO screeninfo([logkey],[rectype],[scrnum],[scrdata],[load],[prjkey],[logID])" +
                      " VALUES('" + r.typeIndex + "','" +
                                   'C' + "','" +
                      r.typeContent.Substring(0, 3) + "','" + // screenNum
                                   scrdata + "','" + // screenData
                                   loadNum.ToString() + "','" +
                                   Key + "'," + logID + ")";


                DbCrud db = new DbCrud();
                if (db.addToDb(sql) == false)
                    return false;
            }
            return true;

        }

        List<DataTable> IMessage.getRecord(string logKey, string logID, string projectKey)
        {
            return null;
        }

        DataTable IMessage.getDescription()
        {
            return null;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            throw new NotImplementedException();
        }
    }
}
