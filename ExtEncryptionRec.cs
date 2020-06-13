using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Logger
{
    class ExtEncryptionRec : App
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
                    if (r.typeContent.Length < 3)
                    {
                        continue;
                    }

                    loadNum++;


                    sql = @"INSERT INTO extEncryption([logkey],[rectype],[keySize],[keyData],[load],[prjkey],[logID])" +
                          " VALUES('" + r.typeIndex + "','" +
                                       'X' + "','" +
                                        r.typeContent.Substring(0, 3) + "','" + // key data size
                                        r.typeContent.Substring(3, r.typeContent.Length - 3) + "','" + // Key Data
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
