using System;
using System.Collections.Generic;
using System.Data;


namespace Logger
{
    public struct fitRec
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

    public class FitData : App, IMessage
    {
        private string prectype;
        private string pfitnum;

        public bool ValidateFit(string fitNum)
        {
            string sql = @"SELECT * FROM [screeninfo] " +
                                 "WHERE [scrnum ] ='" + fitNum + "'";

            Dictionary<string, FitData> resultData = readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error FIT number does not exist");
                return false;
            }
            return true;

        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM fitrec WHERE prjkey = '" +
               projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";

            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT * FROM fitInfo WHERE prjkey = '" + projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public new Dictionary<string, FitData> readData(string sql)
        {
            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            Dictionary<string, FitData> dicData = new Dictionary<string, FitData>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FitData fr = new FitData();
                    fr.prectype = row[2].ToString();
                    fr.pfitnum = row[3].ToString();
                    dicData.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), fr);
                }
            }
            return dicData;
        }

        public bool writeData(List<typeRec> inTypeRecs, string Key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = inTypeRecs.Count + 1;

            int loadNum = 0;
            string sql;

            foreach (typeRec rParent in inTypeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                string[] tmpTypes = rParent.typeContent.Split((char)0x1c);
                List<typeRec> typeRecs = new List<typeRec>();
                fitRec parms = new fitRec();

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


                foreach (typeRec r in typeRecs)
                {
                    if (r.typeContent.Length == 8)
                    {
                        continue;
                    }
                    if (r.typeContent.Substring(0, 3) == "000")
                    {
                        loadNum++;
                    }
                    // convert to hex
                    string[] fit2Hex = new string[42];

                    for (int x = 0, y = 0; x <= r.typeContent.Length - 1; x = x + 3, y++)
                    {
                        string s1 = r.typeContent.Substring(x, 3);
                        if (x == 0)
                        {
                            fit2Hex[y] = s1;
                            continue;
                        }
                        Utility U = new Utility();
                        fit2Hex[y] = U.dec2hex(s1, 2);
                    }

                    sql = @"INSERT INTO fitinfo([logkey],[rectype],[fitnum],[piddx],[pfiid],[pstdx],[pagdx],[pmxpn]," +
                                               "[pckln],[pinpd],[pandx],[panln],[panpd], [prcnt],[pofdx],[pdctb]," +
                                               "[pekey],[pindx],[plndx],[pmmsr],[reserved],[pbfmt],[load],[prjkey],[logID])" +
                          " VALUES('" + r.typeIndex + "','" + // key
                                       'F' + "','" + // record type
                                       fit2Hex[0] + "','" + // fitnum
                                       fit2Hex[1] + "','" + // piddx
                                       fit2Hex[2] + fit2Hex[3] + fit2Hex[4] +
                                       fit2Hex[5] + fit2Hex[6] + "','" + // pfiid
                                       fit2Hex[7] + "','" + // pstdx
                                       fit2Hex[8] + "','" + // pagdx
                                       fit2Hex[9] + "','" + // pmxpn
                                       fit2Hex[10] + "','" + // pckln
                                       fit2Hex[11] + "','" + // pinpd
                                       fit2Hex[12] + "','" + // pandx
                                       fit2Hex[13] + "','" + // panln
                                       fit2Hex[14] + "','" + // panpd
                                       fit2Hex[15] + "','" + // prcnt
                                       fit2Hex[16] + "','" + // pofdx
                                       fit2Hex[17] + fit2Hex[18] + fit2Hex[19] +
                                       fit2Hex[20] + fit2Hex[21] + fit2Hex[22] +
                                       fit2Hex[23] + fit2Hex[24] + "','" + // pdctb
                                       fit2Hex[25] + fit2Hex[26] + fit2Hex[27] +
                                       fit2Hex[28] + fit2Hex[29] + fit2Hex[30] +
                                       fit2Hex[31] + fit2Hex[32] + "','" + // pekey
                                       fit2Hex[33] + fit2Hex[34] + fit2Hex[35] + "','" + // pindx
                                       fit2Hex[36] + "','" + // plndx
                                       fit2Hex[37] + "','" + // pmmsr
                                       fit2Hex[38] + fit2Hex[39] + fit2Hex[40] + "','" + // reserved
                                       fit2Hex[41] + "','" + // pbfmt
                                       loadNum + "','" + // load
                                       Key + "'," + // project key
                                       logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.crudToDb(sql) == false)
                        return false;
                }
                sql = @"INSERT INTO fitrec([logkey],[rectype],[messageClass],[responseFlag],[luno],[messageSeqNumber],
                        [messageSubclass],[messageIdentifier],[load],[prjkey],[logID]) " +
          " VALUES('" +
                        rParent.typeIndex + "','" +
                       'F' + "','" +
                       parms.MessageClass + "','" +
                       parms.ResponseFlag + "','" +
                       parms.Luno + "','" +
                       parms.MessageSeqNumber + "','" +
                       parms.MessageSubclass + "','" +
                       parms.MessageIdentifier + "','" +
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

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'F' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable fitdt = getDescription();
            bool dtFirst = true;

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                    if (dtFirst == true)
                    {
                        for (int colNum = 3; colNum < dt.Columns.Count - 3; colNum++)
                            txtField += App.Prj.getOptionDescription(fitdt, "H" + colNum.ToString("00"), dt.Rows[0][colNum].ToString());
                        dtFirst = false;
                    }
                    else
                    {

                        for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                            for (int fieldNum = 3; fieldNum < dt.Columns.Count - 5; fieldNum++)

                                if (fieldNum == 3)
                                {
                                    txtField += @"\b" + getOptionDescription(fitdt, fieldNum.ToString("00"), dt.Rows[rowNum][fieldNum].ToString()) + @"\b0";
                                }
                                else
                                    txtField += getOptionDescription(fitdt, fieldNum.ToString("00"), dt.Rows[rowNum][fieldNum].ToString());
                    }
            }

            return txtField;
        }
    }
}

