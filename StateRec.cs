using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    public class StateRec : App, IMessage, IStateExtension

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


        public string stateNum   // property
        => pStateNum;
        public string stateType   // property
        => pStateType;
        public string sta1   // property
        => pSta1;
        public string sta2   // property
        => pSta2;
        public string sta3   // property
        => pSta3;
        public string sta4   // property
        => pSta4;
        public string sta5   // property
        => pSta5;
        public string sta6   // property
        => pSta6;
        public string sta7   // property
        => pSta7;
        public string sta8   // property
        => pSta8;

        public virtual void ValidateState(StateRec stateData)
        {
            string stateTypes = @"ABCDEFGHIJKLMNORSTVWXYZbdefgkmwz->&z";
            if (!(stateTypes.Contains(stateData.stateType)))
            {
                Console.WriteLine("Not a valid state type");
            }
        }

        public Dictionary<string, StateRec> ValidateStateNumber(string value)
        {
            int stateNum = Convert.ToInt32(value);
            if (!(stateNum >= 0 && stateNum <= 999))
            {
                Console.WriteLine("Value not in range 000 to 999");
                return null;
            }

            string sql = @"SELECT * FROM [stateinfo] " +
                          "WHERE [stateNum ] ='" + value + "'";

            Dictionary<string, StateRec> resultData = this.readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error state does not exist");
                return null;
            }
            return resultData;
        }

        public new Dictionary<string, StateRec> readData(string sql)
        {
            // here mlh

            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            Dictionary<string, StateRec> dicData = new Dictionary<string, StateRec>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    StateRec sr = new StateRec();
                    sr.pStateNum = row[3].ToString();
                    sr.pStateType = row[4].ToString();
                    sr.pSta1 = row[5].ToString();
                    sr.pSta2 = row[6].ToString();
                    sr.pSta3 = row[7].ToString();
                    sr.pSta4 = row[8].ToString();
                    sr.pSta5 = row[9].ToString();
                    sr.pSta6 = row[10].ToString();
                    sr.pSta7 = row[11].ToString();
                    sr.pSta8 = row[12].ToString();
                    dicData.Add(row[1].ToString() + Convert.ToInt64(row[0]).ToString(), sr);
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
                    // MAC lenght
                    if (r.typeContent.Length == 8)
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
                    if (db.addToDb(sql) == false)
                        return false;
                }
                return true;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM stateInfo WHERE prjkey = '" +
                                                               projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;

        }

        public virtual string getInfo(StateRec stRec)
        {
            string stateType = stRec.StateType;

            string stateNum = stRec.StateNumber;

            DataTable dt = new DataTable();

            log.Info("Generating state record for statey " + stRec.StateType);

            StateRec theRecord = MessageFactory.Create_StateRecord(stRec.StateType);
    
            if (theRecord == null)
            {
                return null;
            }

            if (stRec.StateType == "Z")
            {
                string extensionFound = theRecord.checkZExtensions(stRec);
                if (extensionFound.Length > 0)
                {
                    dt = theRecord.getStateDescription(extensionFound.Substring(3, extensionFound.Length - 3));
                    stateNum = extensionFound.Substring(0, 3);
                    log.Info("DataTable records = " + dt.Rows.Count);
                }
            }
            else
            {

                dt = theRecord.getStateDescription(stRec.StateType);

                log.Info("DataTable records = " + dt.Rows.Count);
            }

            string stateTypetmp = "";

            if (dt.Rows.Count > 0)
            {
                stateTypetmp = dt.Rows[0]["subRecType"].ToString().Trim();

                string fieldData = dt.Rows[0][3].ToString().Trim() + ":\t" + stRec.StateNumber + System.Environment.NewLine;

                if (stateTypetmp == stRec.StateType)
                    fieldData += dt.Rows[1][3].ToString().Trim() + ":\t" + stRec.StateType + System.Environment.NewLine;
                else
                {
                    if (stateTypetmp.Length > 2) stateTypetmp = stRec.StateType;
                    fieldData += dt.Rows[1][3].ToString().Trim() + ":\t" + stRec.StateType + " extension of " + stateTypetmp.Substring(0, 1) + " " + stateNum + System.Environment.NewLine;
                }
                fieldData += dt.Rows[2][3].ToString().Substring(0, 40) + " " + stRec.Val1 + insertDescription(dt.Rows[2][4].ToString());
                fieldData += dt.Rows[3][3].ToString().Substring(0, 40) + " " + stRec.Val2 + insertDescription(dt.Rows[3][4].ToString());
                fieldData += dt.Rows[4][3].ToString().Substring(0, 40) + " " + stRec.Val3 + insertDescription(dt.Rows[4][4].ToString());
                fieldData += dt.Rows[5][3].ToString().Substring(0, 40) + " " + stRec.Val4 + insertDescription(dt.Rows[5][4].ToString());
                fieldData += dt.Rows[6][3].ToString().Substring(0, 40) + " " + stRec.Val5 + insertDescription(dt.Rows[6][4].ToString());
                fieldData += dt.Rows[7][3].ToString().Substring(0, 40) + " " + stRec.Val6 + insertDescription(dt.Rows[7][4].ToString());
                fieldData += dt.Rows[8][3].ToString().Substring(0, 40) + " " + stRec.Val7 + insertDescription(dt.Rows[8][4].ToString());
                fieldData += dt.Rows[9][3].ToString().Substring(0, 40) + " " + stRec.Val8 + insertDescription(dt.Rows[9][4].ToString());
                fieldData += System.Environment.NewLine;

                // is there extension information on the val
                //

                StateRec stRecTmp = new StateRec();
                stRecTmp = stRec;
                stRecTmp.StateType = dt.Rows[0]["subRecType"].ToString().Trim();
 //               currentState.checkExtensions(stRecTmp);
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
                    + "' AND subRecType = '" + stateType + "'";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;

        }

        private string insertDescription(string fieldDescription)
        {
            string description = "";

            if (fieldDescription != "")
            {
                description += System.Environment.NewLine + fieldDescription.Trim() + System.Environment.NewLine;
            }
            else
            {
                description += fieldDescription.Trim() + System.Environment.NewLine;
            }
            return description;
        }

        public virtual void checkExtensions(StateRec st)
        {
        }

        public virtual string checkZExtensions(StateRec st)
        {
            return null;
        }

        DataTable IMessage.getDescription()
        {
            throw new NotImplementedException();
        }
    };
}

