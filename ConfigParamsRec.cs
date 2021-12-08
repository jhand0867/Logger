using System;
using System.Collections.Generic;
using System.Data;

namespace Logger
{

    public class configParamsRec : App, IMessage
    {
        public struct timerRec
        {
            public string timerNum;
            public string timerTics;
        };

        public struct configParams
        {
            public string camera;
            public string cardReaderError;
            public string reserved1;
            public string reserved2;
            public string trackWriteError;
            public string supply;
            public string reserved3;
            public string luno;
            public timerRec[] timers;
        };

        public bool ValidateConfigParams(string logKey)
        {
            string sql = @"SELECT * FROM [configParamsInfo] " +
                                 "WHERE [logkey ] ='" + logKey + "'";

            Dictionary<string, configParams> resultData = readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error Configuration Parameters number does not exist");
                return false;
            }
            return true;

        }

        public new Dictionary<string, configParams> readData(string sql)
        {
            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            Dictionary<string, configParams> dicData = new Dictionary<string, configParams>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    configParams cp = new configParams();
                    cp.camera = row[2].ToString();
                    dicData.Add(row[1].ToString() + Convert.ToInt32(row[0]).ToString(), cp);
                }
            }
            return dicData;
        }

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            LoggerProgressBar1.LoggerProgressBar1 lpb = getLoggerProgressBar();
            lpb.LblTitle = this.ToString();
            lpb.Maximum = (typeRecs.Count / 3) + 1;

            DbCrud db = new DbCrud();
            int loadNum = 0;
            int configCount = typeRecs.Count / 3;
            int count = 0;
            while (loadNum < configCount)
            {
                lpb.Value += lpb.Step;
                lpb.ValueUpdated(lpb.Value);

                typeRec r = typeRecs[count];
                configParams parms = new configParams();

                parms.camera = r.typeContent.Substring(0, 1);
                parms.cardReaderError = r.typeContent.Substring(1, 3);
                parms.reserved1 = r.typeContent.Substring(4, 3);
                parms.reserved2 = r.typeContent.Substring(7, 3);
                parms.trackWriteError = r.typeContent.Substring(10, 3);
                parms.supply = r.typeContent.Substring(13, 3);
                parms.reserved3 = r.typeContent.Substring(16, 9);

                count++;
                r = typeRecs[count];
                parms.luno = r.typeContent;

                count++;
                r = typeRecs[count];
                int timersNum = r.typeContent.Length / 5;

                parms.timers = new timerRec[timersNum];

                for (int x = 0, y = 0; y < timersNum; x = x + 5, y++)
                {
                    parms.timers[y].timerNum = r.typeContent.Substring(x, 2);
                    parms.timers[y].timerTics = r.typeContent.Substring(x + 2, 3);
                }

                count++;
                loadNum++;

                // childs of configParamsInfo
                string sql;
                for (int y = 0; y < timersNum; y++)
                {
                    sql = @"INSERT INTO configParamsTimers([logkey],[rectype],[timerNum],[timerTics],[logID]) ";
                    sql = sql + @" VALUES('" + r.typeIndex + "','P',";
                    sql = sql + "'" + parms.timers[y].timerNum + "',";
                    sql = sql + "'" + parms.timers[y].timerTics + "'," + logID + ")";

                    if (db.crudToDb(sql) == false)
                        return false;

                }

                // save the timers parent record

                sql = @"INSERT INTO configParamsInfo([logkey],[rectype],[camera],[cardReaderError],[reserved1]," +
                        "[reserved2],[trackWriteError],[supply],[reserved3],[luno],[timersCount],[load],[prjkey],[logID])" +
                       " VALUES('" + r.typeIndex + "','" + // key
                                    'P' + "','" + // record type
                                    parms.camera + "','" +
                                    parms.cardReaderError + "','" +
                                    parms.reserved1 + "','" +
                                    parms.reserved2 + "','" +
                                    parms.trackWriteError + "','" +
                                    parms.supply + "','" +
                                    parms.reserved3 + "','" +
                                    parms.luno + "','" +
                                    timersNum.ToString() + "','" +
                                    loadNum.ToString() + "','" +
                                    key + "'," +
                                    logID + ")";

                if (db.crudToDb(sql) == false)
                    return false;
            }
            lpb.Visible = false;
            return true;
        }

        public List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            List<DataTable> dts = new List<DataTable>();
            DbCrud db = new DbCrud();

            string sql = @"SELECT * FROM configParamsInfo WHERE prjkey = '" + projectKey + "' AND logID = '" + logID + "' AND logkey LIKE '" +
                                                               logKey + "%'";
            DataTable dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            sql = @"SELECT * FROM configParamsTimers WHERE logID = '" + logID + "' AND logkey LIKE '" +
                                                   logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            return dts;
        }

        public DataTable getDescription()
        {
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'P' ";

            DbCrud db = new DbCrud();
            DataTable dt = db.GetTableFromDb(sql);
            return dt;
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            List<DataTable> dts = getRecord(logKey, logID, projectKey);
            string txtField = "";

            if (dts == null || dts[0].Rows.Count == 0) { return txtField; }

            DataTable configDt = getDescription();

            foreach (DataTable dt in dts)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    {
                        for (int fieldNum = 3; fieldNum < dt.Columns.Count - 5; fieldNum++)
                        {
                            if (fieldNum == 11)
                            {
                                if (dts[1].Rows.Count > 0)
                                {
                                    txtField += @"==================================================" + System.Environment.NewLine;
                                    txtField += @"TIMERS" + System.Environment.NewLine;
                                    txtField += @"==================================================" + System.Environment.NewLine;
                                    txtField += getTimers(dts[1], configDt);
                                }
                                continue;
                            }
                            string fieldContent = dt.Rows[rowNum][fieldNum].ToString();
                            if (fieldContent == "")
                                continue;
                            else
                                txtField += getOptionDescription(configDt, fieldNum.ToString("00"), fieldContent);
                        }
                    }
                }
                break;
            }
            return txtField;
        }

        private string getTimers(DataTable dt, DataTable configDt)
        {
            string timers = "";
            if (dt.Rows.Count > 0)
                for (int rowNum = 0; rowNum < dt.Rows.Count; rowNum++)
                    timers += getOptionDescription(configDt, "T" + dt.Rows[rowNum][3].ToString(), dt.Rows[rowNum][4].ToString());

            return timers;
        }
    }
}

