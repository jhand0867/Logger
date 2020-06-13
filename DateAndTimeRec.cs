using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Logger
{
    class DateAndTimeRec : App
    {
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
                String sql = "";
                int loadNum = 0;
                foreach (typeRec r in typeRecs)
                {
                    if (r.typeContent.Length < 10)
                    {
                        continue;
                    }

                    loadNum++;


                    sql = @"INSERT INTO DateTime([logkey],[rectype],[date],[time],[load],[prjkey],[logID])" +
                          " VALUES('" + r.typeIndex + "','" +
                                       'D' + "','" +
                                        r.typeContent.Substring(0, 6) + "','" + // date
                                        r.typeContent.Substring(6, 4) + "','" + // time
                                        loadNum.ToString() + "','" + key + "'," + logID + ")";

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

    }
}
