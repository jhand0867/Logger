using System.Collections.Generic;
using System.Data;

namespace Logger
{
    class EnhancedParamsRec : App, IMessage
    {
        private bool timerStartFlag;

        public bool TimerStartFlag { get => timerStartFlag; set => timerStartFlag = value; }

        public struct parameterAndValue
        {
            public string paramName;
            public string paramValue;
        };
        public struct timerRec
        {
            public string timerNum;
            public string timerTics;
        }

        struct enhancedParams
        {
            private string messageClass;
            private string responseFlag;
            private string messageLuno;
            private string messageSeqNumber;
            private string messageSubclass;
            private string messageIdentifier;
            private string luno;
            private parameterAndValue[] options;
            private timerRec[] timers;

            public string MessageClass { get => messageClass; set => messageClass = value; }
            public string ResponseFlag { get => responseFlag; set => responseFlag = value; }
            public string MessageLuno { get => messageLuno; set => messageLuno = value; }
            public string MessageSeqNumber { get => messageSeqNumber; set => messageSeqNumber = value; }
            public string MessageSubclass { get => messageSubclass; set => messageSubclass = value; }
            public string MessageIdentifier { get => messageIdentifier; set => messageIdentifier = value; }
            public string Luno { get => luno; set => luno = value; }
            internal parameterAndValue[] Options { get => options; set => options = value; }
            internal timerRec[] Timers { get => timers; set => timers = value; }

        }
        public bool writeData(List<typeRec> inTypeRecs, string key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = inTypeRecs.Count + 1;

            DbCrud db = new DbCrud();
            int loadNum = 0;

            foreach (typeRec rParent in inTypeRecs)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                enhancedParams parms = new enhancedParams();

                // MLH part added starts
                string[] tmpTypes = rParent.typeContent.Split((char)0x1c);
                List<typeRec> typeRecs = new List<typeRec>();

                int myInd = tmpTypes[0].Length + tmpTypes[1].Length + tmpTypes[2].Length + tmpTypes[3].Length;
                string typeData = rParent.typeContent.Substring(myInd + 4, rParent.typeContent.Length - (myInd + 4));

                string[] dataTypes = typeData.Split((char)0x1c);

                parms.MessageClass = tmpTypes[0].Substring(10, 1);
                if (tmpTypes[0].Length > 11)
                {
                    parms.ResponseFlag = tmpTypes[0].Substring(tmpTypes[0].Length - 1, 1);
                }

                parms.MessageLuno = tmpTypes[1];
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

                // MLH part added end


                int count = 0;

                while (count < typeRecs.Count)
                {

                    typeRec r = typeRecs[count];
                    //  enhancedParams parms = new enhancedParams();

                    parms.Luno = r.typeContent;
                    count++;

                    int optionsCount = 0;

                    if (typeRecs[count - 1].typeIndex == typeRecs[count].typeIndex)
                    {
                        r = typeRecs[count];
                        optionsCount = r.typeContent.Length / 5;

                        parms.Options = new parameterAndValue[optionsCount];

                        for (int x = 0, y = 0; y < optionsCount; x = x + 5, y++)
                        {
                            parms.Options[y].paramName = r.typeContent.Substring(x, 2);
                            parms.Options[y].paramValue = r.typeContent.Substring(x + 2, 3);
                        }

                        count++;
                    }

                    int timersNum = 0;

                    if ((count < typeRecs.Count) &&
                       (typeRecs[count - 1].typeIndex == typeRecs[count].typeIndex))
                    {
                        r = typeRecs[count];
                        timersNum = r.typeContent.Length / 5;

                        parms.Timers = new timerRec[timersNum];

                        for (int x = 0, y = 0; y < timersNum; x = x + 5, y++)
                        {
                            parms.Timers[y].timerNum = r.typeContent.Substring(x, 2);
                            parms.Timers[y].timerTics = r.typeContent.Substring(x + 2, 3);
                        }

                        count++;
                    }
                    loadNum++;

                    // childs of EnhanedParamsInfo

                    string sql;
                    for (int y = 0; y < optionsCount; y++)
                    {
                        sql = @"INSERT INTO enhancedParams([logkey],[rectype],[optionNum],[optionCode],[logID]) ";
                        sql = sql + @" VALUES('" + r.typeIndex + "','C',";
                        sql = sql + "'" + parms.Options[y].paramName + "',";
                        sql = sql + "'" + parms.Options[y].paramValue + "'," + logID + ")";


                        if (db.crudToDb(sql) == false)
                            return false;

                    }

                    for (int y = 0; y < timersNum; y++)
                    {
                        sql = @"INSERT INTO enhancedTimers([logkey],[rectype],[timerNum],[timerSeconds],[logID]) ";
                        sql = sql + @" VALUES('" + r.typeIndex + "','C',";
                        sql = sql + "'" + parms.Timers[y].timerNum + "',";
                        sql = sql + "'" + parms.Timers[y].timerTics + "'," + logID + ")";

                        if (db.crudToDb(sql) == false)
                            return false;

                    }

                    // save the timers parent record

                    sql = @"INSERT INTO enhancedParamsInfo([logkey],[rectype],[messageClass],
	                         [responseFlag],[messageLuno],[messageSeqNumber],[messageSubclass],[messageIdentifier],
                             [luno],[paramsCount],[timersCount],[load],[prjkey],[logID])" +
                           " VALUES('" + r.typeIndex + "','" + // key
                                        "C" + "','" + // record type
                                        parms.MessageClass + "','" +
                                        parms.ResponseFlag + "','" +
                                        parms.Luno + "','" +
                                        parms.MessageSeqNumber + "','" +
                                        parms.MessageSubclass + "','" +
                                        parms.MessageIdentifier + "','" +
                                        parms.Luno + "','" +
                                        optionsCount.ToString() + "','" +
                                        timersNum.ToString() + "','" +
                                        loadNum.ToString() + "','" +
                                        key + "'," + logID + ")";

                    if (db.crudToDb(sql) == false)
                        return false;
                }
            }
            lpb.Visible = false;
            return true;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM enhancedParamsInfo WHERE prjkey = '" + projectKey + "' AND logID = '" + logID +
                                   "' AND logkey LIKE '" + logKey + "%' LIMIT 1";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT id, logkey, rectype, optionNum as Num, optionCode as Code, '1' as type from enhancedParams
                                                        WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT id, logkey, rectype, timerNum as Num, timerSeconds as Code, '2' as type from enhancedTimers
                                                        WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;

        }

        public DataTable getDescription()
        {
            // todo: need to add remaining O and T in the dataDescription

            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'C' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";
            string[] descriptionFields = new string[] { "", "", "" };

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable paramRecDt = getDescription();

            bool dtFirst = true;

            foreach (DataTable dt in dts)
            {
                if (dtFirst == true)
                {
                    for (int colNum = 3; colNum < dt.Columns.Count - 7; colNum++)
                        txtField += App.Prj.getOptionDescription(paramRecDt, "H" + colNum.ToString("00"), dt.Rows[0][colNum].ToString());
                    dtFirst = false;
                }
                else
                {
                    for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    {
                        if (dt.Rows[rowNum][5].ToString() == "1")
                        {
                            if (rowNum == 0)
                            {
                                descriptionFields[0] = @"\b OPTIONS \b0";
                                descriptionFields[1] = "";
                                descriptionFields[2] = "";

                                txtField += App.Prj.insertRowRtf(descriptionFields);
                            }
                            txtField += getOptionDescription(paramRecDt, "O" + dt.Rows[rowNum][3].ToString(), dt.Rows[rowNum][4].ToString());
                        }
                        if (dt.Rows[rowNum][5].ToString() == "2")
                        {
                            if (TimerStartFlag != true)
                            {
                                descriptionFields[0] = @"\b TIMERS \b0";
                                descriptionFields[1] = "";
                                descriptionFields[2] = "";

                                txtField += App.Prj.insertRowRtf(descriptionFields);
                                TimerStartFlag = true;
                            }
                            txtField += getOptionDescription(paramRecDt, "T" + dt.Rows[rowNum][3].ToString(), dt.Rows[rowNum][4].ToString());
                        }
                    }
                }

            }
            return txtField;
        }
    }
}
