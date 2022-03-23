using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{
    public struct stateRec
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

    public class StateData : App, IMessage

    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string pStateNum;
        private string pStateType;
        private string pSta1;
        private string pSta2;
        private string pSta3;
        private string pSta4;
        private string pSta5;
        private string pSta6;
        private string pSta7;
        private string pSta8;


        public string StateNumber
        {
            get { return pStateNum; }
            set { pStateNum = value; }
        }
        public string StateType
        {
            get { return pStateType; }
            set { pStateType = value; }
        }
        public string Val1
        {
            get { return pSta1; }
            set { pSta1 = value; }
        }
        public string Val2
        {
            get { return pSta2; }
            set { pSta2 = value; }
        }
        public string Val3
        {
            get { return pSta3; }
            set { pSta3 = value; }
        }
        public string Val4
        {
            get { return pSta4; }
            set { pSta4 = value; }
        }
        public string Val5
        {
            get { return pSta5; }
            set { pSta5 = value; }
        }
        public string Val6
        {
            get { return pSta6; }
            set { pSta6 = value; }
        }
        public string Val7
        {
            get { return pSta7; }
            set { pSta7 = value; }
        }
        public string Val8
        {
            get { return pSta8; }
            set { pSta8 = value; }
        }


        public virtual void ValidateState(StateData stateData)
        {
            string stateTypes = @"ABCDEFGHIJKLMNORSTVWXYZbdefgkmwz->&z";
            if (!(stateTypes.Contains(stateData.StateType)))
            {
                Console.WriteLine("Not a valid state type");
            }
        }

        public Dictionary<string, StateData> ValidateStateNumber(string value)
        {
            int stateNum = Convert.ToInt32(value);
            if (!(stateNum >= 0 && stateNum <= 999))
            {
                Console.WriteLine("Value not in range 000 to 999");
                return null;
            }

            string sql = @"SELECT * FROM [stateinfo] " +
                          "WHERE [stateNum ] ='" + value + "'";

            Dictionary<string, StateData> resultData = this.readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error state does not exist");
                return null;
            }
            return resultData;
        }

        public new Dictionary<string, StateData> readData(string sql)
        {
            // here mlh

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            Dictionary<string, StateData> dicData = new Dictionary<string, StateData>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    StateData sr = LoggerFactory.Create_StateRecord();
                    sr.StateNumber = row[3].ToString();
                    sr.StateType = row[4].ToString();
                    sr.Val1 = row[5].ToString();
                    sr.Val2 = row[6].ToString();
                    sr.Val3 = row[7].ToString();
                    sr.Val4 = row[8].ToString();
                    sr.Val5 = row[9].ToString();
                    sr.Val6 = row[10].ToString();
                    sr.Val7 = row[11].ToString();
                    sr.Val8 = row[12].ToString();
                    dicData.Add(row[1].ToString() + Convert.ToInt64(row[0]).ToString(), sr);
                }
            }
            return dicData;
        }

        public bool writeData(List<typeRec> inTypeRecs, string Key, string logID)
        {
            // read MessageClass.....
            // DbCrud to save the staterec table

            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = inTypeRecs.Count + 1;

            foreach (typeRec rParent in inTypeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                string[] tmpTypes = rParent.typeContent.Split((char)0x1c);
                List<typeRec> typeRecs = new List<typeRec>();
                stateRec parms = new stateRec();

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

                ///

                String sql = "";
                int loadNum = 0;
                foreach (typeRec r in typeRecs)
                {
                    // record length must be 28
                    // otherwise rest of the read data is ignored

                    if (r.typeContent.Length < 28)
                    {
                        continue;
                    }
                    if (r.typeContent.Substring(0, 3) == "000" && r.typeContent.Substring(3, 1) == "A")
                    {
                        loadNum++;
                    }

                    sql = @"INSERT INTO stateinfo([prjkey],[logkey],[rectype],[statenum],[statetype]," +
                          "[sta1],[sta2],[sta3],[sta4],[sta5],[sta6],[sta7],[sta8],[load],[logID]) " +
                          " VALUES('" + Key + "','" +
                                        r.typeIndex + "','" +
                                       'S' + "','" +
                                       r.typeContent.Substring(0, 3) + "','" +
                                       r.typeContent.Substring(3, 1) + "','" +
                                       r.typeContent.Substring(4, 3) + "','" +
                                       r.typeContent.Substring(7, 3) + "','" +
                                       r.typeContent.Substring(10, 3) + "','" +
                                       r.typeContent.Substring(13, 3) + "','" +
                                       r.typeContent.Substring(16, 3) + "','" +
                                       r.typeContent.Substring(19, 3) + "','" +
                                       r.typeContent.Substring(22, 3) + "','" +
                                       r.typeContent.Substring(25, 3) + "','" +
                                       loadNum.ToString() + "'," +
                                       logID + ")";

                    DbCrud db = new DbCrud();
                    if (db.crudToDb(sql) == false)
                        return false;
                }

                sql = @"INSERT INTO staterec([logkey],[rectype],[messageClass],[responseFlag],[luno],[messageSeqNumber],
                        [messageSubclass],[messageIdentifier],[load],[prjkey],[logID]) " +
                      " VALUES('" +
                                    rParent.typeIndex + "','" +
                                   'S' + "','" +
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

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM staterec WHERE prjkey = '" +
               projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT * FROM stateInfo WHERE prjkey = '" +
                           projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;

        }

        public virtual string parseToView_getInfo(StateData stRec, DataRow StRecDr)
        {
            // base: parseToView(); 

            string extensionStateNum = "";

            DataTable dt = new DataTable();

            log.Info("Generating state record for state " + stRec.StateType);

            StateData theRecord = LoggerFactory.Create_StateRecord(stRec.StateType);

            if (theRecord == null)
            {
                return null;
            }

            string stateTypetmp = stRec.StateType;
            string fieldData = "";
            string[] descriptionFields = new string[] { "", "", "" };

            if (stRec.StateType == "Z")
            {
                string extensionFound = theRecord.checkZExtensions(stRec);
                if (extensionFound.Length > 0)
                {
                    stateTypetmp = extensionFound.Substring(3, extensionFound.Length - 3);
                    dt = theRecord.getStateDescription(stateTypetmp);
                    extensionStateNum = extensionFound.Substring(0, 3);
                    log.Info("DataTable records = " + dt.Rows.Count);
                }

                descriptionFields[0] = "\\b Extension of " + stateTypetmp.Substring(0, 1) + " " + extensionStateNum + @"\b0  ";
                descriptionFields[1] = "";
                descriptionFields[2] = "";

                fieldData += App.Prj.insertRowRtf(descriptionFields);
            }
            else
            {

                dt = theRecord.getStateDescription(stRec.StateType);
                log.Info("DataTable records = " + dt.Rows.Count);
            }

            if (dt.Rows.Count > 0)
            {
                for (int colNum = 3; colNum < StRecDr.Table.Columns.Count - 5; colNum++)
                    fieldData += App.Prj.getOptionDescription(dt, colNum.ToString("00") + stateTypetmp, StRecDr[colNum].ToString());

                StateData stRecTmp = LoggerFactory.Create_StateRecord();
                stRecTmp = stRec;
                stRecTmp.StateType = dt.Rows[0]["subRecType"].ToString().Substring(2, dt.Rows[0]["subRecType"].ToString().Length - 2).Trim();
                theRecord.checkExtensions(stRecTmp);

                return fieldData;
            }
            else
            {
                return "";
            }

        }

        public DataTable getStateDescription(string stateType)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM [dataDescription] WHERE recType = '" + "S"
                    + "' AND SUBSTR(subRecType, 3, " + stateType.Length + ") = '" + stateType + "'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;

        }

        public virtual void checkExtensions(StateData st)
        {
            return;
        }

        public virtual string checkZExtensions(StateData st)
        {
            return null;
        }

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'S' and subRecType like 'H%' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            //string newString = base.parseToView();

            List<DataTable> dts = new List<DataTable>();
            dts = getRecord(logKey, logID, projectKey);

            DataTable paramRecDt = getDescription();

            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            bool dtFirst = true;

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dtFirst == true)
                    {
                        for (int colNum = 3; colNum < dt.Columns.Count - 3; colNum++)
                            txtField += App.Prj.getOptionDescription(paramRecDt, "H" + colNum.ToString("00"), dt.Rows[0][colNum].ToString());
                        dtFirst = false;
                    }
                    else
                    {
                        for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                        {
                            string[] descriptionFields = new string[] { "", "", "" };
                            string txtStateData = "";

                            for (int fieldNum = 3; fieldNum < dt.Columns.Count - 5; fieldNum++)
                            {
                                txtStateData += dt.Rows[rowNum][fieldNum].ToString() + " ";
                            }

                            descriptionFields[0] = "\\b State Data ";
                            descriptionFields[1] = txtStateData + @" \b0 ";
                            descriptionFields[2] = "";

                            txtField += App.Prj.insertRowRtf(descriptionFields);

                            StateData stRec = LoggerFactory.Create_StateRecord();
                            stRec.StateNumber = dt.Rows[rowNum][3].ToString();
                            stRec.StateType = dt.Rows[rowNum][4].ToString();
                            stRec.Val1 = dt.Rows[rowNum][5].ToString();
                            stRec.Val2 = dt.Rows[rowNum][6].ToString();
                            stRec.Val3 = dt.Rows[rowNum][7].ToString();
                            stRec.Val4 = dt.Rows[rowNum][8].ToString();
                            stRec.Val5 = dt.Rows[rowNum][9].ToString();
                            stRec.Val6 = dt.Rows[rowNum][10].ToString();
                            stRec.Val7 = dt.Rows[rowNum][11].ToString();
                            stRec.Val8 = dt.Rows[rowNum][12].ToString();

                            txtField += parseToView_getInfo(stRec, dt.Rows[rowNum]);

                        }

                    }
                }
            }
            return txtField;
        }
    }
}

