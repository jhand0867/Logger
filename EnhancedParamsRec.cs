using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logger
{
    class EnhancedParamsRec : App
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
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

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

                        command = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();

                    }

                    sql = "";
                    for (int y = 0; y < timersNum; y++)
                    {
                        sql = @"INSERT INTO enhancedTimers([logkey],[rectype],[timerNum],[timerSeconds],[logID]) ";
                        sql = sql + @" VALUES('" + r.typeIndex + "','C',";
                        sql = sql + "'" + parms.timers[y].timerNum + "',";
                        sql = sql + "'" + parms.timers[y].timerTics + "'," + logID + ")";

                        command = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                        dataAdapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();

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

                    command = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                    // cnn.Close();
                }
                cnn.Close();
                return true;
            }

            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return false;

            }

        }

        internal List<DataTable> getRecord(string logKey, string logID, string projectKey)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            List<DataTable> dts = new List<DataTable>();
            try
            {
                cnn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT id, logkey, rectype, optionNum as Num, optionCode as Code, '1' as type from enhancedParams
                                                        WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'", cnn))

                {
                    sda.Fill(dt);
                }
                dts.Add(dt);
                dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT id, logkey, rectype, timerNum as Num, timerSeconds as Code, '2' as type from enhancedTimers
                                                        WHERE logID = '" + logID + "' AND logkey LIKE '" + logKey + "%'", cnn))

                {
                    sda.Fill(dt);

                }
                dts.Add(dt);
                return dts;
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }
        }

        internal DataTable getDescription()
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = ConfigurationManager.ConnectionStrings["LoggerDB"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();


            // is the calling state a Y
            // get the info from DataDescription
            // send it back in a string 
            try
            {
                cnn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [dataDescription] WHERE recType = '" + "C"
                    + "'", cnn))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }

            return dt; ;
        }
    }
}
