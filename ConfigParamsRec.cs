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
            DataTable dt = new DataTable();
            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
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
            DbCrud db = new DbCrud();
            int loadNum = 0;
            int configCount = typeRecs.Count / 3;
            int count = 0;
            while (loadNum < configCount)
            {
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
                string sql = "";
                for (int y = 0; y < timersNum; y++)
                {
                    sql = @"INSERT INTO configParamsTimers([logkey],[rectype],[timerNum],[timerTics],[logID]) ";
                    sql = sql + @" VALUES('" + r.typeIndex + "','C',";
                    sql = sql + "'" + parms.timers[y].timerNum + "',";
                    sql = sql + "'" + parms.timers[y].timerTics + "'," + logID + ")";

                    if (db.crudToDb(sql) == false)
                        return false;

                }

                // save the timers parent record

                sql = @"INSERT INTO configParamsInfo([logkey],[rectype],[camera],[cardReaderError],[reserved1]," +
                        "[reserved2],[trackWriteError],[supply],[reserved3],[luno],[timersCount],[load],[prjkey],[logID])" +
                       " VALUES('" + r.typeIndex + "','" + // key
                                    'C' + "','" + // record type
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
            return true;


        }

        List<DataTable> IMessage.getRecord(string logKey, string logID, string projectKey)
        {
            throw new NotImplementedException();
        }

        DataTable IMessage.getDescription()
        {
            throw new NotImplementedException();
        }

        public string parseToView(string logKey, string logID, string projectKey, string recValue)
        {
            throw new NotImplementedException();
        }
    }
}

