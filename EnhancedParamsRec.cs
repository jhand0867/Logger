﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    class EnhancedParamsRec : App, IMessage
    {
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

        public struct enhancedParams
        {
            public string luno;
            public parameterAndValue[] options;
            public timerRec[] timers;

        };

        public bool writeData(List<typeRec> typeRecs, string key, string logID)
        {
            DbCrud db = new DbCrud();
            int loadNum = 0;
                int count = 0;
                
            while (count < typeRecs.Count)
                {
                    typeRec r = typeRecs[count];
                    enhancedParams parms = new enhancedParams();

                    parms.luno = r.typeContent;
                    count++;

                    int optionsCount = 0;

                    if (typeRecs[count - 1].typeIndex == typeRecs[count].typeIndex)
                    {
                        r = typeRecs[count];
                        optionsCount = r.typeContent.Length / 5;

                        parms.options = new parameterAndValue[optionsCount];

                        for (int x = 0, y = 0; y < optionsCount; x = x + 5, y++)
                        {
                            parms.options[y].paramName = r.typeContent.Substring(x, 2);
                            parms.options[y].paramValue = r.typeContent.Substring(x + 2, 3);
                        }

                        count++;
                    }

                    int timersNum = 0;

                    if (typeRecs[count - 1].typeIndex == typeRecs[count].typeIndex)
                    {

                        r = typeRecs[count];
                        timersNum = r.typeContent.Length / 5;

                        parms.timers = new timerRec[timersNum];

                        for (int x = 0, y = 0; y < timersNum; x = x + 5, y++)
                        {
                            parms.timers[y].timerNum = r.typeContent.Substring(x, 2);
                            parms.timers[y].timerTics = r.typeContent.Substring(x + 2, 3);
                        }

                        count++;
                    }
                    loadNum++;

                    // childs of EnhanedParamsInfo

                    string sql = "";
                    for (int y = 0; y < optionsCount; y++)
                    {
                        sql = @"INSERT INTO enhancedParams([logkey],[rectype],[optionNum],[optionCode],[logID]) ";
                        sql = sql + @" VALUES('" + r.typeIndex + "','C',";
                        sql = sql + "'" + parms.options[y].paramName + "',";
                        sql = sql + "'" + parms.options[y].paramValue + "'," + logID + ")";


                    if (db.addToDb(sql) == false)
                        return false;

                }

                    sql = "";
                    for (int y = 0; y < timersNum; y++)
                    {
                        sql = @"INSERT INTO enhancedTimers([logkey],[rectype],[timerNum],[timerSeconds],[logID]) ";
                        sql = sql + @" VALUES('" + r.typeIndex + "','C',";
                        sql = sql + "'" + parms.timers[y].timerNum + "',";
                        sql = sql + "'" + parms.timers[y].timerTics + "'," + logID + ")";

                    if (db.addToDb(sql) == false)
                        return false;

                }

                    // save the timers parent record

                    sql = @"INSERT INTO enhancedParamsInfo([logkey],[rectype],[luno],[paramsCount],[timersCount],[load],[prjkey],[logID])" +
                           " VALUES('" + r.typeIndex + "','" + // key
                                        'C' + "','" + // record type
                                        parms.luno + "','" +
                                        optionsCount.ToString() + "','" +
                                        timersNum.ToString() + "','" +
                                        loadNum.ToString() + "','" +
                                        key + "'," + logID + ")";

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

            string sql = @"SELECT id, logkey, rectype, optionNum as Num, optionCode as Code, '1' as type from enhancedParams
                                                        WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);

            // dt = new DataTable();
            sql = @"SELECT id, logkey, rectype, timerNum as Num, timerSeconds as Code, '2' as type from enhancedTimers
                                                        WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'";
            dt = db.GetTableFromDb(sql);
            dts.Add(dt);
            return dts;

        }

        public DataTable getDescription()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT* FROM[dataDescription] WHERE recType = 'C' ";

            DbCrud db = new DbCrud();
            dt = db.GetTableFromDb(sql);
            return dt;
        }

     }
}
