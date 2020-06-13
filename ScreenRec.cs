using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Logger
{
    public class screenRec : App
    {
        private string plogkey;
        private string prectype;
        private string pscrnum;
        private string pscrdata;
        private string pload;

        public bool ValidateScreen(string scrNum)
        {
            string sql = @"SELECT * FROM [screeninfo] " +
                                 "WHERE [scrnum ] ='" + scrNum + "'";

            Dictionary<string, screenRec> resultData = readData(sql);

            if (resultData.Count <= 0)
            {
                Console.WriteLine("Error screen number does not exist");
                return false;
            }
            return true;

        }

        public new Dictionary<string, screenRec> readData(string sql)
        {

            string connectionString;
            SqlConnection cnn;

            connectionString = @"Data Source = LT-JOSEPHHANDSC\MVDATA; Initial Catalog = logger; User ID=sa; Password=pa55w0rd!";
            cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                Dictionary<string, screenRec> dicData = new Dictionary<string, screenRec>();

                while (dataReader.Read())
                {
                    screenRec sr = new screenRec();
                    sr.prectype = dataReader.GetString(2);
                    sr.pscrnum = dataReader.GetString(3);
                    sr.pscrdata = dataReader.GetString(4);
                    sr.pload = dataReader.GetString(5);
                    dicData.Add(dataReader.GetString(1) + dataReader.GetInt32(0).ToString(), sr);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return dicData;
            }
            catch (Exception dbEx)
            {
                Console.WriteLine(dbEx.ToString());
                return null;
            }
        }

        public bool writeData(List<typeRec> typeRecs, string Key, string logID)
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
                    string scrdata = null;
                    string scrnum = null;
                    if (r.typeContent.Length < 3)
                    {
                        continue;
                    }
                    scrnum = r.typeContent.Substring(0, 3);

                    if (r.typeContent.Substring(0, 3) == "C00")
                    {
                        loadNum++;
                    }

                    if (r.typeContent.Length > 3)
                    {
                        scrdata = r.typeContent.Substring(3, r.typeContent.Length - 3);
                        scrdata = scrdata.Replace(@"'", @"''");
                    }

                    sql = @"INSERT INTO screeninfo([logkey],[rectype],[scrnum],[scrdata],[load],[prjkey],[logID])" +
                          " VALUES('" + r.typeIndex + "','" +
                                       'C' + "','" +
                          r.typeContent.Substring(0, 3) + "','" + // screenNum
                                       scrdata + "','" + // screenData
                                       loadNum.ToString() + "','" +
                                       Key + "'," + logID + ")";

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
