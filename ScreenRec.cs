using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    // todo:  COMPLETE CLASS
    public struct scrRec
    {
        private string rectype;
        private string messageClass;
        private string responseFlag;
        private string luno;
        private string messageSeqNumber;
        private string messageSubclass;
        private string messageIdentifier;

        public string Rectype { get => rectype; set => rectype = value; }
        public string MessageClass { get => messageClass; set => messageClass = value; }
        public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
        public string Luno { get => luno; set => luno = value; }
        public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
        public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
        public string MessageSeqNumber { get => messageSeqNumber; set => messageSeqNumber = value; }
    }

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

        public bool writeData(List<typeRec> inTypeRecs, string Key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = inTypeRecs.Count + 1;

            String sql = "";
            int loadNum = 0;

            foreach (typeRec rParent in inTypeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                string[] tmpTypes = rParent.typeContent.Split((char)0x1c);
                List<typeRec> typeRecs = new List<typeRec>();
                scrRec parms = new scrRec();

                int myInd = tmpTypes[0].Length + tmpTypes[1].Length + tmpTypes[2].Length + tmpTypes[3].Length;
                string typeData = rParent.typeContent.Substring(myInd + 4, rParent.typeContent.Length - (myInd + 4));

                string[] dataTypes = typeData.Split((char)0x1c);

                parms.MessageClass = tmpTypes[0].Substring(10, 1);
                if (tmpTypes[0].Length > 11)
                {
                    parms.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
                }

                parms.Luno = tmpTypes[1];
                parms.MessageSeqNumber = tmpTypes[2];
                parms.MessageSubclass = tmpTypes[3].Substring(0, 1);
                parms.MessageIdentifier = tmpTypes[3].Substring(1, 1);

                foreach (string item in dataTypes)
                {
                    typeRec tr = new typeRec();
                    tr.typeIndex = rParent.typeIndex;
                    tr.typeContent = item;
                    typeRecs.Add(tr);
                }
                int screens = 0;
                foreach (typeRec r in typeRecs)
                {
                    string scrdata = null;
                    string scrnum = null;
                    if (r.typeContent.Length < 3)
                    {
                        continue;
                    }
                    scrnum = r.typeContent.Substring(0, 3);
                    screens++;

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
                                       'E' + "','" +
                          r.typeContent.Substring(0, 3) + "','" + // screenNum
                                       scrdata + "','" + // screenData
                                       loadNum.ToString() + "','" +
                                       Key + "'," + logID + ")";


                    DbCrud db = new DbCrud();
                    if (db.crudToDb(sql) == false)
                        return false;
                }
                sql = @"INSERT INTO screenRec([logkey],[rectype],[messageClass],[responseFlag],[luno],[messageSeqNumber],
                        [messageSubclass],[messageIdentifier],[screens],[load],[prjkey],[logID]) " +
                        " VALUES('" +
                                rParent.typeIndex + "','" +
                               'E' + "','" +
                               parms.MessageClass + "','" +
                               parms.ResponseFlag + "','" +
                               parms.Luno + "','" +
                               parms.MessageSeqNumber + "','" +
                               parms.MessageSubclass + "','" +
                               parms.MessageIdentifier + "'," +
                               screens + ",'" +
                               loadNum.ToString() + "','" +
                               Key + "'," +
                               logID + ")";

                DbCrud db1 = new DbCrud();
                if (db1.crudToDb(sql) == false)
                    return false;

            }

            lpb.Visible = false;
            return true;

        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM screenRec WHERE prjkey = '" +
               projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT * FROM screeninfo WHERE prjkey = '" +
                           projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;

        }

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'E' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable scrdt = getDescription();
            bool dtFirst = true;

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                    if (dtFirst == true)
                    {
                        for (int colNum = 3; colNum < dt.Columns.Count - 3; colNum++)
                            txtField += App.Prj.getOptionDescription(scrdt, "H" + colNum.ToString("00"), dt.Rows[0][colNum].ToString());
                        dtFirst = false;
                    }
                    else
                    {
                        for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                        {
                            for (int fieldNum = 3; fieldNum < dt.Columns.Count - 3; fieldNum++)
                                txtField += getOptionDescription(scrdt, fieldNum.ToString("00"), dt.Rows[rowNum][fieldNum].ToString());
                        }
                    }
            }

            return txtField;
        }
    }

}
