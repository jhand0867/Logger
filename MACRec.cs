using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Logger
{
    class MACRec
    {

        // to be completed
        public void writeMAC(List<typeRec> typeRecs)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = @"Data Source = LT-JOSEPHHANDSC\MVDATA; Initial Catalog = logger; User ID=sa; Password=pa55w0rd!";
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


                    sql = @"INSERT INTO DateTime([logkey],[rectype],[date],[time],[load])" +
                          " VALUES('" + r.typeIndex + "','" +
                                       '8' + "','" +
                                        r.typeContent.Substring(0, 6) + "','" + // date
                                        r.typeContent.Substring(6, 4) + "','" + // time
                                        loadNum.ToString() + "')";

                    command = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                    // cnn.Close();
                }
                cnn.Close();
            }

            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());

            }

        }

    }
}
